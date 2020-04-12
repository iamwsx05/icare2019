using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 住院病历质控评分报表服务类.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsSingleCaseReportServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获得已评分的病历.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInHospitalId">住院号</param>
        /// <param name="p_objGradeReport"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCaseGredeReport( string p_strInHospitalId, out clsGradeReport_VO[] p_objGradeReport)
        {
            p_objGradeReport = null;
            if (string.IsNullOrEmpty(p_strInHospitalId))
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"select distinct a.hisinpatientid_chr,
                b.maindiagnosis,
                c.gradeseqid_int,
                c.areaname_vchr,
                c.graded_int,
                c.recorddate_dat, 
                d.lastname_vchr doctorname,
                e.lastname_vchr patientname,
                g.outhospital_dat

  from t_bse_hisemr_relation        a,
       inhospitalmainrecord_content b,
       t_emr_casegraded             c,
       t_bse_employee               d,
       t_opr_bih_registerdetail     e,
       t_opr_bih_leave              g

 where a.emrinpatientid = b.inpatientid(+)
   and a.registerid_chr = c.registerid_chr
   and c.recorderid_chr = d.empid_chr(+)
   and a.registerid_chr = e.registerid_chr
   and a.registerid_chr = g.registerid_chr
   and a.hisinpatientid_chr = ?
 order by c.gradeseqid_int desc";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInHospitalId;
                DataTable p_dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objDPArr);

                if (lngRes > 0 && p_dtResult != null && p_dtResult.Rows.Count > 0)
                {
                    p_objGradeReport = new clsGradeReport_VO[p_dtResult.Rows.Count];
                    DataRow dr;
                    for (int index = 0; index < p_dtResult.Rows.Count; index++)
                    {
                        dr = p_dtResult.Rows[index];
                        clsGradeReport_VO objTemp = new clsGradeReport_VO();

                        objTemp.m_strInPatientId_vchr = dr["hisinpatientid_chr"] != DBNull.Value ? dr["hisinpatientid_chr"].ToString() : string.Empty;
                        objTemp.m_strPatientName_vchr = dr["patientname"] != DBNull.Value ? dr["patientname"].ToString() : string.Empty;
                        objTemp.m_strDeptName_vchr = dr["areaname_vchr"] != DBNull.Value ? dr["areaname_vchr"].ToString() : string.Empty;
                        if (dr["graded_int"] != DBNull.Value)
                        {
                            switch (Convert.ToInt32(dr["graded_int"]))
                            {
                                case 1:
                                    objTemp.m_strCaseClass_int = "甲级";
                                    break;
                                case 2:
                                    objTemp.m_strCaseClass_int = "乙级";
                                    break;
                                case 3:
                                    objTemp.m_strCaseClass_int = "丙级";
                                    break;
                            }
                        }
                        objTemp.m_strMainDiagnose_vchr = dr["maindiagnosis"] != DBNull.Value ? dr["maindiagnosis"].ToString() : string.Empty;
                        objTemp.m_strQualityDoc_vchr = dr["doctorname"] != DBNull.Value ? dr["doctorname"].ToString() : string.Empty;
                        objTemp.m_strQualityDate_Dat = dr["recorddate_dat"] != DBNull.Value ? Convert.ToDateTime(dr["recorddate_dat"]).ToString("yyyy年MM月dd日") : string.Empty;
                        objTemp.m_dtOutHospital_dat = Convert.ToDateTime(dr["outhospital_dat"]);

                        if (dr["gradeseqid_int"] != DBNull.Value && Convert.ToInt32(dr["gradeseqid_int"]) != 0)
                        {
                            objTemp.m_intGradeSeqId_int = Convert.ToInt32(dr["gradeseqid_int"]);
                        }
                        else
                        {
                            objTemp.m_intGradeSeqId_int = 0;
                        }
                        p_objGradeReport[index] = objTemp;
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            } 
            p_strInHospitalId = null;
            return lngRes;
        }

        /// <summary>
        /// 获得扣分项目明细.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_iGradeSeqId"></param>
        /// <param name="p_iItemIdArr"></param>
        /// <param name="p_fGradeArr"></param>
        /// <param name="p_strDetailArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeDetial( int p_iGradeSeqId, int[] p_iItemIdArr, ref float[] p_fGradeArr, out string[] p_strDetailArr)
        {
            p_strDetailArr = null;
            if (p_iGradeSeqId == 0 || p_iItemIdArr == null || p_iItemIdArr.Length <= 0)
                return -1;
            long lngRes = 0;
            try
            { 
                string strSQL = @"select a.deductcause_vchr, a.realdeduct_int, c.deductgrade_int
  from t_emr_casegradeddetail a,
       (select b.itemid_int,
               b.itemdesc_vchar,
               b.deductgrade_int,
               b.deductscore_int
          from t_emr_casegradeitem b
        connect by prior b.itemid_int = b.parentitemid_int
         start with b.itemid_int = ?) c
 where a.itemid_int = c.itemid_int
   and a.status_int = 1
   and a.gradeseqid_int = ?";

                IDataParameter[] objDPArr = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                DataTable dtResult;
                p_strDetailArr = new string[p_iItemIdArr.Length];
                StringBuilder strBuilder = new StringBuilder();

                for (int index = 0; index < p_iItemIdArr.Length; index++)
                {
                    dtResult = null;
                    strBuilder.Remove(0, strBuilder.Length);

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_iItemIdArr[index];
                    objDPArr[1].Value = p_iGradeSeqId;
                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                    if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                    {
                        DataRow[] drArr = dtResult.Select("deductgrade_int = 0");
                        for (int iRow = 0; iRow < drArr.Length; iRow++)
                        {
                            strBuilder.Append(Convert.ToString(iRow + 1)).Append("、").Append(drArr[iRow]["deductcause_vchr"].ToString()).Append("(").Append(Convert.ToString(drArr[iRow]["realdeduct_int"])).Append("分)；");
                            if (p_fGradeArr[index] > 0f)
                            {
                                p_fGradeArr[index] = p_fGradeArr[index] - Convert.ToSingle(drArr[iRow]["realdeduct_int"]);
                                if (Convert.ToSingle(p_fGradeArr[index]) < 0f)
                                    p_fGradeArr[index] = 0f;
                            }
                        }

                        drArr = dtResult.Select("deductgrade_int <> 0");
                        for (int iRow = 0; iRow < drArr.Length; iRow++)
                        {
                            switch (Convert.ToInt32(drArr[iRow]["deductgrade_int"]))
                            {
                                case 1:
                                    strBuilder.Append(Convert.ToString(iRow + 1)).Append("、").Append(drArr[iRow]["deductcause_vchr"].ToString()).Append("(乙级)；");
                                    break;
                                case 2:
                                    strBuilder.Append(Convert.ToString(iRow + 1)).Append("、").Append(drArr[iRow]["deductcause_vchr"].ToString()).Append("(丙级)；");
                                    break;
                            }
                        }
                        p_strDetailArr[index] = strBuilder.ToString();
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            p_iGradeSeqId = 0; 
            p_iItemIdArr = null;
            return lngRes;
        }

        /// <summary>
        /// 获取扣分项目目录.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objGradeDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGradeItemCatalog( out clsDeductDetail[] p_objGradeDetail)
        {
            p_objGradeDetail = null;
            long lngRes = 0;
            try
            { 
                string strSQL = @" select b.itemid_int, b.itemdesc_vchar, b.deductscore_int
   from t_emr_casegradeitem b
  where b.parentitemid_int = 0
    and b.status_int = 1
  order by b.itemid_int";

                DataTable dtResult = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtResult);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objGradeDetail = new clsDeductDetail[dtResult.Rows.Count];
                    DataRow dr;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        dr = dtResult.Rows[iRow];
                        clsDeductDetail objTemp = new clsDeductDetail();
                        objTemp.m_intItemId_int = Convert.ToInt32(dr["itemid_int"]);
                        objTemp.m_strItem_vchr = dr["itemdesc_vchar"].ToString();
                        objTemp.m_floatStandardMark_float = Convert.ToSingle(dr["deductscore_int"]);
                        p_objGradeDetail[iRow] = objTemp;
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