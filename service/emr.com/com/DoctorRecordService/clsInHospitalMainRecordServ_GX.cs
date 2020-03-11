using System;
using System.EnterpriseServices;
using System.Data;
using System.Xml;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PublicMiddleTier;
using com.digitalwave.Utility.SQLConvert;
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.InHospitalMainRecord
{
    /// <summary>
    /// 住院病案首页(广西)
    /// 使用了Oracle的序列seq_emr
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInHospitalMainRecordServ_GX : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
         

        #region SQL语句
        /// <summary>
        /// 获取首次打印时间
        /// </summary>
        private const string c_strGetFirstPrintDateSQL = @"select firstprintdate
																from t_emr_inhospitalmainrecord_gx
															where status = 0
																and registerid_chr = ?";

        /// <summary>
        /// 设置首次打印时间
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_inhospitalmainrecord_gx
																set firstprintdate = ?
															where registerid_chr=?
																and firstprintdate is null
																and status = 0";

        #region 添加记录
        /// <summary>
        /// 添加记录到主表T_EMR_INHOSPITALMAINRECORD_GX
        /// </summary>
        private const string c_strAddNewMainRecordSQL = @"insert into t_emr_inhospitalmainrecord_gx (inpatientid,inpatientdate,
		opendate,createuserid,status,diagnosisxml,inhospitaldiagnosisxml,maindiagnosisxml,complicationxml,infectiondiagnosisxml,
		pathologydiagnosisxml,icd_10ofmainxml,icd_10ofinfectionxml,icd_10ofcomplicationxml,icd_10ofdiagnosisxml,icd_10ofinhospitaldiaxml,
		icd_10ofpathologydiaxml,statcodeofmainxml,statcodeofinfectionxml,statcodeofpathologydiaxml,statcodeofdiagnosisxml,
		statcodeofinhospitaldiaxml,statcodeofcomplicationxml,scachesourcexml,sensitivexml,neonatedisease1xml,neonatedisease2xml,
		neonatedisease3xml,neonatedisease4xml,salvetimesxml,salvesuccessxml,remindtermxml,rbcxml,pltxml,plasmxml,wholebloodxml,
		otherbloodxml,registerid_chr,emr_seq,othermainconditionxml,othercomplicationxml,otherinfectioncondictionxml,otherpathologydiagnosisxml,submit_int) 
		values (?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,
				?,?,?,?)";

        /// <summary>
        /// 添加记录到子表T_EMR_INHOSPITALMAINREC_GXCON
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_inhospitalmainrec_gxcon (inpatientid,inpatientdate,
		opendate,lastmodifydate,lastmodifyuserid,status,condictionwhenin,confirmdiagnosisdate,diagnosis,inhospitaldiagnosis,maindiagnosis,
		complication,infectiondiagnosis,pathologydiagnosis,icd_10ofmain,icd_10ofinfection,icd_10ofcomplication,icd_10ofdiagnosis,
		icd_10ofinhospitaldia,icd_10ofpathologydia,statcodeofmain,statcodeofinfection,statcodeofpathologydia,statcodeofdiagnosis,
		statcodeofinhospitaldia,statcodeofcomplication,mainconditionseq,complicationseq,infectioncondictionseq,pathologydiagnosisseq,
		scachesource,new5disease,secondleveltransfer,sensitive,hbsag,hcv_ab,hiv_ab,neonatedisease1,neonatedisease2,neonatedisease3,
		neonatedisease4,salvetimes,salvesuccess,hasremind,remindterm,accordwithouthospital,accordinwithout,accordbfoprwithaf,
		accordclinicwithpathology,accordclinicwithradiate,accorddeathwithbodycheck,firstcase,modelcase,quality,antibacterial,
		pathogeny,pathogenyresult,bloodtransactoin,transfusionsaction,ctcheck,mricheck,bloodtype,bloodrh,rbc,plt,plasm,wholeblood,
		otherblood,deptdirectordt,dt,inhospitaldoc,outhospitaldoc,directordt,subdirectordt,attendinforadvancesstudydt,graduatestudentintern,
		intern,totalamt,bedamt,nurseamt,wmamt,cmfinishedamt,cmsemifinishedamt,radiationamt,assayamt,o2amt,bloodamt,treatmentamt,
		operationamt,checkamt,anaethesiaamt,deliverychildamt,babyamt,accompanyamt,otheramt,registerid_chr,neatenname,codingname,
		inputmachinename,statisticname,emr_seq,paytype,othermaincondition,othercomplication,otherinfectioncondiction,
		otherpathologydiagnosis,xraycheck,statcodeofscachesource,icd_10ofscachesource,statcodeofneonatedisease1,statcodeofneonatedisease2,
        statcodeofneonatedisease3,statcodeofneonatedisease4,icd_10ofneonatedisease1,icd_10ofneonatedisease2,icd_10ofneonatedisease3,
        icd_10ofneonatedisease4,remindtermtype,catalog_date,advancedstudiesdtname,deptdirectordtname,dtname,inhospitaldocname,outhospitaldocname,
        directordtname,subdirectordtname) 
		values (?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?)";

        /// <summary>
        /// 添加记录入院诊断T_EMR_INHOSPITALMAINREC_GXID
        /// </summary>
        private const string c_strAddNewRecordInDiagnoseSQL = @"insert into t_emr_inhospitalmainrec_gxid (inpatientid,inpatientdate,
		opendate,lastmodifydate,lastmodifyuserid,status,diagnosisdesc,statcode,icd10,registerid_chr,seqid,emr_seq) 
		values (?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 添加记录到其他诊断T_EMR_INHOSPITALMAINREC_GXOD
        /// </summary>
        private const string c_strAddNewRecordOtherDiagnoseSQL = @"insert into t_emr_inhospitalmainrec_gxod (inpatientid,inpatientdate,
		opendate,lastmodifydate,lastmodifyuserid,status,diagnosisdesc,conditionseq,statcode,icd10,registerid_chr,seqid,emr_seq,othercondition) 
		values (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 添加记录到手术T_EMR_INHOSPITALMAINREC_GXOP
        /// </summary>
        private const string c_strAddNewRecordOperationSQL = @"insert into t_emr_inhospitalmainrec_gxop (inpatientid,inpatientdate,
		opendate,lastmodifydate,lastmodifyuserid,status,seqid,operationdate,operationname,operator,assistant1,assistant2,aanaesthesiamodeid,
		cutlevel,anaesthetist,operationaanaesthesiamodename,registerid_chr,emr_seq,operationid) 
		values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
        #endregion

        #region 修改记录，只修改主表、子表，其它表新插入数据，并将旧有数据的status置为2
        /// <summary>
        /// 修改记录到主表T_EMR_INHOSPITALMAINRECORD_GX
        /// </summary>
        private const string c_strModifyMainRecordSQL = @"update t_emr_inhospitalmainrecord_gx 
		set diagnosisxml=?,inhospitaldiagnosisxml=?,maindiagnosisxml=?,complicationxml=?,infectiondiagnosisxml=?,pathologydiagnosisxml=?,
		icd_10ofmainxml=?,icd_10ofinfectionxml=?,icd_10ofcomplicationxml=?,icd_10ofdiagnosisxml=?,icd_10ofinhospitaldiaxml=?,
		icd_10ofpathologydiaxml=?,statcodeofmainxml=?,statcodeofinfectionxml=?,statcodeofpathologydiaxml=?,statcodeofdiagnosisxml=?,
		statcodeofinhospitaldiaxml=?,statcodeofcomplicationxml=?,scachesourcexml=?,sensitivexml=?,neonatedisease1xml=?,neonatedisease2xml=?,
		neonatedisease3xml=?,neonatedisease4xml=?,salvetimesxml=?,salvesuccessxml=?,remindtermxml=?,rbcxml=?,pltxml=?,plasmxml=?,wholebloodxml=?,
		otherbloodxml=?,othermainconditionxml=?,othercomplicationxml=?,otherinfectioncondictionxml=?,otherpathologydiagnosisxml=?,submit_int=?  where emr_seq=?";

        /// <summary>
        /// 修改记录到子表T_EMR_INHOSPITALMAINREC_GXCON
        /// </summary>
