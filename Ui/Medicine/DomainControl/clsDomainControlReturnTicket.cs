using System;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlReturnTicket ��ժҪ˵����
    /// </summary>
    public class clsDomainControlReturnTicket : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlReturnTicket()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���ݴ��ڻ�ü���ҩ����
        /// <summary>
        /// ���ݴ��ڻ�ü���ҩ����
        /// </summary>
        /// <param name="p_objResult">��������</param>
        /// <returns></returns>
        public long m_ingGetOutDataByWindowID(string p_strID, out clsOutPatienTrecipEinv_VO[] p_objResult, DateTime date)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_ingGetOutDataByWindowID(p_strID, out p_objResult, date);
            return lngRes;
        }
        #endregion


        #region ��ѯ����ҩ����Ϣ
        /// <summary>
        /// ��ѯ����ҩ����Ϣ
        /// </summary>
        /// <param name="p_objResultArr">��������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreList(out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ����ѯ���еĴ���
        /// <summary>
        /// ����ҩ����ѯ���еĴ���
        /// </summary>
        /// <param name="mediD"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinList(string mediD, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStoreWinList(mediD, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ͨ������IDȡ��ǰ��Ҫ��ҩ�Ĵ�����¼
        /// <summary>
        /// ͨ������ID������ID����������ȡ��ǰ��Ҫ��ҩ�Ĵ�����¼
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strOPRecID">����ID</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetOPRecipeListByWinAndOpRecAndType(string p_strOPRecID, string typeID, out clsOprecipeItemDe[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetOPRecipeListByWinAndOpRecAndType(p_strOPRecID, typeID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region  ��ò��˴�����״̬��
        /// <summary>
        /// ��ò��˴�����״̬��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strRegID">�Һ�ID</param>
        /// <param name="p_intStatus">״̬</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMainRecipe(string p_strRegID, out clsOutpatientRecipe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMainRecipe(p_strRegID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����ﴦ����Ʊ���״̬��Ϊ����Ʊ��
        public long m_lngChangStatus(string p_Invoiceno)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChangStatus(p_Invoiceno);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����ﴦ�����״̬��Ϊ����ҩ��
        public long m_lngReturn(string p_Invoiceno)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngReturn(p_Invoiceno);
            return lngRes;
        }
        #endregion

        #region  ������ҩ����ҩ��������ϸ(��ҩ)
        public long m_lngAddNewPwmreciPeDe(clsOutPaticntPwmreciPeDe_VO p_objRecord)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAddNewPwmreciPeDe(p_objRecord);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ������ҩ������ϸ(��ҩ)
        public long m_lngAddNewCmreciPeDe(clsOutPaticntCmreciPeDe_VO p_objRecord)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAddNewCmreciPeDe(p_objRecord);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ������ҩ������ϸ(��ҩ)
        public long m_ingfFidData(string p_strID, out System.Data.DataTable p_objResultArr)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReturnTicketSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_ingfFidData(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


    }
}
