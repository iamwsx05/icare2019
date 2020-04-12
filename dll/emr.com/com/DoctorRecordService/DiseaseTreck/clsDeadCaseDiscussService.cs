using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.DiseaseTrackService
{
	// 实现特殊记录的中间件。
	//病程记录－－死亡病例讨论记录
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsDeadCaseDiscussService	: clsDiseaseTrackService
	{

		#region RegionName
		/// <summary>
		/// 从DeadCaseDiscussRecord获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= "select CreateDate,OpenDate from DeadCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and Status=0";

		/// <summary>
		/// 根据指定表单的信息，从DeadCaseDiscussRecord和DeadCaseDiscussRecordContent查找表单的内容。
		/// 用InPatientID ,InPatientDate ,CreateDate,Status = 0等条件，查询该记录的内容，查找Max(ModifyDate)。
		/// 如果返回lngRes = 1 && rows = 0,则证明此记录已被他人删除掉。
		/// </summary>
		private const string c_strGetRecordContentSQL= @"select a.*,
																b.*
															from DeadCaseDiscussRecord a
															inner join DeadCaseDiscussRecordContent b on (a.InPatientID = ? and
																										a.InPatientDate = ? and
																										a.OpenDate = ?)
															
															where a.Status = 0
															and b.ModifyDate =
																(select Max(ModifyDate)
																	from DeadCaseDiscussRecordContent
																	where (InPatientID = a.InPatientID and
																		InPatientDate = a.InPatientDate and OpenDate = a.OpenDate)) ";

		
		/// <summary>
		/// 从DeadCaseDiscussRecord中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select CreateUserID,OpenDate from DeadCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		//		/// <summary>
		//		/// 从DeadCaseDiscussRecord获取已经存在记录的主要信息,获取修改表单的主要信息
		//		/// </summary>
		//		private const string c_strGetExistInfoSQL= "";

		/// <summary>
		/// 从DeadCaseDiscussRecordContent获取指定表单的最后修改时间。
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate,b.ModifyUserID from DeadCaseDiscussRecord a,DeadCaseDiscussRecordContent b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from DeadCaseDiscussRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		//		/// <summary>
		//		/// 从DeadCaseDiscussRecordContent获取修改表单的主要信息。
		//		/// </summary>
		//		private const string c_strGetModifyRecordSQL= "";

		/// <summary>
		/// 从DeadCaseDiscussRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select DeActivedDate,DeActivedOperatorID from DeadCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";

		/// <summary>
		/// 添加记录到DeadCaseDiscussRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  DeadCaseDiscussRecord(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,IfConfirm,ConfirmReason,ConfirmReasonXML,Status,Address,AddressXML,DiscussContent,DiscussContentXML,DeadDiagnose,DeadDiagnoseXML,DeadReason,DeadReasonXML,UseForReference,UseForReferenceXML,SEQUENCE_INT,MARKSTATUS) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 添加记录到DeadCaseDiscussRecordContent
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  DeadCaseDiscussRecordContent(InPatientID,InPatientDate,OpenDate,ModifyDate,ModifyUserID,Address_Right,DiscussContent_Right,DeadDiagnose_Right,DeadReason_Right,UseForReference_Right,CompereID,ReadOuterID) 
				values(?,?,?,?,?,?,?,?,?,?,?,?)";

		
		/// <summary>
		/// 修改记录到DeadCaseDiscussRecordContent
		/// </summary>
		private const string c_strModifyRecordSQL= "Update DeadCaseDiscussRecord Set Address=?,AddressXML=? ,DiscussContent=?,DiscussContentXML=? ,DeadDiagnose=?,DeadDiagnoseXML=? ,DeadReason=?,DeadReasonXML=? ,UseForReference=?,UseForReferenceXML=?,SEQUENCE_INT=? ,MARKSTATUS=?  Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

		/// <summary>
		/// 修改记录到DeadCaseDiscussRecordContent
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		
		/// <summary>
		/// 设置DeadCaseDiscussRecord中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= "Update DeadCaseDiscussRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";

		/// <summary>
		/// 从DeadCaseDiscussRecord和DeadCaseDiscussRecordContent获取LastModifyDate和FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.FirstPrintDate,b.ModifyDate from DeadCaseDiscussRecord a,DeadCaseDiscussRecordContent b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from DeadCaseDiscussRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";


		/// <summary>
		/// 更新DeadCaseDiscussRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "Update  DeadCaseDiscussRecord Set FirstPrintDate= ? Where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";

		/// <summary>
		/// 从DeadCaseDiscussRecord获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select CreateDate,OpenDate from DeadCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";

		/// <summary>
		/// 从DeadCaseDiscussRecord获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select CreateDate,OpenDate from DeadCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and Status=1";

		
		private const string c_strGetDeleteRecordContentSQL= @"select a.*,
																		b.*
																	from DeadCaseDiscussRecord        a,
																		DeadCaseDiscussRecordContent b
																	where a.InPatientID = ?
																	and a.InPatientDate = ?
																	and a.OpenDate = ?
																	and a.Status = 1
																	and b.InPatientID = a.InPatientID
																	and b.InPatientDate = a.InPatientDate
																	and b.OpenDate = a.OpenDate
																	and b.ModifyDate = (select Max(ModifyDate)
																							from DeadCaseDiscussRecordContent
																							Where InPatientID = a.InPatientID
																							and InPatientDate = a.InPatientDate
																							and OpenDate = a.OpenDate) ";

		/// <summary>
		/// 在DeadCaseDiscussRecordAttendee中获取指定ModifyDate的医师签名。
		/// </summary>
		private const string c_strGetDeleteAttendeeContentSQL= @"select sub2.AttendeeID, PBI.Lastname_Vchr as AttendeeName
																		from DeadCaseDiscussRecord         a,
																			DeadCaseDiscussRecordAttendee sub2,
																			t_bse_employee              PBI
																		where a.InPatientID = ?
																		and a.InPatientDate = ?
																		and a.OpenDate = ?
																		and a.Status = 1
																		and sub2.AttendeeID= PBI.Empno_Chr)
																		and sub2.InPatientID = a.InPatientID
																		and sub2.InPatientDate = a.InPatientDate
																		and sub2.OpenDate = a.OpenDate
																		and sub2.ModifyDate = (select Max(ModifyDate)
																									from DeadCaseDiscussRecordAttendee
																								Where InPatientID = a.InPatientID
																									and InPatientDate = a.InPatientDate
																									and OpenDate = a.OpenDate) ";
		/// <summary>
		/// 在DeadCaseDiscussRecordNoter中获取指定ModifyDate的医师签名。
		/// </summary>
		private const string c_strGetDeleteNoterContentSQL=@"select sub2.NoterID, PBI.Lastname_Vchr as NoterName
																		from DeadCaseDiscussRecord      a,
																			DeadCaseDiscussRecordNoter sub2,
																			t_bse_employee          PBI
																		where a.InPatientID = ?
																		and a.InPatientDate = ?
																		and a.OpenDate = ?
																		and a.Status = 1
																		and sub2.NoterID = PBI.Empno_Chr
																		and sub2.InPatientID = a.InPatientID
																		and sub2.InPatientDate = a.InPatientDate
																		and sub2.OpenDate = a.OpenDate
																		and sub2.ModifyDate = (select Max(ModifyDate)
																									from DeadCaseDiscussRecordNoter
																								Where InPatientID = a.InPatientID
																									and InPatientDate = a.InPatientDate
																									and OpenDate = a.OpenDate) ";
		// 在DeadCaseDiscussRecordAttendee中获取指定表单和ModifyDate的参加人员。
		private string c_strGetAttendeeContentSQL= @"select sub2.AttendeeID, PBI.Lastname_Vchr as AttendeeName
															from DeadCaseDiscussRecord         a,
																DeadCaseDiscussRecordAttendee sub2,
																t_bse_employee              PBI
															where a.InPatientID = ?
															and a.InPatientDate = ?
															and a.OpenDate = ?
															and a.Status = 0
															and sub2.AttendeeID = PBI.Empno_Chr
															and sub2.InPatientID = a.InPatientID
															and sub2.InPatientDate = a.InPatientDate
															and sub2.OpenDate = a.OpenDate
															and sub2.ModifyDate = (select Max(ModifyDate)
																						from DeadCaseDiscussRecordAttendee
																					Where InPatientID = a.InPatientID
																						and InPatientDate = a.InPatientDate
																						and OpenDate = a.OpenDate) ";

		// 在DeadCaseDiscussRecordNoter中获取指定表单和ModifyDate的记录者。
		private string c_strGetNoterContentSQL= @"select sub2.NoterID, PBI.Lastname_Vchr as NoterName
																from DeadCaseDiscussRecord      a,
																	DeadCaseDiscussRecordNoter sub2,
																	t_bse_employee           PBI
																where a.InPatientID = ?
																and a.InPatientDate = ?
																and a.OpenDate = ?
																and a.Status = 0
																and sub2.NoterID = PBI.Empno_Chr
																and sub2.InPatientID = a.InPatientID
																and sub2.InPatientDate = a.InPatientDate
																and sub2.OpenDate = a.OpenDate
																and sub2.ModifyDate = (select Max(ModifyDate)
																							from DeadCaseDiscussRecordNoter
																						Where InPatientID = a.InPatientID
																							and InPatientDate = a.InPatientDate
																							and OpenDate = a.OpenDate) ";

		/// <summary>
		/// 添加记录到DeadCaseDiscussRecordAttendee
		/// </summary>
		private const string c_strAddNewRecordAttendeeSQL= @"insert into  DeadCaseDiscussRecordAttendee(InPatientID,InPatientDate,OpenDate,ModifyDate,AttendeeID) 
								values(?,?,?,?,?)";

		// 添加记录到DeadCaseDiscussRecordNoter
		private const string c_strAddNewRecordNoterSQL =@"insert into  DeadCaseDiscussRecordNoter(InPatientID,InPatientDate,OpenDate,ModifyDate,NoterID) 
								values(?,?,?,?,?)";
		
		// 修改记录到DeadCaseDiscussRecordAttendee
		private const string c_strModifyRecordAttendeeSQL=c_strAddNewRecordAttendeeSQL;

		// 修改记录到DeadCaseDiscussRecordNoter
		private const string c_strModifyRecordNoterSQL=c_strAddNewRecordNoterSQL;

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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeadCaseDiscussService", "m_lngGetRecordTimeList");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeadCaseDiscussService", "m_lngUpdateFirstPrintDate");
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
                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeadCaseDiscussService", "m_lngGetDeleteRecordTimeList");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeadCaseDiscussService", "m_lngGetDeleteRecordTimeListAll");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
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
                    clsDeadCaseDiscussRecordContent objRecordContent = new clsDeadCaseDiscussRecordContent();
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

                    objRecordContent.m_strAddress_Right = dtbValue.Rows[0]["ADDRESS_RIGHT"].ToString();
                    objRecordContent.m_strAddress = dtbValue.Rows[0]["ADDRESS"].ToString();
                    objRecordContent.m_strAddressXML = dtbValue.Rows[0]["ADDRESSXML"].ToString();
                    objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[0]["DISCUSSCONTENT_RIGHT"].ToString();
                    objRecordContent.m_strDiscussContent = dtbValue.Rows[0]["DISCUSSCONTENT"].ToString();
                    objRecordContent.m_strDiscussContentXML = dtbValue.Rows[0]["DISCUSSCONTENTXML"].ToString();
                    objRecordContent.m_strDeadDiagnose_Right = dtbValue.Rows[0]["DEADDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strDeadDiagnose = dtbValue.Rows[0]["DEADDIAGNOSE"].ToString();
                    objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[0]["DEADDIAGNOSEXML"].ToString();
                    objRecordContent.m_strDeadReason_Right = dtbValue.Rows[0]["DEADREASON_RIGHT"].ToString();
                    objRecordContent.m_strDeadReason = dtbValue.Rows[0]["DEADREASON"].ToString();
                    objRecordContent.m_strDeadReasonXML = dtbValue.Rows[0]["DEADREASONXML"].ToString();
                    objRecordContent.m_strUseForReference_Right = dtbValue.Rows[0]["USEFORREFERENCE_RIGHT"].ToString();
                    objRecordContent.m_strUseForReference = dtbValue.Rows[0]["USEFORREFERENCE"].ToString();
                    objRecordContent.m_strUseForReferenceXML = dtbValue.Rows[0]["USEFORREFERENCEXML"].ToString();
                    objRecordContent.m_intMarkStatus = int.Parse(dtbValue.Rows[0]["MARKSTATUS"].ToString());

                    //					objRecordContent.m_strCompereID=dtbValue.Rows[0]["COMPEREID"].ToString();
                    //					objRecordContent.m_strCompereName=dtbValue.Rows[0]["COMPERENAME"].ToString().Trim();
                    //
                    //					objRecordContent.m_strReadOuterID=dtbValue.Rows[0]["READOUTERID"].ToString();
                    //					objRecordContent.m_strReadOuterName=dtbValue.Rows[0]["READOUTERNAME"].ToString().Trim();
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
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
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
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsDeadCaseDiscussRecordContent objContent = (clsDeadCaseDiscussRecordContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(21, out objDPArr);

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
                objDPArr[9].Value = objContent.m_strAddress;
                objDPArr[10].Value = objContent.m_strAddressXML;
                objDPArr[11].Value = objContent.m_strDiscussContent;
                objDPArr[12].Value = objContent.m_strDiscussContentXML;
                objDPArr[13].Value = objContent.m_strDeadDiagnose;
                objDPArr[14].Value = objContent.m_strDeadDiagnoseXML;
                objDPArr[15].Value = objContent.m_strDeadReason;
                objDPArr[16].Value = objContent.m_strDeadReasonXML;
                objDPArr[17].Value = objContent.m_strUseForReference;
                objDPArr[18].Value = objContent.m_strUseForReferenceXML;
                objDPArr[19].Value = lngSequence;
                objDPArr[20].Value = objContent.m_intMarkStatus;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);


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
                objDPArr2[5].Value = objContent.m_strAddress_Right;
                objDPArr2[6].Value = objContent.m_strDiscussContent_Right;
                objDPArr2[7].Value = objContent.m_strDeadDiagnose_Right;
                objDPArr2[8].Value = objContent.m_strDeadReason_Right;
                objDPArr2[9].Value = objContent.m_strUseForReference_Right;
                objDPArr2[10].Value = objContent.m_strCompereID;
                objDPArr2[11].Value = objContent.m_strReadOuterID;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strAttendeeIDArr != null)
                {



                    for (int j = 0; j < objContent.m_strAttendeeIDArr.Length; j++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;

                        objDPArr3[4].Value = objContent.m_strAttendeeIDArr[j];

                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordAttendeeSQL, ref lngEff, objDPArr3);
                        if (lngRes <= 0) return lngRes;
                    }
                }

                if (objContent.m_strNoterIDArr != null)
                {



                    for (int j = 0; j < objContent.m_strNoterIDArr.Length; j++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;

                        objDPArr3[4].Value = objContent.m_strNoterIDArr[j];

                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordNoterSQL, ref lngEff, objDPArr3);
                        if (lngRes <= 0) return lngRes;
                    }
                }

                //释放
                objSign = null;

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
            //返回
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
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsDeadCaseDiscussRecordContent objContent = (clsDeadCaseDiscussRecordContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(15, out objDPArr);


                objDPArr[0].Value = objContent.m_strAddress;
                objDPArr[1].Value = objContent.m_strAddressXML;
                objDPArr[2].Value = objContent.m_strDiscussContent;
                objDPArr[3].Value = objContent.m_strDiscussContentXML;
                objDPArr[4].Value = objContent.m_strDeadDiagnose;
                objDPArr[5].Value = objContent.m_strDeadDiagnoseXML;
                objDPArr[6].Value = objContent.m_strDeadReason;
                objDPArr[7].Value = objContent.m_strDeadReasonXML;
                objDPArr[8].Value = objContent.m_strUseForReference;
                objDPArr[9].Value = objContent.m_strUseForReferenceXML;
                objDPArr[10].Value = lngSequence;
                objDPArr[11].Value = objContent.m_intMarkStatus;

                objDPArr[12].Value = objContent.m_strInPatientID;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = objContent.m_dtmInPatientDate;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = objContent.m_dtmOpenDate;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);


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
                objDPArr2[5].Value = objContent.m_strAddress_Right;
                objDPArr2[6].Value = objContent.m_strDiscussContent_Right;
                objDPArr2[7].Value = objContent.m_strDeadDiagnose_Right;
                objDPArr2[8].Value = objContent.m_strDeadReason_Right;
                objDPArr2[9].Value = objContent.m_strUseForReference_Right;
                objDPArr2[10].Value = objContent.m_strCompereID;
                objDPArr2[11].Value = objContent.m_strReadOuterID;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strAttendeeIDArr != null)
                {



                    for (int j = 0; j < objContent.m_strAttendeeIDArr.Length; j++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;

                        objDPArr3[4].Value = objContent.m_strAttendeeIDArr[j];

                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordAttendeeSQL, ref lngEff, objDPArr3);
                        if (lngRes <= 0) return lngRes;
                    }
                }

                if (objContent.m_strNoterIDArr != null)
                {



                    for (int j = 0; j < objContent.m_strNoterIDArr.Length; j++)
                    {
                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;

                        objDPArr3[4].Value = objContent.m_strNoterIDArr[j];

                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordNoterSQL, ref lngEff, objDPArr3);
                        if (lngRes <= 0) return lngRes;
                    }
                }
                //释放
                objSign = null;

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

            }			
            //返回
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
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
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
                    clsDeadCaseDiscussRecordContent objRecordContent = new clsDeadCaseDiscussRecordContent();
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

                    objRecordContent.m_strAddress_Right = dtbValue.Rows[0]["ADDRESS_RIGHT"].ToString();
                    objRecordContent.m_strAddress = dtbValue.Rows[0]["ADDRESS"].ToString();
                    objRecordContent.m_strAddressXML = dtbValue.Rows[0]["ADDRESSXML"].ToString();
                    objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[0]["DISCUSSCONTENT_RIGHT"].ToString();
                    objRecordContent.m_strDiscussContent = dtbValue.Rows[0]["DISCUSSCONTENT"].ToString();
                    objRecordContent.m_strDiscussContentXML = dtbValue.Rows[0]["DISCUSSCONTENTXML"].ToString();
                    objRecordContent.m_strDeadDiagnose_Right = dtbValue.Rows[0]["DEADDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strDeadDiagnose = dtbValue.Rows[0]["DEADDIAGNOSE"].ToString();
                    objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[0]["DEADDIAGNOSEXML"].ToString();
                    objRecordContent.m_strDeadReason_Right = dtbValue.Rows[0]["DEADREASON_RIGHT"].ToString();
                    objRecordContent.m_strDeadReason = dtbValue.Rows[0]["DEADREASON"].ToString();
                    objRecordContent.m_strDeadReasonXML = dtbValue.Rows[0]["DEADREASONXML"].ToString();
                    objRecordContent.m_strUseForReference_Right = dtbValue.Rows[0]["USEFORREFERENCE_RIGHT"].ToString();
                    objRecordContent.m_strUseForReference = dtbValue.Rows[0]["USEFORREFERENCE"].ToString();
                    objRecordContent.m_strUseForReferenceXML = dtbValue.Rows[0]["USEFORREFERENCEXML"].ToString();

                    //					objRecordContent.m_strCompereID=dtbValue.Rows[0]["COMPEREID"].ToString();
                    //					objRecordContent.m_strCompereName=dtbValue.Rows[0]["COMPERENAME"].ToString();

                    //					objRecordContent.m_strReadOuterID=dtbValue.Rows[0]["READOUTERID"].ToString();
                    //					objRecordContent.m_strReadOuterName=dtbValue.Rows[0]["READOUTERNAME"].ToString();


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

	}// END CLASS DEFINITION clsGeneralDiseaseService








	/// <summary>
	/// 实现死亡病例讨论记录(新)的中间件。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]	
	public class clsDeathCaseDiscussService	: clsDiseaseTrackService
	{
		/// <summary>
		/// 从DeathCaseDiscussRecord获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= "select CreateDate,OpenDate from DeathCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and Status=0";

		/// <summary>
		/// 根据指定表单的信息，从DeathCaseDiscussRecord查找表单的内容。
		/// 用InPatientID ,InPatientDate ,CreateDate,Status = 0等条件，查询该记录的内容，查找Max(ModifyDate)。
		/// 如果返回lngRes = 1 && rows = 0,则证明此记录已被他人删除掉。
		/// </summary>
		//使用新表 modified by tfzhang at 2005年10月24日 17:43:47
        private const string c_strGetRecordContentSQL = @"select a.*,
                                                               F_GETEMPNAMEANDTECHBYID(a.CompereID) CompereName,
                                                               F_GETEMPNAMEANDTECHBYID(a.RecorderID) RecorderName,
                                                               F_GETEMPNAMEANDTECHBYID(a.CompereSignID) CompereSignName
                                                          from DeathCaseDiscussRecord a
                                                         where a.InPatientID = ?
                                                           and a.InPatientDate = ?
                                                           and a.OpenDate = ?
                                                           and a.Status = 0
                                                           and a.ModifyDate = (select Max(ModifyDate)
                                                                                 from DeathCaseDiscussRecord
                                                                                Where InPatientID = a.InPatientID
                                                                                  and InPatientDate = a.InPatientDate
                                                                                  and OpenDate = a.OpenDate)";
		/// <summary>
		/// 从DeathCaseDiscussRecord中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select CreateUserID,OpenDate from DeathCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		/// <summary>
		/// 从DeathCaseDiscussRecordContent获取指定表单的最后修改时间。
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select a.ModifyDate,a.ModifyUserID 
															from DeathCaseDiscussRecord a 
															where a.InPatientID = ? 
															and a.InPatientDate= ? 
															and a.OpenDate= ? 
															and a.Status=0
															and	a.ModifyDate=(select Max(ModifyDate) 
																			from DeathCaseDiscussRecord 
																			Where InPatientID=a.InPatientID 
																			and InPatientDate=a.InPatientDate 
																			and OpenDate=a.OpenDate)";

		/// <summary>
		/// 从DeathCaseDiscussRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select DeActivedDate,DeActivedOperatorID from DeathCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
	
		/// <summary>
		/// 添加记录到DeathCaseDiscussRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  DeathCaseDiscussRecord(InPatientID,
               InPatientDate,OpenDate,CreateDate,CreateUserID,Status,ModifyDate,ModifyUserID,DeadDate,
               DiscussDate,DiscussAddress,CompereID,InHospitalDiagnose,InHospitalDiagnoseXML,SpeakRecord,
               SpeakRecordXML,DeadDiagnose,DeadDiagnoseXML,DeadReason,DeadReasonXML,Verdict,VerdictXML,
               Experience,ExperienceXML,RecorderID,CompereSignID) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到DeathCaseDiscussRecord
		/// </summary>
		private const string c_strModifyRecordSQL= "Update DeathCaseDiscussRecord Set DeadDate=?,DiscussDate=?,DiscussAddress=?,CompereID=?,InHospitalDiagnose=?,InHospitalDiagnoseXML=?,SpeakRecord=?,SpeakRecordXML=?,DeadDiagnose=?,DeadDiagnoseXML=?,DeadReason=?,DeadReasonXML=?,Verdict=?,VerdictXML=?,Experience=?,ExperienceXML=?,RecorderID=? ,ModifyDate=?,ModifyUserID=?,CompereSignID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";
	
		/// <summary>
		/// 设置DeathCaseDiscussRecord中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= "Update DeathCaseDiscussRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";

		/// <summary>
		/// 从DeathCaseDiscussRecord获取LastModifyDate和FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.FirstPrintDate,a.ModifyDate from DeathCaseDiscussRecord a where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and
						a.ModifyDate=(select Max(ModifyDate) from DeathCaseDiscussRecord Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

		/// <summary>
		/// 更新DeathCaseDiscussRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "Update  DeathCaseDiscussRecord Set FirstPrintDate= ? Where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";

		/// <summary>
		/// 从DeathCaseDiscussRecord获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select CreateDate,OpenDate from DeathCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";

		/// <summary>
		/// 从DeathCaseDiscussRecord获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select CreateDate,OpenDate from DeathCaseDiscussRecord Where InPatientID = ? and InPatientDate= ? and Status=1";

		/// <summary>
		/// 获取已删除表单内容。
		/// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.*,
                                                                   F_GETEMPNAMEBYNO(a.CompereID) CompereName,
                                                                   F_GETEMPNAMEBYNO(a.RecorderID) RecorderName,
                                                                   F_GETEMPNAMEBYNO(a.CompereSignID) CompereSignName
                                                              from DeathCaseDiscussRecord a
                                                             where a.InPatientID = ?
                                                               and a.InPatientDate = ?
                                                               and a.OpenDate = ?
                                                               and a.Status = 1
                                                               and a.ModifyDate = (select Max(ModifyDate)
                                                                                     from DeathCaseDiscussRecord
                                                                                    Where InPatientID = a.InPatientID
                                                                                      and InPatientDate = a.InPatientDate
                                                                                      and OpenDate = a.OpenDate)";

		/// <summary>
		/// 在DeathCaseDiscussRecordAttendee中获取指定ModifyDate的医师签名。
		/// </summary>
        private const string c_strGetDeleteAttendeeContentSQL = @"select sub2.AttendeeID, F_GETEMPNAMEBYNO(sub2.AttendeeID) AttendeeName
																	from DeathCaseDiscussRecord         a,
																		DeathCaseDiscussRecordAttendee sub2
																	where a.InPatientID = ?
																	and a.InPatientDate = ?
																	and a.OpenDate = ?
																	and a.Status = 1
																	and sub2.InPatientID = a.InPatientID
																	and sub2.InPatientDate = a.InPatientDate
																	and sub2.OpenDate = a.OpenDate
																	and sub2.ModifyDate = (select Max(ModifyDate)
																								from DeathCaseDiscussRecordAttendee
																							Where InPatientID = a.InPatientID
																								and InPatientDate = a.InPatientDate
																								and OpenDate = a.OpenDate) ";
		/// <summary>
		/// 在DeathCaseDiscussRecord中获取指定ModifyDate的医师签名。
		/// </summary>
        private const string c_strGetDeleteNoterContentSQL = @"select a.RecorderID, F_GETEMPNAMEBYNO(a.RecorderID) RecorderName
																				from DeathCaseDiscussRecord a
																				where a.InPatientID = ?
																				and a.InPatientDate = ?
																				and a.OpenDate = ?
																				and a.Status = 1
																				and a.ModifyDate = (select Max(ModifyDate)
																										from DeathCaseDiscussRecord
																										Where InPatientID = a.InPatientID
																										and InPatientDate = a.InPatientDate
																										and OpenDate = a.OpenDate) ";

		//使用新表 modified by tfzhang at 2005年10月24日 18:12:03
        private string c_strGetAttendeeContentSQL = @"select sub2.AttendeeID, F_GETEMPNAMEBYNO(sub2.AttendeeID) AttendeeName
													from DeathCaseDiscussRecord         a,
														DeathCaseDiscussRecordAttendee sub2
													where a.InPatientID = ?
													and a.InPatientDate = ?
													and a.OpenDate = ?
													and a.Status = 0
													and sub2.InPatientID = a.InPatientID
													and sub2.InPatientDate = a.InPatientDate
													and sub2.OpenDate = a.OpenDate
													and  sub2.ModifyDate =
														(select Max(ModifyDate)
															from DeathCaseDiscussRecordAttendee
															Where InPatientID = a.InPatientID
															and InPatientDate = a.InPatientDate
															and OpenDate = a.OpenDate)";

		//使用新表 modified by tfzhang at 2005年10月24日 18:17:04
        private string c_strGetNoterContentSQL = @"select a.RecorderID, F_GETEMPNAMEBYNO(a.RecorderID) RecorderName
													from DeathCaseDiscussRecord a
													where a.InPatientID = ?
													and a.InPatientDate = ?
													and a.OpenDate = ?
													and a.Status = 0
													and a.ModifyDate = (select Max(ModifyDate)
																				from DeathCaseDiscussRecord
																				Where InPatientID = a.InPatientID
																				and InPatientDate = a.InPatientDate
																				and OpenDate = a.OpenDate) ";
		/// <summary>
		/// 添加记录到DeathCaseDiscussRecordAttendee
		/// </summary>
		private const string c_strAddNewRecordAttendeeSQL= @"insert into  DeathCaseDiscussRecordAttendee(InPatientID,InPatientDate,OpenDate,ModifyDate,AttendeeID) 
								values(?,?,?,?,?)";

		
		// 修改记录到DeadCaseDiscussRecordAttendee
		private const string c_strModifyRecordAttendeeSQL=c_strAddNewRecordAttendeeSQL;

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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeathCaseDiscussService", "m_lngGetRecordTimeList");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeathCaseDiscussService", "m_lngUpdateFirstPrintDate");
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
                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeathCaseDiscussService", "m_lngGetDeleteRecordTimeList");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDeathCaseDiscussService", "m_lngGetDeleteRecordTimeListAll");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
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
                    clsDeadCaseDiscussRecord_VO objRecordContent = new clsDeadCaseDiscussRecord_VO();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    objRecordContent.m_dtmDeadDate = DateTime.Parse(dtbValue.Rows[0]["DEADDATE"].ToString());
                    objRecordContent.m_dtmDiscussDate = DateTime.Parse(dtbValue.Rows[0]["DISCUSSDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_strDiscussAddress = dtbValue.Rows[0]["DISCUSSADDRESS"].ToString();

                    objRecordContent.m_strInHospitalDiagnose = dtbValue.Rows[0]["INHOSPITALDIAGNOSE"].ToString();
                    objRecordContent.m_strInHospitalDiagnoseXML = dtbValue.Rows[0]["INHOSPITALDIAGNOSEXML"].ToString();
                    objRecordContent.m_strSpeakRecord = dtbValue.Rows[0]["SPEAKRECORD"].ToString();
                    objRecordContent.m_strSpeakRecordXML = dtbValue.Rows[0]["SPEAKRECORDXML"].ToString();
                    objRecordContent.m_strDeadDiagnose = dtbValue.Rows[0]["DEADDIAGNOSE"].ToString();
                    objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[0]["DEADDIAGNOSEXML"].ToString();
                    objRecordContent.m_strDeadReason = dtbValue.Rows[0]["DEADREASON"].ToString();
                    objRecordContent.m_strDeadReasonXML = dtbValue.Rows[0]["DEADREASONXML"].ToString();
                    objRecordContent.m_strExperience = dtbValue.Rows[0]["EXPERIENCE"].ToString();
                    objRecordContent.m_strExperienceXML = dtbValue.Rows[0]["EXPERIENCEXML"].ToString();
                    objRecordContent.m_strVerdict = dtbValue.Rows[0]["VERDICT"].ToString();
                    objRecordContent.m_strVerdictXML = dtbValue.Rows[0]["VERDICTXML"].ToString();

                    objRecordContent.m_strCompereID = dtbValue.Rows[0]["COMPEREID"].ToString();
                    objRecordContent.m_strCompereName = dtbValue.Rows[0]["COMPERENAME"].ToString().Trim();

                    objRecordContent.m_strRecorderID = dtbValue.Rows[0]["RECORDERID"].ToString();
                    objRecordContent.m_strRecorderName = dtbValue.Rows[0]["RECORDERNAME"].ToString().Trim();
                    objRecordContent.m_strCompereSignID = dtbValue.Rows[0]["COMPERESIGNID"].ToString().Trim();
                    objRecordContent.m_strCompereSignName = dtbValue.Rows[0]["COMPERESIGNNAME"].ToString().Trim();

                    dtbValue = new DataTable();
                    //按顺序给IDataParameter赋值
                    //					for(int i=0;i<objDPArr.Length;i++)
                    //						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                    long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetAttendeeContentSQL, ref dtbValue, objDPArr);
                    //从DataTable.Rows中获取结果
                    if (lngRes2 > 0 && dtbValue.Rows.Count > 0)
                    {
                        objRecordContent.m_strAttendeeIDArr = new string[dtbValue.Rows.Count];
                        objRecordContent.m_strAttendeeNameArr = new string[dtbValue.Rows.Count];
                        for (int i = 0; i < dtbValue.Rows.Count; i++)
                        {
                            objRecordContent.m_strAttendeeIDArr[i] = dtbValue.Rows[i]["ATTENDEEID"].ToString();
                            objRecordContent.m_strAttendeeNameArr[i] = dtbValue.Rows[i]["ATTENDEENAME"].ToString().Trim();
                        }
                    }


                    p_objRecordContent = objRecordContent;
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
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
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

            }			
            //返回
			return lngRes;

		}

		/// <summary>
		/// 保存记录到数据库。
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
                clsDeadCaseDiscussRecord_VO objContent = (clsDeadCaseDiscussRecord_VO)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(26, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = 0;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = objContent.m_dtmModifyDate;
                objDPArr[7].Value = objContent.m_strModifyUserID;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = objContent.m_dtmDeadDate;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = objContent.m_dtmDiscussDate;
                objDPArr[10].Value = objContent.m_strDiscussAddress;
                objDPArr[11].Value = objContent.m_strCompereID;
                objDPArr[12].Value = objContent.m_strInHospitalDiagnose;
                objDPArr[13].Value = objContent.m_strInHospitalDiagnoseXML;
                objDPArr[14].Value = objContent.m_strSpeakRecord;
                objDPArr[15].Value = objContent.m_strSpeakRecordXML;
                objDPArr[16].Value = objContent.m_strDeadDiagnose;
                objDPArr[17].Value = objContent.m_strDeadDiagnoseXML;
                objDPArr[18].Value = objContent.m_strDeadReason;
                objDPArr[19].Value = objContent.m_strDeadReasonXML;
                objDPArr[20].Value = objContent.m_strVerdict;
                objDPArr[21].Value = objContent.m_strVerdictXML;
                objDPArr[22].Value = objContent.m_strExperience;
                objDPArr[23].Value = objContent.m_strExperienceXML;
                objDPArr[24].Value = objContent.m_strRecorderID;
                objDPArr[25].Value = objContent.m_strCompereSignID;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strAttendeeIDArr != null)
                {



                    for (int j = 0; j < objContent.m_strAttendeeIDArr.Length; j++)
                    {
                        //按顺序给IDataParameter赋值
                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;

                        objDPArr3[4].Value = objContent.m_strAttendeeIDArr[j];

                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordAttendeeSQL, ref lngEff, objDPArr3);
                        if (lngRes <= 0) return lngRes;
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
            //返回
			return lngRes;

		}

		/// <summary>
		/// 把新修改的内容保存到数据库。
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
                clsDeadCaseDiscussRecord_VO objContent = (clsDeadCaseDiscussRecord_VO)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmDeadDate;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmDiscussDate;
                objDPArr[2].Value = objContent.m_strDiscussAddress;
                objDPArr[3].Value = objContent.m_strCompereID;
                objDPArr[4].Value = objContent.m_strInHospitalDiagnose;
                objDPArr[5].Value = objContent.m_strInHospitalDiagnoseXML;
                objDPArr[6].Value = objContent.m_strSpeakRecord;
                objDPArr[7].Value = objContent.m_strSpeakRecordXML;
                objDPArr[8].Value = objContent.m_strDeadDiagnose;
                objDPArr[9].Value = objContent.m_strDeadDiagnoseXML;
                objDPArr[10].Value = objContent.m_strDeadReason;
                objDPArr[11].Value = objContent.m_strDeadReasonXML;
                objDPArr[12].Value = objContent.m_strVerdict;
                objDPArr[13].Value = objContent.m_strVerdictXML;
                objDPArr[14].Value = objContent.m_strExperience;
                objDPArr[15].Value = objContent.m_strExperienceXML;
                objDPArr[16].Value = objContent.m_strRecorderID;
                objDPArr[17].Value = objContent.m_dtmModifyDate;
                objDPArr[18].Value = objContent.m_strModifyUserID;
                objDPArr[19].Value = objContent.m_strCompereSignID;

                objDPArr[20].Value = objContent.m_strInPatientID;
                objDPArr[21].DbType = DbType.DateTime;
                objDPArr[21].Value = objContent.m_dtmInPatientDate;
                objDPArr[22].DbType = DbType.DateTime;
                objDPArr[22].Value = objContent.m_dtmOpenDate;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strAttendeeIDArr != null)
                {



                    for (int j = 0; j < objContent.m_strAttendeeIDArr.Length; j++)
                    {


                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);


                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;
                        objDPArr3[4].Value = objContent.m_strAttendeeIDArr[j];


                        //执行SQL			
                        lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordAttendeeSQL, ref lngEff, objDPArr3);
                        if (lngRes <= 0) return lngRes;
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

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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

            }			
            //返回
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
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //返回

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
                    clsDeadCaseDiscussRecord_VO objRecordContent = new clsDeadCaseDiscussRecord_VO();
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
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_dtmDeadDate = DateTime.Parse(dtbValue.Rows[0]["DEADDATE"].ToString());
                    objRecordContent.m_dtmDiscussDate = DateTime.Parse(dtbValue.Rows[0]["DISCUSSDATE"].ToString());
                    objRecordContent.m_strDiscussAddress = dtbValue.Rows[0]["DISCUSSADDRESS"].ToString();

                    objRecordContent.m_strInHospitalDiagnose = dtbValue.Rows[0]["INHOSPITALDIAGNOSE"].ToString();
                    objRecordContent.m_strInHospitalDiagnoseXML = dtbValue.Rows[0]["INHOSPITALDIAGNOSEXML"].ToString();
                    objRecordContent.m_strSpeakRecord = dtbValue.Rows[0]["SPEAKRECORD"].ToString();
                    objRecordContent.m_strSpeakRecordXML = dtbValue.Rows[0]["SPEAKRECORDXML"].ToString();
                    objRecordContent.m_strVerdict = dtbValue.Rows[0]["VERDICT"].ToString();
                    objRecordContent.m_strVerdictXML = dtbValue.Rows[0]["VERDICTXML"].ToString();
                    objRecordContent.m_strDeadDiagnose = dtbValue.Rows[0]["DEADDIAGNOSE"].ToString();
                    objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[0]["DEADDIAGNOSEXML"].ToString();
                    objRecordContent.m_strDeadReason = dtbValue.Rows[0]["DEADREASON"].ToString();
                    objRecordContent.m_strDeadReasonXML = dtbValue.Rows[0]["DEADREASONXML"].ToString();
                    objRecordContent.m_strRecorderID = dtbValue.Rows[0]["RECORDERID"].ToString();
                    objRecordContent.m_strRecorderName = dtbValue.Rows[0]["RECORDERNAME"].ToString();

                    objRecordContent.m_strCompereID = dtbValue.Rows[0]["COMPEREID"].ToString();
                    objRecordContent.m_strCompereName = dtbValue.Rows[0]["COMPERENAME"].ToString();
                    objRecordContent.m_strCompereSignID = dtbValue.Rows[0]["COMPERESIGNID"].ToString();
                    objRecordContent.m_strCompereSignName = dtbValue.Rows[0]["COMPERESIGNNAME"].ToString();

                    dtbValue = new DataTable();
                    //按顺序给IDataParameter赋值
                    //					for(int i=0;i<objDPArr.Length;i++)
                    //						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                    long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteAttendeeContentSQL, ref dtbValue, objDPArr);
                    //从DataTable.Rows中获取结果
                    if (lngRes2 > 0 && dtbValue.Rows.Count > 0)
                    {
                        objRecordContent.m_strAttendeeIDArr = new string[dtbValue.Rows.Count];
                        objRecordContent.m_strAttendeeNameArr = new string[dtbValue.Rows.Count];
                        for (int i = 0; i < dtbValue.Rows.Count; i++)
                        {
                            objRecordContent.m_strAttendeeIDArr[i] = dtbValue.Rows[i]["ATTENDEEID"].ToString();
                            objRecordContent.m_strAttendeeNameArr[i] = dtbValue.Rows[i]["ATTENDEENAME"].ToString();
                        }
                    }



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

            }			
            //返回
			return lngRes;


			
	}
	}

}
