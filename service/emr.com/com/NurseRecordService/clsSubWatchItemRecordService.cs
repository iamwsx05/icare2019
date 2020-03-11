using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;


namespace com.digitalwave.clsSubWatchItemRecordService
{
	/// <summary>
	/// ʵ�ֹ۲���Ŀ��¼�༭������м����
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsSubWatchItemRecordService	: clsDiseaseTrackService
	{

		// ��GeneralDiseaseRecord��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
//		private const string c_strGetTimeListSQL;

		// ����ָ��������Ϣ����WatchItemRecord��WatchItemRecordContent,WatchItemRecordAllContent���ұ������ݡ�
		// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������

        private const string c_strGetDeleteRecordContentSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       temperaturexml,
       heartrhythmxml,
       heartfrequencyxml,
       bloodoxygensaturationxml,
       bedsidebloodsugarxml,
       breathxml,
       pulsexml,
       bloodpressuresxml,
       bloodpressureaxml,
       pupilleftxml,
       pupilrightxml,
       echoleftxml,
       echorightxml,
       indxml,
       inixml,
       outuxml,
       outsxml,
       outvxml,
       outexml,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       modifydate,
       modifyuserid,
       temperature_last,
       heartrhythm_last,
       heartfrequency_last,
       bloodoxygensaturation_last,
       bedsidebloodsugar_last,
       breath_last,
       pulse_last,
       bloodpressures_last,
       bloodpressurea_last,
       pupilleft_last,
       pupilright_last,
       echoleft_last,
       echoright_last,
       ind_last,
       ini_last,
       outu_last,
       outs_last,
       outv_last,
       oute_last,
       temperature,
       heartrhythm,
       heartfrequency,
       bloodoxygensaturation,
       bedsidebloodsugar,
       breath,
       pulse,
       bloodpressures,
       bloodpressurea,
       pupilleft,
       pupilright,
       echoleft,
       echoright,
       ind,
       ini,
       outu,
       outs,
       outv,
       oute
  from (select t1.inpatientid,
               t1.inpatientdate,
               t1.opendate,
               t1.createdate,
               t1.createuserid,
               t1.temperaturexml,
               t1.heartrhythmxml,
               t1.heartfrequencyxml,
               t1.bloodoxygensaturationxml,
               t1.bedsidebloodsugarxml,
               t1.breathxml,
               t1.pulsexml,
               t1.bloodpressuresxml,
               t1.bloodpressureaxml,
               t1.pupilleftxml,
               t1.pupilrightxml,
               t1.echoleftxml,
               t1.echorightxml,
               t1.indxml,
               t1.inixml,
               t1.outuxml,
               t1.outsxml,
               t1.outvxml,
               t1.outexml,
               t1.ifconfirm,
               t1.confirmreason,
               t1.confirmreasonxml,
               t1.status,
               t1.deactiveddate,
               t1.deactivedoperatorid,
               t1.firstprintdate,
               t2.modifydate,
               t2.modifyuserid,
               t2.temperature_last,
               t2.heartrhythm_last,
               t2.heartfrequency_last,
               t2.bloodoxygensaturation_last,
               t2.bedsidebloodsugar_last,
               t2.breath_last,
               t2.pulse_last,
               t2.bloodpressures_last,
               t2.bloodpressurea_last,
               t2.pupilleft_last,
               t2.pupilright_last,
               t2.echoleft_last,
               t2.echoright_last,
               t2.ind_last,
               t2.ini_last,
               t2.outu_last,
               t2.outs_last,
               t2.outv_last,
               t2.oute_last,
               t3.temperature,
               t3.heartrhythm,
               t3.heartfrequency,
               t3.bloodoxygensaturation,
               t3.bedsidebloodsugar,
               t3.breath,
               t3.pulse,
               t3.bloodpressures,
               t3.bloodpressurea,
               t3.pupilleft,
               t3.pupilright,
               t3.echoleft,
               t3.echoright,
               t3.ind,
               t3.ini,
               t3.outu,
               t3.outs,
               t3.outv,
               t3.oute
          from watchitemrecord           t1,
               watchitemrecordcontent    t2,
               watchitemrecordallcontent t3
         where t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t1.inpatientid = t3.inpatientid
           and t1.inpatientdate = t3.inpatientdate
           and t1.opendate = t3.opendate
           and t1.status = 1
           and t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?
         order by t2.modifydate desc)
 where rownum = 1";

		// ��GeneralDiseaseRecordDoctor�л�ȡָ������ModifyDate��ҽʦǩ����
//		private string c_strGetDoctorContentSQL;

		// ��GeneralDiseaseRecord�л�ȡָ��ʱ��ı���
		// InPatientID ,InPatientDate ,CreateDate,Status = 0
		private const string c_strCheckCreateDateSQL = @"select createuserid,opendate from watchitemrecord where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		// ��GeneralDiseaseRecord��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
//		private const string c_strGetExistInfoSQL;

		// ��GeneralDiseaseRecordContent��ȡ�޸ı�����Ҫ��Ϣ��
//		private const string c_strGetModifyRecordSQL;

		// ��WatchItemRecord��ȡɾ��������Ҫ��Ϣ��
		private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from watchitemrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		// ��Ӽ�¼��WatchItemRecord
		private const string c_strAddNewRecordSQL = @"insert into watchitemrecord(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,temperaturexml,heartrhythmxml,heartfrequencyxml,bloodoxygensaturationxml,bedsidebloodsugarxml,breathxml,pulsexml,bloodpressuresxml,bloodpressureaxml,pupilleftxml,pupilrightxml,echoleftxml,echorightxml,indxml,inixml,outuxml,outsxml,outvxml,outexml) values(
			?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// ��Ӽ�¼��WatchItemRecordContent
		private const string c_strAddNewRecordContentSQL = @"insert into watchitemrecordcontent(
			inpatientid,inpatientdate,opendate,modifydate,modifyuserid,temperature_last,heartrhythm_last,heartfrequency_last,bloodoxygensaturation_last,bedsidebloodsugar_last,breath_last,pulse_last,bloodpressures_last,bloodpressurea_last,pupilleft_last,pupilright_last,echoleft_last,echoright_last,ind_last,ini_last,outu_last,outs_last,outv_last,oute_last
			) values(
					?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		// ��Ӽ�¼��WatchItemRecordAllContent
		private const string c_strAddNewRecordAllContentSQL = @"insert into watchitemrecordallcontent(
			inpatientid,inpatientdate,opendate,temperature,heartrhythm,heartfrequency,bloodoxygensaturation,bedsidebloodsugar,breath,pulse,bloodpressures,bloodpressurea,pupilleft,pupilright,echoleft,echoright,ind,ini,outu,outs,outv,oute
			) values(
			?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// �޸ļ�¼��WatchItemRecord
		/// </summary>
		private const string c_strModifyRecordSQL= @"update watchitemrecord set temperaturexml=?,heartrhythmxml=?,
heartfrequencyxml=?,bloodoxygensaturationxml=?,bedsidebloodsugarxml=?,breathxml=?,pulsexml=?,bloodpressuresxml=?,
bloodpressureaxml=?,pupilleftxml=?,pupilrightxml=?,echoleftxml=?,echorightxml=?,indxml=?,inixml=?,outuxml=?,outsxml=?,outvxml=?,outexml=?  
where inpatientid=? and inpatientdate=? and opendate=? and status=?";
		/// <summary>
		/// �޸ļ�¼��WatchItemRecordAllContent
		/// </summary>
		private const string c_strModifyRecordAllContentSQL= @"update watchitemrecordallcontent set temperature=?,heartrhythm=?,heartfrequency=?,bloodoxygensaturation=?,bedsidebloodsugar=?,breath=?,pulse=?,bloodpressures=?,bloodpressurea=?,pupilleft=?,pupilright=?,echoleft=?,echoright=?,ind=?,ini=?,outu=?,outs=?,outv=?,oute=?
 where inpatientid=? and inpatientdate=? and opendate=?";

		/// <summary>
		/// �޸ļ�¼��WatchItemRecordContent
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;



		// ��Ӽ�¼��GeneralDiseaseRecordDoctor
//		private const string c_strAddNewRecordDoctorSQL;

		// �޸ļ�¼��GeneralDiseaseRecordContent
//		private const string c_strModifyRecordContentSQL;

		// �޸ļ�¼��GeneralDiseaseRecordDoctor
//		private const string c_strModifyRecordDoctorSQL;

		// ����GeneralDiseaseRecord��ɾ����¼����Ϣ
//		private const string c_strDeleteRecordSQL;

		// ����GeneralDiseaseRecord��FirstPrintDate
//		private const string c_strUpdateFirstPrintDateSQL;

		// ��GeneralDiseaseRecord��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
//		private const string c_strGetDeleteRecordTimeListSQL;

		// ��GeneralDiseaseRecord��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
//		private const string c_strGetDeleteRecordTimeListAllSQL;

		// ��һ�㲡�̼�¼���б��л�ȡָ��������Ϣ
//		private const string c_strGetDeleteRecordContentSQL;

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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubWatchItemRecordService","m_lngGetRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//��ȡIDataParameter����
		
			//��˳���IDataParameter��ֵ
		
			//����DataTable
		
			//ִ�в�ѯ���������DataTable
		
			//��DataTable.Rows�л�ȡ���
		
			//���ý��
		
			//����DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubWatchItemRecordService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//������                              
			if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//��ȡIDataParameter����
		
			//��˳���IDataParameter��ֵ
		
			//ִ��SQL
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubWatchItemRecordService","m_lngGetDeleteRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//��ȡIDataParameter����
		
			//��˳���IDataParameter��ֵ
		
			//����DataTable
		
			//ִ�в�ѯ���������DataTable
		
			//��DataTable.Rows�л�ȡ���
		
			//���ý��
		
			//����DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubWatchItemRecordService","m_lngGetDeleteRecordTimeListAll");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//��ȡIDataParameter����
		
			//��˳���IDataParameter��ֵ
		
			//����DataTable
		
			//ִ�в�ѯ���������DataTable
		
			//��DataTable.Rows�л�ȡ���
		
			//���ý��
		
			//����DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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
		
			//��ȡIDataParameter����
			//			string strSQL = @"select a.*,b.* from GeneralDiseaseRecord a,GeneralDiseaseRecordContent b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
			//						b.ModifyDate=(select Max(ModifyDate) from GeneralDiseaseRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.temperaturexml,
       t1.heartrhythmxml,
       t1.heartfrequencyxml,
       t1.bloodoxygensaturationxml,
       t1.bedsidebloodsugarxml,
       t1.breathxml,
       t1.pulsexml,
       t1.bloodpressuresxml,
       t1.bloodpressureaxml,
       t1.pupilleftxml,
       t1.pupilrightxml,
       t1.echoleftxml,
       t1.echorightxml,
       t1.indxml,
       t1.inixml,
       t1.outuxml,
       t1.outsxml,
       t1.outvxml,
       t1.outexml,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_last,
       t2.heartrhythm_last,
       t2.heartfrequency_last,
       t2.bloodoxygensaturation_last,
       t2.bedsidebloodsugar_last,
       t2.breath_last,
       t2.pulse_last,
       t2.bloodpressures_last,
       t2.bloodpressurea_last,
       t2.pupilleft_last,
       t2.pupilright_last,
       t2.echoleft_last,
       t2.echoright_last,
       t2.ind_last,
       t2.ini_last,
       t2.outu_last,
       t2.outs_last,
       t2.outv_last,
       t2.oute_last,
       t3.temperature,
       t3.heartrhythm,
       t3.heartfrequency,
       t3.bloodoxygensaturation,
       t3.bedsidebloodsugar,
       t3.breath,
       t3.pulse,
       t3.bloodpressures,
       t3.bloodpressurea,
       t3.pupilleft,
       t3.pupilright,
       t3.echoleft,
       t3.echoright,
       t3.ind,
       t3.ini,
       t3.outu,
       t3.outs,
       t3.outv,
       t3.oute
  from watchitemrecord           t1,
       watchitemrecordcontent    t2,
       watchitemrecordallcontent t3
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
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
                    #region ���ý��
                    clsSubWatchItemRecordContent objRecordContent = new clsSubWatchItemRecordContent();
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

                    objRecordContent.m_strTemperature = dtbValue.Rows[0]["TEMPERATURE_LAST"].ToString();
                    objRecordContent.m_strTemperatureAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTemperatureXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    objRecordContent.m_strHeartRhythm = dtbValue.Rows[0]["HEARTRHYTHM_LAST"].ToString();
                    objRecordContent.m_strHeartRhythmAll = dtbValue.Rows[0]["HEARTRHYTHM"].ToString();
                    objRecordContent.m_strHeartRhythmXML = dtbValue.Rows[0]["HEARTRHYTHMXML"].ToString();
                    objRecordContent.m_strHeartFrequency = dtbValue.Rows[0]["HEARTFREQUENCY_LAST"].ToString();
                    objRecordContent.m_strHeartFrequencyAll = dtbValue.Rows[0]["HEARTFREQUENCY"].ToString();
                    objRecordContent.m_strHeartFrequencyXML = dtbValue.Rows[0]["HEARTFREQUENCYXML"].ToString();
                    objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[0]["BLOODOXYGENSATURATION_LAST"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[0]["BLOODOXYGENSATURATION"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[0]["BLOODOXYGENSATURATIONXML"].ToString();
                    objRecordContent.m_strBedsideBloodSugar = dtbValue.Rows[0]["BEDSIDEBLOODSUGAR_LAST"].ToString();
                    objRecordContent.m_strBedsideBloodSugarAll = dtbValue.Rows[0]["BEDSIDEBLOODSUGAR"].ToString();
                    objRecordContent.m_strBedsideBloodSugarXML = dtbValue.Rows[0]["BEDSIDEBLOODSUGARXML"].ToString();
                    objRecordContent.m_strBreath = dtbValue.Rows[0]["BREATH_LAST"].ToString();
                    objRecordContent.m_strBreathAll = dtbValue.Rows[0]["BREATH"].ToString();
                    objRecordContent.m_strBreathXML = dtbValue.Rows[0]["BREATHXML"].ToString();
                    objRecordContent.m_strPulse = dtbValue.Rows[0]["PULSE_LAST"].ToString();
                    objRecordContent.m_strPulseAll = dtbValue.Rows[0]["PULSE"].ToString();
                    objRecordContent.m_strPulseXML = dtbValue.Rows[0]["PULSEXML"].ToString();
                    objRecordContent.m_strBloodPressureS = dtbValue.Rows[0]["BLOODPRESSURES_LAST"].ToString();
                    objRecordContent.m_strBloodPressureSAll = dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
                    objRecordContent.m_strBloodPressureSXML = dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
                    objRecordContent.m_strBloodPressureA = dtbValue.Rows[0]["BLOODPRESSUREA_LAST"].ToString();
                    objRecordContent.m_strBloodPressureAAll = dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
                    objRecordContent.m_strBloodPressureAXML = dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
                    objRecordContent.m_strPupilLeft = dtbValue.Rows[0]["PUPILLEFT_LAST"].ToString();
                    objRecordContent.m_strPupilLeftAll = dtbValue.Rows[0]["PUPILLEFT"].ToString();
                    objRecordContent.m_strPupilLeftXML = dtbValue.Rows[0]["PUPILLEFTXML"].ToString();
                    objRecordContent.m_strPupilRight = dtbValue.Rows[0]["PUPILRIGHT_LAST"].ToString();
                    objRecordContent.m_strPupilRightAll = dtbValue.Rows[0]["PUPILRIGHT"].ToString();
                    objRecordContent.m_strPupilRightXML = dtbValue.Rows[0]["PUPILRIGHTXML"].ToString();
                    objRecordContent.m_strEchoLeft = dtbValue.Rows[0]["ECHOLEFT_LAST"].ToString();
                    objRecordContent.m_strEchoLeftAll = dtbValue.Rows[0]["ECHOLEFT"].ToString();
                    objRecordContent.m_strEchoLeftXML = dtbValue.Rows[0]["ECHOLEFTXML"].ToString();
                    objRecordContent.m_strEchoRight = dtbValue.Rows[0]["ECHORIGHT_LAST"].ToString();
                    objRecordContent.m_strEchoRightAll = dtbValue.Rows[0]["ECHORIGHT"].ToString();
                    objRecordContent.m_strEchoRightXML = dtbValue.Rows[0]["ECHORIGHTXML"].ToString();
                    objRecordContent.m_intInD = Convert.ToInt32(dtbValue.Rows[0]["IND_LAST"]);
                    objRecordContent.m_strInDAll = dtbValue.Rows[0]["IND"].ToString();
                    objRecordContent.m_strInDXML = dtbValue.Rows[0]["INDXML"].ToString();
                    objRecordContent.m_intInI = Convert.ToInt32(dtbValue.Rows[0]["INI_LAST"]);
                    objRecordContent.m_strInIAll = dtbValue.Rows[0]["INI"].ToString();
                    objRecordContent.m_strInIXML = dtbValue.Rows[0]["INIXML"].ToString();
                    objRecordContent.m_intOutU = Convert.ToInt32(dtbValue.Rows[0]["OUTU_LAST"]);
                    objRecordContent.m_strOutUAll = dtbValue.Rows[0]["OUTU"].ToString();
                    objRecordContent.m_strOutUXML = dtbValue.Rows[0]["OUTUXML"].ToString();
                    objRecordContent.m_intOutS = Convert.ToInt32(dtbValue.Rows[0]["OUTS_LAST"]);
                    objRecordContent.m_strOutSAll = dtbValue.Rows[0]["OUTS"].ToString();
                    objRecordContent.m_strOutSXML = dtbValue.Rows[0]["OUTSXML"].ToString();
                    objRecordContent.m_intOutV = Convert.ToInt32(dtbValue.Rows[0]["OUTV_LAST"]);
                    objRecordContent.m_strOutVAll = dtbValue.Rows[0]["OUTV"].ToString();
                    objRecordContent.m_strOutVXML = dtbValue.Rows[0]["OUTVXML"].ToString();
                    objRecordContent.m_intOutE = Convert.ToInt32(dtbValue.Rows[0]["OUTE_LAST"]);
                    objRecordContent.m_strOutEAll = dtbValue.Rows[0]["OUTE"].ToString();
                    objRecordContent.m_strOutEXML = dtbValue.Rows[0]["OUTEXML"].ToString();

                    //				//��˳���IDataParameter��ֵ
                    //				for(int i=0;i<objDPArr.Length;i++)
                    //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    //				objDPArr[0].Value=p_strInPatientID;
                    //				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
                    //				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
                    //				long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL,ref dtbValue,objDPArr);
                    //				//��DataTable.Rows�л�ȡ���
                    //				if(lngRes2 > 0 && dtbValue.Rows.Count >0)
                    //				{
                    //					objRecordContent.m_strGeneralDiseaseDoctorIDArr=new string[dtbValue.Rows.Count];
                    //					objRecordContent.m_strGeneralDiseaseDoctorNameArr=new string[dtbValue.Rows.Count];
                    //					for(int i=0;i<dtbValue.Rows.Count;i++)
                    //					{
                    //						objRecordContent.m_strGeneralDiseaseDoctorIDArr[i]=dtbValue.Rows[i]["EMPLOYEEID"].ToString();
                    //						objRecordContent.m_strGeneralDiseaseDoctorNameArr[i]=dtbValue.Rows[i]["FIRSTNAME"].ToString();
                    //					}
                    //				}

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
		/// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
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
		
			//��ȡIDataParameter����
			//string strSQL = "select CreateUserID,OpenDate from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and CreateDate= ? and Status=0";

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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

            } return lngRes;
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ
                clsSubWatchItemRecordContent objContent = (clsSubWatchItemRecordContent)p_objRecordContent;

                //��ȡIDataParameter����

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[28];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(28, out objDPArr);
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
                objDPArr[9].Value = objContent.m_strTemperatureXML;
                objDPArr[10].Value = objContent.m_strHeartRhythmXML;
                objDPArr[11].Value = objContent.m_strHeartFrequencyXML;
                objDPArr[12].Value = objContent.m_strBloodOxygenSaturationXML;
                objDPArr[13].Value = objContent.m_strBedsideBloodSugarXML;
                objDPArr[14].Value = objContent.m_strBreathXML;
                objDPArr[15].Value = objContent.m_strPulseXML;
                objDPArr[16].Value = objContent.m_strBloodPressureSXML;
                objDPArr[17].Value = objContent.m_strBloodPressureAXML;
                objDPArr[18].Value = objContent.m_strPupilLeftXML;
                objDPArr[19].Value = objContent.m_strPupilRightXML;
                objDPArr[20].Value = objContent.m_strEchoLeftXML;
                objDPArr[21].Value = objContent.m_strEchoRightXML;
                objDPArr[22].Value = objContent.m_strInDXML;
                objDPArr[23].Value = objContent.m_strInIXML;
                objDPArr[24].Value = objContent.m_strOutUXML;
                objDPArr[25].Value = objContent.m_strOutSXML;
                objDPArr[26].Value = objContent.m_strOutVXML;
                objDPArr[27].Value = objContent.m_strOutEXML;

                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[24];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(24, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strTemperature;
                objDPArr2[6].Value = objContent.m_strHeartRhythm;
                objDPArr2[7].Value = objContent.m_strHeartFrequency;
                objDPArr2[8].Value = objContent.m_strBloodOxygenSaturation;
                objDPArr2[9].Value = objContent.m_strBedsideBloodSugar;
                objDPArr2[10].Value = objContent.m_strBreath;
                objDPArr2[11].Value = objContent.m_strPulse;
                objDPArr2[12].Value = objContent.m_strBloodPressureS;
                objDPArr2[13].Value = objContent.m_strBloodPressureA;
                objDPArr2[14].Value = objContent.m_strPupilLeft;
                objDPArr2[15].Value = objContent.m_strPupilRight;
                objDPArr2[16].Value = objContent.m_strEchoLeft;
                objDPArr2[17].Value = objContent.m_strEchoRight;
                objDPArr2[18].Value = objContent.m_intInD;
                objDPArr2[19].Value = objContent.m_intInI;
                objDPArr2[20].Value = objContent.m_intOutU;
                objDPArr2[21].Value = objContent.m_intOutS;
                objDPArr2[22].Value = objContent.m_intOutV;
                objDPArr2[23].Value = objContent.m_intOutE;

                IDataParameter[] objDPArr3 = null;//new Oracle.DataAccess.Client.OracleParameter[22];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr3.Length;i++)
                //				objDPArr3[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(22, out objDPArr3);
                objDPArr3[0].Value = objContent.m_strInPatientID;
                objDPArr3[1].DbType = DbType.DateTime;
                objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                objDPArr3[2].DbType = DbType.DateTime;
                objDPArr3[2].Value = objContent.m_dtmOpenDate;
                objDPArr3[3].Value = objContent.m_strTemperatureAll;
                objDPArr3[4].Value = objContent.m_strHeartRhythmAll;
                objDPArr3[5].Value = objContent.m_strHeartFrequencyAll;
                objDPArr3[6].Value = objContent.m_strBloodOxygenSaturationAll;
                objDPArr3[7].Value = objContent.m_strBedsideBloodSugarAll;
                objDPArr3[8].Value = objContent.m_strBreathAll;
                objDPArr3[9].Value = objContent.m_strPulseAll;
                objDPArr3[10].Value = objContent.m_strBloodPressureSAll;
                objDPArr3[11].Value = objContent.m_strBloodPressureAAll;
                objDPArr3[12].Value = objContent.m_strPupilLeftAll;
                objDPArr3[13].Value = objContent.m_strPupilRightAll;
                objDPArr3[14].Value = objContent.m_strEchoLeftAll;
                objDPArr3[15].Value = objContent.m_strEchoRightAll;
                objDPArr3[16].Value = objContent.m_strInDAll;
                objDPArr3[17].Value = objContent.m_strInIAll;
                objDPArr3[18].Value = objContent.m_strOutUAll;
                objDPArr3[19].Value = objContent.m_strOutSAll;
                objDPArr3[20].Value = objContent.m_strOutVAll;
                objDPArr3[21].Value = objContent.m_strOutEAll;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //ִ��SQL
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //ִ��SQL
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordAllContentSQL, ref lngEff, objDPArr3);
                if (lngRes <= 0) return lngRes;

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
		///  ��WatchItemRecordContent��ȡָ����������޸�ʱ�䡣
		/// </summary>
		string c_strCheckLastModifyRecordSQL=clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from watchitemrecord t1,watchitemrecordcontent t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //��˳���IDataParameter��ֵ

                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
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
            return lngRes;	
		}

		/// <summary>
		///  �����޸ĵ����ݱ��浽���ݿ⡣
		/// </summary>
		/// <param name="p_objRecordContent"></param>
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ
                clsSubWatchItemRecordContent objContent = (clsSubWatchItemRecordContent)p_objRecordContent;

                //��ȡIDataParameter����

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[23];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);
                objDPArr[0].Value = objContent.m_strTemperatureXML;
                objDPArr[1].Value = objContent.m_strHeartRhythmXML;
                objDPArr[2].Value = objContent.m_strHeartFrequencyXML;
                objDPArr[3].Value = objContent.m_strBloodOxygenSaturationXML;
                objDPArr[4].Value = objContent.m_strBedsideBloodSugarXML;
                objDPArr[5].Value = objContent.m_strBreathXML;
                objDPArr[6].Value = objContent.m_strPulseXML;
                objDPArr[7].Value = objContent.m_strBloodPressureSXML;
                objDPArr[8].Value = objContent.m_strBloodPressureAXML;
                objDPArr[9].Value = objContent.m_strPupilLeftXML;
                objDPArr[10].Value = objContent.m_strPupilRightXML;
                objDPArr[11].Value = objContent.m_strEchoLeftXML;
                objDPArr[12].Value = objContent.m_strEchoRightXML;
                objDPArr[13].Value = objContent.m_strInDXML;
                objDPArr[14].Value = objContent.m_strInIXML;
                objDPArr[15].Value = objContent.m_strOutUXML;
                objDPArr[16].Value = objContent.m_strOutSXML;
                objDPArr[17].Value = objContent.m_strOutVXML;
                objDPArr[18].Value = objContent.m_strOutEXML;

