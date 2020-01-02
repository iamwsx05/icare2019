using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public class clsDcl_MedicineAcceptance : com.digitalwave.GUI_Base.clsDomainController_Base
    {
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

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_strStoreRoomID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼ����</param>
        /// <param name="p_dtmEnd">��������</param>
        /// <param name="dtbValue">DataTable</param>
        /// <returns></returns>
        internal long m_lngGetAcceptance(string p_strStoreRoomID, DateTime p_dtmBegin, DateTime p_dtmEnd, out DataTable dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetAcceptance(p_strStoreRoomID, p_dtmBegin, p_dtmEnd, out dtbValue);
            return lngRes;
        }


        internal long m_lngGetAcceptance_Med(string p_strStoreRoomID, DateTime p_dtmBegin, DateTime p_dtmEnd, out DataTable dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetAcceptance_Med(p_strStoreRoomID, p_dtmBegin, p_dtmEnd, out dtbValue);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ��ϸ����
        /// </summary>
        /// <param name="p_strStoreRoomID">�ֿ�ID</param>
        /// <param name="p_dtmBegin">��ʼ����</param>
        /// <param name="p_dtmEnd">��������</param>
        /// <param name="dtbValue">DataTable</param>
        /// <returns></returns>
        internal long m_lngGetAcceptanceDetal(string p_strStoreRoomID, DateTime p_dtmBegin, DateTime p_dtmEnd, out DataTable dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetAcceptanceDetal(p_strStoreRoomID, p_dtmBegin, p_dtmEnd, out dtbValue);
            return lngRes;
        }

        internal long m_lngGetAcceptanceDetal_Med(string p_strStoreRoomID, DateTime p_dtmBegin, DateTime p_dtmEnd, out DataTable dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineAcceptanceSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetAcceptanceDetal_Med(p_strStoreRoomID, p_dtmBegin, p_dtmEnd, out dtbValue);
            return lngRes;
        }


    }
}
