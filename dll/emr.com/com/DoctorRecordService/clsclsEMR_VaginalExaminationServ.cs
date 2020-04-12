using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.DiseaseTrackService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 阴道检查记录表
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_VaginalExaminationServ : clsDiseaseTrackService
    {
        #region SQL语句
        /// <summary>
        /// 从T_EMR_VAGINALEXAMINATION获取指定病人的所有没有删除记录的时间。

        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_vaginalexamination t
 where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// 从T_EMR_VAGINALEXAMINATION中获取指定时间的表单,获取已经存在记录的主要信息

        /// InPatientID ,InPatientDate ,CreateDate,Status = 1
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_vaginalexamination t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// 从T_EMR_VAGINALEXAMINATION获取删除表单的主要信息。

        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_vaginalexamination t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// 添加记录到T_EMR_VAGINALEXAMINATION
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_vaginalexamination 
(registerid_chr,createdate_dat,createuserid_chr,ifconfirm_int,status_int,recorduserid_vchr,recorddate_dat,sequence_int,
examinationtime_dat,examinationreason_vchr,metreurysis_vchr,presentation_vchr,presentationheight_vchr,presentationorientation_vchr,
skull_vchr,overlapping_vchr,caputsuccedaneum_vchr,rupturedfetalmembranes_chr,rupturedmode_chr,rupturetime_dat,amnioticfluid_vchr,
amnioticfluidcharacter_vchr,fhr_vchr,ischialspine_vchr,coccyx_chr,sacralbone_vchr,pubicarch_vchr,dc_vchr,ischiumnotch_vchr,
urethralcatheterization_chr,piss_vchr,uccharacter_vchr,project_vchr,examinationreason_xml,metreurysis_xml,presentation_xml,
presentationheight_xml,presentationorientation_xml,skull_xml,overlapping_xml,caputsuccedaneum_xml,amnioticfluid_xml,
amnioticfluidcharacter_xml,fhr_xml,ischialspine_xml,sacralbone_xml,pubicarch_xml,dc_xml,ischiumnotch_xml,piss_xml,uccharacter_xml,
project_xml) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?)";

        /// <summary>
        /// 添加记录到T_EMR_VAGINALEXAMINATIONCON
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_vaginalexaminationcon 
(registerid_chr,createdate_dat,status_int,modifydate,modifyuserid,examinationreason_right,metreurysis_right,presentation_right,
presentationheight_right,presentationorientation_right,skull_right,overlapping_right,caputsuccedaneum_right,amnioticfluid_right,
amnioticfluidcharacter_right,fhr_right,ischialspine_right,sacralbone_right,pubicarch_right,dc_right,ischiumnotch_right,
piss_right,uccharacter_right,project_right) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?)";

        /// <summary>
        /// 修改记录到T_EMR_VAGINALEXAMINATION
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_vaginalexamination set
recorduserid_vchr = ?,recorddate_dat = ?,sequence_int = ?, examinationtime_dat = ?,examinationreason_vchr = ?,metreurysis_vchr = ?,
presentation_vchr = ?,presentationheight_vchr = ?,presentationorientation_vchr = ?,skull_vchr = ?,overlapping_vchr = ?,
caputsuccedaneum_vchr = ?,rupturedfetalmembranes_chr = ?,rupturedmode_chr = ?,rupturetime_dat = ?,amnioticfluid_vchr = ?,
amnioticfluidcharacter_vchr = ?,fhr_vchr = ?,ischialspine_vchr = ?,coccyx_chr = ?,sacralbone_vchr = ?,pubicarch_vchr = ?,dc_vchr = ?,
ischiumnotch_vchr = ?,urethralcatheterization_chr = ?,piss_vchr = ?,uccharacter_vchr = ?,project_vchr = ?,examinationreason_xml = ?,
metreurysis_xml = ?,presentation_xml = ?,presentationheight_xml = ?,presentationorientation_xml = ?,skull_xml = ?,overlapping_xml = ?,
caputsuccedaneum_xml = ?,amnioticfluid_xml = ?,amnioticfluidcharacter_xml = ?,fhr_xml = ?,ischialspine_xml = ?,sacralbone_xml = ?,
pubicarch_xml = ?,dc_xml = ?,ischiumnotch_xml = ?,piss_xml = ?,uccharacter_xml = ?,project_xml = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//49个参数


        /// <summary>
        /// 修改记录到T_EMR_OXTINTRAVENOUSDRIP_RIGHT
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 设置T_EMR_OXTINTRAVENOUSDRIP中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_vaginalexamination t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";

        /// <summary>
        /// 更新T_EMR_OXTINTRAVENOUSDRIP中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_vaginalexamination t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";


        /// <summary>
        /// 从T_EMR_OXTINTRAVENOUSDRIP获取指定病人的所有指定删除者删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_vaginalexamination t
 where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// 从T_EMR_OXTINTRAVENOUSDRIP获取指定病人的所有已经删除的记录时间。

        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_vaginalexamination t
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
        /// 根据住院号获取指定记录的内容.
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">住院日期</param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, clsHRPTableService p_objHRPServ, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //检查参数


            if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
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
       a.examinationtime_dat,
       a.examinationreason_vchr,
       a.metreurysis_vchr,
       a.presentation_vchr,
       a.presentationheight_vchr,
       a.presentationorientation_vchr,
       a.skull_vchr,
       a.overlapping_vchr,
       a.caputsuccedaneum_vchr,
       a.rupturedfetalmembranes_chr,
       a.rupturedmode_chr,
       a.rupturetime_dat,
       a.amnioticfluid_vchr,
       a.amnioticfluidcharacter_vchr,
       a.fhr_vchr,
       a.ischialspine_vchr,
       a.coccyx_chr,
       a.sacralbone_vchr,
       a.pubicarch_vchr,
       a.dc_vchr,
       a.ischiumnotch_vchr,
       a.urethralcatheterization_chr,
       a.piss_vchr,
       a.uccharacter_vchr,
       a.project_vchr,
       a.examinationreason_xml,
       a.metreurysis_xml,
       a.presentation_xml,
       a.presentationheight_xml,
       a.presentationorientation_xml,
       a.skull_xml,
       a.overlapping_xml,
       a.caputsuccedaneum_xml,
       a.amnioticfluid_xml,
       a.amnioticfluidcharacter_xml,
       a.fhr_xml,
       a.ischialspine_xml,
       a.sacralbone_xml,
       a.pubicarch_xml,
       a.dc_xml,
       a.ischiumnotch_xml,
       a.piss_xml,
       a.uccharacter_xml,
       a.project_xml,
       b.status_int,
       b.modifydate,
       b.modifyuserid,
       b.examinationreason_right,
       b.metreurysis_right,
       b.presentation_right,
       b.presentationheight_right,
       b.presentationorientation_right,
       b.skull_right,
       b.overlapping_right,
       b.caputsuccedaneum_right,
       b.amnioticfluid_right,
       b.amnioticfluidcharacter_right,
       b.fhr_right,
       b.ischialspine_right,
       b.sacralbone_right,
       b.pubicarch_right,
       b.dc_right,
       b.ischiumnotch_right,
       b.piss_right,
       b.uccharacter_right,
       b.project_right
  from t_emr_vaginalexamination a
 inner join t_emr_vaginalexaminationcon b on a.registerid_chr =
                                             b.registerid_chr
                                         and a.createdate_dat =
                                             b.createdate_dat
 inner join t_bse_hisemr_relation c on a.registerid_chr = c.registerid_chr
 where a.status_int = 1
   and b.status_int = 1
   and c.emrinpatientid = ?
   and c.emrinpatientdate = ?";

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
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果


                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    #region 设置结果
                    clsEMR_VaginalExaminationValue objRecordContent = new clsEMR_VaginalExaminationValue();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = dtrSelected["registerid_chr"].ToString();
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

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["EXAMINATIONTIME_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmEXAMINATIONTIME_DAT = dtmTemp;

                    objRecordContent.m_strEXAMINATIONREASON_VCHR = dtrSelected["EXAMINATIONREASON_VCHR"].ToString();
                    objRecordContent.m_strMETREURYSIS_VCHR = dtrSelected["METREURYSIS_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATION_VCHR = dtrSelected["PRESENTATION_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_VCHR = dtrSelected["PRESENTATIONHEIGHT_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_VCHR = dtrSelected["PRESENTATIONORIENTATION_VCHR"].ToString();
                    objRecordContent.m_strSKULL_VCHR = dtrSelected["SKULL_VCHR"].ToString();
                    objRecordContent.m_strOVERLAPPING_VCHR = dtrSelected["OVERLAPPING_VCHR"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_VCHR = dtrSelected["CAPUTSUCCEDANEUM_VCHR"].ToString();
                    objRecordContent.m_strRUPTUREDFETALMEMBRANES_CHR = dtrSelected["RUPTUREDFETALMEMBRANES_CHR"].ToString();
                    objRecordContent.m_strRUPTUREDMODE_CHR = dtrSelected["RUPTUREDMODE_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RUPTURETIME_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRUPTURETIME_DAT = dtmTemp;

                    objRecordContent.m_strAMNIOTICFLUID_VCHR = dtrSelected["AMNIOTICFLUID_VCHR"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_VCHR = dtrSelected["AMNIOTICFLUIDCHARACTER_VCHR"].ToString();
                    objRecordContent.m_strFHR_VCHR = dtrSelected["FHR_VCHR"].ToString();
                    objRecordContent.m_strISCHIALSPINE_VCHR = dtrSelected["ISCHIALSPINE_VCHR"].ToString();
                    objRecordContent.m_strCOCCYX_CHR = dtrSelected["COCCYX_CHR"].ToString();
                    objRecordContent.m_strSACRALBONE_VCHR = dtrSelected["SACRALBONE_VCHR"].ToString();
                    objRecordContent.m_strPUBICARCH_VCHR = dtrSelected["PUBICARCH_VCHR"].ToString();
                    objRecordContent.m_strDC_VCHR = dtrSelected["DC_VCHR"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_VCHR = dtrSelected["ISCHIUMNOTCH_VCHR"].ToString();
                    objRecordContent.m_strURETHRALCATHETERIZATION_CHR = dtrSelected["URETHRALCATHETERIZATION_CHR"].ToString();
                    objRecordContent.m_strPISS_VCHR = dtrSelected["PISS_VCHR"].ToString();
                    objRecordContent.m_strUCCHARACTER_VCHR = dtrSelected["UCCHARACTER_VCHR"].ToString();
                    objRecordContent.m_strPROJECT_VCHR = dtrSelected["PROJECT_VCHR"].ToString();
                    objRecordContent.m_strEXAMINATIONREASON_XML = dtrSelected["EXAMINATIONREASON_XML"].ToString();
                    objRecordContent.m_strMETREURYSIS_XML = dtrSelected["METREURYSIS_XML"].ToString();
                    objRecordContent.m_strPRESENTATION_XML = dtrSelected["PRESENTATION_XML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_XML = dtrSelected["PRESENTATIONHEIGHT_XML"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_XML = dtrSelected["PRESENTATIONORIENTATION_XML"].ToString();
                    objRecordContent.m_strSKULL_XML = dtrSelected["SKULL_XML"].ToString();
                    objRecordContent.m_strOVERLAPPING_XML = dtrSelected["OVERLAPPING_XML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_XML = dtrSelected["CAPUTSUCCEDANEUM_XML"].ToString();
                    objRecordContent.m_strAMNIOTICFLUID_XML = dtrSelected["AMNIOTICFLUID_XML"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_XML = dtrSelected["AMNIOTICFLUIDCHARACTER_XML"].ToString();
                    objRecordContent.m_strFHR_XML = dtrSelected["FHR_XML"].ToString();
                    objRecordContent.m_strISCHIALSPINE_XML = dtrSelected["ISCHIALSPINE_XML"].ToString();
                    objRecordContent.m_strSACRALBONE_XML = dtrSelected["SACRALBONE_XML"].ToString();
                    objRecordContent.m_strPUBICARCH_XML = dtrSelected["PUBICARCH_XML"].ToString();
                    objRecordContent.m_strDC_XML = dtrSelected["DC_XML"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_XML = dtrSelected["ISCHIUMNOTCH_XML"].ToString();
                    objRecordContent.m_strPISS_XML = dtrSelected["PISS_XML"].ToString();
                    objRecordContent.m_strUCCHARACTER_XML = dtrSelected["UCCHARACTER_XML"].ToString();
                    objRecordContent.m_strPROJECT_XML = dtrSelected["PROJECT_XML"].ToString();

                    objRecordContent.m_strEXAMINATIONREASON_RIGHT = dtrSelected["EXAMINATIONREASON_RIGHT"].ToString();
                    objRecordContent.m_strMETREURYSIS_RIGHT = dtrSelected["METREURYSIS_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION_RIGHT = dtrSelected["PRESENTATION_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_RIGHT = dtrSelected["PRESENTATIONHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_RIGHT = dtrSelected["PRESENTATIONORIENTATION_RIGHT"].ToString();
                    objRecordContent.m_strSKULL_RIGHT = dtrSelected["SKULL_RIGHT"].ToString();
                    objRecordContent.m_strOVERLAPPING_RIGHT = dtrSelected["OVERLAPPING_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_RIGHT = dtrSelected["CAPUTSUCCEDANEUM_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOTICFLUID_RIGHT = dtrSelected["AMNIOTICFLUID_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_RIGHT = dtrSelected["AMNIOTICFLUIDCHARACTER_RIGHT"].ToString();
                    objRecordContent.m_strFHR_RIGHT = dtrSelected["FHR_RIGHT"].ToString();
                    objRecordContent.m_strISCHIALSPINE_RIGHT = dtrSelected["ISCHIALSPINE_RIGHT"].ToString();
                    objRecordContent.m_strSACRALBONE_RIGHT = dtrSelected["SACRALBONE_RIGHT"].ToString();
                    objRecordContent.m_strPUBICARCH_RIGHT = dtrSelected["PUBICARCH_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtrSelected["DC_RIGHT"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_RIGHT = dtrSelected["ISCHIUMNOTCH_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtrSelected["PISS_RIGHT"].ToString();
                    objRecordContent.m_strUCCHARACTER_RIGHT = dtrSelected["UCCHARACTER_RIGHT"].ToString();
                    objRecordContent.m_strPROJECT_RIGHT = dtrSelected["PROJECT_RIGHT"].ToString();

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
       a.examinationtime_dat,
       a.examinationreason_vchr,
       a.metreurysis_vchr,
       a.presentation_vchr,
       a.presentationheight_vchr,
       a.presentationorientation_vchr,
       a.skull_vchr,
       a.overlapping_vchr,
       a.caputsuccedaneum_vchr,
       a.rupturedfetalmembranes_chr,
       a.rupturedmode_chr,
       a.rupturetime_dat,
       a.amnioticfluid_vchr,
       a.amnioticfluidcharacter_vchr,
       a.fhr_vchr,
       a.ischialspine_vchr,
       a.coccyx_chr,
       a.sacralbone_vchr,
       a.pubicarch_vchr,
       a.dc_vchr,
       a.ischiumnotch_vchr,
       a.urethralcatheterization_chr,
       a.piss_vchr,
       a.uccharacter_vchr,
       a.project_vchr,
       a.examinationreason_xml,
       a.metreurysis_xml,
       a.presentation_xml,
       a.presentationheight_xml,
       a.presentationorientation_xml,
       a.skull_xml,
       a.overlapping_xml,
       a.caputsuccedaneum_xml,
       a.amnioticfluid_xml,
       a.amnioticfluidcharacter_xml,
       a.fhr_xml,
       a.ischialspine_xml,
       a.sacralbone_xml,
       a.pubicarch_xml,
       a.dc_xml,
       a.ischiumnotch_xml,
       a.piss_xml,
       a.uccharacter_xml,
       a.project_xml,
       b.status_int,
       b.modifydate,
       b.modifyuserid,
       b.examinationreason_right,
       b.metreurysis_right,
       b.presentation_right,
       b.presentationheight_right,
       b.presentationorientation_right,
       b.skull_right,
       b.overlapping_right,
       b.caputsuccedaneum_right,
       b.amnioticfluid_right,
       b.amnioticfluidcharacter_right,
       b.fhr_right,
       b.ischialspine_right,
       b.sacralbone_right,
       b.pubicarch_right,
       b.dc_right,
       b.ischiumnotch_right,
       b.piss_right,
       b.uccharacter_right,
       b.project_right
  from t_emr_vaginalexamination a
 inner join t_emr_vaginalexaminationcon b on a.registerid_chr =
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

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    #region 设置结果
                    clsEMR_VaginalExaminationValue objRecordContent = new clsEMR_VaginalExaminationValue();
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

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["EXAMINATIONTIME_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmEXAMINATIONTIME_DAT = dtmTemp;

                    objRecordContent.m_strEXAMINATIONREASON_VCHR = dtrSelected["EXAMINATIONREASON_VCHR"].ToString();
                    objRecordContent.m_strMETREURYSIS_VCHR = dtrSelected["METREURYSIS_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATION_VCHR = dtrSelected["PRESENTATION_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_VCHR = dtrSelected["PRESENTATIONHEIGHT_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_VCHR = dtrSelected["PRESENTATIONORIENTATION_VCHR"].ToString();
                    objRecordContent.m_strSKULL_VCHR = dtrSelected["SKULL_VCHR"].ToString();
                    objRecordContent.m_strOVERLAPPING_VCHR = dtrSelected["OVERLAPPING_VCHR"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_VCHR = dtrSelected["CAPUTSUCCEDANEUM_VCHR"].ToString();
                    objRecordContent.m_strRUPTUREDFETALMEMBRANES_CHR = dtrSelected["RUPTUREDFETALMEMBRANES_CHR"].ToString();
                    objRecordContent.m_strRUPTUREDMODE_CHR = dtrSelected["RUPTUREDMODE_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RUPTURETIME_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRUPTURETIME_DAT = dtmTemp;

                    objRecordContent.m_strAMNIOTICFLUID_VCHR = dtrSelected["AMNIOTICFLUID_VCHR"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_VCHR = dtrSelected["AMNIOTICFLUIDCHARACTER_VCHR"].ToString();
                    objRecordContent.m_strFHR_VCHR = dtrSelected["FHR_VCHR"].ToString();
                    objRecordContent.m_strISCHIALSPINE_VCHR = dtrSelected["ISCHIALSPINE_VCHR"].ToString();
                    objRecordContent.m_strCOCCYX_CHR = dtrSelected["COCCYX_CHR"].ToString();
                    objRecordContent.m_strSACRALBONE_VCHR = dtrSelected["SACRALBONE_VCHR"].ToString();
                    objRecordContent.m_strPUBICARCH_VCHR = dtrSelected["PUBICARCH_VCHR"].ToString();
                    objRecordContent.m_strDC_VCHR = dtrSelected["DC_VCHR"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_VCHR = dtrSelected["ISCHIUMNOTCH_VCHR"].ToString();
                    objRecordContent.m_strURETHRALCATHETERIZATION_CHR = dtrSelected["URETHRALCATHETERIZATION_CHR"].ToString();
                    objRecordContent.m_strPISS_VCHR = dtrSelected["PISS_VCHR"].ToString();
                    objRecordContent.m_strUCCHARACTER_VCHR = dtrSelected["UCCHARACTER_VCHR"].ToString();
                    objRecordContent.m_strPROJECT_VCHR = dtrSelected["PROJECT_VCHR"].ToString();
                    objRecordContent.m_strEXAMINATIONREASON_XML = dtrSelected["EXAMINATIONREASON_XML"].ToString();
                    objRecordContent.m_strMETREURYSIS_XML = dtrSelected["METREURYSIS_XML"].ToString();
                    objRecordContent.m_strPRESENTATION_XML = dtrSelected["PRESENTATION_XML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_XML = dtrSelected["PRESENTATIONHEIGHT_XML"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_XML = dtrSelected["PRESENTATIONORIENTATION_XML"].ToString();
                    objRecordContent.m_strSKULL_XML = dtrSelected["SKULL_XML"].ToString();
                    objRecordContent.m_strOVERLAPPING_XML = dtrSelected["OVERLAPPING_XML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_XML = dtrSelected["CAPUTSUCCEDANEUM_XML"].ToString();
                    objRecordContent.m_strAMNIOTICFLUID_XML = dtrSelected["AMNIOTICFLUID_XML"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_XML = dtrSelected["AMNIOTICFLUIDCHARACTER_XML"].ToString();
                    objRecordContent.m_strFHR_XML = dtrSelected["FHR_XML"].ToString();
                    objRecordContent.m_strISCHIALSPINE_XML = dtrSelected["ISCHIALSPINE_XML"].ToString();
                    objRecordContent.m_strSACRALBONE_XML = dtrSelected["SACRALBONE_XML"].ToString();
                    objRecordContent.m_strPUBICARCH_XML = dtrSelected["PUBICARCH_XML"].ToString();
                    objRecordContent.m_strDC_XML = dtrSelected["DC_XML"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_XML = dtrSelected["ISCHIUMNOTCH_XML"].ToString();
                    objRecordContent.m_strPISS_XML = dtrSelected["PISS_XML"].ToString();
                    objRecordContent.m_strUCCHARACTER_XML = dtrSelected["UCCHARACTER_XML"].ToString();
                    objRecordContent.m_strPROJECT_XML = dtrSelected["PROJECT_XML"].ToString();

                    objRecordContent.m_strEXAMINATIONREASON_RIGHT = dtrSelected["EXAMINATIONREASON_RIGHT"].ToString();
                    objRecordContent.m_strMETREURYSIS_RIGHT = dtrSelected["METREURYSIS_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION_RIGHT = dtrSelected["PRESENTATION_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_RIGHT = dtrSelected["PRESENTATIONHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_RIGHT = dtrSelected["PRESENTATIONORIENTATION_RIGHT"].ToString();
                    objRecordContent.m_strSKULL_RIGHT = dtrSelected["SKULL_RIGHT"].ToString();
                    objRecordContent.m_strOVERLAPPING_RIGHT = dtrSelected["OVERLAPPING_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_RIGHT = dtrSelected["CAPUTSUCCEDANEUM_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOTICFLUID_RIGHT = dtrSelected["AMNIOTICFLUID_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_RIGHT = dtrSelected["AMNIOTICFLUIDCHARACTER_RIGHT"].ToString();
                    objRecordContent.m_strFHR_RIGHT = dtrSelected["FHR_RIGHT"].ToString();
                    objRecordContent.m_strISCHIALSPINE_RIGHT = dtrSelected["ISCHIALSPINE_RIGHT"].ToString();
                    objRecordContent.m_strSACRALBONE_RIGHT = dtrSelected["SACRALBONE_RIGHT"].ToString();
                    objRecordContent.m_strPUBICARCH_RIGHT = dtrSelected["PUBICARCH_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtrSelected["DC_RIGHT"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_RIGHT = dtrSelected["ISCHIUMNOTCH_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtrSelected["PISS_RIGHT"].ToString();
                    objRecordContent.m_strUCCHARACTER_RIGHT = dtrSelected["UCCHARACTER_RIGHT"].ToString();
                    objRecordContent.m_strPROJECT_RIGHT = dtrSelected["PROJECT_RIGHT"].ToString();

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
            clsEMR_VaginalExaminationValue objRecordContent = (clsEMR_VaginalExaminationValue)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region 赋值

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(52, out objDPArr);
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
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = objRecordContent.m_dtmEXAMINATIONTIME_DAT;
                objDPArr[9].Value = objRecordContent.m_strEXAMINATIONREASON_VCHR;
                objDPArr[10].Value = objRecordContent.m_strMETREURYSIS_VCHR;
                objDPArr[11].Value = objRecordContent.m_strPRESENTATION_VCHR;
                objDPArr[12].Value = objRecordContent.m_strPRESENTATIONHEIGHT_VCHR;
                objDPArr[13].Value = objRecordContent.m_strPRESENTATIONORIENTATION_VCHR;
                objDPArr[14].Value = objRecordContent.m_strSKULL_VCHR;
                objDPArr[15].Value = objRecordContent.m_strOVERLAPPING_VCHR;
                objDPArr[16].Value = objRecordContent.m_strCAPUTSUCCEDANEUM_VCHR;
                objDPArr[17].Value = objRecordContent.m_strRUPTUREDFETALMEMBRANES_CHR;
                objDPArr[18].Value = objRecordContent.m_strRUPTUREDMODE_CHR;
                objDPArr[19].DbType = DbType.DateTime;
                objDPArr[19].Value = objRecordContent.m_dtmRUPTURETIME_DAT;
                objDPArr[20].Value = objRecordContent.m_strAMNIOTICFLUID_VCHR;
                objDPArr[21].Value = objRecordContent.m_strAMNIOTICFLUIDCHARACTER_VCHR;
                objDPArr[22].Value = objRecordContent.m_strFHR_VCHR;
                objDPArr[23].Value = objRecordContent.m_strISCHIALSPINE_VCHR;
                objDPArr[24].Value = objRecordContent.m_strCOCCYX_CHR;
                objDPArr[25].Value = objRecordContent.m_strSACRALBONE_VCHR;
                objDPArr[26].Value = objRecordContent.m_strPUBICARCH_VCHR;
                objDPArr[27].Value = objRecordContent.m_strDC_VCHR;
                objDPArr[28].Value = objRecordContent.m_strISCHIUMNOTCH_VCHR;
                objDPArr[29].Value = objRecordContent.m_strURETHRALCATHETERIZATION_CHR;
                objDPArr[30].Value = objRecordContent.m_strPISS_VCHR;
                objDPArr[31].Value = objRecordContent.m_strUCCHARACTER_VCHR;
                objDPArr[32].Value = objRecordContent.m_strPROJECT_VCHR;
                objDPArr[33].Value = objRecordContent.m_strEXAMINATIONREASON_XML;
                objDPArr[34].Value = objRecordContent.m_strMETREURYSIS_XML;
                objDPArr[35].Value = objRecordContent.m_strPRESENTATION_XML;
                objDPArr[36].Value = objRecordContent.m_strPRESENTATIONHEIGHT_XML;
                objDPArr[37].Value = objRecordContent.m_strPRESENTATIONORIENTATION_XML;
                objDPArr[38].Value = objRecordContent.m_strSKULL_XML;
                objDPArr[39].Value = objRecordContent.m_strOVERLAPPING_XML;
                objDPArr[40].Value = objRecordContent.m_strCAPUTSUCCEDANEUM_XML;
                objDPArr[41].Value = objRecordContent.m_strAMNIOTICFLUID_XML;
                objDPArr[42].Value = objRecordContent.m_strAMNIOTICFLUIDCHARACTER_XML;
                objDPArr[43].Value = objRecordContent.m_strFHR_XML;
                objDPArr[44].Value = objRecordContent.m_strISCHIALSPINE_XML;
                objDPArr[45].Value = objRecordContent.m_strSACRALBONE_XML;
                objDPArr[46].Value = objRecordContent.m_strPUBICARCH_XML;
                objDPArr[47].Value = objRecordContent.m_strDC_XML;
                objDPArr[48].Value = objRecordContent.m_strISCHIUMNOTCH_XML;
                objDPArr[49].Value = objRecordContent.m_strPISS_XML;
                objDPArr[50].Value = objRecordContent.m_strUCCHARACTER_XML;
                objDPArr[51].Value = objRecordContent.m_strPROJECT_XML;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(24, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strEXAMINATIONREASON_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strMETREURYSIS_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strPRESENTATION_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strPRESENTATIONHEIGHT_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strPRESENTATIONORIENTATION_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strSKULL_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strOVERLAPPING_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strCAPUTSUCCEDANEUM_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strAMNIOTICFLUID_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strAMNIOTICFLUIDCHARACTER_RIGHT;
                objDPArr2[15].Value = objRecordContent.m_strFHR_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strISCHIALSPINE_RIGHT;
                objDPArr2[17].Value = objRecordContent.m_strSACRALBONE_RIGHT;
                objDPArr2[18].Value = objRecordContent.m_strPUBICARCH_RIGHT;
                objDPArr2[19].Value = objRecordContent.m_strDC_RIGHT;
                objDPArr2[20].Value = objRecordContent.m_strISCHIUMNOTCH_RIGHT;
                objDPArr2[21].Value = objRecordContent.m_strPISS_RIGHT;
                objDPArr2[22].Value = objRecordContent.m_strUCCHARACTER_RIGHT;
                objDPArr2[23].Value = objRecordContent.m_strPROJECT_RIGHT;
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

            clsEMR_VaginalExaminationValue objRecordContent = (clsEMR_VaginalExaminationValue)p_objRecordContent;
            /// <summary>
            /// 从IntensiveTendRecordContent1获取指定表单的最后修改时间。

            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_vaginalexaminationcon t2
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

            clsEMR_VaginalExaminationValue objRecordContent = (clsEMR_VaginalExaminationValue)p_objRecordContent;
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
                objHRPServ.CreateDatabaseParameter(49, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRecordUserID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmEXAMINATIONTIME_DAT;
                objDPArr[4].Value = objRecordContent.m_strEXAMINATIONREASON_VCHR;
                objDPArr[5].Value = objRecordContent.m_strMETREURYSIS_VCHR;
                objDPArr[6].Value = objRecordContent.m_strPRESENTATION_VCHR;
                objDPArr[7].Value = objRecordContent.m_strPRESENTATIONHEIGHT_VCHR;
                objDPArr[8].Value = objRecordContent.m_strPRESENTATIONORIENTATION_VCHR;
                objDPArr[9].Value = objRecordContent.m_strSKULL_VCHR;
                objDPArr[10].Value = objRecordContent.m_strOVERLAPPING_VCHR;
                objDPArr[11].Value = objRecordContent.m_strCAPUTSUCCEDANEUM_VCHR;
                objDPArr[12].Value = objRecordContent.m_strRUPTUREDFETALMEMBRANES_CHR;
                objDPArr[13].Value = objRecordContent.m_strRUPTUREDMODE_CHR;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = objRecordContent.m_dtmRUPTURETIME_DAT;
                objDPArr[15].Value = objRecordContent.m_strAMNIOTICFLUID_VCHR;
                objDPArr[16].Value = objRecordContent.m_strAMNIOTICFLUIDCHARACTER_VCHR;
                objDPArr[17].Value = objRecordContent.m_strFHR_VCHR;
                objDPArr[18].Value = objRecordContent.m_strISCHIALSPINE_VCHR;
                objDPArr[19].Value = objRecordContent.m_strCOCCYX_CHR;
                objDPArr[20].Value = objRecordContent.m_strSACRALBONE_VCHR;
                objDPArr[21].Value = objRecordContent.m_strPUBICARCH_VCHR;
                objDPArr[22].Value = objRecordContent.m_strDC_VCHR;
                objDPArr[23].Value = objRecordContent.m_strISCHIUMNOTCH_VCHR;
                objDPArr[24].Value = objRecordContent.m_strURETHRALCATHETERIZATION_CHR;
                objDPArr[25].Value = objRecordContent.m_strPISS_VCHR;
                objDPArr[26].Value = objRecordContent.m_strUCCHARACTER_VCHR;
                objDPArr[27].Value = objRecordContent.m_strPROJECT_VCHR;
                objDPArr[28].Value = objRecordContent.m_strEXAMINATIONREASON_XML;
                objDPArr[29].Value = objRecordContent.m_strMETREURYSIS_XML;
                objDPArr[30].Value = objRecordContent.m_strPRESENTATION_XML;
                objDPArr[31].Value = objRecordContent.m_strPRESENTATIONHEIGHT_XML;
                objDPArr[32].Value = objRecordContent.m_strPRESENTATIONORIENTATION_XML;
                objDPArr[33].Value = objRecordContent.m_strSKULL_XML;
                objDPArr[34].Value = objRecordContent.m_strOVERLAPPING_XML;
                objDPArr[35].Value = objRecordContent.m_strCAPUTSUCCEDANEUM_XML;
                objDPArr[36].Value = objRecordContent.m_strAMNIOTICFLUID_XML;
                objDPArr[37].Value = objRecordContent.m_strAMNIOTICFLUIDCHARACTER_XML;
                objDPArr[38].Value = objRecordContent.m_strFHR_XML;
                objDPArr[39].Value = objRecordContent.m_strISCHIALSPINE_XML;
                objDPArr[40].Value = objRecordContent.m_strSACRALBONE_XML;
                objDPArr[41].Value = objRecordContent.m_strPUBICARCH_XML;
                objDPArr[42].Value = objRecordContent.m_strDC_XML;
                objDPArr[43].Value = objRecordContent.m_strISCHIUMNOTCH_XML;
                objDPArr[44].Value = objRecordContent.m_strPISS_XML;
                objDPArr[45].Value = objRecordContent.m_strUCCHARACTER_XML;
                objDPArr[46].Value = objRecordContent.m_strPROJECT_XML; ;

                objDPArr[47].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[48].DbType = DbType.Date;
                objDPArr[48].Value = p_objRecordContent.m_dtmCreateDate;

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
                objHRPServ.CreateDatabaseParameter(24, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strEXAMINATIONREASON_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strMETREURYSIS_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strPRESENTATION_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strPRESENTATIONHEIGHT_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strPRESENTATIONORIENTATION_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strSKULL_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strOVERLAPPING_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strCAPUTSUCCEDANEUM_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strAMNIOTICFLUID_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strAMNIOTICFLUIDCHARACTER_RIGHT;
                objDPArr2[15].Value = objRecordContent.m_strFHR_RIGHT;
                objDPArr2[16].Value = objRecordContent.m_strISCHIALSPINE_RIGHT;
                objDPArr2[17].Value = objRecordContent.m_strSACRALBONE_RIGHT;
                objDPArr2[18].Value = objRecordContent.m_strPUBICARCH_RIGHT;
                objDPArr2[19].Value = objRecordContent.m_strDC_RIGHT;
                objDPArr2[20].Value = objRecordContent.m_strISCHIUMNOTCH_RIGHT;
                objDPArr2[21].Value = objRecordContent.m_strPISS_RIGHT;
                objDPArr2[22].Value = objRecordContent.m_strUCCHARACTER_RIGHT;
                objDPArr2[23].Value = objRecordContent.m_strPROJECT_RIGHT;
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
            string strSql = @" update t_emr_vaginalexaminationcon t set t.status_int = 0
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

            clsEMR_VaginalExaminationValue objRecordContent = (clsEMR_VaginalExaminationValue)p_objRecordContent;
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
            /// 从T_EMR_VAGINALEXAMINATION和T_EMR_VAGINALEXAMINATIONCON获取LastModifyDate和FirstPrintDate
            /// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from t_emr_vaginalexamination a
 inner join t_emr_vaginalexaminationcon b on a.registerid_chr =
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

        #region 获取指定已经被删除记录的内容
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
       t1.examinationtime_dat,
       t1.examinationreason_vchr,
       t1.metreurysis_vchr,
       t1.presentation_vchr,
       t1.presentationheight_vchr,
       t1.presentationorientation_vchr,
       t1.skull_vchr,
       t1.overlapping_vchr,
       t1.caputsuccedaneum_vchr,
       t1.rupturedfetalmembranes_chr,
       t1.rupturedmode_chr,
       t1.rupturetime_dat,
       t1.amnioticfluid_vchr,
       t1.amnioticfluidcharacter_vchr,
       t1.fhr_vchr,
       t1.ischialspine_vchr,
       t1.coccyx_chr,
       t1.sacralbone_vchr,
       t1.pubicarch_vchr,
       t1.dc_vchr,
       t1.ischiumnotch_vchr,
       t1.urethralcatheterization_chr,
       t1.piss_vchr,
       t1.uccharacter_vchr,
       t1.project_vchr,
       t1.examinationreason_xml,
       t1.metreurysis_xml,
       t1.presentation_xml,
       t1.presentationheight_xml,
       t1.presentationorientation_xml,
       t1.skull_xml,
       t1.overlapping_xml,
       t1.caputsuccedaneum_xml,
       t1.amnioticfluid_xml,
       t1.amnioticfluidcharacter_xml,
       t1.fhr_xml,
       t1.ischialspine_xml,
       t1.sacralbone_xml,
       t1.pubicarch_xml,
       t1.dc_xml,
       t1.ischiumnotch_xml,
       t1.piss_xml,
       t1.uccharacter_xml,
       t1.project_xml,
       t2.status_int,
       t2.modifydate,
       t2.modifyuserid,
       t2.examinationreason_right,
       t2.metreurysis_right,
       t2.presentation_right,
       t2.presentationheight_right,
       t2.presentationorientation_right,
       t2.skull_right,
       t2.overlapping_right,
       t2.caputsuccedaneum_right,
       t2.amnioticfluid_right,
       t2.amnioticfluidcharacter_right,
       t2.fhr_right,
       t2.ischialspine_right,
       t2.sacralbone_right,
       t2.pubicarch_right,
       t2.dc_right,
       t2.ischiumnotch_right,
       t2.piss_right,
       t2.uccharacter_right,
       t2.project_right
  from t_emr_vaginalexamination t1
 inner join t_emr_vaginalexaminationcon t2 on t1.registerid_chr =
                                              t2.registerid_chr
                                          and t1.createdate_dat =
                                              t2.createdate_dat
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t2.modifydate = (select max(modifydate)
                          from t_emr_vaginalexaminationcon
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
                    clsEMR_VaginalExaminationValue objRecordContent = new clsEMR_VaginalExaminationValue();
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

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["EXAMINATIONTIME_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmEXAMINATIONTIME_DAT = dtmTemp;

                    objRecordContent.m_strEXAMINATIONREASON_VCHR = dtrSelected["EXAMINATIONREASON_VCHR"].ToString();
                    objRecordContent.m_strMETREURYSIS_VCHR = dtrSelected["METREURYSIS_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATION_VCHR = dtrSelected["PRESENTATION_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_VCHR = dtrSelected["PRESENTATIONHEIGHT_VCHR"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_VCHR = dtrSelected["PRESENTATIONORIENTATION_VCHR"].ToString();
                    objRecordContent.m_strSKULL_VCHR = dtrSelected["SKULL_VCHR"].ToString();
                    objRecordContent.m_strOVERLAPPING_VCHR = dtrSelected["OVERLAPPING_VCHR"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_VCHR = dtrSelected["CAPUTSUCCEDANEUM_VCHR"].ToString();
                    objRecordContent.m_strRUPTUREDFETALMEMBRANES_CHR = dtrSelected["RUPTUREDFETALMEMBRANES_CHR"].ToString();
                    objRecordContent.m_strRUPTUREDMODE_CHR = dtrSelected["RUPTUREDMODE_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RUPTURETIME_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRUPTURETIME_DAT = dtmTemp;

                    objRecordContent.m_strAMNIOTICFLUID_VCHR = dtrSelected["AMNIOTICFLUID_VCHR"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_VCHR = dtrSelected["AMNIOTICFLUIDCHARACTER_VCHR"].ToString();
                    objRecordContent.m_strFHR_VCHR = dtrSelected["FHR_VCHR"].ToString();
                    objRecordContent.m_strISCHIALSPINE_VCHR = dtrSelected["ISCHIALSPINE_VCHR"].ToString();
                    objRecordContent.m_strCOCCYX_CHR = dtrSelected["COCCYX_CHR"].ToString();
                    objRecordContent.m_strSACRALBONE_VCHR = dtrSelected["SACRALBONE_VCHR"].ToString();
                    objRecordContent.m_strPUBICARCH_VCHR = dtrSelected["PUBICARCH_VCHR"].ToString();
                    objRecordContent.m_strDC_VCHR = dtrSelected["DC_VCHR"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_VCHR = dtrSelected["ISCHIUMNOTCH_VCHR"].ToString();
                    objRecordContent.m_strURETHRALCATHETERIZATION_CHR = dtrSelected["URETHRALCATHETERIZATION_CHR"].ToString();
                    objRecordContent.m_strPISS_VCHR = dtrSelected["PISS_VCHR"].ToString();
                    objRecordContent.m_strUCCHARACTER_VCHR = dtrSelected["UCCHARACTER_VCHR"].ToString();
                    objRecordContent.m_strPROJECT_VCHR = dtrSelected["PROJECT_VCHR"].ToString();
                    objRecordContent.m_strEXAMINATIONREASON_XML = dtrSelected["EXAMINATIONREASON_XML"].ToString();
                    objRecordContent.m_strMETREURYSIS_XML = dtrSelected["METREURYSIS_XML"].ToString();
                    objRecordContent.m_strPRESENTATION_XML = dtrSelected["PRESENTATION_XML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_XML = dtrSelected["PRESENTATIONHEIGHT_XML"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_XML = dtrSelected["PRESENTATIONORIENTATION_XML"].ToString();
                    objRecordContent.m_strSKULL_XML = dtrSelected["SKULL_XML"].ToString();
                    objRecordContent.m_strOVERLAPPING_XML = dtrSelected["OVERLAPPING_XML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_XML = dtrSelected["CAPUTSUCCEDANEUM_XML"].ToString();
                    objRecordContent.m_strAMNIOTICFLUID_XML = dtrSelected["AMNIOTICFLUID_XML"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_XML = dtrSelected["AMNIOTICFLUIDCHARACTER_XML"].ToString();
                    objRecordContent.m_strFHR_XML = dtrSelected["FHR_XML"].ToString();
                    objRecordContent.m_strISCHIALSPINE_XML = dtrSelected["ISCHIALSPINE_XML"].ToString();
                    objRecordContent.m_strSACRALBONE_XML = dtrSelected["SACRALBONE_XML"].ToString();
                    objRecordContent.m_strPUBICARCH_XML = dtrSelected["PUBICARCH_XML"].ToString();
                    objRecordContent.m_strDC_XML = dtrSelected["DC_XML"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_XML = dtrSelected["ISCHIUMNOTCH_XML"].ToString();
                    objRecordContent.m_strPISS_XML = dtrSelected["PISS_XML"].ToString();
                    objRecordContent.m_strUCCHARACTER_XML = dtrSelected["UCCHARACTER_XML"].ToString();
                    objRecordContent.m_strPROJECT_XML = dtrSelected["PROJECT_XML"].ToString();

                    objRecordContent.m_strEXAMINATIONREASON_RIGHT = dtrSelected["EXAMINATIONREASON_RIGHT"].ToString();
                    objRecordContent.m_strMETREURYSIS_RIGHT = dtrSelected["METREURYSIS_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION_RIGHT = dtrSelected["PRESENTATION_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEIGHT_RIGHT = dtrSelected["PRESENTATIONHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONORIENTATION_RIGHT = dtrSelected["PRESENTATIONORIENTATION_RIGHT"].ToString();
                    objRecordContent.m_strSKULL_RIGHT = dtrSelected["SKULL_RIGHT"].ToString();
                    objRecordContent.m_strOVERLAPPING_RIGHT = dtrSelected["OVERLAPPING_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUM_RIGHT = dtrSelected["CAPUTSUCCEDANEUM_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOTICFLUID_RIGHT = dtrSelected["AMNIOTICFLUID_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOTICFLUIDCHARACTER_RIGHT = dtrSelected["AMNIOTICFLUIDCHARACTER_RIGHT"].ToString();
                    objRecordContent.m_strFHR_RIGHT = dtrSelected["FHR_RIGHT"].ToString();
                    objRecordContent.m_strISCHIALSPINE_RIGHT = dtrSelected["ISCHIALSPINE_RIGHT"].ToString();
                    objRecordContent.m_strSACRALBONE_RIGHT = dtrSelected["SACRALBONE_RIGHT"].ToString();
                    objRecordContent.m_strPUBICARCH_RIGHT = dtrSelected["PUBICARCH_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtrSelected["DC_RIGHT"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH_RIGHT = dtrSelected["ISCHIUMNOTCH_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtrSelected["PISS_RIGHT"].ToString();
                    objRecordContent.m_strUCCHARACTER_RIGHT = dtrSelected["UCCHARACTER_RIGHT"].ToString();
                    objRecordContent.m_strPROJECT_RIGHT = dtrSelected["PROJECT_RIGHT"].ToString();

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
