using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

using System.Collections;

namespace com.digitalwave.clsICUBreathTrackService
{
	/// <summary>
	/// Summary description for clsICUBreathTrackService.
	/// 蔡沐忠 2003-7-7
	/// 实现危重特护记录的中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsICUBreathTrackService : com.digitalwave.clsRecordsService.clsRecordsService
	{
		/// <summary>
		/// 更新首次打印时间
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL_Normal="update  icubreath set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t2.modifyuserid) as modifyusername,
       f_getempnamebyno(t1.createuserid) as createusername,
       t1.createdate as createdate_main,
       t1.opendate as opendate_main,
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
       t1.machinemode,
       t1.machinemodexml,
       t1.breathsoundleft,
       t1.breathsoundleftxml,
       t1.breathsoundright,
       t1.breathsoundrightxml,
       t1.inlength,
       t1.inlengthxml,
       t1.gasbagpress,
       t1.gasbagpressxml,
       t1.tidal_volume,
       t1.tidal_volumexml,
       t1.rate,
       t1.ratexml,
       t1.peak_flow,
       t1.peak_flowxml,
       t1.o2,
       t1.o2xml,
       t1.ps,
       t1.psxml,
       t1.assist_sensitivity,
       t1.assist_sensitivityxml,
       t1.inspiratory_pause,
       t1.inspiratory_pausexml,
       t1.mmv_level,
       t1.mmv_levelxml,
       t1.compliance_comp,
       t1.compliance_compxml,
       t1.inspiratory_time,
       t1.inspiratory_timexml,
       t1.inspiratory_pressure,
       t1.inspiratory_pressurexml,
       t1.base_flow,
       t1.base_flowxml,
       t1.flow_trigger,
       t1.flow_triggerxml,
       t1.pressure_slope,
       t1.pressure_slopexml,
       t1.peep,
       t1.peepxml,
       t1.tidal_vol,
       t1.tidal_volxml,
       t1.total_mv,
       t1.total_mvxml,
       t1.spont_mv,
       t1.spont_mvxml,
       t1.total,
       t1.totalxml,
       t1.spont,
       t1.spontxml,
       t1.i_e_ratio,
       t1.i_e_ratioxml,
       t1.ti,
       t1.tixml,
       t1.mmv,
       t1.mmvxml,
       t1.pear,
       t1.pearxml,
       t1.mean,
       t1.meanxml,
       t1.plateau,
       t1.plateauxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.machinemode_last,
       t2.breathsoundleft_last,
       t2.breathsoundright_last,
       t2.inlength_last,
       t2.gasbagpress_last,
       t2.tidal_volume_last,
       t2.rate_last,
       t2.peak_flow_last,
       t2.o2_last,
       t2.ps_last,
       t2.assist_sensitivity_last,
       t2.inspiratory_pause_last,
       t2.mmv_level_last,
       t2.compliance_comp_last,
       t2.inspiratory_time_last,
       t2.inspiratory_pressure_last,
       t2.base_flow_last,
       t2.flow_trigger_last,
       t2.pressure_slope_last,
       t2.peep_last,
       t2.tidal_vol_last,
       t2.total_mv_last,
       t2.spont_mv_last,
       t2.total_last,
       t2.spont_last,
       t2.i_e_ratio_last,
       t2.ti_last,
       t2.mmv_last,
       t2.pear_last,
       t2.mean_last,
       t2.plateau_last
  from icubreath t1, icubreathcontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate, t2.modifydate";

        private const string c_strGetRecordContentSQL_Single = @"select f_getempnamebyno(t2.modifyuserid) as modifyusername,
       t1.createdate as createdate_main,
       t1.opendate as opendate_main,
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
       t1.machinemode,
       t1.machinemodexml,
       t1.breathsoundleft,
       t1.breathsoundleftxml,
       t1.breathsoundright,
       t1.breathsoundrightxml,
       t1.inlength,
       t1.inlengthxml,
       t1.gasbagpress,
       t1.gasbagpressxml,
       t1.tidal_volume,
       t1.tidal_volumexml,
       t1.rate,
       t1.ratexml,
       t1.peak_flow,
       t1.peak_flowxml,
       t1.o2,
       t1.o2xml,
       t1.ps,
       t1.psxml,
       t1.assist_sensitivity,
       t1.assist_sensitivityxml,
       t1.inspiratory_pause,
       t1.inspiratory_pausexml,
       t1.mmv_level,
       t1.mmv_levelxml,
       t1.compliance_comp,
       t1.compliance_compxml,
       t1.inspiratory_time,
       t1.inspiratory_timexml,
       t1.inspiratory_pressure,
       t1.inspiratory_pressurexml,
       t1.base_flow,
       t1.base_flowxml,
       t1.flow_trigger,
       t1.flow_triggerxml,
       t1.pressure_slope,
       t1.pressure_slopexml,
       t1.peep,
       t1.peepxml,
       t1.tidal_vol,
       t1.tidal_volxml,
       t1.total_mv,
       t1.total_mvxml,
       t1.spont_mv,
       t1.spont_mvxml,
       t1.total,
       t1.totalxml,
       t1.spont,
       t1.spontxml,
       t1.i_e_ratio,
       t1.i_e_ratioxml,
       t1.ti,
       t1.tixml,
       t1.mmv,
       t1.mmvxml,
       t1.pear,
       t1.pearxml,
       t1.mean,
       t1.meanxml,
       t1.plateau,
       t1.plateauxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.machinemode_last,
       t2.breathsoundleft_last,
       t2.breathsoundright_last,
       t2.inlength_last,
       t2.gasbagpress_last,
       t2.tidal_volume_last,
       t2.rate_last,
       t2.peak_flow_last,
       t2.o2_last,
       t2.ps_last,
       t2.assist_sensitivity_last,
       t2.inspiratory_pause_last,
       t2.mmv_level_last,
       t2.compliance_comp_last,
       t2.inspiratory_time_last,
       t2.inspiratory_pressure_last,
       t2.base_flow_last,
       t2.flow_trigger_last,
       t2.pressure_slope_last,
       t2.peep_last,
       t2.tidal_vol_last,
       t2.total_mv_last,
       t2.spont_mv_last,
       t2.total_last,
       t2.spont_last,
       t2.i_e_ratio_last,
       t2.ti_last,
       t2.mmv_last,
       t2.pear_last,
       t2.mean_last,
       t2.plateau_last
  from icubreath t1, icubreathcontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate, t2.modifydate";

		private const string c_strGetModifyRecordSQL = "";

		/// <summary>
		///  从WatchItemRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from icubreath where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		/// <summary>
		///  从WatchItemRecord删除表单的主要信息。
		/// </summary>
		private const string c_strDeleteRecordSQL="update icubreath set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUBreathTrackService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null || p_strInPatientID=="" || p_strInPatientDate==null || p_strInPatientDate=="" ||	p_intRecordTypeArr==null || p_dtmOpenDateArr==null || p_intRecordTypeArr.Length != p_dtmOpenDateArr.Length || p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
		 
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//获取IDataParameter数组
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
			long lngRes=0;				
			long lngEff=0;
			for(int i=0;i<p_dtmOpenDateArr.Length;i++)
			{
			
				//按顺序给IDataParameter赋值(使用p_dtmOpenDateArr[i]和p_dtmFirstPrintDate)
//				for(int j2=0;j2<objDPArr.Length;j2++)
//					objDPArr[j2]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=p_dtmOpenDateArr[i];
				//执行SQL
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL_Normal, ref lngEff, objDPArr);
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
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordOpenDate,
			out clsICUBreath p_objTansDataInfo)
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
				IDataParameter[] objDPArr =null;// new Oracle.DataAccess.Client.OracleParameter[3];
				//按顺序给IDataParameter赋值
				//			for(int i=0;i<objDPArr.Length;i++)
				//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;	
				objDPArr[2].Value=DateTime.Parse(p_strRecordOpenDate);
		
