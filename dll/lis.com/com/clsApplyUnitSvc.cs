using System;
using System.Data;
using System.Collections;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// clsApplyUnitSvc ��ժҪ˵����
    /// 
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsApplyUnitSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        #region ���캯��
        public clsApplyUnitSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //        #region ����һ�����뵥ԪID�õ���������������ID�б�  
        //        [AutoComplete]
        //        public long m_lngGetSampleTypeIDList(
        //            string[] p_strApplyUnitIDArr,
        //            out string[] p_strSampleTypeIDArr)
        //        {
        //            long lngRes = 0;
        //            p_strSampleTypeIDArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsAppGroupSvc","m_lngGetSampleTypeIDList");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"SELECT DISTINCT sample_type_id_chr FROM t_aid_lis_apply_unit 							     
        //						  	   WHERE apply_unit_id_chr in(#)";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            if(p_strApplyUnitIDArr != null)
        //            {
        //                for(int i=0;i<p_strApplyUnitIDArr.Length;i++)
        //                {
        //                    sb.Append("?,");
        //                }
        //                if(sb.Length >0)
        //                    sb.Remove(sb.Length-1,1);
        //            }
        //            string strRep = sb.ToString();
        //            if(strRep.Trim() == "")
        //                return 1;
        //            strSQL = strSQL.Replace("#",strRep);
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //                DataTable dtbResult = null;
        //                IDataParameter[] objDPArr = null;
        //                objHRPSvc.CreateDatabaseParameter(p_strApplyUnitIDArr.Length,out objDPArr);
        //                for(int i=0;i<p_strApplyUnitIDArr.Length;i++)
        //                {
        //                    objDPArr[i].Value = p_strApplyUnitIDArr[i];
        //                }

        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbResult,objDPArr);
        //                objHRPSvc.Dispose();
        //                if(lngRes >0 && dtbResult != null && dtbResult.Rows.Count != 0)
        //                {
        //                    ArrayList arlTemp = new ArrayList();
        //                    foreach(DataRow dr in dtbResult.Rows)
        //                    {
        //                        string strid = dr["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
        //                        if(strid != "")
        //                        {
        //                            arlTemp.Add(strid);
        //                        }
        //                    }
        //                    p_strSampleTypeIDArr = (string[])arlTemp.ToArray(typeof(string));
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion


        #region �������뵥Ԫ
        [AutoComplete]
        public long m_lngSetApplyUnit(clsApplUnit_VO p_objApplyUnit, System.Collections.Generic.List<clsApplUnitDetail_VO> p_arlAddDetail, System.Collections.Generic.List<clsApplUnitDetail_VO> p_arlRemoveDetail)
        {
            long lngRes = 0;

            //1.�������뵥Ԫ�Ļ�������
            lngRes = m_lngSetApplUnit(ref p_objApplyUnit);
            if (lngRes > 0)
            {
                if (p_arlRemoveDetail != null && p_arlRemoveDetail.Count > 0)
                {
                    //2.ɾ�����뵥Ԫ��ϸ�еļ�����Ŀ
                    for (int i = 0; i < p_arlRemoveDetail.Count; i++)
                    {
                        lngRes = m_lngDelApplyUnitDetailByApplyIDAndCheckItemID(p_arlRemoveDetail[i]);
                    }
                }
                if (p_arlAddDetail != null && p_arlAddDetail.Count > 0)
                {
                    //3.�������뵥Ԫ��ϸ�еļ�����Ŀ
                    for (int i = 0; i < p_arlAddDetail.Count; i++)
                    {
                        clsApplUnitDetail_VO objDetail = p_arlAddDetail[i];
                        lngRes = m_lngAddApplUnitDetail(ref objDetail);
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region �������뵥ԪID�ͼ�����ĿIDɾ�����뵥Ԫ��ϸ��Ϣ
        [AutoComplete]
        public long m_lngDelApplyUnitDetailByApplyIDAndCheckItemID(
            clsApplUnitDetail_VO p_objRecord)
        {
            long lngRes = 0;

            string strSQL = @"DELETE FROM t_aid_lis_apply_unit_detail
							   WHERE check_item_id_chr = '" + p_objRecord.strCheckItemID + @"'
								 AND apply_unit_id_chr = '" + p_objRecord.strApplUnitID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRecEff = -1;
                lngRes = objHRPSvc.DoExcuteForDelete(strSQL, ref lngRecEff);
                if (lngRecEff > -1)
                {
                    lngRes = 1;
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //        #region ���ݼ�������ȡ���뵥Ԫ  
        //        [AutoComplete]
        //        public long m_lngGetApplUnitByCheckCategory(string p_strCheckCategory,
        //            out clsApplUnit_VO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsApplyUnitSvc","m_lngGetAllApplUnit");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr, py_code_chr,
        //                                   assist_code01_chr, wb_code_chr, assist_code02_chr,
        //                                   check_category_id_chr, is_no_food_required_chr,
        //                                   is_physical_exam_required_chr, is_reservation_required_chr, price_num,
        //                                   cost_num, sample_type_id_chr, summary_vchr, outer_check_flag_num
        //                              from t_aid_lis_apply_unit
        //                              where CHECK_CATEGORY_ID_CHR = '" + p_strCheckCategory + @"' ORDER BY apply_unit_id_chr";
        //            DataTable dtbApplUnit = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbApplUnit);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbApplUnit != null)
        //                {
        //                    if(dtbApplUnit.Rows.Count > 0)
        //                    {
        //                        p_objResultArr = new clsApplUnit_VO[dtbApplUnit.Rows.Count];
        //                        for(int i=0;i<dtbApplUnit.Rows.Count;i++)
        //                        {
        //                            p_objResultArr[i] = new clsApplUnit_VO();
        //                            ConstructApplUnitVO(dtbApplUnit.Rows[i],ref p_objResultArr[i]);
        //                        }
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region �������뵥ԪID�õ����뵥ԪVO 
        //        [AutoComplete]
        //        public long m_lngGetApplyUnitVOByApplyUnitID(
        //            string p_strApplyUnitID,
        //            out clsApplUnit_VO p_objApplUnit)
        //        {
        //            long lngRes = 0;
        //            p_objApplUnit = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsAppGroupSvc","m_lngGetApplyUnitVOByApplyUnitID");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr, py_code_chr,
        //                                   assist_code01_chr, wb_code_chr, assist_code02_chr,
        //                                   check_category_id_chr, is_no_food_required_chr,
        //                                   is_physical_exam_required_chr, is_reservation_required_chr, price_num,
        //                                   cost_num, sample_type_id_chr, summary_vchr, outer_check_flag_num
        //                              from t_aid_lis_apply_unit
        //                             where apply_unit_id_chr = ?";

        //            try
        //            {
        //                clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                DataTable dtbResult = null;
        //                IDataParameter[] objDPArr = null;
        //                objHRPSvc.CreateDatabaseParameter(1,out objDPArr);
        //                objDPArr[0].Value = p_strApplyUnitID;

        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbResult,objDPArr);
        //                objHRPSvc.Dispose();
        //                if(lngRes >0 && dtbResult != null && dtbResult.Rows.Count != 0)
        //                {
        //                    p_objApplUnit = new clsApplUnit_VO();
        //                    ConstructApplUnitVO(dtbResult.Rows[0],ref p_objApplUnit);
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region �������뵥Ԫ�Ļ�������
        [AutoComplete]
        public long m_lngSetApplUnit(ref clsApplUnit_VO objApplUnit)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_aid_lis_apply_unit
							     SET apply_unit_name_vchr = ?,
									 other_name_vchr = ?,
									 py_code_chr = ?,
									 assist_code01_chr = ?,
									 wb_code_chr = ?,
									 assist_code02_chr = ?,
									 check_category_id_chr = ?,
									 is_no_food_required_chr = ?,
									 is_physical_exam_required_chr = ?,
									 is_reservation_required_chr = ?,
									 PRICE_NUM = ?,
									 COST_NUM = ?,
									 SAMPLE_TYPE_ID_CHR = ?,
									 SUMMARY_VCHR = ?,
									 OUTER_CHECK_FLAG_NUM = ?,
                                     REPORTHOUR = ?,
                                     SamplingInstr = ? 
						  	   WHERE apply_unit_id_chr = ? ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objApplUnitArr = null;
                objHRPSvc.CreateDatabaseParameter(18, out objApplUnitArr);

                objApplUnitArr[0].Value = objApplUnit.strApplUnitName;
                objApplUnitArr[1].Value = objApplUnit.strOtherName;
                objApplUnitArr[2].Value = objApplUnit.strPYCode;
                objApplUnitArr[3].Value = objApplUnit.strAssistCode01;
                objApplUnitArr[4].Value = objApplUnit.strWBCode;
                objApplUnitArr[5].Value = objApplUnit.strAssistCode02;
                objApplUnitArr[6].Value = objApplUnit.strCheckCategoryID;
                objApplUnitArr[7].Value = objApplUnit.strIsNoFoodRequired;
                objApplUnitArr[8].Value = objApplUnit.strIsPhysicsExamRequired;
                objApplUnitArr[9].Value = objApplUnit.strIsReservationRequired;
                objApplUnitArr[10].Value = objApplUnit.strPrice;
                objApplUnitArr[11].Value = objApplUnit.strCost;
                objApplUnitArr[12].Value = objApplUnit.strSampleTypeID;
                objApplUnitArr[13].Value = objApplUnit.strSummary;
                objApplUnitArr[14].Value = objApplUnit.strOutCheckFlag;
                objApplUnitArr[15].Value = objApplUnit.ReportHour;
                objApplUnitArr[16].Value = objApplUnit.SamplingInstr;
                objApplUnitArr[17].Value = objApplUnit.strApplUnitID;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objApplUnitArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ�����뵥Ԫ������ϸ
        [AutoComplete]
        public long m_lngDelApplUnitAndDetail(string strApplUnitID)
        {
            long lngRes = 0;

            lngRes = m_lngDelApplUnitDetail(strApplUnitID);

            if (lngRes > 0)
            {
                lngRes = m_lngDelApplUnit(strApplUnitID);
            }
            return lngRes;
        }
        #endregion

        #region ɾ�����뵥Ԫ��ϸ
        [AutoComplete]
        public long m_lngDelApplUnitDetail(string strApplUnitID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE FROM t_aid_lis_apply_unit_detail WHERE apply_unit_id_chr = '" + strApplUnitID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRecEff = -1;
                lngRes = objHRPSvc.DoExcuteForDelete(strSQL, ref lngRecEff);
                if (lngRecEff > -1)
                {
                    lngRes = 1;
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ�����뵥Ԫ
        [AutoComplete]
        public long m_lngDelApplUnit(string strApplUnitID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE FROM t_aid_lis_apply_unit WHERE apply_unit_id_chr = '" + strApplUnitID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRecEff = -1;
                lngRes = objHRPSvc.DoExcuteForDelete(strSQL, ref lngRecEff);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������뵥Ԫ������ϸ
        [AutoComplete]
        public long m_lngAddApplUnitAndDetail(ref clsApplUnit_VO objApplUnitVO, ref clsApplUnitDetail_VO[] objApplUnitDetailVOList)
        {
            long lngRes = 0;

            if (objApplUnitVO.strApplUnitID == null)
            {
                lngRes = m_lngAddApplUnit(ref objApplUnitVO);
            }
            else
            {
                lngRes = m_lngSetApplUnit(ref objApplUnitVO);
            }
            if (lngRes > 0)
            {
                for (int i = 0; i < objApplUnitDetailVOList.Length; i++)
                {
                    objApplUnitDetailVOList[i].strApplUnitID = objApplUnitVO.strApplUnitID;
                    lngRes = m_lngAddApplUnitDetail(ref objApplUnitDetailVOList[i]);
                }
            }
            return lngRes;
        }
        #endregion

        #region ����һ����¼��t_aid_lis_apply_unit_detail
        [AutoComplete]
        public long m_lngAddApplUnitDetail(ref clsApplUnitDetail_VO objApplUnitDetailVO)
        {
            long lngRes = 0;

            string strSQL = @"INSERT INTO t_aid_lis_apply_unit_detail
										  (check_item_id_chr, apply_unit_id_chr
										  )
								   VALUES (?, ?)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objApplUnitDetailArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objApplUnitDetailArr);

                objApplUnitDetailArr[0].Value = objApplUnitDetailVO.strCheckItemID;
                objApplUnitDetailArr[1].Value = objApplUnitDetailVO.strApplUnitID;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objApplUnitDetailArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����һ����¼����t_aid_lis_apply_unit��
        [AutoComplete]
        public long m_lngAddApplUnit(ref clsApplUnit_VO objApplUnitVO)
        {
            long lngRes = 0;

            string strSQL = @"INSERT INTO t_aid_lis_apply_unit
										  (apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr,
										   py_code_chr, assist_code01_chr, wb_code_chr, assist_code02_chr,
										   check_category_id_chr, is_no_food_required_chr,
										   is_physical_exam_required_chr,is_reservation_required_chr,
										   PRICE_NUM,COST_NUM,SAMPLE_TYPE_ID_CHR,SUMMARY_VCHR,OUTER_CHECK_FLAG_NUM, REPORTHOUR, SamplingInstr 
										   )
								    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?, ?, ?)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objApplUnitArr = null;
                objHRPSvc.CreateDatabaseParameter(18, out objApplUnitArr);

                if (objApplUnitVO.strApplUnitID == null)
                {
                    objApplUnitVO.strApplUnitID = objHRPSvc.m_strGetNewID("t_aid_lis_apply_unit", "apply_unit_id_chr", 6);
                }

                objApplUnitArr[0].Value = objApplUnitVO.strApplUnitID;
                objApplUnitArr[1].Value = objApplUnitVO.strApplUnitName;
                objApplUnitArr[2].Value = objApplUnitVO.strOtherName;
                objApplUnitArr[3].Value = objApplUnitVO.strPYCode;
                objApplUnitArr[4].Value = objApplUnitVO.strAssistCode01;
                objApplUnitArr[5].Value = objApplUnitVO.strWBCode;
                objApplUnitArr[6].Value = objApplUnitVO.strAssistCode02;
                objApplUnitArr[7].Value = objApplUnitVO.strCheckCategoryID;
                objApplUnitArr[8].Value = objApplUnitVO.strIsNoFoodRequired;
                objApplUnitArr[9].Value = objApplUnitVO.strIsPhysicsExamRequired;
                objApplUnitArr[10].Value = objApplUnitVO.strIsReservationRequired;
                objApplUnitArr[11].Value = objApplUnitVO.strPrice;
                objApplUnitArr[12].Value = objApplUnitVO.strCost;
                objApplUnitArr[13].Value = objApplUnitVO.strSampleTypeID;
                objApplUnitArr[14].Value = objApplUnitVO.strSummary;
                objApplUnitArr[15].Value = objApplUnitVO.strOutCheckFlag;
                objApplUnitArr[16].Value = objApplUnitVO.ReportHour;
                objApplUnitArr[17].Value = objApplUnitVO.SamplingInstr;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objApplUnitArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //        #region ��ȡ���е����뵥Ԫ��� 
        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="objApplUnitVOList"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetAllApplUnit(out clsApplUnit_VO[] objApplUnitVOList)
        //        {
        //            long lngRes = 0;
        //            objApplUnitVOList = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsApplyUnitSvc","m_lngGetAllApplUnit");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select apply_unit_id_chr, apply_unit_name_vchr, other_name_vchr, py_code_chr,
        //                                   assist_code01_chr, wb_code_chr, assist_code02_chr,
        //                                   check_category_id_chr, is_no_food_required_chr,
        //                                   is_physical_exam_required_chr, is_reservation_required_chr, price_num,
        //                                   cost_num, sample_type_id_chr, summary_vchr, outer_check_flag_num
        //                              from t_aid_lis_apply_unit";

        //            DataTable dtbApplUnit = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbApplUnit);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbApplUnit != null)
        //                {
        //                    if(dtbApplUnit.Rows.Count > 0)
        //                    {
        //                        objApplUnitVOList = new clsApplUnit_VO[dtbApplUnit.Rows.Count];
        //                        for(int i=0;i<dtbApplUnit.Rows.Count;i++)
        //                        {
        //                            objApplUnitVOList[i] = new clsApplUnit_VO();
        //                            ConstructApplUnitVO(dtbApplUnit.Rows[i],ref objApplUnitVOList[i]);
        //                        }
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ��ȡ���е����뵥Ԫ��ϸ
        //        [AutoComplete]
        //        public long m_lngGetAllApplUnitDetail(out clsApplUnitDetail_VO[] objApplUnitDetailVOList)
        //        {
        //            long lngRes = 0;
        //            objApplUnitDetailVOList = null;

        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsCheckGroupSvc","m_lngGetAllApplUnitDetail");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select check_item_id_chr, apply_unit_id_chr, print_seq_int
        //                                from t_aid_lis_apply_unit_detail";
        //            DataTable dtbApplUnitDetail = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbApplUnitDetail);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbApplUnitDetail != null)
        //                {
        //                    if(dtbApplUnitDetail.Rows.Count > 0)
        //                    {
        //                        objApplUnitDetailVOList = new clsApplUnitDetail_VO[dtbApplUnitDetail.Rows.Count];
        //                        for(int i=0;i<dtbApplUnitDetail.Rows.Count;i++)
        //                        {
        //                            objApplUnitDetailVOList[i] = new clsApplUnitDetail_VO();
        //                            ConstructApplUnitDetailVO(dtbApplUnitDetail.Rows[i],ref objApplUnitDetailVOList[i]);
        //                        }
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

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
            //xing.chen add	����ע�ֶΣ�
            objApplUnitVO.strSummary = objRow["SUMMARY_VCHR"].ToString().Trim();
            //xing.chen add (�Ƿ���Ժ��־�ֶ�)
            objApplUnitVO.strOutCheckFlag = objRow["OUTER_CHECK_FLAG_NUM"].ToString().Trim();
        }
        #endregion
    }
}
