using System;
using System.Data;
using System.Collections;
using System.EnterpriseServices;
using com.digitalwave.Utility; //Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQueryCheckItemSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        #region ��ȡ���뵥Ԫ��������ļ�����Ŀ�б�

        [AutoComplete]
        public long m_lngGetApplyUnitArrCheckItem( string[] p_strApplyUnitArr,
            out clsPISApplyUnitItem[] p_objRecordArr)
        {
            long lngRes = 0;

            p_objRecordArr = null; 
            string strPara = "";

            for (int i = 0; i < p_strApplyUnitArr.Length; i++)
            {
                if (i != p_strApplyUnitArr.Length - 1)
                {
                    strPara += " ?,";
                }
                else
                {
                    strPara += " ?";
                }
            }

            #region SQL
            string strSQL = @"SELECT t1.rptno_chr, t1.pycode_chr, t1.unit_chr, t1.check_item_name_vchr,
                                   t1.is_sex_related_chr, t1.check_item_english_name_vchr,
                                   t1.is_age_related_chr, t1.is_sample_related_chr, t1.formula_vchr,
                                   t1.test_methods_vchr, t1.clinic_meaning_vchr, t1.check_item_id_chr,
                                   t1.shortname_chr, t1.is_qc_required_chr, t1.resulttype_chr,
                                   t1.ref_value_range_vchr, t1.wbcode_chr, t1.assist_code01_chr,
                                   t1.assist_code02_chr, t1.is_no_food_required_chr,
                                   t1.is_physical_exam_required_chr, t1.is_reservation_required_chr,
                                   t1.sample_valid_time_dec, t1.sample_valid_time_unit_chr, t1.modify_dat,
                                   t1.operatorid_chr, t1.check_category_id_chr, t1.ref_max_val_vchr,
                                   t1.ref_min_val_vchr, t1.sampletype_vchr, t1.is_menses_related_chr,
                                   t1.is_calculated_chr, t1.formula_user_vchr, t1.alarm_low_val_vchr,
                                   t1.alarm_up_val_vchr, t1.alert_value_range_vchr,
                                     t2.apply_unit_id_chr,t3.apply_unit_name_vchr
								FROM t_bse_lis_check_item t1, t_aid_lis_apply_unit_detail t2 ,t_aid_lis_apply_unit t3
							   WHERE t2.check_item_id_chr = t1.check_item_id_chr
								 AND t2.apply_unit_id_chr = t3.apply_unit_id_chr
								 AND t2.apply_unit_id_chr in ( " + strPara + @" )
							ORDER BY t2.PRINT_SEQ_INT";
            #endregion

            ArrayList arlApplyUnit = new ArrayList();

            for (int i = 0; i < p_strApplyUnitArr.Length; i++)
            {
                arlApplyUnit.Add(p_strApplyUnitArr[i]);
            }

            try
            {
                System.Data.IDataParameter[] objIDPArr = clsPublicSvc.m_objConstructIDataParameterArr(arlApplyUnit.ToArray());
                DataTable dtbCheckItem = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbCheckItem, objIDPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbCheckItem != null && dtbCheckItem.Rows.Count > 0)
                {
                    p_objRecordArr = new clsPISApplyUnitItem[dtbCheckItem.Rows.Count];
                    for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                    {
                        p_objRecordArr[i] = new clsPISApplyUnitItem();
                        p_objRecordArr[i].m_strAPPLY_UNIT_ID_CHR = dtbCheckItem.Rows[i]["APPLY_UNIT_ID_CHR"].ToString().Trim();
                        p_objRecordArr[i].m_strAPPLY_UNIT_NAME_VCHR = dtbCheckItem.Rows[i]["APPLY_UNIT_NAME_VCHR"].ToString().Trim();
                        p_objRecordArr[i].m_strCHECK_ITEM_ID_CHR = dtbCheckItem.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        p_objRecordArr[i].m_strCHECK_ITEM_NAME_VCHR = dtbCheckItem.Rows[i]["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
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

        #region ֵģ��

        #region ���ݼ��������������ѯģ����Ϣ
        [AutoComplete]
        public long m_lngGetTemplateInfoByCondition( string p_strCheckCategory, string p_strSampleType,
            out clsLisValueTemplate_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 
            string strSQL = @"SELECT a.*
							    FROM t_aid_lis_valuetemplate a
							   WHERE 1=1";

            if (p_strCheckCategory != null && p_strCheckCategory != "")
            {
                strSQL += " AND a.check_category_id_chr = '" + p_strCheckCategory + "'";
            }
            if (p_strSampleType != null && p_strSampleType != "")
            {
                strSQL += " AND a.sample_type_id_chr = '" + p_strSampleType + "'";
            }
            strSQL += " ORDER BY a.template_id_chr";
            DataTable dtbResult = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisValueTemplate_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsLisValueTemplate_VO();
                        p_objResultArr[i1].m_strTEMPLATE_ID_CHR = dtbResult.Rows[i1]["TEMPLATE_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTEMPLATE_NAME_VCHR = dtbResult.Rows[i1]["TEMPLATE_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCHECK_CATEGORY_ID_CHR = dtbResult.Rows[i1]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSAMPLE_TYPE_ID_CHR = dtbResult.Rows[i1]["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSUMMARY_VCHR = dtbResult.Rows[i1]["SUMMARY_VCHR"].ToString().Trim();
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

        #region ����ģ��ID��ѯ��Ӧ��ģ����ϸ��Ϣ
        [AutoComplete]
        public long m_lngGetTemplateDetailByTemplateID( string p_strTemplateID,
            out clsLisValueTemplateDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 
            //change by wjqin(07-4-23)
            //            string strSQL = @"SELECT *
            //								FROM t_aid_lis_valuetemplate_detail
            //							   WHERE template_id_chr = '"+p_strTemplateID+@"'
            //							   ORDER BY INDEX_INT";
            //            try
            //            {
            //                DataTable dtbResult = new DataTable();
            //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            //                objHRPSvc.Dispose();
            //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
            //                {
            //                    p_objResultArr = new clsLisValueTemplateDetail_VO[dtbResult.Rows.Count];
            //                    for(int i1=0;i1<p_objResultArr.Length;i1++)
            //                    {
            //                        p_objResultArr[i1] = new clsLisValueTemplateDetail_VO();
            //                        p_objResultArr[i1].m_strTEMPLATE_ID_CHR = dtbResult.Rows[i1]["TEMPLATE_ID_CHR"].ToString().Trim();
            //                        p_objResultArr[i1].m_intINDEX_INT = Convert.ToInt32(dtbResult.Rows[i1]["INDEX_INT"].ToString().Trim());
            //                        p_objResultArr[i1].m_intSEQ_INT = Convert.ToInt32(dtbResult.Rows[i1]["SEQ_INT"].ToString().Trim());
            //                        p_objResultArr[i1].m_strVALUE_VCHR = dtbResult.Rows[i1]["VALUE_VCHR"].ToString().Trim();
            //                        p_objResultArr[i1].m_intDEFAULT_VALUE_FLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["DEFAULT_VALUE_FLAG_INT"].ToString().Trim());
            //                    }
            //                }
            //            }
            //            catch(Exception objEx)
            //            {
            //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //                bool blnRes = objLogger.LogError(objEx);
            //            }



            string strSQL = @"select template_id_chr,index_int,seq_int,value_vchr,default_value_flag_int
            								from t_aid_lis_valuetemplate_detail
            							   where template_id_chr = ?
            							   order by index_int";
            try
            {
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objDPArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplateID.PadRight(6, ' ');
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisValueTemplateDetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsLisValueTemplateDetail_VO();
                        p_objResultArr[i1].m_strTEMPLATE_ID_CHR = dtbResult.Rows[i1]["TEMPLATE_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intINDEX_INT = Convert.ToInt32(dtbResult.Rows[i1]["INDEX_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSEQ_INT = Convert.ToInt32(dtbResult.Rows[i1]["SEQ_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strVALUE_VCHR = dtbResult.Rows[i1]["VALUE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intDEFAULT_VALUE_FLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["DEFAULT_VALUE_FLAG_INT"].ToString().Trim());
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

        #region ���ݼ�����ĿID��ѯ��Ӧ��ģ����ϸ��Ϣ
        [AutoComplete]
        public long m_lngGetTemplateDetailByCheckItemID( string p_strCheckItemID,
            out clsLisValueTemplateDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 
            string strSQL = @"SELECT a.*
								FROM t_aid_lis_valuetemplate_detail a
									 t_aid_lis_valuetemplate_item b
							   WHERE b.check_item_id_chr = '" + p_strCheckItemID + @"'
							   ORDER BY a.INDEX_INT";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisValueTemplateDetail_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsLisValueTemplateDetail_VO();
                        p_objResultArr[i1].m_strTEMPLATE_ID_CHR = dtbResult.Rows[i1]["TEMPLATE_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intINDEX_INT = Convert.ToInt32(dtbResult.Rows[i1]["INDEX_INT"].ToString().Trim());
                        p_objResultArr[i1].m_intSEQ_INT = Convert.ToInt32(dtbResult.Rows[i1]["SEQ_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strVALUE_VCHR = dtbResult.Rows[i1]["VALUE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intDEFAULT_VALUE_FLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["DEFAULT_VALUE_FLAG_INT"].ToString().Trim());
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

        #region ���ݼ�����ĿID��ѯ��T_AID_LIS_VALUETEMPLATE_ITEM�ļ�¼
        [AutoComplete]
        public long m_lngGetValueTemplateItemByCheckItemID( string p_strCheckItemID,
            out clsLisValueTemplateItem_VO p_objResult)
        {
            long lngRes = 0;
            p_objResult = null; 
            string strSQL = @"SELECT a.*,b.TEMPLATE_NAME_VCHR
								FROM T_AID_LIS_VALUETEMPLATE_ITEM a,
									 T_AID_LIS_VALUETEMPLATE b
							   WHERE a.TEMPLATE_ID_CHR = b.TEMPLATE_ID_CHR
								 AND a.CHECK_ITEM_ID_CHR = '" + p_strCheckItemID + @"'";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsLisValueTemplateItem_VO();
                    p_objResult.m_strCHECK_ITEM_ID_CHR = dtbResult.Rows[0]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                    p_objResult.m_strTEMPLATE_ID_CHR = dtbResult.Rows[0]["TEMPLATE_ID_CHR"].ToString().Trim();
                    p_objResult.m_strTEMPLATE_NAME_VCHR = dtbResult.Rows[0]["TEMPLATE_NAME_VCHR"].ToString().Trim();
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

        #region ����ģ��ID��ѯ��Ӧ��ģ����Ϣ
        [AutoComplete]
        public long m_lngGetValueTemplateByTemplateID( string p_strTemplateID,
            out clsLisValueTemplate_VO p_objResult)
        {
            long lngRes = 0;
            p_objResult = null; 
            string strSQL = @"SELECT *
							    FROM t_aid_lis_valuetemplate
							   WHERE template_id_chr = '" + p_strTemplateID + @"'";
            DataTable dtbResult = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsLisValueTemplate_VO();
                    p_objResult = new clsLisValueTemplate_VO();
                    p_objResult.m_strTEMPLATE_ID_CHR = dtbResult.Rows[0]["TEMPLATE_ID_CHR"].ToString().Trim();
                    p_objResult.m_strTEMPLATE_NAME_VCHR = dtbResult.Rows[0]["TEMPLATE_NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strCHECK_CATEGORY_ID_CHR = dtbResult.Rows[0]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                    p_objResult.m_strSAMPLE_TYPE_ID_CHR = dtbResult.Rows[0]["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
                    p_objResult.m_strSUMMARY_VCHR = dtbResult.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
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

        #region ���ݼ�����ĿID��ѯģ���������Ϣ
        [AutoComplete]
        public long m_lngGetAllTemplateInfoByCheckItemID( string p_strCheckItemID,
            out clsLisValueTemplateItem_VO p_objTemplateItem, out clsLisValueTemplate_VO p_objTemplate,
            out clsLisValueTemplateDetail_VO[] p_objTemplateDetailArr)
        {
            long lngRes = 0;
            p_objTemplateItem = null;
            p_objTemplate = null;
            p_objTemplateDetailArr = null; 
            lngRes = m_lngGetValueTemplateItemByCheckItemID( p_strCheckItemID, out p_objTemplateItem);
            if (lngRes > 0 && p_objTemplateItem != null)
            {
                lngRes = m_lngGetValueTemplateByTemplateID( p_objTemplateItem.m_strTEMPLATE_ID_CHR, out p_objTemplate);
                if (lngRes > 0 && p_objTemplate != null)
                {
                    lngRes = m_lngGetTemplateDetailByTemplateID( p_objTemplateItem.m_strTEMPLATE_ID_CHR, out p_objTemplateDetailArr);
                }
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region ���ݼ�������ȡ������Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemArrByCheckCategory( string p_strCheckCategory,
            out DataTable p_dtbResultArr)
        {
            long lngRes = 0;
            p_dtbResultArr = null; 
            string strSQL = @"SELECT *
								FROM t_bse_lis_check_item
							   WHERE check_category_id_chr = '" + p_strCheckCategory + @"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResultArr);
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

        #region ���ݼ���������Ʒ���,������,��ѯ���еļ�����Ŀ
        [AutoComplete]
        public long m_lngQryCheckItemByCheckCategoryAndSampleType(
            string p_strCheckCategory, string p_strSampleType, string p_strSampleGroup, out DataTable p_dtbCheckItem)
        {
            long lngRes = 0;
            p_dtbCheckItem = null; 
            string strSQL = @"select distinct t1.rptno_chr,
       t1.pycode_chr,
       t1.unit_chr,
       t1.check_item_name_vchr,
       t1.is_sex_related_chr,
       t1.check_item_english_name_vchr,
       t1.is_age_related_chr,
       t1.is_sample_related_chr,
       t1.formula_vchr,
       t1.test_methods_vchr,
       t1.clinic_meaning_vchr,
       t1.check_item_id_chr,
       t1.shortname_chr,
       t1.is_qc_required_chr,
       t1.resulttype_chr,
       t1.ref_value_range_vchr,
       t1.wbcode_chr,
       t1.assist_code01_chr,
       t1.assist_code02_chr,
       t1.is_no_food_required_chr,
       t1.is_physical_exam_required_chr,
       t1.is_reservation_required_chr,
       t1.sample_valid_time_dec,
       t1.sample_valid_time_unit_chr,
       t1.modify_dat,
       t1.operatorid_chr,
       t1.check_category_id_chr,
       t1.ref_max_val_vchr,
       t1.ref_min_val_vchr,
       t1.sampletype_vchr,
       t1.is_menses_related_chr,
       t1.is_calculated_chr,
       t1.formula_user_vchr,
       t1.alarm_low_val_vchr,
       t1.alarm_up_val_vchr,
       t1.alert_value_range_vchr,
       t1.itemprice_mny,
       t2.check_category_desc_vchr
  from t_bse_lis_check_item         t1,
       t_bse_lis_check_category     t2,
       t_aid_lis_sampletype         t3,
       v_lis_bse_sample_group_items t4
 where t1.check_category_id_chr = t2.check_category_id_chr(+)
   and t1.check_item_id_chr = t4.check_item_id_chr(+)
   and t1.sampletype_vchr = t3.sample_type_id_chr
   and t1.sampletype_vchr = '" + p_strSampleType + @"'
   and t1.check_category_id_chr = '" + p_strCheckCategory + @"'";
            if (p_strSampleGroup != null && p_strSampleGroup.Trim() != "")
            {
                strSQL += " and t4.sample_group_id_chr = '" + p_strSampleGroup + "'";
            }
            strSQL += " order by t1.check_item_id_chr";
            p_dtbCheckItem = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbCheckItem);
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

        #region ���ݼ����������������ϲ�ѯ��صļ�����Ŀ��Ϣ
        [AutoComplete]
        public long m_lngGetCheckItemArrByCondition( string p_strCheckCategoryID,
            string p_strSampleTypeID, out clsCheckItem_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 
            string strSQL = @"SELECT * FROM T_BSE_LIS_CHECK_ITEM WHERE 1=1 ";
            if (p_strCheckCategoryID != "")
            {
                strSQL += " AND CHECK_CATEGORY_ID_CHR = '" + p_strCheckCategoryID + "'";
            }
            if (p_strSampleTypeID != "")
            {
                strSQL += " AND SAMPLETYPE_VCHR = '" + p_strSampleTypeID + "'";
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsCheckItem_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsCheckItem_VO();
                        p_objResultArr[i1].m_strRPTNO = dtbResult.Rows[i1]["RPTNO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPycode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUnit = dtbResult.Rows[i1]["UNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_Name = dtbResult.Rows[i1]["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Sex_Related = dtbResult.Rows[i1]["IS_SEX_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_English_Name = dtbResult.Rows[i1]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Age_Related = dtbResult.Rows[i1]["IS_AGE_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Sample_Related = dtbResult.Rows[i1]["IS_SAMPLE_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFormula = dtbResult.Rows[i1]["FORMULA_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTest_Method = dtbResult.Rows[i1]["TEST_METHODS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strClinic_meaning = dtbResult.Rows[i1]["CLINIC_MEANING_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_ID = dtbResult.Rows[i1]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strShortName = dtbResult.Rows[i1]["SHORTNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Qc_Required = dtbResult.Rows[i1]["IS_QC_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strResultType = dtbResult.Rows[i1]["RESULTTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRef_Value_Range = dtbResult.Rows[i1]["REF_VALUE_RANGE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWbcode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAssist_Code01 = dtbResult.Rows[i1]["ASSIST_CODE01_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAssist_Code02 = dtbResult.Rows[i1]["ASSIST_CODE02_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_No_Food_Required = dtbResult.Rows[i1]["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Physical_Exam_Required = dtbResult.Rows[i1]["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Reservation_Required = dtbResult.Rows[i1]["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSample_Valid_Time = dtbResult.Rows[i1]["SAMPLE_VALID_TIME_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strSample_Valid_Time_Unit = dtbResult.Rows[i1]["SAMPLE_VALID_TIME_UNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strModify_Dat = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strOperator_ID = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Category_ID = dtbResult.Rows[i1]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRef_Value_Max = dtbResult.Rows[i1]["REF_MAX_VAL_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRef_Value_Min = dtbResult.Rows[i1]["REF_MIN_VAL_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSampleTypeID = dtbResult.Rows[i1]["SAMPLETYPE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Menses_Related = dtbResult.Rows[i1]["IS_MENSES_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Calculated = dtbResult.Rows[i1]["IS_CALCULATED_CHR"].ToString().Trim();
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

        #region ����check_item_id��ѯ��Ӧ�ļ�����Ŀ��ϢVO
        /// <summary>
        /// ����check_item_id��ѯ��Ӧ�ļ�����Ŀ��ϢVO
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckItemVOByCheckItemID( string p_strCheckItemID, out clsCheckItem_VO p_objCheckItemVO)
        {
            long lngRes = 0;
            p_objCheckItemVO = null; 
            lngRes = 0;
            DataTable dtbItem = null;
            lngRes = m_lngGetCheckItemInfoByCheckItemID( p_strCheckItemID, out dtbItem);
            if (lngRes > 0 && dtbItem != null && dtbItem.Rows.Count != 0)
            {
                p_objCheckItemVO = new clsCheckItem_VO();
                ConstructCheckItemVO(dtbItem.Rows[0], ref p_objCheckItemVO);
            }
            return lngRes;
        }
        #endregion

        #region ����check_item_id�����䡢�Ա���¾����ڲ�ѯ���������Ĳο�ֵ��Χ
        [AutoComplete]
        public long m_lngGetCheckItemRefByCondition( string p_strAge, string p_strSex, string p_strMenses,
            string p_strCheckItemID, out clsCheckItemRef_VO objCheckItemRefVO)
        {
            long lngRes = 0;
            objCheckItemRefVO = null; 
            DataTable dtbCheckItem = null;
            DataTable dtbCheckItemRef = null;

            string strSQL = @"SELECT * FROM T_BSE_LIS_ITEMREF WHERE CHECK_ITEM_ID_CHR = '" + p_strCheckItemID + "'";

            try
            {
                lngRes = m_lngGetCheckItemInfoByCheckItemID( p_strCheckItemID, out dtbCheckItem);
                if (lngRes > 0 && dtbCheckItem != null)
                {
                    if (dtbCheckItem.Rows.Count > 0)
                    {
                        //�жϲο�ֵ���¾������й�
                        if (dtbCheckItem.Rows[0]["IS_MENSES_RELATED_CHR"].ToString().Trim() == "1")
                        {
                            if (p_strMenses == "" || p_strMenses == null)
                            {
                                return 0;
                            }
                            else
                            {
                                strSQL += " AND MENSES_ID_CHR = '" + p_strMenses + "'";
                            }
                        }
                        //�жϲο�ֵ�������й�
                        if (dtbCheckItem.Rows[0]["IS_AGE_RELATED_CHR"].ToString().Trim() == "1")
                        {
                            if (p_strAge == "" || p_strAge == null)
                            {
                                return 0;
                            }
                        }
                        //�жϲο�ֵ���Ա��й�
                        if (dtbCheckItem.Rows[0]["IS_SEX_RELATED_CHR"].ToString().Trim() == "1")
                        {
                            if (p_strSex == "" || p_strSex == null)
                            {
                                return 0;
                            }
                            else
                            {
                                strSQL += " AND SEX_VCHR = '" + p_strSex + "'";
                            }
                        }

                        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckItemRef);
                        objHRPSvc.Dispose();

                        if (lngRes > 0 && dtbCheckItemRef != null)
                        {
                            if (dtbCheckItemRef.Rows.Count > 0)
                            {
                                //���ο�ֵ�������й�
                                if (dtbCheckItem.Rows[0]["IS_AGE_RELATED_CHR"].ToString().Trim() == "1")
                                {
                                    string[] strAgeArr = p_strAge.Split(new char[] { ' ' });
                                    if (strAgeArr != null && strAgeArr.Length == 2)
                                    {
                                        for (int i = 0; i < dtbCheckItemRef.Rows.Count; i++)
                                        {
                                            string[] strFromAgeArr = dtbCheckItemRef.Rows[i]["FROM_AGE_DEC"].ToString().Trim().Split(new char[] { ' ' });
                                            int intFromAge = 0;
                                            int intToAge = 0;
                                            if (strFromAgeArr[1].Trim() == strAgeArr[1].Trim())
                                            {
                                                if (strFromAgeArr != null && strFromAgeArr.Length == 2)
                                                {
                                                    intFromAge = int.Parse(strFromAgeArr[0]);
                                                }
                                            }

                                            string[] strToAgeArr = dtbCheckItemRef.Rows[i]["TO_AGE_DEC"].ToString().Trim().Split(new char[] { ' ' });
                                            if (strFromAgeArr[1].Trim() == strAgeArr[1].Trim())
                                            {
                                                if (strToAgeArr != null && strToAgeArr.Length == 2)
                                                {
                                                    intToAge = int.Parse(strToAgeArr[0]);
                                                }
                                            }

                                            if (intFromAge <= int.Parse(strAgeArr[0].Trim()) && int.Parse(strAgeArr[0].Trim()) <= intToAge)
                                            {
                                                objCheckItemRefVO = new clsCheckItemRef_VO();
                                                objCheckItemRefVO.m_strCheck_Item_ID = dtbCheckItemRef.Rows[i]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                                                objCheckItemRefVO.m_strClinic_Meaning = dtbCheckItemRef.Rows[i]["CLINIC_MEANING_VCHR"].ToString().Trim();
                                                objCheckItemRefVO.m_strFrom_Age = dtbCheckItemRef.Rows[i]["FROM_AGE_DEC"].ToString().Trim();//intFromAge.ToString().Trim();
                                                objCheckItemRefVO.m_strTo_Age = dtbCheckItemRef.Rows[i]["TO_AGE_DEC"].ToString().Trim();//intToAge.ToString().Trim();
                                                objCheckItemRefVO.m_strSeq_Int = dtbCheckItemRef.Rows[i]["SEQ_INT"].ToString().Trim();
                                                objCheckItemRefVO.m_strMenses_ID = dtbCheckItemRef.Rows[i]["MENSES_ID_CHR"].ToString().Trim();
                                                objCheckItemRefVO.m_strMin_Val = dtbCheckItemRef.Rows[i]["MIN_VAL_VCHR"].ToString().Trim();
                                                objCheckItemRefVO.m_strMax_Val = dtbCheckItemRef.Rows[i]["MAX_VAL_VCHR"].ToString().Trim();
                                                objCheckItemRefVO.m_strRef_Val = dtbCheckItemRef.Rows[i]["REF_VALUE_RANGE_VCHR"].ToString().Trim();
                                                objCheckItemRefVO.m_strSex = dtbCheckItemRef.Rows[i]["SEX_VCHR"].ToString().Trim();
                                                objCheckItemRefVO.CrValMin = dtbCheckItemRef.Rows[i]["crvalmin"].ToString().Trim();
                                                objCheckItemRefVO.CrValMax = dtbCheckItemRef.Rows[i]["crvalmax"].ToString().Trim();
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    objCheckItemRefVO = new clsCheckItemRef_VO();
                                    objCheckItemRefVO.m_strCheck_Item_ID = dtbCheckItemRef.Rows[0]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                                    objCheckItemRefVO.m_strClinic_Meaning = dtbCheckItemRef.Rows[0]["CLINIC_MEANING_VCHR"].ToString().Trim();
                                    objCheckItemRefVO.m_strFrom_Age = dtbCheckItemRef.Rows[0]["FROM_AGE_DEC"].ToString().Trim();
                                    objCheckItemRefVO.m_strTo_Age = dtbCheckItemRef.Rows[0]["TO_AGE_DEC"].ToString().Trim();
                                    objCheckItemRefVO.m_strSeq_Int = dtbCheckItemRef.Rows[0]["SEQ_INT"].ToString().Trim();
                                    objCheckItemRefVO.m_strMenses_ID = dtbCheckItemRef.Rows[0]["MENSES_ID_CHR"].ToString().Trim();
                                    objCheckItemRefVO.m_strMin_Val = dtbCheckItemRef.Rows[0]["MIN_VAL_VCHR"].ToString().Trim();
                                    objCheckItemRefVO.m_strMax_Val = dtbCheckItemRef.Rows[0]["MAX_VAL_VCHR"].ToString().Trim();
                                    objCheckItemRefVO.m_strRef_Val = dtbCheckItemRef.Rows[0]["REF_VALUE_RANGE_VCHR"].ToString().Trim();
                                    objCheckItemRefVO.m_strSex = dtbCheckItemRef.Rows[0]["SEX_VCHR"].ToString().Trim();
                                    objCheckItemRefVO.CrValMin = dtbCheckItemRef.Rows[0]["crvalmin"].ToString().Trim();
                                    objCheckItemRefVO.CrValMax = dtbCheckItemRef.Rows[0]["crvalmax"].ToString().Trim();
                                }
                            }
                        }
                    }
                }
                else
                {
                    return lngRes;
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

        #region ����check_item_id��ѯ��Ӧ�ļ�����Ŀ��Ϣ
        [AutoComplete]
        public long m_lngGetCheckItemInfoByCheckItemID( string p_strCheckItemID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            dtbCheckItem = null; 
            string strSQL = @"SELECT * FROM t_bse_lis_check_item WHERE check_item_id_chr = '" + p_strCheckItemID + "'";
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

        #region �������뵥ԪID��ѯ���еļ�����Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemByApplUnitID( string strApplUnitID,
            out DataTable dtbCheckItem)
        {
            long lngRes = 0;

            dtbCheckItem = null; 
            string strSQL = @"select t1.rptno_chr,
       t1.pycode_chr,
       t1.unit_chr,
       t1.check_item_name_vchr,
       t1.is_sex_related_chr,
       t1.check_item_english_name_vchr,
       t1.is_age_related_chr,
       t1.is_sample_related_chr,
       t1.formula_vchr,
       t1.test_methods_vchr,
       t1.clinic_meaning_vchr,
       t1.check_item_id_chr,
       t1.shortname_chr,
       t1.is_qc_required_chr,
       t1.resulttype_chr,
       t1.ref_value_range_vchr,
       t1.wbcode_chr,
       t1.assist_code01_chr,
       t1.assist_code02_chr,
       t1.is_no_food_required_chr,
       t1.is_physical_exam_required_chr,
       t1.is_reservation_required_chr,
       t1.sample_valid_time_dec,
       t1.sample_valid_time_unit_chr,
       t1.modify_dat,
       t1.operatorid_chr,
       t1.check_category_id_chr,
       t1.ref_max_val_vchr,
       t1.ref_min_val_vchr,
       t1.sampletype_vchr,
       t1.is_menses_related_chr,
       t1.is_calculated_chr,
       t1.formula_user_vchr,
       t1.alarm_low_val_vchr,
       t1.alarm_up_val_vchr,
       t1.alert_value_range_vchr,
       t1.itemprice_mny,
       t2.apply_unit_id_chr,
       t2.print_seq_int
  from t_bse_lis_check_item t1, t_aid_lis_apply_unit_detail t2
 where t2.check_item_id_chr = t1.check_item_id_chr
   and t2.apply_unit_id_chr = '" + strApplUnitID + @"'
 order by t2.print_seq_int
";
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

        #region �������뵥ԪID��ѯ���еļ�����Ŀ VO
        [AutoComplete]
        public long m_lngGetCheckItemByApplUnitID( string p_strApplUnitID,
            out clsCheckItem_VO[] p_objCheckItemVOArr)
        {
            long lngRes = 0;

            p_objCheckItemVOArr = null;

            DataTable dtbCheckItem = null;

            lngRes = this.m_lngGetCheckItemByApplUnitID( p_strApplUnitID, out dtbCheckItem);
            if (lngRes > 0 && dtbCheckItem != null)
            {
                p_objCheckItemVOArr = new clsCheckItem_VO[dtbCheckItem.Rows.Count];

                for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                {
                    p_objCheckItemVOArr[i] = new clsCheckItem_VO();
                    this.ConstructCheckItemVO(dtbCheckItem.Rows[i], ref p_objCheckItemVOArr[i]);
                }
            }
            return lngRes;
        }
        #endregion

        #region ���ݱ걾��ID��ѯ���еļ�����Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemBySampleGroupID( string strSampleGroupID,
            out clsCheckItem_VO[] objCheckItemVOList)
        {
            long lngRes = 0;

            objCheckItemVOList = null; 
            string strSQL = @"SELECT t1.rptno_chr, t1.pycode_chr, t1.unit_chr, t1.check_item_name_vchr,
                                   t1.is_sex_related_chr, t1.check_item_english_name_vchr,
                                   t1.is_age_related_chr, t1.is_sample_related_chr, t1.formula_vchr,
                                   t1.test_methods_vchr, t1.clinic_meaning_vchr, t1.check_item_id_chr,
                                   t1.shortname_chr, t1.is_qc_required_chr, t1.resulttype_chr,
                                   t1.ref_value_range_vchr, t1.wbcode_chr, t1.assist_code01_chr,
                                   t1.assist_code02_chr, t1.is_no_food_required_chr,
                                   t1.is_physical_exam_required_chr, t1.is_reservation_required_chr,
                                   t1.sample_valid_time_dec, t1.sample_valid_time_unit_chr, t1.modify_dat,
                                   t1.operatorid_chr, t1.check_category_id_chr, t1.ref_max_val_vchr,
                                   t1.ref_min_val_vchr, t1.sampletype_vchr, t1.is_menses_related_chr,
                                   t1.is_calculated_chr, t1.formula_user_vchr, t1.alarm_low_val_vchr,
                                   t1.alarm_up_val_vchr, t1.alert_value_range_vchr
							    FROM t_bse_lis_check_item t1, t_aid_lis_sample_group_detail t2
							   WHERE t1.check_item_id_chr = t2.check_item_id_chr
								 AND t2.sample_group_id_chr = '" + strSampleGroupID + "'";
            DataTable dtbCheckItem = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckItem);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbCheckItem != null)
                {
                    if (dtbCheckItem.Rows.Count > 0)
                    {
                        objCheckItemVOList = new clsCheckItem_VO[dtbCheckItem.Rows.Count];
                        for (int i = 0; i < dtbCheckItem.Rows.Count; i++)
                        {
                            objCheckItemVOList[i] = new clsCheckItem_VO();
                            ConstructCheckItemVO(dtbCheckItem.Rows[i], ref objCheckItemVOList[i]);
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

        #region ���ݱ걾��ID��ѯ���еļ�����Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemBySampleGroupID( string strSampleGroupID,
            out DataTable dtbCheckItem)
        {
            long lngRes = 0;

            dtbCheckItem = null; 

            string strSQL = @"SELECT t1.rptno_chr, t1.pycode_chr, t1.unit_chr, t1.check_item_name_vchr,
                                   t1.is_sex_related_chr, t1.check_item_english_name_vchr,
                                   t1.is_age_related_chr, t1.is_sample_related_chr, t1.formula_vchr,
                                   t1.test_methods_vchr, t1.clinic_meaning_vchr, t1.check_item_id_chr,
                                   t1.shortname_chr, t1.is_qc_required_chr, t1.resulttype_chr,
                                   t1.ref_value_range_vchr, t1.wbcode_chr, t1.assist_code01_chr,
                                   t1.assist_code02_chr, t1.is_no_food_required_chr,
                                   t1.is_physical_exam_required_chr, t1.is_reservation_required_chr,
                                   t1.sample_valid_time_dec, t1.sample_valid_time_unit_chr, t1.modify_dat,
                                   t1.operatorid_chr, t1.check_category_id_chr, t1.ref_max_val_vchr,
                                   t1.ref_min_val_vchr, t1.sampletype_vchr, t1.is_menses_related_chr,
                                   t1.is_calculated_chr, t1.formula_user_vchr, t1.alarm_low_val_vchr,
                                   t1.alarm_up_val_vchr, t1.alert_value_range_vchr,
                                     t2.print_seq_int
							    FROM t_bse_lis_check_item t1, t_aid_lis_sample_group_detail t2
							   WHERE t1.check_item_id_chr = t2.check_item_id_chr
								 AND t2.sample_group_id_chr = '" + strSampleGroupID + @"'
							ORDER BY t2.print_seq_int";
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

        #region ����CheckItemVO
        [AutoComplete]
        public void ConstructCheckItemVO(System.Data.DataRow objRow, ref clsCheckItem_VO objCheckItemVO)
        {
            objCheckItemVO.m_strCheck_Item_ID = objRow["CHECK_ITEM_ID_CHR"].ToString().Trim();
            objCheckItemVO.m_strCheck_Item_Name = objRow["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
            objCheckItemVO.m_strRPTNO = objRow["RPTNO_CHR"].ToString().Trim();
            objCheckItemVO.m_strPycode = objRow["PYCODE_CHR"].ToString().Trim();
            objCheckItemVO.m_strUnit = objRow["UNIT_CHR"].ToString().Trim();
            objCheckItemVO.m_strIs_Sex_Related = objRow["IS_SEX_RELATED_CHR"].ToString().Trim();
            objCheckItemVO.m_strCheck_Item_English_Name = objRow["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim();
            objCheckItemVO.m_strIs_Age_Related = objRow["IS_AGE_RELATED_CHR"].ToString().Trim();
            objCheckItemVO.m_strIs_Sample_Related = objRow["IS_SAMPLE_RELATED_CHR"].ToString().Trim();
            objCheckItemVO.m_strFormula = objRow["FORMULA_VCHR"].ToString().Trim();
            objCheckItemVO.m_strTest_Method = objRow["TEST_METHODS_VCHR"].ToString().Trim();
            objCheckItemVO.m_strClinic_meaning = objRow["CLINIC_MEANING_VCHR"].ToString().Trim();
            objCheckItemVO.m_strShortName = objRow["SHORTNAME_CHR"].ToString().Trim();
            objCheckItemVO.m_strIs_Qc_Required = objRow["IS_QC_REQUIRED_CHR"].ToString().Trim();
            objCheckItemVO.m_strResultType = objRow["RESULTTYPE_CHR"].ToString().Trim();
            objCheckItemVO.m_strRef_Value_Range = objRow["REF_VALUE_RANGE_VCHR"].ToString().Trim();
            objCheckItemVO.m_strWbcode = objRow["WBCODE_CHR"].ToString().Trim();
            objCheckItemVO.m_strAssist_Code01 = objRow["ASSIST_CODE01_CHR"].ToString().Trim();
            objCheckItemVO.m_strAssist_Code02 = objRow["ASSIST_CODE02_CHR"].ToString().Trim();
            objCheckItemVO.m_strIs_No_Food_Required = objRow["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim();
            objCheckItemVO.m_strIs_Physical_Exam_Required = objRow["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim();
            objCheckItemVO.m_strIs_Reservation_Required = objRow["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim();
            objCheckItemVO.m_strSample_Valid_Time = objRow["SAMPLE_VALID_TIME_DEC"].ToString().Trim();
            objCheckItemVO.m_strSample_Valid_Time_Unit = objRow["SAMPLE_VALID_TIME_UNIT_CHR"].ToString().Trim();
            objCheckItemVO.m_strModify_Dat = objRow["MODIFY_DAT"].ToString().Trim();
            objCheckItemVO.m_strOperator_ID = objRow["OPERATORID_CHR"].ToString().Trim();
            objCheckItemVO.m_strCheck_Category_ID = objRow["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
            objCheckItemVO.m_strRef_Value_Max = objRow["REF_MAX_VAL_VCHR"].ToString().Trim();
            objCheckItemVO.m_strRef_Value_Min = objRow["REF_MIN_VAL_VCHR"].ToString().Trim();
            objCheckItemVO.m_strSampleTypeID = objRow["SAMPLETYPE_VCHR"].ToString().Trim();
            objCheckItemVO.m_strFormula_User_VCHR = objRow["FORMULA_USER_VCHR"].ToString().Trim();
            objCheckItemVO.m_strItemprice_mny = objRow["itemprice_mny"].ToString().Trim();
        }
        #endregion

        #region ���ݼ���������Ʒ����ѯ���еļ�����Ŀ
        [AutoComplete]
        public long m_lngQryCheckItemByCheckCategoryAndSampleType( string strCheckCategory, string strSampleType, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            string strSQL = @"SELECT distinct t1.rptno_chr, t1.pycode_chr, t1.unit_chr, t1.check_item_name_vchr,
                                   t1.is_sex_related_chr, t1.check_item_english_name_vchr,
                                   t1.is_age_related_chr, t1.is_sample_related_chr, t1.formula_vchr,
                                   t1.test_methods_vchr, t1.clinic_meaning_vchr, t1.check_item_id_chr,
                                   t1.shortname_chr, t1.is_qc_required_chr, t1.resulttype_chr,
                                   t1.ref_value_range_vchr, t1.wbcode_chr, t1.assist_code01_chr,
                                   t1.assist_code02_chr, t1.is_no_food_required_chr,
                                   t1.is_physical_exam_required_chr, t1.is_reservation_required_chr,
                                   t1.sample_valid_time_dec, t1.sample_valid_time_unit_chr, t1.modify_dat,
                                   t1.operatorid_chr, t1.check_category_id_chr, t1.ref_max_val_vchr,
                                   t1.ref_min_val_vchr, t1.sampletype_vchr, t1.is_menses_related_chr,
                                   t1.is_calculated_chr, t1.formula_user_vchr, t1.alarm_low_val_vchr,
                                   t1.alarm_up_val_vchr, t1.alert_value_range_vchr, 
                                     t2.check_category_desc_vchr
								FROM t_bse_lis_check_item t1,
									 t_bse_lis_check_category t2,
									 t_aid_lis_sampletype t3
							   WHERE t1.check_category_id_chr = t2.check_category_id_chr(+)
								 AND t1.sampletype_vchr = t3.sample_type_id_chr
								 AND t1.sampletype_vchr = '" + strSampleType + @"'
								 AND t1.check_category_id_chr = '" + strCheckCategory + @"'
							ORDER BY t1.check_item_id_chr";
            dtbCheckItem = null;
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

        #region ��ѯ���еĴ�ӡ�����Ϣ
        [AutoComplete]
        public long m_lngGetAllPrintCategory( out DataTable dtbPrintCategory)
        {
            long lngRes = 0;
            string strSQL = @"SELECT print_category_id_chr, print_category_desc_vchr FROM t_bse_lis_print_category";
            dtbPrintCategory = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbPrintCategory);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ�����еļ������
        [AutoComplete]
        public long m_lngGetAllCheckCategory( out System.Data.DataTable dtbCheckCategory)
        {
            long lngRes = 0;
            string strSQL = @"SELECT CHECK_CATEGORY_ID_CHR,CHECK_CATEGORY_DESC_VCHR FROM T_BSE_LIS_CHECK_CATEGORY";
            dtbCheckCategory = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckCategory);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ�����еļ������
        /// <summary>
        /// ��ѯ�����еļ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckCategory( out clsCheckCategory_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckCategory_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT CHECK_CATEGORY_ID_CHR,CHECK_CATEGORY_DESC_VCHR FROM T_BSE_LIS_CHECK_CATEGORY";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsCheckCategory_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsCheckCategory_VO();
                        p_objResultArr[i1].m_strCheck_Category_ID = dtbResult.Rows[i1]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Category_Name = dtbResult.Rows[i1]["CHECK_CATEGORY_DESC_VCHR"].ToString().Trim();

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

        #region ��ѯ����������ĳһ�������ļ�����Ŀ
        [AutoComplete]
        public long m_lngGetAllCheckItemByCheckCategory( string strCheckCategoryID, out System.Data.DataTable dtbAllCheckItem)
        {
            long lngRes = 0;
            string strSQL = @"SELECT rptno_chr, pycode_chr, unit_chr, check_item_name_vchr,
									is_sex_related_chr, check_item_english_name_vchr, is_age_related_chr,
									is_sample_related_chr, formula_vchr, test_methods_vchr,
									clinic_meaning_vchr, check_item_id_chr, shortname_chr,
									is_qc_required_chr, resulttype_chr, ref_value_range_vchr, wbcode_chr,
									assist_code01_chr, assist_code02_chr, is_no_food_required_chr,
									is_physical_exam_required_chr, is_reservation_required_chr,
									sample_valid_time_dec, sample_valid_time_unit_chr,
									check_category_id_chr,FORMULA_USER_VCHR
								FROM t_bse_lis_check_item
								WHERE check_category_id_chr = '" + strCheckCategoryID + @"'
								ORDER BY check_item_id_chr";
            dtbAllCheckItem = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbAllCheckItem);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ����һ�� Check_Item_ID ��ѯ��ÿ��Check_Item_ID ����ϸ����
        [AutoComplete]
        /// <summary>
        /// ����һ�� Check_Item_ID ��ѯ��ÿ��Check_Item_ID ����ϸ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemIDArr">
        /// ��Ϊ���򷵻�-1��dtbCheckItemsInfoΪ��
        /// �糤��Ϊ0�������е�CheckItem������</param>
        /// <param name="dtbCheckItemsInfo"></param>
        /// <returns></returns>

        public long m_lngGetCheckItemInfoByCheckItemID( string[] p_strCheckItemIDArr, out System.Data.DataTable dtbCheckItemsInfo)
        {
            long lngRes = 0;
            dtbCheckItemsInfo = null;
            string strCondition = null;

            #region ���ݲ��� p_strCheckItemIDArr ����ѯ�������򷵻�
            if (p_strCheckItemIDArr == null)
            {//��������ȷ
                return -1;
            }
            else if (p_strCheckItemIDArr.Length == 0)
            {//��ѯ����CheckItem
                strCondition = "";
            }
            else
            {//���� p_strCheckItemIDArr �����ѯ����
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < p_strCheckItemIDArr.Length; i++)
                {
                    sb.Append("'");
                    sb.Append(p_strCheckItemIDArr[i]);
                    sb.Append("',");
                }
                sb.Remove(sb.Length - 1, 1);
                string strCheckItemIDSet = sb.ToString();
                strCondition = " WHERE CHECK_ITEM_ID_CHR IN(" + strCheckItemIDSet + ")";
            }
            #endregion

            string strSQL = @"SELECT *
								FROM t_bse_lis_check_item " + strCondition;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckItemsInfo);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯĳһ������(������)�����ļ�����Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemByNoSubCheckGroupID( string strCheckGroupID, out DataTable dtbCheckGroupItem)
        {
            long lngRes = 0;
            string strSQL = @"SELECT t1.groupid_chr, t2.check_item_id_chr,t2.check_item_name_vchr,t2.check_item_english_name_vchr
								FROM t_aid_lis_check_group_detail t1, t_bse_lis_check_item t2
							  WHERE t1.check_item_id_chr = t2.check_item_id_chr AND groupid_chr= '" + strCheckGroupID + "'";
            dtbCheckGroupItem = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckGroupItem);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯĳһ������(������)�����ļ�����Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemByNoSubCheckGroupID( string strCheckGroupID, out clsCheckItem_VO[] p_objResultArr)
        {
            long lngRes = 0;
            string strSQL = @"SELECT t1.groupid_chr, t2.* 
								FROM t_aid_lis_check_group_detail t1, t_bse_lis_check_item t2
							  WHERE t1.check_item_id_chr = t2.check_item_id_chr AND groupid_chr= '" + strCheckGroupID + "'";
            DataTable dtbResult = null;
            p_objResultArr = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsCheckItem_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        p_objResultArr[i1] = new clsCheckItem_VO();
                        p_objResultArr[i1].m_strCheck_Item_ID = dtbResult.Rows[i1]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRPTNO = dtbResult.Rows[i1]["RPTNO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPycode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUnit = dtbResult.Rows[i1]["UNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_Name = dtbResult.Rows[i1]["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Sex_Related = dtbResult.Rows[i1]["IS_SEX_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_English_Name = dtbResult.Rows[i1]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Age_Related = dtbResult.Rows[i1]["IS_AGE_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Sample_Related = dtbResult.Rows[i1]["IS_SAMPLE_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFormula = dtbResult.Rows[i1]["FORMULA_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTest_Method = dtbResult.Rows[i1]["TEST_METHODS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strClinic_meaning = dtbResult.Rows[i1]["CLINIC_MEANING_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strShortName = dtbResult.Rows[i1]["SHORTNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Qc_Required = dtbResult.Rows[i1]["IS_QC_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strResultType = dtbResult.Rows[i1]["RESULTTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRef_Value_Range = dtbResult.Rows[i1]["REF_VALUE_RANGE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWbcode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAssist_Code01 = dtbResult.Rows[i1]["ASSIST_CODE01_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAssist_Code02 = dtbResult.Rows[i1]["ASSIST_CODE02_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_No_Food_Required = dtbResult.Rows[i1]["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Physical_Exam_Required = dtbResult.Rows[i1]["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Reservation_Required = dtbResult.Rows[i1]["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSample_Valid_Time = dtbResult.Rows[i1]["SAMPLE_VALID_TIME_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strSample_Valid_Time_Unit = dtbResult.Rows[i1]["SAMPLE_VALID_TIME_UNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strModify_Dat = dtbResult.Rows[i1]["MODIFY_DAT"].ToString().Trim();
                        p_objResultArr[i1].m_strOperator_ID = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Category_ID = dtbResult.Rows[i1]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯĳһ������(������)�����ļ�����Ŀ
        [AutoComplete]
        public long m_lngGetCheckItemByhasSubCheckGroupID( string strCheckGroupID, out DataTable dtbCheckGroupItem)
        {
            long lngRes = 0;
            string strSQL = @"SELECT t3.check_item_name_vchr, t3.check_item_id_chr,
									 t3.check_item_english_name_vchr
								FROM t_aid_lis_check_group t1,
									 t_aid_lis_check_group_detail t2,
									 t_bse_lis_check_item t3
							WHERE t1.groupid_chr IN (SELECT  child_groupid_chr
														FROM t_aid_lis_check_group_relation
														START WITH groupid_chr = '" + strCheckGroupID +
                                                            @"'CONNECT BY PRIOR child_groupid_chr = groupid_chr)
							AND t1.has_subgroup_chr = '0'
							AND t1.groupid_chr = t2.groupid_chr
							AND t2.check_item_id_chr = t3.check_item_id_chr";
            dtbCheckGroupItem = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbCheckGroupItem);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ�����еļ�����Ŀ
        [AutoComplete]
        public long m_lngGetAllCheckItem( out System.Data.DataTable dtbAllCheckItem)
        {
            long lngRes = 0;
            string strSQL = @"SELECT t1.rptno_chr, t1.pycode_chr, t1.unit_chr, t1.check_item_name_vchr,
                                   t1.is_sex_related_chr, t1.check_item_english_name_vchr,
                                   t1.is_age_related_chr, t1.is_sample_related_chr, t1.formula_vchr,
                                   t1.test_methods_vchr, t1.clinic_meaning_vchr, t1.check_item_id_chr,
                                   t1.shortname_chr, t1.is_qc_required_chr, t1.resulttype_chr,
                                   t1.ref_value_range_vchr, t1.wbcode_chr, t1.assist_code01_chr,
                                   t1.assist_code02_chr, t1.is_no_food_required_chr,
                                   t1.is_physical_exam_required_chr, t1.is_reservation_required_chr,
                                   t1.sample_valid_time_dec, t1.sample_valid_time_unit_chr, t1.modify_dat,
                                   t1.operatorid_chr, t1.check_category_id_chr, t1.ref_max_val_vchr,
                                   t1.ref_min_val_vchr, t1.sampletype_vchr, t1.is_menses_related_chr,
                                   t1.is_calculated_chr, t1.formula_user_vchr, t1.alarm_low_val_vchr,
                                   t1.alarm_up_val_vchr, t1.alert_value_range_vchr,
                                     t2.CHECK_CATEGORY_DESC_VCHR 
                                FROM t_bse_lis_check_item t1 LEFT OUTER JOIN t_bse_lis_check_category t2 ON t1.check_category_id_chr = t2.check_category_id_chr order by t1.check_item_id_chr";
            dtbAllCheckItem = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbAllCheckItem);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ�������ļ�����Ŀ
        /// <summary>
        /// ��ѯ�������ļ�����Ŀ
        /// </summary>
        [AutoComplete]
        public long m_lngGetCheckItemByCategoryID(
            string p_strCategoryID,
            out clsCheckItem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckItem_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT *  FROM t_bse_lis_check_item";
            string strCondition = "";
            if (p_strCategoryID.ToString().Trim() != "")
            {
                strCondition = " WHERE " + " check_category_id_chr = '" + p_strCategoryID + "'";
            }
            strSQL += strCondition;
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsCheckItem_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsCheckItem_VO();
                        p_objResultArr[i1].m_strCheck_Item_ID = dtbResult.Rows[i1]["CHECK_ITEM_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRPTNO = dtbResult.Rows[i1]["RPTNO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPycode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUnit = dtbResult.Rows[i1]["UNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_Name = dtbResult.Rows[i1]["CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Sex_Related = dtbResult.Rows[i1]["IS_SEX_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Item_English_Name = dtbResult.Rows[i1]["CHECK_ITEM_ENGLISH_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Age_Related = dtbResult.Rows[i1]["IS_AGE_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Sample_Related = dtbResult.Rows[i1]["IS_SAMPLE_RELATED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strFormula = dtbResult.Rows[i1]["FORMULA_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strTest_Method = dtbResult.Rows[i1]["TEST_METHODS_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strClinic_meaning = dtbResult.Rows[i1]["CLINIC_MEANING_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strShortName = dtbResult.Rows[i1]["SHORTNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Qc_Required = dtbResult.Rows[i1]["IS_QC_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strResultType = dtbResult.Rows[i1]["RESULTTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strRef_Value_Range = dtbResult.Rows[i1]["REF_VALUE_RANGE_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWbcode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAssist_Code01 = dtbResult.Rows[i1]["ASSIST_CODE01_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAssist_Code02 = dtbResult.Rows[i1]["ASSIST_CODE02_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_No_Food_Required = dtbResult.Rows[i1]["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Physical_Exam_Required = dtbResult.Rows[i1]["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strIs_Reservation_Required = dtbResult.Rows[i1]["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSample_Valid_Time = dtbResult.Rows[i1]["SAMPLE_VALID_TIME_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strSample_Valid_Time_Unit = dtbResult.Rows[i1]["SAMPLE_VALID_TIME_UNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strModify_Dat = dtbResult.Rows[i1]["MODIFY_DAT"].ToString().Trim();
                        p_objResultArr[i1].m_strOperator_ID = dtbResult.Rows[i1]["OPERATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCheck_Category_ID = dtbResult.Rows[i1]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();

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

        #region ��ѯ�����е���Ʒ���
        [AutoComplete]
        public long m_lngGetAllSampleType( out System.Data.DataTable dtbAllSampleType)
        {
            long lngRes = 0;
            string strSQL = @"select sample_type_id_chr, sample_type_desc_vchr, pycode_chr, wbcode_chr,
       stdcode1_chr, stdcode2_chr, hasbarcode_int from t_aid_lis_sampletype order by sample_type_id_chr";
            dtbAllSampleType = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbAllSampleType);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ����check_item_id��ѯ�������ڸü�����Ŀ�Ĳο�ֵ��Χ(������Ĭ�ϲο�ֵ)
        [AutoComplete]
        public long m_lngGetItemRefByCheckItemID( string checkItemID, out System.Data.DataTable dtbItemRef)
        {
            long lngRes = 0;
            string strSQL = @"SELECT t1.*
								FROM t_bse_lis_itemref t1
							   WHERE t1.check_item_id_chr = '" + checkItemID + "'";
            dtbItemRef = null;

            DataTable dtbCheckItem = null;
            try
            {
                lngRes = m_lngGetCheckItemInfoByCheckItemID( checkItemID, out dtbCheckItem);
                if (lngRes > 0 && dtbCheckItem != null)
                {
                    if (dtbCheckItem.Rows.Count > 0)
                    {
                        if (dtbCheckItem.Rows[0]["IS_MENSES_RELATED_CHR"].ToString().Trim() == "1")
                        {
                            strSQL = @"SELECT t1.*, t2.dictname_vchr AS menses_name,t2.dictid_chr
										FROM t_bse_lis_itemref t1, t_aid_dict t2
									   WHERE t1.menses_id_chr = t2.dictid_chr(+)
										 AND t2.dictkind_chr = '63'
										 AND t1.check_item_id_chr = '" + checkItemID + "'";
                        }
                    }
                }
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbItemRef);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ����check_item_id��ѯ�ü�����Ŀ��Ĭ�ϲο�ֵ��Χ
        [AutoComplete]
        public long m_lngGetDefaultRefByCheckItemID( string checkItemID, out System.Data.DataTable dtbItemDefaultRef)
        {
            long lngRes = 0;
            string strSQL = @"SELECT CHECK_ITEM_ID_CHR, REF_MAX_VAL_VCHR, REF_MIN_VAL_VCHR, REF_VALUE_RANGE_VCHR FROM t_bse_lis_check_item WHERE CHECK_ITEM_ID_CHR = '" + checkItemID + "'";
            dtbItemDefaultRef = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbItemDefaultRef);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                throw objEx;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡȫ��������Ŀ�ο�ֵ��Χ
        /// <summary>
        /// ��ȡȫ��������Ŀ�ο�ֵ��Χ
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetItemRef()
        {
            DataTable dtRef = new DataTable();
            string Sql = @"select a.check_item_id_chr,
                                   a.seq_int,
                                   a.from_age_dec,
                                   a.to_age_dec,
                                   a.sex_vchr,
                                   a.crvalmin,
                                   a.crvalmax,
                                   b.is_sex_related_chr,
                                   b.is_age_related_chr
                              from t_bse_lis_itemref a
                             inner join t_bse_lis_check_item b
                                on a.check_item_id_chr = b.check_item_id_chr";
            clsHRPTableService svc = new clsHRPTableService();
            svc.lngGetDataTableWithoutParameters(Sql, ref dtRef);
            svc.Dispose();
            return dtRef;
        }
        #endregion

        #region ��ȡ������Ŀ��������
        /// <summary>
        /// ��ȡ������Ŀ��������
        /// </summary>
        /// <param name="checkItemId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetCriticalValueRefLisDept(string checkItemId)
        {
            DataTable dtDept = null;
            string Sql = @"select check_item_id_chr, seq_int, deptid_chr, deptname_vchr from t_criticalvalue_ref_lisdept {0}";
            clsHRPTableService svc = new clsHRPTableService();
            if (string.IsNullOrEmpty(checkItemId))
                Sql = string.Format(Sql, "");
            else
                Sql = string.Format(Sql, "where check_item_id_chr = '" + checkItemId + "'");
            svc.lngGetDataTableWithoutParameters(Sql, ref dtDept);
            if (dtDept == null) dtDept = new DataTable();
            dtDept.TableName = "lisDept";
            return dtDept;
        }
        #endregion
    }
}
