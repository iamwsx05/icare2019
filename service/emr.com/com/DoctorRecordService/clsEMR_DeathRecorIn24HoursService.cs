using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

using com.digitalwave.PublicMiddleTier;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;
using System.Collections;

namespace com.digitalwave.EMR_DeathRecorIn24HoursService
{
	/// <summary>
	/// ClassName:clsEMR_DeathRecordlIn24HoursService
	/// Description:��Ժ24Сʱ��������¼
	/// Author:Jock
	/// Date:05-12-27
	/// </summary>
	/// 

	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsEMR_DeathRecorIn24HoursService : clsDiseaseTrackService
	{
		#region SQL���
		/// <summary>
		/// ��T_EMR_DEATHRECORDIN24HOURS��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// </summary>
		private const string c_strGetTimeListSQL=@"select createdate,opendate 
													from t_emr_deathrecordin24hours 
													where inpatientid = ?
													 and inpatientdate = ?
													 and status=0";

		/// <summary>
		/// ����ָ��������Ϣ����T_EMR_DEATHRECORDIN24HOURS���ұ������ݡ�
		/// </summary>
        private const string c_strGetRecordContentSQL = @"select t.emr_seq,
       t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.createdate,
       t.createuserid,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.status,
       t.representor,
       t.maindescription,
       t.maindescriptionxml,
       t.inhospitalinstance,
       t.inhospitalinstancexml,
       t.inhospitaldiagnose1,
       t.inhospitaldiagnose1xml,
       t.inhospitaldiagnose2,
       t.inhospitaldiagnose2xml,
       t.salvageinstance,
       t.salvageinstancexml,
       t.deathcausation1,
       t.deathcausation1xml,
       t.deathcausation2,
       t.deathcausation2xml,
       t.deathdiagnose1,
       t.deathdiagnose1xml,
       t.deathdiagnose2,
       t.deathdiagnose2xml,
       t.doctorsign,
       t.recorddate,
       t.modifydate,
       t.modifyuserid,
       t.firstprintdate,
       t.outpatientdate,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_deathrecordin24hours d
                 where e.empno_chr = d.doctorsign
                   and e.status_int <> -1
and d.inpatientid = ?
   and d.inpatientdate = ?
   and d.opendate = ?
   and d.status = 0
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.doctorsign
           and rownum = 1) signdocname
  from t_emr_deathrecordin24hours t
 where t.inpatientid = ?
   and t.inpatientdate = ?
   and t.opendate = ?
   and t.status = 0";

		/// <summary>
		/// ��ȡָ��ʱ��ı���
		/// </summary>
		private const string c_strCheckCreateDateSQL=@"select createuserid,opendate from t_emr_deathrecordin24hours where inpatientid = ? and inpatientdate= ? and createdate= ? 
														and status=0";

		/// <summary>
		/// ��T_EMR_DEATHRECORDIN24HOURS��ȡָ����������޸�ʱ�䡣
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select modifydate, modifyuserid from t_emr_deathrecordin24hours where inpatientid = ? and inpatientdate = ? and opendate = ? and status = 0";

		/// <summary>
		/// ��OutHospitalRecord��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL=@"select deactiveddate,deactivedoperatorid from t_emr_deathrecordin24hours where inpatientid = ? and inpatientdate = ? and opendate= ? and status=1 ";

		/// <summary>
		/// ��Ӽ�¼
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into t_emr_deathrecordin24hours (emr_seq,inpatientid,inpatientdate,opendate,createdate,createuserid,status,representor,maindescription,maindescriptionxml,inhospitalinstance,inhospitalinstancexml,inhospitaldiagnose1,inhospitaldiagnose1xml,inhospitaldiagnose2,inhospitaldiagnose2xml,salvageinstance,salvageinstancexml,deathcausation1,deathcausation1xml,deathcausation2,deathcausation2xml,deathdiagnose1,deathdiagnose1xml,deathdiagnose2,deathdiagnose2xml,doctorsign,recorddate,modifydate,modifyuserid)values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
        /// �޸ļ�¼
		/// </summary>
		private const string c_strModifyRecordSQL= @"update t_emr_deathrecordin24hours 
		set representor=?,maindescription=?,maindescriptionxml=?,inhospitalinstance=?,inhospitalinstancexml=?,inhospitaldiagnose1=?,
		inhospitaldiagnose1xml=?,inhospitaldiagnose2=?,inhospitaldiagnose2xml=?,salvageinstance=?,salvageinstancexml=?,deathcausation1=?,
		deathcausation1xml=?,deathcausation2=?,deathcausation2xml=?,deathdiagnose1=?,deathdiagnose1xml=?,
		deathdiagnose2=?,deathdiagnose2xml=?,modifydate=?,modifyuserid=?
		where inpatientid=? 
			and inpatientdate=? 
			and opendate=?
			and status=0";

