using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data; 

namespace com.digitalwave.DiseaseTrackService
{
	// ʵ�������¼���м����
	//��������--��Ժ��¼
[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]	
	public class clsOutHospitalService: clsDiseaseTrackService
	{

		// ��OutHospitalRecord��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		private const string c_strGetTimeListSQL=@"select createdate,opendate 
													from outhospitalrecord 
													where inpatientid = ?
													 and inpatientdate= ?
													 and status=0";

		// ����ָ��������Ϣ����OutHospitalRecord��OutHospitalRecordContent���ұ������ݡ�
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
       a.heartid,
       a.heartidxml,
       a.xrayid,
       a.xrayidxml,
       a.inhospitalcase,
       a.inhospitalcasexml,
       a.inhospitaldiagnose,
       a.inhospitaldiagnosexml,
       a.outhospitaldiagnose,
       a.outhospitaldiagnosexml,
       a.inhospitalby,
       a.inhospitalbyxml,
       a.outhospitalcase,
       a.outhospitalcasexml,
       a.outhospitaladvice,
       a.outhospitaladvicexml,
       b.modifydate,
       b.modifyuserid,
       b.outhospitaldate,
       b.heartid_right,
       b.xrayid_right,
       b.inhospitaldiagnose_right,
       b.outhospitaldiagnose_right,
       b.inhospitalcase_right,
       b.inhospitalby_right,
       b.outhospitalcase_right,
       b.outhospitaladvice_right,
       b.maindoctorid,
       b.doctorid,
       b.maindoctorname,
       b.doctorname
  from outhospitalrecord a, outhospitalrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from outhospitalrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

