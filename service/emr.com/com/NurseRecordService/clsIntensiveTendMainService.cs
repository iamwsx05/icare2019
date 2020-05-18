using System;
//using com.digitalwave.HRPService;
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
	/// 实现主病程记录的中间件。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsIntensiveTendMainService	: com.digitalwave.clsRecordsService.clsRecordsService
	{
		private const string c_strUpdateFirstPrintDateSQL="update  intensivetendrecord1 set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

//		private const string c_strCheckLastModifyRecordSQL=@"select b.ModifyDate,b.ModifyUserID from IntensiveTendRecord1 a,IntensiveTendRecordContent1 b where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and b.InPatientID=a.InPatientID and b.InPatientDate=a.InPatientDate and b.OpenDate=a.OpenDate and
//						b.ModifyDate=(select Max(ModifyDate) from IntensiveTendRecordContent1 Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

//		private const string c_strGetDeleteRecordSQL="select DeActivedDate,DeActivedOperatorID from IntensiveTendRecord1 Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";

//		private const string c_strDeleteRecordSQL="Update IntensiveTendRecord1 Set Status=1,DeActivedDate=?,DeActivedOperatorID=? Where trim(InPatientID)=? and InPatientDate=? and OpenDate=? and Status=0";

        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t3.modifyuserid) as modifyusername,
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
       t1.recordcontent,
       t1.recordcontentxml,
       t1.temperature,
       t1.temperaturexml,
       t1.pulse,
       t1.pulsexml,
       t1.breath,
       t1.breathxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.ind,
       t1.indxml,
       t1.ini,
       t1.inixml,
       t1.pupilleft,
       t1.pupilleftxml,
       t1.pupilright,
       t1.pupilrightxml,
       t1.echoleft,
       t1.echoleftxml,
       t1.echoright,
       t1.echorightxml,
       t1.outu,
       t1.outuxml,
       t1.outv,
       t1.outvxml,
       t1.outs,
       t1.outsxml,
       t1.oute,
       t1.outexml,
       t1.mind,
       t1.mindxml,
       t1.class,
        t1.bloodoxygensaturation,
       t1.bloodoxygensaturationxml,
       t3.modifydate,
       t3.modifyuserid,
       t3.recordcontent_right,
       t3.temperature_right,
       t3.pulse_right,
       t3.breath_right,
       t3.bloodpressures_right,
       t3.bloodpressurea_right,
       t3.ind_right,
       t3.ini_right,
       t3.pupilleft_right,
       t3.pupilright_right,
       t3.echoleft_right,
       t3.echoright_right,
       t3.outu_right,
       t3.outv_right,
       t3.outs_right,
       t3.oute_right,
       t3.mind_right,
t3.bloodoxygensaturation_right
  from intensivetendrecord1 t1, intensivetendrecordcontent1 t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate, t3.modifydate";


        private const string c_strGetRecordContentSQL_Single = @"select f_getempnamebyno(t3.modifyuserid) as modifyusername,
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
       t1.recordcontent,
       t1.recordcontentxml,
       t1.temperature,
       t1.temperaturexml,
       t1.pulse,
       t1.pulsexml,
       t1.breath,
       t1.breathxml,
       t1.bloodpressures,
       t1.bloodpressuresxml,
       t1.bloodpressurea,
       t1.bloodpressureaxml,
       t1.ind,
       t1.indxml,
       t1.ini,
       t1.inixml,
       t1.pupilleft,
       t1.pupilleftxml,
       t1.pupilright,
       t1.pupilrightxml,
       t1.echoleft,
       t1.echoleftxml,
       t1.echoright,
       t1.echorightxml,
       t1.outu,
       t1.outuxml,
       t1.outv,
       t1.outvxml,
       t1.outs,
       t1.outsxml,
       t1.oute,
       t1.outexml,
       t1.mind,
       t1.mindxml,
       t1.class,
t1.bloodoxygensaturation,
       t1.bloodoxygensaturationxml,
       t3.modifydate,
       t3.modifyuserid,
       t3.recordcontent_right,
       t3.temperature_right,
       t3.pulse_right,
       t3.breath_right,
       t3.bloodpressures_right,
       t3.bloodpressurea_right,
       t3.ind_right,
       t3.ini_right,
       t3.pupilleft_right,
       t3.pupilright_right,
       t3.echoleft_right,
       t3.echoright_right,
       t3.outu_right,
       t3.outv_right,
       t3.outs_right,
       t3.oute_right,
       t3.mind_right,