		/// <summary>
		/// ɾ����¼
		/// </summary>
		private const string c_strDeleteRecordSQL=@"update t_emr_deathrecordin24hours 
													set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid = ? and inpatientdate = ? and opendate=? and status=0";

		/// <summary>
		/// ��ȡLastModifyDate��FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"select firstprintdate, modifydate from t_emr_deathrecordin24hours where a.inpatientid = ? and a.inpatientdate = ? and opendate = ? and status = 0";

		/// <summary>
		/// ����T_EMR_DEATHRECORDIN24HOURS��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL=@"update  t_emr_deathrecordin24hours set firstprintdate= ? where inpatientid = ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		/// <summary>
		/// ��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL=@"select createdate,opendate 
																from t_emr_deathrecordin24hours 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";

		/// <summary>
		/// ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL=@"select createdate,opendate 
																from t_emr_deathrecordin24hours 
																where inpatientid = ? 
																and inpatientdate= ? and status=1";

        private const string c_strGetDeleteRecordContentSQL = @"select t.emr_seq,
       t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.createdate,
       t.createuserid,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.status,
       t.representor,
       t.maindescription,
       t.maindescriptionxml,
       t.inhospitalinstance,
       t.inhospitalinstancexml,
       t.inhospitaldiagnose1,
       t.inhospitaldiagnose1xml,
       t.inhospitaldiagnose2,
       t.inhospitaldiagnose2xml,
       t.salvageinstance,
       t.salvageinstancexml,
       t.deathcausation1,
       t.deathcausation1xml,
       t.deathcausation2,
       t.deathcausation2xml,
       t.deathdiagnose1,
       t.deathdiagnose1xml,
       t.deathdiagnose2,
       t.deathdiagnose2xml,
       t.doctorsign,
       t.recorddate,
       t.modifydate,
       t.modifyuserid,
       t.firstprintdate,
       t.outpatientdate,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_deathrecordin24hours d
                 where e.empno_chr = d.doctorsign
                   and e.status_int <> -1
and d.inpatientid = ?
   and d.inpatientdate = ?
   and d.opendate = ?
   and d.status = 1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.doctorsign
           and rownum = 1) as signdocname
  from t_emr_deathrecordin24hours t
 where t.inpatientid = ?
   and t.inpatientdate = ?
   and t.opendate = ?
   and t.status = 1";
        #endregion

        #region  ��ȡ���˵ĸü�¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
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
                objHRPServ = null;
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
        public override long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
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
        public override long m_lngGetDeleteRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID.Trim();
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
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
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
        public override long m_lngGetDeleteRecordTimeListAll(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
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
                objHRPServ = null;
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
	         clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
			try
			{
				//������
				if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
			
			
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(6,out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID.Trim();
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//��DataTable.Rows�л�ȡ���
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					clsEMR_DeathRecordIn24HoursValue objRecordContent = new clsEMR_DeathRecordIn24HoursValue();
					objRecordContent.m_strInPatientID = p_strInPatientID;
					objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);

					objRecordContent.m_dtmCreateDate=Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
					objRecordContent.m_dtmModifyDate=Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
					
					objRecordContent.m_strCreateUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Convert.ToByte(dtbValue.Rows[0]["STATUS"]);
					objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
					objRecordContent.m_strREPRESENTOR = dtbValue.Rows[0]["REPRESENTOR"].ToString();
					objRecordContent.m_strMAINDESCRIPTION = dtbValue.Rows[0]["MAINDESCRIPTION"].ToString();
					objRecordContent.m_strMAINDESCRIPTIONXML = dtbValue.Rows[0]["MAINDESCRIPTIONXML"].ToString();
					objRecordContent.m_strINHOSPITALINSTANCE = dtbValue.Rows[0]["INHOSPITALINSTANCE"].ToString();
					objRecordContent.m_strINHOSPITALINSTANCEXML = dtbValue.Rows[0]["INHOSPITALINSTANCEXML"].ToString();
					objRecordContent.m_strINHOSPITALDIAGNOSE1 = dtbValue.Rows[0]["INHOSPITALDIAGNOSE1"].ToString();
					objRecordContent.m_strINHOSPITALDIAGNOSE1XML = dtbValue.Rows[0]["INHOSPITALDIAGNOSE1XML"].ToString();
					//objRecordContent.m_strINHOSPITALDIAGNOSE2 = dtbValue.Rows[0]["INHOSPITALDIAGNOSE2"].ToString();
					//objRecordContent.m_strINHOSPITALDIAGNOSE2XML = dtbValue.Rows[0]["INHOSPITALDIAGNOSE2XML"].ToString();
					objRecordContent.m_strSALVAGEINSTANCE = dtbValue.Rows[0]["SALVAGEINSTANCE"].ToString();
					objRecordContent.m_strSALVAGEINSTANCEXML = dtbValue.Rows[0]["SALVAGEINSTANCEXML"].ToString();
					objRecordContent.m_strDEATHCAUSATION1 = dtbValue.Rows[0]["DEATHCAUSATION1"].ToString();
					objRecordContent.m_strDEATHCAUSATION1XML = dtbValue.Rows[0]["DEATHCAUSATION1XML"].ToString();
//					objRecordContent.m_strDEATHCAUSATION2 = dtbValue.Rows[0]["DEATHCAUSATION2"].ToString();
//					objRecordContent.m_strDEATHCAUSATION2XML = dtbValue.Rows[0]["DEATHCAUSATION2XML"].ToString();
					objRecordContent.m_strDEATHDIAGNOSE1 = dtbValue.Rows[0]["DEATHDIAGNOSE1"].ToString();
					objRecordContent.m_strDEATHDIAGNOSE1XML = dtbValue.Rows[0]["DEATHDIAGNOSE1XML"].ToString();
//					objRecordContent.m_strDEATHDIAGNOSE2 = dtbValue.Rows[0]["DEATHDIAGNOSE2"].ToString();
//					objRecordContent.m_strDEATHDIAGNOSE2XML = dtbValue.Rows[0]["DEATHDIAGNOSE2XML"].ToString();
					objRecordContent.m_strDOCTORSIGN = dtbValue.Rows[0]["DOCTORSIGN"].ToString();
					objRecordContent.m_strDOCTORSIGNNAME = dtbValue.Rows[0]["SignDocName"].ToString();
					objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);

					p_objRecordContent = objRecordContent;
				}	
				objHRPServ = null;
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
			//����
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
            clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//������
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
			
				
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmCreateDate;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL,ref dtbValue,objDPArr);
						
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
				objHRPServ = null;
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
			//����
			return lngRes;
		}
		#endregion

		#region �����¼�����ݿ�
		/// <summary>
		/// �����¼�����ݿ⡣
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]	
		protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;
			long m_lngEMR_SEQ = 0;
	        clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//������                              
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;

				
			

				#region ��ȡSequence
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);
                m_lngEMR_SEQ = lngSequence;

                //string strGetSeq = @"select seq_emr.nextval from dual";
                //DataTable dtbResult = new DataTable();
                //lngRes= objHRPServ.DoGetDataTable(strGetSeq,ref dtbResult);

                //if(lngRes>0 && dtbResult.Rows.Count>0)
                //    m_lngEMR_SEQ = Convert.ToInt64(dtbResult.Rows[0][0]);
				#endregion

				//��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
				clsEMR_DeathRecordIn24HoursValue objContent = (clsEMR_DeathRecordIn24HoursValue)p_objRecordContent;
				
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(30,out objDPArr);
				objDPArr[0].Value = m_lngEMR_SEQ;
                objDPArr[1].Value = objContent.m_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmInPatientDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmOpenDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value = objContent.m_dtmCreateDate;
				objDPArr[5].Value = objContent.m_strCreateUserID;
				objDPArr[6].Value = 0;
				objDPArr[7].Value = objContent.m_strREPRESENTOR;
				objDPArr[8].Value = objContent.m_strMAINDESCRIPTION;
				objDPArr[9].Value = objContent.m_strMAINDESCRIPTIONXML;
				objDPArr[10].Value = objContent.m_strINHOSPITALINSTANCE;
				objDPArr[11].Value = objContent.m_strINHOSPITALINSTANCEXML;
				objDPArr[12].Value = objContent.m_strINHOSPITALDIAGNOSE1;
				objDPArr[13].Value = objContent.m_strINHOSPITALDIAGNOSE1XML;
				objDPArr[14].Value = objContent.m_strINHOSPITALDIAGNOSE2;
				objDPArr[15].Value = objContent.m_strINHOSPITALDIAGNOSE2XML;

				objDPArr[16].Value = objContent.m_strSALVAGEINSTANCE;
				objDPArr[17].Value = objContent.m_strSALVAGEINSTANCEXML;
				
				objDPArr[18].Value = objContent.m_strDEATHCAUSATION1;
				objDPArr[19].Value = objContent.m_strDEATHCAUSATION1XML;
				objDPArr[20].Value = objContent.m_strDEATHCAUSATION2;
				objDPArr[21].Value = objContent.m_strDEATHCAUSATION2XML;
				objDPArr[22].Value = objContent.m_strDEATHDIAGNOSE1;
				objDPArr[23].Value = objContent.m_strDEATHDIAGNOSE1XML;
				objDPArr[24].Value = objContent.m_strDEATHDIAGNOSE2;
				objDPArr[25].Value = objContent.m_strDEATHDIAGNOSE2XML;

                objDPArr[26].Value = objContent.m_strDOCTORSIGN;
                objDPArr[27].DbType = DbType.DateTime;
                objDPArr[27].Value = objContent.m_dtmRECORDDATE;
                objDPArr[28].DbType = DbType.DateTime;
				objDPArr[28].Value = objContent.m_dtmModifyDate;
				objDPArr[29].Value = objContent.m_strModifyUserID;

				//ִ��SQL	
				long lngEff = 0;
				lngRes =  objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				objHRPServ = null;
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//����
            finally
            {
                //objHRPServ.Dispose();
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
            clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
			try
			{
				//������
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;
				//����DataTable
				DataTable dtbValue = new DataTable();
				//ִ�в�ѯ���������DataTable
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL,ref dtbValue,objDPArr);
				        
			
				//���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL,ref dtbValue,objDPArr);
					
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
					if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

					//���򣬷���Record_Already_Modify
					p_objModifyInfo=new clsPreModifyInfo();
					p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
					return (long)enmOperationResult.Record_Already_Modify;
				}	
				objHRPServ = null;
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//����
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;	
		}
		#endregion

		#region �����޸ĵ����ݱ��浽���ݿ�
		/// <summary>
		/// �����޸ĵ����ݱ��浽���ݿ⡣
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
                clsEMR_DeathRecordIn24HoursValue objContent = (clsEMR_DeathRecordIn24HoursValue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(24, out objDPArr);

                objDPArr[0].Value = objContent.m_strREPRESENTOR;
                objDPArr[1].Value = objContent.m_strMAINDESCRIPTION;
                objDPArr[2].Value = objContent.m_strMAINDESCRIPTIONXML;
                objDPArr[3].Value = objContent.m_strINHOSPITALINSTANCE;
                objDPArr[4].Value = objContent.m_strINHOSPITALINSTANCEXML;
                objDPArr[5].Value = objContent.m_strINHOSPITALDIAGNOSE1;
                objDPArr[6].Value = objContent.m_strINHOSPITALDIAGNOSE1XML;
                objDPArr[7].Value = objContent.m_strINHOSPITALDIAGNOSE2;
                objDPArr[8].Value = objContent.m_strINHOSPITALDIAGNOSE2XML;

                objDPArr[9].Value = objContent.m_strSALVAGEINSTANCE;
                objDPArr[10].Value = objContent.m_strSALVAGEINSTANCEXML;
                objDPArr[11].Value = objContent.m_strDEATHCAUSATION1;
                objDPArr[12].Value = objContent.m_strDEATHCAUSATION1XML;
                objDPArr[13].Value = objContent.m_strDEATHCAUSATION2;
                objDPArr[14].Value = objContent.m_strDEATHCAUSATION2XML;
                objDPArr[15].Value = objContent.m_strDEATHDIAGNOSE1;
                objDPArr[16].Value = objContent.m_strDEATHDIAGNOSE1XML;
                objDPArr[17].Value = objContent.m_strDEATHDIAGNOSE2;
                objDPArr[18].Value = objContent.m_strDEATHDIAGNOSE2XML;

                objDPArr[19].DbType = DbType.DateTime;
                objDPArr[19].Value = objContent.m_dtmModifyDate;
                objDPArr[20].Value = objContent.m_strModifyUserID;
                objDPArr[21].Value = objContent.m_strInPatientID.Trim();
                objDPArr[22].DbType = DbType.DateTime;
                objDPArr[22].Value = objContent.m_dtmInPatientDate;
                objDPArr[23].DbType = DbType.DateTime;
                objDPArr[23].Value = objContent.m_dtmOpenDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////p_objHRPServ.Dispose();
            }
			//����
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
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID.Trim();
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
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////p_objHRPServ.Dispose();
            }
			//����
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
            try
            {
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
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
                //����
                return lngRes;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////p_objHRPServ.Dispose();
            }
			//����
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
            try
            {
                //������
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(6, out objDPArr);

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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    clsEMR_DeathRecordIn24HoursValue objRecordContent = new clsEMR_DeathRecordIn24HoursValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);

                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Convert.ToByte(dtbValue.Rows[0]["STATUS"]);
                    objRecordContent.m_strDeActivedOperatorID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    if (dtbValue.Rows[0]["DEACTIVEDDATE"] == DBNull.Value)
                        objRecordContent.m_dtmDeActivedDate = DateTime.MinValue;
                    else objRecordContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[0]["DEACTIVEDDATE"]);
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
                    objRecordContent.m_strREPRESENTOR = dtbValue.Rows[0]["REPRESENTOR"].ToString();
                    objRecordContent.m_strMAINDESCRIPTION = dtbValue.Rows[0]["MAINDESCRIPTION"].ToString();
                    objRecordContent.m_strMAINDESCRIPTIONXML = dtbValue.Rows[0]["MAINDESCRIPTIONXML"].ToString();
                    objRecordContent.m_strINHOSPITALINSTANCE = dtbValue.Rows[0]["INHOSPITALINSTANCE"].ToString();
                    objRecordContent.m_strINHOSPITALINSTANCEXML = dtbValue.Rows[0]["INHOSPITALINSTANCEXML"].ToString();
                    objRecordContent.m_strINHOSPITALDIAGNOSE1 = dtbValue.Rows[0]["INHOSPITALDIAGNOSE1"].ToString();
                    objRecordContent.m_strINHOSPITALDIAGNOSE1XML = dtbValue.Rows[0]["INHOSPITALDIAGNOSE1XML"].ToString();
                    objRecordContent.m_strINHOSPITALDIAGNOSE2 = dtbValue.Rows[0]["INHOSPITALDIAGNOSE2"].ToString();
                    objRecordContent.m_strINHOSPITALDIAGNOSE2XML = dtbValue.Rows[0]["INHOSPITALDIAGNOSE2XML"].ToString();
                    objRecordContent.m_strSALVAGEINSTANCE = dtbValue.Rows[0]["SALVAGEINSTANCE"].ToString();
                    objRecordContent.m_strSALVAGEINSTANCEXML = dtbValue.Rows[0]["SALVAGEINSTANCEXML"].ToString();
                    objRecordContent.m_strDEATHCAUSATION1 = dtbValue.Rows[0]["DEATHCAUSATION1"].ToString();
                    objRecordContent.m_strDEATHCAUSATION1XML = dtbValue.Rows[0]["DEATHCAUSATION1XML"].ToString();
                    objRecordContent.m_strDEATHCAUSATION2 = dtbValue.Rows[0]["DEATHCAUSATION2"].ToString();
                    objRecordContent.m_strDEATHCAUSATION2XML = dtbValue.Rows[0]["DEATHCAUSATION2XML"].ToString();
                    objRecordContent.m_strDEATHDIAGNOSE1 = dtbValue.Rows[0]["DEATHDIAGNOSE1"].ToString();
                    objRecordContent.m_strDEATHDIAGNOSE1XML = dtbValue.Rows[0]["DEATHDIAGNOSE1XML"].ToString();
                    objRecordContent.m_strDEATHDIAGNOSE2 = dtbValue.Rows[0]["DEATHDIAGNOSE2"].ToString();
                    objRecordContent.m_strDEATHDIAGNOSE2XML = dtbValue.Rows[0]["DEATHDIAGNOSE2XML"].ToString();
                    objRecordContent.m_strDOCTORSIGN = dtbValue.Rows[0]["DOCTORSIGN"].ToString();
                    objRecordContent.m_strDOCTORSIGNNAME = dtbValue.Rows[0]["SignDocName"].ToString();
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);

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
                ////p_objHRPServ.Dispose();
            }
			//����
			return lngRes;			
		}
		#endregion
	}
}
