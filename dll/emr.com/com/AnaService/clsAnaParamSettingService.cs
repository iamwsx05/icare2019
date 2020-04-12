using System;
using weCare.Core.Entity;
using System.Data;
using System.EnterpriseServices; 
using com.digitalwave.iCare.middletier.HRPService; 
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.iCare.middletier.Anaesthesia
{
	/// <summary>
	/// Summary description for clsAnaParamSettingService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]	 
	public class clsAnaParamSettingService : clsDiseaseTrackService
	{
		#region strSql Statement

//        private const string c_strAddPatientItemSQL = @"insert into ana_collectsetting(inpatientid,inpatientdate,opendate,createdate,createuserid,status,ifconfirm
//														,roomid,roomname,monitorname_models)
//														values(?,?,?,?,?,?,?,?,?,?)";


		/// <summary>
		/// 选择单次采集中的所有麻醉事件
		/// </summary>
//        private const string c_strModifyEventSQL = @"select * from v_ana_collection_eventcontent where trim(inpatientid) = ? and 
//														inpatientdate = ? and opendate = ? order by operate_date";

		/// <summary>
		/// 选择采集中的collectSetting记录
		/// </summary>
//        private const string c_strGetCollectSettingSQL = @"select * from ana_collectsetting where trim(inpatientid) = ? and 
//														inpatientdate = ? and opendate = ?";

		/// <summary>
		/// 获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
        //private const string c_strGetTimeListSQL= "select createdate,opendate from ana_collectsetting where trim(inpatientid) = ? and inpatientdate= ? and status=0";

		/// <summary>
		/// 获取用药
		/// </summary>
//        private const string c_strGetDrugSQL = @"select * from v_ana_collection_singledrugcontent where trim(inpatientid) = ? and 
//														inpatientdate = ? and opendate = ? order by operate_date";

		/// <summary>
		/// 获取医生
		/// </summary>
//        private const string c_strGetDoctorsSQL = @"select * from v_relation_anaesthetist where trim(inpatientid) = ? and 
//														inpatientdate = ? and opendate = ?";

		/// <summary>
		/// 获取术中采集数据
		/// </summary>
        //private const string c_strGetCollectionSQL = @"select * from ana_collection where trim(inpatientid) = ? and inpatientdate = ? and opendate = ?";

		/// <summary>
		/// 获取术中采集参数记录
		/// </summary>
		private const string c_strGetCollectionPatamSQL = @"select * from v_relation_collectioneqpparam where trim(inpatientid) = ? and inpatientdate = ? and opendate = ?";


		/// <summary>
		/// 获取麻醉计划中的手术诊断
		/// </summary>
		private const string c_strGetDiagnoseSQL = @"select top 1 * from anaesthesia_plancontent 
														where inpatientid = ? and inpatientdate = ? and opendate < ? 
														order by opendate desc,modifydate desc";

		/// <summary>
		/// 获取麻醉计划中的医生内容
		/// </summary>
		private const string c_strGetDoctorsFromPlanSQL = @"SELECT M2.*, M1.FirstName FROM EmployeeBaseInfo AS M1,
											(SELECT V1.AnaesthetistID,V1.Chief_Flag,V1.OpenDate,
											Max(V1.ModifyDate) AS ModifyDate
											FROM Anaesthesia_PlanAnasthetist AS V1,
											(SELECT TOP 1 InPatientID,InPatientDate,OpenDate, ModifyDate FROM Anaesthesia_PlanContent 
											WHERE InPatientID = ? AND InPatientDate = ?
											AND OpenDate < ?
											ORDER BY OpenDate DESC,ModifyDate DESC) AS T1
											WHERE T1.InPatientID = V1.InPatientID 
											AND T1.InPatientDate = V1.InPatientDate
											AND T1.OpenDate = V1.OpenDate
											GROUP BY V1.AnaesthetistID,V1.Chief_Flag,V1.OpenDate) AS M2
											WHERE M1.EmployeeID = M2.AnaesthetistID";

		#endregion

		/// <summary>
		/// 添加术中采集设置
		/// </summary>
		/// <param name="p_objPatient"></param>
		/// <param name="p_arlSaveItem"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSavePatientSettingService(clsAna_CollectSettingValue p_objAnaCollectSetting)
		{
            string c_strAddPatientItemSQL = @"insert into ana_collectsetting(inpatientid,inpatientdate,opendate,createdate,createuserid,status,ifconfirm	,roomid,roomname,monitorname_models)	values(?,?,?,?,?,?,?,?,?,?)";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(10, out objDPArr);

                objDPArr[0].Value = p_objAnaCollectSetting.m_strInPatientID;//p_arlSaveItem[0];
                objDPArr[1].Value = p_objAnaCollectSetting.m_dtmInPatientDate;//p_arlSaveItem[1];
                objDPArr[2].Value = p_objAnaCollectSetting.m_dtmOpenDate;//p_arlSaveItem[2];
                objDPArr[3].Value = p_objAnaCollectSetting.m_dtmCreateDate;//p_arlSaveItem[3];
                objDPArr[4].Value = p_objAnaCollectSetting.m_strCreateUserID;//p_arlSaveItem[4];
                objDPArr[5].Value = p_objAnaCollectSetting.m_bytStatus;
                objDPArr[6].Value = p_objAnaCollectSetting.m_bytStatus;

                objDPArr[7].Value = p_objAnaCollectSetting.m_strRoomID;//p_arlSaveItem[4];
                objDPArr[8].Value = p_objAnaCollectSetting.m_strRoomName;
                objDPArr[9].Value = p_objAnaCollectSetting.m_strMonitorName_Models;

                DataTable dtResult = new DataTable();
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strAddPatientItemSQL, ref dtResult, objDPArr);
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
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(   string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr, out string[] p_strOpenDateArr)
        {
            string c_strGetTimeListSQL = "select createdate,opendate from ana_collectsetting where trim(inpatientid) = ? and inpatientdate= ? and status=0";
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
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
                                                  deactivedoperatorid,
                                                  status,
                                                  finishdate,
                                                  roomid,
                                                  roomname,
                                                  monitorname_models from ana_collectsetting where trim(inpatientid) = ? and 
													inpatientdate = ? and opendate = ?";
            p_objRecordContent = null;
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
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetCollectSettingSQL, ref p_dtbResult, objDPArr);

                if (lngRes > 0 && p_dtbResult.Rows.Count == 1)
                {
                    //设置结果
                    clsAna_CollectSettingValue m_objCollectSetting = new clsAna_CollectSettingValue();
                    System.Data.DataRow objDataRow = p_dtbResult.Rows[0];
                    m_objCollectSetting.m_strInPatientID = objDataRow["inpatientid"].ToString().Trim();
                    m_objCollectSetting.m_dtmInPatientDate = Convert.ToDateTime(objDataRow["inpatientdate"]);
                    m_objCollectSetting.m_dtmOpenDate = Convert.ToDateTime(objDataRow["opendate"]);
                    m_objCollectSetting.m_dtmCreateDate = Convert.ToDateTime(objDataRow["createdate"]);
                    m_objCollectSetting.m_strCreateUserID = objDataRow["createuserid"].ToString().Trim();

                    m_objCollectSetting.m_strRoomID = objDataRow["roomid"].ToString().Trim();
                    m_objCollectSetting.m_strRoomName = objDataRow["roomname"].ToString().Trim();
                    m_objCollectSetting.m_strMonitorName_Models = objDataRow["monitorname_models"].ToString().Trim();

                    if (objDataRow["ifconfirm"].ToString() == "")
                        m_objCollectSetting.m_bytIfConfirm = 0;
                    else
                        m_objCollectSetting.m_bytIfConfirm = Convert.ToByte(objDataRow["ifconfirm"]);
                    m_objCollectSetting.m_strConfirmReason = objDataRow["confirmreason"].ToString();
                    m_objCollectSetting.m_strConfirmReasonXML = objDataRow["confirmreasonxml"].ToString();

                    if (objDataRow["firstprintdate"].ToString() == "")
                        m_objCollectSetting.m_dtmFirstPrintDate = DateTime.MinValue;
                    else
                        m_objCollectSetting.m_dtmFirstPrintDate = Convert.ToDateTime(objDataRow["firstprintdate"].ToString());
                    if (objDataRow["deactiveddate"].ToString() == "")
                        m_objCollectSetting.m_dtmDeActivedDate = DateTime.MinValue;
                    else
                        m_objCollectSetting.m_dtmDeActivedDate = Convert.ToDateTime(objDataRow["deactiveddate"].ToString());

                    m_objCollectSetting.m_strDeActivedOperatorID = objDataRow["deactivedoperatorid"].ToString();
                    if (objDataRow["ifconfirm"].ToString() == "")
                        m_objCollectSetting.m_bytStatus = 0;
                    else
                        m_objCollectSetting.m_bytStatus = Convert.ToByte(objDataRow["status"]);

                    p_objRecordContent = m_objCollectSetting;
                    
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

		[AutoComplete]
		protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objPreModifyInfo)
		{
			p_objPreModifyInfo = null;
			return 1;
		}

		[AutoComplete]
		protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			return 1;
		}

		[AutoComplete]
		protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,			
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null;
			return 1;
		}

		[AutoComplete]
		protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			return 1;
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
			return 1;
		}

		[AutoComplete]
		public override long m_lngUpdateFirstPrintDate( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			DateTime p_dtmFirstPrintDate)
		{
			return 1;
		}

		[AutoComplete]
		public override long m_lngGetDeleteRecordTimeList( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strDeleteUserID,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr = null;
			p_strOpenRecordTimeArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //string strSql = @"select createdate,opendate from ana_collectsetting where inpatientid = '" + p_strInPatientID + "' and inpatientdate= " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " and status='1'";
                string strSql = @"select createdate,opendate from ana_collectsetting where inpatientid = ? and inpatientdate= ? and status='1'";
                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objArr);
                objArr[0].Value = p_strInPatientID;
                objArr[1].DbType = DbType.DateTime;
                objArr[1].Value = DateTime.Parse(p_strInPatientDate);
                DataTable dtbValue = new DataTable();
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref dtbValue, objArr);
               // long lngRes = objHRPSvc.DoGetDataTable(strSql, ref dtbValue);
                //从DataTable.Rows中获取结果
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateRecordTimeArr = new string[intRowCount];
                    p_strOpenRecordTimeArr = new string[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        //设置结果
                        System.Data.DataRow objDataRow = dtbValue.Rows[i];
                        p_strCreateRecordTimeArr[i] = DateTime.Parse(objDataRow["CreateDate"].ToString()).ToString();
                        p_strOpenRecordTimeArr[i] = DateTime.Parse(objDataRow["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

		[AutoComplete]
		public override long m_lngGetDeleteRecordTimeListAll( string p_strInPatientID,
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

		/// <summary>
		/// 恢复已经结束的麻醉记录单为可重新继续采集的麻醉记录单
		/// </summary>
		/// <param name="p_objCollection"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngRenewCollectionService(clsAna_CollectionValue p_objCollection)
		{
			long lngRet =0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                p_objCollection.m_dtmOpenDate = DateTime.Parse(p_objCollection.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (p_objCollection != null)
                {
                    //string strSQL=@"update Ana_Collection set CollectionEndDate = null " + 
                    //    "where InPatientID='" + p_objCollection.m_strInPatientID + "' " +
                    //    "and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objCollection.m_dtmInPatientDate.ToString()) + " " +
                    //    "and OpenDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objCollection.m_dtmOpenDate.ToString()) + "";
                    string strSQL = @"update Ana_Collection set CollectionEndDate = null where InPatientID=? and InPatientDate=? and OpenDate=?";
                    System.Data.IDataParameter[] objArr = null;
                    objHRPSvc.CreateDatabaseParameter(3, out objArr);
                    objArr[0].DbType = DbType.String;
                    objArr[0].Value = p_objCollection.m_strInPatientID;
                    objArr[1].DbType = DbType.DateTime;
                    objArr[1].Value = DateTime.Parse(p_objCollection.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objArr[2].DbType = DbType.DateTime;
                    objArr[2].Value = p_objCollection.m_dtmOpenDate;
                    long lngEffict = -1;
                    lngRet = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
                    // lngRet = objHRPSvc.DoExcute(strSQL);
                    if (lngRet <= 0 || lngEffict < 0)
                    {
                        lngRet = -1;
                        return lngRet;
                    }

                    clsAnaParamSettingService objSettingSvc = new clsAnaParamSettingService();
                    string dtmEndDate;
                    lngRet = objSettingSvc.m_lngGetAnaCollectionEndDate(p_objCollection, out dtmEndDate);
                    DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-3);
                    if (string.IsNullOrEmpty(dtmEndDate))
                    {
                        dtmEndDate = "1900-1-1 0:0:0";
                        return lngRet;
                    }
                    if (!(Convert.ToDateTime(dtmEndDate) < temp))
                    {
                        return lngRet;
                    }

                    //if (clsHRPTableService.bytDatabase_Selector == 0)
                    //    strSQL = "insert into hrpgzno1_data.dbo.ana_collectioneqpparam select * from hrpgzno1_data.dbo.ana_collectioneqpparamhistory where InPatientID = ? and InPatientDate=? and OpenDate=?";
                    //else if (clsHRPTableService.bytDatabase_Selector == 2)
                    //{
                    //    strSQL = "delete from Ana_CollectionEqpParam where InPatientID = ? and InPatientDate=? and OpenDate=?";
                    //    //lngRet = objHRPSvc.DoExcute(strSQL);
                    //    objArr = null;
                    //    objHRPSvc.CreateDatabaseParameter(3, out objArr);
                    //    objArr[0].DbType = DbType.String;
                    //    objArr[0].Value = p_objCollection.m_strInPatientID;
                    //    objArr[1].DbType = DbType.DateTime;
                    //    objArr[1].Value = DateTime.Parse(p_objCollection.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    //    objArr[2].DbType = DbType.DateTime;
                    //    objArr[2].Value = p_objCollection.m_dtmOpenDate; 
                    //    lngEffict = -1;
                    //    lngRet = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
                    //    if (lngRet <= 0 || lngEffict < 0)
                    //    {
                    //        lngRet = -1;
                    //        return lngRet;
                    //    }
                    //}
                    //    strSQL = "insert into Ana_CollectionEqpParam select * from Ana_CollectionEqpParamHistory where InPatientID = ? and InPatientDate=? and OpenDate=?";
                    
                    //objArr = null;
                    //objHRPSvc.CreateDatabaseParameter(3, out objArr);
                    //objArr[0].DbType = DbType.String;
                    //objArr[0].Value = p_objCollection.m_strInPatientID;
                    //objArr[1].DbType = DbType.DateTime;
                    //objArr[1].Value = DateTime.Parse(p_objCollection.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    //objArr[2].DbType = DbType.DateTime;
                    //objArr[2].Value = p_objCollection.m_dtmOpenDate;
                    //lngEffict = -1;
                    //lngRet = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
                    //if (lngRet <= 0 || lngEffict < 0)
                    //{
                    //    lngRet = -1;
                    //    return lngRet;
                    //}
                    //// lngRet = objHRPSvc.DoExcute(strSQL);

                    //if (lngRet < 0)
                    //    return lngRet;

                    //if (clsHRPTableService.bytDatabase_Selector == 0)
                    //    strSQL = "delete from HRPGZNO1_DATA.dbo.Ana_CollectionEqpParamHistory where InPatientID = ? and InPatientDate=? and OpenDate=?";
                    //else if (clsHRPTableService.bytDatabase_Selector == 2)
                    //    strSQL = "delete from Ana_CollectionEqpParamHistory where InPatientID = ? and InPatientDate=? and OpenDate=?";
                    //objArr = null;
                    //objHRPSvc.CreateDatabaseParameter(3, out objArr);
                    //objArr[0].DbType = DbType.String;
                    //objArr[0].Value = p_objCollection.m_strInPatientID;
                    //objArr[1].DbType = DbType.DateTime;
                    //objArr[1].Value = DateTime.Parse(p_objCollection.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    //objArr[2].DbType = DbType.DateTime;
                    //objArr[2].Value = p_objCollection.m_dtmOpenDate;
                    //lngEffict = -1;
                    //lngRet = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEffict, objArr);
                    //if (lngRet <= 0 || lngEffict < 0)
                    //{
                    //    lngRet = -1;
                    //    return lngRet;
                    //}
                    // lngRet = objHRPSvc.DoExcute(strSQL);
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
            //objHRPSvc.Dispose();
			return lngRet;
		}

		#region GetCollectionEndDate	(获取手术结束时间)
		
		private const string c_strGetCollectionEndDateSql=@"
			select A.*,B.CollectionEndDate
			from Ana_CollectSetting A
			left join Ana_Collection B
			on A.InPatientID=B.InPatientID and A.InPatientDate=B.InPatientDate and A.OpenDate=B.OpenDate
			where trim(A.InPatientID)=? and A.InPatientDate=? and A.OpenDate=? 
			";
		//获取手术结束时间

		[AutoComplete]
		public long m_lngGetAnaCollectionEndDate(clsAna_CollectionValue objCollection,out string strEndDate)
		{
			strEndDate="";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                string strSql = c_strGetCollectionEndDateSql;

                //			IDataParameter[] objParams=new OdbcParameter[3];
                //			for(int i=0;i<objParams.Length;i++) objParams[i]=new OdbcParameter();
                IDataParameter[] objParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out objParams);

                objParams[0].Value = objCollection.m_strInPatientID;
                objParams[1].Value = objCollection.m_dtmInPatientDate;
                objParams[2].Value = objCollection.m_dtmOpenDate;

                DataTable objDT = new DataTable();
                long ret = objHRPSvc.lngGetDataTableWithParameters(strSql, ref objDT, objParams);
                if ((ret > 0) && (objDT.Rows.Count > 0))
                {
                    if (objDT.Rows[0]["CollectionEndDate"] == DBNull.Value)
                    {
                        strEndDate = "";
                    }
                    else
                    {
                        strEndDate = ((DateTime)(objDT.Rows[0]["CollectionEndDate"])).ToString("yyyy-MM-dd HH:mm:ss");
                    }
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
                //objHRPSvc.Dispose();
            }
		}
		#endregion

		/// <summary>
		/// 删除或恢复记录
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_intStatus"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSetRecordStatus(clsTrackRecordContent p_objRecordContent,int p_intStatus)
		{
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_objRecordContent == null) return -1;
                string strSql = "";

                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objArr);
                objArr[0].DbType = DbType.String;
                objArr[0].Value = p_intStatus;
                objArr[1].DbType = DbType.DateTime;
                objArr[1].Value = System.DateTime.Now;
                objArr[2].DbType = DbType.String;
                objArr[2].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objArr[3].DbType = DbType.String;
                objArr[3].Value = p_objRecordContent.m_strInPatientID;
                objArr[4].DbType = DbType.DateTime;
                objArr[4].Value = p_objRecordContent.m_dtmInPatientDate;
                objArr[5].DbType = DbType.DateTime;
                objArr[5].Value = p_objRecordContent.m_dtmCreateDate;

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strSql = @"update ana_collectsetting set status=?,deactiveddate = ?,deactivedoperatorid = ? where inpatientid = ? and inpatientdate= ? and createdate =?";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    strSql = @"update ana_collectsetting set status=?,deactiveddate = ?,deactivedoperatorid = ? where trim(inpatientid) = ? and inpatientdate= ? and createdate = ?";

                long lngEffict = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngEffict, objArr);
                // lngRet = objHRPSvc.DoExcute(strSQL);
                if (lngRes <= 0 || lngEffict < 0)
                {
                    lngRes = -1;
                   
                }
               // return objHRPSvc.DoExcute(strSql);
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
        /// 修改手术开始时间
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_dtmNewStartTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOperationStartTime(clsTrackRecordContent p_objRecordContent, DateTime p_dtmNewStartTime)
        {
            long lngRes = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                if (p_objRecordContent == null) return -1;
                string strSql = "";

                System.Data.IDataParameter[] objArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objArr);
                objArr[0].Value = p_dtmNewStartTime;
                objArr[1].Value = p_objRecordContent.m_strInPatientID;
                objArr[2].Value = p_objRecordContent.m_dtmInPatientDate;
                objArr[3].Value = p_objRecordContent.m_dtmOpenDate;

                strSql = @"update ana_collectsetting set createdate = ? where inpatientid = ? and inpatientdate= ? and opendate = ?";

                long lngEffict = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngEffict, objArr);
                if (lngRes <= 0 || lngEffict < 0)
                {
                    lngRes = -1;

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
    }
}
