using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// 静脉特殊化疗用药观察记录表(广西)(添加，修改)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsVeinSpecialUseDrug_ConService : clsDiseaseTrackService
	{
		 

		#region SQL语句
		/// <summary>
		/// 从icuacad_veinspecialusedrug获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from icuacad_veinspecialusedrug
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// 从icuacad_veinspecialusedrug中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from icuacad_veinspecialusedrug
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// 从icuacad_veinspecialusedrug获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from icuacad_veinspecialusedrug
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// 添加记录到icuacad_veinspecialusedrug  (28col)
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into icuacad_veinspecialusedrug (inpatientid,inpatientdate,opendate,
						createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,begintime_date,begintime_datexml,medicinename_chr,medicinename_chrxml,drop_chr,drop_chrxml,
						minute_chr,minute_chrxml,ingear_chr,ingear_chrxml,abnormity_chr,abnormity_chrxml,solve_chr,solve_chrxml,
						remark_chr,remark_chrxml,underwrite_chr,underwrite_chrxml,checkdate_date,id_chr) 
						values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
								
		/// <summary>
		/// 添加记录到icuacad_veinspecialusedrugcon   (15col)
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into icuacad_veinspecialusedrugcon  (inpatientid,inpatientdate,
						opendate,modifydate,modifyuserid,medicinename_chr_right,drop_chr_right,minute_chr_right,ingear_chr_right,
						abnormity_chr_right,solve_chr_right,remark_chr_right,underwrite_chr_right,checkdate_date,begintime_date_right) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到icuacad_veinspecialusedrug	(19col)+4
		/// </summary>
		private const string c_strModifyRecordSQL= @"update icuacad_veinspecialusedrug 
			set begintime_date=?,begintime_datexml=?,medicinename_chr=?,medicinename_chrxml=?,drop_chr=?,drop_chrxml=?,
		minute_chr=?,minute_chrxml=?,ingear_chr=?,ingear_chrxml=?,abnormity_chr=?,abnormity_chrxml=?,solve_chr=?,solve_chrxml=?,
		remark_chr=?,remark_chrxml=?,underwrite_chr=?,underwrite_chrxml=?,checkdate_date=? 
		  												where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and id_chr = ?
														and status = 0";

		/// <summary>
		/// 修改记录到icuacad_veinspecialusedrugcon 
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// 设置icuacad_veinspecialusedrug中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update icuacad_veinspecialusedrug
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// 更新icuacad_veinspecialusedrug中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update icuacad_veinspecialusedrug
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// 更新icuacad_veinspecialusedrug中FirstPrintDate
		/// </summary>
		private const string c_strUpdateALLFirstPrintDateSQL= @"update icuacad_veinspecialusedrug
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?																
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// 从icuacad_veinspecialusedrug获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from icuacad_veinspecialusedrug
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// 从icuacad_veinspecialusedrug获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from icuacad_veinspecialusedrug
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";



		#endregion

		#region 获取病人的该记录时间列表
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsVeinSpecialUseDrug_ConService","m_lngGetRecordTimeList");
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
		#endregion

		#region 更新数据库中的首次打印时间
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
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsVeinSpecialUseDrug_ConService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
		#endregion

		#region 获取病人的已经被删除记录时间列表
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsVeinSpecialUseDrug_ConService","m_lngGetDeleteRecordTimeList");
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
		#endregion

		#region 获取病人的已经被删除记录时间列表
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsVeinSpecialUseDrug_ConService","m_lngGetDeleteRecordTimeListAll");
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
		#endregion

		#region 获取指定记录的内容
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
			//,t2.MODIFYDATE as MODIFYDATE,t2.MODIFYUSERID as MODIFYUSERID
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
       t1.begintime_date,
       t1.medicinename_chr,
       t1.drop_chr,
       t1.minute_chr,
       t1.ingear_chr,
       t1.abnormity_chr,
       t1.solve_chr,
       t1.remark_chr,
       t1.underwrite_chr,
       t1.checkdate_date,
       t1.begintime_datexml,
       t1.medicinename_chrxml,
       t1.drop_chrxml,
       t1.minute_chrxml,
       t1.ingear_chrxml,
       t1.abnormity_chrxml,
       t1.solve_chrxml,
       t1.remark_chrxml,
       t1.underwrite_chrxml,
       t1.id_chr,
       t2.modifydate,
       t2.modifyuserid,
       t2.medicinename_chr_right,
       t2.drop_chr_right,
       t2.minute_chr_right,
       t2.ingear_chr_right,
       t2.abnormity_chr_right,
       t2.solve_chr_right,
       t2.remark_chr_right,
       t2.underwrite_chr_right,
       t2.begintime_date_right

  from icuacad_veinspecialusedrug t1
  join icuacad_veinspecialusedrugcon t2 on (t1.inpatientid = t2.inpatientid and
                                           t1.inpatientdate =
                                           t2.inpatientdate and
                                           t1.opendate = t2.opendate and
                                           t1.status = 0 and
                                           t1.inpatientid = ? and
                                           t1.inpatientdate = ? and
                                           t1.opendate = ?)
 order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
		
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsVeinSpecialUseDrugValue objRecordContent = new clsVeinSpecialUseDrugValue();
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

                    objRecordContent.m_strBEGINTIME_DATE = dtbValue.Rows[0]["BEGINTIME_DATE"].ToString();
                    objRecordContent.m_strBEGINTIME_DATE_RIGHT = dtbValue.Rows[0]["BEGINTIME_DATE_RIGHT"].ToString();
                    objRecordContent.m_strBEGINTIME_DATEXML = dtbValue.Rows[0]["BEGINTIME_DATEXML"].ToString();

                    objRecordContent.m_strMEDICINENAME_CHR = dtbValue.Rows[0]["MEDICINENAME_CHR"].ToString();
                    objRecordContent.m_strMEDICINENAME_CHR_RIGHT = dtbValue.Rows[0]["MEDICINENAME_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMEDICINENAME_CHRXML = dtbValue.Rows[0]["MEDICINENAME_CHRXML"].ToString();

                    objRecordContent.m_strDROP_CHR = dtbValue.Rows[0]["DROP_CHR"].ToString();
                    objRecordContent.m_strDROP_CHR_RIGHT = dtbValue.Rows[0]["DROP_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDROP_CHRXML = dtbValue.Rows[0]["DROP_CHRXML"].ToString();

                    objRecordContent.m_strMINUTE_CHR = dtbValue.Rows[0]["MINUTE_CHR"].ToString();
                    objRecordContent.m_strMINUTE_CHR_RIGHT = dtbValue.Rows[0]["MINUTE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMINUTE_CHRXML = dtbValue.Rows[0]["MINUTE_CHRXML"].ToString();

                    objRecordContent.m_strINGEAR_CHR = dtbValue.Rows[0]["INGEAR_CHR"].ToString();
                    objRecordContent.m_strINGEAR_CHR_RIGHT = dtbValue.Rows[0]["INGEAR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strINGEAR_CHRXML = dtbValue.Rows[0]["INGEAR_CHRXML"].ToString();

                    objRecordContent.m_strABNORMITY_CHR = dtbValue.Rows[0]["ABNORMITY_CHR"].ToString();
                    objRecordContent.m_strABNORMITY_CHR_RIGHT = dtbValue.Rows[0]["ABNORMITY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strABNORMITY_CHRXML = dtbValue.Rows[0]["ABNORMITY_CHRXML"].ToString();


                    objRecordContent.m_strSOLVE_CHR = dtbValue.Rows[0]["SOLVE_CHR"].ToString();
                    objRecordContent.m_strSOLVE_CHR_RIGHT = dtbValue.Rows[0]["SOLVE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strSOLVE_CHRXML = dtbValue.Rows[0]["SOLVE_CHRXML"].ToString();

                    objRecordContent.m_strREMARK_CHR = dtbValue.Rows[0]["REMARK_CHR"].ToString();
                    objRecordContent.m_strREMARK_CHR_RIGHT = dtbValue.Rows[0]["REMARK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strREMARK_CHRXML = dtbValue.Rows[0]["REMARK_CHRXML"].ToString();

                    objRecordContent.m_strUNDERWRITE_CHR = dtbValue.Rows[0]["UNDERWRITE_CHR"].ToString();
                    objRecordContent.m_strUNDERWRITE_CHR_RIGHT = dtbValue.Rows[0]["UNDERWRITE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUNDERWRITE_CHRXML = dtbValue.Rows[0]["UNDERWRITE_CHRXML"].ToString();

                    objRecordContent.m_strCHECKDATE_DATE = Convert.ToDateTime(dtbValue.Rows[0]["CHECKDATE_DATE"]).ToString("yyyy-MM-dd HH:mm:ss");


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
		#endregion 

		#region 查看是否有相同的记录时间
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

            } return lngRes;
		}
		#endregion 

		#region 保存记录到数据库。添加主表,添加子表.
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
            string strRetID = "";             
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsVeinSpecialUseDrugValue objRecordContent = (clsVeinSpecialUseDrugValue)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (objRecordContent.m_strTemp != "")
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(objRecordContent.m_strTemp);
                if (objRecordContent.m_strID_CHR.Trim() == "-3") //新建标志
                {

                    long lng = m_lngInsertCheckDate(objRecordContent.m_strInPatientID,
                                            objRecordContent.m_dtmInPatientDate.ToString(),
                                            objRecordContent.m_dtmFluidBEGINTIME_DATE.ToString("yyyy-MM-dd HH:mm:ss"),
                                            objRecordContent.m_dtmfluidEndTIME_DATE.ToString("yyyy-MM-dd HH:mm:ss"), out strRetID,
                                            objRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss"));


                    if (lng > 0)
                    {

                    }
                    else
                    {
                        return lng;
                    }
                }

                #region 付值
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(29, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[4].Value = objRecordContent.m_strCreateUserID;
                objDPArr[5].Value = objRecordContent.m_bytIfConfirm;

                if (objRecordContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objRecordContent.m_strConfirmReason;
                if (objRecordContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objRecordContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;

                objDPArr[9].Value = objRecordContent.m_strBEGINTIME_DATE;
                objDPArr[10].Value = objRecordContent.m_strBEGINTIME_DATEXML;

                objDPArr[11].Value = objRecordContent.m_strMEDICINENAME_CHR;
                objDPArr[12].Value = objRecordContent.m_strMEDICINENAME_CHRXML;
                objDPArr[13].Value = objRecordContent.m_strDROP_CHR;
                objDPArr[14].Value = objRecordContent.m_strDROP_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strMINUTE_CHR;
                objDPArr[16].Value = objRecordContent.m_strMINUTE_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strINGEAR_CHR;
                objDPArr[18].Value = objRecordContent.m_strINGEAR_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strABNORMITY_CHR;
                objDPArr[20].Value = objRecordContent.m_strABNORMITY_CHRXML;

                objDPArr[21].Value = objRecordContent.m_strSOLVE_CHR;
                objDPArr[22].Value = objRecordContent.m_strSOLVE_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strREMARK_CHR;
                objDPArr[24].Value = objRecordContent.m_strREMARK_CHRXML;

                objDPArr[25].Value = objRecordContent.m_strUNDERWRITE_CHR;
                objDPArr[26].Value = objRecordContent.m_strUNDERWRITE_CHRXML;
                objDPArr[27].Value = DateTime.Parse(objRecordContent.m_dtmFluidBEGINTIME_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
                if (objRecordContent.m_strID_CHR.Trim() == "-3") //新建标志
                {
                    objDPArr[28].Value = strRetID;
                }
                else
                    objDPArr[28].Value = objRecordContent.m_strID_CHR;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //content
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(15, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[5].Value = objRecordContent.m_strMEDICINENAME_CHR_RIGHT;
                //				objDPArr2[5].Value = "";
                objDPArr2[6].Value = objRecordContent.m_strDROP_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strMINUTE_CHR_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strINGEAR_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strABNORMITY_CHR_RIGHT;

                objDPArr2[10].Value = objRecordContent.m_strSOLVE_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strREMARK_CHR_RIGHT;

                //				objDPArr2[12].Value = objRecordContent.m_strUNDERWRITE_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strUNDERWRITE_CHR;
                objDPArr2[13].DbType = DbType.DateTime;
                objDPArr2[13].Value = DateTime.Parse(objRecordContent.m_dtmFluidBEGINTIME_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr2[14].Value = objRecordContent.m_strBEGINTIME_DATE_RIGHT;

                #endregion
                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

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
		#endregion 

		#region 查看当前记录是否最新的记录
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

			clsVeinSpecialUseDrugValue objRecordContent = (clsVeinSpecialUseDrugValue)p_objRecordContent;
			/// <summary>
			/// 获取指定表单的最后修改时间。
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from icuacad_veinspecialusedrug t1,icuacad_veinspecialusedrugcon  t2
			where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
			and t1.opendate = t2.opendate and t1.status =0
			and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;
		
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
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

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
                    //if (objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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
		#endregion

		#region 把新修改的内容保存到数据库。更新主表,添加子表.
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
			if(p_objRecordContent==null || p_objRecordContent.m_dtmCreateDate.ToString()==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
		
			clsVeinSpecialUseDrugValue objRecordContent = (clsVeinSpecialUseDrugValue)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region set value
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);

                objDPArr[0].Value = objRecordContent.m_strBEGINTIME_DATE;
                objDPArr[1].Value = objRecordContent.m_strBEGINTIME_DATEXML;
                objDPArr[2].Value = objRecordContent.m_strMEDICINENAME_CHR;
                objDPArr[3].Value = objRecordContent.m_strMEDICINENAME_CHRXML;
                objDPArr[4].Value = objRecordContent.m_strDROP_CHR;
                objDPArr[5].Value = objRecordContent.m_strDROP_CHRXML;
                objDPArr[6].Value = objRecordContent.m_strMINUTE_CHR;
                objDPArr[7].Value = objRecordContent.m_strMINUTE_CHRXML;
                objDPArr[8].Value = objRecordContent.m_strINGEAR_CHR;
                objDPArr[9].Value = objRecordContent.m_strINGEAR_CHRXML;
                objDPArr[10].Value = objRecordContent.m_strABNORMITY_CHR;
                objDPArr[11].Value = objRecordContent.m_strABNORMITY_CHRXML;

                objDPArr[12].Value = objRecordContent.m_strSOLVE_CHR;
                objDPArr[13].Value = objRecordContent.m_strSOLVE_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strREMARK_CHR;
                objDPArr[15].Value = objRecordContent.m_strREMARK_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strUNDERWRITE_CHR;
                objDPArr[17].Value = objRecordContent.m_strUNDERWRITE_CHRXML;
                objDPArr[18].DbType = DbType.DateTime;
                objDPArr[18].Value = DateTime.Parse(objRecordContent.m_strCHECKDATE_DATE.Trim());

                objDPArr[19].Value = objRecordContent.m_strInPatientID;
                objDPArr[20].DbType = DbType.DateTime;
                objDPArr[20].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[21].DbType = DbType.DateTime;
                objDPArr[21].Value = objRecordContent.m_dtmOpenDate;
                objDPArr[22].Value = objRecordContent.m_strID_CHR;
                #endregion
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                #region set value
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(15, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[5].Value = objRecordContent.m_strMEDICINENAME_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strDROP_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strMINUTE_CHR_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strINGEAR_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strABNORMITY_CHR_RIGHT;

                objDPArr2[10].Value = objRecordContent.m_strSOLVE_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strREMARK_CHR_RIGHT;

                objDPArr2[12].Value = objRecordContent.m_strUNDERWRITE_CHR;
                objDPArr2[13].DbType = DbType.DateTime;
                objDPArr2[13].Value = DateTime.Parse(objRecordContent.m_strCHECKDATE_DATE.Trim());
                objDPArr2[14].Value = objRecordContent.m_strBEGINTIME_DATE_RIGHT;
                #endregion
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);

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
		#endregion 

		#region 把记录从数据中“删除”。
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

			clsVeinSpecialUseDrugValue objRecordContent = (clsVeinSpecialUseDrugValue)p_objRecordContent;
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

            }
			return lngRes;
		}
		#endregion

		#region  获取数据库中最新的修改时间和首次打印时间
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
			///icuacad_veinspecialusedrug获取LastModifyDate和FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.firstprintdate,b.modifydate from icuacad_veinspecialusedrug a,
					icuacad_veinspecialusedrugcon  b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
					a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate 
					order by b.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;


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
		#endregion 

		#region 获取指定已经被删除记录的内容(用于显示在DG表格中)
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
       t1.begintime_date,
       t1.medicinename_chr,
       t1.drop_chr,
       t1.minute_chr,
       t1.ingear_chr,
       t1.abnormity_chr,
       t1.solve_chr,
       t1.remark_chr,
       t1.underwrite_chr,
       t1.checkdate_date,
       t1.begintime_datexml,
       t1.medicinename_chrxml,
       t1.drop_chrxml,
       t1.minute_chrxml,
       t1.ingear_chrxml,
       t1.abnormity_chrxml,
       t1.solve_chrxml,
       t1.remark_chrxml,
       t1.underwrite_chrxml,
       t1.id_chr,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from icuacad_veinspecialusedrug t1, icuacad_veinspecialusedrugcon t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
		
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
                    clsVeinSpecialUseDrugValue objRecordContent = new clsVeinSpecialUseDrugValue();
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

                    objRecordContent.m_strBEGINTIME_DATE = dtbValue.Rows[0]["BEGINTIME_DATE"].ToString();
                    objRecordContent.m_strBEGINTIME_DATE_RIGHT = dtbValue.Rows[0]["BEGINTIME_DATE_RIGHT"].ToString();
                    objRecordContent.m_strBEGINTIME_DATEXML = dtbValue.Rows[0]["BEGINTIME_DATEXML"].ToString();

                    objRecordContent.m_strMEDICINENAME_CHR = dtbValue.Rows[0]["MEDICINENAME_CHR"].ToString();
                    objRecordContent.m_strMEDICINENAME_CHR_RIGHT = dtbValue.Rows[0]["MEDICINENAME_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMEDICINENAME_CHRXML = dtbValue.Rows[0]["MEDICINENAME_CHRXML"].ToString();

                    objRecordContent.m_strDROP_CHR = dtbValue.Rows[0]["DROP_CHR"].ToString();
                    objRecordContent.m_strDROP_CHR_RIGHT = dtbValue.Rows[0]["DROP_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDROP_CHRXML = dtbValue.Rows[0]["DROP_CHRXML"].ToString();

                    objRecordContent.m_strMINUTE_CHR = dtbValue.Rows[0]["MINUTE_CHR"].ToString();
                    objRecordContent.m_strMINUTE_CHR_RIGHT = dtbValue.Rows[0]["MINUTE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMINUTE_CHRXML = dtbValue.Rows[0]["MINUTE_CHRXML"].ToString();

                    objRecordContent.m_strINGEAR_CHR = dtbValue.Rows[0]["INGEAR_CHR"].ToString();
                    objRecordContent.m_strINGEAR_CHR_RIGHT = dtbValue.Rows[0]["INGEAR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strINGEAR_CHRXML = dtbValue.Rows[0]["INGEAR_CHRXML"].ToString();

                    objRecordContent.m_strABNORMITY_CHR = dtbValue.Rows[0]["ABNORMITY_CHR"].ToString();
                    objRecordContent.m_strABNORMITY_CHR_RIGHT = dtbValue.Rows[0]["ABNORMITY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strABNORMITY_CHRXML = dtbValue.Rows[0]["ABNORMITY_CHRXML"].ToString();


                    objRecordContent.m_strSOLVE_CHR = dtbValue.Rows[0]["SOLVE_CHR"].ToString();
                    objRecordContent.m_strSOLVE_CHR_RIGHT = dtbValue.Rows[0]["SOLVE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strSOLVE_CHRXML = dtbValue.Rows[0]["SOLVE_CHRXML"].ToString();

                    objRecordContent.m_strREMARK_CHR = dtbValue.Rows[0]["REMARK_CHR"].ToString();
                    objRecordContent.m_strREMARK_CHR_RIGHT = dtbValue.Rows[0]["REMARK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strREMARK_CHRXML = dtbValue.Rows[0]["REMARK_CHRXML"].ToString();

                    objRecordContent.m_strUNDERWRITE_CHR = dtbValue.Rows[0]["UNDERWRITE_CHR"].ToString();
                    objRecordContent.m_strUNDERWRITE_CHR_RIGHT = dtbValue.Rows[0]["UNDERWRITE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUNDERWRITE_CHRXML = dtbValue.Rows[0]["UNDERWRITE_CHRXML"].ToString();

                    objRecordContent.m_strCHECKDATE_DATE = Convert.ToDateTime(dtbValue.Rows[0]["CHECKDATE_DATE"]).ToString("yyyy-MM-dd HH:mm:ss");


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
		#endregion 

		#region 从主表中获取所有没删除的数据
		/// <summary>
		/// 从主表中获取所有没删除的数据
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strBEFOREHAND_CHR">产期</param>
		/// <param name="p_strLAYCOUNT_CHR">产次</param>
		/// <returns></returns>
		[AutoComplete]
		public   long m_lngGetAllMainRecord(string p_strInPatientID,
			string p_strInPatientDate,
			out clsVeinSpecialUseDrugValue[] p_objResultArr)
		{			
			p_objResultArr = new clsVeinSpecialUseDrugValue[0];
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
	
			long lngRes = 0;
		
			//			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
			//lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS.clsHISMedChargeTypeSvc","m_lngGetMedChargeTypeInfo");
			if(lngRes < 0)//权限
			{
				return -1;
			}
            string c_strSQL = @"select distinct t1.inpatientid,
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
                t1.begintime_date,
                t1.medicinename_chr,
                t1.drop_chr,
                t1.minute_chr,
                t1.ingear_chr,
                t1.abnormity_chr,
                t1.solve_chr,
                t1.remark_chr,
                t1.underwrite_chr,
                t1.checkdate_date,
                t1.begintime_datexml,
                t1.medicinename_chrxml,
                t1.drop_chrxml,
                t1.minute_chrxml,
                t1.ingear_chrxml,
                t1.abnormity_chrxml,
                t1.solve_chrxml,
                t1.remark_chrxml,
                t1.underwrite_chrxml,
                t1.id_chr
  from icuacad_veinspecialusedrug t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
 order by t1.createdate";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = objHRPServ;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objResultArr = new clsVeinSpecialUseDrugValue[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region set value
                        p_objResultArr[i1] = new clsVeinSpecialUseDrugValue();

                        p_objResultArr[i1].m_strInPatientID = p_strInPatientID;
                        p_objResultArr[i1].m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                        p_objResultArr[i1].m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[i1]["CREATEDATE"].ToString());

                        p_objResultArr[i1].m_strBEGINTIME_DATE = dtbValue.Rows[i1]["BEGINTIME_DATE"].ToString();
                        p_objResultArr[i1].m_strBEGINTIME_DATEXML = dtbValue.Rows[i1]["BEGINTIME_DATEXML"].ToString();

                        p_objResultArr[i1].m_strMEDICINENAME_CHR = dtbValue.Rows[i1]["MEDICINENAME_CHR"].ToString();
                        p_objResultArr[i1].m_strMEDICINENAME_CHRXML = dtbValue.Rows[i1]["MEDICINENAME_CHRXML"].ToString();

                        p_objResultArr[i1].m_strDROP_CHR = dtbValue.Rows[i1]["DROP_CHR"].ToString();
                        p_objResultArr[i1].m_strDROP_CHRXML = dtbValue.Rows[i1]["DROP_CHRXML"].ToString();

                        p_objResultArr[i1].m_strMINUTE_CHR = dtbValue.Rows[i1]["MINUTE_CHR"].ToString();
                        p_objResultArr[i1].m_strMINUTE_CHRXML = dtbValue.Rows[i1]["MINUTE_CHRXML"].ToString();

                        p_objResultArr[i1].m_strINGEAR_CHR = dtbValue.Rows[i1]["INGEAR_CHR"].ToString();
                        p_objResultArr[i1].m_strINGEAR_CHRXML = dtbValue.Rows[i1]["INGEAR_CHRXML"].ToString();

                        p_objResultArr[i1].m_strABNORMITY_CHR = dtbValue.Rows[i1]["ABNORMITY_CHR"].ToString();
                        p_objResultArr[i1].m_strABNORMITY_CHRXML = dtbValue.Rows[i1]["ABNORMITY_CHRXML"].ToString();


                        p_objResultArr[i1].m_strSOLVE_CHR = dtbValue.Rows[i1]["SOLVE_CHR"].ToString();
                        p_objResultArr[i1].m_strSOLVE_CHRXML = dtbValue.Rows[i1]["SOLVE_CHRXML"].ToString();

                        p_objResultArr[i1].m_strREMARK_CHR = dtbValue.Rows[i1]["REMARK_CHR"].ToString();
                        p_objResultArr[i1].m_strREMARK_CHRXML = dtbValue.Rows[i1]["REMARK_CHRXML"].ToString();

                        p_objResultArr[i1].m_strUNDERWRITE_CHR = dtbValue.Rows[i1]["UNDERWRITE_CHR"].ToString();
                        p_objResultArr[i1].m_strUNDERWRITE_CHRXML = dtbValue.Rows[i1]["UNDERWRITE_CHRXML"].ToString();

                        p_objResultArr[i1].m_strCHECKDATE_DATE = Convert.ToDateTime(dtbValue.Rows[i1]["CHECKDATE_DATE"]).ToString("yyyy-MM-dd HH:mm:ss");

                        #endregion
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
		#endregion 

		#region 新建开始用药时间
		/// <summary>
		/// 新建开始用药时间
		/// </summary>
        [AutoComplete]
		public  long m_lngInsertCheckDate(
			string p_strInPatientID,
			string p_strInPatientDate,
			string p_strBeginDate,
			string p_strEndDate		,
			out string strID,
			string p_strCREATEDATE
			)
		{					
			strID = "";
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID=="")
				return (long)enmOperationResult.Parameter_Error;
			long lngRes=0;				
			
	
			string strRetID = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.m_lngGenerateNewID("ICU_AIAD_VEINSPECIALCHECKDATE", "ID_CHR", out strRetID);
                if (lngRes > 0)
                {
                    strID = strRetID;
                    string strSQLInsertCheckDate = @"insert into icu_aiad_veinspecialcheckdate
													(inpatientid,
													inpatientdate, checkdate_date,id_chr,endtime_date,createdate
													)
											values (?,?, ?,?,?,?)";

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strBeginDate);
                    objDPArr[3].Value = strRetID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(p_strEndDate);
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = DateTime.Parse(p_strCREATEDATE);

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQLInsertCheckDate, ref lngEff, objDPArr);
                }
            }
            catch
            {
                 lngRes = 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            } 
            if (lngRes <= 0) return lngRes;
			return (long)enmOperationResult.DB_Succeed;

			#region bak
//			strID = "";
//			//检查参数
//			if(p_strInPatientID==null||p_strInPatientID=="")
//				return (long)enmOperationResult.Parameter_Error;
//			long lngRes=0;				
//			long lngEff=0;
//			
//			//获取IDataParameter数组
//			string SQL = @"select max_sequence_id_chr from t_aid_table_sequence_id 
//							where lower(trim(table_name_vchr)) = lower(trim('ICU_AIAD_VEINSPECIALCHECKDATE'))
//											and lower(trim(col_name_vchr)) = lower(trim('ID_CHR'))";
//			DataTable dt = new DataTable();
//			lngRes = objHRPServ.lngGetDataTableWithoutParameters(SQL,ref dt);
//			if(lngRes>0&& dt != null)
//			{
//				string strRetID = dt.Rows[0]["max_sequence_id_chr"].ToString().Trim();
//				strID = strRetID;
//				string strSqlUpdate = @"update t_aid_table_sequence_id set MAX_SEQUENCE_ID_CHR=to_number(MAX_SEQUENCE_ID_CHR)+1 
//											where lower(trim(table_name_vchr)) = lower(trim('ICU_AIAD_VEINSPECIALCHECKDATE'))
//											and lower(trim(col_name_vchr)) = lower(trim('ID_CHR'))";
//				lngRes = objHRPServ.DoExcute(strSqlUpdate);				
//				if(lngRes>0)
//				{
//					
//					string strSQLInsertCheckDate = @"INSERT INTO icu_aiad_veinspecialcheckdate
//															(inpatientid,
//															inpatientdate, checkdate_date,ID_CHR,ENDTIME_DATE
//															)
//													VALUES ('"+p_strInPatientID+@"',
//															TO_DATE ('"+p_strInPatientDate+"', 'yyyy-mm-dd hh24:mi:ss'), TO_DATE ('"+p_strBeginDate+"', 'yyyy-mm-dd hh24:mi:ss'),'"+strRetID+"',TO_DATE ('"+p_strEndDate+"', 'yyyy-mm-dd hh24:mi:ss'))";
//
//					lngRes = objHRPServ.DoExcute(strSQLInsertCheckDate);				
//				}
//			}
//			if(lngRes <= 0)	return lngRes;
//			return (long)enmOperationResult.DB_Succeed;
			#endregion
		}
		#endregion 

		#region 更新开始用药时间与结束时间
		/// <summary>
		/// 更新开始用药时间与结束时间
		/// </summary>
		[AutoComplete]
		public  long m_lngUpdateBeginEndDate(
			string p_strBeginDate,
			string p_strEndDate	,
			string strID
			
			)
		{					
			long lngRes=0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQLInsertCheckDate = @"update  icu_aiad_veinspecialcheckdate
													set checkdate_date=?,
													endtime_date=? where id_chr=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strBeginDate);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strEndDate);
                objDPArr[2].Value = strID.Trim();

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQLInsertCheckDate, ref lngEff, objDPArr);
            }
            catch
            {
                lngRes = 0;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			if(lngRes <= 0)	return lngRes;
			return (long)enmOperationResult.DB_Succeed;
		}
		#endregion 

		#region 删除一条用药记录 
		/// <summary>
		/// 删除一条用药记录
		/// </summary>
		/// <param name="strID">ID号</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteBeginEndDate(string strID)
		{
			long lngRes=0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQLInsertCheckDate = @"update  icu_aiad_veinspecialcheckdate
													set deleted_int=1 where id_chr=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strID.Trim();

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQLInsertCheckDate, ref lngEff, objDPArr);
            }
            catch
            {
                 lngRes = 0;
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			if(lngRes <= 0)	return lngRes;
			return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 删除一条用药记录下的所有主表记录
		/// </summary>
		/// <param name="strID"></param>
		/// <param name="strDeactivOperator"></param>
		/// <param name="strDeactiveDate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteMain(string strID,string strDeactivOperator, string strDeactiveDate)
		{
			long lngRes=0;	
			if(strDeactivOperator == null || strDeactivOperator == string.Empty 
				|| strDeactiveDate == null || strDeactiveDate == string.Empty
				|| strID == null || strID==string.Empty)
				return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(strDeactiveDate);
                objDPArr[1].Value = strDeactivOperator.Trim();
                objDPArr[2].Value = strID.Trim();

                string strSQLInsertCheckDate = @"update  icuacad_veinspecialusedrug
													set status=1 ,deactiveddate=?,deactivedoperatorid=? where id_chr=?";

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQLInsertCheckDate, ref lngEff, objDPArr);
            }
            catch
            {
                //objHRPServ.Dispose();
                lngRes = 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            } 
            if (lngRes <= 0) return lngRes;
			return (long)enmOperationResult.DB_Succeed;
		}
		#endregion
	}
}
