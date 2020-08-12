using System;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ������Ʋ�
    /// Create by kong 2004-06-16
    /// </summary>
    public class clsDomainControlStorageFin : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainControlStorageFin()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ���ʼ�����  ŷ����ΰ  2004-06-16

        #region ��ѯ���ʵ���
        /// <summary>
        /// ��ѯ���ʵ���
        /// </summary>
        /// <param name="p_strPeriodID">�����ڴ���</param>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_blnFlag">��˱�־��true��δ��ˣ�false�������</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>
        public long m_lngSelectAcct(string p_strPeriodID, string p_strStorageID, bool p_blnFlag, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngSelectAcct(p_strStorageID, p_strPeriodID, p_blnFlag, out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_objItem">���������</param>
        /// <returns></returns>
        public long m_lngAcct(clsStorageOrd_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAcct(p_objItem);

            return lngRes;
        }
        #endregion

        #region �������ʺ��������
        /// <summary>
        /// ������ʺ��������
        /// </summary>
        /// <param name="p_strID">���ݺ�</param>
        /// <param name="p_strTypeID">�������ͺ�</param>
        /// <param name="p_intFlag">���ر�ʶ��1���ɹ�  0��ʧ��  -1���쳣</param>
        /// <returns></returns>
        public long m_lngChgFinAfterOrdAcct(string p_strID, string p_strTypeID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChgFinAfterOrdAcct(p_strID, p_strTypeID, out p_intFlag);


            return lngRes;
        }
        #endregion

        #region �̵㵥����
        /// <summary>
        /// �̵㵥����
        /// </summary>
        /// <param name="p_objItem">�̵㵥����</param>
        /// <returns></returns>
        public long m_lngAcct(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAcct(p_objItem);


            return lngRes;
        }
        #endregion

        #region �̵㵥���ʺ��������
        /// <summary>
        /// �̵㵥���ʺ��������
        /// </summary>
        ///	<param name="p_strID">�̵㵥��</param>
        /// <param name="p_intFlag">���ر�ʶ��1���ɹ���0��ʧ�ܣ�-1���쳣</param>
        /// <returns></returns>
        public long m_lngChgFinAfterCheckAcct(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChgFinAfterCheckAcct(p_strID, out p_intFlag);


            return lngRes;
        }
        #endregion

        #region ���۵�����
        /// <summary>
        /// ���۵�����
        /// </summary>
        /// <param name="p_objItem">���۵�����</param>
        /// <returns></returns>
        public long m_lngAcct(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAcct(p_objItem);


            return lngRes;
        }
        #endregion

        #region ���۵��ʺ��������
        /// <summary>
        /// ���۵��ʺ��������
        /// </summary>
        /// <param name="p_strNo">���۵���</param>
        /// <param name="p_intFlag">���ر�ʶ��1���ɹ�  0��ʧ��  -1���쳣</param>
        /// <returns></returns>
        public long m_lngChgFinAfterChangePriceAcct(string p_strNo, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChgFinAfterChangePriceAcct(p_strNo, out p_intFlag);


            return lngRes;
        }
        #endregion
        #endregion

        #region �����ѯ  ŷ����ΰ  2004-06-16

        #region �ⷿҩƷ��ϸ��

        #region ģ�����ҿⷿҩƷ��ϸ��
        /// <summary>
        /// ģ����ѯ�ⷿҩƷ��ϸ��
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByAny(string p_strSQL, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���ⷿ��ѯ�ⷿҩƷ��ϸ��
        /// <summary>
        /// ���ⷿ��ѯ�ⷿҩƷ��ϸ��
        /// </summary>
        /// <param name="p_strID">�ֿ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByStorage(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯ�ⷿҩƷ��ϸ��
        /// <summary>
        /// �������ڲ�ѯ�ⷿҩƷ��ϸ��
        /// </summary>
        /// <param name="p_strID">�����ڴ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByPeriod(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByPeriod(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯ�ⷿҩƷ��ϸ��
        /// <summary>
        /// ���������Ͳ�ѯ�ⷿҩƷ��ϸ��
        /// </summary>
        /// <param name="p_strID">�������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByType(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯ�ⷿҩƷ��ϸ��
        /// <summary>
        /// ��ҩƷ��ѯ�ⷿҩƷ��ϸ��
        /// </summary>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByMedicine(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ʱ��β�ѯ�ⷿҩƷ��ϸ��
        /// <summary>
        /// ������ʱ��β�ѯ�ⷿҩƷ��ϸ��
        /// </summary>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByDate(string p_strStartDate, string p_strEndDate, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByDate(p_strStartDate, p_strEndDate, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region �ⷿҩƷ�½���

        #region ģ����ѯ�ⷿҩƷ�½���
        /// <summary>
        /// ģ����ѯ�ⷿҩƷ�½���
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByAny(string p_strSQL, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���ֿ��ѯ�ⷿҩƷ�½���
        /// <summary>
        /// ���ֿ��ѯ�ⷿҩƷ�½���
        /// </summary>
        /// <param name="p_strID">�ֿ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByStorage(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯ�ⷿҩƷ�½���
        /// <summary>
        /// �������ڲ�ѯ�ⷿҩƷ�½���
        /// </summary>
        /// <param name="p_strID">�����ڴ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByPeriod(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByPeriod(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯ�ⷿҩƷ�½���
        /// <summary>
        /// ���������Ͳ�ѯ�ⷿҩƷ�½���
        /// </summary>
        /// <param name="p_strID">�������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByType(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯ�ⷿҩƷ�½���
        /// <summary>
        /// ��ҩƷ��ѯ�ⷿҩƷ�½���
        /// </summary>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByMedicine(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region �ⷿҩƷ����

        #region ģ����ѯ�ⷿҩƷ����
        /// <summary>
        /// ģ����ѯ�ⷿҩƷ����
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByAny(string p_strSQL, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���ⷿ��ѯ�ⷿҩƷ����
        /// <summary>
        /// ���ⷿ��ѯ�ⷿҩƷ����
        /// </summary>
        /// <param name="p_strID">�ⷿ�ʺ�</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByStorage(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯ�ⷿҩƷ����
        /// <summary>
        /// ���������Ͳ�ѯ�ⷿҩƷ����
        /// </summary>
        /// <param name="p_strID">�������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByType(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯ�ⷿҩƷ����
        /// <summary>
        /// ��ҩƷ��ѯ�ⷿҩƷ����
        /// </summary>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByMedicine(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region �ⷿ�½���

        #region ģ����ѯ�ⷿ�½���
        /// <summary>
        /// ģ����ѯ�ⷿ�½���
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByAny(string p_strSQL, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���ⷿ��ѯ�ⷿ�½���
        /// <summary>
        /// ���ⷿ��ѯ�ⷿ�½���
        /// </summary>
        /// <param name="p_strID">�ⷿ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByStorage(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯ�ⷿ�½���
        /// <summary>
        /// �������ڲ�ѯ�ⷿ�½���
        /// </summary>
        /// <param name="p_strID">�����ڴ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByPeriod(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByPeriod(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯ�ⷿ�½���
        /// <summary>
        /// ���������Ͳ�ѯ�ⷿ�½���
        /// </summary>
        /// <param name="p_strID">�������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByType(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region �ⷿ����

        #region ģ����ѯ�ⷿ����
        /// <summary>
        /// ģ����ѯ�ⷿ����
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageFinByAny(string p_strSQL, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���ⷿ��ѯ�ⷿ����
        /// <summary>
        /// ���ⷿ��ѯ�ⷿ����
        /// </summary>
        /// <param name="p_strID">�ⷿ����</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageFinByStorage(string p_strID, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ�ѯ�ⷿ����
        /// <summary>
        /// ���������Ͳ�ѯ�ⷿ����
        /// </summary>
        /// <param name="p_strID">�������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngFindStorageFinByType(string p_strID, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #endregion

        #region ��ϵͳ�ķ���

        #region ��ó���⼰����δ���˵�����
        /// <summary>
        /// ��ó���⼰����δ���˵�����
        /// </summary>
        /// <param name="MedStorageArr"></param>
        /// <param name="MedStorageChangArr"></param>
        /// <returns></returns>
        public long m_lngGetMedStorageUnAcct(out DataTable MedStorageArr, out DataTable MedStorageChangArr)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStorageUnAcct(out MedStorageArr, out MedStorageChangArr);
            return lngRes;
        }
        #endregion


        #region ���ݵ���ID��õ�����ϸ
        /// <summary>
        /// ���ݵ���ID��õ�����ϸ
        /// </summary>
        /// <param name="command">1,�����ID��2������ID</param>
        /// <param name="strID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetDeById(int command, string strID, out DataTable dtbResult)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetDeById(command, strID, out dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #endregion
    }
}