t3.bloodoxygensaturation_right
  from intensivetendrecord1 t1, intensivetendrecordcontent1 t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.createdate, t3.modifydate";

		
		/// <summary>
		///  从IntensiveTendRecord1获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL = @"select deactiveddate,deactivedoperatorid from 
								intensivetendrecord1 where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		/// <summary>
		///  从IntensiveTendRecord1删除表单的主要信息。
		/// </summary>
		private const string c_strDeleteRecordSQL=@"update intensivetendrecord1 set status=1,deactiveddate=?,
								deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";



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
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||
				p_intRecordTypeArr==null||p_dtmOpenDateArr==null||p_intRecordTypeArr.Length != p_dtmOpenDateArr.Length ||p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//获取IDataParameter数组
			IDataParameter[] objDPArr = null;
			
			for(int i=0;i<p_intRecordTypeArr.Length;i++)
			{
				//根据不同的子表单，获取不同的SQL语句
				string strSQL = null;
				switch((enmDiseaseTrackType)p_intRecordTypeArr[i])
				{
					case enmDiseaseTrackType.IntensiveTend:
						strSQL =  c_strUpdateFirstPrintDateSQL;
						break;

					default: return (long)enmOperationResult.Parameter_Error; 
				}
			
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
				long lngRes=0;				
				long lngEff=0;
				lngRes = objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
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
			out clsIntensiveTendDataInfo p_objTansDataInfo)
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

				//危重护理记录，使用c_strGetRecordContentSQL
		
				//按顺序给IDataParameter赋值
			
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					clsIntensiveTendRecordContent1 objRecordContent= null;
				
					clsIntensiveTendDataInfo objInfo = null;
				
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//生成 clsIntensiveTendDataInfo
						objInfo = new clsIntensiveTendDataInfo();
						objInfo.m_intFlag = (int)enmRecordsType.IntensiveTend;   //因为可肯定为危重护理记录，所以可设任意值
						//设置结果到 objInfo.m_objRecordContent
						//					objInfo.m_objRecordContent = objRecordContent;
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date;
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()).Date == dtmOpenDate)
						{
							#region 从DataTable.Rows中获取结果    
						
							objRecordContent=new clsIntensiveTendRecordContent1();
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
				
							objRecordContent.m_strTemperature=dtbValue.Rows[j]["TEMPERATURE_RIGHT"].ToString();
							objRecordContent.m_strTemperatureAll=dtbValue.Rows[j]["TEMPERATURE"].ToString();
							objRecordContent.m_strTemperatureXML=dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
							objRecordContent.m_strBreath=dtbValue.Rows[j]["BREATH_RIGHT"].ToString();
							objRecordContent.m_strBreathAll=dtbValue.Rows[j]["BREATH"].ToString();
							objRecordContent.m_strBreathXML=dtbValue.Rows[j]["BREATHXML"].ToString();
							objRecordContent.m_strPulse=dtbValue.Rows[j]["PULSE_RIGHT"].ToString();
							objRecordContent.m_strPulseAll=dtbValue.Rows[j]["PULSE"].ToString();
							objRecordContent.m_strPulseXML=dtbValue.Rows[j]["PULSEXML"].ToString();
							objRecordContent.m_strBloodPressureS=dtbValue.Rows[j]["BLOODPRESSURES_RIGHT"].ToString();
							objRecordContent.m_strBloodPressureSAll=dtbValue.Rows[j]["BLOODPRESSURES"].ToString();
							objRecordContent.m_strBloodPressureSXML=dtbValue.Rows[j]["BLOODPRESSURESXML"].ToString();
							objRecordContent.m_strBloodPressureA=dtbValue.Rows[j]["BLOODPRESSUREA_RIGHT"].ToString();
							objRecordContent.m_strBloodPressureAAll=dtbValue.Rows[j]["BLOODPRESSUREA"].ToString();
							objRecordContent.m_strBloodPressureAXML=dtbValue.Rows[j]["BLOODPRESSUREAXML"].ToString();
							objRecordContent.m_strPupilLeft=dtbValue.Rows[j]["PUPILLEFT_RIGHT"].ToString();
							objRecordContent.m_strPupilLeftAll=dtbValue.Rows[j]["PUPILLEFT"].ToString();
							objRecordContent.m_strPupilLeftXML=dtbValue.Rows[j]["PUPILLEFTXML"].ToString();
							objRecordContent.m_strPupilRight=dtbValue.Rows[j]["PUPILRIGHT_RIGHT"].ToString();
							objRecordContent.m_strPupilRightAll=dtbValue.Rows[j]["PUPILRIGHT"].ToString();
							objRecordContent.m_strPupilRightXML=dtbValue.Rows[j]["PUPILRIGHTXML"].ToString();
							objRecordContent.m_strEchoLeft=dtbValue.Rows[j]["ECHOLEFT_RIGHT"].ToString();
							objRecordContent.m_strEchoLeftAll=dtbValue.Rows[j]["ECHOLEFT"].ToString();
							objRecordContent.m_strEchoLeftXML=dtbValue.Rows[j]["ECHOLEFTXML"].ToString();
							objRecordContent.m_strEchoRight=dtbValue.Rows[j]["ECHORIGHT_RIGHT"].ToString();
							objRecordContent.m_strEchoRightAll=dtbValue.Rows[j]["ECHORIGHT"].ToString();
							objRecordContent.m_strEchoRightXML=dtbValue.Rows[j]["ECHORIGHTXML"].ToString();
							objRecordContent.m_intInD=Convert.ToInt32(dtbValue.Rows[j]["IND_RIGHT"]);
							objRecordContent.m_strInDAll=dtbValue.Rows[j]["IND"].ToString();
							objRecordContent.m_strInDXML=dtbValue.Rows[j]["INDXML"].ToString();
							objRecordContent.m_intInI=Convert.ToInt32(dtbValue.Rows[j]["INI_RIGHT"]);
							objRecordContent.m_strInIAll=dtbValue.Rows[j]["INI"].ToString();
							objRecordContent.m_strInIXML=dtbValue.Rows[j]["INIXML"].ToString();
							objRecordContent.m_intOutU=Convert.ToInt32(dtbValue.Rows[j]["OUTU_RIGHT"]);
							objRecordContent.m_strOutUAll=dtbValue.Rows[j]["OUTU"].ToString();
							objRecordContent.m_strOutUXML=dtbValue.Rows[j]["OUTUXML"].ToString();
							objRecordContent.m_intOutS=Convert.ToInt32(dtbValue.Rows[j]["OUTS_RIGHT"]);
							objRecordContent.m_strOutSAll=dtbValue.Rows[j]["OUTS"].ToString();
							objRecordContent.m_strOutSXML=dtbValue.Rows[j]["OUTSXML"].ToString();
							objRecordContent.m_intOutV=Convert.ToInt32(dtbValue.Rows[j]["OUTV_RIGHT"]);
							objRecordContent.m_strOutVAll=dtbValue.Rows[j]["OUTV"].ToString();
							objRecordContent.m_strOutVXML=dtbValue.Rows[j]["OUTVXML"].ToString();
							objRecordContent.m_intOutE=Convert.ToInt32(dtbValue.Rows[j]["OUTE_RIGHT"]);
							objRecordContent.m_strOutEAll=dtbValue.Rows[j]["OUTE"].ToString();
							objRecordContent.m_strOutEXML=dtbValue.Rows[j]["OUTEXML"].ToString();
                            //objRecordContent.m_strRecordContent=dtbValue.Rows[j]["RECORDCONTENT"].ToString();
                            //objRecordContent.m_strRecordContent_Right=dtbValue.Rows[j]["RECORDCONTENT_RIGHT"].ToString();
                            //objRecordContent.m_strRecordContentXml=dtbValue.Rows[j]["RECORDCONTENTXML"].ToString();
							objRecordContent.m_strMind=dtbValue.Rows[j]["Mind_RIGHT"].ToString();
							objRecordContent.m_strMindAll=dtbValue.Rows[j]["Mind"].ToString();
							objRecordContent.m_strMindXML=dtbValue.Rows[j]["MindXML"].ToString();
							objRecordContent.m_strClass=dtbValue.Rows[j]["Class"].ToString();
                            objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[j]["BloodOxygenSaturation_RIGHT"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[j]["BloodOxygenSaturation"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[j]["BloodOxygenSaturationXML"].ToString();
							//同一条记录的修改,保存在arlModifyData 
							arlModifyData.Add(objRecordContent);
							j++;
							#endregion
						}       
						//后移一条记录，使循环从新的OpenData开始。
						j--;
				
						objInfo.m_objTransDataArr = (clsIntensiveTendRecordContent1[])arlModifyData.ToArray(typeof(clsIntensiveTendRecordContent1));
						arlModifyData.Clear();
			
						//最后一条记录
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
				
						arlTransData.Add(objInfo);
					}
				}
				//返回结果到p_objTansDataInfo
				p_objTansDataInfo = ((clsIntensiveTendDataInfo[])arlTransData.ToArray(typeof(clsIntensiveTendDataInfo)))[0];
		
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
			out clsTransDataInfo[] p_objIntensiveTendInfoArr)
		{
			p_objIntensiveTendInfoArr=null;
			long lngRes = 0;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			//根据不同的连接方式调用不同的方法
			if(clsHRPTableService.bytDatabase_Selector == 0)
				lngRes=m_lngGetTransDataInfoArrWithServSqlServer(p_strInPatientID,p_strInPatientDate,p_objHRPServ,out p_objIntensiveTendInfoArr);
			else
				lngRes=m_lngGetTransDataInfoArrWithServForOracle(p_strInPatientID,p_strInPatientDate,p_objHRPServ,out p_objIntensiveTendInfoArr);
			return lngRes;
		}
		/// <summary>
		/// 获取指定记录的内容(ORACLE)
		/// 先按班统计后按日统计(班次日)
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objIntensiveTendInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngGetTransDataInfoArrWithServForOracle(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objIntensiveTendInfoArr)
		{
			
			p_objIntensiveTendInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
			try
			{
				ArrayList arlTransData = new ArrayList();	
				ArrayList arlClassData=new ArrayList();          //获取同一班次记录
				ArrayList arlModifyData = new ArrayList();       //获取同一条记录的修改记录历史数据
				ArrayList arlTransDataClone = new ArrayList();
				string strGlodClass="";                          //班次比较变量
				string strGlodDay="";                            //逻辑天比较变量
                //int intTemp=0;                                   //同班次病程记录内容数目
				clsIntensiveTendDataInfo objAppendInfo = null;
				DateTime dtmOpenDate;
				DateTime dtmCreateDate_Date;
				string strClass;                                 //班次
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);		
				//生成DataTable
				DataTable dtbValue = new DataTable();
				DataTable dtContent=new DataTable();          //获取病程记录内容

                string strSQL = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.createdate,
       t.createuserid,
       t.modifyuserid,
       t.recordcontent,
       t.recordcontentxml,
       t.class,
       t.description,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid, d.lastname_vchr firstname
  from intensivetendrecorddetail1 t, t_bse_employee d
 where t.status = 0
   and t.modifyuserid = d.empno_chr
   and inpatientid = ?
   and t.inpatientdate = ?";
				//先查询病程记录内容填充到dtcontent
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtContent,objDPArr);
				//dataview 默认过滤没有记录
				DataView dvContent=new DataView(dtContent);
				 dvContent.RowFilter="class='-1'";
				//获取IDataParameter数组
				IDataParameter[] objDPArr1 = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr1);
                objDPArr1[0].Value = p_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
				objDPArr1[1].Value=DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));
				//执行查询，填充结果到DataTable 
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr1);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					clsIntensiveTendRecordContent1 objRecordContent= null;
					clsIntensiveTendDataInfo objInfo = null;
					#region 获得统计数据
                    //佛二每天早8:00统计，一天只统计一次（不分班次）
                    //clsIntensiveTendRecordSummary[] m_objSummaryArr;  //班统计
                    //long m_lngRes = m_lngGetSummaryRecordsNew(p_strInPatientID,p_strInPatientDate,p_objHRPServ,out m_objSummaryArr);
					clsIntensiveTendRecordSummary[] m_objSummaryDayArr; //天统计
					long m_lngRes1 = m_lngGetSummaryRecordsNewDay(p_strInPatientID,p_strInPatientDate,p_objHRPServ,out m_objSummaryDayArr);
					#endregion
					
					#region 先对DataTable表班次做签名控制[不考虑]
