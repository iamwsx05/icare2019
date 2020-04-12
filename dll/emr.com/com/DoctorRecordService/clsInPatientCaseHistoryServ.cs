using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.BaseCaseHistorySevice;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Drawing;
using System.Collections; 

namespace com.digitalwave.InPatientCaseHistoryServ
{
	/// <summary>
	/// Summary description for Class1.
	/// 普通住院病历
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]	
	public class clsInPatientCaseHistoryServ:clsBaseCaseHistorySevice
	{
		public clsInPatientCaseHistoryServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private const string c_strGetTimeListSQL="select inpatientdate,createdate,opendate from inpatientcasehistory_history where inpatientid = ?  and status=0 order by opendate desc";
		
		/// <summary>
		/// 获取画图信息
		/// </summary>
        private const string c_strGetRecordContent_PictureSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       picid,
       backimage,
       frontimage,
       backcolor,
       width,
       height from inpatientcasehistory_picture where inpatientid=? and inpatientdate=?";

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
       a.credibility,
       a.representor,
       a.primarydiagnosedocid,
       a.primarydiagnosedate,
       a.finallydiagnosedocid,
       a.finallydiagnosedate,
       a.maindescriptionxml,
       a.maindescriptionall,
       a.currentstatusxml,
       a.currentstatusall,
       a.beforetimestatusxml,
       a.beforetimestatusall,
       a.ownhistoryxml,
       a.ownhistoryall,
       a.marriagehistoryxml,
       a.marriagehistoryall,
       a.catameniahistoryxml,
       a.catameniahistoryall,
       a.familyhistoryxml,
       a.familyhistoryall,
       a.summaryxml,
       a.summaryall,
       a.primarydiagnosexml,
       a.primarydiagnoseall,
       a.finallydiagnosexml,
       a.finallydiagnoseall,
       a.temperaturexml,
       a.temperatureall,
       a.pulsexml,
       a.pulseall,
       a.breathxml,
       a.breathall,
       a.sysxml,
       a.sysall,
       a.diaxml,
       a.diaall,
       a.bloodpressureunitxml,
       a.bloodpressureunitall,
       a.medicalxml,
       a.medicalall,
       a.professionalcheckxml,
       a.professionalcheckall,
       a.labcheckall,
       a.labcheckxml,
       a.yjsall,
       a.yjsxml,
       a.contrahistoryall,
       a.contrahistoryxml,
       a.shysall,
       a.shysxml,
       a.lcqkall,
       a.lcqkxml,
       a.cqjcall,
       a.cqjcxml,
       a.careplanall,
       a.careplanxml,
       a.oldmaternitysufferall,
       a.oldmaternitysufferxml,
       a.modifydiagnoseall,
       a.modifydiagnosexml,
       a.modifydiagnosedoctorid,
       a.modifydiagnosedate,
       a.adddiagnoseall,
       a.adddiagnosexml,
       a.adddiagnosedoctorid,
       a.adddiagnosedate,
       a.selectedmc,
       a.selectedlastcatameniatime,
       a.selectedameniaage,
       a.ameniaage,
       a.normaldiagnoseall,
       a.normaldiagnoseallxml,
       a.normaldiagnosealldictirid,
       a.normaldiagnosealldate,
       a.weightall,
       a.weightxml,
       a.buchongall,
       a.buchongxml,
       a.buchongid,
       a.buchongdate,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.buchong,
       b.adddiagnose,
       b.diagnosenor,
       b.bloodpressure,b.weight,
       pbi.lastname_vchr as firstname
  from inpatientcasehistory_history a,
       ipcasehistory_historycontent b,
       t_bse_employee               pbi
 where a.inpatientid = ?
   and a.inpatientdate = ?
      
   and a.status = 0
   and a.createuserid = pbi.empno_chr
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.lastmodifydate = (select max(lastmodifydate)
                             from ipcasehistory_historycontent
                            where inpatientid = a.inpatientid
                              and inpatientdate = a.inpatientdate
                              and opendate = a.opendate)";
        private const string c_strGetDeletedRecordContentSQL = @"select a.inpatientid,
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
       a.credibility,
       a.representor,
       a.primarydiagnosedocid,
       a.primarydiagnosedate,
       a.finallydiagnosedocid,
       a.finallydiagnosedate,
       a.maindescriptionxml,
       a.maindescriptionall,
       a.currentstatusxml,
       a.currentstatusall,
       a.beforetimestatusxml,
       a.beforetimestatusall,
       a.ownhistoryxml,
       a.ownhistoryall,
       a.marriagehistoryxml,
       a.marriagehistoryall,
       a.catameniahistoryxml,
       a.catameniahistoryall,
       a.familyhistoryxml,
       a.familyhistoryall,
       a.summaryxml,
       a.summaryall,
       a.primarydiagnosexml,
       a.primarydiagnoseall,
       a.finallydiagnosexml,
       a.finallydiagnoseall,
       a.temperaturexml,
       a.temperatureall,
       a.pulsexml,
       a.pulseall,
       a.breathxml,
       a.breathall,
       a.sysxml,
       a.sysall,
       a.diaxml,
       a.diaall,
       a.bloodpressureunitxml,
       a.bloodpressureunitall,
       a.medicalxml,
       a.medicalall,
       a.professionalcheckxml,
       a.professionalcheckall,
       a.labcheckall,
       a.labcheckxml,
       a.yjsall,
       a.yjsxml,
       a.contrahistoryall,
       a.contrahistoryxml,
       a.shysall,
       a.shysxml,
       a.lcqkall,
       a.lcqkxml,
       a.cqjcall,
       a.cqjcxml,
       a.careplanall,
       a.careplanxml,
       a.oldmaternitysufferall,
       a.oldmaternitysufferxml,
       a.modifydiagnoseall,
       a.modifydiagnosexml,
       a.modifydiagnosedoctorid,
       a.modifydiagnosedate,
       a.adddiagnoseall,
       a.adddiagnosexml,
       a.adddiagnosedoctorid,
       a.adddiagnosedate,
       a.selectedmc,
       a.selectedlastcatameniatime,
       a.selectedameniaage,
       a.ameniaage,
       a.normaldiagnoseall,
       a.normaldiagnoseallxml,
       a.normaldiagnosealldictirid,
       a.normaldiagnosealldate,
       a.buchongall,
       a.buchongxml,
       a.buchongid,
       a.buchongdate,
       a.weightall,
       a.weightxml,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.adddiagnose,
       b.diagnosenor,
       b.buchong,
       b.bloodpressure, 
       b.weight,
       pbi.lastname_vchr as firstname  
	from inpatientcasehistory_history a,ipcasehistory_historycontent b,t_bse_employee pbi 
	where a.inpatientid = ? 
	and a.inpatientdate= ?
	and a.opendate= ?
	and a.status=1
	and a.createuserid=pbi.empno_chr 
	and b.inpatientid=a.inpatientid														
	and b.inpatientdate=a.inpatientdate
	and b.opendate=a.opendate 
	and b.lastmodifydate=(select max(lastmodifydate)
	from ipcasehistory_historycontent 
	where inpatientid=a.inpatientid 
	and inpatientdate=a.inpatientdate 
	and opendate=a.opendate)";

        private const string c_strGetContentSQL_FromRevisit = @"select a.inpatientid,
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
       a.credibility,
       a.representor,
       a.primarydiagnosedocid,
       a.primarydiagnosedate,
       a.finallydiagnosedocid,
       a.finallydiagnosedate,
       a.maindescriptionxml,
       a.maindescriptionall,
       a.currentstatusxml,
       a.currentstatusall,
       a.beforetimestatusxml,
       a.beforetimestatusall,
       a.ownhistoryxml,
       a.ownhistoryall,
       a.marriagehistoryxml,
       a.marriagehistoryall,
       a.catameniahistoryxml,
       a.catameniahistoryall,
       a.familyhistoryxml,
       a.familyhistoryall,
       a.summaryxml,
       a.summaryall,
       a.primarydiagnosexml,
       a.primarydiagnoseall,
       a.finallydiagnosexml,
       a.finallydiagnoseall,
       a.temperaturexml,
       a.temperatureall,
       a.pulsexml,
       a.pulseall,
       a.breathxml,
       a.breathall,
       a.sysxml,
       a.sysall,
       a.diaxml,
       a.diaall,
       a.bloodpressureunitxml,
       a.bloodpressureunitall,
       a.medicalxml,
       a.medicalall,
       a.professionalcheckxml,
       a.professionalcheckall,
       a.labcheckall,
       a.labcheckxml,
       a.yjsall,
       a.yjsxml,
       a.contrahistoryall,
       a.contrahistoryxml,
       a.shysall,
       a.shysxml,
       a.lcqkall,
       a.lcqkxml,
       a.cqjcall,
       a.cqjcxml,
       a.careplanall,
       a.careplanxml,
       a.oldmaternitysufferall,
       a.oldmaternitysufferxml,
       a.modifydiagnoseall,
       a.modifydiagnosexml,
       a.modifydiagnosedoctorid,
       a.modifydiagnosedate,
       a.adddiagnoseall,
       a.adddiagnosexml,
       a.adddiagnosedoctorid,
       a.adddiagnosedate,
       a.selectedmc,
       a.selectedlastcatameniatime,
       a.selectedameniaage,
       a.ameniaage,
       a.normaldiagnoseall,
       a.normaldiagnoseallxml,
       a.normaldiagnosealldictirid,
       a.normaldiagnosealldate,
       a.buchongall,
       a.buchongxml,
       a.buchongid,
       a.buchongdate,
       a.weightall,
       a.weightxml
	from inpatientcasehistory_history a,ipcasehistory_historycontent b
	where a.inpatientid = ? 
	and a.inpatientdate= ?													
	and a.status=0														
	and b.inpatientid=a.inpatientid														
	and b.inpatientdate=a.inpatientdate
	and b.opendate=a.opendate 
	and b.lastmodifydate=(select max(lastmodifydate)
	from ipcasehistory_historycontent 
	where inpatientid=a.inpatientid 
	and inpatientdate=a.inpatientdate 
	and opendate=a.opendate)";

		#region old code
//		private const string c_strGetRecordContentSQL="select top 1 *  from InPatientCaseHistory_History as ms,IPCaseHistory_HistoryContent as co, t_bse_employee as EBI "+
//			"where ms.CreateUserID=EBI.EmployeeID and ms.InPatientID= ? and ms.InPatientDate = ? and ms.OpenDate = ? and ms.Status=0 "+
//			"and ms.InPatientID=co.InPatientID and ms.InPatientDate=co.InPatientDate and ms.OpenDate=co.OpenDate order by co.LastModifyDate desc";
		#endregion

        private const string c_strCheckCreateDateSQL = @"select a.inpatientid,
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
       a.credibility,
       a.representor,
       a.primarydiagnosedocid,
       a.primarydiagnosedate,
       a.finallydiagnosedocid,
       a.finallydiagnosedate,
       a.maindescriptionxml,
       a.maindescriptionall,
       a.currentstatusxml,
       a.currentstatusall,
       a.beforetimestatusxml,
       a.beforetimestatusall,
       a.ownhistoryxml,
       a.ownhistoryall,
       a.marriagehistoryxml,
       a.marriagehistoryall,
       a.catameniahistoryxml,
       a.catameniahistoryall,
       a.familyhistoryxml,
       a.familyhistoryall,
       a.summaryxml,
       a.summaryall,
       a.primarydiagnosexml,
       a.primarydiagnoseall,
       a.finallydiagnosexml,
       a.finallydiagnoseall,
       a.temperaturexml,
       a.temperatureall,
       a.pulsexml,
       a.pulseall,
       a.breathxml,
       a.breathall,
       a.sysxml,
       a.sysall,
       a.diaxml,
       a.diaall,
       a.bloodpressureunitxml,
       a.bloodpressureunitall,
       a.medicalxml,
       a.medicalall,
       a.professionalcheckxml,
       a.professionalcheckall,
       a.labcheckall,
       a.labcheckxml,
       a.yjsall,
       a.yjsxml,
       a.contrahistoryall,
       a.contrahistoryxml,
       a.shysall,
       a.shysxml,
       a.lcqkall,
       a.lcqkxml,
       a.cqjcall,
       a.cqjcxml,
       a.careplanall,
       a.careplanxml,
       a.oldmaternitysufferall,
       a.oldmaternitysufferxml,
       a.modifydiagnoseall,
       a.modifydiagnosexml,
       a.modifydiagnosedoctorid,
       a.modifydiagnosedate,
       a.adddiagnoseall,
       a.adddiagnosexml,
       a.adddiagnosedoctorid,
       a.adddiagnosedate,
       a.selectedmc,
       a.selectedlastcatameniatime,
       a.selectedameniaage,
       a.ameniaage,
       a.normaldiagnoseall,
       a.normaldiagnoseallxml,
       a.normaldiagnosealldictirid,
       a.normaldiagnosealldate,
       a.buchongall,
       a.buchongxml,
       a.buchongid,
       a.buchongdate,
       a.weightall,
       a.weightxml 
from  inpatientcasehistory_history a where a.inpatientid= ? and a.inpatientdate = ?  and a.status =0";

//		private const string c_strGetExistInfoSQL="";

		/// <summary>
		/// 更新GeneralDiseaseRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "update  inpatientcasehistory_history set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";


//		private const string c_strGetModifyRecordSQL="";
		
		private const string c_strGetDeleteRecordSQL="";

        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate, b.lastmodifydate
  from inpatientcasehistory_history a, ipcasehistory_historycontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.lastmodifydate = (select max(lastmodifydate)
                             from ipcasehistory_historycontent
                            where inpatientid = a.inpatientid
                              and inpatientdate = a.inpatientdate
                              and opendate = a.opendate)";

		private const string c_strModifyRecord_PictureSQL = "update inpatientcasehistory_picture set picid=?,frontimage=?,backcolor=?,width=?,height=? where inpatientid=? and inpatientdate=? and opendate=?" ;

		private const string c_strDeleteRecord_PictureSQL = "delete from inpatientcasehistory_picture where inpatientid=? and inpatientdate=? and opendate=?" ;

        private const string c_strModifyRecordSQL = @"update inpatientcasehistory_history
   set credibility               = ?,
       representor               = ?,
       primarydiagnosedocid      = ?,
       primarydiagnosedate       = ?,
       finallydiagnosedocid      = ?,
       finallydiagnosedate       = ?,
       maindescriptionxml        = ?,
       maindescriptionall        = ?,
       currentstatusxml          = ?,
       currentstatusall          = ?,
       beforetimestatusxml       = ?,
       beforetimestatusall       = ?,
       ownhistoryxml             = ?,
       ownhistoryall             = ?,
       marriagehistoryxml        = ?,
       marriagehistoryall        = ?,
       familyhistoryxml          = ?,
       familyhistoryall          = ?,
       summaryxml                = ?,
       summaryall                = ?,
       primarydiagnosexml        = ?,
       primarydiagnoseall        = ?,
       finallydiagnosexml        = ?,
       finallydiagnoseall        = ?,
       temperaturexml            = ?,
       temperatureall            = ?,
       pulsexml                  = ?,
       pulseall                  = ?,
       breathxml                 = ?,
       breathall                 = ?,
       sysxml                    = ?,
       sysall                    = ?,
       diaxml                    = ?,
       diaall                    = ?,
       bloodpressureunitxml      = ?,
       bloodpressureunitall      = ?,
       medicalxml                = ?,
       medicalall                = ?,
       professionalcheckxml      = ?,
       professionalcheckall      = ?,
       labcheckall               = ?,
       labcheckxml               = ?,
       catameniahistoryall       = ?,
       catameniahistoryxml       = ?,
       yjsall                    = ?,
       yjsxml                    = ?,
       contrahistoryall          = ?,
       contrahistoryxml          = ?,
       shysall                   = ?,
       shysxml                   = ?,
       lcqkall                   = ?,
       lcqkxml                   = ?,
       cqjcall                   = ?,
       cqjcxml                   = ?,
       careplanall               = ?,
       careplanxml               = ?,
       oldmaternitysufferall     = ?,
       oldmaternitysufferxml     = ?,
       modifydiagnoseall         = ?,
       modifydiagnosexml         = ?,
       modifydiagnosedoctorid    = ?,
       modifydiagnosedate        = ?,
       adddiagnoseall            = ?,
       adddiagnosexml            = ?,
       adddiagnosedoctorid       = ?,
       adddiagnosedate           = ?,
       selectedmc                = ?,
       selectedlastcatameniatime = ?,
       selectedameniaage         = ?,
       ameniaage                 = ?,
       normaldiagnoseall         = ?,
       normaldiagnoseallxml      = ?,
       normaldiagnosealldictirid = ?,
       normaldiagnosealldate     = ?,
       weightall                 = ?,
       weightxml                 = ?,
       buchongall                = ?,
       buchongxml                = ?,
       buchongid                 = ?,
       buchongdate               = ?
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0";

		private const string c_strDeleteRecordSQL="update inpatientcasehistory_history set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		private const string c_strAddNewRecord_PictureSQL = @"insert into inpatientcasehistory_picture(inpatientid,inpatientdate,opendate,picid,frontimage,backcolor,width,height) 
		values(?,?,?,?,?,?,?,?)";

		private const string c_strAddNewPatient_Disease = @"insert into patient_associate
      (inpatientid, inpatientdate,associateid)
values (?,?,?)";

		private const string c_strAddNewRecordSQL= @"insert into  inpatientcasehistory_history(inpatientid,inpatientdate,
		opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,firstprintdate,deactiveddate,deactivedoperatorid	,
		status,credibility,representor,primarydiagnosedocid,primarydiagnosedate,finallydiagnosedocid,finallydiagnosedate,maindescriptionxml	,
		maindescriptionall,currentstatusxml,currentstatusall,beforetimestatusxml,beforetimestatusall,ownhistoryxml,ownhistoryall,marriagehistoryxml	,
		marriagehistoryall,familyhistoryxml,familyhistoryall,summaryxml,summaryall,primarydiagnosexml,primarydiagnoseall,finallydiagnosexml	,
		finallydiagnoseall,temperaturexml,temperatureall,pulsexml,pulseall,breathxml,breathall,sysxml,sysall,diaxml,diaall,bloodpressureunitxml	,
		bloodpressureunitall,medicalxml,medicalall,professionalcheckxml	,professionalcheckall,labcheckall,labcheckxml,catameniahistoryall,catameniahistoryxml,
	    yjsall, yjsxml, contrahistoryall,contrahistoryxml,shysall,shysxml,lcqkall,lcqkxml,cqjcall,cqjcxml,careplanall,careplanxml,oldmaternitysufferall,oldmaternitysufferxml,
		modifydiagnoseall,
		modifydiagnosexml,
		modifydiagnosedoctorid,
		modifydiagnosedate,
		adddiagnoseall,
		adddiagnosexml,
		adddiagnosedoctorid,
		adddiagnosedate,selectedmc,selectedlastcatameniatime,selectedameniaage,ameniaage,normaldiagnoseall,normaldiagnoseallxml,normaldiagnosealldictirid,normaldiagnosealldate,weightall,weightxml,buchongall,buchongxml,buchongid,buchongdate) 
		values(?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,?,?)";
		
		private const string c_strAddNewRecordContentSQL= @"insert into  ipcasehistory_historycontent(inpatientid,
		inpatientdate,opendate,lastmodifydate,lastmodifyuserid,deactiveddate,deactivedoperatorid,status,maindescription,currentstatus,
		beforetimestatus,ownhistory,marriagehistory,familyhistory,summary,temperature,pulse,breath,
		sys,dia,bloodpressureunit,medical,professionalcheck,labcheck,catameniahistory,firstcatamenia,
		catamenialasttime,catameniacycle,lastcatameniatime,catameniacase,yjs,contrahistory,shys,lcqk,cqjc,pregtimes,borntimes,careplan,chargedoctor,diretdoctor,oldmaternitysuffer,midwife,modifydiagnose,adddiagnose,diagnosenor,bloodpressure,weight,buchong)
		values(?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		private const string c_strAddNewRecordContent_PrimaryDiagnoseSQL=@"insert into  ipcasehistcont_primarydiagnose(inpatientid,
						inpatientdate,opendate,lastmodifydate,indexid,primarydiagnose)
						values(?,?,?,?,?,?)";

		private const string c_strAddNewRecordContent_FinallyDiagnoseSQL=@"insert into  ipcasehistcont_finallydiagnose(inpatientid,
						inpatientdate,opendate,lastmodifydate,indexid,finallydiagnose)
						values(?,?,?,?,?,?)";

		// 获取病人的该记录时间列表。
		[AutoComplete] 
		public override long m_lngGetRecordTimeList(
			string p_strInPatientID,
			out string[] p_strInPatientDateArr,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strInPatientDateArr = null;
			p_strCreateRecordTimeArr=null;
			p_strOpenRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInPatientCaseHistoryServ", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strCreateRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strOpenRecordTimeArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strInPatientDateArr[i] = dtbValue.Rows[i]["INPATIENTDATE"].ToString();
                        p_strCreateRecordTimeArr[i] = dtbValue.Rows[i]["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = dtbValue.Rows[i]["OPENDATE"].ToString();
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
			//返回
			return lngRes;

		
		}

		
		// 更新数据库中的首次打印时间。
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInPatientCaseHistoryServ", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
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

            }			//返回
			return lngRes;

			
		}

		// 获取病人的已经被删除记录时间列表。
		[AutoComplete] 
		public override long m_lngGetDeleteRecordTimeList(
			string p_strInPatientID,
			string p_strInPatientDate,
			string p_strDeleteUserID,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr=null;
			p_strOpenRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInPatientCaseHistoryServ", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                lngRes = (long)enmOperationResult.DB_Succeed;

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

		// 获取病人的已经被删除记录时间列表。
		[AutoComplete] 
		public override long m_lngGetDeleteRecordTimeListAll(
			string p_strInPatientID,
			string p_strInPatientDate,
			out string[] p_strCreateRecordTimeArr,
			out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr=null;
			p_strOpenRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInPatientCaseHistoryServ", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                lngRes = (long)enmOperationResult.DB_Succeed;

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

		// 获取指定记录的内容。
		[AutoComplete] 
		protected override long m_lngGetRecordContentWithServ(
			string p_strInPatientID,
			string p_strInPatientDate,
			/* string p_strOpenRecordTime, */
			clsHRPTableService p_objHRPServ,
			out clsBaseCaseHistoryInfo p_objRecordContent,
			out clsPictureBoxValue[] p_objPicValueArr)
		{
			p_objRecordContent=null;
			p_objPicValueArr = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);

                //			clsInPatientCaseHistoryContent p_objContent =new clsInPatientCaseHistoryContent() ;	
                //生成DataTable
                DataTable dtbValue = new DataTable();

                string strSql = @"select a.inpatientid,
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
       a.credibility,
       a.representor,
       a.primarydiagnosedocid,
       a.primarydiagnosedate,
       a.finallydiagnosedocid,
       a.finallydiagnosedate,
       a.maindescriptionxml,
       a.maindescriptionall,
       a.currentstatusxml,
       a.currentstatusall,
       a.beforetimestatusxml,
       a.beforetimestatusall,
       a.ownhistoryxml,
       a.ownhistoryall,
       a.marriagehistoryxml,
       a.marriagehistoryall,
       a.catameniahistoryxml,
       a.catameniahistoryall,
       a.familyhistoryxml,
       a.familyhistoryall,
       a.summaryxml,
       a.summaryall,
       a.primarydiagnosexml,
       a.primarydiagnoseall,
       a.finallydiagnosexml,
       a.finallydiagnoseall,
       a.temperaturexml,
       a.temperatureall,
       a.pulsexml,
       a.pulseall,
       a.breathxml,
       a.breathall,
       a.sysxml,
       a.sysall,
       a.diaxml,
       a.diaall,
       a.bloodpressureunitxml,
       a.bloodpressureunitall,
       a.medicalxml,
       a.medicalall,
       a.professionalcheckxml,
       a.professionalcheckall,
       a.labcheckall,
       a.labcheckxml,
       a.yjsall,
       a.yjsxml,
       a.contrahistoryall,
       a.contrahistoryxml,
       a.shysall,
       a.shysxml,
       a.lcqkall,
       a.lcqkxml,
       a.cqjcall,
       a.cqjcxml,
       a.careplanall,
       a.careplanxml,
       a.oldmaternitysufferall,
       a.oldmaternitysufferxml,
       a.modifydiagnoseall,
       a.modifydiagnosexml,
       a.modifydiagnosedoctorid,
       a.modifydiagnosedate,
       a.adddiagnoseall,
       a.adddiagnosexml,
       a.adddiagnosedoctorid,
       a.adddiagnosedate,
       a.selectedmc,
       a.selectedlastcatameniatime,
       a.selectedameniaage,
       a.ameniaage,
       a.normaldiagnoseall,
       a.normaldiagnoseallxml,
       a.normaldiagnosealldictirid,
       a.normaldiagnosealldate,
       a.weightall,
       a.weightxml,
       a.buchongall,
       a.buchongxml,
       a.buchongid,
       a.buchongdate,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.adddiagnose,
       b.buchong,
       b.diagnosenor ,b.bloodpressure,b.weight,pbi.lastname_vchr  
	from inpatientcasehistory_history a,ipcasehistory_historycontent b,t_bse_employee pbi 
	where a.inpatientid = ?
	and a.inpatientdate= ?
	and a.status=0
	and a.createuserid=pbi.empno_chr 
	and b.inpatientid=a.inpatientid														
	and b.inpatientdate=a.inpatientdate
	and b.opendate=a.opendate 
	and b.lastmodifydate=(select max(lastmodifydate)
	from ipcasehistory_historycontent 
	where inpatientid=a.inpatientid 
	and inpatientdate=a.inpatientdate 
	and opendate=a.opendate)";

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                //lngRes = objHRPServ.DoGetDataTable(strSql, ref dtbValue);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsInPatientCaseHistoryContent p_objContent = new clsInPatientCaseHistoryContent();
                    DataRow objRow = dtbValue.Rows[0];
                    //for (int i = 0; i < dtbValue.Rows.Count; i++)
                    //{
                    //设置结果
                    p_objContent.m_bytIfConfirm = byte.Parse(objRow["IFCONFIRM"].ToString());
                    p_objContent.m_bytStatus = byte.Parse(objRow["STATUS"].ToString());
                    p_objContent.m_dtmCreateDate = (objRow["CREATEDATE"].ToString() != null ? DateTime.Parse(objRow["CREATEDATE"].ToString()) : DateTime.MinValue);

                    p_objContent.m_dtmDeActivedDate = (objRow["DEACTIVEDDATE"].ToString() != "" ? DateTime.Parse(objRow["DEACTIVEDDATE"].ToString()) : DateTime.MinValue);
                    p_objContent.m_dtmFirstPrintDate = (objRow["FIRSTPRINTDATE"].ToString() != "" ? DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString()) : DateTime.MinValue);
                    p_objContent.m_dtmInPatientDate = (objRow["INPATIENTDATE"].ToString() != "" ? DateTime.Parse(objRow["INPATIENTDATE"].ToString()) : DateTime.MinValue);

                    p_objContent.m_dtmModifyDate = (objRow["LASTMODIFYDATE"].ToString() != "" ? DateTime.Parse(objRow["LASTMODIFYDATE"].ToString()) : DateTime.MinValue);
                    p_objContent.m_dtmOpenDate = (objRow["OPENDATE"].ToString() != "" ? DateTime.Parse(objRow["OPENDATE"].ToString()) : DateTime.MinValue);
                    p_objContent.m_strBeforetimeStatus = objRow["BEFORETIMESTATUS"].ToString();

                    p_objContent.m_strBeforetimeStatusAll = objRow["BEFORETIMESTATUSALL"].ToString();
                    p_objContent.m_strBeforetimeStatusXML = objRow["BEFORETIMESTATUSXML"].ToString();
                    p_objContent.m_strBloodPressureUnit = objRow["BLOODPRESSUREUNIT"].ToString();

                    p_objContent.m_strBloodPressureUnitAll = objRow["BLOODPRESSUREUNITALL"].ToString();
                    p_objContent.m_strBloodPressureUnitXML = objRow["BLOODPRESSUREUNITXML"].ToString();
                    p_objContent.m_strBreath = objRow["BREATH"].ToString();

                    p_objContent.m_strBreathAll = objRow["BREATHALL"].ToString();
                    p_objContent.m_strBreathXML = objRow["BREATHXML"].ToString();
                    p_objContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();

                    p_objContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();
                    p_objContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                    p_objContent.m_strCreateName = objRow["LASTNAME_VCHR"].ToString();
                    p_objContent.m_strCredibility = objRow["CREDIBILITY"].ToString();

                    p_objContent.m_strCurrentStatus = objRow["CURRENTSTATUS"].ToString();
                    p_objContent.m_strCurrentStatusXAll = objRow["CURRENTSTATUSALL"].ToString();
                    p_objContent.m_strCurrentStatusXML = objRow["CURRENTSTATUSXML"].ToString();

                    p_objContent.m_strCatameniaHistory = objRow["CATAMENIAHISTORY"].ToString();
                    p_objContent.m_strCatameniaHistoryAll = objRow["CATAMENIAHISTORYALL"].ToString();
                    p_objContent.m_strCatameniaHistoryXML = objRow["CATAMENIAHISTORYXML"].ToString();

                    p_objContent.m_strDeActivedOperatorID = objRow["DEACTIVEDOPERATORID"].ToString();

                    p_objContent.m_strDia = objRow["DIA"].ToString();
                    p_objContent.m_strDiaAll = objRow["DIAALL"].ToString();
                    p_objContent.m_strDiaXML = objRow["DIAXML"].ToString();

                    p_objContent.m_strFamilyHistory = objRow["FAMILYHISTORY"].ToString();
                    p_objContent.m_strFamilyHistoryAll = objRow["FAMILYHISTORYALL"].ToString();
                    p_objContent.m_strFamilyHistoryXML = objRow["FAMILYHISTORYXML"].ToString();

                    //p_objContent.m_strFinallyDiagnose=objRow["FINALLYDIAGNOSE"].ToString() ;
                    p_objContent.m_strFinallyDiagnoseAll = objRow["FINALLYDIAGNOSEALL"].ToString();
                    p_objContent.m_strFinallyDiagnoseXML = objRow["FINALLYDIAGNOSEXML"].ToString();

                    //血压(新加)
                    p_objContent.m_strBloodPressure = objRow["BLOODPRESSURE"].ToString();
                    //体重(新加)
                    p_objContent.m_strWeight = objRow["WEIGHT"].ToString();
                    p_objContent.m_strWeightAll = objRow["WEIGHTALL"].ToString();
                    p_objContent.m_strWeightXML = objRow["WEIGHTXML"].ToString();

                    p_objContent.m_strFinallyDiagnoseDate = objRow["FINALLYDIAGNOSEDATE"].ToString();
                    p_objContent.m_strFinallyDiagnoseDocID = objRow["FINALLYDIAGNOSEDOCID"].ToString();
                    p_objContent.m_strInPatientID = objRow["INPATIENTID"].ToString();

                    p_objContent.m_strLabCheckAll = objRow["LABCHECKALL"].ToString();
                    p_objContent.m_strLabCheckXML = objRow["LABCHECKXML"].ToString();
                    p_objContent.m_strLabCheck = objRow["LABCHECK"].ToString();

                    p_objContent.m_strMainDescription = objRow["MAINDESCRIPTION"].ToString();
                    p_objContent.m_strMainDescriptionAll = objRow["MAINDESCRIPTIONALL"].ToString();
                    p_objContent.m_strMainDescriptionXML = objRow["MAINDESCRIPTIONXML"].ToString();

                    p_objContent.m_strMarriageHistory = objRow["MARRIAGEHISTORY"].ToString();
                    p_objContent.m_strMarriageHistoryAll = objRow["MARRIAGEHISTORYALL"].ToString();
                    p_objContent.m_strMarriageHistoryXML = objRow["MARRIAGEHISTORYXML"].ToString();

                    p_objContent.m_strMedical = objRow["MEDICAL"].ToString();
                    p_objContent.m_strMedicalAll = objRow["MEDICALALL"].ToString();
                    p_objContent.m_strMedicalXML = objRow["MEDICALXML"].ToString();

                    p_objContent.m_strModifyUserID = objRow["LASTMODIFYUSERID"].ToString();
                    p_objContent.m_strOwnHistory = objRow["OWNHISTORY"].ToString();
                    p_objContent.m_strOwnHistoryAll = objRow["OWNHISTORYALL"].ToString();

                    p_objContent.m_strOwnHistoryXML = objRow["OWNHISTORYXML"].ToString();
                    //p_objContent.m_strPrimaryDiagnose=objRow["PRIMARYDIAGNOSE"].ToString() ;
                    p_objContent.m_strPrimaryDiagnoseAll = objRow["PRIMARYDIAGNOSEALL"].ToString();

                    p_objContent.m_strPrimaryDiagnoseDate = objRow["PRIMARYDIAGNOSEDATE"].ToString();
                    p_objContent.m_strPrimaryDiagnoseDocID = objRow["PRIMARYDIAGNOSEDOCID"].ToString();
                    p_objContent.m_strPrimaryDiagnoseXML = objRow["PRIMARYDIAGNOSEXML"].ToString();

                    p_objContent.m_strProfessionalCheck = objRow["PROFESSIONALCHECK"].ToString();
                    p_objContent.m_strProfessionalCheckAll = objRow["PROFESSIONALCHECKALL"].ToString();
                    p_objContent.m_strProfessionalCheckXML = objRow["PROFESSIONALCHECKXML"].ToString();

                    p_objContent.m_strPulse = objRow["PULSE"].ToString();
                    p_objContent.m_strPulseAll = objRow["PULSEALL"].ToString();
                    p_objContent.m_strPulseXML = objRow["PULSEXML"].ToString();

                    p_objContent.m_strRepresentor = objRow["REPRESENTOR"].ToString();
                    p_objContent.m_strSummary = objRow["SUMMARY"].ToString();
                    p_objContent.m_strSummaryAll = objRow["SUMMARYALL"].ToString();

                    p_objContent.m_strSummaryXML = objRow["SUMMARYXML"].ToString();
                    p_objContent.m_strSys = objRow["SYS"].ToString();
                    p_objContent.m_strSysAll = objRow["SYSALL"].ToString();

                    p_objContent.m_strSysXML = objRow["SYSXML"].ToString();
                    p_objContent.m_strTemperature = objRow["TEMPERATURE"].ToString();
                    p_objContent.m_strTemperatureAll = objRow["TEMPERATUREALL"].ToString();

                    p_objContent.m_strTemperatureXML = objRow["TEMPERATUREXML"].ToString();
                    p_objContent.m_strFirstCatamenia = objRow["FIRSTCATAMENIA"].ToString();
                    p_objContent.m_strCatameniaLastTime = objRow["CATAMENIALASTTIME"].ToString();
                    p_objContent.m_strCatameniaCycle = objRow["CATAMENIACYCLE"].ToString();
                    try
                    {
                        p_objContent.m_dtmLastCatameniaTime = DateTime.Parse(objRow["LASTCATAMENIATIME"].ToString());
                    }
                    catch
                    {
                        p_objContent.m_dtmLastCatameniaTime = DateTime.MinValue;
                    }
                    p_objContent.m_strCatameniaCase = objRow["CATAMENIACASE"].ToString();

                    p_objContent.m_strYJS = objRow["YJS"].ToString();
                    p_objContent.m_strYJSAll = objRow["YJSALL"].ToString();
                    p_objContent.m_strYJSXML = objRow["YJSXML"].ToString();

                    p_objContent.m_strContraHistory = objRow["CONTRAHISTORY"].ToString();
                    p_objContent.m_strContraHistoryAll = objRow["CONTRAHISTORYALL"].ToString();
                    p_objContent.m_strContraHistoryXML = objRow["CONTRAHISTORYXML"].ToString();

                    p_objContent.m_strShYS = objRow["SHYS"].ToString();
                    p_objContent.m_strShYSAll = objRow["SHYSALL"].ToString();
                    p_objContent.m_strShYSXML = objRow["SHYSXML"].ToString();

                    p_objContent.m_strLCQK = objRow["LCQK"].ToString();
                    p_objContent.m_strLCQKAll = objRow["LCQKALL"].ToString();
                    p_objContent.m_strLCQKXML = objRow["LCQKXML"].ToString();

                    p_objContent.m_strCQJC = objRow["CQJC"].ToString();
                    p_objContent.m_strCQJCAll = objRow["CQJCALL"].ToString();
                    p_objContent.m_strCQJCXML = objRow["CQJCXML"].ToString();

                    p_objContent.m_strCarePlan = objRow["CAREPLAN"].ToString();
                    p_objContent.m_strCarePlanAll = objRow["CAREPLANALL"].ToString();
                    p_objContent.m_strCarePlanXML = objRow["CAREPLANXML"].ToString();

                    p_objContent.m_strPregTimes = objRow["PREGTIMES"].ToString();
                    p_objContent.m_strBornTimes = objRow["BORNTIMES"].ToString();
                    p_objContent.m_strChargeDoctor = objRow["CHARGEDOCTOR"].ToString();
                    p_objContent.m_strDiretDoctor = objRow["DIRETDOCTOR"].ToString();
                    p_objContent.m_strMidWife = objRow["MIDWIFE"].ToString();

                    p_objContent.m_strOldMaternitySuffer = objRow["OLDMATERNITYSUFFER"].ToString();
                    p_objContent.m_strOldMaternitySufferAll = objRow["OLDMATERNITYSUFFERALL"].ToString();
                    p_objContent.m_strOldMaternitySufferXML = objRow["OLDMATERNITYSUFFERXML"].ToString();

                    if (objRow["SELECTEDMC"] == DBNull.Value)
                    {
                        p_objContent.m_intSelectedMC = 0;
                    }
                    else
                    {
                        p_objContent.m_intSelectedMC = Convert.ToInt32(objRow["SELECTEDMC"]);
                    }

                    if (objRow["SELECTEDLASTCATAMENIATIME"] == DBNull.Value)
                    {
                        p_objContent.m_intSELECTEDLASTCATAMENIATIME = 0;
                    }
                    else
                    {
                        p_objContent.m_intSELECTEDLASTCATAMENIATIME = Convert.ToInt32(objRow["SELECTEDLASTCATAMENIATIME"]);
                    }

                    if (objRow["SELECTEDAMENIAAGE"] == DBNull.Value)
                    {
                        p_objContent.m_intSELECTEDAMENIAAGE = 0;
                    }
                    else
                    {
                        p_objContent.m_intSELECTEDAMENIAAGE = Convert.ToInt32(objRow["SELECTEDAMENIAAGE"]);
                    }
                    p_objContent.m_strAMENIAAGE = objRow["AMENIAAGE"].ToString();

                    //修正诊断
                    p_objContent.m_strModifyDiagnose = objRow["MODIFYDIAGNOSE"].ToString();
                    p_objContent.m_strModifyDiagnoseAll = objRow["MODIFYDIAGNOSEALL"].ToString();
                    p_objContent.m_strModifyDiagnoseXML = objRow["MODIFYDIAGNOSEXML"].ToString();
                    p_objContent.m_strModifyDiagnoseDoctorID = objRow["MODIFYDIAGNOSEDOCTORID"].ToString();
                    try
                    {
                        p_objContent.m_datModifyDiagnose = DateTime.Parse(objRow["MODIFYDIAGNOSEDATE"].ToString());
                    }
                    catch
                    {
                        p_objContent.m_datModifyDiagnose = DateTime.Now;

                    }

                    //补充诊断
                    p_objContent.m_strAddDiagnose = objRow["ADDDIAGNOSE"].ToString();
                    p_objContent.m_strAddDiagnoseALL = objRow["ADDDIAGNOSEALL"].ToString();
                    p_objContent.m_strAddDiagnoseXML = objRow["ADDDIAGNOSEXML"].ToString();
                    p_objContent.m_strAddDiagnoseDoctorID = objRow["ADDDIAGNOSEDOCTORID"].ToString();
                    try
                    {
                        p_objContent.m_datAddDiagnose = DateTime.Parse(objRow["ADDDIAGNOSEDATE"].ToString());

                    }
                    catch
                    {
                        p_objContent.m_datAddDiagnose = DateTime.Now;

                    }
                    //普通诊断
                    p_objContent.m_strDiagnoseAll = objRow["DIAGNOSENOR"].ToString();
                    p_objContent.m_strDiagnoseOK = objRow["NORMALDIAGNOSEALL"].ToString();
                    p_objContent.m_strDiagnosetxtXML = objRow["NORMALDIAGNOSEALLXML"].ToString();
                    p_objContent.m_strDiagnoseDoc = objRow["NORMALDIAGNOSEALLDICTIRID"].ToString();
                    try
                    {
                        p_objContent.m_dtDiagnoseDate = DateTime.Parse(objRow["NORMALDIAGNOSEALLDATE"].ToString());
                    }
                    catch
                    {
                        p_objContent.m_dtDiagnoseDate = DateTime.Now;
                    }

                    //补充诊断
                    p_objContent.m_strBuChong = objRow["BUCHONG"].ToString();
                    p_objContent.m_strBuChongALL = objRow["BUCHONGALL"].ToString();
                    p_objContent.m_strBuChongXML = objRow["BUCHONGXML"].ToString();
                    p_objContent.m_strBuChongDoctorID = objRow["BUCHONGID"].ToString();
                    try
                    {
                        p_objContent.m_dateBuChong = DateTime.Parse(objRow["BUCHONGDATE"].ToString());

                    }
                    catch
                    {
                        p_objContent.m_dateBuChong = DateTime.Now;

                    }

                    //}
                    p_objRecordContent = p_objContent;
                }

                #region 读取画图信息
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbValue1 = new DataTable();

                //执行查询，填充结果到DataTable
                long lngRes1 = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContent_PictureSQL, ref dtbValue1, objDPArr1);

                ArrayList arlPic = new ArrayList();

                //从DataTable.Rows中获取结果
                if (lngRes1 > 0 && dtbValue1.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbValue1.Rows.Count; i++)
                    {
                        clsPictureBoxValue objPicValue = new clsPictureBoxValue();
                        //					objPicValue.m_imgBack = m_imgBinaryToImage(dtbValue1.Rows[i]["BACKIMAGE"]) ;
                        //					objPicValue.m_imgFront = m_imgBinaryToImage(dtbValue1.Rows[i]["FRONTIMAGE"]);
                        objPicValue.m_bytImage = (byte[])(dtbValue1.Rows[i]["FRONTIMAGE"]);
                        //						objPicValue.clrBack = dtbValue.Rows[i]["BACKCOLOR"];
                        objPicValue.intWidth = Convert.ToInt32(dtbValue1.Rows[i]["WIDTH"]);
                        objPicValue.intHeight = Convert.ToInt32(dtbValue1.Rows[i]["HEIGHT"]);
                        objPicValue.clrBack = Color.FromArgb(Convert.ToInt32(dtbValue1.Rows[i]["BACKCOLOR"]));
                        arlPic.Add(objPicValue);
                    }
                }
                p_objPicValueArr = (clsPictureBoxValue[])arlPic.ToArray(typeof(clsPictureBoxValue));
                arlPic.Clear();

                #endregion


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

		/// <summary>
		/// 出院病人随访复诊提醒获取出院记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strDiagnose"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetRecordFromRevisit(string p_strInPatientID,
			string p_strInPatientDate,out string p_strDiagnose)
		{
			p_strDiagnose = "";
			clsHRPTableService p_objHRPServ = new clsHRPTableService();
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //按顺序给IDataParameter赋值
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetContentSQL_FromRevisit, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_strDiagnose = dtbValue.Rows[0]["PrimaryDiagnoseAll"].ToString();
                } 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			
			return lngRes;
		}

		// 查看是否有相同的记录时间
