using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.DepartmentManagerService;

namespace com.digitalwave.clsICUIntensiveTrackService
{
	/// <summary>
	/// Summary description for clsICUIntensiveTrackService.
	/// 蔡沐忠 2003-7-7
	/// 实现危重特护记录的中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsICUIntensiveTrackService : com.digitalwave.clsRecordsService.clsRecordsService
	{
		/// <summary>
		/// 更新首次打印时间
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL_Normal="update  icuintensivetend set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t2.modifyuserid) as modifyusername,
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
       t1.t,
       t1.t_xml,
       t1.p,
       t1.p_xml,
       t1.r,
       t1.r_xml,
       t1.bp,
       t1.bp_xml,
       t1.cvp,
       t1.cvp_xml,
       t1.bloodsugar,
       t1.bloodsugarxml,
       t1.consciousness,
       t1.consciousnessxml,
       t1.pupilsizeleft,
       t1.pupilsizeleftxml,
       t1.pupilsizeright,
       t1.pupilsizerightxml,
       t1.reflectleft,
       t1.reflectleftxml,
       t1.reflectright,
       t1.reflectrightxml,
       t1.drugname,
       t1.drugnamexml,
       t1.drugdosage,
       t1.drugdosagexml,
       t1.stomachdirection,
       t1.stomachdirectionxml,
       t1.stomachproperty,
       t1.stomachpropertyxml,
       t1.stomachquantity,
       t1.stomachquantityxml,
       t1.inoral,
       t1.inoralxml,
       t1.peedirection,
       t1.peedirectionxml,
       t1.peeproperty,
       t1.peepropertyxml,
       t1.peequantity,
       t1.peequantityxml,
       t1.defecateproperty,
       t1.defecatepropertyxml,
       t1.defecatequantity,
       t1.defecatequantityxml,
       t1.leaddirection,
       t1.leaddirectionxml,
       t1.leadproperty,
       t1.leadpropertyxml,
       t1.leadquantity,
       t1.leadquantityxml,
       t1.sputumproperty,
       t1.sputumpropertyxml,
       t1.sputumquantity,
       t1.sputumquantityxml,
       t1.skin,
       t1.skinxml,
       t1.casehistory,
       t1.casehistoryxml,
       t1.hr,
       t1.hr_xml,
       t1.bp2,
       t1.bp2_xml,
       t1.power,
       t1.powerxml,
       t1.stomachpipe,
       t1.stomachpipexml,
       t1.inoraltype,
       t1.inoraltypexml,
       t1.inoralproperty,
       t1.inoralpropertyxml,
       t1.inoralquantity,
       t1.inoralquantityxml,
       t1.transfusiontotal,
       t1.transfusiontotalxml,
       t1.takefoodtotal,
       t1.takefoodtotalxml,
       t1.leadpipe,
       t1.leadpipexml,
       t1.defecatetimes,
       t1.defecatetimesxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.machinemode_last,
       t2.breathsoundleft_last,
       t2.breathsoundright_last,
       t2.t_last,
       t2.p_last,
       t2.r_last,
       t2.bp_last,
       t2.cvp_last,
       t2.bloodsugar_last,
       t2.consciousness_last,
       t2.pupilsizeleft_last,
       t2.pupilsizeright_last,
       t2.reflectleft_last,
       t2.reflectright_last,
       t2.stomachdirection_last,
       t2.stomachproperty_last,
       t2.stomachquantity_last,
       t2.peedirection_last,
       t2.peeproperty_last,
       t2.peequantity_last,
       t2.defecateproperty_last,
       t2.defecatequantity_last,
       t2.leaddirection_last,
       t2.leadproperty_last,
       t2.leadquantity_last,
       t2.sputumproperty_last,
       t2.sputumquantity_last,
       t2.skin_last,
       t2.casehistory_last,
       t2.hr_last,
       t2.bp2_last,
       t2.power_last,
       t2.stomachpipe_last,
       t2.transfusiontotal_last,
       t2.takefoodtotal_last,
       t2.leadpipe_last,
       t2.defecatetimes_last
  from icuintensivetend t1, icuintensivetendcontent t2
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
       t1.t,
       t1.t_xml,
       t1.p,
       t1.p_xml,
       t1.r,
       t1.r_xml,
       t1.bp,
       t1.bp_xml,
       t1.cvp,
       t1.cvp_xml,
       t1.bloodsugar,
       t1.bloodsugarxml,
       t1.consciousness,
       t1.consciousnessxml,
       t1.pupilsizeleft,
       t1.pupilsizeleftxml,
       t1.pupilsizeright,
       t1.pupilsizerightxml,
       t1.reflectleft,
       t1.reflectleftxml,
       t1.reflectright,
       t1.reflectrightxml,
       t1.drugname,
       t1.drugnamexml,
       t1.drugdosage,
       t1.drugdosagexml,
       t1.stomachdirection,
       t1.stomachdirectionxml,
       t1.stomachproperty,
       t1.stomachpropertyxml,
       t1.stomachquantity,
       t1.stomachquantityxml,
       t1.inoral,
       t1.inoralxml,
       t1.peedirection,
       t1.peedirectionxml,
       t1.peeproperty,
       t1.peepropertyxml,
       t1.peequantity,
       t1.peequantityxml,
       t1.defecateproperty,
       t1.defecatepropertyxml,
       t1.defecatequantity,
       t1.defecatequantityxml,
       t1.leaddirection,
       t1.leaddirectionxml,
       t1.leadproperty,
       t1.leadpropertyxml,
       t1.leadquantity,
       t1.leadquantityxml,
       t1.sputumproperty,
       t1.sputumpropertyxml,
       t1.sputumquantity,
       t1.sputumquantityxml,
       t1.skin,
       t1.skinxml,
       t1.casehistory,
       t1.casehistoryxml,
       t1.hr,
       t1.hr_xml,
       t1.bp2,
       t1.bp2_xml,
       t1.power,
       t1.powerxml,
       t1.stomachpipe,
       t1.stomachpipexml,
       t1.inoraltype,
       t1.inoraltypexml,
       t1.inoralproperty,
       t1.inoralpropertyxml,
       t1.inoralquantity,
       t1.inoralquantityxml,
       t1.transfusiontotal,
       t1.transfusiontotalxml,
       t1.takefoodtotal,
       t1.takefoodtotalxml,
       t1.leadpipe,
       t1.leadpipexml,
       t1.defecatetimes,
       t1.defecatetimesxml,
       t2.modifydate,
       t2.modifyuserid,
       t2.machinemode_last,
       t2.breathsoundleft_last,
       t2.breathsoundright_last,
       t2.t_last,
       t2.p_last,
       t2.r_last,
       t2.bp_last,
       t2.cvp_last,
       t2.bloodsugar_last,
       t2.consciousness_last,
       t2.pupilsizeleft_last,
       t2.pupilsizeright_last,
       t2.reflectleft_last,
       t2.reflectright_last,
       t2.stomachdirection_last,
       t2.stomachproperty_last,
       t2.stomachquantity_last,
       t2.peedirection_last,
       t2.peeproperty_last,
       t2.peequantity_last,
       t2.defecateproperty_last,
       t2.defecatequantity_last,
       t2.leaddirection_last,
       t2.leadproperty_last,
       t2.leadquantity_last,
       t2.sputumproperty_last,
       t2.sputumquantity_last,
       t2.skin_last,
       t2.casehistory_last,
       t2.hr_last,
       t2.bp2_last,
       t2.power_last,
       t2.stomachpipe_last,
       t2.transfusiontotal_last,
       t2.takefoodtotal_last,
       t2.leadpipe_last,
       t2.defecatetimes_last
  from icuintensivetend t1, icuintensivetendcontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.createdate, t2.modifydate";

//		/// <summary>
//		///  从WatchItemRecordContent获取指定表单的最后修改时间。
//		/// </summary>
//		private const string c_strCheckLastModifyRecordSQL=@"select * from(select T2.ModifyDate,T2.ModifyUserID FROM ICUIntensiveTend T1,ICUIntensiveTendContent T2
//															WHERE T1.InPatientID = T2.InPatientID AND T1.InPatientDate = T2.InPatientDate
//															AND T1.OpenDate = T2.OpenDate AND T1.Status =0
//															AND T1.InPatientID = ? AND T1.InPatientDate = ? AND T1.OpenDate = ? ORDER BY T2.ModifyDate DESC)where rownum = 1";

		private const string c_strGetModifyRecordSQL = "";

		/// <summary>
		///  从WatchItemRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from icuintensivetend where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		/// <summary>
		///  从WatchItemRecord删除表单的主要信息。
		/// </summary>
		private const string c_strDeleteRecordSQL="update icuintensivetend set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";
		#region 
//		private const string c_strGetSummarySQL=@"SELECT CreateDate_Date,SUM(TransfusionTotal_Last) AS Transfusion_Total,
//SUM(TakeFoodTotal_Last) AS TakeFood_Total,
//SUM(PeeQuantity_Last) AS OutPeeQuantity_Total,
//SUM(DefecateQuantity_Last) AS OutDefecateQuantity_Total,
//SUM(LeadQuantity_Last) AS OutLeadQuantity_Total,
//SUM(Total_In) AS Total_In,
//SUM(Total_Out) AS Total_Out FROM 
//(SELECT
//case 
//when V2.createdate between convert(datetime,convert(char(10),V2.createdate,120)+' 07:00:00') and convert(datetime,convert(char(10),V2.createdate,120)+' 23:59:59') then convert(char(10),V2.createdate,120)
//when V2.createdate between convert(datetime,convert(char(10),V2.createdate,120)+' 00:00:00') and convert(datetime,convert(char(10),V2.createdate,120)+' 06:59:59') then convert(char(10),dateadd(day,-1,V2.createdate),120)
//end as createdate_date,
//V2.CreateDate,
//CONVERT(float,V2.TransfusionTotal_Last) AS TransfusionTotal_Last,
//CONVERT(float,V2.TakeFoodTotal_Last) AS TakeFoodTotal_Last,
//CONVERT(float,V2.PeeQuantity_Last) AS PeeQuantity_Last,
//CONVERT(float,V2.DefecateQuantity_Last) AS DefecateQuantity_Last,
//CONVERT(float,V2.LeadQuantity_Last) AS LeadQuantity_Last,
//(CONVERT(float,V2.TransfusionTotal_Last)  +
//CONVERT(float,V2.TakeFoodTotal_Last)) AS Total_In,(CONVERT(float,V2.PeeQuantity_Last) +
//CONVERT(float,V2.DefecateQuantity_Last) +
//CONVERT(float,V2.LeadQuantity_Last)) AS Total_Out FROM (SELECT OpenDate,MAX(ModifyDate) AS LastModifyDate 
//FROM ICUIntensiveTendContent Where trim(InPatientID) = ? AND InPatientDate = ?
//GROUP BY OpenDate) AS V1,
//(SELECT T1.CreateDate,T2.* FROM ICUIntensiveTend AS T1,
//ICUIntensiveTendContent AS T2
//WHERE trim(T1.InPatientID) = ? AND T1.InPatientDate = ?
//AND T1.InPatientID = T2.InPatientID AND T1.InPatientDate = T2.InPatientDate
//AND T1.OpenDate = T2.OpenDate AND Status =0)
//AS V2
//WHERE trim(V2.InPatientID) = ? 
//AND V2.InPatientDate = ? AND V1.OpenDate = V2.OpenDate
//AND V1.LastModifyDate = V2.ModifyDate) AS V3
//GROUP BY CreateDate_Date
//ORDER BY CreateDate_Date";
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsICUIntensiveTrackService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null || p_strInPatientID=="" || p_strInPatientDate==null || p_strInPatientDate=="" ||	p_intRecordTypeArr==null || p_dtmOpenDateArr==null || p_intRecordTypeArr.Length != p_dtmOpenDateArr.Length || p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
		 
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
				long lngEff=0;
				for(int i=0;i<p_dtmOpenDateArr.Length;i++)
				{
					objHRPServ.CreateDatabaseParameter(4,out objDPArr);
					//按顺序给IDataParameter赋值(使用p_dtmOpenDateArr[i]和p_dtmFirstPrintDate)
					//				for(int j2=0;j2<objDPArr.Length;j2++)
					//					objDPArr[j2]=new Oracle.DataAccess.Client.OracleParameter();

                    objDPArr[0].DbType = DbType.DateTime;
					objDPArr[0].Value=p_dtmFirstPrintDate;
                    objDPArr[1].Value = p_strInPatientID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[3].DbType = DbType.DateTime;
					objDPArr[3].Value=p_dtmOpenDateArr[i];
					//执行SQL
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL_Normal, ref lngEff, objDPArr);				
				} 
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
			out clsICUIntensiveTendDataInfo p_objTansDataInfo)
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
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
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
					clsICUIntensiveTendContent objRecordContent= null;
					clsICUIntensiveTendDataInfo objInfo = null;
				
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//生成 clsICUIntensiveTendDataInfo
						objInfo = new clsICUIntensiveTendDataInfo();
						objInfo.m_intFlag = (int)enmRecordsType.ICUIntensiveTend;
						//设置结果到 objInfo.m_objRecordContent
						//					objInfo.m_objRecordContent = objRecordContent;
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date;
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date == dtmOpenDate)
						{		 
							#region 从DataTable.Rows中获取结果    
						
							objRecordContent=new clsICUIntensiveTendContent();
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

							objRecordContent.m_strT_Last=dtbValue.Rows[j]["T_LAST"].ToString();
							objRecordContent.m_strT=dtbValue.Rows[j]["T"].ToString();
							objRecordContent.m_strT_XML=dtbValue.Rows[j]["T_XML"].ToString();

							objRecordContent.m_strP_Last=dtbValue.Rows[j]["P_LAST"].ToString();
							objRecordContent.m_strP=dtbValue.Rows[j]["P"].ToString();
							objRecordContent.m_strP_XML=dtbValue.Rows[j]["P_XML"].ToString();

							objRecordContent.m_strR_Last=dtbValue.Rows[j]["R_LAST"].ToString();
							objRecordContent.m_strR=dtbValue.Rows[j]["R"].ToString();
							objRecordContent.m_strR_XML=dtbValue.Rows[j]["R_XML"].ToString();

							objRecordContent.m_strBp_Last=dtbValue.Rows[j]["BP_LAST"].ToString();
							objRecordContent.m_strBp=dtbValue.Rows[j]["BP"].ToString();
							objRecordContent.m_strBp_XML=dtbValue.Rows[j]["BP_XML"].ToString();

							objRecordContent.m_strCVP_Last=dtbValue.Rows[j]["CVP_LAST"].ToString();
							objRecordContent.m_strCVP=dtbValue.Rows[j]["CVP"].ToString();
							objRecordContent.m_strCVP_XML=dtbValue.Rows[j]["CVP_XML"].ToString();

							objRecordContent.m_strBloodSugar_Last=dtbValue.Rows[j]["BLOODSUGAR_LAST"].ToString();
							objRecordContent.m_strBloodSugar=dtbValue.Rows[j]["BLOODSUGAR"].ToString();
							objRecordContent.m_strBloodSugarXML=dtbValue.Rows[j]["BLOODSUGARXML"].ToString();

							objRecordContent.m_strConsciousness_Last=dtbValue.Rows[j]["CONSCIOUSNESS_LAST"].ToString();
							objRecordContent.m_strConsciousness=dtbValue.Rows[j]["CONSCIOUSNESS"].ToString();
							objRecordContent.m_strConsciousnessXML=dtbValue.Rows[j]["CONSCIOUSNESSXML"].ToString();

							objRecordContent.m_strPupilSizeLeft_Last=dtbValue.Rows[j]["PUPILSIZELEFT_LAST"].ToString();
							objRecordContent.m_strPupilSizeLeft=dtbValue.Rows[j]["PUPILSIZELEFT"].ToString();
							objRecordContent.m_strPupilSizeLeftXML=dtbValue.Rows[j]["PUPILSIZELEFTXML"].ToString();

							objRecordContent.m_strPupilSizeRight_Last=dtbValue.Rows[j]["PUPILSIZERIGHT_LAST"].ToString();
							objRecordContent.m_strPupilSizeRight=dtbValue.Rows[j]["PUPILSIZERIGHT"].ToString();
							objRecordContent.m_strPupilSizeRightXML=dtbValue.Rows[j]["PUPILSIZERIGHTXML"].ToString();

							objRecordContent.m_strReflectLeft_Last=dtbValue.Rows[j]["REFLECTLEFT_LAST"].ToString();
							objRecordContent.m_strReflectLeft=dtbValue.Rows[j]["REFLECTLEFT"].ToString();
							objRecordContent.m_strReflectLeftXML=dtbValue.Rows[j]["REFLECTLEFTXML"].ToString();

							objRecordContent.m_strReflectRight_Last=dtbValue.Rows[j]["REFLECTRIGHT_LAST"].ToString();
							objRecordContent.m_strReflectRight=dtbValue.Rows[j]["REFLECTRIGHT"].ToString();
							objRecordContent.m_strReflectRightXML=dtbValue.Rows[j]["REFLECTRIGHTXML"].ToString();
						
							objRecordContent.m_strDrugName=dtbValue.Rows[j]["DRUGNAME"].ToString();
							objRecordContent.m_strDrugNameXML=dtbValue.Rows[j]["DRUGNAMEXML"].ToString();

							objRecordContent.m_strDrugDosage=dtbValue.Rows[j]["DRUGDOSAGE"].ToString();
							objRecordContent.m_strDrugDosageXML=dtbValue.Rows[j]["DRUGDOSAGEXML"].ToString();

							objRecordContent.m_strStomachDirection_Last=dtbValue.Rows[j]["STOMACHDIRECTION_LAST"].ToString();
							objRecordContent.m_strStomachDirection=dtbValue.Rows[j]["STOMACHDIRECTION"].ToString();
							objRecordContent.m_strStomachDirectionXML=dtbValue.Rows[j]["STOMACHDIRECTIONXML"].ToString();

							objRecordContent.m_strStomachProperty_Last=dtbValue.Rows[j]["STOMACHPROPERTY_LAST"].ToString();
							objRecordContent.m_strStomachProperty=dtbValue.Rows[j]["STOMACHPROPERTY"].ToString();
							objRecordContent.m_strStomachPropertyXML=dtbValue.Rows[j]["STOMACHPROPERTYXML"].ToString();

							objRecordContent.m_strStomachQuantity_Last=dtbValue.Rows[j]["STOMACHQUANTITY_LAST"].ToString();
							objRecordContent.m_strStomachQuantity=dtbValue.Rows[j]["STOMACHQUANTITY"].ToString();
							objRecordContent.m_strStomachQuantityXML=dtbValue.Rows[j]["STOMACHQUANTITYXML"].ToString();
						
							objRecordContent.m_strInOral=dtbValue.Rows[j]["INORAL"].ToString();
							objRecordContent.m_strInOralXML=dtbValue.Rows[j]["INORALXML"].ToString();

							objRecordContent.m_strPeeDirection_Last=dtbValue.Rows[j]["PEEDIRECTION_LAST"].ToString();
							objRecordContent.m_strPeeDirection=dtbValue.Rows[j]["PEEDIRECTION"].ToString();
							objRecordContent.m_strPeeDirectionXML=dtbValue.Rows[j]["PEEDIRECTIONXML"].ToString();

							objRecordContent.m_strPeeProperty_Last=dtbValue.Rows[j]["PEEPROPERTY_LAST"].ToString();
							objRecordContent.m_strPeeProperty=dtbValue.Rows[j]["PEEPROPERTY"].ToString();
							objRecordContent.m_strPeePropertyXML=dtbValue.Rows[j]["PEEPROPERTYXML"].ToString();

							objRecordContent.m_strPeeQuantity_Last=dtbValue.Rows[j]["PEEQUANTITY_LAST"].ToString();
							objRecordContent.m_strPeeQuantity=dtbValue.Rows[j]["PEEQUANTITY"].ToString();
							objRecordContent.m_strPeeQuantityXML=dtbValue.Rows[j]["PEEQUANTITYXML"].ToString();

							objRecordContent.m_strDefecateProperty_Last=dtbValue.Rows[j]["DEFECATEPROPERTY_LAST"].ToString();
							objRecordContent.m_strDefecateProperty=dtbValue.Rows[j]["DEFECATEPROPERTY"].ToString();
							objRecordContent.m_strDefecatePropertyXML=dtbValue.Rows[j]["DEFECATEPROPERTYXML"].ToString();

							objRecordContent.m_strDefecateQuantity_Last=dtbValue.Rows[j]["DEFECATEQUANTITY_LAST"].ToString();
							objRecordContent.m_strDefecateQuantity=dtbValue.Rows[j]["DEFECATEQUANTITY"].ToString();
							objRecordContent.m_strDefecateQuantityXML=dtbValue.Rows[j]["DEFECATEQUANTITYXML"].ToString();

							objRecordContent.m_strLeadDirection_Last=dtbValue.Rows[j]["LEADDIRECTION_LAST"].ToString();
							objRecordContent.m_strLeadDirection=dtbValue.Rows[j]["LEADDIRECTION"].ToString();
							objRecordContent.m_strLeadDirectionXML=dtbValue.Rows[j]["LEADDIRECTIONXML"].ToString();

							objRecordContent.m_strLeadProperty_Last=dtbValue.Rows[j]["LEADPROPERTY_LAST"].ToString();
							objRecordContent.m_strLeadProperty=dtbValue.Rows[j]["LEADPROPERTY"].ToString();
							objRecordContent.m_strLeadPropertyXML=dtbValue.Rows[j]["LEADPROPERTYXML"].ToString();

							objRecordContent.m_strLeadQuantity_Last=dtbValue.Rows[j]["LEADQUANTITY_LAST"].ToString();
							objRecordContent.m_strLeadQuantity=dtbValue.Rows[j]["LEADQUANTITY"].ToString();
							objRecordContent.m_strLeadQuantityXML=dtbValue.Rows[j]["LEADQUANTITYXML"].ToString();

							objRecordContent.m_strSputumProperty_Last=dtbValue.Rows[j]["SPUTUMPROPERTY_LAST"].ToString();
							objRecordContent.m_strSputumProperty=dtbValue.Rows[j]["SPUTUMPROPERTY"].ToString();
							objRecordContent.m_strSputumPropertyXML=dtbValue.Rows[j]["SPUTUMPROPERTYXML"].ToString();

							objRecordContent.m_strSputumQuantity_Last=dtbValue.Rows[j]["SPUTUMQUANTITY_LAST"].ToString();
							objRecordContent.m_strSputumQuantity=dtbValue.Rows[j]["SPUTUMQUANTITY"].ToString();
							objRecordContent.m_strSputumQuantityXML=dtbValue.Rows[j]["SPUTUMQUANTITYXML"].ToString();

							objRecordContent.m_strSkin_Last=dtbValue.Rows[j]["SKIN_LAST"].ToString();
							objRecordContent.m_strSkin=dtbValue.Rows[j]["SKIN"].ToString();
							objRecordContent.m_strSkinXML=dtbValue.Rows[j]["SKINXML"].ToString();

							objRecordContent.m_strCaseHistory_Last=dtbValue.Rows[j]["CASEHISTORY_LAST"].ToString();
							objRecordContent.m_strCaseHistory=dtbValue.Rows[j]["CASEHISTORY"].ToString();
							objRecordContent.m_strCaseHistoryXML=dtbValue.Rows[j]["CASEHISTORYXML"].ToString();

							objRecordContent.m_strHR_Last=dtbValue.Rows[j]["HR_LAST"].ToString();
							objRecordContent.m_strHR=dtbValue.Rows[j]["HR"].ToString();
							objRecordContent.m_strHR_XML=dtbValue.Rows[j]["HR_XML"].ToString();

							objRecordContent.m_strBp2_Last=dtbValue.Rows[j]["BP2_LAST"].ToString();
							objRecordContent.m_strBp2=dtbValue.Rows[j]["BP2"].ToString();
							objRecordContent.m_strBp2_XML=dtbValue.Rows[j]["BP2_XML"].ToString();

							objRecordContent.m_strPower_Last=dtbValue.Rows[j]["POWER_LAST"].ToString();
							objRecordContent.m_strPower=dtbValue.Rows[j]["POWER"].ToString();
							objRecordContent.m_strPowerXML=dtbValue.Rows[j]["POWERXML"].ToString();

							objRecordContent.m_strStomachPipe_Last=dtbValue.Rows[j]["STOMACHPIPE_LAST"].ToString();
							objRecordContent.m_strStomachPipe=dtbValue.Rows[j]["STOMACHPIPE"].ToString();
							objRecordContent.m_strStomachPipeXML=dtbValue.Rows[j]["STOMACHPIPEXML"].ToString();
						
							objRecordContent.m_strInOralType=dtbValue.Rows[j]["INORALTYPE"].ToString();
							objRecordContent.m_strInOralTypeXML=dtbValue.Rows[j]["INORALTYPEXML"].ToString();

							objRecordContent.m_strInOralProperty=dtbValue.Rows[j]["INORALPROPERTY"].ToString();
							objRecordContent.m_strInOralPropertyXML=dtbValue.Rows[j]["INORALPROPERTYXML"].ToString();

							objRecordContent.m_strInOralQuantity=dtbValue.Rows[j]["INORALQUANTITY"].ToString();
							objRecordContent.m_strInOralQuantityXML=dtbValue.Rows[j]["INORALQUANTITYXML"].ToString();

							objRecordContent.m_strTransfusionTotal_Last=dtbValue.Rows[j]["TRANSFUSIONTOTAL_LAST"].ToString();
							objRecordContent.m_strTransfusionTotal=dtbValue.Rows[j]["TRANSFUSIONTOTAL"].ToString();
							objRecordContent.m_strTransfusionTotalXML=dtbValue.Rows[j]["TRANSFUSIONTOTALXML"].ToString();

							objRecordContent.m_strTakeFoodTotal_Last=dtbValue.Rows[j]["TAKEFOODTOTAL_LAST"].ToString();
							objRecordContent.m_strTakeFoodTotal=dtbValue.Rows[j]["TAKEFOODTOTAL"].ToString();
							objRecordContent.m_strTakeFoodTotalXML=dtbValue.Rows[j]["TAKEFOODTOTALXML"].ToString();

							objRecordContent.m_strLeadPipe_Last=dtbValue.Rows[j]["LEADPIPE_LAST"].ToString();
							objRecordContent.m_strLeadPipe=dtbValue.Rows[j]["LEADPIPE"].ToString();
							objRecordContent.m_strLeadPipeXML=dtbValue.Rows[j]["LEADPIPEXML"].ToString();
		
							objRecordContent.m_strDefecateTimes_Last=dtbValue.Rows[j]["DEFECATETIMES_LAST"].ToString();
							objRecordContent.m_strDefecateTimes=dtbValue.Rows[j]["DEFECATETIMES"].ToString();
							objRecordContent.m_strDefecateTimesXML=dtbValue.Rows[j]["DEFECATETIMESXML"].ToString();


							//同一条记录的修改,保存在arlModifyData 
							arlModifyData.Add(objRecordContent);
							j++;
							#endregion
						}       
						//后移一条记录，使循环从新的OpenData开始。
						j--;
				
						objInfo.m_objTransDataArr = (clsICUIntensiveTendContent[])arlModifyData.ToArray(typeof(clsICUIntensiveTendContent));
						arlModifyData.Clear();
			
						//最后一条记录
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
				
						arlTransData.Add(objInfo);
					}
				}
				//返回结果到p_objTansDataInfo
				p_objTansDataInfo = ((clsICUIntensiveTendDataInfo[])arlTransData.ToArray(typeof(clsICUIntensiveTendDataInfo)))[0];
		
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objWatchItemInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objICUIntensiveTendInfoArr)
		{
			p_objICUIntensiveTendInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			ArrayList arlTransData = new ArrayList();  
			ArrayList arlModifyData = new ArrayList();
			ArrayList arlTransDataClone = new ArrayList();
			clsICUIntensiveTendDataInfo objAppendInfo = null;
			DateTime dtmOpenDate;
			DateTime dtmCreateDate_Date;
			
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
				//按顺序给IDataParameter赋值
				//			for(int i=0;i<objDPArr.Length;i++)
				//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
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
					clsICUIntensiveTendContent objRecordContent= null;
					clsICUIntensiveTendDataInfo objInfo = null;
					//m_objSummaryArr拿到的是每日的统计数据
					clsICUIntensiveTendSummary[] m_objSummaryArr;
					long m_lngRes = m_lngGetSummaryRecords(p_strInPatientID,p_strInPatientDate,p_objHRPServ,out m_objSummaryArr);
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//生成 clsICUIntensiveTendDataInfo
						objInfo = new clsICUIntensiveTendDataInfo();
						//m_intFlag用来标识这条记录的类型
						objInfo.m_intFlag = (int)enmRecordsType.ICUIntensiveTend;//因为可肯定为危重症特护记录，所以可设任意值
						//设置结果到 objInfo.m_objRecordContent
						//objInfo.m_objRecordContent = objRecordContent;
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate

						dtmOpenDate  = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());

						//如果是同一条记录的修改，也就是OpenDate相同的多条记录
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()) == dtmOpenDate)
						{
							#region 从DataTable.Rows中获取结果  
							objRecordContent=new clsICUIntensiveTendContent();
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
							objRecordContent.m_strBreathSoundRight_Last=dtbValue.Rows[j]["BREATHSOUNDRIGHT"].ToString();
							objRecordContent.m_strBreathSoundRightXML=dtbValue.Rows[j]["BREATHSOUNDRIGHTXML"].ToString();

							objRecordContent.m_strT_Last=dtbValue.Rows[j]["T_LAST"].ToString();
							objRecordContent.m_strT=dtbValue.Rows[j]["T"].ToString();
							objRecordContent.m_strT_XML=dtbValue.Rows[j]["T_XML"].ToString();

							objRecordContent.m_strP_Last=dtbValue.Rows[j]["P_LAST"].ToString();
							objRecordContent.m_strP=dtbValue.Rows[j]["P"].ToString();
							objRecordContent.m_strP_XML=dtbValue.Rows[j]["P_XML"].ToString();

							objRecordContent.m_strR_Last=dtbValue.Rows[j]["R_LAST"].ToString();
							objRecordContent.m_strR=dtbValue.Rows[j]["R"].ToString();
							objRecordContent.m_strR_XML=dtbValue.Rows[j]["R_XML"].ToString();

							objRecordContent.m_strBp_Last=dtbValue.Rows[j]["BP_LAST"].ToString();
							objRecordContent.m_strBp=dtbValue.Rows[j]["BP"].ToString();
							objRecordContent.m_strBp_XML=dtbValue.Rows[j]["BP_XML"].ToString();

							objRecordContent.m_strCVP_Last=dtbValue.Rows[j]["CVP_LAST"].ToString();
							objRecordContent.m_strCVP=dtbValue.Rows[j]["CVP"].ToString();
							objRecordContent.m_strCVP_XML=dtbValue.Rows[j]["CVP_XML"].ToString();

							objRecordContent.m_strBloodSugar_Last=dtbValue.Rows[j]["BLOODSUGAR_LAST"].ToString();
							objRecordContent.m_strBloodSugar=dtbValue.Rows[j]["BLOODSUGAR"].ToString();
							objRecordContent.m_strBloodSugarXML=dtbValue.Rows[j]["BLOODSUGARXML"].ToString();

							objRecordContent.m_strConsciousness_Last=dtbValue.Rows[j]["CONSCIOUSNESS_LAST"].ToString();
							objRecordContent.m_strConsciousness=dtbValue.Rows[j]["CONSCIOUSNESS"].ToString();
							objRecordContent.m_strConsciousnessXML=dtbValue.Rows[j]["CONSCIOUSNESSXML"].ToString();

							objRecordContent.m_strPupilSizeLeft_Last=dtbValue.Rows[j]["PUPILSIZELEFT_LAST"].ToString();
							objRecordContent.m_strPupilSizeLeft=dtbValue.Rows[j]["PUPILSIZELEFT"].ToString();
							objRecordContent.m_strPupilSizeLeftXML=dtbValue.Rows[j]["PUPILSIZELEFTXML"].ToString();

							objRecordContent.m_strPupilSizeRight_Last=dtbValue.Rows[j]["PUPILSIZERIGHT_LAST"].ToString();
							objRecordContent.m_strPupilSizeRight=dtbValue.Rows[j]["PUPILSIZERIGHT"].ToString();
							objRecordContent.m_strPupilSizeRightXML=dtbValue.Rows[j]["PUPILSIZERIGHTXML"].ToString();

							objRecordContent.m_strReflectLeft_Last=dtbValue.Rows[j]["REFLECTLEFT_LAST"].ToString();
							objRecordContent.m_strReflectLeft=dtbValue.Rows[j]["REFLECTLEFT"].ToString();
							objRecordContent.m_strReflectLeftXML=dtbValue.Rows[j]["REFLECTLEFTXML"].ToString();

							objRecordContent.m_strReflectRight_Last=dtbValue.Rows[j]["REFLECTRIGHT_LAST"].ToString();
							objRecordContent.m_strReflectRight=dtbValue.Rows[j]["REFLECTRIGHT"].ToString();
							objRecordContent.m_strReflectRightXML=dtbValue.Rows[j]["REFLECTRIGHTXML"].ToString();
						
							objRecordContent.m_strDrugName=dtbValue.Rows[j]["DRUGNAME"].ToString();
							objRecordContent.m_strDrugNameXML=dtbValue.Rows[j]["DRUGNAMEXML"].ToString();

							objRecordContent.m_strDrugDosage=dtbValue.Rows[j]["DRUGDOSAGE"].ToString();
							objRecordContent.m_strDrugDosageXML=dtbValue.Rows[j]["DRUGDOSAGEXML"].ToString();

							objRecordContent.m_strStomachDirection_Last=dtbValue.Rows[j]["STOMACHDIRECTION_LAST"].ToString();
							objRecordContent.m_strStomachDirection=dtbValue.Rows[j]["STOMACHDIRECTION"].ToString();
							objRecordContent.m_strStomachDirectionXML=dtbValue.Rows[j]["STOMACHDIRECTIONXML"].ToString();

							objRecordContent.m_strStomachProperty_Last=dtbValue.Rows[j]["STOMACHPROPERTY_LAST"].ToString();
							objRecordContent.m_strStomachProperty=dtbValue.Rows[j]["STOMACHPROPERTY"].ToString();
							objRecordContent.m_strStomachPropertyXML=dtbValue.Rows[j]["STOMACHPROPERTYXML"].ToString();

							objRecordContent.m_strStomachQuantity_Last=dtbValue.Rows[j]["STOMACHQUANTITY_LAST"].ToString();
							objRecordContent.m_strStomachQuantity=dtbValue.Rows[j]["STOMACHQUANTITY"].ToString();
							objRecordContent.m_strStomachQuantityXML=dtbValue.Rows[j]["STOMACHQUANTITYXML"].ToString();
					
							objRecordContent.m_strInOral=dtbValue.Rows[j]["INORAL"].ToString();
							objRecordContent.m_strInOralXML=dtbValue.Rows[j]["INORALXML"].ToString();

							objRecordContent.m_strPeeDirection_Last=dtbValue.Rows[j]["PEEDIRECTION_LAST"].ToString();
							objRecordContent.m_strPeeDirection=dtbValue.Rows[j]["PEEDIRECTION"].ToString();
							objRecordContent.m_strPeeDirectionXML=dtbValue.Rows[j]["PEEDIRECTIONXML"].ToString();

							objRecordContent.m_strPeeProperty_Last=dtbValue.Rows[j]["PEEPROPERTY_LAST"].ToString();
							objRecordContent.m_strPeeProperty=dtbValue.Rows[j]["PEEPROPERTY"].ToString();
							objRecordContent.m_strPeePropertyXML=dtbValue.Rows[j]["PEEPROPERTYXML"].ToString();

							objRecordContent.m_strPeeQuantity_Last=dtbValue.Rows[j]["PEEQUANTITY_LAST"].ToString();
							objRecordContent.m_strPeeQuantity=dtbValue.Rows[j]["PEEQUANTITY"].ToString();
							objRecordContent.m_strPeeQuantityXML=dtbValue.Rows[j]["PEEQUANTITYXML"].ToString();

							objRecordContent.m_strDefecateProperty_Last=dtbValue.Rows[j]["DEFECATEPROPERTY_LAST"].ToString();
							objRecordContent.m_strDefecateProperty=dtbValue.Rows[j]["DEFECATEPROPERTY"].ToString();
							objRecordContent.m_strDefecatePropertyXML=dtbValue.Rows[j]["DEFECATEPROPERTYXML"].ToString();

							objRecordContent.m_strDefecateQuantity_Last=dtbValue.Rows[j]["DEFECATEQUANTITY_LAST"].ToString();
							objRecordContent.m_strDefecateQuantity=dtbValue.Rows[j]["DEFECATEQUANTITY"].ToString();
							objRecordContent.m_strDefecateQuantityXML=dtbValue.Rows[j]["DEFECATEQUANTITYXML"].ToString();

							objRecordContent.m_strLeadDirection_Last=dtbValue.Rows[j]["LEADDIRECTION_LAST"].ToString();
							objRecordContent.m_strLeadDirection=dtbValue.Rows[j]["LEADDIRECTION"].ToString();
							objRecordContent.m_strLeadDirectionXML=dtbValue.Rows[j]["LEADDIRECTIONXML"].ToString();

							objRecordContent.m_strLeadProperty_Last=dtbValue.Rows[j]["LEADPROPERTY_LAST"].ToString();
							objRecordContent.m_strLeadProperty=dtbValue.Rows[j]["LEADPROPERTY"].ToString();
							objRecordContent.m_strLeadPropertyXML=dtbValue.Rows[j]["LEADPROPERTYXML"].ToString();

							objRecordContent.m_strLeadQuantity_Last=dtbValue.Rows[j]["LEADQUANTITY_LAST"].ToString();
							objRecordContent.m_strLeadQuantity=dtbValue.Rows[j]["LEADQUANTITY"].ToString();
							objRecordContent.m_strLeadQuantityXML=dtbValue.Rows[j]["LEADQUANTITYXML"].ToString();

							objRecordContent.m_strSputumProperty_Last=dtbValue.Rows[j]["SPUTUMPROPERTY_LAST"].ToString();
							objRecordContent.m_strSputumProperty=dtbValue.Rows[j]["SPUTUMPROPERTY"].ToString();
							objRecordContent.m_strSputumPropertyXML=dtbValue.Rows[j]["SPUTUMPROPERTYXML"].ToString();

							objRecordContent.m_strSputumQuantity_Last=dtbValue.Rows[j]["SPUTUMQUANTITY_LAST"].ToString();
							objRecordContent.m_strSputumQuantity=dtbValue.Rows[j]["SPUTUMQUANTITY"].ToString();
							objRecordContent.m_strSputumQuantityXML=dtbValue.Rows[j]["SPUTUMQUANTITYXML"].ToString();

							objRecordContent.m_strSkin_Last=dtbValue.Rows[j]["SKIN_LAST"].ToString();
							objRecordContent.m_strSkin=dtbValue.Rows[j]["SKIN"].ToString();
							objRecordContent.m_strSkinXML=dtbValue.Rows[j]["SKINXML"].ToString();

							objRecordContent.m_strCaseHistory_Last=dtbValue.Rows[j]["CASEHISTORY_LAST"].ToString();
							objRecordContent.m_strCaseHistory=dtbValue.Rows[j]["CASEHISTORY"].ToString();
							objRecordContent.m_strCaseHistoryXML=dtbValue.Rows[j]["CASEHISTORYXML"].ToString();

							objRecordContent.m_strHR_Last=dtbValue.Rows[j]["HR_LAST"].ToString();
							objRecordContent.m_strHR=dtbValue.Rows[j]["HR"].ToString();
							objRecordContent.m_strHR_XML=dtbValue.Rows[j]["HR_XML"].ToString();

							objRecordContent.m_strBp2_Last=dtbValue.Rows[j]["BP2_LAST"].ToString();
							objRecordContent.m_strBp2=dtbValue.Rows[j]["BP2"].ToString();
							objRecordContent.m_strBp2_XML=dtbValue.Rows[j]["BP2_XML"].ToString();

							objRecordContent.m_strPower_Last=dtbValue.Rows[j]["POWER_LAST"].ToString();
							objRecordContent.m_strPower=dtbValue.Rows[j]["POWER"].ToString();
							objRecordContent.m_strPowerXML=dtbValue.Rows[j]["POWERXML"].ToString();

							objRecordContent.m_strStomachPipe_Last=dtbValue.Rows[j]["STOMACHPIPE_LAST"].ToString();
							objRecordContent.m_strStomachPipe=dtbValue.Rows[j]["STOMACHPIPE"].ToString();
							objRecordContent.m_strStomachPipeXML=dtbValue.Rows[j]["STOMACHPIPEXML"].ToString();
						
							objRecordContent.m_strInOralType=dtbValue.Rows[j]["INORALTYPE"].ToString();
							objRecordContent.m_strInOralTypeXML=dtbValue.Rows[j]["INORALTYPEXML"].ToString();

							objRecordContent.m_strInOralProperty=dtbValue.Rows[j]["INORALPROPERTY"].ToString();
							objRecordContent.m_strInOralPropertyXML=dtbValue.Rows[j]["INORALPROPERTYXML"].ToString();

							objRecordContent.m_strInOralQuantity=dtbValue.Rows[j]["INORALQUANTITY"].ToString();
							objRecordContent.m_strInOralQuantityXML=dtbValue.Rows[j]["INORALQUANTITYXML"].ToString();

							objRecordContent.m_strTransfusionTotal_Last=dtbValue.Rows[j]["TRANSFUSIONTOTAL_LAST"].ToString();
							objRecordContent.m_strTransfusionTotal=dtbValue.Rows[j]["TRANSFUSIONTOTAL"].ToString();
							objRecordContent.m_strTransfusionTotalXML=dtbValue.Rows[j]["TRANSFUSIONTOTALXML"].ToString();

							objRecordContent.m_strTakeFoodTotal_Last=dtbValue.Rows[j]["TAKEFOODTOTAL_LAST"].ToString();
							objRecordContent.m_strTakeFoodTotal=dtbValue.Rows[j]["TAKEFOODTOTAL"].ToString();
							objRecordContent.m_strTakeFoodTotalXML=dtbValue.Rows[j]["TAKEFOODTOTALXML"].ToString();

							objRecordContent.m_strLeadPipe_Last=dtbValue.Rows[j]["LEADPIPE_LAST"].ToString();
							objRecordContent.m_strLeadPipe=dtbValue.Rows[j]["LEADPIPE"].ToString();
							objRecordContent.m_strLeadPipeXML=dtbValue.Rows[j]["LEADPIPEXML"].ToString();
		
							objRecordContent.m_strDefecateTimes_Last=dtbValue.Rows[j]["DEFECATETIMES_LAST"].ToString();
							objRecordContent.m_strDefecateTimes=dtbValue.Rows[j]["DEFECATETIMES"].ToString();
							objRecordContent.m_strDefecateTimesXML=dtbValue.Rows[j]["DEFECATETIMESXML"].ToString();
					
							//同一条记录的修改,保存在arlModifyData 
							arlModifyData.Add(objRecordContent);
							j++;
							#endregion 
						}       
						//后移一条记录，使循环从新的OpenData开始。
						j--;
					
						objInfo.m_objTransDataArr = (clsICUIntensiveTendContent[])arlModifyData.ToArray(typeof(clsICUIntensiveTendContent));
                        int intCount = objInfo.m_objTransDataArr.Length - 1;
                        for (int w1 = 1; w1 < intCount; w1++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
						{
							if(objInfo.m_objTransDataArr[w1-1].m_strModifyUserName == objInfo.m_objTransDataArr[w1].m_strModifyUserName
                                && objInfo.m_objTransDataArr[w1].m_strModifyUserName == objInfo.m_objTransDataArr[w1 + 1].m_strModifyUserName)
								objInfo.m_objTransDataArr[w1-1].m_strModifyUserName = "　";//全角空格字符
						}
						arlModifyData.Clear();
			
						//同一条记录的最后一次修改
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
				
						arlTransData.Add(objInfo);
					} // END for(int j=0;j<dtbValue.Rows.Count;j++)

					#region 统计	
					//如果是七点以前的记录，算前一天的
					if(((clsICUIntensiveTendDataInfo)arlTransData[0]).m_objRecordContent.m_dtmCreateDate.Hour>=7)
					{
						dtmCreateDate_Date = ((clsICUIntensiveTendDataInfo)arlTransData[0]).m_objRecordContent.m_dtmCreateDate.Date;		
					}
					else
					{
						dtmCreateDate_Date = ((clsICUIntensiveTendDataInfo)arlTransData[0]).m_objRecordContent.m_dtmCreateDate.AddDays(-1).Date;
					}
					arlTransDataClone = (ArrayList)arlTransData.Clone();
					if(arlTransData.Count == 1)//只有一条记录时
					{
						//日汇总
						objAppendInfo = new clsICUIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = 0;
						objAppendInfo.m_objRecordContent = new clsICUIntensiveTendContent();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[0].m_strCreateDate + " 23:59:59.998");
						objAppendInfo.m_objICUSummary = m_objSummaryArr[0];
						arlTransDataClone.Add(objAppendInfo);

						//###########统计全部记录总共的内容
						objAppendInfo = new clsICUIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = 0;
						objAppendInfo.m_objRecordContent = new clsICUIntensiveTendContent();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
						objAppendInfo.m_objICUSummary = m_objSummaryArr[0];
						arlTransDataClone.Add(objAppendInfo);
						//#############
					}
					else//多于一条记录时
					{
						try
						{
							int m_intSummaryArrIndex = 0;
							for(int i1=1;i1<arlTransData.Count;i1++)
							{
								//如果是七点钟以前的，算前一天的记录
								DateTime dtmNext = ((clsICUIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate;
								if(dtmNext.Hour<7)
								{
									dtmNext = dtmNext.AddDays(-1).Date; 
								}

								//把当前日期的统计记录的日期和下一条记录的记录日期相比较，如果不同的话就插在当前位置
								if(dtmCreateDate_Date != dtmNext.Date)//((clsICUIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date)
								{
									objAppendInfo = new clsICUIntensiveTendDataInfo();
									objAppendInfo.m_intFlag = 0;
									objAppendInfo.m_objRecordContent = new clsICUIntensiveTendContent();
									objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse((DateTime.Parse(m_objSummaryArr[m_intSummaryArrIndex].m_strCreateDate)).ToString("yyyy-MM-dd") + " 06:59:59").AddDays(1);//DateTime.Parse(m_objSummaryArr[m_intSummaryArrIndex].m_strCreateDate + " 23:59:59.998");
									objAppendInfo.m_objICUSummary = m_objSummaryArr[m_intSummaryArrIndex];
									//insert就是在某个位置插入，add就加在最后
									arlTransDataClone.Insert(i1+m_intSummaryArrIndex,objAppendInfo);

									if(((clsICUIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Hour>=7)
									{
										dtmCreateDate_Date = ((clsICUIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date;
									}
									else
									{
										dtmCreateDate_Date = ((clsICUIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date.AddDays(-1).Date;
									}
									m_intSummaryArrIndex++;
								}
							}
							objAppendInfo = new clsICUIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = 0;
							objAppendInfo.m_objRecordContent = new clsICUIntensiveTendContent();
							objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse((DateTime.Parse(m_objSummaryArr[m_objSummaryArr.Length-1].m_strCreateDate)).ToString("yyyy-MM-dd") + " 06:59:59").AddDays(1);
							objAppendInfo.m_objICUSummary = m_objSummaryArr[m_objSummaryArr.Length-1];
							//						objAppendInfo.m_objICUSummary.m_strCreateDate = DateTime.Parse((DateTime.Parse(m_objSummaryArr[m_objSummaryArr.Length-1].m_strCreateDate)).ToString("yyyy-MM-dd") + " 06:59:59").AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
							arlTransDataClone.Add(objAppendInfo);

							//###########统计全部记录总共的内容
							clsICUIntensiveTendDataInfo objTotalDataInfo;
							clsICUIntensiveTendSummary m_objSummary = new clsICUIntensiveTendSummary();
						
							for(int i1=0;i1<arlTransDataClone.Count;i1++)
							{
								objTotalDataInfo = (clsICUIntensiveTendDataInfo)arlTransDataClone[i1];
								if(objTotalDataInfo.m_intFlag != (int)enmRecordsType.ICUIntensiveTend)
								{
									m_objSummary.m_intTransfusion_Total+=objTotalDataInfo.m_objICUSummary.m_intTransfusion_Total;
									m_objSummary.m_intTakeFood_Total+=objTotalDataInfo.m_objICUSummary.m_intTakeFood_Total;
									m_objSummary.m_intOutPeeQuantity_Total+=objTotalDataInfo.m_objICUSummary.m_intOutPeeQuantity_Total;
									m_objSummary.m_intOutDefecateQuantity_Total+=objTotalDataInfo.m_objICUSummary.m_intOutDefecateQuantity_Total;
									m_objSummary.m_intOutLeadQuantity_Total+=objTotalDataInfo.m_objICUSummary.m_intOutLeadQuantity_Total;
									m_objSummary.m_intTotal_In+=objTotalDataInfo.m_objICUSummary.m_intTotal_In;
									m_objSummary.m_intTotal_Out+=objTotalDataInfo.m_objICUSummary.m_intTotal_Out;								
								}
							}
							objAppendInfo = new clsICUIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = 0;
							objAppendInfo.m_objRecordContent = new clsICUIntensiveTendContent();
							objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
							objAppendInfo.m_objICUSummary = m_objSummary;
							arlTransDataClone.Add(objAppendInfo);
							//#############
						}
						catch(Exception err)
						{
							string m_Str = err.Message + "\r\n" + err.StackTrace;
						}
					}
					#endregion 统计
				}
				//返回结果到p_objTansDataInfoArr
				//如果包括统计的话就返回以下这句
				//			p_objICUIntensiveTendInfoArr = (clsICUIntensiveTendDataInfo[])arlTransDataClone.ToArray(typeof(clsICUIntensiveTendDataInfo));
		
				//以下这句不包括统计
				p_objICUIntensiveTendInfoArr = (clsICUIntensiveTendDataInfo[])arlTransDataClone.ToArray(typeof(clsICUIntensiveTendDataInfo));
                clsDepartmentHandlerService objDept = new clsDepartmentHandlerService();
                string strHospitalName = string.Empty;
                string strHospitalNO = string.Empty;
                long lngGetHospital = objDept.m_lngGetHospitalInfo(out strHospitalName,out strHospitalNO);
                if (strHospitalNO != "450101001")
                {
                    for (int w2 = 0; w2 < p_objICUIntensiveTendInfoArr.Length; w2++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
                    {
                        clsICUIntensiveTendContent[] objTempAInfoArr = ((clsICUIntensiveTendDataInfo)p_objICUIntensiveTendInfoArr[w2]).m_objTransDataArr;
                        if (objTempAInfoArr != null)
                        {
                            for (int w3 = w2 + 1; w3 < p_objICUIntensiveTendInfoArr.Length; w3++)
                            {
                                clsICUIntensiveTendContent[] objTempBInfoArr = ((clsICUIntensiveTendDataInfo)p_objICUIntensiveTendInfoArr[w3]).m_objTransDataArr;
                                if (objTempBInfoArr == null) continue;
                                if (objTempAInfoArr[objTempAInfoArr.Length - 1].m_dtmCreateDate.Date == objTempBInfoArr[0].m_dtmCreateDate.Date)
                                {
                                    string strTempName = "";
                                    for (int w4 = 0; w4 < objTempBInfoArr.Length; w4++)
                                    {
                                        if (objTempBInfoArr[w4].m_strModifyUserName != "　")//全角空格字符
                                        {
                                            strTempName = objTempBInfoArr[w4].m_strModifyUserName;
                                            break;
                                        }
                                    }
                                    if (objTempAInfoArr[objTempAInfoArr.Length - 1].m_strModifyUserName == strTempName)
                                        objTempAInfoArr[objTempAInfoArr.Length - 1].m_strModifyUserName = "　";//全角空格字符
                                    break;
                                }
                                else break;
                            }
                        }
                    }//END for
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
				string c_strCheckLastModifyRecordSQL = "";
				if(clsHRPTableService.bytDatabase_Selector == 2)
				{
                    c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select t2.modifydate, t2.modifyuserid
          from icuintensivetend t1, icuintensivetendcontent t2
         where t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t1.status = 0
           and t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.opendate = ?
         order by t2.modifydate desc)
 where rownum = 1";
				}
				else
				{
					c_strCheckLastModifyRecordSQL=@"select top 1 t2.modifydate,t2.modifyuserid from icuintensivetend t1,icuintensivetendcontent t2
															where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
															and t1.opendate = t2.opendate and t1.status =0
															and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc";
				}
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
				//按顺序给IDataParameter赋值
//				for(int i=0;i<objDPArr.Length;i++)
//					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
				//按顺序给IDataParameter赋值
				//			for(int i=0;i<objDPArr.Length;i++)
				//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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

		#region 统计记录内容
		/// <summary>
		/// 获得统计记录内容
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objSummaryItemInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetSummaryRecords(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsICUIntensiveTendSummary[] p_objSummaryItemInfoArr)
		{
			p_objSummaryItemInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			#region SQL
			string strTempSql = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
				strTempSql = @"case  when v2.createdate between to_date ( substr (to_char (v2.createdate, 'yyyy-mm-dd hh24:mi:ss'
                                                     ),  1,  10  )  || ' 07:00:00',  'yyyy-mm-dd hh24:mi:ss'  )
                               and to_date ( substr (to_char (v2.createdate,  'yyyy-mm-dd hh24:mi:ss'
                                                     ),  1,  10   ) || ' 23:59:59',  'yyyy-mm-dd hh24:mi:ss'  )
                       then substr (to_char (v2.createdate,   'yyyy-mm-dd hh24:mi:ss'  ),  1,   10  )
                    when v2.createdate   between to_date  ( substr (to_char (v2.createdate, 'yyyy-mm-dd hh24:mi:ss'
                                                     ), 1,  10  )  || ' 00:00:00',   'yyyy-mm-dd hh24:mi:ss'  )
                               and to_date ( substr (to_char (v2.createdate,  'yyyy-mm-dd hh24:mi:ss' 
                                                     ), 1,  10  )  || ' 06:59:59',  'yyyy-mm-dd hh24:mi:ss'  )
                       then substr  (to_char  (  to_date  (to_char (v2.createdate,  'yyyy-mm-dd hh24:mi:ss'
                                                     ),  'yyyy-mm-dd hh24:mi:ss'  )  - 1,  'yyyy-mm-dd hh24:mi:ss'
                                  ), 1,   10  )
                 end as createdate_date";
			}
			else
			{
				strTempSql = @"case 
when V2.createdate between convert(datetime,convert(char(10),V2.createdate,120)+' 07:00:00') and convert(datetime,convert(char(10),V2.createdate,120)+' 23:59:59') then convert(char(10),V2.createdate,120)
when V2.createdate between convert(datetime,convert(char(10),V2.createdate,120)+' 00:00:00') and convert(datetime,convert(char(10),V2.createdate,120)+' 06:59:59') then convert(char(10),dateadd(day,-1,V2.createdate),120)
end as createdate_date";
			}
			string c_strGetSummarySQL=@"select   createdate_date, "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (transfusiontotal_last),0) as transfusion_total,
         "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (takefoodtotal_last),0) as takefood_total,
         "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (peequantity_last),0) as outpeequantity_total,
         "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (defecatequantity_last),0) as outdefecatequantity_total,
         "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (leadquantity_last),0) as outleadquantity_total,
         "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (total_in),0) as total_in, "+clsDatabaseSQLConvert.s_StrGetNullFuncName+@"(sum (total_out),0) as total_out
    from (select "+strTempSql+@",
----------------------------------------------
                 v2.createdate,
                 "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.transfusiontotal_last","float")+@" as transfusiontotal_last,
                 "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.takefoodtotal_last","float")+@" as takefoodtotal_last,
                 "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.peequantity_last","float")+@" as peequantity_last,
                 "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.defecatequantity_last","float")+@"  as defecatequantity_last,
                 "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.leadquantity_last","float")+@" as leadquantity_last,
                 (  "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.transfusiontotal_last","float")+@"
                  + "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("takefoodtotal_last","float")+@"
                 ) as total_in,
                 ( "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.peequantity_last","float")+@"
                  + "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.defecatequantity_last","float")+@"
                  + "+ clsDatabaseSQLConvert.s_StrGetTurnNumricFuncName("v2.leadquantity_last","float")+@"
                 ) as total_out
            from (select   opendate, max (modifydate) as lastmodifydate
                      from icuintensivetendcontent
                     where inpatientid = '"+p_strInPatientID.Trim()+@"'
                       and inpatientdate ="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+ @"
                  group by opendate) v1,
                 (select t1.createdate, t2.inpatientid,
       t2.inpatientdate,
       t2.opendate,
       t2.modifydate,
       t2.modifyuserid,
       t2.machinemode_last,
       t2.breathsoundleft_last,
       t2.breathsoundright_last,
       t2.t_last,
       t2.p_last,
       t2.r_last,
       t2.bp_last,
       t2.cvp_last,
       t2.bloodsugar_last,
       t2.consciousness_last,
       t2.pupilsizeleft_last,
       t2.pupilsizeright_last,
       t2.reflectleft_last,
       t2.reflectright_last,
       t2.stomachdirection_last,
       t2.stomachproperty_last,
       t2.stomachquantity_last,
       t2.peedirection_last,
       t2.peeproperty_last,
       t2.peequantity_last,
       t2.defecateproperty_last,
       t2.defecatequantity_last,
       t2.leaddirection_last,
       t2.leadproperty_last,
       t2.leadquantity_last,
       t2.sputumproperty_last,
       t2.sputumquantity_last,
       t2.skin_last,
       t2.casehistory_last,
       t2.hr_last,
       t2.bp2_last,
       t2.power_last,
       t2.stomachpipe_last,
       t2.transfusiontotal_last,
       t2.takefoodtotal_last,
       t2.leadpipe_last,
       t2.defecatetimes_last
                    from icuintensivetend t1, icuintensivetendcontent t2
                   where t1.inpatientid = '" + p_strInPatientID.Trim()+@"'
                     and t1.inpatientdate ="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
                     and t1.inpatientid = t2.inpatientid
                     and t1.inpatientdate = t2.inpatientdate
                     and t1.opendate = t2.opendate
                     and status =0) v2
           where v2.inpatientid ='"+p_strInPatientID.Trim()+@"'
             and v2.inpatientdate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
             and v1.opendate = v2.opendate
             and v1.lastmodifydate = v2.modifydate) v3
