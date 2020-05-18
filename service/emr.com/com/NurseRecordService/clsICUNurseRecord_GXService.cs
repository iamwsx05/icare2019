using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsICUNurseRecord_GXService
{
	/// <summary>
	/// ICU护理记录(广西)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsICUNurseRecord_GXService : clsDiseaseTrackService
	{
		public clsICUNurseRecord_GXService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region SQL语句
		/// <summary>
		/// 从ICUNURSERECORD_GXRECORD获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from icunurserecord_gxrecord
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// 从ICUNURSERECORD_GXRECORD中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from icunurserecord_gxrecord
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// 从ICUNURSERECORD_GXRECORD获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from icunurserecord_gxrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// 添加记录到ICUNURSERECORD_GXRECORD
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into icunurserecord_gxrecord (inpatientid,inpatientdate,opendate,
						createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,inamountitem,inamountitemxml,
						inamountstandby,inamountstandbyxml,inamountfact,inamountfactxml,outemiction,outemictionxml,temperature,
						temperaturexml,hr,hrxml,respiration,respirationxml,bloodpressures,bloodpressuresxml,bloodpressurea,
						bloodpressureaxml,a,axml,sp02,sp02xml,generalinstance,generalinstancexml,diseaseid,custom1,custom1xml,
						custom2,custom2xml,summary,summaryxml,isstat,sumin,sumout,sumintime,sumouttime,autosumin,autosumout,
                        startstattime,instandbysum,autoinstandbysum,infactsum,autoinfactsum,outemictionsum,autooutemictionsum,
                        outcustom1sum,autooutcustom1sum,outcustom2sum,autooutcustom2sum,custom1name,custom2name,custom3name,custom4name) 
						values (?,?,?,?,?,?,?,?,?,?,
								?,?,?,?,?,?,?,?,?,?,
								?,?,?,?,?,?,?,?,?,?,
								?,?,?,?,?,?,?,?,?,?,
								?,?,?,?,?,?,?,?,?,?,
                                ?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 添加记录到ICUNURSERECORD_GXCONTENT
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into icunurserecord_gxcontent (inpatientid,inpatientdate,
						opendate,modifydate,modifyuserid,temperature_right,hr_right,respiration_right,bloodpressures_right,
						bloodpressurea_right,a_right,sp02_right,generalinstance_right,inamountitem_right,inamountstandby_right,
						inamountfact_right,outemiction_right,custom1_right,custom2_right,summary_right)
						 values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到ICUNURSERECORD_GXRECORD
		/// </summary>
		private const string c_strModifyRecordSQL= @"update icunurserecord_gxrecord 
			set inamountitem=?,inamountitemxml=?,inamountstandby=?,inamountstandbyxml=?,inamountfact=?,inamountfactxml=?,outemiction=?,
			outemictionxml=?,temperature=?,temperaturexml=?,hr=?,hrxml=?,respiration=?,respirationxml=?,bloodpressures=?,bloodpressuresxml=?,
			bloodpressurea=?,bloodpressureaxml=?,a=?,axml=?,sp02=?,sp02xml=?,generalinstance=?,generalinstancexml=?,diseaseid=?,custom1=?,
			custom1xml=?,custom2=?,custom2xml=?,summary=?,summaryxml=?,isstat=?,sumin=?,sumout=?,sumintime=?,sumouttime=?,autosumin=?,
            autosumout=?,startstattime=?,instandbysum=?,autoinstandbysum=?,infactsum=?,autoinfactsum=?,outemictionsum=?,autooutemictionsum=?,
            outcustom1sum=?,autooutcustom1sum=?,outcustom2sum=?,autooutcustom2sum=?
			where inpatientid=? and inpatientdate=? and opendate=? and status=0";

		/// <summary>
		/// 修改记录到ICUNURSERECORD_GXCONTENT
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// 设置ICUNURSERECORD_GXRECORD中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update icunurserecord_gxrecord
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// 更新ICUNURSERECORD_GXRECORD中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update icunurserecord_gxrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// 从ICUNURSERECORD_GXRECORD获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from icunurserecord_gxrecord
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// 从ICUNURSERECORD_GXRECORD获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from icunurserecord_gxrecord
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
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
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL,ref dtbValue,objDPArr);
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
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

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
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
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @"  t1.inpatientid,
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
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_right,
       t2.hr_right,
       t2.respiration_right,
       t2.bloodpressures_right,
       t2.bloodpressurea_right,
       t2.a_right,
       t2.sp02_right,
       t2.generalinstance_right,
       t2.inamountitem_right,
       t2.inamountstandby_right,
       t2.inamountfact_right,
       t2.outemiction_right,
       t2.custom1_right,
       t2.custom2_right,
       t2.summary_right
  from icunurserecord_gxrecord t1
  join icunurserecord_gxcontent t2 on (t1.inpatientid = t2.inpatientid and
                                      t1.inpatientdate = t2.inpatientdate and
                                      t1.opendate = t2.opendate and
                                      t1.status = 0 and t1.inpatientid = ? and
                                      t1.inpatientdate = ? and
                                      t1.opendate = ?)
 order by t2.modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
				objDPArr[0].Value=p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
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
					clsICUNurseRecordContentGX objRecordContent=new clsICUNurseRecordContentGX();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

					objRecordContent.m_strTEMPERATURE_RIGHT=dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strTEMPERATURE=dtbValue.Rows[0]["TEMPERATURE"].ToString();
					objRecordContent.m_strTEMPERATUREXML=dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
					objRecordContent.m_strRESPIRATION_RIGHT=dtbValue.Rows[0]["RESPIRATION_RIGHT"].ToString();
					objRecordContent.m_strRESPIRATION=dtbValue.Rows[0]["RESPIRATION"].ToString();
					objRecordContent.m_strRESPIRATIONXML=dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
					objRecordContent.m_strHR_RIGHT=dtbValue.Rows[0]["HR_RIGHT"].ToString();
					objRecordContent.m_strHR=dtbValue.Rows[0]["HR"].ToString();
					objRecordContent.m_strHRXML=dtbValue.Rows[0]["HRXML"].ToString();
					objRecordContent.m_strBLOODPRESSURES_RIGHT=dtbValue.Rows[0]["BLOODPRESSURES_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSURES=dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
					objRecordContent.m_strBLOODPRESSURESXML=dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
					objRecordContent.m_strBLOODPRESSUREA_RIGHT=dtbValue.Rows[0]["BLOODPRESSUREA_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSUREA=dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
					objRecordContent.m_strBLOODPRESSUREAXML=dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
					objRecordContent.m_strA_RIGHT = dtbValue.Rows[0]["A_RIGHT"].ToString();
					objRecordContent.m_strA = dtbValue.Rows[0]["A"].ToString();
					objRecordContent.m_strAXML = dtbValue.Rows[0]["AXML"].ToString();
					objRecordContent.m_strSP02_RIGHT = dtbValue.Rows[0]["SP02_RIGHT"].ToString();
					objRecordContent.m_strSP02 = dtbValue.Rows[0]["SP02"].ToString();
					objRecordContent.m_strSP02XML = dtbValue.Rows[0]["SP02XML"].ToString();
					objRecordContent.m_strGENERALINSTANCE_RIGHT = dtbValue.Rows[0]["GENERALINSTANCE_RIGHT"].ToString();
					objRecordContent.m_strGENERALINSTANCE = dtbValue.Rows[0]["GENERALINSTANCE"].ToString();
					objRecordContent.m_strGENERALINSTANCEXML = dtbValue.Rows[0]["GENERALINSTANCEXML"].ToString();
					objRecordContent.m_strINAMOUNTITEM_RIGHT = dtbValue.Rows[0]["INAMOUNTITEM_RIGHT"].ToString();
					objRecordContent.m_strINAMOUNTITEM = dtbValue.Rows[0]["INAMOUNTITEM"].ToString();
					objRecordContent.m_strINAMOUNTITEMXML = dtbValue.Rows[0]["INAMOUNTITEMXML"].ToString();
					objRecordContent.m_strINAMOUNTSTANDBY_RIGHT = dtbValue.Rows[0]["INAMOUNTSTANDBY_RIGHT"].ToString();
					objRecordContent.m_strINAMOUNTSTANDBY = dtbValue.Rows[0]["INAMOUNTSTANDBY"].ToString();
					objRecordContent.m_strINAMOUNTSTANDBYXML = dtbValue.Rows[0]["INAMOUNTSTANDBYXML"].ToString();
					objRecordContent.m_strINAMOUNTFACT_RIGHT = dtbValue.Rows[0]["INAMOUNTFACT_RIGHT"].ToString();
					objRecordContent.m_strINAMOUNTFACT = dtbValue.Rows[0]["INAMOUNTFACT"].ToString();
					objRecordContent.m_strINAMOUNTFACTXML = dtbValue.Rows[0]["INAMOUNTFACTXML"].ToString();
					objRecordContent.m_strOUTEMICTION_RIGHT = dtbValue.Rows[0]["OUTEMICTION_RIGHT"].ToString();
					objRecordContent.m_strOUTEMICTION = dtbValue.Rows[0]["OUTEMICTION"].ToString();
					objRecordContent.m_strOUTEMICTIONXML = dtbValue.Rows[0]["OUTEMICTIONXML"].ToString();
					objRecordContent.m_strDISEASEID = dtbValue.Rows[0]["DISEASEID"].ToString();

					objRecordContent.m_strCustom1 = dtbValue.Rows[0]["CUSTOM1"].ToString();
					objRecordContent.m_strCustom1XML = dtbValue.Rows[0]["CUSTOM1XML"].ToString();
					objRecordContent.m_strCustom2 = dtbValue.Rows[0]["CUSTOM2"].ToString();
					objRecordContent.m_strCustom2XML = dtbValue.Rows[0]["CUSTOM2XML"].ToString();
					objRecordContent.m_strCustom1Name = dtbValue.Rows[0]["CUSTOM1NAME"].ToString();
					objRecordContent.m_strCustom2Name = dtbValue.Rows[0]["CUSTOM2NAME"].ToString();
					objRecordContent.m_strCustom3Name = dtbValue.Rows[0]["CUSTOM3NAME"].ToString();
					objRecordContent.m_strCustom4Name = dtbValue.Rows[0]["CUSTOM4NAME"].ToString();
					objRecordContent.m_strSummary = dtbValue.Rows[0]["SUMMARY"].ToString();
					objRecordContent.m_strSummaryXML = dtbValue.Rows[0]["SUMMARYXML"].ToString();
					objRecordContent.m_strCustom1_Right = dtbValue.Rows[0]["CUSTOM1_RIGHT"].ToString();
					objRecordContent.m_strCustom2_Right = dtbValue.Rows[0]["CUSTOM2_RIGHT"].ToString();
					objRecordContent.m_strSummary_Right = dtbValue.Rows[0]["SUMMARY_RIGHT"].ToString();

					objRecordContent.m_intISSTAT = dtbValue.Rows[0]["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[0]["ISSTAT"]);
					objRecordContent.m_intSUMINTIME = dtbValue.Rows[0]["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[0]["SUMINTIME"]);
					objRecordContent.m_intSUMOUTTIME = dtbValue.Rows[0]["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[0]["SUMOUTTIME"]);
					objRecordContent.m_strAUTOSUMIN = dtbValue.Rows[0]["AUTOSUMIN"].ToString();
					objRecordContent.m_strAUTOSUMOUT = dtbValue.Rows[0]["AUTOSUMOUT"].ToString();
					objRecordContent.m_strSUMIN = dtbValue.Rows[0]["SUMIN"].ToString();
					objRecordContent.m_strSUMOUT = dtbValue.Rows[0]["SUMOUT"].ToString();
					objRecordContent.m_dtmSTARTSTATTIME = dtbValue.Rows[0]["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(dtbValue.Rows[0]["STARTSTATTIME"]);
                    objRecordContent.m_strINSTANDBYSUM = dtbValue.Rows[0]["INSTANDBYSUM"].ToString();
                    objRecordContent.m_strAUTOINSTANDBYSUM = dtbValue.Rows[0]["AUTOINSTANDBYSUM"].ToString();
                    objRecordContent.m_strINFACTSUM = dtbValue.Rows[0]["INFACTSUM"].ToString();
                    objRecordContent.m_strAUTOINFACTSUM = dtbValue.Rows[0]["AUTOINFACTSUM"].ToString();
                    objRecordContent.m_strOUTEMICTIONSUM = dtbValue.Rows[0]["OUTEMICTIONSUM"].ToString();
                    objRecordContent.m_strAUTOOUTEMICTIONSUM = dtbValue.Rows[0]["AUTOOUTEMICTIONSUM"].ToString();
                    objRecordContent.m_strOUTCUSTOM1SUM = dtbValue.Rows[0]["OUTCUSTOM1SUM"].ToString();
                    objRecordContent.m_strAUTOOUTCUSTOM1SUM = dtbValue.Rows[0]["AUTOOUTCUSTOM1SUM"].ToString();
                    objRecordContent.m_strOUTCUSTOM2SUM = dtbValue.Rows[0]["OUTCUSTOM2SUM"].ToString();
                    objRecordContent.m_strAUTOOUTCUSTOM2SUM = dtbValue.Rows[0]["AUTOOUTCUSTOM2SUM"].ToString();

					p_objRecordContent=	objRecordContent;
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
			clsICUNurseRecordContentGX objRecordContent = (clsICUNurseRecordContentGX)p_objRecordContent;
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(62,out objDPArr);
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

				objDPArr[9].Value=objRecordContent.m_strINAMOUNTITEM;
				objDPArr[10].Value=objRecordContent.m_strINAMOUNTITEMXML;
				objDPArr[11].Value=objRecordContent.m_strINAMOUNTSTANDBY;
				objDPArr[12].Value=objRecordContent.m_strINAMOUNTSTANDBYXML;
				objDPArr[13].Value=objRecordContent.m_strINAMOUNTFACT;
				objDPArr[14].Value=objRecordContent.m_strINAMOUNTFACTXML;
				objDPArr[15].Value=objRecordContent.m_strOUTEMICTION;
				objDPArr[16].Value=objRecordContent.m_strOUTEMICTIONXML;
				objDPArr[17].Value=objRecordContent.m_strTEMPERATURE;
				objDPArr[18].Value=objRecordContent.m_strTEMPERATUREXML;
				objDPArr[19].Value=objRecordContent.m_strHR;
				objDPArr[20].Value=objRecordContent.m_strHRXML;
				objDPArr[21].Value=objRecordContent.m_strRESPIRATION;
				objDPArr[22].Value=objRecordContent.m_strRESPIRATIONXML;
				objDPArr[23].Value=objRecordContent.m_strBLOODPRESSURES;
				objDPArr[24].Value=objRecordContent.m_strBLOODPRESSURESXML;
				objDPArr[25].Value=objRecordContent.m_strBLOODPRESSUREA;
				objDPArr[26].Value=objRecordContent.m_strBLOODPRESSUREAXML;
				objDPArr[27].Value=objRecordContent.m_strA;
				objDPArr[28].Value=objRecordContent.m_strAXML;
				objDPArr[29].Value=objRecordContent.m_strSP02;
				objDPArr[30].Value=objRecordContent.m_strSP02XML;
				objDPArr[31].Value=objRecordContent.m_strGENERALINSTANCE;
				objDPArr[32].Value=objRecordContent.m_strGENERALINSTANCEXML;
				objDPArr[33].Value=objRecordContent.m_strDISEASEID;
				objDPArr[34].Value = objRecordContent.m_strCustom1;
				objDPArr[35].Value = objRecordContent.m_strCustom1XML;
				objDPArr[36].Value = objRecordContent.m_strCustom2;
				objDPArr[37].Value = objRecordContent.m_strCustom2XML;
				objDPArr[38].Value = objRecordContent.m_strSummary;
				objDPArr[39].Value = objRecordContent.m_strSummaryXML;
				objDPArr[40].Value = objRecordContent.m_intISSTAT;
				objDPArr[41].Value = objRecordContent.m_strSUMIN;
				objDPArr[42].Value = objRecordContent.m_strSUMOUT;
				objDPArr[43].Value = objRecordContent.m_intSUMINTIME;
				objDPArr[44].Value = objRecordContent.m_intSUMOUTTIME;
				objDPArr[45].Value = objRecordContent.m_strAUTOSUMIN;
				objDPArr[46].Value = objRecordContent.m_strAUTOSUMOUT;
                objDPArr[47].DbType = DbType.DateTime;
                objDPArr[47].Value = objRecordContent.m_dtmSTARTSTATTIME;
                objDPArr[48].Value = objRecordContent.m_strINSTANDBYSUM;
                objDPArr[49].Value = objRecordContent.m_strAUTOINSTANDBYSUM;
                objDPArr[50].Value = objRecordContent.m_strINFACTSUM;
                objDPArr[51].Value = objRecordContent.m_strAUTOINFACTSUM;
                objDPArr[52].Value = objRecordContent.m_strOUTEMICTIONSUM;
                objDPArr[53].Value = objRecordContent.m_strAUTOOUTEMICTIONSUM;
                objDPArr[54].Value = objRecordContent.m_strOUTCUSTOM1SUM;
                objDPArr[55].Value = objRecordContent.m_strAUTOOUTCUSTOM1SUM;
                objDPArr[56].Value = objRecordContent.m_strOUTCUSTOM2SUM;
                objDPArr[57].Value = objRecordContent.m_strAUTOOUTCUSTOM2SUM;
                objDPArr[58].Value = objRecordContent.m_strCustom1Name;
                objDPArr[59].Value = objRecordContent.m_strCustom2Name;
                objDPArr[60].Value = objRecordContent.m_strCustom3Name;
                objDPArr[61].Value = objRecordContent.m_strCustom4Name;

				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;
			
				IDataParameter[] objDPArr2 = null;
				p_objHRPServ.CreateDatabaseParameter(20,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;	
			
				objDPArr2[5].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strHR_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strRESPIRATION_RIGHT;
				objDPArr2[8].Value = objRecordContent.m_strBLOODPRESSURES_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strBLOODPRESSUREA_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strA_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strSP02_RIGHT;
				objDPArr2[12].Value = objRecordContent.m_strGENERALINSTANCE_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strINAMOUNTITEM_RIGHT;
				objDPArr2[14].Value = objRecordContent.m_strINAMOUNTSTANDBY_RIGHT;
				objDPArr2[15].Value = objRecordContent.m_strINAMOUNTFACT_RIGHT;
				objDPArr2[16].Value = objRecordContent.m_strOUTEMICTION_RIGHT;
				objDPArr2[17].Value = objRecordContent.m_strCustom1_Right;
				objDPArr2[18].Value = objRecordContent.m_strCustom2_Right;
				objDPArr2[19].Value = objRecordContent.m_strSummary_Right;
			
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
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			clsICUNurseRecordContentGX objRecordContent = (clsICUNurseRecordContentGX)p_objRecordContent;
			/// <summary>
			/// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from icunurserecord_gxrecord t1,icunurserecord_gxcontent t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc "+clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
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
		
			clsICUNurseRecordContentGX objRecordContent = (clsICUNurseRecordContentGX)p_objRecordContent;
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(52,out objDPArr);
				objDPArr[0].Value=objRecordContent.m_strINAMOUNTITEM;
				objDPArr[1].Value=objRecordContent.m_strINAMOUNTITEMXML;
				objDPArr[2].Value=objRecordContent.m_strINAMOUNTSTANDBY;
				objDPArr[3].Value=objRecordContent.m_strINAMOUNTSTANDBYXML;
				objDPArr[4].Value=objRecordContent.m_strINAMOUNTFACT;
				objDPArr[5].Value=objRecordContent.m_strINAMOUNTFACTXML;
				objDPArr[6].Value=objRecordContent.m_strOUTEMICTION;
				objDPArr[7].Value=objRecordContent.m_strOUTEMICTIONXML;
				objDPArr[8].Value=objRecordContent.m_strTEMPERATURE;
				objDPArr[9].Value=objRecordContent.m_strTEMPERATUREXML;
				objDPArr[10].Value=objRecordContent.m_strHR;
				objDPArr[11].Value=objRecordContent.m_strHRXML;
				objDPArr[12].Value=objRecordContent.m_strRESPIRATION;
				objDPArr[13].Value=objRecordContent.m_strRESPIRATIONXML;
				objDPArr[14].Value=objRecordContent.m_strBLOODPRESSURES;
				objDPArr[15].Value=objRecordContent.m_strBLOODPRESSURESXML;
				objDPArr[16].Value=objRecordContent.m_strBLOODPRESSUREA;
				objDPArr[17].Value=objRecordContent.m_strBLOODPRESSUREAXML;
				objDPArr[18].Value=objRecordContent.m_strA;
				objDPArr[19].Value=objRecordContent.m_strAXML;
				objDPArr[20].Value=objRecordContent.m_strSP02;
				objDPArr[21].Value=objRecordContent.m_strSP02XML;
				objDPArr[22].Value=objRecordContent.m_strGENERALINSTANCE;
				objDPArr[23].Value=objRecordContent.m_strGENERALINSTANCEXML;
				objDPArr[24].Value=objRecordContent.m_strDISEASEID;
				objDPArr[25].Value = objRecordContent.m_strCustom1;
				objDPArr[26].Value = objRecordContent.m_strCustom1XML;
				objDPArr[27].Value = objRecordContent.m_strCustom2;
				objDPArr[28].Value = objRecordContent.m_strCustom2XML;
				objDPArr[29].Value = objRecordContent.m_strSummary;
				objDPArr[30].Value = objRecordContent.m_strSummaryXML;
				objDPArr[31].Value = objRecordContent.m_intISSTAT;
				objDPArr[32].Value = objRecordContent.m_strSUMIN;
				objDPArr[33].Value = objRecordContent.m_strSUMOUT;
				objDPArr[34].Value = objRecordContent.m_intSUMINTIME;
				objDPArr[35].Value = objRecordContent.m_intSUMOUTTIME;
				objDPArr[36].Value = objRecordContent.m_strAUTOSUMIN;
				objDPArr[37].Value = objRecordContent.m_strAUTOSUMOUT;
                objDPArr[38].DbType = DbType.DateTime;
                objDPArr[38].Value = objRecordContent.m_dtmSTARTSTATTIME;
                objDPArr[39].Value = objRecordContent.m_strINSTANDBYSUM;
                objDPArr[40].Value = objRecordContent.m_strAUTOINSTANDBYSUM;
                objDPArr[41].Value = objRecordContent.m_strINFACTSUM;
                objDPArr[42].Value = objRecordContent.m_strAUTOINFACTSUM;
                objDPArr[43].Value = objRecordContent.m_strOUTEMICTIONSUM;
                objDPArr[44].Value = objRecordContent.m_strAUTOOUTEMICTIONSUM;
                objDPArr[45].Value = objRecordContent.m_strOUTCUSTOM1SUM;
                objDPArr[46].Value = objRecordContent.m_strAUTOOUTCUSTOM1SUM;
                objDPArr[47].Value = objRecordContent.m_strOUTCUSTOM2SUM;
                objDPArr[48].Value = objRecordContent.m_strAUTOOUTCUSTOM2SUM;

                objDPArr[49].Value = objRecordContent.m_strInPatientID;
                objDPArr[50].DbType = DbType.DateTime;
                objDPArr[50].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[51].DbType = DbType.DateTime;
				objDPArr[51].Value=objRecordContent.m_dtmOpenDate;
		
				//执行SQL
				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
				if(lngRes<=0)return lngRes;


				IDataParameter[] objDPArr2 = null; 
				p_objHRPServ.CreateDatabaseParameter(20,out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
				objDPArr2[3].Value=objRecordContent.m_dtmModifyDate;
				objDPArr2[4].Value=objRecordContent.m_strModifyUserID;		
			
				objDPArr2[5].Value = objRecordContent.m_strTEMPERATURE_RIGHT;
				objDPArr2[6].Value = objRecordContent.m_strHR_RIGHT;
				objDPArr2[7].Value = objRecordContent.m_strRESPIRATION_RIGHT;
				objDPArr2[8].Value = objRecordContent.m_strBLOODPRESSURES_RIGHT;
				objDPArr2[9].Value = objRecordContent.m_strBLOODPRESSUREA_RIGHT;
				objDPArr2[10].Value = objRecordContent.m_strA_RIGHT;
				objDPArr2[11].Value = objRecordContent.m_strSP02_RIGHT;
				objDPArr2[12].Value = objRecordContent.m_strGENERALINSTANCE_RIGHT;
				objDPArr2[13].Value = objRecordContent.m_strINAMOUNTITEM_RIGHT;
				objDPArr2[14].Value = objRecordContent.m_strINAMOUNTSTANDBY_RIGHT;
				objDPArr2[15].Value = objRecordContent.m_strINAMOUNTFACT_RIGHT;
				objDPArr2[16].Value = objRecordContent.m_strOUTEMICTION_RIGHT;
				objDPArr2[17].Value = objRecordContent.m_strCustom1_Right;
				objDPArr2[18].Value = objRecordContent.m_strCustom2_Right;
				objDPArr2[19].Value = objRecordContent.m_strSummary_Right;
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

			clsICUNurseRecordContentGX objRecordContent = (clsICUNurseRecordContentGX)p_objRecordContent;
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
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
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			/// <summary>
			/// 从ICUNURSERECORD_GXRECORD和ICUNURSERECORD_GXCONTENT获取LastModifyDate和FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+ @" a.firstprintdate,b.modifydate from icunurserecord_gxrecord a,
					icunurserecord_gxcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;


			long lngRes = 0;
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
       t2.modifydate,
       t2.modifyuserid,
       t2.temperature_right,
       t2.hr_right,
       t2.respiration_right,
       t2.bloodpressures_right,
       t2.bloodpressurea_right,
       t2.a_right,
       t2.sp02_right,
       t2.generalinstance_right,
       t2.inamountitem_right,
       t2.inamountstandby_right,
       t2.inamountfact_right,
       t2.outemiction_right,
       t2.custom1_right,
       t2.custom2_right,
       t2.summary_right,
       f_getempnamebyno(t1.createuserid) as createusername
  from icunurserecord_gxrecord t1, icunurserecord_gxcontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
   and t2.modifydate = (select max(modifydate)
                          from icunurserecord_gxcontent
                         where inpatientid = t2.inpatientid
                           and inpatientdate = t2.inpatientdate
                           and opendate = t2.opendate)";
		
			long lngRes = 0;
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
					clsICUNurseRecordContentGX objRecordContent=new clsICUNurseRecordContentGX();
					objRecordContent.m_strInPatientID=p_strInPatientID;
					objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmOpenDate=DateTime.Parse(p_strOpenDate);
					objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
					objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
				
					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
					objRecordContent.m_strCreateUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strModifyUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_strCreateUserName = dtbValue.Rows[0]["CreateUserName"].ToString();
					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());
					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();

					objRecordContent.m_strTEMPERATURE_RIGHT=dtbValue.Rows[0]["TEMPERATURE_RIGHT"].ToString();
					objRecordContent.m_strTEMPERATURE=dtbValue.Rows[0]["TEMPERATURE"].ToString();
					objRecordContent.m_strTEMPERATUREXML=dtbValue.Rows[0]["TEMPERATUREXML"].ToString();
					objRecordContent.m_strRESPIRATION_RIGHT=dtbValue.Rows[0]["RESPIRATION_RIGHT"].ToString();
					objRecordContent.m_strRESPIRATION=dtbValue.Rows[0]["RESPIRATION"].ToString();
					objRecordContent.m_strRESPIRATIONXML=dtbValue.Rows[0]["RESPIRATIONXML"].ToString();
					objRecordContent.m_strHR_RIGHT=dtbValue.Rows[0]["HR_RIGHT"].ToString();
					objRecordContent.m_strHR=dtbValue.Rows[0]["HR"].ToString();
					objRecordContent.m_strHRXML=dtbValue.Rows[0]["HRXML"].ToString();
					objRecordContent.m_strBLOODPRESSURES_RIGHT=dtbValue.Rows[0]["BLOODPRESSURES_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSURES=dtbValue.Rows[0]["BLOODPRESSURES"].ToString();
					objRecordContent.m_strBLOODPRESSURESXML=dtbValue.Rows[0]["BLOODPRESSURESXML"].ToString();
					objRecordContent.m_strBLOODPRESSUREA_RIGHT=dtbValue.Rows[0]["BLOODPRESSUREA_RIGHT"].ToString();
					objRecordContent.m_strBLOODPRESSUREA=dtbValue.Rows[0]["BLOODPRESSUREA"].ToString();
					objRecordContent.m_strBLOODPRESSUREAXML=dtbValue.Rows[0]["BLOODPRESSUREAXML"].ToString();
					objRecordContent.m_strA_RIGHT = dtbValue.Rows[0]["A_RIGHT"].ToString();
					objRecordContent.m_strA = dtbValue.Rows[0]["A"].ToString();
					objRecordContent.m_strAXML = dtbValue.Rows[0]["AXML"].ToString();
					objRecordContent.m_strSP02_RIGHT = dtbValue.Rows[0]["SP02_RIGHT"].ToString();
					objRecordContent.m_strSP02 = dtbValue.Rows[0]["SP02"].ToString();
					objRecordContent.m_strSP02XML = dtbValue.Rows[0]["SP02XML"].ToString();
					objRecordContent.m_strGENERALINSTANCE_RIGHT = dtbValue.Rows[0]["GENERALINSTANCE_RIGHT"].ToString();
					objRecordContent.m_strGENERALINSTANCE = dtbValue.Rows[0]["GENERALINSTANCE"].ToString();
					objRecordContent.m_strGENERALINSTANCEXML = dtbValue.Rows[0]["GENERALINSTANCEXML"].ToString();
					objRecordContent.m_strINAMOUNTITEM_RIGHT = dtbValue.Rows[0]["INAMOUNTITEM_RIGHT"].ToString();
					objRecordContent.m_strINAMOUNTITEM = dtbValue.Rows[0]["INAMOUNTITEM"].ToString();
					objRecordContent.m_strINAMOUNTITEMXML = dtbValue.Rows[0]["INAMOUNTITEMXML"].ToString();
					objRecordContent.m_strINAMOUNTSTANDBY_RIGHT = dtbValue.Rows[0]["INAMOUNTSTANDBY_RIGHT"].ToString();
					objRecordContent.m_strINAMOUNTSTANDBY = dtbValue.Rows[0]["INAMOUNTSTANDBY"].ToString();
					objRecordContent.m_strINAMOUNTSTANDBYXML = dtbValue.Rows[0]["INAMOUNTSTANDBYXML"].ToString();
					objRecordContent.m_strINAMOUNTFACT_RIGHT = dtbValue.Rows[0]["INAMOUNTFACT_RIGHT"].ToString();
					objRecordContent.m_strINAMOUNTFACT = dtbValue.Rows[0]["INAMOUNTFACT"].ToString();
					objRecordContent.m_strINAMOUNTFACTXML = dtbValue.Rows[0]["INAMOUNTFACTXML"].ToString();
					objRecordContent.m_strOUTEMICTION_RIGHT = dtbValue.Rows[0]["OUTEMICTION_RIGHT"].ToString();
					objRecordContent.m_strOUTEMICTION = dtbValue.Rows[0]["OUTEMICTION"].ToString();
					objRecordContent.m_strOUTEMICTIONXML = dtbValue.Rows[0]["OUTEMICTIONXML"].ToString();
					objRecordContent.m_strDISEASEID = dtbValue.Rows[0]["DISEASEID"].ToString();

					objRecordContent.m_strCustom1 = dtbValue.Rows[0]["CUSTOM1"].ToString();
					objRecordContent.m_strCustom1XML = dtbValue.Rows[0]["CUSTOM1XML"].ToString();
					objRecordContent.m_strCustom2 = dtbValue.Rows[0]["CUSTOM2"].ToString();
					objRecordContent.m_strCustom2XML = dtbValue.Rows[0]["CUSTOM2XML"].ToString();
					objRecordContent.m_strCustom1Name = dtbValue.Rows[0]["CUSTOM1NAME"].ToString();
					objRecordContent.m_strCustom2Name = dtbValue.Rows[0]["CUSTOM2NAME"].ToString();
					objRecordContent.m_strCustom3Name = dtbValue.Rows[0]["CUSTOM3NAME"].ToString();
					objRecordContent.m_strCustom4Name = dtbValue.Rows[0]["CUSTOM4NAME"].ToString();
					objRecordContent.m_strSummary = dtbValue.Rows[0]["SUMMARY"].ToString();
					objRecordContent.m_strSummaryXML = dtbValue.Rows[0]["SUMMARYXML"].ToString();
					objRecordContent.m_strCustom1_Right = dtbValue.Rows[0]["CUSTOM1_RIGHT"].ToString();
					objRecordContent.m_strCustom2_Right = dtbValue.Rows[0]["CUSTOM2_RIGHT"].ToString();
					objRecordContent.m_strSummary_Right = dtbValue.Rows[0]["SUMMARY_RIGHT"].ToString();

					objRecordContent.m_intISSTAT = dtbValue.Rows[0]["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[0]["ISSTAT"]);
					objRecordContent.m_intSUMINTIME = dtbValue.Rows[0]["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[0]["SUMINTIME"]);
					objRecordContent.m_intSUMOUTTIME = dtbValue.Rows[0]["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(dtbValue.Rows[0]["SUMOUTTIME"]);
					objRecordContent.m_strAUTOSUMIN = dtbValue.Rows[0]["AUTOSUMIN"].ToString();
					objRecordContent.m_strAUTOSUMOUT = dtbValue.Rows[0]["AUTOSUMOUT"].ToString();
					objRecordContent.m_strSUMIN = dtbValue.Rows[0]["SUMIN"].ToString();
					objRecordContent.m_strSUMOUT = dtbValue.Rows[0]["SUMOUT"].ToString();
					objRecordContent.m_dtmSTARTSTATTIME = dtbValue.Rows[0]["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(dtbValue.Rows[0]["STARTSTATTIME"]);
                    objRecordContent.m_strINSTANDBYSUM = dtbValue.Rows[0]["INSTANDBYSUM"].ToString();
                    objRecordContent.m_strAUTOINSTANDBYSUM = dtbValue.Rows[0]["AUTOINSTANDBYSUM"].ToString();
                    objRecordContent.m_strINFACTSUM = dtbValue.Rows[0]["INFACTSUM"].ToString();
                    objRecordContent.m_strAUTOINFACTSUM = dtbValue.Rows[0]["AUTOINFACTSUM"].ToString();
                    objRecordContent.m_strOUTEMICTIONSUM = dtbValue.Rows[0]["OUTEMICTIONSUM"].ToString();
                    objRecordContent.m_strAUTOOUTEMICTIONSUM = dtbValue.Rows[0]["AUTOOUTEMICTIONSUM"].ToString();
                    objRecordContent.m_strOUTCUSTOM1SUM = dtbValue.Rows[0]["OUTCUSTOM1SUM"].ToString();
                    objRecordContent.m_strAUTOOUTCUSTOM1SUM = dtbValue.Rows[0]["AUTOOUTCUSTOM1SUM"].ToString();
                    objRecordContent.m_strOUTCUSTOM2SUM = dtbValue.Rows[0]["OUTCUSTOM2SUM"].ToString();
                    objRecordContent.m_strAUTOOUTCUSTOM2SUM = dtbValue.Rows[0]["AUTOOUTCUSTOM2SUM"].ToString();
					p_objRecordContent=	objRecordContent;	
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
		/// 设置自定义列名
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strColumnIndex"></param>
		/// <param name="p_strColumnName"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSetCustomColumnName(string p_strInPatientID, 
			string p_strInPatientDate, 
			string p_strColumnIndex,
			string p_strColumnName)
		{
			long lngRes = 0;
			clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"update icunurserecord_gxrecord set "+p_strColumnIndex+@"=? 
								where inpatientid=? and  inpatientdate=?";
				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
				objDPArr[0].Value=p_strColumnName;
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=DateTime.Parse(p_strInPatientDate);

				long lngEff=0;
				lngRes =  p_objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{	
				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    //p_objHRPServ.Dispose();
                }
			return lngRes;
		}

		/// <summary>
		/// 获取一段时间内的所有入量
		/// </summary>
		/// <param name="p_strEndTime">结束时间</param>
		/// <param name="p_strStartTime">开始时间</param>
		/// <param name="p_strStandbyArr">以数组形式存储备用量</param>
		/// <param name="p_dblFactArr">以数组形式存储实入量</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetInSum(string p_strInPatientID, 
			string p_strInPatientDate, 
			string p_strEndTime, 
			string p_strStartTime, 
			out string[] p_strStandbyArr,
			out double[] p_dblFactArr)
		{
			long lngRes = 0;
			p_strStandbyArr = null;
			p_dblFactArr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select inamountstandby,inamountfact from icunurserecord_gxrecord where inpatientid = ?
									and inpatientdate = ?
									and createdate between ? and ? and status=0";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strStartTime);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strEndTime);

				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count > 0 )
				{	
					p_strStandbyArr = new string[dtbValue.Rows.Count];
					p_dblFactArr = new double[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
						p_strStandbyArr[i] = dtbValue.Rows[i]["INAMOUNTSTANDBY"]==DBNull.Value?"":dtbValue.Rows[i]["INAMOUNTSTANDBY"].ToString();
						p_dblFactArr[i] = dtbValue.Rows[i]["INAMOUNTFACT"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["INAMOUNTFACT"]);
					}
				}
			}
			catch(Exception objEx)
			{
		

				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    //p_objHRPServ.Dispose();
                }
			return lngRes;
		}

		/// <summary>
		/// 获取一段时间内所有出量
		/// </summary>
		/// <param name="p_strEndTime">结束时间</param>
		/// <param name="p_strStartTime">开始时间</param>
		/// <param name="p_dblOutPissArr">以数组形式存储出量>>小便</param>
		/// <param name="p_dblCustom1Arr">以数组形式存储出量>>用户自定义列1</param>
		/// <param name="p_dblCustom2Arr">以数组形式存储出量>>用户自定义列2</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetOutSum(string p_strInPatientID, 
			string p_strInPatientDate, 
			string p_strEndTime,
			string p_strStartTime, 
			out double[] p_dblOutPissArr,
			out double[] p_dblCustom1Arr,
			out double[] p_dblCustom2Arr)
		{
			long lngRes = 0;
			p_dblOutPissArr = null;
			p_dblCustom1Arr = null;
			p_dblCustom2Arr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select outemiction,custom1,custom2 from icunurserecord_gxrecord where inpatientid = ?
									and inpatientdate = ?
									and createdate between ? and ? and status=0";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strStartTime);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=DateTime.Parse(p_strEndTime);

				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 )
				{
					p_dblOutPissArr = new double[dtbValue.Rows.Count];
					p_dblCustom1Arr = new double[dtbValue.Rows.Count];
					p_dblCustom2Arr = new double[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
						p_dblOutPissArr[i] = dtbValue.Rows[i]["OUTEMICTION"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["OUTEMICTION"]);
						p_dblCustom1Arr[i] = dtbValue.Rows[i]["CUSTOM1"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["CUSTOM1"]);
						p_dblCustom2Arr[i] = dtbValue.Rows[i]["CUSTOM2"]==DBNull.Value?0:Convert.ToDouble(dtbValue.Rows[i]["CUSTOM2"]);
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;
		}


		/// <summary>
		/// 获取标记为需统计的记录时间
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_intStatStatus">统计标志</param>
		/// <param name="p_dtmStatTimeArr">记录时间</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetStatRecordTime(string p_strInPatientID, 
			string p_strInPatientDate, 
			out clsICUNurseRecordContentGX[] p_objRecordArr)
		{
			long lngRes = 0;
			p_objRecordArr = null;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
                string strSQL = @"select createdate,sumintime,sumouttime,autosumin,autosumout,sumin,sumout,
                                instandbysum,autoinstandbysum,infactsum,autoinfactsum,outemictionsum,autooutemictionsum,
                                outcustom1sum,autooutcustom1sum,outcustom2sum,autooutcustom2sum
								 from icunurserecord_gxrecord 
									where inpatientid = ?
									and inpatientdate = ?
									and isstat = 1 and status=0 order by createdate";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);

				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
					p_objRecordArr = new clsICUNurseRecordContentGX[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
						p_objRecordArr[i] = new clsICUNurseRecordContentGX();
						p_objRecordArr[i].m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
						p_objRecordArr[i].m_intISSTAT = 1;
						p_objRecordArr[i].m_intSUMINTIME = Convert.ToInt32(dtbValue.Rows[i]["SUMINTIME"]);
						p_objRecordArr[i].m_intSUMOUTTIME = Convert.ToInt32(dtbValue.Rows[i]["SUMOUTTIME"]);
						p_objRecordArr[i].m_strAUTOSUMIN = dtbValue.Rows[i]["AUTOSUMIN"].ToString();
						p_objRecordArr[i].m_strAUTOSUMOUT = dtbValue.Rows[i]["AUTOSUMOUT"].ToString();
						p_objRecordArr[i].m_strSUMIN = dtbValue.Rows[i]["SUMIN"].ToString();
						p_objRecordArr[i].m_strSUMOUT = dtbValue.Rows[i]["SUMOUT"].ToString();
                        p_objRecordArr[i].m_strINSTANDBYSUM = dtbValue.Rows[i]["INSTANDBYSUM"].ToString();
                        p_objRecordArr[i].m_strINFACTSUM = dtbValue.Rows[i]["INFACTSUM"].ToString();
                        p_objRecordArr[i].m_strOUTEMICTIONSUM = dtbValue.Rows[i]["OUTEMICTIONSUM"].ToString();
                        p_objRecordArr[i].m_strOUTCUSTOM1SUM = dtbValue.Rows[i]["OUTCUSTOM1SUM"].ToString();
                        p_objRecordArr[i].m_strOUTCUSTOM2SUM = dtbValue.Rows[i]["OUTCUSTOM2SUM"].ToString();
					}
				}
			}
			catch(Exception objEx)
			{
		

				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 获取病人首次记录的时间
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_dtmRecordDate">记录时间</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMinRecordDate(string p_strInPatientID, 
			string p_strInPatientDate, 
			out DateTime p_dtmRecordDate)
		{
			long lngRes = 0;
			p_dtmRecordDate = DateTime.MinValue;
				clsHRPTableService p_objHRPServ=new clsHRPTableService();
			try
			{
				string strSQL = @"select min(t.createdate) as recorddate
								from icunurserecord_gxrecord t
								where inpatientid = ?
								and t.inpatientdate = ?
								and t.status = 0";

				IDataParameter[] objDPArr = null; 
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);

				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
				//从DataTable.Rows中获取结果
				if(lngRes > 0 && dtbValue.Rows.Count == 1)
				{
					
					p_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["recorddate"]);
				}
			}
			catch(Exception objEx)
			{
		

				string strTmp=objEx.Message;
				clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
			return lngRes;
		}
		#region update first print time
		/// <summary>
		/// update first print time
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
		//[AutoComplete]
		//public  long m_lngUpdateFirstPrintDate(
		//	string p_strInPatientID,
		//	string p_strInPatientDate,
		//	string p_strOpenDate,
		//	DateTime p_dtmFirstPrintDate)
		//{

		//	long lngRes = 0;
		//	clsHRPTableService objHRPServ =new clsHRPTableService();
		//	try
		//	{
		//		//检查参数                              
		//		if(p_strInPatientID==null || p_strInPatientID==""||p_strInPatientDate==null || p_strInPatientDate==""||p_strOpenDate==null || p_strOpenDate=="")
		//			return (long)enmOperationResult.Parameter_Error;			
			
		//		//获取IDataParameter数组
		//		IDataParameter[] objDPArr = null;
  //              objHRPServ.CreateDatabaseParameter(4, out objDPArr);
  //              objDPArr[0].DbType = DbType.DateTime;
		//		objDPArr[0].Value=p_dtmFirstPrintDate;
  //              objDPArr[1].Value = p_strInPatientID;
  //              objDPArr[2].DbType = DbType.DateTime;
  //              objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
  //              objDPArr[3].DbType = DbType.DateTime;
		//		objDPArr[3].Value=DateTime.Parse(p_strOpenDate);
			
		//		//执行SQL
		//		long lngEff=0;
  //              lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
				
		//	}
		//	catch(Exception objEx)
		//	{
		//		string strTmp=objEx.Message;
		//		com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
		//		bool blnRes = objLogger.LogError(objEx);
		//	}
  //              finally
  //              {

  //                  //objHRPServ.Dispose();
  //              }
		//	return lngRes;		
		//}
		#endregion
	}
}
