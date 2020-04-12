using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 术前小结(广西)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_SummaryBeforeOPServ : clsDiseaseTrackService
    {
        #region SQL语句
        #region 获取指定病人的所有没有删除记录的时间
        /// <summary>
        /// 获取指定病人的所有没有删除记录的时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createdate, opendate
                                                      from t_emr_summarybeforeop
                                                     where inpatientid = ?
                                                       and inpatientdate = ?
                                                       and status = 0";
        #endregion

        #region 根据指定表单的信息，查找表单的内容
        /// <summary>
        /// 根据指定表单的信息，查找表单的内容
        /// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.ifconfirm,
       a.markstatus,
       a.sequence_int,
       a.recorddate,
       a.registerid_chr,
       a.diseasesummary,
       a.diseasesummaryxml,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.diagnosisgist,
       a.diagnosisgistxml,
       a.opindication,
       a.opindicationxml,
       a.opmode,
       a.opmodexml,
       a.anamode,
       a.anamodexml,
       a.proceeding,
       a.proceedingxml,
       a.preparebeforeop,
       a.preparebeforeopxml,
       a.emr_seq,
       b.modifydate,
       b.modifyuserid,
       b.status,
       b.diseasesummary_right,
       b.diagnosisbeforeop_right,
       b.diagnosisgist_right,
       b.opindication_right,
       b.opmode_right,
       b.anamode_right,
       b.proceeding_right,
       b.preparebeforeop_right
  from t_emr_summarybeforeop a, t_emr_summarybeforeopcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and a.emr_seq = b.emr_seq
   and b.status = 0";
        #endregion

        #region 获取指定时间的表单
        /// <summary>
        /// 获取指定时间的表单
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
                                                          from t_emr_summarybeforeop
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and createdate = ?
                                                           and status = 0";
        #endregion

        #region 获取指定表单的最后修改时间
        /// <summary>
        /// 获取指定表单的最后修改时间
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate, b.modifyuserid
                                                              from t_emr_summarybeforeopcon b
                                                             where emr_seq = ?
                                                               and b.status = 0";
        #endregion

        #region 获取删除表单的主要信息
        /// <summary>
        /// 获取删除表单的主要信息
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
                                                          from t_emr_summarybeforeop
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and opendate = ?
                                                           and status = 1 ";
        #endregion

        #region 添加记录到T_EMR_SUMMARYBEFOREOP
        /// <summary>
        /// 添加记录到T_EMR_SUMMARYBEFOREOP
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_summarybeforeop (inpatientid,inpatientdate,opendate,
        createdate,createuserid,status,markstatus,sequence_int,recorddate,registerid_chr,diseasesummary,diseasesummaryxml,
        diagnosisbeforeop,diagnosisbeforeopxml,diagnosisgist,diagnosisgistxml,opindication,opindicationxml,opmode,opmodexml,
        anamode,anamodexml,proceeding,proceedingxml,preparebeforeop,preparebeforeopxml,emr_seq,ifconfirm) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?)";
        #endregion

        #region 添加记录到T_EMR_SUMMARYBEFOREOPCON
        /// <summary>
        /// 添加记录到T_EMR_SUMMARYBEFOREOPCON
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_summarybeforeopcon (inpatientid,inpatientdate,opendate,
        modifydate,modifyuserid,status,registerid_chr,diseasesummary_right,diagnosisbeforeop_right,diagnosisgist_right,
        opindication_right,opmode_right,anamode_right,proceeding_right,preparebeforeop_right,emr_seq) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?)";
        #endregion

        #region 修改记录到T_EMR_SUMMARYBEFOREOP
        /// <summary>
        /// 修改记录到T_EMR_SUMMARYBEFOREOP
        /// </summary>
        private const string c_strModifyRecordSQL = @"Update t_emr_summarybeforeop set markstatus = ?,sequence_int = ?,recorddate = ?,
        diseasesummary = ?,diseasesummaryxml = ?, diagnosisbeforeop = ?,diagnosisbeforeopxml = ?,diagnosisgist = ?,diagnosisgistxml = ?,opindication = ?,
        opindicationxml = ?,opmode = ?,opmodexml = ?,anamode = ?,anamodexml = ?,proceeding = ?,proceedingxml = ?,preparebeforeop = ?,preparebeforeopxml = ?
        where emr_seq = ? and status=0";
        #endregion

        #region 修改记录到T_EMR_SUMMARYBEFOREOPCON
        /// <summary>
        /// 设置T_EMR_SUMMARYBEFOREOPCON旧记录状态为2
        /// </summary>
        private const string c_strSetOldRecordSQL = @"update t_emr_summarybeforeopcon set status = 2 where emr_seq = ? and status = 0";

        /// <summary>
        /// 修改记录到T_EMR_SUMMARYBEFOREOPCON
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;
        #endregion

        #region 设置T_EMR_SUMMARYBEFOREOP中删除记录的信息
        /// <summary>
        /// 设置T_EMR_SUMMARYBEFOREOP中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_summarybeforeop
                                                       set status = 1, deactiveddate = ?, deactivedoperatorid = ?
                                                     where emr_seq = ?
                                                       and status = 0";
        #endregion

        #region 获取LastModifyDate和FirstPrintDate
        /// <summary>
        /// 获取LastModifyDate和FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate, b.modifydate
                                                                      from t_emr_summarybeforeop a, t_emr_summarybeforeopcon b
                                                                     where a.inpatientid = ?
                                                                       and a.inpatientdate = ?
                                                                       and a.opendate = ?
                                                                       and a.status = 0
                                                                       and a.emr_seq = b.emr_seq
                                                                       and b.status = 0";
        #endregion

        #region 更新FirstPrintDate
        /// <summary>
        /// 更新FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_summarybeforeop 
															set firstprintdate= ? 
															where inpatientid= ? 
															and inpatientdate= ? 
															and opendate=? 
															and firstprintdate is null 
															and status=0";
        #endregion

        #region 获取指定病人的所有指定删除者删除的记录时间
        /// <summary>
        /// 获取指定病人的所有指定删除者删除的记录时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate,opendate 
																from t_emr_summarybeforeop 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";
        #endregion

        #region 获取指定病人的所有已经删除的记录时间
        /// <summary>
        /// 获取指定病人的所有已经删除的记录时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate,opendate 
																from t_emr_summarybeforeop 
																where inpatientid = ? 
																and inpatientdate= ? and status=1";
        #endregion

        #region 获取已删除记录
        /// <summary>
        /// 获取已删除记录
        /// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.ifconfirm,
       a.markstatus,
       a.sequence_int,
       a.recorddate,
       a.registerid_chr,
       a.diseasesummary,
       a.diseasesummaryxml,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.diagnosisgist,
       a.diagnosisgistxml,
       a.opindication,
       a.opindicationxml,
       a.opmode,
       a.opmodexml,
       a.anamode,
       a.anamodexml,
       a.proceeding,
       a.proceedingxml,
       a.preparebeforeop,
       a.preparebeforeopxml,
       a.emr_seq,
       b.modifydate,
       b.modifyuserid,
       b.status,
       b.diseasesummary_right,
       b.diagnosisbeforeop_right,
       b.diagnosisgist_right,
       b.opindication_right,
       b.opmode_right,
       b.anamode_right,
       b.proceeding_right,
       b.preparebeforeop_right
  from t_emr_summarybeforeop a, t_emr_summarybeforeopcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and a.emr_seq = b.emr_seq
   and b.status = 0";
        #endregion 
        #endregion

        #region 获取病人的该记录时间列表
        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//返回
            return lngRes;
        }
        #endregion

        #region 更新数据库中的首次打印时间
        /// <summary>
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strDeleteUserID">删除者ID</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID.Trim();
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 获取指定记录的内容
        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsEMR_SummaryBeforeOPValue objRecordContent = new clsEMR_SummaryBeforeOPValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    if (dtbValue.Rows[0]["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);

                    if (dtbValue.Rows[0]["MARKSTATUS"] == DBNull.Value)
                    {
                        objRecordContent.m_intMarkStatus = 0;
                    }
                    else
                    {
                        objRecordContent.m_intMarkStatus = Convert.ToInt32(dtbValue.Rows[0]["MARKSTATUS"]);
                    }
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    objRecordContent.m_strREGISTERID_CHR = dtbValue.Rows[0]["REGISTERID_CHR"].ToString();
                    objRecordContent.m_strDISEASESUMMARY = dtbValue.Rows[0]["DISEASESUMMARY"].ToString();
                    objRecordContent.m_strDISEASESUMMARYXML = dtbValue.Rows[0]["DISEASESUMMARYXML"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValue.Rows[0]["DIAGNOSISBEFOREOP"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValue.Rows[0]["DIAGNOSISBEFOREOPXML"].ToString();
                    objRecordContent.m_strDIAGNOSISGIST = dtbValue.Rows[0]["DIAGNOSISGIST"].ToString();
                    objRecordContent.m_strDIAGNOSISGISTXML = dtbValue.Rows[0]["DIAGNOSISGISTXML"].ToString();
                    objRecordContent.m_strOPINDICATION = dtbValue.Rows[0]["OPINDICATION"].ToString();
                    objRecordContent.m_strOPINDICATIONXML = dtbValue.Rows[0]["OPINDICATIONXML"].ToString();
                    objRecordContent.m_strOPMODE = dtbValue.Rows[0]["OPMODE"].ToString();
                    objRecordContent.m_strOPMODEXML = dtbValue.Rows[0]["OPMODEXML"].ToString();
                    objRecordContent.m_strANAMODE = dtbValue.Rows[0]["ANAMODE"].ToString();
                    objRecordContent.m_strANAMODEXML = dtbValue.Rows[0]["ANAMODEXML"].ToString();
                    objRecordContent.m_strPROCEEDING = dtbValue.Rows[0]["PROCEEDING"].ToString();
                    objRecordContent.m_strPROCEEDINGXML = dtbValue.Rows[0]["PROCEEDINGXML"].ToString();
                    objRecordContent.m_strPREPAREBEFOREOP = dtbValue.Rows[0]["PREPAREBEFOREOP"].ToString();
                    objRecordContent.m_strPREPAREBEFOREOPXML = dtbValue.Rows[0]["PREPAREBEFOREOPXML"].ToString();

                    objRecordContent.m_strDISEASESUMMARY_RIGHT = dtbValue.Rows[0]["DISEASESUMMARY_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOP_RIGHT = dtbValue.Rows[0]["DIAGNOSISBEFOREOP_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISGIST_RIGHT = dtbValue.Rows[0]["DIAGNOSISGIST_RIGHT"].ToString();
                    objRecordContent.m_strOPINDICATION_RIGHT = dtbValue.Rows[0]["OPINDICATION_RIGHT"].ToString();
                    objRecordContent.m_strOPMODE_RIGHT = dtbValue.Rows[0]["OPMODE_RIGHT"].ToString();
                    objRecordContent.m_strANAMODE_RIGHT = dtbValue.Rows[0]["ANAMODE_RIGHT"].ToString();
                    objRecordContent.m_strPROCEEDING_RIGHT = dtbValue.Rows[0]["PROCEEDING_RIGHT"].ToString();
                    objRecordContent.m_strPREPAREBEFOREOP_RIGHT = dtbValue.Rows[0]["PREPAREBEFOREOP_RIGHT"].ToString();

                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }

                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 查看是否有相同的记录时间
        /// <summary>
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
                //返回	
                return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 保存记录到数据库。添加主表,添加子表.
        /// <summary>
        /// 保存记录到数据库。添加主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	
                //获取签名流水号
                long lngSequence = 0;
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);
                lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);

                clsEMR_SummaryBeforeOPValue objContent = (clsEMR_SummaryBeforeOPValue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(28, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = 0;
                objDPArr[6].Value = objContent.m_intMarkStatus;
                objDPArr[7].Value = lngSignSequence;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = objContent.m_dtmRECORDDATE;
                objDPArr[9].Value = objContent.m_strREGISTERID_CHR;
                objDPArr[10].Value = objContent.m_strDISEASESUMMARY;
                objDPArr[11].Value = objContent.m_strDISEASESUMMARYXML;
                objDPArr[12].Value = objContent.m_strDIAGNOSISBEFOREOP;
                objDPArr[13].Value = objContent.m_strDIAGNOSISBEFOREOPXML;
                objDPArr[14].Value = objContent.m_strDIAGNOSISGIST;
                objDPArr[15].Value = objContent.m_strDIAGNOSISGISTXML;
                objDPArr[16].Value = objContent.m_strOPINDICATION;
                objDPArr[17].Value = objContent.m_strOPINDICATIONXML;
                objDPArr[18].Value = objContent.m_strOPMODE;
                objDPArr[19].Value = objContent.m_strOPMODEXML;
                objDPArr[20].Value = objContent.m_strANAMODE;
                objDPArr[21].Value = objContent.m_strANAMODEXML;
                objDPArr[22].Value = objContent.m_strPROCEEDING;
                objDPArr[23].Value = objContent.m_strPROCEEDINGXML;
                objDPArr[24].Value = objContent.m_strPREPAREBEFOREOP;
                objDPArr[25].Value = objContent.m_strPREPAREBEFOREOPXML;
                objDPArr[26].Value = lngSequence;
                objDPArr[27].Value = objContent.m_bytIfConfirm;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(16, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = 0;
                objDPArr2[6].Value = objContent.m_strREGISTERID_CHR;
                objDPArr2[7].Value = objContent.m_strDISEASESUMMARY_RIGHT;
                objDPArr2[8].Value = objContent.m_strDIAGNOSISBEFOREOP_RIGHT;
                objDPArr2[9].Value = objContent.m_strDIAGNOSISGIST_RIGHT;
                objDPArr2[10].Value = objContent.m_strOPINDICATION_RIGHT;
                objDPArr2[11].Value = objContent.m_strOPMODE_RIGHT;
                objDPArr2[12].Value = objContent.m_strANAMODE_RIGHT;
                objDPArr2[13].Value = objContent.m_strPROCEEDING_RIGHT;
                objDPArr2[14].Value = objContent.m_strPREPAREBEFOREOP_RIGHT;
                objDPArr2[15].Value = lngSequence;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

                //释放
                objSign = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 查看当前记录是否最新的记录
        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_SummaryBeforeOPValue objContent = p_objRecordContent as clsEMR_SummaryBeforeOPValue;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objContent.m_lngEMR_SEQ;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objContent.m_dtmOpenDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    //if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    //p_objModifyInfo = new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 把新修改的内容保存到数据库
        /// <summary>
        /// 把新修改的内容保存到数据库。更新主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	
                //获取签名流水号
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);

                clsEMR_SummaryBeforeOPValue objContent = (clsEMR_SummaryBeforeOPValue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(20, out objDPArr);
                objDPArr[0].Value = objContent.m_intMarkStatus;
                objDPArr[1].Value = lngSignSequence;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmRECORDDATE;
                objDPArr[3].Value = objContent.m_strDISEASESUMMARY;
                objDPArr[4].Value = objContent.m_strDISEASESUMMARYXML;
                objDPArr[5].Value = objContent.m_strDIAGNOSISBEFOREOP;
                objDPArr[6].Value = objContent.m_strDIAGNOSISBEFOREOPXML;
                objDPArr[7].Value = objContent.m_strDIAGNOSISGIST;
                objDPArr[8].Value = objContent.m_strDIAGNOSISGISTXML;
                objDPArr[9].Value = objContent.m_strOPINDICATION;
                objDPArr[10].Value = objContent.m_strOPINDICATIONXML;
                objDPArr[11].Value = objContent.m_strOPMODE;
                objDPArr[12].Value = objContent.m_strOPMODEXML;
                objDPArr[13].Value = objContent.m_strANAMODE;
                objDPArr[14].Value = objContent.m_strANAMODEXML;
                objDPArr[15].Value = objContent.m_strPROCEEDING;
                objDPArr[16].Value = objContent.m_strPROCEEDINGXML;
                objDPArr[17].Value = objContent.m_strPREPAREBEFOREOP;
                objDPArr[18].Value = objContent.m_strPREPAREBEFOREOPXML;
                objDPArr[19].Value = objContent.m_lngEMR_SEQ;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);

                //设置旧记录状态为2
                IDataParameter[] objDPArrStatus = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArrStatus);
                objDPArrStatus[0].Value = objContent.m_lngEMR_SEQ;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strSetOldRecordSQL, ref lngEff, objDPArrStatus);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(16, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = 0;
                objDPArr2[6].Value = objContent.m_strREGISTERID_CHR;
                objDPArr2[7].Value = objContent.m_strDISEASESUMMARY_RIGHT;
                objDPArr2[8].Value = objContent.m_strDIAGNOSISBEFOREOP_RIGHT;
                objDPArr2[9].Value = objContent.m_strDIAGNOSISGIST_RIGHT;
                objDPArr2[10].Value = objContent.m_strOPINDICATION_RIGHT;
                objDPArr2[11].Value = objContent.m_strOPMODE_RIGHT;
                objDPArr2[12].Value = objContent.m_strANAMODE_RIGHT;
                objDPArr2[13].Value = objContent.m_strPROCEEDING_RIGHT;
                objDPArr2[14].Value = objContent.m_strPREPAREBEFOREOP_RIGHT;
                objDPArr2[15].Value = objContent.m_lngEMR_SEQ;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);


                //释放
                objSign = null;
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

        #region 把记录从数据中“删除”
        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                clsEMR_SummaryBeforeOPValue objContent = p_objRecordContent as clsEMR_SummaryBeforeOPValue;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objContent.m_lngEMR_SEQ;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//返回
            return lngRes;
        }
        #endregion

        #region 获取数据库中最新的修改时间和首次打印时间
        /// <summary>
        /// 获取数据库中最新的修改时间和首次打印时间
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate">修改时间</param>
        /// <param name="p_strFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion

        #region 获取指定已经被删除记录的内容
        /// <summary>
        /// 获取指定已经被删除记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsEMR_SummaryBeforeOPValue objRecordContent = new clsEMR_SummaryBeforeOPValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    if (dtbValue.Rows[0]["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
                    if (dtbValue.Rows[0]["MARKSTATUS"] == DBNull.Value)
                    {
                        objRecordContent.m_intMarkStatus = 0;
                    }
                    else
                    {
                        objRecordContent.m_intMarkStatus = Convert.ToInt32(dtbValue.Rows[0]["MARKSTATUS"]);
                    }
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    objRecordContent.m_strREGISTERID_CHR = dtbValue.Rows[0]["REGISTERID_CHR"].ToString();
                    objRecordContent.m_strDISEASESUMMARY = dtbValue.Rows[0]["DISEASESUMMARY"].ToString();
                    objRecordContent.m_strDISEASESUMMARYXML = dtbValue.Rows[0]["DISEASESUMMARYXML"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValue.Rows[0]["DIAGNOSISBEFOREOP"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValue.Rows[0]["DIAGNOSISBEFOREOPXML"].ToString();
                    objRecordContent.m_strDIAGNOSISGIST = dtbValue.Rows[0]["DIAGNOSISGIST"].ToString();
                    objRecordContent.m_strDIAGNOSISGISTXML = dtbValue.Rows[0]["DIAGNOSISGISTXML"].ToString();
                    objRecordContent.m_strOPINDICATION = dtbValue.Rows[0]["OPINDICATION"].ToString();
                    objRecordContent.m_strOPINDICATIONXML = dtbValue.Rows[0]["OPINDICATIONXML"].ToString();
                    objRecordContent.m_strOPMODE = dtbValue.Rows[0]["OPMODE"].ToString();
                    objRecordContent.m_strOPMODEXML = dtbValue.Rows[0]["OPMODEXML"].ToString();
                    objRecordContent.m_strANAMODE = dtbValue.Rows[0]["ANAMODE"].ToString();
                    objRecordContent.m_strANAMODEXML = dtbValue.Rows[0]["ANAMODEXML"].ToString();
                    objRecordContent.m_strPROCEEDING = dtbValue.Rows[0]["PROCEEDING"].ToString();
                    objRecordContent.m_strPROCEEDINGXML = dtbValue.Rows[0]["PROCEEDINGXML"].ToString();
                    objRecordContent.m_strPREPAREBEFOREOP = dtbValue.Rows[0]["PREPAREBEFOREOP"].ToString();
                    objRecordContent.m_strPREPAREBEFOREOPXML = dtbValue.Rows[0]["PREPAREBEFOREOPXML"].ToString();

                    objRecordContent.m_strDISEASESUMMARY_RIGHT = dtbValue.Rows[0]["DISEASESUMMARY_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOP_RIGHT = dtbValue.Rows[0]["DIAGNOSISBEFOREOP_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISGIST_RIGHT = dtbValue.Rows[0]["DIAGNOSISGIST_RIGHT"].ToString();
                    objRecordContent.m_strOPINDICATION_RIGHT = dtbValue.Rows[0]["OPINDICATION_RIGHT"].ToString();
                    objRecordContent.m_strOPMODE_RIGHT = dtbValue.Rows[0]["OPMODE_RIGHT"].ToString();
                    objRecordContent.m_strANAMODE_RIGHT = dtbValue.Rows[0]["ANAMODE_RIGHT"].ToString();
                    objRecordContent.m_strPROCEEDING_RIGHT = dtbValue.Rows[0]["PROCEEDING_RIGHT"].ToString();
                    objRecordContent.m_strPREPAREBEFOREOP_RIGHT = dtbValue.Rows[0]["PREPAREBEFOREOP_RIGHT"].ToString();

                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }

                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        #endregion
    }
}
