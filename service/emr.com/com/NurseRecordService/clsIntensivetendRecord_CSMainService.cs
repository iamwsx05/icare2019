using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.Utility;
using System.Windows.Forms;
using System.Collections.Generic;

namespace com.digitalwave.clsRecordsService
{
    /// <summary>
    /// 新生儿科患者护理记录(茶山)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsIntensivetendRecord_CSMainService : clsRecordsService
    {
        public clsIntensivetendRecord_CSMainService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
       } 
         #region SQL语句
        private const string c_strUpdateFirstPrintDateSQL = @"update intensivetendrecord_cs
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
                                                               t1.boxtemperature,
                                                               t1.boxtemperaturexml,
                                                               t1.temperature,
                                                               t1.temperaturexml,
                                                               t1.heartrate,
                                                               t1.heartratexml,
                                                               t1.respiration,
                                                               t1.respirationxml,
                                                               t1.bloodpress,
                                                               t1.bloodpressxml,
                                                               t1.spo2,
                                                               t1.spo2xml,
                                                               t1.mind,
                                                               t1.pupil_sizeleft,
                                                               t1.pupil_sizeleftxml,
                                                               t1.pupil_sizeright,
                                                               t1.pupil_sizerightxml,
                                                               t1.pupil_reflectleft,
                                                               t1.pupil_reflectright,
                                                               t1.fontanel,
                                                               t1.facecolor,
                                                               t1.skincolor,
                                                               t1.skinedema,
                                                               t1.skinelasticity,
                                                               t1.skinpattern,
                                                               t1.skinedemaposition,
                                                               t1.skinedemaproperty,
                                                               t1.sequence_int,
                                                               t1.recorddate,
                                                               t2.modifydate,
                                                               t2.modifyuserid,
                                                               t2.boxtemperatureright,
                                                               t2.temperatureright,
                                                               t2.heartrateright,
                                                               t2.respirationright,
                                                               t2.bloodpressright,
                                                               t2.mindright,
                                                               t2.pupil_sizeleftright,
                                                               t2.pupil_sizerightright,
                                                               t2.pupil_reflectleftright,
                                                               t2.pupil_reflectrightright,
                                                               t2.fontanelright,
                                                               t2.facecolorright,
                                                               t2.skincolorright,
                                                               t2.skinedemaright,
                                                               t2.skinelasticityright,
                                                               t2.skinpatternright,
                                                               t2.skindemapositionright,
                                                               t2.skindemapropertyright,
                                                               t2.spo2right
														from intensivetendrecord_cs t1,
															 intensivetendrecord_cscontent t2
														where t1.inpatientid = t2.inpatientid
														        and t1.inpatientdate = t2.inpatientdate
														        and t1.opendate = t2.opendate
														        and t1.status = 0
														        and t1.inpatientid = ?
														        and t1.inpatientdate = ?
														        order by t1.recorddate,t2.modifydate";

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
                                                               t1.boxtemperature,
                                                               t1.boxtemperaturexml,
                                                               t1.temperature,
                                                               t1.temperaturexml,
                                                               t1.heartrate,
                                                               t1.heartratexml,
                                                               t1.respiration,
                                                               t1.respirationxml,
                                                               t1.bloodpress,
                                                               t1.bloodpressxml,
                                                               t1.spo2,
                                                               t1.spo2xml,
                                                               t1.mind,
                                                               t1.pupil_sizeleft,
                                                               t1.pupil_sizeleftxml,
                                                               t1.pupil_sizeright,
                                                               t1.pupil_sizerightxml,
                                                               t1.pupil_reflectleft,
                                                               t1.pupil_reflectright,
                                                               t1.fontanel,
                                                               t1.facecolor,
                                                               t1.skincolor,
                                                               t1.skinedema,
                                                               t1.skinelasticity,
                                                               t1.skinpattern,
                                                               t1.skinedemaposition,
                                                               t1.skinedemaproperty,
                                                               t1.sequence_int,
                                                               t1.recorddate,
                                                               t2.modifydate,
                                                               t2.modifyuserid,
                                                               t2.boxtemperatureright,
                                                               t2.temperatureright,
                                                               t2.heartrateright,
                                                               t2.respirationright,
                                                               t2.bloodpressright,
                                                               t2.mindright,
                                                               t2.pupil_sizeleftright,
                                                               t2.pupil_sizerightright,
                                                               t2.pupil_reflectleftright,
                                                               t2.pupil_reflectrightright,
                                                               t2.fontanelright,
                                                               t2.facecolorright,
                                                               t2.skincolorright,
                                                               t2.skinedemaright,
                                                               t2.skinelasticityright,
                                                               t2.skinpatternright,
                                                               t2.skindemapositionright,
                                                               t2.skindemapropertyright,
                                                               t2.spo2right
														from intensivetendrecord_cs t1,
															 intensivetendrecord_cscontent t2
														where t1.inpatientid = t2.inpatientid
														and t1.inpatientdate = t2.inpatientdate
														and t1.opendate = t2.opendate
														and t1.status = 0
														and t1.inpatientid = ?
														and t1.inpatientdate = ?
														and t1.opendate = ?
														order by t1.recorddate,t2.modifydate";
        private const string c_strGetInpectInfoSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       oralseq,
       modifyuserid,
       incept_kind,
       incept_mete
  from nurserecordinceptinfo where inpatientid = ? and inpatientdate = ? and formid = ? and status =1";
        private const string c_strGetEductionInfoSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       oralseq,
       modifyuserid,
       eduction_kind,
       eduction_mete,
       eduction_color
  from nurserecordeductioninfo where inpatientid = ? and inpatientdate = ? and formid = ? and status = 1";
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from intensivetendrecord_cs
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        private const string c_strDeleteRecordSQL = @"update intensivetendrecord_cs
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

