using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data; 

namespace com.digitalwave.DiseaseTrackService
{
	/// <summary>
	/// ʵ�������¼���м����
	/// һ�㻤���¼
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsGeneralNurseRecordService	: clsDiseaseTrackService
	{

		/// <summary>
		/// ��GeneralNurseRecord��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= "select createdate,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and status=0";
		/// <summary>
		/// ����ָ��������Ϣ����GeneralNurseRecord��GeneralNurseRecordContent���ұ������ݡ�
		/// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		/// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������
		/// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.recordcontent,
       a.recordcontentxml,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
       b.pagination,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,generalnurserecord a
                 where e.empno_chr = a.createuserid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = a.createuserid
           and rownum = 1) firstname
  from generalnurserecord a, generalnurserecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generalnurserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		/// <summary>
		/// ��GeneralNurseRecord�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select createuserid,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		/// <summary>
		/// ��GeneralNurseRecord��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ,��ȡ�޸ı�����Ҫ��Ϣ
		/// </summary>
//		private const string c_strGetExistInfoSQL= "";

		/// <summary>
		/// ��GeneralNurseRecordContent��ȡָ����������޸�ʱ�䡣
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select b.modifydate,b.modifyuserid from generalnurserecord a,generalnurserecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from generalnurserecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
				
		/// <summary>
		/// ��GeneralNurseRecordContent��ȡ�޸ı�����Ҫ��Ϣ��
		/// </summary>
//		private const string c_strGetModifyRecordSQL= "";

		/// <summary>
		/// ��GeneralNurseRecord��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select deactiveddate,deactivedoperatorid from generalnurserecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1";

		/// <summary>
		/// ��Ӽ�¼��GeneralNurseRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  generalnurserecord(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,recordcontent,recordcontentxml) 
				values(?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// ��Ӽ�¼��GeneralNurseRecordContent
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  generalnurserecordcontent(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,recordcontent_right) 
				values(?,?,?,?,?,?)";

		
		/// <summary>
		/// �޸ļ�¼��GeneralNurseRecordContent
		/// </summary>
		private const string c_strModifyRecordSQL= "update generalnurserecord set recordcontent=?,recordcontentxml=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?";

		/// <summary>
		/// �޸ļ�¼��GeneralNurseRecordContent
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����GeneralNurseRecord��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= "update generalnurserecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// ��GeneralNurseRecord��GeneralNurseRecordContent��ȡLastModifyDate��FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.firstprintdate,b.modifydate from generalnurserecord a,generalnurserecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from generalnurserecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
						
		/// <summary>
		/// ����GeneralNurseRecord��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "update  generalnurserecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		/// <summary>
		/// ��GeneralNurseRecord��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select createdate,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";

		/// <summary>
		/// ��GeneralNurseRecordContent�л�ȡָ��������Ϣ��
		/// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.recordcontent,
       a.recordcontentxml,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
       b.pagination
  from generalnurserecord a, generalnurserecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generalnurserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

		/// <summary>
		/// ��GeneralNurseRecord��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select createdate,opendate from generalnurserecord where inpatientid = ? and inpatientdate= ? and status=1";

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
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
                ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngGetRecordTimeList");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;
		
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
				//����
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
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
			long lngCheckRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
                //lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngUpdateFirstPrintDate");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//������                              
				if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
					return (long)enmOperationResult.Parameter_Error;
			
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(4,out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strOpenDate);
			
				//ִ��SQL
				long lngEff=0;
                lngCheckRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
                }

