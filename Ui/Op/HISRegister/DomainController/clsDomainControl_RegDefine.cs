using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_RegDefine ��ժҪ˵����
    /// </summary>
    public class clsDomainControl_RegDefine : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_RegDefine()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP02 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP02();
            }
        }
        #endregion

        // �Һ�����
        #region ��ѯ�Һ����� created by Cameron Wong on Aug 9, 2004
        public long m_lngFindRegType(out clsRegType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngFindRegTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����Һ����� created by Cameron Wong on Aug 9, 2004
        public long m_lngAddRegType(clsRegType_VO p_objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoAddNewRegType(p_objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸ĹҺ����� created by Cameron Wong on Aug 9, 2004
        public long m_lngDoUpdRegByID(clsRegType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdRegTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ͣ�ùҺ����� created by Cameron Wong on Aug 9, 2004
        public long m_lngIsUseing(string p_strID, string p_Isusering)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngIsUseing(p_strID, p_Isusering);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ���Һ����� created by Cameron Wong on Aug 9, 2004
        public long m_lngDelRegByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDelRegTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        // ���Ʒ���
        #region ��ѯ�Һ����� created by Cameron Wong on Aug 9, 2004
        public long m_lngFindCookMethodList(out clsCookMethod_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngFindCookMethodList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������Ʒ��� created by Cameron Wong on Aug 11, 2004
        public long m_lngAddCookMethod(string strName, string strMNemonic, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoAddNewCookMethod(strName, strMNemonic, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸ļ��Ʒ��� created by Cameron Wong on Aug 11, 2004
        public long m_lngDoUpdMethodByID(clsCookMethod_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdMethodByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ�����Ʒ��� created by Cameron Wong on Aug 11, 2004
        public long m_lngDelMethodByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDelMethodByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        //�Һŷ���
        #region ��ȡ�Һŷ����б�	�Ź���		2004-8-8
        public long m_lngFindType(out clsRegchargeType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngFindRegChargeTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����Һŷ���	�Ź���		2004-8-8
        public long m_lngAddType(clsRegchargeType_VO objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngAddNewRegChargeType(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸ĹҺŷ���	�Ź���		2004-8-8
        public long m_lngDoUpdTypeByID(clsRegchargeType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdRegChargeTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ͣ�ùҺŷ���	�Ź���		2004-9-22
        public long m_lngIsUseingRgechargeType(string p_strID, string p_Isusering)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));

            lngRes = proxy.Service.m_lngIsUseingRgechargeType(p_strID, p_Isusering);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ���Һŷ���	�Ź���		2004-8-8
        public long m_lngDelTypeByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDeleteRegChargeTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        //�Һ����
        #region ��ȡ�Һ�����б�	�Ź���		2004-8-9
        public long m_lngFindPatientPayTypeList(out clstPatientPaytype_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngFindPatientPayTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����Һ����	�Ź���		2004-8-9
        public long m_lngAddPatientPayType(clstPatientPaytype_VO objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngAddNewPatientPayType(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸ĹҺ����	�Ź���		2004-8-9
        public long m_lngDoUpdPatientPayTypeByID(clstPatientPaytype_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdPatientPayTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ͣ�ùҺ����	�Ź���		2004-9-22
        public long m_lngIsUseingPAYTYPE(string p_strID, string p_Isusering)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngIsUseingPAYTYPE(p_strID, p_Isusering);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ���Һ����	�Ź���		2004-8-9
        public long m_lngDelTPatientPayTypeByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDeletePatientPayTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���ռƻ��б�	�Ź���		2004-9-24
        /// <summary>
        /// ��ȡ���ռƻ��б�
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetINSPLANDataArr(out clsInsPlan_VO[] p_objResultArr)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetINSPLANDataArr(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

    }
}