//					string strTempName;
//					DateTime dtTempModifyDate;
//					bool blnWhile;
//					for(int j=0;j<dtbValue.Rows.Count-1;j++)//签名控制：同一班次记录只需显示最后一个修改签名（HB）
//					{
//						blnWhile=false;
//						strTempName=dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();
//						strClass=dtbValue.Rows[j]["Class"].ToString();
//						dtTempModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
//						while(j<dtbValue.Rows.Count-1 && dtbValue.Rows[j+1]["Class"].ToString() == strClass)
//						{
//							if(dtTempModifyDate > DateTime.Parse(dtbValue.Rows[j+1]["MODIFYDATE"].ToString()))
//							{
//								dtbValue.Rows[j]["MODIFYUSERNAME"] = "　";//全角空格字符
//								dtbValue.Rows[j+1]["MODIFYUSERNAME"] =strTempName;
//							}
//							else
//							{
//								strTempName=dtbValue.Rows[j+1]["MODIFYUSERNAME"].ToString();
//								dtTempModifyDate=DateTime.Parse(dtbValue.Rows[j+1]["MODIFYDATE"].ToString());
//								dtbValue.Rows[j]["MODIFYUSERNAME"] = "　";//全角空格字符
//							}						
//							j++;
//							blnWhile=true;
//						}
////						if (blnWhile)
////							j--;
//					}
					#endregion

					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate  = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
						strClass=dtbValue.Rows[j]["Class"].ToString();
						if (strGlodClass!=strClass)
                        {
                            #region 屏蔽--wf20080117
                            /*
                            //假如同一班次内病程记录内容数目多于护理单所有记录数目（包括历史数据）
							if (intTemp<dvContent.Count)
							{
								for (int i = intTemp; i < dvContent.Count; i++)
								{
									objInfo = new clsIntensiveTendDataInfo();
									objInfo.m_intFlag = (int)enmRecordsType.IntensiveTend_FC;//因为可肯定为观察项目记录，所以可设任意值
									objRecordContent=new clsIntensiveTendRecordContent1();
									objRecordContent.m_strRecordContent=dvContent[i]["RECORDCONTENT"].ToString();
									objRecordContent.m_strRecordContentXml=dvContent[i]["RECORDCONTENTXML"].ToString();
									objRecordContent.m_strContentModifyUserID=dvContent[i]["ModifyuserID"].ToString();
									objRecordContent.m_strContentModifyUserName=dvContent[i]["firstname"].ToString();
									objRecordContent.m_strClass=dvContent[i]["Class"].ToString();
									objRecordContent.m_dtContentCreateDate=DateTime.Parse( dvContent[i]["Createdate"].ToString());
									arlModifyData.Add(objRecordContent);
									objInfo.m_objTransDataArr = (clsIntensiveTendRecordContent1[])arlModifyData.ToArray(typeof(clsIntensiveTendRecordContent1));
									arlModifyData.Clear();
									//最后一条记录
									objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
									arlTransData.Add(objInfo);
								}
                            }
                             */
                            #endregion
                            strGlodClass =strClass;
                            dvContent.RowFilter="class='"+ strClass+"'";
						    dvContent.Sort="CreateDate asc";
                            //intTemp=0;
                        }
                        //生成 clsIntensiveTendDataInfo
						objInfo = new clsIntensiveTendDataInfo();
						objInfo.m_intFlag = (int)enmRecordsType.IntensiveTend_FC;//因为可肯定为观察项目记录，所以可设任意值
                        #region 获取同一条记录的修改记录历史数据
                        //获取同一条记录的修改记录历史数据
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()) == dtmOpenDate)
						{		
							#region 获取结果
							objRecordContent=new clsIntensiveTendRecordContent1();
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
				
							objRecordContent.m_strTemperature=dtbValue.Rows[j]["TEMPERATURE_RIGHT"].ToString();
							objRecordContent.m_strTemperatureAll=dtbValue.Rows[j]["TEMPERATURE"].ToString();
							objRecordContent.m_strTemperatureXML=dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
							objRecordContent.m_strBreath=dtbValue.Rows[j]["BREATH_RIGHT"].ToString();
							objRecordContent.m_strBreathAll=dtbValue.Rows[j]["BREATH"].ToString();
							objRecordContent.m_strBreathXML=dtbValue.Rows[j]["BREATHXML"].ToString();
							objRecordContent.m_strPulse=dtbValue.Rows[j]["PULSE_RIGHT"].ToString();
							objRecordContent.m_strPulseAll=dtbValue.Rows[j]["PULSE"].ToString();
							objRecordContent.m_strPulseXML=dtbValue.Rows[j]["PULSEXML"].ToString();
							objRecordContent.m_strBloodPressureS=dtbValue.Rows[j]["BLOODPRESSURES_RIGHT"].ToString();
							objRecordContent.m_strBloodPressureSAll=dtbValue.Rows[j]["BLOODPRESSURES"].ToString();
							objRecordContent.m_strBloodPressureSXML=dtbValue.Rows[j]["BLOODPRESSURESXML"].ToString();
							objRecordContent.m_strBloodPressureA=dtbValue.Rows[j]["BLOODPRESSUREA_RIGHT"].ToString();
							objRecordContent.m_strBloodPressureAAll=dtbValue.Rows[j]["BLOODPRESSUREA"].ToString();
							objRecordContent.m_strBloodPressureAXML=dtbValue.Rows[j]["BLOODPRESSUREAXML"].ToString();
							objRecordContent.m_strPupilLeft=dtbValue.Rows[j]["PUPILLEFT_RIGHT"].ToString();
							objRecordContent.m_strPupilLeftAll=dtbValue.Rows[j]["PUPILLEFT"].ToString();
							objRecordContent.m_strPupilLeftXML=dtbValue.Rows[j]["PUPILLEFTXML"].ToString();
							objRecordContent.m_strPupilRight=dtbValue.Rows[j]["PUPILRIGHT_RIGHT"].ToString();
							objRecordContent.m_strPupilRightAll=dtbValue.Rows[j]["PUPILRIGHT"].ToString();
							objRecordContent.m_strPupilRightXML=dtbValue.Rows[j]["PUPILRIGHTXML"].ToString();
							objRecordContent.m_strEchoLeft=dtbValue.Rows[j]["ECHOLEFT_RIGHT"].ToString();
							objRecordContent.m_strEchoLeftAll=dtbValue.Rows[j]["ECHOLEFT"].ToString();
							objRecordContent.m_strEchoLeftXML=dtbValue.Rows[j]["ECHOLEFTXML"].ToString();
							objRecordContent.m_strEchoRight=dtbValue.Rows[j]["ECHORIGHT_RIGHT"].ToString();
							objRecordContent.m_strEchoRightAll=dtbValue.Rows[j]["ECHORIGHT"].ToString();
							objRecordContent.m_strEchoRightXML=dtbValue.Rows[j]["ECHORIGHTXML"].ToString();
							objRecordContent.m_intInD=Convert.ToInt32(dtbValue.Rows[j]["IND_RIGHT"]);
							objRecordContent.m_strInDAll=dtbValue.Rows[j]["IND"].ToString();
							objRecordContent.m_strInDXML=dtbValue.Rows[j]["INDXML"].ToString();
							objRecordContent.m_intInI=Convert.ToInt32(dtbValue.Rows[j]["INI_RIGHT"]);
							objRecordContent.m_strInIAll=dtbValue.Rows[j]["INI"].ToString();
							objRecordContent.m_strInIXML=dtbValue.Rows[j]["INIXML"].ToString();
							objRecordContent.m_intOutU=Convert.ToInt32(dtbValue.Rows[j]["OUTU_RIGHT"]);
							objRecordContent.m_strOutUAll=dtbValue.Rows[j]["OUTU"].ToString();
							objRecordContent.m_strOutUXML=dtbValue.Rows[j]["OUTUXML"].ToString();
							objRecordContent.m_intOutS=Convert.ToInt32(dtbValue.Rows[j]["OUTS_RIGHT"]);
							objRecordContent.m_strOutSAll=dtbValue.Rows[j]["OUTS"].ToString();
							objRecordContent.m_strOutSXML=dtbValue.Rows[j]["OUTSXML"].ToString();
							objRecordContent.m_intOutV=Convert.ToInt32(dtbValue.Rows[j]["OUTV_RIGHT"]);
							objRecordContent.m_strOutVAll=dtbValue.Rows[j]["OUTV"].ToString();
							objRecordContent.m_strOutVXML=dtbValue.Rows[j]["OUTVXML"].ToString();
							objRecordContent.m_intOutE=Convert.ToInt32(dtbValue.Rows[j]["OUTE_RIGHT"]);
							objRecordContent.m_strOutEAll=dtbValue.Rows[j]["OUTE"].ToString();
							objRecordContent.m_strOutEXML=dtbValue.Rows[j]["OUTEXML"].ToString();
							
							objRecordContent.m_strMind=dtbValue.Rows[j]["Mind_RIGHT"].ToString();
							objRecordContent.m_strMindAll=dtbValue.Rows[j]["Mind"].ToString();
							objRecordContent.m_strMindXML=dtbValue.Rows[j]["MindXML"].ToString();
							objRecordContent.m_strClass=dtbValue.Rows[j]["Class"].ToString();
                            objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[j]["BloodOxygenSaturation_RIGHT"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[j]["BloodOxygenSaturation"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[j]["BloodOxygenSaturationXML"].ToString();
							#endregion
                            arlModifyData.Add(objRecordContent);
							#region 对病程记录内容特别处理 屏蔽--wf20080117
                            /*
							if (intTemp<dvContent.Count)
							{
								objRecordContent.m_strRecordContent=dvContent[intTemp]["RECORDCONTENT"].ToString();
								objRecordContent.m_strRecordContentXml=dvContent[intTemp]["RECORDCONTENTXML"].ToString();
								objRecordContent.m_strContentModifyUserID=dvContent[intTemp]["ModifyuserID"].ToString();
								objRecordContent.m_strContentModifyUserName=dvContent[intTemp]["firstname"].ToString();
								objRecordContent.m_dtContentCreateDate=DateTime.Parse(dvContent[intTemp]["Createdate"].ToString());
								arlModifyData.Add(objRecordContent);
								intTemp++;
							}
							else
							{
								objRecordContent.m_strRecordContent="";
								objRecordContent.m_strRecordContentXml="";
								objRecordContent.m_strContentModifyUserName="";
								objRecordContent.m_dtContentCreateDate=DateTime.Parse("1900-01-01");
								arlModifyData.Add(objRecordContent);
							}
                             */
							#endregion
							j++;
                        }
                        #endregion
						objInfo.m_objTransDataArr = (clsIntensiveTendRecordContent1[])arlModifyData.ToArray(typeof(clsIntensiveTendRecordContent1));
						arlModifyData.Clear();
						//最后一条记录
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
                        #region 对病程记录内容的处理--wf20080117
                        DateTime dtmRecordDate = objInfo.m_objRecordContent.m_dtmCreateDate;
                        DateTime dtmNextRecordDate;
                        ArrayList arlCourseDiseasesRecord = new ArrayList();
                        clsCourseDiseasesRecord objDiseasesRecordContent = null;
                        if (j < dtbValue.Rows.Count)
                        {
                            dtmNextRecordDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                            for (int k = 0; k < dvContent.Count; k++)
                            {
                                /* 循环同一天内的病程记录，如果病程记录的记录时间介于本条护理记录的
                                 * 记录时间和下一条护理记录的记录时间之间，则这条病程记录属于本条护
                                 * 理记录
                                 */
                                objDiseasesRecordContent = new clsCourseDiseasesRecord();
                                if (DateTime.Parse(dvContent[k]["Createdate"].ToString()) > dtmRecordDate &&
                                   DateTime.Parse(dvContent[k]["Createdate"].ToString()) < dtmNextRecordDate)
                                {
                                    objDiseasesRecordContent.m_strDiseasesRecordContent = dvContent[k]["RECORDCONTENT"].ToString();
                                    objDiseasesRecordContent.m_strDiseasesRecordContentXml = dvContent[k]["RECORDCONTENTXML"].ToString();
                                    objDiseasesRecordContent.m_strClass = dvContent[k]["CLASS"].ToString();
                                    objDiseasesRecordContent.m_strModifyUserID = dvContent[k]["ModifyuserID"].ToString();
                                    objDiseasesRecordContent.m_strModifyUserName = dvContent[k]["firstname"].ToString();
                                    objDiseasesRecordContent.m_dtmCreateDate = DateTime.Parse(dvContent[k]["Createdate"].ToString());
                                    arlCourseDiseasesRecord.Add(objDiseasesRecordContent);
                                }
                            }
                            objInfo.m_objCourseDiseasesRecordArr = (clsCourseDiseasesRecord[])arlCourseDiseasesRecord.ToArray(typeof(clsCourseDiseasesRecord));
                        }
                        else
                        {
                            for (int m = 0; m < dvContent.Count; m++)
                            {
                                /* 对于最后一条护理记录，循环同一天内的病程记录，如果病程记录的记录时间大
                                 * 于本条护理记录的，则这条病程记录属于本条护理记录
                                 */
                                objDiseasesRecordContent = new clsCourseDiseasesRecord();
                                if (DateTime.Parse(dvContent[m]["Createdate"].ToString()) > dtmRecordDate)
                                {
                                    objDiseasesRecordContent.m_strDiseasesRecordContent = dvContent[m]["RECORDCONTENT"].ToString();
                                    objDiseasesRecordContent.m_strDiseasesRecordContentXml = dvContent[m]["RECORDCONTENTXML"].ToString();
                                    objDiseasesRecordContent.m_strClass = dvContent[m]["CLASS"].ToString();
                                    objDiseasesRecordContent.m_strModifyUserID = dvContent[m]["ModifyuserID"].ToString();
                                    objDiseasesRecordContent.m_strModifyUserName = dvContent[m]["firstname"].ToString();
                                    objDiseasesRecordContent.m_dtmCreateDate = DateTime.Parse(dvContent[m]["Createdate"].ToString());
                                    arlCourseDiseasesRecord.Add(objDiseasesRecordContent);
                                }
                            }
                            objInfo.m_objCourseDiseasesRecordArr = (clsCourseDiseasesRecord[])arlCourseDiseasesRecord.ToArray(typeof(clsCourseDiseasesRecord));
                        }
                        #endregion
                        arlTransData.Add(objInfo);
                        //后移一条记录，使循环从新的OpenDate开始。
						j--;
                    }
                    #region 屏蔽--wf20080117
                    /*
                    //假如病程记录内容数目多于护理单所有记录数目(包括历史数据)
					if (intTemp<dvContent.Count)
					{
						for (int i = intTemp; i < dvContent.Count; i++)
						{
							objInfo = new clsIntensiveTendDataInfo();
							objInfo.m_intFlag = (int)enmRecordsType.IntensiveTend_FC;//因为可肯定为观察项目记录，所以可设任意值
							objRecordContent=new clsIntensiveTendRecordContent1();
							objRecordContent.m_strRecordContent=dvContent[i]["RECORDCONTENT"].ToString();
							objRecordContent.m_strRecordContentXml=dvContent[i]["RECORDCONTENTXML"].ToString();
							objRecordContent.m_strContentModifyUserID=dvContent[i]["ModifyuserID"].ToString();
							objRecordContent.m_strContentModifyUserName=dvContent[i]["firstname"].ToString();
							objRecordContent.m_strClass=dvContent[i]["Class"].ToString();
							objRecordContent.m_dtContentCreateDate=DateTime.Parse( dvContent[i]["Createdate"].ToString());
							arlModifyData.Add(objRecordContent);
							objInfo.m_objTransDataArr = (clsIntensiveTendRecordContent1[])arlModifyData.ToArray(typeof(clsIntensiveTendRecordContent1));
							arlModifyData.Clear();
							//最后一条记录
							objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];

							arlTransData.Add(objInfo);
						}
                    }
                     */
                    #endregion
                    string strClassDay=string.Empty;
					strClass = ((clsIntensiveTendRecordContent1)((clsIntensiveTendDataInfo)arlTransData[0]).m_objTransDataArr[0]).m_strClass;
					arlTransDataClone = (ArrayList)arlTransData.Clone();
					if(arlTransData.Count == 1)//只有一条记录时
					{
						#region 统计班次(一班即一天)记录总共内容
						objAppendInfo = new clsIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = 0;
						objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryDayArr[0].m_strCreateDate + " 07:59:59.998");
						objAppendInfo.m_objItemSummary = m_objSummaryDayArr[0];
						arlTransDataClone.Add(objAppendInfo);
						#endregion

                        #region 屏蔽
                        /*
                        #region 统计当天班次记录的总共的内容
                        int m_intSummaryDayArrIndex = 0;
						objAppendInfo = new clsIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = -1;
						objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryDayArr[m_intSummaryDayArrIndex].m_strCreateDate + " 23:59:59.998");
						objAppendInfo.m_objItemSummary = m_objSummaryDayArr[m_objSummaryDayArr.Length-1];
						arlTransDataClone.Add(objAppendInfo);
						
						#endregion ;

						#region  统计全部记录总共的内容
						objAppendInfo = new clsIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = 0;
						objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
						objAppendInfo.m_objItemSummary = m_objSummaryArr[0];
						arlTransDataClone.Add(objAppendInfo);
						#endregion ;
                         */
                        #endregion

                    }
					else//多于一条记录时
					{
						try
						{
							#region 统计班次记录总共内容
							int m_intSummaryArrIndex = 0;
							for(int i1=1;i1<arlTransData.Count;i1++)
							{
								if(strClass != ((clsIntensiveTendRecordContent1)((clsIntensiveTendDataInfo)arlTransData[i1]).m_objTransDataArr[0]).m_strClass)
								{
									objAppendInfo = new clsIntensiveTendDataInfo();
									objAppendInfo.m_intFlag = 0;
									objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
									objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryDayArr[m_intSummaryArrIndex].m_strCreateDate + " 07:59:59.998");
                                    objAppendInfo.m_objItemSummary = m_objSummaryDayArr[m_intSummaryArrIndex];
									arlTransDataClone.Insert(i1+m_intSummaryArrIndex,objAppendInfo);
									strClass = ((clsIntensiveTendRecordContent1)((clsIntensiveTendDataInfo)arlTransData[i1]).m_objTransDataArr[0]).m_strClass;
									m_intSummaryArrIndex++;
								}
							}
							objAppendInfo = new clsIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = 0;
							objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
                            objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryDayArr[m_objSummaryDayArr.Length - 1].m_strCreateDate + " 07:59:59.998");
                            objAppendInfo.m_objItemSummary = m_objSummaryDayArr[m_objSummaryDayArr.Length - 1];
							arlTransDataClone.Add(objAppendInfo);
							#endregion
                            #region 屏蔽
                            /*
							#region 统计当天班次记录的总共的内容
							int m_intSummaryDayArrIndex = 0;
							int m_intsummaryLoacation=1;
							int m_intSummaryInsert=0;
							for(int i1=1;i1<arlTransDataClone.Count;i1++)
							{
								if(((clsIntensiveTendDataInfo)arlTransDataClone[i1]).m_objItemSummary!=null)
								{
									
									string strSub=((clsIntensiveTendDataInfo)arlTransDataClone[i1]).m_objItemSummary.m_strClass.Substring(0,10);
									if (strSub!=m_objSummaryDayArr[m_intSummaryDayArrIndex].m_strClass)
									{
										objAppendInfo = new clsIntensiveTendDataInfo();
										objAppendInfo.m_intFlag = -1;
										objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
										objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryDayArr[m_intSummaryDayArrIndex].m_strCreateDate + " 23:59:59.998");
										objAppendInfo.m_objItemSummary = m_objSummaryDayArr[m_intSummaryDayArrIndex];
										arlTransDataClone.Insert(m_intSummaryInsert+1,objAppendInfo);
										m_intSummaryDayArrIndex++;
										i1++;
										m_intsummaryLoacation=1+m_intsummaryLoacation;

									}
									m_intSummaryInsert=m_intsummaryLoacation;

								}
								m_intsummaryLoacation=1+m_intsummaryLoacation;


							}
							objAppendInfo = new clsIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = -1;
							objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
							objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryDayArr[m_intSummaryDayArrIndex].m_strCreateDate + " 23:59:59.998");
							objAppendInfo.m_objItemSummary = m_objSummaryDayArr[m_objSummaryDayArr.Length-1];
							arlTransDataClone.Add(objAppendInfo);


						
							#endregion ;

							#region 统计全部记录总共的内容
                            clsIntensiveTendDataInfo objTotalDataInfo;
							clsIntensiveTendRecordSummary m_objSummary = new clsIntensiveTendRecordSummary();
							for(int i1=0;i1<arlTransDataClone.Count;i1++)
							{
								objTotalDataInfo = (clsIntensiveTendDataInfo)arlTransDataClone[i1];
								if(objTotalDataInfo.m_intFlag == -1)
								{
									m_objSummary.m_intTotal_In+=objTotalDataInfo.m_objItemSummary.m_intTotal_In;
									m_objSummary.m_intTotal_Out+=objTotalDataInfo.m_objItemSummary.m_intTotal_Out;
								}
							}
							objAppendInfo = new clsIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = 0;
							objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
							objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
							objAppendInfo.m_objItemSummary = m_objSummary;
							arlTransDataClone.Add(objAppendInfo);
							
							#endregion ;
                             */
