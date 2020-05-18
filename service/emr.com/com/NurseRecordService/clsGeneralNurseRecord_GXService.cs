using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsGeneralNurseRecord_GXService
{
	/// <summary>
	/// clsMainGeneralNurseRecord_GXService ��ժҪ˵����
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsGeneralNurseRecord_GXService : clsDiseaseTrackService
	{
		public clsGeneralNurseRecord_GXService()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region SQL���
		/// <summary>
		/// ��GENERALNURSERECORD_GXRECORD��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from generalnurserecord_gxrecord
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// ��GENERALNURSERECORD_GXRECORD�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from generalnurserecord_gxrecord
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// ��GENERALNURSERECORD_GXRECORD��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from generalnurserecord_gxrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// ��Ӽ�¼��GENERALNURSERECORD_GXRECORD
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  generalnurserecord_gxrecord
						(inpatientid,inpatientdate,opendate,createdate,createuserid,
						ifconfirm,confirmreason,confirmreasonxml,status,temperature,
						temperaturexml,pulse,pulsexml,respiration,respirationxml,
						bloodpressures,bloodpressuresxml,bloodpressurea,bloodpressureaxml,
                        recorddate,class,heartrate,heartratexml,custom1,custom1xml,custom2,
                        custom2xml,custom1name,custom2name) 
						values(?,?,?,?,?,
							   ?,?,?,?,?,
							   ?,?,?,?,?,
							   ?,?,?,?,?,
                               ?,?,?,?,?,
                               ?,?,?,?)";

		/// <summary>
		/// ��Ӽ�¼��GENERALNURSERECORD_GXCONTENT
		/// </summary>
		private const string c_strAddNewRecordContentSQL= @"insert into  generalnurserecord_gxcontent
						(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,
						temperature_right,pulse_right,respiration_right,bloodpressures_right,
                        bloodpressurea_right,heartrate_right,custom1_right,custom2_right)
					    values(?,?,?,?,?,
							   ?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��GENERALNURSERECORD_GXRECORD
		/// </summary>
		private const string c_strModifyRecordSQL= @"update generalnurserecord_gxrecord 
			set temperature=?,temperaturexml=?,pulse=?,pulsexml=?,respiration=?,
				respirationxml=?,bloodpressures=?,bloodpressuresxml=?,bloodpressurea=?,
                bloodpressureaxml=?,heartrate=?,heartratexml=?,custom1=?,custom1xml=?,custom2=?,custom2xml=?
				where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// �޸ļ�¼��GENERALNURSERECORD_GXCONTENT
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����GENERALNURSERECORD_GXRECORD��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update generalnurserecord_gxrecord
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// ����GENERALNURSERECORD_GXRECORD��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update generalnurserecord_gxrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// ��GENERALNURSERECORD_GXRECORD��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from generalnurserecord_gxrecord
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// ��GENERALNURSERECORD_GXRECORD��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from generalnurserecord_gxrecord
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";
		#endregion

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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
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
				string strTmp=objEx.Message;
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//������                              
				if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
					return (long)enmOperationResult.Parameter_Error;			
			
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strOpenDate);
			
				//ִ��SQL
				long lngEff=0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
				
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				objDPArr[2].Value=p_strDeleteUserID;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
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
				string strTmp=objEx.Message;
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
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
				string strTmp=objEx.Message;
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

            string c_strGetRecordContentSQL = @"select t1.inpatientid,
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
       t1.temperature,
       t1.temperaturexml,
       t1.pulse,
       t1.pulsexml,
       t1.respiration,
       t1.respirationxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.recorddate,
       t1.class,
       t1.heartrate,
       t1.heartratexml,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom1name,
       t1.custom2name,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_right,
       t2.pulse_right,
       t2.respiration_right,
       t2.bloodpressures_right,
       t2.bloodpressurea_right,
       t2.heartrate_right,
       t2.custom1_right,
       t2.custom2_right
  from generalnurserecord_gxrecord t1, generalnurserecord_gxcontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t2.modifydate = (select max(modifydate)
                          from generalnurserecord_gxcontent
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)";
		
			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					#region ���ý��
					clsGeneralNurseRecordContent_GX objRecordContent=new clsGeneralNurseRecordContent_GX();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

					objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
					objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
					objRecordContent.m_strPULSE_RIGHT = dtbValue.Rows[0]["PULSE_RIGHT"].ToString();
					objRecordContent.m_strPULSEAll = dtbValue.Rows[0]["PULSE"].ToString();
					objRecordContent.m_strPULSEXML = dtbValue.Rows[0]["PULSEXML"].ToString();
					objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[0]["RESPIRATION_RIGHT"].ToString();
					objRecordContent.m_strRESPIRATIONAll = dtbValue.Rows[0]["RESPIRATION"].ToString();
					objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
					objRecordContent.m_strBLOODPRESSURES_RIGHT = dtbValue.Rows[0]["BLOODPRESSURES_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSURESAll = dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
					objRecordContent.m_strBLOODPRESSURESXML = dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
					objRecordContent.m_strBLOODPRESSUREA_RIGHT = dtbValue.Rows[0]["BLOODPRESSUREA_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSUREAAll = dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
					objRecordContent.m_strBLOODPRESSUREAXML = dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
					objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
					objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
					objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATE_RIGHT"].ToString();
					objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[0]["RECORDDATE"].ToString());
					objRecordContent.m_intClass = dtbValue.Rows[0]["CLASS"]==DBNull.Value ? 0:Convert.ToInt32(dtbValue.Rows[0]["CLASS"]);

                    objRecordContent.m_strCUSTOM1 = dtbValue.Rows[0]["CUSTOM1"].ToString();
                    objRecordContent.m_strCUSTOM1_RIGHT = dtbValue.Rows[0]["CUSTOM1_RIGHT"].ToString();
                    objRecordContent.m_strCUSTOM1XML = dtbValue.Rows[0]["CUSTOM1XML"].ToString();

                    objRecordContent.m_strCUSTOM2 = dtbValue.Rows[0]["CUSTOM2"].ToString();
                    objRecordContent.m_strCUSTOM2_RIGHT = dtbValue.Rows[0]["CUSTOM2_RIGHT"].ToString();
                    objRecordContent.m_strCUSTOM2XML = dtbValue.Rows[0]["CUSTOM2XML"].ToString();

                    objRecordContent.m_strCUSTOM1NAME = dtbValue.Rows[0]["CUSTOM1NAME"].ToString();
                    objRecordContent.m_strCUSTOM2NAME = dtbValue.Rows[0]["CUSTOM2NAME"].ToString();


				p_objRecordContent=	objRecordContent;
					#endregion
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmCreateDate;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL,ref dtbValue,objDPArr);
					
				//�鿴DataTable.Rows.Count
				//�������1����ʾ�Ѿ��и�CreateDate�����Ҳ���ɾ���ļ�¼��
				//��ȡ�ü�¼����Ϣ����ֵ��p_objModifyInfo�С�����ֵʹ��Record_Already_Exist
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					p_objModifyInfo=new clsPreModifyInfo();
					p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
					return (long)enmOperationResult.Record_Already_Exist;
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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
			clsGeneralNurseRecordContent_GX objRecordContent = (clsGeneralNurseRecordContent_GX)p_objRecordContent;
			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(29,out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=objRecordContent.m_dtmCreateDate;
				objDPArr[4].Value=objRecordContent.m_strCreateUserID;
				objDPArr[5].Value=objRecordContent.m_bytIfConfirm;

				if(objRecordContent.m_strConfirmReason==null)
					objDPArr[6].Value=DBNull.Value;
				else
					objDPArr[6].Value=objRecordContent.m_strConfirmReason;
				if(objRecordContent.m_strConfirmReasonXML==null)
					objDPArr[7].Value=DBNull.Value;
				else 
					objDPArr[7].Value=objRecordContent.m_strConfirmReasonXML;
				objDPArr[8].Value=0;

				objDPArr[9].Value=objRecordContent.m_strTEMPERATUREAll;
				objDPArr[10].Value=objRecordContent.m_strTEMPERATUREXML;
				objDPArr[11].Value=objRecordContent.m_strPULSEAll;
				objDPArr[12].Value=objRecordContent.m_strPULSEXML;
				objDPArr[13].Value=objRecordContent.m_strRESPIRATIONAll;
				objDPArr[14].Value=objRecordContent.m_strRESPIRATIONXML;
				objDPArr[15].Value=objRecordContent.m_strBLOODPRESSURESAll;
				objDPArr[16].Value=objRecordContent.m_strBLOODPRESSURESXML;
				objDPArr[17].Value=objRecordContent.m_strBLOODPRESSUREAAll;
				objDPArr[18].Value=objRecordContent.m_strBLOODPRESSUREAXML;
                objDPArr[19].DbType = DbType.DateTime;
				objDPArr[19].Value=objRecordContent.m_dtmRECORDDATE;
				objDPArr[20].Value=objRecordContent.m_intClass;
				objDPArr[21].Value = objRecordContent.m_strHEARTRATE;
				objDPArr[22].Value = objRecordContent.m_strHEARTRATEXML;
                objDPArr[23].Value = objRecordContent.m_strCUSTOM1;
                objDPArr[24].Value = objRecordContent.m_strCUSTOM1XML;
                objDPArr[25].Value = objRecordContent.m_strCUSTOM2;
                objDPArr[26].Value = objRecordContent.m_strCUSTOM2XML;
                objDPArr[27].Value = objRecordContent.m_strCUSTOM1NAME;
                objDPArr[28].Value = objRecordContent.m_strCUSTOM2NAME;

				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;
			
				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(13,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;	
			
				objDPArr2[5].Value=objRecordContent.m_strTEMPERATURE_RIGHT;
				objDPArr2[6].Value=objRecordContent.m_strPULSE_RIGHT;
				objDPArr2[7].Value=objRecordContent.m_strRESPIRATION_RIGHT;
				objDPArr2[8].Value=objRecordContent.m_strBLOODPRESSURES_RIGHT;
				objDPArr2[9].Value=objRecordContent.m_strBLOODPRESSUREA_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strHEARTRATE_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strCUSTOM1_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strCUSTOM2_RIGHT;
			
				//ִ��SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL,ref lngEff,objDPArr2);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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

			clsGeneralNurseRecordContent_GX objRecordContent = (clsGeneralNurseRecordContent_GX)p_objRecordContent;
			/// <summary>
			/// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from generalnurserecord_gxrecord t1,generalnurserecord_gxcontent t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status = 0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����			
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL,ref dtbValue,objDPArr);
			        
				//���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL,ref dtbValue,objDPArr);
				
					if(lngRes>0 && dtbValue.Rows.Count ==1)
					{
						p_objModifyInfo=new clsPreModifyInfo();
						p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
						p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
					}
					return (long)enmOperationResult.Record_Already_Delete;
				}	
					//��DataTable�л�ȡModifyDate��ʹ֮��objRecordContent.m_dtmModifyDate�Ƚ�
				else if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//�����ͬ������DB_Succees
                    //if(objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

					//���򣬷���Record_Already_Modify
                    //p_objModifyInfo=new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
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
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			clsGeneralNurseRecordContent_GX objRecordContent = (clsGeneralNurseRecordContent_GX)p_objRecordContent;
			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(19,out objDPArr);
				objDPArr[0].Value=objRecordContent.m_strTEMPERATUREAll;
				objDPArr[1].Value=objRecordContent.m_strTEMPERATUREXML;
				objDPArr[2].Value=objRecordContent.m_strPULSEAll;
				objDPArr[3].Value=objRecordContent.m_strPULSEXML;
				objDPArr[4].Value=objRecordContent.m_strRESPIRATIONAll;
				objDPArr[5].Value=objRecordContent.m_strRESPIRATIONXML;
				objDPArr[6].Value=objRecordContent.m_strBLOODPRESSURESAll;
				objDPArr[7].Value=objRecordContent.m_strBLOODPRESSURESXML;
				objDPArr[8].Value=objRecordContent.m_strBLOODPRESSUREAAll;
				objDPArr[9].Value=objRecordContent.m_strBLOODPRESSUREAXML;
				objDPArr[10].Value = objRecordContent.m_strHEARTRATE;
				objDPArr[11].Value = objRecordContent.m_strHEARTRATEXML;
                objDPArr[12].Value = objRecordContent.m_strCUSTOM1;
                objDPArr[13].Value = objRecordContent.m_strCUSTOM1XML;
                objDPArr[14].Value = objRecordContent.m_strCUSTOM2;
                objDPArr[15].Value = objRecordContent.m_strCUSTOM2XML;

                objDPArr[16].Value = objRecordContent.m_strInPatientID;
                objDPArr[17].DbType = DbType.DateTime;
                objDPArr[17].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[18].DbType = DbType.DateTime;
				objDPArr[18].Value=objRecordContent.m_dtmOpenDate;
		
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;


				IDataParameter[] objDPArr2 = null; 
				p_objHRPServ.CreateDatabaseParameter(13,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;		
			
				objDPArr2[5].Value=objRecordContent.m_strTEMPERATURE_RIGHT;
				objDPArr2[6].Value=objRecordContent.m_strPULSE_RIGHT;
				objDPArr2[7].Value=objRecordContent.m_strRESPIRATION_RIGHT;
				objDPArr2[8].Value=objRecordContent.m_strBLOODPRESSURES_RIGHT;
				objDPArr2[9].Value=objRecordContent.m_strBLOODPRESSUREA_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strHEARTRATE_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strCUSTOM1_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strCUSTOM2_RIGHT;
				//ִ��SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL,ref lngEff,objDPArr2);
			
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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

			clsGeneralNurseRecordContent_GX objRecordContent = (clsGeneralNurseRecordContent_GX)p_objRecordContent;
			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;	
				objDPArr[4].Value=objRecordContent.m_dtmOpenDate;		
		
				//ִ��SQL
				long lngEff=0;
				lngRes= p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
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
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			/// <summary>
			/// ��IntensiveTendRecord1��IntensiveTendRecordContent1��ȡLastModifyDate��FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.firstprintdate,b.modifydate from generalnurserecord_gxrecord a,
					generalnurserecord_gxcontent b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;


			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//���ý��
					p_strFirstPrintDate=dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
					p_dtmModifyDate=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());								
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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

            string c_strGetDeleteRecordContentSQL = @"select t1.inpatientid,
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
       t1.temperature,
       t1.temperaturexml,
       t1.pulse,
       t1.pulsexml,
       t1.respiration,
       t1.respirationxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.recorddate,
       t1.class,
       t1.heartrate,
       t1.heartratexml,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom1name,
       t1.custom2name,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_right,
       t2.pulse_right,
       t2.respiration_right,
       t2.bloodpressures_right,
       t2.bloodpressurea_right,
       t2.heartrate_right,
       t2.custom1_right,
       t2.custom2_right
  from generalnurserecord_gxrecord t1, generalnurserecord_gxcontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t2.modifydate = (select max(modifydate)
                          from generalnurserecord_gxcontent
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)";
		
			long lngRes = 0;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					#region ���ý��
					clsGeneralNurseRecordContent_GX objRecordContent=new clsGeneralNurseRecordContent_GX();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
				
					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
//					objRecordContent.m_strContentCreateUserName = dtbValue.Rows[0]["CreateUserName"].ToString();
					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

					objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strTEMPERATUREAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
					objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
					objRecordContent.m_strPULSE_RIGHT = dtbValue.Rows[0]["PULSE_RIGHT"].ToString();
					objRecordContent.m_strPULSEAll = dtbValue.Rows[0]["PULSE"].ToString();
					objRecordContent.m_strPULSEXML = dtbValue.Rows[0]["PULSEXML"].ToString();
					objRecordContent.m_strRESPIRATION_RIGHT = dtbValue.Rows[0]["RESPIRATION_RIGHT"].ToString();
					objRecordContent.m_strRESPIRATIONAll = dtbValue.Rows[0]["RESPIRATION"].ToString();
					objRecordContent.m_strRESPIRATIONXML = dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
					objRecordContent.m_strBLOODPRESSURES_RIGHT = dtbValue.Rows[0]["BLOODPRESSURES_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSURESAll = dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
					objRecordContent.m_strBLOODPRESSURESXML = dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
					objRecordContent.m_strBLOODPRESSUREA_RIGHT = dtbValue.Rows[0]["BLOODPRESSUREA_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSUREAAll = dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
					objRecordContent.m_strBLOODPRESSUREAXML = dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
					objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
					objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
					objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATE_RIGHT"].ToString();
					objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[0]["RECORDDATE"].ToString());
					objRecordContent.m_intClass = dtbValue.Rows[0]["CLASS"]==DBNull.Value ? 0:Convert.ToInt32(dtbValue.Rows[0]["CLASS"]);

                    objRecordContent.m_strCUSTOM1 = dtbValue.Rows[0]["CUSTOM1"].ToString();
                    objRecordContent.m_strCUSTOM1_RIGHT = dtbValue.Rows[0]["CUSTOM1_RIGHT"].ToString();
                    objRecordContent.m_strCUSTOM1XML = dtbValue.Rows[0]["CUSTOM1XML"].ToString();

                    objRecordContent.m_strCUSTOM2 = dtbValue.Rows[0]["CUSTOM2"].ToString();
                    objRecordContent.m_strCUSTOM2_RIGHT = dtbValue.Rows[0]["CUSTOM2_RIGHT"].ToString();
                    objRecordContent.m_strCUSTOM2XML = dtbValue.Rows[0]["CUSTOM2XML"].ToString();

                    objRecordContent.m_strCUSTOM1NAME = dtbValue.Rows[0]["CUSTOM1NAME"].ToString();
                    objRecordContent.m_strCUSTOM2NAME = dtbValue.Rows[0]["CUSTOM2NAME"].ToString();

					p_objRecordContent=	objRecordContent;	
					#endregion 
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}

        /// <summary>
        /// �����Զ�������
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strColumnIndex"></param>
        /// <param name="p_strColumnName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCustomColumnName(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strColumnIndex,
            string p_strColumnName)
        {
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"update generalnurserecord_gxrecord set " + p_strColumnIndex + @"=? 
								where inpatientid=? and  inpatientdate=?";
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strColumnName;
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;
        }


		/// <summary>
		/// ���²����¼
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngUpdateDetail(clsGeneralNurseRecordContent_GXDetail p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strINPATIENTID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;
			try
			{
				string strSQL=@"update generalnurserecord_gxdetail 
								set recordcontent=?,recordcontentxml=? ,recordcontent_right=?
								where inpatientid=? and inpatientdate=? and recorddate=?";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(6,out objDPArr);
				objDPArr[0].Value=p_objRecordContent.m_strRECORDCONTENTAll;
				objDPArr[1].Value=p_objRecordContent.m_strRECORDCONTENTXML;
				objDPArr[2].Value=p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[3].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[5].DbType = DbType.DateTime;
				objDPArr[5].Value=p_objRecordContent.m_dtmRECORDDATE;

				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}

		/// <summary>
		/// ��ȡ�����¼����
		/// </summary>
		/// <param name="dtmRecordDate"></param>
		/// <param name="strInPatientID"></param>
		/// <param name="strRecordContent"></param>
		/// <param name="strRecordCotentXML"></param>
		/// <returns></returns>

		[AutoComplete]
		public long m_lngGetDetail(DateTime dtmRecordDate, string strInPatientID,out string strRecordContent,out string strRecordCotentXML)
		{
			strRecordContent="";
			strRecordCotentXML="";
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@"select t.recordcontent_right, t.recordcontentxml
									from generalnurserecord_gxdetail t
								where recorddate = ?
									and inpatientid = ?
									and status = 0";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[41];
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=dtmRecordDate;
				objDPArr[1].Value=strInPatientID;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					strRecordContent=dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
					strRecordCotentXML=dtbValue.Rows[0]["recordcontentxml"].ToString();
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

                    //p_objHRPServ.Dispose();
                }
			return lngRes;
		}

		/// <summary>
		/// ���没���¼����
		/// </summary>
		/// <param name="p_objRecordContent">������Ϣ</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewDetail(clsGeneralNurseRecordContent_GXDetail p_objRecordContent)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strINPATIENTID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@"insert into generalnurserecord_gxdetail (inpatientid,inpatientdate,opendate,
								createdate,createuserid,modifyuserid,recordcontent,recordcontentxml,recorddate,
								status,recordcontent_right,modifydate,class) 
								values (?,?,?,?,?,?,?,?,?,0,?,?,?)";

				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(12,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOPENDATE;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=p_objRecordContent.m_dtmCREATERECORDDATE;
				objDPArr[4].Value=p_objRecordContent.m_strCREATERECORDUSERID;
				objDPArr[5].Value=p_objRecordContent.m_strMODIFYRECORDUSERID;
				objDPArr[6].Value=p_objRecordContent.m_strRECORDCONTENTAll;
                objDPArr[7].Value = p_objRecordContent.m_strRECORDCONTENTXML;
                objDPArr[8].DbType = DbType.DateTime;
				objDPArr[8].Value=p_objRecordContent.m_dtmRECORDDATE;
                objDPArr[9].Value = p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[10].DbType = DbType.DateTime;
				objDPArr[10].Value = p_objRecordContent.m_dtmMODIFYDATE;
				objDPArr[11].Value = p_objRecordContent.m_intClass;
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    //p_objHRPServ.Dispose();
                }
			return lngRes;
		}

		/// <summary>
		/// �޸Ĳ����¼����
		/// </summary>
		/// <param name="p_objRecordContent">������Ϣ</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyDetail(clsGeneralNurseRecordContent_GXDetail p_objRecordContent)
		{
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				//������                              
				if(p_objRecordContent==null || p_objRecordContent.m_strINPATIENTID==null)
					return (long)enmOperationResult.Parameter_Error;
				string strSQL=@"update generalnurserecord_gxdetail set opendate=?,
							modifyuserid=?,modifydate=?,recordcontent=?,recordcontentxml=?,recordcontent_right=?
							where inpatientid=? and inpatientdate=? and recorddate=?";

				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(9, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_objRecordContent.m_dtmOPENDATE;
                objDPArr[1].Value = p_objRecordContent.m_strMODIFYRECORDUSERID;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmMODIFYDATE;
				objDPArr[3].Value=p_objRecordContent.m_strRECORDCONTENTAll;
				objDPArr[4].Value=p_objRecordContent.m_strRECORDCONTENTXML;
				objDPArr[5].Value=p_objRecordContent.m_strRECORDCONTENT_RIGHT;
                objDPArr[6].Value = p_objRecordContent.m_strINPATIENTID;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = p_objRecordContent.m_dtmINPATIENTDATE;
                objDPArr[8].DbType = DbType.DateTime;
				objDPArr[8].Value=p_objRecordContent.m_dtmRECORDDATE;//ע��˴��Ĵ���ʱ��Ϊ���̼�¼���ݵĴ���ʱ��
				
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		///  ��ȡָ�������¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">סԺ����</param>
		/// <param name="p_strRecordDate">��������</param>
		/// <param name="p_objRecordContent">���̼�¼����</param>
		/// <returns>����ֵ</returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,string p_strInPatientDate,string p_strRecordDate,
			out clsGeneralNurseRecordContent_GXDetail p_objRecordContent)
		{
			long lngRes=0;
			p_objRecordContent=null;
            clsHRPTableService p_objHRPServ=new clsHRPTableService();
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
       t.status,
       t.recordcontent_right,
       t.recorddate,
       t.modifydate,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.class,
       f_getempnamebyno(t.createuserid) as lastname_vchr
  from generalnurserecord_gxdetail t
 where inpatientid = ?
   and t.inpatientdate = ?
   and t.status = 0
   and t.recorddate = ?";
				
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strRecordDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					p_objRecordContent=new clsGeneralNurseRecordContent_GXDetail();
					p_objRecordContent.m_dtmCREATERECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["CreateDate"]);
					p_objRecordContent.m_strCREATERECORDUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
					p_objRecordContent.m_strDetailCreateUserName = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
					p_objRecordContent.m_strRECORDCONTENTAll = dtbValue.Rows[0]["RecordContent"].ToString();
					p_objRecordContent.m_strRECORDCONTENTXML = dtbValue.Rows[0]["RecordContentXML"].ToString();
					p_objRecordContent.m_strRECORDCONTENT_RIGHT = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
					p_objRecordContent.m_dtmMODIFYDATE = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
					p_objRecordContent.m_strMODIFYRECORDUSERID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					p_objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
					p_objRecordContent.m_intClass = Convert.ToInt32(dtbValue.Rows[0]["CLASS"]);
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

               //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		/// ɾ��ָ�������¼����
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordDate"></param>
		/// <param name="p_strDelDate"></param>
		/// <param name="p_strDelID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteDetail(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordDate,
			string p_strDelDate, 
			string p_strDelID)
		{
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@" update generalnurserecord_gxdetail set status=1,deactiveddate=?,deactivedoperatorid=?
								where inpatientid=? and  inpatientdate=? and  recorddate=?";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=DateTime.Parse(p_strDelDate);
				objDPArr[1].Value=p_strDelID;
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=DateTime.Parse(p_strRecordDate);
				//ִ�в�ѯ 
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		///  ��ȡָ�����˵Ĳ����¼����
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
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
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
       t.status,
       t.recordcontent_right,
       t.recorddate,
       t.modifydate,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.class,
       f_getempnamebyno(t.createuserid) as lastname_vchr
  from generalnurserecord_gxdetail t
 where t.status = 0
   and t.inpatientid = ?
   and t.inpatientdate = ?";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 )
				{
					p_strRecordContentArr=new string[dtbValue.Rows.Count][];
					for (int i = 0; i < dtbValue.Rows.Count; i++)
					{
						p_strRecordContentArr[i] = new string[6];
						p_strRecordContentArr[i][0] = dtbValue.Rows[0]["RecordContent"].ToString();
						p_strRecordContentArr[i][1] = dtbValue.Rows[0]["RecordContentXML"].ToString();
						p_strRecordContentArr[i][2] = dtbValue.Rows[0]["RECORDDATE"].ToString();
						p_strRecordContentArr[i][3] = dtbValue.Rows[0]["CREATEUSERID"].ToString();
						p_strRecordContentArr[i][4] = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
						p_strRecordContentArr[i][5] = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
					}
				}
			}
			catch(Exception objEx)
			{
		

				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		///  ��ȡָ�����˵���ɾ�������¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">סԺ����</param>
		/// <param name="p_strOpenDate">�״δ�������</param>
		/// <param name="p_objRecordContent">���̼�¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDelRecordContentWithInpatient(string p_strInPatientID,string p_strInPatientDate,
			string p_strOpenDate,
			out clsGeneralNurseRecordContent_GXDetail p_objRecordContent)
		{
			long lngRes=0;
			p_objRecordContent=null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
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
       t.status,
       t.recordcontent_right,
       t.recorddate,
       t.modifydate,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.class,
       F_GETEMPNAMEBYNO(t.createuserid) as LASTNAME_VCHR
  from GENERALNURSERECORD_GXDETAIL t
 where t.status = 1
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.OPENDATE = ?";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 )
				{
					p_objRecordContent=new clsGeneralNurseRecordContent_GXDetail();
					for (int i = 0; i < dtbValue.Rows.Count; i++)
					{
						p_objRecordContent.m_strRECORDCONTENT_RIGHT = dtbValue.Rows[0]["RECORDCONTENT_RIGHT"].ToString();
						p_objRecordContent.m_strRECORDCONTENTXML = dtbValue.Rows[0]["RecordContentXML"].ToString();
						p_objRecordContent.m_dtmCREATERECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
						p_objRecordContent.m_strCREATERECORDUSERID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
						p_objRecordContent.m_strDetailCreateUserName = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
						p_objRecordContent.m_strRECORDCONTENTAll = dtbValue.Rows[0]["RECORDCONTENT"].ToString();
						p_objRecordContent.m_intClass = Convert.ToInt32(dtbValue.Rows[0]["CLASS"]);
					}
				}
			}
			catch(Exception objEx)
			{
		

				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}
	}
}