using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 出入量登记表总结
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_IntakeAndOutputVolumeSumServ : clsDiseaseTrackService
    {
        #region SQL语句
        /// <summary>
        /// 从T_EMR_INTAKEANDOUTPUT_SUM获取指定病人的所有没有删除记录的时间。

        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_intakeandoutput_sum t
 where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// 从T_EMR_INTAKEANDOUTPUT_SUM中获取指定时间的表单,获取已经存在记录的主要信息

        /// InPatientID ,InPatientDate ,CreateDate,Status = 1
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_intakeandoutput_sum t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// 从T_EMR_INTAKEANDOUTPUT_SUM获取删除表单的主要信息。

        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_intakeandoutput_sum t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// 添加记录到T_EMR_INTAKEANDOUTPUT_SUM
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_intakeandoutput_sum 
(registerid_chr,createdate_dat,createuserid_chr,status_int,recorduserid_vchr,recorddate_dat,sequence_int,allurine,
allurinexml,alloutput,alloutputxml,specificgravity,specificgravityxml,allintake,allintakexml,modifydate,modifyuserid,markstatus) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 修改记录到T_EMR_INTAKEANDOUTPUT_SUM
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_intakeandoutput_sum  set
recorduserid_vchr= ?,recorddate_dat= ?,sequence_int= ?,allurine= ?,allurinexml= ?,alloutput= ?,alloutputxml= ?,
specificgravity= ?,specificgravityxml= ?,allintake= ?,allintakexml= ?,modifydate= ?,modifyuserid= ?,markstatus= ?
where registerid_chr = ?
  and createdate_dat = ?
  and status_int = 1";//33个参数


        /// <summary>
        /// 设置T_EMR_INTAKEANDOUTPUT_SUM中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_intakeandoutput_sum t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";


        /// <summary>
        /// 从T_EMR_INTAKEANDOUTPUT_SUM获取指定病人的所有指定删除者删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_intakeandoutput_sum t
 where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// 从T_EMR_INTAKEANDOUTPUT_SUM获取指定病人的所有已经删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_intakeandoutput_sum t
 where t.registerid_chr = ?
   and t.status_int = 0";
        #endregion

        #region 获取病人的该记录时间列表
        /// <summary>
        /// 根据住院登记号获取病人的该记录时间列表

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间数组</param>
        /// <param name="p_strRecordDateArr">界面记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strRegisterId, out string[] p_strCreateDateArr, out string[] p_strRecordDateArr)
        {
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	
            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strRecordDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        /// 获取病人的已经被某用户删除记录时间列表

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strDeleteUserID">删除操作者ID</param>
        /// <param name="p_strRecordTimeArr">用户填写的记录时间数组</param>
        /// <param name="p_strCreatedDateArr">系统生成的记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strRegisterId,
            string p_strDeleteUserID,
            out string[] p_strRecordTimeArr,
            out string[] p_strCreatedDateArr)
        {
            p_strRecordTimeArr = null;
            p_strCreatedDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strDeleteUserID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strCreatedDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreatedDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        /// 获取病人的已经被删除记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间</param>
        /// <param name="p_strRecordTimeArr">界面的记录时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strRegisterId,
            out string[] p_strCreateDateArr,
            out string[] p_strRecordTimeArr)
        {
            p_strCreateDateArr = null;
            p_strRecordTimeArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeListAll");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strRecordTimeArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取指定记录的内容

        /// <summary>
        /// 根据住院登记号获取指定记录的内容。

        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCteatedDate">创建时间</param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strRegisterID, string p_strRecordDate, out clsTrackRecordContent p_objContent)
        {
            p_objContent = null;
            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = @"select registerid_chr,
       createdate_dat,
       createuserid_chr,
       status_int,
       deactiveddate_dat,
       deactivedoperatorid_chr,
       recorduserid_vchr,
       recorddate_dat,
       sequence_int,
       allurine,
       allurinexml,
       alloutput,
       alloutputxml,
       specificgravity,
       specificgravityxml,
       allintake,
       allintakexml,
       modifydate,
       modifyuserid,
       markstatus
  from t_emr_intakeandoutput_sum
 where status_int = 1
   and registerid_chr = ?
   and createdate_dat = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_strRecordDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsEMR_IntakeAndOutputVolumeSum objRecordContent = new clsEMR_IntakeAndOutputVolumeSum();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = p_strRegisterID;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strALLURINE = dtrSelected["ALLURINE"].ToString();
                    objRecordContent.m_strALLURINEXML = dtrSelected["ALLURINEXML"].ToString();
                    objRecordContent.m_strALLOUTPUT = dtrSelected["ALLOUTPUT"].ToString();
                    objRecordContent.m_strALLOUTPUTXML = dtrSelected["ALLOUTPUTXML"].ToString();
                    objRecordContent.m_strSPECIFICGRAVITY = dtrSelected["SPECIFICGRAVITY"].ToString();
                    objRecordContent.m_strSPECIFICGRAVITYXML = dtrSelected["SPECIFICGRAVITYXML"].ToString();
                    objRecordContent.m_strALLINTAKE = dtrSelected["ALLINTAKE"].ToString();
                    objRecordContent.m_strALLINTAKEXML = dtrSelected["ALLINTAKEXML"].ToString();
                    objRecordContent.m_intMarkStatus = Convert.ToInt32(dtrSelected["MARKSTATUS"]);

                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
                    p_objContent = objRecordContent;
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
                objHRPServ = null;
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

            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
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
                objHRPServ = null;
            }
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
            //检查参数                              
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            clsEMR_IntakeAndOutputVolumeSum objRecordContent = (clsEMR_IntakeAndOutputVolumeSum)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region 赋值

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[2].Value = objRecordContent.m_strCreateUserID;
                objDPArr[3].Value = 1;
                objDPArr[4].Value = objRecordContent.m_strRecordUserID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[6].Value = lngSequence;
                objDPArr[7].Value = objRecordContent.m_strALLURINE;
                objDPArr[8].Value = objRecordContent.m_strALLURINEXML;
                objDPArr[9].Value = objRecordContent.m_strALLOUTPUT;
                objDPArr[10].Value = objRecordContent.m_strALLOUTPUTXML;
                objDPArr[11].Value = objRecordContent.m_strSPECIFICGRAVITY;
                objDPArr[12].Value = objRecordContent.m_strSPECIFICGRAVITYXML;
                objDPArr[13].Value = objRecordContent.m_strALLINTAKE;
                objDPArr[14].Value = objRecordContent.m_strALLINTAKEXML;
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = objRecordContent.m_dtmModifyDate;
                objDPArr[16].Value = objRecordContent.m_strModifyUserID;
                objDPArr[17].Value = objRecordContent.m_intMarkStatus;
                #endregion

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);               
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServ = null;
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

            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsEMR_IntakeAndOutputVolumeSum objRecordContent = (clsEMR_IntakeAndOutputVolumeSum)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。

            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_intakeandoutput_sum t2
 where t2.registerid_chr = ?
   and t2.createdate_dat = ?
   and t2.status_int = 1";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                    dtbValue = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_CHR"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE_DAT"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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
                objHRPServ = null;

            } return lngRes;
        }
        #endregion

        #region 把新修改的内容保存到数据库。更新主表,添加子表.
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
            //检查参数

            if (p_objRecordContent == null || p_objRecordContent.m_dtmCreateDate == DateTime.MinValue || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsEMR_IntakeAndOutputVolumeSum objRecordContent = (clsEMR_IntakeAndOutputVolumeSum)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region set value
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(16, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRecordUserID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].Value = objRecordContent.m_strALLURINE;
                objDPArr[4].Value = objRecordContent.m_strALLURINEXML;
                objDPArr[5].Value = objRecordContent.m_strALLOUTPUT;
                objDPArr[6].Value = objRecordContent.m_strALLOUTPUTXML;
                objDPArr[7].Value = objRecordContent.m_strSPECIFICGRAVITY;
                objDPArr[8].Value = objRecordContent.m_strSPECIFICGRAVITYXML;
                objDPArr[9].Value = objRecordContent.m_strALLINTAKE;
                objDPArr[10].Value = objRecordContent.m_strALLINTAKEXML;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = objRecordContent.m_dtmModifyDate;
                objDPArr[12].Value = objRecordContent.m_strModifyUserID;
                objDPArr[13].Value = objRecordContent.m_intMarkStatus;

                objDPArr[14].Value = objRecordContent.m_strRegisterID;
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = objRecordContent.m_dtmCreateDate;

                #endregion
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                objHRPServ = null;

            } return lngRes;
        }
        #endregion

        #region 把记录从数据中“删除”。        
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

            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsEMR_IntakeAndOutputVolumeSum objRecordContent = (clsEMR_IntakeAndOutputVolumeSum)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;

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
                objHRPServ = null;

            } return lngRes;
        }

        [AutoComplete]
        public long m_lngDeleteSummaryRecord(clsTrackRecordContent p_objRecordContent)
        {
            return m_lngDeleteRecord2DB(p_objRecordContent,null);
        }
        #endregion

        #region 获取指定已经被删除记录的内容(用于显示在DG表格中)
        /// <summary>
        /// 获取指定已经被删除记录的内容。

        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strRegisterId, string p_strCreatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select t1.registerid_chr,
       t1.createdate_dat,
       t1.createuserid_chr,
       t1.status_int,
       t1.deactiveddate_dat,
       t1.deactivedoperatorid_chr,
       t1.recorduserid_vchr,
       t1.recorddate_dat,
       t1.sequence_int,
       t1.allurine,
       t1.allurinexml,
       t1.alloutput,
       t1.alloutputxml,
       t1.specificgravity,
       t1.specificgravityxml,
       t1.allintake,
       t1.allintakexml,
       t1.modifydate,
       t1.modifyuserid,
       t1.markstatus
  from t_emr_intakeandoutput_sum t1
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t1.modifydate = (select max(modifydate)
                          from t_emr_intakeandoutput_sum
                         where registerid_chr = ?
                           and createdate_dat = ?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                objDPArr[2].Value = p_strRegisterId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strCreatedDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsEMR_IntakeAndOutputVolumeSum objRecordContent = new clsEMR_IntakeAndOutputVolumeSum();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strALLURINE = dtrSelected["ALLURINE"].ToString();
                    objRecordContent.m_strALLURINEXML = dtrSelected["ALLURINEXML"].ToString();
                    objRecordContent.m_strALLOUTPUT = dtrSelected["ALLOUTPUT"].ToString();
                    objRecordContent.m_strALLOUTPUTXML = dtrSelected["ALLOUTPUTXML"].ToString();
                    objRecordContent.m_strSPECIFICGRAVITY = dtrSelected["SPECIFICGRAVITY"].ToString();
                    objRecordContent.m_strSPECIFICGRAVITYXML = dtrSelected["SPECIFICGRAVITYXML"].ToString();
                    objRecordContent.m_strALLINTAKE = dtrSelected["ALLINTAKE"].ToString();
                    objRecordContent.m_strALLINTAKEXML = dtrSelected["ALLINTAKEXML"].ToString();

                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion



        #region 获取指定日期的出入量情况
        /// <summary>
        /// 获取指定日期的出入量情况
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtmRecordDate">记录日期</param>
        /// <param name="p_objContent">出入量情况</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSpecifyInfo(string p_strRegisterID, DateTime p_dtmRecordDate, out clsEMR_IntakeAndOutputVolumeValue[] p_objContent)
        {
            p_objContent = null;
            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       a.recordtime_vchr,
       a.stool_vchr,
       a.urine_vchr,
       a.gastricjuice_vchr,
       a.bile_vchr,
       a.intestinaljuice_vchr,
       a.chestfluid_vchr,
       a.otheroutput_vchr,
       a.drinkingwater_vchr,
       a.food_vchr,
       a.transfusion_vchr,
       a.sugarwater_vchr,
       a.salinewater_vchr,
       a.otherintake_vchr,
       a.stool_xml,
       a.urine_xml,
       a.gastricjuice_xml,
       a.bile_xml,
       a.intestinaljuice_xml,
       a.chestfluid_xml,
       a.otheroutput_xml,
       a.drinkingwater_xml,
       a.food_xml,
       a.transfusion_xml,
       a.sugarwater_xml,
       a.salinewater_xml,
       a.otherintake_xml,
       a.index_int,
       a.markstatus,
       a.otheroutput_name,
       a.otherintake_name,
       b.modifydate,
       b.modifyuserid,
       b.stool_right,
       b.urine_right,
       b.gastricjuice_right,
       b.bile_right,
       b.intestinaljuice_right,
       b.chestfluid_right,
       b.otheroutput_right,
       b.drinkingwater_right,
       b.food_right,
       b.transfusion_right,
       b.sugarwater_right,
       b.salinewater_right,
       b.otherintake_right
  from t_emr_intakeandoutputvolume a
 inner join t_emr_intakeandoutput_right b on a.registerid_chr =
                                             b.registerid_chr
                                         and a.createdate_dat =
                                             b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?
   and a.recorddate_dat = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmRecordDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    int intTableCount = dtbValue.Rows.Count;
                    DataRow dtrSelected = null;
                    p_objContent = new clsEMR_IntakeAndOutputVolumeValue[intTableCount];

                    for (int i = 0; i < intTableCount; i++)
                    {
                        dtrSelected = dtbValue.Rows[i];
                        p_objContent[i] = new clsEMR_IntakeAndOutputVolumeValue();
                        p_objContent[i].m_strRegisterID = p_strRegisterID;
                        DateTime dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_objContent[i].m_dtmCreateDate = dtmTemp;

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_objContent[i].m_dtmRecordDate = dtmTemp;

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                        p_objContent[i].m_dtmFirstPrintDate = dtmTemp;

                        p_objContent[i].m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                        p_objContent[i].m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                        p_objContent[i].m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                        p_objContent[i].m_dtmModifyDate = dtmTemp;


                        int intTemp = 0;
                        int.TryParse(dtrSelected["IFCONFIRM_INT"].ToString(), out intTemp);
                        p_objContent[i].m_bytIfConfirm = intTemp;
                        intTemp = 0;
                        int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                        p_objContent[i].m_bytStatus = intTemp;

                        p_objContent[i].m_strRECORDTIME_VCHR = dtrSelected["RECORDTIME_VCHR"].ToString();
                        p_objContent[i].m_strSTOOL_VCHR = dtrSelected["STOOL_VCHR"].ToString();
                        p_objContent[i].m_strURINE_VCHR = dtrSelected["URINE_VCHR"].ToString();
                        p_objContent[i].m_strGASTRICJUICE_VCHR = dtrSelected["GASTRICJUICE_VCHR"].ToString();
                        p_objContent[i].m_strBILE_VCHR = dtrSelected["BILE_VCHR"].ToString();
                        p_objContent[i].m_strINTESTINALJUICE_VCHR = dtrSelected["INTESTINALJUICE_VCHR"].ToString();
                        p_objContent[i].m_strCHESTFLUID_VCHR = dtrSelected["CHESTFLUID_VCHR"].ToString();
                        p_objContent[i].m_strOTHEROUTPUT_VCHR = dtrSelected["OTHEROUTPUT_VCHR"].ToString();
                        p_objContent[i].m_strDRINKINGWATER_VCHR = dtrSelected["DRINKINGWATER_VCHR"].ToString();
                        p_objContent[i].m_strFOOD_VCHR = dtrSelected["FOOD_VCHR"].ToString();
                        p_objContent[i].m_strTRANSFUSION_VCHR = dtrSelected["TRANSFUSION_VCHR"].ToString();
                        p_objContent[i].m_strSUGARWATER_VCHR = dtrSelected["SUGARWATER_VCHR"].ToString();
                        p_objContent[i].m_strSALINEWATER_VCHR = dtrSelected["SALINEWATER_VCHR"].ToString();
                        p_objContent[i].m_strOTHERINTAKE_VCHR = dtrSelected["OTHERINTAKE_VCHR"].ToString();
                        p_objContent[i].m_strSTOOL_XML = dtrSelected["STOOL_XML"].ToString();
                        p_objContent[i].m_strURINE_XML = dtrSelected["URINE_XML"].ToString();
                        p_objContent[i].m_strGASTRICJUICE_XML = dtrSelected["GASTRICJUICE_XML"].ToString();
                        p_objContent[i].m_strBILE_XML = dtrSelected["BILE_XML"].ToString();
                        p_objContent[i].m_strINTESTINALJUICE_XML = dtrSelected["INTESTINALJUICE_XML"].ToString();
                        p_objContent[i].m_strCHESTFLUID_XML = dtrSelected["CHESTFLUID_XML"].ToString();
                        p_objContent[i].m_strOTHEROUTPUT_XML = dtrSelected["OTHEROUTPUT_XML"].ToString();
                        p_objContent[i].m_strDRINKINGWATER_XML = dtrSelected["DRINKINGWATER_XML"].ToString();
                        p_objContent[i].m_strFOOD_XML = dtrSelected["FOOD_XML"].ToString();
                        p_objContent[i].m_strTRANSFUSION_XML = dtrSelected["TRANSFUSION_XML"].ToString();
                        p_objContent[i].m_strSUGARWATER_XML = dtrSelected["SUGARWATER_XML"].ToString();
                        p_objContent[i].m_strSALINEWATER_XML = dtrSelected["SALINEWATER_XML"].ToString();
                        p_objContent[i].m_strOTHERINTAKE_XML = dtrSelected["OTHERINTAKE_XML"].ToString();
                        p_objContent[i].m_intINDEX_INT = Convert.ToInt32(dtrSelected["INDEX_INT"]);

                        p_objContent[i].m_strSTOOL_RIGHT = dtrSelected["STOOL_RIGHT"].ToString();
                        p_objContent[i].m_strURINE_RIGHT = dtrSelected["URINE_RIGHT"].ToString();
                        p_objContent[i].m_strGASTRICJUICE_RIGHT = dtrSelected["GASTRICJUICE_RIGHT"].ToString();
                        p_objContent[i].m_strBILE_RIGHT = dtrSelected["BILE_RIGHT"].ToString();
                        p_objContent[i].m_strINTESTINALJUICE_RIGHT = dtrSelected["INTESTINALJUICE_RIGHT"].ToString();
                        p_objContent[i].m_strCHESTFLUID_RIGHT = dtrSelected["CHESTFLUID_RIGHT"].ToString();
                        p_objContent[i].m_strOTHEROUTPUT_RIGHT = dtrSelected["OTHEROUTPUT_RIGHT"].ToString();
                        p_objContent[i].m_strDRINKINGWATER_RIGHT = dtrSelected["DRINKINGWATER_RIGHT"].ToString();
                        p_objContent[i].m_strFOOD_RIGHT = dtrSelected["FOOD_RIGHT"].ToString();
                        p_objContent[i].m_strTRANSFUSION_RIGHT = dtrSelected["TRANSFUSION_RIGHT"].ToString();
                        p_objContent[i].m_strSUGARWATER_RIGHT = dtrSelected["SUGARWATER_RIGHT"].ToString();
                        p_objContent[i].m_strSALINEWATER_RIGHT = dtrSelected["SALINEWATER_RIGHT"].ToString();
                        p_objContent[i].m_strOTHERINTAKE_RIGHT = dtrSelected["OTHERINTAKE_RIGHT"].ToString();

                        //获取签名集合
                        long lngS = 0;
                        if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent[i].objSignerArr);

                            //释放
                            objSign = null;
                        }
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 检查是否已存在相同时间段的记录
        /// <summary>
        /// 检查是否已存在相同时间段的记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtmRecordDate">记录日期</param>
        /// <param name="p_blnIsSame">是否相同</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckHasSameSummary(string p_strRegisterID, DateTime p_dtmRecordDate, out bool p_blnIsSame)
        {
            p_blnIsSame = false;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return 1;
            }

            string strSQL = @"select a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       a.allurine,
       a.allurinexml,
       a.alloutput,
       a.alloutputxml,
       a.specificgravity,
       a.specificgravityxml,
       a.allintake,
       a.allintakexml,
       a.modifydate,
       a.modifyuserid,
       a.markstatus
  from t_emr_intakeandoutput_sum a
 where a.registerid_chr = ?
   and a.recorddate_dat = ?
   and a.status_int = 1";


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmRecordDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_blnIsSame = true;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion
    }
}
