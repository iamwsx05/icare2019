using System;
using System.Collections.Generic;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;
using com.digitalwave.PublicMiddleTier;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 通用ICU护理记录单service
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsICUNurseService : clsDiseaseTrackService
    {


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
           //未实现
            return lngRes;
        }
        #endregion

        #region 更新数据库中的首次打印时间
        /// <summary>
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">入院登记号</param>
        /// <param name="p_strInPatientDate">记录时间</param>
        /// <param name="p_strOpenDate">弃用</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterID,
            string p_strCteateDate,
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
                if (p_strRegisterID == null || p_strRegisterID == "" || p_strCteateDate == null || p_strCteateDate == null)
                    return (long)enmOperationResult.Parameter_Error;
                /// <summary>
                string c_strUpdateFirstPrintDateSQL = @"update t_emr_icunurse t
                                                               set t.firstprintdate_dat =?
                                                             where t.registerid_vchr = ?
                                                               and t.createdate_dat =?
															   and t.status_int=1";
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCteateDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
            long lngRes = 0;
            //未实现
            //返回
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除全部记录时间列表
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
            //未实现
            //返回
            return lngRes;
        }
        #endregion

        #region 获取指定记录的内容
        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">入院登记号</param>
        /// <param name="p_strInPatientDate">创建时间</param>
        /// <param name="p_strOpenDate">弃用</param>
        /// <param name="p_objHRPServ">弃用出入null</param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strRegisterID,
            string p_strCreateDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //检查参数
            if (p_strRegisterID == null || p_strRegisterID == "" || p_strCreateDate == null || p_strCreateDate == "")
                return (long)enmOperationResult.Parameter_Error;
            #region SQL
            string c_strGetRecordContentSQL = @"select t.registerid_vchr,
       t.createdate_dat,
       t.createuserid_vchr,
       t.deactiveddate_dat,
       t.deactivedoperatorid_vchr,
       t.firstprintdate_dat,
       t.sequence_int,
       t.status_int,
       t.recorduserid_vchr,
       t.recorddate_dat,
       t.ifconfirm_int,
       t.temperature,
       t.temperaturexml,
       t.endtemperature,
       t.endtemperaturexml,
       t.afteropdays,
       t.hr,
       t.hrxml,
       t.rhythm,
       t.rhythmxml,
       t.bps,
       t.bpsxml,
       t.bpd,
       t.bpdxml,
       t.abp,
       t.abpxml,
       t.cvp,
       t.cvpxml,
       t.spo2,
       t.spo2xml,
       t.consciousness,
       t.consciousnessxml,
       t.pupil,
       t.pupilxml,
       t.pupir,
       t.pupirxml,
       t.lightreflexl,
       t.lightreflexlxml,
       t.lightreflexr,
       t.lightreflexrxml,
       t.vasoaactive,
       t.vasoaactivexml,
       t.cardiacdiuretic,
       t.cardiacdiureticxml,
       t.liquid1,
       t.liquid1xml,
       t.liquid2,
       t.liquid2xml,
       t.liquid3,
       t.liquid3xml,
       t.liquid4,
       t.liquid4xml,
       t.liquid5,
       t.liquid5xml,
       t.fbool,
       t.fboolxml,
       t.plasma,
       t.plasmaxml,
       t.nose1,
       t.nose1xml,
       t.nose2,
       t.nose2xml,
       t.inperhour,
       t.inperhourxml,
       t.intotal,
       t.intotalxml,
       t.stool,
       t.stoolxml,
       t.stooltotal,
       t.stooltotalxml,
       t.piss,
       t.pissxml,
       t.pisstotal,
       t.pisstotalxml,
       t.drain1,
       t.drain1xml,
       t.drain2,
       t.drain2xml,
       t.drain3,
       t.drain3xml,
       t.drain4,
       t.drain4xml,
       t.drain5,
       t.drain5xml,
       t.outperhour,
       t.outperhourxml,
       t.outtotal,
       t.outtotalxml,
       t.markstatus,
       t.respiration,
       t.respirationxml,
       t.transferid_chr,
       d.posture,
       d.posturexml,
       d.skin,
       d.skinxml,
       d.sputum,
       d.sputumxml,
       d.sputum1,
       d.sputum1xml,
       d.sucker,
       d.suckerxml,
       d.gesticulation,
       d.gesticulationxml,
       d.oral,
       d.oralxml,
       d.perineum,
       d.perineumxml,
       d.spongebath,
       d.spongebathxml,
       d.pt,
       d.ptxml,
       d.act,
       d.actxml,
       d.glu,
       d.gluxml,
       d.k,
       d.kxml,
       d.naplus,
       d.naplusxml,
       d.cl,
       d.clxml,
       d.caplus2,
       d.caplus2xml,
       d.mmodel,
       d.mmodelxml,
       d.pee,
       d.peexml,
       d.tvti,
       d.tvtixml,
       d.frequency,
       d.frequencyxml,
       d.o2,
       d.o2xml,
       d.senseiive,
       d.senseiivexml,
       d.speed,
       d.speedxml,
       d.ie,
       d.iexml,
       d.gaspress,
       d.gaspressxml,
       d.tv,
       d.tvxml,
       d.mv,
       d.mvxml,
       d.pipedepth,
       d.pipedepthxml,
       d.aircellpress,
       d.aircellpressxml,
       d.etco2,
       d.etco2xml,
       d.ph,
       d.phxml,
       d.pco2,
       d.pco2xml,
       d.pao2,
       d.pao2xml,
       d.hco3,
       d.hco3xml,
       d.be,
       d.bexml,
       d.customer1,
       d.customer1xml,
       d.customer2,
       d.customer2xml,
       k.content,
       k.contentxml
  from t_emr_icunurse t
 inner join t_emr_icunursesub d on t.registerid_vchr = d.registerid_vchr
                               and t.createdate_dat = d.createdate_dat
  left outer join t_emr_icunurserecord k on t.registerid_vchr =
                                            k.registerid_vchr
                                        and t.createdate_dat =
                                            k.createdate_dat
                                        and k.status_int = 1
 where t.registerid_vchr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

            #endregion SQL

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                //生成DataTable
                DataTable dtbResult = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbResult, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbResult.Rows.Count == 1)
                {
                    DataRow objRow = dtbResult.Rows[0];
                    #region 设置结果
                    clsICUNurseRecord p_objResult = new clsICUNurseRecord();
                    p_objResult.objRecordContent = new clsICUNurseRecordContent();
                    #region 赋值
                    p_objResult.m_strRegisterID = p_strRegisterID;
                    p_objResult.m_dtmCreateDate = Convert.ToDateTime(objRow["CREATEDATE_DAT"]);
                    p_objResult.m_strCreateUserID = objRow["CREATEUSERID_VCHR"].ToString();
                    try
                    {
                        p_objResult.m_dtmDeActivedDate = Convert.ToDateTime(objRow["DEACTIVEDDATE_DAT"]);
                    }
                    catch (Exception)
                    {
                        p_objResult.m_dtmDeActivedDate = DateTime.Now;
                    }
                     p_objResult.m_strDeActivedOperatorID = objRow["DEACTIVEDOPERATORID_VCHR"].ToString();
                     try
                     {
                        p_objResult.m_dtmFirstPrintDate = Convert.ToDateTime(objRow["FIRSTPRINTDATE_DAT"]);
                     }
                     catch (Exception)
                     {
                         p_objResult.m_dtmFirstPrintDate = DateTime.Now;
                     }
                    try
                    {
                        p_objResult.m_bytStatus = Convert.ToInt32(objRow["STATUS_INT"].ToString());
                        p_objResult.m_bytIfConfirm = Convert.ToInt32(objRow["IFCONFIRM_INT"].ToString());

                    }
                    catch (Exception)
                    {
                        p_objResult.m_bytStatus =1;
                        p_objResult.m_bytIfConfirm = 0;

                    }
                    p_objResult.m_strRecordUserID = objRow["RECORDUSERID_VCHR"].ToString();
                    p_objResult.m_dtmRecordDate = Convert.ToDateTime(objRow["RECORDDATE_DAT"]);
                    p_objResult.m_strTEMPERATURE = objRow["TEMPERATURE"].ToString();
                    p_objResult.m_strTEMPERATUREXML = objRow["TEMPERATUREXML"].ToString();
                    p_objResult.m_strENDTEMPERATURE = objRow["ENDTEMPERATURE"].ToString();
                    p_objResult.m_strENDTEMPERATUREXML = objRow["ENDTEMPERATUREXML"].ToString();
                    p_objResult.m_strAFTEROPDAYS = objRow["AFTEROPDAYS"].ToString();
                    p_objResult.m_strHR = objRow["HR"].ToString();
                    p_objResult.m_strHRXML = objRow["HRXML"].ToString();
                    p_objResult.m_strRHYTHM = objRow["RHYTHM"].ToString();
                    p_objResult.m_strRHYTHMXML = objRow["RHYTHMXML"].ToString();
                    p_objResult.m_strBPD = objRow["BPD"].ToString();
                    p_objResult.m_strBPDXML = objRow["BPDXML"].ToString();
                    p_objResult.m_strBPS = objRow["BPS"].ToString();
                    p_objResult.m_strBPSXML = objRow["BPSXML"].ToString();
                    p_objResult.m_strABP = objRow["ABP"].ToString();
                    p_objResult.m_strABPXML = objRow["ABPXML"].ToString();
                    p_objResult.m_strCVP = objRow["CVP"].ToString();
                    p_objResult.m_strCVPXML = objRow["CVPXML"].ToString();
                    p_objResult.m_strSPO2 = objRow["SPO2"].ToString();
                    p_objResult.m_strSPO2XML = objRow["SPO2XML"].ToString();
                    p_objResult.m_strCONSCIOUSNESS = objRow["CONSCIOUSNESS"].ToString();
                    p_objResult.m_strCONSCIOUSNESSXML = objRow["CONSCIOUSNESSXML"].ToString();
                    p_objResult.m_strPUPIL = objRow["PUPIL"].ToString();
                    p_objResult.m_strPUPILXML = objRow["PUPILXML"].ToString();
                    p_objResult.m_strPUPIR = objRow["PUPIR"].ToString();
                    p_objResult.m_strPUPIRXML = objRow["PUPIRXML"].ToString();
                    p_objResult.m_strLIGHTREFLEXL = objRow["LIGHTREFLEXL"].ToString();
                    p_objResult.m_strLIGHTREFLEXLXML = objRow["LIGHTREFLEXLXML"].ToString();
                    p_objResult.m_strLIGHTREFLEXR = objRow["LIGHTREFLEXR"].ToString();
                    p_objResult.m_strLIGHTREFLEXRXML = objRow["LIGHTREFLEXRXML"].ToString();
                    p_objResult.m_strVASOAACTIVE = objRow["VASOAACTIVE"].ToString();
                    p_objResult.m_strVASOAACTIVEXML = objRow["VASOAACTIVEXML"].ToString();
                    p_objResult.m_strCARDIACDIURETIC = objRow["CARDIACDIURETIC"].ToString();
                    p_objResult.m_strCARDIACDIURETICXML = objRow["CARDIACDIURETICXML"].ToString();
                    p_objResult.m_strLIQUID1 = objRow["LIQUID1"].ToString();
                    p_objResult.m_strLIQUID1XML = objRow["LIQUID1XML"].ToString();
                    p_objResult.m_strLIQUID2 = objRow["LIQUID2"].ToString();
                    p_objResult.m_strLIQUID2XML = objRow["LIQUID2XML"].ToString();
                    p_objResult.m_strLIQUID3 = objRow["LIQUID3"].ToString();
                    p_objResult.m_strLIQUID3XML = objRow["LIQUID3XML"].ToString();
                    p_objResult.m_strLIQUID4 = objRow["LIQUID4"].ToString();
                    p_objResult.m_strLIQUID4XML = objRow["LIQUID4XML"].ToString();
                    p_objResult.m_strLIQUID5 = objRow["LIQUID5"].ToString();
                    p_objResult.m_strLIQUID5XML = objRow["LIQUID5XML"].ToString();
                    p_objResult.m_strFBOOL = objRow["FBOOL"].ToString();
                    p_objResult.m_strFBOOLXML = objRow["FBOOLXML"].ToString();
                    p_objResult.m_strPLASMA = objRow["PLASMA"].ToString();
                    p_objResult.m_strPLASMAXML = objRow["PLASMAXML"].ToString();
                    p_objResult.m_strNOSE1 = objRow["NOSE1"].ToString();
                    p_objResult.m_strNOSE1XML = objRow["NOSE1XML"].ToString();
                    p_objResult.m_strNOSE2 = objRow["NOSE2"].ToString();
                    p_objResult.m_strNOSE2XML = objRow["NOSE2XML"].ToString();
                    p_objResult.m_strINPERHOUR = objRow["INPERHOUR"].ToString();
                    p_objResult.m_strINPERHOURXML = objRow["INPERHOURXML"].ToString();
                    p_objResult.m_strINTOTAL = objRow["INTOTAL"].ToString();
                    p_objResult.m_strINTOTALXML = objRow["INTOTALXML"].ToString();
                    p_objResult.m_strSTOOL = objRow["STOOL"].ToString();
                    p_objResult.m_strSTOOLXML = objRow["STOOLXML"].ToString();
                    p_objResult.m_strSTOOLTOTAL = objRow["STOOLTOTAL"].ToString();
                    p_objResult.m_strSTOOLTOTALXML = objRow["STOOLTOTALXML"].ToString();
                    p_objResult.m_strPISS = objRow["PISS"].ToString();
                    p_objResult.m_strPISSXML = objRow["PISSXML"].ToString();
                    p_objResult.m_strPISSTOTAL = objRow["PISSTOTAL"].ToString();
                    p_objResult.m_strPISSTOTALXML = objRow["PISSTOTALXML"].ToString();
                    p_objResult.m_strDRAIN1 = objRow["DRAIN1"].ToString();
                    p_objResult.m_strDRAIN1XML = objRow["DRAIN1XML"].ToString();
                    p_objResult.m_strDRAIN2 = objRow["DRAIN2"].ToString();
                    p_objResult.m_strDRAIN2XML = objRow["DRAIN2XML"].ToString();
                    p_objResult.m_strDRAIN3 = objRow["DRAIN3"].ToString();
                    p_objResult.m_strDRAIN3XML = objRow["DRAIN3XML"].ToString();
                    p_objResult.m_strDRAIN4 = objRow["DRAIN4"].ToString();
                    p_objResult.m_strDRAIN4XML = objRow["DRAIN4XML"].ToString();
                    p_objResult.m_strDRAIN5 = objRow["DRAIN5"].ToString();
                    p_objResult.m_strDRAIN5XML = objRow["DRAIN5XML"].ToString();
                    p_objResult.m_strOUTPERHOUR = objRow["OUTPERHOUR"].ToString();
                    p_objResult.m_strOUTPERHOURXML = objRow["OUTPERHOURXML"].ToString();
                    p_objResult.m_strOUTTOTAL = objRow["OUTTOTAL"].ToString();
                    p_objResult.m_strOUTTOTALXML = objRow["OUTTOTALXML"].ToString();
                    p_objResult.m_strPOSTURE = objRow["POSTURE"].ToString();
                    p_objResult.m_strPOSTUREXML = objRow["POSTUREXML"].ToString();
                    p_objResult.m_strSKIN = objRow["SKIN"].ToString();
                    p_objResult.m_strSKINXML = objRow["SKINXML"].ToString();
                    p_objResult.m_strSPUTUM = objRow["SPUTUM"].ToString();
                    p_objResult.m_strSPUTUMXML = objRow["SPUTUMXML"].ToString();
                    p_objResult.m_strSUCKER = objRow["SUCKER"].ToString();
                    p_objResult.m_strSUCKERXML = objRow["SUCKERXML"].ToString();
                    p_objResult.m_strGESTICULATION = objRow["GESTICULATION"].ToString();
                    p_objResult.m_strGESTICULATIONXML = objRow["GESTICULATIONXML"].ToString();
                    p_objResult.m_strORAL = objRow["ORAL"].ToString();
                    p_objResult.m_strORALXML = objRow["ORALXML"].ToString();
                    p_objResult.m_strPERINEUM = objRow["PERINEUM"].ToString();
                    p_objResult.m_strPERINEUMXML = objRow["PERINEUMXML"].ToString();
                    p_objResult.m_strSPONGEBATH = objRow["SPONGEBATH"].ToString();
                    p_objResult.m_strSPONGEBATHXML = objRow["SPONGEBATHXML"].ToString();
                    p_objResult.m_strPT = objRow["PT"].ToString();
                    p_objResult.m_strPTXML = objRow["PTXML"].ToString();
                    p_objResult.m_strACT = objRow["ACT"].ToString();
                    p_objResult.m_strACTXML = objRow["ACTXML"].ToString();
                    p_objResult.m_strGLU = objRow["GLU"].ToString();
                    p_objResult.m_strGLUXML = objRow["GLUXML"].ToString();
                    p_objResult.m_strK = objRow["K"].ToString();
                    p_objResult.m_strKXML = objRow["KXML"].ToString();
                    p_objResult.m_strNAPLUS = objRow["NAPLUS"].ToString();
                    p_objResult.m_strNAPLUSXML = objRow["NAPLUSXML"].ToString();
                    p_objResult.m_strCL = objRow["CL"].ToString();
                    p_objResult.m_strCLXML = objRow["CLXML"].ToString();
                    p_objResult.m_strCAPLUS2 = objRow["CAPLUS2"].ToString();
                    p_objResult.m_strCAPLUS2XML = objRow["CAPLUS2XML"].ToString();
                    p_objResult.m_strMMODEL = objRow["MMODEL"].ToString();
                    p_objResult.m_strMMODELXML = objRow["MMODELXML"].ToString();
                    p_objResult.m_strPEE = objRow["PEE"].ToString();
                    p_objResult.m_strPEEXML = objRow["PEEXML"].ToString();
                    p_objResult.m_strTVTI = objRow["TVTI"].ToString();
                    p_objResult.m_strTVTIXML = objRow["TVTIXML"].ToString();
                    p_objResult.m_strFREQUENCY = objRow["FREQUENCY"].ToString();
                    p_objResult.m_strFREQUENCYXML = objRow["FREQUENCYXML"].ToString();
                    p_objResult.m_strO2 = objRow["O2"].ToString();
                    p_objResult.m_strO2XML = objRow["O2XML"].ToString();
                    p_objResult.m_strSENSEIIVE = objRow["SENSEIIVE"].ToString();
                    p_objResult.m_strSENSEIIVEXML = objRow["SENSEIIVEXML"].ToString();
                    p_objResult.m_strSPEED = objRow["SPEED"].ToString();
                    p_objResult.m_strSPEEDXML = objRow["SPEEDXML"].ToString();
                    p_objResult.m_strIE = objRow["IE"].ToString();
                    p_objResult.m_strIEXML = objRow["IEXML"].ToString();
                    p_objResult.m_strGASPRESS = objRow["GASPRESS"].ToString();
                    p_objResult.m_strGASPRESSXML = objRow["GASPRESSXML"].ToString();
                    p_objResult.m_strTV = objRow["TV"].ToString();
                    p_objResult.m_strTVXML = objRow["TVXML"].ToString();
                    p_objResult.m_strMV = objRow["MV"].ToString();
                    p_objResult.m_strMVXML = objRow["MVXML"].ToString();
                    p_objResult.m_strPIPEDEPTH = objRow["PIPEDEPTH"].ToString();
                    p_objResult.m_strPIPEDEPTHXML = objRow["PIPEDEPTHXML"].ToString();
                    p_objResult.m_strAIRCELLPRESS = objRow["AIRCELLPRESS"].ToString();
                    p_objResult.m_strAIRCELLPRESSXML = objRow["AIRCELLPRESSXML"].ToString();
                    p_objResult.m_strETCO2 = objRow["ETCO2"].ToString();
                    p_objResult.m_strETCO2XML = objRow["ETCO2XML"].ToString();
                    p_objResult.m_strPH = objRow["PH"].ToString();
                    p_objResult.m_strPHXML = objRow["PHXML"].ToString();
                    p_objResult.m_strPCO2 = objRow["PCO2"].ToString();
                    p_objResult.m_strPCO2XML = objRow["PCO2XML"].ToString();
                    p_objResult.m_strPAO2 = objRow["PAO2"].ToString();
                    p_objResult.m_strPAO2XML = objRow["PAO2XML"].ToString();
                    p_objResult.m_strHCO3 = objRow["HCO3"].ToString();
                    p_objResult.m_strHCO3XML = objRow["HCO3XML"].ToString();
                    p_objResult.m_strBE = objRow["BE"].ToString();
                    p_objResult.m_strBEXML = objRow["BEXML"].ToString();
                    p_objResult.m_strCUSTOMER1 = objRow["CUSTOMER1"].ToString();
                    p_objResult.m_strCUSTOMER1XML = objRow["CUSTOMER1XML"].ToString();
                    p_objResult.m_strCUSTOMER2 = objRow["CUSTOMER2"].ToString();
                    p_objResult.m_strCUSTOMER2XML = objRow["CUSTOMER2XML"].ToString();
                    p_objResult.m_strRESPIRATION = objRow["RESPIRATION"].ToString();
                    p_objResult.m_strRESPIRATIONXML = objRow["RESPIRATIONXML"].ToString();
                    p_objResult.m_intMarkStatus = Convert.ToInt32(objRow["MARKSTATUS"]);
                    p_objResult.m_strTRANSFERID = objRow["TRANSFERID_CHR"].ToString();
                    //护理内容
                    p_objResult.objRecordContent.m_strCONTENT = objRow["content"].ToString();
                    p_objResult.objRecordContent.m_strCONTENTXML = objRow["contentxml"].ToString();

                    #endregion


                    //获取签名集合
                    if (objRow["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(objRow["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out p_objResult.objSignerArr);

                        //释放
                        objSign = null;
                    }
                    p_objRecordContent = p_objResult;
                    #endregion
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
            //未实现
            lngRes = 1;
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
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsICUNurseRecord p_objRecord = (clsICUNurseRecord)p_objRecordContent;

            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            #region SQL
            string strSQL = @"insert into t_emr_icunurse
                  (registerid_vchr,
                   createdate_dat,
                   createuserid_vchr,
                   sequence_int,
                   status_int,
                   recorduserid_vchr,
                   recorddate_dat,
                   ifconfirm_int,
                   temperature,
                   temperaturexml,
                   endtemperature,
                   endtemperaturexml,
                   afteropdays,
                   hr,
                   hrxml,
                   rhythm,
                   rhythmxml,
                   bpd,
                   bpdxml,
                   abp,
                   abpxml,
                   cvp,
                   cvpxml,
                   spo2,
                   spo2xml,
                   consciousness,
                   consciousnessxml,
                   pupil,
                   pupilxml,
                   lightreflexl,
                   lightreflexlxml,
                   vasoaactive,
                   vasoaactivexml,
                   cardiacdiuretic,
                   cardiacdiureticxml,
                   liquid1,
                   liquid1xml,
                   liquid2,
                   liquid2xml,
                   liquid3,
                   liquid3xml,
                   liquid4,
                   liquid4xml,
                   liquid5,
                   liquid5xml,
                   fbool,
                   fboolxml,
                   plasma,
                   plasmaxml,
                   nose1,
                   nose1xml,
                   nose2,
                   nose2xml,
                   inperhour,
                   inperhourxml,
                   intotal,
                   intotalxml,
                   stool,
                   stoolxml,
                   stooltotal,
                   stooltotalxml,
                   piss,
                   pissxml,
                   pisstotal,
                   pisstotalxml,
                   drain1,
                   drain1xml,
                   drain2,
                   drain2xml,
                   drain3,
                   drain3xml,
                   drain4,
                   drain4xml,
                   drain5,
                   drain5xml,
                   outperhour,
                   outperhourxml,
                   outtotal,
                   outtotalxml,
                   bps,
                   bpsxml,lightreflexr,lightreflexrxml,markstatus,respiration,respirationxml,pupir,transferid_chr)
                values
                  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                   ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                   ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                   ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            string strSQL1 = @"insert into t_emr_icunursesub
                              (registerid_vchr,
                               createdate_dat,
                               posture,
                               posturexml,
                               skin,
                               skinxml,
                               sputum,
                               sputumxml,
                               sucker,
                               suckerxml,
                               gesticulation,
                               gesticulationxml,
                               oral,
                               oralxml,
                               perineum,
                               perineumxml,
                               spongebath,
                               spongebathxml,
                               pt,
                               ptxml,
                               act,
                               actxml,
                               glu,
                               gluxml,
                               k,
                               kxml,
                               naplus,
                               naplusxml,
                               cl,
                               clxml,
                               caplus2,
                               caplus2xml,
                               mmodel,
                               mmodelxml,
                               pee,
                               peexml,
                               tvti,
                               tvtixml,
                               frequency,
                               frequencyxml,
                               o2,
                               o2xml,
                               senseiive,
                               senseiivexml,
                               speed,
                               speedxml,
                               ie,
                               iexml,
                               gaspress,
                               gaspressxml,
                               tv,
                               tvxml,
                               mv,
                               mvxml,
                               pipedepth,
                               pipedepthxml,
                               aircellpress,
                               aircellpressxml,
                               etco2,
                               etco2xml,
                               ph,
                               phxml,
                               pco2,
                               pco2xml,
                               pao2,
                               pao2xml,
                               hco3,
                               hco3xml,
                               be,
                               bexml,
                               customer1,
                               customer1xml,
                               customer2,
                               customer2xml)
                            values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                                   ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                                   ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            string strSQL2 = @"insert into t_emr_icunursecontent
                  (registerid_vchr,
                   createdate_dat,
                   modifydate,
                   status_int,
                   afteropdays,
                   temperature_right,
                   endtemperature_right,
                   hr_right,
                   rhythm_right,
                   bpd_right,
                   abp_right,
                   cvp_right,
                   spo2_right,
                   consciousness_right,
                   pupil_right,
                   lightreflexl_right,
                   vasoaactive_right,
                   cardiacdiuretic_right,
                   liquid1_right,
                   liquid2_right,
                   liquid3_right,
                   liquid4_right,
                   liquid5_right,
                   fbool_right,
                   plasma_right,
                   nose1_right,
                   nose2_right,
                   inperhour_right,
                   intotal_right,
                   stool_right,
                   stooltotal_right,
                   piss_right,
                   pisstotal_right,
                   drain1_right,
                   drain2_right,
                   drain3_right,
                   drain4_right,
                   drain5_right,
                   outperhour_right,
                   outtotal_right,
                   posture_right,
                   skin_right,
                   sputum_right,
                   sucker_right,
                   gesticulation_right,
                   oral_right,
                   perineum_right,
                   spongebath_right,
                   pt_right,
                   act_right,
                   glu_right,
                   k_right,
                   naplus_right,
                   cl_right,
                   caplus2_right,
                   mmodel_right,
                   pee_right,
                   tvti_right,
                   frequency_right,
                   o2_right,
                   senseiive_right,
                   speed_right,
                   ie_right,
                   gaspress_right,
                   tv_right,
                   mv_right,
                   pipedepth_right,
                   aircellpress_right,
                   etco2_right,
                   ph_right,
                   pco2_right,
                   pao2_right,
                   hco3_right,
                   be_right,
                   customer1_right,
                   customer2_right, bps_right, lightreflexr_right,respiration_right,pupir_right)
                values
                ( ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                  ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                  ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";



            #endregion

            try
            {
                DateTime dtNow = DateTime.Now;
                p_objRecord.m_dtmCreateDate = dtNow;
                p_objRecord.objRecordContent.m_dtmCreateDate = dtNow;
                //对预设值做处理
                m_lngPreDefine2DB(p_objRecord);
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);


                #region 保存主表1
                //获取IDataParameter数组
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(88, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strCreateUserID;
                objLisAddItemRefArr[3].Value = lngSequence;
                objLisAddItemRefArr[4].Value = 1;// p_objRecord.m_bytStatus;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strRecordUserID;
                objLisAddItemRefArr[6].DbType = DbType.DateTime;
                objLisAddItemRefArr[6].Value = p_objRecord.m_dtmRecordDate;
                objLisAddItemRefArr[7].Value = p_objRecord.m_bytIfConfirm;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strTEMPERATURE;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strTEMPERATUREXML;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strENDTEMPERATURE;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strENDTEMPERATUREXML;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strAFTEROPDAYS;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strHR;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strHRXML;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strRHYTHM;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strRHYTHMXML;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strBPD;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strBPDXML;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strABP;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strABPXML;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strCVP;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strCVPXML;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strSPO2;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strSPO2XML;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strCONSCIOUSNESS;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCONSCIOUSNESSXML;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strPUPIL;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strPUPILXML;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strLIGHTREFLEXL;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strLIGHTREFLEXLXML;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strVASOAACTIVE;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strVASOAACTIVEXML;
                objLisAddItemRefArr[33].Value = p_objRecord.m_strCARDIACDIURETIC;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strCARDIACDIURETICXML;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strLIQUID1;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strLIQUID1XML;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strLIQUID2;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strLIQUID2XML;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strLIQUID3;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strLIQUID3XML;
                objLisAddItemRefArr[41].Value = p_objRecord.m_strLIQUID4;
                objLisAddItemRefArr[42].Value = p_objRecord.m_strLIQUID4XML;
                objLisAddItemRefArr[43].Value = p_objRecord.m_strLIQUID5;
                objLisAddItemRefArr[44].Value = p_objRecord.m_strLIQUID5XML;
                objLisAddItemRefArr[45].Value = p_objRecord.m_strFBOOL;
                objLisAddItemRefArr[46].Value = p_objRecord.m_strFBOOLXML;
                objLisAddItemRefArr[47].Value = p_objRecord.m_strPLASMA;
                objLisAddItemRefArr[48].Value = p_objRecord.m_strPLASMAXML;
                objLisAddItemRefArr[49].Value = p_objRecord.m_strNOSE1;
                objLisAddItemRefArr[50].Value = p_objRecord.m_strNOSE1XML;
                objLisAddItemRefArr[51].Value = p_objRecord.m_strNOSE2;
                objLisAddItemRefArr[52].Value = p_objRecord.m_strNOSE2XML;
                objLisAddItemRefArr[53].Value = p_objRecord.m_strINPERHOUR;
                objLisAddItemRefArr[54].Value = p_objRecord.m_strINPERHOURXML;
                objLisAddItemRefArr[55].Value = p_objRecord.m_strINTOTAL;
                objLisAddItemRefArr[56].Value = p_objRecord.m_strINTOTALXML;
                objLisAddItemRefArr[57].Value = p_objRecord.m_strSTOOL;
                objLisAddItemRefArr[58].Value = p_objRecord.m_strSTOOLXML;
                objLisAddItemRefArr[59].Value = p_objRecord.m_strSTOOLTOTAL;
                objLisAddItemRefArr[60].Value = p_objRecord.m_strSTOOLTOTALXML;
                objLisAddItemRefArr[61].Value = p_objRecord.m_strPISS;
                objLisAddItemRefArr[62].Value = p_objRecord.m_strPISSXML;
                objLisAddItemRefArr[63].Value = p_objRecord.m_strPISSTOTAL;
                objLisAddItemRefArr[64].Value = p_objRecord.m_strPISSTOTALXML;
                objLisAddItemRefArr[65].Value = p_objRecord.m_strDRAIN1;
                objLisAddItemRefArr[66].Value = p_objRecord.m_strDRAIN1XML;
                objLisAddItemRefArr[67].Value = p_objRecord.m_strDRAIN2;
                objLisAddItemRefArr[68].Value = p_objRecord.m_strDRAIN2XML;
                objLisAddItemRefArr[69].Value = p_objRecord.m_strDRAIN3;
                objLisAddItemRefArr[70].Value = p_objRecord.m_strDRAIN3XML;
                objLisAddItemRefArr[71].Value = p_objRecord.m_strDRAIN4;
                objLisAddItemRefArr[72].Value = p_objRecord.m_strDRAIN4XML;
                objLisAddItemRefArr[73].Value = p_objRecord.m_strDRAIN5;
                objLisAddItemRefArr[74].Value = p_objRecord.m_strDRAIN5XML;
                objLisAddItemRefArr[75].Value = p_objRecord.m_strOUTPERHOUR;
                objLisAddItemRefArr[76].Value = p_objRecord.m_strOUTPERHOURXML;
                objLisAddItemRefArr[77].Value = p_objRecord.m_strOUTTOTAL;
                objLisAddItemRefArr[78].Value = p_objRecord.m_strOUTTOTALXML;
                objLisAddItemRefArr[79].Value = p_objRecord.m_strBPS;
                objLisAddItemRefArr[80].Value = p_objRecord.m_strBPSXML;
                objLisAddItemRefArr[81].Value = p_objRecord.m_strLIGHTREFLEXR;
                objLisAddItemRefArr[82].Value = p_objRecord.m_strLIGHTREFLEXRXML;
                objLisAddItemRefArr[83].Value = p_objRecord.m_intMarkStatus;
                objLisAddItemRefArr[84].Value = p_objRecord.m_strRESPIRATION;
                objLisAddItemRefArr[85].Value = p_objRecord.m_strRESPIRATIONXML;
                objLisAddItemRefArr[86].Value = p_objRecord.m_strPUPIR;
                if (string.IsNullOrEmpty(p_objRecord.m_strTRANSFERID))
                {
                    objLisAddItemRefArr[87].Value = "000000000000";
                }
                else
                {
                    objLisAddItemRefArr[87].Value = p_objRecord.m_strTRANSFERID;
                }

                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objLisAddItemRefArr);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region 保存主表2
                IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(74, out objLisAddItemRefArr1);
                //Please change the datetime and reocrdid 

                objLisAddItemRefArr1[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr1[1].DbType = DbType.DateTime;
                objLisAddItemRefArr1[1].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr1[2].Value = p_objRecord.m_strPOSTURE;
                objLisAddItemRefArr1[3].Value = p_objRecord.m_strPOSTUREXML;
                objLisAddItemRefArr1[4].Value = p_objRecord.m_strSKIN;
                objLisAddItemRefArr1[5].Value = p_objRecord.m_strSKINXML;
                objLisAddItemRefArr1[6].Value = p_objRecord.m_strSPUTUM;
                objLisAddItemRefArr1[7].Value = p_objRecord.m_strSPUTUMXML;
                objLisAddItemRefArr1[8].Value = p_objRecord.m_strSUCKER;
                objLisAddItemRefArr1[9].Value = p_objRecord.m_strSUCKERXML;
                objLisAddItemRefArr1[10].Value = p_objRecord.m_strGESTICULATION;
                objLisAddItemRefArr1[11].Value = p_objRecord.m_strGESTICULATIONXML;
                objLisAddItemRefArr1[12].Value = p_objRecord.m_strORAL;
                objLisAddItemRefArr1[13].Value = p_objRecord.m_strORALXML;
                objLisAddItemRefArr1[14].Value = p_objRecord.m_strPERINEUM;
                objLisAddItemRefArr1[15].Value = p_objRecord.m_strPERINEUMXML;
                objLisAddItemRefArr1[16].Value = p_objRecord.m_strSPONGEBATH;
                objLisAddItemRefArr1[17].Value = p_objRecord.m_strSPONGEBATHXML;
                objLisAddItemRefArr1[18].Value = p_objRecord.m_strPT;
                objLisAddItemRefArr1[19].Value = p_objRecord.m_strPTXML;
                objLisAddItemRefArr1[20].Value = p_objRecord.m_strACT;
                objLisAddItemRefArr1[21].Value = p_objRecord.m_strACTXML;
                objLisAddItemRefArr1[22].Value = p_objRecord.m_strGLU;
                objLisAddItemRefArr1[23].Value = p_objRecord.m_strGLUXML;
                objLisAddItemRefArr1[24].Value = p_objRecord.m_strK;
                objLisAddItemRefArr1[25].Value = p_objRecord.m_strKXML;
                objLisAddItemRefArr1[26].Value = p_objRecord.m_strNAPLUS;
                objLisAddItemRefArr1[27].Value = p_objRecord.m_strNAPLUSXML;
                objLisAddItemRefArr1[28].Value = p_objRecord.m_strCL;
                objLisAddItemRefArr1[29].Value = p_objRecord.m_strCLXML;
                objLisAddItemRefArr1[30].Value = p_objRecord.m_strCAPLUS2;
                objLisAddItemRefArr1[31].Value = p_objRecord.m_strCAPLUS2XML;
                objLisAddItemRefArr1[32].Value = p_objRecord.m_strMMODEL;
                objLisAddItemRefArr1[33].Value = p_objRecord.m_strMMODELXML;
                objLisAddItemRefArr1[34].Value = p_objRecord.m_strPEE;
                objLisAddItemRefArr1[35].Value = p_objRecord.m_strPEEXML;
                objLisAddItemRefArr1[36].Value = p_objRecord.m_strTVTI;
                objLisAddItemRefArr1[37].Value = p_objRecord.m_strTVTIXML;
                objLisAddItemRefArr1[38].Value = p_objRecord.m_strFREQUENCY;
                objLisAddItemRefArr1[39].Value = p_objRecord.m_strFREQUENCYXML;
                objLisAddItemRefArr1[40].Value = p_objRecord.m_strO2;
                objLisAddItemRefArr1[41].Value = p_objRecord.m_strO2XML;
                objLisAddItemRefArr1[42].Value = p_objRecord.m_strSENSEIIVE;
                objLisAddItemRefArr1[43].Value = p_objRecord.m_strSENSEIIVEXML;
                objLisAddItemRefArr1[44].Value = p_objRecord.m_strSPEED;
                objLisAddItemRefArr1[45].Value = p_objRecord.m_strSPEEDXML;
                objLisAddItemRefArr1[46].Value = p_objRecord.m_strIE;
                objLisAddItemRefArr1[47].Value = p_objRecord.m_strIEXML;
                objLisAddItemRefArr1[48].Value = p_objRecord.m_strGASPRESS;
                objLisAddItemRefArr1[49].Value = p_objRecord.m_strGASPRESSXML;
                objLisAddItemRefArr1[50].Value = p_objRecord.m_strTV;
                objLisAddItemRefArr1[51].Value = p_objRecord.m_strTVXML;
                objLisAddItemRefArr1[52].Value = p_objRecord.m_strMV;
                objLisAddItemRefArr1[53].Value = p_objRecord.m_strMVXML;
                objLisAddItemRefArr1[54].Value = p_objRecord.m_strPIPEDEPTH;
                objLisAddItemRefArr1[55].Value = p_objRecord.m_strPIPEDEPTHXML;
                objLisAddItemRefArr1[56].Value = p_objRecord.m_strAIRCELLPRESS;
                objLisAddItemRefArr1[57].Value = p_objRecord.m_strAIRCELLPRESSXML;
                objLisAddItemRefArr1[58].Value = p_objRecord.m_strETCO2;
                objLisAddItemRefArr1[59].Value = p_objRecord.m_strETCO2XML;
                objLisAddItemRefArr1[60].Value = p_objRecord.m_strPH_RIGHT;
                objLisAddItemRefArr1[61].Value = p_objRecord.m_strPHXML;
                objLisAddItemRefArr1[62].Value = p_objRecord.m_strPCO2;
                objLisAddItemRefArr1[63].Value = p_objRecord.m_strPCO2XML;
                objLisAddItemRefArr1[64].Value = p_objRecord.m_strPAO2;
                objLisAddItemRefArr1[65].Value = p_objRecord.m_strPAO2XML;
                objLisAddItemRefArr1[66].Value = p_objRecord.m_strHCO3;
                objLisAddItemRefArr1[67].Value = p_objRecord.m_strHCO3XML;
                objLisAddItemRefArr1[68].Value = p_objRecord.m_strBE;
                objLisAddItemRefArr1[69].Value = p_objRecord.m_strBEXML;
                objLisAddItemRefArr1[70].Value = p_objRecord.m_strCUSTOMER1;
                objLisAddItemRefArr1[71].Value = p_objRecord.m_strCUSTOMER1XML;
                objLisAddItemRefArr1[72].Value = p_objRecord.m_strCUSTOMER2;
                objLisAddItemRefArr1[73].Value = p_objRecord.m_strCUSTOMER2XML;


                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngEff, objLisAddItemRefArr1);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region 保存签名集合
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecord.objSignerArr, lngSequence);

                #endregion

                #region  保存子表

                IDataParameter[] objLisAddItemRefArr2 = null;
                objHRPServ.CreateDatabaseParameter(80, out objLisAddItemRefArr2);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr2[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr2[1].DbType = DbType.DateTime;
                objLisAddItemRefArr2[1].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr2[2].DbType = DbType.DateTime;
                objLisAddItemRefArr2[2].Value = p_objRecord.m_dtmModifyDate;
                objLisAddItemRefArr2[3].Value = 1;// p_objRecord.m_bytStatus;
                objLisAddItemRefArr2[4].Value = p_objRecord.m_strAFTEROPDAYS;
                objLisAddItemRefArr2[5].Value = p_objRecord.m_strTEMPERATURE;
                objLisAddItemRefArr2[6].Value = p_objRecord.m_strENDTEMPERATURE;
                objLisAddItemRefArr2[7].Value = p_objRecord.m_strHR_RIGHT;
                objLisAddItemRefArr2[8].Value = p_objRecord.m_strRHYTHM_RIGHT;
                objLisAddItemRefArr2[9].Value = p_objRecord.m_strBPD_RIGHT;
                objLisAddItemRefArr2[10].Value = p_objRecord.m_strABP_RIGHT;
                objLisAddItemRefArr2[11].Value = p_objRecord.m_strCVP_RIGHT;
                objLisAddItemRefArr2[12].Value = p_objRecord.m_strSPO2_RIGHT;
                objLisAddItemRefArr2[13].Value = p_objRecord.m_strCONSCIOUSNESS_RIGHT;
                objLisAddItemRefArr2[14].Value = p_objRecord.m_strPUPIL_RIGHT;
                objLisAddItemRefArr2[15].Value = p_objRecord.m_strLIGHTREFLEXL_RIGHT;
                objLisAddItemRefArr2[16].Value = p_objRecord.m_strVASOAACTIVE_RIGHT;
                objLisAddItemRefArr2[17].Value = p_objRecord.m_strCARDIACDIURETIC_RIGHT;
                objLisAddItemRefArr2[18].Value = p_objRecord.m_strLIQUID1_RIGHT;
                objLisAddItemRefArr2[19].Value = p_objRecord.m_strLIQUID2_RIGHT;
                objLisAddItemRefArr2[20].Value = p_objRecord.m_strLIQUID3_RIGHT;
                objLisAddItemRefArr2[21].Value = p_objRecord.m_strLIQUID4_RIGHT;
                objLisAddItemRefArr2[22].Value = p_objRecord.m_strLIQUID5_RIGHT;
                objLisAddItemRefArr2[23].Value = p_objRecord.m_strFBOOL_RIGHT;
                objLisAddItemRefArr2[24].Value = p_objRecord.m_strPLASMA_RIGHT;
                objLisAddItemRefArr2[25].Value = p_objRecord.m_strNOSE1_RIGHT;
                objLisAddItemRefArr2[26].Value = p_objRecord.m_strNOSE2_RIGHT;
                objLisAddItemRefArr2[27].Value = p_objRecord.m_strINPERHOUR_RIGHT;
                objLisAddItemRefArr2[28].Value = p_objRecord.m_strINTOTAL_RIGHT;
                objLisAddItemRefArr2[29].Value = p_objRecord.m_strSTOOL_RIGHT;
                objLisAddItemRefArr2[30].Value = p_objRecord.m_strSTOOLTOTAL_RIGHT;
                objLisAddItemRefArr2[31].Value = p_objRecord.m_strPISS_RIGHT;
                objLisAddItemRefArr2[32].Value = p_objRecord.m_strPISSTOTAL_RIGHT;
                objLisAddItemRefArr2[33].Value = p_objRecord.m_strDRAIN1_RIGHT;
                objLisAddItemRefArr2[34].Value = p_objRecord.m_strDRAIN2_RIGHT;
                objLisAddItemRefArr2[35].Value = p_objRecord.m_strDRAIN3_RIGHT;
                objLisAddItemRefArr2[36].Value = p_objRecord.m_strDRAIN4_RIGHT;
                objLisAddItemRefArr2[37].Value = p_objRecord.m_strDRAIN5_RIGHT;
                objLisAddItemRefArr2[38].Value = p_objRecord.m_strOUTPERHOUR_RIGHT;
                objLisAddItemRefArr2[39].Value = p_objRecord.m_strOUTTOTAL_RIGHT;
                objLisAddItemRefArr2[40].Value = p_objRecord.m_strPOSTURE_RIGHT;
                objLisAddItemRefArr2[41].Value = p_objRecord.m_strSKIN_RIGHT;
                objLisAddItemRefArr2[42].Value = p_objRecord.m_strSPUTUM_RIGHT;
                objLisAddItemRefArr2[43].Value = p_objRecord.m_strSUCKER_RIGHT;
                objLisAddItemRefArr2[44].Value = p_objRecord.m_strGESTICULATION_RIGHT;
                objLisAddItemRefArr2[45].Value = p_objRecord.m_strORAL_RIGHT;
                objLisAddItemRefArr2[46].Value = p_objRecord.m_strPERINEUM_RIGHT;
                objLisAddItemRefArr2[47].Value = p_objRecord.m_strSPONGEBATH_RIGHT;
                objLisAddItemRefArr2[48].Value = p_objRecord.m_strPT_RIGHT;
                objLisAddItemRefArr2[49].Value = p_objRecord.m_strACT_RIGHT;
                objLisAddItemRefArr2[50].Value = p_objRecord.m_strGLU_RIGHT;
                objLisAddItemRefArr2[51].Value = p_objRecord.m_strK_RIGHT;
                objLisAddItemRefArr2[52].Value = p_objRecord.m_strNAPLUS_RIGHT;
                objLisAddItemRefArr2[53].Value = p_objRecord.m_strCL_RIGHT;
                objLisAddItemRefArr2[54].Value = p_objRecord.m_strCAPLUS2_RIGHT;
                objLisAddItemRefArr2[55].Value = p_objRecord.m_strMMODEL_RIGHT;
                objLisAddItemRefArr2[56].Value = p_objRecord.m_strPEE_RIGHT;
                objLisAddItemRefArr2[57].Value = p_objRecord.m_strTVTI_RIGHT;
                objLisAddItemRefArr2[58].Value = p_objRecord.m_strFREQUENCY_RIGHT;
                objLisAddItemRefArr2[59].Value = p_objRecord.m_strO2_RIGHT;
                objLisAddItemRefArr2[60].Value = p_objRecord.m_strSENSEIIVE_RIGHT;
                objLisAddItemRefArr2[61].Value = p_objRecord.m_strSPEED_RIGHT;
                objLisAddItemRefArr2[62].Value = p_objRecord.m_strIE_RIGHT;
                objLisAddItemRefArr2[63].Value = p_objRecord.m_strGASPRESS_RIGHT;
                objLisAddItemRefArr2[64].Value = p_objRecord.m_strTV_RIGHT;
                objLisAddItemRefArr2[65].Value = p_objRecord.m_strMV_RIGHT;
                objLisAddItemRefArr2[66].Value = p_objRecord.m_strPIPEDEPTH_RIGHT;
                objLisAddItemRefArr2[67].Value = p_objRecord.m_strAIRCELLPRESS_RIGHT;
                objLisAddItemRefArr2[68].Value = p_objRecord.m_strETCO2_RIGHT;
                objLisAddItemRefArr2[69].Value = p_objRecord.m_strPH_RIGHT;
                objLisAddItemRefArr2[70].Value = p_objRecord.m_strPCO2_RIGHT;
                objLisAddItemRefArr2[71].Value = p_objRecord.m_strPAO2_RIGHT;
                objLisAddItemRefArr2[72].Value = p_objRecord.m_strHCO3_RIGHT;
                objLisAddItemRefArr2[73].Value = p_objRecord.m_strBE_RIGHT;
                objLisAddItemRefArr2[74].Value = p_objRecord.m_strCUSTOMER1_RIGHT;
                objLisAddItemRefArr2[75].Value = p_objRecord.m_strCUSTOMER2_RIGHT;
                objLisAddItemRefArr2[76].Value = p_objRecord.m_strBPS_RIGHT;
                objLisAddItemRefArr2[77].Value = p_objRecord.m_strLIGHTREFLEXR_RIGHT;
                objLisAddItemRefArr2[78].Value = p_objRecord.m_strRESPIRATION_RIGHT;
                objLisAddItemRefArr2[79].Value = p_objRecord.m_strPUPIR_RIGHT;



                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngEff, objLisAddItemRefArr2);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region 保存护理内容
                //若有输入则保存
                if (p_objRecord.objRecordContent.m_strCONTENT.Length != 0)
                {
                    m_lngAddContent2DB(p_objRecord.objRecordContent, lngSequence, p_objRecord.m_dtmModifyDate);
                }
                #endregion

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
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
            long lngRes = 0;
           //未实现
            lngRes = 1;
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
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strRegisterID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsICUNurseRecord p_objRecord = (clsICUNurseRecord)p_objRecordContent;

            #region SQL
            //更新主表1
            string strSQL1 = @"update t_emr_icunurse
                                set    
                                       sequence_int             = ?,
                                       temperature              = ?,
                                       temperaturexml           = ?,
                                       endtemperature           = ?,
                                       endtemperaturexml        = ?,
                                       afteropdays              = ?,
                                       hr                       = ?,
                                       hrxml                    = ?,
                                       rhythm                   = ?,
                                       rhythmxml                = ?,
                                       bpd                      = ?,
                                       bpdxml                   = ?,
                                       abp                      = ?,
                                       abpxml                   = ?,
                                       cvp                      = ?,
                                       cvpxml                   = ?,
                                       spo2                     = ?,
                                       spo2xml                  = ?,
                                       consciousness            = ?,
                                       consciousnessxml         = ?,
                                       pupil                    = ?,
                                       pupilxml                 = ?,
                                       lightreflexl             = ?,
                                       lightreflexlxml          = ?,
                                       vasoaactive              = ?,
                                       vasoaactivexml           = ?,
                                       cardiacdiuretic          = ?,
                                       cardiacdiureticxml       = ?,
                                       liquid1                  = ?,
                                       liquid1xml               = ?,
                                       liquid2                  = ?,
                                       liquid2xml               = ?,
                                       liquid3                  = ?,
                                       liquid3xml               = ?,
                                       liquid4                  = ?,
                                       liquid4xml               = ?,
                                       liquid5                  = ?,
                                       liquid5xml               = ?,
                                       fbool                    = ?,
                                       fboolxml                 = ?,
                                       plasma                   = ?,
                                       plasmaxml                = ?,
                                       nose1                    = ?,
                                       nose1xml                 = ?,
                                       nose2                    = ?,
                                       nose2xml                 = ?,
                                       inperhour                = ?,
                                       inperhourxml             = ?,
                                       intotal                  = ?,
                                       intotalxml               = ?,
                                       stool                    = ?,
                                       stoolxml                 = ?,
                                       stooltotal               = ?,
                                       stooltotalxml            = ?,
                                       piss                     = ?,
                                       pissxml                  = ?,
                                       pisstotal                = ?,
                                       pisstotalxml             = ?,
                                       drain1                   = ?,
                                       drain1xml                = ?,
                                       drain2                   = ?,
                                       drain2xml                = ?,
                                       drain3                   = ?,
                                       drain3xml                = ?,
                                       drain4                   = ?,
                                       drain4xml                = ?,
                                       drain5                   = ?,
                                       drain5xml                = ?,
                                       outperhour               = ?,
                                       outperhourxml            = ?,
                                       outtotal                 = ?,
                                       outtotalxml              = ?,
                                       bps                      = ?,
                                       bpsxml                   = ?,
                                       lightreflexr             = ?,
                                       lightreflexrxml          = ?,
                                       respiration              = ?,
                                       respirationxml           = ?,
                                       pupir                    = ?,
                                       markstatus               = ?
                                where  registerid_vchr          = ?
                                and    createdate_dat           = ?";
            //更新主表2
            string strSQL2 = @"update t_emr_icunursesub
                        set    
                               posture          = ?,
                               posturexml       = ?,
                               skin             = ?,
                               skinxml          = ?,
                               sputum           = ?,
                               sputumxml        = ?,
                               sucker           = ?,
                               suckerxml        = ?,
                               gesticulation    = ?,
                               gesticulationxml = ?,
                               oral             = ?,
                               oralxml          = ?,
                               perineum         = ?,
                               perineumxml      = ?,
                               spongebath       = ?,
                               spongebathxml    = ?,
                               pt               = ?,
                               ptxml            = ?,
                               act              = ?,
                               actxml           = ?,
                               glu              = ?,
                               gluxml           = ?,
                               k                = ?,
                               kxml             = ?,
                               naplus           = ?,
                               naplusxml        = ?,
                               cl               = ?,
                               clxml            = ?,
                               caplus2          = ?,
                               caplus2xml       = ?,
                               mmodel           = ?,
                               mmodelxml        = ?,
                               pee              = ?,
                               peexml           = ?,
                               tvti             = ?,
                               tvtixml          = ?,
                               frequency        = ?,
                               frequencyxml     = ?,
                               o2               = ?,
                               o2xml            = ?,
                               senseiive        = ?,
                               senseiivexml     = ?,
                               speed            = ?,
                               speedxml         = ?,
                               ie               = ?,
                               iexml            = ?,
                               gaspress         = ?,
                               gaspressxml      = ?,
                               tv               = ?,
                               tvxml            = ?,
                               mv               = ?,
                               mvxml            = ?,
                               pipedepth        = ?,
                               pipedepthxml     = ?,
                               aircellpress     = ?,
                               aircellpressxml  = ?,
                               etco2            = ?,
                               etco2xml         = ?,
                               ph               = ?,
                               phxml            = ?,
                               pco2             = ?,
                               pco2xml          = ?,
                               pao2             = ?,
                               pao2xml          = ?,
                               hco3             = ?,
                               hco3xml          = ?,
                               be               = ?,
                               bexml            = ?,
                               customer1        = ?,
                               customer1xml     = ?,
                               customer2        = ?,
                               customer2xml     = ?
                        where  registerid_vchr  = ?
                        and    createdate_dat   =? ";


            //更改子表记录
            string strSQL3 = @"update  t_emr_icunursecontent t
                               set t.status_int =0
                             where t.registerid_vchr = ?
                               and t.createdate_dat = ?  ";
                             
            //新增子表记录
            string strSQL4 = @"insert into t_emr_icunursecontent
                 (registerid_vchr,
                   createdate_dat,
                   modifydate,
                   status_int,
                   afteropdays,
                   temperature_right,
                   endtemperature_right,
                   hr_right,
                   rhythm_right,
                   bpd_right,
                   abp_right,
                   cvp_right,
                   spo2_right,
                   consciousness_right,
                   pupil_right,
                   lightreflexl_right,
                   vasoaactive_right,
                   cardiacdiuretic_right,
                   liquid1_right,
                   liquid2_right,
                   liquid3_right,
                   liquid4_right,
                   liquid5_right,
                   fbool_right,
                   plasma_right,
                   nose1_right,
                   nose2_right,
                   inperhour_right,
                   intotal_right,
                   stool_right,
                   stooltotal_right,
                   piss_right,
                   pisstotal_right,
                   drain1_right,
                   drain2_right,
                   drain3_right,
                   drain4_right,
                   drain5_right,
                   outperhour_right,
                   outtotal_right,
                   posture_right,
                   skin_right,
                   sputum_right,
                   sucker_right,
                   gesticulation_right,
                   oral_right,
                   perineum_right,
                   spongebath_right,
                   pt_right,
                   act_right,
                   glu_right,
                   k_right,
                   naplus_right,
                   cl_right,
                   caplus2_right,
                   mmodel_right,
                   pee_right,
                   tvti_right,
                   frequency_right,
                   o2_right,
                   senseiive_right,
                   speed_right,
                   ie_right,
                   gaspress_right,
                   tv_right,
                   mv_right,
                   pipedepth_right,
                   aircellpress_right,
                   etco2_right,
                   ph_right,
                   pco2_right,
                   pao2_right,
                   hco3_right,
                   be_right,
                   customer1_right,
                   customer2_right, bps_right, lightreflexr_right,respiration_right,pupir_right)
                values
                ( ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                  ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
                  ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";



            #endregion

            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //对预设值做处理
                m_lngPreDefine2DB(p_objRecord);
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region 保存主表1
                //获取IDataParameter数组
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(82, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = lngSequence;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strTEMPERATURE;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strTEMPERATUREXML;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strENDTEMPERATURE;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strENDTEMPERATUREXML;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strAFTEROPDAYS;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strHRXML;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strRHYTHM;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strRHYTHMXML;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strBPD;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strBPDXML;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strABP;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strABPXML;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strCVP;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strCVPXML;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSPO2;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strSPO2XML;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strCONSCIOUSNESS;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strCONSCIOUSNESSXML;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strPUPIL;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strPUPILXML;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strLIGHTREFLEXL;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strLIGHTREFLEXLXML;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strVASOAACTIVE;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strVASOAACTIVEXML;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCARDIACDIURETIC;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strCARDIACDIURETICXML;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strLIQUID1;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strLIQUID1XML;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strLIQUID2;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strLIQUID2XML;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strLIQUID3;
                objLisAddItemRefArr[33].Value = p_objRecord.m_strLIQUID3XML;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strLIQUID4;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strLIQUID4XML;
                objLisAddItemRefArr[36].Value = p_objRecord.m_strLIQUID5;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strLIQUID5XML;
                objLisAddItemRefArr[38].Value = p_objRecord.m_strFBOOL;
                objLisAddItemRefArr[39].Value = p_objRecord.m_strFBOOLXML;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strPLASMA;
                objLisAddItemRefArr[41].Value = p_objRecord.m_strPLASMAXML;
                objLisAddItemRefArr[42].Value = p_objRecord.m_strNOSE1;
                objLisAddItemRefArr[43].Value = p_objRecord.m_strNOSE1XML;
                objLisAddItemRefArr[44].Value = p_objRecord.m_strNOSE2;
                objLisAddItemRefArr[45].Value = p_objRecord.m_strNOSE2XML;
                objLisAddItemRefArr[46].Value = p_objRecord.m_strINPERHOUR;
                objLisAddItemRefArr[47].Value = p_objRecord.m_strINPERHOURXML;
                objLisAddItemRefArr[48].Value = p_objRecord.m_strINTOTAL;
                objLisAddItemRefArr[49].Value = p_objRecord.m_strINTOTALXML;
                objLisAddItemRefArr[50].Value = p_objRecord.m_strSTOOL;
                objLisAddItemRefArr[51].Value = p_objRecord.m_strSTOOLXML;
                objLisAddItemRefArr[52].Value = p_objRecord.m_strSTOOLTOTAL;
                objLisAddItemRefArr[53].Value = p_objRecord.m_strSTOOLTOTALXML;
                objLisAddItemRefArr[54].Value = p_objRecord.m_strPISS;
                objLisAddItemRefArr[55].Value = p_objRecord.m_strPISSXML;
                objLisAddItemRefArr[56].Value = p_objRecord.m_strPISSTOTAL;
                objLisAddItemRefArr[57].Value = p_objRecord.m_strPISSTOTALXML;
                objLisAddItemRefArr[58].Value = p_objRecord.m_strDRAIN1;
                objLisAddItemRefArr[59].Value = p_objRecord.m_strDRAIN1XML;
                objLisAddItemRefArr[60].Value = p_objRecord.m_strDRAIN2;
                objLisAddItemRefArr[61].Value = p_objRecord.m_strDRAIN2XML;
                objLisAddItemRefArr[62].Value = p_objRecord.m_strDRAIN3;
                objLisAddItemRefArr[63].Value = p_objRecord.m_strDRAIN3XML;
                objLisAddItemRefArr[64].Value = p_objRecord.m_strDRAIN4;
                objLisAddItemRefArr[65].Value = p_objRecord.m_strDRAIN4XML;
                objLisAddItemRefArr[66].Value = p_objRecord.m_strDRAIN5;
                objLisAddItemRefArr[67].Value = p_objRecord.m_strDRAIN5XML;
                objLisAddItemRefArr[68].Value = p_objRecord.m_strOUTPERHOUR;
                objLisAddItemRefArr[69].Value = p_objRecord.m_strOUTPERHOURXML;
                objLisAddItemRefArr[70].Value = p_objRecord.m_strOUTTOTAL;
                objLisAddItemRefArr[71].Value = p_objRecord.m_strOUTTOTALXML;
                objLisAddItemRefArr[72].Value = p_objRecord.m_strBPS;
                objLisAddItemRefArr[73].Value = p_objRecord.m_strBPSXML;
                objLisAddItemRefArr[74].Value = p_objRecord.m_strLIGHTREFLEXL;
                objLisAddItemRefArr[75].Value = p_objRecord.m_strLIGHTREFLEXLXML;
                objLisAddItemRefArr[76].Value = p_objRecord.m_strRESPIRATION;
                objLisAddItemRefArr[77].Value = p_objRecord.m_strRESPIRATIONXML;
                objLisAddItemRefArr[78].Value = p_objRecord.m_strPUPIR;
                objLisAddItemRefArr[79].Value = p_objRecord.m_intMarkStatus;
                objLisAddItemRefArr[80].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr[81].DbType = DbType.DateTime;
                objLisAddItemRefArr[81].Value = p_objRecord.m_dtmCreateDate;


                //执行SQL
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngEff, objLisAddItemRefArr);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region 更新主表2
                IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(74, out objLisAddItemRefArr1);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr1[0].Value = p_objRecord.m_strPOSTURE;
                objLisAddItemRefArr1[1].Value = p_objRecord.m_strPOSTUREXML;
                objLisAddItemRefArr1[2].Value = p_objRecord.m_strSKIN;
                objLisAddItemRefArr1[3].Value = p_objRecord.m_strSKINXML;
                objLisAddItemRefArr1[4].Value = p_objRecord.m_strSPUTUM;
                objLisAddItemRefArr1[5].Value = p_objRecord.m_strSPUTUMXML;
                objLisAddItemRefArr1[6].Value = p_objRecord.m_strSUCKER;
                objLisAddItemRefArr1[7].Value = p_objRecord.m_strSUCKERXML;
                objLisAddItemRefArr1[8].Value = p_objRecord.m_strGESTICULATION;
                objLisAddItemRefArr1[9].Value = p_objRecord.m_strGESTICULATIONXML;
                objLisAddItemRefArr1[10].Value = p_objRecord.m_strORAL;
                objLisAddItemRefArr1[11].Value = p_objRecord.m_strORALXML;
                objLisAddItemRefArr1[12].Value = p_objRecord.m_strPERINEUM;
                objLisAddItemRefArr1[13].Value = p_objRecord.m_strPERINEUMXML;
                objLisAddItemRefArr1[14].Value = p_objRecord.m_strSPONGEBATH;
                objLisAddItemRefArr1[15].Value = p_objRecord.m_strSPONGEBATHXML;
                objLisAddItemRefArr1[16].Value = p_objRecord.m_strPT;
                objLisAddItemRefArr1[17].Value = p_objRecord.m_strPTXML;
                objLisAddItemRefArr1[18].Value = p_objRecord.m_strACT;
                objLisAddItemRefArr1[19].Value = p_objRecord.m_strACTXML;
                objLisAddItemRefArr1[20].Value = p_objRecord.m_strGLU;
                objLisAddItemRefArr1[21].Value = p_objRecord.m_strGLUXML;
                objLisAddItemRefArr1[22].Value = p_objRecord.m_strK;
                objLisAddItemRefArr1[23].Value = p_objRecord.m_strKXML;
                objLisAddItemRefArr1[24].Value = p_objRecord.m_strNAPLUS;
                objLisAddItemRefArr1[25].Value = p_objRecord.m_strNAPLUSXML;
                objLisAddItemRefArr1[26].Value = p_objRecord.m_strCL;
                objLisAddItemRefArr1[27].Value = p_objRecord.m_strCLXML;
                objLisAddItemRefArr1[28].Value = p_objRecord.m_strCAPLUS2;
                objLisAddItemRefArr1[29].Value = p_objRecord.m_strCAPLUS2XML;
                objLisAddItemRefArr1[30].Value = p_objRecord.m_strMMODEL;
                objLisAddItemRefArr1[31].Value = p_objRecord.m_strMMODELXML;
                objLisAddItemRefArr1[32].Value = p_objRecord.m_strPEE;
                objLisAddItemRefArr1[33].Value = p_objRecord.m_strPEEXML;
                objLisAddItemRefArr1[34].Value = p_objRecord.m_strTVTI;
                objLisAddItemRefArr1[35].Value = p_objRecord.m_strTVTIXML;
                objLisAddItemRefArr1[36].Value = p_objRecord.m_strFREQUENCY;
                objLisAddItemRefArr1[37].Value = p_objRecord.m_strFREQUENCYXML;
                objLisAddItemRefArr1[38].Value = p_objRecord.m_strO2;
                objLisAddItemRefArr1[39].Value = p_objRecord.m_strO2XML;
                objLisAddItemRefArr1[40].Value = p_objRecord.m_strSENSEIIVE;
                objLisAddItemRefArr1[41].Value = p_objRecord.m_strSENSEIIVEXML;
                objLisAddItemRefArr1[42].Value = p_objRecord.m_strSPEED;
                objLisAddItemRefArr1[43].Value = p_objRecord.m_strSPEEDXML;
                objLisAddItemRefArr1[44].Value = p_objRecord.m_strIE;
                objLisAddItemRefArr1[45].Value = p_objRecord.m_strIEXML;
                objLisAddItemRefArr1[46].Value = p_objRecord.m_strGASPRESS;
                objLisAddItemRefArr1[47].Value = p_objRecord.m_strGASPRESSXML;
                objLisAddItemRefArr1[48].Value = p_objRecord.m_strTV;
                objLisAddItemRefArr1[49].Value = p_objRecord.m_strTVXML;
                objLisAddItemRefArr1[50].Value = p_objRecord.m_strMV;
                objLisAddItemRefArr1[51].Value = p_objRecord.m_strMVXML;
                objLisAddItemRefArr1[52].Value = p_objRecord.m_strPIPEDEPTH;
                objLisAddItemRefArr1[53].Value = p_objRecord.m_strPIPEDEPTHXML;
                objLisAddItemRefArr1[54].Value = p_objRecord.m_strAIRCELLPRESS;
                objLisAddItemRefArr1[55].Value = p_objRecord.m_strAIRCELLPRESSXML;
                objLisAddItemRefArr1[56].Value = p_objRecord.m_strETCO2;
                objLisAddItemRefArr1[57].Value = p_objRecord.m_strETCO2XML;
                objLisAddItemRefArr1[58].Value = p_objRecord.m_strPH_RIGHT;
                objLisAddItemRefArr1[59].Value = p_objRecord.m_strPHXML;
                objLisAddItemRefArr1[60].Value = p_objRecord.m_strPCO2;
                objLisAddItemRefArr1[61].Value = p_objRecord.m_strPCO2XML;
                objLisAddItemRefArr1[62].Value = p_objRecord.m_strPAO2;
                objLisAddItemRefArr1[63].Value = p_objRecord.m_strPAO2XML;
                objLisAddItemRefArr1[64].Value = p_objRecord.m_strHCO3;
                objLisAddItemRefArr1[65].Value = p_objRecord.m_strHCO3XML;
                objLisAddItemRefArr1[66].Value = p_objRecord.m_strBE;
                objLisAddItemRefArr1[67].Value = p_objRecord.m_strBEXML;
                objLisAddItemRefArr1[68].Value = p_objRecord.m_strCUSTOMER1;
                objLisAddItemRefArr1[69].Value = p_objRecord.m_strCUSTOMER1XML;
                objLisAddItemRefArr1[70].Value = p_objRecord.m_strCUSTOMER2;
                objLisAddItemRefArr1[71].Value = p_objRecord.m_strCUSTOMER2XML;
                objLisAddItemRefArr1[72].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr1[73].DbType = DbType.DateTime;
                objLisAddItemRefArr1[73].Value = p_objRecord.m_dtmCreateDate;


                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngEff, objLisAddItemRefArr1);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region 保存签名集合
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecord.objSignerArr, lngSequence);

                #endregion

                #region 修改子表记录状态
                IDataParameter[] objLisAddItemRefArr3 = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr3);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr3[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr3[1].DbType = DbType.DateTime;
                objLisAddItemRefArr3[1].Value = p_objRecord.m_dtmCreateDate;
                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL3, ref lngEff, objLisAddItemRefArr3);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region  保存子表

                IDataParameter[] objLisAddItemRefArr2 = null;
                objHRPServ.CreateDatabaseParameter(80, out objLisAddItemRefArr2);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr2[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr2[1].DbType = DbType.DateTime;
                objLisAddItemRefArr2[1].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr2[2].DbType = DbType.DateTime;
                objLisAddItemRefArr2[2].Value = p_objRecord.m_dtmModifyDate;
                objLisAddItemRefArr2[3].Value = 1;// p_objRecord.m_bytStatus;
                objLisAddItemRefArr2[4].Value = p_objRecord.m_strAFTEROPDAYS;
                objLisAddItemRefArr2[5].Value = p_objRecord.m_strTEMPERATURE_RIGHT;
                objLisAddItemRefArr2[6].Value = p_objRecord.m_strENDTEMPERATURE_RIGHT;
                objLisAddItemRefArr2[7].Value = p_objRecord.m_strHR_RIGHT;
                objLisAddItemRefArr2[8].Value = p_objRecord.m_strRHYTHM_RIGHT;
                objLisAddItemRefArr2[9].Value = p_objRecord.m_strBPD_RIGHT;
                objLisAddItemRefArr2[10].Value = p_objRecord.m_strABP_RIGHT;
                objLisAddItemRefArr2[11].Value = p_objRecord.m_strCVP_RIGHT;
                objLisAddItemRefArr2[12].Value = p_objRecord.m_strSPO2_RIGHT;
                objLisAddItemRefArr2[13].Value = p_objRecord.m_strCONSCIOUSNESS_RIGHT;
                objLisAddItemRefArr2[14].Value = p_objRecord.m_strPUPIL_RIGHT;
                objLisAddItemRefArr2[15].Value = p_objRecord.m_strLIGHTREFLEXL_RIGHT;
                objLisAddItemRefArr2[16].Value = p_objRecord.m_strVASOAACTIVE_RIGHT;
                objLisAddItemRefArr2[17].Value = p_objRecord.m_strCARDIACDIURETIC_RIGHT;
                objLisAddItemRefArr2[18].Value = p_objRecord.m_strLIQUID1_RIGHT;
                objLisAddItemRefArr2[19].Value = p_objRecord.m_strLIQUID2_RIGHT;
                objLisAddItemRefArr2[20].Value = p_objRecord.m_strLIQUID3_RIGHT;
                objLisAddItemRefArr2[21].Value = p_objRecord.m_strLIQUID4_RIGHT;
                objLisAddItemRefArr2[22].Value = p_objRecord.m_strLIQUID5_RIGHT;
                objLisAddItemRefArr2[23].Value = p_objRecord.m_strFBOOL_RIGHT;
                objLisAddItemRefArr2[24].Value = p_objRecord.m_strPLASMA_RIGHT;
                objLisAddItemRefArr2[25].Value = p_objRecord.m_strNOSE1_RIGHT;
                objLisAddItemRefArr2[26].Value = p_objRecord.m_strNOSE2_RIGHT;
                objLisAddItemRefArr2[27].Value = p_objRecord.m_strINPERHOUR_RIGHT;
                objLisAddItemRefArr2[28].Value = p_objRecord.m_strINTOTAL_RIGHT;
                objLisAddItemRefArr2[29].Value = p_objRecord.m_strSTOOL_RIGHT;
                objLisAddItemRefArr2[30].Value = p_objRecord.m_strSTOOLTOTAL_RIGHT;
                objLisAddItemRefArr2[31].Value = p_objRecord.m_strPISS_RIGHT;
                objLisAddItemRefArr2[32].Value = p_objRecord.m_strPISSTOTAL_RIGHT;
                objLisAddItemRefArr2[33].Value = p_objRecord.m_strDRAIN1_RIGHT;
                objLisAddItemRefArr2[34].Value = p_objRecord.m_strDRAIN2_RIGHT;
                objLisAddItemRefArr2[35].Value = p_objRecord.m_strDRAIN3_RIGHT;
                objLisAddItemRefArr2[36].Value = p_objRecord.m_strDRAIN4_RIGHT;
                objLisAddItemRefArr2[37].Value = p_objRecord.m_strDRAIN5_RIGHT;
                objLisAddItemRefArr2[38].Value = p_objRecord.m_strOUTPERHOUR_RIGHT;
                objLisAddItemRefArr2[39].Value = p_objRecord.m_strOUTTOTAL_RIGHT;
                objLisAddItemRefArr2[40].Value = p_objRecord.m_strPOSTURE_RIGHT;
                objLisAddItemRefArr2[41].Value = p_objRecord.m_strSKIN_RIGHT;
                objLisAddItemRefArr2[42].Value = p_objRecord.m_strSPUTUM_RIGHT;
                objLisAddItemRefArr2[43].Value = p_objRecord.m_strSUCKER_RIGHT;
                objLisAddItemRefArr2[44].Value = p_objRecord.m_strGESTICULATION_RIGHT;
                objLisAddItemRefArr2[45].Value = p_objRecord.m_strORAL_RIGHT;
                objLisAddItemRefArr2[46].Value = p_objRecord.m_strPERINEUM_RIGHT;
                objLisAddItemRefArr2[47].Value = p_objRecord.m_strSPONGEBATH_RIGHT;
                objLisAddItemRefArr2[48].Value = p_objRecord.m_strPT_RIGHT;
                objLisAddItemRefArr2[49].Value = p_objRecord.m_strACT_RIGHT;
                objLisAddItemRefArr2[50].Value = p_objRecord.m_strGLU_RIGHT;
                objLisAddItemRefArr2[51].Value = p_objRecord.m_strK_RIGHT;
                objLisAddItemRefArr2[52].Value = p_objRecord.m_strNAPLUS_RIGHT;
                objLisAddItemRefArr2[53].Value = p_objRecord.m_strCL_RIGHT;
                objLisAddItemRefArr2[54].Value = p_objRecord.m_strCAPLUS2_RIGHT;
                objLisAddItemRefArr2[55].Value = p_objRecord.m_strMMODEL_RIGHT;
                objLisAddItemRefArr2[56].Value = p_objRecord.m_strPEE_RIGHT;
                objLisAddItemRefArr2[57].Value = p_objRecord.m_strTVTI_RIGHT;
                objLisAddItemRefArr2[58].Value = p_objRecord.m_strFREQUENCY_RIGHT;
                objLisAddItemRefArr2[59].Value = p_objRecord.m_strO2_RIGHT;
                objLisAddItemRefArr2[60].Value = p_objRecord.m_strSENSEIIVE_RIGHT;
                objLisAddItemRefArr2[61].Value = p_objRecord.m_strSPEED_RIGHT;
                objLisAddItemRefArr2[62].Value = p_objRecord.m_strIE_RIGHT;
                objLisAddItemRefArr2[63].Value = p_objRecord.m_strGASPRESS_RIGHT;
                objLisAddItemRefArr2[64].Value = p_objRecord.m_strTV_RIGHT;
                objLisAddItemRefArr2[65].Value = p_objRecord.m_strMV_RIGHT;
                objLisAddItemRefArr2[66].Value = p_objRecord.m_strPIPEDEPTH_RIGHT;
                objLisAddItemRefArr2[67].Value = p_objRecord.m_strAIRCELLPRESS_RIGHT;
                objLisAddItemRefArr2[68].Value = p_objRecord.m_strETCO2_RIGHT;
                objLisAddItemRefArr2[69].Value = p_objRecord.m_strPH_RIGHT;
                objLisAddItemRefArr2[70].Value = p_objRecord.m_strPCO2_RIGHT;
                objLisAddItemRefArr2[71].Value = p_objRecord.m_strPAO2_RIGHT;
                objLisAddItemRefArr2[72].Value = p_objRecord.m_strHCO3_RIGHT;
                objLisAddItemRefArr2[73].Value = p_objRecord.m_strBE_RIGHT;
                objLisAddItemRefArr2[74].Value = p_objRecord.m_strCUSTOMER1_RIGHT;
                objLisAddItemRefArr2[75].Value = p_objRecord.m_strCUSTOMER2_RIGHT;
                objLisAddItemRefArr2[76].Value = p_objRecord.m_strBPS_RIGHT;
                objLisAddItemRefArr2[77].Value = p_objRecord.m_strLIGHTREFLEXR_RIGHT;
                objLisAddItemRefArr2[78].Value = p_objRecord.m_strRESPIRATION_RIGHT;
                objLisAddItemRefArr2[79].Value = p_objRecord.m_strPUPIR_RIGHT;


                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL4, ref lngEff, objLisAddItemRefArr2);
                if (lngRes <= 0) return lngRes;
                #endregion

                #region 修改护理内容
                //若有输入则保存
                if (p_objRecord.objRecordContent.m_strCONTENT.Length != 0)
                {
                    m_lngDeleteContent2DB(p_objRecord);
                    m_lngAddContent2DB(p_objRecord.objRecordContent, lngSequence, p_objRecord.m_dtmModifyDate);
                }
                #endregion


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
            if (p_objRecordContent == null || p_objRecordContent.m_strRegisterID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsICUNurseRecord p_objRecord = (clsICUNurseRecord)p_objRecordContent;

            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            #region SQL
            //删除主记录
            string strSQL1 = @"update t_emr_icunurse t
                           set t.status_int               = ?,
                               t.deactivedoperatorid_vchr = ?,
                               t.deactiveddate_dat        = ?
                         where t.registerid_vchr = ?
                           and t.createdate_dat = ?";
            //删除子记录
            string strSQL2 = @"update t_emr_icunursecontent t set t.status_int=0 where t.registerid_vchr= ? and t.createdate_dat=?";
            //删除护理内容
            string strSQL3 = @"update t_emr_icunurserecord t set t.status_int=0 where t.registerid_vchr= ? and t.createdate_dat=?";

            #endregion
            try
            {
                //删除主记录
                System.Data.IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(5, out objLisAddItemRefArr1);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr1[0].Value = 0;
                objLisAddItemRefArr1[1].Value = p_objRecord.m_strDeActivedOperatorID;
                objLisAddItemRefArr1[2].DbType = DbType.DateTime;
                objLisAddItemRefArr1[2].Value = p_objRecord.m_dtmDeActivedDate;

                objLisAddItemRefArr1[3].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr1[4].DbType = DbType.DateTime;
                objLisAddItemRefArr1[4].Value = p_objRecord.m_dtmCreateDate;

                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngEff, objLisAddItemRefArr1);

                //删除子记录
                System.Data.IDataParameter[] objLisAddItemRefArr2 = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr2);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr2[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr2[1].DbType = DbType.DateTime;
                objLisAddItemRefArr2[1].Value = p_objRecord.m_dtmCreateDate;

                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngEff, objLisAddItemRefArr2);

                //删除护理内容
                System.Data.IDataParameter[] objLisAddItemRefArr3 = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr3);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr3[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr3[1].DbType = DbType.DateTime;
                objLisAddItemRefArr3[1].Value = p_objRecord.m_dtmCreateDate;

                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL3, ref lngEff, objLisAddItemRefArr3);

                //删除相连的护理内容
                m_lngDeleteContent2DB(p_objRecord);


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
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
           //未实现
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
            //未实现
            //返回
            return lngRes;
        }
        #endregion

        #region 获取指定天数记录列表
        /// <summary>
        /// 获取指定天数记录列表
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_dtReturn"></param>
        /// <returns></returns>
        [AutoComplete]
        protected long m_lngGetRecorContentdlist(string p_strRegisterID,
            string p_strAfterDays,
            out DataTable p_dtReturn)
        {
            p_dtReturn = null;

            //检查参数
            if (p_strRegisterID == null || p_strRegisterID == "" || p_strAfterDays == null || p_strAfterDays == "")
                return (long)enmOperationResult.Parameter_Error;
            string strSQL = @"select registerid_vchr,
       createdate_dat,
       modifydate,
       status_int,
       afteropdays,
       temperature_right,
       endtemperature_right,
       hr_right,
       rhythm_right,
       bps_right,
       bpd_right,
       abp_right,
       cvp_right,
       spo2_right,
       consciousness_right,
       pupil_right,
       pupir_right,
       lightreflexl_right,
       lightreflexr_right,
       vasoaactive_right,
       cardiacdiuretic_right,
       liquid1_right,
       liquid2_right,
       liquid3_right,
       liquid4_right,
       liquid5_right,
       fbool_right,
       plasma_right,
       nose1_right,
       nose2_right,
       inperhour_right,
       intotal_right,
       stool_right,
       stooltotal_right,
       piss_right,
       pisstotal_right,
       drain1_right,
       drain2_right,
       drain3_right,
       drain4_right,
       drain5_right,
       outperhour_right,
       outtotal_right,
       posture_right,
       skin_right,
       sputum_right,
       sputum1_right,
       sucker_right,
       gesticulation_right,
       oral_right,
       perineum_right,
       spongebath_right,
       pt_right,
       act_right,
       glu_right,
       k_right,
       naplus_right,
       cl_right,
       caplus2_right,
       mmodel_right,
       pee_right,
       tvti_right,
       frequency_right,
       o2_right,
       senseiive_right,
       speed_right,
       ie_right,
       gaspress_right,
       tv_right,
       mv_right,
       pipedepth_right,
       aircellpress_right,
       etco2_right,
       ph_right,
       pco2_right,
       pao2_right,
       hco3_right,
       be_right,
       customer1_right,
       customer2_right,
       respiration_right
  from t_emr_icunursecontent t
 where t.registerid_vchr = ?
   and t.afteropdays = ?
   and t.status_int = 1";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].Value = p_strAfterDays;

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtReturn, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        
        #endregion

        #region 获取指定天数护理内容
        /// <summary>
        /// 获取指定天数护理内容
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_dtReturn"></param>
        /// <returns></returns>
        [AutoComplete]
        protected long m_lngGetRecordlist(string p_strRegisterID,
            string p_strAfterDays,
            out DataTable p_dtReturn)
        {
            p_dtReturn = null;

            //检查参数
            if (p_strRegisterID == null || p_strRegisterID == "" || p_strAfterDays == null || p_strAfterDays == "")
                return (long)enmOperationResult.Parameter_Error;
            string strSQL = @"   select registerid_vchr,
       createdate_dat,
       modifydate,
       status_int,
       afteropdays,
       temperature_right,
       endtemperature_right,
       hr_right,
       rhythm_right,
       bps_right,
       bpd_right,
       abp_right,
       cvp_right,
       spo2_right,
       consciousness_right,
       pupil_right,
       pupir_right,
       lightreflexl_right,
       lightreflexr_right,
       vasoaactive_right,
       cardiacdiuretic_right,
       liquid1_right,
       liquid2_right,
       liquid3_right,
       liquid4_right,
       liquid5_right,
       fbool_right,
       plasma_right,
       nose1_right,
       nose2_right,
       inperhour_right,
       intotal_right,
       stool_right,
       stooltotal_right,
       piss_right,
       pisstotal_right,
       drain1_right,
       drain2_right,
       drain3_right,
       drain4_right,
       drain5_right,
       outperhour_right,
       outtotal_right,
       posture_right,
       skin_right,
       sputum_right,
       sputum1_right,
       sucker_right,
       gesticulation_right,
       oral_right,
       perineum_right,
       spongebath_right,
       pt_right,
       act_right,
       glu_right,
       k_right,
       naplus_right,
       cl_right,
       caplus2_right,
       mmodel_right,
       pee_right,
       tvti_right,
       frequency_right,
       o2_right,
       senseiive_right,
       speed_right,
       ie_right,
       gaspress_right,
       tv_right,
       mv_right,
       pipedepth_right,
       aircellpress_right,
       etco2_right,
       ph_right,
       pco2_right,
       pao2_right,
       hco3_right,
       be_right,
       customer1_right,
       customer2_right,
       respiration_right
                                                 from t_emr_icunurserecord t
                                                where t.registerid_vchr = ?
                                                  and t.afteropdays = ?
                                                   and t.status_int = 1";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].Value = p_strAfterDays;

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtReturn, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        } 
        #endregion

        #region 保存、修改预设值
        /// <summary>
        /// 保存、修改预设值
        /// 此方法性能不是很好
        /// 每新增、修改一条记录要操作两次数据库
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected long m_lngPreDefine2DB(clsICUNurseRecord p_objRecord)
        {
            //检查参数                              
            if (p_objRecord == null || p_objRecord.m_strRegisterID == null)
                return (long)enmOperationResult.Parameter_Error;


            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSQL = @"insert into t_emr_icunurseaid (registerid_vchr,afteropdays,weight,opname,
                                    liquid1,liquid2,liquid3,liquid4,liquid5,drain1,drain2,drain3,drain4,drain5,transferid_chr) 
                            values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            string strSQL1 = @"delete from t_emr_icunurseaid t where t.registerid_vchr=? and t.transferid_chr = ? and t.afteropdays=?";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr1);
                objLisAddItemRefArr1[0].Value = p_objRecord.m_strRegisterID;
                if (string.IsNullOrEmpty(p_objRecord.m_strTRANSFERID))
                {
                    objLisAddItemRefArr1[1].Value = "000000000000";
                }
                else
                {
                    objLisAddItemRefArr1[1].Value = p_objRecord.m_strTRANSFERID;
                }                
                objLisAddItemRefArr1[2].Value = p_objRecord.m_strAFTEROPDAYS;

                //执行SQL 先删除
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngEff, objLisAddItemRefArr1);

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(15, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strAFTEROPDAYS;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strWEIGHT;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strOPNAME;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strLIQUID1D;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strLIQUID2D;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strLIQUID3D;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strLIQUID4D;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strLIQUID5D;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strDRAIN1D;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDRAIN2D;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strDRAIN3D;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strDRAIN4D;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strDRAIN5D;
                if (string.IsNullOrEmpty(p_objRecord.m_strTRANSFERID))
                {
                    objLisAddItemRefArr[14].Value = "000000000000";
                }
                else
                {
                    objLisAddItemRefArr[14].Value = p_objRecord.m_strTRANSFERID;
                }

                //执行SQL插入
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objLisAddItemRefArr);
                if (lngRes <= 0) return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        } 
        #endregion

        #region 新增护理记录内容

        [AutoComplete]
        protected long m_lngAddContent2DB(clsICUNurseRecordContent p_objRecord,long lngS,DateTime p_dtmModifyDate)
        {
            //检查参数
            if (p_objRecord == null || p_objRecord.m_strRegisterID == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL4 = @"insert into t_emr_icunurserecord (registerid_vchr,createdate_dat,status_int,content,contentxml,content_right,afteropdays,
                                       markstatus,createuserid_vchr,recorduserid_vchr,recorddate_dat,sequence_int,modifydate) 
                                    values (?,?,?,?,?,?,?,?,?,?,?,?,?)";

                System.Data.IDataParameter[] objLisAddItemRefArr4 = null;
                objHRPServ.CreateDatabaseParameter(13, out objLisAddItemRefArr4);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr4[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr4[1].DbType = DbType.DateTime;
                objLisAddItemRefArr4[1].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr4[2].Value = p_objRecord.m_bytStatus;
                objLisAddItemRefArr4[3].Value = p_objRecord.m_strCONTENT;
                objLisAddItemRefArr4[4].Value = p_objRecord.m_strCONTENTXML;
                objLisAddItemRefArr4[5].Value = p_objRecord.m_strCONTENT_RIGHT;
                objLisAddItemRefArr4[6].Value = p_objRecord.m_strAFTEROPDAYS;
                objLisAddItemRefArr4[7].Value = p_objRecord.m_intMarkStatus;
                objLisAddItemRefArr4[8].Value = p_objRecord.m_strCreateUserID;
                objLisAddItemRefArr4[9].Value = p_objRecord.m_strRecordUserID;
                objLisAddItemRefArr4[10].DbType = DbType.Date;
                objLisAddItemRefArr4[10].Value = p_objRecord.m_dtmRecordDate;
                objLisAddItemRefArr4[11].Value = lngS;
                objLisAddItemRefArr4[12].DbType = DbType.DateTime;
                objLisAddItemRefArr4[12].Value = p_dtmModifyDate;


                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL4, ref lngEff, objLisAddItemRefArr4);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;



        }
        
        #endregion

        #region 修改护理记录内容
        [AutoComplete]
        protected long m_lngModifyContent2DB(clsICUNurseRecordContent p_objRecord, long lngS)
        {
            //检查参数
            if (p_objRecord == null || p_objRecord.m_strRegisterID == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL4 = @"update t_emr_icunurserecord
                                    set    status_int      = ?, 
                                           content         = ?,
                                           contentxml      = ?,
                                           content_right   = ?,
                                           sequence_int    = ?

                                    where  registerid_vchr = ?
                                    and    createdate_dat  = ?";

                System.Data.IDataParameter[] objLisAddItemRefArr4 = null;
                objHRPServ.CreateDatabaseParameter(7, out objLisAddItemRefArr4);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr4[0].Value = p_objRecord.m_bytStatus;
                objLisAddItemRefArr4[1].Value = p_objRecord.m_strCONTENT;
                objLisAddItemRefArr4[2].Value = p_objRecord.m_strCONTENTXML;
                objLisAddItemRefArr4[3].Value = p_objRecord.m_strCONTENT_RIGHT;
                objLisAddItemRefArr4[4].Value = lngS;
                objLisAddItemRefArr4[5].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr4[6].DbType = DbType.DateTime;
                objLisAddItemRefArr4[6].Value = p_objRecord.m_dtmCreateDate;

                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL4, ref lngEff, objLisAddItemRefArr4);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 删除护理记录内容
        [AutoComplete]
        protected long m_lngDeleteContent2DB(clsICUNurseRecord p_objRecord)
        {
            //检查参数
            if (p_objRecord == null || p_objRecord.m_strRegisterID == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL4 = @"update t_emr_icunurserecord
                                    set    
                                           status_int      = 0
                                    where  registerid_vchr = ?
                                    and    createdate_dat  = ?";

                System.Data.IDataParameter[] objLisAddItemRefArr4 = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr4);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr4[0].Value = p_objRecord.m_strRegisterID;
                objLisAddItemRefArr4[1].DbType = DbType.DateTime;
                objLisAddItemRefArr4[1].Value = p_objRecord.m_dtmCreateDate;

                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL4, ref lngEff, objLisAddItemRefArr4);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region 获取指定天数记录集
        /// <summary>
        /// 获取指定天数记录集
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="pstrRegisterID">入院登记号</param>
        /// <param name="p_strTransferID">转科流水号</param>
        /// <param name="intDay">术后/入ICU天数</param>
        /// <param name="pds">返回记录集</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataByDays(string pstrRegisterID, string p_strTransferID, int intDay, out DataSet pds)
        {
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	
            pds=new DataSet();
            //检查参数
            if (pstrRegisterID == null || pstrRegisterID == "" )
                return (long)enmOperationResult.Parameter_Error;

            if (string.IsNullOrEmpty(p_strTransferID))
            {
                p_strTransferID = "000000000000";
            }
            
            long lngRes = 0;

            #region SQL
            string strSQL1 = @"select t.recorddate_dat,t.registerid_vchr,
                               t.createdate_dat  ,
                               t.createuserid_vchr,
                               t.recorduserid_vchr,
                               (select e1.lastname_vchr from t_bse_employee e1 where e1.empid_chr = t.createuserid_vchr) createusername ,
                               d.temperature_right,
                               d.endtemperature_right,
                               d.hr_right,
                               d.rhythm_right,
                               d.respiration_right,
                               d.bps_right || '/' || d.bpd_right as bp_right,
                               d.abp_right,
                               d.cvp_right,
                               d.spo2_right,
                               d.consciousness_right,
                               d.pupil_right || '/' || d.pupir_right as pupi_right,
                               d.lightreflexl_right || '/' || d.lightreflexr_right as lightreflex_right,
                               d.vasoaactive_right,
                               d.cardiacdiuretic_right,
                               d.liquid1_right,
                               d.liquid2_right,
                               d.liquid3_right,
                               d.liquid4_right,
                               d.liquid5_right,
                               d.fbool_right || '/' || d.plasma_right as plasma_right,
                               d.nose1_right || '/' || d.nose2_right as nose_right,
                               d.inperhour_right,
                               d.intotal_right,
                               d.stool_right,
                               d.stooltotal_right,
                               d.piss_right,
                               d.pisstotal_right,
                               d.drain1_right,
                               d.drain2_right,
                               d.drain3_right,
                               d.drain4_right,
                               d.drain5_right,
                               d.outperhour_right,
                               d.outtotal_right,
                               d.posture_right,
                               d.skin_right,
                               d.sputum_right || '/' || d.sputum1_right as sputum_right,
                               d.sucker_right,
                               d.gesticulation_right,
                               d.oral_right,
                               d.perineum_right,
                               d.spongebath_right,
                               d.pt_right,
                               d.act_right,
                               d.glu_right,
                               d.k_right,
                               d.naplus_right,
                               d.cl_right,
                               d.caplus2_right,
                               f_getsignsbyseq(t.sequence_int),
                               t.afteropdays
                          from t_emr_icunurse t
                         inner join t_emr_icunursecontent d on t.registerid_vchr =
                                                               d.registerid_vchr
                                                           and t.createdate_dat = d.createdate_dat
                          where t.registerid_vchr = ? and t.transferid_chr = ?
                               and t.afteropdays = ?
                               and t.status_int = 1 and d.status_int = 1";
            string strSQL2 = @"select  t.recorddate_dat,t.registerid_vchr,
                               t.createdate_dat,
                               t.createuserid_vchr,
                               t.recorduserid_vchr,
                               (select e1.lastname_vchr from t_bse_employee e1 where e1.empid_chr = t.createuserid_vchr) createuserName ,
                               d.mmodel_right,
                               d.pee_right,
                               d.tvti_right,
                               d.frequency_right,
                               d.o2_right,
                               d.senseiive_right,
                               d.speed_right,
                               d.ie_right,
                               d.gaspress_right,
                               d.tv_right,
                               d.mv_right,
                               d.pipedepth_right,
                               d.aircellpress_right,
                               d.etco2_right,
                               d.ph_right,
                               d.pco2_right,
                               d.pao2_right,
                               d.hco3_right,
                               d.be_right,
                               f_getsignsbyseq(t.sequence_int)
                          from t_emr_icunurse t
                         inner join t_emr_icunursecontent d on t.registerid_vchr =
                                                               d.registerid_vchr
                                                           and t.createdate_dat = d.createdate_dat
                         where t.registerid_vchr =? and t.transferid_chr = ?
                               and t.afteropdays =?
                               and t.status_int = 1 and d.status_int = 1";
            string strSQL3 = @"select  t.recorddate_dat,t.registerid_vchr,
                               t.createdate_dat,
                               t.createuserid_vchr,
                               t.recorduserid_vchr,
                               (select e1.lastname_vchr from t_bse_employee e1 where e1.empid_chr = t.createuserid_vchr) createusername ,
                               d.content,
                               f_getsignsbyseq(t.sequence_int)
                          from t_emr_icunurse t
                         inner join t_emr_icunurserecord d on t.registerid_vchr = d.registerid_vchr
                                                          and t.createdate_dat = d.createdate_dat
                          where t.registerid_vchr = ? and t.transferid_chr = ?
                               and t.afteropdays =?
                               and t.status_int = 1 and d.status_int = 1";
            string strSQL4 = @"select registerid_vchr,
               afteropdays,
               weight,
               opname,
               liquid1,
               liquid2,
               liquid3,
               liquid4,
               liquid5,
               drain1,
               drain2,
               drain3,
               drain4,
               drain5,
               transferid_chr
  from t_emr_icunurseaid t
 where t.registerid_vchr = ?
   and t.transferid_chr = ?
   and t.afteropdays = ?
 order by t.afteropdays desc";

            string strSQL5 = @"select t.registerid_vchr,
       t.createdate_dat,
       t.createuserid_vchr,
       t.afteropdays,
       t.transferid_chr,
       t.statisticsstart_dat,
       t.statisticsend_dat,
       t.liquid1_right,
       t.liquid2_right,
       t.liquid3_right,
       t.liquid4_right,
       t.liquid5_right,
       t.fbool_right,
       t.plasma_right,
       t.nose1_right,
       t.nose2_right,
       t.intotal_right,
       t.stool_right,
       t.piss_right,
       t.drain1_right,
       t.drain2_right,
       t.drain3_right,
       t.drain4_right,
       t.drain5_right,
       t.outtotal_right,
       f_getsignsbyseq(t.sequence_int) stsign
  from t_emr_icunursestatistics t
 where t.registerid_vchr = ?
   and t.transferid_chr = ?
   and t.afteropdays = ? and t.status_int = 1";
            #endregion 

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr4 = null;
                objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr4);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr4[0].Value = pstrRegisterID;
                objLisAddItemRefArr4[1].Value = p_strTransferID;
                objLisAddItemRefArr4[2].Value = intDay.ToString();

                //执行SQL
                DataTable dtbResult1 = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL1, ref dtbResult1, objLisAddItemRefArr4);
               dtbResult1.Columns["registerid_vchr"].ColumnName = "入院登记号";
               dtbResult1.Columns["createdate_dat"].ColumnName = "创建时间";
               dtbResult1.Columns["createuserid_vchr"].ColumnName = "创建者ID";
               dtbResult1.Columns["createuserName"].ColumnName = "创建者名称";
               dtbResult1.Columns["recorduserid_vchr"].ColumnName = "记录者ID";
               dtbResult1.Columns["recorddate_dat"].ColumnName = "记录时间";
               dtbResult1.Columns["F_GetSignsBySeq(t.sequence_int)"].ColumnName = "签名";
               dtbResult1.Columns["Temperature_Right"].ColumnName = "体温";
               dtbResult1.Columns["endtemperature_right"].ColumnName = "末梢体温";
               dtbResult1.Columns["hr_right"].ColumnName = "心率";
               dtbResult1.Columns["rhythm_right"].ColumnName = "心律";
               dtbResult1.Columns["RESPIRATION_RIGHT"].ColumnName = "呼吸";
               dtbResult1.Columns["bp_right"].ColumnName = "血压";
               dtbResult1.Columns["abp_right"].ColumnName = "ABP";
               dtbResult1.Columns["cvp_right"].ColumnName = "CVP";
               dtbResult1.Columns["spo2_right"].ColumnName = "SpO2(%)";
               dtbResult1.Columns["consciousness_right"].ColumnName = "意识";
               dtbResult1.Columns["pupi_right"].ColumnName = "瞳孔左/右";
               dtbResult1.Columns["lightreflex_right"].ColumnName = "对光反射左右";
               dtbResult1.Columns["vasoaactive_right"].ColumnName = "血管活性药物";
               dtbResult1.Columns["cardiacdiuretic_right"].ColumnName = "强心利尿药及其他特殊药物";
               dtbResult1.Columns["liquid1_right"].ColumnName = "入量1";
               dtbResult1.Columns["liquid2_right"].ColumnName = "入量2";
               dtbResult1.Columns["liquid3_right"].ColumnName = "入量3";
               dtbResult1.Columns["liquid4_right"].ColumnName = "入量4";
               dtbResult1.Columns["liquid5_right"].ColumnName = "入量5";
               dtbResult1.Columns["plasma_right"].ColumnName = "全血/血浆";
               dtbResult1.Columns["nose_right"].ColumnName = "鼻饲/进饲";
               dtbResult1.Columns["inperhour_right"].ColumnName = "每时入量累计";
               dtbResult1.Columns["intotal_right"].ColumnName = "入量总量累计";
               dtbResult1.Columns["stool_right"].ColumnName = "大便出量";
               dtbResult1.Columns["stooltotal_right"].ColumnName = "大便累计";
               dtbResult1.Columns["piss_right"].ColumnName = "小便出量";
               dtbResult1.Columns["pisstotal_right"].ColumnName = "小便累计";
               dtbResult1.Columns["drain1_right"].ColumnName = "出量1";
               dtbResult1.Columns["drain2_right"].ColumnName = "出量2";
               dtbResult1.Columns["drain3_right"].ColumnName = "出量3";
               dtbResult1.Columns["drain4_right"].ColumnName = "出量4";
               dtbResult1.Columns["drain5_right"].ColumnName = "出量5";
               dtbResult1.Columns["outperhour_right"].ColumnName = "每时出量累计D";
               dtbResult1.Columns["outtotal_right"].ColumnName = "出量总量累计";
               dtbResult1.Columns["posture_right"].ColumnName = "体位";
               dtbResult1.Columns["skin_right"].ColumnName = "皮肤";
               dtbResult1.Columns["sputum_right"].ColumnName = "痰色/痰量";
               dtbResult1.Columns["sucker_right"].ColumnName = "吸痰";
               dtbResult1.Columns["gesticulation_right"].ColumnName = "体疗";
               dtbResult1.Columns["oral_right"].ColumnName = "口腔护理";
               dtbResult1.Columns["perineum_right"].ColumnName = "会阴冲洗";
               dtbResult1.Columns["spongebath_right"].ColumnName = "擦浴";
               dtbResult1.Columns["pt_right"].ColumnName = "PT";
               dtbResult1.Columns["act_right"].ColumnName = "ACT";
               dtbResult1.Columns["glu_right"].ColumnName = "glu";
               dtbResult1.Columns["k_right"].ColumnName = "K";
               dtbResult1.Columns["naplus_right"].ColumnName = "Na++";
               dtbResult1.Columns["cl_right"].ColumnName = "CL-";
               dtbResult1.Columns["caplus2_right"].ColumnName = "Ca++";
               dtbResult1.Columns["AFTEROPDAYS"].ColumnName = "术后天数";
               pds.Tables.Add(dtbResult1);

                System.Data.IDataParameter[] objLisAddItemRefArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr2);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr2[0].Value = pstrRegisterID;
                objLisAddItemRefArr2[1].Value = p_strTransferID;
                objLisAddItemRefArr2[2].Value = intDay.ToString();
                DataTable dtbResult2 = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL2, ref dtbResult2, objLisAddItemRefArr2);
                pds.Tables.Add(dtbResult2);
                dtbResult2.Columns["registerid_vchr"].ColumnName = "入院登记号";
                dtbResult2.Columns["createdate_dat"].ColumnName = "创建时间";
                dtbResult2.Columns["createuserid_vchr"].ColumnName = "创建者ID";
                dtbResult2.Columns["createuserName"].ColumnName = "创建者名称";
                dtbResult2.Columns["recorduserid_vchr"].ColumnName = "记录者ID";
                dtbResult2.Columns["recorddate_dat"].ColumnName = "记录时间";
                dtbResult2.Columns["F_GetSignsBySeq(t.sequence_int)"].ColumnName = "签名";
                dtbResult2.Columns["mmodel_right"].ColumnName = "机械通气模式";
               dtbResult2.Columns["pee_right"].ColumnName = "PEE P/Pi";
               dtbResult2.Columns["tvti_right"].ColumnName = "TV/Ti";
               dtbResult2.Columns["frequency_right"].ColumnName = "频率";
               dtbResult2.Columns["o2_right"].ColumnName = "氧浓度";
               dtbResult2.Columns["senseiive_right"].ColumnName = "灵敏度";
               dtbResult2.Columns["speed_right"].ColumnName = "流速";
               dtbResult2.Columns["ie_right"].ColumnName = "I:E";
               dtbResult2.Columns["gaspress_right"].ColumnName = "气道压力";
               dtbResult2.Columns["tv_right"].ColumnName = "TV";
               dtbResult2.Columns["mv_right"].ColumnName = "MV";
               dtbResult2.Columns["pipedepth_right"].ColumnName = "插管深度";
               dtbResult2.Columns["aircellpress_right"].ColumnName = "气囊压力";
               dtbResult2.Columns["etco2_right"].ColumnName = "ET CO2";
               dtbResult2.Columns["ph_right"].ColumnName = "PH";
               dtbResult2.Columns["pco2_right"].ColumnName = "PCO2";
               dtbResult2.Columns["pao2_right"].ColumnName = "PaO2";
               dtbResult2.Columns["hco3_right"].ColumnName = "HCO3";
               dtbResult2.Columns["be_right"].ColumnName = "BE";



                System.Data.IDataParameter[] objLisAddItemRefArr3 = null;
                objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr3);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr3[0].Value = pstrRegisterID;
                objLisAddItemRefArr3[1].Value = p_strTransferID; 
                objLisAddItemRefArr3[2].Value = intDay.ToString();
                DataTable dtbResult3 = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL3, ref dtbResult3, objLisAddItemRefArr3);
                pds.Tables.Add(dtbResult3);
                dtbResult3.Columns["registerid_vchr"].ColumnName = "入院登记号";
                dtbResult3.Columns["createdate_dat"].ColumnName = "创建时间";
                dtbResult3.Columns["createuserid_vchr"].ColumnName = "创建者ID";
                dtbResult3.Columns["createuserName"].ColumnName = "创建者名称";
                dtbResult3.Columns["recorduserid_vchr"].ColumnName = "记录者ID";
                dtbResult3.Columns["recorddate_dat"].ColumnName = "记录时间";
                dtbResult3.Columns["F_GetSignsBySeq(t.sequence_int)"].ColumnName = "签名";
                dtbResult3.Columns["content"].ColumnName = "护理内容";


                  System.Data.IDataParameter[] objLisAddItemRefArr5 = null;
                objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr5);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr5[0].Value = pstrRegisterID;
                objLisAddItemRefArr5[1].Value = p_strTransferID;
                objLisAddItemRefArr5[2].Value = intDay.ToString();
                  DataTable dtbResult4 = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL4, ref dtbResult4, objLisAddItemRefArr5);
                pds.Tables.Add(dtbResult4);

                System.Data.IDataParameter[] objLisAddItemRefArr6 = null;
                objHRPServ.CreateDatabaseParameter(3, out objLisAddItemRefArr6);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr6[0].Value = pstrRegisterID;
                objLisAddItemRefArr6[1].Value = p_strTransferID;
                objLisAddItemRefArr6[2].Value = intDay.ToString();
                DataTable dtbResult5 = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL5, ref dtbResult5, objLisAddItemRefArr6);
                pds.Tables.Add(dtbResult5);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        /// <summary>
        /// 第一次打开窗体获取记录集
        /// 默认返回最大天数，若没有返回0天
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="pstrRegisterID"></param>
        /// <param name="p_strTransferID"></param>
        /// <param name="pds"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataByFirst( string pstrRegisterID,string p_strTransferID, out DataSet pds)
        {
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	
            pds = new DataSet();
            //检查参数
            if (pstrRegisterID == null || pstrRegisterID == "")
                return (long)enmOperationResult.Parameter_Error;

            if (string.IsNullOrEmpty(p_strTransferID))
            {
                p_strTransferID = "000000000000";
            }
            string strSQL4 = @"select afteropdays,transferid_chr
                              from (select t.afteropdays,t.transferid_chr
                                      from t_emr_icunurseaid t
                                     where t.registerid_vchr = ?
                                      and t.transferid_chr = ?
                                     order by t.afteropdays desc) d
                             where rownum = 1";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try 
            {
                int intDays = 0;
                string strTransferID = string.Empty;

                System.Data.IDataParameter[] objLisAddItemRefArr5 = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr5);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr5[0].Value = pstrRegisterID;
                objLisAddItemRefArr5[1].Value = p_strTransferID;

                DataTable dtbResult4 = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL4, ref dtbResult4, objLisAddItemRefArr5);
                if (dtbResult4.Rows.Count >0)
                {
                    try
                    {
                        intDays = int.Parse(dtbResult4.Rows[0][0].ToString());
                    }
                    catch (Exception)
                    {
                        intDays=0;
                    }
                    
                }
                //获取数据
                m_lngGetDataByDays( pstrRegisterID, p_strTransferID, intDays, out pds);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        /// <summary>
        /// 更新首次打印时间
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateFirstPrintDate(string p_strRegisterID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strUpdateFirstPrintDateSQL = @"update t_emr_icunurse set firstprintdate_dat=?
where registerid_vchr=? and firstprintdate_dat is null and status_int=1";
            try
            {
                //检查参数                              
                if (string.IsNullOrEmpty(p_strRegisterID))
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[1].Value = p_strRegisterID;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
        /// 获取所有转入ICU的转科流水号及入科日期
        /// </summary>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strTransferIDArr">转科流水号</param>
        /// <param name="p_dtmInDeptDateArr">入科日期</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientTransferInICUID(string p_strRegisterID, out string[] p_strTransferIDArr, out DateTime[] p_dtmInDeptDateArr)
        {
            p_strTransferIDArr = null;
            p_dtmInDeptDateArr = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
                return -1;
            long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            string strSQl = @"select t.transferid_chr,t.modify_dat
  from t_opr_bih_transfer t,t_bse_deptconfig c
 where t.registerid_chr = ''
 and t.type_int <> 1
 and t.targetdeptid_chr = c.deptid_chr
 and c.type_int = 1
 order by t.transferid_chr desc";
            try
            {
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 )
                {
                    int intRowsCount = dtResult.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        List<string> strTransferID = new List<string>();
                        List<DateTime> dtmInDate = new List<DateTime>();
                        for (int j1 = 0; j1 < intRowsCount; j1++)
                        {
                            DateTime dtmTemp = DateTime.MinValue;
                            if (DateTime.TryParse(dtResult.Rows[j1]["modify_dat"].ToString(),out dtmTemp))
                            {
                                strTransferID.Add(dtResult.Rows[j1]["transferid_chr"].ToString());
                                dtmInDate.Add(dtmTemp);
                            }
                        }
                        p_strTransferIDArr = strTransferID.ToArray();
                        p_dtmInDeptDateArr = dtmInDate.ToArray();
                    }                    
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
        /// 获取指定时间上一次的记录的总量
        /// 如果指定时间为DateTime.MinValue，则将获取最后一次记录的总量
        /// </summary>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strTransferID">转科流水号</param>
        /// <param name="p_dtmSpesifyTime">指定时间</param>
        /// <param name="p_dblCountIN">入量累计</param>
        /// <param name="p_dblCountOUT">出量累计</param>
        /// <param name="p_dblStoolTotal">大便总量</param>
        /// <param name="p_dblPissTotal">小便总量</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastGross(string p_strRegisterID, string p_strTransferID, DateTime p_dtmSpesifyTime,
            out double p_dblCountIN, out double p_dblCountOUT,out double p_dblStoolTotal, out double p_dblPissTotal)
        {
            p_dblCountIN = 0;
            p_dblCountOUT = 0; 
            p_dblStoolTotal = 0;
            p_dblPissTotal = 0;

            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }
            if (string.IsNullOrEmpty(p_strTransferID))
            {
                p_strTransferID = "000000000000";
            }
            long lngRes = 0;

            string strDate = string.Empty;
            if (p_dtmSpesifyTime != DateTime.MinValue)
            {
                strDate = " and a.recorddate_dat < ?";
            }
            clsHRPTableService objTabService = new clsHRPTableService();
            string strSQl = clsDatabaseSQLConvert.s_StrTop1 + @" b.stooltotal_right,
       b.pisstotal_right,
       b.intotal_right,
       b.outtotal_right
  from t_emr_icunurse a, t_emr_icunursecontent b
 where a.registerid_vchr = b.registerid_vchr
   and a.createdate_dat = b.createdate_dat
   and a.status_int = 1
   and b.status_int = 1
   and a.registerid_vchr = ?
   and a.transferid_chr = ?
   " + strDate + " order by a.recorddate_dat desc " + clsDatabaseSQLConvert.s_StrRownum;
            try
            {
                IDataParameter[] objDPArr = null;
                if (p_dtmSpesifyTime != DateTime.MinValue)
                {
                    objTabService.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strRegisterID;
                    objDPArr[1].Value = p_strTransferID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_dtmSpesifyTime;
                }
                else
                {
                    objTabService.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strRegisterID;
                    objDPArr[1].Value = p_strTransferID;
                }                

                DataTable dtResult = new DataTable();
                lngRes = objTabService.lngGetDataTableWithParameters(strSQl, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    double dblTemp = 0;
                    if (double.TryParse(dtResult.Rows[0]["STOOLTOTAL_RIGHT"].ToString(),out dblTemp))
                    {
                        p_dblStoolTotal = dblTemp;
                    }

                    if (double.TryParse(dtResult.Rows[0]["PISSTOTAL_RIGHT"].ToString(),out dblTemp))
                    {
                        p_dblPissTotal = dblTemp;
                    }

                    if (double.TryParse(dtResult.Rows[0]["INTOTAL_RIGHT"].ToString(), out dblTemp))
                    {
                        p_dblCountIN = dblTemp;
                    }

                    if (double.TryParse(dtResult.Rows[0]["OUTTOTAL_RIGHT"].ToString(),out dblTemp))
                    {
                        p_dblCountOUT = dblTemp;
                    }
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
        /// 新添24小时统计
        /// </summary>
        /// <param name="p_objRecord">统计内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddStatistics(clsICUNurseStatisticsValue p_objRecord)
        {
            if (p_objRecord == null)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("seq_emr_sign", out lngSequence);

                string strSQL = @"insert into t_emr_icunursestatistics (registerid_vchr,createdate_dat,modifydate,status_int,afteropdays,transferid_chr,
liquid1,liquid1xml,liquid2,liquid2xml,liquid3,liquid3xml,liquid4,liquid4xml,liquid5,liquid5xml,fbool,fboolxml,plasma,plasmaxml,
nose1,nose1xml,nose2,nose2xml,intotal,intotalxml,stool,stoolxml,piss,pissxml,drain1,drain1xml,drain2,drain2xml,drain3,drain3xml,
drain4,drain4xml,drain5,drain5xml,outtotal,outtotalxml,markstatus,liquid1_right,liquid2_right,liquid3_right,liquid4_right,
liquid5_right,fbool_right,plasma_right,nose1_right,nose2_right,intotal_right,stool_right,piss_right,drain1_right,drain2_right,
drain3_right,drain4_right,drain5_right,outtotal_right,sequence_int,statisticsstart_dat,statisticsend_dat,createuserid_vchr) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(65, out objDPArr);
                objDPArr[0].Value = p_objRecord.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecord.m_dtmCreateDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecord.m_dtmModifyDate ;
                objDPArr[3].Value = 1;
                objDPArr[4].Value = p_objRecord.m_strAFTEROPDAYS;
                if (string.IsNullOrEmpty(p_objRecord.m_strTRANSFERID_CHR))
                {
                    objDPArr[5].Value = "000000000000";
                }
                else
                {
                    objDPArr[5].Value = p_objRecord.m_strTRANSFERID_CHR;
                }
                objDPArr[6].Value = p_objRecord.m_strLIQUID1;
                objDPArr[7].Value = p_objRecord.m_strLIQUID1XML;
                objDPArr[8].Value = p_objRecord.m_strLIQUID2;
                objDPArr[9].Value = p_objRecord.m_strLIQUID2XML;
                objDPArr[10].Value = p_objRecord.m_strLIQUID3;
                objDPArr[11].Value = p_objRecord.m_strLIQUID3XML;
                objDPArr[12].Value = p_objRecord.m_strLIQUID4;
                objDPArr[13].Value = p_objRecord.m_strLIQUID4XML;
                objDPArr[14].Value = p_objRecord.m_strLIQUID5;
                objDPArr[15].Value = p_objRecord.m_strLIQUID5XML;
                objDPArr[16].Value = p_objRecord.m_strFBOOL;
                objDPArr[17].Value = p_objRecord.m_strFBOOLXML;
                objDPArr[18].Value = p_objRecord.m_strPLASMA;
                objDPArr[19].Value = p_objRecord.m_strPLASMAXML;
                objDPArr[20].Value = p_objRecord.m_strNOSE1;
                objDPArr[21].Value = p_objRecord.m_strNOSE1XML;
                objDPArr[22].Value = p_objRecord.m_strNOSE2;
                objDPArr[23].Value = p_objRecord.m_strNOSE2XML;
                objDPArr[24].Value = p_objRecord.m_strINTOTAL;
                objDPArr[25].Value = p_objRecord.m_strINTOTALXML;
                objDPArr[26].Value = p_objRecord.m_strSTOOL;
                objDPArr[27].Value = p_objRecord.m_strSTOOLXML;
                objDPArr[28].Value = p_objRecord.m_strPISS;
                objDPArr[29].Value = p_objRecord.m_strPISSXML;
                objDPArr[30].Value = p_objRecord.m_strDRAIN1;
                objDPArr[31].Value = p_objRecord.m_strDRAIN1XML;
                objDPArr[32].Value = p_objRecord.m_strDRAIN2;
                objDPArr[33].Value = p_objRecord.m_strDRAIN2XML;
                objDPArr[34].Value = p_objRecord.m_strDRAIN3;
                objDPArr[35].Value = p_objRecord.m_strDRAIN3XML;
                objDPArr[36].Value = p_objRecord.m_strDRAIN4;
                objDPArr[37].Value = p_objRecord.m_strDRAIN4XML;
                objDPArr[38].Value = p_objRecord.m_strDRAIN5;
                objDPArr[39].Value = p_objRecord.m_strDRAIN5XML;
                objDPArr[40].Value = p_objRecord.m_strOUTTOTAL;
                objDPArr[41].Value = p_objRecord.m_strOUTTOTALXML;
                objDPArr[42].Value = p_objRecord.m_intMarkStatus;
                objDPArr[43].Value = p_objRecord.m_strLIQUID1_RIGHT;
                objDPArr[44].Value = p_objRecord.m_strLIQUID2_RIGHT;
                objDPArr[45].Value = p_objRecord.m_strLIQUID3_RIGHT;
                objDPArr[46].Value = p_objRecord.m_strLIQUID4_RIGHT;
                objDPArr[47].Value = p_objRecord.m_strLIQUID5_RIGHT;
                objDPArr[48].Value = p_objRecord.m_strFBOOL_RIGHT;
                objDPArr[49].Value = p_objRecord.m_strPLASMA_RIGHT;
                objDPArr[50].Value = p_objRecord.m_strNOSE1_RIGHT;
                objDPArr[51].Value = p_objRecord.m_strNOSE2_RIGHT;
                objDPArr[52].Value = p_objRecord.m_strINTOTAL_RIGHT;
                objDPArr[53].Value = p_objRecord.m_strSTOOL_RIGHT;
                objDPArr[54].Value = p_objRecord.m_strPISS_RIGHT;
                objDPArr[55].Value = p_objRecord.m_strDRAIN1_RIGHT;
                objDPArr[56].Value = p_objRecord.m_strDRAIN2_RIGHT;
                objDPArr[57].Value = p_objRecord.m_strDRAIN3_RIGHT;
                objDPArr[58].Value = p_objRecord.m_strDRAIN4_RIGHT;
                objDPArr[59].Value = p_objRecord.m_strDRAIN5_RIGHT;
                objDPArr[60].Value = p_objRecord.m_strOUTTOTAL_RIGHT;
                objDPArr[61].Value = lngSequence;
                objDPArr[62].DbType = DbType.DateTime;
                objDPArr[62].Value = p_objRecord.m_dtmSTATISTICSSTART_DAT;
                objDPArr[63].DbType = DbType.DateTime;
                objDPArr[63].Value = p_objRecord.m_dtmSTATISTICSEND_DAT;
                objDPArr[64].Value = p_objRecord.m_strCreateUserID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                lngRes = objSign.m_lngAddSign(p_objRecord.objSignerArr, lngSequence);
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
        /// 修改24小时统计
        /// </summary>
        /// <param name="p_objRecord">统计内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStatistics(clsICUNurseStatisticsValue p_objRecord)
        {
            if (p_objRecord == null)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("seq_emr_sign", out lngSequence);

                string strSQL = @"update t_emr_icunursestatistics set modifydate = ?,afteropdays = ?,transferid_chr = ?,
liquid1 = ?,liquid1xml = ?,liquid2 = ?,liquid2xml = ?,liquid3 = ?,liquid3xml = ?,liquid4 = ?,liquid4xml = ?,liquid5 = ?,liquid5xml = ?,fbool = ?,fboolxml = ?,plasma = ?,plasmaxml = ?,
nose1 = ?,nose1xml = ?,nose2 = ?,nose2xml = ?,intotal = ?,intotalxml = ?,stool = ?,stoolxml = ?,piss = ?,pissxml = ?,drain1 = ?,drain1xml = ?,drain2 = ?,drain2xml = ?,drain3 = ?,drain3xml = ?,
drain4 = ?,drain4xml = ?,drain5 = ?,drain5xml = ?,outtotal = ?,outtotalxml = ?,markstatus = ?,liquid1_right = ?,liquid2_right = ?,liquid3_right = ?,liquid4_right = ?,
liquid5_right = ?,fbool_right = ?,plasma_right = ?,nose1_right = ?,nose2_right = ?,intotal_right = ?,stool_right = ?,piss_right = ?,drain1_right = ?,drain2_right = ?,
drain3_right = ?,drain4_right = ?,drain5_right = ?,outtotal_right = ?,sequence_int = ?,statisticsstart_dat = ?,statisticsend_dat = ?
 where registerid_vchr = ? and createdate_dat = ? and status_int = 1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(63, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecord.m_dtmModifyDate;
                objDPArr[1].Value = p_objRecord.m_strAFTEROPDAYS;
                if (string.IsNullOrEmpty(p_objRecord.m_strTRANSFERID_CHR))
                {
                    objDPArr[2].Value = "000000000000";
                }
                else
                {
                    objDPArr[2].Value = p_objRecord.m_strTRANSFERID_CHR;
                }
                objDPArr[3].Value = p_objRecord.m_strLIQUID1;
                objDPArr[4].Value = p_objRecord.m_strLIQUID1XML;
                objDPArr[5].Value = p_objRecord.m_strLIQUID2;
                objDPArr[6].Value = p_objRecord.m_strLIQUID2XML;
                objDPArr[7].Value = p_objRecord.m_strLIQUID3;
                objDPArr[8].Value = p_objRecord.m_strLIQUID3XML;
                objDPArr[9].Value = p_objRecord.m_strLIQUID4;
                objDPArr[10].Value = p_objRecord.m_strLIQUID4XML;
                objDPArr[11].Value = p_objRecord.m_strLIQUID5;
                objDPArr[12].Value = p_objRecord.m_strLIQUID5XML;
                objDPArr[13].Value = p_objRecord.m_strFBOOL;
                objDPArr[14].Value = p_objRecord.m_strFBOOLXML;
                objDPArr[15].Value = p_objRecord.m_strPLASMA;
                objDPArr[16].Value = p_objRecord.m_strPLASMAXML;
                objDPArr[17].Value = p_objRecord.m_strNOSE1;
                objDPArr[18].Value = p_objRecord.m_strNOSE1XML;
                objDPArr[19].Value = p_objRecord.m_strNOSE2;
                objDPArr[20].Value = p_objRecord.m_strNOSE2XML;
                objDPArr[21].Value = p_objRecord.m_strINTOTAL;
                objDPArr[22].Value = p_objRecord.m_strINTOTALXML;
                objDPArr[23].Value = p_objRecord.m_strSTOOL;
                objDPArr[24].Value = p_objRecord.m_strSTOOLXML;
                objDPArr[25].Value = p_objRecord.m_strPISS;
                objDPArr[26].Value = p_objRecord.m_strPISSXML;
                objDPArr[27].Value = p_objRecord.m_strDRAIN1;
                objDPArr[28].Value = p_objRecord.m_strDRAIN1XML;
                objDPArr[29].Value = p_objRecord.m_strDRAIN2;
                objDPArr[30].Value = p_objRecord.m_strDRAIN2XML;
                objDPArr[31].Value = p_objRecord.m_strDRAIN3;
                objDPArr[32].Value = p_objRecord.m_strDRAIN3XML;
                objDPArr[33].Value = p_objRecord.m_strDRAIN4;
                objDPArr[34].Value = p_objRecord.m_strDRAIN4XML;
                objDPArr[35].Value = p_objRecord.m_strDRAIN5;
                objDPArr[36].Value = p_objRecord.m_strDRAIN5XML;
                objDPArr[37].Value = p_objRecord.m_strOUTTOTAL;
                objDPArr[38].Value = p_objRecord.m_strOUTTOTALXML;
                objDPArr[39].Value = p_objRecord.m_intMarkStatus;
                objDPArr[40].Value = p_objRecord.m_strLIQUID1_RIGHT;
                objDPArr[41].Value = p_objRecord.m_strLIQUID2_RIGHT;
                objDPArr[42].Value = p_objRecord.m_strLIQUID3_RIGHT;
                objDPArr[43].Value = p_objRecord.m_strLIQUID4_RIGHT;
                objDPArr[44].Value = p_objRecord.m_strLIQUID5_RIGHT;
                objDPArr[45].Value = p_objRecord.m_strFBOOL_RIGHT;
                objDPArr[46].Value = p_objRecord.m_strPLASMA_RIGHT;
                objDPArr[47].Value = p_objRecord.m_strNOSE1_RIGHT;
                objDPArr[48].Value = p_objRecord.m_strNOSE2_RIGHT;
                objDPArr[49].Value = p_objRecord.m_strINTOTAL_RIGHT;
                objDPArr[50].Value = p_objRecord.m_strSTOOL_RIGHT;
                objDPArr[51].Value = p_objRecord.m_strPISS_RIGHT;
                objDPArr[52].Value = p_objRecord.m_strDRAIN1_RIGHT;
                objDPArr[53].Value = p_objRecord.m_strDRAIN2_RIGHT;
                objDPArr[54].Value = p_objRecord.m_strDRAIN3_RIGHT;
                objDPArr[55].Value = p_objRecord.m_strDRAIN4_RIGHT;
                objDPArr[56].Value = p_objRecord.m_strDRAIN5_RIGHT;
                objDPArr[57].Value = p_objRecord.m_strOUTTOTAL_RIGHT;
                objDPArr[58].Value = lngSequence;
                objDPArr[59].DbType = DbType.DateTime;
                objDPArr[59].Value = p_objRecord.m_dtmSTATISTICSSTART_DAT;
                objDPArr[60].DbType = DbType.DateTime;
                objDPArr[60].Value = p_objRecord.m_dtmSTATISTICSEND_DAT;
                objDPArr[61].Value = p_objRecord.m_strRegisterID;
                objDPArr[62].DbType = DbType.DateTime;
                objDPArr[62].Value = p_objRecord.m_dtmCreateDate;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                lngRes = objSign.m_lngAddSign(p_objRecord.objSignerArr, lngSequence);
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
        /// 删除统计内容
        /// </summary>
        /// <param name="p_objRecord">统计内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteStatistics(clsICUNurseStatisticsValue p_objRecord)
        {
            if (p_objRecord == null)
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                string strSQL = @"update t_emr_icunursestatistics t
   set t.deactiveddate_dat = ? , t.deactivedoperatorid_vchr = ? ,t.status_int = 0
 where t.registerid_vchr = ?
   and t.createdate_dat = ? 
   and t.transferid_chr = ? 
   and t.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecord.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecord.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecord.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecord.m_dtmCreateDate;
                if (string.IsNullOrEmpty(p_objRecord.m_strTRANSFERID_CHR))
                {
                    objDPArr[4].Value = "000000000000";
                }
                else
                {
                    objDPArr[4].Value = p_objRecord.m_strTRANSFERID_CHR;
                }

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region 获取24小时统计内容
        /// <summary>
        /// 获取24小时统计内容
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtmCreateDate">创建日期</param>
        /// <param name="p_objContent">返回内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStatisticsValue(string p_strRegisterID, DateTime p_dtmCreateDate, out clsICUNurseStatisticsValue p_objContent)
        {
            p_objContent = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return (long)enmOperationResult.Parameter_Error;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select registerid_vchr,
       createdate_dat,
       modifydate,
       status_int,
       afteropdays,
       transferid_chr,
       liquid1,
       liquid1xml,
       liquid2,
       liquid2xml,
       liquid3,
       liquid3xml,
       liquid4,
       liquid4xml,
       liquid5,
       liquid5xml,
       fbool,
       fboolxml,
       plasma,
       plasmaxml,
       nose1,
       nose1xml,
       nose2,
       nose2xml,
       intotal,
       intotalxml,
       stool,
       stoolxml,
       piss,
       pissxml,
       drain1,
       drain1xml,
       drain2,
       drain2xml,
       drain3,
       drain3xml,
       drain4,
       drain4xml,
       drain5,
       drain5xml,
       outtotal,
       outtotalxml,
       markstatus,
       liquid1_right,
       liquid2_right,
       liquid3_right,
       liquid4_right,
       liquid5_right,
       fbool_right,
       plasma_right,
       nose1_right,
       nose2_right,
       intotal_right,
       stool_right,
       piss_right,
       drain1_right,
       drain2_right,
       drain3_right,
       drain4_right,
       drain5_right,
       outtotal_right,
       deactiveddate_dat,
       deactivedoperatorid_vchr,
       sequence_int,
       statisticsstart_dat,
       statisticsend_dat,
       createuserid_vchr
  from t_emr_icunursestatistics t
 where t.registerid_vchr = ?
   and t.createdate_dat = ? and t.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmCreateDate;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    int intRowsCount = dtbResult.Rows.Count;

                    if (intRowsCount != 1)
                    {
                        return -1;
                    }

                    p_objContent = new clsICUNurseStatisticsValue();
                    p_objContent.m_strRegisterID = dtbResult.Rows[0]["REGISTERID_VCHR"].ToString();
                    p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]);
                    p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbResult.Rows[0]["MODIFYDATE"]);
                    p_objContent.m_bytStatus = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"]);
                    p_objContent.m_strAFTEROPDAYS = dtbResult.Rows[0]["AFTEROPDAYS"].ToString();
                    p_objContent.m_strTRANSFERID_CHR = dtbResult.Rows[0]["TRANSFERID_CHR"].ToString();
                    p_objContent.m_strLIQUID1 = dtbResult.Rows[0]["LIQUID1"].ToString();
                    p_objContent.m_strLIQUID1XML = dtbResult.Rows[0]["LIQUID1XML"].ToString();
                    p_objContent.m_strLIQUID2 = dtbResult.Rows[0]["LIQUID2"].ToString();
                    p_objContent.m_strLIQUID2XML = dtbResult.Rows[0]["LIQUID2XML"].ToString();
                    p_objContent.m_strLIQUID3 = dtbResult.Rows[0]["LIQUID3"].ToString();
                    p_objContent.m_strLIQUID3XML = dtbResult.Rows[0]["LIQUID3XML"].ToString();
                    p_objContent.m_strLIQUID4 = dtbResult.Rows[0]["LIQUID4"].ToString();
                    p_objContent.m_strLIQUID4XML = dtbResult.Rows[0]["LIQUID4XML"].ToString();
                    p_objContent.m_strLIQUID5 = dtbResult.Rows[0]["LIQUID5"].ToString();
                    p_objContent.m_strLIQUID5XML = dtbResult.Rows[0]["LIQUID5XML"].ToString();
                    p_objContent.m_strFBOOL = dtbResult.Rows[0]["FBOOL"].ToString();
                    p_objContent.m_strFBOOLXML = dtbResult.Rows[0]["FBOOLXML"].ToString();
                    p_objContent.m_strPLASMA = dtbResult.Rows[0]["PLASMA"].ToString();
                    p_objContent.m_strPLASMAXML = dtbResult.Rows[0]["PLASMAXML"].ToString();
                    p_objContent.m_strNOSE1 = dtbResult.Rows[0]["NOSE1"].ToString();
                    p_objContent.m_strNOSE1XML = dtbResult.Rows[0]["NOSE1XML"].ToString();
                    p_objContent.m_strNOSE2 = dtbResult.Rows[0]["NOSE2"].ToString();
                    p_objContent.m_strNOSE2XML = dtbResult.Rows[0]["NOSE2XML"].ToString();
                    p_objContent.m_strINTOTAL = dtbResult.Rows[0]["INTOTAL"].ToString();
                    p_objContent.m_strINTOTALXML = dtbResult.Rows[0]["INTOTALXML"].ToString();
                    p_objContent.m_strSTOOL = dtbResult.Rows[0]["STOOL"].ToString();
                    p_objContent.m_strSTOOLXML = dtbResult.Rows[0]["STOOLXML"].ToString();
                    p_objContent.m_strPISS = dtbResult.Rows[0]["PISS"].ToString();
                    p_objContent.m_strPISSXML = dtbResult.Rows[0]["PISSXML"].ToString();
                    p_objContent.m_strDRAIN1 = dtbResult.Rows[0]["DRAIN1"].ToString();
                    p_objContent.m_strDRAIN1XML = dtbResult.Rows[0]["DRAIN1XML"].ToString();
                    p_objContent.m_strDRAIN2 = dtbResult.Rows[0]["DRAIN2"].ToString();
                    p_objContent.m_strDRAIN2XML = dtbResult.Rows[0]["DRAIN2XML"].ToString();
                    p_objContent.m_strDRAIN3 = dtbResult.Rows[0]["DRAIN3"].ToString();
                    p_objContent.m_strDRAIN3XML = dtbResult.Rows[0]["DRAIN3XML"].ToString();
                    p_objContent.m_strDRAIN4 = dtbResult.Rows[0]["DRAIN4"].ToString();
                    p_objContent.m_strDRAIN4XML = dtbResult.Rows[0]["DRAIN4XML"].ToString();
                    p_objContent.m_strDRAIN5 = dtbResult.Rows[0]["DRAIN5"].ToString();
                    p_objContent.m_strDRAIN5XML = dtbResult.Rows[0]["DRAIN5XML"].ToString();
                    p_objContent.m_strOUTTOTAL = dtbResult.Rows[0]["OUTTOTAL"].ToString();
                    p_objContent.m_strOUTTOTALXML = dtbResult.Rows[0]["OUTTOTALXML"].ToString();
                    p_objContent.m_intMarkStatus = Convert.ToInt32(dtbResult.Rows[0]["MARKSTATUS"]);
                    p_objContent.m_strLIQUID1_RIGHT = dtbResult.Rows[0]["LIQUID1_RIGHT"].ToString();
                    p_objContent.m_strLIQUID2_RIGHT = dtbResult.Rows[0]["LIQUID2_RIGHT"].ToString();
                    p_objContent.m_strLIQUID3_RIGHT = dtbResult.Rows[0]["LIQUID3_RIGHT"].ToString();
                    p_objContent.m_strLIQUID4_RIGHT = dtbResult.Rows[0]["LIQUID4_RIGHT"].ToString();
                    p_objContent.m_strLIQUID5_RIGHT = dtbResult.Rows[0]["LIQUID5_RIGHT"].ToString();
                    p_objContent.m_strFBOOL_RIGHT = dtbResult.Rows[0]["FBOOL_RIGHT"].ToString();
                    p_objContent.m_strPLASMA_RIGHT = dtbResult.Rows[0]["PLASMA_RIGHT"].ToString();
                    p_objContent.m_strNOSE1_RIGHT = dtbResult.Rows[0]["NOSE1_RIGHT"].ToString();
                    p_objContent.m_strNOSE2_RIGHT = dtbResult.Rows[0]["NOSE2_RIGHT"].ToString();
                    p_objContent.m_strINTOTAL_RIGHT = dtbResult.Rows[0]["INTOTAL_RIGHT"].ToString();
                    p_objContent.m_strSTOOL_RIGHT = dtbResult.Rows[0]["STOOL_RIGHT"].ToString();
                    p_objContent.m_strPISS_RIGHT = dtbResult.Rows[0]["PISS_RIGHT"].ToString();
                    p_objContent.m_strDRAIN1_RIGHT = dtbResult.Rows[0]["DRAIN1_RIGHT"].ToString();
                    p_objContent.m_strDRAIN2_RIGHT = dtbResult.Rows[0]["DRAIN2_RIGHT"].ToString();
                    p_objContent.m_strDRAIN3_RIGHT = dtbResult.Rows[0]["DRAIN3_RIGHT"].ToString();
                    p_objContent.m_strDRAIN4_RIGHT = dtbResult.Rows[0]["DRAIN4_RIGHT"].ToString();
                    p_objContent.m_strDRAIN5_RIGHT = dtbResult.Rows[0]["DRAIN5_RIGHT"].ToString();
                    p_objContent.m_strOUTTOTAL_RIGHT = dtbResult.Rows[0]["OUTTOTAL_RIGHT"].ToString();
                    DateTime dtmTemp = DateTime.MinValue;
                    if (DateTime.TryParse(dtbResult.Rows[0]["DEACTIVEDDATE_DAT"].ToString(), out dtmTemp))
                    {
                        p_objContent.m_dtmDeActivedDate = dtmTemp;
                    }
                    else
                    {
                        p_objContent.m_dtmDeActivedDate = DateTime.MinValue;
                    }
                    p_objContent.m_strDeActivedOperatorID = dtbResult.Rows[0]["DEACTIVEDOPERATORID_VCHR"].ToString();
                    if (DateTime.TryParse(dtbResult.Rows[0]["STATISTICSSTART_DAT"].ToString(),out dtmTemp))
                    {
                        p_objContent.m_dtmSTATISTICSSTART_DAT = dtmTemp;
                    }
                    else
                    {
                        p_objContent.m_dtmSTATISTICSSTART_DAT = DateTime.Now;
                    }
                    if (DateTime.TryParse(dtbResult.Rows[0]["STATISTICSEND_DAT"].ToString(), out dtmTemp))
                    {
                        p_objContent.m_dtmSTATISTICSEND_DAT = dtmTemp;
                    }
                    else
                    {
                        p_objContent.m_dtmSTATISTICSEND_DAT = DateTime.Now;
                    }

                    //获取签名集合
                    if (dtbResult.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbResult.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

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
            return lngRes;
        } 
        #endregion

        /// <summary>
        /// 获取一段时间内的出入量
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strTransferID">转科流水号</param>
        /// <param name="p_dtmStart">统计开始时间</param>
        /// <param name="p_dtmEnd">统计结束时间</param>
        /// <param name="p_objValueArr">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStasticsInSpecifyTime(string p_strRegisterID, string p_strTransferID, DateTime p_dtmStart, DateTime p_dtmEnd, out clsICUNurseStatisticsValue[] p_objValueArr)
        {
            p_objValueArr = null;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return (long)enmOperationResult.Parameter_Error;
            }
            if (string.IsNullOrEmpty(p_strTransferID))
            {
                p_strTransferID = "000000000000";
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select d.liquid1_right,
       d.liquid2_right,
       d.liquid3_right,
       d.liquid4_right,
       d.liquid5_right,
       d.fbool_right,
       d.plasma_right,
       d.nose1_right,
       d.nose2_right,
       d.stool_right,
       d.piss_right,
       d.drain1_right,
       d.drain2_right,
       d.drain3_right,
       d.drain4_right,
       d.drain5_right
  from t_emr_icunurse t
 inner join t_emr_icunursecontent d on t.registerid_vchr =
                                       d.registerid_vchr
                                   and t.createdate_dat = d.createdate_dat
 where t.registerid_vchr = ?
   and t.transferid_chr = ?
   and t.recorddate_dat >= ?
   and t.recorddate_dat < ?
   and t.status_int = 1
   and d.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].Value = p_strTransferID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmStart;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmEnd;

                DataTable dtbResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0)
                {
                    int intRowsCount = dtbResult.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        p_objValueArr = new clsICUNurseStatisticsValue[intRowsCount];
                        DataRow drTemp = null;
                        for (int rowIndex = 0; rowIndex < intRowsCount; rowIndex++)
                        {
                            drTemp = dtbResult.Rows[rowIndex];
                            p_objValueArr[rowIndex] = new clsICUNurseStatisticsValue();
                            p_objValueArr[rowIndex].m_strLIQUID1_RIGHT = drTemp["liquid1_right"].ToString();
                            p_objValueArr[rowIndex].m_strLIQUID2_RIGHT = drTemp["liquid2_right"].ToString();
                            p_objValueArr[rowIndex].m_strLIQUID3_RIGHT = drTemp["liquid3_right"].ToString();
                            p_objValueArr[rowIndex].m_strLIQUID4_RIGHT = drTemp["liquid4_right"].ToString();
                            p_objValueArr[rowIndex].m_strLIQUID5_RIGHT = drTemp["liquid5_right"].ToString();
                            p_objValueArr[rowIndex].m_strFBOOL_RIGHT = drTemp["fbool_right"].ToString();
                            p_objValueArr[rowIndex].m_strPLASMA_RIGHT = drTemp["plasma_right"].ToString();
                            p_objValueArr[rowIndex].m_strNOSE1_RIGHT = drTemp["nose1_right"].ToString();
                            p_objValueArr[rowIndex].m_strNOSE2_RIGHT = drTemp["nose2_right"].ToString();
                            p_objValueArr[rowIndex].m_strSTOOL_RIGHT = drTemp["stool_right"].ToString();
                            p_objValueArr[rowIndex].m_strPISS_RIGHT = drTemp["piss_right"].ToString();
                            p_objValueArr[rowIndex].m_strDRAIN1_RIGHT = drTemp["drain1_right"].ToString();
                            p_objValueArr[rowIndex].m_strDRAIN2_RIGHT = drTemp["drain2_right"].ToString();
                            p_objValueArr[rowIndex].m_strDRAIN3_RIGHT = drTemp["drain3_right"].ToString();
                            p_objValueArr[rowIndex].m_strDRAIN4_RIGHT = drTemp["drain4_right"].ToString();
                            p_objValueArr[rowIndex].m_strDRAIN5_RIGHT = drTemp["drain5_right"].ToString();
                        }
                    }
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
    }
}