		//�Ӹ���������Ѳ�ѯ��Ժ���˼�¼
        private const string c_strGetContentSQL_FromRevisit = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.modifydate,
       a.modifyuserid,
       a.outhospitaldate,
       a.heartid_right,
       a.xrayid_right,
       a.inhospitaldiagnose_right,
       a.outhospitaldiagnose_right,
       a.inhospitalcase_right,
       a.inhospitalby_right,
       a.outhospitalcase_right,
       a.outhospitaladvice_right,
       a.maindoctorid,
       a.doctorid,
       a.maindoctorname,
       a.doctorname,
       pbi1.lastname_vchr as maindoctorname1
  from outhospitalrecordcontent a, outhospitalrecord b, t_bse_employee pbi1
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and b.status = 0
   and a.maindoctorid = pbi1.empno_chr
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and a.modifydate = (select max(modifydate)
                         from outhospitalrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		
		// ��OutHospitalRecord�л�ȡָ��ʱ��ı���
		// InPatientID ,InPatientDate ,CreateDate,Status = 0
		private const string c_strCheckCreateDateSQL=@"select createuserid,opendate
														from outhospitalrecord
														where inpatientid = ? 
														and inpatientdate= ?
														and createdate= ? 
														and status=0";
		//
		//		// ��OutHospitalRecord��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		//		private const string c_strGetExistInfoSQL="";

		// ��OutHospitalRecordContent��ȡָ����������޸�ʱ�䡣
		private const string c_strCheckLastModifyRecordSQL= @"select b.modifydate,b.modifyuserid 
															from outhospitalrecord a,outhospitalrecordcontent b 
															where a.inpatientid = ?
															and a.inpatientdate= ? 
															and a.opendate= ?
															and a.status=0
															and b.inpatientid=a.inpatientid
															and b.inpatientdate=a.inpatientdate 
															and b.opendate=a.opendate and
															b.modifydate=(select max(modifydate) from outhospitalrecordcontent 
															where inpatientid=a.inpatientid
															and inpatientdate=a.inpatientdate 
															and opendate=a.opendate)";
		

		//		// ��OutHospitalRecordContent��ȡ�޸ı�����Ҫ��Ϣ��
		//		private const string c_strGetModifyRecordSQL="";

		// ��OutHospitalRecord��ȡɾ��������Ҫ��Ϣ��
		private const string c_strGetDeleteRecordSQL=@"select deactiveddate,deactivedoperatorid 
														from outhospitalrecord 
														where inpatientid = ?
														and inpatientdate= ?
														and opendate= ? 
														and status=1 ";


		// ��Ӽ�¼��OutHospitalRecord
		private const string c_strAddNewRecordSQL= @"insert into  outhospitalrecord
													(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,heartid,heartidxml,xrayid,xrayidxml,inhospitaldiagnose,inhospitaldiagnosexml,outhospitaldiagnose,outhospitaldiagnosexml,inhospitalcase,inhospitalcasexml,inhospitalby,inhospitalbyxml,outhospitalcase,outhospitalcasexml,outhospitaladvice,outhospitaladvicexml) 
													values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// ��Ӽ�¼��OutHospitalRecordContent
		private const string c_strAddNewRecordContentSQL=@"insert into  outhospitalrecordcontent
														(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,outhospitaldate,heartid_right,xrayid_right,inhospitaldiagnose_right,outhospitaldiagnose_right,maindoctorid,inhospitalcase_right,inhospitalby_right,outhospitalcase_right,outhospitaladvice_right,doctorid,doctorname,maindoctorname) 
														values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��OutHospitalRecord
		/// </summary>
		private const string c_strModifyRecordSQL= @"update outhospitalrecord 
													set heartid=?,heartidxml=?,xrayid=?,xrayidxml=?,inhospitaldiagnose=?,inhospitaldiagnosexml=? ,outhospitaldiagnose=?,outhospitaldiagnosexml=? ,inhospitalcase=?,inhospitalcasexml=?,inhospitalby=?,inhospitalbyxml=?,outhospitalcase=? ,outhospitalcasexml=?,outhospitaladvice=?,outhospitaladvicexml=?
													where inpatientid=? 
													and inpatientdate=? 
													and opendate=?
													and status=0";										
														

		// �޸ļ�¼��OutHospitalRecordContent
		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;

		// ����OutHospitalRecord��ɾ����¼����Ϣ
		private const string c_strDeleteRecordSQL=@"update outhospitalrecord 
													set status=1,deactiveddate=?,deactivedoperatorid=? 
													where inpatientid=? 
													and inpatientdate=?
													and opendate=? 
													and status=0";


		// ��OutHospitalRecord��OutHospitalRecordContent��ȡLastModifyDate��FirstPrintDate
		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"select a.firstprintdate,b.modifydate
																	from outhospitalrecord a,outhospitalrecordcontent b 
																	where a.inpatientid = ?
																	and a.inpatientdate= ?
																	and a.opendate= ? 
																	and a.status=0
																	and b.inpatientid=a.inpatientid 
																	and b.inpatientdate=a.inpatientdate 
																	and b.opendate=a.opendate 
																	and b.modifydate=(select max(modifydate) 
																	from outhospitalrecordcontent 
																	where inpatientid=a.inpatientid 
																	and inpatientdate=a.inpatientdate 
																	and opendate=a.opendate)";
						


		// ����OutHospitalRecord��FirstPrintDate
		private const string c_strUpdateFirstPrintDateSQL=@"update  outhospitalrecord 
															set firstprintdate= ? 
															where inpatientid= ? 
															and inpatientdate= ? 
															and opendate=? 
															and firstprintdate is null 
															and status=0";


		// ��OutHospitalRecord��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListSQL=@"select createdate,opendate 
																from outhospitalrecord 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";


		// ��OutHospitalRecord��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		private const string c_strGetDeleteRecordTimeListAllSQL=@"select createdate,opendate 
																from outhospitalrecord 
																where inpatientid = ? 
																and inpatientdate= ? and status=1";


		// �ڳ�Ժ��¼���б��л�ȡָ��������Ϣ��
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
       a.heartid,
       a.heartidxml,
       a.xrayid,
       a.xrayidxml,
       a.inhospitalcase,
       a.inhospitalcasexml,
       a.inhospitaldiagnose,
       a.inhospitaldiagnosexml,
       a.outhospitaldiagnose,
       a.outhospitaldiagnosexml,
       a.inhospitalby,
       a.inhospitalbyxml,
       a.outhospitalcase,
       a.outhospitalcasexml,
       a.outhospitaladvice,
       a.outhospitaladvicexml,
       b.modifydate,
       b.modifyuserid,
       b.outhospitaldate,
       b.heartid_right,
       b.xrayid_right,
       b.inhospitaldiagnose_right,
       b.outhospitaldiagnose_right,
       b.inhospitalcase_right,
       b.inhospitalby_right,
       b.outhospitalcase_right,
       b.outhospitaladvice_right,
       b.maindoctorid,
       b.doctorid,
       b.maindoctorname,
       b.doctorname,
       (select lastname_vchr
          from (select lastname_vchr, empid_chr, isemployee_int, empno_chr
                  from t_bse_employee
                 where empno_chr = b.maindoctorid
                   and status_int <> -1
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = b.maindoctorid
           and rownum = 1) as maindoctorname1,
       (select lastname_vchr
          from (select lastname_vchr, empid_chr, isemployee_int, empno_chr
                  from t_bse_employee
                 where empno_chr = b.doctorid
                   and status_int <> -1
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = b.doctorid
           and rownum = 1) as doctorname1
  from outhospitalrecord a, outhospitalrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from outhospitalrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngGetRecordTimeList");
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
                //����


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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngUpdateFirstPrintDate");
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngGetDeleteRecordTimeList");
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngGetDeleteRecordTimeListAll");
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
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
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

		/// <summary>
		/// ��ȡָ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="objHRPServ"></param>
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
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || objHRPServ == null)
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
                    clsOutHospitalRecordContent objRecordContent = new clsOutHospitalRecordContent();
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

                    if (dtbValue.Rows[0]["OUTHOSPITALDATE"] == DBNull.Value)
                    {
                        objRecordContent.m_dtmOutHospitalDate = DateTime.MinValue;
                    }
                    else
                    {
                        objRecordContent.m_dtmOutHospitalDate = DateTime.Parse(dtbValue.Rows[0]["OUTHOSPITALDATE"].ToString());
                    }
                    
                    objRecordContent.m_strHeartID_Right = dtbValue.Rows[0]["HEARTID_RIGHT"].ToString();
                    objRecordContent.m_strHeartID = dtbValue.Rows[0]["HEARTID"].ToString();
                    objRecordContent.m_strHeartIDXML = dtbValue.Rows[0]["HEARTIDXML"].ToString();
                    objRecordContent.m_strXRayID_Right = dtbValue.Rows[0]["XRAYID_RIGHT"].ToString();
                    objRecordContent.m_strXRayID = dtbValue.Rows[0]["XRAYID"].ToString();
                    objRecordContent.m_strXRayIDXML = dtbValue.Rows[0]["XRAYIDXML"].ToString();
                    objRecordContent.m_strInHospitalDiagnose_Right = dtbValue.Rows[0]["INHOSPITALDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strInHospitalDiagnose = dtbValue.Rows[0]["INHOSPITALDIAGNOSE"].ToString();
                    objRecordContent.m_strInHospitalDiagnoseXML = dtbValue.Rows[0]["INHOSPITALDIAGNOSEXML"].ToString();
                    objRecordContent.m_strOutHospitalDiagnose_Right = dtbValue.Rows[0]["OUTHOSPITALDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strOutHospitalDiagnose = dtbValue.Rows[0]["OUTHOSPITALDIAGNOSE"].ToString();
                    objRecordContent.m_strOutHospitalDiagnoseXML = dtbValue.Rows[0]["OUTHOSPITALDIAGNOSEXML"].ToString();
                    objRecordContent.m_strInHospitalCase_Right = dtbValue.Rows[0]["INHOSPITALCASE_RIGHT"].ToString();
                    objRecordContent.m_strInHospitalCase = dtbValue.Rows[0]["INHOSPITALCASE"].ToString();
                    objRecordContent.m_strInHospitalCaseXML = dtbValue.Rows[0]["INHOSPITALCASEXML"].ToString();
                    objRecordContent.m_strInHospitalBy_Right = dtbValue.Rows[0]["INHOSPITALBY_RIGHT"].ToString();
                    objRecordContent.m_strInHospitalBy = dtbValue.Rows[0]["INHOSPITALBY"].ToString();
                    objRecordContent.m_strInHospitalByXML = dtbValue.Rows[0]["INHOSPITALBYXML"].ToString();
                    objRecordContent.m_strOutHospitalCase_Right = dtbValue.Rows[0]["OUTHOSPITALCASE_RIGHT"].ToString();
                    objRecordContent.m_strOutHospitalCase = dtbValue.Rows[0]["OUTHOSPITALCASE"].ToString();
                    objRecordContent.m_strOutHospitalCaseXML = dtbValue.Rows[0]["OUTHOSPITALCASEXML"].ToString();
                    objRecordContent.m_strOutHospitalAdvice_Right = dtbValue.Rows[0]["OUTHOSPITALADVICE_RIGHT"].ToString();
                    objRecordContent.m_strOutHospitalAdvice = dtbValue.Rows[0]["OUTHOSPITALADVICE"].ToString();
                    objRecordContent.m_strOutHospitalAdviceXML = dtbValue.Rows[0]["OUTHOSPITALADVICEXML"].ToString();

                    objRecordContent.m_strDoctorID = dtbValue.Rows[0]["DOCTORID"].ToString();
                    objRecordContent.m_strDoctorName = dtbValue.Rows[0]["DOCTORNAME"].ToString().Trim();
                    objRecordContent.m_strMainDoctorID = dtbValue.Rows[0]["MAINDOCTORID"].ToString();
                    objRecordContent.m_strMainDoctorName = dtbValue.Rows[0]["MAINDOCTORNAME"].ToString().Trim();

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

            }
			//����
			return lngRes;
			

		}

		/// <summary>
		/// ��Ժ������ø������ѻ�ȡ��Ժ��¼
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strMainDocName"></param>
		/// <param name="p_strDiagnose"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordFromRevisit(string p_strInPatientID,
			string p_strInPatientDate,out string p_strMainDocName,out string p_strDiagnose)
		{
			p_strMainDocName = "";
			p_strDiagnose = "";
			long lngRes = -1;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

			//��ȡIDataParameter����
			IDataParameter[] objDPArr = null;
			objHRPServ.CreateDatabaseParameter(2,out objDPArr);
			//��˳���IDataParameter��ֵ
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
			//����DataTable
			DataTable dtbValue = new DataTable();
            try
            {
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetContentSQL_FromRevisit, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_strMainDocName = dtbValue.Rows[0]["MainDoctorName1"].ToString().Trim();
                    p_strDiagnose = dtbValue.Rows[0]["OutHospitalDiagnose_Right"].ToString();
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
		/// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
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

		/// <summary>
		/// �����¼�����ݿ⡣�������,����ӱ�.
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
                clsOutHospitalRecordContent objContent = (clsOutHospitalRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
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

                objDPArr[9].Value = objContent.m_strHeartID;
                objDPArr[10].Value = objContent.m_strHeartIDXML;
                objDPArr[11].Value = objContent.m_strXRayID;
                objDPArr[12].Value = objContent.m_strXRayIDXML;
                objDPArr[13].Value = objContent.m_strInHospitalDiagnose;
                objDPArr[14].Value = objContent.m_strInHospitalDiagnoseXML;
                objDPArr[15].Value = objContent.m_strOutHospitalDiagnose;
                objDPArr[16].Value = objContent.m_strOutHospitalDiagnoseXML;
                objDPArr[17].Value = objContent.m_strInHospitalCase;
                objDPArr[18].Value = objContent.m_strInHospitalCaseXML;
                objDPArr[19].Value = objContent.m_strInHospitalBy;
                objDPArr[20].Value = objContent.m_strInHospitalByXML;
                objDPArr[21].Value = objContent.m_strOutHospitalCase;
                objDPArr[22].Value = objContent.m_strOutHospitalCaseXML;
                objDPArr[23].Value = objContent.m_strOutHospitalAdvice;
                objDPArr[24].Value = objContent.m_strOutHospitalAdviceXML;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                if (objContent.m_dtmOutHospitalDate != DateTime.MinValue)
                {
                    objDPArr2[5].DbType = DbType.DateTime;
                    objDPArr2[5].Value = objContent.m_dtmOutHospitalDate;
                }
                else
                {
                    objDPArr2[5].Value = DBNull.Value;
                }
                
                objDPArr2[6].Value = objContent.m_strHeartID_Right;
                objDPArr2[7].Value = objContent.m_strXRayID_Right;
                objDPArr2[8].Value = objContent.m_strInHospitalDiagnose_Right;
                objDPArr2[9].Value = objContent.m_strOutHospitalDiagnose_Right;
                objDPArr2[10].Value = objContent.m_strMainDoctorID;
                objDPArr2[11].Value = objContent.m_strInHospitalCase_Right;
                objDPArr2[12].Value = objContent.m_strInHospitalBy_Right;
                objDPArr2[13].Value = objContent.m_strOutHospitalCase_Right;
                objDPArr2[14].Value = objContent.m_strOutHospitalAdvice_Right;
                objDPArr2[15].Value = objContent.m_strDoctorID;
                objDPArr2[16].Value = objContent.m_strDoctorName;
                objDPArr2[17].Value = objContent.m_strMainDoctorName;

                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);


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
		/// <param name="objHRPServ"></param>		
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
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
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from OutHospitalRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
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
		/// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
                clsOutHospitalRecordContent objContent = (clsOutHospitalRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(19, out objDPArr);


                objDPArr[0].Value = objContent.m_strHeartID;
                objDPArr[1].Value = objContent.m_strHeartIDXML;
                objDPArr[2].Value = objContent.m_strXRayID;
                objDPArr[3].Value = objContent.m_strXRayIDXML;
                objDPArr[4].Value = objContent.m_strInHospitalDiagnose;
                objDPArr[5].Value = objContent.m_strInHospitalDiagnoseXML;
                objDPArr[6].Value = objContent.m_strOutHospitalDiagnose;
                objDPArr[7].Value = objContent.m_strOutHospitalDiagnoseXML;
                objDPArr[8].Value = objContent.m_strInHospitalCase;
                objDPArr[9].Value = objContent.m_strInHospitalCaseXML;
                objDPArr[10].Value = objContent.m_strInHospitalBy;
                objDPArr[11].Value = objContent.m_strInHospitalByXML;
                objDPArr[12].Value = objContent.m_strOutHospitalCase;
                objDPArr[13].Value = objContent.m_strOutHospitalCaseXML;
                objDPArr[14].Value = objContent.m_strOutHospitalAdvice;
                objDPArr[15].Value = objContent.m_strOutHospitalAdviceXML;

                objDPArr[16].Value = objContent.m_strInPatientID;
                objDPArr[17].DbType = DbType.DateTime;
                objDPArr[17].Value = objContent.m_dtmInPatientDate;
                objDPArr[18].DbType = DbType.DateTime;
                objDPArr[18].Value = objContent.m_dtmOpenDate;


                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                if (objContent.m_dtmOutHospitalDate != DateTime.MinValue)
                {
                    objDPArr2[5].DbType = DbType.DateTime;
                    objDPArr2[5].Value = objContent.m_dtmOutHospitalDate;
                }
                else
                {
                    objDPArr2[5].Value = DBNull.Value;
                }
                objDPArr2[6].Value = objContent.m_strHeartID_Right;
                objDPArr2[7].Value = objContent.m_strXRayID_Right;
                objDPArr2[8].Value = objContent.m_strInHospitalDiagnose_Right;
                objDPArr2[9].Value = objContent.m_strOutHospitalDiagnose_Right;
                objDPArr2[10].Value = objContent.m_strMainDoctorID;
                objDPArr2[11].Value = objContent.m_strInHospitalCase_Right;
                objDPArr2[12].Value = objContent.m_strInHospitalBy_Right;
                objDPArr2[13].Value = objContent.m_strOutHospitalCase_Right;
                objDPArr2[14].Value = objContent.m_strOutHospitalAdvice_Right;
                objDPArr2[15].Value = objContent.m_strDoctorID;
                objDPArr2[16].Value = objContent.m_strDoctorName;
                objDPArr2[17].Value = objContent.m_strMainDoctorName;


                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);


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
		/// �Ѽ�¼�������С�ɾ������
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);

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
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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
		/// <param name="objHRPServ"></param>
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
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || objHRPServ == null)
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

		/// <summary>
		/// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="objHRPServ"></param>
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
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || objHRPServ == null)
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
                    clsOutHospitalRecordContent objRecordContent = new clsOutHospitalRecordContent();
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

