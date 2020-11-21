using System;
using System.Data;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQueryAppGroupSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsQueryAppGroupSvc()
        {

        }

        #region ��ѯ���е��Զ������������Ϣ
        [AutoComplete]
        public long m_lngGetAllApplUserGroupRelation(
            out clsApplUserGroupRelation_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT * FROM T_AID_LIS_APPUSER_GROUP_RELATE";
            try
            {
                DataTable dtbResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsApplUserGroupRelation_VO[dtbResult.Rows.Count];
                    for (int i = 0; i < p_objResultArr.Length; i++)
                    {
                        p_objResultArr[i] = new clsApplUserGroupRelation_VO();
                        p_objResultArr[i].strUserGroupID = dtbResult.Rows[i]["USER_GROUP_ID_CHR"].ToString().Trim();
                        p_objResultArr[i].strChildUserGroupID = dtbResult.Rows[i]["CHILD_USER_GROUP_ID_CHR"].ToString().Trim();
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

        #region ��ѯһ���Զ�������(�����Զ�����)�����м�����Ŀ��ϸ
        [AutoComplete]
        public long m_lngGetCheckItemInApplGroupDetailByApplUserGroupID( string strApplUserGroupID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            dtbCheckItem = null; 

            string strSQL = @"SELECT t4.*
								FROM t_aid_lis_appuser_group t1,
									 t_aid_lis_appuser_group_detail t2,
									 t_aid_lis_apply_unit_detail t3,
									 t_bse_lis_check_item t4
							   WHERE t1.user_group_id_chr = t2.user_group_id_chr
								 AND t2.apply_unit_id_chr = t3.apply_unit_id_chr
								 AND t3.check_item_id_chr = t4.check_item_id_chr
								 AND t1.user_group_id_chr = '" + strApplUserGroupID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckItem);
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

        #region ��ѯһ���Զ�������(���Զ�����)�����м�����Ŀ��ϸ
        [AutoComplete]
        public long m_lngGetCheckItemApplGroupRelationByApplUserGroupID( string strApplUserGroupID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            dtbCheckItem = null; 

            string strSQL = @"SELECT t4.*
								FROM t_aid_lis_appuser_group t1,
									 t_aid_lis_appuser_group_detail t2,
									 t_aid_lis_apply_unit_detail t3,
									 t_bse_lis_check_item t4
							   WHERE t1.user_group_id_chr = t2.user_group_id_chr
								 AND t2.apply_unit_id_chr = t3.apply_unit_id_chr
								 AND t3.check_item_id_chr = t4.check_item_id_chr
								 AND t1.user_group_id_chr IN (
													  SELECT     t2.child_user_group_id_chr
														FROM t_aid_lis_appuser_group_relate t2
													  START WITH user_group_id_chr = '" + strApplUserGroupID + @"'
													  CONNECT BY PRIOR child_user_group_id_chr = user_group_id_chr)";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckItem);
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

        #region ��ȡ���е��Զ�������µ��������뵥Ԫ��Ϣ
        [AutoComplete]
        public long m_lngGetAllUserGroupApplUnitID( out DataTable dtbApplUnit)
        {
            long lngRes = 0;
            dtbApplUnit = null; 

            string strSQL = @"SELECT DISTINCT apply_unit_id_chr FROM t_aid_lis_appuser_group_detail";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbApplUnit);
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

        #region �����û��Զ�����ID��ѯ����������뵥Ԫ��Ϣ
        [AutoComplete]
        public long m_lngGetApplUnitByUserGroupID( string strUserGroupID, out clsApplUnit_VO[] objApplUnitVOList)
        {
            long lngRes = 0;
            objApplUnitVOList = null; 

            string strSQL = @"SELECT t2.*
								FROM t_aid_lis_appuser_group_detail t1, t_aid_lis_apply_unit t2
							   WHERE t1.apply_unit_id_chr = t2.apply_unit_id_chr
								 AND t1.user_group_id_chr = '" + strUserGroupID + "'";
            DataTable dtbApplUnit = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbApplUnit);
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

        #region �����û��Զ�����ID��ѯ��������Ϣ
        [AutoComplete]
        public long m_lngGetChildGroupByUserGroupID( string strUserGroupID, out clsApplUserGroup_VO[] objAppUserGroupVOList)
        {
            long lngRes = 0;
            objAppUserGroupVOList = null; 

            string strSQL = @"SELECT t1.*
								FROM t_aid_lis_appuser_group t1, t_aid_lis_appuser_group_relate t2
							   WHERE t1.user_group_id_chr = t2.child_user_group_id_chr
								 AND t2.user_group_id_chr = '" + strUserGroupID + "'";
            DataTable dtbAppUser = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbAppUser);
                if (lngRes > 0 && dtbAppUser != null)
                {
                    if (dtbAppUser.Rows.Count > 0)
                    {
                        objAppUserGroupVOList = new clsApplUserGroup_VO[dtbAppUser.Rows.Count];
                        for (int i = 0; i < dtbAppUser.Rows.Count; i++)
                        {
                            objAppUserGroupVOList[i] = new clsApplUserGroup_VO();
                            ConstructApplUserGroupVO(dtbAppUser.Rows[i], ref objAppUserGroupVOList[i]);
                        }
                    }
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

            objApplUnitVO.strSummary = objRow["SUMMARY_VCHR"].ToString().Trim();
            objApplUnitVO.strOutCheckFlag = objRow["OUTER_CHECK_FLAG_NUM"].ToString().Trim();
            objApplUnitVO.ReportHour = objRow["REPORTHOUR"] == DBNull.Value ? 0 : Convert.ToDecimal(objRow["REPORTHOUR"].ToString());
            objApplUnitVO.SamplingInstr = objRow["SamplingInstr"].ToString();
            objApplUnitVO.Jclx_jj = objRow["Jclx_jj"] == DBNull.Value ? 0 : Convert.ToInt32(objRow["Jclx_jj"].ToString());
            objApplUnitVO.Jclx_jtj = objRow["Jclx_jtj"] == DBNull.Value ? 0 : Convert.ToInt32(objRow["Jclx_jtj"].ToString());
        }
        #endregion

        #region ����clsApplUserGroup_VO ͯ�� 2004.05.26		xing.chen modify
        [AutoComplete]
        private void ConstructApplUserGroupVO(System.Data.DataRow objRow, ref clsApplUserGroup_VO objApplUserGroupVO)
        {
            objApplUserGroupVO.strUserGroupID = objRow["USER_GROUP_ID_CHR"].ToString().Trim();
            objApplUserGroupVO.strUserGroupName = objRow["USER_GROUP_NAME_VCHR"].ToString().Trim();
            objApplUserGroupVO.strHasChildGroup = objRow["HAS_CHILD_GROUP"].ToString().Trim();
            objApplUserGroupVO.strPYCode = objRow["PY_CODE_CHR"].ToString().Trim();
            objApplUserGroupVO.strWBCode = objRow["WB_CODE_CHR"].ToString().Trim();
            objApplUserGroupVO.strAssistCode01 = objRow["ASSIST_CODE01_CHR"].ToString().Trim();
            objApplUserGroupVO.strAssistCode02 = objRow["ASSIST_CODE02_CHR"].ToString().Trim();
            objApplUserGroupVO.strGroupFlag = objRow["GROUP_FLAG_CHR"].ToString().Trim();
            objApplUserGroupVO.strOperatorID = objRow["OPERATOR_ID_CHR"].ToString().Trim();

            objApplUserGroupVO.strSummary = objRow["SUMMARY_VCHR"].ToString().Trim();
        }
        #endregion
    }
}
