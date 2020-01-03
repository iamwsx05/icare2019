using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.HIS
{
    class clsDcl_Multiunit_drug : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDcl_Multiunit_drug()
        {

        }
        #endregion

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP01();
            }
        }
        #endregion 

        #region ��ȡҩƷ�б�
        /// <summary>
        /// ��ȡҩƷ�б�
        /// </summary>
        /// <param name="p_dtMedicineList"></param>
        /// <returns></returns>
        public long m_lngGetTableMedicineList(out DataTable p_dtMedicineList)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            long lngRes = proxy.Service.m_lngGetTableMedicineList(out p_dtMedicineList);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ��λ�б�
        /// <summary>
        /// ��ȡ��λ�б�
        /// </summary>
        /// <param name="p_strId"></param>
        /// <param name="p_intBy"></param>
        /// <param name="p_dtAliasList"></param>
        /// <returns></returns>
        public long m_lngGetTableMultiUnitList(string p_strId, out DataTable p_dtMultiUnit)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            long lngRes = proxy.Service.m_lngGetTableMultiUnitList(p_strId, out p_dtMultiUnit);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��������Ϣ

        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="p_objVO"></param>
        /// <returns></returns>
        public long m_lngDeleteMultiUnit(clsMultiunit_drug_VO p_objVO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            long lngRes = proxy.Service.m_lngDeleteMultiUnit(p_objVO);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������ѯ��λ
        /// <summary>
        /// ����������ѯ��λ
        /// </summary>
        /// <param name="strSeledMedId">ҩƷID </param>
        /// <param name="p_strUnit">��λ����</param>
        /// <param name="p_intPackage_Dec">��λ����</param>
        /// <param name="p_CurruseFlag_Int">�Ƿ�ǰ��λ���</param>
        /// <returns></returns>
        public bool m_blnQueryByIndex(string strSeledMedId, string p_strUnit, int p_intPackage_Dec, int p_CurruseFlag_Int)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            bool lngRes = proxy.Service.m_blnQueryByIndex(strSeledMedId, p_strUnit, p_intPackage_Dec, p_CurruseFlag_Int);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������ѯ�Ƿ�Ϊ��ǰʹ�õ�λ
        /// <summary>
        /// ����������ѯ�Ƿ�Ϊ��ǰʹ�õ�λ
        /// </summary>
        /// <param name="strSeledMedId">ҩƷID </param>
        /// <param name="p_strUnit">��λ����</param>
        /// <param name="p_intPackage_Dec">��λ����</param>
        /// <returns></returns>
        public bool m_blnQueryByIndex(string strSeledMedId, string p_strUnit, int p_intPackage_Dec)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            bool lngRes = proxy.Service.m_blnQueryByIndex(strSeledMedId, p_strUnit, p_intPackage_Dec);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ӵ�λ��Ϣ
        /// <summary>
        /// ��ӵ�λ��Ϣ
        /// </summary>
        /// <param name="p_objVO"></param>
        /// <returns></returns>
        public long m_lngAddMultiUnit(clsMultiunit_drug_VO p_objVO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            long lngRes = proxy.Service.m_lngAddMultiUnit(p_objVO);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion


        #region ���µ�λ��Ϣ
        /// <summary>
        /// ���µ�λ��Ϣ
        /// </summary>
        /// <param name="p_objVO"></param>
        /// <param name="p_strMedicineId"></param>
        /// <param name="p_strUnitName"></param>
        /// <param name="p_intPackAge"></param>
        /// <param name="p_intCurruseFlag"></param>
        /// <returns></returns>
        public long m_lngUpdateMultiUnit(clsMultiunit_drug_VO p_objVO, string p_strMedicineId, string p_strUnitName, int p_intPackAge, int p_intCurruseFlag, int intStatus)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            long lngRes = proxy.Service.m_lngUpdateMultiUnit(p_objVO, p_strMedicineId, p_strUnitName, p_intPackAge, p_intCurruseFlag, intStatus);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion

        #region �����е�λ��Ϊ�ǵ�ǰ��λ
        /// <summary>
        /// �����е�λ��Ϊ�ǵ�ǰ��λ
        /// </summary>
        /// <param name="p_strMedicineId"></param>
        /// <returns></returns>
        public long m_lngSetAllCurruseFlag_0ByItemId(string p_strMedicineId)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc m_objService =
            //    (com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType
            //        (typeof(com.digitalwave.iCare.middletier.HIS.clsMultiunit_drug_Svc));
            long lngRes = proxy.Service.m_lngSetAllCurruseFlag_0ByItemId(p_strMedicineId);
            //m_objService.Dispose();
            return lngRes;
        }
        #endregion
    }
}
