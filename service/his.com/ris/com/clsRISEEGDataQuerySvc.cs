using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.RIS
{
    /// <summary>
    /// 茶山脑电图电图代码查询专用中间件
    /// add by huafeng.xiao
    /// 2008年9月28日15:46:46
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsRISEEGDataQuerySvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取对应申请单的类型ID(脑电图）
        /// <summary>
        /// 获取对应申请单的类型ID(脑电图）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetApplTypeIDRISEEGR(out string IDRISEEGRTypeID)
        {
            IDRISEEGRTypeID = null;
            long lngRes = 0;
            string strSQL = @"select attachtype_int, typeid, status_int, attachtypename_vchr
  from t_aid_apply_rlt where attachtype_int = 5 ";
            //string strSQL = @"select * from T_AID_APPLY_RLT where ATTACHTYPE_INT =5";
            DataTable dtResult = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    IDRISEEGRTypeID = dtResult.Rows[0]["typeid"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获得经颅多普勒TCD报告单
        /// <summary>
        /// 获得经颅多普勒TCD报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTCDReportArr(out clsRIS_TCD_REPORT_VO[] p_objResultArr)
        {
            p_objResultArr = new clsRIS_TCD_REPORT_VO[0];
            long lngRes = 0;
            string strFromDat = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            string strToDat = System.DateTime.Now.ToShortDateString() + " 23:59:59";
            //			string strSQL = @"SELECT *  FROM T_OPR_RIS_TCD_REPORT WHERE STATUS_INT = '1'AND CHECK_DAT BETWEEN TO_DATE('"+strFromDat+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('"+strToDat+"','yyyy-mm-dd hh24:mi:ss') ";
            string strSQL = @"select a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.check_dat,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.diagnose_vchr,
       a.diagnose_xml_vchr,
       a.cure_circs_vchr,
       a.cure_circs_xml_vchr,
       a.ct_result_vchr,
       a.ct_result_xml_vchr,
       a.mri_result_vchr,
       a.mri_result_xml_vchr,
       a.x_ray_result_vchr,
       a.x_ray_result_xml_vchr,
       a.ekg_result_vchr,
       a.ekg_result_xml_vchr,
       a.bus_result_vchr,
       a.bus_result_xml_vchr, b.patientcardid_chr from t_opr_ris_tcd_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
  from t_bse_patientcard
         where status_int = 1 or status_int = 3) b where  a.patient_id_chr=b.patientid_chr(+) and a.status_int = '1'and a.check_dat between ? and ? ";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objArr);
                objArr[0].DbType = DbType.DateTime;
                objArr[0].Value = DateTime.Parse(strFromDat);
                objArr[1].DbType = DbType.DateTime;
                objArr[1].Value = DateTime.Parse(strToDat);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                // lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {

                    p_objResultArr = new clsRIS_TCD_REPORT_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_TCD_REPORT_VO();
                        p_objResultArr[i1].m_strCARD_ID_CHR = objDataRow["patientcardid_chr"].ToString();
                        p_objResultArr[i1].m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(objDataRow["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = objDataRow["DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDIAGNOSE_XML_VCHR = objDataRow["DIAGNOSE_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCURE_CIRCS_VCHR = objDataRow["CURE_CIRCS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCURE_CIRCS_XML_VCHR = objDataRow["CURE_CIRCS_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCT_RESULT_VCHR = objDataRow["CT_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCT_RESULT_XML_VCHR = objDataRow["CT_RESULT_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMRI_RESULT_VCHR = objDataRow["MRI_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMRI_RESULT_XML_VCHR = objDataRow["MRI_RESULT_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strX_RAY_RESULT_VCHR = objDataRow["X_RAY_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strX_RAY_RESULT_XML_VCHR = objDataRow["X_RAY_RESULT_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEKG_RESULT_VCHR = objDataRow["EKG_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEKG_RESULT_XML_VCHR = objDataRow["EKG_RESULT_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBUS_RESULT_VCHR = objDataRow["BUS_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBUS_RESULT_XML_VCHR = objDataRow["BUS_RESULT_XML_VCHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        #endregion

        #region 根据条件组合查询脑电图报告
        /// <summary>
        ///根据条件组合查询脑电图报告
        /// </summary>
        //public long m_lngGetTCDReportByCondition(System.Security.Principal.IPrincipal p_objPrincipal,string p_strFromDat,string p_strToDat,
        [AutoComplete]
        public long m_lngGetTCDReportByCondition(string p_strFromDat, string p_strToDat,
            string p_strPatientNo, string p_strInPatientNo, string p_strPatientName, string p_strDept, string p_strReportNo, string strReporter, out clsRIS_TCD_REPORT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;

            string strSQL = @"select a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.check_dat,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.diagnose_vchr,
       a.diagnose_xml_vchr,
       a.cure_circs_vchr,
       a.cure_circs_xml_vchr,
       a.ct_result_vchr,
       a.ct_result_xml_vchr,
       a.mri_result_vchr,
       a.mri_result_xml_vchr,
       a.x_ray_result_vchr,
       a.x_ray_result_xml_vchr,
       a.ekg_result_vchr,
       a.ekg_result_xml_vchr,
       a.bus_result_vchr,
       a.bus_result_xml_vchr, b.patientcardid_chr from t_opr_ris_tcd_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
  from t_bse_patientcard
         where status_int = 1 OR status_int = 3) b where a.patient_id_chr=b.patientid_chr(+)  and a.status_int = '1' ";
            //string strSQL = @"select a.*, b.patientcardid_chr from T_OPR_RIS_TCD_REPORT a,T_bse_patientcard b WHERE  a.patient_id_chr=b.patientid_chr and (b.status_int=1 or b.status_int=3) and a.STATUS_INT = '1'AND a.CHECK_DAT BETWEEN TO_DATE('"+strFromDat+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('"+strToDat+"','yyyy-mm-dd hh24:mi:ss') ";

            DataTable dtbResult = new DataTable();
            if (p_strFromDat != "" && p_strToDat != "")
            {
                strSQL += " and a.report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('" + p_strToDat + "','yyyy-mm-dd hh24:mi:ss')";
            }
            if (p_strPatientNo != "")
            {
                strSQL += " and a.patient_no_chr = '" + p_strPatientNo + "'";
            }
            if (p_strInPatientNo != "")
            {
                strSQL += " and a.inpatient_no_chr = '" + p_strInPatientNo + "'";
            }
            if (p_strPatientName != "")
            {
                strSQL += " and a.patient_name_vchr like '%" + p_strPatientName + "%'";
            }
            if (p_strDept != "")
            {
                strSQL += " and a.dept_name_vchr like '" + p_strDept + "%'";
            }
            if (p_strReportNo != "")
            {
                strSQL += " and a.report_no_chr = '" + p_strReportNo + "'";
            }
            if (string.IsNullOrEmpty(strReporter))
            {
                strSQL += " and a.reportor_name_vchr = '" + strReporter + "'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    p_objResultArr = new clsRIS_TCD_REPORT_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_TCD_REPORT_VO();
                        p_objResultArr[i1].m_strCARD_ID_CHR = objDataRow["patientcardid_chr"].ToString();
                        p_objResultArr[i1].m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(objDataRow["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = objDataRow["DIAGNOSE_VCHR"].ToString();
                        p_objResultArr[i1].m_strDIAGNOSE_XML_VCHR = objDataRow["DIAGNOSE_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strCURE_CIRCS_VCHR = objDataRow["CURE_CIRCS_VCHR"].ToString();
                        p_objResultArr[i1].m_strCURE_CIRCS_XML_VCHR = objDataRow["CURE_CIRCS_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strCT_RESULT_VCHR = objDataRow["CT_RESULT_VCHR"].ToString();
                        p_objResultArr[i1].m_strCT_RESULT_XML_VCHR = objDataRow["CT_RESULT_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strMRI_RESULT_VCHR = objDataRow["MRI_RESULT_VCHR"].ToString();
                        p_objResultArr[i1].m_strMRI_RESULT_XML_VCHR = objDataRow["MRI_RESULT_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strX_RAY_RESULT_VCHR = objDataRow["X_RAY_RESULT_VCHR"].ToString();
                        p_objResultArr[i1].m_strX_RAY_RESULT_XML_VCHR = objDataRow["X_RAY_RESULT_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strEKG_RESULT_VCHR = objDataRow["EKG_RESULT_VCHR"].ToString();
                        p_objResultArr[i1].m_strEKG_RESULT_XML_VCHR = objDataRow["EKG_RESULT_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strBUS_RESULT_VCHR = objDataRow["BUS_RESULT_VCHR"].ToString();
                        p_objResultArr[i1].m_strBUS_RESULT_XML_VCHR = objDataRow["BUS_RESULT_XML_VCHR"].ToString();
                    }


                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region  获得TCD报告单,返回DataTable
        /// <summary>
        /// 用于生成TCD报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strDept"></param>
        /// <param name="p_objResultDtb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTCDReportDtb(string p_strFromDat, string p_strToDat, string p_strDept, out DataTable p_objResultDtb, string strFirstType, string strFirstValue, string strLastType, string strLastValue, bool flag)
        {
            long lngRes = 0;
            p_objResultDtb = new DataTable();
            string strSQL = @"select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_tcd_report where status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + "','yyyy-mm-dd hh24:mi:ss') order by report_no_chr";
            if (p_strDept != "")
            {
                strSQL += p_strDept;
            }
            if (flag == false)//按年龄段查询
            {
                if (strFirstValue != "" && strLastValue != "")
                {
                    strSQL = @"select distinct * from(
                select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_tcd_report 
                where 
                substr(age_flt,1,1) = '" + strFirstType + @"' and substr(age_flt,2)>=" + strFirstValue + @" and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss')
                union
                select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_tcd_report 
                where 
                substr(age_flt,1,1) > '" + strFirstType + @"' and substr(age_flt,1,1)<'" + strLastType + @"' and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss')
                union
                select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_tcd_report 
                where 
                substr(age_flt,1,1) =  '" + strLastType + @"' and substr(age_flt,2)<=" + strLastValue + @" and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss')
                ) order by report_no_chr";


                    if (strFirstType == strLastType)
                    {
                        strSQL = @"select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_tcd_report 
                where 
                substr(age_flt,1,1) = '" + strFirstType + "' and substr(age_flt,2)>=" + strFirstValue + " and substr(age_flt,2)<=" + strLastValue + " and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss') order by report_no_chr";
                    }
                }
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                for (int i = 0; i < dtbResult.Rows.Count; i++)//把代码转变为年龄
                {
                    dtbResult.Rows[i]["AGE_FLT"] = m_mthAgeChange(dtbResult.Rows[i]["AGE_FLT"].ToString());
                }
                p_objResultDtb = dtbResult;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        /// <summary>
        /// 年龄转换
        /// </summary>
        /// <param name="strage"></param>
        private string m_mthAgeChange(string strage)
        {
            int length = strage.Length;
            string strTextAge = "1";
            string strCmbAge = "岁";
            strCmbAge = strage.Substring(0, 1);//年龄单位
            switch (strCmbAge.Trim())
            {
                case "C":
                    strCmbAge = "岁";
                    break;
                case "B":
                    strCmbAge = "月";
                    break;
                case "A":
                    strCmbAge = "天";
                    break;
            }
            strTextAge = strage.Substring(1, length - 1);
            return strTextAge + strCmbAge;
        }
        #endregion

        #region 根据就诊卡号返回病人诊断信息
        /// <summary>
        /// 根据就诊卡号返回病人诊断信息
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_strDiagByCardId(string p_strCard, out string p_strResult)
        {

            long lngRes = 0;
            p_strResult = "";
            string Sql = "select diagnose from ar_common_apply where cardno=? order by applydate desc";
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                DataTable dt = new DataTable();
                System.Data.IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].DbType = DbType.String;
                parm[0].Value = p_strCard;
                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    p_strResult = dt.Rows[0][0].ToString().Trim();
                }
                else
                {
                    Sql = @"select a.clinicalDiagnosis from eafInterface a where a.cardnumber = ? order by a.requisitionID desc";
                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].DbType = DbType.String;
                    parm[0].Value = p_strCard;
                    lngRes = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                    if (lngRes > 0 && dt.Rows.Count > 0)
                    {
                        p_strResult = dt.Rows[0][0].ToString().Trim();
                    }
                }
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
            return lngRes;
        }

        #endregion

        #region 获得脑电图EEG报告单
        /// <summary>
        /// 获得脑电图EEG报告单
        /// </summary>
        [AutoComplete]
        public long m_lngGetEEGReportArr(out clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            p_objResultArr = new clsRIS_EEG_REPORT_VO[0];
            long lngRes = 0;
            string strFromDat = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            string strToDat = System.DateTime.Now.ToShortDateString() + " 23:59:59";
            //string strSQL = @"SELECT * FROM T_OPR_RIS_EEG_REPORT WHERE STATUS_INT = '1'AND CHECK_DAT BETWEEN TO_DATE('"+strFromDat+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('"+strToDat+"','yyyy-mm-dd hh24:mi:ss')";
            string strSQL = @"select a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.check_dat,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.diagnose_vchr,
       a.diagnose_xml_vchr,
       a.left_right,
       a.before_check,
       a.body_stat,
       a.sense_stat,
       a.drug_stat, b.patientcardid_chr from t_opr_ris_eeg_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
  from t_bse_patientcard
         where status_int = 1 or status_int = 3) b where a.patient_id_chr=b.patientid_chr(+)  and a.status_int = '1'and a.check_dat between ? and ?";
            //select a.*, b.patientcardid_chr from T_OPR_RIS_TCD_REPORT a,T_bse_patientcard b WHERE  a.patient_id_chr=b.patientid_chr and (b.status_int=1 or b.status_int=3) and a.STATUS_INT = '1'AND a.CHECK_DAT BETWEEN TO_DATE('"+strFromDat+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('"+strToDat+"','yyyy-mm-dd hh24:mi:ss') ";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objArr);
                objArr[0].DbType = DbType.DateTime;
                objArr[0].Value = DateTime.Parse(strFromDat);
                objArr[1].DbType = DbType.DateTime;
                objArr[1].Value = DateTime.Parse(strToDat);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    //objHRPSvc.Dispose();
                    //if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    //{
                    p_objResultArr = new clsRIS_EEG_REPORT_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_EEG_REPORT_VO();
                        p_objResultArr[i1].m_strCARD_ID_CHR = objDataRow["patientcardid_chr"].ToString();
                        p_objResultArr[i1].m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(objDataRow["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = objDataRow["DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDIAGNOSE_XML_VCHR = objDataRow["DIAGNOSE_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strLEFT_RIGHT = objDataRow["LEFT_RIGHT"].ToString().Trim();
                        p_objResultArr[i1].m_strBEFORE_CHECK = objDataRow["BEFORE_CHECK"].ToString().Trim();
                        p_objResultArr[i1].m_strBODY_STAT = objDataRow["BODY_STAT"].ToString().Trim();
                        p_objResultArr[i1].m_strSENSE_STAT = objDataRow["SENSE_STAT"].ToString().Trim();
                        p_objResultArr[i1].m_strDRUG_STAT = objDataRow["DRUG_STAT"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获得脑电图EEG报告单BYID
        /// <summary>
        ///获得脑电图EEG报告单BYID
        /// </summary>
        [AutoComplete]
        public long m_lngGetEEGReportByID(string p_strID, out clsRIS_EEG_REPORT_VO p_objResult)
        {
            p_objResult = new clsRIS_EEG_REPORT_VO();
            long lngRes = 0;
            string strSQL = @"select a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.check_dat,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.diagnose_vchr,
       a.diagnose_xml_vchr,
       a.left_right,
       a.before_check,
       a.body_stat,
       a.sense_stat,
       a.drug_stat
  from t_opr_ris_eeg_report
 where status = '1' and id = ?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = p_strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                int intDataRowCount = dtbResult.Rows.Count;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);              
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    System.Data.DataRow objDataRow = dtbResult.Rows[0];
                    p_objResult = new clsRIS_EEG_REPORT_VO();
                    p_objResult.m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                    p_objResult.m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                    p_objResult.m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                    p_objResult.m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                    p_objResult.m_strCHECK_DAT = Convert.ToDateTime(objDataRow["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                    p_objResult.m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                    p_objResult.m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                    p_objResult.m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strDIAGNOSE_VCHR = objDataRow["DIAGNOSE_VCHR"].ToString().Trim();
                    p_objResult.m_strDIAGNOSE_XML_VCHR = objDataRow["DIAGNOSE_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strLEFT_RIGHT = objDataRow["LEFT_RIGHT"].ToString().Trim();
                    p_objResult.m_strBEFORE_CHECK = objDataRow["BEFORE_CHECK"].ToString().Trim();
                    p_objResult.m_strBODY_STAT = objDataRow["BODY_STAT"].ToString().Trim();
                    p_objResult.m_strSENSE_STAT = objDataRow["SENSE_STAT"].ToString().Trim();
                    p_objResult.m_strDRUG_STAT = objDataRow["DRUG_STAT"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 根据条件组合查询脑电图EEG报告
        /// <summary>
        /// 根据条件组合查询脑电图EEG报告  2004.06.23
        /// </summary>
        //public long m_lngGetEEGReportByCondition(System.Security.Principal.IPrincipal p_objPrincipal,string p_strFromDat,string p_strToDat,
        [AutoComplete]
        public long m_lngGetEEGReportByCondition(string p_strFromDat, string p_strToDat,
            string p_strPatientNo, string p_strInPatientNo, string p_strPatientName, string p_strDept, string p_strReportNo, string strReporter, out clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;

            //string strSQL = @"SELECT * FROM T_OPR_RIS_EEG_REPORT WHERE STATUS_INT = '1'";
            string strSQL = @"select a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.check_dat,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.diagnose_vchr,
       a.diagnose_xml_vchr,
       a.left_right,
       a.before_check,
       a.body_stat,
       a.sense_stat,
       a.drug_stat, b.patientcardid_chr from t_opr_ris_eeg_report a,(select *
          from t_bse_patientcard
         where status_int = 1 or status_int = 3) b where a.patient_id_chr=b.patientid_chr(+)  and a.status_int = '1'";

            DataTable dtbResult = new DataTable();
            if (p_strFromDat != "" && p_strToDat != "")
            {
                strSQL += " and a.report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + "','yyyy-mm-dd hh24:mi:ss')";
            }
            if (p_strPatientNo != "")
            {
                strSQL += " and a.patient_no_chr = '" + p_strPatientNo + "'";
            }
            if (p_strInPatientNo != "")
            {
                strSQL += " and a.inpatient_no_chr = '" + p_strInPatientNo + "'";
            }
            if (p_strPatientName != "")
            {
                strSQL += " and a.patient_name_vchr = '" + p_strPatientName + "'";
            }
            if (p_strDept != "")
            {
                strSQL += " and a.dept_name_vchr = '" + p_strDept + "'";
            }
            if (p_strReportNo != "")
            {
                strSQL += " and a.report_no_chr = '" + p_strReportNo + "'";
            }
            if (!string.IsNullOrEmpty(strReporter))
            {
                strSQL += " and a.reportor_name_vchr = '" + strReporter + "'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {

                    p_objResultArr = new clsRIS_EEG_REPORT_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_EEG_REPORT_VO();
                        p_objResultArr[i1].m_strCARD_ID_CHR = objDataRow["patientcardid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(objDataRow["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = objDataRow["DIAGNOSE_VCHR"].ToString();
                        p_objResultArr[i1].m_strDIAGNOSE_XML_VCHR = objDataRow["DIAGNOSE_XML_VCHR"].ToString();
                        p_objResultArr[i1].m_strLEFT_RIGHT = objDataRow["LEFT_RIGHT"].ToString().Trim();
                        p_objResultArr[i1].m_strBEFORE_CHECK = objDataRow["BEFORE_CHECK"].ToString().Trim();
                        p_objResultArr[i1].m_strBODY_STAT = objDataRow["BODY_STAT"].ToString().Trim();
                        p_objResultArr[i1].m_strSENSE_STAT = objDataRow["SENSE_STAT"].ToString().Trim();
                        p_objResultArr[i1].m_strDRUG_STAT = objDataRow["DRUG_STAT"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获得EEG脑电图报告单 用于生成EEG报表
        /// <summary>
        /// 用于生成EEG报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strDept">SQL条件</param>
        /// <param name="p_objResultDtb"></param>
        /// <param name="strFirstType">第一年龄单位</param>
        /// <param name="strFirstValue">第一年龄值</param>
        /// <param name="strLastType">第二年龄单位</param>
        /// <param name="strLastValue">第二年龄值</param>
        /// <param name="flag">true表示一般查询,false表示按年龄段查询</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEEGReportDtb(string p_strFromDat, string p_strToDat, string p_strDept, out DataTable p_objResultDtb, string strFirstType, string strFirstValue, string strLastType, string strLastValue, bool flag)
        {
            long lngRes = 0;
            p_objResultDtb = new DataTable();
            string strSQL = @"select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_eeg_report where status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + "','yyyy-mm-dd hh24:mi:ss') order by report_no_chr";
            if (p_strDept != "")
            {
                strSQL += p_strDept;
            }
            if (flag == false)
            {
                if (strFirstValue != "" && strLastValue != "")
                {
                    strSQL = @"select distinct * from(
                select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_eeg_report 
                where 
                substr(age_flt,1,1) = '" + strFirstType + @"' and substr(age_flt,2)>=" + strFirstValue + @" and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss')
                union
                select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_eeg_report 
                where 
                substr(age_flt,1,1) > '" + strFirstType + @"' and substr(age_flt,1,1)<'" + strLastType + @"' and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss')
                union
                select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_eeg_report 
                where 
                substr(age_flt,1,1) =  '" + strLastType + @"' and substr(age_flt,2)<=" + strLastValue + @" and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss')
                ) order by report_no_chr";


                    if (strFirstType == strLastType)
                    {
                        strSQL = @"select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,diagnose_vchr,summary1_vchr,summary2_vchr from t_opr_ris_eeg_report 
                where 
                substr(age_flt,1,1) = '" + strFirstType + "' and substr(age_flt,2)>=" + strFirstValue + " and substr(age_flt,2)<=" + strLastValue + " and status_int = '1'and report_dat between to_date('" + p_strFromDat + "','yyyy-mm-dd hh24:mi:ss') and to_date('" + p_strToDat + @"','yyyy-mm-dd hh24:mi:ss') order by report_no_chr";
                    }
                }


            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                for (int i = 0; i < dtbResult.Rows.Count; i++)
                {
                    dtbResult.Rows[i]["age_flt"] = m_mthAgeChange(dtbResult.Rows[i]["age_flt"].ToString());
                }
                p_objResultDtb = dtbResult;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        #endregion

        #region 获取送检部门数据
        /// <summary>
        /// 获取送检部门数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="PartName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptData(out DataTable PartName)
        {
            long lngRes = 0;
            System.Data.DataTable p_dtbReportType = null;
            PartName = null;
            string strSQL = @"select  code_vchr as " + "\"" + "编 号" + "\"" + ",deptname_vchr as " + "\"" + "部     门     名     称" + "\"" + ",pycode_chr as 拼音码,deptid_chr from t_bse_deptdesc where  category_int = 0 order by code_vchr";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbReportType);
                PartName = p_dtbReportType;
            }
            catch (System.Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 获取根据部门取医生数据
        /// <summary>
        /// 获取根据部门取医生数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="PartName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoctorData(out DataTable PartName, string p_strDeptID, bool p_blnHasPrescriptionRight)
        {
            long lngRes = 0;
            System.Data.DataTable p_dtbReportType = null;
            PartName = null;
            string strSQL = @"select distinct e.empno_chr as 编号, e.lastname_vchr as 医生姓名,
                e.pycode_chr as 拼音码, e.empid_chr
           from t_bse_employee e, t_bse_deptemp d
           where e.status_int = 1
           and e.empid_chr(+) = d.empid_chr";

            if (p_blnHasPrescriptionRight)
            {
                strSQL += @" and e.hasprescriptionright_chr = '1'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strDeptID.Trim() != "")
                {
                    p_strDeptID = p_strDeptID.PadLeft(7, '0');
                    string strWhere = " and d.deptid_chr=?";
                    strSQL += strWhere;
                    IDataParameter[] Param = null;
                    objHRPSvc.CreateDatabaseParameter(1, out Param);
                    Param[0].Value = p_strDeptID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbReportType, Param);
                }
                else
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbReportType);
                }
                PartName = p_dtbReportType;
            }
            catch (System.Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region 根据住院号获取病人资料
        /// <summary>
        /// 根据住院号获取病人资料
        /// </summary>
        /// <param name="strInPatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatByInPatientID(string strInPatientID, out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;

            string strSQL = @"select a.patientid_chr,
       b.lastname_vchr,
       b.sex_chr,
       b.firstname_vchr,
       b.birth_dat,
       b.occupation_vchr,
       c.deptid_chr,
       c.deptname_vchr,
       e.bed_no,
       e.bedid_chr
  from t_bse_patientcard  a,
       t_bse_patient      b,
       t_bse_deptdesc     c,
       t_opr_bih_register d,
       t_bse_bed          e
 where a.patientid_chr = b.patientid_chr
   and b.inpatientid_chr=d.inpatientid_chr
   and d.deptid_chr=c.deptid_chr
   and d.bedid_chr=e.bedid_chr
   and b.inpatientid_chr = ?
   and (a.status_int = 1 or a.status_int = 3)";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = strInPatientID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;

        }
        #endregion
    }
}
