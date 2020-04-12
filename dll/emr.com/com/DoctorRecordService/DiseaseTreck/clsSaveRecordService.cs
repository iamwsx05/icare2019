using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.DiseaseTrackService
{
	// ʵ�������¼���м����
	//���̼�¼�������ȼ�¼
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsSaveRecordService	: clsDiseaseTrackService
	{

		
		#region  SQL���
		/// <summary>
		/// ��SaveRecord��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
		private const string c_strGetTimeListSQL= "select CreateDate,OpenDate from SaveRecord Where InPatientID = ? and InPatientDate= ? and Status=0";

		/// <summary>
		/// ����ָ��������Ϣ����SaveRecord��SaveRecordContent���ұ������ݡ�
		/// ��InPatientID ,InPatientDate ,CreateDate,Status = 0����������ѯ�ü�¼�����ݣ�����Max(ModifyDate)��
		/// �������lngRes = 1 && rows = 0,��֤���˼�¼�ѱ�����ɾ������
		/// </summary>
		private const string c_strGetRecordContentSQL= @"select a.*, b.*
																		from SaveRecord a, SaveRecordContent b
																		where a.InPatientID = ?
																		and a.InPatientDate = ?
																		and a.OpenDate = ?
																		and a.Status = 0
																		and b.InPatientID = a.InPatientID
																		and b.InPatientDate = a.InPatientDate
																		and b.OpenDate = a.OpenDate
																		and b.ModifyDate = (select Max(ModifyDate)
																								from SaveRecordContent
																								Where InPatientID = a.InPatientID
																								and InPatientDate = a.InPatientDate
																								and OpenDate = a.OpenDate) ";
		
		/// <summary>
		/// ��SaveRecord�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select CreateUserID,OpenDate from SaveRecord Where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=0";

		//		/// <summary>
		//		/// ��SaveRecord��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ,��ȡ�޸ı�����Ҫ��Ϣ
		//		/// </summary>
		//		private const string c_strGetExistInfoSQL= "";

		/// <summary>
		/// ��SaveRecordContent��ȡָ����������޸�ʱ�䡣
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select b.ModifyDate,b.ModifyUserID from SaveRecord a,SaveRecordContent b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from SaveRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";
		
		//		/// <summary>
		//		/// ��SaveRecordContent��ȡ�޸ı�����Ҫ��Ϣ��
		//		/// </summary>
		//		private const string c_strGetModifyRecordSQL= "";

		/// <summary>
		/// ��SaveRecord��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select DeActivedDate,DeActivedOperatorID from SaveRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";

		/// <summary>
		/// ��Ӽ�¼��SaveRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  SaveRecord(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,IfConfirm,ConfirmReason,ConfirmReasonXML,Status,DiseaseName,DiseaseNameXML,DiseaseChangeCase,DiseaseChangeCaseXML,SaveDeal,SaveDealXML,SaveResult,SaveResultXML,AttendPeople,AttendPeopleXML,SEQUENCE_INT,MARKSTATUS) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// ��Ӽ�¼��SaveRecordContent
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  SaveRecordContent(InPatientID,InPatientDate,OpenDate,ModifyDate,ModifyUserID,SaveTime,DiseaseName_Right,DiseaseChangeCase_Right,SaveDeal_Right,SaveResult_Right,ByDoctorID,AttendPeople_Right) 
				values(?,?,?,?,?,?,?,?,?,?,?,?)";

		
		/// <summary>
		/// �޸ļ�¼��SaveRecord
		/// </summary>
		private const string c_strModifyRecordSQL= "Update SaveRecord Set DiseaseName=?,DiseaseNameXML=? ,DiseaseChangeCase=?,DiseaseChangeCaseXML=?,SaveDeal=?,SaveDealXML=?,SaveResult=?,SaveResultXML=?,AttendPeople=? ,AttendPeopleXml=?,SEQUENCE_INT=? ,MARKSTATUS=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";

		/// <summary>
		/// �޸ļ�¼��SaveRecordContent
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		
		/// <summary>
		/// ����SaveRecord��ɾ����¼����Ϣ
		/// </summary>
		private const string c_strDeleteRecordSQL= "Update SaveRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";

		/// <summary>
		/// ��SaveRecord��SaveRecordContent��ȡLastModifyDate��FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.FirstPrintDate,b.ModifyDate from SaveRecord a,SaveRecordContent b where a.InPatientID = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from SaveRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";


		/// <summary>
		/// ����SaveRecord��FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "Update  SaveRecord Set FirstPrintDate= ? Where InPatientID= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";

		/// <summary>
		/// ��SaveRecord��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select CreateDate,OpenDate from SaveRecord Where InPatientID = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";

		/// <summary>
		/// ��SaveRecord��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select CreateDate,OpenDate from SaveRecord Where InPatientID = ? and InPatientDate= ? and Status=1";

		
		private const string c_strGetDeleteRecordContentSQL= @"select a.*, b.*, PBI.Lastname_Vchr as ByDoctorName
																			from SaveRecord a, SaveRecordContent b, t_bse_employee PBI
																			where a.InPatientID = ?
																			and a.InPatientDate = ?
																			and a.OpenDate = ?
																			and a.Status = 1
																			and b.ByDoctorID = PBI.Empno_Chr
																			and b.InPatientID = a.InPatientID
																			and b.InPatientDate = a.InPatientDate
																			and b.OpenDate = a.OpenDate
																			and b.ModifyDate = (select Max(ModifyDate)
																									from SaveRecordContent
																									Where InPatientID = a.InPatientID
																									and InPatientDate = a.InPatientDate
																									and OpenDate = a.OpenDate) ";
			
		/// <summary>
		/// ��SaveRecordDoctor�л�ȡָ��ModifyDate�Ĳμ����ȵ�ҽ����Ա��
		/// </summary>
		private const string c_strGetDeleteDoctorContentSQL=@"select sub2.SaveDoctorID, PBI.Lastname_Vchr as SaveDoctorName
																	from SaveRecord a, SaveRecordDoctor sub2, t_bse_employee PBI
																	where a.InPatientID = ?
																	and a.InPatientDate = ?
																	and a.OpenDate = ?
																	and a.Status = 1
																	and sub2.SaveDoctorID = PBI.Empno_Chr
																	and sub2.InPatientID = a.InPatientID
																	and sub2.InPatientDate = a.InPatientDate
																	and sub2.OpenDate = a.OpenDate
																	and sub2.ModifyDate = (select Max(ModifyDate)
																								from SaveRecordDoctor
																							Where InPatientID = a.InPatientID
																								and InPatientDate = a.InPatientDate
																								and OpenDate = a.OpenDate) ";
		// ��SaveRecordNoter�л�ȡָ������ModifyDate��SaveRecordDoctor�л�ȡָ������ModifyDate�Ĳμ���Ա��
		private string c_strGetDoctorContentSQL= @"select sub2.SaveDoctorID, PBI.Lastname_Vchr as SaveDoctorName
																		from SaveRecord a, SaveRecordDoctor sub2, t_bse_employee PBI
																		where a.InPatientID = ?
																		and a.InPatientDate = ?
																		and a.OpenDate = ?
																		and a.Status = 0
																		and sub2.SaveDoctorID = PBI.Empno_Chr
																		and sub2.InPatientID= a.InPatientID
																		and sub2.InPatientDate = a.InPatientDate
																		and sub2.OpenDate = a.OpenDate
																		and sub2.ModifyDate = (select Max(ModifyDate)
																									from SaveRecordDoctor
																								Where InPatientID = a.InPatientID
																									and InPatientDate = a.InPatientDate
																									and OpenDate = a.OpenDate) ";

		/// <summary>
		/// ��Ӽ�¼��SaveRecordDoctor
		/// </summary>
		private const string c_strAddNewRecordDoctorSQL= @"insert into  SaveRecordDoctor(InPatientID,InPatientDate,OpenDate,ModifyDate,SaveDoctorID) 
								values(?,?,?,?,?)";
				
		// �޸ļ�¼��SaveRecordDoctor
		private const string c_strModifyRecordDoctorSQL=c_strAddNewRecordDoctorSQL;
		private string GetCheckroomInfo = @"select distinct a.inpatientid,a.firstname,(select max(dept_desc.deptname) from dept_desc where dept_desc.deptid = a.indeptid) as deptname,(select max(inpatient_area_desc.area_name) from inpatient_area_desc where inpatient_area_desc.area_id = a.area_id) as areaname,b.inhospitaldiagnose_right,b.outhospitaldiagnose_right,a.bed_id,a.checkroomdoctorid,a.����ҽʦ,a.����ҽʦ from (
select a.*,b.checkroomdoctorid,c.����ҽʦ,d.����ҽʦ 
  from (select distinct idi.inpatientid, p.firstname, idi.bed_id,idi.indeptid,idi.area_id,ipdi.inpatientdate,ipdi.inpatientenddate
                   from indeptinfo idi,
                        inpatientdateinfo ipdi,
                        patientbaseinfo p
                  where idi.inpatientid = ipdi.inpatientid
                    and p.inpatientid = ipdi.inpatientid
                    and idi.inpatientdate = ipdi.inpatientdate) a left join 
       (select inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,firstprintdate,deactiveddate,deactivedoperatorid,status,patientstate,patientstatexml,diagnose,diagnosexml,differentiatediagnose,differentiatediagnosexml,currentcure,currentcurexml,nextcure,nextcurexml,checkroomdoctorid,checkroomdoctorslist,recorder_id,sequence_int,markstatus
          from checkroomrecord
         where checkroomrecord.status = 1
           and checkroomrecord.checkroomdoctorid in (select b.employeeid
                                                          from role_info a inner join role_employee b on a.role_id =
                                                                                                           b.role_id
                                                                                                    where (   a.role_name =
                                                                                                               '����ҽʦ'
                                                                                                         or a.role_name =
                                                                                                               '����ҽʦ'
                                                                                                        ))) b on a.inpatientid = b.inpatientid left join
(select inpatientid,checkroomdoctorid as ����ҽʦ
from checkroomrecord
where checkroomrecord.status = 1
and checkroomrecord.checkroomdoctorid 
in (select b.employeeid
from role_info a inner join role_employee b on a.role_id =
   b.role_id
   where (   a.role_name =
'����ҽʦ'
))) c on a.inpatientid = c.inpatientid left join
(select inpatientid,checkroomdoctorid as ����ҽʦ
from checkroomrecord
where checkroomrecord.status = 1
and checkroomrecord.checkroomdoctorid 
in (select b.employeeid
from role_info a inner join role_employee b on a.role_id =
   b.role_id
   where (   a.role_name =
'����ҽʦ'
))) d on a.inpatientid = d.inpatientid
 ) a ,outhospitalrecordcontent b
                                            where a.inpatientid = b.inpatientid";
		private string GetCheckroomInfoSQLserver = @"select distinct 
	a.inpatientid,
	a.firstname,(
	select max(dept_desc.deptname) from dept_desc where dept_desc.deptid = a.indeptid) as deptname,
	(select max(inpatient_area_desc.area_name) from inpatient_area_desc where inpatient_area_desc.area_id = a.area_id) as areaname,
	b.inhospitaldiagnose_right,
	b.outhospitaldiagnose_right,
	a.bed_id,
	a.checkroomdoctorid,
	a.����ҽʦ,
	a.����ҽʦ 
from 

	(select a.*,b.checkroomdoctorid,c.����ҽʦ,d.����ҽʦ 
	  	from 
		(select distinct 
			idi.inpatientid, 
			p.firstname, 
			idi.bed_id,
			idi.indeptid,
			idi.area_id,
			ipdi.inpatientdate,
			ipdi.inpatientenddate
                   from indeptinfo idi,
                        inpatientdateinfo ipdi,
                        patientbaseinfo p
                  where idi.inpatientid = ipdi.inpatientid
                    and p.inpatientid = ipdi.inpatientid
                    and idi.inpatientdate = ipdi.inpatientdate
		) a left outer join 
	       (select inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,firstprintdate,deactiveddate,deactivedoperatorid,status,patientstate,patientstatexml,diagnose,diagnosexml,differentiatediagnose,differentiatediagnosexml,currentcure,currentcurexml,nextcure,nextcurexml,checkroomdoctorid,checkroomdoctorslist,recorder_id,sequence_int,markstatus
	        from checkroomrecord
	         where checkroomrecord.status = 1
	           and checkroomrecord.checkroomdoctorid 
		in 
		(
		select b.employeeid
		from role_info a inner join role_employee b on a.role_id =
		   b.role_id
		and (   a.role_name ='����ҽʦ'or a.role_name = '����ҽʦ')
		       
		) 
   		) b on  a.inpatientid = b.inpatientid 
left outer join 
              (select inpatientid,checkroomdoctorid as ����ҽʦ
		from checkroomrecord
		where checkroomrecord.status = 1
		and checkroomrecord.checkroomdoctorid 
		in 
		(
			select b.employeeid
			from role_info a inner join role_employee b on a.role_id =
			   b.role_id
			   and (   a.role_name ='����ҽʦ')
		
		)
		) c on a.inpatientid = c.inpatientid
left outer join 
		(select inpatientid,checkroomdoctorid as ����ҽʦ
			from checkroomrecord
			where checkroomrecord.status = 1
			and checkroomrecord.checkroomdoctorid 
			in 
			(
			select b.employeeid
			from role_info a inner join role_employee b on a.role_id =
			   b.role_id
			   and (   a.role_name ='����ҽʦ')
			)
		
		) d on a.inpatientid = d.inpatientid
		 ) a ,outhospitalrecordcontent b
		                                            where 
								a.inpatientid = b.inpatientid ";
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

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSaveRecordService", "m_lngGetRecordTimeList");
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

            }			//����
			return lngRes;


		}
		[AutoComplete] 
		public long m_lngResGetCheckroomInfo( string filter,out DataTable dtbResult)
		{
			long lngRes = 0;
	dtbResult=null;
    clsHRPTableService objHRPServ = new clsHRPTableService();
    try
    {

        dtbResult = new DataTable(); 
        if (filter != "")
        {
            GetCheckroomInfo += filter;
            GetCheckroomInfoSQLserver += filter;
        }

        if (clsHRPTableService.bytDatabase_Selector == 0)
        {
            lngRes = objHRPServ.DoGetDataTable(GetCheckroomInfoSQLserver, ref dtbResult);
        }
        else
        {
            lngRes = objHRPServ.DoGetDataTable(GetCheckroomInfo, ref dtbResult);
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

    }			//����
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSaveRecordService", "m_lngUpdateFirstPrintDate");
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSaveRecordService", "m_lngGetDeleteRecordTimeList");
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
                string strTmp = objEx.Message;
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSaveRecordService", "m_lngGetDeleteRecordTimeListAll");
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

            }		//����
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
                    clsSaveRecordContent objRecordContent = new clsSaveRecordContent();
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

                    objRecordContent.m_dtmSaveTime = DateTime.Parse(dtbValue.Rows[0]["SAVETIME"].ToString());
                    objRecordContent.m_strDiseaseName_Right = dtbValue.Rows[0]["DISEASENAME_RIGHT"].ToString();
                    objRecordContent.m_strDiseaseName = dtbValue.Rows[0]["DISEASENAME"].ToString();
                    objRecordContent.m_strDiseaseNameXML = dtbValue.Rows[0]["DISEASENAMEXML"].ToString();
                    objRecordContent.m_strDiseaseChangeCase_Right = dtbValue.Rows[0]["DISEASECHANGECASE_RIGHT"].ToString();
                    objRecordContent.m_strDiseaseChangeCase = dtbValue.Rows[0]["DISEASECHANGECASE"].ToString();
                    objRecordContent.m_strDiseaseChangeCaseXML = dtbValue.Rows[0]["DISEASECHANGECASEXML"].ToString();
                    objRecordContent.m_strSaveDeal_Right = dtbValue.Rows[0]["SAVEDEAL_RIGHT"].ToString();
                    objRecordContent.m_strSaveDeal = dtbValue.Rows[0]["SAVEDEAL"].ToString();
                    objRecordContent.m_strSaveDealXML = dtbValue.Rows[0]["SAVEDEALXML"].ToString();
                    objRecordContent.m_strSaveResult_Right = dtbValue.Rows[0]["SAVERESULT_RIGHT"].ToString();
                    objRecordContent.m_strSaveResult = dtbValue.Rows[0]["SAVERESULT"].ToString();
                    objRecordContent.m_strSaveResultXML = dtbValue.Rows[0]["SAVERESULTXML"].ToString();
                    objRecordContent.m_intMarkStatus = int.Parse(dtbValue.Rows[0]["MARKSTATUS"].ToString());

                    #region RegionName
                    //					objRecordContent.m_strByDoctorID=dtbValue.Rows[0]["BYDOCTORID"].ToString();
                    //					objRecordContent.m_strByDoctorName=dtbValue.Rows[0]["BYDOCTORNAME"].ToString();
                    //					objRecordContent.m_strAttendPeople = dtbValue.Rows[0]["ATTENDPEOPLE"].ToString();
                    //					objRecordContent.m_strAttendPeopleXML = dtbValue.Rows[0]["ATTENDPEOPLEXML"].ToString();
                    //					objRecordContent.m_strAttendPeople_Right = dtbValue.Rows[0]["ATTENDPEOPLE_RIGHT"].ToString();
                    //	//				dtbValue = new DataTable();				
                    //��˳���IDataParameter��ֵ
                    //				for(int i=0;i<objDPArr.Length;i++)
                    //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    //				objDPArr[0].Value=p_strInPatientID;
                    //				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
                    //				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
                    //				long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL,ref dtbValue,objDPArr);
                    //				//��DataTable.Rows�л�ȡ���
                    //				if(lngRes2 > 0 && dtbValue.Rows.Count >0)
                    //				{
                    //					objRecordContent.m_strSaveDoctorIDArr=new string[dtbValue.Rows.Count];
                    //					objRecordContent.m_strSaveDoctorNameArr=new string[dtbValue.Rows.Count];
                    //					for(int i=0;i<dtbValue.Rows.Count;i++)
                    //					{
                    //						objRecordContent.m_strSaveDoctorIDArr[i]=dtbValue.Rows[i]["SAVEDOCTORID"].ToString();
                    //						objRecordContent.m_strSaveDoctorNameArr[i]=dtbValue.Rows[i]["SAVEDOCTORNAME"].ToString();
                    //					}
                    //				}
                    #endregion
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

            }			//����
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
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
                string strTmp = objEx.Message;
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsSaveRecordContent objContent = (clsSaveRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(21, out objDPArr);

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

                objDPArr[9].Value = objContent.m_strDiseaseName;
                objDPArr[10].Value = objContent.m_strDiseaseNameXML;
                objDPArr[11].Value = objContent.m_strDiseaseChangeCase;
                objDPArr[12].Value = objContent.m_strDiseaseChangeCaseXML;
                objDPArr[13].Value = objContent.m_strSaveDeal;
                objDPArr[14].Value = objContent.m_strSaveDealXML;
                objDPArr[15].Value = objContent.m_strSaveResult;
                objDPArr[16].Value = objContent.m_strSaveResultXML;
                objDPArr[17].Value = objContent.m_strAttendPeople;
                objDPArr[18].Value = objContent.m_strAttendPeopleXML;
                objDPArr[19].Value = lngSequence;
                objDPArr[20].Value = objContent.m_intMarkStatus;


                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].Value = objContent.m_dtmSaveTime;
                objDPArr2[6].Value = objContent.m_strDiseaseName_Right;
                objDPArr2[7].Value = objContent.m_strDiseaseChangeCase_Right;
                objDPArr2[8].Value = objContent.m_strSaveDeal_Right;
                objDPArr2[9].Value = objContent.m_strSaveResult_Right;
                objDPArr2[10].Value = objContent.m_strByDoctorID;
                objDPArr2[11].Value = objContent.m_strAttendPeople_Right;
                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //			if(objContent.m_strSaveDoctorIDArr!=null)
                //			{						
                //
                //				IDataParameter[] objDPArr3 = new Oracle.DataAccess.Client.OracleParameter[5];
                //				
                //
                //				for(int j=0;j<objContent.m_strSaveDoctorIDArr.Length;j++)
                //				{		
                //					//��˳���IDataParameter��ֵ
                //					for(int i=0;i<objDPArr3.Length;i++)
                //						objDPArr3[i]=new Oracle.DataAccess.Client.OracleParameter();
                //
                //					objDPArr3[0].Value=objContent.m_strInPatientID;
                //					objDPArr3[1].Value=objContent.m_dtmInPatientDate;
                //					objDPArr3[2].Value=objContent.m_dtmOpenDate;
                //					objDPArr3[3].Value=objContent.m_dtmModifyDate;
                //
                //					objDPArr3[4].Value=objContent.m_strSaveDoctorIDArr[j];			
                //			
                //					//ִ��SQL			
                //					lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDoctorSQL,ref lngEff,objDPArr3);
                //					if(lngRes<=0)return lngRes;
                //				}
                //			}

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

            }			//����
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
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    lngRes = (long)enmOperationResult.Record_Already_Delete;
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
                    lngRes = (long)enmOperationResult.Record_Already_Modify;
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

            }			//����
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

                clsSaveRecordContent objContent = (clsSaveRecordContent)p_objRecordContent;



                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                objDPArr[0].Value = objContent.m_strDiseaseName;
                objDPArr[1].Value = objContent.m_strDiseaseNameXML;
                objDPArr[2].Value = objContent.m_strDiseaseChangeCase;
                objDPArr[3].Value = objContent.m_strDiseaseChangeCaseXML;
                objDPArr[4].Value = objContent.m_strSaveDeal;
                objDPArr[5].Value = objContent.m_strSaveDealXML;
                objDPArr[6].Value = objContent.m_strSaveResult;
                objDPArr[7].Value = objContent.m_strSaveResultXML;
                objDPArr[8].Value = objContent.m_strAttendPeople;
                objDPArr[9].Value = objContent.m_strAttendPeopleXML;
                objDPArr[10].Value = lngSequence;
                objDPArr[11].Value = objContent.m_intMarkStatus;

                objDPArr[12].Value = objContent.m_strInPatientID;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = objContent.m_dtmInPatientDate;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = objContent.m_dtmOpenDate;


                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;

                objDPArr2[5].DbType = DbType.DateTime;
                objDPArr2[5].Value = objContent.m_dtmSaveTime;
                objDPArr2[6].Value = objContent.m_strDiseaseName_Right;
                objDPArr2[7].Value = objContent.m_strDiseaseChangeCase_Right;
                objDPArr2[8].Value = objContent.m_strSaveDeal_Right;
                objDPArr2[9].Value = objContent.m_strSaveResult_Right;
                objDPArr2[10].Value = objContent.m_strByDoctorID;
                objDPArr2[11].Value = objContent.m_strAttendPeople_Right;

                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //			if(objContent.m_strSaveDoctorIDArr!=null)
                //			{						
                //
                //				IDataParameter[] objDPArr3 = new Oracle.DataAccess.Client.OracleParameter[5];
                //				
                //
                //				for(int j=0;j<objContent.m_strSaveDoctorIDArr.Length;j++)
                //				{		
                //					//��˳���IDataParameter��ֵ
                //					for(int i=0;i<objDPArr3.Length;i++)
                //						objDPArr3[i]=new Oracle.DataAccess.Client.OracleParameter();
                //
                //					objDPArr3[0].Value=objContent.m_strInPatientID;
                //					objDPArr3[1].Value=objContent.m_dtmInPatientDate;
                //					objDPArr3[2].Value=objContent.m_dtmOpenDate;
                //					objDPArr3[3].Value=objContent.m_dtmModifyDate;
                //
                //					objDPArr3[4].Value=objContent.m_strSaveDoctorIDArr[j];			
                //			
                //					//ִ��SQL			
                //					lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL,ref lngEff,objDPArr3);
                //					if(lngRes<=0)return lngRes;
                //				}
                //			}
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

            }			//����
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
                //string strSQL = "Update SaveRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where InPatientID=? and InPatientDate=? and OpenDate=? and Status=0";


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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

            }			//����
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
                string strTmp = objEx.Message;
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteDoctorContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    clsSaveRecordContent objRecordContent = new clsSaveRecordContent();
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

                    objRecordContent.m_dtmSaveTime = DateTime.Parse(dtbValue.Rows[0]["SAVETIME"].ToString());
                    objRecordContent.m_strDiseaseName_Right = dtbValue.Rows[0]["DISEASENAME_RIGHT"].ToString();
                    objRecordContent.m_strDiseaseName = dtbValue.Rows[0]["DISEASENAME"].ToString();
                    objRecordContent.m_strDiseaseNameXML = dtbValue.Rows[0]["DISEASENAMEXML"].ToString();
                    objRecordContent.m_strDiseaseChangeCase_Right = dtbValue.Rows[0]["DISEASECHANGECASE_RIGHT"].ToString();
                    objRecordContent.m_strDiseaseChangeCase = dtbValue.Rows[0]["DISEASECHANGECASE"].ToString();
                    objRecordContent.m_strDiseaseChangeCaseXML = dtbValue.Rows[0]["DISEASECHANGECASEXML"].ToString();
                    objRecordContent.m_strSaveDeal_Right = dtbValue.Rows[0]["SAVEDEAL_RIGHT"].ToString();
                    objRecordContent.m_strSaveDeal = dtbValue.Rows[0]["SAVEDEAL"].ToString();
                    objRecordContent.m_strSaveDealXML = dtbValue.Rows[0]["SAVEDEALXML"].ToString();
                    objRecordContent.m_strSaveResult_Right = dtbValue.Rows[0]["SAVERESULT_RIGHT"].ToString();
                    objRecordContent.m_strSaveResult = dtbValue.Rows[0]["SAVERESULT"].ToString();
                    objRecordContent.m_strSaveResultXML = dtbValue.Rows[0]["SAVERESULTXML"].ToString();
                    objRecordContent.m_strAttendPeople_Right = dtbValue.Rows[0]["ATTENDPEOPLE_RIGHT"].ToString();
                    objRecordContent.m_strAttendPeople = dtbValue.Rows[0]["ATTENDPEOPLE"].ToString();
                    objRecordContent.m_strAttendPeopleXML = dtbValue.Rows[0]["ATTENDPEOPLEXML"].ToString();
                    objRecordContent.m_strByDoctorID = dtbValue.Rows[0]["BYDOCTORID"].ToString();
                    objRecordContent.m_strByDoctorName = dtbValue.Rows[0]["BYDOCTORNAME"].ToString();
                    //��ȡǩ������
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }
                    //				dtbValue = new DataTable();				
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
                    //					objRecordContent.m_strSaveDoctorIDArr=new string[dtbValue.Rows.Count];
                    //					objRecordContent.m_strSaveDoctorNameArr=new string[dtbValue.Rows.Count];
                    //					for(int i=0;i<dtbValue.Rows.Count;i++)
                    //					{
                    //						objRecordContent.m_strSaveDoctorIDArr[i]=dtbValue.Rows[i]["SAVEDOCTORID"].ToString();
                    //						objRecordContent.m_strSaveDoctorNameArr[i]=dtbValue.Rows[i]["SAVEDOCTORNAME"].ToString();
                    //					}
                    //				}

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

            }			//����
			return lngRes;

			

		}

	}// END CLASS DEFINITION clsGeneralDiseaseService

}
