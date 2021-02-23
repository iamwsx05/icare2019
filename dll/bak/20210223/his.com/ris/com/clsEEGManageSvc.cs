using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.RIS
{
    /// <summary>
    /// clsCardiogramManageSvc 的摘要说明。
    /// Alex 2004-5-6
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsEEGManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 添加经颅多普勒TCD报告单
        /// <summary>
        /// 添加经颅多普勒TCD报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewTCDReport(out string p_strRecordID, clsRIS_TCD_REPORT_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "REPORT_ID_CHR", "T_OPR_RIS_TCD_REPORT", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //		   string strSQL = "INSERT INTO T_OPR_RIS_TCD_REPORT (REPORT_ID_CHR,MODIFY_DAT,REPORT_NO_CHR,PATIENT_ID_CHR,PATIENT_NO_CHR,INPATIENT_NO_CHR,PATIENT_NAME_VCHR,SEX_CHR,AGE_FLT,CHECK_DAT,REPORT_DAT,DEPT_ID_CHR,DEPT_NAME_VCHR,IS_INPATIENT_INT,BED_ID_CHR,BED_NO_CHR,SUMMARY1_VCHR,SUMMARY2_VCHR,REPORTOR_ID_CHR,REPORTOR_NAME_VCHR,CONFIRMER_ID_CHR,CONFIRMER_NAME_VCHR,STATUS_INT,OPERATOR_ID_CHR,SUMMARY1_XML_VCHR,SUMMARY2_XML_VCHR,DIAGNOSE_VCHR,DIAGNOSE_XML_VCHR,CURE_CIRCS_VCHR,CURE_CIRCS_XML_VCHR,CT_RESULT_VCHR,CT_RESULT_XML_VCHR,MRI_RESULT_VCHR,MRI_RESULT_XML_VCHR,X_RAY_RESULT_VCHR,X_RAY_RESULT_XML_VCHR,EKG_RESULT_VCHR,EKG_RESULT_XML_VCHR,BUS_RESULT_VCHR,BUS_RESULT_XML_VCHR) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            string strSQL = "insert into t_opr_ris_tcd_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,diagnose_vchr,diagnose_xml_vchr,cure_circs_vchr,cure_circs_xml_vchr,ct_result_vchr,ct_result_xml_vchr,mri_result_vchr,mri_result_xml_vchr,x_ray_result_vchr,x_ray_result_xml_vchr,ekg_result_vchr,ekg_result_xml_vchr,bus_result_vchr,bus_result_xml_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(40, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
                objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strDIAGNOSE_VCHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strDIAGNOSE_XML_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strCURE_CIRCS_VCHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strCURE_CIRCS_XML_VCHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strCT_RESULT_VCHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strCT_RESULT_XML_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strMRI_RESULT_VCHR;
                objLisAddItemRefArr[33].Value = p_objRecord.m_strMRI_RESULT_XML_VCHR;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strX_RAY_RESULT_VCHR;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strX_RAY_RESULT_XML_VCHR;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strEKG_RESULT_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strEKG_RESULT_XML_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strBUS_RESULT_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strBUS_RESULT_XML_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
                p_objRecord = null;
            }
            return lngRes;
        }
        #endregion

        #region 修改经颅多普勒TCD报告单
        /// <summary>
        /// 修改经颅多普勒TCD报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyTCDReport(clsRIS_TCD_REPORT_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_tcd_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,diagnose_vchr,diagnose_xml_vchr,cure_circs_vchr,cure_circs_xml_vchr,ct_result_vchr,ct_result_xml_vchr,mri_result_vchr,mri_result_xml_vchr,x_ray_result_vchr,x_ray_result_xml_vchr,ekg_result_vchr,ekg_result_xml_vchr,bus_result_vchr,bus_result_xml_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(40, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
                objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strDIAGNOSE_VCHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strDIAGNOSE_XML_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strCURE_CIRCS_VCHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strCURE_CIRCS_XML_VCHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strCT_RESULT_VCHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strCT_RESULT_XML_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strMRI_RESULT_VCHR;
                objLisAddItemRefArr[33].Value = p_objRecord.m_strMRI_RESULT_XML_VCHR;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strX_RAY_RESULT_VCHR;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strX_RAY_RESULT_XML_VCHR;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strEKG_RESULT_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strEKG_RESULT_XML_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strBUS_RESULT_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strBUS_RESULT_XML_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
                p_objRecord = null;
            }
            return lngRes;
        }
        #endregion

        #region 删除经颅多普勒TCD报告单
        /// <summary>
        /// 删除经颅多普勒TCD报告单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteTCDReport(clsRIS_TCD_REPORT_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_tcd_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(26, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
                objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
                p_objRecord = null;
            }
            return lngRes;
        }
        #endregion

        #region  审核（EEG报告单）
        /// <summary>
        ///  审核（EEG报告单）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordID"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strEmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfigEEGReport(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "update  t_opr_ris_eeg_report set confirmer_id_chr=?,confirmer_name_vchr=?  where report_id_chr=?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = strEmpID;
                objArr[1].DbType = DbType.String;
                objArr[1].Value = strEmpName;
                objArr[2].DbType = DbType.String;
                objArr[2].Value = p_objRecordID;
                long lngEffict = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
                if (lngRes > 0 && lngEffict > 0)
                {
                    lngRes = 1;
                }
                else
                    lngRes = -1;
                // lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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

        #region  审核（TCD报告单）
        /// <summary>
        /// 审核（TCD报告单）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordID"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strEmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfigTCDReport(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "update  t_opr_ris_tcd_report set confirmer_id_chr=?,confirmer_name_vchr=? where report_id_chr=?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objArr);
                objArr[0].DbType = DbType.String;
                //objArr[0].Value = DateTime.Parse(strEmpID); old 2010.6.30
                objArr[0].Value = strEmpID;

                objArr[1].DbType = DbType.String;
                //objArr[1].Value = DateTime.Parse(strEmpName); old 2010.6.30
                objArr[1].Value = strEmpName
                    ;
                objArr[2].DbType = DbType.String;
                //objArr[2].Value = DateTime.Parse(p_objRecordID); old 2010.6.30
                objArr[2].Value = p_objRecordID;

                long lngEffict = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
                if (lngRes > 0 && lngEffict > 0)
                {
                    lngRes = 1;
                }
                else
                    lngRes = -1;
                objHRPSvc.Dispose();
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
    #region 提供对EEG闹电图数据的访问
    /// <summary>
    /// 提供对EEG闹电图数据的访问
    /// </summary>
    public class clsRIS_EEGManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 添加脑电图EEG报告单
        /// <summary>
        /// 添加脑电图EEG报告单
        /// </summary>
        [AutoComplete]
        public long m_lngAddNewEEGReport(out string p_strRecordID, clsRIS_EEG_REPORT_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "REPORT_ID_CHR", "T_OPR_RIS_EEG_REPORT", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            p_objRecord.m_strREPORT_ID_CHR = p_strRecordID;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_eeg_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,diagnose_vchr,diagnose_xml_vchr,left_right,before_check,body_stat,sense_stat,drug_stat) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(33, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                objLisAddItemRefArr[1].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
                objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strDIAGNOSE_VCHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strDIAGNOSE_XML_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strLEFT_RIGHT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strBEFORE_CHECK;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strBODY_STAT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strSENSE_STAT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strDRUG_STAT;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
                p_objRecord = null;
            }
            return lngRes;
        }
        #endregion

        #region 修改脑电图EEG报告单
        /// <summary>
        ///修改脑电图EEG报告单
        /// </summary>
        [AutoComplete]
        public long m_lngModifyEEGReport(clsRIS_EEG_REPORT_VO p_objRecord)
        {
            long lngRes = 0;
            string strSql = @"update t_opr_ris_eeg_report t set t.status_int= -1 where t.report_id_chr= ? ";


            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_eeg_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,diagnose_vchr,diagnose_xml_vchr,left_right,before_check,body_stat,sense_stat,drug_stat) values  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngRecEff, objLisAddItemRefArr);
                if (lngRes > 0 && lngRecEff > 0)
                {
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(33, out objLisAddItemRefArr);
                    //Please change the datetime and reocrdid 
                    objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                    objLisAddItemRefArr[1].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
                    objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                    objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                    objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                    objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                    objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                    objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                    objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                    objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
                    objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                    objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_ID_CHR;
                    objLisAddItemRefArr[12].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                    objLisAddItemRefArr[13].Value = p_objRecord.m_intIS_INPATIENT_INT;
                    objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_ID_CHR;
                    objLisAddItemRefArr[15].Value = p_objRecord.m_strBED_NO_CHR;
                    objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                    objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                    objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                    objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                    objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                    objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                    objLisAddItemRefArr[22].Value = p_objRecord.m_intSTATUS_INT;
                    objLisAddItemRefArr[23].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                    objLisAddItemRefArr[24].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                    objLisAddItemRefArr[25].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                    objLisAddItemRefArr[26].Value = p_objRecord.m_strDIAGNOSE_VCHR;
                    objLisAddItemRefArr[27].Value = p_objRecord.m_strDIAGNOSE_XML_VCHR;
                    objLisAddItemRefArr[28].Value = p_objRecord.m_strLEFT_RIGHT;
                    objLisAddItemRefArr[29].Value = p_objRecord.m_strBEFORE_CHECK;
                    objLisAddItemRefArr[30].Value = p_objRecord.m_strBODY_STAT;
                    objLisAddItemRefArr[31].Value = p_objRecord.m_strSENSE_STAT;
                    objLisAddItemRefArr[32].Value = p_objRecord.m_strDRUG_STAT;
                    lngRecEff = -1;
                    //往表增加记录
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                    objHRPSvc.Dispose();
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
                p_objRecord = null;
            }
            return lngRes;
        }
        #endregion

        #region 删除脑电图EEG报告单
        /// <summary>
        /// 删除脑电图EEG报告单
        /// </summary>
        [AutoComplete]
        public long m_lngDeleteEEGReport(clsRIS_EEG_REPORT_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            #region 
            //string strSQL = "insert into t_opr_ris_eeg_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,diagnose_vchr,diagnose_xml_vchr,left_right,before_check,body_stat,sense_stat,drug_stat) values  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

            //    System.Data.IDataParameter[] objLisAddItemRefArr = null;
            //    objHRPSvc.CreateDatabaseParameter(33, out objLisAddItemRefArr);
            //    //Please change the datetime and reocrdid 
            //    objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
            //    objLisAddItemRefArr[1].Value = DateTime.Parse(p_objRecord.m_strMODIFY_DAT);
            //    objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
            //    objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
            //    objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
            //    objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
            //    objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
            //    objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
            //    objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
            //    objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
            //    objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
            //    objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_ID_CHR;
            //    objLisAddItemRefArr[12].Value = p_objRecord.m_strDEPT_NAME_VCHR;
            //    objLisAddItemRefArr[13].Value = p_objRecord.m_intIS_INPATIENT_INT;
            //    objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_ID_CHR;
            //    objLisAddItemRefArr[15].Value = p_objRecord.m_strBED_NO_CHR;
            //    objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
            //    objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
            //    objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
            //    objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
            //    objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
            //    objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
            //    objLisAddItemRefArr[22].Value = p_objRecord.m_intSTATUS_INT;
            //    objLisAddItemRefArr[23].Value = p_objRecord.m_strOPERATOR_ID_CHR;
            //    objLisAddItemRefArr[24].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
            //    objLisAddItemRefArr[25].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
            //    objLisAddItemRefArr[26].Value = p_objRecord.m_strDIAGNOSE_VCHR;
            //    objLisAddItemRefArr[27].Value = p_objRecord.m_strDIAGNOSE_XML_VCHR;
            //    objLisAddItemRefArr[28].Value = p_objRecord.m_strLEFT_RIGHT;
            //    objLisAddItemRefArr[29].Value = p_objRecord.m_strBEFORE_CHECK;
            //    objLisAddItemRefArr[30].Value = p_objRecord.m_strBODY_STAT;
            //    objLisAddItemRefArr[31].Value = p_objRecord.m_strSENSE_STAT;
            //    objLisAddItemRefArr[32].Value = p_objRecord.m_strDRUG_STAT;
            #endregion
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                string strSQL = @"update t_opr_ris_eeg_report t set t.status_int= -1 where t.report_id_chr= ? ";

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
                p_objRecord = null;
            }
            return lngRes;
        }
        #endregion

        #region     年龄转换  
        /// <summary>
        /// 年龄转换
        /// </summary>
        /// <param name="strage"></param>
        private string m_mthAgeChange(string strage)
        {
            int length = strage.Length;
            string strTextAge = "1";
            string strCmbAge = "年";
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
    }
    #endregion
}