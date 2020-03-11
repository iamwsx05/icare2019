using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.Utility;

namespace com.digitalwave.clsRecordsService
{
    /// <summary>
    /// 一般患者护理记录(产科新生儿)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGeneralNurseRecord_CSObstetricNewChildMainService : clsRecordsService
    {
        public clsGeneralNurseRecord_CSObstetricNewChildMainService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region SQL语句
        private const string c_strUpdateFirstPrintDateSQL = @"update generalnurse_obstetricnewchild
															set firstprintdate = ?
															    where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t1.createuserid) as createusername,												    
                                                                   t1.inpatientid,
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
                                                                   t3.modifydate,
                                                                   t3.modifyuserid,
                                                                   t3.temperature_right,
                                                                   t3.heartrate_right,
                                                                   t3.respiration_right,
                                                                   t3.fontanel_right,
                                                                   t3.caputsuccedaneum_right,
                                                                   t3.bloodedema_right,
                                                                   t3.facecolor_right,
                                                                   t3.cry_right,
                                                                   t3.suckpower_right,
                                                                   t3.umbilicalregion_right,
                                                                   t3.stool_right,
                                                                   t3.urine_right
														from generalnurse_obstetricnewchild t1,
															 generalnurse_obstetricchildcon t3
														where t1.inpatientid = t3.inpatientid
														and t1.inpatientdate = t3.inpatientdate
														and t1.opendate = t3.opendate
														and t1.status = 0
														and t1.inpatientid = ?
														and t1.inpatientdate = ?
														order by t1.recorddate,t3.modifydate";

        private const string c_strGetRecordContentSQL_Single = @"select f_getempnamebyno(t1.createuserid) as createusername,
																	t1.inpatientid,
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
                                                                   t3.modifydate,
                                                                   t3.modifyuserid,
                                                                   t3.temperature_right,
                                                                   t3.heartrate_right,
                                                                   t3.respiration_right,
                                                                   t3.fontanel_right,
                                                                   t3.caputsuccedaneum_right,
                                                                   t3.bloodedema_right,
                                                                   t3.facecolor_right,
                                                                   t3.cry_right,
                                                                   t3.suckpower_right,
                                                                   t3.umbilicalregion_right,
                                                                   t3.stool_right,
                                                                   t3.urine_right
														from generalnurse_obstetricnewchild t1,
															 generalnurse_obstetricchildcon t3
														where t1.inpatientid = t3.inpatientid
														and t1.inpatientdate = t3.inpatientdate
														and t1.opendate = t3.opendate
														and t1.status = 0
														and t1.inpatientid = ?
														and t1.inpatientdate = ?
														and t1.opendate = ?
														order by t1.recorddate,t3.modifydate";

        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from generalnurse_obstetricnewchild
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        private const string c_strDeleteRecordSQL = @"update generalnurse_obstetricnewchild
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

        private const string c_strGetDetailSQL = @"select t.inpatientid,
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
       t.sequence_int,
       f_getempnamebyno(t.createuserid) as lastname_vchr
  from generalnurse_obechilddetail t
 where t.inpatientid = ?
   and t.inpatientdate = ?
   and t.status = 0
 order by t.recorddate";
        #endregion


        /// <summary>
        /// 更新数据库中的首次打印时间。

        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_intRecordTypeArr">记录类型</param>
        /// <param name="p_dtmOpenDateArr">记录时间(与记录类型及其位置一一对应)</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmOpenDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendMainService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	