                    if (dtbValue.Rows[0]["OUTHOSPITALDATE"] == DBNull.Value)
                    {
                        objRecordContent.m_dtmOutHospitalDate = DateTime.MinValue;
                    }
                    else
                    {
                        objRecordContent.m_dtmOutHospitalDate = DateTime.Parse(dtbValue.Rows[0]["OUTHOSPITALDATE"].ToString());
                    }
                    objRecordContent.m_strHeartID_Right = dtbValue.Rows[0]["HEARTID_RIGHT"].ToString();
                    objRecordContent.m_strHeartID = dtbValue.Rows[0]["HEARTID"].ToString();
                    objRecordContent.m_strHeartIDXML = dtbValue.Rows[0]["HEARTIDXML"].ToString();
                    objRecordContent.m_strXRayID_Right = dtbValue.Rows[0]["XRAYID_RIGHT"].ToString();
                    objRecordContent.m_strXRayID = dtbValue.Rows[0]["XRAYID"].ToString();
                    objRecordContent.m_strXRayIDXML = dtbValue.Rows[0]["XRAYIDXML"].ToString();
                    objRecordContent.m_strInHospitalDiagnose_Right = dtbValue.Rows[0]["INHOSPITALDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strInHospitalDiagnose = dtbValue.Rows[0]["INHOSPITALDIAGNOSE"].ToString();
                    objRecordContent.m_strInHospitalDiagnoseXML = dtbValue.Rows[0]["INHOSPITALDIAGNOSEXML"].ToString();
                    objRecordContent.m_strOutHospitalDiagnose_Right = dtbValue.Rows[0]["OUTHOSPITALDIAGNOSE_RIGHT"].ToString();
                    objRecordContent.m_strOutHospitalDiagnose = dtbValue.Rows[0]["OUTHOSPITALDIAGNOSE"].ToString();
                    objRecordContent.m_strOutHospitalDiagnoseXML = dtbValue.Rows[0]["OUTHOSPITALDIAGNOSEXML"].ToString();
                    objRecordContent.m_strInHospitalCase_Right = dtbValue.Rows[0]["INHOSPITALCASE_RIGHT"].ToString();
                    objRecordContent.m_strInHospitalCase = dtbValue.Rows[0]["INHOSPITALCASE"].ToString();
                    objRecordContent.m_strInHospitalCaseXML = dtbValue.Rows[0]["INHOSPITALCASEXML"].ToString();
                    objRecordContent.m_strInHospitalBy_Right = dtbValue.Rows[0]["INHOSPITALBY_RIGHT"].ToString();
                    objRecordContent.m_strInHospitalBy = dtbValue.Rows[0]["INHOSPITALBY"].ToString();
                    objRecordContent.m_strInHospitalByXML = dtbValue.Rows[0]["INHOSPITALBYXML"].ToString();
                    objRecordContent.m_strOutHospitalCase_Right = dtbValue.Rows[0]["OUTHOSPITALCASE_RIGHT"].ToString();
                    objRecordContent.m_strOutHospitalCase = dtbValue.Rows[0]["OUTHOSPITALCASE"].ToString();
                    objRecordContent.m_strOutHospitalCaseXML = dtbValue.Rows[0]["OUTHOSPITALCASEXML"].ToString();
                    objRecordContent.m_strOutHospitalAdvice_Right = dtbValue.Rows[0]["OUTHOSPITALADVICE_RIGHT"].ToString();
                    objRecordContent.m_strOutHospitalAdvice = dtbValue.Rows[0]["OUTHOSPITALADVICE"].ToString();
                    objRecordContent.m_strOutHospitalAdviceXML = dtbValue.Rows[0]["OUTHOSPITALADVICEXML"].ToString();

                    objRecordContent.m_strDoctorID = dtbValue.Rows[0]["DOCTORID"].ToString();
                    objRecordContent.m_strDoctorName = dtbValue.Rows[0]["DOCTORNAME1"].ToString().Trim();
                    objRecordContent.m_strMainDoctorID = dtbValue.Rows[0]["MAINDOCTORID"].ToString();
                    objRecordContent.m_strMainDoctorName = dtbValue.Rows[0]["MAINDOCTORNAME1"].ToString().Trim();

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
        /// ���³�Ժʱ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtmOutDate">��Ժʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOutDate(string p_strRegisterID,DateTime p_dtmOutDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"update t_opr_bih_leave t
   set t.outhospital_dat = ?
 where t.registerid_chr = ?
   and t.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmOutDate;
                objDPArr[1].Value = p_strRegisterID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡȫ�����ϼ�¼
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllInactiveInfo(string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSql = @" select a.createdate,
	a.opendate,
	a.deactiveddate,
	b.lastname_vchr as createdusername,
	c.lastname_vchr as deactiveusername
from outhospitalrecord a,t_bse_employee b,t_bse_employee c
where trim(a.createuserid) = b.empno_chr
   and trim(a.deactivedoperatorid) = c.empno_chr
   and a.inpatientid = ?
   and a.inpatientdate = ?
  and a.status = 1
order by a.opendate desc";
                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtExist, objDPArr);
                if (lngRes > 0 && dtExist.Rows.Count > 0)
                {
                    p_objInactiveRecordInfoArr = new clsInactiveRecordInfo_VO[dtExist.Rows.Count];
                    DataRow objRow = null;
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtExist.Rows.Count; i++)
                    {
                        objRow = dtExist.Rows[i];
                        clsInactiveRecordInfo_VO objInfo = new clsInactiveRecordInfo_VO();
                        objInfo.m_StrInpatientId = p_strInpatientId;
                        objInfo.m_DtmInpatientDate = p_dtmInpatientDate;
                        if (DateTime.TryParse(objRow["createdate"].ToString(), out dtmTemp))
                            objInfo.m_DtmCreatedDate = dtmTemp;
                        if (DateTime.TryParse(objRow["opendate"].ToString(), out dtmTemp))
                            objInfo.m_DtmOpenDate = dtmTemp;
                        if (DateTime.TryParse(objRow["deactiveddate"].ToString(), out dtmTemp))
                            objInfo.m_DtmDeactiveDate = dtmTemp;
                        objInfo.m_StrCreatedUser = objRow["createdusername"].ToString();
                        objInfo.m_StrDeactiveUser = objRow["deactiveusername"].ToString();
                        p_objInactiveRecordInfoArr[i] = objInfo;
                    }
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }



	}// END CLASS DEFINITION clsOutHospitalService

}
