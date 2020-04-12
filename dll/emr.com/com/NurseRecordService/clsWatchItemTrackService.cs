using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;

namespace com.digitalwave.clsWatchItemTrackService
{
    /// <summary>
    /// Summary description for clsWatchItemTrackService.
    /// Alex 2003-5-14
    /// 实现观察项目记录的中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsWatchItemTrackService : com.digitalwave.clsRecordsService.clsRecordsService
    {
        /// <summary>
        /// 更新首次打印时间
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL_Normal = "update  watchitemrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        //		private const string c_strGetRecordContentSQL =  @"SELECT T1.CreateDate AS CreateDate_Main,T1.OpenDate AS OpenDate_Main,T1.*,T2.*,T3.* FROM WatchItemRecord AS T1,WatchItemRecordContent AS T2,
        //			WatchItemRecordAllContent AS T3	WHERE T1.InPatientID = T2.InPatientID AND T1.InPatientDate = T2.InPatientDate
        //			AND T1.OpenDate = T2.OpenDate AND T1.InPatientID = T3.InPatientID AND T1.InPatientDate = T3.InPatientDate
        //			AND T1.OpenDate = T3.OpenDate AND T1.Status =0
        //			AND trim(T1.InPatientID) = ? AND T1.InPatientDate = ?  ORDER BY T1.CreateDate,T2.ModifyDate";
        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t2.modifyuserid) as modifyusername,
       t1.createdate as createdate_main,
       t1.opendate as opendate_main,
       t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.temperaturexml,
       t1.heartrhythmxml,
       t1.heartfrequencyxml,
       t1.bloodoxygensaturationxml,
       t1.bedsidebloodsugarxml,
       t1.breathxml,
       t1.pulsexml,
       t1.bloodpressuresxml,
       t1.bloodpressureaxml,
       t1.pupilleftxml,
       t1.pupilrightxml,
       t1.echoleftxml,
       t1.echorightxml,
       t1.indxml,
       t1.inixml,
       t1.outuxml,
       t1.outsxml,
       t1.outvxml,
       t1.outexml,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_last,
       t2.heartrhythm_last,
       t2.heartfrequency_last,
       t2.bloodoxygensaturation_last,
       t2.bedsidebloodsugar_last,
       t2.breath_last,
       t2.pulse_last,
       t2.bloodpressures_last,
       t2.bloodpressurea_last,
       t2.pupilleft_last,
       t2.pupilright_last,
       t2.echoleft_last,
       t2.echoright_last,
       t2.ind_last,
       t2.ini_last,
       t2.outu_last,
       t2.outs_last,
       t2.outv_last,
       t2.oute_last,
       t3.temperature,
       t3.heartrhythm,
       t3.heartfrequency,
       t3.bloodoxygensaturation,
       t3.bedsidebloodsugar,
       t3.breath,
       t3.pulse,
       t3.bloodpressures,
       t3.bloodpressurea,
       t3.pupilleft,
       t3.pupilright,
       t3.echoleft,
       t3.echoright,
       t3.ind,
       t3.ini,
       t3.outu,
       t3.outs,
       t3.outv,
       t3.oute
  from watchitemrecord t1
 inner join watchitemrecordcontent t2 on t1.inpatientid = t2.inpatientid
                                     and t1.inpatientdate =
                                         t2.inpatientdate
                                     and t1.opendate = t2.opendate
 inner join watchitemrecordallcontent t3 on t1.inpatientid = t3.inpatientid
                                        and t1.inpatientdate =
                                            t3.inpatientdate
                                        and t1.opendate = t3.opendate
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate, t2.modifydate";

