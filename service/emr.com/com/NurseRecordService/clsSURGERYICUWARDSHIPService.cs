using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.Utility;
using com.digitalwave.DiseaseTrackService;
namespace com.digitalwave.clsSURGERYICUWARDSHIPService
{
	/// <summary>
	/// 实现特殊记录的中间件。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsSURGERYICUWARDSHIPService	: clsDiseaseTrackService
	{

		/// <summary>
		/// 从GeneralNurseRecord获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= "select createdate,opendate from t_emr_surgeryicuwardship where inpatientid = ? and inpatientdate= ? and status=0";
		/// <summary>
		/// 根据指定表单的信息，从GeneralNurseRecord和GeneralNurseRecordContent查找表单的内容。
		/// 用InPatientID ,InPatientDate ,CreateDate,Status = 0等条件，查询该记录的内容，查找Max(ModifyDate)。
		/// 如果返回lngRes = 1 && rows = 0,则证明此记录已被他人删除掉。
		/// </summary>
		/// 
		//.
		#region note:员工表的关链是与新表中的员工ID
        //private string m_strFunctionTemp;
       // m_strFunctionTemp=clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyno");
		private  string c_strGetRecordContentSQL= @"select a.*,a.OpenDate as OpenDate_Main,b.MODIFYDATE,b.MODIFYUSERID,b.PBODYPART as PBODYPART_Right ,
b.PCONSCIOUSNESS as PCONSCIOUSNESS_Right,b.PPUPIL as PPUPIL_Right,b.PREFLECT as PREFLECT_Right,b.CTEMPERATURE as CTEMPERATURE_Right,b.CSMALLTEMPERATURE as CSMALLTEMPERATURE_Right,
b.CHEARTRATE as CHEARTRATE_Right,b.CHEARTRHYTHM as CHEARTRHYTHM_Right,b.CSD as CSD_Right,b.CCVP as CCVP_Right,b.DPHYSIC1 as DPHYSIC1_Right,b.DPHYSIC2 as DPHYSIC2_Right,
b.DPHYSIC3 as DPHYSIC3_Right,b.DPHYSIC4 as DPHYSIC4_Right,b.DPHYSIC5 as DPHYSIC5_Right,b.DPHYSIC6 as DPHYSIC6_Right,b.DPHYSIC7 as DPHYSIC7_Right,b.DPHYSIC8 as DPHYSIC8_Right,
b.DCURE1 as DCURE1_Right,b.DCURE2 as DCURE2_Right,b.DCURE3 as DCURE3_Right,b.DCURE4 as DCURE4_Right,b.DCURE5 as DCURE5_Right,b.DCURE6 as DCURE6_Right,b.DCURE7 as DCURE7_Right,
b.DCURE8 as DCURE8_Right,b.IGS as IGS_Right,b.INS as INS_Right,b.INTATAL as INTATAL_Right,b.OTATAL as OTATAL_Right,b.OEMIEMCTION as OEMIEMCTION_Right,b.OGASTRICJUICE as OGASTRICJUICE_Right,
b.SESPECIALLYNOTE as SESPECIALLYNOTE_Right,b.BBLUSETIME as BBLUSETIME_Right,b.BBLUSEMACHINETYPE as BBLUSEMACHINETYPE_Right,b.BBLUSEMODE as BBLUSEMODE_Right,b.BVT as BVT_Right,
b.BEXPIREDMV as BEXPIREDMV_Right,b.BBLUESPRESSURE as BBLUESPRESSURE_Right,b.BBLUSENUM as BBLUSENUM_Right,b.BFIO2PEEP as BFIO2PEEP_Right,b.BMAXIP as BMAXIP_Right,
b.BBLUSESOUND as BBLUSESOUND_Right,b.BPHLEGMCOLOR as BPHLEGMCOLOR_Right,b.BSQ2 as BSQ2_Right,b.TCOLLECTBLOODPOINT as TCOLLECTBLOODPOINT_Right,
b.TPH as TPH_Right,b.TPCO2 as TPCO2_Right,b.TP02 as TP02_Right,b.THCO3 as THCO3_Right,b.TTCO2 as TTCO2_Right,b.TBE as TBE_Right,b.TSAT as TSAT_Right,
b.TO2CT as TO2CT_Right,b.SCMH2O as SCMH2O_Right,b.SSD as SSD_Right,b.SMEAN as SMEAN_Right,b.SWEDGE as SWEDGE_Right,b.SCOCI as SCOCI_Right,
b.PPUPLRIGHT as PPUPLRIGH_Right,b.PREFLECTRIGHT as PREFLECTRIGHT_Right,b.IBLOODPRODUCE as IBLOODPRODUCE_Right,b.IBLOODPRODUCEADD as IBLOODPRODUCEADD_Right,
b.INNAME1 as INNAME1_Right,b.INNAME2 as INNAME2_Right,b.INNAME3 as INNAME3_Right,b.INNAME4 as INNAME4_Right,b.INAMOUNT1 as INAMOUNT1_Right,
b.INAMOUNT2 as INAMOUNT2_Right,b.INAMOUNT3 as INAMOUNT3_Right,b.INAMOUNT4 as INAMOUNT4_Right,b.OUTNAME1 as OUTNAME1_Right,
b.OUTNAME2 as OUTNAME2_Right,b.OUTNAME3 as OUTNAME3_Right,b.OUTNAME4 as OUTNAME4_Right,b.OUTAMOUNT1 as OUTAMOUNT1_Right,
b.OUTAMOUNT2 as OUTAMOUNT2_Right,b.OUTAMOUNT3 as OUTAMOUNT3_Right,b.OUTAMOUNT4 as OUTAMOUNT4_Right,b.BFI02PEEPRIGHT as BFI02PEEPRIGHT_Right,b.BPHLEGMAMOUNT as BPHLEGMAMOUNT_Right,"+
clsDatabaseSQLConvert.s_strGetFuntionSQL("f_getempnamebyno") + @" (a.CreateUserID) as FirstName  from T_EMR_SURGERYICUWARDSHIP a,T_EMR_SURGERYICUWARDSHIPNOTE b where a.InPatientID = ? and a.InPatientDate= ?   and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
						b.ModifyDate=(select Max(ModifyDate) from T_EMR_SURGERYICUWARDSHIPNOTE Where InPatientID = InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate) ";//ORDER BY b.MODIFYDATE"; 
       
        
        //Need to be modified in the near future

		#endregion

		/// <summary>
		/// 从GeneralNurseRecord中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= "select createuserid,opendate from t_emr_surgeryicuwardship where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		/// <summary>
		/// 从GeneralNurseRecord获取已经存在记录的主要信息,获取修改表单的主要信息
		/// </summary>
//		private const string c_strGetExistInfoSQL= "";

		/// <summary>
		/// 从GeneralNurseRecordContent获取指定表单的最后修改时间。
		/// </summary>
		private const string c_strCheckLastModifyRecordSQL= @"select b.modifydate,b.modifyuserid from t_emr_surgeryicuwardship a,t_emr_surgeryicuwardshipnote b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from t_emr_surgeryicuwardshipnote where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
				
		/// <summary>
		/// 从GeneralNurseRecordContent获取修改表单的主要信息。
		/// </summary>


		/// <summary>
		/// 从GeneralNurseRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= "select deactiveddate,deactivedoperatorid from t_emr_surgeryicuwardship where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1";

	
		/// <summary>
		///
		#region 添加记录到t_emr_surgeryicuwardship外科ICU监护记录
		private const string c_strAddNewRecordSQL= @"insert into  t_emr_surgeryicuwardship(INPATIENTID,INPATIENTDATE,OPENDATE,CREATEDATE,CREATEUSERID,PBODYPART,PCONSCIOUSNESS,PPUPIL,PREFLECT,CTEMPERATURE,
CSMALLTEMPERATURE,CHEARTRATE,CHEARTRHYTHM,CSD,CCVP,DPHYSIC1,DPHYSIC2,DPHYSIC3,DPHYSIC4,DPHYSIC5,
DPHYSIC6,DPHYSIC7,DPHYSIC8,DCURE1,DCURE2,DCURE3,DCURE4,DCURE5,DCURE6,DCURE7,
DCURE8,IGS,INS,INTATAL ,OTATAL,OEMIEMCTION,OGASTRICJUICE,SESPECIALLYNOTE,BBLUSETIME,BBLUSEMACHINETYPE,
BBLUSEMODE,BVT,BEXPIREDMV,BBLUESPRESSURE,BBLUSENUM,BFIO2PEEP,BMAXIP,BBLUSESOUND,BPHLEGMCOLOR,BSQ2,
TCOLLECTBLOODPOINT,TPH ,TPCO2,TP02,THCO3,TTCO2,TBE,TSAT,TO2CT,SCMH2O,
SSD,SMEAN,SWEDGE,SCOCI,PBODYPARTXML,PCONSCIOUSNESSXML, PPUPILXML,PREFLECTXML,CTEMPERATUREXML, CSMALLTEMPERATUREXML,
CHEARTRATEXML,CHEARTRHYTHMXML ,CSDXML ,CCVPXML,IGSXML,INSXML,INTATALXML, OTATALXML,OEMEMCTIONXML,OGASTRICJUICEXML ,
SESPECIALLYNOTEXML ,BBLUSETIMEXML ,BBLUSEMACHINETYPEXML,BBLUSEMONDEXML, BVTXML,BEXPIREDMVXML ,BBLUESPRESSUREXML,BBLUSENUMXML ,BFIO2PEEPXML ,BMAXIPXML ,
BBLUSESOUNDXML,BPHLEGMCOLORXML  ,BSQ2XML ,TCOLLECTBLOODPOINTXML,TPHXML , TPCO2XML ,TPO2XML ,THCO3XML,TTCO2XML,TBEXML ,
TSATXML,TO2CTXML ,SCMH2OXML,SSDXML, SMEANXML, SWEDGEXML, SCOCIXML  ,DPHYSIC1XML,DPHYSIC2XML, DPHYSIC3XML ,
DPHYSIC4XML ,DPHYSIC5XML,DPHYSIC6XML , DPHYSIC7XML, DPHYSIC8XML, DCURE1XML , DCURE2XML, DCURE3XML ,DCURE4XML  ,DCURE5XML ,
DCURE6XML,DCURE7XML,DCURE8XML, FIRSTPRINTDATE ,RECORDDATE,STATUS,WEIGHT,IDCODE,OPERATIONNAME,OPERATIONDATE,DATEAFTEROPERATION,
PPUPLRIGHT, PREFLECTRIGHT, IBLOODPRODUCE, IBLOODPRODUCEADD, INNAME1,INNAME2,INNAME3, INNAME4 ,INAMOUNT1, INAMOUNT2, INAMOUNT3,INAMOUNT4,
OUTNAME1, OUTNAME2,OUTNAME3,OUTNAME4,OUTAMOUNT1,OUTAMOUNT2, OUTAMOUNT3,OUTAMOUNT4,BFI02PEEPRIGHT,BPHLEGMAMOUNT,
PPUPLRIGHTXML,PREFLECTRIGHTXML ,IBLOODPRODUCEXML,IBLOODPRODUCEADDXML, INNAME1XML ,INNAME2XML ,INNAME3XML,INNAME4XML,
INAMOUNT1XML,INAMOUNT2XML,INAMOUNT3XML ,INAMOUNT4XML,OUTNAME1XML , OUTNAME2XML , OUTNAME3XML ,OUTNAME4XML ,OUTAMOUNT1XML,
OUTAMOUNT2XML,OUTAMOUNT3XML, OUTAMOUNT4XML,BFI02PEEPRIGHTXML, BPHLEGMAMOUNTXML) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

#endregion


		/// <summary>
		/// 添加记录到GeneralNurseRecordContent
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into  t_emr_surgeryicuwardshipnote(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,pbodypart,pconsciousness,ppupil,preflect,ctemperature,
csmalltemperature,cheartrate,cheartrhythm,csd,ccvp,dphysic1,dphysic2,dphysic3,dphysic4,dphysic5,
dphysic6,dphysic7,dphysic8,dcure1,dcure2,dcure3,dcure4,dcure5,dcure6,dcure7,
dcure8,igs,ins,intatal ,otatal,oemiemction,ogastricjuice,sespeciallynote,bblusetime,bblusemachinetype,
bblusemode,bvt,bexpiredmv,bbluespressure,bblusenum,bfio2peep,bmaxip,bblusesound,bphlegmcolor,bsq2,
tcollectbloodpoint,tph ,tpco2,tp02,thco3,ttco2,tbe,tsat,to2ct,scmh2o,
ssd,smean,swedge,scoci,ppuplright, preflectright, ibloodproduce, ibloodproduceadd, inname1,inname2,inname3, inname4 ,inamount1, inamount2, inamount3,inamount4,
outname1, outname2,outname3,outname4,outamount1,outamount2, outamount3,outamount4,bfi02peepright,bphlegmamount) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		
		/// <summary>
		/// 修改记录到GeneralNurseRecordContent
		/// </summary>

		private const string c_strModifyRecordSQL= @"update t_emr_surgeryicuwardship set pbodypart=?,pconsciousness=?,ppupil=?,preflect=?,ctemperature=?,
		csmalltemperature=?,cheartrate=?,cheartrhythm=?,csd=?,ccvp=?,dphysic1=?,dphysic2=?,dphysic3=?,dphysic4=?,dphysic5=?,
		dphysic6=?,dphysic7=?,dphysic8=?,dcure1=?,dcure2=?,dcure3=?,dcure4=?,dcure5=?,dcure6=?,dcure7=?,
		dcure8=?,igs=?,ins=?,intatal=? ,otatal=?,oemiemction=?,ogastricjuice=?,sespeciallynote=?,bblusetime=?,bblusemachinetype=?,
		bblusemode=?,bvt=?,bexpiredmv=?,bbluespressure=?,bblusenum=?,bfio2peep=?,bmaxip=?,bblusesound=?,bphlegmcolor=?,bsq2=?,
		tcollectbloodpoint=?,tph=? ,tpco2=?,tp02=?,thco3=?,ttco2=?,tbe=?,tsat=?,to2ct=?,scmh2o=?,
		ssd=?,smean=?,swedge=?,scoci=?,pbodypartxml=?,pconsciousnessxml=?, ppupilxml=?,preflectxml=?,ctemperaturexml=?, csmalltemperaturexml=?,
		cheartratexml=?,cheartrhythmxml=? ,csdxml=? ,ccvpxml=?,igsxml=?,insxml=?,intatalxml=?, otatalxml=?,oememctionxml=?,ogastricjuicexml=? ,
		sespeciallynotexml=? ,bblusetimexml=? ,bblusemachinetypexml=?,bblusemondexml=?, bvtxml=?,bexpiredmvxml=? ,bbluespressurexml=?,bblusenumxml=? ,bfio2peepxml=? ,bmaxipxml=? ,
		bblusesoundxml=?,bphlegmcolorxml=?  ,bsq2xml=? ,tcollectbloodpointxml=?,tphxml=? , tpco2xml=? ,tpo2xml=? ,thco3xml=?,ttco2xml=?,tbexml=? ,
		tsatxml=?,to2ctxml=? ,scmh2oxml=?,ssdxml=?, smeanxml=?, swedgexml=?, scocixml=?  ,dphysic1xml=?,dphysic2xml=?, dphysic3xml=? ,
		dphysic4xml=? ,dphysic5xml=?,dphysic6xml=? , dphysic7xml=?, dphysic8xml=?, dcure1xml=? , dcure2xml=?, dcure3xml=? ,dcure4xml=?  ,dcure5xml=? ,
		dcure6xml=?,dcure7xml=?,dcure8xml=?, firstprintdate=? ,recorddate=?,status=?,weight=?,idcode=?,operationname=?,operationdate=?,dateafteroperation=?,
		ppuplright=?, preflectright=?, ibloodproduce=?, ibloodproduceadd=?, inname1=?,inname2=?,inname3=?, inname4=? ,inamount1=?, inamount2=?, inamount3=?,inamount4=?,
		outname1=?, outname2=?,outname3=?,outname4=?,outamount1=?,outamount2=?, outamount3=?,outamount4=?,bfi02peepright=?,bphlegmamount=?,
		ppuplrightxml=?,preflectrightxml=? ,ibloodproducexml=?,ibloodproduceaddxml=?, inname1xml=? ,inname2xml=? ,inname3xml=?,inname4xml=?,
		inamount1xml=?,inamount2xml=?,inamount3xml=? ,inamount4xml=?,outname1xml=? , outname2xml=? , outname3xml=? ,outname4xml=? ,outamount1xml=?,
		outamount2xml=?,outamount3xml=?, outamount4xml=?,bfi02peeprightxml=?, bphlegmamountxml=?
		where inpatientid=? and inpatientdate=? and opendate=? and status=0";
		/// <summary>
		/// 修改记录到GeneralNurseRecordContent
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// 设置GeneralNurseRecord中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= "update t_emr_surgeryicuwardship set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// 从GeneralNurseRecord和GeneralNurseRecordContent获取LastModifyDate和FirstPrintDate
		/// </summary>
		private const string c_strGetModifyDateAndFirstPrintDateSQL=  @"select a.firstprintdate,b.modifydate from t_emr_surgeryicuwardship a,t_emr_surgeryicuwardshipnote b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from t_emr_surgeryicuwardshipnote where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";
						
