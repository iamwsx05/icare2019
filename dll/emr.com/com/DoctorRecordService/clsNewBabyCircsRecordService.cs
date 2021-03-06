using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.InPatientCaseHistoryServ
{
	/// <summary>
	/// 新生儿入室记录 - 新生儿情况记录
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsNewBabyCircsRecordService : clsDiseaseTrackService
	{
		public clsNewBabyCircsRecordService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region SQL语句
		/// <summary>
		/// 从NEWBABYCIRCSRECORD获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from newbabycircsrecord
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// 从NEWBABYCIRCSRECORD中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from newbabycircsrecord
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// 从NEWBABYCIRCSRECORD获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from newbabycircsrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// 添加记录到NEWBABYCIRCSRECORD
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into newbabycircsrecord (inpatientid,inpatientdate,birthtime,birthdays,
		birthburl,haematoma,fontanel,conjunctiva,secretion,pharynx,whitepoint,icterus,fester,bleeding,agnail,redstern,sternskin,
		heartlung,abdomen,remark,remarkxml,birthburlxml,haematomaxml,fontanelxml,conjunctivaxml,secretionxml,pharynxxml,whitepointxml,
		icterusxml,festerxml,bleedingxml,agnailxml,redsternxml,sternskinxml,heartlungxml,abdomenxml,modifydate,modifyuserid,status,opendate,
		createuserid,createdate,signuserid,signusername,recorddate) 
						values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到NEWBABYCIRCSRECORD
		/// </summary>
//		private const string c_strModifyRecordSQL= @"Update NEWBABYCIRCSRECORD 
//			Set BIRTHTIME=?,BIRTHDAYS=?,BIRTHBURL=?,HAEMATOMA=?,FONTANEL=?,CONJUNCTIVA=?,SECRETION=?,PHARYNX=?,WHITEPOINT=?,ICTERUS=?,FESTER=?,
//		BLEEDING=?,AGNAIL=?,REDSTERN=?,STERNSKIN=?,HEARTLUNG=?,ABDOMEN=?,REMARK=?,REMARKXML=?,BIRTHBURLXML=?,HAEMATOMAXML=?,FONTANELXML=?,
//		CONJUNCTIVAXML=?,SECRETIONXML=?,PHARYNXXML=?,WHITEPOINTXML=?,ICTERUSXML=?,FESTERXML=?,BLEEDINGXML=?,AGNAILXML=?,REDSTERNXML=?,
//		STERNSKINXML=?,HEARTLUNGXML=?,ABDOMENXML=?,MODIFYDATE=?,MODIFYUSERID=?,CREATEUSERID=?,CREATEDATE=?,SIGNUSERID=?,SIGNUSERNAME=?,RECORDDATE=?
//			Where InPatientID=? and InPatientDate=? and OPENDATE=? and Status=0";
		private const string c_strModifyRecordSQL=c_strAddNewRecordSQL;


		/// <summary>
		/// 设置ICUNURSERECORD_GXRECORD中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update newbabycircsrecord
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// 更新ICUNURSERECORD_GXRECORD中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update newbabycircsrecord
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
																	from newbabycircsrecord
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// 从ICUNURSERECORD_GXRECORD获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from newbabycircsrecord
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
				//return lngCheckRes;	

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
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
				//return lngCheckRes;	

			long lngRes = 0;
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
            //if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
            //if(lngCheckRes <= 0)
				//return lngCheckRes;	

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
			
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" inpatientid,
       inpatientdate,
       birthtime,
       birthdays,
       birthburl,
       haematoma,
       fontanel,
       conjunctiva,
       secretion,
       pharynx,
       whitepoint,
       icterus,
       fester,
       bleeding,
       agnail,
       redstern,
       sternskin,
       heartlung,
       abdomen,
       remark,
       remarkxml,
       birthburlxml,
       haematomaxml,
       fontanelxml,
       conjunctivaxml,
       secretionxml,
       pharynxxml,
       whitepointxml,
       icterusxml,
       festerxml,
       bleedingxml,
       agnailxml,
       redsternxml,
       sternskinxml,
       heartlungxml,
       abdomenxml,
       modifydate,
       modifyuserid,
       status,
       opendate,
       deactiveddate,
       deactivedoperatorid,
       createuserid,
       createdate,
       signuserid,
       signusername,
       recorddate
  from newbabycircsrecord
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
 order by modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
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
                    clsNewBabyCircsRecord objRecordContent = new clsNewBabyCircsRecord();

                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                    objRecordContent.m_dtmBIRTHTIME = Convert.ToDateTime(dtbValue.Rows[0]["BIRTHTIME"]);
                    objRecordContent.m_strBIRTHDAYS = dtbValue.Rows[0]["BIRTHDAYS"].ToString();
                    objRecordContent.m_strBIRTHBURL = dtbValue.Rows[0]["BIRTHBURL"].ToString();
                    objRecordContent.m_strHAEMATOMA = dtbValue.Rows[0]["HAEMATOMA"].ToString();
                    objRecordContent.m_strFONTANEL = dtbValue.Rows[0]["FONTANEL"].ToString();
                    objRecordContent.m_strCONJUNCTIVA = dtbValue.Rows[0]["CONJUNCTIVA"].ToString();
                    objRecordContent.m_strSECRETION = dtbValue.Rows[0]["SECRETION"].ToString();
                    objRecordContent.m_strPHARYNX = dtbValue.Rows[0]["PHARYNX"].ToString();
                    objRecordContent.m_strWHITEPOINT = dtbValue.Rows[0]["WHITEPOINT"].ToString();
                    objRecordContent.m_strICTERUS = dtbValue.Rows[0]["ICTERUS"].ToString();
                    objRecordContent.m_strFESTER = dtbValue.Rows[0]["FESTER"].ToString();
                    objRecordContent.m_strBLEEDING = dtbValue.Rows[0]["BLEEDING"].ToString();
                    objRecordContent.m_strAGNAIL = dtbValue.Rows[0]["AGNAIL"].ToString();
                    objRecordContent.m_strREDSTERN = dtbValue.Rows[0]["REDSTERN"].ToString();
                    objRecordContent.m_strSTERNSKIN = dtbValue.Rows[0]["STERNSKIN"].ToString();
                    objRecordContent.m_strHEARTLUNG = dtbValue.Rows[0]["HEARTLUNG"].ToString();
                    objRecordContent.m_strABDOMEN = dtbValue.Rows[0]["ABDOMEN"].ToString();
                    objRecordContent.m_strREMARK = dtbValue.Rows[0]["REMARK"].ToString();
                    objRecordContent.m_strREMARKXML = dtbValue.Rows[0]["REMARKXML"].ToString();
                    objRecordContent.m_strBIRTHBURLXML = dtbValue.Rows[0]["BIRTHBURLXML"].ToString();
                    objRecordContent.m_strHAEMATOMAXML = dtbValue.Rows[0]["HAEMATOMAXML"].ToString();
                    objRecordContent.m_strFONTANELXML = dtbValue.Rows[0]["FONTANELXML"].ToString();
                    objRecordContent.m_strCONJUNCTIVAXML = dtbValue.Rows[0]["CONJUNCTIVAXML"].ToString();
                    objRecordContent.m_strSECRETIONXML = dtbValue.Rows[0]["SECRETIONXML"].ToString();
                    objRecordContent.m_strPHARYNXXML = dtbValue.Rows[0]["PHARYNXXML"].ToString();
                    objRecordContent.m_strWHITEPOINTXML = dtbValue.Rows[0]["WHITEPOINTXML"].ToString();
                    objRecordContent.m_strICTERUSXML = dtbValue.Rows[0]["ICTERUSXML"].ToString();
                    objRecordContent.m_strFESTERXML = dtbValue.Rows[0]["FESTERXML"].ToString();
                    objRecordContent.m_strBLEEDINGXML = dtbValue.Rows[0]["BLEEDINGXML"].ToString();
                    objRecordContent.m_strAGNAILXML = dtbValue.Rows[0]["AGNAILXML"].ToString();
                    objRecordContent.m_strREDSTERNXML = dtbValue.Rows[0]["REDSTERNXML"].ToString();
                    objRecordContent.m_strSTERNSKINXML = dtbValue.Rows[0]["STERNSKINXML"].ToString();
                    objRecordContent.m_strHEARTLUNGXML = dtbValue.Rows[0]["HEARTLUNGXML"].ToString();
                    objRecordContent.m_strABDOMENXML = dtbValue.Rows[0]["ABDOMENXML"].ToString();
                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_strSignUserID = dtbValue.Rows[0]["SIGNUSERID"].ToString();
                    objRecordContent.m_strSignUserName = dtbValue.Rows[0]["SIGNUSERNAME"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
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

            } 
            return lngRes;
		}

		/// <summary>
		/// 保存记录到数据库。
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
			clsNewBabyCircsRecord p_objRecord = (clsNewBabyCircsRecord)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(45, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_objRecord.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = p_objRecord.m_dtmInPatientDate;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = p_objRecord.m_dtmBIRTHTIME;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strBIRTHDAYS;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strBIRTHBURL;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strHAEMATOMA;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strFONTANEL;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strCONJUNCTIVA;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strSECRETION;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strPHARYNX;
                objLisAddItemRefArr[10].Value = p_objRecord.m_strWHITEPOINT;
                objLisAddItemRefArr[11].Value = p_objRecord.m_strICTERUS;
                objLisAddItemRefArr[12].Value = p_objRecord.m_strFESTER;
                objLisAddItemRefArr[13].Value = p_objRecord.m_strBLEEDING;
                objLisAddItemRefArr[14].Value = p_objRecord.m_strAGNAIL;
                objLisAddItemRefArr[15].Value = p_objRecord.m_strREDSTERN;
                objLisAddItemRefArr[16].Value = p_objRecord.m_strSTERNSKIN;
                objLisAddItemRefArr[17].Value = p_objRecord.m_strHEARTLUNG;
                objLisAddItemRefArr[18].Value = p_objRecord.m_strABDOMEN;
                objLisAddItemRefArr[19].Value = p_objRecord.m_strREMARK;
                objLisAddItemRefArr[20].Value = p_objRecord.m_strREMARKXML;
                objLisAddItemRefArr[21].Value = p_objRecord.m_strBIRTHBURLXML;
                objLisAddItemRefArr[22].Value = p_objRecord.m_strHAEMATOMAXML;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strFONTANELXML;
                objLisAddItemRefArr[24].Value = p_objRecord.m_strCONJUNCTIVAXML;
                objLisAddItemRefArr[25].Value = p_objRecord.m_strSECRETIONXML;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strPHARYNXXML;
                objLisAddItemRefArr[27].Value = p_objRecord.m_strWHITEPOINTXML;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strICTERUSXML;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strFESTERXML;
                objLisAddItemRefArr[30].Value = p_objRecord.m_strBLEEDINGXML;
                objLisAddItemRefArr[31].Value = p_objRecord.m_strAGNAILXML;
                objLisAddItemRefArr[32].Value = p_objRecord.m_strREDSTERNXML;
                objLisAddItemRefArr[33].Value = p_objRecord.m_strSTERNSKINXML;
                objLisAddItemRefArr[34].Value = p_objRecord.m_strHEARTLUNGXML;
                objLisAddItemRefArr[35].Value = p_objRecord.m_strABDOMENXML;
                objLisAddItemRefArr[36].DbType = DbType.DateTime;
                objLisAddItemRefArr[36].Value = p_objRecord.m_dtmModifyDate;
                objLisAddItemRefArr[37].Value = p_objRecord.m_strModifyUserID;
                objLisAddItemRefArr[38].Value = 0;
                objLisAddItemRefArr[39].DbType = DbType.DateTime;
                objLisAddItemRefArr[39].Value = p_objRecord.m_dtmOpenDate;
                objLisAddItemRefArr[40].Value = p_objRecord.m_strCreateUserID;
                objLisAddItemRefArr[41].DbType = DbType.DateTime;
                objLisAddItemRefArr[41].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr[42].Value = p_objRecord.m_strSignUserID;
                objLisAddItemRefArr[43].Value = p_objRecord.m_strSignUserName;
                objLisAddItemRefArr[44].DbType = DbType.DateTime;
                objLisAddItemRefArr[44].Value = p_objRecord.m_dtmRecordDate;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
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

			clsNewBabyCircsRecord objRecordContent = (clsNewBabyCircsRecord)p_objRecordContent;
			/// <summary>
			/// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" modifydate,modifyuserid from newbabycircsrecord
			where status =0	and inpatientid = ? and inpatientdate = ? and opendate = ? order by modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (objRecordContent.m_dtmModifyDate == Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]))
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

            } 
            return lngRes;		
		}

		/// <summary>
		/// 把新修改的内容保存到数据库。
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
		
			clsNewBabyCircsRecord p_objRecord = (clsNewBabyCircsRecord)p_objRecordContent;
			long lngRes = 0;


            try
            {
                #region
                //				//获取IDataParameter数组
                //				IDataParameter[] objLisAddItemRefArr = null; 
                //				p_objHRPServ.CreateDatabaseParameter(43,out objLisAddItemRefArr);
                //
                //				objLisAddItemRefArr[0].Value = p_objRecord.m_dtmBIRTHTIME;
                //				objLisAddItemRefArr[1].Value = p_objRecord.m_strBIRTHDAYS;
                //				objLisAddItemRefArr[2].Value = p_objRecord.m_strBIRTHBURL;
                //				objLisAddItemRefArr[3].Value = p_objRecord.m_strHAEMATOMA;
                //				objLisAddItemRefArr[4].Value = p_objRecord.m_strFONTANEL;
                //				objLisAddItemRefArr[5].Value = p_objRecord.m_strCONJUNCTIVA;
                //				objLisAddItemRefArr[6].Value = p_objRecord.m_strSECRETION;
                //				objLisAddItemRefArr[7].Value = p_objRecord.m_strPHARYNX;
                //				objLisAddItemRefArr[8].Value = p_objRecord.m_strWHITEPOINT;
                //				objLisAddItemRefArr[9].Value = p_objRecord.m_strICTERUS;
                //				objLisAddItemRefArr[10].Value = p_objRecord.m_strFESTER;
                //				objLisAddItemRefArr[11].Value = p_objRecord.m_strBLEEDING;
                //				objLisAddItemRefArr[12].Value = p_objRecord.m_strAGNAIL;
                //				objLisAddItemRefArr[13].Value = p_objRecord.m_strREDSTERN;
                //				objLisAddItemRefArr[14].Value = p_objRecord.m_strSTERNSKIN;
                //				objLisAddItemRefArr[15].Value = p_objRecord.m_strHEARTLUNG;
                //				objLisAddItemRefArr[16].Value = p_objRecord.m_strABDOMEN;
                //				objLisAddItemRefArr[17].Value = p_objRecord.m_strREMARK;
                //				objLisAddItemRefArr[18].Value = p_objRecord.m_strREMARKXML;
                //				objLisAddItemRefArr[19].Value = p_objRecord.m_strBIRTHBURLXML;
                //				objLisAddItemRefArr[20].Value = p_objRecord.m_strHAEMATOMAXML;
                //				objLisAddItemRefArr[21].Value = p_objRecord.m_strFONTANELXML;
                //				objLisAddItemRefArr[22].Value = p_objRecord.m_strCONJUNCTIVAXML;
                //				objLisAddItemRefArr[23].Value = p_objRecord.m_strSECRETIONXML;
                //				objLisAddItemRefArr[24].Value = p_objRecord.m_strPHARYNXXML;
                //				objLisAddItemRefArr[25].Value = p_objRecord.m_strWHITEPOINTXML;
                //				objLisAddItemRefArr[26].Value = p_objRecord.m_strICTERUSXML;
                //				objLisAddItemRefArr[27].Value = p_objRecord.m_strFESTERXML;
                //				objLisAddItemRefArr[28].Value = p_objRecord.m_strBLEEDINGXML;
                //				objLisAddItemRefArr[29].Value = p_objRecord.m_strAGNAILXML;
                //				objLisAddItemRefArr[30].Value = p_objRecord.m_strREDSTERNXML;
                //				objLisAddItemRefArr[31].Value = p_objRecord.m_strSTERNSKINXML;
                //				objLisAddItemRefArr[32].Value = p_objRecord.m_strHEARTLUNGXML;
                //				objLisAddItemRefArr[33].Value = p_objRecord.m_strABDOMENXML;
                //				objLisAddItemRefArr[34].Value = p_objRecord.m_dtmModifyDate;
                //				objLisAddItemRefArr[35].Value = p_objRecord.m_strModifyUserID;
                //				objLisAddItemRefArr[36].Value = p_objRecord.m_strCreateUserID;
                //				objLisAddItemRefArr[37].Value = p_objRecord.m_dtmCreateDate;
                //				objLisAddItemRefArr[37].Value = p_objRecord.m_strSignUserID;
                //				objLisAddItemRefArr[38].Value = p_objRecord.m_strSignUserName;
                //				objLisAddItemRefArr[39].Value = p_objRecord.m_dtmRecordDate;
                //				objLisAddItemRefArr[40].Value = p_objRecord.m_strInPatientID;
                //				objLisAddItemRefArr[41].Value = p_objRecord.m_dtmInPatientDate;
                //				objLisAddItemRefArr[42].Value = p_objRecord.m_dtmOpenDate;
                //
                //				//执行SQL
                //				long lngEff=0;
                //				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objLisAddItemRefArr);
                #endregion
                m_lngAddNewRecord2DB(p_objRecord, p_objHRPServ);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
		}

		/// <summary>
		/// 把记录从数据中“删除”(供外部直接调用)
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteCircsRecord(clsTrackRecordContent p_objRecordContent)
		{
			 

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, objHRPServ); 
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
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			clsNewBabyCircsRecord objRecordContent = (clsNewBabyCircsRecord)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmOpenDate;

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

            string c_strGetDeleteRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" inpatientid,
       inpatientdate,
       birthtime,
       birthdays,
       birthburl,
       haematoma,
       fontanel,
       conjunctiva,
       secretion,
       pharynx,
       whitepoint,
       icterus,
       fester,
       bleeding,
       agnail,
       redstern,
       sternskin,
       heartlung,
       abdomen,
       remark,
       remarkxml,
       birthburlxml,
       haematomaxml,
       fontanelxml,
       conjunctivaxml,
       secretionxml,
       pharynxxml,
       whitepointxml,
       icterusxml,
       festerxml,
       bleedingxml,
       agnailxml,
       redsternxml,
       sternskinxml,
       heartlungxml,
       abdomenxml,
       modifydate,
       modifyuserid,
       status,
       opendate,
       deactiveddate,
       deactivedoperatorid,
       createuserid,
       createdate,
       signuserid,
       signusername,
       recorddate
  from newbabycircsrecord
 where status = 1
   and inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
 order by modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;
		
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
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
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsNewBabyCircsRecord objRecordContent = new clsNewBabyCircsRecord();

                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                    objRecordContent.m_dtmBIRTHTIME = Convert.ToDateTime(dtbValue.Rows[0]["BIRTHTIME"]);
                    objRecordContent.m_strBIRTHDAYS = dtbValue.Rows[0]["BIRTHDAYS"].ToString();
                    objRecordContent.m_strBIRTHBURL = dtbValue.Rows[0]["BIRTHBURL"].ToString();
                    objRecordContent.m_strHAEMATOMA = dtbValue.Rows[0]["HAEMATOMA"].ToString();
                    objRecordContent.m_strFONTANEL = dtbValue.Rows[0]["FONTANEL"].ToString();
                    objRecordContent.m_strCONJUNCTIVA = dtbValue.Rows[0]["CONJUNCTIVA"].ToString();
                    objRecordContent.m_strSECRETION = dtbValue.Rows[0]["SECRETION"].ToString();
                    objRecordContent.m_strPHARYNX = dtbValue.Rows[0]["PHARYNX"].ToString();
                    objRecordContent.m_strWHITEPOINT = dtbValue.Rows[0]["WHITEPOINT"].ToString();
                    objRecordContent.m_strICTERUS = dtbValue.Rows[0]["ICTERUS"].ToString();
                    objRecordContent.m_strFESTER = dtbValue.Rows[0]["FESTER"].ToString();
                    objRecordContent.m_strBLEEDING = dtbValue.Rows[0]["BLEEDING"].ToString();
                    objRecordContent.m_strAGNAIL = dtbValue.Rows[0]["AGNAIL"].ToString();
                    objRecordContent.m_strREDSTERN = dtbValue.Rows[0]["REDSTERN"].ToString();
                    objRecordContent.m_strSTERNSKIN = dtbValue.Rows[0]["STERNSKIN"].ToString();
                    objRecordContent.m_strHEARTLUNG = dtbValue.Rows[0]["HEARTLUNG"].ToString();
                    objRecordContent.m_strABDOMEN = dtbValue.Rows[0]["ABDOMEN"].ToString();
                    objRecordContent.m_strREMARK = dtbValue.Rows[0]["REMARK"].ToString();
                    objRecordContent.m_strREMARKXML = dtbValue.Rows[0]["REMARKXML"].ToString();
                    objRecordContent.m_strBIRTHBURLXML = dtbValue.Rows[0]["BIRTHBURLXML"].ToString();
                    objRecordContent.m_strHAEMATOMAXML = dtbValue.Rows[0]["HAEMATOMAXML"].ToString();
                    objRecordContent.m_strFONTANELXML = dtbValue.Rows[0]["FONTANELXML"].ToString();
                    objRecordContent.m_strCONJUNCTIVAXML = dtbValue.Rows[0]["CONJUNCTIVAXML"].ToString();
                    objRecordContent.m_strSECRETIONXML = dtbValue.Rows[0]["SECRETIONXML"].ToString();
                    objRecordContent.m_strPHARYNXXML = dtbValue.Rows[0]["PHARYNXXML"].ToString();
                    objRecordContent.m_strWHITEPOINTXML = dtbValue.Rows[0]["WHITEPOINTXML"].ToString();
                    objRecordContent.m_strICTERUSXML = dtbValue.Rows[0]["ICTERUSXML"].ToString();
                    objRecordContent.m_strFESTERXML = dtbValue.Rows[0]["FESTERXML"].ToString();
                    objRecordContent.m_strBLEEDINGXML = dtbValue.Rows[0]["BLEEDINGXML"].ToString();
                    objRecordContent.m_strAGNAILXML = dtbValue.Rows[0]["AGNAILXML"].ToString();
                    objRecordContent.m_strREDSTERNXML = dtbValue.Rows[0]["REDSTERNXML"].ToString();
                    objRecordContent.m_strSTERNSKINXML = dtbValue.Rows[0]["STERNSKINXML"].ToString();
                    objRecordContent.m_strHEARTLUNGXML = dtbValue.Rows[0]["HEARTLUNGXML"].ToString();
                    objRecordContent.m_strABDOMENXML = dtbValue.Rows[0]["ABDOMENXML"].ToString();
                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[0]["DEACTIVEDDATE"]);
                    objRecordContent.m_strDeActivedOperatorID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_strSignUserID = dtbValue.Rows[0]["SIGNUSERID"].ToString();
                    objRecordContent.m_strSignUserName = dtbValue.Rows[0]["SIGNUSERNAME"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);

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
		/// 获取所有新生儿情况记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strBirthTime"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strBirthTime,
			out clsNewBabyCircsRecord[] p_objRecordArr)
		{
			p_objRecordArr = null;
 			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

            string strGetAllContentSQL = @"select t4.lastname_vchr as createusername, t1.inpatientid,
       t1.inpatientdate,
       t1.birthtime,
       t1.birthdays,
       t1.birthburl,
       t1.haematoma,
       t1.fontanel,
       t1.conjunctiva,
       t1.secretion,
       t1.pharynx,
       t1.whitepoint,
       t1.icterus,
       t1.fester,
       t1.bleeding,
       t1.agnail,
       t1.redstern,
       t1.sternskin,
       t1.heartlung,
       t1.abdomen,
       t1.remark,
       t1.remarkxml,
       t1.birthburlxml,
       t1.haematomaxml,
       t1.fontanelxml,
       t1.conjunctivaxml,
       t1.secretionxml,
       t1.pharynxxml,
       t1.whitepointxml,
       t1.icterusxml,
       t1.festerxml,
       t1.bleedingxml,
       t1.agnailxml,
       t1.redsternxml,
       t1.sternskinxml,
       t1.heartlungxml,
       t1.abdomenxml,
       t1.modifydate,
       t1.modifyuserid,
       t1.status,
       t1.opendate,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.createuserid,
       t1.createdate,
       t1.signuserid,
       t1.signusername,
       t1.recorddate
  from newbabycircsrecord t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
 order by t1.createdate, modifydate";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//护理记录内容  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetAllContentSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsNewBabyCircsRecord objRecordContent = null;
                    p_objRecordArr = new clsNewBabyCircsRecord[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordContent = new clsNewBabyCircsRecord();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        objRecordContent.m_dtmBIRTHTIME = Convert.ToDateTime(dtbValue.Rows[i]["BIRTHTIME"]);
                        objRecordContent.m_strBIRTHDAYS = dtbValue.Rows[i]["BIRTHDAYS"].ToString();
                        objRecordContent.m_strBIRTHBURL = dtbValue.Rows[i]["BIRTHBURL"].ToString();
                        objRecordContent.m_strHAEMATOMA = dtbValue.Rows[i]["HAEMATOMA"].ToString();
                        objRecordContent.m_strFONTANEL = dtbValue.Rows[i]["FONTANEL"].ToString();
                        objRecordContent.m_strCONJUNCTIVA = dtbValue.Rows[i]["CONJUNCTIVA"].ToString();
                        objRecordContent.m_strSECRETION = dtbValue.Rows[i]["SECRETION"].ToString();
                        objRecordContent.m_strPHARYNX = dtbValue.Rows[i]["PHARYNX"].ToString();
                        objRecordContent.m_strWHITEPOINT = dtbValue.Rows[i]["WHITEPOINT"].ToString();
                        objRecordContent.m_strICTERUS = dtbValue.Rows[i]["ICTERUS"].ToString();
                        objRecordContent.m_strFESTER = dtbValue.Rows[i]["FESTER"].ToString();
                        objRecordContent.m_strBLEEDING = dtbValue.Rows[i]["BLEEDING"].ToString();
                        objRecordContent.m_strAGNAIL = dtbValue.Rows[i]["AGNAIL"].ToString();
                        objRecordContent.m_strREDSTERN = dtbValue.Rows[i]["REDSTERN"].ToString();
                        objRecordContent.m_strSTERNSKIN = dtbValue.Rows[i]["STERNSKIN"].ToString();
                        objRecordContent.m_strHEARTLUNG = dtbValue.Rows[i]["HEARTLUNG"].ToString();
                        objRecordContent.m_strABDOMEN = dtbValue.Rows[i]["ABDOMEN"].ToString();
                        objRecordContent.m_strREMARK = dtbValue.Rows[i]["REMARK"].ToString();
                        objRecordContent.m_strREMARKXML = dtbValue.Rows[i]["REMARKXML"].ToString();
                        objRecordContent.m_strBIRTHBURLXML = dtbValue.Rows[i]["BIRTHBURLXML"].ToString();
                        objRecordContent.m_strHAEMATOMAXML = dtbValue.Rows[i]["HAEMATOMAXML"].ToString();
                        objRecordContent.m_strFONTANELXML = dtbValue.Rows[i]["FONTANELXML"].ToString();
                        objRecordContent.m_strCONJUNCTIVAXML = dtbValue.Rows[i]["CONJUNCTIVAXML"].ToString();
                        objRecordContent.m_strSECRETIONXML = dtbValue.Rows[i]["SECRETIONXML"].ToString();
                        objRecordContent.m_strPHARYNXXML = dtbValue.Rows[i]["PHARYNXXML"].ToString();
                        objRecordContent.m_strWHITEPOINTXML = dtbValue.Rows[i]["WHITEPOINTXML"].ToString();
                        objRecordContent.m_strICTERUSXML = dtbValue.Rows[i]["ICTERUSXML"].ToString();
                        objRecordContent.m_strFESTERXML = dtbValue.Rows[i]["FESTERXML"].ToString();
                        objRecordContent.m_strBLEEDINGXML = dtbValue.Rows[i]["BLEEDINGXML"].ToString();
                        objRecordContent.m_strAGNAILXML = dtbValue.Rows[i]["AGNAILXML"].ToString();
                        objRecordContent.m_strREDSTERNXML = dtbValue.Rows[i]["REDSTERNXML"].ToString();
                        objRecordContent.m_strSTERNSKINXML = dtbValue.Rows[i]["STERNSKINXML"].ToString();
                        objRecordContent.m_strHEARTLUNGXML = dtbValue.Rows[i]["HEARTLUNGXML"].ToString();
                        objRecordContent.m_strABDOMENXML = dtbValue.Rows[i]["ABDOMENXML"].ToString();
                        objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        objRecordContent.m_strSignUserID = dtbValue.Rows[i]["SIGNUSERID"].ToString();
                        objRecordContent.m_strSignUserName = dtbValue.Rows[i]["SIGNUSERNAME"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

                        p_objRecordArr[i] = objRecordContent;
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

            } 
            return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 获取所有最新的新生儿情况记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strBirthTime"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllModifiedContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strBirthTime,
			out clsNewBabyCircsRecord[] p_objRecordArr)
		{
			p_objRecordArr = null;
 			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

            string strGetAllContentSQL = @"select t4.lastname_vchr as createusername, t1.inpatientid,
       t1.inpatientdate,
       t1.birthtime,
       t1.birthdays,
       t1.birthburl,
       t1.haematoma,
       t1.fontanel,
       t1.conjunctiva,
       t1.secretion,
       t1.pharynx,
       t1.whitepoint,
       t1.icterus,
       t1.fester,
       t1.bleeding,
       t1.agnail,
       t1.redstern,
       t1.sternskin,
       t1.heartlung,
       t1.abdomen,
       t1.remark,
       t1.remarkxml,
       t1.birthburlxml,
       t1.haematomaxml,
       t1.fontanelxml,
       t1.conjunctivaxml,
       t1.secretionxml,
       t1.pharynxxml,
       t1.whitepointxml,
       t1.icterusxml,
       t1.festerxml,
       t1.bleedingxml,
       t1.agnailxml,
       t1.redsternxml,
       t1.sternskinxml,
       t1.heartlungxml,
       t1.abdomenxml,
       t1.modifydate,
       t1.modifyuserid,
       t1.status,
       t1.opendate,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.createuserid,
       t1.createdate,
       t1.signuserid,
       t1.signusername,
       t1.recorddate
  from newbabycircsrecord t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
   and t1.modifydate = (select max(modifydate)
                          from newbabycircsrecord
                         where inpatientid = t1.inpatientid
                           and inpatientdate = t1.inpatientdate
                           and birthtime = t1.birthtime
                           and createdate = t1.createdate)
 order by t1.createdate, modifydate";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//护理记录内容  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetAllContentSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsNewBabyCircsRecord objRecordContent = null;
                    p_objRecordArr = new clsNewBabyCircsRecord[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordContent = new clsNewBabyCircsRecord();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        objRecordContent.m_dtmBIRTHTIME = Convert.ToDateTime(dtbValue.Rows[i]["BIRTHTIME"]);
                        objRecordContent.m_strBIRTHDAYS = dtbValue.Rows[i]["BIRTHDAYS"].ToString();
                        objRecordContent.m_strBIRTHBURL = dtbValue.Rows[i]["BIRTHBURL"].ToString();
                        objRecordContent.m_strHAEMATOMA = dtbValue.Rows[i]["HAEMATOMA"].ToString();
                        objRecordContent.m_strFONTANEL = dtbValue.Rows[i]["FONTANEL"].ToString();
                        objRecordContent.m_strCONJUNCTIVA = dtbValue.Rows[i]["CONJUNCTIVA"].ToString();
                        objRecordContent.m_strSECRETION = dtbValue.Rows[i]["SECRETION"].ToString();
                        objRecordContent.m_strPHARYNX = dtbValue.Rows[i]["PHARYNX"].ToString();
                        objRecordContent.m_strWHITEPOINT = dtbValue.Rows[i]["WHITEPOINT"].ToString();
                        objRecordContent.m_strICTERUS = dtbValue.Rows[i]["ICTERUS"].ToString();
                        objRecordContent.m_strFESTER = dtbValue.Rows[i]["FESTER"].ToString();
                        objRecordContent.m_strBLEEDING = dtbValue.Rows[i]["BLEEDING"].ToString();
                        objRecordContent.m_strAGNAIL = dtbValue.Rows[i]["AGNAIL"].ToString();
                        objRecordContent.m_strREDSTERN = dtbValue.Rows[i]["REDSTERN"].ToString();
                        objRecordContent.m_strSTERNSKIN = dtbValue.Rows[i]["STERNSKIN"].ToString();
                        objRecordContent.m_strHEARTLUNG = dtbValue.Rows[i]["HEARTLUNG"].ToString();
                        objRecordContent.m_strABDOMEN = dtbValue.Rows[i]["ABDOMEN"].ToString();
                        objRecordContent.m_strREMARK = dtbValue.Rows[i]["REMARK"].ToString();
                        objRecordContent.m_strREMARKXML = dtbValue.Rows[i]["REMARKXML"].ToString();
                        objRecordContent.m_strBIRTHBURLXML = dtbValue.Rows[i]["BIRTHBURLXML"].ToString();
                        objRecordContent.m_strHAEMATOMAXML = dtbValue.Rows[i]["HAEMATOMAXML"].ToString();
                        objRecordContent.m_strFONTANELXML = dtbValue.Rows[i]["FONTANELXML"].ToString();
                        objRecordContent.m_strCONJUNCTIVAXML = dtbValue.Rows[i]["CONJUNCTIVAXML"].ToString();
                        objRecordContent.m_strSECRETIONXML = dtbValue.Rows[i]["SECRETIONXML"].ToString();
                        objRecordContent.m_strPHARYNXXML = dtbValue.Rows[i]["PHARYNXXML"].ToString();
                        objRecordContent.m_strWHITEPOINTXML = dtbValue.Rows[i]["WHITEPOINTXML"].ToString();
                        objRecordContent.m_strICTERUSXML = dtbValue.Rows[i]["ICTERUSXML"].ToString();
                        objRecordContent.m_strFESTERXML = dtbValue.Rows[i]["FESTERXML"].ToString();
                        objRecordContent.m_strBLEEDINGXML = dtbValue.Rows[i]["BLEEDINGXML"].ToString();
                        objRecordContent.m_strAGNAILXML = dtbValue.Rows[i]["AGNAILXML"].ToString();
                        objRecordContent.m_strREDSTERNXML = dtbValue.Rows[i]["REDSTERNXML"].ToString();
                        objRecordContent.m_strSTERNSKINXML = dtbValue.Rows[i]["STERNSKINXML"].ToString();
                        objRecordContent.m_strHEARTLUNGXML = dtbValue.Rows[i]["HEARTLUNGXML"].ToString();
                        objRecordContent.m_strABDOMENXML = dtbValue.Rows[i]["ABDOMENXML"].ToString();
                        objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        objRecordContent.m_strSignUserID = dtbValue.Rows[i]["SIGNUSERID"].ToString();
                        objRecordContent.m_strSignUserName = dtbValue.Rows[i]["SIGNUSERNAME"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

                        p_objRecordArr[i] = objRecordContent;
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

            } 
            return (long)enmOperationResult.DB_Succeed;
		}
	}
}