				//按顺序给IDataParameter赋值
			
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					clsICUBreathContent objRecordContent= null;
					clsICUBreath objInfo = null;
				
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//生成 clsICUBreath
						objInfo = new clsICUBreath();
						objInfo.m_intFlag = (int)enmRecordsType.ICUBreath;
						//设置结果到 objInfo.m_objRecordContent
						//					objInfo.m_objRecordContent = objRecordContent;
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date;
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date == dtmOpenDate)
						{
							#region
							//从DataTable.Rows中获取结果    
							objRecordContent=new clsICUBreathContent();
							objRecordContent.m_strInPatientID=p_strInPatientID;
							objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
							objRecordContent.m_dtmOpenDate=DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
							objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
							objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
				
							if(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString()=="")
								objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
							else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
							objRecordContent.m_strCreateUserID=dtbValue.Rows[j]["CREATEUSERID"].ToString();
							objRecordContent.m_strModifyUserID=dtbValue.Rows[j]["MODIFYUSERID"].ToString();
							objRecordContent.m_strModifyUserName=dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();
							if(dtbValue.Rows[j]["IFCONFIRM"].ToString()=="")
								objRecordContent.m_bytIfConfirm=0;
							else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
							if(dtbValue.Rows[j]["STATUS"].ToString()=="")
								objRecordContent.m_bytStatus=0;
							else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());

							objRecordContent.m_strConfirmReason=dtbValue.Rows[j]["CONFIRMREASON"].ToString();
							objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();
				
							objRecordContent.m_strMachineMode_Last=dtbValue.Rows[j]["MACHINEMODE_LAST"].ToString();
							objRecordContent.m_strMachineMode=dtbValue.Rows[j]["MACHINEMODE"].ToString();
							objRecordContent.m_strMachineModeXML=dtbValue.Rows[j]["MACHINEMODEXML"].ToString();
						
						
							objRecordContent.m_strBreathSoundLeft_Last=dtbValue.Rows[j]["BREATHSOUNDLEFT_LAST"].ToString();
							objRecordContent.m_strBreathSoundLeft=dtbValue.Rows[j]["BREATHSOUNDLEFT"].ToString();
							objRecordContent.m_strBreathSoundLeftXML=dtbValue.Rows[j]["BREATHSOUNDLEFTXML"].ToString();

							objRecordContent.m_strBreathSoundRight_Last=dtbValue.Rows[j]["BREATHSOUNDRIGHT_LAST"].ToString();
							objRecordContent.m_strBreathSoundRight=dtbValue.Rows[j]["BREATHSOUNDRIGHT"].ToString();
							objRecordContent.m_strBreathSoundRightXML=dtbValue.Rows[j]["BREATHSOUNDRIGHTXML"].ToString();

							objRecordContent.m_strInLength_Last=dtbValue.Rows[j]["INLENGTH_LAST"].ToString();
							objRecordContent.m_strInLength=dtbValue.Rows[j]["INLENGTH"].ToString();
							objRecordContent.m_strInLengthXML=dtbValue.Rows[j]["INLENGTHXML"].ToString();

							objRecordContent.m_strGasbagPress_Last=dtbValue.Rows[j]["GASBAGPRESS_LAST"].ToString();
							objRecordContent.m_strGasbagPress=dtbValue.Rows[j]["GASBAGPRESS"].ToString();
							objRecordContent.m_strGasbagPressXML=dtbValue.Rows[j]["GASBAGPRESSXML"].ToString();

							objRecordContent.m_strTIDAL_VOLUME_Last=dtbValue.Rows[j]["TIDAL_VOLUME_LAST"].ToString();
							objRecordContent.m_strTIDAL_VOLUME=dtbValue.Rows[j]["TIDAL_VOLUME"].ToString();
							objRecordContent.m_strTIDAL_VOLUMEXML=dtbValue.Rows[j]["TIDAL_VOLUMEXML"].ToString();

							objRecordContent.m_strRATE_Last=dtbValue.Rows[j]["RATE_LAST"].ToString();
							objRecordContent.m_strRATE=dtbValue.Rows[j]["RATE"].ToString();
							objRecordContent.m_strRATEXML=dtbValue.Rows[j]["RATEXML"].ToString();

							objRecordContent.m_strPEAK_FLOW_Last=dtbValue.Rows[j]["PEAK_FLOW_LAST"].ToString();
							objRecordContent.m_strPEAK_FLOW=dtbValue.Rows[j]["PEAK_FLOW"].ToString();
							objRecordContent.m_strPEAK_FLOWXML=dtbValue.Rows[j]["PEAK_FLOWXML"].ToString();

							objRecordContent.m_strO2_Last=dtbValue.Rows[j]["O2_LAST"].ToString();
							objRecordContent.m_strO2=dtbValue.Rows[j]["O2"].ToString();
							objRecordContent.m_strO2XML=dtbValue.Rows[j]["O2XML"].ToString();

							objRecordContent.m_strPS_Last=dtbValue.Rows[j]["PS_LAST"].ToString();
							objRecordContent.m_strPS=dtbValue.Rows[j]["PS"].ToString();
							objRecordContent.m_strPSXML=dtbValue.Rows[j]["PSXML"].ToString();

							objRecordContent.m_strASSIST_SENSITIVITY_Last=dtbValue.Rows[j]["ASSIST_SENSITIVITY_LAST"].ToString();
							objRecordContent.m_strASSIST_SENSITIVITY=dtbValue.Rows[j]["ASSIST_SENSITIVITY"].ToString();
							objRecordContent.m_strASSIST_SENSITIVITYXML=dtbValue.Rows[j]["ASSIST_SENSITIVITYXML"].ToString();

							objRecordContent.m_strINSPIRATORY_PAUSE_Last=dtbValue.Rows[j]["INSPIRATORY_PAUSE_LAST"].ToString();
							objRecordContent.m_strINSPIRATORY_PAUSE=dtbValue.Rows[j]["INSPIRATORY_PAUSE"].ToString();
							objRecordContent.m_strINSPIRATORY_PAUSEXML=dtbValue.Rows[j]["INSPIRATORY_PAUSEXML"].ToString();

							objRecordContent.m_strMMV_LEVEL_Last=dtbValue.Rows[j]["MMV_LEVEL_LAST"].ToString();
							objRecordContent.m_strMMV_LEVEL=dtbValue.Rows[j]["MMV_LEVEL"].ToString();
							objRecordContent.m_strMMV_LEVELXML=dtbValue.Rows[j]["MMV_LEVELXML"].ToString();

							objRecordContent.m_strCOMPLIANCE_COMP_Last=dtbValue.Rows[j]["COMPLIANCE_COMP_LAST"].ToString();
							objRecordContent.m_strCOMPLIANCE_COMP=dtbValue.Rows[j]["COMPLIANCE_COMP"].ToString();
							objRecordContent.m_strCOMPLIANCE_COMPXML=dtbValue.Rows[j]["COMPLIANCE_COMPXML"].ToString();
						
							objRecordContent.m_strINSPIRATORY_TIME_Last=dtbValue.Rows[j]["INSPIRATORY_TIME_LAST"].ToString();
							objRecordContent.m_strINSPIRATORY_TIME=dtbValue.Rows[j]["INSPIRATORY_TIME"].ToString();
							objRecordContent.m_strINSPIRATORY_TIMEXML=dtbValue.Rows[j]["INSPIRATORY_TIMEXML"].ToString();

							objRecordContent.m_strINSPIRATORY_PRESSURE_Last=dtbValue.Rows[j]["INSPIRATORY_PRESSURE_LAST"].ToString();
							objRecordContent.m_strINSPIRATORY_PRESSURE=dtbValue.Rows[j]["INSPIRATORY_PRESSURE"].ToString();
							objRecordContent.m_strINSPIRATORY_PRESSUREXML=dtbValue.Rows[j]["INSPIRATORY_PRESSUREXML"].ToString();

							objRecordContent.m_strBASE_FLOW_Last=dtbValue.Rows[j]["BASE_FLOW_LAST"].ToString();
							objRecordContent.m_strBASE_FLOW=dtbValue.Rows[j]["BASE_FLOW"].ToString();
							objRecordContent.m_strBASE_FLOWXML=dtbValue.Rows[j]["BASE_FLOWXML"].ToString();

							objRecordContent.m_strFLOW_TRIGGER_Last=dtbValue.Rows[j]["FLOW_TRIGGER_LAST"].ToString();
							objRecordContent.m_strFLOW_TRIGGER=dtbValue.Rows[j]["FLOW_TRIGGER"].ToString();
							objRecordContent.m_strFLOW_TRIGGERXML=dtbValue.Rows[j]["FLOW_TRIGGERXML"].ToString();

							objRecordContent.m_strPRESSURE_SLOPE_Last=dtbValue.Rows[j]["PRESSURE_SLOPE_LAST"].ToString();
							objRecordContent.m_strPRESSURE_SLOPE=dtbValue.Rows[j]["PRESSURE_SLOPE"].ToString();
							objRecordContent.m_strPRESSURE_SLOPEXML=dtbValue.Rows[j]["PRESSURE_SLOPEXML"].ToString();

							objRecordContent.m_strPEEP_Last=dtbValue.Rows[j]["PEEP_LAST"].ToString();
							objRecordContent.m_strPEEP=dtbValue.Rows[j]["PEEP"].ToString();
							objRecordContent.m_strPEEPXML=dtbValue.Rows[j]["PEEPXML"].ToString();

							objRecordContent.m_strTIDAL_VOL_Last=dtbValue.Rows[j]["TIDAL_VOL_LAST"].ToString();
							objRecordContent.m_strTIDAL_VOL=dtbValue.Rows[j]["TIDAL_VOL"].ToString();
							objRecordContent.m_strTIDAL_VOLXML=dtbValue.Rows[j]["TIDAL_VOLXML"].ToString();

							objRecordContent.m_strTOTAL_MV_Last=dtbValue.Rows[j]["TOTAL_MV_LAST"].ToString();
							objRecordContent.m_strTOTAL_MV=dtbValue.Rows[j]["TOTAL_MV"].ToString();
							objRecordContent.m_strTOTAL_MVXML=dtbValue.Rows[j]["TOTAL_MVXML"].ToString();

							objRecordContent.m_strSPONT_MV_Last=dtbValue.Rows[j]["SPONT_MV_LAST"].ToString();
							objRecordContent.m_strSPONT_MV=dtbValue.Rows[j]["SPONT_MV"].ToString();
							objRecordContent.m_strSPONT_MVXML=dtbValue.Rows[j]["SPONT_MVXML"].ToString();

							objRecordContent.m_strTOTAL_Last=dtbValue.Rows[j]["TOTAL_LAST"].ToString();
							objRecordContent.m_strTOTAL=dtbValue.Rows[j]["TOTAL"].ToString();
							objRecordContent.m_strTOTALXML=dtbValue.Rows[j]["TOTALXML"].ToString();

							objRecordContent.m_strSPONT_Last=dtbValue.Rows[j]["SPONT_LAST"].ToString();
							objRecordContent.m_strSPONT=dtbValue.Rows[j]["SPONT"].ToString();
							objRecordContent.m_strSPONTXML=dtbValue.Rows[j]["SPONTXML"].ToString();

							objRecordContent.m_strI_E_RATIO_Last=dtbValue.Rows[j]["I_E_RATIO_LAST"].ToString();
							objRecordContent.m_strI_E_RATIO=dtbValue.Rows[j]["I_E_RATIO"].ToString();
							objRecordContent.m_strI_E_RATIOXML=dtbValue.Rows[j]["I_E_RATIOXML"].ToString();

							objRecordContent.m_strTi_Last=dtbValue.Rows[j]["TI_LAST"].ToString();
							objRecordContent.m_strTi=dtbValue.Rows[j]["TI"].ToString();
							objRecordContent.m_strTiXML=dtbValue.Rows[j]["TIXML"].ToString();

							objRecordContent.m_strMMV_Last=dtbValue.Rows[j]["MMV_LAST"].ToString();
							objRecordContent.m_strMMV=dtbValue.Rows[j]["MMV"].ToString();
							objRecordContent.m_strMMVXML=dtbValue.Rows[j]["MMVXML"].ToString();

							objRecordContent.m_strPEAR_Last=dtbValue.Rows[j]["PEAR_LAST"].ToString();
							objRecordContent.m_strPEAR=dtbValue.Rows[j]["PEAR"].ToString();
							objRecordContent.m_strPEARXML=dtbValue.Rows[j]["PEARXML"].ToString();

							objRecordContent.m_strMEAN_Last=dtbValue.Rows[j]["MEAN_LAST"].ToString();
							objRecordContent.m_strMEAN=dtbValue.Rows[j]["MEAN"].ToString();
							objRecordContent.m_strMEANXML=dtbValue.Rows[j]["MEANXML"].ToString();

							objRecordContent.m_strPLATEAU_Last=dtbValue.Rows[j]["PLATEAU_LAST"].ToString();
							objRecordContent.m_strPLATEAU=dtbValue.Rows[j]["PLATEAU"].ToString();
							objRecordContent.m_strPLATEAUXML=dtbValue.Rows[j]["PLATEAUXML"].ToString();

						
							//同一条记录的修改,保存在arlModifyData 
							arlModifyData.Add(objRecordContent);
							j++;
							#endregion
						}       
						//后移一条记录，使循环从新的OpenData开始。
						j--;
				
						objInfo.m_objTransDataArr = (clsICUBreathContent[])arlModifyData.ToArray(typeof(clsICUBreathContent));
						arlModifyData.Clear();
			
						//最后一条记录
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
				
						arlTransData.Add(objInfo);
					}
				}
				//返回结果到p_objTansDataInfo
				p_objTansDataInfo = ((clsICUBreath[])arlTransData.ToArray(typeof(clsICUBreath)))[0];
		
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
		/// <param name="p_objWatchItemInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objICUBreathInfoArr)
		{
			p_objICUBreathInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			ArrayList arlTransData = new ArrayList();  
			ArrayList arlModifyData = new ArrayList();
			ArrayList arlTransDataClone = new ArrayList();
			clsICUBreath objAppendInfo = null;
			DateTime dtmOpenDate;
			DateTime dtmCreateDate_Date;
			
			long lngRes = 0;
				clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//获取IDataParameter数组
				

				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);	
		
				//按顺序给IDataParameter赋值
			
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					clsICUBreathContent objRecordContent= null;
					clsICUBreath objInfo = null;
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//生成 clsICUBreath
						objInfo = new clsICUBreath();
						//m_intFlag用来标识这条记录的类型
						objInfo.m_intFlag = (int)enmRecordsType.ICUBreath;
						//设置结果到 objInfo.m_objRecordContent
						//objInfo.m_objRecordContent = objRecordContent;
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate

						dtmOpenDate  = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());

						//如果是同一条记录的修改，也就是OpenDate相同的多条记录
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()) == dtmOpenDate)
						{			
							#region
							//从DataTable.Rows中获取结果    
							objRecordContent=new clsICUBreathContent();
							objRecordContent.m_strInPatientID=p_strInPatientID;
							objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
							objRecordContent.m_dtmOpenDate=DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
							objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
							objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
				
							if(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString()=="")
								objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
							else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
							objRecordContent.m_strCreateUserID=dtbValue.Rows[j]["CREATEUSERID"].ToString();
                            objRecordContent.m_strCreateUserName = dtbValue.Rows[j]["CREATEUSERNAME"].ToString();
							objRecordContent.m_strModifyUserID=dtbValue.Rows[j]["MODIFYUSERID"].ToString();
							objRecordContent.m_strModifyUserName=dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();
							if(dtbValue.Rows[j]["IFCONFIRM"].ToString()=="")
								objRecordContent.m_bytIfConfirm=0;
							else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
							if(dtbValue.Rows[j]["STATUS"].ToString()=="")
								objRecordContent.m_bytStatus=0;
							else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());

							objRecordContent.m_strConfirmReason=dtbValue.Rows[j]["CONFIRMREASON"].ToString();
							objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();
				
							objRecordContent.m_strMachineMode_Last=dtbValue.Rows[j]["MACHINEMODE_LAST"].ToString();
							objRecordContent.m_strMachineMode=dtbValue.Rows[j]["MACHINEMODE"].ToString();
							objRecordContent.m_strMachineModeXML=dtbValue.Rows[j]["MACHINEMODEXML"].ToString();
						
							objRecordContent.m_strBreathSoundLeft_Last=dtbValue.Rows[j]["BREATHSOUNDLEFT_LAST"].ToString();
							objRecordContent.m_strBreathSoundLeft=dtbValue.Rows[j]["BREATHSOUNDLEFT"].ToString();
							objRecordContent.m_strBreathSoundLeftXML=dtbValue.Rows[j]["BREATHSOUNDLEFTXML"].ToString();

							objRecordContent.m_strBreathSoundRight_Last=dtbValue.Rows[j]["BREATHSOUNDRIGHT_LAST"].ToString();
							objRecordContent.m_strBreathSoundRight=dtbValue.Rows[j]["BREATHSOUNDRIGHT"].ToString();
							objRecordContent.m_strBreathSoundRightXML=dtbValue.Rows[j]["BREATHSOUNDRIGHTXML"].ToString();

							objRecordContent.m_strInLength_Last=dtbValue.Rows[j]["INLENGTH_LAST"].ToString();
							objRecordContent.m_strInLength=dtbValue.Rows[j]["INLENGTH"].ToString();
							objRecordContent.m_strInLengthXML=dtbValue.Rows[j]["INLENGTHXML"].ToString();

							objRecordContent.m_strGasbagPress_Last=dtbValue.Rows[j]["GASBAGPRESS_LAST"].ToString();
							objRecordContent.m_strGasbagPress=dtbValue.Rows[j]["GASBAGPRESS"].ToString();
							objRecordContent.m_strGasbagPressXML=dtbValue.Rows[j]["GASBAGPRESSXML"].ToString();

							objRecordContent.m_strTIDAL_VOLUME_Last=dtbValue.Rows[j]["TIDAL_VOLUME_LAST"].ToString();
							objRecordContent.m_strTIDAL_VOLUME=dtbValue.Rows[j]["TIDAL_VOLUME"].ToString();
							objRecordContent.m_strTIDAL_VOLUMEXML=dtbValue.Rows[j]["TIDAL_VOLUMEXML"].ToString();

							objRecordContent.m_strRATE_Last=dtbValue.Rows[j]["RATE_LAST"].ToString();
							objRecordContent.m_strRATE=dtbValue.Rows[j]["RATE"].ToString();
							objRecordContent.m_strRATEXML=dtbValue.Rows[j]["RATEXML"].ToString();

							objRecordContent.m_strPEAK_FLOW_Last=dtbValue.Rows[j]["PEAK_FLOW_LAST"].ToString();
							objRecordContent.m_strPEAK_FLOW=dtbValue.Rows[j]["PEAK_FLOW"].ToString();
							objRecordContent.m_strPEAK_FLOWXML=dtbValue.Rows[j]["PEAK_FLOWXML"].ToString();

							objRecordContent.m_strO2_Last=dtbValue.Rows[j]["O2_LAST"].ToString();
							objRecordContent.m_strO2=dtbValue.Rows[j]["O2"].ToString();
							objRecordContent.m_strO2XML=dtbValue.Rows[j]["O2XML"].ToString();

							objRecordContent.m_strPS_Last=dtbValue.Rows[j]["PS_LAST"].ToString();
							objRecordContent.m_strPS=dtbValue.Rows[j]["PS"].ToString();
							objRecordContent.m_strPSXML=dtbValue.Rows[j]["PSXML"].ToString();

							objRecordContent.m_strASSIST_SENSITIVITY_Last=dtbValue.Rows[j]["ASSIST_SENSITIVITY_LAST"].ToString();
							objRecordContent.m_strASSIST_SENSITIVITY=dtbValue.Rows[j]["ASSIST_SENSITIVITY"].ToString();
							objRecordContent.m_strASSIST_SENSITIVITYXML=dtbValue.Rows[j]["ASSIST_SENSITIVITYXML"].ToString();

							objRecordContent.m_strINSPIRATORY_PAUSE_Last=dtbValue.Rows[j]["INSPIRATORY_PAUSE_LAST"].ToString();
							objRecordContent.m_strINSPIRATORY_PAUSE=dtbValue.Rows[j]["INSPIRATORY_PAUSE"].ToString();
							objRecordContent.m_strINSPIRATORY_PAUSEXML=dtbValue.Rows[j]["INSPIRATORY_PAUSEXML"].ToString();

							objRecordContent.m_strMMV_LEVEL_Last=dtbValue.Rows[j]["MMV_LEVEL_LAST"].ToString();
							objRecordContent.m_strMMV_LEVEL=dtbValue.Rows[j]["MMV_LEVEL"].ToString();
							objRecordContent.m_strMMV_LEVELXML=dtbValue.Rows[j]["MMV_LEVELXML"].ToString();

							objRecordContent.m_strCOMPLIANCE_COMP_Last=dtbValue.Rows[j]["COMPLIANCE_COMP_LAST"].ToString();
							objRecordContent.m_strCOMPLIANCE_COMP=dtbValue.Rows[j]["COMPLIANCE_COMP"].ToString();
							objRecordContent.m_strCOMPLIANCE_COMPXML=dtbValue.Rows[j]["COMPLIANCE_COMPXML"].ToString();
						
							objRecordContent.m_strINSPIRATORY_TIME_Last=dtbValue.Rows[j]["INSPIRATORY_TIME_LAST"].ToString();
							objRecordContent.m_strINSPIRATORY_TIME=dtbValue.Rows[j]["INSPIRATORY_TIME"].ToString();
							objRecordContent.m_strINSPIRATORY_TIMEXML=dtbValue.Rows[j]["INSPIRATORY_TIMEXML"].ToString();

							objRecordContent.m_strINSPIRATORY_PRESSURE_Last=dtbValue.Rows[j]["INSPIRATORY_PRESSURE_LAST"].ToString();
							objRecordContent.m_strINSPIRATORY_PRESSURE=dtbValue.Rows[j]["INSPIRATORY_PRESSURE"].ToString();
							objRecordContent.m_strINSPIRATORY_PRESSUREXML=dtbValue.Rows[j]["INSPIRATORY_PRESSUREXML"].ToString();

							objRecordContent.m_strBASE_FLOW_Last=dtbValue.Rows[j]["BASE_FLOW_LAST"].ToString();
							objRecordContent.m_strBASE_FLOW=dtbValue.Rows[j]["BASE_FLOW"].ToString();
							objRecordContent.m_strBASE_FLOWXML=dtbValue.Rows[j]["BASE_FLOWXML"].ToString();

							objRecordContent.m_strFLOW_TRIGGER_Last=dtbValue.Rows[j]["FLOW_TRIGGER_LAST"].ToString();
							objRecordContent.m_strFLOW_TRIGGER=dtbValue.Rows[j]["FLOW_TRIGGER"].ToString();
							objRecordContent.m_strFLOW_TRIGGERXML=dtbValue.Rows[j]["FLOW_TRIGGERXML"].ToString();

							objRecordContent.m_strPRESSURE_SLOPE_Last=dtbValue.Rows[j]["PRESSURE_SLOPE_LAST"].ToString();
							objRecordContent.m_strPRESSURE_SLOPE=dtbValue.Rows[j]["PRESSURE_SLOPE"].ToString();
							objRecordContent.m_strPRESSURE_SLOPEXML=dtbValue.Rows[j]["PRESSURE_SLOPEXML"].ToString();

							objRecordContent.m_strPEEP_Last=dtbValue.Rows[j]["PEEP_LAST"].ToString();
							objRecordContent.m_strPEEP=dtbValue.Rows[j]["PEEP"].ToString();
							objRecordContent.m_strPEEPXML=dtbValue.Rows[j]["PEEPXML"].ToString();

							objRecordContent.m_strTIDAL_VOL_Last=dtbValue.Rows[j]["TIDAL_VOL_LAST"].ToString();
							objRecordContent.m_strTIDAL_VOL=dtbValue.Rows[j]["TIDAL_VOL"].ToString();
							objRecordContent.m_strTIDAL_VOLXML=dtbValue.Rows[j]["TIDAL_VOLXML"].ToString();

							objRecordContent.m_strTOTAL_MV_Last=dtbValue.Rows[j]["TOTAL_MV_LAST"].ToString();
							objRecordContent.m_strTOTAL_MV=dtbValue.Rows[j]["TOTAL_MV"].ToString();
							objRecordContent.m_strTOTAL_MVXML=dtbValue.Rows[j]["TOTAL_MVXML"].ToString();

							objRecordContent.m_strSPONT_MV_Last=dtbValue.Rows[j]["SPONT_MV_LAST"].ToString();
							objRecordContent.m_strSPONT_MV=dtbValue.Rows[j]["SPONT_MV"].ToString();
							objRecordContent.m_strSPONT_MVXML=dtbValue.Rows[j]["SPONT_MVXML"].ToString();

							objRecordContent.m_strTOTAL_Last=dtbValue.Rows[j]["TOTAL_LAST"].ToString();
							objRecordContent.m_strTOTAL=dtbValue.Rows[j]["TOTAL"].ToString();
							objRecordContent.m_strTOTALXML=dtbValue.Rows[j]["TOTALXML"].ToString();

							objRecordContent.m_strSPONT_Last=dtbValue.Rows[j]["SPONT_LAST"].ToString();
							objRecordContent.m_strSPONT=dtbValue.Rows[j]["SPONT"].ToString();
							objRecordContent.m_strSPONTXML=dtbValue.Rows[j]["SPONTXML"].ToString();

							objRecordContent.m_strI_E_RATIO_Last=dtbValue.Rows[j]["I_E_RATIO_LAST"].ToString();
							objRecordContent.m_strI_E_RATIO=dtbValue.Rows[j]["I_E_RATIO"].ToString();
							objRecordContent.m_strI_E_RATIOXML=dtbValue.Rows[j]["I_E_RATIOXML"].ToString();

							objRecordContent.m_strTi_Last=dtbValue.Rows[j]["TI_LAST"].ToString();
							objRecordContent.m_strTi=dtbValue.Rows[j]["TI"].ToString();
							objRecordContent.m_strTiXML=dtbValue.Rows[j]["TIXML"].ToString();

							objRecordContent.m_strMMV_Last=dtbValue.Rows[j]["MMV_LAST"].ToString();
							objRecordContent.m_strMMV=dtbValue.Rows[j]["MMV"].ToString();
							objRecordContent.m_strMMVXML=dtbValue.Rows[j]["MMVXML"].ToString();

							objRecordContent.m_strPEAR_Last=dtbValue.Rows[j]["PEAR_LAST"].ToString();
							objRecordContent.m_strPEAR=dtbValue.Rows[j]["PEAR"].ToString();
							objRecordContent.m_strPEARXML=dtbValue.Rows[j]["PEARXML"].ToString();

							objRecordContent.m_strMEAN_Last=dtbValue.Rows[j]["MEAN_LAST"].ToString();
							objRecordContent.m_strMEAN=dtbValue.Rows[j]["MEAN"].ToString();
							objRecordContent.m_strMEANXML=dtbValue.Rows[j]["MEANXML"].ToString();

							objRecordContent.m_strPLATEAU_Last=dtbValue.Rows[j]["PLATEAU_LAST"].ToString();
							objRecordContent.m_strPLATEAU=dtbValue.Rows[j]["PLATEAU"].ToString();
							objRecordContent.m_strPLATEAUXML=dtbValue.Rows[j]["PLATEAUXML"].ToString();

							//同一条记录的修改,保存在arlModifyData 
							arlModifyData.Add(objRecordContent);
							j++;	
							#endregion
						}       
						//后移一条记录，使循环从新的OpenData开始。
						j--;
					
						objInfo.m_objTransDataArr = (clsICUBreathContent[])arlModifyData.ToArray(typeof(clsICUBreathContent));
						arlModifyData.Clear();
			
						//同一条记录的最后一次修改
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
				
						arlTransData.Add(objInfo);
					}
				
				}
				//返回结果到p_objTansDataInfoArr
				//如果包括统计的话就返回以下这句
				//			p_objICUBreathInfoArr = (clsICUBreath[])arlTransDataClone.ToArray(typeof(clsICUBreath));
		
				//以下这句不包括统计
				p_objICUBreathInfoArr = (clsICUBreath[])arlTransData.ToArray(typeof(clsICUBreath));

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
			
			long lngRes = 0;
			try
			{
				//  从WatchItemRecordContent获取指定表单的最后修改时间。
				string c_strCheckLastModifyRecordSQL = "";
				if(clsHRPTableService.bytDatabase_Selector == 2)
				{
                    c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select t2.modifydate, t2.modifyuserid
          from icubreath t1, icubreathcontent t2
         where t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t1.status = '0'
           and t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?
         order by t2.modifydate desc)
 where rownum = 1";
				}
				else
				{
					c_strCheckLastModifyRecordSQL=@"select top 1 t2.modifydate,t2.modifyuserid from icubreath t1,icubreathcontent t2
															where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
															and t1.opendate = t2.opendate and t1.status = '0'
															and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc";
				}
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;
				//使用strSQL生成DataTable
				DataTable dtbValue = new DataTable();
		
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL,ref dtbValue,objDPArr);
		
				//如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					//string strSQL2 = "select DeActivedDate,DeActivedOperatorID from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
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
					if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

					//否则，返回Record_Already_Modify
					p_objModifyInfo=new clsPreModifyInfo();
					p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
					return (long)enmOperationResult.Record_Already_Modify;
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
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
				p_objHRPServ.CreateDatabaseParameter(5,out objDPArr);
				//按顺序给IDataParameter赋值
				//			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
		
	}
}
