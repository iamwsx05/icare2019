using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.middletier.HIS;//HISMedStore_SVC.dll

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// clsDoctorWorkStationSvc ҽ������վ�м��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDoctorWorkStationSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ����
        public clsDoctorWorkStationSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region �����շ���Ŀ
        [AutoComplete]
        private void m_mthGetType(string ID, ref string strType)
        {
            bool blnTmp = true;
            try
            {
                if (ID.StartsWith("%"))
                {
                    ID = ID.Replace("%", "");
                }
                long lng = Convert.ToInt64(ID.Replace("/", ""));
            }
            catch { blnTmp = false; }
            if (blnTmp)
            {
                strType = "itemcode_vchr";
            }
        }

        /// <summary>
        /// ������ҩ�շ���Ŀ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindWMedicineByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, string p_strRealMedStoreID, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select     a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
                                         a.itemopunit_chr, a.itemipunit_chr, b.deptprep_int, a.dosageunit_chr,
                                         a.packqty_dec, {0}, {1}, 
                                         a.tradeprice_mny,round (a.tradeprice_mny / a.packqty_dec, 4) subtrademoney,
                                         a." + strType.ToLower() + @" type, b.noqtyflag_int,
                                         b.mindosage_dec, b.maxdosage_dec, b.adultdosage_dec,
                                         b.childdosage_dec, b.nmldosage_dec, b.hype_int, a.opchargeflg_int,
                                         a.usageid_chr, a.itemcommname_vchr, b.ispoison_chr,
                                         b.isanaesthesia_chr, b.ischlorpromazine_chr, b.ischlorpromazine2_chr,
                                         a.itemopinvtype_chr, c.usagename_vchr, a.dosage_dec, a.itemcode_vchr,
                                         f.precent_dec, g.partname, h.freqid_chr as freqid,
                                         h.freqname_chr as freqname, h.times_int as freqtimes,
                                         h.days_int as freqdays, y.typename_vchr as ybtypename
                                    from t_bse_chargeitem a,
                                         t_bse_medicine b,
                                         t_bse_usagetype c,
                                         t_bse_chargecatmap d,
                                         (select itemid_chr, precent_dec
                                            from t_aid_inschargeitem
                                           where copayid_chr = ?) f,
                                         ar_apply_partlist g,
                                         t_aid_recipefreq h,
                                         t_aid_medicaretype y
                                   where a.itemsrcid_vchr = b.medicineid_chr(+)
                                     and (upper (a." + strType.ToLower() + @") like ? or upper (a.itemopcode_chr) like ?)
                                     and a.ifstop_int = 0
                                     and b.ifstop_int = 0
                                     and a.insurancetype_vchr = y.typeid_chr(+)
                                     and a.usageid_chr = c.usageid_chr(+)
                                     and a.itemopinvtype_chr = d.catid_chr(+)
                                     and d.groupid_chr = '0001'
                                     and d.internalflag_int = 0
                                     and a.itemid_chr = f.itemid_chr(+)
                                     and a.itemchecktype_chr = g.partid(+)
                                     and a.freqid_chr = h.freqid_chr(+)
                                     and exists (select 1
                                                      from t_ds_storage t1, t_ds_storage_detail t2
                                                     where t1.medicineid_chr = t2.medicineid_chr
                                                       and t1.medicineid_chr = b.medicineid_chr
                                                       and t1.drugstoreid_chr = t2.drugstoreid_chr
                                                       and t1.drugstoreid_chr = ?
                                                       and t1.noqtyflag_int = 0
                                                       and t1.ifstop_int = 0
                                                       and t2.canprovide_Int = 1
                                                       and t2.iprealgross_Int > 0)
                                                      order by a." + strType.ToLower();

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end) as submoney");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4) as submoney");

            //�ԡ�/����ͷ�ǲ�ѯ����ҩ
            if (ID.IndexOf("/") > -1)
            {
                strSQL = @"select distinct a.itemid_chr,
                                    a.itemname_vchr,
                                    a.itemspec_vchr,
                                    a.itemengname_vchr,
                                    a.selfdefine_int,
                                    a.itemchecktype_chr,
                                    a.itemopunit_chr,
                                    a.itemipunit_chr,
                                    b.deptprep_int,
                                    a.dosageunit_chr,
                                    a.packqty_dec,
                                    {0}, 
                                    {1},                                    
                                    a.itemcode_vchr type,
                                    b.noqtyflag_int,
                                    b.mindosage_dec,
                                    b.maxdosage_dec,
                                    b.adultdosage_dec,
                                    b.childdosage_dec,
                                    b.nmldosage_dec,
                                    b.hype_int,
                                    a.opchargeflg_int,
                                    a.usageid_chr,
                                    a.itemcommname_vchr,
                                    b.ispoison_chr,
                                    b.isanaesthesia_chr,
                                    b.ischlorpromazine_chr,
                                    b.ischlorpromazine2_chr,
                                    a.itemopinvtype_chr,
                                    c.usagename_vchr,
                                    a.dosage_dec,
                                    a.itemcode_vchr,
                                    f.precent_dec,
                                    g.partname,
                                    i.freqid_chr as freqid,
                                    i.freqname_chr as freqname,
                                    i.times_int as freqtimes,
                                    i.days_int as freqdays,
                                    y.typename_vchr as ybtypename
                              from t_bse_chargeitem a,
                                   t_bse_medicine b,
                                   t_bse_usagetype c,
                                   (select seqid_chr,create_dat,deptid_chr,itemid_chr,createrid_chr,privilege_int,type_int 
                                      from t_aid_comusechargeitem
                                     where createrid_chr = ?
                                       and type_int = 0
                                    union
                                    select a.seqid_chr,a.create_dat,a.deptid_chr,a.itemid_chr,a.createrid_chr,a.privilege_int,a.type_int 
                                      from t_aid_comusechargeitem a,
                                           (select a.deptid_chr
                                              from t_bse_deptemp a
                                             where a.end_dat is null
                                               and a.empid_chr = ?) b
                                     where a.deptid_chr = b.deptid_chr
                                       and type_int = 0) d,
                                   (select precent_dec,itemid_chr,copayid_chr from t_aid_inschargeitem where copayid_chr = ?) f,
                                   ar_apply_partlist g,
                                   t_bse_chargecatmap h,
                                   t_aid_recipefreq i,
                                   t_aid_medicaretype y
                             where a.itemsrcid_vchr = b.medicineid_chr(+)
                               and a.ifstop_int = 0
                               and a.insurancetype_vchr = y.typeid_chr(+)
                               and a.usageid_chr = c.usageid_chr(+)
                               and a.itemid_chr = d.itemid_chr
                               and a.itemchecktype_chr = g.partid(+)
                               and a.itemopinvtype_chr = h.catid_chr(+)
                               and h.groupid_chr = '0001'
                               and a.freqid_chr = i.freqid_chr(+)
                               and a.itemid_chr = f.itemid_chr(+) 
                               and (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?)
                               and exists (select 1
                              from t_ds_storage t1, t_ds_storage_detail t2
                             where t1.medicineid_chr = t2.medicineid_chr
                               and t1.medicineid_chr = b.medicineid_chr
                               and t1.drugstoreid_chr = t2.drugstoreid_chr
                               and t1.drugstoreid_chr = ?
                               and t1.noqtyflag_int = 0
                               and t1.ifstop_int = 0
                               and t2.canprovide_Int = 1
                               and t2.iprealgross_Int > 0)
                             order by a.itemcode_vchr";

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end) as submoney");
                else
                    strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4) as submoney");

                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = strEmployID;
                ParamArr[1].Value = strEmployID;
                ParamArr[2].Value = strPatientTypeID;
                ParamArr[3].Value = ID.Replace("/", "") + "%";
                ParamArr[4].Value = ID.Replace("/", "") + "%";
                ParamArr[5].Value = p_strRealMedStoreID.Trim();
            }
            else
            {
                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = ID + "%";
                ParamArr[2].Value = ID + "%";
                ParamArr[3].Value = p_strRealMedStoreID.Trim();
            }

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        /// <summary>
        /// ������ҩ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindCMedicineByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, string m_strRealMedStoreID, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr, a.itemengname_vchr, b.deptprep_int, a.usageid_chr, c.usagename_vchr,
                                a.dosageunit_chr,a." + strType + @" type, {0}, {1}, a.tradeprice_mny,round (a.tradeprice_mny / a.packqty_dec, 4) subtrademoney,b.noqtyflag_int,b.mindosage_dec,
                                a.itemopinvtype_chr,b.maxdosage_dec,b.adultdosage_dec,b.childdosage_dec,a.itemcommname_vchr, b.ispoison_chr, b.isanaesthesia_chr, b.ischlorpromazine_chr, b.ischlorpromazine2_chr,
                                b.nmldosage_dec,a.itemipunit_chr,a.opchargeflg_int,a.packqty_dec,a.dosage_dec,a.itemcode_vchr,f.precent_dec, y.typename_vchr as ybtypename  
                                from t_bse_chargeitem a ,t_bse_medicine b, t_bse_usagetype c, t_bse_chargecatmap d, (select itemid_chr, precent_dec from t_aid_inschargeitem where copayid_chr = ?) f, t_aid_medicaretype y 
                                where a.itemsrcid_vchr=b.medicineid_chr(+) and a.usageid_chr=c.usageid_chr(+) and (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 and a.itemopinvtype_chr=d.catid_chr(+) and a.insurancetype_vchr = y.typeid_chr(+) 
	                                and d.groupid_chr='0002' 
                                    and d.internalflag_int=0  
                                    and a.itemid_chr = f.itemid_chr(+)  
                            and exists (select 1
                                   from t_ds_storage t1, t_ds_storage_detail t2
                                  where t1.medicineid_chr = t2.medicineid_chr
                                    and t1.medicineid_chr=b.medicineid_chr
                                    and t1.drugstoreid_chr=t2.drugstoreid_chr
                                    and t1.drugstoreid_chr = ?
                                    and t1.noqtyflag_int = 0
                                    and t1.ifstop_int = 0
                                    and t2.canprovide_Int = 1
                                    and t2.iprealgross_Int > 0 ) order by a." + strType;

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end) as submoney");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4) as submoney");

            //�ԡ�/����ͷ�ǲ�ѯ������ҩ
            if (ID.IndexOf("/") > -1)
            {
                strSQL = @"select distinct a.itemid_chr,a.itemname_vchr,a.itemspec_vchr, a.itemengname_vchr, a.selfdefine_int, a.itemchecktype_chr, a.itemopunit_chr, a.itemipunit_chr, b.deptprep_int,
                                a.dosageunit_chr,a.packqty_dec, {0}, {1}, a.itemcode_vchr type,b.noqtyflag_int,b.mindosage_dec,b.maxdosage_dec,b.adultdosage_dec,b.childdosage_dec,
                                b.nmldosage_dec,b.hype_int,a.opchargeflg_int,a.usageid_chr, c.usagename_vchr, a.itemcommname_vchr, b.ispoison_chr, b.isanaesthesia_chr, b.ischlorpromazine_chr, b.ischlorpromazine2_chr,
                                a.itemopinvtype_chr,a.dosage_dec,a.itemcode_vchr ,f.precent_dec, y.typename_vchr as ybtypename 
                                from t_bse_chargeitem a ,t_bse_medicine b, t_bse_usagetype c,
                                (select seqid_chr,create_dat,deptid_chr,itemid_chr,createrid_chr,privilege_int,type_int
                                  from t_aid_comusechargeitem
                                 where createrid_chr = ? and type_int = 0
                                union
                                select a.seqid_chr,a.create_dat,a.deptid_chr,a.itemid_chr,a.createrid_chr,a.privilege_int,a.type_int 
                                  from t_aid_comusechargeitem a,
                                       (select a.deptid_chr
                                          from t_bse_deptemp a
                                         where a.end_dat is null and a.empid_chr = ?) b
                                 where a.deptid_chr = b.deptid_chr and type_int = 0) d, (select precent_dec,itemid_chr,copayid_chr from t_aid_inschargeitem where copayid_chr = ?) f, t_bse_chargecatmap h, t_aid_medicaretype y  
                                where a.itemsrcid_vchr=b.medicineid_chr(+) and a.ifstop_int =0 and a.usageid_chr=c.usageid_chr(+) and a.insurancetype_vchr = y.typeid_chr(+) 
                                and a.itemid_chr =d.itemid_chr and a.itemopinvtype_chr = h.catid_chr(+) and h.groupid_chr = '0002'  
                                and a.itemid_chr =f.itemid_chr(+)  and (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?)  
                                and exists (select 1
                                   from t_ds_storage t1, t_ds_storage_detail t2
                                  where t1.medicineid_chr = t2.medicineid_chr
                                    and t1.medicineid_chr=b.medicineid_chr
                                    and t1.drugstoreid_chr=t2.drugstoreid_chr
                                    and t1.drugstoreid_chr = ?
                                    and t1.noqtyflag_int = 0
                                    and t1.ifstop_int = 0
                                    and t2.canprovide_Int = 1
                                    and t2.iprealgross_Int > 0 ) order by a.itemcode_vchr";

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end) as submoney");
                else
                    strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4) as submoney");

                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = strEmployID;
                ParamArr[1].Value = strEmployID;
                ParamArr[2].Value = strPatientTypeID;
                ParamArr[3].Value = ID.Replace("/", "") + "%";
                ParamArr[4].Value = ID.Replace("/", "") + "%";
                ParamArr[5].Value = m_strRealMedStoreID;
            }
            else
            {
                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = ID + "%";
                ParamArr[2].Value = ID + "%";
                ParamArr[3].Value = m_strRealMedStoreID;
            }

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                ParamArr = null;
                strSQL = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindTestChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,
							a.itemcode_vchr,f.precent_dec ,a.itemcommname_vchr, 
							a.itemunit_chr itemopunit_chr, {0}, 
							a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr,
							 g.sample_type_id_chr, h.sample_type_desc_vchr, y.typename_vchr as ybtypename   
					   from t_bse_chargeitem a , t_bse_chargecatmap d, 
							(select * from t_aid_inschargeitem where copayid_chr = ?) f,
							t_aid_lis_apply_unit g,
							t_aid_lis_sampletype h, 
                            t_aid_medicaretype y 
					    where (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 
					      and a.itemopinvtype_chr=d.catid_chr(+) 
					      and a.itemsrcid_vchr = g.apply_unit_id_chr(+)
					      and g.sample_type_id_chr = h.sample_type_id_chr(+) 
                          and a.insurancetype_vchr = y.typeid_chr(+) 
					      and d.groupid_chr='0003' 
					      and d.internalflag_int=0   
					      and a.itemid_chr =f.itemid_chr(+)  order by a." + strType;

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny");

            //�ԡ�/����ͷ�ǲ�ѯ���ü�����Ŀ
            if (ID.IndexOf("/") > -1)
            {
                strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,
							a.itemcode_vchr,f.precent_dec ,a.itemcommname_vchr,
							a.itemunit_chr itemopunit_chr, {0},  
							a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr,
							 g.sample_type_id_chr, h.sample_type_desc_vchr, y.typename_vchr as ybtypename   
							from t_bse_chargeitem a , t_bse_chargecatmap d, 
							(select precent_dec,itemid_chr,copayid_chr from t_aid_inschargeitem where copayid_chr = ?) f,
							t_aid_lis_apply_unit g,
							t_aid_lis_sampletype h, 
                            t_aid_medicaretype y,  
							(select seqid_chr,create_dat,deptid_chr,itemid_chr,createrid_chr,privilege_int,type_int
							from t_aid_comusechargeitem
							where createrid_chr = ? and type_int = 0
							union
							select a.seqid_chr,a.create_dat,a.deptid_chr,a.itemid_chr,a.createrid_chr,a.privilege_int,a.type_int
							from t_aid_comusechargeitem a,
								(select a.deptid_chr
									from t_bse_deptemp a
									where a.end_dat is null and a.empid_chr = ?) b
							where a.deptid_chr = b.deptid_chr and type_int = 0) i
					where (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 
					and a.itemopinvtype_chr=d.catid_chr(+) 
					and a.itemsrcid_vchr = g.apply_unit_id_chr(+)
					and g.sample_type_id_chr = h.sample_type_id_chr(+) 
                    and a.insurancetype_vchr = y.typeid_chr(+) 
					and d.groupid_chr='0003' 
					and d.internalflag_int=0 
					and a.itemid_chr = i.itemid_chr 
					and a.itemid_chr =f.itemid_chr(+)  order by a." + strType;

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
                else
                    strSQL = string.Format(strSQL, "a.itemprice_mny");

                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = strEmployID;
                ParamArr[2].Value = strEmployID;
                ParamArr[3].Value = ID.Replace("/", "") + "%";
                ParamArr[4].Value = ID.Replace("/", "") + "%";
            }
            else
            {
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);

                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = ID + "%";
                ParamArr[2].Value = ID + "%";
            }

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                ParamArr = null;
                strSQL = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindExamineChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr,f.precent_dec, a.itemcommname_vchr, a.usageid_chr, 
                                    a.itemunit_chr itemopunit_chr, {0}, a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr , a.itemchecktype_chr, g.partname, y.typename_vchr as ybtypename 
                                     from t_bse_chargeitem a , t_bse_chargecatmap d,
                                     (select * from t_aid_inschargeitem where copayid_chr = ?) f,
                                      ar_apply_partlist g, t_aid_medicaretype y 
                                    where (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 
                                    and a.itemopinvtype_chr=d.catid_chr(+) 
                                    and a.insurancetype_vchr = y.typeid_chr(+) 
                                    and d.groupid_chr='0004'
                                     and d.internalflag_int=0  
                                      and a.itemchecktype_chr = g.partid(+)
                                    and a.itemid_chr =f.itemid_chr(+)  order by a." + strType;

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny");

            //�ԡ�/����ͷ�ǲ�ѯ���ü����Ŀ
            if (ID.IndexOf("/") > -1)
            {
                strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr,f.precent_dec, a.itemcommname_vchr, a.usageid_chr, 
a.itemunit_chr itemopunit_chr, {0}, a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr , a.itemchecktype_chr, g.partname, y.typename_vchr as ybtypename 
 from t_bse_chargeitem a , t_bse_chargecatmap d,
 (select precent_dec,itemid_chr,copayid_chr from t_aid_inschargeitem where copayid_chr = ?) f,
  ar_apply_partlist g, t_aid_medicaretype y, 
	(select seqid_chr,create_dat,deptid_chr,itemid_chr,createrid_chr,privilege_int,type_int
		from t_aid_comusechargeitem
		where createrid_chr = ? and type_int = 0
		union
		select a.seqid_chr,a.create_dat,a.deptid_chr,a.itemid_chr,a.createrid_chr,a.privilege_int,a.type_int
		from t_aid_comusechargeitem a,
			(select a.deptid_chr
				from t_bse_deptemp a
				where a.end_dat is null and a.empid_chr = ?) b
		where a.deptid_chr = b.deptid_chr and type_int = 0) h
where (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 
and a.itemopinvtype_chr=d.catid_chr(+) 
and a.insurancetype_vchr = y.typeid_chr(+) 
and d.groupid_chr='0004'
and d.internalflag_int=0 
and a.itemid_chr = h.itemid_chr 
and a.itemchecktype_chr = g.partid(+)  
and a.itemid_chr =f.itemid_chr(+) order by a." + strType;

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
                else
                    strSQL = string.Format(strSQL, "a.itemprice_mny");

                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = strEmployID;
                ParamArr[2].Value = strEmployID;
                ParamArr[3].Value = ID.Replace("/", "") + "%";
                ParamArr[4].Value = ID.Replace("/", "") + "%";
            }
            else
            {
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);

                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = ID + "%";
                ParamArr[2].Value = ID + "%";
            }

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                ParamArr = null;
                strSQL = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindOPSChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr, a.apply_type_int, f.precent_dec, a.itemcommname_vchr, y.typename_vchr as ybtypename, a.usageid_chr, 
a.itemunit_chr itemopunit_chr, {0}, a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr  from t_bse_chargeitem a , t_bse_chargecatmap d, (select * from t_aid_inschargeitem where copayid_chr = ?) f, t_aid_medicaretype y 
where (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 and a.itemopinvtype_chr=d.catid_chr(+) and d.groupid_chr='0006'  and d.internalflag_int=0 
and a.itemid_chr =f.itemid_chr(+) 
and a.insurancetype_vchr = y.typeid_chr(+)  
order by a." + strType;

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny");

            //�ԡ�/����ͷ�ǲ�ѯ����������Ŀ
            if (ID.IndexOf("/") > -1)
            {
                strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr, a.apply_type_int, f.precent_dec, a.itemcommname_vchr, y.typename_vchr as ybtypename, a.usageid_chr, 
a.itemunit_chr itemopunit_chr, {0}, a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr  from t_bse_chargeitem a , t_bse_chargecatmap d, (select * from t_aid_inschargeitem where copayid_chr = ?) f, t_aid_medicaretype y, 
(select seqid_chr,create_dat,deptid_chr,itemid_chr,createrid_chr,privilege_int,type_int 
		from t_aid_comusechargeitem
		where createrid_chr = ? and type_int = 0
		union
		select a.seqid_chr,a.create_dat,a.deptid_chr,a.itemid_chr,a.createrid_chr,a.privilege_int,a.type_int
		from t_aid_comusechargeitem a,
			(select a.deptid_chr
				from t_bse_deptemp a
				where a.end_dat is null and a.empid_chr = ?) b
		where a.deptid_chr = b.deptid_chr and type_int = 0) g 
where (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 and a.itemid_chr = g.itemid_chr and a.itemopinvtype_chr=d.catid_chr(+) 
and d.groupid_chr='0006' 
and d.internalflag_int=0 
and a.itemid_chr =f.itemid_chr(+) 
and a.insurancetype_vchr = y.typeid_chr(+)  
order by a." + strType;

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
                else
                    strSQL = string.Format(strSQL, "a.itemprice_mny");

                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = strEmployID;
                ParamArr[2].Value = strEmployID;
                ParamArr[3].Value = ID.Replace("/", "") + "%";
                ParamArr[4].Value = ID.Replace("/", "") + "%";
            }
            else
            {
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);

                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = ID + "%";
                ParamArr[2].Value = ID + "%";
            }

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                ParamArr = null;
                strSQL = null;
                ID = null;
                strPatientTypeID = null;
                strEmployID = null;
                objHRPSvc = null;
            }
            return lngRes;
        }

        /// <summary>
        /// ��ѯ�����շ���Ŀ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <param name="strPatientTypeID"></param>
        /// <param name="dt"></param>
        /// <param name="strEmployID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindOPSChargeByID(string strType, string ID, string strPatientTypeID, string strEmployID, out DataTable dt, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr,f.precent_dec, y.typename_vchr as ybtypename, a.usageid_chr, 
a.itemunit_chr itemopunit_chr, {0}, a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr  from t_bse_chargeitem a , t_bse_chargecatmap d, (select * from t_aid_inschargeitem where copayid_chr = ?) f, t_aid_medicaretype y 
where  upper(a." + strType + @") like ? and a.ifstop_int = 0 and a.itemopinvtype_chr=d.catid_chr(+) and d.groupid_chr='0006'  and d.internalflag_int=0 and a.itemid_chr =f.itemid_chr(+) and a.insurancetype_vchr = y.typeid_chr(+) 
            and (a.usageid_chr in (select usageid_chr from t_bse_usagetype where trim(usagename_vchr) = '����'))
            order by a." + strType;

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny");

            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
            ParamArr[0].Value = strPatientTypeID;
            ParamArr[1].Value = ID + "%";

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                ParamArr = null;
                strSQL = null;
            }
            return lngRes;
        }

        /// <summary>
        /// �����շ���ĿID��ѯ�����շ���Ŀ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <param name="strPatientTypeID"></param>
        /// <param name="dt"></param>
        /// <param name="strEmployID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindOPSChargeByID(string ID, string strPatientTypeID, out DataTable dt, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr,f.precent_dec, y.typename_vchr as ybtypename, a.usageid_chr, 
