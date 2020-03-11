using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsIntensiveTendRecord_GXService
{
	/// <summary>
	/// Σ�ػ��߻����¼(����)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsIntensiveTendRecord_GXService : clsDiseaseTrackService
	{
		#region SQL���
		/// <summary>
		/// ��T_EMR_INTENSIVETENDRECORD_GX��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from t_emr_intensivetendrecord_gx
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// ��T_EMR_INTENSIVETENDRECORD_GX�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from t_emr_intensivetendrecord_gx
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// ��T_EMR_INTENSIVETENDRECORD_GX��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from t_emr_intensivetendrecord_gx
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// ��Ӽ�¼��T_EMR_INTENSIVETENDRECORD_GX
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into t_emr_intensivetendrecord_gx (inpatientid,inpatientdate,
				opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,recorddate,initem,initemxml,
				infact,infactxml,outpiss,outpissxml,outstool,outstoolxml,checkt,checktxml,checkp,checkpxml,
				checkr,checkrxml,checkbpa,checkbpaxml,checkbps,checkbpsxml,nursesignid,diagnose,custom1,custom1xml,custom2,
				custom2xml,custom3,custom3xml,custom4,custom4xml,stat_status,isstat,sumin,sumout,sumintime,sumouttime,
				autosumin,autosumout,startstattime) 
				values (?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?)";

		/// <summary>
		/// ��Ӽ�¼��T_EMR_INTENSIVETENDCONTENT_GX
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into t_emr_intensivetendcontent_gx (inpatientid,inpatientdate,
				opendate,modifydate,modifyuserid,initem_right,infact_right,outpiss_right,outstool_right,
				checkt_right,checkp_right,checkr_right,checkbpa_right,checkbps_right,custom1_right,custom2_right,custom3_right,custom4_right) 
				values (?,?,?,?,?,?,?,?,?,?,
						?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��T_EMR_INTENSIVETENDRECORD_GX
		/// </summary>
		private const string c_strModifyRecordSQL= @"update t_emr_intensivetendrecord_gx 
			set recorddate=?,initem=?,initemxml=?,infact=?,infactxml=?,outpiss=?,outpissxml=?,outstool=?,outstoolxml=?,
			checkt=?,checktxml=?,checkp=?,checkpxml=?,checkr=?,checkrxml=?,checkbpa=?,checkbpaxml=?,checkbps=?,
			checkbpsxml=?,nursesignid=?,diagnose=?,custom1=?,custom1xml=?,custom2=?,custom2xml=?,custom3=?,custom3xml=?,
			custom4=?,custom4xml=?,stat_status=?,isstat=?,sumin=?,sumout=?,sumintime=?,sumouttime=?,autosumin=?,autosumout=?,startstattime=?
				where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// �޸ļ�¼��T_EMR_INTENSIVETENDCONTENT_GX
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����T_EMR_INTENSIVETENDRECORD_GX��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update t_emr_intensivetendrecord_gx
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// ����T_EMR_INTENSIVETENDRECORD_GX��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update t_emr_intensivetendrecord_gx
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// ��T_EMR_INTENSIVETENDRECORD_GX��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from t_emr_intensivetendrecord_gx
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// ��T_EMR_INTENSIVETENDRECORD_GX��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from t_emr_intensivetendrecord_gx
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecord_GXService","m_lngGetRecordTimeList");
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecord_GXService","m_lngUpdateFirstPrintDate");
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecord_GXService","m_lngGetDeleteRecordTimeList");
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

		// <summary>
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecord_GXService","m_lngGetDeleteRecordTimeListAll");
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
			long lngRes = 0;
			
			//������
			if(string.IsNullOrEmpty(p_strInPatientID)|| string.IsNullOrEmpty(p_strInPatientDate)||string.IsNullOrEmpty(p_strOpenDate))
				return (long)enmOperationResult.Parameter_Error;

            string strGetRecordContentSQL = @"select (select lastname_vchr
          from (select lastname_vchr, empid_chr, isemployee_int, empno_chr
                  from t_bse_employee
                 where status_int <> -1
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = t1.createuserid
           and rownum = 1) createusername,
       t1.inpatientid,
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
       t1.recorddate,
       t1.initem,
       t1.initemxml,
       t1.infact,
       t1.infactxml,
       t1.outpiss,
       t1.outpissxml,
       t1.outstool,
       t1.outstoolxml,
       t1.checkt,
       t1.checktxml,
       t1.checkp,
       t1.checkpxml,
       t1.checkr,
       t1.checkrxml,
       t1.checkbpa,
       t1.checkbpaxml,
       t1.checkbps,
       t1.checkbpsxml,
       t1.nursesignid,
       t1.diagnose,
       t1.custom1name,
       t1.custom2name,
       t1.custom3name,
       t1.custom4name,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom3,
       t1.custom3xml,
       t1.custom4,
       t1.custom4xml,
       t1.stat_status,
       t1.isstat,
       t1.sumin,
       t1.sumout,
       t1.sumintime,
       t1.sumouttime,
       t1.autosumin,
       t1.autosumout,
       t1.startstattime,
       t2.modifydate,
       t2.modifyuserid,
       t2.initem_right,
       t2.infact_right,
       t2.outpiss_right,
       t2.outstool_right,
       t2.checkt_right,
       t2.checkp_right,
       t2.checkr_right,
       t2.checkbpa_right,
       t2.checkbps_right,
       t2.custom1_right,
       t2.custom2_right,
       t2.custom3_right,
       t2.custom4_right
  from t_emr_intensivetendrecord_gx t1
 inner join t_emr_intensivetendcontent_gx t2 on (t1.inpatientid =
                                                t2.inpatientid and
                                                t1.inpatientdate =
                                                t2.inpatientdate and
                                                t1.opendate = t2.opendate)
 where t2.modifydate = (select max(modifydate)
                          from t_emr_intensivetendcontent_gx
                         where inpatientid = ?
                           and inpatientdate = ?
                           and opendate = ?)
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetRecordContentSQL, ref dtbValue, objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
                    DataRow objRow = dtbValue.Rows[0];
					#region ���ý��
					clsIntensiveTendRecord_GX objRecordContent = new clsIntensiveTendRecord_GX();
					objRecordContent.m_strInPatientID = p_strInPatientID;
					objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate = Convert.ToDateTime(objRow["OPENDATE"]);
					objRecordContent.m_dtmCreateDate = Convert.ToDateTime(objRow["CREATEDATE"]);
					objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
					objRecordContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();
					if(objRow["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(objRow["FIRSTPRINTDATE"]);
					if(objRow["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
					if(objRow["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());
					objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();

					objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(objRow["RECORDDATE"]);
					objRecordContent.m_strINITEM = objRow["INITEM"].ToString();
					objRecordContent.m_strINITEMXML = objRow["INITEMXML"].ToString();
					objRecordContent.m_strINFACT = objRow["INFACT"].ToString();
					objRecordContent.m_strINFACTXML = objRow["INFACTXML"].ToString();
					objRecordContent.m_strOUTPISS = objRow["OUTPISS"].ToString();
					objRecordContent.m_strOUTPISSXML = objRow["OUTPISSXML"].ToString();
					objRecordContent.m_strOUTSTOOL = objRow["OUTSTOOL"].ToString();
					objRecordContent.m_strOUTSTOOLXML = objRow["OUTSTOOLXML"].ToString();
					objRecordContent.m_strCHECKT = objRow["CHECKT"].ToString();
					objRecordContent.m_strCHECKTXML = objRow["CHECKTXML"].ToString();
					objRecordContent.m_strCHECKP = objRow["CHECKP"].ToString();
					objRecordContent.m_strCHECKPXML = objRow["CHECKPXML"].ToString();
					objRecordContent.m_strCHECKR = objRow["CHECKR"].ToString();
					objRecordContent.m_strCHECKRXML = objRow["CHECKRXML"].ToString();
					objRecordContent.m_strCHECKBPA = objRow["CHECKBPA"].ToString();
					objRecordContent.m_strCHECKBPAXML = objRow["CHECKBPAXML"].ToString();
					objRecordContent.m_strCHECKBPS = objRow["CHECKBPS"].ToString();
					objRecordContent.m_strCHECKBPSXML = objRow["CHECKBPSXML"].ToString();
					objRecordContent.m_strNURSESIGNID = objRow["NURSESIGNID"].ToString();
                    objRecordContent.m_strNURSESIGNNAME = objRow["CreateUserName"].ToString();
					objRecordContent.m_strDIAGNOSE = objRow["DIAGNOSE"].ToString();
					objRecordContent.m_strCUSTOM1 = objRow["CUSTOM1"].ToString();
					objRecordContent.m_strCUSTOM1XML = objRow["CUSTOM1XML"].ToString();
					objRecordContent.m_strCUSTOM2 = objRow["CUSTOM2"].ToString();
					objRecordContent.m_strCUSTOM2XML = objRow["CUSTOM2XML"].ToString();
					objRecordContent.m_strCUSTOM3 = objRow["CUSTOM3"].ToString();
					objRecordContent.m_strCUSTOM3XML = objRow["CUSTOM3XML"].ToString();
					objRecordContent.m_strCUSTOM4 = objRow["CUSTOM4"].ToString();
					objRecordContent.m_strCUSTOM4XML = objRow["CUSTOM4XML"].ToString();
					objRecordContent.m_intSTAT_STATUS = objRow["STAT_STATUS"]==DBNull.Value?0:Convert.ToInt32(objRow["STAT_STATUS"]);
					objRecordContent.m_strCUSTOM1NAME = objRow["CUSTOM1NAME"].ToString();
					objRecordContent.m_strCUSTOM2NAME = objRow["CUSTOM2NAME"].ToString();
					objRecordContent.m_strCUSTOM3NAME = objRow["CUSTOM3NAME"].ToString();
					objRecordContent.m_strCUSTOM4NAME = objRow["CUSTOM4NAME"].ToString();
					objRecordContent.m_intISSTAT = objRow["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(objRow["ISSTAT"]);
					objRecordContent.m_intSUMINTIME = objRow["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMINTIME"]);
					objRecordContent.m_intSUMOUTTIME = objRow["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMOUTTIME"]);
					objRecordContent.m_strAUTOSUMIN = objRow["AUTOSUMIN"].ToString();
					objRecordContent.m_strAUTOSUMOUT = objRow["AUTOSUMOUT"].ToString();
					objRecordContent.m_strSUMIN = objRow["SUMIN"].ToString();
					objRecordContent.m_strSUMOUT = objRow["SUMOUT"].ToString();
					objRecordContent.m_dtmSTARTSTATTIME = objRow["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(objRow["STARTSTATTIME"]);

					objRecordContent.m_dtmModifyDate = Convert.ToDateTime(objRow["MODIFYDATE"]);
					objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
					objRecordContent.m_strINITEM_RIGHT = objRow["INITEM_RIGHT"].ToString();
					objRecordContent.m_strINFACT_RIGHT = objRow["INFACT_RIGHT"].ToString();
					objRecordContent.m_strOUTPISS_RIGHT = objRow["OUTPISS_RIGHT"].ToString();
					objRecordContent.m_strOUTSTOOL_RIGHT = objRow["OUTSTOOL_RIGHT"].ToString();
					objRecordContent.m_strCHECKT_RIGHT = objRow["CHECKT_RIGHT"].ToString();
					objRecordContent.m_strCHECKP_RIGHT = objRow["CHECKP_RIGHT"].ToString();
					objRecordContent.m_strCHECKR_RIGHT = objRow["CHECKR_RIGHT"].ToString();
					objRecordContent.m_strCHECKBPA_RIGHT = objRow["CHECKBPA_RIGHT"].ToString();
					objRecordContent.m_strCHECKBPS_RIGHT = objRow["CHECKBPS_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM4_RIGHT = objRow["CUSTOM4_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM3_RIGHT = objRow["CUSTOM3_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM2_RIGHT = objRow["CUSTOM2_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM1_RIGHT = objRow["CUSTOM1_RIGHT"].ToString();

					p_objRecordContent = objRecordContent;
					#endregion
				}
			}
			catch(Exception objEx)
			{
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
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmCreateDate;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);
					
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
			long lngRes = 0;
			clsIntensiveTendRecord_GX objRecordContent = (clsIntensiveTendRecord_GX)p_objRecordContent;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(47,out objDPArr);
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

                objDPArr[9].DbType = DbType.DateTime;
				objDPArr[9].Value = objRecordContent.m_dtmRECORDDATE;
				objDPArr[10].Value = objRecordContent.m_strINITEM;
				objDPArr[11].Value = objRecordContent.m_strINITEMXML;
				objDPArr[12].Value = objRecordContent.m_strINFACT;
				objDPArr[13].Value = objRecordContent.m_strINFACTXML;
				objDPArr[14].Value = objRecordContent.m_strOUTPISS;
				objDPArr[15].Value = objRecordContent.m_strOUTPISSXML;
				objDPArr[16].Value = objRecordContent.m_strOUTSTOOL;
				objDPArr[17].Value = objRecordContent.m_strOUTSTOOLXML;
				objDPArr[18].Value = objRecordContent.m_strCHECKT;
				objDPArr[19].Value = objRecordContent.m_strCHECKTXML;
				objDPArr[20].Value = objRecordContent.m_strCHECKP;
				objDPArr[21].Value = objRecordContent.m_strCHECKPXML;
				objDPArr[22].Value = objRecordContent.m_strCHECKR;
				objDPArr[23].Value = objRecordContent.m_strCHECKRXML;
				objDPArr[24].Value = objRecordContent.m_strCHECKBPA;
				objDPArr[25].Value = objRecordContent.m_strCHECKBPAXML;
				objDPArr[26].Value = objRecordContent.m_strCHECKBPS;
				objDPArr[27].Value = objRecordContent.m_strCHECKBPSXML;
				objDPArr[28].Value = objRecordContent.m_strNURSESIGNID;
				objDPArr[29].Value = objRecordContent.m_strDIAGNOSE;
				objDPArr[30].Value = objRecordContent.m_strCUSTOM1;
				objDPArr[31].Value = objRecordContent.m_strCUSTOM1XML;
				objDPArr[32].Value = objRecordContent.m_strCUSTOM2;
				objDPArr[33].Value = objRecordContent.m_strCUSTOM2XML;
				objDPArr[34].Value = objRecordContent.m_strCUSTOM3;
				objDPArr[35].Value = objRecordContent.m_strCUSTOM3XML;
				objDPArr[36].Value = objRecordContent.m_strCUSTOM4;
				objDPArr[37].Value = objRecordContent.m_strCUSTOM4XML;
				objDPArr[38].Value = objRecordContent.m_intSTAT_STATUS;
				objDPArr[39].Value = objRecordContent.m_intISSTAT;
				objDPArr[40].Value = objRecordContent.m_strSUMIN;
				objDPArr[41].Value = objRecordContent.m_strSUMOUT;
				objDPArr[42].Value = objRecordContent.m_intSUMINTIME;
				objDPArr[43].Value = objRecordContent.m_intSUMOUTTIME;
				objDPArr[44].Value = objRecordContent.m_strAUTOSUMIN;
				objDPArr[45].Value = objRecordContent.m_strAUTOSUMOUT;
                objDPArr[46].DbType = DbType.DateTime;
				objDPArr[46].Value = objRecordContent.m_dtmSTARTSTATTIME;

				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(18,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;

				objDPArr2[5].Value = objRecordContent.m_strINITEM_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strINFACT_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strOUTPISS_RIGHT;
				objDPArr2[8].Value = objRecordContent.m_strOUTSTOOL_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strCHECKT_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strCHECKP_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strCHECKR_RIGHT;
				objDPArr2[12].Value = objRecordContent.m_strCHECKBPA_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strCHECKBPS_RIGHT;
				objDPArr2[14].Value = objRecordContent.m_strCUSTOM1_RIGHT;
				objDPArr2[15].Value = objRecordContent.m_strCUSTOM2_RIGHT;
				objDPArr2[16].Value = objRecordContent.m_strCUSTOM3_RIGHT;
				objDPArr2[17].Value = objRecordContent.m_strCUSTOM4_RIGHT;

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
			long lngRes = 0;
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			clsIntensiveTendRecord_GX objRecordContent = (clsIntensiveTendRecord_GX)p_objRecordContent;
 
			/// <summary>
			/// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from t_emr_intensivetendrecord_gx t1,t_emr_intensivetendcontent_gx t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

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
		
			long lngRes = 0;
			clsIntensiveTendRecord_GX objRecordContent = (clsIntensiveTendRecord_GX)p_objRecordContent;
			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(41, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = objRecordContent.m_dtmRECORDDATE;
				objDPArr[1].Value = objRecordContent.m_strINITEM;
				objDPArr[2].Value = objRecordContent.m_strINITEMXML;
				objDPArr[3].Value = objRecordContent.m_strINFACT;
				objDPArr[4].Value = objRecordContent.m_strINFACTXML;
				objDPArr[5].Value = objRecordContent.m_strOUTPISS;
				objDPArr[6].Value = objRecordContent.m_strOUTPISSXML;
				objDPArr[7].Value = objRecordContent.m_strOUTSTOOL;
				objDPArr[8].Value = objRecordContent.m_strOUTSTOOLXML;
				objDPArr[9].Value = objRecordContent.m_strCHECKT;
				objDPArr[10].Value = objRecordContent.m_strCHECKTXML;
				objDPArr[11].Value = objRecordContent.m_strCHECKP;
				objDPArr[12].Value = objRecordContent.m_strCHECKPXML;
				objDPArr[13].Value = objRecordContent.m_strCHECKR;
				objDPArr[14].Value = objRecordContent.m_strCHECKRXML;
				objDPArr[15].Value = objRecordContent.m_strCHECKBPA;
				objDPArr[16].Value = objRecordContent.m_strCHECKBPAXML;
				objDPArr[17].Value = objRecordContent.m_strCHECKBPS;
				objDPArr[18].Value = objRecordContent.m_strCHECKBPSXML;
				objDPArr[19].Value = objRecordContent.m_strNURSESIGNID;
				objDPArr[20].Value = objRecordContent.m_strDIAGNOSE;
				objDPArr[21].Value = objRecordContent.m_strCUSTOM1;
				objDPArr[22].Value = objRecordContent.m_strCUSTOM1XML;
				objDPArr[23].Value = objRecordContent.m_strCUSTOM2;
				objDPArr[24].Value = objRecordContent.m_strCUSTOM2XML;
				objDPArr[25].Value = objRecordContent.m_strCUSTOM3;
				objDPArr[26].Value = objRecordContent.m_strCUSTOM3XML;
				objDPArr[27].Value = objRecordContent.m_strCUSTOM4;
				objDPArr[28].Value = objRecordContent.m_strCUSTOM4XML;
				objDPArr[29].Value = objRecordContent.m_intSTAT_STATUS;
				objDPArr[30].Value = objRecordContent.m_intISSTAT;
				objDPArr[31].Value = objRecordContent.m_strSUMIN;
				objDPArr[32].Value = objRecordContent.m_strSUMOUT;
				objDPArr[33].Value = objRecordContent.m_intSUMINTIME;
				objDPArr[34].Value = objRecordContent.m_intSUMOUTTIME;
				objDPArr[35].Value = objRecordContent.m_strAUTOSUMIN;
                objDPArr[36].Value = objRecordContent.m_strAUTOSUMOUT;
                objDPArr[37].DbType = DbType.DateTime;
				objDPArr[37].Value = objRecordContent.m_dtmSTARTSTATTIME;

                objDPArr[38].Value = objRecordContent.m_strInPatientID;
                objDPArr[39].DbType = DbType.DateTime;
                objDPArr[39].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[40].DbType = DbType.DateTime;
				objDPArr[40].Value = objRecordContent.m_dtmOpenDate;

				//ִ��SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(18,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;

				objDPArr2[5].Value = objRecordContent.m_strINITEM_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strINFACT_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strOUTPISS_RIGHT;
				objDPArr2[8].Value = objRecordContent.m_strOUTSTOOL_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strCHECKT_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strCHECKP_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strCHECKR_RIGHT;
				objDPArr2[12].Value = objRecordContent.m_strCHECKBPA_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strCHECKBPS_RIGHT;
				objDPArr2[14].Value = objRecordContent.m_strCUSTOM1_RIGHT;
				objDPArr2[15].Value = objRecordContent.m_strCUSTOM2_RIGHT;
				objDPArr2[16].Value = objRecordContent.m_strCUSTOM3_RIGHT;
				objDPArr2[17].Value = objRecordContent.m_strCUSTOM4_RIGHT;

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

			long lngRes = 0;
			clsIntensiveTendRecord_GX objRecordContent = new clsIntensiveTendRecord_GX();

			try
			{
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
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
			long lngRes = 0;
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			/// <summary>
			/// ��IntensiveTendRecord1��IntensiveTendRecordContent1��ȡLastModifyDate��FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+ @" a.firstprintdate,b.modifydate from t_emr_intensivetendrecord_gx a,
					t_emr_intensivetendcontent_gx b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

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
			long lngRes = 0;
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select (select lastname_vchr
          from (select lastname_vchr, empid_chr, isemployee_int, empno_chr
                  from t_bse_employee
                 where status_int <> -1
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = t1.createuserid
           and rownum = 1) createusername,
       t1.inpatientid,
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
       t1.recorddate,
       t1.initem,
       t1.initemxml,
       t1.infact,
       t1.infactxml,
       t1.outpiss,
       t1.outpissxml,
       t1.outstool,
       t1.outstoolxml,
       t1.checkt,
       t1.checktxml,
       t1.checkp,
       t1.checkpxml,
       t1.checkr,
       t1.checkrxml,
       t1.checkbpa,
       t1.checkbpaxml,
       t1.checkbps,
       t1.checkbpsxml,
       t1.nursesignid,
       t1.diagnose,
       t1.custom1name,
       t1.custom2name,
       t1.custom3name,
       t1.custom4name,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom3,
       t1.custom3xml,
       t1.custom4,
       t1.custom4xml,
       t1.stat_status,
       t1.isstat,
       t1.sumin,
       t1.sumout,
       t1.sumintime,
       t1.sumouttime,
       t1.autosumin,
       t1.autosumout,
       t1.startstattime,
       t2.modifydate,
       t2.modifyuserid,
       t2.initem_right,
       t2.infact_right,
       t2.outpiss_right,
       t2.outstool_right,
       t2.checkt_right,
       t2.checkp_right,
       t2.checkr_right,
       t2.checkbpa_right,
       t2.checkbps_right,
       t2.custom1_right,
       t2.custom2_right,
       t2.custom3_right,
       t2.custom4_right
  from t_emr_intensivetendrecord_gx t1
 inner join t_emr_intensivetendcontent_gx t2 on (t1.inpatientid =
                                                t2.inpatientid and
                                                t1.inpatientdate =
                                                t2.inpatientdate and
                                                t1.opendate = t2.opendate)
 where t2.modifydate = (select max(modifydate)
                          from t_emr_intensivetendcontent_gx
                         where inpatientid = ?
                           and inpatientdate = ?
                           and opendate = ?)
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?";
		
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
                    DataRow objRow = dtbValue.Rows[0];
					#region ���ý��
					clsIntensiveTendRecord_GX objRecordContent = new clsIntensiveTendRecord_GX();
					objRecordContent.m_strInPatientID = p_strInPatientID;
					objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate = Convert.ToDateTime(objRow["OPENDATE"]);
					objRecordContent.m_dtmCreateDate = Convert.ToDateTime(objRow["CREATEDATE"]);
					objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
					if(objRow["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(objRow["FIRSTPRINTDATE"]);
					if(objRow["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
					if(objRow["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();
					objRecordContent.m_strDeActivedOperatorID = objRow["DEACTIVEDOPERATORID"].ToString();
					objRecordContent.m_dtmDeActivedDate = Convert.ToDateTime(objRow["DEACTIVEDDATE"]);

					objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(objRow["RECORDDATE"]);
					objRecordContent.m_strINITEM = objRow["INITEM"].ToString();
					objRecordContent.m_strINITEMXML = objRow["INITEMXML"].ToString();
					objRecordContent.m_strINFACT = objRow["INFACT"].ToString();
					objRecordContent.m_strINFACTXML = objRow["INFACTXML"].ToString();
					objRecordContent.m_strOUTPISS = objRow["OUTPISS"].ToString();
					objRecordContent.m_strOUTPISSXML = objRow["OUTPISSXML"].ToString();
					objRecordContent.m_strOUTSTOOL = objRow["OUTSTOOL"].ToString();
					objRecordContent.m_strOUTSTOOLXML = objRow["OUTSTOOLXML"].ToString();
					objRecordContent.m_strCHECKT = objRow["CHECKT"].ToString();
					objRecordContent.m_strCHECKTXML = objRow["CHECKTXML"].ToString();
					objRecordContent.m_strCHECKP = objRow["CHECKP"].ToString();
					objRecordContent.m_strCHECKPXML = objRow["CHECKPXML"].ToString();
					objRecordContent.m_strCHECKR = objRow["CHECKR"].ToString();
					objRecordContent.m_strCHECKRXML = objRow["CHECKRXML"].ToString();
					objRecordContent.m_strCHECKBPA = objRow["CHECKBPA"].ToString();
					objRecordContent.m_strCHECKBPAXML = objRow["CHECKBPAXML"].ToString();
					objRecordContent.m_strCHECKBPS = objRow["CHECKBPS"].ToString();
					objRecordContent.m_strCHECKBPSXML = objRow["CHECKBPSXML"].ToString();
					objRecordContent.m_strNURSESIGNID = objRow["NURSESIGNID"].ToString();
                    objRecordContent.m_strNURSESIGNNAME = objRow["CreateUserName"].ToString();
					objRecordContent.m_strDIAGNOSE = objRow["DIAGNOSE"].ToString();
					objRecordContent.m_strCUSTOM1 = objRow["CUSTOM1"].ToString();
					objRecordContent.m_strCUSTOM1XML = objRow["CUSTOM1XML"].ToString();
					objRecordContent.m_strCUSTOM2 = objRow["CUSTOM2"].ToString();
					objRecordContent.m_strCUSTOM2XML = objRow["CUSTOM2XML"].ToString();
					objRecordContent.m_strCUSTOM3 = objRow["CUSTOM3"].ToString();
					objRecordContent.m_strCUSTOM3XML = objRow["CUSTOM3XML"].ToString();
					objRecordContent.m_strCUSTOM4 = objRow["CUSTOM4"].ToString();
					objRecordContent.m_strCUSTOM4XML = objRow["CUSTOM4XML"].ToString();
					objRecordContent.m_intSTAT_STATUS = objRow["STAT_STATUS"]==DBNull.Value?0:Convert.ToInt32(objRow["STAT_STATUS"]);
					objRecordContent.m_strCUSTOM1NAME = objRow["CUSTOM1NAME"].ToString();
					objRecordContent.m_strCUSTOM2NAME = objRow["CUSTOM2NAME"].ToString();
					objRecordContent.m_strCUSTOM3NAME = objRow["CUSTOM3NAME"].ToString();
					objRecordContent.m_strCUSTOM4NAME = objRow["CUSTOM4NAME"].ToString();
					objRecordContent.m_intISSTAT = objRow["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(objRow["ISSTAT"]);
					objRecordContent.m_intSUMINTIME = objRow["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMINTIME"]);
					objRecordContent.m_intSUMOUTTIME = objRow["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMOUTTIME"]);
					objRecordContent.m_strAUTOSUMIN = objRow["AUTOSUMIN"].ToString();
					objRecordContent.m_strAUTOSUMOUT = objRow["AUTOSUMOUT"].ToString();
					objRecordContent.m_strSUMIN = objRow["SUMIN"].ToString();
					objRecordContent.m_strSUMOUT = objRow["SUMOUT"].ToString();
					objRecordContent.m_dtmSTARTSTATTIME = objRow["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(objRow["STARTSTATTIME"]);

					objRecordContent.m_dtmModifyDate = Convert.ToDateTime(objRow["MODIFYDATE"]);
					objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
					objRecordContent.m_strINITEM_RIGHT = objRow["INITEM_RIGHT"].ToString();
					objRecordContent.m_strINFACT_RIGHT = objRow["INFACT_RIGHT"].ToString();
					objRecordContent.m_strOUTPISS_RIGHT = objRow["OUTPISS_RIGHT"].ToString();
					objRecordContent.m_strOUTSTOOL_RIGHT = objRow["OUTSTOOL_RIGHT"].ToString();
					objRecordContent.m_strCHECKT_RIGHT = objRow["CHECKT_RIGHT"].ToString();
					objRecordContent.m_strCHECKP_RIGHT = objRow["CHECKP_RIGHT"].ToString();
					objRecordContent.m_strCHECKR_RIGHT = objRow["CHECKR_RIGHT"].ToString();
					objRecordContent.m_strCHECKBPA_RIGHT = objRow["CHECKBPA_RIGHT"].ToString();
					objRecordContent.m_strCHECKBPS_RIGHT = objRow["CHECKBPS_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM4_RIGHT = objRow["CUSTOM4_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM3_RIGHT = objRow["CUSTOM3_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM2_RIGHT = objRow["CUSTOM2_RIGHT"].ToString();
					objRecordContent.m_strCUSTOM1_RIGHT = objRow["CUSTOM1_RIGHT"].ToString();

					p_objRecordContent = objRecordContent;
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
		/// ���²����¼
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngUpdateDetail(clsIntensiveTendRecordDetail_GX p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;
			try
			{
				string strSQL=@"update t_emr_intensivetenddetail_gx 
								set detailcontent=?,detailcontentxml=? 
								where inpatientid=? and inpatientdate=? and opendate=?";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(5,out objDPArr);
				objDPArr[0].Value=p_objRecordContent.m_strDETAILCONTENT;
				objDPArr[1].Value=p_objRecordContent.m_strDETAILCONTENTXML;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=p_objRecordContent.m_dtmOpenDate;

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
				string strSQL= @"select t.detailcontent, t.detailcontentxml
									from t_emr_intensivetenddetail_gx t
								where opendate = ?
									and inpatientid = ?
									and status = 0";
				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null;
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
					strRecordContent=dtbValue.Rows[0]["DETAILCONTENT"].ToString();
					strRecordCotentXML=dtbValue.Rows[0]["DETAILCONTENTXML"].ToString();
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
		public long m_lngAddNewDetail(clsIntensiveTendRecordDetail_GX p_objRecordContent)
		{
			//������                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@"insert into t_emr_intensivetenddetail_gx (inpatientid,inpatientdate,opendate,createdate,
				createuserid,modifyuserid,modifydate,detailrecorddate,detailcontent,detailcontentxml,detailsignid,status,stat_status) 
				values (?,?,?,?,?,?,?,?,?,?,?,?,?)";

				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(13,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=p_objRecordContent.m_dtmCreateDate;
				objDPArr[4].Value=p_objRecordContent.m_strCreateUserID;
                objDPArr[5].Value = p_objRecordContent.m_strModifyUserID;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr[7].DbType = DbType.DateTime;
				objDPArr[7].Value=p_objRecordContent.m_dtmDETAILRECORDDATE;
				objDPArr[8].Value=p_objRecordContent.m_strDETAILCONTENT;
				objDPArr[9].Value=p_objRecordContent.m_strDETAILCONTENTXML;
				objDPArr[10].Value = p_objRecordContent.m_strDETAILSIGNID;
				objDPArr[11].Value = 0;
				objDPArr[12].Value = p_objRecordContent.m_intSTAT_STATUS;
				
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
		public long m_lngModifyDetail(clsIntensiveTendRecordDetail_GX p_objRecordContent)
		{
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				//������                              
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
					return (long)enmOperationResult.Parameter_Error;
                string strSQL = @"update t_emr_intensivetenddetail_gx
   set modifyuserid     = ?,
       modifydate       = ?,
       detailcontent    = ?,
       detailcontentxml = ?
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?";

				//��ȡIDataParameter����
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(7,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strModifyUserID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=p_objRecordContent.m_dtmModifyDate;
				objDPArr[2].Value=p_objRecordContent.m_strDETAILCONTENT;
				objDPArr[3].Value=p_objRecordContent.m_strDETAILCONTENTXML;
                objDPArr[4].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[6].DbType = DbType.DateTime;
				objDPArr[6].Value=p_objRecordContent.m_dtmOpenDate;//ע��˴��Ĵ���ʱ��Ϊ���̼�¼���ݵĴ���ʱ��
				
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
		/// ��ȡָ�������¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strRecordDate">��¼����</param>
		/// <param name="p_strRecordContentArr">���������¼����([0]��ʵ����ʱ��,[1]��¼��ID,[2]��¼������[3]����[4]����XML[5]��¼ʱ��[6]�޸�ʱ��)</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,string p_strInPatientDate,string p_strRecordDate,
			out string[] p_strRecordContentArr)
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
       t.modifydate,
       t.detailrecorddate,
       t.detailcontent,
       t.detailcontentxml,
       t.detailsignid,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.stat_status,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_intensivetenddetail_gx i
                 where e.empno_chr = i.detailsignid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.detailsignid
           and rownum = 1) lastname_vchr
  from t_emr_intensivetenddetail_gx t
 where t.status = 0
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.opendate = ?";
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
                    DataRow objRow = dtbValue.Rows[0];
					p_strRecordContentArr=new string[8]; 
					p_strRecordContentArr[0]=objRow["CreateDate"].ToString();
					p_strRecordContentArr[1]=objRow["DETAILSIGNID"].ToString();
					p_strRecordContentArr[2]=objRow["LASTNAME_VCHR"].ToString();
					p_strRecordContentArr[3]=objRow["DETAILCONTENT"].ToString();
					p_strRecordContentArr[4]=objRow["DETAILCONTENTXML"].ToString();
					p_strRecordContentArr[5]=objRow["DETAILRECORDDATE"].ToString();
					p_strRecordContentArr[6]=objRow["MODIFYDATE"].ToString();
                    p_strRecordContentArr[7] = objRow["CREATEUSERID"].ToString();
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
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
				string strSQL=@" update t_emr_intensivetenddetail_gx set status=1,deactiveddate=?,deactivedoperatorid=?
								where inpatientid=? and  inpatientdate=? and  opendate=?";
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
		/// <param name="p_strRecordContentArr">���̼�¼��������([0]����[1]����XML[2]��¼ʱ��[3]ǩ����ID[4]ǩ��������)</param>
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
       t.modifydate,
       t.detailrecorddate,
       t.detailcontent,
       t.detailcontentxml,
       t.detailsignid,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.stat_status,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_intensivetenddetail_gx i
                 where e.empno_chr = i.detailsignid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.detailsignid
           and rownum = 1) lastname_vchr
  from t_emr_intensivetenddetail_gx t
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
						p_strRecordContentArr[i] = new string[5];
						p_strRecordContentArr[i][0] = dtbValue.Rows[0]["DETAILCONTENT"].ToString();
						p_strRecordContentArr[i][1] = dtbValue.Rows[0]["DETAILCONTENTXML"].ToString();
						p_strRecordContentArr[i][2] = dtbValue.Rows[0]["DETAILRECORDDATE"].ToString();
						p_strRecordContentArr[i][3] = dtbValue.Rows[0]["DETAILSIGNID"].ToString();
						p_strRecordContentArr[i][4] = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
					}
				}
			}
			catch(Exception objEx)
			{
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
		///  ��ȡָ�����˵�ɾ�������¼����
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">סԺ����</param>
		/// <param name="p_objRecordContent">���̼�¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDelRecordContentWithInpatient(string p_strInPatientID,string p_strInPatientDate,
			string p_strOpenDate,
			out clsIntensiveTendRecordDetail_GX p_objRecordContent)
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
       t.modifydate,
       t.detailrecorddate,
       t.detailcontent,
       t.detailcontentxml,
       t.detailsignid,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.stat_status,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_intensivetenddetail_gx i
                 where e.empno_chr = i.detailsignid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.detailsignid
           and rownum = 1) lastname_vchr
  from t_emr_intensivetenddetail_gx t
 where t.status = 1
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.opendate = ?";
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
					p_objRecordContent = new clsIntensiveTendRecordDetail_GX();
					for (int i = 0; i < dtbValue.Rows.Count; i++)
					{
						p_objRecordContent.m_strDETAILCONTENT = dtbValue.Rows[0]["DETAILCONTENT"].ToString();
						p_objRecordContent.m_strDETAILCONTENTXML = dtbValue.Rows[0]["DETAILCONTENTXML"].ToString();
						p_objRecordContent.m_dtmDETAILRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["DETAILRECORDDATE"]);
						p_objRecordContent.m_strDETAILSIGNID = dtbValue.Rows[0]["DETAILSIGNID"].ToString();
						p_objRecordContent.m_strDETAILSIGNNAME = dtbValue.Rows[0]["LASTNAME_VCHR"].ToString();
						p_objRecordContent.m_intSTAT_STATUS = dtbValue.Rows[0]["STAT_STATUS"]==DBNull.Value ? 0:Convert.ToInt32(dtbValue.Rows[0]["STAT_STATUS"]);
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
		/// ��ȡһ��ʱ���ڵ���������
		/// </summary>
		/// <param name="p_strEndTime">����ʱ��</param>
		/// <param name="p_strStartTime">��ʼʱ��</param>
		/// <param name="p_dblInSumArr">��������ʽ�洢��������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetInSum(string p_strInPatientID, 
			string p_strInPatientDate, 
			string p_strEndTime, 
			string p_strStartTime, 
			out double[] p_dblInSumArr)
		{
			long lngRes = 0;
			p_dblInSumArr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select infact from t_emr_intensivetendrecord_gx where inpatientid = ?
									and inpatientdate = ?
									and recorddate between ? and ? and status=0";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strStartTime);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strEndTime);

				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 )
				{
					p_dblInSumArr = new double[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
						p_dblInSumArr[i] = dtbValue.Rows[i]["INFACT"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["INFACT"]);
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
		/// ��ȡһ��ʱ�������г���
		/// </summary>
		/// <param name="p_strEndTime">����ʱ��</param>
		/// <param name="p_strStartTime">��ʼʱ��</param>
		/// <param name="p_dblOutPissArr">��������ʽ�洢����>>С��</param>
		/// <param name="p_dblOutStoolArr">��������ʽ�洢����>>���</param>
		/// <param name="p_dblCustom1Arr">��������ʽ�洢����>>�û��Զ�����1</param>
		/// <param name="p_dblCustom2Arr">��������ʽ�洢����>>�û��Զ�����2</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetOutSum(string p_strInPatientID, 
			string p_strInPatientDate, 
			string p_strEndTime,
			string p_strStartTime, 
			out double[] p_dblOutPissArr,
			out double[] p_dblOutStoolArr,
			out double[] p_dblCustom1Arr,
			out double[] p_dblCustom2Arr)
		{
			long lngRes = 0;
			p_dblOutPissArr = null;
			p_dblOutStoolArr = null;
			p_dblCustom1Arr = null;
			p_dblCustom2Arr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select outpiss,outstool,custom1,custom2 from t_emr_intensivetendrecord_gx where inpatientid = ?
									and inpatientdate = ?
									and recorddate between ? and ? and status=0";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strStartTime);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strEndTime);

				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 )
				{
					p_dblOutPissArr = new double[dtbValue.Rows.Count];
					p_dblOutStoolArr = new double[dtbValue.Rows.Count];
					p_dblCustom1Arr = new double[dtbValue.Rows.Count];
					p_dblCustom2Arr = new double[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
						p_dblOutPissArr[i] = dtbValue.Rows[i]["OUTPISS"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["OUTPISS"]);
						p_dblOutStoolArr[i] = dtbValue.Rows[i]["OUTSTOOL"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["OUTSTOOL"]);
						p_dblCustom1Arr[i] = dtbValue.Rows[i]["CUSTOM1"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["CUSTOM1"]);
						p_dblCustom2Arr[i] = dtbValue.Rows[i]["CUSTOM2"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["CUSTOM2"]);
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
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"update t_emr_intensivetendrecord_gx set "+p_strColumnIndex+@"=? 
								where inpatientid=? and  inpatientdate=?";
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
				objDPArr[0].Value=p_strColumnName;
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strInPatientDate);

				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
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
		/// ��ȡ���Ϊ��ͳ�Ƶļ�¼ʱ��
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_intStatStatus">ͳ�Ʊ�־</param>
		/// <param name="p_dtmStatTimeArr">��¼ʱ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetStatRecordTime(string p_strInPatientID, 
			string p_strInPatientDate, 
			out clsIntensiveTendRecord_GX[] p_objRecordArr)
		{
			long lngRes = 0;
			p_objRecordArr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select recorddate,sumintime,sumouttime,autosumin,autosumout,sumin,sumout
								 from t_emr_intensivetendrecord_gx 
									where inpatientid = ?
									and inpatientdate = ?
									and isstat = 1 and status=0 order by recorddate";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);

				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objRecordArr = new clsIntensiveTendRecord_GX[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
						p_objRecordArr[i] = new clsIntensiveTendRecord_GX();
						p_objRecordArr[i].m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);
						p_objRecordArr[i].m_intISSTAT = 1;
						p_objRecordArr[i].m_intSUMINTIME = Convert.ToInt32(dtbValue.Rows[i]["SUMINTIME"]);
						p_objRecordArr[i].m_intSUMOUTTIME = Convert.ToInt32(dtbValue.Rows[i]["SUMOUTTIME"]);
						p_objRecordArr[i].m_strAUTOSUMIN = dtbValue.Rows[i]["AUTOSUMIN"].ToString();
						p_objRecordArr[i].m_strAUTOSUMOUT = dtbValue.Rows[i]["AUTOSUMOUT"].ToString();
						p_objRecordArr[i].m_strSUMIN = dtbValue.Rows[i]["SUMIN"].ToString();
						p_objRecordArr[i].m_strSUMOUT = dtbValue.Rows[i]["SUMOUT"].ToString();
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
		/// ��ȡ�����״μ�¼��ʱ��
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_dtmRecordDate">��¼ʱ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMinRecordDate(string p_strInPatientID, 
			string p_strInPatientDate, 
			out DateTime p_dtmRecordDate)
		{
			long lngRes = 0;
			p_dtmRecordDate = DateTime.MinValue;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select min(t.recorddate) as recorddate
								from t_emr_intensivetendrecord_gx t
								where inpatientid = ?
								and t.inpatientdate = ?
								and t.status = 0";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);

				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count == 1)
				{
					
					p_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["recorddate"]);
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
