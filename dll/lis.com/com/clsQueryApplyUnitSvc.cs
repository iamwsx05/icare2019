using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQueryApplyUnitSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        public clsQueryApplyUnitSvc()
        {

        }

        #region ����һ�����뵥ԪID�õ���������������ID�б�
        [AutoComplete]
        public long m_lngGetSampleTypeIDList(
            string[] p_strApplyUnitIDArr,
            out string[] p_strSampleTypeIDArr)
        {
            long lngRes = 0;
            p_strSampleTypeIDArr = null; 

            string strSQL = @"SELECT DISTINCT sample_type_id_chr FROM t_aid_lis_apply_unit 							     
						  	   WHERE apply_unit_id_chr in(#)";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (p_strApplyUnitIDArr != null)
            {
                for (int i = 0; i < p_strApplyUnitIDArr.Length; i++)
                {
                    sb.Append("?,");
                }
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
            }
            string strRep = sb.ToString();
            if (strRep.Trim() == "")
                return 1;
            strSQL = strSQL.Replace("#", strRep);
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                DataTable dtbResult = null;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(p_strApplyUnitIDArr.Length, out objDPArr);
                for (int i = 0; i < p_strApplyUnitIDArr.Length; i++)
                {
                    objDPArr[i].Value = p_strApplyUnitIDArr[i];
                }

                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count != 0)
                {
                    ArrayList arlTemp = new ArrayList();
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string strid = dr["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
                        if (strid != "")
                        {
                            arlTemp.Add(strid);
                        }
                    }
                    p_strSampleTypeIDArr = (string[])arlTemp.ToArray(typeof(string));
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݼ�������ȡ���뵥Ԫ
        [AutoComplete]
        public long m_lngGetApplUnitByCheckCategory( string p_strCheckCategory,
            out clsApplUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"select apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr, py_code_chr,
                                   assist_code01_chr, wb_code_chr, assist_code02_chr,
                                   check_category_id_chr, is_no_food_required_chr,
                                   is_physical_exam_required_chr, is_reservation_required_chr, price_num,
                                   cost_num, sample_type_id_chr, summary_vchr, outer_check_flag_num, reporthour, SamplingInstr, jclx_jj, jclx_jtj   
                              from t_aid_lis_apply_unit
                              where CHECK_CATEGORY_ID_CHR = '" + p_strCheckCategory + @"' ORDER BY apply_unit_id_chr";
            DataTable dtbApplUnit = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbApplUnit);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbApplUnit != null)
                {
                    if (dtbApplUnit.Rows.Count > 0)
                    {
                        p_objResultArr = new clsApplUnit_VO[dtbApplUnit.Rows.Count];
                        for (int i = 0; i < dtbApplUnit.Rows.Count; i++)
                        {
                            p_objResultArr[i] = new clsApplUnit_VO();
                            ConstructApplUnitVO(dtbApplUnit.Rows[i], ref p_objResultArr[i]);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������뵥ԪID�õ����뵥ԪVO
        [AutoComplete]
        public long m_lngGetApplyUnitVOByApplyUnitID(
            string p_strApplyUnitID,
            out clsApplUnit_VO p_objApplUnit)
        {
            long lngRes = 0;
            p_objApplUnit = null; 

            string strSQL = @"select apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr, py_code_chr,
                                   assist_code01_chr, wb_code_chr, assist_code02_chr,
                                   check_category_id_chr, is_no_food_required_chr,
                                   is_physical_exam_required_chr, is_reservation_required_chr, price_num,
                                   cost_num, sample_type_id_chr, summary_vchr, outer_check_flag_num, reporthour, SamplingInstr, jclx_jj, jclx_jtj   
                              from t_aid_lis_apply_unit
                             where apply_unit_id_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                DataTable dtbResult = null;
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strApplyUnitID;

                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count != 0)
                {
                    p_objApplUnit = new clsApplUnit_VO();
                    ConstructApplUnitVO(dtbResult.Rows[0], ref p_objApplUnit);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���е����뵥Ԫ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objApplUnitVOList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllApplUnit( out clsApplUnit_VO[] objApplUnitVOList)
        {
            long lngRes = 0;
            objApplUnitVOList = null; 

            string strSQL = @"select apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr, py_code_chr,
                                   assist_code01_chr, wb_code_chr, assist_code02_chr,
                                   check_category_id_chr, is_no_food_required_chr,
                                   is_physical_exam_required_chr, is_reservation_required_chr, price_num,
                                   cost_num, sample_type_id_chr, summary_vchr, outer_check_flag_num, reporthour, SamplingInstr, jclx_jj, jclx_jtj   
                              from t_aid_lis_apply_unit";

            DataTable dtbApplUnit = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbApplUnit);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbApplUnit != null)
                {
                    if (dtbApplUnit.Rows.Count > 0)
                    {
                        objApplUnitVOList = new clsApplUnit_VO[dtbApplUnit.Rows.Count];
                        for (int i = 0; i < dtbApplUnit.Rows.Count; i++)
                        {
                            objApplUnitVOList[i] = new clsApplUnit_VO();
                            ConstructApplUnitVO(dtbApplUnit.Rows[i], ref objApplUnitVOList[i]);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���е����뵥Ԫ��ϸ
        [AutoComplete]
        public long m_lngGetAllApplUnitDetail( out clsApplUnitDetail_VO[] objApplUnitDetailVOList)
        {
            long lngRes = 0;
            objApplUnitDetailVOList = null; 

            string strSQL = @"select check_item_id_chr, apply_unit_id_chr, print_seq_int
                                from t_aid_lis_apply_unit_detail";
            DataTable dtbApplUnitDetail = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbApplUnitDetail);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbApplUnitDetail != null)
                {
                    if (dtbApplUnitDetail.Rows.Count > 0)
                    {
                        objApplUnitDetailVOList = new clsApplUnitDetail_VO[dtbApplUnitDetail.Rows.Count];
                        for (int i = 0; i < dtbApplUnitDetail.Rows.Count; i++)
                        {
                            objApplUnitDetailVOList[i] = new clsApplUnitDetail_VO();
                            ConstructApplUnitDetailVO(dtbApplUnitDetail.Rows[i], ref objApplUnitDetailVOList[i]);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����claApplUnitDetail_VO
        [AutoComplete]
        private void ConstructApplUnitDetailVO(System.Data.DataRow objRow, ref clsApplUnitDetail_VO objApplUnitDetailVOList)
        {
            objApplUnitDetailVOList.strApplUnitID = objRow["APPLY_UNIT_ID_CHR"].ToString().Trim();
            objApplUnitDetailVOList.strCheckItemID = objRow["CHECK_ITEM_ID_CHR"].ToString().Trim();
        }
        #endregion

        #region ����clsApplUnitVO
        [AutoComplete]
        private void ConstructApplUnitVO(System.Data.DataRow objRow, ref clsApplUnit_VO objApplUnitVO)
        {
            objApplUnitVO.strApplUnitID = objRow["APPLY_UNIT_ID_CHR"].ToString().Trim();
            objApplUnitVO.strApplUnitName = objRow["APPLY_UNIT_NAME_VCHR"].ToString().Trim();
            objApplUnitVO.strOtherName = objRow["OTHER_NAME_VCHR"].ToString().Trim();
            objApplUnitVO.strPYCode = objRow["PY_CODE_CHR"].ToString().Trim();
            objApplUnitVO.strWBCode = objRow["WB_CODE_CHR"].ToString().Trim();
            objApplUnitVO.strAssistCode01 = objRow["ASSIST_CODE01_CHR"].ToString().Trim();
            objApplUnitVO.strAssistCode02 = objRow["ASSIST_CODE02_CHR"].ToString().Trim();
            objApplUnitVO.strCheckCategoryID = objRow["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
            objApplUnitVO.strIsNoFoodRequired = objRow["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim();
            objApplUnitVO.strIsPhysicsExamRequired = objRow["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim();
            objApplUnitVO.strIsReservationRequired = objRow["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim();
            objApplUnitVO.strPrice = objRow["PRICE_NUM"].ToString().Trim();
            objApplUnitVO.strCost = objRow["COST_NUM"].ToString().Trim();
            objApplUnitVO.strSampleTypeID = objRow["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
            objApplUnitVO.strSummary = objRow["SUMMARY_VCHR"].ToString().Trim();
            objApplUnitVO.strOutCheckFlag = objRow["OUTER_CHECK_FLAG_NUM"].ToString().Trim();
            objApplUnitVO.ReportHour = objRow["REPORTHOUR"] == DBNull.Value ? 0 : Convert.ToDecimal(objRow["REPORTHOUR"].ToString());
            objApplUnitVO.SamplingInstr = objRow["SamplingInstr"].ToString();
            objApplUnitVO.Jclx_jj = objRow["Jclx_jj"] == DBNull.Value ? 0 : Convert.ToInt32(objRow["Jclx_jj"].ToString());
            objApplUnitVO.Jclx_jtj = objRow["Jclx_jtj"] == DBNull.Value ? 0 : Convert.ToInt32(objRow["Jclx_jtj"].ToString());
        }
        #endregion

        #region ��ȡ���뵥Ԫ����
        /// <summary>
        /// ��ȡ���뵥Ԫ����
        /// </summary>
        /// <param name="p_strAppUnitArr"></param>
        /// <param name="p_strOutAppUnitArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryAppUnitSeq(string[] p_strAppUnitArr, out string[] p_strOutAppUnitArr)
        {
            long lngRes = 0;
            p_strOutAppUnitArr = null;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"select b.apply_unit_id_chr, a.print_seq_int
  from t_aid_lis_report_group_detail a, t_aid_lis_sample_group_unit b
 where a.sample_group_id_chr = b.sample_group_id_chr
   and b.apply_unit_id_chr in (?
";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(p_strAppUnitArr.Length, out objDPArr);
                objDPArr[0].Value = p_strAppUnitArr[0];
                for (int i = 1; i < p_strAppUnitArr.Length; i++)
                {
                    objDPArr[i].Value = p_strAppUnitArr[i];
                    strSQL += ",?";
                }
                strSQL += ")";
                DataTable dtResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    DataView dtView = dtResult.DefaultView;
                    dtView.Sort = "print_seq_int asc";
                    p_strOutAppUnitArr = new string[dtView.Count];
                    for (int i = 0; i < dtView.Count; i++)
                    {
                        p_strOutAppUnitArr[i] = dtView[i]["apply_unit_id_chr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, false);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = null;
            }
            return lngRes;

        }
        #endregion
    }
}
