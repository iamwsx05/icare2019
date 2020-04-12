using System;
//using com.digitalwave.iCare.middletier.HRPService_Orders;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert; 

namespace com.digitalwave.InHospitalMainRecord
{
    /// <summary>
    /// 操作Oracle8i
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsQuery8iServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsQuery8iServ()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获取军惠ICD10表信息
        /// <summary>
        /// 获取军惠ICD10表信息
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICDFrom8i(out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
           clsHRPTableService objServ = new clsHRPTableService();
           try
           {

               string strSQL = @"select diagnosis_code, diagnosis_name, input_code, statistic_code
									from diagnosis_dict  order by diagnosis_code,diagnosis_name";

               p_dtbResult = new DataTable();

               lngRes = objServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
           }
           catch (Exception objEx)
           {
               string strTmp = objEx.Message;
               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
               bool blnRes = objLogger.LogError(objEx);
           }
           finally
           {
               //objServ.Dispose();
           }
            //返回
            return lngRes;
        }
        #endregion

        #region 查询麻醉方式
        /// <summary>
        /// 查询麻醉方式
        /// </summary>
        /// <param name="p_strInput"></param>
        /// <param name="strXML"></param>
        /// <param name="intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAnaesthesiaModeLikeID(  out clsAnaesthesiaModeInOperation[] p_objAnaesthesiaModeInOperation)
        {
            p_objAnaesthesiaModeInOperation = null;
            long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            { 
                string strCommand = " select serial_no,anaesthesia_name from anaesthesia_dict order by serial_no";
                DataTable objResult = new DataTable();

                lngRes = objServ.lngGetDataTableWithoutParameters(strCommand, ref objResult);

                if (lngRes > 0 && objResult.Rows.Count > 0)
                {
                    p_objAnaesthesiaModeInOperation = new clsAnaesthesiaModeInOperation[objResult.Rows.Count];
                    for (int i = 0; i < objResult.Rows.Count; i++)
                    {
                        p_objAnaesthesiaModeInOperation[i] = new clsAnaesthesiaModeInOperation();
                        p_objAnaesthesiaModeInOperation[i].strAnaesthesiaModeID = objResult.Rows[i]["SERIAL_NO"].ToString();
                        p_objAnaesthesiaModeInOperation[i].strAnaesthesiaModeName = objResult.Rows[i]["ANAESTHESIA_NAME"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 查询获得手术名称、编码
        /// <summary>
        /// 查询获得手术名称、编码
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbOpDesc"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOperationDesc( out DataTable p_dtbOpDesc)
        {
            p_dtbOpDesc = null;
            long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            { 
                string strCommand = " select operation_code,operation_name,input_code from operation_dict order by operation_code";
                DataTable objResult = new DataTable();

                lngRes = objServ.lngGetDataTableWithoutParameters(strCommand, ref objResult);

                if (lngRes > 0 && objResult.Rows.Count > 0)
                {
                    p_dtbOpDesc = objResult;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 诊断记录
        /// <summary>
        /// 删除指定诊断记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelDiagnosis(string p_strPatientID, int p_intVisitID)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;
           clsHRPTableService objServ = new clsHRPTableService();
           try
           {

               string strDelSQL = @"delete from diagnosis where patient_id = '" + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString() ;
               lngRes = objServ.DoExcute(strDelSQL);
               
           }
           catch (Exception objEx)
           {
               string strTmp = objEx.Message;
               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
               bool blnRes = objLogger.LogError(objEx);
           }
           finally
           {
               //objServ.Dispose();
           }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 添加诊断记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_intDiagnoseType"></param>
        /// <param name="p_intDiagnoseNO"></param>
        /// <param name="p_strDiagnoseDesc"></param>
        /// <param name="p_strDiagnoseDate"></param>
        /// <param name="p_strTreatResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertDiagnosis(string p_strPatientID, int p_intVisitID, int p_intDiagnoseType,
            int p_intDiagnoseNO, string p_strDiagnoseDesc, string p_strDiagnoseDate, string p_strTreatResult)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();
                string strInsertSQL = @"insert into diagnosis ( patient_id , visit_id , diagnosis_type , diagnosis_no , diagnosis_desc , diagnosis_date , treat_result  ) 
				values ( '" + p_strPatientID.Trim() + "' , " + p_intVisitID.ToString() + " , '" + p_intDiagnoseType.ToString() + "' , '" + p_intDiagnoseNO.ToString() + "' , '"
                    + p_strDiagnoseDesc + "' , " + com.digitalwave.Utility.SQLConvert.clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strDiagnoseDate) + " , '" + p_strTreatResult + "' )";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 诊断分类记录
        /// <summary>
        /// 删除指定诊断分类记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelDiagnosticCategory(string p_strPatientID, int p_intVisitID)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;
           clsHRPTableService objServ = new clsHRPTableService();
            try
            {
              
                string strDelSQL = @"delete from diagnostic_category where patient_id = '" + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString() ;
                lngRes = objServ.DoExcute(strDelSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 添加诊断分类记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_intDiagnoseType"></param>
        /// <param name="p_intDiagnoseNO"></param>
        /// <param name="p_strDiagnoseCode"></param>
        /// <param name="p_strStatisticCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertDiagnosticCategory(string p_strPatientID, int p_intVisitID, int p_intDiagnoseType,
            int p_intDiagnoseNO, string p_strDiagnoseCode, string p_strStatisticCode)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();

                string strInsertSQL = @"insert into diagnostic_category ( patient_id , visit_id , diagnosis_type , diagnosis_no , diagnosis_code , statistic_code ) 
				values ( '" + p_strPatientID.Trim() + "' , " + p_intVisitID.ToString() + " , '" + p_intDiagnoseType.ToString() + "' , '" + p_intDiagnoseNO.ToString() + "' , '"
                    + p_strDiagnoseCode + "' , '" + p_strStatisticCode + "') ";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 手术记录
        /// <summary>
        /// 删除指定手术记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelOperation(string p_strPatientID, int p_intVisitID)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;
             clsHRPTableService objServ = new clsHRPTableService();
             try
             {

                 string strDelSQL = @"delete from operation where patient_id = '" + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString();
                 lngRes = objServ.DoExcute(strDelSQL);
                 
             }
             catch (Exception objEx)
             {
                 string strTmp = objEx.Message;
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                 bool blnRes = objLogger.LogError(objEx);
             }
             finally
             {
                 //objServ.Dispose();
             }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 添加手术记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_strOperatingDate"></param>
        /// <param name="p_strOperationDesc"></param>
        /// <param name="p_strOperationCode"></param>
        /// <param name="p_strOperationNO"></param>
        /// <param name="p_strHeal"></param>
        /// <param name="p_strAnaesthesiaMethod"></param>
        /// <param name="p_strOperator"></param>
        /// <param name="p_strOperatorAssistantI"></param>
        /// <param name="p_strOperatorAssistantII"></param>
        /// <param name="p_strAnaesthesiaOperator"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertOperation(string p_strPatientID, int p_intVisitID, string p_strOperatingDate,
            string p_strOperationDesc, string p_strOperationCode, string p_strOperationNO, string p_strHeal,
            string p_strAnaesthesiaMethod, string p_strOperator, string p_strOperatorAssistantI, string p_strOperatorAssistantII,
            string p_strAnaesthesiaOperator)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();

                string strInsertSQL = @"insert into operation ( patient_id, visit_id, operating_date, operation_desc, operation_code, operation_no,
				heal, anaesthesia_method, operator, operator_assistant_i, operator_assistant_ii, anaesthesia_operator) 
				values ( '" + p_strPatientID.Trim() + "', " + p_intVisitID.ToString() + ", " + com.digitalwave.Utility.SQLConvert.clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOperatingDate) + ", '"
                + p_strOperationDesc + "', '" + p_strOperationCode + "', '" + p_strOperationNO + "', '" + p_strHeal + "', '" + p_strAnaesthesiaMethod + "', '" + p_strOperator + "', '" + p_strOperatorAssistantI + "', '"
                + p_strOperatorAssistantII + "', '" + p_strAnaesthesiaOperator + "' )";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 诊断对照记录
        /// <summary>
        /// 删除指定的诊断对照记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelDiagComparing(string p_strPatientID, int p_intVisitID)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;
           clsHRPTableService objServ = new clsHRPTableService();
           try
           {

               string strDelSQL = @"delete from diag_comparing where patient_id = '" + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString();
               lngRes = objServ.DoExcute(strDelSQL);
               
           }
           catch (Exception objEx)
           {
               string strTmp = objEx.Message;
               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
               bool blnRes = objLogger.LogError(objEx);
           }
           finally
           {
               //objServ.Dispose();
           }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 添加诊断对照记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_strDiagCompareGroup"></param>
        /// <param name="p_strDiagCorrespondence"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertDiagComparing(string p_strPatientID, int p_intVisitID,
            string p_strDiagCompareGroup, string p_strDiagCorrespondence)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();

                string strInsertSQL = @"insert into diag_comparing ( patient_id, visit_id, diag_compare_group, diag_correspondence) 
				values ( '" + p_strPatientID.Trim() + "', " + p_intVisitID.ToString() + ", '" + p_strDiagCompareGroup + "','" + p_strDiagCorrespondence + "')";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 输血记录
        /// <summary>
        /// 删除指定的输血记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelBloodTransfusion(string p_strPatientID, int p_intVisitID)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {

                string strDelSQL = @"delete from blood_transfusion where patient_id = '" + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString();
                lngRes = objServ.DoExcute(strDelSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 添加输血记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_strWholeBlood"></param>
        /// <param name="p_strRedCell"></param>
        /// <param name="p_strPlatelet"></param>
        /// <param name="p_strPlasma"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertBloodTransfusion(string p_strPatientID, int p_intVisitID,
            string p_strWholeBlood, string p_strRedCell, string p_strPlatelet, string p_strPlasma, string p_strOther)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();

                string strInsertSQL = @"insert into blood_transfusion ( patient_id, visit_id, whole_blood, pure_red_cell,platelet,plasma,other) 
				values ( '" + p_strPatientID.Trim() + "', " + p_intVisitID.ToString() + ", '" + p_strWholeBlood + "','" + p_strRedCell + "','" + p_strPlatelet + "','" + p_strPlasma + "','" + p_strOther + "')";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 费用记录
        /// <summary>
        /// 查找是否已有该费用记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_strFeeType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicalCostInfo(string p_strPatientID, int p_intVisitID, string p_strFeeType)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty
                || p_intVisitID <= 0 || p_strFeeType == null || p_strFeeType == string.Empty)
                return -1;
            long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {

                string strDelSQL = @"select * from medical_costs where patient_id = '" + p_strPatientID.Trim() 
                    + "' and visit_id = " + p_intVisitID.ToString() + " and fee_type='" + p_strFeeType.Trim() + "'";
                DataTable dtValue = null;
                lngRes = objServ.lngGetDataTableWithoutParameters(strDelSQL, ref dtValue);
                if (lngRes <= 0 || dtValue == null || dtValue.Rows.Count <= 0)
                    return -1;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 刪除指定指定费用记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelMedicalCosts(string p_strPatientID, int p_intVisitID)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {

                string strDelSQL = @"delete from medical_costs where patient_id = '" + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString();
                lngRes = objServ.DoExcute(strDelSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 添加费用记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_strFeeType"></param>
        /// <param name="p_dblCosts"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertMedicalCosts(string p_strPatientID, int p_intVisitID, string p_strFeeType, double p_dblCosts)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();

                string strInsertSQL = @"insert into medical_costs ( patient_id, visit_id, fee_type, costs) 
				values ( '" + p_strPatientID.Trim() + "', " + p_intVisitID.ToString() + ", '" + p_strFeeType + "'," + p_dblCosts.ToString() + ")";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 更将费用记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_strFeeType"></param>
        /// <param name="p_dblCosts"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyMedicalCosts(string p_strPatientID, int p_intVisitID, string p_strFeeType, double p_dblCosts)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0)
                return -1;
            long lngRes = 0;

            try
            {
                clsHRPTableService objServ = new clsHRPTableService();

                string strInsertSQL = @"update medical_costs set costs = " + p_dblCosts.ToString() + " where patient_id = '"
                    + p_strPatientID.Trim() + "' and visit_id = " + p_intVisitID.ToString() + " and fee_type = '" + p_strFeeType + "'";
                lngRes = objServ.DoExcute(strInsertSQL);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 更新病人住院主记录
        /// <summary>
        /// 更新病人住院主记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_intVisitID"></param>
        /// <param name="p_objContent"></param>
        /// <param name="p_strDirDocID"></param>
        /// <param name="p_strAttendingDocID"></param>
        /// <param name="p_strOutDoc"></param>
        /// <param name="p_strInDoc"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyPatVisit(string p_strPatientID, int p_intVisitID, clsInHospitalMainRecord_GXContent p_objContent,
            string p_strDirDocID, string p_strAttendingDocID, string p_strOutDoc, string p_strInDoc)
        {
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_intVisitID <= 0 || p_objContent == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objServ = new clsHRPTableService();
            try
            {


                string strInsertSQL = @"update pat_visit set pat_adm_condition='" + (p_objContent.m_intCONDICTIONWHENIN == -1 ? "0" : p_objContent.m_intCONDICTIONWHENIN.ToString()) + "',diagnosis_date=" + com.digitalwave.Utility.SQLConvert.clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy-MM-dd HH:mm:ss"))
                    + ",emer_treat_times=" + p_objContent.m_intSALVETIMES.ToString() + ",esc_emer_times=" + p_objContent.m_intSALVESUCCESS.ToString() + ",second_transfer=" + (p_objContent.m_intSECONDLEVELTRANSFER == -1 ? "2" : p_objContent.m_intSECONDLEVELTRANSFER.ToString()) + ",new_five_diagnosis=" + (p_objContent.m_intNEW5DISEASE == -1 ? "2" : p_objContent.m_intNEW5DISEASE.ToString()) + ",alergy_drugs='" + (p_objContent.m_strSENSITIVE == null ? "" : p_objContent.m_strSENSITIVE)
                    + "',hbsag=" + p_objContent.m_intHBSAG.ToString() + ",hcv_ab=" + p_objContent.m_intHCV_AB.ToString() + ",hiv_ab=" + p_objContent.m_intHIV_AB.ToString() + ",follow_indicator=" + (p_objContent.m_intHASREMIND == 1 ? "1" : "0") + ",teaching_category=" + (p_objContent.m_intMODELCASE == -1 ? "2" : p_objContent.m_intMODELCASE.ToString()) + ",first_case=" + (p_objContent.m_intFIRSTCASE == -1 ? "2" : p_objContent.m_intFIRSTCASE.ToString())
                    + ",mr_quality='" + (p_objContent.m_intQUALITY == -1 ? "0" : p_objContent.m_intQUALITY.ToString()) + "',blood_tran_react_times=" + (p_objContent.m_intBLOODTRANSACTOIN == -1 ? "0" : p_objContent.m_intBLOODTRANSACTOIN.ToString()) + ",infusion_react_times=" + (p_objContent.m_intTRANSFUSIONSACTION == -1 ? "0" : p_objContent.m_intTRANSFUSIONSACTION.ToString()) + ",ct=" + (p_objContent.m_intCTCHECK == -1 ? "2" : p_objContent.m_intCTCHECK.ToString()) + ",mri=" + (p_objContent.m_intMRICHECK == -1 ? "2" : p_objContent.m_intMRICHECK.ToString())
                    + ",blood_type='" + (p_objContent.m_intBLOODTYPE == -1 ? "0" : p_objContent.m_intBLOODTYPE.ToString()) + "',blood_type_rh='" + (p_objContent.m_intBLOODRH == -1 ? "0" : p_objContent.m_intBLOODRH.ToString()) + "',director='" + (p_strDirDocID == null ? "" : p_strDirDocID.Trim()) + "',attending_doctor='" + (p_strAttendingDocID == null ? "" : p_strAttendingDocID.Trim()) + "',doctor_enter='" + (p_strInDoc == null ? "" : p_strInDoc.Trim())
                    + "',doctor_out='" + (p_strOutDoc == null ? "" : p_strOutDoc.Trim()) + "',follow_interval=" + (p_objContent.m_strREMINDTERM == string.Empty ? "null" : p_objContent.m_strREMINDTERM) + ",follow_interval_units='" + (p_objContent.m_intREMINDTERMTYPE <= 0 ? "" : p_objContent.m_intREMINDTERMTYPE.ToString()) + "'  where patient_id='" + p_strPatientID.Trim() + "' and visit_id = '" + p_intVisitID.ToString() + "'";
                lngRes = objServ.DoExcute(strInsertSQL);
                
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);

            }
            finally
            {
                //objServ.Dispose();
            }
            //返回
            return lngRes;
        }
        #endregion

        #region VIP病案数
        /// <summary>
        /// VIP病案数
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVipPatientNum(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*)
                                  from pat_visit, pat_master_index
                                 where pat_visit.catalog_date >= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBegin.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.catalog_date < " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEnd.ToString("yyyy-MM-dd"))
                                   +@"and pat_master_index.vip_indicator > 2
                                   and pat_visit.patient_id = pat_master_index.patient_id";
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 新建疾病编码数
        /// <summary>
        /// 新建疾病编码数
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngNewDiagDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*)
                                  from diagnosis_dict
                                 where create_date >= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBegin.ToString("yyyy-MM-dd"))
                                   + @"and create_date < " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEnd.ToString("yyyy-MM-dd"));
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 新建手术编码数
        /// <summary>
        /// 新建手术编码数
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngNewOpDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*)
                                  from operation_dict
                                 where create_date >= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBegin.ToString("yyyy-MM-dd"))
                                   + @"and create_date < " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEnd.ToString("yyyy-MM-dd"));
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 编目诊断总数
        /// <summary>
        /// 编目诊断总数
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCatalogDiagDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(*)
                                  from diagnostic_category, pat_visit
                                 where pat_visit.catalog_date >= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBegin.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.catalog_date < " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEnd.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.patient_id = diagnostic_category.patient_id
                                   and pat_visit.visit_id = diagnostic_category.visit_id";
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 编目诊断种类数
        /// <summary>
        /// 编目诊断总类数
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCatalogDiagTypeDict(DateTime p_dtmBegin, DateTime p_dtmEnd, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(distinct diagnostic_category.diagnosis_code)
                                  from diagnostic_category, pat_visit
                                 where pat_visit.catalog_date >= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBegin.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.catalog_date < " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEnd.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.patient_id = diagnostic_category.patient_id
                                   and pat_visit.visit_id = diagnostic_category.visit_id";
                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 特定编码诊断种类数
        /// <summary>
        /// 特定编码诊断总类数(如V码，E码，M码)
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_strDiagType">特定编码</param>
        /// <param name="p_strNum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCatalogSpecifyDiagTypeDict(DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strDiagType, out string p_strNum)
        {
            long lngRes = 0;
            p_strNum = "0";
            if (p_strDiagType == null)
                return -1;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select count(distinct diagnostic_category.diagnosis_code)
                                  from diagnostic_category, pat_visit
                                 where pat_visit.catalog_date >= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBegin.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.catalog_date < " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEnd.ToString("yyyy-MM-dd"))
                                   + @"and pat_visit.patient_id = diagnostic_category.patient_id
                                   and pat_visit.visit_id = diagnostic_category.visit_id
                                   And (diagnostic_category.diagnosis_code like '"+p_strDiagType.Trim()+"%')";
                                                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count == 1)
                {
                    p_strNum = dtbResult.Rows[0][0].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取医嘱状态
        /// <summary>
        /// 获取医嘱状态
        /// </summary>
        /// <param name="p_objStatus">医嘱状态</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderStatusDicMap(out clsOrderStatus_VO p_objStatus)
        {
            long lngRes = 0;
            p_objStatus = null;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select * from order_status_dicmap";

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objStatus = new clsOrderStatus_VO();
                    p_objStatus.m_strCANCEL_STATUS = dtbResult.Rows[0]["CANCEL_STATUS"].ToString();
                    p_objStatus.m_strEXEC_STATUS = dtbResult.Rows[0]["EXEC_STATUS"].ToString();
                    p_objStatus.m_strNEW_STATUS = dtbResult.Rows[0]["NEW_STATUS"].ToString();
                    p_objStatus.m_strPOST_STATUS = dtbResult.Rows[0]["POST_STATUS"].ToString();
                    p_objStatus.m_strSTOP_STATUS = dtbResult.Rows[0]["STOP_STATUS"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 查询符合条件的医嘱表的病人
        /// <summary>
        /// 查询符合条件的医嘱表的病人
        /// </summary>
        /// <param name="p_strSQL">查询语句</param>
        /// <param name="p_dtbPatient">病人</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrdersPatient(string p_strSQL, out DataTable p_dtbPatient)
        {
            long lngRes = 0;
            p_dtbPatient = null;

            if (string.IsNullOrEmpty(p_strSQL))
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtbPatient);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 获取军惠表中的病人收费信息
        /// <summary>
        /// 获取军惠表中的病人收费信息
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_intVisitID">入院次数</param>
        /// <param name="p_objContent">暂存收费信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCost(string p_strInPatientID,
            int p_intVisitID,
            out clsInHospitalMainRecord_GXContent p_objContent)
        {
            p_objContent = null;
            long lngRes = 0;
 //           if (string.IsNullOrEmpty(p_strInPatientID))
 //               return -1;

 //           string strGetCost = @"select t.fee_type, t.costs
 // from medrec.medical_costs t
 //where t.patient_id = ?
 //  and t.visit_id = ?";

 //           clsHRPTableService objHRPServ = new clsHRPTableService();
 //           try
 //           {
 //               IDataParameter[] objDPArr = null;
 //               objHRPServ.CreateDatabaseOledbParameter(2, out objDPArr);
 //               objDPArr[0].Value = p_strInPatientID.Trim();
 //               objDPArr[1].Value = p_intVisitID;

 //               DataTable m_dtbResult = new DataTable();

 //               lngRes = objHRPServ.lngGetDataTableWithParameters(strGetCost, ref m_dtbResult, objDPArr);

 //               if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
 //               {
 //                   p_objContent = new clsInHospitalMainRecord_GXContent();
 //                   double dblCost = 0.0;
 //                   double dblSum = 0.0;
 //                   for (int i = 0; i < m_dtbResult.Rows.Count; i++)
 //                   {
 //                       dblCost = Convert.ToDouble(m_dtbResult.Rows[i]["COSTS"]);
 //                       switch (m_dtbResult.Rows[i]["FEE_TYPE"].ToString().Trim())
 //                       {
 //                           case "床位":
 //                               p_objContent.m_dblBEDAMT = dblCost;
 //                               break;
 //                           case "护理":
 //                               p_objContent.m_dblNURSEAMT = dblCost;
 //                               break;
 //                           case "西药":
 //                               p_objContent.m_dblWMAMT = dblCost;
 //                               break;
 //                           case "中成":
 //                               p_objContent.m_dblCMFINISHEDAMT = dblCost;
 //                               break;
 //                           case "中草":
 //                               p_objContent.m_dblCMSEMIFINISHEDAMT = dblCost;
 //                               break;
 //                           case "放射":
 //                               p_objContent.m_dblRADIATIONAMT = dblCost;
 //                               break;
 //                           case "化验":
 //                               p_objContent.m_dblASSAYAMT = dblCost;
 //                               break;
 //                           case "输氧":
 //                               p_objContent.m_dblO2AMT = dblCost;
 //                               break;
 //                           case "输血":
 //                               p_objContent.m_dblBLOODAMT = dblCost;
 //                               break;
 //                           case "诊疗":
 //                               p_objContent.m_dblTREATMENTAMT = dblCost;
 //                               break;
 //                           case "手术":
 //                               p_objContent.m_dblOPERATIONAMT = dblCost;
 //                               break;
 //                           case "检查":
 //                               p_objContent.m_dblCHECKAMT = dblCost;
 //                               break;
 //                           case "麻醉":
 //                               p_objContent.m_dblANAETHESIAAMT = dblCost;
 //                               break;
 //                           case "接生":
 //                               p_objContent.m_dblDELIVERYCHILDAMT = dblCost;
 //                               break;
 //                           case "婴儿":
 //                               p_objContent.m_dblBABYAMT = dblCost;
 //                               break;
 //                           case "陪床":
 //                               p_objContent.m_dblACCOMPANYAMT = dblCost;
 //                               break;
 //                           case "其他":
 //                               p_objContent.m_dblOTHERAMT = dblCost;
 //                               break;
 //                       }
 //                       dblSum += dblCost;
 //                   }
 //                   p_objContent.m_dblTOTALAMT = dblSum;
 //               }
 //           }
 //           catch (Exception objEx)
 //           {

 //               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
 //               bool blnRes = objLogger.LogError(objEx);
 //           }      //返回
            return lngRes;
        }
        #endregion
    }
}
