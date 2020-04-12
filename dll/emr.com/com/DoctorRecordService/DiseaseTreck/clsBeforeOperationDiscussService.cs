using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.DiseaseTrackService
{
	// 实现特殊记录的中间件。
	//病程记录－－术前记录
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsBeforeOperationDiscussService	: clsDiseaseTrackService
	{

		#region RegionName
		/// <summary>
		/// 从BeforeOperationDiscussRecord获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= "select CreateDate,OpenDate from BeforeOperationDiscussRecord Where InPatientID = ? and InPatientDate= ? and Status=0";

		/// <summary>
		/// 根据指定表单的信息，从BeforeOperationDiscussRecord和BFOPRDISCUSSRECCONT查找表单的内容。
		/// 用InPatientID ,InPatientDate ,CreateDate,Status = 0等条件，查询该记录的内容，查找Max(ModifyDate)。
		/// 如果返回lngRes = 1 && rows = 0,则证明此记录已被他人删除掉。
		/// </summary>
		private const string c_strGetRecordContentSQL= @"select a.*, b.*
															from BeforeOperationDiscussRecord a,
																BFOPRDISCUSSRECCONT          b
															where a.InPatientID = ?
															and a.InPatientDate = ?
															and a.OpenDate = ?
															and a.Status = 0
															and b.InPatientID = a.InPatientID
															and b.InPatientDate = a.InPatientDate
															and b.OpenDate = a.OpenDate
															and b.ModifyDate = (select Max(ModifyDate)
																					from BFOPRDISCUSSRECCONT
																					Where InPatientID = a.InPatientID
																					and InPatientDate = a.InPatientDate
																					and OpenDate = a.OpenDate) ";
																	
		/// <summary>
		/// 从BeforeOperationDiscussRecord中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select CreateUserID,OpenDate from BeforeOperationDiscussRecord Where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		//		/// <summary>
		//		/// 从BeforeOperationDiscussRecord获取已经存在记录的主要信息,获取修改表单的主要信息
		//		/// </summary>
		//		private const string c_strGetExistInfoSQL= "";

		/// <summary>
		/// 从BFOPRDISCUSSRECCONT获取指定表单的最后修改时间。
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate,b.ModifyUserID from BeforeOperationDiscussRecord a,BFOPRDISCUSSRECCONT b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from BFOPRDISCUSSRECCONT Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		//		/// <summary>
		//		/// 从BFOPRDISCUSSRECCONT获取修改表单的主要信息。
		//		/// </summary>
		//		private const string c_strGetModifyRecordSQL= "";

		/// <summary>
		/// 从BeforeOperationDiscussRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select DeActivedDate,DeActivedOperatorID from BeforeOperationDiscussRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";

		/// <summary>
		/// 添加记录到BeforeOperationDiscussRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  BeforeOperationDiscussRecord(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,IfConfirm,ConfirmReason,ConfirmReasonXML,Status,Address,AddressXML,DiscussContent,DiscussContentXML,SEQUENCE_INT,MARKSTATUS) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 添加记录到BFOPRDISCUSSRECCONT
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  BFOPRDISCUSSRECCONT(InPatientID,InPatientDate,OpenDate,ModifyDate,ModifyUserID,Address_Right,DiscussContent_Right,CompereID) 
				values(?,?,?,?,?,?,?,?)";

		
		/// <summary>
		/// 修改记录到BFOPRDISCUSSRECCONT
		/// </summary>
		private const string c_strModifyRecordSQL= "Update BeforeOperationDiscussRecord Set Address=?,AddressXML=? ,DiscussContent=?,DiscussContentXML=? ,SEQUENCE_INT=? ,MARKSTATUS=?  Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

		/// <summary>
		/// 修改记录到BFOPRDISCUSSRECCONT
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		
		/// <summary>
		/// 设置BeforeOperationDiscussRecord中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= "Update BeforeOperationDiscussRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";

		/// <summary>
		/// 从BeforeOperationDiscussRecord和BFOPRDISCUSSRECCONT获取LastModifyDate和FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.FirstPrintDate,b.ModifyDate from BeforeOperationDiscussRecord a,BFOPRDISCUSSRECCONT b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from BFOPRDISCUSSRECCONT Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";


		/// <summary>
		/// 更新BeforeOperationDiscussRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "Update  BeforeOperationDiscussRecord Set FirstPrintDate= ? Where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";

		/// <summary>
		/// 从BeforeOperationDiscussRecord获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select CreateDate,OpenDate from BeforeOperationDiscussRecord Where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";

		/// <summary>
		/// 从BeforeOperationDiscussRecord获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select CreateDate,OpenDate from BeforeOperationDiscussRecord Where InPatientID = ? and InPatientDate= ? and Status=1";

		
		private const string c_strGetDeleteRecordContentSQL= @"select a.*, b.*
																from BeforeOperationDiscussRecord a,
																	BFOPRDISCUSSRECCONT          b
																where a.InPatientID = ?
																and a.InPatientDate = ?
																and a.OpenDate = ?
																and a.Status = 1
																and b.InPatientID = a.InPatientID
																and b.InPatientDate = a.InPatientDate
																and b.OpenDate = a.OpenDate
																and b.ModifyDate = (select Max(ModifyDate)
																						from BFOPRDISCUSSRECCONT
																						Where InPatientID = a.InPatientID
																						and InPatientDate = a.InPatientDate
																						and OpenDate = a.OpenDate) ";
		/// <summary>
		/// 在bfoprdiscussrecattendee中获取指定ModifyDate的医师签名。
		/// </summary>
		private const string c_strGetDeleteAttendeeContentSQL= @"select sub2.AttendeeID, PBI.Lastname_Vchr as AttendeeName
																from BeforeOperationDiscussRecord a,
																	bfoprdiscussrecattendee      sub2,
																	t_bse_employee             PBI
																where a.InPatientID = ?
																and a.InPatientDate = ?
																and a.OpenDate = ?
																and a.Status = 1
																and sub2.AttendeeID = PBI.Empno_Chr
																and sub2.InPatientID = a.InPatientID
																and sub2.InPatientDate = a.InPatientDate
																and sub2.OpenDate = a.OpenDate
																and sub2.ModifyDate = (select Max(ModifyDate)
																							from bfoprdiscussrecattendee
																						Where InPatientID = a.InPatientID
																							and InPatientDate = a.InPatientDate
																							and OpenDate = a.OpenDate) ";
	
		
		private const string c_strGetDeleteNoterContentSQL=@"select sub2.NoterID, PBI.Lastname_Vchr as NoterName
																		from BeforeOperationDiscussRecord a,
																			BfOprDiscussRecNoter         sub2,
																			t_bse_employee             PBI
																		where a.InPatientID = ?
																		and a.InPatientDate = ?
																		and a.OpenDate = ?
																		and a.Status = 1
																		and sub2.NoterID = PBI.Empno_Chr
																		and sub2.InPatientID = a.InPatientID
																		and sub2.InPatientDate = a.InPatientDate
																		and sub2.OpenDate = a.OpenDate
																		and sub2.ModifyDate = (select Max(ModifyDate)
																									from BfOprDiscussRecNoter
																								Where InPatientID = a.InPatientID
																									and InPatientDate = a.InPatientDate
																									and OpenDate = a.OpenDate) ";
		// 在bfoprdiscussrecattendee中获取指定表单和ModifyDate的参加人员。
		private string c_strGetAttendeeContentSQL= @"select sub2.AttendeeID, PBI.Lastname_Vchr as AttendeeName
																		from BeforeOperationDiscussRecord a,
																			bfoprdiscussrecattendee      sub2,
																			t_bse_employee             PBI
																		where a.InPatientID = ?
																		and a.InPatientDate = ?
																		and a.OpenDate = ?
																		and a.Status = 0
																		and sub2.AttendeeID = PBI.Empno_Chr
																		and sub2.InPatientID = a.InPatientID
																		and sub2.InPatientDate = a.InPatientDate
																		and sub2.OpenDate = a.OpenDate
																		and sub2.ModifyDate = (select Max(ModifyDate)
																									from bfoprdiscussrecattendee
																								Where InPatientID = a.InPatientID
																									and InPatientDate = a.InPatientDate
																									and OpenDate = a.OpenDate) ";

		// 在BfOprDiscussRecNoter中获取指定表单和ModifyDate的记录者。
		private string c_strGetNoterContentSQL= @"select sub2.NoterID, PBI.Lastname_Vchr as NoterName
																	from BeforeOperationDiscussRecord a,
																		BfOprDiscussRecNoter         sub2,
																		t_bse_employee             PBI
																	where a.InPatientID = ?
																	and a.InPatientDate = ?
																	and a.OpenDate = ?
																	and a.Status = 0
																	and sub2.NoterID = PBI.Empno_Chr
																	and sub2.InPatientID = a.InPatientID
																	and sub2.InPatientDate = a.InPatientDate
																	and sub2.OpenDate = a.OpenDate
																	and sub2.ModifyDate = (select Max(ModifyDate)
																								from BfOprDiscussRecNoter
																							Where InPatientID = a.InPatientID
																								and InPatientDate = a.InPatientDate
																								and OpenDate = a.OpenDate) ";

		/// <summary>
		/// 添加记录到bfoprdiscussrecattendee
		/// </summary>
		private const string c_strAddNewRecordAttendeeSQL= @"insert into  bfoprdiscussrecattendee(InPatientID,InPatientDate,OpenDate,ModifyDate,AttendeeID) 
								values(?,?,?,?,?)";

		// 添加记录到BfOprDiscussRecNoter
		private const string c_strAddNewRecordNoterSQL =@"insert into  BfOprDiscussRecNoter(InPatientID,InPatientDate,OpenDate,ModifyDate,NoterID) 
								values(?,?,?,?,?)";
		
		// 修改记录到bfoprdiscussrecattendee
		private const string c_strModifyRecordAttendeeSQL=c_strAddNewRecordAttendeeSQL;

		// 修改记录到BfOprDiscussRecNoter
		private const string c_strModifyRecordNoterSQL=c_strAddNewRecordNoterSQL;
		#endregion

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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationDiscussService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //string strSQL = "select CreateDate,OpenDate from BeforeOperationDiscussRecord Where trim(InPatientID) = ? and InPatientDate= ? and Status=0";

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

        #region 更新数据库中的首次打印时间
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationDiscussService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

                //获取IDataParameter数组
                //string strSQL = "Update  BeforeOperationDiscussRecord Set FirstPrintDate= ? Where trim(InPatientID)= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";


                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationDiscussService", "m_lngGetDeleteRecordTimeList");
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
                ;

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
		#endregion

        #region 获取病人的已经被删除记录时间列表
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationDiscussService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
		#endregion

		#region 获取指定记录的内容
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
                    clsBeforeOperationDiscussRecordContent objRecordContent = new clsBeforeOperationDiscussRecordContent();
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
                    objRecordContent.m_intMarkStatus = int.Parse(dtbValue.Rows[0]["MARKSTATUS"].ToString());

                    #region MyRegion
                    //					objRecordContent.m_strCompereID=dtbValue.Rows[0]["COMPEREID"].ToString();
                    //					objRecordContent.m_strCompereName=dtbValue.Rows[0]["COMPERENAME"].ToString();

                    //					dtbValue = new DataTable();				
                    ////					//按顺序给IDataParameter赋值
                    ////					for(int i=0;i<objDPArr.Length;i++)
                    ////						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    //					objHRPServ.CreateDatabaseParameter(3,out objDPArr);		
                    //					objDPArr[0].Value=p_strInPatientID;
                    //					objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
                    //					objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
                    //					long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetAttendeeContentSQL,ref dtbValue,objDPArr);
                    //					//从DataTable.Rows中获取结果
                    //					if(lngRes2 > 0 && dtbValue.Rows.Count >0)
                    //					{
                    //						objRecordContent.m_strAttendeeIDArr=new string[dtbValue.Rows.Count];
                    //						objRecordContent.m_strAttendeeNameArr=new string[dtbValue.Rows.Count];
                    //						for(int i=0;i<dtbValue.Rows.Count;i++)
                    //						{
                    //							objRecordContent.m_strAttendeeIDArr[i]=dtbValue.Rows[i]["ATTENDEEID"].ToString();
                    //							objRecordContent.m_strAttendeeNameArr[i]=dtbValue.Rows[i]["ATTENDEENAME"].ToString();
                    //						}
                    //					}
                    //
                    //					dtbValue = new DataTable();
                    ////					//按顺序给IDataParameter赋值
                    ////					for(int i=0;i<objDPArr.Length;i++)
                    ////						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    //					objHRPServ.CreateDatabaseParameter(3,out objDPArr);		
                    //					objDPArr[0].Value=p_strInPatientID;
                    //					objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
                    //					objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
                    //					long lngRes3 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetNoterContentSQL,ref dtbValue,objDPArr);
                    //					//从DataTable.Rows中获取结果
                    //					if(lngRes3 > 0 && dtbValue.Rows.Count >0)
                    //					{
                    //						objRecordContent.m_strNoterIDArr=new string[dtbValue.Rows.Count];
                    //						objRecordContent.m_strNoterNameArr=new string[dtbValue.Rows.Count];
                    //						for(int i=0;i<dtbValue.Rows.Count;i++)
                    //						{
                    //							objRecordContent.m_strNoterIDArr[i]=dtbValue.Rows[i]["NOTERID"].ToString();
                    //							objRecordContent.m_strNoterNameArr[i]=dtbValue.Rows[i]["NOTERNAME"].ToString();
                    //						}
                    //					} 
                    #endregion
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
			p_objModifyInfo=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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

                clsBeforeOperationDiscussRecordContent objContent = (clsBeforeOperationDiscussRecordContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(15, out objDPArr);

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
                objDPArr[13].Value = lngSequence;
                objDPArr[14].Value = objContent.m_intMarkStatus;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(8, out objDPArr2);
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
                objDPArr2[7].Value = objContent.m_strCompereID;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);

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
                        //						if(lngRes<=0)
                        //							return lngRes;
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
		#endregion

        #region 查看当前记录是否最新的记录。
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
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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

            }			//返回
			return lngRes;
				
		}
		#endregion

        #region 把新修改的内容保存到数据库。更新主表,添加子表.
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

                clsBeforeOperationDiscussRecordContent objContent = (clsBeforeOperationDiscussRecordContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(9, out objDPArr);
                objDPArr[0].Value = objContent.m_strAddress;
                objDPArr[1].Value = objContent.m_strAddressXML;
                objDPArr[2].Value = objContent.m_strDiscussContent;
                objDPArr[3].Value = objContent.m_strDiscussContentXML;
                objDPArr[4].Value = lngSequence;
                objDPArr[5].Value = objContent.m_intMarkStatus;

                objDPArr[6].Value = objContent.m_strInPatientID;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = objContent.m_dtmInPatientDate;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = objContent.m_dtmOpenDate;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(8, out objDPArr2);
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
                objDPArr2[7].Value = objContent.m_strCompereID;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strAttendeeIDArr != null)
                {

                    IDataParameter[] objDPArr3 = null;


                    for (int j = 0; j < objContent.m_strAttendeeIDArr.Length; j++)
                    {
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

                    IDataParameter[] objDPArr3 = null;


                    for (int j = 0; j < objContent.m_strNoterIDArr.Length; j++)
                    {
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

            }			//返回
            return lngRes;


        } 
        #endregion

        #region 把记录从数据中“删除”。
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
                    clsBeforeOperationDiscussRecordContent objRecordContent = new clsBeforeOperationDiscussRecordContent();
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
                //返回
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
        #endregion

	}// END CLASS DEFINITION clsGeneralDiseaseService

}
