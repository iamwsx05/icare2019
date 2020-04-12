using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 手术器械、敷料点数表(新)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_OPInstrumentService : clsDiseaseTrackService
    {
        #region SQL语句

        #region  获取指定病人的所有未删除记录的时间
        /// <summary>
        /// 获取指定病人的所有未删除记录的时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select distinct createdate,opendate 
													from t_emr_opinstrument 
													where inpatientid = ?
													 and inpatientdate= ?
													 and status=0";
        #endregion

        #region 查找表单内容
        /// <summary>
        /// 查找表单内容
        /// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.modifydate,
       a.modifyuserid,
       a.emr_seq,
       a.instrumentid,
       a.beforeop,
       a.beforeclose,
       a.afterclose,
       a.sequence_int,
       a.recorddate,
       b.instrumentid dict_instrumentid,
       b.instrumentname dict_instrumentname,
       b.orderid dict_orderid,
       b.status dict_status
  from t_emr_opinstrument_dict b
  left outer join (select inpatientid,
                          inpatientdate,
                          opendate,
                          createdate,
                          createuserid,
                          firstprintdate,
                          deactiveddate,
                          deactivedoperatorid,
                          status,
                          modifydate,
                          modifyuserid,
                          emr_seq,
                          instrumentid,
                          beforeop,
                          beforeclose,
                          afterclose,
                          sequence_int,
                          recorddate
                     from t_emr_opinstrument
                    where inpatientid = ?
                      and inpatientdate = ?
                      and opendate = ?
                      and status = 0) a on a.instrumentid = b.instrumentid
 where b.status = 0
 order by b.orderid";
        #endregion

        #region 获取指定时间的表单
        /// <summary>
        /// 获取指定时间的表单
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select distinct createuserid,opendate
														from t_emr_opinstrument
														where emr_seq = ? 
														and status=0";
        #endregion

        #region 获取指定表单的最后修改时间
        /// <summary>
        /// 获取指定表单的最后修改时间
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select distinct a.modifydate, a.modifyuserid
                                                              from t_emr_opinstrument a
                                                             where emr_seq = ?
                                                               and a.status = 0";
        #endregion

        #region 获取删除表单的主要信息
        /// <summary>
        /// 获取删除表单的主要信息
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select distinct deactiveddate,deactivedoperatorid 
														from t_emr_opinstrument 
														where inpatientid = ?
														and inpatientdate= ?
														and opendate= ? 
														and status=1 ";
        #endregion

        #region 添加记录
        /// <summary>
        /// 添加记录
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_opinstrument (inpatientid,inpatientdate,opendate,
        createdate,createuserid,status,modifydate,modifyuserid,emr_seq,instrumentid,beforeop,beforeclose,afterclose,sequence_int,recorddate) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?)";
        #endregion

        #region 设置旧记录状态为2
        /// <summary>
        /// 设置旧记录状态为2
        /// </summary>
        private const string c_strSetOldRecordSQL = @"update t_emr_opinstrument set status = 2 where emr_seq = ? and status = 0";
        #endregion

        #region 修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        private const string c_strModifyRecordSQL = c_strAddNewRecordSQL;
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_opinstrument 
													set status=1,deactiveddate=?,deactivedoperatorid=? 
													where emr_seq = ? 
													and status = 0";
        #endregion

        #region 获取LastModifyDate和FirstPrintDate
        /// <summary>
        /// 获取LastModifyDate和FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select distinct a.firstprintdate, a.modifydate
                                                                          from t_emr_opinstrument a
                                                                         where a.inpatientid = ?
                                                                           and a.inpatientdate = ?
                                                                           and a.opendate = ?
                                                                           and a.status = 0";
        #endregion

        #region 更新FirstPrintDate
        /// <summary>
        /// 更新FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_opinstrument 
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
        private const string c_strGetDeleteRecordTimeListSQL = @"select distinct createdate,opendate 
																from t_emr_opinstrument 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";
        #endregion

        #region 获取指定病人的所有已经删除的记录时间
        /// <summary>
        /// 获取指定病人的所有已经删除的记录时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select distinct createdate,opendate 
																from t_emr_opinstrument 
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
       a.status,
       a.modifydate,
       a.modifyuserid,
       a.emr_seq,
       a.instrumentid,
       a.beforeop,
       a.beforeclose,
       a.afterclose,
       a.sequence_int,
       a.recorddate,
       b.instrumentid dict_instrumentid,
       b.instrumentname dict_instrumentname,
       b.orderid dict_orderid,
       b.status dict_status
  from t_emr_opinstrument_dict b
  left outer join (select inpatientid,
                          inpatientdate,
                          opendate,
                          createdate,
                          createuserid,
                          firstprintdate,
                          deactiveddate,
                          deactivedoperatorid,
                          status,
                          modifydate,
                          modifyuserid,
                          emr_seq,
                          instrumentid,
                          beforeop,
                          beforeclose,
                          afterclose,
                          sequence_int,
                          recorddate
                     from t_emr_opinstrument
                    where inpatientid = ?
                      and inpatientdate = ?
                      and opendate = ?
                      and status = 1) a on a.instrumentid = b.instrumentid
 where b.status = 0
 order by b.orderid";
        #endregion

        #region 添加记录到字典表
        /// <summary>
        /// 添加项目到字典表
        /// </summary>
        private const string c_strAddNewToDict = @"insert into t_emr_opinstrument_dict (instrumentid,instrumentname,orderid,status) values (?,?,?,?)"; 
        #endregion

        #region 修改项目名称
        /// <summary>
        /// 修改项目名称
        /// </summary>
        private const string c_strModifyToDict = @"update t_emr_opinstrument_dict set instrumentname = ? where instrumentid = ?"; 
        #endregion

        #region 停用字典表项目
        /// <summary>
        /// 停用字典表项目
        /// </summary>
        private const string c_strDelFromDict = @"update t_emr_opinstrument_dict set status = 1,orderid = ? where instrumentid = ?"; 
        #endregion

        #region 启用字典表项目
        /// <summary>
        /// 启用字典表项目
        /// </summary>
        private const string c_strActiveFromDict = @"update t_emr_opinstrument_dict set status = 0 where instrumentid = ?";
        #endregion

        #region 设置已启用项目顺序
        /// <summary>
        /// 设置已启用项目顺序
        /// </summary>
        private const string c_strSetOrderID = @"update t_emr_opinstrument_dict set orderid = ? where instrumentid = ?"; 
        #endregion

        #region 获取所有字典表项目
        /// <summary>
        /// 获取所有字典表项目
        /// </summary>
        private const string c_strGetAllDictItems = @"select instrumentid, instrumentname, orderid, status
  from t_emr_opinstrument_dict
 order by instrumentname"; 
        #endregion

        #region 获取已启用项目
        /// <summary>
        /// 获取已启用项目
        /// </summary>
        private const string c_strGetActiveDictItems = @"select instrumentid, instrumentname, orderid, status from t_emr_opinstrument_dict where status = 0 order by orderid"; 
        #endregion

        #region 检查字典表是否已有该项目
        /// <summary>
        /// 检查字典表是否已有该项目
        /// </summary>
        private const string c_strCheckSameItem = @"select instrumentid from t_emr_opinstrument_dict where instrumentname = ?"; 
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
                //返回
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	

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

        #region 获取病人的已经被指定删除者删除记录时间列表
        /// <summary>
        /// 获取病人的已经被指定删除者删除记录时间列表。
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strDeleteUserID))
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
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
        /// <param name="objHRPServ"></param>
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate)
                    || string.IsNullOrEmpty(p_strOpenDate) || objHRPServ == null)
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsEMR_OPInstrument objOPInstrument = new clsEMR_OPInstrument();
                    objOPInstrument.m_strInPatientID = p_strInPatientID.Trim();
                    objOPInstrument.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objOPInstrument.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objOPInstrument.m_objOPInstrument = new clsEMR_OPInstrumentItem[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            objOPInstrument.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                            objOPInstrument.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                            objOPInstrument.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                            objOPInstrument.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                            if (dtbValue.Rows[0]["FirstPrintDate"] == DBNull.Value)
                                objOPInstrument.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objOPInstrument.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[0]["FirstPrintDate"]);
                            objOPInstrument.m_bytStatus = 0;
                            objOPInstrument.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
                            if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                            {
                                objOPInstrument.m_lngSign_SEQ = Convert.ToInt64(dtbValue.Rows[0]["SEQUENCE_INT"]);
                            }
                            else
                            {
                                objOPInstrument.m_lngSign_SEQ = -1;
                            }
                            objOPInstrument.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                        }
                        objOPInstrument.m_objOPInstrument[i] = new clsEMR_OPInstrumentItem();
                        objOPInstrument.m_objOPInstrument[i].m_strAfterClose = dtbValue.Rows[i]["AFTERCLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeClose = dtbValue.Rows[i]["BEFORECLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeOP = dtbValue.Rows[i]["BEFOREOP"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo = new clsEMR_OPInstrument_Dict();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_instrumentid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOrderID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_orderid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_strOPInstrumentName =
                            dtbValue.Rows[i]["Dict_instrumentname"].ToString();
                    }
                    if (objOPInstrument.m_lngSign_SEQ != -1)
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        lngRes = objSign.m_lngGetSign(objOPInstrument.m_lngSign_SEQ, out objOPInstrument.objSignerArr);
                        objSign = null;
                    }
                    p_objRecordContent = objOPInstrument;
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

        #region 查看是否有相同的记录时间
        /// <summary>
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OPInstrument objOPInstrument = p_objRecordContent as clsEMR_OPInstrument;
                if (objOPInstrument == null)
                    return -1;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = objOPInstrument.m_lngEMR_SEQ;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

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

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//返回
            return lngRes;
        } 
        #endregion

        #region 保存记录到数据库
        /// <summary>
        /// 保存记录到数据库。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            long lngSequence = 0;
            long lngSignSeq = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsEMR_OPInstrument objContent = p_objRecordContent as clsEMR_OPInstrument;
                if (objContent == null || objContent.m_objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSeq = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSeq.m_lngGetSequenceValue("seq_emr", out lngSequence);
                lngRes = objSeq.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSeq);
                DateTime dtNow = DateTime.Now;

                for (int i = 0; i < objContent.m_objOPInstrument.Length; i++)
                {
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[4].Value = objContent.m_strCreateUserID;
                    objDPArr[5].Value = 0;
                    objDPArr[6].DbType = DbType.DateTime;
                    objDPArr[6].Value = DateTime.Parse(objContent.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[7].Value = objContent.m_strModifyUserID;
                    objDPArr[8].Value = lngSequence;
                    if (objContent.m_objOPInstrument[i].m_objOPInstrumentInfo == null)
                        objDPArr[9].Value = DBNull.Value;
                    else
                        objDPArr[9].Value = objContent.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID;
                    objDPArr[10].Value = objContent.m_objOPInstrument[i].m_strBeforeOP;
                    objDPArr[11].Value = objContent.m_objOPInstrument[i].m_strBeforeClose;
                    objDPArr[12].Value = objContent.m_objOPInstrument[i].m_strAfterClose;
                    objDPArr[13].Value = lngSignSeq;
                    objDPArr[14].DbType = DbType.DateTime;
                    objDPArr[14].Value = DateTime.Parse(objContent.m_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    //执行SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);

                    if (lngRes < 0)
                        return lngRes;
                }
                //保存签名集合
                if (objContent.objSignerArr != null && objContent.objSignerArr.Length > 0)
                {
                    lngRes = objSeq.m_lngAddSign(objContent.objSignerArr, lngSignSeq);
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

        #region 查看当前记录是否最新的记录
        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="objHRPServ"></param>		
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OPInstrument objOPInstrument = p_objRecordContent as clsEMR_OPInstrument;
                if (objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = objOPInstrument.m_lngEMR_SEQ;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    IDataParameter[] objDPArr2 = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr2);

                    objDPArr2[0].Value = objOPInstrument.m_strInPatientID;
                    objDPArr2[1].DbType = DbType.DateTime;
                    objDPArr2[1].Value = objOPInstrument.m_dtmInPatientDate;
                    objDPArr2[2].DbType = DbType.DateTime;
                    objDPArr2[2].Value = objOPInstrument.m_dtmOpenDate;
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr2);

                    if (lngRes > 0 && dtbValue.Rows.Count > 0)
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
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
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
        /// 把新修改的内容保存到数据库
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                long lngSignSeq = -1;
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSeq);
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsEMR_OPInstrument objContent = p_objRecordContent as clsEMR_OPInstrument;

                if (objContent == null || objContent.m_objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArrSetOld = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArrSetOld);
                objDPArrSetOld[0].Value = objContent.m_lngEMR_SEQ;
                long lngEffOld = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strSetOldRecordSQL, ref lngEffOld, objDPArrSetOld);
                if (lngRes < 0)
                    return lngRes;

                for (int i = 0; i < objContent.m_objOPInstrument.Length; i++)
                {
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[4].Value = objContent.m_strCreateUserID;
                    objDPArr[5].Value = 0;
                    objDPArr[6].DbType = DbType.DateTime;
                    objDPArr[6].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[7].Value = objContent.m_strModifyUserID;
                    objDPArr[8].Value = objContent.m_lngEMR_SEQ;
                    if (objContent.m_objOPInstrument[i].m_objOPInstrumentInfo == null)
                        objDPArr[9].Value = DBNull.Value;
                    else
                        objDPArr[9].Value = objContent.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID;
                    objDPArr[10].Value = objContent.m_objOPInstrument[i].m_strBeforeOP;
                    objDPArr[11].Value = objContent.m_objOPInstrument[i].m_strBeforeClose;
                    objDPArr[12].Value = objContent.m_objOPInstrument[i].m_strAfterClose;
                    objDPArr[13].Value = lngSignSeq;
                    objDPArr[14].DbType = DbType.DateTime;
                    objDPArr[14].Value = DateTime.Parse(objContent.m_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    //执行SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);

                    if (lngRes < 0)
                        return lngRes;
                }
                //保存签名集合
                if (objContent.objSignerArr != null && objContent.objSignerArr.Length > 0)
                {
                    lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSeq);
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

        #region 把记录从数据中删除
        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OPInstrument objOPInstrument = p_objRecordContent as clsEMR_OPInstrument;
                if (objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objOPInstrument.m_dtmDeActivedDate;
                objDPArr[1].Value = objOPInstrument.m_strDeActivedOperatorID;
                objDPArr[2].Value = objOPInstrument.m_lngEMR_SEQ;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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
        /// <param name="objHRPServ"></param>
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || objHRPServ == null)
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //返回
                return lngRes;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//返回
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
        /// <param name="objHRPServ"></param>
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || objHRPServ == null)
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsEMR_OPInstrument objOPInstrument = new clsEMR_OPInstrument();
                    objOPInstrument.m_strInPatientID = p_strInPatientID.Trim();
                    objOPInstrument.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objOPInstrument.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objOPInstrument.m_objOPInstrument = new clsEMR_OPInstrumentItem[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            objOPInstrument.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                            objOPInstrument.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                            objOPInstrument.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                            objOPInstrument.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                            if (dtbValue.Rows[0]["FirstPrintDate"] == DBNull.Value)
                                objOPInstrument.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objOPInstrument.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[0]["FirstPrintDate"]);
                            objOPInstrument.m_bytStatus = 0;
                            objOPInstrument.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
                            if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                            {
                                objOPInstrument.m_lngSign_SEQ = Convert.ToInt64(dtbValue.Rows[0]["SEQUENCE_INT"]);
                            }
                            else
                            {
                                objOPInstrument.m_lngSign_SEQ = -1;
                            } 
                            objOPInstrument.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                        }
                        objOPInstrument.m_objOPInstrument[i] = new clsEMR_OPInstrumentItem();
                        objOPInstrument.m_objOPInstrument[i].m_strAfterClose = dtbValue.Rows[i]["AFTERCLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeClose = dtbValue.Rows[i]["BEFORECLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeOP = dtbValue.Rows[i]["BEFOREOP"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo = new clsEMR_OPInstrument_Dict();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_instrumentid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOrderID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_orderid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_strOPInstrumentName =
                            dtbValue.Rows[i]["Dict_instrumentname"].ToString();
                    }
                    if (objOPInstrument.m_lngSign_SEQ != -1)
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        lngRes = objSign.m_lngGetSign(objOPInstrument.m_lngSign_SEQ, out objOPInstrument.objSignerArr);
                        objSign = null;
                    }
                    p_objRecordContent = objOPInstrument;
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

        #region 添加项目至字典表
        /// <summary>
        /// 添加项目至字典表
        /// </summary>
        /// <param name="p_strItemName">项目名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewToDict(string p_strItemName)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                if (string.IsNullOrEmpty(p_strItemName))
                    return (long)enmOperationResult.Parameter_Error;

                int intOPInstrumentID = 0;
                lngRes = objHRPServ.m_lngGenerateNewID("T_EMR_OPINSTRUMENT_DICT", "INSTRUMENTID", out intOPInstrumentID);
                
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);

                objDPArr[0].Value = intOPInstrumentID;
                objDPArr[1].Value = p_strItemName.Trim();
                objDPArr[2].Value = DBNull.Value;
                objDPArr[3].Value = 1;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewToDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 修改字典表项目
        /// <summary>
        /// 修改字典表项目
        /// </summary>
        /// <param name="p_intOPInstrumentID"></param>
        /// <param name="p_strOPInstrumentName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyToDisc(int p_intOPInstrumentID, string p_strOPInstrumentName)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                if (string.IsNullOrEmpty(p_strOPInstrumentName))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strOPInstrumentName.Trim();
                objDPArr[1].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyToDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 停用字典表项目
        /// <summary>
        /// 停用字典表项目
        /// </summary>
        /// <param name="p_intOPInstrumentID">项目ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeActiveItemFromDict(int p_intOPInstrumentID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = DBNull.Value;
                objDPArr[1].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDelFromDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 启用字典表项目
        /// <summary>
        /// 启用字典表项目
        /// </summary>
        /// <param name="p_intOPInstrumentID">项目ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngActiveItemFromDict(int p_intOPInstrumentID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strActiveFromDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 设置已启用项目顺序
        /// <summary>
        /// 设置已启用项目顺序
        /// </summary>
        /// <param name="p_intOPInstrumentID">项目ID</param>
        /// <param name="p_intOrderID">顺序号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderID(int p_intOPInstrumentID, int p_intOrderID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_intOrderID;
                objDPArr[1].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strSetOrderID, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 获取所有字典表项目
        /// <summary>
        /// 获取所有字典表项目
        /// </summary>
        /// <param name="p_obDictItems">所有项目</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllItemsFromDict(out clsEMR_OPInstrument_Dict[] p_obDictItems)
        {
            p_obDictItems = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.DoGetDataTable(c_strGetAllDictItems,ref dtbValue);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_obDictItems = new clsEMR_OPInstrument_Dict[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_obDictItems[i] = new clsEMR_OPInstrument_Dict();
                        p_obDictItems[i].m_intOPInstrumentID = Convert.ToInt32(dtbValue.Rows[i]["INSTRUMENTID"]);
                        if(dtbValue.Rows[i]["ORDERID"] != DBNull.Value)
                            p_obDictItems[i].m_intOrderID = Convert.ToInt32(dtbValue.Rows[i]["ORDERID"]);
                        p_obDictItems[i].m_intStatus = Convert.ToInt32(dtbValue.Rows[i]["STATUS"]);
                        p_obDictItems[i].m_strOPInstrumentName = dtbValue.Rows[i]["INSTRUMENTNAME"].ToString();
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

        #region 获取已启用字典表项目
        /// <summary>
        /// 获取已启用字典表项目
        /// </summary>
        /// <param name="p_obDictItems">已启用项目</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetActiveItemsFromDict(out clsEMR_OPInstrument_Dict[] p_obDictItems)
        {
            p_obDictItems = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.DoGetDataTable(c_strGetActiveDictItems, ref dtbValue);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_obDictItems = new clsEMR_OPInstrument_Dict[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_obDictItems[i] = new clsEMR_OPInstrument_Dict();
                        p_obDictItems[i].m_intOPInstrumentID = Convert.ToInt32(dtbValue.Rows[i]["INSTRUMENTID"]);
                        if (dtbValue.Rows[i]["ORDERID"] != DBNull.Value)
                            p_obDictItems[i].m_intOrderID = Convert.ToInt32(dtbValue.Rows[i]["ORDERID"]);
                        p_obDictItems[i].m_intStatus = Convert.ToInt32(dtbValue.Rows[i]["STATUS"]);
                        p_obDictItems[i].m_strOPInstrumentName = dtbValue.Rows[i]["INSTRUMENTNAME"].ToString();
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

        #region 检查字典表是否已有该项目
        /// <summary>
        /// 检查字典表是否已有该项目
        /// </summary>
        /// <param name="p_strOPInstrumentName">项目名称</param>
        /// <param name="p_intOPInstrumentID">项目ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckSameItemID(string p_strOPInstrumentName,out int p_intOPInstrumentID)
        {
            long lngRes = 0;
            p_intOPInstrumentID = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if(string.IsNullOrEmpty(p_strOPInstrumentName))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strOPInstrumentName.Trim();

                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckSameItem, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_intOPInstrumentID = Convert.ToInt32(dtbValue.Rows[0][0]);
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
    }
}