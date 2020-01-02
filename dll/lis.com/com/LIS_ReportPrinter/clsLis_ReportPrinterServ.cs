using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ
{
    /// <summary>
    /// 
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsLis_ReportPrinterServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获取检验报告打印信息
        /// </summary>
        /// <param name="p_strPatientCard"></param>
        /// <param name="p_objReportPrint"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLisReportPrintInfo(string p_strPatientCard, string p_strMinTime, out clsLis_ReportPrint_VO p_objReportPrint,out string p_strPatientName)
        {
            p_objReportPrint = null;
            p_strPatientName = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strPatientCard) || string.IsNullOrEmpty(p_strMinTime))
            {
                return lngRes;
            }

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
                                 inner join t_opr_lis_app_report b on a.application_id_chr =
                                                                      b.application_id_chr
                                                                  and b.status_int > 0
                                 inner join t_opr_lis_app_apply_unit c on c.application_id_chr =
                                                                          a.application_id_chr
                                 inner join t_bse_patientcard d on d.patientid_chr = a.patientid_chr
                                                               and d.status_int != 0
                                 where a.pstatus_int = 2
                                   --and a.patient_type_id_chr = 2
                                   and a.modify_dat >= to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                   and d.patientcardid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strMinTime;
                objDPArr[1].Value = p_strPatientCard;

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
                        int.TryParse(dtResult.Compute("Count(status_int)", "status_int <> 2").ToString(), out iCount);
                        p_objReportPrint.m_intNotCommitReoprts = iCount;

                        int.TryParse(dtResult.Compute("Count(report_print_chr)", "report_print_chr > 0").ToString(), out iCount);
                        p_objReportPrint.m_intHasPrintReports = iCount;
                    }
                }
                strSQL = @"select a.lastname_vchr
  from t_bse_patient a, t_bse_patientcard b
 where a.patientid_chr = b.patientid_chr
   and b.patientcardid_chr = ?";
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strPatientCard;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strPatientName = dtResult.Rows[0]["lastname_vchr"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                p_strPatientCard = null;
                objHRPServ = null;
            }
            return lngRes;
        }

        #region 写报告打印状态
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_strAPPLICATION_ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthWriteReportPrintState(string m_strAPPLICATION_ID)
        {
            string strSQL = null;

            IDataParameter[] objDPArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = m_strAPPLICATION_ID;
            long lngRes = 0;
            //lngRes = objHRPSvc.lngExecuteProc(@"p_opr_lis_report", objDPArr);
            //if (lngRes <= 0)
            //{
            //    ContextUtil.SetAbort();
            //    return lngRes;
            //}

            strSQL = @"update t_opr_lis_app_report t
   set t.report_print_chr = 1,
       t.report_print_dat = ?,
       t.modify_dat       = sysdate
 where t.application_id_chr = ?
   and status_int = 2
";
            objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = System.DateTime.Now;
            objDPArr[1].Value = m_strAPPLICATION_ID;
            long flag = 0;
            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref flag, objDPArr);
            flag = 0;
            objHRPSvc.Dispose();
            objDPArr = null;
            return lngRes;
        }
        #endregion

        #region 根据身份证号获取医保卡号
        /// <summary>
        /// 根据身份证号获取医保卡号
        /// </summary>
        /// <param name="p_strIDCard"></param>
        /// <param name="p_strPatientCard"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientCardByIDCard(string p_strIDCard, ref string p_strPatientCard)
        {
            long lngRes = 0;

            string strSQL = @"select b.patientcardid_chr
                                from t_bse_patient a, t_bse_patientcard b
                               where a.idcard_chr = ?
                                 and a.patientid_chr = b.patientid_chr
                                 and b.status_int > 0";

            try
            {
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strIDCard;
                DataTable dtResult = new DataTable();
                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (dtResult.Rows.Count != 0)
                {
                    p_strPatientCard = dtResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion
    }
}
