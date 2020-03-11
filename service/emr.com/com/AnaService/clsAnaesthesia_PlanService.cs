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
    /// Summary description for clsAnaesthesia_PlanService.
    /// alex 2003-8-11 麻醉计划的MiddleTier
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAnaesthesia_PlanService : clsDiseaseTrackService
    {
        public clsAnaesthesia_PlanService()
        {
            //
            // TODO: Add constructor logic here
            //
        }


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
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            string c_strGetTimeListSQL = "select createdate,opendate from anaesthesia_plan where trim(inpatientid) = ? and inpatientdate= ? and status=0";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
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
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                int intRowCount = dtbValue.Rows.Count;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
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
            }
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
        public override long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            string c_strUpdateFirstPrintDateSQL = "update anaesthesia_plan set firstprintdate= ? where trim(inpatientid)= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";
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
        public override long m_lngGetDeleteRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            string c_strGetDeleteRecordTimeListSQL = "select createdate,opendate from anaesthesia_plan where trim(inpatientid) = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";
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
                int intRowCount = dtbValue.Rows.Count;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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
                return (long)enmOperationResult.DB_Fail;
            }
            finally
            {
                objHRPSvc.Dispose();
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
        public override long m_lngGetDeleteRecordTimeListAll(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            string c_strGetDeleteRecordTimeListAllSQL = "select createdate,opendate from anaesthesia_plan where trim(inpatientid) = ? and inpatientdate= ? and status=1";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
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
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strOpenDateArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateDateArr[i] = DateTime.Parse(objDataRow["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(objDataRow["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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
                return (long)enmOperationResult.DB_Fail;
            }
            finally
            {
                objHRPSvc.Dispose();
            }
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
                IDataParameter[] objDPArr1 = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr1);

                objDPArr1[0].Value = p_strInPatientID;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr1[2].Value = DateTime.Parse(p_strOpenDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                string c_strGetRecordContentSQL = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strGetRecordContentSQL = @"select top 1 * from anaesthesia_plan as t1,
							anaesthesia_plancontent as t2 where	t1.inpatientid = ? and t1.inpatientdate =? and t1.opendate = ?
							and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate and t1.status =0	order by t2.modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strGetRecordContentSQL = @"select * from (select t1.*, t2.modifydate,t2.modifyuserid,t2.weight,t2.bloodtype,t2.diagnose,t2.operationname,	t2.specialcase,t2.anamode,t2.observeselect,t2.anainducement,t2.asalevel,t2.asaemergency,t2.keepdrug,t2.keepliquid,	t2.remark,t2.unreuseddrug,t2.inducementdrug,t2.restoredrug,t2.anaoperationroomid,t2.anaoperationroomname,t2.equipmentmodelnames	from anaesthesia_plan t1,anaesthesia_plancontent t2 where trim(t1.inpatientid) = ? and t1.inpatientdate =? and t1.opendate = ?	and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate	and t1.status =0	order by t2.modifydate desc) where rownum=1";

                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount == 1)
                {

                    //设置结果
                    System.Data.DataRow objDataRow = dtbValue.Rows[0];
                    clsAnaesthesia_PlanValue objRecordContent = new clsAnaesthesia_PlanValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(objDataRow["createdate"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(objDataRow["modifydate"].ToString());

                    if (objDataRow["firstprintdate"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objDataRow["firstprintdate"].ToString());
                    objRecordContent.m_strCreateUserID = objDataRow["createuserid"].ToString();
                    objRecordContent.m_strModifyUserID = objDataRow["modifyuserid"].ToString();
                    if (objDataRow["ifconfirm"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(objDataRow["ifconfirm"].ToString());
                    if (objDataRow["status"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(objDataRow["status"].ToString());

                    objRecordContent.m_strConfirmReason = objDataRow["confirmreason"].ToString();
                    objRecordContent.m_strConfirmReasonXML = objDataRow["confirmreasonxml"].ToString();

                    objRecordContent.m_strWeight_All = objDataRow["weight_all"].ToString();
                    objRecordContent.m_strWeightXML = objDataRow["weightxml"].ToString();
                    objRecordContent.m_strBloodType_All = objDataRow["bloodtype_all"].ToString();
                    objRecordContent.m_strBloodTypeXML = objDataRow["bloodtypexml"].ToString();
                    objRecordContent.m_strDiagnose_All = objDataRow["diagnose_all"].ToString();
                    objRecordContent.m_strDiagnoseXML = objDataRow["diagnosexml"].ToString();
                    objRecordContent.m_strOperationName_All = objDataRow["operationname_all"].ToString();
                    objRecordContent.m_strOperationNameXML = objDataRow["operationnamexml"].ToString();
                    objRecordContent.m_strSpecialCase_All = objDataRow["specialcase_all"].ToString();
                    objRecordContent.m_strSpecialCaseXML = objDataRow["specialcasexml"].ToString();
                    objRecordContent.m_strAnaMode_All = objDataRow["anamode_all"].ToString();
                    objRecordContent.m_strAnaModeXML = objDataRow["anamodexml"].ToString();
                    objRecordContent.m_strObserveSelect_All = objDataRow["observeselect_all"].ToString();
                    objRecordContent.m_strObserveSelectXML = objDataRow["observeselectxml"].ToString();
                    objRecordContent.m_strAnaInducement_All = objDataRow["anainducement_all"].ToString();
                    objRecordContent.m_strAnaInducementXML = objDataRow["anainducementxml"].ToString();
                    objRecordContent.m_strASALevel_All = objDataRow["asalevel_all"].ToString();
                    objRecordContent.m_strASALevelXML = objDataRow["asalevelxml"].ToString();
                    objRecordContent.m_strASAEmergency_All = objDataRow["asaemergency_all"].ToString();
                    objRecordContent.m_strASAEmergencyXML = objDataRow["asaemergencyxml"].ToString();
                    objRecordContent.m_strKeepDrug_All = objDataRow["keepdrug_all"].ToString();
                    objRecordContent.m_strKeepDrugXML = objDataRow["keepdrugxml"].ToString();
                    objRecordContent.m_strKeepLiquid_All = objDataRow["keepliquid_all"].ToString();
                    objRecordContent.m_strKeepLiquidXML = objDataRow["keepliquidxml"].ToString();
                    objRecordContent.m_strUnReusedDrug_All = objDataRow["unreuseddrug"].ToString();
                    objRecordContent.m_strUnReusedDrugXML = objDataRow["unreuseddrugxml"].ToString();
                    objRecordContent.m_strInducementDrug_All = objDataRow["inducementdrug_all"].ToString();
                    objRecordContent.m_strInducementDrugXML = objDataRow["inducementdrugxml"].ToString();
                    objRecordContent.m_strRestoreDrug_All = objDataRow["restoredrug_all"].ToString();
                    objRecordContent.m_strRestoreDrugXML = objDataRow["restoredrugxml"].ToString();
                    objRecordContent.m_strRemark_All = objDataRow["remark_all"].ToString();
                    objRecordContent.m_strRemarkXML = objDataRow["remarkxml"].ToString();


                    objRecordContent.m_strWeight = objDataRow["weight"].ToString();
                    objRecordContent.m_strBloodType = objDataRow["bloodtype"].ToString();
                    objRecordContent.m_strDiagnose = objDataRow["diagnose"].ToString();
                    objRecordContent.m_strOperationName = objDataRow["operationname"].ToString();
                    objRecordContent.m_strSpecialCase = objDataRow["specialcase"].ToString();
                    objRecordContent.m_strAnaMode = objDataRow["anamode"].ToString();
                    objRecordContent.m_strInducementDrug = objDataRow["inducementdrug"].ToString();
                    objRecordContent.m_strRestoreDrug = objDataRow["restoredrug"].ToString();
                    objRecordContent.m_strUnReusedDrug = objDataRow["unreuseddrug"].ToString();
                    objRecordContent.m_strObserveSelect = objDataRow["observeselect"].ToString();
                    objRecordContent.m_strAnaInducement = objDataRow["anainducement"].ToString();
                    objRecordContent.m_strASALevel = objDataRow["asalevel"].ToString();
                    if (clsHRPTableService.bytDatabase_Selector == 0)
                        objRecordContent.m_bolASAEmergency = Convert.ToBoolean(objDataRow["asaemergency"]);
                    else if (clsHRPTableService.bytDatabase_Selector == 2)
                        objRecordContent.m_bolASAEmergency = (objDataRow["asaemergency"] != DBNull.Value && Convert.ToInt32(objDataRow["asaemergency"]) == 1 ? true : false);
                    objRecordContent.m_strKeepDrug = objDataRow["keepdrug"].ToString();
                    objRecordContent.m_strKeepLiquid = objDataRow["keepliquid"].ToString();
                    objRecordContent.m_strRemark = objDataRow["remark"].ToString();
                    objRecordContent.m_strAnaOperationRoomID = objDataRow["anaoperationroomid"].ToString();
                    objRecordContent.m_strAnaOperationRoomName = objDataRow["anaoperationroomname"].ToString();
                    objRecordContent.m_strEquipmentModelNames = objDataRow["equipmentmodelnames"].ToString();

                    string c_strGetRecordAnasthetistSQL = @"select t3.lastname_vchr firstname,t2.* from anaesthesia_plan t1,
			anaesthesia_plananasthetist t2,
			t_bse_employee t3
			where trim(t1.inpatientid) = ? and t1.inpatientdate =? and t1.opendate = ? and t2.modifydate = ?
			and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
			and t1.status =0
			and t2.anaesthetistid = t3.empid_chr
			order by t2.modifydate desc";
                    DataTable dtbSubTable = new DataTable();
                    objDPArr1[3].Value = DateTime.Parse(objDataRow["modifydate"].ToString());
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetRecordAnasthetistSQL, ref dtbSubTable, objDPArr1);
                    if (lngRes > 0 && dtbValue.Rows.Count >= 1)
                    {
                        clsAnaesthesia_PlanAnasthetistValue[] objAnasthetistArr = new clsAnaesthesia_PlanAnasthetistValue[dtbSubTable.Rows.Count];
                        for (int i = 0; i < objAnasthetistArr.Length; i++)
                        {
                            objAnasthetistArr[i] = new clsAnaesthesia_PlanAnasthetistValue();
                            objAnasthetistArr[i].m_strInPatientID = objRecordContent.m_strInPatientID;
                            objAnasthetistArr[i].m_dtmInPatientDate = objRecordContent.m_dtmInPatientDate;
                            objAnasthetistArr[i].m_dtmOpenDate = objRecordContent.m_dtmOpenDate;
                            objAnasthetistArr[i].m_dtmModifyDate = objRecordContent.m_dtmModifyDate;
                            objAnasthetistArr[i].m_strChiefFlag = dtbSubTable.Rows[i]["chief_flag"].ToString();
                            objAnasthetistArr[i].m_strAnaesthetistID = dtbSubTable.Rows[i]["anaesthetistid"].ToString();
                            objAnasthetistArr[i].m_strAnaesthetistName = dtbSubTable.Rows[i]["firstname"].ToString();
                        }
                        objRecordContent.m_objAnasthetistArr = objAnasthetistArr;
                    }

                    p_objRecordContent = objRecordContent;
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
            string c_strCheckCreateDateSQL = @"select createuserid,opendate from anaesthesia_plan where trim(inpatientid) = ? and inpatientdate= ? and createdate= ? and status=0";
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
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);
                int intRowCount = dtbValue.Rows.Count;
                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && intRowCount == 1)
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
                clsAnaesthesia_PlanValue objContent = (clsAnaesthesia_PlanValue)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(39, out objDPArr);

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

                objDPArr[9].Value = objContent.m_strWeight_All;
                objDPArr[10].Value = objContent.m_strWeightXML;
                objDPArr[11].Value = objContent.m_strBloodType_All;
                objDPArr[12].Value = objContent.m_strBloodTypeXML;
                objDPArr[13].Value = objContent.m_strDiagnose_All;
                objDPArr[14].Value = objContent.m_strDiagnoseXML;
                objDPArr[15].Value = objContent.m_strOperationName_All;
                objDPArr[16].Value = objContent.m_strOperationNameXML;
                objDPArr[17].Value = objContent.m_strSpecialCase_All;
                objDPArr[18].Value = objContent.m_strSpecialCaseXML;
                objDPArr[19].Value = objContent.m_strAnaMode_All;
                objDPArr[20].Value = objContent.m_strAnaModeXML;
                objDPArr[21].Value = objContent.m_strObserveSelect_All;
                objDPArr[22].Value = objContent.m_strObserveSelectXML;
                objDPArr[23].Value = objContent.m_strAnaInducement_All;
                objDPArr[24].Value = objContent.m_strAnaInducementXML;
                objDPArr[25].Value = objContent.m_strASALevelXML;
                objDPArr[26].Value = objContent.m_strKeepDrug_All;
                objDPArr[27].Value = objContent.m_strKeepDrugXML;
                objDPArr[28].Value = objContent.m_strKeepLiquid_All;
                objDPArr[29].Value = objContent.m_strKeepLiquidXML;
                objDPArr[30].Value = objContent.m_strRemark_All;
                objDPArr[31].Value = objContent.m_strRemarkXML;
                objDPArr[32].Value = objContent.m_strUnReusedDrug_All;
                objDPArr[33].Value = objContent.m_strUnReusedDrugXML;
                objDPArr[34].Value = objContent.m_strInducementDrug_All;
                objDPArr[35].Value = objContent.m_strInducementDrugXML;
                objDPArr[36].Value = objContent.m_strRestoreDrug_All;
                objDPArr[37].Value = objContent.m_strRestoreDrugXML;
                objDPArr[38].Value = objContent.m_strASALevel_All;


                for (int i1 = 0; i1 < objDPArr.Length; i1++)
                {
                    if (objDPArr[i1].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }
                }

                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(24, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strWeight;
                objDPArr2[6].Value = objContent.m_strBloodType;
                objDPArr2[7].Value = objContent.m_strDiagnose;
                objDPArr2[8].Value = objContent.m_strOperationName;
                objDPArr2[9].Value = objContent.m_strSpecialCase;
                objDPArr2[10].Value = objContent.m_strAnaMode;
                objDPArr2[11].Value = objContent.m_strObserveSelect;
                objDPArr2[12].Value = objContent.m_strAnaInducement;
                objDPArr2[13].Value = objContent.m_strASALevel;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    objDPArr2[14].Value = objContent.m_bolASAEmergency;
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    objDPArr2[14].Value = (objContent.m_bolASAEmergency ? 1 : 0);
                objDPArr2[15].Value = objContent.m_strKeepDrug;
                objDPArr2[16].Value = objContent.m_strKeepLiquid;
                objDPArr2[17].Value = objContent.m_strRemark;
                objDPArr2[18].Value = objContent.m_strUnReusedDrug;
                objDPArr2[19].Value = objContent.m_strInducementDrug;
                objDPArr2[20].Value = objContent.m_strRestoreDrug;
                objDPArr2[21].Value = objContent.m_strAnaOperationRoomID;
                objDPArr2[22].Value = objContent.m_strAnaOperationRoomName;
                objDPArr2[23].Value = objContent.m_strEquipmentModelNames;

                for (int i1 = 0; i1 < objDPArr2.Length; i1++)
                {
                    if (objDPArr2[i1].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }
                }
                //执行SQL
                string c_strAddNewRecordSQL = @"insert into anaesthesia_plan
			(inpatientid,inpatientdate,opendate,createdate,createuserid,
			ifconfirm,confirmreason,confirmreasonxml,status,weight_all,
			weightxml,bloodtype_all,bloodtypexml,diagnose_all,diagnosexml,
			operationname_all,operationnamexml,specialcase_all,specialcasexml,anamode_all,
			anamodexml,observeselect_all,observeselectxml,anainducement_all,anainducementxml,
			asalevelxml,keepdrug_all,
			keepdrugxml,keepliquid_all,keepliquidxml,remark_all,remarkxml,unreuseddrug_all,unreuseddrugxml,
			inducementdrug_all,inducementdrugxml,restoredrug_all,restoredrugxml,asalevel_all) values
			(
			?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?,?,
			?,?,?,?,?,?,?,?,?)";
                long lngEff = 0;
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //执行SQL
                string c_strAddNewRecordContentSQL = @"insert into anaesthesia_plancontent
			(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,weight,bloodtype,
			diagnose,operationname,specialcase,anamode,observeselect,anainducement,asalevel,
			asaemergency,keepdrug,keepliquid,remark,unreuseddrug,inducementdrug,restoredrug,
			anaoperationroomid,anaoperationroomname,equipmentmodelnames
			) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_objEquipmentArr != null)
                {
                    for (int i = 0; i < objContent.m_objEquipmentArr.Length; i++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPSvc.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;
                        objDPArr3[4].Value = objContent.m_objEquipmentArr[i].strParamID;

                        long lngEffect = 0;
                        string c_strAddEquipmentParamSQL = @"insert into anaesthesia_planmonparam values(?,?,?,?,?)";
                        long lngResult = p_objHRPServ.lngExecuteParameterSQL(c_strAddEquipmentParamSQL, ref lngEffect, objDPArr3);
                        if (lngResult <= 0) return lngResult;
                    }
                }

                lngRes = m_lngAddNewAnasthetist(p_objRecordContent, p_objHRPServ);
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
            }
        }



        /// <summary>
        /// 保存记录到数据库。麻醉医生子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewAnasthetist(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            string c_strAddAnaesthetistSQL = @"insert into anaesthesia_plananasthetist
			(inpatientid,inpatientdate,opendate,modifydate,chief_flag,anaesthetistid) 
			values(?,?,?,?,?,?)";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                clsAnaesthesia_PlanValue objContent = (clsAnaesthesia_PlanValue)p_objRecordContent;
                if (objContent.m_objAnasthetistArr == null || objContent.m_objAnasthetistArr.Length <= 0)
                    return (long)enmOperationResult.DB_Succeed;

                IDataParameter[] objDPArr = null;
                long lngEff = 0;
                long lngRes = 0;
                for (int i1 = 0; i1 < objContent.m_objAnasthetistArr.Length; i1++)
                {
                    //				objDPArr = new OdbcParameter[6];
                    //				//按顺序给IDataParameter赋值
                    //				for(int i=0;i<objDPArr.Length;i++)
                    //					objDPArr[i]=new OdbcParameter();
                    objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].Value = DateTime.Parse(objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[2].Value = DateTime.Parse(objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[3].Value = DateTime.Parse(objContent.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[4].Value = int.Parse(objContent.m_objAnasthetistArr[i1].m_strChiefFlag);
                    objDPArr[5].Value = objContent.m_objAnasthetistArr[i1].m_strAnaesthetistID;


                    //执行SQL
                    lngEff = 0;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddAnaesthetistSQL, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
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
                    c_strCheckLastModifyRecordSQL = @"select top 1 t2.modifydate,t2.modifyuserid from anaesthesia_plan as t1,anaesthesia_plancontent as t2
						where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
						and t1.opendate = t2.opendate and t1.status =0
						and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select t2.modifydate, t2.modifyuserid
          from anaesthesia_plancontent t2, anaesthesia_plan t1
         where t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t1.status = 0
           and trim(t1.inpatientid) = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?
         order by t2.modifydate desc) t3
 where rownum = 1";
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);
                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from anaesthesia_plan 
			where trim(inpatientid) = ? and inpatientdate= ? and opendate= ? and status=1 ";
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["deactivedoperatorid"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["deactiveddate"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["modifydate"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["modifyuserid"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["modifydate"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
                }
                return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return (long)enmOperationResult.DB_Fail;
            }
            finally
            {
                objHRPSvc.Dispose();
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
                clsAnaesthesia_PlanValue objContent = (clsAnaesthesia_PlanValue)p_objRecordContent;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(28, out objDPArr);

                objDPArr[0].Value = objContent.m_strWeight_All;
                objDPArr[1].Value = objContent.m_strWeightXML;
                objDPArr[2].Value = objContent.m_strBloodType_All;
                objDPArr[3].Value = objContent.m_strBloodTypeXML;
                objDPArr[4].Value = objContent.m_strDiagnose_All;
                objDPArr[5].Value = objContent.m_strDiagnoseXML;
                objDPArr[6].Value = objContent.m_strOperationName_All;
                objDPArr[7].Value = objContent.m_strOperationNameXML;
                objDPArr[8].Value = objContent.m_strSpecialCase_All;
                objDPArr[9].Value = objContent.m_strSpecialCaseXML;
                objDPArr[10].Value = objContent.m_strAnaMode_All;
                objDPArr[11].Value = objContent.m_strAnaModeXML;
                objDPArr[12].Value = objContent.m_strObserveSelect_All;
                objDPArr[13].Value = objContent.m_strObserveSelectXML;
                objDPArr[14].Value = objContent.m_strAnaInducement_All;
                objDPArr[15].Value = objContent.m_strAnaInducementXML;
                objDPArr[16].Value = objContent.m_strASALevelXML;
                objDPArr[17].Value = objContent.m_strKeepDrug_All;
                objDPArr[18].Value = objContent.m_strKeepDrugXML;
                objDPArr[19].Value = objContent.m_strKeepLiquid_All;
                objDPArr[20].Value = objContent.m_strKeepLiquidXML;
                objDPArr[21].Value = objContent.m_strRemark_All;
                objDPArr[22].Value = objContent.m_strRemarkXML;
                objDPArr[23].Value = objContent.m_strASALevel_All;
                objDPArr[24].Value = objContent.m_strInPatientID;
                objDPArr[25].Value = objContent.m_dtmInPatientDate;
                objDPArr[26].Value = objContent.m_dtmOpenDate;
                objDPArr[27].Value = 0;


                //			IDataParameter[] objDPArr2 = new OdbcParameter[24];
                //			//按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new OdbcParameter();
                IDataParameter[] objDPArr2 = null;
                objHRPSvc.CreateDatabaseParameter(24, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strWeight;
                objDPArr2[6].Value = objContent.m_strBloodType;
                objDPArr2[7].Value = objContent.m_strDiagnose;
                objDPArr2[8].Value = objContent.m_strOperationName;
                objDPArr2[9].Value = objContent.m_strSpecialCase;
                objDPArr2[10].Value = objContent.m_strAnaMode;
                objDPArr2[11].Value = objContent.m_strObserveSelect;
                objDPArr2[12].Value = objContent.m_strAnaInducement;
                objDPArr2[13].Value = objContent.m_strASALevel;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    objDPArr2[14].Value = Convert.ToInt32(objContent.m_bolASAEmergency);
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    objDPArr2[14].Value = objContent.m_bolASAEmergency ? 1 : 0;
                objDPArr2[15].Value = objContent.m_strKeepDrug;
                objDPArr2[16].Value = objContent.m_strKeepLiquid;
                objDPArr2[17].Value = objContent.m_strRemark;
                objDPArr2[18].Value = objContent.m_strUnReusedDrug;
                objDPArr2[19].Value = objContent.m_strInducementDrug;
                objDPArr2[20].Value = objContent.m_strRestoreDrug;
                objDPArr2[21].Value = objContent.m_strAnaOperationRoomID;
                objDPArr2[22].Value = objContent.m_strAnaOperationRoomName;
                objDPArr2[23].Value = objContent.m_strEquipmentModelNames;
                //执行SQL
                long lngEff = 0;
                string c_strModifyRecordSQL = @"update anaesthesia_plan set 
			weight_all=?,weightxml=?,bloodtype_all=?,bloodtypexml=?,diagnose_all=?,diagnosexml=?,
			operationname_all=?,operationnamexml=?,specialcase_all=?,specialcasexml=?,
			anamode_all=?,anamodexml=?,observeselect_all=?,observeselectxml=?,
			anainducement_all=?,anainducementxml=?,asalevelxml=?,keepdrug_all=?,keepdrugxml=?,
			keepliquid_all=?,keepliquidxml=?,remark_all=?,remarkxml=?,asalevel_all=?
			where trim(inpatientid)=? and inpatientdate=? and opendate=? and status=?";
                long lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                if (objContent.m_objEquipmentArr != null)
                {
                    for (int i = 0; i < objContent.m_objEquipmentArr.Length; i++)
                    {
                        //					IDataParameter[] objDPArr3 = new OdbcParameter[5];
                        //					//按顺序给IDataParameter赋值
                        //					for(int j=0;j<objDPArr3.Length;j++)
                        //						objDPArr3[j]=new OdbcParameter();
                        IDataParameter[] objDPArr3 = null;
                        objHRPSvc.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;
                        objDPArr3[4].Value = objContent.m_objEquipmentArr[i].strParamID;

                        string c_strAddEquipmentParamSQL = @"insert into anaesthesia_planmonparam values(?,?,?,?,?)";
                        long lngEffect = 0;
                        long lngResult = p_objHRPServ.lngExecuteParameterSQL(c_strAddEquipmentParamSQL, ref lngEffect, objDPArr3);
                        if (lngResult <= 0) return lngResult;
                    }
                }
                string c_strAddNewRecordContentSQL = @"insert into anaesthesia_plancontent
			(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,weight,bloodtype,
			diagnose,operationname,specialcase,anamode,observeselect,anainducement,asalevel,
			asaemergency,keepdrug,keepliquid,remark,unreuseddrug,inducementdrug,restoredrug,
			anaoperationroomid,anaoperationroomname,equipmentmodelnames
			) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                lngRes = m_lngAddNewAnasthetist(p_objRecordContent, p_objHRPServ);
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
            string c_strDeleteRecordSQL = @"update anaesthesia_plan set status=1,deactiveddate=?,deactivedoperatorid=? where trim(inpatientid)=? and inpatientdate=? and opendate=? and status=0";
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
            string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate,
			b.modifydate from anaesthesia_plan a,anaesthesia_plancontent b 
			where trim(a.inpatientid) = ? and a.inpatientdate= ? and a.opendate= ? 
			and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate 
			and b.opendate=a.opendate and
			b.modifydate=(select max(modifydate) from anaesthesia_plancontent 
			where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
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
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["firstprintdate"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["modifydate"].ToString());
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
            }
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
                string c_strGetDeleteRecordContentSQL = "";
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    c_strGetDeleteRecordContentSQL = @"select top 1 * from anaesthesia_plan as t1,
								anaesthesia_plancontent as t2 where	t1.inpatientid = ? and t1.inpatientdate =? and t1.opendate = ?
								and	t1.inpatientid =  t2.inpatientid and t1.inpatientdate = t2.inpatientdate and t1.opendate = t2.opendate
								and t1.status =1	order by t2.modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    c_strGetDeleteRecordContentSQL = @"select inpatientid,
         inpatientdate,
         opendate,
         createdate,
         createuserid,
         ifconfirm,
         confirmreason,
         confirmreasonxml,
         firstprintdate,
         deactiveddate,
         deactivedoperatorid,
         status,
         weight_all,
         weightxml,
         bloodtype_all,
         bloodtypexml,
         diagnose_all,
         diagnosexml,
         operationname_all,
         operationnamexml,
         specialcase_all,
         specialcasexml,
         anamode_all,
         anamodexml,
         observeselect_all,
         observeselectxml,
         anainducement_all,
         anainducementxml,
         asalevel_all,
         asalevelxml,
         asaemergency_all,
         asaemergencyxml,
         keepdrug_all,
         keepdrugxml,
         keepliquid_all,
         keepliquidxml,
         remark_all,
         remarkxml,
         unreuseddrug_all,
         unreuseddrugxml,
         inducementdrug_all,
         inducementdrugxml,
         restoredrug_all,
         restoredrugxml,
         modifydate,
                 modifyuserid,
                 weight,
                 bloodtype,
                 diagnose,
                 operationname,
                 specialcase,
                 anamode,
                 observeselect,
                 anainducement,
                 asalevel,
                 asaemergency,
                 keepdrug,
                 keepliquid,
                 remark,
                 unreuseddrug,
                 inducementdrug,
                 restoredrug,
                 anaoperationroomid,
                 anaoperationroomname,
                 equipmentmodelnames
  from (select t1.inpatientid,
                t1.inpatientdate,
                t1.opendate,
                t1.createdate,
                t1.createuserid,
                t1.ifconfirm,
                t1.confirmreason,
                t1.confirmreasonxml,
                t1.firstprintdate,
                t1.deactiveddate,
                t1.deactivedoperatorid,
                t1.status,
                t1.weight_all,
                t1.weightxml,
                t1.bloodtype_all,
                t1.bloodtypexml,
                t1.diagnose_all,
                t1.diagnosexml,
                t1.operationname_all,
                t1.operationnamexml,
                t1.specialcase_all,
                t1.specialcasexml,
                t1.anamode_all,
                t1.anamodexml,
                t1.observeselect_all,
                t1.observeselectxml,
                t1.anainducement_all,
                t1.anainducementxml,
                t1.asalevel_all,
                t1.asalevelxml,
                t1.asaemergency_all,
                t1.asaemergencyxml,
                t1.keepdrug_all,
                t1.keepdrugxml,
                t1.keepliquid_all,
                t1.keepliquidxml,
                t1.remark_all,
                t1.remarkxml,
                t1.unreuseddrug_all,
                t1.unreuseddrugxml,
                t1.inducementdrug_all,
                t1.inducementdrugxml,
                t1.restoredrug_all,
                t1.restoredrugxml,                
                t2.modifydate,
                t2.modifyuserid,
                t2.weight,
                t2.bloodtype,
                t2.diagnose,
                t2.operationname,
                t2.specialcase,
                t2.anamode,
                t2.observeselect,
                t2.anainducement,
                t2.asalevel,
                t2.asaemergency,
                t2.keepdrug,
                t2.keepliquid,
                t2.remark,
                t2.unreuseddrug,
                t2.inducementdrug,
                t2.restoredrug,
                t2.anaoperationroomid,
                t2.anaoperationroomname,
                t2.equipmentmodelnames
           from anaesthesia_plan t1, anaesthesia_plancontent t2
          where trim(t1.inpatientid) = ?
          and t1.inpatientdate = ?
          and t1.opendate = ? and
          t1.inpatientid = t2.inpatientid
       and t1.inpatientdate = t2.inpatientdate
       and t1.opendate = t2.opendate
       and t1.status = 1
          order by t2.modifydate desc)
 where rownum = 1";
                long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsAnaesthesia_PlanValue objRecordContent = new clsAnaesthesia_PlanValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CreateDate"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["ModifyDate"].ToString());

                    if (dtbValue.Rows[0]["FirstPrintDate"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FirstPrintDate"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CreateUserID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["ModifyUserID"].ToString();
                    if (dtbValue.Rows[0]["IfConfirm"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IfConfirm"].ToString());
                    if (dtbValue.Rows[0]["Status"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["Status"].ToString());


                    objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["ConfirmReason"].ToString();
                    objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["ConfirmReasonXML"].ToString();

                    objRecordContent.m_strWeight_All = dtbValue.Rows[0]["Weight_All"].ToString();
                    objRecordContent.m_strWeightXML = dtbValue.Rows[0]["WeightXML"].ToString();
                    objRecordContent.m_strBloodType_All = dtbValue.Rows[0]["BloodType_All"].ToString();
                    objRecordContent.m_strBloodTypeXML = dtbValue.Rows[0]["BloodTypeXML"].ToString();
                    objRecordContent.m_strDiagnose_All = dtbValue.Rows[0]["Diagnose_All"].ToString();
                    objRecordContent.m_strDiagnoseXML = dtbValue.Rows[0]["DiagnoseXML"].ToString();
                    objRecordContent.m_strOperationName_All = dtbValue.Rows[0]["OperationName_All"].ToString();
                    objRecordContent.m_strOperationNameXML = dtbValue.Rows[0]["OperationNameXML"].ToString();
                    objRecordContent.m_strSpecialCase_All = dtbValue.Rows[0]["SpecialCase_All"].ToString();
                    objRecordContent.m_strSpecialCaseXML = dtbValue.Rows[0]["SpecialCaseXML"].ToString();
                    objRecordContent.m_strAnaMode_All = dtbValue.Rows[0]["AnaMode_All"].ToString();
                    objRecordContent.m_strAnaModeXML = dtbValue.Rows[0]["AnaModeXML"].ToString();
                    objRecordContent.m_strObserveSelect_All = dtbValue.Rows[0]["ObserveSelect_All"].ToString();
                    objRecordContent.m_strObserveSelectXML = dtbValue.Rows[0]["ObserveSelectXML"].ToString();
                    objRecordContent.m_strAnaInducement_All = dtbValue.Rows[0]["AnaInducement_All"].ToString();
                    objRecordContent.m_strAnaInducementXML = dtbValue.Rows[0]["AnaInducementXML"].ToString();
                    objRecordContent.m_strASALevel_All = dtbValue.Rows[0]["ASALevel_All"].ToString();
                    objRecordContent.m_strASALevelXML = dtbValue.Rows[0]["ASALevelXML"].ToString();
                    objRecordContent.m_strASAEmergency_All = dtbValue.Rows[0]["ASAEmergency_All"].ToString();
                    objRecordContent.m_strASAEmergencyXML = dtbValue.Rows[0]["ASAEmergencyXML"].ToString();
                    objRecordContent.m_strKeepDrug_All = dtbValue.Rows[0]["KeepDrug_All"].ToString();
                    objRecordContent.m_strKeepDrugXML = dtbValue.Rows[0]["KeepDrugXML"].ToString();
                    objRecordContent.m_strKeepLiquid_All = dtbValue.Rows[0]["KeepLiquid_All"].ToString();
                    objRecordContent.m_strKeepLiquidXML = dtbValue.Rows[0]["KeepLiquidXML"].ToString();
                    objRecordContent.m_strRemark_All = dtbValue.Rows[0]["Remark_All"].ToString();
                    objRecordContent.m_strRemarkXML = dtbValue.Rows[0]["RemarkXML"].ToString();


                    objRecordContent.m_strWeight = dtbValue.Rows[0]["Weight"].ToString();
                    objRecordContent.m_strBloodType = dtbValue.Rows[0]["BloodType"].ToString();
                    objRecordContent.m_strDiagnose = dtbValue.Rows[0]["Diagnose"].ToString();
                    objRecordContent.m_strOperationName = dtbValue.Rows[0]["OperationName"].ToString();
                    objRecordContent.m_strSpecialCase = dtbValue.Rows[0]["SpecialCase"].ToString();
                    objRecordContent.m_strAnaMode = dtbValue.Rows[0]["AnaMode"].ToString();
                    objRecordContent.m_strObserveSelect = dtbValue.Rows[0]["ObserveSelect"].ToString();
                    objRecordContent.m_strAnaInducement = dtbValue.Rows[0]["AnaInducement"].ToString();
                    objRecordContent.m_strASALevel = dtbValue.Rows[0]["ASALevel"].ToString();
                    if (clsHRPTableService.bytDatabase_Selector == 0)
                        objRecordContent.m_bolASAEmergency = Convert.ToBoolean(dtbValue.Rows[0]["ASAEmergency"]);
                    else if (clsHRPTableService.bytDatabase_Selector == 2)
                        objRecordContent.m_bolASAEmergency = (dtbValue.Rows[0]["ASAEMERGENCY"] != DBNull.Value && Convert.ToInt32(dtbValue.Rows[0]["ASAEMERGENCY"]) == 1 ? true : false);
                    objRecordContent.m_strKeepDrug = dtbValue.Rows[0]["KeepDrug"].ToString();
                    objRecordContent.m_strKeepLiquid = dtbValue.Rows[0]["KeepLiquid"].ToString();
                    objRecordContent.m_strRemark = dtbValue.Rows[0]["Remark"].ToString();

                    p_objRecordContent = objRecordContent;
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
            }
        }

        public long m_lngGetTest()
        {
            return 1;
        }

        [AutoComplete]
        public string m_strGetEmployeeName(string p_strEmployeeID)
        {
            string strTmp = "";
            if (p_strEmployeeID == null)
                return strTmp;

            if (p_strEmployeeID.Trim().Length == 0)
                return strTmp;

            string SQL = "select lastname_vchr from t_bse_employee where empid_chr  = ? and status_int = 1";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            DataTable dtResult = null;
            try
            {
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objArr);
                objArr[0].Value = p_strEmployeeID;
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtResult, objArr);

                // objHRPSvc.DoGetDataTable(SQL, ref dt);

                if (dtResult == null)
                    return strTmp;
                if (dtResult.Rows.Count > 0)
                    strTmp = dtResult.Rows[0]["lastname_vchr"].ToString();
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return strTmp;
        }

    }
}
