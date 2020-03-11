using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.InPatientCaseHistoryServ
{
    /// <summary>
    /// 中期妊娠三合一
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsGestationMisbirthsthreeRecService : clsDiseaseTrackService
    {
        #region SQL语句
        /// <summary>
        /// 从gestationmisbirthsthree_record获取指定病人的所有没有删除记录的时间。
        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createdate, opendate
														from gestationmisbirthsthree_record
													where inpatientid = ? and inpatientdate = ?
														and status = 0";

        /// <summary>
        /// 从gestationmisbirthsthree_record中获取指定时间的表单,获取已经存在记录的主要信息
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
															from gestationmisbirthsthree_record
														where inpatientid = ? and inpatientdate = ?
															and createdate = ?
																and status = 0";

        /// <summary>
        /// 从gestationmisbirthsthree_record获取删除表单的主要信息。
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from gestationmisbirthsthree_record
														where inpatientid = ? and inpatientdate = ?
															and opendate = ?
															and status = 1";

        /// <summary>
        /// 从ICUNURSERECORD_GXRECORD获取指定病人的所有指定删除者删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate, opendate
																	from gestationmisbirthsthree_record
																where inpatientid = ? and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

        /// <summary>
        /// 从ICUNURSERECORD_GXRECORD获取指定病人的所有已经删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate, opendate
																		from gestationmisbirthsthree_record
																	where inpatientid = ? and inpatientdate = ?
																		and status = 1";
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

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
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
            }
            finally
            {
                //objHRPServ.Dispose();

            }
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

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
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

            finally
            {
                //objHRPServ.Dispose();

            }
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

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
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

            finally
            {
                //objHRPServ.Dispose();

            }
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

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            #region sql
            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" registerid_chr,inpatientid,
       inpatientdate,
       bloodpressure_vchr,
       bloodpressure_xml,
       temperature_vchr,
       temperature_xml,
       pulse_vchr,
       pulse_xml,
       contractions_vchr,
       contractions_xml,
       bleeding_vchr,
       bleeding_xml,
       brokenwater_vchr,
       brokenwater_xml,
       fetal_vchr,
       fetal_xml,
       miyaguchisize_vchr,
       miyaguchisize_xml,
       modifydate,
       modifyuserid,
       status,
       opendate,
       deactiveddate,
       deactivedoperatorid,
       createuserid,
       createdate,
       signuserid,
       signusername,
       recorddate
  from gestationmisbirthsthree_record
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
 order by modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;
            #endregion
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsGestationMisbirthsthreeVO objRecordContent = new clsGestationMisbirthsthreeVO();
                    objRecordContent.m_strInPatientID = dtbValue.Rows[0]["inpatientid"].ToString();
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[0]["inpatientdate"]);
                    objRecordContent.m_strBLOODPRESSURE_VCHR = dtbValue.Rows[0]["BLOODPRESSURE_VCHR"].ToString().Trim();
                    objRecordContent.m_strBLOODPRESSURE_XML = dtbValue.Rows[0]["BLOODPRESSURE_XML"].ToString().Trim();
                    objRecordContent.m_strTEMPERATURE_VCHR = dtbValue.Rows[0]["TEMPERATURE_VCHR"].ToString().Trim();
                    objRecordContent.m_strTEMPERATURE_XML = dtbValue.Rows[0]["TEMPERATURE_XML"].ToString().Trim();
                    objRecordContent.m_strPULSE_VCHR = dtbValue.Rows[0]["PULSE_VCHR"].ToString().Trim();
                    objRecordContent.m_strPULSE_XML = dtbValue.Rows[0]["PULSE_XML"].ToString().Trim();
                    objRecordContent.m_strCONTRACTIONS_VCHR = dtbValue.Rows[0]["CONTRACTIONS_VCHR"].ToString().Trim();
                    objRecordContent.m_strCONTRACTIONS_XML = dtbValue.Rows[0]["CONTRACTIONS_XML"].ToString().Trim();
                    objRecordContent.m_strBLEEDING_VCHR = dtbValue.Rows[0]["BLEEDING_VCHR"].ToString().Trim();
                    objRecordContent.m_strBLEEDING_XML = dtbValue.Rows[0]["BLEEDING_XML"].ToString().Trim();
                    objRecordContent.m_strBROKENWATER_VCHR = dtbValue.Rows[0]["BROKENWATER_VCHR"].ToString().Trim();
                    objRecordContent.m_strBROKENWATER_XML = dtbValue.Rows[0]["BROKENWATER_XML"].ToString().Trim();
                    objRecordContent.m_strFETAL_VCHR = dtbValue.Rows[0]["FETAL_VCHR"].ToString().Trim();
                    objRecordContent.m_strFETAL_XML = dtbValue.Rows[0]["FETAL_XML"].ToString().Trim();
                    objRecordContent.m_strMIYAGUCHISIZE_VCHR = dtbValue.Rows[0]["MIYAGUCHISIZE_VCHR"].ToString().Trim();
                    objRecordContent.m_strMIYAGUCHISIZE_XML = dtbValue.Rows[0]["MIYAGUCHISIZE_XML"].ToString().Trim();
                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_strSignUserID = dtbValue.Rows[0]["SIGNUSERID"].ToString();
                    objRecordContent.m_strSignUserName = dtbValue.Rows[0]["SIGNUSERNAME"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    p_objRecordContent = objRecordContent;
                    #endregion
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
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

            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
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
            }

            finally
            {
                //objHRPServ.Dispose();

            }
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
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            clsGestationMisbirthsthreeVO objRecordContent = (clsGestationMisbirthsthreeVO)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
            /// </summary>
            string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" modifydate,modifyuserid from gestationmisbirthsthree_record
			where status =0	and inpatientid = ? and inpatientdate = ? and opendate = ? order by modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (objRecordContent.m_dtmModifyDate == Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]))
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

            finally
            {
                //objHRPServ.Dispose();

            }
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
            return lngRes;
        }

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
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            string c_strGetDeleteRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" registerid_chr,inpatientid,
       inpatientdate,
       bloodpressure_vchr,
       bloodpressure_xml,
       temperature_vchr,
       temperature_xml,
       pulse_vchr,
       pulse_xml,
       contractions_vchr,
       contractions_xml,
       bleeding_vchr,
       bleeding_xml,
       brokenwater_vchr,
       brokenwater_xml,
       fetal_vchr,
       fetal_xml,
       miyaguchisize_vchr,
       miyaguchisize_xml,
       modifydate,
       modifyuserid,
       status,
       opendate,
       deactiveddate,
       deactivedoperatorid,
       createuserid,
       createdate,
       signuserid,
       signusername,
       recorddate
  from gestationmisbirthsthree_record
 where status = 1
   and inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
 order by modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
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
                objHRPServ.Dispose();
                objHRPServ = null;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsGestationMisbirthsthreeVO objRecordContent = new clsGestationMisbirthsthreeVO();
                    objRecordContent.m_strInPatientID = dtbValue.Rows[0]["inpatientid"].ToString();
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[0]["inpatientdate"]);
                    objRecordContent.m_strBLOODPRESSURE_VCHR = dtbValue.Rows[0]["BLOODPRESSURE_VCHR"].ToString().Trim();
                    objRecordContent.m_strBLOODPRESSURE_XML = dtbValue.Rows[0]["BLOODPRESSURE_XML"].ToString().Trim();
                    objRecordContent.m_strTEMPERATURE_VCHR = dtbValue.Rows[0]["TEMPERATURE_VCHR"].ToString().Trim();
                    objRecordContent.m_strTEMPERATURE_XML = dtbValue.Rows[0]["TEMPERATURE_XML"].ToString().Trim();
                    objRecordContent.m_strPULSE_VCHR = dtbValue.Rows[0]["PULSE_VCHR"].ToString().Trim();
                    objRecordContent.m_strPULSE_XML = dtbValue.Rows[0]["PULSE_XML"].ToString().Trim();
                    objRecordContent.m_strCONTRACTIONS_VCHR = dtbValue.Rows[0]["CONTRACTIONS_VCHR"].ToString().Trim();
                    objRecordContent.m_strCONTRACTIONS_XML = dtbValue.Rows[0]["CONTRACTIONS_XML"].ToString().Trim();
                    objRecordContent.m_strBLEEDING_VCHR = dtbValue.Rows[0]["BLEEDING_VCHR"].ToString().Trim();
                    objRecordContent.m_strBLEEDING_XML = dtbValue.Rows[0]["BLEEDING_XML"].ToString().Trim();
                    objRecordContent.m_strBROKENWATER_VCHR = dtbValue.Rows[0]["BROKENWATER_VCHR"].ToString().Trim();
                    objRecordContent.m_strBROKENWATER_XML = dtbValue.Rows[0]["BROKENWATER_XML"].ToString().Trim();
                    objRecordContent.m_strFETAL_VCHR = dtbValue.Rows[0]["FETAL_VCHR"].ToString().Trim();
                    objRecordContent.m_strFETAL_XML = dtbValue.Rows[0]["FETAL_XML"].ToString().Trim();
                    objRecordContent.m_strMIYAGUCHISIZE_VCHR = dtbValue.Rows[0]["MIYAGUCHISIZE_VCHR"].ToString().Trim();
                    objRecordContent.m_strMIYAGUCHISIZE_XML = dtbValue.Rows[0]["MIYAGUCHISIZE_XML"].ToString().Trim();
                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[0]["DEACTIVEDDATE"]);
                    objRecordContent.m_strDeActivedOperatorID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_strSignUserID = dtbValue.Rows[0]["SIGNUSERID"].ToString();
                    objRecordContent.m_strSignUserName = dtbValue.Rows[0]["SIGNUSERNAME"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);

                    p_objRecordContent = objRecordContent;
                    #endregion
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
        #endregion

        #region 获取所有新生儿情况记录
        /// <summary>
        /// 获取所有新生儿情况记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strBirthTime,
            out clsGestationMisbirthsthreeVO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            long lngRes = -1;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            string strGetAllContentSQL = @"select t4.lastname_vchr as createusername, t1.registerid_chr,t1.inpatientid,
       t1.inpatientdate,
       t1.bloodpressure_vchr,
       t1.bloodpressure_xml,
       t1.temperature_vchr,
       t1.temperature_xml,
       t1.pulse_vchr,
       t1.pulse_xml,
       t1.contractions_vchr,
       t1.contractions_xml,
       t1.bleeding_vchr,
       t1.bleeding_xml,
       t1.brokenwater_vchr,
       t1.brokenwater_xml,
       t1.fetal_vchr,
       t1.fetal_xml,
       t1.miyaguchisize_vchr,
       t1.miyaguchisize_xml,
       t1.modifydate,
       t1.modifyuserid,
       t1.status,
       t1.opendate,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.createuserid,
       t1.createdate,
       t1.signuserid,
       t1.signusername,
       t1.recorddate
  from gestationmisbirthsthree_record t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
 order by t1.createdate, modifydate";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//护理记录内容  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetAllContentSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsGestationMisbirthsthreeVO objRecordContent = null;
                    p_objRecordArr = new clsGestationMisbirthsthreeVO[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordContent = new clsGestationMisbirthsthreeVO();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[i]["inpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[i]["inpatientdate"]);
                        objRecordContent.m_strBLOODPRESSURE_VCHR = dtbValue.Rows[i]["BLOODPRESSURE_VCHR"].ToString().Trim();
                        objRecordContent.m_strBLOODPRESSURE_XML = dtbValue.Rows[i]["BLOODPRESSURE_XML"].ToString().Trim();
                        objRecordContent.m_strTEMPERATURE_VCHR = dtbValue.Rows[i]["TEMPERATURE_VCHR"].ToString().Trim();
                        objRecordContent.m_strTEMPERATURE_XML = dtbValue.Rows[i]["TEMPERATURE_XML"].ToString().Trim();
                        objRecordContent.m_strPULSE_VCHR = dtbValue.Rows[i]["PULSE_VCHR"].ToString().Trim();
                        objRecordContent.m_strPULSE_XML = dtbValue.Rows[i]["PULSE_XML"].ToString().Trim();
                        objRecordContent.m_strCONTRACTIONS_VCHR = dtbValue.Rows[i]["CONTRACTIONS_VCHR"].ToString().Trim();
                        objRecordContent.m_strCONTRACTIONS_XML = dtbValue.Rows[i]["CONTRACTIONS_XML"].ToString().Trim();
                        objRecordContent.m_strBLEEDING_VCHR = dtbValue.Rows[i]["BLEEDING_VCHR"].ToString().Trim();
                        objRecordContent.m_strBLEEDING_XML = dtbValue.Rows[i]["BLEEDING_XML"].ToString().Trim();
                        objRecordContent.m_strBROKENWATER_VCHR = dtbValue.Rows[i]["BROKENWATER_VCHR"].ToString().Trim();
                        objRecordContent.m_strBROKENWATER_XML = dtbValue.Rows[i]["BROKENWATER_XML"].ToString().Trim();
                        objRecordContent.m_strFETAL_VCHR = dtbValue.Rows[i]["FETAL_VCHR"].ToString().Trim();
                        objRecordContent.m_strFETAL_XML = dtbValue.Rows[i]["FETAL_XML"].ToString().Trim();
                        objRecordContent.m_strMIYAGUCHISIZE_VCHR = dtbValue.Rows[i]["MIYAGUCHISIZE_VCHR"].ToString().Trim();
                        objRecordContent.m_strMIYAGUCHISIZE_XML = dtbValue.Rows[i]["MIYAGUCHISIZE_XML"].ToString().Trim();
                        objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        objRecordContent.m_strSignUserID = dtbValue.Rows[i]["SIGNUSERID"].ToString();
                        objRecordContent.m_strSignUserName = dtbValue.Rows[i]["SIGNUSERNAME"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

                        p_objRecordArr[i] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region 获取所有最新的新生儿情况记录
        /// <summary>
        /// 获取所有最新的新生儿情况记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllModifiedContent(string p_strInPatientID,
            string p_strInPatientDate,
            out clsGestationMisbirthsthreeVO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            long lngRes = -1;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error; ;
            #region sql
            string strGetAllContentSQL = @"select t4.lastname_vchr as createusername,t1.registerid_chr, t1.inpatientid,
       t1.inpatientdate,
       t1.bloodpressure_vchr,
       t1.bloodpressure_xml,
       t1.temperature_vchr,
       t1.temperature_xml,
       t1.pulse_vchr,
       t1.pulse_xml,
       t1.contractions_vchr,
       t1.contractions_xml,
       t1.bleeding_vchr,
       t1.bleeding_xml,
       t1.brokenwater_vchr,
       t1.brokenwater_xml,
       t1.fetal_vchr,
       t1.fetal_xml,
       t1.miyaguchisize_vchr,
       t1.miyaguchisize_xml,
       t1.modifydate,
       t1.modifyuserid,
       t1.status,
       t1.opendate,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.createuserid,
       t1.createdate,
       t1.signuserid,
       t1.signusername,
       t1.recorddate
  from gestationmisbirthsthree_record t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
   and t1.modifydate = (select max(modifydate)
                          from gestationmisbirthsthree_record
                         where inpatientid = t1.inpatientid
                           and inpatientdate = t1.inpatientdate
                           and createdate = t1.createdate)
 order by t1.createdate, modifydate";
            #endregion
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//护理记录内容  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetAllContentSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsGestationMisbirthsthreeVO objRecordContent = null;
                    p_objRecordArr = new clsGestationMisbirthsthreeVO[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordContent = new clsGestationMisbirthsthreeVO();
                        objRecordContent.m_strInPatientID = dtbValue.Rows[i]["inpatientid"].ToString();
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[i]["inpatientdate"]);
                        objRecordContent.m_strBLOODPRESSURE_VCHR = dtbValue.Rows[i]["BLOODPRESSURE_VCHR"].ToString().Trim();
                        objRecordContent.m_strBLOODPRESSURE_XML = dtbValue.Rows[i]["BLOODPRESSURE_XML"].ToString().Trim();
                        objRecordContent.m_strTEMPERATURE_VCHR = dtbValue.Rows[i]["TEMPERATURE_VCHR"].ToString().Trim();
                        objRecordContent.m_strTEMPERATURE_XML = dtbValue.Rows[i]["TEMPERATURE_XML"].ToString().Trim();
                        objRecordContent.m_strPULSE_VCHR = dtbValue.Rows[i]["PULSE_VCHR"].ToString().Trim();
                        objRecordContent.m_strPULSE_XML = dtbValue.Rows[i]["PULSE_XML"].ToString().Trim();
                        objRecordContent.m_strCONTRACTIONS_VCHR = dtbValue.Rows[i]["CONTRACTIONS_VCHR"].ToString().Trim();
                        objRecordContent.m_strCONTRACTIONS_XML = dtbValue.Rows[i]["CONTRACTIONS_XML"].ToString().Trim();
                        objRecordContent.m_strBLEEDING_VCHR = dtbValue.Rows[i]["BLEEDING_VCHR"].ToString().Trim();
                        objRecordContent.m_strBLEEDING_XML = dtbValue.Rows[i]["BLEEDING_XML"].ToString().Trim();
                        objRecordContent.m_strBROKENWATER_VCHR = dtbValue.Rows[i]["BROKENWATER_VCHR"].ToString().Trim();
                        objRecordContent.m_strBROKENWATER_XML = dtbValue.Rows[i]["BROKENWATER_XML"].ToString().Trim();
                        objRecordContent.m_strFETAL_VCHR = dtbValue.Rows[i]["FETAL_VCHR"].ToString().Trim();
                        objRecordContent.m_strFETAL_XML = dtbValue.Rows[i]["FETAL_XML"].ToString().Trim();
                        objRecordContent.m_strMIYAGUCHISIZE_VCHR = dtbValue.Rows[i]["MIYAGUCHISIZE_VCHR"].ToString().Trim();
                        objRecordContent.m_strMIYAGUCHISIZE_XML = dtbValue.Rows[i]["MIYAGUCHISIZE_XML"].ToString().Trim();
                        objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        objRecordContent.m_strSignUserID = dtbValue.Rows[i]["SIGNUSERID"].ToString();
                        objRecordContent.m_strSignUserName = dtbValue.Rows[i]["SIGNUSERNAME"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

                        p_objRecordArr[i] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region SQL语句
        /// <summary>
        /// 添加记录到gestationmisbirthsthree_record
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into gestationmisbirthsthree_record
  (inpatientid,
   inpatientdate,
   bloodpressure_vchr,
   bloodpressure_xml,
   temperature_vchr,
   temperature_xml,
   pulse_vchr,
   pulse_xml,
   contractions_vchr,
   contractions_xml,
   bleeding_vchr,
   bleeding_xml,
   brokenwater_vchr,
   brokenwater_xml,
   fetal_vchr,
   fetal_xml,
   miyaguchisize_vchr,
   miyaguchisize_xml,
   modifydate,
   modifyuserid,
   status,
   opendate,
   createuserid,
   createdate,
   signuserid,
   signusername,
   recorddate,registerid_chr)
values 
(
?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 修改记录到gestationmisbirthsthree_record
        /// </summary>
        private const string c_strModifyRecordSQL = c_strAddNewRecordSQL;


        /// <summary>
        /// 设置ICUNURSERECORD_GXRECORD中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update gestationmisbirthsthree_record
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ? and inpatientdate = ?
														and opendate = ?
														and status = 0";

        /// <summary>
        /// 更新ICUNURSERECORD_GXRECORD中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update gestationmisbirthsthree_record
																set firstprintdate = ?
															where inpatientid = ? and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";
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
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            long lngRes = 0;
            return lngRes;
        }
        #endregion

        #region 保存记录到数据库
        /// <summary>
        /// 保存记录到数据库。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsGestationMisbirthsthreeVO p_objRecord = (clsGestationMisbirthsthreeVO)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(28, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_objRecord.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = p_objRecord.m_dtmInPatientDate;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strBLOODPRESSURE_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strBLOODPRESSURE_XML;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strTEMPERATURE_VCHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strTEMPERATURE_XML;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPULSE_VCHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strPULSE_XML;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strCONTRACTIONS_VCHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strCONTRACTIONS_XML;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strBLEEDING_VCHR;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strBLEEDING_XML;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strBROKENWATER_VCHR;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strBROKENWATER_XML;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strFETAL_VCHR;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strFETAL_XML;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strMIYAGUCHISIZE_VCHR;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strMIYAGUCHISIZE_XML;
                objLisAddItemRefArr[18].DbType = DbType.DateTime;
                objLisAddItemRefArr[18].Value = p_objRecord.m_dtmModifyDate;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strModifyUserID;
                objLisAddItemRefArr[20].Value = 0;
                objLisAddItemRefArr[21].DbType = DbType.DateTime;
                objLisAddItemRefArr[21].Value = p_objRecord.m_dtmOpenDate;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strCreateUserID;
                objLisAddItemRefArr[23].DbType = DbType.DateTime;
                objLisAddItemRefArr[23].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strSignUserID;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strSignUserName;
                objLisAddItemRefArr[26].DbType = DbType.DateTime;
                objLisAddItemRefArr[26].Value = p_objRecord.m_dtmRecordDate;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strRegisterID;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
        #endregion

        #region 把新修改的内容保存到数据库
        /// <summary>
        /// 把新修改的内容保存到数据库。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            clsGestationMisbirthsthreeVO p_objRecord = (clsGestationMisbirthsthreeVO)p_objRecordContent;
            long lngRes = 0;


            try
            {

                m_lngAddNewRecord2DB(p_objRecord, p_objHRPServ);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region 把记录从数据中“删除”
        /// <summary>
        /// 把记录从数据中“删除”(供外部直接调用)
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteCircsRecord(clsTrackRecordContent p_objRecordContent)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, objHRPServ);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
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
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            clsGestationMisbirthsthreeVO objRecordContent = (clsGestationMisbirthsthreeVO)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
        }
        #endregion
    }
}
