using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{
	/// <summary>
	///ʵ�������¼���м����
	///�����󲡳̼�¼�м��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsAfterOperationService : clsDiseaseTrackService
	{
		#region SQL ���
		// ��AfterOperationRecord��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		private const string c_strGetTimeListSQL="select createdate,opendate from afteroperationrecord where inpatientid = ? and inpatientdate= ? and status=0";

		// ����ָ��������Ϣ����AfterOperationRecord��AfterOperationRecordContent���ұ������ݡ�
		// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.anaesthesiamode,
       a.anaesthesiamodexml,
       a.operationname,
       a.operationnamexml,
       a.operationdiagnose,
       a.operationdiagnosexml,
       a.inoperationseeing,
       a.inoperationseeingxml,
       a.afteroperationdeal,
       a.afteroperationdealxml,
       a.afteroperationnotice,
       a.afteroperationnoticexml,
       a.cuthealupstatus,
       a.cuthealupstatusxml,
       a.takeoutstitchesdate,
       a.sequence_int,
       a.markstatus,
       b.modifydate,
       b.modifyuserid,
       b.anaesthesiamode_right,
       b.operationname_right,
       b.operationdiagnose_right,
       b.inoperationseeing_right,
       b.afteroperationdeal_right,
       b.afteroperationnotice_right,
       b.cuthealupstatus_right
  from afteroperationrecord a, afteroperationrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from afteroperationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		
		// ��AfterOperationRecord�л�ȡָ��ʱ��ı���
		// InPatientID ,InPatientDate ,CreateDate,Status = 0
		private const string c_strCheckCreateDateSQL="select createuserid,opendate from afteroperationrecord where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		//
		//		// ��AfterOperationRecord��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		//		private const string c_strGetExistInfoSQL="";

		// ��AfterOperationRecordContent��ȡָ����������޸�ʱ�䡣
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate,b.modifyuserid from afteroperationrecord a,afteroperationrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from afteroperationrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
		

		//		// ��AfterOperationRecordContent��ȡ�޸ı�����Ҫ��Ϣ��
		//		private const string c_strGetModifyRecordSQL="";

		// ��AfterOperationRecord��ȡɾ��������Ҫ��Ϣ��
		private const string c_strGetDeleteRecordSQL="select deactiveddate,deactivedoperatorid from afteroperationrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";


		// ��Ӽ�¼��AfterOperationRecord
		private const string c_strAddNewRecordSQL= @"insert into  afteroperationrecord(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,anaesthesiamode,anaesthesiamodexml,operationname,operationnamexml,operationdiagnose,operationdiagnosexml,inoperationseeing,inoperationseeingxml,afteroperationdeal,afteroperationdealxml,afteroperationnotice,afteroperationnoticexml,cuthealupstatus,cuthealupstatusxml,takeoutstitchesdate,sequence_int) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// ��Ӽ�¼��AfterOperationRecordContent
		private const string c_strAddNewRecordContentSQL=@"insert into  afteroperationrecordcontent(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,anaesthesiamode_right,operationname_right,operationdiagnose_right,inoperationseeing_right,afteroperationdeal_right,afteroperationnotice_right,cuthealupstatus_right) 
				values(?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��AfterOperationRecord
		/// </summary>
		private const string c_strModifyRecordSQL= "update afteroperationrecord set anaesthesiamode=?,anaesthesiamodexml=? ,operationname=?,operationnamexml=? ,operationdiagnose=?,operationdiagnosexml=? ,inoperationseeing=?,inoperationseeingxml=?,afteroperationdeal=? ,afteroperationdealxml=? ,afteroperationnotice=? ,afteroperationnoticexml=? ,cuthealupstatus=?,cuthealupstatusxml=?,takeoutstitchesdate=? ,sequence_int=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

		// �޸ļ�¼��AfterOperationRecordContent
		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;

		// ����AfterOperationRecord��ɾ����¼����Ϣ
		private const string c_strDeleteRecordSQL="update afteroperationrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";


		// ��AfterOperationRecord��AfterOperationRecordContent��ȡLastModifyDate��FirstPrintDate
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate,b.modifydate from afteroperationrecord a,afteroperationrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from afteroperationrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";


		// ����AfterOperationRecord��FirstPrintDate
		private const string c_strUpdateFirstPrintDateSQL="update  afteroperationrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";


		// ��AfterOperationRecord��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListSQL="select createdate,opendate from afteroperationrecord where inpatientid = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";


		// ��AfterOperationRecord��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListAllSQL="select createdate,opendate from afteroperationrecord where inpatientid = ? and inpatientdate= ? and status=1";


		// �ڽ����¼���б��л�ȡָ��������Ϣ��
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.anaesthesiamode,
       a.anaesthesiamodexml,
       a.operationname,
       a.operationnamexml,
       a.operationdiagnose,
       a.operationdiagnosexml,
       a.inoperationseeing,
       a.inoperationseeingxml,
       a.afteroperationdeal,
       a.afteroperationdealxml,
       a.afteroperationnotice,
       a.afteroperationnoticexml,
       a.cuthealupstatus,
       a.cuthealupstatusxml,
       a.takeoutstitchesdate,
       a.sequence_int,
       a.markstatus,
       b.modifydate,
       b.modifyuserid,
       b.anaesthesiamode_right,
       b.operationname_right,
       b.operationdiagnose_right,
       b.inoperationseeing_right,
       b.afteroperationdeal_right,
       b.afteroperationnotice_right,
       b.cuthealupstatus_right
  from afteroperationrecord a, afteroperationrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from afteroperationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		
		#endregion

		#region ����
		/// <summary>
		/// ��ȡ���˵ĸü�¼ʱ���б�
		/// </summary>
			/// <param name="p_objPrincipal"></param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsAfterOperationService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
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
		/// �������ݿ��е��״δ�ӡʱ�䡣
		/// </summary>
			/// <param name="p_objPrincipal"></param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsAfterOperationService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
			/// <param name="p_objPrincipal"></param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsAfterOperationService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
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
		/// <param name="p_objPrincipal"></param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsAfterOperationService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
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

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
                    clsAfterOperationRecordContent objRecordContent = new clsAfterOperationRecordContent();
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

                    objRecordContent.m_strAnaesthesiaMode_Right = dtbValue.Rows[0]["ANAESTHESIAMODE_RIGHT"].ToString();
                    objRecordContent.m_strAnaesthesiaMode = dtbValue.Rows[0]["ANAESTHESIAMODE"].ToString();
                    objRecordContent.m_strAnaesthesiaModeXML = dtbValue.Rows[0]["ANAESTHESIAMODEXML"].ToString();
                    objRecordContent.m_strOperationName_Right = dtbValue.Rows[0]["OPERATIONNAME_RIGHT"].ToString();
                    objRecordContent.m_strOperationName = dtbValue.Rows[0]["OPERATIONNAME"].ToString();
                    objRecordContent.m_strOperationNameXML = dtbValue.Rows[0]["OPERATIONNAMEXML"].ToString();
                    objRecordContent.m_strOperationDiagnose_Right = dtbValue.Rows[0]["OPERATIONDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strOperationDiagnose = dtbValue.Rows[0]["OPERATIONDIAGNOSE"].ToString();
                    objRecordContent.m_strOperationDiagnoseXML = dtbValue.Rows[0]["OPERATIONDIAGNOSEXML"].ToString();
                    objRecordContent.m_strInOperationSeeing_Right = dtbValue.Rows[0]["INOPERATIONSEEING_RIGHT"].ToString();
                    objRecordContent.m_strInOperationSeeing = dtbValue.Rows[0]["INOPERATIONSEEING"].ToString();
                    objRecordContent.m_strInOperationSeeingXML = dtbValue.Rows[0]["INOPERATIONSEEINGXML"].ToString();
                    objRecordContent.m_strAfterOperationDeal_Right = dtbValue.Rows[0]["AFTEROPERATIONDEAL_RIGHT"].ToString();
                    objRecordContent.m_strAfterOperationDeal = dtbValue.Rows[0]["AFTEROPERATIONDEAL"].ToString();
                    objRecordContent.m_strAfterOperationDealXML = dtbValue.Rows[0]["AFTEROPERATIONDEALXML"].ToString();

                    objRecordContent.m_strAfterOperationNotice_Right = dtbValue.Rows[0]["AFTEROPERATIONNOTICE_RIGHT"].ToString();
                    objRecordContent.m_strAfterOperationNotice = dtbValue.Rows[0]["AFTEROPERATIONNOTICE"].ToString();
                    objRecordContent.m_strAfterOperationNoticeXML = dtbValue.Rows[0]["AFTEROPERATIONNOTICEXML"].ToString();
                    objRecordContent.m_strCutHealUpStatus_Right = dtbValue.Rows[0]["CUTHEALUPSTATUS_RIGHT"].ToString();
                    objRecordContent.m_strCutHealUpStatus = dtbValue.Rows[0]["CUTHEALUPSTATUS"].ToString();
                    objRecordContent.m_strCutHealUpStatusXML = dtbValue.Rows[0]["CUTHEALUPSTATUSXML"].ToString();
                    if (dtbValue.Rows[0]["TAKEOUTSTITCHESDATE"].ToString() == "")
                        objRecordContent.m_dtmTakeOutStitchesDate = DateTime.MinValue;
                    else objRecordContent.m_dtmTakeOutStitchesDate = DateTime.Parse(dtbValue.Rows[0]["TAKEOUTSTITCHESDATE"].ToString());
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

            }
		//����
			return lngRes;
		}
		/// <summary>
		/// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out  clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
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
                if (p_objRecordContent == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                if (p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsAfterOperationRecordContent objContent = (clsAfterOperationRecordContent)p_objRecordContent;

                //��ȡIDataParameter����
                 IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[24];
                objHRPServ.CreateDatabaseParameter(25, out objDPArr);
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
                objDPArr[9].Value = objContent.m_strAnaesthesiaMode;
                objDPArr[10].Value = objContent.m_strAnaesthesiaModeXML;
                objDPArr[11].Value = objContent.m_strOperationName;
                objDPArr[12].Value = objContent.m_strOperationNameXML;
                objDPArr[13].Value = objContent.m_strOperationDiagnose;
                objDPArr[14].Value = objContent.m_strOperationDiagnoseXML;
                objDPArr[15].Value = objContent.m_strInOperationSeeing;
                objDPArr[16].Value = objContent.m_strInOperationSeeingXML;
                objDPArr[17].Value = objContent.m_strAfterOperationDeal;
                objDPArr[18].Value = objContent.m_strAfterOperationDealXML;
                objDPArr[19].Value = objContent.m_strAfterOperationNotice;
                objDPArr[20].Value = objContent.m_strAfterOperationNoticeXML;
                objDPArr[21].Value = objContent.m_strCutHealUpStatus;
                objDPArr[22].Value = objContent.m_strCutHealUpStatusXML;
                objDPArr[23].Value = objContent.m_dtmTakeOutStitchesDate;
                objDPArr[24].Value = lngSequence;


                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);



                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[12];
                objHRPServ.CreateDatabaseParameter(12, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strAnaesthesiaMode_Right;
                objDPArr2[6].Value = objContent.m_strOperationName_Right;
                objDPArr2[7].Value = objContent.m_strOperationDiagnose_Right;
                objDPArr2[8].Value = objContent.m_strInOperationSeeing_Right;
                objDPArr2[9].Value = objContent.m_strAfterOperationDeal_Right;
                objDPArr2[10].Value = objContent.m_strAfterOperationNotice_Right;
                objDPArr2[11].Value = objContent.m_strCutHealUpStatus_Right;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
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

            }
			//����
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
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
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
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from AfterOperationRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

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

                clsAfterOperationRecordContent objContent = (clsAfterOperationRecordContent)p_objRecordContent;

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(19, out objDPArr);

                objDPArr[0].Value = objContent.m_strAnaesthesiaMode;
                objDPArr[1].Value = objContent.m_strAnaesthesiaModeXML;
                objDPArr[2].Value = objContent.m_strOperationName;
                objDPArr[3].Value = objContent.m_strOperationNameXML;
                objDPArr[4].Value = objContent.m_strOperationDiagnose;
                objDPArr[5].Value = objContent.m_strOperationDiagnoseXML;
                objDPArr[6].Value = objContent.m_strInOperationSeeing;
                objDPArr[7].Value = objContent.m_strInOperationSeeingXML;
                objDPArr[8].Value = objContent.m_strAfterOperationDeal;
                objDPArr[9].Value = objContent.m_strAfterOperationDealXML;
                objDPArr[10].Value = objContent.m_strAfterOperationNotice;
                objDPArr[11].Value = objContent.m_strAfterOperationNoticeXML;
                objDPArr[12].Value = objContent.m_strCutHealUpStatus;
                objDPArr[13].Value = objContent.m_strCutHealUpStatusXML;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = objContent.m_dtmTakeOutStitchesDate;
                objDPArr[15].Value = lngSequence;

                objDPArr[16].Value = objContent.m_strInPatientID;
                objDPArr[17].DbType = DbType.DateTime;
                objDPArr[17].Value = objContent.m_dtmInPatientDate;
                objDPArr[18].DbType = DbType.DateTime;
                objDPArr[18].Value = objContent.m_dtmOpenDate;


                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[12];
                objHRPServ.CreateDatabaseParameter(12, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strAnaesthesiaMode_Right;
                objDPArr2[6].Value = objContent.m_strOperationName_Right;
                objDPArr2[7].Value = objContent.m_strOperationDiagnose_Right;
                objDPArr2[8].Value = objContent.m_strInOperationSeeing_Right;
                objDPArr2[9].Value = objContent.m_strAfterOperationDeal_Right;
                objDPArr2[10].Value = objContent.m_strAfterOperationNotice_Right;
                objDPArr2[11].Value = objContent.m_strCutHealUpStatus_Right;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
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

            }
			
			//����
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
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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

            }
			//����
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

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
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
                    clsAfterOperationRecordContent objRecordContent = new clsAfterOperationRecordContent();
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

                    objRecordContent.m_strAnaesthesiaMode_Right = dtbValue.Rows[0]["ANAESTHESIAMODE_RIGHT"].ToString();
                    objRecordContent.m_strAnaesthesiaMode = dtbValue.Rows[0]["ANAESTHESIAMODE"].ToString();
                    objRecordContent.m_strAnaesthesiaModeXML = dtbValue.Rows[0]["ANAESTHESIAMODEXML"].ToString();
                    objRecordContent.m_strOperationName_Right = dtbValue.Rows[0]["OPERATIONNAME_RIGHT"].ToString();
                    objRecordContent.m_strOperationName = dtbValue.Rows[0]["OPERATIONNAME"].ToString();
                    objRecordContent.m_strOperationNameXML = dtbValue.Rows[0]["OPERATIONNAMEXML"].ToString();
                    objRecordContent.m_strOperationDiagnose_Right = dtbValue.Rows[0]["OPERATIONDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strOperationDiagnose = dtbValue.Rows[0]["OPERATIONDIAGNOSE"].ToString();
                    objRecordContent.m_strOperationDiagnoseXML = dtbValue.Rows[0]["OPERATIONDIAGNOSEXML"].ToString();
                    objRecordContent.m_strInOperationSeeing_Right = dtbValue.Rows[0]["INOPERATIONSEEING_RIGHT"].ToString();
                    objRecordContent.m_strInOperationSeeing = dtbValue.Rows[0]["INOPERATIONSEEING"].ToString();
                    objRecordContent.m_strInOperationSeeingXML = dtbValue.Rows[0]["INOPERATIONSEEINGXML"].ToString();
                    objRecordContent.m_strAfterOperationDeal_Right = dtbValue.Rows[0]["AFTEROPERATIONDEAL_RIGHT"].ToString();
                    objRecordContent.m_strAfterOperationDeal = dtbValue.Rows[0]["AFTEROPERATIONDEAL"].ToString();
                    objRecordContent.m_strAfterOperationDealXML = dtbValue.Rows[0]["AFTEROPERATIONDEALXML"].ToString();
                    objRecordContent.m_strAfterOperationNotice_Right = dtbValue.Rows[0]["AFTEROPERATIONNOTICE_RIGHT"].ToString();
                    objRecordContent.m_strAfterOperationNotice = dtbValue.Rows[0]["AFTEROPERATIONNOTICE"].ToString();
                    objRecordContent.m_strAfterOperationNoticeXML = dtbValue.Rows[0]["AFTEROPERATIONNOTICEXML"].ToString();
                    objRecordContent.m_strCutHealUpStatus_Right = dtbValue.Rows[0]["CUTHEALUPSTATUS_RIGHT"].ToString();
                    objRecordContent.m_strCutHealUpStatus = dtbValue.Rows[0]["CUTHEALUPSTATUS"].ToString();
                    objRecordContent.m_strCutHealUpStatusXML = dtbValue.Rows[0]["CUTHEALUPSTATUSXML"].ToString();
                    if (dtbValue.Rows[0]["TAKEOUTSTITCHESDATE"].ToString() == "")
                        objRecordContent.m_dtmTakeOutStitchesDate = DateTime.MinValue;
                    else objRecordContent.m_dtmTakeOutStitchesDate = DateTime.Parse(dtbValue.Rows[0]["TAKEOUTSTITCHESDATE"].ToString());
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

	}

}