#endregion

                        }
						catch(Exception err)
						{
							string m_Str = err.Message + "\r\n" + err.StackTrace;
						}
					}
				}
				//返回结果到p_objTansDataInfoArr
				p_objIntensiveTendInfoArr = (clsIntensiveTendDataInfo[])arlTransDataClone.ToArray(typeof(clsIntensiveTendDataInfo));
				
				#region 屏蔽

				//				for(int w2=0;w2<p_objIntensiveTendInfoArr.Length;w2++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
//				{
//					clsIntensiveTendRecordContent1[] objTempAInfoArr = ((clsIntensiveTendDataInfo)p_objIntensiveTendInfoArr[w2]).m_objTransDataArr;
//					if(objTempAInfoArr != null)
//					{
//						for(int w3=w2+1;w3<p_objIntensiveTendInfoArr.Length;w3++)
//						{
//							clsIntensiveTendRecordContent1[] objTempBInfoArr = ((clsIntensiveTendDataInfo)p_objIntensiveTendInfoArr[w3]).m_objTransDataArr;
//							if(objTempBInfoArr == null) continue;
//							if(objTempAInfoArr[objTempAInfoArr.Length-1].m_dtmCreateDate.Date == objTempBInfoArr[0].m_dtmCreateDate.Date)
//							{
//								string strTempName = "";
//								for(int w4=0;w4<objTempBInfoArr.Length;w4++)
//								{
//									if(objTempBInfoArr[w4].m_strModifyUserName != "　")//全角空格字符
//									{
//										strTempName = objTempBInfoArr[w4].m_strModifyUserName;
//										break;
//									}
//								}
//								if(objTempAInfoArr[objTempAInfoArr.Length-1].m_strModifyUserName == strTempName)
//									objTempAInfoArr[objTempAInfoArr.Length-1].m_strModifyUserName = "　";//全角空格字符
//								break;
//							}
//							else break;
//						}
//					}
//				}

				#endregion ;
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
		/// 获取指定记录的内容(SQLSERVER)
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objIntensiveTendInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected   long m_lngGetTransDataInfoArrWithServSqlServer(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objIntensiveTendInfoArr)
		{
			
			p_objIntensiveTendInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
			try
			{
				ArrayList arlTransData = new ArrayList();  
				ArrayList arlModifyData = new ArrayList();       //获取同一条记录的修改记录历史数据
				ArrayList arlTransDataClone = new ArrayList();
				clsIntensiveTendDataInfo objAppendInfo = null;
				DateTime dtmOpenDate;
				DateTime dtmCreateDate_Date;
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);		
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					clsIntensiveTendRecordContent1 objRecordContent= null;
					clsIntensiveTendDataInfo objInfo = null;
					clsIntensiveTendRecordSummary[] m_objSummaryArr;
					long m_lngRes = m_lngGetSummaryRecords(p_strInPatientID,p_strInPatientDate,p_objHRPServ,out m_objSummaryArr);
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//生成 clsIntensiveTendDataInfo
						objInfo = new clsIntensiveTendDataInfo();
						objInfo.m_intFlag = (int)enmRecordsType.IntensiveTend;//因为可肯定为观察项目记录，所以可设任意值
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate  = DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString());
						//获取同一条记录的修改记录历史数据
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE_MAIN"].ToString()) == dtmOpenDate)
						{		
							#region 从DataTable.Rows中获取结果
							objRecordContent=new clsIntensiveTendRecordContent1();
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
				
							objRecordContent.m_strTemperature=dtbValue.Rows[j]["TEMPERATURE_RIGHT"].ToString();
							objRecordContent.m_strTemperatureAll=dtbValue.Rows[j]["TEMPERATURE"].ToString();
							objRecordContent.m_strTemperatureXML=dtbValue.Rows[j]["TEMPERATUREXML"].ToString();
							objRecordContent.m_strBreath=dtbValue.Rows[j]["BREATH_RIGHT"].ToString();
							objRecordContent.m_strBreathAll=dtbValue.Rows[j]["BREATH"].ToString();
							objRecordContent.m_strBreathXML=dtbValue.Rows[j]["BREATHXML"].ToString();
							objRecordContent.m_strPulse=dtbValue.Rows[j]["PULSE_RIGHT"].ToString();
							objRecordContent.m_strPulseAll=dtbValue.Rows[j]["PULSE"].ToString();
							objRecordContent.m_strPulseXML=dtbValue.Rows[j]["PULSEXML"].ToString();
							objRecordContent.m_strBloodPressureS=dtbValue.Rows[j]["BLOODPRESSURES_RIGHT"].ToString();
							objRecordContent.m_strBloodPressureSAll=dtbValue.Rows[j]["BLOODPRESSURES"].ToString();
							objRecordContent.m_strBloodPressureSXML=dtbValue.Rows[j]["BLOODPRESSURESXML"].ToString();
							objRecordContent.m_strBloodPressureA=dtbValue.Rows[j]["BLOODPRESSUREA_RIGHT"].ToString();
							objRecordContent.m_strBloodPressureAAll=dtbValue.Rows[j]["BLOODPRESSUREA"].ToString();
							objRecordContent.m_strBloodPressureAXML=dtbValue.Rows[j]["BLOODPRESSUREAXML"].ToString();
							objRecordContent.m_strPupilLeft=dtbValue.Rows[j]["PUPILLEFT_RIGHT"].ToString();
							objRecordContent.m_strPupilLeftAll=dtbValue.Rows[j]["PUPILLEFT"].ToString();
							objRecordContent.m_strPupilLeftXML=dtbValue.Rows[j]["PUPILLEFTXML"].ToString();
							objRecordContent.m_strPupilRight=dtbValue.Rows[j]["PUPILRIGHT_RIGHT"].ToString();
							objRecordContent.m_strPupilRightAll=dtbValue.Rows[j]["PUPILRIGHT"].ToString();
							objRecordContent.m_strPupilRightXML=dtbValue.Rows[j]["PUPILRIGHTXML"].ToString();
							objRecordContent.m_strEchoLeft=dtbValue.Rows[j]["ECHOLEFT_RIGHT"].ToString();
							objRecordContent.m_strEchoLeftAll=dtbValue.Rows[j]["ECHOLEFT"].ToString();
							objRecordContent.m_strEchoLeftXML=dtbValue.Rows[j]["ECHOLEFTXML"].ToString();
							objRecordContent.m_strEchoRight=dtbValue.Rows[j]["ECHORIGHT_RIGHT"].ToString();
							objRecordContent.m_strEchoRightAll=dtbValue.Rows[j]["ECHORIGHT"].ToString();
							objRecordContent.m_strEchoRightXML=dtbValue.Rows[j]["ECHORIGHTXML"].ToString();
							objRecordContent.m_intInD=Convert.ToInt32(dtbValue.Rows[j]["IND_RIGHT"]);
							objRecordContent.m_strInDAll=dtbValue.Rows[j]["IND"].ToString();
							objRecordContent.m_strInDXML=dtbValue.Rows[j]["INDXML"].ToString();
							objRecordContent.m_intInI=Convert.ToInt32(dtbValue.Rows[j]["INI_RIGHT"]);
							objRecordContent.m_strInIAll=dtbValue.Rows[j]["INI"].ToString();
							objRecordContent.m_strInIXML=dtbValue.Rows[j]["INIXML"].ToString();
							objRecordContent.m_intOutU=Convert.ToInt32(dtbValue.Rows[j]["OUTU_RIGHT"]);
							objRecordContent.m_strOutUAll=dtbValue.Rows[j]["OUTU"].ToString();
							objRecordContent.m_strOutUXML=dtbValue.Rows[j]["OUTUXML"].ToString();
							objRecordContent.m_intOutS=Convert.ToInt32(dtbValue.Rows[j]["OUTS_RIGHT"]);
							objRecordContent.m_strOutSAll=dtbValue.Rows[j]["OUTS"].ToString();
							objRecordContent.m_strOutSXML=dtbValue.Rows[j]["OUTSXML"].ToString();
							objRecordContent.m_intOutV=Convert.ToInt32(dtbValue.Rows[j]["OUTV_RIGHT"]);
							objRecordContent.m_strOutVAll=dtbValue.Rows[j]["OUTV"].ToString();
							objRecordContent.m_strOutVXML=dtbValue.Rows[j]["OUTVXML"].ToString();
							objRecordContent.m_intOutE=Convert.ToInt32(dtbValue.Rows[j]["OUTE_RIGHT"]);
							objRecordContent.m_strOutEAll=dtbValue.Rows[j]["OUTE"].ToString();
							objRecordContent.m_strOutEXML=dtbValue.Rows[j]["OUTEXML"].ToString();
                            //objRecordContent.m_strRecordContent=dtbValue.Rows[j]["RECORDCONTENT"].ToString();
                            //objRecordContent.m_strRecordContent_Right=dtbValue.Rows[j]["RECORDCONTENT_RIGHT"].ToString();
                            //objRecordContent.m_strRecordContentXml=dtbValue.Rows[j]["RECORDCONTENTXML"].ToString();
							objRecordContent.m_strMind=dtbValue.Rows[j]["Mind_RIGHT"].ToString();
							objRecordContent.m_strMindAll=dtbValue.Rows[j]["Mind"].ToString();
							objRecordContent.m_strMindXML=dtbValue.Rows[j]["MindXML"].ToString();
							objRecordContent.m_strClass=dtbValue.Rows[j]["Class"].ToString();
                            objRecordContent.m_strBloodOxygenSaturation = dtbValue.Rows[j]["BloodOxygenSaturation_RIGHT"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationAll = dtbValue.Rows[j]["BloodOxygenSaturation"].ToString();
                            objRecordContent.m_strBloodOxygenSaturationXML = dtbValue.Rows[j]["BloodOxygenSaturationXML"].ToString();
							//同一条记录的修改,保存在arlModifyData 
							arlModifyData.Add(objRecordContent);
							j++;
							#endregion 
						}       
						//后移一条记录，使循环从新的OpenData开始。
						j--;
					
						objInfo.m_objTransDataArr = (clsIntensiveTendRecordContent1[])arlModifyData.ToArray(typeof(clsIntensiveTendRecordContent1));
					
						for(int w1=1;w1<objInfo.m_objTransDataArr.Length;w1++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
						{
							if(objInfo.m_objTransDataArr[w1-1].m_strModifyUserName == objInfo.m_objTransDataArr[w1].m_strModifyUserName)
								objInfo.m_objTransDataArr[w1-1].m_strModifyUserName = "　";//全角空格字符
						}
						arlModifyData.Clear();
			
						//最后一条记录
						objInfo.m_objRecordContent = objInfo.m_objTransDataArr[objInfo.m_objTransDataArr.Length-1];
				
						arlTransData.Add(objInfo);
					}

					dtmCreateDate_Date = ((clsIntensiveTendDataInfo)arlTransData[0]).m_objRecordContent.m_dtmCreateDate.Date;
					arlTransDataClone = (ArrayList)arlTransData.Clone();
					if(arlTransData.Count == 1)//只有一条记录时
					{
						objAppendInfo = new clsIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = 0;
						objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[0].m_strCreateDate + " 23:59:59.998");
						objAppendInfo.m_objItemSummary = m_objSummaryArr[0];
						arlTransDataClone.Add(objAppendInfo);

						//###########统计全部记录总共的内容
						objAppendInfo = new clsIntensiveTendDataInfo();
						objAppendInfo.m_intFlag = 0;
						objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
						objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
						objAppendInfo.m_objItemSummary = m_objSummaryArr[0];
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
								if(dtmCreateDate_Date != ((clsIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date)
								{
									objAppendInfo = new clsIntensiveTendDataInfo();
									objAppendInfo.m_intFlag = 0;
									objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
									objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[m_intSummaryArrIndex].m_strCreateDate + " 23:59:59.998");
									objAppendInfo.m_objItemSummary = m_objSummaryArr[m_intSummaryArrIndex];
									arlTransDataClone.Insert(i1+m_intSummaryArrIndex,objAppendInfo);
									dtmCreateDate_Date = ((clsIntensiveTendDataInfo)arlTransData[i1]).m_objRecordContent.m_dtmCreateDate.Date;
									m_intSummaryArrIndex++;
								}
							}
							objAppendInfo = new clsIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = 0;
							objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
							objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.Parse(m_objSummaryArr[m_objSummaryArr.Length-1].m_strCreateDate + " 23:59:59.998");
							objAppendInfo.m_objItemSummary = m_objSummaryArr[m_objSummaryArr.Length-1];
							arlTransDataClone.Add(objAppendInfo);

							//###########统计全部记录总共的内容
							clsIntensiveTendDataInfo objTotalDataInfo;
							clsIntensiveTendRecordSummary m_objSummary = new clsIntensiveTendRecordSummary();
							for(int i1=0;i1<arlTransDataClone.Count;i1++)
							{
								objTotalDataInfo = (clsIntensiveTendDataInfo)arlTransDataClone[i1];
								if(objTotalDataInfo.m_intFlag != (int)enmRecordsType.IntensiveTend)
								{
									m_objSummary.m_intTotal_In+=objTotalDataInfo.m_objItemSummary.m_intTotal_In;
									m_objSummary.m_intTotal_Out+=objTotalDataInfo.m_objItemSummary.m_intTotal_Out;
								}
							}
							objAppendInfo = new clsIntensiveTendDataInfo();
							objAppendInfo.m_intFlag = 0;
							objAppendInfo.m_objRecordContent = new clsIntensiveTendRecordContent1();
							objAppendInfo.m_objRecordContent.m_dtmCreateDate = DateTime.MaxValue;
							objAppendInfo.m_objItemSummary = m_objSummary;
							arlTransDataClone.Add(objAppendInfo);
							//#############
						}
						catch(Exception err)
						{
							string m_Str = err.Message + "\r\n" + err.StackTrace;
						}
					}
				}
				//返回结果到p_objTansDataInfoArr
				p_objIntensiveTendInfoArr = (clsIntensiveTendDataInfo[])arlTransDataClone.ToArray(typeof(clsIntensiveTendDataInfo));
				for(int w2=0;w2<p_objIntensiveTendInfoArr.Length;w2++)//签名控制：同一天同一个人的记录只需显示最后一个签名（HB）
				{
					clsIntensiveTendRecordContent1[] objTempAInfoArr = ((clsIntensiveTendDataInfo)p_objIntensiveTendInfoArr[w2]).m_objTransDataArr;
					if(objTempAInfoArr != null)
					{
						for(int w3=w2+1;w3<p_objIntensiveTendInfoArr.Length;w3++)
						{
							clsIntensiveTendRecordContent1[] objTempBInfoArr = ((clsIntensiveTendDataInfo)p_objIntensiveTendInfoArr[w3]).m_objTransDataArr;
							if(objTempBInfoArr == null) continue;
							if(objTempAInfoArr[objTempAInfoArr.Length-1].m_dtmCreateDate.Date == objTempBInfoArr[0].m_dtmCreateDate.Date)
							{
								string strTempName = "";
								for(int w4=0;w4<objTempBInfoArr.Length;w4++)
								{
									if(objTempBInfoArr[w4].m_strModifyUserName != "　")//全角空格字符
									{
										strTempName = objTempBInfoArr[w4].m_strModifyUserName;
										break;
									}
								}
								if(objTempAInfoArr[objTempAInfoArr.Length-1].m_strModifyUserName == strTempName)
									objTempAInfoArr[objTempAInfoArr.Length-1].m_strModifyUserName = "　";//全角空格字符
								break;
							}
							else break;
						}
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
			//根据不同的子表单，获取不同的SQL语句
			string strSQL = null;
			/// <summary>
		///  从IntensiveTendRecordContent1获取指定表单的最后修改时间。
		/// </summary>
		string c_strCheckLastModifyRecordSQL=clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from 
											intensivetendrecord1 t1,intensivetendrecordcontent1 t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status =0
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			switch((enmDiseaseTrackType)p_intRecordType)
			{
				case enmDiseaseTrackType.IntensiveTend:
					strSQL = c_strCheckLastModifyRecordSQL;
					break;
				case enmDiseaseTrackType.IntensiveTend_FC:
					strSQL = c_strCheckLastModifyRecordSQL;
					break;

				default: return (long)enmOperationResult.Parameter_Error; 
			}
		
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
				//按顺序给IDataParameter赋值
				//			for(int i=0;i<objDPArr.Length;i++)
				//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
					//string strSQL2 = "select DeActivedDate,DeActivedOperatorID from IntensiveTendRecord1 Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
					//根据不同的子表单，获取不同的SQL语句
					string strSQL2 = null;
					switch((enmDiseaseTrackType)p_intRecordType)
					{
						case enmDiseaseTrackType.IntensiveTend:
							strSQL2 = c_strGetDeleteRecordSQL;
							break;

						default: return (long)enmOperationResult.Parameter_Error; 
					}

					//按顺序给IDataParameter赋值
//					for(int i=0;i<objDPArr.Length;i++)//必须重新分配内存,即使是相同的内容
//						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
					p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
					objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;

					lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL2,ref dtbValue,objDPArr);
				
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
			//根据不同的子表单，获取不同的SQL语句
			string strSQL = null;
			switch((enmDiseaseTrackType)p_intRecordType)
			{
				case enmDiseaseTrackType.IntensiveTend:
					strSQL = c_strDeleteRecordSQL;
					break;
				case enmDiseaseTrackType.IntensiveTend_FC:
					strSQL = c_strDeleteRecordSQL;
					break;

				default: return (long)enmOperationResult.Parameter_Error; 
			}
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
				lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
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
			out clsIntensiveTendRecordSummary[] p_objSummaryItemInfoArr)
		{
			p_objSummaryItemInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			string strSql = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
                strSql = @"select createdate_date,
       sum(total_in) as total_in,
       sum(total_out) as total_out
  from (select (to_date(to_char(v2.createdate, 'yyyy-mm-dd'), 'yyyy-mm-dd')) as createdate_date,
               v2.createdate,
               (v2.ind_right + v2.ini_right) as total_in,
               (v2.outu_right + v2.outs_right + v2.outv_right +
               v2.oute_right) as total_out
          from (select opendate, max(modifydate) as lastmodifydate
                  from intensivetendrecordcontent1
                 where inpatientid = ?
                   and inpatientdate = ?
                 group by opendate) v1,
               (select t1.createdate,
                       t2.inpatientid,
                       t2.inpatientdate,
                       t2.opendate,
                       t2.modifydate,
                       t2.ind_right,
                       t2.ini_right,
                       t2.outu_right,
                       t2.outv_right,
                       t2.outs_right,
                       t2.oute_right
                  from intensivetendrecord1        t1,
                       intensivetendrecordcontent1 t2
                 where t1.inpatientid = ?
                   and t1.inpatientdate = ?
                   and t1.inpatientid = t2.inpatientid
                   and t1.inpatientdate = t2.inpatientdate
                   and t1.opendate = t2.opendate
                   and status = 0) v2
         where v2.inpatientid = ?
           and v2.inpatientdate = ?
           and v1.opendate = v2.opendate
           and v1.lastmodifydate = v2.modifydate) v3
 group by createdate_date
 order by createdate_date";
			}
			else
			{
                strSql = @"select createdate_date,
       sum(total_in) as total_in,
       sum(total_out) as total_out
  from (select convert(char(10), v2.createdate, 120) as createdate_date,
               v2.createdate,
               (v2.ind_right + v2.ini_right) as total_in,
               (v2.outu_right + v2.outs_right + v2.outv_right +
               v2.oute_right) as total_out
          from -------------------
               (select opendate, max(modifydate) as lastmodifydate
                  from intensivetendrecordcontent1
                 where inpatientid = ?
                   and inpatientdate = ?
                 group by opendate) v1,
               -------------------
               (select t1.createdate,
                       t2.inpatientid,
                       t2.inpatientdate,
                       t2.opendate,
                       t2.modifydate,
                       t2.ind_right,
                       t2.ini_right,
                       t2.outu_right,
                       t2.outv_right,
                       t2.outs_right,
                       t2.oute_right
                  from intensivetendrecord1        t1,
                       intensivetendrecordcontent1 as t2
                 where t1.inpatientid = ?
                   and t1.inpatientdate = ?
                   and t1.inpatientid = t2.inpatientid
                   and t1.inpatientdate = t2.inpatientdate
                   and t1.opendate = t2.opendate
                   and status = 0) v2
        --------------------------
         where v2.inpatientid = ?
           and v2.inpatientdate = ?
           and v1.opendate = v2.opendate
           and v1.lastmodifydate = v2.modifydate) v3
 group by createdate_date
 order by createdate_date";
			}

            IDataParameter[] objDPArr = null;
            p_objHRPServ.CreateDatabaseParameter(6, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].Value = p_strInPatientID;
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[4].Value = p_strInPatientID;
            objDPArr[5].DbType = DbType.DateTime;
            objDPArr[5].Value = DateTime.Parse(p_strInPatientDate);


			DataTable dtbValue = new DataTable();
			//执行查询，填充结果到DataTable       