                return lngCheckRes;
						
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
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
                ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngGetDeleteRecordTimeListAll");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;
		
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
				//����
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
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
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{

                ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsGeneralNurseRecordService","m_lngGetDeleteRecordTimeListAll");
                ////if(lngCheckRes <= 0)
                //    //return lngCheckRes;	

				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
					return (long)enmOperationResult.Parameter_Error;
		
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
				//����
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    objHRPServ.Dispose();
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
			long lngRes = 0;
			try
			{
			
				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				new clsHRPTableService().CreateDatabaseParameter(3,out objDPArr);

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
                    DataRow objRow = dtbValue.Rows[0];
					//���ý��
					clsGeneralNurseRecordContent objRecordContent=new clsGeneralNurseRecordContent();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate=DateTime.Parse(objRow["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate=DateTime.Parse(objRow["MODIFYDATE"].ToString());
				
					if(objRow["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=objRow["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=objRow["MODIFYUSERID"].ToString();

					objRecordContent.m_strSignName = objRow["FIRSTNAME"].ToString();

					if(objRow["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
					if(objRow["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());
					objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();
				
					objRecordContent.m_strRecordContent_Right=objRow["RECORDCONTENT_RIGHT"].ToString();
					objRecordContent.m_strRecordContent=objRow["RECORDCONTENT"].ToString();
					objRecordContent.m_strRecordContentXml=objRow["RECORDCONTENTXML"].ToString();
							
					p_objRecordContent=	objRecordContent;		
				}
				//����
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
			long lngRes = 0;
			try
			{

				//������
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
				IDataParameter[] objDPArr = null;
				new clsHRPTableService().CreateDatabaseParameter(3,out objDPArr);

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
				//����	
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
			long lngRes = 0;
			try
			{
				//������                              
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				//��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
				clsGeneralNurseRecordContent objContent = (clsGeneralNurseRecordContent)p_objRecordContent;
		
				IDataParameter[] objDPArr = null;
				new clsHRPTableService().CreateDatabaseParameter(11,out objDPArr);

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
				objDPArr[9].Value=objContent.m_strRecordContent;
				objDPArr[10].Value=objContent.m_strRecordContentXml;
			
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(6,out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objContent.m_dtmModifyDate;
				objDPArr2[4].Value=objContent.m_strModifyUserID;			
				objDPArr2[5].Value=objContent.m_strRecordContent_Right;
			
				//ִ��SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL,ref lngEff,objDPArr2);
				if(lngRes<=0)return lngRes;
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
			long lngRes = 0;
			try
			{
				//������
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;
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
					//��DataTable�л�ȡModifyDate��ʹ֮��p_objRecordContent.m_dtmModifyDate�Ƚ�
				else if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//�����ͬ������DB_Succees
                    //if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

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
			long lngRes = 0;
			try
			{
				//������
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				//��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
				clsGeneralNurseRecordContent objContent = (clsGeneralNurseRecordContent)p_objRecordContent;
		
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(5,out objDPArr);

				objDPArr[0].Value=objContent.m_strRecordContent;
				objDPArr[1].Value=objContent.m_strRecordContentXml;
                objDPArr[2].Value = objContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=objContent.m_dtmOpenDate;		
			
			
				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				//��˳���IDataParameter��ֵ
				p_objHRPServ.CreateDatabaseParameter(6,out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objContent.m_dtmModifyDate;
				objDPArr2[4].Value=objContent.m_strModifyUserID;			
				objDPArr2[5].Value=objContent.m_strRecordContent_Right;
			
				//ִ��SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL,ref lngEff,objDPArr2);
				if(lngRes<=0)return lngRes;
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
			long lngRes = 0;
			try
			{
				//������
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=p_objRecordContent.m_strDeActivedOperatorID;			
		
				//ִ��SQL
				long lngEff=0;
				lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);
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
			long lngRes = 0;
			try
			{
				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
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
				//����
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
			long lngRes = 0;
			try
			{
				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
		
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
                    DataRow objRow = dtbValue.Rows[0];
					//���ý��
					clsGeneralNurseRecordContent objRecordContent=new clsGeneralNurseRecordContent();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate=DateTime.Parse(objRow["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate=DateTime.Parse(objRow["MODIFYDATE"].ToString());
				
					if(objRow["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=objRow["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=objRow["MODIFYUSERID"].ToString();
					objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
					objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());
					objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();
				
					objRecordContent.m_strRecordContent_Right=objRow["RECORDCONTENT_RIGHT"].ToString();
					objRecordContent.m_strRecordContent=objRow["RECORDCONTENT"].ToString();
					objRecordContent.m_strRecordContentXml=objRow["RECORDCONTENTXML"].ToString();
				
					p_objRecordContent=	objRecordContent;		
				}
				//����
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
        }
	}// END CLASS DEFINITION clsGeneralNurseRecordService

}
