using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{
	/// <summary>
	/// �״β��̼�¼(��һ��ҽ��)
	/// </summary>
	// ʵ�������¼���м����
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsFirstIllnessNote_ZYService : clsDiseaseTrackService
	{
		// ��FIRSTILLNESSNOTERECORD_GZZY��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		private const string c_strGetTimeListSQL="select CreateDate,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and Status=0";


		// ����ָ��������Ϣ����FIRSTILLNESSNOTERECORD_GZZY��FIRSTILLNESSNOTERECORDCON_GZZY���ұ������ݡ�
		// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������

		private const string c_strGetRecordContentSQL=@"select a.*,b.* from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		// ��FIRSTILLNESSNOTERECORD_GZZY�л�ȡָ��ʱ��ı���
		// InPatientID ,InPatientDate ,CreateDate,Status = 0
		private const string c_strCheckCreateDateSQL="select CreateUserID,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		// ��FIRSTILLNESSNOTERECORDCON_GZZY��ȡָ����������޸�ʱ�䡣
		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate,b.ModifyUserID from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		// ��FIRSTILLNESSNOTERECORD_GZZY��ȡɾ��������Ҫ��Ϣ��
		private const string c_strGetDeleteRecordSQL="select DeActivedDate,DeActivedOperatorID from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";


		// ��Ӽ�¼��FIRSTILLNESSNOTERECORD_GZZY
		private const string c_strAddNewRecordSQL= @"insert into  FIRSTILLNESSNOTERECORD_GZZY(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,
		IfConfirm,ConfirmReason,ConfirmReasonXML,Status,MostlyContent,MostlyContentXML,OriginalDiagnose,OriginalDiagnoseXML,ThereunderDiagnose,ThereunderDiagnoseXML,DiagnoseDiffe,DiagnoseDiffeXML,CurePlan,CurePlanXML,IDENTIFYRESTON,IDENTIFYRESTONXML,IDENTIFYDIAGNOSE,IDENTIFYDIAGNOSEXML) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// ��Ӽ�¼��FIRSTILLNESSNOTERECORDCON_GZZY
		private const string c_strAddNewRecordContentSQL=@"insert into  FIRSTILLNESSNOTERECORDCON_GZZY(InPatientID,InPatientDate,OpenDate,ModifyDate,ModifyUserID,MostlyContent_Right,OriginalDiagnose_Right,ThereunderDiagnose_Right,DiagnoseDiffe_Right,CurePlan_Right,IDENTIFYRESTON_RIGHT,IDENTIFYDIAGNOSE_RIGHT) 
				values(?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��FIRSTILLNESSNOTERECORD_GZZY
		/// </summary>
		private const string c_strModifyRecordSQL= "Update FIRSTILLNESSNOTERECORD_GZZY Set MostlyContent=?,MostlyContentXML=? ,OriginalDiagnose=?,OriginalDiagnoseXML=? ,ThereunderDiagnose=?,ThereunderDiagnoseXML=? ,DiagnoseDiffe=?,DiagnoseDiffeXML=?,CurePlan=?,CurePlanXML=?,IDENTIFYRESTON=?,IDENTIFYRESTONXML=?,IDENTIFYDIAGNOSE=?,IDENTIFYDIAGNOSEXML=? where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

		// �޸ļ�¼��FIRSTILLNESSNOTERECORDCON_GZZY
		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;

		// ����FIRSTILLNESSNOTERECORD_GZZY��ɾ����¼����Ϣ
		private const string c_strDeleteRecordSQL="Update FIRSTILLNESSNOTERECORD_GZZY Set Status=1,DeActivedDate=?,DeActivedOperatorID=? where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";


		// ��FIRSTILLNESSNOTERECORD_GZZY��FIRSTILLNESSNOTERECORDCON_GZZY��ȡLastModifyDate��FirstPrintDate
		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"select a.FirstPrintDate,b.ModifyDate from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";


		// ����FIRSTILLNESSNOTERECORD_GZZY��FirstPrintDate
		private const string c_strUpdateFirstPrintDateSQL="Update  FIRSTILLNESSNOTERECORD_GZZY Set FirstPrintDate= ? where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";


		// ��FIRSTILLNESSNOTERECORD_GZZY��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListSQL="select CreateDate,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";


		// ��FIRSTILLNESSNOTERECORD_GZZY��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListAllSQL="select CreateDate,OpenDate from FIRSTILLNESSNOTERECORD_GZZY where InPatientID = ? and InPatientDate= ? and Status=1";


		// �ڽ����¼���б��л�ȡָ��������Ϣ��
		private const string c_strGetDeleteRecordContentSQL=@"select a.*,b.* from FIRSTILLNESSNOTERECORD_GZZY a,FIRSTILLNESSNOTERECORDCON_GZZY b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=1 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from FIRSTILLNESSNOTERECORDCON_GZZY where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		

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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngGetRecordTimeList");
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
			//����
			return lngRes;


		}

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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngUpdateFirstPrintDate");
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngGetDeleteRecordTimeList");
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
			//����
			return lngRes;

		}

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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsFirstIllnessNote_ZYService", "m_lngGetDeleteRecordTimeListAll");
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

            }			//����
			return lngRes;


		}

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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
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

            }			//����
			return lngRes;

			

		}

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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //�鿴DataTable.Rows.Count
                //�������1����ʾ�Ѿ��и�CreateDate�����Ҳ���ɾ���ļ�¼��
                //��ȡ�ü�¼����Ϣ����ֵ��p_objModifyInfo�С�����ֵʹ��Record_Already_Exist
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

            }			//����
			return lngRes;


		}

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


                //ִ��SQL
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


                //ִ��SQL			
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

            }			//����
			return lngRes;

		}

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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
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
                //��DataTable�л�ȡModifyDate��ʹ֮��p_objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["ModifyDate"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
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

            }			//����
			return lngRes;

		}

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


                //ִ��SQL
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


                //ִ��SQL			
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

            }			//����
			return lngRes;


		}

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
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//����
			return lngRes;


		}

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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
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
            //����
			return lngRes;

		}

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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
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

            }			//����
			return lngRes;

			

		}
		
	}// END CLASS DEFINITION clsDiseaseSummaryService
}