[AutoComplete] 		
		protected override long m_lngCheckCreateDate(clsBaseCaseHistoryInfo  p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objPreModifyInfo)
		{
			p_objPreModifyInfo=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objPreModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objPreModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString());
                        p_objPreModifyInfo.m_strActionUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
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

            }			//返回
			return lngRes;


		}

		
		// 查看当前记录是否最新的记录。
[AutoComplete] 		
		protected override long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;
			long lngRes = 0;
			string c_strCheckLastModifyRecordSQL=null;
	
			if (clsHRPTableService.bytDatabase_Selector==0)
			{
                c_strCheckLastModifyRecordSQL = "select top 1 lastmodifydate,lastmodifyuserid from ipcasehistory_historycontent where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by lastmodifydate desc";
			}
            else if (clsHRPTableService.bytDatabase_Selector == 2)
			{
				 c_strCheckLastModifyRecordSQL= @"select lastmodifydate, lastmodifyuserid
  from (select lastmodifydate, lastmodifyuserid
          from ipcasehistory_historycontent
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by lastmodifydate desc)
 where rownum = 1";
			}
            else if (clsHRPTableService.bytDatabase_Selector == 4)
            {
                c_strCheckLastModifyRecordSQL = " select lastmodifydate,lastmodifyuserid from ipcasehistory_historycontent where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by lastmodifydate desc fetch first 1 row only";
            }


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果,
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["LASTMODIFYDATE"].ToString());
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[i]["LASTMODIFYUSERID"].ToString();
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

            }			//返回
			return lngRes;
	
	
		}


		// 保存记录到数据库。
