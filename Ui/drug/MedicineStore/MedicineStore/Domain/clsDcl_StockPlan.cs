using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    public class clsDcl_StockPlan : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ��ǰԱ���Ƿ���ҩ������ɫ
        /// <summary>
        /// ��ȡ��ǰԱ���Ƿ���ҩ������ɫ
        /// </summary>
        /// <param name="p_strEmpID">Ա��ID</param>
        /// <param name="p_blnHasRole">�Ƿ���ҩ������ɫ</param>
        /// <returns></returns>
        internal long m_lngCheckEmpHasRole(string p_strEmpID, out bool p_blnHasRole)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.common.clsCommonInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.common.clsCommonInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.common.clsCommonInfoSvc));
            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngCheckEmpHasRole(p_strEmpID, "ҩ�����", out p_blnHasRole);
            return lngRes;
        }
        #endregion

        #region ��ȡ�ֿ���
        /// <summary>
        /// ��ȡ�ֿ���
        /// </summary>
        /// <param name="p_strStoreRoomID">�ֿ�ID</param>
        /// <param name="p_strStoreRoomName">�ֿ���</param>
        /// <returns></returns>
        internal long m_lngGetStoreRoomName(string p_strStoreRoomID, out string p_strStoreRoomName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStoreRoomName(p_strStoreRoomID, out p_strStoreRoomName);
            return lngRes;
        }
        #endregion


        #region ɾ��ָ��������Ϣ
        /// <summary>
        /// ɾ��ָ��������Ϣ
        /// </summary>
        /// <param name="p_lngSeriesID">��������</param>
        /// <returns></returns>
        internal long m_lngDeleteMainStockPlan(long p_lngSeriesID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngDeleteMainStockPlan(p_lngSeriesID);
            return lngRes;
        }
        /// <summary>
        /// ɾ��ָ��������Ϣ
        /// </summary>
        /// <param name="p_lngSeriesID">��������</param>
        /// <returns></returns>
        internal long m_lngDeleteMainStockPlan(long[] p_lngSeriesID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngDeleteMainStockPlan(p_lngSeriesID);
            return lngRes;
        }
        #endregion

        #region ��ȡ��ϸ������
        /// <summary>
        /// ��ȡ��ϸ������
        /// </summary>
        /// <param name="p_lngSeries2ID">��������</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        internal long m_lngGetStockPlanDetail(long p_lngSeries2ID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStockPlanDetail(p_lngSeries2ID, out p_dtbValue);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ������뵥
        /// </summary>
        /// <param name="lngSEQ">��������</param>
        /// <param name="m_strExamer">�����</param>
        /// <param name="m_datExamDate">���ʱ��</param>
        /// <returns></returns>
        internal long m_lngCommitStockPlan(long lngSEQ, string m_strExamer, DateTime m_datExamDate)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngCommitStockPlan(lngSEQ, m_strExamer, m_datExamDate);
            return lngRes;
        }

        internal long m_lngUnCommit(long[] lngSEQArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngUnCommit(lngSEQArr);
            return lngRes;
        }

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_dtmBeginDate">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEndDate">��ѯ����ʱ��</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strMedicineName">ҩƷ����</param>
        /// <param name="p_strVendorName">��Ӧ������</param>
        /// <param name="p_strStockPlanID">���ݺ�</param>
        /// <param name="p_strMedicinePreptype">ҩƷ����</param>
        /// <param name="p_dtbValue">��������</param>
        /// <returns></returns>
        internal long m_lngGetStockPlan(DateTime p_dtmBeginDate, DateTime p_dtmEndDate, string p_strStorageID,
            string p_strMedicineName, string p_strVendorName, string p_strStockPlanID, string p_strMedicinePreptype, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStockPlan(p_dtmBeginDate, p_dtmEndDate, p_strStorageID, p_strMedicineName, p_strVendorName, p_strStockPlanID, p_strMedicinePreptype, out p_dtbValue);
            return lngRes;
        }
        #endregion

        #region ��ȡָ���ֿ��ҩƷ����
        /// <summary>
        /// ��ȡָ���ֿ��ҩƷ����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objMTVO">ҩƷ�Ƽ�����</param>
        /// <returns></returns>
        internal long m_lngGetStorageMedicineType(string p_strStorageID, out clsMS_MedicineType_VO[] p_objMTVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStorageMedicineType(p_strStorageID, out p_objMTVO);
            return lngRes;
        }
        #endregion

        #region ��ȡ��ϸ������
        /// <summary>
        /// ��ȡ��ϸ������
        /// </summary>
        /// <param name="p_lngSeries2ID">��ϸ��������</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        internal long m_mthGetStockPlanDetail(long p_lngSeries2ID, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStockPlanDetail(p_lngSeries2ID, out p_dtbValue);
            return lngRes;
        }
        #endregion
    }
}
