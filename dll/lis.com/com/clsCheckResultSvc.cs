using System;
using System.Data;
using System.Collections;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.Utility; //Utility.dll
using Microsoft.VisualBasic; 

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsCheckResultSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        #region SQL���

        private const string c_strAddNewCheckResult_SQL = @"INSERT INTO t_opr_lis_check_result(modify_dat, 
														groupid_chr, 
														check_item_id_chr, 
														sample_id_chr, 
														result_vchr, 
														unit_vchr, 
														deviceid_chr, 
														device_check_item_name_vchr, 
														refrange_vchr, 
														check_item_name_vchr, 
														check_item_english_name_vchr, 
														min_val_dec, 
														max_val_dec, 
														abnormal_flag_chr, 
														check_dat, 
														clinicapp_vchr, 
														memo_vchr, 
														confirm_dat, 
														pointliststr_vchr, 
														Summary_Vchr, 
														graph_img, 
														status_int, 
														checker1_chr, 
														checker2_chr, 
														confirm_person_chr, 
														operator_id_chr, 
														check_deptid_chr, 
														GRAPH_FORMAT_NAME_VCHR, 
														IS_GRAPH_RESULT_NUM)
						VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
        #endregion

        #region �������ݵ���

        #region ����T_OPR_LIS_RESULT_IMPORT_REQ��״̬
        [AutoComplete]
        public long m_lngSetResultImportReqStatus( string p_strDeviceID, string p_strDeviceSampleID,
            string p_strCheckDat, string p_strStatus)
        {
            long lngRes = 0; 

            string strSQL = @"UPDATE t_opr_lis_result_import_req a
								 SET a.status_int = '" + p_strStatus + @"'
							   WHERE a.deviceid_chr = '" + p_strDeviceID + @"' 
								 AND a.check_dat = TO_DATE('" + p_strCheckDat + @"','yyyy-mm-dd hh24:mi:ss')
								 AND TRIM(a.device_sampleid_chr) = '" + p_strDeviceSampleID + @"'
								 AND a.status_int > 0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region ��������ָ���λ��
        [AutoComplete]
        public long m_lngSetResultImportReqEndPoint( clsLisResultImportReq_VO p_objRecord)
        {
            long lngRes = 0; 

            string strSQL_FindEndPoint = @"UPDATE t_opr_lis_result_import_req a
											  SET a.is_autobind_endpointer_int = 0
										    WHERE a.is_autobind_endpointer_int = 1
											  AND a.deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL_FindEndPoint);
                //�����µĽ���ָ��
                lngRes = m_lngSetResultImportReq( p_objRecord);
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

        #region ���±�T_OPR_LIS_RESULT_IMPORT_REQ����Ϣ
        [AutoComplete]
        public long m_lngSetResultImportReq( clsLisResultImportReq_VO p_objRecord)
        {
            long lngRes = 0; 

            string strSQL = @"UPDATE t_opr_lis_result_import_req a
								 SET a.status_int = '" + p_objRecord.m_intSTATUS_INT + @"',
									 a.is_autobind_endpointer_int = '" + p_objRecord.m_intIS_AUTOBIND_ENDPOINTER_INT + @"'
							   WHERE a.deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR + @"' 
								 AND a.import_req_int = '" + p_objRecord.m_strIMPORT_REQ_INT + @"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        //        #region ����������ѯT_OPR_LIS_RESULT_IMPORT_REQ�����Ϣ 
        //        [AutoComplete]
        //        public long m_lngGetResultImportReqByCondition(string p_strDeviceID,
        //            string p_strCheckDatFrom,string p_strCheckDatTo,out clsLisResultImportReq_VO[] p_objResultArr)
        //        {
        //            long lngRes=0;
        //            p_objResultArr = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetResultImportReqByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            #region SQL
        //            string strSQL = @"SELECT a.*
        //								FROM t_opr_lis_result_import_req a 
        //							   WHERE 1=1 ";
        //            string strSQL_CheckDatFrom = " AND a.check_dat >= ? ";
        //            string strSQL_CheckDatTo = " AND a.check_dat <= ? ";
        //            string strSQL_DeviceID = "AND a.deviceid_chr = ? ";
        //            #endregion

        //            ArrayList arlSQL = new ArrayList();
        //            ArrayList arlParm = new ArrayList();

        //            #region ����
        //            if(p_strDeviceID != null && p_strDeviceID.Trim() != "")
        //            {
        //                arlSQL.Add(strSQL_DeviceID);
        //                arlParm.Add(p_strDeviceID);
        //            }
        //            if(p_strCheckDatFrom != null && Microsoft.VisualBasic.Information.IsDate(p_strCheckDatFrom.Trim()))
        //            {
        //                arlSQL.Add(strSQL_CheckDatFrom);
        //                arlParm.Add(DateTime.Parse(p_strCheckDatFrom.Trim()));
        //            }
        //            if(p_strCheckDatTo != null && Microsoft.VisualBasic.Information.IsDate(p_strCheckDatTo.Trim()))
        //            {
        //                arlSQL.Add(strSQL_CheckDatTo);
        //                arlParm.Add(DateTime.Parse(p_strCheckDatTo.Trim()));
        //            }
        //            #endregion

        //            foreach(object obj in arlSQL)
        //            {
        //                strSQL += obj.ToString();
        //            }

        //            strSQL += " ORDER BY a.IMPORT_REQ_INT";

        //            int intParmCount = arlSQL.Count;

        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //            IDataParameter[] objDPArr = null;
        //            objHRPSvc.CreateDatabaseParameter(intParmCount,out objDPArr);

        //            for(int i=0;i< intParmCount;i++)
        //            {
        //                objDPArr[i].Value = arlParm[i];
        //            }

        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult,objDPArr);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsLisResultImportReq_VO[dtbResult.Rows.Count];
        //                    for(int i1=0;i1<p_objResultArr.Length;i1++)
        //                    {
        //                        p_objResultArr[i1] = new clsLisResultImportReq_VO();
        //                        p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        p_objResultArr[i1].m_strDEVICE_SAMPLEID_CHR = dtbResult.Rows[i1]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strDEVICEID_CHR = dtbResult.Rows[i1]["DEVICEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
        //                        p_objResultArr[i1].m_intIS_AUTOBIND_ENDPOINTER_INT = Convert.ToInt32(dtbResult.Rows[i1]["IS_AUTOBIND_ENDPOINTER_INT"].ToString().Trim());
        //                        p_objResultArr[i1].m_strIMPORT_REQ_INT = dtbResult.Rows[i1]["IMPORT_REQ_INT"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #endregion

        //        #region ��ѯ�õ�  CheckResultVO 

        //        /// <summary>
        //        /// ��ѯ�õ�  CheckResultVO
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strSampleID"></param>
        //        /// <param name="p_strCheckItemID"></param>
        //        /// <param name="p_objResultVO"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetCheckResultVO(
        //            string p_strSampleID,string p_strCheckItemID,out clsCheckResult_VO p_objResultVO)
        //        {
        //            long lngRes = 0;
        //            p_objResultVO = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetCheckResultVO");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            lngRes = 0;

        //            string strSQL = @"SELECT t1.*
        //								FROM t_opr_lis_check_result t1
        //							   WHERE t1.sample_id_chr = ?
        //								 AND t1.check_item_id_chr = ?
        //								 AND t1.status_int > 0";

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                IDataParameter[] objDPArr = null;
        //                objHRPSvc.CreateDatabaseParameter(2,out objDPArr);
        //                objDPArr[0].Value = p_strSampleID;
        //                objDPArr[1].Value = p_strCheckItemID;

        //                DataTable dtbCheckResult = new DataTable();
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbCheckResult,objDPArr);

        //                if(lngRes > 0 && dtbCheckResult != null && dtbCheckResult.Rows.Count != 0)
        //                {
        //                    p_objResultVO = new clsVOConstructor().m_objConstructCheckResultVO(dtbCheckResult.Rows[0]);
        //                }
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ��ѯ�õ�  CheckResultTable 

        //        /// <summary>
        //        /// ��ѯ�õ�  CheckResultTable 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strAppID"></param>
        //        /// <param name="p_strReportID"></param>
        //        /// <param name="p_strGroupID"></param>
        //        /// <param name="p_blnRealResult"></param>
        //        /// <param name="p_dtbResultList"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetCheckResultTable(
        //            string p_strAppID,string p_strOringinDate,bool p_blnRealResult,out DataTable p_dtbResultList)
        //        {
        //            long lngRes = 0;
        //            p_dtbResultList = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetCheckResultTable");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            lngRes = 0;

        //            try
        //            {
        //                DateTime.Parse(p_strOringinDate);
        //            }
        //            catch
        //            {
        //                p_strOringinDate = "1900-01-01 00:00:00";
        //            }			

        //            string strSQL = "";

        //            #region SQL
        //            if(p_blnRealResult)
        //            {
        //                strSQL = @"SELECT
        //									t2.sample_id_chr, t3.check_item_id_chr, t4.print_seq_int group_seq,
        //									t5.item_print_seq_int item_seq, t1.check_category_id_chr,
        //									t1.sampletype_vchr, t1.unit_chr item_unit_chr, t1.rptno_chr,
        //									t1.formula_vchr, t1.resulttype_chr, t1.is_calculated_chr,
        //									t1.alarm_low_val_vchr, t1.alarm_up_val_vchr,
        //									t1.alert_value_range_vchr, NULL alert_flag,
        //									t6.device_check_item_name_vchr device_item_name_vchr, t6.*,
        //									t8.status_int samplestatus, 0 new_result, 0 modify_flag,
        //									NULL raw_result_vchr, NULL eff_caculate_id_chr,0 invisible
        //							FROM t_opr_lis_app_sample t2,
        //								t_opr_lis_app_check_item t3,
        //								t_opr_lis_sample t8,
        //								t_opr_lis_check_result t6,
        //								t_bse_lis_check_item t1,
        //								t_aid_lis_report_group_detail t4,
        //								v_lis_bse_sample_group_items t5,
        //								t_bse_lis_check_item_dev_item t7, t_opr_lis_application t9
        //							WHERE  t3.report_group_id_chr = t2.report_group_id_chr
        //							AND t3.sample_group_id_chr = t2.sample_group_id_chr
        //							AND t3.check_item_id_chr = t1.check_item_id_chr
        //							AND t8.sample_id_chr = t2.sample_id_chr
        //							AND t2.sample_group_id_chr = t4.sample_group_id_chr(+)
        //							AND t3.check_item_id_chr = t5.check_item_id_chr
        //							AND t6.check_item_id_chr = t3.check_item_id_chr
        //							AND t6.sample_id_chr = t2.sample_id_chr
        //							AND t3.check_item_id_chr = t7.check_item_id_chr(+)
        //							AND t2.sample_group_id_chr = t5.sample_group_id_chr
        //							AND t6.status_int > 0
        //							AND t8.status_int > 0 AND t9.pstatus_int + 0 > 0
        //							AND t2.application_id_chr = t9.application_id_chr 
        //   AND t3.application_id_chr = t9.application_id_chr
        //   AND t8.application_id_chr = t9.application_id_chr
        //   AND t8.modify_dat >= t9.oringin_dat and t6.modify_dat >= t9.oringin_dat and t8.modify_dat >= ? and t6.modify_dat >= ? 
        //   AND t9.application_id_chr = ?  AND t2.application_id_chr = ?  AND t3.application_id_chr = ?  AND t8.application_id_chr = ?
        //   AND t9.oringin_dat = ?
        //							";//t_bse_lis_device_check_item t8//AND t7.device_check_item_id_chr = t8.device_check_item_id_chr//
        //            }//AND t7.device_model_id_chr = t8.device_model_id_chr
        //            else
        //            {
        //                strSQL = @"SELECT t2.sample_id_chr, t3.check_item_id_chr, t4.print_seq_int group_seq,
        //								t5.item_print_seq_int item_seq,t1.check_category_id_chr,t1.sampletype_vchr,
        //							    t1.unit_chr item_unit_chr, t1.rptno_chr,
        //								t1.formula_vchr, t1.resulttype_chr, t1.is_calculated_chr,
        //								t1.alarm_low_val_vchr,t1.alarm_up_val_vchr,t1.alert_value_range_vchr,NULL alert_flag,
        //								t8.device_check_item_name_vchr device_item_name_vchr,
        //								f_getitemref_low (t3.check_item_id_chr,
        //													TRIM(t9.age_chr),
        //													TRIM(t9.sex_chr),
        //													'menses'
        //													) min_val_dec,
        //								f_getitemref_up (t3.check_item_id_chr,
        //													TRIM(t9.age_chr),
        //													TRIM(t9.sex_chr),
        //													'menses'
        //												) max_val_dec,
        //								f_getitemref_range (t3.check_item_id_chr,
        //													TRIM(t9.age_chr),
        //													TRIM(t9.sex_chr),
        //													'menses'
        //													) refrange_vchr,
        //								t3.sample_group_id_chr groupid_chr,t11.value_vchr result_vchr,
        //								t6.*, t10.status_int samplestatus, 1 new_result, 0 modify_flag,
        //								NULL raw_result_vchr, NULL eff_caculate_id_chr,0 invisible
        //							FROM t_bse_lis_check_item t1,
        //								t_opr_lis_app_sample t2,
        //								t_opr_lis_app_check_item t3,
        //								t_aid_lis_report_group_detail t4,
        //								v_lis_bse_sample_group_items t5,
        //								(SELECT *
        //									FROM t_opr_lis_check_result
        //									WHERE modify_dat =
        //											TO_DATE ('1111-11-11 00:00:00', 'yyyy-mm-dd hh24:mi:ss')) t6,
        //								(SELECT t71.*, t70.*
        //									FROM t_bse_lis_check_item_dev_item t70,
        //										t_bse_lis_device_check_item t71
        //									WHERE t71.device_model_id_chr = t70.device_model_id_chr
        //									AND t71.device_check_item_id_chr = t70.device_check_item_id_chr) t8,
        //								t_opr_lis_application t9,
        //								t_opr_lis_sample t10,
        //								v_lis_aid_check_item_default t11
        //							WHERE t3.application_id_chr = t2.application_id_chr
        //							AND t3.report_group_id_chr = t2.report_group_id_chr
        //							AND t3.sample_group_id_chr = t2.sample_group_id_chr
        //							AND t3.check_item_id_chr = t1.check_item_id_chr
        //							AND t10.sample_id_chr = t2.sample_id_chr
        //							AND t10.status_int >= 0
        //							AND t2.sample_group_id_chr = t4.sample_group_id_chr(+)
        //							AND t5.check_item_id_chr = t3.check_item_id_chr
        //							AND t5.sample_group_id_chr = t3.sample_group_id_chr
        //							AND t3.check_item_id_chr = t6.check_item_id_chr(+)
        //							AND t3.check_item_id_chr = t8.check_item_id_chr(+)
        //							AND t9.application_id_chr = t2.application_id_chr
        //							AND t9.pstatus_int + 0 > 0
        //							AND t11.check_item_id_chr(+) = t3.check_item_id_chr
        //							AND t10.modify_dat >= t9.oringin_dat							
        //							AND t9.application_id_chr = ? 
        //							AND t9.oringin_dat >= ?
        //							";

        //            }
        //            #endregion

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                if(p_blnRealResult)
        //                {
        //                    IDataParameter[] objDPArr = null;
        //                    objHRPSvc.CreateDatabaseParameter(7,out objDPArr);
        //                    objDPArr[0].Value = DateTime.Parse(p_strOringinDate);
        //                    objDPArr[1].Value = DateTime.Parse(p_strOringinDate);
        //                    objDPArr[2].Value = p_strAppID;
        //                    objDPArr[3].Value = p_strAppID;
        //                    objDPArr[4].Value = p_strAppID;
        //                    objDPArr[5].Value = p_strAppID;
        //                    objDPArr[6].Value = DateTime.Parse(p_strOringinDate);
        //                    lngRes = 0;
        //                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbResultList,objDPArr);
        //                    objHRPSvc.Dispose();
        //                }
        //                else
        //                {
        //                    IDataParameter[] objDPArr = null;
        //                    objHRPSvc.CreateDatabaseParameter(2,out objDPArr);
        //                    objDPArr[0].Value = p_strAppID;
        //                    objDPArr[1].Value = DateTime.Parse(p_strOringinDate);
        //                    lngRes = 0;
        //                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbResultList,objDPArr);
        //                    objHRPSvc.Dispose();		
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion


        //        #region ��ӡʱ���浥��ѯ

        //        #region ������ӡ���浥 
        //        [AutoComplete]
        //        public long m_lngGetBatchReportDataByCondition(string p_strFromSampleID,
        //            string p_strToSampleID,string p_strFromConfirmDat,string p_strToConfirmDat,string p_strReportGroupID,out clsLisBatchReport_VO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetBatchReportDataByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            #region SQL
        //            string strSQL = @"SELECT DISTINCT t1.sex_chr,t1.patient_name_vchr, t2.*, t4.deptname_vchr, 
        //									 t7.lastname_vchr AS applyer,
        //									 t9.report_group_name_vchr,t11.check_dat
        //								FROM t_opr_lis_sample t1,
        //									 t_opr_lis_app_report t2,
        //									 t_opr_lis_app_sample t3,
        //									 t_bse_deptdesc t4,
        //									 t_bse_employee t7,
        //									 t_aid_lis_report_group t9,
        //									 (SELECT t10.check_dat,t10.sample_id_chr
        //										FROM t_opr_lis_device_relation t10
        //									   WHERE t10.status_int > 0) t11
        //							   WHERE t2.report_group_id_chr = t3.report_group_id_chr
        //								 AND t2.application_id_chr = t3.application_id_chr
        //								 AND t3.sample_id_chr = t1.sample_id_chr
        //								 AND t1.appl_empid_chr = t7.empid_chr(+)
        //								 AND t1.appl_deptid_chr = t4.deptid_chr(+)
        //								 AND t2.report_group_id_chr = t9.report_group_id_chr
        //								 AND t1.status_int = 6
        //								 AND t2.status_int > 0
        //								 AND t1.sample_id_chr = t11.sample_id_chr(+)";
        //            string strSQL_SampleNOFrom = " AND t1.sample_id_chr >= ? ";
        //            string strSQL_SampleNOTo = " AND t1.sample_id_chr <= ? ";
        //            string strSQL_ComfirmDatFrom = " AND t2.CONFIRM_DAT >= ? ";
        //            string strSQL_ConfirmDatTo = " AND t2.CONFIRM_DAT <= ? ";
        //            string strSQL_ReportGroupID = " AND t2.report_group_id_chr = ? ";
        //            #endregion

        //            ArrayList arlSQL = new ArrayList();
        //            ArrayList arlParm = new ArrayList();

        //            #region ����
        //            if(p_strFromSampleID != null && p_strFromSampleID.ToString().Trim() != "")
        //            {
        //                arlSQL.Add(strSQL_SampleNOFrom);
        //                arlParm.Add(p_strFromSampleID);
        //            }
        //            if(p_strToSampleID != null && p_strToSampleID.ToString().Trim() != "")
        //            {
        //                arlSQL.Add(strSQL_SampleNOTo);
        //                arlParm.Add(p_strToSampleID);
        //            }
        //            if(p_strFromConfirmDat != null && Microsoft.VisualBasic.Information.IsDate(p_strFromConfirmDat.Trim()))
        //            {
        //                arlSQL.Add(strSQL_ComfirmDatFrom);
        //                arlParm.Add(DateTime.Parse(p_strFromConfirmDat.Trim()));
        //            }
        //            if(p_strToConfirmDat != null && Microsoft.VisualBasic.Information.IsDate(p_strToConfirmDat.Trim()))
        //            {
        //                arlSQL.Add(strSQL_ConfirmDatTo);
        //                arlParm.Add(DateTime.Parse(p_strToConfirmDat.Trim()));
        //            }
        //            if(p_strReportGroupID != null && p_strReportGroupID.Trim() != "")
        //            {
        //                arlSQL.Add(strSQL_ReportGroupID);
        //                arlParm.Add(p_strReportGroupID.Trim());
        //            }
        //            #endregion

        //            foreach(object obj in arlSQL)
        //            {
        //                strSQL += obj.ToString();
        //            }

        //            strSQL += " ORDER BY t11.check_dat";

        //            int intParmCount = arlSQL.Count;

        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //            IDataParameter[] objDPArr = null;
        //            objHRPSvc.CreateDatabaseParameter(intParmCount,out objDPArr);

        //            for(int i=0;i< intParmCount;i++)
        //            {
        //                objDPArr[i].Value = arlParm[i];
        //            }

        //            try
        //            {
        //                DataTable dtbResult = null;
        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult,objDPArr);
        //                if(lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsLisBatchReport_VO[dtbResult.Rows.Count];
        //                    for(int i=0;i<dtbResult.Rows.Count;i++)
        //                    {
        //                        p_objResultArr[i] = new clsLisBatchReport_VO();
        //                        p_objResultArr[i].strApplyDept = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
        //                        p_objResultArr[i].strConfirmDat = dtbResult.Rows[i]["CONFIRM_DAT"].ToString().Trim();
        //                        p_objResultArr[i].strPatientName = dtbResult.Rows[i]["patient_name_vchr"].ToString().Trim();
        //                        p_objResultArr[i].strReportGroupName = dtbResult.Rows[i]["report_group_name_vchr"].ToString().Trim();
        //                        p_objResultArr[i].strSex = dtbResult.Rows[i]["sex_chr"].ToString().Trim();

        //                        string strSQL_BaseInfo = @"SELECT t1.*, t2.*, t4.deptname_vchr, t5.lastname_vchr AS reportor,
        //														  t6.lastname_vchr AS confirmer, t7.lastname_vchr AS applyer,
        //														  t8.sample_type_desc_vchr, t9.report_group_name_vchr,
        //														  t11.device_sampleid_chr
        //													 FROM t_opr_lis_sample t1,
        //														  t_opr_lis_app_report t2,
        //														  t_opr_lis_app_sample t3,
        //														  T_BSE_DEPTDESC t4,
        //														  t_bse_employee t5,
        //														  t_bse_employee t6,
        //														  t_bse_employee t7,
        //														  t_aid_lis_sampletype t8,
        //														  t_aid_lis_report_group t9,
        //														  (SELECT t10.device_sampleid_chr,t10.sample_id_chr
        //															 FROM t_opr_lis_device_relation t10
        //														    WHERE t10.status_int > 0) t11
        //													WHERE t2.report_group_id_chr = t3.report_group_id_chr
        //													  AND t2.application_id_chr = t3.application_id_chr
        //													  AND t3.sample_id_chr = t1.sample_id_chr
        //													  AND t2.reportor_id_chr = t5.empid_chr(+)
        //													  AND t2.confirmer_id_chr = t6.empid_chr(+)
        //													  AND t1.appl_empid_chr = t7.empid_chr(+)
        //													  AND t1.appl_deptid_chr = t4.deptid_chr(+)
        //													  AND t1.sample_type_id_chr = t8.sample_type_id_chr(+)
        //													  AND t2.report_group_id_chr = t9.report_group_id_chr
        //													  AND t1.sample_id_chr = t11.sample_id_chr(+)
        //													  AND t1.status_int = 6
        //													  AND t2.status_int > 0
        //													  AND t2.report_group_id_chr = '"+dtbResult.Rows[i]["report_group_id_chr"].ToString().Trim()+@"'
        //													  AND t2.application_id_chr = '"+dtbResult.Rows[i]["application_id_chr"].ToString().Trim()+@"'";

        //                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL_BaseInfo,ref p_objResultArr[i].m_dtbReportBaseInfo);

        //                        if(lngRes > 0 && p_objResultArr[i].m_dtbReportBaseInfo != null && p_objResultArr[i].m_dtbReportBaseInfo.Rows.Count > 0)
        //                        {

        //                            string strSQL_Result = @"SELECT a.*, d.print_title_vchr, e.print_seq_int AS report_print_seq_int,
        //																f.print_seq_int AS sample_print_seq_int, g.rptno_chr
        //														   FROM t_opr_lis_check_result a,
        //																t_opr_lis_app_report b,
        //																t_opr_lis_app_sample c,
        //																t_aid_lis_report_group d,
        //																t_aid_lis_report_group_detail e,
        //																t_aid_lis_sample_group_detail f,
        //																t_bse_lis_check_item g
        //														  WHERE a.groupid_chr = b.report_group_id_chr
        //															AND b.report_group_id_chr = c.report_group_id_chr
        //															AND b.application_id_chr = c.application_id_chr
        //															AND a.groupid_chr = d.report_group_id_chr
        //															AND d.report_group_id_chr = e.report_group_id_chr
        //															AND e.sample_group_id_chr = f.sample_group_id_chr
        //															AND c.sample_group_id_chr = f.sample_group_id_chr
        //															AND a.check_item_id_chr = g.check_item_id_chr
        //															AND a.check_item_id_chr = f.check_item_id_chr
        //															AND a.sample_id_chr = c.sample_id_chr
        //															AND a.status_int > 0
        //															AND b.status_int > 0
        //															AND b.report_group_id_chr = '"+dtbResult.Rows[i]["report_group_id_chr"].ToString().Trim()+@"'
        //															AND b.application_id_chr = '"+dtbResult.Rows[i]["application_id_chr"].ToString().Trim()+@"'";
        //                            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL_Result,ref p_objResultArr[i].m_dtbCheckResult);

        //                        }
        //                    }
        //                }
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����������ѯ������ӡ�ı��浥�б�
        //        [AutoComplete]
        //        public long m_lngGetLisBatchReportListByCondition(string p_strFromSampleID,
        //            string p_strToSampleID,string p_strFromConfirmDat,string p_strToConfirmDat,string p_strReportGroupID,out clsLisBatchReportList_VO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetLisBatchReportListByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            #region SQL
        //            string strSQL = @"SELECT DISTINCT t1.sex_chr, t1.patient_name_vchr, 
        //                                        t2.application_id_chr, t2.report_group_id_chr, t2.modify_dat,
        //                                       t2.summary_vchr, t2.operator_id_chr, t2.status_int, t2.report_dat,
        //                                       t2.reportor_id_chr, t2.confirm_dat, t2.confirmer_id_chr,
        //                                       t2.xml_summary_vchr, t2.annotation_vchr, t2.xml_annotation_vchr, 
        //                                        t4.deptname_vchr, t7.lastname_vchr AS applyer, t9.report_group_name_vchr,
        //										t2.report_dat,t1.application_form_no_chr
        //								FROM t_opr_lis_application t1,
        //										t_opr_lis_app_report t2,
        //										t_opr_lis_app_sample t3,
        //										T_BSE_DEPTDESC t4,
        //										t_bse_employee t7,
        //										t_aid_lis_report_group t9
        //								WHERE t2.report_group_id_chr = t3.report_group_id_chr
        //									AND t2.application_id_chr = t3.application_id_chr
        //									AND t2.application_id_chr = t1.application_id_chr
        //									AND t1.appl_empid_chr = t7.empid_chr(+)
        //									AND t1.appl_deptid_chr = t4.deptid_chr(+)
        //									AND t2.report_group_id_chr = t9.report_group_id_chr
        //									AND t2.status_int = 2
        //									AND t1.pstatus_int > 0";
        //            //			string strSQL_CheckNOFrom = " AND t1.application_form_no_chr >= ? ";
        //            //			string strSQL_CheckNOTo = " AND t1.application_form_no_chr <= ? ";
        //            //			string strSQL_ComfirmDatFrom = " AND t2.CONFIRM_DAT >= ? ";
        //            //			string strSQL_ConfirmDatTo = " AND t2.CONFIRM_DAT <= ? ";
        //            //			string strSQL_ReportGroupID = " AND t2.report_group_id_chr = ? ";
        //            string strSQL_CheckNOFrom = " AND t1.application_form_no_chr >= '"+p_strFromSampleID+@"'";
        //            string strSQL_CheckNOTo = " AND t1.application_form_no_chr <= '"+p_strToSampleID+@"'";
        //            string strSQL_ComfirmDatFrom = " AND t2.CONFIRM_DAT >= TO_DATE('"+p_strFromConfirmDat+@"','yyyy-mm-dd hh24:mi:ss')";
        //            string strSQL_ConfirmDatTo = " AND t2.CONFIRM_DAT <= TO_DATE('"+p_strToConfirmDat+@"','yyyy-mm-dd hh24:mi:ss')";
        //            string strSQL_ReportGroupID = " AND t2.report_group_id_chr = '"+p_strReportGroupID+@"'";
        //            #endregion

        //            //			ArrayList arlSQL = new ArrayList();
        //            //			ArrayList arlParm = new ArrayList();

        //            #region ����
        //            if(p_strFromSampleID != null && p_strFromSampleID.ToString().Trim() != "")
        //            {
        //                strSQL += strSQL_CheckNOFrom;
        //                //				arlSQL.Add(strSQL_CheckNOFrom);
        //                //				arlParm.Add(p_strFromSampleID);
        //            }
        //            if(p_strToSampleID != null && p_strToSampleID.ToString().Trim() != "")
        //            {
        //                strSQL += strSQL_CheckNOTo;
        //                //				arlSQL.Add(strSQL_CheckNOTo);
        //                //				arlParm.Add(p_strToSampleID);
        //            }
        //            if(p_strFromConfirmDat != null && Microsoft.VisualBasic.Information.IsDate(p_strFromConfirmDat.Trim()))
        //            {
        //                strSQL += strSQL_ComfirmDatFrom;
        //                //				arlSQL.Add(strSQL_ComfirmDatFrom);
        //                //				arlParm.Add(DateTime.Parse(p_strFromConfirmDat.Trim()));
        //            }
        //            if(p_strToConfirmDat != null && Microsoft.VisualBasic.Information.IsDate(p_strToConfirmDat.Trim()))
        //            {
        //                strSQL += strSQL_ConfirmDatTo;
        //                //				arlSQL.Add(strSQL_ConfirmDatTo);
        //                //				arlParm.Add(DateTime.Parse(p_strToConfirmDat.Trim()));
        //            }
        //            if(p_strReportGroupID != null && p_strReportGroupID.Trim() != "")
        //            {
        //                strSQL += strSQL_ReportGroupID;
        //                //				arlSQL.Add(strSQL_ReportGroupID);
        //                //				arlParm.Add(p_strReportGroupID.Trim());
        //            }
        //            #endregion

        //            //			foreach(object obj in arlSQL)
        //            //			{
        //            //				strSQL += obj.ToString();
        //            //			}

        //            strSQL += " ORDER BY t2.report_group_id_chr,t1.application_form_no_chr";

        //            //			int intParmCount = arlSQL.Count;

        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //            //			IDataParameter[] objDPArr = null;
        //            //			objHRPSvc.CreateDatabaseParameter(intParmCount,out objDPArr);
        //            //
        //            //			for(int i=0;i< intParmCount;i++)
        //            //			{
        //            //				objDPArr[i].Value = arlParm[i];
        //            //			}

        //            try
        //            {
        //                DataTable dtbResult = null;
        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                //				lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult,objDPArr);
        //                if(lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsLisBatchReportList_VO[dtbResult.Rows.Count];
        //                    for(int i=0;i<dtbResult.Rows.Count;i++)
        //                    {
        //                        p_objResultArr[i] = new clsLisBatchReportList_VO();
        //                        p_objResultArr[i].strApplyDept = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
        //                        p_objResultArr[i].strConfirmDat = dtbResult.Rows[i]["CONFIRM_DAT"].ToString().Trim();
        //                        p_objResultArr[i].strPatientName = dtbResult.Rows[i]["patient_name_vchr"].ToString().Trim();
        //                        p_objResultArr[i].strReportGroupName = dtbResult.Rows[i]["report_group_name_vchr"].ToString().Trim();
        //                        p_objResultArr[i].strSex = dtbResult.Rows[i]["sex_chr"].ToString().Trim();
        //                        p_objResultArr[i].strApplicationID = dtbResult.Rows[i]["application_id_chr"].ToString().Trim();
        //                        p_objResultArr[i].strReportGroupID = dtbResult.Rows[i]["report_group_id_chr"].ToString().Trim();
        //                        p_objResultArr[i].strCheckNO = dtbResult.Rows[i]["application_form_no_chr"].ToString().Trim();
        //                    }
        //                }
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region �������뵥�źͱ�����Ų�ѯ������ӡ��Ϣ�б�
        //        [AutoComplete]
        //        public long m_lngGetLisBatchReportDetailByCondition(clsLisBatchReportList_VO[] p_objReportList,
        //            out clsLisBatchReportDetail_VO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetLisBatchReportDetailByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            try
        //            {
        //                //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                if(p_objReportList != null)
        //                {
        //                    p_objResultArr = new clsLisBatchReportDetail_VO[p_objReportList.Length];
        //                    for(int i=0;i<p_objReportList.Length;i++)
        //                    {
        //                        p_objResultArr[i] = new clsLisBatchReportDetail_VO();

        //                        lngRes = 0;
        //                        lngRes = m_lngGetReportInfoByReportGroupIDAndApplicationID(
        //                            p_objReportList[i].strReportGroupID,p_objReportList[i].strApplicationID,true, out p_objResultArr[i].m_dtbReportBaseInfo);

        //                        if(lngRes > 0 && p_objResultArr[i].m_dtbReportBaseInfo != null && p_objResultArr[i].m_dtbReportBaseInfo.Rows.Count > 0)
        //                        {
        //                            lngRes = 0;
        //                            lngRes = m_lngGetCheckResultByReportGroupIDAndApplicationID(
        //                                p_objReportList[i].strApplicationID,p_objReportList[i].strReportGroupID,true,out p_objResultArr[i].m_dtbCheckResult);

        //                        }
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����report_group_id��application_id_chr��ѯ���浥�����Ϣ 
        //        /// <summary>
        //        /// ����report_group_id��application_id_chr��ѯ���浥�����Ϣ
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strReportGroupID">������ID</param>
        //        /// <param name="p_strApplID">���뵥ID</param>
        //        /// <param name="p_blnConfirmed">�Ƿ����</param>
        //        /// <param name="p_dtbReportInfo">���ر��浥�����Ϣ</param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetReportInfoByReportGroupIDAndApplicationID(
        //            string p_strReportGroupID,string p_strApplID,bool p_blnConfirmed,out DataTable p_dtbReportInfo)
        //        {
        //            long lngRes = 0;
        //            p_dtbReportInfo = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetReportInfoByReportGroupIDAndApplicationID");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strConfirmed = "";

        //            if(p_blnConfirmed)
        //            {
        //                strConfirmed = " AND t2.status_int = 2";
        //            }
        //            else
        //            {
        //                strConfirmed = " AND t2.status_int > 0";
        //            }

        //            string strSQL = @"SELECT t1.*, t2.*, t4.deptname_vchr, t5.lastname_vchr AS reportor,
        //									 t6.lastname_vchr AS confirmer, t7.lastname_vchr AS applyer,
        //									 t8.sample_type_desc_vchr, t9.application_form_no_chr AS check_no_chr,
        //									 t10.print_title_vchr, t9.SUMMARY_VCHR AS application_summary
        //								FROM t_opr_lis_sample t1,
        //									 t_opr_lis_app_report t2,
        //									 t_opr_lis_app_sample t3,
        //									 T_BSE_DEPTDESC t4,
        //									 t_bse_employee t5,
        //									 t_bse_employee t6,
        //									 t_bse_employee t7,
        //									 t_aid_lis_sampletype t8,
        //									 t_opr_lis_application t9,
        //									 t_aid_lis_report_group t10
        //							   WHERE t2.application_id_chr = '"+p_strApplID+@"'
        //								 AND t2.report_group_id_chr = '"+p_strReportGroupID+@"'
        //								 AND t2.report_group_id_chr = t3.report_group_id_chr
        //								 AND t2.application_id_chr = t3.application_id_chr
        //								 AND t3.sample_id_chr = t1.sample_id_chr
        //								 AND t2.reportor_id_chr = t5.empid_chr(+)
        //								 AND t2.confirmer_id_chr = t6.empid_chr(+)
        //								 AND t9.appl_empid_chr = t7.empid_chr(+)
        //								 AND t9.appl_deptid_chr = t4.deptid_chr(+)
        //								 AND t1.sample_type_id_chr = t8.sample_type_id_chr(+)
        //								 AND t2.application_id_chr = t9.application_id_chr
        //								 AND t2.report_group_id_chr = t10.report_group_id_chr
        //								 AND t9.pstatus_int > 0
        //								 AND t1.status_int > 0
        //								 AND t2.status_int > 0";
        //            strSQL += strConfirmed;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbReportInfo);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����report_group_id��application_id_chr��ѯ����Ľ����¼ 
        //        /// <summary>
        //        /// ����report_group_id��application_id_chr��ѯ����Ľ����¼
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="strApplicationID">���뵥ID</param>
        //        /// <param name="strReportGroupID">������ID</param>
        //        /// <param name="blnConfirmed">�Ƿ����</param>
        //        /// <param name="dtbCheckResult">���ؽ����Ϣ</param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetCheckResultByReportGroupIDAndApplicationID(
        //            string p_strApplicationID,string p_strReportGroupID,bool p_blnConfirmed,out DataTable p_dtbCheckResult)
        //        {
        //            long lngRes = 0;
        //            p_dtbCheckResult = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetCheckResultByReportGroupIDAndApplicationID");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strConfirmed = "";

        //            if(p_blnConfirmed)
        //            {
        //                strConfirmed = " AND t2.status_int = 2";
        //            }
        //            else
        //            {
        //                strConfirmed = " AND t2.status_int > 0";
        //            }
        //            string strSQL0 = @"SELECT t4.oringin_dat,t3.sample_id_chr
        //									FROM t_opr_lis_app_report t2, 
        //										t_opr_lis_app_sample t3, t_opr_lis_application t4
        //									WHERE t2.application_id_chr = '" + p_strApplicationID + @"' 
        //									AND t2.report_group_id_chr = '" + p_strReportGroupID + @"'
        //									AND t4.application_id_chr = t2.application_id_chr
        //									AND t4.pstatus_int >= 0
        //									AND t3.application_id_chr = t2.application_id_chr
        //									AND t3.report_group_id_chr = t2.report_group_id_chr      
        //									";
        //            //t7.assist_code02_chr AS item_type ���� ϸ��������ʽ��ҩ����
        //            string strSQL = @"SELECT /*+ use_hash(t1) */ t1.*, t9.print_title_vchr, t5.print_seq_int AS report_print_seq_int,
        //										t8.item_print_seq_int AS sample_print_seq_int, t7.rptno_chr, t7.assist_code02_chr AS item_type, t7.shortname_chr
        //									FROM (SELECT /*+ all_rows */* 
        //											FROM t_opr_lis_check_result
        //											WHERE sample_id_chr = ?
        //											AND modify_dat >= TO_DATE(?,'yyyy-mm-dd hh24:mi:ss') and status_int = 1) t1,
        //										t_aid_lis_report_group_detail t5,
        //										t_bse_lis_check_item t7,
        //										v_lis_bse_sample_group_items t8,
        //										t_aid_lis_sample_group t9
        //									WHERE t9.sample_group_id_chr = t1.groupid_chr
        //									AND t7.check_item_id_chr = t1.check_item_id_chr
        //									AND t8.check_item_id_chr = t1.check_item_id_chr
        //									AND t8.sample_group_id_chr = t1.groupid_chr
        //									AND t5.sample_group_id_chr = t1.groupid_chr";

        //            strSQL0 += strConfirmed;

        //            //			string strSQLAllRows = "ALTER SESSION SET OPTIMIZER_MODE=ALL_ROWS";

        //            string strSampleID = null;
        //            string strOriginDate = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL0,ref p_dtbCheckResult);
        //                if(lngRes == 1 && p_dtbCheckResult != null && p_dtbCheckResult.Rows.Count >0)
        //                {
        //                    strSampleID = p_dtbCheckResult.Rows[0]["sample_id_chr"].ToString().Trim();
        //                    strOriginDate = p_dtbCheckResult.Rows[0]["oringin_dat"].ToString().Trim();
        //                    IDataParameter[] objDPArr = null;
        //                    objHRPSvc.CreateDatabaseParameter(2,out objDPArr);
        //                    objDPArr[0].Value = strSampleID;
        //                    objDPArr[1].Value = strOriginDate;
        //                    p_dtbCheckResult = null;
        //                    lngRes = 0;
        //                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref p_dtbCheckResult,objDPArr);
        //                }
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #endregion

        #region ���������

        #region [U]��t_opr_lis_check_result�����һ����¼
        [AutoComplete]
        private long m_lngAddNewCheckResult( clsCheckResult_VO p_objCheckResultVO)
        {
            long lngRes = 0; 
            lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objCheckResultArr = null;
                objHRPSvc.CreateDatabaseParameter(29, out objCheckResultArr);

                try
                {
                    objCheckResultArr[0].Value = DateTime.Parse(p_objCheckResultVO.m_strModify_Dat);
                }
                catch
                {
                    p_objCheckResultVO.m_strModify_Dat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objCheckResultArr[0].Value = DateTime.Parse(p_objCheckResultVO.m_strModify_Dat);
                }
                objCheckResultArr[1].Value = p_objCheckResultVO.m_strGroupID;
                objCheckResultArr[2].Value = p_objCheckResultVO.m_strCheck_Item_ID;
                objCheckResultArr[3].Value = p_objCheckResultVO.m_strSample_ID;
                objCheckResultArr[4].Value = p_objCheckResultVO.m_strResult;
                objCheckResultArr[5].Value = p_objCheckResultVO.m_strUnit;
                objCheckResultArr[6].Value = p_objCheckResultVO.m_strDeviceID;
                objCheckResultArr[7].Value = p_objCheckResultVO.m_strDevice_Check_Item_Name;
                objCheckResultArr[8].Value = p_objCheckResultVO.m_strRefrange;
                objCheckResultArr[9].Value = p_objCheckResultVO.m_strCheck_Item_Name;
                objCheckResultArr[10].Value = p_objCheckResultVO.m_strCheck_Item_English_Name;
                objCheckResultArr[11].Value = p_objCheckResultVO.m_strMin_Val;
                objCheckResultArr[12].Value = p_objCheckResultVO.m_strMax_Val;
                objCheckResultArr[13].Value = p_objCheckResultVO.m_strAbnormal_Flag;
                if (p_objCheckResultVO.m_strCheck_Dat == null || p_objCheckResultVO.m_strCheck_Dat.Trim() == "")
                {
                    objCheckResultArr[14].Value = System.DBNull.Value;
                }
                else
                {
                    objCheckResultArr[14].Value = System.DateTime.Parse(p_objCheckResultVO.m_strCheck_Dat);
                }
                objCheckResultArr[15].Value = p_objCheckResultVO.m_strClinicApp;
                objCheckResultArr[16].Value = p_objCheckResultVO.m_strMemo;
                if (p_objCheckResultVO.m_strConfirm_Dat == null || p_objCheckResultVO.m_strConfirm_Dat.Trim() == "")
                {
                    objCheckResultArr[17].Value = System.DBNull.Value;
                }
                else
                {
                    objCheckResultArr[17].Value = System.DateTime.Parse(p_objCheckResultVO.m_strConfirm_Dat);
                }
                objCheckResultArr[18].Value = p_objCheckResultVO.m_strPointliststr;
                objCheckResultArr[19].Value = p_objCheckResultVO.m_strSummary;
                objCheckResultArr[20].Value = p_objCheckResultVO.m_byaGraph;
                objCheckResultArr[21].Value = p_objCheckResultVO.m_intStatus;
                objCheckResultArr[22].Value = p_objCheckResultVO.m_strChecker1;
                objCheckResultArr[23].Value = p_objCheckResultVO.m_strChecker2;
                objCheckResultArr[24].Value = p_objCheckResultVO.m_strConfirm_Person;
                objCheckResultArr[25].Value = p_objCheckResultVO.m_strOperator_ID;
                objCheckResultArr[26].Value = p_objCheckResultVO.m_strCheck_DeptID;
                objCheckResultArr[27].Value = p_objCheckResultVO.strGraphFormatName;
                objCheckResultArr[28].Value = p_objCheckResultVO.intIsGraphResult;


                long lngRecEff = -1;

                //����t_opr_lis_application���Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddNewCheckResult_SQL, ref lngRecEff, objCheckResultArr);


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

            }
            return lngRes;
        }
        #endregion

        #region [U]��t_opr_lis_check_result����������¼����һ����¼���ϴ����м�������м���������룬��Ϊ��������Ҫ��Ҳ����������Ҫ��

        /// <summary>
        /// ���ñ�����ʱ,���贫�� p_strSampleIDArr �е����е����������м�����Ŀ���,��ֻ�ܴ���
        /// �� p_strSampleIDArr �б�֮�е������ļ�����Ŀ���;
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckResultList"></param>
        /// <param name="p_strSampleIDArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddCheckResultList( clsCheckResult_VO[] p_objCheckResultList, string[] p_strSampleIDArr, string p_strOriginDate)
        {
            long lngRes = 0;
            string strCurrTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
            lngRes = 0;
            try
            {
                string strSQL1 = null;

                strSQL1 = @"UPDATE t_opr_lis_check_result
									SET status_int = -1
									WHERE status_int > 0 AND sample_id_chr IN (";

                string strSQL2 = @")";
                if (p_strSampleIDArr == null || p_strSampleIDArr.Length == 0)
                    return -2;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(p_strSampleIDArr.Length, out objDPArr);
                for (int i = 0; i < p_strSampleIDArr.Length; i++)
                {
                    sb.Append("?,");
                    objDPArr[i].Value = p_strSampleIDArr[i];
                }
                sb.Remove(sb.Length - 1, 1);
                string strSQL3 = sb.ToString();

                string strSQL = strSQL1 + strSQL3 + strSQL2;
                try
                {
                    DateTime dtm = Convert.ToDateTime(p_strOriginDate);
                    strSQL = strSQL + @" AND modify_dat >= to_date('" + p_strOriginDate + "','yyyy-mm-dd hh24:mi:ss')";
                }
                catch { }

                lngRes = 0;
                long lngEff = 0;
                strSQL = @"update t_opr_lis_check_result
									set status_int = -1
									where status_int > 0 and sample_id_chr =? and modify_dat>=? ";
                int n = 0;

                DbType[] dbTypes = new DbType[] { 
                        DbType.String,DbType.DateTime
                       
                        };
                object[][] objValues = new object[2][];


                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[p_strSampleIDArr.Length];//��ʼ��
                }
                DateTime m_dtTime;
                DateTime.TryParse(p_strOriginDate, out m_dtTime);
                for (int k1 = 0; k1 < p_strSampleIDArr.Length; k1++)
                {

                    n = -1;
                    //��ˮ��
                    objValues[++n][k1] = p_strSampleIDArr[k1].ToString().PadRight(10, ' ');
                    objValues[++n][k1] = m_dtTime;
                    //��������
                }

                if (p_strSampleIDArr.Length > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    lngRes = 1;
                }
                objHRPSvc.Dispose();
                if (lngRes == 1)
                {
                    lngRes = 0;
                    clsSampleSvc objSampleSv = new clsSampleSvc();
                    lngRes = objSampleSv.m_lngUpdateSampleFlag( p_strSampleIDArr, 3, 5, p_strOriginDate);
                    objSampleSv.Dispose();
                }
                if (lngRes == 1)
                {
                    if (p_objCheckResultList != null)
                    {
                        for (int i = 0; i < p_objCheckResultList.Length; i++)
                        {
                            if (p_objCheckResultList[i] != null)
                            {
                                p_objCheckResultList[i].m_strModify_Dat = strCurrTime;
                                lngRes = 0;
                                lngRes = m_lngAddNewCheckResult( p_objCheckResultList[i]);
                                if (lngRes <= 0)
                                {
                                    throw new Exception("������ʧ��!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region �����ӿ�

        //        #region ���ݼ������ں�������Ų�ѯDeviceResultLog
        //        /// <summary>
        //        /// ���ݼ������ں�������Ų�ѯDeviceResultLog
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strCheckDatFrom"></param>
        //        /// <param name="p_strCheckDatTo"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceResultLogByCondition(string p_strCheckDatFrom,
        //            string p_strCheckDatTo,string p_strDeviceID,out clsResultLogVO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_mthGetDeviceResultLogByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"SELECT *
        //								FROM t_opr_Lis_result_log
        //							   WHERE CHECK_DAT between TO_DATE('"+p_strCheckDatFrom+@"','yyyy-mm-dd HH24:mi:ss') 
        //												   and TO_DATE('"+p_strCheckDatTo+@"','yyyy-mm-dd hh24:mi:ss')
        //								 AND DEVICEID_CHR = '"+p_strDeviceID+@"'";

        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsResultLogVO[dtbResult.Rows.Count];
        //                    for(int i=0;i<p_objResultArr.Length;i++)
        //                    {
        //                        p_objResultArr[i] = new clsResultLogVO();
        //                        p_objResultArr[i].m_strBeginIndex = dtbResult.Rows[i]["BEGIN_IDX_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strCheckDat = dtbResult.Rows[i]["CHECK_DAT"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceID = dtbResult.Rows[i]["DEVICEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceSampleID = dtbResult.Rows[i]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strEndIndex = dtbResult.Rows[i]["END_IDX_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strIMPORT_REQ_INT = dtbResult.Rows[i]["IMPORT_REQ_INT"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = 0;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ��������ŵõ���Ӧ�������������� 
        //        /// <summary>
        //        /// ��������ŵõ���Ӧ��������������
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strAppID"></param>
        //        /// <param name="p_objDRVOArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceRelationByAppID(
        //            string p_strAppID,out clsT_LIS_DeviceRelationVO[] p_objDRVOArr)
        //        {
        //            long lngRes=0;
        //            p_objDRVOArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceDataByAppointment");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            lngRes = 0;

        //            if(p_strAppID == null)
        //                return -1;

        //            DataTable dtbRelation = null;
        //            string strSQL = @"SELECT DISTINCT t2.*
        //								FROM t_opr_lis_app_sample t1, t_opr_lis_device_relation t2
        //								WHERE t1.sample_id_chr = t2.sample_id_chr
        //								AND t2.status_int > 0
        //								AND t1.application_id_chr = ?";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //                System.Data.IDataParameter[] objDPArr= null;
        //                objHRPSvc.CreateDatabaseParameter(1,out objDPArr);

        //                objDPArr[0].Value = p_strAppID.Trim();
        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbRelation, objDPArr);
        //                objHRPSvc.Dispose();

        //                if(lngRes > 0  && dtbRelation != null && dtbRelation.Rows.Count != 0)
        //                {
        //                    p_objDRVOArr = new clsT_LIS_DeviceRelationVO[dtbRelation.Rows.Count];
        //                    for(int i=0;i<dtbRelation.Rows.Count;i++)
        //                    {
        //                        p_objDRVOArr[i] = new clsVOConstructor().m_objConstructDeviceRelationVO(dtbRelation.Rows[i]);
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = 0;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ���������ں�,����

        #region ������������������־
        /// <summary>
        /// ������������������־
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_intImportReq"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetDeviceSamplesRecheck(
            string p_strDeviceID, int p_intImportReq)
        {
            long lngRes = 0; 

            string strSQL = @"UPDATE t_opr_lis_result_import_req
								SET recheck_flag_chr = '1'
								WHERE deviceid_chr = ? AND import_req_int = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strDeviceID;
                objDPArr[1].Value = p_intImportReq;

                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        //        #region ����Imp_Req_int������ID��ѯ�걾�б� 
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strImpReq"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleListByCondition(string p_strImpReq,string p_strDeviceID,
        //            out clsResultLogVO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceSampleListByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            #region SQL
        //            string strSQL = @"SELECT a.*,d.devicesample_status_int, d.sample_status_int
        //							    FROM t_opr_lis_result_log a,
        //									 (SELECT b.status_int AS devicesample_status_int,
        //											 c.status_int AS sample_status_int, c.sample_id_chr,
        //											 b.device_sampleid_chr, b.check_dat, b.import_req_int,
        //											 b.deviceid_chr
        //										FROM t_opr_lis_device_relation b, t_opr_lis_sample c
        //									   WHERE b.status_int > 0
        //										 AND b.sample_id_chr = c.sample_id_chr
        //										 AND c.status_int > -1) d
        //							  WHERE a.import_req_int = d.import_req_int(+)
        //								AND a.deviceid_chr = d.deviceid_chr(+)
        //								AND a.import_req_int = '"+p_strImpReq+@"'
        //								AND a.deviceid_chr = '"+p_strDeviceID+@"'
        //							 ORDER BY a.import_req_int";
        //            #endregion

        //            DataTable dtbResult = new DataTable();
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsResultLogVO[dtbResult.Rows.Count];
        //                    for(int i=0;i<dtbResult.Rows.Count;i++)
        //                    {
        //                        p_objResultArr[i] = new clsResultLogVO();
        //                        p_objResultArr[i].m_strBeginIndex = dtbResult.Rows[i]["BEGIN_IDX_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strCheckDat = dtbResult.Rows[i]["CHECK_DAT"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceID = dtbResult.Rows[i]["DEVICEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceSampleID = dtbResult.Rows[i]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strEndIndex = dtbResult.Rows[i]["END_IDX_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strUseFlag = dtbResult.Rows[i]["USE_FLAG_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strIMPORT_REQ_INT = dtbResult.Rows[i]["IMPORT_REQ_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceSample_status = dtbResult.Rows[i]["devicesample_status_int"].ToString().Trim();
        //                        p_objResultArr[i].m_strSample_status = dtbResult.Rows[i]["sample_status_int"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region �������������ţ�����ID�ͼ���ʱ���ѯ�걾�б� 
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceSampleID"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_strCheckDat"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleListByCondition(string p_strDeviceSampleID,
        //            string p_strDeviceID,string p_strCheckDat,out clsResultLogVO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceSampleListByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            #region SQL
        //            string strSQL = @"SELECT a.*,d.devicesample_status_int, d.sample_status_int
        //							    FROM t_opr_lis_result_log a,
        //									 (SELECT b.status_int AS devicesample_status_int,
        //											 c.status_int AS sample_status_int, c.sample_id_chr,
        //											 b.device_sampleid_chr, b.check_dat, b.import_req_int,
        //											 b.deviceid_chr
        //										FROM t_opr_lis_device_relation b, t_opr_lis_sample c
        //									   WHERE b.status_int > 0
        //										 AND b.sample_id_chr = c.sample_id_chr
        //										 AND c.status_int > -1) d
        //							  WHERE a.import_req_int = d.import_req_int(+)
        //								AND a.deviceid_chr = d.deviceid_chr(+)
        //								AND a.device_sampleid_chr = '"+p_strDeviceSampleID+@"'
        //								AND TRUNC (a.check_dat) =
        //										TRUNC (TO_DATE ('"+p_strCheckDat+@"', 'yyyy-mm-dd'))
        //								AND a.deviceid_chr = '"+p_strDeviceID+@"'
        //							 ORDER BY a.import_req_int";
        //            #endregion

        //            DataTable dtbResult = new DataTable();
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsResultLogVO[dtbResult.Rows.Count];
        //                    for(int i=0;i<dtbResult.Rows.Count;i++)
        //                    {
        //                        p_objResultArr[i] = new clsResultLogVO();
        //                        p_objResultArr[i].m_strBeginIndex = dtbResult.Rows[i]["BEGIN_IDX_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strCheckDat = dtbResult.Rows[i]["CHECK_DAT"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceID = dtbResult.Rows[i]["DEVICEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceSampleID = dtbResult.Rows[i]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strEndIndex = dtbResult.Rows[i]["END_IDX_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strUseFlag = dtbResult.Rows[i]["USE_FLAG_CHR"].ToString().Trim();
        //                        p_objResultArr[i].m_strIMPORT_REQ_INT = dtbResult.Rows[i]["IMPORT_REQ_INT"].ToString().Trim();
        //                        p_objResultArr[i].m_strDeviceSample_status = dtbResult.Rows[i]["devicesample_status_int"].ToString().Trim();
        //                        p_objResultArr[i].m_strSample_status = dtbResult.Rows[i]["sample_status_int"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region �����ںϺ����������������־��Ϣ
        [AutoComplete]
        public long m_lngAddNewDeviceCheckResultArrANDLog( clsDeviceReslutVO[] p_objDeviceResultArr,
            clsResultLogVO p_objResultLog)
        {
            long lngRes = 0; 

            for (int i = 0; i < p_objDeviceResultArr.Length; i++)
            {
                lngRes = m_lngAddNewDeviceResult( p_objDeviceResultArr[i]);
                if (i == 0)
                {
                    p_objResultLog.m_strBeginIndex = p_objDeviceResultArr[i].m_strIdx;
                }
                else if (i == p_objDeviceResultArr.Length - 1)
                {
                    p_objResultLog.m_strEndIndex = p_objDeviceResultArr[i].m_strIdx;
                }
            }

            //			lngRes = m_lngSetUseFlagByCondition(p_objResultLog.m_strDeviceID,int.Parse(p_objResultLog.m_strIMPORT_REQ_INT));
            if (lngRes > 0)
            {
                lngRes = m_lngAddNewResultLog( p_objResultLog);
                if (lngRes > 0)
                {
                    try
                    {
                        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                        #region �õ����������Ĳɼ����
                        DataTable dtbReq = null;
                        int intImportReq = -1;
                        string strSQL_GetImportReqInt = @"SELECT MAX (import_req_int) + 1 AS import_req_int
															FROM t_opr_lis_result_import_req
														   WHERE deviceid_chr = ? 
														GROUP BY deviceid_chr";

                        System.Data.IDataParameter[] objDPArr4 = null;
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr4);
                        objDPArr4[0].Value = p_objResultLog.m_strDeviceID;

                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL_GetImportReqInt, ref dtbReq, objDPArr4);
                        if (lngRes == 1 && dtbReq != null && dtbReq.Rows.Count != 0)
                        {
                            intImportReq = int.Parse(dtbReq.Rows[0]["import_req_int"].ToString());
                        }
                        else if (lngRes == 1)
                        {
                            intImportReq = 0;
                        }
                        else
                        {
                            lngRes = 0;
                        }
                        #endregion

                        if (lngRes > 0)
                        {
                            string strSQL = @"INSERT INTO t_opr_lis_result_import_req(CHECK_DAT,DEVICEID_CHR,DEVICE_SAMPLEID_CHR,IMPORT_REQ_INT) 
												   VALUES(?,?,?,?)";
                            System.Data.IDataParameter[] objArr = null;
                            objHRPSvc.CreateDatabaseParameter(4, out objArr);
                            objArr[0].Value = p_objResultLog.m_strCheckDat;
                            objArr[1].Value = p_objResultLog.m_strDeviceID;
                            objArr[2].Value = p_objResultLog.m_strDeviceSampleID;
                            objArr[3].Value = intImportReq;

                            long lngRecEff = -1;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objArr);
                            objHRPSvc.Dispose();
                        }
                    }
                    catch (Exception objEx)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region ���t_opr_lis_result��һ����¼
        [AutoComplete]
        public long m_lngAddNewDeviceResult(
            clsDeviceReslutVO objDeviceResultVO)
        {
            long lngRes = 0; 

            #region SQL
            string strSQL = @"INSERT INTO t_opr_lis_result
										  (idx_int, result_vchr, device_check_item_name_vchr, unit_vchr,
										   device_sampleid_chr, refrange_vchr, min_val_dec, max_val_dec,
										   abnormal_flag_vchr, deviceid_chr, check_dat, pstatus_int
										  )
							  VALUES (?, ?, ?, ?,
									  ?, ?, ?, ?,
									  ?, ?, ?, ?
									 )";
            #endregion

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //			objDeviceResultVO.m_strIdx = (objHRPSvc.intGetNewNumericID("IDX_INT","t_opr_lis_result")).ToString().Trim();
            objDeviceResultVO.m_strIdx = new clsLIS_Svc().m_mthGetNewResultIndex(1, true).ToString();

            try
            {
                System.Data.IDataParameter[] objDeviceResultArr = null;
                objHRPSvc.CreateDatabaseParameter(12, out objDeviceResultArr);
                objDeviceResultArr[0].Value = objDeviceResultVO.m_strIdx;
                objDeviceResultArr[1].Value = objDeviceResultVO.m_strResult;
                objDeviceResultArr[2].Value = objDeviceResultVO.m_strDeviceCheckItemName;
                objDeviceResultArr[3].Value = objDeviceResultVO.m_strUnit;
                objDeviceResultArr[4].Value = objDeviceResultVO.m_strDeviceSampleID;
                objDeviceResultArr[5].Value = objDeviceResultVO.m_strRefRange;
                objDeviceResultArr[6].Value = objDeviceResultVO.m_strMinVal;
                objDeviceResultArr[7].Value = objDeviceResultVO.m_strMaxVal;
                objDeviceResultArr[8].Value = objDeviceResultVO.m_strAbnormalFlag;
                objDeviceResultArr[9].Value = objDeviceResultVO.m_strDeviceID;
                objDeviceResultArr[10].Value = System.DateTime.Parse(objDeviceResultVO.m_strCheckDat);
                objDeviceResultArr[11].Value = objDeviceResultVO.m_strPstatus;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDeviceResultArr);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ����t_opr_lis_result_log��User_flag�ֶ�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDevcieID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDat"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetUseFlagByCondition( string p_strDevcieID, int p_strImpReqInt)
        {
            long lngRes = 0; 

            string strSQL = @"UPDATE t_opr_lis_result_log
								 SET use_flag_chr = '0'
							   WHERE import_req_int = '" + p_strImpReqInt + @"'
								 AND deviceid_chr = '" + p_strDevcieID + @"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ���t_opr_lis_result_log����һ����¼
        [AutoComplete]
        public long m_lngAddNewResultLog( clsResultLogVO objResultLogVO)
        {
            long lngRes = 0; 

            string strSQL = @"INSERT INTO t_opr_lis_result_log
										  (begin_idx_int, check_dat, device_sampleid_chr, end_idx_int,
										   deviceid_chr,use_flag_chr,import_req_int
										  )
								   VALUES (?, ?, ?, ?, ?, ?, ?)";
            try
            {                
                if( Convert.ToDecimal(objResultLogVO.m_strBeginIndex) > Convert.ToDecimal(objResultLogVO.m_strEndIndex))
                {
                    string tmpIdx = objResultLogVO.m_strEndIndex;
                    objResultLogVO.m_strEndIndex = objResultLogVO.m_strBeginIndex;
                    objResultLogVO.m_strBeginIndex = tmpIdx;
                }

                System.Data.IDataParameter[] objResultLogArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(7, out objResultLogArr);
                objResultLogArr[0].Value = objResultLogVO.m_strBeginIndex;
                objResultLogArr[1].Value = System.DateTime.Parse(objResultLogVO.m_strCheckDat);
                objResultLogArr[2].Value = objResultLogVO.m_strDeviceSampleID;
                objResultLogArr[3].Value = objResultLogVO.m_strEndIndex;
                objResultLogArr[4].Value = objResultLogVO.m_strDeviceID;
                objResultLogArr[5].Value = objResultLogVO.m_strUseFlag;
                objResultLogArr[6].Value = objResultLogVO.m_strIMPORT_REQ_INT;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objResultLogArr);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion
        #endregion

        //        #region ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�󶨺���ȡ����
        //        /// <summary>
        //        /// ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�󶨺���ȡ����
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_strDeviceSampleID"></param>
        //        /// <param name="p_strCheckDate"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns>
        //        /// С�ڵ��� 0 : ��ѯʧ��; 
        //        /// 100: �޿ɰ󶨵���������; 
        //        /// 300: ָ������������������ʷ��¼; 
        //        /// 400:ָ��������������ԭʼ����; 
        //        /// ����: �ɹ�����
        //        /// </returns>
        //        [AutoComplete]
        //        public long m_lngQueryBindAndGetDeviceDataByAppointment(
        //            string p_strDeviceID,string p_strDeviceSampleID,string p_strCheckDate,
        //            out clsDeviceReslutVO[] p_objResultArr)
        //        {
        //            long lngRes=0;
        //            p_objResultArr = null;
        //            DataTable dtbResult = null;
        //            clsResultLogVO objResultLogVO = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceDataByAppointment");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			

        //            if(p_strDeviceID == null || p_strDeviceSampleID == null || !Microsoft.VisualBasic.Information.IsDate(p_strCheckDate))
        //                return -1;

        //            try
        //            {
        //                lngRes = 0;
        //                lngRes = m_lngQueryBindByAppointment(p_strDeviceID,p_strDeviceSampleID,p_strCheckDate,out objResultLogVO);
        //                if(lngRes == 100 || lngRes == 300 || lngRes == 400 || lngRes <= 0)
        //                {
        //                    return lngRes;
        //                }
        //                else
        //                {
        //                    lngRes = 0;
        //                    int intBeginIndex = int.Parse(objResultLogVO.m_strBeginIndex);
        //                    int intEndIndex = int.Parse(objResultLogVO.m_strEndIndex);
        //                    lngRes = m_lngGetDeviceData(p_strDeviceID,objResultLogVO.m_strDeviceSampleID,objResultLogVO.m_strCheckDat, intBeginIndex,intEndIndex,out p_objResultArr);
        //                }

        //            }			
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�� 
        //        /// <summary>
        //        /// ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�� ���� 2004.06.10
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_strDeviceSampleID"></param>
        //        /// <param name="p_strCheckDate"></param>
        //        /// <param name="p_objResultLogVO">�Դ˵������������ȡ����</param>
        //        /// <returns>
        //        /// С�ڵ��� 0 : ��ѯʧ��;
        //        /// 100: �޿ɰ󶨵���������;
        //        /// 300: ָ������������������ʷ��¼;
        //        /// ����: �ɹ�����
        //        /// </returns>
        //        [AutoComplete]
        //        public long m_lngQueryBindByAppointment(
        //            string p_strDeviceID,string p_strDeviceSampleID,string p_strCheckDate,
        //            out clsResultLogVO p_objResultLogVO)
        //        {
        //            long lngRes=0;
        //            p_objResultLogVO = null;
        //            DataTable dtbImportReq = null;
        //            string strSQL = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceDataByAppointment");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			
        //            if(p_strDeviceID == null || p_strDeviceSampleID == null || !Microsoft.VisualBasic.Information.IsDate(p_strCheckDate))
        //                return -1;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //                #region ��ѯ��
        //                #region �� SQL ���ܲ���
        //                //				strSQL =@"SELECT *
        //                //							FROM t_opr_lis_result_import_req
        //                //							WHERE CONCAT (deviceid_chr, TO_CHAR (import_req_int)) =
        //                //									(SELECT   CONCAT (deviceid_chr,
        //                //														TO_CHAR (MAX (import_req_int))
        //                //													) AS flag
        //                //										FROM t_opr_lis_result_import_req
        //                //										WHERE deviceid_chr = ? 
        //                //											AND TRIM (device_sampleid_chr) = ? 
        //                //											AND TRUNC (check_dat) = 
        //                //												TRUNC (TO_DATE (?, 'yyyy-mm-dd hh24:mi:ss'))
        //                //									GROUP BY deviceid_chr)";

        //                #endregion	

        //                strSQL = @"SELECT *
        //							FROM (SELECT   *
        //										FROM t_opr_lis_result_import_req
        //									WHERE deviceid_chr = ? 
        //										AND TRIM (device_sampleid_chr) = ? 
        //										AND TRUNC (check_dat) =
        //												TRUNC (TO_DATE (?,
        //																'yyyy-mm-dd hh24:mi:ss'))
        //									ORDER BY import_req_int DESC)
        //							WHERE ROWNUM = 1";

        //                System.Data.IDataParameter[] objDPArr= null;
        //                objHRPSvc.CreateDatabaseParameter(3,out objDPArr);

        //                objDPArr[0].Value = p_strDeviceID.Trim();
        //                objDPArr[1].Value = p_strDeviceSampleID.Trim();				
        //                objDPArr[2].Value = p_strCheckDate;
        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbImportReq, objDPArr);
        //                objHRPSvc.Dispose();
        //                #endregion

        //                if(lngRes > 0  && (dtbImportReq == null || dtbImportReq.Rows.Count == 0))
        //                {
        //                    return 100;//�޿ɰ󶨵���������
        //                }
        //                else if(lngRes <= 0)
        //                {
        //                    return lngRes;//��ѯʧ��
        //                }
        //                DataRow dtrSample = dtbImportReq.Rows[0];

        //                string strCheckDate = dtrSample["check_dat"].ToString();
        //                int intImportReq = int.Parse(dtrSample["import_req_int"].ToString().Trim());
        //                lngRes = 0;
        //                lngRes = m_lngQueryResultLog(p_strDeviceID,intImportReq,out p_objResultLogVO);

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = -1;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region  m_lngGetDeviceData ����ָ�����������,REQ ,��ѯ DeviceResultLog ����ȡ�������� 
        //        /// <summary>
        //        ///  ����ָ�����������,REQ ,��ѯ DeviceResultLog ����ȡ�������� ���� 2004.06.10	
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_intImportReq"></param>
        //        /// <param name="p_objDeviceResultList"></param>
        //        /// <returns>
        //        /// С�� 0 : ��ѯʧ��;
        //        /// 300:ָ����������������ʷ��¼
        //        /// 400:ָ��������������ԭʼ����
        //        /// ����: �ɹ����� 
        //        /// </returns>
        //        [AutoComplete]		
        //        public long m_lngGetDeviceData(
        //            string p_strDeviceID,int p_intImportReq,
        //            out clsDeviceReslutVO[] p_objDeviceResultList)
        //        {
        //            long lngRes=0;
        //            p_objDeviceResultList = null;
        //            DataTable dtbResult = null;
        //            string strSQL = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceData");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			
        //            if(p_strDeviceID == null)
        //                return -1;
        //            try
        //            {
        //                clsResultLogVO objResultLogVO = null;
        //                lngRes = 0;
        //                lngRes = m_lngQueryResultLog(p_strDeviceID, p_intImportReq,out objResultLogVO);
        //                if(lngRes <= 0)
        //                    return lngRes;
        //                if(objResultLogVO == null)
        //                {
        //                    return 300;
        //                }
        //                lngRes = 0;
        //                lngRes = this.m_lngGetDeviceData(p_strDeviceID,
        //                    objResultLogVO.m_strDeviceSampleID, objResultLogVO.m_strCheckDat,int.Parse(objResultLogVO.m_strBeginIndex.Trim()),int.Parse(objResultLogVO.m_strEndIndex.Trim()),out p_objDeviceResultList);
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = -1;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region m_lngQueryResultLog ����ָ�����������,REQ ��ѯDeviceResultLog 
        //        /// <summary>
        //        /// ����ָ�����������,REQ ��ѯDeviceResultLog ���� 2004.06.10
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_intImportReq"></param>
        //        /// <param name="p_objResultLogVO">�Դ˵������������ȡ����</param>
        //        /// <returns>
        //        /// С�� 0 : ��ѯʧ��; 
        //        /// 300: ָ������������������ʷ��¼; 
        //        /// ����: �ɹ����� 
        //        /// </returns>
        //        [AutoComplete]
        //        public long m_lngQueryResultLog(
        //            string p_strDeviceID,int p_intImportReq,
        //            out clsResultLogVO p_objResultLogVO)
        //        {
        //            long lngRes=0;
        //            p_objResultLogVO = null;
        //            DataTable dtbResultLog = null;
        //            string strSQL = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngQueryResultLog");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			
        //            if(p_strDeviceID == null)
        //                return -1;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();			

        //                strSQL =@"SELECT deviceid_chr,import_req_int,device_sampleid_chr,check_dat,begin_idx_int,end_idx_int
        //						  FROM t_opr_lis_result_log
        //						  WHERE deviceid_chr = ? 
        //						  AND import_req_int = ?
        //						  ";

        //                System.Data.IDataParameter[] objDPArr1= null;
        //                objHRPSvc.CreateDatabaseParameter(2,out objDPArr1);

        //                objDPArr1[0].Value = p_strDeviceID.Trim();
        //                objDPArr1[1].Value = p_intImportReq;

        //                lngRes = 0;
        //                lngRes=objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbResultLog, objDPArr1);


        //                if(lngRes > 0  && (dtbResultLog == null || dtbResultLog.Rows.Count == 0))
        //                {
        //                    return 300;//ָ����������������ʷ��¼
        //                }
        //                else if(lngRes > 0 )
        //                {
        //                    p_objResultLogVO = new clsResultLogVO();
        //                    p_objResultLogVO.m_strBeginIndex = dtbResultLog.Rows[0]["BEGIN_IDX_INT"].ToString().Trim();
        //                    p_objResultLogVO.m_strCheckDat = dtbResultLog.Rows[0]["CHECK_DAT"].ToString().Trim();
        //                    p_objResultLogVO.m_strDeviceID = dtbResultLog.Rows[0]["DEVICEID_CHR"].ToString().Trim();
        //                    p_objResultLogVO.m_strDeviceSampleID = dtbResultLog.Rows[0]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                    p_objResultLogVO.m_strEndIndex = dtbResultLog.Rows[0]["END_IDX_INT"].ToString().Trim();
        //                    p_objResultLogVO.m_strIMPORT_REQ_INT = dtbResultLog.Rows[0]["IMPORT_REQ_INT"].ToString().Trim();
        //                }

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = -1;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region  m_lngGetDeviceData ����ָ�����������,��������,�������������,����ʼ�����ͽ���������ȡ�������� 
        //        /// <summary>
        //        ///  ����ָ�����������,��������,�������������,����ʼ�����ͽ���������ȡ�������� ���� 2004.06.10
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_strDeviceSampleID"></param>
        //        /// <param name="p_strCheckDate"></param>
        //        /// <param name="p_intBeginIndex"></param>
        //        /// <param name="p_intEndIndex"></param>
        //        /// <param name="p_objDeviceResultList"></param>
        //        /// <returns>
        //        /// С�ڵ��� 0 : ��ѯʧ��; 
        //        /// 400:ָ��������������ԭʼ����
        //        /// ����: �ɹ����� 
        //        /// </returns>
        //        [AutoComplete]		
        //        public long m_lngGetDeviceData(
        //            string p_strDeviceID,string p_strDeviceSampleID,string p_strCheckDate,int p_intBeginIndex,int p_intEndIndex,
        //            out clsDeviceReslutVO[] p_objDeviceResultList)
        //        {
        //            long lngRes=0;
        //            p_objDeviceResultList = null;
        //            DataTable dtbResult = null;
        //            string strSQL = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetDeviceData");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			
        //            if(p_strDeviceID == null || p_strDeviceSampleID == null || !Microsoft.VisualBasic.Information.IsDate(p_strCheckDate))
        //                return -1;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                //change by wjqin(07-4-23)

        ////                strSQL =@"SELECT *
        ////						  FROM t_opr_lis_result 
        ////						  WHERE deviceid_chr = '"+ p_strDeviceID.Trim() + @"'  
        ////						  AND TRIM(device_sampleid_chr) = '"+ p_strDeviceSampleID.Trim() + @"'  
        ////						  AND check_dat = to_date('" + p_strCheckDate.Trim() + @"','yyyy-mm-dd hh24:mi:ss') 
        ////						  AND idx_int >= " + p_intBeginIndex.ToString() + @" 
        ////						  AND idx_int <= " + p_intEndIndex.ToString() + " ";



        ////                lngRes = 0;
        ////                lngRes=objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                /*==================================>*/
        //                DateTime m_dtDate;
        //                DateTime.TryParse(p_strCheckDate, out m_dtDate);
        //                    strSQL = @"
        //                    SELECT idx_int,device_check_item_name_vchr,device_sampleid_chr,abnormal_flag_vchr, check_dat, 
        //                    min_val_dec,  max_val_dec,deviceid_chr, 
        //                    pstatus_int, refrange_vchr, result_vchr, unit_vchr,
        //                    graph_img,graph_format_name_vchr,  is_graph_result_num
        //                    FROM t_opr_lis_result
        //						  WHERE deviceid_chr = ?
        //						  AND trim(device_sampleid_chr) = ?
        //						  AND check_dat = ?
        //						  AND idx_int >= ? 
        //						  AND idx_int <= ?
        //                         ";
        //                    lngRes = 0;
        //                    System.Data.IDataParameter[] objDPArr = null;
        //                    objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
        //                    objDPArr[0].Value = p_strDeviceID.PadRight(6, ' ');
        //                    objDPArr[1].Value = p_strDeviceSampleID.Trim();
        //                    objDPArr[2].Value = m_dtDate;
        //                    objDPArr[3].Value = p_intBeginIndex;
        //                    objDPArr[4].Value = p_intEndIndex;

        //                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);



        //                /*<================================*/

        //                if(lngRes > 0 && (dtbResult == null || dtbResult.Rows.Count == 0))
        //                {
        //                    return 400;//ָ��������������ԭʼ����
        //                }
        //                else if(lngRes > 0)
        //                {
        //                    p_objDeviceResultList = new clsDeviceReslutVO[dtbResult.Rows.Count];
        //                    for(int i=0;i<dtbResult.Rows.Count;i++)
        //                    {
        //                        p_objDeviceResultList[i] = new clsDeviceReslutVO();
        //                        p_objDeviceResultList[i].m_strAbnormalFlag = dtbResult.Rows[i]["ABNORMAL_FLAG_VCHR"].ToString().Trim().ToString().Trim();
        //                        p_objDeviceResultList[i].m_strCheckDat = dtbResult.Rows[i]["CHECK_DAT"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strDeviceCheckItemName = dtbResult.Rows[i]["DEVICE_CHECK_ITEM_NAME_VCHR"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strDeviceID = dtbResult.Rows[i]["DEVICEID_CHR"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strDeviceSampleID = dtbResult.Rows[i]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strIdx = dtbResult.Rows[i]["IDX_INT"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strMaxVal = dtbResult.Rows[i]["MAX_VAL_DEC"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strMinVal = dtbResult.Rows[i]["MIN_VAL_DEC"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strPstatus = dtbResult.Rows[i]["PSTATUS_INT"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strRefRange = dtbResult.Rows[i]["REFRANGE_VCHR"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strResult = dtbResult.Rows[i]["RESULT_VCHR"].ToString().Trim();
        //                        p_objDeviceResultList[i].m_strUnit = dtbResult.Rows[i]["UNIT_VCHR"].ToString().Trim();

        //                        p_objDeviceResultList[i].strGraphFormatName = dtbResult.Rows[i]["GRAPH_FORMAT_NAME_VCHR"].ToString().Trim();
        //                        if(dtbResult.Rows[i]["GRAPH_IMG"] != DBNull.Value)
        //                        {
        //                            p_objDeviceResultList[i].bytGraph = (byte[])dtbResult.Rows[i]["GRAPH_IMG"];
        //                        }
        //                        if(dtbResult.Rows[i]["IS_GRAPH_RESULT_NUM"] != DBNull.Value)
        //                        {
        //                            p_objDeviceResultList[i].intIsGraphResult = Convert.ToInt32(dtbResult.Rows[i]["IS_GRAPH_RESULT_NUM"].ToString().Trim());
        //                        }
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = -1;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #endregion

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        //        #region ���Զ��󶨷�ʽ,����ָ�����������  ��ѯ�󶨺���ȡ���� 
        //        /// <summary>
        //        /// ��ָ����ŷ�ʽ,����ָ�����������,������������Ų�ѯ�󶨺���ȡ����
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns>
        //        /// С�� 0 : ��ѯʧ��; 
        //        /// 100: �޿ɰ󶨵���������;
        //        /// 300: ָ�������������Ŵ�����δ��,������ʷ��¼; 
        //        /// 400:ָ��������������ԭʼ����; 
        //        /// ����: �ɹ�����
        //        /// </returns>
        //        [AutoComplete]
        //        public long m_lngQueryBindAndGetDeviceDataByAutoBind(
        //            string p_strDeviceID,out clsDeviceReslutVO[] p_objResultArr)
        //        {
        //            long lngRes=0;
        //            p_objResultArr = null;
        //            DataTable dtbResult = null;
        //            clsResultLogVO objResultLogVO = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngQueryBindAndGetDeviceDataByAutoBind");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			

        //            if(p_strDeviceID == null)
        //                return -1;
        //            try
        //            {
        //                lngRes = 0;
        //                lngRes = m_lngQueryBindByAutoBind(p_strDeviceID,out objResultLogVO);
        //                if(lngRes == 100 || lngRes == 300 || lngRes <= 0)
        //                {
        //                    return lngRes;
        //                }
        //                else
        //                {
        //                    lngRes = 0;
        //                    int intBeginIndex = int.Parse(objResultLogVO.m_strBeginIndex);
        //                    string strSid = objResultLogVO.m_strDeviceSampleID.Trim();
        //                    string strCheckDate = objResultLogVO.m_strCheckDat;
        //                    int intEndIndex = int.Parse(objResultLogVO.m_strEndIndex);
        //                    lngRes = m_lngGetDeviceData(p_strDeviceID,strSid,strCheckDate, intBeginIndex,intEndIndex,out p_objResultArr);
        //                }

        //            }			
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���Զ��󶨷�ʽ,����ָ����������� ��ѯ�� 
        //        /// <summary>
        //        /// ���Զ��󶨷�ʽ,����ָ����������� ��ѯ�� ���� 2004.06.10
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_objResultLogVO">�Դ˵������������ȡ����</param>
        //        /// <returns>
        //        /// С�� 0 : ��ѯʧ��;
        //        /// 100: �޿ɰ󶨵���������;
        //        /// 300: �ɰ󶨵����������Ŵ�����δ��,������ʷ��¼ 
        //        /// ����: �ɹ�����
        //        /// </returns>
        //        [AutoComplete]
        //        public long m_lngQueryBindByAutoBind(
        //            string p_strDeviceID,out clsResultLogVO p_objResultLogVO)
        //        {
        //            long lngRes=0;
        //            p_objResultLogVO = null;
        //            DataTable dtbResultLog = null;
        //            DataTable dtbImportReq = null;
        //            string strSQL = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngQueryBindByAutoBind");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }			
        //            if(p_strDeviceID == null)
        //            {
        //                return -1;
        //            }	
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //                int intLastBindIndex = -1;

        //                #region �������һ�εİ󶨵�
        //                dtbImportReq = null;
        //                lngRes = 0;

        //                strSQL = @"SELECT *
        //						  FROM t_opr_lis_result_import_req
        //						  WHERE deviceid_chr = ? 
        //					 	  AND IS_AUTOBIND_ENDPOINTER_INT = 1";

        //                System.Data.IDataParameter[] objDPArr0= null;
        //                objHRPSvc.CreateDatabaseParameter(1,out objDPArr0);

        //                objDPArr0[0].Value=p_strDeviceID.Trim();


        //                lngRes=objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbImportReq, objDPArr0);
        //                #endregion

        //                if(lngRes <= 0)
        //                {
        //                    return lngRes;
        //                }

        //                if(dtbImportReq != null && dtbImportReq.Rows.Count != 0)
        //                {
        //                    intLastBindIndex = int.Parse(dtbImportReq.Rows[0]["IMPORT_REQ_INT"].ToString().Trim());
        //                }
        //                else
        //                {
        //                    intLastBindIndex = -1;
        //                }

        //                #region ��ѯ��

        //                dtbImportReq = null;
        //                lngRes = 0;

        //                strSQL =@"SELECT *
        //						  FROM t_opr_lis_result_import_req
        //						  WHERE deviceid_chr = ? 
        //					 	  AND IMPORT_REQ_INT > ? 
        //						  AND STATUS_INT = 1  ";

        //                System.Data.IDataParameter[] objDPArr= null;
        //                objHRPSvc.CreateDatabaseParameter(2,out objDPArr);

        //                objDPArr[0].Value=p_strDeviceID.Trim();
        //                objDPArr[1].Value= intLastBindIndex;


        //                lngRes=objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbImportReq, objDPArr);
        //                #endregion

        //                if(lngRes > 0  && (dtbImportReq == null || dtbImportReq.Rows.Count == 0))
        //                {
        //                    return 100;//�޿ɰ󶨵���������
        //                }
        //                else if(lngRes <= 0)
        //                {
        //                    return lngRes;//��ѯʧ��
        //                }

        //                int intImportReq = int.Parse(dtbImportReq.Rows[0]["IMPORT_REQ_INT"].ToString().Trim());
        //                lngRes = m_lngQueryResultLog(p_strDeviceID,intImportReq,out p_objResultLogVO);

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //                lngRes = -1;
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region OLD
        #region SQl
        private const string c_strAddNewDeviceRelation_SQL = @"INSERT INTO t_opr_lis_device_relation(SEQ_ID_CHR, 
															DEVICEID_CHR, 
															device_sampleid_chr, 
															CHECK_DAT, 
															sample_id_chr, 
															PositionID_Chr, 
															STATUS_INT,
															RECEPTION_DAT,
															SEQ_ID_DEVICE_CHR)
															VALUES(?,?,?,?,?,?,?,?,?)";
        #endregion

        #region ����������ѯ���浥��Ϣ
        //		public long m_lngGetAllReportInfo(string strFromDat,string strToDat,string strDeviceID,string strSampleFrom,string strSampleTo,string strPatientType,string strDept,string strPatientCardID,string strPatientName,out clsLisBatchReport_VO[] objBatchReportList)
        //		{
        //			long lngRes = 0;
        //			string strSampleSQL = @"SELECT a.*, e.dictname_vchr AS patienttype, c.lastname_vchr AS applyemployee,
        //										   d.deptname_vchr AS deptname,g.devicename_vchr,f.device_sampleid_chr
        //									  FROM t_opr_lis_sample a,
        //										   t_bse_employee c,
        //										   t_bse_deptbaseinfo d,
        //										   t_aid_dict e,
        //										   t_opr_lis_device_relation f,
        //										   t_bse_lis_device g
        //									 WHERE a.appl_deptid_chr = d.deptid_chr(+)
        //									   AND a.appl_empid_chr = c.empid_chr(+)
        //									   AND a.patient_type_chr = e.dictid_chr(+)
        //									   AND e.dictkind_chr(+) = '61'
        //									   AND a.status_int = 6
        //									   AND a.sample_id_chr = f.sample_id_chr
        //									   AND f.deviceid_chr = g.deviceid_chr
        //									   AND f.status_int > 0
        //									   AND a.appl_dat BETWEEN TO_DATE ('"+strFromDat+"', 'yyyy-mm-dd hh24:mi:ss') AND TO_DATE ('"+strToDat+"', 'yyyy-mm-dd hh24:mi:ss')";
        //			string strSampleID = "";
        //			if(strDeviceID != "")
        //			{
        //				strSampleSQL += " AND f.deviceid_chr = '"+strDeviceID+"'";
        //			}
        //			if(strSampleFrom != "" && strSampleTo != "")
        //			{
        //				strSampleSQL += " AND f.device_sampleid_chr between '"+strSampleFrom+"' and '"+strSampleTo+"'";
        //			}
        //			if(strPatientType != "")
        //			{
        //				strSampleSQL += " AND a.patient_type_chr = '"+strPatientType+"'";
        //			}
        //			if(strDept != "")
        //			{
        //				strSampleSQL += " AND a.appl_deptid_chr = '"+strDept+"'";
        //			}
        //			if(strPatientCardID != "")
        //			{
        //				strSampleSQL += " AND a.patientcardid_chr = '"+strPatientCardID+"'";
        //			}
        //			if(strPatientName != "")
        //			{
        //				strSampleSQL += " AND a.patient_name_vchr = '"+strPatientName+"'";
        //			}
        //			DataTable dtbSample = null;
        //			objBatchReportList = null;
        //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //			lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc","m_lngGetAllReportInfo");
        //			if(lngRes < 0)
        //			{
        //				return -1;
        //			}
        //			try
        //			{
        //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSampleSQL,ref dtbSample);
        //				if(lngRes > 0 && dtbSample != null)
        //				{
        //					int count = dtbSample.Rows.Count;
        //					if(count > 0)
        //					{
        //						objBatchReportList = new clsBatchReport_VO[count];
        //						for(int i=0;i<count;i++)
        //						{
        //							objBatchReportList[i] = new clsBatchReport_VO();
        //							objBatchReportList[i].m_objSampleVO = new clsSampleVO();
        //							objBatchReportList[i].strDeviceName = dtbSample.Rows[i]["devicename_vchr"].ToString().Trim();
        //							objBatchReportList[i].strDeviceSampleID = dtbSample.Rows[i]["device_sampleid_chr"].ToString().Trim();
        //							ConstructSampleVO(dtbSample.Rows[i],ref objBatchReportList[i].m_objSampleVO);
        //							strSampleID = objBatchReportList[i].m_objSampleVO.m_strSAMPLE_ID;
        //							string strResultSQL = @"SELECT a.* ,
        //														   b.lastname_vchr AS checkperson, 
        //														   c.lastname_vchr AS confirmperson
        //													  FROM t_opr_lis_check_result a, 
        //														   t_bse_employee b, 
        //														   t_bse_employee c 
        //													 WHERE a.checker1_chr = b.empid_chr(+)
        //													   AND a.confirm_person_chr = c.empid_chr(+)
        //													   AND a.sample_id_chr = '"+strSampleID+@"' 
        //													   AND a.status_int > 0";
        //							lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strResultSQL,ref objBatchReportList[i].m_dtbCheckResult);
        //						}
        //					}
        //				}
        //				objHRPSvc.Dispose();
        //			}
        //			catch(Exception objEx)
        //			{
        //				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //				bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //			}
        //			return lngRes;
        //		}
        #endregion

        //#region ����SampleVO 
        //[AutoComplete]
        //private void ConstructSampleVO(DataRow objRow, ref clsSampleVO objSampleVO)
        //{
        //    if(objRow["SAMPLE_ID_CHR"] != System.DBNull.Value)
        //        objSampleVO.m_strSAMPLE_ID = objRow["SAMPLE_ID_CHR"].ToString().Trim();

        //    if(objRow["application_form_no_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strApplication_ID = objRow["application_form_no_chr"].ToString().Trim();

        //    if(objRow["appl_dat"] != System.DBNull.Value)
        //        objSampleVO.m_strAppl_Dat = objRow["appl_dat"].ToString().Trim();

        //    if(objRow["sex_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strSex = objRow["sex_chr"].ToString().Trim();

        //    if(objRow["patient_name_vchr"] != System.DBNull.Value)
        //        objSampleVO.m_strPatient_Name = objRow["patient_name_vchr"].ToString().Trim();

        //    if(objRow["patient_subno_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strPatient_SubNO = objRow["patient_subno_chr"].ToString().Trim();

        //    if(objRow["age_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strAge = objRow["age_chr"].ToString().Trim();

        //    if(objRow["patient_type_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strPatient_Type = objRow["patienttype"].ToString().Trim();

        //    if(objRow["diagnose_vchr"] != System.DBNull.Value)
        //        objSampleVO.m_strDiagnose = objRow["diagnose_vchr"].ToString().Trim();

        //    if(objRow["sampletype_vchr"] != System.DBNull.Value)
        //        objSampleVO.m_strSampleType = objRow["sampletype_vchr"].ToString().Trim();

        //    if(objRow["samplestate_vchr"] != System.DBNull.Value)
        //        objSampleVO.m_strSampleState = objRow["samplestate_vchr"].ToString().Trim();

        //    if(objRow["bedno_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strBedNO = objRow["bedno_chr"].ToString().Trim();

        //    if(objRow["icd_vchr"] != System.DBNull.Value)
        //        objSampleVO.m_strIcd = objRow["icd_vchrr"].ToString().Trim();

        //    if(objRow["PATIENTCARDID_CHR"] != System.DBNull.Value)
        //        objSampleVO.m_strPatientCardID = objRow["PATIENTCARDID_CHR"].ToString().Trim();

        //    if(objRow["barcode_vchr"] != System.DBNull.Value)
        //        objSampleVO.m_strBarCode = objRow["barcode_vchr"].ToString().Trim();

        //    if(objRow["patientid_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strPatientID = objRow["patientid_chr"].ToString().Trim();

        //    if(objRow["sampling_date_dat"] != System.DBNull.Value)
        //        objSampleVO.m_strSampling_Dat = objRow["sampling_date_dat"].ToString().Trim();

        //    if(objRow["operator_id_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strOperator_ID = objRow["operator_id_chr"].ToString().Trim();

        //    if(objRow["appl_empid_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strAppl_EmpID = objRow["applyemployee"].ToString().Trim();

        //    if(objRow["appl_deptid_chr"] != System.DBNull.Value)
        //        objSampleVO.m_strAppl_DeptID = objRow["deptname"].ToString().Trim();

        //    if(objRow["status_int"] !=System.DBNull.Value)
        //        objSampleVO.m_strStatus=objRow["status_int"].ToString().Trim();

        //    if(objRow["SAMPLE_TYPE_ID_CHR"]!=System.DBNull.Value)
        //        objSampleVO.m_strSampleType=objRow["SAMPLE_TYPE_ID_CHR"].ToString().Trim();

        //    if(objRow["QCSAMPLEID_CHR"] != System.DBNull.Value)
        //    {
        //        objSampleVO.m_strQCSampleID = objRow["QCSAMPLEID_CHR"].ToString().Trim();
        //    }

        //    if(objRow["SAMPLEKIND_CHR"] != System.DBNull.Value)
        //    {
        //        objSampleVO.m_strSampleKind = objRow["SAMPLEKIND_CHR"].ToString().Trim();
        //    }

        //    if(objRow["CHECK_DATE_DAT"] != System.DBNull.Value)
        //    {
        //        objSampleVO.m_strCheckDat = objRow["CHECK_DATE_DAT"].ToString().Trim();
        //    }
        //}

        //#endregion

        #region ���t_opr_lis_result��t_opr_lis_result_log 1
        [AutoComplete]
        public long m_lngAddNullResultAndReslutLog( string strDeviceID, string strDeviceSampleID, string strCheckDat)
        {
            long lngRes = 0;
            string strDeviceCheckItemSQL = @"SELECT t1.device_check_item_name_vchr, t1.device_model_id_chr,
													t1.device_check_item_id_chr
											   FROM t_bse_lis_device_check_item t1, t_bse_lis_device t2
											  WHERE t1.device_model_id_chr = t2.device_model_id_chr
												AND t2.deviceid_chr = '" + strDeviceID + "'";
            DataTable dtbDeviceCheckItemName = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strDeviceCheckItemSQL, ref dtbDeviceCheckItemName);
                objHRPSvc.Dispose();
                int intBeginIdx = objHRPSvc.intGetNewNumericID("IDX_INT", "T_OPR_LIS_RESULT");
                int intTempIdx = intBeginIdx;
                clsDeviceReslutVO objDeviceResultVO = new clsDeviceReslutVO();
                for (int i = 0; i < dtbDeviceCheckItemName.Rows.Count; i++)
                {
                    objDeviceResultVO.m_strCheckDat = strCheckDat;
                    objDeviceResultVO.m_strDeviceCheckItemName = dtbDeviceCheckItemName.Rows[i]["DEVICE_CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                    objDeviceResultVO.m_strDeviceID = strDeviceID;
                    objDeviceResultVO.m_strDeviceSampleID = strDeviceSampleID;
                    objDeviceResultVO.m_strIdx = intTempIdx.ToString().Trim();
                    objDeviceResultVO.m_strPstatus = "1";
                    lngRes = this.m_lngAddDeviceResult( objDeviceResultVO);
                    intTempIdx++;
                }
                clsResultLogVO objResultLogVO = new clsResultLogVO();
                objResultLogVO.m_strBeginIndex = intBeginIdx.ToString().Trim();
                objResultLogVO.m_strCheckDat = strCheckDat;
                objResultLogVO.m_strDeviceID = strDeviceID;
                objResultLogVO.m_strDeviceSampleID = strDeviceSampleID;
                intTempIdx--;
                objResultLogVO.m_strEndIndex = intTempIdx.ToString().Trim();
                lngRes = this.m_lngAddResultLog( objResultLogVO);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ���t_opr_lis_result��һ����¼
        [AutoComplete]
        public long m_lngAddDeviceResult( clsDeviceReslutVO objDeviceResultVO)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_lis_result
										  (idx_int, result_vchr, device_check_item_name_vchr, unit_vchr,
										   device_sampleid_chr, refrange_vchr, min_val_dec, max_val_dec,
										   abnormal_flag_vchr, deviceid_chr, check_dat, pstatus_int
										  )
							  VALUES (?, ?, ?, ?,
									  ?, ?, ?, ?,
									  ?, ?, ?, ?
									 )";
            try
            {
                System.Data.IDataParameter[] objDeviceResultArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(12, out objDeviceResultArr);
                objDeviceResultArr[0].Value = objDeviceResultVO.m_strIdx;
                objDeviceResultArr[1].Value = objDeviceResultVO.m_strResult;
                objDeviceResultArr[2].Value = objDeviceResultVO.m_strDeviceCheckItemName;
                objDeviceResultArr[3].Value = objDeviceResultVO.m_strUnit;
                objDeviceResultArr[4].Value = objDeviceResultVO.m_strDeviceSampleID;
                objDeviceResultArr[5].Value = objDeviceResultVO.m_strRefRange;
                objDeviceResultArr[6].Value = objDeviceResultVO.m_strMinVal;
                objDeviceResultArr[7].Value = objDeviceResultVO.m_strMaxVal;
                objDeviceResultArr[8].Value = objDeviceResultVO.m_strAbnormalFlag;
                objDeviceResultArr[9].Value = objDeviceResultVO.m_strDeviceID;
                objDeviceResultArr[10].Value = System.DateTime.Parse(objDeviceResultVO.m_strCheckDat);
                objDeviceResultArr[11].Value = objDeviceResultVO.m_strPstatus;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDeviceResultArr);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ���t_opr_lis_result_log����һ����¼
        [AutoComplete]
        public long m_lngAddResultLog( clsResultLogVO objResultLogVO)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO t_opr_lis_result_log
										  (begin_idx_int, check_dat, device_sampleid_chr, end_idx_int,
										   deviceid_chr
										  )
								   VALUES (?, ?, ?, ?, ?)";
            try
            {
                if (Convert.ToDecimal(objResultLogVO.m_strBeginIndex) > Convert.ToDecimal(objResultLogVO.m_strEndIndex))
                {
                    string tmpIdx = objResultLogVO.m_strEndIndex;
                    objResultLogVO.m_strEndIndex = objResultLogVO.m_strBeginIndex;
                    objResultLogVO.m_strBeginIndex = tmpIdx;
                }

                System.Data.IDataParameter[] objResultLogArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(5, out objResultLogArr);
                objResultLogArr[0].Value = objResultLogVO.m_strBeginIndex;
                objResultLogArr[1].Value = System.DateTime.Parse(objResultLogVO.m_strCheckDat);
                objResultLogArr[2].Value = objResultLogVO.m_strDeviceSampleID;
                objResultLogArr[3].Value = objResultLogVO.m_strEndIndex;
                objResultLogArr[4].Value = objResultLogVO.m_strDeviceID;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objResultLogArr);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        //        #region �������뵥�ͼ�������ѯ������ 
        //        [AutoComplete]
        //        public long m_lngGetCheckResultByApplFormNoAndCheckGroupID( string strApplFormNo,string strGroupID,out DataTable dtbCheckResult)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT t1.modify_dat, t1.groupid_chr, t1.check_item_id_chr, t1.sample_id_chr,											t1.result_vchr,t1.unit_vchr, t1.deviceid_chr,																	t1.device_check_item_name_vchr, t1.refrange_vchr AS ss,
        //									t1.check_item_name_vchr, t1.check_item_english_name_vchr, t1.min_val_dec,
        //									t1.max_val_dec, t1.abnormal_flag_chr, t1.check_dat, t1.clinicapp_vchr, 
        //									t1.memo_vchr,t1.confirm_dat, t1.pointliststr_vchr, t1.summary_vchr, 
        //									t1.graph_img, t1.status_int,t1.checker1_chr, t1.checker2_chr, 
        //									t1.confirm_person_chr, t1.operator_id_chr,t1.check_deptid_chr, 
        //									t2.lastname_vchr AS confirmperson,t3.lastname_vchr AS checkperson,
        //									t1.refrange_vchr
        //								FROM t_opr_lis_check_result t1,t_bse_employee t2,t_bse_employee t3,
        //									 t_bse_lis_check_item t4
        //								WHERE t1.status_int = 1
        //								AND t1.check_item_id_chr = t4.check_item_id_chr
        //								AND (t1.confirm_person_chr = t2.empid_chr(+))
        //								AND (t1.checker1_chr = t3.empid_chr(+))
        //								AND sample_id_chr IN (
        //														SELECT sample_id_chr
        //															FROM t_opr_lis_applgrpsmp
        //														WHERE groupid_chr = '"+strGroupID
        //                +"' AND application_form_no_chr = '"+strApplFormNo+"')";
        //            dtbCheckResult = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckResult); 
        //                objHRPSvc.Dispose(); 
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ������Ʒ�Ų�ѯ��t_opr_lis_check_result�Ľ����Ϣ 
        //        [AutoComplete]
        //        public long m_lngGetManualCheckResultBySampleID(string strSampleID,out DataTable dtbManualCheckResult)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT modify_dat, 
        //									groupid_chr, 
        //									check_item_id_chr, 
        //									sample_id_chr, 
        //									result_vchr, 
        //									unit_vchr as UNIT_CHR, 
        //									deviceid_chr, 
        //									device_check_item_name_vchr, 
        //									refrange_vchr as REF_VALUE_RANGE_VCHR, 
        //									check_item_name_vchr, 
        //									check_item_english_name_vchr, 
        //									min_val_dec, 
        //									max_val_dec, 
        //									abnormal_flag_chr, 
        //									check_dat, 
        //									clinicapp_vchr, 
        //									memo_vchr, 
        //									confirm_dat, 
        //									pointliststr_vchr, 
        //									Summary_Vchr, 
        //									graph_img, 
        //									status_int, 
        //									checker1_chr, 
        //									checker2_chr, 
        //									confirm_person_chr, 
        //									operator_id_chr, 
        //									check_deptid_chr
        //								FROM t_opr_lis_check_result
        //								WHERE sample_id_chr = '"+strSampleID
        //                +"' AND status_int = 1";
        //            dtbManualCheckResult = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbManualCheckResult); 
        //                objHRPSvc.Dispose();                  
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ������ƷID�ź�check_item_id���±�t_opr_lis_check_result�еļ�¼��Ϣ
        [AutoComplete]
        public long m_lngSetCheckResultBySampleIDAndCheck_item_id( clsCheckResult_VO p_objCheckResultVO)
        {
            long lngRes = 0;
            string strCheckResult = p_objCheckResultVO.m_strResult;
            string strSampleID = p_objCheckResultVO.m_strSample_ID;
            string strCheckItemID = p_objCheckResultVO.m_strCheck_Item_ID;
            string strSQL = @"UPDATE t_opr_lis_check_result SET result_vchr = '" + strCheckResult + "' WHERE sample_id_chr = '" + strSampleID + "' AND CHECK_ITEM_ID_CHR = '" + strCheckItemID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        //        #region �����������ڲ�ѯ�ֹ�¼��ļ�����Ŀ�ı걾����
        //        [AutoComplete]
        //        public long m_lngGetSampleListByApplDat(DateTime p_dtBegin,DateTime p_dtEnd,string flag,out System.Data.DataTable p_dtbSampleList)
        //        {
        //            long lngRes = 0;
        //            p_dtbSampleList=null;
        //            string strfilter = null;
        //            if(flag == "1")
        //            {
        //                strfilter = "NOT IN";
        //            }
        //            else
        //            {
        //                strfilter = "IN";
        //            }
        //            string strSQL = @"SELECT t1.sample_id_chr,t2.groupid_chr,t3.groupname_vchr,t1.appl_dat,t2.stepflag_chr 
        //								FROM t_opr_lis_sample t1, t_opr_lis_req_check t2, t_aid_lis_check_group t3
        //								WHERE t1.sample_id_chr = t2.sample_id_chr
        //								AND t2.groupid_chr = t3.groupid_chr
        //								AND appl_dat BETWEEN TO_DATE ('"+p_dtBegin.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss') AND TO_DATE ('"+p_dtEnd.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss')
        //								AND  t1.samplekind_chr = '2'
        //								AND t1.sample_id_chr "+strfilter+" (SELECT sample_id_chr FROM t_opr_lis_check_result)";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbSampleList); 
        //                objHRPSvc.Dispose();  

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����SampleID��GroupID��ѯ�ֹ�¼��ľ��������Ŀ 
        //        [AutoComplete]
        //        public long m_lngGetCheckItemBySampleIDAndGroupID(string strSampleID,string strGroupID,out System.Data.DataTable dtbManualCheckItemList)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT t2.check_item_name_vchr, t1.check_item_id_chr, t2.unit_chr,t2.ref_value_range_vchr,  t2.unit_chr, t2.check_item_english_name_vchr 
        //								FROM t_opr_lis_req_check_detail t1, t_bse_lis_check_item t2
        //								WHERE t1.check_item_id_chr = t2.check_item_id_chr
        //								AND t1.sample_id_chr = '"+strSampleID+
        //                "'AND t1.groupid_chr = '"+strGroupID+"'";
        //            dtbManualCheckItemList = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbManualCheckItemList); 
        //                objHRPSvc.Dispose();  

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ���±�t_opr_lis_check_result������¼
        [AutoComplete]
        public long m_lngSetCheckResultList( clsCheckResult_VO[] p_objCheckResultList)
        {
            ArrayList sampleIdList = new ArrayList();
            long lngRes = 0;
            try
            {
                for (int i = 0; i < p_objCheckResultList.Length; i++)
                {
                    if (!sampleIdList.Contains(p_objCheckResultList[i].m_strSample_ID))
                        sampleIdList.Add(p_objCheckResultList[i].m_strSample_ID);
                    m_lngSetCheckResultBySampleIDAndCheck_item_id( p_objCheckResultList[i]);
                }
                lngRes = 1;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        #region ���t_opr_lis_device_relation����һ���¼�¼
        [AutoComplete]
        public long m_lngAddnewDeviceRelation( clsDeviceRelation_VO p_objDeviceRelation_VO)
        {
            long lngRes = 0;
            string strNowDat = System.DateTime.Now.ToShortDateString().Trim();
            string strFromDat = strNowDat + " 00:00:00";
            string strToDat = strNowDat + " 23:59:59";
            string strDeviceID = p_objDeviceRelation_VO.m_strDeviceID;
            string strSQL = @"SELECT MAX (t1.seq_id_chr)
								FROM t_opr_lis_device_relation t1
							   WHERE t1.RECEPTION_DAT BETWEEN TO_DATE ('" + strFromDat + @"','yyyy-mm-dd hh24:mi:ss')
													  AND TO_DATE ('" + strToDat + @"','yyyy-mm-dd hh24:mi:ss')
								 AND t1.deviceid_chr = '" + strDeviceID + "'";
            DataTable dtbSeqID = null;
            string strSeqNo = null;
            try
            {
                System.Data.IDataParameter[] objDeviceRelationArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(9, out objDeviceRelationArr);

                p_objDeviceRelation_VO.m_strSeq_ID_Device = objHRPSvc.m_strGetNewID("t_opr_lis_device_relation", "SEQ_ID_DEVICE_CHR", 10);
                //��������ID�����ڻ�ȡSeqNo
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSeqID);
                if (lngRes > 0)
                {
                    if (dtbSeqID.Rows.Count > 0 && dtbSeqID.Rows[0][0].ToString() != "")
                    {
                        int MaxID = int.Parse(dtbSeqID.Rows[0][0].ToString().Trim()) + 1;
                        strSeqNo = MaxID.ToString("0000000000");
                    }
                    else
                    {
                        strSeqNo = "0000000001";
                    }

                    p_objDeviceRelation_VO.m_strSeq_ID = strSeqNo;
                    p_objDeviceRelation_VO.m_strReceptionDat = System.DateTime.Now.ToString().Trim();

                    objDeviceRelationArr[0].Value = p_objDeviceRelation_VO.m_strSeq_ID;
                    objDeviceRelationArr[1].Value = p_objDeviceRelation_VO.m_strDeviceID;
                    objDeviceRelationArr[2].Value = p_objDeviceRelation_VO.m_strDevice_SampleID;
                    if (Microsoft.VisualBasic.Information.IsDate(p_objDeviceRelation_VO.m_strCheck_dat))
                    { objDeviceRelationArr[3].Value = System.DateTime.Parse(p_objDeviceRelation_VO.m_strCheck_dat); }
                    objDeviceRelationArr[4].Value = p_objDeviceRelation_VO.m_strSample_ID;
                    objDeviceRelationArr[5].Value = p_objDeviceRelation_VO.m_strPositionID;
                    objDeviceRelationArr[6].Value = p_objDeviceRelation_VO.m_intPStatus;
                    objDeviceRelationArr[7].Value = System.DateTime.Parse(p_objDeviceRelation_VO.m_strReceptionDat);
                    objDeviceRelationArr[8].Value = p_objDeviceRelation_VO.m_strSeq_ID_Device;

                    long lngRecEff = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddNewDeviceRelation_SQL, ref lngRecEff, objDeviceRelationArr);
                }

                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        //        #region ���������ţ������ͺţ���һ̨���������Ĵ��ţ��ͼ������ڷ�Χ����ѯ����������(�������ڸö�ʱ����������ģ��걾���� 
        //        //�����ؽ��������������걾�ţ���ƷBarcode
        //        //�ͼ������ڡ�
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="strDeviceId">�豸ID</param>
        //        /// <param name="dtBegin">��ѯ��ʼʱ��</param>
        //        /// <param name="dtEnd">��ѯ����ʱ��</param>
        //        /// <param name="dtbDeviceSampleList"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleList(string p_strDeviceID, string strBeginDate, string strEndDate, out System.Data.DataTable p_dtbDeviceSampleList)
        //        {
        //            long lngRes=0;
        //            string strSQL=null;
        //            p_dtbDeviceSampleList=null;
        //            strSQL=@"select d.*, e.barcode_vchr from (select b.*, c.PositionID_Chr, c.sample_id_chr, c.STATUS_INT FROM
        //					(select distinct a.deviceid_chr,
        //					a.device_sampleid_chr,
        //					a.check_dat
        //					from t_opr_lis_result_log a where a.deviceid_chr = '" + p_strDeviceID + "' and " + 
        //                @"a.check_dat > to_date('" + strBeginDate + "','yyyy-mm-dd hh24:mi:ss') " + 
        //                @"and a.check_dat < to_date('" + strEndDate + "','yyyy-mm-dd hh24:mi:ss')) b left join " +
        //                @"(select f.* from t_opr_lis_device_relation f where f.deviceid_chr = '" + p_strDeviceID + "' " + 
        //                @"and f.check_dat > to_date('" + strBeginDate + "','yyyy-mm-dd hh24:mi:ss') " + 
        //                @"and f.check_dat < to_date('" + strEndDate + "','yyyy-mm-dd hh24:mi:ss')) c " +
        //                @"on b.deviceid_chr = c.DEVICEID_CHR and b.CHECK_DAT = c.CHECK_DAT 
        //					and b.DEVICE_SAMPLEID_CHR = c.DEVICE_SAMPLEID_CHR) d left join t_opr_lis_sample e
        //					on d.sample_id_chr = e.sample_id_chr ";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbDeviceSampleList); 
        //                objHRPSvc.Dispose();  

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���������ţ������ͺţ���һ̨���������Ĵ��ţ��ͼ������ڷ�Χ����ѯ����������(�������ڸö�ʱ����������ģ��걾���� 
        //        //�����ؽ��������������걾�ţ��������Ŀ�����������Ŀ��
        //        //�ͼ������ڡ�
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="strDeviceId">�豸ID</param>
        //        /// <param name="dtBegin">��ѯ��ʼʱ��</param>
        //        /// <param name="dtEnd">��ѯ����ʱ��</param>
        //        /// <param name="intDeviceSampleObj">0-ȫ���걾;1-δ��˱걾</param>
        //        /// <param name="dtbDeviceSampleList"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleList(string p_strDeviceID, DateTime p_dtBegin, DateTime p_dtEnd, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSampleList)
        //        {
        //            long lngRes=0;
        //            string strSQL=null;
        //            p_dtbDeviceSampleList=null;
        //            strSQL=@"SELECT t2.deviceid_chr, t2.sample_id_chr, t2.device_sampleid_chr,t2.check_dat, t1.groupid_chr, t1.stepflag_chr,t3.groupname_vchr,t4.barcode_vchr, t4.application_form_no_chr,t4.status_int,t4.application_form_no_chr as application_id_chr
        //                         FROM t_opr_lis_req_check t1,t_opr_lis_device_relation t2, t_aid_lis_check_group t3, t_opr_lis_sample t4 
        //                         WHERE (t2.check_dat BETWEEN TO_DATE ('"+p_dtBegin.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss') AND TO_DATE ('"+p_dtEnd.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss')) 
        //                         AND t2.deviceid_chr = '"+p_strDeviceID+@"' AND t1.sample_id_chr = t2.sample_id_chr AND t4.sample_id_chr = t1.sample_id_chr 
        //                         AND t3.groupid_chr = t1.groupid_chr AND t4.status_int"+((p_intDeviceSampleObj==1)?">= 1 AND t4.status_int <= 5":">=1 AND t4.status_int <= 6");
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbDeviceSampleList); 
        //                objHRPSvc.Dispose();  

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���������ţ������ͺţ���һ̨���������Ĵ��ţ��ͼ������ڷ�Χ���ټ���һ��������Ŀ�������飩�ţ���ѯ�������������������ڸö�ʱ����������ĸü�����Ŀ�ģ��걾���ϡ�
        //        //���ؽ��������������걾�ţ��������Ŀ�����������Ŀ���ͼ������ڡ�
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDeviceID"></param>
        //        /// <param name="p_dtBegin"></param>
        //        /// <param name="p_dtEnd"></param>
        //        /// <param name="strCheckGroupID">���ID�����������飩</param>
        //        /// <param name="p_intDeviceSampleObj">0-ȫ���걾;1-δ��˱걾</param>
        //        /// <param name="p_dtbDeviceSample"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleList(string p_strDeviceID, DateTime p_dtBegin, DateTime p_dtEnd, string p_strCheckGroupID, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSample)
        //        {
        //            long lngRes=0;
        //            p_dtbDeviceSample=null;
        //            string strSQL=null;

        //            strSQL=@"SELECT t2.deviceid_chr, t2.sample_id_chr, t2.device_sampleid_chr,t2.check_dat, t1.groupid_chr, t1.stepflag_chr, t3.groupname_vchr
        //                         FROM t_opr_lis_req_check t1,t_opr_lis_device_relation t2, t_aid_lis_check_group t3
        //                         WHERE (t2.check_dat BETWEEN TO_DATE ('"+p_dtBegin.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss') AND TO_DATE ('"+p_dtEnd.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss'))
        //                         AND t2.deviceid_chr = '"+p_strDeviceID+@"' AND t1.groupid_chr = '"+p_strCheckGroupID+@"' AND t1.sample_id_chr = t2.sample_id_chr
        //                         AND t3.groupid_chr = t1.groupid_chr AND t1.stepflag_chr"+((p_intDeviceSampleObj==1)?"='1'":">='1'");

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbDeviceSample); 
        //                objHRPSvc.Dispose();  
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���������ţ������ͺţ���һ̨���������Ĵ��ţ��ͼ������ڷ�Χ���ټ��������걾�ŷ�Χ����ѯ�������������������ڸö�ʱ����������ı걾��Χ�ڵģ��걾���ϡ�
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleList(string p_strDeviceId, DateTime p_dtBegin, DateTime p_dtEnd, string p_strDeviceSampleIDBegin, string p_strDeviceSampleIDEnd, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSampleList)
        //        {
        //            long lngRes=0;
        //            p_dtbDeviceSampleList=null;
        //            string strSQL=null;
        //            strSQL=@"SELECT t2.deviceid_chr, t2.sample_id_chr, t2.device_sampleid_chr,t2.check_dat, t1.groupid_chr, t1.stepflag_chr, t3.groupname_vchr
        //                         FROM t_opr_lis_req_check t1,t_opr_lis_device_relation t2, t_aid_lis_check_group t3
        //                         WHERE (t2.check_dat BETWEEN TO_DATE ('"+p_dtBegin.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss') AND TO_DATE ('"+p_dtEnd.ToString()+@"', 'YYYY-MM-DD hh24:mi:ss'))
        //                         AND t2.device_sampleid_chr BETWEEN '"+p_strDeviceSampleIDBegin+@"' AND '"+p_strDeviceSampleIDEnd+
        //                @"' AND t2.deviceid_chr = '"+p_strDeviceId+@"' AND t1.sample_id_chr = t2.sample_id_chr
        //                         AND t3.groupid_chr = t1.groupid_chr AND t1.stepflag_chr"+((p_intDeviceSampleObj==1)?"='1'":">='1'");
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbDeviceSampleList); 
        //                objHRPSvc.Dispose();  
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;

        //        }
        //        #endregion	

        //        #region  ���������걾���б���һ�����ϣ�����DataTable�У�, ��ѯ���ñ걾�����м�����Ŀ�Ľ����Ĵ��š����ơ�ֵ����ϸ���ݡ�
        //        /// <summary>
        //        /// ע�⣬�����Ǵ�t_opr_lis_result���в�ѯ�����õ��Ľ��Ϊ��������Ľ��
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_dtbDeviceSampleList">�˽ṹ�����淽����DataTable�ṹ��ͬ</param>
        //        /// <param name="p_dtbDeviceResultList"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetDeviceResultList(System.Data.DataTable p_dtbDeviceSampleList, out System.Data.DataTable p_dtbDeviceResultList)
        //        {
        //            long lngRes=0;
        //            p_dtbDeviceResultList=null;
        //            string strSQL=null;
        //            string strDeviceSampleID;
        //            string strDeviceId;
        //            DateTime dtCheckDate;
        //            DataRow drDeviceResult;

        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();		
        //            try
        //            {   
        //                for(int i=0;i<p_dtbDeviceSampleList.Rows.Count;i++)
        //                {
        //                    if(p_dtbDeviceSampleList.Rows[i]["DEVICE_SAMPLEID_CHR"]!=System.DBNull.Value)
        //                    {
        //                        strDeviceSampleID=p_dtbDeviceSampleList.Rows[i]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        dtCheckDate = (DateTime)(p_dtbDeviceSampleList.Rows[i]["CHECK_DAT"]);
        //                        strDeviceId = p_dtbDeviceSampleList.Rows[i]["DEVICEID_CHR"].ToString().Trim();

        //                        strSQL=@"select t1.IDX_INT,	t1.result_vchr,t1.device_check_item_name_vchr,
        //								t1.unit_vchr,t1.device_sampleid_chr,t1.refrange_vchr,t1.min_val_dec,t1.max_val_dec,
        //								t1.abnormal_flag_vchr,t1.deviceid_chr,t1.check_dat,t1.PStatus_int,t2.sample_id_chr
        //								from t_opr_lis_result t1,t_opr_lis_device_relation t2 
        //                                where t1.device_sampleid_chr='"+strDeviceSampleID
        //                            +@"' and t1.deviceid_chr='"+strDeviceId+@"' and 
        //                                t1.check_dat = to_date('"+dtCheckDate.ToString()+@"','yyyy-mm-dd hh24:mi:ss')
        //								and t1.IDX_INT >= (select max(begin_idx_int) from t_opr_lis_result_log t3 where
        //                                t3.device_sampleid_chr='"+strDeviceSampleID+@"' and t3.deviceid_chr='"+strDeviceId+@"' and 
        //                                t3.check_dat = to_date('"+dtCheckDate.ToString()+@"','yyyy-mm-dd hh24:mi:ss'))
        //                                and t1.device_sampleid_chr=t2.device_sampleid_chr 
        //                                and t1.deviceid_chr=t2.deviceid_chr
        //							    and t1.check_dat=t2.check_dat";		                

        //                        System.Data.DataTable objDT_ResultListTmp= null;
        //                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref objDT_ResultListTmp);
        //                        objHRPSvc.Dispose();
        //                        if(lngRes>0)
        //                        {
        //                            if (p_dtbDeviceResultList==null)
        //                            {
        //                                p_dtbDeviceResultList = objDT_ResultListTmp.Clone();
        //                            }
        //                            for (int j=0;j<objDT_ResultListTmp.Rows.Count;j++)
        //                            {
        //                                drDeviceResult = p_dtbDeviceResultList.NewRow();
        //                                drDeviceResult.ItemArray = objDT_ResultListTmp.Rows[j].ItemArray;
        //                                p_dtbDeviceResultList.Rows.Add(drDeviceResult);
        //                            }
        //                        }
        //                    }
        //                }	
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            finally
        //            {
        //                objHRPSvc.Dispose();
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���������걾���б���һ�����ϣ�����DataTable�У�����ѯ���ñ걾����Ҫ���еļ�����Ŀ�Ľ����Ĵ��š����Ƶ���ϸ���ݡ� 
        //        /// <summary>
        //        /// ע�⣬����Ҫ���������걾�Ų�ѯ����Ӧ�ı걾�ţ�ϵͳ�ڲ������ģ������ݱ걾�ţ���t_opr_lis_req_check��t_opr_lis_req_check_detail�в�ѯ���������õ��Ľ���뷽��11���õ��Ľ�����ƣ���û�н��ֵ�����⣬���ܶ���һЩ������Ľ����¼��
        //        //Long getResultList(DataTable dtbDeviceSample, out DataTable dtbResultList)
        //        //˵�������dtbDeviceSample�Ľṹ�뷽��8~11һ�������У������˼������ڡ����⣬dtbResultList��Ҫ�н��ֵ�ֶΣ���Ȼ���ֶ���ʱΪ�ա���ο�t_opr_lis_check_result��ṹȷ���˷��ؽ���Ľṹ��
        //        //2004.2.26֣�����޸�
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_dtbDeviceSample"></param>
        //        /// <param name="p_dtbResultList"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetResultList(System.Data.DataTable p_dtbDeviceSample,out System.Data.DataTable p_dtbResultList)
        //        {
        //            long lngRes=0;
        //            p_dtbResultList=null;
        //            string strSQL=null;
        //            string strDeviceSampleID;
        //            DateTime dtCheckDate;
        //            string strDeviceId;
        //            DataRow drResultList;
        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //            try
        //            {
        //                for(int i=0;i<p_dtbDeviceSample.Rows.Count;i++)
        //                {
        //                    if(p_dtbDeviceSample.Rows[i]["device_sampleid_chr"]!=System.DBNull.Value)
        //                    {
        //                        strDeviceSampleID=p_dtbDeviceSample.Rows[i]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        dtCheckDate = (DateTime)(p_dtbDeviceSample.Rows[i]["CHECK_DAT"]);
        //                        strDeviceId = p_dtbDeviceSample.Rows[i]["DEVICEID_CHR"].ToString().Trim();
        //                        //following sql clause modified by chen kun.
        //                        strSQL=@"SELECT SYSDATE modify_dat, tt1.groupid_chr, tt1.check_item_id_chr,tt1.sample_id_chr,
        //                              NULL raw_result_vchr, NULL result_vchr, NULL unit_vchr,
        //                              tt1.deviceid_chr, NULL refrange_vchr, tt1.check_item_name_vchr,
        //                              tt1.check_item_english_name_vchr, NULL min_val_dec, NULL max_val_dec,
        //                              NULL abnormal_flag_chr, NULL check_dat,
        //                              tt1.clinic_meaning_vchr clinicapp_vchr, NULL memo_vchr,
        //                              NULL pointliststr_vchr, NULL summary_vchr, NULL graph_img,
        //                              (CASE
        //                                 WHEN LENGTH (tt1.formula_vchr) > 0
        //                                 THEN 1
        //                                 ELSE 0
        //                              END) iscalitem, tt1.formula_vchr, tt2.device_check_item_id_chr,
        //                              tt2.device_check_item_name_vchr, tt2.has_graph_result_int,NULL idx_int,NULL isdirty,NULL saved,
        //							  tt1.resulttype_chr
        //                              FROM (SELECT t1.deviceid_chr, t1.check_dat, t1.device_sampleid_chr,
        //                              t1.sample_id_chr, t2.check_item_id_chr, t2.groupid_chr,
        //                              t3.check_item_name_vchr, t3.check_item_english_name_vchr,
        //                              t3.formula_vchr, t3.clinic_meaning_vchr,t3.resulttype_chr
        //                              FROM t_opr_lis_device_relation t1,
        //                                   t_opr_lis_req_check_detail t2,
        //                                   t_bse_lis_check_item t3
        //                              WHERE t1.sample_id_chr = t2.sample_id_chr
        //                               AND t2.check_item_id_chr = t3.check_item_id_chr
        //                               AND t1.deviceid_chr = '"+strDeviceId+@"'
        //                               AND t1.device_sampleid_chr = '"+strDeviceSampleID+@"'
        //                               AND t1.check_dat = to_date('"+dtCheckDate.ToString()+@"','yyyy-mm-dd hh24:mi:ss')) tt1,
        //                            (SELECT t1.deviceid_chr, t1.device_model_id_chr, t2.check_item_id_chr,
        //                             t2.device_check_item_id_chr, t3.device_check_item_name_vchr,
        //                             t3.has_graph_result_int
        //                             FROM t_bse_lis_device t1,
        //                              t_bse_lis_check_item_dev_item t2,
        //                              t_bse_lis_device_check_item t3
        //                             WHERE t1.device_model_id_chr = t2.device_model_id_chr
        //                                  AND t2.device_check_item_id_chr = t3.device_check_item_id_chr
        //                                  AND t2.device_model_id_chr = t3.device_model_id_chr
        //                                  AND t1.deviceid_chr = '"+strDeviceId+@"') tt2
        //                             WHERE tt1.check_item_id_chr = tt2.check_item_id_chr";

        //                        System.Data.DataTable objDT_ResultListTmp = null;
        //                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref objDT_ResultListTmp);
        //                        objHRPSvc.Dispose();
        //                        if(lngRes>0)
        //                        {
        //                            if (p_dtbResultList==null)
        //                            {
        //                                p_dtbResultList = objDT_ResultListTmp.Clone();
        //                            }
        //                            for (int j=0;j<objDT_ResultListTmp.Rows.Count;j++)
        //                            {
        //                                drResultList = p_dtbResultList.NewRow();
        //                                //drResultList = new DataRow();
        //                                drResultList.ItemArray = objDT_ResultListTmp.Rows[j].ItemArray;
        //                                p_dtbResultList.Rows.Add(drResultList);
        //                            }
        //                        }  
        //                    }
        //                }                
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            finally
        //            {
        //                objHRPSvc.Dispose();
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���������걾���б���һ�����ϣ�����DataTable�У�����ѯ���ñ걾�ļ�����Ŀ���Ѿ��еĽ����ϸ���ݡ�
        //        [AutoComplete]
        //        public long m_lngGetCurrentResultList(System.Data.DataTable p_dtbDeviceSampleList,out System.Data.DataTable p_dtbCurrentResults)
        //        {
        //            long lngRes=0;
        //            p_dtbCurrentResults=null;
        //            string strSampleID;
        //            string strGroupId;
        //            DataRow drCurrentResult;
        //            System.Data.DataTable objDT_ResultListTmp= null;
        //            try
        //            {   
        //                for(int i=0;i<p_dtbDeviceSampleList.Rows.Count;i++)
        //                {
        //                    if((p_dtbDeviceSampleList.Rows[i]["SAMPLE_ID_CHR"]!=System.DBNull.Value)&&(p_dtbDeviceSampleList.Rows[i]["GROUPID_CHR"]!=System.DBNull.Value))
        //                    {
        //                        strSampleID=p_dtbDeviceSampleList.Rows[i]["SAMPLE_ID_CHR"].ToString().Trim();
        //                        strGroupId = p_dtbDeviceSampleList.Rows[i]["GROUPID_CHR"].ToString().Trim();
        //                        lngRes = this.m_lngGetCheckResult(strSampleID,strGroupId,out objDT_ResultListTmp);

        //                        if(lngRes>0)
        //                        {
        //                            if (p_dtbCurrentResults==null)
        //                            {
        //                                p_dtbCurrentResults = objDT_ResultListTmp.Clone();
        //                            }
        //                            for (int j=0;j<objDT_ResultListTmp.Rows.Count;j++)
        //                            {
        //                                drCurrentResult = p_dtbCurrentResults.NewRow();
        //                                drCurrentResult.ItemArray = objDT_ResultListTmp.Rows[j].ItemArray;
        //                                p_dtbCurrentResults.Rows.Add(drCurrentResult);
        //                            }
        //                        }
        //                    }
        //                }	
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ����SampleID���ñ�t_opr_lis_req_check�ֶ�stepflag_chr��״̬
        [AutoComplete]
        public long m_lngSetReqCheckStepFlag( string p_strSampleID, int p_intStepFlag)
        {
            long lngRes = 0;
            string strSQL = @" UPDATE t_opr_lis_req_check SET stepflag_chr = '" + p_intStepFlag.ToString() + @"'WHERE sample_id_chr = '" + p_strSampleID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        //        #region ���������š�������Ŀ���������飩����ѯ��Ӧ�ļ������������t_opr_lis_check_result��ѯ�� 
        //        [AutoComplete]
        //        public long m_lngGetCheckResult(string p_strSampleID, string p_strCheckGroupID, out System.Data.DataTable p_dtbCheckResult)
        //        {
        //            long lngRes=0;
        //            p_dtbCheckResult=null;
        //            string strSQL=@"SELECT t1.modify_dat, t1.groupid_chr, t1.check_item_id_chr, t1.sample_id_chr, t1.result_vchr,
        //							t1.unit_vchr, t1.deviceid_chr, t1.device_check_item_name_vchr, t1.refrange_vchr,
        //							t1.check_item_name_vchr, t1.check_item_english_name_vchr, t1.min_val_dec,
        //							t1.max_val_dec, t1.abnormal_flag_chr, t1.check_dat, t1.clinicapp_vchr, t1.memo_vchr,
        //							t1.confirm_dat, t1.pointliststr_vchr, t1.summary_vchr, t1.graph_img, t1.status_int,
        //							t1.checker1_chr, t1.checker2_chr, t1.confirm_person_chr, t1.operator_id_chr,
        //							t1.check_deptid_chr,t2.lastname_vchr as confirmPerson,t3.lastname_vchr as checkPerson
        //							FROM t_opr_lis_check_result t1,t_bse_employee t2,t_bse_employee t3
        //							WHERE groupid_chr = '"+p_strCheckGroupID+@"'
        //							AND (t1.confirm_person_chr = t2.empid_chr(+))
        //							AND (t1.checker1_chr = t3.empid_chr(+))
        //							AND sample_id_chr = '"+p_strSampleID+"'and t1.status_int=1" ;

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc=new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
        //                lngRes=objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbCheckResult);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion 

        //        #region ��ѯ�����걾�ļ����� 
        //        [AutoComplete]
        //        public long m_lngGetUnionCheckResult(string p_strSmapleIDFirst,string p_strGroupIDFirst,string p_strSampleIDSecond,string p_strGroupIDSecond,out System.Data.DataTable p_dtbResult)
        //        {
        //            long lngRes=0;
        //            p_dtbResult=null;
        //            string strSQL=@"SELECT modify_dat, groupid_chr, check_item_id_chr, sample_id_chr, result_vchr,
        //								unit_vchr, deviceid_chr, device_check_item_name_vchr, refrange_vchr,
        //								check_item_name_vchr, check_item_english_name_vchr, min_val_dec,
        //								max_val_dec, abnormal_flag_chr, check_dat, clinicapp_vchr, memo_vchr,
        //								confirm_dat, pointliststr_vchr, summary_vchr, graph_img, t1.status_int,
        //								checker1_chr, checker2_chr, confirm_person_chr, operator_id_chr,
        //								check_deptid_chr,t2.lastname_vchr as confirmPerson,t3.lastname_vchr as checkPerson
        //							FROM t_opr_lis_check_result t1,t_bse_employee t2,t_bse_employee t3
        //							WHERE (t1.confirm_person_chr = t2.empid_chr(+))
        //   AND (t1.checker1_chr = t3.empid_chr(+)) AND  groupid_chr in ( '"+p_strGroupIDFirst+"','"+p_strGroupIDSecond+"') AND sample_id_chr in ('"+p_strSmapleIDFirst+"','"+p_strSampleIDSecond+"') AND t1.status_int = 1 ";


        //            try
        //            {

        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc=new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
        //                lngRes=objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbResult);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region �������������¼��״̬
        [AutoComplete]
        public long m_lngRefreshDeviceResultStatus( string p_strDeviceID, DateTime p_dtCheckDate, string p_strDeviceSampleID, int p_intStatus)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_lis_result
							SET PStatus_int =" + " p_intStatus where deviceid_chr = " + p_strDeviceID + " and device_sampleid_chr=" + p_strDeviceSampleID + " and check_dat=to_date('" + p_dtCheckDate.ToString() + "','yyyy-mm-dd hh24:mi:ss') and PStatus_int=1";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        //        #region ���ݼ������ڻ���������ڲ�ѯ���鵥��(�����ظ�) 
        //        [AutoComplete]
        //        public long m_lngGetApplFormNOByDateRange( string p_strDateFieldName, System.DateTime p_dtBegin, System.DateTime p_dtEnd, out System.Data.DataTable p_dtbAppl)
        //        {
        //            long lngRes=0;
        //            p_dtbAppl = null;
        //            string strSQL=@"SELECT DISTINCT application_form_no_chr
        //							FROM t_opr_lis_sample
        //							WHERE status_int=1 and  sample_id_chr IN (
        //									SELECT  sample_id_chr
        //												FROM t_opr_lis_check_result
        //												WHERE status_int=1 and "+p_strDateFieldName+" BETWEEN '"+p_dtBegin+@"'
        //																				AND '"+p_dtEnd+"')";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc=new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
        //                lngRes=objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbAppl);
        //                objHRPSvc.Dispose();                                				
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��   
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����SampleID,GroupID�����������飩��ѯ�����,����Ҫ��ѯ������һ����t_opr_lis_check_result,һ����t_aid_lis_check_group_detail����ӡʱ����ӡ��𣬴�ӡ˳���ӡ�� 
        //        [AutoComplete]
        //            /// <summary>
        //            /// ����SampleID,GroupID�����������飩��ѯ�����
        //            /// </summary>
        //            /// <param name="p_objPrincipal"></param>
        //            /// <param name="p_strGroupID"></param>
        //            /// <param name="p_strSmapleID"></param>
        //            /// <param name="p_dtbPrintResult">
        //            /// modify_dat
        //            /// groupid_chr
        //            /// check_item_id_chr
        //            /// sample_id_chr
        //            /// result_vchr
        //            /// unit_vchr 
        //            /// device_check_item_name_vchr
        //            /// refrange_vchr
        //            /// check_item_name_vchr
        //            /// check_item_english_name_vchr
        //            /// min_val_dec
        //            /// max_val_dec
        //            /// abnormal_flag_chr
        //            /// check_dat
        //            /// clinicapp_vchr
        //            /// memo_vchr
        //            /// confirm_dat
        //            /// deviceid_chr
        //            /// pointliststr_vchr
        //            /// summary_vchr
        //            /// graph_img
        //            /// status_int
        //            /// checker1_chr
        //            /// checker2_chr
        //            /// confirm_person_chr
        //            /// operator_id_chr
        //            /// check_deptid_chr
        //            /// print_ord_int
        //            /// </param>
        //            /// <returns></returns>
        //        public long m_lngGetPrintResult(string p_strGroupID,string p_strSmapleID,out System.Data.DataTable p_dtbPrintResult)
        //        {
        //            long lngRes=0;
        //            p_dtbPrintResult=null;
        //            string strSQL=@"SELECT t1.modify_dat, t1.groupid_chr, t1.check_item_id_chr, t1.sample_id_chr,
        //							t1.result_vchr, t1.unit_vchr, t1.device_check_item_name_vchr,
        //							t1.refrange_vchr, t1.check_item_name_vchr,
        //							t1.check_item_english_name_vchr, t1.min_val_dec, t1.max_val_dec,
        //							t1.abnormal_flag_chr, t1.check_dat, t1.clinicapp_vchr, t1.memo_vchr,
        //							t1.confirm_dat, t1.deviceid_chr, t1.pointliststr_vchr, t1.summary_vchr,
        //							t1.graph_img, t1.status_int, t1.checker1_chr, t1.checker2_chr,
        //							t1.confirm_person_chr, t1.operator_id_chr, t1.check_deptid_chr,
        //							t2.print_ord_int
        //							FROM t_opr_lis_check_result t1, t_aid_lis_check_group_detail t2
        //							WHERE t1.groupid_chr = '"+p_strGroupID+@"'
        //							AND t1.sample_id_chr = '"+p_strSmapleID+@"'
        //							AND t1.groupid_chr = t2.groupid_chr 
        //							AND t1.status_int > 0 
        //							AND t1.check_item_id_chr = t2.check_item_id_chr";

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc=new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
        //                lngRes=objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbPrintResult);
        //                objHRPSvc.Dispose();   
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��  
        //            }
        //            return lngRes;
        //        }
        //        #endregion 

        //        #region  ���������걾��, �����ţ��������ڲ�ѯ���ñ걾�����н����Ĵ��š����ơ�ֵ����ϸ���ݡ�
        //        [AutoComplete]
        //        public long m_lngGetDeviceSampleResultList( string strDeviceID,string strDeviceSampleID, string strCheckDate, out System.Data.DataTable dtbSampleResultList)
        //        {
        //            long lngRes = 0;
        //            dtbSampleResultList = null;
        //            string strSQL = @"SELECT tab1.check_item_name_vchr, tab2.result_vchr, tab2.unit_vchr,
        //		                        tab2.refrange_vchr, tab2.max_val_dec, tab2.min_val_dec,tab2.abnormal_flag_vchr
        //			                  FROM 
        //                                (SELECT t11.check_item_name_vchr, t12.device_check_item_name_vchr
        //								   FROM t_bse_lis_check_item t11,t_bse_lis_device_check_item t12,
        //									  t_bse_lis_check_item_dev_item t13,t_bse_lis_device t14
        //								   WHERE t14.deviceid_chr = '"+strDeviceID+@"' AND t12.device_model_id_chr = t14.device_model_id_chr
        //								   AND t13.device_check_item_id_chr = t12.device_check_item_id_chr
        //								   AND t13.device_model_id_chr = t12.device_model_id_chr 
        //                                   AND t11.check_item_id_chr = t13.check_item_id_chr) tab1,
        //		                        (SELECT t1.device_check_item_name_vchr, t1.result_vchr, t1.unit_vchr,
        //		                                t1.refrange_vchr, t1.max_val_dec, t1.min_val_dec, t1.abnormal_flag_vchr
        //			                       FROM t_opr_lis_result t1
        //					               WHERE t1.device_sampleid_chr = '"+strDeviceSampleID+@"'
        //		                           AND t1.check_dat = TO_DATE ('"+strCheckDate+@"', 'yyyy-mm-dd hh24:mi:ss')
        //		                           AND t1.deviceid_chr = '"+strDeviceID+@"'
        //		                           AND t1.idx_int >=
        //		                              (SELECT MAX (t2.begin_idx_int)
        //			                             FROM t_opr_lis_result_log t2
        //										 WHERE t2.device_sampleid_chr = '"+strDeviceSampleID+@"'
        //		                                 AND t2.check_dat = TO_DATE ('"+strCheckDate+@"', 'yyyy-mm-dd hh24:mi:ss')
        //		                                 AND t2.deviceid_chr = '"+strDeviceID+@"')) tab2
        //							  WHERE tab2.device_check_item_name_vchr = tab1.device_check_item_name_vchr";

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleResultList); 
        //                objHRPSvc.Dispose();  

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }
        //        #endregion
        #endregion
    }
}