//			long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetSummarySQL,ref dtbValue,objDPArr);
            long lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
			//循环DataTable
			if(lngRes > 0 && dtbValue.Rows.Count >0)
			{
				p_objSummaryItemInfoArr = new clsIntensiveTendRecordSummary[dtbValue.Rows.Count];
				for(int i1=0;i1<dtbValue.Rows.Count;i1++)
				{
					p_objSummaryItemInfoArr[i1] = new clsIntensiveTendRecordSummary();
					p_objSummaryItemInfoArr[i1].m_strCreateDate = dtbValue.Rows[i1]["CREATEDATE_DATE"].ToString();
//					p_objSummaryItemInfoArr[i1].m_intInD_Total = Convert.ToInt32(dtbValue.Rows[i1]["IND_TOTAL"]);
//					p_objSummaryItemInfoArr[i1].m_intInI_Total = Convert.ToInt32(dtbValue.Rows[i1]["INI_TOTAL"]);
//					p_objSummaryItemInfoArr[i1].m_intOutU_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTU_TOTAL"]);
//					p_objSummaryItemInfoArr[i1].m_intOutS_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTS_TOTAL"]);
//					p_objSummaryItemInfoArr[i1].m_intOutV_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTV_TOTAL"]);
//					p_objSummaryItemInfoArr[i1].m_intOutE_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTE_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_In = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_IN"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_Out = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_OUT"]);
				}
			}

			return (long)enmOperationResult.DB_Succeed;
		}


		#region 按班统计＋按日统计
		/// <summary>
		/// 获得统计记录内容new(按班统计)
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objSummaryItemInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetSummaryRecordsNew(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsIntensiveTendRecordSummary[] p_objSummaryItemInfoArr)
		{
			p_objSummaryItemInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			string strSql = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
                strSql = @"select class, sum(total_in) as total_in, sum(total_out) as total_out
  from (select t1.createdate,
               (t2.ind_right + t2.ini_right) as total_in,
               (t2.outu_right + t2.outs_right + t2.outv_right +
               t2.oute_right) as total_out,
               t1.class
          from intensivetendrecord1 t1, intensivetendrecordcontent1 t2
         where t1.inpatientid = ?
           and t1.inpatientdate = ?
           and t1.inpatientid = t2.inpatientid
           and t1.inpatientdate = t2.inpatientdate
           and t1.opendate = t2.opendate
           and t2.modifydate =
               (select max(modifydate)
                  from intensivetendrecordcontent1
                 where inpatientid = ?
                   and inpatientdate = ?
                   and t1.opendate = opendate)
           and status = 0
         order by t1.createdate, t2.modifydate)
 group by class";
			}
			else
			{
					
			}
            IDataParameter[] objDPArr = null;
            p_objHRPServ.CreateDatabaseParameter(4, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].Value = p_strInPatientID;
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

			DataTable dtbValue = new DataTable();
			//执行查询，填充结果到DataTable       
			//			long lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetSummarySQL,ref dtbValue,objDPArr);
            long lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
			//循环DataTable
			if(lngRes > 0 && dtbValue.Rows.Count >0)
			{
				p_objSummaryItemInfoArr = new clsIntensiveTendRecordSummary[dtbValue.Rows.Count];
				for(int i1=0;i1<dtbValue.Rows.Count;i1++)
				{
					p_objSummaryItemInfoArr[i1] = new clsIntensiveTendRecordSummary();
					p_objSummaryItemInfoArr[i1].m_strClass = dtbValue.Rows[i1]["Class"].ToString();
					//					p_objSummaryItemInfoArr[i1].m_intInD_Total = Convert.ToInt32(dtbValue.Rows[i1]["IND_TOTAL"]);
					//					p_objSummaryItemInfoArr[i1].m_intInI_Total = Convert.ToInt32(dtbValue.Rows[i1]["INI_TOTAL"]);
					//					p_objSummaryItemInfoArr[i1].m_intOutU_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTU_TOTAL"]);
					//					p_objSummaryItemInfoArr[i1].m_intOutS_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTS_TOTAL"]);
					//					p_objSummaryItemInfoArr[i1].m_intOutV_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTV_TOTAL"]);
					//					p_objSummaryItemInfoArr[i1].m_intOutE_Total = Convert.ToInt32(dtbValue.Rows[i1]["OUTE_TOTAL"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_In = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_IN"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_Out = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_OUT"]);
				}
			}

			return (long)enmOperationResult.DB_Succeed;
		}


		/// <summary>
		/// 获得统计记录内容new(按天统计（一班即一天）)
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objSummaryItemInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetSummaryRecordsNewDay(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsIntensiveTendRecordSummary[] p_objSummaryItemInfoArr)
		{
			p_objSummaryItemInfoArr=null;
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
 			string strSql = "";
			if(clsHRPTableService.bytDatabase_Selector == 2)
			{
				strSql = @"select classday, sum(total_in) as total_in, sum(total_out) as total_out
							from (select (t2.ind_right + t2.ini_right) as total_in,
								(t2.outu_right + t2.outs_right + t2.outv_right + t2.oute_right) as total_out,
								substr(t1.class, 1, 10) as classday
							from intensivetendrecord1 t1, intensivetendrecordcontent1 t2
							where t1.inpatientid ='"+p_strInPatientID
							 +"'and t1.inpatientdate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@" 
							and t1.inpatientid = t2.inpatientid
							and t1.inpatientdate = t2.inpatientdate
							and t1.opendate = t2.opendate
							and t2.modifydate =
								(select max(modifydate)
									from intensivetendrecordcontent1
									where inpatientid ='"+p_strInPatientID
									+"' and inpatientdate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@" 
									and t1.opendate = opendate)
							and status =0
							order by t1.createdate, t2.modifydate)
							group by classday order by classday";
			}
			else
			{
					
			}

			DataTable dtbValue = new DataTable();
			long lngRes = p_objHRPServ.lngGetDataTableWithoutParameters(strSql,ref dtbValue);
			if(lngRes > 0 && dtbValue.Rows.Count >0)
			{
				p_objSummaryItemInfoArr = new clsIntensiveTendRecordSummary[dtbValue.Rows.Count];
				for(int i1=0;i1<dtbValue.Rows.Count;i1++)
				{
					p_objSummaryItemInfoArr[i1] = new clsIntensiveTendRecordSummary();
					p_objSummaryItemInfoArr[i1].m_strClass = dtbValue.Rows[i1]["ClassDay"].ToString();
 					p_objSummaryItemInfoArr[i1].m_intTotal_In = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_IN"]);
					p_objSummaryItemInfoArr[i1].m_intTotal_Out = Convert.ToInt32(dtbValue.Rows[i1]["TOTAL_OUT"]);
				}
			}
			return (long)enmOperationResult.DB_Succeed;
		}
		#endregion ;	
	}// END CLASS DEFINITION clsIntensiveTendMainService

}

