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
    /// 血液净化记录(添加，修改)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBloodCleanseRecord_contentService : clsDiseaseTrackService
    {
        #region SQL语句
        /// <summary>
        /// 从t_emr_bloodcleanserecord_all获取指定病人的所有没有删除记录的时间。
        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_bloodcleanserecord_all t
 Where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// 从t_emr_bloodcleanserecord_all中获取指定时间的表单,获取已经存在记录的主要信息
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_bloodcleanserecord_all t
 Where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// 从t_emr_bloodcleanserecord_all获取删除表单的主要信息。
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_bloodcleanserecord_all t
 Where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// 添加记录到t_emr_bloodcleanserecord_all
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_bloodcleanserecord_all
  (touxiya_chr,
   jingmaiya_chr,
   gansuliang_chr,
   xueliuliang_chr,
   tiwen_chr,
   maibo_chr,
   xueya_chr,
   huxi_chr,
   faleng_chr,
   fare_chr,
   toutong_chr,
   tuoshuiliang_chr,
   outu_chr,
   chouchu_chr,
   xinyi_chr,
   nanongdu_chr,
   chuli_chr,
   chuli_chrxml,
   registerid_chr,
   createdate_dat,
   createuserid_chr,
   ifconfirm_int,
   status_int,
   recordusername_vchr,
   recorddate_dat)
values
  (?,?,?,?,?,?,?,?,?,?,
   ?,?,?,?,?,?,?,?,?,?,?,0,1,?,?)";//23个参数

        /// <summary>
        /// 添加记录到t_emr_bloodcleanserecord_right
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_bloodcleanserecord_right
  (modifydate,
   modifyuserid,
   touxiya_chr_right,
   jingmaiya_chr_right,
   gansuliang_chr_right,
   xueliuliang_chr_right,
   tiwen_chr_right,
   maibo_chr_right,
   xueya_chr_right,
   huxi_chr_right,
   faleng_chr_right,
   fare_chr_right,
   toutong_chr_right,
   tuoshuiliang_chr_right,
   outu_chr_right,
   chouchu_chr_right,
   xinyi_chr_right,
   nanongdu_chr_right,
   chuli_chr_right,
   registerid_chr,
   createdate_dat,
   status_int)
values
  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)";//21个参数

        /// <summary>
        /// 修改记录到t_emr_bloodcleanserecord_all
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_bloodcleanserecord_all
   set touxiya_chr = ?,
       jingmaiya_chr = ?,
       gansuliang_chr = ?,
       xueliuliang_chr = ?,
       tiwen_chr = ?,
       maibo_chr = ?,
       xueya_chr = ?,
       huxi_chr = ?,
       faleng_chr = ?,
       fare_chr = ?,
       toutong_chr = ?,
       tuoshuiliang_chr = ?,
       outu_chr = ?,
       chouchu_chr = ?,
       xinyi_chr = ?,
       nanongdu_chr = ?,
       chuli_chr = ?,
       chuli_chrxml = ?,
       recordusername_vchr = ?,
       recorddate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//22个参数

        /// <summary>
        /// 修改记录到t_emr_bloodcleanserecord_right
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 设置t_emr_bloodcleanserecord_all中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_bloodcleanserecord_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and Status_int = 1";

        /// <summary>
        /// 更新t_emr_bloodcleanserecord_all中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_bloodcleanserecord_all t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";


        /// <summary>
        /// 从t_emr_bloodcleanserecord_all获取指定病人的所有指定删除者删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_bloodcleanserecord_all t
 Where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// 从t_emr_bloodcleanserecord_all获取指定病人的所有已经删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_bloodcleanserecord_all t
 where t.registerid_chr = ?
   and t.status_int = 0";



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
        public virtual long m_lngGetRecordTimeList(
            string p_strRegisterId, out string[] p_strCreateDateArr, out string[] p_strRecordDateArr)
        {
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //设置结果
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
        #endregion

        #region 更新数据库中的首次打印时间
        /// <summary>
        ///  更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">这里为空，用于更新所有打印时间</param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmFirstPrintDate)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

                string strSql = @"update t_emr_bloodcleansesub_all t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                //执行SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
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
        public virtual long m_lngGetDeleteRecordTimeList(
            string p_strRegisterId,
            string p_strDeleteUserID,
            out string[] p_strRecordTimeArr,
            out string[] p_strCreatedDateArr)
        {
            p_strRecordTimeArr = null;
            p_strCreatedDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strDeleteUserID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strCreatedDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //设置结果
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreatedDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        /// 获取病人的已经被删除记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间</param>
        /// <param name="p_strRecordTimeArr">界面的记录时间</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeListAll(
            string p_strRegisterId,
            out string[] p_strCreateDateArr,
            out string[] p_strRecordTimeArr)
        {
            p_strCreateDateArr = null;
            p_strRecordTimeArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeListAll");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strRecordTimeArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //设置结果
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取指定记录的内容
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
            p_objRecordContent = null;

            //检查参数
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCteatedDate))
                return (long)enmOperationResult.Parameter_Error;
            string strGetRecordContentSQL = @"select a.touxiya_chr,
       a.jingmaiya_chr,
       a.gansuliang_chr,
       a.xueliuliang_chr,
       a.tiwen_chr,
       a.maibo_chr,
       a.xueya_chr,
       a.huxi_chr,
       a.faleng_chr,
       a.fare_chr,
       a.toutong_chr,
       a.tuoshuiliang_chr,
       a.outu_chr,
       a.chouchu_chr,
       a.xinyi_chr,
       a.nanongdu_chr,
       a.chuli_chr,
       a.chuli_chrxml,
       a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recordusername_vchr,
       a.recorddate_dat,
       b.modifydate,
       b.modifyuserid,
       b.touxiya_chr_right,
       b.jingmaiya_chr_right,
       b.gansuliang_chr_right,
       b.xueliuliang_chr_right,
       b.tiwen_chr_right,
       b.maibo_chr_right,
       b.xueya_chr_right,
       b.huxi_chr_right,
       b.faleng_chr_right,
       b.fare_chr_right,
       b.toutong_chr_right,
       b.tuoshuiliang_chr_right,
       b.outu_chr_right,
       b.chouchu_chr_right,
       b.xinyi_chr_right,
       b.nanongdu_chr_right,
       b.chuli_chr_right
  from t_emr_bloodcleanserecord_all a
 inner join  t_emr_bloodcleanserecord_right b on a.registerid_chr = b.registerid_chr
                                    and a.createdate_dat = b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?
   and a.createdate_dat = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCteatedDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsDialyseRecord_Value objRecordContent = new clsDialyseRecord_Value();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRECORDUSERNAME_CHR = dtbValue.Rows[0]["RECORDUSERNAME_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strTOUXIYA_CHR = dtbValue.Rows[0]["touxiya_chr"].ToString();
                    objRecordContent.m_strJINGMAI_CHR = dtbValue.Rows[0]["jingmaiya_chr"].ToString();
                    objRecordContent.m_strGANSULIANG_CHR = dtbValue.Rows[0]["gansuliang_chr"].ToString();
                    objRecordContent.m_strXUELIULIANG_CHR = dtbValue.Rows[0]["xueliuliang_chr"].ToString();
                    objRecordContent.m_strTIWEN_CHR = dtbValue.Rows[0]["tiwen_chr"].ToString();
                    objRecordContent.m_strMAIBO_CHR = dtbValue.Rows[0]["maibo_chr"].ToString();
                    objRecordContent.m_strXUEYA_CHR = dtbValue.Rows[0]["xueya_chr"].ToString();
                    objRecordContent.m_strHUXI_CHR = dtbValue.Rows[0]["huxi_chr"].ToString();
                    objRecordContent.m_strFALENG_CHR = dtbValue.Rows[0]["faleng_chr"].ToString();
                    objRecordContent.m_strFARE_CHR = dtbValue.Rows[0]["fare_chr"].ToString();
                    objRecordContent.m_strTOUTONG_CHR = dtbValue.Rows[0]["toutong_chr"].ToString();
                    objRecordContent.m_strTUOSHUILIANG_CHR = dtbValue.Rows[0]["tuoshuiliang_chr"].ToString();
                    objRecordContent.m_strOUTU_CHR = dtbValue.Rows[0]["outu_chr"].ToString();
                    objRecordContent.m_strCHOUCHU_CHR = dtbValue.Rows[0]["chouchu_chr"].ToString();
                    objRecordContent.m_strXINYI_CHR = dtbValue.Rows[0]["xinyi_chr"].ToString();
                    objRecordContent.m_strNANONGDU_CHR = dtbValue.Rows[0]["nanongdu_chr"].ToString();
                    objRecordContent.m_strCHULI_CHR = dtbValue.Rows[0]["chuli_chr"].ToString();
                    objRecordContent.m_strCHULI_CHR_RIGHT = dtbValue.Rows[0]["chuli_chr_right"].ToString();
                    objRecordContent.m_strCHULI_CHRXML = dtbValue.Rows[0]["chuli_chrxml"].ToString();
                    #endregion 
                    p_objRecordContent = objRecordContent;
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
            p_objModifyInfo = null;

            //检查参数
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
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
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            clsDialyseRecord_Value objRecordContent = (clsDialyseRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 付值
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strTOUXIYA_CHR;
                objDPArr[1].Value = objRecordContent.m_strJINGMAI_CHR;
                objDPArr[2].Value = objRecordContent.m_strGANSULIANG_CHR;
                objDPArr[3].Value = objRecordContent.m_strXUELIULIANG_CHR;
                objDPArr[4].Value = objRecordContent.m_strTIWEN_CHR;
                objDPArr[5].Value = objRecordContent.m_strMAIBO_CHR;
                objDPArr[6].Value = objRecordContent.m_strXUEYA_CHR;
                objDPArr[7].Value = objRecordContent.m_strHUXI_CHR;
                objDPArr[8].Value = objRecordContent.m_strFALENG_CHR;
                objDPArr[9].Value = objRecordContent.m_strFARE_CHR;
                objDPArr[10].Value = objRecordContent.m_strTOUTONG_CHR;
                objDPArr[11].Value = objRecordContent.m_strTUOSHUILIANG_CHR;
                objDPArr[12].Value = objRecordContent.m_strOUTU_CHR;
                objDPArr[13].Value = objRecordContent.m_strCHOUCHU_CHR;
                objDPArr[14].Value = objRecordContent.m_strXINYI_CHR;
                objDPArr[15].Value = objRecordContent.m_strNANONGDU_CHR;
                objDPArr[16].Value = objRecordContent.m_strCHULI_CHR;
                objDPArr[17].Value = objRecordContent.m_strCHULI_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strRegisterID;
                objDPArr[19].DbType = DbType.Date;
                objDPArr[19].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[20].Value = objRecordContent.m_strCreateUserID;
                objDPArr[21].Value = objRecordContent.m_strRECORDUSERNAME_CHR;
                objDPArr[22].DbType = DbType.Date;
                objDPArr[22].Value = objRecordContent.m_dtmRecordDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(21, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = objRecordContent.m_strModifyUserID;

                objDPArr2[2].Value = objRecordContent.m_strTOUXIYA_CHR;
                objDPArr2[3].Value = objRecordContent.m_strJINGMAI_CHR;
                objDPArr2[4].Value = objRecordContent.m_strGANSULIANG_CHR;
                objDPArr2[5].Value = objRecordContent.m_strXUELIULIANG_CHR;
                objDPArr2[6].Value = objRecordContent.m_strTIWEN_CHR;
                objDPArr2[7].Value = objRecordContent.m_strMAIBO_CHR;
                objDPArr2[8].Value = objRecordContent.m_strXUEYA_CHR;
                objDPArr2[9].Value = objRecordContent.m_strHUXI_CHR;
                objDPArr2[10].Value = objRecordContent.m_strFALENG_CHR;
                objDPArr2[11].Value = objRecordContent.m_strFARE_CHR;
                objDPArr2[12].Value = objRecordContent.m_strTOUTONG_CHR;
                objDPArr2[13].Value = objRecordContent.m_strTUOSHUILIANG_CHR;
                objDPArr2[14].Value = objRecordContent.m_strOUTU_CHR;
                objDPArr2[15].Value = objRecordContent.m_strCHOUCHU_CHR;
                objDPArr2[16].Value = objRecordContent.m_strXINYI_CHR;
                objDPArr2[17].Value = objRecordContent.m_strNANONGDU_CHR;
                objDPArr2[18].Value = objRecordContent.m_strCHULI_CHR_RIGHT;

                objDPArr2[19].Value = objRecordContent.m_strRegisterID;
                objDPArr2[20].DbType = DbType.Date;
                objDPArr2[20].Value = objRecordContent.m_dtmCreateDate;
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
                objHRPServ = null;
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
            p_objModifyInfo = null;
            //检查参数
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsDialyseRecord_Value objRecordContent = (clsDialyseRecord_Value)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_bloodcleanserecord_right t2
 where t2.registerid_chr = ?
   and t2.createdate_dat = ?
   and t2.status_int = 1";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                    dtbValue = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_CHR"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE_DAT"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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
            if (p_objRecordContent == null || p_objRecordContent.m_dtmCreateDate == DateTime.MinValue || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsDialyseRecord_Value objRecordContent = (clsDialyseRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region set value
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(22, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strTOUXIYA_CHR;
                objDPArr[1].Value = objRecordContent.m_strJINGMAI_CHR;
                objDPArr[2].Value = objRecordContent.m_strGANSULIANG_CHR;
                objDPArr[3].Value = objRecordContent.m_strXUELIULIANG_CHR;
                objDPArr[4].Value = objRecordContent.m_strTIWEN_CHR;
                objDPArr[5].Value = objRecordContent.m_strMAIBO_CHR;
                objDPArr[6].Value = objRecordContent.m_strXUEYA_CHR;
                objDPArr[7].Value = objRecordContent.m_strHUXI_CHR;
                objDPArr[8].Value = objRecordContent.m_strFALENG_CHR;
                objDPArr[9].Value = objRecordContent.m_strFARE_CHR;
                objDPArr[10].Value = objRecordContent.m_strTOUTONG_CHR;
                objDPArr[11].Value = objRecordContent.m_strTUOSHUILIANG_CHR;
                objDPArr[12].Value = objRecordContent.m_strOUTU_CHR;
                objDPArr[13].Value = objRecordContent.m_strCHOUCHU_CHR;
                objDPArr[14].Value = objRecordContent.m_strXINYI_CHR;
                objDPArr[15].Value = objRecordContent.m_strNANONGDU_CHR;
                objDPArr[16].Value = objRecordContent.m_strCHULI_CHR;
                objDPArr[17].Value = objRecordContent.m_strCHULI_CHRXML;

                objDPArr[18].Value = objRecordContent.m_strRECORDUSERNAME_CHR;
                objDPArr[19].DbType = DbType.Date;
                objDPArr[19].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[20].Value = objRecordContent.m_strRegisterID;
                objDPArr[21].DbType = DbType.Date;
                objDPArr[21].Value = objRecordContent.m_dtmCreateDate;

                #endregion
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                #region set value

                lngRes = m_lngDeleteContentInfo(objRecordContent.m_strRegisterID, objRecordContent.m_dtmCreateDate);

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(21, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[2].Value = objRecordContent.m_strTOUXIYA_CHR;
                objDPArr2[3].Value = objRecordContent.m_strJINGMAI_CHR;
                objDPArr2[4].Value = objRecordContent.m_strGANSULIANG_CHR;
                objDPArr2[5].Value = objRecordContent.m_strXUELIULIANG_CHR;
                objDPArr2[6].Value = objRecordContent.m_strTIWEN_CHR;
                objDPArr2[7].Value = objRecordContent.m_strMAIBO_CHR;
                objDPArr2[8].Value = objRecordContent.m_strXUEYA_CHR;
                objDPArr2[9].Value = objRecordContent.m_strHUXI_CHR;
                objDPArr2[10].Value = objRecordContent.m_strFALENG_CHR;
                objDPArr2[11].Value = objRecordContent.m_strFARE_CHR;
                objDPArr2[12].Value = objRecordContent.m_strTOUTONG_CHR;
                objDPArr2[13].Value = objRecordContent.m_strTUOSHUILIANG_CHR;
                objDPArr2[14].Value = objRecordContent.m_strOUTU_CHR;
                objDPArr2[15].Value = objRecordContent.m_strCHOUCHU_CHR;
                objDPArr2[16].Value = objRecordContent.m_strXINYI_CHR;
                objDPArr2[17].Value = objRecordContent.m_strNANONGDU_CHR;
                objDPArr2[18].Value = objRecordContent.m_strCHULI_CHR_RIGHT;
                objDPArr2[19].Value = objRecordContent.m_strRegisterID;
                objDPArr2[20].DbType = DbType.Date;
                objDPArr2[20].Value = objRecordContent.m_dtmCreateDate;
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

            } return lngRes;
        }
        #endregion

        #region 把记录从数据中“删除”。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <returns></returns>
        [AutoComplete]
        long m_lngDeleteContentInfo(string p_strRegisterId, DateTime p_dtmCreatedDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MaxValue)
                return -1;
            string strSql = @" update t_emr_bloodcleanserecord_right t set t.status_int = 0
 where t.registerid_chr = ? and t.createdate_dat = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmCreatedDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
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
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsDialyseRecord_Value objRecordContent = (clsDialyseRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;

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
                objHRPServ = null;

            } return lngRes;
        }
        #endregion

        #region  获取数据库中最新的修改时间和首次打印时间
        /// <summary>
        /// 获取数据库中最新的修改时间和首次打印时间
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_dtmModifyDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strRegisterId, string p_strCreatedDate,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            //检查参数
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;
            /// <summary>
            /// 从t_emr_bloodcleanserecord_all和t_emr_bloodcleanserecord_right获取LastModifyDate和FirstPrintDate
            /// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from t_emr_bloodcleanserecord_all a
 inner join t_emr_bloodcleanserecord_right b on a.registerid_chr =
                                              b.registerid_chr
                                          and a.createdate_dat =
                                              b.createdate_dat
 where a.registerid_chr = ?
   and a.createdate_dat = ?
   and a.status_int = 1
   and b.status_int = 1";


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["firstprintdate_dat"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["modifydate"].ToString());
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取指定已经被删除记录的内容(用于显示在DG表格中)
        /// <summary>
        /// 获取指定已经被删除记录的内容。
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strRegisterId, string p_strCreatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //检查参数
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select t1.touxiya_chr,
       t1.jingmaiya_chr,
       t1.gansuliang_chr,
       t1.xueliuliang_chr,
       t1.tiwen_chr,
       t1.maibo_chr,
       t1.xueya_chr,
       t1.huxi_chr,
       t1.faleng_chr,
       t1.fare_chr,
       t1.toutong_chr,
       t1.tuoshuiliang_chr,
       t1.outu_chr,
       t1.chouchu_chr,
       t1.xinyi_chr,
       t1.nanongdu_chr,
       t1.chuli_chr,
       t1.chuli_chrxml,
       t1.registerid_chr,
       t1.createdate_dat,
       t1.createuserid_chr,
       t1.ifconfirm_int,
       t1.status_int,
       t1.deactiveddate_dat,
       t1.deactivedoperatorid_chr,
       t1.firstprintdate_dat,
       t1.recordusername_vchr,
       t1.recorddate_dat,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from t_emr_bloodcleanserecord_all t1
 inner join t_emr_bloodcleanserecord_right t2 on t1.registerid_chr =
                                         t2.registerid_chr
                                     and t1.createdate_dat =
                                         t2.createdate_dat
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t2.modifydate =
       (select max(modifydate)
          from t_emr_bloodcleanserecord_right
         where registerid_chr = t1.registerid_chr
           and createdate_dat = t1.createdate_dat)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsDialyseRecord_Value objRecordContent = new clsDialyseRecord_Value();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRECORDUSERNAME_CHR = dtbValue.Rows[0]["RECORDUSERNAME_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strTOUXIYA_CHR = dtbValue.Rows[0]["touxiya_chr"].ToString();
                    objRecordContent.m_strJINGMAI_CHR = dtbValue.Rows[0]["jingmaiya_chr"].ToString();
                    objRecordContent.m_strGANSULIANG_CHR = dtbValue.Rows[0]["gansuliang_chr"].ToString();
                    objRecordContent.m_strXUELIULIANG_CHR = dtbValue.Rows[0]["xueliuliang_chr"].ToString();
                    objRecordContent.m_strTIWEN_CHR = dtbValue.Rows[0]["tiwen_chr"].ToString();
                    objRecordContent.m_strMAIBO_CHR = dtbValue.Rows[0]["maibo_chr"].ToString();
                    objRecordContent.m_strXUEYA_CHR = dtbValue.Rows[0]["xueya_chr"].ToString();
                    objRecordContent.m_strHUXI_CHR = dtbValue.Rows[0]["huxi_chr"].ToString();
                    objRecordContent.m_strFALENG_CHR = dtbValue.Rows[0]["faleng_chr"].ToString();
                    objRecordContent.m_strFARE_CHR = dtbValue.Rows[0]["fare_chr"].ToString();
                    objRecordContent.m_strTOUTONG_CHR = dtbValue.Rows[0]["toutong_chr"].ToString();
                    objRecordContent.m_strTUOSHUILIANG_CHR = dtbValue.Rows[0]["tuoshuiliang_chr"].ToString();
                    objRecordContent.m_strOUTU_CHR = dtbValue.Rows[0]["outu_chr"].ToString();
                    objRecordContent.m_strCHOUCHU_CHR = dtbValue.Rows[0]["chouchu_chr"].ToString();
                    objRecordContent.m_strXINYI_CHR = dtbValue.Rows[0]["xinyi_chr"].ToString();
                    objRecordContent.m_strNANONGDU_CHR = dtbValue.Rows[0]["nanongdu_chr"].ToString();
                    objRecordContent.m_strCHULI_CHR = dtbValue.Rows[0]["chuli_chr"].ToString();
                    objRecordContent.m_strCHULI_CHRXML = dtbValue.Rows[0]["chuli_chrxml"].ToString();
                   
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion
    }
}
