using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 评分查询报表服务类.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsQueryDeduceCauseServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 扣分原因查询.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_Query">查询条件.</param>
        /// <param name="p_objQueryCauseArr">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryDeduceCause( clsQueryResultCondition_VO p_Query, out clsQueryDeduceCause_VO[] p_objQueryCauseArr)
        {
            p_objQueryCauseArr = null;
            if (p_Query == null)
                return -1;
            if (p_Query.m_intReportType != 1)
                return -1;

            long lngRes = 0;
            try
            { 
                int ParameterCount = 2;
                #region SQL语句.
                string strSQL = @"select a.hisinpatientid_chr,
       b.lastname_vchr,
       b.areaname_vchr,
       b.graded_int,
       b.changedocname,
       b.score_int,
       c.deductcause_vchr,
       c.realdeduct_int,
       d.deductgrade_int
  from t_bse_hisemr_relation  a,
       t_emr_casegraded       b,
       t_emr_casegradeddetail c,
       t_emr_casegradeitem    d
 where a.registerid_chr = b.registerid_chr
   and b.gradeseqid_int = c.gradeseqid_int
   and c.itemid_int = d.itemid_int
   and b.outhospitaldate_dat between ? and ?";

                if (p_Query.m_strDeptId != string.Empty)
                {
                    strSQL += " and b.areaid_chr = ?";
                    ParameterCount++;
                }
                if (p_Query.m_intGraded != 0)
                {
                    strSQL += " and b.graded_int = ?";
                    ParameterCount++;
                }

                strSQL += " order by a.hisinpatientid_chr";

                #endregion

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(ParameterCount, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_Query.m_dtOutHospital_dat1.Date;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_Query.m_dtOutHospital_dat2.Date.AddHours(24);
                if (ParameterCount == 4)
                {
                    objDPArr[2].Value = p_Query.m_strDeptId;
                    objDPArr[3].Value = p_Query.m_intGraded;
                }
                if (ParameterCount == 3)
                {
                    if (p_Query.m_strDeptId == string.Empty)
                    {
                        objDPArr[2].Value = p_Query.m_intGraded;
                    }
                    else
                    {
                        objDPArr[2].Value = p_Query.m_strDeptId;
                    }
                }

                DataTable dtlResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtlResult, objDPArr);

                #region 如果dtlResult有记录,将其转为VO.
                if (lngRes > 0 && dtlResult != null && dtlResult.Rows.Count > 0)
                {
                    DataTable dtlHisInpatient = null;
                    string[] columnNames = new string[1];
                    columnNames[0] = "hisinpatientid_chr";
                    dtlHisInpatient = dtlResult.DefaultView.ToTable(true, columnNames);


                    if (dtlHisInpatient.Rows.Count > 0)
                    {
                        p_objQueryCauseArr = new clsQueryDeduceCause_VO[dtlHisInpatient.Rows.Count];

                        for (int iRow = 0; iRow < dtlHisInpatient.Rows.Count; iRow++)
                        {
                            string strHisInpatient = dtlHisInpatient.Rows[iRow][0].ToString();
                            DataRow[] drCauseArr = dtlResult.Select("hisinpatientid_chr = '" + strHisInpatient + "'");
                            if (drCauseArr != null && drCauseArr.Length > 0)
                            {
                                clsQueryDeduceCause_VO objTemp = new clsQueryDeduceCause_VO();
                                DataRow dr = drCauseArr[0];
                                objTemp.m_strInPatientId_vchr = dr["hisinpatientid_chr"] != DBNull.Value ? dr["hisinpatientid_chr"].ToString() : string.Empty;
                                objTemp.m_strPatientName_vchr = dr["lastname_vchr"] != DBNull.Value ? dr["lastname_vchr"].ToString() : string.Empty;
                                objTemp.m_strDeptName_vchr = dr["areaname_vchr"] != DBNull.Value ? dr["areaname_vchr"].ToString() : string.Empty;
                                if (dr["graded_int"] != DBNull.Value)
                                {
                                    switch (Convert.ToInt32(dr["graded_int"]))
                                    {
                                        case 1:
                                            objTemp.m_strGrade_int = "甲级";
                                            break;
                                        case 2:
                                            objTemp.m_strGrade_int = "乙级";
                                            break;
                                        case 3:
                                            objTemp.m_strGrade_int = "丙级";
                                            break;
                                    }
                                }
                                objTemp.m_strChangeDocName_vchr = dr["changedocname"] != DBNull.Value ? dr["changedocname"].ToString() : string.Empty;

                                int index = 0;
                                StringBuilder strBuilder = new StringBuilder();
                                for (index = 0; index < drCauseArr.Length; index++)
                                {
                                    if (drCauseArr[index]["deductcause_vchr"] != DBNull.Value)
                                    {
                                        strBuilder.Append((index + 1).ToString() + "、" + drCauseArr[index]["deductcause_vchr"].ToString());

                                        switch (Convert.ToInt32(drCauseArr[index]["deductgrade_int"]))
                                        {
                                            case 1:
                                                strBuilder.Append("(乙级); ");
                                                break;
                                            case 2:
                                                strBuilder.Append("(丙级); ");
                                                break;
                                            default:
                                                strBuilder.Append("(" + drCauseArr[index]["realdeduct_int"].ToString() + "分); ");
                                                break;
                                        }
                                    }
                                }
                                strBuilder.Append((index + 1).ToString() + "、" + "最终分数(" + drCauseArr[0]["score_int"].ToString() + "分); ");
                                objTemp.m_strDeductCause_vchr = strBuilder.ToString();
                                p_objQueryCauseArr[iRow] = objTemp;
                            }
                        }
                    }
                }
                #endregion

                #region 如果dtlResult有记录,将其转为VO, 实现效率不高且复杂.已不用.
                //if (lngRes > 0 && dtlResult != null && dtlResult.Rows.Count > 0)
                //{
                //    p_objQueryCauseArr = new clsQueryDeduceCause_VO[dtlResult.Rows.Count];
                //    DataRow dr = null;
                //    clsQueryDeduceCause_VO objTemp = null;
                //    int index = 0;
                //    int ideduceCount = 1;
                //    string strHisInpatientId = string.Empty;
                //    StringBuilder strBuilder = new StringBuilder();
                //    dtlResult.DefaultView.ToTable(
                //    for (int iRow = 0; iRow < dtlResult.Rows.Count; iRow++)
                //    {
                //        dr = dtlResult.Rows[iRow];
                //        if (strHisInpatientId != dr["hisinpatientid_chr"].ToString())
                //        {
                //            if (objTemp != null && strBuilder != null)
                //            {
                //                strBuilder.Append(ideduceCount.ToString() + "、" + "最终分数(" + dr["score_int"].ToString() + "); ");
                //                objTemp.strDeductCause_vchr = strBuilder.ToString();
                //                p_objQueryCauseArr[index++] = objTemp;
                //            }

                //            ideduceCount = 1;
                //            strBuilder.Remove(0, strBuilder.Length);
                //            objTemp = new clsQueryDeduceCause_VO();
                //            objTemp.strInPatientId_vchr = dr["hisinpatientid_chr"] != DBNull.Value ? dr["hisinpatientid_chr"].ToString() : string.Empty;
                //            objTemp.strPatientName_vchr = dr["lastname_vchr"] != DBNull.Value ? dr["lastname_vchr"].ToString() : string.Empty;
                //            objTemp.strDeptName_vchr = dr["deptname_vch"] != DBNull.Value ? dr["deptname_vch"].ToString() : string.Empty;

                //            switch (Convert.ToInt32(dr["graded_int"]))
                //            {
                //                case 1:
                //                    objTemp.strGrade_int = "甲级";
                //                    break;
                //                case 2:
                //                    objTemp.strGrade_int = "乙级";
                //                    break;
                //                case 3:
                //                    objTemp.strGrade_int = "丙级";
                //                    break;
                //            }

                //            objTemp.strChangeDocName_vchr = dr["changedocname"] != DBNull.Value ? dr["changedocname"].ToString() : string.Empty;
                //            if (dr["deductcause_vchr"] != DBNull.Value)
                //            {
                //                strBuilder.Append(ideduceCount++.ToString() + "、" + dr["deductcause_vchr"].ToString());

                //                switch (Convert.ToInt32(dr["deductgrade_int"]))
                //                {
                //                    case 1:
                //                        strBuilder.Append("(乙级); ");
                //                        break;
                //                    case 2:
                //                        strBuilder.Append("(丙级); ");
                //                        break;
                //                    default:
                //                        strBuilder.Append("(" + dr["realdeduct_int"].ToString() + "); ");
                //                        break;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (dr["deductcause_vchr"] != DBNull.Value)
                //            {
                //                strBuilder.Append(ideduceCount++.ToString() + "、" + dr["deductcause_vchr"].ToString());

                //                switch (Convert.ToInt32(dr["deductgrade_int"]))
                //                {
                //                    case 1:
                //                        strBuilder.Append("(乙级); ");
                //                        break;
                //                    case 2:
                //                        strBuilder.Append("(丙级); ");
                //                        break;
                //                    default:
                //                        strBuilder.Append("(" + dr["realdeduct_int"].ToString() + "); ");
                //                        break;
                //                }
                //            }
                //        }

                //    }
                //    strBuilder.Append(ideduceCount.ToString() + "、" + "最终分数(" + dr["score_int"].ToString() + "); ");
                //    objTemp.strDeductCause_vchr = strBuilder.ToString();
                //    p_objQueryCauseArr[index] = objTemp;
                //}
                #endregion
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// 评分结果统计.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_Query">查询条件.</param>
        /// <param name="p_objQueryStatResultArr">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryStatResult( clsQueryResultCondition_VO p_Query, out clsQueryStatResult_VO[] p_objQueryStatResultArr)
        {
            p_objQueryStatResultArr = null;

            if (p_Query.m_intReportType != 2)
                return -1;

            long lngRes = 0;
            try
            { 
                string strSQL = @"select a.hisinpatientid_chr,
       b.lastname_vchr,
       b.graded_int,
       b.score_int,
       b.areaname_vchr,
       b.changedocname
  from t_bse_hisemr_relation a, t_emr_casegraded b
 where a.registerid_chr = b.registerid_chr
   and b.outhospitaldate_dat between ? and ?";

                int iParameterCount = 2;
                if (p_Query.m_strDeptId != string.Empty)
                {
                    strSQL += " and b.areaid_chr = ?";
                    iParameterCount++;
                }
                if (p_Query.m_intGraded != 0)
                {
                    strSQL += " and b.graded_int = ?";
                    iParameterCount++;
                }
                strSQL += " order by b.deptid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(iParameterCount, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_Query.m_dtOutHospital_dat1.Date;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_Query.m_dtOutHospital_dat2.Date.AddHours(24);

                iParameterCount = 2;
                if (p_Query.m_strDeptId != string.Empty)
                {
                    objDPArr[iParameterCount].Value = p_Query.m_strDeptId;
                    iParameterCount++;
                }
                if (p_Query.m_intGraded != 0)
                {
                    objDPArr[iParameterCount].Value = p_Query.m_intGraded;
                    iParameterCount++;
                }

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objQueryStatResultArr = new clsQueryStatResult_VO[dtResult.Rows.Count];
                    DataRow dr = null;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        dr = dtResult.Rows[iRow];
                        clsQueryStatResult_VO objTemp = new clsQueryStatResult_VO();
                        objTemp.m_strInPatientId_vchr = dr["hisinpatientid_chr"] != DBNull.Value ? dr["hisinpatientid_chr"].ToString() : string.Empty;
                        objTemp.m_strPatientName_vchr = dr["lastname_vchr"] != DBNull.Value ? dr["lastname_vchr"].ToString() : string.Empty;
                        if (dr["graded_int"] != DBNull.Value)
                        {
                            switch (Convert.ToInt32(dr["graded_int"]))
                            {
                                case 1:
                                    objTemp.m_strGrade_int = "甲级";
                                    break;
                                case 2:
                                    objTemp.m_strGrade_int = "乙级";
                                    break;
                                case 3:
                                    objTemp.m_strGrade_int = "丙级";
                                    break;
                            }
                        }
                        objTemp.m_floatRealScored = dr["score_int"] != DBNull.Value ? Convert.ToSingle(dr["score_int"]) : 0f;
                        objTemp.m_strDeptName_vchr = dr["areaname_vchr"] != DBNull.Value ? dr["areaname_vchr"].ToString() : string.Empty;
                        objTemp.m_strChangeDocName_vchr = dr["changedocname"] != DBNull.Value ? dr["changedocname"].ToString() : string.Empty;

                        p_objQueryStatResultArr[iRow] = objTemp;
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
    }
}