                objDPArr[19].Value = objContent.m_strInPatientID;
                objDPArr[20].DbType = DbType.DateTime;
                objDPArr[20].Value = objContent.m_dtmInPatientDate;
                objDPArr[21].DbType = DbType.DateTime;
                objDPArr[21].Value = objContent.m_dtmOpenDate;
                objDPArr[22].Value = 0;


                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[22];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(22, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strTemperatureAll;
                objDPArr2[1].Value = objContent.m_strHeartRhythmAll;
                objDPArr2[2].Value = objContent.m_strHeartFrequencyAll;
                objDPArr2[3].Value = objContent.m_strBloodOxygenSaturationAll;
                objDPArr2[4].Value = objContent.m_strBedsideBloodSugarAll;
                objDPArr2[5].Value = objContent.m_strBreathAll;
                objDPArr2[6].Value = objContent.m_strPulseAll;
                objDPArr2[7].Value = objContent.m_strBloodPressureSAll;
                objDPArr2[8].Value = objContent.m_strBloodPressureAAll;
                objDPArr2[9].Value = objContent.m_strPupilLeftAll;
                objDPArr2[10].Value = objContent.m_strPupilRightAll;
                objDPArr2[11].Value = objContent.m_strEchoLeftAll;
                objDPArr2[12].Value = objContent.m_strEchoRightAll;
                objDPArr2[13].Value = objContent.m_strInDAll;
                objDPArr2[14].Value = objContent.m_strInIAll;
                objDPArr2[15].Value = objContent.m_strOutUAll;
                objDPArr2[16].Value = objContent.m_strOutSAll;
                objDPArr2[17].Value = objContent.m_strOutVAll;
                objDPArr2[18].Value = objContent.m_strOutEAll;

                objDPArr2[19].Value = objContent.m_strInPatientID;
                objDPArr2[20].DbType = DbType.DateTime;
                objDPArr2[20].Value = objContent.m_dtmInPatientDate;
                objDPArr2[21].DbType = DbType.DateTime;
                objDPArr2[21].Value = objContent.m_dtmOpenDate;

                IDataParameter[] objDPArr3 = null;//new Oracle.DataAccess.Client.OracleParameter[24];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr3.Length;i++)
                //				objDPArr3[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(24, out objDPArr3);
                objDPArr3[0].Value = objContent.m_strInPatientID;
                objDPArr3[1].DbType = DbType.DateTime;
                objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                objDPArr3[2].DbType = DbType.DateTime;
                objDPArr3[2].Value = objContent.m_dtmOpenDate;
                objDPArr3[3].DbType = DbType.DateTime;
                objDPArr3[3].Value = objContent.m_dtmModifyDate;
                objDPArr3[4].Value = objContent.m_strModifyUserID;
                objDPArr3[5].Value = objContent.m_strTemperature;
                objDPArr3[6].Value = objContent.m_strHeartRhythm;
                objDPArr3[7].Value = objContent.m_strHeartFrequency;
                objDPArr3[8].Value = objContent.m_strBloodOxygenSaturation;
                objDPArr3[9].Value = objContent.m_strBedsideBloodSugar;
                objDPArr3[10].Value = objContent.m_strBreath;
                objDPArr3[11].Value = objContent.m_strPulse;
                objDPArr3[12].Value = objContent.m_strBloodPressureS;
                objDPArr3[13].Value = objContent.m_strBloodPressureA;
                objDPArr3[14].Value = objContent.m_strPupilLeft;
                objDPArr3[15].Value = objContent.m_strPupilRight;
                objDPArr3[16].Value = objContent.m_strEchoLeft;
                objDPArr3[17].Value = objContent.m_strEchoRight;
                objDPArr3[18].Value = objContent.m_intInD;
                objDPArr3[19].Value = objContent.m_intInI;
                objDPArr3[20].Value = objContent.m_intOutU;
                objDPArr3[21].Value = objContent.m_intOutS;
                objDPArr3[22].Value = objContent.m_intOutV;
                objDPArr3[23].Value = objContent.m_intOutE;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordAllContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr3);
                if (lngRes <= 0) return lngRes;

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
		/// �Ѽ�¼�������С�ɾ������
		///	�ڱ༭�����ز���ʵ�ֱ�����
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
		