//        private const string c_strModifyRecordContentSQL = @"Update T_EMR_INHOSPITALMAINREC_GXCON
//		set CONDICTIONWHENIN=?,CONFIRMDIAGNOSISDATE=?,DIAGNOSIS=?,INHOSPITALDIAGNOSIS=?,MAINDIAGNOSIS=?,
//		COMPLICATION=?,INFECTIONDIAGNOSIS=?,PATHOLOGYDIAGNOSIS=?,ICD_10OFMAIN=?,ICD_10OFINFECTION=?,ICD_10OFCOMPLICATION=?,ICD_10OFDIAGNOSIS=?,
//		ICD_10OFINHOSPITALDIA=?,ICD_10OFPATHOLOGYDIA=?,STATCODEOFMAIN=?,STATCODEOFINFECTION=?,STATCODEOFPATHOLOGYDIA=?,STATCODEOFDIAGNOSIS=?,
//		STATCODEOFINHOSPITALDIA=?,STATCODEOFCOMPLICATION=?,MAINCONDITIONSEQ=?,COMPLICATIONSEQ=?,INFECTIONCONDICTIONSEQ=?,PATHOLOGYDIAGNOSISSEQ=?,
//		SCACHESOURCE=?,NEW5DISEASE=?,SECONDLEVELTRANSFER=?,SENSITIVE=?,HBSAG=?,HCV_AB=?,HIV_AB=?,NEONATEDISEASE1=?,NEONATEDISEASE2=?,NEONATEDISEASE3=?,
//		NEONATEDISEASE4=?,SALVETIMES=?,SALVESUCCESS=?,HASREMIND=?,REMINDTERM=?,ACCORDWITHOUTHOSPITAL=?,ACCORDINWITHOUT=?,ACCORDBFOPRWITHAF=?,
//		ACCORDCLINICWITHPATHOLOGY=?,ACCORDCLINICWITHRADIATE=?,ACCORDDEATHWITHBODYCHECK=?,FIRSTCASE=?,MODELCASE=?,QUALITY=?,ANTIBACTERIAL=?,
//		PATHOGENY=?,PATHOGENYRESULT=?,BLOODTRANSACTOIN=?,TRANSFUSIONSACTION=?,CTCHECK=?,MRICHECK=?,BLOODTYPE=?,BLOODRH=?,RBC=?,PLT=?,PLASM=?,WHOLEBLOOD=?,
//		OTHERBLOOD=?,DEPTDIRECTORDT=?,DT=?,INHOSPITALDOC=?,OUTHOSPITALDOC=?,DIRECTORDT=?,SUBDIRECTORDT=?,ATTENDINFORADVANCESSTUDYDT=?,GRADUATESTUDENTINTERN=?,
//		INTERN=?,TOTALAMT=?,BEDAMT=?,NURSEAMT=?,WMAMT=?,CMFINISHEDAMT=?,CMSEMIFINISHEDAMT=?,RADIATIONAMT=?,ASSAYAMT=?,O2AMT=?,BLOODAMT=?,TREATMENTAMT=?,
//		OPERATIONAMT=?,CHECKAMT=?,ANAETHESIAAMT=?,DELIVERYCHILDAMT=?,BABYAMT=?,ACCOMPANYAMT=?,OTHERAMT=?,NEATENNAME=?,CODINGNAME=?,
//		INPUTMACHINENAME=?,STATISTICNAME=?,PAYTYPE=?,OTHERMAINCONDITION=?,OTHERCOMPLICATION=?,OTHERINFECTIONCONDICTION=?,OTHERPATHOLOGYDIAGNOSIS=?,
//		LASTMODIFYDATE=?,LASTMODIFYUSERID=?,XRAYCHECK=?,STATCODEOFSCACHESOURCE=?,ICD_10OFSCACHESOURCE=?,STATCODEOFNEONATEDISEASE1=?,STATCODEOFNEONATEDISEASE2=?,
//        STATCODEOFNEONATEDISEASE3=?,STATCODEOFNEONATEDISEASE4=?,ICD_10OFNEONATEDISEASE1=?,ICD_10OFNEONATEDISEASE2=?,ICD_10OFNEONATEDISEASE3=?,
//        ICD_10OFNEONATEDISEASE4=?,REMINDTERMTYPE=?,CATALOG_DATE=? where EMR_SEQ=?";
        private const string c_strModifyRecordContentSQL = @"update t_emr_inhospitalmainrec_gxcon 
																	set status=2 where emr_seq=? and status=0";

        /// <summary>
        /// 修改状态到入院诊断T_EMR_INHOSPITALMAINREC_GXID
        /// </summary>
        private const string c_strModifyRecordInDiagnoseSQL = @"update t_emr_inhospitalmainrec_gxid 
																	set status=2 where emr_seq=? and status=0";

        /// <summary>
        /// 修改状态到其他诊断T_EMR_INHOSPITALMAINREC_GXOD
        /// </summary>
        private const string c_strModifyRecordOtherDiagnoseSQL = @"update t_emr_inhospitalmainrec_gxod 
																	set status=2 where emr_seq=? and status=0";

        /// <summary>
        /// 修改状态到手术信息T_EMR_INHOSPITALMAINREC_GXOP
        /// </summary>
        private const string c_strModifyRecordOperationSQL = @"update t_emr_inhospitalmainrec_gxop 
																set status=2 where emr_seq=? and status=0";
        #endregion

        /// <summary>
        /// 获取创建时间
        /// </summary>
        private const string c_strGetOpenDate = @"select opendate
													from t_emr_inhospitalmainrecord_gx
												where status = 0
													and registerid_chr = ?
                                                    order by opendate";

        /// <summary>
        /// 获取最后修改人及时间
        /// </summary>
        private const string c_strGetMaxModifyInfo = @"select lastmodifydate,lastmodifyuserid
															from t_emr_inhospitalmainrec_gxcon
														where status = 0
															and emr_seq = ?";

        #region 获取主表记录
        /// <summary>
        /// 获取主表的有效记录
        /// </summary>
        private const string c_strGetMainInfo = @"select inpatientid,
       inpatientdate,
       opendate,
       createuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       firstprintdate,
       diagnosisxml,
       inhospitaldiagnosisxml,
       maindiagnosisxml,
       complicationxml,
       infectiondiagnosisxml,
       pathologydiagnosisxml,
       icd_10ofmainxml,
       icd_10ofinfectionxml,
       icd_10ofcomplicationxml,
       icd_10ofdiagnosisxml,
       icd_10ofinhospitaldiaxml,
       icd_10ofpathologydiaxml,
       statcodeofmainxml,
       statcodeofinfectionxml,
       statcodeofpathologydiaxml,
       statcodeofdiagnosisxml,
       statcodeofinhospitaldiaxml,
       statcodeofcomplicationxml,
       scachesourcexml,
       sensitivexml,
       neonatedisease1xml,
       neonatedisease2xml,
       neonatedisease3xml,
       neonatedisease4xml,
       salvetimesxml,
       salvesuccessxml,
       remindtermxml,
       rbcxml,
       pltxml,
       plasmxml,
       wholebloodxml,
       otherbloodxml,
       registerid_chr,
       emr_seq,
       othermainconditionxml,
       othercomplicationxml,
       otherinfectioncondictionxml,
       otherpathologydiagnosisxml,
       submit_int
  from t_emr_inhospitalmainrecord_gx
 where status = 0
   and registerid_chr = ?
   and opendate = ?";

        /// <summary>
        /// 获取主表的已提交或未提交但已超过提交时限(保存记录时更改状态为已提交)记录
        /// </summary>
        private const string c_strGetMainSubmitInfo = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.firstprintdate,
       a.diagnosisxml,
       a.inhospitaldiagnosisxml,
       a.maindiagnosisxml,
       a.complicationxml,
       a.infectiondiagnosisxml,
       a.pathologydiagnosisxml,
       a.icd_10ofmainxml,
       a.icd_10ofinfectionxml,
       a.icd_10ofcomplicationxml,
       a.icd_10ofdiagnosisxml,
       a.icd_10ofinhospitaldiaxml,
       a.icd_10ofpathologydiaxml,
       a.statcodeofmainxml,
       a.statcodeofinfectionxml,
       a.statcodeofpathologydiaxml,
       a.statcodeofdiagnosisxml,
       a.statcodeofinhospitaldiaxml,
       a.statcodeofcomplicationxml,
       a.scachesourcexml,
       a.sensitivexml,
       a.neonatedisease1xml,
       a.neonatedisease2xml,
       a.neonatedisease3xml,
       a.neonatedisease4xml,
       a.salvetimesxml,
       a.salvesuccessxml,
       a.remindtermxml,
       a.rbcxml,
       a.pltxml,
       a.plasmxml,
       a.wholebloodxml,
       a.otherbloodxml,
       a.registerid_chr,
       a.emr_seq,
       a.othermainconditionxml,
       a.othercomplicationxml,
       a.otherinfectioncondictionxml,
       a.otherpathologydiagnosisxml,
       a.submit_int
  from t_emr_inhospitalmainrecord_gx a
 where a.status = 0
   and a.registerid_chr = ?
   and a.opendate = ?
   and (a.submit_int = 1 or
       (a.submit_int = 0 and
       a.registerid_chr in
       (select b.registerid_chr
            from t_opr_bih_leave b
           where sysdate - b.modify_dat >
                 (select t.setstatus_int / 24
                    from t_sys_setting t
                   where t.setid_chr = '3006'))))";

        /// <summary>
        /// 获取主表的已删除记录
        /// </summary>
        private const string c_strGetDelMainInfo = @"select inpatientid,
       inpatientdate,
       opendate,
       createuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       firstprintdate,
       diagnosisxml,
       inhospitaldiagnosisxml,
       maindiagnosisxml,
       complicationxml,
       infectiondiagnosisxml,
       pathologydiagnosisxml,
       icd_10ofmainxml,
       icd_10ofinfectionxml,
       icd_10ofcomplicationxml,
       icd_10ofdiagnosisxml,
       icd_10ofinhospitaldiaxml,
       icd_10ofpathologydiaxml,
       statcodeofmainxml,
       statcodeofinfectionxml,
       statcodeofpathologydiaxml,
       statcodeofdiagnosisxml,
       statcodeofinhospitaldiaxml,
       statcodeofcomplicationxml,
       scachesourcexml,
       sensitivexml,
       neonatedisease1xml,
       neonatedisease2xml,
       neonatedisease3xml,
       neonatedisease4xml,
       salvetimesxml,
       salvesuccessxml,
       remindtermxml,
       rbcxml,
       pltxml,
       plasmxml,
       wholebloodxml,
       otherbloodxml,
       registerid_chr,
       emr_seq,
       othermainconditionxml,
       othercomplicationxml,
       otherinfectioncondictionxml,
       otherpathologydiagnosisxml,
       submit_int
  from t_emr_inhospitalmainrecord_gx
 where status = 1
   and registerid_chr = ?
   and opendate = ?";
        #endregion

        #region 获取子表记录
        /// <summary>
        /// 获取子表的有效记录
        /// </summary>
        private const string c_strGetContentInfo = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.lastmodifydate,
       t.lastmodifyuserid,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.status,
       t.condictionwhenin,
       t.confirmdiagnosisdate,
       t.diagnosis,
       t.inhospitaldiagnosis,
       t.maindiagnosis,
       t.complication,
       t.infectiondiagnosis,
       t.pathologydiagnosis,
       t.icd_10ofmain,
       t.icd_10ofinfection,
       t.icd_10ofcomplication,
       t.icd_10ofdiagnosis,
       t.icd_10ofinhospitaldia,
       t.icd_10ofpathologydia,
       t.statcodeofmain,
       t.statcodeofinfection,
       t.statcodeofpathologydia,
       t.statcodeofdiagnosis,
       t.statcodeofinhospitaldia,
       t.statcodeofcomplication,
       t.mainconditionseq,
       t.complicationseq,
       t.infectioncondictionseq,
       t.pathologydiagnosisseq,
       t.scachesource,
       t.new5disease,
       t.secondleveltransfer,
       t.sensitive,
       t.hbsag,
       t.hcv_ab,
       t.hiv_ab,
       t.neonatedisease1,
       t.neonatedisease2,
       t.neonatedisease3,
       t.neonatedisease4,
       t.salvetimes,
       t.salvesuccess,
       t.hasremind,
       t.remindterm,
       t.accordwithouthospital,
       t.accordinwithout,
       t.accordbfoprwithaf,
       t.accordclinicwithpathology,
       t.accordclinicwithradiate,
       t.accorddeathwithbodycheck,
       t.firstcase,
       t.modelcase,
       t.quality,
       t.antibacterial,
       t.pathogeny,
       t.pathogenyresult,
       t.bloodtransactoin,
       t.transfusionsaction,
       t.ctcheck,
       t.mricheck,
       t.bloodtype,
       t.bloodrh,
       t.rbc,
       t.plt,
       t.plasm,
       t.wholeblood,
       t.otherblood,
       t.deptdirectordt,
       t.dt,
       t.inhospitaldoc,
       t.outhospitaldoc,
       t.directordt,
       t.subdirectordt,
       t.attendinforadvancesstudydt,
       t.graduatestudentintern,
       t.intern,
       t.totalamt,
       t.bedamt,
       t.nurseamt,
       t.wmamt,
       t.cmfinishedamt,
       t.cmsemifinishedamt,
       t.radiationamt,
       t.assayamt,
       t.o2amt,
       t.bloodamt,
       t.treatmentamt,
       t.operationamt,
       t.checkamt,
       t.anaethesiaamt,
       t.deliverychildamt,
       t.babyamt,
       t.accompanyamt,
       t.otheramt,
       t.registerid_chr,
       t.emr_seq,
       t.paytype,
       t.othermaincondition,
       t.othercomplication,
       t.otherinfectioncondiction,
       t.otherpathologydiagnosis,
       t.neatenname,
       t.codingname,
       t.inputmachinename,
       t.statisticname,
       t.xraycheck,
       t.statcodeofscachesource,
       t.icd_10ofscachesource,
       t.statcodeofneonatedisease1,
       t.statcodeofneonatedisease2,
       t.statcodeofneonatedisease3,
       t.statcodeofneonatedisease4,
       t.icd_10ofneonatedisease1,
       t.icd_10ofneonatedisease2,
       t.icd_10ofneonatedisease3,
       t.icd_10ofneonatedisease4,
       t.remindtermtype,
       t.catalog_date,
       t.advancedstudiesdtname,
       t.deptdirectordtname,
       t.dtname,
       t.inhospitaldocname,
       t.outhospitaldocname,
       t.directordtname,
       t.subdirectordtname
  from t_emr_inhospitalmainrec_gxcon t
 where t.status = 0
   and t.emr_seq = ?";

        /// <summary>
        /// 获取已删除的子表记录
        /// </summary>
        private const string c_strGetDelContentInfo = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.lastmodifydate,
       t.lastmodifyuserid,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.status,
       t.condictionwhenin,
       t.confirmdiagnosisdate,
       t.diagnosis,
       t.inhospitaldiagnosis,
       t.maindiagnosis,
       t.complication,
       t.infectiondiagnosis,
       t.pathologydiagnosis,
       t.icd_10ofmain,
       t.icd_10ofinfection,
       t.icd_10ofcomplication,
       t.icd_10ofdiagnosis,
       t.icd_10ofinhospitaldia,
       t.icd_10ofpathologydia,
       t.statcodeofmain,
       t.statcodeofinfection,
       t.statcodeofpathologydia,
       t.statcodeofdiagnosis,
       t.statcodeofinhospitaldia,
       t.statcodeofcomplication,
       t.mainconditionseq,
       t.complicationseq,
       t.infectioncondictionseq,
       t.pathologydiagnosisseq,
       t.scachesource,
       t.new5disease,
       t.secondleveltransfer,
       t.sensitive,
       t.hbsag,
       t.hcv_ab,
       t.hiv_ab,
       t.neonatedisease1,
       t.neonatedisease2,
       t.neonatedisease3,
       t.neonatedisease4,
       t.salvetimes,
       t.salvesuccess,
       t.hasremind,
       t.remindterm,
       t.accordwithouthospital,
       t.accordinwithout,
       t.accordbfoprwithaf,
       t.accordclinicwithpathology,
       t.accordclinicwithradiate,
       t.accorddeathwithbodycheck,
       t.firstcase,
       t.modelcase,
       t.quality,
       t.antibacterial,
       t.pathogeny,
       t.pathogenyresult,
       t.bloodtransactoin,
       t.transfusionsaction,
       t.ctcheck,
       t.mricheck,
       t.bloodtype,
       t.bloodrh,
       t.rbc,
       t.plt,
       t.plasm,
       t.wholeblood,
       t.otherblood,
       t.deptdirectordt,
       t.dt,
       t.inhospitaldoc,
       t.outhospitaldoc,
       t.directordt,
       t.subdirectordt,
       t.attendinforadvancesstudydt,
       t.graduatestudentintern,
       t.intern,
       t.totalamt,
       t.bedamt,
       t.nurseamt,
       t.wmamt,
       t.cmfinishedamt,
       t.cmsemifinishedamt,
       t.radiationamt,
       t.assayamt,
       t.o2amt,
       t.bloodamt,
       t.treatmentamt,
       t.operationamt,
       t.checkamt,
       t.anaethesiaamt,
       t.deliverychildamt,
       t.babyamt,
       t.accompanyamt,
       t.otheramt,
       t.registerid_chr,
       t.emr_seq,
       t.paytype,
       t.othermaincondition,
       t.othercomplication,
       t.otherinfectioncondiction,
       t.otherpathologydiagnosis,
       t.neatenname,
       t.codingname,
       t.inputmachinename,
       t.statisticname,
       t.xraycheck,
       t.statcodeofscachesource,
       t.icd_10ofscachesource,
       t.statcodeofneonatedisease1,
       t.statcodeofneonatedisease2,
       t.statcodeofneonatedisease3,
       t.statcodeofneonatedisease4,
       t.icd_10ofneonatedisease1,
       t.icd_10ofneonatedisease2,
       t.icd_10ofneonatedisease3,
       t.icd_10ofneonatedisease4,
       t.remindtermtype,
       t.catalog_date,
       t.advancedstudiesdtname,
       t.deptdirectordtname,
       t.dtname,
       t.inhospitaldocname,
       t.outhospitaldocname,
       t.directordtname,
       t.subdirectordtname
  from t_emr_inhospitalmainrec_gxcon t
 where t.status = 1
   and t.emr_seq = ?";
        #endregion

        #region 获取入院诊断
        /// <summary>
        /// 获取有效的入院诊断
        /// </summary>
        private const string c_strGetInDiagnosisInfo = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosisdesc,
       statcode,
       icd10,
       registerid_chr,
       seqid,
       emr_seq
  from t_emr_inhospitalmainrec_gxid
 where status = 0
   and emr_seq = ?
 order by seqid";

        /// <summary>
        /// 获取已删除的入院诊断
        /// </summary>
        private const string c_strGetDelInDiagnosisInfo = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosisdesc,
       statcode,
       icd10,
       registerid_chr,
       seqid,
       emr_seq
  from t_emr_inhospitalmainrec_gxid
 where status = 1
   and emr_seq = ?
 order by seqid";
        #endregion

        #region 获取其他诊断
        /// <summary>
        /// 获取有效的其他诊断
        /// </summary>
        private const string c_strGetOtherDiagnosisInfo = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosisdesc,
       conditionseq,
       statcode,
       icd10,
       registerid_chr,
       seqid,
       emr_seq,
       othercondition
  from t_emr_inhospitalmainrec_gxod
 where status = 0
   and emr_seq = ?
 order by seqid";

        /// <summary>
        /// 获取已删除的其他诊断
        /// </summary>
        private const string c_strGetDelOtherDiagnosisInfo = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       diagnosisdesc,
       conditionseq,
       statcode,
       icd10,
       registerid_chr,
       seqid,
       emr_seq,
       othercondition
  from t_emr_inhospitalmainrec_gxod
 where status = 1
   and emr_seq = ?
 order by seqid";
        #endregion

        #region 获取手术信息
        /// <summary>
        /// 获取有效的手术信息
        /// </summary>
        private const string c_strGetOperationInfo = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.lastmodifydate,
       t.lastmodifyuserid,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.status,
       t.seqid,
       t.operationdate,
       t.operationname,
       t.operator,
       t.assistant1,
       t.assistant2,
       t.aanaesthesiamodeid,
       t.cutlevel,
       t.anaesthetist,
       t.operationaanaesthesiamodename,
       t.registerid_chr,
       t.emr_seq,
       t.operationid,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.operator
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.operator
           and rownum = 1) as operatorname,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.assistant1
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = t.assistant1
           and rownum = 1) as assistant1name,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.assistant2
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = t.assistant2
           and rownum = 1) as assistant2name,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.anaesthetist
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = t.anaesthetist
           and rownum = 1) as anaesthetistname
  from t_emr_inhospitalmainrec_gxop t
 where t.status = 0
   and t.emr_seq = ?
 order by operationdate";

        /// <summary>
        /// 获取已删除的手术信息
        /// </summary>
        private const string c_strGetDelOperationInfo = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.lastmodifydate,
       t.lastmodifyuserid,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.status,
       t.seqid,
       t.operationdate,
       t.operationname,
       t.operator,
       t.assistant1,
       t.assistant2,
       t.aanaesthesiamodeid,
       t.cutlevel,
       t.anaesthetist,
       t.operationaanaesthesiamodename,
       t.registerid_chr,
       t.emr_seq,
       t.operationid,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.operator
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.operator
           and rownum = 1) as operatorname,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.assistant1
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.assistant1
           and rownum = 1) as assistant1name,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.assistant2
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.assistant2
           and rownum = 1) as assistant2name,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_inhospitalmainrec_gxop i
                 where e.empno_chr = i.anaesthetist
                   and e.status_int <> -1
                   and i.status = 0
                   and i.emr_seq = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.anaesthetist
           and rownum = 1) as anaesthetistname
  from t_emr_inhospitalmainrec_gxop t
 where t.status = 1
   and t.emr_seq = ?
 order by operationdate";
        #endregion

        /// <summary>
        /// 获取最后的修改时间和修改人
        /// </summary>
        private const string c_strGetLastModifyDateAndUser = @"select t2.lastmodifydate, t2.lastmodifyuserid
																from t_emr_inhospitalmainrecord_gx t1, t_emr_inhospitalmainrec_gxcon t2
																where t1.status = 0
																and t2.status = 0
																and t1.emr_seq = t2.emr_seq
																and t1.registerid_chr = ?";

        #region 删除记录
        /// <summary>
        /// 删除主表记录
        /// </summary>
        private const string c_strDeleteMainRecord = @"update t_emr_inhospitalmainrecord_gx
													set deactiveddate = ?, deactivedoperatorid = ?, status = 1
													where emr_seq = ? and status=0";


        /// <summary>
        /// 删除子表记录
        /// </summary>
        private const string c_strDeleteContentSQL = @"update t_emr_inhospitalmainrec_gxcon 
															set deactiveddate = ?, deactivedoperatorid = ?, status=1 
															where emr_seq=? and status=0";

        /// <summary>
        /// 删除其他诊断记录
        /// </summary>
        private const string c_strDeleteInDiagnoseSQL = @"update t_emr_inhospitalmainrec_gxid 
																	set deactiveddate = ?, deactivedoperatorid = ?, status=1 
																	where emr_seq=? and status=0";

        /// <summary>
        /// 删除其他诊断记录
        /// </summary>
        private const string c_strDeleteOtherDiagnoseSQL = @"update t_emr_inhospitalmainrec_gxod 
																	set deactiveddate = ?, deactivedoperatorid = ?, status=1 
																	where emr_seq=? and status=0";

        /// <summary>
        /// 删除手术信息记录
        /// </summary>
        private const string c_strDeleteOperationSQL = @"update t_emr_inhospitalmainrec_gxop 
																set deactiveddate = ?, deactivedoperatorid = ?, status=1 
																where emr_seq=? and status=0";
        #endregion

        /// <summary>
        /// 获取最后删除时间,以及删除人
        /// </summary>
        private const string c_strGetDeactivedDateAndUser = @"select t.deactiveddate, t.deactivedoperatorid
																from t_emr_inhospitalmainrecord_gx t
																where t.registerid_chr = ?
																and t.status = 1
																and t.emr_seq = (select max(emr_seq)
																					from t_emr_inhospitalmainrecord_gx
																					where registerid_chr = t.registerid_chr)";

        /// <summary>
        /// 查找该表在该条件下是否有重复的记录
        /// </summary>
        private const string c_strGetRecordCount = @"select count(registerid_chr) recordcount
  from t_emr_inhospitalmainrecord_gx
 where registerid_chr = ?
   and status = 0";

        /// <summary>
        /// 从同步表中读取病人收费信息
        /// </summary>
        private const string c_strGetCost = @"select t.inpatientid, t.inpatientdate, t.fee_type, t.costs
  from t_emr_pat_costs t
 where inpatientid = ?
   and t.inpatientdate = ?";

        /// <summary>
        /// 从同步表中读取病人手术信息
        /// </summary>
        private const string c_strGetOpInfo = @"select t.inpatientid,
       t.inpatientdate,
       t.seqid,
       t.operationid,
       t.operationdate,
       t.operationname,
       t.operator,
       t.assistant1,
       t.assistant2,
       t.aanaesthesiamodeid,
       t.cutlevel,
       t.anaesthetist,
       t.operationaanaesthesiamodename,
       t.operationanaesthetistname,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_pat_operation o
                 where e.empno_chr = o.operator
                   and e.status_int <> -1
                    and o.inpatientid = ?
                    and o.inpatientdate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.operator
           and rownum = 1) as operatorname,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_pat_operation o
                 where e.empno_chr = o.assistant1
                   and e.status_int <> -1
                    and o.inpatientid = ?
                    and o.inpatientdate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.assistant1
           and rownum = 1) as assistant1name,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_pat_operation o
                 where e.empno_chr = o.assistant2
                   and e.status_int <> -1
                    and o.inpatientid = ?
                    and o.inpatientdate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.assistant2
           and rownum = 1) as assistant2name,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_pat_operation o
                 where e.empno_chr = o.anaesthetist
                   and e.status_int <> -1
                    and o.inpatientid = ?
                    and o.inpatientdate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.anaesthetist
           and rownum = 1) as anaesthetistname
  from t_emr_pat_operation t
 where t.inpatientid = ?
   and t.inpatientdate = ?
 order by t.seqid";

        private const string c_strSetCatalogDate = @"update t_emr_inhospitalmainrec_gxcon set catalog_date = ?
                                                   where seqid = ?";
        #endregion

        #region 获取首次打印时间
        /// <summary>
        /// 获取首次打印时间
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strRegisterID">流水号</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFirstPrintDate(
            string p_strRegisterID, out DateTime p_dtmFirstPrintDate)
        {
            p_dtmFirstPrintDate = DateTime.MinValue;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngGetFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == string.Empty)
                    return -1;

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strRegisterID.Trim();

                //查询第一次打印时间	
                System.Data.DataTable dtbResult = new System.Data.DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetFirstPrintDateSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0][0] != DBNull.Value)
                        p_dtmFirstPrintDate = Convert.ToDateTime(dtbResult.Rows[0][0]);
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

            }            //返回
            return lngRes;
        }
        #endregion

        #region 设置首次打印时间
        /// <summary>
        /// 设置首次打印时间
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strRegisterID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateFirstPrintDate(
            string p_strRegisterID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == string.Empty)
                    return -1;

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objLisAddItemRefArr[1].Value = p_strRegisterID.Trim();

                //更新第一次打印时间			
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objLisAddItemRefArr);

            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }            //返回
            return lngRes;
        }
        #endregion

        #region 添加新纪录
        /// <summary>
        /// 添加新纪录
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_objCollection">住院病案记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNew(
            clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            long lngRes  = 0;
            long m_lngEMR_SEQ = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_objCollection == null)
                    return -1;
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                #region 添加主表和子表
                if (p_objCollection.m_objMain == null || p_objCollection.m_objContent == null)
                    return -1;

                //获取序列号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);
                m_lngEMR_SEQ = lngSequence;
                //string strGetSeq = @"select seq_emr.nextval from dual";
                //DataTable dtbResult = new DataTable();
                //lngRes = objHRPServ.DoGetDataTable(strGetSeq, ref dtbResult);

                //if (lngRes  > 0 && dtbResult.Rows.Count > 0)
                //    m_lngEMR_SEQ = Convert.ToInt64(dtbResult.Rows[0][0]);

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(44, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_objCollection.m_objMain.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = p_objCollection.m_objMain.m_dtmInPatientDate;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = p_objCollection.m_objMain.m_dtmOpenDate;
                objLisAddItemRefArr[3].Value = p_objCollection.m_objMain.m_strCreateUserID;
                objLisAddItemRefArr[4].Value = 0;
                objLisAddItemRefArr[5].Value = p_objCollection.m_objMain.m_strDIAGNOSISXML;
                objLisAddItemRefArr[6].Value = p_objCollection.m_objMain.m_strINHOSPITALDIAGNOSISXML;
                objLisAddItemRefArr[7].Value = p_objCollection.m_objMain.m_strMAINDIAGNOSISXML;
                objLisAddItemRefArr[8].Value = p_objCollection.m_objMain.m_strCOMPLICATIONXML;
                objLisAddItemRefArr[9].Value = p_objCollection.m_objMain.m_strINFECTIONDIAGNOSISXML;
                objLisAddItemRefArr[10].Value = p_objCollection.m_objMain.m_strPATHOLOGYDIAGNOSISXML;
                objLisAddItemRefArr[11].Value = p_objCollection.m_objMain.m_strICD_10OFMAINXML;
                objLisAddItemRefArr[12].Value = p_objCollection.m_objMain.m_strICD_10OFINFECTIONXML;
                objLisAddItemRefArr[13].Value = p_objCollection.m_objMain.m_strICD_10OFCOMPLICATIONXML;
                objLisAddItemRefArr[14].Value = p_objCollection.m_objMain.m_strICD_10OFDIAGNOSISXML;
                objLisAddItemRefArr[15].Value = p_objCollection.m_objMain.m_strICD_10OFINHOSPITALDIAXML;
                objLisAddItemRefArr[16].Value = p_objCollection.m_objMain.m_strICD_10OFPATHOLOGYDIAXML;
                objLisAddItemRefArr[17].Value = p_objCollection.m_objMain.m_strSTATCODEOFMAINXML;
                objLisAddItemRefArr[18].Value = p_objCollection.m_objMain.m_strSTATCODEOFINFECTIONXML;
                objLisAddItemRefArr[19].Value = p_objCollection.m_objMain.m_strSTATCODEOFPATHOLOGYDIAXML;
                objLisAddItemRefArr[20].Value = p_objCollection.m_objMain.m_strSTATCODEOFDIAGNOSISXML;
                objLisAddItemRefArr[21].Value = p_objCollection.m_objMain.m_strSTATCODEOFINHOSPITALDIAXML;
                objLisAddItemRefArr[22].Value = p_objCollection.m_objMain.m_strSTATCODEOFCOMPLICATIONXML;
                objLisAddItemRefArr[23].Value = p_objCollection.m_objMain.m_strSCACHESOURCEXML;
                objLisAddItemRefArr[24].Value = p_objCollection.m_objMain.m_strSENSITIVEXML;
                objLisAddItemRefArr[25].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE1XML;
                objLisAddItemRefArr[26].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE2XML;
                objLisAddItemRefArr[27].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE3XML;
                objLisAddItemRefArr[28].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE4XML;
                objLisAddItemRefArr[29].Value = p_objCollection.m_objMain.m_strSALVETIMESXML;
                objLisAddItemRefArr[30].Value = p_objCollection.m_objMain.m_strSALVESUCCESSXML;
                objLisAddItemRefArr[31].Value = p_objCollection.m_objMain.m_strREMINDTERMXML;
                objLisAddItemRefArr[32].Value = p_objCollection.m_objMain.m_strRBCXML;
                objLisAddItemRefArr[33].Value = p_objCollection.m_objMain.m_strPLTXML;
                objLisAddItemRefArr[34].Value = p_objCollection.m_objMain.m_strPLASMXML;
                objLisAddItemRefArr[35].Value = p_objCollection.m_objMain.m_strWHOLEBLOODXML;
                objLisAddItemRefArr[36].Value = p_objCollection.m_objMain.m_strOTHERBLOODXML;
                objLisAddItemRefArr[37].Value = p_objCollection.m_objMain.m_strREGISTERID_CHR.Trim();
                objLisAddItemRefArr[38].Value = m_lngEMR_SEQ;
                objLisAddItemRefArr[39].Value = p_objCollection.m_objMain.m_strOTHERMAINCONDITIONXML;
                objLisAddItemRefArr[40].Value = p_objCollection.m_objMain.m_strOTHERCOMPLICATIONXML;
                objLisAddItemRefArr[41].Value = p_objCollection.m_objMain.m_strOTHERINFECTIONCONDICTIONXML;
                objLisAddItemRefArr[42].Value = p_objCollection.m_objMain.m_strOTHERPATHOLOGYDIAGNOSISXML;
                objLisAddItemRefArr[43].Value = p_objCollection.m_objMain.m_intSUBMIT_INT;

                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewMainRecordSQL, ref lngRecEff, objLisAddItemRefArr);
                if (lngRes  <= 0)
                    return -1;

                System.Data.IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(126, out objLisAddItemRefArr1);

                objLisAddItemRefArr1[0].Value = p_objCollection.m_objContent.m_strInPatientID;
                objLisAddItemRefArr1[1].DbType = DbType.DateTime;
                objLisAddItemRefArr1[1].Value = p_objCollection.m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr1[2].DbType = DbType.DateTime;
                objLisAddItemRefArr1[2].Value = p_objCollection.m_objContent.m_dtmOpenDate;
                objLisAddItemRefArr1[3].DbType = DbType.DateTime;
                objLisAddItemRefArr1[3].Value = p_objCollection.m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr1[4].Value = p_objCollection.m_objContent.m_strModifyUserID;
                objLisAddItemRefArr1[5].Value = 0;
                objLisAddItemRefArr1[6].Value = p_objCollection.m_objContent.m_intCONDICTIONWHENIN;
                objLisAddItemRefArr1[7].DbType = DbType.DateTime;
                objLisAddItemRefArr1[7].Value = p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE;
                objLisAddItemRefArr1[8].Value = p_objCollection.m_objContent.m_strDIAGNOSIS;
                objLisAddItemRefArr1[9].Value = p_objCollection.m_objContent.m_strINHOSPITALDIAGNOSIS;
                objLisAddItemRefArr1[10].Value = p_objCollection.m_objContent.m_strMAINDIAGNOSIS;
                objLisAddItemRefArr1[11].Value = p_objCollection.m_objContent.m_strCOMPLICATION;
                objLisAddItemRefArr1[12].Value = p_objCollection.m_objContent.m_strINFECTIONDIAGNOSIS;
                objLisAddItemRefArr1[13].Value = p_objCollection.m_objContent.m_strPATHOLOGYDIAGNOSIS;
                objLisAddItemRefArr1[14].Value = p_objCollection.m_objContent.m_strICD_10OFMAIN;
                objLisAddItemRefArr1[15].Value = p_objCollection.m_objContent.m_strICD_10OFINFECTION;
                objLisAddItemRefArr1[16].Value = p_objCollection.m_objContent.m_strICD_10OFCOMPLICATION;
                objLisAddItemRefArr1[17].Value = p_objCollection.m_objContent.m_strICD_10OFDIAGNOSIS;
                objLisAddItemRefArr1[18].Value = p_objCollection.m_objContent.m_strICD_10OFINHOSPITALDIA;
                objLisAddItemRefArr1[19].Value = p_objCollection.m_objContent.m_strICD_10OFPATHOLOGYDIA;
                objLisAddItemRefArr1[20].Value = p_objCollection.m_objContent.m_strSTATCODEOFMAIN;
                objLisAddItemRefArr1[21].Value = p_objCollection.m_objContent.m_strSTATCODEOFINFECTION;
                objLisAddItemRefArr1[22].Value = p_objCollection.m_objContent.m_strSTATCODEOFPATHOLOGYDIA;
                objLisAddItemRefArr1[23].Value = p_objCollection.m_objContent.m_strSTATCODEOFDIAGNOSIS;
                objLisAddItemRefArr1[24].Value = p_objCollection.m_objContent.m_strSTATCODEOFINHOSPITALDIA;
                objLisAddItemRefArr1[25].Value = p_objCollection.m_objContent.m_strSTATCODEOFCOMPLICATION;
                objLisAddItemRefArr1[26].Value = p_objCollection.m_objContent.m_intMAINCONDITIONSEQ;
                objLisAddItemRefArr1[27].Value = p_objCollection.m_objContent.m_intCOMPLICATIONSEQ;
                objLisAddItemRefArr1[28].Value = p_objCollection.m_objContent.m_intINFECTIONCONDICTIONSEQ;
                objLisAddItemRefArr1[29].Value = p_objCollection.m_objContent.m_intPATHOLOGYDIAGNOSISSEQ;
                objLisAddItemRefArr1[30].Value = p_objCollection.m_objContent.m_strSCACHESOURCE;
                objLisAddItemRefArr1[31].Value = p_objCollection.m_objContent.m_intNEW5DISEASE;
                objLisAddItemRefArr1[32].Value = p_objCollection.m_objContent.m_intSECONDLEVELTRANSFER;
                objLisAddItemRefArr1[33].Value = p_objCollection.m_objContent.m_strSENSITIVE;
                objLisAddItemRefArr1[34].Value = p_objCollection.m_objContent.m_intHBSAG;
                objLisAddItemRefArr1[35].Value = p_objCollection.m_objContent.m_intHCV_AB;
                objLisAddItemRefArr1[36].Value = p_objCollection.m_objContent.m_intHIV_AB;
                objLisAddItemRefArr1[37].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE1;
                objLisAddItemRefArr1[38].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE2;
                objLisAddItemRefArr1[39].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE3;
                objLisAddItemRefArr1[40].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE4;
                objLisAddItemRefArr1[41].Value = p_objCollection.m_objContent.m_intSALVETIMES;
                objLisAddItemRefArr1[42].Value = p_objCollection.m_objContent.m_intSALVESUCCESS;
                objLisAddItemRefArr1[43].Value = p_objCollection.m_objContent.m_intHASREMIND;
                objLisAddItemRefArr1[44].Value = p_objCollection.m_objContent.m_strREMINDTERM;
                objLisAddItemRefArr1[45].Value = p_objCollection.m_objContent.m_intACCORDWITHOUTHOSPITAL;
                objLisAddItemRefArr1[46].Value = p_objCollection.m_objContent.m_intACCORDINWITHOUT;
                objLisAddItemRefArr1[47].Value = p_objCollection.m_objContent.m_intACCORDBFOPRWITHAF;
                objLisAddItemRefArr1[48].Value = p_objCollection.m_objContent.m_intACCORDCLINICWITHPATHOLOGY;
                objLisAddItemRefArr1[49].Value = p_objCollection.m_objContent.m_intACCORDCLINICWITHRADIATE;
                objLisAddItemRefArr1[50].Value = p_objCollection.m_objContent.m_intACCORDDEATHWITHBODYCHECK;
                objLisAddItemRefArr1[51].Value = p_objCollection.m_objContent.m_intFIRSTCASE;
                objLisAddItemRefArr1[52].Value = p_objCollection.m_objContent.m_intMODELCASE;
                objLisAddItemRefArr1[53].Value = p_objCollection.m_objContent.m_intQUALITY;
                objLisAddItemRefArr1[54].Value = p_objCollection.m_objContent.m_intANTIBACTERIAL;
                objLisAddItemRefArr1[55].Value = p_objCollection.m_objContent.m_intPATHOGENY;
                objLisAddItemRefArr1[56].Value = p_objCollection.m_objContent.m_intPATHOGENYRESULT;
                objLisAddItemRefArr1[57].Value = p_objCollection.m_objContent.m_intBLOODTRANSACTOIN;
                objLisAddItemRefArr1[58].Value = p_objCollection.m_objContent.m_intTRANSFUSIONSACTION;
                objLisAddItemRefArr1[59].Value = p_objCollection.m_objContent.m_intCTCHECK;
                objLisAddItemRefArr1[60].Value = p_objCollection.m_objContent.m_intMRICHECK;
                objLisAddItemRefArr1[61].Value = p_objCollection.m_objContent.m_intBLOODTYPE;
                objLisAddItemRefArr1[62].Value = p_objCollection.m_objContent.m_intBLOODRH;
                objLisAddItemRefArr1[63].Value = p_objCollection.m_objContent.m_strRBC;
                objLisAddItemRefArr1[64].Value = p_objCollection.m_objContent.m_strPLT;
                objLisAddItemRefArr1[65].Value = p_objCollection.m_objContent.m_strPLASM;
                objLisAddItemRefArr1[66].Value = p_objCollection.m_objContent.m_strWHOLEBLOOD;
                objLisAddItemRefArr1[67].Value = p_objCollection.m_objContent.m_strOTHERBLOOD;
                objLisAddItemRefArr1[68].Value = p_objCollection.m_objContent.m_strDEPTDIRECTORDT;
                objLisAddItemRefArr1[69].Value = p_objCollection.m_objContent.m_strDT;
                objLisAddItemRefArr1[70].Value = p_objCollection.m_objContent.m_strINHOSPITALDOC;
                objLisAddItemRefArr1[71].Value = p_objCollection.m_objContent.m_strOUTHOSPITALDOC;
                objLisAddItemRefArr1[72].Value = p_objCollection.m_objContent.m_strDIRECTORDT;
                objLisAddItemRefArr1[73].Value = p_objCollection.m_objContent.m_strSUBDIRECTORDT;
                objLisAddItemRefArr1[74].Value = p_objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDT;
                objLisAddItemRefArr1[75].Value = p_objCollection.m_objContent.m_strGRADUATESTUDENTINTERNNAME;
                objLisAddItemRefArr1[76].Value = p_objCollection.m_objContent.m_strINTERNNAME;
                objLisAddItemRefArr1[77].Value = p_objCollection.m_objContent.m_dblTOTALAMT;
                objLisAddItemRefArr1[78].Value = p_objCollection.m_objContent.m_dblBEDAMT;
                objLisAddItemRefArr1[79].Value = p_objCollection.m_objContent.m_dblNURSEAMT;
                objLisAddItemRefArr1[80].Value = p_objCollection.m_objContent.m_dblWMAMT;
                objLisAddItemRefArr1[81].Value = p_objCollection.m_objContent.m_dblCMFINISHEDAMT;
                objLisAddItemRefArr1[82].Value = p_objCollection.m_objContent.m_dblCMSEMIFINISHEDAMT;
                objLisAddItemRefArr1[83].Value = p_objCollection.m_objContent.m_dblRADIATIONAMT;
                objLisAddItemRefArr1[84].Value = p_objCollection.m_objContent.m_dblASSAYAMT;
                objLisAddItemRefArr1[85].Value = p_objCollection.m_objContent.m_dblO2AMT;
                objLisAddItemRefArr1[86].Value = p_objCollection.m_objContent.m_dblBLOODAMT;
                objLisAddItemRefArr1[87].Value = p_objCollection.m_objContent.m_dblTREATMENTAMT;
                objLisAddItemRefArr1[88].Value = p_objCollection.m_objContent.m_dblOPERATIONAMT;
                objLisAddItemRefArr1[89].Value = p_objCollection.m_objContent.m_dblCHECKAMT;
                objLisAddItemRefArr1[90].Value = p_objCollection.m_objContent.m_dblANAETHESIAAMT;
                objLisAddItemRefArr1[91].Value = p_objCollection.m_objContent.m_dblDELIVERYCHILDAMT;
                objLisAddItemRefArr1[92].Value = p_objCollection.m_objContent.m_dblBABYAMT;
                objLisAddItemRefArr1[93].Value = p_objCollection.m_objContent.m_dblACCOMPANYAMT;
                objLisAddItemRefArr1[94].Value = p_objCollection.m_objContent.m_dblOTHERAMT;
                objLisAddItemRefArr1[95].Value = p_objCollection.m_objContent.m_strREGISTERID_CHR.Trim();
                objLisAddItemRefArr1[96].Value = p_objCollection.m_objContent.m_strNEATENNAME;
                objLisAddItemRefArr1[97].Value = p_objCollection.m_objContent.m_strCODINGNAME;
                objLisAddItemRefArr1[98].Value = p_objCollection.m_objContent.m_strINPUTMACHINENAME;
                objLisAddItemRefArr1[99].Value = p_objCollection.m_objContent.m_strSTATISTICNAME;
                objLisAddItemRefArr1[100].Value = m_lngEMR_SEQ;
                objLisAddItemRefArr1[101].Value = p_objCollection.m_objContent.m_strPAYTYPE;
                objLisAddItemRefArr1[102].Value = p_objCollection.m_objContent.m_strOTHERMAINCONDITION;
                objLisAddItemRefArr1[103].Value = p_objCollection.m_objContent.m_strOTHERCOMPLICATION;
                objLisAddItemRefArr1[104].Value = p_objCollection.m_objContent.m_strOTHERINFECTIONCONDICTION;
                objLisAddItemRefArr1[105].Value = p_objCollection.m_objContent.m_strOTHERPATHOLOGYDIAGNOSIS;
                objLisAddItemRefArr1[106].Value = p_objCollection.m_objContent.m_intXRAYCHECK;
                objLisAddItemRefArr1[107].Value = p_objCollection.m_objContent.m_strSTATCODEOFSCACHESOURCE;
                objLisAddItemRefArr1[108].Value = p_objCollection.m_objContent.m_strICD_10OFSCACHESOURCE;
                objLisAddItemRefArr1[109].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE1;
                objLisAddItemRefArr1[110].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE2;
                objLisAddItemRefArr1[111].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE3;
                objLisAddItemRefArr1[112].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE4;
                objLisAddItemRefArr1[113].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE1;
                objLisAddItemRefArr1[114].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE2;
                objLisAddItemRefArr1[115].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE3;
                objLisAddItemRefArr1[116].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE4;
                objLisAddItemRefArr1[117].Value = p_objCollection.m_objContent.m_intREMINDTERMTYPE;
                objLisAddItemRefArr1[118].DbType = DbType.DateTime;
                if (p_objCollection.m_objContent.m_dtmCATALOG_DATE == DateTime.MinValue)
                {
                    objLisAddItemRefArr1[118].Value = null;
                }
                else
                {
                    objLisAddItemRefArr1[118].Value = p_objCollection.m_objContent.m_dtmCATALOG_DATE;
                }
                objLisAddItemRefArr1[119].Value = p_objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDTNAME;
                objLisAddItemRefArr1[120].Value = p_objCollection.m_objContent.m_strDEPTDIRECTORDTNAME;
                objLisAddItemRefArr1[121].Value = p_objCollection.m_objContent.m_strDTNAME;
                objLisAddItemRefArr1[122].Value = p_objCollection.m_objContent.m_strINHOSPITALDOCNAME;
                objLisAddItemRefArr1[123].Value = p_objCollection.m_objContent.m_strOUTHOSPITALDOCNAME;
                objLisAddItemRefArr1[124].Value = p_objCollection.m_objContent.m_strDIRECTORDTNAME;
                objLisAddItemRefArr1[125].Value = p_objCollection.m_objContent.m_strSUBDIRECTORDTNAME;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngRecEff, objLisAddItemRefArr1);
                if (lngRes  <= 0)
                    return -1;
                #endregion

                #region 添加入院诊断
                if (p_objCollection.m_objInDiagnosisArr != null)
                {
                    System.Data.IDataParameter[] objLisAddItemRefArr4 = null;
                    for (int i = 0; i < p_objCollection.m_objInDiagnosisArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(12, out objLisAddItemRefArr4);

                        objLisAddItemRefArr4[0].Value = p_objCollection.m_objInDiagnosisArr[i].m_strInPatientID;
                        objLisAddItemRefArr4[1].DbType = DbType.DateTime;
                        objLisAddItemRefArr4[1].Value = p_objCollection.m_objInDiagnosisArr[i].m_dtmInPatientDate;
                        objLisAddItemRefArr4[2].DbType = DbType.DateTime;
                        objLisAddItemRefArr4[2].Value = p_objCollection.m_objInDiagnosisArr[i].m_dtmOpenDate;
                        objLisAddItemRefArr4[3].DbType = DbType.DateTime;
                        objLisAddItemRefArr4[3].Value = p_objCollection.m_objInDiagnosisArr[i].m_dtmModifyDate;
                        objLisAddItemRefArr4[4].Value = p_objCollection.m_objInDiagnosisArr[i].m_strModifyUserID;
                        objLisAddItemRefArr4[5].Value = 0;
                        objLisAddItemRefArr4[6].Value = p_objCollection.m_objInDiagnosisArr[i].m_strDIAGNOSISDESC;
                        objLisAddItemRefArr4[7].Value = p_objCollection.m_objInDiagnosisArr[i].m_strSTATCODE;
                        objLisAddItemRefArr4[8].Value = p_objCollection.m_objInDiagnosisArr[i].m_strICD10;
                        objLisAddItemRefArr4[9].Value = p_objCollection.m_objInDiagnosisArr[i].m_strREGISTERID_CHR.Trim();
                        objLisAddItemRefArr4[10].Value = p_objCollection.m_objInDiagnosisArr[i].m_strSEQID;
                        objLisAddItemRefArr4[11].Value = m_lngEMR_SEQ;

                        //往表增加记录
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordInDiagnoseSQL, ref lngRecEff, objLisAddItemRefArr4);
                        if (lngRes  <= 0)
                            return -1;
                    }
                }
                #endregion

                #region 添加其他诊断
                if (p_objCollection.m_objOtherDiagnosisArr != null)
                {
                    System.Data.IDataParameter[] objLisAddItemRefArr2 = null;
                    for (int i = 0; i < p_objCollection.m_objOtherDiagnosisArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(14, out objLisAddItemRefArr2);

                        objLisAddItemRefArr2[0].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strInPatientID;
                        objLisAddItemRefArr2[1].DbType = DbType.DateTime;
                        objLisAddItemRefArr2[1].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_dtmInPatientDate;
                        objLisAddItemRefArr2[2].DbType = DbType.DateTime;
                        objLisAddItemRefArr2[2].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_dtmOpenDate;
                        objLisAddItemRefArr2[3].DbType = DbType.DateTime;
                        objLisAddItemRefArr2[3].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_dtmModifyDate;
                        objLisAddItemRefArr2[4].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strModifyUserID;
                        objLisAddItemRefArr2[5].Value = 0;
                        objLisAddItemRefArr2[6].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strDIAGNOSISDESC;
                        objLisAddItemRefArr2[7].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_intCONDITIONSEQ;
                        objLisAddItemRefArr2[8].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strSTATCODE;
                        objLisAddItemRefArr2[9].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strICD10;
                        objLisAddItemRefArr2[10].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strREGISTERID_CHR.Trim();
                        objLisAddItemRefArr2[11].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strSEQID;
                        objLisAddItemRefArr2[12].Value = m_lngEMR_SEQ;
                        objLisAddItemRefArr2[13].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strOTHERCONDITION;

                        //往表增加记录
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordOtherDiagnoseSQL, ref lngRecEff, objLisAddItemRefArr2);
                        if (lngRes  <= 0)
                            return -1;
                    }
                }
                #endregion

                #region 添加手术信息
                if (p_objCollection.m_objOperationArr != null)
                {
                    System.Data.IDataParameter[] objLisAddItemRefArr3 = null;
                    for (int i = 0; i < p_objCollection.m_objOperationArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(19, out objLisAddItemRefArr3);

                        objLisAddItemRefArr3[0].Value = p_objCollection.m_objOperationArr[i].m_strInPatientID;
                        objLisAddItemRefArr3[1].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[1].Value = p_objCollection.m_objOperationArr[i].m_dtmInPatientDate;
                        objLisAddItemRefArr3[2].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[2].Value = p_objCollection.m_objOperationArr[i].m_dtmOpenDate;
                        objLisAddItemRefArr3[3].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[3].Value = p_objCollection.m_objOperationArr[i].m_dtmModifyDate;
                        objLisAddItemRefArr3[4].Value = p_objCollection.m_objOperationArr[i].m_strModifyUserID;
                        objLisAddItemRefArr3[5].Value = 0;
                        objLisAddItemRefArr3[6].Value = p_objCollection.m_objOperationArr[i].m_strSEQID;
                        objLisAddItemRefArr3[7].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[7].Value = p_objCollection.m_objOperationArr[i].m_dtmOPERATIONDATE;
                        objLisAddItemRefArr3[8].Value = p_objCollection.m_objOperationArr[i].m_strOPERATIONNAME;
                        if (p_objCollection.m_objOperationArr[i].m_strOPERATOR != null)
                            objLisAddItemRefArr3[9].Value = p_objCollection.m_objOperationArr[i].m_strOPERATOR.Trim();
                        else
                            objLisAddItemRefArr3[9].Value = "";
                        if (p_objCollection.m_objOperationArr[i].m_strASSISTANT1 != null)
                            objLisAddItemRefArr3[10].Value = p_objCollection.m_objOperationArr[i].m_strASSISTANT1.Trim();
                        else
                            objLisAddItemRefArr3[10].Value = "";
                        if (p_objCollection.m_objOperationArr[i].m_strASSISTANT2 != null)
                            objLisAddItemRefArr3[11].Value = p_objCollection.m_objOperationArr[i].m_strASSISTANT2.Trim();
                        else
                            objLisAddItemRefArr3[11].Value = "";
                        if (p_objCollection.m_objOperationArr[i].m_strAANAESTHESIAMODEID != null)
                            objLisAddItemRefArr3[12].Value = p_objCollection.m_objOperationArr[i].m_strAANAESTHESIAMODEID.Trim();
                        else
                            objLisAddItemRefArr3[12].Value = "";
                        objLisAddItemRefArr3[13].Value = p_objCollection.m_objOperationArr[i].m_strCUTLEVEL;
                        if (p_objCollection.m_objOperationArr[i].m_strANAESTHETIST != null)
                            objLisAddItemRefArr3[14].Value = p_objCollection.m_objOperationArr[i].m_strANAESTHETIST.Trim();
                        else
                            objLisAddItemRefArr3[14].Value = "";
                        objLisAddItemRefArr3[15].Value = p_objCollection.m_objOperationArr[i].m_strOPERATIONAANAESTHESIAMODENAME;
                        objLisAddItemRefArr3[16].Value = p_objCollection.m_objOperationArr[i].m_strREGISTERID_CHR.Trim();
                        objLisAddItemRefArr3[17].Value = m_lngEMR_SEQ;
                        objLisAddItemRefArr3[18].Value = p_objCollection.m_objOperationArr[i].m_strOPERATIONID;

                        //往表增加记录
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordOperationSQL, ref lngRecEff, objLisAddItemRefArr3);
                        if (lngRes  <= 0)
                            return -1;
                    }
                }
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

            }         //返回
            return lngRes ;
        }
        #endregion

        #region 修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_objCollection">住院病案记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModify(
            clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            long lngRes  = 0;
            long m_lngEMR_SEQ = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_objCollection == null)
                    return -1;
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngModify");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_objCollection.m_objMain == null || p_objCollection.m_objContent == null)
                    return -1;

                m_lngEMR_SEQ = p_objCollection.m_objMain.m_lngEMR_SEQ;

                #region 修改主表，子表
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(38, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_objCollection.m_objMain.m_strDIAGNOSISXML;
                objLisAddItemRefArr[1].Value = p_objCollection.m_objMain.m_strINHOSPITALDIAGNOSISXML;
                objLisAddItemRefArr[2].Value = p_objCollection.m_objMain.m_strMAINDIAGNOSISXML;
                objLisAddItemRefArr[3].Value = p_objCollection.m_objMain.m_strCOMPLICATIONXML;
                objLisAddItemRefArr[4].Value = p_objCollection.m_objMain.m_strINFECTIONDIAGNOSISXML;
                objLisAddItemRefArr[5].Value = p_objCollection.m_objMain.m_strPATHOLOGYDIAGNOSISXML;
                objLisAddItemRefArr[6].Value = p_objCollection.m_objMain.m_strICD_10OFMAINXML;
                objLisAddItemRefArr[7].Value = p_objCollection.m_objMain.m_strICD_10OFINFECTIONXML;
                objLisAddItemRefArr[8].Value = p_objCollection.m_objMain.m_strICD_10OFCOMPLICATIONXML;
                objLisAddItemRefArr[9].Value = p_objCollection.m_objMain.m_strICD_10OFDIAGNOSISXML;
                objLisAddItemRefArr[10].Value = p_objCollection.m_objMain.m_strICD_10OFINHOSPITALDIAXML;
                objLisAddItemRefArr[11].Value = p_objCollection.m_objMain.m_strICD_10OFPATHOLOGYDIAXML;
                objLisAddItemRefArr[12].Value = p_objCollection.m_objMain.m_strSTATCODEOFMAINXML;
                objLisAddItemRefArr[13].Value = p_objCollection.m_objMain.m_strSTATCODEOFINFECTIONXML;
                objLisAddItemRefArr[14].Value = p_objCollection.m_objMain.m_strSTATCODEOFPATHOLOGYDIAXML;
                objLisAddItemRefArr[15].Value = p_objCollection.m_objMain.m_strSTATCODEOFDIAGNOSISXML;
                objLisAddItemRefArr[16].Value = p_objCollection.m_objMain.m_strSTATCODEOFINHOSPITALDIAXML;
                objLisAddItemRefArr[17].Value = p_objCollection.m_objMain.m_strSTATCODEOFCOMPLICATIONXML;
                objLisAddItemRefArr[18].Value = p_objCollection.m_objMain.m_strSCACHESOURCEXML;
                objLisAddItemRefArr[19].Value = p_objCollection.m_objMain.m_strSENSITIVEXML;
                objLisAddItemRefArr[20].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE1XML;
                objLisAddItemRefArr[21].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE2XML;
                objLisAddItemRefArr[22].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE3XML;
                objLisAddItemRefArr[23].Value = p_objCollection.m_objMain.m_strNEONATEDISEASE4XML;
                objLisAddItemRefArr[24].Value = p_objCollection.m_objMain.m_strSALVETIMESXML;
                objLisAddItemRefArr[25].Value = p_objCollection.m_objMain.m_strSALVESUCCESSXML;
                objLisAddItemRefArr[26].Value = p_objCollection.m_objMain.m_strREMINDTERMXML;
                objLisAddItemRefArr[27].Value = p_objCollection.m_objMain.m_strRBCXML;
                objLisAddItemRefArr[28].Value = p_objCollection.m_objMain.m_strPLTXML;
                objLisAddItemRefArr[29].Value = p_objCollection.m_objMain.m_strPLASMXML;
                objLisAddItemRefArr[30].Value = p_objCollection.m_objMain.m_strWHOLEBLOODXML;
                objLisAddItemRefArr[31].Value = p_objCollection.m_objMain.m_strOTHERBLOODXML;
                objLisAddItemRefArr[32].Value = p_objCollection.m_objMain.m_strOTHERMAINCONDITIONXML;
                objLisAddItemRefArr[33].Value = p_objCollection.m_objMain.m_strOTHERCOMPLICATIONXML;
                objLisAddItemRefArr[34].Value = p_objCollection.m_objMain.m_strOTHERINFECTIONCONDICTIONXML;
                objLisAddItemRefArr[35].Value = p_objCollection.m_objMain.m_strOTHERPATHOLOGYDIAGNOSISXML;
                objLisAddItemRefArr[36].Value = p_objCollection.m_objMain.m_intSUBMIT_INT;
                objLisAddItemRefArr[37].Value = m_lngEMR_SEQ;

                long lngRecEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyMainRecordSQL, ref lngRecEff, objLisAddItemRefArr);
                if (lngRes <= 0)
                    return -1;

                //修改子表记录
                System.Data.IDataParameter[] objSeqConArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objSeqConArr);
                objSeqConArr[0].Value = m_lngEMR_SEQ;
                //将子表的旧记录状态置为2
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngRecEff, objSeqConArr);
                if (lngRes <= 0)
                    return -1;

                System.Data.IDataParameter[] objLisAddItemRefArr1 = null;
                objHRPServ.CreateDatabaseParameter(126, out objLisAddItemRefArr1);

                objLisAddItemRefArr1[0].Value = p_objCollection.m_objContent.m_strInPatientID;
                objLisAddItemRefArr1[1].DbType = DbType.DateTime;
                objLisAddItemRefArr1[1].Value = p_objCollection.m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr1[2].DbType = DbType.DateTime;
                objLisAddItemRefArr1[2].Value = p_objCollection.m_objContent.m_dtmOpenDate;
                objLisAddItemRefArr1[3].DbType = DbType.DateTime;
                objLisAddItemRefArr1[3].Value = p_objCollection.m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr1[4].Value = p_objCollection.m_objContent.m_strModifyUserID;
                objLisAddItemRefArr1[5].Value = 0;
                objLisAddItemRefArr1[6].Value = p_objCollection.m_objContent.m_intCONDICTIONWHENIN;
                objLisAddItemRefArr1[7].DbType = DbType.DateTime;
                objLisAddItemRefArr1[7].Value = p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE;
                objLisAddItemRefArr1[8].Value = p_objCollection.m_objContent.m_strDIAGNOSIS;
                objLisAddItemRefArr1[9].Value = p_objCollection.m_objContent.m_strINHOSPITALDIAGNOSIS;
                objLisAddItemRefArr1[10].Value = p_objCollection.m_objContent.m_strMAINDIAGNOSIS;
                objLisAddItemRefArr1[11].Value = p_objCollection.m_objContent.m_strCOMPLICATION;
                objLisAddItemRefArr1[12].Value = p_objCollection.m_objContent.m_strINFECTIONDIAGNOSIS;
                objLisAddItemRefArr1[13].Value = p_objCollection.m_objContent.m_strPATHOLOGYDIAGNOSIS;
                objLisAddItemRefArr1[14].Value = p_objCollection.m_objContent.m_strICD_10OFMAIN;
                objLisAddItemRefArr1[15].Value = p_objCollection.m_objContent.m_strICD_10OFINFECTION;
                objLisAddItemRefArr1[16].Value = p_objCollection.m_objContent.m_strICD_10OFCOMPLICATION;
                objLisAddItemRefArr1[17].Value = p_objCollection.m_objContent.m_strICD_10OFDIAGNOSIS;
                objLisAddItemRefArr1[18].Value = p_objCollection.m_objContent.m_strICD_10OFINHOSPITALDIA;
                objLisAddItemRefArr1[19].Value = p_objCollection.m_objContent.m_strICD_10OFPATHOLOGYDIA;
                objLisAddItemRefArr1[20].Value = p_objCollection.m_objContent.m_strSTATCODEOFMAIN;
                objLisAddItemRefArr1[21].Value = p_objCollection.m_objContent.m_strSTATCODEOFINFECTION;
                objLisAddItemRefArr1[22].Value = p_objCollection.m_objContent.m_strSTATCODEOFPATHOLOGYDIA;
                objLisAddItemRefArr1[23].Value = p_objCollection.m_objContent.m_strSTATCODEOFDIAGNOSIS;
                objLisAddItemRefArr1[24].Value = p_objCollection.m_objContent.m_strSTATCODEOFINHOSPITALDIA;
                objLisAddItemRefArr1[25].Value = p_objCollection.m_objContent.m_strSTATCODEOFCOMPLICATION;
                objLisAddItemRefArr1[26].Value = p_objCollection.m_objContent.m_intMAINCONDITIONSEQ;
                objLisAddItemRefArr1[27].Value = p_objCollection.m_objContent.m_intCOMPLICATIONSEQ;
                objLisAddItemRefArr1[28].Value = p_objCollection.m_objContent.m_intINFECTIONCONDICTIONSEQ;
                objLisAddItemRefArr1[29].Value = p_objCollection.m_objContent.m_intPATHOLOGYDIAGNOSISSEQ;
                objLisAddItemRefArr1[30].Value = p_objCollection.m_objContent.m_strSCACHESOURCE;
                objLisAddItemRefArr1[31].Value = p_objCollection.m_objContent.m_intNEW5DISEASE;
                objLisAddItemRefArr1[32].Value = p_objCollection.m_objContent.m_intSECONDLEVELTRANSFER;
                objLisAddItemRefArr1[33].Value = p_objCollection.m_objContent.m_strSENSITIVE;
                objLisAddItemRefArr1[34].Value = p_objCollection.m_objContent.m_intHBSAG;
                objLisAddItemRefArr1[35].Value = p_objCollection.m_objContent.m_intHCV_AB;
                objLisAddItemRefArr1[36].Value = p_objCollection.m_objContent.m_intHIV_AB;
                objLisAddItemRefArr1[37].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE1;
                objLisAddItemRefArr1[38].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE2;
                objLisAddItemRefArr1[39].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE3;
                objLisAddItemRefArr1[40].Value = p_objCollection.m_objContent.m_strNEONATEDISEASE4;
                objLisAddItemRefArr1[41].Value = p_objCollection.m_objContent.m_intSALVETIMES;
                objLisAddItemRefArr1[42].Value = p_objCollection.m_objContent.m_intSALVESUCCESS;
                objLisAddItemRefArr1[43].Value = p_objCollection.m_objContent.m_intHASREMIND;
                objLisAddItemRefArr1[44].Value = p_objCollection.m_objContent.m_strREMINDTERM;
                objLisAddItemRefArr1[45].Value = p_objCollection.m_objContent.m_intACCORDWITHOUTHOSPITAL;
                objLisAddItemRefArr1[46].Value = p_objCollection.m_objContent.m_intACCORDINWITHOUT;
                objLisAddItemRefArr1[47].Value = p_objCollection.m_objContent.m_intACCORDBFOPRWITHAF;
                objLisAddItemRefArr1[48].Value = p_objCollection.m_objContent.m_intACCORDCLINICWITHPATHOLOGY;
                objLisAddItemRefArr1[49].Value = p_objCollection.m_objContent.m_intACCORDCLINICWITHRADIATE;
                objLisAddItemRefArr1[50].Value = p_objCollection.m_objContent.m_intACCORDDEATHWITHBODYCHECK;
                objLisAddItemRefArr1[51].Value = p_objCollection.m_objContent.m_intFIRSTCASE;
                objLisAddItemRefArr1[52].Value = p_objCollection.m_objContent.m_intMODELCASE;
                objLisAddItemRefArr1[53].Value = p_objCollection.m_objContent.m_intQUALITY;
                objLisAddItemRefArr1[54].Value = p_objCollection.m_objContent.m_intANTIBACTERIAL;
                objLisAddItemRefArr1[55].Value = p_objCollection.m_objContent.m_intPATHOGENY;
                objLisAddItemRefArr1[56].Value = p_objCollection.m_objContent.m_intPATHOGENYRESULT;
                objLisAddItemRefArr1[57].Value = p_objCollection.m_objContent.m_intBLOODTRANSACTOIN;
                objLisAddItemRefArr1[58].Value = p_objCollection.m_objContent.m_intTRANSFUSIONSACTION;
                objLisAddItemRefArr1[59].Value = p_objCollection.m_objContent.m_intCTCHECK;
                objLisAddItemRefArr1[60].Value = p_objCollection.m_objContent.m_intMRICHECK;
                objLisAddItemRefArr1[61].Value = p_objCollection.m_objContent.m_intBLOODTYPE;
                objLisAddItemRefArr1[62].Value = p_objCollection.m_objContent.m_intBLOODRH;
                objLisAddItemRefArr1[63].Value = p_objCollection.m_objContent.m_strRBC;
                objLisAddItemRefArr1[64].Value = p_objCollection.m_objContent.m_strPLT;
                objLisAddItemRefArr1[65].Value = p_objCollection.m_objContent.m_strPLASM;
                objLisAddItemRefArr1[66].Value = p_objCollection.m_objContent.m_strWHOLEBLOOD;
                objLisAddItemRefArr1[67].Value = p_objCollection.m_objContent.m_strOTHERBLOOD;
                objLisAddItemRefArr1[68].Value = p_objCollection.m_objContent.m_strDEPTDIRECTORDT;
                objLisAddItemRefArr1[69].Value = p_objCollection.m_objContent.m_strDT;
                objLisAddItemRefArr1[70].Value = p_objCollection.m_objContent.m_strINHOSPITALDOC;
                objLisAddItemRefArr1[71].Value = p_objCollection.m_objContent.m_strOUTHOSPITALDOC;
                objLisAddItemRefArr1[72].Value = p_objCollection.m_objContent.m_strDIRECTORDT;
                objLisAddItemRefArr1[73].Value = p_objCollection.m_objContent.m_strSUBDIRECTORDT;
                objLisAddItemRefArr1[74].Value = p_objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDT;
                objLisAddItemRefArr1[75].Value = p_objCollection.m_objContent.m_strGRADUATESTUDENTINTERNNAME;
                objLisAddItemRefArr1[76].Value = p_objCollection.m_objContent.m_strINTERNNAME;
                objLisAddItemRefArr1[77].Value = p_objCollection.m_objContent.m_dblTOTALAMT;
                objLisAddItemRefArr1[78].Value = p_objCollection.m_objContent.m_dblBEDAMT;
                objLisAddItemRefArr1[79].Value = p_objCollection.m_objContent.m_dblNURSEAMT;
                objLisAddItemRefArr1[80].Value = p_objCollection.m_objContent.m_dblWMAMT;
                objLisAddItemRefArr1[81].Value = p_objCollection.m_objContent.m_dblCMFINISHEDAMT;
                objLisAddItemRefArr1[82].Value = p_objCollection.m_objContent.m_dblCMSEMIFINISHEDAMT;
                objLisAddItemRefArr1[83].Value = p_objCollection.m_objContent.m_dblRADIATIONAMT;
                objLisAddItemRefArr1[84].Value = p_objCollection.m_objContent.m_dblASSAYAMT;
                objLisAddItemRefArr1[85].Value = p_objCollection.m_objContent.m_dblO2AMT;
                objLisAddItemRefArr1[86].Value = p_objCollection.m_objContent.m_dblBLOODAMT;
                objLisAddItemRefArr1[87].Value = p_objCollection.m_objContent.m_dblTREATMENTAMT;
                objLisAddItemRefArr1[88].Value = p_objCollection.m_objContent.m_dblOPERATIONAMT;
                objLisAddItemRefArr1[89].Value = p_objCollection.m_objContent.m_dblCHECKAMT;
                objLisAddItemRefArr1[90].Value = p_objCollection.m_objContent.m_dblANAETHESIAAMT;
                objLisAddItemRefArr1[91].Value = p_objCollection.m_objContent.m_dblDELIVERYCHILDAMT;
                objLisAddItemRefArr1[92].Value = p_objCollection.m_objContent.m_dblBABYAMT;
                objLisAddItemRefArr1[93].Value = p_objCollection.m_objContent.m_dblACCOMPANYAMT;
                objLisAddItemRefArr1[94].Value = p_objCollection.m_objContent.m_dblOTHERAMT;
                objLisAddItemRefArr1[95].Value = p_objCollection.m_objContent.m_strREGISTERID_CHR.Trim();
                objLisAddItemRefArr1[96].Value = p_objCollection.m_objContent.m_strNEATENNAME;
                objLisAddItemRefArr1[97].Value = p_objCollection.m_objContent.m_strCODINGNAME;
                objLisAddItemRefArr1[98].Value = p_objCollection.m_objContent.m_strINPUTMACHINENAME;
                objLisAddItemRefArr1[99].Value = p_objCollection.m_objContent.m_strSTATISTICNAME;
                objLisAddItemRefArr1[100].Value = m_lngEMR_SEQ;
                objLisAddItemRefArr1[101].Value = p_objCollection.m_objContent.m_strPAYTYPE;
                objLisAddItemRefArr1[102].Value = p_objCollection.m_objContent.m_strOTHERMAINCONDITION;
                objLisAddItemRefArr1[103].Value = p_objCollection.m_objContent.m_strOTHERCOMPLICATION;
                objLisAddItemRefArr1[104].Value = p_objCollection.m_objContent.m_strOTHERINFECTIONCONDICTION;
                objLisAddItemRefArr1[105].Value = p_objCollection.m_objContent.m_strOTHERPATHOLOGYDIAGNOSIS;
                objLisAddItemRefArr1[106].Value = p_objCollection.m_objContent.m_intXRAYCHECK;
                objLisAddItemRefArr1[107].Value = p_objCollection.m_objContent.m_strSTATCODEOFSCACHESOURCE;
                objLisAddItemRefArr1[108].Value = p_objCollection.m_objContent.m_strICD_10OFSCACHESOURCE;
                objLisAddItemRefArr1[109].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE1;
                objLisAddItemRefArr1[110].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE2;
                objLisAddItemRefArr1[111].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE3;
                objLisAddItemRefArr1[112].Value = p_objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE4;
                objLisAddItemRefArr1[113].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE1;
                objLisAddItemRefArr1[114].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE2;
                objLisAddItemRefArr1[115].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE3;
                objLisAddItemRefArr1[116].Value = p_objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE4;
                objLisAddItemRefArr1[117].Value = p_objCollection.m_objContent.m_intREMINDTERMTYPE;
                objLisAddItemRefArr1[118].DbType = DbType.DateTime;
                if (p_objCollection.m_objContent.m_dtmCATALOG_DATE == DateTime.MinValue)
                {
                    objLisAddItemRefArr1[118].Value = null;
                }
                else
                {
                    objLisAddItemRefArr1[118].Value = p_objCollection.m_objContent.m_dtmCATALOG_DATE;
                }
                objLisAddItemRefArr1[119].Value = p_objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDTNAME;
                objLisAddItemRefArr1[120].Value = p_objCollection.m_objContent.m_strDEPTDIRECTORDTNAME;
                objLisAddItemRefArr1[121].Value = p_objCollection.m_objContent.m_strDTNAME;
                objLisAddItemRefArr1[122].Value = p_objCollection.m_objContent.m_strINHOSPITALDOCNAME;
                objLisAddItemRefArr1[123].Value = p_objCollection.m_objContent.m_strOUTHOSPITALDOCNAME;
                objLisAddItemRefArr1[124].Value = p_objCollection.m_objContent.m_strDIRECTORDTNAME;
                objLisAddItemRefArr1[125].Value = p_objCollection.m_objContent.m_strSUBDIRECTORDTNAME;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngRecEff, objLisAddItemRefArr1);
                if (lngRes <= 0)
                    return -1;
                #endregion

                #region 添加入院诊断
                if (p_objCollection.m_objInDiagnosisArr != null)
                {
                    System.Data.IDataParameter[] objSeqArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objSeqArr);
                    objSeqArr[0].Value = m_lngEMR_SEQ;
                    //将其他诊断的旧记录状态置为2
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordInDiagnoseSQL, ref lngRecEff, objSeqArr);
                    if (lngRes <= 0)
                        return -1;

                    System.Data.IDataParameter[] objLisAddItemRefArr4 = null;
                    for (int i = 0; i < p_objCollection.m_objInDiagnosisArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(12, out objLisAddItemRefArr4);

                        objLisAddItemRefArr4[0].Value = p_objCollection.m_objInDiagnosisArr[i].m_strInPatientID;
                        objLisAddItemRefArr4[1].DbType = DbType.DateTime;
                        objLisAddItemRefArr4[1].Value = p_objCollection.m_objInDiagnosisArr[i].m_dtmInPatientDate;
                        objLisAddItemRefArr4[2].DbType = DbType.DateTime;
                        objLisAddItemRefArr4[2].Value = p_objCollection.m_objInDiagnosisArr[i].m_dtmOpenDate;
                        objLisAddItemRefArr4[3].DbType = DbType.DateTime;
                        objLisAddItemRefArr4[3].Value = p_objCollection.m_objInDiagnosisArr[i].m_dtmModifyDate;
                        objLisAddItemRefArr4[4].Value = p_objCollection.m_objInDiagnosisArr[i].m_strModifyUserID;
                        objLisAddItemRefArr4[5].Value = 0;
                        objLisAddItemRefArr4[6].Value = p_objCollection.m_objInDiagnosisArr[i].m_strDIAGNOSISDESC;
                        objLisAddItemRefArr4[7].Value = p_objCollection.m_objInDiagnosisArr[i].m_strSTATCODE;
                        objLisAddItemRefArr4[8].Value = p_objCollection.m_objInDiagnosisArr[i].m_strICD10;
                        objLisAddItemRefArr4[9].Value = p_objCollection.m_objInDiagnosisArr[i].m_strREGISTERID_CHR.Trim();
                        objLisAddItemRefArr4[10].Value = p_objCollection.m_objInDiagnosisArr[i].m_strSEQID;
                        objLisAddItemRefArr4[11].Value = m_lngEMR_SEQ;

                        //往表增加记录
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordInDiagnoseSQL, ref lngRecEff, objLisAddItemRefArr4);
                        if (lngRes <= 0)
                            return -1;
                    }
                }
                #endregion

                #region 添加其他诊断
                if (p_objCollection.m_objOtherDiagnosisArr != null)
                {
                    System.Data.IDataParameter[] objSeqArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objSeqArr);
                    objSeqArr[0].Value = m_lngEMR_SEQ;
                    //将其他诊断的旧记录状态置为2
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordOtherDiagnoseSQL, ref lngRecEff, objSeqArr);
                    if (lngRes <= 0)
                        return -1;

                    System.Data.IDataParameter[] objLisAddItemRefArr2 = null;
                    for (int i = 0; i < p_objCollection.m_objOtherDiagnosisArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(14, out objLisAddItemRefArr2);

                        objLisAddItemRefArr2[0].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strInPatientID;
                        objLisAddItemRefArr2[1].DbType = DbType.DateTime;
                        objLisAddItemRefArr2[1].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_dtmInPatientDate;
                        objLisAddItemRefArr2[2].DbType = DbType.DateTime;
                        objLisAddItemRefArr2[2].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_dtmOpenDate;
                        objLisAddItemRefArr2[3].DbType = DbType.DateTime;
                        objLisAddItemRefArr2[3].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_dtmModifyDate;
                        objLisAddItemRefArr2[4].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strModifyUserID;
                        objLisAddItemRefArr2[5].Value = 0;
                        objLisAddItemRefArr2[6].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strDIAGNOSISDESC;
                        objLisAddItemRefArr2[7].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_intCONDITIONSEQ;
                        objLisAddItemRefArr2[8].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strSTATCODE;
                        objLisAddItemRefArr2[9].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strICD10;
                        objLisAddItemRefArr2[10].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strREGISTERID_CHR.Trim();
                        objLisAddItemRefArr2[11].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strSEQID;
                        objLisAddItemRefArr2[12].Value = m_lngEMR_SEQ;
                        objLisAddItemRefArr2[13].Value = p_objCollection.m_objOtherDiagnosisArr[i].m_strOTHERCONDITION;

                        //往表增加记录
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordOtherDiagnoseSQL, ref lngRecEff, objLisAddItemRefArr2);
                        if (lngRes <= 0)
                            return -1;
                    }
                }
                #endregion

                #region 添加手术信息
                if (p_objCollection.m_objOperationArr != null)
                {
                    System.Data.IDataParameter[] objSeqArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objSeqArr);
                    objSeqArr[0].Value = m_lngEMR_SEQ;
                    //将手术信息的旧记录状态置为2
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordOperationSQL, ref lngRecEff, objSeqArr);
                    if (lngRes <= 0)
                        return -1;

                    System.Data.IDataParameter[] objLisAddItemRefArr3 = null;
                    for (int i = 0; i < p_objCollection.m_objOperationArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(19, out objLisAddItemRefArr3);

                        objLisAddItemRefArr3[0].Value = p_objCollection.m_objOperationArr[i].m_strInPatientID;
                        objLisAddItemRefArr3[1].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[1].Value = p_objCollection.m_objOperationArr[i].m_dtmInPatientDate;
                        objLisAddItemRefArr3[2].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[2].Value = p_objCollection.m_objOperationArr[i].m_dtmOpenDate;
                        objLisAddItemRefArr3[3].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[3].Value = p_objCollection.m_objOperationArr[i].m_dtmModifyDate;
                        objLisAddItemRefArr3[4].Value = p_objCollection.m_objOperationArr[i].m_strModifyUserID;
                        objLisAddItemRefArr3[5].Value = 0;
                        objLisAddItemRefArr3[6].Value = p_objCollection.m_objOperationArr[i].m_strSEQID;
                        objLisAddItemRefArr3[7].DbType = DbType.DateTime;
                        objLisAddItemRefArr3[7].Value = p_objCollection.m_objOperationArr[i].m_dtmOPERATIONDATE;
                        objLisAddItemRefArr3[8].Value = p_objCollection.m_objOperationArr[i].m_strOPERATIONNAME;
                        if (p_objCollection.m_objOperationArr[i].m_strOPERATOR != null)
                            objLisAddItemRefArr3[9].Value = p_objCollection.m_objOperationArr[i].m_strOPERATOR.Trim();
                        else
                            objLisAddItemRefArr3[9].Value = "";
                        if (p_objCollection.m_objOperationArr[i].m_strASSISTANT1 != null)
                            objLisAddItemRefArr3[10].Value = p_objCollection.m_objOperationArr[i].m_strASSISTANT1.Trim();
                        else
                            objLisAddItemRefArr3[10].Value = "";
                        if (p_objCollection.m_objOperationArr[i].m_strASSISTANT2 != null)
                            objLisAddItemRefArr3[11].Value = p_objCollection.m_objOperationArr[i].m_strASSISTANT2.Trim();
                        else
                            objLisAddItemRefArr3[11].Value = "";
                        if (p_objCollection.m_objOperationArr[i].m_strAANAESTHESIAMODEID != null)
                            objLisAddItemRefArr3[12].Value = p_objCollection.m_objOperationArr[i].m_strAANAESTHESIAMODEID.Trim();
                        else
                            objLisAddItemRefArr3[12].Value = "";
                        objLisAddItemRefArr3[13].Value = p_objCollection.m_objOperationArr[i].m_strCUTLEVEL;
                        if (p_objCollection.m_objOperationArr[i].m_strANAESTHETIST != null)
                            objLisAddItemRefArr3[14].Value = p_objCollection.m_objOperationArr[i].m_strANAESTHETIST.Trim();
                        else
                            objLisAddItemRefArr3[14].Value = "";
                        objLisAddItemRefArr3[15].Value = p_objCollection.m_objOperationArr[i].m_strOPERATIONAANAESTHESIAMODENAME;
                        objLisAddItemRefArr3[16].Value = p_objCollection.m_objOperationArr[i].m_strREGISTERID_CHR.Trim();
                        objLisAddItemRefArr3[17].Value = m_lngEMR_SEQ;
                        objLisAddItemRefArr3[18].Value = p_objCollection.m_objOperationArr[i].m_strOPERATIONID;

                        //往表增加记录
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordOperationSQL, ref lngRecEff, objLisAddItemRefArr3);
                        if (lngRes <= 0)
                            return -1;
                    }
                }
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

            }            //返回
            return lngRes ;
        }
        #endregion

        #region 获得创建日期
        /// <summary>
        ///  获得创建日期
        /// 如果没有，则表示还没有生成过病案首页
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_dtmOpenDate">创建时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOpenDateInfo(
            string p_strRegisterID, out DateTime[] p_dtmOpenDate)
        {
            p_dtmOpenDate = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngGetOpenDateInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == "")
                    return -1;

                IDataParameter[] objSeqArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objSeqArr);
                objSeqArr[0].Value = p_strRegisterID.Trim();

                DataTable objResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetOpenDate, ref objResult, objSeqArr);

                if (lngRes > 0 && objResult.Rows.Count > 0)
                {
                    p_dtmOpenDate = new DateTime[objResult.Rows.Count];
                    for (int i = 0; i < p_dtmOpenDate.Length; i++)
                    {
                        p_dtmOpenDate[i] = Convert.ToDateTime(objResult.Rows[i][0]);
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

            }      //返回
            return lngRes;

        }
        #endregion

        #region 获得最后修改人及时间
        /// <summary>
        /// 获得最后修改人及时间
        /// </summary>
        /// <param name="m_lngEMR_Seq">序列号</param>
        /// <param name="p_strLastModifyUserID">最后修改人</param>
        /// <param name="p_dtmLastModifyDate">最后修改时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLastModifyDateInfo(long m_lngEMR_Seq, out string p_strLastModifyUserID, out DateTime p_dtmLastModifyDate)
        {
            p_dtmLastModifyDate = DateTime.MinValue;
            p_strLastModifyUserID = "";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objSeqArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objSeqArr);
                objSeqArr[0].Value = m_lngEMR_Seq;

                DataTable objResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetMaxModifyInfo, ref objResult, objSeqArr);

                if (lngRes > 0 && objResult.Rows.Count > 0)
                {
                    p_dtmLastModifyDate = Convert.ToDateTime(objResult.Rows[0][0]);
                    p_strLastModifyUserID = objResult.Rows[0][1].ToString();
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

            }            //返回
            return lngRes;

        }
        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strOpenDate">创建时间</param>
        /// <param name="p_blnIsSumit">是否查询已提交记录</param>
        /// <param name="p_objCollection">病案记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInfo(
            string p_strRegisterID,
            string p_strOpenDate, bool p_blnIsSumit,
            out clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            long lngRes = 0;
            p_objCollection = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngGetInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                clsInHospitalMainRecord_GX_Collection objCollection = new clsInHospitalMainRecord_GX_Collection();
                long m_lngEMR_Seq = 0;

                #region 获取主表及子表内容
                IDataParameter[] objMainArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objMainArr);
                objMainArr[0].Value = p_strRegisterID.Trim();
                objMainArr[1].DbType = DbType.DateTime;
                objMainArr[1].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtMain = new DataTable();

                if (p_blnIsSumit)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetMainSubmitInfo, ref dtMain, objMainArr);
                else
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetMainInfo, ref dtMain, objMainArr);

                if (lngRes <= 0 || dtMain.Rows.Count <= 0)
                    return -1;
                else
                {
                    objCollection.m_objMain = new clsInHospitalMainRecord_GX();
                    objCollection.m_objMain.m_strInPatientID = dtMain.Rows[0]["INPATIENTID"].ToString();
                    objCollection.m_objMain.m_dtmInPatientDate = Convert.ToDateTime(dtMain.Rows[0]["INPATIENTDATE"]);
                    objCollection.m_objMain.m_dtmOpenDate = Convert.ToDateTime(dtMain.Rows[0]["OPENDATE"]);
                    objCollection.m_objMain.m_strCreateUserID = dtMain.Rows[0]["CREATEUSERID"].ToString();
                    if (dtMain.Rows[0]["STATUS"].ToString() == "")
                        objCollection.m_objMain.m_bytStatus = 0;
                    else objCollection.m_objMain.m_bytStatus = Byte.Parse(dtMain.Rows[0]["STATUS"].ToString());
                    if (dtMain.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objCollection.m_objMain.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objCollection.m_objMain.m_dtmFirstPrintDate = Convert.ToDateTime(dtMain.Rows[0]["FIRSTPRINTDATE"]);
                    objCollection.m_objMain.m_strDIAGNOSISXML = dtMain.Rows[0]["DIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strINHOSPITALDIAGNOSISXML = dtMain.Rows[0]["INHOSPITALDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strMAINDIAGNOSISXML = dtMain.Rows[0]["MAINDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strCOMPLICATIONXML = dtMain.Rows[0]["COMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strINFECTIONDIAGNOSISXML = dtMain.Rows[0]["INFECTIONDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strPATHOLOGYDIAGNOSISXML = dtMain.Rows[0]["PATHOLOGYDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFMAINXML = dtMain.Rows[0]["ICD_10OFMAINXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFINFECTIONXML = dtMain.Rows[0]["ICD_10OFINFECTIONXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFCOMPLICATIONXML = dtMain.Rows[0]["ICD_10OFCOMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFDIAGNOSISXML = dtMain.Rows[0]["ICD_10OFDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFINHOSPITALDIAXML = dtMain.Rows[0]["ICD_10OFINHOSPITALDIAXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFPATHOLOGYDIAXML = dtMain.Rows[0]["ICD_10OFPATHOLOGYDIAXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFMAINXML = dtMain.Rows[0]["STATCODEOFMAINXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFINFECTIONXML = dtMain.Rows[0]["STATCODEOFINFECTIONXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFPATHOLOGYDIAXML = dtMain.Rows[0]["STATCODEOFPATHOLOGYDIAXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFDIAGNOSISXML = dtMain.Rows[0]["STATCODEOFDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFINHOSPITALDIAXML = dtMain.Rows[0]["STATCODEOFINHOSPITALDIAXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFCOMPLICATIONXML = dtMain.Rows[0]["STATCODEOFCOMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strSCACHESOURCEXML = dtMain.Rows[0]["SCACHESOURCEXML"].ToString();
                    objCollection.m_objMain.m_strSENSITIVEXML = dtMain.Rows[0]["SENSITIVEXML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE1XML = dtMain.Rows[0]["NEONATEDISEASE1XML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE2XML = dtMain.Rows[0]["NEONATEDISEASE2XML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE3XML = dtMain.Rows[0]["NEONATEDISEASE3XML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE4XML = dtMain.Rows[0]["NEONATEDISEASE4XML"].ToString();
                    objCollection.m_objMain.m_strSALVETIMESXML = dtMain.Rows[0]["SALVETIMESXML"].ToString();
                    objCollection.m_objMain.m_strSALVESUCCESSXML = dtMain.Rows[0]["SALVESUCCESSXML"].ToString();
                    objCollection.m_objMain.m_strREMINDTERMXML = dtMain.Rows[0]["REMINDTERMXML"].ToString();
                    objCollection.m_objMain.m_strRBCXML = dtMain.Rows[0]["RBCXML"].ToString();
                    objCollection.m_objMain.m_strPLTXML = dtMain.Rows[0]["PLTXML"].ToString();
                    objCollection.m_objMain.m_strPLASMXML = dtMain.Rows[0]["PLASMXML"].ToString();
                    objCollection.m_objMain.m_strWHOLEBLOODXML = dtMain.Rows[0]["WHOLEBLOODXML"].ToString();
                    objCollection.m_objMain.m_strOTHERBLOODXML = dtMain.Rows[0]["OTHERBLOODXML"].ToString();
                    objCollection.m_objMain.m_strREGISTERID_CHR = dtMain.Rows[0]["REGISTERID_CHR"].ToString();
                    objCollection.m_objMain.m_strOTHERMAINCONDITIONXML = dtMain.Rows[0]["OTHERMAINCONDITIONXML"].ToString();
                    objCollection.m_objMain.m_strOTHERCOMPLICATIONXML = dtMain.Rows[0]["OTHERCOMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strOTHERINFECTIONCONDICTIONXML = dtMain.Rows[0]["OTHERINFECTIONCONDICTIONXML"].ToString();
                    objCollection.m_objMain.m_strOTHERPATHOLOGYDIAGNOSISXML = dtMain.Rows[0]["OTHERPATHOLOGYDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_lngEMR_SEQ = Convert.ToInt64(dtMain.Rows[0]["EMR_SEQ"]);
                    if (dtMain.Rows[0]["SUBMIT_INT"] == DBNull.Value)
                        objCollection.m_objMain.m_intSUBMIT_INT = 0;
                    else
                        objCollection.m_objMain.m_intSUBMIT_INT = Convert.ToInt32(dtMain.Rows[0]["SUBMIT_INT"].ToString());
                    m_lngEMR_Seq = Convert.ToInt64(dtMain.Rows[0]["EMR_SEQ"]);
                }

                IDataParameter[] objContentArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objContentArr);
                objContentArr[0].Value = m_lngEMR_Seq;

                DataTable dtContent = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetContentInfo, ref dtContent, objContentArr);

                if (lngRes <= 0 || dtContent.Rows.Count <= 0)
                    return -1;
                else
                {
                    objCollection.m_objContent = new clsInHospitalMainRecord_GXContent();
                    objCollection.m_objContent.m_strInPatientID = dtContent.Rows[0]["INPATIENTID"].ToString();
                    objCollection.m_objContent.m_dtmInPatientDate = Convert.ToDateTime(dtContent.Rows[0]["INPATIENTDATE"]);
                    objCollection.m_objContent.m_dtmOpenDate = Convert.ToDateTime(dtContent.Rows[0]["OPENDATE"]);
                    objCollection.m_objContent.m_dtmModifyDate = Convert.ToDateTime(dtContent.Rows[0]["LASTMODIFYDATE"]);
                    objCollection.m_objContent.m_strModifyUserID = dtContent.Rows[0]["LASTMODIFYUSERID"].ToString();
                    if (dtContent.Rows[0]["STATUS"].ToString() == "")
                        objCollection.m_objContent.m_bytStatus = 0;
                    else objCollection.m_objContent.m_bytStatus = Byte.Parse(dtContent.Rows[0]["STATUS"].ToString());
                    objCollection.m_objContent.m_intCONDICTIONWHENIN = Convert.ToInt32(dtContent.Rows[0]["CONDICTIONWHENIN"].ToString());
                    objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE = Convert.ToDateTime(dtContent.Rows[0]["CONFIRMDIAGNOSISDATE"]);
                    objCollection.m_objContent.m_strDIAGNOSIS = dtContent.Rows[0]["DIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strINHOSPITALDIAGNOSIS = dtContent.Rows[0]["INHOSPITALDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strMAINDIAGNOSIS = dtContent.Rows[0]["MAINDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strCOMPLICATION = dtContent.Rows[0]["COMPLICATION"].ToString();
                    objCollection.m_objContent.m_strINFECTIONDIAGNOSIS = dtContent.Rows[0]["INFECTIONDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strPATHOLOGYDIAGNOSIS = dtContent.Rows[0]["PATHOLOGYDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strICD_10OFMAIN = dtContent.Rows[0]["ICD_10OFMAIN"].ToString();
                    objCollection.m_objContent.m_strICD_10OFINFECTION = dtContent.Rows[0]["ICD_10OFINFECTION"].ToString();
                    objCollection.m_objContent.m_strICD_10OFCOMPLICATION = dtContent.Rows[0]["ICD_10OFCOMPLICATION"].ToString();
                    objCollection.m_objContent.m_strICD_10OFDIAGNOSIS = dtContent.Rows[0]["ICD_10OFDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strICD_10OFINHOSPITALDIA = dtContent.Rows[0]["ICD_10OFINHOSPITALDIA"].ToString();
                    objCollection.m_objContent.m_strICD_10OFPATHOLOGYDIA = dtContent.Rows[0]["ICD_10OFPATHOLOGYDIA"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFMAIN = dtContent.Rows[0]["STATCODEOFMAIN"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFINFECTION = dtContent.Rows[0]["STATCODEOFINFECTION"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFPATHOLOGYDIA = dtContent.Rows[0]["STATCODEOFPATHOLOGYDIA"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFDIAGNOSIS = dtContent.Rows[0]["STATCODEOFDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFINHOSPITALDIA = dtContent.Rows[0]["STATCODEOFINHOSPITALDIA"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFCOMPLICATION = dtContent.Rows[0]["STATCODEOFCOMPLICATION"].ToString();
                    objCollection.m_objContent.m_intMAINCONDITIONSEQ = dtContent.Rows[0]["MAINCONDITIONSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["MAINCONDITIONSEQ"].ToString());
                    objCollection.m_objContent.m_intCOMPLICATIONSEQ = dtContent.Rows[0]["COMPLICATIONSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["COMPLICATIONSEQ"].ToString());
                    objCollection.m_objContent.m_intINFECTIONCONDICTIONSEQ = dtContent.Rows[0]["INFECTIONCONDICTIONSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["INFECTIONCONDICTIONSEQ"].ToString());
                    objCollection.m_objContent.m_intPATHOLOGYDIAGNOSISSEQ = dtContent.Rows[0]["PATHOLOGYDIAGNOSISSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["PATHOLOGYDIAGNOSISSEQ"].ToString());
                    objCollection.m_objContent.m_strSCACHESOURCE = dtContent.Rows[0]["SCACHESOURCE"].ToString();
                    objCollection.m_objContent.m_intNEW5DISEASE = Convert.ToInt32(dtContent.Rows[0]["NEW5DISEASE"].ToString());
                    objCollection.m_objContent.m_intSECONDLEVELTRANSFER = Convert.ToInt32(dtContent.Rows[0]["SECONDLEVELTRANSFER"].ToString());
                    objCollection.m_objContent.m_strSENSITIVE = dtContent.Rows[0]["SENSITIVE"].ToString();
                    objCollection.m_objContent.m_intHBSAG = Convert.ToInt32(dtContent.Rows[0]["HBSAG"].ToString());
                    objCollection.m_objContent.m_intHCV_AB = Convert.ToInt32(dtContent.Rows[0]["HCV_AB"].ToString());
                    objCollection.m_objContent.m_intHIV_AB = Convert.ToInt32(dtContent.Rows[0]["HIV_AB"].ToString());
                    objCollection.m_objContent.m_strNEONATEDISEASE1 = dtContent.Rows[0]["NEONATEDISEASE1"].ToString();
                    objCollection.m_objContent.m_strNEONATEDISEASE2 = dtContent.Rows[0]["NEONATEDISEASE2"].ToString();
                    objCollection.m_objContent.m_strNEONATEDISEASE3 = dtContent.Rows[0]["NEONATEDISEASE3"].ToString();
                    objCollection.m_objContent.m_strNEONATEDISEASE4 = dtContent.Rows[0]["NEONATEDISEASE4"].ToString();
                    objCollection.m_objContent.m_intSALVETIMES = Convert.ToInt32(dtContent.Rows[0]["SALVETIMES"].ToString());
                    objCollection.m_objContent.m_intSALVESUCCESS = Convert.ToInt32(dtContent.Rows[0]["SALVESUCCESS"].ToString());
                    objCollection.m_objContent.m_intHASREMIND = Convert.ToInt32(dtContent.Rows[0]["HASREMIND"].ToString());
                    objCollection.m_objContent.m_strREMINDTERM = dtContent.Rows[0]["REMINDTERM"].ToString();
                    objCollection.m_objContent.m_intACCORDWITHOUTHOSPITAL = Convert.ToInt32(dtContent.Rows[0]["ACCORDWITHOUTHOSPITAL"].ToString());
                    objCollection.m_objContent.m_intACCORDINWITHOUT = Convert.ToInt32(dtContent.Rows[0]["ACCORDINWITHOUT"].ToString());
                    objCollection.m_objContent.m_intACCORDBFOPRWITHAF = Convert.ToInt32(dtContent.Rows[0]["ACCORDBFOPRWITHAF"].ToString());
                    objCollection.m_objContent.m_intACCORDCLINICWITHPATHOLOGY = Convert.ToInt32(dtContent.Rows[0]["ACCORDCLINICWITHPATHOLOGY"].ToString());
                    objCollection.m_objContent.m_intACCORDCLINICWITHRADIATE = Convert.ToInt32(dtContent.Rows[0]["ACCORDCLINICWITHRADIATE"].ToString());
                    objCollection.m_objContent.m_intACCORDDEATHWITHBODYCHECK = Convert.ToInt32(dtContent.Rows[0]["ACCORDDEATHWITHBODYCHECK"].ToString());
                    objCollection.m_objContent.m_intFIRSTCASE = Convert.ToInt32(dtContent.Rows[0]["FIRSTCASE"].ToString());
                    objCollection.m_objContent.m_intMODELCASE = Convert.ToInt32(dtContent.Rows[0]["MODELCASE"].ToString());
                    objCollection.m_objContent.m_intQUALITY = Convert.ToInt32(dtContent.Rows[0]["QUALITY"].ToString());
                    objCollection.m_objContent.m_intANTIBACTERIAL = Convert.ToInt32(dtContent.Rows[0]["ANTIBACTERIAL"].ToString());
                    objCollection.m_objContent.m_intPATHOGENY = Convert.ToInt32(dtContent.Rows[0]["PATHOGENY"].ToString());
                    objCollection.m_objContent.m_intPATHOGENYRESULT = Convert.ToInt32(dtContent.Rows[0]["PATHOGENYRESULT"].ToString());
                    objCollection.m_objContent.m_intBLOODTRANSACTOIN = Convert.ToInt32(dtContent.Rows[0]["BLOODTRANSACTOIN"].ToString());
                    objCollection.m_objContent.m_intTRANSFUSIONSACTION = Convert.ToInt32(dtContent.Rows[0]["TRANSFUSIONSACTION"].ToString());
                    objCollection.m_objContent.m_intCTCHECK = Convert.ToInt32(dtContent.Rows[0]["CTCHECK"].ToString());
                    objCollection.m_objContent.m_intMRICHECK = Convert.ToInt32(dtContent.Rows[0]["MRICHECK"].ToString());
                    objCollection.m_objContent.m_intBLOODTYPE = dtContent.Rows[0]["BLOODTYPE"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["BLOODTYPE"].ToString());
                    objCollection.m_objContent.m_intBLOODRH = dtContent.Rows[0]["BLOODRH"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["BLOODRH"].ToString());
                    objCollection.m_objContent.m_strRBC = dtContent.Rows[0]["RBC"].ToString();
                    objCollection.m_objContent.m_strPLT = dtContent.Rows[0]["PLT"].ToString();
                    objCollection.m_objContent.m_strPLASM = dtContent.Rows[0]["PLASM"].ToString();
                    objCollection.m_objContent.m_strWHOLEBLOOD = dtContent.Rows[0]["WHOLEBLOOD"].ToString();
                    objCollection.m_objContent.m_strOTHERBLOOD = dtContent.Rows[0]["OTHERBLOOD"].ToString();
                    objCollection.m_objContent.m_strDEPTDIRECTORDT = dtContent.Rows[0]["DEPTDIRECTORDT"].ToString();
                    objCollection.m_objContent.m_strDEPTDIRECTORDTNAME = dtContent.Rows[0]["DEPTDIRECTORDTNAME"].ToString();
                    objCollection.m_objContent.m_strDT = dtContent.Rows[0]["DT"].ToString();
                    objCollection.m_objContent.m_strDTNAME = dtContent.Rows[0]["DTNAME"].ToString();
                    objCollection.m_objContent.m_strINHOSPITALDOC = dtContent.Rows[0]["INHOSPITALDOC"].ToString();
                    objCollection.m_objContent.m_strINHOSPITALDOCNAME = dtContent.Rows[0]["INHOSPITALDOCNAME"].ToString();
                    objCollection.m_objContent.m_strOUTHOSPITALDOC = dtContent.Rows[0]["OUTHOSPITALDOC"].ToString();
                    objCollection.m_objContent.m_strOUTHOSPITALDOCNAME = dtContent.Rows[0]["OUTHOSPITALDOCNAME"].ToString();
                    objCollection.m_objContent.m_strDIRECTORDT = dtContent.Rows[0]["DIRECTORDT"].ToString();
                    objCollection.m_objContent.m_strDIRECTORDTNAME = dtContent.Rows[0]["DIRECTORDTNAME"].ToString();
                    objCollection.m_objContent.m_strSUBDIRECTORDT = dtContent.Rows[0]["SUBDIRECTORDT"].ToString();
                    objCollection.m_objContent.m_strSUBDIRECTORDTNAME = dtContent.Rows[0]["SUBDIRECTORDTNAME"].ToString();
                    objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDT = dtContent.Rows[0]["ATTENDINFORADVANCESSTUDYDT"].ToString();
                    objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDTNAME = dtContent.Rows[0]["ADVANCEDSTUDIESDTNAME"].ToString();
                    objCollection.m_objContent.m_strGRADUATESTUDENTINTERNNAME = dtContent.Rows[0]["GRADUATESTUDENTINTERN"].ToString();
                    objCollection.m_objContent.m_strINTERNNAME = dtContent.Rows[0]["INTERN"].ToString();
                    objCollection.m_objContent.m_dblTOTALAMT = Convert.ToDouble(dtContent.Rows[0]["TOTALAMT"]);
                    objCollection.m_objContent.m_dblBEDAMT = Convert.ToDouble(dtContent.Rows[0]["BEDAMT"]); ;
                    objCollection.m_objContent.m_dblNURSEAMT = Convert.ToDouble(dtContent.Rows[0]["NURSEAMT"]);
                    objCollection.m_objContent.m_dblWMAMT = Convert.ToDouble(dtContent.Rows[0]["WMAMT"]);
                    objCollection.m_objContent.m_dblCMFINISHEDAMT = Convert.ToDouble(dtContent.Rows[0]["CMFINISHEDAMT"]);
                    objCollection.m_objContent.m_dblCMSEMIFINISHEDAMT = Convert.ToDouble(dtContent.Rows[0]["CMSEMIFINISHEDAMT"]);
                    objCollection.m_objContent.m_dblRADIATIONAMT = Convert.ToDouble(dtContent.Rows[0]["RADIATIONAMT"]);
                    objCollection.m_objContent.m_dblASSAYAMT = Convert.ToDouble(dtContent.Rows[0]["ASSAYAMT"]);
                    objCollection.m_objContent.m_dblO2AMT = Convert.ToDouble(dtContent.Rows[0]["O2AMT"]);
                    objCollection.m_objContent.m_dblBLOODAMT = Convert.ToDouble(dtContent.Rows[0]["BLOODAMT"]);
                    objCollection.m_objContent.m_dblTREATMENTAMT = Convert.ToDouble(dtContent.Rows[0]["TREATMENTAMT"]);
                    objCollection.m_objContent.m_dblOPERATIONAMT = Convert.ToDouble(dtContent.Rows[0]["OPERATIONAMT"]);
                    objCollection.m_objContent.m_dblCHECKAMT = Convert.ToDouble(dtContent.Rows[0]["CHECKAMT"]);
                    objCollection.m_objContent.m_dblANAETHESIAAMT = Convert.ToDouble(dtContent.Rows[0]["ANAETHESIAAMT"]);
                    objCollection.m_objContent.m_dblDELIVERYCHILDAMT = Convert.ToDouble(dtContent.Rows[0]["DELIVERYCHILDAMT"]);
                    objCollection.m_objContent.m_dblBABYAMT = Convert.ToDouble(dtContent.Rows[0]["BABYAMT"]);
                    objCollection.m_objContent.m_dblACCOMPANYAMT = Convert.ToDouble(dtContent.Rows[0]["ACCOMPANYAMT"]);
                    objCollection.m_objContent.m_dblOTHERAMT = Convert.ToDouble(dtContent.Rows[0]["OTHERAMT"]);
                    objCollection.m_objContent.m_strREGISTERID_CHR = dtContent.Rows[0]["REGISTERID_CHR"].ToString();
                    objCollection.m_objContent.m_strNEATENNAME = dtContent.Rows[0]["NEATENNAME"].ToString();
                    objCollection.m_objContent.m_strCODINGNAME = dtContent.Rows[0]["CODINGNAME"].ToString();
                    objCollection.m_objContent.m_strINPUTMACHINENAME = dtContent.Rows[0]["INPUTMACHINENAME"].ToString();
                    objCollection.m_objContent.m_strSTATISTICNAME = dtContent.Rows[0]["STATISTICNAME"].ToString();
                    objCollection.m_objContent.m_strOTHERMAINCONDITION = dtContent.Rows[0]["OTHERMAINCONDITION"].ToString();
                    objCollection.m_objContent.m_strOTHERCOMPLICATION = dtContent.Rows[0]["OTHERCOMPLICATION"].ToString();
                    objCollection.m_objContent.m_strOTHERINFECTIONCONDICTION = dtContent.Rows[0]["OTHERINFECTIONCONDICTION"].ToString();
                    objCollection.m_objContent.m_strOTHERPATHOLOGYDIAGNOSIS = dtContent.Rows[0]["OTHERPATHOLOGYDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strPAYTYPE = dtContent.Rows[0]["PAYTYPE"].ToString();
                    objCollection.m_objContent.m_intXRAYCHECK = Convert.ToInt32(dtContent.Rows[0]["XRAYCHECK"].ToString());
                    objCollection.m_objContent.m_strSTATCODEOFSCACHESOURCE = dtContent.Rows[0]["STATCODEOFSCACHESOURCE"].ToString();
                    objCollection.m_objContent.m_strICD_10OFSCACHESOURCE = dtContent.Rows[0]["ICD_10OFSCACHESOURCE"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE1 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE1"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE2 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE2"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE3 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE3"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE4 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE4"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE1 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE1"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE2 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE2"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE3 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE3"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE4 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE4"].ToString();
                    if (dtContent.Rows[0]["REMINDTERMTYPE"] == DBNull.Value)
                        objCollection.m_objContent.m_intREMINDTERMTYPE = 0;
                    else
                        objCollection.m_objContent.m_intREMINDTERMTYPE = Convert.ToInt32(dtContent.Rows[0]["REMINDTERMTYPE"].ToString());
                    if (dtContent.Rows[0]["CATALOG_DATE"] == DBNull.Value)
                        objCollection.m_objContent.m_dtmCATALOG_DATE = DateTime.MinValue;
                    else
                        objCollection.m_objContent.m_dtmCATALOG_DATE = Convert.ToDateTime(dtContent.Rows[0]["CATALOG_DATE"]);
                    //if (dtContent.Rows[0]["ATTENDINFORADVANCESSTUDYDT"] == DBNull.Value)
                    //{
                        //objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDTNAME = dtContent.Rows[0]["ADVANCEDSTUDIESDTNAME"].ToString();
                    //}
                    objCollection.m_objContent.m_lngEMR_SEQ = m_lngEMR_Seq;
                }
                #endregion

                #region 获取入院诊断内容
                IDataParameter[] objInDiaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objInDiaArr);
                objInDiaArr[0].Value = m_lngEMR_Seq;

                DataTable dtInDia = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetInDiagnosisInfo, ref dtInDia, objInDiaArr);

                if (lngRes > 0 && dtInDia.Rows.Count > 0)
                {
                    objCollection.m_objInDiagnosisArr = new clsInHospitalMainRecord_GXInDiagnosis[dtInDia.Rows.Count];
                    for (int i = 0; i < dtInDia.Rows.Count; i++)
                    {
                        objCollection.m_objInDiagnosisArr[i] = new clsInHospitalMainRecord_GXInDiagnosis();
                        objCollection.m_objInDiagnosisArr[i].m_strInPatientID = dtInDia.Rows[i]["INPATIENTID"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_dtmInPatientDate = Convert.ToDateTime(dtInDia.Rows[i]["INPATIENTDATE"]);
                        objCollection.m_objInDiagnosisArr[i].m_dtmOpenDate = Convert.ToDateTime(dtInDia.Rows[i]["OPENDATE"]);
                        objCollection.m_objInDiagnosisArr[i].m_dtmModifyDate = Convert.ToDateTime(dtInDia.Rows[i]["LASTMODIFYDATE"]);
                        objCollection.m_objInDiagnosisArr[i].m_strModifyUserID = dtInDia.Rows[i]["LASTMODIFYUSERID"].ToString();
                        if (dtInDia.Rows[i]["STATUS"].ToString() == "")
                            objCollection.m_objInDiagnosisArr[i].m_bytStatus = 0;
                        else objCollection.m_objInDiagnosisArr[i].m_bytStatus = Byte.Parse(dtInDia.Rows[i]["STATUS"].ToString());
                        objCollection.m_objInDiagnosisArr[i].m_strDIAGNOSISDESC = dtInDia.Rows[i]["DIAGNOSISDESC"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strSTATCODE = dtInDia.Rows[i]["STATCODE"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strICD10 = dtInDia.Rows[i]["ICD10"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strREGISTERID_CHR = dtInDia.Rows[i]["REGISTERID_CHR"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strSEQID = dtInDia.Rows[i]["SEQID"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_lngEMR_SEQ = m_lngEMR_Seq;
                    }
                }
                #endregion

                #region 获取其他诊断内容
                IDataParameter[] objOtherDiaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objOtherDiaArr);
                objOtherDiaArr[0].Value = m_lngEMR_Seq;

                DataTable dtOtherDia = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetOtherDiagnosisInfo, ref dtOtherDia, objOtherDiaArr);

                if (lngRes > 0 && dtOtherDia.Rows.Count > 0)
                {
                    objCollection.m_objOtherDiagnosisArr = new clsInHospitalMainRecord_GXOtherDiagnose[dtOtherDia.Rows.Count];
                    for (int i = 0; i < dtOtherDia.Rows.Count; i++)
                    {
                        objCollection.m_objOtherDiagnosisArr[i] = new clsInHospitalMainRecord_GXOtherDiagnose();
                        objCollection.m_objOtherDiagnosisArr[i].m_strInPatientID = dtOtherDia.Rows[i]["INPATIENTID"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmInPatientDate = Convert.ToDateTime(dtOtherDia.Rows[i]["INPATIENTDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmOpenDate = Convert.ToDateTime(dtOtherDia.Rows[i]["OPENDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmModifyDate = Convert.ToDateTime(dtOtherDia.Rows[i]["LASTMODIFYDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_strModifyUserID = dtOtherDia.Rows[i]["LASTMODIFYUSERID"].ToString();
                        if (dtOtherDia.Rows[i]["STATUS"].ToString() == "")
                            objCollection.m_objOtherDiagnosisArr[i].m_bytStatus = 0;
                        else objCollection.m_objOtherDiagnosisArr[i].m_bytStatus = Byte.Parse(dtOtherDia.Rows[i]["STATUS"].ToString());
                        objCollection.m_objOtherDiagnosisArr[i].m_strDIAGNOSISDESC = dtOtherDia.Rows[i]["DIAGNOSISDESC"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_intCONDITIONSEQ = Convert.ToInt32(dtOtherDia.Rows[i]["CONDITIONSEQ"].ToString());
                        objCollection.m_objOtherDiagnosisArr[i].m_strSTATCODE = dtOtherDia.Rows[i]["STATCODE"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strICD10 = dtOtherDia.Rows[i]["ICD10"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strREGISTERID_CHR = dtOtherDia.Rows[i]["REGISTERID_CHR"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strSEQID = dtOtherDia.Rows[i]["SEQID"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strOTHERCONDITION = dtOtherDia.Rows[i]["OTHERCONDITION"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_lngEMR_SEQ = m_lngEMR_Seq;
                    }
                }
                #endregion

                #region 获取手术信息
                IDataParameter[] objOprationArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objOprationArr);
                objOprationArr[0].Value = m_lngEMR_Seq;
                objOprationArr[1].Value = m_lngEMR_Seq;
                objOprationArr[2].Value = m_lngEMR_Seq;
                objOprationArr[3].Value = m_lngEMR_Seq;
                objOprationArr[4].Value = m_lngEMR_Seq;

                DataTable dtOperation = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetOperationInfo, ref dtOperation, objOprationArr);

                if (lngRes > 0 && dtOperation.Rows.Count > 0)
                {
                    objCollection.m_objOperationArr = new clsInHospitalMainRecord_GXOperation[dtOperation.Rows.Count];
                    for (int j = 0; j < dtOperation.Rows.Count; j++)
                    {
                        objCollection.m_objOperationArr[j] = new clsInHospitalMainRecord_GXOperation();
                        objCollection.m_objOperationArr[j].m_strInPatientID = dtOperation.Rows[j]["INPATIENTID"].ToString();
                        objCollection.m_objOperationArr[j].m_dtmInPatientDate = Convert.ToDateTime(dtOperation.Rows[j]["INPATIENTDATE"]);
                        objCollection.m_objOperationArr[j].m_dtmOpenDate = Convert.ToDateTime(dtOperation.Rows[j]["OPENDATE"]);
                        objCollection.m_objOperationArr[j].m_dtmModifyDate = Convert.ToDateTime(dtOperation.Rows[j]["LASTMODIFYDATE"]);
                        objCollection.m_objOperationArr[j].m_strModifyUserID = dtOperation.Rows[j]["LASTMODIFYUSERID"].ToString();
                        if (dtOperation.Rows[j]["STATUS"].ToString() == "")
                            objCollection.m_objOperationArr[j].m_bytStatus = 0;
                        else objCollection.m_objOperationArr[j].m_bytStatus = Byte.Parse(dtOperation.Rows[j]["STATUS"].ToString());
                        objCollection.m_objOperationArr[j].m_strSEQID = dtOperation.Rows[j]["SEQID"].ToString();
                        objCollection.m_objOperationArr[j].m_dtmOPERATIONDATE = Convert.ToDateTime(dtOperation.Rows[j]["OPERATIONDATE"]);
                        objCollection.m_objOperationArr[j].m_strOPERATIONNAME = dtOperation.Rows[j]["OPERATIONNAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATOR = dtOperation.Rows[j]["OPERATOR"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATORNAME = dtOperation.Rows[j]["OPERATORNAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT1 = dtOperation.Rows[j]["ASSISTANT1"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT1NAME = dtOperation.Rows[j]["ASSISTANT1NAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT2 = dtOperation.Rows[j]["ASSISTANT2"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT2NAME = dtOperation.Rows[j]["ASSISTANT2NAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strAANAESTHESIAMODEID = dtOperation.Rows[j]["AANAESTHESIAMODEID"].ToString();
                        objCollection.m_objOperationArr[j].m_strCUTLEVEL = dtOperation.Rows[j]["CUTLEVEL"].ToString();
                        objCollection.m_objOperationArr[j].m_strANAESTHETIST = dtOperation.Rows[j]["ANAESTHETIST"].ToString();
                        objCollection.m_objOperationArr[j].m_strANAESTHETISTNAME = dtOperation.Rows[j]["ANAESTHETISTNAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATIONAANAESTHESIAMODENAME = dtOperation.Rows[j]["OPERATIONAANAESTHESIAMODENAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strREGISTERID_CHR = dtOperation.Rows[j]["REGISTERID_CHR"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATIONID = dtOperation.Rows[j]["OPERATIONID"].ToString();
                        objCollection.m_objOperationArr[j].m_lngEMR_SEQ = m_lngEMR_Seq;
                    }
                }
                #endregion
                p_objCollection = objCollection;
            }
            catch (Exception objEx)
            {
                
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }            //返回
            return lngRes;
        }
        #endregion

        #region 获取已删除记录
        /// <summary>
        /// 获取已删除记录
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strOpenDate">创建时间</param>
        /// <param name="p_objCollection">病案记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDelInfo(
            string p_strRegisterID,
            string p_strOpenDate,
            out clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            long lngRes = 0;
            p_objCollection = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngGetInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                clsInHospitalMainRecord_GX_Collection objCollection = new clsInHospitalMainRecord_GX_Collection();
                long m_lngEMR_Seq = 0;

                #region 获取主表及子表内容
                IDataParameter[] objMainArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objMainArr);
                objMainArr[0].Value = p_strRegisterID.Trim();
                objMainArr[1].DbType = DbType.DateTime;
                objMainArr[1].Value = DateTime.Parse(p_strOpenDate);

                DataTable dtMain = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDelMainInfo, ref dtMain, objMainArr);

                if (lngRes <= 0 || dtMain.Rows.Count <= 0)
                    return -1;
                else
                {
                    objCollection.m_objMain = new clsInHospitalMainRecord_GX();
                    objCollection.m_objMain.m_strInPatientID = dtMain.Rows[0]["INPATIENTID"].ToString();
                    objCollection.m_objMain.m_dtmInPatientDate = Convert.ToDateTime(dtMain.Rows[0]["INPATIENTDATE"]);
                    objCollection.m_objMain.m_dtmOpenDate = Convert.ToDateTime(dtMain.Rows[0]["OPENDATE"]);
                    objCollection.m_objMain.m_strCreateUserID = dtMain.Rows[0]["CREATEUSERID"].ToString();
                    if (dtMain.Rows[0]["STATUS"].ToString() == "")
                        objCollection.m_objMain.m_bytStatus = 0;
                    else objCollection.m_objMain.m_bytStatus = Byte.Parse(dtMain.Rows[0]["STATUS"].ToString());
                    if (dtMain.Rows[0]["FIRSTPRINTDATE"].ToString() == "")
                        objCollection.m_objMain.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objCollection.m_objMain.m_dtmFirstPrintDate = Convert.ToDateTime(dtMain.Rows[0]["FIRSTPRINTDATE"]);
                    objCollection.m_objMain.m_dtmDeActivedDate = Convert.ToDateTime(dtMain.Rows[0]["DEACTIVEDDATE"]);
                    objCollection.m_objMain.m_strDeActivedOperatorID = dtMain.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    objCollection.m_objMain.m_strDIAGNOSISXML = dtMain.Rows[0]["DIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strINHOSPITALDIAGNOSISXML = dtMain.Rows[0]["INHOSPITALDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strMAINDIAGNOSISXML = dtMain.Rows[0]["MAINDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strCOMPLICATIONXML = dtMain.Rows[0]["COMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strINFECTIONDIAGNOSISXML = dtMain.Rows[0]["INFECTIONDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strPATHOLOGYDIAGNOSISXML = dtMain.Rows[0]["PATHOLOGYDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFMAINXML = dtMain.Rows[0]["ICD_10OFMAINXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFINFECTIONXML = dtMain.Rows[0]["ICD_10OFINFECTIONXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFCOMPLICATIONXML = dtMain.Rows[0]["ICD_10OFCOMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFDIAGNOSISXML = dtMain.Rows[0]["ICD_10OFDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFINHOSPITALDIAXML = dtMain.Rows[0]["ICD_10OFINHOSPITALDIAXML"].ToString();
                    objCollection.m_objMain.m_strICD_10OFPATHOLOGYDIAXML = dtMain.Rows[0]["ICD_10OFPATHOLOGYDIAXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFMAINXML = dtMain.Rows[0]["STATCODEOFMAINXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFINFECTIONXML = dtMain.Rows[0]["STATCODEOFINFECTIONXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFPATHOLOGYDIAXML = dtMain.Rows[0]["STATCODEOFPATHOLOGYDIAXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFDIAGNOSISXML = dtMain.Rows[0]["STATCODEOFDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFINHOSPITALDIAXML = dtMain.Rows[0]["STATCODEOFINHOSPITALDIAXML"].ToString();
                    objCollection.m_objMain.m_strSTATCODEOFCOMPLICATIONXML = dtMain.Rows[0]["STATCODEOFCOMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strSCACHESOURCEXML = dtMain.Rows[0]["SCACHESOURCEXML"].ToString();
                    objCollection.m_objMain.m_strSENSITIVEXML = dtMain.Rows[0]["SENSITIVEXML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE1XML = dtMain.Rows[0]["NEONATEDISEASE1XML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE2XML = dtMain.Rows[0]["NEONATEDISEASE2XML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE3XML = dtMain.Rows[0]["NEONATEDISEASE3XML"].ToString();
                    objCollection.m_objMain.m_strNEONATEDISEASE4XML = dtMain.Rows[0]["NEONATEDISEASE4XML"].ToString();
                    objCollection.m_objMain.m_strSALVETIMESXML = dtMain.Rows[0]["SALVETIMESXML"].ToString();
                    objCollection.m_objMain.m_strSALVESUCCESSXML = dtMain.Rows[0]["SALVESUCCESSXML"].ToString();
                    objCollection.m_objMain.m_strREMINDTERMXML = dtMain.Rows[0]["REMINDTERMXML"].ToString();
                    objCollection.m_objMain.m_strRBCXML = dtMain.Rows[0]["RBCXML"].ToString();
                    objCollection.m_objMain.m_strPLTXML = dtMain.Rows[0]["PLTXML"].ToString();
                    objCollection.m_objMain.m_strPLASMXML = dtMain.Rows[0]["PLASMXML"].ToString();
                    objCollection.m_objMain.m_strWHOLEBLOODXML = dtMain.Rows[0]["WHOLEBLOODXML"].ToString();
                    objCollection.m_objMain.m_strOTHERBLOODXML = dtMain.Rows[0]["OTHERBLOODXML"].ToString();
                    objCollection.m_objMain.m_strREGISTERID_CHR = dtMain.Rows[0]["REGISTERID_CHR"].ToString();
                    objCollection.m_objMain.m_strOTHERMAINCONDITIONXML = dtMain.Rows[0]["OTHERMAINCONDITIONXML"].ToString();
                    objCollection.m_objMain.m_strOTHERCOMPLICATIONXML = dtMain.Rows[0]["OTHERCOMPLICATIONXML"].ToString();
                    objCollection.m_objMain.m_strOTHERINFECTIONCONDICTIONXML = dtMain.Rows[0]["OTHERINFECTIONCONDICTIONXML"].ToString();
                    objCollection.m_objMain.m_strOTHERPATHOLOGYDIAGNOSISXML = dtMain.Rows[0]["OTHERPATHOLOGYDIAGNOSISXML"].ToString();
                    objCollection.m_objMain.m_lngEMR_SEQ = Convert.ToInt64(dtMain.Rows[0]["EMR_SEQ"]);
                    if (dtMain.Rows[0]["SUBMIT_INT"] == DBNull.Value)
                        objCollection.m_objMain.m_intSUBMIT_INT = 0;
                    else
                        objCollection.m_objMain.m_intSUBMIT_INT = Convert.ToInt32(dtMain.Rows[0]["SUBMIT_INT"].ToString());
                    m_lngEMR_Seq = Convert.ToInt64(dtMain.Rows[0]["EMR_SEQ"]);
                }

                IDataParameter[] objContentArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objContentArr);
                objContentArr[0].Value = m_lngEMR_Seq;

                DataTable dtContent = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDelContentInfo, ref dtContent, objContentArr);

                if (lngRes <= 0 || dtContent.Rows.Count <= 0)
                    return -1;
                else
                {
                    objCollection.m_objContent = new clsInHospitalMainRecord_GXContent();
                    objCollection.m_objContent.m_strInPatientID = dtContent.Rows[0]["INPATIENTID"].ToString();
                    objCollection.m_objContent.m_dtmInPatientDate = Convert.ToDateTime(dtContent.Rows[0]["INPATIENTDATE"]);
                    objCollection.m_objContent.m_dtmOpenDate = Convert.ToDateTime(dtContent.Rows[0]["OPENDATE"]);
                    objCollection.m_objContent.m_dtmModifyDate = Convert.ToDateTime(dtContent.Rows[0]["LASTMODIFYDATE"]);
                    objCollection.m_objContent.m_strModifyUserID = dtContent.Rows[0]["LASTMODIFYUSERID"].ToString();
                    if (dtContent.Rows[0]["STATUS"].ToString() == "")
                        objCollection.m_objContent.m_bytStatus = 0;
                    else objCollection.m_objContent.m_bytStatus = Byte.Parse(dtContent.Rows[0]["STATUS"].ToString());
                    objCollection.m_objContent.m_dtmDeActivedDate = Convert.ToDateTime(dtContent.Rows[0]["DEACTIVEDDATE"]);
                    objCollection.m_objContent.m_strDeActivedOperatorID = dtContent.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    objCollection.m_objContent.m_intCONDICTIONWHENIN = Convert.ToInt32(dtContent.Rows[0]["CONDICTIONWHENIN"].ToString());
                    objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE = Convert.ToDateTime(dtContent.Rows[0]["CONFIRMDIAGNOSISDATE"]);
                    objCollection.m_objContent.m_strDIAGNOSIS = dtContent.Rows[0]["DIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strINHOSPITALDIAGNOSIS = dtContent.Rows[0]["INHOSPITALDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strMAINDIAGNOSIS = dtContent.Rows[0]["MAINDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strCOMPLICATION = dtContent.Rows[0]["COMPLICATION"].ToString();
                    objCollection.m_objContent.m_strINFECTIONDIAGNOSIS = dtContent.Rows[0]["INFECTIONDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strPATHOLOGYDIAGNOSIS = dtContent.Rows[0]["PATHOLOGYDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strICD_10OFMAIN = dtContent.Rows[0]["ICD_10OFMAIN"].ToString();
                    objCollection.m_objContent.m_strICD_10OFINFECTION = dtContent.Rows[0]["ICD_10OFINFECTION"].ToString();
                    objCollection.m_objContent.m_strICD_10OFCOMPLICATION = dtContent.Rows[0]["ICD_10OFCOMPLICATION"].ToString();
                    objCollection.m_objContent.m_strICD_10OFDIAGNOSIS = dtContent.Rows[0]["ICD_10OFDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strICD_10OFINHOSPITALDIA = dtContent.Rows[0]["ICD_10OFINHOSPITALDIA"].ToString();
                    objCollection.m_objContent.m_strICD_10OFPATHOLOGYDIA = dtContent.Rows[0]["ICD_10OFPATHOLOGYDIA"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFMAIN = dtContent.Rows[0]["STATCODEOFMAIN"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFINFECTION = dtContent.Rows[0]["STATCODEOFINFECTION"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFPATHOLOGYDIA = dtContent.Rows[0]["STATCODEOFPATHOLOGYDIA"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFDIAGNOSIS = dtContent.Rows[0]["STATCODEOFDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFINHOSPITALDIA = dtContent.Rows[0]["STATCODEOFINHOSPITALDIA"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFCOMPLICATION = dtContent.Rows[0]["STATCODEOFCOMPLICATION"].ToString();
                    objCollection.m_objContent.m_intMAINCONDITIONSEQ = dtContent.Rows[0]["MAINCONDITIONSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["MAINCONDITIONSEQ"].ToString());
                    objCollection.m_objContent.m_intCOMPLICATIONSEQ = dtContent.Rows[0]["COMPLICATIONSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["COMPLICATIONSEQ"].ToString());
                    objCollection.m_objContent.m_intINFECTIONCONDICTIONSEQ = dtContent.Rows[0]["INFECTIONCONDICTIONSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["INFECTIONCONDICTIONSEQ"].ToString());
                    objCollection.m_objContent.m_intPATHOLOGYDIAGNOSISSEQ = dtContent.Rows[0]["PATHOLOGYDIAGNOSISSEQ"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["PATHOLOGYDIAGNOSISSEQ"].ToString());
                    objCollection.m_objContent.m_strSCACHESOURCE = dtContent.Rows[0]["SCACHESOURCE"].ToString();
                    objCollection.m_objContent.m_intNEW5DISEASE = Convert.ToInt32(dtContent.Rows[0]["NEW5DISEASE"].ToString());
                    objCollection.m_objContent.m_intSECONDLEVELTRANSFER = Convert.ToInt32(dtContent.Rows[0]["SECONDLEVELTRANSFER"].ToString());
                    objCollection.m_objContent.m_strSENSITIVE = dtContent.Rows[0]["SENSITIVE"].ToString();
                    objCollection.m_objContent.m_intHBSAG = Convert.ToInt32(dtContent.Rows[0]["HBSAG"].ToString());
                    objCollection.m_objContent.m_intHCV_AB = Convert.ToInt32(dtContent.Rows[0]["HCV_AB"].ToString());
                    objCollection.m_objContent.m_intHIV_AB = Convert.ToInt32(dtContent.Rows[0]["HIV_AB"].ToString());
                    objCollection.m_objContent.m_strNEONATEDISEASE1 = dtContent.Rows[0]["NEONATEDISEASE1"].ToString();
                    objCollection.m_objContent.m_strNEONATEDISEASE2 = dtContent.Rows[0]["NEONATEDISEASE2"].ToString();
                    objCollection.m_objContent.m_strNEONATEDISEASE3 = dtContent.Rows[0]["NEONATEDISEASE3"].ToString();
                    objCollection.m_objContent.m_strNEONATEDISEASE4 = dtContent.Rows[0]["NEONATEDISEASE4"].ToString();
                    objCollection.m_objContent.m_intSALVETIMES = Convert.ToInt32(dtContent.Rows[0]["SALVETIMES"].ToString());
                    objCollection.m_objContent.m_intSALVESUCCESS = Convert.ToInt32(dtContent.Rows[0]["SALVESUCCESS"].ToString());
                    objCollection.m_objContent.m_intHASREMIND = Convert.ToInt32(dtContent.Rows[0]["HASREMIND"].ToString());
                    objCollection.m_objContent.m_strREMINDTERM = dtContent.Rows[0]["REMINDTERM"].ToString();
                    objCollection.m_objContent.m_intACCORDWITHOUTHOSPITAL = Convert.ToInt32(dtContent.Rows[0]["ACCORDWITHOUTHOSPITAL"].ToString());
                    objCollection.m_objContent.m_intACCORDINWITHOUT = Convert.ToInt32(dtContent.Rows[0]["ACCORDINWITHOUT"].ToString());
                    objCollection.m_objContent.m_intACCORDBFOPRWITHAF = Convert.ToInt32(dtContent.Rows[0]["ACCORDBFOPRWITHAF"].ToString());
                    objCollection.m_objContent.m_intACCORDCLINICWITHPATHOLOGY = Convert.ToInt32(dtContent.Rows[0]["ACCORDCLINICWITHPATHOLOGY"].ToString());
                    objCollection.m_objContent.m_intACCORDCLINICWITHRADIATE = Convert.ToInt32(dtContent.Rows[0]["ACCORDCLINICWITHRADIATE"].ToString());
                    objCollection.m_objContent.m_intACCORDDEATHWITHBODYCHECK = Convert.ToInt32(dtContent.Rows[0]["ACCORDDEATHWITHBODYCHECK"].ToString());
                    objCollection.m_objContent.m_intFIRSTCASE = Convert.ToInt32(dtContent.Rows[0]["FIRSTCASE"].ToString());
                    objCollection.m_objContent.m_intMODELCASE = Convert.ToInt32(dtContent.Rows[0]["MODELCASE"].ToString());
                    objCollection.m_objContent.m_intQUALITY = Convert.ToInt32(dtContent.Rows[0]["QUALITY"].ToString());
                    objCollection.m_objContent.m_intANTIBACTERIAL = Convert.ToInt32(dtContent.Rows[0]["ANTIBACTERIAL"].ToString());
                    objCollection.m_objContent.m_intPATHOGENY = Convert.ToInt32(dtContent.Rows[0]["PATHOGENY"].ToString());
                    objCollection.m_objContent.m_intPATHOGENYRESULT = Convert.ToInt32(dtContent.Rows[0]["PATHOGENYRESULT"].ToString());
                    objCollection.m_objContent.m_intBLOODTRANSACTOIN = Convert.ToInt32(dtContent.Rows[0]["BLOODTRANSACTOIN"].ToString());
                    objCollection.m_objContent.m_intTRANSFUSIONSACTION = Convert.ToInt32(dtContent.Rows[0]["TRANSFUSIONSACTION"].ToString());
                    objCollection.m_objContent.m_intCTCHECK = Convert.ToInt32(dtContent.Rows[0]["CTCHECK"].ToString());
                    objCollection.m_objContent.m_intMRICHECK = Convert.ToInt32(dtContent.Rows[0]["MRICHECK"].ToString());
                    objCollection.m_objContent.m_intBLOODTYPE = dtContent.Rows[0]["BLOODTYPE"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["BLOODTYPE"].ToString());
                    objCollection.m_objContent.m_intBLOODRH = dtContent.Rows[0]["BLOODRH"] == DBNull.Value ? -1 : Convert.ToInt32(dtContent.Rows[0]["BLOODRH"].ToString());
                    objCollection.m_objContent.m_strRBC = dtContent.Rows[0]["RBC"].ToString();
                    objCollection.m_objContent.m_strPLT = dtContent.Rows[0]["PLT"].ToString();
                    objCollection.m_objContent.m_strPLASM = dtContent.Rows[0]["PLASM"].ToString();
                    objCollection.m_objContent.m_strWHOLEBLOOD = dtContent.Rows[0]["WHOLEBLOOD"].ToString();
                    objCollection.m_objContent.m_strOTHERBLOOD = dtContent.Rows[0]["OTHERBLOOD"].ToString();
                    objCollection.m_objContent.m_strDEPTDIRECTORDT = dtContent.Rows[0]["DEPTDIRECTORDT"].ToString();
                    objCollection.m_objContent.m_strDEPTDIRECTORDTNAME = dtContent.Rows[0]["DEPTDIRECTORDTNAME"].ToString();
                    objCollection.m_objContent.m_strDT = dtContent.Rows[0]["DT"].ToString();
                    objCollection.m_objContent.m_strDTNAME = dtContent.Rows[0]["DTNAME"].ToString();
                    objCollection.m_objContent.m_strINHOSPITALDOC = dtContent.Rows[0]["INHOSPITALDOC"].ToString();
                    objCollection.m_objContent.m_strINHOSPITALDOCNAME = dtContent.Rows[0]["INHOSPITALDOCNAME"].ToString();
                    objCollection.m_objContent.m_strOUTHOSPITALDOC = dtContent.Rows[0]["OUTHOSPITALDOC"].ToString();
                    objCollection.m_objContent.m_strOUTHOSPITALDOCNAME = dtContent.Rows[0]["OUTHOSPITALDOCNAME"].ToString();
                    objCollection.m_objContent.m_strDIRECTORDT = dtContent.Rows[0]["DIRECTORDT"].ToString();
                    objCollection.m_objContent.m_strDIRECTORDTNAME = dtContent.Rows[0]["DIRECTORDTNAME"].ToString();
                    objCollection.m_objContent.m_strSUBDIRECTORDT = dtContent.Rows[0]["SUBDIRECTORDT"].ToString();
                    objCollection.m_objContent.m_strSUBDIRECTORDTNAME = dtContent.Rows[0]["SUBDIRECTORDTNAME"].ToString();
                    objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDT = dtContent.Rows[0]["ATTENDINFORADVANCESSTUDYDT"].ToString();
                    objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDTNAME = dtContent.Rows[0]["ADVANCEDSTUDIESDTNAME"].ToString();
                    objCollection.m_objContent.m_strGRADUATESTUDENTINTERNNAME = dtContent.Rows[0]["GRADUATESTUDENTINTERN"].ToString();
                    objCollection.m_objContent.m_strINTERNNAME = dtContent.Rows[0]["INTERN"].ToString();
                    objCollection.m_objContent.m_dblTOTALAMT = Convert.ToDouble(dtContent.Rows[0]["TOTALAMT"]);
                    objCollection.m_objContent.m_dblBEDAMT = Convert.ToDouble(dtContent.Rows[0]["BEDAMT"]);
                    objCollection.m_objContent.m_dblNURSEAMT = Convert.ToDouble(dtContent.Rows[0]["NURSEAMT"]);
                    objCollection.m_objContent.m_dblWMAMT = Convert.ToDouble(dtContent.Rows[0]["WMAMT"]);
                    objCollection.m_objContent.m_dblCMFINISHEDAMT = Convert.ToDouble(dtContent.Rows[0]["CMFINISHEDAMT"]);
                    objCollection.m_objContent.m_dblCMSEMIFINISHEDAMT = Convert.ToDouble(dtContent.Rows[0]["CMSEMIFINISHEDAMT"]);
                    objCollection.m_objContent.m_dblRADIATIONAMT = Convert.ToDouble(dtContent.Rows[0]["RADIATIONAMT"]);
                    objCollection.m_objContent.m_dblASSAYAMT = Convert.ToDouble(dtContent.Rows[0]["ASSAYAMT"]);
                    objCollection.m_objContent.m_dblO2AMT = Convert.ToDouble(dtContent.Rows[0]["O2AMT"]);
                    objCollection.m_objContent.m_dblBLOODAMT = Convert.ToDouble(dtContent.Rows[0]["BLOODAMT"]);
                    objCollection.m_objContent.m_dblTREATMENTAMT = Convert.ToDouble(dtContent.Rows[0]["TREATMENTAMT"]);
                    objCollection.m_objContent.m_dblOPERATIONAMT = Convert.ToDouble(dtContent.Rows[0]["OPERATIONAMT"]);
                    objCollection.m_objContent.m_dblCHECKAMT = Convert.ToDouble(dtContent.Rows[0]["CHECKAMT"]);
                    objCollection.m_objContent.m_dblANAETHESIAAMT = Convert.ToDouble(dtContent.Rows[0]["ANAETHESIAAMT"]);
                    objCollection.m_objContent.m_dblDELIVERYCHILDAMT = Convert.ToDouble(dtContent.Rows[0]["DELIVERYCHILDAMT"]);
                    objCollection.m_objContent.m_dblBABYAMT = Convert.ToDouble(dtContent.Rows[0]["BABYAMT"]);
                    objCollection.m_objContent.m_dblACCOMPANYAMT = Convert.ToDouble(dtContent.Rows[0]["ACCOMPANYAMT"]);
                    objCollection.m_objContent.m_dblOTHERAMT = Convert.ToDouble(dtContent.Rows[0]["OTHERAMT"]);
                    objCollection.m_objContent.m_strREGISTERID_CHR = dtContent.Rows[0]["REGISTERID_CHR"].ToString();
                    objCollection.m_objContent.m_strNEATENNAME = dtContent.Rows[0]["NEATENNAME"].ToString();
                    objCollection.m_objContent.m_strCODINGNAME = dtContent.Rows[0]["CODINGNAME"].ToString();
                    objCollection.m_objContent.m_strINPUTMACHINENAME = dtContent.Rows[0]["INPUTMACHINENAME"].ToString();
                    objCollection.m_objContent.m_strSTATISTICNAME = dtContent.Rows[0]["STATISTICNAME"].ToString();
                    objCollection.m_objContent.m_strOTHERMAINCONDITION = dtContent.Rows[0]["OTHERMAINCONDITION"].ToString();
                    objCollection.m_objContent.m_strOTHERCOMPLICATION = dtContent.Rows[0]["OTHERCOMPLICATION"].ToString();
                    objCollection.m_objContent.m_strOTHERINFECTIONCONDICTION = dtContent.Rows[0]["OTHERINFECTIONCONDICTION"].ToString();
                    objCollection.m_objContent.m_strOTHERPATHOLOGYDIAGNOSIS = dtContent.Rows[0]["OTHERPATHOLOGYDIAGNOSIS"].ToString();
                    objCollection.m_objContent.m_strPAYTYPE = dtContent.Rows[0]["PAYTYPE"].ToString();
                    objCollection.m_objContent.m_intXRAYCHECK = Convert.ToInt32(dtContent.Rows[0]["XRAYCHECK"].ToString());
                    objCollection.m_objContent.m_strSTATCODEOFSCACHESOURCE = dtContent.Rows[0]["STATCODEOFSCACHESOURCE"].ToString();
                    objCollection.m_objContent.m_strICD_10OFSCACHESOURCE = dtContent.Rows[0]["ICD_10OFSCACHESOURCE"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE1 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE1"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE2 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE2"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE3 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE3"].ToString();
                    objCollection.m_objContent.m_strSTATCODEOFNEONATEDISEASE4 = dtContent.Rows[0]["STATCODEOFNEONATEDISEASE4"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE1 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE1"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE2 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE2"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE3 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE3"].ToString();
                    objCollection.m_objContent.m_strICD_10OFNEONATEDISEASE4 = dtContent.Rows[0]["ICD_10OFNEONATEDISEASE4"].ToString();
                    if (dtContent.Rows[0]["REMINDTERMTYPE"] == DBNull.Value)
                        objCollection.m_objContent.m_intREMINDTERMTYPE = 0;
                    else
                        objCollection.m_objContent.m_intREMINDTERMTYPE = Convert.ToInt32(dtContent.Rows[0]["REMINDTERMTYPE"].ToString());
                    if (dtContent.Rows[0]["CATALOG_DATE"] == DBNull.Value)
                        objCollection.m_objContent.m_dtmCATALOG_DATE = DateTime.MinValue;
                    else
                        objCollection.m_objContent.m_dtmCATALOG_DATE = Convert.ToDateTime(dtContent.Rows[0]["CATALOG_DATE"]);
                    //if (dtContent.Rows[0]["ATTENDINFORADVANCESSTUDYDT"] == DBNull.Value)
                    //{
                    //    objCollection.m_objContent.m_strATTENDINFORADVANCESSTUDYDTNAME = dtContent.Rows[0]["ADVANCEDSTUDIESDTNAME"].ToString();
                    //}
                    objCollection.m_objContent.m_lngEMR_SEQ = m_lngEMR_Seq;
                }
                #endregion

                #region 获取入院诊断内容
                IDataParameter[] objInDiaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objInDiaArr);
                objInDiaArr[0].Value = m_lngEMR_Seq;

                DataTable dtInDia = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetInDiagnosisInfo, ref dtInDia, objInDiaArr);

                if (lngRes > 0 && dtInDia.Rows.Count > 0)
                {
                    objCollection.m_objInDiagnosisArr = new clsInHospitalMainRecord_GXInDiagnosis[dtInDia.Rows.Count];
                    for (int i = 0; i < dtInDia.Rows.Count; i++)
                    {
                        objCollection.m_objInDiagnosisArr[i] = new clsInHospitalMainRecord_GXInDiagnosis();
                        objCollection.m_objInDiagnosisArr[i].m_strInPatientID = dtInDia.Rows[i]["INPATIENTID"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_dtmInPatientDate = Convert.ToDateTime(dtInDia.Rows[i]["INPATIENTDATE"]);
                        objCollection.m_objInDiagnosisArr[i].m_dtmOpenDate = Convert.ToDateTime(dtInDia.Rows[i]["OPENDATE"]);
                        objCollection.m_objInDiagnosisArr[i].m_dtmModifyDate = Convert.ToDateTime(dtInDia.Rows[i]["LASTMODIFYDATE"]);
                        objCollection.m_objInDiagnosisArr[i].m_strModifyUserID = dtInDia.Rows[i]["LASTMODIFYUSERID"].ToString();
                        if (dtInDia.Rows[i]["STATUS"].ToString() == "")
                            objCollection.m_objInDiagnosisArr[i].m_bytStatus = 0;
                        else objCollection.m_objInDiagnosisArr[i].m_bytStatus = Byte.Parse(dtInDia.Rows[i]["STATUS"].ToString());
                        objCollection.m_objInDiagnosisArr[i].m_strDIAGNOSISDESC = dtInDia.Rows[i]["DIAGNOSISDESC"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strSTATCODE = dtInDia.Rows[i]["STATCODE"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strICD10 = dtInDia.Rows[i]["ICD10"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strREGISTERID_CHR = dtInDia.Rows[i]["REGISTERID_CHR"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_strSEQID = dtInDia.Rows[i]["SEQID"].ToString();
                        objCollection.m_objInDiagnosisArr[i].m_lngEMR_SEQ = m_lngEMR_Seq;
                    }
                }
                #endregion

                #region 获取其他诊断内容
                IDataParameter[] objOtherDiaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objOtherDiaArr);
                objOtherDiaArr[0].Value = m_lngEMR_Seq;

                DataTable dtOtherDia = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDelOtherDiagnosisInfo, ref dtOtherDia, objOtherDiaArr);

                if (lngRes > 0 && dtOtherDia.Rows.Count > 0)
                {
                    objCollection.m_objOtherDiagnosisArr = new clsInHospitalMainRecord_GXOtherDiagnose[dtOtherDia.Rows.Count];
                    for (int i = 0; i < dtOtherDia.Rows.Count; i++)
                    {
                        objCollection.m_objOtherDiagnosisArr[i] = new clsInHospitalMainRecord_GXOtherDiagnose();
                        objCollection.m_objOtherDiagnosisArr[i].m_strInPatientID = dtOtherDia.Rows[i]["INPATIENTID"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmInPatientDate = Convert.ToDateTime(dtOtherDia.Rows[i]["INPATIENTDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmOpenDate = Convert.ToDateTime(dtOtherDia.Rows[i]["OPENDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmModifyDate = Convert.ToDateTime(dtOtherDia.Rows[i]["LASTMODIFYDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_strModifyUserID = dtOtherDia.Rows[i]["LASTMODIFYUSERID"].ToString();
                        if (dtOtherDia.Rows[i]["STATUS"].ToString() == "")
                            objCollection.m_objOtherDiagnosisArr[i].m_bytStatus = 0;
                        else objCollection.m_objOtherDiagnosisArr[i].m_bytStatus = Byte.Parse(dtOtherDia.Rows[i]["STATUS"].ToString());
                        objCollection.m_objOtherDiagnosisArr[i].m_dtmDeActivedDate = Convert.ToDateTime(dtOtherDia.Rows[0]["DEACTIVEDDATE"]);
                        objCollection.m_objOtherDiagnosisArr[i].m_strDeActivedOperatorID = dtOtherDia.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strDIAGNOSISDESC = dtOtherDia.Rows[i]["DIAGNOSISDESC"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_intCONDITIONSEQ = Convert.ToInt32(dtOtherDia.Rows[i]["CONDITIONSEQ"].ToString());
                        objCollection.m_objOtherDiagnosisArr[i].m_strSTATCODE = dtOtherDia.Rows[i]["STATCODE"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strICD10 = dtOtherDia.Rows[i]["ICD10"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strREGISTERID_CHR = dtOtherDia.Rows[i]["REGISTERID_CHR"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strSEQID = dtOtherDia.Rows[i]["SEQID"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_strOTHERCONDITION = dtOtherDia.Rows[i]["OTHERCONDITION"].ToString();
                        objCollection.m_objOtherDiagnosisArr[i].m_lngEMR_SEQ = m_lngEMR_Seq;
                    }
                }
                #endregion

                #region 获取手术信息
                IDataParameter[] objOprationArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objOprationArr);
                objOprationArr[0].Value = m_lngEMR_Seq;
                objOprationArr[1].Value = m_lngEMR_Seq;
                objOprationArr[2].Value = m_lngEMR_Seq;
                objOprationArr[3].Value = m_lngEMR_Seq;
                objOprationArr[4].Value = m_lngEMR_Seq;

                DataTable dtOperation = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDelOperationInfo, ref dtOperation, objOprationArr);

                if (lngRes > 0 && dtOperation.Rows.Count > 0)
                {
                    objCollection.m_objOperationArr = new clsInHospitalMainRecord_GXOperation[dtOperation.Rows.Count];
                    for (int j = 0; j < dtOperation.Rows.Count; j++)
                    {
                        objCollection.m_objOperationArr[j] = new clsInHospitalMainRecord_GXOperation();
                        objCollection.m_objOperationArr[j].m_strInPatientID = dtOperation.Rows[j]["INPATIENTID"].ToString();
                        objCollection.m_objOperationArr[j].m_dtmInPatientDate = Convert.ToDateTime(dtOperation.Rows[j]["INPATIENTDATE"]);
                        objCollection.m_objOperationArr[j].m_dtmOpenDate = Convert.ToDateTime(dtOperation.Rows[j]["OPENDATE"]);
                        objCollection.m_objOperationArr[j].m_dtmModifyDate = Convert.ToDateTime(dtOperation.Rows[j]["LASTMODIFYDATE"]);
                        objCollection.m_objOperationArr[j].m_strModifyUserID = dtOperation.Rows[j]["LASTMODIFYUSERID"].ToString();
                        if (dtOperation.Rows[j]["STATUS"].ToString() == "")
                            objCollection.m_objOperationArr[j].m_bytStatus = 0;
                        else objCollection.m_objOperationArr[j].m_bytStatus = Byte.Parse(dtOperation.Rows[j]["STATUS"].ToString());
                        objCollection.m_objOperationArr[j].m_dtmDeActivedDate = Convert.ToDateTime(dtOperation.Rows[0]["DEACTIVEDDATE"]);
                        objCollection.m_objOperationArr[j].m_strDeActivedOperatorID = dtOperation.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        objCollection.m_objOperationArr[j].m_strSEQID = dtOperation.Rows[j]["SEQID"].ToString();
                        objCollection.m_objOperationArr[j].m_dtmOPERATIONDATE = Convert.ToDateTime(dtOperation.Rows[j]["OPERATIONDATE"]);
                        objCollection.m_objOperationArr[j].m_strOPERATIONNAME = dtOperation.Rows[j]["OPERATIONNAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATOR = dtOperation.Rows[j]["OPERATOR"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATORNAME = dtOperation.Rows[j]["OPERATORNAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT1 = dtOperation.Rows[j]["ASSISTANT1"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT1NAME = dtOperation.Rows[j]["ASSISTANT1NAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT2 = dtOperation.Rows[j]["ASSISTANT2"].ToString();
                        objCollection.m_objOperationArr[j].m_strASSISTANT2NAME = dtOperation.Rows[j]["ASSISTANT2NAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strAANAESTHESIAMODEID = dtOperation.Rows[j]["AANAESTHESIAMODEID"].ToString();
                        objCollection.m_objOperationArr[j].m_strCUTLEVEL = dtOperation.Rows[j]["CUTLEVEL"].ToString();
                        objCollection.m_objOperationArr[j].m_strANAESTHETIST = dtOperation.Rows[j]["ANAESTHETIST"].ToString();
                        objCollection.m_objOperationArr[j].m_strANAESTHETISTNAME = dtOperation.Rows[j]["ANAESTHETISTNAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATIONAANAESTHESIAMODENAME = dtOperation.Rows[j]["OPERATIONAANAESTHESIAMODENAME"].ToString();
                        objCollection.m_objOperationArr[j].m_strREGISTERID_CHR = dtOperation.Rows[j]["REGISTERID_CHR"].ToString();
                        objCollection.m_objOperationArr[j].m_strOPERATIONID = dtOperation.Rows[j]["OPERATIONID"].ToString();
                        objCollection.m_objOperationArr[j].m_lngEMR_SEQ = m_lngEMR_Seq;
                    }
                }
                #endregion

                p_objCollection = objCollection;
            }
            catch (Exception objEx)
            {
                
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }            //返回
            return lngRes;
        }
        #endregion

        #region 模糊查询麻醉方式
        /// <summary>
        /// 模糊查询麻醉方式
        /// </summary>
        /// <param name="p_strInput"></param>
        /// <param name="strXML"></param>
        /// <param name="intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAnaesthesiaModeLikeID(
            string p_strInput, out clsAnaesthesiaModeInOperation[] p_objAnaesthesiaModeInOperation)
        {
            p_objAnaesthesiaModeInOperation = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngGetAnaesthesiaModeLikeID");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strInput == null)
                    return -1;

                string strCommand = " select anaesthesiamodeid,anaesthesiamodename from anaesthesiamode where status=0 and anaesthesiamodeid like '" + p_strInput + "%' ";
                DataTable objResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strCommand, ref objResult);

                if (lngRes > 0 && objResult.Rows.Count > 0)
                {
                    p_objAnaesthesiaModeInOperation = new clsAnaesthesiaModeInOperation[objResult.Rows.Count];
                    for (int i = 0; i < objResult.Rows.Count; i++)
                    {
                        p_objAnaesthesiaModeInOperation[i] = new clsAnaesthesiaModeInOperation();
                        p_objAnaesthesiaModeInOperation[i].strAnaesthesiaModeID = objResult.Rows[i]["AnaesthesiaModeID"].ToString();
                        p_objAnaesthesiaModeInOperation[i].strAnaesthesiaModeName = objResult.Rows[i]["AnaesthesiaModeName"].ToString();
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

            }            //返回
            return lngRes;


        }

        #endregion

        #region 查找该表在该条件下是否有重复的记录
        /// <summary>
        /// 查找该表在该条件下是否有重复的记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记表流水号</param>
        /// <param name="p_intRows">记录数目</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordCount(
            string p_strRegisterID, out int p_intRows)
        {
            p_intRows = 0;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ_GX", "m_lngGetRecordCount");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == "")
                    return -1;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                DataTable m_dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordCount, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    p_intRows = int.Parse(m_dtbResult.Rows[0][0].ToString());
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

            }            //返回
            return lngRes;
        }
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_lngEMR_Seq">流水号</param>
        /// <param name="p_strOperatorID">操作者ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord(
            long p_lngEMR_Seq, string p_strOperatorID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strOperatorID == null || p_strOperatorID == "")
                    return -1;

                string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = Convert.ToDateTime(strNow);
                objDPArr[1].Value = p_strOperatorID.Trim();
                objDPArr[2].Value = p_lngEMR_Seq;
                long lngRecEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteMainRecord, ref lngRecEff, objDPArr);

                if (lngRes <= 0)
                    return -1;

                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].DbType = DbType.DateTime;
                objDPArr1[0].Value = Convert.ToDateTime(strNow);
                objDPArr1[1].Value = p_strOperatorID.Trim();
                objDPArr1[2].Value = p_lngEMR_Seq;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteContentSQL, ref lngRecEff, objDPArr1);

                if (lngRes <= 0)
                    return -1;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = Convert.ToDateTime(strNow);
                objDPArr2[1].Value = p_strOperatorID.Trim();
                objDPArr2[2].Value = p_lngEMR_Seq;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOtherDiagnoseSQL, ref lngRecEff, objDPArr2);

                if (lngRes <= 0)
                    return -1;

                IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].DbType = DbType.DateTime;
                objDPArr3[0].Value = Convert.ToDateTime(strNow);
                objDPArr3[1].Value = p_strOperatorID.Trim();
                objDPArr3[2].Value = p_lngEMR_Seq;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteOperationSQL, ref lngRecEff, objDPArr3);

                if (lngRes <= 0)
                    return -1;

                IDataParameter[] objDPArr4 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr4);
                objDPArr4[0].DbType = DbType.DateTime;
                objDPArr4[0].Value = Convert.ToDateTime(strNow);
                objDPArr4[1].Value = p_strOperatorID.Trim();
                objDPArr4[2].Value = p_lngEMR_Seq;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteInDiagnoseSQL, ref lngRecEff, objDPArr4);
            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }            //返回
            return lngRes;
        }
        #endregion

        #region 获得最后删除时间,以及删除人
        /// <summary>
        /// 获得最后删除时间,以及删除人
        /// </summary>
        /// <param name="p_strRegisterID">住院登记流水号</param>
        /// <param name="p_strDeactivedDate">删除时间</param>
        /// <param name="p_strDeactivedUserID">删除者</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeactivedDateAndUser(
            string p_strRegisterID, out DateTime p_dtmDeactivedDate, out string p_strDeactivedUserID)
        {
            p_dtmDeactivedDate = DateTime.MinValue;
            p_strDeactivedUserID = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsInHospitalMainRecordServ", "m_lngGetDeactivedDateAndUser");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strRegisterID == null || p_strRegisterID == "")
                    return -1;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                DataTable m_dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeactivedDateAndUser, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_dtmDeactivedDate = Convert.ToDateTime(m_dtbResult.Rows[0]["DeActivedDate"]);
                    p_strDeactivedUserID = m_dtbResult.Rows[0]["DeactivedOperatorID"].ToString();
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

            }            //返回
            return lngRes;
        }
        #endregion

        #region 获取转科情况及入院、出院科室
        /// <summary>
        /// 获取转科情况及入院、出院科室
        /// </summary>
        /// <param name="m_strRegisterID"></param>
        /// <param name="m_objDeptInstance"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInHospitalMainTransDeptInstance(string p_strRegisterID, out clsInHospitalMainTransDeptInstance p_objDeptInstance)
        {
            long lngRes = 0;
            p_objDeptInstance = new clsInHospitalMainTransDeptInstance();
            if (p_strRegisterID == null || p_strRegisterID.Trim() == "")
                return -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterID.Trim();

                string strSQL = @"select tt.sourcedeptid_chr,
									td1.deptname_vchr as sourcedeptname,
									tt.targetdeptid_chr,
									td2.deptname_vchr as targetdeptname,
									tt.type_int,
									tt.modify_dat as transdeptdate,
									tl.modify_dat as outdate
								from t_opr_bih_register tr
								left outer join t_opr_bih_transfer tt on tr.registerid_chr =
																		tt.registerid_chr and tt.type_int <> 1
								left outer join t_opr_bih_leave tl on tr.registerid_chr =
																		tl.registerid_chr
																	and tl.status_int = 1
								left outer join t_bse_deptdesc td1 on td1.deptid_chr =
																		tt.sourcedeptid_chr
								left outer join t_bse_deptdesc td2 on td2.deptid_chr =
																		tt.targetdeptid_chr
								where tr.registerid_chr = ? and tr.status_int = 1 order by tt.modify_dat";

                DataTable m_dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    int intType = 0;
                    string strSourceDept = "";
                    string strTargetDept = "";
                    DateTime dtmTransDeptDate = DateTime.MinValue;
                    ArrayList arrSourceDeptID = new ArrayList();
                    ArrayList arrTargetDeptID = new ArrayList();
                    ArrayList arrSourceDeptName = new ArrayList();
                    ArrayList arrTargetDeptName = new ArrayList();
                    ArrayList arrTransDeptDate = new ArrayList();
                    for (int i = 0; i < m_dtbResult.Rows.Count; i++)
                    {
                        if (m_dtbResult.Rows[i]["TYPE_INT"] == DBNull.Value)
                            continue;
                        intType = Convert.ToInt32(m_dtbResult.Rows[i]["TYPE_INT"]);
                        if (intType == 3)//转区
                        {
                            strSourceDept = m_dtbResult.Rows[i]["SOURCEDEPTID_CHR"].ToString();
                            strTargetDept = m_dtbResult.Rows[i]["TARGETDEPTID_CHR"].ToString();
                            dtmTransDeptDate = m_dtbResult.Rows[i]["TransDeptDate"] == DBNull.Value ?
                                new DateTime(1900, 1, 1) : Convert.ToDateTime(m_dtbResult.Rows[i]["TransDeptDate"]);
                            if (strSourceDept != null && strTargetDept != null && strSourceDept.Trim() != "" && strTargetDept.Trim() != "")
                            {
                                arrSourceDeptID.Add(strSourceDept);
                                arrTargetDeptID.Add(strTargetDept);
                                arrSourceDeptName.Add(m_dtbResult.Rows[i]["SourceDeptName"].ToString());
                                arrTargetDeptName.Add(m_dtbResult.Rows[i]["TargetDeptName"].ToString());
                                arrTransDeptDate.Add(dtmTransDeptDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else if (intType == 5)//入院
                        {
                            p_objDeptInstance.m_strInPatientDeptID = m_dtbResult.Rows[i]["TARGETDEPTID_CHR"].ToString();
                            p_objDeptInstance.m_strInPatientDeptName = m_dtbResult.Rows[i]["TargetDeptName"].ToString();
                        }
                        else if (intType == 6)//出院
                        {
                            p_objDeptInstance.m_strOutPatientDeptID = m_dtbResult.Rows[i]["SOURCEDEPTID_CHR"].ToString();
                            p_objDeptInstance.m_strOutPatientDeptName = m_dtbResult.Rows[i]["SourceDeptName"].ToString();
                            p_objDeptInstance.m_demOutPatientDate = m_dtbResult.Rows[i]["OutDate"] == DBNull.Value ?
                                new DateTime(1900, 1, 1) : Convert.ToDateTime(m_dtbResult.Rows[i]["OutDate"]);
                        }
                    }
                    if (arrSourceDeptID.Count != 0 && arrTargetDeptID.Count != 0)
                    {
                        p_objDeptInstance.m_strTransSourceDeptIDArr = (string[])arrSourceDeptID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetDeptIDArr = (string[])arrTargetDeptID.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransSourceDeptNameArr = (string[])arrSourceDeptName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransTargetDeptNameArr = (string[])arrTargetDeptName.ToArray(typeof(string));
                        p_objDeptInstance.m_strTransDeptDateArr = (string[])arrTransDeptDate.ToArray(typeof(string));
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

            }            //返回
            return lngRes;
        }
        #endregion

        #region 获取同步表中的病人收费信息
        /// <summary>
        /// 获取同步表中的病人收费信息
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院时间</param>
        /// <param name="p_objContent">暂存收费信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCost(string p_strInPatientID,
            string p_strInPatientDate,
            out clsInHospitalMainRecord_GXContent p_objContent)
        {
            p_objContent = null;
            long lngRes = 0;
            if (p_strInPatientDate == null || p_strInPatientDate == "" || p_strInPatientID == null || p_strInPatientID == "")
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable m_dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetCost, ref m_dtbResult, objDPArr);

                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_objContent = new clsInHospitalMainRecord_GXContent();
                    double dblCost = 0.0;
                    double dblSum = 0.0;
                    for (int i = 0; i < m_dtbResult.Rows.Count; i++)
                    {
                        dblCost = Convert.ToDouble(m_dtbResult.Rows[i]["COSTS"]);
                        switch (m_dtbResult.Rows[i]["FEE_TYPE"].ToString().Trim())
                        {
                            case "床位":
                                p_objContent.m_dblBEDAMT = dblCost;
                                break;
                            case "护理":
                                p_objContent.m_dblNURSEAMT = dblCost;
                                break;
                            case "西药":
                                p_objContent.m_dblWMAMT = dblCost;
                                break;
                            case "中成":
                                p_objContent.m_dblCMFINISHEDAMT = dblCost;
                                break;
                            case "中草":
                                p_objContent.m_dblCMSEMIFINISHEDAMT = dblCost;
                                break;
                            case "放射":
                                p_objContent.m_dblRADIATIONAMT = dblCost;
                                break;
                            case "化验":
                                p_objContent.m_dblASSAYAMT = dblCost;
                                break;
                            case "输氧":
                                p_objContent.m_dblO2AMT = dblCost;
                                break;
                            case "输血":
                                p_objContent.m_dblBLOODAMT = dblCost;
                                break;
                            case "诊疗":
                                p_objContent.m_dblTREATMENTAMT = dblCost;
                                break;
                            case "手术":
                                p_objContent.m_dblOPERATIONAMT = dblCost;
                                break;
                            case "检查":
                                p_objContent.m_dblCHECKAMT = dblCost;
                                break;
                            case "麻醉":
                                p_objContent.m_dblANAETHESIAAMT = dblCost;
                                break;
                            case "接生":
                                p_objContent.m_dblDELIVERYCHILDAMT = dblCost;
                                break;
                            case "婴儿":
                                p_objContent.m_dblBABYAMT = dblCost;
                                break;
                            case "陪床":
                                p_objContent.m_dblACCOMPANYAMT = dblCost;
                                break;
                            case "其他":
                                p_objContent.m_dblOTHERAMT = dblCost;
                                break;
                        }
                        dblSum += dblCost;
                    }
                    p_objContent.m_dblTOTALAMT = dblSum;
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

            }         //返回
            return lngRes;
        }
        #endregion

        #region 获取同步表中的病人手术信息
        /// <summary>
        /// 获取同步表中的病人手术信息
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOpInfo(string p_strInPatientID, string p_strInPatientDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            if (p_strInPatientDate == null || p_strInPatientDate == "" || p_strInPatientID == null || p_strInPatientID == "")
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(10, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strInPatientID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].Value = p_strInPatientID.Trim();
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[6].Value = p_strInPatientID.Trim();
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[8].Value = p_strInPatientID.Trim();
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = DateTime.Parse(p_strInPatientDate);

                p_dtbResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetOpInfo, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }            //返回
            return lngRes;
        }
        #endregion
    }
}
