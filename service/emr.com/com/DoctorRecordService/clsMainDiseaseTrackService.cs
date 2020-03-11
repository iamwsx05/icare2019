using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

using System.Collections;

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// 实现主病程记录的中间件。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]	
	public class clsMainDiseaseTrackService	: com.digitalwave.clsRecordsService.clsRecordsService
	{

		#region 一般病程记录
		private const string c_strUpdateFirstPrintDateSQL_Normal="update  generaldiseaserecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_Normal=@"select b.modifydate,b.modifyuserid from generaldiseaserecord a,generaldiseaserecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from generaldiseaserecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_Normal="select deactiveddate,deactivedoperatorid from generaldiseaserecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_Normal="update generaldiseaserecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_Normal = @"select a.inpatientid,
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
       a.recordtitle,
       a.recordtitletype,
       a.recordcontent,
       a.recordcontentxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
b.pagination
  from generaldiseaserecord a, generaldiseaserecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generaldiseaserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?

		private const string c_strGetDoctorContentSQL_Normal= @"select sub2.employeeid, pbi.lastname_vchr as firstname
																		from generaldiseaserecord       a,
																			generaldiseaserecorddoctor sub2,
																			t_bse_employee           pbi
																		where a.inpatientid = ?
																		and a.inpatientdate = ?
																		and a.opendate = ?
																		and a.status = 0
																		and sub2.employeeid = pbi.empno_chr
																		and sub2.inpatientid = a.inpatientid
																		and sub2.inpatientdate = a.inpatientdate
																		and sub2.opendate = a.opendate
																		and sub2.modifydate = (select max(modifydate)
																									from generaldiseaserecorddoctor
																								where inpatientid = a.inpatientid
																									and inpatientdate = a.inpatientdate
																									and opendate = a.opendate)  ";
		#endregion 一般病程记录

		#region 交班记录
		private const string c_strUpdateFirstPrintDateSQL_HandOver="update  handoverrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_HandOver=@"select b.modifydate,b.modifyuserid from handoverrecord a,handoverrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from handoverrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_HandOver="select deactiveddate,deactivedoperatorid from handoverrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_HandOver="update handoverrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_HandOver = @"select a.inpatientid,
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
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.currentdiagnose,
       a.currentdiagnosexml,
       a.casehistory,
       a.casehistoryxml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.originaldiagnose_right,
       b.currentdiagnose_right,
       b.casehistory_right,
       b.referral_right,b.pagination
  from handoverrecord a, handoverrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from handoverrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?
		#endregion 交班记录
		
		#region 接班记录
		private const string c_strUpdateFirstPrintDateSQL_TakeOver="update  takeoverrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_TakeOver=@"select b.modifydate,b.modifyuserid from takeoverrecord a,takeoverrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from takeoverrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_TakeOver="select deactiveddate,deactivedoperatorid from takeoverrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_TakeOver="update takeoverrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_TakeOver = @"select a.inpatientid,
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
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.currentdiagnose,
       a.currentdiagnosexml,
       a.casehistory,
       a.casehistoryxml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.originaldiagnose_right,
       b.currentdiagnose_right,
       b.casehistory_right,
       b.referral_right,b.pagination
  from takeoverrecord a, takeoverrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from takeoverrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?
		#endregion 接班记录

		#region 会诊记录
		private const string c_strUpdateFirstPrintDateSQL_Consultation="update  consultationrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_Consultation=@"select b.modifydate,b.modifyuserid from consultationrecord a,consultationrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from consultationrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_Consultation="select deactiveddate,deactivedoperatorid from consultationrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_Consultation="update consultationrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_Consultation = @"select a.inpatientid,
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
       a.casehistory,
       a.casehistoryxml,
       a.consultationorder,
       a.consultationorderxml,
       a.consultationidea,
       a.consultationideaxml,
       a.otherhospitalxml,
       a.otherhospital,
       a.hasreplied,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.consultationtime,
       b.applyconsultationdeptid,
       b.askconsultationdeptid,
       b.consultationdeptid,
       b.casehistory_right,
       b.consultationorder_right,
       b.consultationidea_right,
       b.consultationdate,
       b.maindoctorid,
       b.otherhospital_right,
       c1.deptname as applydeptname,
       c2.deptname as askdeptname,
       c3.deptname as deptname,b.pagination
  from consultationrecord        a,
       consultationrecordcontent b,
       dept_desc                 c1,
       dept_desc                 c2,
       dept_desc                 c3
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.applyconsultationdeptid = c1.deptid
   and b.askconsultationdeptid = c2.deptid
   and b.consultationdeptid = c3.deptid
   and b.modifydate = (select max(modifydate)
                         from consultationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)
   and c1.modifydate =
       (select max(modifydate)
          from dept_desc
         where deptid = b.applyconsultationdeptid)";
		private const string c_strGetDoctorContentSQL_Consultation1= @"select sub2.employeeid, pbi.lastname_vchr as firstname
																						from consultationrecord       a,
																							consultationrecorddoctor sub2,
																							t_bse_employee         pbi
																						where a.inpatientid = ?
																						and a.inpatientdate = ?
																						and a.opendate = ?
																						and sub2.employeeflag <> 1
																						and a.status = 0
																						and sub2.employeeid = pbi.empno_chr
																						and sub2.inpatientid = a.inpatientid
																						and sub2.inpatientdate = a.inpatientdate
																						and sub2.opendate = a.opendate
																						and sub2.modifydate = (select max(modifydate)
																													from consultationrecorddoctor
																												where inpatientid = a.inpatientid
																													and inpatientdate = a.inpatientdate
																													and opendate = a.opendate)";
			private const string c_strGetDoctorContentSQL_Consultation2= @"select sub2.employeeid, pbi.lastname_vchrame as firstname
																				from consultationrecord       a,
																					consultationrecorddoctor sub2,
																					t_bse_employee         pbi
																				where a.inpatientid = ?
																				and a.inpatientdate = ?
																				and a.opendate = ?
																				and sub2.employeeflag <> 0
																				and a.status = 0
																				and sub2.employeeid = pbi.empno_chr
																				and sub2.inpatientid = a.inpatientid
																				and sub2.inpatientdate = a.inpatientdate
																				and sub2.opendate = a.opendate
																				and sub2.modifydate = (select max(modifydate)
																											from consultationrecorddoctor
																										where inpatientid = a.inpatientid
																											and inpatientdate = a.inpatientdate
																											and opendate = a.opendate) ";
			#endregion 会诊记录

		#region 病程小结
		private const string c_strUpdateFirstPrintDateSQL_DiseaseSummary="update  diseasesummaryrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_DiseaseSummary=@"select b.modifydate,b.modifyuserid from diseasesummaryrecord a,diseasesummaryrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from diseasesummaryrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_DiseaseSummary="select deactiveddate,deactivedoperatorid from diseasesummaryrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_DiseaseSummary="update diseasesummaryrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_DiseaseSummary = @"select a.inpatientid,
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
       a.inhospitalcase,
       a.inhospitalcasexml,
       a.inhospitaldiagnose,
       a.inhospitaldiagnosexml,
       a.diagnoseby,
       a.diagnosebyxml,
       a.currentcase,
       a.currentcasexml,
       a.currentdiagnose,
       a.currentdiagnosexml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.inhospitalcase_right,
       b.inhospitaldiagnose_right,
       b.diagnoseby_right,
       b.currentcase_right,
       b.currentdiagnose_right,
       b.referral_right,
       b.doctorid,
       b.pagination
  from diseasesummaryrecord a, diseasesummaryrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from diseasesummaryrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?
		#endregion 病程小结

		#region 转出记录
		private const string c_strUpdateFirstPrintDateSQL_Convey="update  conveyrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_Convey=@"select b.modifydate,b.modifyuserid from conveyrecord a,conveyrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from conveyrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_Convey="select deactiveddate,deactivedoperatorid from conveyrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_Convey="update conveyrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_Convey = @"select a.inpatientid,
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
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.conveydiagnose,
       a.conveydiagnosexml,
       a.casehistory,
       a.casehistoryxml,
       a.consultation,
       a.consultationxml,
       a.conveyreason,
       a.conveyreasonxml,
       a.notice,
       a.noticexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.originaldiagnose_right,
       b.conveydiagnose_right,
       b.casehistory_right,
       b.consultation_right,
       b.conveyreason_right,
       b.notice_right,
       b.maindoctorid,b.pagination
  from conveyrecord a, conveyrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from conveyrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		#endregion 转出记录

		#region 转入记录
		private const string c_strUpdateFirstPrintDateSQL_TurnIn="update  turninrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_TurnIn=@"select b.modifydate,b.modifyuserid from turninrecord a,turninrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from turninrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_TurnIn="select deactiveddate,deactivedoperatorid from turninrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_TurnIn="update turninrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_TurnIn = @"select a.inpatientid,
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
       a.casebeforeturnin,
       a.casebeforeturninxml,
       a.turninreason,
       a.turninreasonxml,
       a.caseafterturnin,
       a.caseafterturninxml,
       a.turnindiagnose,
       a.turnindiagnosexml,
       a.referral,
       a.referralxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.casebeforeturnin_right,
       b.turninreason_right,
       b.caseafterturnin_right,
       b.turnindiagnose_right,
       b.referral_right,b.pagination
  from turninrecord a, turninrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from turninrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?
		#endregion 转入记录

		#region 查房记录
		private const string c_strUpdateFirstPrintDateSQL_CheckRoom=@"update checkroomrecord
																		set firstprintdate = ?
																		where inpatientid = ?
																		and inpatientdate = ?
																		and opendate = ?
																		and firstprintdate is null
																		and status = 0";

		private const string c_strCheckLastModifyRecordSQL_CheckRoom=@"select b.modifydate,b.modifyuserid from checkroomrecord a,checkroomrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from checkroomrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_CheckRoom="select deactiveddate,deactivedoperatorid from checkroomrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_CheckRoom="update checkroomrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_CheckRoom = @"select a.inpatientid,
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
       a.patientstate,
       a.patientstatexml,
       a.diagnose,
       a.diagnosexml,
       a.differentiatediagnose,
       a.differentiatediagnosexml,
       a.currentcure,
       a.currentcurexml,
       a.nextcure,
       a.nextcurexml,
       a.checkroomdoctorid,
       a.checkroomdoctorslist,
       a.recorder_id,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.patientstate_right,
       b.diagnose_right,
       b.differentiatediagnose_right,
       b.currentcure_right,
       b.nextcure_right,
       b.checkroomdoctorid,
       b.checkroomdoctorslist,b.pagination
  from checkroomrecord a, checkroomrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from checkroomrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
 
		#endregion 查房记录

		#region 病例讨论记录
		private const string c_strUpdateFirstPrintDateSQL_CaseDiscuss="update  casediscussrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_CaseDiscuss=@"select b.modifydate,b.modifyuserid from casediscussrecord a,casediscussrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from casediscussrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_CaseDiscuss="select deactiveddate,deactivedoperatorid from casediscussrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_CaseDiscuss="update casediscussrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_CaseDiscuss = @"select a.inpatientid,
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
       a.address,
       a.addressxml,
       a.discusscontent,
       a.discusscontentxml,
       a.sequence_int,
       a.markstatus, a.opendate as opendate_main, b.modifydate,
       b.modifyuserid,
       b.address_right,
       b.discusscontent_right,
       b.compereid,b.pagination
  from casediscussrecord a, casediscussrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from casediscussrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		// 在CaseDiscussRecordAttendee中获取指定表单和ModifyDate的参加人员。
		private string c_strGetAttendeeContentSQL_CaseDiscuss= @"select sub2.attendeeid, pbi.lastname_vchr as attendeename
																			from casediscussrecord         a,
																				casediscussrecordattendee sub2,
																				t_bse_employee          pbi
																			where a.inpatientid = ?
																			and a.inpatientdate = ?
																			and a.opendate = ?
																			and a.status = 0
																			and sub2.attendeeid = pbi.empno_chr
																			and sub2.inpatientid = a.inpatientid
																			and sub2.inpatientdate = a.inpatientdate
																			and sub2.opendate = a.opendate
																			and sub2.modifydate = (select max(modifydate)
																										from casediscussrecordattendee
																									where inpatientid = a.inpatientid
																										and inpatientdate = a.inpatientdate
																										and opendate = a.opendate) ";
		// 在CaseDiscussRecordNoter中获取指定表单和ModifyDate的记录者。
		private string c_strGetNoterContentSQL_CaseDiscuss= @"select sub2.noterid, pbi.lastname_vchr as notername
																	from casediscussrecord      a,
																		casediscussrecordnoter sub2,
																		t_bse_employee       pbi
																	where a.inpatientid = ?
																	and a.inpatientdate = ?
																	and a.opendate = ?
																	and a.status = 0
																	and sub2.noterid = pbi.empno_chr
																	and sub2.inpatientid = a.inpatientid
																	and sub2.inpatientdate = a.inpatientdate
																	and sub2.opendate = a.opendate
																	and sub2.modifydate = (select max(modifydate)
																								from casediscussrecordnoter
																							where inpatientid = a.inpatientid
																								and inpatientdate = a.inpatientdate
																								and opendate = a.opendate) ";
		#endregion 病例讨论记录

		#region 术前讨论记录
		private const string c_strUpdateFirstPrintDateSQL_BeforeOperationDiscuss="update  beforeoperationdiscussrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_BeforeOperationDiscuss=@"select b.modifydate,b.modifyuserid from beforeoperationdiscussrecord a,bfoprdiscussreccont b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from bfoprdiscussreccont where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_BeforeOperationDiscuss="select deactiveddate,deactivedoperatorid from beforeoperationdiscussrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_BeforeOperationDiscuss="update beforeoperationdiscussrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_BeforeOperationDiscuss = @"select a.inpatientid,
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
       a.address,
       a.addressxml,
       a.discusscontent,
       a.discusscontentxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.address_right,
       b.discusscontent_right,
       b.compereid,b.pagination
  from beforeoperationdiscussrecord a, bfoprdiscussreccont b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from bfoprdiscussreccont
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?
		// 在bfoprdiscussrecattendee中获取指定表单和ModifyDate的参加人员。
		private string c_strGetAttendeeContentSQL_BeforeOperationDiscuss= @"select sub2.attendeeid, pbi.lastname_vchr as attendeename
																					from beforeoperationdiscussrecord a,
																						bfoprdiscussrecattendee      sub2,
																						t_bse_employee             pbi
																					where a.inpatientid = ?
																					and a.inpatientdate = ?
																					and a.opendate = ?
																					and a.status = 0
																					and sub2.attendeeid = pbi.empno_chr
																					and sub2.inpatientid = a.inpatientid
																					and sub2.inpatientdate = a.inpatientdate
																					and sub2.opendate = a.opendate
																					and sub2.modifydate = (select max(modifydate)
																												from bfoprdiscussrecattendee
																											where inpatientid = a.inpatientid
																												and inpatientdate = a.inpatientdate
																												and opendate = a.opendate) ";
		// 在BfOprDiscussRecNoter中获取指定表单和ModifyDate的记录者。
		private string c_strGetNoterContentSQL_BeforeOperationDiscuss= @"select sub2.noterid, pbi.lastname_vchr as notername
																			from beforeoperationdiscussrecord a,
																				bfoprdiscussrecnoter         sub2,
																				t_bse_employee             pbi
																			where a.inpatientid = ?
																			and a.inpatientdate = ?
																			and a.opendate = ?
																			and a.status = 0
																			and sub2.noterid = pbi.empno_chr
																			and sub2.inpatientid = a.inpatientid
																			and sub2.inpatientdate = a.inpatientdate
																			and sub2.opendate = a.opendate
																			and sub2.modifydate = (select max(modifydate)
																										from bfoprdiscussrecnoter
																									where inpatientid = a.inpatientid
																										and inpatientdate = a.inpatientdate
																										and opendate = a.opendate) ";
		#endregion 术前讨论记录

		#region 死亡病例讨论记录
		private const string c_strUpdateFirstPrintDateSQL_DeadCaseDiscuss="update  deadcasediscussrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_DeadCaseDiscuss=@"select b.modifydate,b.modifyuserid from deadcasediscussrecord a,deadcasediscussrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from deadcasediscussrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_DeadCaseDiscuss="select deactiveddate,deactivedoperatorid from deadcasediscussrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_DeadCaseDiscuss="update deadcasediscussrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_DeadCaseDiscuss = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.address,
       a.addressxml,
       a.discusscontent,
       a.discusscontentxml,
       a.deaddiagnose,
       a.deaddiagnosexml,
       a.deadreason,
       a.deadreasonxml,
       a.useforreference,
       a.useforreferencexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.opendate,
       b.modifydate,
       b.modifyuserid,
       b.address_right,
       b.discusscontent_right,
       b.deaddiagnose_right,
       b.deadreason_right,
       b.useforreference_right,
       b.compereid,
       b.readouterid,b.pagination
  from deadcasediscussrecord a
 inner join deadcasediscussrecordcontent b on a.inpatientid = b.inpatientid
                                          and a.inpatientdate =
                                              b.inpatientdate
                                          and a.opendate = b.opendate

 where a.inpatientid = ?
   and a.inpatientdate = ?
 and a.status = 0
   and b.modifydate =
       (select max(modifydate)
          from deadcasediscussrecordcontent
         where (inpatientid = a.inpatientid and
               inpatientdate = a.inpatientdate and opendate = a.opendate))";//2
		
		// 在DeadCaseDiscussRecordAttendee中获取指定表单和ModifyDate的参加人员。
		private string c_strGetAttendeeContentSQL_DeadCaseDiscuss= @"select sub2.attendeeid, pbi.lastname_vchr as attendeename
																		from deadcasediscussrecord         a,
																			deadcasediscussrecordattendee sub2,
																			t_bse_employee              pbi
																		where a.inpatientid = ?
																		and a.inpatientdate = ?
																		and a.opendate = ?
																		and a.status = 0
																		and sub2.attendeeid = pbi.empno_chr
																		and sub2.inpatientid = a.inpatientid
																		and sub2.inpatientdate = a.inpatientdate
																		and sub2.opendate = a.opendate
																		and sub2.modifydate = (select max(modifydate)
																									from deadcasediscussrecordattendee
																								where inpatientid = a.inpatientid
																									and inpatientdate = a.inpatientdate
																									and opendate = a.opendate) ";
		// 在DeadCaseDiscussRecordNoter中获取指定表单和ModifyDate的记录者。
		private string c_strGetNoterContentSQL_DeadCaseDiscuss= @"select sub2.noterid, pbi.lastname_vchr as notername
																	from deadcasediscussrecord      a,
																		deadcasediscussrecordnoter sub2,
																		t_bse_employee           pbi
																	where a.inpatientid = ?
																	and a.inpatientdate = ?
																	and a.opendate = ?
																	and a.status = 0
																	and sub2.noterid = pbi.empno_chr
																	and sub2.inpatientid = a.inpatientid
																	and sub2.inpatientdate = a.inpatientdate
																	and sub2.opendate = a.opendate
																	and sub2.modifydate = (select max(modifydate)
																								from deadcasediscussrecordnoter
																							where inpatientid = a.inpatientid
																								and inpatientdate = a.inpatientdate
																								and opendate = a.opendate) ";
		#endregion 死亡病例讨论记录

		#region 死亡记录
		private const string c_strUpdateFirstPrintDateSQL_Dead="update  deadrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_Dead=@"select b.modifydate,b.modifyuserid from deadrecord a,deadrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from deadrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_Dead="select deactiveddate,deactivedoperatorid from deadrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_Dead="update deadrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_Dead = @"select a.inpatientid,
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
       a.inhospitalcase,
       a.inhospitalcasexml,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.diagnoseby,
       a.diagnosebyxml,
       a.deaddiagnose,
       a.deaddiagnosexml,
       a.deadreason,
       a.deadreasonxml,
       a.experience,
       a.experiencexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.deaddate,
       b.inhospitalcase_right,
       b.originaldiagnose_right,
       b.diagnoseby_right,
       b.deaddiagnose_right,
       b.deadreason_right,
       b.experience_right,
       b.doctorid,b.pagination
  from deadrecord a, deadrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from deadrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
		#endregion 死亡记录
		
		#region 出院记录
		private const string c_strUpdateFirstPrintDateSQL_OutHospital="update  outhospitalrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_OutHospital=@"select b.modifydate,b.modifyuserid from outhospitalrecord a,outhospitalrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from outhospitalrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_OutHospital="select deactiveddate,deactivedoperatorid from outhospitalrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_OutHospital="update outhospitalrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_OutHospital = @"select a.inpatientid,
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
       a.opendate as opendate_main,
       pbi1.lastname_vchr as maindoctorname,
       pbi2.lastname_vchr as doctorname,b.pagination
  from outhospitalrecord        a,
       outhospitalrecordcontent b,
       t_bse_employee           pbi1,
       t_bse_employee           pbi2
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.maindoctorid = pbi1.empno_chr
   and b.doctorid = pbi2.empno_chr
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from outhospitalrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate) ";

		#endregion 出院记录

		#region 手术后病程记录
		private const string c_strUpdateFirstPrintDateSQL_AfterOperation="update  afteroperationrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_AfterOperation=@"select b.modifydate,b.modifyuserid from afteroperationrecord a,afteroperationrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from afteroperationrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_AfterOperation="select deactiveddate,deactivedoperatorid from afteroperationrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_AfterOperation="update afteroperationrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_AfterOperation = @"select a.inpatientid,
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
       a.anaesthesiamode,
       a.anaesthesiamodexml,
       a.operationname,
       a.operationnamexml,
       a.operationdiagnose,
       a.operationdiagnosexml,
       a.inoperationseeing,
       a.inoperationseeingxml,
       a.afteroperationdeal,
       a.afteroperationdealxml,
       a.afteroperationnotice,
       a.afteroperationnoticexml,
       a.cuthealupstatus,
       a.cuthealupstatusxml,
       a.takeoutstitchesdate,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.anaesthesiamode_right,
       b.operationname_right,
       b.operationdiagnose_right,
       b.inoperationseeing_right,
       b.afteroperationdeal_right,
       b.afteroperationnotice_right,
       b.cuthealupstatus_right,b.pagination
  from afteroperationrecord a, afteroperationrecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from afteroperationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?
		#endregion 手术后病程记录

		#region 抢救记录
		private const string c_strUpdateFirstPrintDateSQL_Save="update  saverecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_Save=@"select b.modifydate,b.modifyuserid from saverecord a,saverecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from saverecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_Save="select deactiveddate,deactivedoperatorid from saverecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_Save="update saverecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_Save = @"select a.inpatientid,
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
       a.diseasename,
       a.diseasenamexml,
       a.diseasechangecase,
       a.diseasechangecasexml,
       a.savedeal,
       a.savedealxml,
       a.saveresult,
       a.saveresultxml,
       a.attendpeople,
       a.attendpeoplexml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.savetime,
       b.diseasename_right,
       b.diseasechangecase_right,
       b.savedeal_right,
       b.saveresult_right,
       b.bydoctorid,
       b.attendpeople_right,b.pagination
  from saverecord a, saverecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from saverecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
																
		// 在SaveRecordDoctor中获取指定表单和ModifyDate的参加人员。
		private const string c_strGetDoctorContentSQL_Save= @"select sub2.savedoctorid, pbi.lastname_vchr as savedoctorname
																from saverecord a, saverecorddoctor sub2, t_bse_employee pbi
																where a.inpatientid = ?
																and a.inpatientdate = ?
																and a.opendate = ?
																and a.status = 0
																and sub2.savedoctorid = pbi.empno_chr
																and sub2.inpatientid = a.inpatientid
																and sub2.inpatientdate = a.inpatientdate
																and sub2.opendate = a.opendate
																and sub2.modifydate = (select max(modifydate)
																							from saverecorddoctor
																						where inpatientid = a.inpatientid
																							and inpatientdate = a.inpatientdate
																							and opendate = a.opendate) ";
		#endregion 抢救记录

		#region 首次病程记录
		private const string c_strUpdateFirstPrintDateSQL_FirstIllnessNote="update  firstillnessnoterecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_FirstIllnessNotel=@"select b.modifydate,b.modifyuserid from firstillnessnoterecord a,firstillnessnoterecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from firstillnessnoterecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_FirstIllnessNote="select deactiveddate,deactivedoperatorid from firstillnessnoterecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_FirstIllnessNote="update firstillnessnoterecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_FirstIllnessNote = @"select a.inpatientid,
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
       a.mostlycontent,
       a.mostlycontentxml,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.thereunderdiagnose,
       a.thereunderdiagnosexml,
       a.diagnosediffe,
       a.diagnosediffexml,
       a.cureplan,
       a.cureplanxml,
       a.sequence_int,
       a.markstatus,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.mostlycontent_right,
       b.originaldiagnose_right,
       b.thereunderdiagnose_right,
       b.diagnosediffe_right,
       b.cureplan_right,
       b.doctorid,b.pagination
  from firstillnessnoterecord a, firstillnessnoterecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from firstillnessnoterecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?

		private const string c_strGetDoctorContentSQL_FirstIllnessNote= @"select sub2.employeeid, pbi.lastname_vchr as firstname
																			from firstillnessnoterecord        a,
																				firstillnessnoterecordcontent sub2,
																				t_bse_employee              pbi
																			where a.inpatientid = ?
																			and a.inpatientdate = ?
																			and a.opendate = ?
																			and a.status = 0
																			and sub2.employeeid = pbi.empno_chr
																			and sub2.inpatientid = a.inpatientid
																			and sub2.inpatientdate = a.inpatientdate
																			and sub2.opendate = a.opendate
																			and sub2.modifydate = (select max(modifydate)
																										from firstillnessnoterecordcontent
																									where inpatientid = a.inpatientid
																										and inpatientdate = a.inpatientdate
																										and opendate = a.opendate) ";
		#endregion 一般病程记录

		#region 首次病程记录 中医科
		private const string c_strUpdateFirstPrintDateSQL_FirstIllnessNote_ZY="update  firstillnessnoterecord_gzzy set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_FirstIllnessNotel_ZY=@"select b.modifydate,b.modifyuserid from firstillnessnoterecord_gzzy a,firstillnessnoterecordcon_gzzy b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from firstillnessnoterecordcon_gzzy where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_FirstIllnessNote_ZY="select deactiveddate,deactivedoperatorid from firstillnessnoterecord_gzzy where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_FirstIllnessNote_ZY="update firstillnessnoterecord_gzzy set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_FirstIllnessNote_ZY = @"select a.inpatientid,
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
       a.mostlycontent,
       a.mostlycontentxml,
       a.originaldiagnose,
       a.originaldiagnosexml,
       a.thereunderdiagnose,
       a.thereunderdiagnosexml,
       a.diagnosediffe,
       a.diagnosediffexml,
       a.cureplan,
       a.cureplanxml,
       a.identifyreston,
       a.identifyrestonxml,
       a.identifydiagnose,
       a.identifydiagnosexml,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.mostlycontent_right,
       b.originaldiagnose_right,
       b.thereunderdiagnose_right,
       b.diagnosediffe_right,
       b.cureplan_right,
       b.doctorid,
       b.identifyreston_right,
       b.identifydiagnose_right
  from firstillnessnoterecord_gzzy a, firstillnessnoterecordcon_gzzy b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from firstillnessnoterecordcon_gzzy
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";//and a.OpenDate= ?

		private const string c_strGetDoctorContentSQL_FirstIllnessNote_ZY= @"select sub2.doctorid, pbi.lastname_vchr as firstname
																			from firstillnessnoterecord_gzzy        a,
																				firstillnessnoterecordcon_gzzy sub2,
																				t_bse_employee              pbi
																			where a.inpatientid = ?
																			and a.inpatientdate = ?
																			and a.opendate = ?
																			and a.status = 0
																			and sub2.doctorid = pbi.empno_chr
																			and sub2.inpatientid = a.inpatientid
																			and sub2.inpatientdate = a.inpatientdate
																			and sub2.opendate = a.opendate
																			and sub2.modifydate = (select max(modifydate)
																										from firstillnessnoterecordcon_gzzy
																									where inpatientid = a.inpatientid
																										and inpatientdate = a.inpatientdate
																										and opendate = a.opendate) ";
		#endregion 一般病程记录

        #region 术前小结
        private const string c_strUpdateFirstPrintDateSQL_SummaryBeforeOP = "update  t_emr_summarybeforeop set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strCheckLastModifyRecordSQL_SummaryBeforeOP = @"select b.modifydate,b.modifyuserid
                                                          from t_emr_summarybeforeop a, t_emr_summarybeforeopcon b
                                                         where a.inpatientid = ?
                                                           and a.inpatientdate = ?
                                                           and a.opendate = ?
                                                           and a.status = 0
                                                           and a.emr_seq = b.emr_seq
                                                           and b.status = 0";

        private const string c_strGetDeleteRecordSQL_SummaryBeforeOP = @"select deactiveddate, deactivedoperatorid
                                                          from t_emr_summarybeforeop
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and opendate = ?
                                                           and status = 1 ";

        private const string c_strDeleteRecordSQL_SummaryBeforeOP = @"update t_emr_summarybeforeop
                                                       set status = 1, deactiveddate = ?, deactivedoperatorid = ? 
                                                        where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_SummaryBeforeOP = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.ifconfirm,
       a.markstatus,
       a.sequence_int,
       a.recorddate,
       a.registerid_chr,
       a.diseasesummary,
       a.diseasesummaryxml,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.diagnosisgist,
       a.diagnosisgistxml,
       a.opindication,
       a.opindicationxml,
       a.opmode,
       a.opmodexml,
       a.anamode,
       a.anamodexml,
       a.proceeding,
       a.proceedingxml,
       a.preparebeforeop,
       a.preparebeforeopxml,
       a.emr_seq,
       b.modifydate,
       b.modifyuserid,
       b.registerid_chr,
       b.diseasesummary_right,
       b.diagnosisbeforeop_right,
       b.diagnosisgist_right,
       b.opindication_right,
       b.opmode_right,
       b.anamode_right,
       b.proceeding_right,
       b.preparebeforeop_right,
       a.opendate as opendate_main,b.pagination
  from t_emr_summarybeforeop a, t_emr_summarybeforeopcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.emr_seq = b.emr_seq
   and b.status = 0";//and a.OpenDate= ?
        #endregion 

		private const string c_strGetRecordContentSQL_1="";

		private const string c_strGetRecordContentSQL_2="";

		private const string c_strGetRecordContentSQL_="";

		private const string c_strCheckLastModifyRecordSQL="";

		private const string c_strGetModifyRecordSQL="";

		private const string c_strGetDeleteRecordSQL="";

		private const string c_strDeleteRecordSQL="";

        #region 查询子病程记录的个数
        const string c_strGetIsRecordExists = @"select 
