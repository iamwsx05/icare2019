

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
	/// 候产记录(添加，修改)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsWaitLayRecord_AcadService : clsDiseaseTrackService
	{
		#region constructor
		public clsWaitLayRecord_AcadService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion

		#region SQL语句
		/// <summary>
		/// 从IcuAcad_WaitLayRecord获取指定病人的所有没有删除记录的时间。
		/// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
		/// </summary>
		private const string c_strGetTimeListSQL= @"select createdate, opendate
														from icuacad_waitlayrecord
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

		/// <summary>
		/// 从IcuAcad_WaitLayRecord中获取指定时间的表单,获取已经存在记录的主要信息
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
		private const string c_strCheckCreateDateSQL= @"select createuserid, opendate
															from icuacad_waitlayrecord
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

		/// <summary>
		/// 从IcuAcad_WaitLayRecord获取删除表单的主要信息。
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate, deactivedoperatorid
															from icuacad_waitlayrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		/// <summary>
		/// 添加记录到IcuAcad_WaitLayRecord
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into icuacad_waitlayrecord (inpatientid,inpatientdate,opendate,
						createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,laycount_chr,laycount_chrxml,beforehand_chr,beforehand_chrxml,recorddate_chr,recorddate_chrxml,
						time_chr,time_chrxml,bloodpressure_chr,bloodpressure_chrxml,embryolocation_chr,embryolocation_chrxml,embryoheart_chr,embryoheart_chrxml,
						intermission_chr,intermission_chrxml,persist_chr,persist_chrxml,intensity_chr,intensity_chrxml,palacemouth_chr,palacemouth_chrxml,show_chr,show_chrxml,
						caul_chr,caul_chrxml,anuscheck_chr,anuscheck_chrxml,other_chr,other_chrxml,scrutator_chr,scrutator_chrxml,bloodpressure2_chr,bloodpressure2_chrxml,recorddate) 
						values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 添加记录到IcuAcad_WaitLayContent
		/// </summary>
		private const string c_strAddNewRecordContentSQL=  @"insert into icuacad_waitlaycontent (inpatientid,inpatientdate,
						opendate,modifydate,modifyuserid,laycount_chr_right,beforehand_chr_right,recorddate_chr_right,time_chr_right,
						bloodpressure_chr_right,embryolocation_chr_right,embryoheart_chr_right,intermission_chr_right,persist_chr_right,intensity_chr_right,
						palacemouth_chr_right,show_chr_right,caul_chr_right,anuscheck_chr_right,other_chr_right,scrutator_chr_right,bloodpressure2_chr_right) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 修改记录到IcuAcad_WaitLayRecord
		/// </summary>
		private const string c_strModifyRecordSQL= @"update icuacad_waitlayrecord 
			set laycount_chr=?,laycount_chrxml=?,beforehand_chr=?,beforehand_chrxml=?,recorddate_chr=?,
            recorddate_chrxml=?,time_chr=?,time_chrxml=?,bloodpressure_chr=?,bloodpressure_chrxml=?,
		embryolocation_chr=?,embryolocation_chrxml=?,embryoheart_chr=?,embryoheart_chrxml=?,intermission_chr=?,
		intermission_chrxml=?,persist_chr=?,persist_chrxml=?,intensity_chr=?,intensity_chrxml=?,
palacemouth_chr=?,palacemouth_chrxml=?,show_chr=?,show_chrxml=?,caul_chr=?,
		caul_chrxml=?,anuscheck_chr=?,anuscheck_chrxml=?,other_chr=?,other_chrxml=?,
