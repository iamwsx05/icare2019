using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{
	/// <summary>
	/// 首次病程记录(市一中医科)
	/// </summary>
	// 实现特殊记录的中间件。
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsFirstIllnessNote_ZYService : clsDiseaseTrackService
	{
		// 从FIRSTILLNESSNOTERECORD_GZZY获取指定病人的所有没有删除记录的时间。
		// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		private const string c_strGetTimeListSQL="select CreateDate,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and Status=0";


		// 根据指定表单的信息，从FIRSTILLNESSNOTERECORD_GZZY和FIRSTILLNESSNOTERECORDCON_GZZY查找表单的内容。
		// 用InPatientID ,InPatientDate ,CreateDate,Status = 0等条件，查询该记录的内容，查找Max(ModifyDate)。
		// 如果返回lngRes = 1 && rows = 0,则证明此记录已被他人删除掉。

		private const string c_strGetRecordContentSQL=@"select a.*,b.* from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		// 从FIRSTILLNESSNOTERECORD_GZZY中获取指定时间的表单。
		// InPatientID ,InPatientDate ,CreateDate,Status = 0
		private const string c_strCheckCreateDateSQL="select CreateUserID,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		// 从FIRSTILLNESSNOTERECORDCON_GZZY获取指定表单的最后修改时间。
		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate,b.ModifyUserID from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		// 从FIRSTILLNESSNOTERECORD_GZZY获取删除表单的主要信息。
		private const string c_strGetDeleteRecordSQL="select DeActivedDate,DeActivedOperatorID from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";


		// 添加记录到FIRSTILLNESSNOTERECORD_GZZY
		private const string c_strAddNewRecordSQL= @"insert into  FIRSTILLNESSNOTERECORD_GZZY(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,
		IfConfirm,ConfirmReason,ConfirmReasonXML,Status,MostlyContent,MostlyContentXML,OriginalDiagnose,OriginalDiagnoseXML,ThereunderDiagnose,ThereunderDiagnoseXML,DiagnoseDiffe,DiagnoseDiffeXML,CurePlan,CurePlanXML,IDENTIFYRESTON,IDENTIFYRESTONXML,IDENTIFYDIAGNOSE,IDENTIFYDIAGNOSEXML) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// 添加记录到FIRSTILLNESSNOTERECORDCON_GZZY
		private const string c_strAddNewRecordContentSQL=@"insert into  FIRSTILLNESSNOTERECORDCON_GZZY(InPatientID,InPatientDate,OpenDate,ModifyDate,ModifyUserID,MostlyContent_Right,OriginalDiagnose_Right,ThereunderDiagnose_Right,DiagnoseDiffe_Right,CurePlan_Right,IDENTIFYRESTON_RIGHT,IDENTIFYDIAGNOSE_RIGHT) 
				values(?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到FIRSTILLNESSNOTERECORD_GZZY
		/// </summary>
		private const string c_strModifyRecordSQL= "Update FIRSTILLNESSNOTERECORD_GZZY Set MostlyContent=?,MostlyContentXML=? ,OriginalDiagnose=?,OriginalDiagnoseXML=? ,ThereunderDiagnose=?,ThereunderDiagnoseXML=? ,DiagnoseDiffe=?,DiagnoseDiffeXML=?,CurePlan=?,CurePlanXML=?,IDENTIFYRESTON=?,IDENTIFYRESTONXML=?,IDENTIFYDIAGNOSE=?,IDENTIFYDIAGNOSEXML=? where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

		// 修改记录到FIRSTILLNESSNOTERECORDCON_GZZY
		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;

		// 设置FIRSTILLNESSNOTERECORD_GZZY中删除记录的信息
		private const string c_strDeleteRecordSQL="Update FIRSTILLNESSNOTERECORD_GZZY Set Status=1,DeActivedDate=?,DeActivedOperatorID=? where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";


		// 从FIRSTILLNESSNOTERECORD_GZZY和FIRSTILLNESSNOTERECORDCON_GZZY获取LastModifyDate和FirstPrintDate
		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"select a.FirstPrintDate,b.ModifyDate from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";


		// 更新FIRSTILLNESSNOTERECORD_GZZY中FirstPrintDate
		private const string c_strUpdateFirstPrintDateSQL="Update  FIRSTILLNESSNOTERECORD_GZZY Set FirstPrintDate= ? where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";


		// 从FIRSTILLNESSNOTERECORD_GZZY获取指定病人的所有指定删除者删除的记录时间。
		// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		private const string c_strGetDeleteRecordTimeListSQL="select CreateDate,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";


		// 从FIRSTILLNESSNOTERECORD_GZZY获取指定病人的所有已经删除的记录时间。
		// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		private const string c_strGetDeleteRecordTimeListAllSQL="select CreateDate,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and Status=1";


		// 在交班记录所有表中获取指定表单的信息。
		private const string c_strGetDeleteRecordContentSQL=@"select a.*,b.* from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=1 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		

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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


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
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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
			//返回
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

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
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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
			//返回
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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

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
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OpenDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

            }			//返回
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
			p_objRecordContent=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
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
                    //设置结果
                    clsFirstIllnessNote_ZYRecordContent objRecordContent = new clsFirstIllnessNote_ZYRecordContent();
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

                    objRecordContent.m_strMostlyContent_Right = dtbValue.Rows[0]["MostlyContent_Right"].ToString();
                    objRecordContent.m_strMostlyContent = dtbValue.Rows[0]["MostlyContent"].ToString();
                    objRecordContent.m_strMostlyContentXML = dtbValue.Rows[0]["MostlyContentXML"].ToString();

                    objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[0]["OriginalDiagnose_Right"].ToString();
                    objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[0]["OriginalDiagnose"].ToString();
                    objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[0]["OriginalDiagnoseXML"].ToString();

                    objRecordContent.m_strThereunderDiagnose_Right = dtbValue.Rows[0]["ThereunderDiagnose_Right"].ToString();
                    objRecordContent.m_strThereunderDiagnose = dtbValue.Rows[0]["ThereunderDiagnose"].ToString();
                    objRecordContent.m_strThereunderDiagnoseXML = dtbValue.Rows[0]["ThereunderDiagnoseXML"].ToString();

                    objRecordContent.m_strDiagnoseDiffe_Right = dtbValue.Rows[0]["DiagnoseDiffe_Right"].ToString();
                    objRecordContent.m_strDiagnoseDiffe = dtbValue.Rows[0]["DiagnoseDiffe"].ToString();
                    objRecordContent.m_strDiagnoseDiffeXML = dtbValue.Rows[0]["DiagnoseDiffeXML"].ToString();

                    objRecordContent.m_strCurePlan_Right = dtbValue.Rows[0]["CurePlan_Right"].ToString();
                    objRecordContent.m_strCurePlan = dtbValue.Rows[0]["CurePlan"].ToString();
                    objRecordContent.m_strCurePlanXML = dtbValue.Rows[0]["CurePlanXML"].ToString();

                    objRecordContent.m_strIdentifyDiagnose_Right = dtbValue.Rows[0]["IDENTIFYDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strIdentifyDiagnos = dtbValue.Rows[0]["IDENTIFYDIAGNOSE"].ToString();
                    objRecordContent.m_strIdentifyDiagnoseXML = dtbValue.Rows[0]["IDENTIFYDIAGNOSEXML"].ToString();

                    objRecordContent.m_strIdentifyReston_Right = dtbValue.Rows[0]["IDENTIFYRESTON_RIGHT"].ToString();
                    objRecordContent.m_strIdentifyReston = dtbValue.Rows[0]["IDENTIFYRESTON"].ToString();
                    objRecordContent.m_strIdentifyRestonXML = dtbValue.Rows[0]["IDENTIFYRESTONXML"].ToString();

                    p_objRecordContent = objRecordContent;
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

            }			//返回
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
			p_objModifyInfo=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

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
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CreateUserID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OpenDate"].ToString());
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
                //objHRPServ.Dispose();

            }			//返回
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                //clsDiseaseSummaryRecordContent objContent = (clsDiseaseSummaryRecordContent)p_objRecordContent;
                clsFirstIllnessNote_ZYRecordContent objContent = (clsFirstIllnessNote_ZYRecordContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
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
                objDPArr[8].Value = 0;

                objDPArr[9].Value = objContent.m_strMostlyContent;
                objDPArr[10].Value = objContent.m_strMostlyContentXML;
                objDPArr[11].Value = objContent.m_strOriginalDiagnose;
                objDPArr[12].Value = objContent.m_strOriginalDiagnoseXML;
                objDPArr[13].Value = objContent.m_strThereunderDiagnose;
                objDPArr[14].Value = objContent.m_strThereunderDiagnoseXML;
                objDPArr[15].Value = objContent.m_strDiagnoseDiffe;
                objDPArr[16].Value = objContent.m_strDiagnoseDiffeXML;
                objDPArr[17].Value = objContent.m_strCurePlan;
                objDPArr[18].Value = objContent.m_strCurePlanXML;
                objDPArr[19].Value = objContent.m_strIdentifyReston;
                objDPArr[20].Value = objContent.m_strIdentifyRestonXML;
                objDPArr[21].Value = objContent.m_strIdentifyDiagnos;
                objDPArr[22].Value = objContent.m_strIdentifyDiagnoseXML;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strMostlyContent_Right;
                objDPArr2[6].Value = objContent.m_strOriginalDiagnose_Right;
                objDPArr2[7].Value = objContent.m_strThereunderDiagnose_Right;
                objDPArr2[8].Value = objContent.m_strDiagnoseDiffe_Right;
                objDPArr2[9].Value = objContent.m_strCurePlan_Right;
                objDPArr2[10].Value = objContent.m_strIdentifyReston_Right;
                objDPArr2[11].Value = objContent.m_strIdentifyDiagnose_Right;


                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);


            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
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
			p_objModifyInfo=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from DiseaseSummaryRecord where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
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

            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsFirstIllnessNote_ZYRecordContent objContent = (clsFirstIllnessNote_ZYRecordContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr);


                objDPArr[0].Value = objContent.m_strMostlyContent;
                objDPArr[1].Value = objContent.m_strMostlyContentXML;

                objDPArr[2].Value = objContent.m_strOriginalDiagnose;
                objDPArr[3].Value = objContent.m_strOriginalDiagnoseXML;

                objDPArr[4].Value = objContent.m_strThereunderDiagnose;
                objDPArr[5].Value = objContent.m_strThereunderDiagnoseXML;

                objDPArr[6].Value = objContent.m_strDiagnoseDiffe;
                objDPArr[7].Value = objContent.m_strDiagnoseDiffeXML;

                objDPArr[8].Value = objContent.m_strCurePlan;
                objDPArr[9].Value = objContent.m_strCurePlanXML;

                objDPArr[10].Value = objContent.m_strIdentifyReston;
                objDPArr[11].Value = objContent.m_strIdentifyRestonXML;

                objDPArr[12].Value = objContent.m_strIdentifyDiagnos;
                objDPArr[13].Value = objContent.m_strIdentifyDiagnoseXML;

                objDPArr[14].Value = objContent.m_strInPatientID;
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = objContent.m_dtmInPatientDate;
                objDPArr[16].DbType = DbType.DateTime;
                objDPArr[16].Value = objContent.m_dtmOpenDate;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_strMostlyContent_Right;
                objDPArr2[6].Value = objContent.m_strOriginalDiagnose_Right;
                objDPArr2[7].Value = objContent.m_strThereunderDiagnose_Right;
                objDPArr2[8].Value = objContent.m_strDiagnoseDiffe_Right;
                objDPArr2[9].Value = objContent.m_strCurePlan_Right;
                objDPArr2[10].Value = objContent.m_strIdentifyReston_Right;
                objDPArr2[11].Value = objContent.m_strIdentifyDiagnose_Right;


                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);


            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);


                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
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
			p_dtmModifyDate=DateTime.Now;
			p_strFirstPrintDate=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

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
                    p_strFirstPrintDate = dtbValue.Rows[0]["FirstPrintDate"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["ModifyDate"].ToString());
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
            //返回
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
			p_objRecordContent=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
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
                    //设置结果
                    clsFirstIllnessNote_ZYRecordContent objRecordContent = new clsFirstIllnessNote_ZYRecordContent();
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

                    objRecordContent.m_strMostlyContent_Right = dtbValue.Rows[0]["MostlyContent_Right"].ToString();
                    objRecordContent.m_strMostlyContent = dtbValue.Rows[0]["MostlyContent"].ToString();
                    objRecordContent.m_strMostlyContentXML = dtbValue.Rows[0]["MostlyContentXML"].ToString();

                    objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[0]["OriginalDiagnose_Right"].ToString();
                    objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[0]["OriginalDiagnose"].ToString();
                    objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[0]["OriginalDiagnoseXML"].ToString();

                    objRecordContent.m_strThereunderDiagnose_Right = dtbValue.Rows[0]["ThereunderDiagnose_Right"].ToString();
                    objRecordContent.m_strThereunderDiagnose = dtbValue.Rows[0]["ThereunderDiagnose"].ToString();
                    objRecordContent.m_strThereunderDiagnoseXML = dtbValue.Rows[0]["ThereunderDiagnoseXML"].ToString();

                    objRecordContent.m_strDiagnoseDiffe_Right = dtbValue.Rows[0]["DiagnoseDiffe_Right"].ToString();
                    objRecordContent.m_strDiagnoseDiffe = dtbValue.Rows[0]["DiagnoseDiffe"].ToString();
                    objRecordContent.m_strDiagnoseDiffeXML = dtbValue.Rows[0]["DiagnoseDiffeXML"].ToString();

                    objRecordContent.m_strCurePlan_Right = dtbValue.Rows[0]["CurePlan_Right"].ToString();
                    objRecordContent.m_strCurePlan = dtbValue.Rows[0]["CurePlan"].ToString();
                    objRecordContent.m_strCurePlanXML = dtbValue.Rows[0]["CurePlanXML"].ToString();

                    objRecordContent.m_strIdentifyDiagnose_Right = dtbValue.Rows[0]["IDENTIFYDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strIdentifyDiagnos = dtbValue.Rows[0]["IDENTIFYDIAGNOSE"].ToString();
                    objRecordContent.m_strIdentifyDiagnoseXML = dtbValue.Rows[0]["IDENTIFYDIAGNOSEXML"].ToString();

                    objRecordContent.m_strIdentifyReston_Right = dtbValue.Rows[0]["IDENTIFYRESTON_RIGHT"].ToString();
                    objRecordContent.m_strIdentifyReston = dtbValue.Rows[0]["IDENTIFYRESTON"].ToString();
                    objRecordContent.m_strIdentifyRestonXML = dtbValue.Rows[0]["IDENTIFYRESTONXML"].ToString();


                    p_objRecordContent = objRecordContent;
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

            }			//返回
			return lngRes;

			

		}
		
	}// END CLASS DEFINITION clsDiseaseSummaryService
}
