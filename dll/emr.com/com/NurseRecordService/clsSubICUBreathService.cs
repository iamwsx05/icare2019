using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.clsSubICUBreathService
{
	/// <summary>
	/// Summary description for clsSubICUBreathService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsSubICUBreathService:clsDiseaseTrackService
	{
		public clsSubICUBreathService()
		{}
	

		
		private const string c_strCheckCreateDateSQL = @"select createuserid,opendate from icubreath where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

		private const string c_strAddNewRecordSQL = @"insert into icubreath
      (inpatientid, inpatientdate, opendate, createdate, createuserid, ifconfirm, 
      confirmreason, confirmreasonxml, status,   
      machinemode, machinemodexml, breathsoundleft, 
      breathsoundleftxml, breathsoundright, breathsoundrightxml, inlength, 
      inlengthxml, gasbagpress, gasbagpressxml, tidal_volume, 
      tidal_volumexml, rate, ratexml, peak_flow, peak_flowxml, o2, o2xml, 
      ps, psxml, assist_sensitivity, assist_sensitivityxml, inspiratory_pause, 
      inspiratory_pausexml, mmv_level, mmv_levelxml, compliance_comp, 
      compliance_compxml, inspiratory_time, inspiratory_timexml, 
      inspiratory_pressure, inspiratory_pressurexml, base_flow, 
      base_flowxml, flow_trigger, flow_triggerxml, pressure_slope, 
      pressure_slopexml, peep, peepxml, tidal_vol, tidal_volxml, total_mv, 
      total_mvxml, spont_mv, spont_mvxml, total, totalxml, spont, 
      spontxml, i_e_ratio, i_e_ratioxml, ti, tixml, mmv, mmvxml, pear, pearxml, 
      mean, meanxml, plateau, plateauxml)
      VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";


		private const string c_strAddNewRecordContentSQL = @"insert into icubreathcontent
      (inpatientid, inpatientdate, opendate, modifydate, modifyuserid, 
      machinemode_last, breathsoundleft_last, breathsoundright_last, inlength_last, 
      gasbagpress_last, tidal_volume_last, rate_last, peak_flow_last, o2_last, 
      ps_last, assist_sensitivity_last, inspiratory_pause_last, mmv_level_last, 
      compliance_comp_last, inspiratory_time_last, 
      inspiratory_pressure_last, base_flow_last, flow_trigger_last, 
      pressure_slope_last, peep_last, tidal_vol_last, total_mv_last, 
      spont_mv_last, total_last, spont_last, i_e_ratio_last, ti_last, mmv_last, 
      pear_last, mean_last, plateau_last)
      VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		private const string c_strGetModifyDateAndFirstPrintDateSQL=@"";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from icubreath where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strModifyRecordContentSQL=c_strAddNewRecordContentSQL;
		private const string c_strModifyRecordSQL=@"update icubreath
set machinemode =?, machinemodexml =?, breathsoundleft =?, breathsoundleftxml =?, 
      breathsoundright =?, breathsoundrightxml =?, inlength =?, inlengthxml =?, 
      gasbagpress =?, gasbagpressxml =?, tidal_volume =?, tidal_volumexml =?, 
      rate =?, ratexml =?, peak_flow =?, peak_flowxml =?, o2 =?, o2xml =?, ps =?, 
      psxml =?, assist_sensitivity =?, assist_sensitivityxml =?, 
      inspiratory_pause =?, inspiratory_pausexml =?, mmv_level =?, 
      mmv_levelxml =?, compliance_comp =?, compliance_compxml =?, 
      inspiratory_time =?, inspiratory_timexml =?, inspiratory_pressure =?, 
      inspiratory_pressurexml =?, base_flow =?, base_flowxml =?, 
      flow_trigger =?, flow_triggerxml =?, pressure_slope =?, 
      pressure_slopexml =?, peep =?, peepxml =?, tidal_vol =?, tidal_volxml =?, 
      total_mv =?, total_mvxml =?, spont_mv =?, spont_mvxml =?, total =?, 
      totalxml =?, spont =?, spontxml =?, i_e_ratio =?, i_e_ratioxml =?, ti =?, tixml =?, 
      mmv =?, mmvxml =?, pear =?, pearxml =?, mean =?, meanxml =?, plateau =?, 
      plateauxml =?
where inpatientid=? and inpatientdate=? and opendate=? and status=?";


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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUBreathService","m_lngGetRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//生成DataTable
		
			//执行查询，填充结果到DataTable
		
			//从DataTable.Rows中获取结果
		
			//设置结果
		
			//返回DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUBreathService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//检查参数                              
			if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//执行SQL
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUBreathService","m_lngGetDeleteRecordTimeList");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//生成DataTable
		
			//执行查询，填充结果到DataTable
		
			//从DataTable.Rows中获取结果
		
			//设置结果
		
			//返回DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSubICUBreathService","m_lngGetDeleteRecordTimeListAll");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//生成DataTable
		
			//执行查询，填充结果到DataTable
		
			//从DataTable.Rows中获取结果
		
			//设置结果
		
			//返回DB_Succees
			return (long)enmOperationResult.DB_Succeed;
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
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
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
       t2. gasbagpress_last,
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
 where t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 0
 order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
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
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsICUBreathContent objRecordContent = new clsICUBreathContent();
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

                    objRecordContent.m_strMachineMode = dtbValue.Rows[0]["MACHINEMODE"].ToString();
                    objRecordContent.m_strMachineModeXML = dtbValue.Rows[0]["MACHINEMODEXML"].ToString();
                    objRecordContent.m_strBreathSoundLeft = dtbValue.Rows[0]["BREATHSOUNDLEFT"].ToString();
                    objRecordContent.m_strBreathSoundLeftXML = dtbValue.Rows[0]["BREATHSOUNDLEFTXML"].ToString();
                    objRecordContent.m_strBreathSoundRight = dtbValue.Rows[0]["BREATHSOUNDRIGHT"].ToString();
                    objRecordContent.m_strBreathSoundRightXML = dtbValue.Rows[0]["BREATHSOUNDRIGHTXML"].ToString();
                    objRecordContent.m_strInLength = dtbValue.Rows[0]["INLENGTH"].ToString();
                    objRecordContent.m_strInLengthXML = dtbValue.Rows[0]["INLENGTHXML"].ToString();
                    objRecordContent.m_strGasbagPress = dtbValue.Rows[0]["GASBAGPRESS"].ToString();
                    objRecordContent.m_strGasbagPressXML = dtbValue.Rows[0]["GASBAGPRESSXML"].ToString();

                    objRecordContent.m_strTIDAL_VOLUME = dtbValue.Rows[0]["TIDAL_VOLUME"].ToString();
                    objRecordContent.m_strTIDAL_VOLUMEXML = dtbValue.Rows[0]["TIDAL_VOLUMEXML"].ToString();
                    objRecordContent.m_strRATE = dtbValue.Rows[0]["RATE"].ToString();
                    objRecordContent.m_strRATEXML = dtbValue.Rows[0]["RATEXML"].ToString();
                    objRecordContent.m_strPEAK_FLOW = dtbValue.Rows[0]["PEAK_FLOW"].ToString();
                    objRecordContent.m_strPEAK_FLOWXML = dtbValue.Rows[0]["PEAK_FLOWXML"].ToString();
                    objRecordContent.m_strO2 = dtbValue.Rows[0]["O2"].ToString();
                    objRecordContent.m_strO2XML = dtbValue.Rows[0]["O2XML"].ToString();
                    objRecordContent.m_strPS = dtbValue.Rows[0]["PS"].ToString();
                    objRecordContent.m_strPSXML = dtbValue.Rows[0]["PSXML"].ToString();
                    objRecordContent.m_strASSIST_SENSITIVITY = dtbValue.Rows[0]["ASSIST_SENSITIVITY"].ToString();
                    objRecordContent.m_strASSIST_SENSITIVITYXML = dtbValue.Rows[0]["ASSIST_SENSITIVITYXML"].ToString();
                    objRecordContent.m_strINSPIRATORY_PAUSE = dtbValue.Rows[0]["INSPIRATORY_PAUSE"].ToString();
                    objRecordContent.m_strINSPIRATORY_PAUSEXML = dtbValue.Rows[0]["INSPIRATORY_PAUSEXML"].ToString();
                    objRecordContent.m_strMMV_LEVEL = dtbValue.Rows[0]["MMV_LEVEL"].ToString();
                    objRecordContent.m_strMMV_LEVELXML = dtbValue.Rows[0]["MMV_LEVELXML"].ToString();
                    objRecordContent.m_strCOMPLIANCE_COMP = dtbValue.Rows[0]["COMPLIANCE_COMP"].ToString();
                    objRecordContent.m_strCOMPLIANCE_COMPXML = dtbValue.Rows[0]["COMPLIANCE_COMPXML"].ToString();
                    objRecordContent.m_strINSPIRATORY_TIME = dtbValue.Rows[0]["INSPIRATORY_TIME"].ToString();
                    objRecordContent.m_strINSPIRATORY_TIMEXML = dtbValue.Rows[0]["INSPIRATORY_TIMEXML"].ToString();
                    objRecordContent.m_strINSPIRATORY_PRESSURE = dtbValue.Rows[0]["INSPIRATORY_PRESSURE"].ToString();
                    objRecordContent.m_strINSPIRATORY_PRESSUREXML = dtbValue.Rows[0]["INSPIRATORY_PRESSUREXML"].ToString();
                    objRecordContent.m_strBASE_FLOW = dtbValue.Rows[0]["BASE_FLOW"].ToString();
                    objRecordContent.m_strBASE_FLOWXML = dtbValue.Rows[0]["BASE_FLOWXML"].ToString();
                    objRecordContent.m_strFLOW_TRIGGER = dtbValue.Rows[0]["FLOW_TRIGGER"].ToString();
                    objRecordContent.m_strFLOW_TRIGGERXML = dtbValue.Rows[0]["FLOW_TRIGGERXML"].ToString();
                    objRecordContent.m_strPRESSURE_SLOPE = dtbValue.Rows[0]["PRESSURE_SLOPE"].ToString();
                    objRecordContent.m_strPRESSURE_SLOPEXML = dtbValue.Rows[0]["PRESSURE_SLOPEXML"].ToString();
                    objRecordContent.m_strPEEP = dtbValue.Rows[0]["PEEP"].ToString();
                    objRecordContent.m_strPEEPXML = dtbValue.Rows[0]["PEEPXML"].ToString();
                    objRecordContent.m_strTIDAL_VOL = dtbValue.Rows[0]["TIDAL_VOL"].ToString();
                    objRecordContent.m_strTIDAL_VOLXML = dtbValue.Rows[0]["TIDAL_VOLXML"].ToString();
                    objRecordContent.m_strTOTAL_MV = dtbValue.Rows[0]["TOTAL_MV"].ToString();
                    objRecordContent.m_strTOTAL_MVXML = dtbValue.Rows[0]["TOTAL_MVXML"].ToString();
                    objRecordContent.m_strSPONT_MV = dtbValue.Rows[0]["SPONT_MV"].ToString();
                    objRecordContent.m_strSPONT_MVXML = dtbValue.Rows[0]["SPONT_MVXML"].ToString();
                    objRecordContent.m_strTOTAL = dtbValue.Rows[0]["TOTAL"].ToString();
                    objRecordContent.m_strTOTALXML = dtbValue.Rows[0]["TOTALXML"].ToString();
                    objRecordContent.m_strSPONT = dtbValue.Rows[0]["SPONT"].ToString();
                    objRecordContent.m_strSPONTXML = dtbValue.Rows[0]["SPONTXML"].ToString();
                    objRecordContent.m_strI_E_RATIO = dtbValue.Rows[0]["I_E_RATIO"].ToString();
                    objRecordContent.m_strI_E_RATIOXML = dtbValue.Rows[0]["I_E_RATIOXML"].ToString();
                    objRecordContent.m_strTi = dtbValue.Rows[0]["TI"].ToString();
                    objRecordContent.m_strTiXML = dtbValue.Rows[0]["TIXML"].ToString();
                    objRecordContent.m_strMMV = dtbValue.Rows[0]["MMV"].ToString();
                    objRecordContent.m_strMMVXML = dtbValue.Rows[0]["MMVXML"].ToString();
                    objRecordContent.m_strPEAR = dtbValue.Rows[0]["PEAR"].ToString();
                    objRecordContent.m_strPEARXML = dtbValue.Rows[0]["PEARXML"].ToString();
                    objRecordContent.m_strMEAN = dtbValue.Rows[0]["MEAN"].ToString();
                    objRecordContent.m_strMEANXML = dtbValue.Rows[0]["MEANXML"].ToString();
                    objRecordContent.m_strPLATEAU = dtbValue.Rows[0]["PLATEAU"].ToString();
                    objRecordContent.m_strPLATEAUXML = dtbValue.Rows[0]["PLATEAUXML"].ToString();


                    objRecordContent.m_strMachineMode_Last = dtbValue.Rows[0]["MACHINEMODE_LAST"].ToString();
                    objRecordContent.m_strBreathSoundLeft_Last = dtbValue.Rows[0]["BREATHSOUNDLEFT_LAST"].ToString();
                    objRecordContent.m_strBreathSoundRight_Last = dtbValue.Rows[0]["BREATHSOUNDRIGHT_LAST"].ToString();
                    objRecordContent.m_strInLength_Last = dtbValue.Rows[0]["INLENGTH_LAST"].ToString();
                    objRecordContent.m_strGasbagPress_Last = dtbValue.Rows[0]["GASBAGPRESS_LAST"].ToString();
                    objRecordContent.m_strTIDAL_VOLUME_Last = dtbValue.Rows[0]["TIDAL_VOLUME_LAST"].ToString();
                    objRecordContent.m_strRATE_Last = dtbValue.Rows[0]["RATE_LAST"].ToString();
                    objRecordContent.m_strPEAK_FLOW_Last = dtbValue.Rows[0]["PEAK_FLOW_LAST"].ToString();
                    objRecordContent.m_strO2_Last = dtbValue.Rows[0]["O2_LAST"].ToString();
                    objRecordContent.m_strPS_Last = dtbValue.Rows[0]["PS_LAST"].ToString();
                    objRecordContent.m_strASSIST_SENSITIVITY_Last = dtbValue.Rows[0]["ASSIST_SENSITIVITY_LAST"].ToString();
                    objRecordContent.m_strINSPIRATORY_PAUSE_Last = dtbValue.Rows[0]["INSPIRATORY_PAUSE_LAST"].ToString();
                    objRecordContent.m_strMMV_LEVEL_Last = dtbValue.Rows[0]["MMV_LEVEL_LAST"].ToString();
                    objRecordContent.m_strCOMPLIANCE_COMP_Last = dtbValue.Rows[0]["COMPLIANCE_COMP_LAST"].ToString();
                    objRecordContent.m_strINSPIRATORY_TIME_Last = dtbValue.Rows[0]["INSPIRATORY_TIME_LAST"].ToString();
                    objRecordContent.m_strINSPIRATORY_PRESSURE_Last = dtbValue.Rows[0]["INSPIRATORY_PRESSURE_LAST"].ToString();
                    objRecordContent.m_strBASE_FLOW_Last = dtbValue.Rows[0]["BASE_FLOW_LAST"].ToString();
                    objRecordContent.m_strFLOW_TRIGGER_Last = dtbValue.Rows[0]["FLOW_TRIGGER_LAST"].ToString();
                    objRecordContent.m_strPRESSURE_SLOPE_Last = dtbValue.Rows[0]["PRESSURE_SLOPE_LAST"].ToString();
                    objRecordContent.m_strPEEP_Last = dtbValue.Rows[0]["PEEP_LAST"].ToString();
                    objRecordContent.m_strTIDAL_VOL_Last = dtbValue.Rows[0]["TIDAL_VOL_LAST"].ToString();
                    objRecordContent.m_strTOTAL_MV_Last = dtbValue.Rows[0]["TOTAL_MV_LAST"].ToString();
                    objRecordContent.m_strSPONT_MV_Last = dtbValue.Rows[0]["SPONT_MV_LAST"].ToString();
                    objRecordContent.m_strTOTAL_Last = dtbValue.Rows[0]["TOTAL_LAST"].ToString();
                    objRecordContent.m_strSPONT_Last = dtbValue.Rows[0]["SPONT_LAST"].ToString();
                    objRecordContent.m_strI_E_RATIO_Last = dtbValue.Rows[0]["I_E_RATIO_LAST"].ToString();
                    objRecordContent.m_strTi_Last = dtbValue.Rows[0]["TI_LAST"].ToString();
                    objRecordContent.m_strMMV_Last = dtbValue.Rows[0]["MMV_LAST"].ToString();
                    objRecordContent.m_strPEAR_Last = dtbValue.Rows[0]["PEAR_LAST"].ToString();
                    objRecordContent.m_strMEAN_Last = dtbValue.Rows[0]["MEAN_LAST"].ToString();
                    objRecordContent.m_strPLATEAU_Last = dtbValue.Rows[0]["PLATEAU_LAST"].ToString();


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

		/// <summary>
		/// 查看是否有相同的记录时间
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
			//string strSQL = "select CreateUserID,OpenDate from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and CreateDate= ? and Status=0";

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

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
		/// 保存记录到数据库。添加主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
                clsICUBreathContent objContent = (clsICUBreathContent)p_objRecordContent;

                //获取IDataParameter数组

                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[71];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(71, out objDPArr);
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

                objDPArr[9].Value = objContent.m_strMachineMode;
                objDPArr[10].Value = objContent.m_strMachineModeXML;
                objDPArr[11].Value = objContent.m_strBreathSoundLeft;
                objDPArr[12].Value = objContent.m_strBreathSoundLeftXML;
                objDPArr[13].Value = objContent.m_strBreathSoundRight;
                objDPArr[14].Value = objContent.m_strBreathSoundRightXML;
                objDPArr[15].Value = objContent.m_strInLength;
                objDPArr[16].Value = objContent.m_strInLengthXML;
                objDPArr[17].Value = objContent.m_strGasbagPress;
                objDPArr[18].Value = objContent.m_strGasbagPressXML;
                objDPArr[19].Value = objContent.m_strTIDAL_VOLUME;
                objDPArr[20].Value = objContent.m_strTIDAL_VOLUMEXML;
                objDPArr[21].Value = objContent.m_strRATE;
                objDPArr[22].Value = objContent.m_strRATEXML;
                objDPArr[23].Value = objContent.m_strPEAK_FLOW;
                objDPArr[24].Value = objContent.m_strPEAK_FLOWXML;
                objDPArr[25].Value = objContent.m_strO2;
                objDPArr[26].Value = objContent.m_strO2XML;
                objDPArr[27].Value = objContent.m_strPS;
                objDPArr[28].Value = objContent.m_strPSXML;
                objDPArr[29].Value = objContent.m_strASSIST_SENSITIVITY;
                objDPArr[30].Value = objContent.m_strASSIST_SENSITIVITYXML;
                objDPArr[31].Value = objContent.m_strINSPIRATORY_PAUSE;
                objDPArr[32].Value = objContent.m_strINSPIRATORY_PAUSEXML;
                objDPArr[33].Value = objContent.m_strMMV_LEVEL;
                objDPArr[34].Value = objContent.m_strMMV_LEVELXML;
                objDPArr[35].Value = objContent.m_strCOMPLIANCE_COMP;
                objDPArr[36].Value = objContent.m_strCOMPLIANCE_COMPXML;
                objDPArr[37].Value = objContent.m_strINSPIRATORY_TIME;
                objDPArr[38].Value = objContent.m_strINSPIRATORY_TIMEXML;
                objDPArr[39].Value = objContent.m_strINSPIRATORY_PRESSURE;
                objDPArr[40].Value = objContent.m_strINSPIRATORY_PRESSUREXML;
                objDPArr[41].Value = objContent.m_strBASE_FLOW;
                objDPArr[42].Value = objContent.m_strBASE_FLOWXML;
                objDPArr[43].Value = objContent.m_strFLOW_TRIGGER;
                objDPArr[44].Value = objContent.m_strFLOW_TRIGGERXML;
                objDPArr[45].Value = objContent.m_strPRESSURE_SLOPE;
                objDPArr[46].Value = objContent.m_strPRESSURE_SLOPEXML;
                objDPArr[47].Value = objContent.m_strPEEP;
                objDPArr[48].Value = objContent.m_strPEEPXML;
                objDPArr[49].Value = objContent.m_strTIDAL_VOL;
                objDPArr[50].Value = objContent.m_strTIDAL_VOLXML;
                objDPArr[51].Value = objContent.m_strTOTAL_MV;
                objDPArr[52].Value = objContent.m_strTOTAL_MVXML;
                objDPArr[53].Value = objContent.m_strSPONT_MV;
                objDPArr[54].Value = objContent.m_strSPONT_MVXML;
                objDPArr[55].Value = objContent.m_strTOTAL;
                objDPArr[56].Value = objContent.m_strTOTALXML;
                objDPArr[57].Value = objContent.m_strSPONT;
                objDPArr[58].Value = objContent.m_strSPONTXML;
                objDPArr[59].Value = objContent.m_strI_E_RATIO;
                objDPArr[60].Value = objContent.m_strI_E_RATIOXML;
                objDPArr[61].Value = objContent.m_strTi;
                objDPArr[62].Value = objContent.m_strTiXML;
                objDPArr[63].Value = objContent.m_strMMV;
                objDPArr[64].Value = objContent.m_strMMVXML;
                objDPArr[65].Value = objContent.m_strPEAR;
                objDPArr[66].Value = objContent.m_strPEARXML;
                objDPArr[67].Value = objContent.m_strMEAN;
                objDPArr[68].Value = objContent.m_strMEANXML;
                objDPArr[69].Value = objContent.m_strPLATEAU;
                objDPArr[70].Value = objContent.m_strPLATEAUXML;


                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[36];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(36, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strMachineMode_Last;
                objDPArr2[6].Value = objContent.m_strBreathSoundLeft_Last;
                objDPArr2[7].Value = objContent.m_strBreathSoundRight_Last;
                objDPArr2[8].Value = objContent.m_strInLength_Last;
                objDPArr2[9].Value = objContent.m_strGasbagPress_Last;
                objDPArr2[10].Value = objContent.m_strTIDAL_VOLUME_Last;
                objDPArr2[11].Value = objContent.m_strRATE_Last;
                objDPArr2[12].Value = objContent.m_strPEAK_FLOW_Last;
                objDPArr2[13].Value = objContent.m_strO2_Last;
                objDPArr2[14].Value = objContent.m_strPS_Last;
                objDPArr2[15].Value = objContent.m_strASSIST_SENSITIVITY_Last;
                objDPArr2[16].Value = objContent.m_strINSPIRATORY_PAUSE_Last;
                objDPArr2[17].Value = objContent.m_strMMV_LEVEL_Last;
                objDPArr2[18].Value = objContent.m_strCOMPLIANCE_COMP_Last;
                objDPArr2[19].Value = objContent.m_strINSPIRATORY_TIME_Last;
                objDPArr2[20].Value = objContent.m_strINSPIRATORY_PRESSURE_Last;
                objDPArr2[21].Value = objContent.m_strBASE_FLOW_Last;
                objDPArr2[22].Value = objContent.m_strFLOW_TRIGGER_Last;
                objDPArr2[23].Value = objContent.m_strPRESSURE_SLOPE_Last;
                objDPArr2[24].Value = objContent.m_strPEEP_Last;
                objDPArr2[25].Value = objContent.m_strTIDAL_VOL_Last;
                objDPArr2[26].Value = objContent.m_strTOTAL_MV_Last;
                objDPArr2[27].Value = objContent.m_strSPONT_MV_Last;
                objDPArr2[28].Value = objContent.m_strTOTAL_Last;
                objDPArr2[29].Value = objContent.m_strSPONT_Last;
                objDPArr2[30].Value = objContent.m_strI_E_RATIO_Last;
                objDPArr2[31].Value = objContent.m_strTi_Last;
                objDPArr2[32].Value = objContent.m_strMMV_Last;
                objDPArr2[33].Value = objContent.m_strPEAR_Last;
                objDPArr2[34].Value = objContent.m_strMEAN_Last;
                objDPArr2[35].Value = objContent.m_strPLATEAU_Last;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //执行SQL
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

            } return lngRes;
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
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from icubreath t1,icubreathcontent t2
					where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
					and t1.opendate = t2.opendate and t1.status =0
					and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //按顺序给IDataParameter赋值

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from WatchItemRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

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

            } return lngRes;	
		}

	
		/// <summary>
		///  把新修改的内容保存到数据库。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值
                clsICUBreathContent objContent = (clsICUBreathContent)p_objRecordContent;

                //获取IDataParameter数组

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[66];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(66, out objDPArr);
                objDPArr[0].Value = objContent.m_strMachineMode;
                objDPArr[1].Value = objContent.m_strMachineModeXML;
                objDPArr[2].Value = objContent.m_strBreathSoundLeft;
                objDPArr[3].Value = objContent.m_strBreathSoundLeftXML;
                objDPArr[4].Value = objContent.m_strBreathSoundRight;
                objDPArr[5].Value = objContent.m_strBreathSoundRightXML;
                objDPArr[6].Value = objContent.m_strInLength;
                objDPArr[7].Value = objContent.m_strInLengthXML;
                objDPArr[8].Value = objContent.m_strGasbagPress;
                objDPArr[9].Value = objContent.m_strGasbagPressXML;
                objDPArr[10].Value = objContent.m_strTIDAL_VOLUME;
                objDPArr[11].Value = objContent.m_strTIDAL_VOLUMEXML;
                objDPArr[12].Value = objContent.m_strRATE;
                objDPArr[13].Value = objContent.m_strRATEXML;
                objDPArr[14].Value = objContent.m_strPEAK_FLOW;
                objDPArr[15].Value = objContent.m_strPEAK_FLOWXML;
                objDPArr[16].Value = objContent.m_strO2;
                objDPArr[17].Value = objContent.m_strO2XML;
                objDPArr[18].Value = objContent.m_strPS;
                objDPArr[19].Value = objContent.m_strPSXML;
                objDPArr[20].Value = objContent.m_strASSIST_SENSITIVITY;
                objDPArr[21].Value = objContent.m_strASSIST_SENSITIVITYXML;
                objDPArr[22].Value = objContent.m_strINSPIRATORY_PAUSE;
                objDPArr[23].Value = objContent.m_strINSPIRATORY_PAUSEXML;
                objDPArr[24].Value = objContent.m_strMMV_LEVEL;
                objDPArr[25].Value = objContent.m_strMMV_LEVELXML;
                objDPArr[26].Value = objContent.m_strCOMPLIANCE_COMP;
                objDPArr[27].Value = objContent.m_strCOMPLIANCE_COMPXML;
                objDPArr[28].Value = objContent.m_strINSPIRATORY_TIME;
                objDPArr[29].Value = objContent.m_strINSPIRATORY_TIMEXML;
                objDPArr[30].Value = objContent.m_strINSPIRATORY_PRESSURE;
                objDPArr[31].Value = objContent.m_strINSPIRATORY_PRESSUREXML;
                objDPArr[32].Value = objContent.m_strBASE_FLOW;
                objDPArr[33].Value = objContent.m_strBASE_FLOWXML;
                objDPArr[34].Value = objContent.m_strFLOW_TRIGGER;
                objDPArr[35].Value = objContent.m_strFLOW_TRIGGERXML;
                objDPArr[36].Value = objContent.m_strPRESSURE_SLOPE;
                objDPArr[37].Value = objContent.m_strPRESSURE_SLOPEXML;
                objDPArr[38].Value = objContent.m_strPEEP;
                objDPArr[39].Value = objContent.m_strPEEPXML;
                objDPArr[40].Value = objContent.m_strTIDAL_VOL;
                objDPArr[41].Value = objContent.m_strTIDAL_VOLXML;
                objDPArr[42].Value = objContent.m_strTOTAL_MV;
                objDPArr[43].Value = objContent.m_strTOTAL_MVXML;
                objDPArr[44].Value = objContent.m_strSPONT_MV;
                objDPArr[45].Value = objContent.m_strSPONT_MVXML;
                objDPArr[46].Value = objContent.m_strTOTAL;
                objDPArr[47].Value = objContent.m_strTOTALXML;
                objDPArr[48].Value = objContent.m_strSPONT;
                objDPArr[49].Value = objContent.m_strSPONTXML;
                objDPArr[50].Value = objContent.m_strI_E_RATIO;
                objDPArr[51].Value = objContent.m_strI_E_RATIOXML;
                objDPArr[52].Value = objContent.m_strTi;
                objDPArr[53].Value = objContent.m_strTiXML;
                objDPArr[54].Value = objContent.m_strMMV;
                objDPArr[55].Value = objContent.m_strMMVXML;
                objDPArr[56].Value = objContent.m_strPEAR;
                objDPArr[57].Value = objContent.m_strPEARXML;
                objDPArr[58].Value = objContent.m_strMEAN;
                objDPArr[59].Value = objContent.m_strMEANXML;
                objDPArr[60].Value = objContent.m_strPLATEAU;
                objDPArr[61].Value = objContent.m_strPLATEAUXML;

                objDPArr[62].Value = objContent.m_strInPatientID;
                objDPArr[63].DbType = DbType.DateTime;
                objDPArr[63].Value = objContent.m_dtmInPatientDate;
                objDPArr[64].DbType = DbType.DateTime;
                objDPArr[64].Value = objContent.m_dtmOpenDate;
                objDPArr[65].Value = 0;


                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[36];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr2.Length;i++)
                //				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
                p_objHRPServ.CreateDatabaseParameter(36, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strMachineMode_Last;
                objDPArr2[6].Value = objContent.m_strBreathSoundLeft_Last;
                objDPArr2[7].Value = objContent.m_strBreathSoundRight_Last;
                objDPArr2[8].Value = objContent.m_strInLength_Last;
                objDPArr2[9].Value = objContent.m_strGasbagPress_Last;
                objDPArr2[10].Value = objContent.m_strTIDAL_VOLUME_Last;
                objDPArr2[11].Value = objContent.m_strRATE_Last;
                objDPArr2[12].Value = objContent.m_strPEAK_FLOW_Last;
                objDPArr2[13].Value = objContent.m_strO2_Last;
                objDPArr2[14].Value = objContent.m_strPS_Last;
                objDPArr2[15].Value = objContent.m_strASSIST_SENSITIVITY_Last;
                objDPArr2[16].Value = objContent.m_strINSPIRATORY_PAUSE_Last;
                objDPArr2[17].Value = objContent.m_strMMV_LEVEL_Last;
                objDPArr2[18].Value = objContent.m_strCOMPLIANCE_COMP_Last;
                objDPArr2[19].Value = objContent.m_strINSPIRATORY_TIME_Last;
                objDPArr2[20].Value = objContent.m_strINSPIRATORY_PRESSURE_Last;
                objDPArr2[21].Value = objContent.m_strBASE_FLOW_Last;
                objDPArr2[22].Value = objContent.m_strFLOW_TRIGGER_Last;
                objDPArr2[23].Value = objContent.m_strPRESSURE_SLOPE_Last;
                objDPArr2[24].Value = objContent.m_strPEEP_Last;
                objDPArr2[25].Value = objContent.m_strTIDAL_VOL_Last;
                objDPArr2[26].Value = objContent.m_strTOTAL_MV_Last;
                objDPArr2[27].Value = objContent.m_strSPONT_MV_Last;
                objDPArr2[28].Value = objContent.m_strTOTAL_Last;
                objDPArr2[29].Value = objContent.m_strSPONT_Last;
                objDPArr2[30].Value = objContent.m_strI_E_RATIO_Last;
                objDPArr2[31].Value = objContent.m_strTi_Last;
                objDPArr2[32].Value = objContent.m_strMMV_Last;
                objDPArr2[33].Value = objContent.m_strPEAR_Last;
                objDPArr2[34].Value = objContent.m_strMEAN_Last;
                objDPArr2[35].Value = objContent.m_strPLATEAU_Last;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);

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
		///	在编辑窗体重不用实现本方法
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			//获取IDataParameter数组
		
			//按顺序给IDataParameter赋值
		
			//执行SQL
			return (long)enmOperationResult.DB_Succeed;
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
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
			out clsTrackRecordContent p_objRecordContent)
		{
			p_objRecordContent=null;
			
			//检查参数
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
       t2. gasbagpress_last,
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
 where t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
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
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsICUBreathContent objRecordContent = new clsICUBreathContent();
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

                    objRecordContent.m_strMachineMode = dtbValue.Rows[0]["MACHINEMODE"].ToString();
                    objRecordContent.m_strMachineModeXML = dtbValue.Rows[0]["MACHINEMODEXML"].ToString();
                    objRecordContent.m_strBreathSoundLeft = dtbValue.Rows[0]["BREATHSOUNDLEFT"].ToString();
                    objRecordContent.m_strBreathSoundLeftXML = dtbValue.Rows[0]["BREATHSOUNDLEFTXML"].ToString();
                    objRecordContent.m_strBreathSoundRight = dtbValue.Rows[0]["BREATHSOUNDRIGHT"].ToString();
                    objRecordContent.m_strBreathSoundRightXML = dtbValue.Rows[0]["BREATHSOUNDRIGHTXML"].ToString();
                    objRecordContent.m_strInLength = dtbValue.Rows[0]["INLENGTH"].ToString();
                    objRecordContent.m_strInLengthXML = dtbValue.Rows[0]["INLENGTHXML"].ToString();
                    objRecordContent.m_strGasbagPress = dtbValue.Rows[0]["GASBAGPRESS"].ToString();
                    objRecordContent.m_strGasbagPressXML = dtbValue.Rows[0]["GASBAGPRESSXML"].ToString();

                    objRecordContent.m_strTIDAL_VOLUME = dtbValue.Rows[0]["TIDAL_VOLUME"].ToString();
                    objRecordContent.m_strTIDAL_VOLUMEXML = dtbValue.Rows[0]["TIDAL_VOLUMEXML"].ToString();
                    objRecordContent.m_strRATE = dtbValue.Rows[0]["RATE"].ToString();
                    objRecordContent.m_strRATEXML = dtbValue.Rows[0]["RATEXML"].ToString();
                    objRecordContent.m_strPEAK_FLOW = dtbValue.Rows[0]["PEAK_FLOW"].ToString();
                    objRecordContent.m_strPEAK_FLOWXML = dtbValue.Rows[0]["PEAK_FLOWXML"].ToString();
                    objRecordContent.m_strO2 = dtbValue.Rows[0]["O2"].ToString();
                    objRecordContent.m_strO2XML = dtbValue.Rows[0]["O2XML"].ToString();
                    objRecordContent.m_strPS = dtbValue.Rows[0]["PS"].ToString();
                    objRecordContent.m_strPSXML = dtbValue.Rows[0]["PSXML"].ToString();
                    objRecordContent.m_strASSIST_SENSITIVITY = dtbValue.Rows[0]["ASSIST_SENSITIVITY"].ToString();
                    objRecordContent.m_strASSIST_SENSITIVITYXML = dtbValue.Rows[0]["ASSIST_SENSITIVITYXML"].ToString();
                    objRecordContent.m_strINSPIRATORY_PAUSE = dtbValue.Rows[0]["INSPIRATORY_PAUSE"].ToString();
                    objRecordContent.m_strINSPIRATORY_PAUSEXML = dtbValue.Rows[0]["INSPIRATORY_PAUSEXML"].ToString();
                    objRecordContent.m_strMMV_LEVEL = dtbValue.Rows[0]["MMV_LEVEL"].ToString();
                    objRecordContent.m_strMMV_LEVELXML = dtbValue.Rows[0]["MMV_LEVELXML"].ToString();
                    objRecordContent.m_strCOMPLIANCE_COMP = dtbValue.Rows[0]["COMPLIANCE_COMP"].ToString();
                    objRecordContent.m_strCOMPLIANCE_COMPXML = dtbValue.Rows[0]["COMPLIANCE_COMPXML"].ToString();
                    objRecordContent.m_strINSPIRATORY_TIME = dtbValue.Rows[0]["INSPIRATORY_TIME"].ToString();
                    objRecordContent.m_strINSPIRATORY_TIMEXML = dtbValue.Rows[0]["INSPIRATORY_TIMEXML"].ToString();
                    objRecordContent.m_strINSPIRATORY_PRESSURE = dtbValue.Rows[0]["INSPIRATORY_PRESSURE"].ToString();
                    objRecordContent.m_strINSPIRATORY_PRESSUREXML = dtbValue.Rows[0]["INSPIRATORY_PRESSUREXML"].ToString();
                    objRecordContent.m_strBASE_FLOW = dtbValue.Rows[0]["BASE_FLOW"].ToString();
                    objRecordContent.m_strBASE_FLOWXML = dtbValue.Rows[0]["BASE_FLOWXML"].ToString();
                    objRecordContent.m_strFLOW_TRIGGER = dtbValue.Rows[0]["FLOW_TRIGGER"].ToString();
                    objRecordContent.m_strFLOW_TRIGGERXML = dtbValue.Rows[0]["FLOW_TRIGGERXML"].ToString();
                    objRecordContent.m_strPRESSURE_SLOPE = dtbValue.Rows[0]["PRESSURE_SLOPE"].ToString();
                    objRecordContent.m_strPRESSURE_SLOPEXML = dtbValue.Rows[0]["PRESSURE_SLOPEXML"].ToString();
                    objRecordContent.m_strPEEP = dtbValue.Rows[0]["PEEP"].ToString();
                    objRecordContent.m_strPEEPXML = dtbValue.Rows[0]["PEEPXML"].ToString();
                    objRecordContent.m_strTIDAL_VOL = dtbValue.Rows[0]["TIDAL_VOL"].ToString();
                    objRecordContent.m_strTIDAL_VOLXML = dtbValue.Rows[0]["TIDAL_VOLXML"].ToString();
                    objRecordContent.m_strTOTAL_MV = dtbValue.Rows[0]["TOTAL_MV"].ToString();
                    objRecordContent.m_strTOTAL_MVXML = dtbValue.Rows[0]["TOTAL_MVXML"].ToString();
                    objRecordContent.m_strSPONT_MV = dtbValue.Rows[0]["SPONT_MV"].ToString();
                    objRecordContent.m_strSPONT_MVXML = dtbValue.Rows[0]["SPONT_MVXML"].ToString();
                    objRecordContent.m_strTOTAL = dtbValue.Rows[0]["TOTAL"].ToString();
                    objRecordContent.m_strTOTALXML = dtbValue.Rows[0]["TOTALXML"].ToString();
                    objRecordContent.m_strSPONT = dtbValue.Rows[0]["SPONT"].ToString();
                    objRecordContent.m_strSPONTXML = dtbValue.Rows[0]["SPONTXML"].ToString();
                    objRecordContent.m_strI_E_RATIO = dtbValue.Rows[0]["I_E_RATIO"].ToString();
                    objRecordContent.m_strI_E_RATIOXML = dtbValue.Rows[0]["I_E_RATIOXML"].ToString();
                    objRecordContent.m_strTi = dtbValue.Rows[0]["TI"].ToString();
                    objRecordContent.m_strTiXML = dtbValue.Rows[0]["TIXML"].ToString();
                    objRecordContent.m_strMMV = dtbValue.Rows[0]["MMV"].ToString();
                    objRecordContent.m_strMMVXML = dtbValue.Rows[0]["MMVXML"].ToString();
                    objRecordContent.m_strPEAR = dtbValue.Rows[0]["PEAR"].ToString();
                    objRecordContent.m_strPEARXML = dtbValue.Rows[0]["PEARXML"].ToString();
                    objRecordContent.m_strMEAN = dtbValue.Rows[0]["MEAN"].ToString();
                    objRecordContent.m_strMEANXML = dtbValue.Rows[0]["MEANXML"].ToString();
                    objRecordContent.m_strPLATEAU = dtbValue.Rows[0]["PLATEAU"].ToString();
                    objRecordContent.m_strPLATEAUXML = dtbValue.Rows[0]["PLATEAUXML"].ToString();


                    objRecordContent.m_strMachineMode_Last = dtbValue.Rows[0]["MACHINEMODE_LAST"].ToString();
                    objRecordContent.m_strBreathSoundLeft_Last = dtbValue.Rows[0]["BREATHSOUNDLEFT_LAST"].ToString();
                    objRecordContent.m_strBreathSoundRight_Last = dtbValue.Rows[0]["BREATHSOUNDRIGHT_LAST"].ToString();
                    objRecordContent.m_strInLength_Last = dtbValue.Rows[0]["INLENGTH_LAST"].ToString();
                    objRecordContent.m_strGasbagPress_Last = dtbValue.Rows[0]["GASBAGPRESS_LAST"].ToString();
                    objRecordContent.m_strTIDAL_VOLUME_Last = dtbValue.Rows[0]["TIDAL_VOLUME_LAST"].ToString();
                    objRecordContent.m_strRATE_Last = dtbValue.Rows[0]["RATE_LAST"].ToString();
                    objRecordContent.m_strPEAK_FLOW_Last = dtbValue.Rows[0]["PEAK_FLOW_LAST"].ToString();
                    objRecordContent.m_strO2_Last = dtbValue.Rows[0]["O2_LAST"].ToString();
                    objRecordContent.m_strPS_Last = dtbValue.Rows[0]["PS_LAST"].ToString();
                    objRecordContent.m_strASSIST_SENSITIVITY_Last = dtbValue.Rows[0]["ASSIST_SENSITIVITY_LAST"].ToString();
                    objRecordContent.m_strINSPIRATORY_PAUSE_Last = dtbValue.Rows[0]["INSPIRATORY_PAUSE_LAST"].ToString();
                    objRecordContent.m_strMMV_LEVEL_Last = dtbValue.Rows[0]["MMV_LEVEL_LAST"].ToString();
                    objRecordContent.m_strCOMPLIANCE_COMP_Last = dtbValue.Rows[0]["COMPLIANCE_COMP_LAST"].ToString();
                    objRecordContent.m_strINSPIRATORY_TIME_Last = dtbValue.Rows[0]["INSPIRATORY_TIME_LAST"].ToString();
                    objRecordContent.m_strINSPIRATORY_PRESSURE_Last = dtbValue.Rows[0]["INSPIRATORY_PRESSURE_LAST"].ToString();
                    objRecordContent.m_strBASE_FLOW_Last = dtbValue.Rows[0]["BASE_FLOW_LAST"].ToString();
                    objRecordContent.m_strFLOW_TRIGGER_Last = dtbValue.Rows[0]["FLOW_TRIGGER_LAST"].ToString();
                    objRecordContent.m_strPRESSURE_SLOPE_Last = dtbValue.Rows[0]["PRESSURE_SLOPE_LAST"].ToString();
                    objRecordContent.m_strPEEP_Last = dtbValue.Rows[0]["PEEP_LAST"].ToString();
                    objRecordContent.m_strTIDAL_VOL_Last = dtbValue.Rows[0]["TIDAL_VOL_LAST"].ToString();
                    objRecordContent.m_strTOTAL_MV_Last = dtbValue.Rows[0]["TOTAL_MV_LAST"].ToString();
                    objRecordContent.m_strSPONT_MV_Last = dtbValue.Rows[0]["SPONT_MV_LAST"].ToString();
                    objRecordContent.m_strTOTAL_Last = dtbValue.Rows[0]["TOTAL_LAST"].ToString();
                    objRecordContent.m_strSPONT_Last = dtbValue.Rows[0]["SPONT_LAST"].ToString();
                    objRecordContent.m_strI_E_RATIO_Last = dtbValue.Rows[0]["I_E_RATIO_LAST"].ToString();
                    objRecordContent.m_strTi_Last = dtbValue.Rows[0]["TI_LAST"].ToString();
                    objRecordContent.m_strMMV_Last = dtbValue.Rows[0]["MMV_LAST"].ToString();
                    objRecordContent.m_strPEAR_Last = dtbValue.Rows[0]["PEAR_LAST"].ToString();
                    objRecordContent.m_strMEAN_Last = dtbValue.Rows[0]["MEAN_LAST"].ToString();
                    objRecordContent.m_strPLATEAU_Last = dtbValue.Rows[0]["PLATEAU_LAST"].ToString();


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
