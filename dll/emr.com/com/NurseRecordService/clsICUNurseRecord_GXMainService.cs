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
	public class clsICUNurseRecord_GXMainService : clsRecordsService
	{
		public clsICUNurseRecord_GXMainService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region SQL语句
		private const string c_strUpdateFirstPrintDateSQL=@"update icunurserecord_gxrecord
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
       t1.inamountitem,
       t1.inamountitemxml,
       t1.inamountstandby,
       t1.inamountstandbyxml,
       t1.inamountfact,
       t1.inamountfactxml,
       t1.outemiction,
       t1.outemictionxml,
       t1.temperature,
       t1.temperaturexml,
       t1.hr,
       t1.hrxml,
       t1.respiration,
       t1.respirationxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.a,
       t1.axml,
       t1.sp02,
       t1.sp02xml,
       t1.generalinstance,
       t1.generalinstancexml,
       t1.diseaseid,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom1name,
       t1.custom2name,
       t1.custom3name,
       t1.custom4name,
       t1.summary,
       t1.summaryxml,
       t1.isstat,
       t1.sumin,
       t1.sumout,
       t1.sumintime,
       t1.sumouttime,
       t1.autosumin,
       t1.autosumout,
       t1.startstattime,
       t1.instandbysum,
       t1.autoinstandbysum,
       t1.infactsum,
       t1.autoinfactsum,
       t1.outemictionsum,
       t1.autooutemictionsum,
       t1.outcustom1sum,
       t1.autooutcustom1sum,
       t1.outcustom2sum,
       t1.autooutcustom2sum,
       t3.modifydate,
       t3.modifyuserid,
       t3.temperature_right,
       t3.hr_right,
       t3.respiration_right,
       t3.bloodpressures_right,
       t3.bloodpressurea_right,
       t3.a_right,
       t3.sp02_right,
       t3.generalinstance_right,
       t3.inamountitem_right,
       t3.inamountstandby_right,
       t3.inamountfact_right,
       t3.outemiction_right,
       t3.custom1_right,
       t3.custom2_right,
       t3.summary_right
  from icunurserecord_gxrecord t1, icunurserecord_gxcontent t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate, t3.modifydate";

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
       t1.inamountitem,
       t1.inamountitemxml,
       t1.inamountstandby,
       t1.inamountstandbyxml,
       t1.inamountfact,
       t1.inamountfactxml,
       t1.outemiction,
       t1.outemictionxml,
       t1.temperature,
       t1.temperaturexml,
       t1.hr,
       t1.hrxml,
       t1.respiration,
       t1.respirationxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.a,
       t1.axml,
       t1.sp02,
       t1.sp02xml,
       t1.generalinstance,
       t1.generalinstancexml,
       t1.diseaseid,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom1name,
       t1.custom2name,
       t1.custom3name,
       t1.custom4name,
       t1.summary,
       t1.summaryxml,
       t1.isstat,
       t1.sumin,
       t1.sumout,
       t1.sumintime,
       t1.sumouttime,
       t1.autosumin,
       t1.autosumout,
       t1.startstattime,
       t1.instandbysum,
       t1.autoinstandbysum,
       t1.infactsum,
       t1.autoinfactsum,
       t1.outemictionsum,
       t1.autooutemictionsum,
       t1.outcustom1sum,
       t1.autooutcustom1sum,
       t1.outcustom2sum,
       t1.autooutcustom2sum,
       t3.modifydate,
       t3.modifyuserid,
       t3.temperature_right,
       t3.hr_right,
       t3.respiration_right,
       t3.bloodpressures_right,
       t3.bloodpressurea_right,
       t3.a_right,
       t3.sp02_right,
       t3.generalinstance_right,
       t3.inamountitem_right,
       t3.inamountstandby_right,
       t3.inamountfact_right,
       t3.outemiction_right,
       t3.custom1_right,
       t3.custom2_right,
       t3.summary_right
  from icunurserecord_gxrecord t1, icunurserecord_gxcontent t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.createdate, t3.modifydate";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from icunurserecord_gxrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		private const string c_strDeleteRecordSQL=@"update icunurserecord_gxrecord
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendMainService","m_lngUpdateFirstPrintDate");
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
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordOpenDate,
			out clsICUNurseRecordContentGX[] p_objTansDataInfo)
		{
			
			p_objTansDataInfo=null;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
				clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				ArrayList arlTransData = new ArrayList();  
				ArrayList arlModifyData = new ArrayList();
				DateTime dtmOpenDate;

				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;		
				objDPArr[2].Value=DateTime.Parse(p_strRecordOpenDate);
					
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_objTansDataInfo = new clsICUNurseRecordContentGX[dtbValue.Rows.Count];
					clsICUNurseRecordContentGX objRecordContent= null;
				
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date;
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date == dtmOpenDate)
						{
							#region 从DataTable.Rows中获取结果    
						
							objRecordContent=new clsICUNurseRecordContentGX();
							objRecordContent.m_strInPatientID=p_strInPatientID;
							objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
							objRecordContent.m_dtmOpenDate=DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString());
							objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
							objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
				
							if(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString()=="")
								objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
							else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
							objRecordContent.m_strCreateUserID=dtbValue.Rows[j]["CREATEUSERID"].ToString();
							objRecordContent.m_strCreateUserName = dtbValue.Rows[j]["CreateUserName"].ToString();
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
				
							objRecordContent.m_strTEMPERATURE_RIGHT=dtbValue.Rows[j]["TEMPERATURE_RIGHT"].ToString();
							objRecordContent.m_strTEMPERATURE=dtbValue.Rows[j]["TEMPERATURE"].ToString();
							objRecordContent.m_strTEMPERATUREXML=dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
							objRecordContent.m_strRESPIRATION_RIGHT=dtbValue.Rows[j]["RESPIRATION_RIGHT"].ToString();
							objRecordContent.m_strRESPIRATION=dtbValue.Rows[j]["RESPIRATION"].ToString();
							objRecordContent.m_strRESPIRATIONXML=dtbValue.Rows[j]["RESPIRATIONXML"].ToString();
							objRecordContent.m_strHR_RIGHT=dtbValue.Rows[j]["HR_RIGHT"].ToString();
							objRecordContent.m_strHR=dtbValue.Rows[j]["HR"].ToString();
							objRecordContent.m_strHRXML=dtbValue.Rows[j]["HRXML"].ToString();
							objRecordContent.m_strBLOODPRESSURES_RIGHT=dtbValue.Rows[j]["BLOODPRESSURES_RIGHT"].ToString();
							objRecordContent.m_strBLOODPRESSURES=dtbValue.Rows[j]["BLOODPRESSURES"].ToString();
							objRecordContent.m_strBLOODPRESSURESXML=dtbValue.Rows[j]["BLOODPRESSURESXML"].ToString();
							objRecordContent.m_strBLOODPRESSUREA_RIGHT=dtbValue.Rows[j]["BLOODPRESSUREA_RIGHT"].ToString();
							objRecordContent.m_strBLOODPRESSUREA=dtbValue.Rows[j]["BLOODPRESSUREA"].ToString();
							objRecordContent.m_strBLOODPRESSUREAXML=dtbValue.Rows[j]["BLOODPRESSUREAXML"].ToString();
							objRecordContent.m_strA_RIGHT = dtbValue.Rows[j]["A_RIGHT"].ToString();
							objRecordContent.m_strA = dtbValue.Rows[j]["A"].ToString();
							objRecordContent.m_strAXML = dtbValue.Rows[j]["AXML"].ToString();
							objRecordContent.m_strSP02_RIGHT = dtbValue.Rows[j]["SP02_RIGHT"].ToString();
							objRecordContent.m_strSP02 = dtbValue.Rows[j]["SP02"].ToString();
							objRecordContent.m_strSP02XML = dtbValue.Rows[j]["SP02XML"].ToString();
							objRecordContent.m_strGENERALINSTANCE_RIGHT = dtbValue.Rows[j]["GENERALINSTANCE_RIGHT"].ToString();
							objRecordContent.m_strGENERALINSTANCE = dtbValue.Rows[j]["GENERALINSTANCE"].ToString();
							objRecordContent.m_strGENERALINSTANCEXML = dtbValue.Rows[j]["GENERALINSTANCEXML"].ToString();
							objRecordContent.m_strINAMOUNTITEM_RIGHT = dtbValue.Rows[j]["INAMOUNTITEM_RIGHT"].ToString();
							objRecordContent.m_strINAMOUNTITEM = dtbValue.Rows[j]["INAMOUNTITEM"].ToString();
							objRecordContent.m_strINAMOUNTITEMXML = dtbValue.Rows[j]["INAMOUNTITEMXML"].ToString();
							objRecordContent.m_strINAMOUNTSTANDBY_RIGHT = dtbValue.Rows[j]["INAMOUNTSTANDBY_RIGHT"].ToString();
							objRecordContent.m_strINAMOUNTSTANDBY = dtbValue.Rows[j]["INAMOUNTSTANDBY"].ToString();
							objRecordContent.m_strINAMOUNTSTANDBYXML = dtbValue.Rows[j]["INAMOUNTSTANDBYXML"].ToString();
							objRecordContent.m_strINAMOUNTFACT_RIGHT = dtbValue.Rows[j]["INAMOUNTFACT_RIGHT"].ToString();
							objRecordContent.m_strINAMOUNTFACT = dtbValue.Rows[j]["INAMOUNTFACT"].ToString();
							objRecordContent.m_strINAMOUNTFACTXML = dtbValue.Rows[j]["INAMOUNTFACTXML"].ToString();
							objRecordContent.m_strOUTEMICTION_RIGHT = dtbValue.Rows[j]["OUTEMICTION_RIGHT"].ToString();
							objRecordContent.m_strOUTEMICTION = dtbValue.Rows[j]["OUTEMICTION"].ToString();
							objRecordContent.m_strOUTEMICTIONXML = dtbValue.Rows[j]["OUTEMICTIONXML"].ToString();
							objRecordContent.m_strDISEASEID = dtbValue.Rows[j]["DISEASEID"].ToString();

							objRecordContent.m_strCustom1 = dtbValue.Rows[j]["CUSTOM1"].ToString();
							objRecordContent.m_strCustom1XML = dtbValue.Rows[j]["CUSTOM1XML"].ToString();
							objRecordContent.m_strCustom2 = dtbValue.Rows[j]["CUSTOM2"].ToString();
							objRecordContent.m_strCustom2XML = dtbValue.Rows[j]["CUSTOM2XML"].ToString();
							objRecordContent.m_strCustom1Name = dtbValue.Rows[j]["CUSTOM1NAME"].ToString();
							objRecordContent.m_strCustom2Name = dtbValue.Rows[j]["CUSTOM2NAME"].ToString();
							objRecordContent.m_strCustom3Name = dtbValue.Rows[j]["CUSTOM3NAME"].ToString();
							objRecordContent.m_strCustom4Name = dtbValue.Rows[j]["CUSTOM4NAME"].ToString();
							objRecordContent.m_strSummary = dtbValue.Rows[j]["SUMMARY"].ToString();
							objRecordContent.m_strSummaryXML = dtbValue.Rows[j]["SUMMARYXML"].ToString();
							objRecordContent.m_strCustom1_Right = dtbValue.Rows[j]["CUSTOM1_RIGHT"].ToString();
							objRecordContent.m_strCustom2_Right = dtbValue.Rows[j]["CUSTOM2_RIGHT"].ToString();
							objRecordContent.m_strSummary_Right = dtbValue.Rows[j]["SUMMARY_RIGHT"].ToString();

							objRecordContent.m_intISSTAT = dtbValue.Rows[j]["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[j]["ISSTAT"]);
							objRecordContent.m_intSUMINTIME = dtbValue.Rows[j]["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[j]["SUMINTIME"]);
							objRecordContent.m_intSUMOUTTIME = dtbValue.Rows[j]["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[j]["SUMOUTTIME"]);
							objRecordContent.m_strAUTOSUMIN = dtbValue.Rows[j]["AUTOSUMIN"].ToString();
							objRecordContent.m_strAUTOSUMOUT = dtbValue.Rows[j]["AUTOSUMOUT"].ToString();
							objRecordContent.m_strSUMIN = dtbValue.Rows[j]["SUMIN"].ToString();
							objRecordContent.m_strSUMOUT = dtbValue.Rows[j]["SUMOUT"].ToString();
							objRecordContent.m_dtmSTARTSTATTIME = dtbValue.Rows[j]["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(dtbValue.Rows[j]["STARTSTATTIME"]);
                            objRecordContent.m_strINSTANDBYSUM = dtbValue.Rows[j]["INSTANDBYSUM"].ToString();
                            objRecordContent.m_strAUTOINSTANDBYSUM = dtbValue.Rows[j]["AUTOINSTANDBYSUM"].ToString();
                            objRecordContent.m_strINFACTSUM = dtbValue.Rows[j]["INFACTSUM"].ToString();
                            objRecordContent.m_strAUTOINFACTSUM = dtbValue.Rows[j]["AUTOINFACTSUM"].ToString();
                            objRecordContent.m_strOUTEMICTIONSUM = dtbValue.Rows[j]["OUTEMICTIONSUM"].ToString();
                            objRecordContent.m_strAUTOOUTEMICTIONSUM = dtbValue.Rows[j]["AUTOOUTEMICTIONSUM"].ToString();
                            objRecordContent.m_strOUTCUSTOM1SUM = dtbValue.Rows[j]["OUTCUSTOM1SUM"].ToString();
                            objRecordContent.m_strAUTOOUTCUSTOM1SUM = dtbValue.Rows[j]["AUTOOUTCUSTOM1SUM"].ToString();
                            objRecordContent.m_strOUTCUSTOM2SUM = dtbValue.Rows[j]["OUTCUSTOM2SUM"].ToString();
                            objRecordContent.m_strAUTOOUTCUSTOM2SUM = dtbValue.Rows[j]["AUTOOUTCUSTOM2SUM"].ToString();
							#endregion
						}       
				
						p_objTansDataInfo[j] = objRecordContent;
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
			clsICUNurseRecordContentGX[] p_objGeneralNurseRecordArr = null;
			p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsICUNurseRecordContentGXDataInfo objDataInfo = new clsICUNurseRecordContentGXDataInfo();
			
			try
			{
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);	

				DataTable dtbContent = new DataTable();//护理记录内容  

				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
				if(lngRes > 0 && dtbContent.Rows.Count > 0)
				{
					clsICUNurseRecordContentGX objRecordContent = null;
					p_objGeneralNurseRecordArr = new clsICUNurseRecordContentGX[dtbContent.Rows.Count];
					for(int i=0; i<dtbContent.Rows.Count; i++)
					{
						objRecordContent = new clsICUNurseRecordContentGX();
						objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbContent.Rows[i]["CREATEDATE"].ToString());
						objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbContent.Rows[i]["MODIFYDATE"].ToString());
						objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

						if(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString()=="")
							objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
						else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString());
						objRecordContent.m_strCreateUserID=dtbContent.Rows[i]["CREATEUSERID"].ToString();
						objRecordContent.m_strModifyUserID=dtbContent.Rows[i]["MODIFYUSERID"].ToString();
						objRecordContent.m_strCreateUserName = dtbContent.Rows[i]["CreateUserName"].ToString();
						if(dtbContent.Rows[i]["IFCONFIRM"].ToString()=="")
							objRecordContent.m_bytIfConfirm=0;
						else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbContent.Rows[i]["IFCONFIRM"].ToString());
						if(dtbContent.Rows[i]["STATUS"].ToString()=="")
							objRecordContent.m_bytStatus=0;
						else objRecordContent.m_bytStatus=Byte.Parse(dtbContent.Rows[i]["STATUS"].ToString());

						objRecordContent.m_strConfirmReason=dtbContent.Rows[i]["CONFIRMREASON"].ToString();
						objRecordContent.m_strConfirmReasonXML=dtbContent.Rows[i]["CONFIRMREASONXML"].ToString();

						objRecordContent.m_strTEMPERATURE_RIGHT=dtbContent.Rows[i]["TEMPERATURE_RIGHT"].ToString();
						objRecordContent.m_strTEMPERATURE=dtbContent.Rows[i]["TEMPERATURE"].ToString();
						objRecordContent.m_strTEMPERATUREXML=dtbContent.Rows[i]["TEMPERATUREXML"].ToString();
						objRecordContent.m_strRESPIRATION_RIGHT=dtbContent.Rows[i]["RESPIRATION_RIGHT"].ToString();
						objRecordContent.m_strRESPIRATION=dtbContent.Rows[i]["RESPIRATION"].ToString();
						objRecordContent.m_strRESPIRATIONXML=dtbContent.Rows[i]["RESPIRATIONXML"].ToString();
						objRecordContent.m_strHR_RIGHT=dtbContent.Rows[i]["HR_RIGHT"].ToString();
						objRecordContent.m_strHR=dtbContent.Rows[i]["HR"].ToString();
						objRecordContent.m_strHRXML=dtbContent.Rows[i]["HRXML"].ToString();
						objRecordContent.m_strBLOODPRESSURES_RIGHT=dtbContent.Rows[i]["BLOODPRESSURES_RIGHT"].ToString();
						objRecordContent.m_strBLOODPRESSURES=dtbContent.Rows[i]["BLOODPRESSURES"].ToString();
						objRecordContent.m_strBLOODPRESSURESXML=dtbContent.Rows[i]["BLOODPRESSURESXML"].ToString();
						objRecordContent.m_strBLOODPRESSUREA_RIGHT=dtbContent.Rows[i]["BLOODPRESSUREA_RIGHT"].ToString();
						objRecordContent.m_strBLOODPRESSUREA=dtbContent.Rows[i]["BLOODPRESSUREA"].ToString();
						objRecordContent.m_strBLOODPRESSUREAXML=dtbContent.Rows[i]["BLOODPRESSUREAXML"].ToString();
						objRecordContent.m_strA_RIGHT = dtbContent.Rows[i]["A_RIGHT"].ToString();
						objRecordContent.m_strA = dtbContent.Rows[i]["A"].ToString();
						objRecordContent.m_strAXML = dtbContent.Rows[i]["AXML"].ToString();
						objRecordContent.m_strSP02_RIGHT = dtbContent.Rows[i]["SP02_RIGHT"].ToString();
						objRecordContent.m_strSP02 = dtbContent.Rows[i]["SP02"].ToString();
						objRecordContent.m_strSP02XML = dtbContent.Rows[i]["SP02XML"].ToString();
						objRecordContent.m_strGENERALINSTANCE_RIGHT = dtbContent.Rows[i]["GENERALINSTANCE_RIGHT"].ToString();
						objRecordContent.m_strGENERALINSTANCE = dtbContent.Rows[i]["GENERALINSTANCE"].ToString();
						objRecordContent.m_strGENERALINSTANCEXML = dtbContent.Rows[i]["GENERALINSTANCEXML"].ToString();
						objRecordContent.m_strINAMOUNTITEM_RIGHT = dtbContent.Rows[i]["INAMOUNTITEM_RIGHT"].ToString();
						objRecordContent.m_strINAMOUNTITEM = dtbContent.Rows[i]["INAMOUNTITEM"].ToString();
						objRecordContent.m_strINAMOUNTITEMXML = dtbContent.Rows[i]["INAMOUNTITEMXML"].ToString();
						objRecordContent.m_strINAMOUNTSTANDBY_RIGHT = dtbContent.Rows[i]["INAMOUNTSTANDBY_RIGHT"].ToString();
						objRecordContent.m_strINAMOUNTSTANDBY = dtbContent.Rows[i]["INAMOUNTSTANDBY"].ToString();
						objRecordContent.m_strINAMOUNTSTANDBYXML = dtbContent.Rows[i]["INAMOUNTSTANDBYXML"].ToString();
						objRecordContent.m_strINAMOUNTFACT_RIGHT = dtbContent.Rows[i]["INAMOUNTFACT_RIGHT"].ToString();
						objRecordContent.m_strINAMOUNTFACT = dtbContent.Rows[i]["INAMOUNTFACT"].ToString();
						objRecordContent.m_strINAMOUNTFACTXML = dtbContent.Rows[i]["INAMOUNTFACTXML"].ToString();
						objRecordContent.m_strOUTEMICTION_RIGHT = dtbContent.Rows[i]["OUTEMICTION_RIGHT"].ToString();
						objRecordContent.m_strOUTEMICTION = dtbContent.Rows[i]["OUTEMICTION"].ToString();
						objRecordContent.m_strOUTEMICTIONXML = dtbContent.Rows[i]["OUTEMICTIONXML"].ToString();
						objRecordContent.m_strDISEASEID = dtbContent.Rows[i]["DISEASEID"].ToString();

						objRecordContent.m_strCustom1 = dtbContent.Rows[i]["CUSTOM1"].ToString();
						objRecordContent.m_strCustom1XML = dtbContent.Rows[i]["CUSTOM1XML"].ToString();
						objRecordContent.m_strCustom2 = dtbContent.Rows[i]["CUSTOM2"].ToString();
						objRecordContent.m_strCustom2XML = dtbContent.Rows[i]["CUSTOM2XML"].ToString();
						objRecordContent.m_strCustom1Name = dtbContent.Rows[i]["CUSTOM1NAME"].ToString();
						objRecordContent.m_strCustom2Name = dtbContent.Rows[i]["CUSTOM2NAME"].ToString();
						objRecordContent.m_strCustom3Name = dtbContent.Rows[i]["CUSTOM3NAME"].ToString();
						objRecordContent.m_strCustom4Name = dtbContent.Rows[i]["CUSTOM4NAME"].ToString();
						objRecordContent.m_strSummary = dtbContent.Rows[i]["SUMMARY"].ToString();
						objRecordContent.m_strSummaryXML = dtbContent.Rows[i]["SUMMARYXML"].ToString();
						objRecordContent.m_strCustom1_Right = dtbContent.Rows[i]["CUSTOM1_RIGHT"].ToString();
						objRecordContent.m_strCustom2_Right = dtbContent.Rows[i]["CUSTOM2_RIGHT"].ToString();
						objRecordContent.m_strSummary_Right = dtbContent.Rows[i]["SUMMARY_RIGHT"].ToString();

						objRecordContent.m_intISSTAT = dtbContent.Rows[i]["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(dtbContent.Rows[i]["ISSTAT"]);
						objRecordContent.m_intSUMINTIME = dtbContent.Rows[i]["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbContent.Rows[i]["SUMINTIME"]);
						objRecordContent.m_intSUMOUTTIME = dtbContent.Rows[i]["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbContent.Rows[i]["SUMOUTTIME"]);
						objRecordContent.m_strAUTOSUMIN = dtbContent.Rows[i]["AUTOSUMIN"].ToString();
						objRecordContent.m_strAUTOSUMOUT = dtbContent.Rows[i]["AUTOSUMOUT"].ToString();
						objRecordContent.m_strSUMIN = dtbContent.Rows[i]["SUMIN"].ToString();
						objRecordContent.m_strSUMOUT = dtbContent.Rows[i]["SUMOUT"].ToString();
						objRecordContent.m_dtmSTARTSTATTIME = dtbContent.Rows[i]["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(dtbContent.Rows[i]["STARTSTATTIME"]);
                        objRecordContent.m_strINSTANDBYSUM = dtbContent.Rows[i]["INSTANDBYSUM"].ToString();
                        objRecordContent.m_strAUTOINSTANDBYSUM = dtbContent.Rows[i]["AUTOINSTANDBYSUM"].ToString();
                        objRecordContent.m_strINFACTSUM = dtbContent.Rows[i]["INFACTSUM"].ToString();
                        objRecordContent.m_strAUTOINFACTSUM = dtbContent.Rows[i]["AUTOINFACTSUM"].ToString();
                        objRecordContent.m_strOUTEMICTIONSUM = dtbContent.Rows[i]["OUTEMICTIONSUM"].ToString();
                        objRecordContent.m_strAUTOOUTEMICTIONSUM = dtbContent.Rows[i]["AUTOOUTEMICTIONSUM"].ToString();
                        objRecordContent.m_strOUTCUSTOM1SUM = dtbContent.Rows[i]["OUTCUSTOM1SUM"].ToString();
                        objRecordContent.m_strAUTOOUTCUSTOM1SUM = dtbContent.Rows[i]["AUTOOUTCUSTOM1SUM"].ToString();
                        objRecordContent.m_strOUTCUSTOM2SUM = dtbContent.Rows[i]["OUTCUSTOM2SUM"].ToString();
                        objRecordContent.m_strAUTOOUTCUSTOM2SUM = dtbContent.Rows[i]["AUTOOUTCUSTOM2SUM"].ToString();
						p_objGeneralNurseRecordArr[i] = objRecordContent;
					}
					objDataInfo.m_objRecordContent = p_objGeneralNurseRecordArr[p_objGeneralNurseRecordArr.Length-1];
				}
				objDataInfo.m_objRecordArr = p_objGeneralNurseRecordArr;
				objDataInfo.m_intFlag = (int)enmRecordsType.ICUNurseRecord_GX;
				
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

			//检查参数          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from 
											icunurserecord_gxrecord t1,icunurserecord_gxcontent t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status = 0
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc "+clsDatabaseSQLConvert.s_StrRownum;

				
			long lngRes = 0;
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

        /// <summary>
        /// 获取所有没删除的数据
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objTansDataInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMainRecord(string p_strInPatientID,
            string p_strInPatientDate,
            out clsICUNurseRecordContentGX[] p_objTansDataInfo)
        {

            p_objTansDataInfo = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
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
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbContent = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    p_objTansDataInfo = new clsICUNurseRecordContentGX[dtbContent.Rows.Count];
                    clsICUNurseRecordContentGX objRecordContent = null;

                    for (int i = 0; i < dtbContent.Rows.Count; i++)
                    {
                        
                            #region 从DataTable.Rows中获取结果

                            objRecordContent = new clsICUNurseRecordContentGX();
                            objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbContent.Rows[i]["CREATEDATE"].ToString());
                            objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbContent.Rows[i]["MODIFYDATE"].ToString());
                            objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

                            if (dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                                objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString());
                            objRecordContent.m_strCreateUserID = dtbContent.Rows[i]["CREATEUSERID"].ToString();
                            objRecordContent.m_strModifyUserID = dtbContent.Rows[i]["MODIFYUSERID"].ToString();
                            objRecordContent.m_strCreateUserName = dtbContent.Rows[i]["CreateUserName"].ToString();
                            if (dtbContent.Rows[i]["IFCONFIRM"].ToString() == "")
                                objRecordContent.m_bytIfConfirm = 0;
                            else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbContent.Rows[i]["IFCONFIRM"].ToString());
                            if (dtbContent.Rows[i]["STATUS"].ToString() == "")
                                objRecordContent.m_bytStatus = 0;
                            else objRecordContent.m_bytStatus = Byte.Parse(dtbContent.Rows[i]["STATUS"].ToString());

                            objRecordContent.m_strConfirmReason = dtbContent.Rows[i]["CONFIRMREASON"].ToString();
                            objRecordContent.m_strConfirmReasonXML = dtbContent.Rows[i]["CONFIRMREASONXML"].ToString();

                            objRecordContent.m_strTEMPERATURE_RIGHT = dtbContent.Rows[i]["TEMPERATURE_RIGHT"].ToString();
                            objRecordContent.m_strTEMPERATURE = dtbContent.Rows[i]["TEMPERATURE"].ToString();
                            objRecordContent.m_strTEMPERATUREXML = dtbContent.Rows[i]["TEMPERATUREXML"].ToString();
                            objRecordContent.m_strRESPIRATION_RIGHT = dtbContent.Rows[i]["RESPIRATION_RIGHT"].ToString();
                            objRecordContent.m_strRESPIRATION = dtbContent.Rows[i]["RESPIRATION"].ToString();
                            objRecordContent.m_strRESPIRATIONXML = dtbContent.Rows[i]["RESPIRATIONXML"].ToString();
                            objRecordContent.m_strHR_RIGHT = dtbContent.Rows[i]["HR_RIGHT"].ToString();
                            objRecordContent.m_strHR = dtbContent.Rows[i]["HR"].ToString();
                            objRecordContent.m_strHRXML = dtbContent.Rows[i]["HRXML"].ToString();
                            objRecordContent.m_strBLOODPRESSURES_RIGHT = dtbContent.Rows[i]["BLOODPRESSURES_RIGHT"].ToString();
                            objRecordContent.m_strBLOODPRESSURES = dtbContent.Rows[i]["BLOODPRESSURES"].ToString();
                            objRecordContent.m_strBLOODPRESSURESXML = dtbContent.Rows[i]["BLOODPRESSURESXML"].ToString();
                            objRecordContent.m_strBLOODPRESSUREA_RIGHT = dtbContent.Rows[i]["BLOODPRESSUREA_RIGHT"].ToString();
                            objRecordContent.m_strBLOODPRESSUREA = dtbContent.Rows[i]["BLOODPRESSUREA"].ToString();
                            objRecordContent.m_strBLOODPRESSUREAXML = dtbContent.Rows[i]["BLOODPRESSUREAXML"].ToString();
                            objRecordContent.m_strA_RIGHT = dtbContent.Rows[i]["A_RIGHT"].ToString();
                            objRecordContent.m_strA = dtbContent.Rows[i]["A"].ToString();
                            objRecordContent.m_strAXML = dtbContent.Rows[i]["AXML"].ToString();
                            objRecordContent.m_strSP02_RIGHT = dtbContent.Rows[i]["SP02_RIGHT"].ToString();
                            objRecordContent.m_strSP02 = dtbContent.Rows[i]["SP02"].ToString();
                            objRecordContent.m_strSP02XML = dtbContent.Rows[i]["SP02XML"].ToString();
                            objRecordContent.m_strGENERALINSTANCE_RIGHT = dtbContent.Rows[i]["GENERALINSTANCE_RIGHT"].ToString();
                            objRecordContent.m_strGENERALINSTANCE = dtbContent.Rows[i]["GENERALINSTANCE"].ToString();
                            objRecordContent.m_strGENERALINSTANCEXML = dtbContent.Rows[i]["GENERALINSTANCEXML"].ToString();
                            objRecordContent.m_strINAMOUNTITEM_RIGHT = dtbContent.Rows[i]["INAMOUNTITEM_RIGHT"].ToString();
                            objRecordContent.m_strINAMOUNTITEM = dtbContent.Rows[i]["INAMOUNTITEM"].ToString();
                            objRecordContent.m_strINAMOUNTITEMXML = dtbContent.Rows[i]["INAMOUNTITEMXML"].ToString();
                            objRecordContent.m_strINAMOUNTSTANDBY_RIGHT = dtbContent.Rows[i]["INAMOUNTSTANDBY_RIGHT"].ToString();
                            objRecordContent.m_strINAMOUNTSTANDBY = dtbContent.Rows[i]["INAMOUNTSTANDBY"].ToString();
                            objRecordContent.m_strINAMOUNTSTANDBYXML = dtbContent.Rows[i]["INAMOUNTSTANDBYXML"].ToString();
                            objRecordContent.m_strINAMOUNTFACT_RIGHT = dtbContent.Rows[i]["INAMOUNTFACT_RIGHT"].ToString();
                            objRecordContent.m_strINAMOUNTFACT = dtbContent.Rows[i]["INAMOUNTFACT"].ToString();
                            objRecordContent.m_strINAMOUNTFACTXML = dtbContent.Rows[i]["INAMOUNTFACTXML"].ToString();
                            objRecordContent.m_strOUTEMICTION_RIGHT = dtbContent.Rows[i]["OUTEMICTION_RIGHT"].ToString();
                            objRecordContent.m_strOUTEMICTION = dtbContent.Rows[i]["OUTEMICTION"].ToString();
                            objRecordContent.m_strOUTEMICTIONXML = dtbContent.Rows[i]["OUTEMICTIONXML"].ToString();
                            objRecordContent.m_strDISEASEID = dtbContent.Rows[i]["DISEASEID"].ToString();

                            objRecordContent.m_strCustom1 = dtbContent.Rows[i]["CUSTOM1"].ToString();
                            objRecordContent.m_strCustom1XML = dtbContent.Rows[i]["CUSTOM1XML"].ToString();
                            objRecordContent.m_strCustom2 = dtbContent.Rows[i]["CUSTOM2"].ToString();
                            objRecordContent.m_strCustom2XML = dtbContent.Rows[i]["CUSTOM2XML"].ToString();
                            objRecordContent.m_strCustom1Name = dtbContent.Rows[i]["CUSTOM1NAME"].ToString();
                            objRecordContent.m_strCustom2Name = dtbContent.Rows[i]["CUSTOM2NAME"].ToString();
                            objRecordContent.m_strCustom3Name = dtbContent.Rows[i]["CUSTOM3NAME"].ToString();
                            objRecordContent.m_strCustom4Name = dtbContent.Rows[i]["CUSTOM4NAME"].ToString();
                            objRecordContent.m_strSummary = dtbContent.Rows[i]["SUMMARY"].ToString();
                            objRecordContent.m_strSummaryXML = dtbContent.Rows[i]["SUMMARYXML"].ToString();
                            objRecordContent.m_strCustom1_Right = dtbContent.Rows[i]["CUSTOM1_RIGHT"].ToString();
                            objRecordContent.m_strCustom2_Right = dtbContent.Rows[i]["CUSTOM2_RIGHT"].ToString();
                            objRecordContent.m_strSummary_Right = dtbContent.Rows[i]["SUMMARY_RIGHT"].ToString();

                            objRecordContent.m_intISSTAT = dtbContent.Rows[i]["ISSTAT"] == DBNull.Value ? -1 : Convert.ToInt32(dtbContent.Rows[i]["ISSTAT"]);
                            objRecordContent.m_intSUMINTIME = dtbContent.Rows[i]["SUMINTIME"] == DBNull.Value ? -1 : Convert.ToInt32(dtbContent.Rows[i]["SUMINTIME"]);
                            objRecordContent.m_intSUMOUTTIME = dtbContent.Rows[i]["SUMOUTTIME"] == DBNull.Value ? -1 : Convert.ToInt32(dtbContent.Rows[i]["SUMOUTTIME"]);
                            objRecordContent.m_strAUTOSUMIN = dtbContent.Rows[i]["AUTOSUMIN"].ToString();
                            objRecordContent.m_strAUTOSUMOUT = dtbContent.Rows[i]["AUTOSUMOUT"].ToString();
                            objRecordContent.m_strSUMIN = dtbContent.Rows[i]["SUMIN"].ToString();
                            objRecordContent.m_strSUMOUT = dtbContent.Rows[i]["SUMOUT"].ToString();
                            objRecordContent.m_dtmSTARTSTATTIME = dtbContent.Rows[i]["STARTSTATTIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtbContent.Rows[i]["STARTSTATTIME"]);
                            objRecordContent.m_strINSTANDBYSUM = dtbContent.Rows[i]["INSTANDBYSUM"].ToString();
                            objRecordContent.m_strAUTOINSTANDBYSUM = dtbContent.Rows[i]["AUTOINSTANDBYSUM"].ToString();
                            objRecordContent.m_strINFACTSUM = dtbContent.Rows[i]["INFACTSUM"].ToString();
                            objRecordContent.m_strAUTOINFACTSUM = dtbContent.Rows[i]["AUTOINFACTSUM"].ToString();
                            objRecordContent.m_strOUTEMICTIONSUM = dtbContent.Rows[i]["OUTEMICTIONSUM"].ToString();
                            objRecordContent.m_strAUTOOUTEMICTIONSUM = dtbContent.Rows[i]["AUTOOUTEMICTIONSUM"].ToString();
                            objRecordContent.m_strOUTCUSTOM1SUM = dtbContent.Rows[i]["OUTCUSTOM1SUM"].ToString();
                            objRecordContent.m_strAUTOOUTCUSTOM1SUM = dtbContent.Rows[i]["AUTOOUTCUSTOM1SUM"].ToString();
                            objRecordContent.m_strOUTCUSTOM2SUM = dtbContent.Rows[i]["OUTCUSTOM2SUM"].ToString();
                            objRecordContent.m_strAUTOOUTCUSTOM2SUM = dtbContent.Rows[i]["AUTOOUTCUSTOM2SUM"].ToString();
                            #endregion
                        

                        p_objTansDataInfo[i] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
	}
}