        private const string c_strGetRecordContentSQL_Single = @"select f_getempnamebyno(t2.modifyuserid) as modifyusername,
       t1.createdate as createdate_main,
       t1.opendate as opendate_main,
       t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.temperaturexml,
       t1.heartrhythmxml,
       t1.heartfrequencyxml,
       t1.bloodoxygensaturationxml,
       t1.bedsidebloodsugarxml,
       t1.breathxml,
       t1.pulsexml,
       t1.bloodpressuresxml,
       t1.bloodpressureaxml,
       t1.pupilleftxml,
       t1.pupilrightxml,
       t1.echoleftxml,
       t1.echorightxml,
       t1.indxml,
       t1.inixml,
       t1.outuxml,
       t1.outsxml,
       t1.outvxml,
       t1.outexml,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_last,
       t2.heartrhythm_last,
       t2.heartfrequency_last,
       t2.bloodoxygensaturation_last,
       t2.bedsidebloodsugar_last,
       t2.breath_last,
       t2.pulse_last,
       t2.bloodpressures_last,
       t2.bloodpressurea_last,
       t2.pupilleft_last,
       t2.pupilright_last,
       t2.echoleft_last,
       t2.echoright_last,
       t2.ind_last,
       t2.ini_last,
       t2.outu_last,
       t2.outs_last,
       t2.outv_last,
       t2.oute_last,
       t3.temperature,
       t3.heartrhythm,
       t3.heartfrequency,
       t3.bloodoxygensaturation,
       t3.bedsidebloodsugar,
       t3.breath,
       t3.pulse,
       t3.bloodpressures,
       t3.bloodpressurea,
       t3.pupilleft,
       t3.pupilright,
       t3.echoleft,
       t3.echoright,
       t3.ind,
       t3.ini,
       t3.outu,
       t3.outs,
       t3.outv,
       t3.oute
  from watchitemrecord t1
 inner join watchitemrecordcontent t2 on t1.inpatientid = t2.inpatientid
                                     and t1.inpatientdate =
                                         t2.inpatientdate
                                     and t1.opendate = t2.opendate
 inner join watchitemrecordallcontent t3 on t1.inpatientid = t3.inpatientid
                                        and t1.inpatientdate =
                                            t3.inpatientdate
                                        and t1.opendate = t3.opendate
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.createdate, t2.modifydate";

        private const string c_strGetModifyRecordSQL = "";

        /// <summary>
        ///  从WatchItemRecord获取删除表单的主要信息。
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from watchitemrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