scrutator_chr=?,scrutator_chrxml=? ,bloodpressure2_chr=?,bloodpressure2_chrxml=?
			where inpatientid=? and inpatientdate=? and status=0 and createdate=?";

		/// <summary>
		/// 修改记录到IcuAcad_WaitLayRecord
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// 设置IcuAcad_WaitLayRecord中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= @"update icuacad_waitlayrecord
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

		/// <summary>
		/// 更新IcuAcad_WaitLayRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update icuacad_waitlayrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// 更新IcuAcad_WaitLayRecord中FirstPrintDate
		/// </summary>
		private const string c_strUpdateALLFirstPrintDateSQL= @"update icuacad_waitlayrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?																
																and firstprintdate is null
																and status = 0";

		/// <summary>
		/// 从IcuAcad_WaitLayRecord获取指定病人的所有指定删除者删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListSQL= @"select createdate, opendate
																	from icuacad_waitlayrecord
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

		/// <summary>
		/// 从IcuAcad_WaitLayRecord获取指定病人的所有已经删除的记录时间。
		/// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
		/// </summary>
		private const string c_strGetDeleteRecordTimeListAllSQL= @"select createdate, opendate
																		from icuacad_waitlayrecord
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadService","m_lngGetRecordTimeList");
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

            } return lngRes;
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
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadService","m_lngUpdateFirstPrintDate");
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadService","m_lngGetDeleteRecordTimeList");
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

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadService","m_lngGetDeleteRecordTimeListAll");
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
       t1.laycount_chr,
       t1.beforehand_chr,
       t1.recorddate_chr,
       t1.time_chr,
       t1.bloodpressure_chr,
       t1.embryolocation_chr,
       t1.embryoheart_chr,
       t1.intermission_chr,
       t1.persist_chr,
       t1.intensity_chr,
       t1.palacemouth_chr,
       t1.show_chr,
       t1.caul_chr,
       t1.anuscheck_chr,
       t1.other_chr,
       t1.scrutator_chr,
       t1.laycount_chrxml,
       t1.beforehand_chrxml,
       t1.recorddate_chrxml,
       t1.time_chrxml,
       t1.bloodpressure_chrxml,
       t1.embryolocation_chrxml,
       t1.embryoheart_chrxml,
       t1.intermission_chrxml,
       t1.persist_chrxml,
       t1.palacemouth_chrxml,
       t1.show_chrxml,
       t1.caul_chrxml,
       t1.anuscheck_chrxml,
       t1.other_chrxml,
       t1.scrutator_chrxml,
       t1.intensity_chrxml,
       t1.bloodpressure2_chr,
       t1.bloodpressure2_chrxml,
        t1.recorddate,
       t2.modifydate,
       t2.modifyuserid,
       t2.laycount_chr_right,
       t2.beforehand_chr_right,
       t2.recorddate_chr_right,
       t2.time_chr_right,
       t2.bloodpressure_chr_right,
       t2.embryolocation_chr_right,
       t2.embryoheart_chr_right,
       t2.intermission_chr_right,
       t2.persist_chr_right,
       t2.intensity_chr_right,
       t2.palacemouth_chr_right,
       t2.show_chr_right,
       t2.caul_chr_right,
       t2.anuscheck_chr_right,
       t2.other_chr_right,
       t2.scrutator_chr_right,
       t2.bloodpressure2_chr_right
  from icuacad_waitlayrecord t1
  join icuacad_waitlaycontent t2 on (t1.inpatientid = t2.inpatientid and
                                    t1.inpatientdate = t2.inpatientdate and
                                    t1.opendate = t2.opendate and
                                    t1.status = 0 and t1.inpatientid = ? and
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
                    clsIcuAcad_WaitLayRecord objRecordContent = new clsIcuAcad_WaitLayRecord();
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


                    objRecordContent.m_strLayCount_chr = dtbValue.Rows[0]["LAYCOUNT_CHR"].ToString();
                    objRecordContent.m_strLayCount_chr_RIGHT = dtbValue.Rows[0]["LAYCOUNT_CHR_RIGHT"].ToString();
                    objRecordContent.m_strLayCount_chrXML = dtbValue.Rows[0]["LAYCOUNT_CHRXML"].ToString();

                    objRecordContent.m_strBeforehand_chr = dtbValue.Rows[0]["BEFOREHAND_CHR"].ToString();
                    objRecordContent.m_strBeforehand_chr_RIGHT = dtbValue.Rows[0]["BEFOREHAND_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBeforehand_chrXML = dtbValue.Rows[0]["BEFOREHAND_CHRXML"].ToString();

                    objRecordContent.m_strRecordDate_chr = dtbValue.Rows[0]["RECORDDATE_CHR"].ToString();
                    objRecordContent.m_strRecordDate_chr_RIGHT = dtbValue.Rows[0]["RECORDDATE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strRecordDate_chrXML = dtbValue.Rows[0]["RECORDDATE_CHRXML"].ToString();

                    objRecordContent.m_strTime_chr = dtbValue.Rows[0]["TIME_CHR"].ToString();
                    objRecordContent.m_strTime_chr_RIGHT = dtbValue.Rows[0]["TIME_CHR_RIGHT"].ToString();
                    objRecordContent.m_strTime_chrXML = dtbValue.Rows[0]["TIME_CHRXML"].ToString();

                    objRecordContent.m_strBloodPressure_chr = dtbValue.Rows[0]["BLOODPRESSURE_CHR"].ToString();
                    objRecordContent.m_strBloodPressure_chr_RIGHT = dtbValue.Rows[0]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressure_chrXML = dtbValue.Rows[0]["BLOODPRESSURE_CHRXML"].ToString();

                    objRecordContent.m_strBloodPressure2_chr = dtbValue.Rows[0]["BLOODPRESSURE2_CHR"].ToString();
                    objRecordContent.m_strBloodPressure2_chrXML = dtbValue.Rows[0]["BLOODPRESSURE2_CHRXML"].ToString();
                    objRecordContent.m_strBloodPressure2_chr_RIGHT = dtbValue.Rows[0]["BLOODPRESSURE2_CHR_RIGHT"].ToString();


                    objRecordContent.m_strEmbryoLocation_chr = dtbValue.Rows[0]["EMBRYOLOCATION_CHR"].ToString();
                    objRecordContent.m_strEmbryoLocation_chr_RIGHT = dtbValue.Rows[0]["EMBRYOLOCATION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEmbryoLocation_chrXML = dtbValue.Rows[0]["EMBRYOLOCATION_CHRXML"].ToString();


                    objRecordContent.m_strEmbryoHeart_chr = dtbValue.Rows[0]["EMBRYOHEART_CHR"].ToString();
                    objRecordContent.m_strEmbryoHeart_chr_RIGHT = dtbValue.Rows[0]["EMBRYOHEART_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEmbryoHeart_chrXML = dtbValue.Rows[0]["EMBRYOHEART_CHRXML"].ToString();

                    objRecordContent.m_strIntermission_chr = dtbValue.Rows[0]["INTERMISSION_CHR"].ToString();
                    objRecordContent.m_strIntermission_chr_RIGHT = dtbValue.Rows[0]["INTERMISSION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strIntermission_chrXML = dtbValue.Rows[0]["INTERMISSION_CHRXML"].ToString();

                    objRecordContent.m_strPersist_chr = dtbValue.Rows[0]["PERSIST_CHR"].ToString();
                    objRecordContent.m_strPersist_chr_RIGHT = dtbValue.Rows[0]["PERSIST_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPersist_chrXML = dtbValue.Rows[0]["PERSIST_CHRXML"].ToString();

                    objRecordContent.m_strIntensity_chr = dtbValue.Rows[0]["INTENSITY_CHR"].ToString();
                    objRecordContent.m_strIntensity_RIGHT = dtbValue.Rows[0]["INTENSITY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strIntensity_chrXML = dtbValue.Rows[0]["INTENSITY_CHRXML"].ToString();

                    objRecordContent.m_strPalaceMouth_chr = dtbValue.Rows[0]["PALACEMOUTH_CHR"].ToString();
                    objRecordContent.m_strPalaceMouth_chr_RIGHT = dtbValue.Rows[0]["PALACEMOUTH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPalaceMouth_chrXML = dtbValue.Rows[0]["PALACEMOUTH_CHRXML"].ToString();

                    objRecordContent.m_strShow_chr = dtbValue.Rows[0]["SHOW_CHR"].ToString();
                    objRecordContent.m_strShow_chr_RIGHT = dtbValue.Rows[0]["SHOW_CHR_RIGHT"].ToString();
                    objRecordContent.m_strShow_chrXML = dtbValue.Rows[0]["SHOW_CHRXML"].ToString();

                    objRecordContent.m_strCaul_chr = dtbValue.Rows[0]["CAUL_CHR"].ToString();
                    objRecordContent.m_strCaul_chr_RIGHT = dtbValue.Rows[0]["CAUL_CHR_RIGHT"].ToString();
                    objRecordContent.m_strCaul_chrXML = dtbValue.Rows[0]["CAUL_CHRXML"].ToString();

                    objRecordContent.m_strAnusCheck_chr = dtbValue.Rows[0]["ANUSCHECK_CHR"].ToString();
                    objRecordContent.m_strAnusCheck_chr_RIGHT = dtbValue.Rows[0]["ANUSCHECK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strAnusCheck_chrXML = dtbValue.Rows[0]["ANUSCHECK_CHRXML"].ToString();

                    objRecordContent.m_strOther_chr = dtbValue.Rows[0]["OTHER_CHR"].ToString();
                    objRecordContent.m_strOther_chr_RIGHT = dtbValue.Rows[0]["OTHER_CHR_RIGHT"].ToString();
                    objRecordContent.m_strOther_chrXML = dtbValue.Rows[0]["OTHER_CHRXML"].ToString();

                    objRecordContent.m_strScrutator_chr = dtbValue.Rows[0]["SCRUTATOR_CHR"].ToString();
                    objRecordContent.m_strScrutator_chr_RIGHT = dtbValue.Rows[0]["SCRUTATOR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strScrutator_chrXML = dtbValue.Rows[0]["SCRUTATOR_CHRXML"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["recorddate"]);



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

            }
			return lngRes;
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
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsIcuAcad_WaitLayRecord objRecordContent = (clsIcuAcad_WaitLayRecord)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 付值
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(44, out objDPArr);
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


                objDPArr[9].Value = objRecordContent.m_strLayCount_chr;
                objDPArr[10].Value = objRecordContent.m_strLayCount_chrXML;
                objDPArr[11].Value = objRecordContent.m_strBeforehand_chr;
                objDPArr[12].Value = objRecordContent.m_strBeforehand_chrXML;
                objDPArr[13].Value = objRecordContent.m_strRecordDate_chr;
                objDPArr[14].Value = objRecordContent.m_strRecordDate_chrXML;
                objDPArr[15].Value = objRecordContent.m_strTime_chr;
                objDPArr[16].Value = objRecordContent.m_strTime_chrXML;
                objDPArr[17].Value = objRecordContent.m_strBloodPressure_chr;
                objDPArr[18].Value = objRecordContent.m_strBloodPressure_chrXML;
                objDPArr[19].Value = objRecordContent.m_strEmbryoLocation_chr;
                objDPArr[20].Value = objRecordContent.m_strEmbryoLocation_chrXML;

                objDPArr[21].Value = objRecordContent.m_strEmbryoHeart_chr;
                objDPArr[22].Value = objRecordContent.m_strEmbryoHeart_chrXML;
                objDPArr[23].Value = objRecordContent.m_strIntermission_chr;
                objDPArr[24].Value = objRecordContent.m_strIntermission_chrXML;

                objDPArr[25].Value = objRecordContent.m_strPersist_chr;
                objDPArr[26].Value = objRecordContent.m_strPersist_chrXML;
                objDPArr[27].Value = objRecordContent.m_strIntensity_chr;
                objDPArr[28].Value = objRecordContent.m_strIntensity_chrXML;

                objDPArr[29].Value = objRecordContent.m_strPalaceMouth_chr;
                objDPArr[30].Value = objRecordContent.m_strPalaceMouth_chrXML;
                objDPArr[31].Value = objRecordContent.m_strShow_chr;
                objDPArr[32].Value = objRecordContent.m_strShow_chrXML;
                objDPArr[33].Value = objRecordContent.m_strCaul_chr;
                objDPArr[34].Value = objRecordContent.m_strCaul_chrXML;

                objDPArr[35].Value = objRecordContent.m_strAnusCheck_chr;
                objDPArr[36].Value = objRecordContent.m_strAnusCheck_chrXML;


                objDPArr[37].Value = objRecordContent.m_strOther_chr;
                objDPArr[38].Value = objRecordContent.m_strOther_chrXML;

                objDPArr[39].Value = objRecordContent.m_strScrutator_chr;
                objDPArr[40].Value = objRecordContent.m_strScrutator_chrXML;

                objDPArr[41].Value = objRecordContent.m_strBloodPressure2_chr;
                objDPArr[42].Value = objRecordContent.m_strBloodPressure2_chrXML;
                objDPArr[43].DbType = DbType.DateTime;
                //objDPArr[43].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[43].Value = objRecordContent.m_dtmCreateDate;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;




                #region 更新IcuAcad_WaitLayRecord中产次与产期

                string c_strUpdateLaycountSQL = @"update icuacad_waitlayrecord
																set laycount_chr = ?,beforehand_chr=?
															where inpatientid = ?
																and inpatientdate = ?	
																and status = 0";
                IDataParameter[] objDPArr3 = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
                objDPArr3[0].Value = objRecordContent.m_strLayCount_chr;
                objDPArr3[1].Value = objRecordContent.m_strBeforehand_chr;
                objDPArr3[2].Value = objRecordContent.m_strInPatientID;
                objDPArr3[3].DbType = DbType.DateTime;
                objDPArr3[3].Value = objRecordContent.m_dtmInPatientDate;

                //执行SQL
                long lngEff3 = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateLaycountSQL, ref lngEff3, objDPArr3);
                if (lngRes <= 0) return lngRes;

                #endregion

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(22, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[5].Value = objRecordContent.m_strLayCount_chr_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strBeforehand_chr_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strRecordDate_chr_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strTime_chr_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strBloodPressure_chr_RIGHT;

                objDPArr2[10].Value = objRecordContent.m_strEmbryoLocation_chr_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strEmbryoHeart_chr_RIGHT;

                objDPArr2[12].Value = objRecordContent.m_strIntermission_chr_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strPersist_chr_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strIntensity_RIGHT;
                objDPArr2[15].Value = objRecordContent.m_strPalaceMouth_chr_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strShow_chr_RIGHT;

                objDPArr2[17].Value = objRecordContent.m_strCaul_chr_RIGHT;
                objDPArr2[18].Value = objRecordContent.m_strAnusCheck_chr_RIGHT;
                objDPArr2[19].Value = objRecordContent.m_strOther_chr_RIGHT;
                objDPArr2[20].Value = objRecordContent.m_strScrutator_chr_RIGHT;
                objDPArr2[21].Value = objRecordContent.m_strBloodPressure2_chr_RIGHT;

                #endregion
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

			clsIcuAcad_WaitLayRecord objRecordContent = (clsIcuAcad_WaitLayRecord)p_objRecordContent;
			/// <summary>
			/// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
			/// </summary>
			string c_strCheckLastModifyRecordSQL= clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from icuacad_waitlayrecord t1,icuacad_waitlaycontent t2
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
		
			clsIcuAcad_WaitLayRecord objRecordContent = (clsIcuAcad_WaitLayRecord)p_objRecordContent;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region set value
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(37, out objDPArr);

                objDPArr[0].Value = objRecordContent.m_strLayCount_chr;
                objDPArr[1].Value = objRecordContent.m_strLayCount_chrXML;
                objDPArr[2].Value = objRecordContent.m_strBeforehand_chr;
                objDPArr[3].Value = objRecordContent.m_strBeforehand_chrXML;
                objDPArr[4].Value = objRecordContent.m_strRecordDate_chr == null ? "" : objRecordContent.m_strRecordDate_chr;
                objDPArr[5].Value = objRecordContent.m_strRecordDate_chrXML == null ? "" : objRecordContent.m_strRecordDate_chrXML;
                objDPArr[6].Value = objRecordContent.m_strTime_chr == null ? "" : objRecordContent.m_strTime_chr;
                objDPArr[7].Value = objRecordContent.m_strTime_chrXML == null ? "" : objRecordContent.m_strTime_chrXML;
                objDPArr[8].Value = objRecordContent.m_strBloodPressure_chr;
                objDPArr[9].Value = objRecordContent.m_strBloodPressure_chrXML;
                objDPArr[10].Value = objRecordContent.m_strEmbryoLocation_chr;
                objDPArr[11].Value = objRecordContent.m_strEmbryoLocation_chrXML;

                objDPArr[12].Value = objRecordContent.m_strEmbryoHeart_chr;
                objDPArr[13].Value = objRecordContent.m_strEmbryoHeart_chrXML;
                objDPArr[14].Value = objRecordContent.m_strIntermission_chr;
                objDPArr[15].Value = objRecordContent.m_strIntermission_chrXML;

                objDPArr[16].Value = objRecordContent.m_strPersist_chr;
                objDPArr[17].Value = objRecordContent.m_strPersist_chrXML;
                objDPArr[18].Value = objRecordContent.m_strIntensity_chr;
                objDPArr[19].Value = objRecordContent.m_strIntensity_chrXML;

                objDPArr[20].Value = objRecordContent.m_strPalaceMouth_chr;
                objDPArr[21].Value = objRecordContent.m_strPalaceMouth_chrXML;
                objDPArr[22].Value = objRecordContent.m_strShow_chr;
                objDPArr[23].Value = objRecordContent.m_strShow_chrXML;
                objDPArr[24].Value = objRecordContent.m_strCaul_chr;
                objDPArr[25].Value = objRecordContent.m_strCaul_chrXML;

                objDPArr[26].Value = objRecordContent.m_strAnusCheck_chr;
                objDPArr[27].Value = objRecordContent.m_strAnusCheck_chrXML;


                objDPArr[28].Value = objRecordContent.m_strOther_chr;
                objDPArr[29].Value = objRecordContent.m_strOther_chrXML;

                objDPArr[30].Value = objRecordContent.m_strScrutator_chr;
                objDPArr[31].Value = objRecordContent.m_strScrutator_chrXML;

                objDPArr[32].Value = objRecordContent.m_strBloodPressure2_chr;
                objDPArr[33].Value = objRecordContent.m_strBloodPressure2_chrXML;
                //objDPArr[34].DbType = DbType.DateTime;
                //objDPArr[34].Value = p_objRecordContent.m_dtmRecordDate;

                objDPArr[34].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[35].DbType = DbType.DateTime;
                objDPArr[35].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[36].DbType = DbType.DateTime;
                objDPArr[36].Value = p_objRecordContent.m_dtmCreateDate;

                #endregion
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;


                #region 更新IcuAcad_WaitLayRecord中产次与产期

                string c_strUpdateLaycountSQL = @"update icuacad_waitlayrecord
																set laycount_chr = ?,beforehand_chr=?
															where inpatientid = ?
																and inpatientdate = ?	
																and status = 0";
                IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
                objDPArr3[0].Value = objRecordContent.m_strLayCount_chr;
                objDPArr3[1].Value = objRecordContent.m_strBeforehand_chr;
                objDPArr3[2].Value = objRecordContent.m_strInPatientID;
                objDPArr3[3].DbType = DbType.DateTime;
                objDPArr3[3].Value = objRecordContent.m_dtmInPatientDate;

                //执行SQL
                long lngEff3 = 0;
                long lngRes3 = objHRPServ.lngExecuteParameterSQL(c_strUpdateLaycountSQL, ref lngEff3, objDPArr3);
                if (lngRes3 <= 0) return lngRes3;

                #endregion

                #region set value
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(22, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objRecordContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[5].Value = objRecordContent.m_strLayCount_chr_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strBeforehand_chr_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strRecordDate_chr_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strTime_chr_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strBloodPressure_chr_RIGHT;

                objDPArr2[10].Value = objRecordContent.m_strEmbryoLocation_chr_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strEmbryoHeart_chr_RIGHT;

                objDPArr2[12].Value = objRecordContent.m_strIntermission_chr_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strPersist_chr_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strIntensity_RIGHT;
                objDPArr2[15].Value = objRecordContent.m_strPalaceMouth_chr_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strShow_chr_RIGHT;
                objDPArr2[17].Value = objRecordContent.m_strCaul_chr_RIGHT;
                objDPArr2[18].Value = objRecordContent.m_strAnusCheck_chr_RIGHT;
                objDPArr2[19].Value = objRecordContent.m_strOther_chr_RIGHT;
                objDPArr2[20].Value = objRecordContent.m_strScrutator_chr_RIGHT;
                objDPArr2[21].Value = objRecordContent.m_strBloodPressure2_chr_RIGHT;
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

			clsIcuAcad_WaitLayRecord objRecordContent = (clsIcuAcad_WaitLayRecord)p_objRecordContent;
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
			/// 从IcuAcad_WaitLayRecord和IcuAcad_WaitLayContent获取LastModifyDate和FirstPrintDate
			/// </summary>
			string c_strGetModifyDateAndFirstPrintDateSQL=  clsDatabaseSQLConvert.s_StrTop1+@" a.firstprintdate,b.modifydate from icuacad_waitlayrecord a,
					icuacad_waitlaycontent b where inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and 
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
       t1.laycount_chr,
       t1.beforehand_chr,
       t1.recorddate_chr,
       t1.time_chr,
       t1.bloodpressure_chr,
       t1.embryolocation_chr,
       t1.embryoheart_chr,
       t1.intermission_chr,
       t1.persist_chr,
       t1.intensity_chr,
       t1.palacemouth_chr,
       t1.show_chr,
       t1.caul_chr,
       t1.anuscheck_chr,
       t1.other_chr,
       t1.scrutator_chr,
       t1.laycount_chrxml,
       t1.beforehand_chrxml,
       t1.recorddate_chrxml,
       t1.time_chrxml,
       t1.bloodpressure_chrxml,
       t1.embryolocation_chrxml,
       t1.embryoheart_chrxml,
       t1.intermission_chrxml,
       t1.persist_chrxml,
       t1.palacemouth_chrxml,
       t1.show_chrxml,
       t1.caul_chrxml,
       t1.anuscheck_chrxml,
       t1.other_chrxml,
       t1.scrutator_chrxml,
       t1.intensity_chrxml,
       t1.bloodpressure2_chr,
       t1.bloodpressure2_chrxml,
        t1.recorddate,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from icuacad_waitlayrecord t1, icuacad_waitlaycontent t2
 where t1.inpatientid = t2.inpatientid
   and t1.inpatientdate = t2.inpatientdate
   and t1.opendate = t2.opendate
   and t1.status = 1
   and t1.inpatientid = ?
   and t1.inpatientdate =?
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
                    clsIcuAcad_WaitLayRecord objRecordContent = new clsIcuAcad_WaitLayRecord();
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


                    objRecordContent.m_strLayCount_chr = dtbValue.Rows[0]["LAYCOUNT_CHR"].ToString();
                    objRecordContent.m_strLayCount_chr_RIGHT = dtbValue.Rows[0]["LAYCOUNT_CHR_RIGHT"].ToString();
                    objRecordContent.m_strLayCount_chrXML = dtbValue.Rows[0]["LAYCOUNT_CHRXML"].ToString();

                    objRecordContent.m_strBeforehand_chr = dtbValue.Rows[0]["BEFOREHAND_CHR"].ToString();
                    objRecordContent.m_strBeforehand_chr_RIGHT = dtbValue.Rows[0]["BEFOREHAND_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBeforehand_chrXML = dtbValue.Rows[0]["BEFOREHAND_CHRXML"].ToString();

                    objRecordContent.m_strRecordDate_chr = dtbValue.Rows[0]["RECORDDATE_CHR"].ToString();
                    objRecordContent.m_strRecordDate_chr_RIGHT = dtbValue.Rows[0]["RECORDDATE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strRecordDate_chrXML = dtbValue.Rows[0]["RECORDDATE_CHRXML"].ToString();

                    objRecordContent.m_strTime_chr = dtbValue.Rows[0]["TIME_CHR"].ToString();
                    objRecordContent.m_strTime_chr_RIGHT = dtbValue.Rows[0]["TIME_CHR_RIGHT"].ToString();
                    objRecordContent.m_strTime_chrXML = dtbValue.Rows[0]["TIME_CHRXML"].ToString();

                    objRecordContent.m_strBloodPressure_chr = dtbValue.Rows[0]["BLOODPRESSURE_CHR"].ToString();
                    objRecordContent.m_strBloodPressure_chr_RIGHT = dtbValue.Rows[0]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressure_chrXML = dtbValue.Rows[0]["BLOODPRESSURE_CHRXML"].ToString();

                    objRecordContent.m_strBloodPressure2_chr = dtbValue.Rows[0]["BLOODPRESSURE2_CHR"].ToString();
                    objRecordContent.m_strBloodPressure2_chr_RIGHT = dtbValue.Rows[0]["BLOODPRESSURE2_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBloodPressure2_chrXML = dtbValue.Rows[0]["BLOODPRESSURE2_CHRXML"].ToString();

                    objRecordContent.m_strEmbryoLocation_chr = dtbValue.Rows[0]["EMBRYOLOCATION_CHR"].ToString();
                    objRecordContent.m_strEmbryoLocation_chr_RIGHT = dtbValue.Rows[0]["EMBRYOLOCATION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEmbryoLocation_chrXML = dtbValue.Rows[0]["EMBRYOLOCATION_CHRXML"].ToString();


                    objRecordContent.m_strEmbryoHeart_chr = dtbValue.Rows[0]["EMBRYOHEART_CHR"].ToString();
                    objRecordContent.m_strEmbryoHeart_chr_RIGHT = dtbValue.Rows[0]["EMBRYOHEART_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEmbryoHeart_chrXML = dtbValue.Rows[0]["EMBRYOHEART_CHRXML"].ToString();

                    objRecordContent.m_strIntermission_chr = dtbValue.Rows[0]["INTERMISSION_CHR"].ToString();
                    objRecordContent.m_strIntermission_chr_RIGHT = dtbValue.Rows[0]["INTERMISSION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strIntermission_chrXML = dtbValue.Rows[0]["INTERMISSION_CHRXML"].ToString();

                    objRecordContent.m_strPersist_chr = dtbValue.Rows[0]["PERSIST_CHR"].ToString();
                    objRecordContent.m_strPersist_chr_RIGHT = dtbValue.Rows[0]["PERSIST_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPersist_chrXML = dtbValue.Rows[0]["PERSIST_CHRXML"].ToString();

                    objRecordContent.m_strIntensity_chr = dtbValue.Rows[0]["INTENSITY_CHR"].ToString();
                    objRecordContent.m_strIntensity_RIGHT = dtbValue.Rows[0]["INTENSITY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strIntensity_chrXML = dtbValue.Rows[0]["INTENSITY_CHRXML"].ToString();

                    objRecordContent.m_strPalaceMouth_chr = dtbValue.Rows[0]["PALACEMOUTH_CHR"].ToString();
                    objRecordContent.m_strPalaceMouth_chr_RIGHT = dtbValue.Rows[0]["PALACEMOUTH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPalaceMouth_chrXML = dtbValue.Rows[0]["PALACEMOUTH_CHRXML"].ToString();

                    objRecordContent.m_strShow_chr = dtbValue.Rows[0]["SHOW_CHR"].ToString();
                    objRecordContent.m_strShow_chr_RIGHT = dtbValue.Rows[0]["SHOW_CHR_RIGHT"].ToString();
                    objRecordContent.m_strShow_chrXML = dtbValue.Rows[0]["SHOW_CHRXML"].ToString();

                    objRecordContent.m_strCaul_chr = dtbValue.Rows[0]["CAUL_CHR"].ToString();
                    objRecordContent.m_strCaul_chr_RIGHT = dtbValue.Rows[0]["CAUL_CHR_RIGHT"].ToString();
                    objRecordContent.m_strCaul_chrXML = dtbValue.Rows[0]["CAUL_CHRXML"].ToString();

                    objRecordContent.m_strAnusCheck_chr = dtbValue.Rows[0]["ANUSCHECK_CHR"].ToString();
                    objRecordContent.m_strAnusCheck_chr_RIGHT = dtbValue.Rows[0]["ANUSCHECK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strAnusCheck_chrXML = dtbValue.Rows[0]["ANUSCHECK_CHRXML"].ToString();

                    objRecordContent.m_strOther_chr = dtbValue.Rows[0]["OTHER_CHR"].ToString();
                    objRecordContent.m_strOther_chr_RIGHT = dtbValue.Rows[0]["OTHER_CHR_RIGHT"].ToString();
                    objRecordContent.m_strOther_chrXML = dtbValue.Rows[0]["OTHER_CHRXML"].ToString();

                    objRecordContent.m_strScrutator_chr = dtbValue.Rows[0]["SCRUTATOR_CHR"].ToString();
                    objRecordContent.m_strScrutator_chr_RIGHT = dtbValue.Rows[0]["SCRUTATOR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strScrutator_chrXML = dtbValue.Rows[0]["SCRUTATOR_CHRXML"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["recorddate"]);

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

		#region 从主表中获取产期与产次
		/// <summary>
		/// 从主表中获取产期与产次
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strBEFOREHAND_CHR">产期</param>
		/// <param name="p_strLAYCOUNT_CHR">产次</param>
		/// <returns></returns>
		[AutoComplete]
		public   long m_lngGetBEFOREHAND_CHR_LAYCOUNT_CHR(string p_strInPatientID,
			string p_strInPatientDate,
			out string p_strBEFOREHAND_CHR,
			out string p_strLAYCOUNT_CHR)
		{			
			
			p_strBEFOREHAND_CHR = "";
			p_strLAYCOUNT_CHR = "";
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

			string c_strSQL=  @"select distinct t1.laycount_chr,t1.beforehand_chr															
														from icuacad_waitlayrecord t1,
															icuacad_waitlaycontent t3,
															t_bse_employee             t4
														where t1.inpatientid = t3.inpatientid
														and t1.inpatientdate = t3.inpatientdate
														and t1.opendate = t3.opendate
														and t1.status =0
														and t1.inpatientid = ?
														and t1.inpatientdate = ?
														and t1.createuserid = t4.empno_chr
														order by t1.createdate";


			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                clsHRPTableService p_objHRPServ = objHRPServ;
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    //设置结果
                    p_strLAYCOUNT_CHR = dtbValue.Rows[0]["LAYCOUNT_CHR"].ToString().Trim();
                    p_strBEFOREHAND_CHR = dtbValue.Rows[0]["BEFOREHAND_CHR"].ToString().Trim();
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
			out clsIcuAcad_WaitLayRecord[] p_objResultArr)
		{			
			p_objResultArr = new clsIcuAcad_WaitLayRecord[0];
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
                t1.laycount_chr,
                t1.beforehand_chr,
                t1.recorddate_chr,
                t1.time_chr,
                t1.bloodpressure_chr,
                t1.embryolocation_chr,
                t1.embryoheart_chr,
                t1.intermission_chr,
                t1.persist_chr,
                t1.intensity_chr,
                t1.palacemouth_chr,
                t1.show_chr,
                t1.caul_chr,
                t1.anuscheck_chr,
                t1.other_chr,
                t1.scrutator_chr,
                t1.laycount_chrxml,
                t1.beforehand_chrxml,
                t1.recorddate_chrxml,
                t1.time_chrxml,
                t1.bloodpressure_chrxml,
                t1.embryolocation_chrxml,
                t1.embryoheart_chrxml,
                t1.intermission_chrxml,
                t1.persist_chrxml,
                t1.palacemouth_chrxml,
                t1.show_chrxml,
                t1.caul_chrxml,
                t1.anuscheck_chrxml,
                t1.other_chrxml,
                t1.scrutator_chrxml,
                t1.intensity_chrxml,
                t1.bloodpressure2_chr,
                t1.bloodpressure2_chrxml,
t1.recorddate
  from icuacad_waitlayrecord t1, t_bse_employee t4
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
                    p_objResultArr = new clsIcuAcad_WaitLayRecord[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        #region set value
                        p_objResultArr[i1] = new clsIcuAcad_WaitLayRecord();

                        p_objResultArr[i1].m_strInPatientID = p_strInPatientID;
                        p_objResultArr[i1].m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                        p_objResultArr[i1].m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[i1]["CREATEDATE"].ToString());

                        p_objResultArr[i1].m_strLayCount_chr = dtbValue.Rows[i1]["LAYCOUNT_CHR"].ToString();

                        p_objResultArr[i1].m_strBeforehand_chr = dtbValue.Rows[i1]["BEFOREHAND_CHR"].ToString();

                        p_objResultArr[i1].m_strRecordDate_chr = dtbValue.Rows[i1]["RECORDDATE_CHR"].ToString();

                        p_objResultArr[i1].m_strTime_chr = dtbValue.Rows[i1]["TIME_CHR"].ToString();

                        p_objResultArr[i1].m_strBloodPressure_chr = dtbValue.Rows[i1]["BLOODPRESSURE_CHR"].ToString();

                        p_objResultArr[i1].m_strBloodPressure2_chr = dtbValue.Rows[i1]["BLOODPRESSURE2_CHR"].ToString();

                        p_objResultArr[i1].m_strEmbryoLocation_chr = dtbValue.Rows[i1]["EMBRYOLOCATION_CHR"].ToString();

                        p_objResultArr[i1].m_strEmbryoHeart_chr = dtbValue.Rows[i1]["EMBRYOHEART_CHR"].ToString();

                        p_objResultArr[i1].m_strIntermission_chr = dtbValue.Rows[i1]["INTERMISSION_CHR"].ToString();

                        p_objResultArr[i1].m_strPersist_chr = dtbValue.Rows[i1]["PERSIST_CHR"].ToString();

                        p_objResultArr[i1].m_strIntensity_chr = dtbValue.Rows[i1]["INTENSITY_CHR"].ToString();

                        p_objResultArr[i1].m_strPalaceMouth_chr = dtbValue.Rows[i1]["PALACEMOUTH_CHR"].ToString();

                        p_objResultArr[i1].m_strShow_chr = dtbValue.Rows[i1]["SHOW_CHR"].ToString();

                        p_objResultArr[i1].m_strCaul_chr = dtbValue.Rows[i1]["CAUL_CHR"].ToString();

                        p_objResultArr[i1].m_strAnusCheck_chr = dtbValue.Rows[i1]["ANUSCHECK_CHR"].ToString();

                        p_objResultArr[i1].m_strOther_chr = dtbValue.Rows[i1]["OTHER_CHR"].ToString();

                        p_objResultArr[i1].m_strScrutator_chr = dtbValue.Rows[i1]["SCRUTATOR_CHR"].ToString();
                        p_objResultArr[i1].m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i1]["recorddate"]);

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

		#region update all first print date
		/// <summary>
		/// update all first print date。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngUpdateALLFirstPrintDate(	string p_strInPatientID,
			string p_strInPatientDate,
			DateTime p_dtmFirstPrintDate)
		{
//			long lngCheckRes =new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadService","m_lngUpdateALLFirstPrintDate");
//			//if(lngCheckRes <= 0)
//				//return lngCheckRes;	

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateALLFirstPrintDateSQL, ref lngEff, objDPArr);

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
	}
}
