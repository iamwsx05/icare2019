using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainControl_LisDevice ��ժҪ˵����
    /// Alex 2004-5-6
    /// </summary>
    public class clsDomainController_CheckItemManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        private string strCheckItemID;

        public clsDomainController_CheckItemManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public clsDomainController_CheckItemManage(string p_strCheckItemID)
        {
            strCheckItemID = p_strCheckItemID;
        }

        #region ���ݼ�������ȡ������Ŀ ͯ�� 2004.10.12
        public long m_lngGetCheckItemArrByCheckCategory(string p_strCheckCategory, out DataTable p_dtbResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemArrByCheckCategory(p_strCheckCategory, out p_dtbResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ֵģ��

        #region ����ģ��ID����T_AID_LIS_VALUETEMPLATE_DETAIL��Ĭ�ϱ��
        public long m_lngSetDefaultFlagByTemplateID(string p_strTemplateID, int p_intFlag)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetDefaultFlagByTemplateID(p_strTemplateID, p_intFlag);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ��������������ѯģ����Ϣ ͯ�� 2004.08.10
        public long m_lngGetTemplateInfoByCondition(string p_strCheckCategory, string p_strSampleType, out clsLisValueTemplate_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetTemplateInfoByCondition(p_strCheckCategory, p_strSampleType, out p_objResultArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ģ��ID��ѯ��Ӧ��ģ����ϸ��Ϣ ͯ�� 2004.08.10
        public long m_lngGetTemplateDetailByTemplateID(string p_strTemplateID, out clsLisValueTemplateDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetTemplateDetailByTemplateID(p_strTemplateID, out p_objResultArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ�����ĿID��ѯ��Ӧ��ģ����ϸ��Ϣ ͯ�� 2004.08.11
        public long m_lngGetTemplateDetailByCheckItemID(string p_strCheckItemID, out clsLisValueTemplateDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetTemplateDetailByCheckItemID(p_strCheckItemID, out p_objResultArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������¼����T_AID_LIS_VALUETEMPLATE_ITEM ͯ�� 2004.08.11
        public long m_lngAddNewValueTemplateItem(clsLisValueTemplateItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewValueTemplateItem(p_objRecord);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ����T_AID_LIS_VALUETEMPLATE_ITEM�ļ�¼ ͯ�� 2004.08.11
        public long m_lngDelValueTemplateItem(clsLisValueTemplateItem_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelValueTemplateItem(p_objRecord);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ģ�壨�������ģ��ĸ��ã���p_objOldRecordΪnull)
        public long m_lngReuseTemplate(clsLisValueTemplateItem_VO p_objOldRecord, clsLisValueTemplateItem_VO p_objNewRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngReuseTemplate(p_objOldRecord, p_objNewRecord);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ�����ĿID��ѯ��T_AID_LIS_VALUETEMPLATE_ITEM�ļ�¼ ͯ�� 2004.08.11
        public long m_lngGetValueTemplateItemByCheckItemID(string p_strCheckItemID, out clsLisValueTemplateItem_VO p_objResult)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetValueTemplateItemByCheckItemID(p_strCheckItemID, out p_objResult);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������Ŀ��ģ�弰����ϸ ͯ�� 2004.08.11
        public long m_lngAddNewCheckItemVauleTemplate(clsLisValueTemplate_VO p_objValueTemplate, clsLisValueTemplateItem_VO p_objValueTemplateItem,
            clsLisValueTemplateDetail_VO[] p_objValueTemplateDetailArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewCheckItemVauleTemplate(p_objValueTemplate, p_objValueTemplateItem, p_objValueTemplateDetailArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������ɾ�����޸�ģ����ϸ��Ϣ ͯ�� 2004.08.11
        public long m_lngValueTemplateDetailArr(List<clsLisValueTemplateDetail_VO> p_arlAddNewArr, List<clsLisValueTemplateDetail_VO> p_arlDelArr, List<clsLisValueTemplateDetail_VO> p_arlUpdArr, string p_strTemplateID, string p_strIdx)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngValueTemplateDetailArr(p_arlAddNewArr, p_arlDelArr, p_arlUpdArr, p_strTemplateID, p_strIdx);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ�����ĿID��ѯģ���������Ϣ ͯ�� 2004.08.12
        public long m_lngGetAllTemplateInfoByCheckItemID(string p_strCheckItemID, out clsLisValueTemplateItem_VO p_objTemplateItem,
            out clsLisValueTemplate_VO p_objTemplate,
            out clsLisValueTemplateDetail_VO[] p_objTemplateDetailArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllTemplateInfoByCheckItemID(p_strCheckItemID, out p_objTemplateItem,
               out p_objTemplate, out p_objTemplateDetailArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #endregion

        #region ���¼�����Ŀ�Ͳο�ֵ
        public long m_lngSetCheckItemAndRef(clsCheckItem_VO p_objCheckItem, clsCheckItemRef_VO[] p_objCheckItemRefArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetCheckItemAndRef(p_objCheckItem, p_objCheckItemRefArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��������Ŀ
        public long m_lngDelCheckItemAndRef(string p_strCheckItemID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemAndRef(p_strCheckItemID);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���±�ü�����Ŀ�Ļ�����Ϣ
        public long m_lngSetCheckItemDetailByCheckItemID(clsCheckItem_VO p_objCheckItem)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetCheckItemDetailByCheckItemID(ref p_objCheckItem);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ�������������ͱ걾���ѯ��Ӧ�ļ�����Ŀ ͯ�� 2004.07.28
        public long m_lngQryCheckItemByCheckCategoryAndSampleType(string p_strCheckCategory, string p_strSampleType, string p_strSampleGroup,
            out DataTable dtbAllCheckItem)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngQryCheckItemByCheckCategoryAndSampleType(p_strCheckCategory, p_strSampleType, p_strSampleGroup,
               out dtbAllCheckItem);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ����������������ϲ�ѯ������Ŀ ͯ�� 2004.06.29
        public long m_lngGetCheckItemArrByCondition(string p_strCheckCategoryID, string p_strSampleTypeID, out clsCheckItem_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCheckItemArrByCondition(p_strCheckCategoryID, p_strSampleTypeID, out p_objResultArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����check_item_id��ѯ��Ӧ�ļ�����Ŀ��ϢVO ���� 2004.06.09
        /// <summary>
        /// ����check_item_id��ѯ��Ӧ�ļ�����Ŀ��ϢVO
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemVO"></param>
        /// <returns></returns>
        public long m_lngGetCheckItemVOByCheckItemID(string p_strCheckItemID, out clsCheckItem_VO p_objCheckItemVO)
        {
            long lngRes = 0;
            p_objCheckItemVO = null;
            lngRes = proxy.Service.m_lngGetCheckItemVOByCheckItemID(p_strCheckItemID, out p_objCheckItemVO);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ĳһ������Ŀ�µ����вο�ֵ��Χ ͯ�� 2004.06.08
        public long m_lngDelCheckItemRefByCheckItemID(string strCheckItemID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelCheckItemRef(strCheckItemID);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������Ŀ ͯ�� 2004.06.08
        public long m_lngAddNewCheckItem(ref clsCheckItem_VO objCheckItemVO)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddCheckItem(ref objCheckItemVO);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ĳһ������Ŀ�µĲο�ֵϵ�� ͯ�� 2004.06.08
        public long m_lngAddCheckItemRefList(ref clsCheckItemRef_VO[] objCheckItemRefVOList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddCheckItemRefList(ref objCheckItemRefVOList);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ���������Ʒ����ѯ������Ŀ ͯ�� 2004.05.16
        public long m_lngGetCheckItemByCheckCategoryAndSampleType(string strCheckCategory, string strSampleType, out DataTable dtbAllCheckItem)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngQryCheckItemByCheckCategoryAndSampleType(strCheckCategory, strSampleType, out dtbAllCheckItem);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ�����Ŀ�����ü�����Ŀ
        /// <summary>
        /// ���ݼ�����Ŀ�����ü�����Ŀ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckItemByCategoryID(string p_strCategoryID, out clsCheckItem_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckItem_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetCheckItemByCategoryID(p_strCategoryID, out p_objResultArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���еļ�����Ŀ���
        /// <summary>
        /// ��ѯ���еļ�����Ŀ���
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckCategory(out clsCheckCategory_VO[] p_objResultArr)
        {
            p_objResultArr = new clsCheckCategory_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetCheckCategory(out p_objResultArr);
            //			objCheckItemSvc.Dispose();
            return lngRes;


        }
        #endregion

        #region ��ѯ���еļ�����Ŀ��� 2004.07.15
        public long m_lngGetCheckCategory(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllCheckCategory(out p_dtbResult);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �õ�������Ŀ�Ĳο���Χ ���� 2004.4.22 18:57
        public static clsReferenceValue s_objGetCheckItemRefVal(string p_strCheckItemID, string p_strSex, string p_strAge, string p_strMenses)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            DataTable dtbCheckItemRef = null;
            clsReferenceValue objRefVal = null;
            try
            {
                DataRow dtrRef = null;
                #region �Ӹ��Ӳο�ֵ���ѯ���������Ĳο�ֵ
                clsCheckItemRef_VO objCheckItemRefVO = null;
                lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetCheckItemRefByCondition(p_strAge, p_strSex, p_strMenses, p_strCheckItemID, out objCheckItemRefVO);
                if (lngRes > 0 && objCheckItemRefVO != null)
                {
                    objRefVal = new clsReferenceValue(objCheckItemRefVO.m_strMin_Val, objCheckItemRefVO.m_strMax_Val);
                    objRefVal.m_StrRefRange = objCheckItemRefVO.m_strRef_Val;
                    return objRefVal;
                }
                #endregion
                #region ������Ӳο�ֵ����û�з��������Ĳο�ֵ��õ�Ĭ�ϵĲο�ֵ
                if (dtrRef == null)
                {
                    lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetDefaultRefByCheckItemID(p_strCheckItemID, out dtbCheckItemRef);
                    if (lngRes > 0 && dtbCheckItemRef != null && dtbCheckItemRef.Rows.Count != 0)
                    {
                        dtrRef = dtbCheckItemRef.Rows[0];
                        objRefVal = new clsReferenceValue(
                            dtrRef["REF_MIN_VAL_VCHR"] == System.DBNull.Value ? null : dtrRef["REF_MIN_VAL_VCHR"].ToString().Trim(),
                            dtrRef["REF_MAX_VAL_VCHR"] == System.DBNull.Value ? null : dtrRef["REF_MAX_VAL_VCHR"].ToString().Trim());
                        objRefVal.m_StrRefRange = dtrRef["REF_VALUE_RANGE_VCHR"] == System.DBNull.Value ? null : dtrRef["REF_VALUE_RANGE_VCHR"].ToString().Trim();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                //				try
                //				{
                new com.digitalwave.Utility.clsLogText().LogError(ex);
                //				}
                //				catch{}
                objRefVal = null;
            }
            return objRefVal;
        }
        public clsReferenceValue m_objGetCheckItemRefVal(string p_strSex, string p_strAge, string p_strMenses)
        {
            return clsDomainController_CheckItemManage.s_objGetCheckItemRefVal(this.strCheckItemID, p_strSex, p_strAge, p_strMenses);
        }
        #endregion xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        #region �õ�������Ŀ CheckItem �ĵ�λ�ķ��� ���� 2004.04.24 15:41
        public static string s_strGetCheckItemUnitByCheckItemID(string p_strCheckItemID)
        {
            if (p_strCheckItemID == null)
                return null;
            long lngRes = 0;
            string strUnit = null;
            DataTable dtbCheckItemsInfo = null;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis()).Service.m_lngGetCheckItemInfoByCheckItemID(new string[] { p_strCheckItemID }, out dtbCheckItemsInfo);
                //				objCheckCheckItemSvc.Dispose();
                if (lngRes > 0 && dtbCheckItemsInfo != null && dtbCheckItemsInfo.Rows.Count != 0)
                {
                    strUnit = dtbCheckItemsInfo.Rows[0]["UNIT_CHR"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            return strUnit;
        }
        public string m_strGetCheckItemUnit()
        {
            return s_strGetCheckItemUnitByCheckItemID(this.strCheckItemID);
        }
        #endregion xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
    }
}