        /// <summary>
        ///  从WatchItemRecord删除表单的主要信息。
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update watchitemrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";


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
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWatchItemTrackService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_intRecordTypeArr == null || p_dtmOpenDateArr == null || p_intRecordTypeArr.Length != p_dtmOpenDateArr.Length || p_dtmFirstPrintDate == DateTime.MinValue)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[4];
                long lngEff = 0;
                for (int i = 0 ; i < p_dtmOpenDateArr.Length ; i++)
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
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL_Normal, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
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
            out clsWatchItemDataInfo p_objTansDataInfo)
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
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strRecordOpenDate;

                //观察项目记录，使用c_strGetRecordContentSQL

                //按顺序给IDataParameter赋值

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsSubWatchItemRecordContent objRecordContent = null;
                    clsWatchItemDataInfo objInfo = null;

                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        //生成 clsWatchItemDataInfo
                        objInfo = new clsWatchItemDataInfo();
                        objInfo.m_intFlag = (int)enmRecordsType.WatchItem;   //因为可肯定为观察项目记录，所以可设任意值
                        //设置结果到 objInfo.m_objRecordContent
                        //					objInfo.m_objRecordContent = objRecordContent;
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date == dtmOpenDate)
                        {
                            #region 	从DataTable.Rows中获取结果 	\
                            objRecordContent = new clsSubWatchItemRecordContent();
                            objRecordContent.m_strInPatientID = p_strInPatientID;
                            objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                            objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                            objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                            if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                                objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                            objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
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

                            objRecordContent.m_strTemperature = dtbValue.Rows[j]["TEMPERATURE_LAST"].ToString();
                            objRecordContent.m_strTemperatureAll = dtbValue.Rows[j]["TEMPERATURE"].ToString();
                            objRecordContent.m_strTemperatureXML = dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
                            objRecordContent.m_strHeartRhythm = dtbValue.Rows[j]["HEARTRHYTHM_LAST"].ToString();
                            objRecordContent.m_strHeartRhythmAll = dtbValue.Rows[j]["HEARTRHYTHM"].ToString();
                            objRecordContent.m_strHeartRhythmXML = dtbValue.Rows[j]["HEARTRHYTHMXML"].ToString();
                            objRecordContent.m_strHeartFrequency = dtbValue.Rows[j]["HEARTFREQUENCY_LAST"].ToString();
                            objRecordContent.m_strHeartFrequencyAll = dtbValue.Rows[j]["HEARTFREQUENCY"].ToString();
                            objRecordContent.m_strHeartFrequencyXML = dtbValue.Rows[j]["HEARTFREQUENCYXML"].ToString();
                            objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[j]["BLOODOXYGENSATURATION_LAST"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[j]["BLOODOXYGENSATURATION"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[j]["BLOODOXYGENSATURATIONXML"].ToString();
                            objRecordContent.m_strBedsideBloodSugar = dtbValue.Rows[j]["BEDSIDEBLOODSUGAR_LAST"].ToString();
                            objRecordContent.m_strBedsideBloodSugarAll = dtbValue.Rows[j]["BEDSIDEBLOODSUGAR"].ToString();
                            objRecordContent.m_strBedsideBloodSugarXML = dtbValue.Rows[j]["BEDSIDEBLOODSUGARXML"].ToString();
                            objRecordContent.m_strBreath = dtbValue.Rows[j]["BREATH_LAST"].ToString();
                            objRecordContent.m_strBreathAll = dtbValue.Rows[j]["BREATH"].ToString();
                            objRecordContent.m_strBreathXML = dtbValue.Rows[j]["BREATHXML"].ToString();
                            objRecordContent.m_strPulse = dtbValue.Rows[j]["PULSE_LAST"].ToString();
                            objRecordContent.m_strPulseAll = dtbValue.Rows[j]["PULSE"].ToString();
                            objRecordContent.m_strPulseXML = dtbValue.Rows[j]["PULSEXML"].ToString();
                            objRecordContent.m_strBloodPressureS = dtbValue.Rows[j]["BLOODPRESSURES_LAST"].ToString();
                            objRecordContent.m_strBloodPressureSAll = dtbValue.Rows[j]["BLOODPRESSURES"].ToString();
                            objRecordContent.m_strBloodPressureSXML = dtbValue.Rows[j]["BLOODPRESSURESXML"].ToString();
                            objRecordContent.m_strBloodPressureA = dtbValue.Rows[j]["BLOODPRESSUREA_LAST"].ToString();
                            objRecordContent.m_strBloodPressureAAll = dtbValue.Rows[j]["BLOODPRESSUREA"].ToString();
                            objRecordContent.m_strBloodPressureAXML = dtbValue.Rows[j]["BLOODPRESSUREAXML"].ToString();
                            objRecordContent.m_strPupilLeft = dtbValue.Rows[j]["PUPILLEFT_LAST"].ToString();
                            objRecordContent.m_strPupilLeftAll = dtbValue.Rows[j]["PUPILLEFT"].ToString();
                            objRecordContent.m_strPupilLeftXML = dtbValue.Rows[j]["PUPILLEFTXML"].ToString();
                            objRecordContent.m_strPupilRight = dtbValue.Rows[j]["PUPILRIGHT_LAST"].ToString();
                            objRecordContent.m_strPupilRightAll = dtbValue.Rows[j]["PUPILRIGHT"].ToString();
                            objRecordContent.m_strPupilRightXML = dtbValue.Rows[j]["PUPILRIGHTXML"].ToString();
                            objRecordContent.m_strEchoLeft = dtbValue.Rows[j]["ECHOLEFT_LAST"].ToString();
                            objRecordContent.m_strEchoLeftAll = dtbValue.Rows[j]["ECHOLEFT"].ToString();
                            objRecordContent.m_strEchoLeftXML = dtbValue.Rows[j]["ECHOLEFTXML"].ToString();
                            objRecordContent.m_strEchoRight = dtbValue.Rows[j]["ECHORIGHT_LAST"].ToString();
                            objRecordContent.m_strEchoRightAll = dtbValue.Rows[j]["ECHORIGHT"].ToString();
                            objRecordContent.m_strEchoRightXML = dtbValue.Rows[j]["ECHORIGHTXML"].ToString();
                            objRecordContent.m_intInD = Convert.ToInt32(dtbValue.Rows[j]["IND_LAST"]);
                            objRecordContent.m_strInDAll = dtbValue.Rows[j]["IND"].ToString();
                            objRecordContent.m_strInDXML = dtbValue.Rows[j]["INDXML"].ToString();
                            objRecordContent.m_intInI = Convert.ToInt32(dtbValue.Rows[j]["INI_LAST"]);
                            objRecordContent.m_strInIAll = dtbValue.Rows[j]["INI"].ToString();
                            objRecordContent.m_strInIXML = dtbValue.Rows[j]["INIXML"].ToString();
                            objRecordContent.m_intOutU = Convert.ToInt32(dtbValue.Rows[j]["OUTU_LAST"]);
                            objRecordContent.m_strOutUAll = dtbValue.Rows[j]["OUTU"].ToString();
                            objRecordContent.m_strOutUXML = dtbValue.Rows[j]["OUTUXML"].ToString();
                            objRecordContent.m_intOutS = Convert.ToInt32(dtbValue.Rows[j]["OUTS_LAST"]);
                            objRecordContent.m_strOutSAll = dtbValue.Rows[j]["OUTS"].ToString();
                            objRecordContent.m_strOutSXML = dtbValue.Rows[j]["OUTSXML"].ToString();
                            objRecordContent.m_intOutV = Convert.ToInt32(dtbValue.Rows[j]["OUTV_LAST"]);
                            objRecordContent.m_strOutVAll = dtbValue.Rows[j]["OUTV"].ToString();
                            objRecordContent.m_strOutVXML = dtbValue.Rows[j]["OUTVXML"].ToString();
                            objRecordContent.m_intOutE = Convert.ToInt32(dtbValue.Rows[j]["OUTE_LAST"]);
                            objRecordContent.m_strOutEAll = dtbValue.Rows[j]["OUTE"].ToString();
                            objRecordContent.m_strOutEXML = dtbValue.Rows[j]["OUTEXML"].ToString();


                            //同一条记录的修改,保存在arlModifyData 
                            arlModifyData.Add(objRecordContent);
                            j++;
                            #endregion
                        }
                        //后移一条记录，使循环从新的OpenData开始。
                        j--;

                        objInfo.m_objTransDataArr = (clsSubWatchItemRecordContent[])arlModifyData.ToArray(typeof(clsSubWatchItemRecordContent));
                        arlModifyData.Clear();

                        //最后一条记录
                        objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length - 1];

                        arlTransData.Add(objInfo);
                    }
                }
                //返回结果到p_objTansDataInfo
                p_objTansDataInfo = ((clsWatchItemDataInfo[])arlTransData.ToArray(typeof(clsWatchItemDataInfo)))[0];

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

        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objWatchItemInfoArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsTransDataInfo[] p_objWatchItemInfoArr)
        {
            p_objWatchItemInfoArr = null;

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                ArrayList arlTransData = new ArrayList();
                ArrayList arlModifyData = new ArrayList();
                ArrayList arlTransDataClone = new ArrayList();
                clsWatchItemDataInfo objAppendInfo = null;
                DateTime dtmOpenDate;
                DateTime dtmCreateDate_Date;
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //观察项目记录，使用c_strGetRecordContentSQL

                //按顺序给IDataParameter赋值

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsSubWatchItemRecordContent objRecordContent = null;
                    clsWatchItemDataInfo objInfo = null;
                    clsWatchItemSummary[] m_objSummaryArr;
                    long m_lngRes = m_lngGetSummaryRecords(p_strInPatientID, p_strInPatientDate, p_objHRPServ, out m_objSummaryArr);
                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        //生成 clsWatchItemDataInfo
                        objInfo = new clsWatchItemDataInfo();
                        objInfo.m_intFlag = (int)enmRecordsType.WatchItem;//因为可肯定为观察项目记录，所以可设任意值
                        //设置结果到 objInfo.m_objRecordContent
                        //					objInfo.m_objRecordContent = objRecordContent;
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate

                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        #region While
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()) == dtmOpenDate)
                        {
                            //从DataTable.Rows中获取结果    
                            objRecordContent = new clsSubWatchItemRecordContent();
                            objRecordContent.m_strInPatientID = p_strInPatientID;
                            objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                            objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                            objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                            if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                                objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                            objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
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

                            objRecordContent.m_strTemperature = dtbValue.Rows[j]["TEMPERATURE_LAST"].ToString();
                            objRecordContent.m_strTemperatureAll = dtbValue.Rows[j]["TEMPERATURE"].ToString();
                            objRecordContent.m_strTemperatureXML = dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
                            objRecordContent.m_strHeartRhythm = dtbValue.Rows[j]["HEARTRHYTHM_LAST"].ToString();
                            objRecordContent.m_strHeartRhythmAll = dtbValue.Rows[j]["HEARTRHYTHM"].ToString();
                            objRecordContent.m_strHeartRhythmXML = dtbValue.Rows[j]["HEARTRHYTHMXML"].ToString();
                            objRecordContent.m_strHeartFrequency = dtbValue.Rows[j]["HEARTFREQUENCY_LAST"].ToString();
                            objRecordContent.m_strHeartFrequencyAll = dtbValue.Rows[j]["HEARTFREQUENCY"].ToString();
                            objRecordContent.m_strHeartFrequencyXML = dtbValue.Rows[j]["HEARTFREQUENCYXML"].ToString();
                            objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[j]["BLOODOXYGENSATURATION_LAST"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[j]["BLOODOXYGENSATURATION"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[j]["BLOODOXYGENSATURATIONXML"].ToString();
                            objRecordContent.m_strBedsideBloodSugar = dtbValue.Rows[j]["BEDSIDEBLOODSUGAR_LAST"].ToString();
                            objRecordContent.m_strBedsideBloodSugarAll = dtbValue.Rows[j]["BEDSIDEBLOODSUGAR"].ToString();
                            objRecordContent.m_strBedsideBloodSugarXML = dtbValue.Rows[j]["BEDSIDEBLOODSUGARXML"].ToString();
                            objRecordContent.m_strBreath = dtbValue.Rows[j]["BREATH_LAST"].ToString();
                            objRecordContent.m_strBreathAll = dtbValue.Rows[j]["BREATH"].ToString();
                            objRecordContent.m_strBreathXML = dtbValue.Rows[j]["BREATHXML"].ToString();
                            objRecordContent.m_strPulse = dtbValue.Rows[j]["PULSE_LAST"].ToString();
                            objRecordContent.m_strPulseAll = dtbValue.Rows[j]["PULSE"].ToString();
                            objRecordContent.m_strPulseXML = dtbValue.Rows[j]["PULSEXML"].ToString();
                            objRecordContent.m_strBloodPressureS = dtbValue.Rows[j]["BLOODPRESSURES_LAST"].ToString();
                            objRecordContent.m_strBloodPressureSAll = dtbValue.Rows[j]["BLOODPRESSURES"].ToString();
                            objRecordContent.m_strBloodPressureSXML = dtbValue.Rows[j]["BLOODPRESSURESXML"].ToString();
                            objRecordContent.m_strBloodPressureA = dtbValue.Rows[j]["BLOODPRESSUREA_LAST"].ToString();
                            objRecordContent.m_strBloodPressureAAll = dtbValue.Rows[j]["BLOODPRESSUREA"].ToString();
                            objRecordContent.m_strBloodPressureAXML = dtbValue.Rows[j]["BLOODPRESSUREAXML"].ToString();
                            objRecordContent.m_strPupilLeft = dtbValue.Rows[j]["PUPILLEFT_LAST"].ToString();
                            objRecordContent.m_strPupilLeftAll = dtbValue.Rows[j]["PUPILLEFT"].ToString();
                            objRecordContent.m_strPupilLeftXML = dtbValue.Rows[j]["PUPILLEFTXML"].ToString();
                            objRecordContent.m_strPupilRight = dtbValue.Rows[j]["PUPILRIGHT_LAST"].ToString();
                            objRecordContent.m_strPupilRightAll = dtbValue.Rows[j]["PUPILRIGHT"].ToString();
                            objRecordContent.m_strPupilRightXML = dtbValue.Rows[j]["PUPILRIGHTXML"].ToString();
                            objRecordContent.m_strEchoLeft = dtbValue.Rows[j]["ECHOLEFT_LAST"].ToString();
                            objRecordContent.m_strEchoLeftAll = dtbValue.Rows[j]["ECHOLEFT"].ToString();
                            objRecordContent.m_strEchoLeftXML = dtbValue.Rows[j]["ECHOLEFTXML"].ToString();
                            objRecordContent.m_strEchoRight = dtbValue.Rows[j]["ECHORIGHT_LAST"].ToString();
                            objRecordContent.m_strEchoRightAll = dtbValue.Rows[j]["ECHORIGHT"].ToString();
                            objRecordContent.m_strEchoRightXML = dtbValue.Rows[j]["ECHORIGHTXML"].ToString();
                            objRecordContent.m_intInD = Convert.ToInt32(dtbValue.Rows[j]["IND_LAST"]);
                            objRecordContent.m_strInDAll = dtbValue.Rows[j]["IND"].ToString();
                            objRecordContent.m_strInDXML = dtbValue.Rows[j]["INDXML"].ToString();
                            objRecordContent.m_intInI = Convert.ToInt32(dtbValue.Rows[j]["INI_LAST"]);
                            objRecordContent.m_strInIAll = dtbValue.Rows[j]["INI"].ToString();
                            objRecordContent.m_strInIXML = dtbValue.Rows[j]["INIXML"].ToString();
                            objRecordContent.m_intOutU = Convert.ToInt32(dtbValue.Rows[j]["OUTU_LAST"]);
                            objRecordContent.m_strOutUAll = dtbValue.Rows[j]["OUTU"].ToString();
                            objRecordContent.m_strOutUXML = dtbValue.Rows[j]["OUTUXML"].ToString();
                            objRecordContent.m_intOutS = Convert.ToInt32(dtbValue.Rows[j]["OUTS_LAST"]);
                            objRecordContent.m_strOutSAll = dtbValue.Rows[j]["OUTS"].ToString();
                            objRecordContent.m_strOutSXML = dtbValue.Rows[j]["OUTSXML"].ToString();
                            objRecordContent.m_intOutV = Convert.ToInt32(dtbValue.Rows[j]["OUTV_LAST"]);
                            objRecordContent.m_strOutVAll = dtbValue.Rows[j]["OUTV"].ToString();
                            objRecordContent.m_strOutVXML = dtbValue.Rows[j]["OUTVXML"].ToString();
                            objRecordContent.m_intOutE = Convert.ToInt32(dtbValue.Rows[j]["OUTE_LAST"]);
                            objRecordContent.m_strOutEAll = dtbValue.Rows[j]["OUTE"].ToString();
                            objRecordContent.m_strOutEXML = dtbValue.Rows[j]["OUTEXML"].ToString();

                            //同一条记录的修改,保存在arlModifyData 
                            arlModifyData.Add(objRecordContent);
                            j++;
                        } //End While
                        #endregion While

                        //后移一条记录，使循环从新的OpenData开始。
                        j--;

                        objInfo.m_objTransDataArr = (clsSubWatchItemRecordContent[])arlModifyData.ToArray(typeof(clsSubWatchItemRecordContent));
                        for (int w1 = 1 ; w1 < objInfo.m_objTransDataArr.Length ; w1++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
                        {
                            if (objInfo.m_objTransDataArr[w1 - 1].m_strModifyUserName == objInfo.m_objTransDataArr[w1].m_strModifyUserName)
                                objInfo.m_objTransDataArr[w1 - 1].m_strModifyUserName = "　";//全角空格字符
                        }
                        arlModifyData.Clear();

                        //最后一条记录
                        objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length - 1];

                        arlTransData.Add(objInfo);
                    }

                    dtmCreateDate_Date = ((clsWatchItemDataInfo)arlTransData[0]).m_objRecordContent.m_dtmCreateDate.Date;
                    arlTransDataClone = (ArrayList)arlTransData.Clone();
                    if (arlTransData.Count == 1)//只有一条记录时
                    {
                        objAppendInfo = new clsWatchItemDataInfo();
                        objAppendInfo.m_intFlag = 0;
                        objAppendInfo.m_objRecordContent = new clsSubWatchItemRecordContent();
                        //objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[0].m_strCreateDate + " 23:59:59.998");
                        objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse((DateTime.Parse(m_objSummaryArr[0].m_strCreateDate).ToString("yyyy-MM-dd")) + " 23:59:59.998");
                        objAppendInfo.m_objItemSummary = m_objSummaryArr[0];
                        arlTransDataClone.Add(objAppendInfo);

                        //###########统计全部记录总共的内容
                        objAppendInfo = new clsWatchItemDataInfo();
                        objAppendInfo.m_intFlag = 0;
                        objAppendInfo.m_objRecordContent = new clsSubWatchItemRecordContent();
                        objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
                        objAppendInfo.m_objItemSummary = m_objSummaryArr[0];
                        arlTransDataClone.Add(objAppendInfo);
                        //#############
                    }
                    else//多于一条记录时
                    {
                        try
                        {
                            int m_intSummaryArrIndex = 0;
                            for (int i1 = 1 ; i1 < arlTransData.Count ; i1++)
                            {
                                if (dtmCreateDate_Date != ((clsWatchItemDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date)
                                {
                                    objAppendInfo = new clsWatchItemDataInfo();
                                    objAppendInfo.m_intFlag = 0;
                                    objAppendInfo.m_objRecordContent = new clsSubWatchItemRecordContent();
                                    //objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[m_intSummaryArrIndex].m_strCreateDate + " 23:59:59.998");
                                    objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse((DateTime.Parse(m_objSummaryArr[m_intSummaryArrIndex].m_strCreateDate).ToString("yyyy-MM-dd")) + " 23:59:59.998");
                                    objAppendInfo.m_objItemSummary = m_objSummaryArr[m_intSummaryArrIndex];
                                    arlTransDataClone.Insert(i1 + m_intSummaryArrIndex, objAppendInfo);
                                    dtmCreateDate_Date = ((clsWatchItemDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date;
                                    m_intSummaryArrIndex++;
                                }
                            }
                            objAppendInfo = new clsWatchItemDataInfo();
                            objAppendInfo.m_intFlag = 0;
                            objAppendInfo.m_objRecordContent = new clsSubWatchItemRecordContent();
                            //objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[m_objSummaryArr.Length - 1].m_strCreateDate + " 23:59:59.998");
                            objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse((DateTime.Parse(m_objSummaryArr[m_objSummaryArr.Length - 1].m_strCreateDate).ToString("yyyy-MM-dd")) + " 23:59:59.998");
                            objAppendInfo.m_objItemSummary = m_objSummaryArr[m_objSummaryArr.Length - 1];
                            arlTransDataClone.Add(objAppendInfo);

                            //###########统计全部记录总共的内容
                            clsWatchItemDataInfo objTotalDataInfo;
                            clsWatchItemSummary m_objSummary = new clsWatchItemSummary();
                            for (int i1 = 0 ; i1 < arlTransDataClone.Count ; i1++)
                            {
                                objTotalDataInfo = (clsWatchItemDataInfo)arlTransDataClone[i1];
                                if (objTotalDataInfo.m_intFlag != (int)enmRecordsType.WatchItem)
                                {
                                    m_objSummary.m_intInD_Total += objTotalDataInfo.m_objItemSummary.m_intInD_Total;
                                    m_objSummary.m_intInI_Total += objTotalDataInfo.m_objItemSummary.m_intInI_Total;
                                    m_objSummary.m_intOutE_Total += objTotalDataInfo.m_objItemSummary.m_intOutE_Total;
                                    m_objSummary.m_intOutS_Total += objTotalDataInfo.m_objItemSummary.m_intOutS_Total;
                                    m_objSummary.m_intOutV_Total += objTotalDataInfo.m_objItemSummary.m_intOutV_Total;
                                    m_objSummary.m_intOutU_Total += objTotalDataInfo.m_objItemSummary.m_intOutU_Total;
                                    m_objSummary.m_intTotal_In += objTotalDataInfo.m_objItemSummary.m_intTotal_In;
                                    m_objSummary.m_intTotal_Out += objTotalDataInfo.m_objItemSummary.m_intTotal_Out;
                                }
                            }
                            objAppendInfo = new clsWatchItemDataInfo();
                            objAppendInfo.m_intFlag = 0;
                            objAppendInfo.m_objRecordContent = new clsSubWatchItemRecordContent();
                            objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
                            objAppendInfo.m_objItemSummary = m_objSummary;
                            arlTransDataClone.Add(objAppendInfo);
                            //#############
                        }
                        catch (Exception err)
                        {
                            string m_Str = err.Message + "\r\n" + err.StackTrace;
                        }
                    }
                }
                //返回结果到p_objTansDataInfoArr
                p_objWatchItemInfoArr = (clsWatchItemDataInfo[])arlTransDataClone.ToArray(typeof(clsWatchItemDataInfo));
                for (int w2 = 0 ; w2 < p_objWatchItemInfoArr.Length ; w2++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
                {
                    clsSubWatchItemRecordContent[] objTempAInfoArr = ((clsWatchItemDataInfo)p_objWatchItemInfoArr[w2]).m_objTransDataArr;
                    if (objTempAInfoArr != null)
                    {
                        for (int w3 = w2 + 1 ; w3 < p_objWatchItemInfoArr.Length ; w3++)
                        {
                            clsSubWatchItemRecordContent[] objTempBInfoArr = ((clsWatchItemDataInfo)p_objWatchItemInfoArr[w3]).m_objTransDataArr;
                            if (objTempBInfoArr == null) continue;
                            if (objTempAInfoArr[objTempAInfoArr.Length - 1].m_dtmCreateDate.Date == objTempBInfoArr[0].m_dtmCreateDate.Date)
                            {
                                string strTempName = "";
                                for (int w4 = 0 ; w4 < objTempBInfoArr.Length ; w4++)
                                {
                                    if (objTempBInfoArr[w4].m_strModifyUserName != "　")//全角空格字符
                                    {
                                        strTempName = objTempBInfoArr[w4].m_strModifyUserName;
                                        break;
                                    }
                                }
                                if (objTempAInfoArr[objTempAInfoArr.Length - 1].m_strModifyUserName == strTempName)
                                    objTempAInfoArr[objTempAInfoArr.Length - 1].m_strModifyUserName = "　";//全角空格字符
                                break;
                            }
                            else break;
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

            }
            return lngRes;
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

            /// <summary>
            ///  从WatchItemRecordContent获取指定表单的最后修改时间。
            /// </summary>
            string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from watchitemrecord t1,watchitemrecordcontent t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[3];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
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
            finally
            {
                //objHRPServ.Dispose();

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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
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

        /// <summary>
        /// 获得统计记录内容
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objSummaryItemInfoArr"></param>
        /// <returns></returns>
        [AutoComplete]
    private long m_lngGetSummaryRecords(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsWatchItemSummary[] p_objSummaryItemInfoArr)
        {
            p_objSummaryItemInfoArr = null;

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            #region SQL
            string c_strGetSummarySQL = "";
            if (clsHRPTableService.bytDatabase_Selector == 2)
            {
                c_strGetSummarySQL = @"select createdate_date,
       sum(ind_last) as ind_total,
       sum(ini_last) as ini_total,
       sum(outu_last) as outu_total,
       sum(outs_last) as outs_total,
       sum(outv_last) as outv_total,
       sum(oute_last) as oute_total,
       sum(total_in) as total_in,
       sum(total_out) as total_out
  from (select to_date(to_char(v2.createdate, 'yyyy-mm-dd'), 'yyyy-mm-dd') as createdate_date,
               v2.createdate,
               v2.ind_last,
               v2.ini_last,
               v2.outu_last,
               outs_last,
               v2.outv_last,
               v2.oute_last,
               (v2.ind_last + v2.ini_last) as total_in,
               (v2.outu_last + outs_last + v2.outv_last + v2.oute_last) as total_out
          from (select opendate, max(modifydate) as lastmodifydate
                  from watchitemrecordcontent
                 where inpatientid = ?
                   and inpatientdate = ?
                 group by opendate) v1,
               (select t1.createdate,
                       t2.inpatientid,
                       t2.inpatientdate,
                       t2.opendate,
                       t2.modifydate,
                       t2.ind_last,
                       t2.ini_last,
                       t2.outu_last,
                       t2.outs_last,
                       t2.outv_last,
                       t2.oute_last
                  from watchitemrecord t1, watchitemrecordcontent t2
                 where t1.inpatientid = ?
                   and t1.inpatientdate = ?
                   and t1.inpatientid = t2.inpatientid
                   and t1.inpatientdate = t2.inpatientdate
                   and t1.opendate = t2.opendate
                   and status = 0) v2
         where v2.inpatientid = ?
           and v2.inpatientdate = ?
           and v1.opendate = v2.opendate
           and v1.lastmodifydate = v2.modifydate) v3
 group by createdate_date
 order by createdate_date";
            }
            else
            {
                c_strGetSummarySQL = @"select createdate_date,
       sum(ind_last) as ind_total,
       sum(ini_last) as ini_total,
       sum(outu_last) as outu_total,
       sum(outs_last) as outs_total,
       sum(outv_last) as outv_total,
       sum(oute_last) as oute_total,
       sum(total_in) as total_in,
       sum(total_out) as total_out
  from (select convert(char(10), v2.createdate, 120) as createdate_date,
               v2.createdate,
               v2.ind_last,
               v2.ini_last,
               v2.outu_last,
               outs_last,
               v2.outv_last,
               v2.oute_last,
               (v2.ind_last + v2.ini_last) as total_in,
               (v2.outu_last + outs_last + v2.outv_last + v2.oute_last) as total_out
          from (select opendate, max(modifydate) as lastmodifydate
                  from watchitemrecordcontent
                 where inpatientid = ?
                   and inpatientdate = ?
                 group by opendate) as v1,
               (select t1.createdate,
                       t2.inpatientid,
                       t2.inpatientdate,
                       t2.opendate,
                       t2.modifydate,
                       t2.ind_last,
                       t2.ini_last,
                       t2.outu_last,
                       t2.outs_last,
                       t2.outv_last,
                       t2.oute_last
                  from watchitemrecord as t1, watchitemrecordcontent as t2
                 where t1.inpatientid = ?
                   and t1.inpatientdate = ?
                   and t1.inpatientid = t2.inpatientid
                   and t1.inpatientdate = t2.inpatientdate
                   and t1.opendate = t2.opendate
                   and status = 0) as v2
         where v2.inpatientid = ?
           and v2.inpatientdate = ?
           and v1.opendate = v2.opendate
           and v1.lastmodifydate = v2.modifydate) as v3
 group by createdate_date
 order by createdate_date";
            }
            #endregion

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[6];
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].Value = p_strInPatientID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetSummarySQL, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objSummaryItemInfoArr = new clsWatchItemSummary[dtbValue.Rows.Count];
                    for (int i1 = 0 ; i1 < dtbValue.Rows.Count ; i1++)
                    {
                        p_objSummaryItemInfoArr[i1] = new clsWatchItemSummary();
                        p_objSummaryItemInfoArr[i1].m_strCreateDate = dtbValue.Rows[i1]["CREATEDATE_DATE"].ToString();
                        p_objSummaryItemInfoArr[i1].m_intInD_Total = Convert.ToInt32(dtbValue.Rows[i1]["IND_TOTAL"]);
                        p_objSummaryItemInfoArr[i1].m_intInI_Total = Convert.ToInt32(dtbValue.Rows[i1]["INI_TOTAL"]);
                        p_objSummaryItemInfoArr[i1].m_intOutU_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTU_TOTAL"]);
                        p_objSummaryItemInfoArr[i1].m_intOutS_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTS_TOTAL"]);
                        p_objSummaryItemInfoArr[i1].m_intOutV_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTV_TOTAL"]);
                        p_objSummaryItemInfoArr[i1].m_intOutE_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTE_TOTAL"]);
                        p_objSummaryItemInfoArr[i1].m_intTotal_In = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_IN"]);
                        p_objSummaryItemInfoArr[i1].m_intTotal_Out = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_OUT"]);
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
    }
}
