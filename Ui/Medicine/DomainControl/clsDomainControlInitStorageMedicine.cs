using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    #region  ԭʼ���DomainControl
    /// <summary>
    /// ԭʼ���
    /// Create kong by 2004-06-08
    /// </summary>
    public class clsDomainControlInitStorageMedicine : com.digitalwave.GUI_Base.clsDomainController_Base    //GUI_Base.dll
    {
        public clsDomainControlInitStorageMedicine()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ����ԭʼ����¼
        /// </summary>
        /// <param name="p_objItem">ԭʼ�����Ϣ</param>
        /// <returns></returns>
        public long m_lngDoAddNewInitStorageMedicine(clsInitStorageMedicine_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewInitStorageMedicine(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// �޸�ԭʼ����¼
        /// </summary>
        /// <param name="p_objItem">ԭʼ�����Ϣ</param>
        /// <returns></returns>
        public long m_lngDoUpdateInitStorageMedicine(clsInitStorageMedicine_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdInitStorageMedicine(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ɾ��ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ɾ��ԭʼ����¼
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�</param>
        /// <param name="p_strMedicineID">ҩƷ</param>
        /// <returns></returns>
        public long m_lngDoDeleteInitStorageMedicine(string p_strStorageID, string p_strMedicineID)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteInitStorageMedicine(p_strStorageID, p_strMedicineID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ģ������ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ģ������ԭʼ����¼
        /// </summary>
        /// <param name="p_strSQL">SQL�ű�</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        public long m_lngFindInitStorageMedicineByAny(string p_strSQL, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByAny(p_strSQL, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ���ֿ�ID��ҩƷID����ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ���ֿ��ҩƷ����ԭʼ�����Ϣ
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�</param>
        /// <param name="p_strMedicineID">ҩƷ</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        public long m_lngFindInitStorageMedicineByKey(string p_strStorageID, string p_strMedicineID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByKey(p_strStorageID, p_strMedicineID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ���ֿ����ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ���ֿ����ԭʼ����¼
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        public long m_lngFindInitStorageMedicineByStorageID(string p_strStorageID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByStorageID(p_strStorageID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ��ҩƷ����ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ��ҩƷ����ԭʼ����¼
        /// </summary>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        public long m_lngFindInitStorageMedicineByMedicineID(string p_strMedicineID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByMedicineID(p_strMedicineID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ��������ԭʼ����¼  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ��������ԭʼ����¼
        /// </summary>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        public long m_lngFindAllInitStorageMedicine(out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllInitStorageMedicine(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
    #endregion
}
