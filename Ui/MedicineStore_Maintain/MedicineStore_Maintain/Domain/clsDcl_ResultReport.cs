using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public class clsDcl_ResultReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ�̵�������Ϣ
        /// <summary>
        /// ��ȡ�̵�������Ϣ
        /// </summary>
        /// <param name="p_strStarDate">��ѯ��ʼʱ��</param>
        /// <param name="p_strEndDate">��ѯ����ʱ��</param>
        /// <param name="p_strStorage">�ֿ�ID</param>
        /// <param name="dtbStorageCheck">��������</param>
        /// <returns></returns>
        internal long m_lngGetStorageCheck(DateTime p_strStarDate, DateTime p_strEndDate, string p_strStorage, out DataTable dtbStorageCheck)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageCheckSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetStorageCheck(p_strStarDate, p_strEndDate, p_strStorage, out dtbStorageCheck);
            return lngRes;
        }
        #endregion

        #region ��ȡ�̵���ϸ����Ϣ

        /// <summary>
        /// ��ȡ�̵���ϸ����Ϣ
        /// </summary>
        /// <param name="p_lngSeriesId">�������к�</param>
        /// <param name="dtbStorageCheck_detail">��ϸ����Ϣ</param>
        /// <returns></returns>
        internal long m_lngGetStorageCheck_detail(long p_lngSeriesId, out DataTable dtbStorageCheck_detail)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStorageCheckSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStorageCheck_detail(p_lngSeriesId, out dtbStorageCheck_detail);
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
    }
}
