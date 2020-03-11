using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.Utility;
namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// ICU护理记录(广西)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsSURGERYICUWARDSHIPMainService : clsRecordsService
	{
		public clsSURGERYICUWARDSHIPMainService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region SQL语句
		private const string c_strUpdateFirstPrintDateSQL=@"update t_emr_surgeryicuwardship
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t1.createuserid) as createusername,
       t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.weight,
       t1.idcode,
       t1.operationname,
       t1.operationdate,
       t1.dateafteroperation,
       t1.pbodypart,
       t1.pconsciousness,
       t1.ppupil,
       t1.preflect,
       t1.ctemperature,
       t1.csmalltemperature,
       t1.cheartrate,
       t1.cheartrhythm,
       t1.csd,
       t1.ccvp,
       t1.dphysic1,
       t1.dphysic2,
       t1.dphysic3,
       t1.dphysic4,
       t1.dphysic5,
       t1.dphysic6,
       t1.dphysic7,
       t1.dphysic8,
       t1.dcure1,
       t1.dcure2,
       t1.dcure3,
       t1.dcure4,
       t1.dcure5,
       t1.dcure6,
       t1.dcure7,
       t1.dcure8,
       t1.igs,
       t1.ins,
       t1.intatal,
       t1.otatal,
       t1.oemiemction,
       t1.ogastricjuice,
       t1.sespeciallynote,
       t1.bblusetime,
       t1.bblusemachinetype,
       t1.bblusemode,
       t1.bvt,
       t1.bexpiredmv,
       t1.bbluespressure,
       t1.bblusenum,
       t1.bfio2peep,
       t1.bmaxip,
       t1.bblusesound,
       t1.bphlegmcolor,
       t1.bsq2,
       t1.tcollectbloodpoint,
       t1.tph,
       t1.tpco2,
       t1.tp02,
       t1.thco3,
       t1.ttco2,
       t1.tbe,
       t1.tsat,
       t1.to2ct,
       t1.scmh2o,
       t1.ssd,
       t1.smean,
       t1.swedge,
       t1.scoci,
       t1.pbodypartxml,
       t1.pconsciousnessxml,
       t1.ppupilxml,
       t1.preflectxml,
       t1.ctemperaturexml,
       t1.csmalltemperaturexml,
       t1.cheartratexml,
       t1.cheartrhythmxml,
       t1.csdxml,
       t1.ccvpxml,
       t1.igsxml,
       t1.insxml,
       t1.intatalxml,
       t1.otatalxml,
       t1.oememctionxml,
       t1.ogastricjuicexml,
       t1.sespeciallynotexml,
       t1.bblusetimexml,
       t1.bblusemachinetypexml,
       t1.bblusemondexml,
       t1.bvtxml,
       t1.bexpiredmvxml,
       t1.bbluespressurexml,
       t1.bblusenumxml,
       t1.bfio2peepxml,
       t1.bmaxipxml,
       t1.bblusesoundxml,
       t1.bphlegmcolorxml,
       t1.bsq2xml,
       t1.tcollectbloodpointxml,
       t1.tphxml,
       t1.tpco2xml,
       t1.tpo2xml,
       t1.thco3xml,
       t1.ttco2xml,
       t1.tbexml,
       t1.tsatxml,
       t1.to2ctxml,
       t1.scmh2oxml,
       t1.ssdxml,
       t1.smeanxml,
       t1.swedgexml,
       t1.scocixml,
       t1.firstprintdate,
       t1.recorddate,
       t1.status,
       t1.dphysic1xml,
       t1.dphysic2xml,
       t1.dphysic3xml,
       t1.dphysic4xml,
       t1.dphysic5xml,
       t1.dphysic6xml,
       t1.dphysic7xml,
       t1.dphysic8xml,
       t1.dcure1xml,
       t1.dcure2xml,
       t1.dcure3xml,
       t1.dcure4xml,
       t1.dcure5xml,
       t1.dcure6xml,
       t1.dcure7xml,
       t1.dcure8xml,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.ppuplright,
       t1.preflectright,
       t1.ibloodproduce,
       t1.ibloodproduceadd,
       t1.inname1,
       t1.inname2,
       t1.inname3,
       t1.inname4,
       t1.inamount1,
       t1.inamount2,
       t1.inamount3,
       t1.inamount4,
       t1.outname1,
       t1.outname2,
       t1.outname3,
       t1.outname4,
       t1.outamount1,
       t1.outamount2,
       t1.outamount3,
       t1.outamount4,
       t1.bfi02peepright,
       t1.bphlegmamount,
       t1.ppuplrightxml,
       t1.preflectrightxml,
       t1.ibloodproducexml,
       t1.ibloodproduceaddxml,
       t1.inname1xml,
       t1.inname2xml,
       t1.inname3xml,
       t1.inname4xml,
       t1.inamount1xml,
       t1.inamount2xml,
       t1.inamount3xml,
       t1.inamount4xml,
       t1.outname1xml,
       t1.outname2xml,
       t1.outname3xml,
       t1.outname4xml,
       t1.outamount1xml,
       t1.outamount2xml,
       t1.outamount3xml,
       t1.outamount4xml,
       t1.bfi02peeprightxml,
       t1.bphlegmamountxml,
       b.modifydate,
       b.modifyuserid,
       b.modifydate,
       b.modifyuserid,
       b.pbodypart as pbodypart_right,
       b.pconsciousness as pconsciousness_right,
       b.ppupil as ppupil_right,
       b.preflect as preflect_right,
       b.ctemperature as ctemperature_right,
       b.csmalltemperature as csmalltemperature_right,
       b.cheartrate as cheartrate_right,
       b.cheartrhythm as cheartrhythm_right,
       b.csd as csd_right,
       b.ccvp as ccvp_right,
       b.dphysic1 as dphysic1_right,
       b.dphysic2 as dphysic2_right,
       b.dphysic3 as dphysic3_right,
       b.dphysic4 as dphysic4_right,
       b.dphysic5 as dphysic5_right,
       b.dphysic6 as dphysic6_right,
       b.dphysic7 as dphysic7_right,
       b.dphysic8 as dphysic8_right,
       b.dcure1 as dcure1_right,
       b.dcure2 as dcure2_right,
       b.dcure3 as dcure3_right,
       b.dcure4 as dcure4_right,
       b.dcure5 as dcure5_right,
       b.dcure6 as dcure6_right,
       b.dcure7 as dcure7_right,
       b.dcure8 as dcure8_right,
       b.igs as igs_right,
       b.ins as ins_right,
       b.intatal as intatal_right,
       b.otatal as otatal_right,
       b.oemiemction as oemiemction_right,
       b.ogastricjuice as ogastricjuice_right,
       b.sespeciallynote as sespeciallynote_right,
       b.bblusetime as bblusetime_right,
       b.bblusemachinetype as bblusemachinetype_right,
       b.bblusemode as bblusemode_right,
       b.bvt as bvt_right,
       b.bexpiredmv as bexpiredmv_right,
       b.bbluespressure as bbluespressure_right,
       b.bblusenum as bblusenum_right,
       b.bfio2peep as bfio2peep_right,
       b.bmaxip as bmaxip_right,
       b.bblusesound as bblusesound_right,
       b.bphlegmcolor as bphlegmcolor_right,
       b.bsq2 as bsq2_right,
       b.tcollectbloodpoint as tcollectbloodpoint_right,
       b.tph as tph_right,
       b.tpco2 as tpco2_right,
       b.tp02 as tp02_right,
       b.thco3 as thco3_right,
       b.ttco2 as ttco2_right,
       b.tbe as tbe_right,
       b.tsat as tsat_right,
       b.to2ct as to2ct_right,
       b.scmh2o as scmh2o_right,
       b.ssd as ssd_right,
       b.smean as smean_right,
       b.swedge as swedge_right,
       b.scoci as scoci_right,
       b.ppuplright as ppuplrigh_right,
       b.preflectright as preflectright_right,
       b.ibloodproduce as ibloodproduce_right,
       b.ibloodproduceadd as ibloodproduceadd_right,
       b.inname1 as inname1_right,
       b.inname2 as inname2_right,
       b.inname3 as inname3_right,
       b.inname4 as inname4_right,
       b.inamount1 as inamount1_right,
       b.inamount2 as inamount2_right,
       b.inamount3 as inamount3_right,
       b.inamount4 as inamount4_right,
       b.outname1 as outname1_right,
       b.outname2 as outname2_right,
       b.outname3 as outname3_right,
       b.outname4 as outname4_right,
       b.outamount1 as outamount1_right,
       b.outamount2 as outamount2_right,
       b.outamount3 as outamount3_right,
       b.outamount4 as outamount4_right,
       b.bfi02peepright as bfi02peepright_right,
       b.bphlegmamount as bphlegmamount_right
  from t_emr_surgeryicuwardship t1, t_emr_surgeryicuwardshipnote b
 where t1.inpatientid = b.inpatientid
   and t1.inpatientdate = b.inpatientdate
   and t1.opendate = b.opendate
   and t1.status = '0'
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by b.modifydate";

														//ORDER BY T1.RECORDDATE";


        private const string c_strGetRecordContentSQL_Single = @"select f_getempnamebyno(t1.createuserid) as createusername,
															t1.*,b.modifydate,b.modifyuserid
														from t_emr_surgeryicuwardship t1,t_emr_surgeryicuwardshipnote b
														where t1.inpatientid = b.inpatientid
														and t1.inpatientdate = b.inpatientdate
														and t1.opendate = b.opendate
														and	t1.status = '0'
														and t1.inpatientid = ?
														and t1.inpatientdate = ?
														order by b.modifydate";
														//ORDER BY T1.CREATEDATE";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from t_emr_surgeryicuwardship
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";//OpenDate

		private const string c_strDeleteRecordSQL=@"update t_emr_surgeryicuwardship
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";
		#endregion

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
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendMainService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	
			

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||
				p_dtmOpenDateArr==null||p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
		    //获取IDataParameter数组
			IDataParameter[] objDPArr = null;
			for(int i=0; i<p_dtmOpenDateArr.Length; i++)
			{
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=p_dtmOpenDateArr[i];
				//执行SQL
				long lngEff=0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);				
				if(lngRes <= 0)	return lngRes;
			} 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;

			return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 修改或添加一条记录时读数据库
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordOpenDate,
			out clsISURGERYICUWARDSHIP[] p_objTansDataInfo)
		{
			
			p_objTansDataInfo=null;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                ArrayList arlTransData = new ArrayList();
                ArrayList arlModifyData = new ArrayList();
                DateTime dtmOpenDate;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strRecordOpenDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objTansDataInfo = new clsISURGERYICUWARDSHIP[dtbValue.Rows.Count];
                    clsISURGERYICUWARDSHIP objRecordContent = null;

                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date == dtmOpenDate)
                        {
                            #region 从DataTable.Rows中获取结果

                            objRecordContent = new clsISURGERYICUWARDSHIP();
                            objRecordContent.m_strInPatientID = p_strInPatientID;
                            objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString());
                            objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                            objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                            if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                                objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                            objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                            objRecordContent.m_strCreateUserName = dtbValue.Rows[j]["CreateUserName"].ToString();
                            objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                            objRecordContent.m_strModifyUserName = dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();

                            if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                                objRecordContent.m_bytStatus = 0;
                            else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());


                            objRecordContent.m_strPBODYPART = dtbValue.Rows[j]["PBODYPART"].ToString();
                            objRecordContent.m_strPBODYPARTXML = dtbValue.Rows[j]["PBODYPARTXML"].ToString();
                            objRecordContent.m_strPCONSCIOUSNESS = dtbValue.Rows[j]["PCONSCIOUSNESS"].ToString();
                            objRecordContent.m_strPCONSCIOUSNESSXML = dtbValue.Rows[j]["PCONSCIOUSNESSXML"].ToString();
                            objRecordContent.m_strPPUPIL = dtbValue.Rows[j]["PPUPIL"].ToString();
                            objRecordContent.m_strPPUPILXML = dtbValue.Rows[j]["PPUPILXML"].ToString();
                            objRecordContent.m_strPREFLECT = dtbValue.Rows[j]["PREFLECT"].ToString();
                            objRecordContent.m_strPREFLECTXML = dtbValue.Rows[j]["PREFLECTXML"].ToString();
                            objRecordContent.m_strCTEMPERATURE = dtbValue.Rows[j]["CTEMPERATURE"].ToString();
                            objRecordContent.m_strCTEMPERATUREXML = dtbValue.Rows[j]["CTEMPERATUREXML"].ToString();
                            objRecordContent.m_strCSMALLTEMPERATURE = dtbValue.Rows[j]["CSMALLTEMPERATURE"].ToString();
                            objRecordContent.m_strCSMALLTEMPERATUREXML = dtbValue.Rows[j]["CSMALLTEMPERATUREXML"].ToString();
                            objRecordContent.m_strCHEARTRATE = dtbValue.Rows[j]["CHEARTRATE"].ToString();
                            objRecordContent.m_strCHEARTRATEXML = dtbValue.Rows[j]["CHEARTRATEXML"].ToString();
                            objRecordContent.m_strCHEARTRHYTHM = dtbValue.Rows[j]["CHEARTRHYTHM"].ToString();
                            objRecordContent.m_strCHEARTRHYTHMXML = dtbValue.Rows[j]["CHEARTRHYTHMXML"].ToString();
                            objRecordContent.m_strCSD = dtbValue.Rows[j]["CSD"].ToString();
                            objRecordContent.m_strCSDXML = dtbValue.Rows[j]["CSDXML"].ToString();
                            objRecordContent.m_strCCVP = dtbValue.Rows[j]["CCVP"].ToString();
                            objRecordContent.m_strCCVPXML = dtbValue.Rows[j]["CCVPXML"].ToString();

                            objRecordContent.m_strDPHYSIC1 = dtbValue.Rows[j]["DPHYSIC1"].ToString();
                            objRecordContent.m_strDPHYSIC1XML = dtbValue.Rows[j]["DPHYSIC1XML"].ToString();
                            objRecordContent.m_strDPHYSIC2 = dtbValue.Rows[j]["DPHYSIC2"].ToString();
                            objRecordContent.m_strDPHYSIC2XML = dtbValue.Rows[j]["DPHYSIC2XML"].ToString();
                            objRecordContent.m_strDPHYSIC3 = dtbValue.Rows[j]["DPHYSIC3"].ToString();
                            objRecordContent.m_strDPHYSIC3XML = dtbValue.Rows[j]["DPHYSIC3XML"].ToString();
                            objRecordContent.m_strDPHYSIC4 = dtbValue.Rows[j]["DPHYSIC4"].ToString();
                            objRecordContent.m_strDPHYSIC4XML = dtbValue.Rows[j]["DPHYSIC4XML"].ToString();
                            objRecordContent.m_strDPHYSIC5 = dtbValue.Rows[j]["DPHYSIC5"].ToString();
                            objRecordContent.m_strDPHYSIC5XML = dtbValue.Rows[j]["DPHYSIC5XML"].ToString();
                            objRecordContent.m_strDPHYSIC6 = dtbValue.Rows[j]["DPHYSIC6"].ToString();
                            objRecordContent.m_strDPHYSIC6XML = dtbValue.Rows[j]["DPHYSIC6XML"].ToString();
                            objRecordContent.m_strDPHYSIC7 = dtbValue.Rows[j]["DPHYSIC7"].ToString();
                            objRecordContent.m_strDPHYSIC7XML = dtbValue.Rows[j]["DPHYSIC7XML"].ToString();
                            objRecordContent.m_strDPHYSIC8 = dtbValue.Rows[j]["DPHYSIC8"].ToString();
                            objRecordContent.m_strDPHYSIC8XML = dtbValue.Rows[j]["DPHYSIC8XML"].ToString();

                            objRecordContent.m_strDCURE1 = dtbValue.Rows[j]["DCURE1"].ToString();
                            objRecordContent.m_strDCURE1XML = dtbValue.Rows[j]["DCURE1XML"].ToString();
                            objRecordContent.m_strDCURE2 = dtbValue.Rows[j]["DCURE2"].ToString();
                            objRecordContent.m_strDCURE2XML = dtbValue.Rows[j]["DCURE2XML"].ToString();
                            objRecordContent.m_strDCURE3 = dtbValue.Rows[j]["DCURE3"].ToString();
                            objRecordContent.m_strDCURE3XML = dtbValue.Rows[j]["DCURE3XML"].ToString();
                            objRecordContent.m_strDCURE4 = dtbValue.Rows[j]["DCURE4"].ToString();
                            objRecordContent.m_strDCURE4XML = dtbValue.Rows[j]["DCURE4XML"].ToString();
                            objRecordContent.m_strDCURE5 = dtbValue.Rows[j]["DCURE5"].ToString();
                            objRecordContent.m_strDCURE5XML = dtbValue.Rows[j]["DCURE5XML"].ToString();
                            objRecordContent.m_strDCURE6 = dtbValue.Rows[j]["DCURE6"].ToString();
                            objRecordContent.m_strDCURE6XML = dtbValue.Rows[j]["DCURE6XML"].ToString();
                            objRecordContent.m_strDCURE7 = dtbValue.Rows[j]["DCURE7"].ToString();
                            objRecordContent.m_strDCURE7XML = dtbValue.Rows[j]["DCURE7XML"].ToString();
                            objRecordContent.m_strDCURE8 = dtbValue.Rows[j]["DCURE8"].ToString();
                            objRecordContent.m_strDCURE8XML = dtbValue.Rows[j]["DCURE8XML"].ToString();

                            objRecordContent.m_fltIGS = float.Parse(dtbValue.Rows[j]["IGS"].ToString());
                            objRecordContent.m_strIGSXML = dtbValue.Rows[j]["IGSXML"].ToString();
                            objRecordContent.m_fltINS = float.Parse(dtbValue.Rows[j]["INS"].ToString());
                            objRecordContent.m_strINSXML = dtbValue.Rows[j]["INSXML"].ToString();
                            objRecordContent.m_fltINTATAL = float.Parse(dtbValue.Rows[j]["INTATAL"].ToString());
                            objRecordContent.m_strINTATALXML = dtbValue.Rows[j]["INTATALXML"].ToString();
                            objRecordContent.m_fltOTATAL = float.Parse(dtbValue.Rows[j]["OTATAL"].ToString());
                            objRecordContent.m_strOTATALXML = dtbValue.Rows[j]["OTATALXML"].ToString();
                            objRecordContent.m_fltOEMIEMCTION = float.Parse(dtbValue.Rows[j]["OEMIEMCTION"].ToString());
                            objRecordContent.m_strOEMEMCTIONXML = dtbValue.Rows[j]["OEMEMCTIONXML"].ToString();
                            objRecordContent.m_fltOGASTRICJUICE = float.Parse(dtbValue.Rows[j]["OGASTRICJUICE"].ToString());
                            objRecordContent.m_strOGASTRICJUICEXML = dtbValue.Rows[j]["OGASTRICJUICEXML"].ToString();
                            objRecordContent.m_strSESPECIALLYNOTE = dtbValue.Rows[j]["SESPECIALLYNOTE"].ToString();
                            objRecordContent.m_strSESPECIALLYNOTEXML = dtbValue.Rows[j]["SESPECIALLYNOTEXML"].ToString();
                            objRecordContent.m_strBBLUSETIME = dtbValue.Rows[j]["BBLUSETIME"].ToString();
                            objRecordContent.m_strBBLUSETIMEXML = dtbValue.Rows[j]["BBLUSETIMEXML"].ToString();

                            objRecordContent.m_strBBLUSEMACHINETYPE = dtbValue.Rows[j]["BBLUSEMACHINETYPE"].ToString();
                            objRecordContent.m_strBBLUSEMACHINETYPEXML = dtbValue.Rows[j]["BBLUSEMACHINETYPEXML"].ToString();
                            objRecordContent.m_strBBLUSEMODE = dtbValue.Rows[j]["BBLUSEMODE"].ToString();
                            objRecordContent.m_strBBLUSEMONDEXML = dtbValue.Rows[j]["BBLUSEMONDEXML"].ToString();
                            objRecordContent.m_strBVT = dtbValue.Rows[j]["BVT"].ToString();
                            objRecordContent.m_strBVTXML = dtbValue.Rows[j]["BVTXML"].ToString();
                            objRecordContent.m_strBEXPIREDMV = dtbValue.Rows[j]["BEXPIREDMV"].ToString();
                            objRecordContent.m_strBEXPIREDMVXML = dtbValue.Rows[j]["BEXPIREDMVXML"].ToString();
                            objRecordContent.m_strBBLUESPRESSURE = dtbValue.Rows[j]["BBLUESPRESSURE"].ToString();
                            objRecordContent.m_strBBLUESPRESSUREXML = dtbValue.Rows[j]["BBLUESPRESSUREXML"].ToString();
                            objRecordContent.m_strBBLUSENUM = dtbValue.Rows[j]["BBLUSENUM"].ToString();
                            objRecordContent.m_strBBLUSENUMXML = dtbValue.Rows[j]["BBLUSENUMXML"].ToString();
                            objRecordContent.m_strBFIO2PEEP = dtbValue.Rows[j]["BFIO2PEEP"].ToString();
                            objRecordContent.m_strBFIO2PEEPXML = dtbValue.Rows[j]["BFIO2PEEPXML"].ToString();
                            objRecordContent.m_strBMAXIP = dtbValue.Rows[j]["BMAXIP"].ToString();
                            objRecordContent.m_strBMAXIPXML = dtbValue.Rows[j]["BMAXIPXML"].ToString();

                            objRecordContent.m_strBBLUSESOUND = dtbValue.Rows[j]["BBLUSESOUND"].ToString();
                            objRecordContent.m_strBBLUSESOUNDXML = dtbValue.Rows[j]["BBLUSESOUNDXML"].ToString();
                            objRecordContent.m_strBPHLEGMCOLOR = dtbValue.Rows[j]["BPHLEGMCOLOR"].ToString();
                            objRecordContent.m_strBPHLEGMCOLORXML = dtbValue.Rows[j]["BPHLEGMCOLORXML"].ToString();
                            objRecordContent.m_strBSQ2 = dtbValue.Rows[j]["BSQ2"].ToString();
                            objRecordContent.m_strBSQ2XML = dtbValue.Rows[j]["BSQ2XML"].ToString();
                            objRecordContent.m_strTCOLLECTBLOODPOINT = dtbValue.Rows[j]["TCOLLECTBLOODPOINT"].ToString();
                            objRecordContent.m_strTCOLLECTBLOODPOINTXML = dtbValue.Rows[j]["TCOLLECTBLOODPOINTXML"].ToString();
                            objRecordContent.m_strTPH = dtbValue.Rows[j]["TPH"].ToString();
                            objRecordContent.m_strTPHXML = dtbValue.Rows[j]["TPHXML"].ToString();
                            objRecordContent.m_strTPCO2 = dtbValue.Rows[j]["TPCO2"].ToString();
                            objRecordContent.m_strTPCO2XML = dtbValue.Rows[j]["TPCO2XML"].ToString();
                            objRecordContent.m_strTP02 = dtbValue.Rows[j]["TP02"].ToString();
                            objRecordContent.m_strTPCO2XML = dtbValue.Rows[j]["TPO2XML"].ToString();
                            objRecordContent.m_strTHCO3 = dtbValue.Rows[j]["THCO3"].ToString();
                            objRecordContent.m_strTHCO3XML = dtbValue.Rows[j]["THCO3XML"].ToString();

                            objRecordContent.m_strTTCO2 = dtbValue.Rows[j]["TTCO2"].ToString();
                            objRecordContent.m_strTTCO2XML = dtbValue.Rows[j]["TTCO2XML"].ToString();
                            objRecordContent.m_strTBE = dtbValue.Rows[j]["TBE"].ToString();
                            objRecordContent.m_strTBEXML = dtbValue.Rows[j]["TBEXML"].ToString();
                            objRecordContent.m_strTSAT = dtbValue.Rows[j]["TSAT"].ToString();
                            objRecordContent.m_strTSATXML = dtbValue.Rows[j]["TSATXML"].ToString();
                            objRecordContent.m_strTO2CT = dtbValue.Rows[j]["TO2CT"].ToString();
                            objRecordContent.m_strTO2CTXML = dtbValue.Rows[j]["TO2CTXML"].ToString();
                            objRecordContent.m_strSCMH2O = dtbValue.Rows[j]["SCMH2O"].ToString();
                            objRecordContent.m_strSCMH2OXML = dtbValue.Rows[j]["SCMH2OXML"].ToString();
                            objRecordContent.m_strSSD = dtbValue.Rows[j]["SSD"].ToString();
                            objRecordContent.m_strSSDXML = dtbValue.Rows[j]["SSDXML"].ToString();
                            objRecordContent.m_strSMEAN = dtbValue.Rows[j]["SMEAN"].ToString();
                            objRecordContent.m_strSMEANXML = dtbValue.Rows[j]["SMEANXML"].ToString();
                            objRecordContent.m_strSWEDGE = dtbValue.Rows[j]["SWEDGE"].ToString();
                            objRecordContent.m_strSWEDGEXML = dtbValue.Rows[j]["SWEDGEXML"].ToString();
                            objRecordContent.m_strSCOCI = dtbValue.Rows[j]["SCOCI"].ToString();
                            objRecordContent.m_strSCOCIXML = dtbValue.Rows[j]["SCOCIXML"].ToString();

                            objRecordContent.m_strPBODYPART_Right = dtbValue.Rows[j]["PBODYPART_Right"].ToString();
                            objRecordContent.m_strPCONSCIOUSNESS_Right = dtbValue.Rows[j]["PCONSCIOUSNESS_Right"].ToString();
                            objRecordContent.m_strPPUPIL_Right = dtbValue.Rows[j]["PPUPIL_Right"].ToString();
                            objRecordContent.m_strPREFLECT_Right = dtbValue.Rows[j]["PREFLECT_Right"].ToString();
                            objRecordContent.m_strCTEMPERATURE_Right = dtbValue.Rows[j]["CTEMPERATURE_Right"].ToString();
                            objRecordContent.m_strCSMALLTEMPERATURE_Right = dtbValue.Rows[j]["CSMALLTEMPERATURE_Right"].ToString();
                            objRecordContent.m_strCHEARTRHYTHM_Right = dtbValue.Rows[j]["CHEARTRHYTHM_Right"].ToString();
                            objRecordContent.m_strCSD_Right = dtbValue.Rows[j]["CSD_Right"].ToString();
                            objRecordContent.m_strCCVP_Right = dtbValue.Rows[j]["CCVP_Right"].ToString();
                            objRecordContent.m_strDPHYSIC1_Right = dtbValue.Rows[j]["DPHYSIC1_Right"].ToString();
                            objRecordContent.m_strDPHYSIC2_Right = dtbValue.Rows[j]["DPHYSIC2_Right"].ToString();
                            objRecordContent.m_strDPHYSIC3_Right = dtbValue.Rows[j]["DPHYSIC3_Right"].ToString();
                            objRecordContent.m_strDPHYSIC4_Right = dtbValue.Rows[j]["DPHYSIC4_Right"].ToString();
                            objRecordContent.m_strDPHYSIC5_Right = dtbValue.Rows[j]["DPHYSIC5_Right"].ToString();
                            objRecordContent.m_strDPHYSIC6_Right = dtbValue.Rows[j]["DPHYSIC6_Right"].ToString();
                            objRecordContent.m_strDPHYSIC7_Right = dtbValue.Rows[j]["DPHYSIC7_Right"].ToString();
                            objRecordContent.m_strDPHYSIC8_Right = dtbValue.Rows[j]["DPHYSIC8_Right"].ToString();
                            objRecordContent.m_strDCURE1_Right = dtbValue.Rows[j]["DCURE1_Right"].ToString();
                            objRecordContent.m_strDCURE2_Right = dtbValue.Rows[j]["DCURE2_Right"].ToString();
                            objRecordContent.m_strDCURE3_Right = dtbValue.Rows[j]["DCURE3_Right"].ToString();
                            objRecordContent.m_strDCURE4_Right = dtbValue.Rows[j]["DCURE4_Right"].ToString();
                            objRecordContent.m_strDCURE5_Right = dtbValue.Rows[j]["DCURE5_Right"].ToString();
                            objRecordContent.m_strDCURE6_Right = dtbValue.Rows[j]["DCURE6_Right"].ToString();
                            objRecordContent.m_strDCURE7_Right = dtbValue.Rows[j]["DCURE7_Right"].ToString();
                            objRecordContent.m_strDCURE8_Right = dtbValue.Rows[j]["DCURE8_Right"].ToString();
                            objRecordContent.m_strIGS_Right = dtbValue.Rows[j]["IGS_Right"].ToString();
                            objRecordContent.m_strINS_Right = dtbValue.Rows[j]["INS_Right"].ToString();
                            objRecordContent.m_strINTATAL_Right = dtbValue.Rows[j]["INTATAL_Right"].ToString();
                            objRecordContent.m_strOTATAL_Right = dtbValue.Rows[j]["OTATAL_Right"].ToString();
                            objRecordContent.m_strOEMIEMCTION_Right = dtbValue.Rows[j]["OEMIEMCTION_Right"].ToString();
                            objRecordContent.m_strOGASTRICJUICE_Right = dtbValue.Rows[j]["OGASTRICJUICE_Right"].ToString();
                            objRecordContent.m_strSESPECIALLYNOTE_Right = dtbValue.Rows[j]["SESPECIALLYNOTE_Right"].ToString();
                            objRecordContent.m_strBBLUSETIME_Right = dtbValue.Rows[j]["BBLUSETIME_Right"].ToString();
                            objRecordContent.m_strBBLUSEMACHINETYPE_Right = dtbValue.Rows[j]["BBLUSEMACHINETYPE_Right"].ToString();
                            objRecordContent.m_strBBLUSEMODE_Right = dtbValue.Rows[j]["BBLUSEMODE_Right"].ToString();
                            objRecordContent.m_strBVT_Right = dtbValue.Rows[j]["BVT_Right"].ToString();
                            objRecordContent.m_strBEXPIREDMV_Right = dtbValue.Rows[j]["BEXPIREDMV_Right"].ToString();
                            objRecordContent.m_strBBLUESPRESSURE_Right = dtbValue.Rows[j]["BBLUESPRESSURE_Right"].ToString();
                            objRecordContent.m_strBBLUSENUM_Right = dtbValue.Rows[j]["BBLUSENUM_Right"].ToString();
                            objRecordContent.m_strBFIO2PEEP_Right = dtbValue.Rows[j]["BFIO2PEEP_Right"].ToString();
                            objRecordContent.m_strBMAXIP_Right = dtbValue.Rows[j]["BMAXIP_Right"].ToString();
                            objRecordContent.m_strBBLUSESOUND_Right = dtbValue.Rows[j]["BBLUSESOUND_Right"].ToString();
                            objRecordContent.m_strBPHLEGMCOLOR_Right = dtbValue.Rows[j]["BPHLEGMCOLOR_Right"].ToString();
                            objRecordContent.m_strBSQ2_Right = dtbValue.Rows[j]["BSQ2_Right"].ToString();
                            objRecordContent.m_strTCOLLECTBLOODPOINT_Right = dtbValue.Rows[j]["TCOLLECTBLOODPOINT_Right"].ToString();
                            objRecordContent.m_strTPH_Right = dtbValue.Rows[j]["TPH_Right"].ToString();
                            objRecordContent.m_strTPCO2_Right = dtbValue.Rows[j]["TPCO2_Right"].ToString();
                            objRecordContent.m_strTP02_Right = dtbValue.Rows[j]["TP02_Right"].ToString();
                            objRecordContent.m_strTHCO3_Right = dtbValue.Rows[j]["THCO3_Right"].ToString();
                            objRecordContent.m_strTTCO2_Right = dtbValue.Rows[j]["TTCO2_Right"].ToString();
                            objRecordContent.m_strTSAT_Right = dtbValue.Rows[j]["TSAT_Right"].ToString();
                            objRecordContent.m_strTO2CT_Right = dtbValue.Rows[j]["TO2CT_Right"].ToString();
                            objRecordContent.m_strSCMH2O_Right = dtbValue.Rows[j]["SCMH2O_Right"].ToString();
                            objRecordContent.m_strSSD_Right = dtbValue.Rows[j]["SSD_Right"].ToString();
                            objRecordContent.m_strSMEAN_Right = dtbValue.Rows[j]["SMEAN_Right"].ToString();
                            objRecordContent.m_strSWEDGE_Right = dtbValue.Rows[j]["SWEDGE_Right"].ToString();
                            objRecordContent.m_strSCOCI_Right = dtbValue.Rows[j]["SCOCI_Right"].ToString();
                            objRecordContent.m_strTBE_Right = dtbValue.Rows[j]["TBE_Right"].ToString();
                            objRecordContent.m_strCHEARTRATE_Right = dtbValue.Rows[j]["CHEARTRATE_Right"].ToString();


                            #region//add field 20051117
                            objRecordContent.m_strPPUPLRIGH_RightT = dtbValue.Rows[j]["PPUPLRIGH_Right"].ToString();  //瞳孔(右)
                            objRecordContent.m_strPREFLECTRIGHT_Right = dtbValue.Rows[j]["PREFLECTRIGHT_Right"].ToString();//对光反射(右)'
                            objRecordContent.m_strIBLOODPRODUCE_Right = dtbValue.Rows[j]["IBLOODPRODUCE_Right"].ToString();//血制品';
                            objRecordContent.m_strIBLOODPRODUCEAD_Right = dtbValue.Rows[j]["IBLOODPRODUCEADD_Right"].ToString();// '血制品累计量';
                            objRecordContent.m_strINNAME1_Right = dtbValue.Rows[j]["INNAME1_Right"].ToString();//入量名称1';
                            objRecordContent.m_strINNAME2_Right = dtbValue.Rows[j]["INNAME2_Right"].ToString();//入量名称2';
                            objRecordContent.m_strINNAME3_Right = dtbValue.Rows[j]["INNAME3_Right"].ToString();//入量名称3';
                            objRecordContent.m_strINNAME4_Right = dtbValue.Rows[j]["INNAME4_Right"].ToString();//入量名称4';
                            objRecordContent.m_strINAMOUNT1_Right = dtbValue.Rows[j]["INAMOUNT1_Right"].ToString();//入量数量1';
                            objRecordContent.m_strINAMOUNT2_Right = dtbValue.Rows[j]["INAMOUNT2_Right"].ToString();//入量数量2';
                            objRecordContent.m_strINAMOUNT3_Right = dtbValue.Rows[j]["INAMOUNT3_Right"].ToString();//入量数量3';
                            objRecordContent.m_strINAMOUNT4_Right = dtbValue.Rows[j]["INAMOUNT4_Right"].ToString();//入量数量4';
                            objRecordContent.m_strOUTNAME1_Right = dtbValue.Rows[j]["OUTNAME1_Right"].ToString();//出量名称1';
                            objRecordContent.m_strOUTNAME2_Right = dtbValue.Rows[j]["OUTNAME2_Right"].ToString();//出量名称2';
                            objRecordContent.m_strOUTNAME3_Right = dtbValue.Rows[j]["OUTNAME3_Right"].ToString();//出量名称3';
                            objRecordContent.m_strOUTNAME4_Right = dtbValue.Rows[j]["OUTNAME4_Right"].ToString();//出量名称4';
                            objRecordContent.m_strOUTAMOUNT1_Right = dtbValue.Rows[j]["OUTAMOUNT1_Right"].ToString();//出量数量1';
                            objRecordContent.m_strOUTAMOUNT2_Right = dtbValue.Rows[j]["OUTAMOUNT2_Right"].ToString();//出量数量2';
                            objRecordContent.m_strOUTAMOUNT3_Right = dtbValue.Rows[j]["OUTAMOUNT3_Right"].ToString();//出量数量3';
                            objRecordContent.m_strOUTAMOUNT4_Right = dtbValue.Rows[j]["OUTAMOUNT4_Right"].ToString();//出量数量4';
                            objRecordContent.m_strBFI02PEEPRIGHT_Right = dtbValue.Rows[j]["BFI02PEEPRIGHT_Right"].ToString();//PEEP
                            objRecordContent.m_strBPHLEGMAMOUNT_Right = dtbValue.Rows[j]["BPHLEGMAMOUNT_Right"].ToString();//痰量

                            objRecordContent.m_strPPUPLRIGHT = dtbValue.Rows[j]["PPUPLRIGHT"].ToString();  //瞳孔(右)
                            objRecordContent.m_strPREFLECTRIGHT = dtbValue.Rows[j]["PREFLECTRIGHT"].ToString();//对光反射(右)'
                            objRecordContent.m_fltIBLOODPRODUCE = float.Parse(dtbValue.Rows[j]["IBLOODPRODUCE"].ToString());//血制品';
                            objRecordContent.m_fltIBLOODPRODUCEADD = float.Parse(dtbValue.Rows[j]["IBLOODPRODUCEADD"].ToString());// '血制品累计量';
                            objRecordContent.m_strINNAME1 = dtbValue.Rows[j]["INNAME1"].ToString();//入量名称1';
                            objRecordContent.m_strINNAME2 = dtbValue.Rows[j]["INNAME2"].ToString();//入量名称2';
                            objRecordContent.m_strINNAME3 = dtbValue.Rows[j]["INNAME3"].ToString();//入量名称3';
                            objRecordContent.m_strINNAME4 = dtbValue.Rows[j]["INNAME4"].ToString();//入量名称4';
                            objRecordContent.m_fltINAMOUNT1 = float.Parse(dtbValue.Rows[j]["INAMOUNT1"].ToString());//入量数量1';
                            objRecordContent.m_fltINAMOUNT2 = float.Parse(dtbValue.Rows[j]["INAMOUNT2"].ToString());//入量数量2';
                            objRecordContent.m_fltINAMOUNT3 = float.Parse(dtbValue.Rows[j]["INAMOUNT3"].ToString());//入量数量3';
                            objRecordContent.m_fltINAMOUNT4 = float.Parse(dtbValue.Rows[j]["INAMOUNT4"].ToString());//入量数量4';
                            objRecordContent.m_strOUTNAME1 = dtbValue.Rows[j]["OUTNAME1"].ToString();//出量名称1';
                            objRecordContent.m_strOUTNAME2 = dtbValue.Rows[j]["OUTNAME2"].ToString();//出量名称2';
                            objRecordContent.m_strOUTNAME3 = dtbValue.Rows[j]["OUTNAME3"].ToString();//出量名称3';
                            objRecordContent.m_strOUTNAME4 = dtbValue.Rows[j]["OUTNAME4"].ToString();//出量名称4';
                            objRecordContent.m_fltOUTAMOUNT1 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT1"].ToString());//出量数量1';
                            objRecordContent.m_fltOUTAMOUNT2 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT2"].ToString());//出量数量2';
                            objRecordContent.m_fltOUTAMOUNT3 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT3"].ToString());//出量数量3';
                            objRecordContent.m_fltOUTAMOUNT4 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT4"].ToString());//出量数量4';
                            objRecordContent.m_strBFI02PEEPRIGHT = dtbValue.Rows[j]["BFI02PEEPRIGHT"].ToString();//PEEP
                            objRecordContent.m_strBPHLEGMAMOUNT = dtbValue.Rows[j]["BPHLEGMAMOUNT"].ToString();//痰量

                            objRecordContent.m_strPPUPLRIGHTXML = dtbValue.Rows[j]["PPUPLRIGHTXML"].ToString();  //瞳孔(右)
                            objRecordContent.m_strPREFLECTRIGHTXML = dtbValue.Rows[j]["PREFLECTRIGHTXML"].ToString();//对光反射(右)'
                            objRecordContent.m_strIBLOODPRODUCEXML = dtbValue.Rows[j]["IBLOODPRODUCEXML"].ToString();//血制品';
                            objRecordContent.m_strIBLOODPRODUCEADDXML = dtbValue.Rows[j]["IBLOODPRODUCEADDXML"].ToString();// '血制品累计量';
                            objRecordContent.m_strINNAME1XML = dtbValue.Rows[j]["INNAME1XML"].ToString();//入量名称1';
                            objRecordContent.m_strINNAME2XML = dtbValue.Rows[j]["INNAME2XML"].ToString();//入量名称2';
                            objRecordContent.m_strINNAME3XML = dtbValue.Rows[j]["INNAME3XML"].ToString();//入量名称3';
                            objRecordContent.m_strINNAME4XML = dtbValue.Rows[j]["INNAME4XML"].ToString();//入量名称4';
                            objRecordContent.m_strINAMOUNT1XML = dtbValue.Rows[j]["INAMOUNT1XML"].ToString();//入量数量1';
                            objRecordContent.m_strINAMOUNT2XML = dtbValue.Rows[j]["INAMOUNT2XML"].ToString();//入量数量2';
                            objRecordContent.m_strINAMOUNT3XML = dtbValue.Rows[j]["INAMOUNT3XML"].ToString();//入量数量3';
                            objRecordContent.m_strINAMOUNT4XML = dtbValue.Rows[j]["INAMOUNT4XML"].ToString();//入量数量4';
                            objRecordContent.m_strOUTNAME1XML = dtbValue.Rows[j]["OUTNAME1XML"].ToString();//出量名称1';
                            objRecordContent.m_strOUTNAME2XML = dtbValue.Rows[j]["OUTNAME2XML"].ToString();//出量名称2';
                            objRecordContent.m_strOUTNAME3XML = dtbValue.Rows[j]["OUTNAME3XML"].ToString();//出量名称3';
                            objRecordContent.m_strOUTNAME4XML = dtbValue.Rows[j]["OUTNAME4XML"].ToString();//出量名称4';
                            objRecordContent.m_strOUTAMOUNT1XML = dtbValue.Rows[j]["OUTAMOUNT1XML"].ToString();//出量数量1';
                            objRecordContent.m_strOUTAMOUNT2XML = dtbValue.Rows[j]["OUTAMOUNT2XML"].ToString();//出量数量2';
                            objRecordContent.m_strOUTAMOUNT3XML = dtbValue.Rows[j]["OUTAMOUNT3XML"].ToString();//出量数量3';
                            objRecordContent.m_strOUTAMOUNT4XML = dtbValue.Rows[j]["OUTAMOUNT4XML"].ToString();//出量数量4';
                            objRecordContent.m_strBFI02PEEPRIGHTXML = dtbValue.Rows[j]["BFI02PEEPRIGHTXML"].ToString();//PEEP
                            objRecordContent.m_strBPHLEGMAMOUNTXML = dtbValue.Rows[j]["BPHLEGMAMOUNTXML"].ToString();//痰量

                            #endregion

                            objRecordContent.m_strWEIGHT = dtbValue.Rows[j]["WEIGHT"].ToString();
                            objRecordContent.m_strIDCODE = dtbValue.Rows[j]["IDCODE"].ToString();
                            objRecordContent.m_strOPERATIONNAME = dtbValue.Rows[j]["OPERATIONNAME"].ToString();
                            if (dtbValue.Rows[j]["OPERATIONDATE"].ToString().Trim().Length > 0)
                                objRecordContent.m_strOPERATIONDATE = DateTime.Parse(dtbValue.Rows[j]["OPERATIONDATE"].ToString().Trim());
                            else
                                objRecordContent.m_strOPERATIONDATE = DateTime.Parse("1900-01-01");

                            objRecordContent.m_strDATEAFTEROPERATION = dtbValue.Rows[j]["DATEAFTEROPERATION"].ToString();



                            #endregion
                        }

                        p_objTansDataInfo[j] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            } return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 获取指定记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objGeneralNurseRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objIntensiveTendInfoArr)
		{
			clsISURGERYICUWARDSHIP[] p_objISURGERYICUWARDSHIP = null;
			p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsISURGERYICUWARDSHIPDataInfo objDataInfo = new clsISURGERYICUWARDSHIPDataInfo();

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//护理记录内容  

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsISURGERYICUWARDSHIP objRecordContent = null;
                    p_objISURGERYICUWARDSHIP = new clsISURGERYICUWARDSHIP[dtbValue.Rows.Count];
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        objRecordContent = new clsISURGERYICUWARDSHIP();
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OpenDate"].ToString());

                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                        objRecordContent.m_strCreateUserName = dtbValue.Rows[j]["CreateUserName"].ToString();
                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                            objRecordContent.m_strSTATUS = "0";
                        else objRecordContent.m_strSTATUS = dtbValue.Rows[j]["STATUS"].ToString();

                        objRecordContent.m_strPBODYPART = dtbValue.Rows[j]["PBODYPART"].ToString();
                        objRecordContent.m_strPBODYPARTXML = dtbValue.Rows[j]["PBODYPARTXML"].ToString();
                        objRecordContent.m_strPCONSCIOUSNESS = dtbValue.Rows[j]["PCONSCIOUSNESS"].ToString();
                        objRecordContent.m_strPCONSCIOUSNESSXML = dtbValue.Rows[j]["PCONSCIOUSNESSXML"].ToString();
                        objRecordContent.m_strPPUPIL = dtbValue.Rows[j]["PPUPIL"].ToString();
                        objRecordContent.m_strPPUPILXML = dtbValue.Rows[j]["PPUPILXML"].ToString();
                        objRecordContent.m_strPREFLECT = dtbValue.Rows[j]["PREFLECT"].ToString();
                        objRecordContent.m_strPREFLECTXML = dtbValue.Rows[j]["PREFLECTXML"].ToString();
                        objRecordContent.m_strCTEMPERATURE = dtbValue.Rows[j]["CTEMPERATURE"].ToString();
                        objRecordContent.m_strCTEMPERATUREXML = dtbValue.Rows[j]["CTEMPERATUREXML"].ToString();
                        objRecordContent.m_strCSMALLTEMPERATURE = dtbValue.Rows[j]["CSMALLTEMPERATURE"].ToString();
                        objRecordContent.m_strCSMALLTEMPERATUREXML = dtbValue.Rows[j]["CSMALLTEMPERATUREXML"].ToString();
                        objRecordContent.m_strCHEARTRATE = dtbValue.Rows[j]["CHEARTRATE"].ToString();
                        objRecordContent.m_strCHEARTRATEXML = dtbValue.Rows[j]["CHEARTRATEXML"].ToString();
                        objRecordContent.m_strCHEARTRHYTHM = dtbValue.Rows[j]["CHEARTRHYTHM"].ToString();
                        objRecordContent.m_strCHEARTRHYTHMXML = dtbValue.Rows[j]["CHEARTRHYTHMXML"].ToString();
                        objRecordContent.m_strCSD = dtbValue.Rows[j]["CSD"].ToString();
                        objRecordContent.m_strCSDXML = dtbValue.Rows[j]["CSDXML"].ToString();
                        objRecordContent.m_strCCVP = dtbValue.Rows[j]["CCVP"].ToString();
                        objRecordContent.m_strCCVPXML = dtbValue.Rows[j]["CCVPXML"].ToString();

                        objRecordContent.m_strDPHYSIC1 = dtbValue.Rows[j]["DPHYSIC1"].ToString();
                        objRecordContent.m_strDPHYSIC1XML = dtbValue.Rows[j]["DPHYSIC1XML"].ToString();
                        objRecordContent.m_strDPHYSIC2 = dtbValue.Rows[j]["DPHYSIC2"].ToString();
                        objRecordContent.m_strDPHYSIC2XML = dtbValue.Rows[j]["DPHYSIC2XML"].ToString();
                        objRecordContent.m_strDPHYSIC3 = dtbValue.Rows[j]["DPHYSIC3"].ToString();
                        objRecordContent.m_strDPHYSIC3XML = dtbValue.Rows[j]["DPHYSIC3XML"].ToString();
                        objRecordContent.m_strDPHYSIC4 = dtbValue.Rows[j]["DPHYSIC4"].ToString();
                        objRecordContent.m_strDPHYSIC4XML = dtbValue.Rows[j]["DPHYSIC4XML"].ToString();
                        objRecordContent.m_strDPHYSIC5 = dtbValue.Rows[j]["DPHYSIC5"].ToString();
                        objRecordContent.m_strDPHYSIC5XML = dtbValue.Rows[j]["DPHYSIC5XML"].ToString();
                        objRecordContent.m_strDPHYSIC6 = dtbValue.Rows[j]["DPHYSIC6"].ToString();
                        objRecordContent.m_strDPHYSIC6XML = dtbValue.Rows[j]["DPHYSIC6XML"].ToString();
                        objRecordContent.m_strDPHYSIC7 = dtbValue.Rows[j]["DPHYSIC7"].ToString();
                        objRecordContent.m_strDPHYSIC7XML = dtbValue.Rows[j]["DPHYSIC7XML"].ToString();
                        objRecordContent.m_strDPHYSIC8 = dtbValue.Rows[j]["DPHYSIC8"].ToString();
                        objRecordContent.m_strDPHYSIC8XML = dtbValue.Rows[j]["DPHYSIC8XML"].ToString();

                        objRecordContent.m_strDCURE1 = dtbValue.Rows[j]["DCURE1"].ToString();
                        objRecordContent.m_strDCURE1XML = dtbValue.Rows[j]["DCURE1XML"].ToString();
                        objRecordContent.m_strDCURE2 = dtbValue.Rows[j]["DCURE2"].ToString();
                        objRecordContent.m_strDCURE2XML = dtbValue.Rows[j]["DCURE2XML"].ToString();
                        objRecordContent.m_strDCURE3 = dtbValue.Rows[j]["DCURE3"].ToString();
                        objRecordContent.m_strDCURE3XML = dtbValue.Rows[j]["DCURE3XML"].ToString();
                        objRecordContent.m_strDCURE4 = dtbValue.Rows[j]["DCURE4"].ToString();
                        objRecordContent.m_strDCURE4XML = dtbValue.Rows[j]["DCURE4XML"].ToString();
                        objRecordContent.m_strDCURE5 = dtbValue.Rows[j]["DCURE5"].ToString();
                        objRecordContent.m_strDCURE5XML = dtbValue.Rows[j]["DCURE5XML"].ToString();
                        objRecordContent.m_strDCURE6 = dtbValue.Rows[j]["DCURE6"].ToString();
                        objRecordContent.m_strDCURE6XML = dtbValue.Rows[j]["DCURE6XML"].ToString();
                        objRecordContent.m_strDCURE7 = dtbValue.Rows[j]["DCURE7"].ToString();
                        objRecordContent.m_strDCURE7XML = dtbValue.Rows[j]["DCURE7XML"].ToString();
                        objRecordContent.m_strDCURE8 = dtbValue.Rows[j]["DCURE8"].ToString();
                        objRecordContent.m_strDCURE8XML = dtbValue.Rows[j]["DCURE8XML"].ToString();

                        objRecordContent.m_fltIGS = float.Parse(dtbValue.Rows[j]["IGS"].ToString());
                        objRecordContent.m_strIGSXML = dtbValue.Rows[j]["IGSXML"].ToString();
                        objRecordContent.m_fltINS = float.Parse(dtbValue.Rows[j]["INS"].ToString());
                        objRecordContent.m_strINSXML = dtbValue.Rows[j]["INSXML"].ToString();
                        objRecordContent.m_fltINTATAL = float.Parse(dtbValue.Rows[j]["INTATAL"].ToString());
                        objRecordContent.m_strINTATALXML = dtbValue.Rows[j]["INTATALXML"].ToString();
                        objRecordContent.m_fltOTATAL = float.Parse(dtbValue.Rows[j]["OTATAL"].ToString());
                        objRecordContent.m_strOTATALXML = dtbValue.Rows[j]["OTATALXML"].ToString();
                        objRecordContent.m_fltOEMIEMCTION = float.Parse(dtbValue.Rows[j]["OEMIEMCTION"].ToString());
                        objRecordContent.m_strOEMEMCTIONXML = dtbValue.Rows[j]["OEMEMCTIONXML"].ToString();
                        objRecordContent.m_fltOGASTRICJUICE = float.Parse(dtbValue.Rows[j]["OGASTRICJUICE"].ToString());
                        objRecordContent.m_strOGASTRICJUICEXML = dtbValue.Rows[j]["OGASTRICJUICEXML"].ToString();
                        objRecordContent.m_strSESPECIALLYNOTE = dtbValue.Rows[j]["SESPECIALLYNOTE"].ToString();
                        objRecordContent.m_strSESPECIALLYNOTEXML = dtbValue.Rows[j]["SESPECIALLYNOTEXML"].ToString();
                        objRecordContent.m_strBBLUSETIME = dtbValue.Rows[j]["BBLUSETIME"].ToString();
                        objRecordContent.m_strBBLUSETIMEXML = dtbValue.Rows[j]["BBLUSETIMEXML"].ToString();

                        objRecordContent.m_strBBLUSEMACHINETYPE = dtbValue.Rows[j]["BBLUSEMACHINETYPE"].ToString();
                        objRecordContent.m_strBBLUSEMACHINETYPEXML = dtbValue.Rows[j]["BBLUSEMACHINETYPEXML"].ToString();
                        objRecordContent.m_strBBLUSEMODE = dtbValue.Rows[j]["BBLUSEMODE"].ToString();
                        objRecordContent.m_strBBLUSEMONDEXML = dtbValue.Rows[j]["BBLUSEMONDEXML"].ToString();
                        objRecordContent.m_strBVT = dtbValue.Rows[j]["BVT"].ToString();
                        objRecordContent.m_strBVTXML = dtbValue.Rows[j]["BVTXML"].ToString();
                        objRecordContent.m_strBEXPIREDMV = dtbValue.Rows[j]["BEXPIREDMV"].ToString();
                        objRecordContent.m_strBEXPIREDMVXML = dtbValue.Rows[j]["BEXPIREDMVXML"].ToString();
                        objRecordContent.m_strBBLUESPRESSURE = dtbValue.Rows[j]["BBLUESPRESSURE"].ToString();
                        objRecordContent.m_strBBLUESPRESSUREXML = dtbValue.Rows[j]["BBLUESPRESSUREXML"].ToString();
                        objRecordContent.m_strBBLUSENUM = dtbValue.Rows[j]["BBLUSENUM"].ToString();
                        objRecordContent.m_strBBLUSENUMXML = dtbValue.Rows[j]["BBLUSENUMXML"].ToString();
                        objRecordContent.m_strBFIO2PEEP = dtbValue.Rows[j]["BFIO2PEEP"].ToString();
                        objRecordContent.m_strBFIO2PEEPXML = dtbValue.Rows[j]["BFIO2PEEPXML"].ToString();
                        objRecordContent.m_strBMAXIP = dtbValue.Rows[j]["BMAXIP"].ToString();
                        objRecordContent.m_strBMAXIPXML = dtbValue.Rows[j]["BMAXIPXML"].ToString();

                        objRecordContent.m_strBBLUSESOUND = dtbValue.Rows[j]["BBLUSESOUND"].ToString();
                        objRecordContent.m_strBBLUSESOUNDXML = dtbValue.Rows[j]["BBLUSESOUNDXML"].ToString();
                        objRecordContent.m_strBPHLEGMCOLOR = dtbValue.Rows[j]["BPHLEGMCOLOR"].ToString();
                        objRecordContent.m_strBPHLEGMCOLORXML = dtbValue.Rows[j]["BPHLEGMCOLORXML"].ToString();
                        objRecordContent.m_strBSQ2 = dtbValue.Rows[j]["BSQ2"].ToString();
                        objRecordContent.m_strBSQ2XML = dtbValue.Rows[j]["BSQ2XML"].ToString();
                        objRecordContent.m_strTCOLLECTBLOODPOINT = dtbValue.Rows[j]["TCOLLECTBLOODPOINT"].ToString();
                        objRecordContent.m_strTCOLLECTBLOODPOINTXML = dtbValue.Rows[j]["TCOLLECTBLOODPOINTXML"].ToString();
                        objRecordContent.m_strTPH = dtbValue.Rows[j]["TPH"].ToString();
                        objRecordContent.m_strTPHXML = dtbValue.Rows[j]["TPHXML"].ToString();
                        objRecordContent.m_strTPCO2 = dtbValue.Rows[j]["TPCO2"].ToString();
                        objRecordContent.m_strTPCO2XML = dtbValue.Rows[j]["TPCO2XML"].ToString();
                        objRecordContent.m_strTP02 = dtbValue.Rows[j]["TP02"].ToString();
                        objRecordContent.m_strTPCO2XML = dtbValue.Rows[j]["TPO2XML"].ToString();
                        objRecordContent.m_strTHCO3 = dtbValue.Rows[j]["THCO3"].ToString();
                        objRecordContent.m_strTHCO3XML = dtbValue.Rows[j]["THCO3XML"].ToString();

                        objRecordContent.m_strTTCO2 = dtbValue.Rows[j]["TTCO2"].ToString();
                        objRecordContent.m_strTTCO2XML = dtbValue.Rows[j]["TTCO2XML"].ToString();
                        objRecordContent.m_strTBE = dtbValue.Rows[j]["TBE"].ToString();
                        objRecordContent.m_strTBEXML = dtbValue.Rows[j]["TBEXML"].ToString();
                        objRecordContent.m_strTSAT = dtbValue.Rows[j]["TSAT"].ToString();
                        objRecordContent.m_strTSATXML = dtbValue.Rows[j]["TSATXML"].ToString();
                        objRecordContent.m_strTO2CT = dtbValue.Rows[j]["TO2CT"].ToString();
                        objRecordContent.m_strTO2CTXML = dtbValue.Rows[j]["TO2CTXML"].ToString();
                        objRecordContent.m_strSCMH2O = dtbValue.Rows[j]["SCMH2O"].ToString();
                        objRecordContent.m_strSCMH2OXML = dtbValue.Rows[j]["SCMH2OXML"].ToString();
                        objRecordContent.m_strSSD = dtbValue.Rows[j]["SSD"].ToString();
                        objRecordContent.m_strSSDXML = dtbValue.Rows[j]["SSDXML"].ToString();
                        objRecordContent.m_strSMEAN = dtbValue.Rows[j]["SMEAN"].ToString();
                        objRecordContent.m_strSMEANXML = dtbValue.Rows[j]["SMEANXML"].ToString();
                        objRecordContent.m_strSWEDGE = dtbValue.Rows[j]["SWEDGE"].ToString();
                        objRecordContent.m_strSWEDGEXML = dtbValue.Rows[j]["SWEDGEXML"].ToString();
                        objRecordContent.m_strSCOCI = dtbValue.Rows[j]["SCOCI"].ToString();
                        objRecordContent.m_strSCOCIXML = dtbValue.Rows[j]["SCOCIXML"].ToString();

                        objRecordContent.m_strPBODYPART_Right = dtbValue.Rows[j]["PBODYPART_Right"].ToString();
                        objRecordContent.m_strPCONSCIOUSNESS_Right = dtbValue.Rows[j]["PCONSCIOUSNESS_Right"].ToString();
                        objRecordContent.m_strPPUPIL_Right = dtbValue.Rows[j]["PPUPIL_Right"].ToString();
                        objRecordContent.m_strPREFLECT_Right = dtbValue.Rows[j]["PREFLECT_Right"].ToString();
                        objRecordContent.m_strCTEMPERATURE_Right = dtbValue.Rows[j]["CTEMPERATURE_Right"].ToString();
                        objRecordContent.m_strCSMALLTEMPERATURE_Right = dtbValue.Rows[j]["CSMALLTEMPERATURE_Right"].ToString();
                        objRecordContent.m_strCHEARTRHYTHM_Right = dtbValue.Rows[j]["CHEARTRHYTHM_Right"].ToString();
                        objRecordContent.m_strCSD_Right = dtbValue.Rows[j]["CSD_Right"].ToString();
                        objRecordContent.m_strCCVP_Right = dtbValue.Rows[j]["CCVP_Right"].ToString();
                        objRecordContent.m_strDPHYSIC1_Right = dtbValue.Rows[j]["DPHYSIC1_Right"].ToString();
                        objRecordContent.m_strDPHYSIC2_Right = dtbValue.Rows[j]["DPHYSIC2_Right"].ToString();
                        objRecordContent.m_strDPHYSIC3_Right = dtbValue.Rows[j]["DPHYSIC3_Right"].ToString();
                        objRecordContent.m_strDPHYSIC4_Right = dtbValue.Rows[j]["DPHYSIC4_Right"].ToString();
                        objRecordContent.m_strDPHYSIC5_Right = dtbValue.Rows[j]["DPHYSIC5_Right"].ToString();
                        objRecordContent.m_strDPHYSIC6_Right = dtbValue.Rows[j]["DPHYSIC6_Right"].ToString();
                        objRecordContent.m_strDPHYSIC7_Right = dtbValue.Rows[j]["DPHYSIC7_Right"].ToString();
                        objRecordContent.m_strDPHYSIC8_Right = dtbValue.Rows[j]["DPHYSIC8_Right"].ToString();
                        objRecordContent.m_strDCURE1_Right = dtbValue.Rows[j]["DCURE1_Right"].ToString();
                        objRecordContent.m_strDCURE2_Right = dtbValue.Rows[j]["DCURE2_Right"].ToString();
                        objRecordContent.m_strDCURE3_Right = dtbValue.Rows[j]["DCURE3_Right"].ToString();
                        objRecordContent.m_strDCURE4_Right = dtbValue.Rows[j]["DCURE4_Right"].ToString();
                        objRecordContent.m_strDCURE5_Right = dtbValue.Rows[j]["DCURE5_Right"].ToString();
                        objRecordContent.m_strDCURE6_Right = dtbValue.Rows[j]["DCURE6_Right"].ToString();
                        objRecordContent.m_strDCURE7_Right = dtbValue.Rows[j]["DCURE7_Right"].ToString();
                        objRecordContent.m_strDCURE8_Right = dtbValue.Rows[j]["DCURE8_Right"].ToString();
                        objRecordContent.m_strIGS_Right = dtbValue.Rows[j]["IGS_Right"].ToString();
                        objRecordContent.m_strINS_Right = dtbValue.Rows[j]["INS_Right"].ToString();
                        objRecordContent.m_strINTATAL_Right = dtbValue.Rows[j]["INTATAL_Right"].ToString();
                        objRecordContent.m_strOTATAL_Right = dtbValue.Rows[j]["OTATAL_Right"].ToString();
                        objRecordContent.m_strOEMIEMCTION_Right = dtbValue.Rows[j]["OEMIEMCTION_Right"].ToString();
                        objRecordContent.m_strOGASTRICJUICE_Right = dtbValue.Rows[j]["OGASTRICJUICE_Right"].ToString();
                        objRecordContent.m_strSESPECIALLYNOTE_Right = dtbValue.Rows[j]["SESPECIALLYNOTE_Right"].ToString();
                        objRecordContent.m_strBBLUSETIME_Right = dtbValue.Rows[j]["BBLUSETIME_Right"].ToString();
                        objRecordContent.m_strBBLUSEMACHINETYPE_Right = dtbValue.Rows[j]["BBLUSEMACHINETYPE_Right"].ToString();
                        objRecordContent.m_strBBLUSEMODE_Right = dtbValue.Rows[j]["BBLUSEMODE_Right"].ToString();
                        objRecordContent.m_strBVT_Right = dtbValue.Rows[j]["BVT_Right"].ToString();
                        objRecordContent.m_strBEXPIREDMV_Right = dtbValue.Rows[j]["BEXPIREDMV_Right"].ToString();
                        objRecordContent.m_strBBLUESPRESSURE_Right = dtbValue.Rows[j]["BBLUESPRESSURE_Right"].ToString();
                        objRecordContent.m_strBBLUSENUM_Right = dtbValue.Rows[j]["BBLUSENUM_Right"].ToString();
                        objRecordContent.m_strBFIO2PEEP_Right = dtbValue.Rows[j]["BFIO2PEEP_Right"].ToString();
                        objRecordContent.m_strBMAXIP_Right = dtbValue.Rows[j]["BMAXIP_Right"].ToString();
                        objRecordContent.m_strBBLUSESOUND_Right = dtbValue.Rows[j]["BBLUSESOUND_Right"].ToString();
                        objRecordContent.m_strBPHLEGMCOLOR_Right = dtbValue.Rows[j]["BPHLEGMCOLOR_Right"].ToString();
                        objRecordContent.m_strBSQ2_Right = dtbValue.Rows[j]["BSQ2_Right"].ToString();
                        objRecordContent.m_strTCOLLECTBLOODPOINT_Right = dtbValue.Rows[j]["TCOLLECTBLOODPOINT_Right"].ToString();
                        objRecordContent.m_strTPH_Right = dtbValue.Rows[j]["TPH_Right"].ToString();
                        objRecordContent.m_strTPCO2_Right = dtbValue.Rows[j]["TPCO2_Right"].ToString();
                        objRecordContent.m_strTP02_Right = dtbValue.Rows[j]["TP02_Right"].ToString();
                        objRecordContent.m_strTHCO3_Right = dtbValue.Rows[j]["THCO3_Right"].ToString();
                        objRecordContent.m_strTTCO2_Right = dtbValue.Rows[j]["TTCO2_Right"].ToString();
                        objRecordContent.m_strTSAT_Right = dtbValue.Rows[j]["TSAT_Right"].ToString();
                        objRecordContent.m_strTO2CT_Right = dtbValue.Rows[j]["TO2CT_Right"].ToString();
                        objRecordContent.m_strSCMH2O_Right = dtbValue.Rows[j]["SCMH2O_Right"].ToString();
                        objRecordContent.m_strSSD_Right = dtbValue.Rows[j]["SSD_Right"].ToString();
                        objRecordContent.m_strSMEAN_Right = dtbValue.Rows[j]["SMEAN_Right"].ToString();
                        objRecordContent.m_strSWEDGE_Right = dtbValue.Rows[j]["SWEDGE_Right"].ToString();
                        objRecordContent.m_strSCOCI_Right = dtbValue.Rows[j]["SCOCI_Right"].ToString();
                        objRecordContent.m_strTBE_Right = dtbValue.Rows[j]["TBE_Right"].ToString();
                        objRecordContent.m_strCHEARTRATE_Right = dtbValue.Rows[j]["CHEARTRATE_Right"].ToString();

                        #region//add field 20051117
                        objRecordContent.m_strPPUPLRIGH_RightT = dtbValue.Rows[j]["PPUPLRIGH_Right"].ToString();  //瞳孔(右)
                        objRecordContent.m_strPREFLECTRIGHT_Right = dtbValue.Rows[j]["PREFLECTRIGHT_Right"].ToString();//对光反射(右)'
                        objRecordContent.m_strIBLOODPRODUCE_Right = dtbValue.Rows[j]["IBLOODPRODUCE_Right"].ToString();//血制品';
                        objRecordContent.m_strIBLOODPRODUCEAD_Right = dtbValue.Rows[j]["IBLOODPRODUCEADD_Right"].ToString();// '血制品累计量';
                        objRecordContent.m_strINNAME1_Right = dtbValue.Rows[j]["INNAME1_Right"].ToString();//入量名称1';
                        objRecordContent.m_strINNAME2_Right = dtbValue.Rows[j]["INNAME2_Right"].ToString();//入量名称2';
                        objRecordContent.m_strINNAME3_Right = dtbValue.Rows[j]["INNAME3_Right"].ToString();//入量名称3';
                        objRecordContent.m_strINNAME4_Right = dtbValue.Rows[j]["INNAME4_Right"].ToString();//入量名称4';
                        objRecordContent.m_strINAMOUNT1_Right = dtbValue.Rows[j]["INAMOUNT1_Right"].ToString();//入量数量1';
                        objRecordContent.m_strINAMOUNT2_Right = dtbValue.Rows[j]["INAMOUNT2_Right"].ToString();//入量数量2';
                        objRecordContent.m_strINAMOUNT3_Right = dtbValue.Rows[j]["INAMOUNT3_Right"].ToString();//入量数量3';
                        objRecordContent.m_strINAMOUNT4_Right = dtbValue.Rows[j]["INAMOUNT4_Right"].ToString();//入量数量4';
                        objRecordContent.m_strOUTNAME1_Right = dtbValue.Rows[j]["OUTNAME1_Right"].ToString();//出量名称1';
                        objRecordContent.m_strOUTNAME2_Right = dtbValue.Rows[j]["OUTNAME2_Right"].ToString();//出量名称2';
                        objRecordContent.m_strOUTNAME3_Right = dtbValue.Rows[j]["OUTNAME3_Right"].ToString();//出量名称3';
                        objRecordContent.m_strOUTNAME4_Right = dtbValue.Rows[j]["OUTNAME4_Right"].ToString();//出量名称4';
                        objRecordContent.m_strOUTAMOUNT1_Right = dtbValue.Rows[j]["OUTAMOUNT1_Right"].ToString();//出量数量1';
                        objRecordContent.m_strOUTAMOUNT2_Right = dtbValue.Rows[j]["OUTAMOUNT2_Right"].ToString();//出量数量2';
                        objRecordContent.m_strOUTAMOUNT3_Right = dtbValue.Rows[j]["OUTAMOUNT3_Right"].ToString();//出量数量3';
                        objRecordContent.m_strOUTAMOUNT4_Right = dtbValue.Rows[j]["OUTAMOUNT4_Right"].ToString();//出量数量4';
                        objRecordContent.m_strBFI02PEEPRIGHT_Right = dtbValue.Rows[j]["BFI02PEEPRIGHT_Right"].ToString();//PEEP
                        objRecordContent.m_strBPHLEGMAMOUNT_Right = dtbValue.Rows[j]["BPHLEGMAMOUNT_Right"].ToString();//痰量

                        objRecordContent.m_strPPUPLRIGHT = dtbValue.Rows[j]["PPUPLRIGHT"].ToString();  //瞳孔(右)
                        objRecordContent.m_strPREFLECTRIGHT = dtbValue.Rows[j]["PREFLECTRIGHT"].ToString();//对光反射(右)'
                        objRecordContent.m_fltIBLOODPRODUCE = float.Parse(dtbValue.Rows[j]["IBLOODPRODUCE"].ToString());//血制品';
                        objRecordContent.m_fltIBLOODPRODUCEADD = float.Parse(dtbValue.Rows[j]["IBLOODPRODUCEADD"].ToString());// '血制品累计量';
                        objRecordContent.m_strINNAME1 = dtbValue.Rows[j]["INNAME1"].ToString();//入量名称1';
                        objRecordContent.m_strINNAME2 = dtbValue.Rows[j]["INNAME2"].ToString();//入量名称2';
                        objRecordContent.m_strINNAME3 = dtbValue.Rows[j]["INNAME3"].ToString();//入量名称3';
                        objRecordContent.m_strINNAME4 = dtbValue.Rows[j]["INNAME4"].ToString();//入量名称4';
                        objRecordContent.m_fltINAMOUNT1 = float.Parse(dtbValue.Rows[j]["INAMOUNT1"].ToString());//入量数量1';
                        objRecordContent.m_fltINAMOUNT2 = float.Parse(dtbValue.Rows[j]["INAMOUNT2"].ToString());//入量数量2';
                        objRecordContent.m_fltINAMOUNT3 = float.Parse(dtbValue.Rows[j]["INAMOUNT3"].ToString());//入量数量3';
                        objRecordContent.m_fltINAMOUNT4 = float.Parse(dtbValue.Rows[j]["INAMOUNT4"].ToString());//入量数量4';
                        objRecordContent.m_strOUTNAME1 = dtbValue.Rows[j]["OUTNAME1"].ToString();//出量名称1';
                        objRecordContent.m_strOUTNAME2 = dtbValue.Rows[j]["OUTNAME2"].ToString();//出量名称2';
                        objRecordContent.m_strOUTNAME3 = dtbValue.Rows[j]["OUTNAME3"].ToString();//出量名称3';
                        objRecordContent.m_strOUTNAME4 = dtbValue.Rows[j]["OUTNAME4"].ToString();//出量名称4';
                        objRecordContent.m_fltOUTAMOUNT1 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT1"].ToString());//出量数量1';
                        objRecordContent.m_fltOUTAMOUNT2 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT2"].ToString());//出量数量2';
                        objRecordContent.m_fltOUTAMOUNT3 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT3"].ToString());//出量数量3';
                        objRecordContent.m_fltOUTAMOUNT4 = float.Parse(dtbValue.Rows[j]["OUTAMOUNT4"].ToString());//出量数量4';
                        objRecordContent.m_strBFI02PEEPRIGHT = dtbValue.Rows[j]["BFI02PEEPRIGHT"].ToString();//PEEP
                        objRecordContent.m_strBPHLEGMAMOUNT = dtbValue.Rows[j]["BPHLEGMAMOUNT"].ToString();//痰量

                        objRecordContent.m_strPPUPLRIGHTXML = dtbValue.Rows[j]["PPUPLRIGHTXML"].ToString();  //瞳孔(右)
                        objRecordContent.m_strPREFLECTRIGHTXML = dtbValue.Rows[j]["PREFLECTRIGHTXML"].ToString();//对光反射(右)'
                        objRecordContent.m_strIBLOODPRODUCEXML = dtbValue.Rows[j]["IBLOODPRODUCEXML"].ToString();//血制品';
                        objRecordContent.m_strIBLOODPRODUCEADDXML = dtbValue.Rows[j]["IBLOODPRODUCEADDXML"].ToString();// '血制品累计量';
                        objRecordContent.m_strINNAME1XML = dtbValue.Rows[j]["INNAME1XML"].ToString();//入量名称1';
                        objRecordContent.m_strINNAME2XML = dtbValue.Rows[j]["INNAME2XML"].ToString();//入量名称2';
                        objRecordContent.m_strINNAME3XML = dtbValue.Rows[j]["INNAME3XML"].ToString();//入量名称3';
                        objRecordContent.m_strINNAME4XML = dtbValue.Rows[j]["INNAME4XML"].ToString();//入量名称4';
                        objRecordContent.m_strINAMOUNT1XML = dtbValue.Rows[j]["INAMOUNT1XML"].ToString();//入量数量1';
                        objRecordContent.m_strINAMOUNT2XML = dtbValue.Rows[j]["INAMOUNT2XML"].ToString();//入量数量2';
                        objRecordContent.m_strINAMOUNT3XML = dtbValue.Rows[j]["INAMOUNT3XML"].ToString();//入量数量3';
                        objRecordContent.m_strINAMOUNT4XML = dtbValue.Rows[j]["INAMOUNT4XML"].ToString();//入量数量4';
                        objRecordContent.m_strOUTNAME1XML = dtbValue.Rows[j]["OUTNAME1XML"].ToString();//出量名称1';
                        objRecordContent.m_strOUTNAME2XML = dtbValue.Rows[j]["OUTNAME2XML"].ToString();//出量名称2';
                        objRecordContent.m_strOUTNAME3XML = dtbValue.Rows[j]["OUTNAME3XML"].ToString();//出量名称3';
                        objRecordContent.m_strOUTNAME4XML = dtbValue.Rows[j]["OUTNAME4XML"].ToString();//出量名称4';
                        objRecordContent.m_strOUTAMOUNT1XML = dtbValue.Rows[j]["OUTAMOUNT1XML"].ToString();//出量数量1';
                        objRecordContent.m_strOUTAMOUNT2XML = dtbValue.Rows[j]["OUTAMOUNT2XML"].ToString();//出量数量2';
                        objRecordContent.m_strOUTAMOUNT3XML = dtbValue.Rows[j]["OUTAMOUNT3XML"].ToString();//出量数量3';
                        objRecordContent.m_strOUTAMOUNT4XML = dtbValue.Rows[j]["OUTAMOUNT4XML"].ToString();//出量数量4';
                        objRecordContent.m_strBFI02PEEPRIGHTXML = dtbValue.Rows[j]["BFI02PEEPRIGHTXML"].ToString();//PEEP
                        objRecordContent.m_strBPHLEGMAMOUNTXML = dtbValue.Rows[j]["BPHLEGMAMOUNTXML"].ToString();//痰量

                        #endregion

                        if (dtbValue.Rows[j]["DEACTIVEDDATE"] == DBNull.Value)
                            objRecordContent.m_dtmDEACTIVEDDATE = DateTime.Parse("1900-01-01");
                        else
                            objRecordContent.m_dtmDEACTIVEDDATE = DateTime.Parse(dtbValue.Rows[j]["DEACTIVEDDATE"].ToString());
                        objRecordContent.m_strDEACTIVEDOPERATORID = dtbValue.Rows[j]["DEACTIVEDOPERATORID"].ToString();
                        if (dtbValue.Rows[j]["MODIFYDATE"] == DBNull.Value)//RECORDDATE
                            objRecordContent.m_dtmRECORDDATE = DateTime.Parse("1900-01-01");
                        else
                            objRecordContent.m_dtmRECORDDATE = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                        objRecordContent.m_strWEIGHT = dtbValue.Rows[j]["WEIGHT"].ToString();
                        objRecordContent.m_strIDCODE = dtbValue.Rows[j]["IDCODE"].ToString();
                        objRecordContent.m_strOPERATIONNAME = dtbValue.Rows[j]["OPERATIONNAME"].ToString();
                        if (dtbValue.Rows[j]["OPERATIONDATE"].ToString().Trim().Length > 0)
                            objRecordContent.m_strOPERATIONDATE = DateTime.Parse(dtbValue.Rows[j]["OPERATIONDATE"].ToString().Trim());
                        else
                            objRecordContent.m_strOPERATIONDATE = DateTime.Parse("1900-01-01");
                        objRecordContent.m_strDATEAFTEROPERATION = dtbValue.Rows[j]["DATEAFTEROPERATION"].ToString();


                        p_objISURGERYICUWARDSHIP[j] = objRecordContent;
                    }
                }
                objDataInfo.m_objRecordArr = p_objISURGERYICUWARDSHIP;


                p_objIntensiveTendInfoArr[0] = objDataInfo;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return (long)enmOperationResult.DB_Succeed;
		}

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
			p_objModifyInfo=null;

			//检查参数          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from 
											t_emr_surgeryicuwardship t1,t_emr_surgeryicuwardshipnote t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status = '0'
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

				
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

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
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
	}
}
