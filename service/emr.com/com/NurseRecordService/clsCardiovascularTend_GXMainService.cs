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
	/// 心血管外科特护记录(广西)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsCardiovascularTend_GXMainService : clsRecordsService
	{
		#region SQL语句
		private const string c_strUpdateFirstPrintDateSQL=@"update t_emr_cardiovasculartend_gx
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
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.recorddate,
       t1.infact1,
       t1.infact1xml,
       t1.infact2,
       t1.infact2xml,
       t1.infact3,
       t1.infact3xml,
       t1.infact4,
       t1.infact4xml,
       t1.infact5,
       t1.infact5xml,
       t1.inblood,
       t1.inbloodxml,
       t1.inperhour,
       t1.inperhourxml,
       t1.insum,
       t1.insumxml,
       t1.outsum,
       t1.outsumxml,
       t1.outperhour,
       t1.outperhourxml,
       t1.outfactpisssum,
       t1.outfactpisssumxml,
       t1.outfactpiss,
       t1.outfactpissxml,
       t1.outfactchestjuice,
       t1.outfactchestjuicexml,
       t1.outfactchestjuicesum,
       t1.outfactchestjuicesumxml,
       t1.outfactgastricjuice,
       t1.outfactgastricjuicexml,
       t1.expandvasmedicine,
       t1.cardiacdiuresis,
       t1.othermedicine,
       t1.consciousness,
       t1.consciousnessxml,
       t1.pupil,
       t1.pupilxml,
       t1.leftpupil,
       t1.leftpupilxml,
       t1.rightpupil,
       t1.rightpupilxml,
       t1.temperature,
       t1.temperaturexml,
       t1.twigtemperature,
       t1.twigtemperaturexml,
       t1.heartrate,
       t1.heartratexml,
       t1.heartrhythm,
       t1.heartrhythmxml,
       t1.bpa,
       t1.bpaxml,
       t1.bps,
       t1.bpsxml,
       t1.avgbp,
       t1.avgbpxml,
       t1.cvp,
       t1.cvpxml,
       t1.lap,
       t1.lapxml,
       t1.breathmachine,
       t1.breathmachinexml,
       t1.insertdepth,
       t1.insertdepthxml,
       t1.assistant,
       t1.assistantxml,
       t1.fio2,
       t1.fio2xml,
       t1.peep,
       t1.peepxml,
       t1.tv,
       t1.tvxml,
       t1.vf,
       t1.vfxml,
       t1.breathtimes,
       t1.breathtimesxml,
       t1.leftbreathvoice,
       t1.leftbreathvoicexml,
       t1.rightbreathvoice,
       t1.rightbreathvoicexml,
       t1.phlegmcolor,
       t1.phlegmcolorxml,
       t1.phlegmquantity,
       t1.phlegmquantityxml,
       t1.gesticulation,
       t1.gesticulationxml,
       t1.physicaltherapy,
       t1.physicaltherapyxml,
       t1.remark,
       t1.remarkxml,
       t1.wbc,
       t1.wbcxml,
       t1.hb,
       t1.hbxml,
       t1.rbc,
       t1.rbcxml,
       t1.hct,
       t1.hctxml,
       t1.plt,
       t1.pltxml,
       t1.ph,
       t1.phxml,
       t1.pco2,
       t1.pco2xml,
       t1.pao2,
       t1.pao2xml,
       t1.hco3,
       t1.hco3xml,
       t1.be,
       t1.bexml,
       t1.kplus,
       t1.kplusxml,
       t1.naplus,
       t1.naplusxml,
       t1.cisub,
       t1.cisubxml,
       t1.caplusplus,
       t1.caplusplusxml,
       t1.glu,
       t1.gluxml,
       t1.bun,
       t1.bunxml,
       t1.ua,
       t1.uaxml,
       t1.anhydride,
       t1.anhydridexml,
       t1.co2cp,
       t1.co2cpxml,
       t1.pt,
       t1.ptxml,
       t1.xraycheck,
       t1.xraycheckxml,
       t1.act,
       t1.actxml,
       t1.proportion,
       t1.proportionxml,
       t1.albumen,
       t1.albumenxml,
       t1.hiddenblood,
       t1.hiddenbloodxml,
       t1.skin,
       t1.skinxml,
       t1.washperineum,
       t1.washperineumxml,
       t1.brushbath,
       t1.brushbathxml,
       t1.mouthtend,
       t1.mouthtendxml,
       t1.ie,
       t1.iexml,
       t1.inspiration,
       t1.inspirationxml,
       t1.spo,
       t1.spoxml,
       t3.modifydate,
       t3.modifyuserid,
       t3.infact1_right,
       t3.infact2_right,
       t3.infact3_right,
       t3.infact4_right,
       t3.infact5_right,
       t3.inblood_right,
       t3.inperhour_right,
       t3.insum_right,
       t3.outsum_right,
       t3.outperhour_right,
       t3.outfactpisssum_right,
       t3.outfactpiss_right,
       t3.outfactchestjuice_right,
       t3.outfactchestjuicesum_right,
       t3.outfactgastricjuice_right,
       t3.expandvasmedicine_right,
       t3.cardiacdiuresis_right,
       t3.othermedicine_right,
       t3.consciousness_right,
       t3.pupil_right,
       t3.leftpupil_right,
       t3.rightpupil_right,
       t3.temperature_right,
       t3.twigtemperature_right,
       t3.heartrate_right,
       t3.heartrhythm_right,
       t3.bpa_right,
       t3.bps_right,
       t3.avgbp_right,
       t3.cvp_right,
       t3.lap_right,
       t3.breathmachine_right,
       t3.insertdepth_right,
       t3.assistant_right,
       t3.fio2_right,
       t3.peep_right,
       t3.tv_right,
       t3.vf_right,
       t3.breathtimes_right,
       t3.leftbreathvoice_right,
       t3.rightbreathvoice_right,
       t3.phlegmcolor_right,
       t3.phlegmquantity_right,
       t3.gesticulation_right,
       t3.physicaltherapy_right,
       t3.remark_right,
       t3.wbc_right,
       t3.hb_right,
       t3.rbc_right,
       t3.hct_right,
       t3.plt_right,
       t3.ph_right,
       t3.pco2_right,
       t3.pao2_right,
       t3.hco3_right,
       t3.be_right,
       t3.kplus_right,
       t3.naplus_right,
       t3.cisub_right,
       t3.caplusplus_right,
       t3.glu_right,
       t3.bun_right,
       t3.ua_right,
       t3.anhydride_right,
       t3.co2cp_right,
       t3.pt_right,
       t3.xraycheck_right,
       t3.act_right,
       t3.proportion_right,
       t3.albumen_right,
       t3.hiddenblood_right,
       t3.skin_right,
       t3.washperineum_right,
       t3.brushbath_right,
       t3.mouthtend_right,
       t3.ie_right,
       t3.inspiration_right,
       t3.spo_right
  from t_emr_cardiovasculartend_gx t1, t_emr_cardiovasculartendcon_gx t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.recorddate, t3.modifydate";

        private const string c_strGetRecordContentSQL_Single = @"select f_getempnamebyno(t1.createuserid) as createusername,
       t1.inpatientid,
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
       t1.recorddate,
       t1.infact1,
       t1.infact1xml,
       t1.infact2,
       t1.infact2xml,
       t1.infact3,
       t1.infact3xml,
       t1.infact4,
       t1.infact4xml,
       t1.infact5,
       t1.infact5xml,
       t1.inblood,
       t1.inbloodxml,
       t1.inperhour,
       t1.inperhourxml,
       t1.insum,
       t1.insumxml,
       t1.outsum,
       t1.outsumxml,
       t1.outperhour,
       t1.outperhourxml,
       t1.outfactpisssum,
       t1.outfactpisssumxml,
       t1.outfactpiss,
       t1.outfactpissxml,
       t1.outfactchestjuice,
       t1.outfactchestjuicexml,
       t1.outfactchestjuicesum,
       t1.outfactchestjuicesumxml,
       t1.outfactgastricjuice,
       t1.outfactgastricjuicexml,
       t1.expandvasmedicine,
       t1.cardiacdiuresis,
       t1.othermedicine,
       t1.consciousness,
       t1.consciousnessxml,
       t1.pupil,
       t1.pupilxml,
       t1.leftpupil,
       t1.leftpupilxml,
       t1.rightpupil,
       t1.rightpupilxml,
       t1.temperature,
       t1.temperaturexml,
       t1.twigtemperature,
       t1.twigtemperaturexml,
       t1.heartrate,
       t1.heartratexml,
       t1.heartrhythm,
       t1.heartrhythmxml,
       t1.bpa,
       t1.bpaxml,
       t1.bps,
       t1.bpsxml,
       t1.avgbp,
       t1.avgbpxml,
       t1.cvp,
       t1.cvpxml,
       t1.lap,
       t1.lapxml,
       t1.breathmachine,
       t1.breathmachinexml,
       t1.insertdepth,
       t1.insertdepthxml,
       t1.assistant,
       t1.assistantxml,
       t1.fio2,
       t1.fio2xml,
       t1.peep,
       t1.peepxml,
       t1.tv,
       t1.tvxml,
       t1.vf,
       t1.vfxml,
       t1.breathtimes,
       t1.breathtimesxml,
       t1.leftbreathvoice,
       t1.leftbreathvoicexml,
       t1.rightbreathvoice,
       t1.rightbreathvoicexml,
       t1.phlegmcolor,
       t1.phlegmcolorxml,
       t1.phlegmquantity,
       t1.phlegmquantityxml,
       t1.gesticulation,
       t1.gesticulationxml,
       t1.physicaltherapy,
       t1.physicaltherapyxml,
       t1.remark,
       t1.remarkxml,
       t1.wbc,
       t1.wbcxml,
       t1.hb,
       t1.hbxml,
       t1.rbc,
       t1.rbcxml,
       t1.hct,
       t1.hctxml,
       t1.plt,
       t1.pltxml,
       t1.ph,
       t1.phxml,
       t1.pco2,
       t1.pco2xml,
       t1.pao2,
       t1.pao2xml,
       t1.hco3,
       t1.hco3xml,
       t1.be,
       t1.bexml,
       t1.kplus,
       t1.kplusxml,
       t1.naplus,
       t1.naplusxml,
       t1.cisub,
       t1.cisubxml,
       t1.caplusplus,
       t1.caplusplusxml,
       t1.glu,
       t1.gluxml,
       t1.bun,
       t1.bunxml,
       t1.ua,
       t1.uaxml,
       t1.anhydride,
       t1.anhydridexml,
       t1.co2cp,
       t1.co2cpxml,
       t1.pt,
       t1.ptxml,
       t1.xraycheck,
       t1.xraycheckxml,
       t1.act,
       t1.actxml,
       t1.proportion,
       t1.proportionxml,
       t1.albumen,
       t1.albumenxml,
       t1.hiddenblood,
       t1.hiddenbloodxml,
       t1.skin,
       t1.skinxml,
       t1.washperineum,
       t1.washperineumxml,
       t1.brushbath,
       t1.brushbathxml,
       t1.mouthtend,
       t1.mouthtendxml,
       t1.ie,
       t1.iexml,
       t1.inspiration,
       t1.inspirationxml,
       t1.spo,
       t1.spoxml,
       t3.modifydate,
       t3.modifyuserid,
       t3.infact1_right,
       t3.infact2_right,
       t3.infact3_right,
       t3.infact4_right,
       t3.infact5_right,
       t3.inblood_right,
       t3.inperhour_right,
       t3.insum_right,
       t3.outsum_right,
       t3.outperhour_right,
       t3.outfactpisssum_right,
       t3.outfactpiss_right,
       t3.outfactchestjuice_right,
       t3.outfactchestjuicesum_right,
       t3.outfactgastricjuice_right,
       t3.expandvasmedicine_right,
       t3.cardiacdiuresis_right,
       t3.othermedicine_right,
       t3.consciousness_right,
       t3.pupil_right,
       t3.leftpupil_right,
       t3.rightpupil_right,
       t3.temperature_right,
       t3.twigtemperature_right,
       t3.heartrate_right,
       t3.heartrhythm_right,
       t3.bpa_right,
       t3.bps_right,
       t3.avgbp_right,
       t3.cvp_right,
       t3.lap_right,
       t3.breathmachine_right,
       t3.insertdepth_right,
       t3.assistant_right,
       t3.fio2_right,
       t3.peep_right,
       t3.tv_right,
       t3.vf_right,
       t3.breathtimes_right,
       t3.leftbreathvoice_right,
       t3.rightbreathvoice_right,
       t3.phlegmcolor_right,
       t3.phlegmquantity_right,
       t3.gesticulation_right,
       t3.physicaltherapy_right,
       t3.remark_right,
       t3.wbc_right,
       t3.hb_right,
       t3.rbc_right,
       t3.hct_right,
       t3.plt_right,
       t3.ph_right,
       t3.pco2_right,
       t3.pao2_right,
       t3.hco3_right,
       t3.be_right,
       t3.kplus_right,
       t3.naplus_right,
       t3.cisub_right,
       t3.caplusplus_right,
       t3.glu_right,
       t3.bun_right,
       t3.ua_right,
       t3.anhydride_right,
       t3.co2cp_right,
       t3.pt_right,
       t3.xraycheck_right,
       t3.act_right,
       t3.proportion_right,
       t3.albumen_right,
       t3.hiddenblood_right,
       t3.skin_right,
       t3.washperineum_right,
       t3.brushbath_right,
       t3.mouthtend_right,
       t3.ie_right,
       t3.inspiration_right,
       t3.spo_right
  from t_emr_cardiovasculartend_gx t1, t_emr_cardiovasculartendcon_gx t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.recorddate between ? and ?
 order by t1.recorddate, t3.modifydate";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from t_emr_cardiovasculartend_gx
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		private const string c_strDeleteRecordSQL=@"update t_emr_cardiovasculartend_gx
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

        private const string c_strGetBaseInfoSQL = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.createdate,
       t.createuserid,
       t.modifydate,
       t.modifyuserid,
       t.weight,
       t.afteropdays,
       t.opname,
       t.opmedicine1,
       t.opmedicine2,
       t.opmedicine3,
       t.opmedicine4,
       t.opmedicine5,
       t.longclasssignid,
       t.officesignid,
       t.smallnightclasssignid,
       t.bignightclasssignid,
       t.recorddate,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       f_getempnamebyno(t.createuserid) as createusername
  from t_emr_cardiovascularbaseinfo t
 where t.status = 0
   and inpatientid = ?
   and t.inpatientdate = ?
 order by t.recorddate";

		private const string c_strDeleteBaseInfoSQL=@"update t_emr_cardiovascularbaseinfo
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCardiovascularTend_GXMainService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;				

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||
				p_dtmOpenDateArr==null||p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
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
				long lngRes=0;				
				long lngEff=0;
				lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL,ref lngEff,objDPArr);
                if (lngRes <= 0)
                {
                    //objHRPServ.Dispose();
                    return lngRes;
                }
			}

            //objHRPServ.Dispose();
			return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 修改或添加一条记录时读数据库
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordDate,
			out clsCardiovascularTend_GX[] p_objTansDataInfo)
		{
			p_objTansDataInfo=null;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
				clsHRPTableService objHRPServ =new clsHRPTableService();

			try
			{
				DateTime dtmStart = DateTime.Parse(p_strRecordDate);
				string strEnd = dtmStart.ToString("yyyy-MM-dd 23:59:59");

				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(4,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = dtmStart;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(strEnd);
					
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_objTansDataInfo = new clsCardiovascularTend_GX[dtbValue.Rows.Count];
					clsCardiovascularTend_GX objRecordContent= null;
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						objRecordContent = new clsCardiovascularTend_GX();
						#region 从DataTable.Rows中获取结果
						objRecordContent.m_strInPatientID = p_strInPatientID;
						objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
						objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
						objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
						objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
						objRecordContent.m_strConfirmReason = dtbValue.Rows[i]["CONFIRMREASON"].ToString();
						objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[i]["CONFIRMREASONXML"].ToString();
						if(dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString()=="")
							objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
						else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
						objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

						objRecordContent.m_strINFACT1 = dtbValue.Rows[i]["INFACT1"].ToString();
						objRecordContent.m_strINFACT1XML = dtbValue.Rows[i]["INFACT1XML"].ToString();
						objRecordContent.m_strINFACT2 = dtbValue.Rows[i]["INFACT2"].ToString();
						objRecordContent.m_strINFACT2XML = dtbValue.Rows[i]["INFACT2XML"].ToString();
						objRecordContent.m_strINFACT3 = dtbValue.Rows[i]["INFACT3"].ToString();
						objRecordContent.m_strINFACT3XML = dtbValue.Rows[i]["INFACT3XML"].ToString();
						objRecordContent.m_strINFACT4 = dtbValue.Rows[i]["INFACT4"].ToString();
						objRecordContent.m_strINFACT4XML = dtbValue.Rows[i]["INFACT4XML"].ToString();
						objRecordContent.m_strINFACT5 = dtbValue.Rows[i]["INFACT5"].ToString();
						objRecordContent.m_strINFACT5XML = dtbValue.Rows[i]["INFACT5XML"].ToString();
						objRecordContent.m_strINBLOOD = dtbValue.Rows[i]["INBLOOD"].ToString();
						objRecordContent.m_strINBLOODXML = dtbValue.Rows[i]["INBLOODXML"].ToString();
						objRecordContent.m_strINPERHOUR = dtbValue.Rows[i]["INPERHOUR"].ToString();
						objRecordContent.m_strINPERHOURXML = dtbValue.Rows[i]["INPERHOURXML"].ToString();
						objRecordContent.m_strINSUM = dtbValue.Rows[i]["INSUM"].ToString();
						objRecordContent.m_strINSUMXML = dtbValue.Rows[i]["INSUMXML"].ToString();
						objRecordContent.m_strOUTSUM = dtbValue.Rows[i]["OUTSUM"].ToString();
						objRecordContent.m_strOUTSUMXML = dtbValue.Rows[i]["OUTSUMXML"].ToString();
						objRecordContent.m_strOUTPERHOUR = dtbValue.Rows[i]["OUTPERHOUR"].ToString();
						objRecordContent.m_strOUTPERHOURXML = dtbValue.Rows[i]["OUTPERHOURXML"].ToString();
						objRecordContent.m_strOUTFACTPISSSUM = dtbValue.Rows[i]["OUTFACTPISSSUM"].ToString();
						objRecordContent.m_strOUTFACTPISSSUMXML = dtbValue.Rows[i]["OUTFACTPISSSUMXML"].ToString();
						objRecordContent.m_strOUTFACTPISS = dtbValue.Rows[i]["OUTFACTPISS"].ToString();
						objRecordContent.m_strOUTFACTPISSXML = dtbValue.Rows[i]["OUTFACTPISSXML"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICE = dtbValue.Rows[i]["OUTFACTCHESTJUICE"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICEXML = dtbValue.Rows[i]["OUTFACTCHESTJUICEXML"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICESUM = dtbValue.Rows[i]["OUTFACTCHESTJUICESUM"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICESUMXML = dtbValue.Rows[i]["OUTFACTCHESTJUICESUMXML"].ToString();
						objRecordContent.m_strOUTFACTGASTRICJUICE = dtbValue.Rows[i]["OUTFACTGASTRICJUICE"].ToString();
						objRecordContent.m_strOUTFACTGASTRICJUICEXML = dtbValue.Rows[i]["OUTFACTGASTRICJUICEXML"].ToString();
						objRecordContent.m_strEXPANDVASMEDICINE = dtbValue.Rows[i]["EXPANDVASMEDICINE"].ToString();
						objRecordContent.m_strCARDIACDIURESIS = dtbValue.Rows[i]["CARDIACDIURESIS"].ToString();
						objRecordContent.m_strOTHERMEDICINE = dtbValue.Rows[i]["OTHERMEDICINE"].ToString();
						objRecordContent.m_strCONSCIOUSNESS = dtbValue.Rows[i]["CONSCIOUSNESS"].ToString();
						objRecordContent.m_strCONSCIOUSNESSXML = dtbValue.Rows[i]["CONSCIOUSNESSXML"].ToString();
						objRecordContent.m_strPUPIL = dtbValue.Rows[i]["PUPIL"].ToString();
						objRecordContent.m_strPUPILXML = dtbValue.Rows[i]["PUPILXML"].ToString();
						objRecordContent.m_strLEFTPUPIL = dtbValue.Rows[i]["LEFTPUPIL"].ToString();
						objRecordContent.m_strLEFTPUPILXML = dtbValue.Rows[i]["LEFTPUPILXML"].ToString();
						objRecordContent.m_strRIGHTPUPIL = dtbValue.Rows[i]["RIGHTPUPIL"].ToString();
						objRecordContent.m_strRIGHTPUPILXML = dtbValue.Rows[i]["RIGHTPUPILXML"].ToString();
						objRecordContent.m_strTEMPERATURE = dtbValue.Rows[i]["TEMPERATURE"].ToString();
						objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[i]["TEMPERATUREXML"].ToString();
						objRecordContent.m_strTWIGTEMPERATURE = dtbValue.Rows[i]["TWIGTEMPERATURE"].ToString();
						objRecordContent.m_strTWIGTEMPERATUREXML = dtbValue.Rows[i]["TWIGTEMPERATUREXML"].ToString();
						objRecordContent.m_strHEARTRATE = dtbValue.Rows[i]["HEARTRATE"].ToString();
						objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[i]["HEARTRATEXML"].ToString();
						objRecordContent.m_strHEARTRHYTHM = dtbValue.Rows[i]["HEARTRHYTHM"].ToString();
						objRecordContent.m_strHEARTRHYTHMXML = dtbValue.Rows[i]["HEARTRHYTHMXML"].ToString();
						objRecordContent.m_strBPA = dtbValue.Rows[i]["BPA"].ToString();
						objRecordContent.m_strBPAXML = dtbValue.Rows[i]["BPAXML"].ToString();
						objRecordContent.m_strBPS = dtbValue.Rows[i]["BPS"].ToString();
						objRecordContent.m_strBPSXML = dtbValue.Rows[i]["BPSXML"].ToString();
						objRecordContent.m_strAVGBP = dtbValue.Rows[i]["AVGBP"].ToString();
						objRecordContent.m_strAVGBPXML = dtbValue.Rows[i]["AVGBPXML"].ToString();
						objRecordContent.m_strCVP = dtbValue.Rows[i]["CVP"].ToString();
						objRecordContent.m_strCVPXML = dtbValue.Rows[i]["CVPXML"].ToString();
						objRecordContent.m_strLAP = dtbValue.Rows[i]["LAP"].ToString();
						objRecordContent.m_strLAPXML = dtbValue.Rows[i]["LAPXML"].ToString();

                        objRecordContent.m_strSPO = dtbValue.Rows[i]["SPO"].ToString();
                        objRecordContent.m_strSPOXML = dtbValue.Rows[i]["SPOXML"].ToString();

						objRecordContent.m_strBREATHMACHINE = dtbValue.Rows[i]["BREATHMACHINE"].ToString();
						objRecordContent.m_strBREATHMACHINEXML = dtbValue.Rows[i]["BREATHMACHINEXML"].ToString();
						objRecordContent.m_strINSERTDEPTH = dtbValue.Rows[i]["INSERTDEPTH"].ToString();
						objRecordContent.m_strINSERTDEPTHXML = dtbValue.Rows[i]["INSERTDEPTHXML"].ToString();
						objRecordContent.m_strASSISTANT = dtbValue.Rows[i]["ASSISTANT"].ToString();
						objRecordContent.m_strASSISTANTXML = dtbValue.Rows[i]["ASSISTANTXML"].ToString();
						objRecordContent.m_strFIO2 = dtbValue.Rows[i]["FIO2"].ToString();
						objRecordContent.m_strFIO2XML = dtbValue.Rows[i]["FIO2XML"].ToString();
						objRecordContent.m_strPEEP = dtbValue.Rows[i]["PEEP"].ToString();
						objRecordContent.m_strPEEPXML = dtbValue.Rows[i]["PEEPXML"].ToString();
						objRecordContent.m_strTV = dtbValue.Rows[i]["TV"].ToString();
						objRecordContent.m_strTVXML = dtbValue.Rows[i]["TVXML"].ToString();
						objRecordContent.m_strVF = dtbValue.Rows[i]["VF"].ToString();
						objRecordContent.m_strVFXML = dtbValue.Rows[i]["VFXML"].ToString();
						objRecordContent.m_strBREATHTIMES = dtbValue.Rows[i]["BREATHTIMES"].ToString();
						objRecordContent.m_strBREATHTIMESXML = dtbValue.Rows[i]["BREATHTIMESXML"].ToString();
						objRecordContent.m_strLEFTBREATHVOICE = dtbValue.Rows[i]["LEFTBREATHVOICE"].ToString();
						objRecordContent.m_strLEFTBREATHVOICEXML = dtbValue.Rows[i]["LEFTBREATHVOICEXML"].ToString();
						objRecordContent.m_strRIGHTBREATHVOICE = dtbValue.Rows[i]["RIGHTBREATHVOICE"].ToString();
						objRecordContent.m_strRIGHTBREATHVOICEXML = dtbValue.Rows[i]["RIGHTBREATHVOICEXML"].ToString();
						objRecordContent.m_strPHLEGMCOLOR = dtbValue.Rows[i]["PHLEGMCOLOR"].ToString();
						objRecordContent.m_strPHLEGMCOLORXML = dtbValue.Rows[i]["PHLEGMCOLORXML"].ToString();
						objRecordContent.m_strPHLEGMQUANTITY = dtbValue.Rows[i]["PHLEGMQUANTITY"].ToString();
						objRecordContent.m_strPHLEGMQUANTITYXML = dtbValue.Rows[i]["PHLEGMQUANTITYXML"].ToString();
						objRecordContent.m_strGESTICULATION = dtbValue.Rows[i]["GESTICULATION"].ToString();
						objRecordContent.m_strGESTICULATIONXML = dtbValue.Rows[i]["GESTICULATIONXML"].ToString();
						objRecordContent.m_strPHYSICALTHERAPY = dtbValue.Rows[i]["PHYSICALTHERAPY"].ToString();
						objRecordContent.m_strPHYSICALTHERAPYXML = dtbValue.Rows[i]["PHYSICALTHERAPYXML"].ToString();
						objRecordContent.m_strREMARK = dtbValue.Rows[i]["REMARK"].ToString();
						objRecordContent.m_strREMARKXML = dtbValue.Rows[i]["REMARKXML"].ToString();
						objRecordContent.m_strWBC = dtbValue.Rows[i]["WBC"].ToString();
						objRecordContent.m_strWBCXML = dtbValue.Rows[i]["WBCXML"].ToString();
						objRecordContent.m_strHB = dtbValue.Rows[i]["HB"].ToString();
						objRecordContent.m_strHBXML = dtbValue.Rows[i]["HBXML"].ToString();
						objRecordContent.m_strRBC = dtbValue.Rows[i]["RBC"].ToString();
						objRecordContent.m_strRBCXML = dtbValue.Rows[i]["RBCXML"].ToString();
						objRecordContent.m_strHCT = dtbValue.Rows[i]["HCT"].ToString();
						objRecordContent.m_strHCTXML = dtbValue.Rows[i]["HCTXML"].ToString();
						objRecordContent.m_strPLT = dtbValue.Rows[i]["PLT"].ToString();
						objRecordContent.m_strPLTXML = dtbValue.Rows[i]["PLTXML"].ToString();
						objRecordContent.m_strPH = dtbValue.Rows[i]["PH"].ToString();
						objRecordContent.m_strPHXML = dtbValue.Rows[i]["PHXML"].ToString();
						objRecordContent.m_strPCO2 = dtbValue.Rows[i]["PCO2"].ToString();
						objRecordContent.m_strPCO2XML = dtbValue.Rows[i]["PCO2XML"].ToString();
						objRecordContent.m_strPAO2 = dtbValue.Rows[i]["PAO2"].ToString();
						objRecordContent.m_strPAO2XML = dtbValue.Rows[i]["PAO2XML"].ToString();
						objRecordContent.m_strHCO3 = dtbValue.Rows[i]["HCO3"].ToString();
						objRecordContent.m_strHCO3XML = dtbValue.Rows[i]["HCO3XML"].ToString();
						objRecordContent.m_strBE = dtbValue.Rows[i]["BE"].ToString();
						objRecordContent.m_strBEXML = dtbValue.Rows[i]["BEXML"].ToString();
						objRecordContent.m_strKPLUS = dtbValue.Rows[i]["KPLUS"].ToString();
						objRecordContent.m_strKPLUSXML = dtbValue.Rows[i]["KPLUSXML"].ToString();
						objRecordContent.m_strNAPLUS = dtbValue.Rows[i]["NAPLUS"].ToString();
						objRecordContent.m_strNAPLUSXML = dtbValue.Rows[i]["NAPLUSXML"].ToString();
						objRecordContent.m_strCISUB = dtbValue.Rows[i]["CISUB"].ToString();
						objRecordContent.m_strCISUBXML = dtbValue.Rows[i]["CISUBXML"].ToString();
						objRecordContent.m_strCAPLUSPLUS = dtbValue.Rows[i]["CAPLUSPLUS"].ToString();
						objRecordContent.m_strCAPLUSPLUSXML = dtbValue.Rows[i]["CAPLUSPLUSXML"].ToString();
						objRecordContent.m_strGLU = dtbValue.Rows[i]["GLU"].ToString();
						objRecordContent.m_strGLUXML = dtbValue.Rows[i]["GLUXML"].ToString();
						objRecordContent.m_strBUN = dtbValue.Rows[i]["BUN"].ToString();
						objRecordContent.m_strBUNXML = dtbValue.Rows[i]["BUNXML"].ToString();
						objRecordContent.m_strUA = dtbValue.Rows[i]["UA"].ToString();
						objRecordContent.m_strUAXML = dtbValue.Rows[i]["UAXML"].ToString();
						objRecordContent.m_strANHYDRIDE = dtbValue.Rows[i]["ANHYDRIDE"].ToString();
						objRecordContent.m_strANHYDRIDEXML = dtbValue.Rows[i]["ANHYDRIDEXML"].ToString();
						objRecordContent.m_strCO2CP = dtbValue.Rows[i]["CO2CP"].ToString();
						objRecordContent.m_strCO2CPXML = dtbValue.Rows[i]["CO2CPXML"].ToString();
						objRecordContent.m_strPT = dtbValue.Rows[i]["PT"].ToString();
						objRecordContent.m_strPTXML = dtbValue.Rows[i]["PTXML"].ToString();
						objRecordContent.m_strXRAYCHECK = dtbValue.Rows[i]["XRAYCHECK"].ToString();
						objRecordContent.m_strXRAYCHECKXML = dtbValue.Rows[i]["XRAYCHECKXML"].ToString();
						objRecordContent.m_strACT = dtbValue.Rows[i]["ACT"].ToString();
						objRecordContent.m_strACTXML = dtbValue.Rows[i]["ACTXML"].ToString();
						objRecordContent.m_strPROPORTION = dtbValue.Rows[i]["PROPORTION"].ToString();
						objRecordContent.m_strPROPORTIONXML = dtbValue.Rows[i]["PROPORTIONXML"].ToString();
						objRecordContent.m_strALBUMEN = dtbValue.Rows[i]["ALBUMEN"].ToString();
						objRecordContent.m_strALBUMENXML = dtbValue.Rows[i]["ALBUMENXML"].ToString();
						objRecordContent.m_strHIDDENBLOOD = dtbValue.Rows[i]["HIDDENBLOOD"].ToString();
						objRecordContent.m_strHIDDENBLOODXML = dtbValue.Rows[i]["HIDDENBLOODXML"].ToString();
						objRecordContent.m_strSKIN = dtbValue.Rows[i]["SKIN"].ToString();
						objRecordContent.m_strSKINXML = dtbValue.Rows[i]["SKINXML"].ToString();
						objRecordContent.m_strWASHPERINEUM = dtbValue.Rows[i]["WASHPERINEUM"].ToString();
						objRecordContent.m_strWASHPERINEUMXML = dtbValue.Rows[i]["WASHPERINEUMXML"].ToString();
						objRecordContent.m_strBRUSHBATH = dtbValue.Rows[i]["BRUSHBATH"].ToString();
						objRecordContent.m_strBRUSHBATHXML = dtbValue.Rows[i]["BRUSHBATHXML"].ToString();
						objRecordContent.m_strMOUTHTEND = dtbValue.Rows[i]["MOUTHTEND"].ToString();
						objRecordContent.m_strMOUTHTENDXML = dtbValue.Rows[i]["MOUTHTENDXML"].ToString();
						objRecordContent.m_strIE = dtbValue.Rows[i]["IE"].ToString();
						objRecordContent.m_strIEXML = dtbValue.Rows[i]["IEXML"].ToString();
						objRecordContent.m_strIE_RIGHT = dtbValue.Rows[i]["IE_RIGHT"].ToString();
						objRecordContent.m_strINSPIRATION = dtbValue.Rows[i]["INSPIRATION"].ToString();
						objRecordContent.m_strINSPIRATIONXML = dtbValue.Rows[i]["INSPIRATIONXML"].ToString();
						objRecordContent.m_strINSPIRATION_RIGHT = dtbValue.Rows[i]["INSPIRATION_RIGHT"].ToString();
						
						objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
						objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
						objRecordContent.m_strINFACT1_RIGHT = dtbValue.Rows[i]["INFACT1_RIGHT"].ToString();
						objRecordContent.m_strINFACT2_RIGHT = dtbValue.Rows[i]["INFACT2_RIGHT"].ToString();
						objRecordContent.m_strINFACT3_RIGHT = dtbValue.Rows[i]["INFACT3_RIGHT"].ToString();
						objRecordContent.m_strINFACT4_RIGHT = dtbValue.Rows[i]["INFACT4_RIGHT"].ToString();
						objRecordContent.m_strINFACT5_RIGHT = dtbValue.Rows[i]["INFACT5_RIGHT"].ToString();
						objRecordContent.m_strINBLOOD_RIGHT = dtbValue.Rows[i]["INBLOOD_RIGHT"].ToString();
						objRecordContent.m_strINPERHOUR_RIGHT = dtbValue.Rows[i]["INPERHOUR_RIGHT"].ToString();
						objRecordContent.m_strINSUM_RIGHT = dtbValue.Rows[i]["INSUM_RIGHT"].ToString();
						objRecordContent.m_strOUTSUM_RIGHT = dtbValue.Rows[i]["OUTSUM_RIGHT"].ToString();
						objRecordContent.m_strOUTPERHOUR_RIGHT = dtbValue.Rows[i]["OUTPERHOUR_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTPISSSUM_RIGHT = dtbValue.Rows[i]["OUTFACTPISSSUM_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTPISS_RIGHT = dtbValue.Rows[i]["OUTFACTPISS_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICE_RIGHT = dtbValue.Rows[i]["OUTFACTCHESTJUICE_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICESUM_RIGHT = dtbValue.Rows[i]["OUTFACTCHESTJUICESUM_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTGASTRICJUICE_RIGHT = dtbValue.Rows[i]["OUTFACTGASTRICJUICE_RIGHT"].ToString();
						objRecordContent.m_strEXPANDVASMEDICINE_RIGHT = dtbValue.Rows[i]["EXPANDVASMEDICINE_RIGHT"].ToString();
						objRecordContent.m_strCARDIACDIURESIS_RIGHT = dtbValue.Rows[i]["CARDIACDIURESIS_RIGHT"].ToString();
						objRecordContent.m_strOTHERMEDICINE_RIGHT = dtbValue.Rows[i]["OTHERMEDICINE_RIGHT"].ToString();
						objRecordContent.m_strCONSCIOUSNESS_RIGHT = dtbValue.Rows[i]["CONSCIOUSNESS_RIGHT"].ToString();
						objRecordContent.m_strPUPIL_RIGHT = dtbValue.Rows[i]["PUPIL_RIGHT"].ToString();
						objRecordContent.m_strLEFTPUPIL_RIGHT = dtbValue.Rows[i]["LEFTPUPIL_RIGHT"].ToString();
						objRecordContent.m_strRIGHTPUPIL_RIGHT = dtbValue.Rows[i]["RIGHTPUPIL_RIGHT"].ToString();
						objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[i]["TEMPERATURE_RIGHT"].ToString();
						objRecordContent.m_strTWIGTEMPERATURE_RIGHT = dtbValue.Rows[i]["TWIGTEMPERATURE_RIGHT"].ToString();
						objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[i]["HEARTRATE_RIGHT"].ToString();
						objRecordContent.m_strHEARTRHYTHM_RIGHT = dtbValue.Rows[i]["HEARTRHYTHM_RIGHT"].ToString();
						objRecordContent.m_strBPA_RIGHT = dtbValue.Rows[i]["BPA_RIGHT"].ToString();
						objRecordContent.m_strBPS_RIGHT = dtbValue.Rows[i]["BPS_RIGHT"].ToString();
						objRecordContent.m_strAVGBP_RIGHT = dtbValue.Rows[i]["AVGBP_RIGHT"].ToString();
						objRecordContent.m_strCVP_RIGHT = dtbValue.Rows[i]["CVP_RIGHT"].ToString();
						objRecordContent.m_strLAP_RIGHT = dtbValue.Rows[i]["LAP_RIGHT"].ToString();
                        objRecordContent.m_strSPO_RIGHT = dtbValue.Rows[i]["SPO_RIGHT"].ToString();
						objRecordContent.m_strBREATHMACHINE_RIGHT = dtbValue.Rows[i]["BREATHMACHINE_RIGHT"].ToString();
						objRecordContent.m_strINSERTDEPTH_RIGHT = dtbValue.Rows[i]["INSERTDEPTH_RIGHT"].ToString();
						objRecordContent.m_strASSISTANT_RIGHT = dtbValue.Rows[i]["ASSISTANT_RIGHT"].ToString();
						objRecordContent.m_strFIO2_RIGHT = dtbValue.Rows[i]["FIO2_RIGHT"].ToString();
						objRecordContent.m_strPEEP_RIGHT = dtbValue.Rows[i]["PEEP_RIGHT"].ToString();
						objRecordContent.m_strTV_RIGHT = dtbValue.Rows[i]["TV_RIGHT"].ToString();
						objRecordContent.m_strVF_RIGHT = dtbValue.Rows[i]["VF_RIGHT"].ToString();
						objRecordContent.m_strBREATHTIMES_RIGHT = dtbValue.Rows[i]["BREATHTIMES_RIGHT"].ToString();
						objRecordContent.m_strLEFTBREATHVOICE_RIGHT = dtbValue.Rows[i]["LEFTBREATHVOICE_RIGHT"].ToString();
						objRecordContent.m_strRIGHTBREATHVOICE_RIGHT = dtbValue.Rows[i]["RIGHTBREATHVOICE_RIGHT"].ToString();
						objRecordContent.m_strPHLEGMCOLOR_RIGHT = dtbValue.Rows[i]["PHLEGMCOLOR_RIGHT"].ToString();
						objRecordContent.m_strPHLEGMQUANTITY_RIGHT = dtbValue.Rows[i]["PHLEGMQUANTITY_RIGHT"].ToString();
						objRecordContent.m_strGESTICULATION_RIGHT = dtbValue.Rows[i]["GESTICULATION_RIGHT"].ToString();
						objRecordContent.m_strPHYSICALTHERAPY_RIGHT = dtbValue.Rows[i]["PHYSICALTHERAPY_RIGHT"].ToString();
						objRecordContent.m_strREMARK_RIGHT = dtbValue.Rows[i]["REMARK_RIGHT"].ToString();
						objRecordContent.m_strWBC_RIGHT = dtbValue.Rows[i]["WBC_RIGHT"].ToString();
						objRecordContent.m_strHB_RIGHT = dtbValue.Rows[i]["HB_RIGHT"].ToString();
						objRecordContent.m_strRBC_RIGHT = dtbValue.Rows[i]["RBC_RIGHT"].ToString();
						objRecordContent.m_strHCT_RIGHT = dtbValue.Rows[i]["HCT_RIGHT"].ToString();
						objRecordContent.m_strPLT_RIGHT = dtbValue.Rows[i]["PLT_RIGHT"].ToString();
						objRecordContent.m_strPH_RIGHT = dtbValue.Rows[i]["PH_RIGHT"].ToString();
						objRecordContent.m_strPCO2_RIGHT = dtbValue.Rows[i]["PCO2_RIGHT"].ToString();
						objRecordContent.m_strPAO2_RIGHT = dtbValue.Rows[i]["PAO2_RIGHT"].ToString();
						objRecordContent.m_strHCO3_RIGHT = dtbValue.Rows[i]["HCO3_RIGHT"].ToString();
						objRecordContent.m_strBE_RIGHT = dtbValue.Rows[i]["BE_RIGHT"].ToString();
						objRecordContent.m_strKPLUS_RIGHT = dtbValue.Rows[i]["KPLUS_RIGHT"].ToString();
						objRecordContent.m_strNAPLUS_RIGHT = dtbValue.Rows[i]["NAPLUS_RIGHT"].ToString();
						objRecordContent.m_strCISUB_RIGHT = dtbValue.Rows[i]["CISUB_RIGHT"].ToString();
						objRecordContent.m_strCAPLUSPLUS_RIGHT = dtbValue.Rows[i]["CAPLUSPLUS_RIGHT"].ToString();
						objRecordContent.m_strGLU_RIGHT = dtbValue.Rows[i]["GLU_RIGHT"].ToString();
						objRecordContent.m_strBUN_RIGHT = dtbValue.Rows[i]["BUN_RIGHT"].ToString();
						objRecordContent.m_strUA_RIGHT = dtbValue.Rows[i]["UA_RIGHT"].ToString();
						objRecordContent.m_strANHYDRIDE_RIGHT = dtbValue.Rows[i]["ANHYDRIDE_RIGHT"].ToString();
						objRecordContent.m_strCO2CP_RIGHT = dtbValue.Rows[i]["CO2CP_RIGHT"].ToString();
						objRecordContent.m_strPT_RIGHT = dtbValue.Rows[i]["PT_RIGHT"].ToString();
						objRecordContent.m_strXRAYCHECK_RIGHT = dtbValue.Rows[i]["XRAYCHECK_RIGHT"].ToString();
						objRecordContent.m_strACT_RIGHT = dtbValue.Rows[i]["ACT_RIGHT"].ToString();
						objRecordContent.m_strPROPORTION_RIGHT = dtbValue.Rows[i]["PROPORTION_RIGHT"].ToString();
						objRecordContent.m_strALBUMEN_RIGHT = dtbValue.Rows[i]["ALBUMEN_RIGHT"].ToString();
						objRecordContent.m_strHIDDENBLOOD_RIGHT = dtbValue.Rows[i]["HIDDENBLOOD_RIGHT"].ToString();
						objRecordContent.m_strSKIN_RIGHT = dtbValue.Rows[i]["SKIN_RIGHT"].ToString();
						objRecordContent.m_strWASHPERINEUM_RIGHT = dtbValue.Rows[i]["WASHPERINEUM_RIGHT"].ToString();
						objRecordContent.m_strBRUSHBATH_RIGHT = dtbValue.Rows[i]["BRUSHBATH_RIGHT"].ToString();
						objRecordContent.m_strMOUTHTEND_RIGHT = dtbValue.Rows[i]["MOUTHTEND_RIGHT"].ToString();
						
						p_objTansDataInfo[i] = objRecordContent;
						#endregion
					}
				}		
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
			clsCardiovascularTend_GX[] p_objCardiovascularTendArr = null;
			clsCardiovascularBaseInfo_GX[] p_objBaseInfoArr = null;
			p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsCardiovascularTend_GXDataInfo objDataInfo = new clsCardiovascularTend_GXDataInfo();
			
			try
			{
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);	

				DataTable dtbContent = new DataTable();//特护记录内容  
				DataTable dtbBaseInfo = new DataTable();//公共信息

				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
				if(lngRes > 0 && dtbContent.Rows.Count > 0)
				{
					clsCardiovascularTend_GX objRecordContent = null;
					p_objCardiovascularTendArr = new clsCardiovascularTend_GX[dtbContent.Rows.Count];
					for(int i=0; i<dtbContent.Rows.Count; i++)
					{
						#region 特护记录
						objRecordContent = new clsCardiovascularTend_GX();
						objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbContent.Rows[i]["CREATEDATE"].ToString());
						objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbContent.Rows[i]["MODIFYDATE"].ToString());
						objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

						if(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString()=="")
							objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
						else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString());
						objRecordContent.m_strCreateUserID=dtbContent.Rows[i]["CREATEUSERID"].ToString();
						objRecordContent.m_strModifyUserID=dtbContent.Rows[i]["MODIFYUSERID"].ToString();
						if(dtbContent.Rows[i]["IFCONFIRM"].ToString()=="")
							objRecordContent.m_bytIfConfirm=0;
						else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbContent.Rows[i]["IFCONFIRM"].ToString());
						if(dtbContent.Rows[i]["STATUS"].ToString()=="")
							objRecordContent.m_bytStatus=0;
						else objRecordContent.m_bytStatus=Byte.Parse(dtbContent.Rows[i]["STATUS"].ToString());

						objRecordContent.m_strConfirmReason=dtbContent.Rows[i]["CONFIRMREASON"].ToString();
						objRecordContent.m_strConfirmReasonXML=dtbContent.Rows[i]["CONFIRMREASONXML"].ToString();

						objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbContent .Rows[i]["RECORDDATE"]);

						objRecordContent.m_strINFACT1 = dtbContent .Rows[i]["INFACT1"].ToString();
						objRecordContent.m_strINFACT1XML = dtbContent .Rows[i]["INFACT1XML"].ToString();
						objRecordContent.m_strINFACT2 = dtbContent .Rows[i]["INFACT2"].ToString();
						objRecordContent.m_strINFACT2XML = dtbContent .Rows[i]["INFACT2XML"].ToString();
						objRecordContent.m_strINFACT3 = dtbContent .Rows[i]["INFACT3"].ToString();
						objRecordContent.m_strINFACT3XML = dtbContent .Rows[i]["INFACT3XML"].ToString();
						objRecordContent.m_strINFACT4 = dtbContent .Rows[i]["INFACT4"].ToString();
						objRecordContent.m_strINFACT4XML = dtbContent .Rows[i]["INFACT4XML"].ToString();
						objRecordContent.m_strINFACT5 = dtbContent .Rows[i]["INFACT5"].ToString();
						objRecordContent.m_strINFACT5XML = dtbContent .Rows[i]["INFACT5XML"].ToString();
						objRecordContent.m_strINBLOOD = dtbContent .Rows[i]["INBLOOD"].ToString();
						objRecordContent.m_strINBLOODXML = dtbContent .Rows[i]["INBLOODXML"].ToString();
						objRecordContent.m_strINPERHOUR = dtbContent .Rows[i]["INPERHOUR"].ToString();
						objRecordContent.m_strINPERHOURXML = dtbContent .Rows[i]["INPERHOURXML"].ToString();
						objRecordContent.m_strINSUM = dtbContent .Rows[i]["INSUM"].ToString();
						objRecordContent.m_strINSUMXML = dtbContent .Rows[i]["INSUMXML"].ToString();
						objRecordContent.m_strOUTSUM = dtbContent .Rows[i]["OUTSUM"].ToString();
						objRecordContent.m_strOUTSUMXML = dtbContent .Rows[i]["OUTSUMXML"].ToString();
						objRecordContent.m_strOUTPERHOUR = dtbContent .Rows[i]["OUTPERHOUR"].ToString();
						objRecordContent.m_strOUTPERHOURXML = dtbContent .Rows[i]["OUTPERHOURXML"].ToString();
						objRecordContent.m_strOUTFACTPISSSUM = dtbContent .Rows[i]["OUTFACTPISSSUM"].ToString();
						objRecordContent.m_strOUTFACTPISSSUMXML = dtbContent .Rows[i]["OUTFACTPISSSUMXML"].ToString();
						objRecordContent.m_strOUTFACTPISS = dtbContent .Rows[i]["OUTFACTPISS"].ToString();
						objRecordContent.m_strOUTFACTPISSXML = dtbContent .Rows[i]["OUTFACTPISSXML"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICE = dtbContent .Rows[i]["OUTFACTCHESTJUICE"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICEXML = dtbContent .Rows[i]["OUTFACTCHESTJUICEXML"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICESUM = dtbContent .Rows[i]["OUTFACTCHESTJUICESUM"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICESUMXML = dtbContent .Rows[i]["OUTFACTCHESTJUICESUMXML"].ToString();
						objRecordContent.m_strOUTFACTGASTRICJUICE = dtbContent .Rows[i]["OUTFACTGASTRICJUICE"].ToString();
						objRecordContent.m_strOUTFACTGASTRICJUICEXML = dtbContent .Rows[i]["OUTFACTGASTRICJUICEXML"].ToString();
						objRecordContent.m_strEXPANDVASMEDICINE = dtbContent .Rows[i]["EXPANDVASMEDICINE"].ToString();
						objRecordContent.m_strCARDIACDIURESIS = dtbContent .Rows[i]["CARDIACDIURESIS"].ToString();
						objRecordContent.m_strOTHERMEDICINE = dtbContent .Rows[i]["OTHERMEDICINE"].ToString();
						objRecordContent.m_strCONSCIOUSNESS = dtbContent .Rows[i]["CONSCIOUSNESS"].ToString();
						objRecordContent.m_strCONSCIOUSNESSXML = dtbContent .Rows[i]["CONSCIOUSNESSXML"].ToString();
						objRecordContent.m_strPUPIL = dtbContent .Rows[i]["PUPIL"].ToString();
						objRecordContent.m_strPUPILXML = dtbContent .Rows[i]["PUPILXML"].ToString();
						objRecordContent.m_strLEFTPUPIL = dtbContent .Rows[i]["LEFTPUPIL"].ToString();
						objRecordContent.m_strLEFTPUPILXML = dtbContent .Rows[i]["LEFTPUPILXML"].ToString();
						objRecordContent.m_strRIGHTPUPIL = dtbContent .Rows[i]["RIGHTPUPIL"].ToString();
						objRecordContent.m_strRIGHTPUPILXML = dtbContent .Rows[i]["RIGHTPUPILXML"].ToString();
						objRecordContent.m_strTEMPERATURE = dtbContent .Rows[i]["TEMPERATURE"].ToString();
						objRecordContent.m_strTEMPERATUREXML = dtbContent .Rows[i]["TEMPERATUREXML"].ToString();
						objRecordContent.m_strTWIGTEMPERATURE = dtbContent .Rows[i]["TWIGTEMPERATURE"].ToString();
						objRecordContent.m_strTWIGTEMPERATUREXML = dtbContent .Rows[i]["TWIGTEMPERATUREXML"].ToString();
						objRecordContent.m_strHEARTRATE = dtbContent .Rows[i]["HEARTRATE"].ToString();
						objRecordContent.m_strHEARTRATEXML = dtbContent .Rows[i]["HEARTRATEXML"].ToString();
						objRecordContent.m_strHEARTRHYTHM = dtbContent .Rows[i]["HEARTRHYTHM"].ToString();
						objRecordContent.m_strHEARTRHYTHMXML = dtbContent .Rows[i]["HEARTRHYTHMXML"].ToString();
						objRecordContent.m_strBPA = dtbContent .Rows[i]["BPA"].ToString();
						objRecordContent.m_strBPAXML = dtbContent .Rows[i]["BPAXML"].ToString();
						objRecordContent.m_strBPS = dtbContent .Rows[i]["BPS"].ToString();
						objRecordContent.m_strBPSXML = dtbContent .Rows[i]["BPSXML"].ToString();
						objRecordContent.m_strAVGBP = dtbContent .Rows[i]["AVGBP"].ToString();
						objRecordContent.m_strAVGBPXML = dtbContent .Rows[i]["AVGBPXML"].ToString();
						objRecordContent.m_strCVP = dtbContent .Rows[i]["CVP"].ToString();
						objRecordContent.m_strCVPXML = dtbContent .Rows[i]["CVPXML"].ToString();
						objRecordContent.m_strLAP = dtbContent .Rows[i]["LAP"].ToString();
						objRecordContent.m_strLAPXML = dtbContent .Rows[i]["LAPXML"].ToString();

                        objRecordContent.m_strSPO = dtbContent.Rows[i]["SPO"].ToString();
                        objRecordContent.m_strSPOXML = dtbContent.Rows[i]["SPOXML"].ToString();

						objRecordContent.m_strBREATHMACHINE = dtbContent .Rows[i]["BREATHMACHINE"].ToString();
						objRecordContent.m_strBREATHMACHINEXML = dtbContent .Rows[i]["BREATHMACHINEXML"].ToString();
						objRecordContent.m_strINSERTDEPTH = dtbContent .Rows[i]["INSERTDEPTH"].ToString();
						objRecordContent.m_strINSERTDEPTHXML = dtbContent .Rows[i]["INSERTDEPTHXML"].ToString();
						objRecordContent.m_strASSISTANT = dtbContent .Rows[i]["ASSISTANT"].ToString();
						objRecordContent.m_strASSISTANTXML = dtbContent .Rows[i]["ASSISTANTXML"].ToString();
						objRecordContent.m_strFIO2 = dtbContent .Rows[i]["FIO2"].ToString();
						objRecordContent.m_strFIO2XML = dtbContent .Rows[i]["FIO2XML"].ToString();
						objRecordContent.m_strPEEP = dtbContent .Rows[i]["PEEP"].ToString();
						objRecordContent.m_strPEEPXML = dtbContent .Rows[i]["PEEPXML"].ToString();
						objRecordContent.m_strTV = dtbContent .Rows[i]["TV"].ToString();
						objRecordContent.m_strTVXML = dtbContent .Rows[i]["TVXML"].ToString();
						objRecordContent.m_strVF = dtbContent .Rows[i]["VF"].ToString();
						objRecordContent.m_strVFXML = dtbContent .Rows[i]["VFXML"].ToString();
						objRecordContent.m_strBREATHTIMES = dtbContent .Rows[i]["BREATHTIMES"].ToString();
						objRecordContent.m_strBREATHTIMESXML = dtbContent .Rows[i]["BREATHTIMESXML"].ToString();
						objRecordContent.m_strLEFTBREATHVOICE = dtbContent .Rows[i]["LEFTBREATHVOICE"].ToString();
						objRecordContent.m_strLEFTBREATHVOICEXML = dtbContent .Rows[i]["LEFTBREATHVOICEXML"].ToString();
						objRecordContent.m_strRIGHTBREATHVOICE = dtbContent .Rows[i]["RIGHTBREATHVOICE"].ToString();
						objRecordContent.m_strRIGHTBREATHVOICEXML = dtbContent .Rows[i]["RIGHTBREATHVOICEXML"].ToString();
						objRecordContent.m_strPHLEGMCOLOR = dtbContent .Rows[i]["PHLEGMCOLOR"].ToString();
						objRecordContent.m_strPHLEGMCOLORXML = dtbContent .Rows[i]["PHLEGMCOLORXML"].ToString();
						objRecordContent.m_strPHLEGMQUANTITY = dtbContent .Rows[i]["PHLEGMQUANTITY"].ToString();
						objRecordContent.m_strPHLEGMQUANTITYXML = dtbContent .Rows[i]["PHLEGMQUANTITYXML"].ToString();
						objRecordContent.m_strGESTICULATION = dtbContent .Rows[i]["GESTICULATION"].ToString();
						objRecordContent.m_strGESTICULATIONXML = dtbContent .Rows[i]["GESTICULATIONXML"].ToString();
						objRecordContent.m_strPHYSICALTHERAPY = dtbContent .Rows[i]["PHYSICALTHERAPY"].ToString();
						objRecordContent.m_strPHYSICALTHERAPYXML = dtbContent .Rows[i]["PHYSICALTHERAPYXML"].ToString();
						objRecordContent.m_strREMARK = dtbContent .Rows[i]["REMARK"].ToString();
						objRecordContent.m_strREMARKXML = dtbContent .Rows[i]["REMARKXML"].ToString();
						objRecordContent.m_strWBC = dtbContent .Rows[i]["WBC"].ToString();
						objRecordContent.m_strWBCXML = dtbContent .Rows[i]["WBCXML"].ToString();
						objRecordContent.m_strHB = dtbContent .Rows[i]["HB"].ToString();
						objRecordContent.m_strHBXML = dtbContent .Rows[i]["HBXML"].ToString();
						objRecordContent.m_strRBC = dtbContent .Rows[i]["RBC"].ToString();
						objRecordContent.m_strRBCXML = dtbContent .Rows[i]["RBCXML"].ToString();
						objRecordContent.m_strHCT = dtbContent .Rows[i]["HCT"].ToString();
						objRecordContent.m_strHCTXML = dtbContent .Rows[i]["HCTXML"].ToString();
						objRecordContent.m_strPLT = dtbContent .Rows[i]["PLT"].ToString();
						objRecordContent.m_strPLTXML = dtbContent .Rows[i]["PLTXML"].ToString();
						objRecordContent.m_strPH = dtbContent .Rows[i]["PH"].ToString();
						objRecordContent.m_strPHXML = dtbContent .Rows[i]["PHXML"].ToString();
						objRecordContent.m_strPCO2 = dtbContent .Rows[i]["PCO2"].ToString();
						objRecordContent.m_strPCO2XML = dtbContent .Rows[i]["PCO2XML"].ToString();
						objRecordContent.m_strPAO2 = dtbContent .Rows[i]["PAO2"].ToString();
						objRecordContent.m_strPAO2XML = dtbContent .Rows[i]["PAO2XML"].ToString();
						objRecordContent.m_strHCO3 = dtbContent .Rows[i]["HCO3"].ToString();
						objRecordContent.m_strHCO3XML = dtbContent .Rows[i]["HCO3XML"].ToString();
						objRecordContent.m_strBE = dtbContent .Rows[i]["BE"].ToString();
						objRecordContent.m_strBEXML = dtbContent .Rows[i]["BEXML"].ToString();
						objRecordContent.m_strKPLUS = dtbContent .Rows[i]["KPLUS"].ToString();
						objRecordContent.m_strKPLUSXML = dtbContent .Rows[i]["KPLUSXML"].ToString();
						objRecordContent.m_strNAPLUS = dtbContent .Rows[i]["NAPLUS"].ToString();
						objRecordContent.m_strNAPLUSXML = dtbContent .Rows[i]["NAPLUSXML"].ToString();
						objRecordContent.m_strCISUB = dtbContent .Rows[i]["CISUB"].ToString();
						objRecordContent.m_strCISUBXML = dtbContent .Rows[i]["CISUBXML"].ToString();
						objRecordContent.m_strCAPLUSPLUS = dtbContent .Rows[i]["CAPLUSPLUS"].ToString();
						objRecordContent.m_strCAPLUSPLUSXML = dtbContent .Rows[i]["CAPLUSPLUSXML"].ToString();
						objRecordContent.m_strGLU = dtbContent .Rows[i]["GLU"].ToString();
						objRecordContent.m_strGLUXML = dtbContent .Rows[i]["GLUXML"].ToString();
						objRecordContent.m_strBUN = dtbContent .Rows[i]["BUN"].ToString();
						objRecordContent.m_strBUNXML = dtbContent .Rows[i]["BUNXML"].ToString();
						objRecordContent.m_strUA = dtbContent .Rows[i]["UA"].ToString();
						objRecordContent.m_strUAXML = dtbContent .Rows[i]["UAXML"].ToString();
						objRecordContent.m_strANHYDRIDE = dtbContent .Rows[i]["ANHYDRIDE"].ToString();
						objRecordContent.m_strANHYDRIDEXML = dtbContent .Rows[i]["ANHYDRIDEXML"].ToString();
						objRecordContent.m_strCO2CP = dtbContent .Rows[i]["CO2CP"].ToString();
						objRecordContent.m_strCO2CPXML = dtbContent .Rows[i]["CO2CPXML"].ToString();
						objRecordContent.m_strPT = dtbContent .Rows[i]["PT"].ToString();
						objRecordContent.m_strPTXML = dtbContent .Rows[i]["PTXML"].ToString();
						objRecordContent.m_strXRAYCHECK = dtbContent .Rows[i]["XRAYCHECK"].ToString();
						objRecordContent.m_strXRAYCHECKXML = dtbContent .Rows[i]["XRAYCHECKXML"].ToString();
						objRecordContent.m_strACT = dtbContent .Rows[i]["ACT"].ToString();
						objRecordContent.m_strACTXML = dtbContent .Rows[i]["ACTXML"].ToString();
						objRecordContent.m_strPROPORTION = dtbContent .Rows[i]["PROPORTION"].ToString();
						objRecordContent.m_strPROPORTIONXML = dtbContent .Rows[i]["PROPORTIONXML"].ToString();
						objRecordContent.m_strALBUMEN = dtbContent .Rows[i]["ALBUMEN"].ToString();
						objRecordContent.m_strALBUMENXML = dtbContent .Rows[i]["ALBUMENXML"].ToString();
						objRecordContent.m_strHIDDENBLOOD = dtbContent .Rows[i]["HIDDENBLOOD"].ToString();
						objRecordContent.m_strHIDDENBLOODXML = dtbContent .Rows[i]["HIDDENBLOODXML"].ToString();
						objRecordContent.m_strSKIN = dtbContent .Rows[i]["SKIN"].ToString();
						objRecordContent.m_strSKINXML = dtbContent .Rows[i]["SKINXML"].ToString();
						objRecordContent.m_strWASHPERINEUM = dtbContent .Rows[i]["WASHPERINEUM"].ToString();
						objRecordContent.m_strWASHPERINEUMXML = dtbContent .Rows[i]["WASHPERINEUMXML"].ToString();
						objRecordContent.m_strBRUSHBATH = dtbContent .Rows[i]["BRUSHBATH"].ToString();
						objRecordContent.m_strBRUSHBATHXML = dtbContent .Rows[i]["BRUSHBATHXML"].ToString();
						objRecordContent.m_strMOUTHTEND = dtbContent .Rows[i]["MOUTHTEND"].ToString();
						objRecordContent.m_strMOUTHTENDXML = dtbContent .Rows[i]["MOUTHTENDXML"].ToString();
						objRecordContent.m_strIE = dtbContent.Rows[i]["IE"].ToString();
						objRecordContent.m_strIEXML = dtbContent.Rows[i]["IEXML"].ToString();
						objRecordContent.m_strIE_RIGHT = dtbContent.Rows[i]["IE_RIGHT"].ToString();
						objRecordContent.m_strINSPIRATION = dtbContent.Rows[i]["INSPIRATION"].ToString();
						objRecordContent.m_strINSPIRATIONXML = dtbContent.Rows[i]["INSPIRATIONXML"].ToString();
						objRecordContent.m_strINSPIRATION_RIGHT = dtbContent.Rows[i]["INSPIRATION_RIGHT"].ToString();
							
						objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbContent .Rows[i]["MODIFYDATE"]);
						objRecordContent.m_strModifyUserID = dtbContent .Rows[i]["MODIFYUSERID"].ToString();
						objRecordContent.m_strINFACT1_RIGHT = dtbContent .Rows[i]["INFACT1_RIGHT"].ToString();
						objRecordContent.m_strINFACT2_RIGHT = dtbContent .Rows[i]["INFACT2_RIGHT"].ToString();
						objRecordContent.m_strINFACT3_RIGHT = dtbContent .Rows[i]["INFACT3_RIGHT"].ToString();
						objRecordContent.m_strINFACT4_RIGHT = dtbContent .Rows[i]["INFACT4_RIGHT"].ToString();
						objRecordContent.m_strINFACT5_RIGHT = dtbContent .Rows[i]["INFACT5_RIGHT"].ToString();
						objRecordContent.m_strINBLOOD_RIGHT = dtbContent .Rows[i]["INBLOOD_RIGHT"].ToString();
						objRecordContent.m_strINPERHOUR_RIGHT = dtbContent .Rows[i]["INPERHOUR_RIGHT"].ToString();
						objRecordContent.m_strINSUM_RIGHT = dtbContent .Rows[i]["INSUM_RIGHT"].ToString();
						objRecordContent.m_strOUTSUM_RIGHT = dtbContent .Rows[i]["OUTSUM_RIGHT"].ToString();
						objRecordContent.m_strOUTPERHOUR_RIGHT = dtbContent .Rows[i]["OUTPERHOUR_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTPISSSUM_RIGHT = dtbContent .Rows[i]["OUTFACTPISSSUM_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTPISS_RIGHT = dtbContent .Rows[i]["OUTFACTPISS_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICE_RIGHT = dtbContent .Rows[i]["OUTFACTCHESTJUICE_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTCHESTJUICESUM_RIGHT = dtbContent .Rows[i]["OUTFACTCHESTJUICESUM_RIGHT"].ToString();
						objRecordContent.m_strOUTFACTGASTRICJUICE_RIGHT = dtbContent .Rows[i]["OUTFACTGASTRICJUICE_RIGHT"].ToString();
						objRecordContent.m_strEXPANDVASMEDICINE_RIGHT = dtbContent .Rows[i]["EXPANDVASMEDICINE_RIGHT"].ToString();
						objRecordContent.m_strCARDIACDIURESIS_RIGHT = dtbContent .Rows[i]["CARDIACDIURESIS_RIGHT"].ToString();
						objRecordContent.m_strOTHERMEDICINE_RIGHT = dtbContent .Rows[i]["OTHERMEDICINE_RIGHT"].ToString();
						objRecordContent.m_strCONSCIOUSNESS_RIGHT = dtbContent .Rows[i]["CONSCIOUSNESS_RIGHT"].ToString();
						objRecordContent.m_strPUPIL_RIGHT = dtbContent .Rows[i]["PUPIL_RIGHT"].ToString();
						objRecordContent.m_strLEFTPUPIL_RIGHT = dtbContent .Rows[i]["LEFTPUPIL_RIGHT"].ToString();
						objRecordContent.m_strRIGHTPUPIL_RIGHT = dtbContent .Rows[i]["RIGHTPUPIL_RIGHT"].ToString();
						objRecordContent.m_strTEMPERATURE_RIGHT = dtbContent .Rows[i]["TEMPERATURE_RIGHT"].ToString();
						objRecordContent.m_strTWIGTEMPERATURE_RIGHT = dtbContent .Rows[i]["TWIGTEMPERATURE_RIGHT"].ToString();
						objRecordContent.m_strHEARTRATE_RIGHT = dtbContent .Rows[i]["HEARTRATE_RIGHT"].ToString();
						objRecordContent.m_strHEARTRHYTHM_RIGHT = dtbContent .Rows[i]["HEARTRHYTHM_RIGHT"].ToString();
						objRecordContent.m_strBPA_RIGHT = dtbContent .Rows[i]["BPA_RIGHT"].ToString();
						objRecordContent.m_strBPS_RIGHT = dtbContent .Rows[i]["BPS_RIGHT"].ToString();
						objRecordContent.m_strAVGBP_RIGHT = dtbContent .Rows[i]["AVGBP_RIGHT"].ToString();
						objRecordContent.m_strCVP_RIGHT = dtbContent .Rows[i]["CVP_RIGHT"].ToString();
						objRecordContent.m_strLAP_RIGHT = dtbContent .Rows[i]["LAP_RIGHT"].ToString();
                        objRecordContent.m_strSPO_RIGHT = dtbContent.Rows[i]["SPO_RIGHT"].ToString();
						objRecordContent.m_strBREATHMACHINE_RIGHT = dtbContent .Rows[i]["BREATHMACHINE_RIGHT"].ToString();
						objRecordContent.m_strINSERTDEPTH_RIGHT = dtbContent .Rows[i]["INSERTDEPTH_RIGHT"].ToString();
						objRecordContent.m_strASSISTANT_RIGHT = dtbContent .Rows[i]["ASSISTANT_RIGHT"].ToString();
						objRecordContent.m_strFIO2_RIGHT = dtbContent .Rows[i]["FIO2_RIGHT"].ToString();
						objRecordContent.m_strPEEP_RIGHT = dtbContent .Rows[i]["PEEP_RIGHT"].ToString();
						objRecordContent.m_strTV_RIGHT = dtbContent .Rows[i]["TV_RIGHT"].ToString();
						objRecordContent.m_strVF_RIGHT = dtbContent .Rows[i]["VF_RIGHT"].ToString();
						objRecordContent.m_strBREATHTIMES_RIGHT = dtbContent .Rows[i]["BREATHTIMES_RIGHT"].ToString();
						objRecordContent.m_strLEFTBREATHVOICE_RIGHT = dtbContent .Rows[i]["LEFTBREATHVOICE_RIGHT"].ToString();
						objRecordContent.m_strRIGHTBREATHVOICE_RIGHT = dtbContent .Rows[i]["RIGHTBREATHVOICE_RIGHT"].ToString();
						objRecordContent.m_strPHLEGMCOLOR_RIGHT = dtbContent .Rows[i]["PHLEGMCOLOR_RIGHT"].ToString();
						objRecordContent.m_strPHLEGMQUANTITY_RIGHT = dtbContent .Rows[i]["PHLEGMQUANTITY_RIGHT"].ToString();
						objRecordContent.m_strGESTICULATION_RIGHT = dtbContent .Rows[i]["GESTICULATION_RIGHT"].ToString();
						objRecordContent.m_strPHYSICALTHERAPY_RIGHT = dtbContent .Rows[i]["PHYSICALTHERAPY_RIGHT"].ToString();
						objRecordContent.m_strREMARK_RIGHT = dtbContent .Rows[i]["REMARK_RIGHT"].ToString();
						objRecordContent.m_strWBC_RIGHT = dtbContent .Rows[i]["WBC_RIGHT"].ToString();
						objRecordContent.m_strHB_RIGHT = dtbContent .Rows[i]["HB_RIGHT"].ToString();
						objRecordContent.m_strRBC_RIGHT = dtbContent .Rows[i]["RBC_RIGHT"].ToString();
						objRecordContent.m_strHCT_RIGHT = dtbContent .Rows[i]["HCT_RIGHT"].ToString();
						objRecordContent.m_strPLT_RIGHT = dtbContent .Rows[i]["PLT_RIGHT"].ToString();
						objRecordContent.m_strPH_RIGHT = dtbContent .Rows[i]["PH_RIGHT"].ToString();
						objRecordContent.m_strPCO2_RIGHT = dtbContent .Rows[i]["PCO2_RIGHT"].ToString();
						objRecordContent.m_strPAO2_RIGHT = dtbContent .Rows[i]["PAO2_RIGHT"].ToString();
						objRecordContent.m_strHCO3_RIGHT = dtbContent .Rows[i]["HCO3_RIGHT"].ToString();
						objRecordContent.m_strBE_RIGHT = dtbContent .Rows[i]["BE_RIGHT"].ToString();
						objRecordContent.m_strKPLUS_RIGHT = dtbContent .Rows[i]["KPLUS_RIGHT"].ToString();
						objRecordContent.m_strNAPLUS_RIGHT = dtbContent .Rows[i]["NAPLUS_RIGHT"].ToString();
						objRecordContent.m_strCISUB_RIGHT = dtbContent .Rows[i]["CISUB_RIGHT"].ToString();
						objRecordContent.m_strCAPLUSPLUS_RIGHT = dtbContent .Rows[i]["CAPLUSPLUS_RIGHT"].ToString();
						objRecordContent.m_strGLU_RIGHT = dtbContent .Rows[i]["GLU_RIGHT"].ToString();
						objRecordContent.m_strBUN_RIGHT = dtbContent .Rows[i]["BUN_RIGHT"].ToString();
						objRecordContent.m_strUA_RIGHT = dtbContent .Rows[i]["UA_RIGHT"].ToString();
						objRecordContent.m_strANHYDRIDE_RIGHT = dtbContent .Rows[i]["ANHYDRIDE_RIGHT"].ToString();
						objRecordContent.m_strCO2CP_RIGHT = dtbContent .Rows[i]["CO2CP_RIGHT"].ToString();
						objRecordContent.m_strPT_RIGHT = dtbContent .Rows[i]["PT_RIGHT"].ToString();
						objRecordContent.m_strXRAYCHECK_RIGHT = dtbContent .Rows[i]["XRAYCHECK_RIGHT"].ToString();
						objRecordContent.m_strACT_RIGHT = dtbContent .Rows[i]["ACT_RIGHT"].ToString();
						objRecordContent.m_strPROPORTION_RIGHT = dtbContent .Rows[i]["PROPORTION_RIGHT"].ToString();
						objRecordContent.m_strALBUMEN_RIGHT = dtbContent .Rows[i]["ALBUMEN_RIGHT"].ToString();
						objRecordContent.m_strHIDDENBLOOD_RIGHT = dtbContent .Rows[i]["HIDDENBLOOD_RIGHT"].ToString();
						objRecordContent.m_strSKIN_RIGHT = dtbContent .Rows[i]["SKIN_RIGHT"].ToString();
						objRecordContent.m_strWASHPERINEUM_RIGHT = dtbContent .Rows[i]["WASHPERINEUM_RIGHT"].ToString();
						objRecordContent.m_strBRUSHBATH_RIGHT = dtbContent .Rows[i]["BRUSHBATH_RIGHT"].ToString();
						objRecordContent.m_strMOUTHTEND_RIGHT = dtbContent .Rows[i]["MOUTHTEND_RIGHT"].ToString();
						#endregion

						p_objCardiovascularTendArr[i] = objRecordContent;
					}
					objDataInfo.m_objRecordContent = p_objCardiovascularTendArr[p_objCardiovascularTendArr.Length-1];
				}
				objDataInfo.m_objRecordArr = p_objCardiovascularTendArr;

				IDataParameter[] objDPArr1 = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID.Trim();
                objDPArr1[1].DbType = DbType.DateTime;
				objDPArr1[1].Value=DateTime.Parse(p_strInPatientDate);

				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetBaseInfoSQL, ref dtbBaseInfo, objDPArr1);

				if(lngRes > 0 && dtbBaseInfo.Rows.Count > 0)
				{
					clsCardiovascularBaseInfo_GX objDetail = null;
					p_objBaseInfoArr = new clsCardiovascularBaseInfo_GX[dtbBaseInfo.Rows.Count];
					for(int j=0; j<dtbBaseInfo.Rows.Count; j++)
					{
						objDetail = new clsCardiovascularBaseInfo_GX();
						objDetail.m_strInPatientID = p_strInPatientID;
						objDetail.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
						objDetail.m_dtmOpenDate = Convert.ToDateTime(dtbBaseInfo.Rows[j]["OPENDATE"]);
						objDetail.m_dtmCreateDate = Convert.ToDateTime(dtbBaseInfo.Rows[j]["CREATEDATE"]);
						objDetail.m_strCreateUserID = dtbBaseInfo.Rows[j]["CREATEUSERID"].ToString();
						objDetail.m_dtmModifyDate = Convert.ToDateTime(dtbBaseInfo.Rows[j]["MODIFYDATE"]);
						objDetail.m_strModifyUserID = dtbBaseInfo.Rows[j]["MODIFYUSERID"].ToString();
						objDetail.m_strAFTEROPDAYS = dtbBaseInfo.Rows[j]["AFTEROPDAYS"].ToString();
						objDetail.m_strOPNAME = dtbBaseInfo.Rows[j]["OPNAME"].ToString();
						objDetail.m_strOPMEDICINE1 = dtbBaseInfo.Rows[j]["OPMEDICINE1"].ToString();
						objDetail.m_strOPMEDICINE2 = dtbBaseInfo.Rows[j]["OPMEDICINE2"].ToString();
						objDetail.m_strOPMEDICINE3 = dtbBaseInfo.Rows[j]["OPMEDICINE3"].ToString();
						objDetail.m_strOPMEDICINE4 = dtbBaseInfo.Rows[j]["OPMEDICINE4"].ToString();
						objDetail.m_strOPMEDICINE5 = dtbBaseInfo.Rows[j]["OPMEDICINE5"].ToString();
						objDetail.m_strLONGCLASSSIGNID = dtbBaseInfo.Rows[j]["LONGCLASSSIGNID"].ToString();
						objDetail.m_strOFFICESIGNID = dtbBaseInfo.Rows[j]["OFFICESIGNID"].ToString();
						objDetail.m_strSMALLNIGHTCLASSSIGNID = dtbBaseInfo.Rows[j]["SMALLNIGHTCLASSSIGNID"].ToString();
						objDetail.m_strBIGNIGHTCLASSSIGNID = dtbBaseInfo.Rows[j]["BIGNIGHTCLASSSIGNID"].ToString();
						objDetail.m_dtmRECORDDATE = Convert.ToDateTime(dtbBaseInfo.Rows[j]["RECORDDATE"]);

						p_objBaseInfoArr[j] = objDetail;
					}
					objDataInfo.m_objBaseInfoArr = p_objBaseInfoArr;
				}
				if(objDataInfo.m_objRecordArr == null && objDataInfo.m_objBaseInfoArr != null)
				{
					objDataInfo.m_objRecordContent = new clsCardiovascularTend_GX();
					objDataInfo.m_objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
				}
				objDataInfo.m_intFlag = (int)enmRecordsType.CardiovascularTend_GX;
				p_objIntensiveTendInfoArr[0] = objDataInfo;
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				return 0;
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
			long lngRes = 0;

			//检查参数          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from 
											t_emr_cardiovasculartend_gx t1,t_emr_cardiovasculartendcon_gx t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status =0
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;
				//使用strSQL生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable            
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
			   
				//如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
					objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;

					lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL,ref dtbValue,objDPArr);
				
					if(lngRes>0 && dtbValue.Rows.Count ==1)
					{
						p_objModifyInfo=new clsPreModifyInfo();
						p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
						p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
					}
					return (long)enmOperationResult.Record_Already_Delete;
				}	
					//从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
				else if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//如果相同，返回DB_Succees
                    //if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

					//否则，返回Record_Already_Modify
                    //p_objModifyInfo=new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
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
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=p_objRecordContent.m_dtmOpenDate;

				//执行SQL
				long lngEff=0;
				lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}

		/// <summary>
		/// “删除”基本信息
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngDeleteBaseInfo(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=p_objRecordContent.m_dtmOpenDate;

				//执行SQL
				long lngEff=0;
				lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strGetBaseInfoSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
	}
}
