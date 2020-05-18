using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 病历评分服务类.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsGradedServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取有效的评分项目

        /// <summary>
        /// 获取有效的评分项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetActiveItem( out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;

            try
            { 
                string strSQL = @"select t.itemid_int       itemid,
       t.parentitemid_int parentitemid,
       t.itemdesc_vchar   itemdesc,
       t.itemtype_int     itemtype,
       t.deductscore_int  deductscore,
       t.deductgrade_int  deductgrade,
       t.multiitem_int    multiitem
  from t_emr_casegradeitem t
  where t.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 使用树结构查询获得有效的评分项目.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objGradeItemVOArr">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeItemByTree( out clsGradeItem_VO[] p_objGradeItemVOArr)
        {
            p_objGradeItemVOArr = null;
            long lngRes = 0;

            try
            { 
                string strSQL = @"select t.itemid_int,
       t.parentitemid_int,
       t.status_int,
       t.itemdesc_vchar,
       t.itemtype_int,
       t.deductscore_int,
       t.deductgrade_int,
       t.multiitem_int
  from t_emr_casegradeitem t
 where t.status_int = 1
connect by prior t.itemid_int = t.parentitemid_int
 start with parentitemid_int = 0
 order by t.itemid_int";

                DataTable dtResult = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

                if (dtResult.Rows.Count > 0)
                {
                    p_objGradeItemVOArr = new clsGradeItem_VO[dtResult.Rows.Count];
                    DataRow dr;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        clsGradeItem_VO objTempVO = new clsGradeItem_VO();
                        dr = dtResult.Rows[iRow];
                        objTempVO.m_intItemId_Int = Convert.ToInt32(dr["itemid_int"]);
                        objTempVO.m_intParentItemId_Int = Convert.ToInt32(dr["parentitemid_int"]);
                        objTempVO.m_strItemDesc_Vchr = Convert.ToString(dr["itemdesc_vchar"]);
                        objTempVO.m_intItemType_Int = Convert.ToInt32(dr["itemtype_int"]);
                        objTempVO.m_floatDeductsScore_Num = Convert.ToSingle(dr["deductscore_int"]);
                        objTempVO.m_intDeductGrade_Int = Convert.ToInt32(dr["deductgrade_int"]);
                        objTempVO.m_intMultItem_Int = Convert.ToInt32(dr["multiitem_int"]);

                        p_objGradeItemVOArr[iRow] = objTempVO;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        #endregion

        #region 获取已评分明细项目

        /// <summary>
        /// 获取已评分明细项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_iGradeseqid">关联t_emr_casegraded.</param>
        /// <param name="p_dtResult">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeDetail(int p_iGradeseqid, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;
            try
            { 
                string strSQL = @"select a.detailseqid_int,
            a.itemid_int,
            a.deductcause_vchr,
            a.realdeduct_int,
            b.parentitemid_int,
            b.itemdesc_vchar,
            b.deductgrade_int,
            b.deductscore_int
       from t_emr_casegradeddetail a,
            t_emr_casegradeitem b
      where a.gradeseqid_int = ?
        and a.itemid_int = b.itemid_int
        and a.status_int = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_iGradeseqid;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);
                if (p_dtResult != null)
                {
                    DataColumn dcCol = new DataColumn("deductgradedesc");
                    p_dtResult.Columns.Add(dcCol);

                    if (p_dtResult.Rows.Count > 0)
                    {
                        DataRow dr;
                        for (int iRow = 0; iRow < p_dtResult.Rows.Count; iRow++)
                        {
                            dr = p_dtResult.Rows[iRow];
                            switch (Convert.ToInt32(dr["deductgrade_int"]))
                            {
                                case 1:
                                    dr["deductgradedesc"] = "乙级";
                                    break;
                                case 2:
                                    dr["deductgradedesc"] = "丙级";
                                    break;
                                default:
                                    dr["deductgradedesc"] = string.Empty;
                                    break;
                            }
                        }
                    }
                }

                //if (dtResult.Rows.Count > 0)
                //{
                //    p_objResultDetailArr = new clsGradeResultDetail_VO[dtResult.Rows.Count];
                //    DataRow dr;
                //    clsGradeResultDetail_VO objTemp;
                //    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                //    {
                //        objTemp = new clsGradeResultDetail_VO();
                //        dr = dtResult.Rows[iRow];
                //        objTemp.lDetailSeqId_long = Convert.ToInt64(dr["detailseqid_int"]);
                //        objTemp.iItemId_int = Convert.ToInt32(dr["itemid_int"]);
                //        objTemp.strDeductCause_vchr = Convert.ToString(dr["deductcause_vchr"]);
                //        objTemp.fRealDeduct_float = Convert.ToSingle(dr["realdeduct_int"]);
                //        objTemp.iGradeSeqId_int = p_iGradeseqid;
                //        objTemp.iStatus_int = 1;

                //        p_objResultDetailArr[iRow] = objTemp;

                //    }
                //}
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            return lngRes;
        }
        #endregion

        #region 通过病人住院号,HIS最新住院号,查找要评分的病历的相关信息
        /// <summary>
        /// 通过病人住院号,HIS最新住院号,查找要评分的病历的相关信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_InPatientId">病人住院号</param>
        /// <param name="p_objResultVO">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeDetailByInPatientId( string p_InPatientId, out clsGradeResult_VO p_objResultVO)
        {
            p_objResultVO = null;
            long lngRes = 0;
            try
            { 
                string strSQL = @"select a.registerid_chr       registerid,
       a.inareadate_dat       indata,
       a.areaid_chr           areaid,
       a.deptid_chr           deptid,
       a.casedoctor_chr       doctorid,
       b.lastname_vchr        patientname,
       c.outhospital_dat      outdata,
       d.deptname_vchr        areaname,
       e.lastname_vchr        doctorname,
       f.deptname_vchr        deptname,
       g.gradeseqid_int       gradeseqid,
       g.recorderid_chr       recorderid,
       g.recorddate_dat       recorddate,
       g.score_int            score,
       g.graded_int           graded,
       g.canreversededuct_int canreversededuct,
       g.sign_int             sign_int,
       h.hisinpatientid_chr   inpatientid

  from t_opr_bih_register       a,
       t_opr_bih_registerdetail b,
       t_opr_bih_leave          c,
       t_bse_deptdesc           d,
       t_bse_employee           e,
       t_bse_deptdesc           f,
       t_emr_casegraded         g,
       t_bse_hisemr_relation    h

 where a.registerid_chr = b.registerid_chr(+)
   and a.registerid_chr = h.registerid_chr
   and a.status_int = 1
   and (g.status_int = 1 or g.status_int is null)
   and a.registerid_chr = g.registerid_chr(+)
   and a.registerid_chr = c.registerid_chr
   and a.areaid_chr = d.deptid_chr(+)
   and a.deptid_chr = f.deptid_chr(+)
   and a.casedoctor_chr = e.empid_chr
   and h.hisinpatientid_chr = ?
 order by h.hisinpatientdate desc";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_InPatientId;
                DataTable dtlData = null;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtlData, objDPArr);

                if (dtlData.Rows.Count >= 1)
                {
                    p_objResultVO = new clsGradeResult_VO();
                    DataRow dr = dtlData.Rows[0];

                    p_objResultVO.m_intGradeSeqId_int = dr["gradeseqid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["gradeseqid"]);
                    p_objResultVO.m_strRegisterId_chr = dr["registerid"] != DBNull.Value ? dr["registerid"].ToString() : string.Empty;
                    p_objResultVO.m_strInPatientId_vchr = dr["inpatientid"] != DBNull.Value ? dr["inpatientid"].ToString() : string.Empty;
                    p_objResultVO.m_strLastName_vchr = dr["patientname"] != DBNull.Value ? dr["patientname"].ToString() : string.Empty;
                    p_objResultVO.m_strRecorderId_chr = dr["recorderid"] != DBNull.Value ? dr["recorderid"].ToString() : string.Empty;
                    p_objResultVO.m_dtRecordDate_dat = dr["recorddate"] != DBNull.Value ? Convert.ToDateTime(dr["recorddate"]) : DateTime.MinValue;
                    p_objResultVO.m_intStatus_int = 1;
                    p_objResultVO.m_intSign_int = dr["sign_int"] != DBNull.Value ? Convert.ToInt32(dr["sign_int"]) : 0;
                    p_objResultVO.m_floatScore_float = dr["score"] != DBNull.Value ? Convert.ToSingle(dr["score"]) : 0f;
                    p_objResultVO.m_intGraded_int = dr["graded"] != DBNull.Value ? Convert.ToInt32(dr["graded"]) : 0;
                    p_objResultVO.m_intCanDeScore_int = dr["canreversededuct"] != DBNull.Value ? Convert.ToInt32(dr["canreversededuct"]) : 0;
                    p_objResultVO.m_strDeptId_chr = dr["deptid"] != DBNull.Value ? dr["deptid"].ToString() : string.Empty;
                    p_objResultVO.m_strDeptName_vchr = dr["deptname"] != DBNull.Value ? dr["deptname"].ToString() : string.Empty;
                    p_objResultVO.m_dtInHospitalDate_dat = dr["indata"] != DBNull.Value ? Convert.ToDateTime(dr["indata"]) : DateTime.MinValue;
                    p_objResultVO.m_dtOutHospitalDate_dat = dr["outdata"] != DBNull.Value ? Convert.ToDateTime(dr["outdata"]) : DateTime.MinValue;
                    p_objResultVO.m_strAreaId_chr = dr["areaid"] != DBNull.Value ? dr["areaid"].ToString() : string.Empty;
                    p_objResultVO.m_strAreaName_vchr = dr["areaname"] != DBNull.Value ? dr["areaname"].ToString() : string.Empty;
                    p_objResultVO.m_strChangeDocId_chr = dr["doctorid"] != DBNull.Value ? dr["doctorid"].ToString() : string.Empty;
                    p_objResultVO.m_strChangeDocName_vchr = dr["doctorname"] != DBNull.Value ? dr["doctorname"].ToString() : string.Empty;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 添加评分明细项目
        /// <summary>
        /// 添加评分明细项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetailArr">明细项目VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveGradeDetail( clsGradeResultDetail_VO[] p_objDetailArr)
        {
            if (p_objDetailArr == null || p_objDetailArr.Length <= 0)
                return -1;

            long lngRes = 0;
            try
            { 
                clsHRPTableService objServ = new clsHRPTableService();

                // 获得评分明细表的序列号
                int m_iStartId = 0;
                clsPublicGradeServ objPubServ = new clsPublicGradeServ();
                //lngRes = objPubServ.m_lngGetSequenceArr( p_objDetailArr.Length, out m_iStartId);
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
               
                string strSQL = @"insert into t_emr_casegradeddetail
  (detailseqid_int,
   gradeseqid_int,
   itemid_int,
   deductcause_vchr,
   realdeduct_int,
   status_int)
values
  (?, ?, ?, ?, ?, ?)";

                long lngEff = -1;
                IDataParameter[] objDPArr = null;
                clsGradeResultDetail_VO objDetailVO;
                for (int iRow = 0; iRow < p_objDetailArr.Length; iRow++)
                {
                    long lngSequence = 0;
                    objSign.m_lngGetSequenceValue("seq_emr_casegrade", out lngSequence);

                    objDetailVO = p_objDetailArr[iRow];
                    objServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = lngSequence;
                    objDPArr[1].Value = objDetailVO.m_intGradeSeqId_int;
                    objDPArr[2].Value = objDetailVO.m_intItemId_int;
                    objDPArr[3].Value = objDetailVO.m_strDeductCause_vchr;
                    objDPArr[4].Value = objDetailVO.m_floatRealDeduct_float;
                    objDPArr[5].Value = 1;

                    lngRes = objServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 修改住院评分明细项目

        /// <summary>
        /// 修改住院评分明细项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDetailArr">评分明细项目</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateGradeDetail( clsGradeResultDetail_VO[] p_objDetailArr)
        {
            if (p_objDetailArr == null || p_objDetailArr.Length <= 0)
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"update t_emr_casegradeddetail
set deductcause_vchr = ?,
realdeduct_int = ?
where detailseqid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_objDetailArr.Length; iRow++)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objDetailArr[iRow].m_strDeductCause_vchr;
                    objDPArr[1].Value = p_objDetailArr[iRow].m_floatRealDeduct_float;
                    objDPArr[2].Value = p_objDetailArr[iRow].m_intDetailSeqId_int;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 删除住院评分明细项目

        /// <summary>
        /// 删除住院评分明细项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelGradeDetail( int[] intArr)
        {
            if (intArr == null)
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"update t_emr_casegradeddetail
set status_int = 0
where detailseqid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                long lngEff = -1;

                for (int iRow = 0; iRow < intArr.Length; iRow++)
                {
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = intArr[iRow];
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 获得评分明细表的最大序列号,已不用

        /// <summary>
        /// 获得评分明细表的最大序列号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MaxSeqId">返回最大序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxSeqId( out long MaxSeqId)
        {
            MaxSeqId = 0;
            long lngRes = 0;
            try
            { 
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @"a.detailseqid_int
  from t_emr_casegradeddetail a
 order by a.detailseqid_int desc" + clsDatabaseSQLConvert.s_StrRownum;

                DataTable dtResult = null;

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                if (dtResult != null && dtResult.Rows.Count > 0)
                    MaxSeqId = Convert.ToInt64(dtResult.Rows[0][0]);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 获得住院病历评分主表的最大序列号

        /// <summary>
        /// 获得住院病历评分主表的最大序列号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MaxSeqId">返回最大序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxCaseGradeSeqId( out int MaxSeqId)
        {
            MaxSeqId = 0;
            long lngRes = 0;
            try
            { 
                string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @"b.gradeseqid_int
   from t_emr_casegraded b
  order by b.gradeseqid_int" + clsDatabaseSQLConvert.s_StrRownum;

                clsHRPTableService objHRPServ = new clsHRPTableService();

                DataTable dtResult = null;

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                    MaxSeqId = Convert.ToInt32(dtResult.Rows[0][0]);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region 添加住院病历评分记录

        /// <summary>
        /// 添加住院病历评分记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultVO">住院病历评分记录</param>
        /// <param name="p_iStartId">返回此记录的序列号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveCaseGrade( clsGradeResult_VO p_objResultVO, out int p_iStartId)
        {
            p_iStartId = 0;
            if (p_objResultVO == null)
                return -1;

            long lngRes = 0;
            try
            { 
                string strSQL = string.Empty;

                // 第一次评分
                if (p_objResultVO.m_intGradeSeqId_int == 0)
                {
                    strSQL = @"insert into t_emr_casegraded
   (gradeseqid_int,
    registerid_chr,
    lastname_vchr,
    recorderid_chr,
    recorddate_dat,
    status_int,
    sign_int,
    score_int,
    graded_int,
    canreversededuct_int,
    deptid_chr,
    deptname_vch,
    outhospitaldate_dat,
    areaid_chr,
    areaname_vchr,
    changedocid,
    changedocname)
 values
   (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    // 获得住院病历评分主表的序列号
                    clsPublicGradeServ objPubServ = new clsPublicGradeServ();
                    lngRes = objPubServ.m_lngGetSequence( out p_iStartId);


                    clsHRPTableService objHRPServ = new clsHRPTableService();
                    long lngEff = -1;
                    IDataParameter[] objDPArr = null;

                    objHRPServ.CreateDatabaseParameter(17, out objDPArr);

                    objDPArr[0].Value = p_iStartId;
                    objDPArr[1].Value = p_objResultVO.m_strRegisterId_chr;
                    objDPArr[2].Value = p_objResultVO.m_strLastName_vchr;
                    objDPArr[3].Value = p_objResultVO.m_strRecorderId_chr;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = p_objResultVO.m_dtRecordDate_dat;
                    objDPArr[5].Value = 1;
                    objDPArr[6].Value = p_objResultVO.m_intSign_int;
                    objDPArr[7].Value = p_objResultVO.m_floatScore_float;
                    objDPArr[8].Value = p_objResultVO.m_intGraded_int;
                    objDPArr[9].Value = p_objResultVO.m_intCanDeScore_int;
                    objDPArr[10].Value = p_objResultVO.m_strDeptId_chr;
                    objDPArr[11].Value = p_objResultVO.m_strDeptName_vchr;
                    objDPArr[12].DbType = DbType.DateTime;
                    objDPArr[12].Value = p_objResultVO.m_dtOutHospitalDate_dat;
                    objDPArr[13].Value = p_objResultVO.m_strAreaId_chr;
                    objDPArr[14].Value = p_objResultVO.m_strAreaName_vchr;
                    objDPArr[15].Value = p_objResultVO.m_strChangeDocId_chr;
                    objDPArr[16].Value = p_objResultVO.m_strChangeDocName_vchr;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
                else
                {
                    p_iStartId = p_objResultVO.m_intGradeSeqId_int;
                    strSQL = @"update t_emr_casegraded
   set registerid_chr       = ?,
       lastname_vchr        = ?,
       recorderid_chr       = ?,
       recorddate_dat       = ?,
       status_int           = ?,
       sign_int             = ?,
       score_int            = ?,
       graded_int           = ?,
       canreversededuct_int = ?,
       deptid_chr           = ?,
       deptname_vch         = ?,
       outhospitaldate_dat  = ?,
       areaid_chr           = ?,
       areaname_vchr        = ?,
       changedocid          = ?,
       changedocname        = ?
 where gradeseqid_int = ?";

                    clsHRPTableService objHRPServ = new clsHRPTableService();
                    long lngEff = -1;
                    IDataParameter[] objDPArr = null;

                    objHRPServ.CreateDatabaseParameter(17, out objDPArr);

                    objDPArr[0].Value = p_objResultVO.m_strRegisterId_chr;
                    objDPArr[1].Value = p_objResultVO.m_strLastName_vchr;
                    objDPArr[2].Value = p_objResultVO.m_strRecorderId_chr;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_objResultVO.m_dtRecordDate_dat;
                    objDPArr[4].Value = 1;
                    objDPArr[5].Value = p_objResultVO.m_intSign_int;
                    objDPArr[6].Value = p_objResultVO.m_floatScore_float;
                    objDPArr[7].Value = p_objResultVO.m_intGraded_int;
                    objDPArr[8].Value = p_objResultVO.m_intCanDeScore_int;
                    objDPArr[9].Value = p_objResultVO.m_strDeptId_chr;
                    objDPArr[10].Value = p_objResultVO.m_strDeptName_vchr;
                    objDPArr[11].DbType = DbType.DateTime;
                    objDPArr[11].Value = p_objResultVO.m_dtOutHospitalDate_dat;
                    objDPArr[12].Value = p_objResultVO.m_strAreaId_chr;
                    objDPArr[13].Value = p_objResultVO.m_strAreaName_vchr;
                    objDPArr[14].Value = p_objResultVO.m_strChangeDocId_chr;
                    objDPArr[15].Value = p_objResultVO.m_strChangeDocName_vchr;
                    objDPArr[16].Value = p_iStartId;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_objResultVO = null;
            return lngRes;
        }
        #endregion
    }
}
