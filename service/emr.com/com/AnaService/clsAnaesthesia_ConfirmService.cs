using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.iCare.middletier.Anaesthesia
{
    /// <summary>
    /// Summary description for clsAnaesthesia_ConfirmService.
    /// alex 2003-8-11 麻醉同意书的MiddleTier
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAnaesthesia_ConfirmService : clsDiseaseTrackService
    {
        public clsAnaesthesia_ConfirmService()
        {
            //
            // TODO: Add constructor logic here
            //
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
            string c_strUpdateFirstPrintDateSQL = "update anaesthesia_confirm set firstprintdate= ? where trim(inpatientid)= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            { 
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                return objHRPSvc.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
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
        public override long m_lngGetDeleteRecordTimeList(            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            string c_strGetDeleteRecordTimeListSQL = "select createdate,opendate from anaesthesia_confirm where trim(inpatientid) = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            { 
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                //返回DB_Succees
                return (long)enmOperationResult.DB_Succeed;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
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
        public override long m_lngGetDeleteRecordTimeListAll(            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            string c_strGetDeleteRecordTimeListAllSQL = "select createdate,opendate from anaesthesia_confirm where trim(inpatientid) = ? and inpatientdate= ? and status=1";
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                //返回DB_Succees
                return (long)enmOperationResult.DB_Succeed;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
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
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
                clsAnaesthesia_ConfirmValue objContent = (clsAnaesthesia_ConfirmValue)p_objRecordContent;
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                objSign.m_lngGetSequenceValue("seq_emr_sign", out lngSequence);


                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(32, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = objContent.m_bytIfConfirm;
                if (objContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objContent.m_strConfirmReason;
                if (objContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;//status

                objDPArr[9].Value = objContent.m_strDiagnose_All;
                objDPArr[10].Value = objContent.m_strDiagnoseXML;
                objDPArr[11].Value = objContent.m_strOperationName_All;
                objDPArr[12].Value = objContent.m_strOperationNameXML;
                objDPArr[13].Value = objContent.m_strAnaMode_All;
                objDPArr[14].Value = objContent.m_strAnaModeXML;
                //			objDPArr[15].Value=objContent.m_strAnnounceSelection_All;
                objDPArr[15].Value = objContent.m_strAnnounceSelectionXML;
                objDPArr[16].Value = objContent.m_strAnnounceOther_All;
                objDPArr[17].Value = objContent.m_strAnnounceOtherXML;
                objDPArr[18].Value = objContent.m_strPatientSign_All;
                objDPArr[19].Value = objContent.m_strPatientSignXML;
                objDPArr[20].Value = objContent.m_strPatientSibSign_All;
                objDPArr[21].Value = objContent.m_strPatientSibSignXML;
                objDPArr[22].Value = objContent.m_strPatient_Tel_All;
                objDPArr[23].Value = objContent.m_strPatient_TelXML;
                objDPArr[24].Value = objContent.m_strSignerRelation_All;
                objDPArr[25].Value = objContent.m_strSignerRelationXML;
                objDPArr[26].Value = objContent.m_strSignerID_All;
                objDPArr[27].Value = objContent.m_strSignerIDXML;

                objDPArr[28].Value = objContent.m_strSpecialCase_All;
                objDPArr[29].Value = objContent.m_strSpecialCase_Xml;
                //add by huafeng.xiao
                objDPArr[30].Value = objContent.m_strOthers;
                objDPArr[31].Value = objContent.m_strOthers_XML;

                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(34, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strDiagnose;
                objDPArr2[6].Value = objContent.m_strOperationName;
                objDPArr2[7].Value = objContent.m_strAnaMode;


                #region OLD
                //				objDPArr2[8].Value=objContent.m_bolAnnounceSelection14;
                //				objDPArr2[9].Value=objContent.m_bolAnnounceSelection13;
                //				objDPArr2[10].Value=objContent.m_bolAnnounceSelection12;
                //				objDPArr2[11].Value=objContent.m_bolAnnounceSelection11;
                //				objDPArr2[12].Value=objContent.m_bolAnnounceSelection10;
                //				objDPArr2[13].Value=objContent.m_bolAnnounceSelection9;
                //				objDPArr2[14].Value=objContent.m_bolAnnounceSelection8;
                //				objDPArr2[15].Value=objContent.m_bolAnnounceSelection7;
                //				objDPArr2[16].Value=objContent.m_bolAnnounceSelection6;
                //				objDPArr2[17].Value=objContent.m_bolAnnounceSelection5;
                //				objDPArr2[18].Value=objContent.m_bolAnnounceSelection4;
                //				objDPArr2[19].Value=objContent.m_bolAnnounceSelection3;
                //				objDPArr2[20].Value=objContent.m_bolAnnounceSelection2;
                //				objDPArr2[21].Value=objContent.m_bolAnnounceSelection1;
                #endregion

                objDPArr2[8].Value = BoolToInt(objContent.m_bolAnnounceSelection14);
                objDPArr2[9].Value = BoolToInt(objContent.m_bolAnnounceSelection13);
                objDPArr2[10].Value = BoolToInt(objContent.m_bolAnnounceSelection12);
                objDPArr2[11].Value = BoolToInt(objContent.m_bolAnnounceSelection11);
                objDPArr2[12].Value = BoolToInt(objContent.m_bolAnnounceSelection10);
                objDPArr2[13].Value = BoolToInt(objContent.m_bolAnnounceSelection9);
                objDPArr2[14].Value = BoolToInt(objContent.m_bolAnnounceSelection8);
                objDPArr2[15].Value = BoolToInt(objContent.m_bolAnnounceSelection7);
                objDPArr2[16].Value = BoolToInt(objContent.m_bolAnnounceSelection6);
                objDPArr2[17].Value = BoolToInt(objContent.m_bolAnnounceSelection5);
                objDPArr2[18].Value = BoolToInt(objContent.m_bolAnnounceSelection4);
                objDPArr2[19].Value = BoolToInt(objContent.m_bolAnnounceSelection3);
                objDPArr2[20].Value = BoolToInt(objContent.m_bolAnnounceSelection2);
                objDPArr2[21].Value = BoolToInt(objContent.m_bolAnnounceSelection1);

                objDPArr2[22].Value = objContent.m_strAnnounceOther;
                objDPArr2[23].Value = objContent.m_strPatientSign;
                objDPArr2[24].Value = objContent.m_strPatientSibSign;
                objDPArr2[25].Value = objContent.m_strPatient_Tel;
                objDPArr2[26].Value = objContent.m_strSignerRelation;
                objDPArr2[27].Value = objContent.m_strSignerID;

                objDPArr2[28].Value = objContent.m_strSpecialCase;
                objDPArr2[29].Value = lngSequence;
                objDPArr2[30].Value = BoolToInt(objContent.m_bolAnnounceSelection15);
                objDPArr2[31].Value = BoolToInt(objContent.m_bolAnnounceSelection16);
                objDPArr2[32].Value = BoolToInt(objContent.m_bolAnnounceSelection17);
                //add by huafeng.xiao
                //2008.8.28
                objDPArr2[33].Value = objContent.m_strOthers;
                string c_strAddNewRecordSQL = @"insert into anaesthesia_confirm
			(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,
			confirmreason,confirmreasonxml,status,diagnose_all,diagnosexml,operationname_all,
			operationnamexml,anamode_all,anamodexml,announceselectionxml,announceother_all,
			announceotherxml,patientsign_all,patientsignxml,patientsibsign_all,patientsibsignxml,patient_tel_all,
			patient_telxml,signerrelation_all,signerrelationxml,signerid_all,signeridxml, 
			specialcase_all,specialcase_xml,others_vchr,others_vchr_xml
			) VALUES(
			?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,
			?,?,?,?)";

                //执行SQL
                long lngEff = 0;
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //执行SQL
                string c_strAddNewRecordContentSQL = @"insert into anaesthesia_confirmcontent(
			inpatientid,inpatientdate,opendate,modifydate,modifyuserid,diagnose,
			operationname,anamode,announceselection14,announceselection13,announceselection12,announceselection11,
			announceselection10,announceselection9,announceselection8,announceselection7,announceselection6,announceselection5,
			announceselection4,announceselection3,announceselection2,announceselection1,announceother,patientsign,
			patientsibsign,patient_tel,signerrelation,signerid, specialcase,sequence_int,announceselection15,announceselection16,announceselection17,others_vchr
			) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, ? ,?,?,?,?,?)";
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);
                //释放
                objSign = null;
                if (lngRes <= 0) return lngRes;


                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
        }



        /// <summary>
        ///  把新修改的内容保存到数据库。
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
                clsAnaesthesia_ConfirmValue objContent = (clsAnaesthesia_ConfirmValue)p_objRecordContent;
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(27, out objDPArr);

                objDPArr[0].Value = objContent.m_strDiagnose_All;
                objDPArr[1].Value = objContent.m_strDiagnoseXML;
                objDPArr[2].Value = objContent.m_strOperationName_All;
                objDPArr[3].Value = objContent.m_strOperationNameXML;
                objDPArr[4].Value = objContent.m_strAnaMode_All;
                objDPArr[5].Value = objContent.m_strAnaModeXML;
                //			objDPArr[6].Value=objContent.m_strAnnounceSelection_All;
                objDPArr[6].Value = objContent.m_strAnnounceSelectionXML;
                objDPArr[7].Value = objContent.m_strAnnounceOther_All;
                objDPArr[8].Value = objContent.m_strAnnounceOtherXML;
                objDPArr[9].Value = objContent.m_strPatientSign_All;
                objDPArr[10].Value = objContent.m_strPatientSignXML;
                objDPArr[11].Value = objContent.m_strPatientSibSign_All;
                objDPArr[12].Value = objContent.m_strPatientSibSignXML;
                objDPArr[13].Value = objContent.m_strPatient_Tel_All;
                objDPArr[14].Value = objContent.m_strPatient_TelXML;
                objDPArr[15].Value = objContent.m_strSignerRelation_All;
                objDPArr[16].Value = objContent.m_strSignerRelationXML;
                objDPArr[17].Value = objContent.m_strSignerID_All;
                objDPArr[18].Value = objContent.m_strSignerIDXML;

                objDPArr[19].Value = objContent.m_strSpecialCase_All;
                objDPArr[20].Value = objContent.m_strSpecialCase_Xml;
                objDPArr[21].Value = objContent.m_strOthers;
                objDPArr[22].Value = objContent.m_strOthers_XML;

                objDPArr[23].Value = objContent.m_strInPatientID;
                objDPArr[24].Value = objContent.m_dtmInPatientDate;
                objDPArr[25].Value = objContent.m_dtmOpenDate;
                objDPArr[26].Value = 0;

                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(34, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strDiagnose;
                objDPArr2[6].Value = objContent.m_strOperationName;
                objDPArr2[7].Value = objContent.m_strAnaMode;
                #region OLD
                //				objDPArr2[8].Value=objContent.m_bolAnnounceSelection14;
                //				objDPArr2[9].Value=objContent.m_bolAnnounceSelection13;
                //				objDPArr2[10].Value=objContent.m_bolAnnounceSelection12;
                //				objDPArr2[11].Value=objContent.m_bolAnnounceSelection11;
                //				objDPArr2[12].Value=objContent.m_bolAnnounceSelection10;
                //				objDPArr2[13].Value=objContent.m_bolAnnounceSelection9;
                //				objDPArr2[14].Value=objContent.m_bolAnnounceSelection8;
                //				objDPArr2[15].Value=objContent.m_bolAnnounceSelection7;
                //				objDPArr2[16].Value=objContent.m_bolAnnounceSelection6;
                //				objDPArr2[17].Value=objContent.m_bolAnnounceSelection5;
                //				objDPArr2[18].Value=objContent.m_bolAnnounceSelection4;
                //				objDPArr2[19].Value=objContent.m_bolAnnounceSelection3;
                //				objDPArr2[20].Value=objContent.m_bolAnnounceSelection2;
                //				objDPArr2[21].Value=objContent.m_bolAnnounceSelection1;
                #endregion

                objDPArr2[8].Value = BoolToInt(objContent.m_bolAnnounceSelection14);
                objDPArr2[9].Value = BoolToInt(objContent.m_bolAnnounceSelection13);
                objDPArr2[10].Value = BoolToInt(objContent.m_bolAnnounceSelection12);
                objDPArr2[11].Value = BoolToInt(objContent.m_bolAnnounceSelection11);
                objDPArr2[12].Value = BoolToInt(objContent.m_bolAnnounceSelection10);
                objDPArr2[13].Value = BoolToInt(objContent.m_bolAnnounceSelection9);
                objDPArr2[14].Value = BoolToInt(objContent.m_bolAnnounceSelection8);
                objDPArr2[15].Value = BoolToInt(objContent.m_bolAnnounceSelection7);
                objDPArr2[16].Value = BoolToInt(objContent.m_bolAnnounceSelection6);
                objDPArr2[17].Value = BoolToInt(objContent.m_bolAnnounceSelection5);
                objDPArr2[18].Value = BoolToInt(objContent.m_bolAnnounceSelection4);
                objDPArr2[19].Value = BoolToInt(objContent.m_bolAnnounceSelection3);
                objDPArr2[20].Value = BoolToInt(objContent.m_bolAnnounceSelection2);
                objDPArr2[21].Value = BoolToInt(objContent.m_bolAnnounceSelection1);
                objDPArr2[22].Value = objContent.m_strAnnounceOther;
                objDPArr2[23].Value = objContent.m_strPatientSign;
                objDPArr2[24].Value = objContent.m_strPatientSibSign;
                objDPArr2[25].Value = objContent.m_strPatient_Tel;
                objDPArr2[26].Value = objContent.m_strSignerRelation;
                objDPArr2[27].Value = objContent.m_strSignerID;

                objDPArr2[28].Value = objContent.m_strSpecialCase;
                objDPArr2[29].Value = lngSequence;
                objDPArr2[30].Value = BoolToInt(objContent.m_bolAnnounceSelection15);
                objDPArr2[31].Value = BoolToInt(objContent.m_bolAnnounceSelection16);
                objDPArr2[32].Value = BoolToInt(objContent.m_bolAnnounceSelection17);
                objDPArr2[33].Value = objContent.m_strOthers;

                //执行SQL
                string c_strModifyRecordSQL = @"update anaesthesia_confirm set diagnose_all=?,
			diagnosexml=?,operationname_all=?,operationnamexml=?,anamode_all=?,anamodexml=?,
			announceselectionxml=?,announceother_all=?,announceotherxml=?,
			patientsign_all=?,patientsignxml=?,patientsibsign_all=?,patientsibsignxml=?,
			patient_tel_all=?,patient_telxml=?,signerrelation_all=?,signerrelationxml=?,
			signerid_all=?,signeridxml=? , specialcase_all=? , specialcase_xml=?,others_vchr=?,others_vchr_xml=?
			where  inpatientid=? and inpatientdate=? and 
			opendate=? and status=?";
                long lngEff = 0;
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                string c_strAddNewRecordContentSQL = @"insert into anaesthesia_confirmcontent(
			inpatientid,inpatientdate,opendate,modifydate,modifyuserid,diagnose,
			operationname,anamode,announceselection14,announceselection13,announceselection12,announceselection11,
			announceselection10,announceselection9,announceselection8,announceselection7,announceselection6,announceselection5,
			announceselection4,announceselection3,announceselection2,announceselection1,announceother,patientsign,
			patientsibsign,patient_tel,signerrelation,signerid, specialcase,sequence_int,announceselection15,announceselection16,announceselection17,others_vchr
			) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?, ? ,?,?,?,?,?)";
                string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);
                //释放
                objSign = null;

                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
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
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                string c_strDeleteRecordSQL = @"update anaesthesia_confirm set status=1,deactiveddate=?,deactivedoperatorid=? where trim(inpatientid)=? and inpatientdate=? and opendate=? and status=0";
                long lngEff = 0;
                return p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
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
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate,
			b.modifydate from anaesthesia_confirm a,anaesthesia_plancontent b 
			where trim(a.inpatientid) = ? and a.inpatientdate= ? and a.opendate= ? 
			and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate 
			and b.opendate=a.opendate and
			b.modifydate=(select max(modifydate) from anaesthesia_confirmcontent 
			where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FirstPrintDate"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["ModifyDate"].ToString());
                }
                //返回DB_Succees
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
        }

        /// <summary>
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            string c_strCheckCreateDateSQL = @"select createuserid,opendate 
        			from anaesthesia_confirm where trim(inpatientid) = ? and inpatientdate= ? and createdate= ? and status=0";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = objDataRow["CreateUserID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(objDataRow["OpenDate"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
                //返回	
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }

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
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //按顺序给IDataParameter赋值

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable

                string c_strCheckLastModifyRecordSQL = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strCheckLastModifyRecordSQL = @"select top 1 t2.modifydate,
								t2.modifyuserid from anaesthesia_confirm as t1,anaesthesia_confirmcontent as t2
								where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
								and t1.opendate = t2.opendate and t1.status =0
								and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid from (select t2.modifydate,
								t2.modifyuserid from anaesthesia_confirm t1,anaesthesia_confirmcontent t2
								where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
								and t1.opendate = t2.opendate and t1.status =0
								and trim(t1.inpatientid) = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc) where rownum =1";
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from anaesthesia_confirm 
			where trim(inpatientid) = ? and inpatientdate= ? and opendate= ? and status=1 ";
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DeActivedOperatorID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DeActivedDate"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["ModifyDate"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["ModifyUserID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["ModifyDate"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
                }
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return -1;
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
        }


        [AutoComplete]
        private bool IntToBool(int intFlag)
        {
            if (intFlag == 0)
                return false;
            else
                return true;
        }
        [AutoComplete]
        private int BoolToInt(bool blnFlag)
        {
            if (blnFlag)
                return 1;
            else
                return 0;
        }
    }
}
