using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    public class clsDcl_StockPlan_Detail : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ�����������
        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <param name="p_intCommitFolw">�����������</param>
        /// <returns></returns>
        internal long m_lngGetCommitFlow(out int p_intCommitFolw)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("0901", out p_intCommitFolw);
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�������Ϣ
        /// <summary>
        /// ��ȡҩƷ�������Ϣ
        /// </summary>
        /// <param name="p_strAssistCode">��ѯ����</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbMedicine">���ؽ��</param>
        /// <returns></returns>
        internal long m_lngGetBaseMedicine(string p_strAssistCode, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetBaseMedicineWithGross(p_strAssistCode, p_strStorageID, out p_dtbMedicine);
            return lngRes;
        }
        #endregion

        #region  ��ȡ��������ת�Ľ�������
        /// <summary>
        ///  ��ȡ��������ת�Ľ�������
        /// </summary>
        /// <returns></returns>
        public long m_mthGetAccountperiodTime(out DateTime datAccountperiodTime)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_mthGetAccountperiodTime(out datAccountperiodTime);
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

        /// <summary>
        /// �����������ϸ������
        /// </summary>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailArr"></param>
        /// <returns></returns>
        public long m_lngAddNewPlanMedInfo(int m_intCommit, ref clsMS_StockPlan_VO m_objMainVo, ref clsMS_StockPlan_Detail_VO[] m_objDetailArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngAddNewPlanMedInfo(m_intCommit, ref m_objMainVo, ref m_objDetailArr);
            return lngRes;
        }

        /// <summary>
        /// �����������ϸ������
        /// </summary>
        /// <param name="m_objMainVo"></param>
        /// <param name="m_objDetailArr"></param>
        /// <returns></returns>
        public long m_lngUpdatePlanMedInfo(int m_intCommit, clsMS_StockPlan_VO m_objMainVo, ref clsMS_StockPlan_Detail_VO[] m_objDetailArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngUpdatePlanMedInfo(m_intCommit, m_objMainVo, ref m_objDetailArr);
            return lngRes;
        }

        /// <summary>
        /// �������к�ɾ��ҩƷid
        /// </summary>
        /// <param name="m_lngSeqid"></param>
        /// <returns></returns>
        public long m_lngDelMedDetail(long m_lngSeqid)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngDelPlanMedDetail(m_lngSeqid);
            return lngRes;
        }

        /// <summary>
        /// �Ƿ������
        /// </summary>
        /// <param name="p_strBillNo"></param>
        /// <param name="p_intStatus"></param>
        internal void m_mthGetCommitStatus(string p_strBillNo, out int p_intStatus)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            (new weCare.Proxy.ProxyDrug02()).Service.m_mthGetCommitStatus(p_strBillNo, out p_intStatus);
        }

        /// <summary>
        /// �Զ�������ɹ�����        
        /// </summary>
        /// <param name="p_strStorageID">ҩ���</param>
        /// <param name="p_strMedicineID">ҩƷ��</param>
        /// <param name="p_intAmount">����</param>
        internal void m_mthGetAmount(string p_strStorageID, string p_strMedicineID, out double p_intAmount)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            (new weCare.Proxy.ProxyDrug02()).Service.m_mthGetAmount(p_strStorageID, p_strMedicineID, out p_intAmount);
        }

        /// <summary>
        /// �Զ�������ɹ�����
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_strMedicineID"></param>
        /// <param name="p_intRealAmount"></param>
        /// <param name="p_intTopAmount"></param>
        /// <param name="p_intNeapAmount"></param>
        internal void m_mthGetAmount(string p_strStorageID, string p_strMedicineID, out double p_intRealAmount, out double p_intTopAmount, out double p_intNeapAmount)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //     (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            (new weCare.Proxy.ProxyDrug02()).Service.m_mthGetAmount(p_strStorageID, p_strMedicineID, out p_intRealAmount, out p_intTopAmount, out p_intNeapAmount);
        }

        #region ��ȡ��ϸ������(��ӡ)
        /// <summary>
        /// ��ȡ��ϸ������(��ӡ)
        /// </summary>
        /// <param name="p_lngSeries2ID">��������</param>
        /// <param name="p_intState">����״̬</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        internal long m_lngGetStockPlanForPrint(long p_lngSeries2ID, int p_intState, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            lngRes = (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStockPlanForPrint(p_lngSeries2ID, p_intState, out p_dtbValue);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ��ȡ��ӡ����0ΪĬ�ϣ�1Ϊ̨ɽ
        /// </summary>
        /// <param name="m_intType">����</param>
        internal void m_lngGetPrintType(out int m_intType)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("5032", out m_intType);
        }

        /// <summary>
        /// ��ȡ��ϸ������(��ӡ)̨ɽ
        /// </summary>
        /// <param name="p_lngSeries2ID">��������</param>
        /// <param name="p_intState">����״̬</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        internal void m_lngGetStockPlanForPrintTaiShan(long p_lngSeries2ID, int p_intState, out DataTable p_dtbValue)
        {
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsStockPlanSVC));
            (new weCare.Proxy.ProxyDrug02()).Service.m_lngGetStockPlanForPrintTaiShan(p_lngSeries2ID, p_intState, out p_dtbValue);
        }
    }
}
