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
    public class clsCardiogramManageSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 添加心电图报告
        /// <summary>
        /// 添加心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewCardiogramReport(out string p_strRecordID, clsRIS_CardiogramReport_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "report_id_chr", "t_opr_ris_cardiogram_report", out p_strRecordID);


            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = "insert into t_opr_ris_cardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,specialflag_int,applyid_int,e_axes_vchr, applydoctor_name_vchr, applydoctor_id_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, ?, ?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(38, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
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
                objLisAddItemRefArr[16].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strRHYTHM_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strHEART_RATE_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strP_R_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strQRS_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strQ_T_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strHEART_ROOM_VCHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[33].Value = p_objRecord.m_intIsSpicalPatient;
                objLisAddItemRefArr[34].Value = p_objRecord.m_intApplyID;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strE_Axes_vchr;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strApplyDoctorName;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strApplyDoctorID;

                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                if (lngRes > 0 && lngRecEff > 0)
                {
                    lngRes = -1;
                    lngRecEff = -1;
                    strSQL = @"update ar_common_apply t set t.status_int=2 where t.applyid=?";
                    objLisAddItemRefArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].DbType = DbType.Int32;
                    objLisAddItemRefArr[0].Value = p_objRecord.m_intApplyID;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                    //if (lngRecEff > 0 && lngRes > 0)
                    //{
                    //    lngRes = 1;
                    //}
                    //else
                    //{
                    //    lngRes = -1;
                    //}
                }
                else
                {
                    return lngRes - 1;
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
            }
            return lngRes;
        }
        #endregion

        #region 修改心电图报告
        /// <summary>
        /// 修改心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyCardiogramReport(clsRIS_CardiogramReport_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //                insert into t_opr_ris_cardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr
            string strSQL = "insert into t_opr_ris_cardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr,specialflag_int,applyid_int,e_axes_vchr, applydoctor_name_vchr, applydoctor_id_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(38, out objLisAddItemRefArr);
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
                objLisAddItemRefArr[16].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strRHYTHM_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strHEART_RATE_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strP_R_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strQRS_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strQ_T_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strHEART_ROOM_VCHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[33].Value = p_objRecord.m_intIsSpicalPatient;
                objLisAddItemRefArr[34].Value = p_objRecord.m_intApplyID;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strE_Axes_vchr;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strApplyDoctorName;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strApplyDoctorID;
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
            }
            return lngRes;
        }
        #endregion

        #region 删除心电图报告
        /// <summary>
        /// 删除心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteCardiogramReport(clsRIS_CardiogramReport_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string strSQL = "insert into t_opr_ris_cardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr   ,  summary1_xml_vchr,summary2_xml_vchr ) values ('" +
            //    p_objRecord.m_strREPORT_ID_CHR + "',TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_strREPORT_NO_CHR + "','" + p_objRecord.m_strPATIENT_ID_CHR + "','" + p_objRecord.m_strPATIENT_NO_CHR + "','" + p_objRecord.m_strINPATIENT_NO_CHR + "','" + p_objRecord.m_strPATIENT_NAME_VCHR + "','" + p_objRecord.m_strSEX_CHR + "','" + p_objRecord.m_strAGE_FLT.ToString() + "',TO_DATE('" + p_objRecord.m_strCHECK_DAT + "','YYYY-MM-DD hh24:mi:ss'),TO_DATE('" + p_objRecord.m_strREPORT_DAT + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_strDEPT_ID_CHR + "','" + p_objRecord.m_strDEPT_NAME_VCHR + "','" + p_objRecord.m_intIS_INPATIENT_INT.ToString() + "','" + p_objRecord.m_strBED_ID_CHR + "','" + p_objRecord.m_strBED_NO_CHR + "','" + p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR + "','" + p_objRecord.m_strRHYTHM_VCHR + "','" + p_objRecord.m_strHEART_RATE_VCHR + "','" + p_objRecord.m_strP_R_VCHR + "','" + p_objRecord.m_strQRS_VCHR + "','" + p_objRecord.m_strQ_T_VCHR + "','" + p_objRecord.m_strSUMMARY1_VCHR + "','" + p_objRecord.m_strSUMMARY2_VCHR + "','" + p_objRecord.m_strREPORTOR_ID_CHR + "','" + p_objRecord.m_strREPORTOR_NAME_VCHR + "','" + p_objRecord.m_strCONFIRMER_ID_CHR + "','" + p_objRecord.m_strCONFIRMER_NAME_VCHR + "','" + p_objRecord.m_strHEART_ROOM_VCHR + "','0','" + p_objRecord.m_strOPERATOR_ID_CHR + "','" + p_objRecord.m_strSUMMARY1_XML_VCHR + "','" + p_objRecord.m_strSUMMARY2_XML_VCHR + "')";
            string strSQL = @"insert into t_opr_ris_cardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,summary1_xml_vchr,summary2_xml_vchr ) values (?,?,?,?,?,?,?,?,?,?,
                                                              ?,?,?,?,?,?,?,?,?,?,
                                                              ?,?,?,?,?,?,?,?,?,?,?,?,?)";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
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
                objLisAddItemRefArr[16].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strRHYTHM_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strHEART_RATE_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strP_R_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strQRS_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strQ_T_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strHEART_ROOM_VCHR;
                objLisAddItemRefArr[29].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                //objLisAddItemRefArr[33].Value = p_objRecord.m_intIsSpicalPatient;
                //objLisAddItemRefArr[34].Value = p_objRecord.m_intApplyID;
                //objLisAddItemRefArr[35].Value = p_objRecord.m_strE_Axes_vchr;
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
            }
            return lngRes;
        }
        #endregion

        #region 添加动态心电图报告
        /// <summary>
        /// 添加动态心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDCardiogramReport(out string p_strRecordID, clsRIS_DCardiogramReport_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "report_id_chr", "t_opr_ris_dcardiogram_report", out p_strRecordID);

            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_dcardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,checkfrom_dat,checkto_dat,check_channels_vchr,graph_type_int,qrs_total_chr,heartrate_average_int,heartrate_max_int,heartrate_min_int,heartrate_max_dat,heartrate_min_dat,heartrate_base_int,check_channels_xml_vchr,clinical_diagnose_xml_vchr,summary1_xml_vchr,summary2_xml_vchr,heartrate_base_vchr,specialflag_int) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(42, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strHEART_ROOM_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[25].Value = DateTime.Parse(p_objRecord.m_strCHECKFROM_DAT);
                objLisAddItemRefArr[26].Value = DateTime.Parse(p_objRecord.m_strCHECKTO_DAT);
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCHECK_CHANNELS_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_intGRAPH_TYPE_INT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strQRS_TOTAL_CHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_intHEARTRATE_AVERAGE_INT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_intHEARTRATE_MAX_INT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_intHEARTRATE_MIN_INT;
                objLisAddItemRefArr[33].Value = DateTime.Parse(p_objRecord.m_strHEARTRATE_MAX_DAT);
                objLisAddItemRefArr[34].Value = DateTime.Parse(p_objRecord.m_strHEARTRATE_MIN_DAT);
                objLisAddItemRefArr[35].Value = p_objRecord.m_intHEARTRATE_BASE_INT;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strCHECK_CHANNELS_XML_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_XML_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strHEARTRATE_BASE_VCHR;
                objLisAddItemRefArr[41].Value = p_objRecord.m_intIsSpicalPatient;

                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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

        #region 修改动态心电图报告
        /// <summary>
        /// 修改动态心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDCardiogramReport(clsRIS_DCardiogramReport_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_dcardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,checkfrom_dat,checkto_dat,check_channels_vchr,graph_type_int,qrs_total_chr,heartrate_average_int,heartrate_max_int,heartrate_min_int,heartrate_max_dat,heartrate_min_dat,heartrate_base_int,check_channels_xml_vchr,clinical_diagnose_xml_vchr,summary1_xml_vchr,summary2_xml_vchr,heartrate_base_vchr,specialflag_int) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            //            string strSQL = @"		update T_OPR_RIS_DCARDIOGRAM_REPORT 
            //		set REPORT_ID_CHR=?,MODIFY_DAT=?,REPORT_NO_CHR=?,PATIENT_ID_CHR=?,
            //		PATIENT_NO_CHR=?,INPATIENT_NO_CHR=?,PATIENT_NAME_VCHR=?,SEX_CHR=?,
            //		AGE_FLT=?,REPORT_DAT=?,DEPT_ID_CHR=?,DEPT_NAME_VCHR=?,IS_INPATIENT_INT=?,
            //		BED_ID_CHR=?,BED_NO_CHR=?,CLINICAL_DIAGNOSE_VCHR=?,SUMMARY1_VCHR=?,
            //		SUMMARY2_VCHR=?,REPORTOR_ID_CHR=?,REPORTOR_NAME_VCHR=?,CONFIRMER_ID_CHR=?,
            //		CONFIRMER_NAME_VCHR=?,HEART_ROOM_VCHR=?,STATUS_INT=?,OPERATOR_ID_CHR=?,
            //		CHECKFROM_DAT=?,CHECKTO_DAT=?,CHECK_CHANNELS_VCHR=?,GRAPH_TYPE_INT=?,QRS_TOTAL_CHR=?,
            //		HEARTRATE_AVERAGE_INT=?,HEARTRATE_MAX_INT=?,HEARTRATE_MIN_INT=?,HEARTRATE_MAX_DAT=?,
            //		HEARTRATE_MIN_DAT=?,HEARTRATE_BASE_INT=?,CHECK_CHANNELS_XML_VCHR=?,
            //		CLINICAL_DIAGNOSE_XML_VCHR=?,SUMMARY1_XML_VCHR=?,SUMMARY2_XML_VCHR=?,
            //		HEARTRATE_BASE_VCHR=?
            //		where REPORT_ID_CHR=?";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(42, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strHEART_ROOM_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[25].Value = DateTime.Parse(p_objRecord.m_strCHECKFROM_DAT);
                objLisAddItemRefArr[26].Value = DateTime.Parse(p_objRecord.m_strCHECKTO_DAT);
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCHECK_CHANNELS_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_intGRAPH_TYPE_INT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strQRS_TOTAL_CHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_intHEARTRATE_AVERAGE_INT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_intHEARTRATE_MAX_INT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_intHEARTRATE_MIN_INT;
                objLisAddItemRefArr[33].Value = DateTime.Parse(p_objRecord.m_strHEARTRATE_MAX_DAT);
                objLisAddItemRefArr[34].Value = DateTime.Parse(p_objRecord.m_strHEARTRATE_MIN_DAT);
                objLisAddItemRefArr[35].Value = p_objRecord.m_intHEARTRATE_BASE_INT;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strCHECK_CHANNELS_XML_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_XML_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strHEARTRATE_BASE_VCHR;
                objLisAddItemRefArr[41].Value = p_objRecord.m_intIsSpicalPatient;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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

        #region  删除动态心电图报告
        /// <summary>
        /// 删除动态心电图报告
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDCardiogramReport(clsRIS_DCardiogramReport_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string strSQL = "insert into t_opr_ris_dcardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,checkfrom_dat,checkto_dat,check_channels_vchr,graph_type_int,qrs_total_chr,heartrate_average_int,heartrate_max_int,heartrate_min_int,heartrate_max_dat,heartrate_min_dat,heartrate_base_int , check_channels_xml_vchr,clinical_diagnose_xml_vchr,summary1_xml_vchr,summary2_xml_vchr) values ('" +
            //    p_objRecord.m_strREPORT_ID_CHR + "',TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_strREPORT_NO_CHR + "','" + p_objRecord.m_strPATIENT_ID_CHR + "','" + p_objRecord.m_strPATIENT_NO_CHR + "','" + p_objRecord.m_strINPATIENT_NO_CHR + "','" + p_objRecord.m_strPATIENT_NAME_VCHR + "','" + p_objRecord.m_strSEX_CHR + "','" + p_objRecord.m_strAGE_FLT.ToString() + "',TO_DATE('" + p_objRecord.m_strREPORT_DAT + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_strDEPT_ID_CHR + "','" + p_objRecord.m_strDEPT_NAME_VCHR + "','" + p_objRecord.m_intIS_INPATIENT_INT.ToString() + "','" + p_objRecord.m_strBED_ID_CHR + "','" + p_objRecord.m_strBED_NO_CHR + "','" + p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR + "','" + p_objRecord.m_strSUMMARY1_VCHR + "','" + p_objRecord.m_strSUMMARY2_VCHR + "','" + p_objRecord.m_strREPORTOR_ID_CHR + "','" + p_objRecord.m_strREPORTOR_NAME_VCHR + "','" + p_objRecord.m_strCONFIRMER_ID_CHR + "','" + p_objRecord.m_strCONFIRMER_NAME_VCHR + "','" + p_objRecord.m_strHEART_ROOM_VCHR + "','0','" + p_objRecord.m_strOPERATOR_ID_CHR + "',TO_DATE('" + p_objRecord.m_strCHECKFROM_DAT + "','YYYY-MM-DD hh24:mi:ss'),TO_DATE('" + p_objRecord.m_strCHECKTO_DAT + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_strCHECK_CHANNELS_VCHR + "','" + p_objRecord.m_intGRAPH_TYPE_INT.ToString() + "','" + p_objRecord.m_strQRS_TOTAL_CHR + "','" + p_objRecord.m_intHEARTRATE_AVERAGE_INT.ToString() + "','" + p_objRecord.m_intHEARTRATE_MAX_INT.ToString() + "','" + p_objRecord.m_intHEARTRATE_MIN_INT.ToString() + "',TO_DATE('" + p_objRecord.m_strHEARTRATE_MAX_DAT + "','YYYY-MM-DD hh24:mi:ss'),TO_DATE('" + p_objRecord.m_strHEARTRATE_MIN_DAT + "','YYYY-MM-DD hh24:mi:ss'),'" + p_objRecord.m_intHEARTRATE_BASE_INT.ToString() + "','" + p_objRecord.m_strCHECK_CHANNELS_XML_VCHR + "','" + p_objRecord.m_strCLINICAL_DIAGNOSE_XML_VCHR + "','" + p_objRecord.m_strSUMMARY1_XML_VCHR + "','" + p_objRecord.m_strSUMMARY2_XML_VCHR + "')";
            string strSQL = @"insert into t_opr_ris_dcardiogram_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,summary1_vchr,summary2_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,heart_room_vchr,status_int,operator_id_chr,checkfrom_dat,checkto_dat,check_channels_vchr,graph_type_int,qrs_total_chr,heartrate_average_int,heartrate_max_int,heartrate_min_int,heartrate_max_dat,heartrate_min_dat,heartrate_base_int , check_channels_xml_vchr,clinical_diagnose_xml_vchr,summary1_xml_vchr,summary2_xml_vchr) 
values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?      
       ,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(40, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strREPORT_ID_CHR;
                objLisAddItemRefArr[1].Value = DateTime.Parse(strDateTime);
                objLisAddItemRefArr[2].Value = p_objRecord.m_strREPORT_NO_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strPATIENT_ID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strPATIENT_NO_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strINPATIENT_NO_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPATIENT_NAME_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strSEX_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strAGE_FLT;
                objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strREPORT_DAT);
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDEPT_ID_CHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDEPT_NAME_VCHR;
                objLisAddItemRefArr[12].Value = p_objRecord.m_intIS_INPATIENT_INT;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strBED_ID_CHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strBED_NO_CHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSUMMARY1_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSUMMARY2_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strHEART_ROOM_VCHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[25].Value = DateTime.Parse(p_objRecord.m_strCHECKFROM_DAT);
                objLisAddItemRefArr[26].Value = DateTime.Parse(p_objRecord.m_strCHECKTO_DAT);
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCHECK_CHANNELS_VCHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_intGRAPH_TYPE_INT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strQRS_TOTAL_CHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_intHEARTRATE_AVERAGE_INT;
                objLisAddItemRefArr[31].Value = p_objRecord.m_intHEARTRATE_MAX_INT;
                objLisAddItemRefArr[32].Value = p_objRecord.m_intHEARTRATE_MIN_INT;
                objLisAddItemRefArr[33].Value = DateTime.Parse(p_objRecord.m_strHEARTRATE_MAX_DAT);
                objLisAddItemRefArr[34].Value = DateTime.Parse(p_objRecord.m_strHEARTRATE_MIN_DAT);
                objLisAddItemRefArr[35].Value = p_objRecord.m_intHEARTRATE_BASE_INT;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strCHECK_CHANNELS_XML_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_XML_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strSUMMARY1_XML_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strSUMMARY2_XML_VCHR;
                //objLisAddItemRefArr[40].Value = p_objRecord.m_strHEARTRATE_BASE_VCHR;
                //objLisAddItemRefArr[41].Value = p_objRecord.m_intIsSpicalPatient;

                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
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

        #region 添加心电图（运动平板运动实验报告）
        /// <summary>
        /// 添加心电图（运动平板运动实验报告）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewSportReport(out string p_strRecordID, clsafmt_report_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = objHRPSvc.lngGenerateID(10, "REPORT_ID_CHR", "T_OPR_RIS_AFMT_REPORT", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            if (p_strRecordID == "" || p_strRecordID == "0000000000")
                p_strRecordID = "0000000001";
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_afmt_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,specialflag_int,lie_pst_vchr,stand_pst_vchr,deep_breath_vchr,before_active_vchr,forecast_qty_int,forecast_qty_vchr,test_plan_vchr,active_load_level_vchr,active_load_mph_vchr,active_load_per_vchr,active_total_time_vchr,hr_top_vchr,hr_per_vchr,hr_max_work_vchr,stop_reason_vchr,active_st_int,active_st_vchr,active_st_mode_vchr,appear_led_vchr,appear_led_xml_vchr,hr_scope_vchr,hr_scope_xml_vchr,time_scope_vchr,time_scope_xml_vchr,active_st_max_int,active_st_max_vchr,active_st_max_led_vchr,active_st_max_time_vchr,hr_wrong_vchr,hr_wrong_xml_vchr,actived_bp_vchr,active_result_vchr,active_result_xml_vchr,test_result_vchr,test_result_xml_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(64, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;
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
                objLisAddItemRefArr[16].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strRHYTHM_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strHEART_RATE_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strP_R_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strQRS_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strQ_T_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_intSPECIALFLAG_INT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strLIE_PST_VCHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strSTAND_PST_VCHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strDEEP_BREATH_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strBEFORE_ACTIVE_VCHR;
                objLisAddItemRefArr[33].Value = p_objRecord.m_intFORECAST_QTY_INT;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strFORECAST_QTY_VCHR;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strTEST_PLAN_VCHR;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strACTIVE_LOAD_LEVEL_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strACTIVE_LOAD_MPH_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strACTIVE_LOAD_PER_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strACTIVE_TOTAL_TIME_VCHR;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strHR_TOP_VCHR;
                objLisAddItemRefArr[41].Value = p_objRecord.m_strHR_PER_VCHR;
                objLisAddItemRefArr[42].Value = p_objRecord.m_strHR_MAX_WORK_VCHR;
                objLisAddItemRefArr[43].Value = p_objRecord.m_strSTOP_REASON_VCHR;
                objLisAddItemRefArr[44].Value = p_objRecord.m_intACTIVE_ST_INT;
                objLisAddItemRefArr[45].Value = p_objRecord.m_strACTIVE_ST_VCHR;
                objLisAddItemRefArr[46].Value = p_objRecord.m_strACTIVE_ST_MODE_VCHR;
                objLisAddItemRefArr[47].Value = p_objRecord.m_strAPPEAR_LED_VCHR;
                objLisAddItemRefArr[48].Value = p_objRecord.m_strAPPEAR_LED_XML_VCHR;
                objLisAddItemRefArr[49].Value = p_objRecord.m_strHR_SCOPE_VCHR;
                objLisAddItemRefArr[50].Value = p_objRecord.m_strHR_SCOPE_XML_VCHR;
                objLisAddItemRefArr[51].Value = p_objRecord.m_strTIME_SCOPE_VCHR;
                objLisAddItemRefArr[52].Value = p_objRecord.m_strTIME_SCOPE_XML_VCHR;
                objLisAddItemRefArr[53].Value = p_objRecord.m_intACTIVE_ST_MAX_INT;
                objLisAddItemRefArr[54].Value = p_objRecord.m_strACTIVE_ST_MAX_VCHR;
                objLisAddItemRefArr[55].Value = p_objRecord.m_strACTIVE_ST_MAX_LED_VCHR;
                objLisAddItemRefArr[56].Value = p_objRecord.m_strACTIVE_ST_MAX_TIME_VCHR;
                objLisAddItemRefArr[57].Value = p_objRecord.m_strHR_WRONG_VCHR;
                objLisAddItemRefArr[58].Value = p_objRecord.m_strHR_WRONG_XML_VCHR;
                objLisAddItemRefArr[59].Value = p_objRecord.m_strACTIVED_BP_VCHR;
                objLisAddItemRefArr[60].Value = p_objRecord.m_strACTIVE_RESULT_VCHR;
                objLisAddItemRefArr[61].Value = p_objRecord.m_strACTIVE_RESULT_XML_VCHR;
                objLisAddItemRefArr[62].Value = p_objRecord.m_strTEST_RESULT_VCHR;
                objLisAddItemRefArr[63].Value = p_objRecord.m_strTEST_RESULT_XML_VCHR;
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

        #region  修改心电图（运动平板运动实验报告）
        /// <summary>
        ///修改心电图（运动平板运动实验报告） 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifySportReport(clsafmt_report_VO p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "insert into t_opr_ris_afmt_report (report_id_chr,modify_dat,report_no_chr,patient_id_chr,patient_no_chr,inpatient_no_chr,patient_name_vchr,sex_chr,age_flt,check_dat,report_dat,dept_id_chr,dept_name_vchr,is_inpatient_int,bed_id_chr,bed_no_chr,clinical_diagnose_vchr,rhythm_vchr,heart_rate_vchr,p_r_vchr,qrs_vchr,q_t_vchr,reportor_id_chr,reportor_name_vchr,confirmer_id_chr,confirmer_name_vchr,status_int,operator_id_chr,specialflag_int,lie_pst_vchr,stand_pst_vchr,deep_breath_vchr,before_active_vchr,forecast_qty_int,forecast_qty_vchr,test_plan_vchr,active_load_level_vchr,active_load_mph_vchr,active_load_per_vchr,active_total_time_vchr,hr_top_vchr,hr_per_vchr,hr_max_work_vchr,stop_reason_vchr,active_st_int,active_st_vchr,active_st_mode_vchr,appear_led_vchr,appear_led_xml_vchr,hr_scope_vchr,hr_scope_xml_vchr,time_scope_vchr,time_scope_xml_vchr,active_st_max_int,active_st_max_vchr,active_st_max_led_vchr,active_st_max_time_vchr,hr_wrong_vchr,hr_wrong_xml_vchr,actived_bp_vchr,active_result_vchr,active_result_xml_vchr,test_result_vchr,test_result_xml_vchr) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            //    string strSQL = @"update T_OPR_RIS_AFMT_REPORT
            //  set REPORT_ID_CHR=?,MODIFY_DAT=?,REPORT_NO_CHR=?,PATIENT_ID_CHR=?,PATIENT_NO_CHR=?,INPATIENT_NO_CHR=?,PATIENT_NAME_VCHR=?,SEX_CHR=?,AGE_FLT=?,CHECK_DAT=?,REPORT_DAT=?,DEPT_ID_CHR=?,DEPT_NAME_VCHR=?,IS_INPATIENT_INT=?,BED_ID_CHR=?,BED_NO_CHR=?,CLINICAL_DIAGNOSE_VCHR=?
            // ,RHYTHM_VCHR=?,HEART_RATE_VCHR=?,P_R_VCHR=?,QRS_VCHR=?,Q_T_VCHR=?,REPORTOR_ID_CHR=?,REPORTOR_NAME_VCHR=?,
            // CONFIRMER_ID_CHR=?,CONFIRMER_NAME_VCHR=?,STATUS_INT=?,OPERATOR_ID_CHR=?,
            // SPECIALFLAG_INT=?,LIE_PST_VCHR=?,STAND_PST_VCHR=?,DEEP_BREATH_VCHR=?,
            // BEFORE_ACTIVE_VCHR=?,FORECAST_QTY_INT=?,FORECAST_QTY_VCHR=?,TEST_PLAN_VCHR=?,
            // ACTIVE_LOAD_LEVEL_VCHR=?,ACTIVE_LOAD_MPH_VCHR=?,ACTIVE_LOAD_PER_VCHR=?,
            // ACTIVE_TOTAL_TIME_VCHR=?,HR_TOP_VCHR=?,HR_PER_VCHR=?,HR_MAX_WORK_VCHR=?,STOP_REASON_VCHR=?,
            // ACTIVE_ST_INT=?,ACTIVE_ST_VCHR=?,ACTIVE_ST_MODE_VCHR=?,APPEAR_LED_VCHR=?,APPEAR_LED_XML_VCHR=?,
            // HR_SCOPE_VCHR=?,HR_SCOPE_XML_VCHR=?,TIME_SCOPE_VCHR=?,TIME_SCOPE_XML_VCHR=?,ACTIVE_ST_MAX_INT=?,
            // ACTIVE_ST_MAX_VCHR=?,ACTIVE_ST_MAX_LED_VCHR=?,ACTIVE_ST_MAX_TIME_VCHR=?,HR_WRONG_VCHR=?,HR_WRONG_XML_VCHR=?,
            // ACTIVED_BP_VCHR=?,ACTIVE_RESULT_VCHR=?,ACTIVE_RESULT_XML_VCHR=?,TEST_RESULT_VCHR=?,TEST_RESULT_XML_VCHR=?
            //	where REPORT_ID_CHR=?";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(64, out objLisAddItemRefArr);
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
                objLisAddItemRefArr[16].Value = p_objRecord.m_strCLINICAL_DIAGNOSE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strRHYTHM_VCHR;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strHEART_RATE_VCHR;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strP_R_VCHR;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strQRS_VCHR;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strQ_T_VCHR;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strREPORTOR_ID_CHR;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strREPORTOR_NAME_VCHR;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strCONFIRMER_ID_CHR;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strCONFIRMER_NAME_VCHR;
                objLisAddItemRefArr[26].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strOPERATOR_ID_CHR;
                objLisAddItemRefArr[28].Value = p_objRecord.m_intSPECIALFLAG_INT;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strLIE_PST_VCHR;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strSTAND_PST_VCHR;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strDEEP_BREATH_VCHR;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strBEFORE_ACTIVE_VCHR;
                objLisAddItemRefArr[33].Value = p_objRecord.m_intFORECAST_QTY_INT;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strFORECAST_QTY_VCHR;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strTEST_PLAN_VCHR;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strACTIVE_LOAD_LEVEL_VCHR;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strACTIVE_LOAD_MPH_VCHR;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strACTIVE_LOAD_PER_VCHR;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strACTIVE_TOTAL_TIME_VCHR;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strHR_TOP_VCHR;
                objLisAddItemRefArr[41].Value = p_objRecord.m_strHR_PER_VCHR;
                objLisAddItemRefArr[42].Value = p_objRecord.m_strHR_MAX_WORK_VCHR;
                objLisAddItemRefArr[43].Value = p_objRecord.m_strSTOP_REASON_VCHR;
                objLisAddItemRefArr[44].Value = p_objRecord.m_intACTIVE_ST_INT;
                objLisAddItemRefArr[45].Value = p_objRecord.m_strACTIVE_ST_VCHR;
                objLisAddItemRefArr[46].Value = p_objRecord.m_strACTIVE_ST_MODE_VCHR;
                objLisAddItemRefArr[47].Value = p_objRecord.m_strAPPEAR_LED_VCHR;
                objLisAddItemRefArr[48].Value = p_objRecord.m_strAPPEAR_LED_XML_VCHR;
                objLisAddItemRefArr[49].Value = p_objRecord.m_strHR_SCOPE_VCHR;
                objLisAddItemRefArr[50].Value = p_objRecord.m_strHR_SCOPE_XML_VCHR;
                objLisAddItemRefArr[51].Value = p_objRecord.m_strTIME_SCOPE_VCHR;
                objLisAddItemRefArr[52].Value = p_objRecord.m_strTIME_SCOPE_XML_VCHR;
                objLisAddItemRefArr[53].Value = p_objRecord.m_intACTIVE_ST_MAX_INT;
                objLisAddItemRefArr[54].Value = p_objRecord.m_strACTIVE_ST_MAX_VCHR;
                objLisAddItemRefArr[55].Value = p_objRecord.m_strACTIVE_ST_MAX_LED_VCHR;
                objLisAddItemRefArr[56].Value = p_objRecord.m_strACTIVE_ST_MAX_TIME_VCHR;
                objLisAddItemRefArr[57].Value = p_objRecord.m_strHR_WRONG_VCHR;
                objLisAddItemRefArr[58].Value = p_objRecord.m_strHR_WRONG_XML_VCHR;
                objLisAddItemRefArr[59].Value = p_objRecord.m_strACTIVED_BP_VCHR;
                objLisAddItemRefArr[60].Value = p_objRecord.m_strACTIVE_RESULT_VCHR;
                objLisAddItemRefArr[61].Value = p_objRecord.m_strACTIVE_RESULT_XML_VCHR;
                objLisAddItemRefArr[62].Value = p_objRecord.m_strTEST_RESULT_VCHR;
                objLisAddItemRefArr[63].Value = p_objRecord.m_strTEST_RESULT_XML_VCHR;
                //  objLisAddItemRefArr[64].Value = p_objRecord.m_strREPORT_ID_CHR;
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

        #region  根据ID删除数据（运动平板运动实验报告）
        /// <summary>
        /// 根据ID删除数据（运动平板运动实验报告）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteSportReport(string p_objRecordID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "delete from t_opr_ris_afmt_report where report_id_chr=?";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = p_objRecordID;
                long lngEffict = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
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
                p_objRecordID = null;
            }
            return lngRes;
        }
        #endregion

        #region  审核（运动平板运动实验报告）
        /// <summary>
        ///  审核（运动平板运动实验报告）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordID"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strEmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteSportReportEmp(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "update  t_opr_ris_afmt_report set confirmer_id_chr=?,confirmer_name_vchr=?  where report_id_chr=?";
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
                p_objRecordID = null;
                strEmpName = null;
                strEmpID = null;
            }
            return lngRes;
        }
        #endregion

        #region  审核（心电图实验报告）
        /// <summary>
        ///  审核（心电图实验报告）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordID"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strEmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfigCardiogramReport(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "update  t_opr_ris_cardiogram_report set confirmer_id_chr=?,confirmer_name_vchr=?  where report_id_chr=?";
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
                p_objRecordID = null;
                strEmpName = null;
                strEmpID = null;
            }
            return lngRes;
        }
        #endregion

        #region  审核（动态心电图实验报告）
        /// <summary>
        ///  审核（动态心电图实验报告）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordID"></param>
        /// <param name="strEmpID"></param>
        /// <param name="strEmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfigDmnCardiogramReport(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "update  t_opr_ris_dcardiogram_report set confirmer_id_chr=?,confirmer_name_vchr=? where report_id_chr= ?";
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
                p_objRecordID = null;
                strEmpName = null;
                strEmpID = null;
            }
            return lngRes;
        }
        #endregion
    }
}
