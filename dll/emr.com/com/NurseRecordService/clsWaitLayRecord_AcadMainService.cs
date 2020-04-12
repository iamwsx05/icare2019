
using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.Utility;

namespace com.digitalwave.clsRecordsService
{
    /// <summary>
    /// 候产记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsWaitLayRecord_AcadMainService : clsRecordsService
    {
        #region constructor
        public clsWaitLayRecord_AcadMainService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region SQL语句
        private const string c_strUpdateFirstPrintDateSQL = @"update icuacad_waitlayrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        private const string c_strGetRecordContentSQL = @"select distinct f_getempnamebyno(t1.createuserid) as createusername,
                t1.inpatientid,
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
                t3.modifydate,
                t3.modifyuserid,
                t3.laycount_chr_right,
                t3.beforehand_chr_right,
                t3.recorddate_chr_right,
                t3.time_chr_right,
                t3.bloodpressure_chr_right,
                t3.embryolocation_chr_right,
                t3.embryoheart_chr_right,
                t3.intermission_chr_right,
                t3.persist_chr_right,
                t3.intensity_chr_right,
                t3.palacemouth_chr_right,
                t3.show_chr_right,
                t3.caul_chr_right,
                t3.anuscheck_chr_right,
                t3.other_chr_right,
                t3.scrutator_chr_right,
                t3.bloodpressure2_chr_right
  from icuacad_waitlayrecord t1, icuacad_waitlaycontent t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = '0'
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate,t3.modifydate asc";

        private const string c_strGetRecordContentSQL_Single = @"select distinct f_getempnamebyno(t1.createuserid) as createusername,
                t1.inpatientid,
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
                t3.modifydate,
                t3.modifyuserid,
                t3.laycount_chr_right,
                t3.beforehand_chr_right,
                t3.recorddate_chr_right,
                t3.time_chr_right,
                t3.bloodpressure_chr_right,
                t3.embryolocation_chr_right,
                t3.embryoheart_chr_right,
                t3.intermission_chr_right,
                t3.persist_chr_right,
                t3.intensity_chr_right,
                t3.palacemouth_chr_right,
                t3.show_chr_right,
                t3.caul_chr_right,
                t3.anuscheck_chr_right,
                t3.other_chr_right,
                t3.scrutator_chr_right,
                t3.bloodpressure2_chr_right
  from icuacad_waitlayrecord t1, icuacad_waitlaycontent t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = '0'
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.createdate";

        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from icuacad_waitlayrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        private const string c_strDeleteRecordSQL = @"update icuacad_waitlayrecord
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";
        #endregion

        #region 更新数据库中的首次打印时间
        /// <summary>
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_intRecordTypeArr">记录类型</param>
        /// <param name="p_dtmOpenDateArr">记录时间(与记录类型及其位置一一对应)</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmOpenDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsWaitLayRecord_AcadMainService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	


            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" ||
                p_dtmOpenDateArr == null || p_dtmFirstPrintDate == DateTime.MinValue)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                for (int i = 0 ; i < p_dtmOpenDateArr.Length ; i++)
                {
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_dtmFirstPrintDate;
                    objDPArr[1].Value = p_strInPatientID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_dtmOpenDateArr[i];
                    //执行SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
                    if (lngRes <= 0) return lngRes;
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region 修改或添加一条记录时读数据库
        /// <summary>
        /// 修改或添加一条记录时读数据库
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strRecordOpenDate"></param>
        /// <param name="p_objTansDataInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strRecordOpenDate,
            out clsIcuAcad_WaitLayRecord[] p_objTansDataInfo)
        {
            p_objTansDataInfo = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                ArrayList arlTransData = new ArrayList();
                ArrayList arlModifyData = new ArrayList();
                DateTime dtmOpenDate;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strRecordOpenDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single, ref dtbValue, objDPArr);
                //循环DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objTansDataInfo = new clsIcuAcad_WaitLayRecord[dtbValue.Rows.Count];
                    clsIcuAcad_WaitLayRecord objRecordContent = null;

                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date == dtmOpenDate)
                        {
                            #region 从DataTable.Rows中获取结果

                            objRecordContent = new clsIcuAcad_WaitLayRecord();
                            objRecordContent.m_strInPatientID = p_strInPatientID;
                            objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString());
                            objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
                            objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

                            if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
                                objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
                            objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
                            //							objRecordContent.m_strContentCreateUserName = dtbValue.Rows[j]["CreateUserName"].ToString();
                            objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
                            objRecordContent.m_strModifyUserName = dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();
                            if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
                                objRecordContent.m_bytIfConfirm = 0;
                            else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
                            if (dtbValue.Rows[j]["STATUS"].ToString() == "")
                                objRecordContent.m_bytStatus = 0;
                            else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());

                            objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
                            objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

                            objRecordContent.m_strLayCount_chr = dtbValue.Rows[j]["LAYCOUNT_CHR"].ToString();
                            objRecordContent.m_strLayCount_chr_RIGHT = dtbValue.Rows[j]["LAYCOUNT_CHR_RIGHT"].ToString();
                            objRecordContent.m_strLayCount_chrXML = dtbValue.Rows[j]["LAYCOUNT_CHRXML"].ToString();

                            objRecordContent.m_strBeforehand_chr = dtbValue.Rows[j]["BEFOREHAND_CHR"].ToString();
                            objRecordContent.m_strBeforehand_chr_RIGHT = dtbValue.Rows[j]["BEFOREHAND_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBeforehand_chrXML = dtbValue.Rows[j]["BEFOREHAND_CHRXML"].ToString();

                            objRecordContent.m_strRecordDate_chr = dtbValue.Rows[j]["RECORDDATE_CHR"].ToString();
                            objRecordContent.m_strRecordDate_chr_RIGHT = dtbValue.Rows[j]["RECORDDATE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strRecordDate_chrXML = dtbValue.Rows[j]["RECORDDATE_CHRXML"].ToString();

                            objRecordContent.m_strTime_chr = dtbValue.Rows[j]["TIME_CHR"].ToString();
                            objRecordContent.m_strTime_chr_RIGHT = dtbValue.Rows[j]["TIME_CHR_RIGHT"].ToString();
                            objRecordContent.m_strTime_chrXML = dtbValue.Rows[j]["TIME_CHRXML"].ToString();

                            objRecordContent.m_strBloodPressure_chr = dtbValue.Rows[j]["BLOODPRESSURE_CHR"].ToString();
                            objRecordContent.m_strBloodPressure_chr_RIGHT = dtbValue.Rows[j]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBloodPressure_chrXML = dtbValue.Rows[j]["BLOODPRESSURE_CHRXML"].ToString();

                            objRecordContent.m_strBloodPressure2_chr_RIGHT = dtbValue.Rows[j]["BLOODPRESSURE2_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBloodPressure2_chr = dtbValue.Rows[j]["BLOODPRESSURE2_CHR"].ToString();
                            objRecordContent.m_strBloodPressure2_chrXML = dtbValue.Rows[j]["BLOODPRESSURE2_CHRXML"].ToString();

                            objRecordContent.m_strEmbryoLocation_chr = dtbValue.Rows[j]["EMBRYOLOCATION_CHR"].ToString();
                            objRecordContent.m_strEmbryoLocation_chr_RIGHT = dtbValue.Rows[j]["EMBRYOLOCATION_CHR_RIGHT"].ToString();
                            objRecordContent.m_strEmbryoLocation_chrXML = dtbValue.Rows[j]["EMBRYOLOCATION_CHRXML"].ToString();


                            objRecordContent.m_strEmbryoHeart_chr = dtbValue.Rows[j]["EMBRYOHEART_CHR"].ToString();
                            objRecordContent.m_strEmbryoHeart_chr_RIGHT = dtbValue.Rows[j]["EMBRYOHEART_CHR_RIGHT"].ToString();
                            objRecordContent.m_strEmbryoHeart_chrXML = dtbValue.Rows[j]["EMBRYOHEART_CHRXML"].ToString();

                            objRecordContent.m_strIntermission_chr = dtbValue.Rows[j]["INTERMISSION_CHR"].ToString();
                            objRecordContent.m_strIntermission_chr_RIGHT = dtbValue.Rows[j]["INTERMISSION_CHR_RIGHT"].ToString();
                            objRecordContent.m_strIntermission_chrXML = dtbValue.Rows[j]["INTERMISSION_CHRXML"].ToString();

                            objRecordContent.m_strPersist_chr = dtbValue.Rows[j]["PERSIST_CHR"].ToString();
                            objRecordContent.m_strPersist_chr_RIGHT = dtbValue.Rows[j]["PERSIST_CHR_RIGHT"].ToString();
                            objRecordContent.m_strPersist_chrXML = dtbValue.Rows[j]["PERSIST_CHRXML"].ToString();

                            objRecordContent.m_strIntensity_chr = dtbValue.Rows[j]["INTENSITY_CHR"].ToString();
                            objRecordContent.m_strIntensity_RIGHT = dtbValue.Rows[j]["INTENSITY_CHR_RIGHT"].ToString();
                            objRecordContent.m_strIntensity_chrXML = dtbValue.Rows[j]["INTENSITY_CHRXML"].ToString();

                            objRecordContent.m_strPalaceMouth_chr = dtbValue.Rows[j]["PALACEMOUTH_CHR"].ToString();
                            objRecordContent.m_strPalaceMouth_chr_RIGHT = dtbValue.Rows[j]["PALACEMOUTH_CHR_RIGHT"].ToString();
                            objRecordContent.m_strPalaceMouth_chrXML = dtbValue.Rows[j]["PALACEMOUTH_CHRXML"].ToString();

                            objRecordContent.m_strShow_chr = dtbValue.Rows[j]["SHOW_CHR"].ToString();
                            objRecordContent.m_strShow_chr_RIGHT = dtbValue.Rows[j]["SHOW_CHR_RIGHT"].ToString();
                            objRecordContent.m_strShow_chrXML = dtbValue.Rows[j]["SHOW_CHRXML"].ToString();

                            objRecordContent.m_strCaul_chr = dtbValue.Rows[j]["CAUL_CHR"].ToString();
                            objRecordContent.m_strCaul_chr_RIGHT = dtbValue.Rows[j]["CAUL_CHR_RIGHT"].ToString();
                            objRecordContent.m_strCaul_chrXML = dtbValue.Rows[j]["CAUL_CHRXML"].ToString();

                            objRecordContent.m_strAnusCheck_chr = dtbValue.Rows[j]["ANUSCHECK_CHR"].ToString();
                            objRecordContent.m_strAnusCheck_chr_RIGHT = dtbValue.Rows[j]["ANUSCHECK_CHR_RIGHT"].ToString();
                            objRecordContent.m_strAnusCheck_chrXML = dtbValue.Rows[j]["ANUSCHECK_CHRXML"].ToString();

                            objRecordContent.m_strOther_chr = dtbValue.Rows[j]["OTHER_CHR"].ToString();
                            objRecordContent.m_strOther_chr_RIGHT = dtbValue.Rows[j]["OTHER_CHR_RIGHT"].ToString();
                            objRecordContent.m_strOther_chrXML = dtbValue.Rows[j]["OTHER_CHRXML"].ToString();

                            objRecordContent.m_strScrutatorID= dtbValue.Rows[j]["SCRUTATOR_CHR"].ToString();
                            objRecordContent.m_strScrutator_chr = dtbValue.Rows[j]["SCRUTATOR_CHR"].ToString();
                            objRecordContent.m_strScrutator_chr_RIGHT = dtbValue.Rows[j]["SCRUTATOR_CHR_RIGHT"].ToString();
                            objRecordContent.m_strScrutator_chrXML = dtbValue.Rows[j]["SCRUTATOR_CHRXML"].ToString();
                            objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[j]["recorddate"]);
                            #endregion
                        }

                        p_objTansDataInfo[j] = objRecordContent;
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
        #endregion

        #region 获取指定记录的内容
        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objGeneralNurseRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsTransDataInfo[] p_objIntensiveTendInfoArr)
        {
            clsIcuAcad_WaitLayRecord[] p_objGeneralNurseRecordArr = null;
            //			clsGeneralNurseRecordContent_GXDetail[] p_objGeneralNurseDetailArr = null;
            p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
            long lngRes = -1;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsIcuAcad_WaitLayContentDataInfo objDataInfo = new clsIcuAcad_WaitLayContentDataInfo();

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //				DataTable dtbDetail = new DataTable();//病情记录内容
                DataTable dtbContent = new DataTable();//护理记录内容  

                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    clsIcuAcad_WaitLayRecord objRecordContent = null;
                    p_objGeneralNurseRecordArr = new clsIcuAcad_WaitLayRecord[dtbContent.Rows.Count];
                    for (int i = 0 ; i < dtbContent.Rows.Count ; i++)
                    {
                        #region set values
                        objRecordContent = new clsIcuAcad_WaitLayRecord();
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbContent.Rows[i]["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbContent.Rows[i]["MODIFYDATE"].ToString());
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

                        if (dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = dtbContent.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = dtbContent.Rows[i]["MODIFYUSERID"].ToString();
                        //						objRecordContent.m_strContentCreateUserName = dtbContent.Rows[i]["CreateUserName"].ToString();
                        if (dtbContent.Rows[i]["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbContent.Rows[i]["IFCONFIRM"].ToString());
                        if (dtbContent.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbContent.Rows[i]["STATUS"].ToString());

                        objRecordContent.m_strLayCount_chr = dtbContent.Rows[i]["LAYCOUNT_CHR"].ToString();
                        objRecordContent.m_strLayCount_chr_RIGHT = dtbContent.Rows[i]["LAYCOUNT_CHR_RIGHT"].ToString();
                        objRecordContent.m_strLayCount_chrXML = dtbContent.Rows[i]["LAYCOUNT_CHRXML"].ToString();

                        objRecordContent.m_strBeforehand_chr = dtbContent.Rows[i]["BEFOREHAND_CHR"].ToString();
                        objRecordContent.m_strBeforehand_chr_RIGHT = dtbContent.Rows[i]["BEFOREHAND_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBeforehand_chrXML = dtbContent.Rows[i]["BEFOREHAND_CHRXML"].ToString();

                        objRecordContent.m_strRecordDate_chr = dtbContent.Rows[i]["RECORDDATE_CHR"].ToString();
                        objRecordContent.m_strRecordDate_chr_RIGHT = dtbContent.Rows[i]["RECORDDATE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strRecordDate_chrXML = dtbContent.Rows[i]["RECORDDATE_CHRXML"].ToString();

                        objRecordContent.m_strTime_chr = dtbContent.Rows[i]["TIME_CHR"].ToString();
                        objRecordContent.m_strTime_chr_RIGHT = dtbContent.Rows[i]["TIME_CHR_RIGHT"].ToString();
                        objRecordContent.m_strTime_chrXML = dtbContent.Rows[i]["TIME_CHRXML"].ToString();

                        objRecordContent.m_strBloodPressure_chr = dtbContent.Rows[i]["BLOODPRESSURE_CHR"].ToString();
                        objRecordContent.m_strBloodPressure_chr_RIGHT = dtbContent.Rows[i]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBloodPressure_chrXML = dtbContent.Rows[i]["BLOODPRESSURE_CHRXML"].ToString();

                        objRecordContent.m_strBloodPressure2_chr = dtbContent.Rows[i]["BLOODPRESSURE2_CHR"].ToString();
                        objRecordContent.m_strBloodPressure2_chr_RIGHT = dtbContent.Rows[i]["BLOODPRESSURE2_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBloodPressure2_chrXML = dtbContent.Rows[i]["BLOODPRESSURE2_CHRXML"].ToString();

                        objRecordContent.m_strEmbryoLocation_chr = dtbContent.Rows[i]["EMBRYOLOCATION_CHR"].ToString();
                        objRecordContent.m_strEmbryoLocation_chr_RIGHT = dtbContent.Rows[i]["EMBRYOLOCATION_CHR_RIGHT"].ToString();
                        objRecordContent.m_strEmbryoLocation_chrXML = dtbContent.Rows[i]["EMBRYOLOCATION_CHRXML"].ToString();


                        objRecordContent.m_strEmbryoHeart_chr = dtbContent.Rows[i]["EMBRYOHEART_CHR"].ToString();
                        objRecordContent.m_strEmbryoHeart_chr_RIGHT = dtbContent.Rows[i]["EMBRYOHEART_CHR_RIGHT"].ToString();
                        objRecordContent.m_strEmbryoHeart_chrXML = dtbContent.Rows[i]["EMBRYOHEART_CHRXML"].ToString();

                        objRecordContent.m_strIntermission_chr = dtbContent.Rows[i]["INTERMISSION_CHR"].ToString();
                        objRecordContent.m_strIntermission_chr_RIGHT = dtbContent.Rows[i]["INTERMISSION_CHR_RIGHT"].ToString();
                        objRecordContent.m_strIntermission_chrXML = dtbContent.Rows[i]["INTERMISSION_CHRXML"].ToString();

                        objRecordContent.m_strPersist_chr = dtbContent.Rows[i]["PERSIST_CHR"].ToString();
                        objRecordContent.m_strPersist_chr_RIGHT = dtbContent.Rows[i]["PERSIST_CHR_RIGHT"].ToString();
                        objRecordContent.m_strPersist_chrXML = dtbContent.Rows[i]["PERSIST_CHRXML"].ToString();

                        objRecordContent.m_strIntensity_chr = dtbContent.Rows[i]["INTENSITY_CHR"].ToString();
                        objRecordContent.m_strIntensity_RIGHT = dtbContent.Rows[i]["INTENSITY_CHR_RIGHT"].ToString();
                        objRecordContent.m_strIntensity_chrXML = dtbContent.Rows[i]["INTENSITY_CHRXML"].ToString();

                        objRecordContent.m_strPalaceMouth_chr = dtbContent.Rows[i]["PALACEMOUTH_CHR"].ToString();
                        objRecordContent.m_strPalaceMouth_chr_RIGHT = dtbContent.Rows[i]["PALACEMOUTH_CHR_RIGHT"].ToString();
                        objRecordContent.m_strPalaceMouth_chrXML = dtbContent.Rows[i]["PALACEMOUTH_CHRXML"].ToString();

                        objRecordContent.m_strShow_chr = dtbContent.Rows[i]["SHOW_CHR"].ToString();
                        objRecordContent.m_strShow_chr_RIGHT = dtbContent.Rows[i]["SHOW_CHR_RIGHT"].ToString();
                        objRecordContent.m_strShow_chrXML = dtbContent.Rows[i]["SHOW_CHRXML"].ToString();

                        objRecordContent.m_strCaul_chr = dtbContent.Rows[i]["CAUL_CHR"].ToString();
                        objRecordContent.m_strCaul_chr_RIGHT = dtbContent.Rows[i]["CAUL_CHR_RIGHT"].ToString();
                        objRecordContent.m_strCaul_chrXML = dtbContent.Rows[i]["CAUL_CHRXML"].ToString();

                        objRecordContent.m_strAnusCheck_chr = dtbContent.Rows[i]["ANUSCHECK_CHR"].ToString();
                        objRecordContent.m_strAnusCheck_chr_RIGHT = dtbContent.Rows[i]["ANUSCHECK_CHR_RIGHT"].ToString();
                        objRecordContent.m_strAnusCheck_chrXML = dtbContent.Rows[i]["ANUSCHECK_CHRXML"].ToString();

                        objRecordContent.m_strOther_chr = dtbContent.Rows[i]["OTHER_CHR"].ToString();
                        objRecordContent.m_strOther_chr_RIGHT = dtbContent.Rows[i]["OTHER_CHR_RIGHT"].ToString();
                        objRecordContent.m_strOther_chrXML = dtbContent.Rows[i]["OTHER_CHRXML"].ToString();

                        objRecordContent.m_strScrutator_chr = dtbContent.Rows[i]["SCRUTATOR_CHR"].ToString();
                        objRecordContent.m_strScrutator_chr_RIGHT = dtbContent.Rows[i]["SCRUTATOR_CHR_RIGHT"].ToString();
                        objRecordContent.m_strScrutator_chrXML = dtbContent.Rows[i]["SCRUTATOR_CHRXML"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbContent.Rows[i]["CREATEDATE"].ToString());
                        p_objGeneralNurseRecordArr[i] = objRecordContent;
                        #endregion
                    }
                }
                objDataInfo.m_objRecordArr = p_objGeneralNurseRecordArr;
                p_objIntensiveTendInfoArr[0] = objDataInfo;

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
        #endregion

        #region 查看当前记录是否最新的记录
        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //检查参数          
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" t2.modifydate,t2.modifyuserid from 
											icuacad_waitlayrecord t1,icuacad_waitlaycontent t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status = '0'
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;


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
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    //if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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

        #region 把记录从数据中“删除”。
        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

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
    }
}
