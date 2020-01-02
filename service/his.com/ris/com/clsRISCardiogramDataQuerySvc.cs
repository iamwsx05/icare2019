using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.RIS
{
    /// <summary>
    /// 茶山心电图代码查询专用中间件
    /// add by huafeng.xiao
    /// 2008年9月28日15:46:46
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsRISCardiogramDataQuerySvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获得心电图报告
        /// <summary>
        /// 获得心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCardiogramReportArr(out clsRIS_CardiogramReport_VO[] p_objResultArr)
        {
            p_objResultArr = new clsRIS_CardiogramReport_VO[0];
            long lngRes = 0;
            string strFromDat = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            string strToDat = System.DateTime.Now.ToShortDateString() + " 23:59:59";

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
                                   a.clinical_diagnose_vchr,
                                   a.rhythm_vchr,
                                   a.heart_rate_vchr,
                                   a.p_r_vchr,
                                   a.qrs_vchr,
                                   a.q_t_vchr,
                                   a.summary1_vchr,
                                   a.summary2_vchr,
                                   a.reportor_id_chr,
                                   a.reportor_name_vchr,
                                   a.confirmer_id_chr,
                                   a.confirmer_name_vchr,
                                   a.heart_room_vchr,
                                   a.status_int,
                                   a.operator_id_chr,
                                   a.summary1_xml_vchr,
                                   a.summary2_xml_vchr,
                                   a.specialflag_int,
                                   a.e_axes_vchr,
                                   a.applyid_int,
                                   b.patientcardid_chr,
                                   a.applydoctor_name_vchr,
                                   a.applydoctor_id_vchr
                              from t_opr_ris_cardiogram_report a,
                                   (select patientcardid_chr, patientid_chr, issue_date, status_int
                                      from t_bse_patientcard
                                     where status_int = 1
                                        or status_int = 3) b,
                                   ar_common_apply c
                             where a.patient_id_chr = b.patientid_chr(+)
                               and a.applyid_int = c.applyid(+)
                               and a.status_int = '1'
                               and a.check_dat between ? and ?";
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
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    p_objResultArr = new clsRIS_CardiogramReport_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_CardiogramReport_VO();
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
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRHYTHM_VCHR = objDataRow["RHYTHM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_RATE_VCHR = objDataRow["HEART_RATE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strP_R_VCHR = objDataRow["P_R_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQRS_VCHR = objDataRow["QRS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQ_T_VCHR = objDataRow["Q_T_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString();//.ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString();//.ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_ROOM_VCHR = objDataRow["HEART_ROOM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strE_Axes_vchr = objDataRow["E_AXES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strApplyDoctorName = objDataRow["applydoctor_name_vchr"].ToString().Trim();
                        p_objResultArr[i1].m_strApplyDoctorID = objDataRow["applydoctor_id_vchr"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = int.Parse(objDataRow["SPECIALFLAG_INT"].ToString().Trim());

                        }
                        catch
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = 0;
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
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        #endregion

        #region 获得运动心电图
        /// <summary>
        /// 获得运动心电图
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArrSport"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSportReportArr(out clsafmt_report_VO[] p_objResultArrSport)
        {
            p_objResultArrSport = new clsafmt_report_VO[0];
            long lngRes = 0;
            string strFromDat = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            string strToDat = System.DateTime.Now.ToShortDateString() + " 23:59:59";
            //string strSQL = @"SELECT * FROM t_opr_ris_afmt_report WHERE  STATUS_INT = '0' and REPORT_DAT BETWEEN TO_DATE('"+strFromDat+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('"+strToDat+"','yyyy-mm-dd hh24:mi:ss')";
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
       a.clinical_diagnose_vchr,
       a.rhythm_vchr,
       a.heart_rate_vchr,
       a.p_r_vchr,
       a.qrs_vchr,
       a.q_t_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.specialflag_int,
       a.lie_pst_vchr,
       a.stand_pst_vchr,
       a.deep_breath_vchr,
       a.before_active_vchr,
       a.forecast_qty_int,
       a.forecast_qty_vchr,
       a.test_plan_vchr,
       a.active_load_level_vchr,
       a.active_load_mph_vchr,
       a.active_load_per_vchr,
       a.active_total_time_vchr,
       a.hr_top_vchr,
       a.hr_per_vchr,
       a.hr_max_work_vchr,
       a.stop_reason_vchr,
       a.active_st_int,
       a.active_st_vchr,
       a.active_st_mode_vchr,
       a.appear_led_vchr,
       a.appear_led_xml_vchr,
       a.hr_scope_vchr,
       a.hr_scope_xml_vchr,
       a.time_scope_vchr,
       a.time_scope_xml_vchr,
       a.active_st_max_int,
       a.active_st_max_vchr,
       a.active_st_max_led_vchr,
       a.active_st_max_time_vchr,
       a.hr_wrong_vchr,
       a.hr_wrong_xml_vchr,
       a.actived_bp_vchr,
       a.active_result_vchr,
       a.active_result_xml_vchr,
       a.test_result_vchr,
       a.test_result_xml_vchr,b.patientcardid_chr from t_opr_ris_afmt_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
          from t_bse_patientcard
         where status_int = 1 or status_int = 3) b where a.patient_id_chr=b.patientid_chr(+)   and a.status_int = '0' and a.report_dat between ? and ?";

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
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    p_objResultArrSport = new clsafmt_report_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArrSport[i1] = new clsafmt_report_VO();
                        p_objResultArrSport[i1].m_strCARD_ID_CHR = objDataRow["patientcardid_chr"].ToString();
                        p_objResultArrSport[i1].m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArrSport[i1].m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                        p_objResultArrSport[i1].m_strCHECK_DAT = Convert.ToDateTime(objDataRow["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArrSport[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArrSport[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArrSport[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strRHYTHM_VCHR = objDataRow["RHYTHM_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHEART_RATE_VCHR = objDataRow["HEART_RATE_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strP_R_VCHR = objDataRow["P_R_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strQRS_VCHR = objDataRow["QRS_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strQ_T_VCHR = objDataRow["Q_T_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArrSport[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_intSPECIALFLAG_INT = Convert.ToInt32(objDataRow["SPECIALFLAG_INT"].ToString().Trim());
                        p_objResultArrSport[i1].m_strLIE_PST_VCHR = objDataRow["LIE_PST_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strSTAND_PST_VCHR = objDataRow["STAND_PST_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strDEEP_BREATH_VCHR = objDataRow["DEEP_BREATH_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strBEFORE_ACTIVE_VCHR = objDataRow["BEFORE_ACTIVE_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_intFORECAST_QTY_INT = Convert.ToInt32(objDataRow["FORECAST_QTY_INT"].ToString().Trim());
                        p_objResultArrSport[i1].m_strFORECAST_QTY_VCHR = objDataRow["FORECAST_QTY_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strTEST_PLAN_VCHR = objDataRow["TEST_PLAN_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_LOAD_LEVEL_VCHR = objDataRow["ACTIVE_LOAD_LEVEL_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_LOAD_MPH_VCHR = objDataRow["ACTIVE_LOAD_MPH_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_LOAD_PER_VCHR = objDataRow["ACTIVE_LOAD_PER_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_TOTAL_TIME_VCHR = objDataRow["ACTIVE_TOTAL_TIME_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_TOP_VCHR = objDataRow["HR_TOP_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_PER_VCHR = objDataRow["HR_PER_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_MAX_WORK_VCHR = objDataRow["HR_MAX_WORK_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strSTOP_REASON_VCHR = objDataRow["STOP_REASON_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_intACTIVE_ST_INT = Convert.ToInt32(objDataRow["ACTIVE_ST_INT"].ToString().Trim());
                        p_objResultArrSport[i1].m_strACTIVE_ST_VCHR = objDataRow["ACTIVE_ST_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_ST_MODE_VCHR = objDataRow["ACTIVE_ST_MODE_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strAPPEAR_LED_VCHR = objDataRow["APPEAR_LED_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strAPPEAR_LED_XML_VCHR = objDataRow["APPEAR_LED_XML_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_SCOPE_VCHR = objDataRow["HR_SCOPE_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_SCOPE_XML_VCHR = objDataRow["HR_SCOPE_XML_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strTIME_SCOPE_VCHR = objDataRow["TIME_SCOPE_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strTIME_SCOPE_XML_VCHR = objDataRow["TIME_SCOPE_XML_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_intACTIVE_ST_MAX_INT = Convert.ToInt32(objDataRow["ACTIVE_ST_MAX_INT"].ToString().Trim());
                        p_objResultArrSport[i1].m_strACTIVE_ST_MAX_VCHR = objDataRow["ACTIVE_ST_MAX_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_ST_MAX_LED_VCHR = objDataRow["ACTIVE_ST_MAX_LED_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_ST_MAX_TIME_VCHR = objDataRow["ACTIVE_ST_MAX_TIME_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_WRONG_VCHR = objDataRow["HR_WRONG_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strHR_WRONG_XML_VCHR = objDataRow["HR_WRONG_XML_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVED_BP_VCHR = objDataRow["ACTIVED_BP_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_RESULT_VCHR = objDataRow["ACTIVE_RESULT_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strACTIVE_RESULT_XML_VCHR = objDataRow["ACTIVE_RESULT_XML_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strTEST_RESULT_VCHR = objDataRow["TEST_RESULT_VCHR"].ToString().Trim();
                        p_objResultArrSport[i1].m_strTEST_RESULT_XML_VCHR = objDataRow["TEST_RESULT_XML_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArrSport[i1].m_intSPECIALFLAG_INT = int.Parse(objDataRow["SPECIALFLAG_INT"].ToString().Trim());

                        }
                        catch
                        {
                            p_objResultArrSport[i1].m_intSPECIALFLAG_INT = 0;
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
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }

        #endregion

        #region 通过ID获得心电图报告
        /// <summary>
        /// 获得心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCardiogramReportByID(string p_strID, out clsRIS_CardiogramReport_VO p_objResult)
        {
            p_objResult = new clsRIS_CardiogramReport_VO();
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
                                   a.clinical_diagnose_vchr,
                                   a.rhythm_vchr,
                                   a.heart_rate_vchr,
                                   a.p_r_vchr,
                                   a.qrs_vchr,
                                   a.q_t_vchr,
                                   a.summary1_vchr,
                                   a.summary2_vchr,
                                   a.reportor_id_chr,
                                   a.reportor_name_vchr,
                                   a.confirmer_id_chr,
                                   a.confirmer_name_vchr,
                                   a.heart_room_vchr,
                                   a.status_int,
                                   a.operator_id_chr,
                                   a.summary1_xml_vchr,
                                   a.summary2_xml_vchr,
                                   a.specialflag_int,
                                   a.e_axes_vchr,
                                   a.applyid_int,
                                   b.patientcardid_chr,
                                   c.doctorname
                              from t_opr_ris_cardiogram_report a
                             right join t_bse_patientcard b
                                on a.patient_id_chr = b.patientid_chr
                              left join ar_common_apply c
                                on a.applyid_int = c.applyid
                             where a.status_int = '1'
                               and (b.status_int = 1 or b.status_int = 3)
                               and a.report_id_chr = ?";
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
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    System.Data.DataRow objDataRow = dtbResult.Rows[0];
                    p_objResult = new clsRIS_CardiogramReport_VO();
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
                    p_objResult.m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                    p_objResult.m_strRHYTHM_VCHR = objDataRow["RHYTHM_VCHR"].ToString().Trim();
                    p_objResult.m_strHEART_RATE_VCHR = objDataRow["HEART_RATE_VCHR"].ToString().Trim();
                    p_objResult.m_strP_R_VCHR = objDataRow["P_R_VCHR"].ToString().Trim();
                    p_objResult.m_strQRS_VCHR = objDataRow["QRS_VCHR"].ToString().Trim();
                    p_objResult.m_strQ_T_VCHR = objDataRow["Q_T_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strHEART_ROOM_VCHR = objDataRow["HEART_ROOM_VCHR"].ToString().Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strApplyDoctorName = objDataRow["DOCTORNAME"].ToString().Trim();

                    p_objResult.m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strE_Axes_vchr = objDataRow["E_AXES_VCHR"].ToString().Trim();

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

        #region 根据条件组合查询心电图报告 
        /// <summary>
        /// 根据条件组合查询心电图报告 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strPatientNo"></param>
        /// <param name="p_strInPatientNo"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strDept"></param>
        /// <param name="p_strReportNo"></param>
        /// <param name="strIsSpecail"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCardiogramReportByCondition(string p_strFromDat, string p_strToDat,
           string p_strPatientNo, string p_strInPatientNo, string p_strPatientName, string p_strDept, string p_strReportNo, string strIsSpecail, string strReporter, out clsRIS_CardiogramReport_VO[] p_objResultArr)
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
                                   a.clinical_diagnose_vchr,
                                   a.rhythm_vchr,
                                   a.heart_rate_vchr,
                                   a.p_r_vchr,
                                   a.qrs_vchr,
                                   a.q_t_vchr,
                                   a.summary1_vchr,
                                   a.summary2_vchr,
                                   a.reportor_id_chr,
                                   a.reportor_name_vchr,
                                   a.confirmer_id_chr,
                                   a.confirmer_name_vchr,
                                   a.heart_room_vchr,
                                   a.status_int,
                                   a.operator_id_chr,
                                   a.summary1_xml_vchr,
                                   a.summary2_xml_vchr,
                                   a.specialflag_int,
                                   a.e_axes_vchr,
                                   a.applyid_int,
                                   b.patientcardid_chr,
                                   c.doctorname
                              from t_opr_ris_cardiogram_report a,
                                   (select patientcardid_chr, patientid_chr, issue_date, status_int
                                      from t_bse_patientcard
                                     where status_int = 1
                                        or status_int = 3) b,
                                   ar_common_apply c
                             where a.status_int = '1'
                               and a.patient_id_chr = b.patientid_chr(+)
                               and a.applyid_int = c.applyid(+) 
                               ";
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
                strSQL += " and a.patient_name_vchr like  '%" + p_strPatientName + "%'";
            }
            if (p_strDept != "")
            {
                strSQL += " and dept_name_vchr like '" + p_strDept + "%'";
            }
            if (p_strReportNo != "")
            {
                strSQL += " and a.report_no_chr = '" + p_strReportNo + "'";
            }
            if (strIsSpecail != "")
            {
                strSQL += " and a.specialflag_int = '" + strIsSpecail + "'";
            }
            if (!string.IsNullOrEmpty(strReporter))
            {
                strSQL += " and a.reportor_name_vchr='" + strReporter + "'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    p_objResultArr = new clsRIS_CardiogramReport_VO[intDataRowCount];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_CardiogramReport_VO();
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
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRHYTHM_VCHR = objDataRow["RHYTHM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_RATE_VCHR = objDataRow["HEART_RATE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strP_R_VCHR = objDataRow["P_R_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQRS_VCHR = objDataRow["QRS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQ_T_VCHR = objDataRow["Q_T_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_ROOM_VCHR = objDataRow["HEART_ROOM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strE_Axes_vchr = objDataRow["E_AXES_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strApplyDoctorName = objDataRow["DOCTORNAME"].ToString().Trim();
                        if (objDataRow["APPLYID_INT"] != DBNull.Value)
                            p_objResultArr[i1].m_intApplyID = Convert.ToInt32(objDataRow["APPLYID_INT"].ToString().Trim());

                        try
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = int.Parse(objDataRow["SPECIALFLAG_INT"].ToString().Trim());
                        }
                        catch
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = 0;
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
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 根据条件产生Table,用于报表
        /// <summary>
        /// 根据条件产生Table,用于报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strDept"></param>
        /// <param name="strDiagnoses"></param>
        /// <param name="p_objResultDtb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCardiogramdbt(string p_strFromDat, string p_strToDat, string p_strDept, string strDiagnoses, out DataTable p_objResultDtb)
        {
            long lngRes = 0;
            p_objResultDtb = new DataTable();
            string strSQL = @"select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,clinical_diagnose_vchr,dept_name_vchr as summary1_vchr,summary2_vchr,specialflag_int from t_opr_ris_cardiogram_report where status_int = '1'and report_dat between ? and ?";
            if (p_strDept != "")
            {
                strSQL += "AND DEPT_NAME_VCHR=?";
            }
            if (strDiagnoses != "")
            {
                strSQL += "AND SUMMARY2_VCHR like %?%";
            }
            int intI = 2;
            if (!string.IsNullOrEmpty(p_strDept))
            {
                intI = 3;
                if (!string.IsNullOrEmpty(strDiagnoses))
                {
                    intI = 4;
                }
            }
            else if (!string.IsNullOrEmpty(strDiagnoses))
            {
                intI = 3;
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;

                objHRPSvc.CreateDatabaseParameter(intI, out objArr);
                objArr[0].DbType = DbType.DateTime;
                objArr[0].Value = DateTime.Parse(p_strFromDat);
                objArr[1].DbType = DbType.DateTime;
                objArr[1].Value = DateTime.Parse(p_strToDat);
                if (!string.IsNullOrEmpty(p_strDept))
                {
                    objArr[2].DbType = DbType.String;
                    objArr[2].Value = p_strDept;
                    if (!string.IsNullOrEmpty(strDiagnoses))
                    {
                        objArr[3].DbType = DbType.String;
                        objArr[3].Value = strDiagnoses;
                    }
                }
                else if (!string.IsNullOrEmpty(strDiagnoses))
                {
                    objArr[2].DbType = DbType.String;
                    objArr[2].Value = strDiagnoses;
                }
                DataTable dtbResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                // lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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

        #region 获得动态心电图
        /// <summary>
        /// 获得动态心电图
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDCardiogramReportArr(out clsRIS_DCardiogramReport_VO[] p_objResultArr)
        {
            p_objResultArr = new clsRIS_DCardiogramReport_VO[0];
            long lngRes = 0;
            string strFromDat = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            string strToDat = System.DateTime.Now.ToShortDateString() + " 23:59:59";
            //string strSQL = @"SELECT * FROM T_OPR_RIS_DCARDIOGRAM_REPORT WHERE STATUS_INT = '1' AND REPORT_DAT BETWEEN TO_DATE('"+strFromDat+"','yyyy-mm-dd hh24:mi:ss') AND TO_DATE('"+strToDat+"','yyyy-mm-dd hh24:mi:ss')";
            string strSQL = @"select a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.clinical_diagnose_vchr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.heart_room_vchr,
       a.status_int,
       a.operator_id_chr,
       a.checkfrom_dat,
       a.checkto_dat,
       a.check_channels_vchr,
       a.graph_type_int,
       a.qrs_total_chr,
       a.heartrate_average_int,
       a.heartrate_max_int,
       a.heartrate_min_int,
       a.heartrate_max_dat,
       a.heartrate_min_dat,
       a.heartrate_base_int,
       a.check_channels_xml_vchr,
       a.clinical_diagnose_xml_vchr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.specialflag_int,
       a.heartrate_base_vchr,b.patientcardid_chr from t_opr_ris_dcardiogram_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
          from t_bse_patientcard
         where status_int = 1 or status_int = 3) b  where a.patient_id_chr=b.patientid_chr(+) and a.status_int = '1' and  a.status_int = '1' and a.report_dat between ? and ?";
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
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objResultArr = new clsRIS_DCardiogramReport_VO[intRowCount];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_DCardiogramReport_VO();
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
                        p_objResultArr[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString();//.Trim();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString();//.Trim();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_ROOM_VCHR = objDataRow["HEART_ROOM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECKFROM_DAT = Convert.ToDateTime(objDataRow["CHECKFROM_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCHECKTO_DAT = Convert.ToDateTime(objDataRow["CHECKTO_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCHECK_CHANNELS_VCHR = objDataRow["CHECK_CHANNELS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intGRAPH_TYPE_INT = Convert.ToInt32(objDataRow["GRAPH_TYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strQRS_TOTAL_CHR = objDataRow["QRS_TOTAL_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intHEARTRATE_AVERAGE_INT = Convert.ToInt32(objDataRow["HEARTRATE_AVERAGE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intHEARTRATE_MAX_INT = Convert.ToInt32(objDataRow["HEARTRATE_MAX_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intHEARTRATE_MIN_INT = Convert.ToInt32(objDataRow["HEARTRATE_MIN_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strHEARTRATE_MAX_DAT = Convert.ToDateTime(objDataRow["HEARTRATE_MAX_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strHEARTRATE_MIN_DAT = Convert.ToDateTime(objDataRow["HEARTRATE_MIN_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_intHEARTRATE_BASE_INT = Convert.ToInt32(objDataRow["HEARTRATE_BASE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strHEARTRATE_BASE_VCHR = objDataRow["HEARTRATE_BASE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECK_CHANNELS_XML_VCHR = objDataRow["CHECK_CHANNELS_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_XML_VCHR = objDataRow["CLINICAL_DIAGNOSE_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = int.Parse(objDataRow["SPECIALFLAG_INT"].ToString().Trim());

                        }
                        catch
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = 0;
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
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获得动态心电图
        /// <summary>
        /// 获得动态心电图
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDCardiogramReportByID(string p_strID, out clsRIS_DCardiogramReport_VO p_objResult)
        {
            p_objResult = new clsRIS_DCardiogramReport_VO();
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
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.clinical_diagnose_vchr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.heart_room_vchr,
       a.status_int,
       a.operator_id_chr,
       a.checkfrom_dat,
       a.checkto_dat,
       a.check_channels_vchr,
       a.graph_type_int,
       a.qrs_total_chr,
       a.heartrate_average_int,
       a.heartrate_max_int,
       a.heartrate_min_int,
       a.heartrate_max_dat,
       a.heartrate_min_dat,
       a.heartrate_base_int,
       a.check_channels_xml_vchr,
       a.clinical_diagnose_xml_vchr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.specialflag_int,
       a.heartrate_base_vchr
  from t_opr_ris_dcardiogram_report a where a.status_int = '1' and a.report_id_chr = ?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = p_strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                // lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    System.Data.DataRow objDataRow = dtbResult.Rows[0];
                    p_objResult = new clsRIS_DCardiogramReport_VO();
                    p_objResult.m_strREPORT_ID_CHR = objDataRow["REPORT_ID_CHR"].ToString().Trim();
                    p_objResult.m_strMODIFY_DAT = Convert.ToDateTime(objDataRow["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strREPORT_NO_CHR = objDataRow["REPORT_NO_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENT_ID_CHR = objDataRow["PATIENT_ID_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENT_NO_CHR = objDataRow["PATIENT_NO_CHR"].ToString().Trim();
                    p_objResult.m_strINPATIENT_NO_CHR = objDataRow["INPATIENT_NO_CHR"].ToString().Trim();
                    p_objResult.m_strPATIENT_NAME_VCHR = objDataRow["PATIENT_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strSEX_CHR = objDataRow["SEX_CHR"].ToString().Trim();
                    p_objResult.m_strAGE_FLT = objDataRow["AGE_FLT"].ToString().Trim();
                    p_objResult.m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                    p_objResult.m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                    p_objResult.m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                    p_objResult.m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                    p_objResult.m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strHEART_ROOM_VCHR = objDataRow["HEART_ROOM_VCHR"].ToString().Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strCHECKFROM_DAT = Convert.ToDateTime(objDataRow["CHECKFROM_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strCHECKTO_DAT = Convert.ToDateTime(objDataRow["CHECKTO_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strCHECK_CHANNELS_VCHR = objDataRow["CHECK_CHANNELS_VCHR"].ToString().Trim();
                    p_objResult.m_intGRAPH_TYPE_INT = Convert.ToInt32(objDataRow["GRAPH_TYPE_INT"].ToString().Trim());
                    p_objResult.m_strQRS_TOTAL_CHR = objDataRow["QRS_TOTAL_CHR"].ToString().Trim();
                    p_objResult.m_intHEARTRATE_AVERAGE_INT = Convert.ToInt32(objDataRow["HEARTRATE_AVERAGE_INT"].ToString().Trim());
                    p_objResult.m_intHEARTRATE_MAX_INT = Convert.ToInt32(objDataRow["HEARTRATE_MAX_INT"].ToString().Trim());
                    p_objResult.m_intHEARTRATE_MIN_INT = Convert.ToInt32(objDataRow["HEARTRATE_MIN_INT"].ToString().Trim());
                    p_objResult.m_strHEARTRATE_MAX_DAT = Convert.ToDateTime(objDataRow["HEARTRATE_MAX_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_strHEARTRATE_MIN_DAT = Convert.ToDateTime(objDataRow["HEARTRATE_MIN_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intHEARTRATE_BASE_INT = Convert.ToInt32(objDataRow["HEARTRATE_BASE_INT"].ToString().Trim());

                    p_objResult.m_strCHECK_CHANNELS_XML_VCHR = objDataRow["CHECK_CHANNELS_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strCLINICAL_DIAGNOSE_XML_VCHR = objDataRow["CLINICAL_DIAGNOSE_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
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

        #region 根据条件组合查询动态心电图报告
        /// <summary>
        /// 根据条件组合查询动态心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat">起始时间</param>
        /// <param name="p_strToDat">截止时间</param>
        /// <param name="p_strPatientNo">病人ID</param>
        /// <param name="p_strInPatientNo">住院号</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <param name="p_strDept">科室</param>
        /// <param name="p_strReportNo">检查号</param>
        /// <param name="p_objResultArr">输出结果</param>
        /// /// <param name="p_strReportNo">是否特殊病人</param>
        /// <param name="p_objResultArr">报告医师</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDCardiogramReportByCondition(string p_strFromDat, string p_strToDat,
            string p_strPatientNo, string p_strInPatientNo, string p_strPatientName, string p_strDept, string p_strReportNo, string strIsSpecail, string strReporter,
            out clsRIS_DCardiogramReport_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            //string strSQL = @"SELECT * FROM T_OPR_RIS_DCARDIOGRAM_REPORT WHERE STATUS_INT = '1'";
            string strSQL = @"SELECT a.report_id_chr,
       a.modify_dat,
       a.report_no_chr,
       a.patient_id_chr,
       a.patient_no_chr,
       a.inpatient_no_chr,
       a.patient_name_vchr,
       a.sex_chr,
       a.age_flt,
       a.report_dat,
       a.dept_id_chr,
       a.dept_name_vchr,
       a.is_inpatient_int,
       a.bed_id_chr,
       a.bed_no_chr,
       a.clinical_diagnose_vchr,
       a.summary1_vchr,
       a.summary2_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.heart_room_vchr,
       a.status_int,
       a.operator_id_chr,
       a.checkfrom_dat,
       a.checkto_dat,
       a.check_channels_vchr,
       a.graph_type_int,
       a.qrs_total_chr,
       a.heartrate_average_int,
       a.heartrate_max_int,
       a.heartrate_min_int,
       a.heartrate_max_dat,
       a.heartrate_min_dat,
       a.heartrate_base_int,
       a.check_channels_xml_vchr,
       a.clinical_diagnose_xml_vchr,
       a.summary1_xml_vchr,
       a.summary2_xml_vchr,
       a.specialflag_int,
       a.heartrate_base_vchr,b.patientcardid_chr from t_opr_ris_dcardiogram_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
          from t_bse_patientcard
         where status_int = 1 or status_int = 3) b where a.status_int = '1' and a.patient_id_chr=b.patientid_chr(+)  ";
            if (p_strFromDat != "" && p_strToDat != "")
            {
                //DateTime m_dtmFromDat = Convert.ToDateTime(DateTime.Parse(p_strFromDat).ToString("yyyy-MM-dd HH:mm:ss"));
                //DateTime m_dtmToDat = Convert.ToDateTime(DateTime.Parse(p_strToDat).ToString("yyyy-MM-dd HH:mm:ss"));
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
                strSQL += " and a.reportor_name_vchr='" + strReporter + "'";
            }
            if (strIsSpecail != "")
            {
                strSQL += " and a.specialflag_int = '" + strIsSpecail + "'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objResultArr = new clsRIS_DCardiogramReport_VO[intRowCount];
                    for (int i1 = 0; i1 < intRowCount; i1++)
                    {
                        DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsRIS_DCardiogramReport_VO();
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
                        p_objResultArr[i1].m_strREPORT_DAT = Convert.ToDateTime(objDataRow["REPORT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strDEPT_ID_CHR = objDataRow["DEPT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPT_NAME_VCHR = objDataRow["DEPT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intIS_INPATIENT_INT = Convert.ToInt32(objDataRow["IS_INPATIENT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strBED_ID_CHR = objDataRow["BED_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBED_NO_CHR = objDataRow["BED_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_VCHR = objDataRow["SUMMARY1_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_VCHR = objDataRow["SUMMARY2_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_ROOM_VCHR = objDataRow["HEART_ROOM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECKFROM_DAT = Convert.ToDateTime(objDataRow["CHECKFROM_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCHECKTO_DAT = Convert.ToDateTime(objDataRow["CHECKTO_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strCHECK_CHANNELS_VCHR = objDataRow["CHECK_CHANNELS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intGRAPH_TYPE_INT = Convert.ToInt32(objDataRow["GRAPH_TYPE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strQRS_TOTAL_CHR = objDataRow["QRS_TOTAL_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intHEARTRATE_AVERAGE_INT = Convert.ToInt32(objDataRow["HEARTRATE_AVERAGE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intHEARTRATE_MAX_INT = Convert.ToInt32(objDataRow["HEARTRATE_MAX_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intHEARTRATE_MIN_INT = Convert.ToInt32(objDataRow["HEARTRATE_MIN_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strHEARTRATE_MAX_DAT = Convert.ToDateTime(objDataRow["HEARTRATE_MAX_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strHEARTRATE_MIN_DAT = Convert.ToDateTime(objDataRow["HEARTRATE_MIN_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_intHEARTRATE_BASE_INT = Convert.ToInt32(objDataRow["HEARTRATE_BASE_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strCHECK_CHANNELS_XML_VCHR = objDataRow["CHECK_CHANNELS_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_XML_VCHR = objDataRow["CLINICAL_DIAGNOSE_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY1_XML_VCHR = objDataRow["SUMMARY1_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY2_XML_VCHR = objDataRow["SUMMARY2_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEARTRATE_BASE_VCHR = objDataRow["HEARTRATE_BASE_VCHR"].ToString().Trim();
                        try
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = int.Parse(objDataRow["SPECIALFLAG_INT"].ToString().Trim());
                        }
                        catch
                        {
                            p_objResultArr[i1].m_intIsSpicalPatient = 0;
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
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 根据条件产生Table,用于报表
        /// <summary>
        /// 根据条件产生Table,用于报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strDept"></param>
        /// <param name="strDiagnoses"></param>
        /// <param name="p_objResultDtb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDnmCardiogramdbt(string p_strFromDat, string p_strToDat, string p_strDept, string strDiagnoses, out DataTable p_objResultDtb)
        {
            long lngRes = 0;
            p_objResultDtb = new DataTable();
            string strSQL = @"select report_dat,report_no_chr,patient_name_vchr,sex_chr,age_flt,inpatient_no_chr,patient_no_chr,clinical_diagnose_vchr,dept_name_vchr as summary1_vchr,summary2_vchr,specialflag_int from t_opr_ris_dcardiogram_report where status_int = '1'and report_dat between ? and ?";
            if (p_strDept != "")
            {
                strSQL += "and dept_name_vchr='" + p_strDept + "'";
            }
            if (strDiagnoses != "")
            {
                strSQL += "and clinical_diagnose_vchr like '%" + strDiagnoses + "%'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objArr);
                objArr[0].DbType = DbType.DateTime;
                objArr[0].Value = DateTime.Parse(p_strFromDat);
                objArr[1].DbType = DbType.DateTime;
                objArr[1].Value = DateTime.Parse(p_strToDat);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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

        #region 根据ID获取数据（运动平板运动实验报告）
        /// <summary>
        /// 根据ID获取数据（运动平板运动实验报告）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSportReport(string p_strID, out clsafmt_report_VO p_objResult)
        {
            p_objResult = new clsafmt_report_VO();
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
       a.clinical_diagnose_vchr,
       a.rhythm_vchr,
       a.heart_rate_vchr,
       a.p_r_vchr,
       a.qrs_vchr,
       a.q_t_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.specialflag_int,
       a.lie_pst_vchr,
       a.stand_pst_vchr,
       a.deep_breath_vchr,
       a.before_active_vchr,
       a.forecast_qty_int,
       a.forecast_qty_vchr,
       a.test_plan_vchr,
       a.active_load_level_vchr,
       a.active_load_mph_vchr,
       a.active_load_per_vchr,
       a.active_total_time_vchr,
       a.hr_top_vchr,
       a.hr_per_vchr,
       a.hr_max_work_vchr,
       a.stop_reason_vchr,
       a.active_st_int,
       a.active_st_vchr,
       a.active_st_mode_vchr,
       a.appear_led_vchr,
       a.appear_led_xml_vchr,
       a.hr_scope_vchr,
       a.hr_scope_xml_vchr,
       a.time_scope_vchr,
       a.time_scope_xml_vchr,
       a.active_st_max_int,
       a.active_st_max_vchr,
       a.active_st_max_led_vchr,
       a.active_st_max_time_vchr,
       a.hr_wrong_vchr,
       a.hr_wrong_xml_vchr,
       a.actived_bp_vchr,
       a.active_result_vchr,
       a.active_result_xml_vchr,
       a.test_result_vchr,
       a.test_result_xml_vchr
  from t_opr_ris_afmt_report a where report_id_chr = ?";
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
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    System.Data.DataRow objDataRow = dtbResult.Rows[0];
                    p_objResult = new clsafmt_report_VO();
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
                    p_objResult.m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                    p_objResult.m_strRHYTHM_VCHR = objDataRow["RHYTHM_VCHR"].ToString().Trim();
                    p_objResult.m_strHEART_RATE_VCHR = objDataRow["HEART_RATE_VCHR"].ToString().Trim();
                    p_objResult.m_strP_R_VCHR = objDataRow["P_R_VCHR"].ToString().Trim();
                    p_objResult.m_strQRS_VCHR = objDataRow["QRS_VCHR"].ToString().Trim();
                    p_objResult.m_strQ_T_VCHR = objDataRow["Q_T_VCHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                    p_objResult.m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                    p_objResult.m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                    p_objResult.m_intSPECIALFLAG_INT = Convert.ToInt32(objDataRow["SPECIALFLAG_INT"].ToString().Trim());
                    p_objResult.m_strLIE_PST_VCHR = objDataRow["LIE_PST_VCHR"].ToString().Trim();
                    p_objResult.m_strSTAND_PST_VCHR = objDataRow["STAND_PST_VCHR"].ToString().Trim();
                    p_objResult.m_strDEEP_BREATH_VCHR = objDataRow["DEEP_BREATH_VCHR"].ToString().Trim();
                    p_objResult.m_strBEFORE_ACTIVE_VCHR = objDataRow["BEFORE_ACTIVE_VCHR"].ToString().Trim();
                    p_objResult.m_intFORECAST_QTY_INT = Convert.ToInt32(objDataRow["FORECAST_QTY_INT"].ToString().Trim());
                    p_objResult.m_strFORECAST_QTY_VCHR = objDataRow["FORECAST_QTY_VCHR"].ToString().Trim();
                    p_objResult.m_strTEST_PLAN_VCHR = objDataRow["TEST_PLAN_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_LOAD_LEVEL_VCHR = objDataRow["ACTIVE_LOAD_LEVEL_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_LOAD_MPH_VCHR = objDataRow["ACTIVE_LOAD_MPH_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_LOAD_PER_VCHR = objDataRow["ACTIVE_LOAD_PER_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_TOTAL_TIME_VCHR = objDataRow["ACTIVE_TOTAL_TIME_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_TOP_VCHR = objDataRow["HR_TOP_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_PER_VCHR = objDataRow["HR_PER_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_MAX_WORK_VCHR = objDataRow["HR_MAX_WORK_VCHR"].ToString().Trim();
                    p_objResult.m_strSTOP_REASON_VCHR = objDataRow["STOP_REASON_VCHR"].ToString().Trim();
                    p_objResult.m_intACTIVE_ST_INT = Convert.ToInt32(objDataRow["ACTIVE_ST_INT"].ToString().Trim());
                    p_objResult.m_strACTIVE_ST_VCHR = objDataRow["ACTIVE_ST_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_ST_MODE_VCHR = objDataRow["ACTIVE_ST_MODE_VCHR"].ToString().Trim();
                    p_objResult.m_strAPPEAR_LED_VCHR = objDataRow["APPEAR_LED_VCHR"].ToString().Trim();
                    p_objResult.m_strAPPEAR_LED_XML_VCHR = objDataRow["APPEAR_LED_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_SCOPE_VCHR = objDataRow["HR_SCOPE_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_SCOPE_XML_VCHR = objDataRow["HR_SCOPE_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strTIME_SCOPE_VCHR = objDataRow["TIME_SCOPE_VCHR"].ToString().Trim();
                    p_objResult.m_strTIME_SCOPE_XML_VCHR = objDataRow["TIME_SCOPE_XML_VCHR"].ToString().Trim();
                    p_objResult.m_intACTIVE_ST_MAX_INT = Convert.ToInt32(objDataRow["ACTIVE_ST_MAX_INT"].ToString().Trim());
                    p_objResult.m_strACTIVE_ST_MAX_VCHR = objDataRow["ACTIVE_ST_MAX_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_ST_MAX_LED_VCHR = objDataRow["ACTIVE_ST_MAX_LED_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_ST_MAX_TIME_VCHR = objDataRow["ACTIVE_ST_MAX_TIME_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_WRONG_VCHR = objDataRow["HR_WRONG_VCHR"].ToString().Trim();
                    p_objResult.m_strHR_WRONG_XML_VCHR = objDataRow["HR_WRONG_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVED_BP_VCHR = objDataRow["ACTIVED_BP_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_RESULT_VCHR = objDataRow["ACTIVE_RESULT_VCHR"].ToString().Trim();
                    p_objResult.m_strACTIVE_RESULT_XML_VCHR = objDataRow["ACTIVE_RESULT_XML_VCHR"].ToString().Trim();
                    p_objResult.m_strTEST_RESULT_VCHR = objDataRow["TEST_RESULT_VCHR"].ToString().Trim();
                    p_objResult.m_strTEST_RESULT_XML_VCHR = objDataRow["TEST_RESULT_XML_VCHR"].ToString().Trim();
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

        #region 根据条件组合查询活动平板运动试验报告单
        /// <summary>
        /// 根据条件组合查询活动平板运动试验报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strPatientNo"></param>
        /// <param name="p_strInPatientNo"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strDept"></param>
        /// <param name="p_strReportNo"></param>
        /// <param name="strIsSpecail"></param>
        /// <param name="p_objResultArr"></param>
        /// /// <param name="strReporter"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSportReportByCondition(string p_strFromDat, string p_strToDat,
            string p_strPatientNo, string p_strInPatientNo, string p_strPatientName, string p_strDept, string p_strReportNo, string strIsSpecail, string strReporter,
            out clsafmt_report_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            //string strSQL = @"SELECT * FROM T_OPR_RIS_AFMT_REPORT WHERE STATUS_INT = '0'";
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
       a.clinical_diagnose_vchr,
       a.rhythm_vchr,
       a.heart_rate_vchr,
       a.p_r_vchr,
       a.qrs_vchr,
       a.q_t_vchr,
       a.reportor_id_chr,
       a.reportor_name_vchr,
       a.confirmer_id_chr,
       a.confirmer_name_vchr,
       a.status_int,
       a.operator_id_chr,
       a.specialflag_int,
       a.lie_pst_vchr,
       a.stand_pst_vchr,
       a.deep_breath_vchr,
       a.before_active_vchr,
       a.forecast_qty_int,
       a.forecast_qty_vchr,
       a.test_plan_vchr,
       a.active_load_level_vchr,
       a.active_load_mph_vchr,
       a.active_load_per_vchr,
       a.active_total_time_vchr,
       a.hr_top_vchr,
       a.hr_per_vchr,
       a.hr_max_work_vchr,
       a.stop_reason_vchr,
       a.active_st_int,
       a.active_st_vchr,
       a.active_st_mode_vchr,
       a.appear_led_vchr,
       a.appear_led_xml_vchr,
       a.hr_scope_vchr,
       a.hr_scope_xml_vchr,
       a.time_scope_vchr,
       a.time_scope_xml_vchr,
       a.active_st_max_int,
       a.active_st_max_vchr,
       a.active_st_max_led_vchr,
       a.active_st_max_time_vchr,
       a.hr_wrong_vchr,
       a.hr_wrong_xml_vchr,
       a.actived_bp_vchr,
       a.active_result_vchr,
       a.active_result_xml_vchr,
       a.test_result_vchr,
       a.test_result_xml_vchr,b.patientcardid_chr from t_opr_ris_afmt_report a,(select patientcardid_chr, patientid_chr, issue_date, status_int
          from t_bse_patientcard
         where status_int = 1 or status_int = 3) b where a.status_int = '0' and  a.patient_id_chr=b.patientid_chr(+)  ";

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
                strSQL += "  and a.reportor_name_vchr = '" + strReporter + "'";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                int intDataRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intDataRowCount > 0)
                {
                    p_objResultArr = new clsafmt_report_VO[intDataRowCount];
                    for (int i1 = 0; i1 < intDataRowCount; i1++)
                    {
                        System.Data.DataRow objDataRow = dtbResult.Rows[i1];
                        p_objResultArr[i1] = new clsafmt_report_VO();

                        p_objResultArr[i1].m_strCARD_ID_CHR = objDataRow["patientcardid_chr"].ToString();// 2005年11月1日 xigui.peng 修改

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
                        p_objResultArr[i1].m_strCLINICAL_DIAGNOSE_VCHR = objDataRow["CLINICAL_DIAGNOSE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRHYTHM_VCHR = objDataRow["RHYTHM_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHEART_RATE_VCHR = objDataRow["HEART_RATE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strP_R_VCHR = objDataRow["P_R_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQRS_VCHR = objDataRow["QRS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strQ_T_VCHR = objDataRow["Q_T_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_ID_CHR = objDataRow["REPORTOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strREPORTOR_NAME_VCHR = objDataRow["REPORTOR_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = objDataRow["CONFIRMER_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCONFIRMER_NAME_VCHR = objDataRow["CONFIRMER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(objDataRow["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = objDataRow["OPERATOR_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSPECIALFLAG_INT = Convert.ToInt32(objDataRow["SPECIALFLAG_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strLIE_PST_VCHR = objDataRow["LIE_PST_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTAND_PST_VCHR = objDataRow["STAND_PST_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEEP_BREATH_VCHR = objDataRow["DEEP_BREATH_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strBEFORE_ACTIVE_VCHR = objDataRow["BEFORE_ACTIVE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intFORECAST_QTY_INT = Convert.ToInt32(objDataRow["FORECAST_QTY_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strFORECAST_QTY_VCHR = objDataRow["FORECAST_QTY_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTEST_PLAN_VCHR = objDataRow["TEST_PLAN_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_LOAD_LEVEL_VCHR = objDataRow["ACTIVE_LOAD_LEVEL_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_LOAD_MPH_VCHR = objDataRow["ACTIVE_LOAD_MPH_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_LOAD_PER_VCHR = objDataRow["ACTIVE_LOAD_PER_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_TOTAL_TIME_VCHR = objDataRow["ACTIVE_TOTAL_TIME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_TOP_VCHR = objDataRow["HR_TOP_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_PER_VCHR = objDataRow["HR_PER_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_MAX_WORK_VCHR = objDataRow["HR_MAX_WORK_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTOP_REASON_VCHR = objDataRow["STOP_REASON_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intACTIVE_ST_INT = Convert.ToInt32(objDataRow["ACTIVE_ST_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strACTIVE_ST_VCHR = objDataRow["ACTIVE_ST_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_ST_MODE_VCHR = objDataRow["ACTIVE_ST_MODE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAPPEAR_LED_VCHR = objDataRow["APPEAR_LED_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAPPEAR_LED_XML_VCHR = objDataRow["APPEAR_LED_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_SCOPE_VCHR = objDataRow["HR_SCOPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_SCOPE_XML_VCHR = objDataRow["HR_SCOPE_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTIME_SCOPE_VCHR = objDataRow["TIME_SCOPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTIME_SCOPE_XML_VCHR = objDataRow["TIME_SCOPE_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intACTIVE_ST_MAX_INT = Convert.ToInt32(objDataRow["ACTIVE_ST_MAX_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strACTIVE_ST_MAX_VCHR = objDataRow["ACTIVE_ST_MAX_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_ST_MAX_LED_VCHR = objDataRow["ACTIVE_ST_MAX_LED_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_ST_MAX_TIME_VCHR = objDataRow["ACTIVE_ST_MAX_TIME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_WRONG_VCHR = objDataRow["HR_WRONG_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strHR_WRONG_XML_VCHR = objDataRow["HR_WRONG_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVED_BP_VCHR = objDataRow["ACTIVED_BP_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_RESULT_VCHR = objDataRow["ACTIVE_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strACTIVE_RESULT_XML_VCHR = objDataRow["ACTIVE_RESULT_XML_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTEST_RESULT_VCHR = objDataRow["TEST_RESULT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTEST_RESULT_XML_VCHR = objDataRow["TEST_RESULT_XML_VCHR"].ToString().Trim();
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

        #region 根据住院号查询病人资料
        /// <summary>
        /// 根据住院号查询病人资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strBihNo"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string m_strBihNo, out DataTable m_objTable)
        {
            m_objTable = new DataTable();
            long lngRes = 0;
            string strSQL = @"select a.patientid_chr, a.inpatientid_chr, b.lastname_vchr, b.sex_chr,
                              b.firstname_vchr, b.birth_dat, b.occupation_vchr
                              from t_opr_bih_register a, t_bse_patient b
                              where a.patientid_chr = b.patientid_chr and a.inpatientid_chr = ?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].Value = m_strBihNo;
                objArr[0].DbType = DbType.String;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, objArr);
                //  lngRes = objHRPSvc.DoGetDataTable(strSQL, ref m_objTable);

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

        #region 根据卡号检索病人资料
        /// <summary>
        /// 根据卡号检索病人资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="carID">卡号</param>
        /// <param name="dtResult">返回病人资料</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPat(string carID, out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = 0;
            //string strSQL = @"select a.PATIENTID_CHR,b.INPATIENTID_CHR,b.LASTNAME_VCHR,b.SEX_CHR,b.FIRSTNAME_VCHR,b.BIRTH_DAT,b.OCCUPATION_VCHR
            //                            from T_BSE_PATIENTCARD a,
            //                                 T_BSE_PATIENT b
            //                            where a.PATIENTID_CHR=b.PATIENTID_CHR
            //                                  and  a.PATIENTCARDID_CHR='" + carID + "' and (a.status_int=1 or a.status_int=3)";
            string strSQL = @"select a.patientid_chr,b.inpatientid_chr,b.lastname_vchr,b.sex_chr,b.firstname_vchr,b.birth_dat,b.occupation_vchr
                            from t_bse_patientcard a,
                                 t_bse_patient b
                            where a.patientid_chr=b.patientid_chr
                                  and  a.patientcardid_chr= ? and (a.status_int=1 or a.status_int=3)";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = carID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objArr);
                // lngRes = objHRPSvc.DoGetDataTable(strSQL, ref tbPat);

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

        #region 获取对应申请单的类型ID(心电图）
        /// <summary>
        /// 获取对应申请单的类型ID(心电图）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetApplTypeID(out string TypeID)
        {
            TypeID = null;
            long lngRes = 0;
            string strSQL = @"select attachtype_int, typeid, status_int, attachtypename_vchr
  from t_aid_apply_rlt where attachtype_int =4";
            DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                if (dt.Rows.Count > 0)
                {
                    TypeID = dt.Rows[0]["typeid"].ToString();
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

        #region 从t_sys_setting 得setstatus_int
        /// <summary>
        /// 从t_sys_setting 得setstatus_int
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strsetid_chr"></param>
        /// <param name="p_strModuledid"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_strGetsetstatusFromt_sys_setting(string p_strsetid_chr, string p_strModuledid, out string p_strResult)
        {

            long lngRes = 0;
            p_strResult = "";
            //string strSQL = "SELECT  setstatus_int FROM t_sys_setting  WHERE setid_chr = '" + p_strsetid_chr + "' and MODULEID_CHR='" + p_strModuledid + "'";
            string strSQL = "select  setstatus_int from t_sys_setting  where setid_chr = ? and moduleid_chr=?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objArr);
                objArr[0].Value = p_strsetid_chr;
                objArr[0].DbType = DbType.String;
                objArr[1].DbType = DbType.String;
                objArr[1].Value = p_strModuledid;
                // lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objArr);
                int intRowCount = dtbResult.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strResult = dtbResult.Rows[0][0].ToString().Trim();
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

        #region 获取部门数据
        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <param name="dtDept"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetDEPTDESC(out DataTable dtDept)
        {
            dtDept = new DataTable();
            long lngRes = 0;
            //string strSQL = @"select CODE_VCHR,DEPTNAME_VCHR,PYCODE_CHR,WBCODE_CHR,DEPTID_CHR from T_BSE_DEPTDESC where  DEPTNAME_VCHR <> '所有'   order by SHORTNO_CHR";
            string strSQL = @"select code_vchr,deptname_vchr,pycode_chr,wbcode_chr,deptid_chr from t_bse_deptdesc where  deptname_vchr <> '所有'   order by shortno_chr";
            //DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtDept);
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
            //           string strSQL = @"select a.patientid_chr,b.lastname_vchr,b.sex_chr,b.firstname_vchr,b.birth_dat,b.occupation_vchr
            //                            from t_bse_patientcard a,
            //                                 t_bse_patient b
            //                            where a.patientid_chr=b.patientid_chr
            //                                  and  b.inpatientid_chr= ? and (a.status_int=1 or a.status_int=3)";
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
                // lngRes = objHRPSvc.DoGetDataTable(strSQL, ref tbPat);

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
