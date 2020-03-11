using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data; 
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 病案质量反馈通知服务类

    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCaseQualityFeedBackServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 通过住院号获取病案质量反馈通知病人信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_objFeedBackInfo">病案质量反馈通知病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo( string p_strInpatientId, out clsQualityFeedBackInfo_VO p_objFeedBackInfo)
        {
            p_objFeedBackInfo = null;
            if (p_strInpatientId == null || p_strInpatientId.Length <= 0)
                return -1;

            long lngRes = 0;
            try
            { 

                string strSQL = @"select a.hisinpatientid_chr hisinpatientid,
       b.inareadate_dat     inareadate,
       c.lastname_vchr      lastname,
       d.outhospital_dat    outhospitaldate,
       e.deptname_vchr      deptname,
       f.deptname_vchr      areaname
  from t_bse_hisemr_relation    a,
       t_opr_bih_register       b,
       t_opr_bih_registerdetail c,
       t_opr_bih_leave          d,
       t_bse_deptdesc           e,
       t_bse_deptdesc           f
 where a.registerid_chr = b.registerid_chr
   and a.registerid_chr = c.registerid_chr
   and a.registerid_chr = d.registerid_chr
   and d.outdeptid_chr = e.deptid_chr
   and d.outareaid_chr = f.deptid_chr
   and a.hisinpatientid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                DataRow dr;
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    dr = dtResult.Rows[0];
                    p_objFeedBackInfo = new clsQualityFeedBackInfo_VO();
                    p_objFeedBackInfo.m_strHisInpatientId = dr["hisinpatientid"] != DBNull.Value ? dr["hisinpatientid"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_strInpatientName = dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_strInpatientDept = dr["deptname"] != DBNull.Value ? dr["deptname"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_strInpatientArea = dr["areaname"] != DBNull.Value ? dr["areaname"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_dtInpatientDat = dr["inareadate"] != DBNull.Value ? Convert.ToDateTime(dr["inareadate"]) : DateTime.MinValue;
                    p_objFeedBackInfo.m_dtOutHospitalDat = dr["outhospitaldate"] != DBNull.Value ? Convert.ToDateTime(dr["outhospitaldate"]) : DateTime.MinValue;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_strInpatientId = null;
            return lngRes;
        }

        /// <summary>
        /// 通过住院登记号获取病案质量反馈通知病人信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_objFeedBackInfo">病案质量反馈通知病人信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByReg( string p_strRegisterId, out clsQualityFeedBackInfo_VO p_objFeedBackInfo)
        {
            p_objFeedBackInfo = null;
            if (p_strRegisterId == null || p_strRegisterId.Length <= 0)
                return -1;

            long lngRes = 0;
            try
            { 

                string strSQL = @"select a.hisinpatientid_chr hisinpatientid,
       b.inareadate_dat     inareadate,
       c.lastname_vchr      lastname,
       d.outhospital_dat    outhospitaldate,
       e.deptname_vchr      deptname,
       f.deptname_vchr      areaname
  from t_bse_hisemr_relation    a,
       t_opr_bih_register       b,
       t_opr_bih_registerdetail c,
       t_opr_bih_leave          d,
       t_bse_deptdesc           e,
       t_bse_deptdesc           f
 where a.registerid_chr = b.registerid_chr
   and a.registerid_chr = c.registerid_chr
   and a.registerid_chr = d.registerid_chr
   and d.outdeptid_chr = e.deptid_chr
   and d.outareaid_chr = f.deptid_chr
   and a.registerid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                DataRow dr;
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    dr = dtResult.Rows[0];
                    p_objFeedBackInfo = new clsQualityFeedBackInfo_VO();
                    p_objFeedBackInfo.m_strHisInpatientId = dr["hisinpatientid"] != DBNull.Value ? dr["hisinpatientid"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_strInpatientName = dr["lastname"] != DBNull.Value ? dr["lastname"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_strInpatientDept = dr["deptname"] != DBNull.Value ? dr["deptname"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_strInpatientArea = dr["areaname"] != DBNull.Value ? dr["areaname"].ToString() : string.Empty;
                    p_objFeedBackInfo.m_dtInpatientDat = dr["inareadate"] != DBNull.Value ? Convert.ToDateTime(dr["inareadate"]) : DateTime.MinValue;
                    p_objFeedBackInfo.m_dtOutHospitalDat = dr["outhospitaldate"] != DBNull.Value ? Convert.ToDateTime(dr["outhospitaldate"]) : DateTime.MinValue;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_strRegisterId = null;
            return lngRes;
        }

        /// <summary>
        /// 获取病案质量反馈通知
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_dtInpatientDat">入院时间</param>
        /// <param name="p_objFeedBack">病案质量反馈通知</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeedBackInfo( string p_strInpatientId, DateTime p_dtInpatientDat, out clsCaseQualityFeedBack_VO p_objFeedBack)
        {
            p_objFeedBack = null;
            if (p_strInpatientId == null || p_dtInpatientDat == DateTime.MinValue)
                return -1;
            long lngRes = 0;
            try
            { 
                DataTable dtResult = null;
                DataRow dr;

                #region 先从t_emr_casequalityfeedback表中获取病案质量反馈通知
                string strSQL = @"select b.feedbackid_int,
       b.registerid_chr,
       b.doctorid_chr,
       b.doctorname_vchr,
       b.message_vchr,
       b.createddate_dat,
       b.recorddate_dat,
       b.signindocid_chr,
       b.signindate_dat,
       b.status_int,
       b.deactivedate,
       b.deactiveuserid,
       b.signindocname_vchr,
       b.bedno_vchr,
       c.lastname_vchr
  from t_bse_hisemr_relation     a,
       t_emr_casequalityfeedback b,
       t_bse_employee            c
 where a.registerid_chr = b.registerid_chr
   and b.signindocid_chr = c.empid_chr(+)
   and b.status_int = 1
   and a.hisinpatientid_chr = ?
   and a.hisinpatientdate between ? and ?
";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtInpatientDat.Date;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtInpatientDat.Date.AddHours(24);
                //DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objFeedBack = new clsCaseQualityFeedBack_VO();
                    dr = dtResult.Rows[0];

                    p_objFeedBack.m_intFeedBackId_int = dr["feedbackid_int"] != DBNull.Value ? Convert.ToInt32(dr["feedbackid_int"]) : 0;
                    p_objFeedBack.m_strRegisterId_chr = dr["registerid_chr"] != DBNull.Value ? dr["registerid_chr"].ToString() : string.Empty;
                    p_objFeedBack.m_strDoctorId_chr = dr["doctorid_chr"] != DBNull.Value ? dr["doctorid_chr"].ToString() : string.Empty;
                    p_objFeedBack.m_strDoctorName_vchr = dr["doctorname_vchr"] != DBNull.Value ? dr["doctorname_vchr"].ToString() : string.Empty;
                    p_objFeedBack.m_strMessage_vchr = dr["message_vchr"] != DBNull.Value ? dr["message_vchr"].ToString() : string.Empty;
                    p_objFeedBack.m_dtCreatedDate_dat = dr["createddate_dat"] != DBNull.Value ? Convert.ToDateTime(dr["createddate_dat"]) : DateTime.MinValue;
                    p_objFeedBack.m_dtRecordDate_dat = dr["recorddate_dat"] != DBNull.Value ? Convert.ToDateTime(dr["recorddate_dat"]) : DateTime.MinValue;
                    p_objFeedBack.m_strSigninDocId_chr = dr["signindocid_chr"] != DBNull.Value ? dr["signindocid_chr"].ToString() : string.Empty;
                    p_objFeedBack.m_strSigninDocName = dr["lastname_vchr"] != DBNull.Value ? dr["lastname_vchr"].ToString() : string.Empty;
                    p_objFeedBack.m_dtSigninDate_dat = dr["signindate_dat"] != DBNull.Value ? Convert.ToDateTime(dr["signindate_dat"]) : DateTime.MinValue;
                    p_objFeedBack.m_intStatus_int = 1;
                    p_objFeedBack.m_dtDeactiveDate_dat = dr["deactivedate"] != DBNull.Value ? Convert.ToDateTime(dr["deactivedate"]) : DateTime.MinValue;
                    p_objFeedBack.m_strDeactiveUserId_chr = dr["deactiveuserid"] != DBNull.Value ? dr["deactiveuserid"].ToString() : string.Empty;
                    p_objFeedBack.m_strSigninDocName_vchr = dr["signindocname_vchr"] != DBNull.Value ? dr["signindocname_vchr"].ToString() : string.Empty;
                    p_objFeedBack.m_strBedNo_vchr = dr["bedno_vchr"] != DBNull.Value ? dr["bedno_vchr"].ToString() : string.Empty;
                }
                #endregion
                else
                {
                    // 通过扣分项目明细表生成病案质量反馈通知
                    lngRes = m_lngGetFeedBackFromGraded(p_strInpatientId, p_dtInpatientDat, out p_objFeedBack);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            } 
            p_strInpatientId = null;
            return lngRes;
        }

        /// <summary>
        /// 通过扣分项目明细表生成病案质量反馈通知
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInpatientId">住院号</param>
        /// <param name="p_dtInpatientDat">入院时间</param>
        /// <param name="p_objFeedBack">病案质量反馈通知</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeedBackFromGraded( string p_strInpatientId, DateTime p_dtInpatientDat, out clsCaseQualityFeedBack_VO p_objFeedBack)
        {
            p_objFeedBack = null;
            if (p_strInpatientId == null && p_dtInpatientDat == DateTime.MinValue)
                return -1;
            long lngRes = 0;
            try
            { 

                string strSQL = @"select a.registerid_chr,
       b.changedocid,
       b.changedocname,
       c.deductcause_vchr,
       c.realdeduct_int,
       d.outbedid_chr,
       e.deductgrade_int
  from t_bse_hisemr_relation  a,
       t_emr_casegraded       b,
       t_emr_casegradeddetail c,
       t_opr_bih_leave        d,
       t_emr_casegradeitem    e
 where a.registerid_chr = b.registerid_chr
   and b.gradeseqid_int = c.gradeseqid_int
   and c.itemid_int = e.itemid_int
   and b.status_int = 1
   and c.status_int = 1
   and e.status_int = 1
   and a.registerid_chr = d.registerid_chr
   and a.hisinpatientid_chr = ?
   and a.hisinpatientdate between ? and ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtInpatientDat.Date;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtInpatientDat.Date.AddHours(24);

                DataTable dtResult = null;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objFeedBack = new clsCaseQualityFeedBack_VO();
                    DataRow dr = dtResult.Rows[0];
                    p_objFeedBack.m_intFeedBackId_int = 0;

                    p_objFeedBack.m_strRegisterId_chr = dr["registerid_chr"].ToString();
                    p_objFeedBack.m_strDoctorId_chr = dr["changedocid"] != DBNull.Value ? dr["changedocid"].ToString() : string.Empty;
                    p_objFeedBack.m_strDoctorName_vchr = dr["changedocname"] != DBNull.Value ? dr["changedocname"].ToString() : string.Empty;
                    p_objFeedBack.m_strBedNo_vchr = dr["outbedid_chr"] != DBNull.Value ? dr["outbedid_chr"].ToString() : string.Empty;
                    //p_objFeedBack.strMessage_vchr = string.Empty;

                    StringBuilder strBuilder = new StringBuilder();
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        dr = dtResult.Rows[iRow];
                        if (Convert.ToInt32(dr["deductgrade_int"]) == 0)
                        {
                            strBuilder.Append(Convert.ToString(iRow + 1)).Append("、").Append(dr["deductcause_vchr"].ToString());
                            strBuilder.Append("(").Append(dr["realdeduct_int"].ToString()).Append("分);\r\n");
                        }
                        else
                        {
                            switch (Convert.ToInt32(dr["deductgrade_int"]))
                            {
                                case 1:
                                    {
                                        strBuilder.Append(Convert.ToString(iRow + 1)).Append("、").Append(dr["deductcause_vchr"].ToString());
                                        strBuilder.Append("(").Append("乙级").Append(")；\r\n");
                                        break;
                                    }
                                case 2:
                                    {
                                        strBuilder.Append(Convert.ToString(iRow + 1)).Append("、").Append(dr["deductcause_vchr"].ToString());
                                        strBuilder.Append("(").Append("丙级").Append(")；\r\n");
                                        break;
                                    }
                            }
                        }
                    }
                    p_objFeedBack.m_strMessage_vchr = strBuilder.ToString();

                    p_objFeedBack.m_dtCreatedDate_dat = DateTime.MinValue;
                    p_objFeedBack.m_dtDeactiveDate_dat = DateTime.MinValue;
                    p_objFeedBack.m_dtRecordDate_dat = DateTime.MinValue;
                    p_objFeedBack.m_dtSigninDate_dat = DateTime.MinValue;

                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            } 
            p_strInpatientId = null;
            return lngRes;
        }

        /// <summary>
        /// 更新病案质量反馈通知.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objFeedBack">病案质量反馈通知</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateFeedBack( clsCaseQualityFeedBack_VO p_objFeedBack)
        {
            if (p_objFeedBack == null)
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"update t_emr_casequalityfeedback t
      set t.message_vchr    = ?,
          t.recorddate_dat  = ?,
          t.signindocid_chr = ?,
          t.signindate_dat  = ?
    where t.feedbackid_int = ?
      and t.status_int = ?";

                clsHRPTableService objHRPSrev = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSrev.CreateDatabaseParameter(6, out objDPArr);

                objDPArr[0].Value = p_objFeedBack.m_strMessage_vchr;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objFeedBack.m_dtRecordDate_dat;
                if (string.IsNullOrEmpty(p_objFeedBack.m_strSigninDocId_chr))
                {
                    objDPArr[2].Value = DBNull.Value;
                    objDPArr[3].Value = DBNull.Value;
                }
                else
                {
                    objDPArr[2].Value = p_objFeedBack.m_strSigninDocId_chr;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_objFeedBack.m_dtSigninDate_dat;
                }
                objDPArr[4].Value = p_objFeedBack.m_intFeedBackId_int;
                objDPArr[5].Value = 1;

                long lngRecordAffect = 0;
                lngRes = objHRPSrev.lngExecuteParameterSQL(strSQL, ref lngRecordAffect, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_objFeedBack = null; 
            return lngRes;
        }

        /// <summary>
        /// 插入病案质量反馈通知记录.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objFeedBack">病案质量反馈通知</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertFeedBack( clsCaseQualityFeedBack_VO p_objFeedBack)
        {
            if (p_objFeedBack == null)
                return -1;
            long lngRes = 0;
            try
            { 
                // 获取一个序列号
                if (p_objFeedBack.m_intFeedBackId_int == 0)
                {
                    int iSeqId = 0;
                    clsPublicGradeServ objServ = new clsPublicGradeServ();
                    lngRes = objServ.m_lngGetSequence( out iSeqId);
                    p_objFeedBack.m_intFeedBackId_int = iSeqId;
                }

                string strSQL = @"insert into t_emr_casequalityfeedback t
      (t.feedbackid_int,
       t.registerid_chr,
       t.doctorid_chr,
       t.doctorname_vchr,
       t.message_vchr,
       t.createddate_dat,
       t.recorddate_dat,
       t.signindocid_chr,
       t.signindate_dat,
       t.status_int,
       t.bedno_vchr)
    values
      (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(11, out objDPArr);

                objDPArr[0].Value = p_objFeedBack.m_intFeedBackId_int;
                objDPArr[1].Value = p_objFeedBack.m_strRegisterId_chr;
                objDPArr[2].Value = p_objFeedBack.m_strDoctorId_chr;
                objDPArr[3].Value = p_objFeedBack.m_strDoctorName_vchr;
                objDPArr[4].Value = p_objFeedBack.m_strMessage_vchr;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objFeedBack.m_dtCreatedDate_dat;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = p_objFeedBack.m_dtRecordDate_dat;
                if (string.IsNullOrEmpty(p_objFeedBack.m_strSigninDocId_chr))
                {
                    objDPArr[7].Value = DBNull.Value;
                    objDPArr[8].Value = DBNull.Value;
                }
                else
                {
                    objDPArr[7].Value = p_objFeedBack.m_strSigninDocId_chr;
                    objDPArr[8].DbType = DbType.DateTime;
                    objDPArr[8].Value = p_objFeedBack.m_dtSigninDate_dat;
                }
                objDPArr[9].Value = 1;
                objDPArr[10].Value = p_objFeedBack.m_strBedNo_vchr;

                long lngRecordAffect = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRecordAffect, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            } 
            p_objFeedBack = null;
            return lngRes;
        }

        /// <summary>
        /// 删除病案质量反馈通知记录,只是把状态改为 0,并记录删除记录者.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objFeedBack">病案质量反馈通知</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteFeedBack( clsCaseQualityFeedBack_VO p_objFeedBack)
        {
            if (p_objFeedBack == null)
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"update t_emr_casequalityfeedback t
    set t.status_int         = 0,
        t.deactivedate       = ?,
        t.deactiveuserid     = ?,
        t.signindocname_vchr = ?
  where t.feedbackid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objFeedBack.m_dtDeactiveDate_dat;
                objDPArr[1].Value = p_objFeedBack.m_strDeactiveUserId_chr;
                objDPArr[2].Value = p_objFeedBack.m_strSigninDocName_vchr;
                objDPArr[3].Value = p_objFeedBack.m_intFeedBackId_int;

                long lngRecordAffect = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRecordAffect, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            } 
            p_objFeedBack = null;
            return lngRes;
        }
    }
}
