﻿using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.DiseaseTrackService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.clsEMR_WAITLAYRECORD_GXServ
{
    /// <summary>
    /// 产程记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_WAITLAYRECORD_GXServ : clsDiseaseTrackService
    {
        #region SQL语句
        /// <summary>
        /// 从T_EMR_WAITLAYRECORD_GX获取指定病人的所有没有删除记录的时间。

        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_waitlayrecord_gx t
 where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// 从T_EMR_WAITLAYRECORD_GX中获取指定时间的表单,获取已经存在记录的主要信息

        /// InPatientID ,InPatientDate ,CreateDate,Status = 1
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_waitlayrecord_gx t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// 从T_EMR_WAITLAYRECORD_GX获取删除表单的主要信息。

        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_waitlayrecord_gx t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// 添加记录到T_EMR_WAITLAYRECORD_GX
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_waitlayrecord_gx 
(registerid_chr,createdate_dat,createuserid_chr,ifconfirm_int,status_int,recorduserid_vchr,recorddate_dat,sequence_int,
laycount_chr,beforehand,bloodpressure_s_chr,embryolocation_chr,embryoheart_chr,intermission_chr,persist_chr,intensity_chr,
palacemouth_chr,show_chr,caul_chr,anuscheck_chr,other_chr,remark_chr,bloodpressure_s_chrxml,embryolocation_chrxml,
embryoheart_chrxml,intermission_chrxml,persist_chrxml,palacemouth_chrxml,show_chrxml,caul_chrxml,anuscheck_chrxml,
other_chrxml,remark_chrxml,intensity_chrxml,bloodpressure_a_chr,bloodpressure_a_chrxml,markstatus,checkemp_chr,checkemp_xml) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 添加记录到T_EMR_WAITLAYRECORD_RIGHT_GX
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_waitlayrecord_right_gx 
(registerid_chr,createdate_dat,status_int,modifydate,modifyuserid,bloodpressure_s_chr_right,embryolocation_chr_right,
embryoheart_chr_right,intermission_chr_right,persist_chr_right,intensity_chr_right,palacemouth_chr_right,show_chr_right,
caul_chr_right,anuscheck_chr_right,other_chr_right,remark_chr_right,bloodpressure_a_chr_right,checkemp_right) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 修改记录到T_EMR_WAITLAYRECORD_GX
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_waitlayrecord_gx
   set recorduserid_vchr = ?,recorddate_dat = ?,sequence_int = ?,laycount_chr = ?,beforehand = ?,bloodpressure_s_chr = ?,
embryolocation_chr = ?,embryoheart_chr = ?,intermission_chr = ?,persist_chr = ?,intensity_chr = ?,palacemouth_chr = ?,
show_chr = ?,caul_chr = ?,anuscheck_chr = ?,other_chr = ?,remark_chr = ?,bloodpressure_s_chrxml = ?,embryolocation_chrxml = ?,
embryoheart_chrxml = ?,intermission_chrxml = ?,persist_chrxml = ?,palacemouth_chrxml = ?,show_chrxml = ?,caul_chrxml = ?,
anuscheck_chrxml = ?, other_chrxml = ?,remark_chrxml = ?,intensity_chrxml = ?,bloodpressure_a_chr = ?,bloodpressure_a_chrxml = ?,
markstatus = ?,checkemp_chr = ?,checkemp_xml = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";

        /// <summary>
        /// 修改记录到T_EMR_WAITLAYRECORD_RIGHT_GX
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 设置T_EMR_WAITLAYRECORD_GX中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_waitlayrecord_gx t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";

        /// <summary>
        /// 更新T_EMR_WAITLAYRECORD_GX中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_waitlayrecord_gx t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";


        /// <summary>
        /// 从T_EMR_WAITLAYRECORD_GX获取指定病人的所有指定删除者删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_waitlayrecord_gx t
 where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// 从T_EMR_WAITLAYRECORD_GX获取指定病人的所有已经删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_waitlayrecord_gx t
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
        public override long m_lngGetRecordTimeList(
            string p_strRegisterId, out string[] p_strCreateDateArr, out string[] p_strRecordDateArr)
        {
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngUpdateFirstPrintDate");
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
                    p_strRecordDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
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

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
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
        public override long m_lngGetDeleteRecordTimeList(
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
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
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
        public override long m_lngGetDeleteRecordTimeListAll(
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
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
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

            string strGetRecordContentSQL = @"select a.registerid_chr,
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
       a.laycount_chr,
       a.beforehand,
       a.bloodpressure_s_chr,
       a.embryolocation_chr,
       a.embryoheart_chr,
       a.intermission_chr,
       a.persist_chr,
       a.intensity_chr,
       a.palacemouth_chr,
       a.show_chr,
       a.caul_chr,
       a.anuscheck_chr,
       a.other_chr,
       a.remark_chr,
       a.bloodpressure_s_chrxml,
       a.embryolocation_chrxml,
       a.embryoheart_chrxml,
       a.intermission_chrxml,
       a.persist_chrxml,
       a.palacemouth_chrxml,
       a.show_chrxml,
       a.caul_chrxml,
       a.anuscheck_chrxml,
       a.other_chrxml,
       a.remark_chrxml,
       a.intensity_chrxml,
       a.bloodpressure_a_chr,
       a.bloodpressure_a_chrxml,
       a.markstatus,
       a.checkemp_chr,
       a.checkemp_xml,
       b.modifydate,
       b.modifyuserid,
       b.bloodpressure_s_chr_right,
       b.embryolocation_chr_right,
       b.embryoheart_chr_right,
       b.intermission_chr_right,
       b.persist_chr_right,
       b.intensity_chr_right,
       b.palacemouth_chr_right,
       b.show_chr_right,
       b.caul_chr_right,
       b.anuscheck_chr_right,
       b.other_chr_right,
       b.remark_chr_right,
       b.bloodpressure_a_chr_right,
       b.checkemp_right
  from t_emr_waitlayrecord_gx a
 inner join t_emr_waitlayrecord_right_gx b on a.registerid_chr =
                                              b.registerid_chr
                                          and a.createdate_dat =
                                              b.createdate_dat
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
                    clsEMR_WAITLAYRECORD_GX objRecordContent = new clsEMR_WAITLAYRECORD_GX();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtrSelected["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strLAYCOUNT_CHR = dtrSelected["LAYCOUNT_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["BEFOREHAND"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmBEFOREHAND = dtmTemp;

                    objRecordContent.m_strBLOODPRESSURE_S_CHR = dtrSelected["BLOODPRESSURE_S_CHR"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_A_CHR = dtrSelected["BLOODPRESSURE_A_CHR"].ToString();
                    objRecordContent.m_strEMBRYOLOCATION_CHR = dtrSelected["EMBRYOLOCATION_CHR"].ToString();
                    objRecordContent.m_strEMBRYOHEART_CHR = dtrSelected["EMBRYOHEART_CHR"].ToString();
                    objRecordContent.m_strINTERMISSION_CHR = dtrSelected["INTERMISSION_CHR"].ToString();
                    objRecordContent.m_strPERSIST_CHR = dtrSelected["PERSIST_CHR"].ToString();
                    objRecordContent.m_strINTENSITY_CHR = dtrSelected["INTENSITY_CHR"].ToString();
                    objRecordContent.m_strPALACEMOUTH_CHR = dtrSelected["PALACEMOUTH_CHR"].ToString();
                    objRecordContent.m_strSHOW_CHR = dtrSelected["SHOW_CHR"].ToString();
                    objRecordContent.m_strCAUL_CHR = dtrSelected["CAUL_CHR"].ToString();
                    objRecordContent.m_strANUSCHECK_CHR = dtrSelected["ANUSCHECK_CHR"].ToString();
                    objRecordContent.m_strOTHER_CHR = dtrSelected["OTHER_CHR"].ToString();
                    objRecordContent.m_strREMARK_CHR = dtrSelected["REMARK_CHR"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_S_CHRXML = dtrSelected["BLOODPRESSURE_S_CHRXML"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_A_CHRXML = dtrSelected["BLOODPRESSURE_A_CHRXML"].ToString();
                    objRecordContent.m_strEMBRYOLOCATION_CHRXML = dtrSelected["EMBRYOLOCATION_CHRXML"].ToString();
                    objRecordContent.m_strEMBRYOHEART_CHRXML = dtrSelected["EMBRYOHEART_CHRXML"].ToString();
                    objRecordContent.m_strINTERMISSION_CHRXML = dtrSelected["INTERMISSION_CHRXML"].ToString();
                    objRecordContent.m_strPERSIST_CHRXML = dtrSelected["PERSIST_CHRXML"].ToString();
                    objRecordContent.m_strPALACEMOUTH_CHRXML = dtrSelected["PALACEMOUTH_CHRXML"].ToString();
                    objRecordContent.m_strSHOW_CHRXML = dtrSelected["SHOW_CHRXML"].ToString();
                    objRecordContent.m_strCAUL_CHRXML = dtrSelected["CAUL_CHRXML"].ToString();
                    objRecordContent.m_strANUSCHECK_CHRXML = dtrSelected["ANUSCHECK_CHRXML"].ToString();
                    objRecordContent.m_strOTHER_CHRXML = dtrSelected["OTHER_CHRXML"].ToString();
                    objRecordContent.m_strREMARK_CHRXML = dtrSelected["REMARK_CHRXML"].ToString();
                    objRecordContent.m_strINTENSITY_CHRXML = dtrSelected["INTENSITY_CHRXML"].ToString();

                    objRecordContent.m_strBLOODPRESSURE_S_CHR_RIGHT = dtrSelected["BLOODPRESSURE_S_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_A_CHR_RIGHT = dtrSelected["BLOODPRESSURE_A_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEMBRYOLOCATION_CHR_RIGHT = dtrSelected["EMBRYOLOCATION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEMBRYOHEART_CHR_RIGHT = dtrSelected["EMBRYOHEART_CHR_RIGHT"].ToString();
                    objRecordContent.m_strINTERMISSION_CHR_RIGHT = dtrSelected["INTERMISSION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPERSIST_CHR_RIGHT = dtrSelected["PERSIST_CHR_RIGHT"].ToString();
                    objRecordContent.m_strINTENSITY_CHR_RIGHT = dtrSelected["INTENSITY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPALACEMOUTH_CHR_RIGHT = dtrSelected["PALACEMOUTH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strSHOW_CHR_RIGHT = dtrSelected["SHOW_CHR_RIGHT"].ToString();
                    objRecordContent.m_strCAUL_CHR_RIGHT = dtrSelected["CAUL_CHR_RIGHT"].ToString();
                    objRecordContent.m_strANUSCHECK_CHR_RIGHT = dtrSelected["ANUSCHECK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strOTHER_CHR_RIGHT = dtrSelected["OTHER_CHR_RIGHT"].ToString();
                    objRecordContent.m_strREMARK_CHR_RIGHT = dtrSelected["REMARK_CHR_RIGHT"].ToString();
                    objRecordContent.m_intMarkStatus = Convert.ToInt32(dtrSelected["MARKSTATUS"]);

                    objRecordContent.m_strCHECKEMP_CHR = dtrSelected["CHECKEMP_CHR"].ToString();
                    objRecordContent.m_strCHECKEMP_RIGHT = dtrSelected["CHECKEMP_RIGHT"].ToString();
                    objRecordContent.m_strCHECKEMP_XML = dtrSelected["CHECKEMP_XML"].ToString();
                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
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
                objHRPServ = null;
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
                objHRPServ = null;
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
            clsEMR_WAITLAYRECORD_GX objRecordContent = (clsEMR_WAITLAYRECORD_GX)p_objRecordContent;
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
                objHRPServ.CreateDatabaseParameter(39, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[2].Value = objRecordContent.m_strCreateUserID;
                objDPArr[3].Value = 0;
                objDPArr[4].Value = 1;
                objDPArr[5].Value = objRecordContent.m_strRecordUserID;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[7].Value = lngSequence;
                objDPArr[8].Value = objRecordContent.m_strLAYCOUNT_CHR;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = objRecordContent.m_dtmBEFOREHAND;
                objDPArr[10].Value = objRecordContent.m_strBLOODPRESSURE_S_CHR;
                objDPArr[11].Value = objRecordContent.m_strEMBRYOLOCATION_CHR;
                objDPArr[12].Value = objRecordContent.m_strEMBRYOHEART_CHR;
                objDPArr[13].Value = objRecordContent.m_strINTERMISSION_CHR;
                objDPArr[14].Value = objRecordContent.m_strPERSIST_CHR;
                objDPArr[15].Value = objRecordContent.m_strINTENSITY_CHR;
                objDPArr[16].Value = objRecordContent.m_strPALACEMOUTH_CHR;
                objDPArr[17].Value = objRecordContent.m_strSHOW_CHR;
                objDPArr[18].Value = objRecordContent.m_strCAUL_CHR;
                objDPArr[19].Value = objRecordContent.m_strANUSCHECK_CHR;
                objDPArr[20].Value = objRecordContent.m_strOTHER_CHR;
                objDPArr[21].Value = objRecordContent.m_strREMARK_CHR;
                objDPArr[22].Value = objRecordContent.m_strBLOODPRESSURE_S_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strEMBRYOLOCATION_CHRXML;
                objDPArr[24].Value = objRecordContent.m_strEMBRYOHEART_CHRXML;
                objDPArr[25].Value = objRecordContent.m_strINTERMISSION_CHRXML;
                objDPArr[26].Value = objRecordContent.m_strPERSIST_CHRXML;
                objDPArr[27].Value = objRecordContent.m_strPALACEMOUTH_CHRXML;
                objDPArr[28].Value = objRecordContent.m_strSHOW_CHRXML;
                objDPArr[29].Value = objRecordContent.m_strCAUL_CHRXML;
                objDPArr[30].Value = objRecordContent.m_strANUSCHECK_CHRXML;
                objDPArr[31].Value = objRecordContent.m_strOTHER_CHRXML;
                objDPArr[32].Value = objRecordContent.m_strREMARK_CHRXML;
                objDPArr[33].Value = objRecordContent.m_strINTENSITY_CHRXML;
                objDPArr[34].Value = objRecordContent.m_strBLOODPRESSURE_A_CHR;
                objDPArr[35].Value = objRecordContent.m_strBLOODPRESSURE_A_CHRXML;
                objDPArr[36].Value = objRecordContent.m_intMarkStatus;
                objDPArr[37].Value = objRecordContent.m_strCHECKEMP_CHR;
                objDPArr[38].Value = objRecordContent.m_strCHECKEMP_XML;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(19, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strBLOODPRESSURE_S_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strEMBRYOLOCATION_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strEMBRYOHEART_CHR_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strINTERMISSION_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strPERSIST_CHR_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strINTENSITY_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strPALACEMOUTH_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strSHOW_CHR_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strCAUL_CHR_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strANUSCHECK_CHR_RIGHT;
                objDPArr2[15].Value = objRecordContent.m_strOTHER_CHR_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strREMARK_CHR_RIGHT;
                objDPArr2[17].Value = objRecordContent.m_strBLOODPRESSURE_A_CHR_RIGHT;
                objDPArr2[18].Value = objRecordContent.m_strCHECKEMP_RIGHT;
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

            clsEMR_WAITLAYRECORD_GX objRecordContent = (clsEMR_WAITLAYRECORD_GX)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。

            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_waitlayrecord_right_gx t2
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
                objHRPServ = null;

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

            clsEMR_WAITLAYRECORD_GX objRecordContent = (clsEMR_WAITLAYRECORD_GX)p_objRecordContent;
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
                objHRPServ.CreateDatabaseParameter(36, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRecordUserID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].Value = objRecordContent.m_strLAYCOUNT_CHR;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmBEFOREHAND;
                objDPArr[5].Value = objRecordContent.m_strBLOODPRESSURE_S_CHR;
                objDPArr[6].Value = objRecordContent.m_strEMBRYOLOCATION_CHR;
                objDPArr[7].Value = objRecordContent.m_strEMBRYOHEART_CHR;
                objDPArr[8].Value = objRecordContent.m_strINTERMISSION_CHR;
                objDPArr[9].Value = objRecordContent.m_strPERSIST_CHR;
                objDPArr[10].Value = objRecordContent.m_strINTENSITY_CHR;
                objDPArr[11].Value = objRecordContent.m_strPALACEMOUTH_CHR;
                objDPArr[12].Value = objRecordContent.m_strSHOW_CHR;
                objDPArr[13].Value = objRecordContent.m_strCAUL_CHR;
                objDPArr[14].Value = objRecordContent.m_strANUSCHECK_CHR;
                objDPArr[15].Value = objRecordContent.m_strOTHER_CHR;
                objDPArr[16].Value = objRecordContent.m_strREMARK_CHR;
                objDPArr[17].Value = objRecordContent.m_strBLOODPRESSURE_S_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strEMBRYOLOCATION_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strEMBRYOHEART_CHRXML;
                objDPArr[20].Value = objRecordContent.m_strINTERMISSION_CHRXML;
                objDPArr[21].Value = objRecordContent.m_strPERSIST_CHRXML;
                objDPArr[22].Value = objRecordContent.m_strPALACEMOUTH_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strSHOW_CHRXML;
                objDPArr[24].Value = objRecordContent.m_strCAUL_CHRXML;
                objDPArr[25].Value = objRecordContent.m_strANUSCHECK_CHRXML;
                objDPArr[26].Value = objRecordContent.m_strOTHER_CHRXML;
                objDPArr[27].Value = objRecordContent.m_strREMARK_CHRXML;
                objDPArr[28].Value = objRecordContent.m_strINTENSITY_CHRXML;
                objDPArr[29].Value = objRecordContent.m_strBLOODPRESSURE_A_CHR;
                objDPArr[30].Value = objRecordContent.m_strBLOODPRESSURE_A_CHRXML;
                objDPArr[31].Value = objRecordContent.m_intMarkStatus;
                objDPArr[32].Value = objRecordContent.m_strCHECKEMP_CHR;
                objDPArr[33].Value = objRecordContent.m_strCHECKEMP_XML;

                objDPArr[34].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[35].DbType = DbType.Date;
                objDPArr[35].Value = p_objRecordContent.m_dtmCreateDate;

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
                objHRPServ.CreateDatabaseParameter(19, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strBLOODPRESSURE_S_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strEMBRYOLOCATION_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strEMBRYOHEART_CHR_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strINTERMISSION_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strPERSIST_CHR_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strINTENSITY_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strPALACEMOUTH_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strSHOW_CHR_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strCAUL_CHR_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strANUSCHECK_CHR_RIGHT;
                objDPArr2[15].Value = objRecordContent.m_strOTHER_CHR_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strREMARK_CHR_RIGHT;
                objDPArr2[17].Value = objRecordContent.m_strBLOODPRESSURE_A_CHR_RIGHT;
                objDPArr2[18].Value = objRecordContent.m_strCHECKEMP_RIGHT;
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
                objHRPServ = null;

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
            string strSql = @" update t_emr_waitlayrecord_right_gx t set t.status_int = 0
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

            clsEMR_WAITLAYRECORD_GX objRecordContent = (clsEMR_WAITLAYRECORD_GX)p_objRecordContent;
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
            /// 从T_EMR_WAITLAYRECORD_GX和T_EMR_WAITLAYRECORD_RIGHT_GX获取LastModifyDate和FirstPrintDate
            /// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from t_emr_waitlayrecord_gx a
 inner join t_emr_waitlayrecord_right_gx b on a.registerid_chr =
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

            string c_strGetDeleteRecordContentSQL = @"select t1.registerid_chr,
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
       t1.laycount_chr,
       t1.beforehand,
       t1.bloodpressure_s_chr,
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
       t1.remark_chr,
       t1.bloodpressure_s_chrxml,
       t1.embryolocation_chrxml,
       t1.embryoheart_chrxml,
       t1.intermission_chrxml,
       t1.persist_chrxml,
       t1.palacemouth_chrxml,
       t1.show_chrxml,
       t1.caul_chrxml,
       t1.anuscheck_chrxml,
       t1.other_chrxml,
       t1.remark_chrxml,
       t1.intensity_chrxml,
       t1.bloodpressure_a_chr,
       t1.bloodpressure_a_chrxml,
       t1.markstatus,
       t1.checkemp_chr,
       t1.checkemp_xml,
       t2.modifydate,
       t2.modifyuserid,
       t2.bloodpressure_s_chr_right,
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
       t2.remark_chr_right,
       t2.bloodpressure_a_chr_right,
       t2.checkemp_right
  from t_emr_waitlayrecord_gx t1
 inner join t_emr_waitlayrecord_right_gx t2 on t1.registerid_chr =
                                               t2.registerid_chr
                                           and t1.createdate_dat =
                                               t2.createdate_dat
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t2.modifydate = (select max(modifydate)
                          from t_emr_waitlayrecord_right_gx
                         where registerid_chr = ?
                           and createdate_dat = ?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                objDPArr[2].Value = p_strRegisterId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strCreatedDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region 设置结果
                    clsEMR_WAITLAYRECORD_GX objRecordContent = new clsEMR_WAITLAYRECORD_GX();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtrSelected["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strLAYCOUNT_CHR = dtrSelected["LAYCOUNT_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["BEFOREHAND"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmBEFOREHAND = dtmTemp;

                    objRecordContent.m_strBLOODPRESSURE_S_CHR = dtrSelected["BLOODPRESSURE_S_CHR"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_A_CHR = dtrSelected["BLOODPRESSURE_A_CHR"].ToString();
                    objRecordContent.m_strEMBRYOLOCATION_CHR = dtrSelected["EMBRYOLOCATION_CHR"].ToString();
                    objRecordContent.m_strEMBRYOHEART_CHR = dtrSelected["EMBRYOHEART_CHR"].ToString();
                    objRecordContent.m_strINTERMISSION_CHR = dtrSelected["INTERMISSION_CHR"].ToString();
                    objRecordContent.m_strPERSIST_CHR = dtrSelected["PERSIST_CHR"].ToString();
                    objRecordContent.m_strINTENSITY_CHR = dtrSelected["INTENSITY_CHR"].ToString();
                    objRecordContent.m_strPALACEMOUTH_CHR = dtrSelected["PALACEMOUTH_CHR"].ToString();
                    objRecordContent.m_strSHOW_CHR = dtrSelected["SHOW_CHR"].ToString();
                    objRecordContent.m_strCAUL_CHR = dtrSelected["CAUL_CHR"].ToString();
                    objRecordContent.m_strANUSCHECK_CHR = dtrSelected["ANUSCHECK_CHR"].ToString();
                    objRecordContent.m_strOTHER_CHR = dtrSelected["OTHER_CHR"].ToString();
                    objRecordContent.m_strREMARK_CHR = dtrSelected["REMARK_CHR"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_S_CHRXML = dtrSelected["BLOODPRESSURE_S_CHRXML"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_A_CHRXML = dtrSelected["BLOODPRESSURE_A_CHRXML"].ToString();
                    objRecordContent.m_strEMBRYOLOCATION_CHRXML = dtrSelected["EMBRYOLOCATION_CHRXML"].ToString();
                    objRecordContent.m_strEMBRYOHEART_CHRXML = dtrSelected["EMBRYOHEART_CHRXML"].ToString();
                    objRecordContent.m_strINTERMISSION_CHRXML = dtrSelected["INTERMISSION_CHRXML"].ToString();
                    objRecordContent.m_strPERSIST_CHRXML = dtrSelected["PERSIST_CHRXML"].ToString();
                    objRecordContent.m_strPALACEMOUTH_CHRXML = dtrSelected["PALACEMOUTH_CHRXML"].ToString();
                    objRecordContent.m_strSHOW_CHRXML = dtrSelected["SHOW_CHRXML"].ToString();
                    objRecordContent.m_strCAUL_CHRXML = dtrSelected["CAUL_CHRXML"].ToString();
                    objRecordContent.m_strANUSCHECK_CHRXML = dtrSelected["ANUSCHECK_CHRXML"].ToString();
                    objRecordContent.m_strOTHER_CHRXML = dtrSelected["OTHER_CHRXML"].ToString();
                    objRecordContent.m_strREMARK_CHRXML = dtrSelected["REMARK_CHRXML"].ToString();
                    objRecordContent.m_strINTENSITY_CHRXML = dtrSelected["INTENSITY_CHRXML"].ToString();

                    objRecordContent.m_strBLOODPRESSURE_S_CHR_RIGHT = dtrSelected["BLOODPRESSURE_S_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_A_CHR_RIGHT = dtrSelected["BLOODPRESSURE_A_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEMBRYOLOCATION_CHR_RIGHT = dtrSelected["EMBRYOLOCATION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEMBRYOHEART_CHR_RIGHT = dtrSelected["EMBRYOHEART_CHR_RIGHT"].ToString();
                    objRecordContent.m_strINTERMISSION_CHR_RIGHT = dtrSelected["INTERMISSION_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPERSIST_CHR_RIGHT = dtrSelected["PERSIST_CHR_RIGHT"].ToString();
                    objRecordContent.m_strINTENSITY_CHR_RIGHT = dtrSelected["INTENSITY_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPALACEMOUTH_CHR_RIGHT = dtrSelected["PALACEMOUTH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strSHOW_CHR_RIGHT = dtrSelected["SHOW_CHR_RIGHT"].ToString();
                    objRecordContent.m_strCAUL_CHR_RIGHT = dtrSelected["CAUL_CHR_RIGHT"].ToString();
                    objRecordContent.m_strANUSCHECK_CHR_RIGHT = dtrSelected["ANUSCHECK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strOTHER_CHR_RIGHT = dtrSelected["OTHER_CHR_RIGHT"].ToString();
                    objRecordContent.m_strREMARK_CHR_RIGHT = dtrSelected["REMARK_CHR_RIGHT"].ToString();

                    objRecordContent.m_strCHECKEMP_CHR = dtrSelected["CHECKEMP_CHR"].ToString();
                    objRecordContent.m_strCHECKEMP_RIGHT = dtrSelected["CHECKEMP_RIGHT"].ToString();
                    objRecordContent.m_strCHECKEMP_XML = dtrSelected["CHECKEMP_XML"].ToString();
                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
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
