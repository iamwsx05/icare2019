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
    /// 静脉特殊化疗用药观察记录表(广西)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsVeinSpecialUseDrug_MainService : clsRecordsService
    {


        #region SQL语句
        private const string c_strUpdateFirstPrintDateSQL = @"update icuacad_veinspecialusedrug 
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

//        private const string c_strGetRecordContentSQL = @"SELECT distinct f_getempnamebyno(T1.CREATEUSERID) AS CreateUserName,
//															T1.*,
//															t3.*
//														FROM icuacad_veinspecialusedrug  T1,
//															icuacad_veinspecialusedrugcon  T3
//														WHERE T1.InPatientID = T3.InPatientID
//														AND T1.InPatientDate = T3.InPatientDate
//														AND T1.OpenDate = T3.OpenDate
//														AND T1.Status =0
//														AND T1.InPatientID = ?
//														AND T1.InPatientDate = ?
//														ORDER BY T1.BEGINTIME_DATE ,t3.modifydate";
        private const string c_strGetRecordContentSQL = @"select f_getempnamebyno(t1.createuserid) as createusername,
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
       t3.modifydate,
       t3.modifyuserid,
       t3.medicinename_chr_right,
       t3.drop_chr_right,
       t3.minute_chr_right,
       t3.ingear_chr_right,
       t3.abnormity_chr_right,
       t3.solve_chr_right,
       t3.remark_chr_right,
       t3.underwrite_chr_right,
       t3.checkdate_date,
       t3.begintime_date_right
  from icuacad_veinspecialusedrug t1, icuacad_veinspecialusedrugcon t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.begintime_date, t3.modifydate";


//        private const string c_strGetRecordContentSQL_Single = @"SELECT distinct f_getempnamebyno(T1.CREATEUSERID) AS CreateUserName,
//																	T1.*,
//																	t3.*
//																FROM icuacad_veinspecialusedrug   T1,
//																	icuacad_veinspecialusedrugcon   T3
//																WHERE T1.InPatientID = T3.InPatientID
//																AND T1.InPatientDate = T3.InPatientDate
//																AND T1.OpenDate = T3.OpenDate
//																AND T1.Status =0
//																AND T1.InPatientID = ?
//																AND T1.InPatientDate = ?
//																AND T1.OpenDate = ?
//																ORDER BY T1.BEGINTIME_DATE ,t3.modifydate";
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
                t3.modifydate,
                t3.modifyuserid,
                t3.medicinename_chr_right,
                t3.drop_chr_right,
                t3.minute_chr_right,
                t3.ingear_chr_right,
                t3.abnormity_chr_right,
                t3.solve_chr_right,
                t3.remark_chr_right,
                t3.underwrite_chr_right,
                t3.checkdate_date,
                t3.begintime_date_right
  from icuacad_veinspecialusedrug t1, icuacad_veinspecialusedrugcon t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.begintime_date, t3.modifydate";

        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from icuacad_veinspecialusedrug 
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        private const string c_strDeleteRecordSQL = @"update icuacad_veinspecialusedrug 
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";
        //获取记录时间列表
        private const string c_strGetCheckDateSQL = @"select checkdate_date 
                                                        from icu_aiad_veinspecialcheckdate
                                                        where  deleted_int = 0 
                                                               and inpatientid = ?
                                                               and inpatientdate = ?
                                                        order by checkdate_date asc";
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
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsVeinSpecialUseDrug_MainService","m_lngUpdateFirstPrintDate");
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
            out clsVeinSpecialUseDrugValue[] p_objTansDataInfo)
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
                    p_objTansDataInfo = new clsVeinSpecialUseDrugValue[dtbValue.Rows.Count];
                    clsVeinSpecialUseDrugValue objRecordContent = null;

                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date == dtmOpenDate)
                        {
                            #region 从DataTable.Rows中获取结果

                            objRecordContent = new clsVeinSpecialUseDrugValue();
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

                            objRecordContent.m_strBEGINTIME_DATE = dtbValue.Rows[j]["BEGINTIME_DATE"].ToString();
                            objRecordContent.m_strBEGINTIME_DATE_RIGHT = dtbValue.Rows[j]["BEGINTIME_DATE_RIGHT"].ToString();
                            objRecordContent.m_strBEGINTIME_DATEXML = dtbValue.Rows[j]["BEGINTIME_DATEXML"].ToString();

                            objRecordContent.m_strMEDICINENAME_CHR = dtbValue.Rows[j]["MEDICINENAME_CHR"].ToString();
                            objRecordContent.m_strMEDICINENAME_CHR_RIGHT = dtbValue.Rows[j]["MEDICINENAME_CHR_RIGHT"].ToString();
                            objRecordContent.m_strMEDICINENAME_CHRXML = dtbValue.Rows[j]["MEDICINENAME_CHRXML"].ToString();

                            objRecordContent.m_strDROP_CHR = dtbValue.Rows[j]["DROP_CHR"].ToString();
                            objRecordContent.m_strDROP_CHR_RIGHT = dtbValue.Rows[j]["DROP_CHR_RIGHT"].ToString();
                            objRecordContent.m_strDROP_CHRXML = dtbValue.Rows[j]["DROP_CHRXML"].ToString();

                            objRecordContent.m_strMINUTE_CHR = dtbValue.Rows[j]["MINUTE_CHR"].ToString();
                            objRecordContent.m_strMINUTE_CHR_RIGHT = dtbValue.Rows[j]["MINUTE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strMINUTE_CHRXML = dtbValue.Rows[j]["MINUTE_CHRXML"].ToString();

                            objRecordContent.m_strINGEAR_CHR = dtbValue.Rows[j]["INGEAR_CHR"].ToString();
                            objRecordContent.m_strINGEAR_CHR_RIGHT = dtbValue.Rows[j]["INGEAR_CHR_RIGHT"].ToString();
                            objRecordContent.m_strINGEAR_CHRXML = dtbValue.Rows[j]["INGEAR_CHRXML"].ToString();

                            objRecordContent.m_strABNORMITY_CHR = dtbValue.Rows[j]["ABNORMITY_CHR"].ToString();
                            objRecordContent.m_strABNORMITY_CHR_RIGHT = dtbValue.Rows[j]["ABNORMITY_CHR_RIGHT"].ToString();
                            objRecordContent.m_strABNORMITY_CHRXML = dtbValue.Rows[j]["ABNORMITY_CHRXML"].ToString();


                            objRecordContent.m_strSOLVE_CHR = dtbValue.Rows[j]["SOLVE_CHR"].ToString();
                            objRecordContent.m_strSOLVE_CHR_RIGHT = dtbValue.Rows[j]["SOLVE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strSOLVE_CHRXML = dtbValue.Rows[j]["SOLVE_CHRXML"].ToString();

                            objRecordContent.m_strREMARK_CHR = dtbValue.Rows[j]["REMARK_CHR"].ToString();
                            objRecordContent.m_strREMARK_CHR_RIGHT = dtbValue.Rows[j]["REMARK_CHR_RIGHT"].ToString();
                            objRecordContent.m_strREMARK_CHRXML = dtbValue.Rows[j]["REMARK_CHRXML"].ToString();

                            objRecordContent.m_strUNDERWRITE_CHR = dtbValue.Rows[j]["UNDERWRITE_CHR"].ToString();
                            objRecordContent.m_strUNDERWRITE_CHR_RIGHT = dtbValue.Rows[j]["UNDERWRITE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strUNDERWRITE_CHRXML = dtbValue.Rows[j]["UNDERWRITE_CHRXML"].ToString();

                            objRecordContent.m_strCHECKDATE_DATE = Convert.ToDateTime(dtbValue.Rows[j]["CHECKDATE_DATE"]).ToString("yyyy-MM-dd HH:mm:ss");


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

            } return (long)enmOperationResult.DB_Succeed;
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
            clsVeinSpecialUseDrugValue[] p_objGeneralNurseRecordArr = null;
            //			clsGeneralNurseRecordContent_GXDetail[] p_objGeneralNurseDetailArr = null;
            p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
            long lngRes = -1;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsVeinSpecialUseDrugValueContentDataInfo objDataInfo = new clsVeinSpecialUseDrugValueContentDataInfo();

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
                    clsVeinSpecialUseDrugValue objRecordContent = null;
                    p_objGeneralNurseRecordArr = new clsVeinSpecialUseDrugValue[dtbContent.Rows.Count];
                    for (int i = 0 ; i < dtbContent.Rows.Count ; i++)
                    {
                        #region set values
                        objRecordContent = new clsVeinSpecialUseDrugValue();
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

                        objRecordContent.m_strBEGINTIME_DATE = dtbContent.Rows[i]["BEGINTIME_DATE"].ToString();
                        objRecordContent.m_strBEGINTIME_DATE_RIGHT = dtbContent.Rows[i]["BEGINTIME_DATE_RIGHT"].ToString();
                        objRecordContent.m_strBEGINTIME_DATEXML = dtbContent.Rows[i]["BEGINTIME_DATEXML"].ToString();

                        objRecordContent.m_strMEDICINENAME_CHR = dtbContent.Rows[i]["MEDICINENAME_CHR"].ToString();
                        objRecordContent.m_strMEDICINENAME_CHR_RIGHT = dtbContent.Rows[i]["MEDICINENAME_CHR_RIGHT"].ToString();
                        objRecordContent.m_strMEDICINENAME_CHRXML = dtbContent.Rows[i]["MEDICINENAME_CHRXML"].ToString();

                        objRecordContent.m_strDROP_CHR = dtbContent.Rows[i]["DROP_CHR"].ToString();
                        objRecordContent.m_strDROP_CHR_RIGHT = dtbContent.Rows[i]["DROP_CHR_RIGHT"].ToString();
                        objRecordContent.m_strDROP_CHRXML = dtbContent.Rows[i]["DROP_CHRXML"].ToString();

                        objRecordContent.m_strMINUTE_CHR = dtbContent.Rows[i]["MINUTE_CHR"].ToString();
                        objRecordContent.m_strMINUTE_CHR_RIGHT = dtbContent.Rows[i]["MINUTE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strMINUTE_CHRXML = dtbContent.Rows[i]["MINUTE_CHRXML"].ToString();

                        objRecordContent.m_strINGEAR_CHR = dtbContent.Rows[i]["INGEAR_CHR"].ToString();
                        objRecordContent.m_strINGEAR_CHR_RIGHT = dtbContent.Rows[i]["INGEAR_CHR_RIGHT"].ToString();
                        objRecordContent.m_strINGEAR_CHRXML = dtbContent.Rows[i]["INGEAR_CHRXML"].ToString();

                        objRecordContent.m_strABNORMITY_CHR = dtbContent.Rows[i]["ABNORMITY_CHR"].ToString();
                        objRecordContent.m_strABNORMITY_CHR_RIGHT = dtbContent.Rows[i]["ABNORMITY_CHR_RIGHT"].ToString();
                        objRecordContent.m_strABNORMITY_CHRXML = dtbContent.Rows[i]["ABNORMITY_CHRXML"].ToString();


                        objRecordContent.m_strSOLVE_CHR = dtbContent.Rows[i]["SOLVE_CHR"].ToString();
                        objRecordContent.m_strSOLVE_CHR_RIGHT = dtbContent.Rows[i]["SOLVE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strSOLVE_CHRXML = dtbContent.Rows[i]["SOLVE_CHRXML"].ToString();

                        objRecordContent.m_strREMARK_CHR = dtbContent.Rows[i]["REMARK_CHR"].ToString();
                        objRecordContent.m_strREMARK_CHR_RIGHT = dtbContent.Rows[i]["REMARK_CHR_RIGHT"].ToString();
                        objRecordContent.m_strREMARK_CHRXML = dtbContent.Rows[i]["REMARK_CHRXML"].ToString();

                        objRecordContent.m_strUNDERWRITE_CHR = dtbContent.Rows[i]["UNDERWRITE_CHR"].ToString();
                        objRecordContent.m_strUNDERWRITE_CHR_RIGHT = dtbContent.Rows[i]["UNDERWRITE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strUNDERWRITE_CHRXML = dtbContent.Rows[i]["UNDERWRITE_CHRXML"].ToString();

                        objRecordContent.m_strCHECKDATE_DATE = Convert.ToDateTime(dtbContent.Rows[i]["CHECKDATE_DATE"]).ToString("yyyy-MM-dd HH:mm:ss");

                        objRecordContent.m_strID_CHR = dtbContent.Rows[i]["ID_CHR"].ToString();

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

            } return (long)enmOperationResult.DB_Succeed;
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
											icuacad_veinspecialusedrug  t1,icuacad_veinspecialusedrugcon  t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status =0
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

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

            } return lngRes;

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

            } return lngRes;
        }
        #endregion

        #region 新建开始用药时间
        /// <summary>
        /// 新建开始用药时间
        /// </summary>
        [AutoComplete]
        public long m_lngInsertCheckDate(
            string p_strInPatientID,
            string p_strInPatientDate)
        {

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            long lngEff = 0;

            //获取IDataParameter数组
            string SQL = @"select max_sequence_id_chr from t_aid_table_sequence_id 
							where lower(trim(table_name_vchr)) = lower(trim('ICU_AIAD_VEINSPECIALCHECKDATE'))
											and lower(trim(col_name_vchr)) = lower(trim('ID_CHR'))";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dt = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0 && dt != null)
                {
                    string strRetID = dt.Rows[0]["max_sequence_id_chr"].ToString().Trim();

                    string strSqlUpdate = @"update t_aid_table_sequence_id set MAX_SEQUENCE_ID_CHR=to_number(MAX_SEQUENCE_ID_CHR)+1 
											where lower(trim(table_name_vchr)) = lower(trim('ICU_AIAD_VEINSPECIALCHECKDATE'))
											and lower(trim(col_name_vchr)) = lower(trim('ID_CHR'))";
                    lngRes = objHRPServ.DoExcute(strSqlUpdate);
                    if (lngRes > 0)
                    {
                        string strSQLInsertCheckDate = @"insert into icu_aiad_veinspecialcheckdate
															(inpatientid,
															inpatientdate, checkdate_date,id_chr
															)
													values (?,?, sysdate,?)";

                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].Value = p_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                        objDPArr[2].Value = strRetID;

                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQLInsertCheckDate, ref lngEff, objDPArr);
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            if (lngRes <= 0) return lngRes;
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region 取开始用药时间
        /// <summary>
        /// 取开始用药时间
        /// </summary>
        [AutoComplete]
        public long m_lngGetSELECTCheckDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            out DataTable p_dtResult)
        {

            //检查参数
            p_dtResult = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            //获取IDataParameter数组
            //			string strSQLSELECTCheckDate =@"SELECT   b.createdate,a.checkdate_date, a.endtime_date, a.id_chr
            //						FROM icu_aiad_veinspecialcheckdate a , ICUACAD_VEINSPECIALUSEDRUG b
            //									WHERE a.deleted_int = 0 and b.status=0 and a.id_chr = b.id_chr
            //											AND a.inpatientid = '"+p_strInPatientID+@"'
            //											AND a.inpatientdate =TO_DATE ('"+p_strInPatientDate+"', 'yyyy-mm-dd hh24:mi:ss') ORDER BY b.createdate ASC";
            string strSQLSELECTCheckDate = @"select createdate,checkdate_date,endtime_date,id_chr from  icu_aiad_veinspecialcheckdate
														where deleted_int=0 and 
													inpatientid =? and  inpatientdate = ? order by createdate asc";
            long lngRes = 0;
            long lngEff = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                p_dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQLSELECTCheckDate, ref p_dtResult, objDPArr);
                if (lngRes <= 0)
                {
                    p_dtResult = null;
                    return lngRes;
                }

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region 更新首次打印时间
        /// <summary>
        /// 更新首次打印时间。
        /// </summary>
        [AutoComplete]
        public long m_lngUpdateFirstPrintDateNew(
            string p_strID_CHR,
            DateTime p_dtmFirstPrintDate)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = p_dtmFirstPrintDate;
            objDPArr[1].Value = p_strID_CHR;
            //执行SQL
            long lngRes = 0;
            long lngEff = 0;
            string strUpdateFirstPrintDateSQL = @"update icuacad_veinspecialusedrug 
																set firstprintdate = ?
															where id_chr = ?
																and firstprintdate is null
																and status = 0";


            try
            {
                lngRes = objHRPServ.lngExecuteParameterSQL(strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes; if (lngRes <= 0) return lngRes;

            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region 获取记录时间列表
        ///<summary>
        ///获取记录时间列表
        ///</summary>
        ///<param name="p_strInPatientID">住院号</param>
        ///<param name="p_strInPatientDate">住院日期</param>
        ///<returns></returns>
        [AutoComplete]
        public long m_lngGetCheckDate(string p_strInPatientID, string p_strInPatientDate, out DateTime[] p_dtmCheckDateArr)
        {
            p_dtmCheckDateArr=null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate.Trim());
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetCheckDateSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_dtmCheckDateArr = new DateTime[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_dtmCheckDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CHECKDATE_DATE"].ToString());
                    }
                }
                return lngRes;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }
            finally
            {
            }
            
        }

        #endregion

        #region 从主表获取指定CheckDate的记录
        ///<summary>从主表获取指定CheckDate的记录</summary>
        ///<param name="p_strInPatientID">住院号</param>
        ///<param name="p_strInPatientDate">入院日期</param>
        ///<param name="p_dtmCheckDate">CheckDate</param>
        ///<returns></returns>
        [AutoComplete]
        public long m_lngGetCheckDateRecord(string p_strInPatientID, string p_strInPatientDate, DateTime p_dtmCheckDate, out clsVeinSpecialUseDrugValue[] p_objResultArr)
        {
            p_objResultArr = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_dtmCheckDate == null || p_dtmCheckDate == DateTime.MinValue)
                return (long)enmOperationResult.Parameter_Error;
            string c_strSQL = @" select t1.inpatientid,
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
       t2.checkdate_date as fluidbegintime_date,
       t2.endtime_date
  from icuacad_veinspecialusedrug t1
  left join icu_aiad_veinspecialcheckdate t2 on t1.inpatientid =
                                                t2.inpatientid
                                            and t1.inpatientdate =
                                                t2.inpatientdate
                                            and t1.id_chr = t2.id_chr
 where t1.inpatientid = ?
   and t1.status = 0
   and t1.inpatientdate = ?
   and t2.deleted_int = 0
   and t2.checkdate_date = ?
 order by t1.begintime_date asc";                       
                                                     
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 1;
            try
            {
                DataTable dtbValue = new DataTable();
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmCheckDate;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strSQL, ref dtbValue, objDPArr);
                if (lngRes > 0)
                {
                    if (dtbValue.Rows.Count > 0)
                    {
                        p_objResultArr = new clsVeinSpecialUseDrugValue[dtbValue.Rows.Count];
                        for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                        {
                            #region set value
                            p_objResultArr[i1] = new clsVeinSpecialUseDrugValue();

                            p_objResultArr[i1].m_strInPatientID = p_strInPatientID;
                            p_objResultArr[i1].m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            p_objResultArr[i1].m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[i1]["CREATEDATE"].ToString());

                            p_objResultArr[i1].m_dtmFluidBEGINTIME_DATE = DateTime.Parse(dtbValue.Rows[i1]["fluidbegintime_date"].ToString());
                            p_objResultArr[i1].m_dtmfluidEndTIME_DATE = DateTime.Parse(dtbValue.Rows[i1]["endtime_date"].ToString());

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
                    else
                    {
                         c_strSQL = @"select endtime_date 
                                                from icu_aiad_veinspecialcheckdate
                                                where inpatientid = ?
                                                      and inpatientdate = ?
                                                      and checkdate_date = ?
                                                      and deleted_int = 0";

                         DataTable dtbValue1 = new DataTable();
                         //获取IDataParameter数组
                         IDataParameter[] objDPArr1 = null;
                         objHRPSvc.CreateDatabaseParameter(3, out objDPArr1);
                         objDPArr1[0].Value = p_strInPatientID;
                         objDPArr1[1].DbType = DbType.DateTime;
                         objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                         objDPArr1[2].DbType = DbType.DateTime;
                         objDPArr1[2].Value = p_dtmCheckDate;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strSQL, ref dtbValue1, objDPArr1);
                        if (lngRes > 0 && dtbValue1.Rows.Count > 0)
                        {
                            p_objResultArr = new clsVeinSpecialUseDrugValue[1];
                            p_objResultArr[0] = new clsVeinSpecialUseDrugValue();
                            p_objResultArr[0].m_strInPatientID = p_strInPatientID;
                            p_objResultArr[0].m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                            p_objResultArr[0].m_dtmFluidBEGINTIME_DATE = p_dtmCheckDate;
                            p_objResultArr[0].m_dtmfluidEndTIME_DATE = DateTime.Parse(dtbValue1.Rows[0]["endtime_date"].ToString());
                        }
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
    }
}