a.itemunit_chr itemopunit_chr, {0}, a.selfdefine_int,a.itemcode_vchr type, a.itemengname_vchr  from t_bse_chargeitem a , t_bse_chargecatmap d, (select * from t_aid_inschargeitem where copayid_chr = ?) f, t_aid_medicaretype y 
where a.itemid_chr = ? and a.ifstop_int =0 and a.itemopinvtype_chr=d.catid_chr(+) and d.groupid_chr='0006'  and d.internalflag_int=0 and a.itemid_chr = f.itemid_chr(+) and a.insurancetype_vchr = y.typeid_chr(+) order by a.itemcode_vchr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny");

            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
            ParamArr[0].Value = strPatientTypeID;
            ParamArr[1].Value = ID;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                strSQL = null;
                ParamArr = null;
                ID = null;
                strPatientTypeID = null;
                objHRPSvc = null;
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_mthFindOtherChargeByID(string strType, string ID, string strPatientTypeID, out DataTable dt, string strEmployID, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            if (strType != "itemid_chr")
            {
                m_mthGetType(ID, ref strType);
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr,f.precent_dec, a.itemcommname_vchr, b.deptprep_int, y.typename_vchr as ybtypename, 
         decode (a.opchargeflg_int,
                 0, itemopunit_chr,
                 1, itemipunit_chr
                ) as itemopunit_chr,
         decode (a.opchargeflg_int,
                 0, {0},
                 1, {1}
                ) as itemprice_mny,a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr from t_bse_chargeitem a, t_bse_medicine b, t_bse_chargecatmap d,  (select * from t_aid_inschargeitem where copayid_chr = ?) f, t_aid_medicaretype y 
         where a.itemsrcid_vchr = b.medicineid_chr(+) and (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 and a.itemopinvtype_chr=d.catid_chr(+) and d.groupid_chr='0005' and d.internalflag_int=0 and a.itemid_chr =f.itemid_chr(+) 
           and a.insurancetype_vchr = y.typeid_chr(+) order by a." + strType;

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end)", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end)");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4)");

            //�ԡ�/����ͷ�ǲ�ѯ���ü����Ŀ
            if (ID.IndexOf("/") > -1)
            {
                strSQL = @"select a.itemid_chr,a.itemname_vchr,a.itemspec_vchr,a.itemopinvtype_chr,a.itemcode_vchr,f.precent_dec, a.itemcommname_vchr, b.deptprep_int, y.typename_vchr as ybtypename, 
         decode (a.opchargeflg_int,
                 0, itemopunit_chr,
                 1, itemipunit_chr
                ) as itemopunit_chr,
         decode (a.opchargeflg_int,
                 0, {0},
                 1, {1}
                ) as itemprice_mny,a.selfdefine_int,a." + strType + @" type, a.itemengname_vchr from t_bse_chargeitem a, t_bse_medicine b, t_bse_chargecatmap d, (select * from t_aid_inschargeitem where copayid_chr = ?) f,
       (select seqid_chr,create_dat,deptid_chr,itemid_chr,createrid_chr,privilege_int,type_int
		from t_aid_comusechargeitem
		where createrid_chr = ? and type_int = 0
		union
		select a.seqid_chr,a.create_dat,a.deptid_chr,a.itemid_chr,a.createrid_chr,a.privilege_int,a.type_int
		from t_aid_comusechargeitem a,
			(select a.deptid_chr
				from t_bse_deptemp a
				where a.end_dat is null and a.empid_chr = ?) b
		where a.deptid_chr = b.deptid_chr and type_int = 0) g, t_aid_medicaretype y  
        where a.itemsrcid_vchr = b.medicineid_chr(+) and (upper(a." + strType + @") like ? or upper(a.itemopcode_chr) like ?) and a.ifstop_int =0 and a.itemid_chr = g.itemid_chr 
          and a.itemopinvtype_chr=d.catid_chr(+) and d.groupid_chr='0005' and d.internalflag_int=0 
          and a.itemid_chr =f.itemid_chr(+) and a.insurancetype_vchr = y.typeid_chr(+) order by a." + strType;

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end)", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end)");
                else
                    strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4)");

                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = strEmployID;
                ParamArr[2].Value = strEmployID;
                ParamArr[3].Value = ID.Replace("/", "") + "%";
                ParamArr[4].Value = ID.Replace("/", "") + "%";
            }
            else
            {
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = strPatientTypeID;
                ParamArr[1].Value = ID + "%";
                ParamArr[2].Value = ID + "%";
            }

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                strSQL = null;
                ParamArr = null;
                ID = null;
                strPatientTypeID = null;
                strEmployID = null;
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ����Ƶ��
        [AutoComplete]
        public long m_mthFindFrequency(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select freqid_chr,freqname_chr,usercode_chr,times_int,days_int,lexectime_vchr,texectime_vchr,execweekday_chr,printdesc_vchr,opfredesc_vchr from t_aid_recipefreq where upper(usercode_chr) like ? or upper(freqname_chr) like ? order by usercode_chr";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = ID.ToUpper() + "%";
                ParamArr[1].Value = ID.ToUpper() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                ParamArr = null;
                ID = null;
                strSQL = null;
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindFrequency2(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select freqid_chr,freqname_chr,usercode_chr,times_int,days_int,lexectime_vchr,texectime_vchr,execweekday_chr,printdesc_vchr,opfredesc_vchr from t_aid_recipefreq where freqid_chr = ?";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                ParamArr = null;
                ID = null;
                strSQL = null;
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region �����÷�
        [AutoComplete]
        public long m_mthFindUsage(string ID, int scope, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strScope = "";
            if (scope == 1)
            {
                strScope = " and (scope_int = 0 or scope_int = 1) ";
            }
            else if (scope == 2)
            {
                strScope = " and (scope_int = 0 or scope_int = 2) ";
            }
            else
            {
                strScope = " and (scope_int = 0 or scope_int = 1 or scope_int = 2) ";
            }

            string strSQL = @"select usageid_chr,usagename_vchr,usercode_chr,pycode_vchr,wbcode_vchr,scope_int,putmed_int,test_int,opusagedesc from t_bse_usagetype where (upper(usercode_chr) like ? or upper(usagename_vchr) like ? or upper(wbcode_vchr) like ? or upper(pycode_vchr) like ?)" + strScope + " order by scope_int, usercode_chr";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = ID.ToUpper() + "%";
                ParamArr[1].Value = ID.ToUpper() + "%";
                ParamArr[2].Value = ID.ToUpper() + "%";
                ParamArr[3].Value = ID.ToUpper() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);

                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                strSQL = null;
                ParamArr = null;
                ID = null;
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindUsage2(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select  usageid_chr,usagename_vchr,usercode_chr,pycode_vchr,wbcode_vchr,scope_int,putmed_int,test_int,opusagedesc  from t_bse_usagetype where usageid_chr = ?";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                strSQL = null;
                ParamArr = null;
                objHRPSvc = null;
                ID = null;
            }
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="OPR_VO">������VO</param>
        /// <param name="CH_VO">����VO</param>
        /// <param name="DR_VO">���VO</param>
        /// <param name="PWM_VO">1</param>
        /// <param name="CM_VO">2</param>
        /// <param name="CHK_VO">3</param>
        /// <param name="TR_VO">4</param>
        /// <param name="OP_VO">5</param>
        /// <param name="Other_VO">6</param>
        /// <param name="strRecipeID">����ID</param>
        /// <param name="strCaseHisID">����ID</param>
        /// <param name="IsModify">true ����,false�޸�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthSaveAllData(clsOutPatientRecipe_VO OPR_VO, clsOutpatientCaseHis_VO CH_VO, clsOutpatientDiagRec_VO DR_VO, clsOutpatientPWMRecipeDe_VO[] PWM_VO, clsOutpatientCMRecipeDe_VO[] CM_VO,
            clsOutpatientCHKRecipeDe_VO[] CHK_VO, clsOutpatientTestRecipeDe_VO[] TR_VO,
            clsOutpatientOPSRecipeDe_VO[] OP_VO, clsOutpatientOtherRecipeDe_VO[] Other_VO, clsOutpatientOrderRecipeDe_VO[] Order_VO,
            out string strRecipeID, out string strCaseHisID, bool IsModify, bool blnCashStatus, bool blnSaveCash)
        {
            long ret = 0;
            strRecipeID = "";
            strCaseHisID = "";

            // 2019-10-25 ������ϸΪ�գ������洦�� (�հ״���) --�� ����ֻд��������𣬻ָ�
            bool isSaveRecipe = true;
            //if ((PWM_VO == null || PWM_VO.Length == 0) &&
            //   (CM_VO == null || CM_VO.Length == 0) &&
            //   (CHK_VO == null || CHK_VO.Length == 0) &&
            //   (TR_VO == null || TR_VO.Length == 0) &&
            //   (OP_VO == null || OP_VO.Length == 0) &&
            //   (Other_VO == null || Other_VO.Length == 0) &&
            //   (Order_VO == null || Order_VO.Length == 0))
            //{
            //    isSaveRecipe = false;
            //}

            if (isSaveRecipe)
            {
                ret = this.m_mthSaveRecipeMain(OPR_VO, out strRecipeID, blnSaveCash);

                if (ret > 0)
                {
                    if (blnSaveCash)
                    {
                        ret = m_mthSaveCaseHistory(CH_VO, blnCashStatus, ref strRecipeID);
                        if (ret > 0)
                        {
                            strCaseHisID = CH_VO.m_strCaseHisID;
                        }
                    }
                    //	ret =m_mthSaveDiagnoses(p_objPrincipal,DR_VO,IsModify,strRecipeID);
                    if (!IsModify)
                    {
                        ret = this.m_mthDeleteRecipeDetail(strRecipeID);
                    }
                    ret = this.m_mthSaveDetail1(PWM_VO, strRecipeID);
                    ret = this.m_mthSaveDetail2(CM_VO, strRecipeID);
                    ret = this.m_mthSaveDetail3(CHK_VO, strRecipeID);
                    ret = this.m_mthSaveDetail4(TR_VO, strRecipeID);
                    ret = this.m_mthSaveDetail5(OP_VO, strRecipeID);
                    ret = this.m_mthSaveDetail6(Other_VO, strRecipeID);
                    ret = this.m_mthSaveDetailOrder(Order_VO, strRecipeID);
                }
            }
            else
            {
                if (blnSaveCash)
                {
                    ret = m_mthSaveCaseHistory(CH_VO, blnCashStatus, ref strRecipeID);
                    if (ret > 0)
                    {
                        strCaseHisID = CH_VO.m_strCaseHisID;
                    }
                }
            }
            return ret;

        }
        /// <summary>
        /// ���洦������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="clsVO"></param>
        /// <param name="p_strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthSaveRecipeMain(clsOutPatientRecipe_VO clsVO, out string p_strID, bool blnSaveCash)
        {
            p_strID = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            if (blnSaveCash)
            {
                if (clsVO.m_strCaseHistoryID.Trim() == "")
                {
                    if (clsVO.m_strOutpatRecipeID != "" && clsVO.m_strOutpatRecipeID != null)
                    {
                        clsVO.m_strCaseHistoryID = clsVO.m_strOutpatRecipeID;
                    }
                    else
                    {
                        clsVO.m_strCaseHistoryID = p_strID;
                    }
                }
            }
            else
            {
                clsVO.m_strCaseHistoryID = "";
            }

            long lngRes = 0, lngAffects = 0;
            clsHRPTableService objHRPSvc = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                string strSQL = "";
                if (clsVO.m_strRecipeType.Trim() == "" || clsVO.m_strRecipeType == null)
                {
                    clsVO.m_strRecipeType = "0";
                }

                if (clsVO.m_strOutpatRecipeID == null || clsVO.m_strOutpatRecipeID.Trim() == "")
                {
                    IDataParameter[] ParamArr = null;

                    if (string.IsNullOrEmpty(clsVO.ipAddr))
                    { 
                        DataTable tempdt = null;
                        strSQL = @"select t.macname_vchr, t.mac_vchr
                                  from t_sys_log t
                                 where t.empid_chr = ?
                                   and (t.logtime_dat between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 order by t.logtime_dat desc";

                        objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                        ParamArr[0].Value = clsVO.m_strDoctorID;
                        ParamArr[1].Value = DateTime.Now.AddDays(-100).ToString("yyyy-MM-dd") + " 00:00:00";
                        ParamArr[2].Value = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tempdt, ParamArr);
                        if (tempdt != null && tempdt.Rows.Count > 0)//�ж�����Ѿ��շѴ����ͷ���
                        {
                            clsVO.ipAddr = tempdt.Rows[0]["mac_vchr"].ToString();
                        }
                    }

                    clsVO.m_strOutpatRecipeID = p_strID;
                    clsVO.m_intType = 0;
                    strSQL = @"insert into t_opr_outpatientrecipe(outpatrecipeid_chr,patientid_chr,createdate_dat,registerid_chr,diagdr_chr,diagdept_chr,recordemp_chr,recorddate_dat,
																pstauts_int,paytypeid_chr,casehisid_chr,recipeflag_int,type_int, createtype_int, seculevel, isproxyboilmed, macAddr) 
                                                        values(?, ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, ?, ?, ?, ?, ?)";



                    objHRPSvc.CreateDatabaseParameter(17, out ParamArr);
                    ParamArr[0].Value = clsVO.m_strOutpatRecipeID;
                    ParamArr[1].Value = clsVO.m_strPatientID;
                    ParamArr[2].Value = clsVO.m_strCreateDate;
                    ParamArr[3].Value = clsVO.m_strRegisterID;
                    ParamArr[4].Value = clsVO.m_strDoctorID;
                    ParamArr[5].Value = clsVO.m_strDepID;
                    ParamArr[6].Value = clsVO.m_strOperatorID;
                    ParamArr[7].Value = clsVO.m_strRecordDate;
                    ParamArr[8].Value = clsVO.m_intPStatus;
                    ParamArr[9].Value = clsVO.m_strPatientType;
                    ParamArr[10].Value = clsVO.m_strCaseHistoryID;
                    ParamArr[11].Value = clsVO.m_intType;
                    ParamArr[12].Value = clsVO.m_strRecipeType;
                    ParamArr[13].Value = clsVO.intCreatetype;
                    ParamArr[14].Value = clsVO.SecuLevel;
                    ParamArr[15].Value = clsVO.IsProxyBoilMed;
                    ParamArr[16].Value = clsVO.ipAddr;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                }
                else
                {
                    p_strID = clsVO.m_strOutpatRecipeID;
                    strSQL = @"update t_opr_outpatientrecipe
								set paytypeid_chr = ?,									
									type_int = ?,
									casehisid_chr = ?,
                                    seculevel = ?  
							  where outpatrecipeid_chr = ?";

                    IDataParameter[] ParamArr = null;

                    objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                    ParamArr[0].Value = clsVO.m_strPatientType;
                    ParamArr[1].Value = clsVO.m_strRecipeType;
                    ParamArr[2].Value = clsVO.m_strCaseHistoryID;
                    ParamArr[3].Value = clsVO.SecuLevel;
                    ParamArr[4].Value = clsVO.m_strOutpatRecipeID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
                lngRes = -2;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="clsVO"></param>
        /// <param name="IsNew"></param>
        [AutoComplete]
        public long m_mthSaveDiagnoses(clsOutpatientDiagRec_VO clsVO, bool IsNew, string strRecipe)
        {
            long lngRes = 0, lngAffects = 0;
            string strSQL = "";
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                if (IsNew)
                {
                    strSQL = @"insert into t_opr_outpatientdiagrec(outpatientdiagrecid_chr,patientid_chr,registerid_chr,diagdr_chr,diagdept_chr,recordemp_chr,recorddate_dat,diagimport_vchr,
                                diagmemo_vchr,diagstd_vchr,curprinciple_vchr,curestd_vchr,defend_vchr) values(?, ?, ?, ?, ?, ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, ?, ?)";

                    IDataParameter[] ParamArr = null;

                    objHRPSvc.CreateDatabaseParameter(13, out ParamArr);
                    ParamArr[0].Value = strRecipe;
                    ParamArr[1].Value = clsVO.m_strPatientID;
                    ParamArr[2].Value = clsVO.m_strRegisterID;
                    ParamArr[3].Value = clsVO.m_strDiagDrID;
                    ParamArr[4].Value = clsVO.m_strDiagDeptID;
                    ParamArr[5].Value = clsVO.m_strRecordEmpID;
                    ParamArr[6].Value = clsVO.m_strRecordDate;
                    ParamArr[7].Value = clsVO.m_strDiagImport;
                    ParamArr[8].Value = clsVO.m_strDiagMemo;
                    ParamArr[9].Value = clsVO.m_strDiagStd;
                    ParamArr[10].Value = clsVO.m_strCurePrinciple;
                    ParamArr[11].Value = clsVO.m_strCureStd;
                    ParamArr[12].Value = clsVO.m_strDefend;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                    ParamArr = null;
                    strSQL = null;
                }
                else
                {
                    strSQL = @"update t_opr_outpatientdiagrec set patientid_chr=?, registerid_chr=?, diagdr_chr =?, diagdept_chr=?, recordemp_chr=?, 
                                    recorddate_dat=to_date(?, 'yyyy-mm-dd hh24:mi:ss'), diagimport_vchr=?, diagmemo_vchr=?, diagstd_vchr=?, curprinciple_vchr=?, curestd_vchr=?, defend_vchr=? 
                              where outpatientdiagrecid_chr=?";

                    IDataParameter[] ParamArr = null;

                    objHRPSvc.CreateDatabaseParameter(13, out ParamArr);
                    ParamArr[0].Value = clsVO.m_strPatientID;
                    ParamArr[1].Value = clsVO.m_strRegisterID;
                    ParamArr[2].Value = clsVO.m_strDiagDrID;
                    ParamArr[3].Value = clsVO.m_strDiagDeptID;
                    ParamArr[4].Value = clsVO.m_strRecordEmpID;
                    ParamArr[5].Value = clsVO.m_strRecordDate;
                    ParamArr[6].Value = clsVO.m_strDiagImport;
                    ParamArr[7].Value = clsVO.m_strDiagMemo;
                    ParamArr[8].Value = clsVO.m_strDiagStd;
                    ParamArr[9].Value = clsVO.m_strCurePrinciple;
                    ParamArr[10].Value = clsVO.m_strCureStd;
                    ParamArr[11].Value = clsVO.m_strDefend;
                    ParamArr[12].Value = strRecipe;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                    ParamArr = null;
                    strSQL = null;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="clsVO"></param>
        /// <param name="IsNew"></param>
        [AutoComplete]
        public long m_mthSaveCaseHistory(clsOutpatientCaseHis_VO clsVO, bool IsNew, ref string strCashID)
        {
            long lngRes = 0, lngAffects = 0;
            if (clsVO.m_strCaseHisID.Trim() == "" || clsVO.m_strCaseHisID == null)
            {
                if (strCashID != "" && strCashID != null)
                {
                    clsVO.m_strCaseHisID = strCashID;
                }
                else
                {
                    clsVO.m_strCaseHisID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    strCashID = clsVO.m_strCaseHisID;
                }
            }

            if (lngRes < 0)
            {
                return -1;
            }

            string strSQL = "";
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (IsNew)
                {
                    strSQL = @"insert into t_opr_outpatientcasehis(casehisid_chr,modifydate_dat,patientid_chr,registerid_chr,diagdr_chr,diagdept_chr,recordemp_chr,recorddate_dat, status_int, diagmain_vchr,diagmain_xml_vchr,diagcurr_vchr,diagcurr_xml_vhcr,diaghis_vchr,diaghis_xml_vchr,aidcheck_vchr,
                                aidcheck_xml_vchr,diag_vchr,diag_xml_vchr,treatment_vchr,treatment_xml_vchr,remark_vchr,remark_xml_vchr,anaphylaxis_vchr,bodycheck_vchr,bodychrck_xml_vchr,prihis_vchr,prihis_xml_vchr,parcasehisid_chr,caldept_vchr,caldept_xml_vchr, iszzsq) values(
                                        ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(32, out ParamArr);
                    ParamArr[0].Value = clsVO.m_strCaseHisID;
                    ParamArr[1].Value = clsVO.m_strModifyDate;
                    ParamArr[2].Value = clsVO.m_strPatientID;
                    ParamArr[3].Value = clsVO.m_strRegisterID;
                    ParamArr[4].Value = clsVO.m_strDiagDrID;
                    ParamArr[5].Value = clsVO.m_strDiagDeptID;
                    ParamArr[6].Value = clsVO.m_strRecordEmpID;
                    ParamArr[7].Value = clsVO.m_strRecordDate;
                    ParamArr[8].Value = clsVO.m_intStatus;
                    ParamArr[9].Value = clsVO.strDiagMain;
                    ParamArr[10].Value = clsVO.strDiagMain_XML;
                    ParamArr[11].Value = clsVO.strDiagCurr;
                    ParamArr[12].Value = clsVO.strDiagCurr_XML;
                    ParamArr[13].Value = clsVO.strDiagHis;
                    ParamArr[14].Value = clsVO.strDiagHis_XML;
                    ParamArr[15].Value = clsVO.strAidCheck;
                    ParamArr[16].Value = clsVO.strAidCheck_XML;
                    ParamArr[17].Value = clsVO.strDiag;
                    ParamArr[18].Value = clsVO.strDiag_XML;
                    ParamArr[19].Value = clsVO.strTreatMent;
                    ParamArr[20].Value = clsVO.strTreatMent_XML;
                    ParamArr[21].Value = clsVO.strReMark;
                    ParamArr[22].Value = clsVO.strReMark_XML;
                    ParamArr[23].Value = clsVO.strAnaPhyLaXis;
                    ParamArr[24].Value = clsVO.strExamineResult;
                    ParamArr[25].Value = clsVO.strExamineResult_XML;
                    ParamArr[26].Value = clsVO.m_strPRIHIS_VCHR;
                    ParamArr[27].Value = clsVO.m_strPRIHIS_XML_VCHR;
                    ParamArr[28].Value = clsVO.strParentCaseHistoryID;
                    ParamArr[29].Value = clsVO.strCALDEPT_VCHR;
                    ParamArr[30].Value = clsVO.strCALDEPT_VCHR_XML;
                    ParamArr[31].Value = clsVO.IsZzsq;
                }
                else
                {
                    strSQL = @"update t_opr_outpatientcasehis set modifydate_dat=to_date(?, 'yyyy-mm-dd hh24:mi:ss'), patientid_chr=?, registerid_chr = ?, diagdr_chr=?, diagdept_chr=?, 
                                                                recordemp_chr=?, recorddate_dat=to_date(?, 'yyyy-mm-dd hh24:mi:ss'), diagmain_vchr=?, diagmain_xml_vchr=?, diagcurr_vchr=?, 
                                                                diagcurr_xml_vhcr=?, diaghis_vchr=?, diaghis_xml_vchr=?, aidcheck_vchr=?, aidcheck_xml_vchr=?, 
                                                                diag_vchr=?, parcasehisid_chr=?, diag_xml_vchr=?, treatment_vchr=?, prihis_vchr=?, prihis_xml_vchr=?,
                                                                treatment_xml_vchr=?, remark_vchr=?, remark_xml_vchr=?, bodycheck_vchr=?, bodychrck_xml_vchr=?, caldept_vchr=?, 
                                                                caldept_xml_vchr=?, anaphylaxis_vchr=?, status_int = ?, iszzsq = ? where status_int = 1 and casehisid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(32, out ParamArr);
                    ParamArr[0].Value = clsVO.m_strModifyDate;
                    ParamArr[1].Value = clsVO.m_strPatientID;
                    ParamArr[2].Value = clsVO.m_strRegisterID;
                    ParamArr[3].Value = clsVO.m_strDiagDrID;
                    ParamArr[4].Value = clsVO.m_strDiagDeptID;
                    ParamArr[5].Value = clsVO.m_strRecordEmpID;
                    ParamArr[6].Value = clsVO.m_strRecordDate;
                    ParamArr[7].Value = clsVO.strDiagMain;
                    ParamArr[8].Value = clsVO.strDiagMain_XML;
                    ParamArr[9].Value = clsVO.strDiagCurr;
                    ParamArr[10].Value = clsVO.strDiagCurr_XML;
                    ParamArr[11].Value = clsVO.strDiagHis;
                    ParamArr[12].Value = clsVO.strDiagHis_XML;
                    ParamArr[13].Value = clsVO.strAidCheck;
                    ParamArr[14].Value = clsVO.strAidCheck_XML;
                    ParamArr[15].Value = clsVO.strDiag;
                    ParamArr[16].Value = clsVO.strParentCaseHistoryID;
                    ParamArr[17].Value = clsVO.strDiag_XML;
                    ParamArr[18].Value = clsVO.strTreatMent;
                    ParamArr[19].Value = clsVO.m_strPRIHIS_VCHR;
                    ParamArr[20].Value = clsVO.m_strPRIHIS_XML_VCHR;
                    ParamArr[21].Value = clsVO.strTreatMent_XML;
                    ParamArr[22].Value = clsVO.strReMark;
                    ParamArr[23].Value = clsVO.strReMark_XML;
                    ParamArr[24].Value = clsVO.strExamineResult;
                    ParamArr[25].Value = clsVO.strExamineResult_XML;
                    ParamArr[26].Value = clsVO.strCALDEPT_VCHR;
                    ParamArr[27].Value = clsVO.strCALDEPT_VCHR_XML;
                    ParamArr[28].Value = clsVO.strAnaPhyLaXis;
                    ParamArr[29].Value = clsVO.m_intStatus;
                    ParamArr[30].Value = clsVO.IsZzsq;
                    ParamArr[31].Value = clsVO.m_strCaseHisID;
                }
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                if (lngRes > 0)
                {
                    //�Ƿ������ Ϊ������Ϊ�޹��� 0 �ޣ�1��
                    int isallergic = 0;
                    if (clsVO.strAnaPhyLaXis.Trim() != "")
                    {
                        isallergic = 1;
                    }

                    strSQL = @"update t_bse_patient 
								set allergicdesc_vchr = ?,
									ifallergic_int = ? 
								where patientid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = clsVO.strAnaPhyLaXis;
                    ParamArr[1].Value = isallergic;
                    ParamArr[2].Value = clsVO.m_strPatientID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);

                    strSQL = @" delete from  t_opr_opch_icd10 where casehisid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = clsVO.m_strCaseHisID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                    if (lngRes > 0)
                    {
                        if (clsVO.objIllnessArr != null)
                        {
                            for (int i = 0; i < clsVO.objIllnessArr.Count; i++)
                            {
                                clsICD10_VO obj = clsVO.objIllnessArr[i] as clsICD10_VO;
                                strSQL = @" insert into t_opr_opch_icd10(casehisid_chr,icdcode_vchr,icdname_vchr) values(?,?,?)";

                                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                                ParamArr[0].Value = clsVO.m_strCaseHisID;
                                ParamArr[1].Value = obj.strICDCODE_VCHR;
                                ParamArr[2].Value = obj.strICDNAME_VCHR;

                                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                            }
                        }
                    }
                }
                ParamArr = null;
                strSQL = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #region ���洦����ϸ
        [AutoComplete]
        public long m_mthSaveDetail1(clsOutpatientPWMRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1;
            clsHRPTableService objHRPSvc = null;
            string strSQL = @"insert into t_tmp_outpatientpwmrecipede
                                          (outpatrecipedeid_chr,
                                           rowno_chr,
                                           rowno_vchr2,
                                           itemid_chr,
                                           unitid_chr,
                                           usageid_chr,
                                           tolqty_dec,
                                           unitprice_mny,
                                           tolprice_mny,
                                           days_int,
                                           qty_dec,
                                           discount_dec,
                                           outpatrecipeid_chr,
                                           freqid_chr,
                                           hypetest_int,
                                           desc_vchr,
                                           usageparentid_vchr,
                                           attachparentid_vchr,
                                           itemspec_vchr,
                                           dosage_dec,
                                           dosageunit_chr,
                                           attachitembasenum_dec,
                                           itemname_vchr,
                                           deptmed_int,
                                           toldiffprice_mny)
                                        values
                                          (seq_recipeid.nextval,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?)";

            int intRowsCount = clsVO.Length;
            if (intRowsCount > 0)
            {
                object[][] objParams = new object[24][];
                for (int i1 = 0; i1 < 24; i1++)
                {
                    objParams[i1] = new object[intRowsCount];
                }
                for (int i1 = 0; i1 < intRowsCount; i1++)
                {
                    objParams[0][i1] = clsVO[i1].m_strRowNo;
                    objParams[1][i1] = clsVO[i1].m_strRowNo2;
                    objParams[2][i1] = clsVO[i1].m_strItemID;
                    objParams[3][i1] = clsVO[i1].m_strUnit;
                    objParams[4][i1] = clsVO[i1].m_strUsageID.Trim();
                    objParams[5][i1] = clsVO[i1].m_decTolQty;
                    objParams[6][i1] = clsVO[i1].m_decPrice;
                    objParams[7][i1] = clsVO[i1].m_decTolPrice;
                    objParams[8][i1] = clsVO[i1].m_decDays;
                    objParams[9][i1] = clsVO[i1].m_decQty;
                    objParams[10][i1] = clsVO[i1].m_decDiscount;
                    objParams[11][i1] = strRecipe;
                    objParams[12][i1] = clsVO[i1].m_strFrequency;
                    objParams[13][i1] = clsVO[i1].m_strHYPETEST_INT;
                    objParams[14][i1] = clsVO[i1].m_strDESC_VCHR;
                    objParams[15][i1] = clsVO[i1].m_strUSAGEPARENTID_VCHR;
                    objParams[16][i1] = clsVO[i1].m_strATTACHPARENTID_VCHR;
                    objParams[17][i1] = clsVO[i1].m_strItemspec;
                    objParams[18][i1] = clsVO[i1].m_decDosage;
                    objParams[19][i1] = clsVO[i1].m_strDosageunit;
                    objParams[20][i1] = clsVO[i1].m_decAttachitembasenum;
                    objParams[21][i1] = clsVO[i1].m_strItemname;
                    objParams[22][i1] = clsVO[i1].m_intDeptmed;
                    objParams[23][i1] = clsVO[i1].m_decTolDiffPrice;// ���������
                }

                DbType[] objTypes = new DbType[24]{
                        DbType.String,  DbType.String, DbType.String, DbType.String, DbType.String,
                        DbType.Decimal, DbType.Decimal,DbType.Decimal,DbType.Decimal,  DbType.Decimal,
                        DbType.Decimal, DbType.String, DbType.String, DbType.String,  DbType.String,
                        DbType.String,  DbType.String, DbType.String, DbType.Decimal,DbType.String,
                        DbType.Decimal, DbType.String, DbType.Int32, DbType.Decimal
                    };

                try
                {
                    objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objParams, objTypes);
                    objParams = null;
                    objTypes = null;
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    objLogger = null;
                }
                finally
                {
                    clsVO = null;
                    objHRPSvc = null;
                    strSQL = null;
                }
            }

            return lngRes;
        }
        [AutoComplete]
        public long m_mthSaveDetail2(clsOutpatientCMRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1;

            clsHRPTableService objHRPSvc = null;
            string strSQL = @"insert into t_tmp_outpatientcmrecipede
                                              (outpatrecipedeid_chr,
                                               outpatrecipeid_chr,
                                               rowno_chr,
                                               rowno_vchr2,
                                               itemid_chr,
                                               unitid_chr,
                                               usageid_chr,
                                               qty_dec,
                                               unitprice_mny,
                                               tolprice_mny,
                                               discount_dec,
                                               times_int,
                                               min_qty_dec,
                                               sumusage_vchr,
                                               itemname_vchr,
                                               itemspec_vchr,
                                               deptmed_int,
                                               usagedetail_vchr,
                                               toldiffprice_mny
                                                )
                                            values
                                              (seq_recipeid.nextval, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)";

            int intRowsCount = clsVO.Length;
            if (intRowsCount > 0)
            {
                object[][] objParams = new object[18][];
                for (int i1 = 0; i1 < 18; i1++)
                {
                    objParams[i1] = new object[intRowsCount];
                }
                for (int i1 = 0; i1 < intRowsCount; i1++)
                {
                    objParams[0][i1] = strRecipe;
                    objParams[1][i1] = clsVO[i1].m_strRowNo.Trim();
                    objParams[2][i1] = clsVO[i1].m_strRowNo2.Trim();
                    objParams[3][i1] = clsVO[i1].m_strItemID.Trim();
                    objParams[4][i1] = clsVO[i1].m_strUnit.Trim();
                    objParams[5][i1] = clsVO[i1].m_strUsageID.Trim();
                    objParams[6][i1] = clsVO[i1].m_decQty;
                    objParams[7][i1] = clsVO[i1].m_decPrice;
                    objParams[8][i1] = clsVO[i1].m_decTolPrice;
                    objParams[9][i1] = clsVO[i1].m_decDiscount;
                    objParams[10][i1] = clsVO[i1].m_intTimes;
                    objParams[11][i1] = clsVO[i1].m_decMIN_QTY_DEC;
                    objParams[12][i1] = clsVO[i1].m_strCMedicineUsage;
                    objParams[13][i1] = clsVO[i1].m_strItemname;
                    objParams[14][i1] = clsVO[i1].m_strItemspec;
                    objParams[15][i1] = clsVO[i1].m_intDeptmed;
                    objParams[16][i1] = clsVO[i1].m_strUsageDetail;
                    objParams[17][i1] = clsVO[i1].m_decTolDiffPrice;// ���������
                }
                DbType[] objTypes = new DbType[18] {
                        DbType.String, DbType.String, DbType.String, DbType.String, DbType.String,
                        DbType.String, DbType.Decimal,DbType.Decimal,DbType.Decimal,DbType.Decimal,
                        DbType.Int32,  DbType.Decimal,DbType.String, DbType.String, DbType.String,
                        DbType.Int32,  DbType.String, DbType.Decimal
                    };

                try
                {
                    objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objParams, objTypes);
                    objParams = null;
                    objTypes = null;
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    objLogger = null;
                }
                finally
                {
                    objHRPSvc = null;
                    strSQL = null;
                    clsVO = null;
                }
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthSaveDetail3(clsOutpatientCHKRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1;
            clsHRPTableService objHRPSvc = null;
            string strSQL = @"insert into t_tmp_outpatientchkrecipede
                                              (outpatrecipedeid_chr,
                                               outpatrecipeid_chr,
                                               rowno_chr,
                                               itemid_chr,
                                               price_mny,
                                               oprdept_chr,
                                               discount_dec,
                                               tolprice_mny,
                                               qty_dec,
                                               attachid_vchr,
                                               sampleid_vchr,
                                               usageparentid_vchr,
                                               attachparentid_vchr,
                                               quickflag_int,
                                               attachitembasenum_dec,
                                               usageitembasenum_dec,
                                               itemname_vchr,
                                               itemspec_vchr,
                                               itemunit_vchr,
                                               itemusagedetail_vchr,
                                               deptmed_int,
                                               orderid_vchr,
                                               orderbasenum_dec)
                                            values
                                              (seq_recipeid.nextval,
                                               ?, ?, ?, ?, ?,
                                               ?, ?, ?, ?, ?,
                                               ?, ?, ?, ?, ?,
                                               ?, ?, ?, ?, ?,
                                               ?, ?)";

            int intRowsCount = clsVO.Length;
            if (intRowsCount > 0)
            {
                object[][] objParams = new object[22][];
                for (int i1 = 0; i1 < 22; i1++)
                {
                    objParams[i1] = new object[intRowsCount];
                }
                for (int i1 = 0; i1 < intRowsCount; i1++)
                {
                    objParams[0][i1] = strRecipe;
                    objParams[1][i1] = clsVO[i1].m_strRowNo;
                    objParams[2][i1] = clsVO[i1].m_strItemID;
                    objParams[3][i1] = clsVO[i1].m_decPrice;
                    objParams[4][i1] = clsVO[i1].m_strOprDeptID;
                    objParams[5][i1] = clsVO[i1].m_decDiscount;
                    objParams[6][i1] = clsVO[i1].m_decTolPrice;
                    objParams[7][i1] = clsVO[i1].m_decQty;
                    objParams[8][i1] = clsVO[i1].strApplyID;
                    objParams[9][i1] = clsVO[i1].strSampletypeID;
                    objParams[10][i1] = clsVO[i1].m_strUSAGEPARENTID_VCHR;
                    objParams[11][i1] = clsVO[i1].m_strATTACHPARENTID_VCHR;
                    objParams[12][i1] = clsVO[i1].m_strQuickflag_CHR;
                    objParams[13][i1] = clsVO[i1].m_decAttachitembasenum;
                    objParams[14][i1] = clsVO[i1].m_decUsageitembasenum;
                    objParams[15][i1] = clsVO[i1].m_strItemname;
                    objParams[16][i1] = clsVO[i1].m_strItemspec;
                    objParams[17][i1] = clsVO[i1].m_strItemunit;
                    objParams[18][i1] = clsVO[i1].m_strUsagedetail;
                    objParams[19][i1] = clsVO[i1].m_intDeptmed;
                    objParams[20][i1] = clsVO[i1].m_strOrderID;
                    objParams[21][i1] = clsVO[i1].m_decOrderBaseNum;
                }

                DbType[] objTypes = new DbType[22] {
                        DbType.String,  DbType.String, DbType.String, DbType.Decimal, DbType.String,
                        DbType.Decimal, DbType.Decimal,DbType.Decimal,DbType.String,  DbType.String,
                        DbType.String,  DbType.String, DbType.String,  DbType.Decimal, DbType.Decimal,
                        DbType.String,  DbType.String, DbType.String, DbType.String,  DbType.Int32,
                        DbType.String, DbType.Decimal
                    };

                try
                {
                    objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objParams, objTypes);
                    objParams = null;
                    objTypes = null;
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    objLogger = null;
                }
                finally
                {
                    clsVO = null;
                    objHRPSvc = null;
                    strSQL = null;
                }
            }

            return lngRes;
        }
        [AutoComplete]
        public long m_mthSaveDetail4(clsOutpatientTestRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1;

            clsHRPTableService objHRPSvc = null;

            string strSQL = @"insert into t_tmp_outpatienttestrecipede
                                          (outpatrecipedeid_chr,
                                           outpatrecipeid_chr,
                                           rowno_chr,
                                           itemid_chr,
                                           price_mny,
                                           oprdept_chr,
                                           discount_dec,
                                           tolprice_mny,
                                           qty_dec,
                                           attachid_vchr,
                                           checkpartid_vchr,
                                           usageparentid_vchr,
                                           attachparentid_vchr,
                                           attachitembasenum_dec,
                                           usageitembasenum_dec,
                                           itemname_vchr,
                                           itemspec_vchr,
                                           itemunit_vchr,
                                           itemusagedetail_vchr,
                                           deptmed_int,
                                           usageid_chr,
                                           orderid_vchr,
                                           orderbasenum_dec)
                                        values
                                          (seq_recipeid.nextval,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?, ?,
                                           ?, ?, ?, ?, ?,
                                           ?, ?)";
            int intRowsCount = clsVO.Length;
            if (intRowsCount > 0)
            {
                object[][] objParams = new object[22][];
                for (int i1 = 0; i1 < 22; i1++)
                {
                    objParams[i1] = new object[intRowsCount];
                }
                for (int i = 0; i < intRowsCount; i++)
                {
                    objParams[0][i] = strRecipe;
                    objParams[1][i] = clsVO[i].m_strRowNo;
                    objParams[2][i] = clsVO[i].m_strItemID;
                    objParams[3][i] = clsVO[i].m_decPrice;
                    objParams[4][i] = clsVO[i].m_strOprDeptID;
                    objParams[5][i] = clsVO[i].m_decDiscount;
                    objParams[6][i] = clsVO[i].m_decTolPrice;
                    objParams[7][i] = clsVO[i].m_decQty;
                    objParams[8][i] = clsVO[i].strApplyID;
                    objParams[9][i] = clsVO[i].strPartID;
                    objParams[10][i] = clsVO[i].m_strUSAGEPARENTID_VCHR;
                    objParams[11][i] = clsVO[i].m_strATTACHPARENTID_VCHR;
                    objParams[12][i] = clsVO[i].m_decAttachitembasenum;
                    objParams[13][i] = clsVO[i].m_decUsageitembasenum;
                    objParams[14][i] = clsVO[i].m_strItemname;
                    objParams[15][i] = clsVO[i].m_strItemspec;
                    objParams[16][i] = clsVO[i].m_strItemunit;
                    objParams[17][i] = clsVO[i].m_strUsagedetail;
                    objParams[18][i] = clsVO[i].m_intDeptmed;
                    objParams[19][i] = clsVO[i].m_strUsageID;
                    objParams[20][i] = clsVO[i].m_strOrderID;
                    objParams[21][i] = clsVO[i].m_decOrderBaseNum;
                }
                DbType[] objTypes = new DbType[22] {
                        DbType.String,  DbType.String, DbType.String, DbType.Decimal, DbType.String,
                        DbType.Decimal, DbType.Decimal,DbType.Decimal,DbType.String,  DbType.String,
                        DbType.String,  DbType.String, DbType.Decimal,DbType.Decimal, DbType.String,
                        DbType.String,  DbType.String, DbType.String, DbType.Int32,   DbType.String,
                        DbType.String,  DbType.Decimal
                    };
                try
                {
                    objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objParams, objTypes);
                    objParams = null;
                    objTypes = null;
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    objLogger = null;
                }
                finally
                {
                    objHRPSvc = null;
                    strSQL = null;
                    clsVO = null;
                }
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthSaveDetail5(clsOutpatientOPSRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1;

            clsHRPTableService objHRPSvc = null;

            string strSQL = @"insert into t_tmp_outpatientopsrecipede(outpatrecipedeid_chr,outpatrecipeid_chr,rowno_chr,itemid_chr,price_mny,oprdept_chr,discount_dec,tolprice_mny,qty_dec,attachid_vchr,usageparentid_vchr,
                                                                       attachparentid_vchr, attachitembasenum_dec, usageitembasenum_dec, itemname_vchr, itemspec_vchr, itemunit_vchr, itemusagedetail_vchr, deptmed_int, usageid_chr, orderid_vchr, orderbasenum_dec) values(
                                                                       seq_recipeid.nextval, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            int intRowsCount = clsVO.Length;
            if (intRowsCount > 0)
            {
                object[][] objPrams = new object[21][];
                for (int i1 = 0; i1 < 21; i1++)
                {
                    objPrams[i1] = new object[intRowsCount];
                }
                for (int i = 0; i < intRowsCount; i++)
                {
                    objPrams[0][i] = strRecipe;
                    objPrams[1][i] = clsVO[i].m_strRowNo;
                    objPrams[2][i] = clsVO[i].m_strItemID;
                    objPrams[3][i] = clsVO[i].m_decPrice;
                    objPrams[4][i] = clsVO[i].m_strOprDeptID;
                    objPrams[5][i] = clsVO[i].m_decDiscount;
                    objPrams[6][i] = clsVO[i].m_decTolPrice;
                    objPrams[7][i] = clsVO[i].m_decQty;
                    objPrams[8][i] = clsVO[i].strApplyID;
                    objPrams[9][i] = clsVO[i].m_strUSAGEPARENTID_VCHR;
                    objPrams[10][i] = clsVO[i].m_strATTACHPARENTID_VCHR;
                    objPrams[11][i] = clsVO[i].m_decAttachitembasenum;
                    objPrams[12][i] = clsVO[i].m_decUsageitembasenum;
                    objPrams[13][i] = clsVO[i].m_strItemname;
                    objPrams[14][i] = clsVO[i].m_strItemspec;
                    objPrams[15][i] = clsVO[i].m_strItemunit;
                    objPrams[16][i] = clsVO[i].m_strUsagedetail;
                    objPrams[17][i] = clsVO[i].m_intDeptmed;
                    objPrams[18][i] = clsVO[i].m_strUsageID;
                    objPrams[19][i] = clsVO[i].m_strOrderID;
                    objPrams[20][i] = clsVO[i].m_decOrderBaseNum;
                }
                DbType[] objTypes = new DbType[21] {
                        DbType.String,  DbType.String, DbType.String, DbType.Decimal, DbType.String,
                        DbType.Decimal, DbType.Decimal,DbType.Decimal,DbType.String,  DbType.String,
                        DbType.String,  DbType.Decimal, DbType.Decimal,DbType.String, DbType.String,
                        DbType.String,  DbType.String, DbType.Int32,   DbType.String, DbType.String,
                        DbType.Decimal
                    };
                try
                {
                    objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objPrams, objTypes);
                    objPrams = null;
                    objTypes = null;
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    objLogger = null;
                }
                finally
                {
                    objHRPSvc = null;
                    strSQL = null;
                    clsVO = null;
                }
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthSaveDetail6(clsOutpatientOtherRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1;
            clsHRPTableService objHRPSvc = null;

            string strSQL = @"insert into t_tmp_outpatientothrecipede(outpatrecipedeid_chr,rowno_chr,itemid_chr,unitid_chr,qty_dec,unitprice_mny,tolprice_mny,discount_dec,outpatrecipeid_chr,
                                                                       attachid_vchr,usageparentid_vchr,attachparentid_vchr, attachitembasenum_dec, usageitembasenum_dec, itemname_vchr, itemspec_vchr, itemunit_vchr, itemusagedetail_vchr, deptmed_int) values( 
                                                                       seq_recipeid.nextval, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            int intRowsCount = clsVO.Length;
            if (intRowsCount > 0)
            {
                object[][] objParams = new object[18][];
                for (int i1 = 0; i1 < 18; i1++)
                {
                    objParams[i1] = new object[intRowsCount];
                }
                for (int i = 0; i < intRowsCount; i++)
                {
                    objParams[0][i] = clsVO[i].m_strRowNo;
                    objParams[1][i] = clsVO[i].m_strItemID;
                    objParams[2][i] = clsVO[i].m_strUnit;
                    objParams[3][i] = clsVO[i].m_decQty;
                    objParams[4][i] = clsVO[i].m_decPrice;
                    objParams[5][i] = clsVO[i].m_decTolPrice;
                    objParams[6][i] = clsVO[i].m_decDiscount;
                    objParams[7][i] = strRecipe;
                    objParams[8][i] = clsVO[i].strApplyID;
                    objParams[9][i] = clsVO[i].m_strUSAGEPARENTID_VCHR;
                    objParams[10][i] = clsVO[i].m_strATTACHPARENTID_VCHR;
                    objParams[11][i] = clsVO[i].m_decAttachitembasenum;
                    objParams[12][i] = clsVO[i].m_decUsageitembasenum;
                    objParams[13][i] = clsVO[i].m_strItemname;
                    objParams[14][i] = clsVO[i].m_strItemspec;
                    objParams[15][i] = clsVO[i].m_strItemunit;
                    objParams[16][i] = clsVO[i].m_strUsagedetail;
                    objParams[17][i] = clsVO[i].m_intDeptmed;
                }
                DbType[] objTypes = new DbType[18] {
                        DbType.String,  DbType.String, DbType.String, DbType.Decimal, DbType.Decimal,
                        DbType.Decimal, DbType.Decimal,DbType.String,DbType.String,  DbType.String,
                        DbType.String,  DbType.Decimal, DbType.Decimal,DbType.String, DbType.String,
                        DbType.String,  DbType.String, DbType.Int32
                    };
                try
                {
                    objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objParams, objTypes);
                    objParams = null;
                    objTypes = null;
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                    objLogger = null;
                }
                finally
                {
                    objHRPSvc = null;
                    strSQL = null;
                    clsVO = null;
                }
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthSaveDetailOrder(clsOutpatientOrderRecipeDe_VO[] clsVO, string strRecipe)
        {
            long lngRes = 1, lngAffects = 0;

            if (clsVO == null)
            {
                return lngRes;
            }

            try
            {
                string ID = "";
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                string strSQL = "";
                for (int i = 0; i < clsVO.Length; i++)
                {
                    strSQL = @"insert into T_OPR_OUTPATIENT_ORDERDIC(ORDERID_INT, OUTPATRECIPEID_CHR, TABLEINDEX_INT, ORDERQUE_INT, ORDERDICID_CHR, ORDERDICNAME_VCHR, SPEC_VCHR, QTY_DEC, ATTACHID_VCHR, 
                                                                     SAMPLEID_VCHR, CHECKPARTID_VCHR, pricemny_dec, totalmny_dec, attachorderid_vchr, attachorderbasenum_dec, sbbasemny_dec, isEmer) Values(
                                                                     seq_recipeorderid.NEXTVAL, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    IDataParameter[] ParamArr = null;
                    objHRPSvc.CreateDatabaseParameter(16, out ParamArr);
                    ParamArr[0].Value = strRecipe;
                    ParamArr[1].Value = clsVO[i].m_strTableIndex;
                    ParamArr[2].Value = clsVO[i].m_strRowNo;
                    ParamArr[3].Value = clsVO[i].m_strOrderID;
                    ParamArr[4].Value = clsVO[i].m_strOrderName;
                    ParamArr[5].Value = clsVO[i].m_strOrderSpec;
                    ParamArr[6].Value = clsVO[i].m_decQty;
                    ParamArr[7].Value = clsVO[i].m_strAttachID;
                    ParamArr[8].Value = clsVO[i].m_strSampleID;
                    ParamArr[9].Value = clsVO[i].m_strCheckPartID;
                    ParamArr[10].Value = clsVO[i].m_decPriceMny;
                    ParamArr[11].Value = clsVO[i].m_decTotalMny;
                    ParamArr[12].Value = clsVO[i].m_strAttachOrderID;
                    ParamArr[13].Value = clsVO[i].m_decAttachOrderBaseNum;
                    ParamArr[14].Value = clsVO[i].m_decSbBaseMny;
                    ParamArr[15].Value = clsVO[i].isEmer;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                    if (lngRes < 1)
                    {
                        return lngRes;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #endregion

        #region ɾ��������ϸ
        [AutoComplete]
        public long m_mthDeleteRecipeDetail(string ID)
        {
            long lngRes = 0;

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                string strSQL = "P_DELETERECIPEBYID ";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[1];

                objParams[0] = new clsSQLParamDefinitionVO();
                objParams[0].objParameter_Value = ID;
                objParams[0].strParameter_Type = clsOracleDbType.strChar;
                objParams[0].strParameter_Name = "RecipeID";
                lngRes = objHRPSvc.lngExecuteParameterProc(strSQL, objParams);
                if (lngRes < 1)
                {
                    return lngRes;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID���Ҵ�����
        [AutoComplete]
        public long m_mthGetRepiceNo(string type, out DataTable dt, string ID)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select outpatrecipeid_chr from t_opr_outpatientrecipe where patientid_chr = ? and pstauts_int= ?";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;


            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = ID;
                ParamArr[1].Value = type;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ���ݴ����Ų�����ϸ
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindDiagnoses(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select diagimport_vchr,diagmemo_vchr,diagstd_vchr,curprinciple_vchr,curestd_vchr,defend_vchr  from t_opr_outpatientdiagrec
                               where outpatientdiagrecid_chr=?";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindCaseHistory(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.casehisid_chr,
       a.modifydate_dat,
       a.patientid_chr,
       a.registerid_chr,
       a.diagdr_chr,
       a.diagdept_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.status_int,
       a.diagmain_vchr,
       a.diagmain_xml_vchr,
       a.diagcurr_vchr,
       a.diagcurr_xml_vhcr,
       a.diaghis_vchr,
       a.diaghis_xml_vchr,
       a.aidcheck_vchr,
       a.aidcheck_xml_vchr,
       a.diag_vchr,
       a.diag_xml_vchr,
       a.treatment_vchr,
       a.treatment_xml_vchr,
       a.remark_vchr,
       a.remark_xml_vchr,
       a.anaphylaxis_vchr,
       a.bodycheck_vchr,
       a.bodychrck_xml_vchr,
       a.prihis_vchr,
       a.prihis_xml_vchr,
       a.parcasehisid_chr,
       a.anaphylaxis_xml_vchr,
       a.caldept_vchr,
       a.caldept_xml_vchr,
       a.iszzsq 
 from  t_opr_outpatientcasehis a,t_opr_outpatientrecipe b
                                where a.casehisid_chr=b.casehisid_chr(+)
                                and b.outpatrecipeid_chr = ?";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPatientID"></param>
        /// <param name="strDoctorID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindMaxCaseHistory(string strPatientID, string strDoctorID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select   casehisid_chr, modifydate_dat, patientid_chr, registerid_chr,
                                       diagdr_chr, diagdept_chr, recordemp_chr, recorddate_dat, status_int,
                                       diagmain_vchr, diagmain_xml_vchr, diagcurr_vchr, diagcurr_xml_vhcr,
                                       diaghis_vchr, diaghis_xml_vchr, aidcheck_vchr, aidcheck_xml_vchr,
                                       diag_vchr, diag_xml_vchr, treatment_vchr, treatment_xml_vchr,
                                       remark_vchr, remark_xml_vchr, anaphylaxis_vchr, bodycheck_vchr,
                                       bodychrck_xml_vchr, prihis_vchr, prihis_xml_vchr, parcasehisid_chr,
                                       anaphylaxis_xml_vchr, caldept_vchr, caldept_xml_vchr, iszzsq 
                                  from t_opr_outpatientcasehis
                                 where modifydate_dat =
                                          (select   max (modifydate_dat) as modifydate_dat
                                               from t_opr_outpatientcasehis
                                              where patientid_chr = ?
                                                and diagdr_chr = ?
                                                and modifydate_dat between ? and ?
                                           group by patientid_chr)
                                   and patientid_chr = ? ";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = strPatientID;
                ParamArr[1].Value = strDoctorID;
                ParamArr[2].DbType = DbType.DateTime;
                ParamArr[2].Value = DateTime.Parse(DateTime.Now.ToShortDateString() + " 00:00:00");
                ParamArr[3].DbType = DbType.DateTime;
                ParamArr[3].Value = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                ParamArr[4].Value = strPatientID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_mthFindRecipeDetail1(string ID, out DataTable dt, bool flag, bool isChildPrice)//��ҩ
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select a.outpatrecipedeid_chr,a.rowno_chr,a.itemid_chr,a.unitid_chr,a.usageid_chr,a.tolqty_dec,
       a.unitprice_mny,a.tolprice_mny,a.days_int,a.qty_dec,a.discount_dec,a.outpatrecipeid_chr,
       a.freqid_chr,a.hypetest_int,a.desc_vchr,a.usageparentid_vchr,a.attachparentid_vchr,
       a.itemspec_vchr,a.dosage_dec,a.dosageunit_chr,a.attachitembasenum_dec,a.usageitembasenum_dec,
       a.itemname_vchr,a.deptmed_int,a.rowno_vchr2, b.usagename_vchr, c.freqname_chr, c.times_int, c.days_int days,
                                     d.itemcode_vchr, d.itemengname_vchr, d.ifstop_int, 
                                     d.packqty_dec, d.opchargeflg_int, d.dosage_dec, d.itemipunit_chr, 
                                     d.itemopunit_chr, d.itemopinvtype_chr, e.hype_int, e.noqtyflag_int, 
                                     d.itemspec_vchr as spec, d.opchargeflg_int, d.itemopunit_chr, 
                                     {0}, {1},
                                     d.dosageunit_chr as dosageunit,a.toldiffprice_mny,d.tradeprice_mny, round(d.tradeprice_mny / d.packqty_dec, 4) subtrademoney
                                from t_tmp_outpatientpwmrecipede a,
                                     t_bse_usagetype b,
                                     t_aid_recipefreq c,
                                     t_bse_chargeitem d,
                                     t_bse_medicine e
                               where a.usageid_chr = b.usageid_chr(+)
                                 and a.freqid_chr = c.freqid_chr(+)
                                 and a.itemid_chr = d.itemid_chr(+)
                                 and d.itemsrcid_vchr = e.medicineid_chr(+)
                                 and outpatrecipeid_chr = ? order by rowno_chr";

            if (flag)
            {
                strSQL = @"select a.outpatrecipedeid_chr,a.rowno_chr,a.itemid_chr,a.unitid_chr,a.usageid_chr,a.tolqty_dec,
       a.unitprice_mny,a.tolprice_mny,a.days_int,a.qty_dec,a.discount_dec,a.outpatrecipeid_chr,
       a.freqid_chr,a.hypetest_int,a.desc_vchr,a.usageparentid_vchr,a.attachparentid_vchr,
       a.itemspec_vchr,a.dosage_dec,a.dosageunit_chr,a.attachitembasenum_dec,a.usageitembasenum_dec,
       a.itemname_vchr,a.deptmed_int,a.rowno_vchr2, b.usagename_vchr, c.freqname_chr, c.times_int, c.days_int days,
                                 d.itemcode_vchr, d.itemengname_vchr, d.ifstop_int, 
                                 d.packqty_dec, 1 opchargeflg_int, d.dosage_dec, d.itemipunit_chr, 
                                 d.itemopunit_chr, d.itemopinvtype_chr,e.hype_int, e.noqtyflag_int, 
                                 d.itemspec_vchr as spec, d.opchargeflg_int, d.itemopunit_chr,  
                                 {0}, {1},
                                 d.dosageunit_chr as dosageunit    
                            from t_opr_outpatientpwmrecipede a,
                                 t_bse_usagetype b,
                                 t_aid_recipefreq c,
                                 t_bse_chargeitem d,
                                 t_bse_medicine e
                           where a.usageid_chr = b.usageid_chr(+)
                             and a.freqid_chr = c.freqid_chr(+)
                             and a.itemid_chr = d.itemid_chr(+)
                             and d.itemsrcid_vchr = e.medicineid_chr(+)  
                             and outpatrecipeid_chr = ? order by rowno_chr";
            }

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case d.ischildprice when 1 then (d.itemprice_mny * " + EntityChildPrice.AddScale + ") else d.itemprice_mny end) as itemprice_mny", "(case d.ischildprice when 1 then round(d.itemprice_mny * " + EntityChildPrice.AddScale + " / d.packqty_dec, 4) else round(d.itemprice_mny / d.packqty_dec, 4) end) as submoney");
            else
                strSQL = string.Format(strSQL, "d.itemprice_mny", "round(d.itemprice_mny / d.packqty_dec, 4) as submoney");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail2(string ID, out DataTable dt, bool flag, bool isChildPrice)//��ҩ
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatientcmrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatientcmrecipede";
            }
            string strSQL = @"select a.itemid_chr itemid,
       a.unitid_chr unit,
       a.qty_dec quantity,
       a.deptmed_int,
       a.unitprice_mny price,
       a.tolprice_mny summoney,
       a.usageid_chr,
       a.rowno_chr,
       a.sumusage_vchr,
       b.ifstop_int,
       a.itemname_vchr itemname,
       a.itemspec_vchr dec,
       b.itemcode_vchr,
       a.usageparentid_vchr,
       a.attachparentid_vchr,
       b.itemopinvtype_chr invtype,
       b.itemcatid_chr catid,
       a.discount_dec,
       a.usagedetail_vchr,
       a.times_int times,
       a.min_qty_dec,
       {0}, {1},
       b.itemopinvtype_chr,
       b.itemengname_vchr,
       b.opchargeflg_int,
       b.packqty_dec,
       b.dosage_dec,
       c.mindosage_dec,
       c.maxdosage_dec,
       d.usagename_vchr,
       c.noqtyflag_int,
       b.itemspec_vchr spec,       
       b.itemipunit_chr as dosageunit,
       a.toldiffprice_mny,
       b.tradeprice_mny,
       round(b.tradeprice_mny / b.packqty_dec, 4) subtrademoney
  from " + strTableName + @" a,
       t_bse_chargeitem      b,
       t_bse_medicine        c,
       t_bse_usagetype       d
 where a.itemid_chr = b.itemid_chr(+)
   and b.itemsrcid_vchr = c.medicineid_chr(+)
   and a.usageid_chr = d.usageid_chr(+)  
   and outpatrecipeid_chr = ?";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny", "(case b.ischildprice when 1 then round(b.itemprice_mny * " + EntityChildPrice.AddScale + " / b.packqty_dec, 4) else round(b.itemprice_mny / b.packqty_dec, 4) end) as submoney");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny", "round(b.itemprice_mny / b.packqty_dec, 4) as submoney");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail3(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatientchkrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatientchkrecipede";
            }
            string strSQL = @"select a.itemid_chr itemid,
                                   a.itemunit_vchr unit,
                                   a.qty_dec quantity,
                                   a.deptmed_int,
                                   a.price_mny price,
                                   a.tolprice_mny summoney,
                                   b.itemengname_vchr,
                                   a.itemusagedetail_vchr,
                                   b.ifstop_int,
                                   a.itemname_vchr itemname,
                                   a.itemspec_vchr dec,
                                   b.itemopinvtype_chr,
                                   b.itemcode_vchr,
                                   b.itemopinvtype_chr invtype,
                                   b.itemcatid_chr catid,
                                   a.discount_dec,
                                   a.attachid_vchr,
                                   a.attachitembasenum_dec,
                                   a.usageitembasenum_dec,
                                   b.selfdefine_int selfdefine,
                                   1 times,
                                   a.sampleid_vchr,
                                   g.sample_type_desc_vchr,
                                   a.usageparentid_vchr,
                                   a.attachparentid_vchr,
                                   a.quickflag_int,
                                   a.rowno_chr,
                                   a.orderid_vchr,
                                   a.orderbasenum_dec,
                                   b.itemspec_vchr spec,
                                   {0},
                                   b.itemunit_chr
                              from " + strTableName + @" a, t_bse_chargeitem b, t_aid_lis_sampletype g
                             where a.itemid_chr = b.itemid_chr(+)
                               and a.sampleid_vchr = g.sample_type_id_chr(+)   
                               and outpatrecipeid_chr = ?
                             order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny");

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail3ByOrder(string RecID, string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatientchkrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatientchkrecipede";
            }
            string strSQL = @"select a.itemid_chr,
                                   a.itemunit_vchr unit,
                                   a.qty_dec quantity,
                                   a.deptmed_int,
                                   a.price_mny price,
                                   a.tolprice_mny summoney,
                                   b.itemengname_vchr,
                                   a.itemusagedetail_vchr,
                                   b.ifstop_int,
                                   a.itemname_vchr itemname,
                                   a.itemspec_vchr dec,
                                   b.itemopinvtype_chr,
                                   b.itemcode_vchr,
                                   b.itemopinvtype_chr invtype,
                                   b.itemcatid_chr catid,
                                   a.discount_dec,
                                   a.attachid_vchr,
                                   a.attachitembasenum_dec,
                                   a.usageitembasenum_dec,
                                   b.selfdefine_int selfdefine,
                                   1 times,
                                   a.sampleid_vchr,
                                   g.sample_type_desc_vchr,
                                   a.usageparentid_vchr,
                                   a.attachparentid_vchr,
                                   a.quickflag_int,
                                   a.rowno_chr,
                                   a.orderid_vchr,
                                   a.orderbasenum_dec,
                                   b.itemspec_vchr spec,
                                   {0},
                                   b.itemunit_chr,
                                   a.qty_dec totalqty_dec,
                                   a.discount_dec as precent_dec
                              from " + strTableName + @"     a,
                                   t_bse_chargeitem          b,
                                   t_aid_lis_sampletype      g,
                                   t_opr_outpatient_orderdic ord
                             where a.itemid_chr = b.itemid_chr(+)
                               and a.sampleid_vchr = g.sample_type_id_chr(+)
                               and a.outpatrecipeid_chr = ord.outpatrecipeid_chr
                               and ('[PK]' || a.orderid_vchr) = ord.attachorderid_vchr 
                               and ord.tableindex_int = 3
                               and ord.outpatrecipeid_chr = ?
                               and ord.attachorderid_vchr = ?
                             order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RecID;
                ParamArr[1].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail4(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatienttestrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatienttestrecipede";
            }
            string strSQL = @"select a.itemid_chr itemid,
       a.itemunit_vchr unit,
       a.qty_dec quantity,
       a.deptmed_int,
       a.price_mny price,
       a.tolprice_mny summoney,
       b.itemengname_vchr,
       a.itemusagedetail_vchr,
       b.ifstop_int,
       a.usageid_chr,
       a.itemname_vchr itemname,
       a.itemspec_vchr dec,
       b.itemopinvtype_chr,
       b.itemcode_vchr,
       b.itemopinvtype_chr invtype,
       b.itemcatid_chr catid,
       a.discount_dec,
       a.attachid_vchr,
       a.attachitembasenum_dec,
       a.usageitembasenum_dec,
       b.selfdefine_int selfdefine,
       1 times,
       a.checkpartid_vchr,
       d.partname,
       a.usageparentid_vchr,
       a.attachparentid_vchr,
       a.rowno_chr,
       a.orderid_vchr,
       a.orderbasenum_dec,
       b.itemspec_vchr spec,
       {0},
       b.itemunit_chr
  from " + strTableName + @" a, t_bse_chargeitem b, ar_apply_partlist d
 where a.itemid_chr = b.itemid_chr(+)
   and a.checkpartid_vchr = d.partid(+)   
   and outpatrecipeid_chr = ?
 order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail4ByOrder(string RecID, string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatienttestrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatienttestrecipede";
            }
            string strSQL = @"select a.itemid_chr,
                                       a.itemunit_vchr unit,
                                       a.qty_dec quantity,
                                       a.deptmed_int,
                                       a.price_mny price,
                                       a.tolprice_mny summoney,
                                       b.itemengname_vchr,
                                       a.itemusagedetail_vchr,
                                       b.ifstop_int,
                                       a.usageid_chr,
                                       a.itemname_vchr itemname,
                                       a.itemspec_vchr dec,
                                       b.itemopinvtype_chr,
                                       b.itemcode_vchr,
                                       b.itemopinvtype_chr invtype,
                                       b.itemcatid_chr catid,
                                       a.discount_dec,
                                       a.attachid_vchr,
                                       a.attachitembasenum_dec,
                                       a.usageitembasenum_dec,
                                       b.selfdefine_int selfdefine,
                                       1 times,
                                       a.checkpartid_vchr,
                                       d.partname,
                                       a.usageparentid_vchr,
                                       a.attachparentid_vchr,
                                       a.rowno_chr,
                                       a.orderid_vchr,
                                       a.orderbasenum_dec,
                                       b.itemspec_vchr spec,
                                       {0},
                                       b.itemunit_chr,
                                       a.qty_dec totalqty_dec,
                                       a.discount_dec as precent_dec
                                  from " + strTableName + @"     a,
                                       t_bse_chargeitem          b,
                                       ar_apply_partlist         d,
                                       t_opr_outpatient_orderdic ord
                                 where a.itemid_chr = b.itemid_chr(+)
                                   and a.checkpartid_vchr = d.partid(+)
                                   and ('[PK]' || a.orderid_vchr) = ord.attachorderid_vchr
                                   and a.outpatrecipeid_chr = ord.outpatrecipeid_chr   
                                   and ord.tableindex_int = 4
                                   and ord.outpatrecipeid_chr = ?
                                   and ord.attachorderid_vchr = ?
                                 order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RecID;
                ParamArr[1].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail5(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatientopsrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatientopsrecipede";
            }
            string strSQL = @"select a.itemid_chr itemid,
       a.itemunit_vchr unit,
       a.qty_dec quantity,
       a.deptmed_int,
       a.price_mny price,
       a.tolprice_mny summoney,
       b.itemengname_vchr,
       a.itemusagedetail_vchr,
       b.ifstop_int,
       a.usageid_chr,
       a.itemname_vchr itemname,
       a.itemspec_vchr dec,
       b.itemopinvtype_chr,
       b.itemcode_vchr,
       b.itemopinvtype_chr invtype,
       b.itemcatid_chr catid,
       a.discount_dec,
       a.attachid_vchr,
       b.selfdefine_int selfdefine,
       1 times,
       a.usageparentid_vchr,
       a.attachparentid_vchr,
       a.attachitembasenum_dec,
       a.usageitembasenum_dec,
       a.rowno_chr,
       a.orderid_vchr,
       a.orderbasenum_dec,
       b.itemspec_vchr spec,
       {0},
       b.itemunit_chr
  from " + strTableName + @" a, t_bse_chargeitem b
 where a.itemid_chr = b.itemid_chr(+)   
   and outpatrecipeid_chr = ?
 order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail5ByOrder(string RecID, string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatientopsrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatientopsrecipede";
            }
            string strSQL = @"select a.itemid_chr,
                                   a.itemunit_vchr unit,
                                   a.qty_dec quantity,
                                   a.deptmed_int,
                                   a.price_mny price,
                                   a.tolprice_mny summoney,
                                   b.itemengname_vchr,
                                   a.itemusagedetail_vchr,
                                   b.ifstop_int,
                                   a.usageid_chr,
                                   a.itemname_vchr itemname,
                                   a.itemspec_vchr dec,
                                   b.itemopinvtype_chr,
                                   b.itemcode_vchr,
                                   b.itemopinvtype_chr invtype,
                                   b.itemcatid_chr catid,
                                   a.discount_dec,
                                   a.attachid_vchr,
                                   b.selfdefine_int selfdefine,
                                   1 times,
                                   a.usageparentid_vchr,
                                   a.attachparentid_vchr,
                                   a.attachitembasenum_dec,
                                   a.usageitembasenum_dec,
                                   a.rowno_chr,
                                   a.orderid_vchr,
                                   a.orderbasenum_dec,
                                   b.itemspec_vchr spec,
                                   {0},
                                   b.itemunit_chr,
                                   a.qty_dec as totalqty_dec,
                                   a.discount_dec as precent_dec
                              from " + strTableName + @"     a,
                                   t_bse_chargeitem          b,
                                   t_opr_outpatient_orderdic ord
                             where a.itemid_chr = b.itemid_chr(+)
                               and a.outpatrecipeid_chr = ord.outpatrecipeid_chr
                               and ('[PK]' || a.orderid_vchr) = ord.attachorderid_vchr    
                               and ord.tableindex_int = 5
                               and ord.outpatrecipeid_chr = ?
                               and ord.attachorderid_vchr = ?
                             order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RecID;
                ParamArr[1].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthFindRecipeDetail6(string ID, out DataTable dt, bool flag, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strTableName = "t_tmp_outpatientothrecipede";
            if (flag)
            {
                strTableName = "t_opr_outpatientothrecipede";
            }
            string strSQL = @"select a.itemid_chr itemid,
       a.itemunit_vchr unit,
       a.qty_dec quantity,
       a.deptmed_int,
       a.unitprice_mny price,
       a.tolprice_mny summoney,
       b.itemengname_vchr,
       a.itemusagedetail_vchr,
       b.ifstop_int,
       a.itemname_vchr itemname,
       a.itemspec_vchr dec,
       b.itemopinvtype_chr,
       b.itemcode_vchr,
       b.itemopinvtype_chr invtype,
       b.itemcatid_chr catid,
       a.discount_dec,
       a.attachid_vchr,
       b.selfdefine_int selfdefine,
       1 times,
       a.usageparentid_vchr,
       a.attachparentid_vchr,
       a.attachitembasenum_dec,
       a.usageitembasenum_dec,
       a.rowno_chr,
       b.itemspec_vchr spec,
       {0},
       b.itemunit_chr,
       b.opchargeflg_int,
       {1},
       b.itemopunit_chr, 
       b.itemipunit_chr,
       {2} 
  from " + strTableName + @" a, t_bse_chargeitem b
 where a.itemid_chr = b.itemid_chr(+)  
   and a.outpatrecipeid_chr = ?
 order by a.rowno_chr";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny", "(case b.ischildprice when 1 then round(b.itemprice_mny * " + EntityChildPrice.AddScale + " / b.packqty_dec, 4) else round(b.itemprice_mny / b.packqty_dec, 4) end) as submoney", "(case b.ischildprice when 1 then (b.itemprice_mny * " + EntityChildPrice.AddScale + ") else b.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "b.itemprice_mny", "round(b.itemprice_mny / b.packqty_dec, 4) as submoney", "b.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_mthFindRecipeDetailOrder(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select a.orderid_int, a.outpatrecipeid_chr, a.tableindex_int,
                                     a.orderque_int, a.orderdicid_chr, a.orderdicname_vchr, a.spec_vchr,
                                     a.qty_dec, a.attachid_vchr, a.sampleid_vchr, a.checkpartid_vchr,
                                     a.pricemny_dec, a.totalmny_dec, a.attachorderid_vchr,
                                     a.attachorderbasenum_dec, b.status_int, d.sample_type_desc_vchr,
                                     f.partname, b.usercode_chr, b.engname_vchr, a.usageid_chr,
                                     b.lisapplyunitid_chr, b.applytypeid_chr, a.sbbasemny_dec,b.itemid_chr
                                from t_opr_outpatient_orderdic a,
                                     t_bse_bih_orderdic b,
                                     t_aid_lis_sampletype d,
                                     ar_apply_partlist f
                               where a.orderdicid_chr = b.orderdicid_chr
                                 and a.sampleid_vchr = d.sample_type_id_chr(+)
                                 and a.checkpartid_vchr = f.partid(+)
                                 and a.outpatrecipeid_chr = ?
                            order by a.tableindex_int, a.orderque_int ";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;

            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region �շѸ��ݴ����Ų�ѯ������ϸ
        /// <summary>
        /// �շѸ��ݴ����Ų�ѯ������ϸ
        /// </summary>
        /// <param name="p_strRecipeNo"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeDetail(string p_strRecipeNo, out DataTable dtResult)
        {
            long lngRes = -1;
            dtResult = new DataTable();
            try
            {
                string SQL = @"select a.itemname_vchr     itemname,
                               ''                  opritemname,
                               a.tolqty_dec        quantity,
                               a.itemspec_vchr     itemspec,
                               a.unitid_chr        unit,
                               a.unitprice_mny     itemprice,
                               a.tolprice_mny      summoney
                          from t_tmp_outpatientpwmrecipede a
                         where a.outpatrecipeid_chr = ?
                        union all
                        select a.itemname_vchr     itemname,
                               ''                  opritemname,
                               a.min_qty_dec       quantity,
                               a.itemspec_vchr     itemspec,
                               a.unitid_chr        unit,
                               a.unitprice_mny     itemprice,
                               a.tolprice_mny      summoney
                          from t_tmp_outpatientcmrecipede a
                         where a.outpatrecipeid_chr = ?
                        union all
                        select a.itemname_vchr     itemname,
                               c.orderdicname_vchr opritemname,
                               a.qty_dec           quantity,
                               a.itemspec_vchr     itemspec,
                               a.itemunit_vchr     unit,
                               a.price_mny         itemprice,
                               a.tolprice_mny      summoney
                          from t_tmp_outpatientchkrecipede a,
                               t_aid_bih_orderdic_charge   b,
                               t_opr_outpatient_orderdic   c
                         where b.orderdicid_chr = c.orderdicid_chr(+)
                           and a.itemid_chr = b.itemid_chr
                           and c.outpatrecipeid_chr = a.outpatrecipeid_chr
                           and a.outpatrecipeid_chr = ?
                        union all
                        select a.itemname_vchr     itemname,
                               c.orderdicname_vchr opritemname,
                               a.qty_dec           quantity,
                               a.itemspec_vchr     itemspec,
                               a.itemunit_vchr     unit,
                               a.price_mny         itemprice,
                               a.tolprice_mny      summoney
                          from t_tmp_outpatienttestrecipede a,
                               t_aid_bih_orderdic_charge    b,
                               t_opr_outpatient_orderdic    c
                         where b.orderdicid_chr = c.orderdicid_chr(+)
                           and a.itemid_chr = b.itemid_chr
                           and c.outpatrecipeid_chr = a.outpatrecipeid_chr
                           and a.outpatrecipeid_chr = ?
                        union all
                        select a.itemname_vchr     itemname,
                               c.orderdicname_vchr opritemname,
                               a.qty_dec           quantity,
                               a.itemspec_vchr     itemspec,
                               a.itemunit_vchr     unit,
                               a.price_mny         itemprice,
                               a.tolprice_mny      summoney
                          from t_tmp_outpatientopsrecipede a,
                               t_aid_bih_orderdic_charge   b,
                               t_opr_outpatient_orderdic   c
                         where b.orderdicid_chr = c.orderdicid_chr(+)
                           and a.itemid_chr = b.itemid_chr
                           and c.outpatrecipeid_chr = a.outpatrecipeid_chr
                           and a.outpatrecipeid_chr = ?
                        union all
                        select a.itemname_vchr     itemname,
                               ''                  opritemname,
                               a.qty_dec           quantity,
                               a.itemspec_vchr     itemspec,
                               a.itemunit_vchr     unit,
                               a.unitprice_mny     itemprice,
                               a.tolprice_mny      summoney
                          from t_tmp_outpatientothrecipede a
                         where a.outpatrecipeid_chr = ?";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param;
                objHRPSvc.CreateDatabaseParameter(6, out param);
                param[0].Value = p_strRecipeNo;
                param[1].Value = p_strRecipeNo;
                param[2].Value = p_strRecipeNo;
                param[3].Value = p_strRecipeNo;
                param[4].Value = p_strRecipeNo;
                param[5].Value = p_strRecipeNo;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtResult, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            return lngRes;
        }
        #endregion

        #region �����÷����Ҹ��ӵ��շ���Ŀ
        [AutoComplete]
        public long m_mthGetChargeItemByUsageID(string strID, out DataTable dt, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select decode (a.opchargeflg_int,
                 0, itemopunit_chr,
                 1, itemipunit_chr
                ) as itemopunit_chr,
               decode (a.opchargeflg_int,
                 0, {0},
                 1, {1}
                ) as itemprice_mny,a.itemid_chr,
       a.itemname_vchr,
       a.itemcode_vchr,
       a.itempycode_chr,
       a.itemwbcode_chr,
       a.itemsrcid_vchr,
       a.itemsrctype_int,
       a.itemspec_vchr,
       a.itemprice_mny,
       a.itemunit_chr,
       a.itemopunit_chr,
       a.itemipunit_chr,
       a.itemopcalctype_chr,
       a.itemipcalctype_chr,
       a.itemopinvtype_chr,
       a.itemipinvtype_chr,
       a.dosage_dec,
       a.dosageunit_chr,
       a.isgroupitem_int,
       a.itemcatid_chr,
       a.usageid_chr,
       a.itemopcode_chr,
       a.insuranceid_chr,
       a.selfdefine_int,
       a.packqty_dec,
       a.tradeprice_mny,
       a.poflag_int,
       a.isrich_int,
       a.opchargeflg_int,
       a.itemsrcname_vchr,
       a.itemsrctypename_vchr,
       a.itemengname_vchr,
       a.ifstop_int,
       a.pdcarea_vchr,
       a.ipchargeflg_int,
       a.insurancetype_vchr,
       a.apply_type_int,
       a.itembihctype_chr,
       a.defaultpart_vchr,
       a.itemchecktype_chr,
       a.itemcommname_vchr,
       a.ordercateid_chr,
       a.freqid_chr,
       a.inpinsurancetype_vchr,
       a.ordercateid1_chr,
       a.isselfpay_chr,
       a.itemprice_mny_old,
       a.itemprice_mny_new,
       a.keepuse_int,
       a.price_temp,
       a.itemspec_vchr1,
       a.lastchange_dat, b.qty_dec, b.continueusetype_int, g.sample_type_id_chr,
                                     h.sample_type_desc_vchr, d.partname, e.deptprep_int
                                from t_bse_chargeitem a,
                                     t_bse_chargeitemusagegroup b,
                                     t_aid_lis_apply_unit g,
                                     t_aid_lis_sampletype h,
                                     ar_apply_partlist d,
                                     t_bse_medicine e
                               where b.itemid_chr = a.itemid_chr(+)                                 
                                 and a.itemsrcid_vchr = g.apply_unit_id_chr(+)
                                 and g.sample_type_id_chr = h.sample_type_id_chr(+)
                                 and a.itemchecktype_chr = d.partid(+)
                                 and b.qty_dec > 0
                                 and trim(a.itemsrcid_vchr) = trim(e.medicineid_chr(+))
                                 and b.usageid_chr = ?";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end)", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end)");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4)");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_mthGetChargeItemByUsageID(string strID, string p_strPayTypeID, out DataTable dt, bool isChildPrice)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select /*decode(a.opchargeflg_int, 0, itemopunit_chr, 1, itemipunit_chr) as itemopunit_chr,
                                       decode(a.opchargeflg_int,
                                              0,
                                              a.itemprice_mny,
                                              1,
                                              round(a.itemprice_mny / a.packqty_dec, 4)) as itemprice_mny,*/
                                       a.itemid_chr,
                                       a.itemname_vchr,
                                       a.itemcode_vchr,
                                       a.itempycode_chr,
                                       a.itemwbcode_chr,
                                       a.itemsrcid_vchr,
                                       a.itemsrctype_int,
                                       a.itemspec_vchr,
                                       {0},
                                       a.itemunit_chr,
                                       a.itemopunit_chr,
                                       a.itemipunit_chr,
                                       a.itemopcalctype_chr,
                                       a.itemipcalctype_chr,
                                       a.itemopinvtype_chr,
                                       a.itemipinvtype_chr,
                                       a.dosage_dec,
                                       a.dosageunit_chr,
                                       a.isgroupitem_int,
                                       a.itemcatid_chr,
                                       a.usageid_chr,
                                       a.itemopcode_chr,
                                       a.insuranceid_chr,
                                       a.selfdefine_int,
                                       a.packqty_dec,
                                       a.tradeprice_mny,
                                       a.poflag_int,
                                       a.isrich_int,
                                       a.opchargeflg_int,
                                       a.itemsrcname_vchr,
                                       a.itemsrctypename_vchr,
                                       a.itemengname_vchr,
                                       a.ifstop_int,
                                       a.pdcarea_vchr,
                                       a.ipchargeflg_int,
                                       a.insurancetype_vchr,
                                       a.apply_type_int,
                                       a.itembihctype_chr,
                                       a.defaultpart_vchr,
                                       a.itemchecktype_chr,
                                       a.itemcommname_vchr,
                                       a.ordercateid_chr,
                                       a.freqid_chr,
                                       a.inpinsurancetype_vchr,
                                       a.ordercateid1_chr,
                                       a.isselfpay_chr,
                                       a.itemprice_mny_old,
                                       a.itemprice_mny_new,
                                       a.keepuse_int,
                                       a.price_temp,
                                       a.itemspec_vchr1,
                                       b.clinictype_int,
                                       b.qty_dec,
                                       b.continueusetype_int,
                                       g.sample_type_id_chr,
                                       h.sample_type_desc_vchr,
                                       d.partname,
                                       e.deptprep_int,
                                       t.precent_dec 
                                  from t_bse_chargeitem           a,
                                       t_bse_chargeitemusagegroup b,
                                       t_aid_lis_apply_unit       g,
                                       t_aid_lis_sampletype       h,
                                       ar_apply_partlist          d,
                                       t_bse_medicine             e,
                                       t_aid_inschargeitem        t
                                 where b.itemid_chr = a.itemid_chr
                                   and a.itemsrcid_vchr = g.apply_unit_id_chr(+)
                                   and g.sample_type_id_chr = h.sample_type_id_chr(+)
                                   and a.itemchecktype_chr = d.partid(+)
                                   and a.itemsrcid_vchr = e.medicineid_chr(+)
                                   and a.itemid_chr = t.itemid_chr
                                   and b.qty_dec > 0   
                                   and b.usageid_chr = ?
                                   and t.copayid_chr = ?";

            if (isChildPrice)
                strSQL = string.Format(strSQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                strSQL = string.Format(strSQL, "a.itemprice_mny");

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = strID;
                ParamArr[1].Value = p_strPayTypeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ����ģ���б�
        /// <summary>
        /// ����ģ���б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ID">��ѯ����</param>
        /// <param name="strType">��ѯ�ֶ�</param>
        /// <param name="strCreatID">������ID</param>
        /// <param name="dt">�б�</param>
        /// <returns>�ɹ�1,ʧ�� 0</returns>
        [AutoComplete]
        public long m_mthFindAccordRecipe(string ID, string strType, string strCreatID, out DataTable dt, int flag)
        {
            dt = new DataTable();
            long lngRes = 0;

            bool blnTmp = true;
            try
            {
                long lng = long.Parse(ID.Replace("%", ""));
            }
            catch { blnTmp = false; }
            if (blnTmp)
            {
                strType = "usercode_chr";
            }

            string strSQL = @"select *
  from (select a.recipeid_chr,
               a.recipename_chr,
               a.usercode_chr,
               a.pycode_chr,
               a.wbcode_chr,
               a.diseasename_vchr
          from t_aid_concertrecipe a
         where a.flag_int = ?
           and a.privilege_int = 0
           and a.status_int = 1
           and (upper(a." + strType + ") like ? or a." + strType + @" is null)
        union
        select a.recipeid_chr,
               a.recipename_chr,
               a.usercode_chr,
               a.pycode_chr,
               a.wbcode_chr,
               a.diseasename_vchr
          from t_aid_concertrecipe a
         where a.flag_int = ?
           and a.privilege_int = 1
           and a.createrid_chr = ?
           and a.status_int = 1
           and (upper(a." + strType + ") like ? or a." + strType + @" is null)
        union
        select aa.recipeid_chr,
               aa.recipename_chr,
               aa.usercode_chr,
               aa.pycode_chr,
               aa.wbcode_chr,
               aa.diseasename_vchr
          from t_aid_concertrecipe aa,
               (select a.recipeid_chr
                  from t_aid_concertrecipedept a
                 where a.deptid_chr in (select deptid_chr
                                          from t_bse_deptemp
                                         where empid_chr = ?)) bb
         where aa.recipeid_chr = bb.recipeid_chr
           and aa.privilege_int = 2
           and aa.flag_int = ?
           and aa.status_int = 1
           and (upper(aa." + strType + @") like ? or
               aa." + strType + @" is null))
 order by recipename_chr";

            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                string strTempSql = "select * from t_sys_setting where setid_chr ='0032'";
                if (flag == 0)//ҽ����
                {
                    strTempSql = "select * from t_sys_setting where setid_chr ='0033'";
                }
                DataTable dtTemp = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strTempSql, ref dtTemp);
                bool tempFlag = false;
                if (dtTemp.Rows.Count > 0)//�������ã������0�򲻵��ã������1�������Э������
                {
                    tempFlag = dtTemp.Rows[0]["setstatus_int"].ToString().Trim() == "1";
                }
                if (tempFlag)//����ʹ�ñ�־��
                {
                    strSQL = @"select *
  from (select a.recipeid_chr,
               a.recipename_chr,
               a.usercode_chr,
               a.pycode_chr,
               a.wbcode_chr,
               a.diseasename_vchr
          from t_aid_concertrecipe a
         where a.privilege_int = 0
           and a.status_int = 1
           and (upper(a." + strType + ") like ? or a." + strType + @" is null)
        union
        select a.recipeid_chr,
               a.recipename_chr,
               a.usercode_chr,
               a.pycode_chr,
               a.wbcode_chr,
               a.diseasename_vchr
          from t_aid_concertrecipe a
         where a.privilege_int = 1
           and a.createrid_chr = ?
           and a.status_int = 1
           and (upper(a." + strType + ") like ? or a." + strType + @" is null)
        union
        select aa.recipeid_chr,
               aa.recipename_chr,
               aa.usercode_chr,
               aa.pycode_chr,
               aa.wbcode_chr,
               aa.diseasename_vchr
          from t_aid_concertrecipe aa,
               (select a.recipeid_chr
                  from t_aid_concertrecipedept a
                 where a.deptid_chr in (select deptid_chr
                                          from t_bse_deptemp
                                         where empid_chr = ?)) bb
         where aa.recipeid_chr = bb.recipeid_chr
           and aa.privilege_int = 2
           and aa.status_int = 1
           and (upper(aa." + strType + @") like ? or
               aa." + strType + @" is null))
 order by recipename_chr";

                    objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                    ParamArr[0].Value = ID + "%";
                    ParamArr[1].Value = strCreatID;
                    ParamArr[2].Value = ID + "%";
                    ParamArr[3].Value = strCreatID;
                    ParamArr[4].Value = ID + "%";
                }
                else
                {
                    objHRPSvc.CreateDatabaseParameter(8, out ParamArr);
                    ParamArr[0].Value = flag.ToString();
                    ParamArr[1].Value = ID + "%";
                    ParamArr[2].Value = flag.ToString();
                    ParamArr[3].Value = strCreatID;
                    ParamArr[4].Value = ID + "%";
                    ParamArr[5].Value = strCreatID;
                    ParamArr[6].Value = flag.ToString();
                    ParamArr[7].Value = ID + "%";
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region �������뵥����ĿԴID�����շ���Ŀ
        [AutoComplete]
        public long m_mthFindChargeItemByApplyBillID(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select A.ITEMID_CHR,A.ITEMNAME_VCHR,A.ITEMSPEC_VCHR,A.ITEMCODE_VCHR,
A.ITEMOPUNIT_CHR, a.itemprice_mny, A.SELFDEFINE_INT,A.ITEMCODE_VCHR type from t_bse_chargeitem A , T_BSE_CHARGECATMAP D
where  A.ITEMSRCID_VCHR = ? AND a.ITEMOPINVTYPE_CHR=d.catid_chr(+) and d.groupid_chr='0003' and d.INTERNALFLAG_INT=0 ";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = ID;
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������ĿID�������ҩƷ
        [AutoComplete]
        public long m_mthFindTabuByID(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select aa.itemid_chr, aa.itemname_vchr
  from t_bse_chargeitem aa,
       t_bse_medicine bb,
       (select c.refmedicinestdid_chr
          from t_bse_chargeitem a, t_bse_medicine b, t_bse_medstdrelation c
         where a.itemsrcid_vchr = b.medicineid_chr
           and b.medicinestdid_chr = c.medicinestdid_chr
           and a.itemid_chr = ?) cc
 where bb.medicinestdid_chr = cc.refmedicinestdid_chr
   and aa.itemsrcid_vchr = bb.medicineid_chr";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = ID;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                //objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                ID = null;
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ����������ĿID�������ҩƷ(סԺ��)
        [AutoComplete]
        public long m_mthFindTabuByID2(string ID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT dd.orderdicid_chr, ee.name_chr
  FROM t_bse_chargeitem aa,
       t_bse_medicine bb,
       (SELECT c.refmedicinestdid_chr
          FROM t_bse_chargeitem a,
               t_bse_medicine b,
               t_bse_medstdrelation c,
               t_aid_bih_orderdic_charge d
         WHERE a.itemsrcid_vchr = b.medicineid_chr
           AND b.medicinestdid_chr = c.medicinestdid_chr
           AND a.itemid_chr = d.itemid_chr
           AND d.orderdicid_chr = ?) cc,
       t_aid_bih_orderdic_charge dd,
       t_bse_bih_orderdic ee
 WHERE  cc.refmedicinestdid_chr=bb.medicinestdid_chr 
   AND bb.medicineid_chr =aa.itemsrcid_vchr 
   AND aa.itemid_chr = dd.itemid_chr
   AND dd.orderdicid_chr = ee.orderdicid_chr
";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
            ParamArr[0].Value = ID;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ��ϸ��Ϣ
        [AutoComplete]
        public void m_mthGetMedicineInfo(string ID, out string strText)
        {
            strText = "��ҩƷ��Ϣ����!";
            DataTable dt = new DataTable();
            long lngRes = 0;
            string strSQL = @" select c.context_vchr
                                 from t_bse_chargeitem a, t_bse_medicine b , t_bse_medicinestddesc c
                                where a.itemsrcid_vchr = trim(b.medicineid_chr(+))  
                                  and b.medicinestdid_chr = c.medicinestdid_chr(+)
                                  and a.itemid_chr = ?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = ID;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    strText = dt.Rows[0]["context_vchr"].ToString().Trim();
                    if (strText.Trim() == "")
                    {
                        strText = "��ҩƷ��Ϣ����!";
                    }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

        }

        [AutoComplete]
        public void m_mthGetMedicineInfo(string ID, out string strText, out string strRemark)
        {
            strText = "��ҩƷ��Ϣ����!";
            strRemark = "";
            DataTable dt = new DataTable();
            long lngRes = 0;
            string strSQL = @" select c.context_vchr, c.remark_vchr 
                                 from t_bse_chargeitem a, t_bse_medicine b , t_bse_medicinestddesc c
                                where a.itemsrcid_vchr = trim(b.medicineid_chr(+))  
                                  and b.medicinestdid_chr = c.medicinestdid_chr(+)
                                  and a.itemid_chr = ?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = ID;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    bool b = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["remark_vchr"].ToString().Trim() != "")
                        {
                            strText = dt.Rows[i]["context_vchr"].ToString().Trim();
                            if (strText.Trim() == "")
                            {
                                strText = "��ҩƷ��Ϣ����!";
                            }

                            strRemark = dt.Rows[i]["remark_vchr"].ToString().Trim();
                            b = true;
                            break;
                        }
                    }

                    if (!b)
                    {
                        strText = dt.Rows[0]["context_vchr"].ToString().Trim();
                        if (strText.Trim() == "")
                        {
                            strText = "��ҩƷ��Ϣ����!";
                        }

                        strRemark = dt.Rows[0]["remark_vchr"].ToString().Trim();
                    }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

        }
        #endregion

        #region ���ϴ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ID"></param>
        /// <param name="p_status"></param>
        /// <returns>ʧ��-4 ,�ɹ�ԭ��״̬</returns>
        [AutoComplete]
        public long m_mthDelRecipe(string ID, string p_status)
        {
            /*
             * 1.�ж��Ƿ��������ƺ����Ĵ���
             * 2.�ǣ����������ƺ����Ĵ���
             * 3.�񣬰���ԭ�ȵĴ���
             * **/

            long lngRes = -4;

            string patientId = string.Empty;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            try
            {
                string strSQL = @"select pstauts_int, isgreen_int, patientid_chr from t_opr_outpatientrecipe where outpatrecipeid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;

                if (p_status.Trim() == "-1")
                {
                    DataTable tempdt = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tempdt, ParamArr);
                    if (lngRes > 0 && tempdt.Rows.Count > 0)
                    {
                        DataRow dtrTmp = tempdt.Rows[0];
                        patientId = dtrTmp["patientid_chr"].ToString();

                        //����������ƺ�����
                        if (dtrTmp["isgreen_int"].ToString().Trim() == "1")
                        {
                            lngRes = m_lngDelRecipeOfFirDiagFinSettl(ID, p_status);
                            if (lngRes < 0)
                            {
                                return lngRes;
                            }
                        }
                        else
                        {
                            if (dtrTmp["pstauts_int"].ToString().Trim() == "2")
                            {
                                return 2;
                            }
                            else
                            {
                                // ��©�ж�
                                lngRes = m_lngDelRecipeOfFirDiagFinSettl(ID, p_status);
                                if (lngRes < 0)
                                {
                                    return lngRes;
                                }
                            }
                        }
                    }
                }


                if (p_status.Trim() == "0")//0����ҽ���½�,��ʱҪɾ������
                {
                    strSQL = @"update T_OPR_OUTPATIENTRECIPE set PSTAUTS_INT = -1 where OUTPATRECIPEID_CHR = '" + ID + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                else
                {
                    strSQL = @"update T_OPR_OUTPATIENTRECIPE set PSTAUTS_INT = " + p_status + " where OUTPATRECIPEID_CHR = '" + ID + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);

                    if (p_status.Trim() == "-1")
                    {
                        //����ʱ��ͨ�����뵥��дΪɾ��״̬
                        /*
                        strSQL = @"update ar_common_apply 
								set deleted = 1 
								where applyid in (
													select attachid_vchr 
													  from t_tmp_outpatienttestrecipede
													 where outpatrecipeid_chr = '" + ID + "')";
                         * */
                        strSQL = @"update ar_common_apply 
								set deleted = 1 
								where applyid in (
													select attachid_vchr 
													  from t_opr_attachrelation
													 where sourceitemid_vchr = '" + ID + "')";

                        lngRes = objHRPSvc.DoExcute(strSQL);

                        //���������뵥��Ϊɾ��״̬
                        strSQL = @"update t_opr_opsapply set status_int = -1 where outpatrecipeid_chr = '" + ID + "'";
                        lngRes = objHRPSvc.DoExcute(strSQL);

                        //����ʱ�Ѽ������뵥���� CS-540 (ID:14514) ҽ�����ϵ�����������ʾ���뵥������ 
                        strSQL = @"update t_opr_lis_application 
								set pstatus_int = -1 
								where application_id_chr in (
													select attachid_vchr 
													  from t_opr_outpatient_orderdic
													 where outpatrecipeid_chr = '" + ID + "')";
                        lngRes = objHRPSvc.DoExcute(strSQL);

                        strSQL = string.Format(@"update eafApplication set status = -2 where patientId = '{0}' and recipeId = '{1}'", patientId, ID);
                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }

                    if (p_status.Trim() == "-1" || p_status.Trim() == "-2")
                    {
                        DataTable dt = new DataTable();

                        strSQL = @"select recipeflag_int, patientid_chr, diagdr_chr, registerid_chr from t_opr_outpatientrecipe where outpatrecipeid_chr = '" + ID + "'";

                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                        if (lngRes > 0)
                        {
                            //����
                            if (dt.Rows[0]["recipeflag_int"].ToString() == "1")
                            {
                                string patientid = dt.Rows[0]["patientid_chr"].ToString();
                                string diagdr = dt.Rows[0]["diagdr_chr"].ToString();

                                if (dt.Rows[0]["registerid_chr"].ToString().Trim() == "")
                                {
                                    strSQL = @"select a.outpatrecipeid_chr
                                                 from t_opr_outpatientrecipe a
                                                where a.pstauts_int = 4 
                                                  and a.recipeflag_int = 2
                                                  and a.patientid_chr = '" + patientid + @"' 
                                                  and a.diagdr_chr = '" + diagdr + @"' 
                                                  and to_char (a.recorddate_dat, 'yyyy-mm-dd') = to_char (sysdate, 'yyyy-mm-dd') 
                                                order by a.outpatrecipeid_chr asc";
                                }
                                else
                                {
                                    int DateInterval = 0;
                                    strSQL = @"select setstatus_int from t_sys_setting where setid_chr = '0058'";
                                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                                    if (lngRes > 0 && dt.Rows.Count == 1)
                                    {
                                        string s = dt.Rows[0][0].ToString().Trim();
                                        if (s != "" && Convert.ToInt32(s) > 0)
                                        {
                                            DateInterval = Convert.ToInt32(s) - 1;
                                        }
                                    }

                                    strSQL = @"select a.outpatrecipeid_chr
                                                 from t_opr_outpatientrecipe a
                                                where a.pstauts_int = 4 
                                                  and a.recipeflag_int = 2
                                                  and a.patientid_chr = '" + patientid + @"' 
                                                  and a.diagdr_chr = '" + diagdr + @"' 
                                                  and (to_char (a.recorddate_dat, 'yyyy-mm-dd')
                                                          between to_char (sysdate - " + DateInterval.ToString() + @", 'yyyy-mm-dd')
                                                              and to_char (sysdate, 'yyyy-mm-dd')
                                                       )             
                                                order by a.outpatrecipeid_chr asc";
                                }

                                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);

                                if (lngRes > 0 && dt.Rows.Count > 0)
                                {
                                    strSQL = @"update t_opr_outpatientrecipe set recipeflag_int = 1 where outpatrecipeid_chr = '" + dt.Rows[0][0].ToString() + "'";

                                    lngRes = objHRPSvc.DoExcute(strSQL);
                                }
                            }
                        }
                    }
                }

                objHRPSvc.Dispose();
                lngRes = long.Parse(p_status);
            }
            catch (Exception objEx)
            {
                lngRes = -4;//����ʧ��
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            return lngRes;

        }

        #region ���������ƺ����Ĵ���
        /// <summary>
        /// ���������ƺ����Ĵ���
        /// ���ؽ����-4 ����ʧ�� -3 ��ʾ�Ѿ���ҩƷ����ҩ����ҩ���������Ѿ���ҩ��ҩ��
        /// -5 ��ʾ�Ѿ��м��鿪ʼ��  -7 ��ʾ�Ѿ��м�鿪ʼ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecipeID">������</param>
        /// <param name="p_strStatus">״̬</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDelRecipeOfFirDiagFinSettl(string p_strRecipeID, string p_strStatus)
        {
            /*
             * 1.�ж����������顢����Ƿ�����
             * 2.�ж�ҩƷ�����Ƿ���ҩ�����߷�ҩ
             * 3.���1��2�����ˣ���ʾ�����˷�
             * **/
            long lngRes = -1;
            long lngAff = -1;


            try
            {
                DataTable dtbTmp = new DataTable();
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] objParam = null;

                #region ҩƷ
                //
                string strSql = @"select a.outpatrecipeid_chr,
                                       decode(b.pstatus_int, 3, 1, 2, 1, 0) as sendMedStatus
                                  from t_opr_recipesendentry a, t_opr_recipesend b
                                 where a.sid_int = b.sid_int
                                   and a.outpatrecipeid_chr = ?";
                objSvc.CreateDatabaseParameter(1, out objParam);
                objParam[0].Value = p_strRecipeID;
                lngRes = objSvc.lngGetDataTableWithParameters(strSql, ref dtbTmp, objParam);

                if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                {
                    int intCountRows = dtbTmp.Rows.Count;

                    for (int i1 = 0; i1 < intCountRows; i1++)
                    {
                        //��ʾ�Ѿ���ҩƷ����ҩ����ҩ���������Ѿ���ҩ��ҩ��
                        if (dtbTmp.Rows[i1]["sendMedStatus"].ToString().Trim() == "1")
                        {
                            return -3;
                        }
                    }
                }

                #endregion

                #region ����
                strSql = string.Empty;
                strSql = @"select a.outpatrecipeid_chr,
                               a.orderdicid_chr,
                               decode(d.status_int, 3, 1, 4, 1, 5, 1, 6, 1, 0) as isfinish
                          from t_opr_outpatient_orderdic a, t_opr_lis_sample d
                         where a.attachid_vchr = d.application_id_chr
                           and d.status_int > 0
                           and a.tableindex_int = 3
                           and a.outpatrecipeid_chr = ?";

                objSvc.CreateDatabaseParameter(1, out objParam);
                objParam[0].Value = p_strRecipeID;
                lngRes = objSvc.lngGetDataTableWithParameters(strSql, ref dtbTmp, objParam);

                if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                {
                    int intCountRows = dtbTmp.Rows.Count;

                    for (int i1 = 0; i1 < intCountRows; i1++)
                    {
                        //��ʾ�Ѿ��м��鿪ʼ��
                        if (dtbTmp.Rows[i1]["isfinish"].ToString().Trim() == "1")
                        {
                            return -5;
                        }
                    }
                }
                #endregion

                #region ���
                //
                strSql = string.Empty;
                strSql = @"select a.outpatrecipeid_chr,
                               a.orderdicid_chr,
                               decode(d.status_int, 2, 1, 1, 1, 0) as isfinish
                          from t_opr_outpatient_orderdic a, ar_common_apply d
                         where a.attachid_vchr = d.applyid
                           and a.tableindex_int = 4      
                           and a.outpatrecipeid_chr = ?";

                objSvc.CreateDatabaseParameter(1, out objParam);
                objParam[0].Value = p_strRecipeID;
                lngRes = objSvc.lngGetDataTableWithParameters(strSql, ref dtbTmp, objParam);

                if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                {
                    int intCountRows = dtbTmp.Rows.Count;

                    for (int i1 = 0; i1 < intCountRows; i1++)
                    {
                        //��ʾ�Ѿ��м�鿪ʼ��
                        if (dtbTmp.Rows[i1]["isfinish"].ToString().Trim() == "1")
                        {
                            return -7;
                        }
                    }
                }
                #endregion

                #region ����
                //�������뵥δ���ύ��
                #endregion

                #region ����ҩƷ��ҩ��
                strSql = string.Empty;
                strSql = @"update t_opr_medrecipesend t
                               set t.pstatus_int = -2
                             where t.outpatrecipeid_chr = ?";
                objSvc.CreateDatabaseParameter(1, out objParam);
                objParam[0].Value = p_strRecipeID;

                lngRes = objSvc.lngExecuteParameterSQL(strSql, ref lngAff, objParam);

                strSql = string.Empty;
                strSql = @"update t_opr_recipesend t
                               set t.pstatus_int = -2
                             where t.sid_int in
                                   (select r.sid_int
                                      from t_opr_recipesendentry r
                                     where r.outpatrecipeid_chr = ?) ";

                objSvc.CreateDatabaseParameter(1, out objParam);
                objParam[0].Value = p_strRecipeID;

                lngRes = objSvc.lngExecuteParameterSQL(strSql, ref lngAff, objParam);
                #endregion
            }
            catch (Exception objEx)
            {
                lngRes = -4;//����ʧ��
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }

            return lngRes;
        }
        #endregion
        #endregion

        #region ȡ������Һŵ�
        /// <summary>
        /// ���ݲ���ID��ҽ��IDȡ������ĹҺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDoctor">ҽ��ID</param>
        /// <param name="strDemp">����ID</param>
        /// <param name="dt">���غ���Һţ���</param>
        [AutoComplete]
        public long m_lngGetGeg(string strDoctor, string strDep, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            if ((strDep + strDoctor) == "")
            {
                return 0;
            }

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strtemp = "";
            if (strDoctor.Trim() != "")
            {
                strtemp = " AND (   a.waitdiagdr_chr = ? ";
                if (strDep.Trim() != "")
                {
                    strtemp += " OR a.waitdiagdept_chr = ?)";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = strDoctor;
                    ParamArr[1].Value = strDep;
                }
                else
                {
                    strtemp += ")";
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = strDoctor;
                }
            }
            else
            {
                strtemp = " AND a.waitdiagdept_chr = ?";
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strDep;
            }

            strtemp += " order by a.order_int";

            string strSQL = @"SELECT a.waitdiaglistid_chr, a.registerid_chr, a.order_int, a.registerop_vchr,
                                   b.patientcardid_chr, c.lastname_vchr, c.sex_chr,
                                   (CASE FLOOR (MONTHS_BETWEEN (SYSDATE, c.birth_dat) / 12) WHEN 0 THEN TO_CHAR(FLOOR (MONTHS_BETWEEN (SYSDATE, c.birth_dat))) || '����' ELSE  TO_CHAR(FLOOR (MONTHS_BETWEEN (SYSDATE, c.birth_dat) / 12)) || '��' END) AS birth_dat,
                                   b.registerno_chr, d.lastname_vchr AS docname, e.deptname_vchr,f.paytypename_vchr
                              FROM t_opr_waitdiaglist a,
                                   t_opr_patientregister b,
                                   t_bse_patient c,
                                   t_bse_employee d,
                                   t_bse_deptdesc e,
                                   t_bse_patientpaytype  f
                             WHERE a.registerid_chr = b.registerid_chr(+)
                               AND b.patientid_chr = c.patientid_chr(+)
                               AND b.diagdept_chr = e.deptid_chr(+)
                               AND b.diagdoctor_chr = d.empid_chr(+)
                               and b.paytypeid_chr =f.paytypeid_chr(+)
                               AND a.registerdate_dat = TO_CHAR (SYSDATE)
                               AND a.pstatus_int = 1" + strtemp;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ����Һ�
        [AutoComplete]
        public long m_lngGetTakeGeg(string strDoctor, string strBeginDate, string strEndDate, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string strSQL = @"SELECT a.takediagrecid_chr, a.registerid_chr,
                                   TO_CHAR (a.takediagtime_dat, 'yyyy-mm-dd hh24:mi') takediagtime_dat,
                                   TO_CHAR (a.endtime_dat, 'yyyy-mm-dd hh24:mi') endtime_dat,
                                   a.pstatus_int, c.lastname_vchr, c.sex_chr,
                                   (CASE FLOOR (MONTHS_BETWEEN (SYSDATE, c.birth_dat) / 12) WHEN 0 THEN TO_CHAR(FLOOR (MONTHS_BETWEEN (SYSDATE, c.birth_dat))) || '����' ELSE TO_CHAR(FLOOR (MONTHS_BETWEEN (SYSDATE, c.birth_dat) / 12)) || '��'  END) AS birth_dat,
                                   '            ' as state, b.patientcardid_chr,e.paytypename_vchr
                              FROM t_opr_takediagrec a, t_bse_patientcard b, t_bse_patient c,t_opr_patientregister D,t_bse_patientpaytype E
                             WHERE a.patientid_chr = c.patientid_chr(+)
                               AND a.patientid_chr = b.patientid_chr(+)
                               and a.registerid_chr =d.registerid_chr(+)
                               and c.paytypeid_chr=e.paytypeid_chr(+)
                               and (to_char(a.takediagtime_dat, 'yyyy-mm-dd') between ? and ?)    
                               and takediagdr_chr = ?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
            ParamArr[0].Value = strBeginDate;
            ParamArr[1].Value = strEndDate;
            ParamArr[2].Value = strDoctor;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewTakeDiagrec(out string p_strRecordID, clsTakeDiagrec p_objRecord)
        {
            long lngRes = 0, lngAffects = 0;
            p_strRecordID = "";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = p_objRecord.m_strREGISTERID_CHR;

            //lngRes = objHRPSvc.m_lngGenerateNewID("T_OPR_TAKEDIAGREC","TAKEDIAGRECID_CHR",out p_strRecordID);
            //if(lngRes < 0)
            //    return lngRes;

            //����ID
            DataTable dt = new DataTable();
            string SQL = "select lpad(seq_takediagrecid.NEXTVAL, 18, '0') from dual";
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
            if (lngRes > 0)
            {
                p_strRecordID = dt.Rows[0][0].ToString();
            }

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "update t_opr_patientregister set PSTATUS_INT = 2 where REGISTERID_CHR = ?";
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
            }
            catch
            {
                return -1;
            }
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = p_objRecord.m_strREGISTERID_CHR.Trim();
            strSQL = @"update t_opr_waitdiaglist
                           set pstatus_int = 2
                         where trim (registerid_chr) = ?";
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
            }
            catch
            {
                return -1;
            }
            if (lngRes < 0) return lngRes;

            //���ҵ����Ǳ�����������Ϊ��0����ʾ����������ı���˳�
            if (p_objRecord.m_strTAKEDIAGDEPT_CHR == "0")
            {
                return lngRes;
            }

            strSQL = @"insert into t_opr_takediagrec
                                    (takediagrecid_chr, registerid_chr, takediagdr_chr,
                                     takediagdept_chr, takediagtime_dat, patientid_chr, paytypeid_chr
                                    )
                             values (?, ?, ?,
                                     ?, ?, ?, ?
                                    )";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strTAKEDIAGDR_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strTAKEDIAGDEPT_CHR;
                objLisAddItemRefArr[4].Value = DateTime.Parse(p_objRecord.m_strTAKEDIAGTIME_DAT);
                objLisAddItemRefArr[5].Value = p_objRecord.m_strPatientID;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPayTypeID;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        #endregion

        #region ���ĹҺŹ���״̬
        [AutoComplete]
        public long m_lngEndTakeReg(string strRegID, string strWaitID)
        {
            long lngRes = 0, lngAffects = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = strRegID;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"update t_opr_patientregister set PSTATUS_INT = 4 where REGISTERID_CHR = ?";

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
            }
            catch
            {

            }
            if (lngRes > 0)
            {
                strSQL = @"update T_OPR_TAKEDIAGREC set PSTATUS_INT=2,ENDTIME_DAT =to_date(?,'yyyy-mm-dd hh24:mi:ss')  where TAKEDIAGRECID_CHR = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = DateTime.Now.ToString();
                ParamArr[1].Value = strWaitID;

                try
                {
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                }
                catch { }
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ĹҺŹ���״̬
        [AutoComplete]
        public long m_mthContinue(string strWaitID, int statues, string strType)
        {
            long lngRes = 0, lngAffects = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string strSQL = @"update T_OPR_TAKEDIAGREC set PSTATUS_INT = ? ";
            if (statues == 2)
            {
                strSQL += ",ENDTIME_DAT =to_date(?,'yyyy-mm-dd hh24:mi:ss')  where " + strType + " = ?";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = statues.ToString();
                ParamArr[1].Value = DateTime.Now.ToString();
                ParamArr[2].Value = strWaitID;

                string temp = @"update t_opr_patientregister set PSTATUS_INT = 4  where RECORDDATE_DAT BETWEEN TO_DATE(?,'yyyy-mm-dd hh24:mi:ss') 
	                                 AND TO_DATE(?,'yyyy-mm-dd hh24:mi:ss') and PATIENTID_CHR = ?";

                IDataParameter[] ParamArr1 = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr1);
                ParamArr1[0].Value = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                ParamArr1[1].Value = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                ParamArr1[2].Value = strWaitID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(temp, ref lngAffects, ParamArr1);
            }
            else
            {
                strSQL += ",ENDTIME_DAT =null  where " + strType + " = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = statues.ToString();
                ParamArr[1].Value = strWaitID;
            }

            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);

            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ĹҺŹ���״̬
        [AutoComplete]
        public long m_lngReturnWait(string strRegID, string strWaitID)
        {
            long lngRes = 0, lngAffects = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = strRegID;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"update t_opr_patientregister set PSTATUS_INT =1 where REGISTERID_CHR = ?";

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = "update T_OPR_WAITDIAGLIST set PSTATUS_INT = 1 where REGISTERID_CHR = ?";

            try
            {
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strRegID;
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
            }
            catch
            {
                return -1;
            }
            if (lngRes > 0)
            {
                strSQL = @"delete T_OPR_TAKEDIAGREC where TAKEDIAGRECID_CHR = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strWaitID;

                try
                {
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                }
                catch (Exception objEx2)
                {
                    string strTmp = objEx2.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx2);
                }
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��鵱ǰ�Һ�״̬
        [AutoComplete]
        public long m_lngGetCurRegF(string RegID)
        {
            long lngRes = 0;
            DataTable dt = new DataTable();
            string strSQL = @"select count(REGISTERID_CHR) from t_opr_patientregister where FLAG_INT<>3 and PSTATUS_INT=1 and REGISTERID_CHR = ?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
            ParamArr[0].Value = RegID;

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch
            {
                //System.Windows.Forms.

            }
            if (lngRes > 0)
            {
                lngRes = long.Parse(dt.Rows[0][0].ToString());
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���˿�������
        /// <summary>
        /// ��ȡ���˿�������
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <returns></returns>
        [AutoComplete]
        public int m_mthGetPatientSeeDocTimes(string strPatientID)
        {

            int timesnum = 0;
            string strSQL = @"select count(takediagdr_chr) as nums
								from t_opr_takediagrec
							   where pstatus_int = 2 								
								 and patientid_chr = ? 
                            group by takediagdr_chr";

            try
            {
                DataTable dt = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strPatientID;

                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    timesnum += int.Parse(dt.Rows[i]["nums"].ToString());
                }
                timesnum += 1;
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return timesnum;
        }
        #endregion

        #region �����÷�
        [AutoComplete]
        public long m_mthGetinjectInfo(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select usageid_chr, type_int, orderid_vchr from t_opr_setusage";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���Ҷ�Ӧ����Ϣ
        [AutoComplete]
        public long m_mthRelationInfo(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select mapid_chr, groupid_chr, catid_chr, internalflag_int from T_BSE_CHARGECATMAP  where INTERNALFLAG_INT=0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������������Ϣ
        [AutoComplete]
        public long m_mthGetRecipeTypeInfo(out clsRecipeType_VO[] objRTVO, string strEx)
        {
            long lngRes = 0;
            objRTVO = null;
            string strSQL = "select type_int, typename_vchr, r_int, g_int, b_int, remark_vchr, medproperty_vchr from T_AID_RECIPETYPE order by TYPE_INT";


            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    objRTVO = new clsRecipeType_VO[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                    {
                        objRTVO[i1] = new clsRecipeType_VO();
                        objRTVO[i1].B_INT = int.Parse(dtResult.Rows[i1]["B_INT"].ToString());
                        objRTVO[i1].G_INT = int.Parse(dtResult.Rows[i1]["G_INT"].ToString());
                        objRTVO[i1].R_INT = int.Parse(dtResult.Rows[i1]["R_INT"].ToString());
                        objRTVO[i1].REMARK_VCHR = dtResult.Rows[i1]["REMARK_VCHR"].ToString().Trim();
                        objRTVO[i1].TYPE_INT = dtResult.Rows[i1]["TYPE_INT"].ToString().Trim();
                        objRTVO[i1].TYPENAME_VCHR = dtResult.Rows[i1]["TYPENAME_VCHR"].ToString().Trim();
                        objRTVO[i1].MedProperty = dtResult.Rows[i1]["medproperty_vchr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ڲ�������ʾ����Ŀ����
        [AutoComplete]
        public long m_mthGetUnDisplayCat(out DataTable dt, string strID)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select seqid_chr,typeid_chr,typename_vchr from T_OPR_OUTPATIENTCASEHISCHR  where SEQID_CHR = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���Ҽ�����
        [AutoComplete]
        public long m_mthGetApplyTypeByID(string strItemID, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"SELECT a.APPLY_TYPE_INT,a.itemchecktype_chr, b.partname
                              FROM t_bse_chargeitem a, AR_APPLY_PARTLIST  b
                             WHERE a.itemchecktype_chr = b.partid(+) and  a.ITEMID_CHR = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strItemID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������ĿID����ԴID.
        [AutoComplete]
        public string m_mthGetResourceIDByItemID(string strItemID)
        {
            string lngRes = "";

            string strSQL = "select ITEMSRCID_VCHR from T_BSE_CHARGEITEM where ITEMID_CHR = ?";
            try
            {
                DataTable dt = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strItemID;

                long l = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count == 1)
                {
                    lngRes = dt.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����������ĿID����ԴID.
        /// <summary>
        /// ����������ĿID����ԴID.
        /// </summary>
        /// <param name="strItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_mthGetResourceIDByOrderDicID(string strItemID)
        {
            string lngRes = "";

            string strSQL = @"select applytypeid_chr 
                                from t_bse_bih_orderdic
                               where orderdicid_chr = ? 
                                 and status_int = 1";
            try
            {
                DataTable dt = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strItemID;

                long l = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count == 1)
                {
                    lngRes = dt.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �ύͬʱ�������뵥ӳ���
        /// <summary>
        /// �ύͬʱ�������뵥ӳ���
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="objArr"></param>
        /// <param name="objOPSarr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthPutIn(string strID, List<clsATTACHRELATION_VO> objArr, List<clsOutops_VO> objOPSarr)
        {
            long lngRes = 0, lngAffects = 0;

            string strSQL = @"select pstauts_int, patientid_chr, diagdr_chr, registerid_chr from t_opr_outpatientrecipe where outpatrecipeid_chr = ?";
            DataTable tempdt = new DataTable();

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = strID;

            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tempdt, ParamArr);
            if (lngRes > 0 && tempdt.Rows.Count > 0)//�ж�����Ѿ��շѴ����ͷ���
            {
                if (tempdt.Rows[0]["pstauts_int"].ToString().Trim() == "2")
                {
                    return 2;
                }
            }
            else
            {
                if (tempdt.Rows.Count == 0)
                {
                    return 9;
                }
            }

            //�Ƿ�Һű�־
            bool blnRegFlag = false;
            string registerId = tempdt.Rows[0]["registerid_chr"].ToString().Trim();
            string doctId = tempdt.Rows[0]["diagdr_chr"].ToString().Trim();

            if (this.m_blnIsAvailRegister(registerId, doctId))
            {
                blnRegFlag = true;
            }

            int ArchTakeFlag = 0;
            int RecipeType = 0;     // 1 ����; ��1 ����
            if (blnRegFlag)
            {
                RecipeType = int.Parse(this.m_strGetRecipeType(registerId, doctId, 1));
                ArchTakeFlag = 1;
            }
            else
            {
                RecipeType = int.Parse(this.m_strGetRecipeType(tempdt.Rows[0]["patientid_chr"].ToString(), doctId, 0));
            }

            #region 2020-03-04 ����.�հ״�����������            
            if (RecipeType != 1)
            {
                strSQL = @"select 1 from t_tmp_outpatientpwmrecipede t where t.outpatrecipeid_chr = ?
                            union all 
                            select 1 from t_tmp_outpatientcmrecipede t where t.outpatrecipeid_chr = ?
                            union all 
                            select 1 from t_tmp_outpatientchkrecipede t where t.outpatrecipeid_chr = ?
                            union all 
                            select 1 from t_tmp_outpatienttestrecipede t where t.outpatrecipeid_chr = ?
                            union all 
                            select 1 from t_tmp_outpatientopsrecipede t where t.outpatrecipeid_chr = ?
                            union all 
                            select 1 from t_tmp_outpatientothrecipede t where t.outpatrecipeid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = strID;
                ParamArr[1].Value = strID;
                ParamArr[2].Value = strID;
                ParamArr[3].Value = strID;
                ParamArr[4].Value = strID;
                ParamArr[5].Value = strID;
                DataTable dtTmp = null;
                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, ParamArr);
                if (dtTmp == null || dtTmp.Rows.Count == 0)
                {
                    weCare.Core.Utils.ExceptionLog.OutPutException("�հ׸���:" + strID + ", �������ύ������");
                    return -3;
                }
            }
            #endregion

            // 2020-09-14 ���ڱ��洦��ʱ
            //string macAddr = string.Empty;
            //strSQL = @"select t.macname_vchr, t.mac_vchr
            //              from t_sys_log t
            //             where t.empid_chr = ?
            //               and (t.logtime_dat between
            //                   to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
            //                   to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
            //             order by t.logtime_dat desc";

            //objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
            //ParamArr[0].Value = doctId;
            //ParamArr[1].Value = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            //ParamArr[2].Value = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            //lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tempdt, ParamArr);
            //if (tempdt != null && tempdt.Rows.Count > 0)//�ж�����Ѿ��շѴ����ͷ���
            //{
            //    macAddr = tempdt.Rows[0]["mac_vchr"].ToString();
            //}

            //strSQL = @"update t_opr_outpatientrecipe set pstauts_int = 4, recipeflag_int = ?, archtakeflag_int = ?, macAddr = ? where outpatrecipeid_chr = ?";

            //objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
            //ParamArr[0].Value = RecipeType;
            //ParamArr[1].Value = ArchTakeFlag;
            //ParamArr[2].Value = macAddr;
            //ParamArr[3].Value = strID;

            strSQL = @"update t_opr_outpatientrecipe set pstauts_int = 4, recipeflag_int = ?, archtakeflag_int = ? where outpatrecipeid_chr = ?";

            objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
            ParamArr[0].Value = RecipeType;
            ParamArr[1].Value = ArchTakeFlag;
            ParamArr[2].Value = strID;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr); //�������뵥��Ϣ��
                if (lngRes > 0 && (objArr.Count > 0 || objOPSarr.Count > 0))
                {
                    DataTable dt = null;
                    foreach (clsATTACHRELATION_VO objVo in objArr)
                    {
                        strSQL = @"select 1 from t_opr_attachrelation where attachid_vchr = ?";
                        objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                        ParamArr[0].Value = objVo.strATTACHID_VCHR;
                        objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            strSQL = @"insert into t_opr_attachrelation
                                          (attarelaid_chr,
                                           sysfrom_int,
                                           attachtype_int,
                                           sourceitemid_vchr,
                                           attachid_vchr,
                                           urgency_int,
                                           status_int,
                                           chargedetail_vchr,
                                           diagnosepart_vchr)
                                        values
                                          (seq_attachrelation.nextval, ?, ?, ?, ?, ?, 0, ?, ?)";

                            objHRPSvc.CreateDatabaseParameter(7, out ParamArr);
                            ParamArr[0].Value = objVo.strSYSFROM_INT;
                            ParamArr[1].Value = objVo.strATTACHTYPE_INT;
                            ParamArr[2].Value = objVo.strSOURCEITEMID_VCHR;
                            ParamArr[3].Value = objVo.strATTACHID_VCHR;
                            ParamArr[4].Value = objVo.strURGENCY_INT;
                            ParamArr[5].Value = objVo.strChargeDetail;
                            ParamArr[6].Value = objVo.strDiagnosePart;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                        }
                    }

                    foreach (clsOutops_VO objops in objOPSarr)
                    {
                        if (objops.recipeid.Trim() == "")
                        {
                            objops.recipeid = strID;
                        }

                        lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_opsapply", "applyid_vchr", out objops.applyid);
                        strSQL = @"insert into t_opr_opsapply(applyid_vchr, outpatrecipeid_chr, itemid_chr, opsdeptid_chr, opsbookingdate_dat, status_int, note_vchr) values(
                                                              ?, ?, ?, ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?)";

                        objHRPSvc.CreateDatabaseParameter(7, out ParamArr);
                        ParamArr[0].Value = objops.applyid;
                        ParamArr[1].Value = objops.recipeid;
                        ParamArr[2].Value = objops.chrgitem;
                        ParamArr[3].Value = objops.deptid;
                        ParamArr[4].Value = objops.bookingdate;
                        ParamArr[5].Value = objops.status;
                        ParamArr[6].Value = objops.note;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffects, ParamArr);
                    }
                }
            }
            catch (Exception objEx)
            {
                lngRes = -4;//����ʧ��
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݲ����Ż�ȡ������Ϣ
        /// <summary>
        /// ���ݲ����Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ID"></param>
        /// <param name="objICD10"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthIllnessInfo(string ID, out clsICD10_VO[] objICD10)
        {
            objICD10 = null;
            long lngRes = 0;
            string strSQL = @" select casehisid_chr,icdcode_vchr,icdname_vchr from t_opr_opch_icd10 where CASEHISID_CHR = ?";
            try
            {
                DataTable dt = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    objICD10 = new clsICD10_VO[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objICD10[i] = new clsICD10_VO();
                        objICD10[i].strCASEHISID_CHR = dt.Rows[i]["CASEHISID_CHR"].ToString().Trim();
                        objICD10[i].strICDCODE_VCHR = dt.Rows[i]["ICDCODE_VCHR"].ToString().Trim();
                        objICD10[i].strICDNAME_VCHR = dt.Rows[i]["ICDNAME_VCHR"].ToString().Trim();
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������������
        [AutoComplete]
        public long m_lngGetLisSampletyType(string strID, string strType, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"SELECT sample_type_id_chr, sample_type_desc_vchr, pycode_chr, wbcode_chr, stdcode1_chr, stdcode2_chr, hasbarcode_int
                              FROM t_aid_lis_sampletype
                             WHERE sample_type_desc_vchr LIKE ?
                                OR pycode_chr LIKE ?
                                OR wbcode_chr LIKE ? 
                                order by sample_type_desc_vchr";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = strID + "%";
                ParamArr[1].Value = strID + "%";
                ParamArr[2].Value = strID + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ���ز�λ
        [AutoComplete]
        public long m_mthLoadCheckPart(out DataTable dt, string strID, string strEx)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"SELECT a.partid, a.partname, a.typeid, a.deleted, a.assistcode_chr, a.pycode_vchr, a.wbcode_vchr
                              FROM ar_apply_partlist a, t_bse_chargeitem b
                             WHERE a.typeid = b.apply_type_int
                               AND b.itemid_chr = ?
                               AND (a.assistcode_chr LIKE ? OR a.PARTNAME LIKE ?)
                               AND a.deleted = 0";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = strID;
                ParamArr[1].Value = strEx + "%";
                ParamArr[2].Value = strEx + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_mthLoadCheckPartOrder(out DataTable dt, string strID, string strEx)
        {
            long lngRes = 0;
            dt = new DataTable();
            string strSQL = @"SELECT a.partid, a.partname, a.typeid, a.deleted, a.assistcode_chr, a.pycode_vchr, a.wbcode_vchr
                              FROM ar_apply_partlist a
                             WHERE a.typeid = ?                            
                               AND (a.assistcode_chr LIKE ? OR a.PARTNAME LIKE ?)
                               AND a.deleted = 0";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = strID;
                ParamArr[1].Value = strEx + "%";
                ParamArr[2].Value = strEx + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݲ���ID������ҩ���
        [AutoComplete]
        public long m_mthGetUsingMedicineByPatientID(out DataTable dt, string strID, string strEx)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"SELECT   *
                                FROM (SELECT a.*, b.itemname_vchr, b.itemcode_vchr, b.itemspec_vchr,b.ITEMOPUNIT_CHR,
                                             c.typename_vchr
                                        FROM (SELECT   a.itemid_chr, SUM (a.tolqty_dec) AS COUNT
                                                  FROM t_opr_outpatientpwmrecipede a,
                                                       t_opr_outpatientrecipe b
                                                 WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                                   AND b.pstauts_int = 2
                                                   AND b.patientid_chr = ? 
                                              GROUP BY a.itemid_chr) a,
                                             t_bse_chargeitem b,
                                             t_bse_chargeitemextype c
                                       WHERE a.itemid_chr = b.itemid_chr(+)
                                         AND b.itemopinvtype_chr = c.typeid_chr(+)
                                         AND c.flag_int = 2
                                      UNION
                                      SELECT a.*, b.itemname_vchr, b.itemcode_vchr, b.itemspec_vchr,b.ITEMOPUNIT_CHR,
                                             c.typename_vchr
                                        FROM (SELECT   a.itemid_chr, SUM (a.qty_dec) AS COUNT
                                                  FROM t_opr_outpatientcmrecipede a,
                                                       t_opr_outpatientrecipe b
                                                 WHERE a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                                   AND b.pstauts_int = 2
                                                   AND b.patientid_chr = ? 
                                              GROUP BY a.itemid_chr) a,
                                             t_bse_chargeitem b,
                                             t_bse_chargeitemextype c
                                       WHERE a.itemid_chr = b.itemid_chr(+)
                                         AND b.itemopinvtype_chr = c.typeid_chr(+)
                                         AND c.flag_int = 2)
                                     ORDER BY itemcode_vchr";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = strID;
                ParamArr[1].Value = strID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ������Ϣ
        [AutoComplete]
        public long m_mthGetChangePriceInfo(string strID, out DataTable dt, string strEx)
        {
            dt = new DataTable();
            long lngRes = 0;

            string strSQL = @"select a.itemid_chr,
                                   a.effect_dat,
                                   to_char(a.preprice_mny) as preprice_mny,
                                   to_char(a.curprice_mny) as curprice_mny,
                                   a.unit_vchr,
                                   a.chargeorderid_chr,
                                   a.seqid_chr,
                                   b.itemcode_vchr,
                                   b.itemname_vchr
                              from t_opr_chargeitempricehis a
                              left join t_bse_chargeitem b
                                on a.itemid_chr = b.itemid_chr
                             where a.itemid_chr = ?
                             order by effect_dat desc";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region ��ȡ�շ���Ŀ����
        /// <summary>
        /// ��ȡ�շ���Ŀ����
        /// </summary>
        /// <param name="p_strChargeItem"></param>
        /// <param name="dtRecord"></param>
        [AutoComplete]
        public long m_lngGetSubChargeItem(string p_strChargeItem, out DataTable dtRecord, bool isChildPrice)
        {
            long lngRes = 0;
            string SQL = @"select a.itemid_chr,
       a.itemname_vchr,
       a.itemcode_vchr,
       a.itempycode_chr,
       a.itemwbcode_chr,
       a.itemsrcid_vchr,
       a.itemsrctype_int,
       a.itemspec_vchr,
       {0},
       a.itemunit_chr,
       a.itemopunit_chr,
       a.itemipunit_chr,
       a.itemopcalctype_chr,
       a.itemipcalctype_chr,
       a.itemopinvtype_chr,
       a.itemipinvtype_chr,
       a.dosage_dec,
       a.dosageunit_chr,
       a.isgroupitem_int,
       a.itemcatid_chr,
       a.usageid_chr,
       a.itemopcode_chr,
       a.insuranceid_chr,
       a.selfdefine_int,
       a.packqty_dec,
       a.tradeprice_mny,
       a.poflag_int,
       a.isrich_int,
       a.opchargeflg_int,
       a.itemsrcname_vchr,
       a.itemsrctypename_vchr,
       a.itemengname_vchr,
       a.ifstop_int,
       a.pdcarea_vchr,
       a.ipchargeflg_int,
       a.insurancetype_vchr,
       a.apply_type_int,
       a.itembihctype_chr,
       a.defaultpart_vchr,
       a.itemchecktype_chr,
       a.itemcommname_vchr,
       a.ordercateid_chr,
       a.freqid_chr,
       a.inpinsurancetype_vchr,
       a.ordercateid1_chr,
       a.isselfpay_chr,
       a.itemprice_mny_old,
       a.itemprice_mny_new,
       a.keepuse_int,
       a.price_temp,
       a.itemspec_vchr1,
       a.lastchange_dat, b.qty_int, b.usageid_chr, h.usagename_vchr, b.freqid_chr as subfreqid_chr, g.freqname_chr, g.times_int, g.days_int fdays,   
								  b.days_int, b.totalqty_dec, c.qty_dec, c.continueusetype_int, d.sample_type_id_chr, 
								  e.sample_type_desc_vchr, f.partname, i.noqtyflag_int, i.deptprep_int, b.usescope_int
							 from t_bse_chargeitem a,
								  (select itemid_chr, subitemid_chr, qty_int, usageid_chr, freqid_chr, days_int, totalqty_dec, usescope_int, continueusetype_int from t_bse_subchargeitem where (itemid_chr = ?)) b,
								  t_bse_chargeitemusagegroup c,
								  t_aid_lis_apply_unit d,
								  t_aid_lis_sampletype e,
								  ar_apply_partlist f,
								  t_aid_recipefreq g,
								  t_bse_usagetype h,
                                  t_bse_medicine i  									 		
							where a.itemid_chr = b.subitemid_chr
							  and a.itemid_chr = c.itemid_chr(+)
							  and a.usageid_chr = c.usageid_chr(+)
							  and a.itemsrcid_vchr = d.apply_unit_id_chr(+)
							  and d.sample_type_id_chr = e.sample_type_id_chr(+)
							  and a.itemchecktype_chr = f.partid(+)
							  and b.freqid_chr = g.freqid_chr(+)
							  and b.usageid_chr = h.usageid_chr(+)
                              and a.itemsrcid_vchr = i.medicineid_chr(+) 
                               ";

            if (isChildPrice)
                SQL = string.Format(SQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                SQL = string.Format(SQL, "a.itemprice_mny");

            dtRecord = new DataTable();
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strChargeItem;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ�Ϊ�շ���Ŀ������Ŀ
        /// <summary>
        /// �ж��Ƿ�Ϊ�շ���Ŀ������Ŀ
        /// </summary>
        /// <param name="strSubChrgItem"></param>
        /// <param name="strChrgItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnIsSubChrgItem(string strSubChrgItem, string strChrgItem)
        {
            long lngRes = 0;
            bool blnRet = false;
            string SQL = @"select count(itemid_chr) nums from t_bse_subchargeitem where subitemid_chr = ? and itemid_chr = ?";
            DataTable dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = strSubChrgItem;
                ParamArr[1].Value = strChrgItem;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows[0][0].ToString() != "0")
                {
                    blnRet = true;
                }
            }
            return blnRet;
        }
        #endregion

        #region ���ݲ����Ų�����Ч������
        /// <summary>
        /// ���ݲ����Ų�����Ч������
        /// </summary>
        /// <param name="strCaseID"></param>
        /// <param name="dtRecord"></param>
        [AutoComplete]
        public long m_lngGetRecipeIDByCaseID(string strCaseID, out DataTable dtRecord)
        {
            long lngRes = 0;
            string SQL = @"select outpatrecipeid_chr, pstauts_int
							 from t_opr_outpatientrecipe
							where pstauts_int >= 0 
							  and casehisid_chr = ?";
            dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strCaseID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݹҺ�����ID�ж��Ƿ���
        /// <summary>
        /// ���ݹҺ�����ID�ж��Ƿ���
        /// </summary>
        /// <param name="strRegTypeID"></param>
        /// <returns></returns>		
        [AutoComplete]
        public bool m_blnCheckRegiterType(string strRegTypeID)
        {
            long lngRes = 0;
            bool blnRet = false;
            string SQL = @"select count(registertypeid_chr) nums from t_bse_registertype where urgency_int = 1 and registertypeid_chr = ?";
            DataTable dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = strRegTypeID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows[0][0].ToString() != "0")
                {
                    blnRet = true;
                }
            }
            return blnRet;
        }
        #endregion

        #region ���ݴ������жϸô����Ƿ����շ�
        /// <summary>
        /// ���ݴ������жϸô����Ƿ����շ�
        /// </summary>
        /// <param name="strRecID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckRecipeChrg(string strRecID)
        {
            long lngRes = 0;
            bool blnRet = false;
            string SQL = @"select count(outpatrecipeid_chr) nums from t_opr_outpatientrecipe where pstauts_int = 2 and outpatrecipeid_chr = ?";
            DataTable dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strRecID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows[0][0].ToString() != "0")
                {
                    blnRet = true;
                }
            }
            return blnRet;
        }

        #endregion

        #region ��ȡҽ������վ��״̬����
        /// <summary>
        /// ��ȡҽ������վ��״̬����
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWSParm(string strType, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"select setid_chr, setname_vchr, setdesc_vchr, setstatus_int, moduleid_chr from T_SYS_SETTING where SETID_CHR = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = strType;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������Ա�б�
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllergiclist(out DataTable dtRecord, string DoctorID, string Status)
        {
            long lngRes = 0;
            string SQL = @"select a.patientid_chr, a.createempid_chr, a.outpatrecipeid_chr, a.create_dat, a.allergicmed_vchr, a.allergicdesc_vchr, a.status_int, d.patientcardid_chr, c.name_vchr, c.sex_chr, c.birth_dat
							 from t_opr_allergic a,
								  t_opr_outpatientrecipe b,
								  t_bse_patient c,
								  t_bse_patientcard d
						    where (a.patientid_chr = c.patientid_chr)
							  and (a.patientid_chr = d.patientid_chr) 
							  and (a.outpatrecipeid_chr = b.outpatrecipeid_chr)
							  and (d.status_int <> 0) 
							  and (to_char(a.create_dat,'yyyy-mm-dd') = to_char(sysdate,'yyyy-mm-dd'))
							  and (b.diagdr_chr = ?) 
							  and (a.status_int like ?)
							order by a.create_dat";

            dtRecord = null;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = DoctorID;
                ParamArr[1].Value = Status;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ȷ�ϸ��¹�����Ϣ
        /// <summary>
        /// ȷ�ϸ��¹�����Ϣ 
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="RecipeID"></param>
        /// <param name="AllergicMed"></param>
        /// <param name="AllergicDesc"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateallergic(string PatientID, string RecipeID, string AllergicMed, string AllergicDesc)
        {
            string SQL = "";
            long lngRes = 0, lngAffects = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                IDataParameter[] ParamArr = null;

                SQL = @"update t_opr_allergic
							set	allergicmed_vchr = ?,
								allergicdesc_vchr = ?,
								status_int = 1 
						where patientid_chr = ? 
						  and outpatrecipeid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = AllergicMed;
                ParamArr[1].Value = AllergicDesc;
                ParamArr[2].Value = PatientID;
                ParamArr[3].Value = RecipeID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"update t_bse_patient 
							set ifallergic_int = 1,
								allergicdesc_vchr = allergicdesc_vchr || ? || ? 
						where patientid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = AllergicMed;
                ParamArr[1].Value = AllergicDesc;
                ParamArr[2].Value = PatientID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"update t_opr_outpatientcasehis
							set anaphylaxis_vchr = anaphylaxis_vchr || ? || ? 
						where casehisid_chr in (select casehisid_chr from t_opr_outpatientrecipe where outpatrecipeid_chr = ?)";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = AllergicMed;
                ParamArr[1].Value = AllergicDesc;
                ParamArr[2].Value = RecipeID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݿ��ҡ�ԤԼʱ���ȡ�������������¼
        /// <summary>
        /// ���ݿ��ҡ�ԤԼʱ���ȡ�������������¼
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="bookingdate"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPSApply(out DataTable dtRecord, string bookingdate, string deptid, int flag, int ischrg)
        {
            dtRecord = new DataTable();
            long lngRes = 0;

            string SQL = @"select to_number(a.applyid_vchr) as applyid_vchr, a.outpatrecipeid_chr, a.itemid_chr, a.opsdeptid_chr, 
                                  a.opsbookingdate_dat, a.confirmemp_chr, a.confirmdate_dat, a.status_int, a.note_vchr,
                                  b.name_vchr, b.sex_chr, b.birth_dat,
                                  d.itemname_vchr, e.deptname_vchr, f.patientcardid_chr,
                                  g.lastname_vchr, c.recorddate_dat, h.applyid_vchr as repflag,c.pstauts_int
                             from t_opr_opsapply a,
                                  t_bse_patient b,
                                  t_opr_outpatientrecipe c,
                                  t_bse_chargeitem d,
                                  t_bse_deptdesc e,
                                  t_bse_patientcard f,
                                  t_bse_employee g,
                                  t_opr_opsrecord h
                            where a.outpatrecipeid_chr = c.outpatrecipeid_chr
                              and c.patientid_chr = b.patientid_chr
                              and a.itemid_chr = d.itemid_chr(+)
                              and a.opsdeptid_chr = e.deptid_chr(+)
                              and c.patientid_chr = f.patientid_chr(+)
                              and c.diagdr_chr = g.empid_chr(+)
                              and (f.status_int = 1 or f.status_int = 3)
                              and a.applyid_vchr = h.applyid_vchr(+)";
            if (flag == 0)
            {
                SQL += " and a.opsdeptid_chr = '" + deptid + "' and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd hh24:mm') <= '" + bookingdate + "'";
            }
            else if (flag == 1)
            {
                if (ischrg == 1)
                {
                    SQL += " and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
                }
                else
                {
                    //SQL += " and c.pstauts_int = 2 and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
                    SQL += "  and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
                }
            }
            else if (flag == 2)
            {
                SQL += " and a.status_int = 1 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
            }
            SQL += " order by a.opsbookingdate_dat ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// ���ݿ��ҡ�ԤԼʱ���ȡ�������������¼
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="bookingdate"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPSApply(out DataTable dtRecord, string bookingdate, string deptid, int flag, int ischrg, bool p_blnPstauts_int_2)
        {
            dtRecord = new DataTable();
            long lngRes = 0;

            string SQL = @"select to_number(a.applyid_vchr) as applyid_vchr, a.outpatrecipeid_chr, a.itemid_chr, a.opsdeptid_chr, 
                                  a.opsbookingdate_dat, a.confirmemp_chr, a.confirmdate_dat, a.status_int, a.note_vchr,
                                  b.name_vchr, b.sex_chr, b.birth_dat,
                                  d.itemname_vchr, e.deptname_vchr, f.patientcardid_chr,
                                  g.lastname_vchr, c.recorddate_dat, h.applyid_vchr as repflag
                             from t_opr_opsapply a,
                                  t_bse_patient b,
                                  t_opr_outpatientrecipe c,
                                  t_bse_chargeitem d,
                                  t_bse_deptdesc e,
                                  t_bse_patientcard f,
                                  t_bse_employee g,
                                  t_opr_opsrecord h
                            where a.outpatrecipeid_chr = c.outpatrecipeid_chr
                              and c.patientid_chr = b.patientid_chr
                              and a.itemid_chr = d.itemid_chr(+)
                              and a.opsdeptid_chr = e.deptid_chr(+)
                              and c.patientid_chr = f.patientid_chr(+)
                              and c.diagdr_chr = g.empid_chr(+)
                              and (f.status_int = 1 or f.status_int = 3)
                              and a.applyid_vchr = h.applyid_vchr(+)";
            if (p_blnPstauts_int_2)//����ʾ��Ʊ��
            {
                SQL += " and c.pstauts_int != -2";
            }

            if (flag == 0)
            {
                SQL += " and a.opsdeptid_chr = '" + deptid + "' and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd hh24:mm') <= '" + bookingdate + "'";
            }
            else if (flag == 1)
            {
                if (ischrg == 1)
                {
                    SQL += " and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
                }
                else
                {
                    SQL += " and c.pstauts_int = 2 and a.status_int = 0 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
                }
            }
            else if (flag == 2)
            {
                SQL += " and a.status_int = 1 and to_char(a.opsbookingdate_dat, 'yyyy-mm-dd') = to_char(sysdate, 'yyyy-mm-dd')";
            }
            SQL += " order by a.opsbookingdate_dat ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������뵥�Ż�ȡ�������������¼
        /// <summary>
        /// �������뵥�Ż�ȡ�������������¼
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="applyid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPSApply(out DataTable dtRecord, string applyid)
        {
            dtRecord = new DataTable();
            long lngRes = 0;

            string SQL = @"select to_number(a.applyid_vchr) as applyid_vchr, a.outpatrecipeid_chr, a.itemid_chr, a.opsdeptid_chr, 
                                  a.opsbookingdate_dat, a.confirmemp_chr, a.confirmdate_dat, a.status_int, a.note_vchr,
                                  b.name_vchr, b.sex_chr, b.birth_dat,
                                  d.itemname_vchr, e.deptname_vchr, f.patientcardid_chr,
                                  g.lastname_vchr, c.recorddate_dat
                             from t_opr_opsapply a,
                                  t_bse_patient b,
                                  t_opr_outpatientrecipe c,
                                  t_bse_chargeitem d,
                                  t_bse_deptdesc e,
                                  t_bse_patientcard f,
                                  t_bse_employee g
                            where a.outpatrecipeid_chr = c.outpatrecipeid_chr
                              and c.patientid_chr = b.patientid_chr
                              and a.itemid_chr = d.itemid_chr(+)
                              and a.opsdeptid_chr = e.deptid_chr(+)
                              and c.patientid_chr = f.patientid_chr(+)
                              and c.diagdr_chr = g.empid_chr(+)
                              and (f.status_int = 1 or f.status_int = 3)
                              and to_number(a.applyid_vchr) = " + applyid;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ȷ�������������뵥
        /// <summary>
        /// ȷ�������������뵥
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfrimOPS(string applyid, string empid)
        {
            string SQL = "";
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                SQL = @"update t_opr_opsapply
                            set status_int = 1,
                                confirmemp_chr = '" + empid + @"',
                                confirmdate_dat = sysdate
                        where to_number(applyid_vchr) = " + applyid;

                lngRes = objHRPSvc.DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        /// <summary>
        /// ȷ�������������浥
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfrimOPSReport(string applyid, string empid)
        {
            string SQL = "";
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                SQL = @"update t_opr_opsrecord
                            set status_int = 1,
                                confirmemp_chr = '" + empid + @"',
                                confirmdate_dat = sysdate
                        where to_number(applyid_vchr) = " + applyid;

                lngRes = objHRPSvc.DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���ؿ���ID�Ϳ�����������
        /// <summary>
        /// ���ؿ���ID�Ϳ�����������
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptArr"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptAreaByLike(string p_strLike, out DataTable dtResult, string p_strDeptID)
        {
            dtResult = m_dtbGetDept(p_strLike, p_strDeptID);
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                return 0;
            }

            //p_strDeptArr = new string[dtResult.Rows.Count, 2];

            //for (int i = 0; i < dtResult.Rows.Count; i++)
            //{
            //    p_strDeptArr[i, 0] = dtResult.Rows[i]["DEPTID_CHR"].ToString().Trim();
            //    p_strDeptArr[i, 1] = dtResult.Rows[i]["DEPTNAME_VCHR"].ToString().Trim();
            //}

            return 1;
        }
        #endregion

        #region ȡ�ÿ���
        /// <summary>
        /// ȡ�ÿ���
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        private DataTable m_dtbGetDept(string p_strLike, string p_strDeptID)
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            try
            {
                if (p_strLike == null)
                {
                    return null;
                }

                string strSql = "select t.deptid_chr, t.modify_dat, t.deptname_vchr, t.category_int, t.inpatientoroutpatient_int, t.operatorid_chr, t.address_vchr, t.pycode_chr, t.attributeid, t.parentid, t.createdate_dat, t.status_int, t.deactivate_dat, t.wbcode_chr, t.code_vchr, t.extendid_vchr, t.shortno_chr, t.stdbed_count_int, t.putmed_int, t.usercode_vchr, t.expertdeptflag_int from t_bse_deptdesc t ";

                clsHRPTableService objHRPServer = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (p_strLike.Trim() != "")
                {
                    strSql += @" where (t.deptid_chr like ? or t.deptname_vchr like ?  
								 or (t.pycode_chr like ?) or (t.wbcode_chr like ?)
								 or t.shortno_chr like ?) and t.status_int = '1' and t.category_int = '0'";

                    objHRPServer.CreateDatabaseParameter(5, out ParamArr);
                    ParamArr[0].Value = p_strLike.Trim() + "%";
                    ParamArr[1].Value = "%" + p_strLike.Trim() + "%";
                    ParamArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                    ParamArr[3].Value = p_strLike.Trim().ToUpper() + "%";
                    ParamArr[4].Value = p_strLike.Trim() + "%";
                }
                else
                {
                    strSql += @" where t.status_int = '1' and t.category_int = '0'";
                }

                if (p_strDeptID != null)
                {
                    if (p_strDeptID.Trim() != "")
                    {
                        strSql += @" and t.attributeid = '0000003' and t.parentid = ?";

                        if (ParamArr == null)
                        {
                            objHRPServer.CreateDatabaseParameter(1, out ParamArr);
                            ParamArr[1].Value = p_strDeptID.Trim();
                        }
                        else
                        {
                            objHRPServer.CreateDatabaseParameter(6, out ParamArr);
                            ParamArr[0].Value = p_strLike.Trim() + "%";
                            ParamArr[1].Value = "%" + p_strLike.Trim() + "%";
                            ParamArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                            ParamArr[3].Value = p_strLike.Trim().ToUpper() + "%";
                            ParamArr[4].Value = p_strLike.Trim() + "%";
                            ParamArr[5].Value = p_strDeptID.Trim();
                        }
                    }
                }

                strSql += @"order by deptname_vchr";

                long lngRes = 0;

                if (ParamArr == null)
                {
                    lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
                }
                else
                {
                    lngRes = objHRPServer.lngGetDataTableWithParameters(strSql, ref dtResult, ParamArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return dtResult;
        }
        #endregion

        #region ���ݲ���Ա���Ż�ȡID������������
        /// <summary>
        /// ���ݲ���Ա���Ż�ȡID������������
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="empno"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetempinfo(out DataTable dtRecord, string empno)
        {
            long lngRes = 0;
            dtRecord = new DataTable();

            string SQL = @"select empid_chr, lastname_vchr, psw_chr from t_bse_employee where status_int = 1 and empno_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = empno;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��������������¼��Ϣ
        /// <summary>
        /// ��������������¼��Ϣ
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="Ops"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveOPS(string applyid, clsOutops_VO objops)
        {
            string SQL = "";
            long lngRes = 0;
            DataTable dt = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                SQL = "select applyid_vchr from t_opr_opsapply where to_number(applyid_vchr) = " + applyid;

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (dt.Rows.Count == 1)
                {
                    applyid = dt.Rows[0][0].ToString();
                }

                SQL = "delete from t_opr_opsrecord where applyid_vchr = '" + applyid + "'";

                lngRes = objHRPSvc.DoExcute(SQL);

                SQL = @"insert into t_opr_opsrecord(applyid_vchr, opsname_vchr, opsdate_dat, prediagnoses_vchr, enddiagnoses_vchr,
                                                    opsdoctor_chr, opsassistant1_chr, opsappliance_chr, opsanamode_chr, anaemp1_chr,
                                                    opsresult_vchr, signdoctor_chr, signdate_dat) values('" +
                                                applyid + "', '" +
                                                objops.opsname + "', to_date('" +
                                                objops.opsdate + "', 'yyyy-mm-dd hh24:mi:ss'), '" +
                                                objops.prediagnoses + "', '" +
                                                objops.enddiagnoses + "', '" +
                                                objops.opsdoctor + "', '" +
                                                objops.opsassistant1 + "', '" +
                                                objops.opsappliance + "', '" +
                                                objops.opsanamode + "', '" +
                                                objops.anaempid1 + "', '" +
                                                objops.opsresult + "', '" +
                                                objops.signdoctor + "', to_date('" +
                                                objops.signdate + "', 'yyyy-mm-dd hh24:mi:ss'))";

                lngRes = objHRPSvc.DoExcute(SQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������뵥�Ż�ȡ�������浥��Ϣ
        /// <summary>
        /// �������뵥�Ż�ȡ�������浥��Ϣ
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetopsrecord(string applyid, out DataTable dtRecord)
        {
            long lngRes = 0;
            dtRecord = new DataTable();

            string SQL = @"select applyid_vchr, opsname_vchr, opsdate_dat, prediagnoses_vchr, enddiagnoses_vchr, opsdoctor_chr, opsassistant1_chr, opsassistant2_chr, opsassistant3_chr, opsappliance_chr, opsanamode_chr, anaemp1_chr, anaemp2_chr, opsresult_vchr, signdoctor_chr, signdate_dat, note_vchr, confirmemp_chr, confirmdate_dat, status_int from t_opr_opsrecord where to_number(applyid_vchr) = " + applyid;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ�������δͨ���ļ�¼
        /// <summary>
        /// ��ȡ�������δͨ���ļ�¼
        /// </summary>
        /// <param name="DoctorID"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetrecipeconfirmfall(string DoctorID, out DataTable dtRecord)
        {
            long lngRes = 0;
            string SQL = @"select a.outpatrecipeid_chr,
       a.patientid_chr,
       a.createdate_dat,
       a.registerid_chr,
       a.diagdr_chr,
       a.diagdept_chr,
       a.recordemp_chr,
       a.recorddate_dat,
       a.pstauts_int,
       a.recipeflag_int,
       a.outpatrecipeno_vchr,
       a.paytypeid_chr,
       a.casehisid_chr,
       a.groupid_chr,
       a.type_int,
       a.confirm_int,
       a.confirmdesc_vchr,
       a.createtype_int,
       a.deptmed_int,
       a.archtakeflag_int,
       a.printed_int,
       a.chargedeptid_chr, c.patientcardid_chr, b.name_vchr, b.sex_chr, b.birth_dat 
                            from t_opr_outpatientrecipe a,
                                 t_bse_patient b,
                                 t_bse_patientcard c
                            where (a.patientid_chr = b.patientid_chr) 
                              and (a.patientid_chr = c.patientid_chr)
                              and (c.status_int <> 0) 
                              and (a.confirm_int = -1)
                              and (to_char(a.createdate_dat,'yyyy-mm-dd') = to_char(sysdate,'yyyy-mm-dd'))
                              and (a.diagdr_chr = ?) 
                            order by a.outpatrecipeid_chr";

            dtRecord = null;

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = DoctorID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ�����������浥��Ϣ
        /// <summary>
        /// ��ȡ�����������浥��Ϣ
        /// </summary>
        /// <param name="SQLSelect"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetopsreports(string SQLSelect, out DataTable dtRecord)
        {
            long lngRes = 0;
            dtRecord = null;

            string SQL = @"select to_number(a.applyid_vchr) as applyid_vchr, b.name_vchr, b.sex_chr, b.birth_dat,
                                  e.deptname_vchr, f.patientcardid_chr, h.opsname_vchr,
                                  h.opsdate_dat, h.opsdoctor_chr, h.opsresult_vchr
                             from t_opr_opsapply a,
                                  t_bse_patient b,
                                  t_opr_outpatientrecipe c,
                                  t_bse_chargeitem d,
                                  t_bse_deptdesc e,
                                  t_bse_patientcard f,
                                  t_bse_employee g,
                                  t_opr_opsrecord h
                            where a.outpatrecipeid_chr = c.outpatrecipeid_chr
                              and c.patientid_chr = b.patientid_chr
                              and a.itemid_chr = d.itemid_chr(+)
                              and a.opsdeptid_chr = e.deptid_chr(+)
                              and c.patientid_chr = f.patientid_chr(+)
                              and c.diagdr_chr = g.empid_chr(+)
                              and (f.status_int = 1 or f.status_int = 3)
                              and a.applyid_vchr = h.applyid_vchr";

            SQL += SQLSelect;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ����ʽ
        /// <summary>
        /// ��ȡ����ʽ
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetanaesthesiamode(out DataTable dtRecord)
        {
            long lngRes = 0;
            dtRecord = new DataTable();

            string SQL = @"select anaesthesiamodeid,
       anaesthesiamodename,
       iftechnology,
       status,
       deaactiveddate,
       operationid
  from anaesthesiamode ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ģ������Ա��������Ա��ID��Ա����������
        /// <summary>
        /// ģ������Ա��������Ա��ID��Ա����������
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByLike(string p_strLike, out DataTable dtName)
        {
            long lngRes = 0;
            dtName = null;

            try
            {
                if (p_strLike == null)
                {
                    return -1;
                }

                string strSql = @"select distinct t.empno_chr,t.lastname_vchr from t_bse_employee t inner join t_bse_deptemp a
									on t.empid_chr = a.empid_chr inner join t_bse_deptdesc b on a.deptid_chr = b.deptid_chr where (
									t.empno_chr like ? or t.lastname_vchr like ? or t.pycode_chr like ? 
									or t.shortname_chr like ?) and t.status_int = '1' order by t.lastname_vchr";

                DataTable dtResult = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = p_strLike.Trim() + "%";
                ParamArr[1].Value = "%" + p_strLike.Trim() + "%";
                ParamArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                ParamArr[3].Value = p_strLike.Trim() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, ParamArr);

                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                {
                    return 0;
                }
                dtName = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["EMPNO_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //}
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        /// <summary>
        /// ģ������Ա��������Ա��ID��Ա����������
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByLikeNew(string p_strLike, out DataTable dtName)
        {
            long lngRes = 0;
            dtName = null;

            try
            {
                if (p_strLike == null)
                {
                    return -1;
                }

                string strSql = @"select distinct t.EMPID_CHR ,t.empno_chr,t.lastname_vchr from t_bse_employee t inner join t_bse_deptemp a
									on t.empid_chr = a.empid_chr inner join t_bse_deptdesc b on a.deptid_chr = b.deptid_chr where (
									t.empno_chr like ? or t.lastname_vchr like ? or t.pycode_chr like ? 
									or t.shortname_chr like ?) and t.status_int = '1' order by t.empno_chr";

                DataTable dtResult = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = p_strLike.Trim() + "%";
                ParamArr[1].Value = "%" + p_strLike.Trim() + "%";
                ParamArr[2].Value = p_strLike.Trim().ToUpper() + "%";
                ParamArr[3].Value = p_strLike.Trim() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtResult, ParamArr);

                if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                {
                    return 0;
                }
                dtName = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 3];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["EMPNO_CHR"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["LASTNAME_VCHR"].ToString().Trim();
                //    p_strNameArr[i, 2] = dtResult.Rows[i]["EMPID_CHR"].ToString().Trim();
                //}
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        /// <summary>
        /// ģ������Ա��������Ա��ID��Ա����������
        /// </summary>
        /// <param name="p_strLike"></param>
        /// <param name="p_strNameArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployeeNameByID(string p_strLike, out DataTable dtName)
        {
            long lngRes = -1;
            dtName = null;

            try
            {
                if (p_strLike == null)
                {
                    return -1;
                }

                string strSql = @"SELECT empid_chr ,lastname_vchr
                                            FROM  t_bse_employee  ";
                strSql += " where " + p_strLike;
                DataTable dtResult = new DataTable();
                clsHRPTableService objHRPServer = new clsHRPTableService();
                lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
                if (lngRes <= 0)
                {
                    return -1;
                }
                else
                {
                    lngRes = 1;
                }
                if (dtResult.Rows.Count <= 0)
                {
                    return 0;
                }
                dtName = dtResult;
                //p_strNameArr = new string[dtResult.Rows.Count, 2];
                //for (int i = 0; i < dtResult.Rows.Count; i++)
                //{
                //    p_strNameArr[i, 0] = dtResult.Rows[i]["empid_chr"].ToString().Trim();
                //    p_strNameArr[i, 1] = dtResult.Rows[i]["lastname_vchr"].ToString().Trim();
                //}
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region �����շ�ID�ж��Ƿ�������������
        /// <summary>
        /// �����շ�ID�ж��Ƿ�������������
        /// </summary>
        /// <param name="chrgitemcode"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnChkopsitem(string chrgitemcode)
        {
            long lngRes = 0;
            bool blnRet = false;

            string SQL = @"select count(itemid_chr) as nums 
                             from t_bse_chargeitem 
                            where (usageid_chr in (select usageid_chr from t_bse_usagetype where trim(usagename_vchr) = '����'))
                              and itemid_chr = ?";
            DataTable dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = chrgitemcode;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows[0][0].ToString() != "0")
                {
                    blnRet = true;
                }
            }
            return blnRet;
        }
        #endregion

        #region ����������ȡ�������뵥��Ϣ.

        /// <summary>
        /// ����������ȡ�������뵥��Ϣ
        /// </summary>
        /// <param name="p_strOr"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplyOPInfoByOrCondition(string p_strOr, out DataTable dtRecord)
        {//״̬ -1 ɾ����0 �½���1 ��ȷ�ϣ�2 �˻�
            long lngRes = 0;
            dtRecord = new DataTable();
            string SQL = @"SELECT   TO_NUMBER (a.applyid_vchr) AS applyid_vchr, b.name_vchr, rtrim(b.sex_chr) as sex_chr,
                                                         b.birth_dat,  f.patientcardid_chr,a.opsbookingdate_dat,d.ITEMNAME_VCHR as OPSNAME_VCHR
                                                        , CASE 
                                                         WHEN a.status_int = -1 then 'ɾ��'
                                                         WHEN a.status_int =0   then '�½�'
                                                         WHEN a.status_int = 1  then '��ȷ��'
                                                         ELSE '�˻�' 
                                                        END status_int
                            FROM t_opr_opsapply a,
                                 t_bse_patient b,
                                 t_opr_outpatientrecipe c,
                                 t_bse_chargeitem d,
                                 t_bse_deptdesc e,
                                 t_bse_patientcard f,
                                 t_bse_employee g,
                                 t_opr_opsrecord h
                           WHERE a.outpatrecipeid_chr = c.outpatrecipeid_chr
                             AND c.patientid_chr = b.patientid_chr
                             AND a.itemid_chr = d.itemid_chr(+)
                             AND a.opsdeptid_chr = e.deptid_chr(+)
                             AND c.patientid_chr = f.patientid_chr(+)
                             AND c.diagdr_chr = g.empid_chr(+)
                             AND (f.status_int = 1 OR f.status_int = 3)
                             AND a.applyid_vchr = h.applyid_vchr(+) ";
            if (p_strOr != "")
            {
                SQL += p_strOr;
            }
            SQL += " ORDER BY applyid_vchr ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region �����������뵥��ϢԤԼʱ��
        /// <summary>
        /// �����������뵥��ϢԤԼʱ��
        /// </summary>
        /// <param name="p_strAPPLYID_VCHR"></param>
        /// <param name="p_strOPSBOOKINGDATE_DAT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplyOPInfoUpdateDate(string p_strAPPLYID_VCHR, string p_strOPSBOOKINGDATE_DAT)
        {
            long lngRes = 0;
            string SQL = "update t_opr_opsapply set OPSBOOKINGDATE_DAT=to_date('" + p_strOPSBOOKINGDATE_DAT + "','yyyy-MM-dd HH24:mi:ss') where APPLYID_VCHR='" + p_strAPPLYID_VCHR + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(SQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ���ɻ�����ݶ�Ӧ�ű�
        /// <summary>
        /// ���ɻ�����ݶ�Ӧ�ű�
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="paytypeid"></param>
        /// <param name="idno"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGenpatientidentityno(string pid, string paytypeid, string idno)
        {
            string SQL = "";
            long lngRes = 0, lngAffects = 0;

            // 2020-03-26
            if (string.IsNullOrEmpty(pid))
            {
                return 1;
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                DataTable dt = null;

                SQL = @"select 1 from t_bse_patientidentityno where patientid_chr = ? and paytypeid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = pid;
                ParamArr[1].Value = paytypeid;
                objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SQL = @"update t_bse_patientidentityno set idno_vchr = ? 
                            where patientid_chr = ? and paytypeid_chr = ? ";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = idno;
                    ParamArr[1].Value = pid;
                    ParamArr[2].Value = paytypeid;
                }
                else
                {
                    SQL = @"insert into t_bse_patientidentityno(patientid_chr, paytypeid_chr, idno_vchr)
                                                    values (?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = pid;
                    ParamArr[1].Value = paytypeid;
                    ParamArr[2].Value = idno;

                }
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //SQL = "delete from t_bse_patientidentityno where patientid_chr = ? and paytypeid_chr = ?";

                //objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                //ParamArr[0].Value = pid;
                //ParamArr[1].Value = paytypeid;

                //lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݻ���ID�������ID��ȡ�������Ӧ��
        /// <summary>
        /// ���ݻ���ID�������ID��ȡ�������Ӧ��
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="paytypeid"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetpatientidentityno(string pid, string paytypeid)
        {
            long lngRes = 0;
            string idno = "";
            string SQL = "select idno_vchr from t_bse_patientidentityno where patientid_chr = ? and paytypeid_chr = ?";
            DataTable dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = pid;
                ParamArr[1].Value = paytypeid;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows.Count == 1)
                {
                    idno = dtRecord.Rows[0][0].ToString().Trim();
                }
            }

            return idno;
        }
        #endregion

        #region ���ݷ�Ʊ�Ż�ȡ�������Ӧ��
        /// <summary>
        /// ���ݷ�Ʊ�Ż�ȡ�������Ӧ��
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="paytypeid"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetpatientidentityno(string invo)
        {
            long lngRes = 0;
            string idno = "";
            string SQL = @"select a.idno_vchr 
                             from t_bse_patientidentityno a,
                                  t_opr_outpatientrecipeinv b
                            where a.patientid_chr = b.patientid_chr
                              and a.paytypeid_chr = b.paytypeid_chr
                              and b.invoiceno_vchr = ?";

            DataTable dtRecord = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = invo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows.Count == 1)
                {
                    idno = dtRecord.Rows[0][0].ToString().Trim();
                }
            }

            return idno;
        }
        #endregion

        #region ���ݲ���ԱID��ȡ������ҩ������Ȩ��
        /// <summary>
        /// ���ݲ���ԱID��ȡ������ҩ������Ȩ��
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="neurpur"></param>
        /// <param name="drugpur"></param>
        /// <returns></returns>
        [AutoComplete]
        public void m_mthGetmedpurview(string empid, out string neurpur, out string drugpur, out string recpur)
        {
            neurpur = "";
            drugpur = "";
            recpur = "";
            string SQL = @"select HASPSYCHOSISPRESCRIPTIONRIGHT_, HASOPIATEPRESCRIPTIONRIGHT_CHR, HASPRESCRIPTIONRIGHT_CHR from t_bse_employee where empid_chr = ?";
            DataTable dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = empid;

                long lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count == 1)
                {
                    neurpur = dt.Rows[0]["HASPSYCHOSISPRESCRIPTIONRIGHT_"].ToString();
                    drugpur = dt.Rows[0]["HASOPIATEPRESCRIPTIONRIGHT_CHR"].ToString();
                    recpur = dt.Rows[0]["HASPRESCRIPTIONRIGHT_CHR"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #region �����շ���Ŀ�ж��Ƿ�ΪƬ��
        /// <summary>
        /// �����շ���Ŀ�ж��Ƿ�ΪƬ��
        /// </summary>
        /// <param name="chrgitemid"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckmedicament(string chrgitemid)
        {
            long lngRes = 0;
            bool blnRet = false;

            string SQL = @"select count(a.itemid_chr) as nums
                             from t_bse_chargeitem a,
                                  t_bse_medicine b,
                                  t_aid_medicinepreptype c
                            where a.itemsrcid_vchr = b.medicineid_chr
                              and b.medicinepreptype_chr = c.medicinepreptype_chr
                              and trim(c.medicinepreptypename_vchr) like '%Ƭ��%'
                              and a.itemid_chr = ?";

            DataTable dtRecord = new DataTable();

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = chrgitemid;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            if (lngRes > 0)
            {
                if (dtRecord.Rows[0][0].ToString() != "0")
                {
                    blnRet = true;
                }
            }
            return blnRet;
        }
        #endregion

        #region ���ó�ʼ��ģ������
        /// <summary>
        /// ���ó�ʼ��ģ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strFormDesc"></param>
        /// <param name="p_strControlName"></param>
        /// <param name="p_strControlDesc"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetModeByItem(string p_strFormName, string p_strFormDesc, string p_strControlName, string p_strControlDesc)
        {
            long lngRes = 0;
            DataTable dt = new DataTable();
            #region һ��SQL���
            //            string strSQL = @"
            //                                DECLARE
            //                                   v_count   NUMBER := 0;
            //                                   v_id      NUMBER := 0;
            //                                BEGIN
            //                                   SELECT COUNT (*)
            //                                     INTO v_count
            //                                     FROM gui_control
            //                                    WHERE form_id = '"+p_strFormName+@"'
            //                                          AND control_id = '"+p_strControlName+@"';
            //
            //                                   IF v_count = 0
            //                                   THEN
            //                                      BEGIN
            //                                         INSERT INTO gui_control
            //                                                     (form_id, control_id,
            //                                                      control_desc, order_no
            //                                                     )
            //                                              VALUES ('"+p_strFormName+@"', '"+p_strControlName+@"',
            //                                                      '"+p_strControlDesc+@"', 0
            //                                                     );
            //                                      END;
            //                                   END IF;
            //
            //                                   SELECT MAX (ID) + 1
            //                                     INTO v_id
            //                                     FROM gui_form;
            //
            //                                   SELECT COUNT (*)
            //                                     INTO v_count
            //                                     FROM gui_form
            //                                    WHERE form_id = '"+p_strFormName+@"';
            //
            //                                   IF v_count = 0
            //                                   THEN
            //                                      BEGIN
            //                                         INSERT INTO gui_form
            //                                                     (ID, form_id, parent_id, form_desc
            //                                                     )
            //                                              VALUES (v_id, '"+p_strFormName+@"', 0, '"+p_strFormDesc+@"'
            //                                                     );
            //                                      END;
            //                                   END IF;
            //                                END;
            //                                ";

            #endregion
            try
            {
                #region ����������"һ��SQL���"�����Ĳ��,��Ϊ���治֪��ؿ��
                string strSelectId = @" SELECT MAX (ID) + 1  FROM gui_form";
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSelectId, ref dt);
                string id = dt.Rows[0][0].ToString();
                string strSel = @"SELECT COUNT (*)
                                                     
                                                     FROM gui_control
                                                    WHERE form_id = '" + p_strFormName + @"'
                                                          AND control_id = '" + p_strControlName + @"'";
                lngRes = objHRPSvc.DoGetDataTable(strSel, ref dt);
                string strinsert = "";
                if (lngRes > 0)
                {
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        strinsert = @" INSERT INTO gui_control
                                                                     (form_id, control_id,
                                                                      control_desc, order_no
                                                                     )
                                                              VALUES ('" + p_strFormName + @"', '" + p_strControlName + @"',
                                                                      '" + p_strControlDesc + @"', 0
                                                                     )";
                        lngRes = objHRPSvc.DoExcute(strinsert);
                        if (lngRes <= 0)
                        {
                            return lngRes;
                        }
                    }
                }
                else
                {
                    return lngRes;
                }
                string sel2 = @"     SELECT COUNT (*)
                                                     
                                                     FROM gui_form
                                                    WHERE form_id = '" + p_strFormName + @"'";
                lngRes = objHRPSvc.DoGetDataTable(sel2, ref dt);
                strinsert = "";
                if (lngRes > 0)
                {
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        strinsert = @"  INSERT INTO gui_form
                                                                     (ID, form_id, parent_id, form_desc
                                                                     )
                                                              VALUES (" + id + @", '" + p_strFormName + @"', 0, '" + p_strFormDesc + @"'
                                                                     )";
                        lngRes = objHRPSvc.DoExcute(strinsert);
                        if (lngRes <= 0)
                        {
                            return lngRes;
                        }
                    }
                }
                else
                {
                    return lngRes;
                }
                #endregion

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ���ԡ���������һ����������
        /// <summary>
        /// ��ȡҩƷ���ԡ���������һ����������
        /// </summary>
        /// <param name="chrgitemid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetmedproperty(string chrgitemid, out DataTable dtRecord)
        {
            long lngRes = 0;
            dtRecord = new DataTable();

            string SQL = @"select a.isanaesthesia_chr, a.ispoison_chr, a.ischlorpromazine_chr, a.ischlorpromazine2_chr, a.medicinetypeid_chr 
                             from t_bse_medicine a,
                                  t_bse_chargeitem b
                            where a.medicineid_chr = b.itemsrcid_vchr
                              and b.itemid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = chrgitemid;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ��ϵͳ����
        /// <summary>
        /// ��ȡ��ϵͳ����
        /// </summary>
        /// <param name="parmcode">��������</param>
        /// <returns>ֵ</returns>
        [AutoComplete]
        public string m_strGetSysparm(string parmcode)
        {
            string parmvalue = "";
            string SQL = @"select parmvalue_vchr
                                 from t_bse_sysparm 
                                where status_int = 1 
                                  and parmcode_chr = ?";
            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {

                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = parmcode;
                DataTable dt = new DataTable();
                long l = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    parmvalue = dt.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                ParamArr = null;
                SQL = null;
            }

            return parmvalue;
        }
        /// <summary>
        /// ������ȡϵͳ����
        /// </summary>
        /// <param name="p_strParamKeyArr"></param>
        /// <param name="p_hasParamValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysparm(string[] p_strParamKeyArr, out Dictionary<string, string> p_hasParamValue)
        {
            long lngRes = 0;
            p_hasParamValue = null;
            System.Text.StringBuilder sbSub = new System.Text.StringBuilder();
            foreach (string strParamKey in p_strParamKeyArr)
            {
                sbSub.Append("'").Append(strParamKey).Append("',");
            }
            sbSub.Length = sbSub.Length - 1;
            string strSub = sbSub.ToString();
            sbSub = null;

            if (strSub == string.Empty)
            {
                return lngRes;
            }

            string SQL = @"select parmcode_chr, parmvalue_vchr
                                 from t_bse_sysparm 
                                where status_int = 1 
                                  and parmcode_chr in( " + strSub + " )";
            sbSub = null;

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_hasParamValue = new Dictionary<string, string>();
                    foreach (DataRow dtrTemp in dt.Rows)
                    {
                        p_hasParamValue.Add(dtrTemp["parmcode_chr"].ToString().Trim(), dtrTemp["parmvalue_vchr"].ToString().Trim());
                    }
                }
                dt = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                SQL = null;
            }

            return lngRes;
        }
        /// <summary>
        /// ������ȡϵͳ��������
        /// </summary>
        /// <param name="p_strParamKeyArr"></param>
        /// <param name="p_hasParamValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysSetting(string[] p_strParamKeyArr, out Dictionary<string, string> p_hasParamValue)
        {
            long lngRes = 0;
            p_hasParamValue = null;
            System.Text.StringBuilder sbSub = new System.Text.StringBuilder();
            foreach (string strParamKey in p_strParamKeyArr)
            {
                sbSub.Append("'").Append(strParamKey).Append("',");
            }
            sbSub.Length = sbSub.Length - 1;
            string strSub = sbSub.ToString();
            sbSub = null;

            if (strSub == string.Empty)
            {
                return lngRes;
            }

            string SQL = @"select setid_chr, setstatus_int from t_sys_setting where setid_chr in( " + strSub + " )";
            sbSub = null;

            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_hasParamValue = new Dictionary<string, string>();
                    foreach (DataRow dtrTemp in dt.Rows)
                    {
                        p_hasParamValue.Add(dtrTemp["setid_chr"].ToString().Trim(), dtrTemp["setstatus_int"].ToString().Trim());
                    }
                }
                dt = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                objLogger = null;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                SQL = null;
            }

            return lngRes;
        }
        #endregion

        #region �Զ����ɵ�ǰ��������(1 ������2 ����)
        /// <summary>
        /// �Զ����ɵ�ǰ��������(1 ������2 ����)
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="DoctorID"></param>
        /// <param name="Flag">0 δ�Һ� 1 �ѹҺ�</param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetRecipeType(string PatientID, string DoctorID, int Flag)
        {
            string RecipeType = "1";
            string SQL = "";
            long l = 0;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                DataTable dt = new DataTable();

                if (Flag == 0)
                {
                    System.DateTime sDate = clsGetServerDate.s_GetServerDate().Date;
                    string st1 = sDate.ToString("yyyy-MM-dd");

                    SQL = @"select count(a.outpatrecipeid_chr) 
                              from t_opr_outpatientrecipe a 
                             where a.patientid_chr = ?  
                               and a.diagdr_chr = ? 
                               and (a.pstauts_int = 2 or a.pstauts_int = 3 or a.pstauts_int = 4) 
                               and a.recorddate_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date(?,'yyyy-mm-dd hh24:mi:ss')";

                    objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                    ParamArr[0].Value = PatientID;
                    ParamArr[1].Value = DoctorID;
                    ParamArr[2].Value = st1 + " 00:00:00";
                    ParamArr[3].Value = st1 + " 23:59:59";

                }
                else if (Flag == 1)
                {
                    int TimeInterval = 0;
                    SQL = @"select setstatus_int from t_sys_setting where setid_chr = '0067'";
                    l = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                    if (l > 0 && dt.Rows.Count == 1)
                    {
                        string s = dt.Rows[0][0].ToString().Trim();
                        if (s != "" && Convert.ToInt32(s) > 0)
                        {
                            TimeInterval = Convert.ToInt32(s);
                        }
                    }

                    SQL = @"select count(a.outpatrecipeid_chr) 
                              from t_opr_outpatientrecipe a 
                             where trim(a.registerid_chr) = ?  
                               and a.diagdr_chr = ? 
                               and (a.pstauts_int = 2 or a.pstauts_int = 3 or a.pstauts_int = 4) 
                               and (sysdate between a.recorddate_dat and (a.recorddate_dat + ?/24))";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = PatientID.Trim();
                    ParamArr[1].Value = DoctorID;
                    ParamArr[2].Value = TimeInterval;
                }

                l = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count == 1)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                    {
                        RecipeType = "2";
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return RecipeType;
        }
        #endregion

        #region (ҽ��)��ȡ�ز�ҽ�������ֹ����

        #endregion

        #region (ҽ��)��ȡ���ֲ���Ϣ
        /// <summary>
        /// (ҽ��)��ȡ���ֲ���Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBSpeciaTypeDisease(out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SQL = @"select a.deacode_chr,
                                   a.deadesc_vchr,
                                   a.yearmoney_int,
                                   a.sort_int,
                                   a.status_int,
                                   a.note_vchr 
                             from t_opr_ybspecialtypedisease a                                     
                            where a.status_int = 1 ";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region (ҽ��)�������ֲ������ȡ����Ӧ��ICD10
        /// <summary>
        /// (ҽ��)�������ֲ������ȡ����Ӧ��ICD10
        /// </summary>
        /// <param name="deacode"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICD10ByDeacode(string deacode, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SQL = @"select a.deacode_chr,
                                   a.deadesc_vchr,
                                   a.yearmoney_int,
                                   a.sort_int,
                                   a.status_int,
                                   a.note_vchr, b.icdcode_chr, c.icdname_vchr 
                                 from t_opr_ybspecialtypedisease a, 
                                      t_opr_ybdeadeficd10 b,
                                      t_aid_icd10 c  
                                where a.deacode_chr = b.deacode_chr 
                                  and b.icdcode_chr = c.icdcode_chr  
                                  and a.status_int = 1 
                                  and a.deacode_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = deacode;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region (ҽ��)����ICD10��ȡ����Ӧ�����ֲ�����
        /// <summary>
        /// (ҽ��)����ICD10��ȡ����Ӧ�����ֲ�����
        /// </summary>
        /// <param name="icd10_id"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBSpeciaTypeDiseaseByICD10(string icd10_id, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SQL = @"select a.deacode_chr,
                                   a.deadesc_vchr,
                                   a.yearmoney_int,
                                   a.sort_int,
                                   a.status_int,
                                   a.note_vchr, b.icdcode_chr  
                                 from t_opr_ybspecialtypedisease a, 
                                      t_opr_ybdeadeficd10 b 
                                where a.deacode_chr = b.deacode_chr 
                                  and a.status_int = 1 
                                  and b.icdcode_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = icd10_id;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region (ҽ��)�������ֲ������ȡ����Ӧ���շ���Ŀ
        /// <summary>
        /// (ҽ��)�������ֲ������ȡ����Ӧ���շ���Ŀ
        /// </summary>
        /// <param name="deacode"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBSpecChargeItemByDeacode(string deacode, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SQL = @"select a.deacode_chr,
                                   a.deadesc_vchr,
                                   a.yearmoney_int,
                                   a.sort_int,
                                   a.status_int,
                                   a.note_vchr, b.itemid_chr  
                                 from t_opr_ybspecialtypedisease a, 
                                      t_opr_ybdeadefchargeitem b 
                                where a.deacode_chr = b.deacode_chr 
                                  and a.status_int = 1 
                                  and a.deacode_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //((OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;
                ParamArr[0].Value = deacode;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ���ݴ����Ż�ȡ������ӡ��Ϣ
        /// <summary>
        /// ���ݴ����Ż�ȡ������ӡ��Ϣ
        /// </summary>
        /// <param name="m_objPrintcipal"></param>
        /// <param name="strRecipedeID"></param>
        /// <param name="obj_VO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutpatientRecipeDetail(string strRecipedeID, out clsOutpatientPrintRecipe_VO obj_VO)
        {
            long lngRes = 0;
            string strSQL = @"SELECT a.outpatrecipeid_chr, a.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       a.unitid_chr, a.tolqty_dec AS qty_dec, a.unitprice_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, a.usageid_chr, a.days_int,
       a.freqid_chr, d.usagename_vchr, a.desc_vchr, b.itemopinvtype_chr,
       a.dosage_dec, a.itemspec_vchr, a.qty_dec AS dosageqty, a.itemname_vchr,
       b.itemcode_vchr, f.typename_vchr, e.freqname_chr, 0 times_int,
       0 min_qty_dec1, '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       't_opr_outpatientpwmrecipede' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, a.discount_dec,
       g.mednormalname_vchr, k.type_int, '' itemunit_vchr,
       g.medicinetypeid_chr
       FROM t_opr_outpatientpwmrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f,
       t_bse_usagetype d,
       t_aid_recipefreq e,
       t_bse_medicine g,
       (SELECT DISTINCT usageid_chr, type_int
                   FROM t_opr_setusage) k
       WHERE a.itemid_chr = b.itemid_chr
       AND a.deptmed_int <> 1
       AND a.outpatrecipeid_chr = ?
       AND b.itemopinvtype_chr = f.typeid_chr
       AND f.flag_int = 2
       AND a.usageid_chr = d.usageid_chr(+)
       AND a.freqid_chr = e.freqid_chr(+)
       AND b.itemsrcid_vchr = g.medicineid_chr(+)
       AND a.usageid_chr = k.usageid_chr(+)
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       a.unitid_chr, (a.qty_dec * a.times_int) AS qty_dec,
       a.unitprice_mny AS price_mny, a.tolprice_mny, a.medstoreid_chr,
       '' usageid_chr, 0 AS days_int, '' freqid_chr, d.usagename_vchr,
       '' desc_vchr, b.itemopinvtype_chr, b.dosage_dec, a.itemspec_vchr,
       0 AS dosageqty, a.itemname_vchr, b.itemcode_vchr, f.typename_vchr,
       e.freqname_chr, a.times_int, a.min_qty_dec AS min_qty_dec1,
       '' usagename_vchr, a.min_qty_dec, a.sumusage_vchr,
       't_opr_outpatientcmrecipede' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, a.discount_dec,
       g.mednormalname_vchr, k.type_int, '' itemunit_vchr,
       g.medicinetypeid_chr
       FROM t_opr_outpatientcmrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f,
       t_bse_usagetype d,
       t_aid_recipefreq e,
       t_bse_medicine g,
       (SELECT DISTINCT usageid_chr, type_int
                   FROM t_opr_setusage) k
       WHERE a.itemid_chr = b.itemid_chr
       AND a.deptmed_int <> 1
       AND a.outpatrecipeid_chr = ? 
       AND a.itemid_chr = e.freqid_chr(+)
       AND b.itemopinvtype_chr = f.typeid_chr
       AND f.flag_int = 2
       AND a.usageid_chr = d.usageid_chr(+)
       AND b.itemsrcid_vchr = g.medicineid_chr(+)
       AND a.usageid_chr = k.usageid_chr(+)
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       a.unitid_chr, a.qty_dec AS qty_dec, a.unitprice_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       't_opr_outpatientothrecipede' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       g.mednormalname_vchr, 0 type_int, a.itemunit_vchr,
       g.medicinetypeid_chr
       FROM t_opr_outpatientothrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f,
       t_bse_medicine g
       WHERE a.itemid_chr = b.itemid_chr
       AND a.deptmed_int <> 1
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr
       AND b.itemsrcid_vchr = g.medicineid_chr(+)
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       '' unitid_chr, a.qty_dec AS qty_dec, a.price_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       'T_OPR_OUTPATIENTCHKRECIPEDE' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       '' AS mednormalname_vchr, 0 type_int, a.itemunit_vchr,
       '' medicinetypeid_chr
       FROM t_opr_outpatientchkrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f
       WHERE a.itemid_chr = b.itemid_chr
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       '' unitid_chr, a.qty_dec AS qty_dec, a.price_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       'T_OPR_OUTPATIENTTESTRECIPEDE' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       '' AS mednormalname_vchr, 0 type_int, a.itemunit_vchr,
       '' medicinetypeid_chr
       FROM t_opr_outpatienttestrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f
       WHERE a.itemid_chr = b.itemid_chr
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr
       UNION ALL
       SELECT a.outpatrecipeid_chr, b.dosageunit_chr, a.rowno_chr, a.itemid_chr,
       '' unitid_chr, a.qty_dec AS qty_dec, a.price_mny AS price_mny,
       a.tolprice_mny, a.medstoreid_chr, '' AS usageid_chr, 0 AS days_int,
       '' AS freqid_chr, '' AS usagename_vchr, '' AS desc_vchr,
       b.itemopinvtype_chr, 0 AS dosage_dec, a.itemspec_vchr,
       a.qty_dec AS dosageqty, a.itemname_vchr, b.itemcode_vchr,
       f.typename_vchr, '' freqname_chr, 0 times_int, 0 min_qty_dec1,
       '' usagename_vchr, 0 min_qty_dec, '' sumusage_vchr,
       'T_OPR_OUTPATIENTOPSRECIPEDE' AS fromtable,
       b.itemsrcid_vchr AS medicineid_chr, b.dosage_dec AS discount_dec,
       '' AS mednormalname_vchr, 0 type_int, a.itemunit_vchr,
       '' medicinetypeid_chr
       FROM t_opr_outpatientopsrecipede a,
       t_bse_chargeitem b,
       t_bse_chargeitemextype f
       WHERE a.itemid_chr = b.itemid_chr
       AND a.outpatrecipeid_chr = ? 
       AND b.itemopinvtype_chr = f.typeid_chr";
            obj_VO = null;
            try
            {
                DataTable dtbResult = new DataTable();
                clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = strRecipedeID;
                ParamArr[1].Value = strRecipedeID;
                ParamArr[2].Value = strRecipedeID;
                ParamArr[3].Value = strRecipedeID;
                ParamArr[4].Value = strRecipedeID;
                ParamArr[5].Value = strRecipedeID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    obj_VO = new clsOutpatientPrintRecipe_VO();

                    obj_VO.objinjectArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                    obj_VO.objPRDArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                    obj_VO.objPRDArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
                    decimal m_decWM = 0;
                    decimal m_decCM = 0;
                    decimal m_decOther = 0;
                    decimal m_decCheck = 0;
                    decimal m_decTest = 0;
                    decimal m_decOperation = 0;
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsOutpatientPrintRecipeDetail_VO objtemp = new clsOutpatientPrintRecipeDetail_VO();

                        objtemp.m_strChargeName = dtbResult.Rows[i]["ITEMNAME_VCHR"].ToString().Trim();
                        objtemp.m_strCount = dtbResult.Rows[i]["QTY_DEC"].ToString().Trim() + dtbResult.Rows[i]["UNITID_CHR"].ToString().Trim();
                        objtemp.m_strPrice = dtbResult.Rows[i]["PRICE_MNY"].ToString().Trim();
                        objtemp.m_strSumPrice = dtbResult.Rows[i]["TOLPRICE_MNY"].ToString().Trim();
                        objtemp.m_strUnit = dtbResult.Rows[i]["UNITID_CHR"].ToString().Trim();
                        objtemp.m_strFrequency = dtbResult.Rows[i]["FREQNAME_CHR"].ToString().Trim();
                        objtemp.m_strDosage = dtbResult.Rows[i]["DOSAGEQTY"].ToString().Trim() + dtbResult.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim();
                        objtemp.m_strDays = dtbResult.Rows[i]["DAYS_INT"].ToString().Trim();
                        objtemp.m_strSpec = dtbResult.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim();
                        objtemp.m_strUsage = dtbResult.Rows[i]["USAGENAME_VCHR"].ToString().Trim();
                        objtemp.m_strRowNo = dtbResult.Rows[i]["ROWNO_CHR"].ToString().Trim();
                        objtemp.m_strUsageDetail = dtbResult.Rows[i]["DESC_VCHR"].ToString().Trim();
                        objtemp.m_strInvoiceCat = dtbResult.Rows[i]["itemopinvtype_chr"].ToString().Trim();
                        obj_VO.m_strHerbalmedicineUsage = "";
                        if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "t_opr_outpatientpwmrecipede")
                        {
                            m_decWM += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                            obj_VO.objPRDArr.Add(objtemp);
                        }
                        else if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "t_opr_outpatientcmrecipede")
                        {
                            obj_VO.m_strHerbalmedicineUsage = dtbResult.Rows[i]["SUMUSAGE_VCHR"].ToString().Trim();
                            obj_VO.m_strTimes = dtbResult.Rows[i]["TIMES_INT"].ToString().Trim();
                            m_decCM += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                            obj_VO.objPRDArr2.Add(objtemp);
                        }
                        else
                        {
                            if (dtbResult.Rows[i]["TYPENAME_VCHR"].ToString().Trim() != "����" && dtbResult.Rows[i]["TYPENAME_VCHR"].ToString().Trim() != "���")
                            {
                                objtemp.m_strCount = dtbResult.Rows[i]["QTY_DEC"].ToString().Trim() + dtbResult.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim();
                                obj_VO.objinjectArr.Add(objtemp);
                                if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "T_OPR_OUTPATIENTCHKRECIPEDE")
                                {
                                    m_decCheck += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "T_OPR_OUTPATIENTTESTRECIPEDE")
                                {
                                    m_decTest += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else if (dtbResult.Rows[i]["fromtable"].ToString().Trim() == "T_OPR_OUTPATIENTOPSRECIPEDE")
                                {
                                    m_decOperation += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else
                                {
                                    m_decOther += decimal.Parse(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                            }
                        }

                    }
                    obj_VO.m_strWMedicineCost = m_decWM.ToString("0.00");
                    obj_VO.m_strZCMedicineCost = m_decCM.ToString("0.00");
                    obj_VO.m_strCureCost = ((decimal)(m_decCheck + m_decTest + m_decOperation + m_decOther)).ToString("0.00");


                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT a.outpatrecipeid_chr, a.patientid_chr, a.createdate_dat, 
a.registerid_chr, a.diagdr_chr, a.diagdept_chr, a.recordemp_chr,
 a.recorddate_dat, a.pstauts_int, a.recipeflag_int, a.outpatrecipeno_vchr,
  a.paytypeid_chr, a.casehisid_chr, a.groupid_chr, a.type_int, a.confirm_int,
   a.confirmdesc_vchr, a.createtype_int, a.deptmed_int, a.archtakeflag_int,
    a.printed_int, a.chargedeptid_chr, b.name_vchr, c.lastname_vchr, d.deptname_vchr,b.BIRTH_DAT,
       e.lastname_vchr AS recordemp, h.homeaddress_vchr,h.SEX_CHR,h.IDCARD_CHR,
       h.homephone_vchr, h.govcard_chr, h.difficulty_vchr, h.insuranceid_vchr,
       k.paytypename_vchr, p.diag_vchr, j.patientcardid_chr,
       (SELECT SUM (totalsum_mny)
          FROM t_opr_outpatientrecipeinv
         WHERE outpatrecipeid_chr = ? 
  AND totalsum_mny > 0) totailmoney
  FROM t_opr_outpatientrecipe a,
       t_bse_patient  b,
       t_bse_employee c,
       t_bse_deptdesc d,
       t_bse_employee e,
       t_bse_patient h,
       t_bse_patientpaytype k,
       t_bse_patientcard j,
       t_opr_outpatientcasehis p
 WHERE a.patientid_chr = b.patientid_chr(+)
   AND a.diagdr_chr = c.empid_chr(+)
   AND a.diagdept_chr = d.deptid_chr(+)
   AND a.recordemp_chr = e.empid_chr(+)
   AND a.patientid_chr = h.patientid_chr(+)
   AND a.paytypeid_chr = k.paytypeid_chr(+)
   AND a.patientid_chr = j.patientid_chr(+)
   AND a.casehisid_chr = p.casehisid_chr(+)
   AND a.outpatrecipeid_chr = ?";

            try
            {
                DataTable dtbResult = new DataTable();
                clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = strRecipedeID;
                ParamArr[1].Value = strRecipedeID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, ParamArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    DateTime dteBirth = Convert.ToDateTime("1900-1-1");
                    if (dtbResult.Rows[0]["BIRTH_DAT"] != System.DBNull.Value)
                        dteBirth = Convert.ToDateTime(dtbResult.Rows[0]["BIRTH_DAT"].ToString());

                    if (obj_VO == null)
                    {
                        obj_VO = new clsOutpatientPrintRecipe_VO();
                        obj_VO.objinjectArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                        obj_VO.objPRDArr = new List<clsOutpatientPrintRecipeDetail_VO>();
                        obj_VO.objPRDArr2 = new List<clsOutpatientPrintRecipeDetail_VO>();
                    }
                    obj_VO.m_strAge = com.digitalwave.iCare.middletier.HIS.clsConvertDateTime.CalcAge(dteBirth);
                    obj_VO.m_strDiagDrName = dtbResult.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
                    obj_VO.m_strHospitalName = "��ݸ��ɽҽԺ";
                    obj_VO.m_strPatientName = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                    obj_VO.m_strPhotoNo = dtbResult.Rows[0]["HOMEPHONE_VCHR"].ToString().Trim();
                    obj_VO.m_strCardID = dtbResult.Rows[0]["PATIENTCARDID_CHR"].ToString().Trim();
                    obj_VO.m_strdiagnose = dtbResult.Rows[0]["diag_vchr"].ToString().Trim();
                    obj_VO.m_strPatientType = dtbResult.Rows[0]["PAYTYPENAME_VCHR"].ToString().Trim();
                    obj_VO.m_strDiagDeptID = dtbResult.Rows[0]["DEPTNAME_VCHR"].ToString().Trim();
                    obj_VO.m_strRecipeID = strRecipedeID;
                    obj_VO.m_strRecordEmpID = dtbResult.Rows[0]["DIAGDR_CHR"].ToString().Trim().Substring(3);//Ա��ID
                    obj_VO.m_strIDcardno = dtbResult.Rows[0]["IDCARD_CHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["RECORDDATE_DAT"] != System.DBNull.Value)
                        obj_VO.m_strPrintDate = DateTime.Parse(dtbResult.Rows[0]["RECORDDATE_DAT"].ToString()).ToString("yyyy-MM-dd");
                    else
                        obj_VO.m_strPrintDate = DateTime.Now.ToString("yyyy-MM-dd");
                    obj_VO.m_strSex = dtbResult.Rows[0]["SEX_CHR"].ToString().Trim();
                    obj_VO.m_strSelfPay = "";
                    obj_VO.m_strChargeUp = "";
                    obj_VO.m_strRecipePrice = "";

                    obj_VO.m_strHerbalmedicineUsage = "";
                    obj_VO.m_strAddress = dtbResult.Rows[0]["HOMEADDRESS_VCHR"].ToString().Trim();
                    if (dtbResult.Rows[0]["RECIPEFLAG_INT"].ToString().Trim() == "1")
                    {
                        obj_VO.m_strRecipeType = "����";
                    }
                    else if (dtbResult.Rows[0]["RECIPEFLAG_INT"].ToString().Trim() == "2")
                    {
                        obj_VO.m_strRecipeType = "����";
                    }
                    else
                    {
                        obj_VO.m_strRecipeType = "";
                    }
                    obj_VO.m_strGOVCARD = dtbResult.Rows[0]["GOVCARD_CHR"].ToString().Trim();
                    obj_VO.m_strINSURANCEID = dtbResult.Rows[0]["INSURANCEID_VCHR"].ToString().Trim();
                    obj_VO.m_strRegisterID = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    obj_VO.m_strPayType = dtbResult.Rows[0]["PAYTYPEID_CHR"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ����������Ϣ
        /// <summary>
        ///  ��ȡ����������Ϣ
        /// </summary>
        /// <param name="m_Principal"></param>
        /// <param name="m_strRecipeID"></param>
        /// <param name="m_objRTVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeTypeInfo(string m_strRecipeID, out clsRecipeType_VO m_objRTVO)
        {
            long lngRes = -1;
            DataTable m_objTable = new DataTable();
            m_objRTVO = null;
            string strSQL = @"SELECT a.type_int, a.typename_vchr, a.r_int, a.g_int, a.b_int, a.remark_vchr, a.medproperty_vchr
                              FROM t_aid_recipetype a, t_opr_outpatientrecipe b
                              WHERE a.type_int = b.type_int AND b.outpatrecipeid_chr = ?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService m_objHRP = new clsHRPTableService();
            IDataParameter[] ParamArr = null;
            m_objHRP.CreateDatabaseParameter(1, out ParamArr);
            ParamArr[0].Value = m_strRecipeID;
            lngRes = m_objHRP.lngGetDataTableWithParameters(strSQL, ref m_objTable, ParamArr);
            if (lngRes > 0 && m_objTable.Rows.Count > 0)
            {
                m_objRTVO = new clsRecipeType_VO();
                m_objRTVO.B_INT = int.Parse(m_objTable.Rows[0]["B_INT"].ToString().Trim());
                m_objRTVO.G_INT = int.Parse(m_objTable.Rows[0]["G_INT"].ToString().Trim());
                m_objRTVO.R_INT = int.Parse(m_objTable.Rows[0]["R_INT"].ToString().Trim());
                m_objRTVO.REMARK_VCHR = m_objTable.Rows[0]["REMARK_VCHR"].ToString().Trim();
                m_objRTVO.TYPENAME_VCHR = m_objTable.Rows[0]["TYPENAME_VCHR"].ToString().Trim();
                m_objRTVO.TYPE_INT = m_objTable.Rows[0]["TYPE_INT"].ToString().Trim();
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�����շ���Ŀ�ٴ���˼
        /// <summary>
        /// ��ȡ�����շ���Ŀ�ٴ���˼
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLisItemClinicMeaning(string ItemID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @" select a.check_item_name_vchr, a.clinic_meaning_vchr 
                              from t_bse_lis_check_item a, 
                                   t_aid_lis_apply_unit_detail b, 
                                   t_bse_chargeitem c  
                             where a.check_item_id_chr = b.check_item_id_chr 
                               and b.apply_unit_id_chr = c.itemsrcid_vchr 
                               and c.itemid_chr like ?";
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ItemID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ���ô���Ȩ
        /// <summary>
        /// ��ȡ���￪��ҽ���б�
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoctorList(out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select d.empno_chr, d.empid_chr, d.lastname_vchr, nvl(e.deptname_vchr, '[δ��]') as deptname  
                                from t_sys_module a, 
                                     t_sys_rolemodulemap b, 
                                     t_sys_emprolemap c,
                                     t_bse_employee d,   
                                     (select ta.empid_chr, tb.deptname_vchr 
     	                                from t_bse_deptemp ta,
			                                 t_bse_deptdesc tb
		                                where ta.deptid_chr = tb.deptid_chr
		                                  and ta.default_dept_int = 1	 
	                                 ) e	
                                where a.moduleid_chr = b.moduleid_chr
                                  and b.roleid_chr = c.roleid_chr
                                  and c.empid_chr = d.empid_chr
                                  and d.empid_chr = e.empid_chr(+)  
                                  and lower(a.classname_vchr) = 'com.digitalwave.icare.gui.his.frmdoctorworkstation' 
                             order by e.deptname_vchr asc";
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        /// <summary>
        /// ����ҽ��Ȩ�޶����
        /// </summary>
        /// <param name="objArr"></param>
        /// <param name="Flag">1 �¼� 2 ɾ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveDoctorRecipePurview(List<clsOutRecipePurview_VO> objArr, int Flag)
        {
            string SQL = "";
            long lngRes = 0, lngAffects = 0;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                for (int i = 0; i < objArr.Count; i++)
                {
                    clsOutRecipePurview_VO RecipePurview_VO = objArr[i];

                    string empid = RecipePurview_VO.EmpID;

                    List<string> purarr = RecipePurview_VO.PurviewArr;

                    for (int j = 0; j < purarr.Count; j++)
                    {
                        string purviewid = purarr[j].ToString();

                        if (Flag == 1)
                        {
                            SQL = "delete from t_opr_defrecipetabpage where empid_chr = ? and purview_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = empid;
                            ParamArr[1].Value = purviewid;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                            SQL = "insert into t_opr_defrecipetabpage (empid_chr, purview_chr) values (?, ?)";

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = empid;
                            ParamArr[1].Value = purviewid;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                        }
                        else if (Flag == 2)
                        {
                            SQL = "delete from t_opr_defrecipetabpage where empid_chr = ? and purview_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = empid;
                            ParamArr[1].Value = purviewid;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡҽ��Ȩ�޶����
        /// </summary>
        /// <param name="DoctID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoctorRecipePurview(string DoctID, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SQL = @"select a.empid_chr, a.purview_chr
                             from t_opr_defrecipetabpage a                                     
                            where a.empid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = DoctID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ���Ҵ���-������Ŀ
        /// <summary>
        /// ���Ҵ���-������Ŀ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="OrderCatArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindRecipeOrderByID(string ID, List<string> OrderCatArr, out DataTable dt, bool isChildPrice)
        {
            long lngRes = 0;
            dt = new DataTable();

            string SubStr = "";

            if (OrderCatArr != null && OrderCatArr.Count > 0)
            {
                string str = "";

                for (int i = 0; i < OrderCatArr.Count; i++)
                {
                    str += "b.ordercateid_chr = '" + OrderCatArr[i] + "' or ";
                }

                str = str.Trim();
                SubStr = " and (" + str.Substring(0, str.Length - 2) + ")";
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                string strSQL = @" select a.orderdicid_chr, a.name_chr, a.des_vchr, a.usercode_chr, a.wbcode_chr, a.pycode_chr,
 a.execdept_chr, a.ordercateid_chr, a.itemid_chr, a.nullitemdosageunit_chr, 
 a.nullitemuseunit_chr, a.nullitemdosetypeid_chr, a.status_int, a.sampleid_vchr, 
 a.partid_vchr, a.engname_vchr, a.commname_vchr, a.nullitemfreqid_vchr, a.nullitemuse_dec,
  a.lisapplyunitid_chr, a.applytypeid_chr, a.newchargetype_int, a.opexecdept_chr , a.sampleid_vchr as sample_type_id_chr, d.sample_type_desc_vchr, e.totalmny, f.partname, g.usageid_chr,
                                           h.itemspec_vchr, h.itemopinvtype_chr, h.ybtypename, h.itemunit         
                                      from t_bse_bih_orderdic a,
                                           t_aid_bih_ordercate b,       
                                           t_aid_lis_apply_unit c,
                                           t_aid_lis_sampletype d,
                                           (
	   	                                    select a.orderdicid_chr, sum(round(b.qty_int * c.itemprice_mny, 2)) as totalmny
		   	                                  from t_bse_bih_orderdic a,
       		                                       t_aid_bih_orderdic_charge b,
       		                                       (select m.itemid_chr,
                                                           (case m.opchargeflg_int
                                                             when 1 then
                                                              {0}
                                                             else
                                                              {1}
                                                           end) as itemprice_mny
                                                      from t_bse_chargeitem m
                                                     where m.ifstop_int = 0) c
       	                                     where a.orderdicid_chr = b.orderdicid_chr
		                                       and b.itemid_chr = c.itemid_chr(+)		                                      
	                                        group by a.orderdicid_chr	   
	                                       ) e,
                                           ar_apply_partlist f,
                                           t_bse_chargeitem g,
                                           (
                                            select a.orderdicid_chr, c.itemspec_vchr, c.itemopinvtype_chr, c.typename_vchr as ybtypename, (case c.ipchargeflg_int when 1 then c.itemipunit_chr else c.itemunit_chr end) as itemunit 
                                            from t_bse_bih_orderdic a,
                                                 ( select a.itemid_chr, a.itemspec_vchr, a.itemopinvtype_chr, b.typename_vchr, a.ipchargeflg_int, a.itemipunit_chr, a.itemunit_chr  
                                                     from t_bse_chargeitem a,
                                                          t_aid_medicaretype b    
                                                    where a.inpinsurancetype_vchr = b.typeid_chr(+)  
                                                      and a.ifstop_int = 0      
                                                 ) c 
                                            where a.itemid_chr = c.itemid_chr(+)                                                                                              
                                           ) h   
                                     where a.ordercateid_chr = b.ordercateid_chr                                         
                                       and a.applytypeid_chr = c.apply_unit_id_chr(+) 
                                       and a.sampleid_vchr = d.sample_type_id_chr(+) 
                                       and a.orderdicid_chr = e.orderdicid_chr 
                                       and a.orderdicid_chr = h.orderdicid_chr 
                                       and a.partid_vchr = f.partid(+) 
                                       and a.itemid_chr = g.itemid_chr(+)    
                                       and a.status_int = 1 " + SubStr + @"                                        
                                       and ((lower(a.pycode_chr) like ?) or (lower(a.wbcode_chr) like ?)
                                            or ((lower(a.usercode_chr) like ?)) or (lower(a.name_chr || a.commname_vchr) like ?)) 
                                 order by a.name_chr ";

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case m.ischildprice when 1 then round(m.itemprice_mny * " + EntityChildPrice.AddScale + " / m.packqty_dec, 4) else round(m.itemprice_mny / m.packqty_dec, 4) end)", "(case m.ischildprice when 1 then (m.itemprice_mny * " + EntityChildPrice.AddScale + ") else m.itemprice_mny end)");
                else
                    strSQL = string.Format(strSQL, "round(m.itemprice_mny / m.packqty_dec, 4)", "m.itemprice_mny");

                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = ID.ToLower() + "%";
                ParamArr[1].Value = ID.ToLower() + "%";
                ParamArr[2].Value = ID.ToLower() + "%";
                ParamArr[3].Value = "%" + ID.ToLower() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ���Ҵ���-������Ŀ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindRecipeOrderByID(string ID, out DataTable dt, bool isChildPrice)
        {
            long lngRes = 0;
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                string strSQL = @"select   a.orderdicid_chr, a.name_chr, a.des_vchr, a.usercode_chr,
                                         a.wbcode_chr, a.pycode_chr, a.execdept_chr, a.ordercateid_chr,
                                         a.itemid_chr, a.nullitemdosageunit_chr, a.nullitemuseunit_chr,
                                         a.nullitemdosetypeid_chr, a.status_int, a.sampleid_vchr,
                                         a.partid_vchr, a.engname_vchr, a.commname_vchr,
                                         a.nullitemfreqid_vchr, a.nullitemuse_dec, a.lisapplyunitid_chr,
                                         a.applytypeid_chr, a.newchargetype_int,
                                         a.sampleid_vchr as sample_type_id_chr, d.sample_type_desc_vchr,
                                         e.totalmny, f.partname, g.usageid_chr
                                    from t_bse_bih_orderdic a,
                                         t_aid_bih_ordercate b,
                                         t_aid_lis_apply_unit c,
                                         t_aid_lis_sampletype d,
                                         (select a.orderdicid_chr,
       sum(round(b.qty_int * c.itemprice_mny, 2)) as totalmny
  from t_bse_bih_orderdic a,
       t_aid_bih_orderdic_charge b,
       (select m.itemid_chr,
               m.ifstop_int,
               (case m.opchargeflg_int
                 when 1 then
                  {0}
                 else
                  {1}
               end) as itemprice_mny
          from t_bse_chargeitem m) c
 where a.orderdicid_chr = b.orderdicid_chr
   and b.itemid_chr = c.itemid_chr
   and c.ifstop_int = 0
 group by a.orderdicid_chr) e,
                                         ar_apply_partlist f,
                                         t_bse_chargeitem g
                                   where a.ordercateid_chr = b.ordercateid_chr
                                     and a.applytypeid_chr = c.apply_unit_id_chr(+)
                                     and a.sampleid_vchr = d.sample_type_id_chr(+)
                                     and a.orderdicid_chr = e.orderdicid_chr
                                     and a.partid_vchr = f.partid(+)
                                     and a.itemid_chr = g.itemid_chr(+)
                                     and a.status_int = 1
                                     and a.orderdicid_chr = ?
                                order by a.name_chr";

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case m.ischildprice when 1 then round(m.itemprice_mny * " + EntityChildPrice.AddScale + " / m.packqty_dec, 4) else round(m.itemprice_mny / m.packqty_dec, 4) end)", "(case m.ischildprice when 1 then (m.itemprice_mny * " + EntityChildPrice.AddScale + ") else m.itemprice_mny end)");
                else
                    strSQL = string.Format(strSQL, "round(m.itemprice_mny / m.packqty_dec, 4)", "m.itemprice_mny");

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����������Ŀ��ȡ�շ���Ŀ
        /// <summary>
        /// ����������Ŀ��ȡ�շ���Ŀ
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemByOrderID(string OrderID, string PayTypeID, out DataTable dt, bool isChildPrice)
        {
            long lngRes = 0;
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                string strSQL = @" select decode (c.ipchargeflg_int, 0, {0}, 1, {1}
              ) as itemprice_mny,c.itemid_chr, c.itemname_vchr, c.itemcode_vchr, c.itempycode_chr, c.itemwbcode_chr, c.itemsrcid_vchr,
 c.itemsrctype_int, c.itemspec_vchr, {2}, c.itemunit_chr,
  c.itemopunit_chr, c.itemipunit_chr, c.itemopcalctype_chr, c.itemipcalctype_chr, 
  c.itemopinvtype_chr, c.itemipinvtype_chr, c.dosage_dec, c.dosageunit_chr, c.isgroupitem_int, 
  c.itemcatid_chr, c.usageid_chr, c.itemopcode_chr, c.insuranceid_chr, c.selfdefine_int,
  c.packqty_dec, c.tradeprice_mny, c.poflag_int, c.isrich_int, c.opchargeflg_int, 
  c.itemsrcname_vchr, c.itemsrctypename_vchr, c.itemengname_vchr, c.ifstop_int, 
  c.pdcarea_vchr, c.ipchargeflg_int, c.insurancetype_vchr, c.apply_type_int, 
  c.itembihctype_chr, c.defaultpart_vchr, c.itemchecktype_chr, c.itemcommname_vchr, 
  c.ordercateid_chr, c.freqid_chr, c.inpinsurancetype_vchr, c.ordercateid1_chr, 
  c.isselfpay_chr, c.itemprice_mny_old, c.itemprice_mny_new, c.keepuse_int, 
  c.price_temp, c.itemspec_vchr1, c.lastchange_dat, c.itemname_vchr as itemname, b.qty_int as totalqty_dec, b.usescope_int, d.precent_dec
                                      from t_bse_bih_orderdic a,
                                           t_aid_bih_orderdic_charge b,
                                           t_bse_chargeitem c,                                         
                                           (select precent_dec,itemid_chr,copayid_chr from t_aid_inschargeitem where copayid_chr = ?) d
                                     where a.orderdicid_chr = b.orderdicid_chr 
                                       and b.itemid_chr = c.itemid_chr                                        
                                       and c.itemid_chr = d.itemid_chr(+) 
                                       and a.status_int = 1      
                                       and c.ifstop_int = 0                                      
                                       and a.orderdicid_chr = ? 
                                  order by c.itemname_vchr ";

                if (isChildPrice)
                    strSQL = string.Format(strSQL, "(case c.ischildprice when 1 then (c.itemprice_mny * " + EntityChildPrice.AddScale + ") else c.itemprice_mny end)", "(case c.ischildprice when 1 then round(c.itemprice_mny * " + EntityChildPrice.AddScale + " / c.packqty_dec, 4) else round(c.itemprice_mny / c.packqty_dec, 4) end)", "(case c.ischildprice when 1 then (c.itemprice_mny * " + EntityChildPrice.AddScale + ") else c.itemprice_mny end)");
                else
                    strSQL = string.Format(strSQL, "c.itemprice_mny", "round(c.itemprice_mny / c.packqty_dec, 4)", "c.itemprice_mny");

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = PayTypeID;
                ParamArr[1].Value = OrderID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �ж�������Ŀ(���)�Ƿ��������
        /// <summary>
        /// �ж�������Ŀ(���)�Ƿ��������
        /// </summary>
        /// <param name="OrderID">������ĿID</param>
        /// <param name="InvoCatArr">��Ʊ��������</param>
        /// <param name="SysType">ϵͳ 1 ���� 2 סԺ</param>
        /// <param name="ItemNums">��Ŀ����</param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckOrderDiscount(string OrderID, List<string> InvoCatArr, int SysType, int ItemNums)
        {
            long lngRes = 0;
            bool blnRet = false;
            DataTable dt = new DataTable();

            string SubStr = "";

            if (InvoCatArr != null && InvoCatArr.Count > 0)
            {
                string str = "";

                for (int i = 0; i < InvoCatArr.Count; i++)
                {
                    if (SysType == 1)
                    {
                        str += "c.itemopinvtype_chr = '" + InvoCatArr[i] + "' or ";
                    }
                    else if (SysType == 2)
                    {
                        str += "c.itemipinvtype_chr = '" + InvoCatArr[i].ToString() + "' or ";
                    }
                }

                str = str.Trim();
                SubStr = " and (" + str.Substring(0, str.Length - 2) + ")";
            }

            if (SysType == 1)
            {
                SubStr += " group by c.itemopinvtype_chr ";
            }
            else if (SysType == 2)
            {
                SubStr += " group by c.itemipinvtype_chr ";
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                string strSQL = @"select count(b.itemid_chr)
                                    from t_bse_bih_orderdic a,
                                         t_aid_bih_orderdic_charge b,
                                         t_bse_chargeitem c
                                    where a.orderdicid_chr = b.orderdicid_chr 
                                      and b.itemid_chr = c.itemid_chr 
                                      and a.status_int = 1    
                                      and a.orderdicid_chr = ? " + SubStr + @"                                     
                                    having count(b.itemid_chr) > ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = OrderID;
                ParamArr[1].Value = ItemNums;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, ParamArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    int count = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "0")
                        {
                            count += int.Parse(dt.Rows[i][0].ToString());
                        }
                    }
                    if (count > 0)
                    {
                        blnRet = true;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return blnRet;
        }
        #endregion

        #region ����Э������
        /// <summary>
        /// ����Э������
        /// </summary>
        /// <param name="CreateEmpID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAccordRecipe(string CreateEmpID, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select  a.recipeid_chr, a.recipename_chr, a.usercode_chr, a.pycode_chr,
                                   a.wbcode_chr, a.diseasename_vchr, a.privilege_int, a.status_int
                              from t_aid_concertrecipe a
                             where a.privilege_int = 0                              
                               and a.flag_int = 0
                            union all
                            select a.recipeid_chr, a.recipename_chr, a.usercode_chr, a.pycode_chr,
                                   a.wbcode_chr, a.diseasename_vchr, a.privilege_int, a.status_int
                              from t_aid_concertrecipe a
                             where a.privilege_int = 1
                               and a.createrid_chr = ?                            
                               and a.flag_int = 0
                            union all
                            select a.recipeid_chr, a.recipename_chr, a.usercode_chr, a.pycode_chr,
                                   a.wbcode_chr, a.diseasename_vchr, a.privilege_int, a.status_int
                              from t_aid_concertrecipe a,
                                   (select a.recipeid_chr
                                      from t_aid_concertrecipedept a
                                     where exists (select 1
                                                     from t_bse_deptemp b
                                                    where a.deptid_chr = b.deptid_chr and b.empid_chr = ?)) b
                             where a.recipeid_chr = b.recipeid_chr
                               and a.privilege_int = 2                              
                               and a.flag_int = 0";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = CreateEmpID;
                ParamArr[1].Value = CreateEmpID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region �ۺϲ�ѯ�շ���Ŀ
        /// <summary>
        /// �ۺϲ�ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr"></param>
        /// <param name="Type">1 ��ҩ 2 ��ҩ 3 ���� 4 ��� 5 ���� 6 ����</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindChargeItem(string FindStr, int Type, out DataTable dt, bool isChildPrice)
        {
            long lngRes = 0;
            string SQL = "";
            dt = new DataTable();

            if (Type == 1)
            {
                SQL = @"select   a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
                                 a.itemopunit_chr, a.itemipunit_chr, b.deptprep_int, a.dosageunit_chr,
                                 a.packqty_dec, {0}, {1}, 
                                 a.itempycode_chr as type, b.noqtyflag_int,
                                 b.mindosage_dec, b.maxdosage_dec, b.adultdosage_dec,
                                 b.childdosage_dec, b.nmldosage_dec, b.hype_int, a.opchargeflg_int,
                                 a.usageid_chr, a.itemcommname_vchr, b.ispoison_chr,
                                 b.isanaesthesia_chr, b.ischlorpromazine_chr, b.ischlorpromazine2_chr,
                                 a.itemopinvtype_chr, c.usagename_vchr, a.dosage_dec, a.itemcode_vchr,
                                 100 as precent_dec, g.partname, h.freqid_chr as freqid,
                                 h.freqname_chr as freqname, h.times_int as freqtimes,
                                 h.days_int as freqdays, y.typename_vchr as ybtypename
                            from t_bse_chargeitem a,
                                 t_bse_medicine b,
                                 t_bse_usagetype c,
                                 t_bse_chargecatmap d,
                                 ar_apply_partlist g,
                                 t_aid_recipefreq h,
                                 t_aid_medicaretype y
                           where a.itemsrcid_vchr = b.medicineid_chr(+)                            
                             and a.ifstop_int = 0
                             and a.insurancetype_vchr = y.typeid_chr(+)
                             and a.usageid_chr = c.usageid_chr(+)
                             and a.itemopinvtype_chr = d.catid_chr(+)
                             and d.groupid_chr = '0001'
                             and d.internalflag_int = 0
                             and a.itemchecktype_chr = g.partid(+)                          
                             and a.freqid_chr = h.freqid_chr(+)
                             and (   (a.itemname_vchr like ?)
                                  or (lower (a.itemcode_vchr) like ?)
                                  or (lower (a.itempycode_chr) like ?)
                                  or (lower (a.itemwbcode_chr) like ?)
                                  or (lower (a.itemopcode_chr) like ?)
                                 )
                        order by a.itempycode_chr";
            }
            else if (Type == 2)
            {
                SQL = @"select   a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
                                 b.deptprep_int, a.usageid_chr, c.usagename_vchr, a.dosageunit_chr,
                                 a.itempycode_chr as type,
                                 {0}, {1}, b.noqtyflag_int,
                                 b.mindosage_dec, a.itemopinvtype_chr, b.maxdosage_dec,
                                 b.adultdosage_dec, b.childdosage_dec, a.itemcommname_vchr,
                                 b.ispoison_chr, b.isanaesthesia_chr, b.ischlorpromazine_chr,
                                 b.ischlorpromazine2_chr, b.nmldosage_dec, a.itemipunit_chr,
                                 a.opchargeflg_int, a.packqty_dec, a.dosage_dec, a.itemcode_vchr,
                                 100 as precent_dec, y.typename_vchr as ybtypename
                            from t_bse_chargeitem a,
                                 t_bse_medicine b,
                                 t_bse_usagetype c,
                                 t_bse_chargecatmap d,
                                 t_aid_medicaretype y
                           where a.itemsrcid_vchr = b.medicineid_chr(+)
                             and a.usageid_chr = c.usageid_chr(+)
                             and a.ifstop_int = 0
                             and a.itemopinvtype_chr = d.catid_chr(+)
                             and a.insurancetype_vchr = y.typeid_chr(+)
                             and d.groupid_chr = '0002'   
                             and d.internalflag_int = 0
                             and (   (a.itemname_vchr like ?)
                                  or (lower (a.itemcode_vchr) like ?)
                                  or (lower (a.itempycode_chr) like ?)
                                  or (lower (a.itemwbcode_chr) like ?)
                                  or (lower (a.itemopcode_chr) like ?)
                                 )
                        order by a.itempycode_chr";
            }
            else if (Type == 3)
            {

            }
            else if (Type == 4)
            {

            }
            else if (Type == 5)
            {

            }
            else if (Type == 6)
            {
                SQL = @"select   a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemopinvtype_chr,
                                 a.itemcode_vchr, 100 as precent_dec, a.itemcommname_vchr,
                                 b.deptprep_int, y.typename_vchr as ybtypename,
                                 a.itemunit_chr as itemopunit_chr, {0}, {1}, a.selfdefine_int,
                                 a.itempycode_chr as type, a.itemengname_vchr
                            from t_bse_chargeitem a,
                                 t_bse_medicine b,
                                 t_bse_chargecatmap d,
                                 t_aid_medicaretype y
                           where a.itemsrcid_vchr = b.medicineid_chr(+)
                             and a.ifstop_int = 0
                             and a.itemopinvtype_chr = d.catid_chr(+)
                             and d.groupid_chr = '0005'
                             and d.internalflag_int = 0  
                             and a.insurancetype_vchr = y.typeid_chr(+)
                             and (   (a.itemname_vchr like ?)
                                  or (lower (a.itemcode_vchr) like ?)
                                  or (lower (a.itempycode_chr) like ?)
                                  or (lower (a.itemwbcode_chr) like ?)
                                  or (lower (a.itemopcode_chr) like ?)
                                 )
                        order by a.itempycode_chr";
            }

            if (Type == 1 || Type == 2 || Type == 6)
            {
                if (isChildPrice)
                    SQL = string.Format(SQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end) as submoney");
                else
                    SQL = string.Format(SQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4) as submoney");
            }
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                ParamArr[0].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[1].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[2].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[3].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[4].Value = FindStr.Trim().ToLower() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ����Э������
        /// <summary>
        /// ����Э������
        /// </summary>
        /// <param name="AccordRecipe_VO"></param>
        /// <param name="objRecEntryArr"></param>
        /// <param name="RecipeID"></param>
        [AutoComplete]
        public long m_lngSaveAccordRecipe(clsAccordRecipe_VO AccordRecipe_VO, List<clsAccordRecipePlus_VO> objRecEntryArr, out string RecipeID)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";

            RecipeID = AccordRecipe_VO.RecipeID;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (RecipeID.Trim() != "")
                {
                    this.m_lngDelAccordRecipe(RecipeID);
                }
                else
                {
                    DataTable dt = new DataTable();

                    //ȡ����ID
                    SQL = "select lpad(seq_accordrecipeid.nextval, 10, '0') from dual";
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                    if (lngRes > 0)
                    {
                        RecipeID = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        return 0;
                    }
                }

                SQL = @"insert into t_aid_concertrecipe (recipeid_chr, recipename_chr, privilege_int, usercode_chr, wbcode_chr, 
                                                         pycode_chr, status_int, createrid_chr, flag_int, diseasename_vchr)
                                                 values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                objHRPSvc.CreateDatabaseParameter(10, out ParamArr);
                ParamArr[0].Value = RecipeID;
                ParamArr[1].Value = AccordRecipe_VO.RecipeName;
                ParamArr[2].Value = AccordRecipe_VO.Privilege;
                ParamArr[3].Value = AccordRecipe_VO.UserCode;
                ParamArr[4].Value = AccordRecipe_VO.WbCode;
                ParamArr[5].Value = AccordRecipe_VO.PyCode;
                ParamArr[6].Value = AccordRecipe_VO.Status;
                ParamArr[7].Value = AccordRecipe_VO.CreaterID;
                ParamArr[8].Value = AccordRecipe_VO.Flag;
                ParamArr[9].Value = AccordRecipe_VO.DiseaseName;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                // ����ģ��
                if (AccordRecipe_VO.Privilege == "2")
                {
                    SQL = @"insert into t_aid_concertrecipedept (recipeid_chr, deptid_chr ) values (?, ?)";
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = RecipeID;
                    ParamArr[1].Value = AccordRecipe_VO.DeptID;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }

                for (int i = 0; i < objRecEntryArr.Count; i++)
                {

                    SQL = @"insert into t_aid_concertrecipe_plus (recipeid_chr, plusid_chr, itemid_chr, recno_chr, qty_dec, days_int, freqid_chr, 
                                                                  usageid_vchr, sampleid_chr, partid_int, type_int, class_int, rowno_int)
                                                          values (?, lpad(seq_accordrecipeplusid.nextval, 10, '0'), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    clsAccordRecipePlus_VO AccordRecipePlus_VO = objRecEntryArr[i];

                    objHRPSvc.CreateDatabaseParameter(12, out ParamArr);
                    ParamArr[0].Value = RecipeID;
                    ParamArr[1].Value = AccordRecipePlus_VO.ItemID;
                    ParamArr[2].Value = AccordRecipePlus_VO.RecNO;
                    ParamArr[3].Value = AccordRecipePlus_VO.Qty;
                    ParamArr[4].Value = AccordRecipePlus_VO.Days;
                    ParamArr[5].Value = AccordRecipePlus_VO.FreqID;
                    ParamArr[6].Value = AccordRecipePlus_VO.UsageID;
                    ParamArr[7].Value = AccordRecipePlus_VO.SampleID;
                    ParamArr[8].Value = AccordRecipePlus_VO.PartID;
                    ParamArr[9].Value = AccordRecipePlus_VO.Type;
                    ParamArr[10].Value = AccordRecipePlus_VO.Class;
                    ParamArr[11].Value = AccordRecipePlus_VO.RowNO;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ��ѯЭ������
        /// <summary>
        /// ��ѯЭ������
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="ClassID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAccordRecipe(string RecipeID, int ClassID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = "";
            dt = new DataTable();

            if (ClassID == 1)
            {
                SQL = @"select   a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
                                 a.itemopunit_chr, a.itemipunit_chr, b.deptprep_int, a.dosageunit_chr,
                                 a.itemprice_mny, round (a.itemprice_mny / a.packqty_dec, 4) as submoney, 
                                 a.packqty_dec, a.itemcode_vchr, 
                                 a.itempycode_chr as type, 0 as noqtyflag_int, b.mindosage_dec,
                                 b.maxdosage_dec, b.adultdosage_dec, b.childdosage_dec,
                                 b.nmldosage_dec, b.hype_int, a.opchargeflg_int, 
                                 a.itemcommname_vchr, b.ispoison_chr, 0 as ifstop_int, b.isanaesthesia_chr,
                                 b.ischlorpromazine_chr, b.ischlorpromazine2_chr, a.itemopinvtype_chr,
                                 c.usagename_vchr, a.dosage_dec, a.itemcode_vchr, 100 as precent_dec,
                                 h.freqname_chr as freqname, h.times_int as freqtimes, h.days_int as freqdays,
                                 y.typename_vchr as ybtypename, tp.recno_chr as recno, tp.qty_dec as recqty, 
                                 tp.days_int as recdays, tp.usageid_vchr as usageid_chr, tp.freqid_chr as freqid,
                                 tp.class_int as recclass, b.medicineid_chr 
                            from t_bse_chargeitem a,
                                 t_bse_medicine b,
                                 t_bse_usagetype c,
                                 t_aid_recipefreq h,
                                 t_aid_medicaretype y,
                                 t_aid_concertrecipe_plus tp 
                           where a.itemsrcid_vchr = b.medicineid_chr                             
                             and a.insurancetype_vchr = y.typeid_chr(+)   
                             and tp.itemid_chr = a.itemid_chr
                             and tp.usageid_vchr = c.usageid_chr(+)
                             and tp.freqid_chr = h.freqid_chr(+)
                             and tp.type_int = 0
                             and tp.class_int = 1 
                             and tp.recipeid_chr = ?
                        order by tp.rowno_int";
            }
            else if (ClassID == 2)
            {
                SQL = @"select   a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemengname_vchr,
                                 b.deptprep_int,  c.usagename_vchr, a.dosageunit_chr, a.itemcode_vchr, 
                                 a.itempycode_chr as type, 0 as ifstop_int,
                                 a.itemprice_mny, round (a.itemprice_mny / a.packqty_dec, 4) as submoney,
                                 0 as noqtyflag_int, b.mindosage_dec, a.itemopinvtype_chr,
                                 b.maxdosage_dec, b.adultdosage_dec, b.childdosage_dec,
                                 a.itemcommname_vchr, b.ispoison_chr, b.isanaesthesia_chr,
                                 b.ischlorpromazine_chr, b.ischlorpromazine2_chr, b.nmldosage_dec,
                                 a.itemipunit_chr, a.opchargeflg_int, a.packqty_dec, a.dosage_dec,
                                 a.itemcode_vchr, 100 as precent_dec, y.typename_vchr as ybtypename,
                                 tp.qty_dec as recqty, tp.usageid_vchr as usageid_chr, tp.class_int as recclass, b.medicineid_chr  
                            from t_bse_chargeitem a,
                                 t_bse_medicine b,
                                 t_bse_usagetype c,
                                 t_aid_medicaretype y,
                                 t_aid_concertrecipe_plus tp 
                           where a.itemsrcid_vchr = b.medicineid_chr                            
                             and a.insurancetype_vchr = y.typeid_chr(+)      
                             and tp.itemid_chr = a.itemid_chr
                             and tp.usageid_vchr = c.usageid_chr(+)
                             and tp.type_int = 0
                             and tp.class_int = 2 
                             and tp.recipeid_chr = ?
                        order by tp.rowno_int";
            }
            else if (ClassID == 3 || ClassID == 4 || ClassID == 5)
            {
                SQL = @"select   a.orderdicid_chr, a.name_chr, a.des_vchr, a.usercode_chr,
                                 a.wbcode_chr, a.pycode_chr, a.execdept_chr, a.ordercateid_chr,
                                 a.itemid_chr, a.nullitemdosageunit_chr, a.nullitemuseunit_chr,
                                 a.nullitemdosetypeid_chr, a.status_int, a.sampleid_vchr,
                                 a.engname_vchr, a.commname_vchr, a.nullitemfreqid_vchr,
                                 a.nullitemuse_dec, a.lisapplyunitid_chr, a.applytypeid_chr,
                                 a.newchargetype_int, d.sample_type_desc_vchr, f.partname,
                                 h.itemspec_vchr, h.itemopinvtype_chr, h.ybtypename, h.itemunit,
                                 h.ifstop_int, h.itemcode_vchr, tp.qty_dec as recqty, 
                                 tp.usageid_vchr as usageid_chr, tp.class_int as recclass,
                                 tp.sampleid_chr as sample_type_id_chr, tp.partid_int as partid_vchr, 0 as noqtyflag_int 
                            from t_bse_bih_orderdic a,
                                 t_aid_lis_apply_unit c,
                                 t_aid_lis_sampletype d,
                                 ar_apply_partlist f,
                                 (select a.orderdicid_chr, c.itemspec_vchr, c.itemopinvtype_chr,
                                         c.typename_vchr as ybtypename, c.ifstop_int, c.itemcode_vchr, 
                                         (case c.ipchargeflg_int
                                             when 1
                                                then c.itemipunit_chr
                                             else c.itemunit_chr
                                          end
                                         ) as itemunit
                                    from t_bse_bih_orderdic a,
                                         (select a.itemid_chr, a.itemspec_vchr, a.itemopinvtype_chr,
                                                 b.typename_vchr, a.ipchargeflg_int, a.itemipunit_chr,
                                                 a.itemunit_chr, a.ifstop_int, a.itemcode_vchr 
                                            from t_bse_chargeitem a, t_aid_medicaretype b
                                           where a.inpinsurancetype_vchr = b.typeid_chr(+)) c
                                   where a.itemid_chr = c.itemid_chr(+)) h,
                                 t_aid_concertrecipe_plus tp
                           where a.applytypeid_chr = c.apply_unit_id_chr(+)
                             and a.orderdicid_chr = h.orderdicid_chr
                             and a.status_int = 1         
                             and tp.itemid_chr = a.orderdicid_chr
                             and tp.sampleid_chr = d.sample_type_id_chr(+)
                             and tp.partid_int = f.partid(+)
                             and tp.type_int = 1
                             and tp.class_int = ?
                             and tp.recipeid_chr = ?
                        order by tp.rowno_int";
            }
            else if (ClassID == 6)
            {
                SQL = @"select   a.itemid_chr, a.itemname_vchr, a.itemspec_vchr, a.itemopinvtype_chr,
                                 a.itemcode_vchr, 100 as precent_dec, a.itemcommname_vchr, 0 as noqtyflag_int, 
                                 b.deptprep_int, y.typename_vchr as ybtypename, 0 as ifstop_int, 
                                 a.itemunit_chr as itemopunit_chr, a.itemprice_mny, a.selfdefine_int,
                                 a.itempycode_chr as type, a.itemengname_vchr, tp.qty_dec as recqty,
                                 tp.class_int as recclass, b.medicineid_chr     
                            from t_bse_chargeitem a,
                                 t_bse_medicine b,
                                 t_aid_medicaretype y,
                                 t_aid_concertrecipe_plus tp 
                           where a.itemsrcid_vchr = b.medicineid_chr(+)                            
                             and a.insurancetype_vchr = y.typeid_chr(+)
                             and tp.itemid_chr = a.itemid_chr
                             and tp.type_int = 0
                             and tp.class_int = 6  
                             and tp.recipeid_chr = ?
                        order by tp.rowno_int";
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (ClassID == 3 || ClassID == 4 || ClassID == 5)
                {
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = ClassID;
                    ParamArr[1].Value = RecipeID;
                }
                else
                {
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RecipeID;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);

                if (ClassID == 1 || ClassID == 2 || ClassID == 6)
                {
                    string medId = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["medicineid_chr"] == DBNull.Value) continue;
                        medId += "'" + dr["medicineid_chr"].ToString() + "',";
                    }
                    if (medId != string.Empty)
                    {
                        medId = medId.TrimEnd(',');
                        string Sql = string.Empty;
                        DataTable dtTmp = null;
                        DataRow[] drr = null;
                        Sql = @"select ds.medicineid_chr, sum(ds.ifstop_int) as ifStop from t_ds_storage ds where ds.medicineid_chr in ({0}) group by ds.medicineid_chr";
                        Sql = string.Format(Sql, medId);
                        objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dtTmp);
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["medicineid_chr"] == DBNull.Value) continue;
                            drr = dtTmp.Select("medicineid_chr = '" + dr["medicineid_chr"].ToString() + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                dr["ifstop_int"] = ((Convert.ToDecimal(drr[0]["ifStop"].ToString()) > 0) ? 1 : 0);
                            }
                        }

                        Sql = @"select ds.medicineid_chr, sum(ds.noqtyflag_int) as noQty from t_ds_storage ds where ds.medicineid_chr in ({0}) group by ds.medicineid_chr";
                        Sql = string.Format(Sql, medId);
                        objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dtTmp);
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["medicineid_chr"] == DBNull.Value) continue;
                            drr = dtTmp.Select("medicineid_chr = '" + dr["medicineid_chr"].ToString() + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                dr["noqtyflag_int"] = ((Convert.ToDecimal(drr[0]["noQty"].ToString()) > 0) ? 1 : 0);
                            }
                        }
                    }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="RecipeID"></param>
        [AutoComplete]
        public long m_lngDelAccordRecipe(string RecipeID)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @"delete from t_aid_concertrecipe a where a.recipeid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RecipeID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"delete from t_aid_concertrecipedept a where a.recipeid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RecipeID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"delete from t_aid_concertrecipe_plus a where a.recipeid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RecipeID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region �ж��Ƿ�Ϊ��Ч�Һ�
        /// <summary>
        /// �ж��Ƿ�Ϊ��Ч�Һ�
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DoctID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnIsAvailRegister(string RegID, string DoctID)
        {
            long lngRes = 0;
            string SQL = "";

            bool ret = false;
            DataTable dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                int TimeInterval = 0;
                DataTable dt1 = new DataTable();
                SQL = @"select setstatus_int from t_sys_setting where setid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = "0067";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt1, ParamArr);
                if (lngRes > 0 && dt1.Rows.Count == 1)
                {
                    string s = dt1.Rows[0][0].ToString().Trim();
                    if (s != "" && Convert.ToInt32(s) > 0)
                    {
                        TimeInterval = Convert.ToInt32(s);
                    }
                }

                SQL = @"select  count (a.registerid_chr) as nums
                          from t_opr_patientregister a
                         where a.registerid_chr = ? 
                           and (a.takedoctor_chr = ? or a.takedoctor_chr is null)
                           and (sysdate between recorddate_dat and (recorddate_dat + ?/24))";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = RegID;
                ParamArr[1].Value = DoctID;
                ParamArr[2].Value = TimeInterval;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                    {
                        ret = true;

                        SQL = @"update t_opr_patientregister a
                                   set a.takedoctor_chr = ? 
                                 where a.registerid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = DoctID;
                        ParamArr[1].Value = RegID;

                        long lngAffects = 0;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return ret;
        }
        #endregion

        #region ϵͳ�ڲ����뵥����
        /// <summary>
        /// ϵͳ�ڲ����뵥����
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAPPLY_RLT(out System.Collections.Generic.Dictionary<string, string> objArr)
        {
            objArr = null;
            DataTable dt = new DataTable();
            long lngRes = -1;
            string strSQL = "select a.attachtype_int, a.typeid from T_AID_APPLY_RLT a where a.status_int = 0";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                objHRPSvc.Dispose();
                if (lngRes > 0)
                {
                    int intRowCount = dt.Rows.Count;
                    DataRow dr;
                    objArr = new System.Collections.Generic.Dictionary<string, string>(intRowCount);
                    for (int i1 = 0; i1 < intRowCount; i1++)
                    {
                        dr = dt.Rows[i1];
                        objArr.Add(dr["typeid"].ToString(), dr["attachtype_int"].ToString());
                    }
                }
                dt.Dispose();
                dt = null;
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ������ȡ����
        /// <summary>
        /// ������ȡ����
        /// </summary>
        /// <param name="p_strPayTypeID"></param>
        /// <param name="p_strItemIDArr"></param>
        /// <param name="p_hasItemScale"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemScaleByArr(string p_strPayTypeID, string[] p_strItemIDArr, ref Dictionary<string, string> p_hasItemScale)
        {
            long lngRes = 0;

            System.Text.StringBuilder sbSub = new System.Text.StringBuilder();
            for (int i = 0; i < p_strItemIDArr.Length; i++)
            {
                sbSub.Append("'").Append(p_strItemIDArr[i]).Append("',");
                //strSub += "'" + p_strItemIDArr[i] + "',";
            }
            //strSub = strSub.Substring(0, strSub.Length - 1);
            sbSub.Length = sbSub.Length - 1;
            string strSub = sbSub.ToString();
            sbSub = null;

            string SQL = @"select a.itemid_chr, b.precent_dec 
                              from t_bse_chargeitem a, t_aid_inschargeitem b
                             where a.itemid_chr = b.itemid_chr
                               and a.itemid_chr in (" + strSub + @")
                               and b.copayid_chr = ?";



            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strPayTypeID;

                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);

                DataRow dr = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];

                    if (!p_hasItemScale.ContainsKey(dr["itemid_chr"].ToString()))
                    {
                        p_hasItemScale.Add(dr["itemid_chr"].ToString(), dr["precent_dec"].ToString());
                    }
                }

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;



        }

        #endregion

        #region �жϵ�һ�ξ���
        /// <summary>
        /// �жϵ�һ�ξ���
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckFirstDiag(string p_strPatientID)
        {
            long lngRes = 0;
            bool blnIsFirst = false;
            string strSQL = @"select a.outpatrecipeid_chr
  from t_opr_outpatientrecipe a
 where a.pstauts_int = 2
   and a.createdate_dat = trunc(sysdate)      
   and a.recipeflag_int = 1
   and a.patientid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                DataTable dtbTemp = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbTemp != null)
                {
                    if (dtbTemp.Rows.Count == 0)
                        blnIsFirst = true;
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return blnIsFirst;
        }
        #endregion

        #region ��ȡ����������Ϣ
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="p_strRecipeID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeMainInfo(string p_strRecipeID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            string SQL = @"select pstauts_int, patientid_chr, diagdr_chr, registerid_chr, recipeflag_int from t_opr_outpatientrecipe where outpatrecipeid_chr = ?";
            p_dtResult = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strRecipeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref p_dtResult, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�����Ĵ������ͣ�0Ϊҽ����1Ϊ�շ�Ա
        /// <summary>
        /// ��ȡ�����Ĵ������ͣ�0Ϊҽ����1Ϊ�շ�Ա
        /// </summary>
        /// <param name="p_createtype">���ش����Ĵ�������</param>
        /// <param name="p_strRecipeNO">������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecipeCreateType(ref int p_createtype, string p_strRecipeNO)
        {
            long lngRes = -1;
            string SQL = @"select r.createtype_int
                              from t_opr_outpatientrecipe r
                             where r.outpatrecipeid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].DbType = DbType.String;
                param[0].Value = p_strRecipeNO;
                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
                if (dt.Rows.Count > 0)
                {
                    p_createtype = Convert.ToInt32(dt.Rows[0][0]);
                }
                else
                {
                    lngRes = -1;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡָ�����ŵ�ҽ������Ƿ����Ϣ
        /// <summary>
        /// ��ȡָ�����ŵ�ҽ������Ƿ����Ϣ
        /// </summary>
        /// <param name="p_strCard"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVipDebtByCard(string p_strCard, out DataTable p_dtResult)
        {
            long lngRes = 0;
            //            string SQL = @"select a.patientname_chr,
            //                                   a.invoiceno_vchr,
            //                                   d.patientcardid_chr,
            //                                   b.sex_chr,
            //                                   a.outpatrecipeid_chr,
            //                                   b.idcard_chr,
            //                                   a.deptname_chr,
            //                                   b.mobile_chr,
            //                                   a.totalsum_mny
            //                              from t_opr_outpatientrecipeinv a, t_bse_patient b, t_bse_patientcard d
            //                             where a.recorddate_dat < to_date(?, 'yyyy-mm-dd hh24:mi:ss')
            //                               and a.patientid_chr = b.patientid_chr
            //                               and a.patientid_chr = d.patientid_chr
            //                               and a.isvouchers_int = 2
            //                               and d.patientcardid_chr = ?
            //                               and not exists (select t.invoiceno_vchr
            //                                      from t_opr_outpatientrecipeinv t
            //                                     where t.isvouchers_int = 2
            //                                       and t.status_int = 2
            //                                       and a.invoiceno_vchr = t.invoiceno_vchr)
            //                                and exists
            //                             (select t.outpatrecipeid_chr
            //                                      from t_opr_outpatientrecipe t
            //                                     where t.pstauts_int <> -1
            //                                       and a.outpatrecipeid_chr = t.outpatrecipeid_chr)";
            string SQL = @"select a.patientname_chr,
                               a.invoiceno_vchr,
                               d.patientcardid_chr,
                               b.sex_chr,
                               a.outpatrecipeid_chr,
                               b.idcard_chr,
                               a.deptname_chr,
                               b.mobile_chr,
                               e.totalsum_mny
                          from t_opr_outpatientrecipeinv a,
                               t_bse_patient b,
                               t_bse_patientcard d,
                               (select id, sum(tolprice_mny) totalsum_mny
                                  from (select m.outpatrecipeid_chr id, m.tolprice_mny --ҩƷ��
                                          from (select a.tolprice_mny,
                                                       a.outpatrecipeid_chr,
                                                       'WM' medtype
                                                  from t_tmp_outpatientpwmrecipede a,
                                                       t_opr_outpatientrecipeinv   b
                                                 where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                                   and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                                                       and b.isvouchers_int = 2
                                                union all
                                                select a.tolprice_mny,
                                                       a.outpatrecipeid_chr,
                                                       'CM' medtype
                                                  from t_tmp_outpatientcmrecipede a,
                                                       t_opr_outpatientrecipeinv  b
                                                 where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                                   and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                                                       and b.isvouchers_int = 2
                                                union all
                                                select a.tolprice_mny,
                                                       a.outpatrecipeid_chr,
                                                       'QTH' medtype
                                                  from t_tmp_outpatientothrecipede a,
                                                       t_opr_outpatientrecipeinv   b
                                                 where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                                  and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                                                       and b.isvouchers_int = 2) m,
                                               (select a.outpatrecipeid_chr,
                                                       c.medicnetype_int,
                                                       decode(c.medicnetype_int,
                                                              1,
                                                              'WM',
                                                              2,
                                                              'CM',
                                                              3,
                                                              'QTH',
                                                              4,
                                                              '') as medtype,
                                                       decode(b.pstatus_int, 3, 1, 0) as issendmed
                                                  from t_opr_recipesendentry a,
                                                       t_opr_recipesend      b,
                                                       t_bse_medstore        c
                                                 where a.sid_int = b.sid_int
                                                   and b.medstoreid_chr = c.medstoreid_chr
                                                   and not exists (select t.outpatrecipeid_chr
                                                          from t_opr_returnmed t
                                                         where t.outpatrecipeid_chr = a.outpatrecipeid_chr)) n
                                         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
                                           and m.medtype = n.medtype
                                           and n.issendmed = 1
                                        union all
                                        select t.outpatrecipeid_chr id, 0 tolprice_mny --���顢��顢����������
                  from  t_opr_itemconfirm t
                         where t.status_int = 1) t
                                 group by t.id) e
                         where a.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                           and a.patientid_chr = b.patientid_chr
                           and a.patientid_chr = d.patientid_chr
                           and a.outpatrecipeid_chr = e.id
                           and a.isvouchers_int = 2
                           and d.patientcardid_chr = ?
                           and not exists (select t.invoiceno_vchr
                                  from t_opr_outpatientrecipeinv t
                                 where t.isvouchers_int = 2
                                   and t.status_int = 2
                                   and a.invoiceno_vchr = t.invoiceno_vchr)";

            p_dtResult = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                string strDate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                string strStart = "2014-03-10 00:00:00";
                objHRPSvc.CreateDatabaseParameter(9, out ParamArr);
                ParamArr[0].Value = strStart;
                ParamArr[1].Value = strDate;
                ParamArr[2].Value = strStart;
                ParamArr[3].Value = strDate;
                ParamArr[4].Value = strStart;
                ParamArr[5].Value = strDate;
                ParamArr[6].Value = strStart;
                ParamArr[7].Value = strDate;
                ParamArr[8].Value = p_strCard;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref p_dtResult, ParamArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetPatientWeight(string pid)
        {
            string weight = string.Empty;
            string Sql = @"select t.weight from t_bse_patient t where t.patientid_chr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = pid;
                DataTable dt = new DataTable();
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                svc = null;
                if (dt.Rows.Count > 0)
                {
                    weight = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return weight;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="weight"></param>
        [AutoComplete]
        public void UpdatePatientWeight(string pid, string weight)
        {
            long lngAffects = 0;
            string Sql = @"update t_bse_patient set weight = ? where patientid_chr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = weight;
                parm[1].Value = pid;
                svc.lngExecuteParameterSQL(Sql, ref lngAffects, parm);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #region ��ȡ���ƶ�ҩƷ
        /// <summary>
        /// ��ȡ���ƶ�ҩƷ
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetProduceDrugs()
        {
            DataTable dt = null;
            clsHRPTableService svc = null;
            string Sql = @"select a.medicineid_chr, a.medicinename_vchr, b.itemid_chr
                             from t_bse_medicine a
                            inner join t_bse_chargeitem b
                               on a.medicineid_chr = b.itemsrcid_vchr
                            where a.ifstop_int = 0
                              and b.ifstop_int = 0
                              and a.isproducedrugs = 1";
            try
            {
                svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                svc.Dispose();
                svc = null;
            }
            return dt;
        }
        #endregion

        #region ���պ�Դ���´���ID
        /// <summary>
        /// ���պ�Դ����
        /// </summary>
        /// <param name="doctId"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool UpdateTodayPlatformRecipe(string doctId, string recipeId)
        {
            bool ret = false;
            string Sql = string.Empty;
            clsHRPTableService svc = null;
            try
            {
                Sql = @"select a.serNo,
                               a.deptCode,
                               a.deptName,
                               a.doctCode,
                               a.doctName,
                               a.regDate,
                               a.startTime,
                               a.endTime,
                               a.subscribeNo,
                               a.recipeId,
                               a.sendDate
                          from opRegPlatformLog a
                         inner join t_bse_employee b
                            on a.doctcode = b.empno_chr
                         where a.regdate = ?
                           and a.recipeid is null
                           and b.empid_chr = ?
                         order by a.subscribeno";

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = DateTime.Now.ToString("yyyy-MM-dd");
                parm[1].Value = doctId;
                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal serNo = 0;
                    DateTime dtmNow = DateTime.Now;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToDateTime(dr["regDate"].ToString() + " " + dr["startTime"].ToString()) <= dtmNow &&
                            dtmNow <= Convert.ToDateTime(dr["regDate"].ToString() + " " + dr["endTime"].ToString()))
                        {
                            serNo = Convert.ToDecimal(dr["serNo"].ToString());
                            break;
                        }
                    }
                    if (serNo == 0) serNo = Convert.ToDecimal(dt.Rows[0]["serNo"].ToString());

                    Sql = @"update opRegPlatformLog set recipeId = ? where serNo = ?";
                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = recipeId;
                    parm[1].Value = serNo;
                    long affectRows = 0;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    if (affectRows > 0)
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception objEx)
            {
                ret = false;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                svc.Dispose();
                svc = null;
            }
            return ret;
        }
        #endregion

        #region ���պ�Դ���·���ʱ��
        /// <summary>
        /// ���պ�Դ���·���ʱ��
        /// </summary>
        /// <param name="serNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool UpdateTodayPlatformSendDate(decimal serNo)
        {
            bool ret = false;
            string Sql = string.Empty;
            clsHRPTableService svc = null;
            try
            {
                Sql = @"update opRegPlatformLog set sendDate = sysdate where serNo = ?";
                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = serNo;
                long affectRows = 0;
                svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                if (affectRows > 0)
                {
                    ret = true;
                }
            }
            catch (Exception objEx)
            {
                ret = false;
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                svc.Dispose();
                svc = null;
            }
            return ret;
        }

        #endregion

        #region ��ݸ��ԤԼƽ̨.�ж��Ƿ���ԤԼ��¼
        /// <summary>
        /// ��ݸ��ԤԼƽ̨.�ж��Ƿ���ԤԼ��¼
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <param name="bookingNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsWechatPlatformBooking(string cardNo, string regDate, string recipeId, out decimal bookingNo, out string psOrderNum, out string doctCode)
        {
            bookingNo = 0;
            psOrderNum = string.Empty;
            doctCode = string.Empty;
            string Sql = string.Empty;
            try
            {
                Sql = @"select a.serno,
                               a.deptcode,
                               a.doctcode,
                               a.starttime,
                               a.endtime,
                               a.psordnum,
                               b.deptname_vchr as deptname,
                               c.lastname_vchr as doctname
                          from opregbooking a
                          left join t_bse_deptdesc b
                            on a.deptcode = b.code_vchr
                          left join t_bse_employee c
                            on a.doctcode = c.empno_chr
                         where a.cardno = ?
                           and a.regdate = ?
                           and a.psOrdNum is not null
                           and a.status >= 0";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = cardNo;
                parm[1].Value = regDate;

                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    bookingNo = Convert.ToDecimal(dr["serNo"].ToString());
                    psOrderNum = dr["psOrdNum"].ToString();
                    doctCode = dr["doctCode"].ToString();

                    if (!string.IsNullOrEmpty(recipeId))
                    {
                        Sql = @"insert into opRegPlatformLog
                                  (serNo,
                                   deptCode,
                                   deptName,
                                   doctCode,
                                   doctName,
                                   regDate,
                                   startTime,
                                   endTime,
                                   subscribeNo,
                                   recipeId,
                                   recordDate,
                                   sendDate,
                                   flag)
                                values
                                  (seq_opRegPlatformLog.nextval, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                        int n = -1;
                        long affectRows = 0;
                        svc.CreateDatabaseParameter(12, out parm);
                        parm[++n].Value = dr["deptCode"].ToString();
                        parm[++n].Value = dr["deptname"].ToString();
                        parm[++n].Value = dr["doctcode"].ToString();
                        parm[++n].Value = dr["doctname"].ToString();
                        parm[++n].Value = regDate;
                        parm[++n].Value = dr["starttime"].ToString();
                        parm[++n].Value = dr["endtime"].ToString();
                        parm[++n].Value = dr["psordnum"].ToString();
                        parm[++n].Value = recipeId;
                        parm[++n].Value = DateTime.Now;
                        parm[++n].Value = null;
                        parm[++n].Value = 1;
                        svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    }
                    return true;    // ����ԤԼ
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion

        #region ��ȡ������Һ���˵��
        /// <summary>
        /// ��ȡ������Һ���˵��
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetRecipeIVDRIInstruction(string recipeId)
        {
            string ivdriInstr = string.Empty;
            string Sql = @"select t.ivdriinstruction from t_opr_outpatientrecipe t where t.outpatrecipeid_chr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = recipeId;
                DataTable dt = new DataTable();
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                svc = null;
                if (dt.Rows.Count > 0)
                {
                    ivdriInstr = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return ivdriInstr;
        }
        #endregion

        #region ���¾�����Һ���˵��
        /// <summary>
        /// ���¾�����Һ���˵��
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="ivdriInstr"></param>
        [AutoComplete]
        public bool UpdateRecipeIVDRIInstruction(string recipeId, string ivdriInstr)
        {
            long lngAffects = 0;
            string Sql = @"update t_opr_outpatientrecipe set ivdriinstruction = ? where outpatrecipeid_chr = ?";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = ivdriInstr;
                parm[1].Value = recipeId;
                svc.lngExecuteParameterSQL(Sql, ref lngAffects, parm);
                if (lngAffects > 0) return true;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion

        #region ��Ժ֪ͨ��

        #region ��ȡ�������
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetPatientPayType()
        {
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                string Sql = @"select t.paytypeid_chr as paytypeid, t.paytypename_vchr as paytypename
                                  from t_bse_patientpaytype t
                                 where t.isusing_num = 1
                                 order by t.paytypeid_chr";
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRet = objLogger.LogError(ex);
            }
            finally
            {
                svc = null;
            }
            return dt;
        }
        #endregion

        #region ��ȡICD10
        /// <summary>
        /// ��ȡICD10
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetIcd10()
        {
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                string Sql = @"select t.icdcode_chr  as icd10code,
                                       t.icdname_vchr as icd10name,
                                       upper(t.pycode_chr)   as icd10pycode
                                  from t_aid_icd10 t
                                 order by t.icdcode_chr";
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRet = objLogger.LogError(ex);
            }
            finally
            {
                svc = null;
            }
            return dt;
        }
        #endregion

        #region ��ȡ����(����)
        /// <summary>
        /// ��ȡ����(����)
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetDeptDesc()
        {
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                string Sql = @"select t.deptid_chr    as deptid,
                                       t.code_vchr     as deptcode,
                                       t.deptname_vchr as deptname,
                                       t.pycode_chr    as deptpycode
                                  from t_bse_deptdesc t 
                                 where t.attributeid = '0000003'";
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRet = objLogger.LogError(ex);
            }
            finally
            {
                svc = null;
            }
            return dt;
        }
        #endregion

        #region ��ȡסԺRegisterId
        /// <summary>
        /// ��ȡסԺRegisterId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetIpRegisterId(string patientId)
        {
            string registerId = string.Empty;
            DataTable dt = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                string Sql = @"select max(t.registerid_chr) as registerId 
                                  from t_opr_bih_register t
                                 where t.patientid_chr = ?
                                   and t.status_int = 1
                                   and t.pstatus_int <> 3";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = patientId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    registerId = dt.Rows[0]["registerId"].ToString();
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRet = objLogger.LogError(ex);
            }
            return registerId;
        }
        #endregion

        #region ��ȡסԺ�Ǽ���Ϣ
        /// <summary>
        /// ��ȡסԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="patVo"></param>
        /// <param name="bihVo"></param>
        [AutoComplete]
        public void GetRegister(string registerId, out clsPatient_VO patVo, out clsT_Opr_Bih_Register_VO bihVo)
        {
            patVo = null;
            bihVo = null;
            string Sql = string.Empty;
            try
            {
                DataTable dt = null;
                DataRow dr = null;
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"select t.idcard_chr, t.homephone_vchr, t.homeaddress_vchr
                          from t_opr_bih_registerdetail t
                         where t.registerid_chr = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = registerId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    patVo = new clsPatient_VO();
                    patVo.strID_Card = dr["idcard_chr"].ToString();
                    patVo.strHomePhone = dr["homephone_vchr"].ToString();
                    patVo.strHomeAddress = dr["homeaddress_vchr"].ToString();
                }

                Sql = @"select paytypeid_chr,
                               deptid_chr,
                               areaid_chr,
                               state_int,
                               icd10diagid_vchr,
                               icd10diagtext_vchr,
                               inmode,
                               clinicsayprepay,
                               inpatientid_chr,  
                               inpatient_dat 
                          from t_opr_bih_register
                         where registerid_chr = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = registerId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    bihVo = new clsT_Opr_Bih_Register_VO();
                    bihVo.m_strPAYTYPEID_CHR = dr["paytypeid_chr"].ToString();
                    bihVo.m_strDEPTID_CHR = dr["deptid_chr"].ToString();
                    bihVo.m_strAREAID_CHR = dr["areaid_chr"].ToString();
                    bihVo.m_intSTATE_INT = Convert.ToInt32(dr["state_int"].ToString());
                    bihVo.m_strICD10DIAGID_VCHR = dr["icd10diagid_vchr"].ToString();
                    bihVo.m_strICD10DIAGTEXT_VCHR = dr["icd10diagtext_vchr"].ToString();
                    bihVo.m_strCLINICSAYPREPAY = dr["clinicsayprepay"].ToString();
                    bihVo.inMode = Convert.ToInt32(dr["inmode"].ToString());
                    bihVo.m_strINPATIENTID_CHR = dr["inpatientid_chr"].ToString();
                    bihVo.m_strINPATIENT_DAT = Convert.ToDateTime(dr["inpatient_dat"].ToString()).ToString("yyyy-MM-dd HH:mm");
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRet = objLogger.LogError(ex);
            }
        }
        #endregion

        #region ��Ժ�Ǽ�
        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="patVo"></param>
        /// <param name="bihVo"></param>
        /// <param name="regId"></param>
        /// <param name="ipNo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [AutoComplete]
        public int InRegister(clsPatient_VO patVo, clsT_Opr_Bih_Register_VO bihVo, out string regId, out string ipNo, out string error)
        {
            error = string.Empty;
            regId = string.Empty;
            ipNo = string.Empty;
            long affectRows = 0;
            string Sql = string.Empty;
            DataTable dt = null;
            DataRow dr = null;
            try
            {
                // var
                int n = 0;
                string registerId = bihVo.m_strREGISTERID_CHR;
                bool isNew = string.IsNullOrEmpty(registerId) ? true : false;
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                #region select

                if (isNew)
                {
                    Sql = @"select a.incount, b.inpatientcount_int, b.registerid_chr
                          from (select count(t1.registerid_chr) incount
                                  from t_opr_bih_register t1
                                 where t1.patientid_chr = ?
                                   and t1.status_int = 1
                                   and t1.pstatus_int <> 3) a,
                               (select max(t2.inpatientcount_int) + 1 as inpatientcount_int,
                                       max(t2.registerid_chr) as registerid_chr
                                  from t_opr_bih_register t2
                                 where t2.patientid_chr = ?
                                   and t2.status_int = 1
                                   and t2.inpatientnotype_int = 1) b";

                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = patVo.strPatientID;
                    parm[1].Value = patVo.strPatientID;
                    svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        if (dr["incount"] != DBNull.Value && Convert.ToInt32(dr["incount"].ToString()) > 0)
                        {
                            error = "�ò����Ѿ���Ժ";
                            return -3;  //�ò����Ѿ���Ժ
                        }
                        else if (dr["inpatientcount_int"] != DBNull.Value && Convert.ToInt32(dr["inpatientcount_int"].ToString()) > 0)
                        {
                            bihVo.m_intINPATIENTCOUNT_INT = Convert.ToInt32(dr["inpatientcount_int"].ToString());
                        }
                        else
                        {
                            bihVo.m_intINPATIENTCOUNT_INT = 1;
                        }
                    }
                }

                // ���ݲ���ID��ȡ����ID
                Sql = @"select a.parentid as deptId 
                          from t_bse_deptdesc a
                         where a.status_int = 1
                           and a.deptid_chr = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = bihVo.m_strAREAID_CHR;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                bihVo.m_strDEPTID_CHR = dt.Rows[0]["deptId"].ToString();

                #endregion

                #region update t_bse_patient

                Sql = @"update t_bse_patient
                           set idcard_chr = ?, homeaddress_vchr = ?, homephone_vchr = ?
                         where patientid_chr = ?";

                svc.CreateDatabaseParameter(4, out parm);
                n = -1;
                parm[++n].Value = patVo.strID_Card;
                parm[++n].Value = patVo.strHomeAddress;
                parm[++n].Value = patVo.strHomePhone;
                parm[++n].Value = patVo.strPatientID;
                svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                #endregion

                if (isNew)
                {
                    #region var

                    svc.m_lngGenerateNewID("T_Opr_Bih_Register", "registerid_chr", out registerId);
                    // סԺ��
                    string headStr = string.Empty;
                    int sour = 0;
                    DateTime dtmNow = DateTime.Now;
                    clsBeINpatientNOSvc ipNoSvc = new clsBeINpatientNOSvc();
                    ipNoSvc.m_lngGetBigPatientIDNor(out headStr, out ipNo, out sour);
                    if (string.IsNullOrEmpty(ipNo))
                    {
                        error = "����סԺ��ʧ�ܡ�";
                        return -99;
                    }

                    regId = registerId;

                    #endregion

                    #region insert into t_opr_bih_register

                    Sql = @"insert into t_opr_bih_register
                                  (registerid_chr,
                                   modify_dat,
                                   patientid_chr,
                                   inpatientid_chr,
                                   inpatient_dat,
                                   deptid_chr,
                                   areaid_chr,
                                   type_int,
                                   inpatientcount_int,
                                   state_int,
                                   status_int,
                                   operatorid_chr,
                                   pstatus_int,
                                   inpatientnotype_int,
                                   inareadate_dat,
                                   mzdoctor_chr,
                                   icd10diagid_vchr,
                                   icd10diagtext_vchr,
                                   isfromclinic,
                                   clinicsayprepay,
                                   paytypeid_chr,
                                   feestatus_int,
                                   casedoctordept_chr,
                                   checkstatus_int,
                                   inmode)
                                values
                                  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                   ?, ?, ?, ?, ? ) ";

                    svc.CreateDatabaseParameter(25, out parm);
                    n = -1;
                    parm[++n].Value = registerId;
                    parm[++n].Value = dtmNow;
                    parm[++n].Value = patVo.strPatientID;
                    parm[++n].Value = ipNo;
                    parm[++n].Value = dtmNow;
                    parm[++n].Value = bihVo.m_strDEPTID_CHR;
                    parm[++n].Value = bihVo.m_strAREAID_CHR;
                    parm[++n].Value = 1;
                    parm[++n].Value = bihVo.m_intINPATIENTCOUNT_INT;
                    parm[++n].Value = bihVo.m_intSTATE_INT;
                    parm[++n].Value = 1;
                    parm[++n].Value = bihVo.m_strOPERATORID_CHR;
                    parm[++n].Value = 0;
                    parm[++n].Value = 1;    // 1=����; 2=���ۺ�
                    parm[++n].Value = null;
                    parm[++n].Value = bihVo.m_strOPERATORID_CHR;
                    parm[++n].Value = bihVo.m_strICD10DIAGID_VCHR;
                    parm[++n].Value = bihVo.m_strICD10DIAGTEXT_VCHR;
                    parm[++n].Value = 1;
                    parm[++n].Value = bihVo.m_strCLINICSAYPREPAY;
                    parm[++n].Value = bihVo.m_strPAYTYPEID_CHR;
                    parm[++n].Value = 0;
                    parm[++n].Value = bihVo.m_strCaseDoctorDept;
                    parm[++n].Value = 0;
                    parm[++n].Value = bihVo.inMode;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    #endregion

                    #region insert into t_opr_bih_registerdetail

                    Sql = @"insert into t_opr_bih_registerdetail
                                  (registerid_chr,
                                   lastname_vchr,
                                   idcard_chr,
                                   married_chr,
                                   birthplace_vchr,
                                   homeaddress_vchr,
                                   sex_chr,
                                   nationality_vchr,
                                   firstname_vchr,
                                   birth_dat,
                                   race_vchr,
                                   nativeplace_vchr,
                                   occupation_vchr,
                                   name_vchr,
                                   homephone_vchr,
                                   officephone_vchr,
                                   insuranceid_vchr,
                                   mobile_chr,
                                   officeaddress_vchr,
                                   employer_vchr,
                                   officepc_vchr,
                                   homepc_chr,
                                   email_vchr,
                                   contactpersonfirstname_vchr,
                                   contactpersonlastname_vchr,
                                   contactpersonaddress_vchr,
                                   contactpersonphone_vchr,
                                   contactpersonpc_chr,
                                   patientrelation_vchr,
                                   firstdate_dat,
                                   isemployee_int,
                                   status_int,
                                   deactivate_dat,
                                   operatorid_chr,
                                   modify_dat,
                                   optimes_int,
                                   govcard_chr,
                                   bloodtype_chr,
                                   ifallergic_int,
                                   allergicdesc_vchr,
                                   difficulty_vchr,
                                   consigneeaddr)
                                  select ?,
                                         t.lastname_vchr,
                                         ?,
                                         t.married_chr,
                                         t.birthplace_vchr,
                                         ?,
                                         t.sex_chr,
                                         t.nationality_vchr,
                                         t.firstname_vchr,
                                         t.birth_dat,
                                         t.race_vchr,
                                         t.nativeplace_vchr,
                                         t.occupation_vchr,
                                         t.name_vchr,
                                         ?,
                                         t.officephone_vchr,
                                         t.insuranceid_vchr,
                                         t.mobile_chr,
                                         t.officeaddress_vchr,
                                         t.employer_vchr,
                                         t.officepc_vchr,
                                         t.homepc_chr,
                                         t.email_vchr,
                                         t.contactpersonfirstname_vchr,
                                         t.contactpersonlastname_vchr,
                                         t.contactpersonaddress_vchr,
                                         t.contactpersonphone_vchr,
                                         t.contactpersonpc_chr,
                                         t.patientrelation_vchr,
                                         t.firstdate_dat,
                                         t.isemployee_int,
                                         t.status_int,
                                         null,
                                         ?,
                                         ?,
                                         t.optimes_int,
                                         t.govcard_chr,
                                         t.bloodtype_chr,
                                         t.ifallergic_int,
                                         t.allergicdesc_vchr,
                                         t.difficulty_vchr,
                                         t.consigneeaddr
                                    from t_bse_patient t
                                   where t.patientid_chr = ?";

                    svc.CreateDatabaseParameter(7, out parm);
                    n = -1;
                    parm[++n].Value = registerId;
                    parm[++n].Value = patVo.strID_Card;
                    parm[++n].Value = patVo.strHomeAddress;
                    parm[++n].Value = patVo.strHomePhone;
                    parm[++n].Value = bihVo.m_strOPERATORID_CHR;
                    parm[++n].Value = dtmNow;
                    parm[++n].Value = patVo.strPatientID;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    #endregion

                    #region insert into t_bse_hisemr_relation

                    Sql = @"insert into t_bse_hisemr_relation
                              (registerid_chr,
                               emrinpatientid,
                               emrinpatientdate,
                               hisinpatientid_chr,
                               hisinpatientdate,
                               operatorid_chr,
                               operat_dat)
                            values
                              (?, ?, ?, ?, ?, ?, ?)";

                    svc.CreateDatabaseParameter(7, out parm);
                    n = -1;
                    parm[++n].Value = registerId;
                    parm[++n].Value = ipNo;
                    parm[++n].Value = dtmNow;
                    parm[++n].Value = ipNo;
                    parm[++n].Value = dtmNow;
                    parm[++n].Value = bihVo.m_strOPERATORID_CHR;
                    parm[++n].Value = dtmNow;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    #endregion

                    #region insert into t_opr_bih_transfer

                    Sql = @"insert into t_opr_bih_transfer
                              (transferid_chr,
                               sourcedeptid_chr,
                               sourceareaid_chr,
                               sourcebedid_chr,
                               targetdeptid_chr,
                               targetareaid_chr,
                               targetbedid_chr,
                               type_int,
                               des_vchr,
                               operatorid_chr,
                               registerid_chr,
                               modify_dat)
                            values
                              (lpad(seq_opr_bih_transfer.nextval, 12, '0'),
                               null, null, null, ?, ?,
                               null, 5, null, ?, ?, ?)";

                    svc.CreateDatabaseParameter(5, out parm);
                    n = -1;
                    parm[++n].Value = bihVo.m_strDEPTID_CHR;
                    parm[++n].Value = bihVo.m_strAREAID_CHR;
                    parm[++n].Value = bihVo.m_strOPERATORID_CHR;
                    parm[++n].Value = registerId;
                    parm[++n].Value = dtmNow;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    #endregion

                    #region update max ipno

                    ipNoSvc.m_lngAddBigIDTableMax(1, headStr, ipNo, sour);

                    #endregion

                    #region insert into t_opr_bih_patientstate

                    Sql = @"insert into t_opr_bih_patientstate
                              (seqid_int, registerid_chr, state_int, active_dat, operatorid_chr)
                            values
                              (seq_patientstate.nextval, ?, ?, sysdate, ?)";

                    svc.CreateDatabaseParameter(3, out parm);
                    n = -1;
                    parm[++n].Value = registerId;
                    parm[++n].Value = bihVo.m_intSTATE_INT;
                    parm[++n].Value = bihVo.m_strOPERATORID_CHR;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    #endregion

                }
                else
                {
                    #region update

                    Sql = @"update t_opr_bih_register
                               set paytypeid_chr      = ?,
                                   deptid_chr         = ?,
                                   areaid_chr         = ?, 
                                   state_int          = ?,
                                   icd10diagid_vchr   = ?,
                                   icd10diagtext_vchr = ?,
                                   inmode             = ?,
                                   clinicsayprepay    = ?
                             where registerid_chr = ?";

                    svc.CreateDatabaseParameter(9, out parm);
                    n = -1;
                    parm[++n].Value = bihVo.m_strPAYTYPEID_CHR;
                    parm[++n].Value = bihVo.m_strDEPTID_CHR;
                    parm[++n].Value = bihVo.m_strAREAID_CHR;
                    parm[++n].Value = bihVo.m_intSTATE_INT;
                    parm[++n].Value = bihVo.m_strICD10DIAGID_VCHR;
                    parm[++n].Value = bihVo.m_strICD10DIAGTEXT_VCHR;
                    parm[++n].Value = bihVo.inMode;
                    parm[++n].Value = bihVo.m_strCLINICSAYPREPAY;
                    parm[++n].Value = registerId;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    Sql = @"update t_opr_bih_registerdetail
                               set idcard_chr = ?, homeaddress_vchr = ?, homephone_vchr = ?
                             where registerid_chr = ?";

                    svc.CreateDatabaseParameter(4, out parm);
                    n = -1;
                    parm[++n].Value = patVo.strID_Card;
                    parm[++n].Value = patVo.strHomeAddress;
                    parm[++n].Value = patVo.strHomePhone;
                    parm[++n].Value = registerId;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    #endregion
                }
            }
            catch (Exception objEx)
            {
                affectRows = -99;
                error = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)affectRows;
        }
        #endregion

        #region ȡ����Ժ
        /// <summary>
        /// ȡ����Ժ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="cancelOperId"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [AutoComplete]
        public int CancelRegister(string registerId, string cancelOperId, out string error)
        {
            long affectRows = 0;
            DataTable dt = null;
            string Sql = string.Empty;
            error = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"select t.bedid_chr from t_opr_bih_register t where t.registerid_chr = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = registerId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["bedid_chr"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[0]["bedid_chr"].ToString()))
                    {
                        error = "�Ѱ��Ŵ�λ��ҽ������ȡ����Ժ�Ǽǣ�����ϵסԺ����";
                        return -1;
                    }
                }

                Sql = @"update t_opr_bih_register t
                           set t.status_int = -1, t.cancelerid_chr = ?, t.cancel_dat = sysdate 
                         where t.registerid_chr = ?";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = cancelOperId;
                parm[1].Value = registerId;
                svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

            }
            catch (Exception objEx)
            {
                error = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)affectRows;
        }
        #endregion

        #region ��ȡ������Ժ���
        /// <summary>
        /// ��ȡ������Ժ���
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public string GetTodayInNumber(string ipNo)
        {
            string number = "";
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            Sql = @"select t.registerid_chr, t.inpatientid_chr
                      from t_opr_bih_register t
                     where t.inpatient_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                           to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                     order by t.inpatient_dat";

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            svc.CreateDatabaseParameter(2, out parm);
            parm[0].Value = date + " 00:00:00";
            parm[1].Value = date + " 23:59:59";

            DataTable dt = null;
            svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["inpatientid_chr"].ToString() == ipNo)
                    {
                        number = DateTime.Now.ToString("yyyyMMdd") + Convert.ToString(i + 1).PadLeft(3, '0');
                        break;
                    }
                }
            }
            else
            {
                number = DateTime.Now.ToString("yyyyMMdd") + "001";
            }
            return number;
        }
        #endregion

        #endregion

        #region ���ݴ���ID��ȡ���߳�������
        /// <summary>
        /// ���ݴ���ID��ȡ���߳�������
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime GetBirthdayByRecipeId(string recipeId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            svc.CreateDatabaseParameter(1, out parm);
            parm[0].Value = recipeId;

            Sql = @"select a.outpatrecipeid_chr, b.birth_dat
                      from t_opr_outpatientrecipe a
                     inner join t_bse_patient b
                        on a.patientid_chr = b.patientid_chr
                     where a.outpatrecipeid_chr = ?";

            svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            svc.Dispose();
            svc = null;

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToDateTime(dt.Rows[0]["birth_dat"].ToString());
            else
                return DateTime.Now;
        }
        #endregion
    }

    #region ʱ�任��class
    /// <summary>
    /// ʱ�任��class
    /// </summary>
    public class clsConvertDateTime
    {
        #region ��������
        /// <summary>
        /// �������䣬���ݷ��ص�ֵ�õ����꣬�»���
        /// </summary>
        /// <param name="dteBirth">��������</param>
        /// <param name="intAge">����õ�������</param>
        /// <returns></returns>
        public static Age CalcAge(DateTime dteBirth, out int intAge)
        {
            Age age = Age.Year;
            intAge = 0;
            com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc = new clsOPDoctorPlanSvc();

            DateTime dteNow = DateTime.Now;
            int intYear = dteBirth.Year;
            int intMonth = dteBirth.Month;
            int intDay = dteBirth.Day;

            if ((dteNow.Year - intYear) > 0)
            {
                intAge = dteNow.Year - intYear;
                age = Age.Year;
            }
            else if ((dteNow.Month - intMonth) > 0)
            {
                intAge = dteNow.Month - intMonth;
                age = Age.Month;
            }
            else
            {
                intAge = dteNow.Day - intDay;
                age = Age.Day;
            }

            return age;

        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="dteBirth">��������</param>
        /// <returns></returns>
        internal static string CalcAge(DateTime dteBirth)
        {
            int intAge = 0;
            string strAge = "";
            Age age = Age.Year;
            age = clsConvertDateTime.CalcAge(dteBirth, out intAge);
            switch (age)
            {
                case Age.Year:
                    strAge = intAge.ToString();
                    break;
                case Age.Month:
                    strAge = intAge.ToString() + "����";
                    break;
                case Age.Day:
                    strAge = intAge.ToString() + "��";
                    break;
            }
            return strAge;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public enum Age
        {
            /// <summary>
            /// ��
            /// </summary>
            Year,
            /// <summary>
            /// ��
            /// </summary>
            Month,
            /// <summary>
            /// ��
            /// </summary>
            Day
        }
        #endregion
    }
    #endregion
}