(select count(inpatientid) from generaldiseaserecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) generaldiseaserecord,
(select count(inpatientid) from handoverrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) handoverrecord,
(select count(inpatientid) from takeoverrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) takeoverrecord,
(select count(inpatientid) from diseasesummaryrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) diseasesummaryrecord,
(select count(inpatientid) from conveyrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) conveyrecord,
(select count(inpatientid) from turninrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) turninrecord,
(select count(inpatientid) from checkroomrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) checkroomrecord,
(select count(inpatientid) from casediscussrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) casediscussrecord,
(select count(inpatientid) from beforeoperationdiscussrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) beforeopdiscussrecord,
(select count(inpatientid) from deadcasediscussrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) deadcasediscussrecord,
(select count(inpatientid) from deadrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) deadrecord,
(select count(inpatientid) from afteroperationrecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) afteroperationrecord,
(select count(inpatientid) from saverecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) saverecord,
(select count(inpatientid) from firstillnessnoterecord where 
(inpatientid) = ? and inpatientdate= ?  and status=0) firstillnessnoterecord,
(select count(inpatientid) from firstillnessnoterecord_gzzy where 
(inpatientid) = ? and inpatientdate= ?  and status=0) firstillnesrec_gzzy,
(select count(inpatientid) from t_emr_summarybeforeop where 
(inpatientid) = ? and inpatientdate= ?  and status=0) emr_summarybeforeop";
        #endregion 

        #region 更新数据库中的首次打印时间
        /// <summary>
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_intRecordTypeArr">记录类型</param>
        /// <param name="p_dtmOpenDateArr">记录时间(与记录类型及其位置一一对应)</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmOpenDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsMainDiseaseTrackService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" ||
                    p_intRecordTypeArr == null || p_dtmOpenDateArr == null || p_intRecordTypeArr.Length != p_dtmOpenDateArr.Length || p_dtmFirstPrintDate == DateTime.MinValue)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;

                for (int i = 0; i < p_intRecordTypeArr.Length; i++)
                {
                    //根据不同的子表单，获取不同的SQL语句
                    string strSQL = null;
                    switch ((enmDiseaseTrackType)p_intRecordTypeArr[i])
                    {
                        case enmDiseaseTrackType.GeneralDisease:
                            strSQL = c_strUpdateFirstPrintDateSQL_Normal;
                            break;
                        case enmDiseaseTrackType.HandOver:
                            strSQL = c_strUpdateFirstPrintDateSQL_HandOver;
                            break;
                        case enmDiseaseTrackType.TakeOver:
                            strSQL = c_strUpdateFirstPrintDateSQL_TakeOver;
                            break;
                        case enmDiseaseTrackType.Consultation:
                            strSQL = c_strUpdateFirstPrintDateSQL_Consultation;
                            break;
                        case enmDiseaseTrackType.DiseaseSummary:
                            strSQL = c_strUpdateFirstPrintDateSQL_DiseaseSummary;
                            break;
                        case enmDiseaseTrackType.Convey:
                            strSQL = c_strUpdateFirstPrintDateSQL_Convey;
                            break;
                        case enmDiseaseTrackType.TurnIn:
                            strSQL = c_strUpdateFirstPrintDateSQL_TurnIn;
                            break;
                        case enmDiseaseTrackType.CheckRoom:
                            strSQL = c_strUpdateFirstPrintDateSQL_CheckRoom;
                            break;
                        case enmDiseaseTrackType.CaseDiscuss:
                            strSQL = c_strUpdateFirstPrintDateSQL_CaseDiscuss;
                            break;
                        case enmDiseaseTrackType.BeforeOperationDiscuss:
                            strSQL = c_strUpdateFirstPrintDateSQL_BeforeOperationDiscuss;
                            break;
                        case enmDiseaseTrackType.DeadCaseDiscuss:
                            strSQL = c_strUpdateFirstPrintDateSQL_DeadCaseDiscuss;
                            break;
                        case enmDiseaseTrackType.Dead:
                            strSQL = c_strUpdateFirstPrintDateSQL_Dead;
                            break;
                        case enmDiseaseTrackType.OutHospital:
                            strSQL = c_strUpdateFirstPrintDateSQL_OutHospital;
                            break;
                        case enmDiseaseTrackType.AfterOperation:
                            strSQL = c_strUpdateFirstPrintDateSQL_AfterOperation;
                            break;
                        case enmDiseaseTrackType.Save:
                            strSQL = c_strUpdateFirstPrintDateSQL_Save;
                            break;
                        case enmDiseaseTrackType.FirstIllnessNote:
                            strSQL = c_strUpdateFirstPrintDateSQL_FirstIllnessNote;
                            break;
                        case enmDiseaseTrackType.FirstIllnessNote_ZY:
                            strSQL = c_strUpdateFirstPrintDateSQL_FirstIllnessNote_ZY;
                            break;
                        case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                            strSQL = c_strUpdateFirstPrintDateSQL_SummaryBeforeOP;
                            break;

                        default: return (long)enmOperationResult.Parameter_Error;
                    }

                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_dtmFirstPrintDate;
                    objDPArr[1].Value = p_strInPatientID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_dtmOpenDateArr[i];
                    //执行SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
                }

                return (long)enmOperationResult.DB_Succeed;

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;
        } 
        #endregion

        #region 获取指定记录的内容
        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objTansDataInfoArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;

            long lngRes = 0;

            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                DateTime dtmInpatientDate = Convert.ToDateTime(p_strInPatientDate);
                #region 先去数据库查看写了那些病程
                DataTable dtbRecordCount = new DataTable();
                lngRes = m_lngCheckExistsRecord(p_strInPatientID, dtmInpatientDate, out dtbRecordCount);
                if (lngRes <= 0 || dtbRecordCount.Rows.Count == 0)
                    return -1;
                #endregion 

                ArrayList arlTransData = new ArrayList();

                #region 一般病程记录

                if (dtbRecordCount.Rows[0]["GeneralDiseaseRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetGeneralDiseaseRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 交班记录
                if (dtbRecordCount.Rows[0]["HandOverRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetHandOverRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 接班记录
                if (dtbRecordCount.Rows[0]["TakeOverRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetTakeOverRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 会诊记录
                //			//会诊记录，使用c_strGetRecordContentSQL_Consultation,//注意:此时查询条件中没有OpenDate,与会诊记录的Server中SQL条件不同,故结果会有多行
                //			//获取IDataParameter数组
                //			/*IDataParameter[]*/ objDPArr = new Oracle.DataAccess.Client.OracleParameter[2];
                //			//按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                //			objDPArr[0].Value=p_strInPatientID;
                //			objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);		
                //			
                //			//生成DataTable
                //			/*DataTable*/ dtbValue = new DataTable();
                //			//执行查询，填充结果到DataTable       
                //			/*long*/ lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Consultation,ref dtbValue,objDPArr);
                //			
                //			//循环DataTable
                //			if(lngRes > 0 && dtbValue.Rows.Count >0)
                //			{				
                //				for(int j=0;j<dtbValue.Rows.Count;j++)
                //				{
                //					#region 从DataTable.Rows中获取结果
                //					//设置结果
                //					clsConsultationRecordContent objRecordContent=new clsConsultationRecordContent();
                //					objRecordContent.m_strInPatientID=p_strInPatientID;
                //					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
                //					objRecordContent.m_dtmOpenDate=DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                //					objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                //					objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
                //				
                //					if(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString()=="")
                //						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
                //					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                //					objRecordContent.m_strCreateUserID=dtbValue.Rows[j]["CREATEUSERID"].ToString();
                //					objRecordContent.m_strModifyUserID=dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                //					if(dtbValue.Rows[j]["IFCONFIRM"].ToString()=="")
                //						objRecordContent.m_bytIfConfirm=0;
                //					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                //					if(dtbValue.Rows[j]["STATUS"].ToString()=="")
                //						objRecordContent.m_bytStatus=0;
                //					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                //					objRecordContent.m_strConfirmReason=dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                //					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();
                //
                //					objRecordContent.m_intConsultationTime = int.Parse(dtbValue.Rows[j]["CONSULTATIONTIME"].ToString());
                //
                //					objRecordContent.m_strApplyConsultationDeptID = dtbValue.Rows[j]["APPLYCONSULTATIONDEPTID"].ToString();
                //					objRecordContent.m_strApplyConsultationDeptName = dtbValue.Rows[j]["APPLYDEPTNAME"].ToString();
                //
                //					objRecordContent.m_strAskConsultationDeptID = dtbValue.Rows[j]["ASKCONSULTATIONDEPTID"].ToString();
                //					objRecordContent.m_strAskConsultationDeptName = dtbValue.Rows[j]["ASKDEPTNAME"].ToString();
                //
                //					objRecordContent.m_strConsultationDeptID = dtbValue.Rows[j]["CONSULTATIONDEPTID"].ToString();
                //					objRecordContent.m_strConsultationDeptName = dtbValue.Rows[j]["DEPTNAME"].ToString();
                //
                //					objRecordContent.m_strCaseHistory_Right=dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                //					objRecordContent.m_strCaseHistory=dtbValue.Rows[j]["CASEHISTORY"].ToString();
                //					objRecordContent.m_strCaseHistoryXml=dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                //
                //					objRecordContent.m_strConsultationOrder_Right=dtbValue.Rows[j]["CONSULTATIONORDER_RIGHT"].ToString();
                //					objRecordContent.m_strConsultationOrder=dtbValue.Rows[j]["CONSULTATIONORDER"].ToString();
                //					objRecordContent.m_strConsultationOrderXml=dtbValue.Rows[j]["CONSULTATIONORDERXML"].ToString();
                //
                //					objRecordContent.m_strConsultationIdea_Right=dtbValue.Rows[j]["CONSULTATIONIDEA_RIGHT"].ToString();
                //					objRecordContent.m_strConsultationIdea=dtbValue.Rows[j]["CONSULTATIONIDEA"].ToString();
                //					objRecordContent.m_strConsultationIdeaXml=dtbValue.Rows[j]["CONSULTATIONIDEAXML"].ToString();
                //				
                //					objRecordContent.m_dtmConsultationDate = DateTime.Parse(dtbValue.Rows[j]["CONSULTATIONDATE"].ToString());				
                //					objRecordContent.m_strMainDoctorID=dtbValue.Rows[j]["MAINDOCTORID"].ToString();
                ////					objRecordContent.m_strMainDoctorName=dtbValue.Rows[j]["FIRSTNAME"].ToString();
                //				
                //					DataTable dtbValue2 = new DataTable();
                //					IDataParameter[] objDPArr2 = new Oracle.DataAccess.Client.OracleParameter[3];
                //					//按顺序给IDataParameter赋值
                //					for(int i=0;i<objDPArr2.Length;i++)
                //						objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                //					objDPArr2[0].Value=p_strInPatientID;
                //					objDPArr2[1].Value=DateTime.Parse(p_strInPatientDate);
                //					objDPArr2[2].Value=objRecordContent.m_dtmOpenDate;
                //
                //					long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL_Consultation1,ref dtbValue2,objDPArr2);
                //					//从DataTable.Rows中获取结果
                //					if(lngRes2 > 0 && dtbValue2.Rows.Count >0)
                //					{
                //						objRecordContent.m_strRequestDoctorIDArr=new string[dtbValue2.Rows.Count];
                //						objRecordContent.m_strRequestDoctorNameArr=new string[dtbValue2.Rows.Count];
                //						for(int i=0;i<dtbValue2.Rows.Count;i++)
                //						{
                //							objRecordContent.m_strRequestDoctorIDArr[i]=dtbValue2.Rows[i]["EMPLOYEEID"].ToString();
                //							objRecordContent.m_strRequestDoctorNameArr[i]=dtbValue2.Rows[i]["FIRSTNAME"].ToString();
                //						}
                //					}
                //
                //					DataTable dtbValue3 = new DataTable();
                //					IDataParameter[] objDPArr3 = new Oracle.DataAccess.Client.OracleParameter[3];
                //					//按顺序给IDataParameter赋值
                //					for(int i=0;i<objDPArr3.Length;i++)
                //						objDPArr3[i]=new Oracle.DataAccess.Client.OracleParameter();
                //					objDPArr3[0].Value=p_strInPatientID;
                //					objDPArr3[1].Value=DateTime.Parse(p_strInPatientDate);
                //					objDPArr3[2].Value=objRecordContent.m_dtmOpenDate;
                //
                //					long lngRes3 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL_Consultation2,ref dtbValue3,objDPArr3);
                //					//从DataTable.Rows中获取结果
                //					if(lngRes3 > 0 && dtbValue3.Rows.Count >0)
                //					{
                //						objRecordContent.m_strConsultationDoctorIDArr=new string[dtbValue3.Rows.Count];
                //						objRecordContent.m_strConsultationDoctorNameArr=new string[dtbValue3.Rows.Count];
                //						for(int i=0;i<dtbValue3.Rows.Count;i++)
                //						{
                //							objRecordContent.m_strConsultationDoctorIDArr[i]=dtbValue3.Rows[i]["EMPLOYEEID"].ToString();
                //							objRecordContent.m_strConsultationDoctorNameArr[i]=dtbValue3.Rows[i]["FIRSTNAME"].ToString();
                //						}
                //					}
                //
                //
                //						
                //					#endregion 从DataTable.Rows中获取结果
                //					//获取签名集合
                //				if (dtbValue.Rows[j]["SEQUENCE_INT"]!=DBNull.Value)
                //				{
                //					long lngS=long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                //					com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign=new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                //					long lngTemp=objSign.m_lngGetSign(lngS,out objRecordContent.objSignerArr);
                //				}
                //					//生成 clsTransDataInfo
                //					clsTransDataInfo objInfo = new clsTransDataInfo();
                //					objInfo.m_intFlag = (int)enmDiseaseTrackType.Consultation;
                //			
                //					//设置结果到 objInfo.m_objRecordContent
                //					objInfo.m_objRecordContent=	objRecordContent;
                //
                //					arlTransData.Add(objInfo);
                //				}
                //			}  
                #endregion 

                #region 病程小结
                if (dtbRecordCount.Rows[0]["DiseaseSummaryRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetDiseaseSummaryRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 
	
                #region 转出记录
                if (dtbRecordCount.Rows[0]["ConveyRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetConveyRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 转入记录
                if (dtbRecordCount.Rows[0]["TurnInRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetTurnInRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 查房记录
                if (dtbRecordCount.Rows[0]["CheckRoomRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetCheckRoomRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 病例讨论记录
                if (dtbRecordCount.Rows[0]["CaseDiscussRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetCaseDiscussRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 	

                #region 术前讨论记录
                if (dtbRecordCount.Rows[0]["BeforeOpDiscussRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetBeforeOperationDiscussRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion

                #region 死亡病例讨论记录
                if (dtbRecordCount.Rows[0]["DeadCaseDiscussRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetDeadCaseDiscussRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 死亡记录
                if (dtbRecordCount.Rows[0]["DeadRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetDeadRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 出院记录
                //			//一般病程记录，使用c_strGetRecordContentSQL_Normal,//注意:此时查询条件中没有OpenDate,与一般病程记录的Server中SQL条件不同,故结果会有多行
                //			//获取IDataParameter数组
                //			/*IDataParameter[]*/ objDPArr = new Oracle.DataAccess.Client.OracleParameter[2];
                //			//按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                //			objDPArr[0].Value=p_strInPatientID;
                //			objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);		
                //			
                //			//生成DataTable
                //			/*DataTable*/ dtbValue = new DataTable();
                //			//执行查询，填充结果到DataTable       
                //			/*long*/ lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_OutHospital,ref dtbValue,objDPArr);
                //			
                //			//循环DataTable
                //			if(lngRes > 0 && dtbValue.Rows.Count >0)
                //			{				
                //				for(int j=0;j<dtbValue.Rows.Count;j++)
                //				{
                //					#region 从DataTable.Rows中获取结果
                //					//设置结果
                //					clsOutHospitalRecordContent objRecordContent=new clsOutHospitalRecordContent();
                //					objRecordContent.m_strInPatientID=p_strInPatientID;
                //					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
                //					objRecordContent.m_dtmOpenDate=DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                //					objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                //					objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
                //				
                //					if(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString()=="")
                //						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
                //					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                //					objRecordContent.m_strCreateUserID=dtbValue.Rows[j]["CREATEUSERID"].ToString();
                //					objRecordContent.m_strModifyUserID=dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                //					if(dtbValue.Rows[j]["IFCONFIRM"].ToString()=="")
                //						objRecordContent.m_bytIfConfirm=0;
                //					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                //					if(dtbValue.Rows[j]["STATUS"].ToString()=="")
                //						objRecordContent.m_bytStatus=0;
                //					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                //					objRecordContent.m_strConfirmReason=dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                //					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();
                //
                //					objRecordContent.m_dtmOutHospitalDate=DateTime.Parse(dtbValue.Rows[j]["OUTHOSPITALDATE"].ToString());
                //					objRecordContent.m_strHeartID_Right=dtbValue.Rows[j]["HEARTID_RIGHT"].ToString();
                //					objRecordContent.m_strHeartID=dtbValue.Rows[j]["HEARTID"].ToString();
                //					objRecordContent.m_strHeartIDXML=dtbValue.Rows[j]["HEARTIDXML"].ToString();
                //					objRecordContent.m_strXRayID_Right=dtbValue.Rows[j]["XRAYID_RIGHT"].ToString();
                //					objRecordContent.m_strXRayID=dtbValue.Rows[j]["XRAYID"].ToString();
                //					objRecordContent.m_strXRayIDXML=dtbValue.Rows[j]["XRAYIDXML"].ToString();
                //					objRecordContent.m_strInHospitalDiagnose_Right=dtbValue.Rows[j]["INHOSPITALDIAGNOSE_RIGHT"].ToString();
                //					objRecordContent.m_strInHospitalDiagnose=dtbValue.Rows[j]["INHOSPITALDIAGNOSE"].ToString();
                //					objRecordContent.m_strInHospitalDiagnoseXML=dtbValue.Rows[j]["INHOSPITALDIAGNOSEXML"].ToString();
                //					objRecordContent.m_strOutHospitalDiagnose_Right=dtbValue.Rows[j]["OUTHOSPITALDIAGNOSE_RIGHT"].ToString();
                //					objRecordContent.m_strOutHospitalDiagnose=dtbValue.Rows[j]["OUTHOSPITALDIAGNOSE"].ToString();
                //					objRecordContent.m_strOutHospitalDiagnoseXML=dtbValue.Rows[j]["OUTHOSPITALDIAGNOSEXML"].ToString();
                //					objRecordContent.m_strInHospitalCase_Right=dtbValue.Rows[j]["INHOSPITALCASE_RIGHT"].ToString();
                //					objRecordContent.m_strInHospitalCase=dtbValue.Rows[j]["INHOSPITALCASE"].ToString();
                //					objRecordContent.m_strInHospitalCaseXML=dtbValue.Rows[j]["INHOSPITALCASEXML"].ToString();
                //					objRecordContent.m_strInHospitalBy_Right=dtbValue.Rows[j]["INHOSPITALBY_RIGHT"].ToString();
                //					objRecordContent.m_strInHospitalBy=dtbValue.Rows[j]["INHOSPITALBY"].ToString();
                //					objRecordContent.m_strInHospitalByXML=dtbValue.Rows[j]["INHOSPITALBYXML"].ToString();
                //					objRecordContent.m_strOutHospitalCase_Right=dtbValue.Rows[j]["OUTHOSPITALCASE_RIGHT"].ToString();
                //					objRecordContent.m_strOutHospitalCase=dtbValue.Rows[j]["OUTHOSPITALCASE"].ToString();
                //					objRecordContent.m_strOutHospitalCaseXML=dtbValue.Rows[j]["OUTHOSPITALCASEXML"].ToString();
                //					objRecordContent.m_strOutHospitalAdvice_Right=dtbValue.Rows[j]["OUTHOSPITALADVICE_RIGHT"].ToString();
                //					objRecordContent.m_strOutHospitalAdvice=dtbValue.Rows[j]["OUTHOSPITALADVICE"].ToString();
                //					objRecordContent.m_strOutHospitalAdviceXML=dtbValue.Rows[j]["OUTHOSPITALADVICEXML"].ToString();
                //				
                //					objRecordContent.m_strDoctorID=dtbValue.Rows[j]["DOCTORID"].ToString();
                //					objRecordContent.m_strDoctorName=dtbValue.Rows[j]["DOCTORNAME"].ToString().trim();
                //					objRecordContent.m_strMainDoctorID=dtbValue.Rows[j]["MAINDOCTORID"].ToString();
                //					objRecordContent.m_strMainDoctorName=dtbValue.Rows[j]["MAINDOCTORNAME"].ToString().trim();
                //				
                //					#endregion 从DataTable.Rows中获取结果
                //					//获取签名集合
                //				if (dtbValue.Rows[j]["SEQUENCE_INT"]!=DBNull.Value)
                //				{
                //					long lngS=long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                //					com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign=new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                //					long lngTemp=objSign.m_lngGetSign(lngS,out objRecordContent.objSignerArr);
                //				}
                //					//生成 clsTransDataInfo
                //					clsTransDataInfo objInfo = new clsTransDataInfo();
                //					objInfo.m_intFlag = (int)enmDiseaseTrackType.OutHospital;
                //			
                //					//设置结果到 objInfo.m_objRecordContent
                //					objInfo.m_objRecordContent=	objRecordContent;
                //
                //					arlTransData.Add(objInfo);
                //				}
                //			}  
                #endregion 出院记录

                #region 手术后病程记录
                if (dtbRecordCount.Rows[0]["AfterOperationRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetAfterOperationRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 	

                #region 抢救记录
                if (dtbRecordCount.Rows[0]["SaveRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetSaveRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 首次病程记录
                if (dtbRecordCount.Rows[0]["FirstIllnessNoteRecord"].ToString() != "0")
                {
                    lngRes = m_lngGetFirstIllnessNoteRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 首次病程记录 市一中医科
                if (dtbRecordCount.Rows[0]["firstillnesrec_gzzy"].ToString() != "0")
                {
                    lngRes = m_lngGetFirstIllnessNote_ZYRecord(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                #region 术前小结
                if (dtbRecordCount.Rows[0]["EMR_SummaryBeforeOP"].ToString() != "0")
                {
                    lngRes = m_lngGetSummaryBeforeOP(p_strInPatientID, dtmInpatientDate, ref arlTransData);
                }
                #endregion 

                //返回结果到p_objTansDataInfoArr
                p_objTansDataInfoArr = (clsTransDataInfo[])arlTransData.ToArray(typeof(clsTransDataInfo));

                lngRes = (long)enmOperationResult.DB_Succeed;

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//返回
            return lngRes;

        } 
        #endregion

        #region 先去数据库查看写了那些病程
        /// <summary>
        /// 先去数据库查看写了那些病程
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_dtbRecordCount"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngCheckExistsRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, out DataTable p_dtbRecordCount)
        {
            p_dtbRecordCount = new DataTable();
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(32, out objDPArr);
                for (int kk = 0; kk < objDPArr.Length; kk++)
                {
                    objDPArr[kk++].Value = p_strInPatientID;
                    objDPArr[kk].Value = p_dtmInpatientDate;
                }
                DataTable dtbRecordCount = new DataTable();
                string strSQL = c_strGetIsRecordExists;
                if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL += " from dual";
                }
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbRecordCount, objDPArr);
                if (lngRes <= 0 || dtbRecordCount.Rows.Count == 0)
                    return lngRes;
                p_dtbRecordCount = dtbRecordCount;
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取一般病程记录
        /// <summary>
        /// 获取一般病程记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetGeneralDiseaseRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            //一般病程记录，使用c_strGetRecordContentSQL_Normal,//注意:此时查询条件中没有OpenDate,与一般病程记录的Server中SQL条件不同,故结果会有多行
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Normal, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsGeneralDiseaseRecordContent objRecordContent = new clsGeneralDiseaseRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strRecordTitle = dtbValue.Rows[j]["RECORDTITLE"].ToString();
                        if (dtbValue.Rows[j]["RECORDTITLETYPE"].ToString() == "")
                            objRecordContent.m_intRecordTitleType = -1;
                        else objRecordContent.m_intRecordTitleType = int.Parse(dtbValue.Rows[j]["RECORDTITLETYPE"].ToString());
                        objRecordContent.m_strRecordContent_Right = dtbValue.Rows[j]["RECORDCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strRecordContent = dtbValue.Rows[j]["RECORDCONTENT"].ToString();
                        objRecordContent.m_strRecordContentXml = dtbValue.Rows[j]["RECORDCONTENTXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();


                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.GeneralDisease;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取交班记录
        /// <summary>
        /// 获取交班记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetHandOverRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_HandOver, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsHandOverRecordContent objRecordContent = new clsHandOverRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCurrentDiagnose_Right = dtbValue.Rows[j]["CURRENTDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentDiagnose = dtbValue.Rows[j]["CURRENTDIAGNOSE"].ToString();
                        objRecordContent.m_strCurrentDiagnoseXML = dtbValue.Rows[j]["CURRENTDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.HandOver;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取接班记录
        /// <summary>
        /// 获取接班记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetTakeOverRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_TakeOver, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsTakeOverRecordContent objRecordContent = new clsTakeOverRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCurrentDiagnose_Right = dtbValue.Rows[j]["CURRENTDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentDiagnose = dtbValue.Rows[j]["CURRENTDIAGNOSE"].ToString();
                        objRecordContent.m_strCurrentDiagnoseXML = dtbValue.Rows[j]["CURRENTDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.TakeOver;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取病程小结
        /// <summary>
        /// 获取病程小结
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetDiseaseSummaryRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_DiseaseSummary, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsDiseaseSummaryRecordContent objRecordContent = new clsDiseaseSummaryRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strInHospitalCase_Right = dtbValue.Rows[j]["INHOSPITALCASE_RIGHT"].ToString();
                        objRecordContent.m_strInHospitalCase = dtbValue.Rows[j]["INHOSPITALCASE"].ToString();
                        objRecordContent.m_strInHospitalCaseXML = dtbValue.Rows[j]["INHOSPITALCASEXML"].ToString();
                        objRecordContent.m_strInHospitalDiagnose_Right = dtbValue.Rows[j]["INHOSPITALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strInHospitalDiagnose = dtbValue.Rows[j]["INHOSPITALDIAGNOSE"].ToString();
                        objRecordContent.m_strInHospitalDiagnoseXML = dtbValue.Rows[j]["INHOSPITALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDiagnoseBy_Right = dtbValue.Rows[j]["DIAGNOSEBY_RIGHT"].ToString();
                        objRecordContent.m_strDiagnoseBy = dtbValue.Rows[j]["DIAGNOSEBY"].ToString();
                        objRecordContent.m_strDiagnoseByXML = dtbValue.Rows[j]["DIAGNOSEBYXML"].ToString();
                        objRecordContent.m_strCurrentCase_Right = dtbValue.Rows[j]["CURRENTCASE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentCase = dtbValue.Rows[j]["CURRENTCASE"].ToString();
                        objRecordContent.m_strCurrentCaseXML = dtbValue.Rows[j]["CURRENTCASEXML"].ToString();
                        objRecordContent.m_strCurrentDiagnose_Right = dtbValue.Rows[j]["CURRENTDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentDiagnose = dtbValue.Rows[j]["CURRENTDIAGNOSE"].ToString();
                        objRecordContent.m_strCurrentDiagnoseXML = dtbValue.Rows[j]["CURRENTDIAGNOSEXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.DiseaseSummary;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取转出记录
        /// <summary>
        /// 获取转出记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetConveyRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Convey, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsConveyRecordContent objRecordContent = new clsConveyRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strConveyDiagnose_Right = dtbValue.Rows[j]["CONVEYDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strConveyDiagnose = dtbValue.Rows[j]["CONVEYDIAGNOSE"].ToString();
                        objRecordContent.m_strConveyDiagnoseXML = dtbValue.Rows[j]["CONVEYDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXML = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();
                        objRecordContent.m_strConsultation_Right = dtbValue.Rows[j]["CONSULTATION_RIGHT"].ToString();
                        objRecordContent.m_strConsultation = dtbValue.Rows[j]["CONSULTATION"].ToString();
                        objRecordContent.m_strConsultationXML = dtbValue.Rows[j]["CONSULTATIONXML"].ToString();
                        objRecordContent.m_strConveyReason_Right = dtbValue.Rows[j]["CONVEYREASON_RIGHT"].ToString();
                        objRecordContent.m_strConveyReason = dtbValue.Rows[j]["CONVEYREASON"].ToString();
                        objRecordContent.m_strConveyReasonXML = dtbValue.Rows[j]["CONVEYREASONXML"].ToString();
                        objRecordContent.m_strNotice_Right = dtbValue.Rows[j]["NOTICE_RIGHT"].ToString();
                        objRecordContent.m_strNotice = dtbValue.Rows[j]["NOTICE"].ToString();
                        objRecordContent.m_strNoticeXML = dtbValue.Rows[j]["NOTICEXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        //						objRecordContent.m_strMainDoctorID=dtbValue.Rows[j]["MAINDOCTORID"].ToString();
                        //						objRecordContent.m_strMainDoctorName=dtbValue.Rows[j]["MAINDOCTORNAME"].ToString();

                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.Convey;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取转入记录
        /// <summary>
        /// 获取转入记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetTurnInRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_TurnIn, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsTurnInRecordContent objRecordContent = new clsTurnInRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strCaseBeforeTurnIn_Right = dtbValue.Rows[j]["CASEBEFORETURNIN_RIGHT"].ToString();
                        objRecordContent.m_strCaseBeforeTurnIn = dtbValue.Rows[j]["CASEBEFORETURNIN"].ToString();
                        objRecordContent.m_strCaseBeforeTurnInXML = dtbValue.Rows[j]["CASEBEFORETURNINXML"].ToString();
                        objRecordContent.m_strTurnInReason_Right = dtbValue.Rows[j]["TURNINREASON_RIGHT"].ToString();
                        objRecordContent.m_strTurnInReason = dtbValue.Rows[j]["TURNINREASON"].ToString();
                        objRecordContent.m_strTurnInReasonXML = dtbValue.Rows[j]["TURNINREASONXML"].ToString();
                        objRecordContent.m_strCaseAfterTurnIn_Right = dtbValue.Rows[j]["CASEAFTERTURNIN_RIGHT"].ToString();
                        objRecordContent.m_strCaseAfterTurnIn = dtbValue.Rows[j]["CASEAFTERTURNIN"].ToString();
                        objRecordContent.m_strCaseAfterTurnInXML = dtbValue.Rows[j]["CASEAFTERTURNINXML"].ToString();
                        objRecordContent.m_strTurnInDiagnose_Right = dtbValue.Rows[j]["TURNINDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strTurnInDiagnose = dtbValue.Rows[j]["TURNINDIAGNOSE"].ToString();
                        objRecordContent.m_strTurnInDiagnoseXML = dtbValue.Rows[j]["TURNINDIAGNOSEXML"].ToString();
                        objRecordContent.m_strReferral_Right = dtbValue.Rows[j]["REFERRAL_RIGHT"].ToString();
                        objRecordContent.m_strReferral = dtbValue.Rows[j]["REFERRAL"].ToString();
                        objRecordContent.m_strReferralXML = dtbValue.Rows[j]["REFERRALXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.TurnIn;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取查房记录
        /// <summary>
        /// 获取查房记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetCheckRoomRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate ;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_CheckRoom, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsCheckRoomRecordContent objRecordContent = new clsCheckRoomRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strPatientState_Right = dtbValue.Rows[j]["PATIENTSTATE_RIGHT"].ToString();
                        objRecordContent.m_strPatientState = dtbValue.Rows[j]["PATIENTSTATE"].ToString();
                        objRecordContent.m_strPatientStateXML = dtbValue.Rows[j]["PATIENTSTATEXML"].ToString();
                        objRecordContent.m_strDiagnose_Right = dtbValue.Rows[j]["DIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDiagnose = dtbValue.Rows[j]["DIAGNOSE"].ToString();
                        objRecordContent.m_strDiagnoseXML = dtbValue.Rows[j]["DIAGNOSEXML"].ToString();
                        objRecordContent.m_strDifferentiateDiagnose_Right = dtbValue.Rows[j]["DIFFERENTIATEDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDifferentiateDiagnose = dtbValue.Rows[j]["DIFFERENTIATEDIAGNOSE"].ToString();
                        objRecordContent.m_strDifferentiateDiagnoseXML = dtbValue.Rows[j]["DIFFERENTIATEDIAGNOSEXML"].ToString();
                        objRecordContent.m_strCurrentCure_Right = dtbValue.Rows[j]["CURRENTCURE_RIGHT"].ToString();
                        objRecordContent.m_strCurrentCure = dtbValue.Rows[j]["CURRENTCURE"].ToString();
                        objRecordContent.m_strCurrentCureXML = dtbValue.Rows[j]["CURRENTCUREXML"].ToString();
                        objRecordContent.m_strNextCure_Right = dtbValue.Rows[j]["NEXTCURE_RIGHT"].ToString();
                        objRecordContent.m_strNextCure = dtbValue.Rows[j]["NEXTCURE"].ToString();
                        objRecordContent.m_strNextCureXML = dtbValue.Rows[j]["NEXTCUREXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.CheckRoom;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取病例讨论记录
        /// <summary>
        /// 获取病例讨论记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetCaseDiscussRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                 objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_CaseDiscuss, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsCaseDiscussRecordContent objRecordContent = new clsCaseDiscussRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAddress_Right = dtbValue.Rows[j]["ADDRESS_RIGHT"].ToString();
                        objRecordContent.m_strAddress = dtbValue.Rows[j]["ADDRESS"].ToString();
                        objRecordContent.m_strAddressXML = dtbValue.Rows[j]["ADDRESSXML"].ToString();
                        objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[j]["DISCUSSCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strDiscussContent = dtbValue.Rows[j]["DISCUSSCONTENT"].ToString();
                        objRecordContent.m_strDiscussContentXML = dtbValue.Rows[j]["DISCUSSCONTENTXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();

                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.CaseDiscuss;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取术前讨论记录
        /// <summary>
        /// 获取术前讨论记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetBeforeOperationDiscussRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_BeforeOperationDiscuss, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsBeforeOperationDiscussRecordContent objRecordContent = new clsBeforeOperationDiscussRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAddress_Right = dtbValue.Rows[j]["ADDRESS_RIGHT"].ToString();
                        objRecordContent.m_strAddress = dtbValue.Rows[j]["ADDRESS"].ToString();
                        objRecordContent.m_strAddressXML = dtbValue.Rows[j]["ADDRESSXML"].ToString();
                        objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[j]["DISCUSSCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strDiscussContent = dtbValue.Rows[j]["DISCUSSCONTENT"].ToString();
                        objRecordContent.m_strDiscussContentXML = dtbValue.Rows[j]["DISCUSSCONTENTXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();


                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.BeforeOperationDiscuss;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取死亡病例讨论记录
        /// <summary>
        /// 获取死亡病例讨论记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetDeadCaseDiscussRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_DeadCaseDiscuss, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsDeadCaseDiscussRecordContent objRecordContent = new clsDeadCaseDiscussRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAddress_Right = dtbValue.Rows[j]["ADDRESS_RIGHT"].ToString();
                        objRecordContent.m_strAddress = dtbValue.Rows[j]["ADDRESS"].ToString();
                        objRecordContent.m_strAddressXML = dtbValue.Rows[j]["ADDRESSXML"].ToString();
                        objRecordContent.m_strDiscussContent_Right = dtbValue.Rows[j]["DISCUSSCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strDiscussContent = dtbValue.Rows[j]["DISCUSSCONTENT"].ToString();
                        objRecordContent.m_strDiscussContentXML = dtbValue.Rows[j]["DISCUSSCONTENTXML"].ToString();
                        objRecordContent.m_strDeadDiagnose_Right = dtbValue.Rows[j]["DEADDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDeadDiagnose = dtbValue.Rows[j]["DEADDIAGNOSE"].ToString();
                        objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[j]["DEADDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDeadReason_Right = dtbValue.Rows[j]["DEADREASON_RIGHT"].ToString();
                        objRecordContent.m_strDeadReason = dtbValue.Rows[j]["DEADREASON"].ToString();
                        objRecordContent.m_strDeadReasonXML = dtbValue.Rows[j]["DEADREASONXML"].ToString();
                        objRecordContent.m_strUseForReference_Right = dtbValue.Rows[j]["USEFORREFERENCE_RIGHT"].ToString();
                        objRecordContent.m_strUseForReference = dtbValue.Rows[j]["USEFORREFERENCE"].ToString();
                        objRecordContent.m_strUseForReferenceXML = dtbValue.Rows[j]["USEFORREFERENCEXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();


                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.DeadCaseDiscuss;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取死亡记录
        /// <summary>
        /// 获取死亡记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetDeadRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate ;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Dead, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsDeadRecordContent objRecordContent = new clsDeadRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_dtmDeadDate = DateTime.Parse(dtbValue.Rows[j]["DEADDATE"].ToString());
                        objRecordContent.m_strInHospitalCase_Right = dtbValue.Rows[j]["INHOSPITALCASE_RIGHT"].ToString();
                        objRecordContent.m_strInHospitalCase = dtbValue.Rows[j]["INHOSPITALCASE"].ToString();
                        objRecordContent.m_strInHospitalCaseXML = dtbValue.Rows[j]["INHOSPITALCASEXML"].ToString();
                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["ORIGINALDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["ORIGINALDIAGNOSE"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["ORIGINALDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDiagnoseBy_Right = dtbValue.Rows[j]["DIAGNOSEBY_RIGHT"].ToString();
                        objRecordContent.m_strDiagnoseBy = dtbValue.Rows[j]["DIAGNOSEBY"].ToString();
                        objRecordContent.m_strDiagnoseByXML = dtbValue.Rows[j]["DIAGNOSEBYXML"].ToString();
                        objRecordContent.m_strDeadDiagnose_Right = dtbValue.Rows[j]["DEADDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strDeadDiagnose = dtbValue.Rows[j]["DEADDIAGNOSE"].ToString();
                        objRecordContent.m_strDeadDiagnoseXML = dtbValue.Rows[j]["DEADDIAGNOSEXML"].ToString();
                        objRecordContent.m_strDeadReason_Right = dtbValue.Rows[j]["DEADREASON_RIGHT"].ToString();
                        objRecordContent.m_strDeadReason = dtbValue.Rows[j]["DEADREASON"].ToString();
                        objRecordContent.m_strDeadReasonXML = dtbValue.Rows[j]["DEADREASONXML"].ToString();
                        objRecordContent.m_strExperience_Right = dtbValue.Rows[j]["EXPERIENCE_RIGHT"].ToString();
                        objRecordContent.m_strExperience = dtbValue.Rows[j]["EXPERIENCE"].ToString();
                        objRecordContent.m_strExperienceXML = dtbValue.Rows[j]["EXPERIENCEXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();

                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.Dead;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取手术后病程记录
        /// <summary>
        /// 获取手术后病程记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetAfterOperationRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate ;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_AfterOperation, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsAfterOperationRecordContent objRecordContent = new clsAfterOperationRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_strAnaesthesiaMode_Right = dtbValue.Rows[j]["ANAESTHESIAMODE_RIGHT"].ToString();
                        objRecordContent.m_strAnaesthesiaMode = dtbValue.Rows[j]["ANAESTHESIAMODE"].ToString();
                        objRecordContent.m_strAnaesthesiaModeXML = dtbValue.Rows[j]["ANAESTHESIAMODEXML"].ToString();
                        objRecordContent.m_strOperationName_Right = dtbValue.Rows[j]["OPERATIONNAME_RIGHT"].ToString();
                        objRecordContent.m_strOperationName = dtbValue.Rows[j]["OPERATIONNAME"].ToString();
                        objRecordContent.m_strOperationNameXML = dtbValue.Rows[j]["OPERATIONNAMEXML"].ToString();
                        objRecordContent.m_strOperationDiagnose_Right = dtbValue.Rows[j]["OPERATIONDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strOperationDiagnose = dtbValue.Rows[j]["OPERATIONDIAGNOSE"].ToString();
                        objRecordContent.m_strOperationDiagnoseXML = dtbValue.Rows[j]["OPERATIONDIAGNOSEXML"].ToString();
                        objRecordContent.m_strInOperationSeeing_Right = dtbValue.Rows[j]["INOPERATIONSEEING_RIGHT"].ToString();
                        objRecordContent.m_strInOperationSeeing = dtbValue.Rows[j]["INOPERATIONSEEING"].ToString();
                        objRecordContent.m_strInOperationSeeingXML = dtbValue.Rows[j]["INOPERATIONSEEINGXML"].ToString();
                        objRecordContent.m_strAfterOperationDeal_Right = dtbValue.Rows[j]["AFTEROPERATIONDEAL_RIGHT"].ToString();
                        objRecordContent.m_strAfterOperationDeal = dtbValue.Rows[j]["AFTEROPERATIONDEAL"].ToString();
                        objRecordContent.m_strAfterOperationDealXML = dtbValue.Rows[j]["AFTEROPERATIONDEALXML"].ToString();

                        objRecordContent.m_strAfterOperationNotice_Right = dtbValue.Rows[j]["AFTEROPERATIONNOTICE_RIGHT"].ToString();
                        objRecordContent.m_strAfterOperationNotice = dtbValue.Rows[j]["AFTEROPERATIONNOTICE"].ToString();
                        objRecordContent.m_strAfterOperationNoticeXML = dtbValue.Rows[j]["AFTEROPERATIONNOTICEXML"].ToString();
                        objRecordContent.m_strCutHealUpStatus_Right = dtbValue.Rows[j]["CUTHEALUPSTATUS_RIGHT"].ToString();
                        objRecordContent.m_strCutHealUpStatus = dtbValue.Rows[j]["CUTHEALUPSTATUS"].ToString();
                        objRecordContent.m_strCutHealUpStatusXML = dtbValue.Rows[j]["CUTHEALUPSTATUSXML"].ToString();
                        if (dtbValue.Rows[j]["TAKEOUTSTITCHESDATE"].ToString() == "")
                            objRecordContent.m_dtmTakeOutStitchesDate = DateTime.MinValue;
                        else objRecordContent.m_dtmTakeOutStitchesDate = DateTime.Parse(dtbValue.Rows[j]["TAKEOUTSTITCHESDATE"].ToString());
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.AfterOperation;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取抢救记录
        /// <summary>
        /// 获取抢救记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetSaveRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate ;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Save, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsSaveRecordContent objRecordContent = new clsSaveRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_dtmSaveTime = DateTime.Parse(dtbValue.Rows[j]["SAVETIME"].ToString());
                        objRecordContent.m_strDiseaseName_Right = dtbValue.Rows[j]["DISEASENAME_RIGHT"].ToString();
                        objRecordContent.m_strDiseaseName = dtbValue.Rows[j]["DISEASENAME"].ToString();
                        objRecordContent.m_strDiseaseNameXML = dtbValue.Rows[j]["DISEASENAMEXML"].ToString();
                        objRecordContent.m_strDiseaseChangeCase_Right = dtbValue.Rows[j]["DISEASECHANGECASE_RIGHT"].ToString();
                        objRecordContent.m_strDiseaseChangeCase = dtbValue.Rows[j]["DISEASECHANGECASE"].ToString();
                        objRecordContent.m_strDiseaseChangeCaseXML = dtbValue.Rows[j]["DISEASECHANGECASEXML"].ToString();
                        objRecordContent.m_strSaveDeal_Right = dtbValue.Rows[j]["SAVEDEAL_RIGHT"].ToString();
                        objRecordContent.m_strSaveDeal = dtbValue.Rows[j]["SAVEDEAL"].ToString();
                        objRecordContent.m_strSaveDealXML = dtbValue.Rows[j]["SAVEDEALXML"].ToString();
                        objRecordContent.m_strSaveResult_Right = dtbValue.Rows[j]["SAVERESULT_RIGHT"].ToString();
                        objRecordContent.m_strSaveResult = dtbValue.Rows[j]["SAVERESULT"].ToString();
                        objRecordContent.m_strSaveResultXML = dtbValue.Rows[j]["SAVERESULTXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();

                        #endregion 从DataTable.Rows中获取结果
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }
                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.Save;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取首次病程记录
        /// <summary>
        /// 获取首次病程记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetFirstIllnessNoteRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                /*DataTable*/
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                /*long*/
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_FirstIllnessNote, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsFirstIllnessNoteRecordContent objRecordContent = new clsFirstIllnessNoteRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OpenDate_Main"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CreateDate"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["ModifyDate"].ToString());

                        if (dtbValue.Rows[j]["FirstPrintDate"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FirstPrintDate"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["ModifyUserID"].ToString();
                        if (dtbValue.Rows[j]["IfConfirm"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IfConfirm"].ToString());
                        if (dtbValue.Rows[j]["Status"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["Status"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["ConfirmReason"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["ConfirmReasonXML"].ToString();

                        objRecordContent.m_strMostlyContent_Right = dtbValue.Rows[j]["MostlyContent_Right"].ToString();
                        objRecordContent.m_strMostlyContent = dtbValue.Rows[j]["MostlyContent"].ToString();
                        objRecordContent.m_strMostlyContentXML = dtbValue.Rows[j]["MostlyContentXML"].ToString();
                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["OriginalDiagnose_Right"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["OriginalDiagnose"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["OriginalDiagnoseXML"].ToString();
                        objRecordContent.m_strThereunderDiagnose_Right = dtbValue.Rows[j]["ThereunderDiagnose_Right"].ToString();
                        objRecordContent.m_strThereunderDiagnose = dtbValue.Rows[j]["ThereunderDiagnose"].ToString();
                        objRecordContent.m_strThereunderDiagnoseXML = dtbValue.Rows[j]["ThereunderDiagnoseXML"].ToString();
                        objRecordContent.m_strDiagnoseDiffe_Right = dtbValue.Rows[j]["DiagnoseDiffe_Right"].ToString();
                        objRecordContent.m_strDiagnoseDiffe = dtbValue.Rows[j]["DiagnoseDiffe"].ToString();
                        objRecordContent.m_strDiagnoseDiffeXML = dtbValue.Rows[j]["DiagnoseDiffeXML"].ToString();
                        objRecordContent.m_strCurePlan_Right = dtbValue.Rows[j]["CurePlan_Right"].ToString();
                        objRecordContent.m_strCurePlan = dtbValue.Rows[j]["CurePlan"].ToString();
                        objRecordContent.m_strCurePlanXML = dtbValue.Rows[j]["CurePlanXML"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        #endregion 从DataTable.Rows中获取结果

                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value || dtbValue.Rows[j]["SEQUENCE_INT"].ToString().Length != 0)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);
                        }

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.FirstIllnessNote;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取首次病程记录(市一中医科)
        /// <summary>
        /// 获取首次病程记录(市一中医科)
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetFirstIllnessNote_ZYRecord(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_FirstIllnessNote_ZY, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsFirstIllnessNote_ZYRecordContent objRecordContent = new clsFirstIllnessNote_ZYRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OpenDate_Main"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CreateDate"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["ModifyDate"].ToString());

                        if (dtbValue.Rows[j]["FirstPrintDate"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FirstPrintDate"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CreateUserID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["ModifyUserID"].ToString();
                        if (dtbValue.Rows[j]["IfConfirm"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IfConfirm"].ToString());
                        if (dtbValue.Rows[j]["Status"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["Status"].ToString());
                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["ConfirmReason"].ToString();
                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["ConfirmReasonXML"].ToString();

                        objRecordContent.m_strMostlyContent_Right = dtbValue.Rows[j]["MostlyContent_Right"].ToString();
                        objRecordContent.m_strMostlyContent = dtbValue.Rows[j]["MostlyContent"].ToString();
                        objRecordContent.m_strMostlyContentXML = dtbValue.Rows[j]["MostlyContentXML"].ToString();
                        objRecordContent.m_strOriginalDiagnose_Right = dtbValue.Rows[j]["OriginalDiagnose_Right"].ToString();
                        objRecordContent.m_strOriginalDiagnose = dtbValue.Rows[j]["OriginalDiagnose"].ToString();
                        objRecordContent.m_strOriginalDiagnoseXML = dtbValue.Rows[j]["OriginalDiagnoseXML"].ToString();
                        objRecordContent.m_strThereunderDiagnose_Right = dtbValue.Rows[j]["ThereunderDiagnose_Right"].ToString();
                        objRecordContent.m_strThereunderDiagnose = dtbValue.Rows[j]["ThereunderDiagnose"].ToString();
                        objRecordContent.m_strThereunderDiagnoseXML = dtbValue.Rows[j]["ThereunderDiagnoseXML"].ToString();
                        objRecordContent.m_strDiagnoseDiffe_Right = dtbValue.Rows[j]["DiagnoseDiffe_Right"].ToString();
                        objRecordContent.m_strDiagnoseDiffe = dtbValue.Rows[j]["DiagnoseDiffe"].ToString();
                        objRecordContent.m_strDiagnoseDiffeXML = dtbValue.Rows[j]["DiagnoseDiffeXML"].ToString();
                        objRecordContent.m_strCurePlan_Right = dtbValue.Rows[j]["CurePlan_Right"].ToString();
                        objRecordContent.m_strCurePlan = dtbValue.Rows[j]["CurePlan"].ToString();
                        objRecordContent.m_strCurePlanXML = dtbValue.Rows[j]["CurePlanXML"].ToString();
                        objRecordContent.m_strIdentifyDiagnose_Right = dtbValue.Rows[j]["IDENTIFYDIAGNOSE_RIGHT"].ToString();
                        objRecordContent.m_strIdentifyDiagnos = dtbValue.Rows[j]["IDENTIFYDIAGNOSE"].ToString();
                        objRecordContent.m_strIdentifyDiagnoseXML = dtbValue.Rows[j]["IDENTIFYDIAGNOSEXML"].ToString();
                        objRecordContent.m_strIdentifyReston_Right = dtbValue.Rows[j]["IDENTIFYRESTON_RIGHT"].ToString();
                        objRecordContent.m_strIdentifyReston = dtbValue.Rows[j]["IDENTIFYRESTON"].ToString();
                        objRecordContent.m_strIdentifyRestonXML = dtbValue.Rows[j]["IDENTIFYRESTONXML"].ToString();

                        #endregion 从DataTable.Rows中获取结果

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.FirstIllnessNote_ZY;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 获取术前小结
        /// <summary>
        /// 获取术前小结
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_arlTransData"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetSummaryBeforeOP(string p_strInPatientID, DateTime p_dtmInpatientDate, ref ArrayList p_arlTransData)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_SummaryBeforeOP, ref dtbValue, objDPArr);

                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        #region 从DataTable.Rows中获取结果
                        //设置结果
                        clsEMR_SummaryBeforeOPValue objRecordContent = new clsEMR_SummaryBeforeOPValue();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = p_dtmInpatientDate;
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[j]["OPENDATE"]);

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"] == DBNull.Value)
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[j]["STATUS"] == DBNull.Value)
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
                        if (dtbValue.Rows[j]["EMR_SEQ"] == DBNull.Value)
                            return -1;
                        objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[j]["EMR_SEQ"]);

                        if (dtbValue.Rows[j]["MARKSTATUS"] == DBNull.Value)
                        {
                            objRecordContent.m_intMarkStatus = 0;
                        }
                        else
                        {
                            objRecordContent.m_intMarkStatus = Convert.ToInt32(dtbValue.Rows[j]["MARKSTATUS"]);
                        }
                        objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[j]["RECORDDATE"]);
                        objRecordContent.m_strREGISTERID_CHR = dtbValue.Rows[j]["REGISTERID_CHR"].ToString();
                        objRecordContent.m_strDISEASESUMMARY = dtbValue.Rows[j]["DISEASESUMMARY"].ToString();
                        objRecordContent.m_strDISEASESUMMARYXML = dtbValue.Rows[j]["DISEASESUMMARYXML"].ToString();
                        objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValue.Rows[j]["DIAGNOSISBEFOREOP"].ToString();
                        objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValue.Rows[j]["DIAGNOSISBEFOREOPXML"].ToString();
                        objRecordContent.m_strDIAGNOSISGIST = dtbValue.Rows[j]["DIAGNOSISGIST"].ToString();
                        objRecordContent.m_strDIAGNOSISGISTXML = dtbValue.Rows[j]["DIAGNOSISGISTXML"].ToString();
                        objRecordContent.m_strOPINDICATION = dtbValue.Rows[j]["OPINDICATION"].ToString();
                        objRecordContent.m_strOPINDICATIONXML = dtbValue.Rows[j]["OPINDICATIONXML"].ToString();
                        objRecordContent.m_strOPMODE = dtbValue.Rows[j]["OPMODE"].ToString();
                        objRecordContent.m_strOPMODEXML = dtbValue.Rows[j]["OPMODEXML"].ToString();
                        objRecordContent.m_strANAMODE = dtbValue.Rows[j]["ANAMODE"].ToString();
                        objRecordContent.m_strANAMODEXML = dtbValue.Rows[j]["ANAMODEXML"].ToString();
                        objRecordContent.m_strPROCEEDING = dtbValue.Rows[j]["PROCEEDING"].ToString();
                        objRecordContent.m_strPROCEEDINGXML = dtbValue.Rows[j]["PROCEEDINGXML"].ToString();
                        objRecordContent.m_strPREPAREBEFOREOP = dtbValue.Rows[j]["PREPAREBEFOREOP"].ToString();
                        objRecordContent.m_strPREPAREBEFOREOPXML = dtbValue.Rows[j]["PREPAREBEFOREOPXML"].ToString();

                        objRecordContent.m_strDISEASESUMMARY_RIGHT = dtbValue.Rows[j]["DISEASESUMMARY_RIGHT"].ToString();
                        objRecordContent.m_strDIAGNOSISBEFOREOP_RIGHT = dtbValue.Rows[j]["DIAGNOSISBEFOREOP_RIGHT"].ToString();
                        objRecordContent.m_strDIAGNOSISGIST_RIGHT = dtbValue.Rows[j]["DIAGNOSISGIST_RIGHT"].ToString();
                        objRecordContent.m_strOPINDICATION_RIGHT = dtbValue.Rows[j]["OPINDICATION_RIGHT"].ToString();
                        objRecordContent.m_strOPMODE_RIGHT = dtbValue.Rows[j]["OPMODE_RIGHT"].ToString();
                        objRecordContent.m_strANAMODE_RIGHT = dtbValue.Rows[j]["ANAMODE_RIGHT"].ToString();
                        objRecordContent.m_strPROCEEDING_RIGHT = dtbValue.Rows[j]["PROCEEDING_RIGHT"].ToString();
                        objRecordContent.m_strPREPAREBEFOREOP_RIGHT = dtbValue.Rows[j]["PREPAREBEFOREOP_RIGHT"].ToString();
                        objRecordContent.m_StrPagination = dtbValue.Rows[j]["PAGINATION"].ToString();
                        //获取签名集合
                        if (dtbValue.Rows[j]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign_SummaryBeforeOP = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign_SummaryBeforeOP.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                            //释放
                            objSign_SummaryBeforeOP = null;
                        }

                        #endregion 从DataTable.Rows中获取结果

                        //生成 clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.EMR_SummaryBeforeOP;

                        //设置结果到 objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        p_arlTransData.Add(objInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 查看当前记录是否最新的记录
        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数          
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //根据不同的子表单，获取不同的SQL语句
                string strSQL = null;
                switch ((enmDiseaseTrackType)p_intRecordType)
                {
                    case enmDiseaseTrackType.GeneralDisease:
                        strSQL = c_strCheckLastModifyRecordSQL_Normal;
                        break;
                    case enmDiseaseTrackType.HandOver:
                        strSQL = c_strCheckLastModifyRecordSQL_HandOver;
                        break;
                    case enmDiseaseTrackType.TakeOver:
                        strSQL = c_strCheckLastModifyRecordSQL_TakeOver;
                        break;
                    case enmDiseaseTrackType.Consultation:
                        strSQL = c_strCheckLastModifyRecordSQL_Consultation;
                        break;
                    case enmDiseaseTrackType.DiseaseSummary:
                        strSQL = c_strCheckLastModifyRecordSQL_DiseaseSummary;
                        break;
                    case enmDiseaseTrackType.Convey:
                        strSQL = c_strCheckLastModifyRecordSQL_Convey;
                        break;
                    case enmDiseaseTrackType.TurnIn:
                        strSQL = c_strCheckLastModifyRecordSQL_TurnIn;
                        break;
                    case enmDiseaseTrackType.CheckRoom:
                        strSQL = c_strCheckLastModifyRecordSQL_CheckRoom;
                        break;
                    case enmDiseaseTrackType.CaseDiscuss:
                        strSQL = c_strCheckLastModifyRecordSQL_CaseDiscuss;
                        break;
                    case enmDiseaseTrackType.BeforeOperationDiscuss:
                        strSQL = c_strCheckLastModifyRecordSQL_BeforeOperationDiscuss;
                        break;
                    case enmDiseaseTrackType.DeadCaseDiscuss:
                        strSQL = c_strCheckLastModifyRecordSQL_DeadCaseDiscuss;
                        break;
                    case enmDiseaseTrackType.Dead:
                        strSQL = c_strCheckLastModifyRecordSQL_Dead;
                        break;
                    case enmDiseaseTrackType.OutHospital:
                        strSQL = c_strCheckLastModifyRecordSQL_OutHospital;
                        break;
                    case enmDiseaseTrackType.AfterOperation:
                        strSQL = c_strCheckLastModifyRecordSQL_AfterOperation;
                        break;
                    case enmDiseaseTrackType.Save:
                        strSQL = c_strCheckLastModifyRecordSQL_Save;
                        break;
                    case enmDiseaseTrackType.FirstIllnessNote:
                        strSQL = c_strCheckLastModifyRecordSQL_FirstIllnessNotel;
                        break;
                    case enmDiseaseTrackType.FirstIllnessNote_ZY:
                        strSQL = c_strCheckLastModifyRecordSQL_FirstIllnessNotel_ZY;
                        break;
                    case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                        strSQL = c_strCheckLastModifyRecordSQL_SummaryBeforeOP;
                        break;

                    default: return (long)enmOperationResult.Parameter_Error;
                }


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from ConsultationRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    //根据不同的子表单，获取不同的SQL语句
                    string strSQL2 = null;
                    switch ((enmDiseaseTrackType)p_intRecordType)
                    {
                        case enmDiseaseTrackType.GeneralDisease:
                            strSQL2 = c_strGetDeleteRecordSQL_Normal;
                            break;
                        case enmDiseaseTrackType.HandOver:
                            strSQL2 = c_strGetDeleteRecordSQL_HandOver;
                            break;
                        case enmDiseaseTrackType.TakeOver:
                            strSQL2 = c_strGetDeleteRecordSQL_TakeOver;
                            break;
                        case enmDiseaseTrackType.Consultation:
                            strSQL2 = c_strGetDeleteRecordSQL_Consultation;
                            break;
                        case enmDiseaseTrackType.DiseaseSummary:
                            strSQL2 = c_strGetDeleteRecordSQL_DiseaseSummary;
                            break;
                        case enmDiseaseTrackType.Convey:
                            strSQL2 = c_strGetDeleteRecordSQL_Convey;
                            break;
                        case enmDiseaseTrackType.TurnIn:
                            strSQL2 = c_strGetDeleteRecordSQL_TurnIn;
                            break;
                        case enmDiseaseTrackType.CheckRoom:
                            strSQL2 = c_strGetDeleteRecordSQL_CheckRoom;
                            break;
                        case enmDiseaseTrackType.CaseDiscuss:
                            strSQL2 = c_strGetDeleteRecordSQL_CaseDiscuss;
                            break;
                        case enmDiseaseTrackType.BeforeOperationDiscuss:
                            strSQL2 = c_strGetDeleteRecordSQL_BeforeOperationDiscuss;
                            break;
                        case enmDiseaseTrackType.DeadCaseDiscuss:
                            strSQL2 = c_strGetDeleteRecordSQL_DeadCaseDiscuss;
                            break;
                        case enmDiseaseTrackType.Dead:
                            strSQL2 = c_strGetDeleteRecordSQL_Dead;
                            break;
                        case enmDiseaseTrackType.OutHospital:
                            strSQL2 = c_strGetDeleteRecordSQL_OutHospital;
                            break;
                        case enmDiseaseTrackType.AfterOperation:
                            strSQL2 = c_strGetDeleteRecordSQL_AfterOperation;
                            break;
                        case enmDiseaseTrackType.Save:
                            strSQL2 = c_strGetDeleteRecordSQL_Save;
                            break;
                        case enmDiseaseTrackType.FirstIllnessNote:
                            strSQL2 = c_strGetDeleteRecordSQL_FirstIllnessNote;
                            break;
                        case enmDiseaseTrackType.FirstIllnessNote_ZY:
                            strSQL2 = c_strGetDeleteRecordSQL_FirstIllnessNote_ZY;
                            break;
                        case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                            strSQL2 = c_strGetDeleteRecordSQL_SummaryBeforeOP;
                            break;

                        default: return (long)enmOperationResult.Parameter_Error;
                    }

                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL2, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
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
            //返回
            return lngRes;


        } 
        #endregion

        #region 把记录从数据中“删除”
        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //根据不同的子表单，获取不同的SQL语句
                string strSQL = null;
                switch ((enmDiseaseTrackType)p_intRecordType)
                {
                    case enmDiseaseTrackType.GeneralDisease:
                        strSQL = c_strDeleteRecordSQL_Normal;
                        break;
                    case enmDiseaseTrackType.HandOver:
                        strSQL = c_strDeleteRecordSQL_HandOver;
                        break;
                    case enmDiseaseTrackType.TakeOver:
                        strSQL = c_strDeleteRecordSQL_TakeOver;
                        break;
                    case enmDiseaseTrackType.Consultation:
                        strSQL = c_strDeleteRecordSQL_Consultation;
                        break;
                    case enmDiseaseTrackType.DiseaseSummary:
                        strSQL = c_strDeleteRecordSQL_DiseaseSummary;
                        break;
                    case enmDiseaseTrackType.Convey:
                        strSQL = c_strDeleteRecordSQL_Convey;
                        break;
                    case enmDiseaseTrackType.TurnIn:
                        strSQL = c_strDeleteRecordSQL_TurnIn;
                        break;
                    case enmDiseaseTrackType.CheckRoom:
                        strSQL = c_strDeleteRecordSQL_CheckRoom;
                        break;
                    case enmDiseaseTrackType.CaseDiscuss:
                        strSQL = c_strDeleteRecordSQL_CaseDiscuss;
                        break;
                    case enmDiseaseTrackType.BeforeOperationDiscuss:
                        strSQL = c_strDeleteRecordSQL_BeforeOperationDiscuss;
                        break;
                    case enmDiseaseTrackType.DeadCaseDiscuss:
                        strSQL = c_strDeleteRecordSQL_DeadCaseDiscuss;
                        break;
                    case enmDiseaseTrackType.Dead:
                        strSQL = c_strDeleteRecordSQL_Dead;
                        break;
                    case enmDiseaseTrackType.OutHospital:
                        strSQL = c_strDeleteRecordSQL_OutHospital;
                        break;
                    case enmDiseaseTrackType.AfterOperation:
                        strSQL = c_strDeleteRecordSQL_AfterOperation;
                        break;
                    case enmDiseaseTrackType.Save:
                        strSQL = c_strDeleteRecordSQL_Save;
                        break;
                    case enmDiseaseTrackType.FirstIllnessNote:
                        strSQL = c_strDeleteRecordSQL_FirstIllnessNote;
                        break;
                    case enmDiseaseTrackType.FirstIllnessNote_ZY:
                        strSQL = c_strDeleteRecordSQL_FirstIllnessNote_ZY;
                        break;
                    case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                        strSQL = c_strDeleteRecordSQL_SummaryBeforeOP;
                        break;

                    default: return (long)enmOperationResult.Parameter_Error;
                }


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

                //执行SQL
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

            }			//返回
            return lngRes;


        } 
        #endregion

	}// END CLASS DEFINITION clsMainDiseaseTrackService

}
