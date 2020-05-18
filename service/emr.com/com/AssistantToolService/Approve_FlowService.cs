using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.Approve_FlowService
{
	/// <summary>
	/// Summary description for Class1.
	/// 审核流程中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsApprove_FlowService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		private string strGenerateDocID()
		{
			string strOutput="";
			new clsHRPTableService().lngGenerateID (20,"Doc_ID","Approve_Flow_Doc_Info",out strOutput );
			if(strOutput ==null || strOutput =="")
				strOutput ="00000000000000000001";
			return(strOutput );
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strSubjectID"></param>
		/// <param name="p_objApprove_FlowValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long strGetApprove_Flow_ID_From_SubjectID( string p_strSubjectID,out clsApprove_FlowValue p_objApprove_FlowValue)
		{
			p_objApprove_FlowValue=null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsAPACHEIIValuation","add_new_record");
			//if(lngCheckRes <= 0)
				////return lngCheckRes;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = "select approve_flow_id, subject_id, create_date, end_date, skipgrade, description from approve_flow where subject_id=?";
                DataTable objDataTableResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSubjectID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref objDataTableResult, objDPArr);
                if (lngRes > 0 && objDataTableResult.Rows.Count == 1)
                {
                    p_objApprove_FlowValue = new clsApprove_FlowValue();
                    p_objApprove_FlowValue.m_strApprove_Flow_ID = objDataTableResult.Rows[0]["APPROVE_FLOW_ID"].ToString();
                    p_objApprove_FlowValue.m_strSubject_ID = objDataTableResult.Rows[0]["SUBJECT_ID"].ToString();
                    p_objApprove_FlowValue.m_strCreate_Date = objDataTableResult.Rows[0]["CREATE_DATE"].ToString();
                    p_objApprove_FlowValue.m_strEnd_Date = objDataTableResult.Rows[0]["END_DATE"].ToString();
                    p_objApprove_FlowValue.m_strSkipGrade = objDataTableResult.Rows[0]["SKIPGRADE"].ToString();
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
		/// 
		/// </summary>
		/// <param name="p_strApprove_FlowID"></param>
		/// <returns></returns>
		[AutoComplete]
		private long lngGetApprove_FlowStepsCount(string p_strApprove_FlowID)
		{
			long lngRes = 0;
			string strOutput="0";
			
			clsHRPTableService objHRPServ = new clsHRPTableService();
		    try
			{
                string strSql = "select count(approve_flow_id) as stepcount from approve_flow_step where approve_flow_id=?";
				DataTable objDataTableResult=new DataTable ();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strApprove_FlowID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref objDataTableResult, objDPArr);
				if(lngRes > 0 && objDataTableResult.Rows.Count == 1)
				{
					strOutput=objDataTableResult.Rows[0]["STEPCOUNT"].ToString();
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			} 
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			return(long.Parse (strOutput));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strSubject_ID"></param>
		/// <param name="p_strInPatient_ID"></param>
		/// <param name="p_strInPatient_Date"></param>
		/// <param name="p_strOpen_Date"></param>
		/// <param name="p_strDept_ID"></param>
		/// <param name="lngEff"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngApproveDocument( string p_strEmployeeID,string p_strSubject_ID,string p_strInPatient_ID,string p_strInPatient_Date,string p_strOpen_Date,string p_strDept_ID,ref long lngEff)
		{		
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsAPACHEIIValuation","add_new_record");
			//if(lngCheckRes <= 0)
				////return lngCheckRes;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                clsApprove_Flow_Doc_InfoValue objApprove_Flow_Doc_InfoValue = new clsApprove_Flow_Doc_InfoValue();
                clsApprove_Flow_Doc_StatusValue objApprove_Flow_Doc_StatusValue = new clsApprove_Flow_Doc_StatusValue();
                clsApprove_FlowValue objApprove_FlowValue = null;
                //没有员工号
                if (p_strEmployeeID == null || p_strEmployeeID.Length <= 0)
                {
                    return ((long)enmApproveResult.EmployeeID_Error);
                }

                //获得该单的审核流ID
                strGetApprove_Flow_ID_From_SubjectID( p_strSubject_ID, out objApprove_FlowValue);
                if (objApprove_FlowValue == null || objApprove_FlowValue.m_strApprove_Flow_ID == null || objApprove_FlowValue.m_strApprove_Flow_ID == "")
                {
                    //MessageBox.Show ("没有找到该类单的审核流定义");
                    return ((long)enmApproveResult.System_Not_Define);
                }
                int intSequence = 0;	//当前进行的审核步骤
                #region 检查是否新建的单,新建的单设置初始状态为"生成",否则设置当前状态 刘颖源,2003-6-19 14:16:18
                string strSQL = "select a.doc_id,a.approve_flow_id,b.set_date,b.sequence from approve_flow_doc_info a,approve_flow_doc_status b " +
                    "where a.doc_id =b.doc_id and a.inpatientid=? and a.inpatientdate=? and a.opendate=? and a.subject_id=? " +
                    " and  b.set_date in (select max(set_date) from approve_flow_doc_status group by doc_id )"
                    ;
                DataTable objDataTableResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatient_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatient_Date);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpen_Date);
                objDPArr[3].Value = p_strSubject_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                bool blnIsNewDocument = false;
                if (lngRes <= 0 || objDataTableResult.Rows.Count <= 0)		//新建的单
                {
                    objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID = objApprove_FlowValue.m_strApprove_Flow_ID;
                    objApprove_Flow_Doc_InfoValue.m_strDept_ID = p_strDept_ID;
                    objApprove_Flow_Doc_InfoValue.m_strDoc_ID = strGenerateDocID();
                    objApprove_Flow_Doc_InfoValue.m_strInPatientDate = p_strInPatient_Date;
                    objApprove_Flow_Doc_InfoValue.m_strInPatientID = p_strInPatient_ID;
                    objApprove_Flow_Doc_InfoValue.m_strOpenDate = p_strOpen_Date;
                    objApprove_Flow_Doc_InfoValue.m_strSubject_ID = p_strSubject_ID;
                    blnIsNewDocument = true;

                }
                else
                {
                    objApprove_Flow_Doc_InfoValue.m_strDoc_ID = objDataTableResult.Rows[0]["DOC_ID"].ToString();
                    objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID = objDataTableResult.Rows[0]["APPROVE_FLOW_ID"].ToString();
                    intSequence = int.Parse(objDataTableResult.Rows[0]["SEQUENCE"].ToString());
                    if (intSequence < 0)
                    {
                        //MessageBox.Show ("该单已经删除!");
                        return ((long)enmApproveResult.Document_Has_Been_Deleted);
                    }
                    blnIsNewDocument = false;
                }
                #endregion

                //获得按照该审核流总的步骤数目
                long intStepsCount = lngGetApprove_FlowStepsCount(objApprove_FlowValue.m_strApprove_Flow_ID);
                if (intSequence >= intStepsCount)						//该单已经经过最终审核，不能在往下审核
                {
                    //MessageBox.Show ("该单已经经过最终审核，不能在往下审核");				
                    return ((long)enmApproveResult.Document_Has_Been_Finished);
                }

                #region 获得当前用户可以审核哪一步
                string strCurRole = clsDatabaseSQLConvert.s_StrTop1 + @" sequence_no
								from approve_flow_role 
								where approve_flow_id = ? and role_id in (select role_id from role_employee where status=0 and employeeid = ?) order by sequence_no desc" + clsDatabaseSQLConvert.s_StrRownum;

                DataTable objDataTableRoles = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objApprove_FlowValue.m_strApprove_Flow_ID;
                objDPArr[1].Value = p_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCurRole, ref objDataTableRoles,objDPArr);
                if (lngRes <= 0 || objDataTableRoles.Rows.Count <= 0)				//没有权限操作
                {
                    return ((long)enmApproveResult.No_Purview);
                }
                else
                {
                    if (int.Parse(objDataTableRoles.Rows[0]["SEQUENCE_NO"].ToString()) <= intSequence)
                        return ((long)enmApproveResult.No_Purview);
                    else
                        intSequence = int.Parse(objDataTableRoles.Rows[0]["SEQUENCE_NO"].ToString());
                }
                #endregion

                #region 检验当前审核流中的第n步能否由该用户完成,刘颖源,2003-6-19 18:51:33
                //			string strRoleSql="select Role_ID from approve_flow_step a " + 
                //				" left join approve_flow_role b on (a.role_group_id=b.role_group_id and a.approve_flow_id=b.approve_flow_id and a.status_id=b.status_id and a.sequence_no=b.sequence_no) " + 
                //				" where a.approve_flow_id='" + objApprove_FlowValue.m_strApprove_Flow_ID + "' and a.Sequence_No=" + intSequence + " and b.Role_id in " + 
                //				" (select Role_ID from Role_Employee where status=0 and EmployeeID='" + p_strEmployeeID + "')";
                //			DataTable objDataTableRoles=new DataTable ();
                //			lngRes = new clsHRPTableService().DoGetDataTable (strRoleSql, ref objDataTableRoles);
                //			if(lngRes <=0 || objDataTableRoles.Rows.Count <=0)				//没有权限操作
                //			{
                //				//MessageBox.Show ("没有权限操作!");
                //				return((long)enmApproveResult.No_Purview );
                //			}
                #endregion

                #region 保存表 Approve_Flow_Doc_Info,刘颖源,2003-6-19 13:48:26
                if (blnIsNewDocument && objApprove_Flow_Doc_InfoValue != null)
                {
                    strSQL = "insert into approve_flow_doc_info(doc_id, approve_flow_id, dept_id, inpatientid, inpatientdate, opendate, subject_id) values(?, ?, ?, ?, ?, ?, ?)";
                    //					IDataParameter objParameterDoc_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterDoc_ID.Value = objApprove_Flow_Doc_InfoValue.m_strDoc_ID;
                    //					IDataParameter objParameterApprove_Flow_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterApprove_Flow_ID.Value = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                    //					IDataParameter objParameterDept_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterDept_ID.Value = objApprove_Flow_Doc_InfoValue.m_strDept_ID;
                    //					IDataParameter objParameterInPatientID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterInPatientID.Value = objApprove_Flow_Doc_InfoValue.m_strInPatientID;
                    //					IDataParameter objParameterInPatientDate = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterInPatientDate.Value = objApprove_Flow_Doc_InfoValue.m_strInPatientDate;
                    //					IDataParameter objParameterOpenDate = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterOpenDate.Value = objApprove_Flow_Doc_InfoValue.m_strOpenDate;
                    //					IDataParameter objParameterSubject_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSubject_ID.Value = objApprove_Flow_Doc_InfoValue.m_strSubject_ID;							

                    //IDataParameter[] objDPArr = null;//new IDataParameter[7];
                    objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                    objDPArr[0].Value = objApprove_Flow_Doc_InfoValue.m_strDoc_ID;
                    objDPArr[1].Value = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                    objDPArr[2].Value = objApprove_Flow_Doc_InfoValue.m_strDept_ID;
                    objDPArr[3].Value = objApprove_Flow_Doc_InfoValue.m_strInPatientID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(objApprove_Flow_Doc_InfoValue.m_strInPatientDate);
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = DateTime.Parse(objApprove_Flow_Doc_InfoValue.m_strOpenDate);
                    objDPArr[6].Value = objApprove_Flow_Doc_InfoValue.m_strSubject_ID;


                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
                #endregion

                objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objApprove_Flow_Doc_StatusValue.m_strDoc_ID = objApprove_Flow_Doc_InfoValue.m_strDoc_ID;
                objApprove_Flow_Doc_StatusValue.m_strSequence = intSequence.ToString();
                objApprove_Flow_Doc_StatusValue.m_strSet_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID = p_strEmployeeID;

                #region 保存表 Approve_Flow_Doc_Status,刘颖源,2003-6-19 19:07:00
                if (objApprove_Flow_Doc_StatusValue != null)
                {
                    strSQL = "insert into approve_flow_doc_status(doc_id, approve_flow_id, set_date, set_employeeid, sequence) values(?, ?, ?, ?, ?)";
                    //					IDataParameter objParameterDoc_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterDoc_ID.Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    //					IDataParameter objParameterApprove_Flow_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterApprove_Flow_ID.Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    //					IDataParameter objParameterSet_Date = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_Date.Value = objApprove_Flow_Doc_StatusValue.m_strSet_Date;
                    //					IDataParameter objParameterSet_EmployeeID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_EmployeeID.Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    //					IDataParameter objParameterSequence = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSequence.Value = objApprove_Flow_Doc_StatusValue.m_strSequence;


                    IDataParameter[] objApprove_Flow_Doc_StatusArr = null;//new IDataParameter[5];
                    objHRPServ.CreateDatabaseParameter(5, out objApprove_Flow_Doc_StatusArr);
                    objApprove_Flow_Doc_StatusArr[0].Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    objApprove_Flow_Doc_StatusArr[1].Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    objApprove_Flow_Doc_StatusArr[2].DbType = DbType.DateTime;
                    objApprove_Flow_Doc_StatusArr[2].Value = DateTime.Parse(objApprove_Flow_Doc_StatusValue.m_strSet_Date);
                    objApprove_Flow_Doc_StatusArr[3].Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    objApprove_Flow_Doc_StatusArr[4].Value = objApprove_Flow_Doc_StatusValue.m_strSequence;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objApprove_Flow_Doc_StatusArr);
                }
                #endregion


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
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strSubject_ID"></param>
		/// <param name="p_strInPatient_ID"></param>
		/// <param name="p_strInPatient_Date"></param>
		/// <param name="p_strOpen_Date"></param>
		/// <param name="p_strDept_ID"></param>
		/// <param name="lngEff"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngUntreadDocumentOneLevel( string p_strEmployeeID,string p_strSubject_ID,string p_strInPatient_ID,string p_strInPatient_Date,string p_strOpen_Date,string p_strDept_ID,ref long lngEff)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsAPACHEIIValuation","add_new_record");
			//if(lngCheckRes <= 0)
				////return lngCheckRes;

			int intSequence=0;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                clsApprove_Flow_Doc_StatusValue objApprove_Flow_Doc_StatusValue = new clsApprove_Flow_Doc_StatusValue();
                clsApprove_Flow_Doc_InfoValue objApprove_Flow_Doc_InfoValue = new clsApprove_Flow_Doc_InfoValue();

                #region 找出对应那张单的最后一次审核
                string strSQL = @"select a.doc_id,a.approve_flow_id,b.set_date,b.sequence from approve_flow_doc_info a,approve_flow_doc_status b 
                    where a.doc_id =b.doc_id and a.inpatientid=? and a.inpatientdate=? and a.opendate=? and a.subject_id=? 
                     and  b.set_date in (select max(set_date) from approve_flow_doc_status group by doc_id )"
                    ;
                DataTable objDataTableResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatient_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatient_Date);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpen_Date);
                objDPArr[3].Value = p_strSubject_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult,objDPArr);
                if (lngRes <= 0 || objDataTableResult.Rows.Count <= 0)		//新建的单
                {
                    //MessageBox.Show ("没有找到该单进行审核的信息");
                    return ((long)enmApproveResult.Not_Found_Approve_Info);
                }
                else
                {
                    objApprove_Flow_Doc_InfoValue.m_strDoc_ID = objDataTableResult.Rows[0]["DOC_ID"].ToString();
                    objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID = objDataTableResult.Rows[0]["APPROVE_FLOW_ID"].ToString();
                    intSequence = int.Parse(objDataTableResult.Rows[0]["SEQUENCE"].ToString());
                    if (intSequence < 0)
                    {
                        //MessageBox.Show ("该单已经删除!");
                        return ((long)enmApproveResult.Document_Has_Been_Deleted);
                    }
                    intSequence--;
                    if (intSequence < 0)
                    {
                        //MessageBox.Show ("已经退回到最上一级!");
                        return ((long)enmApproveResult.Is_Top_Level);
                    }
                }
                #endregion

                objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objApprove_Flow_Doc_StatusValue.m_strDoc_ID = objApprove_Flow_Doc_InfoValue.m_strDoc_ID;
                objApprove_Flow_Doc_StatusValue.m_strSequence = intSequence.ToString();
                objApprove_Flow_Doc_StatusValue.m_strSet_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID = p_strEmployeeID;

                #region 获得当前用户可以审核哪一步
                string strCurRole = clsDatabaseSQLConvert.s_StrTop1 + @" sequence_no
								from approve_flow_role 
								where approve_flow_id = ? and role_id in (select role_id from role_employee where status=0 and employeeid = ?) order by sequence_no desc" + clsDatabaseSQLConvert.s_StrRownum;

                DataTable objDataTableRoles = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objDPArr[1].Value = p_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCurRole, ref objDataTableRoles, objDPArr);
                if (lngRes <= 0 || objDataTableRoles.Rows.Count <= 0)				//没有权限操作
                {
                    return ((long)enmApproveResult.No_Purview);
                }
                else
                {
                    if (int.Parse(objDataTableRoles.Rows[0]["SEQUENCE_NO"].ToString()) < (intSequence + 1))
                        return ((long)enmApproveResult.No_Purview);
                }
                #endregion

                #region 检验当前审核流中的第n步能否由该用户完成
                //			string strRoleSql="select Role_ID from approve_flow_step a " + 
                //				" left join approve_flow_role b on (a.role_group_id=b.role_group_id and a.approve_flow_id=b.approve_flow_id and a.status_id=b.status_id and a.sequence_no=b.sequence_no) " + 
                //				" where a.approve_flow_id='" + objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID + "' and a.Sequence_No=" + (intSequence+1) + " and b.Role_id in " + 
                //				" (select Role_ID from Role_Employee where status=0 and EmployeeID='" + p_strEmployeeID + "')";
                //			DataTable objDataTableRoles=new DataTable ();
                //			lngRes = new clsHRPTableService().DoGetDataTable (strRoleSql, ref objDataTableRoles);
                //			if(lngRes <=0 || objDataTableRoles.Rows.Count <=0)				//没有权限操作
                //			{
                //				//MessageBox.Show ("没有权限操作!");
                //				return((long)enmApproveResult.No_Purview );
                //			}
                #endregion


                #region 保存表 Approve_Flow_Doc_Status,刘颖源,2003-6-19 19:07:00
                if (objApprove_Flow_Doc_StatusValue != null)
                {
                    strSQL = @"insert into approve_flow_doc_status(Doc_ID, Approve_Flow_ID, Set_Date, Set_EmployeeID, Sequence) values(?, ?, ?, ?, ?)";
                    //					IDataParameter objParameterDoc_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterDoc_ID.Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    //					IDataParameter objParameterApprove_Flow_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterApprove_Flow_ID.Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    //					IDataParameter objParameterSet_Date = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_Date.Value = objApprove_Flow_Doc_StatusValue.m_strSet_Date;
                    //					IDataParameter objParameterSet_EmployeeID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_EmployeeID.Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    //					IDataParameter objParameterSequence = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSequence.Value = objApprove_Flow_Doc_StatusValue.m_strSequence;


                    IDataParameter[] objApprove_Flow_Doc_StatusArr = null;//new IDataParameter[5];
                    objHRPServ.CreateDatabaseParameter(5, out objApprove_Flow_Doc_StatusArr);
                    objApprove_Flow_Doc_StatusArr[0].Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    objApprove_Flow_Doc_StatusArr[1].Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    objApprove_Flow_Doc_StatusArr[2].DbType = DbType.DateTime;
                    objApprove_Flow_Doc_StatusArr[2].Value = DateTime.Parse(objApprove_Flow_Doc_StatusValue.m_strSet_Date);
                    objApprove_Flow_Doc_StatusArr[3].Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    objApprove_Flow_Doc_StatusArr[4].Value = objApprove_Flow_Doc_StatusValue.m_strSequence;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objApprove_Flow_Doc_StatusArr);
                }
                #endregion


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
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strSubject_ID"></param>
		/// <param name="p_strInPatient_ID"></param>
		/// <param name="p_strInPatient_Date"></param>
		/// <param name="p_strOpen_Date"></param>
		/// <param name="p_strDept_ID"></param>
		/// <param name="lngEff"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngUntreadDocumentAll( string p_strEmployeeID,string p_strSubject_ID,string p_strInPatient_ID,string p_strInPatient_Date,string p_strOpen_Date,string p_strDept_ID,ref long lngEff)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsAPACHEIIValuation","add_new_record");
			//if(lngCheckRes <= 0)
				////return lngCheckRes;

			int intSequence=0;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                clsApprove_Flow_Doc_StatusValue objApprove_Flow_Doc_StatusValue = new clsApprove_Flow_Doc_StatusValue();
                clsApprove_Flow_Doc_InfoValue objApprove_Flow_Doc_InfoValue = new clsApprove_Flow_Doc_InfoValue();

                string strSQL = @"select a.doc_id,a.approve_flow_id,b.set_date,b.sequence from approve_flow_doc_info a,approve_flow_doc_status b 
                    where a.doc_id =b.doc_id and a.inpatientid=? and a.inpatientdate=? and a.opendate=? and a.subject_id=? 
                     and  b.set_date in (select max(set_date) from approve_flow_doc_status group by doc_id )"
                    ;
                DataTable objDataTableResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatient_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatient_Date);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpen_Date);
                objDPArr[3].Value = p_strSubject_ID;


                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult,objDPArr);
                if (lngRes <= 0 || objDataTableResult.Rows.Count <= 0)		//新建的单
                {
                    //MessageBox.Show ("没有找到该单进行审核的信息");
                    return ((long)enmApproveResult.Not_Found_Approve_Info);
                }
                else
                {
                    objApprove_Flow_Doc_InfoValue.m_strDoc_ID = objDataTableResult.Rows[0]["DOC_ID"].ToString();
                    objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID = objDataTableResult.Rows[0]["APPROVE_FLOW_ID"].ToString();
                    intSequence = int.Parse(objDataTableResult.Rows[0]["SEQUENCE"].ToString());
                    if (intSequence < 0)
                    {
                        //MessageBox.Show ("该单已经删除");
                        return ((long)enmApproveResult.Document_Has_Been_Deleted);
                    }
                    //				intSequence =1;
                    if (intSequence <= 0)
                    {
                        //MessageBox.Show ("已经退回到最上一级!");
                        return ((long)enmApproveResult.Is_Top_Level);
                    }
                }

                objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objApprove_Flow_Doc_StatusValue.m_strDoc_ID = objApprove_Flow_Doc_InfoValue.m_strDoc_ID;
                objApprove_Flow_Doc_StatusValue.m_strSequence = intSequence.ToString();
                objApprove_Flow_Doc_StatusValue.m_strSet_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID = p_strEmployeeID;

                #region 获得当前用户可以审核哪一步
                string strCurRole = clsDatabaseSQLConvert.s_StrTop1 + @" sequence_no
								from approve_flow_role 
								where approve_flow_id = ? and role_id in (select role_id from role_employee where status=0 and employeeid = ?) order by sequence_no desc" + clsDatabaseSQLConvert.s_StrRownum;

                DataTable objDataTableRoles = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objDPArr[1].Value = p_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCurRole, ref objDataTableRoles,objDPArr);
                if (lngRes <= 0 || objDataTableRoles.Rows.Count <= 0)				//没有权限操作
                {
                    return ((long)enmApproveResult.No_Purview);
                }
                else
                {
                    if (int.Parse(objDataTableRoles.Rows[0]["SEQUENCE_NO"].ToString()) < intSequence)
                        return ((long)enmApproveResult.No_Purview);
                }
                #endregion

                #region 保存表 Approve_Flow_Doc_Status,刘颖源,2003-6-19 19:07:00
                if (objApprove_Flow_Doc_StatusValue != null)
                {
                    strSQL = @"insert into Approve_Flow_Doc_Status(Doc_ID, Approve_Flow_ID, Set_Date, Set_EmployeeID, Sequence) values(?, ?, ?, ?, ?)";
                    //					IDataParameter objParameterDoc_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterDoc_ID.Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    //					IDataParameter objParameterApprove_Flow_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterApprove_Flow_ID.Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    //					IDataParameter objParameterSet_Date = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_Date.Value = objApprove_Flow_Doc_StatusValue.m_strSet_Date;
                    //					IDataParameter objParameterSet_EmployeeID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_EmployeeID.Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    //					IDataParameter objParameterSequence = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSequence.Value = 0;


                    IDataParameter[] objApprove_Flow_Doc_StatusArr = null;//new IDataParameter[5];
                    objHRPServ.CreateDatabaseParameter(5, out objApprove_Flow_Doc_StatusArr);
                    objApprove_Flow_Doc_StatusArr[0].Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    objApprove_Flow_Doc_StatusArr[1].Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    objApprove_Flow_Doc_StatusArr[2].DbType = DbType.DateTime;
                    objApprove_Flow_Doc_StatusArr[2].Value = DateTime.Parse(objApprove_Flow_Doc_StatusValue.m_strSet_Date);
                    objApprove_Flow_Doc_StatusArr[3].Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    objApprove_Flow_Doc_StatusArr[4].Value = 0;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objApprove_Flow_Doc_StatusArr);
                }
                #endregion


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
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strSubject_ID"></param>
		/// <param name="p_strInPatient_ID"></param>
		/// <param name="p_strInPatient_Date"></param>
		/// <param name="p_strOpen_Date"></param>
		/// <param name="p_strDept_ID"></param>
		/// <param name="lngEff"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngDeleteDocument( string p_strEmployeeID,string p_strSubject_ID,string p_strInPatient_ID,string p_strInPatient_Date,string p_strOpen_Date,string p_strDept_ID,ref long lngEff)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsAPACHEIIValuation","add_new_record");
			//if(lngCheckRes <= 0)
				////return lngCheckRes;

			int intSequence=-1;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                clsApprove_Flow_Doc_StatusValue objApprove_Flow_Doc_StatusValue = new clsApprove_Flow_Doc_StatusValue();
                clsApprove_Flow_Doc_InfoValue objApprove_Flow_Doc_InfoValue = new clsApprove_Flow_Doc_InfoValue();

                string strSQL = @"select a.doc_id,a.approve_flow_id,b.set_date,b.sequence from approve_flow_doc_info a,approve_flow_doc_status b 
                    where a.doc_id =b.doc_id and a.inpatientid=? and a.inpatientdate=? and a.opendate=? and a.subject_id=? 
                    and  b.set_date in (select max(set_date) from approve_flow_doc_status group by doc_id)"
                    ;
                DataTable objDataTableResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatient_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatient_Date);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpen_Date);
                objDPArr[3].Value = p_strSubject_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);
                if (lngRes <= 0 || objDataTableResult.Rows.Count <= 0)		//新建的单
                {
                    //MessageBox.Show ("没有找到该单进行审核的信息");
                    return ((long)enmApproveResult.Not_Found_Approve_Info);
                }
                else
                {
                    objApprove_Flow_Doc_InfoValue.m_strDoc_ID = objDataTableResult.Rows[0]["DOC_ID"].ToString();
                    objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID = objDataTableResult.Rows[0]["APPROVE_FLOW_ID"].ToString();
                    intSequence = int.Parse(objDataTableResult.Rows[0]["SEQUENCE"].ToString());
                    if (intSequence < 0)
                    {
                        //MessageBox.Show ("该单已经删除!");
                        return ((long)enmApproveResult.Document_Has_Been_Deleted);
                    }
                    intSequence = -1;
                }

                objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objApprove_Flow_Doc_StatusValue.m_strDoc_ID = objApprove_Flow_Doc_InfoValue.m_strDoc_ID;
                objApprove_Flow_Doc_StatusValue.m_strSequence = intSequence.ToString();
                objApprove_Flow_Doc_StatusValue.m_strSet_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID = p_strEmployeeID;

                #region 保存表 Approve_Flow_Doc_Status,刘颖源,2003-6-19 19:07:00
                if (objApprove_Flow_Doc_StatusValue != null)
                {
                    strSQL = @"insert into Approve_Flow_Doc_Status(Doc_ID, Approve_Flow_ID, Set_Date, Set_EmployeeID, Sequence) values(?, ?, ?, ?, ?)";
                    //					IDataParameter objParameterDoc_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterDoc_ID.Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    //					IDataParameter objParameterApprove_Flow_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterApprove_Flow_ID.Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    //					IDataParameter objParameterSet_Date = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_Date.Value = objApprove_Flow_Doc_StatusValue.m_strSet_Date;
                    //					IDataParameter objParameterSet_EmployeeID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_EmployeeID.Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    //					IDataParameter objParameterSequence = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSequence.Value = objApprove_Flow_Doc_StatusValue.m_strSequence;


                    IDataParameter[] objApprove_Flow_Doc_StatusArr = null;//new IDataParameter[5];
                    objHRPServ.CreateDatabaseParameter(5, out objApprove_Flow_Doc_StatusArr);
                    objApprove_Flow_Doc_StatusArr[0].Value = objApprove_Flow_Doc_StatusValue.m_strDoc_ID;
                    objApprove_Flow_Doc_StatusArr[1].Value = objApprove_Flow_Doc_StatusValue.m_strApprove_Flow_ID;
                    objApprove_Flow_Doc_StatusArr[2].DbType = DbType.DateTime;
                    objApprove_Flow_Doc_StatusArr[2].Value = DateTime.Parse(objApprove_Flow_Doc_StatusValue.m_strSet_Date);
                    objApprove_Flow_Doc_StatusArr[3].Value = objApprove_Flow_Doc_StatusValue.m_strSet_EmployeeID;
                    objApprove_Flow_Doc_StatusArr[4].Value = objApprove_Flow_Doc_StatusValue.m_strSequence;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objApprove_Flow_Doc_StatusArr);
                }
                #endregion
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

            } return lngRes;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strSubject_ID"></param>
		/// <param name="p_strInPatient_ID"></param>
		/// <param name="p_strInPatient_Date"></param>
		/// <param name="p_strOpen_Date"></param>
		/// <param name="p_strDept_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public bool lngCanYouDoIt( string p_strEmployeeID,string p_strSubject_ID,string p_strInPatient_ID,string p_strInPatient_Date,string p_strOpen_Date,string p_strDept_ID)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsAPACHEIIValuation","add_new_record");
			//if(lngCheckRes <= 0)
                //return false;	

			int intSequence=0;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                clsApprove_Flow_Doc_StatusValue objApprove_Flow_Doc_StatusValue = new clsApprove_Flow_Doc_StatusValue();
                clsApprove_Flow_Doc_InfoValue objApprove_Flow_Doc_InfoValue = new clsApprove_Flow_Doc_InfoValue();

                #region 找出对应那张单的最后一次审核
                string strSQL = @"select a.doc_id,a.approve_flow_id,b.set_date,b.sequence from approve_flow_doc_info a,approve_flow_doc_status b 
                    where a.doc_id =b.doc_id and a.inpatientid=? and a.inpatientdate=? and a.opendate=? and a.subject_id=? 
                    and  b.set_date in (select max(set_date) from approve_flow_doc_status group by doc_id )"
                    ;
                DataTable objDataTableResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatient_ID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatient_Date);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpen_Date);
                objDPArr[3].Value = p_strSubject_ID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult,objDPArr);
                if (lngRes <= 0 || objDataTableResult.Rows.Count <= 0)		//新建的单
                {
                    //("没有找到该单进行审核的信息");
                    return true;
                }
                else
                {
                    objApprove_Flow_Doc_InfoValue.m_strDoc_ID = objDataTableResult.Rows[0]["DOC_ID"].ToString();
                    objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID = objDataTableResult.Rows[0]["APPROVE_FLOW_ID"].ToString();
                    intSequence = int.Parse(objDataTableResult.Rows[0]["SEQUENCE"].ToString());

                    if (intSequence == 0)//审核已退到最低层次
                        return true;
                }
                #endregion

                #region 获得当前用户可以审核哪一步
                string strCurRole = clsDatabaseSQLConvert.s_StrTop1 + @" sequence_no
								from approve_flow_role 
								where approve_flow_id = ? and role_id in (select role_id from role_employee where status=0 and employeeid = ?) order by sequence_no desc" + clsDatabaseSQLConvert.s_StrRownum;

                DataTable objDataTableRoles = new DataTable();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objApprove_Flow_Doc_InfoValue.m_strApprove_Flow_ID;
                objDPArr[1].Value = p_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCurRole, ref objDataTableRoles,objDPArr);
                if (lngRes <= 0 || objDataTableRoles.Rows.Count <= 0)				//没有权限操作
                {
                    return false;
                }
                else
                {
                    if (int.Parse(objDataTableRoles.Rows[0]["SEQUENCE_NO"].ToString()) < intSequence)
                        return false;
                }
                return true;
                #endregion

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
			return true;
			
		}

		/// <summary>
		/// 得到员工的角色信息
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <returns></returns>
		[AutoComplete]
		public string[] strGetEmployeeRole(string p_strEmployeeID)
		{
			string[] strEmployeeRoleArry=null;
			string SQL="select role_name from role_employee,role_info where employeeid=? and status=0 and role_employee.role_id=role_info.role_id";
			DataTable dtRecord=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strEmployeeID.Trim();

                objHRPServ.lngGetDataTableWithParameters(SQL, ref dtRecord,objDPArr);

                if (dtRecord != null || dtRecord.Rows.Count > 0)
                {
                    strEmployeeRoleArry = new string[dtRecord.Rows.Count];
                    for (int i = 0; i < dtRecord.Rows.Count; i++)
                    {
                        strEmployeeRoleArry[i] = dtRecord.Rows[i][0].ToString();
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }

			return strEmployeeRoleArry;

		}

		/// <summary>
		/// 是否审核记录
		/// </summary>
		/// <param name="p_strFormName"></param>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strCurrUserLevel"></param>
		/// <param name="p_strCreateUserLevel"></param>
		/// <param name="p_intCurrAppNo"></param>
		/// <param name="p_blnCZType"></param>
		/// <returns></returns>
        [AutoComplete]
		public  long lngCanAuditing(string p_strFormName,string p_strPatientID,string p_strInPatientDate,string p_strOpenDate,string p_strCurrUserLevel,string p_strCreateUserLevel,ref int p_intCurrAppNo,bool p_blnCZType)
		{
			long lngRes=0;
			
				System.Data.DataTable dtRecord=null;
				if(p_strFormName.Trim().Length==0)
					return -1;

				if(p_strPatientID.Trim().Length==0)
					return -1;
			
				if(p_blnCZType==true)
				{
					if(int.Parse(p_strCurrUserLevel)<=int.Parse(p_strCreateUserLevel))
					{
						return -1;
					}
				}			
				string Sql="";
				if(clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
				{
					Sql=@"select audi_level
						from auditing_info
						where audi_inpatientid = ?
						and audi_inpatientdate = ?
						and audi_opendate = ?
						and audi_formid = ?
						and rownum < 2
						order by audi_no desc";
				}
				else if(clsHRPTableService.bytDatabase_Selector==0)
				{
					Sql=@"select top 1 audi_level
						from auditing_info
						where audi_inpatientid = ?
						and audi_inpatientdate = ?
						and audi_opendate = ?
						and audi_formid = ?
						order by audi_no desc";
				}
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    Sql = @"select   audi_level
						from auditing_info
						where audi_inpatientid = ?
						and audi_inpatientdate = ?
						and audi_opendate = ?
						and audi_formid = ?
						order by audi_no desc fetch first 1 row only";
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                try
                {
                    System.Data.IDataParameter[] objDPArrSelect = null;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArrSelect);
                    objDPArrSelect[0].Value = p_strPatientID;
                    objDPArrSelect[1].DbType = DbType.DateTime;
                    objDPArrSelect[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArrSelect[2].DbType = DbType.DateTime;
                    objDPArrSelect[2].Value = DateTime.Parse(p_strOpenDate);
                    objDPArrSelect[3].Value = p_strFormName;
                    lngRes = objHRPServ.lngGetDataTableWithParameters(Sql, ref dtRecord, objDPArrSelect);
                    if (dtRecord != null)
                    {
                        if (dtRecord.Rows.Count > 0)
                        {
                            p_intCurrAppNo = int.Parse(dtRecord.Rows[0][0].ToString());
                            if (int.Parse(p_strCurrUserLevel) < int.Parse(dtRecord.Rows[0][0].ToString()))
                                return -1;
                            else if (int.Parse(p_strCurrUserLevel) == int.Parse(dtRecord.Rows[0][0].ToString()))
                                return 0;
                            else
                                return 1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                        p_intCurrAppNo = -1;
                    return -1;
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
		/// 审核记录
		/// </summary>
		/// <param name="p_strFormName"></param>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strCurrUserLevel"></param>
		/// <returns></returns>
        [AutoComplete]
		public long lngExecAuditing( string p_strOperatorID, string p_strFormName,string p_strPatientID,string p_strInPatientDate,string p_strOpenDate,string p_strCurrUserLevel)
		{
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string Sql = "";
                long lngAffecdet = 0;
                System.Data.IDataParameter[] objDPArrInsert = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArrInsert);
                objDPArrInsert[0].Value = p_strPatientID;
                objDPArrInsert[1].DbType = DbType.DateTime;
                objDPArrInsert[1].Value = Convert.ToDateTime(p_strInPatientDate);
                objDPArrInsert[2].DbType = DbType.DateTime;
                objDPArrInsert[2].Value = Convert.ToDateTime(p_strOpenDate);
                objDPArrInsert[3].Value = p_strFormName;
                objDPArrInsert[4].Value = p_strCurrUserLevel;
                objDPArrInsert[5].Value = p_strOperatorID;
                //适应oracle sqlserver版本更改 tfzhang 2005-6-23 17:57:21
                Sql = @"insert into auditing_info (audi_no,audi_inpatientid,audi_inpatientdate,audi_opendate,audi_formid,audi_level,audi_rid)
                      values ((select nvl(max(audi_no),0)+1 from auditing_info),?,?,?,?,?,?)";
                lngRes = objHRPServ.lngExecuteParameterSQL(Sql, ref lngAffecdet, objDPArrInsert);

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
		/// 退审记录
		/// </summary>
		/// <param name="p_strFormName"></param>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_intCurrAppNo"></param>
		/// <returns></returns>
        [AutoComplete]
		public long lngBackAuditing(string p_strOperatorID,string p_strFormName,string p_strPatientID,string p_strInPatientDate,string p_strOpenDate,int p_intCurrAppNo)
		{
			
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                long lngAffecdet = 0;
                string Sql = "";
                if (p_intCurrAppNo == -1 || p_intCurrAppNo == 0)
                    return -99;

                System.Data.IDataParameter[] objDPArrInsert = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArrInsert);
                objDPArrInsert[0].Value = p_strPatientID;
                objDPArrInsert[1].DbType = DbType.DateTime;
                objDPArrInsert[1].Value = Convert.ToDateTime(p_strInPatientDate);
                objDPArrInsert[1].DbType = DbType.DateTime;
                objDPArrInsert[2].Value = Convert.ToDateTime(p_strOpenDate);
                objDPArrInsert[3].Value = p_strFormName;
                objDPArrInsert[4].Value = p_intCurrAppNo - 1;
                //objDPArrInsert[4].Value="";
                objDPArrInsert[5].Value = p_strOperatorID;
                Sql = @"insert into auditing_info (audi_no,audi_inpatientid,audi_inpatientdate,audi_opendate,audi_formid,audi_level,audi_rid) 
                      values ((select nvl(max(audi_no),0)+1 from auditing_info),?,?,?,?,?,?)";
                lngRes = objHRPServ.lngExecuteParameterSQL(Sql, ref lngAffecdet, objDPArrInsert);

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
		/// 获取员工级别
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <param name="p_strLevel"></param>
		/// <param name="p_enmFormType"></param>
		/// <returns></returns>
        [AutoComplete]
		public long lngGetUserLevel(string p_strUserId,ref string p_strLevel,int p_intFormType)
		{
			
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                System.Data.DataTable dtRecord = null;
                string Sql = "";
                System.Data.IDataParameter[] objDPArrInsert = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArrInsert);
                objDPArrInsert[0].Value = p_strUserId.Trim();
                objDPArrInsert[1].Value = p_intFormType;

                Sql = @"select audl_level
							from auditing_level
							where audl_roletypeid in (select roletypeid
														from role_employee, role_type, role_info
														where employeeid = ?
														and role_name = roletypename
														and role_employee.role_id = role_info.role_id
														and status = 0) and audl_type = ? ";

                lngRes = objHRPServ.lngGetDataTableWithParameters(Sql, ref dtRecord, objDPArrInsert);

                if (dtRecord != null && dtRecord.Rows.Count > 0)
                {
                    p_strLevel = dtRecord.Rows[0][0].ToString();
                }
                else
                    p_strLevel = "0";
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