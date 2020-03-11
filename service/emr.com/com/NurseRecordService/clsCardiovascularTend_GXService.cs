using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsCardiovascularTend_GXService
{
	/// <summary>
	/// 心血管外科特护记录(广西)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsCardiovascularTend_GXService : clsDiseaseTrackService
	{
		#region SQL语句
		/// <summary>
		/// 从T_EMR_CARDIOVASCULARTEND_GX获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from t_emr_cardiovasculartend_gx
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// 从T_EMR_CARDIOVASCULARTEND_GX中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from t_emr_cardiovasculartend_gx
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// 从T_EMR_CARDIOVASCULARTEND_GX获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from t_emr_cardiovasculartend_gx
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// 添加记录到T_EMR_CARDIOVASCULARTEND_GX
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into t_emr_cardiovasculartend_gx (inpatientid,inpatientdate,
				opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,recorddate,infact1,infact1xml,
				infact2,infact2xml,infact3,infact3xml,infact4,infact4xml,infact5,infact5xml,inblood,inbloodxml,inperhour,
				inperhourxml,insum,insumxml,outsum,outsumxml,outperhour,outperhourxml,outfactpisssum,outfactpisssumxml,
				outfactpiss,outfactpissxml,outfactchestjuice,outfactchestjuicexml,outfactchestjuicesum,outfactchestjuicesumxml,
				outfactgastricjuice,outfactgastricjuicexml,expandvasmedicine,cardiacdiuresis,othermedicine,consciousness,
				consciousnessxml,pupil,pupilxml,leftpupil,leftpupilxml,rightpupil,rightpupilxml,temperature,temperaturexml,
				twigtemperature,twigtemperaturexml,heartrate,heartratexml,heartrhythm,heartrhythmxml,bpa,bpaxml,bps,bpsxml,
				avgbp,avgbpxml,cvp,cvpxml,lap,lapxml,breathmachine,breathmachinexml,insertdepth,insertdepthxml,assistant,
				assistantxml,fio2,fio2xml,peep,peepxml,tv,tvxml,vf,vfxml,breathtimes,breathtimesxml,leftbreathvoice,leftbreathvoicexml,
				rightbreathvoice,rightbreathvoicexml,phlegmcolor,phlegmcolorxml,phlegmquantity,phlegmquantityxml,gesticulation,
				gesticulationxml,physicaltherapy,physicaltherapyxml,remark,remarkxml,wbc,wbcxml,hb,hbxml,rbc,rbcxml,hct,hctxml,
				plt,pltxml,ph,phxml,pco2,pco2xml,pao2,pao2xml,hco3,hco3xml,be,bexml,kplus,kplusxml,naplus,naplusxml,cisub,cisubxml,
				caplusplus,caplusplusxml,glu,gluxml,bun,bunxml,ua,uaxml,anhydride,anhydridexml,co2cp,co2cpxml,pt,ptxml,xraycheck,
				xraycheckxml,act,actxml,proportion,proportionxml,albumen,albumenxml,hiddenblood,hiddenbloodxml,skin,skinxml,washperineum,
				washperineumxml,brushbath,brushbathxml,mouthtend,mouthtendxml,ie,iexml,inspiration,inspirationxml,spo,spoxml)
				values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 添加记录到T_EMR_CARDIOVASCULARTENDCON_GX
		/// </summary>
		private const string c_strAddNewRecordContentSQL= @"insert into t_emr_cardiovasculartendcon_gx (inpatientid,inpatientdate,
				opendate,modifydate,modifyuserid,infact1_right,infact2_right,infact3_right,infact4_right,infact5_right,
				inblood_right,inperhour_right,insum_right,outsum_right,outperhour_right,outfactpisssum_right,outfactpiss_right,
				outfactchestjuice_right,outfactchestjuicesum_right,outfactgastricjuice_right,expandvasmedicine_right,cardiacdiuresis_right,
				othermedicine_right,consciousness_right,pupil_right,leftpupil_right,rightpupil_right,temperature_right,
				twigtemperature_right,heartrate_right,heartrhythm_right,bpa_right,bps_right,avgbp_right,cvp_right,lap_right,
				breathmachine_right,insertdepth_right,assistant_right,fio2_right,peep_right,tv_right,vf_right,breathtimes_right,
				leftbreathvoice_right,rightbreathvoice_right,phlegmcolor_right,phlegmquantity_right,gesticulation_right,physicaltherapy_right,
				remark_right,wbc_right,hb_right,rbc_right,hct_right,plt_right,ph_right,pco2_right,pao2_right,hco3_right,be_right,
				kplus_right,naplus_right,cisub_right,caplusplus_right,glu_right,bun_right,ua_right,anhydride_right,co2cp_right,
				pt_right,xraycheck_right,act_right,proportion_right,albumen_right,hiddenblood_right,skin_right,washperineum_right,
				brushbath_right,mouthtend_right,ie_right,inspiration_right,spo_right) 
				values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
				?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到T_EMR_CARDIOVASCULARTEND_GX //pay attention
		/// </summary>
		private const string c_strModifyRecordSQL= @"update t_emr_cardiovasculartend_gx 
			set recorddate=?,infact1=?,infact1xml=?,
				infact2=?,infact2xml=?,infact3=?,infact3xml=?,infact4=?,infact4xml=?,infact5=?,infact5xml=?,inblood=?,inbloodxml=?,inperhour=?,
				inperhourxml=?,insum=?,insumxml=?,outsum=?,outsumxml=?,outperhour=?,outperhourxml=?,outfactpisssum=?,outfactpisssumxml=?,
				outfactpiss=?,outfactpissxml=?,outfactchestjuice=?,outfactchestjuicexml=?,outfactchestjuicesum=?,outfactchestjuicesumxml=?,
				outfactgastricjuice=?,outfactgastricjuicexml=?,expandvasmedicine=?,cardiacdiuresis=?,othermedicine=?,consciousness=?,
				consciousnessxml=?,pupil=?,pupilxml=?,leftpupil=?,leftpupilxml=?,rightpupil=?,rightpupilxml=?,temperature=?,temperaturexml=?,
				twigtemperature=?,twigtemperaturexml=?,heartrate=?,heartratexml=?,heartrhythm=?,heartrhythmxml=?,bpa=?,bpaxml=?,bps=?,bpsxml=?,
				avgbp=?,avgbpxml=?,cvp=?,cvpxml=?,lap=?,lapxml=?,breathmachine=?,breathmachinexml=?,insertdepth=?,insertdepthxml=?,assistant=?,
				assistantxml=?,fio2=?,fio2xml=?,peep=?,peepxml=?,tv=?,tvxml=?,vf=?,vfxml=?,breathtimes=?,breathtimesxml=?,leftbreathvoice=?,leftbreathvoicexml=?,
				rightbreathvoice=?,rightbreathvoicexml=?,phlegmcolor=?,phlegmcolorxml=?,phlegmquantity=?,phlegmquantityxml=?,gesticulation=?,
				gesticulationxml=?,physicaltherapy=?,physicaltherapyxml=?,remark=?,remarkxml=?,wbc=?,wbcxml=?,hb=?,hbxml=?,rbc=?,rbcxml=?,hct=?,hctxml=?,
				plt=?,pltxml=?,ph=?,phxml=?,pco2=?,pco2xml=?,pao2=?,pao2xml=?,hco3=?,hco3xml=?,be=?,bexml=?,kplus=?,kplusxml=?,naplus=?,naplusxml=?,cisub=?,cisubxml=?,
				caplusplus=?,caplusplusxml=?,glu=?,gluxml=?,bun=?,bunxml=?,ua=?,uaxml=?,anhydride=?,anhydridexml=?,co2cp=?,co2cpxml=?,pt=?,ptxml=?,xraycheck=?,
				xraycheckxml=?,act=?,actxml=?,proportion=?,proportionxml=?,albumen=?,albumenxml=?,hiddenblood=?,hiddenbloodxml=?,skin=?,skinxml=?,washperineum=?,
				washperineumxml=?,brushbath=?,brushbathxml=?,mouthtend=?,mouthtendxml=?,ie=?,iexml=?,inspiration=?,inspirationxml=?,spo=?,spoxml=? 
				where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// 修改记录到T_EMR_INTENSIVETENDCONTENT_GX
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// 设置T_EMR_CARDIOVASCULARTEND_GX中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update t_emr_cardiovasculartend_gx
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// 更新T_EMR_CARDIOVASCULARTEND_GX中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update t_emr_cardiovasculartend_gx
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// 从T_EMR_CARDIOVASCULARTEND_GX获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from t_emr_cardiovasculartend_gx
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// 从T_EMR_CARDIOVASCULARTEND_GX获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from t_emr_cardiovasculartend_gx
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";
		#endregion

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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCardiovascularTend_GXService","m_lngGetRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCardiovascularTend_GXService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();

			try
			{
				//检查参数                              
				if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
					return (long)enmOperationResult.Parameter_Error;			
			
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strOpenDate);
			
				//执行SQL
				long lngEff=0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
				
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCardiovascularTend_GXService","m_lngGetDeleteRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				objDPArr[2].Value=p_strDeleteUserID;
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
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

		// <summary>
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCardiovascularTend_GXService","m_lngGetDeleteRecordTimeListAll");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
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
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = @"select t1.inpatientid,
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
       t2.modifydate,
       t2.modifyuserid,
       t2.infact1_right,
       t2.infact2_right,
       t2.infact3_right,
       t2.infact4_right,
       t2.infact5_right,
       t2.inblood_right,
       t2.inperhour_right,
       t2.insum_right,
       t2.outsum_right,
       t2.outperhour_right,
       t2.outfactpisssum_right,
       t2.outfactpiss_right,
       t2.outfactchestjuice_right,
       t2.outfactchestjuicesum_right,
       t2.outfactgastricjuice_right,
       t2.expandvasmedicine_right,
       t2.cardiacdiuresis_right,
       t2.othermedicine_right,
       t2.consciousness_right,
       t2.pupil_right,
       t2.leftpupil_right,
       t2.rightpupil_right,
       t2.temperature_right,
       t2.twigtemperature_right,
       t2.heartrate_right,
       t2.heartrhythm_right,
       t2.bpa_right,
       t2.bps_right,
       t2.avgbp_right,
       t2.cvp_right,
       t2.lap_right,
       t2.breathmachine_right,
       t2.insertdepth_right,
       t2.assistant_right,
       t2.fio2_right,
       t2.peep_right,
       t2.tv_right,
       t2.vf_right,
       t2.breathtimes_right,
       t2.leftbreathvoice_right,
       t2.rightbreathvoice_right,
       t2.phlegmcolor_right,
       t2.phlegmquantity_right,
       t2.gesticulation_right,
       t2.physicaltherapy_right,
       t2.remark_right,
       t2.wbc_right,
       t2.hb_right,
       t2.rbc_right,
       t2.hct_right,
       t2.plt_right,
       t2.ph_right,
       t2.pco2_right,
       t2.pao2_right,
       t2.hco3_right,
       t2.be_right,
       t2.kplus_right,
       t2.naplus_right,
       t2.cisub_right,
       t2.caplusplus_right,
       t2.glu_right,
       t2.bun_right,
       t2.ua_right,
       t2.anhydride_right,
       t2.co2cp_right,
       t2.pt_right,
       t2.xraycheck_right,
       t2.act_right,
       t2.proportion_right,
       t2.albumen_right,
       t2.hiddenblood_right,
       t2.skin_right,
       t2.washperineum_right,
       t2.brushbath_right,
       t2.mouthtend_right,
       t2.ie_right,
       t2.inspiration_right,
       t2.spo_right
  from t_emr_cardiovasculartend_gx t1
 inner join t_emr_cardiovasculartendcon_gx t2 on (t1.inpatientid =
                                                 t2.inpatientid and
                                                 t1.inpatientdate =
                                                 t2.inpatientdate and
                                                 t1.opendate = t2.opendate)
 where t2.modifydate = (select max(modifydate)
                          from t_emr_cardiovasculartendcon_gx
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?";

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					#region 设置结果
					clsCardiovascularTend_GX objRecordContent = new clsCardiovascularTend_GX();
					objRecordContent.m_strInPatientID = p_strInPatientID;
					objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[0]["OPENDATE"]);
					objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
					objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();
					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(dtbValue.Rows[0]["FIRSTPRINTDATE"]);
					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

					objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
					objRecordContent.m_strINFACT1 = dtbValue.Rows[0]["INFACT1"].ToString();
					objRecordContent.m_strINFACT1XML = dtbValue.Rows[0]["INFACT1XML"].ToString();
					objRecordContent.m_strINFACT2 = dtbValue.Rows[0]["INFACT2"].ToString();
					objRecordContent.m_strINFACT2XML = dtbValue.Rows[0]["INFACT2XML"].ToString();
					objRecordContent.m_strINFACT3 = dtbValue.Rows[0]["INFACT3"].ToString();
					objRecordContent.m_strINFACT3XML = dtbValue.Rows[0]["INFACT3XML"].ToString();
					objRecordContent.m_strINFACT4 = dtbValue.Rows[0]["INFACT4"].ToString();
					objRecordContent.m_strINFACT4XML = dtbValue.Rows[0]["INFACT4XML"].ToString();
					objRecordContent.m_strINFACT5 = dtbValue.Rows[0]["INFACT5"].ToString();
					objRecordContent.m_strINFACT5XML = dtbValue.Rows[0]["INFACT5XML"].ToString();
					objRecordContent.m_strINBLOOD = dtbValue.Rows[0]["INBLOOD"].ToString();
					objRecordContent.m_strINBLOODXML = dtbValue.Rows[0]["INBLOODXML"].ToString();
					objRecordContent.m_strINPERHOUR = dtbValue.Rows[0]["INPERHOUR"].ToString();
					objRecordContent.m_strINPERHOURXML = dtbValue.Rows[0]["INPERHOURXML"].ToString();
					objRecordContent.m_strINSUM = dtbValue.Rows[0]["INSUM"].ToString();
					objRecordContent.m_strINSUMXML = dtbValue.Rows[0]["INSUMXML"].ToString();
					objRecordContent.m_strOUTSUM = dtbValue.Rows[0]["OUTSUM"].ToString();
					objRecordContent.m_strOUTSUMXML = dtbValue.Rows[0]["OUTSUMXML"].ToString();
					objRecordContent.m_strOUTPERHOUR = dtbValue.Rows[0]["OUTPERHOUR"].ToString();
					objRecordContent.m_strOUTPERHOURXML = dtbValue.Rows[0]["OUTPERHOURXML"].ToString();
					objRecordContent.m_strOUTFACTPISSSUM = dtbValue.Rows[0]["OUTFACTPISSSUM"].ToString();
					objRecordContent.m_strOUTFACTPISSSUMXML = dtbValue.Rows[0]["OUTFACTPISSSUMXML"].ToString();
					objRecordContent.m_strOUTFACTPISS = dtbValue.Rows[0]["OUTFACTPISS"].ToString();
					objRecordContent.m_strOUTFACTPISSXML = dtbValue.Rows[0]["OUTFACTPISSXML"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICE = dtbValue.Rows[0]["OUTFACTCHESTJUICE"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICEXML = dtbValue.Rows[0]["OUTFACTCHESTJUICEXML"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICESUM = dtbValue.Rows[0]["OUTFACTCHESTJUICESUM"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICESUMXML = dtbValue.Rows[0]["OUTFACTCHESTJUICESUMXML"].ToString();
					objRecordContent.m_strOUTFACTGASTRICJUICE = dtbValue.Rows[0]["OUTFACTGASTRICJUICE"].ToString();
					objRecordContent.m_strOUTFACTGASTRICJUICEXML = dtbValue.Rows[0]["OUTFACTGASTRICJUICEXML"].ToString();
					objRecordContent.m_strEXPANDVASMEDICINE = dtbValue.Rows[0]["EXPANDVASMEDICINE"].ToString();
					objRecordContent.m_strCARDIACDIURESIS = dtbValue.Rows[0]["CARDIACDIURESIS"].ToString();
					objRecordContent.m_strOTHERMEDICINE = dtbValue.Rows[0]["OTHERMEDICINE"].ToString();
					objRecordContent.m_strCONSCIOUSNESS = dtbValue.Rows[0]["CONSCIOUSNESS"].ToString();
					objRecordContent.m_strCONSCIOUSNESSXML = dtbValue.Rows[0]["CONSCIOUSNESSXML"].ToString();
					objRecordContent.m_strPUPIL = dtbValue.Rows[0]["PUPIL"].ToString();
					objRecordContent.m_strPUPILXML = dtbValue.Rows[0]["PUPILXML"].ToString();
					objRecordContent.m_strLEFTPUPIL = dtbValue.Rows[0]["LEFTPUPIL"].ToString();
					objRecordContent.m_strLEFTPUPILXML = dtbValue.Rows[0]["LEFTPUPILXML"].ToString();
					objRecordContent.m_strRIGHTPUPIL = dtbValue.Rows[0]["RIGHTPUPIL"].ToString();
					objRecordContent.m_strRIGHTPUPILXML = dtbValue.Rows[0]["RIGHTPUPILXML"].ToString();
					objRecordContent.m_strTEMPERATURE = dtbValue.Rows[0]["TEMPERATURE"].ToString();
					objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
					objRecordContent.m_strTWIGTEMPERATURE = dtbValue.Rows[0]["TWIGTEMPERATURE"].ToString();
					objRecordContent.m_strTWIGTEMPERATUREXML = dtbValue.Rows[0]["TWIGTEMPERATUREXML"].ToString();
					objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
					objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
					objRecordContent.m_strHEARTRHYTHM = dtbValue.Rows[0]["HEARTRHYTHM"].ToString();
					objRecordContent.m_strHEARTRHYTHMXML = dtbValue.Rows[0]["HEARTRHYTHMXML"].ToString();
					objRecordContent.m_strBPA = dtbValue.Rows[0]["BPA"].ToString();
					objRecordContent.m_strBPAXML = dtbValue.Rows[0]["BPAXML"].ToString();
					objRecordContent.m_strBPS = dtbValue.Rows[0]["BPS"].ToString();
					objRecordContent.m_strBPSXML = dtbValue.Rows[0]["BPSXML"].ToString();
					objRecordContent.m_strAVGBP = dtbValue.Rows[0]["AVGBP"].ToString();
					objRecordContent.m_strAVGBPXML = dtbValue.Rows[0]["AVGBPXML"].ToString();
					objRecordContent.m_strCVP = dtbValue.Rows[0]["CVP"].ToString();
					objRecordContent.m_strCVPXML = dtbValue.Rows[0]["CVPXML"].ToString();
					objRecordContent.m_strLAP = dtbValue.Rows[0]["LAP"].ToString();
					objRecordContent.m_strLAPXML = dtbValue.Rows[0]["LAPXML"].ToString();

                    objRecordContent.m_strSPO = dtbValue.Rows[0]["SPO"].ToString();
                    objRecordContent.m_strSPOXML = dtbValue.Rows[0]["SPOXML"].ToString();

					objRecordContent.m_strBREATHMACHINE = dtbValue.Rows[0]["BREATHMACHINE"].ToString();
					objRecordContent.m_strBREATHMACHINEXML = dtbValue.Rows[0]["BREATHMACHINEXML"].ToString();
					objRecordContent.m_strINSERTDEPTH = dtbValue.Rows[0]["INSERTDEPTH"].ToString();
					objRecordContent.m_strINSERTDEPTHXML = dtbValue.Rows[0]["INSERTDEPTHXML"].ToString();
					objRecordContent.m_strASSISTANT = dtbValue.Rows[0]["ASSISTANT"].ToString();
					objRecordContent.m_strASSISTANTXML = dtbValue.Rows[0]["ASSISTANTXML"].ToString();
					objRecordContent.m_strFIO2 = dtbValue.Rows[0]["FIO2"].ToString();
					objRecordContent.m_strFIO2XML = dtbValue.Rows[0]["FIO2XML"].ToString();
					objRecordContent.m_strPEEP = dtbValue.Rows[0]["PEEP"].ToString();
					objRecordContent.m_strPEEPXML = dtbValue.Rows[0]["PEEPXML"].ToString();
					objRecordContent.m_strTV = dtbValue.Rows[0]["TV"].ToString();
					objRecordContent.m_strTVXML = dtbValue.Rows[0]["TVXML"].ToString();
					objRecordContent.m_strVF = dtbValue.Rows[0]["VF"].ToString();
					objRecordContent.m_strVFXML = dtbValue.Rows[0]["VFXML"].ToString();
					objRecordContent.m_strBREATHTIMES = dtbValue.Rows[0]["BREATHTIMES"].ToString();
					objRecordContent.m_strBREATHTIMESXML = dtbValue.Rows[0]["BREATHTIMESXML"].ToString();
					objRecordContent.m_strLEFTBREATHVOICE = dtbValue.Rows[0]["LEFTBREATHVOICE"].ToString();
					objRecordContent.m_strLEFTBREATHVOICEXML = dtbValue.Rows[0]["LEFTBREATHVOICEXML"].ToString();
					objRecordContent.m_strRIGHTBREATHVOICE = dtbValue.Rows[0]["RIGHTBREATHVOICE"].ToString();
					objRecordContent.m_strRIGHTBREATHVOICEXML = dtbValue.Rows[0]["RIGHTBREATHVOICEXML"].ToString();
					objRecordContent.m_strPHLEGMCOLOR = dtbValue.Rows[0]["PHLEGMCOLOR"].ToString();
					objRecordContent.m_strPHLEGMCOLORXML = dtbValue.Rows[0]["PHLEGMCOLORXML"].ToString();
					objRecordContent.m_strPHLEGMQUANTITY = dtbValue.Rows[0]["PHLEGMQUANTITY"].ToString();
					objRecordContent.m_strPHLEGMQUANTITYXML = dtbValue.Rows[0]["PHLEGMQUANTITYXML"].ToString();
					objRecordContent.m_strGESTICULATION = dtbValue.Rows[0]["GESTICULATION"].ToString();
					objRecordContent.m_strGESTICULATIONXML = dtbValue.Rows[0]["GESTICULATIONXML"].ToString();
					objRecordContent.m_strPHYSICALTHERAPY = dtbValue.Rows[0]["PHYSICALTHERAPY"].ToString();
					objRecordContent.m_strPHYSICALTHERAPYXML = dtbValue.Rows[0]["PHYSICALTHERAPYXML"].ToString();
					objRecordContent.m_strREMARK = dtbValue.Rows[0]["REMARK"].ToString();
					objRecordContent.m_strREMARKXML = dtbValue.Rows[0]["REMARKXML"].ToString();
					objRecordContent.m_strWBC = dtbValue.Rows[0]["WBC"].ToString();
					objRecordContent.m_strWBCXML = dtbValue.Rows[0]["WBCXML"].ToString();
					objRecordContent.m_strHB = dtbValue.Rows[0]["HB"].ToString();
					objRecordContent.m_strHBXML = dtbValue.Rows[0]["HBXML"].ToString();
					objRecordContent.m_strRBC = dtbValue.Rows[0]["RBC"].ToString();
					objRecordContent.m_strRBCXML = dtbValue.Rows[0]["RBCXML"].ToString();
					objRecordContent.m_strHCT = dtbValue.Rows[0]["HCT"].ToString();
					objRecordContent.m_strHCTXML = dtbValue.Rows[0]["HCTXML"].ToString();
					objRecordContent.m_strPLT = dtbValue.Rows[0]["PLT"].ToString();
					objRecordContent.m_strPLTXML = dtbValue.Rows[0]["PLTXML"].ToString();
					objRecordContent.m_strPH = dtbValue.Rows[0]["PH"].ToString();
					objRecordContent.m_strPHXML = dtbValue.Rows[0]["PHXML"].ToString();
					objRecordContent.m_strPCO2 = dtbValue.Rows[0]["PCO2"].ToString();
					objRecordContent.m_strPCO2XML = dtbValue.Rows[0]["PCO2XML"].ToString();
					objRecordContent.m_strPAO2 = dtbValue.Rows[0]["PAO2"].ToString();
					objRecordContent.m_strPAO2XML = dtbValue.Rows[0]["PAO2XML"].ToString();
					objRecordContent.m_strHCO3 = dtbValue.Rows[0]["HCO3"].ToString();
					objRecordContent.m_strHCO3XML = dtbValue.Rows[0]["HCO3XML"].ToString();
					objRecordContent.m_strBE = dtbValue.Rows[0]["BE"].ToString();
					objRecordContent.m_strBEXML = dtbValue.Rows[0]["BEXML"].ToString();
					objRecordContent.m_strKPLUS = dtbValue.Rows[0]["KPLUS"].ToString();
					objRecordContent.m_strKPLUSXML = dtbValue.Rows[0]["KPLUSXML"].ToString();
					objRecordContent.m_strNAPLUS = dtbValue.Rows[0]["NAPLUS"].ToString();
					objRecordContent.m_strNAPLUSXML = dtbValue.Rows[0]["NAPLUSXML"].ToString();
					objRecordContent.m_strCISUB = dtbValue.Rows[0]["CISUB"].ToString();
					objRecordContent.m_strCISUBXML = dtbValue.Rows[0]["CISUBXML"].ToString();
					objRecordContent.m_strCAPLUSPLUS = dtbValue.Rows[0]["CAPLUSPLUS"].ToString();
					objRecordContent.m_strCAPLUSPLUSXML = dtbValue.Rows[0]["CAPLUSPLUSXML"].ToString();
					objRecordContent.m_strGLU = dtbValue.Rows[0]["GLU"].ToString();
					objRecordContent.m_strGLUXML = dtbValue.Rows[0]["GLUXML"].ToString();
					objRecordContent.m_strBUN = dtbValue.Rows[0]["BUN"].ToString();
					objRecordContent.m_strBUNXML = dtbValue.Rows[0]["BUNXML"].ToString();
					objRecordContent.m_strUA = dtbValue.Rows[0]["UA"].ToString();
					objRecordContent.m_strUAXML = dtbValue.Rows[0]["UAXML"].ToString();
					objRecordContent.m_strANHYDRIDE = dtbValue.Rows[0]["ANHYDRIDE"].ToString();
					objRecordContent.m_strANHYDRIDEXML = dtbValue.Rows[0]["ANHYDRIDEXML"].ToString();
					objRecordContent.m_strCO2CP = dtbValue.Rows[0]["CO2CP"].ToString();
					objRecordContent.m_strCO2CPXML = dtbValue.Rows[0]["CO2CPXML"].ToString();
					objRecordContent.m_strPT = dtbValue.Rows[0]["PT"].ToString();
					objRecordContent.m_strPTXML = dtbValue.Rows[0]["PTXML"].ToString();
					objRecordContent.m_strXRAYCHECK = dtbValue.Rows[0]["XRAYCHECK"].ToString();
					objRecordContent.m_strXRAYCHECKXML = dtbValue.Rows[0]["XRAYCHECKXML"].ToString();
					objRecordContent.m_strACT = dtbValue.Rows[0]["ACT"].ToString();
					objRecordContent.m_strACTXML = dtbValue.Rows[0]["ACTXML"].ToString();
					objRecordContent.m_strPROPORTION = dtbValue.Rows[0]["PROPORTION"].ToString();
					objRecordContent.m_strPROPORTIONXML = dtbValue.Rows[0]["PROPORTIONXML"].ToString();
					objRecordContent.m_strALBUMEN = dtbValue.Rows[0]["ALBUMEN"].ToString();
					objRecordContent.m_strALBUMENXML = dtbValue.Rows[0]["ALBUMENXML"].ToString();
					objRecordContent.m_strHIDDENBLOOD = dtbValue.Rows[0]["HIDDENBLOOD"].ToString();
					objRecordContent.m_strHIDDENBLOODXML = dtbValue.Rows[0]["HIDDENBLOODXML"].ToString();
					objRecordContent.m_strSKIN = dtbValue.Rows[0]["SKIN"].ToString();
					objRecordContent.m_strSKINXML = dtbValue.Rows[0]["SKINXML"].ToString();
					objRecordContent.m_strWASHPERINEUM = dtbValue.Rows[0]["WASHPERINEUM"].ToString();
					objRecordContent.m_strWASHPERINEUMXML = dtbValue.Rows[0]["WASHPERINEUMXML"].ToString();
					objRecordContent.m_strBRUSHBATH = dtbValue.Rows[0]["BRUSHBATH"].ToString();
					objRecordContent.m_strBRUSHBATHXML = dtbValue.Rows[0]["BRUSHBATHXML"].ToString();
					objRecordContent.m_strMOUTHTEND = dtbValue.Rows[0]["MOUTHTEND"].ToString();
					objRecordContent.m_strMOUTHTENDXML = dtbValue.Rows[0]["MOUTHTENDXML"].ToString();
					objRecordContent.m_strIE = dtbValue.Rows[0]["IE"].ToString();
					objRecordContent.m_strIEXML = dtbValue.Rows[0]["IEXML"].ToString();
					objRecordContent.m_strIE_RIGHT = dtbValue.Rows[0]["IE_RIGHT"].ToString();
					objRecordContent.m_strINSPIRATION = dtbValue.Rows[0]["INSPIRATION"].ToString();
					objRecordContent.m_strINSPIRATIONXML = dtbValue.Rows[0]["INSPIRATIONXML"].ToString();
					objRecordContent.m_strINSPIRATION_RIGHT = dtbValue.Rows[0]["INSPIRATION_RIGHT"].ToString();
							
					objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
					objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					objRecordContent.m_strINFACT1_RIGHT = dtbValue.Rows[0]["INFACT1_RIGHT"].ToString();
					objRecordContent.m_strINFACT2_RIGHT = dtbValue.Rows[0]["INFACT2_RIGHT"].ToString();
					objRecordContent.m_strINFACT3_RIGHT = dtbValue.Rows[0]["INFACT3_RIGHT"].ToString();
					objRecordContent.m_strINFACT4_RIGHT = dtbValue.Rows[0]["INFACT4_RIGHT"].ToString();
					objRecordContent.m_strINFACT5_RIGHT = dtbValue.Rows[0]["INFACT5_RIGHT"].ToString();
					objRecordContent.m_strINBLOOD_RIGHT = dtbValue.Rows[0]["INBLOOD_RIGHT"].ToString();
					objRecordContent.m_strINPERHOUR_RIGHT = dtbValue.Rows[0]["INPERHOUR_RIGHT"].ToString();
					objRecordContent.m_strINSUM_RIGHT = dtbValue.Rows[0]["INSUM_RIGHT"].ToString();
					objRecordContent.m_strOUTSUM_RIGHT = dtbValue.Rows[0]["OUTSUM_RIGHT"].ToString();
					objRecordContent.m_strOUTPERHOUR_RIGHT = dtbValue.Rows[0]["OUTPERHOUR_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTPISSSUM_RIGHT = dtbValue.Rows[0]["OUTFACTPISSSUM_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTPISS_RIGHT = dtbValue.Rows[0]["OUTFACTPISS_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICE_RIGHT = dtbValue.Rows[0]["OUTFACTCHESTJUICE_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICESUM_RIGHT = dtbValue.Rows[0]["OUTFACTCHESTJUICESUM_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTGASTRICJUICE_RIGHT = dtbValue.Rows[0]["OUTFACTGASTRICJUICE_RIGHT"].ToString();
					objRecordContent.m_strEXPANDVASMEDICINE_RIGHT = dtbValue.Rows[0]["EXPANDVASMEDICINE_RIGHT"].ToString();
					objRecordContent.m_strCARDIACDIURESIS_RIGHT = dtbValue.Rows[0]["CARDIACDIURESIS_RIGHT"].ToString();
					objRecordContent.m_strOTHERMEDICINE_RIGHT = dtbValue.Rows[0]["OTHERMEDICINE_RIGHT"].ToString();
					objRecordContent.m_strCONSCIOUSNESS_RIGHT = dtbValue.Rows[0]["CONSCIOUSNESS_RIGHT"].ToString();
					objRecordContent.m_strPUPIL_RIGHT = dtbValue.Rows[0]["PUPIL_RIGHT"].ToString();
					objRecordContent.m_strLEFTPUPIL_RIGHT = dtbValue.Rows[0]["LEFTPUPIL_RIGHT"].ToString();
					objRecordContent.m_strRIGHTPUPIL_RIGHT = dtbValue.Rows[0]["RIGHTPUPIL_RIGHT"].ToString();
					objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strTWIGTEMPERATURE_RIGHT = dtbValue.Rows[0]["TWIGTEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATE_RIGHT"].ToString();
					objRecordContent.m_strHEARTRHYTHM_RIGHT = dtbValue.Rows[0]["HEARTRHYTHM_RIGHT"].ToString();
					objRecordContent.m_strBPA_RIGHT = dtbValue.Rows[0]["BPA_RIGHT"].ToString();
					objRecordContent.m_strBPS_RIGHT = dtbValue.Rows[0]["BPS_RIGHT"].ToString();
					objRecordContent.m_strAVGBP_RIGHT = dtbValue.Rows[0]["AVGBP_RIGHT"].ToString();
					objRecordContent.m_strCVP_RIGHT = dtbValue.Rows[0]["CVP_RIGHT"].ToString();
					objRecordContent.m_strLAP_RIGHT = dtbValue.Rows[0]["LAP_RIGHT"].ToString();
                    objRecordContent.m_strSPO_RIGHT = dtbValue.Rows[0]["SPO_RIGHT"].ToString();
					objRecordContent.m_strBREATHMACHINE_RIGHT = dtbValue.Rows[0]["BREATHMACHINE_RIGHT"].ToString();
					objRecordContent.m_strINSERTDEPTH_RIGHT = dtbValue.Rows[0]["INSERTDEPTH_RIGHT"].ToString();
					objRecordContent.m_strASSISTANT_RIGHT = dtbValue.Rows[0]["ASSISTANT_RIGHT"].ToString();
					objRecordContent.m_strFIO2_RIGHT = dtbValue.Rows[0]["FIO2_RIGHT"].ToString();
					objRecordContent.m_strPEEP_RIGHT = dtbValue.Rows[0]["PEEP_RIGHT"].ToString();
					objRecordContent.m_strTV_RIGHT = dtbValue.Rows[0]["TV_RIGHT"].ToString();
					objRecordContent.m_strVF_RIGHT = dtbValue.Rows[0]["VF_RIGHT"].ToString();
					objRecordContent.m_strBREATHTIMES_RIGHT = dtbValue.Rows[0]["BREATHTIMES_RIGHT"].ToString();
					objRecordContent.m_strLEFTBREATHVOICE_RIGHT = dtbValue.Rows[0]["LEFTBREATHVOICE_RIGHT"].ToString();
					objRecordContent.m_strRIGHTBREATHVOICE_RIGHT = dtbValue.Rows[0]["RIGHTBREATHVOICE_RIGHT"].ToString();
					objRecordContent.m_strPHLEGMCOLOR_RIGHT = dtbValue.Rows[0]["PHLEGMCOLOR_RIGHT"].ToString();
					objRecordContent.m_strPHLEGMQUANTITY_RIGHT = dtbValue.Rows[0]["PHLEGMQUANTITY_RIGHT"].ToString();
					objRecordContent.m_strGESTICULATION_RIGHT = dtbValue.Rows[0]["GESTICULATION_RIGHT"].ToString();
					objRecordContent.m_strPHYSICALTHERAPY_RIGHT = dtbValue.Rows[0]["PHYSICALTHERAPY_RIGHT"].ToString();
					objRecordContent.m_strREMARK_RIGHT = dtbValue.Rows[0]["REMARK_RIGHT"].ToString();
					objRecordContent.m_strWBC_RIGHT = dtbValue.Rows[0]["WBC_RIGHT"].ToString();
					objRecordContent.m_strHB_RIGHT = dtbValue.Rows[0]["HB_RIGHT"].ToString();
					objRecordContent.m_strRBC_RIGHT = dtbValue.Rows[0]["RBC_RIGHT"].ToString();
					objRecordContent.m_strHCT_RIGHT = dtbValue.Rows[0]["HCT_RIGHT"].ToString();
					objRecordContent.m_strPLT_RIGHT = dtbValue.Rows[0]["PLT_RIGHT"].ToString();
					objRecordContent.m_strPH_RIGHT = dtbValue.Rows[0]["PH_RIGHT"].ToString();
					objRecordContent.m_strPCO2_RIGHT = dtbValue.Rows[0]["PCO2_RIGHT"].ToString();
					objRecordContent.m_strPAO2_RIGHT = dtbValue.Rows[0]["PAO2_RIGHT"].ToString();
					objRecordContent.m_strHCO3_RIGHT = dtbValue.Rows[0]["HCO3_RIGHT"].ToString();
					objRecordContent.m_strBE_RIGHT = dtbValue.Rows[0]["BE_RIGHT"].ToString();
					objRecordContent.m_strKPLUS_RIGHT = dtbValue.Rows[0]["KPLUS_RIGHT"].ToString();
					objRecordContent.m_strNAPLUS_RIGHT = dtbValue.Rows[0]["NAPLUS_RIGHT"].ToString();
					objRecordContent.m_strCISUB_RIGHT = dtbValue.Rows[0]["CISUB_RIGHT"].ToString();
					objRecordContent.m_strCAPLUSPLUS_RIGHT = dtbValue.Rows[0]["CAPLUSPLUS_RIGHT"].ToString();
					objRecordContent.m_strGLU_RIGHT = dtbValue.Rows[0]["GLU_RIGHT"].ToString();
					objRecordContent.m_strBUN_RIGHT = dtbValue.Rows[0]["BUN_RIGHT"].ToString();
					objRecordContent.m_strUA_RIGHT = dtbValue.Rows[0]["UA_RIGHT"].ToString();
					objRecordContent.m_strANHYDRIDE_RIGHT = dtbValue.Rows[0]["ANHYDRIDE_RIGHT"].ToString();
					objRecordContent.m_strCO2CP_RIGHT = dtbValue.Rows[0]["CO2CP_RIGHT"].ToString();
					objRecordContent.m_strPT_RIGHT = dtbValue.Rows[0]["PT_RIGHT"].ToString();
					objRecordContent.m_strXRAYCHECK_RIGHT = dtbValue.Rows[0]["XRAYCHECK_RIGHT"].ToString();
					objRecordContent.m_strACT_RIGHT = dtbValue.Rows[0]["ACT_RIGHT"].ToString();
					objRecordContent.m_strPROPORTION_RIGHT = dtbValue.Rows[0]["PROPORTION_RIGHT"].ToString();
					objRecordContent.m_strALBUMEN_RIGHT = dtbValue.Rows[0]["ALBUMEN_RIGHT"].ToString();
					objRecordContent.m_strHIDDENBLOOD_RIGHT = dtbValue.Rows[0]["HIDDENBLOOD_RIGHT"].ToString();
					objRecordContent.m_strSKIN_RIGHT = dtbValue.Rows[0]["SKIN_RIGHT"].ToString();
					objRecordContent.m_strWASHPERINEUM_RIGHT = dtbValue.Rows[0]["WASHPERINEUM_RIGHT"].ToString();
					objRecordContent.m_strBRUSHBATH_RIGHT = dtbValue.Rows[0]["BRUSHBATH_RIGHT"].ToString();
					objRecordContent.m_strMOUTHTEND_RIGHT = dtbValue.Rows[0]["MOUTHTEND_RIGHT"].ToString();					

					p_objRecordContent = objRecordContent;
					#endregion
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

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
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
				objDPArr[2].Value=p_objRecordContent.m_dtmCreateDate;
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL,ref dtbValue,objDPArr);
					
				//查看DataTable.Rows.Count
				//如果等于1，表示已经有该CreateDate，并且不是删除的记录。
				//获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					p_objModifyInfo=new clsPreModifyInfo();
					p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
					return (long)enmOperationResult.Record_Already_Exist;
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
			clsCardiovascularTend_GX objRecordContent = (clsCardiovascularTend_GX)p_objRecordContent;
			try
			{
				#region 获取IDataParameter数组 T_EMR_CARDIOVASCULARTEND_GX
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(163,out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=objRecordContent.m_dtmCreateDate;
				objDPArr[4].Value=objRecordContent.m_strCreateUserID;
				objDPArr[5].Value=objRecordContent.m_bytIfConfirm;

				if(objRecordContent.m_strConfirmReason==null)
					objDPArr[6].Value=DBNull.Value;
				else
					objDPArr[6].Value=objRecordContent.m_strConfirmReason;
				if(objRecordContent.m_strConfirmReasonXML==null)
					objDPArr[7].Value=DBNull.Value;
				else 
					objDPArr[7].Value=objRecordContent.m_strConfirmReasonXML;
				objDPArr[8].Value=0;

                objDPArr[9].DbType = DbType.DateTime;
				objDPArr[9].Value = objRecordContent.m_dtmRECORDDATE;
				objDPArr[10].Value = objRecordContent.m_strINFACT1;
				objDPArr[11].Value = objRecordContent.m_strINFACT1XML;
				objDPArr[12].Value = objRecordContent.m_strINFACT2;
				objDPArr[13].Value = objRecordContent.m_strINFACT2XML;
				objDPArr[14].Value = objRecordContent.m_strINFACT3;
				objDPArr[15].Value = objRecordContent.m_strINFACT3XML;
				objDPArr[16].Value = objRecordContent.m_strINFACT4;
				objDPArr[17].Value = objRecordContent.m_strINFACT4XML;
				objDPArr[18].Value = objRecordContent.m_strINFACT5;
				objDPArr[19].Value = objRecordContent.m_strINFACT5XML;
				objDPArr[20].Value = objRecordContent.m_strINBLOOD;
				objDPArr[21].Value = objRecordContent.m_strINBLOODXML;
				objDPArr[22].Value = objRecordContent.m_strINPERHOUR;
				objDPArr[23].Value = objRecordContent.m_strINPERHOURXML;
				objDPArr[24].Value = objRecordContent.m_strINSUM;
				objDPArr[25].Value = objRecordContent.m_strINSUMXML;
				objDPArr[26].Value = objRecordContent.m_strOUTSUM;
				objDPArr[27].Value = objRecordContent.m_strOUTSUMXML;
				objDPArr[28].Value = objRecordContent.m_strOUTPERHOUR;
				objDPArr[29].Value = objRecordContent.m_strOUTPERHOURXML;
				objDPArr[30].Value = objRecordContent.m_strOUTFACTPISSSUM;
				objDPArr[31].Value = objRecordContent.m_strOUTFACTPISSSUMXML;
				objDPArr[32].Value = objRecordContent.m_strOUTFACTPISS;
				objDPArr[33].Value = objRecordContent.m_strOUTFACTPISSXML;
				objDPArr[34].Value = objRecordContent.m_strOUTFACTCHESTJUICE;
				objDPArr[35].Value = objRecordContent.m_strOUTFACTCHESTJUICEXML;
				objDPArr[36].Value = objRecordContent.m_strOUTFACTCHESTJUICESUM;
				objDPArr[37].Value = objRecordContent.m_strOUTFACTCHESTJUICESUMXML;
				objDPArr[38].Value = objRecordContent.m_strOUTFACTGASTRICJUICE;
				objDPArr[39].Value = objRecordContent.m_strOUTFACTGASTRICJUICEXML;
				objDPArr[40].Value = objRecordContent.m_strEXPANDVASMEDICINE;
				objDPArr[41].Value = objRecordContent.m_strCARDIACDIURESIS;
				objDPArr[42].Value = objRecordContent.m_strOTHERMEDICINE;
				objDPArr[43].Value = objRecordContent.m_strCONSCIOUSNESS;
				objDPArr[44].Value = objRecordContent.m_strCONSCIOUSNESSXML;
				objDPArr[45].Value = objRecordContent.m_strPUPIL;
				objDPArr[46].Value = objRecordContent.m_strPUPILXML;
				objDPArr[47].Value = objRecordContent.m_strLEFTPUPIL;
				objDPArr[48].Value = objRecordContent.m_strLEFTPUPILXML;
				objDPArr[49].Value = objRecordContent.m_strRIGHTPUPIL;
				objDPArr[50].Value = objRecordContent.m_strRIGHTPUPILXML;
				objDPArr[51].Value = objRecordContent.m_strTEMPERATURE;
				objDPArr[52].Value = objRecordContent.m_strTEMPERATUREXML;
				objDPArr[53].Value = objRecordContent.m_strTWIGTEMPERATURE;
				objDPArr[54].Value = objRecordContent.m_strTWIGTEMPERATUREXML;
				objDPArr[55].Value = objRecordContent.m_strHEARTRATE;
				objDPArr[56].Value = objRecordContent.m_strHEARTRATEXML;
				objDPArr[57].Value = objRecordContent.m_strHEARTRHYTHM;
				objDPArr[58].Value = objRecordContent.m_strHEARTRHYTHMXML;
				objDPArr[59].Value = objRecordContent.m_strBPA;
				objDPArr[60].Value = objRecordContent.m_strBPAXML;
				objDPArr[61].Value = objRecordContent.m_strBPS;
				objDPArr[62].Value = objRecordContent.m_strBPSXML;
				objDPArr[63].Value = objRecordContent.m_strAVGBP;
				objDPArr[64].Value = objRecordContent.m_strAVGBPXML;
				objDPArr[65].Value = objRecordContent.m_strCVP;
				objDPArr[66].Value = objRecordContent.m_strCVPXML;
				objDPArr[67].Value = objRecordContent.m_strLAP;
				objDPArr[68].Value = objRecordContent.m_strLAPXML;
				objDPArr[69].Value = objRecordContent.m_strBREATHMACHINE;
				objDPArr[70].Value = objRecordContent.m_strBREATHMACHINEXML;
				objDPArr[71].Value = objRecordContent.m_strINSERTDEPTH;
				objDPArr[72].Value = objRecordContent.m_strINSERTDEPTHXML;
				objDPArr[73].Value = objRecordContent.m_strASSISTANT;
				objDPArr[74].Value = objRecordContent.m_strASSISTANTXML;
				objDPArr[75].Value = objRecordContent.m_strFIO2;
				objDPArr[76].Value = objRecordContent.m_strFIO2XML;
				objDPArr[77].Value = objRecordContent.m_strPEEP;
				objDPArr[78].Value = objRecordContent.m_strPEEPXML;
				objDPArr[79].Value = objRecordContent.m_strTV;
				objDPArr[80].Value = objRecordContent.m_strTVXML;
				objDPArr[81].Value = objRecordContent.m_strVF;
				objDPArr[82].Value = objRecordContent.m_strVFXML;
				objDPArr[83].Value = objRecordContent.m_strBREATHTIMES;
				objDPArr[84].Value = objRecordContent.m_strBREATHTIMESXML;
				objDPArr[85].Value = objRecordContent.m_strLEFTBREATHVOICE;
				objDPArr[86].Value = objRecordContent.m_strLEFTBREATHVOICEXML;
				objDPArr[87].Value = objRecordContent.m_strRIGHTBREATHVOICE;
				objDPArr[88].Value = objRecordContent.m_strRIGHTBREATHVOICEXML;
				objDPArr[89].Value = objRecordContent.m_strPHLEGMCOLOR;
				objDPArr[90].Value = objRecordContent.m_strPHLEGMCOLORXML;
				objDPArr[91].Value = objRecordContent.m_strPHLEGMQUANTITY;
				objDPArr[92].Value = objRecordContent.m_strPHLEGMQUANTITYXML;
				objDPArr[93].Value = objRecordContent.m_strGESTICULATION;
				objDPArr[94].Value = objRecordContent.m_strGESTICULATIONXML;
				objDPArr[95].Value = objRecordContent.m_strPHYSICALTHERAPY;
				objDPArr[96].Value = objRecordContent.m_strPHYSICALTHERAPYXML;
				objDPArr[97].Value = objRecordContent.m_strREMARK;
				objDPArr[98].Value = objRecordContent.m_strREMARKXML;
				objDPArr[99].Value = objRecordContent.m_strWBC;
				objDPArr[100].Value = objRecordContent.m_strWBCXML;
				objDPArr[101].Value = objRecordContent.m_strHB;
				objDPArr[102].Value = objRecordContent.m_strHBXML;
				objDPArr[103].Value = objRecordContent.m_strRBC;
				objDPArr[104].Value = objRecordContent.m_strRBCXML;
				objDPArr[105].Value = objRecordContent.m_strHCT;
				objDPArr[106].Value = objRecordContent.m_strHCTXML;
				objDPArr[107].Value = objRecordContent.m_strPLT;
				objDPArr[108].Value = objRecordContent.m_strPLTXML;
				objDPArr[109].Value = objRecordContent.m_strPH;
				objDPArr[110].Value = objRecordContent.m_strPHXML;
				objDPArr[111].Value = objRecordContent.m_strPCO2;
				objDPArr[112].Value = objRecordContent.m_strPCO2XML;
				objDPArr[113].Value = objRecordContent.m_strPAO2;
				objDPArr[114].Value = objRecordContent.m_strPAO2XML;
				objDPArr[115].Value = objRecordContent.m_strHCO3;
				objDPArr[116].Value = objRecordContent.m_strHCO3XML;
				objDPArr[117].Value = objRecordContent.m_strBE;
				objDPArr[118].Value = objRecordContent.m_strBEXML;
				objDPArr[119].Value = objRecordContent.m_strKPLUS;
				objDPArr[120].Value = objRecordContent.m_strKPLUSXML;
				objDPArr[121].Value = objRecordContent.m_strNAPLUS;
				objDPArr[122].Value = objRecordContent.m_strNAPLUSXML;
				objDPArr[123].Value = objRecordContent.m_strCISUB;
				objDPArr[124].Value = objRecordContent.m_strCISUBXML;
				objDPArr[125].Value = objRecordContent.m_strCAPLUSPLUS;
				objDPArr[126].Value = objRecordContent.m_strCAPLUSPLUSXML;
				objDPArr[127].Value = objRecordContent.m_strGLU;
				objDPArr[128].Value = objRecordContent.m_strGLUXML;
				objDPArr[129].Value = objRecordContent.m_strBUN;
				objDPArr[130].Value = objRecordContent.m_strBUNXML;
				objDPArr[131].Value = objRecordContent.m_strUA;
				objDPArr[132].Value = objRecordContent.m_strUAXML;
				objDPArr[133].Value = objRecordContent.m_strANHYDRIDE;
				objDPArr[134].Value = objRecordContent.m_strANHYDRIDEXML;
				objDPArr[135].Value = objRecordContent.m_strCO2CP;
				objDPArr[136].Value = objRecordContent.m_strCO2CPXML;
				objDPArr[137].Value = objRecordContent.m_strPT;
				objDPArr[138].Value = objRecordContent.m_strPTXML;
				objDPArr[139].Value = objRecordContent.m_strXRAYCHECK;
				objDPArr[140].Value = objRecordContent.m_strXRAYCHECKXML;
				objDPArr[141].Value = objRecordContent.m_strACT;
				objDPArr[142].Value = objRecordContent.m_strACTXML;
				objDPArr[143].Value = objRecordContent.m_strPROPORTION;
				objDPArr[144].Value = objRecordContent.m_strPROPORTIONXML;
				objDPArr[145].Value = objRecordContent.m_strALBUMEN;
				objDPArr[146].Value = objRecordContent.m_strALBUMENXML;
				objDPArr[147].Value = objRecordContent.m_strHIDDENBLOOD;
				objDPArr[148].Value = objRecordContent.m_strHIDDENBLOODXML;
				objDPArr[149].Value = objRecordContent.m_strSKIN;
				objDPArr[150].Value = objRecordContent.m_strSKINXML;
				objDPArr[151].Value = objRecordContent.m_strWASHPERINEUM;
				objDPArr[152].Value = objRecordContent.m_strWASHPERINEUMXML;
				objDPArr[153].Value = objRecordContent.m_strBRUSHBATH;
				objDPArr[154].Value = objRecordContent.m_strBRUSHBATHXML;
				objDPArr[155].Value = objRecordContent.m_strMOUTHTEND;
				objDPArr[156].Value = objRecordContent.m_strMOUTHTENDXML;
				objDPArr[157].Value = objRecordContent.m_strIE;
				objDPArr[158].Value = objRecordContent.m_strIEXML;
				objDPArr[159].Value = objRecordContent.m_strINSPIRATION;
				objDPArr[160].Value = objRecordContent.m_strINSPIRATIONXML;
                //Addd SPO
                objDPArr[161].Value = objRecordContent.m_strSPO;
                objDPArr[162].Value = objRecordContent.m_strSPOXML;

				#endregion

				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				#region 获取IDataParameter数组 T_EMR_CARDIOVASCULARTENDCON_GX
				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(83,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;

				objDPArr2[5].Value = objRecordContent.m_strINFACT1_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strINFACT2_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strINFACT3_RIGHT;
				objDPArr2[8].Value = objRecordContent.m_strINFACT4_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strINFACT5_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strINBLOOD_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strINPERHOUR_RIGHT;
				objDPArr2[12].Value = objRecordContent.m_strINSUM_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strOUTSUM_RIGHT;
				objDPArr2[14].Value = objRecordContent.m_strOUTPERHOUR_RIGHT;
				objDPArr2[15].Value = objRecordContent.m_strOUTFACTPISSSUM_RIGHT;
				objDPArr2[16].Value = objRecordContent.m_strOUTFACTPISS_RIGHT;
				objDPArr2[17].Value = objRecordContent.m_strOUTFACTCHESTJUICE_RIGHT;
				objDPArr2[18].Value = objRecordContent.m_strOUTFACTCHESTJUICESUM_RIGHT;
				objDPArr2[19].Value = objRecordContent.m_strOUTFACTGASTRICJUICE_RIGHT;
				objDPArr2[20].Value = objRecordContent.m_strEXPANDVASMEDICINE_RIGHT;
				objDPArr2[21].Value = objRecordContent.m_strCARDIACDIURESIS_RIGHT;
				objDPArr2[22].Value = objRecordContent.m_strOTHERMEDICINE_RIGHT;
				objDPArr2[23].Value = objRecordContent.m_strCONSCIOUSNESS_RIGHT;
				objDPArr2[24].Value = objRecordContent.m_strPUPIL_RIGHT;
				objDPArr2[25].Value = objRecordContent.m_strLEFTPUPIL_RIGHT;
				objDPArr2[26].Value = objRecordContent.m_strRIGHTPUPIL_RIGHT;
				objDPArr2[27].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
				objDPArr2[28].Value = objRecordContent.m_strTWIGTEMPERATURE_RIGHT;
				objDPArr2[29].Value = objRecordContent.m_strHEARTRATE_RIGHT;
				objDPArr2[30].Value = objRecordContent.m_strHEARTRHYTHM_RIGHT;
				objDPArr2[31].Value = objRecordContent.m_strBPA_RIGHT;
				objDPArr2[32].Value = objRecordContent.m_strBPS_RIGHT;
				objDPArr2[33].Value = objRecordContent.m_strAVGBP_RIGHT;
				objDPArr2[34].Value = objRecordContent.m_strCVP_RIGHT;
				objDPArr2[35].Value = objRecordContent.m_strLAP_RIGHT;
				objDPArr2[36].Value = objRecordContent.m_strBREATHMACHINE_RIGHT;
				objDPArr2[37].Value = objRecordContent.m_strINSERTDEPTH_RIGHT;
				objDPArr2[38].Value = objRecordContent.m_strASSISTANT_RIGHT;
				objDPArr2[39].Value = objRecordContent.m_strFIO2_RIGHT;
				objDPArr2[40].Value = objRecordContent.m_strPEEP_RIGHT;
				objDPArr2[41].Value = objRecordContent.m_strTV_RIGHT;
				objDPArr2[42].Value = objRecordContent.m_strVF_RIGHT;
				objDPArr2[43].Value = objRecordContent.m_strBREATHTIMES_RIGHT;
				objDPArr2[44].Value = objRecordContent.m_strLEFTBREATHVOICE_RIGHT;
				objDPArr2[45].Value = objRecordContent.m_strRIGHTBREATHVOICE_RIGHT;
				objDPArr2[46].Value = objRecordContent.m_strPHLEGMCOLOR_RIGHT;
				objDPArr2[47].Value = objRecordContent.m_strPHLEGMQUANTITY_RIGHT;
				objDPArr2[48].Value = objRecordContent.m_strGESTICULATION_RIGHT;
				objDPArr2[49].Value = objRecordContent.m_strPHYSICALTHERAPY_RIGHT;
				objDPArr2[50].Value = objRecordContent.m_strREMARK_RIGHT;
				objDPArr2[51].Value = objRecordContent.m_strWBC_RIGHT;
				objDPArr2[52].Value = objRecordContent.m_strHB_RIGHT;
				objDPArr2[53].Value = objRecordContent.m_strRBC_RIGHT;
				objDPArr2[54].Value = objRecordContent.m_strHCT_RIGHT;
				objDPArr2[55].Value = objRecordContent.m_strPLT_RIGHT;
				objDPArr2[56].Value = objRecordContent.m_strPH_RIGHT;
				objDPArr2[57].Value = objRecordContent.m_strPCO2_RIGHT;
				objDPArr2[58].Value = objRecordContent.m_strPAO2_RIGHT;
				objDPArr2[59].Value = objRecordContent.m_strHCO3_RIGHT;
				objDPArr2[60].Value = objRecordContent.m_strBE_RIGHT;
				objDPArr2[61].Value = objRecordContent.m_strKPLUS_RIGHT;
				objDPArr2[62].Value = objRecordContent.m_strNAPLUS_RIGHT;
				objDPArr2[63].Value = objRecordContent.m_strCISUB_RIGHT;
				objDPArr2[64].Value = objRecordContent.m_strCAPLUSPLUS_RIGHT;
				objDPArr2[65].Value = objRecordContent.m_strGLU_RIGHT;
				objDPArr2[66].Value = objRecordContent.m_strBUN_RIGHT;
				objDPArr2[67].Value = objRecordContent.m_strUA_RIGHT;
				objDPArr2[68].Value = objRecordContent.m_strANHYDRIDE_RIGHT;
				objDPArr2[69].Value = objRecordContent.m_strCO2CP_RIGHT;
				objDPArr2[70].Value = objRecordContent.m_strPT_RIGHT;
				objDPArr2[71].Value = objRecordContent.m_strXRAYCHECK_RIGHT;
				objDPArr2[72].Value = objRecordContent.m_strACT_RIGHT;
				objDPArr2[73].Value = objRecordContent.m_strPROPORTION_RIGHT;
				objDPArr2[74].Value = objRecordContent.m_strALBUMEN_RIGHT;
				objDPArr2[75].Value = objRecordContent.m_strHIDDENBLOOD_RIGHT;
				objDPArr2[76].Value = objRecordContent.m_strSKIN_RIGHT;
				objDPArr2[77].Value = objRecordContent.m_strWASHPERINEUM_RIGHT;
				objDPArr2[78].Value = objRecordContent.m_strBRUSHBATH_RIGHT;
				objDPArr2[79].Value = objRecordContent.m_strMOUTHTEND_RIGHT;
				objDPArr2[80].Value = objRecordContent.m_strIE_RIGHT;
				objDPArr2[81].Value = objRecordContent.m_strINSPIRATION_RIGHT;

                objDPArr2[82].Value = objRecordContent.m_strSPO_RIGHT;
				#endregion

				//执行SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL,ref lngEff,objDPArr2);
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
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			clsCardiovascularTend_GX objRecordContent = (clsCardiovascularTend_GX)p_objRecordContent;
 
			/// <summary>
			/// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from t_emr_cardiovasculartend_gx t1,t_emr_cardiovasculartendcon_gx t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			try
			{
				//获取IDataParameter数组			
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL,ref dtbValue,objDPArr);
			        
				//如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL,ref dtbValue,objDPArr);
				
					if(lngRes>0 && dtbValue.Rows.Count ==1)
					{
						p_objModifyInfo=new clsPreModifyInfo();
						p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
						p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
					}
					return (long)enmOperationResult.Record_Already_Delete;
				}	
					//从DataTable中获取ModifyDate，使之于objRecordContent.m_dtmModifyDate比较
				else if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//如果相同，返回DB_Succees
                    //if(objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

                    ////否则，返回Record_Already_Modify
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
		/// 把新修改的内容保存到数据库。更新主表,添加子表.
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
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
			clsCardiovascularTend_GX objRecordContent = (clsCardiovascularTend_GX)p_objRecordContent;
			try
			{ 
				#region  获取IDataParameter数组 T_EMR_CARDIOVASCULARTEND_GX
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(157,out objDPArr);
                //p_objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = objRecordContent.m_dtmRECORDDATE;
				objDPArr[1].Value = objRecordContent.m_strINFACT1;
				objDPArr[2].Value = objRecordContent.m_strINFACT1XML;
				objDPArr[3].Value = objRecordContent.m_strINFACT2;
				objDPArr[4].Value = objRecordContent.m_strINFACT2XML;
				objDPArr[5].Value = objRecordContent.m_strINFACT3;
				objDPArr[6].Value = objRecordContent.m_strINFACT3XML;
				objDPArr[7].Value = objRecordContent.m_strINFACT4;
				objDPArr[8].Value = objRecordContent.m_strINFACT4XML;
				objDPArr[9].Value = objRecordContent.m_strINFACT5;
				objDPArr[10].Value = objRecordContent.m_strINFACT5XML;
				objDPArr[11].Value = objRecordContent.m_strINBLOOD;
				objDPArr[12].Value = objRecordContent.m_strINBLOODXML;
				objDPArr[13].Value = objRecordContent.m_strINPERHOUR;
				objDPArr[14].Value = objRecordContent.m_strINPERHOURXML;
				objDPArr[15].Value = objRecordContent.m_strINSUM;
				objDPArr[16].Value = objRecordContent.m_strINSUMXML;
				objDPArr[17].Value = objRecordContent.m_strOUTSUM;
				objDPArr[18].Value = objRecordContent.m_strOUTSUMXML;
				objDPArr[19].Value = objRecordContent.m_strOUTPERHOUR;
				objDPArr[20].Value = objRecordContent.m_strOUTPERHOURXML;
				objDPArr[21].Value = objRecordContent.m_strOUTFACTPISSSUM;
				objDPArr[22].Value = objRecordContent.m_strOUTFACTPISSSUMXML;
				objDPArr[23].Value = objRecordContent.m_strOUTFACTPISS;
				objDPArr[24].Value = objRecordContent.m_strOUTFACTPISSXML;
				objDPArr[25].Value = objRecordContent.m_strOUTFACTCHESTJUICE;
				objDPArr[26].Value = objRecordContent.m_strOUTFACTCHESTJUICEXML;
				objDPArr[27].Value = objRecordContent.m_strOUTFACTCHESTJUICESUM;
				objDPArr[28].Value = objRecordContent.m_strOUTFACTCHESTJUICESUMXML;
				objDPArr[29].Value = objRecordContent.m_strOUTFACTGASTRICJUICE;
				objDPArr[30].Value = objRecordContent.m_strOUTFACTGASTRICJUICEXML;
				objDPArr[31].Value = objRecordContent.m_strEXPANDVASMEDICINE;
				objDPArr[32].Value = objRecordContent.m_strCARDIACDIURESIS;
				objDPArr[33].Value = objRecordContent.m_strOTHERMEDICINE;
				objDPArr[34].Value = objRecordContent.m_strCONSCIOUSNESS;
				objDPArr[35].Value = objRecordContent.m_strCONSCIOUSNESSXML;
				objDPArr[36].Value = objRecordContent.m_strPUPIL;
				objDPArr[37].Value = objRecordContent.m_strPUPILXML;
				objDPArr[38].Value = objRecordContent.m_strLEFTPUPIL;
				objDPArr[39].Value = objRecordContent.m_strLEFTPUPILXML;
				objDPArr[40].Value = objRecordContent.m_strRIGHTPUPIL;
				objDPArr[41].Value = objRecordContent.m_strRIGHTPUPILXML;
				objDPArr[42].Value = objRecordContent.m_strTEMPERATURE;
				objDPArr[43].Value = objRecordContent.m_strTEMPERATUREXML;
				objDPArr[44].Value = objRecordContent.m_strTWIGTEMPERATURE;
				objDPArr[45].Value = objRecordContent.m_strTWIGTEMPERATUREXML;
				objDPArr[46].Value = objRecordContent.m_strHEARTRATE;
				objDPArr[47].Value = objRecordContent.m_strHEARTRATEXML;
				objDPArr[48].Value = objRecordContent.m_strHEARTRHYTHM;
				objDPArr[49].Value = objRecordContent.m_strHEARTRHYTHMXML;
				objDPArr[50].Value = objRecordContent.m_strBPA;
				objDPArr[51].Value = objRecordContent.m_strBPAXML;
				objDPArr[52].Value = objRecordContent.m_strBPS;
				objDPArr[53].Value = objRecordContent.m_strBPSXML;
				objDPArr[54].Value = objRecordContent.m_strAVGBP;
				objDPArr[55].Value = objRecordContent.m_strAVGBPXML;
				objDPArr[56].Value = objRecordContent.m_strCVP;
				objDPArr[57].Value = objRecordContent.m_strCVPXML;
				objDPArr[58].Value = objRecordContent.m_strLAP;
				objDPArr[59].Value = objRecordContent.m_strLAPXML;
				objDPArr[60].Value = objRecordContent.m_strBREATHMACHINE;
				objDPArr[61].Value = objRecordContent.m_strBREATHMACHINEXML;
				objDPArr[62].Value = objRecordContent.m_strINSERTDEPTH;
				objDPArr[63].Value = objRecordContent.m_strINSERTDEPTHXML;
				objDPArr[64].Value = objRecordContent.m_strASSISTANT;
				objDPArr[65].Value = objRecordContent.m_strASSISTANTXML;
				objDPArr[66].Value = objRecordContent.m_strFIO2;
				objDPArr[67].Value = objRecordContent.m_strFIO2XML;
				objDPArr[68].Value = objRecordContent.m_strPEEP;
				objDPArr[69].Value = objRecordContent.m_strPEEPXML;
				objDPArr[70].Value = objRecordContent.m_strTV;
				objDPArr[71].Value = objRecordContent.m_strTVXML;
				objDPArr[72].Value = objRecordContent.m_strVF;
				objDPArr[73].Value = objRecordContent.m_strVFXML;
				objDPArr[74].Value = objRecordContent.m_strBREATHTIMES;
				objDPArr[75].Value = objRecordContent.m_strBREATHTIMESXML;
				objDPArr[76].Value = objRecordContent.m_strLEFTBREATHVOICE;
				objDPArr[77].Value = objRecordContent.m_strLEFTBREATHVOICEXML;
				objDPArr[78].Value = objRecordContent.m_strRIGHTBREATHVOICE;
				objDPArr[79].Value = objRecordContent.m_strRIGHTBREATHVOICEXML;
				objDPArr[80].Value = objRecordContent.m_strPHLEGMCOLOR;
				objDPArr[81].Value = objRecordContent.m_strPHLEGMCOLORXML;
				objDPArr[82].Value = objRecordContent.m_strPHLEGMQUANTITY;
				objDPArr[83].Value = objRecordContent.m_strPHLEGMQUANTITYXML;
				objDPArr[84].Value = objRecordContent.m_strGESTICULATION;
				objDPArr[85].Value = objRecordContent.m_strGESTICULATIONXML;
				objDPArr[86].Value = objRecordContent.m_strPHYSICALTHERAPY;
				objDPArr[87].Value = objRecordContent.m_strPHYSICALTHERAPYXML;
				objDPArr[88].Value = objRecordContent.m_strREMARK;
				objDPArr[89].Value = objRecordContent.m_strREMARKXML;
				objDPArr[90].Value = objRecordContent.m_strWBC;
				objDPArr[91].Value = objRecordContent.m_strWBCXML;
				objDPArr[92].Value = objRecordContent.m_strHB;
				objDPArr[93].Value = objRecordContent.m_strHBXML;
				objDPArr[94].Value = objRecordContent.m_strRBC;
				objDPArr[95].Value = objRecordContent.m_strRBCXML;
				objDPArr[96].Value = objRecordContent.m_strHCT;
				objDPArr[97].Value = objRecordContent.m_strHCTXML;
				objDPArr[98].Value = objRecordContent.m_strPLT;
				objDPArr[99].Value = objRecordContent.m_strPLTXML;
				objDPArr[100].Value = objRecordContent.m_strPH;
				objDPArr[101].Value = objRecordContent.m_strPHXML;
				objDPArr[102].Value = objRecordContent.m_strPCO2;
				objDPArr[103].Value = objRecordContent.m_strPCO2XML;
				objDPArr[104].Value = objRecordContent.m_strPAO2;
				objDPArr[105].Value = objRecordContent.m_strPAO2XML;
				objDPArr[106].Value = objRecordContent.m_strHCO3;
				objDPArr[107].Value = objRecordContent.m_strHCO3XML;
				objDPArr[108].Value = objRecordContent.m_strBE;
				objDPArr[109].Value = objRecordContent.m_strBEXML;
				objDPArr[110].Value = objRecordContent.m_strKPLUS;
				objDPArr[111].Value = objRecordContent.m_strKPLUSXML;
				objDPArr[112].Value = objRecordContent.m_strNAPLUS;
				objDPArr[113].Value = objRecordContent.m_strNAPLUSXML;
				objDPArr[114].Value = objRecordContent.m_strCISUB;
				objDPArr[115].Value = objRecordContent.m_strCISUBXML;
				objDPArr[116].Value = objRecordContent.m_strCAPLUSPLUS;
				objDPArr[117].Value = objRecordContent.m_strCAPLUSPLUSXML;
				objDPArr[118].Value = objRecordContent.m_strGLU;
				objDPArr[119].Value = objRecordContent.m_strGLUXML;
				objDPArr[120].Value = objRecordContent.m_strBUN;
				objDPArr[121].Value = objRecordContent.m_strBUNXML;
				objDPArr[122].Value = objRecordContent.m_strUA;
				objDPArr[123].Value = objRecordContent.m_strUAXML;
				objDPArr[124].Value = objRecordContent.m_strANHYDRIDE;
				objDPArr[125].Value = objRecordContent.m_strANHYDRIDEXML;
				objDPArr[126].Value = objRecordContent.m_strCO2CP;
				objDPArr[127].Value = objRecordContent.m_strCO2CPXML;
				objDPArr[128].Value = objRecordContent.m_strPT;
				objDPArr[129].Value = objRecordContent.m_strPTXML;
				objDPArr[130].Value = objRecordContent.m_strXRAYCHECK;
				objDPArr[131].Value = objRecordContent.m_strXRAYCHECKXML;
				objDPArr[132].Value = objRecordContent.m_strACT;
				objDPArr[133].Value = objRecordContent.m_strACTXML;
				objDPArr[134].Value = objRecordContent.m_strPROPORTION;
				objDPArr[135].Value = objRecordContent.m_strPROPORTIONXML;
				objDPArr[136].Value = objRecordContent.m_strALBUMEN;
				objDPArr[137].Value = objRecordContent.m_strALBUMENXML;
				objDPArr[138].Value = objRecordContent.m_strHIDDENBLOOD;
				objDPArr[139].Value = objRecordContent.m_strHIDDENBLOODXML;
				objDPArr[140].Value = objRecordContent.m_strSKIN;
				objDPArr[141].Value = objRecordContent.m_strSKINXML;
				objDPArr[142].Value = objRecordContent.m_strWASHPERINEUM;
				objDPArr[143].Value = objRecordContent.m_strWASHPERINEUMXML;
				objDPArr[144].Value = objRecordContent.m_strBRUSHBATH;
				objDPArr[145].Value = objRecordContent.m_strBRUSHBATHXML;
				objDPArr[146].Value = objRecordContent.m_strMOUTHTEND;
				objDPArr[147].Value = objRecordContent.m_strMOUTHTENDXML;
				objDPArr[148].Value = objRecordContent.m_strIE;
				objDPArr[149].Value = objRecordContent.m_strIEXML;
				objDPArr[150].Value = objRecordContent.m_strINSPIRATION;
				objDPArr[151].Value = objRecordContent.m_strINSPIRATIONXML;

                objDPArr[152].Value = objRecordContent.m_strSPO;
                objDPArr[153].Value = objRecordContent.m_strSPOXML;

                objDPArr[154].Value = objRecordContent.m_strInPatientID;
                objDPArr[155].DbType = DbType.DateTime;
                objDPArr[155].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[156].DbType = DbType.DateTime;
				objDPArr[156].Value = objRecordContent.m_dtmOpenDate;

                
				#endregion

				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;

				#region  获取IDataParameter数组 T_EMR_CARDIOVASCULARTENDCON_GX
				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(83,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;

				objDPArr2[5].Value = objRecordContent.m_strINFACT1_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strINFACT2_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strINFACT3_RIGHT;
				objDPArr2[8].Value = objRecordContent.m_strINFACT4_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strINFACT5_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strINBLOOD_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strINPERHOUR_RIGHT;
				objDPArr2[12].Value = objRecordContent.m_strINSUM_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strOUTSUM_RIGHT;
				objDPArr2[14].Value = objRecordContent.m_strOUTPERHOUR_RIGHT;
				objDPArr2[15].Value = objRecordContent.m_strOUTFACTPISSSUM_RIGHT;
				objDPArr2[16].Value = objRecordContent.m_strOUTFACTPISS_RIGHT;
				objDPArr2[17].Value = objRecordContent.m_strOUTFACTCHESTJUICE_RIGHT;
				objDPArr2[18].Value = objRecordContent.m_strOUTFACTCHESTJUICESUM_RIGHT;
				objDPArr2[19].Value = objRecordContent.m_strOUTFACTGASTRICJUICE_RIGHT;
				objDPArr2[20].Value = objRecordContent.m_strEXPANDVASMEDICINE_RIGHT;
				objDPArr2[21].Value = objRecordContent.m_strCARDIACDIURESIS_RIGHT;
				objDPArr2[22].Value = objRecordContent.m_strOTHERMEDICINE_RIGHT;
				objDPArr2[23].Value = objRecordContent.m_strCONSCIOUSNESS_RIGHT;
				objDPArr2[24].Value = objRecordContent.m_strPUPIL_RIGHT;
				objDPArr2[25].Value = objRecordContent.m_strLEFTPUPIL_RIGHT;
				objDPArr2[26].Value = objRecordContent.m_strRIGHTPUPIL_RIGHT;
				objDPArr2[27].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
				objDPArr2[28].Value = objRecordContent.m_strTWIGTEMPERATURE_RIGHT;
				objDPArr2[29].Value = objRecordContent.m_strHEARTRATE_RIGHT;
				objDPArr2[30].Value = objRecordContent.m_strHEARTRHYTHM_RIGHT;
				objDPArr2[31].Value = objRecordContent.m_strBPA_RIGHT;
				objDPArr2[32].Value = objRecordContent.m_strBPS_RIGHT;
				objDPArr2[33].Value = objRecordContent.m_strAVGBP_RIGHT;
				objDPArr2[34].Value = objRecordContent.m_strCVP_RIGHT;
				objDPArr2[35].Value = objRecordContent.m_strLAP_RIGHT;
				objDPArr2[36].Value = objRecordContent.m_strBREATHMACHINE_RIGHT;
				objDPArr2[37].Value = objRecordContent.m_strINSERTDEPTH_RIGHT;
				objDPArr2[38].Value = objRecordContent.m_strASSISTANT_RIGHT;
				objDPArr2[39].Value = objRecordContent.m_strFIO2_RIGHT;
				objDPArr2[40].Value = objRecordContent.m_strPEEP_RIGHT;
				objDPArr2[41].Value = objRecordContent.m_strTV_RIGHT;
				objDPArr2[42].Value = objRecordContent.m_strVF_RIGHT;
				objDPArr2[43].Value = objRecordContent.m_strBREATHTIMES_RIGHT;
				objDPArr2[44].Value = objRecordContent.m_strLEFTBREATHVOICE_RIGHT;
				objDPArr2[45].Value = objRecordContent.m_strRIGHTBREATHVOICE_RIGHT;
				objDPArr2[46].Value = objRecordContent.m_strPHLEGMCOLOR_RIGHT;
				objDPArr2[47].Value = objRecordContent.m_strPHLEGMQUANTITY_RIGHT;
				objDPArr2[48].Value = objRecordContent.m_strGESTICULATION_RIGHT;
				objDPArr2[49].Value = objRecordContent.m_strPHYSICALTHERAPY_RIGHT;
				objDPArr2[50].Value = objRecordContent.m_strREMARK_RIGHT;
				objDPArr2[51].Value = objRecordContent.m_strWBC_RIGHT;
				objDPArr2[52].Value = objRecordContent.m_strHB_RIGHT;
				objDPArr2[53].Value = objRecordContent.m_strRBC_RIGHT;
				objDPArr2[54].Value = objRecordContent.m_strHCT_RIGHT;
				objDPArr2[55].Value = objRecordContent.m_strPLT_RIGHT;
				objDPArr2[56].Value = objRecordContent.m_strPH_RIGHT;
				objDPArr2[57].Value = objRecordContent.m_strPCO2_RIGHT;
				objDPArr2[58].Value = objRecordContent.m_strPAO2_RIGHT;
				objDPArr2[59].Value = objRecordContent.m_strHCO3_RIGHT;
				objDPArr2[60].Value = objRecordContent.m_strBE_RIGHT;
				objDPArr2[61].Value = objRecordContent.m_strKPLUS_RIGHT;
				objDPArr2[62].Value = objRecordContent.m_strNAPLUS_RIGHT;
				objDPArr2[63].Value = objRecordContent.m_strCISUB_RIGHT;
				objDPArr2[64].Value = objRecordContent.m_strCAPLUSPLUS_RIGHT;
				objDPArr2[65].Value = objRecordContent.m_strGLU_RIGHT;
				objDPArr2[66].Value = objRecordContent.m_strBUN_RIGHT;
				objDPArr2[67].Value = objRecordContent.m_strUA_RIGHT;
				objDPArr2[68].Value = objRecordContent.m_strANHYDRIDE_RIGHT;
				objDPArr2[69].Value = objRecordContent.m_strCO2CP_RIGHT;
				objDPArr2[70].Value = objRecordContent.m_strPT_RIGHT;
				objDPArr2[71].Value = objRecordContent.m_strXRAYCHECK_RIGHT;
				objDPArr2[72].Value = objRecordContent.m_strACT_RIGHT;
				objDPArr2[73].Value = objRecordContent.m_strPROPORTION_RIGHT;
				objDPArr2[74].Value = objRecordContent.m_strALBUMEN_RIGHT;
				objDPArr2[75].Value = objRecordContent.m_strHIDDENBLOOD_RIGHT;
				objDPArr2[76].Value = objRecordContent.m_strSKIN_RIGHT;
				objDPArr2[77].Value = objRecordContent.m_strWASHPERINEUM_RIGHT;
				objDPArr2[78].Value = objRecordContent.m_strBRUSHBATH_RIGHT;
				objDPArr2[79].Value = objRecordContent.m_strMOUTHTEND_RIGHT;
				objDPArr2[80].Value = objRecordContent.m_strIE_RIGHT;
				objDPArr2[81].Value = objRecordContent.m_strINSPIRATION_RIGHT;

                objDPArr2[82].Value = objRecordContent.m_strSPO_RIGHT;

				#endregion

				//执行SQL			
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL,ref lngEff,objDPArr2);
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

			long lngRes = 0;
			clsCardiovascularTend_GX objRecordContent = new clsCardiovascularTend_GX();

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;	
				objDPArr[4].Value=objRecordContent.m_dtmOpenDate;		
		
				//执行SQL
				long lngEff=0;
				lngRes= p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);

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
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			/// <summary>
			/// 从T_EMR_CARDIOVASCULARTEND_GX和T_EMR_CARDIOVASCULARTENDCON_GX获取LastModifyDate和FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.firstprintdate,b.modifydate from t_emr_cardiovasculartend_gx a,
					t_emr_cardiovasculartendcon_gx b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//设置结果
					p_strFirstPrintDate=dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
					p_dtmModifyDate=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());								
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
			long lngRes = 0;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select t1.inpatientid,
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
       t2.modifydate,
       t2.modifyuserid,
       t2.infact1_right,
       t2.infact2_right,
       t2.infact3_right,
       t2.infact4_right,
       t2.infact5_right,
       t2.inblood_right,
       t2.inperhour_right,
       t2.insum_right,
       t2.outsum_right,
       t2.outperhour_right,
       t2.outfactpisssum_right,
       t2.outfactpiss_right,
       t2.outfactchestjuice_right,
       t2.outfactchestjuicesum_right,
       t2.outfactgastricjuice_right,
       t2.expandvasmedicine_right,
       t2.cardiacdiuresis_right,
       t2.othermedicine_right,
       t2.consciousness_right,
       t2.pupil_right,
       t2.leftpupil_right,
       t2.rightpupil_right,
       t2.temperature_right,
       t2.twigtemperature_right,
       t2.heartrate_right,
       t2.heartrhythm_right,
       t2.bpa_right,
       t2.bps_right,
       t2.avgbp_right,
       t2.cvp_right,
       t2.lap_right,
       t2.breathmachine_right,
       t2.insertdepth_right,
       t2.assistant_right,
       t2.fio2_right,
       t2.peep_right,
       t2.tv_right,
       t2.vf_right,
       t2.breathtimes_right,
       t2.leftbreathvoice_right,
       t2.rightbreathvoice_right,
       t2.phlegmcolor_right,
       t2.phlegmquantity_right,
       t2.gesticulation_right,
       t2.physicaltherapy_right,
       t2.remark_right,
       t2.wbc_right,
       t2.hb_right,
       t2.rbc_right,
       t2.hct_right,
       t2.plt_right,
       t2.ph_right,
       t2.pco2_right,
       t2.pao2_right,
       t2.hco3_right,
       t2.be_right,
       t2.kplus_right,
       t2.naplus_right,
       t2.cisub_right,
       t2.caplusplus_right,
       t2.glu_right,
       t2.bun_right,
       t2.ua_right,
       t2.anhydride_right,
       t2.co2cp_right,
       t2.pt_right,
       t2.xraycheck_right,
       t2.act_right,
       t2.proportion_right,
       t2.albumen_right,
       t2.hiddenblood_right,
       t2.skin_right,
       t2.washperineum_right,
       t2.brushbath_right,
       t2.mouthtend_right,
       t2.ie_right,
       t2.inspiration_right,
       t2.spo_right
  from t_emr_cardiovasculartend_gx t1
 inner join t_emr_cardiovasculartendcon_gx t2 on (t1.inpatientid =
                                                 t2.inpatientid and
                                                 t1.inpatientdate =
                                                 t2.inpatientdate and
                                                 t1.opendate = t2.opendate)
 where t2.modifydate = (select max(modifydate)
                          from t_emr_cardiovasculartendcon_gx
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?";
		
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strOpenDate);
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					#region 设置结果
					clsCardiovascularTend_GX objRecordContent = new clsCardiovascularTend_GX();
					objRecordContent.m_strInPatientID = p_strInPatientID;
					objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[0]["OPENDATE"]);
					objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
					objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(dtbValue.Rows[0]["FIRSTPRINTDATE"]);
					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();
					objRecordContent.m_strDeActivedOperatorID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
					objRecordContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[0]["DEACTIVEDDATE"]);

					objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
					objRecordContent.m_strINFACT1 = dtbValue.Rows[0]["INFACT1"].ToString();
					objRecordContent.m_strINFACT1XML = dtbValue.Rows[0]["INFACT1XML"].ToString();
					objRecordContent.m_strINFACT2 = dtbValue.Rows[0]["INFACT2"].ToString();
					objRecordContent.m_strINFACT2XML = dtbValue.Rows[0]["INFACT2XML"].ToString();
					objRecordContent.m_strINFACT3 = dtbValue.Rows[0]["INFACT3"].ToString();
					objRecordContent.m_strINFACT3XML = dtbValue.Rows[0]["INFACT3XML"].ToString();
					objRecordContent.m_strINFACT4 = dtbValue.Rows[0]["INFACT4"].ToString();
					objRecordContent.m_strINFACT4XML = dtbValue.Rows[0]["INFACT4XML"].ToString();
					objRecordContent.m_strINFACT5 = dtbValue.Rows[0]["INFACT5"].ToString();
					objRecordContent.m_strINFACT5XML = dtbValue.Rows[0]["INFACT5XML"].ToString();
					objRecordContent.m_strINBLOOD = dtbValue.Rows[0]["INBLOOD"].ToString();
					objRecordContent.m_strINBLOODXML = dtbValue.Rows[0]["INBLOODXML"].ToString();
					objRecordContent.m_strINPERHOUR = dtbValue.Rows[0]["INPERHOUR"].ToString();
					objRecordContent.m_strINPERHOURXML = dtbValue.Rows[0]["INPERHOURXML"].ToString();
					objRecordContent.m_strINSUM = dtbValue.Rows[0]["INSUM"].ToString();
					objRecordContent.m_strINSUMXML = dtbValue.Rows[0]["INSUMXML"].ToString();
					objRecordContent.m_strOUTSUM = dtbValue.Rows[0]["OUTSUM"].ToString();
					objRecordContent.m_strOUTSUMXML = dtbValue.Rows[0]["OUTSUMXML"].ToString();
					objRecordContent.m_strOUTPERHOUR = dtbValue.Rows[0]["OUTPERHOUR"].ToString();
					objRecordContent.m_strOUTPERHOURXML = dtbValue.Rows[0]["OUTPERHOURXML"].ToString();
					objRecordContent.m_strOUTFACTPISSSUM = dtbValue.Rows[0]["OUTFACTPISSSUM"].ToString();
					objRecordContent.m_strOUTFACTPISSSUMXML = dtbValue.Rows[0]["OUTFACTPISSSUMXML"].ToString();
					objRecordContent.m_strOUTFACTPISS = dtbValue.Rows[0]["OUTFACTPISS"].ToString();
					objRecordContent.m_strOUTFACTPISSXML = dtbValue.Rows[0]["OUTFACTPISSXML"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICE = dtbValue.Rows[0]["OUTFACTCHESTJUICE"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICEXML = dtbValue.Rows[0]["OUTFACTCHESTJUICEXML"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICESUM = dtbValue.Rows[0]["OUTFACTCHESTJUICESUM"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICESUMXML = dtbValue.Rows[0]["OUTFACTCHESTJUICESUMXML"].ToString();
					objRecordContent.m_strOUTFACTGASTRICJUICE = dtbValue.Rows[0]["OUTFACTGASTRICJUICE"].ToString();
					objRecordContent.m_strOUTFACTGASTRICJUICEXML = dtbValue.Rows[0]["OUTFACTGASTRICJUICEXML"].ToString();
					objRecordContent.m_strEXPANDVASMEDICINE = dtbValue.Rows[0]["EXPANDVASMEDICINE"].ToString();
					objRecordContent.m_strCARDIACDIURESIS = dtbValue.Rows[0]["CARDIACDIURESIS"].ToString();
					objRecordContent.m_strOTHERMEDICINE = dtbValue.Rows[0]["OTHERMEDICINE"].ToString();
					objRecordContent.m_strCONSCIOUSNESS = dtbValue.Rows[0]["CONSCIOUSNESS"].ToString();
					objRecordContent.m_strCONSCIOUSNESSXML = dtbValue.Rows[0]["CONSCIOUSNESSXML"].ToString();
					objRecordContent.m_strPUPIL = dtbValue.Rows[0]["PUPIL"].ToString();
					objRecordContent.m_strPUPILXML = dtbValue.Rows[0]["PUPILXML"].ToString();
					objRecordContent.m_strLEFTPUPIL = dtbValue.Rows[0]["LEFTPUPIL"].ToString();
					objRecordContent.m_strLEFTPUPILXML = dtbValue.Rows[0]["LEFTPUPILXML"].ToString();
					objRecordContent.m_strRIGHTPUPIL = dtbValue.Rows[0]["RIGHTPUPIL"].ToString();
					objRecordContent.m_strRIGHTPUPILXML = dtbValue.Rows[0]["RIGHTPUPILXML"].ToString();
					objRecordContent.m_strTEMPERATURE = dtbValue.Rows[0]["TEMPERATURE"].ToString();
					objRecordContent.m_strTEMPERATUREXML = dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
					objRecordContent.m_strTWIGTEMPERATURE = dtbValue.Rows[0]["TWIGTEMPERATURE"].ToString();
					objRecordContent.m_strTWIGTEMPERATUREXML = dtbValue.Rows[0]["TWIGTEMPERATUREXML"].ToString();
					objRecordContent.m_strHEARTRATE = dtbValue.Rows[0]["HEARTRATE"].ToString();
					objRecordContent.m_strHEARTRATEXML = dtbValue.Rows[0]["HEARTRATEXML"].ToString();
					objRecordContent.m_strHEARTRHYTHM = dtbValue.Rows[0]["HEARTRHYTHM"].ToString();
					objRecordContent.m_strHEARTRHYTHMXML = dtbValue.Rows[0]["HEARTRHYTHMXML"].ToString();
					objRecordContent.m_strBPA = dtbValue.Rows[0]["BPA"].ToString();
					objRecordContent.m_strBPAXML = dtbValue.Rows[0]["BPAXML"].ToString();
					objRecordContent.m_strBPS = dtbValue.Rows[0]["BPS"].ToString();
					objRecordContent.m_strBPSXML = dtbValue.Rows[0]["BPSXML"].ToString();
					objRecordContent.m_strAVGBP = dtbValue.Rows[0]["AVGBP"].ToString();
					objRecordContent.m_strAVGBPXML = dtbValue.Rows[0]["AVGBPXML"].ToString();
					objRecordContent.m_strCVP = dtbValue.Rows[0]["CVP"].ToString();
					objRecordContent.m_strCVPXML = dtbValue.Rows[0]["CVPXML"].ToString();
					objRecordContent.m_strLAP = dtbValue.Rows[0]["LAP"].ToString();
					objRecordContent.m_strLAPXML = dtbValue.Rows[0]["LAPXML"].ToString();

                    objRecordContent.m_strSPO = dtbValue.Rows[0]["SPO"].ToString();
                    objRecordContent.m_strSPOXML = dtbValue.Rows[0]["SPOXML"].ToString();

					objRecordContent.m_strBREATHMACHINE = dtbValue.Rows[0]["BREATHMACHINE"].ToString();
					objRecordContent.m_strBREATHMACHINEXML = dtbValue.Rows[0]["BREATHMACHINEXML"].ToString();
					objRecordContent.m_strINSERTDEPTH = dtbValue.Rows[0]["INSERTDEPTH"].ToString();
					objRecordContent.m_strINSERTDEPTHXML = dtbValue.Rows[0]["INSERTDEPTHXML"].ToString();
					objRecordContent.m_strASSISTANT = dtbValue.Rows[0]["ASSISTANT"].ToString();
					objRecordContent.m_strASSISTANTXML = dtbValue.Rows[0]["ASSISTANTXML"].ToString();
					objRecordContent.m_strFIO2 = dtbValue.Rows[0]["FIO2"].ToString();
					objRecordContent.m_strFIO2XML = dtbValue.Rows[0]["FIO2XML"].ToString();
					objRecordContent.m_strPEEP = dtbValue.Rows[0]["PEEP"].ToString();
					objRecordContent.m_strPEEPXML = dtbValue.Rows[0]["PEEPXML"].ToString();
					objRecordContent.m_strTV = dtbValue.Rows[0]["TV"].ToString();
					objRecordContent.m_strTVXML = dtbValue.Rows[0]["TVXML"].ToString();
					objRecordContent.m_strVF = dtbValue.Rows[0]["VF"].ToString();
					objRecordContent.m_strVFXML = dtbValue.Rows[0]["VFXML"].ToString();
					objRecordContent.m_strBREATHTIMES = dtbValue.Rows[0]["BREATHTIMES"].ToString();
					objRecordContent.m_strBREATHTIMESXML = dtbValue.Rows[0]["BREATHTIMESXML"].ToString();
					objRecordContent.m_strLEFTBREATHVOICE = dtbValue.Rows[0]["LEFTBREATHVOICE"].ToString();
					objRecordContent.m_strLEFTBREATHVOICEXML = dtbValue.Rows[0]["LEFTBREATHVOICEXML"].ToString();
					objRecordContent.m_strRIGHTBREATHVOICE = dtbValue.Rows[0]["RIGHTBREATHVOICE"].ToString();
					objRecordContent.m_strRIGHTBREATHVOICEXML = dtbValue.Rows[0]["RIGHTBREATHVOICEXML"].ToString();
					objRecordContent.m_strPHLEGMCOLOR = dtbValue.Rows[0]["PHLEGMCOLOR"].ToString();
					objRecordContent.m_strPHLEGMCOLORXML = dtbValue.Rows[0]["PHLEGMCOLORXML"].ToString();
					objRecordContent.m_strPHLEGMQUANTITY = dtbValue.Rows[0]["PHLEGMQUANTITY"].ToString();
					objRecordContent.m_strPHLEGMQUANTITYXML = dtbValue.Rows[0]["PHLEGMQUANTITYXML"].ToString();
					objRecordContent.m_strGESTICULATION = dtbValue.Rows[0]["GESTICULATION"].ToString();
					objRecordContent.m_strGESTICULATIONXML = dtbValue.Rows[0]["GESTICULATIONXML"].ToString();
					objRecordContent.m_strPHYSICALTHERAPY = dtbValue.Rows[0]["PHYSICALTHERAPY"].ToString();
					objRecordContent.m_strPHYSICALTHERAPYXML = dtbValue.Rows[0]["PHYSICALTHERAPYXML"].ToString();
					objRecordContent.m_strREMARK = dtbValue.Rows[0]["REMARK"].ToString();
					objRecordContent.m_strREMARKXML = dtbValue.Rows[0]["REMARKXML"].ToString();
					objRecordContent.m_strWBC = dtbValue.Rows[0]["WBC"].ToString();
					objRecordContent.m_strWBCXML = dtbValue.Rows[0]["WBCXML"].ToString();
					objRecordContent.m_strHB = dtbValue.Rows[0]["HB"].ToString();
					objRecordContent.m_strHBXML = dtbValue.Rows[0]["HBXML"].ToString();
					objRecordContent.m_strRBC = dtbValue.Rows[0]["RBC"].ToString();
					objRecordContent.m_strRBCXML = dtbValue.Rows[0]["RBCXML"].ToString();
					objRecordContent.m_strHCT = dtbValue.Rows[0]["HCT"].ToString();
					objRecordContent.m_strHCTXML = dtbValue.Rows[0]["HCTXML"].ToString();
					objRecordContent.m_strPLT = dtbValue.Rows[0]["PLT"].ToString();
					objRecordContent.m_strPLTXML = dtbValue.Rows[0]["PLTXML"].ToString();
					objRecordContent.m_strPH = dtbValue.Rows[0]["PH"].ToString();
					objRecordContent.m_strPHXML = dtbValue.Rows[0]["PHXML"].ToString();
					objRecordContent.m_strPCO2 = dtbValue.Rows[0]["PCO2"].ToString();
					objRecordContent.m_strPCO2XML = dtbValue.Rows[0]["PCO2XML"].ToString();
					objRecordContent.m_strPAO2 = dtbValue.Rows[0]["PAO2"].ToString();
					objRecordContent.m_strPAO2XML = dtbValue.Rows[0]["PAO2XML"].ToString();
					objRecordContent.m_strHCO3 = dtbValue.Rows[0]["HCO3"].ToString();
					objRecordContent.m_strHCO3XML = dtbValue.Rows[0]["HCO3XML"].ToString();
					objRecordContent.m_strBE = dtbValue.Rows[0]["BE"].ToString();
					objRecordContent.m_strBEXML = dtbValue.Rows[0]["BEXML"].ToString();
					objRecordContent.m_strKPLUS = dtbValue.Rows[0]["KPLUS"].ToString();
					objRecordContent.m_strKPLUSXML = dtbValue.Rows[0]["KPLUSXML"].ToString();
					objRecordContent.m_strNAPLUS = dtbValue.Rows[0]["NAPLUS"].ToString();
					objRecordContent.m_strNAPLUSXML = dtbValue.Rows[0]["NAPLUSXML"].ToString();
					objRecordContent.m_strCISUB = dtbValue.Rows[0]["CISUB"].ToString();
					objRecordContent.m_strCISUBXML = dtbValue.Rows[0]["CISUBXML"].ToString();
					objRecordContent.m_strCAPLUSPLUS = dtbValue.Rows[0]["CAPLUSPLUS"].ToString();
					objRecordContent.m_strCAPLUSPLUSXML = dtbValue.Rows[0]["CAPLUSPLUSXML"].ToString();
					objRecordContent.m_strGLU = dtbValue.Rows[0]["GLU"].ToString();
					objRecordContent.m_strGLUXML = dtbValue.Rows[0]["GLUXML"].ToString();
					objRecordContent.m_strBUN = dtbValue.Rows[0]["BUN"].ToString();
					objRecordContent.m_strBUNXML = dtbValue.Rows[0]["BUNXML"].ToString();
					objRecordContent.m_strUA = dtbValue.Rows[0]["UA"].ToString();
					objRecordContent.m_strUAXML = dtbValue.Rows[0]["UAXML"].ToString();
					objRecordContent.m_strANHYDRIDE = dtbValue.Rows[0]["ANHYDRIDE"].ToString();
					objRecordContent.m_strANHYDRIDEXML = dtbValue.Rows[0]["ANHYDRIDEXML"].ToString();
					objRecordContent.m_strCO2CP = dtbValue.Rows[0]["CO2CP"].ToString();
					objRecordContent.m_strCO2CPXML = dtbValue.Rows[0]["CO2CPXML"].ToString();
					objRecordContent.m_strPT = dtbValue.Rows[0]["PT"].ToString();
					objRecordContent.m_strPTXML = dtbValue.Rows[0]["PTXML"].ToString();
					objRecordContent.m_strXRAYCHECK = dtbValue.Rows[0]["XRAYCHECK"].ToString();
					objRecordContent.m_strXRAYCHECKXML = dtbValue.Rows[0]["XRAYCHECKXML"].ToString();
					objRecordContent.m_strACT = dtbValue.Rows[0]["ACT"].ToString();
					objRecordContent.m_strACTXML = dtbValue.Rows[0]["ACTXML"].ToString();
					objRecordContent.m_strPROPORTION = dtbValue.Rows[0]["PROPORTION"].ToString();
					objRecordContent.m_strPROPORTIONXML = dtbValue.Rows[0]["PROPORTIONXML"].ToString();
					objRecordContent.m_strALBUMEN = dtbValue.Rows[0]["ALBUMEN"].ToString();
					objRecordContent.m_strALBUMENXML = dtbValue.Rows[0]["ALBUMENXML"].ToString();
					objRecordContent.m_strHIDDENBLOOD = dtbValue.Rows[0]["HIDDENBLOOD"].ToString();
					objRecordContent.m_strHIDDENBLOODXML = dtbValue.Rows[0]["HIDDENBLOODXML"].ToString();
					objRecordContent.m_strSKIN = dtbValue.Rows[0]["SKIN"].ToString();
					objRecordContent.m_strSKINXML = dtbValue.Rows[0]["SKINXML"].ToString();
					objRecordContent.m_strWASHPERINEUM = dtbValue.Rows[0]["WASHPERINEUM"].ToString();
					objRecordContent.m_strWASHPERINEUMXML = dtbValue.Rows[0]["WASHPERINEUMXML"].ToString();
					objRecordContent.m_strBRUSHBATH = dtbValue.Rows[0]["BRUSHBATH"].ToString();
					objRecordContent.m_strBRUSHBATHXML = dtbValue.Rows[0]["BRUSHBATHXML"].ToString();
					objRecordContent.m_strMOUTHTEND = dtbValue.Rows[0]["MOUTHTEND"].ToString();
					objRecordContent.m_strMOUTHTENDXML = dtbValue.Rows[0]["MOUTHTENDXML"].ToString();
					objRecordContent.m_strIE = dtbValue.Rows[0]["IE"].ToString();
					objRecordContent.m_strIEXML = dtbValue.Rows[0]["IEXML"].ToString();
					objRecordContent.m_strIE_RIGHT = dtbValue.Rows[0]["IE_RIGHT"].ToString();
					objRecordContent.m_strINSPIRATION = dtbValue.Rows[0]["INSPIRATION"].ToString();
					objRecordContent.m_strINSPIRATIONXML = dtbValue.Rows[0]["INSPIRATIONXML"].ToString();
					objRecordContent.m_strINSPIRATION_RIGHT = dtbValue.Rows[0]["INSPIRATION_RIGHT"].ToString();
							
					objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
					objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					objRecordContent.m_strINFACT1_RIGHT = dtbValue.Rows[0]["INFACT1_RIGHT"].ToString();
					objRecordContent.m_strINFACT2_RIGHT = dtbValue.Rows[0]["INFACT2_RIGHT"].ToString();
					objRecordContent.m_strINFACT3_RIGHT = dtbValue.Rows[0]["INFACT3_RIGHT"].ToString();
					objRecordContent.m_strINFACT4_RIGHT = dtbValue.Rows[0]["INFACT4_RIGHT"].ToString();
					objRecordContent.m_strINFACT5_RIGHT = dtbValue.Rows[0]["INFACT5_RIGHT"].ToString();
					objRecordContent.m_strINBLOOD_RIGHT = dtbValue.Rows[0]["INBLOOD_RIGHT"].ToString();
					objRecordContent.m_strINPERHOUR_RIGHT = dtbValue.Rows[0]["INPERHOUR_RIGHT"].ToString();
					objRecordContent.m_strINSUM_RIGHT = dtbValue.Rows[0]["INSUM_RIGHT"].ToString();
					objRecordContent.m_strOUTSUM_RIGHT = dtbValue.Rows[0]["OUTSUM_RIGHT"].ToString();
					objRecordContent.m_strOUTPERHOUR_RIGHT = dtbValue.Rows[0]["OUTPERHOUR_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTPISSSUM_RIGHT = dtbValue.Rows[0]["OUTFACTPISSSUM_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTPISS_RIGHT = dtbValue.Rows[0]["OUTFACTPISS_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICE_RIGHT = dtbValue.Rows[0]["OUTFACTCHESTJUICE_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTCHESTJUICESUM_RIGHT = dtbValue.Rows[0]["OUTFACTCHESTJUICESUM_RIGHT"].ToString();
					objRecordContent.m_strOUTFACTGASTRICJUICE_RIGHT = dtbValue.Rows[0]["OUTFACTGASTRICJUICE_RIGHT"].ToString();
					objRecordContent.m_strEXPANDVASMEDICINE_RIGHT = dtbValue.Rows[0]["EXPANDVASMEDICINE_RIGHT"].ToString();
					objRecordContent.m_strCARDIACDIURESIS_RIGHT = dtbValue.Rows[0]["CARDIACDIURESIS_RIGHT"].ToString();
					objRecordContent.m_strOTHERMEDICINE_RIGHT = dtbValue.Rows[0]["OTHERMEDICINE_RIGHT"].ToString();
					objRecordContent.m_strCONSCIOUSNESS_RIGHT = dtbValue.Rows[0]["CONSCIOUSNESS_RIGHT"].ToString();
					objRecordContent.m_strPUPIL_RIGHT = dtbValue.Rows[0]["PUPIL_RIGHT"].ToString();
					objRecordContent.m_strLEFTPUPIL_RIGHT = dtbValue.Rows[0]["LEFTPUPIL_RIGHT"].ToString();
					objRecordContent.m_strRIGHTPUPIL_RIGHT = dtbValue.Rows[0]["RIGHTPUPIL_RIGHT"].ToString();
					objRecordContent.m_strTEMPERATURE_RIGHT = dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strTWIGTEMPERATURE_RIGHT = dtbValue.Rows[0]["TWIGTEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strHEARTRATE_RIGHT = dtbValue.Rows[0]["HEARTRATE_RIGHT"].ToString();
					objRecordContent.m_strHEARTRHYTHM_RIGHT = dtbValue.Rows[0]["HEARTRHYTHM_RIGHT"].ToString();
					objRecordContent.m_strBPA_RIGHT = dtbValue.Rows[0]["BPA_RIGHT"].ToString();
					objRecordContent.m_strBPS_RIGHT = dtbValue.Rows[0]["BPS_RIGHT"].ToString();
					objRecordContent.m_strAVGBP_RIGHT = dtbValue.Rows[0]["AVGBP_RIGHT"].ToString();
					objRecordContent.m_strCVP_RIGHT = dtbValue.Rows[0]["CVP_RIGHT"].ToString();
					objRecordContent.m_strLAP_RIGHT = dtbValue.Rows[0]["LAP_RIGHT"].ToString();
                    objRecordContent.m_strSPO_RIGHT = dtbValue.Rows[0]["SPO_RIGHT"].ToString();

					objRecordContent.m_strBREATHMACHINE_RIGHT = dtbValue.Rows[0]["BREATHMACHINE_RIGHT"].ToString();
					objRecordContent.m_strINSERTDEPTH_RIGHT = dtbValue.Rows[0]["INSERTDEPTH_RIGHT"].ToString();
					objRecordContent.m_strASSISTANT_RIGHT = dtbValue.Rows[0]["ASSISTANT_RIGHT"].ToString();
					objRecordContent.m_strFIO2_RIGHT = dtbValue.Rows[0]["FIO2_RIGHT"].ToString();
					objRecordContent.m_strPEEP_RIGHT = dtbValue.Rows[0]["PEEP_RIGHT"].ToString();
					objRecordContent.m_strTV_RIGHT = dtbValue.Rows[0]["TV_RIGHT"].ToString();
					objRecordContent.m_strVF_RIGHT = dtbValue.Rows[0]["VF_RIGHT"].ToString();
					objRecordContent.m_strBREATHTIMES_RIGHT = dtbValue.Rows[0]["BREATHTIMES_RIGHT"].ToString();
					objRecordContent.m_strLEFTBREATHVOICE_RIGHT = dtbValue.Rows[0]["LEFTBREATHVOICE_RIGHT"].ToString();
					objRecordContent.m_strRIGHTBREATHVOICE_RIGHT = dtbValue.Rows[0]["RIGHTBREATHVOICE_RIGHT"].ToString();
					objRecordContent.m_strPHLEGMCOLOR_RIGHT = dtbValue.Rows[0]["PHLEGMCOLOR_RIGHT"].ToString();
					objRecordContent.m_strPHLEGMQUANTITY_RIGHT = dtbValue.Rows[0]["PHLEGMQUANTITY_RIGHT"].ToString();
					objRecordContent.m_strGESTICULATION_RIGHT = dtbValue.Rows[0]["GESTICULATION_RIGHT"].ToString();
					objRecordContent.m_strPHYSICALTHERAPY_RIGHT = dtbValue.Rows[0]["PHYSICALTHERAPY_RIGHT"].ToString();
					objRecordContent.m_strREMARK_RIGHT = dtbValue.Rows[0]["REMARK_RIGHT"].ToString();
					objRecordContent.m_strWBC_RIGHT = dtbValue.Rows[0]["WBC_RIGHT"].ToString();
					objRecordContent.m_strHB_RIGHT = dtbValue.Rows[0]["HB_RIGHT"].ToString();
					objRecordContent.m_strRBC_RIGHT = dtbValue.Rows[0]["RBC_RIGHT"].ToString();
					objRecordContent.m_strHCT_RIGHT = dtbValue.Rows[0]["HCT_RIGHT"].ToString();
					objRecordContent.m_strPLT_RIGHT = dtbValue.Rows[0]["PLT_RIGHT"].ToString();
					objRecordContent.m_strPH_RIGHT = dtbValue.Rows[0]["PH_RIGHT"].ToString();
					objRecordContent.m_strPCO2_RIGHT = dtbValue.Rows[0]["PCO2_RIGHT"].ToString();
					objRecordContent.m_strPAO2_RIGHT = dtbValue.Rows[0]["PAO2_RIGHT"].ToString();
					objRecordContent.m_strHCO3_RIGHT = dtbValue.Rows[0]["HCO3_RIGHT"].ToString();
					objRecordContent.m_strBE_RIGHT = dtbValue.Rows[0]["BE_RIGHT"].ToString();
					objRecordContent.m_strKPLUS_RIGHT = dtbValue.Rows[0]["KPLUS_RIGHT"].ToString();
					objRecordContent.m_strNAPLUS_RIGHT = dtbValue.Rows[0]["NAPLUS_RIGHT"].ToString();
					objRecordContent.m_strCISUB_RIGHT = dtbValue.Rows[0]["CISUB_RIGHT"].ToString();
					objRecordContent.m_strCAPLUSPLUS_RIGHT = dtbValue.Rows[0]["CAPLUSPLUS_RIGHT"].ToString();
					objRecordContent.m_strGLU_RIGHT = dtbValue.Rows[0]["GLU_RIGHT"].ToString();
					objRecordContent.m_strBUN_RIGHT = dtbValue.Rows[0]["BUN_RIGHT"].ToString();
					objRecordContent.m_strUA_RIGHT = dtbValue.Rows[0]["UA_RIGHT"].ToString();
					objRecordContent.m_strANHYDRIDE_RIGHT = dtbValue.Rows[0]["ANHYDRIDE_RIGHT"].ToString();
					objRecordContent.m_strCO2CP_RIGHT = dtbValue.Rows[0]["CO2CP_RIGHT"].ToString();
					objRecordContent.m_strPT_RIGHT = dtbValue.Rows[0]["PT_RIGHT"].ToString();
					objRecordContent.m_strXRAYCHECK_RIGHT = dtbValue.Rows[0]["XRAYCHECK_RIGHT"].ToString();
					objRecordContent.m_strACT_RIGHT = dtbValue.Rows[0]["ACT_RIGHT"].ToString();
					objRecordContent.m_strPROPORTION_RIGHT = dtbValue.Rows[0]["PROPORTION_RIGHT"].ToString();
					objRecordContent.m_strALBUMEN_RIGHT = dtbValue.Rows[0]["ALBUMEN_RIGHT"].ToString();
					objRecordContent.m_strHIDDENBLOOD_RIGHT = dtbValue.Rows[0]["HIDDENBLOOD_RIGHT"].ToString();
					objRecordContent.m_strSKIN_RIGHT = dtbValue.Rows[0]["SKIN_RIGHT"].ToString();
					objRecordContent.m_strWASHPERINEUM_RIGHT = dtbValue.Rows[0]["WASHPERINEUM_RIGHT"].ToString();
					objRecordContent.m_strBRUSHBATH_RIGHT = dtbValue.Rows[0]["BRUSHBATH_RIGHT"].ToString();
					objRecordContent.m_strMOUTHTEND_RIGHT = dtbValue.Rows[0]["MOUTHTEND_RIGHT"].ToString();

					p_objRecordContent = objRecordContent;
					#endregion 
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
		/// 更新基本信息
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngUpdateBaseInfo(clsCardiovascularBaseInfo_GX p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;
			clsHRPTableService p_objHRPServ = new clsHRPTableService();
			try
			{
				string strSQL=@"update t_emr_cardiovascularbaseinfo 
								set modifydate=?,modifyuserid=?,weight=?,afteropdays=?,opname=?,opmedicine1=?,opmedicine2=?,
								opmedicine3=?,opmedicine4=?,opmedicine5=?,longclasssignid=?,officesignid=?,smallnightclasssignid=?,
								bignightclasssignid=? 
								where inpatientid=? and inpatientdate=? and recorddate=? and status=0";
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(17, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value = p_objRecordContent.m_dtmModifyDate;
				objDPArr[1].Value = p_objRecordContent.m_strModifyUserID;
				objDPArr[2].Value = p_objRecordContent.m_dblWEITHT;
				objDPArr[3].Value = p_objRecordContent.m_strAFTEROPDAYS;
				objDPArr[4].Value = p_objRecordContent.m_strOPNAME;
				objDPArr[5].Value = p_objRecordContent.m_strOPMEDICINE1;
				objDPArr[6].Value = p_objRecordContent.m_strOPMEDICINE2;
				objDPArr[7].Value = p_objRecordContent.m_strOPMEDICINE3;
				objDPArr[8].Value = p_objRecordContent.m_strOPMEDICINE4;
				objDPArr[9].Value = p_objRecordContent.m_strOPMEDICINE5;
				objDPArr[10].Value = p_objRecordContent.m_strLONGCLASSSIGNID;
				objDPArr[11].Value = p_objRecordContent.m_strOFFICESIGNID;
				objDPArr[12].Value = p_objRecordContent.m_strSMALLNIGHTCLASSSIGNID;
				objDPArr[13].Value = p_objRecordContent.m_strBIGNIGHTCLASSSIGNID;
                objDPArr[14].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[16].DbType = DbType.DateTime;
				objDPArr[16].Value = p_objRecordContent.m_dtmRECORDDATE;

				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            finally
            {
                //p_objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 保存基本信息
		/// </summary>
		/// <param name="p_objRecordContent">病人信息</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewBaseInfo(clsCardiovascularBaseInfo_GX p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@"insert into t_emr_cardiovascularbaseinfo (inpatientid,inpatientdate,opendate,createdate,
					createuserid,modifydate,modifyuserid,weight,afteropdays,opname,opmedicine1,opmedicine2,opmedicine3,
					opmedicine4,opmedicine5,longclasssignid,officesignid,smallnightclasssignid,bignightclasssignid,
					recorddate,status) 
					values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				//获取IDataParameter数组
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(21,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;
                objDPArr[4].Value = p_objRecordContent.m_strCreateUserID;
                objDPArr[5].DbType = DbType.DateTime;
				objDPArr[5].Value = p_objRecordContent.m_dtmModifyDate;
				objDPArr[6].Value = p_objRecordContent.m_strModifyUserID;
				objDPArr[7].Value = p_objRecordContent.m_dblWEITHT;
				objDPArr[8].Value = p_objRecordContent.m_strAFTEROPDAYS;
				objDPArr[9].Value = p_objRecordContent.m_strOPNAME;
				objDPArr[10].Value = p_objRecordContent.m_strOPMEDICINE1;
				objDPArr[11].Value = p_objRecordContent.m_strOPMEDICINE2;
				objDPArr[12].Value = p_objRecordContent.m_strOPMEDICINE3;
				objDPArr[13].Value = p_objRecordContent.m_strOPMEDICINE4;
				objDPArr[14].Value = p_objRecordContent.m_strOPMEDICINE5;
				objDPArr[15].Value = p_objRecordContent.m_strLONGCLASSSIGNID;
				objDPArr[16].Value = p_objRecordContent.m_strOFFICESIGNID;
				objDPArr[17].Value = p_objRecordContent.m_strSMALLNIGHTCLASSSIGNID;
                objDPArr[18].Value = p_objRecordContent.m_strBIGNIGHTCLASSSIGNID;
                objDPArr[19].DbType = DbType.DateTime;
				objDPArr[19].Value = p_objRecordContent.m_dtmRECORDDATE;
				objDPArr[20].Value = 0;
				
				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //p_objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 获取指定基本信息
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strRecordDate">记录日期</param>
		/// <param name="p_objRecordContent">返回所需记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBaseInfo(string p_strInPatientID,string p_strInPatientDate,string p_strRecordDate,
			out clsCardiovascularBaseInfo_GX p_objRecordContent)
		{
			long lngRes=0;
			p_objRecordContent=null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
                string strSQL = @"select t.inpatientid,
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
       f_getempnamebyno(t.createuserid) as lastname_vchr
  from t_emr_cardiovascularbaseinfo t
 where t.status = 0
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.recorddate = ?";
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strRecordDate);
				//生成DataTable
				DataTable dtbResult = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbResult,objDPArr);
				//从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
				{
					p_objRecordContent = new clsCardiovascularBaseInfo_GX(); 
					p_objRecordContent.m_strInPatientID = dtbResult.Rows[0]["INPATIENTID"].ToString();
					p_objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbResult.Rows[0]["INPATIENTDATE"]);
					p_objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbResult.Rows[0]["OPENDATE"]);
					p_objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE"]);
					p_objRecordContent.m_strCreateUserID = dtbResult.Rows[0]["CREATEUSERID"].ToString();
					p_objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbResult.Rows[0]["MODIFYDATE"]);
					p_objRecordContent.m_strModifyUserID = dtbResult.Rows[0]["MODIFYUSERID"].ToString();
					p_objRecordContent.m_dblWEITHT = Convert.ToDouble(dtbResult.Rows[0]["WEIGHT"]);
					p_objRecordContent.m_strAFTEROPDAYS = dtbResult.Rows[0]["AFTEROPDAYS"].ToString();
					p_objRecordContent.m_strOPNAME = dtbResult.Rows[0]["OPNAME"].ToString();
					p_objRecordContent.m_strOPMEDICINE1 = dtbResult.Rows[0]["OPMEDICINE1"].ToString();
					p_objRecordContent.m_strOPMEDICINE2 = dtbResult.Rows[0]["OPMEDICINE2"].ToString();
					p_objRecordContent.m_strOPMEDICINE3 = dtbResult.Rows[0]["OPMEDICINE3"].ToString();
					p_objRecordContent.m_strOPMEDICINE4 = dtbResult.Rows[0]["OPMEDICINE4"].ToString();
					p_objRecordContent.m_strOPMEDICINE5 = dtbResult.Rows[0]["OPMEDICINE5"].ToString();
					p_objRecordContent.m_strLONGCLASSSIGNID = dtbResult.Rows[0]["LONGCLASSSIGNID"].ToString();
					p_objRecordContent.m_strOFFICESIGNID = dtbResult.Rows[0]["OFFICESIGNID"].ToString();
					p_objRecordContent.m_strSMALLNIGHTCLASSSIGNID = dtbResult.Rows[0]["SMALLNIGHTCLASSSIGNID"].ToString();
					p_objRecordContent.m_strBIGNIGHTCLASSSIGNID = dtbResult.Rows[0]["BIGNIGHTCLASSSIGNID"].ToString();
					p_objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbResult.Rows[0]["RECORDDATE"]);
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
                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		/// 删除指定基本信息
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strRecordDate">记录时间</param>
		/// <param name="p_strDeactivedDate">删除日期</param>
		/// <param name="p_strDeactivedOperatorID">删除者ID</param></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteBaseInfo(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordDate,
			string p_strDeactivedDate,
			string p_strDeactivedOperatorID)
		{
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@"update t_emr_cardiovascularbaseinfo
								set status = 1, deactiveddate = ?, deactivedoperatorid = ?
								where inpatientid = ?
								and inpatientdate = ?
								and recorddate = ?";
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=DateTime.Parse(p_strDeactivedDate);
				objDPArr[1].Value=p_strDeactivedOperatorID;
                objDPArr[2].Value = p_strInPatientID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=DateTime.Parse(p_strRecordDate);
				//执行查询 
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		/// 删除该天的特护记录
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strRecordDate">记录时间</param>
		/// <param name="p_strDeactivedDate">删除日期</param>
		/// <param name="p_strDeactivedOperatorID">删除者ID</param></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteDayTend(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordDate,
			string p_strDeactivedDate,
			string p_strDeactivedOperatorID)
		{
			long lngRes=0;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL=@"update t_emr_cardiovasculartend_gx
								set status = 1, deactiveddate = ?, deactivedoperatorid = ?
								where inpatientid = ?
								and inpatientdate = ?
								and recorddate between ? and ?";
				DateTime dtmStart = DateTime.Parse(p_strRecordDate);
				string strEnd = dtmStart.ToString("yyyy-MM-dd 23:59:59");
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=DateTime.Parse(p_strDeactivedDate);
				objDPArr[1].Value=p_strDeactivedOperatorID;
                objDPArr[2].Value = p_strInPatientID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = dtmStart;
                objDPArr[5].DbType = DbType.DateTime;
				objDPArr[5].Value=DateTime.Parse(strEnd);
				//执行查询 
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //p_objHRPServ.Dispose();
            }
			return lngRes;

		}

		/// <summary>
		/// 获取指定病人所有记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objBaseInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBaseInfo(string p_strInPatientID,
			string p_strInPatientDate,
			out clsCardiovascularBaseInfo_GX[] p_objBaseInfoArr)
		{
			long lngRes = 0;
			p_objBaseInfoArr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
                string strSQL = @"select t.inpatientid,
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
       d.lastname_vchr
  from t_emr_cardiovascularbaseinfo t, t_bse_employee d
 where t.createuserid = d.empno_chr
   and t.status = 0
   and inpatientid = ?
   and t.inpatientdate = ?
 order by recorddate";
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
				//生成DataTable
				DataTable dtbResult = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbResult,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbResult.Rows.Count >0)
				{
					p_objBaseInfoArr = new clsCardiovascularBaseInfo_GX[dtbResult.Rows.Count];
					clsCardiovascularBaseInfo_GX objRecordContent = null;
					for(int i=0; i<dtbResult.Rows.Count; i++)
					{
						objRecordContent = new clsCardiovascularBaseInfo_GX();
						objRecordContent.m_strInPatientID = dtbResult.Rows[i]["INPATIENTID"].ToString();
						objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(dtbResult.Rows[i]["INPATIENTDATE"]);
						objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbResult.Rows[i]["OPENDATE"]);
						objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbResult.Rows[i]["CREATEDATE"]);
						objRecordContent.m_strCreateUserID = dtbResult.Rows[i]["CREATEUSERID"].ToString();
						objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbResult.Rows[i]["MODIFYDATE"]);
						objRecordContent.m_strModifyUserID = dtbResult.Rows[i]["MODIFYUSERID"].ToString();
						objRecordContent.m_dblWEITHT = Convert.ToDouble(dtbResult.Rows[i]["WEIGHT"]);
						objRecordContent.m_strAFTEROPDAYS = dtbResult.Rows[i]["AFTEROPDAYS"].ToString();
						objRecordContent.m_strOPNAME = dtbResult.Rows[i]["OPNAME"].ToString();
						objRecordContent.m_strOPMEDICINE1 = dtbResult.Rows[i]["OPMEDICINE1"].ToString();
						objRecordContent.m_strOPMEDICINE2 = dtbResult.Rows[i]["OPMEDICINE2"].ToString();
						objRecordContent.m_strOPMEDICINE3 = dtbResult.Rows[i]["OPMEDICINE3"].ToString();
						objRecordContent.m_strOPMEDICINE4 = dtbResult.Rows[i]["OPMEDICINE4"].ToString();
						objRecordContent.m_strOPMEDICINE5 = dtbResult.Rows[i]["OPMEDICINE5"].ToString();
						objRecordContent.m_strLONGCLASSSIGNID = dtbResult.Rows[i]["LONGCLASSSIGNID"].ToString();
						objRecordContent.m_strOFFICESIGNID = dtbResult.Rows[i]["OFFICESIGNID"].ToString();
						objRecordContent.m_strSMALLNIGHTCLASSSIGNID = dtbResult.Rows[i]["SMALLNIGHTCLASSSIGNID"].ToString();
						objRecordContent.m_strBIGNIGHTCLASSSIGNID = dtbResult.Rows[i]["BIGNIGHTCLASSSIGNID"].ToString();
						objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbResult.Rows[i]["RECORDDATE"]);
						p_objBaseInfoArr[i] = objRecordContent;
					}
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
                //p_objHRPServ.Dispose();
            }
			return lngRes;
		}
	}
}
