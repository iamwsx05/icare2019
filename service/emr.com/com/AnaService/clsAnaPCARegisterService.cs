using System;
using weCare.Core.Entity;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.iCare.middletier.Anaesthesia
{
    /// <summary>
    /// Summary description for clsAnaPCARegisterService.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAnaPCARegisterService : clsDiseaseTrackService
    {
        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr, out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = -1;
            // 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
            string c_strGetTimeListSQL = "select createdate,opendate from ana_pcaregister where trim(inpatientid) = ? and inpatientdate= ? and status=0";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["createdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
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
            }
            //返回
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
            string c_strGetCollectSettingSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate,
       deactiveddate,
       deactivedoperator,
       status,
       pre_diagnose_all,
       pre_diagnose_xml,
       operationname_all,
       operationname_xml,
       keepdrug_all,
       keepdrug_xml,
       operationsummary_all,
       operationsummary_xml,
       anamode_all,
       anamode_xml,
       levelpipe_all,
       levelpipe_xml,
       pcasummary_all,
       pcasummaryxml,
       pcamode from ana_pcaregister where trim(inpatientid) = ? and 
														inpatientdate = ? and opendate = ?";
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                DataTable p_dtbResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetCollectSettingSQL, ref p_dtbResult, objDPArr);

                if (lngRes > 0 && p_dtbResult.Rows.Count == 1)
                {
                    //设置结果
                    clsAnaPCARegisterValue m_objPCASetting = new clsAnaPCARegisterValue();
                    //
                    m_objPCASetting.m_strInPatientID = p_dtbResult.Rows[0]["inpatientid"].ToString().Trim();
                    m_objPCASetting.m_dtmInPatientDate = Convert.ToDateTime(p_dtbResult.Rows[0]["inpatientdate"]);
                    m_objPCASetting.m_dtmOpenDate = Convert.ToDateTime(p_dtbResult.Rows[0]["opendate"]);
                    m_objPCASetting.m_dtmCreateDate = Convert.ToDateTime(p_dtbResult.Rows[0]["createdate"]);
                    //				m_objPCASetting.m_dtmModifyDate = Convert.ToDateTime(p_dtbResult.Rows[0]["CreateDate"]);
                    m_objPCASetting.m_strCreateUserID = p_dtbResult.Rows[0]["createuserid"].ToString().Trim();
                    if (p_dtbResult.Rows[0]["ifconfirm"].ToString() == "")
                        m_objPCASetting.m_bytIfConfirm = 0;
                    else
                        m_objPCASetting.m_bytIfConfirm = Convert.ToByte(p_dtbResult.Rows[0]["ifconfirm"]);
                    m_objPCASetting.m_strConfirmReason = p_dtbResult.Rows[0]["confirmreason"].ToString();
                    m_objPCASetting.m_strConfirmReasonXML = p_dtbResult.Rows[0]["confirmreasonxml"].ToString();

                    if (p_dtbResult.Rows[0]["firstprintdate"].ToString() == "")
                        m_objPCASetting.m_dtmFirstPrintDate = DateTime.MinValue;
                    else
                        m_objPCASetting.m_dtmFirstPrintDate = Convert.ToDateTime(p_dtbResult.Rows[0]["firstprintdate"].ToString());
                    if (p_dtbResult.Rows[0]["deactiveddate"].ToString() == "")
                        m_objPCASetting.m_dtmDeActivedDate = DateTime.MinValue;
                    else
                        m_objPCASetting.m_dtmDeActivedDate = Convert.ToDateTime(p_dtbResult.Rows[0]["deactiveddate"].ToString());

                    //				m_objPCASetting.m_strDeActivedOperatorID = p_dtbResult.Rows[0]["DeActivedOperatorID"].ToString();
                    if (p_dtbResult.Rows[0]["ifconfirm"].ToString() == "")
                        m_objPCASetting.m_bytStatus = 0;
                    else
                        m_objPCASetting.m_bytStatus = Convert.ToByte(p_dtbResult.Rows[0]["status"]);

                    //				m_objPCASetting.m_arrAnaesthetist = ;
                    m_objPCASetting.m_strAnaModeAll = p_dtbResult.Rows[0]["anamode_all"].ToString();
                    m_objPCASetting.m_strAnaModeXML = p_dtbResult.Rows[0]["anamode_xml"].ToString();
                    m_objPCASetting.m_strKeepDrugAll = p_dtbResult.Rows[0]["keepdrug_all"].ToString();
                    m_objPCASetting.m_strKeepDrugXML = p_dtbResult.Rows[0]["keepdrug_xml"].ToString();
                    m_objPCASetting.m_strLevelPipeAll = p_dtbResult.Rows[0]["levelpipe_all"].ToString();
                    m_objPCASetting.m_strLevelPipeXML = p_dtbResult.Rows[0]["levelpipe_xml"].ToString();
                    m_objPCASetting.m_strOperationNameAll = p_dtbResult.Rows[0]["operationname_all"].ToString();
                    m_objPCASetting.m_strOperationNameXML = p_dtbResult.Rows[0]["operationname_xml"].ToString();
                    m_objPCASetting.m_strOperationSummaryAll = p_dtbResult.Rows[0]["operationsummary_all"].ToString();
                    m_objPCASetting.m_strOperationSummaryXML = p_dtbResult.Rows[0]["operationsummary_xml"].ToString();
                    m_objPCASetting.m_strPCASummaryAll = p_dtbResult.Rows[0]["pcasummary_all"].ToString();
                    m_objPCASetting.m_strPCASummaryXML = p_dtbResult.Rows[0]["pcasummaryxml"].ToString();
                    m_objPCASetting.m_strPreDiagnoseAll = p_dtbResult.Rows[0]["pre_diagnose_all"].ToString();
                    m_objPCASetting.m_strPreDiagnoseXML = p_dtbResult.Rows[0]["pre_diagnose_xml"].ToString();

                    m_objPCASetting.m_strPCAMode = p_dtbResult.Rows[0]["pcamode"].ToString().Trim();
                    //
                    m_lngGetPCAContent(p_objHRPServ, ref m_objPCASetting);

                    p_objRecordContent = m_objPCASetting;

                }

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
            }
            return lngRes;
        }

        //获取PCARegisterContent内容
        [AutoComplete]
        protected long m_lngGetPCAContent(clsHRPTableService p_objHRPServ, ref clsAnaPCARegisterValue p_objRecordContent)
        {
            string c_strGetPCARegisterContentSQL = @"
			select a.*
			from ana_pcaregistercontent a
			join
			(
				select inpatientid,inpatientdate,opendate,max(modifydate) modifydate
				from ana_pcaregistercontent
				group by inpatientid,inpatientdate,opendate
			) b on a.inpatientid=b.inpatientid and a.inpatientdate=b.inpatientdate
			and a.opendate=b.opendate and a.modifydate=b.modifydate
			where trim(a.inpatientid)=? and a.inpatientdate=? and a.opendate=? 
		";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //			IDataParameter[] objParams=new OdbcParameter[3];
                //			for(int i=0;i<objParams.Length;i++) objParams[i]=new OdbcParameter();
                IDataParameter[] objParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out objParams);

                objParams[0].Value = p_objRecordContent.m_strInPatientID;
                objParams[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objParams[2].Value = p_objRecordContent.m_dtmOpenDate;

                DataTable objDT = new DataTable();
                long ret = p_objHRPServ.lngGetDataTableWithParameters(c_strGetPCARegisterContentSQL, ref objDT, objParams);
                if ((ret > 0) && (objDT.Rows.Count > 0))
                {
                    p_objRecordContent.m_strPreDiagnose = objDT.Rows[0]["pre_diagnose"].ToString().Trim();
                    p_objRecordContent.m_strOperationName = objDT.Rows[0]["operationname"].ToString().Trim();
                    p_objRecordContent.m_strKeepDrug = objDT.Rows[0]["keepdrug"].ToString().Trim();
                    p_objRecordContent.m_strOperationSummary = objDT.Rows[0]["operationsummary"].ToString().Trim();
                    p_objRecordContent.m_strAnaMode = objDT.Rows[0]["anamode"].ToString().Trim();
                    p_objRecordContent.m_strLevelPipe = objDT.Rows[0]["levelpipe"].ToString().Trim();
                    p_objRecordContent.m_strPCASummary = objDT.Rows[0]["pcasummary"].ToString().Trim();

                    p_objRecordContent.m_strPCAStartDate = ToDateTime(objDT.Rows[0]["pcastartdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objRecordContent.m_strPCAEndDate = ToDateTime(objDT.Rows[0]["pcaenddate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    p_objRecordContent.m_intPCADays = ToInt(objDT.Rows[0]["pcadays"]);
                    p_objRecordContent.m_strContent_Grid = objDT.Rows[0]["contentgrid"].ToString().Trim();
                    p_objRecordContent.m_strContent_Text = objDT.Rows[0]["contenttext"].ToString().Trim();
                    p_objRecordContent.m_strContent_Radio = objDT.Rows[0]["contentradio"].ToString().Trim();

                    return 1;
                }
                else
                {
                    return 0;
                }
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
            }
        }

        /// <summary>
        /// 从麻醉记录单中获取手术设置
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAnaContentFromRecord(ArrayList p_arlPatient,
            ref DataTable p_dtbResult)
        {
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_arlPatient[0].ToString();
                objDPArr[1].Value = DateTime.Parse(p_arlPatient[1].ToString());
                objDPArr[2].Value = DateTime.Parse(p_arlPatient[2].ToString());

                string c_strGetAnaDiagnoseSQL = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strGetAnaDiagnoseSQL = @"select top 1 * from ana_collectioncontent 
												where inpatientid = ? and inpatientdate = ?
												and opendate < ? order by modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strGetAnaDiagnoseSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       modifyuserid,
       ananumber,
       weight,
       sbp,
       dbp,
       temperature,
       pulse,
       breath,
       pre_diagnose,
       pre_operationname,
       after_diagnose,
       after_operationname,
       bodystate,
       asalevel,
       asaemergency,
       pre_anadrug,
       anamode,
       anaplane,
       inducement,
       anaeffect,
       keepdrug,
       anasummary,
       operationsummary,
       leaving_room,
       check_after,
       inducementdrug,
       bloodtype,
       stature,
       flowno,
       bloodout from (select inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       modifyuserid,
       ananumber,
       weight,
       sbp,
       dbp,
       temperature,
       pulse,
       breath,
       pre_diagnose,
       pre_operationname,
       after_diagnose,
       after_operationname,
       bodystate,
       asalevel,
       asaemergency,
       pre_anadrug,
       anamode,
       anaplane,
       inducement,
       anaeffect,
       keepdrug,
       anasummary,
       operationsummary,
       leaving_room,
       check_after,
       inducementdrug,
       bloodtype,
       stature,
       flowno,
       bloodout from ana_collectioncontent	where trim(inpatientid) = ? and inpatientdate = ?
												and opendate < ? order by modifydate desc) where rownum=1";


                lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetAnaDiagnoseSQL, ref p_dtbResult, objDPArr);
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
            }
            return lngRes;
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
            out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;
            return 1;
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
                clsAnaPCARegisterValue objContent = (clsAnaPCARegisterValue)p_objRecordContent;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(24, out objDPArr);

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


                objDPArr[9].Value = objContent.m_strPreDiagnoseAll;
                objDPArr[10].Value = objContent.m_strPreDiagnoseXML;
                objDPArr[11].Value = objContent.m_strOperationNameAll;
                objDPArr[12].Value = objContent.m_strOperationNameXML;
                objDPArr[13].Value = objContent.m_strKeepDrugAll;
                objDPArr[14].Value = objContent.m_strKeepDrugXML;
                objDPArr[15].Value = objContent.m_strOperationSummaryAll;
                objDPArr[16].Value = objContent.m_strOperationSummaryXML;
                objDPArr[17].Value = objContent.m_strAnaModeAll;
                objDPArr[18].Value = objContent.m_strAnaModeXML;
                objDPArr[19].Value = objContent.m_strLevelPipeAll;
                objDPArr[20].Value = objContent.m_strLevelPipeXML;
                objDPArr[21].Value = objContent.m_strPCASummaryAll;
                objDPArr[22].Value = objContent.m_strPCASummaryXML;

                objDPArr[23].Value = objContent.m_strPCAMode;


                for (int i1 = 0; i1 < objDPArr.Length; i1++)
                {
                    if (objDPArr[i1].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }
                }

                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(18, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strPreDiagnose;
                objDPArr2[6].Value = objContent.m_strOperationName;
                objDPArr2[7].Value = objContent.m_strKeepDrug;
                objDPArr2[8].Value = objContent.m_strOperationSummary;
                objDPArr2[9].Value = objContent.m_strAnaMode;
                objDPArr2[10].Value = objContent.m_strLevelPipe;
                objDPArr2[11].Value = objContent.m_strPCASummary;

                objDPArr2[12].Value = DateTime.Parse(objContent.m_strPCAStartDate);
                objDPArr2[13].Value = DateTime.Parse(objContent.m_strPCAEndDate);
                objDPArr2[14].Value = objContent.m_intPCADays;
                objDPArr2[15].Value = objContent.m_strContent_Grid;
                objDPArr2[16].Value = objContent.m_strContent_Text;
                objDPArr2[17].Value = objContent.m_strContent_Radio;


                for (int i1 = 0; i1 < objDPArr2.Length; i1++)
                {
                    if (objDPArr2[i1].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }
                }
                //执行SQL
                string c_strAddPCARegisterSQL = @"insert into ana_pcaregister 
							(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,
							confirmreason,confirmreasonxml,status,pre_diagnose_all,pre_diagnose_xml,
							operationname_all,operationname_xml,keepdrug_all,keepdrug_xml,operationsummary_all,
							operationsummary_xml,anamode_all,anamode_xml,levelpipe_all,levelpipe_xml,pcasummary_all,pcasummaryxml,pcamode)	values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                long lngEff = 0;
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddPCARegisterSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //执行SQL
                string c_strAddPCARegisterContentSQL = @"insert into ana_pcaregistercontent values
																(?,?,?,?,?,?  ,?,?,?,?,?,?,  ?,?,?,?,?,?)";
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddPCARegisterContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_arrAnaesthetist != null)
                {
                    for (int i = 0; i < objContent.m_arrAnaesthetist.Length; i++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPSvc.CreateDatabaseParameter(6, out objDPArr3);


                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;
                        objDPArr3[4].Value = objContent.m_arrAnaesthetist[i].m_strAnaesthetistID;
                        objDPArr3[5].Value = objContent.m_arrAnaesthetist[i].m_strAnaFlag;

                        string c_strAddAnaesthetistSQL = @"insert into ana_pcaregisteranaesthetist
													values(?,?,?,?,?,?)";
                        long lngEffect = 0;
                        long lngResult = p_objHRPServ.lngExecuteParameterSQL(c_strAddAnaesthetistSQL, ref lngEffect, objDPArr3);
                        if (lngResult <= 0)
                            return lngResult;
                    }
                }

                return 1;
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
            }
        }

        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            return 1;
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
                clsAnaPCARegisterValue objContent = (clsAnaPCARegisterValue)p_objRecordContent;

                #region 主表
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(19, out objDPArr);

                objDPArr[0].Value = objContent.m_strPreDiagnoseAll;
                objDPArr[1].Value = objContent.m_strPreDiagnoseXML;
                objDPArr[2].Value = objContent.m_strOperationNameAll;
                objDPArr[3].Value = objContent.m_strOperationNameXML;
                objDPArr[4].Value = objContent.m_strKeepDrugAll;
                objDPArr[5].Value = objContent.m_strKeepDrugXML;
                objDPArr[6].Value = objContent.m_strOperationSummaryAll;
                objDPArr[7].Value = objContent.m_strOperationSummaryXML;
                objDPArr[8].Value = objContent.m_strAnaModeAll;
                objDPArr[9].Value = objContent.m_strAnaModeXML;
                objDPArr[10].Value = objContent.m_strLevelPipeAll;
                objDPArr[11].Value = objContent.m_strLevelPipeXML;
                objDPArr[12].Value = objContent.m_strPCASummaryAll;
                objDPArr[13].Value = objContent.m_strPCASummaryXML;
                objDPArr[14].Value = objContent.m_strPCAMode;

                objDPArr[15].Value = objContent.m_strInPatientID;
                objDPArr[16].Value = objContent.m_dtmInPatientDate;
                objDPArr[17].Value = objContent.m_dtmOpenDate;
                objDPArr[18].Value = 0;

                //修改主表
                string c_strModifyPCARegisterSQL = @"update ana_pcaregister set pre_diagnose_all = ?, pre_diagnose_xml = ?,
							operationname_all = ?, operationname_xml = ?, keepdrug_all = ?, keepdrug_xml = ?,
							operationsummary_all = ?, operationsummary_xml = ?, anamode_all = ?, anamode_xml = ?,
							levelpipe_all = ?, levelpipe_xml = ?, pcasummary_all = ?, pcasummaryxml = ? ,pcamode=? 
							where trim(inpatientid)=? and inpatientdate=? and opendate=? and status=?";
                long lngEff = 0;
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyPCARegisterSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                #endregion


                #region 子表
                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(18, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strPreDiagnose;
                objDPArr2[6].Value = objContent.m_strOperationName;
                objDPArr2[7].Value = objContent.m_strKeepDrug;
                objDPArr2[8].Value = objContent.m_strOperationSummary;
                objDPArr2[9].Value = objContent.m_strAnaMode;
                objDPArr2[10].Value = objContent.m_strLevelPipe;
                objDPArr2[11].Value = objContent.m_strPCASummary;

                objDPArr2[12].Value = DateTime.Parse(objContent.m_strPCAStartDate);
                objDPArr2[13].Value = DateTime.Parse(objContent.m_strPCAEndDate);
                objDPArr2[14].Value = objContent.m_intPCADays;
                objDPArr2[15].Value = objContent.m_strContent_Grid;
                objDPArr2[16].Value = objContent.m_strContent_Text;
                objDPArr2[17].Value = objContent.m_strContent_Radio;

                //添加子表
                string c_strAddPCARegisterContentSQL = @"insert into ana_pcaregistercontent values
																(?,?,?,?,?,?  ,?,?,?,?,?,?,  ?,?,?,?,?,?)";
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddPCARegisterContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0)
                    return lngRes;
                #endregion


                #region 医生列表
                if (objContent.m_arrAnaesthetist != null)
                {
                    for (int i = 0; i < objContent.m_arrAnaesthetist.Length; i++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPSvc.CreateDatabaseParameter(6, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;
                        objDPArr3[4].Value = objContent.m_arrAnaesthetist[i].m_strAnaesthetistID;
                        objDPArr3[5].Value = objContent.m_arrAnaesthetist[i].m_strAnaFlag;

                        string c_strAddAnaesthetistSQL = @"insert into ana_pcaregisteranaesthetist
													values(?,?,?,?,?,?)";
                        long lngEffect = 0;
                        long lngResult = p_objHRPServ.lngExecuteParameterSQL(c_strAddAnaesthetistSQL, ref lngEffect, objDPArr3);
                        if (lngResult <= 0) return lngResult;
                    }
                }

                #endregion

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
            }
        }

        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            return 1;
        }

        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.MinValue;
            p_strFirstPrintDate = null;

            try
            {
                string strSql = @"select max(modifydate) modifydate from ana_pcaregistercontent where trim(inpatientid) = ? and
					inpatientdate = ? and opendate = ?";

                IDataParameter[] objDPArr3 = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].Value = p_strInPatientID;
                objDPArr3[1].DbType = DbType.DateTime;
                objDPArr3[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr3[2].DbType = DbType.DateTime;
                objDPArr3[2].Value = DateTime.Parse(p_strOpenDate);
                DataTable dtbResult = new DataTable();
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbResult, objDPArr3);
                //long lngRes = p_objHRPServ.DoGetDataTable(strSql,ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count == 1)
                {
                    try
                    {
                        p_dtmModifyDate = DateTime.Parse(dtbResult.Rows[0]["modifydate"].ToString());
                    }
                    catch
                    {
                        p_dtmModifyDate = DateTime.MinValue;
                    }
                    p_strFirstPrintDate = null;
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
                p_objHRPServ.Dispose();
            }
        }

        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            return 1;
        }

        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return 1;
        }

        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return 1;
        }

        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            return 1;
        }

        [AutoComplete]
        private DateTime ToDateTime(object objValue)
        {
            try
            {
                return Convert.ToDateTime(objValue);
            }
            catch (Exception)
            {
                return Convert.ToDateTime("1900-1-1 0:0:0");
            }
        }
        [AutoComplete]
        private int ToInt(object objValue)
        {
            try
            {
                return Convert.ToInt32(objValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