        private const string c_strGetDetailSQL = @"select inpatientid,
                                                           inpatientdate,
                                                           opendate,
                                                           createdate,
                                                           createuserid,
                                                           modifyuserid,
                                                           recordcontent,
                                                           recordcontentxml,
                                                           recordcontent_right,
                                                           status,
                                                           recorddate,
                                                           modifydate,
                                                           deactiveddate,
                                                           deactivedoperatorid,
                                                           sequence_int,
                                                           f_getempnamebyno(t.createuserid) as lastname_vchr
                                                      from intensivetendrecord_csdetail t
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
            out clsIntensivetendRecordContent_CS[] p_objTansDataInfo)
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
                    p_objTansDataInfo = new clsIntensivetendRecordContent_CS[dtbValue.Rows.Count];
                    clsIntensivetendRecordContent_CS objRecordContent = null;
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date == dtmOpenDate)
                        {
                            #region 从DataTable.Rows中获取结果
                            objRecordContent = new clsIntensivetendRecordContent_CS();
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
                            //箱温
                            objRecordContent.m_strBOXTEMPERATURE_RIGHT = dtbValue.Rows[j]["BOXTEMPERATURERIGHT"].ToString();
                            objRecordContent.m_strBOXTEMPERATUREALL = dtbValue.Rows[j]["BOXTEMPERATURE"].ToString();
                            objRecordContent.m_strBOXTEMPERATUREXML = dtbValue.Rows[j]["BOXTEMPERATUREXML"].ToString();
                            //体温
                            objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[j]["TEMPERATURERIGHT"].ToString();
                            objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[j]["TEMPERATURE"].ToString();
                            objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
                            //心率
                            objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[j]["HEARTRATERIGHT"].ToString();
                            objRecordContent.m_strHEARTRATE = dtbValue.Rows[j]["HEARTRATE"].ToString();
                            objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[j]["HEARTRATEXML"].ToString();
                            //呼吸
                            objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[j]["RESPIRATIONRIGHT"].ToString();
                            objRecordContent.m_strRESPIRATION = dtbValue.Rows[j]["RESPIRATION"].ToString();
                            objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[j]["RESPIRATIONXML"].ToString();
                            //血压
                            objRecordContent.m_strBLOODPRESSURES_RIGHT = dtbValue.Rows[j]["BLOODPRESSRIGHT"].ToString();
                            objRecordContent.m_strBLOODPRESSURES = dtbValue.Rows[j]["BLOODPRESS"].ToString();
                            objRecordContent.m_strBLOODPRESSURESXML = dtbValue.Rows[j]["BLOODPRESSXML"].ToString();
                            //spo2
                            objRecordContent.m_strSPO2_RIGHT = dtbValue.Rows[j]["SPO2RIGHT"].ToString();
                            objRecordContent.m_strSPO2 = dtbValue.Rows[j]["SPO2"].ToString();
                            objRecordContent.m_strSPO2XML = dtbValue.Rows[j]["SPO2XML"].ToString();
                            //神志
                            objRecordContent.m_strMind = dtbValue.Rows[j]["Mind"].ToString();
                            //瞳孔大小左
                            objRecordContent.m_strPupilSizeLeft_RIGHT = dtbValue.Rows[j]["PUPIL_SIZELEFTRIGHT"].ToString();
                            objRecordContent.m_strPupilSizeLeft = dtbValue.Rows[j]["PUPIL_SIZELEFT"].ToString();
                            objRecordContent.m_strPupilSizeLeftXML = dtbValue.Rows[j]["PUPIL_SIZELEFTXML"].ToString();
                            //瞳孔大小右
                            objRecordContent.m_strPupilSizeRight_RIGHT = dtbValue.Rows[j]["PUPIL_SIZERIGHTRIGHT"].ToString();
                            objRecordContent.m_strPupilSizeRight = dtbValue.Rows[j]["PUPIL_SIZERIGHT"].ToString();
                            objRecordContent.m_strPupilSizeRightXML = dtbValue.Rows[j]["PUPIL_SIZERIGHTXML"].ToString();
                            //瞳孔反射左
                            objRecordContent.m_strPupilReflectLeft = dtbValue.Rows[j]["PUPIL_REFLECTLEFTRIGHT"].ToString();
                            //瞳孔反射右
                            objRecordContent.m_strPupilReflectRight = dtbValue.Rows[j]["PUPIL_REFLECTRIGHTRIGHT"].ToString();
                            //囟门
                            objRecordContent.m_strFontanel = dtbValue.Rows[j]["FONTANELRIGHT"].ToString();
                            //面色
                            objRecordContent.m_strFaceColor = dtbValue.Rows[j]["FACECOLORRIGHT"].ToString();
                            //皮肤颜色
                            objRecordContent.m_strSkinColor = dtbValue.Rows[j]["SKINCOLORRIGHT"].ToString();
                            //皮肤硬肿
                            objRecordContent.m_strSkinEdema = dtbValue.Rows[j]["SKINEDEMARIGHT"].ToString();
                            //皮肤弹性
                            objRecordContent.m_strSkinLasticity = dtbValue.Rows[j]["SKINELASTICITYRIGHT"].ToString();
                            //皮肤花纹
                            objRecordContent.m_strSkinPattern = dtbValue.Rows[j]["SKINPATTERNRIGHT"].ToString();
                            //皮肤浮肿部位
                            objRecordContent.m_strSkinEdemaPosition = dtbValue.Rows[j]["SKINDEMAPOSITIONRIGHT"].ToString();
                            //皮肤浮肿性质
                            objRecordContent.m_strSkinEdemaProperty = dtbValue.Rows[j]["SKINDEMAPROPERTYRIGHT"].ToString();
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
            clsIntensivetendRecordContent_CS[] p_objGeneralNurseRecordArr = null;
            clsIntensivetendRecordContent_CSDetail[] p_objGeneralNurseDetailArr = null;
            p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
            long lngRes = -1;
            long lngInpectRes = 0;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsIntensivetendRecordContent_CSDataInfo objDataInfo = new clsIntensivetendRecordContent_CSDataInfo();
            try
            {
                IDataParameter[] objDPArr = null;
                IDataParameter[] objDPArr1 = null;
                IDataParameter[] objDPArr3 = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                #region 优化修改部分
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID.Trim();
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr1[2].Value = "frmIntensivetendRecord_CSRec";
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].Value = p_strInPatientID.Trim();
                objDPArr3[1].DbType = DbType.DateTime;
                objDPArr3[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr3[2].Value = "frmIntensivetendRecord_CSRec";
                #endregion 

                DataTable dtbInpectValue = new DataTable();//摄入信息
                DataTable dtbEductionValue = new DataTable();//排出信息
                DataTable dtbDetail = new DataTable();//病情记录内容
                DataTable dtbContent = new DataTable();//护理记录内容  
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    clsIntensivetendRecordContent_CS objRecordContent = null;

                    #region 优化修改部分
                    List<long> m_lis = new List<long>();
                    DataTable sing = new DataTable();
                    List<long> dinosaurs = new List<long>();
                    for (int mm = 0; mm < dtbContent.Rows.Count; mm++)
                    {
                        dinosaurs.Add(long.Parse(dtbContent.Rows[mm]["SEQUENCE_INT"].ToString()));

                    }
                    List<clsNurseRecordInpectInfo> objInpectList = new List<clsNurseRecordInpectInfo>();
                    List<clsNurseRecordEductionInfo> objEductionList = new List<clsNurseRecordEductionInfo>();

                    //签名一次查出加快速度。
                    long lngdd = m_lngGetSignOne(dinosaurs, p_strInPatientID.Trim(), p_strInPatientDate, "intensivetendrecord_cs", out sing);

                    lngInpectRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetInpectInfoSQL, ref dtbInpectValue, objDPArr1);

                    lngInpectRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetEductionInfoSQL, ref dtbEductionValue, objDPArr3);
                    #endregion 
                    DataRow dataRow = null;
                    string strReturnValue = string.Empty;
                    bool blnIsShowLevel = true;
                    if (strReturnValue == "1")
                    {
                        blnIsShowLevel = true;
                    }
                    else
                    {
                        blnIsShowLevel = false;
                    }
                    p_objGeneralNurseRecordArr = new clsIntensivetendRecordContent_CS[dtbContent.Rows.Count];
                    for (int i = 0; i < dtbContent.Rows.Count; i++)
                    {
                        dataRow = dtbContent.Rows[i];
                        objRecordContent = new clsIntensivetendRecordContent_CS();
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dataRow["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dataRow["MODIFYDATE"].ToString());
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dataRow["OpenDate"].ToString());
                        if (dataRow["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dataRow["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dataRow["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dataRow["MODIFYUSERID"].ToString();
                        objRecordContent.m_strContentCreateUserName = dataRow["CreateUserName"].ToString();
                        if (dataRow["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dataRow["IFCONFIRM"].ToString());
                        if (dataRow["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dataRow["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dataRow["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dataRow["CONFIRMREASONXML"].ToString();

                        //箱温
                        objRecordContent.m_strBOXTEMPERATURE_RIGHT = dataRow["BOXTEMPERATURERIGHT"].ToString();
                        objRecordContent.m_strBOXTEMPERATUREALL = dataRow["BOXTEMPERATURE"].ToString();
                        objRecordContent.m_strBOXTEMPERATUREXML = dataRow["BOXTEMPERATUREXML"].ToString();
                        //体温
                        objRecordContent.m_strTEMPERATURE_RIGHT = dataRow["TEMPERATURERIGHT"].ToString();
                        objRecordContent.m_strTEMPERATUREAll = dataRow["TEMPERATURE"].ToString();
                        objRecordContent.m_strTEMPERATUREXML = dataRow["TEMPERATUREXML"].ToString();
                        //心率
                        objRecordContent.m_strHEARTRATE_RIGHT = dataRow["HEARTRATERIGHT"].ToString();
                        objRecordContent.m_strHEARTRATE = dataRow["HEARTRATE"].ToString();
                        objRecordContent.m_strHEARTRATEXML = dataRow["HEARTRATEXML"].ToString();
                        //呼吸
                        objRecordContent.m_strRESPIRATION_RIGHT = dataRow["RESPIRATIONRIGHT"].ToString();
                        objRecordContent.m_strRESPIRATION = dataRow["RESPIRATION"].ToString();
                        objRecordContent.m_strRESPIRATIONXML = dataRow["RESPIRATIONXML"].ToString();
                        //血压
                        objRecordContent.m_strBLOODPRESSURES_RIGHT = dataRow["BLOODPRESSRIGHT"].ToString();
                        objRecordContent.m_strBLOODPRESSURES = dataRow["BLOODPRESS"].ToString();
                        objRecordContent.m_strBLOODPRESSURESXML = dataRow["BLOODPRESSXML"].ToString();
                        //spo2
                        objRecordContent.m_strSPO2_RIGHT = dataRow["SPO2RIGHT"].ToString();
                        objRecordContent.m_strSPO2 = dataRow["SPO2"].ToString();
                        objRecordContent.m_strSPO2XML = dataRow["SPO2XML"].ToString();
                        //神志
                        objRecordContent.m_strMind = dataRow["Mind"].ToString();
                        //瞳孔大小左
                        objRecordContent.m_strPupilSizeLeft_RIGHT = dataRow["PUPIL_SIZELEFTRIGHT"].ToString();
                        objRecordContent.m_strPupilSizeLeft = dataRow["PUPIL_SIZELEFT"].ToString();
                        objRecordContent.m_strPupilSizeLeftXML = dataRow["PUPIL_SIZELEFTXML"].ToString();
                        //瞳孔大小右
                        objRecordContent.m_strPupilSizeRight_RIGHT = dataRow["PUPIL_SIZERIGHTRIGHT"].ToString();
                        objRecordContent.m_strPupilSizeRight = dataRow["PUPIL_SIZERIGHT"].ToString();
                        objRecordContent.m_strPupilSizeRightXML = dataRow["PUPIL_SIZERIGHTXML"].ToString();
                        //瞳孔反射左
                        objRecordContent.m_strPupilReflectLeft = dataRow["PUPIL_REFLECTLEFTRIGHT"].ToString();
                        //瞳孔反射右
                        objRecordContent.m_strPupilReflectRight = dataRow["PUPIL_REFLECTRIGHTRIGHT"].ToString();
                        //囟门
                        objRecordContent.m_strFontanel = dataRow["FONTANELRIGHT"].ToString();
                        //面色
                        objRecordContent.m_strFaceColor = dataRow["FACECOLORRIGHT"].ToString();
                        //皮肤颜色
                        objRecordContent.m_strSkinColor = dataRow["SKINCOLORRIGHT"].ToString();
                        //皮肤硬肿
                        objRecordContent.m_strSkinEdema = dataRow["SKINEDEMARIGHT"].ToString();
                        //皮肤弹性
                        objRecordContent.m_strSkinLasticity = dataRow["SKINELASTICITYRIGHT"].ToString();
                        //皮肤花纹
                        objRecordContent.m_strSkinPattern = dataRow["SKINPATTERNRIGHT"].ToString();
                        //皮肤浮肿部位
                        objRecordContent.m_strSkinEdemaPosition = dataRow["SKINDEMAPOSITIONRIGHT"].ToString();
                        //皮肤浮肿性质
                        objRecordContent.m_strSkinEdemaProperty = dataRow["SKINDEMAPROPERTYRIGHT"].ToString();
                        //记录时间
                        objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dataRow["RECORDDATE"].ToString());
                        //获取签名集合
                        if (dataRow["SEQUENCE_INT"] != DBNull.Value)
                        {

                            //if (sing.ContainsKey(dataRow["SEQUENCE_INT"].ToString()))
                            //{
                            //    clsEmrSigns_VO[] m_sinb = new clsEmrSigns_VO[1];
                            //    m_sinb[0] = (clsEmrSigns_VO)sing[dataRow["SEQUENCE_INT"].ToString()];
                            //    objRecordContent.objSignerArr = m_sinb;
                            //}
                            #region 优化修改部分
                            DataRow[] drsing = sing.Select("SIGN_INT='" + dataRow["SEQUENCE_INT"].ToString() + "'");
                            if (drsing.Length > 0)
                            {
                                clsEmrSigns_VO[] objEmrSigns_VO = new clsEmrSigns_VO[drsing.Length];
                                for (int li = 0; li < drsing.Length; li++)
                                {
                                    objEmrSigns_VO[li] = new clsEmrSigns_VO();
                                    objEmrSigns_VO[li].objEmployee = new clsEmrEmployeeBase_VO();

                                    objEmrSigns_VO[li].sequeid = drsing[li]["SIGN_INT"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPID_CHR = drsing[li]["EMPID_VCHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strLASTNAME_VCHR = drsing[li]["LASTNAME_VCHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPNO_CHR = drsing[li]["EMPNO_CHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strTECHNICALRANK_CHR = drsing[li]["EMPTECHNICALRANK"].ToString().Trim();
                                    objEmrSigns_VO[li].objEmployee.m_strLEVEL_CHR = drsing[li]["TECHNICALLEVEL_CHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPPWD_VCHR = drsing[li]["PSW_CHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPKEY_VCHR = drsing[li]["DIGITALSIGN_DTA"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strPYCODE_VCHR = drsing[li]["PYCODE_CHR"].ToString();
                                    int intTemp = 1;
                                    int.TryParse(drsing[li]["STATUS_INT"].ToString(), out intTemp);
                                    objEmrSigns_VO[li].objEmployee.m_intSTATUS_INT = intTemp;
                                    objEmrSigns_VO[li].controlName = drsing[li]["CAGETORY_VCHR"].ToString();
                                    objEmrSigns_VO[li].m_strFORMID_VCHR = drsing[li]["FORMNAME_VCHR"].ToString();
                                    objEmrSigns_VO[li].m_strREGISTERID_CHR = drsing[li]["REGISTERID_VCHR"].ToString();
                                    if (blnIsShowLevel)
                                    {
                                        objEmrSigns_VO[li].objEmployee.m_strTechnicalRank = drsing[li]["TECHNICALRANK_VCHR"].ToString();
                                    }
                                    else
                                    {
                                        objEmrSigns_VO[li].objEmployee.m_strTechnicalRank = string.Empty;
                                    }
                                    objEmrSigns_VO[li].objEmployee.m_StrHistroyLevel = drsing[li]["HISTROYLEVEL"].ToString();
                                    objEmrSigns_VO[li].m_dtmModiftDate = Convert.ToDateTime(drsing[li]["MODIFYDATE_DAT"]);

                                }
                                objRecordContent.objSignerArr = objEmrSigns_VO;
                            }

                            #endregion 
                        }

                        #region 旧的
                        //p_objHRPServ.CreateDatabaseParameter(4, out objDPArr1);
                        //objDPArr1[0].Value = p_strInPatientID.Trim();
                        //objDPArr1[1].DbType = DbType.DateTime;
                        //objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                        //objDPArr1[2].DbType = DbType.DateTime;
                        //objDPArr1[2].Value = DateTime.Parse(dataRow["OpenDate"].ToString());
                        //objDPArr1[3].Value = "frmIntensivetendRecord_CSRec";
                        //p_objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
                        //objDPArr3[0].Value = p_strInPatientID.Trim();
                        //objDPArr3[1].DbType = DbType.DateTime;
                        //objDPArr3[1].Value = DateTime.Parse(p_strInPatientDate);
                        //objDPArr3[2].DbType = DbType.DateTime;
                        //objDPArr3[2].Value = DateTime.Parse(dataRow["OpenDate"].ToString());
                        //objDPArr3[3].Value = "frmIntensivetendRecord_CSRec";

                        //lngInpectRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetInpectInfoSQL, ref dtbInpectValue, objDPArr1);
                        //if (lngInpectRes > 0 && dtbInpectValue.Rows.Count > 0)
                        //{
                        //    clsNurseRecordInpectInfo[] objInpectArr = new clsNurseRecordInpectInfo[dtbInpectValue.Rows.Count];
                        //    for (int m = 0; m < dtbInpectValue.Rows.Count; m++)
                        //    {
                        //        objInpectArr[m] = new clsNurseRecordInpectInfo();
                        //        objInpectArr[m].m_strINPECT_KIND = dtbInpectValue.Rows[m]["incept_kind"].ToString();
                        //        objInpectArr[m].m_strINPECT_METE = dtbInpectValue.Rows[m]["incept_mete"].ToString();
                        //    }
                        //    objRecordContent.m_objInpectArr = objInpectArr;
                        //}
                        //lngInpectRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetEductionInfoSQL, ref dtbEductionValue, objDPArr3);
                        //if (lngInpectRes > 0 && dtbEductionValue.Rows.Count > 0)
                        //{
                        //    clsNurseRecordEductionInfo[] objEductionArr = new clsNurseRecordEductionInfo[dtbEductionValue.Rows.Count];
                        //    for (int p = 0; p < dtbEductionValue.Rows.Count; p++)
                        //    {
                        //        objEductionArr[p] = new clsNurseRecordEductionInfo();
                        //        objEductionArr[p].m_strEDUCTION_KIND = dtbEductionValue.Rows[p]["eduction_kind"].ToString();
                        //        objEductionArr[p].m_strEDUCTION_METE = dtbEductionValue.Rows[p]["eduction_mete"].ToString();
                        //    }
                        //    objRecordContent.m_objEductionArr = objEductionArr;
                        //}
                        #endregion 

                        #region 优化修改部分
                        if (lngInpectRes > 0 && dtbInpectValue.Rows.Count > 0)
                        {
                            clsNurseRecordInpectInfo[] objInpectArr = null;
                            objInpectList.Clear();
                            DataRow[] drInpectV = dtbInpectValue.Select("OPENDATE='" + DateTime.Parse(dataRow["OPENDATE"].ToString()).ToString("yyyy年MM月dd日 HH:mm:ss") + "'");
                            if (drInpectV.Length > 0)
                            {
                                for (int l = 0; l < drInpectV.Length; l++)
                                {
                                    clsNurseRecordInpectInfo objInpect = new clsNurseRecordInpectInfo();
                                    objInpect.m_strINPECT_KIND = drInpectV[l]["INCEPT_KIND"].ToString();
                                    objInpect.m_strINPECT_METE = drInpectV[l]["INCEPT_METE"].ToString();
                                    objInpectList.Add(objInpect);
                                }
                                //break;
                            }
                            if (objInpectList.Count > 0)
                            {
                                objInpectArr = objInpectList.ToArray();
                            }
                            objInpectList.Clear();
                            objRecordContent.m_objInpectArr = objInpectArr;
                        }

                        if (lngInpectRes > 0 && dtbEductionValue.Rows.Count > 0)
                        {
                            clsNurseRecordEductionInfo[] objEductionArr = null;
                            objEductionList.Clear();
                            DataRow[] drEductionV = dtbEductionValue.Select("OPENDATE='" + DateTime.Parse(dataRow["OPENDATE"].ToString()).ToString("yyyy年MM月dd日 HH:mm:ss") + "'");
                            if (drEductionV.Length > 0)
                            {
                                for (int li = 0; li < drEductionV.Length; li++)
                                {
                                    clsNurseRecordEductionInfo objEduction = new clsNurseRecordEductionInfo();
                                    objEduction.m_strEDUCTION_KIND = drEductionV[li]["EDUCTION_KIND"].ToString();
                                    objEduction.m_strEDUCTION_METE = drEductionV[li]["EDUCTION_METE"].ToString();
                                    objEduction.m_strEDUCTION_COLOR = drEductionV[li]["EDUCTION_COLOR"].ToString();
                                    objEductionList.Add(objEduction);
                                }
                            }
                            if (objEductionList.Count > 0)
                            {
                                objEductionArr = objEductionList.ToArray();
                            }
                            objEductionList.Clear();
                            objRecordContent.m_objEductionArr = objEductionArr;
                        }
                        #endregion

                        p_objGeneralNurseRecordArr[i] = objRecordContent;
                    }
                    objDataInfo.m_objRecordContent = p_objGeneralNurseRecordArr[p_objGeneralNurseRecordArr.Length - 1];
                }
                objDataInfo.m_objRecordArr = p_objGeneralNurseRecordArr;
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                objDPArr2[0].Value = p_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDetailSQL, ref dtbDetail, objDPArr2);
                if (lngRes > 0 && dtbDetail.Rows.Count > 0)
                {
                    clsIntensivetendRecordContent_CSDetail objDetail = null;
                    p_objGeneralNurseDetailArr = new clsIntensivetendRecordContent_CSDetail[dtbDetail.Rows.Count];

                    #region 优化修改部分
                    DataTable singDetail = new DataTable();
                    List<long> dinosaursDetail = new List<long>();
                    for (int mm = 0; mm < dtbDetail.Rows.Count; mm++)
                    {
                        dinosaursDetail.Add(long.Parse(dtbDetail.Rows[mm]["SEQUENCE_INT"].ToString()));

                    }
                    //签名一次查出加快速度。
                    long lngdd = m_lngGetSignOne(dinosaursDetail, p_strInPatientID.Trim(), p_strInPatientDate, "intensivetendrecord_csdetail", out singDetail);
                    #endregion 
                    string strReturnValue = string.Empty;
                    bool blnIsShowLevel = true;
                    if (strReturnValue == "1")
                    {
                        blnIsShowLevel = true;
                    }
                    else
                    {
                        blnIsShowLevel = false;
                    }
                    for (int j = 0; j < dtbDetail.Rows.Count; j++)
                    {
                        objDetail = new clsIntensivetendRecordContent_CSDetail();
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
                       
                        #region 旧的
                        //获取签名集合
                        //if (dtbDetail.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        //{
                        //    long lngS = long.Parse(dtbDetail.Rows[j]["SEQUENCE_INT"].ToString());
                        //    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        //    long lngTemp = objSign.m_lngGetSign(lngS, out objDetail.objSignerArr);
                        //    //释放
                        //    objSign = null;
                        //}
                        #endregion

                        #region 优化修改部分
                        if (dtbDetail.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            //if (singDetail.ContainsKey(dtbDetail.Rows[j]["SEQUENCE_INT"].ToString()))
                            //{
                            //    clsEmrSigns_VO[] m_sinb = new clsEmrSigns_VO[1];
                            //    m_sinb[0] = (clsEmrSigns_VO)singDetail[dtbDetail.Rows[j]["SEQUENCE_INT"].ToString()];
                            //    objDetail.objSignerArr = m_sinb;
                            //}
                            #region 优化修改部分
                            DataRow[] drsing = singDetail.Select("SIGN_INT='" + dtbDetail.Rows[j]["SEQUENCE_INT"].ToString() + "'");
                            if (drsing.Length > 0)
                            {
                                clsEmrSigns_VO[] objEmrSigns_VO = new clsEmrSigns_VO[drsing.Length];
                                for (int li = 0; li < drsing.Length; li++)
                                {
                                    objEmrSigns_VO[li] = new clsEmrSigns_VO();
                                    objEmrSigns_VO[li].objEmployee = new clsEmrEmployeeBase_VO();

                                    objEmrSigns_VO[li].sequeid = drsing[li]["SIGN_INT"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPID_CHR = drsing[li]["EMPID_VCHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strLASTNAME_VCHR = drsing[li]["LASTNAME_VCHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPNO_CHR = drsing[li]["EMPNO_CHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strTECHNICALRANK_CHR = drsing[li]["EMPTECHNICALRANK"].ToString().Trim();
                                    objEmrSigns_VO[li].objEmployee.m_strLEVEL_CHR = drsing[li]["TECHNICALLEVEL_CHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPPWD_VCHR = drsing[li]["PSW_CHR"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strEMPKEY_VCHR = drsing[li]["DIGITALSIGN_DTA"].ToString();
                                    objEmrSigns_VO[li].objEmployee.m_strPYCODE_VCHR = drsing[li]["PYCODE_CHR"].ToString();
                                    int intTemp = 1;
                                    int.TryParse(drsing[li]["STATUS_INT"].ToString(), out intTemp);
                                    objEmrSigns_VO[li].objEmployee.m_intSTATUS_INT = intTemp;
                                    objEmrSigns_VO[li].controlName = drsing[li]["CAGETORY_VCHR"].ToString();
                                    objEmrSigns_VO[li].m_strFORMID_VCHR = drsing[li]["FORMNAME_VCHR"].ToString();
                                    objEmrSigns_VO[li].m_strREGISTERID_CHR = drsing[li]["REGISTERID_VCHR"].ToString();
                                    if (blnIsShowLevel)
                                    {
                                        objEmrSigns_VO[li].objEmployee.m_strTechnicalRank = drsing[li]["TECHNICALRANK_VCHR"].ToString();
                                    }
                                    else
                                    {
                                        objEmrSigns_VO[li].objEmployee.m_strTechnicalRank = string.Empty;
                                    }
                                    objEmrSigns_VO[li].objEmployee.m_StrHistroyLevel = drsing[li]["HISTROYLEVEL"].ToString();
                                    objEmrSigns_VO[li].m_dtmModiftDate = Convert.ToDateTime(drsing[li]["MODIFYDATE_DAT"]);

                                }
                                objDetail.objSignerArr = objEmrSigns_VO;
                            }

                            #endregion
                        }
                        #endregion 

                        p_objGeneralNurseDetailArr[j] = objDetail;
                    }
                }
                objDataInfo.m_objDetailArr = p_objGeneralNurseDetailArr;
                objDataInfo.m_intFlag = (int)enmRecordsType.GeneralNurseRecord_GXRec;

                if (objDataInfo.m_objRecordArr == null)
                {
                    objDataInfo.m_objRecordContent = new clsIntensivetendRecordContent_CS();
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

        #region 根据流水号获取签名集合
        /// <summary>
        /// 根据流水号获取签名集合
        /// </summary>
        /// <param name="p_lngSequence">流水号</param>
        /// <param name="p_objSignsArr">签名集合</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSignOne(List<long> m_list, string p_inpatientId, string p_inpatientDate, string p_strTableName, out DataTable p_objSignsArr)
        {
            long lngRes = 0;
            p_objSignsArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (m_list.Count == 0)
                    return -1;

                string strSeqSQL = string.Empty;
                string strReturnValue = string.Empty;
                bool blnIsShowLevel = true;
                //lngRes = m_lngGetConfigBySettingID("3014", out strReturnValue);

                if (strReturnValue == "1")
                {
                    strSeqSQL = "order by t.technicallevel_vchr desc,t.seq_int";
                }
                else
                {
                    strSeqSQL = "order by t.modifydate_dat,t.seq_int";
                }

                //lngRes = m_lngGetConfigBySettingID("3015", out strReturnValue);
                if (strReturnValue == "1")
                {
                    blnIsShowLevel = true;
                }
                else
                {
                    blnIsShowLevel = false;
                }

                string strSql = @"select 
                                    t.sign_int,t.empid_vchr,
									d.lastname_vchr,
                                    d.technicalrank_chr emptechnicalrank,
									t.technicalrank_vchr,
									d.empno_chr,
									d.psw_chr,
									d.digitalsign_dta,
									d.pycode_chr,
									d.status_int,
									d.technicallevel_chr,
                                    t.technicallevel_vchr histroylevel,
									t.cagetory_vchr,
									t.formname_vchr,
									t.registerid_vchr,t.modifydate_dat
								from t_emr_signcollection t
								left outer join t_bse_employee d on t.empid_vchr = d.empid_chr
                                inner join # m on m.status =0
                                where t.sign_int = m.sequence_int
                                and m.inpatientid = ?
                                and m.inpatientdate = ?
                                order by t.technicallevel_vchr desc,t.seq_int";
                //string str = "";

                //foreach (long m_lng in m_list)
                //{

                //    str += "'" + m_lng + "',";
                //}
                //str = str.Remove(str.Length - 1);
                strSql = strSql.Replace("#", p_strTableName);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_inpatientId.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_inpatientDate);

                //DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                int intSignCount = dtbValue.Rows.Count;
                DataRow objRow = null;
                clsEmrSigns_VO objEmrSigns_VO = null;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && intSignCount > 0)
                {
                    p_objSignsArr = new DataTable();
                    p_objSignsArr = dtbValue;
                    #region 屏蔽
                    //for (int i = 0; i < intSignCount; i++)
                    //{
                    //    objRow = dtbValue.Rows[i];
                    //    objEmrSigns_VO = new clsEmrSigns_VO();
                    //    objEmrSigns_VO.objEmployee = new clsEmrEmployeeBase_VO();

                    //    objEmrSigns_VO.sequeid = objRow["SIGN_INT"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strEMPID_CHR = objRow["EMPID_VCHR"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strLASTNAME_VCHR = objRow["LASTNAME_VCHR"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strEMPNO_CHR = objRow["EMPNO_CHR"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strTECHNICALRANK_CHR = objRow["EMPTECHNICALRANK"].ToString().Trim();
                    //    objEmrSigns_VO.objEmployee.m_strLEVEL_CHR = objRow["TECHNICALLEVEL_CHR"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strEMPPWD_VCHR = objRow["PSW_CHR"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strEMPKEY_VCHR = objRow["DIGITALSIGN_DTA"].ToString();
                    //    objEmrSigns_VO.objEmployee.m_strPYCODE_VCHR = objRow["PYCODE_CHR"].ToString();
                    //    int intTemp = 1;
                    //    int.TryParse(objRow["STATUS_INT"].ToString(), out intTemp);
                    //    objEmrSigns_VO.objEmployee.m_intSTATUS_INT = intTemp;
                    //    objEmrSigns_VO.controlName = objRow["CAGETORY_VCHR"].ToString();
                    //    objEmrSigns_VO.m_strFORMID_VCHR = objRow["FORMNAME_VCHR"].ToString();
                    //    objEmrSigns_VO.m_strREGISTERID_CHR = objRow["REGISTERID_VCHR"].ToString();
                    //    if (blnIsShowLevel)
                    //    {
                    //        objEmrSigns_VO.objEmployee.m_strTechnicalRank = objRow["TECHNICALRANK_VCHR"].ToString();
                    //    }
                    //    else
                    //    {
                    //        objEmrSigns_VO.objEmployee.m_strTechnicalRank = string.Empty;
                    //    }
                    //    objEmrSigns_VO.objEmployee.m_StrHistroyLevel = objRow["HISTROYLEVEL"].ToString();
                    //    objEmrSigns_VO.m_dtmModiftDate = Convert.ToDateTime(objRow["MODIFYDATE_DAT"]);
                    //    //显示职称
                    //    //objEmrSigns_VO.objEmployee.m_strLASTNAME_VCHR = objEmrSigns_VO.objEmployee.m_strTechnicalRank + " " + objEmrSigns_VO.objEmployee.m_strLASTNAME_VCHR;
                    //    p_objSignsArr.Add(objEmrSigns_VO.sequeid, objEmrSigns_VO);
                    //}
                    #endregion
                }
                ////释放
                //objHRPServ = null;

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
            //返回
            return lngRes;
        }
        #endregion

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
											intensivetendrecord_cs t1,intensivetendrecord_cscontent t2
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