            //检查参数

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" ||
                p_dtmOpenDateArr == null || p_dtmFirstPrintDate == DateTime.MinValue)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr = null;
            for (int i = 0; i < p_dtmOpenDateArr.Length; i++)
            {
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmOpenDateArr[i];
                //执行SQL
                long lngRes = 0;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
            }
            //objHRPServ.Dispose();
            return (long)enmOperationResult.DB_Succeed;
        }

        /// <summary>
        /// 修改或添加一条记录时读数据库
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strRecordOpenDate"></param>
        /// <param name="p_objTansDataInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strRecordOpenDate,
            out clsGeneralNurseRecordContent_ObstetricNewChild[] p_objTansDataInfo)
        {
            p_objTansDataInfo = null;
            //检查参数

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                ArrayList arlTransData = new ArrayList();
                ArrayList arlModifyData = new ArrayList();
                DateTime dtmOpenDate;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strRecordOpenDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objTansDataInfo = new clsGeneralNurseRecordContent_ObstetricNewChild[dtbValue.Rows.Count];
                    clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = null;

                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date == dtmOpenDate)
                        {
                            #region 从DataTable.Rows中获取结果
                            objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChild();
                            objRecordContent.m_strInPatientID = p_strInPatientID;
                            objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                            objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                            objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                            if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                                objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                            objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                            objRecordContent.m_strContentCreateUserName = dtbValue.Rows[j]["CreateUserName"].ToString();
                            objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                            objRecordContent.m_strModifyUserName = dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();
                            if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                                objRecordContent.m_bytIfConfirm = 0;
                            else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                            if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                                objRecordContent.m_bytStatus = 0;
                            else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());

                            objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                            objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                            //体温
                            objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[j]["TEMPERATURE_RIGHT"].ToString();
                            objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[j]["TEMPERATURE"].ToString();
                            objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
                            //心率
                            objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[j]["HEARTRATE_RIGHT"].ToString();
                            objRecordContent.m_strHEARTRATE = dtbValue.Rows[j]["HEARTRATE"].ToString();
                            objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[j]["HEARTRATEXML"].ToString();
                            //呼吸
                            objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[j]["RESPIRATION_RIGHT"].ToString();
                            objRecordContent.m_strRESPIRATION = dtbValue.Rows[j]["RESPIRATION"].ToString();
                            objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[j]["RESPIRATIONXML"].ToString();
                            //囟门
                            objRecordContent.m_strFONTANEL = dtbValue.Rows[j]["FONTANEL"].ToString();
                            //产瘤
                            objRecordContent.m_strCAPUTSUCCEDANEUM = dtbValue.Rows[j]["CAPUTSUCCEDANEUM"].ToString();
                            //血肿
                            objRecordContent.m_strBLOODEDEMA = dtbValue.Rows[j]["BLOODEDEMA"].ToString();
                            //面色
                            objRecordContent.m_strFACECOLOR = dtbValue.Rows[j]["FACECOLOR"].ToString();
                            //哭声
                            objRecordContent.m_strCRY = dtbValue.Rows[j]["CRY"].ToString();
                            //吸吮力
                            objRecordContent.m_strSUCKPOWER = dtbValue.Rows[j]["SUCKPOWER"].ToString();
                            //脐部
                            objRecordContent.m_strUMBILICALREGION = dtbValue.Rows[j]["UMBILICALREGION"].ToString();
                            //大便
                            objRecordContent.m_strSTOOL_RIGHT = dtbValue.Rows[j]["STOOL_RIGHT"].ToString();
                            objRecordContent.m_strSTOOL = dtbValue.Rows[j]["STOOL"].ToString();
                            objRecordContent.m_strSTOOLXML = dtbValue.Rows[j]["STOOLXML"].ToString();
                            //小便
                            objRecordContent.m_strURINE_RIGHT = dtbValue.Rows[j]["URINE_RIGHT"].ToString();
                            objRecordContent.m_strURINE = dtbValue.Rows[j]["URINE"].ToString();
                            objRecordContent.m_strURINEXML = dtbValue.Rows[j]["URINEXML"].ToString();
                            //记录时间
                            objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[j]["RECORDDATE"].ToString());
                            //获取签名集合
                            if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                            {
                                long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                                long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                                //释放
                                objSign = null;
                            }
                            #endregion
                        }

                        p_objTansDataInfo[j] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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

        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objGeneralNurseRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsTransDataInfo[] p_objIntensiveTendInfoArr)
        {
            clsGeneralNurseRecordContent_ObstetricNewChild[] p_objGeneralNurseRecordArr = null;
            clsGeneralNurseRecordContent_ObstetricNewChildDetail[] p_objGeneralNurseDetailArr = null;
            p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
            long lngRes = -1;
            //检查参数

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsGeneralNurseRecordContent_ObstetricNewChildDataInfo objDataInfo = new clsGeneralNurseRecordContent_ObstetricNewChildDataInfo();
            try
            {
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbDetail = new DataTable();//病情记录内容
                DataTable dtbContent = new DataTable();//护理记录内容  

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    clsGeneralNurseRecordContent_ObstetricNewChild objRecordContent = null;
                    p_objGeneralNurseRecordArr = new clsGeneralNurseRecordContent_ObstetricNewChild[dtbContent.Rows.Count];
                    for (int i = 0; i < dtbContent.Rows.Count; i++)
                    {
                        objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChild();
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbContent.Rows[i]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbContent.Rows[i]["MODIFYDATE"].ToString());
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

                        if (dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbContent.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbContent.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_strContentCreateUserName = dtbContent.Rows[i]["CreateUserName"].ToString();
                        if (dtbContent.Rows[i]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbContent.Rows[i]["IFCONFIRM"].ToString());
                        if (dtbContent.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbContent.Rows[i]["STATUS"].ToString());

                        objRecordContent.m_strConfirmReason = dtbContent.Rows[i]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbContent.Rows[i]["CONFIRMREASONXML"].ToString();

                        //体温
                        objRecordContent.m_strTEMPERATURE_RIGHT = dtbContent.Rows[i]["TEMPERATURE_RIGHT"].ToString();
                        objRecordContent.m_strTEMPERATUREAll = dtbContent.Rows[i]["TEMPERATURE"].ToString();
                        objRecordContent.m_strTEMPERATUREXML = dtbContent.Rows[i]["TEMPERATUREXML"].ToString();
                        //心率
                        objRecordContent.m_strHEARTRATE_RIGHT = dtbContent.Rows[i]["HEARTRATE_RIGHT"].ToString();
                        objRecordContent.m_strHEARTRATE = dtbContent.Rows[i]["HEARTRATE"].ToString();
                        objRecordContent.m_strHEARTRATEXML = dtbContent.Rows[i]["HEARTRATEXML"].ToString();
                        //呼吸
                        objRecordContent.m_strRESPIRATION_RIGHT = dtbContent.Rows[i]["RESPIRATION_RIGHT"].ToString();
                        objRecordContent.m_strRESPIRATION = dtbContent.Rows[i]["RESPIRATION"].ToString();
                        objRecordContent.m_strRESPIRATIONXML = dtbContent.Rows[i]["RESPIRATIONXML"].ToString();
                        //囟门
                        objRecordContent.m_strFONTANEL = dtbContent.Rows[i]["FONTANEL"].ToString();
                        //产瘤
                        objRecordContent.m_strCAPUTSUCCEDANEUM = dtbContent.Rows[i]["CAPUTSUCCEDANEUM"].ToString();
                        //血肿
                        objRecordContent.m_strBLOODEDEMA = dtbContent.Rows[i]["BLOODEDEMA"].ToString();
                        //面色
                        objRecordContent.m_strFACECOLOR = dtbContent.Rows[i]["FACECOLOR"].ToString();
                        //哭声
                        objRecordContent.m_strCRY = dtbContent.Rows[i]["CRY"].ToString();
                        //吸吮力
                        objRecordContent.m_strSUCKPOWER = dtbContent.Rows[i]["SUCKPOWER"].ToString();
                        //脐部
                        objRecordContent.m_strUMBILICALREGION = dtbContent.Rows[i]["UMBILICALREGION"].ToString();
                        //大便
                        objRecordContent.m_strSTOOL_RIGHT = dtbContent.Rows[i]["STOOL_RIGHT"].ToString();
                        objRecordContent.m_strSTOOL = dtbContent.Rows[i]["STOOL"].ToString();
                        objRecordContent.m_strSTOOLXML = dtbContent.Rows[i]["STOOLXML"].ToString();
                        //小便
                        objRecordContent.m_strURINE_RIGHT = dtbContent.Rows[i]["URINE_RIGHT"].ToString();
                        objRecordContent.m_strURINE = dtbContent.Rows[i]["URINE"].ToString();
                        objRecordContent.m_strURINEXML = dtbContent.Rows[i]["URINEXML"].ToString();
                        //记录时间
                        objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbContent.Rows[i]["RECORDDATE"].ToString());
                        //获取签名集合
                        if (dtbContent.Rows[i]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbContent.Rows[i]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                            //释放
                            objSign = null;
                        }
                        p_objGeneralNurseRecordArr[i] = objRecordContent;
                    }
                    objDataInfo.m_objRecordContent = p_objGeneralNurseRecordArr[p_objGeneralNurseRecordArr.Length - 1];
                }
                objDataInfo.m_objRecordArr = p_objGeneralNurseRecordArr;

                IDataParameter[] objDPArr1 = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID.Trim();
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDetailSQL, ref dtbDetail, objDPArr1);
                if (lngRes > 0 && dtbDetail.Rows.Count > 0)
                {
                    clsGeneralNurseRecordContent_ObstetricNewChildDetail objDetail = null;
                    p_objGeneralNurseDetailArr = new clsGeneralNurseRecordContent_ObstetricNewChildDetail[dtbDetail.Rows.Count];
                    for (int j = 0; j < dtbDetail.Rows.Count; j++)
                    {
                        objDetail = new clsGeneralNurseRecordContent_ObstetricNewChildDetail();

                        objDetail.m_dtmCREATERECORDDATE = DateTime.Parse(dtbDetail.Rows[j]["CREATEDATE"].ToString());
                        objDetail.m_dtmMODIFYDATE = DateTime.Parse(dtbDetail.Rows[j]["MODIFYDATE"].ToString());

                        objDetail.m_strCREATERECORDUSERID = dtbDetail.Rows[j]["CREATEUSERID"].ToString();
                        objDetail.m_strMODIFYRECORDUSERID = dtbDetail.Rows[j]["MODIFYUSERID"].ToString();
                        objDetail.m_strDetailCreateUserName = dtbDetail.Rows[j]["LASTNAME_VCHR"].ToString();
                        objDetail.m_dtmRECORDDATE = DateTime.Parse(dtbDetail.Rows[j]["RECORDDATE"].ToString());
                        if (dtbDetail.Rows[j]["STATUS"].ToString() == "")
                            objDetail.m_bytStatus = 0;
                        else objDetail.m_bytStatus = Byte.Parse(dtbDetail.Rows[j]["STATUS"].ToString());
                        objDetail.m_strRECORDCONTENT_RIGHT = dtbDetail.Rows[j]["RECORDCONTENT_RIGHT"].ToString();
                        objDetail.m_strRECORDCONTENTAll = dtbDetail.Rows[j]["RECORDCONTENT"].ToString();
                        objDetail.m_strRECORDCONTENTXML = dtbDetail.Rows[j]["RECORDCONTENTXML"].ToString();
                        //获取签名集合
                        if (dtbDetail.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbDetail.Rows[j]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objDetail.objSignerArr);

                            //释放
                            objSign = null;
                        }
                        p_objGeneralNurseDetailArr[j] = objDetail;
                    }
                }
                objDataInfo.m_objDetailArr = p_objGeneralNurseDetailArr;
                objDataInfo.m_intFlag = (int)enmRecordsType.GeneralNurseRecord_GXRec;

                if (objDataInfo.m_objRecordArr == null)
                {
                    objDataInfo.m_objRecordContent = new clsGeneralNurseRecordContent_ObstetricNewChild();
                    objDataInfo.m_objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                }
                p_objIntensiveTendInfoArr[0] = objDataInfo;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }
            return (long)enmOperationResult.DB_Succeed;
        }

        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //检查参数          
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from 
											generalnurse_obstetricnewchild t1,generalnurse_obstetricchildcon t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status = 0
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;


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
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

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
                    //if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数

            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                //按顺序给IDataParameter赋值

                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

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
    }
}