		/// <summary>
		/// 更新GeneralNurseRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= "update  t_emr_surgeryicuwardship set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		/// <summary>
		/// 从GeneralNurseRecord获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= "select createdate,opendate from t_emr_surgeryicuwardship where inpatientid = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";

		/// <summary>
		/// 在GeneralNurseRecordContent中获取指定表单的信息。
		/// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.weight,
       a.idcode,
       a.operationname,
       a.operationdate,
       a.dateafteroperation,
       a.pbodypart,
       a.pconsciousness,
       a.ppupil,
       a.preflect,
       a.ctemperature,
       a.csmalltemperature,
       a.cheartrate,
       a.cheartrhythm,
       a.csd,
       a.ccvp,
       a.dphysic1,
       a.dphysic2,
       a.dphysic3,
       a.dphysic4,
       a.dphysic5,
       a.dphysic6,
       a.dphysic7,
       a.dphysic8,
       a.dcure1,
       a.dcure2,
       a.dcure3,
       a.dcure4,
       a.dcure5,
       a.dcure6,
       a.dcure7,
       a.dcure8,
       a.igs,
       a.ins,
       a.intatal,
       a.otatal,
       a.oemiemction,
       a.ogastricjuice,
       a.sespeciallynote,
       a.bblusetime,
       a.bblusemachinetype,
       a.bblusemode,
       a.bvt,
       a.bexpiredmv,
       a.bbluespressure,
       a.bblusenum,
       a.bfio2peep,
       a.bmaxip,
       a.bblusesound,
       a.bphlegmcolor,
       a.bsq2,
       a.tcollectbloodpoint,
       a.tph,
       a.tpco2,
       a.tp02,
       a.thco3,
       a.ttco2,
       a.tbe,
       a.tsat,
       a.to2ct,
       a.scmh2o,
       a.ssd,
       a.smean,
       a.swedge,
       a.scoci,
       a.pbodypartxml,
       a.pconsciousnessxml,
       a.ppupilxml,
       a.preflectxml,
       a.ctemperaturexml,
       a.csmalltemperaturexml,
       a.cheartratexml,
       a.cheartrhythmxml,
       a.csdxml,
       a.ccvpxml,
       a.igsxml,
       a.insxml,
       a.intatalxml,
       a.otatalxml,
       a.oememctionxml,
       a.ogastricjuicexml,
       a.sespeciallynotexml,
       a.bblusetimexml,
       a.bblusemachinetypexml,
       a.bblusemondexml,
       a.bvtxml,
       a.bexpiredmvxml,
       a.bbluespressurexml,
       a.bblusenumxml,
       a.bfio2peepxml,
       a.bmaxipxml,
       a.bblusesoundxml,
       a.bphlegmcolorxml,
       a.bsq2xml,
       a.tcollectbloodpointxml,
       a.tphxml,
       a.tpco2xml,
       a.tpo2xml,
       a.thco3xml,
       a.ttco2xml,
       a.tbexml,
       a.tsatxml,
       a.to2ctxml,
       a.scmh2oxml,
       a.ssdxml,
       a.smeanxml,
       a.swedgexml,
       a.scocixml,
       a.firstprintdate,
       a.recorddate,
       a.status,
       a.dphysic1xml,
       a.dphysic2xml,
       a.dphysic3xml,
       a.dphysic4xml,
       a.dphysic5xml,
       a.dphysic6xml,
       a.dphysic7xml,
       a.dphysic8xml,
       a.dcure1xml,
       a.dcure2xml,
       a.dcure3xml,
       a.dcure4xml,
       a.dcure5xml,
       a.dcure6xml,
       a.dcure7xml,
       a.dcure8xml,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.ppuplright,
       a.preflectright,
       a.ibloodproduce,
       a.ibloodproduceadd,
       a.inname1,
       a.inname2,
       a.inname3,
       a.inname4,
       a.inamount1,
       a.inamount2,
       a.inamount3,
       a.inamount4,
       a.outname1,
       a.outname2,
       a.outname3,
       a.outname4,
       a.outamount1,
       a.outamount2,
       a.outamount3,
       a.outamount4,
       a.bfi02peepright,
       a.bphlegmamount,
       a.ppuplrightxml,
       a.preflectrightxml,
       a.ibloodproducexml,
       a.ibloodproduceaddxml,
       a.inname1xml,
       a.inname2xml,
       a.inname3xml,
       a.inname4xml,
       a.inamount1xml,
       a.inamount2xml,
       a.inamount3xml,
       a.inamount4xml,
       a.outname1xml,
       a.outname2xml,
       a.outname3xml,
       a.outname4xml,
       a.outamount1xml,
       a.outamount2xml,
       a.outamount3xml,
       a.outamount4xml,
       a.bfi02peeprightxml,
       a.bphlegmamountxml,
       b.modifydate,
       b.modifyuserid,
       b.pbodypart,
       b.pconsciousness,
       b.ppupil,
       b.preflect,
       b.ctemperature,
       b.csmalltemperature,
       b.cheartrate,
       b.cheartrhythm,
       b.csd,
       b.ccvp,
       b.dphysic1,
       b.dphysic2,
       b.dphysic3,
       b.dphysic4,
       b.dphysic5,
       b.dphysic6,
       b.dphysic7,
       b.dphysic8,
       b.dcure1,
       b.dcure2,
       b.dcure3,
       b.dcure4,
       b.dcure5,
       b.dcure6,
       b.dcure7,
       b.dcure8,
       b.igs,
       b.ins,
       b.intatal,
       b.otatal,
       b.oemiemction,
       b.ogastricjuice,
       b.sespeciallynote,
       b.bblusetime,
       b.bblusemachinetype,
       b.bblusemode,
       b.bvt,
       b.bexpiredmv,
       b.bbluespressure,
       b.bblusenum,
       b.bfio2peep,
       b.bmaxip,
       b.bblusesound,
       b.bphlegmcolor,
       b.bsq2,
       b.tcollectbloodpoint,
       b.tph,
       b.tpco2,
       b.tp02,
       b.thco3,
       b.ttco2,
       b.tbe,
       b.tsat,
       b.to2ct,
       b.scmh2o,
       b.ssd,
       b.smean,
       b.swedge,
       b.scoci,
       b.ppuplright,
       b.preflectright,
       b.ibloodproduce,
       b.ibloodproduceadd,
       b.inname1,
       b.inname2,
       b.inname3,
       b.inname4,
       b.inamount1,
       b.inamount2,
       b.inamount3,
       b.inamount4,
       b.outname1,
       b.outname2,
       b.outname3,
       b.outname4,
       b.outamount1,
       b.outamount2,
       b.outamount3,
       b.outamount4,
       b.bfi02peepright,
       b.bphlegmamount
  from t_emr_surgeryicuwardship a, t_emr_surgeryicuwardshipnote b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from t_emr_surgeryicuwardshipnote
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

		/// <summary>
		/// 从GeneralNurseRecord获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= "select createdate,opendate from t_emr_surgeryicuwardship where inpatientid = ? and inpatientdate= ? and status=1";

