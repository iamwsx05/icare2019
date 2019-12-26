using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.iCare.common;


namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// �������,���м����ȡ����.
    /// </summary>
    public class clsDcl_MedicineStoreroomSet : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ�ֿ�������Ϣ.
        /// <summary>
        /// ��ȡ�ֿ�������Ϣ.
        /// </summary>
        /// <param name="p_MedicineStoreArr">���ؽ��.</param>
        /// <returns></returns>
        internal long m_lngGetMedicineStoreInfo(out clsMS_MedicineStoreroom_VO[] p_MedicineStoreArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedicineStoreroomInfo(out p_MedicineStoreArr);
            return lngRes;
        }
        #endregion

        #region ��ȡָ���ֿ������õ�ҩƷ����.
        /// <summary>
        /// ��ȡָ���ֿ������õ�ҩƷ����
        /// </summary>
        /// <param name="p_strStoreRoomID">�ֿ�</param>
        /// <param name="p_objType">ҩƷ����</param>
        /// <returns></returns>
        internal long m_lngGetStoreRoomSetCheck(string p_strStoreRoomID, out clsMS_MedicineType_VO[] p_objType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStoreRoomSetCheck(p_strStoreRoomID, out p_objType);
            return lngRes;
        }
        #endregion
        #region ��ȡָ��ҩ�������õ�ҩƷ����ID
        /// <summary>
        ///  ��ȡָ��ҩ�������õ�ҩƷ����ID
        /// </summary>
        /// <param name="p_strStoreRoomID">ҩ��</param>
        /// <param name="p_objType">ҩƷ����</param>
        /// <returns></returns>
        public long m_lngGetMedStoreSetCheck(string p_strStoreRoomID, out clsMS_MedicineType_VO[] p_objType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedStoreSetCheck(p_strStoreRoomID, out p_objType);
            return lngRes;
        }
        #endregion
        #region ��ȡҩƷ������Ϣ.
        /// <summary>
        /// ��ȡҩƷ������Ϣ.
        /// </summary>
        /// <param name="p_dtbMedicine">���ؽ��.</param>
        /// <returns></returns>
        internal long m_lngGetMedicineInfo(out clsMS_MedicineType_VO[] p_objMedicineType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedicineInfo(out p_objMedicineType);
            return lngRes;
        }
        #endregion

        #region ��ӿ���¼.

        /// <summary>
        ///  ��ӿ���¼.
        /// </summary>
        /// <param name="p_objMedicineStoreArr">�����ⷿ��¼.</param>
        /// <returns></returns>
        internal long m_lngInsertMedicineStoreInfo(ref clsMS_MedicineStoreroom_VO p_objMedicineStore)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngAddNewMedicineStoreInfo(ref p_objMedicineStore);
            return lngRes;
        }
        #endregion
        #region ��ӿⷿ��¼.

        /// <summary>
        /// ��ӿⷿ��¼.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedicineStoreroom">�����ⷿ��¼.</param>
        /// <returns></returns>
        public long m_lngAddNewMedStoreSetInfo(ref clsMS_MedicineStoreroom_VO p_objMedicineStoreroom)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngAddNewMedStoreSetInfo(ref p_objMedicineStoreroom);
            return lngRes;
        }
        #endregion
        #region ɾ���ⷿ��¼.

        /// <summary>
        /// ɾ���ⷿ��¼.
        /// </summary>
        /// <param name="strStoreID">Ҫɾ���Ŀⷿ��¼�ĿⷿID.</param>
        /// <returns></returns>
        internal long m_lngDeleteMedicineStoreInfo(string strStoreID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngDeleteMedicineStoreInfo(strStoreID);
            return lngRes;
        }
        #endregion
        #region ɾ���ⷿ��¼.

        /// <summary>
        /// ɾ���ⷿ��¼.
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strStoreID">ָ��Ҫɾ���Ŀⷿ��¼.</param>
        /// <returns></returns>
        public long m_lngDeleteMedStoreSetInfo(string strStoreID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSetSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngDeleteMedStoreSetInfo(strStoreID);
            return lngRes;
        }
        #endregion
        #region ��ѯ�ⷿҩƷ��Ϣ.

        /// <summary>
        /// ��ѯ�ⷿҩƷ��Ϣ.
        /// </summary>
        /// <param name="strStoreID">��ѯ����.</param>
        /// <param name="strMedicineNameArr">���ؽ��.</param>
        /// <returns></returns>
        internal long m_lngSelectMedicineName(string strStoreID, out string[] strMedicineNameArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedicineStoreroomSet_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngSelectMedicineName(strStoreID, out strMedicineNameArr);
            return lngRes;
        }
        #endregion

        #region ��ȡ����ҩ��������Ϣ��
        /// <summary>
        ///  ��ȡ����ҩ��������Ϣ��
        /// </summary>
        /// <param name="m_dtMedStore"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreInfo(out DataTable m_dtMedStore)
        {

            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetMedStoreInfo(out m_dtMedStore);
            return lngRes;

        }
        #endregion

        #region ��ȡ���һ�����Ϣ��
        /// <summary>
        /// ��ȡ����ҩ��������Ϣ��
        /// </summary>
        /// <param name="m_dtDeptDesc"></param>
        /// <returns></returns>
        public long m_lngGetDeptInfo(out DataTable m_dtDeptDesc)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetDeptInfo(out m_dtDeptDesc);
            return lngRes;

        }
        #endregion
    }
}
