using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// �̵���˱�
    /// </summary>
    public class clsDcl_BalanceReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡʱ��������������
        /// <summary>
        /// ��ȡʱ��������������
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtmQueryDate">ʱ��</param>
        /// <param name="p_strStorageName">������</param>
        /// <returns></returns>
        internal long m_lngGetAccount(string p_strStorageID, DateTime p_dtmQueryDate, out string p_strStorageName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));

            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetAccount(p_strStorageID, p_dtmQueryDate, out p_strStorageName);
            return lngRes;
        }
        #endregion

        #region ��ȡҩ���̵���˱�����
        /// <summary>
        /// ��ȡҩ���̵���˱�����
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        internal long m_lngGetBalance(string p_strStorageID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC));

            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBalance(p_strStorageID, out p_dtbResult);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ȡ���ϴν�תʱ��
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="dtBegin"></param>
        /// <returns></returns>
        internal long m_lngGetLastBalanceTime(string p_strStorageID, out DateTime dtBegin)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC));

            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetLastBalanceTime(p_strStorageID, out dtBegin);
            return lngRes;
        }

        #region ��ȡϵͳ��������
        /// <summary>
        /// ��ȡϵͳ��������
        /// </summary>
        /// <param name="p_strCode">���ô���</param>
        /// <param name="p_strParm">��������</param>
        /// <returns></returns>
        internal long m_lngGetSysParm(string p_strCode, out string p_strParm)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.common.clsCommonInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.common.clsCommonInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.common.clsCommonInfoSvc));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysParm(p_strCode, out p_strParm);
            return lngRes;
        }
        #endregion

        #region ��ȡҩ���̵���˱����ݣ��������ڣ�
        /// <summary>
        /// ��ȡҩ���̵���˱����ݣ��������ڣ�
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        internal long m_lngGetBalanceDetail(string p_strStorageID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC));

            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBalanceDetail(p_strStorageID, out p_dtbResult);
            return lngRes;
        }
        #endregion


        internal long m_lngGetRoomid(out DataTable dtTemp)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsOutStorage_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsOutStorage_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsOutStorage_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetRoomid(out dtTemp);
            return lngRes;
        }

        internal long m_lngGetBalanceDetailForDrugStore(string p_strStorageID, string p_strAccountID, string p_strLastAccountID,
            DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC));

            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBalanceDetailForDrugStore(p_strStorageID, p_strAccountID, p_strLastAccountID, p_dtmStart, p_dtmEnd, out dtbResult);
            return lngRes;
        }

        internal long m_lngGetRecipeDetail(string p_strStorageID, DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC));

            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetRecipeDetail(p_strStorageID, p_dtmStart, p_dtmEnd, out dtbResult);
            return lngRes;
        }

        internal void m_mthGetAccountIDListForDrugStore(string p_strStorageID, out clsMS_AccountPeriodVO[] p_objAccArr)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsBalanceReportSVC));

            (new weCare.Proxy.ProxyDrug()).Service.m_mthGetAccountIDListForDrugStore(p_strStorageID, out p_objAccArr);
        }

        /// <summary>
        /// ȡ�õ�������
        /// </summary>
        /// <param name="p_strType"></param>
        /// <returns></returns>
        public long m_lngGetBillType(bool p_blnForDrugStore, out string p_strType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            if (p_blnForDrugStore)
                lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetSysParaByID("8007", out p_strType);
            else
                lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetSysParaByID("5002", out p_strType);
            return lngRes;
        }
    }
}
