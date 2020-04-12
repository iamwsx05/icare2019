using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.emr.EMR_FollowUpSurveyServ
{
    /// <summary>
    /// 随访记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_FollowUpSurveyRecordServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取记录时间列表
        /// <summary>
        /// 获取记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtmCreateTimeList">记录时间列表</param>
        /// <param name="p_lngSEQ_EMR">序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetRecordTimeList(string p_strRegisterID, out DateTime[] p_dtmCreateTimeList, out long[] p_lngSEQ_EMR)
        {
            p_dtmCreateTimeList = null;
            p_lngSEQ_EMR = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select f.createdate_dat,SEQ_EMR
  from T_EMR_FOLLOWUPSURVEYRECORD f
 where f.registerid_chr = ?
   and f.status = 1
 order by f.createdate_dat desc";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strRegisterID;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int rowsCount = dtbResult.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }

                    List<DateTime> dtmRecordTime = new List<DateTime>();
                    List<long> lngSEQ_EMR = new List<long>();
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < rowsCount; i++)
                    {
                        if (DateTime.TryParse(dtbResult.Rows[i][0].ToString(), out dtmTemp))
                        {
                            dtmRecordTime.Add(dtmTemp);
                            lngSEQ_EMR.Add(Convert.ToInt64(dtbResult.Rows[i][1]));
                        }
                    }
                    p_dtmCreateTimeList = dtmRecordTime.ToArray();
                    p_lngSEQ_EMR = lngSEQ_EMR.ToArray();
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

        #region 获取随访记录
        /// <summary>
        /// 获取随访记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ_EMR">序列号</param>
        /// <param name="p_objValue">随访记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordValue(long p_lngSEQ_EMR, out clsEMR_FollowUpSurveyRecordValue p_objValue)
        {
            p_objValue = null;

            long lngRes = 0;
            try
            {
                string strSQL = @"select f.*
  from T_EMR_FOLLOWUPSURVEYRECORD f
 where f.SEQ_EMR = ?
   and f.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_lngSEQ_EMR;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int rowsCount = dtbResult.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }
                    DateTime dtmTemp = DateTime.MinValue;

                    p_objValue = new clsEMR_FollowUpSurveyRecordValue();
                    p_objValue.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString();
                    p_objValue.m_dtmCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]);
                    p_objValue.m_lngSEQ_EMR = p_lngSEQ_EMR;

                    if (DateTime.TryParse(dtbResult.Rows[0]["FOLLOWUPSURVEYDATE"].ToString(), out dtmTemp))
                    {
                        p_objValue.m_dtmFOLLOWUPSURVEYDATE = dtmTemp;
                    }
                    else
                    {
                        p_objValue.m_dtmFOLLOWUPSURVEYDATE = DateTime.Now;
                    }

                    p_objValue.m_strCREATEUSERID_CHR = dtbResult.Rows[0]["CREATEUSERID_CHR"].ToString();

                    if (DateTime.TryParse(dtbResult.Rows[0]["MODIFYDATE_DAT"].ToString(), out dtmTemp))
                    {
                        p_objValue.m_dtmMODIFYDATE_DAT = dtmTemp;
                    }
                    else
                    {
                        p_objValue.m_dtmMODIFYDATE_DAT = DateTime.MinValue;
                    }

                    if (DateTime.TryParse(dtbResult.Rows[0]["DEACTIVEDDATE"].ToString(), out dtmTemp))
                    {
                        p_objValue.m_dtmDEACTIVEDDATE = dtmTemp;
                    }
                    else
                    {
                        p_objValue.m_dtmDEACTIVEDDATE = DateTime.MinValue;
                    }
                    p_objValue.m_strDEACTIVEDOPERATORID = dtbResult.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    p_objValue.m_intSTATUS = Convert.ToInt32(dtbResult.Rows[0]["STATUS"]);
                    p_objValue.m_strFOLLOWUPSURVEYRECORD = dtbResult.Rows[0]["FOLLOWUPSURVEYRECORD"].ToString();

                    //获取签名集合
                    if (dtbResult.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        p_objValue.m_lngSEQ_Sign = Convert.ToInt64(dtbResult.Rows[0]["SEQUENCE_INT"]);
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(p_objValue.m_lngSEQ_Sign, out p_objValue.m_objSignerArr);

                        //释放
                        objSign = null;
                    }

                    lngRes = m_lngGetICDDiagnosis(p_lngSEQ_EMR, out p_objValue.m_objICDCodeArr);
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

        #region 添加随访记录
        /// <summary>
        /// 添加随访记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">随访记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewRecord(clsEMR_FollowUpSurveyRecordValue p_objRecord)
        {
            if (p_objRecord == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"INSERT INTO T_EMR_FOLLOWUPSURVEYRECORD 
(REGISTERID_CHR,CREATEDATE_DAT,CREATEUSERID_CHR,MODIFYDATE_DAT,STATUS,FOLLOWUPSURVEYRECORD,SEQUENCE_INT,SEQ_EMR,FOLLOWUPSURVEYDATE) 
VALUES (?,?,?,?,?,?,?,?,?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                //获取签名流水号

                long lngSequence = 0;
                long lngSEQ_EMR = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR", out lngSEQ_EMR);

                string strNow = objSign.m_strGetDBServerTime();
                DateTime dtmNow = DateTime.Now;
                if (!DateTime.TryParse(strNow, out dtmNow))
                {
                    dtmNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objRecord.m_strREGISTERID_CHR;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtmNow;
                objDPArr[2].Value = p_objRecord.m_strCREATEUSERID_CHR;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = dtmNow;
                objDPArr[4].Value = 1;
                objDPArr[5].Value = p_objRecord.m_strFOLLOWUPSURVEYRECORD;
                objDPArr[6].Value = lngSequence;
                objDPArr[7].Value = lngSEQ_EMR;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecord.m_dtmFOLLOWUPSURVEYDATE;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                if (lngRes < 0)
                {
                    return lngRes;
                }
                lngRes = objSign.m_lngAddSign(p_objRecord.m_objSignerArr, lngSequence);

                if (p_objRecord.m_objICDCodeArr != null && p_objRecord.m_objICDCodeArr.Length > 0)
                {
                    for (int io = 0; io < p_objRecord.m_objICDCodeArr.Length; io++)
                    {
                        p_objRecord.m_objICDCodeArr[io].m_dtmCREATEDATE_DAT = dtmNow;
                        p_objRecord.m_objICDCodeArr[io].m_lngSEQ_EMR = lngSEQ_EMR;
                    }

                    lngRes = m_lngAddNewDiagnosis(p_objRecord.m_objICDCodeArr);
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

        #region 修改随访记录
        /// <summary>
        /// 修改随访记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord">随访记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRecord(clsEMR_FollowUpSurveyRecordValue p_objRecord)
        {
            if (p_objRecord == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update T_EMR_FOLLOWUPSURVEYRECORD f
   set f.status = -1
 where f.SEQ_EMR = ?
   and f.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objRecord.m_lngSEQ_EMR;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                if (lngRes <= 0)
                {
                    return -1;
                }

                strSQL = @"INSERT INTO T_EMR_FOLLOWUPSURVEYRECORD 
(REGISTERID_CHR,CREATEDATE_DAT,CREATEUSERID_CHR,MODIFYDATE_DAT,STATUS,FOLLOWUPSURVEYRECORD,SEQUENCE_INT,SEQ_EMR,FOLLOWUPSURVEYDATE) 
VALUES (?,?,?," + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",?,?,?,?,?)";


                //获取签名流水号

                long lngSequence = 0;
                long lngSEQ_EMR = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR", out lngSEQ_EMR);

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objRecord.m_strREGISTERID_CHR;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecord.m_dtmCREATEDATE_DAT;
                objDPArr[2].Value = p_objRecord.m_strCREATEUSERID_CHR;
                objDPArr[3].Value = 1;
                objDPArr[4].Value = p_objRecord.m_strFOLLOWUPSURVEYRECORD;
                objDPArr[5].Value = lngSequence;
                objDPArr[6].Value = lngSEQ_EMR;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = p_objRecord.m_dtmFOLLOWUPSURVEYDATE;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes < 0)
                {
                    return lngRes;
                }
                lngRes = objSign.m_lngAddSign(p_objRecord.m_objSignerArr, lngSequence);

                string strSQLICD = @"update T_EMR_FOLLOWUPSURVEY_ICD t set t.status = -1 where t.seq_emr = ? and t.status = 1";

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objRecord.m_lngSEQ_EMR;

                lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQLICD, ref lngEff, objDPArr);

                if (p_objRecord.m_objICDCodeArr != null && p_objRecord.m_objICDCodeArr.Length > 0)
                {
                    for (int io = 0; io < p_objRecord.m_objICDCodeArr.Length; io++)
                    {
                        p_objRecord.m_objICDCodeArr[io].m_lngSEQ_EMR = lngSEQ_EMR;
                        p_objRecord.m_objICDCodeArr[io].m_dtmCREATEDATE_DAT = p_objRecord.m_dtmCREATEDATE_DAT;
                    }
                    m_lngAddNewDiagnosis(p_objRecord.m_objICDCodeArr);
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

        #region 删除选定记录
        /// <summary>
        /// 删除选定记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ_EMR">序列号</param>
        /// <param name="p_strOperatorID">删除操作者ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord(long p_lngSEQ_EMR, string p_strOperatorID)
        {
            if (string.IsNullOrEmpty(p_strOperatorID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"update T_EMR_FOLLOWUPSURVEYRECORD f
   set f.status = 0, f.deactiveddate = " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @", f.deactivedoperatorid = ?
 where f.SEQ_EMR = ?
   and f.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strOperatorID;
                objDPArr[1].Value = p_lngSEQ_EMR;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                if (lngRes > 0)
                {
                    strSQL = @"update T_EMR_FOLLOWUPSURVEY_ICD t
   set t.status = 0
 where t.seq_emr = ?
   and t.status = 1";

                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    //赋值

                    objDPArr[0].Value = p_lngSEQ_EMR;

                    lngEff = -1;
                    long lngRes1 = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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

        #region 新添诊断及ICD码

        /// <summary>
        /// 新添诊断及ICD码

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objICD">诊断及ICD码内容(SEQ_EMR应由外部传入，与主表保持一致)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDiagnosis(clsEMR_FollowUpSurvey_ICD[] p_objICD)
        {
            if (p_objICD == null || p_objICD.Length == 0)
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                long lngEff = -1;

                string strSQL = @"INSERT INTO T_EMR_FOLLOWUPSURVEY_ICD 
(REGISTERID_CHR,CREATEDATE_DAT,SEQ_EMR,ORDER_ID,STATUS,ICD10,DIAGNOSIS) VALUES (?,?,?,?,?,?,?)";

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objICD.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(7, out objDPArr2);
                        objDPArr2[0].Value = p_objICD[i].m_strREGISTERID_CHR;
                        objDPArr2[1].DbType = DbType.DateTime;
                        objDPArr2[1].Value = p_objICD[i].m_dtmCREATEDATE_DAT;
                        objDPArr2[2].Value = p_objICD[i].m_lngSEQ_EMR;
                        objDPArr2[3].Value = p_objICD[i].m_intORDER_ID;
                        objDPArr2[4].Value = 1;
                        objDPArr2[5].Value = p_objICD[i].m_strICD10;
                        objDPArr2[6].Value = p_objICD[i].m_strDIAGNOSIS;

                        lngEff = -1;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.Int64, DbType.Int32, DbType.Int32, DbType.String, DbType.String };
                    object[][] objValues = new object[7][];

                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_objICD.Length];//初始化


                    }
                    for (int k1 = 0; k1 < p_objICD.Length; k1++)
                    {
                        objValues[0][k1] = p_objICD[k1].m_strREGISTERID_CHR;
                        objValues[1][k1] = p_objICD[k1].m_dtmCREATEDATE_DAT;
                        objValues[2][k1] = p_objICD[k1].m_lngSEQ_EMR;
                        objValues[3][k1] = p_objICD[k1].m_intORDER_ID;
                        objValues[4][k1] = 1;
                        objValues[5][k1] = p_objICD[k1].m_strICD10;
                        objValues[6][k1] = p_objICD[k1].m_strDIAGNOSIS;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region 获取诊断及ICD码

        /// <summary>
        /// 获取诊断及ICD码

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ_EMR">记录索引</param>
        /// <param name="p_objICD">返回内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICDDiagnosis(long p_lngSEQ_EMR, out clsEMR_FollowUpSurvey_ICD[] p_objICD)
        {
            long lngRes = 0;
            p_objICD = null;
            try
            {
                string strSQL = @"select t.*
  from T_EMR_FOLLOWUPSURVEY_ICD t
 where t.seq_emr = ?
   and t.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_lngSEQ_EMR;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        DataRow drCurrent = null;
                        p_objICD = new clsEMR_FollowUpSurvey_ICD[intRowsCount];

                        for (int i = 0; i < intRowsCount; i++)
                        {
                            drCurrent = dtbResult.Rows[i];
                            p_objICD[i] = new clsEMR_FollowUpSurvey_ICD();

                            p_objICD[i].m_dtmCREATEDATE_DAT = Convert.ToDateTime(drCurrent["CREATEDATE_DAT"]);
                            p_objICD[i].m_intORDER_ID = Convert.ToInt32(drCurrent["ORDER_ID"]);
                            p_objICD[i].m_intSTATUS = Convert.ToInt32(drCurrent["STATUS"]);
                            p_objICD[i].m_lngSEQ_EMR = p_lngSEQ_EMR;
                            p_objICD[i].m_strDIAGNOSIS = drCurrent["DIAGNOSIS"].ToString();
                            p_objICD[i].m_strICD10 = drCurrent["ICD10"].ToString();
                            p_objICD[i].m_strREGISTERID_CHR = drCurrent["REGISTERID_CHR"].ToString();
                        }
                    }
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

        #region 获取选中的住院信息

        /// <summary>
        /// 获取选中的住院信息

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetSelectedInHospitalInfo(string p_strRegisterID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @" select re.hisinpatientdate,
       re.registerid_chr,
       re.hisinpatientid_chr,
       le.modify_dat outdate,
       de.lastname_vchr,
       de.sex_chr,
       de.birth_dat
  from t_bse_hisemr_relation re
 inner join t_opr_bih_leave le on le.registerid_chr = re.registerid_chr
                              and le.status_int = 1
 inner join t_opr_bih_registerdetail de on de.registerid_chr =
                                           re.registerid_chr
 where re.registerid_chr = ? ";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值


                objDPArr[0].Value = p_strRegisterID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}