		/// <summary>
		/// 获取病人的该记录时间列表。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
		/// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsGeneralNurseRecordService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //string strSQL = "select CreateDate,OpenDate from GeneralNurseRecord Where trim(InPatientID) = ? and InPatientDate= ? and Status=0";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[2];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                //返回
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
		/// 更新数据库中的首次打印时间。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
		[AutoComplete]
		public override long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			DateTime p_dtmFirstPrintDate)
		{
			long lngCheckRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsGeneralNurseRecordService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

                //获取IDataParameter数组
                //string strSQL = "Update  GeneralNurseRecord Set FirstPrintDate= ? Where trim(InPatientID)= ? and InPatientDate= ? and OpenDate=? and FirstPrintDate IS NULL and Status=0";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[4];
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
                lngCheckRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
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

            return lngCheckRes;
						
		}

		/// <summary>
		/// 获取病人的已经被删除记录时间列表。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strDeleteUserID">删除者ID</param>
		/// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
		/// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsGeneralNurseRecordService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //string strSQL = "select CreateDate,OpenDate from GeneralNurseRecord Where trim(InPatientID) = ? and InPatientDate= ? and DeActivedOperatorID= ? and Status=1";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                //返回
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
		/// 获取病人的已经被删除记录时间列表。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
		/// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
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

                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsGeneralNurseRecordService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //string strSQL = "select CreateDate,OpenDate from GeneralNurseRecord Where trim(InPatientID) = ? and InPatientDate= ? and Status=1";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[2];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                //返回
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
		/// 获取指定记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">记录内容</param>
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

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //			string strSQL = @"select a.*,b.* from GeneralNurseRecord a,GeneralNurseRecordContent b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
                //						b.ModifyDate=(select Max(ModifyDate) from GeneralNurseRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)//==1)
                {
                    //设置结果这里取子表记录
                    //clsGeneralNurseRecordContent objRecordContent=new clsGeneralNurseRecordContent();
                    clsISURGERYICUWARDSHIP objRecordContent = new clsISURGERYICUWARDSHIP();
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

                    objRecordContent.m_strSignName = dtbValue.Rows[0]["FIRSTNAME"].ToString();

                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_strSTATUS = "0";
                    else objRecordContent.m_strSTATUS = dtbValue.Rows[0]["STATUS"].ToString();
                    objRecordContent.m_strPBODYPART = dtbValue.Rows[0]["PBODYPART"].ToString();
                    objRecordContent.m_strPBODYPARTXML = dtbValue.Rows[0]["PBODYPARTXML"].ToString();
                    objRecordContent.m_strPCONSCIOUSNESS = dtbValue.Rows[0]["PCONSCIOUSNESS"].ToString();
                    objRecordContent.m_strPCONSCIOUSNESSXML = dtbValue.Rows[0]["PCONSCIOUSNESSXML"].ToString();
                    objRecordContent.m_strPPUPIL = dtbValue.Rows[0]["PPUPIL"].ToString();
                    objRecordContent.m_strPPUPILXML = dtbValue.Rows[0]["PPUPILXML"].ToString();
                    objRecordContent.m_strPREFLECT = dtbValue.Rows[0]["PREFLECT"].ToString();
                    objRecordContent.m_strPREFLECTXML = dtbValue.Rows[0]["PREFLECTXML"].ToString();
                    objRecordContent.m_strCTEMPERATURE = dtbValue.Rows[0]["CTEMPERATURE"].ToString();
                    objRecordContent.m_strCTEMPERATUREXML = dtbValue.Rows[0]["CTEMPERATUREXML"].ToString();
                    objRecordContent.m_strCSMALLTEMPERATURE = dtbValue.Rows[0]["CSMALLTEMPERATURE"].ToString();
                    objRecordContent.m_strCSMALLTEMPERATUREXML = dtbValue.Rows[0]["CSMALLTEMPERATUREXML"].ToString();
                    objRecordContent.m_strCHEARTRATE = dtbValue.Rows[0]["CHEARTRATE"].ToString();
                    objRecordContent.m_strCHEARTRATEXML = dtbValue.Rows[0]["CHEARTRATEXML"].ToString();
                    objRecordContent.m_strCHEARTRHYTHM = dtbValue.Rows[0]["CHEARTRHYTHM"].ToString();
                    objRecordContent.m_strCHEARTRHYTHMXML = dtbValue.Rows[0]["CHEARTRHYTHMXML"].ToString();
                    objRecordContent.m_strCSD = dtbValue.Rows[0]["CSD"].ToString();
                    objRecordContent.m_strCSDXML = dtbValue.Rows[0]["CSDXML"].ToString();
                    objRecordContent.m_strCCVP = dtbValue.Rows[0]["CCVP"].ToString();
                    objRecordContent.m_strCCVPXML = dtbValue.Rows[0]["CCVPXML"].ToString();

                    objRecordContent.m_strDPHYSIC1 = dtbValue.Rows[0]["DPHYSIC1"].ToString();
                    objRecordContent.m_strDPHYSIC1XML = dtbValue.Rows[0]["DPHYSIC1XML"].ToString();
                    objRecordContent.m_strDPHYSIC2 = dtbValue.Rows[0]["DPHYSIC2"].ToString();
                    objRecordContent.m_strDPHYSIC2XML = dtbValue.Rows[0]["DPHYSIC2XML"].ToString();
                    objRecordContent.m_strDPHYSIC3 = dtbValue.Rows[0]["DPHYSIC3"].ToString();
                    objRecordContent.m_strDPHYSIC3XML = dtbValue.Rows[0]["DPHYSIC3XML"].ToString();
                    objRecordContent.m_strDPHYSIC4 = dtbValue.Rows[0]["DPHYSIC4"].ToString();
                    objRecordContent.m_strDPHYSIC4XML = dtbValue.Rows[0]["DPHYSIC4XML"].ToString();
                    objRecordContent.m_strDPHYSIC5 = dtbValue.Rows[0]["DPHYSIC5"].ToString();
                    objRecordContent.m_strDPHYSIC5XML = dtbValue.Rows[0]["DPHYSIC5XML"].ToString();
                    objRecordContent.m_strDPHYSIC6 = dtbValue.Rows[0]["DPHYSIC6"].ToString();
                    objRecordContent.m_strDPHYSIC6XML = dtbValue.Rows[0]["DPHYSIC6XML"].ToString();
                    objRecordContent.m_strDPHYSIC7 = dtbValue.Rows[0]["DPHYSIC7"].ToString();
                    objRecordContent.m_strDPHYSIC7XML = dtbValue.Rows[0]["DPHYSIC7XML"].ToString();
                    objRecordContent.m_strDPHYSIC8 = dtbValue.Rows[0]["DPHYSIC8"].ToString();
                    objRecordContent.m_strDPHYSIC8XML = dtbValue.Rows[0]["DPHYSIC8XML"].ToString();

                    objRecordContent.m_strDCURE1 = dtbValue.Rows[0]["DCURE1"].ToString();
                    objRecordContent.m_strDCURE1XML = dtbValue.Rows[0]["DCURE1XML"].ToString();
                    objRecordContent.m_strDCURE2 = dtbValue.Rows[0]["DCURE2"].ToString();
                    objRecordContent.m_strDCURE2XML = dtbValue.Rows[0]["DCURE2XML"].ToString();
                    objRecordContent.m_strDCURE3 = dtbValue.Rows[0]["DCURE3"].ToString();
                    objRecordContent.m_strDCURE3XML = dtbValue.Rows[0]["DCURE3XML"].ToString();
                    objRecordContent.m_strDCURE4 = dtbValue.Rows[0]["DCURE4"].ToString();
                    objRecordContent.m_strDCURE4XML = dtbValue.Rows[0]["DCURE4XML"].ToString();
                    objRecordContent.m_strDCURE5 = dtbValue.Rows[0]["DCURE5"].ToString();
                    objRecordContent.m_strDCURE5XML = dtbValue.Rows[0]["DCURE5XML"].ToString();
                    objRecordContent.m_strDCURE6 = dtbValue.Rows[0]["DCURE6"].ToString();
                    objRecordContent.m_strDCURE6XML = dtbValue.Rows[0]["DCURE6XML"].ToString();
                    objRecordContent.m_strDCURE7 = dtbValue.Rows[0]["DCURE7"].ToString();
                    objRecordContent.m_strDCURE7XML = dtbValue.Rows[0]["DCURE7XML"].ToString();
                    objRecordContent.m_strDCURE8 = dtbValue.Rows[0]["DCURE8"].ToString();
                    objRecordContent.m_strDCURE8XML = dtbValue.Rows[0]["DCURE8XML"].ToString();

                    objRecordContent.m_fltIGS = float.Parse(dtbValue.Rows[0]["IGS"].ToString());
                    objRecordContent.m_strIGSXML = dtbValue.Rows[0]["IGSXML"].ToString();
                    objRecordContent.m_fltINS = float.Parse(dtbValue.Rows[0]["INS"].ToString());
                    objRecordContent.m_strINSXML = dtbValue.Rows[0]["INSXML"].ToString();
                    objRecordContent.m_fltINTATAL = float.Parse(dtbValue.Rows[0]["INTATAL"].ToString());
                    objRecordContent.m_strINTATALXML = dtbValue.Rows[0]["INTATALXML"].ToString();
                    objRecordContent.m_fltOTATAL = float.Parse(dtbValue.Rows[0]["OTATAL"].ToString());
                    objRecordContent.m_strOTATALXML = dtbValue.Rows[0]["OTATALXML"].ToString();
                    objRecordContent.m_fltOEMIEMCTION = float.Parse(dtbValue.Rows[0]["OEMIEMCTION"].ToString());
                    objRecordContent.m_strOEMEMCTIONXML = dtbValue.Rows[0]["OEMEMCTIONXML"].ToString();
                    objRecordContent.m_fltOGASTRICJUICE = float.Parse(dtbValue.Rows[0]["OGASTRICJUICE"].ToString());
                    objRecordContent.m_strOGASTRICJUICEXML = dtbValue.Rows[0]["OGASTRICJUICEXML"].ToString();
                    objRecordContent.m_strSESPECIALLYNOTE = dtbValue.Rows[0]["SESPECIALLYNOTE"].ToString();
                    objRecordContent.m_strSESPECIALLYNOTEXML = dtbValue.Rows[0]["SESPECIALLYNOTEXML"].ToString();
                    objRecordContent.m_strBBLUSETIME = dtbValue.Rows[0]["BBLUSETIME"].ToString();
                    objRecordContent.m_strBBLUSETIMEXML = dtbValue.Rows[0]["BBLUSETIMEXML"].ToString();

                    objRecordContent.m_strBBLUSEMACHINETYPE = dtbValue.Rows[0]["BBLUSEMACHINETYPE"].ToString();
                    objRecordContent.m_strBBLUSEMACHINETYPEXML = dtbValue.Rows[0]["BBLUSEMACHINETYPEXML"].ToString();
                    objRecordContent.m_strBBLUSEMODE = dtbValue.Rows[0]["BBLUSEMODE"].ToString();
                    objRecordContent.m_strBBLUSEMONDEXML = dtbValue.Rows[0]["BBLUSEMONDEXML"].ToString();
                    objRecordContent.m_strBVT = dtbValue.Rows[0]["BVT"].ToString();
                    objRecordContent.m_strBVTXML = dtbValue.Rows[0]["BVTXML"].ToString();
                    objRecordContent.m_strBEXPIREDMV = dtbValue.Rows[0]["BEXPIREDMV"].ToString();
                    objRecordContent.m_strBEXPIREDMVXML = dtbValue.Rows[0]["BEXPIREDMVXML"].ToString();
                    objRecordContent.m_strBBLUESPRESSURE = dtbValue.Rows[0]["BBLUESPRESSURE"].ToString();
                    objRecordContent.m_strBBLUESPRESSUREXML = dtbValue.Rows[0]["BBLUESPRESSUREXML"].ToString();
                    objRecordContent.m_strBBLUSENUM = dtbValue.Rows[0]["BBLUSENUM"].ToString();
                    objRecordContent.m_strBBLUSENUMXML = dtbValue.Rows[0]["BBLUSENUMXML"].ToString();
                    objRecordContent.m_strBFIO2PEEP = dtbValue.Rows[0]["BFIO2PEEP"].ToString();
                    objRecordContent.m_strBFIO2PEEPXML = dtbValue.Rows[0]["BFIO2PEEPXML"].ToString();
                    objRecordContent.m_strBMAXIP = dtbValue.Rows[0]["BMAXIP"].ToString();
                    objRecordContent.m_strBMAXIPXML = dtbValue.Rows[0]["BMAXIPXML"].ToString();

                    objRecordContent.m_strBBLUSESOUND = dtbValue.Rows[0]["BBLUSESOUND"].ToString();
                    objRecordContent.m_strBBLUSESOUNDXML = dtbValue.Rows[0]["BBLUSESOUNDXML"].ToString();
                    objRecordContent.m_strBPHLEGMCOLOR = dtbValue.Rows[0]["BPHLEGMCOLOR"].ToString();
                    objRecordContent.m_strBPHLEGMCOLORXML = dtbValue.Rows[0]["BPHLEGMCOLORXML"].ToString();
                    objRecordContent.m_strBSQ2 = dtbValue.Rows[0]["BSQ2"].ToString();
                    objRecordContent.m_strBSQ2XML = dtbValue.Rows[0]["BSQ2XML"].ToString();
                    objRecordContent.m_strTCOLLECTBLOODPOINT = dtbValue.Rows[0]["TCOLLECTBLOODPOINT"].ToString();
                    objRecordContent.m_strTCOLLECTBLOODPOINTXML = dtbValue.Rows[0]["TCOLLECTBLOODPOINTXML"].ToString();
                    objRecordContent.m_strTPH = dtbValue.Rows[0]["TPH"].ToString();
                    objRecordContent.m_strTPHXML = dtbValue.Rows[0]["TPHXML"].ToString();
                    objRecordContent.m_strTPCO2 = dtbValue.Rows[0]["TPCO2"].ToString();
                    objRecordContent.m_strTPCO2XML = dtbValue.Rows[0]["TPCO2XML"].ToString();
                    objRecordContent.m_strTP02 = dtbValue.Rows[0]["TP02"].ToString();
                    objRecordContent.m_strTPO2XML = dtbValue.Rows[0]["TPO2XML"].ToString();
                    objRecordContent.m_strTHCO3 = dtbValue.Rows[0]["THCO3"].ToString();
                    objRecordContent.m_strTHCO3XML = dtbValue.Rows[0]["THCO3XML"].ToString();

                    objRecordContent.m_strTTCO2 = dtbValue.Rows[0]["TTCO2"].ToString();
                    objRecordContent.m_strTTCO2XML = dtbValue.Rows[0]["TTCO2XML"].ToString();
                    objRecordContent.m_strTBE = dtbValue.Rows[0]["TBE"].ToString();
                    objRecordContent.m_strTBEXML = dtbValue.Rows[0]["TBEXML"].ToString();
                    objRecordContent.m_strTSAT = dtbValue.Rows[0]["TSAT"].ToString();
                    objRecordContent.m_strTSATXML = dtbValue.Rows[0]["TSATXML"].ToString();
                    objRecordContent.m_strTO2CT = dtbValue.Rows[0]["TO2CT"].ToString();
                    objRecordContent.m_strTO2CTXML = dtbValue.Rows[0]["TO2CTXML"].ToString();
                    objRecordContent.m_strSCMH2O = dtbValue.Rows[0]["SCMH2O"].ToString();
                    objRecordContent.m_strSCMH2OXML = dtbValue.Rows[0]["SCMH2OXML"].ToString();
                    objRecordContent.m_strSSD = dtbValue.Rows[0]["SSD"].ToString();
                    objRecordContent.m_strSSDXML = dtbValue.Rows[0]["SSDXML"].ToString();
                    objRecordContent.m_strSMEAN = dtbValue.Rows[0]["SMEAN"].ToString();
                    objRecordContent.m_strSMEANXML = dtbValue.Rows[0]["SMEANXML"].ToString();
                    objRecordContent.m_strSWEDGE = dtbValue.Rows[0]["SWEDGE"].ToString();
                    objRecordContent.m_strSWEDGEXML = dtbValue.Rows[0]["SWEDGEXML"].ToString();
                    objRecordContent.m_strSCOCI = dtbValue.Rows[0]["SCOCI"].ToString();
                    objRecordContent.m_strSCOCIXML = dtbValue.Rows[0]["SCOCIXML"].ToString();
                    //				
                    objRecordContent.m_strPBODYPART_Right = dtbValue.Rows[0]["PBODYPART_Right"].ToString();
                    objRecordContent.m_strPCONSCIOUSNESS_Right = dtbValue.Rows[0]["PCONSCIOUSNESS_Right"].ToString();
                    objRecordContent.m_strPPUPIL_Right = dtbValue.Rows[0]["PPUPIL_Right"].ToString();
                    objRecordContent.m_strPREFLECT_Right = dtbValue.Rows[0]["PREFLECT_Right"].ToString();
                    objRecordContent.m_strCTEMPERATURE_Right = dtbValue.Rows[0]["CTEMPERATURE_Right"].ToString();
                    objRecordContent.m_strCSMALLTEMPERATURE_Right = dtbValue.Rows[0]["CSMALLTEMPERATURE_Right"].ToString();
                    objRecordContent.m_strCHEARTRHYTHM_Right = dtbValue.Rows[0]["CHEARTRHYTHM_Right"].ToString();
                    objRecordContent.m_strCSD_Right = dtbValue.Rows[0]["CSD_Right"].ToString();
                    objRecordContent.m_strCCVP_Right = dtbValue.Rows[0]["CCVP_Right"].ToString();
                    objRecordContent.m_strDPHYSIC1_Right = dtbValue.Rows[0]["DPHYSIC1_Right"].ToString();
                    objRecordContent.m_strDPHYSIC2_Right = dtbValue.Rows[0]["DPHYSIC2_Right"].ToString();
                    objRecordContent.m_strDPHYSIC3_Right = dtbValue.Rows[0]["DPHYSIC3_Right"].ToString();
                    objRecordContent.m_strDPHYSIC4_Right = dtbValue.Rows[0]["DPHYSIC4_Right"].ToString();
                    objRecordContent.m_strDPHYSIC5_Right = dtbValue.Rows[0]["DPHYSIC5_Right"].ToString();
                    objRecordContent.m_strDPHYSIC6_Right = dtbValue.Rows[0]["DPHYSIC6_Right"].ToString();
                    objRecordContent.m_strDPHYSIC7_Right = dtbValue.Rows[0]["DPHYSIC7_Right"].ToString();
                    objRecordContent.m_strDPHYSIC8_Right = dtbValue.Rows[0]["DPHYSIC8_Right"].ToString();
                    objRecordContent.m_strDCURE1_Right = dtbValue.Rows[0]["DCURE1_Right"].ToString();
                    objRecordContent.m_strDCURE2_Right = dtbValue.Rows[0]["DCURE2_Right"].ToString();
                    objRecordContent.m_strDCURE3_Right = dtbValue.Rows[0]["DCURE3_Right"].ToString();
                    objRecordContent.m_strDCURE4_Right = dtbValue.Rows[0]["DCURE4_Right"].ToString();
                    objRecordContent.m_strDCURE5_Right = dtbValue.Rows[0]["DCURE5_Right"].ToString();
                    objRecordContent.m_strDCURE6_Right = dtbValue.Rows[0]["DCURE6_Right"].ToString();
                    objRecordContent.m_strDCURE7_Right = dtbValue.Rows[0]["DCURE7_Right"].ToString();
                    objRecordContent.m_strDCURE8_Right = dtbValue.Rows[0]["DCURE8_Right"].ToString();
                    objRecordContent.m_strIGS_Right = dtbValue.Rows[0]["IGS_Right"].ToString();
                    objRecordContent.m_strINS_Right = dtbValue.Rows[0]["INS_Right"].ToString();
                    objRecordContent.m_strINTATAL_Right = dtbValue.Rows[0]["INTATAL_Right"].ToString();
                    objRecordContent.m_strOTATAL_Right = dtbValue.Rows[0]["OTATAL_Right"].ToString();
                    objRecordContent.m_strOEMIEMCTION_Right = dtbValue.Rows[0]["OEMIEMCTION_Right"].ToString();
                    objRecordContent.m_strOGASTRICJUICE_Right = dtbValue.Rows[0]["OGASTRICJUICE_Right"].ToString();
                    objRecordContent.m_strSESPECIALLYNOTE_Right = dtbValue.Rows[0]["SESPECIALLYNOTE_Right"].ToString();
                    objRecordContent.m_strBBLUSETIME_Right = dtbValue.Rows[0]["BBLUSETIME_Right"].ToString();
                    objRecordContent.m_strBBLUSEMACHINETYPE_Right = dtbValue.Rows[0]["BBLUSEMACHINETYPE_Right"].ToString();
                    objRecordContent.m_strBBLUSEMODE_Right = dtbValue.Rows[0]["BBLUSEMODE_Right"].ToString();
                    objRecordContent.m_strBVT_Right = dtbValue.Rows[0]["BVT_Right"].ToString();
                    objRecordContent.m_strBEXPIREDMV_Right = dtbValue.Rows[0]["BEXPIREDMV_Right"].ToString();
                    objRecordContent.m_strBBLUESPRESSURE_Right = dtbValue.Rows[0]["BBLUESPRESSURE_Right"].ToString();
                    objRecordContent.m_strBBLUSENUM_Right = dtbValue.Rows[0]["BBLUSENUM_Right"].ToString();
                    objRecordContent.m_strBFIO2PEEP_Right = dtbValue.Rows[0]["BFIO2PEEP_Right"].ToString();
                    objRecordContent.m_strBMAXIP_Right = dtbValue.Rows[0]["BMAXIP_Right"].ToString();
                    objRecordContent.m_strBBLUSESOUND_Right = dtbValue.Rows[0]["BBLUSESOUND_Right"].ToString();
                    objRecordContent.m_strBPHLEGMCOLOR_Right = dtbValue.Rows[0]["BPHLEGMCOLOR_Right"].ToString();
                    objRecordContent.m_strBSQ2_Right = dtbValue.Rows[0]["BSQ2_Right"].ToString();
                    objRecordContent.m_strTCOLLECTBLOODPOINT_Right = dtbValue.Rows[0]["TCOLLECTBLOODPOINT_Right"].ToString();
                    objRecordContent.m_strTPH_Right = dtbValue.Rows[0]["TPH_Right"].ToString();
                    objRecordContent.m_strTPCO2_Right = dtbValue.Rows[0]["TPCO2_Right"].ToString();
                    objRecordContent.m_strTP02_Right = dtbValue.Rows[0]["TP02_Right"].ToString();
                    objRecordContent.m_strTHCO3_Right = dtbValue.Rows[0]["THCO3_Right"].ToString();
                    objRecordContent.m_strTTCO2_Right = dtbValue.Rows[0]["TTCO2_Right"].ToString();
                    objRecordContent.m_strTSAT_Right = dtbValue.Rows[0]["TSAT_Right"].ToString();
                    objRecordContent.m_strTO2CT_Right = dtbValue.Rows[0]["TO2CT_Right"].ToString();
                    objRecordContent.m_strSCMH2O_Right = dtbValue.Rows[0]["SCMH2O_Right"].ToString();
                    objRecordContent.m_strSSD_Right = dtbValue.Rows[0]["SSD_Right"].ToString();
                    objRecordContent.m_strSMEAN_Right = dtbValue.Rows[0]["SMEAN_Right"].ToString();
                    objRecordContent.m_strSWEDGE_Right = dtbValue.Rows[0]["SWEDGE_Right"].ToString();
                    objRecordContent.m_strSCOCI_Right = dtbValue.Rows[0]["SCOCI_Right"].ToString();
                    objRecordContent.m_strTBE_Right = dtbValue.Rows[0]["TBE_Right"].ToString();
                    objRecordContent.m_strCHEARTRATE_Right = dtbValue.Rows[0]["CHEARTRATE_Right"].ToString();

                    #region//add field 20051117
                    objRecordContent.m_strPPUPLRIGH_RightT = dtbValue.Rows[0]["PPUPLRIGH_Right"].ToString();  //瞳孔(右)
                    objRecordContent.m_strPREFLECTRIGHT_Right = dtbValue.Rows[0]["PREFLECTRIGHT_Right"].ToString();//对光反射(右)'
                    objRecordContent.m_strIBLOODPRODUCE_Right = dtbValue.Rows[0]["IBLOODPRODUCE_Right"].ToString();//血制品';
                    objRecordContent.m_strIBLOODPRODUCEAD_Right = dtbValue.Rows[0]["IBLOODPRODUCEADD_Right"].ToString();// '血制品累计量';
                    objRecordContent.m_strINNAME1_Right = dtbValue.Rows[0]["INNAME1_Right"].ToString();//入量名称1';
                    objRecordContent.m_strINNAME2_Right = dtbValue.Rows[0]["INNAME2_Right"].ToString();//入量名称2';
                    objRecordContent.m_strINNAME3_Right = dtbValue.Rows[0]["INNAME3_Right"].ToString();//入量名称3';
                    objRecordContent.m_strINNAME4_Right = dtbValue.Rows[0]["INNAME4_Right"].ToString();//入量名称4';
                    objRecordContent.m_strINAMOUNT1_Right = dtbValue.Rows[0]["INAMOUNT1_Right"].ToString();//入量数量1';
                    objRecordContent.m_strINAMOUNT2_Right = dtbValue.Rows[0]["INAMOUNT2_Right"].ToString();//入量数量2';
                    objRecordContent.m_strINAMOUNT3_Right = dtbValue.Rows[0]["INAMOUNT3_Right"].ToString();//入量数量3';
                    objRecordContent.m_strINAMOUNT4_Right = dtbValue.Rows[0]["INAMOUNT4_Right"].ToString();//入量数量4';
                    objRecordContent.m_strOUTNAME1_Right = dtbValue.Rows[0]["OUTNAME1_Right"].ToString();//出量名称1';
                    objRecordContent.m_strOUTNAME2_Right = dtbValue.Rows[0]["OUTNAME2_Right"].ToString();//出量名称2';
                    objRecordContent.m_strOUTNAME3_Right = dtbValue.Rows[0]["OUTNAME3_Right"].ToString();//出量名称3';
                    objRecordContent.m_strOUTNAME4_Right = dtbValue.Rows[0]["OUTNAME4_Right"].ToString();//出量名称4';
                    objRecordContent.m_strOUTAMOUNT1_Right = dtbValue.Rows[0]["OUTAMOUNT1_Right"].ToString();//出量数量1';
                    objRecordContent.m_strOUTAMOUNT2_Right = dtbValue.Rows[0]["OUTAMOUNT2_Right"].ToString();//出量数量2';
                    objRecordContent.m_strOUTAMOUNT3_Right = dtbValue.Rows[0]["OUTAMOUNT3_Right"].ToString();//出量数量3';
                    objRecordContent.m_strOUTAMOUNT4_Right = dtbValue.Rows[0]["OUTAMOUNT4_Right"].ToString();//出量数量4';
                    objRecordContent.m_strBFI02PEEPRIGHT_Right = dtbValue.Rows[0]["BFI02PEEPRIGHT_Right"].ToString();//PEEP
                    objRecordContent.m_strBPHLEGMAMOUNT_Right = dtbValue.Rows[0]["BPHLEGMAMOUNT_Right"].ToString();//痰量

                    objRecordContent.m_strPPUPLRIGHT = dtbValue.Rows[0]["PPUPLRIGHT"].ToString();  //瞳孔(右)
                    objRecordContent.m_strPREFLECTRIGHT = dtbValue.Rows[0]["PREFLECTRIGHT"].ToString();//对光反射(右)'
                    objRecordContent.m_fltIBLOODPRODUCE = float.Parse(dtbValue.Rows[0]["IBLOODPRODUCE"].ToString());//血制品';
                    objRecordContent.m_fltIBLOODPRODUCEADD = float.Parse(dtbValue.Rows[0]["IBLOODPRODUCEADD"].ToString());// '血制品累计量';
                    objRecordContent.m_strINNAME1 = dtbValue.Rows[0]["INNAME1"].ToString();//入量名称1';
                    objRecordContent.m_strINNAME2 = dtbValue.Rows[0]["INNAME2"].ToString();//入量名称2';
                    objRecordContent.m_strINNAME3 = dtbValue.Rows[0]["INNAME3"].ToString();//入量名称3';
                    objRecordContent.m_strINNAME4 = dtbValue.Rows[0]["INNAME4"].ToString();//入量名称4';
                    objRecordContent.m_fltINAMOUNT1 = float.Parse(dtbValue.Rows[0]["INAMOUNT1"].ToString());//入量数量1';
                    objRecordContent.m_fltINAMOUNT2 = float.Parse(dtbValue.Rows[0]["INAMOUNT2"].ToString());//入量数量2';
                    objRecordContent.m_fltINAMOUNT3 = float.Parse(dtbValue.Rows[0]["INAMOUNT3"].ToString());//入量数量3';
                    objRecordContent.m_fltINAMOUNT4 = float.Parse(dtbValue.Rows[0]["INAMOUNT4"].ToString());//入量数量4';
                    objRecordContent.m_strOUTNAME1 = dtbValue.Rows[0]["OUTNAME1"].ToString();//出量名称1';
                    objRecordContent.m_strOUTNAME2 = dtbValue.Rows[0]["OUTNAME2"].ToString();//出量名称2';
                    objRecordContent.m_strOUTNAME3 = dtbValue.Rows[0]["OUTNAME3"].ToString();//出量名称3';
                    objRecordContent.m_strOUTNAME4 = dtbValue.Rows[0]["OUTNAME4"].ToString();//出量名称4';
                    objRecordContent.m_fltOUTAMOUNT1 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT1"].ToString());//出量数量1';
                    objRecordContent.m_fltOUTAMOUNT2 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT2"].ToString());//出量数量2';
                    objRecordContent.m_fltOUTAMOUNT3 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT3"].ToString());//出量数量3';
                    objRecordContent.m_fltOUTAMOUNT4 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT4"].ToString());//出量数量4';
                    objRecordContent.m_strBFI02PEEPRIGHT = dtbValue.Rows[0]["BFI02PEEPRIGHT"].ToString();//PEEP
                    objRecordContent.m_strBPHLEGMAMOUNT = dtbValue.Rows[0]["BPHLEGMAMOUNT"].ToString();//痰量

                    objRecordContent.m_strPPUPLRIGHTXML = dtbValue.Rows[0]["PPUPLRIGHTXML"].ToString();  //瞳孔(右)
                    objRecordContent.m_strPREFLECTRIGHTXML = dtbValue.Rows[0]["PREFLECTRIGHTXML"].ToString();//对光反射(右)'
                    objRecordContent.m_strIBLOODPRODUCEXML = dtbValue.Rows[0]["IBLOODPRODUCEXML"].ToString();//血制品';
                    objRecordContent.m_strIBLOODPRODUCEADDXML = dtbValue.Rows[0]["IBLOODPRODUCEADDXML"].ToString();// '血制品累计量';
                    objRecordContent.m_strINNAME1XML = dtbValue.Rows[0]["INNAME1XML"].ToString();//入量名称1';
                    objRecordContent.m_strINNAME2XML = dtbValue.Rows[0]["INNAME2XML"].ToString();//入量名称2';
                    objRecordContent.m_strINNAME3XML = dtbValue.Rows[0]["INNAME3XML"].ToString();//入量名称3';
                    objRecordContent.m_strINNAME4XML = dtbValue.Rows[0]["INNAME4XML"].ToString();//入量名称4';
                    objRecordContent.m_strINAMOUNT1XML = dtbValue.Rows[0]["INAMOUNT1XML"].ToString();//入量数量1';
                    objRecordContent.m_strINAMOUNT2XML = dtbValue.Rows[0]["INAMOUNT2XML"].ToString();//入量数量2';
                    objRecordContent.m_strINAMOUNT3XML = dtbValue.Rows[0]["INAMOUNT3XML"].ToString();//入量数量3';
                    objRecordContent.m_strINAMOUNT4XML = dtbValue.Rows[0]["INAMOUNT4XML"].ToString();//入量数量4';
                    objRecordContent.m_strOUTNAME1XML = dtbValue.Rows[0]["OUTNAME1XML"].ToString();//出量名称1';
                    objRecordContent.m_strOUTNAME2XML = dtbValue.Rows[0]["OUTNAME2XML"].ToString();//出量名称2';
                    objRecordContent.m_strOUTNAME3XML = dtbValue.Rows[0]["OUTNAME3XML"].ToString();//出量名称3';
                    objRecordContent.m_strOUTNAME4XML = dtbValue.Rows[0]["OUTNAME4XML"].ToString();//出量名称4';
                    objRecordContent.m_strOUTAMOUNT1XML = dtbValue.Rows[0]["OUTAMOUNT1XML"].ToString();//出量数量1';
                    objRecordContent.m_strOUTAMOUNT2XML = dtbValue.Rows[0]["OUTAMOUNT2XML"].ToString();//出量数量2';
                    objRecordContent.m_strOUTAMOUNT3XML = dtbValue.Rows[0]["OUTAMOUNT3XML"].ToString();//出量数量3';
                    objRecordContent.m_strOUTAMOUNT4XML = dtbValue.Rows[0]["OUTAMOUNT4XML"].ToString();//出量数量4';
                    objRecordContent.m_strBFI02PEEPRIGHTXML = dtbValue.Rows[0]["BFI02PEEPRIGHTXML"].ToString();//PEEP
                    objRecordContent.m_strBPHLEGMAMOUNTXML = dtbValue.Rows[0]["BPHLEGMAMOUNTXML"].ToString();//痰量

                    #endregion
                    objRecordContent.m_strWEIGHT = dtbValue.Rows[0]["WEIGHT"].ToString();
                    objRecordContent.m_strIDCODE = dtbValue.Rows[0]["IDCODE"].ToString();
                    objRecordContent.m_strOPERATIONNAME = dtbValue.Rows[0]["OPERATIONNAME"].ToString();
                    if (dtbValue.Rows[0]["OPERATIONDATE"].ToString().Trim().Length > 0)
                        objRecordContent.m_strOPERATIONDATE = DateTime.Parse(dtbValue.Rows[0]["OPERATIONDATE"].ToString().Trim());
                    else
                        objRecordContent.m_strOPERATIONDATE = DateTime.Parse("1900-01-01");

                    objRecordContent.m_strDATEAFTEROPERATION = dtbValue.Rows[0]["DATEAFTEROPERATION"].ToString();

                    p_objRecordContent = objRecordContent;
                }
                //返回
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
		/// 查看是否有相同的记录时间
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
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

                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //string strSQL = "select CreateUserID,OpenDate from GeneralNurseRecord Where trim(InPatientID) = ? and InPatientDate= ? and CreateDate= ? and Status=0";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
                //返回	
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
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
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
				//检查参数                              
				if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
					return (long)enmOperationResult.Parameter_Error;
				//把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
				clsISURGERYICUWARDSHIP objContent = (clsISURGERYICUWARDSHIP)p_objRecordContent;
		
				//获取IDataParameter数组
				//			string strSQL = @"insert into  GeneralNurseRecord(InPatientID,InPatientDate,OpenDate,CreateDate,CreateUserID,IfConfirm,ConfirmReason,ConfirmReasonXML,Status,RecordTitle,RecordTitleType,RecordContent,RecordContentXML) 
				//				values(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				//IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[11];
				IDataParameter[] objDPArr = null;
				 objHRPServ.CreateDatabaseParameter(175,out objDPArr);
				#region
                 objDPArr[0].Value = objContent.m_strInPatientID;
                 objDPArr[1].DbType = DbType.DateTime;
                 objDPArr[1].Value = objContent.m_dtmInPatientDate;
                 objDPArr[2].DbType = DbType.DateTime;
                 objDPArr[2].Value = objContent.m_dtmOpenDate;
                 objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value= objContent.m_dtmCreateDate;//System.DateTime.Now ;//;// ;
				objDPArr[4].Value=objContent.m_strCreateUserID;
				
				objDPArr[5].Value=objContent.m_strPBODYPART;
				objDPArr[6].Value=objContent.m_strPCONSCIOUSNESS;
				objDPArr[7].Value=objContent.m_strPPUPIL;
				objDPArr[8].Value=objContent.m_strPREFLECT;

				objDPArr[9].Value=objContent.m_strCTEMPERATURE;
				objDPArr[10].Value=objContent.m_strCSMALLTEMPERATURE;
				objDPArr[11].Value=objContent.m_strCHEARTRATE;
				objDPArr[12].Value=objContent.m_strCHEARTRHYTHM;

				objDPArr[13].Value=objContent.m_strCSD;
				objDPArr[14].Value=objContent.m_strCCVP;
				objDPArr[15].Value=objContent.m_strDPHYSIC1;
				objDPArr[16].Value=objContent.m_strDPHYSIC2;

				objDPArr[17].Value=objContent.m_strDPHYSIC3;
				objDPArr[18].Value=objContent.m_strDPHYSIC4;
				objDPArr[19].Value=objContent.m_strDPHYSIC5;
				objDPArr[20].Value=objContent.m_strDPHYSIC6;

				objDPArr[21].Value=objContent.m_strDPHYSIC7;
				objDPArr[22].Value=objContent.m_strDPHYSIC8;
				objDPArr[23].Value=objContent.m_strDCURE1;
				objDPArr[24].Value=objContent.m_strDCURE2;

				objDPArr[25].Value=objContent.m_strDCURE3;
				objDPArr[26].Value=objContent.m_strDCURE4;
				objDPArr[27].Value=objContent.m_strDCURE5;
				objDPArr[28].Value=objContent.m_strDCURE6;
				objDPArr[29].Value=objContent.m_strDCURE7;
				objDPArr[30].Value=objContent.m_strDCURE8;

				objDPArr[31].Value=objContent.m_fltIGS;
				objDPArr[32].Value=objContent.m_fltINS;
				objDPArr[33].Value=objContent.m_fltINTATAL;
				objDPArr[34].Value=objContent.m_fltOTATAL;

				objDPArr[35].Value=objContent.m_fltOEMIEMCTION;
				objDPArr[36].Value=objContent.m_fltOGASTRICJUICE;
				objDPArr[37].Value=objContent.m_strSESPECIALLYNOTE;
				objDPArr[38].Value=objContent.m_strBBLUSETIME;

				objDPArr[39].Value=objContent.m_strBBLUSEMACHINETYPE;
				objDPArr[40].Value=objContent.m_strBBLUSEMODE;
				objDPArr[41].Value=objContent.m_strBVT;
				objDPArr[42].Value=objContent.m_strBEXPIREDMV;
				objDPArr[43].Value=objContent.m_strBBLUESPRESSURE;
				objDPArr[44].Value=objContent.m_strBBLUSENUM;

				objDPArr[45].Value=objContent.m_strBFIO2PEEP;
				objDPArr[46].Value=objContent.m_strBMAXIP;
				objDPArr[47].Value=objContent.m_strBBLUSESOUND;
				objDPArr[48].Value=objContent.m_strBPHLEGMCOLOR;

				objDPArr[49].Value=objContent.m_strBSQ2;
				objDPArr[50].Value=objContent.m_strTCOLLECTBLOODPOINT;
				objDPArr[51].Value=objContent.m_strTPH;
				objDPArr[52].Value=objContent.m_strTPCO2;

				objDPArr[53].Value=objContent.m_strTP02;
				objDPArr[54].Value=objContent.m_strTHCO3;
				objDPArr[55].Value=objContent.m_strTTCO2;
				objDPArr[56].Value=objContent.m_strTBE;
				objDPArr[57].Value=objContent.m_strTSAT;
				objDPArr[58].Value=objContent.m_strTO2CT;

				objDPArr[59].Value=objContent.m_strSCMH2O;
				objDPArr[60].Value=objContent.m_strSSD;
				objDPArr[61].Value=objContent.m_strSMEAN;
				objDPArr[62].Value=objContent.m_strSWEDGE;
				objDPArr[63].Value=objContent.m_strSCOCI;

				objDPArr[64].Value=objContent.m_strPBODYPARTXML;
				objDPArr[65].Value=objContent.m_strPCONSCIOUSNESSXML;
				objDPArr[66].Value=objContent.m_strPPUPILXML;
				objDPArr[67].Value=objContent.m_strPREFLECTXML;

				objDPArr[68].Value=objContent.m_strCTEMPERATUREXML;
				objDPArr[69].Value=objContent.m_strCSMALLTEMPERATUREXML;
				objDPArr[70].Value=objContent.m_strCHEARTRATEXML;
				objDPArr[71].Value=objContent.m_strCHEARTRHYTHMXML;

				objDPArr[72].Value=objContent.m_strCSDXML;
				objDPArr[73].Value=objContent.m_strCCVPXML;
				objDPArr[74].Value=objContent.m_strIGSXML;
				objDPArr[75].Value=objContent.m_strINSXML;

				objDPArr[76].Value=objContent.m_strINTATALXML;
				objDPArr[77].Value=objContent.m_strOTATALXML;
				objDPArr[78].Value=objContent.m_strOEMEMCTIONXML;
				objDPArr[79].Value=objContent.m_strOGASTRICJUICEXML;

				objDPArr[80].Value=objContent.m_strSESPECIALLYNOTEXML;
				objDPArr[81].Value=objContent.m_strBBLUSETIMEXML;
				objDPArr[82].Value=objContent.m_strBBLUSEMACHINETYPEXML;
				objDPArr[83].Value=objContent.m_strBBLUSEMONDEXML;

				objDPArr[84].Value=objContent.m_strBVTXML;
				objDPArr[85].Value=objContent.m_strBEXPIREDMVXML;
				objDPArr[86].Value=objContent.m_strBBLUESPRESSUREXML;
				objDPArr[87].Value=objContent.m_strBBLUSENUMXML;
				objDPArr[88].Value=objContent.m_strBFIO2PEEPXML;
				objDPArr[89].Value=objContent.m_strBMAXIPXML;

				objDPArr[90].Value=objContent.m_strBBLUSESOUNDXML;
				objDPArr[91].Value=objContent.m_strBPHLEGMCOLORXML;
				objDPArr[92].Value=objContent.m_strBSQ2XML;
				objDPArr[93].Value=objContent.m_strTCOLLECTBLOODPOINTXML;

				objDPArr[94].Value=objContent.m_strTPHXML;
				objDPArr[95].Value=objContent.m_strTPCO2XML;
				objDPArr[96].Value=objContent.m_strTPO2XML;
				objDPArr[97].Value=objContent.m_strTHCO3XML;

				objDPArr[98].Value=objContent.m_strTTCO2XML;
				objDPArr[99].Value=objContent.m_strTBEXML;
				objDPArr[100].Value=objContent.m_strTSATXML;
				objDPArr[101].Value=objContent.m_strTO2CTXML;
				objDPArr[102].Value=objContent.m_strSCMH2OXML;
				objDPArr[103].Value=objContent.m_strSSDXML;

				objDPArr[104].Value=objContent.m_strSMEANXML;
				objDPArr[105].Value=objContent.m_strSWEDGEXML;
				objDPArr[106].Value=objContent.m_strSCOCIXML;

				
				objDPArr[107].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[108].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[109].Value=objContent.m_strDPHYSIC1XML;

				objDPArr[110].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[111].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[112].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[113].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[114].Value=objContent.m_strDPHYSIC1XML;
				objDPArr[115].Value=objContent.m_strDCURE1XML;

				objDPArr[116].Value=objContent.m_strDCURE1XML;
				objDPArr[117].Value=objContent.m_strDCURE1XML;
				objDPArr[118].Value=objContent.m_strDCURE1XML;
				objDPArr[119].Value=objContent.m_strDCURE1XML;
				objDPArr[120].Value=objContent.m_strDCURE1XML;
				objDPArr[121].Value=objContent.m_strDCURE1XML;
				objDPArr[122].Value=objContent.m_strDCURE1XML;
				
				objDPArr[123].Value=objContent.m_strFIRSTPRITDATE;
                objDPArr[124].DbType = DbType.DateTime;
				objDPArr[124].Value=objContent.m_dtmRECORDDATE;
				objDPArr[125].Value=0;//objContent.m_strSTATUS;

				objDPArr[126].Value=objContent.m_strWEIGHT;
				objDPArr[127].Value=objContent.m_strIDCODE;
				objDPArr[128].Value=objContent.m_strOPERATIONNAME;
				objDPArr[129].Value=objContent.m_strOPERATIONDATE;
				objDPArr[130].Value=objContent.m_strDATEAFTEROPERATION;

				objDPArr[131].Value=objContent.m_strPPUPLRIGHT;  //瞳孔(右)
				objDPArr[132].Value=objContent.m_strPREFLECTRIGHT;//对光反射(右)'
				objDPArr[133].Value=objContent.m_fltIBLOODPRODUCE;//血制品';
				objDPArr[134].Value=objContent.m_fltIBLOODPRODUCEADD;// '血制品累计量';
				objDPArr[135].Value=objContent.m_strINNAME1;//入量名称1';
				objDPArr[136].Value=objContent.m_strINNAME2;//入量名称2';
				objDPArr[137].Value=objContent.m_strINNAME3;//入量名称3';
				objDPArr[138].Value=objContent.m_strINNAME4;//入量名称4';
				objDPArr[139].Value=objContent.m_fltINAMOUNT1;//入量数量1';
				objDPArr[140].Value=objContent.m_fltINAMOUNT2;//入量数量2';
				objDPArr[141].Value=objContent.m_fltINAMOUNT3;//入量数量3';
				objDPArr[142].Value=objContent.m_fltINAMOUNT4;//入量数量4';
				objDPArr[143].Value=objContent.m_strOUTNAME1;//出量名称1';
				objDPArr[144].Value=objContent.m_strOUTNAME2;//出量名称2';
				objDPArr[145].Value=objContent.m_strOUTNAME3;//出量名称3';
				objDPArr[146].Value=objContent.m_strOUTNAME4;//出量名称4';
				objDPArr[147].Value=objContent.m_fltOUTAMOUNT1;//出量数量1';
				objDPArr[148].Value=objContent.m_fltOUTAMOUNT2;//出量数量2';
				objDPArr[149].Value=objContent.m_fltOUTAMOUNT3;//出量数量3';
				objDPArr[150].Value=objContent.m_fltOUTAMOUNT4;//出量数量4';
				objDPArr[151].Value=objContent.m_strBFI02PEEPRIGHT;//PEEP
				objDPArr[152].Value=objContent.m_strBPHLEGMAMOUNT;//痰量

				objDPArr[153].Value=objContent.m_strPPUPLRIGHTXML;  //瞳孔(右)
				objDPArr[154].Value=objContent.m_strPREFLECTRIGHTXML;//对光反射(右)'
				objDPArr[155].Value=objContent.m_strIBLOODPRODUCEXML;//血制品';
				objDPArr[156].Value=objContent.m_strIBLOODPRODUCEADDXML;// '血制品累计量';
				objDPArr[157].Value=objContent.m_strINNAME1XML;//入量名称1';
				objDPArr[158].Value=objContent.m_strINNAME2XML;//入量名称2';
				objDPArr[159].Value=objContent.m_strINNAME3XML;//入量名称3';
				objDPArr[160].Value=objContent.m_strINNAME4XML;//入量名称4';
				objDPArr[161].Value=objContent.m_strINAMOUNT1XML;//入量数量1';
				objDPArr[162].Value=objContent.m_strINAMOUNT2XML;//入量数量2';
				objDPArr[163].Value=objContent.m_strINAMOUNT3XML;//入量数量3';
				objDPArr[164].Value=objContent.m_strINAMOUNT4XML;//入量数量4';
				objDPArr[165].Value=objContent.m_strOUTNAME1XML;//出量名称1';
				objDPArr[166].Value=objContent.m_strOUTNAME2XML;//出量名称2';
				objDPArr[167].Value=objContent.m_strOUTNAME3XML;//出量名称3';
				objDPArr[168].Value=objContent.m_strOUTNAME4XML;//出量名称4';
				objDPArr[169].Value=objContent.m_strOUTAMOUNT1XML;//出量数量1';
				objDPArr[170].Value=objContent.m_strOUTAMOUNT2XML;//出量数量2';
				objDPArr[171].Value=objContent.m_strOUTAMOUNT3XML;//出量数量3';
				objDPArr[172].Value=objContent.m_strOUTAMOUNT4XML;//出量数量4';
				objDPArr[173].Value=objContent.m_strBFI02PEEPRIGHTXML;//PEEP
				objDPArr[174].Value=objContent.m_strBPHLEGMAMOUNTXML;//痰量
				#endregion
				
				
				
				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[6];
				//按顺序给IDataParameter赋值
				//				for(int i=0;i<objDPArr2.Length;i++)
				//					objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
				p_objHRPServ.CreateDatabaseParameter(86,out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) ;//objContent.m_dtmMODIFYDATE;//m_dtmCreateDate;
				objDPArr2[4].Value=objContent.m_strCreateUserID;
				
				objDPArr2[5].Value=objContent.m_strPBODYPART;
				objDPArr2[6].Value=objContent.m_strPCONSCIOUSNESS;
				objDPArr2[7].Value=objContent.m_strPPUPIL;
				objDPArr2[8].Value=objContent.m_strPREFLECT;

				objDPArr2[9].Value=objContent.m_strCTEMPERATURE;
				objDPArr2[10].Value=objContent.m_strCSMALLTEMPERATURE;
				objDPArr2[11].Value=objContent.m_strCHEARTRATE;
				objDPArr2[12].Value=objContent.m_strCHEARTRHYTHM;

				objDPArr2[13].Value=objContent.m_strCSD;
				objDPArr2[14].Value=objContent.m_strCCVP;
				objDPArr2[15].Value=objContent.m_strDPHYSIC1;
				objDPArr2[16].Value=objContent.m_strDPHYSIC2;

				objDPArr2[17].Value=objContent.m_strDPHYSIC3;
				objDPArr2[18].Value=objContent.m_strDPHYSIC4;
				objDPArr2[19].Value=objContent.m_strDPHYSIC5;
				objDPArr2[20].Value=objContent.m_strDPHYSIC6;

				objDPArr2[21].Value=objContent.m_strDPHYSIC7;
				objDPArr2[22].Value=objContent.m_strDPHYSIC8;
				objDPArr2[23].Value=objContent.m_strDCURE1;
				objDPArr2[24].Value=objContent.m_strDCURE2;

				objDPArr2[25].Value=objContent.m_strDCURE3;
				objDPArr2[26].Value=objContent.m_strDCURE4;
				objDPArr2[27].Value=objContent.m_strDCURE5;
				objDPArr2[28].Value=objContent.m_strDCURE6;
				objDPArr2[29].Value=objContent.m_strDCURE7;
				objDPArr2[30].Value=objContent.m_strDCURE8;

				objDPArr2[31].Value=objContent.m_fltIGS;
				objDPArr2[32].Value=objContent.m_fltINS;
				objDPArr2[33].Value=objContent.m_fltINTATAL;
				objDPArr2[34].Value=objContent.m_fltOTATAL;

				objDPArr2[35].Value=objContent.m_fltOEMIEMCTION;
				objDPArr2[36].Value=objContent.m_fltOGASTRICJUICE;
				objDPArr2[37].Value=objContent.m_strSESPECIALLYNOTE;
				objDPArr2[38].Value=objContent.m_strBBLUSETIME;

				objDPArr2[39].Value=objContent.m_strBBLUSEMACHINETYPE;
				objDPArr2[40].Value=objContent.m_strBBLUSEMODE;
				objDPArr2[41].Value=objContent.m_strBVT;
				objDPArr2[42].Value=objContent.m_strBEXPIREDMV;
				objDPArr2[43].Value=objContent.m_strBBLUESPRESSURE;
				objDPArr2[44].Value=objContent.m_strBBLUSENUM;

				objDPArr2[45].Value=objContent.m_strBFIO2PEEP;
				objDPArr2[46].Value=objContent.m_strBMAXIP;
				objDPArr2[47].Value=objContent.m_strBBLUSESOUND;
				objDPArr2[48].Value=objContent.m_strBPHLEGMCOLOR;

				objDPArr2[49].Value=objContent.m_strBSQ2;
				objDPArr2[50].Value=objContent.m_strTCOLLECTBLOODPOINT;
				objDPArr2[51].Value=objContent.m_strTPH;
				objDPArr2[52].Value=objContent.m_strTPCO2;

				objDPArr2[53].Value=objContent.m_strTP02;
				objDPArr2[54].Value=objContent.m_strTHCO3;
				objDPArr2[55].Value=objContent.m_strTTCO2;
				objDPArr2[56].Value=objContent.m_strTBE;
				objDPArr2[57].Value=objContent.m_strTSAT;
				objDPArr2[58].Value=objContent.m_strTO2CT;

				objDPArr2[59].Value=objContent.m_strSCMH2O;
				objDPArr2[60].Value=objContent.m_strSSD;
				objDPArr2[61].Value=objContent.m_strSMEAN;
				objDPArr2[62].Value=objContent.m_strSWEDGE;
				objDPArr2[63].Value=objContent.m_strSCOCI;
		
				objDPArr2[64].Value=objContent.m_strPPUPLRIGHT;  //瞳孔(右)
				objDPArr2[65].Value=objContent.m_strPREFLECTRIGHT;//对光反射(右)'
				objDPArr2[66].Value=objContent.m_fltIBLOODPRODUCE;//血制品';
				objDPArr2[67].Value=objContent.m_fltIBLOODPRODUCEADD;// '血制品累计量';
				objDPArr2[68].Value=objContent.m_strINNAME1;//入量名称1';
				objDPArr2[69].Value=objContent.m_strINNAME2;//入量名称2';
				objDPArr2[70].Value=objContent.m_strINNAME3;//入量名称3';
				objDPArr2[71].Value=objContent.m_strINNAME4;//入量名称4';
				objDPArr2[72].Value=objContent.m_fltINAMOUNT1;//入量数量1';
				objDPArr2[73].Value=objContent.m_fltINAMOUNT2;//入量数量2';
				objDPArr2[74].Value=objContent.m_fltINAMOUNT3;//入量数量3';
				objDPArr2[75].Value=objContent.m_fltINAMOUNT4;//入量数量4';
				objDPArr2[76].Value=objContent.m_strOUTNAME1;//出量名称1';
				objDPArr2[77].Value=objContent.m_strOUTNAME2;//出量名称2';
				objDPArr2[78].Value=objContent.m_strOUTNAME3;//出量名称3';
				objDPArr2[79].Value=objContent.m_strOUTNAME4;//出量名称4';
				objDPArr2[80].Value=objContent.m_fltOUTAMOUNT1;//出量数量1';
				objDPArr2[81].Value=objContent.m_fltOUTAMOUNT2;//出量数量2';
				objDPArr2[82].Value=objContent.m_fltOUTAMOUNT3;//出量数量3';
				objDPArr2[83].Value=objContent.m_fltOUTAMOUNT4;//出量数量4';
				objDPArr2[84].Value=objContent.m_strBFI02PEEPRIGHT;//PEEP
				objDPArr2[85].Value=objContent.m_strBPHLEGMAMOUNT;//痰量
				//执行SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL,ref lngEff,objDPArr2);
				if(lngRes<=0)return lngRes;
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);}
	
	    finally
	    {
	      //objHRPServ.Dispose();

	    }		

			return lngRes;
		}

		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>		
		/// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
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
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //获取IDataParameter数组			
                //string strSQL = "select top 1 ModifyDate,ModifyUserID from GeneralNurseRecordContent Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=0 order by ModifyDate desc";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from GeneralNurseRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

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
                    //if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
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

            }

			return lngRes;			
		}

		/// <summary>
		/// 把新修改的内容保存到数据库。更新主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
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
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                //clsGeneralNurseRecordContent objContent = (clsGeneralNurseRecordContent)p_objRecordContent;
                clsISURGERYICUWARDSHIP objContent = (clsISURGERYICUWARDSHIP)p_objRecordContent;

                //获取IDataParameter数组
                //string strSQL = "Update GeneralNurseRecord Set RecordTitle=?,RecordTitleType=? ,RecordContent=?,RecordContentXML=? Where trim(InPatientID)=? and InPatientDate=? and OpenDate=? and Status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?,

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[5];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(173, out objDPArr); //


                objDPArr[0].Value = objContent.m_strPBODYPART;
                objDPArr[1].Value = objContent.m_strPCONSCIOUSNESS;
                objDPArr[2].Value = objContent.m_strPPUPIL;
                objDPArr[3].Value = objContent.m_strPREFLECT;

                objDPArr[4].Value = objContent.m_strCTEMPERATURE;
                objDPArr[5].Value = objContent.m_strCSMALLTEMPERATURE;
                objDPArr[6].Value = objContent.m_strCHEARTRATE;
                objDPArr[7].Value = objContent.m_strCHEARTRHYTHM;

                objDPArr[8].Value = objContent.m_strCSD;
                objDPArr[9].Value = objContent.m_strCCVP;
                objDPArr[10].Value = objContent.m_strDPHYSIC1;
                objDPArr[11].Value = objContent.m_strDPHYSIC2;

                objDPArr[12].Value = objContent.m_strDPHYSIC3;
                objDPArr[13].Value = objContent.m_strDPHYSIC4;
                objDPArr[14].Value = objContent.m_strDPHYSIC5;
                objDPArr[15].Value = objContent.m_strDPHYSIC6;

                objDPArr[16].Value = objContent.m_strDPHYSIC7;
                objDPArr[17].Value = objContent.m_strDPHYSIC8;
                objDPArr[18].Value = objContent.m_strDCURE1;
                objDPArr[19].Value = objContent.m_strDCURE2;

                objDPArr[20].Value = objContent.m_strDCURE3;
                objDPArr[21].Value = objContent.m_strDCURE4;
                objDPArr[22].Value = objContent.m_strDCURE5;
                objDPArr[23].Value = objContent.m_strDCURE6;
                objDPArr[24].Value = objContent.m_strDCURE7;
                objDPArr[25].Value = objContent.m_strDCURE8;

                objDPArr[26].Value = objContent.m_fltIGS;
                objDPArr[27].Value = objContent.m_fltINS;
                objDPArr[28].Value = objContent.m_fltINTATAL;
                objDPArr[29].Value = objContent.m_fltOTATAL;

                objDPArr[30].Value = objContent.m_fltOEMIEMCTION;
                objDPArr[31].Value = objContent.m_fltOGASTRICJUICE;
                objDPArr[32].Value = objContent.m_strSESPECIALLYNOTE;
                objDPArr[33].Value = objContent.m_strBBLUSETIME;

                objDPArr[34].Value = objContent.m_strBBLUSEMACHINETYPE;
                objDPArr[35].Value = objContent.m_strBBLUSEMODE;
                objDPArr[36].Value = objContent.m_strBVT;
                objDPArr[37].Value = objContent.m_strBEXPIREDMV;
                objDPArr[38].Value = objContent.m_strBBLUESPRESSURE;
                objDPArr[39].Value = objContent.m_strBBLUSENUM;

                objDPArr[40].Value = objContent.m_strBFIO2PEEP;
                objDPArr[41].Value = objContent.m_strBMAXIP;
                objDPArr[42].Value = objContent.m_strBBLUSESOUND;
                objDPArr[43].Value = objContent.m_strBPHLEGMCOLOR;

                objDPArr[44].Value = objContent.m_strBSQ2;
                objDPArr[45].Value = objContent.m_strTCOLLECTBLOODPOINT;
                objDPArr[46].Value = objContent.m_strTPH;
                objDPArr[47].Value = objContent.m_strTPCO2;

                objDPArr[48].Value = objContent.m_strTP02;
                objDPArr[49].Value = objContent.m_strTHCO3;
                objDPArr[50].Value = objContent.m_strTTCO2;
                objDPArr[51].Value = objContent.m_strTBE;
                objDPArr[52].Value = objContent.m_strTSAT;
                objDPArr[53].Value = objContent.m_strTO2CT;

                objDPArr[54].Value = objContent.m_strSCMH2O;
                objDPArr[55].Value = objContent.m_strSSD;
                objDPArr[56].Value = objContent.m_strSMEAN;
                objDPArr[57].Value = objContent.m_strSWEDGE;
                objDPArr[58].Value = objContent.m_strSCOCI;

                objDPArr[59].Value = objContent.m_strPBODYPARTXML;
                objDPArr[60].Value = objContent.m_strPCONSCIOUSNESSXML;
                objDPArr[61].Value = objContent.m_strPPUPILXML;
                objDPArr[62].Value = objContent.m_strPREFLECTXML;

                objDPArr[63].Value = objContent.m_strCTEMPERATUREXML;
                objDPArr[64].Value = objContent.m_strCSMALLTEMPERATUREXML;
                objDPArr[65].Value = objContent.m_strCHEARTRATEXML;
                objDPArr[66].Value = objContent.m_strCHEARTRHYTHMXML;

                objDPArr[67].Value = objContent.m_strCSDXML;
                objDPArr[68].Value = objContent.m_strCCVPXML;
                objDPArr[69].Value = objContent.m_strIGSXML;
                objDPArr[70].Value = objContent.m_strINSXML;

                objDPArr[71].Value = objContent.m_strINTATALXML;
                objDPArr[72].Value = objContent.m_strOTATALXML;
                objDPArr[73].Value = objContent.m_strOEMEMCTIONXML;
                objDPArr[74].Value = objContent.m_strOGASTRICJUICEXML;

                objDPArr[75].Value = objContent.m_strSESPECIALLYNOTEXML;
                objDPArr[76].Value = objContent.m_strBBLUSETIMEXML;
                objDPArr[77].Value = objContent.m_strBBLUSEMACHINETYPEXML;
                objDPArr[78].Value = objContent.m_strBBLUSEMONDEXML;

                objDPArr[79].Value = objContent.m_strBVTXML;
                objDPArr[80].Value = objContent.m_strBEXPIREDMVXML;
                objDPArr[81].Value = objContent.m_strBBLUESPRESSUREXML;
                objDPArr[82].Value = objContent.m_strBBLUSENUMXML;
                objDPArr[83].Value = objContent.m_strBFIO2PEEPXML;
                objDPArr[84].Value = objContent.m_strBMAXIPXML;

                objDPArr[85].Value = objContent.m_strBBLUSESOUNDXML;
                objDPArr[86].Value = objContent.m_strBPHLEGMCOLORXML;
                objDPArr[87].Value = objContent.m_strBSQ2XML;
                objDPArr[88].Value = objContent.m_strTCOLLECTBLOODPOINTXML;

                objDPArr[89].Value = objContent.m_strTPHXML;
                objDPArr[90].Value = objContent.m_strTPCO2XML;
                objDPArr[91].Value = objContent.m_strTPO2XML;
                objDPArr[92].Value = objContent.m_strTHCO3XML;

                objDPArr[93].Value = objContent.m_strTTCO2XML;
                objDPArr[94].Value = objContent.m_strTBEXML;
                objDPArr[95].Value = objContent.m_strTSATXML;
                objDPArr[96].Value = objContent.m_strTO2CTXML;
                objDPArr[97].Value = objContent.m_strSCMH2OXML;
                objDPArr[98].Value = objContent.m_strSSDXML;

                objDPArr[99].Value = objContent.m_strSMEANXML;
                objDPArr[100].Value = objContent.m_strSWEDGEXML;
                objDPArr[101].Value = objContent.m_strSCOCIXML;


                objDPArr[102].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[103].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[104].Value = objContent.m_strDPHYSIC1XML;

                objDPArr[105].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[106].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[107].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[108].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[109].Value = objContent.m_strDPHYSIC1XML;
                objDPArr[110].Value = objContent.m_strDCURE1XML;

                objDPArr[111].Value = objContent.m_strDCURE1XML;
                objDPArr[112].Value = objContent.m_strDCURE1XML;
                objDPArr[113].Value = objContent.m_strDCURE1XML;
                objDPArr[114].Value = objContent.m_strDCURE1XML;
                objDPArr[115].Value = objContent.m_strDCURE1XML;
                objDPArr[116].Value = objContent.m_strDCURE1XML;
                objDPArr[117].Value = objContent.m_strDCURE1XML;

                objDPArr[118].Value = objContent.m_strFIRSTPRITDATE;
                objDPArr[119].DbType = DbType.DateTime;
                objDPArr[119].Value = objContent.m_dtmRECORDDATE;
                objDPArr[120].Value = 0;//objContent.m_strSTATUS;

                objDPArr[121].Value = objContent.m_strWEIGHT;
                objDPArr[122].Value = objContent.m_strIDCODE;
                objDPArr[123].Value = objContent.m_strOPERATIONNAME;
                objDPArr[124].Value = objContent.m_strOPERATIONDATE;
                objDPArr[125].Value = objContent.m_strDATEAFTEROPERATION;



                //objDPArr[3].Value=objContent.m_dtmCreateDate;
                //objDPArr[4].Value=objContent.m_strCreateUserID;

                objDPArr[126].Value = objContent.m_strPPUPLRIGHT;  //瞳孔(右)
                objDPArr[127].Value = objContent.m_strPREFLECTRIGHT;//对光反射(右)'
                objDPArr[128].Value = objContent.m_fltIBLOODPRODUCE;//血制品';
                objDPArr[129].Value = objContent.m_fltIBLOODPRODUCEADD;// '血制品累计量';
                objDPArr[130].Value = objContent.m_strINNAME1;//入量名称1';
                objDPArr[131].Value = objContent.m_strINNAME2;//入量名称2';
                objDPArr[132].Value = objContent.m_strINNAME3;//入量名称3';
                objDPArr[133].Value = objContent.m_strINNAME4;//入量名称4';
                objDPArr[134].Value = objContent.m_fltINAMOUNT1;//入量数量1';
                objDPArr[135].Value = objContent.m_fltINAMOUNT2;//入量数量2';
                objDPArr[136].Value = objContent.m_fltINAMOUNT3;//入量数量3';
                objDPArr[137].Value = objContent.m_fltINAMOUNT4;//入量数量4';
                objDPArr[138].Value = objContent.m_strOUTNAME1;//出量名称1';
                objDPArr[139].Value = objContent.m_strOUTNAME2;//出量名称2';
                objDPArr[140].Value = objContent.m_strOUTNAME3;//出量名称3';
                objDPArr[141].Value = objContent.m_strOUTNAME4;//出量名称4';
                objDPArr[142].Value = objContent.m_fltOUTAMOUNT1;//出量数量1';
                objDPArr[143].Value = objContent.m_fltOUTAMOUNT2;//出量数量2';
                objDPArr[144].Value = objContent.m_fltOUTAMOUNT3;//出量数量3';
                objDPArr[145].Value = objContent.m_fltOUTAMOUNT4;//出量数量4';
                objDPArr[146].Value = objContent.m_strBFI02PEEPRIGHT;//PEEP
                objDPArr[147].Value = objContent.m_strBPHLEGMAMOUNT;//痰量

                objDPArr[148].Value = objContent.m_strPPUPLRIGHTXML;  //瞳孔(右)
                objDPArr[149].Value = objContent.m_strPREFLECTRIGHTXML;//对光反射(右)'
                objDPArr[150].Value = objContent.m_strIBLOODPRODUCEXML;//血制品';
                objDPArr[151].Value = objContent.m_strIBLOODPRODUCEADDXML;// '血制品累计量';
                objDPArr[152].Value = objContent.m_strINNAME1XML;//入量名称1';
                objDPArr[153].Value = objContent.m_strINNAME2XML;//入量名称2';
                objDPArr[154].Value = objContent.m_strINNAME3XML;//入量名称3';
                objDPArr[155].Value = objContent.m_strINNAME4XML;//入量名称4';
                objDPArr[156].Value = objContent.m_strINAMOUNT1XML;//入量数量1';
                objDPArr[157].Value = objContent.m_strINAMOUNT2XML;//入量数量2';
                objDPArr[158].Value = objContent.m_strINAMOUNT3XML;//入量数量3';
                objDPArr[159].Value = objContent.m_strINAMOUNT4XML;//入量数量4';
                objDPArr[160].Value = objContent.m_strOUTNAME1XML;//出量名称1';
                objDPArr[161].Value = objContent.m_strOUTNAME2XML;//出量名称2';
                objDPArr[162].Value = objContent.m_strOUTNAME3XML;//出量名称3';
                objDPArr[163].Value = objContent.m_strOUTNAME4XML;//出量名称4';
                objDPArr[164].Value = objContent.m_strOUTAMOUNT1XML;//出量数量1';
                objDPArr[165].Value = objContent.m_strOUTAMOUNT2XML;//出量数量2';
                objDPArr[166].Value = objContent.m_strOUTAMOUNT3XML;//出量数量3';
                objDPArr[167].Value = objContent.m_strOUTAMOUNT4XML;//出量数量4';
                objDPArr[168].Value = objContent.m_strBFI02PEEPRIGHTXML;//PEEP
                objDPArr[169].Value = objContent.m_strBPHLEGMAMOUNTXML;//痰量

                objDPArr[170].Value = objContent.m_strInPatientID;
                objDPArr[171].DbType = DbType.DateTime;
                objDPArr[171].Value = objContent.m_dtmInPatientDate;
                objDPArr[172].DbType = DbType.DateTime;
                objDPArr[172].Value = objContent.m_dtmOpenDate;
                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                //按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr2.Length;i++)
                //					objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();


                p_objHRPServ.CreateDatabaseParameter(86, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));// objContent.m_dtmMODIFYDATEm_dtmCreateDate;
                objDPArr2[4].Value = objContent.m_strCreateUserID;

                objDPArr2[5].Value = objContent.m_strPBODYPART;
                objDPArr2[6].Value = objContent.m_strPCONSCIOUSNESS;
                objDPArr2[7].Value = objContent.m_strPPUPIL;
                objDPArr2[8].Value = objContent.m_strPREFLECT;

                objDPArr2[9].Value = objContent.m_strCTEMPERATURE;
                objDPArr2[10].Value = objContent.m_strCSMALLTEMPERATURE;
                objDPArr2[11].Value = objContent.m_strCHEARTRATE;
                objDPArr2[12].Value = objContent.m_strCHEARTRHYTHM;

                objDPArr2[13].Value = objContent.m_strCSD;
                objDPArr2[14].Value = objContent.m_strCCVP;
                objDPArr2[15].Value = objContent.m_strDPHYSIC1;
                objDPArr2[16].Value = objContent.m_strDPHYSIC2;

                objDPArr2[17].Value = objContent.m_strDPHYSIC3;
                objDPArr2[18].Value = objContent.m_strDPHYSIC4;
                objDPArr2[19].Value = objContent.m_strDPHYSIC5;
                objDPArr2[20].Value = objContent.m_strDPHYSIC6;

                objDPArr2[21].Value = objContent.m_strDPHYSIC7;
                objDPArr2[22].Value = objContent.m_strDPHYSIC8;
                objDPArr2[23].Value = objContent.m_strDCURE1;
                objDPArr2[24].Value = objContent.m_strDCURE2;

                objDPArr2[25].Value = objContent.m_strDCURE3;
                objDPArr2[26].Value = objContent.m_strDCURE4;
                objDPArr2[27].Value = objContent.m_strDCURE5;
                objDPArr2[28].Value = objContent.m_strDCURE6;
                objDPArr2[29].Value = objContent.m_strDCURE7;
                objDPArr2[30].Value = objContent.m_strDCURE8;

                objDPArr2[31].Value = objContent.m_fltIGS;
                objDPArr2[32].Value = objContent.m_fltINS;
                objDPArr2[33].Value = objContent.m_fltINTATAL;
                objDPArr2[34].Value = objContent.m_fltOTATAL;

                objDPArr2[35].Value = objContent.m_fltOEMIEMCTION;
                objDPArr2[36].Value = objContent.m_fltOGASTRICJUICE;
                objDPArr2[37].Value = objContent.m_strSESPECIALLYNOTE;
                objDPArr2[38].Value = objContent.m_strBBLUSETIME;

                objDPArr2[39].Value = objContent.m_strBBLUSEMACHINETYPE;
                objDPArr2[40].Value = objContent.m_strBBLUSEMODE;
                objDPArr2[41].Value = objContent.m_strBVT;
                objDPArr2[42].Value = objContent.m_strBEXPIREDMV;
                objDPArr2[43].Value = objContent.m_strBBLUESPRESSURE;
                objDPArr2[44].Value = objContent.m_strBBLUSENUM;

                objDPArr2[45].Value = objContent.m_strBFIO2PEEP;
                objDPArr2[46].Value = objContent.m_strBMAXIP;
                objDPArr2[47].Value = objContent.m_strBBLUSESOUND;
                objDPArr2[48].Value = objContent.m_strBPHLEGMCOLOR;

                objDPArr2[49].Value = objContent.m_strBSQ2;
                objDPArr2[50].Value = objContent.m_strTCOLLECTBLOODPOINT;
                objDPArr2[51].Value = objContent.m_strTPH;
                objDPArr2[52].Value = objContent.m_strTPCO2;

                objDPArr2[53].Value = objContent.m_strTP02;
                objDPArr2[54].Value = objContent.m_strTHCO3;
                objDPArr2[55].Value = objContent.m_strTTCO2;
                objDPArr2[56].Value = objContent.m_strTBE;
                objDPArr2[57].Value = objContent.m_strTSAT;
                objDPArr2[58].Value = objContent.m_strTO2CT;

                objDPArr2[59].Value = objContent.m_strSCMH2O;
                objDPArr2[60].Value = objContent.m_strSSD;
                objDPArr2[61].Value = objContent.m_strSMEAN;
                objDPArr2[62].Value = objContent.m_strSWEDGE;
                objDPArr2[63].Value = objContent.m_strSCOCI;

                objDPArr2[64].Value = objContent.m_strPPUPLRIGHT;  //瞳孔(右)
                objDPArr2[65].Value = objContent.m_strPREFLECTRIGHT;//对光反射(右)'
                objDPArr2[66].Value = objContent.m_fltIBLOODPRODUCE;//血制品';
                objDPArr2[67].Value = objContent.m_fltIBLOODPRODUCEADD;// '血制品累计量';
                objDPArr2[68].Value = objContent.m_strINNAME1;//入量名称1';
                objDPArr2[69].Value = objContent.m_strINNAME2;//入量名称2';
                objDPArr2[70].Value = objContent.m_strINNAME3;//入量名称3';
                objDPArr2[71].Value = objContent.m_strINNAME4;//入量名称4';
                objDPArr2[72].Value = objContent.m_fltINAMOUNT1;//入量数量1';
                objDPArr2[73].Value = objContent.m_fltINAMOUNT2;//入量数量2';
                objDPArr2[74].Value = objContent.m_fltINAMOUNT3;//入量数量3';
                objDPArr2[75].Value = objContent.m_fltINAMOUNT4;//入量数量4';
                objDPArr2[76].Value = objContent.m_strOUTNAME1;//出量名称1';
                objDPArr2[77].Value = objContent.m_strOUTNAME2;//出量名称2';
                objDPArr2[78].Value = objContent.m_strOUTNAME3;//出量名称3';
                objDPArr2[79].Value = objContent.m_strOUTNAME4;//出量名称4';
                objDPArr2[80].Value = objContent.m_fltOUTAMOUNT1;//出量数量1';
                objDPArr2[81].Value = objContent.m_fltOUTAMOUNT2;//出量数量2';
                objDPArr2[82].Value = objContent.m_fltOUTAMOUNT3;//出量数量3';
                objDPArr2[83].Value = objContent.m_fltOUTAMOUNT4;//出量数量4';
                objDPArr2[84].Value = objContent.m_strBFI02PEEPRIGHT;//PEEP
                objDPArr2[85].Value = objContent.m_strBPHLEGMAMOUNT;//痰量
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
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
		/// 把记录从数据中“删除”。
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
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
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //获取IDataParameter数组
                //string strSQL = "Update GeneralNurseRecord Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where trim(InPatientID)=? and InPatientDate=? and OpenDate=? and Status=0";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[2];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;

                //执行SQL
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

            } return lngRes;

		}

		/// <summary>
		/// 获取数据库中最新的修改时间和首次打印时间
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_dtmModifyDate">修改时间</param>
		/// <param name="p_strFirstPrintDate">首次打印时间</param>
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
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //			string strSQL = @"select a.FirstPrintDate,b.ModifyDate from GeneralNurseRecord a,GeneralNurseRecordContent b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
                //						b.ModifyDate=(select Max(ModifyDate) from GeneralNurseRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //返回
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
		/// 获取指定已经被删除记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			clsHRPTableService p_objHRPServ,
			out clsTrackRecordContent p_objRecordContent)//out clsTrackRecordContent p_objRecordContent)
		{
				p_objRecordContent=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                //			string strSQL = @"select a.*,b.* from GeneralNurseRecord a,GeneralNurseRecordContent b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=1 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
                //						b.ModifyDate=(select Max(ModifyDate) from GeneralNurseRecordContent Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

                //IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)//==1)
                {
                    //设置结果

                    clsISURGERYICUWARDSHIP objRecordContent = new clsISURGERYICUWARDSHIP();
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

                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_strSTATUS = "0";
                    else objRecordContent.m_strSTATUS = dtbValue.Rows[0]["STATUS"].ToString();
                    objRecordContent.m_strPBODYPART = dtbValue.Rows[0]["PBODYPART"].ToString();
                    objRecordContent.m_strPBODYPARTXML = dtbValue.Rows[0]["PBODYPARTXML"].ToString();
                    objRecordContent.m_strPCONSCIOUSNESS = dtbValue.Rows[0]["PCONSCIOUSNESS"].ToString();
                    objRecordContent.m_strPCONSCIOUSNESSXML = dtbValue.Rows[0]["PCONSCIOUSNESSXML"].ToString();
                    objRecordContent.m_strPPUPIL = dtbValue.Rows[0]["PPUPIL"].ToString();
                    objRecordContent.m_strPPUPILXML = dtbValue.Rows[0]["PPUPILXML"].ToString();
                    objRecordContent.m_strPREFLECT = dtbValue.Rows[0]["PREFLECT"].ToString();
                    objRecordContent.m_strPREFLECTXML = dtbValue.Rows[0]["PREFLECTXML"].ToString();
                    objRecordContent.m_strCTEMPERATURE = dtbValue.Rows[0]["CTEMPERATURE"].ToString();
                    objRecordContent.m_strCTEMPERATUREXML = dtbValue.Rows[0]["CTEMPERATUREXML"].ToString();
                    objRecordContent.m_strCSMALLTEMPERATURE = dtbValue.Rows[0]["CSMALLTEMPERATURE"].ToString();
                    objRecordContent.m_strCSMALLTEMPERATUREXML = dtbValue.Rows[0]["CSMALLTEMPERATUREXML"].ToString();
                    objRecordContent.m_strCHEARTRATE = dtbValue.Rows[0]["CHEARTRATE"].ToString();
                    objRecordContent.m_strCHEARTRATEXML = dtbValue.Rows[0]["CHEARTRATEXML"].ToString();
                    objRecordContent.m_strCHEARTRHYTHM = dtbValue.Rows[0]["CHEARTRHYTHM"].ToString();
                    objRecordContent.m_strCHEARTRHYTHMXML = dtbValue.Rows[0]["CHEARTRHYTHMXML"].ToString();
                    objRecordContent.m_strCSD = dtbValue.Rows[0]["CSD"].ToString();
                    objRecordContent.m_strCSDXML = dtbValue.Rows[0]["CSDXML"].ToString();
                    objRecordContent.m_strCCVP = dtbValue.Rows[0]["CCVP"].ToString();
                    objRecordContent.m_strCCVPXML = dtbValue.Rows[0]["CCVPXML"].ToString();

                    objRecordContent.m_strDPHYSIC1 = dtbValue.Rows[0]["DPHYSIC1"].ToString();
                    objRecordContent.m_strDPHYSIC1XML = dtbValue.Rows[0]["DPHYSIC1XML"].ToString();
                    objRecordContent.m_strDPHYSIC2 = dtbValue.Rows[0]["DPHYSIC2"].ToString();
                    objRecordContent.m_strDPHYSIC2XML = dtbValue.Rows[0]["DPHYSIC2XML"].ToString();
                    objRecordContent.m_strDPHYSIC3 = dtbValue.Rows[0]["DPHYSIC3"].ToString();
                    objRecordContent.m_strDPHYSIC3XML = dtbValue.Rows[0]["DPHYSIC3XML"].ToString();
                    objRecordContent.m_strDPHYSIC4 = dtbValue.Rows[0]["DPHYSIC4"].ToString();
                    objRecordContent.m_strDPHYSIC4XML = dtbValue.Rows[0]["DPHYSIC4XML"].ToString();
                    objRecordContent.m_strDPHYSIC5 = dtbValue.Rows[0]["DPHYSIC5"].ToString();
                    objRecordContent.m_strDPHYSIC5XML = dtbValue.Rows[0]["DPHYSIC5XML"].ToString();
                    objRecordContent.m_strDPHYSIC6 = dtbValue.Rows[0]["DPHYSIC6"].ToString();
                    objRecordContent.m_strDPHYSIC6XML = dtbValue.Rows[0]["DPHYSIC6XML"].ToString();
                    objRecordContent.m_strDPHYSIC7 = dtbValue.Rows[0]["DPHYSIC7"].ToString();
                    objRecordContent.m_strDPHYSIC7XML = dtbValue.Rows[0]["DPHYSIC7XML"].ToString();
                    objRecordContent.m_strDPHYSIC8 = dtbValue.Rows[0]["DPHYSIC8"].ToString();
                    objRecordContent.m_strDPHYSIC8XML = dtbValue.Rows[0]["DPHYSIC8XML"].ToString();

                    objRecordContent.m_strDCURE1 = dtbValue.Rows[0]["DCURE1"].ToString();
                    objRecordContent.m_strDCURE1XML = dtbValue.Rows[0]["DCURE1XML"].ToString();
                    objRecordContent.m_strDCURE2 = dtbValue.Rows[0]["DCURE2"].ToString();
                    objRecordContent.m_strDCURE2XML = dtbValue.Rows[0]["DCURE2XML"].ToString();
                    objRecordContent.m_strDCURE3 = dtbValue.Rows[0]["DCURE3"].ToString();
                    objRecordContent.m_strDCURE3XML = dtbValue.Rows[0]["DCURE3XML"].ToString();
                    objRecordContent.m_strDCURE4 = dtbValue.Rows[0]["DCURE4"].ToString();
                    objRecordContent.m_strDCURE4XML = dtbValue.Rows[0]["DCURE4XML"].ToString();
                    objRecordContent.m_strDCURE5 = dtbValue.Rows[0]["DCURE5"].ToString();
                    objRecordContent.m_strDCURE5XML = dtbValue.Rows[0]["DCURE5XML"].ToString();
                    objRecordContent.m_strDCURE6 = dtbValue.Rows[0]["DCURE6"].ToString();
                    objRecordContent.m_strDCURE6XML = dtbValue.Rows[0]["DCURE6XML"].ToString();
                    objRecordContent.m_strDCURE7 = dtbValue.Rows[0]["DCURE7"].ToString();
                    objRecordContent.m_strDCURE7XML = dtbValue.Rows[0]["DCURE7XML"].ToString();
                    objRecordContent.m_strDCURE8 = dtbValue.Rows[0]["DCURE8"].ToString();
                    objRecordContent.m_strDCURE8XML = dtbValue.Rows[0]["DCURE8XML"].ToString();

                    objRecordContent.m_fltIGS = float.Parse(dtbValue.Rows[0]["IGS"].ToString());
                    objRecordContent.m_strIGSXML = dtbValue.Rows[0]["IGSXML"].ToString();
                    objRecordContent.m_fltINS = float.Parse(dtbValue.Rows[0]["INS"].ToString());
                    objRecordContent.m_strINSXML = dtbValue.Rows[0]["INSXML"].ToString();
                    objRecordContent.m_fltINTATAL = float.Parse(dtbValue.Rows[0]["INTATAL"].ToString());
                    objRecordContent.m_strINTATALXML = dtbValue.Rows[0]["INTATALXML"].ToString();
                    objRecordContent.m_fltOTATAL = float.Parse(dtbValue.Rows[0]["OTATAL"].ToString());
                    objRecordContent.m_strOTATALXML = dtbValue.Rows[0]["OTATALXML"].ToString();
                    objRecordContent.m_fltOEMIEMCTION = float.Parse(dtbValue.Rows[0]["OEMIEMCTION"].ToString());
                    objRecordContent.m_strOEMEMCTIONXML = dtbValue.Rows[0]["OEMEMCTIONXML"].ToString();
                    objRecordContent.m_fltOGASTRICJUICE = float.Parse(dtbValue.Rows[0]["OGASTRICJUICE"].ToString());
                    objRecordContent.m_strOGASTRICJUICEXML = dtbValue.Rows[0]["OGASTRICJUICEXML"].ToString();
                    objRecordContent.m_strSESPECIALLYNOTE = dtbValue.Rows[0]["SESPECIALLYNOTE"].ToString();
                    objRecordContent.m_strSESPECIALLYNOTEXML = dtbValue.Rows[0]["SESPECIALLYNOTEXML"].ToString();
                    objRecordContent.m_strBBLUSETIME = dtbValue.Rows[0]["BBLUSETIME"].ToString();
                    objRecordContent.m_strBBLUSETIMEXML = dtbValue.Rows[0]["BBLUSETIMEXML"].ToString();

                    objRecordContent.m_strBBLUSEMACHINETYPE = dtbValue.Rows[0]["BBLUSEMACHINETYPE"].ToString();
                    objRecordContent.m_strBBLUSEMACHINETYPEXML = dtbValue.Rows[0]["BBLUSEMACHINETYPEXML"].ToString();
                    objRecordContent.m_strBBLUSEMODE = dtbValue.Rows[0]["BBLUSEMODE"].ToString();
                    objRecordContent.m_strBBLUSEMONDEXML = dtbValue.Rows[0]["BBLUSEMONDEXML"].ToString();
                    objRecordContent.m_strBVT = dtbValue.Rows[0]["BVT"].ToString();
                    objRecordContent.m_strBVTXML = dtbValue.Rows[0]["BVTXML"].ToString();
                    objRecordContent.m_strBEXPIREDMV = dtbValue.Rows[0]["BEXPIREDMV"].ToString();
                    objRecordContent.m_strBEXPIREDMVXML = dtbValue.Rows[0]["BEXPIREDMVXML"].ToString();
                    objRecordContent.m_strBBLUESPRESSURE = dtbValue.Rows[0]["BBLUESPRESSURE"].ToString();
                    objRecordContent.m_strBBLUESPRESSUREXML = dtbValue.Rows[0]["BBLUESPRESSUREXML"].ToString();
                    objRecordContent.m_strBBLUSENUM = dtbValue.Rows[0]["BBLUSENUM"].ToString();
                    objRecordContent.m_strBBLUSENUMXML = dtbValue.Rows[0]["BBLUSENUMXML"].ToString();
                    objRecordContent.m_strBFIO2PEEP = dtbValue.Rows[0]["BFIO2PEEP"].ToString();
                    objRecordContent.m_strBFIO2PEEPXML = dtbValue.Rows[0]["BFIO2PEEPXML"].ToString();
                    objRecordContent.m_strBMAXIP = dtbValue.Rows[0]["BMAXIP"].ToString();
                    objRecordContent.m_strBMAXIPXML = dtbValue.Rows[0]["BMAXIPXML"].ToString();

                    objRecordContent.m_strBBLUSESOUND = dtbValue.Rows[0]["BBLUSESOUND"].ToString();
                    objRecordContent.m_strBBLUSESOUNDXML = dtbValue.Rows[0]["BBLUSESOUNDXML"].ToString();
                    objRecordContent.m_strBPHLEGMCOLOR = dtbValue.Rows[0]["BPHLEGMCOLOR"].ToString();
                    objRecordContent.m_strBPHLEGMCOLORXML = dtbValue.Rows[0]["BPHLEGMCOLORXML"].ToString();
                    objRecordContent.m_strBSQ2 = dtbValue.Rows[0]["BSQ2"].ToString();
                    objRecordContent.m_strBSQ2XML = dtbValue.Rows[0]["BSQ2XML"].ToString();
                    objRecordContent.m_strTCOLLECTBLOODPOINT = dtbValue.Rows[0]["TCOLLECTBLOODPOINT"].ToString();
                    objRecordContent.m_strTCOLLECTBLOODPOINTXML = dtbValue.Rows[0]["TCOLLECTBLOODPOINTXML"].ToString();
                    objRecordContent.m_strTPH = dtbValue.Rows[0]["TPH"].ToString();
                    objRecordContent.m_strTPHXML = dtbValue.Rows[0]["TPHXML"].ToString();
                    objRecordContent.m_strTPCO2 = dtbValue.Rows[0]["TPCO2"].ToString();
                    objRecordContent.m_strTPCO2XML = dtbValue.Rows[0]["TPCO2XML"].ToString();
                    objRecordContent.m_strTP02 = dtbValue.Rows[0]["TP02"].ToString();
                    objRecordContent.m_strTPO2XML = dtbValue.Rows[0]["TPO2XML"].ToString();
                    objRecordContent.m_strTHCO3 = dtbValue.Rows[0]["THCO3"].ToString();
                    objRecordContent.m_strTHCO3XML = dtbValue.Rows[0]["THCO3XML"].ToString();

                    objRecordContent.m_strTTCO2 = dtbValue.Rows[0]["TTCO2"].ToString();
                    objRecordContent.m_strTTCO2XML = dtbValue.Rows[0]["TTCO2XML"].ToString();
                    objRecordContent.m_strTBE = dtbValue.Rows[0]["TBE"].ToString();
                    objRecordContent.m_strTBEXML = dtbValue.Rows[0]["TBEXML"].ToString();
                    objRecordContent.m_strTSAT = dtbValue.Rows[0]["TSAT"].ToString();
                    objRecordContent.m_strTSATXML = dtbValue.Rows[0]["TSATXML"].ToString();
                    objRecordContent.m_strTO2CT = dtbValue.Rows[0]["TO2CT"].ToString();
                    objRecordContent.m_strTO2CTXML = dtbValue.Rows[0]["TO2CTXML"].ToString();
                    objRecordContent.m_strSCMH2O = dtbValue.Rows[0]["SCMH2O"].ToString();
                    objRecordContent.m_strSCMH2OXML = dtbValue.Rows[0]["SCMH2OXML"].ToString();
                    objRecordContent.m_strSSD = dtbValue.Rows[0]["SSD"].ToString();
                    objRecordContent.m_strSSDXML = dtbValue.Rows[0]["SSDXML"].ToString();
                    objRecordContent.m_strSMEAN = dtbValue.Rows[0]["SMEAN"].ToString();
                    objRecordContent.m_strSMEANXML = dtbValue.Rows[0]["SMEANXML"].ToString();
                    objRecordContent.m_strSWEDGE = dtbValue.Rows[0]["SWEDGE"].ToString();
                    objRecordContent.m_strSWEDGEXML = dtbValue.Rows[0]["SWEDGEXML"].ToString();
                    objRecordContent.m_strSCOCI = dtbValue.Rows[0]["SCOCI"].ToString();
                    objRecordContent.m_strSCOCIXML = dtbValue.Rows[0]["SCOCIXML"].ToString();

                    #region//add field 20051117
                    objRecordContent.m_strPPUPLRIGHT = dtbValue.Rows[0]["PPUPLRIGHT"].ToString();  //瞳孔(右)
                    objRecordContent.m_strPREFLECTRIGHT = dtbValue.Rows[0]["PREFLECTRIGHT"].ToString();//对光反射(右)'
                    objRecordContent.m_fltIBLOODPRODUCE = float.Parse(dtbValue.Rows[0]["IBLOODPRODUCE"].ToString());//血制品';
                    objRecordContent.m_fltIBLOODPRODUCEADD = float.Parse(dtbValue.Rows[0]["IBLOODPRODUCEADD"].ToString());// '血制品累计量';
                    objRecordContent.m_strINNAME1 = dtbValue.Rows[0]["INNAME1"].ToString();//入量名称1';
                    objRecordContent.m_strINNAME2 = dtbValue.Rows[0]["INNAME2"].ToString();//入量名称2';
                    objRecordContent.m_strINNAME3 = dtbValue.Rows[0]["INNAME3"].ToString();//入量名称3';
                    objRecordContent.m_strINNAME4 = dtbValue.Rows[0]["INNAME4"].ToString();//入量名称4';
                    objRecordContent.m_fltINAMOUNT1 = float.Parse(dtbValue.Rows[0]["INAMOUNT1"].ToString());//入量数量1';
                    objRecordContent.m_fltINAMOUNT2 = float.Parse(dtbValue.Rows[0]["INAMOUNT2"].ToString());//入量数量2';
                    objRecordContent.m_fltINAMOUNT3 = float.Parse(dtbValue.Rows[0]["INAMOUNT3"].ToString());//入量数量3';
                    objRecordContent.m_fltINAMOUNT4 = float.Parse(dtbValue.Rows[0]["INAMOUNT4"].ToString());//入量数量4';
                    objRecordContent.m_strOUTNAME1 = dtbValue.Rows[0]["OUTNAME1"].ToString();//出量名称1';
                    objRecordContent.m_strOUTNAME2 = dtbValue.Rows[0]["OUTNAME2"].ToString();//出量名称2';
                    objRecordContent.m_strOUTNAME3 = dtbValue.Rows[0]["OUTNAME3"].ToString();//出量名称3';
                    objRecordContent.m_strOUTNAME4 = dtbValue.Rows[0]["OUTNAME4"].ToString();//出量名称4';
                    objRecordContent.m_fltOUTAMOUNT1 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT1"].ToString());//出量数量1';
                    objRecordContent.m_fltOUTAMOUNT2 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT2"].ToString());//出量数量2';
                    objRecordContent.m_fltOUTAMOUNT3 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT3"].ToString());//出量数量3';
                    objRecordContent.m_fltOUTAMOUNT4 = float.Parse(dtbValue.Rows[0]["OUTAMOUNT4"].ToString());//出量数量4';
                    objRecordContent.m_strBFI02PEEPRIGHT = dtbValue.Rows[0]["BFI02PEEPRIGHT"].ToString();//PEEP
                    objRecordContent.m_strBPHLEGMAMOUNT = dtbValue.Rows[0]["BPHLEGMAMOUNT"].ToString();//痰量

                    objRecordContent.m_strPPUPLRIGHTXML = dtbValue.Rows[0]["PPUPLRIGHTXML"].ToString();  //瞳孔(右)
                    objRecordContent.m_strPREFLECTRIGHTXML = dtbValue.Rows[0]["PREFLECTRIGHTXML"].ToString();//对光反射(右)'
                    objRecordContent.m_strIBLOODPRODUCEXML = dtbValue.Rows[0]["IBLOODPRODUCEXML"].ToString();//血制品';
                    objRecordContent.m_strIBLOODPRODUCEADDXML = dtbValue.Rows[0]["IBLOODPRODUCEADDXML"].ToString();// '血制品累计量';
                    objRecordContent.m_strINNAME1XML = dtbValue.Rows[0]["INNAME1XML"].ToString();//入量名称1';
                    objRecordContent.m_strINNAME2XML = dtbValue.Rows[0]["INNAME2XML"].ToString();//入量名称2';
                    objRecordContent.m_strINNAME3XML = dtbValue.Rows[0]["INNAME3XML"].ToString();//入量名称3';
                    objRecordContent.m_strINNAME4XML = dtbValue.Rows[0]["INNAME4XML"].ToString();//入量名称4';
                    objRecordContent.m_strINAMOUNT1XML = dtbValue.Rows[0]["INAMOUNT1XML"].ToString();//入量数量1';
                    objRecordContent.m_strINAMOUNT2XML = dtbValue.Rows[0]["INAMOUNT2XML"].ToString();//入量数量2';
                    objRecordContent.m_strINAMOUNT3XML = dtbValue.Rows[0]["INAMOUNT3XML"].ToString();//入量数量3';
                    objRecordContent.m_strINAMOUNT4XML = dtbValue.Rows[0]["INAMOUNT4XML"].ToString();//入量数量4';
                    objRecordContent.m_strOUTNAME1XML = dtbValue.Rows[0]["OUTNAME1XML"].ToString();//出量名称1';
                    objRecordContent.m_strOUTNAME2XML = dtbValue.Rows[0]["OUTNAME2XML"].ToString();//出量名称2';
                    objRecordContent.m_strOUTNAME3XML = dtbValue.Rows[0]["OUTNAME3XML"].ToString();//出量名称3';
                    objRecordContent.m_strOUTNAME4XML = dtbValue.Rows[0]["OUTNAME4XML"].ToString();//出量名称4';
                    objRecordContent.m_strOUTAMOUNT1XML = dtbValue.Rows[0]["OUTAMOUNT1XML"].ToString();//出量数量1';
                    objRecordContent.m_strOUTAMOUNT2XML = dtbValue.Rows[0]["OUTAMOUNT2XML"].ToString();//出量数量2';
                    objRecordContent.m_strOUTAMOUNT3XML = dtbValue.Rows[0]["OUTAMOUNT3XML"].ToString();//出量数量3';
                    objRecordContent.m_strOUTAMOUNT4XML = dtbValue.Rows[0]["OUTAMOUNT4XML"].ToString();//出量数量4';
                    objRecordContent.m_strBFI02PEEPRIGHTXML = dtbValue.Rows[0]["BFI02PEEPRIGHTXML"].ToString();//PEEP
                    objRecordContent.m_strBPHLEGMAMOUNTXML = dtbValue.Rows[0]["BPHLEGMAMOUNTXML"].ToString();//痰量

                    #endregion

                    objRecordContent.m_strWEIGHT = dtbValue.Rows[0]["WEIGHT"].ToString();
                    objRecordContent.m_strIDCODE = dtbValue.Rows[0]["IDCODE"].ToString();
                    objRecordContent.m_strOPERATIONNAME = dtbValue.Rows[0]["OPERATIONNAME"].ToString();
                    if (dtbValue.Rows[0]["OPERATIONDATE"].ToString().Trim().Length > 0)
                        objRecordContent.m_strOPERATIONDATE = DateTime.Parse(dtbValue.Rows[0]["OPERATIONDATE"].ToString().Trim());
                    else
                        objRecordContent.m_strOPERATIONDATE = DateTime.Parse("1900-01-01");

                    objRecordContent.m_strDATEAFTEROPERATION = dtbValue.Rows[0]["DATEAFTEROPERATION"].ToString();

                    p_objRecordContent = objRecordContent;
                }
                //返回
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
