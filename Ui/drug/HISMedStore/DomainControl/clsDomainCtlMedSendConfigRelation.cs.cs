using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS;//HISMedStore_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �����
    ///��ҩ��������ҩ���ڵĹ�ϵ ��ժҪ˵����
    ///  Create by xgpeng 2006-02-15
    /// </summary>
    public class clsDomainCtlMedSendConfigRelation : clsDomainController_Base
    {
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public clsDomainCtlMedSendConfigRelation()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ���ҩ����Ϣ(����)
        /// <summary>
        /// ���ҩ����Ϣ(����)
        /// </summary>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreInfo(out DataTable p_dtable)
        {
            long lngRes = 0;
            //clsMedSendConfigRelation_SVC objSvc =
            //    (clsMedSendConfigRelation_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedSendConfigRelation_SVC));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreInfo(out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ��ID��ȡҩ��������Ϣ 
        /// <summary>
        /// ����ҩ��ID��ȡҩ��������Ϣ 
        /// </summary>
        /// <param name="p_TypeID">����ID</param>
        /// <param name="flage">0-��ҩ���� ; 1-��ҩ����</param>
        /// <param name="p_objResArr"></param>
        /// <returns></returns>
        public long m_lngGetMedWindowInfo(string p_TypeID, int flage, out clsOPMedStoreWin_VO[] p_objResArr)
        {
            long lngRes = 0;

            //clsMedSendConfigRelation_SVC objSvc =
            //    (clsMedSendConfigRelation_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedSendConfigRelation_SVC));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedWindowInfo(p_TypeID, flage, out p_objResArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ҩ����ID��ȡ��ҩ����->��ҩ���� ��Ϣ   
        /// <summary>
        /// ������ҩ����ID��ȡ��ҩ����->��ҩ���� ��Ϣ   
        /// </summary>
        /// <param name="p_WinID"></param>
        /// <param name="p_objResArr"></param>
        /// <returns></returns>
        public long m_lngGetMedWinByID(string p_WinID, out clsMedSendConfig_VO[] p_objResArr)
        {
            long lngRes = 0;

            //clsMedSendConfigRelation_SVC objSvc =
            //    (clsMedSendConfigRelation_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedSendConfigRelation_SVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedWinByID(p_WinID, out p_objResArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���� ��ҩ����->��ҩ���� ��ϵ
        /// <summary>
        /// ���� ��ҩ����->��ҩ���� ��ϵ
        /// </summary>
        /// <param name="p_intSeq">��ˮ��</param>
        /// <param name="p_objWinArr"></param>
        /// <returns></returns>
        public long m_lngAddMedSendGiveRelation(out int p_intSeq, clsMedSendConfig_VO p_objWinArr)
        {
            long lngRes = 0;

            //clsMedSendConfigRelation_SVC objSvc =
            //    (clsMedSendConfigRelation_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedSendConfigRelation_SVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddMedSendGiveRelation(out p_intSeq, p_objWinArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ�� ��ҩ����->��ҩ���� ��ϵ
        /// <summary>
        /// ɾ�� ��ҩ����->��ҩ���� ��ϵ
        /// </summary>
        /// <param name="p_intID">��ˮ��</param>
        /// <returns></returns>
        public long m_thDelMedSendGiveRelation(int p_intID)
        {
            long lngRes = 0;

            //clsMedSendConfigRelation_SVC objSvc =
            //    (clsMedSendConfigRelation_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedSendConfigRelation_SVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_thDelMedSendGiveRelation(p_intID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���� �����ƶ���¼
        /// <summary>
        /// ���� �����ƶ���¼
        /// </summary>
        /// <param name="p_objWinArr"></param>
        /// <returns></returns>
        public long m_lngUpdateMovRecord(clsMedSendConfig_VO[] p_objWinArr)
        {
            long lngRes = 0;

            //clsMedSendConfigRelation_SVC objSvc =
            //    (clsMedSendConfigRelation_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedSendConfigRelation_SVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdateMovRecord(p_objWinArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


    }
}