[AutoComplete] 		
		protected override long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
			clsPictureBoxValue[] p_objPicValueArr,string p_strDiseaseID,string p_strDeptID,
			clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsInPatientCaseHistoryContent m_objContent = (clsInPatientCaseHistoryContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(92, out objDPArr);


                objDPArr[0].Value = m_objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = m_objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = m_objContent.m_dtmCreateDate;
                objDPArr[4].Value = m_objContent.m_strCreateUserID;
                objDPArr[5].Value = m_objContent.m_bytIfConfirm;
                objDPArr[6].Value = m_objContent.m_strConfirmReason;
                objDPArr[7].Value = m_objContent.m_strConfirmReasonXML;
                objDPArr[8].DbType = DbType.DateTime;
                if (m_objContent.m_dtmFirstPrintDate == DateTime.MinValue)
                    objDPArr[8].Value = DBNull.Value;
                else
                    objDPArr[8].Value = m_objContent.m_dtmFirstPrintDate;
                objDPArr[9].DbType = DbType.DateTime;
                if (m_objContent.m_dtmDeActivedDate == DateTime.MinValue)
                    objDPArr[9].Value = DBNull.Value;
                else
                    objDPArr[9].Value = m_objContent.m_dtmDeActivedDate;



                objDPArr[10].Value = m_objContent.m_strDeActivedOperatorID;
                objDPArr[11].Value = m_objContent.m_bytStatus;
                objDPArr[12].Value = m_objContent.m_strCredibility;
                objDPArr[13].Value = m_objContent.m_strRepresentor;
                objDPArr[14].Value = m_objContent.m_strPrimaryDiagnoseDocID;
                objDPArr[15].DbType = DbType.DateTime;
                if (string.IsNullOrEmpty( m_objContent.m_strPrimaryDiagnoseDate))
                    objDPArr[15].Value = DBNull.Value;
                else
                {
                    try
                    {
                        objDPArr[15].Value = DateTime.Parse(m_objContent.m_strPrimaryDiagnoseDate);
                    }
                    catch { objDPArr[15].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); }
                }

                objDPArr[16].Value = m_objContent.m_strFinallyDiagnoseDocID;
                objDPArr[17].DbType = DbType.DateTime;
                if (m_objContent.m_strFinallyDiagnoseDate == "")
                    objDPArr[17].Value = DBNull.Value;
                else
                {
                    try
                    {
                        objDPArr[17].Value = DateTime.Parse(m_objContent.m_strFinallyDiagnoseDate);
                    }
                    catch
                    {
                        objDPArr[17].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }

                objDPArr[18].Value = m_objContent.m_strMainDescriptionXML;
                objDPArr[19].Value = m_objContent.m_strMainDescriptionAll;
                objDPArr[20].Value = m_objContent.m_strCurrentStatusXML;
                objDPArr[21].Value =  m_objContent.m_strCurrentStatusXAll;
                objDPArr[22].Value = m_objContent.m_strBeforetimeStatusXML;
                objDPArr[23].Value = m_objContent.m_strBeforetimeStatusAll;
                objDPArr[24].Value = m_objContent.m_strOwnHistoryXML;
                objDPArr[25].Value = m_objContent.m_strOwnHistoryAll;
                objDPArr[26].Value = m_objContent.m_strMarriageHistoryXML;
                objDPArr[27].Value = m_objContent.m_strMarriageHistoryAll;
                objDPArr[28].Value = m_objContent.m_strFamilyHistoryXML;
                objDPArr[29].Value = m_objContent.m_strFamilyHistoryAll;
                objDPArr[30].Value =  m_objContent.m_strSummaryXML;
                objDPArr[31].Value =  m_objContent.m_strSummaryAll;
                objDPArr[32].Value = m_objContent.m_strPrimaryDiagnoseXML;
                objDPArr[33].Value = m_objContent.m_strPrimaryDiagnoseAll;
                objDPArr[34].Value = m_objContent.m_strFinallyDiagnoseXML;
                objDPArr[35].Value = m_objContent.m_strFinallyDiagnoseAll;
                objDPArr[36].Value = m_objContent.m_strTemperatureXML;
                objDPArr[37].Value = m_objContent.m_strTemperatureAll;
                objDPArr[38].Value = m_objContent.m_strPulseXML;
                objDPArr[39].Value = m_objContent.m_strPulseAll;
                objDPArr[40].Value =  m_objContent.m_strBreathXML;
                objDPArr[41].Value =  m_objContent.m_strBreathAll;
                objDPArr[42].Value = m_objContent.m_strSysXML;
                objDPArr[43].Value = m_objContent.m_strSysAll;
                objDPArr[44].Value = m_objContent.m_strDiaXML;
                objDPArr[45].Value = m_objContent.m_strDiaAll;
                objDPArr[46].Value = m_objContent.m_strBloodPressureUnitXML;
                objDPArr[47].Value = m_objContent.m_strBloodPressureUnitAll;
                objDPArr[48].Value = m_objContent.m_strMedicalXML;
                objDPArr[49].Value = m_objContent.m_strMedicalAll;
                objDPArr[50].Value = m_objContent.m_strProfessionalCheckXML;
                objDPArr[51].Value = m_objContent.m_strProfessionalCheckAll;
                objDPArr[52].Value = m_objContent.m_strLabCheckAll;
                objDPArr[53].Value = m_objContent.m_strLabCheckXML;
                objDPArr[54].Value = m_objContent.m_strCatameniaHistoryAll;
                objDPArr[55].Value = m_objContent.m_strCatameniaHistoryXML;
                objDPArr[56].Value = m_objContent.m_strYJSAll;
                objDPArr[57].Value = m_objContent.m_strYJSXML;
                objDPArr[58].Value = m_objContent.m_strContraHistoryAll;
                objDPArr[59].Value = m_objContent.m_strContraHistoryAll;
                objDPArr[60].Value =  m_objContent.m_strShYSAll;
                objDPArr[61].Value =  m_objContent.m_strShYSXML;
                objDPArr[62].Value =  m_objContent.m_strLCQKAll;
                objDPArr[63].Value =  m_objContent.m_strLCQKXML;
                objDPArr[64].Value = m_objContent.m_strCQJCAll;
                objDPArr[65].Value = m_objContent.m_strCQJCXML;
                objDPArr[66].Value = m_objContent.m_strCarePlanAll;
                objDPArr[67].Value = m_objContent.m_strCarePlanXML;
                objDPArr[68].Value = m_objContent.m_strOldMaternitySufferAll;
                objDPArr[69].Value = m_objContent.m_strOldMaternitySufferXML;
                //修正诊断
                objDPArr[70].Value = m_objContent.m_strModifyDiagnoseAll;
                objDPArr[71].Value = m_objContent.m_strModifyDiagnoseXML;
                objDPArr[72].Value = m_objContent.m_strModifyDiagnoseDoctorID;
                objDPArr[73].DbType = DbType.DateTime;
                objDPArr[73].Value = m_objContent.m_datModifyDiagnose;
                //补充诊断
                objDPArr[74].Value = m_objContent.m_strAddDiagnoseALL;
                objDPArr[75].Value = m_objContent.m_strAddDiagnoseXML;
                objDPArr[76].Value = m_objContent.m_strAddDiagnoseDoctorID;
                objDPArr[77].DbType = DbType.DateTime;
                objDPArr[77].Value = m_objContent.m_datAddDiagnose;
                objDPArr[78].Value = m_objContent.m_intSelectedMC;
                objDPArr[79].Value = m_objContent.m_intSELECTEDLASTCATAMENIATIME;
                objDPArr[80].Value = m_objContent.m_intSELECTEDAMENIAAGE;
                objDPArr[81].Value = m_objContent.m_strAMENIAAGE;
                //普通诊断
                objDPArr[82].Value = m_objContent.m_strDiagnoseOK;
                objDPArr[83].Value = m_objContent.m_strDiagnosetxtXML;
                objDPArr[84].Value = m_objContent.m_strDiagnoseDoc;
                objDPArr[85].DbType = DbType.DateTime;
                objDPArr[85].Value = m_objContent.m_dtDiagnoseDate;
                //体重
                objDPArr[86].Value = m_objContent.m_strWeightAll;
                objDPArr[87].Value = m_objContent.m_strWeightXML;

                //补充诊断
                objDPArr[88].Value = m_objContent.m_strBuChongALL;
                objDPArr[89].Value = m_objContent.m_strBuChongXML;
                objDPArr[90].Value = m_objContent.m_strBuChongDoctorID;
                objDPArr[91].DbType = DbType.DateTime;
                objDPArr[91].Value = m_objContent.m_dateBuChong;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                #region 保存画图

                if (p_objPicValueArr != null && p_objPicValueArr.Length > 0)
                {


                    for (int j = 0; j < p_objPicValueArr.Length; j++)
                    {
                        IDataParameter[] objDPArr0 = null;
                        objHRPServ.CreateDatabaseParameter(8, out objDPArr0);


                        objDPArr0[0].Value = m_objContent.m_strInPatientID;
                        objDPArr0[1].DbType = DbType.DateTime;
                        objDPArr0[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr0[2].DbType = DbType.DateTime;
                        objDPArr0[2].Value = m_objContent.m_dtmOpenDate;

                        objDPArr0[3].Value = j + 1;
                        //					objDPArr0[4].DbType = System.Data.DbType.Binary;
                        //					objDPArr0[5].DbType = System.Data.DbType.Binary;

                        //					if(p_objPicValueArr[j].m_imgBack!=null)
                        //						objDPArr0[4].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgBack);	
                        //					else
                        //					objDPArr0[4].Value= new System.Array();
                        objDPArr0[4].DbType = DbType.Binary;
                        if (p_objPicValueArr[j].m_bytImage != null)
                            objDPArr0[4].Value = p_objPicValueArr[j].m_bytImage;//m_bytImageToBinary(p_objPicValueArr[j].m_imgFront);	
                        else
                            objDPArr0[4].Value = System.DBNull.Value;

                        //					objDPArr0[5].Value=Convert.ToString(p_objPicValueArr[j].clrBack);
                        objDPArr0[5].Value = p_objPicValueArr[j].clrBack.ToArgb();

                        //					int intTemp = 32;
                        //					Color.FromArgb((intTemp)

                        objDPArr0[6].Value = p_objPicValueArr[j].intWidth;
                        objDPArr0[7].Value = p_objPicValueArr[j].intHeight;


                        //执行SQL
                        lngEff = 0;
                        long lngRes0 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecord_PictureSQL, ref lngEff, objDPArr0);
                        if (lngRes0 <= 0) return lngRes0;
                    }
                }
                #endregion

                #region 保存病名
                if (p_strDiseaseID != "")//套装模板与病名挂勾
                {
                    lngRes = m_lngSavePatient_Disease(m_objContent.m_strInPatientID, m_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strDiseaseID, p_strDeptID, p_objHRPServ);
                    if (lngRes <= 0) return lngRes;
                }
                #endregion

                //******************************************************************
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(48, out objDPArr1);


                objDPArr1[0].Value = m_objContent.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                objDPArr1[3].DbType = DbType.DateTime;
                objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                objDPArr1[4].Value = (m_objContent.m_strModifyUserID == null ? "" : m_objContent.m_strModifyUserID);

                objDPArr1[5].DbType = DbType.DateTime;
                if (m_objContent.m_dtmDeActivedDate == DateTime.MinValue)
                    objDPArr1[5].Value = DBNull.Value;
                else
                    objDPArr1[5].Value = m_objContent.m_dtmDeActivedDate;

                objDPArr1[6].Value = m_objContent.m_strDeActivedOperatorID;
                objDPArr1[7].Value = m_objContent.m_bytStatus;

                objDPArr1[8].Value = m_objContent.m_strMainDescription;
                objDPArr1[9].Value = m_objContent.m_strCurrentStatus;
                objDPArr1[10].Value = (m_objContent.m_strBeforetimeStatus == null ? "" : m_objContent.m_strBeforetimeStatus);
                objDPArr1[11].Value = (m_objContent.m_strOwnHistory == null ? "" : m_objContent.m_strOwnHistory);
                objDPArr1[12].Value = (m_objContent.m_strMarriageHistory == null ? "" : m_objContent.m_strMarriageHistory);
                objDPArr1[13].Value = (m_objContent.m_strFamilyHistory == null ? "" : m_objContent.m_strFamilyHistory);
                objDPArr1[14].Value = (m_objContent.m_strSummary == null ? "" : m_objContent.m_strSummary);
                //				objDPArr1[15].Value=(m_objContent.m_strPrimaryDiagnose==null ? "":m_objContent.m_strPrimaryDiagnose);
                //				objDPArr1[16].Value=(m_objContent.m_strFinallyDiagnose==null ? "":m_objContent.m_strFinallyDiagnose);
                objDPArr1[15].Value = (m_objContent.m_strTemperature == null ? "" : m_objContent.m_strTemperature);
                objDPArr1[16].Value = (m_objContent.m_strPulse == null ? "" : m_objContent.m_strPulse);
                objDPArr1[17].Value = (m_objContent.m_strBreath == null ? "" : m_objContent.m_strBreath);
                objDPArr1[18].Value = (m_objContent.m_strSys == null ? "" : m_objContent.m_strSys);
                objDPArr1[19].Value = (m_objContent.m_strDia == null ? "" : m_objContent.m_strDia);
                objDPArr1[20].Value = (m_objContent.m_strBloodPressureUnit == null ? "" : m_objContent.m_strBloodPressureUnit);
                objDPArr1[21].Value = (m_objContent.m_strMedical == null ? "" : m_objContent.m_strMedical);
                objDPArr1[22].Value = (m_objContent.m_strProfessionalCheck == null ? "" : m_objContent.m_strProfessionalCheck);
                objDPArr1[23].Value = (m_objContent.m_strLabCheck == null ? "" : m_objContent.m_strLabCheck);
                objDPArr1[24].Value = (m_objContent.m_strCatameniaHistory == null ? "" : m_objContent.m_strCatameniaHistory);
                objDPArr1[25].Value = (m_objContent.m_strFirstCatamenia == null ? "" : m_objContent.m_strFirstCatamenia);
                objDPArr1[26].Value = (m_objContent.m_strCatameniaLastTime == null ? "" : m_objContent.m_strCatameniaLastTime);
                objDPArr1[27].Value = (m_objContent.m_strCatameniaCycle == null ? "" : m_objContent.m_strCatameniaCycle);
                objDPArr1[28].DbType = DbType.DateTime;
                objDPArr1[28].Value = m_objContent.m_dtmLastCatameniaTime;
                objDPArr1[29].Value = (m_objContent.m_strCatameniaCase == null ? "" : m_objContent.m_strCatameniaCase);
                objDPArr1[30].Value = (m_objContent.m_strYJS == null ? "" : m_objContent.m_strYJS);
                objDPArr1[31].Value = (m_objContent.m_strContraHistory == null ? "" : m_objContent.m_strContraHistory);
                objDPArr1[32].Value = (m_objContent.m_strShYS == null ? "" : m_objContent.m_strShYS);
                objDPArr1[33].Value = (m_objContent.m_strLCQK == null ? "" : m_objContent.m_strLCQK);
                objDPArr1[34].Value = (m_objContent.m_strCQJC == null ? "" : m_objContent.m_strCQJC);
                objDPArr1[35].Value = (m_objContent.m_strPregTimes == null ? "" : m_objContent.m_strPregTimes);
                objDPArr1[36].Value = (m_objContent.m_strBornTimes == null ? "" : m_objContent.m_strBornTimes);
                objDPArr1[37].Value = (m_objContent.m_strCarePlan == null ? "" : m_objContent.m_strCarePlan);
                objDPArr1[38].Value = (m_objContent.m_strChargeDoctor == null ? "" : m_objContent.m_strChargeDoctor);
                objDPArr1[39].Value = (m_objContent.m_strDiretDoctor == null ? "" : m_objContent.m_strDiretDoctor);
                objDPArr1[40].Value = (m_objContent.m_strOldMaternitySuffer == null ? "" : m_objContent.m_strOldMaternitySuffer);
                objDPArr1[41].Value = (m_objContent.m_strMidWife == null ? "" : m_objContent.m_strMidWife);
                //修正诊断
                objDPArr1[42].Value = (m_objContent.m_strModifyDiagnose == null ? "" : m_objContent.m_strModifyDiagnose);
                //补充诊断
                objDPArr1[43].Value = (m_objContent.m_strAddDiagnose == null ? "" : m_objContent.m_strAddDiagnose);
                //普通诊断
                objDPArr1[44].Value = (m_objContent.m_strDiagnoseAll == null ? "" : m_objContent.m_strDiagnoseAll);
                //血压
                objDPArr1[45].Value = (m_objContent.m_strBloodPressure == null ? "" : m_objContent.m_strBloodPressure);
                //体重
                objDPArr1[46].Value = (m_objContent.m_strWeight == null ? "" : m_objContent.m_strWeight);
                //补充诊断
                objDPArr1[47].Value = (m_objContent.m_strBuChong == null ? "" : m_objContent.m_strBuChong);

                //执行SQL
                lngEff = 0;
                long lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr1);
                if (lngRes1 <= 0) return lngRes1;

                //******************************************************************
                if (m_objContent.m_strPrimaryDiagnoseArr != null)
                {
                    for (int j1 = 0; j1 < m_objContent.m_strPrimaryDiagnoseArr.Length; j1++)
                    {
                        //						objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[6];
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr1.Length;i++)
                        //							objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                        //				IDataParameter[] objDPArr = null; 
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr1);

                        objDPArr1[0].Value = m_objContent.m_strInPatientID;
                        objDPArr1[1].DbType = DbType.DateTime;
                        objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr1[2].DbType = DbType.DateTime;
                        objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                        objDPArr1[4].Value = j1;

                        if (m_objContent.m_strPrimaryDiagnoseArr[j1] == null || m_objContent.m_strPrimaryDiagnoseArr[j1].Trim() == "")
                            continue;
                        objDPArr1[5].Value = m_objContent.m_strPrimaryDiagnoseArr[j1];

                        //执行SQL					
                        lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContent_PrimaryDiagnoseSQL, ref lngEff, objDPArr1);
                        if (lngRes1 <= 0) return lngRes1;
                    }
                }

                //******************************************************************
                if (m_objContent.m_strFinallyDiagnoseArr != null)
                {
                    for (int j1 = 0; j1 < m_objContent.m_strFinallyDiagnoseArr.Length; j1++)
                    {
                        //						objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[6];
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr1.Length;i++)
                        //							objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                        //				IDataParameter[] objDPArr = null; 
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr1);

                        objDPArr1[0].Value = m_objContent.m_strInPatientID;
                        objDPArr1[1].DbType = DbType.DateTime;
                        objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr1[2].DbType = DbType.DateTime;
                        objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                        objDPArr1[4].Value = j1;

                        if (m_objContent.m_strFinallyDiagnoseArr[j1] == null || m_objContent.m_strFinallyDiagnoseArr[j1].Trim() == "")
                            continue;
                        objDPArr1[5].Value = m_objContent.m_strFinallyDiagnoseArr[j1];

                        //执行SQL					
                        lngRes1 = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContent_FinallyDiagnoseSQL, ref lngEff, objDPArr1);
                        if (lngRes1 <= 0) return lngRes1;
                    }
                }

                lngRes = lngRes1;
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
[AutoComplete] 
		private byte [] m_bytImageToBinary(Image p_img)
		{
			System.IO.MemoryStream objTempStream = new System.IO.MemoryStream();

			p_img.Save(objTempStream,System.Drawing.Imaging.ImageFormat.Bmp);

			return objTempStream.ToArray();
		}
