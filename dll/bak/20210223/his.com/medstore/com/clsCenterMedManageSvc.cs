using System;
using System.EnterpriseServices;
using System.Data;
using System.Text;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCenterMedManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 常量

        private const string SQL_SELECT = @"select a.putmeddetailid_chr,
                                                   a.areaid_chr,
                                                   a.medid_chr,
                                                   a.recipeno_int,
                                                   a.exectimes_int,
                                                   a.unitprice_mny,
                                                   a.unit_vchr,
                                                   a.get_dec,
                                                   a.isput_int,
                                                   a.puttype_int,
                                                   a.putmedreqid_chr,
                                                   a.exectime_vchr,
                                                   a.pubdate_dat,
                                                   a.isrecruit_int,
                                                   a.dosage_dec,
                                                   a.dosageunit_vchr,
                                                   a.execdays_int,
                                                   a.outgetmeddays_int,
                                                   a.orderexectype_int,
                                                   a.activatetype_int,
                                                   (' ' || b.assistcode_chr || '               |' || a.chargeitemid_chr) as assistcode_chr,
                                                   {0},
                                                   b.medspec_vchr,
                                                   b.putmedtype_int,
                                                   b.packqty_dec,
                                                   b.ipchargeflg_int,
                                                   c.code_vchr,
                                                   c.deptname_vchr,
                                                   d.inpatientid_chr,
                                                   e.lastname_vchr,
                                                   e.sex_chr,
                                                   e.birth_dat,
                                                   trim(f.code_chr) code_chr,
                                                   g.putmed_int,
                                                   g.usagename_vchr,
                                                   h.freqname_chr,
                                                   j.repare_int,
                                                   k.flaga_int as medproperty,
                                                   card.patientcardid_chr,
                                                   h.opfredesc_vchr,
                                                   a.create_dat,
                                                   sysdate - 1 as yesterday,
                                                   d.icd10diagtext_vchr,
                                                   a.registerid_chr,
                                                   a.orderid_chr,
                                                   a.medstoreid_chr,
                                                   a.isclinicsub,
                                                   b.highriskflag,
                                                   b.isanaesthesia_chr,
                                                   b.ischlorpromazine_chr,
                                                   ord.isproxyboilmed,
                                                   nvl(pre.preamount, 0) as premedamount
                                              from t_bih_opr_putmeddetail a,
                                                   t_bse_medicine b,
                                                   t_bse_deptdesc c,
                                                   t_opr_bih_register d,
                                                   t_opr_bih_registerdetail e,
                                                   t_bse_bed f,
                                                   t_bse_usagetype g,
                                                   t_aid_recipefreq h,
                                                   t_opr_bih_orderexecute j,
                                                   t_bse_patientcard card,
                                                   t_aid_medicinepreptype k,
                                                   t_opr_bih_order ord,
                                                   (select refputmedreqid_chr, sum(pp.recqty) as preamount
                                                      from t_pretestmed pp
                                                     group by pp.refputmedreqid_chr) pre,
                                                   t_opr_bih_patientcharge chrg 
                                             where (a.areaid_chr = c.deptid_chr)
                                               and (a.medid_chr = b.medicineid_chr)
                                               and (b.medicinepreptype_chr = k.medicinepreptype_chr(+))
                                               and (a.registerid_chr = d.registerid_chr)
                                               and (d.registerid_chr = e.registerid_chr)
                                               and (a.bedid_chr = f.bedid_chr(+))
                                               and (a.dosetypeid_chr = g.usageid_chr(+))
                                               and (a.execfreqid_chr = h.freqid_chr(+))
                                               and (a.orderexecid_chr = j.orderexecid_chr(+))
                                               and (a.paientid_chr = card.patientid_chr)
                                               and (a.orderid_chr = ord.orderid_chr(+))
                                               and (a.putmeddetailid_chr = pre.refputmedreqid_chr(+))
                                               and a.pchargeid_chr = chrg.pchargeid_chr 
                                               and a.status_int = 1";


        #endregion

        #region 查询有未配药病人的病区
        /// <summary>
        /// 查询有未配药病人的病区
        /// </summary>
        /// <param name="medType"></param>
        /// <param name="DayScope"></param>
        /// <param name="hastArea"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetDisputArea(string medType, int DayScope, out System.Collections.Generic.Dictionary<string, string> hastArea, int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;

            hastArea = new System.Collections.Generic.Dictionary<string, string>();

            string strSub = "";

            if (medType.Trim() != "")
            {
                strSub += " and t.medicnetype_int in (" + medType + ")";
            }

            if (intTimeSpan != 1)
            {
                if (DayScope > 0)
                {
                    DayScope = DayScope - 1;
                    DateTime dtNow = System.DateTime.Now;
                    strSub += " and t.create_dat between to_date('" + dtNow.AddDays(-DayScope).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            else
            {
                strSub += " and (t.create_dat between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            }

            string strSQL = @"select d.deptid_chr
                                from t_bse_deptdesc d,
                                     t_bih_opr_putmeddetail t
                               where t.areaid_chr = d.deptid_chr
                                 and t.isput_int = 0
                                 and t.putmedtype_int = 1" + strSub;

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    string areaId;
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        areaId = dtbResult.Rows[i1]["deptid_chr"].ToString();
                        if (!hastArea.ContainsKey(areaId))
                        {
                            hastArea.Add(areaId, areaId);
                        }
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

        #region 查询病区全院出院带药标志
        /// <summary>
        /// 查询病区全院出院带药标志
        /// </summary>
        /// <param name="medType"></param>
        /// <param name="DayScope"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetOutFlag(string medType, int DayScope, out DataTable p_dtResult, int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSub = "";
            if (intTimeSpan != 1)
            {
                if (DayScope > 0)
                {
                    DayScope = DayScope - 1;
                    DateTime dtNow = System.DateTime.Now;
                    strSub += " and a.create_dat between to_date('" + dtNow.AddDays(-DayScope).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            else
            {
                strSub = " and (a.create_dat between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();


                string strSQL;
                strSQL = @"select a.PUTMEDDETAILID_CHR, a.AREAID_CHR
                              from T_BIH_OPR_PUTMEDDETAIL a
                             where a.MEDICNETYPE_INT in (" + medType + @")
                               and a.PUTMEDTYPE_INT = 1
                               and a.ISPUT_INT = 0
                               and a.NEEDCONFIRM_INT = 0
                               and a.ORDEREXECTYPE_INT = 4 
                               and rownum = 1 " + strSub;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);

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

        #region 查询已置全区发送完毕标志的病区
        /// <summary>
        /// 查询已置全区发送完毕标志的病区
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="hastArea"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetSendCompleteArea(out System.Collections.Generic.Dictionary<string, string> hastArea, int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;

            hastArea = new System.Collections.Generic.Dictionary<string, string>();
            string strSQL = "";

            if (intTimeSpan != 1)
            {
                strSQL = @"select areaid_chr, ISPUT_INT from T_OPR_BIH_AREAPUTMEDRECORD
                              where STATUS_INT = 1 and trunc(PUT_DAT) = trunc(sysdate)";
            }
            else
            {
                strSQL = @"select areaid_chr, isput_int
  from t_opr_bih_areaputmedrecord
 where status_int = 1
   and (put_dat between to_date ('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + @"', 'yyyy-mm-dd hh24:mi:ss')
                    and to_date ('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')) ";
            }

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    string areaId;
                    string isPut;

                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        areaId = dtbResult.Rows[i1]["areaid_chr"].ToString();
                        isPut = dtbResult.Rows[i1]["ISPUT_INT"].ToString();

                        if (!hastArea.ContainsKey(areaId))
                        {
                            hastArea.Add(areaId, isPut);
                        }
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

        #region 查询病区集中配药单
        /// <summary>
        /// 查询病区集中配药单
        /// </summary>
        /// <param name="p_strAreaId"></param>
        /// <param name="medType"></param>
        /// <param name="DayScope"></param>
        /// <param name="p_dtResult"></param>
        /// <param name="p_intMedName">1 商品名  2 通用名</param>
        /// <param name="intTimeSpan">是否用时间段来查询 1 是 其它 否</param>
        /// <param name="dtmBegin">开始时间</param>
        /// <param name="dtmEnd">结束时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCentralizedByAreaId(string p_strAreaId, string medType, int DayScope, out DataTable p_dtResult, int p_intMedName, int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd, int typeId)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();

            string strSub = "";
            if (intTimeSpan != 1)
            {
                if (DayScope > 0)
                {
                    DayScope = DayScope - 1;
                    DateTime dtNow = System.DateTime.Now;
                    strSub += " and a.create_dat between to_date('" + dtNow.AddDays(-DayScope).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            else
            {
                strSub = " and (a.create_dat between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            }

            string subStr3 = " a.ACTIVATETYPE_INT = 1 and ";
            string subStr2 = string.Empty;
            if (typeId == 0)
            {
                subStr2 = " and (b.highriskflag is null or b.highriskflag = 0) and (b.isanaesthesia_chr is null or b.isanaesthesia_chr = 'F') and (b.ischlorpromazine_chr is null or b.ischlorpromazine_chr = 'F') ";
            }
            else if (typeId == 1)
            {
                subStr2 = " and b.highriskflag = 1 ";
                subStr3 = " a.ACTIVATETYPE_INT in (1,2,3) and ";
            }
            else if (typeId == 2)
            {
                subStr2 = " and (b.isanaesthesia_chr = 'T' or b.ischlorpromazine_chr = 'T') ";
                subStr3 = " a.ACTIVATETYPE_INT in (1,2,3) and ";
            }
            string strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.AREASEQ_INT = 0 and 
                                            a.ISPUT_INT = 0 and 
                                            a.NEEDCONFIRM_INT = 0 and
                                           " + subStr3 + @" 
                                            a.ORDEREXECTYPE_INT <> 4 and
                                            a.AREAID_CHR = ? 
                                           " + strSub + @" 
                                           " + subStr2 + @" 
                                   order by CODE_CHR, b.ASSISTCODE_CHR";

            if (p_intMedName == 2)
            {
                //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
            }
            else if (p_intMedName == 1)
            {
                //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as medicinename_vchr");
            }
            else
            {
                //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strAreaId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);
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

        #region Filter9011
        /// <summary>
        /// Filter9011
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        [AutoComplete]
        DataTable Filter9011(DataTable dtSource)
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string Sql = @"select a.parmvalue_vchr from t_bse_sysparm a where a.status_int = 1 and a.parmcode_chr = '9011'";
                DataTable dt9011 = null;
                clsHRPTableService svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt9011);
                if (dt9011 != null && dt9011.Rows.Count > 0 && Convert.ToInt32(dt9011.Rows[0]["parmvalue_vchr"].ToString()) == 1)
                {
                    for (int i = dtSource.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtSource.Rows[i]["isproxyboilmed"] != DBNull.Value && Convert.ToInt32(dtSource.Rows[i]["isproxyboilmed"].ToString()) > 0)
                        {
                            dtSource.Rows.RemoveAt(i);
                        }
                    }
                    dtSource.AcceptChanges();
                }
            }
            return dtSource;
        }
        #endregion

        #region 查询病区统一配药单(全区发送完毕)
        /// <summary>
        /// 查询病区统一配药单(全区发送完毕)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetUnitByAreaId(string p_strAreaId, string medType, out DataTable p_dtResult, int p_intMedName,
                                    int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;
            string strSQL = "";
            p_dtResult = new DataTable();

            if (intTimeSpan != 1)
            {
                strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            EXISTS (select 1 from T_OPR_BIH_AREAPUTMEDRECORD fa where fa.SEQ_INT = a.AREASEQ_INT and fa.status_int = 1 and fa.ISPUT_INT = 0 and trunc(fa.PUT_DAT) = trunc(sysdate)) and
                                            a.ISPUT_INT = 0 and 
                                            a.NEEDCONFIRM_INT = 0 and
                                            a.ACTIVATETYPE_INT = 1 and
                                            a.ORDEREXECTYPE_INT <> 4 and 
                                            a.AREAID_CHR = ?";
            }
            else
            {
                strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            EXISTS (select 1 from T_OPR_BIH_AREAPUTMEDRECORD fa where fa.SEQ_INT = a.AREASEQ_INT and fa.status_int = 1 
                                            and fa.ISPUT_INT = 0 and (fa.PUT_DAT between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + @"','yyyy-mm-dd hh24:mi:ss'))) and
                                            a.ISPUT_INT = 0 and 
                                            a.NEEDCONFIRM_INT = 0 and
                                            a.ACTIVATETYPE_INT = 1 and
                                            a.ORDEREXECTYPE_INT <> 4 and 
                                            a.AREAID_CHR = ?";
            }

            try
            {
                if (p_intMedName == 2)
                {
                    //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                }
                else if (p_intMedName == 1)
                {
                    //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as  medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as  medicinename_vchr");
                }
                else
                {
                    //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as  medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as  medicinename_vchr");
                }

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strAreaId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);
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

        #region 查询病区出院带药配药单
        /// <summary>
        /// 查询病区出院带药配药单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetOutByAreaId(string p_strAreaId, string medType, int DayScope, out DataTable p_dtResult, int p_intMedName,
                                   int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                string strSub = "";
                if (intTimeSpan != 1)
                {
                    if (DayScope > 0)
                    {
                        DayScope = DayScope - 1;
                        DateTime dtNow = System.DateTime.Now;
                        strSub += " and a.create_dat between to_date('" + dtNow.AddDays(-DayScope).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    }
                }
                else
                {
                    strSub = " and (a.create_dat between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
                }

                string strSQL;
                if (p_strAreaId == "%")
                {
                    strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.AREASEQ_INT = 0 and 
                                            a.ISPUT_INT = 0 and 
                                            a.NEEDCONFIRM_INT = 0 and
                                            a.ORDEREXECTYPE_INT = 4" + strSub;
                    if (p_intMedName == 2)
                    {
                        //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                        strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                    }
                    else if (p_intMedName == 1)
                    {
                        //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                        strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as medicinename_vchr");
                    }
                    else
                    {
                        //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                        strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                    }
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                }
                else
                {
                    strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.AREASEQ_INT = 0 and 
                                            a.ISPUT_INT = 0 and 
                                            a.NEEDCONFIRM_INT = 0 and
                                            a.ORDEREXECTYPE_INT = 4 and
                                            a.AREAID_CHR = ?" + strSub;

                    if (p_intMedName == 2)
                    {
                        //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                        strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                    }
                    else if (p_intMedName == 1)
                    {
                        //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                        strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as medicinename_vchr");
                    }
                    else
                    {
                        //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                        strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                    }

                    System.Data.IDataParameter[] arrParams = null;
                    objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                    arrParams[0].Value = p_strAreaId;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                    p_dtResult = this.Filter9011(p_dtResult);
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

        #region 查询病区确认药配药单
        /// <summary>
        /// 查询病区确认药配药单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAffirmanceByAreaId(string p_strAreaId, string medType, int DayScope, out DataTable p_dtResult, int p_intMedName,
                                          int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSub = "";
            if (intTimeSpan != 1)
            {
                if (DayScope > 0)
                {
                    DayScope = DayScope - 1;
                    DateTime dtNow = System.DateTime.Now;
                    strSub += " and a.create_dat between to_date('" + dtNow.AddDays(-DayScope).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            else
            {
                strSub = " and (a.create_dat between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            }

            string subStr2 = " and (b.highriskflag is null or b.highriskflag = 0) and (b.isanaesthesia_chr is null or b.isanaesthesia_chr = 'F') and (b.ischlorpromazine_chr is null or b.ischlorpromazine_chr = 'F') ";

            string strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.AREASEQ_INT = 0 and 
                                            a.ISPUT_INT = 0 and 
                                            a.ACTIVATETYPE_INT in (2, 3) and
                                            a.AREAID_CHR = ? 
                                          " + subStr2 + @" 
                                          " + strSub;

            try
            {
                if (p_intMedName == 2)
                {
                    //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                }
                else if (p_intMedName == 1)
                {
                    //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as medicinename_vchr");
                }
                else
                {
                    //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || '') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || '') ') as medicinename_vchr");
                }

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strAreaId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);
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

        #region 查询病区处方药配药单
        /// <summary>
        /// 查询病区处方药配药单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetRecipeByAreaId(string p_strAreaId, string medType, int DayScope, out DataTable p_dtResult, int p_intMedName,
                                      int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSub = "";
            if (intTimeSpan != 1)
            {
                if (DayScope > 0)
                {
                    DayScope = DayScope - 1;
                    DateTime dtNow = System.DateTime.Now;
                    strSub += " and a.create_dat between to_date('" + dtNow.AddDays(-DayScope).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                }
            }
            else
            {
                strSub = " and (a.create_dat between to_date('" + dtmBegin.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtmEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            }

            string strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + medType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.AREASEQ_INT = 0 and 
                                            a.ISPUT_INT = 0 and 
                                            a.ACTIVATETYPE_INT in (4, 5) and
                                            a.AREAID_CHR = ?" + strSub;


            try
            {
                if (p_intMedName == 2)
                {
                    //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                }
                else if (p_intMedName == 1)
                {
                    //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as medicinename_vchr");
                }
                else
                {
                    //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                    strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
                }

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strAreaId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);

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

        #region 查询配药执行单
        /// <summary>
        /// 查询配药执行单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="areaId"></param>
        /// <param name="putMedType"></param>
        /// <param name="putMedType"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPutmedReqByAreaId(string areaId, int putMedType, string medStoreId, out DataTable dtResult, int intTimeSpan, DateTime dtmBegin, DateTime dtmEnd, int medTypeId)
        {
            long lngRes = 0;
            dtResult = new DataTable();
            string Sql = string.Empty;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                System.Data.IDataParameter[] parm = null;
                if (intTimeSpan != 1)
                {
                    Sql = @"select putmedreqid_chr,
                                   status_int,
                                   creator_chr,
                                   create_dat,
                                   areaid_chr,
                                   putmedtype_int,
                                   medstoreid_chr
                              from t_bih_opr_putmedreq
                             where status_int = 1
                               and trunc(create_dat) = trunc(sysdate)
                               and putmedtype_int = ?
                               and areaid_chr = ?
                               and medstoreid_chr = ?
                               and (medTypeId is null or medTypeId = ?)";
                    svc.CreateDatabaseParameter(4, out parm);
                    parm[0].Value = putMedType;
                    parm[1].Value = areaId;
                    parm[2].Value = medStoreId;
                    parm[3].Value = medTypeId;
                }
                else
                {
                    Sql = @"select putmedreqid_chr,
                                   status_int,
                                   creator_chr,
                                   create_dat,
                                   areaid_chr,
                                   putmedtype_int,
                                   medstoreid_chr
                              from t_bih_opr_putmedreq
                             where status_int = 1
                               and putmedtype_int = ?
                               and areaid_chr = ?
                               and medstoreid_chr = ?
                               and (medTypeId is null or medTypeId = ?)
                               and (create_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";
                    svc.CreateDatabaseParameter(6, out parm);
                    parm[0].Value = putMedType;
                    parm[1].Value = areaId;
                    parm[2].Value = medStoreId;
                    parm[3].Value = medTypeId;
                    parm[4].Value = dtmBegin.ToString("yyyy-MM-dd HH:mm:ss");
                    parm[5].Value = dtmEnd.ToString("yyyy-MM-dd HH:mm:ss");
                }

                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dtResult, parm);
                svc.Dispose();

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

        #region 根据配药流水号查询配药单
        /// <summary>
        /// 根据配药流水号查询配药单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_putMedReqId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPutMedReqById(string p_putMedReqId, out DataTable p_dtResult, int p_intMedName)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = SQL_SELECT + @" and
                                            a.PUTMEDREQID_CHR = ?";

            if (p_intMedName == 2)
            {
                //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
            }
            else if (p_intMedName == 1)
            {
                //strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                strSQL = string.Format(strSQL, "(b.medicinename_vchr || '(' || b.mednormalname_vchr || ') ') as medicinename_vchr");
            }
            else
            {
                //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
                strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_putMedReqId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);

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

        #region 根据药房ID查询药房所对应的病区
        /// <summary>
        /// 根据药房ID查询药房所对应的病区
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medStoreId"></param>
        /// <param name="hastArea"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetAreaByMedStoreId(string medStoreId, out System.Collections.Generic.Dictionary<string, string>[] hastArea)
        {
            long lngRes = 0;

            hastArea = null;
            string strSQL = @"select a.areaid_chr, b.deptname_vchr
                                from t_bse_area_medstore_rlt a, 
                                     t_bse_deptdesc b                                    
                               where a.areaid_chr = b.deptid_chr
                                 and a.status_int = 1
                                 and a.medstoreid_chr = ?
                              order by b.code_vchr, a.order_int";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = medStoreId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    hastArea = new System.Collections.Generic.Dictionary<string, string>[dtbResult.Rows.Count];
                    string areaId;
                    string areaName;

                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        areaId = dtbResult.Rows[i1]["AREAID_CHR"].ToString();
                        areaName = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString();

                        hastArea[i1] = new System.Collections.Generic.Dictionary<string, string>();
                        hastArea[i1].Add("AREAID_CHR", areaId);
                        hastArea[i1].Add("DEPTNAME_VCHR", areaName);

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

        #region 根据药房ID查询药房信息
        /// <summary>
        /// 根据药房ID查询药房信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medStoreId"></param>
        /// <param name="medStoreVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetMedStoreInfById(string medStoreId, out clsMedStore_VO medStoreVo)
        {
            long lngRes = 0;

            medStoreVo = new clsMedStore_VO();
            string strSQL = @"select a.MEDSTOREID_CHR, 
                                     a.MEDSTORENAME_VCHR,
                                     a.MEDSTORETYPE_INT,
                                     a.MEDICNETYPE_INT,
                                     a.URGENCE_INT  
                              from 
                                   T_BSE_MEDSTORE a
                              where 
                                    a.MEDSTOREID_CHR = ? ";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = medStoreId;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    medStoreVo.m_strMedStoreID = medStoreId;
                    medStoreVo.m_strMedStoreName = dtbResult.Rows[0]["MEDSTORENAME_VCHR"].ToString();
                    medStoreVo.m_intMedStoreType = int.Parse(dtbResult.Rows[0]["MEDSTORETYPE_INT"].ToString());
                    medStoreVo.m_intMedicneType = int.Parse(dtbResult.Rows[0]["MEDICNETYPE_INT"].ToString());
                    medStoreVo.m_intUrgency = int.Parse(dtbResult.Rows[0]["URGENCE_INT"].ToString());
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

        #region 查询已配的药品
        /// <summary>
        /// 查询已配的药品
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPutMedDetailByDate(string p_strBegin, string p_strEnd, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = SQL_SELECT + @" and 
                                            a.ISPUT_INT = 1 and 
                                            a.PUBDATE_DAT > to_date('" + p_strBegin + @"', 'yyyy-mm-dd hh24:mi:ss') and 
                                            a.PUBDATE_DAT < to_date('" + p_strEnd + "', 'yyyy-mm-dd hh24:mi:ss')";


            //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
            strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);
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

        #region 查询配药明细
        /// <summary>
        /// 查询配药明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_intIsput"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPutMedDetailByDate(string p_strBegin, string p_strEnd, int p_intIsput, string p_strMedStoreId, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();

            string strSQL;

            if (p_intIsput == 1)
            {
                strSQL = SQL_SELECT + @" and 
                                            a.ISPUT_INT = 1 and 
                                            a.MEDSTOREID_CHR = '" + p_strMedStoreId + @"' and
                                            a.PUBDATE_DAT > to_date('" + p_strBegin + @"', 'yyyy-mm-dd hh24:mi:ss') and 
                                            a.PUBDATE_DAT < to_date('" + p_strEnd + "', 'yyyy-mm-dd hh24:mi:ss')";
            }
            else
            {
                strSQL = SQL_SELECT + @" and a.MEDICNETYPE_INT in (" + p_strMedType + @") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.ISPUT_INT = 0 and 
                                            a.PUBDATE_DAT > to_date('" + p_strBegin + @"', 'yyyy-mm-dd hh24:mi:ss') and 
                                            a.PUBDATE_DAT < to_date('" + p_strEnd + "', 'yyyy-mm-dd hh24:mi:ss')";

            }

            //strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ' || trim(nvl(ord.remark_vchr, ''))) as medicinename_vchr");
            strSQL = string.Format(strSQL, "(b.mednormalname_vchr || '(' || b.medicinename_vchr || ') ') as medicinename_vchr");

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                objHRPSvc.Dispose();
                p_dtResult = this.Filter9011(p_dtResult);

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

        #region 查询配药明细
        /// <summary>
        /// 查询配药明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intRptType">报表类型，0：汇总；1：按病人</param> 
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_intIsput"></param>
        /// <param name="p_strMedStoreId"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPutMedDetailByDate(int p_intRptType,
            string p_strBegin,
            string p_strEnd,
            int p_intIsput,
            string p_strMedStoreId,
            string p_strMedType,
            string p_strAreaId,
            out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();

            StringBuilder strSQL;
            if (p_intRptType == 0)
            {
                strSQL = new StringBuilder(@"select a.putmeddetailid_chr,
                             a.unit_vchr,
                             a.unitprice_mny,
                             a.get_dec,       	   
                             b.medicineid_chr, 	
                             b.assistcode_chr,
                             b.medicinename_vchr || '(' || b.mednormalname_vchr || ')' medicinename_vchr,
                             b.medspec_vchr,
                             c.code_vchr,
                             c.deptname_vchr,       
                             d.inpatientid_chr,
                             e.lastname_vchr,
                             e.sex_chr,
                             f.code_chr
                          from 
                               t_bih_opr_putmeddetail   a,
                               t_bse_medicine           b,
                               t_bse_deptdesc           c,
                               t_opr_bih_register       d,
                               t_opr_bih_registerdetail e,
                               t_bse_bed                f

                         where (a.areaid_chr = c.deptid_chr)
                           and (a.medid_chr = b.medicineid_chr)
                           and (a.registerid_chr = d.registerid_chr)
                           and (d.registerid_chr = e.registerid_chr)
                           and (a.bedid_chr = f.bedid_chr(+))");
            }
            else
            {
                strSQL = new StringBuilder(@"select a.putmeddetailid_chr,
                             a.unit_vchr,
                             a.unitprice_mny,
                             a.get_dec,       	   
                             b.medicineid_chr, 	
                             b.assistcode_chr,
                             b.medicinename_vchr || '(' || b.mednormalname_vchr || ')' medicinename_vchr,
                             b.medspec_vchr,
                             c.code_vchr,
                             c.deptname_vchr,       
                             d.inpatientid_chr,
                             e.lastname_vchr,
                             e.sex_chr,
                             f.code_chr,
                             a.recipeno_int 
                          from 
                               t_bih_opr_putmeddetail   a,
                               t_bse_medicine           b,
                               t_bse_deptdesc           c,
                               t_opr_bih_register       d,
                               t_opr_bih_registerdetail e,
                               t_bse_bed                f
                         where (a.areaid_chr = c.deptid_chr)
                           and (a.medid_chr = b.medicineid_chr)
                           and (a.registerid_chr = d.registerid_chr)
                           and (d.registerid_chr = e.registerid_chr)
                           and (a.bedid_chr = f.bedid_chr(+))");
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_intIsput == 1)
                {


                    strSQL.Append(@" and 
                                            a.ISPUT_INT = 1 and 
                                            a.MEDSTOREID_CHR = ? and
                                            a.AREAID_CHR = ? and
                                            a.PUBDATE_DAT >= ? and 
                                            a.PUBDATE_DAT <= ? ");
                    objHRPSvc.CreateDatabaseParameter(4, out arrParams);
                    arrParams[0].Value = p_strMedStoreId;
                    arrParams[1].Value = p_strAreaId;
                    arrParams[2].Value = DateTime.Parse(p_strBegin);
                    arrParams[3].Value = DateTime.Parse(p_strEnd);


                }
                else
                {
                    strSQL.Append(@" and a.MEDICNETYPE_INT in (");
                    strSQL.Append(p_strMedType);
                    strSQL.Append(@") and
                                            a.PUTMEDTYPE_INT = 1 and
                                            a.ISPUT_INT = 0 and
                                            a.AREAID_CHR = ? and
                                            a.CREATE_DAT >= ? and 
                                            a.CREATE_DAT <= ? ");

                    objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                    arrParams[0].Value = p_strAreaId;
                    arrParams[1].Value = DateTime.Parse(p_strBegin);
                    arrParams[2].Value = DateTime.Parse(p_strEnd);
                }

                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtResult, arrParams);
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

        #region 查询中心药房退药明细
        /// <summary>
        /// 查询中心药房退药明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intRptType">报表类型，0：汇总；1：按病人</param>
        /// <param name="p_strBegin"></param>
        /// <param name="p_strEnd"></param>
        /// <param name="p_strAreaId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetChargeMedDetailByDate(int p_intRptType, string p_strBegin, string p_strEnd, string p_strAreaId, string p_strMedType, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            String strSQL;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objDPArr = null;

                //                strSQL = @"select 
                //                             a.pchargeid_chr ,
                //                             a.amount_dec,
                //                             a.unit_vchr,
                //                             a.unitprice_dec,
                //                             b.medicineid_chr,
                //                             b.assistcode_chr,
                //                             b.medicinename_vchr || '(' || b.mednormalname_vchr || ')' medicinename_vchr, 
                //                             b.medspec_vchr,
                //                             c.inpatientid_chr,
                //                             d.lastname_vchr,
                //                             d.sex_chr,
                //                             e.deptid_chr,
                //                             e.deptname_vchr,
                //                             f.recipeno2_int,
                //                             g.code_chr
                //                          from t_opr_bih_patientcharge a
                //                             inner join t_bse_medicine b on a.chargeitemid_chr = b.medicineid_chr
                //                               and a.status_int = 1 
                //                             inner join t_opr_bih_register c on a.registerid_chr = c.registerid_chr
                //                               and c.status_int =1 
                //                             inner join t_opr_bih_registerdetail d on a.registerid_chr = d.registerid_chr
                //                               and d.status_int = 1 
                //                             inner join t_bse_deptdesc e on a.curareaid_chr = e.deptid_chr
                //                               and e.status_int =1
                //                             inner join t_opr_bih_order f on a.orderid_chr = f.orderid_chr 
                //                             inner join t_bse_bed g on c.bedid_chr = g.bedid_chr 
                //                          where (a.amount_dec < 0)
                //                             and (a.curareaid_chr = ?)
                //                             and (a.chargeactive_dat >= ? )
                //                             and (a.chargeactive_dat <= ? )";

                if (p_intRptType == 0)
                {
                    strSQL = @"select 
                             a.pchargeid_chr ,
                             a.amount_dec,
                             a.unit_vchr,
                             a.unitprice_dec,
                             b.medicineid_chr,
                             b.assistcode_chr,
                             b.medicinename_vchr || '(' || b.mednormalname_vchr || ')' medicinename_vchr, 
                             b.medspec_vchr,
                             c.inpatientid_chr,
                             d.lastname_vchr,
                             d.sex_chr,
                             e.deptid_chr,
                             e.deptname_vchr,
                             f.code_chr
                          from t_opr_bih_patientcharge a,
                               t_bse_medicine b,
                               t_opr_bih_register c,
                               t_opr_bih_registerdetail d,
                               t_bse_deptdesc e,
                               t_bse_bed f
                          where a.chargeitemid_chr = b.medicineid_chr
                            and a.registerid_chr = c.registerid_chr
                            and a.registerid_chr = d.registerid_chr
                            and a.curareaid_chr = e.deptid_chr
                            and c.bedid_chr = f.bedid_chr(+)
                            and a.status_int = 1
                            and c.status_int =1
                            and d.status_int = 1
                            and e.status_int =1                             
                            and (a.amount_dec < 0)
                            and b.putmedtype_int = 1
                            and (a.curareaid_chr = ?)
                            and (a.chargeactive_dat >= to_date(?,'yyyy-MM-dd hh24:mi:ss'))
                            and (a.chargeactive_dat <= to_date(?,'yyyy-MM-dd hh24:mi:ss'))
                            and ( b.medicnetype_int in (" + p_strMedType + ") )";
                }
                else
                {
                    strSQL = @"select 
                             a.pchargeid_chr ,
                             a.amount_dec,
                             a.unit_vchr,
                             a.unitprice_dec,
                             b.medicineid_chr,
                             b.assistcode_chr,
                             b.medicinename_vchr || '(' || b.mednormalname_vchr || ')' medicinename_vchr, 
                             b.medspec_vchr,
                             c.inpatientid_chr,
                             d.lastname_vchr,
                             d.sex_chr,
                             e.deptid_chr,
                             e.deptname_vchr,
                             null as recipeno2_int,
                             g.code_chr
                          from t_opr_bih_patientcharge a,
                               t_bse_medicine b,
                               t_opr_bih_register c,
                               t_opr_bih_registerdetail d,
                               t_bse_deptdesc e,                              
                               t_bse_bed g
                          where a.chargeitemid_chr = b.medicineid_chr
                            and a.registerid_chr = c.registerid_chr
                            and a.registerid_chr = d.registerid_chr
                            and a.curareaid_chr = e.deptid_chr                            
                            and c.bedid_chr = g.bedid_chr(+)
                            and a.status_int = 1
                            and c.status_int =1
                            and d.status_int = 1
                            and e.status_int =1 
                            and (a.amount_dec < 0)
                            and b.putmedtype_int = 1
                            and (a.curareaid_chr = ?)
                            and (a.chargeactive_dat >= to_date(?,'yyyy-MM-dd hh24:mi:ss'))
                            and (a.chargeactive_dat <= to_date(?,'yyyy-MM-dd hh24:mi:ss'))
                            and ( b.medicnetype_int in (" + p_strMedType + ") )";
                }

                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strAreaId;
                objDPArr[1].Value = p_strBegin;
                objDPArr[2].Value = p_strEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
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

        #region 查询中药配药明细
        /// <summary>
        /// 查询中药配药明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strShortDate"></param>
        /// <param name="p_intIsput"></param>
        /// <param name="p_strMedStoreId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetTCMedDetailByDate(string p_strShortDate, int p_intIsput, string p_strMedStoreId, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"select a.putmeddetailid_chr,
                                       a.registerid_chr,
                                       a.areaid_chr,
                                       a.bedid_chr,
                                       g.recipeno_int,
                                       a.medid_chr,
                                       a.medname_vchr,
                                       a.dosage_dec,
                                       a.dosageunit_vchr,
                                       a.unitprice_mny,
                                       a.unit_vchr,
                                       a.get_dec,
                                       a.outgetmeddays_int,
                                       a.create_dat,
                                       b.inpatientid_chr,
                                       c.lastname_vchr,
                                       c.sex_chr,
                                       c.birth_dat,
                                       e.code_chr,
                                       f.deptname_vchr,
                                       g.creatorid_chr,
                                       g.creator_chr,
                                       g.remark_vchr,
                                       g.DOSETYPENAME_CHR
                                  from t_bih_opr_putmeddetail   a,
                                       t_opr_bih_order          g,
                                       t_opr_bih_register       b,
                                       t_opr_bih_registerdetail c,                                      
                                       t_bse_bed                e,
                                       t_bse_deptdesc           f
                                 where a.registerid_chr = b.registerid_chr
                                   and b.registerid_chr = c.registerid_chr
                                   and a.areaid_chr = f.deptid_chr(+)
                                   and a.bedid_chr = e.bedid_chr(+)
                                   and a.orderid_chr = g.orderid_chr
                                   and a.medicnetype_int = 2 ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                if (p_intIsput == 1)
                {
                    //已配
                    strSQL += @" 
                               and a.isput_int = 1
                               and a.medstoreid_chr = ?
                               and a.pubdate_dat > ?
                               and a.pubdate_dat < ?";

                    objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                    arrParams[0].Value = p_strMedStoreId;
                    arrParams[1].Value = DateTime.Parse(p_strShortDate + " 00:00:00");
                    arrParams[2].Value = DateTime.Parse(p_strShortDate + " 23:59:59");
                }
                else
                {
                    strSQL += @"
                               and a.isput_int = 0
                               and a.create_dat > ?
                               and a.create_dat < ?";

                    objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                    arrParams[0].Value = DateTime.Parse(p_strShortDate + " 00:00:00");
                    arrParams[1].Value = DateTime.Parse(p_strShortDate + " 23:59:59");
                }



                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 个人药单查询
        /// <summary>
        /// 个人药单查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedType"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPatientMed(string p_strMedType, string p_strRegisterId, out DataTable p_dtResult)
        {
            long lngRes = 0;

            p_dtResult = new DataTable();
            string strSQL = @"SELECT a.PUTMEDDETAILID_CHR,
                                       a.AREAID_CHR,
                                       a.UNITPRICE_MNY,
                                       a.UNIT_VCHR,
                                       a.GET_DEC,
                                       a.ISPUT_INT,
                                       a.PUTTYPE_INT,
                                       a.PUBDATE_DAT,
                                       a.ISRECRUIT_INT,
                                       a.DOSAGE_DEC,
                                       a.DOSAGEUNIT_VCHR,
                                       a.EXECDAYS_INT,
                                       a.OUTGETMEDDAYS_INT,
                                       a.ORDEREXECTYPE_INT,
                                       b.ASSISTCODE_CHR,
                                       b.MEDICINENAME_VCHR || '(' || MEDNORMALNAME_VCHR || ')' MEDICINENAME_VCHR,
                                       b.MEDSPEC_VCHR,
                                       b.PUTMEDTYPE_INT,
                                       c.CODE_VCHR,
                                       c.DEPTNAME_VCHR,
                                       d.INPATIENTID_CHR,
                                       e.LASTNAME_VCHR,
                                       e.SEX_CHR,
                                       f.CODE_CHR CODE_CHR,
                                       h.FREQNAME_CHR,
                                       i.RECIPENO_INT,
                                       i.RECIPENO2_INT,
                                       i.createdate_dat,
                                       i.stopdate_dat,
                                       i.EXECUTEDATE_DAT,
                                       i.DOSETYPENAME_CHR,
                                       j.executedate_vchr
                                  FROM T_BIH_OPR_PUTMEDDETAIL   a,
                                       T_BSE_MEDICINE           b,
                                       T_BSE_DEPTDESC           c,
                                       T_OPR_BIH_REGISTER       d,
                                       T_OPR_BIH_REGISTERDETAIL e,
                                       T_BSE_BED                f,
                                       T_AID_RECIPEFREQ         h,
                                       T_OPR_BIH_ORDER          i,
                                       T_OPR_BIH_ORDEREXECUTE   j                                       
                                 WHERE (a.AREAID_CHR = c.DEPTID_CHR)
                                   and (a.MEDID_CHR = b.MEDICINEID_CHR)
                                   and (a.REGISTERID_CHR = d.REGISTERID_CHR)
                                   and (d.REGISTERID_CHR = e.REGISTERID_CHR)
                                   and (a.BEDID_CHR = f.BEDID_CHR)
                                   and (a.EXECFREQID_CHR = h.FREQID_CHR(+))
                                   and (a.ORDERID_CHR = i.ORDERID_CHR(+))
                                   and (a.orderexecid_chr = j.orderexecid_chr(+))
                                   and a.putmedtype_int = 1
                                   and a.MEDICNETYPE_INT in (" + p_strMedType + @")
                                   and a.registerid_chr = ? 
                                 ORDER BY a.PUBDATE_DAT,i.EXECUTEDATE_DAT DESC ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = p_strRegisterId;

                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
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

        #region 根据住院ID查询诊疗卡号
        /// <summary>
        /// 根据住院ID查询诊疗卡号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strPatientCardID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientCardID(string p_strInPatientID, out string p_strPatientCardID)
        {
            long lngRes = 0;
            p_strPatientCardID = "";
            string strSQL = @"select patientcardid_chr
                              from t_bse_patient t1, t_bse_patientcard t2
                             where t1.patientid_chr = t2.patientid_chr
                               and t1.inpatientid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = p_strInPatientID;

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paramArr);
                if (dtResult.Rows.Count != 0)
                {
                    p_strPatientCardID = dtResult.Rows[0][0].ToString();
                }
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx.Message);
            }
            return lngRes;
        }
        #endregion

        #region 根据用法ID查询用法名称
        /// <summary>
        /// 根据用法ID查询用法名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strUsageID"></param>
        /// <param name="p_strUsageName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsageNameByID(string p_strUsageID, out string p_strUsageName)
        {
            long lngRes = 0;
            p_strUsageName = "";
            string strSQL = @"select t.usagename_vchr
                              from t_bse_usagetype t
                             where t.usageid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = p_strUsageID;

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paramArr);
                if (dtResult.Rows.Count != 0)
                {
                    p_strUsageName = dtResult.Rows[0][0].ToString();
                }
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx.Message);
            }
            return lngRes;
        }
        #endregion

        #region 根据频率名称查询出相关执行次数和时间
        /// <summary>
        /// 根据频率名称查询出相关执行次数和时间
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFreqName"></param>
        /// <param name="p_dtFreqDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFreqDetailByFreqName(string p_strFreqName, out string p_strTimes, out string p_strDays, out string p_strExecWeekday)
        {
            long lngRes = 0;
            p_strTimes = "";
            p_strDays = "";
            p_strExecWeekday = "";
            string strSQL = @"select t.times_int, t.days_int, t.execweekday_chr
                              from t_aid_recipefreq t
                             where t.freqname_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = p_strFreqName;

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paramArr);
                if (dtResult.Rows.Count != 0)
                {
                    p_strTimes = dtResult.Rows[0][0].ToString();
                    p_strDays = dtResult.Rows[0][1].ToString();
                    p_strExecWeekday = dtResult.Rows[0][2].ToString();
                }
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx.Message);
            }
            return lngRes;
        }
        #endregion

        #region 根据药品ID查询出相关的药品信息
        /// <summary>
        /// 根据药品ID查询出相关的药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedID"></param>
        /// <param name="p_strDosage"></param>
        /// <param name="p_strIpUnit"></param>
        /// <param name="p_strPrepType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedDetailByMedID(string p_strMedID, out string p_strDosage, out string p_strIpUnit, out string p_strPrepType)
        {
            long lngRes = 0;
            p_strDosage = "";
            p_strIpUnit = "";
            p_strPrepType = "";
            string strSQL = @"select t.dosage_dec, t.ipunit_chr, t.medicinepreptype_chr
                                  from t_bse_medicine t
                                 where t.medicineid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = p_strMedID;

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paramArr);
                if (dtResult.Rows.Count != 0)
                {
                    p_strDosage = dtResult.Rows[0][0].ToString();
                    p_strIpUnit = dtResult.Rows[0][1].ToString();
                    p_strPrepType = dtResult.Rows[0][2].ToString();
                }
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx.Message);
            }
            return lngRes;
        }
        #endregion

        #region 获取系统时间
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public DateTime m_datGetServerTime()
        {
            return DateTime.Now;
        }
        #endregion

        #region 根据医嘱ID查询医嘱状态
        /// <summary>
        /// 根据医嘱ID查询医嘱状态
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID"></param>
        /// <param name="p_strStatus"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderStatusByID(string p_strOrderID, out string p_strStatus)
        {
            long lngRes = 0;
            p_strStatus = "";

            string strSQL = @"select t.status_int, t.orderid_chr, p.putmeddetailid_chr
                                from t_opr_bih_order t, t_bih_opr_putmeddetail p
                               where t.orderid_chr = p.orderid_chr
                                 and p.putmeddetailid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = p_strOrderID;

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paramArr);
                if (dtResult.Rows.Count != 0)
                {
                    p_strStatus = dtResult.Rows[0]["status_int"].ToString();
                }
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx.Message);
            }
            return lngRes;
        }
        #endregion

        #region 合并配药单

        #region 查已摆药列表
        /// <summary>
        /// 查已摆药列表
        /// </summary>
        /// <param name="isUseDate"></param>
        /// <param name="medStoreId"></param>
        /// <param name="dtmBegin"></param>
        /// <param name="dtmEnd"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetPutMedReq(bool isUseDate, string medStoreId, DateTime dtmBegin, DateTime dtmEnd)
        {
            string Sql = string.Empty;
            DataTable dt = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                if (isUseDate)
                {
                    Sql = @"select a.putmedreqid_chr,
                               a.creator_chr,
                               a.create_dat,
                               a.areaid_chr,
                               a.putmedtype_int,
                               a.medstoreid_chr,
                               b.deptname_vchr
                          from t_bih_opr_putmedreq a
                         inner join t_bse_deptdesc b
                            on a.areaid_chr = b.deptid_chr
                         where a.status_int = 1
                           and a.putmedtype_int = 1
                           and a.medstoreid_chr = ?
                           and (a.create_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss') ) order by a.create_dat desc";

                    svc.CreateDatabaseParameter(3, out parm);
                    parm[0].Value = medStoreId;
                    parm[1].Value = dtmBegin.ToString("yyyy-MM-dd HH:mm:ss");
                    parm[2].Value = dtmEnd.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    Sql = @"select a.putmedreqid_chr,
                                   a.creator_chr,
                                   a.create_dat,
                                   a.areaid_chr,
                                   a.putmedtype_int,
                                   a.medstoreid_chr,
                                   b.deptname_vchr
                              from t_bih_opr_putmedreq a
                             inner join t_bse_deptdesc b
                                on a.areaid_chr = b.deptid_chr
                             where a.status_int = 1
                               and Trunc(a.create_dat) = Trunc(sysdate)
                               and a.putmedtype_int = 1
                               and a.medstoreid_chr = ?";
                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = medStoreId;
                }
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText log = new clsLogText();
                log.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #endregion

        #region 疗程用药刷新数据源
        /// <summary>
        /// 疗程用药刷新数据源
        /// </summary>
        /// <param name="storeId">药房ID</param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetCureMedInfo(string storeId)
        {
            string Sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"select d.deptid_chr as deptId,
                               d.deptname_vchr as deptName,
                               a.registerid_chr as registerid,
                               a.postdate_dat as orderDate,
                               a.orderid_chr as orderId,
                               a.name_vchr as orderName,
                               a.orderdicid_chr as orderDicId,
                               a.dosetypename_chr as usageName,
                               a.curedays as cureDays,
                               (a.get_dec * a.curedays) as preAmount,
                               a.getunit_chr as unit,
                               a.checkdate as checkDate
                          from t_opr_bih_order a
                         inner join t_bse_bih_orderdic b
                            on a.orderdicid_chr = b.orderdicid_chr
                         inner join t_opr_bih_orderchargedept c
                            on a.orderid_chr = c.orderid_chr
                           and b.itemid_chr = c.chargeitemid_chr
                         inner join t_bse_deptdesc d
                            on a.curareaid_chr = d.deptid_chr
                         inner join t_bse_medstore e
                            on c.clacarea_chr = e.deptid_chr
                         where (a.postdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                           and e.medstoreid_chr = ?
                           and (a.checkstate is null or a.checkstate = 0)
                           and a.curedays > 0";

                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                parm[1].Value = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                parm[2].Value = storeId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText log = new clsLogText();
                log.LogError(objEx);
            }
            return dt;
        }
        #endregion
    }
}
