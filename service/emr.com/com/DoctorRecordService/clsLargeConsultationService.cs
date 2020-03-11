using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.DiseaseTrackService
{
	/// <summary>
	/// 大会诊记录中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsLargeConsultationService : clsDiseaseTrackService
	{
		#region RegionName

		/// <summary>
        /// 参数REGISTERID_CHR
		/// </summary>
        private const string c_strGetTimeListSQL = "select createdate_dat,recorddate_dat from t_emr_largeconsultation where registerid_chr = ?  and status_int=1";

		/// <summary>
        /// 参数registerid_chr，createdate_dat
		/// </summary>
        private const string c_strGetRecordContentSQL = @"
select p.registerid_chr,
       p.recorduserid_vchr,
       p.recorddate_dat,
       p.createdate_dat,
       p.createuserid_chr,
       p.address_vchr,
       p.addressxml_vchr,
       p.discusscontent_vchr,
       p.discusscontentxml_vchr,
       p.markstatus_int,
       p.sequence_int,
       p.attendee_vchr,
       p.attendeexml_vchr,
       c.modifydate_dat,
       c.modifyuserid_chr,
       c.address_right_vchr,
       c.discusscontent_right_vchr,
       c.attendee_right_vchr,
       p.firstprintdate_dat,
       p.ifconfirm_int,
       p.status_int
  from t_emr_largeconsultontent c
 inner join t_emr_largeconsultation p on c.registerid_chr =
                                         p.registerid_chr
                                     and c.createdate_dat =
                                         p.createdate_dat
 where p.status_int = 1
   and p.registerid_chr = ?
   and p.createdate_dat = ?
   and c.status_int = 1 ";
		
		/// <summary>
        /// 参数registerid_chr，createdate_dat
		/// </summary>
        private const string c_strCheckCreateDateSQL = "select createuserid_chr,createdate_dat from t_emr_largeconsultation where registerid_chr = ? and createdate_dat = ? and status_int=1";

		/// <summary>
		/// 从CaseDiscussRecordContent获取指定表单的最后修改时间。
        /// 参数registerid_chr，createdate_dat
		/// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"
select c.modifydate_dat, c.modifyuserid_chr
  from t_emr_largeconsultontent c
 where c.registerid_chr = ?
   and c.createdate_dat = ?
   and c.status_int = 1";

		/// <summary>
		/// 从CaseDiscussRecord获取某条作废记录的主要信息。
        /// 参数registerid_chr，createdate_dat
		/// </summary>
		private const string c_strGetDeleteRecordSQL= @"select deactiveddate_dat, deactivedoperatorid_chr
  from t_emr_largeconsultation
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 0";

		/// <summary>
        /// 参数13个
		/// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_largeconsultation
  (registerid_chr,
   recorduserid_vchr,
   recorddate_dat,
   createdate_dat,
   createuserid_chr,
   address_vchr,
   addressxml_vchr,
   discusscontent_vchr,
   discusscontentxml_vchr,
   markstatus_int,
   sequence_int,
   attendee_vchr,
   attendeexml_vchr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

		/// <summary>
        /// 参数7个
		/// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_largeconsultontent
  (registerid_chr,
   createdate_dat,
   status_int,
   modifydate_dat,
   modifyuserid_chr,
   address_right_vchr,
   discusscontent_right_vchr,
   attendee_right_vchr)
values
  (?, ?, 1, ?, ?, ?, ?, ?)";

		
		/// <summary>
        /// 参数12个
		/// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_largeconsultation
   set recorduserid_vchr      = ?,
       recorddate_dat         = ?,
       address_vchr           = ?,
       addressxml_vchr        = ?,
       discusscontent_vchr    = ?,
       discusscontentxml_vchr = ?,
       markstatus_int         = ?,
       sequence_int           = ?,
       attendee_vchr          = ?,
       attendeexml_vchr       = ?
 where registerid_chr = ?
   and createdate_dat = ?
    and status_int = 1";

		/// <summary>
		/// 修改记录到Content
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		
		/// <summary>
		/// zuofei参数deactiveddate_dat，deactivedoperatorid_chr，registerid_chr，createdate_dat
		/// </summary>
		private const string c_strDeleteRecordSQL=@"update t_emr_largeconsultation
   set deactiveddate_dat = ?, deactivedoperatorid_chr = ?, status_int = 0
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";

		/// <summary>
        /// LastModifyDate和FirstPrintDate；参数registerid_chr，createdate_dat
		/// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select p.firstprintdate_dat, c.modifydate_dat
  from t_emr_largeconsultontent c
 inner join t_emr_largeconsultation p on c.registerid_chr =
                                         p.registerid_chr
                                     and c.createdate_dat =
                                         p.createdate_dat
 where p.status_int = 1
   and p.registerid_chr = ?
   and p.createdate_dat = ?
   and c.status_int = 1";


		/// <summary>
        /// 更新FirstPrintDate;参数firstprintdate_dat,registerid_chr，createdate_dat
		/// </summary>
		private const string c_strUpdateFirstPrintDateSQL= @"update t_emr_largeconsultation
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and firstprintdate_dat is null
   and status_int = 1";

		/// <summary>
		/// 从CaseDiscussRecord获取指定病人的所有指定删除者删除的记录时间。
        /// 参数registerid_chr，deactivedoperatorid_chr
		/// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = "select createdate_dat,recorddate_dat from t_emr_largeconsultation where registerid_chr = ? and deactivedoperatorid_chr = ? and status_int=0";

		/// <summary>
		/// 从CaseDiscussRecord获取指定病人的所有已经删除的记录时间。
        /// 参数registerid_chr
		/// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = "select createdate_dat,recorddate_dat from t_emr_largeconsultation where registerid_chr = ?  and status_int=0";

        /// <summary>
        /// 参数registerid_chr，createdate_dat
        /// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"
select p.registerid_chr,
       p.recorduserid_vchr,
       p.recorddate_dat,
       p.createdate_dat,
       p.createuserid_chr,
       p.address_vchr,
       p.addressxml_vchr,
       p.discusscontent_vchr,
       p.discusscontentxml_vchr,
       p.markstatus_int,
       p.sequence_int,
       p.attendee_vchr,
       p.attendeexml_vchr,
       c.modifydate_dat,
       c.modifyuserid_chr,
       c.address_right_vchr,
       c.discusscontent_right_vchr,
       c.attendee_right_vchr
  from t_emr_largeconsultontent c
 inner join t_emr_largeconsultation p on c.registerid_chr =
                                         p.registerid_chr
                                     and c.createdate_dat =
                                         p.createdate_dat
 where p.status_int = 0
   and p.registerid_chr = ?
   and p.createdate_dat = ?
   and c.status_int = 1 ";
		
		#endregion


        #region 获取病人的该记录时间列表
        /// <summary>
        /// 根据住院登记号获取病人的该记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间数组</param>
        /// <param name="p_strRecordDateArr">界面记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strRegisterId, out string[] p_strCreateDateArr, out string[] p_strRecordDateArr)
        { 
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCaseDiscussService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (string.IsNullOrEmpty(p_strRegisterId))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbValue = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strRecordDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["createdate_dat"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strRecordDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["recorddate_dat"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;

        } 
        #endregion

        /// <summary>
        ///  更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建时间</param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmFirstPrintDate)
        {
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCaseDiscussService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                DateTime dtmCreatedDate = DateTime.Now;
                              
                if (string.IsNullOrEmpty(p_strRegisterId) || !DateTime.TryParse(p_strCreatedDate,out dtmCreatedDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = dtmCreatedDate;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

        /// <summary>
        /// 获取病人的已经被某用户删除记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strDeleteUserID">删除操作者ID</param>
        /// <param name="p_strRecordTimeArr">用户填写的记录时间数组</param>
        /// <param name="p_strCreatedDateArr">系统生成的记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strRegisterId,
            string p_strDeleteUserID,
            out string[] p_strRecordTimeArr,
            out string[] p_strCreatedDateArr)
        {
            p_strCreatedDateArr = null;
			p_strRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCaseDiscussService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strDeleteUserID))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[2].Value = p_strDeleteUserID;

                DataTable dtbValue = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreatedDateArr = new string[intRowCount];
                    p_strRecordTimeArr = new string[intRowCount];
                    DataRow objRow = null;
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbValue.Rows[i];
                        if (DateTime.TryParse(objRow["createdate_dat"].ToString(), out dtmTemp))
                            p_strCreatedDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            p_strCreatedDateArr[i] = "";
                        if (DateTime.TryParse(objRow["recorddate_dat"].ToString(), out dtmTemp))
                            p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            p_strRecordTimeArr[i] = "";
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

        /// <summary>
        /// 获取病人的已经被删除记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间</param>
        /// <param name="p_strRecordTimeArr">界面的记录时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strRegisterId,
            out string[] p_strCreateDateArr,
            out string[] p_strRecordTimeArr)
        {
			p_strCreateDateArr=null;
			p_strRecordTimeArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCaseDiscussService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (string.IsNullOrEmpty(p_strRegisterId))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbValue = new DataTable();

                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateDateArr = new string[intRowCount];
                    p_strRecordTimeArr = new string[intRowCount];
                    DataRow objRow = null;
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbValue.Rows[i];
                        if (DateTime.TryParse(objRow["createdate_dat"].ToString(), out dtmTemp))
                            p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            p_strCreateDateArr[i] = "";
                        if (DateTime.TryParse(objRow["recorddate_dat"].ToString(), out dtmTemp))
                            p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            p_strRecordTimeArr[i] = "";
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
 		}

        /// <summary>
        /// 根据住院登记号获取指定记录的内容。
        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCteatedDate">创建时间</param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strRegisterId, string p_strCteatedDate, out clsTrackRecordContent p_objRecordContent)
        {
			p_objRecordContent=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DateTime dtmCreatedDate = DateTime.MinValue;
                //检查参数
                if (string.IsNullOrEmpty(p_strRegisterId) || !DateTime.TryParse(p_strCteatedDate, out dtmCreatedDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtmCreatedDate;

                DataTable dtbValue = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow objRow = dtbValue.Rows[0];
                    DateTime dtmTemp = DateTime.MinValue;
                    clsLargeConsultationContent objRecordContent = new clsLargeConsultationContent();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    if (DateTime.TryParse(objRow["recorddate_dat"].ToString(), out dtmTemp))
                        objRecordContent.m_dtmRecordDate = dtmTemp;
                    objRecordContent.m_dtmCreateDate = dtmCreatedDate;
                    if (DateTime.TryParse(objRow["modifydate_dat"].ToString(), out dtmTemp))
                        objRecordContent.m_dtmModifyDate = dtmTemp;

                    if (DateTime.TryParse(objRow["firstprintdate_dat"].ToString(), out dtmTemp))
                        objRecordContent.m_dtmFirstPrintDate = dtmTemp;
                    objRecordContent.m_strCreateUserID = objRow["createuserid_chr"].ToString();
                    objRecordContent.m_strModifyUserID = objRow["modifyuserid_chr"].ToString();
                    int intTemp = 0;
                    if (int.TryParse(objRow["ifconfirm_int"].ToString(),out intTemp))
                        objRecordContent.m_bytIfConfirm = intTemp;
                    else objRecordContent.m_bytIfConfirm = 0;
                    if (int.TryParse(objRow["status_int"].ToString(), out intTemp))
                        objRecordContent.m_bytStatus = intTemp;
                    else objRecordContent.m_bytStatus = 1;

                    objRecordContent.m_strAddress_Right = objRow["address_right_vchr"].ToString();
                    objRecordContent.m_strAddress = objRow["address_vchr"].ToString();
                    objRecordContent.m_strAddressXML = objRow["addressxml_vchr"].ToString();
                    objRecordContent.m_strDiscussContent_Right = objRow["discusscontent_right_vchr"].ToString();
                    objRecordContent.m_strDiscussContent = objRow["discusscontent_vchr"].ToString();
                    objRecordContent.m_strDiscussContentXML = objRow["discusscontentxml_vchr"].ToString();

                    objRecordContent.m_strAttendeeName = objRow["attendee_vchr"].ToString();
                    objRecordContent.m_strAttendeeName_Right = objRow["attendee_right_vchr"].ToString();
                    objRecordContent.m_strAttendeeNameXml = objRow["attendeexml_vchr"].ToString();
                    if (int.TryParse(objRow["markstatus_int"].ToString(), out intTemp))
                        objRecordContent.m_intMarkStatus = intTemp;
                    else
                        objRecordContent.m_intMarkStatus = 1;

                    //获取签名集合
                    if (!(objRow["SEQUENCE_INT"] is DBNull))
                    {
                        long lngS = long.Parse(objRow["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                if (string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;

                DataTable dtbValue = new DataTable();

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["createuserid_chr"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["createdate_dat"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                if (string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsLargeConsultationContent objContent = (clsLargeConsultationContent)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr);
                objDPArr[0].Value = objContent.m_strRegisterID;
                objDPArr[1].Value = objContent.m_strRecordUserID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmRecordDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = objContent.m_strAddress;
                objDPArr[6].Value = objContent.m_strAddressXML;
                objDPArr[7].Value = objContent.m_strDiscussContent;
                objDPArr[8].Value = objContent.m_strDiscussContentXML;
                objDPArr[9].Value = objContent.m_intMarkStatus;
                objDPArr[10].Value = lngSequence;
                objDPArr[11].Value = objContent.m_strAttendeeName;
                objDPArr[12].Value = objContent.m_strAttendeeNameXml;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmCreateDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmModifyDate;
                objDPArr2[3].Value = objContent.m_strModifyUserID;
                objDPArr2[4].Value = objContent.m_strAddress_Right;
                objDPArr2[5].Value = objContent.m_strDiscussContent_Right;
                objDPArr2[6].Value = objContent.m_strAttendeeName_Right;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                //释放
                objSign = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }			
            //返回
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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                if (string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["deactivedoperatorid_chr"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["deactiveddate_dat"].ToString());
                    }
                    lngRes = (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["modifydate_dat"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["modifyuserid_chr"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["modifydate_dat"].ToString());
                    lngRes = (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //检查参数
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                if (string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值	
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                clsLargeConsultationContent objContent = (clsLargeConsultationContent)p_objRecordContent;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                objDPArr[0].Value = objContent.m_strRecordUserID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmRecordDate;
                objDPArr[2].Value = objContent.m_strAddress;
                objDPArr[3].Value = objContent.m_strAddressXML;
                objDPArr[4].Value = objContent.m_strDiscussContent;
                objDPArr[5].Value = objContent.m_strDiscussContentXML;
                objDPArr[6].Value = objContent.m_intMarkStatus;
                objDPArr[7].Value = lngSequence;
                objDPArr[8].Value = objContent.m_strAttendeeName;
                objDPArr[9].Value = objContent.m_strAttendeeNameXml;

                objDPArr[10].Value = objContent.m_strRegisterID;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = objContent.m_dtmCreateDate;


                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSequence);


                string strSql = @"update t_emr_largeconsultontent
   set status_int = -1
 where createdate_dat = ?
   and registerid_chr = ?";

                IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr3);
                objDPArr3[0].DbType = DbType.DateTime;
                objDPArr3[0].Value = objContent.m_dtmCreateDate;
                objDPArr3[1].Value = objContent.m_strRegisterID;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr3);

                if (lngRes <= 0) throw new Exception("update history record error!");


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmCreateDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmModifyDate;
                objDPArr2[3].Value = objContent.m_strModifyUserID;
                objDPArr2[4].Value = objContent.m_strAddress_Right;
                objDPArr2[5].Value = objContent.m_strDiscussContent_Right;
                objDPArr2[6].Value = objContent.m_strAttendeeName_Right;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

                //释放
                objSign = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
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
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }		
			return lngRes;
		}

        /// <summary>
        /// 获取数据库中最新的修改时间和首次打印时间
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="dtmModifyDate">修改时间</param>
        /// <param name="strFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strRegisterId, string p_strCreatedDate, out DateTime p_dtmModifyDate, out string p_strFirstPrintDate)
        {
			p_dtmModifyDate=DateTime.Now;
			p_strFirstPrintDate=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DateTime dtmCreatedDate = DateTime.MinValue;
                //检查参数
                if (string.IsNullOrEmpty(p_strRegisterId) || !DateTime.TryParse(p_strCreatedDate,out dtmCreatedDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtmCreatedDate;

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_strFirstPrintDate = dtbValue.Rows[0]["firstprintdate_dat"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["modifydate_dat"].ToString());
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建时间</param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetDeleteRecordContentWithServ(string p_strRegisterId, string p_strCreatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DateTime dtmCreatedDate = DateTime.MinValue;
                //检查参数
                if (string.IsNullOrEmpty(p_strRegisterId) || !DateTime.TryParse(p_strCreatedDate, out dtmCreatedDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = dtmCreatedDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow objRow = dtbValue.Rows[0];
                    DateTime dtmTemp = DateTime.MinValue;
                    clsLargeConsultationContent objRecordContent = new clsLargeConsultationContent();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    if (DateTime.TryParse(objRow["recorddate_dat"].ToString(), out dtmTemp))
                        objRecordContent.m_dtmRecordDate = dtmTemp;
                    objRecordContent.m_dtmCreateDate = dtmCreatedDate;
                    if (DateTime.TryParse(objRow["modifydate_dat"].ToString(), out dtmTemp))
                        objRecordContent.m_dtmModifyDate = dtmTemp;

                    if (DateTime.TryParse(objRow["firstprintdate_dat"].ToString(), out dtmTemp))
                        objRecordContent.m_dtmFirstPrintDate = dtmTemp;
                    objRecordContent.m_strCreateUserID = objRow["createuserid_chr"].ToString();
                    objRecordContent.m_strModifyUserID = objRow["modifyuserid_chr"].ToString();
                    int intTemp = 0;
                    if (int.TryParse(objRow["ifconfirm_int"].ToString(), out intTemp))
                        objRecordContent.m_bytIfConfirm = intTemp;
                    else objRecordContent.m_bytIfConfirm = 0;
                    if (int.TryParse(objRow["status_int"].ToString(), out intTemp))
                        objRecordContent.m_bytStatus = intTemp;
                    else objRecordContent.m_bytStatus = 1;

                    objRecordContent.m_strAddress_Right = objRow["address_right_vchr"].ToString();
                    objRecordContent.m_strAddress = objRow["address_vchr"].ToString();
                    objRecordContent.m_strAddressXML = objRow["addressxml_vchr"].ToString();
                    objRecordContent.m_strDiscussContent_Right = objRow["discusscontent_right_vchr"].ToString();
                    objRecordContent.m_strDiscussContent = objRow["discusscontent_vchr"].ToString();
                    objRecordContent.m_strDiscussContentXML = objRow["discusscontentxml_vchr"].ToString();

                    objRecordContent.m_strAttendeeName = objRow["attendee_vchr"].ToString();
                    objRecordContent.m_strAttendeeName_Right = objRow["attendee_right_vchr"].ToString();
                    objRecordContent.m_strAttendeeNameXml = objRow["attendeexml_vchr"].ToString();
                    if (int.TryParse(objRow["markstatus_int"].ToString(), out intTemp))
                        objRecordContent.m_intMarkStatus = intTemp;
                    else
                        objRecordContent.m_intMarkStatus = 1;

                    //获取签名集合
                    if (!(objRow["SEQUENCE_INT"] is DBNull))
                    {
                        long lngS = long.Parse(objRow["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        objSign = null;
                    }
                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}

	}// END CLASS DEFINITION clsGeneralDiseaseService

}