[AutoComplete] 
		private Image m_imgBinaryToImage(object p_obj)
		{
			System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])p_obj);

			Image img = new Bitmap(objStream);

			return img;
		}
		
		// 把新修改的内容保存到数据库。
[AutoComplete] 		
		protected override long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
			clsPictureBoxValue[] p_objPicValueArr,string p_strDiseaseID,string p_strDeptID,
			clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsInPatientCaseHistoryContent m_objContent = (clsInPatientCaseHistoryContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(83, out objDPArr);


                objDPArr[0].Value =  m_objContent.m_strCredibility;
                objDPArr[1].Value =  m_objContent.m_strRepresentor;
                objDPArr[2].Value =  m_objContent.m_strPrimaryDiagnoseDocID;

                objDPArr[3].DbType = DbType.DateTime;
                if (m_objContent.m_strPrimaryDiagnoseDate == "")
                    objDPArr[3].Value = DBNull.Value;
                else
                {
                    try
                    {
                        objDPArr[3].Value = DateTime.Parse(m_objContent.m_strPrimaryDiagnoseDate);
                    }
                    catch { objDPArr[3].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); }
                }
                objDPArr[4].Value = (m_objContent.m_strFinallyDiagnoseDocID == null ? "" : m_objContent.m_strFinallyDiagnoseDocID);

                objDPArr[5].DbType = DbType.DateTime;
                if (m_objContent.m_strFinallyDiagnoseDate == "")
                    objDPArr[5].Value = DBNull.Value;
                else
                {
                    try
                    {
                        objDPArr[5].Value = DateTime.Parse(m_objContent.m_strFinallyDiagnoseDate);
                    }
                    catch
                    {
                        objDPArr[5].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }

                objDPArr[6].Value = (m_objContent.m_strMainDescriptionXML == null ? "" : m_objContent.m_strMainDescriptionXML);
                objDPArr[7].Value = (m_objContent.m_strMainDescriptionAll == null ? "" : m_objContent.m_strMainDescriptionAll);
                objDPArr[8].Value = (m_objContent.m_strCurrentStatusXML == null ? "" : m_objContent.m_strCurrentStatusXML);
                objDPArr[9].Value = (m_objContent.m_strCurrentStatusXAll == null ? "" : m_objContent.m_strCurrentStatusXAll);
                objDPArr[10].Value = (m_objContent.m_strBeforetimeStatusXML == null ? "" : m_objContent.m_strBeforetimeStatusXML);
                objDPArr[11].Value = (m_objContent.m_strBeforetimeStatusAll == null ? "" : m_objContent.m_strBeforetimeStatusAll);
                objDPArr[12].Value = (m_objContent.m_strOwnHistoryXML == null ? "" : m_objContent.m_strOwnHistoryXML);
                objDPArr[13].Value = (m_objContent.m_strOwnHistoryAll == null ? "" : m_objContent.m_strOwnHistoryAll);
                objDPArr[14].Value = (m_objContent.m_strMarriageHistoryXML == null ? "" : m_objContent.m_strMarriageHistoryXML);
                objDPArr[15].Value = (m_objContent.m_strMarriageHistoryAll == null ? "" : m_objContent.m_strMarriageHistoryAll);
                objDPArr[16].Value = (m_objContent.m_strFamilyHistoryXML == null ? "" : m_objContent.m_strFamilyHistoryXML);
                objDPArr[17].Value = (m_objContent.m_strFamilyHistoryAll == null ? "" : m_objContent.m_strFamilyHistoryAll);
                objDPArr[18].Value = (m_objContent.m_strSummaryXML == null ? "" : m_objContent.m_strSummaryXML);
                objDPArr[19].Value = (m_objContent.m_strSummaryAll == null ? "" : m_objContent.m_strSummaryAll);
                objDPArr[20].Value = (m_objContent.m_strPrimaryDiagnoseXML == null ? "" : m_objContent.m_strPrimaryDiagnoseXML);
                objDPArr[21].Value = (m_objContent.m_strPrimaryDiagnoseAll == null ? "" : m_objContent.m_strPrimaryDiagnoseAll);
                objDPArr[22].Value = (m_objContent.m_strFinallyDiagnoseXML == null ? "" : m_objContent.m_strFinallyDiagnoseXML);
                objDPArr[23].Value = (m_objContent.m_strFinallyDiagnoseAll == null ? "" : m_objContent.m_strFinallyDiagnoseAll);
                objDPArr[24].Value = (m_objContent.m_strTemperatureXML == null ? "" : m_objContent.m_strTemperatureXML);
                objDPArr[25].Value = (m_objContent.m_strTemperatureAll == null ? "" : m_objContent.m_strTemperatureAll);
                objDPArr[26].Value = (m_objContent.m_strPulseXML == null ? "" : m_objContent.m_strPulseXML);
                objDPArr[27].Value = (m_objContent.m_strPulseAll == null ? "" : m_objContent.m_strPulseAll);
                objDPArr[28].Value = (m_objContent.m_strBreathXML == null ? "" : m_objContent.m_strBreathXML);
                objDPArr[29].Value = (m_objContent.m_strBreathAll == null ? "" : m_objContent.m_strBreathAll);
                objDPArr[30].Value = (m_objContent.m_strSysXML == null ? "" : m_objContent.m_strSysXML);
                objDPArr[31].Value = (m_objContent.m_strSysAll == null ? "" : m_objContent.m_strSysAll);
                objDPArr[32].Value = (m_objContent.m_strDiaXML == null ? "" : m_objContent.m_strDiaXML);
                objDPArr[33].Value = (m_objContent.m_strDiaAll == null ? "" : m_objContent.m_strDiaAll);
                objDPArr[34].Value = (m_objContent.m_strBloodPressureUnitXML == null ? "" : m_objContent.m_strBloodPressureUnitXML);
                objDPArr[35].Value = (m_objContent.m_strBloodPressureUnitAll == null ? "" : m_objContent.m_strBloodPressureUnitAll);
                objDPArr[36].Value = (m_objContent.m_strMedicalXML == null ? "" : m_objContent.m_strMedicalXML);
                objDPArr[37].Value = (m_objContent.m_strMedicalAll == null ? "" : m_objContent.m_strMedicalAll);
                objDPArr[38].Value = (m_objContent.m_strProfessionalCheckXML == null ? "" : m_objContent.m_strProfessionalCheckXML);
                objDPArr[39].Value = (m_objContent.m_strProfessionalCheckAll == null ? "" : m_objContent.m_strProfessionalCheckAll);
                objDPArr[40].Value = (m_objContent.m_strLabCheckAll == null ? "" : m_objContent.m_strLabCheckAll);
                objDPArr[41].Value = (m_objContent.m_strLabCheckXML == null ? "" : m_objContent.m_strLabCheckXML);
                objDPArr[42].Value = (m_objContent.m_strCatameniaHistoryAll == null ? "" : m_objContent.m_strCatameniaHistoryAll);
                objDPArr[43].Value = (m_objContent.m_strCatameniaHistoryXML == null ? "" : m_objContent.m_strCatameniaHistoryXML);

                objDPArr[44].Value = (m_objContent.m_strYJSAll == null ? "" : m_objContent.m_strYJSAll);
                objDPArr[45].Value = (m_objContent.m_strYJSXML == null ? "" : m_objContent.m_strYJSXML);
                objDPArr[46].Value = (m_objContent.m_strContraHistoryAll == null ? "" : m_objContent.m_strContraHistoryAll);
                objDPArr[47].Value = (m_objContent.m_strContraHistoryXML == null ? "" : m_objContent.m_strContraHistoryXML);
                objDPArr[48].Value = (m_objContent.m_strShYSAll == null ? "" : m_objContent.m_strShYSAll);
                objDPArr[49].Value = (m_objContent.m_strShYSXML == null ? "" : m_objContent.m_strShYSXML);
                objDPArr[50].Value = (m_objContent.m_strLCQKAll == null ? "" : m_objContent.m_strLCQKAll);
                objDPArr[51].Value = (m_objContent.m_strLCQKXML == null ? "" : m_objContent.m_strLCQKXML);
                objDPArr[52].Value = (m_objContent.m_strCQJCAll == null ? "" : m_objContent.m_strCQJCAll);
                objDPArr[53].Value = (m_objContent.m_strCQJCXML == null ? "" : m_objContent.m_strCQJCXML);

                objDPArr[54].Value = (m_objContent.m_strCarePlanAll == null ? "" : m_objContent.m_strCarePlanAll);
                objDPArr[55].Value = (m_objContent.m_strCarePlanXML == null ? "" : m_objContent.m_strCarePlanXML);
                objDPArr[56].Value = (m_objContent.m_strOldMaternitySufferAll == null ? "" : m_objContent.m_strOldMaternitySufferAll);
                objDPArr[57].Value = (m_objContent.m_strOldMaternitySufferXML == null ? "" : m_objContent.m_strOldMaternitySufferXML);

                //修正诊断
                objDPArr[58].Value = (m_objContent.m_strModifyDiagnoseAll == null ? "" : m_objContent.m_strModifyDiagnoseAll);
                objDPArr[59].Value = (m_objContent.m_strModifyDiagnoseXML == null ? "" : m_objContent.m_strModifyDiagnoseXML);
                objDPArr[60].Value = (m_objContent.m_strModifyDiagnoseDoctorID == null ? "" : m_objContent.m_strModifyDiagnoseDoctorID);

                objDPArr[61].DbType = DbType.DateTime;
                try
                {
                    objDPArr[61].Value = m_objContent.m_datModifyDiagnose;
                }
                catch
                {
                    objDPArr[61].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                //补充诊断
                objDPArr[62].Value = (m_objContent.m_strAddDiagnoseALL == null ? "" : m_objContent.m_strAddDiagnoseALL);
                objDPArr[63].Value = (m_objContent.m_strAddDiagnoseXML == null ? "" : m_objContent.m_strAddDiagnoseXML);
                objDPArr[64].Value = (m_objContent.m_strAddDiagnoseDoctorID == null ? "" : m_objContent.m_strAddDiagnoseDoctorID);

                objDPArr[65].DbType = DbType.DateTime;
                try
                {
                    objDPArr[65].Value = m_objContent.m_datAddDiagnose;
                }
                catch
                {
                    objDPArr[65].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                objDPArr[66].Value = m_objContent.m_intSelectedMC;
                objDPArr[67].Value = m_objContent.m_intSELECTEDLASTCATAMENIATIME;
                objDPArr[68].Value = m_objContent.m_intSELECTEDAMENIAAGE;
                objDPArr[69].Value = m_objContent.m_strAMENIAAGE;           

               
                //普通诊断
                objDPArr[70].Value = (m_objContent.m_strDiagnoseOK == null ? "" : m_objContent.m_strDiagnoseOK);
                objDPArr[71].Value = (m_objContent.m_strDiagnosetxtXML == null ? "" : m_objContent.m_strDiagnosetxtXML);
                objDPArr[72].Value = (m_objContent.m_strDiagnoseDoc == null ? "" : m_objContent.m_strDiagnoseDoc);
                objDPArr[73].DbType = DbType.DateTime;
                objDPArr[73].Value = m_objContent.m_dtDiagnoseDate;
                //体重
                objDPArr[74].Value = m_objContent.m_strWeightAll;
                objDPArr[75].Value = m_objContent.m_strWeightXML;

                //补充诊断
                objDPArr[76].Value = (m_objContent.m_strBuChongALL == null ? "" : m_objContent.m_strBuChongALL);
                objDPArr[77].Value = (m_objContent.m_strBuChongXML == null ? "" : m_objContent.m_strBuChongXML);
                objDPArr[78].Value = (m_objContent.m_strBuChongDoctorID == null ? "" : m_objContent.m_strBuChongDoctorID);

                objDPArr[79].DbType = DbType.DateTime;
                try
                {
                    objDPArr[79].Value = m_objContent.m_dateBuChong;
                }
                catch
                {
                    objDPArr[79].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                //条件
                objDPArr[80].Value = m_objContent.m_strInPatientID;
                objDPArr[81].DbType = DbType.DateTime;
                objDPArr[81].Value = m_objContent.m_dtmInPatientDate;
                objDPArr[82].DbType = DbType.DateTime;
                objDPArr[82].Value = m_objContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                #region 旧的修改画图
                //			if(p_objPicValueArr!=null && p_objPicValueArr.Length>0)
                //			{
                //				IDataParameter[] objDPArr0 = new Oracle.DataAccess.Client.OracleParameter[8];
                //
                //				for(int j=0;j<p_objPicValueArr.Length;j++)
                //				{
                //					//按顺序给IDataParameter赋值
                //					for(int i=0;i<objDPArr0.Length;i++)
                //						objDPArr0[i]=new Oracle.DataAccess.Client.OracleParameter();
                //
                //					objDPArr0[0].Value=j+1;
                //					//					objDPArr0[4].DbType = System.Data.DbType.Binary;
                //					//					objDPArr0[5].DbType = System.Data.DbType.Binary;
                //
                //					//					if(p_objPicValueArr[j].m_imgBack!=null)
                //					//						objDPArr0[4].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgBack);	
                //					//					else
                //					//					objDPArr0[4].Value= new System.Array();
                //						
                //					if(p_objPicValueArr[j].m_imgFront!=null)
                //						objDPArr0[1].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgFront);	
                //					else
                //						objDPArr0[1].Value= System.DBNull.Value;
                //
                //					//					objDPArr0[5].Value=Convert.ToString(p_objPicValueArr[j].clrBack);
                //					objDPArr0[2].Value=p_objPicValueArr[j].clrBack.ToArgb();
                //
                //					//					int intTemp = 32;
                //					//					Color.FromArgb((intTemp)
                //
                //					objDPArr0[3].Value= p_objPicValueArr[j].intWidth;
                //					objDPArr0[4].Value= p_objPicValueArr[j].intHeight;
                //
                //					objDPArr0[5].Value=m_objContent.m_strInPatientID;
                //					objDPArr0[6].Value=m_objContent.m_dtmInPatientDate;
                //					objDPArr0[7].Value=m_objContent.m_dtmOpenDate;
                //
                //					//执行SQL
                //					lngEff=0;
                //					long lngRes0 =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecord_PictureSQL,ref lngEff,objDPArr0);
                //					if(lngRes0<=0)	return lngRes0;
                //				}
                //			}
                #endregion 旧的修改画图

                #region 新的修改画图

                #region 先删除旧的记录,以防数据库过大
                //			if(p_objPicValueArr!=null && p_objPicValueArr.Length>0)
                //			{
                //					IDataParameter[] objDPArr00 = new Oracle.DataAccess.Client.OracleParameter[3];
                //
                //	//				for(int j=0;j<p_objPicValueArr.Length;j++)
                //	//				{
                //						//按顺序给IDataParameter赋值
                //						for(int i=0;i<objDPArr00.Length;i++)
                //							objDPArr00[i]=new Oracle.DataAccess.Client.OracleParameter();
                IDataParameter[] objDPArr00 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr00);

                objDPArr00[0].Value = m_objContent.m_strInPatientID;
                objDPArr00[1].DbType = DbType.DateTime;
                objDPArr00[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr00[2].DbType = DbType.DateTime;
                objDPArr00[2].Value = m_objContent.m_dtmOpenDate;

                //执行SQL
                lngEff = 0;
                long lngRes00 = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecord_PictureSQL, ref lngEff, objDPArr00);
                if (lngRes00 <= 0) return lngRes00;
                //				}
                //			}
                #endregion 先删除旧的记录,以防数据库过大

                #region 再添加新的记录
                if (p_objPicValueArr != null && p_objPicValueArr.Length > 0)
                {


                    for (int j = 0; j < p_objPicValueArr.Length; j++)
                    {

                        IDataParameter[] objDPArr0 = null;
                        objHRPServ.CreateDatabaseParameter(8, out objDPArr0);

                        objDPArr0[0].Value = m_objContent.m_strInPatientID;
                        objDPArr0[1].DbType = DbType.DateTime;
                        objDPArr0[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr0[2].DbType = DbType.DateTime;
                        objDPArr0[2].Value = m_objContent.m_dtmOpenDate;

                        objDPArr0[3].Value = j + 1;
                        //					objDPArr0[4].DbType = System.Data.DbType.Binary;
                        //					objDPArr0[5].DbType = System.Data.DbType.Binary;

                        //					if(p_objPicValueArr[j].m_imgBack!=null)
                        //						objDPArr0[4].Value= m_bytImageToBinary(p_objPicValueArr[j].m_imgBack);	
                        //					else
                        //					objDPArr0[4].Value= new System.Array();
                        objDPArr0[4].DbType = DbType.Binary;
                        if (p_objPicValueArr[j].m_bytImage != null)
                            objDPArr0[4].Value = p_objPicValueArr[j].m_bytImage;//m_bytImageToBinary(p_objPicValueArr[j].m_imgFront);	
                        else
                            objDPArr0[4].Value = System.DBNull.Value;

                        //					objDPArr0[5].Value=Convert.ToString(p_objPicValueArr[j].clrBack);
                        objDPArr0[5].Value = p_objPicValueArr[j].clrBack.ToArgb();

                        //					int intTemp = 32;
                        //					Color.FromArgb((intTemp)

                        objDPArr0[6].Value = p_objPicValueArr[j].intWidth;
                        objDPArr0[7].Value = p_objPicValueArr[j].intHeight;


                        //执行SQL
                        lngEff = 0;
                        long lngRes0 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecord_PictureSQL, ref lngEff, objDPArr0);
                        if (lngRes0 <= 0) return lngRes0;
                    }
                }
                #endregion 再添加新的记录
                #endregion 新的修改画图

                #region 保存病名
                if (p_strDiseaseID != "")//套装模板与病名挂勾
                {
                    lngRes = m_lngSavePatient_Disease(m_objContent.m_strInPatientID, m_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strDiseaseID, p_strDeptID, p_objHRPServ);
                    if (lngRes <= 0) return lngRes;
                }
                #endregion


                //******************************************************************
                //				IDataParameter[] objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[42];
                //				//按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr1.Length;i++)
                //					objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(48, out objDPArr1);

                objDPArr1[0].Value = m_objContent.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                objDPArr1[3].DbType = DbType.DateTime;
                objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                objDPArr1[4].Value = (m_objContent.m_strModifyUserID == null ? "" : m_objContent.m_strModifyUserID);

                objDPArr1[5].DbType = DbType.DateTime;
                if (m_objContent.m_dtmDeActivedDate == DateTime.MinValue)
                    objDPArr1[5].Value = DBNull.Value;
                else
                    objDPArr1[5].Value = m_objContent.m_dtmDeActivedDate;

                objDPArr1[6].Value = (m_objContent.m_strDeActivedOperatorID == null ? "" : m_objContent.m_strDeActivedOperatorID);
                objDPArr1[7].Value = m_objContent.m_bytStatus;

                objDPArr1[8].Value = (m_objContent.m_strMainDescription == null ? "" : m_objContent.m_strMainDescription);
                objDPArr1[9].Value = (m_objContent.m_strCurrentStatus == null ? "" : m_objContent.m_strCurrentStatus);
                objDPArr1[10].Value = (m_objContent.m_strBeforetimeStatus == null ? "" : m_objContent.m_strBeforetimeStatus);
                objDPArr1[11].Value = (m_objContent.m_strOwnHistory == null ? "" : m_objContent.m_strOwnHistory);
                objDPArr1[12].Value = (m_objContent.m_strMarriageHistory == null ? "" : m_objContent.m_strMarriageHistory);
                objDPArr1[13].Value = (m_objContent.m_strFamilyHistory == null ? "" : m_objContent.m_strFamilyHistory);
                objDPArr1[14].Value = (m_objContent.m_strSummary == null ? "" : m_objContent.m_strSummary);
                //			objDPArr1[15].Value=(m_objContent.m_strPrimaryDiagnose==null ? "":m_objContent.m_strPrimaryDiagnose);
                //			objDPArr1[16].Value=(m_objContent.m_strFinallyDiagnose==null ? "":m_objContent.m_strFinallyDiagnose);
                objDPArr1[15].Value = (m_objContent.m_strTemperature == null ? "" : m_objContent.m_strTemperature);
                objDPArr1[16].Value = (m_objContent.m_strPulse == null ? "" : m_objContent.m_strPulse);
                objDPArr1[17].Value = (m_objContent.m_strBreath == null ? "" : m_objContent.m_strBreath);
                objDPArr1[18].Value = (m_objContent.m_strSys == null ? "" : m_objContent.m_strSys);
                objDPArr1[19].Value = (m_objContent.m_strDia == null ? "" : m_objContent.m_strDia);
                objDPArr1[20].Value = (m_objContent.m_strBloodPressureUnit == null ? "" : m_objContent.m_strBloodPressureUnit);
                objDPArr1[21].Value = (m_objContent.m_strMedical == null ? "" : m_objContent.m_strMedical);
                objDPArr1[22].Value = (m_objContent.m_strProfessionalCheck == null ? "" : m_objContent.m_strProfessionalCheck);
                objDPArr1[23].Value = (m_objContent.m_strLabCheck == null ? "" : m_objContent.m_strLabCheck);
                objDPArr1[24].Value = (m_objContent.m_strCatameniaHistory == null ? "" : m_objContent.m_strCatameniaHistory);
                objDPArr1[25].Value = (m_objContent.m_strFirstCatamenia == null ? "" : m_objContent.m_strFirstCatamenia);
                objDPArr1[26].Value = (m_objContent.m_strCatameniaLastTime == null ? "" : m_objContent.m_strCatameniaLastTime);
                objDPArr1[27].Value = (m_objContent.m_strCatameniaCycle == null ? "" : m_objContent.m_strCatameniaCycle);
                objDPArr1[28].DbType = DbType.DateTime;
                objDPArr1[28].Value = m_objContent.m_dtmLastCatameniaTime;
                objDPArr1[29].Value = (m_objContent.m_strCatameniaCase == null ? "" : m_objContent.m_strCatameniaCase);
                objDPArr1[30].Value = (m_objContent.m_strYJS == null ? "" : m_objContent.m_strYJS);
                objDPArr1[31].Value = (m_objContent.m_strContraHistory == null ? "" : m_objContent.m_strContraHistory);
                objDPArr1[32].Value = (m_objContent.m_strShYS == null ? "" : m_objContent.m_strShYS);
                objDPArr1[33].Value = (m_objContent.m_strLCQK == null ? "" : m_objContent.m_strLCQK);
                objDPArr1[34].Value = (m_objContent.m_strCQJC == null ? "" : m_objContent.m_strCQJC);
                objDPArr1[35].Value = (m_objContent.m_strPregTimes == null ? "" : m_objContent.m_strPregTimes);
                objDPArr1[36].Value = (m_objContent.m_strBornTimes == null ? "" : m_objContent.m_strBornTimes);
                objDPArr1[37].Value = (m_objContent.m_strCarePlan == null ? "" : m_objContent.m_strCarePlan);
                objDPArr1[38].Value = (m_objContent.m_strChargeDoctor == null ? "" : m_objContent.m_strChargeDoctor);
                objDPArr1[39].Value = (m_objContent.m_strDiretDoctor == null ? "" : m_objContent.m_strDiretDoctor);
                objDPArr1[40].Value = (m_objContent.m_strOldMaternitySuffer == null ? "" : m_objContent.m_strOldMaternitySuffer);
                objDPArr1[41].Value = (m_objContent.m_strMidWife == null ? "" : m_objContent.m_strMidWife);
                //修正诊断
                objDPArr1[42].Value = (m_objContent.m_strModifyDiagnose == null ? "" : m_objContent.m_strModifyDiagnose);
                //补充诊断
                objDPArr1[43].Value = (m_objContent.m_strAddDiagnose == null ? "" : m_objContent.m_strAddDiagnose);
                //普通诊断
                objDPArr1[44].Value = (m_objContent.m_strDiagnoseAll == null ? "" : m_objContent.m_strDiagnoseAll);
                //血压
                objDPArr1[45].Value = (m_objContent.m_strBloodPressure == null ? "" : m_objContent.m_strBloodPressure);
                //体重
                objDPArr1[46].Value = (m_objContent.m_strWeight == null ? "" : m_objContent.m_strWeight);
                //补充诊断
                objDPArr1[47].Value = (m_objContent.m_strBuChong == null ? "" : m_objContent.m_strBuChong);

                //执行SQL
                lngEff = 0;
                long lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr1);
                if (lngRes1 <= 0) return lngRes1;

                //******************************************************************
                if (m_objContent.m_strPrimaryDiagnoseArr != null)
                {
                    for (int j1 = 0; j1 < m_objContent.m_strPrimaryDiagnoseArr.Length; j1++)
                    {
                        //						objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[6];
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr1.Length;i++)
                        //							objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                        //				IDataParameter[] objDPArr = null; 
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr1);

                        objDPArr1[0].Value = m_objContent.m_strInPatientID;
                        objDPArr1[1].DbType = DbType.DateTime;
                        objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr1[2].DbType = DbType.DateTime;
                        objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                        objDPArr1[4].Value = j1;

                        if (m_objContent.m_strPrimaryDiagnoseArr[j1] == null || m_objContent.m_strPrimaryDiagnoseArr[j1].Trim() == "")
                            continue;
                        objDPArr1[5].Value = m_objContent.m_strPrimaryDiagnoseArr[j1];

                        //执行SQL					
                        lngRes1 = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContent_PrimaryDiagnoseSQL, ref lngEff, objDPArr1);
                        if (lngRes1 <= 0) return lngRes1;
                    }
                }

                //******************************************************************
                if (m_objContent.m_strFinallyDiagnoseArr != null)
                {
                    for (int j1 = 0; j1 < m_objContent.m_strFinallyDiagnoseArr.Length; j1++)
                    {
                        //						objDPArr1 = new Oracle.DataAccess.Client.OracleParameter[6];
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr1.Length;i++)
                        //							objDPArr1[i]=new Oracle.DataAccess.Client.OracleParameter();
                        //				IDataParameter[] objDPArr = null; 
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr1);

                        objDPArr1[0].Value = m_objContent.m_strInPatientID;
                        objDPArr1[1].DbType = DbType.DateTime;
                        objDPArr1[1].Value = m_objContent.m_dtmInPatientDate;
                        objDPArr1[2].DbType = DbType.DateTime;
                        objDPArr1[2].Value = m_objContent.m_dtmOpenDate;
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = m_objContent.m_dtmModifyDate;
                        objDPArr1[4].Value = j1;

                        if (m_objContent.m_strFinallyDiagnoseArr[j1] == null || m_objContent.m_strFinallyDiagnoseArr[j1].Trim() == "")
                            continue;
                        objDPArr1[5].Value = m_objContent.m_strFinallyDiagnoseArr[j1];

                        //执行SQL					
                        lngRes1 = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContent_FinallyDiagnoseSQL, ref lngEff, objDPArr1);
                        if (lngRes1 <= 0) return lngRes1;
                    }
                }
                 
                lngRes = lngRes1;

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

		
		// 把记录从数据中“删除”。
		[AutoComplete] 
		protected override long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                    return (long)enmOperationResult.Parameter_Error;
                //获取IDataParameter数组

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
                p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngEff < 0) return -1;

                #region 删除画图信息
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecord_PictureSQL, ref lngEff, objDPArr1);
                #endregion

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

		
		// 获取数据库中最新的修改时间和首次打印时间
		[AutoComplete] 
		protected override long m_lngGetModifyDateAndFirstPrintDate
			(string p_strInPatientID,
			string p_strInPatientDate,/*string p_strOpenRecordTime,*/clsHRPTableService p_objHRPServ,
			out DateTime p_dtmModifyDate,
			out string p_strFirstPrintDate)
		{
			p_dtmModifyDate=DateTime.MinValue ;
			p_strFirstPrintDate=null;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["LASTMODIFYDATE"].ToString());
                }
                //返回
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

            }			//返回
			return lngRes;

		}

		// 获取指定已经被删除记录的内容。		
		[AutoComplete] 
		protected override long m_lngGetDeleteRecordContentWithServ(
			string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenRecordTime,
			clsHRPTableService p_objHRPServ,
			out clsBaseCaseHistoryInfo p_objRecordContent)
		{
			p_objRecordContent=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenRecordTime);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeletedRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsInPatientCaseHistoryContent p_objContent = new clsInPatientCaseHistoryContent();
                    DataRow objRow = dtbValue.Rows[0];
                    //for (int i = 0; i < dtbValue.Rows.Count; i++)
                    //{
                        //设置结果
                        p_objContent.m_bytIfConfirm = byte.Parse(objRow["IFCONFIRM"].ToString());
                        p_objContent.m_bytStatus = byte.Parse(objRow["STATUS"].ToString());
                        p_objContent.m_dtmCreateDate = (objRow["CREATEDATE"].ToString() != null ? DateTime.Parse(objRow["CREATEDATE"].ToString()) : DateTime.MinValue);

                        p_objContent.m_dtmDeActivedDate = (objRow["DEACTIVEDDATE"].ToString() != "" ? DateTime.Parse(objRow["DEACTIVEDDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmFirstPrintDate = (objRow["FIRSTPRINTDATE"].ToString() != "" ? DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmInPatientDate = (objRow["INPATIENTDATE"].ToString() != "" ? DateTime.Parse(objRow["INPATIENTDATE"].ToString()) : DateTime.MinValue);

                        p_objContent.m_dtmModifyDate = (objRow["LASTMODIFYDATE"].ToString() != "" ? DateTime.Parse(objRow["LASTMODIFYDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_dtmOpenDate = (objRow["OPENDATE"].ToString() != "" ? DateTime.Parse(objRow["OPENDATE"].ToString()) : DateTime.MinValue);
                        p_objContent.m_strBeforetimeStatus = objRow["BEFORETIMESTATUS"].ToString();

                        p_objContent.m_strBeforetimeStatusAll = objRow["BEFORETIMESTATUSALL"].ToString();
                        p_objContent.m_strBeforetimeStatusXML = objRow["BEFORETIMESTATUSXML"].ToString();
                        p_objContent.m_strBloodPressureUnit = objRow["BLOODPRESSUREUNIT"].ToString();

                        p_objContent.m_strBloodPressureUnitAll = objRow["BLOODPRESSUREUNITALL"].ToString();
                        p_objContent.m_strBloodPressureUnitXML = objRow["BLOODPRESSUREUNITXML"].ToString();
                        p_objContent.m_strBreath = objRow["BREATH"].ToString();

                        p_objContent.m_strBreathAll = objRow["BREATHALL"].ToString();
                        p_objContent.m_strBreathXML = objRow["BREATHXML"].ToString();
                        p_objContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();

                        p_objContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();
                        p_objContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                        p_objContent.m_strCreateName = objRow["FIRSTNAME"].ToString();
                        p_objContent.m_strCredibility = objRow["CREDIBILITY"].ToString();

                        p_objContent.m_strCurrentStatus = objRow["CURRENTSTATUS"].ToString();
                        p_objContent.m_strCurrentStatusXAll = objRow["CURRENTSTATUSALL"].ToString();
                        p_objContent.m_strCurrentStatusXML = objRow["CURRENTSTATUSXML"].ToString();

                        p_objContent.m_strCatameniaHistory = objRow["CATAMENIAHISTORY"].ToString();
                        p_objContent.m_strCatameniaHistoryAll = objRow["CATAMENIAHISTORYALL"].ToString();
                        p_objContent.m_strCatameniaHistoryXML = objRow["CATAMENIAHISTORYXML"].ToString();

                        p_objContent.m_strDeActivedOperatorID = objRow["DEACTIVEDOPERATORID"].ToString();

                        p_objContent.m_strDia = objRow["DIA"].ToString();
                        p_objContent.m_strDiaAll = objRow["DIAALL"].ToString();
                        p_objContent.m_strDiaXML = objRow["DIAXML"].ToString();

                        p_objContent.m_strFamilyHistory = objRow["FAMILYHISTORY"].ToString();
                        p_objContent.m_strFamilyHistoryAll = objRow["FAMILYHISTORYALL"].ToString();
                        p_objContent.m_strFamilyHistoryXML = objRow["FAMILYHISTORYXML"].ToString();

                        //p_objContent.m_strFinallyDiagnose=objRow["FINALLYDIAGNOSE"].ToString() ;
                        p_objContent.m_strFinallyDiagnoseAll = objRow["FINALLYDIAGNOSEALL"].ToString();
                        p_objContent.m_strFinallyDiagnoseXML = objRow["FINALLYDIAGNOSEXML"].ToString();

                        p_objContent.m_strFinallyDiagnoseDate = objRow["FINALLYDIAGNOSEDATE"].ToString();
                        p_objContent.m_strFinallyDiagnoseDocID = objRow["FINALLYDIAGNOSEDOCID"].ToString();
                        p_objContent.m_strInPatientID = objRow["INPATIENTID"].ToString();

                        p_objContent.m_strLabCheckAll = objRow["LABCHECKALL"].ToString();
                        p_objContent.m_strLabCheckXML = objRow["LABCHECKXML"].ToString();
                        p_objContent.m_strLabCheck = objRow["LABCHECK"].ToString();

                        p_objContent.m_strMainDescription = objRow["MAINDESCRIPTION"].ToString();
                        p_objContent.m_strMainDescriptionAll = objRow["MAINDESCRIPTIONALL"].ToString();
                        p_objContent.m_strMainDescriptionXML = objRow["MAINDESCRIPTIONXML"].ToString();

                        p_objContent.m_strMarriageHistory = objRow["MARRIAGEHISTORY"].ToString();
                        p_objContent.m_strMarriageHistoryAll = objRow["MARRIAGEHISTORYALL"].ToString();
                        p_objContent.m_strMarriageHistoryXML = objRow["MARRIAGEHISTORYXML"].ToString();

                        p_objContent.m_strMedical = objRow["MEDICAL"].ToString();
                        p_objContent.m_strMedicalAll = objRow["MEDICALALL"].ToString();
                        p_objContent.m_strMedicalXML = objRow["MEDICALXML"].ToString();

                        p_objContent.m_strModifyUserID = objRow["LASTMODIFYUSERID"].ToString();
                        p_objContent.m_strOwnHistory = objRow["OWNHISTORY"].ToString();
                        p_objContent.m_strOwnHistoryAll = objRow["OWNHISTORYALL"].ToString();

                        p_objContent.m_strOwnHistoryXML = objRow["OWNHISTORYXML"].ToString();
                        //p_objContent.m_strPrimaryDiagnose=objRow["PRIMARYDIAGNOSE"].ToString() ;
                        p_objContent.m_strPrimaryDiagnoseAll = objRow["PRIMARYDIAGNOSEALL"].ToString();

                        p_objContent.m_strPrimaryDiagnoseDate = objRow["PRIMARYDIAGNOSEDATE"].ToString();
                        p_objContent.m_strPrimaryDiagnoseDocID = objRow["PRIMARYDIAGNOSEDOCID"].ToString();
                        p_objContent.m_strPrimaryDiagnoseXML = objRow["PRIMARYDIAGNOSEXML"].ToString();

                        p_objContent.m_strProfessionalCheck = objRow["PROFESSIONALCHECK"].ToString();
                        p_objContent.m_strProfessionalCheckAll = objRow["PROFESSIONALCHECKALL"].ToString();
                        p_objContent.m_strProfessionalCheckXML = objRow["PROFESSIONALCHECKXML"].ToString();

                        p_objContent.m_strPulse = objRow["PULSE"].ToString();
                        p_objContent.m_strPulseAll = objRow["PULSEALL"].ToString();
                        p_objContent.m_strPulseXML = objRow["PULSEXML"].ToString();

                        p_objContent.m_strRepresentor = objRow["REPRESENTOR"].ToString();
                        p_objContent.m_strSummary = objRow["SUMMARY"].ToString();
                        p_objContent.m_strSummaryAll = objRow["SUMMARYALL"].ToString();

                        p_objContent.m_strSummaryXML = objRow["SUMMARYXML"].ToString();
                        p_objContent.m_strSys = objRow["SYS"].ToString();
                        p_objContent.m_strSysAll = objRow["SYSALL"].ToString();

                        p_objContent.m_strSysXML = objRow["SYSXML"].ToString();
                        p_objContent.m_strTemperature = objRow["TEMPERATURE"].ToString();
                        p_objContent.m_strTemperatureAll = objRow["TEMPERATUREALL"].ToString();

                        p_objContent.m_strTemperatureXML = objRow["TEMPERATUREXML"].ToString();

                        p_objContent.m_strYJS = objRow["YJS"].ToString();
                        p_objContent.m_strYJSAll = objRow["YJSALL"].ToString();
                        p_objContent.m_strYJSXML = objRow["YJSXML"].ToString();

                        p_objContent.m_strContraHistory = objRow["CONTRAHISTORY"].ToString();
                        p_objContent.m_strContraHistoryAll = objRow["CONTRAHISTORYALL"].ToString();
                        p_objContent.m_strContraHistoryXML = objRow["CONTRAHISTORYXML"].ToString();

                        p_objContent.m_strShYS = objRow["SHYS"].ToString();
                        p_objContent.m_strShYSAll = objRow["SHYSALL"].ToString();
                        p_objContent.m_strShYSXML = objRow["SHYSXML"].ToString();

                        p_objContent.m_strLCQK = objRow["LCQK"].ToString();
                        p_objContent.m_strLCQKAll = objRow["LCQKALL"].ToString();
                        p_objContent.m_strLCQKXML = objRow["LCQKXML"].ToString();

                        p_objContent.m_strCQJC = objRow["CQJC"].ToString();
                        p_objContent.m_strCQJCAll = objRow["CQJCALL"].ToString();
                        p_objContent.m_strCQJCXML = objRow["CQJCXML"].ToString();

                        p_objContent.m_strCarePlan = objRow["CAREPLAN"].ToString();
                        p_objContent.m_strCarePlanAll = objRow["CAREPLANALL"].ToString();
                        p_objContent.m_strCarePlanXML = objRow["CAREPLANXML"].ToString();

                        p_objContent.m_strPregTimes = objRow["PREGTIMES"].ToString();
                        p_objContent.m_strBornTimes = objRow["BORNTIMES"].ToString();
                        p_objContent.m_strChargeDoctor = objRow["CHARGEDOCTOR"].ToString();
                        p_objContent.m_strDiretDoctor = objRow["DIRETDOCTOR"].ToString();
                        p_objContent.m_strMidWife = objRow["MIDWIFE"].ToString();

                        p_objContent.m_strOldMaternitySuffer = objRow["OLDMATERNITYSUFFER"].ToString();
                        p_objContent.m_strOldMaternitySufferAll = objRow["OLDMATERNITYSUFFERALL"].ToString();
                        p_objContent.m_strOldMaternitySufferXML = objRow["OLDMATERNITYSUFFERXML"].ToString();
                        if (objRow["SELECTEDMC"] == DBNull.Value)
                        {
                            p_objContent.m_intSelectedMC = 0;
                        }
                        else
                        {
                            p_objContent.m_intSelectedMC = Convert.ToInt32(objRow["SELECTEDMC"]);
                        }

                        if (objRow["SELECTEDLASTCATAMENIATIME"] == DBNull.Value)
                        {
                            p_objContent.m_intSELECTEDLASTCATAMENIATIME = 0;
                        }
                        else
                        {
                            p_objContent.m_intSELECTEDLASTCATAMENIATIME = Convert.ToInt32(objRow["SELECTEDLASTCATAMENIATIME"]);
                        }

                        if (objRow["SELECTEDAMENIAAGE"] == DBNull.Value)
                        {
                            p_objContent.m_intSELECTEDAMENIAAGE = 0;
                        }
                        else
                        {
                            p_objContent.m_intSELECTEDAMENIAAGE = Convert.ToInt32(objRow["SELECTEDAMENIAAGE"]);
                        }
                        p_objContent.m_strAMENIAAGE = objRow["AMENIAAGE"].ToString();

                        //修正诊断
                        p_objContent.m_strModifyDiagnose = objRow["MODIFYDIAGNOSE"].ToString();
                        p_objContent.m_strModifyDiagnoseAll = objRow["MODIFYDIAGNOSEALL"].ToString();
                        p_objContent.m_strModifyDiagnoseXML = objRow["MODIFYDIAGNOSEXML"].ToString();
                        p_objContent.m_strModifyDiagnoseDoctorID = objRow["MODIFYDIAGNOSEDOCTORID"].ToString();
                        try
                        {
                            p_objContent.m_datModifyDiagnose = DateTime.Parse(objRow["MODIFYDIAGNOSEDATE"].ToString());
                        }
                        catch
                        {
                            p_objContent.m_datModifyDiagnose = DateTime.Now;

                        }
                        //补充诊断
                        p_objContent.m_strAddDiagnose = objRow["ADDDIAGNOSE"].ToString();
                        p_objContent.m_strAddDiagnoseALL = objRow["ADDDIAGNOSEALL"].ToString();
                        p_objContent.m_strAddDiagnoseXML = objRow["ADDDIAGNOSEXML"].ToString();
                        p_objContent.m_strAddDiagnoseDoctorID = objRow["ADDDIAGNOSEDOCTORID"].ToString();
                        try
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Parse(objRow["ADDDIAGNOSEDATE"].ToString());

                        }
                        catch
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Now;

                        }

                        //补充诊断
                        p_objContent.m_strBuChong = objRow["BUCHONG"].ToString();
                        p_objContent.m_strBuChongALL = objRow["BUCHONGALL"].ToString();
                        p_objContent.m_strBuChongXML = objRow["BUCHONGXML"].ToString();
                        p_objContent.m_strBuChongDoctorID = objRow["BUCHONGID"].ToString();
                        try
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Parse(objRow["BUCHONGDATE"].ToString());

                        }
                        catch
                        {
                            p_objContent.m_datAddDiagnose = DateTime.Now;

                        }

                        //普通诊断
                        p_objContent.m_strDiagnoseAll = objRow["DIAGNOSENOR"].ToString();
                        p_objContent.m_strDiagnoseOK = objRow["NORMALDIAGNOSEALL"].ToString();
                        p_objContent.m_strDiagnosetxtXML = objRow["NORMALDIAGNOSEALLXML"].ToString();
                        p_objContent.m_strDiagnoseDoc = objRow["NORMALDIAGNOSEALLDICTIRID"].ToString();
                        
                        //体重
                        p_objContent.m_strWeightAll = objRow["WEIGHTALL"].ToString();
                        p_objContent.m_strWeight = objRow["WEIGHT"].ToString();
                        p_objContent.m_strWeightXML = objRow["WEIGHTXML"].ToString();
                        //血压
                        p_objContent.m_strBloodPressure = objRow["bloodpressure"].ToString();
                        try
                        {
                            p_objContent.m_dtDiagnoseDate = DateTime.Parse(objRow["NORMALDIAGNOSEALLDATE"].ToString());
                        }
                        catch
                        {
                            p_objContent.m_dtDiagnoseDate = DateTime.Now;
                        }

                    //}

                    p_objRecordContent = p_objContent;
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

            }			//返回
			return lngRes;


		}
[AutoComplete] 
		private long m_lngAddPatient_Disease(string p_strPatID,string p_strInPatientDate,string p_strDiseaseID)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strPatID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDiseaseID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPatient_Disease, ref lngEff, objDPArr);

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

