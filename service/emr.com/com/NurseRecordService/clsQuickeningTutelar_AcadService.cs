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
	/// ̥���໤����ӡ��޸ģ�
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsQuickeningTutelar_AcadService : clsDiseaseTrackService
	{
		public clsQuickeningTutelar_AcadService()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region SQL���
		/// <summary>
		/// ��ICUACAD_QUICKENINGTUTELARTABLE��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from icuacad_quickeningtutelartable
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// ��ICUACAD_QUICKENINGTUTELARTABLE�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from icuacad_quickeningtutelartable
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// ��ICUACAD_QUICKENINGTUTELARTABLE��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from icuacad_quickeningtutelartable
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// ��Ӽ�¼��ICUACAD_QUICKENINGTUTELARTABLE
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into icuacad_quickeningtutelartable (inpatientid,inpatientdate,opendate,
						createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,pregnantteam_chr,pregnantteam_chrxml,morning_chr,morning_chrxml,midday_chr,midday_chrxml,
						evening_chr,evening_chrxml,quickeningnum_chr,quickeningnum_chrxml) 
						values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// ��Ӽ�¼��ICUACAD_QUICKENTUTELARCONTENT
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into icuacad_quickentutelarcontent (inpatientid,inpatientdate,
						opendate,modifydate,modifyuserid,pregnantteam_chr_right,morning_chr_right,midday_chr_right,evening_chr_right,
						quickeningnum_chr_right) values (?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��ICUACAD_QUICKENINGTUTELARTABLE
		/// </summary>
		private const string c_strModifyRecordSQL= @"update icuacad_quickeningtutelartable 
			set pregnantteam_chr=?,pregnantteam_chrxml=?,morning_chr=?,morning_chrxml=?,midday_chr=?,midday_chrxml=?,
		    evening_chr=?,evening_chrxml=?,quickeningnum_chr=?,quickeningnum_chrxml=?
			where inpatientid=? and inpatientdate=? and status=0 and createdate=?";

		/// <summary>
		/// �޸ļ�¼��ICUACAD_QUICKENINGTUTELARTABLE
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����ICUACAD_QUICKENINGTUTELARTABLE��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update icuacad_quickeningtutelartable
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// ����ICUACAD_QUICKENINGTUTELARTABLE��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update icuacad_quickeningtutelartable
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// ����ICUACAD_QUICKENINGTUTELARTABLE��FirstPrintDate
		/// </summary>
		private const string c_strUpdateALLFirstPrintDateSQL= @"update icuacad_quickeningtutelartable
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?																
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// ��ICUACAD_QUICKENINGTUTELARTABLE��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from icuacad_quickeningtutelartable
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// ��ICUACAD_QUICKENINGTUTELARTABLE��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from icuacad_quickeningtutelartable
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsQuickeningTutelar_AcadService","m_lngGetRecordTimeList");
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
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsQuickeningTutelar_AcadService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsQuickeningTutelar_AcadService","m_lngGetDeleteRecordTimeList");
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

            } return lngRes;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsQuickeningTutelar_AcadService","m_lngGetDeleteRecordTimeListAll");
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
       t1.pregnantteam_chr,
       t1.morning_chr,
       t1.midday_chr,
       t1.evening_chr,
       t1.quickeningnum_chr,
       t1.pregnantteam_chrxml,
       t1.morning_chrxml,
       t1.midday_chrxml,
       t1.evening_chrxml,
       t1.quickeningnum_chrxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.pregnantteam_chr_right,
       t2.morning_chr_right,
       t2.midday_chr_right,
       t2.evening_chr_right,
       t2.quickeningnum_chr_right
  from icuacad_quickeningtutelartable t1
  join icuacad_quickentutelarcontent t2 on (trim(t1.inpatientid) =
                                           trim(t2.inpatientid) and
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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region ���ý��
                    clsQuickeningTutelarValue objRecordContent = new clsQuickeningTutelarValue();
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


                    objRecordContent.m_strPREGNANTTEAM_CHR = dtbValue.Rows[0]["PREGNANTTEAM_CHR"].ToString();
                    objRecordContent.m_strPREGNANTTEAM_CHR_RIGHT = dtbValue.Rows[0]["PREGNANTTEAM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPREGNANTTEAM_CHRXML = dtbValue.Rows[0]["PREGNANTTEAM_CHRXML"].ToString();

                    objRecordContent.m_strMORNING_CHR = dtbValue.Rows[0]["MORNING_CHR"].ToString();
                    objRecordContent.m_strMORNING_CHR_RIGHT = dtbValue.Rows[0]["MORNING_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMORNING_CHRXML = dtbValue.Rows[0]["MORNING_CHRXML"].ToString();

                    objRecordContent.m_strMIDDAY_CHR = dtbValue.Rows[0]["MIDDAY_CHR"].ToString();
                    objRecordContent.m_strMIDDAY_CHR_RIGHT = dtbValue.Rows[0]["MIDDAY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMIDDAY_CHRXML = dtbValue.Rows[0]["MIDDAY_CHRXML"].ToString();

                    objRecordContent.m_strEVENING_CHR = dtbValue.Rows[0]["EVENING_CHR"].ToString();
                    objRecordContent.m_strEVENING_CHR_RIGHT = dtbValue.Rows[0]["EVENING_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEVENING_CHRXML = dtbValue.Rows[0]["EVENING_CHRXML"].ToString();

                    objRecordContent.m_strQUICKENINGNUM_CHR = dtbValue.Rows[0]["QUICKENINGNUM_CHR"].ToString();
                    objRecordContent.m_strQUICKENINGNUM_CHR_RIGHT = dtbValue.Rows[0]["QUICKENINGNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strQUICKENINGNUM_CHRXML = dtbValue.Rows[0]["QUICKENINGNUM_CHRXML"].ToString();



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
			clsQuickeningTutelarValue objRecordContent = (clsQuickeningTutelarValue)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region ��ֵ
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(19, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[4].Value = objRecordContent.m_strCreateUserID;
                objDPArr[5].Value = objRecordContent.m_bytIfConfirm;

                if (objRecordContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objRecordContent.m_strConfirmReason;
                if (objRecordContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objRecordContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;/////////////////////////////////////////


                objDPArr[9].Value = objRecordContent.m_strPREGNANTTEAM_CHR;
                objDPArr[10].Value = objRecordContent.m_strPREGNANTTEAM_CHRXML;
                objDPArr[11].Value = objRecordContent.m_strMORNING_CHR;
                objDPArr[12].Value = objRecordContent.m_strMORNING_CHRXML;
                objDPArr[13].Value = objRecordContent.m_strMIDDAY_CHR;
                objDPArr[14].Value = objRecordContent.m_strMIDDAY_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strEVENING_CHR;
                objDPArr[16].Value = objRecordContent.m_strEVENING_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strQUICKENINGNUM_CHR;
                objDPArr[18].Value = objRecordContent.m_strQUICKENINGNUM_CHRXML;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;




                #region ����ICUACAD_QUICKENINGTUTELARTABLE�в��������
                //		 
                //				string c_strUpdateLaycountSQL= @"Update ICUACAD_QUICKENINGTUTELARTABLE
                //																Set LAYCOUNT_CHR = ?,BEFOREHAND_CHR=?
                //															Where InPatientID = ?
                //																and InPatientDate = ?	
                //																and Status = 0";
                //				IDataParameter[] objDPArr3 = null;
                //				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr3);
                //				objDPArr3[0].Value=objRecordContent.m_strLayCount_chr;
                //				objDPArr3[1].Value=objRecordContent.m_strBeforehand_chr;
                //				objDPArr3[2].Value=objRecordContent.m_strInPatientID;
                //				objDPArr3[3].Value=objRecordContent.m_dtmInPatientDate;
                //
                //				//ִ��SQL
                //				long lngEff3=0;
                //				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strUpdateLaycountSQL,ref lngEff3,objDPArr3);
                //				if(lngRes<=0)return lngRes;
                //
                #endregion

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(10, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[5].Value = objRecordContent.m_strPREGNANTTEAM_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strMORNING_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strMIDDAY_CHR_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strEVENING_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strQUICKENINGNUM_CHR_RIGHT;
                #endregion
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

			clsQuickeningTutelarValue objRecordContent = (clsQuickeningTutelarValue)p_objRecordContent;
			/// <summary>
			/// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from icuacad_quickeningtutelartable t1,icuacad_quickentutelarcontent t2
			where trim(t1.inpatientid) = trim(t2.inpatientid) and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    //if (objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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
		
			clsQuickeningTutelarValue objRecordContent = (clsQuickeningTutelarValue)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region set value
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr);

                objDPArr[0].Value = objRecordContent.m_strPREGNANTTEAM_CHR;
                objDPArr[1].Value = objRecordContent.m_strPREGNANTTEAM_CHRXML;
                objDPArr[2].Value = objRecordContent.m_strMORNING_CHR;
                objDPArr[3].Value = objRecordContent.m_strMORNING_CHRXML;
                //				objDPArr[6].Value=objRecordContent.m_strRecordDate_chr==null?"":objRecordContent.m_strRecordDate_chr;
                //				objDPArr[7].Value=objRecordContent.m_strRecordDate_chrXML==null?"":objRecordContent.m_strRecordDate_chrXML;
                //				objDPArr[8].Value=objRecordContent.m_strTime_chr==null?"":objRecordContent.m_strTime_chr;
                objDPArr[4].Value = objRecordContent.m_strMIDDAY_CHR;
                objDPArr[5].Value = objRecordContent.m_strMIDDAY_CHRXML;
                objDPArr[6].Value = objRecordContent.m_strEVENING_CHR;
                objDPArr[7].Value = objRecordContent.m_strEVENING_CHRXML;
                objDPArr[8].Value = objRecordContent.m_strQUICKENINGNUM_CHR;
                objDPArr[9].Value = objRecordContent.m_strQUICKENINGNUM_CHRXML;
                objDPArr[10].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[12].DbType = DbType.DateTime;
                objDPArr[12].Value = p_objRecordContent.m_dtmCreateDate;

                #endregion
                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                #region ����ICUACAD_QUICKENINGTUTELARTABLE�в��������

                //				string c_strUpdateLaycountSQL= @"Update ICUACAD_QUICKENINGTUTELARTABLE
                //																Set LAYCOUNT_CHR = ?,BEFOREHAND_CHR=?
                //															Where InPatientID = ?
                //																and InPatientDate = ?	
                //																and Status = 0";
                //				IDataParameter[] objDPArr3 = null;
                //				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr3);
                //				objDPArr3[0].Value=objRecordContent.m_strLayCount_chr;
                //				objDPArr3[1].Value=objRecordContent.m_strBeforehand_chr;
                //				objDPArr3[2].Value=objRecordContent.m_strInPatientID;
                //				objDPArr3[3].Value=objRecordContent.m_dtmInPatientDate;
                //
                //				//ִ��SQL
                //				long lngEff3=0;
                //				long lngRes3 =  p_objHRPServ.lngExecuteParameterSQL(c_strUpdateLaycountSQL,ref lngEff3,objDPArr3);
                //				if(lngRes3<=0)return lngRes3;

                #endregion

                #region set value
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(10, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[5].Value = objRecordContent.m_strPREGNANTTEAM_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strMORNING_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strMIDDAY_CHR_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strEVENING_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strQUICKENINGNUM_CHR_RIGHT;
                #endregion
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

			clsQuickeningTutelarValue objRecordContent = (clsQuickeningTutelarValue)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmOpenDate;

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
			/// ��ICUACAD_QUICKENINGTUTELARTABLE��ICUACAD_QUICKENTUTELARCONTENT��ȡLastModifyDate��FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.firstprintdate,b.modifydate from icuacad_quickeningtutelartable a,
					icuacad_quickentutelarcontent b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;


			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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
       t1.pregnantteam_chr,
       t1.morning_chr,
       t1.midday_chr,
       t1.evening_chr,
       t1.quickeningnum_chr,
       t1.pregnantteam_chrxml,
       t1.morning_chrxml,
       t1.midday_chrxml,
       t1.evening_chrxml,
       t1.quickeningnum_chrxml,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from icuacad_quickeningtutelartable t1, icuacad_quickentutelarcontent t2
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
                    #region ���ý��
                    clsQuickeningTutelarValue objRecordContent = new clsQuickeningTutelarValue();
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


                    objRecordContent.m_strPREGNANTTEAM_CHR = dtbValue.Rows[0]["PREGNANTTEAM_CHR"].ToString();
                    objRecordContent.m_strPREGNANTTEAM_CHR_RIGHT = dtbValue.Rows[0]["PREGNANTTEAM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPREGNANTTEAM_CHRXML = dtbValue.Rows[0]["PREGNANTTEAM_CHRXML"].ToString();

                    objRecordContent.m_strMORNING_CHR = dtbValue.Rows[0]["MORNING_CHR"].ToString();
                    objRecordContent.m_strMORNING_CHR_RIGHT = dtbValue.Rows[0]["MORNING_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMORNING_CHRXML = dtbValue.Rows[0]["MORNING_CHRXML"].ToString();

                    objRecordContent.m_strMIDDAY_CHR = dtbValue.Rows[0]["MIDDAY_CHR"].ToString();
                    objRecordContent.m_strMIDDAY_CHR_RIGHT = dtbValue.Rows[0]["MIDDAY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMIDDAY_CHRXML = dtbValue.Rows[0]["MIDDAY_CHRXML"].ToString();

                    objRecordContent.m_strEVENING_CHR = dtbValue.Rows[0]["EVENING_CHR"].ToString();
                    objRecordContent.m_strEVENING_CHR_RIGHT = dtbValue.Rows[0]["EVENING_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEVENING_CHRXML = dtbValue.Rows[0]["EVENING_CHRXML"].ToString();

                    objRecordContent.m_strQUICKENINGNUM_CHR = dtbValue.Rows[0]["QUICKENINGNUM_CHR"].ToString();
                    objRecordContent.m_strQUICKENINGNUM_CHR_RIGHT = dtbValue.Rows[0]["QUICKENINGNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strQUICKENINGNUM_CHRXML = dtbValue.Rows[0]["QUICKENINGNUM_CHRXML"].ToString();
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
			out clsQuickeningTutelarValue[] p_objResultArr)
		{			
			p_objResultArr = new clsQuickeningTutelarValue[0];
			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
	
			long lngRes = 0;
		
			//			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
			//lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS.clsHISMedChargeTypeSvc","m_lngGetMedChargeTypeInfo");
			if(lngRes < 0)//Ȩ��
			{
				return -1;
			}
            string c_strSQL = @"select distinct t1.inpatientid,
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
                t1.pregnantteam_chr,
                t1.morning_chr,
                t1.midday_chr,
                t1.evening_chr,
                t1.quickeningnum_chr,
                t1.pregnantteam_chrxml,
                t1.morning_chrxml,
                t1.midday_chrxml,
                t1.evening_chrxml,
                t1.quickeningnum_chrxml
  from icuacad_quickeningtutelartable t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
 order by t1.createdate";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objResultArr = new clsQuickeningTutelarValue[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region set value
                        p_objResultArr[i1] = new clsQuickeningTutelarValue();

                        p_objResultArr[i1].m_strInPatientID = p_strInPatientID;
                        p_objResultArr[i1].m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                        p_objResultArr[i1].m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[i1]["CREATEDATE"].ToString());

                        p_objResultArr[i1].m_strPREGNANTTEAM_CHR = dtbValue.Rows[i1]["PREGNANTTEAM_CHR"].ToString();

                        p_objResultArr[i1].m_strMORNING_CHR = dtbValue.Rows[i1]["MORNING_CHR"].ToString();

                        p_objResultArr[i1].m_strMIDDAY_CHR = dtbValue.Rows[i1]["MIDDAY_CHR"].ToString();

                        p_objResultArr[i1].m_strEVENING_CHR = dtbValue.Rows[i1]["EVENING_CHR"].ToString();

                        p_objResultArr[i1].m_strQUICKENINGNUM_CHR = dtbValue.Rows[i1]["QUICKENINGNUM_CHR"].ToString();
                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
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
			//			long lngCheckRes =new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsQuickeningTutelar_AcadService","m_lngUpdateALLFirstPrintDate");
			//			//if(lngCheckRes <= 0)
			//				//return lngCheckRes;	

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateALLFirstPrintDateSQL, ref lngEff, objDPArr);

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
		#endregion
	}
}
