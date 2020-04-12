using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.clsGeneralNurseRecord_CSObstetricNewChildService
{
    /// <summary>
    /// 一般患者护理记录(茶山产科新生儿)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGeneralNurseRecord_CSObstetricNewChildService : clsDiseaseTrackService
    {
        public clsGeneralNurseRecord_CSObstetricNewChildService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region SQL语句
        /// <summary>
        /// 从generalnurse_obstetricnewchild获取指定病人的所有没有删除记录的时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createDate, opendate
														from generalnurse_obstetricnewchild
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

        /// <summary>
        /// 从generalnurse_obstetricnewchild中获取指定时间的表单,获取已经存在记录的主要信息

        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
															from generalnurse_obstetricnewchild
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

        /// <summary>
        /// 从generalnurse_obstetricnewchild获取删除表单的主要信息。

        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from generalnurse_obstetricnewchild
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        /// <summary>
        /// 添加记录到generalnurse_obstetricnewchild
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into  generalnurse_obstetricnewchild
						(inpatientid,inpatientdate,opendate,createdate,createuserid,
						ifconfirm,confirmreason,confirmreasonxml,status,temperature,
						temperaturexml,heartrate,heartratexml,respiration,respirationxml,
						fontanel,caputsuccedaneum,bloodedema,facecolor,cry,suckpower,
                        umbilicalregion,stool,stoolxml,urine,urinexml,recorddate,
                        sequence_int) 
						values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
							   ?,?,?,?,?,?,?,?)";//28

        /// <summary>
        /// 添加记录到generalnurse_obstetricchildcon
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into  generalnurse_obstetricchildcon
						(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,
						temperature_right,heartrate_right,respiration_right,
                        fontanel_right,caputsuccedaneum_right, bloodedema_right,
                        facecolor_right,cry_right,suckpower_right,umbilicalregion_right,
                        stool_right,urine_right)
					    values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";//17

        /// <summary>
        /// 修改记录到generalnurse_obstetricnewchild
        /// </summary>
        private const string c_strModifyRecordSQL = @"update generalnurse_obstetricnewchild 
			set temperature=?,temperaturexml=?,heartrate=?,heartratexml=?,respiration=?,
				respirationxml=?,fontanel=?,caputsuccedaneum=?,bloodedema=?,facecolor=?,
                cry=?,suckpower=?,umbilicalregion=?,stool=?,stoolxml=?,urine=?,urinexml=?,
                sequence_int = ?
				where inpatientid=? and inpatientdate=? and opendate=? and status=0";//21

        /// <summary>
        /// 修改记录到generalnurse_obstetricchildcon
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 设置generalnurse_obstetricnewchild中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update generalnurse_obstetricnewchild
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

        /// <summary>
        /// 更新generalnurse_obstetricnewchild中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update generalnurse_obstetricnewchild
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        /// <summary>
        /// 从generalnurse_obstetricnewchild获取指定病人的所有指定删除者删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate, opendate
																	from generalnurse_obstetricnewchild
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

        /// <summary>
        /// 从generalnurse_obstetricnewchild获取指定病人的所有已经删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate, opendate
																		from generalnurse_obstetricnewchild
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";
        #endregion

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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

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
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //objHRPServ.Dispose();
            }
            return lngRes;
        }

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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
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
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //objHRPServ.Dispose();
            }
            return lngRes;
        }

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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

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
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //objHRPServ.Dispose();
            }
            return lngRes;
        }

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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

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
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //objHRPServ.Dispose();
            }
            return lngRes;
        }

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

            string c_strGetRecordContentSQL = @"select t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.temperature,
       t1.temperaturexml,
       t1.heartrate,
       t1.heartratexml,
       t1.respiration,
       t1.respirationxml,
       t1.fontanel,
       t1.caputsuccedaneum,
       t1.bloodedema,
       t1.facecolor,
       t1.cry,
       t1.suckpower,
       t1.umbilicalregion,
       t1.stool,
       t1.stoolxml,
       t1.urine,
       t1.urinexml,
       t1.recorddate,                                                           
       t1.sequence_int,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_right,
       t2.heartrate_right,
       t2.respiration_right,
       t2.fontanel_right,
       t2.caputsuccedaneum_right,
       t2.bloodedema_right,
       t2.facecolor_right,
       t2.cry_right,
       t2.suckpower_right,
       t2.umbilicalregion_right,
       t2.stool_right,
       t2.urine_right
  from generalnurse_obstetricnewchild t1, generalnurse_obstetricchildcon t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t2.modifydate = (select max(modifydate)
                          from generalnurse_obstetricchildcon
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)";

            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
                    #region 设置结果
                    clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChild();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["IFCONFIRM"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    
                    objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();
                    //体温
                    objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
                    objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    //心率
                    objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATE_RIGHT"].ToString();
                    objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
                    objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
                    //呼吸
                    objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[0]["RESPIRATION_RIGHT"].ToString();
                    objRecordContent.m_strRESPIRATION = dtbValue.Rows[0]["RESPIRATION"].ToString();
                    objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
                    //囟门
                    objRecordContent.m_strFONTANEL = dtbValue.Rows[0]["FONTANEL"].ToString();
                    //产瘤
                    objRecordContent.m_strCAPUTSUCCEDANEUM = dtbValue.Rows[0]["CAPUTSUCCEDANEUM"].ToString();
                    //血肿
                    objRecordContent.m_strBLOODEDEMA = dtbValue.Rows[0]["BLOODEDEMA"].ToString();
                    //面色
                    objRecordContent.m_strFACECOLOR = dtbValue.Rows[0]["FACECOLOR"].ToString();
                    //哭声
                    objRecordContent.m_strCRY = dtbValue.Rows[0]["CRY"].ToString();
                    //吸吮力
                    objRecordContent.m_strSUCKPOWER = dtbValue.Rows[0]["SUCKPOWER"].ToString();
                    //脐部
                    objRecordContent.m_strUMBILICALREGION = dtbValue.Rows[0]["UMBILICALREGION"].ToString();
                    //大便
                    objRecordContent.m_strSTOOL_RIGHT = dtbValue.Rows[0]["STOOL_RIGHT"].ToString();
                    objRecordContent.m_strSTOOL = dtbValue.Rows[0]["STOOL"].ToString();
                    objRecordContent.m_strSTOOLXML = dtbValue.Rows[0]["STOOLXML"].ToString();
                    //小便
                    objRecordContent.m_strURINE_RIGHT = dtbValue.Rows[0]["URINE_RIGHT"].ToString();
                    objRecordContent.m_strURINE = dtbValue.Rows[0]["URINE"].ToString();
                    objRecordContent.m_strURINEXML = dtbValue.Rows[0]["URINEXML"].ToString();
                    //记录时间
                    objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[0]["RECORDDATE"].ToString());
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
                    #endregion
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

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
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
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
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

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
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            //获取签名流水号

            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = (clsGeneralNurseRecordContent_ObstetricNewChild)p_objRecordContent;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(28, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[4].Value = objRecordContent.m_strCreateUserID;
                objDPArr[5].Value = objRecordContent.m_bytIfConfirm;

                if (objRecordContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objRecordContent.m_strConfirmReason;
                if (objRecordContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objRecordContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;
                objDPArr[9].Value = objRecordContent.m_strTEMPERATUREAll;
                objDPArr[10].Value = objRecordContent.m_strTEMPERATUREXML;
                objDPArr[11].Value = objRecordContent.m_strHEARTRATE;
                objDPArr[12].Value = objRecordContent.m_strHEARTRATEXML;
                objDPArr[13].Value = objRecordContent.m_strRESPIRATION;
                objDPArr[14].Value = objRecordContent.m_strRESPIRATIONXML;
                objDPArr[15].Value = objRecordContent.m_strFONTANEL;
                objDPArr[16].Value = objRecordContent.m_strCAPUTSUCCEDANEUM;
                objDPArr[17].Value = objRecordContent.m_strBLOODEDEMA;
                objDPArr[18].Value = objRecordContent.m_strFACECOLOR;
                objDPArr[19].Value = objRecordContent.m_strCRY;
                objDPArr[20].Value = objRecordContent.m_strSUCKPOWER;
                objDPArr[21].Value = objRecordContent.m_strUMBILICALREGION;
                objDPArr[22].Value = objRecordContent.m_strSTOOL;
                objDPArr[23].Value = objRecordContent.m_strSTOOLXML;
                objDPArr[24].Value = objRecordContent.m_strURINE;
                objDPArr[25].Value = objRecordContent.m_strURINEXML;
                objDPArr[26].DbType = DbType.DateTime;
                objDPArr[26].Value = objRecordContent.m_dtmRECORDDATE;
                objDPArr[27].Value = lngSequence;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(17, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strHEARTRATE_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strRESPIRATION_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strFONTANEL;
                objDPArr2[9].Value = objRecordContent.m_strCAPUTSUCCEDANEUM;
                objDPArr2[10].Value = objRecordContent.m_strBLOODEDEMA;
                objDPArr2[11].Value = objRecordContent.m_strFACECOLOR;
                objDPArr2[12].Value = objRecordContent.m_strCRY;
                objDPArr2[13].Value = objRecordContent.m_strSUCKPOWER;
                objDPArr2[14].Value = objRecordContent.m_strUMBILICALREGION;
                objDPArr2[15].Value = objRecordContent.m_strSTOOL_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strURINE_RIGHT;
                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

                //释放
                objSign = null;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

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

            clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = (clsGeneralNurseRecordContent_ObstetricNewChild)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。

            /// </summary>
            string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from generalnurse_obstetricnewchild t1,generalnurse_obstetricchildcon t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status = 0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            try
            {
                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

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
                    return (long)enmOperationResult.DB_Succeed;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

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

            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = (clsGeneralNurseRecordContent_ObstetricNewChild)p_objRecordContent;
            long lngRes = 0;
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(21, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strTEMPERATUREAll;
                objDPArr[1].Value = objRecordContent.m_strTEMPERATUREXML;
                objDPArr[2].Value = objRecordContent.m_strHEARTRATE;
                objDPArr[3].Value = objRecordContent.m_strHEARTRATEXML;
                objDPArr[4].Value = objRecordContent.m_strRESPIRATION;
                objDPArr[5].Value = objRecordContent.m_strRESPIRATIONXML;

                objDPArr[6].Value = objRecordContent.m_strFONTANEL;
                objDPArr[7].Value = objRecordContent.m_strCAPUTSUCCEDANEUM;
                objDPArr[8].Value = objRecordContent.m_strBLOODEDEMA;
                objDPArr[9].Value = objRecordContent.m_strFACECOLOR;
                objDPArr[10].Value = objRecordContent.m_strCRY;
                objDPArr[11].Value = objRecordContent.m_strSUCKPOWER;
                objDPArr[12].Value = objRecordContent.m_strUMBILICALREGION;

                objDPArr[13].Value = objRecordContent.m_strSTOOL;
                objDPArr[14].Value = objRecordContent.m_strSTOOLXML;
                objDPArr[15].Value = objRecordContent.m_strURINE;
                objDPArr[16].Value = objRecordContent.m_strURINEXML;
                objDPArr[17].Value = lngSequence;
                objDPArr[18].Value = objRecordContent.m_strInPatientID;
                objDPArr[19].DbType = DbType.DateTime;
                objDPArr[19].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[20].DbType = DbType.DateTime;
                objDPArr[20].Value = objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(17, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strHEARTRATE_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strRESPIRATION_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strFONTANEL;
                objDPArr2[9].Value = objRecordContent.m_strCAPUTSUCCEDANEUM;
                objDPArr2[10].Value = objRecordContent.m_strBLOODEDEMA;
                objDPArr2[11].Value = objRecordContent.m_strFACECOLOR;
                objDPArr2[12].Value = objRecordContent.m_strCRY;
                objDPArr2[13].Value = objRecordContent.m_strSUCKPOWER;
                objDPArr2[14].Value = objRecordContent.m_strUMBILICALREGION;
                objDPArr2[15].Value = objRecordContent.m_strSTOOL_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strURINE_RIGHT;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

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

            clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = (clsGeneralNurseRecordContent_ObstetricNewChild)p_objRecordContent;
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
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
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

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
            //检查参数

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            /// <summary>
            /// 从IntensiveTendRecord1和IntensiveTendRecordContent1获取LastModifyDate和FirstPrintDate
            /// </summary>
            string c_strGetModifyDateAndFirstPrintDateSQL = clsDatabaseSQLConvert.s_StrTop1 + @" a.firstprintdate,b.modifydate from generalnurse_obstetricnewchild a,
					generalnurse_obstetricchildcon b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;


            long lngRes = 0;
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
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
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

            string c_strGetDeleteRecordContentSQL = @"select t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.temperature,
       t1.temperaturexml,
       t1.heartrate,
       t1.heartratexml,
       t1.respiration,
       t1.respirationxml,
       t1.fontanel,
       t1.caputsuccedaneum,
       t1.bloodedema,
       t1.facecolor,
       t1.cry,
       t1.suckpower,
       t1.umbilicalregion,
       t1.stool,
       t1.stoolxml,
       t1.urine,
       t1.urinexml,
       t1.recorddate,                                                           
       t1.sequence_int,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_right,
       t2.heartrate_right,
       t2.respiration_right,
       t2.fontanel_right,
       t2.caputsuccedaneum_right,
       t2.bloodedema_right,
       t2.facecolor_right,
       t2.cry_right,
       t2.suckpower_right,
       t2.umbilicalregion_right,
       t2.stool_right,
       t2.urine_right
  from generalnurse_obstetricnewchild t1, generalnurse_obstetricchildcon t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t2.modifydate = (select max(modifydate)
                          from generalnurse_obstetricchildcon
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)";

            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
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
                    #region 设置结果
                    clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChild();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //					objRecordContent.m_strContentCreateUserName = dtbValue.Rows[0]["CreateUserName"].ToString();
                    if (dtbValue.Rows[0]["IFCONFIRM"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();
                    //体温
                    objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
                    objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    //心率
                    objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATE_RIGHT"].ToString();
                    objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
                    objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
                    //呼吸
                    objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[0]["RESPIRATION_RIGHT"].ToString();
                    objRecordContent.m_strRESPIRATION = dtbValue.Rows[0]["RESPIRATION"].ToString();
                    objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
                    //囟门
                    objRecordContent.m_strFONTANEL = dtbValue.Rows[0]["FONTANEL"].ToString();
                    //产瘤
                    objRecordContent.m_strCAPUTSUCCEDANEUM = dtbValue.Rows[0]["CAPUTSUCCEDANEUM"].ToString();
                    //血肿
                    objRecordContent.m_strBLOODEDEMA = dtbValue.Rows[0]["BLOODEDEMA"].ToString();
                    //面色
                    objRecordContent.m_strFACECOLOR = dtbValue.Rows[0]["FACECOLOR"].ToString();
                    //哭声
                    objRecordContent.m_strCRY = dtbValue.Rows[0]["CRY"].ToString();
                    //吸吮力
                    objRecordContent.m_strSUCKPOWER = dtbValue.Rows[0]["SUCKPOWER"].ToString();
                    //脐部
                    objRecordContent.m_strUMBILICALREGION = dtbValue.Rows[0]["UMBILICALREGION"].ToString();
                    //大便
                    objRecordContent.m_strSTOOL_RIGHT = dtbValue.Rows[0]["STOOL_RIGHT"].ToString();
                    objRecordContent.m_strSTOOL = dtbValue.Rows[0]["STOOL"].ToString();
                    objRecordContent.m_strSTOOLXML = dtbValue.Rows[0]["STOOLXML"].ToString();
                    //小便
                    objRecordContent.m_strURINE_RIGHT = dtbValue.Rows[0]["URINE_RIGHT"].ToString();
                    objRecordContent.m_strURINE = dtbValue.Rows[0]["URINE"].ToString();
                    objRecordContent.m_strURINEXML = dtbValue.Rows[0]["URINEXML"].ToString();
                    //记录时间
                    objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[0]["RECORDDATE"].ToString());
                    p_objRecordContent = objRecordContent;
                    #endregion
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 更新病情记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateDetail(clsGeneralNurseRecordContent_ObstetricNewChildDetail p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strINPATIENTID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            try
            {
                string strSQL = @"update generalnurse_obechilddetail 
								set recordcontent=?,recordcontentxml=? ,recordcontent_right=?,
                                sequence_int=?
								where inpatientid=? and inpatientdate=? and recorddate=? and status = 0";
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[1].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[2].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[3].Value = lngSequence.ToString();
                objDPArr[4].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objRecordContent.m_dtmRECORDDATE;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取病情记录内容
        /// </summary>
        /// <param name="dtmRecordDate"></param>
        /// <param name="strInPatientID"></param>
        /// <param name="strRecordContent"></param>
        /// <param name="strRecordCotentXML"></param>
        /// <returns></returns>

        [AutoComplete]
        public long m_lngGetDetail(DateTime dtmRecordDate, string strInPatientID, out string strRecordContent, out string strRecordCotentXML)
        {
            strRecordContent = "";
            strRecordCotentXML = "";
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select t.recordcontent_right, t.recordcontentxml
									from generalnurse_obechilddetail t
								where recorddate = ?
									and inpatientid = ?
									and status = 0";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[41];
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = dtmRecordDate;
                objDPArr[1].Value = strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    strRecordContent = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    strRecordCotentXML = dtbValue.Rows[0]["recordcontentxml"].ToString();
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

                //p_objHRPServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 保存病情记录内容
        /// </summary>
        /// <param name="p_objRecordContent">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDetail(clsGeneralNurseRecordContent_ObstetricNewChildDetail p_objRecordContent)
        {
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strINPATIENTID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            //获取签名流水号

            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            try
            {
                string strSQL = @"insert into generalnurse_obechilddetail (inpatientid,inpatientdate,opendate,
								createdate,createuserid,modifyuserid,recordcontent,recordcontentxml,recorddate,
								status,recordcontent_right,modifydate,sequence_int) 
								values (?,?,?,?,?,?,?,?,?,0,?,?,?)";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOPENDATE;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCREATERECORDDATE;
                objDPArr[4].Value = p_objRecordContent.m_strCREATERECORDUSERID;
                objDPArr[5].Value = p_objRecordContent.m_strMODIFYRECORDUSERID;
                objDPArr[6].Value = p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[7].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecordContent.m_dtmRECORDDATE;
                objDPArr[9].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = p_objRecordContent.m_dtmMODIFYDATE;
                objDPArr[11].Value = lngSequence;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 修改病情记录内容
        /// </summary>
        /// <param name="p_objRecordContent">病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyDetail(clsGeneralNurseRecordContent_ObstetricNewChildDetail p_objRecordContent)
        {
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strINPATIENTID == null)
                    return (long)enmOperationResult.Parameter_Error;
                string strSQL = @"update generalnurse_obechilddetail set opendate=?,
							modifyuserid=?,modifydate=?,recordcontent=?,recordcontentxml=?,recordcontent_right=?,
                            sequence_int=?
							where inpatientid=? and inpatientdate=? and recorddate=? and status = 0";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(10, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmOPENDATE;
                objDPArr[1].Value = p_objRecordContent.m_strMODIFYRECORDUSERID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmMODIFYDATE;
                objDPArr[3].Value = p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[4].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[5].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[6].Value = lngSequence.ToString();
                objDPArr[7].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = p_objRecordContent.m_dtmRECORDDATE;//注意此处的创建时间为病程记录内容的创建时间
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        ///  获取指定病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strRecordDate">创建日期</param>
        /// <param name="p_objRecordContent">病程记录内容</param>
        /// <returns>返回值</returns>
        [AutoComplete]
        public long m_lngGetRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strRecordDate,
            out clsGeneralNurseRecordContent_ObstetricNewChildDetail p_objRecordContent)
        {
            long lngRes = 0;
            p_objRecordContent = null;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select   t.inpatientid,
                                           t.inpatientdate,
                                           t.opendate,
                                           t.createdate,
                                           t.createuserid,
                                           t.modifyuserid,
                                           t.recordcontent,
                                           t.recordcontentxml,
                                           t.status,
                                           t.recordcontent_right,
                                           t.recorddate,
                                           t.modifydate,
                                           t.deactiveddate,
                                           t.deactivedoperatorid,
                                           f_getempnamebyno(t.createuserid) as lastname_vchr,
                                           t.sequence_int
                                  from generalnurse_obechilddetail t
								  where  inpatientid=? and t.inpatientdate=?  and t.status=0 and t.recorddate=?";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strRecordDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChildDetail();
                    p_objRecordContent.m_dtmCREATERECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["CreateDate"]);
                    p_objRecordContent.m_strCREATERECORDUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objRecordContent.m_strDetailCreateUserName = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                    p_objRecordContent.m_strRECORDCONTENTAll = dtbValue.Rows[0]["RecordContent"].ToString();
                    p_objRecordContent.m_strRECORDCONTENTXML = dtbValue.Rows[0]["RecordContentXML"].ToString();
                    p_objRecordContent.m_strRECORDCONTENT_RIGHT = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    p_objRecordContent.m_dtmMODIFYDATE = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    p_objRecordContent.m_strMODIFYRECORDUSERID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    //获取签名集合
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out p_objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
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

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// 删除指定病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strRecordDate"></param>
        /// <param name="p_strDelDate"></param>
        /// <param name="p_strDelID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDetail(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strRecordDate,
            string p_strDelDate,
            string p_strDelID)
        {
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @" update generalnurse_obechilddetail set status=1,deactiveddate=?,deactivedoperatorid=?
								where inpatientid=? and  inpatientdate=? and  recorddate=?";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strDelDate);
                objDPArr[1].Value = p_strDelID;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strRecordDate);
                //执行查询 
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        ///  获取指定病人的病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strRecordContentArr">病程记录内容数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContentWithInpatient(string p_strInPatientID, string p_strInPatientDate,
            out string[][] p_strRecordContentArr)
        {
            long lngRes = 0;
            p_strRecordContentArr = null;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select   t.inpatientid,
                                           t.inpatientdate,
                                           t.opendate,
                                           t.createdate,
                                           t.createuserid,
                                           t.modifyuserid,
                                           t.recordcontent,
                                           t.recordcontentxml,
                                           t.status,
                                           t.recordcontent_right,
                                           t.recorddate,
                                           t.modifydate,
                                           t.deactiveddate,
                                           t.deactivedoperatorid,
                                           f_getempnamebyno(t.createuserid) as lastname_vchr,
                                            t.sequence_int
							    from generalnurse_obechilddetail t 
								where t.status = 0
									and t.inpatientid = ?
									and t.inpatientdate = ?";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0)
                {
                    p_strRecordContentArr = new string[dtbValue.Rows.Count][];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_strRecordContentArr[i] = new string[7];
                        p_strRecordContentArr[i][0] = dtbValue.Rows[0]["RecordContent"].ToString();
                        p_strRecordContentArr[i][1] = dtbValue.Rows[0]["RecordContentXML"].ToString();
                        p_strRecordContentArr[i][2] = dtbValue.Rows[0]["RECORDDATE"].ToString();
                        p_strRecordContentArr[i][3] = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                        p_strRecordContentArr[i][4] = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                        p_strRecordContentArr[i][5] = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {


                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        ///  获取指定病人的已删除病情记录内容
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strOpenDate">首次创建日期</param>
        /// <param name="p_objRecordContent">病程记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDelRecordContentWithInpatient(string p_strInPatientID, string p_strInPatientDate,
            string p_strOpenDate,
            out clsGeneralNurseRecordContent_ObstetricNewChildDetail p_objRecordContent)
        {
            long lngRes = 0;
            p_objRecordContent = null;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select   t.inpatientid,
                                           t.inpatientdate,
                                           t.opendate,
                                           t.createdate,
                                           t.createuserid,
                                           t.modifyuserid,
                                           t.recordcontent,
                                           t.recordcontentxml,
                                           t.status,
                                           t.recordcontent_right,
                                           t.recorddate,
                                           t.modifydate,
                                           t.deactiveddate,
                                           t.deactivedoperatorid,
                                           f_getempnamebyno(t.createuserid) as lastname_vchr,
                                            t.sequence_int
							    from generalnurse_obechilddetail t 
								where t.status = 1
									and t.inpatientid = ?
									and t.inpatientdate = ?
									and t.opendate=?";
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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0)
                {
                    p_objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChildDetail();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objRecordContent.m_strRECORDCONTENT_RIGHT = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                        p_objRecordContent.m_strRECORDCONTENTXML = dtbValue.Rows[0]["RecordContentXML"].ToString();
                        p_objRecordContent.m_dtmCREATERECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                        p_objRecordContent.m_strCREATERECORDUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                        p_objRecordContent.m_strDetailCreateUserName = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                        p_objRecordContent.m_strRECORDCONTENTAll = dtbValue.Rows[0]["RECORDCONTENT"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {


                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;

        }
    }
}