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
    public class clsIcuAcad_PostPartumRecord_contentService : clsDiseaseTrackService
    {
        #region SQL语句
        /// <summary>
        /// 从ICUACAD_POSTPARTUMSEERECORD获取指定病人的所有没有删除记录的时间。
        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// 从ICUACAD_POSTPARTUMSEERECORD中获取指定时间的表单,获取已经存在记录的主要信息
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// 从ICUACAD_POSTPARTUMSEERECORD获取删除表单的主要信息。
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.DEACTIVEDOPERATORID_CHR
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// 添加记录到ICUACAD_POSTPARTUMSEERECORD
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_postpartum_all
  (postportum_num_chr,
   uterusbottom_chr,
   uteruspinch_chr,
   milknum_chr,
   breastbulge_chr,
   nipple_chr,
   dewnum_chr,
   dewcolor_chr,
   dewfuck_chr,
   perineum_chr,
   bp_chr,
   urine_chr,
   annotations_chr,
   postportum_num_chrxml,
   uterusbottom_chrxml,
   uteruspinch_chrxml,
   milknum_chrxml,
   breastbulge_chrxml,
   nipple_chrxml,
   dewnum_chrxml,
   dewcolor_chrxml,
   dewfuck_chrxml,
   perineum_chrxml,
   bp_chrxml,
   urine_chrxml,
   annotations_chrxml,
   registerid_chr,
   createdate_dat,
   createuserid_chr,
   ifconfirm_int,
   status_int,
   recorduserid_vchr,
   recorddate_dat,
   sequence_int)
values
  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
   ?,?,?,?,?,?,?,?,?,?,?,?,?,0,1,?,?,?)";//32个参数

        /// <summary>
        /// 添加记录到ICUACAD_POSTPARTUMSEECONTENT
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_postpartum_right
  (modifydate,
   modifyuserid,
   postportum_num_chr_right,
   uterusbottom_chr_right,
   uteruspinch_chr_right,
   milknum_chr_right,
   breastbulge_chr_right,
   nipple_chr_right,
   dewnum_chr_right,
   dewcolor_chr_right,
   dewfuck_chr_right,
   perineum_chr_right,
   bp_chr_right,
   urine_chr_right,
   annotations_chr_right,
   registerid_chr,
   createdate_dat,
   status_int)
