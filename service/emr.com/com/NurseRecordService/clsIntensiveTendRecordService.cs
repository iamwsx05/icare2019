using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.IntensiveTendRecordService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsIntensiveTendRecordService: clsDiseaseTrackService
	{
		#region SQL
		/// <summary>
		/// ��IntensiveTendRecord1��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= "select createdate,opendate from intensivetendrecord1 where inpatientid = ? and inpatientdate= ? and status=0";

		
		/// <summary>
		/// ��IntensiveTendRecord1�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select createuserid,opendate from intensivetendrecord1 where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		//		/// <summary>
		//		/// ��IntensiveTendRecord1��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ,��ȡ�޸ı�����Ҫ��Ϣ
		//		/// </summary>
		//		private const string c_strGetExistInfoSQL= "";

		
		//		/// <summary>
		//		/// ��IntensiveTendRecordContent1��ȡ�޸ı�����Ҫ��Ϣ��
		//		/// </summary>
		//		private const string c_strGetModifyRecordSQL= "";

		/// <summary>
		/// ��IntensiveTendRecord1��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select deactiveddate,deactivedoperatorid from intensivetendrecord1 where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		/// <summary>
		/// ��Ӽ�¼��IntensiveTendRecord1
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  intensivetendrecord1
						(inpatientid,inpatientdate,opendate,createdate,createuserid,
						ifconfirm,confirmreason,confirmreasonxml,status,recordcontent,recordcontentxml,temperature,
						temperaturexml,pulse,pulsexml,breath,breathxml,bloodpressures,bloodpressuresxml,bloodpressurea,
						bloodpressureaxml,ind,indxml,ini,inixml,pupilleft,pupilleftxml,pupilright,pupilrightxml,echoleft,
						echoleftxml,echoright,echorightxml,outu,outuxml,outv,outvxml,outs,outsxml,oute,outexml,mind,mindxml,class,bloodoxygensaturation,bloodoxygensaturationxml) 
						values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// ��Ӽ�¼��IntensiveTendRecordContent1
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  intensivetendrecordcontent1
						(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,
						recordcontent_right,temperature_right,pulse_right,breath_right,bloodpressures_right,bloodpressurea_right,
						ind_right,ini_right,pupilleft_right,pupilright_right,echoleft_right,echoright_right,outu_right,outv_right,
						outs_right,oute_right,mind_right,bloodoxygensaturation_right) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		
		/// <summary>
		/// �޸ļ�¼��IntensiveTendRecord1
		/// </summary>
		private const string c_strModifyRecordSQL= @"update intensivetendrecord1 
			set recordcontent=?,recordcontentxml=?,temperature=?,temperaturexml=?,pulse=?,pulsexml=?,breath=?,
				breathxml=?,bloodpressures=?,bloodpressuresxml=?,bloodpressurea=?,bloodpressureaxml=?,ind=?,
				indxml=?,ini=?,inixml=?,pupilleft=?,pupilleftxml=?,pupilright=?,pupilrightxml=?,echoleft=?,
				echoleftxml=?,echoright=?,echorightxml=?,outu=?,outuxml=?,outv=?,outvxml=?,outs=?,outsxml=?,
				oute=?,outexml=?,mind=?,mindxml=?,bloodoxygensaturation=?,bloodoxygensaturationxml=?
				where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// �޸ļ�¼��IntensiveTendRecordContent1
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����IntensiveTendRecord1��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= "update intensivetendrecord1 set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		
		/// <summary>
		/// ����IntensiveTendRecord1��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "update  intensivetendrecord1 set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		/// <summary>
		/// ��IntensiveTendRecord1��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select createdate,opendate from intensivetendrecord1 where inpatientid = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";

		/// <summary>
		/// ��IntensiveTendRecord1��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select createdate,opendate from intensivetendrecord1 where inpatientid = ? and inpatientdate= ? and status=1";

		#endregion SQL

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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
		    try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
				 objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                 objDPArr[0].Value = p_strInPatientID;
                 objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				objDPArr[2].Value=p_strDeleteUserID;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes =  objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//���ý��
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
				}
			}
			catch(Exception objEx)
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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
			
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		/// <summary>
		/// ����ָ��������Ϣ����IntensiveTendRecord1��IntensiveTendRecordContent1���ұ������ݡ�
		/// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		/// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������
		/// </summary>
            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.recordcontent,
       t1.recordcontentxml,
       t1.temperature,
       t1.temperaturexml,
       t1.pulse,
       t1.pulsexml,
       t1.breath,
       t1.breathxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.ind,
       t1.indxml,
       t1.ini,
       t1.inixml,
       t1.pupilleft,
       t1.pupilleftxml,
       t1.pupilright,
       t1.pupilrightxml,
       t1.echoleft,
       t1.echoleftxml,
       t1.echoright,
       t1.echorightxml,
       t1.outu,
       t1.outuxml,
       t1.outv,
       t1.outvxml,
       t1.outs,
       t1.outsxml,
       t1.oute,
       t1.outexml,
       t1.mind,
       t1.mindxml,
       t1.class,
       t1.bloodoxygensaturation,
       t1.bloodoxygensaturationxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.recordcontent_right,
       t2.temperature_right,
       t2.pulse_right,
       t2.breath_right,
       t2.bloodpressures_right,
       t2.bloodpressurea_right,
       t2.ind_right,
       t2.ini_right,
       t2.pupilleft_right,
       t2.pupilright_right,
       t2.echoleft_right,
       t2.echoright_right,
       t2.outu_right,
       t2.outv_right,
       t2.outs_right,
       t2.oute_right,
       t2.mind_right,
       t2.bloodoxygensaturation_right
  from intensivetendrecord1 t1
  join intensivetendrecordcontent1 t2 on (t1.inpatientid = t2.inpatientid and
                                         t1.inpatientdate =
                                         t2.inpatientdate and
                                         t1.opendate = t2.opendate and
                                         t1.status = 0 and
                                         t1.inpatientid = ? and
                                         t1.inpatientdate = ? and
                                         t1.opendate = ?)
 order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
                    #region ���ý��
                    clsIntensiveTendRecordContent1 objRecordContent = new clsIntensiveTendRecordContent1();
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

                    //objRecordContent.m_strRecordContent_Right = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    //objRecordContent.m_strRecordContent = dtbValue.Rows[0]["RECORDCONTENT"].ToString();
                    //objRecordContent.m_strRecordContentXml = dtbValue.Rows[0]["RECORDCONTENTXML"].ToString();

                    objRecordContent.m_strTemperature = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
                    objRecordContent.m_strTemperatureAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTemperatureXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    objRecordContent.m_strBreath = dtbValue.Rows[0]["BREATH_RIGHT"].ToString();
                    objRecordContent.m_strBreathAll = dtbValue.Rows[0]["BREATH"].ToString();
                    objRecordContent.m_strBreathXML = dtbValue.Rows[0]["BREATHXML"].ToString();
                    objRecordContent.m_strPulse = dtbValue.Rows[0]["PULSE_RIGHT"].ToString();
                    objRecordContent.m_strPulseAll = dtbValue.Rows[0]["PULSE"].ToString();
                    objRecordContent.m_strPulseXML = dtbValue.Rows[0]["PULSEXML"].ToString();
                    objRecordContent.m_strBloodPressureS = dtbValue.Rows[0]["BLOODPRESSURES_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressureSAll = dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
                    objRecordContent.m_strBloodPressureSXML = dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
                    objRecordContent.m_strBloodPressureA = dtbValue.Rows[0]["BLOODPRESSUREA_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressureAAll = dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
                    objRecordContent.m_strBloodPressureAXML = dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
                    objRecordContent.m_strPupilLeft = dtbValue.Rows[0]["PUPILLEFT_RIGHT"].ToString();
                    objRecordContent.m_strPupilLeftAll = dtbValue.Rows[0]["PUPILLEFT"].ToString();
                    objRecordContent.m_strPupilLeftXML = dtbValue.Rows[0]["PUPILLEFTXML"].ToString();
                    objRecordContent.m_strPupilRight = dtbValue.Rows[0]["PUPILRIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPupilRightAll = dtbValue.Rows[0]["PUPILRIGHT"].ToString();
                    objRecordContent.m_strPupilRightXML = dtbValue.Rows[0]["PUPILRIGHTXML"].ToString();
                    objRecordContent.m_strEchoLeft = dtbValue.Rows[0]["ECHOLEFT_RIGHT"].ToString();
                    objRecordContent.m_strEchoLeftAll = dtbValue.Rows[0]["ECHOLEFT"].ToString();
                    objRecordContent.m_strEchoLeftXML = dtbValue.Rows[0]["ECHOLEFTXML"].ToString();
                    objRecordContent.m_strEchoRight = dtbValue.Rows[0]["ECHORIGHT_RIGHT"].ToString();
                    objRecordContent.m_strEchoRightAll = dtbValue.Rows[0]["ECHORIGHT"].ToString();
                    objRecordContent.m_strEchoRightXML = dtbValue.Rows[0]["ECHORIGHTXML"].ToString();
                    objRecordContent.m_intInD = Convert.ToInt32(dtbValue.Rows[0]["IND_RIGHT"]);
                    objRecordContent.m_strInDAll = dtbValue.Rows[0]["IND"].ToString();
                    objRecordContent.m_strInDXML = dtbValue.Rows[0]["INDXML"].ToString();
                    objRecordContent.m_intInI = Convert.ToInt32(dtbValue.Rows[0]["INI_RIGHT"]);
                    objRecordContent.m_strInIAll = dtbValue.Rows[0]["INI"].ToString();
                    objRecordContent.m_strInIXML = dtbValue.Rows[0]["INIXML"].ToString();
                    objRecordContent.m_intOutU = Convert.ToInt32(dtbValue.Rows[0]["OUTU_RIGHT"]);
                    objRecordContent.m_strOutUAll = dtbValue.Rows[0]["OUTU"].ToString();
                    objRecordContent.m_strOutUXML = dtbValue.Rows[0]["OUTUXML"].ToString();
                    objRecordContent.m_intOutS = Convert.ToInt32(dtbValue.Rows[0]["OUTS_RIGHT"]);
                    objRecordContent.m_strOutSAll = dtbValue.Rows[0]["OUTS"].ToString();
                    objRecordContent.m_strOutSXML = dtbValue.Rows[0]["OUTSXML"].ToString();
                    objRecordContent.m_intOutV = Convert.ToInt32(dtbValue.Rows[0]["OUTV_RIGHT"]);
                    objRecordContent.m_strOutVAll = dtbValue.Rows[0]["OUTV"].ToString();
                    objRecordContent.m_strOutVXML = dtbValue.Rows[0]["OUTVXML"].ToString();
                    objRecordContent.m_intOutE = Convert.ToInt32(dtbValue.Rows[0]["OUTE_RIGHT"]);
                    objRecordContent.m_strOutEAll = dtbValue.Rows[0]["OUTE"].ToString();
                    objRecordContent.m_strOutEXML = dtbValue.Rows[0]["OUTEXML"].ToString();
                    objRecordContent.m_strMind = dtbValue.Rows[0]["Mind_RIGHT"].ToString();
                    objRecordContent.m_strMindAll = dtbValue.Rows[0]["Mind"].ToString();
                    objRecordContent.m_strMindXML = dtbValue.Rows[0]["MindXML"].ToString();
                    objRecordContent.m_strClass = dtbValue.Rows[0]["Class"].ToString();
                    objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[0]["BloodOxygenSaturation_Right"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[0]["BloodOxygenSaturation"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[0]["BloodOxygenSaturationXML"].ToString();

                    p_objRecordContent = objRecordContent;
                    #endregion
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

			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
		/// �����¼�����ݿ⡣�������,����ӱ�.
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			//��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
			clsIntensiveTendRecordContent1 objContent = (clsIntensiveTendRecordContent1)p_objRecordContent;
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
		    try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[41];
				p_objHRPServ.CreateDatabaseParameter(46,out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=objContent.m_dtmCreateDate;
				objDPArr[4].Value=objContent.m_strCreateUserID;
				objDPArr[5].Value=objContent.m_bytIfConfirm;

				if(objContent.m_strConfirmReason==null)
					objDPArr[6].Value=DBNull.Value;
				else
					objDPArr[6].Value=objContent.m_strConfirmReason;
				if(objContent.m_strConfirmReasonXML==null)
					objDPArr[7].Value=DBNull.Value;
				else 
					objDPArr[7].Value=objContent.m_strConfirmReasonXML;
				objDPArr[8].Value=0;
                objDPArr[9].Value = "";// objContent.m_strRecordContent;
                objDPArr[10].Value = "";// objContent.m_strRecordContentXml;

				objDPArr[11].Value=objContent.m_strTemperatureAll;
				objDPArr[12].Value=objContent.m_strTemperatureXML;
				objDPArr[13].Value=objContent.m_strPulseAll;
				objDPArr[14].Value=objContent.m_strPulseXML;
				objDPArr[15].Value=objContent.m_strBreathAll;
				objDPArr[16].Value=objContent.m_strBreathXML;
				objDPArr[17].Value=objContent.m_strBloodPressureSAll;
				objDPArr[18].Value=objContent.m_strBloodPressureSXML;
				objDPArr[19].Value=objContent.m_strBloodPressureAAll;
				objDPArr[20].Value=objContent.m_strBloodPressureAXML;
				objDPArr[21].Value=objContent.m_strInDAll;
				objDPArr[22].Value=objContent.m_strInDXML;
				objDPArr[23].Value=objContent.m_strInIAll;
				objDPArr[24].Value=objContent.m_strInIXML;
				objDPArr[25].Value=objContent.m_strPupilLeftAll;
				objDPArr[26].Value=objContent.m_strPupilLeftXML;
				objDPArr[27].Value=objContent.m_strPupilRightAll;
				objDPArr[28].Value=objContent.m_strPupilRightXML;
				objDPArr[29].Value=objContent.m_strEchoLeftAll;
				objDPArr[30].Value=objContent.m_strEchoLeftXML;
				objDPArr[31].Value=objContent.m_strEchoRightAll;
				objDPArr[32].Value=objContent.m_strEchoRightXML;
				objDPArr[33].Value=objContent.m_strOutUAll;
				objDPArr[34].Value=objContent.m_strOutUXML;
				objDPArr[35].Value=objContent.m_strOutVAll;
				objDPArr[36].Value=objContent.m_strOutVXML;
				objDPArr[37].Value=objContent.m_strOutSAll;
				objDPArr[38].Value=objContent.m_strOutSXML;
				objDPArr[39].Value=objContent.m_strOutEAll;
				objDPArr[40].Value=objContent.m_strOutEXML;
				objDPArr[41].Value=objContent.m_strMindAll;
				objDPArr[42].Value=objContent.m_strMindXML;
				objDPArr[43].Value=objContent.m_strClass;
                objDPArr[44].Value = objContent.m_strBloodOxygenSaturationAll;
                objDPArr[45].Value = objContent.m_strBloodOxygenSaturationXML;
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				//ִ��SQL���
				lngRes =m_lngUpdateRecordContentWithSame(p_objRecordContent,p_objHRPServ);
				if(lngRes<=0)return lngRes;
			
				IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[21];
				p_objHRPServ.CreateDatabaseParameter(23,out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;			
                objDPArr2[5].Value = objContent.m_strRecordContent_Right;
			
				objDPArr2[6].Value=objContent.m_strTemperature;
				objDPArr2[7].Value=objContent.m_strPulse;
				objDPArr2[8].Value=objContent.m_strBreath;
				objDPArr2[9].Value=objContent.m_strBloodPressureS;
				objDPArr2[10].Value=objContent.m_strBloodPressureA;			
				objDPArr2[11].Value=objContent.m_intInD;

				objDPArr2[12].Value=objContent.m_intInI;
				objDPArr2[13].Value=objContent.m_strPupilLeft;
				objDPArr2[14].Value=objContent.m_strPupilRight;
				objDPArr2[15].Value=objContent.m_strEchoLeft;
				objDPArr2[16].Value=objContent.m_strEchoRight;			
				objDPArr2[17].Value=objContent.m_intOutU;

				objDPArr2[18].Value=objContent.m_intOutV;
				objDPArr2[19].Value=objContent.m_intOutS;
				objDPArr2[20].Value=objContent.m_intOutE;
				objDPArr2[21].Value=objContent.m_strMind;
                objDPArr2[22].Value = objContent.m_strBloodOxygenSaturation;
			
				//ִ��SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL,ref lngEff,objDPArr2);
				
			}
			catch(Exception objEx)
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
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			/// <summary>
		/// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
		/// </summary>
		string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from intensivetendrecord1 t1,intensivetendrecordcontent1 t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����			
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from IntensiveTendRecord1 Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
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
                    //if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
                    //p_objModifyInfo = new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
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

            } return lngRes;		
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
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			//��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
			clsIntensiveTendRecordContent1 objContent = (clsIntensiveTendRecordContent1)p_objRecordContent;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(39, out objDPArr);
                objDPArr[0].Value = "";//objContent.m_strRecordContent;
                objDPArr[1].Value = "";//objContent.m_strRecordContentXml;
                objDPArr[2].Value = objContent.m_strTemperatureAll;
                objDPArr[3].Value = objContent.m_strTemperatureXML;
                objDPArr[4].Value = objContent.m_strPulseAll;
                objDPArr[5].Value = objContent.m_strPulseXML;
                objDPArr[6].Value = objContent.m_strBreathAll;
                objDPArr[7].Value = objContent.m_strBreathXML;
                objDPArr[8].Value = objContent.m_strBloodPressureSAll;
                objDPArr[9].Value = objContent.m_strBloodPressureSXML;
                objDPArr[10].Value = objContent.m_strBloodPressureAAll;
                objDPArr[11].Value = objContent.m_strBloodPressureAXML;
                objDPArr[12].Value = objContent.m_strInDAll;
                objDPArr[13].Value = objContent.m_strInDXML;
                objDPArr[14].Value = objContent.m_strInIAll;
                objDPArr[15].Value = objContent.m_strInIXML;
                objDPArr[16].Value = objContent.m_strPupilLeftAll;
                objDPArr[17].Value = objContent.m_strPupilLeftXML;
                objDPArr[18].Value = objContent.m_strPupilRightAll;
                objDPArr[19].Value = objContent.m_strPupilRightXML;
                objDPArr[20].Value = objContent.m_strEchoLeftAll;
                objDPArr[21].Value = objContent.m_strEchoLeftXML;
                objDPArr[22].Value = objContent.m_strEchoRight;
                objDPArr[23].Value = objContent.m_strEchoRightXML;
                objDPArr[24].Value = objContent.m_strOutUAll;
                objDPArr[25].Value = objContent.m_strOutUXML;
                objDPArr[26].Value = objContent.m_strOutVAll;
                objDPArr[27].Value = objContent.m_strOutVXML;
                objDPArr[28].Value = objContent.m_strOutSAll;
                objDPArr[29].Value = objContent.m_strOutSXML;
                objDPArr[30].Value = objContent.m_strOutEAll;
                objDPArr[31].Value = objContent.m_strOutEXML;
                objDPArr[32].Value = objContent.m_strMind;
                objDPArr[33].Value = objContent.m_strMindXML;
                objDPArr[34].Value = objContent.m_strBloodOxygenSaturationAll;
                objDPArr[35].Value = objContent.m_strBloodOxygenSaturationXML;
                objDPArr[36].Value = objContent.m_strInPatientID;
                objDPArr[37].DbType = DbType.DateTime;
                objDPArr[37].Value = objContent.m_dtmInPatientDate;
                objDPArr[38].DbType = DbType.DateTime;
                objDPArr[38].Value = objContent.m_dtmOpenDate;
                //			objDPArr[35].Value=objContent.m_bytStatus;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //ִ��SQL���
                lngRes = m_lngUpdateRecordContentWithSame(p_objRecordContent, p_objHRPServ);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(23, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strRecordContent_Right;

                objDPArr2[6].Value = objContent.m_strTemperature;
                objDPArr2[7].Value = objContent.m_strPulse;
                objDPArr2[8].Value = objContent.m_strBreath;
                objDPArr2[9].Value = objContent.m_strBloodPressureS;
                objDPArr2[10].Value = objContent.m_strBloodPressureA;
                objDPArr2[11].Value = objContent.m_intInD;

                objDPArr2[12].Value = objContent.m_intInI;
                objDPArr2[13].Value = objContent.m_strPupilLeft;
                objDPArr2[14].Value = objContent.m_strPupilRight;
                objDPArr2[15].Value = objContent.m_strEchoLeft;
                objDPArr2[16].Value = objContent.m_strEchoRight;
                objDPArr2[17].Value = objContent.m_intOutU;

                objDPArr2[18].Value = objContent.m_intOutV;
                objDPArr2[19].Value = objContent.m_intOutS;
                objDPArr2[20].Value = objContent.m_intOutE;
                objDPArr2[21].Value = objContent.m_strMind;
                objDPArr2[22].Value = objContent.m_strBloodOxygenSaturation;

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

            }
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
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

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

            } return lngRes;
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
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		/// <summary>
		/// ��IntensiveTendRecord1��IntensiveTendRecordContent1��ȡLastModifyDate��FirstPrintDate
		/// </summary>
		 string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.firstprintdate,b.modifydate from intensivetendrecord1 a,
					intensivetendrecordcontent1 b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;


			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.recordcontent,
       t1.recordcontentxml,
       t1.temperature,
       t1.temperaturexml,
       t1.pulse,
       t1.pulsexml,
       t1.breath,
       t1.breathxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.ind,
       t1.indxml,
       t1.ini,
       t1.inixml,
       t1.pupilleft,
       t1.pupilleftxml,
       t1.pupilright,
       t1.pupilrightxml,
       t1.echoleft,
       t1.echoleftxml,
       t1.echoright,
       t1.echorightxml,
       t1.outu,
       t1.outuxml,
       t1.outv,
       t1.outvxml,
       t1.outs,
       t1.outsxml,
       t1.oute,
       t1.outexml,
       t1.mind,
       t1.mindxml,
       t1.class,
       t1.bloodoxygensaturation,
       t1.bloodoxygensaturationxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.recordcontent_right,
       t2.temperature_right,
       t2.pulse_right,
       t2.breath_right,
       t2.bloodpressures_right,
       t2.bloodpressurea_right,
       t2.ind_right,
       t2.ini_right,
       t2.pupilleft_right,
       t2.pupilright_right,
       t2.echoleft_right,
       t2.echoright_right,
       t2.outu_right,
       t2.outv_right,
       t2.outs_right,
       t2.oute_right,
       t2.mind_right,
        t2.bloodoxygensaturation_right
  from intensivetendrecord1 t1, intensivetendrecordcontent1 t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
                    #region ���ý��
                    clsIntensiveTendRecordContent1 objRecordContent = new clsIntensiveTendRecordContent1();
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
                    objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
                    objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

                    //objRecordContent.m_strRecordContent_Right = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
                    //objRecordContent.m_strRecordContent = dtbValue.Rows[0]["RECORDCONTENT"].ToString();
                    //objRecordContent.m_strRecordContentXml = dtbValue.Rows[0]["RECORDCONTENTXML"].ToString();

                    objRecordContent.m_strTemperature = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
                    objRecordContent.m_strTemperatureAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTemperatureXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    objRecordContent.m_strBreath = dtbValue.Rows[0]["BREATH_RIGHT"].ToString();
                    objRecordContent.m_strBreathAll = dtbValue.Rows[0]["BREATH"].ToString();
                    objRecordContent.m_strBreathXML = dtbValue.Rows[0]["BREATHXML"].ToString();
                    objRecordContent.m_strPulse = dtbValue.Rows[0]["PULSE_RIGHT"].ToString();
                    objRecordContent.m_strPulseAll = dtbValue.Rows[0]["PULSE"].ToString();
                    objRecordContent.m_strPulseXML = dtbValue.Rows[0]["PULSEXML"].ToString();
                    objRecordContent.m_strBloodPressureS = dtbValue.Rows[0]["BLOODPRESSURES_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressureSAll = dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
                    objRecordContent.m_strBloodPressureSXML = dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
                    objRecordContent.m_strBloodPressureA = dtbValue.Rows[0]["BLOODPRESSUREA_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressureAAll = dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
                    objRecordContent.m_strBloodPressureAXML = dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
                    objRecordContent.m_strPupilLeft = dtbValue.Rows[0]["PUPILLEFT_RIGHT"].ToString();
                    objRecordContent.m_strPupilLeftAll = dtbValue.Rows[0]["PUPILLEFT"].ToString();
                    objRecordContent.m_strPupilLeftXML = dtbValue.Rows[0]["PUPILLEFTXML"].ToString();
                    objRecordContent.m_strPupilRight = dtbValue.Rows[0]["PUPILRIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPupilRightAll = dtbValue.Rows[0]["PUPILRIGHT"].ToString();
                    objRecordContent.m_strPupilRightXML = dtbValue.Rows[0]["PUPILRIGHTXML"].ToString();
                    objRecordContent.m_strEchoLeft = dtbValue.Rows[0]["ECHOLEFT_RIGHT"].ToString();
                    objRecordContent.m_strEchoLeftAll = dtbValue.Rows[0]["ECHOLEFT"].ToString();
                    objRecordContent.m_strEchoLeftXML = dtbValue.Rows[0]["ECHOLEFTXML"].ToString();
                    objRecordContent.m_strEchoRight = dtbValue.Rows[0]["ECHORIGHT_RIGHT"].ToString();
                    objRecordContent.m_strEchoRightAll = dtbValue.Rows[0]["ECHORIGHT"].ToString();
                    objRecordContent.m_strEchoRightXML = dtbValue.Rows[0]["ECHORIGHTXML"].ToString();
                    objRecordContent.m_intInD = Convert.ToInt32(dtbValue.Rows[0]["IND_RIGHT"]);
                    objRecordContent.m_strInDAll = dtbValue.Rows[0]["IND"].ToString();
                    objRecordContent.m_strInDXML = dtbValue.Rows[0]["INDXML"].ToString();
                    objRecordContent.m_intInI = Convert.ToInt32(dtbValue.Rows[0]["INI_RIGHT"]);
                    objRecordContent.m_strInIAll = dtbValue.Rows[0]["INI"].ToString();
                    objRecordContent.m_strInIXML = dtbValue.Rows[0]["INIXML"].ToString();
                    objRecordContent.m_intOutU = Convert.ToInt32(dtbValue.Rows[0]["OUTU_RIGHT"]);
                    objRecordContent.m_strOutUAll = dtbValue.Rows[0]["OUTU"].ToString();
                    objRecordContent.m_strOutUXML = dtbValue.Rows[0]["OUTUXML"].ToString();
                    objRecordContent.m_intOutS = Convert.ToInt32(dtbValue.Rows[0]["OUTS_RIGHT"]);
                    objRecordContent.m_strOutSAll = dtbValue.Rows[0]["OUTS"].ToString();
                    objRecordContent.m_strOutSXML = dtbValue.Rows[0]["OUTSXML"].ToString();
                    objRecordContent.m_intOutV = Convert.ToInt32(dtbValue.Rows[0]["OUTV_RIGHT"]);
                    objRecordContent.m_strOutVAll = dtbValue.Rows[0]["OUTV"].ToString();
                    objRecordContent.m_strOutVXML = dtbValue.Rows[0]["OUTVXML"].ToString();
                    objRecordContent.m_intOutE = Convert.ToInt32(dtbValue.Rows[0]["OUTE_RIGHT"]);
                    objRecordContent.m_strOutEAll = dtbValue.Rows[0]["OUTE"].ToString();
                    objRecordContent.m_strOutEXML = dtbValue.Rows[0]["OUTEXML"].ToString();
                    objRecordContent.m_strMind = Convert.ToString(dtbValue.Rows[0]["Mind_RIGHT"]);
                    objRecordContent.m_strMindAll = Convert.ToString(dtbValue.Rows[0]["Mind"]);
                    objRecordContent.m_strMindXML = Convert.ToString(dtbValue.Rows[0]["MindXML"]);
                    objRecordContent.m_strClass = dtbValue.Rows[0]["Class"].ToString();
                    objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[0]["BloodOxygenSaturation_RIGHT"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[0]["BloodOxygenSaturation"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[0]["BloodOxygenSaturationXML"].ToString();

                    p_objRecordContent = objRecordContent;
                    #endregion
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

            } return lngRes;
		}

		/// <summary>
		/// ����������Ϣ,ͬһ��α���һ��
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngUpdateRecordContentWithSame(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsIntensiveTendRecordContent1 objContent = (clsIntensiveTendRecordContent1)p_objRecordContent;
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"update IntensiveTendRecord1 
								Set RecordContent=?,RecordContentXML=? where Class=? and InpatientID=? and inpatientdate=?";
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = "";//objContent.m_strRecordContent;
                objDPArr[1].Value = "";//objContent.m_strRecordContentXml;
                objDPArr[2].Value = objContent.m_strClass;
                objDPArr[3].Value = objContent.m_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objContent.m_dtmInPatientDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
		/// ��ȡͬһ��β��̼�¼����
		/// </summary>
		/// <param name="strClass"></param>
		/// <param name="strRecordConment"></param>
		/// <param name="strRecordComentXML"></param>
		/// <returns></returns>

		[AutoComplete]
		public long m_lngGetRecordContentWithSame(string strClass,string strID,out string strRecordConment,out string strRecordComentXML)
		{
			strRecordConment="";
			strRecordComentXML="";
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"select t.recordcontent,t.recordcontentxml from intensivetendrecord1 t where class=? and inpatientid=? and status=0";
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[41];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strClass;
                objDPArr[1].Value = strID;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    strRecordConment = dtbValue.Rows[0]["recordcontent"].ToString();
                    strRecordComentXML = dtbValue.Rows[0]["recordcontentxml"].ToString();
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

            } return lngRes;	


		}

		 /// <summary>
		 /// ���没�̼�¼����
		 /// </summary>
		 /// <param name="p_objRecordContent">������Ϣ</param>
		 /// <param name="p_objHRPServ"></param>
		 /// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecordContent(clsTrackRecordContent p_objRecordContent)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			//��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
            clsCourseDiseasesRecord objContent = (clsCourseDiseasesRecord)p_objRecordContent;
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSQL = @"insert into intensivetendrecorddetail1 (inpatientid,inpatientdate,opendate,
								createdate,createuserid,modifyuserid,recordcontent,recordcontentxml,class,status) 
								values (?,?,?,?,?,?,?,?,?,?)";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(10, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = objContent.m_strModifyUserID;
                objDPArr[6].Value = objContent.m_strDiseasesRecordContent;
                objDPArr[7].Value = objContent.m_strDiseasesRecordContentXml;
                objDPArr[8].Value = objContent.m_strClass;
                objDPArr[9].Value = 0;
                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
		}
		/// <summary>
		/// �޸Ĳ��̼�¼����
		/// </summary>
		/// <param name="p_objRecordContent">������Ϣ</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecordContent(clsTrackRecordContent p_objRecordContent)
		{
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;

                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
                clsCourseDiseasesRecord objContent = (clsCourseDiseasesRecord)p_objRecordContent;
                string strSQL = @"update intensivetendrecorddetail1 set opendate=?,
							modifyuserid=?,recordcontent=?,recordcontentxml=?
							where inpatientid=? and inpatientdate=? and createdate=?";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmOpenDate;
                objDPArr[1].Value = objContent.m_strModifyUserID;
                objDPArr[2].Value = objContent.m_strDiseasesRecordContent;
                objDPArr[3].Value = objContent.m_strDiseasesRecordContentXml;
                objDPArr[4].Value = objContent.m_strInPatientID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = objContent.m_dtmInPatientDate;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = objContent.m_dtmCreateDate;//ע��˴��Ĵ���ʱ��Ϊ���̼�¼���ݵĴ���ʱ��

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;

		}
		/// <summary>
		///  ��ȡָ�����̼�¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">סԺ����</param>
		/// <param name="p_strCreateDate">��������</param>
		/// <param name="p_strRecordContent">���̼�¼����</param>
		/// <param name="p_strRecordContentXML">���̼�¼���ݺۼ�</param>
		/// <returns>����ֵ</returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,
			out string[] p_strRecordContentArr)
		{
			long lngRes=0;
			p_strRecordContentArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.createdate,
       t.createuserid,
       t.modifyuserid,
       t.recordcontent,
       t.recordcontentxml,
       t.class,
       t.description,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       d.lastname_vchr
  from intensivetendrecorddetail1 t, t_bse_employee d
 where t.createuserid = d.empno_chr
   and t.status = 0
   and inpatientid = ?
   and t.inpatientdate = ?
   and t.createdate = ?";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_strRecordContentArr = new string[5];
                    p_strRecordContentArr[0] = dtbValue.Rows[0]["CreateDate"].ToString();
                    p_strRecordContentArr[1] = dtbValue.Rows[0]["ModifyUserID"].ToString();
                    p_strRecordContentArr[2] = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
                    p_strRecordContentArr[3] = dtbValue.Rows[0]["RecordContent"].ToString();
                    p_strRecordContentArr[4] = dtbValue.Rows[0]["RecordContentXML"].ToString();
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

            } return lngRes;

		}

		/// <summary>
		/// ɾ��ָ�����̼�¼����
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strDelDate"></param>
		/// <param name="p_strDelID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strCreateDate,
			string p_strDelDate, 
			string p_strDelID)
		{
			long lngRes=0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @" update intensivetendrecorddetail1 set status=1,deactiveddate=?,deactivedoperatorid=?
								where inpatientid=? and  inpatientdate=? and  createdate=?";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strDelDate);
                objDPArr[1].Value = p_strDelID;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strCreateDate);
                //ִ�в�ѯ 
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;

		}

		/// <summary>
		///  ��ȡָ�����˵Ĳ��̼�¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">סԺ����</param>
		/// <param name="p_strRecordContentArr">���̼�¼��������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContentWithInpatient(string p_strInPatientID,string p_strInPatientDate,
			out string[][] p_strRecordContentArr)
		{
			long lngRes=0;
			p_strRecordContentArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       modifyuserid,
       recordcontent,
       recordcontentxml,
       class,
       description,
       status,
       deactiveddate,
       deactivedoperatorid
  from intensivetendrecorddetail1
 where status = 0
   and inpatientid = ?
   and inpatientdate = ?";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0)
                {
                    p_strRecordContentArr = new string[dtbValue.Rows.Count][];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_strRecordContentArr[i] = new string[3];
                        p_strRecordContentArr[i][0] = dtbValue.Rows[0]["RecordContent"].ToString();
                        p_strRecordContentArr[i][1] = dtbValue.Rows[0]["RecordContentXML"].ToString();
                        p_strRecordContentArr[i][2] = dtbValue.Rows[0]["class"].ToString();

                    }
                }
            }
            catch (Exception objEx)
            {



                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;

		}	

		/// <summary>
		///  ��ȡָ������ɾ�����̼�¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">סԺ����</param>
		/// <param name="p_strOpenDate">��������</param>
		/// <param name="p_objRecordContent">���̼�¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDelRecordContentWithInpatient(string p_strInPatientID,string p_strInPatientDate,
			string p_strOpenDate,
            out clsCourseDiseasesRecord p_objRecordContent)
		{
			long lngRes=0;
			p_objRecordContent=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.modifyuserid,
       a.recordcontent,
       a.recordcontentxml,
       a.class,
       a.description,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid
  from intensivetendrecorddetail1 a
 where a.status = 1
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?";

                //��ȡIDataParameter����
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0)
                {
                    p_objRecordContent = new clsCourseDiseasesRecord();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objRecordContent.m_strDiseasesRecordContent = dtbValue.Rows[0]["RecordContent"].ToString();
                        p_objRecordContent.m_strDiseasesRecordContentXml = dtbValue.Rows[0]["RecordContentXML"].ToString();
                        p_objRecordContent.m_strClass = dtbValue.Rows[0]["class"].ToString();
                        p_objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                        p_objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
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
