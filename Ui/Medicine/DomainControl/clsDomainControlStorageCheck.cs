using System;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll

using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �̵����
    /// Create by kong 2004-06-14
    /// </summary>
    public class clsDomainControlStorageCheck : com.digitalwave.GUI_Base.clsDomainController_Base   //GUI_Base.dll
    {
        #region clsDomainControlStorageCheck
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainControlStorageCheck()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region �����̵��¼��
        /// <summary>
        /// �����̵��¼��
        /// </summary>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageCheck(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageCheck(p_objItem);

            return lngRes;

        }
        #endregion

        #region �޸��̵��¼��
        /// <summary>
        /// �޸��̵��¼��
        /// </summary>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        public long m_lngDoUpdateStorageCheck(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStorageCheck(p_objItem);

            return lngRes;

        }
        #endregion

        #region ɾ���̵��¼��
        /// <summary>
        /// ɾ���̵��¼��
        /// </summary>
        /// <param name="p_strID">�̵��¼����</param>
        /// <returns></returns>
        public long m_lngDoDeleteStorageCheck(string p_strID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageCheck(p_strID);

            return lngRes;

        }
        #endregion

        #region �����̵���ϸ��
        /// <summary>
        /// �����̵���ϸ��
        /// </summary>
        /// <param name="p_objItem">�̵���ϸ������</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageChekDetail(clsStorageCheckDetail_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageChekDetail(p_objItem);

            return lngRes;

        }
        #endregion

        #region �޸��̵���ϸ��
        /// <summary>
        /// �޸��̵���ϸ��
        /// </summary>
        /// <param name="p_objItem">�̵���ϸ������</param>
        /// <returns></returns>
        public long m_lngDoUpdateStorageChekDetail(clsStorageCheckDetail_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStorageChekDetail(p_objItem);

            return lngRes;

        }
        #endregion

        #region ɾ���̵���ϸ��
        /// <summary>
        /// ɾ���̵���ϸ��
        /// </summary>
        /// <param name="p_strID">�̵���ϸ����</param>
        /// <returns></returns>
        public long m_lngDoDeleteStorageChekDetail(string p_strID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageChekDetail(p_strID);

            return lngRes;

        }
        #endregion

        #region ����̵㵥
        /// <summary>
        /// ����̵㵥
        /// </summary>
        /// <param name="p_objItem">�̵��¼������</param>
        /// <returns></returns>
        public long m_lngAduit(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAduit(p_objItem);

            return lngRes;
        }
        #endregion

        #region ����̵㵥����Ŀ��
        /// <summary>
        /// ��˺���Ŀ��
        /// </summary>
        /// <param name="p_strID">�̵㵥��</param>
        /// <param name="p_intFlag">���ر�ʶ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        public long m_lngChangeStorageAfterAduit(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChangeStorageAfterAduit(p_strID, out p_intFlag);

            return lngRes;
        }
        #endregion

        #region ģ�������̵��¼��
        /// <summary>
        /// ģ�������̵��¼��
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByAny(string p_strSQL, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByAny(p_strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region �����Ų����̵��¼��
        /// <summary>
        /// �����Ų����̵��¼��
        /// </summary>
        /// <param name="p_strID">�̵��¼����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByID(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByID(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ���ֿ�����̵��¼��
        /// <summary>
        /// ���ֿ�����̵��¼��
        /// </summary>
        /// <param name="p_strID">�ֿ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByStorage(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByStorage(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ���������Ͳ����̵��¼��
        /// <summary>
        /// ���������Ͳ����̵��¼��
        /// </summary>
        /// <param name="p_strID">�������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByType(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByType(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region �������ڲ����̵��¼��
        /// <summary>
        /// �������ڲ����̵��¼��
        /// </summary>
        /// <param name="p_strID">�����ڴ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByPeriod(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByPeriod(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ��ʱ��β����̵��¼��
        /// <summary>
        /// ��ʱ��β����̵��¼��
        /// </summary>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByDate(string p_strStartDate, string p_strEndDate, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByDate(p_strStartDate, p_strEndDate, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region �������е��̵��¼��
        /// <summary>
        /// �������е��̵��¼��
        /// </summary>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindAllStorageCheck(out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllStorageCheck(out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region ģ�������̵���ϸ��
        /// <summary>
        /// ģ�������̵���ϸ��
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckDetailByAny(string p_strSQL, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetailByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���̵㵥�Ų����̵���ϸ��
        /// <summary>
        /// ���̵㵥�Ų����̵���ϸ��
        /// </summary>
        /// <param name="p_strID">�̵��¼����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckDetailByID(string p_strID, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetailByCheckID(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ�����̵���ϸ��
        /// <summary>
        /// ��ҩƷ�����̵���ϸ��
        /// </summary>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckDetailByMedicine(string p_strID, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetailByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��õ�ǰ�����̵��¼����
        /// <summary>
        ///  ��õ�ǰ�����̵��¼����
        /// </summary>
        /// <param name="p_strResult">�������</param>
        /// <returns></returns>
        public long m_lngGetMaxCheckID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxCheckID(out p_strResult);

            return lngRes;
        }
        #endregion

        #region ��õ�ǰ�����̵���ϸ����
        /// <summary>
        /// ��õ�ǰ�����̵���ϸ����
        /// </summary>
        /// <param name="p_strResult">�������</param>
        /// <returns></returns>
        public long m_lngGetMaxDetailID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxDetailID(out p_strResult);

            return lngRes;
        }
        #endregion

        #region ��ϵͳ�ķ���
        #region ������е��̵�����
        /// <summary>
        /// ������е��̵�����
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetStorageDeTail(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageDeTail(out dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ������еĲֿ���Ϣ
        /// <summary>
        /// ������еĲֿ���Ϣ
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetStorage(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetAllStorage(out dtbResult);
            return lngRes;
        }
        #endregion

        #region ������е�ҩƷ����
        /// <summary>
        /// ������е�ҩƷ����
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedinicePrepType(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            DataTable dtbResult1 = null;
            DataTable dtbResult2 = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedinicePrepType(out dtbResult, out dtbResult1, out dtbResult2);
            return lngRes;
        }
        #endregion

        #region �Զ����ɳ���ⵥ
        /// <summary>
        /// �Զ����ɳ���ⵥ
        /// </summary>
        /// <param name="dtStorCheckData"></param>
        /// <returns></returns>
        public long m_lngGetAutoGreat(DataTable dtStorCheckData)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAutoGreat(dtStorCheckData);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ��������̵��������ĵ�������
        /// <summary>
        /// �ж��Ƿ��������̵��������ĵ�������
        /// </summary>
        /// <param name="typeName">�������������</param>
        /// <param name="typeID">���ص�������ID</param>
        /// <returns>2�У�3���Ǹ������ڸ��¿�����в�����</returns>
        public long m_lngisCheckType(string typeName, out string typeID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngisCheckType(typeName, out typeID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion



    }
}