values
  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)";//17个参数

        /// <summary>
        /// 修改记录到ICUACAD_POSTPARTUMSEERECORD
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_postpartum_all
   set postportum_num_chr = ?,
       uterusbottom_chr = ?,
       uteruspinch_chr = ?,
       milknum_chr = ?,
       breastbulge_chr = ?,
       nipple_chr = ?,
       dewnum_chr = ?,
       dewcolor_chr = ?,
       dewfuck_chr = ?,
       perineum_chr = ?,
       bp_chr = ?,
       urine_chr = ?,
       annotations_chr = ?,
       postportum_num_chrxml = ?,
       uterusbottom_chrxml = ?,
       uteruspinch_chrxml = ?,
       milknum_chrxml = ?,
       breastbulge_chrxml = ?,
       nipple_chrxml = ?,
       dewnum_chrxml = ?,
       dewcolor_chrxml = ?,
       dewfuck_chrxml = ?,
       perineum_chrxml = ?,
       bp_chrxml = ?,
       urine_chrxml = ?,
       annotations_chrxml = ?,
       recorduserid_vchr = ?,
       recorddate_dat = ?,
       sequence_int = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//31个参数

        /// <summary>
        /// 修改记录到ICUACAD_POSTPARTUMSEERECORD
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 设置ICUACAD_POSTPARTUMSEERECORD中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_postpartum_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and Status_int = 1";

        /// <summary>
        /// 更新ICUACAD_POSTPARTUMSEERECORD中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_postpartum_all t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";


        /// <summary>
        /// 从ICUACAD_POSTPARTUMSEERECORD获取指定病人的所有指定删除者删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// 从ICUACAD_POSTPARTUMSEERECORD获取指定病人的所有已经删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_postpartum_all t
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

                string strSql = @"update t_emr_postpartumannoall t
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
            //,t2.MODIFYDATE as MODIFYDATE,t2.MODIFYUSERID as MODIFYUSERID
            string strGetRecordContentSQL = @"select a.postportum_num_chr,
       a.uterusbottom_chr,
       a.uteruspinch_chr,
       a.milknum_chr,
       a.breastbulge_chr,
       a.nipple_chr,
       a.dewnum_chr,
       a.dewcolor_chr,
       a.dewfuck_chr,
       a.perineum_chr,
       a.bp_chr,
       a.urine_chr,
       a.annotations_chr,
       a.postportum_num_chrxml,
       a.uterusbottom_chrxml,
       a.uteruspinch_chrxml,
       a.milknum_chrxml,
       a.breastbulge_chrxml,
       a.nipple_chrxml,
       a.dewnum_chrxml,
       a.dewcolor_chrxml,
       a.dewfuck_chrxml,
       a.perineum_chrxml,
       a.bp_chrxml,
       a.urine_chrxml,
       a.annotations_chrxml,
       a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       b.modifydate,
       b.modifyuserid,
       b.postportum_num_chr_right,
       b.uterusbottom_chr_right,
       b.uteruspinch_chr_right,
       b.milknum_chr_right,
       b.breastbulge_chr_right,
       b.nipple_chr_right,
       b.dewnum_chr_right,
       b.dewcolor_chr_right,
       b.dewfuck_chr_right,
       b.perineum_chr_right,
       b.bp_chr_right,
       b.urine_chr_right,
       b.annotations_chr_right,
       b.status_int
  from t_emr_postpartum_all a
 inner join t_emr_postpartum_right b on a.registerid_chr = b.registerid_chr
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
                    clsIcuAcad_PostPartumRecord_Value objRecordContent = new clsIcuAcad_PostPartumRecord_Value();
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
                    objRecordContent.m_strRecordUserID = dtbValue.Rows[0]["RECORDUSERID_VCHR"].ToString();
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

                    objRecordContent.m_strANNOTATIONS_CHR = dtbValue.Rows[0]["ANNOTATIONS_CHR"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHR_RIGHT = dtbValue.Rows[0]["ANNOTATIONS_CHR_RIGHT"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHRXML = dtbValue.Rows[0]["ANNOTATIONS_CHRXML"].ToString();

                    objRecordContent.m_strBP_CHR = dtbValue.Rows[0]["BP_CHR"].ToString();
                    objRecordContent.m_strBP_CHR_RIGHT = dtbValue.Rows[0]["BP_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBP_CHRXML = dtbValue.Rows[0]["BP_CHRXML"].ToString();

                    objRecordContent.m_strBREASTBULGE_CHR = dtbValue.Rows[0]["BREASTBULGE_CHR"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHR_RIGHT = dtbValue.Rows[0]["BREASTBULGE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHRXML = dtbValue.Rows[0]["BREASTBULGE_CHRXML"].ToString();

                    objRecordContent.m_strDEWCOLOR_CHR = dtbValue.Rows[0]["DEWCOLOR_CHR"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHR_RIGHT = dtbValue.Rows[0]["DEWCOLOR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHRXML = dtbValue.Rows[0]["DEWCOLOR_CHRXML"].ToString();

                    objRecordContent.m_strDEWFUCK_CHR = dtbValue.Rows[0]["DEWFUCK_CHR"].ToString();
                    objRecordContent.m_strDEWFUCK_CHR_RIGHT = dtbValue.Rows[0]["DEWFUCK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWFUCK_CHRXML = dtbValue.Rows[0]["DEWFUCK_CHRXML"].ToString();

                    objRecordContent.m_strDEWNUM_CHR = dtbValue.Rows[0]["DEWNUM_CHR"].ToString();
                    objRecordContent.m_strDEWNUM_CHR_RIGHT = dtbValue.Rows[0]["DEWNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWNUM_CHRXML = dtbValue.Rows[0]["DEWNUM_CHRXML"].ToString();

                    objRecordContent.m_strMILKNUM_CHR = dtbValue.Rows[0]["MILKNUM_CHR"].ToString();
                    objRecordContent.m_strMILKNUM_CHR_RIGHT = dtbValue.Rows[0]["MILKNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMILKNUM_CHRXML = dtbValue.Rows[0]["MILKNUM_CHRXML"].ToString();

                    objRecordContent.m_strNIPPLE_CHR = dtbValue.Rows[0]["NIPPLE_CHR"].ToString();
                    objRecordContent.m_strNIPPLE_CHR_RIGHT = dtbValue.Rows[0]["NIPPLE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strNIPPLE_CHRXML = dtbValue.Rows[0]["NIPPLE_CHRXML"].ToString();

                    objRecordContent.m_strPERINEUM_CHR = dtbValue.Rows[0]["PERINEUM_CHR"].ToString();
                    objRecordContent.m_strPERINEUM_CHR_RIGHT = dtbValue.Rows[0]["PERINEUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPERINEUM_CHRXML = dtbValue.Rows[0]["PERINEUM_CHRXML"].ToString();

                    objRecordContent.m_strPOSTPORTUM_NUM_CHR = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHRXML = dtbValue.Rows[0]["POSTPORTUM_NUM_CHRXML"].ToString();

                    objRecordContent.m_strURINE_CHR = dtbValue.Rows[0]["URINE_CHR"].ToString();
                    objRecordContent.m_strURINE_CHR_RIGHT = dtbValue.Rows[0]["URINE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strURINE_CHRXML = dtbValue.Rows[0]["URINE_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSBOTTOM_CHR = dtbValue.Rows[0]["UTERUSBOTTOM_CHR"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT = dtbValue.Rows[0]["UTERUSBOTTOM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHRXML = dtbValue.Rows[0]["UTERUSBOTTOM_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSPINCH_CHR = dtbValue.Rows[0]["UTERUSPINCH_CHR"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHR_RIGHT = dtbValue.Rows[0]["UTERUSPINCH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHRXML = dtbValue.Rows[0]["UTERUSPINCH_CHRXML"].ToString();

                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
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
            clsIcuAcad_PostPartumRecord_Value objRecordContent = (clsIcuAcad_PostPartumRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region 付值
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(32, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR;
                objDPArr[1].Value = objRecordContent.m_strUTERUSBOTTOM_CHR;
                objDPArr[2].Value = objRecordContent.m_strUTERUSPINCH_CHR;
                objDPArr[3].Value = objRecordContent.m_strMILKNUM_CHR;
                objDPArr[4].Value = objRecordContent.m_strBREASTBULGE_CHR;
                objDPArr[5].Value = objRecordContent.m_strNIPPLE_CHR;
                objDPArr[6].Value = objRecordContent.m_strDEWNUM_CHR;
                objDPArr[7].Value = objRecordContent.m_strDEWCOLOR_CHR;
                objDPArr[8].Value = objRecordContent.m_strDEWFUCK_CHR;
                objDPArr[9].Value = objRecordContent.m_strPERINEUM_CHR;
                objDPArr[10].Value = objRecordContent.m_strBP_CHR;
                objDPArr[11].Value = objRecordContent.m_strURINE_CHR;
                objDPArr[12].Value = objRecordContent.m_strANNOTATIONS_CHR;

                objDPArr[13].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strUTERUSBOTTOM_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strUTERUSPINCH_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strMILKNUM_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strBREASTBULGE_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strNIPPLE_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strDEWNUM_CHRXML;
                objDPArr[20].Value = objRecordContent.m_strDEWCOLOR_CHRXML;
                objDPArr[21].Value = objRecordContent.m_strDEWFUCK_CHRXML;
                objDPArr[22].Value = objRecordContent.m_strPERINEUM_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strBP_CHRXML;
                objDPArr[24].Value = objRecordContent.m_strURINE_CHRXML;
                objDPArr[25].Value = objRecordContent.m_strANNOTATIONS_CHRXML;

                objDPArr[26].Value = objRecordContent.m_strRegisterID;
                objDPArr[27].DbType = DbType.Date;
                objDPArr[27].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[28].Value = objRecordContent.m_strCreateUserID;
                objDPArr[29].Value = objRecordContent.m_strRecordUserID;
                objDPArr[30].DbType = DbType.Date;
                objDPArr[30].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[31].Value = lngSequence;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[2].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT;
                objDPArr2[3].Value = objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT;
                objDPArr2[4].Value = objRecordContent.m_strUTERUSPINCH_CHR_RIGHT;
                objDPArr2[5].Value = objRecordContent.m_strMILKNUM_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strBREASTBULGE_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strNIPPLE_CHR_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strDEWNUM_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strDEWCOLOR_CHR_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strDEWFUCK_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strPERINEUM_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strBP_CHR_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strURINE_CHR_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strANNOTATIONS_CHR_RIGHT;

                objDPArr2[15].Value = objRecordContent.m_strRegisterID;
                objDPArr2[16].DbType = DbType.Date;
                objDPArr2[16].Value = objRecordContent.m_dtmCreateDate;
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

            clsIcuAcad_PostPartumRecord_Value objRecordContent = (clsIcuAcad_PostPartumRecord_Value)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。
            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_postpartum_right t2
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

            clsIcuAcad_PostPartumRecord_Value objRecordContent = (clsIcuAcad_PostPartumRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region set value
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(31, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR;
                objDPArr[1].Value = objRecordContent.m_strUTERUSBOTTOM_CHR;
                objDPArr[2].Value = objRecordContent.m_strUTERUSPINCH_CHR;
                objDPArr[3].Value = objRecordContent.m_strMILKNUM_CHR;
                objDPArr[4].Value = objRecordContent.m_strBREASTBULGE_CHR;
                objDPArr[5].Value = objRecordContent.m_strNIPPLE_CHR;
                objDPArr[6].Value = objRecordContent.m_strDEWNUM_CHR;
                objDPArr[7].Value = objRecordContent.m_strDEWCOLOR_CHR;
                objDPArr[8].Value = objRecordContent.m_strDEWFUCK_CHR;
                objDPArr[9].Value = objRecordContent.m_strPERINEUM_CHR;
                objDPArr[10].Value = objRecordContent.m_strBP_CHR;
                objDPArr[11].Value = objRecordContent.m_strURINE_CHR;
                objDPArr[12].Value = objRecordContent.m_strANNOTATIONS_CHR;

                objDPArr[13].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strUTERUSBOTTOM_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strUTERUSPINCH_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strMILKNUM_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strBREASTBULGE_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strNIPPLE_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strDEWNUM_CHRXML;
                objDPArr[20].Value = objRecordContent.m_strDEWCOLOR_CHRXML;
                objDPArr[21].Value = objRecordContent.m_strDEWFUCK_CHRXML;
                objDPArr[22].Value = objRecordContent.m_strPERINEUM_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strBP_CHRXML;
                objDPArr[24].Value = objRecordContent.m_strURINE_CHRXML;
                objDPArr[25].Value = objRecordContent.m_strANNOTATIONS_CHRXML;

                objDPArr[26].Value = p_objRecordContent.m_strRecordUserID;
                objDPArr[27].DbType = DbType.Date;
                objDPArr[27].Value = p_objRecordContent.m_dtmRecordDate;
                objDPArr[28].Value = lngSequence;
                objDPArr[29].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[30].DbType = DbType.Date;
                objDPArr[30].Value = p_objRecordContent.m_dtmCreateDate;

                #endregion
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

                #region set value

                lngRes = m_lngDeleteContentInfo(objRecordContent.m_strRegisterID, objRecordContent.m_dtmCreateDate);

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[2].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT;
                objDPArr2[3].Value = objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT;
                objDPArr2[4].Value = objRecordContent.m_strUTERUSPINCH_CHR_RIGHT;
                objDPArr2[5].Value = objRecordContent.m_strMILKNUM_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strBREASTBULGE_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strNIPPLE_CHR_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strDEWNUM_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strDEWCOLOR_CHR_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strDEWFUCK_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strPERINEUM_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strBP_CHR_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strURINE_CHR_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strANNOTATIONS_CHR_RIGHT;

                objDPArr2[15].Value = objRecordContent.m_strRegisterID;
                objDPArr2[16].DbType = DbType.Date;
                objDPArr2[16].Value = objRecordContent.m_dtmCreateDate;
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
            string strSql = @" update t_emr_postpartum_right t set t.status_int = 0
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

            clsIcuACAD_PostPartumseeRecord_VO objRecordContent = (clsIcuACAD_PostPartumseeRecord_VO)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
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
            /// 从ICUACAD_POSTPARTUMSEERECORD和ICUACAD_POSTPARTUMSEECONTENT获取LastModifyDate和FirstPrintDate
            /// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from t_emr_postpartum_all a
 inner join t_emr_postpartum_right b on a.registerid_chr =
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

            string c_strGetDeleteRecordContentSQL = @"select t1.postportum_num_chr,
       t1.uterusbottom_chr,
       t1.uteruspinch_chr,
       t1.milknum_chr,
       t1.breastbulge_chr,
       t1.nipple_chr,
       t1.dewnum_chr,
       t1.dewcolor_chr,
       t1.dewfuck_chr,
       t1.perineum_chr,
       t1.bp_chr,
       t1.urine_chr,
       t1.annotations_chr,
       t1.postportum_num_chrxml,
       t1.uterusbottom_chrxml,
       t1.uteruspinch_chrxml,
       t1.milknum_chrxml,
       t1.breastbulge_chrxml,
       t1.nipple_chrxml,
       t1.dewnum_chrxml,
       t1.dewcolor_chrxml,
       t1.dewfuck_chrxml,
       t1.perineum_chrxml,
       t1.bp_chrxml,
       t1.urine_chrxml,
       t1.annotations_chrxml,
       t1.registerid_chr,
       t1.createdate_dat,
       t1.createuserid_chr,
       t1.ifconfirm_int,
       t1.status_int,
       t1.deactiveddate_dat,
       t1.deactivedoperatorid_chr,
       t1.firstprintdate_dat,
       t1.recorduserid_vchr,
       t1.recorddate_dat,
       t1.sequence_int,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from t_emr_postpartum_all t1
 inner join t_emr_postpartum_right t2 on t1.registerid_chr =
                                         t2.registerid_chr
                                     and t1.createdate_dat =
                                         t2.createdate_dat
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t2.modifydate =
       (select max(modifydate)
          from icuacad_postpartumseecontent
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
                    clsIcuAcad_PostPartumRecord_Value objRecordContent = new clsIcuAcad_PostPartumRecord_Value();
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
                    objRecordContent.m_strRecordUserID = dtbValue.Rows[0]["RECORDUSERID_VCHR"].ToString();
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

                    objRecordContent.m_strANNOTATIONS_CHR = dtbValue.Rows[0]["ANNOTATIONS_CHR"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHR_RIGHT = dtbValue.Rows[0]["ANNOTATIONS_CHR_RIGHT"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHRXML = dtbValue.Rows[0]["ANNOTATIONS_CHRXML"].ToString();

                    objRecordContent.m_strBP_CHR = dtbValue.Rows[0]["BP_CHR"].ToString();
                    objRecordContent.m_strBP_CHR_RIGHT = dtbValue.Rows[0]["BP_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBP_CHRXML = dtbValue.Rows[0]["BP_CHRXML"].ToString();

                    objRecordContent.m_strBREASTBULGE_CHR = dtbValue.Rows[0]["BREASTBULGE_CHR"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHR_RIGHT = dtbValue.Rows[0]["BREASTBULGE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHRXML = dtbValue.Rows[0]["BREASTBULGE_CHRXML"].ToString();

                    objRecordContent.m_strDEWCOLOR_CHR = dtbValue.Rows[0]["DEWCOLOR_CHR"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHR_RIGHT = dtbValue.Rows[0]["DEWCOLOR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHRXML = dtbValue.Rows[0]["DEWCOLOR_CHRXML"].ToString();

                    objRecordContent.m_strDEWFUCK_CHR = dtbValue.Rows[0]["DEWFUCK_CHR"].ToString();
                    objRecordContent.m_strDEWFUCK_CHR_RIGHT = dtbValue.Rows[0]["DEWFUCK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWFUCK_CHRXML = dtbValue.Rows[0]["DEWFUCK_CHRXML"].ToString();

                    objRecordContent.m_strDEWNUM_CHR = dtbValue.Rows[0]["DEWNUM_CHR"].ToString();
                    objRecordContent.m_strDEWNUM_CHR_RIGHT = dtbValue.Rows[0]["DEWNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWNUM_CHRXML = dtbValue.Rows[0]["DEWNUM_CHRXML"].ToString();

                    objRecordContent.m_strMILKNUM_CHR = dtbValue.Rows[0]["MILKNUM_CHR"].ToString();
                    objRecordContent.m_strMILKNUM_CHR_RIGHT = dtbValue.Rows[0]["MILKNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMILKNUM_CHRXML = dtbValue.Rows[0]["MILKNUM_CHRXML"].ToString();

                    objRecordContent.m_strNIPPLE_CHR = dtbValue.Rows[0]["NIPPLE_CHR"].ToString();
                    objRecordContent.m_strNIPPLE_CHR_RIGHT = dtbValue.Rows[0]["NIPPLE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strNIPPLE_CHRXML = dtbValue.Rows[0]["NIPPLE_CHRXML"].ToString();

                    objRecordContent.m_strPERINEUM_CHR = dtbValue.Rows[0]["PERINEUM_CHR"].ToString();
                    objRecordContent.m_strPERINEUM_CHR_RIGHT = dtbValue.Rows[0]["PERINEUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPERINEUM_CHRXML = dtbValue.Rows[0]["PERINEUM_CHRXML"].ToString();

                    objRecordContent.m_strPOSTPORTUM_NUM_CHR = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHRXML = dtbValue.Rows[0]["POSTPORTUM_NUM_CHRXML"].ToString();

                    objRecordContent.m_strURINE_CHR = dtbValue.Rows[0]["URINE_CHR"].ToString();
                    objRecordContent.m_strURINE_CHR_RIGHT = dtbValue.Rows[0]["URINE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strURINE_CHRXML = dtbValue.Rows[0]["URINE_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSBOTTOM_CHR = dtbValue.Rows[0]["UTERUSBOTTOM_CHR"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT = dtbValue.Rows[0]["UTERUSBOTTOM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHRXML = dtbValue.Rows[0]["UTERUSBOTTOM_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSPINCH_CHR = dtbValue.Rows[0]["UTERUSPINCH_CHR"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHR_RIGHT = dtbValue.Rows[0]["UTERUSPINCH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHRXML = dtbValue.Rows[0]["UTERUSPINCH_CHRXML"].ToString();

                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
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
