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
    /// 随访记录提醒
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_FollowUpSurveyRemindServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取最后一次住院信息

        /// <summary>
        /// 获取最后一次住院信息

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEMRInPatientID">EMR住院号</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetLatestInHospitalInfo(string p_strEMRInPatientID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strEMRInPatientID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" re.hisinpatientdate,
       le.modify_dat outdate,
       de.lastname_vchr,
       de.sex_chr,
       re.registerid_chr,
       re.HISINPATIENTID_CHR
  from T_BSE_HISEMR_RELATION re
 inner join t_opr_bih_leave le on le.registerid_chr = re.registerid_chr and le.STATUS_INT=1
 inner join t_opr_bih_registerdetail de on de.registerid_chr =
                                           re.registerid_chr
 where re.emrinpatientid = ?
 order by re.emrinpatientdate desc " + clsDatabaseSQLConvert.s_StrRownum;

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strEMRInPatientID;

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

        #region 获取选择的随访设置

        /// <summary>
        /// 获取选择的随访设置

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_lngSEQ_EMR">序列号</param>
        /// <param name="p_objValue">随访提醒设置</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSelectedFollowUpSurveyRemind(long m_lngSEQ_EMR, out clsEMR_FollowUpSurveyRemindValue p_objValue)
        {
            p_objValue = null;

            long lngRes = -1;
            try
            {
                string strSQL = @"select t.*
  from T_EMR_FOLLOWUPSURVEYREMIND t
 where t.SEQ_EMR = ?
   and t.STATUS=1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = m_lngSEQ_EMR;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0 && dtbResult != null)
                {
                    int rowsCount = dtbResult.Rows.Count;
                    if (rowsCount > 0)
                    {
                        DataRow drFirst = dtbResult.Rows[0];
                        p_objValue = new clsEMR_FollowUpSurveyRemindValue();
                        p_objValue.m_strREGISTERID_CHR = drFirst["REGISTERID_CHR"].ToString();
                        p_objValue.m_intSTATUS = Convert.ToInt32(drFirst["STATUS"]);
                        p_objValue.m_dtmFOLLOWUPSURVEYTIME_DAT = Convert.ToDateTime(drFirst["FOLLOWUPSURVEYTIME_DAT"]);
                        p_objValue.m_intANTICIPATETIME_INT = Convert.ToInt32(drFirst["ANTICIPATETIME_INT"]);
                        p_objValue.m_strREMAINDTEXT = drFirst["REMAINDTEXT"].ToString();
                        p_objValue.m_strOPERATORID_CHR = drFirst["OPERATORID_CHR"].ToString();
                        p_objValue.m_dtmCREATEDATE_DAT = Convert.ToDateTime(drFirst["CREATEDATE_DAT"]);
                        p_objValue.m_lngSEQ_EMR = m_lngSEQ_EMR;
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

        #region 添加新的随访提醒设置
        /// <summary>
        /// 添加新的随访提醒设置
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue">随访提醒设置</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewFollowUpSurveyRemind(clsEMR_FollowUpSurveyRemindValue p_objValue)
        {
            if (p_objValue == null)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"INSERT INTO T_EMR_FOLLOWUPSURVEYREMIND 
(REGISTERID_CHR,STATUS,FOLLOWUPSURVEYTIME_DAT,ANTICIPATETIME_INT,REMAINDTEXT,OPERATORID_CHR,CREATEDATE_DAT,SEQ_EMR) 
VALUES (?,?,?,?,?,?," + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",?)";

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR", out lngSequence);

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objValue.m_strREGISTERID_CHR;
                objDPArr[1].Value = 1;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objValue.m_dtmFOLLOWUPSURVEYTIME_DAT;
                objDPArr[3].Value = p_objValue.m_intANTICIPATETIME_INT;
                objDPArr[4].Value = p_objValue.m_strREMAINDTEXT;
                objDPArr[5].Value = p_objValue.m_strOPERATORID_CHR;
                objDPArr[6].Value = lngSequence;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 修改随访提醒设置
        /// <summary>
        /// 修改随访提醒设置 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue">随访提醒设置</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyFollowUpSurveyRemind(clsEMR_FollowUpSurveyRemindValue p_objValue)
        {
            if (p_objValue == null)
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"update T_EMR_FOLLOWUPSURVEYREMIND 
set ANTICIPATETIME_INT=?,REMAINDTEXT=?,OPERATORID_CHR=?,FOLLOWUPSURVEYTIME_DAT=?
where SEQ_EMR=?";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                //赋值

                objDPArr[0].Value = p_objValue.m_intANTICIPATETIME_INT;
                objDPArr[1].Value = p_objValue.m_strREMAINDTEXT;
                objDPArr[2].Value = p_objValue.m_strOPERATORID_CHR;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objValue.m_dtmFOLLOWUPSURVEYTIME_DAT;
                objDPArr[4].Value = p_objValue.m_lngSEQ_EMR;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (DataException objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除指定的随访提醒设置

        /// <summary>
        /// 删除指定的随访提醒设置

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_lngSEQ_EMR">序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteFollowUpSurveyRemind(long m_lngSEQ_EMR)
        {
            long lngRes = -1;
            try
            {
                string strSQL = @"update T_EMR_FOLLOWUPSURVEYREMIND 
set STATUS=0 where SEQ_EMR=?";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = m_lngSEQ_EMR;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (DataException objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取指定病人随访时间列表
        /// <summary>
        /// 获取指定病人随访时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtmTimeList">病人随访时间列表</param>
        /// <param name="m_lngEMR_SEQ">序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFollowUpSurveyTimeList(string p_strRegisterID, out DateTime[] p_dtmTimeList, out long[] m_lngEMR_SEQ)
        {
            p_dtmTimeList = null;
            m_lngEMR_SEQ = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"select t.FOLLOWUPSURVEYTIME_DAT,SEQ_EMR
  from T_EMR_FOLLOWUPSURVEYREMIND t
 where t.status = 1
   and t.registerid_chr = ?
 order by t.FOLLOWUPSURVEYTIME_DAT desc";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strRegisterID;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null)
                {
                    int rowsCount = dtResult.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }
                    List<DateTime> dtTime = new List<DateTime>();
                    List<long> lngSEQ = new List<long>();
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
                    {
                        if (DateTime.TryParse(dtResult.Rows[rowIndex][0].ToString(), out dtmTemp))
                        {
                            dtTime.Add(dtmTemp);
                            lngSEQ.Add(Convert.ToInt64(dtResult.Rows[rowIndex][1]));
                        }
                    }
                    p_dtmTimeList = dtTime.ToArray();
                    m_lngEMR_SEQ = lngSEQ.ToArray();
                }
            }
            catch (DataException objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取到期提示的基本信息

        /// <summary>
        /// 获取到期提示的基本信息

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDueTimeRemind(out DataTable p_dtbResult)
        {
            p_dtbResult = null;

            long lngRes = -1;
            try
            {
                string strDateDiff = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    strDateDiff = "(t.followupsurveytime_dat - sysdate)";
                }
                else
                {
                    strDateDiff = "(DATEDIFF(mi, getdate(), t.followupsurveytime_dat) / 1440)";
                }

                string strSQL = @"select t.followupsurveytime_dat,
       t.operatorid_chr,
       t.remaindtext,
       t.anticipatetime_int
  from t_emr_followupsurveyremind t
 where t.status = 1
   and " + strDateDiff + @" between t.anticipatetime_int - 1 and
       t.anticipatetime_int + 1
   order by t.operatorid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
            }
            catch (DataException objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }
}
