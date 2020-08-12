using System;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlMedicineStorageCheck ��ժҪ˵����
    /// </summary>
    public class clsDomainControlMedicineStorageCheck : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlMedicineStorageCheck()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ������еĲֿ���Ϣ
        /// <summary>
        /// ������еĲֿ���Ϣ
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetStorage(string p_strStorageFlag, out DataTable dtbResult)
        {
            //long lngRes = 0;
            //dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            //lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArr( out dtbResult);
            //return lngRes;
            long lngRes = 0;
    //        com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
    //(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArr2(p_strStorageFlag, out dtbResult);

            return lngRes;

        }
        #endregion

        #region ��ȡ�ֿ����������̵㵥��
        /// <summary>
        /// ��ȡ�ֿ����������̵㵥��
        /// </summary>
        /// <param name="dtbResult"></param>		
        /// <returns></returns>
        public long m_lngGetCheckBill(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByAny(out dtbResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ�̵㵥�������ϸ
        public long m_lngGetCheckDetail(string strStorageID, string strStorageFlag, string strCheckBillID, out System.Data.DataTable dtbResult, string str1, bool isEm)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetail(strCheckBillID, out dtbResult, str1, isEm);
            return lngRes;
        }
        #endregion ��ȡ�̵㵥�������ϸ

        #region ��ȡ��ǰ�����ϸ����
        /// <summary>
        /// ��ȡ��ǰ�����ϸ����
        /// </summary>
        /// <param name="strStorageID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="strFlag"></param>
        /// <returns></returns>
        public long m_lngGetStorageCheckDetail(string strStorageID, out System.Data.DataTable dtbResult, string strFlag, clsHISMedType_VO[] medType, clsMedicinePrepType_VO[] PrepType, clsMedicineType_VO[] MedicineType, bool isShowZero, bool isShowStop)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageCheckDetail(strStorageID, out dtbResult, strFlag, medType, PrepType, MedicineType, isShowZero, isShowStop);
            return lngRes;
        }
        #endregion ��ȡ�̵㵥�������ϸ

        #region ������е�ҩƷ����
        /// <summary>
        /// ������е�ҩƷ����
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedinicePrepType(out DataTable dtbResult, out DataTable dtbResult1, out DataTable dtbResult2)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedinicePrepType(out dtbResult, out dtbResult1, out dtbResult2);
            return lngRes;
        }
        #endregion

        #region �����̵��¼��
        /// <summary>
        /// �����̵��¼��
        /// </summary>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageCheck(ref clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageCheck(p_objItem);
            return lngRes;

        }
        #endregion �����̵��¼��

        #region �����̵㵥��ɾ���̵㵥
        /// <summary>
        /// �����̵㵥��ɾ���̵㵥
        /// </summary>
        /// <param name="strCheckBillID">�̵㵥��</param>
        /// <returns></returns>
        public long m_lngDelCheckBill(string strCheckBillID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageCheck(strCheckBillID);
            return lngRes;

        }
        #endregion �����̵㵥��ɾ���̵㵥

        #region �̵㵥���
        public long m_lngAuditCheckBill(clsStorageCheckDetail_VO[] p_objItem, string strAuditorID, string strAuditDate, string p_strStorageFlag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAuditCheckBill(p_objItem, strAuditorID, strAuditDate, p_strStorageFlag);
            return lngRes;
        }
        #endregion

        #region �ϲ��̵㵥
        /// <summary>
        /// �ϲ��̵㵥
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="p_objItem"></param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        public long m_lngUnionData(System.Collections.Generic.List<string> objList, clsStorageCheck_VO p_objItem, out DataTable dtCheckOut)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngUnionData(objList, p_objItem, out dtCheckOut);
            return lngRes;
        }
        #endregion

        #region �����̵���ϸ
        public long m_lngDoAddNewStorageCheckDetail(clsStorageCheckDetail_VO[] p_objItem, string strRemark)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageChekDetail(p_objItem, strRemark);
            return lngRes;
        }
        #endregion �����̵���ϸ

        #region ��ȡ��������
        public long m_lngGetPeriod(out System.Data.DataTable dt)
        {
            long lngRes = 0;
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriod(out dt);
            return lngRes;
        }
        #endregion ��ȡ��������
    }
}