group by createdate_date
order by createdate_date";
			#endregion SQL
			//获取IDataParameter数组
//			IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
//			//按顺序给IDataParameter赋值
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
//			objDPArr[0].Value=p_strInPatientID;
//			objDPArr[1].Value=p_strInPatientID;
//			objDPArr[2].Value=p_strInPatientID;

			DataTable dtbValue = new DataTable();
			//执行查询，填充结果到DataTable       
			long lngRes = p_objHRPServ.DoGetDataTable(c_strGetSummarySQL,ref dtbValue);
			//循环DataTable
			if(lngRes > 0 && dtbValue.Rows.Count >0)
			{
				p_objSummaryItemInfoArr = new clsICUIntensiveTendSummary[dtbValue.Rows.Count];
				for(int i1=0;i1<dtbValue.Rows.Count;i1++)
				{
					p_objSummaryItemInfoArr[i1] = new clsICUIntensiveTendSummary();
					p_objSummaryItemInfoArr[i1].m_strCreateDate = dtbValue.Rows[i1]["CREATEDATE_DATE"].ToString();
					p_objSummaryItemInfoArr[i1].m_intTransfusion_Total = Convert.ToInt32(dtbValue.Rows[i1]["TRANSFUSION_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intTakeFood_Total = Convert.ToInt32(dtbValue.Rows[i1]["TAKEFOOD_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intOutPeeQuantity_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTPEEQUANTITY_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intOutDefecateQuantity_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTDEFECATEQUANTITY_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intOutLeadQuantity_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTLEADQUANTITY_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_In = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_IN"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_Out = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_OUT"]);
				}
			}

			return (long)enmOperationResult.DB_Succeed;
		}
		#endregion 统计记录内容
	}
}
