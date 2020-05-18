
using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// �߲�������ι۲��(��ӣ��޸�)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsHurryVeinRecord_ContentService : clsDiseaseTrackService
	{
		#region constructor
		public clsHurryVeinRecord_ContentService()
		{

		}
		#endregion

		#region SQL���
		/// <summary>
		/// ��ICUACAD_HURRYVEINRECORD��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from icuacad_hurryveinrecord
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// ��ICUACAD_HURRYVEINRECORD�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from icuacad_hurryveinrecord
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// ��ICUACAD_HURRYVEINRECORD��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from icuacad_hurryveinrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// ��Ӽ�¼��ICUACAD_HURRYVEINRECORD  (51col)
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into icuacad_hurryveinrecord (inpatientid,inpatientdate,opendate,
						createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,
											chroma_chr, 
											chroma_chrxml, 
											dropcount_chr, 
											dropcount_chrxml, 
											palaceshrink_chr, 
											palaceshrink_chrxml, 
											embryoheart_chr, 
											embryoheart_chrxml, 
											expand_chr, 
											expand_chrxml, 
											presentation_chr, 
											presentation_chrxml, 
											bloodpressure_chr, 
											bloodpressure_chrxml, 
											specialrecord_chr, 
											specialrecord_chrxml, 
											signature_chr, 
											signature_chrxml, 
											laycount_chr, 
											laycount_chrxml, 
											pregnantweek_chr, 
											pregnantweek_chrxml, 
											scorecount_chr, 
											scorecount_chrxml, 
											rdbneckexpand_chr, 
											rdbneckexpand_chrxml, 
											rdbneckshink_chr, 
											rdbneckshink_chrxml, 
											rdbhighlow_chr, 
											rdbhighlow_chrxml, 
											rdbneckhard_chr, 
											rdbneckhard_chrxml, 
											droppingcase_chr, 
											droppingcase_chrxml, 
											indicate_chr, 
											indicate_chrxml, 
											usecount_chr, 
											usecount_chrxml, 
											layway_chr, 
											layway_chrxml,
											rdbnecklocation_chr, 
											rdbnecklocation_chrxml
											) 
						values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
								
								
		/// <summary>
		/// ��Ӽ�¼��ICUACAD_HURRYVEINCONTENT  (14col)
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into icuacad_hurryveincontent (inpatientid,inpatientdate,
						opendate,modifydate,modifyuserid,
											chroma_chr_right, 
											dropcount_chr_right, 
											palaceshrink_chr_right, 
											embryoheart_chr_right, 
											expand_chr_right, 
											presentation_chr_right, 
											bloodpressure_chr_right, 
											specialrecord_chr_right, 
											signature_chr_right
											) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��ICUACAD_HURRYVEINRECORD	(21��?)
		/// </summary>
		private const string c_strModifyRecordSQL= @"update icuacad_hurryveinrecord 
													set 
											chroma_chr=?, 
											chroma_chrxml=?, 
											dropcount_chr=?, 
											dropcount_chrxml=?, 
											palaceshrink_chr=?, 
											palaceshrink_chrxml=?, 
											embryoheart_chr=?, 
											embryoheart_chrxml=?, 
											expand_chr=?, 
											expand_chrxml=?, 
											presentation_chr=?, 
											presentation_chrxml=?, 
											bloodpressure_chr=?, 
											bloodpressure_chrxml=?, 
											specialrecord_chr=?, 
											specialrecord_chrxml=?, 
											signature_chr=?, 
											signature_chrxml=?
										
								where inpatientid=? and inpatientdate=? and status=0 and createdate=?";

		/// <summary>
		/// �޸ļ�¼��ICUACAD_HURRYVEINCONTENT
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����ICUACAD_HURRYVEINRECORD��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update icuacad_hurryveinrecord
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// ����ICUACAD_HURRYVEINRECORD��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update icuacad_hurryveinrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// ����ICUACAD_HURRYVEINRECORD��FirstPrintDate
		/// </summary>
		private const string c_strUpdateALLFirstPrintDateSQL= @"update icuacad_hurryveinrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?																
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// ��ICUACAD_HURRYVEINRECORD��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from icuacad_hurryveinrecord
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// ��ICUACAD_HURRYVEINRECORD��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from icuacad_hurryveinrecord
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";



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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHurryVeinRecord_ContentService","m_lngGetRecordTimeList");
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHurryVeinRecord_ContentService","m_lngUpdateFirstPrintDate");
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHurryVeinRecord_ContentService","m_lngGetDeleteRecordTimeList");
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
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL,ref dtbValue,objDPArr);
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHurryVeinRecord_ContentService","m_lngGetDeleteRecordTimeListAll");
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
			
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			//,t2.MODIFYDATE as MODIFYDATE,t2.MODIFYUSERID as MODIFYUSERID
			string c_strGetRecordContentSQL= clsDatabaseSQLConvert.s_StrTop1+@" t1.*,t2.modifydate,t2.modifyuserid, 
													t2.chroma_chr_right, 
													t2.dropcount_chr_right, 
													t2.palaceshrink_chr_right, 
													t2.embryoheart_chr_right, 
													t2.expand_chr_right, 
													t2.presentation_chr_right, 
													t2.bloodpressure_chr_right, 
													t2.specialrecord_chr_right, 
													t2.signature_chr_right

								  from icuacad_hurryveinrecord t1 join icuacad_hurryveincontent  t2
									on( t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
									and t1.opendate = t2.opendate and t1.status =0
									and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ?) order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
		
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
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					#region ���ý��
					clsHurryVeinRecord  objRecordContent=new clsHurryVeinRecord ();
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

							
					objRecordContent.m_strCHROMA_CHR=dtbValue.Rows[0]["CHROMA_CHR"].ToString();
					objRecordContent.m_strCHROMA_CHR_RIGHT=dtbValue.Rows[0]["CHROMA_CHR_RIGHT"].ToString();
					objRecordContent.m_strCHROMA_CHRXML=dtbValue.Rows[0]["CHROMA_CHRXML"].ToString();
							
					objRecordContent.m_strDROPCOUNT_CHR=dtbValue.Rows[0]["DROPCOUNT_CHR"].ToString();
					objRecordContent.m_strDROPCOUNT_CHR_RIGHT=dtbValue.Rows[0]["DROPCOUNT_CHR_RIGHT"].ToString();
					objRecordContent.m_strDROPCOUNT_CHRXML=dtbValue.Rows[0]["DROPCOUNT_CHRXML"].ToString();

					objRecordContent.m_strPALACESHRINK_CHR=dtbValue.Rows[0]["PALACESHRINK_CHR"].ToString();
					objRecordContent.m_strPALACESHRINK_CHR_RIGHT=dtbValue.Rows[0]["PALACESHRINK_CHR_RIGHT"].ToString();
					objRecordContent.m_strPALACESHRINK_CHRXML=dtbValue.Rows[0]["PALACESHRINK_CHRXML"].ToString();

					objRecordContent.m_strEMBRYOHEART_CHR=dtbValue.Rows[0]["EMBRYOHEART_CHR"].ToString();
					objRecordContent.m_strEMBRYOHEART_CHR_RIGHT=dtbValue.Rows[0]["EMBRYOHEART_CHR_RIGHT"].ToString();
					objRecordContent.m_strEMBRYOHEART_CHRXML=dtbValue.Rows[0]["EMBRYOHEART_CHRXML"].ToString();

					objRecordContent.m_strEXPAND_CHR_RIGHT=dtbValue.Rows[0]["EXPAND_CHR_RIGHT"].ToString();
					objRecordContent.m_strEXPAND_CHR=dtbValue.Rows[0]["EXPAND_CHR"].ToString();
					objRecordContent.m_strEXPAND_CHRXML=dtbValue.Rows[0]["EXPAND_CHRXML"].ToString();

					objRecordContent.m_strPRESENTATION_CHR=dtbValue.Rows[0]["PRESENTATION_CHR"].ToString();
					objRecordContent.m_strPRESENTATION_CHR_RIGHT=dtbValue.Rows[0]["PRESENTATION_CHR_RIGHT"].ToString();
					objRecordContent.m_strPRESENTATION_CHRXML=dtbValue.Rows[0]["PRESENTATION_CHRXML"].ToString();


					objRecordContent.m_strBLOODPRESSURE_CHR=dtbValue.Rows[0]["BLOODPRESSURE_CHR"].ToString();
					objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT=dtbValue.Rows[0]["BLOODPRESSURE_CHR_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSURE_CHRXML=dtbValue.Rows[0]["BLOODPRESSURE_CHRXML"].ToString();

					objRecordContent.m_strSPECIALRECORD_CHR=dtbValue.Rows[0]["SPECIALRECORD_CHR"].ToString();
					objRecordContent.m_strSPECIALRECORD_CHR_RIGHT=dtbValue.Rows[0]["SPECIALRECORD_CHR_RIGHT"].ToString();
					objRecordContent.m_strSPECIALRECORD_CHRXML=dtbValue.Rows[0]["SPECIALRECORD_CHRXML"].ToString();

					objRecordContent.m_strSIGNATURE_CHR=dtbValue.Rows[0]["SIGNATURE_CHR"].ToString();
					objRecordContent.m_strSIGNATURE_CHR_RIGHT=dtbValue.Rows[0]["SIGNATURE_CHR_RIGHT"].ToString();
					objRecordContent.m_strSIGNATURE_CHRXML=dtbValue.Rows[0]["SIGNATURE_CHRXML"].ToString();

	

					objRecordContent.m_strLAYCOUNT_CHR =dtbValue.Rows[0]["LAYCOUNT_CHR"].ToString();							
					objRecordContent.m_strLAYCOUNT_CHRXML =dtbValue.Rows[0]["LAYCOUNT_CHRXML"].ToString();

					objRecordContent.m_strPREGNANTWEEK_CHR =dtbValue.Rows[0]["PREGNANTWEEK_CHR"].ToString();							
					objRecordContent.m_strPREGNANTWEEK_CHRXML =dtbValue.Rows[0]["PREGNANTWEEK_CHRXML"].ToString();

					objRecordContent.m_strSCORECOUNT_CHR =dtbValue.Rows[0]["SCORECOUNT_CHR"].ToString();							
					objRecordContent.m_strSCORECOUNT_CHRXML =dtbValue.Rows[0]["SCORECOUNT_CHRXML"].ToString();

					objRecordContent.m_strRDBNECKEXPAND_CHR =dtbValue.Rows[0]["RDBNECKEXPAND_CHR"].ToString();							
					objRecordContent.m_strRDBNECKEXPAND_CHRXML =dtbValue.Rows[0]["RDBNECKEXPAND_CHRXML"].ToString();

					objRecordContent.m_strRDBNECKSHINK_CHR =dtbValue.Rows[0]["RDBNECKSHINK_CHR"].ToString();							
					objRecordContent.m_strRDBNECKSHINK_CHRXML =dtbValue.Rows[0]["RDBNECKSHINK_CHRXML"].ToString();

					objRecordContent.m_strRDBHIGHLOW_CHR =dtbValue.Rows[0]["RDBHIGHLOW_CHR"].ToString();							
					objRecordContent.m_strRDBHIGHLOW_CHRXML =dtbValue.Rows[0]["RDBHIGHLOW_CHRXML"].ToString();
							

					objRecordContent.m_strRDBNECKHARD_CHR =dtbValue.Rows[0]["RDBNECKHARD_CHR"].ToString();							
					objRecordContent.m_strRDBNECKHARD_CHRXML =dtbValue.Rows[0]["RDBNECKHARD_CHRXML"].ToString();

							
					objRecordContent.m_strDROPPINGCASE_CHR =dtbValue.Rows[0]["DROPPINGCASE_CHR"].ToString();							
					objRecordContent.m_strDROPPINGCASE_CHRXML =dtbValue.Rows[0]["DROPPINGCASE_CHRXML"].ToString();

							
					objRecordContent.m_strINDICATE_CHR =dtbValue.Rows[0]["INDICATE_CHR"].ToString();							
					objRecordContent.m_strINDICATE_CHRXML =dtbValue.Rows[0]["INDICATE_CHRXML"].ToString();

					objRecordContent.m_strUSECOUNT_CHR =dtbValue.Rows[0]["USECOUNT_CHR"].ToString();							
					objRecordContent.m_strUSECOUNT_CHRXML =dtbValue.Rows[0]["USECOUNT_CHRXML"].ToString();

							
					objRecordContent.m_strLAYWAY_CHR =dtbValue.Rows[0]["LAYWAY_CHR"].ToString();							
					objRecordContent.m_strLAYWAY_CHRXML =dtbValue.Rows[0]["LAYWAY_CHRXML"].ToString();

					objRecordContent.m_strRDBNECKLOCATION_CHR =dtbValue.Rows[0]["RDBNECKLOCATION_CHR"].ToString();							
					objRecordContent.m_strRDBNECKLOCATION_CHRXML =dtbValue.Rows[0]["RDBNECKLOCATION_CHRXML"].ToString();

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
		#endregion 

		#region �����¼�����ݿ⡣�������,����ӱ�.
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
			clsHurryVeinRecord  objRecordContent = (clsHurryVeinRecord )p_objRecordContent;
			long lngRes = 0;
			try
			{
				#region ��ֵ
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(51,out objDPArr);
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



								
				objDPArr[9].Value=objRecordContent.m_strCHROMA_CHR;
				objDPArr[10].Value=objRecordContent.m_strCHROMA_CHRXML;

				objDPArr[11].Value=objRecordContent.m_strDROPCOUNT_CHR;
				objDPArr[12].Value=objRecordContent.m_strDROPCOUNT_CHRXML;
				objDPArr[13].Value=objRecordContent.m_strPALACESHRINK_CHR;
				objDPArr[14].Value=objRecordContent.m_strPALACESHRINK_CHRXML;
				objDPArr[15].Value=objRecordContent.m_strEMBRYOHEART_CHR;
				objDPArr[16].Value=objRecordContent.m_strEMBRYOHEART_CHRXML;
				objDPArr[17].Value=objRecordContent.m_strEXPAND_CHR;
				objDPArr[18].Value=objRecordContent.m_strEXPAND_CHRXML;
				objDPArr[19].Value=objRecordContent.m_strPRESENTATION_CHR;
				objDPArr[20].Value=objRecordContent.m_strPRESENTATION_CHRXML;

				objDPArr[21].Value=objRecordContent.m_strBLOODPRESSURE_CHR;
				objDPArr[22].Value=objRecordContent.m_strBLOODPRESSURE_CHRXML;
				objDPArr[23].Value=objRecordContent.m_strSPECIALRECORD_CHR;
				objDPArr[24].Value=objRecordContent.m_strSPECIALRECORD_CHRXML;

				objDPArr[25].Value=objRecordContent.m_strSIGNATURE_CHR;
				objDPArr[26].Value=objRecordContent.m_strSIGNATURE_CHRXML;
				objDPArr[27].Value=objRecordContent.m_strLAYCOUNT_CHR;
				objDPArr[28].Value=objRecordContent.m_strLAYCOUNT_CHRXML;

				objDPArr[29].Value=objRecordContent.m_strPREGNANTWEEK_CHR;
				objDPArr[30].Value=objRecordContent.m_strPREGNANTWEEK_CHRXML;
				objDPArr[31].Value=objRecordContent.m_strSCORECOUNT_CHR;
				objDPArr[32].Value=objRecordContent.m_strSCORECOUNT_CHRXML;
				objDPArr[33].Value=objRecordContent.m_strRDBNECKEXPAND_CHR;
				objDPArr[34].Value=objRecordContent.m_strRDBNECKEXPAND_CHRXML;

				objDPArr[35].Value=objRecordContent.m_strRDBNECKSHINK_CHR;
				objDPArr[36].Value=objRecordContent.m_strRDBNECKSHINK_CHRXML;		

				objDPArr[37].Value=objRecordContent.m_strRDBHIGHLOW_CHR;
				objDPArr[38].Value=objRecordContent.m_strRDBHIGHLOW_CHRXML;
				
				objDPArr[39].Value=objRecordContent.m_strRDBNECKHARD_CHR;
				objDPArr[40].Value=objRecordContent.m_strRDBNECKHARD_CHRXML;
				objDPArr[41].Value=objRecordContent.m_strDROPPINGCASE_CHR;
				objDPArr[42].Value=objRecordContent.m_strDROPPINGCASE_CHRXML;
				objDPArr[43].Value=objRecordContent.m_strINDICATE_CHR;
				objDPArr[44].Value=objRecordContent.m_strINDICATE_CHRXML;

				objDPArr[45].Value=objRecordContent.m_strUSECOUNT_CHRXML;
				objDPArr[46].Value=objRecordContent.m_strUSECOUNT_CHR;

				objDPArr[47].Value=objRecordContent.m_strLAYWAY_CHR;
				objDPArr[48].Value=objRecordContent.m_strLAYWAY_CHRXML;

				objDPArr[49].Value=objRecordContent.m_strRDBNECKLOCATION_CHR;
				objDPArr[50].Value=objRecordContent.m_strRDBNECKLOCATION_CHRXML;
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(14,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;	
			
				objDPArr2[5].Value = objRecordContent.m_strCHROMA_CHR_RIGHT;

				objDPArr2[6].Value = objRecordContent.m_strDROPCOUNT_CHR_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strPALACESHRINK_CHR_RIGHT;

				objDPArr2[8].Value = objRecordContent.m_strEMBRYOHEART_CHR_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strEXPAND_CHR_RIGHT;

				objDPArr2[10].Value = objRecordContent.m_strPRESENTATION_CHR_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT;

				objDPArr2[12].Value = objRecordContent.m_strSPECIALRECORD_CHR_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strSIGNATURE_CHR_RIGHT;
				
				#endregion 
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
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			clsHurryVeinRecord  objRecordContent = (clsHurryVeinRecord )p_objRecordContent;
			/// <summary>
			/// ��ȡָ����������޸�ʱ�䡣
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" T2.ModifyDate,T2.ModifyUserID FROM ICUACAD_HURRYVEINRECORD T1,ICUACAD_HURRYVEINCONTENT T2
			WHERE T1.InPatientID = T2.InPatientID AND T1.InPatientDate = T2.InPatientDate
			AND T1.OpenDate = T2.OpenDate AND T1.Status =0
			AND T1.InPatientID = ? AND T1.InPatientDate = ? AND T1.OpenDate = ? ORDER BY T2.ModifyDate DESC"+clsDatabaseSQLConvert.s_StrRownum;
		
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
		#endregion

		#region �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�.
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
			if(p_objRecordContent==null || p_objRecordContent.m_dtmCreateDate.ToString()==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			clsHurryVeinRecord  objRecordContent = (clsHurryVeinRecord )p_objRecordContent;
			long lngRes = 0;
			try
			{
				#region set value
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(21,out objDPArr);
			
				objDPArr[0].Value=objRecordContent.m_strCHROMA_CHR;
				objDPArr[1].Value=objRecordContent.m_strCHROMA_CHRXML;
				objDPArr[2].Value=objRecordContent.m_strDROPCOUNT_CHR;
				objDPArr[3].Value=objRecordContent.m_strDROPCOUNT_CHRXML;
				objDPArr[4].Value=objRecordContent.m_strPALACESHRINK_CHR;
				objDPArr[5].Value=objRecordContent.m_strPALACESHRINK_CHRXML;

				objDPArr[6].Value=objRecordContent.m_strEMBRYOHEART_CHR;
				objDPArr[7].Value=objRecordContent.m_strEMBRYOHEART_CHRXML;
				objDPArr[8].Value=objRecordContent.m_strEXPAND_CHR;
				objDPArr[9].Value=objRecordContent.m_strEXPAND_CHRXML;
				objDPArr[10].Value=objRecordContent.m_strPRESENTATION_CHR;
				objDPArr[11].Value=objRecordContent.m_strPRESENTATION_CHRXML;

				objDPArr[12].Value=objRecordContent.m_strBLOODPRESSURE_CHR;
				objDPArr[13].Value=objRecordContent.m_strBLOODPRESSURE_CHRXML;
				objDPArr[14].Value=objRecordContent.m_strSPECIALRECORD_CHR;
				objDPArr[15].Value=objRecordContent.m_strSPECIALRECORD_CHRXML;


				objDPArr[16].Value=objRecordContent.m_strSIGNATURE_CHR;
				objDPArr[17].Value=objRecordContent.m_strSIGNATURE_CHRXML;

                objDPArr[18].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[19].DbType = DbType.DateTime;
                objDPArr[19].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[20].DbType = DbType.DateTime;
				objDPArr[20].Value=p_objRecordContent.m_dtmCreateDate;
			
				#endregion 
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				#region set value
				IDataParameter[] objDPArr2 = null; 
				p_objHRPServ.CreateDatabaseParameter(14,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;	
			
				objDPArr2[5].Value = objRecordContent.m_strCHROMA_CHR_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strDROPCOUNT_CHR_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strPALACESHRINK_CHR_RIGHT;

				objDPArr2[8].Value = objRecordContent.m_strEMBRYOHEART_CHR_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strEXPAND_CHR_RIGHT;

				objDPArr2[10].Value = objRecordContent.m_strPRESENTATION_CHR_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT;

				objDPArr2[12].Value = objRecordContent.m_strSPECIALRECORD_CHR_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strSIGNATURE_CHR_RIGHT;

				#endregion 
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
		#endregion 

		#region �Ѽ�¼�������С�ɾ������
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

			clsHurryVeinRecord  objRecordContent = (clsHurryVeinRecord )p_objRecordContent;
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
		#endregion

		#region  ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
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
			///ICUACAD_HURRYVEINRECORD��ȡLastModifyDate��FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.FirstPrintDate,b.ModifyDate from ICUACAD_HURRYVEINRECORD a,
					ICUACAD_HURRYVEINCONTENT b where InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and 
					a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate 
					ORDER BY b.ModifyDate DESC"+clsDatabaseSQLConvert.s_StrRownum;


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
		#endregion 

		#region ��ȡָ���Ѿ���ɾ����¼������(������ʾ��DG�����)
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
		
			string c_strGetDeleteRecordContentSQL= clsDatabaseSQLConvert.s_StrTop1+@" t1.*,t2.MODIFYDATE as MODIFYDATE,t2.MODIFYUSERID as MODIFYUSERID  FROM ICUACAD_HURRYVEINRECORD T1,ICUACAD_HURRYVEINCONTENT T2
									WHERE T1.InPatientID = T2.InPatientID AND T1.InPatientDate = T2.InPatientDate
									AND T1.OpenDate = T2.OpenDate AND 
									T1.Status =1
									AND T1.InPatientID = ? AND T1.InPatientDate = ? AND T1.OpenDate = ? ORDER BY T2.ModifyDate DESC"+clsDatabaseSQLConvert.s_StrRownum;
		
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
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					#region ���ý��
					clsHurryVeinRecord  objRecordContent=new clsHurryVeinRecord ();
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


							
					objRecordContent.m_strCHROMA_CHR=dtbValue.Rows[0]["CHROMA_CHR"].ToString();
					objRecordContent.m_strCHROMA_CHR_RIGHT=dtbValue.Rows[0]["CHROMA_CHR_RIGHT"].ToString();
					objRecordContent.m_strCHROMA_CHRXML=dtbValue.Rows[0]["CHROMA_CHRXML"].ToString();
							
					objRecordContent.m_strDROPCOUNT_CHR=dtbValue.Rows[0]["DROPCOUNT_CHR"].ToString();
					objRecordContent.m_strDROPCOUNT_CHR_RIGHT=dtbValue.Rows[0]["DROPCOUNT_CHR_RIGHT"].ToString();
					objRecordContent.m_strDROPCOUNT_CHRXML=dtbValue.Rows[0]["DROPCOUNT_CHRXML"].ToString();

					objRecordContent.m_strPALACESHRINK_CHR=dtbValue.Rows[0]["PALACESHRINK_CHR"].ToString();
					objRecordContent.m_strPALACESHRINK_CHR_RIGHT=dtbValue.Rows[0]["PALACESHRINK_CHR_RIGHT"].ToString();
					objRecordContent.m_strPALACESHRINK_CHRXML=dtbValue.Rows[0]["PALACESHRINK_CHRXML"].ToString();

					objRecordContent.m_strEMBRYOHEART_CHR=dtbValue.Rows[0]["EMBRYOHEART_CHR"].ToString();
					objRecordContent.m_strEMBRYOHEART_CHR_RIGHT=dtbValue.Rows[0]["EMBRYOHEART_CHR_RIGHT"].ToString();
					objRecordContent.m_strEMBRYOHEART_CHRXML=dtbValue.Rows[0]["EMBRYOHEART_CHRXML"].ToString();

					objRecordContent.m_strEXPAND_CHR_RIGHT=dtbValue.Rows[0]["EXPAND_CHR_RIGHT"].ToString();
					objRecordContent.m_strEXPAND_CHR=dtbValue.Rows[0]["EXPAND_CHR"].ToString();
					objRecordContent.m_strEXPAND_CHRXML=dtbValue.Rows[0]["EXPAND_CHRXML"].ToString();

					objRecordContent.m_strPRESENTATION_CHR=dtbValue.Rows[0]["PRESENTATION_CHR"].ToString();
					objRecordContent.m_strPRESENTATION_CHR_RIGHT=dtbValue.Rows[0]["PRESENTATION_CHR_RIGHT"].ToString();
					objRecordContent.m_strPRESENTATION_CHRXML=dtbValue.Rows[0]["PRESENTATION_CHRXML"].ToString();


					objRecordContent.m_strBLOODPRESSURE_CHR=dtbValue.Rows[0]["BLOODPRESSURE_CHR"].ToString();
					objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT=dtbValue.Rows[0]["BLOODPRESSURE_CHR_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSURE_CHRXML=dtbValue.Rows[0]["BLOODPRESSURE_CHRXML"].ToString();

					objRecordContent.m_strSPECIALRECORD_CHR=dtbValue.Rows[0]["SPECIALRECORD_CHR"].ToString();
					objRecordContent.m_strSPECIALRECORD_CHR_RIGHT=dtbValue.Rows[0]["SPECIALRECORD_CHR_RIGHT"].ToString();
					objRecordContent.m_strSPECIALRECORD_CHRXML=dtbValue.Rows[0]["SPECIALRECORD_CHRXML"].ToString();

					objRecordContent.m_strSIGNATURE_CHR=dtbValue.Rows[0]["SIGNATURE_CHR"].ToString();
					objRecordContent.m_strSIGNATURE_CHR_RIGHT=dtbValue.Rows[0]["SIGNATURE_CHR_RIGHT"].ToString();
					objRecordContent.m_strSIGNATURE_CHRXML=dtbValue.Rows[0]["SIGNATURE_CHRXML"].ToString();

	

					objRecordContent.m_strLAYCOUNT_CHR =dtbValue.Rows[0]["LAYCOUNT_CHR"].ToString();							
					objRecordContent.m_strLAYCOUNT_CHRXML =dtbValue.Rows[0]["LAYCOUNT_CHRXML"].ToString();

					objRecordContent.m_strPREGNANTWEEK_CHR =dtbValue.Rows[0]["PREGNANTWEEK_CHR"].ToString();							
					objRecordContent.m_strPREGNANTWEEK_CHRXML =dtbValue.Rows[0]["PREGNANTWEEK_CHRXML"].ToString();

					objRecordContent.m_strSCORECOUNT_CHR =dtbValue.Rows[0]["SCORECOUNT_CHR"].ToString();							
					objRecordContent.m_strSCORECOUNT_CHRXML =dtbValue.Rows[0]["SCORECOUNT_CHRXML"].ToString();

					objRecordContent.m_strRDBNECKEXPAND_CHR =dtbValue.Rows[0]["RDBNECKEXPAND_CHR"].ToString();							
					objRecordContent.m_strRDBNECKEXPAND_CHRXML =dtbValue.Rows[0]["RDBNECKEXPAND_CHRXML"].ToString();

					objRecordContent.m_strRDBNECKSHINK_CHR =dtbValue.Rows[0]["RDBNECKSHINK_CHR"].ToString();							
					objRecordContent.m_strRDBNECKSHINK_CHRXML =dtbValue.Rows[0]["RDBNECKSHINK_CHRXML"].ToString();

					objRecordContent.m_strRDBHIGHLOW_CHR =dtbValue.Rows[0]["RDBHIGHLOW_CHR"].ToString();							
					objRecordContent.m_strRDBHIGHLOW_CHRXML =dtbValue.Rows[0]["RDBHIGHLOW_CHRXML"].ToString();
							

					objRecordContent.m_strRDBNECKHARD_CHR =dtbValue.Rows[0]["RDBNECKHARD_CHR"].ToString();							
					objRecordContent.m_strRDBNECKHARD_CHRXML =dtbValue.Rows[0]["RDBNECKHARD_CHRXML"].ToString();

					objRecordContent.m_strRDBNECKLOCATION_CHR =dtbValue.Rows[0]["RDBNECKLOCATION_CHR"].ToString();							
					objRecordContent.m_strRDBNECKLOCATION_CHRXML =dtbValue.Rows[0]["RDBNECKLOCATION_CHRXML"].ToString();

							
					objRecordContent.m_strDROPPINGCASE_CHR =dtbValue.Rows[0]["DROPPINGCASE_CHR"].ToString();							
					objRecordContent.m_strDROPPINGCASE_CHRXML =dtbValue.Rows[0]["DROPPINGCASE_CHRXML"].ToString();

							
					objRecordContent.m_strINDICATE_CHR =dtbValue.Rows[0]["INDICATE_CHR"].ToString();							
					objRecordContent.m_strINDICATE_CHRXML =dtbValue.Rows[0]["INDICATE_CHRXML"].ToString();

					objRecordContent.m_strUSECOUNT_CHR =dtbValue.Rows[0]["USECOUNT_CHR"].ToString();							
					objRecordContent.m_strUSECOUNT_CHRXML =dtbValue.Rows[0]["USECOUNT_CHRXML"].ToString();

							
					objRecordContent.m_strLAYWAY_CHR =dtbValue.Rows[0]["LAYWAY_CHR"].ToString();							
					objRecordContent.m_strLAYWAY_CHRXML =dtbValue.Rows[0]["LAYWAY_CHRXML"].ToString();


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
		#endregion 

		#region �������и��²���,����,��...
		/// <summary>
		///�������и��²���,����,��...
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <returns></returns>
		[AutoComplete]
		public   long m_lngGetUpdateOther(
										string p_strInPatientID,
										string p_strInPatientDate,
										string p_strlaycount_chr,
										string p_strPregnantweek_chr,
										string p_strScorecount_chr,
										string p_strRdbneckexpand_chr,
										string p_strRdbneckshink_chr,
										string p_strRdbhighlow_chr,
										string p_strRdbneckhard_chr,
										string p_strDroppingcase_chr,
										string p_strIndicate_chr,
										string p_strUsecount_chr,
										string p_strLayway_chr,
										string p_strRdbnecklocation_chr
										)
		{			
			

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;

			
				clsHRPTableService p_objHRPServ = new clsHRPTableService();
			try
			{

				#region ���һ����¼��û��,�����һ��state=1�ļ�¼,�Ա�����ε��ֶ�.
				string strSel = "select count(InPatientID) from ICUACAD_HURRYVEINRECORD";
				string strInsert = "insert into ICUACAD_HURRYVEINRECORD(INPATIENTID,INPATIENTDATE,OPENDATE,CREATEDATE,STATUS) values(?,?,?,?,0)";

				DataTable dbResult = null;
				lngRes = p_objHRPServ.DoGetDataTable(strSel, ref dbResult);
				if(lngRes>0)
				{
					if(dbResult != null)
					{
						if(dbResult.Rows.Count> 0)
						{
							if(dbResult.Rows[0][0].ToString() == "0")
							{
								IDataParameter[] objDPArr4 = null;
								p_objHRPServ.CreateDatabaseParameter(4,out objDPArr4);
                                objDPArr4[0].Value = p_strInPatientID;
                                objDPArr4[1].DbType = DbType.DateTime;
                                objDPArr4[1].Value = Convert.ToDateTime(p_strInPatientDate);
                                objDPArr4[2].DbType = DbType.DateTime;
                                objDPArr4[2].Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                objDPArr4[3].DbType = DbType.DateTime;
								objDPArr4[3].Value=Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
								lngRes =  p_objHRPServ.lngExecuteParameterSQL(strInsert,ref lngRes,objDPArr4);
							}
						}
					}
				}
				else
				{
					return lngRes;
				}
				
				#endregion

				#region update 
				string c_strUpdateLaycountSQL= @"Update ICUACAD_HURRYVEINRECORD
																Set 
																		laycount_chr=?, 

																		pregnantweek_chr=?, 

																		scorecount_chr=?, 

																		rdbneckexpand_chr=?, 

																		rdbneckshink_chr=?, 

																		rdbhighlow_chr=?, 

																		rdbneckhard_chr=?, 

																		droppingcase_chr=?, 

																		indicate_chr=?, 

																		usecount_chr=?, 

																		layway_chr=?, 

																		rdbnecklocation_chr=? 

															Where InPatientID = ?
																and InPatientDate = ?	
																and Status = 0";
			
				IDataParameter[] objDPArr3 = null;
				p_objHRPServ.CreateDatabaseParameter(14,out objDPArr3);
				objDPArr3[0].Value=p_strlaycount_chr;
				objDPArr3[1].Value=p_strPregnantweek_chr;
				objDPArr3[2].Value=p_strScorecount_chr;
				objDPArr3[3].Value=p_strRdbneckexpand_chr;

				objDPArr3[4].Value=p_strRdbneckshink_chr;
				objDPArr3[5].Value=p_strRdbhighlow_chr;
				objDPArr3[6].Value=p_strRdbneckhard_chr;
				objDPArr3[7].Value=p_strDroppingcase_chr;

				objDPArr3[8].Value=p_strIndicate_chr;
				objDPArr3[9].Value=p_strUsecount_chr;
				objDPArr3[10].Value=p_strLayway_chr;
				objDPArr3[11].Value=p_strRdbnecklocation_chr;
                objDPArr3[12].Value = p_strInPatientID;
                objDPArr3[13].DbType = DbType.DateTime;
				objDPArr3[13].Value=Convert.ToDateTime(p_strInPatientDate);
	
				//ִ��SQL
			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strUpdateLaycountSQL,ref lngRes,objDPArr3);
				#endregion
			
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
		#endregion 

		#region �������л�ȡ����,����,��...
		/// <summary>
		/// �������л�ȡ����,����,��...
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <returns></returns>
		[AutoComplete]
		public   long m_lngGetOther(
			string p_strInPatientID,
			string p_strInPatientDate,
			out string p_strlaycount_chr,
			out string p_strPregnantweek_chr,
			out string p_strScorecount_chr,
			out string p_strRdbneckexpand_chr,
			out string p_strRdbneckshink_chr,
			out string p_strRdbhighlow_chr,
			out string p_strRdbneckhard_chr,
			out string p_strDroppingcase_chr,
			out string p_strIndicate_chr,
			out string p_strUsecount_chr,
			out string p_strLayway_chr,
			out string p_strRdbnecklocation_chr)
		{			
			
			 p_strlaycount_chr = "";
			 p_strPregnantweek_chr = "";
			 p_strScorecount_chr = "";
			 p_strRdbneckexpand_chr = "";
			 p_strRdbneckshink_chr = "";
			 p_strRdbhighlow_chr = "";
			 p_strRdbneckhard_chr = "";
			 p_strDroppingcase_chr = "";
			 p_strIndicate_chr = "";
			 p_strUsecount_chr = "";
			 p_strLayway_chr = "";
			 p_strRdbnecklocation_chr = "";
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

			#region sql bakup
//			string c_strSQL=  @"select  
//												T1.laycount_chr, 
//													T1.laycount_chrxml, 
//													T1.pregnantweek_chr, 
//													T1.pregnantweek_chrxml, 
//													T1.scorecount_chr, 
//													T1.scorecount_chrxml, 
//													T1.rdbneckexpand_chr, 
//													T1.rdbneckexpand_chrxml, 
//													T1.rdbneckshink_chr, 
//													T1.rdbneckshink_chrxml, 
//													T1.rdbhighlow_chr, 
//													T1.rdbhighlow_chrxml, 
//													T1.rdbneckhard_chr, 
//													T1.rdbneckhard_chrxml, 
//													T1.droppingcase_chr, 
//													T1.droppingcase_chrxml, 
//													T1.indicate_chr, 
//													T1.indicate_chrxml, 
//													T1.usecount_chr, 
//													T1.usecount_chrxml, 
//													T1.layway_chr, 
//													T1.layway_chrxml, 
//													T1.rdbnecklocation_chr, 
//													T1.rdbnecklocation_chrxml												
//														
//														FROM ICUACAD_HURRYVEINRECORD T1,
//															ICUACAD_HURRYVEINCONTENT T3,
//															T_BSE_EMPLOYEE             T4
//														WHERE T1.InPatientID = T3.InPatientID
//														AND T1.InPatientDate = T3.InPatientDate
//														AND T1.OpenDate = T3.OpenDate
//														AND T1.Status =0
//														AND T1.InPatientID = ?
//														AND T1.InPatientDate = ?
//														AND T1.CREATEUSERID = T4.EMPNO_CHR
//														ORDER BY T1.CREATEDATE";
			#endregion

			string c_strSQL=  @"select  
												T1.laycount_chr, 
													T1.laycount_chrxml, 
													T1.pregnantweek_chr, 
													T1.pregnantweek_chrxml, 
													T1.scorecount_chr, 
													T1.scorecount_chrxml, 
													T1.rdbneckexpand_chr, 
													T1.rdbneckexpand_chrxml, 
													T1.rdbneckshink_chr, 
													T1.rdbneckshink_chrxml, 
													T1.rdbhighlow_chr, 
													T1.rdbhighlow_chrxml, 
													T1.rdbneckhard_chr, 
													T1.rdbneckhard_chrxml, 
													T1.droppingcase_chr, 
													T1.droppingcase_chrxml, 
													T1.indicate_chr, 
													T1.indicate_chrxml, 
													T1.usecount_chr, 
													T1.usecount_chrxml, 
													T1.layway_chr, 
													T1.layway_chrxml, 
													T1.rdbnecklocation_chr, 
													T1.rdbnecklocation_chrxml												
														
														FROM ICUACAD_HURRYVEINRECORD T1
														WHERE 
														T1.Status =0
														AND T1.InPatientID = ?
														AND T1.InPatientDate = ?
														ORDER BY T1.CREATEDATE";


			long lngRes = 0;
				clsHRPTableService objHRPServ = new clsHRPTableService();
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
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					//���ý��
					p_strlaycount_chr = dtbValue.Rows[0]["laycount_chr"].ToString().Trim();
					p_strPregnantweek_chr = dtbValue.Rows[0]["Pregnantweek_chr"].ToString().Trim();
					p_strScorecount_chr = dtbValue.Rows[0]["Scorecount_chr"].ToString().Trim();
					p_strRdbneckexpand_chr = dtbValue.Rows[0]["Rdbneckexpand_chr"].ToString().Trim();
					p_strRdbneckshink_chr = dtbValue.Rows[0]["Rdbneckshink_chr"].ToString().Trim();
					p_strRdbhighlow_chr = dtbValue.Rows[0]["Rdbhighlow_chr"].ToString().Trim();
					p_strRdbneckhard_chr = dtbValue.Rows[0]["Rdbneckhard_chr"].ToString().Trim();
					p_strDroppingcase_chr = dtbValue.Rows[0]["Droppingcase_chr"].ToString().Trim();
					p_strIndicate_chr = dtbValue.Rows[0]["Indicate_chr"].ToString().Trim();
					p_strUsecount_chr = dtbValue.Rows[0]["Usecount_chr"].ToString().Trim();
					p_strLayway_chr = dtbValue.Rows[0]["Layway_chr"].ToString().Trim();
					p_strRdbnecklocation_chr = dtbValue.Rows[0]["Rdbnecklocation_chr"].ToString().Trim();
							
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
		#endregion 

		#region �������л�ȡ����ûɾ��������
		/// <summary>
		/// �������л�ȡ����ûɾ��������
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strBEFOREHAND_CHR">����</param>
		/// <param name="p_strLAYCOUNT_CHR">����</param>
		/// <returns></returns>
		[AutoComplete]
		public   long m_lngGetAllMainRecord(string p_strInPatientID,
			string p_strInPatientDate,
			out clsHurryVeinRecord [] p_objResultArr)
		{			
			p_objResultArr = new clsHurryVeinRecord [0];
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
	
			long lngRes = 0;
		
			if(lngRes < 0)//Ȩ��
			{
				return -1;
			}
			string c_strSQL=  @"SELECT distinct T1.*										
														FROM ICUACAD_HURRYVEINRECORD T1,															
															T_BSE_EMPLOYEE             T4
														WHERE 									
														 T1.Status =0
														AND T1.InPatientID = ?
														AND T1.InPatientDate = ?
														AND T1.CREATEUSERID = T4.EMPNO_CHR
														ORDER BY T1.CREATEDATE";
				clsHRPTableService objHRPSvc = new clsHRPTableService();
			try
			{
				DataTable dtbValue = new DataTable();

				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				objHRPSvc.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);

				lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strSQL,ref dtbValue,objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objResultArr = new clsHurryVeinRecord [dtbValue.Rows.Count];
					for(int i1=0;i1<p_objResultArr.Length;i1++)
					{
						#region set value
						p_objResultArr[i1] = new clsHurryVeinRecord ();
					
						p_objResultArr[i1].m_strInPatientID=p_strInPatientID;
						p_objResultArr[i1].m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
						p_objResultArr[i1].m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[i1]["CREATEDATE"].ToString());

						p_objResultArr[i1].m_strCHROMA_CHR=dtbValue.Rows[i1]["CHROMA_CHR"].ToString();
							
						p_objResultArr[i1].m_strDROPCOUNT_CHR=dtbValue.Rows[i1]["DROPCOUNT_CHR"].ToString();

						p_objResultArr[i1].m_strPALACESHRINK_CHR=dtbValue.Rows[i1]["PALACESHRINK_CHR"].ToString();

						p_objResultArr[i1].m_strEMBRYOHEART_CHR=dtbValue.Rows[i1]["EMBRYOHEART_CHR"].ToString();

						p_objResultArr[i1].m_strEXPAND_CHR =dtbValue.Rows[i1]["EXPAND_CHR"].ToString();

						p_objResultArr[i1].m_strPRESENTATION_CHR=dtbValue.Rows[i1]["PRESENTATION_CHR"].ToString();

						p_objResultArr[i1].m_strBLOODPRESSURE_CHR=dtbValue.Rows[i1]["BLOODPRESSURE_CHR"].ToString();

						p_objResultArr[i1].m_strSPECIALRECORD_CHR=dtbValue.Rows[i1]["SPECIALRECORD_CHR"].ToString();

						p_objResultArr[i1].m_strSIGNATURE_CHR=dtbValue.Rows[i1]["SIGNATURE_CHR"].ToString();

						p_objResultArr[i1].m_strLAYCOUNT_CHR=dtbValue.Rows[i1]["LAYCOUNT_CHR"].ToString();

						p_objResultArr[i1].m_strPREGNANTWEEK_CHR=dtbValue.Rows[i1]["PREGNANTWEEK_CHR"].ToString();

						p_objResultArr[i1].m_strSCORECOUNT_CHR=dtbValue.Rows[i1]["SCORECOUNT_CHR"].ToString();

						p_objResultArr[i1].m_strRDBNECKEXPAND_CHR=dtbValue.Rows[i1]["RDBNECKEXPAND_CHR"].ToString();

						p_objResultArr[i1].m_strRDBNECKSHINK_CHR=dtbValue.Rows[i1]["RDBNECKSHINK_CHR"].ToString();

						p_objResultArr[i1].m_strRDBHIGHLOW_CHR=dtbValue.Rows[i1]["RDBHIGHLOW_CHR"].ToString();

						p_objResultArr[i1].m_strRDBNECKHARD_CHR=dtbValue.Rows[i1]["RDBNECKHARD_CHR"].ToString();

						p_objResultArr[i1].m_strDROPPINGCASE_CHR=dtbValue.Rows[i1]["DROPPINGCASE_CHR"].ToString();

						p_objResultArr[i1].m_strINDICATE_CHR=dtbValue.Rows[i1]["INDICATE_CHR"].ToString();

						p_objResultArr[i1].m_strUSECOUNT_CHR=dtbValue.Rows[i1]["USECOUNT_CHR"].ToString();

						p_objResultArr[i1].m_strLAYWAY_CHR=dtbValue.Rows[i1]["LAYWAY_CHR"].ToString();

						p_objResultArr[i1].m_strRDBNECKLOCATION_CHR=dtbValue.Rows[i1]["RDBNECKLOCATION_CHR"].ToString();

						#endregion
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                objHRPSvc.Dispose();
            }
			return lngRes;
		}
		#endregion 

		#region update all first print date
		/// <summary>
		/// update all first print date��
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngUpdateALLFirstPrintDate(	string p_strInPatientID,
			string p_strInPatientDate,
			DateTime p_dtmFirstPrintDate)
		{
			//			long lngCheckRes =new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadService","m_lngUpdateALLFirstPrintDate");
			//			//if(lngCheckRes <= 0)
			//				//return lngCheckRes;	

			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//������                              
				if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;			
			
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strInPatientDate);
			
				//ִ��SQL
				long lngEff=0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateALLFirstPrintDateSQL, ref lngEff, objDPArr);
				
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
		#endregion
	}
}
