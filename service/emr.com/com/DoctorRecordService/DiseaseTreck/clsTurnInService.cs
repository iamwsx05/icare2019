using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.DiseaseTrackService
{
	// ʵ�������¼���м����
	//���̼�¼����ת���¼
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsTurnInService: clsDiseaseTrackService
	{

		#region  SQL���
		// ��TurnInRecord��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		private const string c_strGetTimeListSQL="select CreateDate,OpenDate from TurnInRecord Where InPatientID = ? and InPatientDate= ? and Status=0";


		// ����ָ��������Ϣ����TurnInRecord��TurnInRecordContent���ұ������ݡ�
		// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������

		private const string c_strGetRecordContentSQL=@"select a.*,b.* from TurnInRecord a,TurnInRecordContent b  where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0  and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from TurnInRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		// ��TurnInRecord�л�ȡָ��ʱ��ı���
		// InPatientID ,InPatientDate ,CreateDate,Status = 0
		private const string c_strCheckCreateDateSQL="select CreateUserID,OpenDate from TurnInRecord Where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		//
		//		// ��TurnInRecord��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		//		private const string c_strGetExistInfoSQL="";

		// ��TurnInRecordContent��ȡָ����������޸�ʱ�䡣
		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate,b.ModifyUserID from TurnInRecord a,TurnInRecordContent b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from TurnInRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		

		//		// ��TurnInRecordContent��ȡ�޸ı�����Ҫ��Ϣ��
		//		private const string c_strGetModifyRecordSQL="";

		// ��TurnInRecord��ȡɾ��������Ҫ��Ϣ��
		private const string c_strGetDeleteRecordSQL="select DeActivedDate,DeActivedOperatorID from TurnInRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";


		// ��Ӽ�¼��TurnInRecord
		private const string c_strAddNewRecordSQL= @"insert into  TurnInRecord(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,IfConfirm,ConfirmReason,ConfirmReasonXML,Status,CaseBeforeTurnIn,CaseBeforeTurnInXML,TurnInReason,TurnInReasonXML,CaseAfterTurnIn,CaseAfterTurnInXML,TurnInDiagnose,TurnInDiagnoseXML,Referral,ReferralXML,SEQUENCE_INT,MARKSTATUS) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// ��Ӽ�¼��TurnInRecordContent
		private const string c_strAddNewRecordContentSQL=@"insert into  TurnInRecordContent(InPatientID,InPatientDate,OpenDate,ModifyDate,ModifyUserID,CaseBeforeTurnIn_Right,TurnInReason_Right,CaseAfterTurnIn_Right,TurnInDiagnose_Right,Referral_Right) 
				values(?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��TurnInRecord
		/// </summary>
		private const string c_strModifyRecordSQL= "Update TurnInRecord Set CaseBeforeTurnIn=?,CaseBeforeTurnInXML=? ,TurnInReason=?,TurnInReasonXML=? ,CaseAfterTurnIn=?,CaseAfterTurnInXML=? ,TurnInDiagnose=?,TurnInDiagnoseXML=?,Referral=? ,ReferralXML=? ,SEQUENCE_INT=? ,MARKSTATUS=?  Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

		// �޸ļ�¼��TurnInRecordContent
		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;

		// ����TurnInRecord��ɾ����¼����Ϣ
		private const string c_strDeleteRecordSQL="Update TurnInRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";


		// ��TurnInRecord��TurnInRecordContent��ȡLastModifyDate��FirstPrintDate
		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"select a.FirstPrintDate,b.ModifyDate from TurnInRecord a,TurnInRecordContent b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from TurnInRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";


		// ����TurnInRecord��FirstPrintDate
		private const string c_strUpdateFirstPrintDateSQL="Update  TurnInRecord Set FirstPrintDate= ? Where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";


		// ��TurnInRecord��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListSQL="select CreateDate,OpenDate from TurnInRecord Where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";


		// ��TurnInRecord��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListAllSQL="select CreateDate,OpenDate from TurnInRecord Where InPatientID = ? and InPatientDate= ? and Status=1";


		// �ڽ����¼���б��л�ȡָ��������Ϣ��
		private const string c_strGetDeleteRecordContentSQL=@"select a.*,b.* from TurnInRecord a,TurnInRecordContent b  where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=1  and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from TurnInRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		#endregion

		#region ��ȡ���˵ĸü�¼ʱ���б�
		/// <summary>
		/// ��ȡ���˵ĸü�¼ʱ���б�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
		/// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTurnInService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;



                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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
			//����
			return lngRes;


		}
		#endregion

		#region �������ݿ��е��״δ�ӡʱ��
		/// <summary>
		/// �������ݿ��е��״δ�ӡʱ�䡣
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTurnInService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //ִ��SQL
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
			//����
			return lngRes;

						
		}
		#endregion

		#region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
		/// <summary>
		/// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strDeleteUserID">ɾ����ID</param>
		/// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
		/// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
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

                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTurnInService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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

            }			//����
			return lngRes;


		}
		#endregion

		#region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
		/// <summary>
		/// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
		/// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTurnInService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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

            }			//����
			return lngRes;


	
		}
		#endregion

		#region ��ȡָ����¼������
		/// <summary>
		/// ��ȡָ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">��¼����</param>
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

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    clsTurnInRecordContent objRecordContent = new clsTurnInRecordContent();
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

                    objRecordContent.m_strCaseBeforeTurnIn_Right = dtbValue.Rows[0]["CASEBEFORETURNIN_RIGHT"].ToString();
                    objRecordContent.m_strCaseBeforeTurnIn = dtbValue.Rows[0]["CASEBEFORETURNIN"].ToString();
                    objRecordContent.m_strCaseBeforeTurnInXML = dtbValue.Rows[0]["CASEBEFORETURNINXML"].ToString();
                    objRecordContent.m_strTurnInReason_Right = dtbValue.Rows[0]["TURNINREASON_RIGHT"].ToString();
                    objRecordContent.m_strTurnInReason = dtbValue.Rows[0]["TURNINREASON"].ToString();
                    objRecordContent.m_strTurnInReasonXML = dtbValue.Rows[0]["TURNINREASONXML"].ToString();
                    objRecordContent.m_strCaseAfterTurnIn_Right = dtbValue.Rows[0]["CASEAFTERTURNIN_RIGHT"].ToString();
                    objRecordContent.m_strCaseAfterTurnIn = dtbValue.Rows[0]["CASEAFTERTURNIN"].ToString();
                    objRecordContent.m_strCaseAfterTurnInXML = dtbValue.Rows[0]["CASEAFTERTURNINXML"].ToString();
                    objRecordContent.m_strTurnInDiagnose_Right = dtbValue.Rows[0]["TURNINDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strTurnInDiagnose = dtbValue.Rows[0]["TURNINDIAGNOSE"].ToString();
                    objRecordContent.m_strTurnInDiagnoseXML = dtbValue.Rows[0]["TURNINDIAGNOSEXML"].ToString();
                    objRecordContent.m_strReferral_Right = dtbValue.Rows[0]["REFERRAL_RIGHT"].ToString();
                    objRecordContent.m_strReferral = dtbValue.Rows[0]["REFERRAL"].ToString();
                    objRecordContent.m_strReferralXML = dtbValue.Rows[0]["REFERRALXML"].ToString();
                    objRecordContent.m_intMarkStatus = int.Parse(dtbValue.Rows[0]["MARKSTATUS"].ToString());

                    //��ȡǩ������
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }
                    p_objRecordContent = objRecordContent;
                }
                //����
                return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//����
			return lngRes;

		
		}
		#endregion

		#region �鿴�Ƿ�����ͬ�ļ�¼ʱ��
		/// <summary>
		/// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objPreModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
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
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;



                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //�鿴DataTable.Rows.Count
                //�������1����ʾ�Ѿ��и�CreateDate�����Ҳ���ɾ���ļ�¼��
                //��ȡ�ü�¼����Ϣ����ֵ��p_objModifyInfo�С�����ֵʹ��Record_Already_Exist
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

            }			//����
			return lngRes;


		}
		#endregion

		#region �����¼�����ݿ⡣�������,����ӱ�
		/// <summary>
		/// �����¼�����ݿ⡣�������,����ӱ�.
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
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
                //������                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsTurnInRecordContent objContent = (clsTurnInRecordContent)p_objRecordContent;


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
                objDPArr[9].Value = objContent.m_strCaseBeforeTurnIn;
                objDPArr[10].Value = objContent.m_strCaseBeforeTurnInXML;
                objDPArr[11].Value = objContent.m_strTurnInReason;
                objDPArr[12].Value = objContent.m_strTurnInReasonXML;
                objDPArr[13].Value = objContent.m_strCaseAfterTurnIn;
                objDPArr[14].Value = objContent.m_strCaseAfterTurnInXML;
                objDPArr[15].Value = objContent.m_strTurnInDiagnose;
                objDPArr[16].Value = objContent.m_strTurnInDiagnoseXML;
                objDPArr[17].Value = objContent.m_strReferral;
                objDPArr[18].Value = objContent.m_strReferralXML;
                objDPArr[19].Value = lngSequence;
                objDPArr[20].Value = objContent.m_intMarkStatus;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(10, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strCaseBeforeTurnIn_Right;
                objDPArr2[6].Value = objContent.m_strTurnInReason_Right;
                objDPArr2[7].Value = objContent.m_strCaseAfterTurnIn_Right;
                objDPArr2[8].Value = objContent.m_strTurnInDiagnose_Right;
                objDPArr2[9].Value = objContent.m_strReferral_Right;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                //�ͷ�
                objSign = null;

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

            }			//����
			return lngRes;

		}
		#endregion

		#region �鿴��ǰ��¼�Ƿ����µļ�¼
		/// <summary>
		/// �鿴��ǰ��¼�Ƿ����µļ�¼��
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>		
		/// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
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
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from TurnInRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��p_objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
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

            }			//����
			return lngRes;

		}
		#endregion

		#region �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�
		/// <summary>
		/// �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�.
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
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
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsTurnInRecordContent objContent = (clsTurnInRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                objDPArr[0].Value = objContent.m_strCaseBeforeTurnIn;
                objDPArr[1].Value = objContent.m_strCaseBeforeTurnInXML;
                objDPArr[2].Value = objContent.m_strTurnInReason;
                objDPArr[3].Value = objContent.m_strTurnInReasonXML;
                objDPArr[4].Value = objContent.m_strCaseAfterTurnIn;
                objDPArr[5].Value = objContent.m_strCaseAfterTurnInXML;
                objDPArr[6].Value = objContent.m_strTurnInDiagnose;
                objDPArr[7].Value = objContent.m_strTurnInDiagnoseXML;
                objDPArr[8].Value = objContent.m_strReferral;
                objDPArr[9].Value = objContent.m_strReferralXML;
                objDPArr[10].Value = lngSequence;
                objDPArr[11].Value = objContent.m_intMarkStatus;

                objDPArr[12].Value = objContent.m_strInPatientID;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = objContent.m_dtmInPatientDate;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = objContent.m_dtmOpenDate;


                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(10, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strCaseBeforeTurnIn_Right;
                objDPArr2[6].Value = objContent.m_strTurnInReason_Right;
                objDPArr2[7].Value = objContent.m_strCaseAfterTurnIn_Right;
                objDPArr2[8].Value = objContent.m_strTurnInDiagnose_Right;
                objDPArr2[9].Value = objContent.m_strReferral_Right;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                //�ͷ�
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

            }			//����
			return lngRes;
		

		}
		#endregion

		#region �Ѽ�¼�������С�ɾ����
		/// <summary>
		/// �Ѽ�¼�������С�ɾ������
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
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
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;

                //ִ��SQL
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

            }			//����
			return lngRes;


		}
		#endregion

		#region ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
		/// <summary>
		/// ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_dtmModifyDate">�޸�ʱ��</param>
		/// <param name="p_strFirstPrintDate">�״δ�ӡʱ��</param>
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
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
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

            }			//����
			return lngRes;

		}
		#endregion

		#region ��ȡָ���Ѿ���ɾ����¼������
		/// <summary>
		/// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">��¼����</param>
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
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    clsTurnInRecordContent objRecordContent = new clsTurnInRecordContent();
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

                    objRecordContent.m_strCaseBeforeTurnIn_Right = dtbValue.Rows[0]["CASEBEFORETURNIN_RIGHT"].ToString();
                    objRecordContent.m_strCaseBeforeTurnIn = dtbValue.Rows[0]["CASEBEFORETURNIN"].ToString();
                    objRecordContent.m_strCaseBeforeTurnInXML = dtbValue.Rows[0]["CASEBEFORETURNINXML"].ToString();
                    objRecordContent.m_strTurnInReason_Right = dtbValue.Rows[0]["TURNINREASON_RIGHT"].ToString();
                    objRecordContent.m_strTurnInReason = dtbValue.Rows[0]["TURNINREASON"].ToString();
                    objRecordContent.m_strTurnInReasonXML = dtbValue.Rows[0]["TURNINREASONXML"].ToString();
                    objRecordContent.m_strCaseAfterTurnIn_Right = dtbValue.Rows[0]["CASEAFTERTURNIN_RIGHT"].ToString();
                    objRecordContent.m_strCaseAfterTurnIn = dtbValue.Rows[0]["CASEAFTERTURNIN"].ToString();
                    objRecordContent.m_strCaseAfterTurnInXML = dtbValue.Rows[0]["CASEAFTERTURNINXML"].ToString();
                    objRecordContent.m_strTurnInDiagnose_Right = dtbValue.Rows[0]["TURNINDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strTurnInDiagnose = dtbValue.Rows[0]["TURNINDIAGNOSE"].ToString();
                    objRecordContent.m_strTurnInDiagnoseXML = dtbValue.Rows[0]["TURNINDIAGNOSEXML"].ToString();
                    objRecordContent.m_strReferral_Right = dtbValue.Rows[0]["REFERRAL_RIGHT"].ToString();
                    objRecordContent.m_strReferral = dtbValue.Rows[0]["REFERRAL"].ToString();
                    objRecordContent.m_strReferralXML = dtbValue.Rows[0]["REFERRALXML"].ToString();
                    //��ȡǩ������
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
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

            }			//����
			return lngRes;

		}
		#endregion

	}// END CLASS DEFINITION clsHandOverService

}
