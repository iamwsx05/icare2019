using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data; 

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 住院病历评分与统计服务类
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInpatientCaseGradeAndStatServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获得要评分的住院病历

        /// <summary>
        /// 获得要评分的住院病历.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQuery_VO">查询条件VO.</param>
        /// <param name="p_objResultArr">返回结果.</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeCase( clsQueryCondition_VO p_objQuery_VO, out clsGradeResult_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            if (p_objQuery_VO == null)
                return -1;

            long lngRes = 0;
            try
            { 
                // 参数数目
                int iParameterCount = 0;

                #region SQL语句
                string strSQL = string.Empty;

                strSQL = @"select a.registerid_chr registerid,
       a.inareadate_dat indata,
       a.areaid_chr     areaid,
       a.deptid_chr     deptid,
       a.casedoctor_chr doctorid,  
       b.lastname_vchr   patientname,
       c.outhospital_dat outdata,    
       d.deptname_vchr areaname,
       e.lastname_vchr doctorname,
       f.deptname_vchr deptname,      
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
   and a.registerid_chr = g.registerid_chr(+)
   and a.registerid_chr = c.registerid_chr(+)
   and (c.pstatus_int = 1 or c.outhospital_dat is null)
   and a.areaid_chr = d.deptid_chr(+)
   and a.deptid_chr = f.deptid_chr(+)
   and a.casedoctor_chr = e.empid_chr(+)";

                
                if (p_objQuery_VO.m_strInPatientId_chr != string.Empty)
                {
                    strSQL += " and h.hisinpatientid_chr = ?";// + p_objQuery_VO.strRegisterId_chr;
                    iParameterCount++;
                }
                if (p_objQuery_VO.m_strLastName_vchr != string.Empty)
                {
                    strSQL += " and b.lastname_vchr = ?";// + p_objQuery_VO.strLastName_vchr + "%";
                    iParameterCount++;
                }
                if (p_objQuery_VO.m_strAreaId_chr != string.Empty)
                {
                    strSQL += " and a.areaid_chr = ?";// + p_objQuery_VO.strAreaId_chr;
                    iParameterCount++;
                }
                if (p_objQuery_VO.m_blnContainTime)
                {
                    strSQL += " and c.outhospital_dat between ? and ?"; //+ new OracleDateTime(p_objQuery_VO.dtOutHospitalDate1_dat).ToString() + " and " + new OracleDateTime(p_objQuery_VO.dtOutHospitalDate2_dat).ToString();
                    iParameterCount += 2;
                }

                strSQL += " order by h.hisinpatientid_chr, a.inareadate_dat";

                #endregion

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DataTable dtResult = null;
                if (iParameterCount > 0)
                {
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(iParameterCount, out objDPArr);
                    iParameterCount = 0;
                    if (p_objQuery_VO.m_strInPatientId_chr != string.Empty)
                    {
                        objDPArr[iParameterCount].Value = p_objQuery_VO.m_strInPatientId_chr;
                        iParameterCount++;
                    }
                    if (p_objQuery_VO.m_strLastName_vchr != string.Empty)
                    {
                        objDPArr[iParameterCount].Value = p_objQuery_VO.m_strLastName_vchr;
                        iParameterCount++;
                    }
                    if (p_objQuery_VO.m_strAreaId_chr != string.Empty)
                    {
                        objDPArr[iParameterCount].Value = p_objQuery_VO.m_strAreaId_chr;
                        iParameterCount++;
                    }
                    if (p_objQuery_VO.m_blnContainTime)
                    {
                        objDPArr[iParameterCount].DbType = DbType.DateTime;
                        objDPArr[iParameterCount].Value = p_objQuery_VO.m_dtOutHospitalDate1_dat;
                        iParameterCount++;
                        objDPArr[iParameterCount].DbType = DbType.DateTime;
                        objDPArr[iParameterCount].Value = p_objQuery_VO.m_dtOutHospitalDate2_dat;
                        iParameterCount++;
                    }

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                }
                else
                {
                    lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                }

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsGradeResult_VO[dtResult.Rows.Count];
                    DataRow dr;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        clsGradeResult_VO objTemp = new clsGradeResult_VO();
                        dr = dtResult.Rows[iRow];

                        objTemp.m_intGradeSeqId_int = dr["gradeseqid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["gradeseqid"]);
                        objTemp.m_strRegisterId_chr = dr["registerid"] != DBNull.Value ? dr["registerid"].ToString() : string.Empty;
                        objTemp.m_strInPatientId_vchr = dr["inpatientid"] != DBNull.Value ? dr["inpatientid"].ToString() : string.Empty;
                        objTemp.m_strLastName_vchr = dr["patientname"] != DBNull.Value ? dr["patientname"].ToString() : string.Empty;
                        objTemp.m_strRecorderId_chr = dr["recorderid"] != DBNull.Value ? dr["recorderid"].ToString() : string.Empty;
                        objTemp.m_dtRecordDate_dat = dr["recorddate"] != DBNull.Value ? Convert.ToDateTime(dr["recorddate"]) : DateTime.MinValue;
                        objTemp.m_intStatus_int = 1;
                        objTemp.m_intSign_int = dr["sign_int"] != DBNull.Value ? Convert.ToInt32(dr["sign_int"]) : 0;
                        objTemp.m_floatScore_float = dr["score"] != DBNull.Value ? Convert.ToSingle(dr["score"]) : 0f;
                        objTemp.m_intGraded_int = dr["graded"] != DBNull.Value ? Convert.ToInt32(dr["graded"]) : 0;
                        objTemp.m_intCanDeScore_int = dr["canreversededuct"] != DBNull.Value ? Convert.ToInt32(dr["canreversededuct"]) : 0;
                        objTemp.m_strDeptId_chr = dr["deptid"] != DBNull.Value ? dr["deptid"].ToString() : string.Empty;
                        objTemp.m_strDeptName_vchr = dr["deptname"] != DBNull.Value ? dr["deptname"].ToString() : string.Empty;
                        objTemp.m_dtInHospitalDate_dat = dr["indata"] != DBNull.Value ? Convert.ToDateTime(dr["indata"]) : DateTime.MinValue;
                        objTemp.m_dtOutHospitalDate_dat = dr["outdata"] != DBNull.Value ? Convert.ToDateTime(dr["outdata"]) : DateTime.MinValue;
                        objTemp.m_strAreaId_chr = dr["areaid"] != DBNull.Value ? dr["areaid"].ToString() : string.Empty;
                        objTemp.m_strAreaName_vchr = dr["areaname"] != DBNull.Value ? dr["areaname"].ToString() : string.Empty;
                        objTemp.m_strChangeDocId_chr = dr["doctorid"] != DBNull.Value ? dr["doctorid"].ToString() : string.Empty;
                        objTemp.m_strChangeDocName_vchr = dr["doctorname"] != DBNull.Value ? dr["doctorname"].ToString() : string.Empty;

                        p_objResultArr[iRow] = objTemp;
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
    }
}
