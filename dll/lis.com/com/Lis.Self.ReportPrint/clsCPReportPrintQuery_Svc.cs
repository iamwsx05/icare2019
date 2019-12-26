using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace iCare.Lis.Self.CPReportPrint.svc
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsCPReportPrintQuery_Svc : clsMiddleTierBase
    {
        #region 通过各种id获取报告单
        /// <summary>
        /// 根据病人ID获取已经审核的报告单ID
        /// </summary>
        /// <param name="p_strPatienID"></param>
        /// <param name="p_lstApplicationID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLisApplicationIDByPatientID(string p_strPatienID, out DataTable dtRes)
        {
            dtRes = new DataTable();
            if (string.IsNullOrEmpty(p_strPatienID))
                return 0;
            clsHRPTableService objServ = new clsHRPTableService();
            string strSql = @"select distinct t1.application_id_chr,t4.apply_unit_id_chr 
from t_opr_lis_app_report t1
inner join t_opr_lis_app_sample_relation t2
on t1.application_id_chr = t2.application_id_chr
inner join t_opr_lis_sample t3
on t2.sample_id_chr = t3.sample_id_chr
inner join t_opr_lis_app_apply_unit t4 
on t1.application_id_chr = t4.application_id_chr
where t1.status_int = 3
and t1.report_print_chr = 0
and t3.status_int >= 3
and t3.patientid_chr = ?";
            IDataParameter[] objIDPArr = null;
            objServ.CreateDatabaseParameter(1, out objIDPArr);
            objIDPArr[0].Value = p_strPatienID;
            long lngRes = 0;
           // dtRes = new DataTable();
            try
            {
                lngRes = objServ.lngGetDataTableWithParameters(strSql, ref dtRes, objIDPArr);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 根据诊疗卡ID获取已经审核的报告单ID
        /// </summary>
        /// <param name="p_strPatienCardID"></param>
        /// <param name="p_lstApplicationID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLisApplicationIDByPatientCardID(string p_strPatienCardID, out DataTable dtRes)
        {
            dtRes = new DataTable();
            if (string.IsNullOrEmpty(p_strPatienCardID))
                return 0;
            clsHRPTableService objServ = new clsHRPTableService();
            string strSql = @"select distinct t1.application_id_chr,t4.apply_unit_id_chr 
from t_opr_lis_app_report t1
inner join t_opr_lis_app_sample_relation t2
on t1.application_id_chr = t2.application_id_chr
inner join t_opr_lis_sample t3
on t2.sample_id_chr = t3.sample_id_chr
inner join t_opr_lis_app_apply_unit t4 
on t1.application_id_chr = t4.application_id_chr
where t1.status_int = 3
and t1.report_print_chr = 0
and t3.status_int >= 3
and t3.patientcardid_chr = ?";
            IDataParameter[] objIDPArr = null;
            objServ.CreateDatabaseParameter(1, out objIDPArr);
            objIDPArr[0].Value = p_strPatienCardID;
            long lngRes = 0;
            try
            {      
                lngRes = objServ.lngGetDataTableWithParameters(strSql, ref dtRes, objIDPArr);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 根据社保卡号获取已经审核的报告单ID
        /// </summary>
        /// <param name="p_strPatienCardID"></param>
        /// <param name="p_lstApplicationID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLisLisApplicationIDBySBCardID(string p_strSBCardID, int p_intSBType,out DataTable dtRes)
        {
            string strSQL = @"select distinct t1.application_id_chr,t4.apply_unit_id_chr
from t_opr_lis_app_report t1
inner join t_opr_lis_app_sample_relation t2
on t1.application_id_chr = t2.application_id_chr
inner join t_opr_lis_sample t3
on t2.sample_id_chr = t3.sample_id_chr
inner join t_opr_lis_app_apply_unit t4 
on t1.application_id_chr = t4.application_id_chr
inner join t_bse_patientcardtype t5 
on t3.patientid_chr = t5.patientid_chr
where t1.status_int = 3
and t1.report_print_chr = 0
and t3.status_int >= 3
and t5.paycardstatus_int = 1
and t5.paycardtype_int = ?
and t5.paycardno_vchr = ? ";
            string strQue = @"select t.* from t_bse_patientcardtype t
where t.paycardtype_int = ?
and t.paycardno_vchr = ?
and t.paycardstatus_int = 1";
            dtRes = new DataTable();
            if (string.IsNullOrEmpty(p_strSBCardID))
                return 0;
            clsHRPTableService objServ = new clsHRPTableService();
            IDataParameter[] objIDPArr = null;
            objServ.CreateDatabaseParameter(2, out objIDPArr);
            objIDPArr[0].Value = p_intSBType;
            objIDPArr[1].Value = p_strSBCardID;
            long lngRes = 0;
            DataTable dtTemp = new DataTable();
            lngRes = objServ.lngGetDataTableWithParameters(strQue, ref dtTemp, objIDPArr);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                objIDPArr = null;
                objServ.CreateDatabaseParameter(2, out objIDPArr);
                objIDPArr[0].Value = p_intSBType;
                objIDPArr[1].Value = p_strSBCardID;
                lngRes = objServ.lngGetDataTableWithParameters(strSQL, ref dtRes, objIDPArr);
                return lngRes;
            }
            else
                //99 无医保卡信息
                return 99;
        }
        #endregion

        #region 用PatientID来查询已审核的报告单
        /// <summary>
        /// 用PatientID来查询已审核的报告单
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="p_objReportPrint"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportPrintInfo(string PatientID, out clsLis_ReportPrint_VO p_objReportPrint)
        {
            p_objReportPrint = null;
            long lngRes = 0;

            clsHRPTableService objHRPServ = null;
            try
            {
                string strSQL = @"select a.patient_name_vchr,
                                       a.application_id_chr,
                                       b.report_group_id_chr,
                                       b.report_print_chr,
                                       b.status_int,
                                       c.apply_unit_id_chr
                                  from t_opr_lis_application a
                                 inner join t_opr_lis_app_report b
                                    on a.application_id_chr = b.application_id_chr
                                   and b.status_int > 0
                                 inner join t_opr_lis_app_apply_unit c
                                    on c.application_id_chr = a.application_id_chr
                                 inner join t_bse_patientcardtype e
                                    on a.patientid_chr = e.patientid_chr
                                 where a.pstatus_int = 2
                                   and b.status_int = 2
                                   --and a.patient_type_id_chr = 2
                                   and e.paycardno_vchr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = PatientID;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {




                    DataRow drTemp = null;
                    Dictionary<string, clsLisAppPrinter> objApp_Printer = new Dictionary<string, clsLisAppPrinter>();
                    string strTemp = "";
                    string strAppTemp = "";
                    string strAppUnitID = "";
                    string strReportStatus = "";
                    clsLisAppPrinter objTemp = null;

                    for (int idx = 0; idx < dtResult.Rows.Count; idx++)
                    {
                        drTemp = dtResult.Rows[idx];
                        strAppTemp = drTemp["application_id_chr"].ToString().Trim();
                        strReportStatus = drTemp["status_int"].ToString().Trim();
                        strTemp = drTemp["report_print_chr"].ToString().Trim();

                        if (strReportStatus == "2")
                        {
                            if (string.IsNullOrEmpty(strTemp) || strTemp == "0")
                            {
                                if (objApp_Printer.ContainsKey(strAppTemp))
                                {
                                    objTemp = objApp_Printer[strAppTemp];
                                    strAppUnitID = drTemp["apply_unit_id_chr"].ToString().Trim();
                                    if (!objTemp.m_lstAppUnitIDArr.Contains(strAppUnitID))
                                    {
                                        objTemp.m_lstAppUnitIDArr.Add(strAppUnitID);
                                    }
                                }
                                else
                                {
                                    objTemp = new clsLisAppPrinter();
                                    objTemp.m_strApplicationID = strAppTemp;
                                    objTemp.m_strAppGroupID = drTemp["report_group_id_chr"].ToString().Trim();
                                    objTemp.m_lstAppUnitIDArr = new List<string>();
                                    objTemp.m_lstAppUnitIDArr.Add(drTemp["apply_unit_id_chr"].ToString().Trim());

                                    objApp_Printer.Add(strAppTemp, objTemp);
                                }
                            }
                            else
                            {
                                if (!objApp_Printer.ContainsKey(strAppTemp))
                                {
                                    objApp_Printer.Add(strAppTemp, null);
                                }
                            }
                        }
                        else
                        {
                            if (!objApp_Printer.ContainsKey(strAppTemp))
                            {
                                objApp_Printer.Add(strAppTemp, null);
                            }
                        }
                    }

                    if (objApp_Printer.Count > 0)
                    {
                        p_objReportPrint = new clsLis_ReportPrint_VO();
                        p_objReportPrint.m_intTotalReports = objApp_Printer.Count;

                        List<clsLisAppPrinter> objLisApp = new List<clsLisAppPrinter>();

                        foreach (clsLisAppPrinter obj in objApp_Printer.Values)
                        {
                            if (obj == null)
                            {
                                continue;
                            }
                            objLisApp.Add(obj);
                        }

                        p_objReportPrint.m_objAppPrinterArr = objLisApp.ToArray();
                        p_objReportPrint.m_strPatientName = dtResult.Rows[dtResult.Rows.Count - 1]["patient_name_vchr"].ToString().Trim();

                        dtResult = dtResult.DefaultView.ToTable(true, new string[] { "application_id_chr", "report_print_chr", "status_int" });
                        int iCount = 0;
                        int.TryParse(dtResult.Compute("Count(status_int)", "status_int = 2").ToString(), out iCount);
                        p_objReportPrint.m_intNotCommitReoprts = iCount;

                        int.TryParse(dtResult.Compute("Count(report_print_chr)", "report_print_chr > 0").ToString(), out iCount);
                        p_objReportPrint.m_intHasPrintReports = iCount;
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion
    }
}