[AutoComplete] 
		private long m_lngSavePatient_Disease(string p_strPatID,string p_strInPatientDate,string p_strDiseaseID,string p_strDeptID,clsHRPTableService p_objHRPServ)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSql = @"select a.inpatientid, a.inpatientdate, a.associateid,b.associateid, b.deptid, b.formname, b.templatesetid, b.associatename, b.type from patient_associate a
								inner join templateset_associate b on a.associateid = b.associateid
								where inpatientid = ? 
								and inpatientdate = ?
								and b.type = '0' and b.deptid = ?";
                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeptID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtExist, objDPArr);
                if (lngRes <= 0) return lngRes;
                if (dtExist.Rows.Count > 0)
                {

                    strSql = @"delete patient_associate
								where 
								inpatientid = ? 
								and inpatientdate = ?
								and associateid in
								(select b.associateid from patient_associate a
								inner join templateset_associate b on a.associateid = b.associateid
								where inpatientid = ? 
								and inpatientdate = ?
								and b.type = '0' and b.deptid = ?)";

                    objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                    objDPArr[0].Value = p_strPatID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].Value = p_strPatID;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[4].Value = p_strDeptID;

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff,objDPArr);
                }
                if (p_strDiseaseID.Trim() != "")
                    lngRes = m_lngAddPatient_Disease(p_strPatID, p_strInPatientDate, p_strDiseaseID);

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
        /// <summary>
        /// 获取一次住院全部作废记录
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetAllInactiveInfo(string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSql = @"select t.createdate,
       t.opendate,
       t.deactiveddate,
       e2.lastname_vchr createdusername,
       e3.lastname_vchr deactiveusername
  from inpatientcasehistory_history t, t_bse_employee e2, t_bse_employee e3
 where t.createuserid = e2.empno_chr
   and t.deactivedoperatorid = e3.empno_chr
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.status = 1
 order by t.opendate desc";
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

	}
}