			//��ȡIDataParameter����
		
			//��˳���IDataParameter��ֵ
		
			//ִ��SQL
			return (long)enmOperationResult.DB_Succeed;
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
		
			//��ȡIDataParameter����
			//			string strSQL = @"select a.FirstPrintDate,b.ModifyDate from GeneralDiseaseRecord a,GeneralDiseaseRecordContent b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
			//						b.ModifyDate=(select Max(ModifyDate) from GeneralDiseaseRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

		// ��WatchItemRecord��WatchItemRecordContent��ȡModifyDate��FirstPrintDate
		string c_strGetModifyDateAndFirstPrintDateSQL = clsDatabaseSQLConvert.s_StrTop1+@" t1.firstprintdate,t2.modifydate from watchitemrecord t1,watchitemrecordcontent t2
where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
and t1.opendate = t2.opendate and t1.status =0
and ltrim(ttrim(t1.inpatientid)) = ?and t1.inpatientdate = ?and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
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
			string c_strGetDeleteRecordContentSQL = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
                c_strGetDeleteRecordContentSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       temperaturexml,
       heartrhythmxml,
       heartfrequencyxml,
       bloodoxygensaturationxml,
       bedsidebloodsugarxml,
       breathxml,
       pulsexml,
       bloodpressuresxml,
       bloodpressureaxml,
       pupilleftxml,
       pupilrightxml,
       echoleftxml,
       echorightxml,
       indxml,
       inixml,
       outuxml,
       outsxml,
       outvxml,
       outexml,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       modifydate,
       modifyuserid,
       temperature_last,
       heartrhythm_last,
       heartfrequency_last,
       bloodoxygensaturation_last,
       bedsidebloodsugar_last,
       breath_last,
       pulse_last,
       bloodpressures_last,
       bloodpressurea_last,
       pupilleft_last,
       pupilright_last,
       echoleft_last,
       echoright_last,
       ind_last,
       ini_last,
       outu_last,
       outs_last,
       outv_last,
       oute_last,
       temperature,
       heartrhythm,
       heartfrequency,
       bloodoxygensaturation,
       bedsidebloodsugar,
       breath,
       pulse,
       bloodpressures,
       bloodpressurea,
       pupilleft,
       pupilright,
       echoleft,
       echoright,
       ind,
       ini,
       outu,
       outs,
       outv,
       oute
  from (select t1.inpatientid,
               t1.inpatientdate,
               t1.opendate,
               t1.createdate,
               t1.createuserid,
               t1.temperaturexml,
               t1.heartrhythmxml,
               t1.heartfrequencyxml,
               t1.bloodoxygensaturationxml,
               t1.bedsidebloodsugarxml,
               t1.breathxml,
               t1.pulsexml,
               t1.bloodpressuresxml,
               t1.bloodpressureaxml,
               t1.pupilleftxml,
               t1.pupilrightxml,
               t1.echoleftxml,
               t1.echorightxml,
               t1.indxml,
               t1.inixml,
               t1.outuxml,
               t1.outsxml,
               t1.outvxml,
               t1.outexml,
               t1.ifconfirm,
               t1.confirmreason,
               t1.confirmreasonxml,
               t1.status,
               t1.deactiveddate,
               t1.deactivedoperatorid,
               t1.firstprintdate,
               t2.modifydate,
               t2.modifyuserid,
               t2.temperature_last,
               t2.heartrhythm_last,
               t2.heartfrequency_last,
               t2.bloodoxygensaturation_last,
               t2.bedsidebloodsugar_last,
               t2.breath_last,
               t2.pulse_last,
               t2.bloodpressures_last,
               t2.bloodpressurea_last,
               t2.pupilleft_last,
               t2.pupilright_last,
               t2.echoleft_last,
               t2.echoright_last,
               t2.ind_last,
               t2.ini_last,
               t2.outu_last,
               t2.outs_last,
               t2.outv_last,
               t2.oute_last,
               t3.temperature,
               t3.heartrhythm,
               t3.heartfrequency,
               t3.bloodoxygensaturation,
               t3.bedsidebloodsugar,
               t3.breath,
               t3.pulse,
               t3.bloodpressures,
               t3.bloodpressurea,
               t3.pupilleft,
               t3.pupilright,
               t3.echoleft,
               t3.echoright,
               t3.ind,
               t3.ini,
               t3.outu,
               t3.outs,
               t3.outv,
               t3.oute
          from watchitemrecord           t1,
               watchitemrecordcontent    t2,
               watchitemrecordallcontent t3
         where t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t1.inpatientid = t3.inpatientid
           and t1.inpatientdate = t3.inpatientdate
           and t1.opendate = t3.opendate
           and t1.status = 1
           and t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?
         order by t2.modifydate desc)
 where rownum = 1";
			}
			else
			{
                c_strGetDeleteRecordContentSQL = @"select top 1 t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.temperaturexml,
       t1.heartrhythmxml,
       t1.heartfrequencyxml,
       t1.bloodoxygensaturationxml,
       t1.bedsidebloodsugarxml,
       t1.breathxml,
       t1.pulsexml,
       t1.bloodpressuresxml,
       t1.bloodpressureaxml,
       t1.pupilleftxml,
       t1.pupilrightxml,
       t1.echoleftxml,
       t1.echorightxml,
       t1.indxml,
       t1.inixml,
       t1.outuxml,
       t1.outsxml,
       t1.outvxml,
       t1.outexml,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_last,
       t2.heartrhythm_last,
       t2.heartfrequency_last,
       t2.bloodoxygensaturation_last,
       t2.bedsidebloodsugar_last,
       t2.breath_last,
       t2.pulse_last,
       t2.bloodpressures_last,
       t2.bloodpressurea_last,
       t2.pupilleft_last,
       t2.pupilright_last,
       t2.echoleft_last,
       t2.echoright_last,
       t2.ind_last,
       t2.ini_last,
       t2.outu_last,
       t2.outs_last,
       t2.outv_last,
       t2.oute_last,
       t3.temperature,
       t3.heartrhythm,
       t3.heartfrequency,
       t3.bloodoxygensaturation,
       t3.bedsidebloodsugar,
       t3.breath,
       t3.pulse,
       t3.bloodpressures,
       t3.bloodpressurea,
       t3.pupilleft,
       t3.pupilright,
       t3.echoleft,
       t3.echoright,
       t3.ind,
       t3.ini,
       t3.outu,
       t3.outs,
       t3.outv,
       t3.oute
  from watchitemrecord           as t1,
       watchitemrecordcontent    as t2,
       watchitemrecordallcontent as t3
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t2.modifydate desc";
			}
			//��ȡIDataParameter����
			//			string strSQL = @"select a.*,b.* from GeneralDiseaseRecord a,GeneralDiseaseRecordContent b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
			//						b.ModifyDate=(select Max(ModifyDate) from GeneralDiseaseRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
                    clsSubWatchItemRecordContent objRecordContent = new clsSubWatchItemRecordContent();
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

                    objRecordContent.m_strTemperature = dtbValue.Rows[0]["TEMPERATURE_LAST"].ToString();
                    objRecordContent.m_strTemperatureAll = dtbValue.Rows[0]["TEMPERATURE"].ToString();
                    objRecordContent.m_strTemperatureXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
                    objRecordContent.m_strHeartRhythm = dtbValue.Rows[0]["HEARTRHYTHM_LAST"].ToString();
                    objRecordContent.m_strHeartRhythmAll = dtbValue.Rows[0]["HEARTRHYTHM"].ToString();
                    objRecordContent.m_strHeartRhythmXML = dtbValue.Rows[0]["HEARTRHYTHMXML"].ToString();
                    objRecordContent.m_strHeartFrequency = dtbValue.Rows[0]["HEARTFREQUENCY_LAST"].ToString();
                    objRecordContent.m_strHeartFrequencyAll = dtbValue.Rows[0]["HEARTFREQUENCY"].ToString();
                    objRecordContent.m_strHeartFrequencyXML = dtbValue.Rows[0]["HEARTFREQUENCYXML"].ToString();
                    objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[0]["BLOODOXYGENSATURATION_LAST"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[0]["BLOODOXYGENSATURATION"].ToString();
                    objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[0]["BLOODOXYGENSATURATIONXML"].ToString();
                    objRecordContent.m_strBedsideBloodSugar = dtbValue.Rows[0]["BEDSIDEBLOODSUGAR_LAST"].ToString();
                    objRecordContent.m_strBedsideBloodSugarAll = dtbValue.Rows[0]["BEDSIDEBLOODSUGAR"].ToString();
                    objRecordContent.m_strBedsideBloodSugarXML = dtbValue.Rows[0]["BEDSIDEBLOODSUGARXML"].ToString();
                    objRecordContent.m_strBreath = dtbValue.Rows[0]["BREATH_LAST"].ToString();
                    objRecordContent.m_strBreathAll = dtbValue.Rows[0]["BREATH"].ToString();
                    objRecordContent.m_strBreathXML = dtbValue.Rows[0]["BREATHXML"].ToString();
                    objRecordContent.m_strPulse = dtbValue.Rows[0]["PULSE_LAST"].ToString();
                    objRecordContent.m_strPulseAll = dtbValue.Rows[0]["PULSE"].ToString();
                    objRecordContent.m_strPulseXML = dtbValue.Rows[0]["PULSEXML"].ToString();
                    objRecordContent.m_strBloodPressureS = dtbValue.Rows[0]["BLOODPRESSURES_LAST"].ToString();
                    objRecordContent.m_strBloodPressureSAll = dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
                    objRecordContent.m_strBloodPressureSXML = dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
                    objRecordContent.m_strBloodPressureA = dtbValue.Rows[0]["BLOODPRESSUREA_LAST"].ToString();
                    objRecordContent.m_strBloodPressureAAll = dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
                    objRecordContent.m_strBloodPressureAXML = dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
                    objRecordContent.m_strPupilLeft = dtbValue.Rows[0]["PUPILLEFT_LAST"].ToString();
                    objRecordContent.m_strPupilLeftAll = dtbValue.Rows[0]["PUPILLEFT"].ToString();
                    objRecordContent.m_strPupilLeftXML = dtbValue.Rows[0]["PUPILLEFTXML"].ToString();
                    objRecordContent.m_strPupilRight = dtbValue.Rows[0]["PUPILRIGHT_LAST"].ToString();
                    objRecordContent.m_strPupilRightAll = dtbValue.Rows[0]["PUPILRIGHT"].ToString();
                    objRecordContent.m_strPupilRightXML = dtbValue.Rows[0]["PUPILRIGHTXML"].ToString();
                    objRecordContent.m_strEchoLeft = dtbValue.Rows[0]["ECHOLEFT_LAST"].ToString();
                    objRecordContent.m_strEchoLeftAll = dtbValue.Rows[0]["ECHOLEFT"].ToString();
                    objRecordContent.m_strEchoLeftXML = dtbValue.Rows[0]["ECHOLEFTXML"].ToString();
                    objRecordContent.m_strEchoRight = dtbValue.Rows[0]["ECHORIGHT_LAST"].ToString();
                    objRecordContent.m_strEchoRightAll = dtbValue.Rows[0]["ECHORIGHT"].ToString();
                    objRecordContent.m_strEchoRightXML = dtbValue.Rows[0]["ECHORIGHTXML"].ToString();
                    objRecordContent.m_intInD = Convert.ToInt32(dtbValue.Rows[0]["IND_LAST"]);
                    objRecordContent.m_strInDAll = dtbValue.Rows[0]["IND"].ToString();
                    objRecordContent.m_strInDXML = dtbValue.Rows[0]["INDXML"].ToString();
                    objRecordContent.m_intInI = Convert.ToInt32(dtbValue.Rows[0]["INI_LAST"]);
                    objRecordContent.m_strInIAll = dtbValue.Rows[0]["INI"].ToString();
                    objRecordContent.m_strInIXML = dtbValue.Rows[0]["INIXML"].ToString();
                    objRecordContent.m_intOutU = Convert.ToInt32(dtbValue.Rows[0]["OUTU_LAST"]);
                    objRecordContent.m_strOutUAll = dtbValue.Rows[0]["OUTU"].ToString();
                    objRecordContent.m_strOutUXML = dtbValue.Rows[0]["OUTUXML"].ToString();
                    objRecordContent.m_intOutS = Convert.ToInt32(dtbValue.Rows[0]["OUTS_LAST"]);
                    objRecordContent.m_strOutSAll = dtbValue.Rows[0]["OUTS"].ToString();
                    objRecordContent.m_strOutSXML = dtbValue.Rows[0]["OUTSXML"].ToString();
                    objRecordContent.m_intOutV = Convert.ToInt32(dtbValue.Rows[0]["OUTV_LAST"]);
                    objRecordContent.m_strOutVAll = dtbValue.Rows[0]["OUTV"].ToString();
                    objRecordContent.m_strOutVXML = dtbValue.Rows[0]["OUTVXML"].ToString();
                    objRecordContent.m_intOutE = Convert.ToInt32(dtbValue.Rows[0]["OUTE_LAST"]);
                    objRecordContent.m_strOutEAll = dtbValue.Rows[0]["OUTE"].ToString();
                    objRecordContent.m_strOutEXML = dtbValue.Rows[0]["OUTEXML"].ToString();

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

	}
}
