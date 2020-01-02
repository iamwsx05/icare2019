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
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQuerySampleGroupSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        #region ���캯��
        public clsQuerySampleGroupSvc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

        #region �������뵥ԪID��ѯ�걾�������뵥Ԫ�Ĺ�ϵ
        [AutoComplete]
        public long m_lngGetSampleGroupUnitByApplUnitID( string p_strApplUnitID,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null; 

            string strSQL = @"SELECT * FROM T_AID_LIS_SAMPLE_GROUP_UNIT WHERE APPLY_UNIT_ID_CHR = '" + p_strApplUnitID + @"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����������ID��ѯ���������°��������뵥Ԫ
        /// <summary>
        /// ����������ID��ѯ���������°��������뵥Ԫ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleGroupID">��"" || =nullΪ��ѯȫ��</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplUnitBySampleGroupID( string p_strSampleGroupID,
            out clsLisSampleGroupUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT a.*, b.apply_unit_name_vchr
								FROM t_aid_lis_sample_group_unit a, t_aid_lis_apply_unit b
							   WHERE a.apply_unit_id_chr = b.apply_unit_id_chr";
            if (p_strSampleGroupID != null && p_strSampleGroupID != "")
            {
                strSQL += @" AND a.sample_group_id_chr = '" + p_strSampleGroupID + @"'";
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisSampleGroupUnit_VO[dtbResult.Rows.Count];
                    for (int i = 0; i < p_objResultArr.Length; i++)
                    {
                        p_objResultArr[i] = new clsLisSampleGroupUnit_VO();
                        p_objResultArr[i].m_strAPPLY_UNIT_DESC_VCHR = dtbResult.Rows[i]["apply_unit_name_vchr"].ToString().Trim();
                        p_objResultArr[i].m_strAPPLY_UNIT_ID_CHR = dtbResult.Rows[i]["apply_unit_id_chr"].ToString().Trim();
                        p_objResultArr[i].m_strSAMPLE_GROUP_ID_CHR = dtbResult.Rows[i]["sample_group_id_chr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݱ걾��ID��ȡ����ı걾����
        [AutoComplete]
        public long m_lngGetGroupSampleTypeBySampleGroupID( string p_strSampleGroupID,
            out clsLisGroupSampleType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT t1.sample_type_desc_vchr, t2.sample_group_id_chr,
									 t2.sample_type_id_chr
								FROM t_aid_lis_sampletype t1, t_aid_lis_group_sample_type t2
							   WHERE t1.sample_type_id_chr = t2.sample_type_id_chr
								 AND t2.sample_group_id_chr = '" + p_strSampleGroupID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisGroupSampleType_VO[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsLisGroupSampleType_VO();
                        p_objResultArr[i].m_strSAMPLE_GROUP_ID_CHR = dtbResult.Rows[i]["sample_group_id_chr"].ToString().Trim();
                        p_objResultArr[i].m_strSAMPLE_TYPE_DESC_VCHR = dtbResult.Rows[i]["sample_type_desc_vchr"].ToString().Trim();
                        p_objResultArr[i].m_strSAMPLE_TYPE_ID_CHR = dtbResult.Rows[i]["sample_type_id_chr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����������ID �õ��������Ӧ�������ͺ��б�
        /// <summary>
        /// ����������ID �õ��������Ӧ�������ͺ��б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_strSampleGroupModelArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSampleGroupModelArr( string p_strSampleGroupID, out string[] p_strSampleGroupModelArr)
        {
            long lngRes = 0;
            p_strSampleGroupModelArr = null; 

            string strSQL = @"SELECT *
								FROM t_aid_lis_sample_group_model t1
								WHERE sample_group_id_chr = ?
								";
            DataTable dtbResult = null;
            lngRes = 0;
            try
            {
                System.Data.IDataParameter[] objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr(p_strSampleGroupID);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objParamArr);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strSampleGroupModelArr = new string[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_strSampleGroupModelArr[i] = dtbResult.Rows[i]["device_model_id_chr"].ToString().Trim();
                    }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        #region ���ݱ걾��ID��ȡ��Ӧ�������ͺ��б�
        [AutoComplete]
        public long m_lngGetDeviceModelArrBySampleGroupID( string p_strSampleGroupID,
            out clsLisSampleGroupModel_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT b.DEVICE_MODEL_DESC_VCHR,a.*
								FROM t_aid_lis_sample_group_model a,
									 t_bse_lis_device_model b
							   WHERE a.device_model_id_chr = b.device_model_id_chr
								 AND a.sample_group_id_chr = '" + p_strSampleGroupID + @"'";

            try
            {
                DataTable dtbResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisSampleGroupModel_VO[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsLisSampleGroupModel_VO();
                        p_objResultArr[i].m_strDEVICE_MODEL_ID_CHR = dtbResult.Rows[i]["DEVICE_MODEL_ID_CHR"].ToString().Trim();
                        p_objResultArr[i].m_strDEVICE_MODEL_DESC_VCHR = dtbResult.Rows[i]["DEVICE_MODEL_DESC_VCHR"].ToString().Trim();
                        p_objResultArr[i].m_strSAMPLE_GROUP_ID_CHR = dtbResult.Rows[i]["SAMPLE_GROUP_ID_CHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region �õ���������б�
        /// <summary>
        /// �õ���������б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCategory"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_dtpResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSampleGroupList(
            string p_strCategory, string p_strSampleType, out DataTable p_dtpResult)
        {
            long lngRes = 0;
            p_dtpResult = null; 

            #region SQL
            string strSQL = @"SELECT t1.*
								FROM t_aid_lis_sample_group t1, 
									 t_aid_lis_group_sample_type t2
							   WHERE t1.sample_group_id_chr = t2.sample_group_id_chr ";

            string strSQL_Category = " AND t1.CHECK_CATEGORY_ID_CHR = ? ";
            string strSQL_TYPE = " AND t2.SAMPLE_TYPE_ID_CHR = ? ";
            #endregion

            ArrayList arlSQL = new ArrayList();
            ArrayList arlParm = new ArrayList();

            #region ����
            if (p_strCategory != null && p_strCategory.ToString().Trim() != "")
            {
                arlSQL.Add(strSQL_Category);
                arlParm.Add(p_strCategory.Trim());
            }
            if (p_strSampleType != null && p_strSampleType.ToString().Trim() != "")
            {
                arlSQL.Add(strSQL_TYPE);
                arlParm.Add(p_strSampleType.Trim());
            }
            #endregion

            foreach (object obj in arlSQL)
            {
                strSQL += obj.ToString();
            }

            int intParmCount = arlSQL.Count;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(intParmCount, out objDPArr);

            for (int i = 0; i < intParmCount; i++)
            {
                objDPArr[i].Value = arlParm[i];
            }

            try
            {
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtpResult, objDPArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ���ݱ걾ID��ȡ�걾��VO
        [AutoComplete]
        public long m_lngGetSampleGroupVOBySampleGroupID( string p_strSampleGroupID,
            out clsSampleGroup_VO p_objResultVO)
        {
            long lngRes = 0;
            p_objResultVO = null; 

            string strSQL = @"select t2.sample_type_id_chr, t1.sample_group_id_chr, t1.py_code_chr,
                                   t1.wb_code_chr, t1.assist_code01_chr, t1.assist_code02_chr,
                                   t1.is_hand_work_int, t1.device_model_id_chr, t1.remark_vchr,
                                   t1.check_category_id_chr, t1.sample_type_id_chr,
                                   t1.sample_group_name_chr, t1.print_title_vchr, t1.print_seq_int
                              from t_aid_lis_sample_group t1, t_aid_lis_group_sample_type t2
                             where t1.sample_group_id_chr = t2.sample_group_id_chr
                               and t1.sample_group_id_chr = '" + p_strSampleGroupID + "'";
            DataTable dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultVO = new clsSampleGroup_VO();
                    ConstructSampleGroupVO(dtbResult.Rows[0], ref p_objResultVO);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region �������뵥ԪID�õ������ڱ걾���VO
        /// <summary>
        /// ���ݼ�����ĿID�õ������ڱ걾���VO,����ӡ˳��  
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_intPrintSeq"></param>
        /// <param name="p_objSampleGroupVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSampleGoupVOByApplyUnitID( string p_strApplyUnitID, out clsSampleGroup_VO p_objSampleGroupVO)
        {
            long lngRes = 0;
            p_objSampleGroupVO = null; 

            string strSQL = @"select t1.sample_group_id_chr,
       t1.py_code_chr,
       t1.wb_code_chr,
       t1.assist_code01_chr,
       t1.assist_code02_chr,
       t1.is_hand_work_int,
       t1.device_model_id_chr,
       t1.remark_vchr,
       t1.check_category_id_chr,
       t1.sample_group_name_chr,
       t1.print_title_vchr,
       t1.print_seq_int,
       t3.sample_type_id_chr
  from t_aid_lis_sample_group      t1,
       t_aid_lis_sample_group_unit t2,
       t_aid_lis_group_sample_type t3
 where t1.sample_group_id_chr = t2.sample_group_id_chr
   and t1.sample_group_id_chr = t3.sample_group_id_chr(+)
   and t2.apply_unit_id_chr ='" + p_strApplyUnitID + "'";

            DataTable dtbRet = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbRet);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbRet != null)
                {
                    if (dtbRet.Rows.Count > 0)
                    {
                        p_objSampleGroupVO = new clsSampleGroup_VO();

                        ConstructSampleGroupVO(dtbRet.Rows[0], ref p_objSampleGroupVO);

                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ��ȡĳһ�걾���µ���ϸ����
        [AutoComplete]
        public long m_lngGetAllSampleGroupDetail( string strSampleGroupID, ref clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            long lngRes = 0; 

            string strSQL = @"SELECT   t1.sample_group_id_chr, t2.check_item_id_chr, t2.print_seq_int
									FROM t_aid_lis_sample_group_unit t1, t_aid_lis_apply_unit_detail t2
								WHERE t1.apply_unit_id_chr = t2.apply_unit_id_chr  AND T1.sample_group_id_chr = '" + strSampleGroupID + @"' 
								ORDER BY print_seq_int";
            DataTable dtbSampleGroupDetail = null;
            objSampleGroupDetailVOList = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleGroupDetail);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbSampleGroupDetail != null)
                {
                    if (dtbSampleGroupDetail.Rows.Count > 0)
                    {
                        if (dtbSampleGroupDetail.Rows.Count > 0)
                        {
                            objSampleGroupDetailVOList = new clsSampleGroupDetail_VO[dtbSampleGroupDetail.Rows.Count];
                            for (int i = 0; i < dtbSampleGroupDetail.Rows.Count; i++)
                            {
                                objSampleGroupDetailVOList[i] = new clsSampleGroupDetail_VO();
                                ConstructSampleGroupDetailVO(dtbSampleGroupDetail.Rows[i], ref objSampleGroupDetailVOList[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���еı걾��
        [AutoComplete]
        public long m_lngGetAllSampleGroup( ref clsSampleGroup_VO[] objSampleGroupVOList)
        {
            long lngRes = 0; 

            string strSQL = @"select   sample_group_id_chr, py_code_chr, wb_code_chr, assist_code01_chr,
                                     assist_code02_chr, is_hand_work_int, device_model_id_chr,
                                     remark_vchr, check_category_id_chr, sample_type_id_chr,
                                     sample_group_name_chr, print_title_vchr, print_seq_int
                                from t_aid_lis_sample_group
                            order by sample_group_id_chr";
            DataTable dtbSampleGroup = null;
            objSampleGroupVOList = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleGroup);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbSampleGroup != null)
                {
                    if (dtbSampleGroup.Rows.Count > 0)
                    {
                        objSampleGroupVOList = new clsSampleGroup_VO[dtbSampleGroup.Rows.Count];
                        for (int i = 0; i < dtbSampleGroup.Rows.Count; i++)
                        {
                            objSampleGroupVOList[i] = new clsSampleGroup_VO();
                            ConstructSampleGroupVO(dtbSampleGroup.Rows[i], ref objSampleGroupVOList[i]);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ����clsSampleGroupVO
        [AutoComplete]
        private void ConstructSampleGroupVO(System.Data.DataRow objRow, ref clsSampleGroup_VO p_objSampleGroupVO)
        {
            p_objSampleGroupVO.strSampleGroupID = objRow["SAMPLE_GROUP_ID_CHR"].ToString().Trim();
            p_objSampleGroupVO.strPYCode = objRow["PY_CODE_CHR"].ToString().Trim();
            p_objSampleGroupVO.strWBCode = objRow["WB_CODE_CHR"].ToString().Trim();
            p_objSampleGroupVO.strAssistCode01 = objRow["ASSIST_CODE01_CHR"].ToString().Trim();
            p_objSampleGroupVO.strAssistCode02 = objRow["ASSIST_CODE02_CHR"].ToString().Trim();
            p_objSampleGroupVO.strRemark = objRow["REMARK_VCHR"].ToString().Trim();
            p_objSampleGroupVO.strIsHandWork = objRow["IS_HAND_WORK_INT"].ToString().Trim();
            p_objSampleGroupVO.strDeviceModleID = objRow["DEVICE_MODEL_ID_CHR"].ToString().Trim();
            p_objSampleGroupVO.strCheckCategoryID = objRow["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
            p_objSampleGroupVO.strSampleTypeID = objRow["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
            p_objSampleGroupVO.strSampleGroupName = objRow["SAMPLE_GROUP_NAME_CHR"].ToString().Trim();
            p_objSampleGroupVO.strPRINT_TITLE_VCHR = objRow["PRINT_TITLE_VCHR"].ToString().Trim();
        }
        #endregion

        #region ����clsSampleGroupVODetail
        [AutoComplete]
        private void ConstructSampleGroupDetailVO(System.Data.DataRow objRow, ref clsSampleGroupDetail_VO objSampleGroupDetailVO)
        {
            objSampleGroupDetailVO.strSampleGroupID = objRow["SAMPLE_GROUP_ID_CHR"].ToString().Trim();
            objSampleGroupDetailVO.strCheckItemID = objRow["CHECK_ITEM_ID_CHR"].ToString().Trim();
            objSampleGroupDetailVO.strPrintSeq = objRow["PRINT_SEQ_INT"].ToString().Trim();
        }
        #endregion

        #region ��ȡ���еı걾����ϸ
        [AutoComplete]
        public long m_lngGetAllSampleGroupDetail( out clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            long lngRes = 0;
            objSampleGroupDetailVOList = null; 

            string strSQL = @"SELECT   t1.sample_group_id_chr, t2.check_item_id_chr, t2.print_seq_int
									FROM t_aid_lis_sample_group_unit t1, t_aid_lis_apply_unit_detail t2
								WHERE t1.apply_unit_id_chr = t2.apply_unit_id_chr
								ORDER BY sample_group_id_chr, print_seq_int";
            DataTable dtbSampleGroupDetail = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleGroupDetail);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbSampleGroupDetail != null)
                {
                    if (dtbSampleGroupDetail.Rows.Count > 0)
                    {
                        if (dtbSampleGroupDetail.Rows.Count > 0)
                        {
                            objSampleGroupDetailVOList = new clsSampleGroupDetail_VO[dtbSampleGroupDetail.Rows.Count];
                            for (int i = 0; i < dtbSampleGroupDetail.Rows.Count; i++)
                            {
                                objSampleGroupDetailVOList[i] = new clsSampleGroupDetail_VO();
                                ConstructSampleGroupDetailVO(dtbSampleGroupDetail.Rows[i], ref objSampleGroupDetailVOList[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion
    }
}
