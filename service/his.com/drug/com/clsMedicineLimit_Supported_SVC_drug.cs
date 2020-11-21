using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ���������������
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsMedicineLimit_Supported_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region ��ȡ���������������
        /// <summary>
        /// ��ȡ���������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">ҩ��ID</param>
        /// <param name="p_strDrugType">ҩƷ����</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��</param>
        /// <param name="p_dtbResult">���������������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetLimitData( string p_strStorageID,string p_strDrugType,bool p_blnIsHospital,ref DataTable p_dtbResult)
        {
            long lngRes = 0;

            //����COM����
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            StringBuilder strSQL = new StringBuilder();
            if (p_blnIsHospital)
            {
                strSQL.Append(@"select b.assistcode_chr,
			 b.medicineid_chr,
			 b.medicinename_vchr,
			 b.medspec_vchr,
             b.productorid_chr,
			 decode(b.ipchargeflg_int, 0, b.opunit_chr, b.ipunit_chr) unit_chr,
			 a.drugstoreid_chr,
			 c.medstorename_vchr,
			 (select currentgross_num
					from (select decode(g.ipchargeflg_int,
															0,
															sum(round(f.iprealgross_int / f.packqty_dec, 2)),
															sum(f.iprealgross_int)) currentgross_num,
											 f.drugstoreid_chr,
											 f.medicineid_chr
									from t_ds_storage_detail f
									left join t_bse_medicine g on g.medicineid_chr =
																								f.medicineid_chr
								 where f.status = 1
								 group by f.drugstoreid_chr,
													g.ipchargeflg_int,
													f.medicineid_chr)
				 where rownum = 1
					 and drugstoreid_chr = ?
					 and medicineid_chr = b.medicineid_chr) realgross_int,
			 a.tiptoplimit_int,
			 a.neaplimit_int
	from t_bse_medicine b
	left join t_ds_medlimit a on a.medicineid_chr = b.medicineid_chr
													 and (a.drugstoreid_chr = ?)
	left join t_bse_medstore c on c.deptid_chr = a.drugstoreid_chr
 where b.medicinetypeid_chr = ? and b.ifstop_int = 0");
            }
            else
            {
                strSQL.Append(@"select b.assistcode_chr,
			 b.medicineid_chr,
			 b.medicinename_vchr,
			 b.medspec_vchr,
             b.productorid_chr,
			 decode(b.opchargeflg_int, 0, b.opunit_chr, b.ipunit_chr) unit_chr,
			 a.drugstoreid_chr,
			 c.medstorename_vchr,
			 (select currentgross_num
					from (select decode(g.opchargeflg_int,
															0,
															sum(round(f.iprealgross_int / f.packqty_dec, 2)),
															sum(f.iprealgross_int)) currentgross_num,
											 f.drugstoreid_chr,
											 f.medicineid_chr
									from t_ds_storage_detail f
									left join t_bse_medicine g on g.medicineid_chr =
																								f.medicineid_chr
								 where f.status = 1
								 group by f.drugstoreid_chr,
													g.opchargeflg_int,
													f.medicineid_chr)
				 where rownum = 1
					 and drugstoreid_chr = ?
					 and medicineid_chr = b.medicineid_chr) realgross_int,
			 a.tiptoplimit_int,
			 a.neaplimit_int
	from t_bse_medicine b
	left join t_ds_medlimit a on a.medicineid_chr = b.medicineid_chr
													 and (a.drugstoreid_chr = ?)
	left join t_bse_medstore c on c.deptid_chr = a.drugstoreid_chr
 where b.medicinetypeid_chr = ? and b.ifstop_int = 0");
            }
            
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strStorageID;
                objDPArr[1].Value = p_strStorageID;
                objDPArr[2].Value = p_strDrugType;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtbResult, objDPArr);
                if(p_dtbResult != null && p_dtbResult.Rows.Count > 0)
                {
                    DataView dvResult = p_dtbResult.DefaultView;
                    dvResult.Sort = "assistcode_chr";
                    p_dtbResult = dvResult.ToTable();
                    p_dtbResult.PrimaryKey = new DataColumn[] { p_dtbResult.Columns["medicineid_chr"] };
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_dtbResult = null;
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����޸ĺ������
        /// <summary>
        /// �����޸ĺ������
        /// </summary>
        /// <param name="p_objLimit">ҩ�����������������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveMedicine(clsDS_MedicineLimit[] p_objLimit)
        {
            long lngRes = -1;
            string strSQLQuery = @"select tiptoplimit_int,neaplimit_int from t_ds_medlimit where drugstoreid_chr = ? and medicineid_chr = ?";
            string strSQLEdit = @"update t_ds_medlimit set tiptoplimit_int = ?,neaplimit_int = ? where drugstoreid_chr = ? and medicineid_chr = ?";
            string strSQLNew = @"insert into t_ds_medlimit (tiptoplimit_int,neaplimit_int,drugstoreid_chr,medicineid_chr) values (?,?,?,?)";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            DataTable dtbResult = new DataTable();
            IDataParameter[] objDPQueryArr = null;
            IDataParameter[] objDPEditArr = null;
            IDataParameter[] objDPNewArr = null;
            for (int iOr = 0; iOr < p_objLimit.Length; iOr++)
            {
                objHRPServ.CreateDatabaseParameter(2, out objDPQueryArr);
                objDPQueryArr[0].Value = p_objLimit[iOr].m_strDrugstoreid_chr;
                objDPQueryArr[1].Value = p_objLimit[iOr].m_strMedicineid_chr;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQLQuery, ref dtbResult, objDPQueryArr);
                if (dtbResult.Rows.Count > 0)
                {
                    objHRPServ.CreateDatabaseParameter(4, out objDPEditArr);
                    objDPEditArr[0].Value = p_objLimit[iOr].m_dblTiptoplimit_int;
                    objDPEditArr[1].Value = p_objLimit[iOr].m_dblNeaplimit_int;
                    objDPEditArr[2].Value = p_objLimit[iOr].m_strDrugstoreid_chr;
                    objDPEditArr[3].Value = p_objLimit[iOr].m_strMedicineid_chr;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQLEdit, ref lngRes, objDPEditArr);
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(4, out objDPNewArr);
                    objDPNewArr[0].Value = p_objLimit[iOr].m_dblTiptoplimit_int;
                    objDPNewArr[1].Value = p_objLimit[iOr].m_dblNeaplimit_int;
                    objDPNewArr[2].Value = p_objLimit[iOr].m_strDrugstoreid_chr;
                    objDPNewArr[3].Value = p_objLimit[iOr].m_strMedicineid_chr;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQLNew, ref lngRes, objDPNewArr);
                }
            }
            objHRPServ.Dispose();
            objHRPServ = null;
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�������Ϣ
        /// <summary>
        /// ��ȡҩƷ�������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAssistCode">��ѯ����</param> 
        /// <param name="p_strDrugType">ҩƷ����</param>
        /// <param name="p_dtbMedicine">���ؽ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBaseMedicine( string p_strAssistCode,string p_strDrugType, out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;
            long lngRes = 0;

            if (p_strAssistCode == null)
            {
                p_strAssistCode = string.Empty;
            }

            try
            {
                string strSQL = @"select t.assistcode_chr,
       t.medicinename_vchr,
       t.medspec_vchr,
       t.opunit_chr,
       t.ipunit_chr,
       t.packqty_dec,
       t.productorid_chr,
       t.pycode_chr,
       t.wbcode_chr,
       t.medicineid_chr,
       t.ispoison_chr,
       t.ischlorpromazine2_chr,
       t.unitprice_mny,
       t.medicinetypeid_chr,
       t.tradeprice_mny,
       t.limitunitprice_mny,
       t.opchargeflg_int,
       t.ipchargeflg_int,
       t.ifstop_int,
       b.itemname_vchr as aliasname_vchr,
       b.pycode_vchr as aliaspycode_vchr,
       b.wbcode_vchr as aliaswbcode_vchr
  from t_bse_medicine t
  left join t_bse_chargeitem a on t.medicineid_chr = a.itemsrcid_vchr
  left join t_bse_itemalias_drug b on a.itemid_chr = b.itemid_chr
                                  and b.status_int = 1
                                  and b.flag_int = 1
 where t.assistcode_chr like ?
    and t.medicinetypeid_chr = ? and  t.deleted_int=0
 order by t.assistcode_chr,t.medicineid_chr";
                
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strAssistCode + "%";
                objDPArr[1].Value = p_strDrugType;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�����������������(סԺҩ��ʹ��)
        /// <summary>
        /// ��ȡҩƷ�����������������(סԺҩ��ʹ��)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">ҩ��ID</param>
        /// <param name="p_dtmStart">��ʼʱ��</param>
        /// <param name="p_dtmEnd">����ʱ��</param>
        /// <param name="p_dtbResult">���������������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetNeapData( string p_strStorageID,DateTime p_dtmStart,DateTime p_dtmEnd, ref DataTable p_dtbResult)
        {
            long lngRes = 0;

            //����COM����
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            
            string strSQL = @"select 'F' ifcheck,
       b.assistcode_chr,
       b.medicineid_chr,
       b.medicinename_vchr,
       b.medspec_vchr,
       b.opunit_chr,
       a.drugstoreid_chr,
       c.medstorename_vchr,
       nvl((decode(b.ipchargeflg_int,
                   0,
                   sum(round(d.iprealgross_int / d.packqty_dec, 2)),
                   sum(d.iprealgross_int))),
           0) currentgross_num,
       a.tiptoplimit_int,
       a.neaplimit_int,
       0 opamount_int,
       b.packqty_dec,
       0 ipamount_int,
       b.ipunit_chr,
       decode(b.ipchargeflg_int, 0, b.opunit_chr, b.ipunit_chr) unit_chr,
       a.tiptoplimit_int amount_int,
       b.opchargeflg_int,
       b.ipchargeflg_int,
       b.productorid_chr,
       b.unitprice_mny,
       (select askdate_dat
          from (select s.askdate_dat, t.medicineid_chr, s.askdept_chr
                  from t_ds_ask_detail t
                  left join t_ds_ask s on s.seriesid_int = t.seriesid2_int
                 where s.status_int = 2
                 order by s.askdate_dat desc)
         where askdept_chr = a.drugstoreid_chr
           and medicineid_chr = a.medicineid_chr
           and rownum = 1) askdate_dat,
       b.medicinetypeid_chr,
       b.requestpackqty_dec,
       b.requestunit_chr,
       0 requestamount_int,
       0 useamount_int
  from t_bse_medicine b
  left join t_ds_medlimit a on a.medicineid_chr = b.medicineid_chr
  left join t_bse_medstore c on c.deptid_chr = a.drugstoreid_chr
  left join t_ds_storage_detail d on d.medicineid_chr = b.medicineid_chr
                                 and d.drugstoreid_chr = c.deptid_chr
 where c.medstoreid_chr = ?
 group by b.assistcode_chr,
          b.medicineid_chr,
          b.medicinename_vchr,
          b.medspec_vchr,
          b.opunit_chr,
          a.drugstoreid_chr,
          c.medstorename_vchr,
          b.opchargeflg_int,
          b.ipchargeflg_int,
          a.tiptoplimit_int,
          a.neaplimit_int,
          a.tiptoplimit_int,
          b.ipunit_chr,
          b.packqty_dec,
          b.opchargeflg_int,
          b.productorid_chr,
          b.unitprice_mny,
          b.medicinetypeid_chr,
          b.requestpackqty_dec,
          b.requestunit_chr,
          a.medicineid_chr";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStorageID;                

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
                if (p_dtbResult != null && p_dtbResult.Rows.Count > 0)
                {
                    DataTable dtbTemp = new DataTable();
                    strSQL = @"select a.medicineid_chr,
       nvl(sum(decode(a.type_int,
                      1,
                      decode(b.ipchargeflg_int,
                             0,
                             a.opamount_int,
                             a.ipamount_int),
                      2,
                      -decode(b.ipchargeflg_int,
                              0,
                              a.opamount_int,
                              a.ipamount_int))),
           0) useamount_int
  from t_ds_putmedaccount_detail a
  left join t_bse_medicine b on b.medicineid_chr = a.medicineid_chr
  left join t_bse_medstore c on c.deptid_chr = a.drugstoreid_int
 where c.medstoreid_chr = ?
   and a.state_int <> 0
   and a.operatedate_dat between ? and ?
 group by a.medicineid_chr";
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                    objDPArr[1].Value = p_dtmStart;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmEnd;
                    objDPArr[2].DbType = DbType.DateTime;
                    objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                    if (dtbTemp != null && dtbTemp.Rows.Count > 0)
                    {
                        dtbTemp.PrimaryKey = new DataColumn[] { dtbTemp.Columns["medicineid_chr"] };
                    }

                    DataRow drRow = null;
                    DataRow drTemp = null;
                    for (int i1 = 0; i1 < p_dtbResult.Rows.Count; i1++)
                    {
                        drRow = p_dtbResult.Rows[i1];
                        if (dtbTemp != null && dtbTemp.Rows.Count > 0)
                        {
                            drTemp = dtbTemp.Rows.Find(drRow["medicineid_chr"]);
                            if (drTemp != null)
                            {
                                drRow["useamount_int"] = drTemp["useamount_int"];
                            }
                        }
                        if(Convert.ToDouble(drRow["requestpackqty_dec"]) > 0)
                        {                            
                            if (Convert.ToInt16(drRow["ipchargeflg_int"]) == 0)
                            {
                                drRow["requestamount_int"] = Math.Ceiling(Convert.ToDouble(drRow["amount_int"]) / Convert.ToDouble(drRow["requestpackqty_dec"]));
                                drRow["opamount_int"] = Convert.ToDouble(drRow["requestamount_int"]) * Convert.ToDouble(drRow["requestpackqty_dec"]);
                                drRow["ipamount_int"] = Convert.ToDouble(drRow["opamount_int"]) * Convert.ToDouble(drRow["packqty_dec"]);
                                drRow["amount_int"] = Convert.ToDouble(drRow["opamount_int"]);
                            }
                            else
                            {
                                drRow["requestamount_int"] = Math.Ceiling((Convert.ToDouble(drRow["amount_int"]) / Convert.ToDouble(drRow["packqty_dec"])) / Convert.ToDouble(drRow["requestpackqty_dec"]));
                                drRow["opamount_int"] = Convert.ToDouble(drRow["requestamount_int"]) * Convert.ToDouble(drRow["requestpackqty_dec"]);
                                drRow["ipamount_int"] = Convert.ToDouble(drRow["opamount_int"]) * Convert.ToDouble(drRow["packqty_dec"]);
                                drRow["amount_int"] = Convert.ToDouble(drRow["ipamount_int"]);
                            }
                        }                        
                    }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_dtbResult = null;
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ����
        /// <summary>
        /// ��ȡҩƷ�������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">���ؽ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineType(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select medicinetypeid_chr,medicinetypename_vchr from t_aid_medicinetype";

                clsHRPTableService objHRPServ = new clsHRPTableService();                
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
#endregion

        //���淽���൥λά��ʹ��
        #region ��ȡҩƷ��Ϣ
        /// <summary>
        /// ��ȡҩƷ��Ϣ
        /// </summary>
        /// <param name="p_dtMedicineList">ҩƷ��Ϣ�б�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTableMedicineList(ref DataTable p_dtMedicineList)
        {
            long lngRes = -1;

            string strSQL = @"select a.itemid_chr,
                                     a.itemcode_vchr,
                                     a.itempycode_chr,
                                     a.itemwbcode_chr,
                                     a.itemname_vchr,
                                     a.itemcommname_vchr,
                                     a.itemengname_vchr
                                from t_bse_chargeitem a, t_bse_medicine b
                               where a.itemsrcid_vchr = b.medicineid_chr
                                 and b.multiunitflag_int = 1
                                 and a.ifstop_int = 0";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtMedicineList);
                int x = p_dtMedicineList.Rows.Count;
                objHRPSvc.Dispose();
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

        #region ����ҩƷId��ȡ��Ӧ�ĵ�λ�б�
        /// <summary>
        /// ����ҩƷId��ȡ��Ӧ�ĵ�λ�б�
        /// </summary>
        /// <param name="p_strMedId">ҩƷId</param>
        /// <param name="p_dtMultiUnit"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTableMultiUnitList(string p_strMedId, out DataTable p_dtMultiUnit)
        {
            long lngRes = -1;
            p_dtMultiUnit = null;

            string strSQL = @"select a.itemid_chr,
       a.unit_vchr,
       a.package_dec,
       case
         when a.curruseflag_int = 0 then
          '��'
         else
          '��'
       end as curruseflag_int,
       case
         when a.status_int = 0 then
          'ͣ��'
         else
          '����'
       end as status_int
  from t_bse_itemmultiunit_drug a, t_bse_chargeitem b
 where b.itemsrcid_vchr = ?
   and a.itemid_chr = b.itemid_chr ";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strMedId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtMultiUnit, ParamArr);

                objHRPSvc.Dispose();
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

        #region ����������ѯ��λ
        /// <summary>
        /// ����������ѯ��λ
        /// </summary>
        /// <param name="strSeledMedId">ҩƷID </param>
        /// <param name="p_strUnit">��λ����</param>
        /// <param name="p_intPackage_Dec">����</param>
        /// <param name="p_CurruseFlag_Int">�Ƿ�ǰ��λ���</param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnQueryByIndex(string strSeledMedId, string p_strUnit, int p_intPackage_Dec, int p_CurruseFlag_Int)
        {
            bool blnIsFind = false;

            string strSQL = @"select a.itemid_chr, a.unit_vchr,a.status_int
                                  from t_bse_itemmultiunit_drug a
                                 where a.itemid_chr = ?
                                   and a.unit_vchr = ?
                                   and a.package_dec = ?
                                   and a.curruseflag_int = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = strSeledMedId;
                ParamArr[1].Value = p_strUnit;
                ParamArr[2].DbType = DbType.Int16;
                ParamArr[2].Value = p_intPackage_Dec;
                ParamArr[3].DbType = DbType.Int16;
                ParamArr[3].Value = p_CurruseFlag_Int;

                DataTable dtValue = new DataTable();
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtValue, ParamArr);
                if (dtValue != null && dtValue.Rows.Count > 0)
                {
                    blnIsFind = true;
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return blnIsFind;
        }
        #endregion

        #region ����������ѯ�Ƿ�Ϊ��ǰʹ�õ�λ
        /// <summary>
        /// ����������ѯ�Ƿ�Ϊ��ǰʹ�õ�λ
        /// </summary>
        /// <param name="strSeledMedId">ҩƷID </param>
        /// <param name="p_strUnit">��λ����</param>
        /// <param name="p_intPackage_Dec">����</param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnQueryByIndex(string strSeledMedId, string p_strUnit, int p_intPackage_Dec)
        {
            bool blnIsFind = false;

            string strSQL = @"select a.itemid_chr,a.unit_vchr 
                                    from  t_bse_itemmultiunit_drug a
                                    where a.itemid_chr= ?
		                            and a.unit_vchr= ?
                                    and a.package_dec=?
                                    and a.curruseflag_int=1";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = strSeledMedId;
                ParamArr[1].Value = p_strUnit;
                ParamArr[2].DbType = DbType.Int16;
                ParamArr[2].Value = p_intPackage_Dec;

                DataTable dtValue = new DataTable();
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtValue, ParamArr);
                if (dtValue != null && dtValue.Rows.Count > 0)
                {
                    blnIsFind = true;
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return blnIsFind;
        }
        #endregion     

        #region ����ҩƷId��ȡ��Ӧ���շ���ĿID
        /// <summary>
        /// ����ҩƷId��ȡ��Ӧ���շ���ĿID
        /// </summary>
        /// <param name="p_strMedicineID">ҩƷId</param>
        /// <param name="p_strItemID">��ӦChargeItemID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemID(string p_strMedicineID, out string p_strItemID)
        {
            p_strItemID = string.Empty;
            long lngRes = -1;
            DataTable p_dtbTemp = null;

            string strSQL = @"select a.itemid_chr from t_bse_chargeitem a where a.itemsrcid_vchr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strMedicineID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbTemp, ParamArr);
                if (p_dtbTemp != null && p_dtbTemp.Rows.Count > 0)
                {
                    p_strItemID = p_dtbTemp.Rows[0][0].ToString();
                }
                objHRPSvc.Dispose();
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
    }
}
