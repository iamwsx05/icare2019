using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS;
using com.digitalwave.GUI_Base;
using weCare.Core.Entity;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ҩ��������Ϣ
    /// Create by kong 2004-07-05
    /// </summary>
    public class clsDomainControlMedStoreBseInfo : clsDomainController_Base
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainControlMedStoreBseInfo()
        {

        }
        #endregion

        #region ҩ����Ϣ
        #region ����ҩ����Ϣ
        /// <summary>
        /// ����ҩ����Ϣ
        /// </summary>
        /// <param name="p_objItem">ҩ������</param>
        /// <returns></returns>
        public long m_lngAddNewMedStore(clsMedStore_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddNewMedStore(p_objItem);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region �޸�ҩ����Ϣ
        /// <summary>
        /// �޸�ҩ����Ϣ
        /// </summary>
        /// <param name="p_objItem">ҩ������</param>
        /// <returns></returns>
        public long m_lngUpdMedStoreByID(clsMedStore_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdMedStoreByID(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ҩ����Ϣ
        /// <summary>
        /// ɾ��ҩ����Ϣ
        /// </summary>
        /// <param name="p_strID">ҩ������</param>
        /// <returns></returns>
        public long m_lngDeleteMedStoreByID(string p_strID)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedStoreByID(p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ����Ϣ
        /// <summary>
        /// ģ����ѯҩ����Ϣ
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreByAny(string p_strSQL, out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreByAny(p_strSQL, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩ�������ѯҩ����Ϣ
        /// <summary>
        /// ��ҩ�������ѯҩ����Ϣ
        /// </summary>
        /// <param name="p_strID">ҩ������</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreByID(string p_strID, out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreByID(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩ�����Ͳ�ѯҩ����Ϣ
        /// <summary>
        /// ��ҩ�����Ͳ�ѯҩ����Ϣ
        /// </summary>
        /// <param name="p_intID">ҩ�����ʹ��룬1������ҩ����2��סԺҩ�� 3��ȫԺҩ��</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreByStoreType(int p_intID, out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreByStoreType(p_intID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩƷ���Ͳ�ѯҩ����Ϣ
        /// <summary>
        /// ��ҩƷ���Ͳ�ѯҩ����Ϣ
        /// </summary>
        /// <param name="p_intID">ҩƷ���ͣ�1����ҩ��2����ҩ</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreByMedicineType(int p_intID, out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreByMedicineType(p_intID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ����ҩ����Ϣ
        /// <summary>
        /// ��ѯ����ҩ����Ϣ
        /// </summary>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreList(out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������ҩ������
        /// <summary>
        /// �������ҩ������
        /// </summary>
        /// <param name="p_strID">ҩ������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreID(out p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion

        #region ������Ϣ
        #region ����ҩ������
        /// <summary>
        /// ����ҩ������
        /// </summary>
        /// <param name="p_objItem">ҩ����������</param>
        /// <returns></returns>
        public long m_lngAddNewMedStoreWin(clsOPMedStoreWin_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddNewMedStoreWin(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�ҩ������
        /// <summary>
        /// �޸�ҩ������
        /// </summary>
        /// <param name="p_objItem">��������</param>
        /// <returns></returns>
        public long m_lngUpdMedStoreWin(clsOPMedStoreWin_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdMedStoreWin(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ҩ������
        /// <summary>
        /// ɾ��ҩ������
        /// </summary>
        /// <param name="p_strID">ҩ�����ڴ���</param>
        /// <returns></returns>
        public long m_lngDeleteMedStoreWin(string p_strID)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedStoreWin(p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ��������Ϣ
        /// <summary>
        /// ģ����ѯҩ��������Ϣ
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinByAny(string p_strSQL, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinByAny(p_strSQL, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����ںŲ�ѯ������Ϣ
        /// <summary>
        /// �����ںŲ�ѯ������Ϣ
        /// </summary>
        /// <param name="p_strID">���ں�</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinByID(string p_strID, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinByID(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩ����ѯ����
        /// <summary>
        /// ��ҩ����ѯ����
        /// </summary>
        /// <param name="p_strID">ҩ������</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinByMedStoreID(string p_strID, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinByMedStoreID(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������ѯ����
        /// <summary>
        /// ��������ѯ����
        /// </summary>
        /// <param name="p_Type">��������</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinList(int p_Type, out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinList(p_Type, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯ���еĴ���
        /// <summary>
        /// ��ѯ���еĴ���
        /// </summary>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinList(out clsOPMedStoreWin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPMedStoreWin_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �õ���ǰ���Ĵ��ں�
        /// <summary>
        /// �õ���ǰ���Ĵ��ں�
        /// </summary>
        /// <param name="p_strID">���ں�</param>
        /// <returns></returns>
        public long m_lngGetMedStoreWinID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinID(out p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion

        #region ҩ���޶�
        #region ����ҩ���޶�
        /// <summary>
        /// ����ҩ���޶�
        /// </summary>
        /// <param name="p_objItem">ҩ���޶�����</param>
        /// <returns></returns>
        public long m_lngAddNewMedStoreLimit(DataTable dtRow)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddNewMedStoreLimit(dtRow);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ҩƷ����
        /// <summary>
        /// ���ҩƷ����
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetMed(out DataTable dtResult)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMed(out dtResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�ҩ���޶�
        /// <summary>
        /// �޸�ҩ���޶�
        /// </summary>
        /// <param name="p_objItem">ҩ���޶�����</param>
        /// <returns></returns>
        public long m_lngUpdMedStoreLimitByID(DataTable dtRow)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdMedStoreLimitByID(dtRow);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ҩ���޶�
        /// <summary>
        /// ɾ��ҩ���޶�
        /// </summary>
        /// <param name="p_strMedStoreID">ҩ������</param>
        /// <param name="p_strMedicineID">ҩƷ����</param>
        /// <returns></returns>
        public long m_lngDeleteMedStoreLimitByID(string p_strMedStoreID, string p_strMedicineID)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedStoreLimitByID(p_strMedStoreID, p_strMedicineID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ҩ���޶�
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long m_lngCheckMedStoreLimit()
        {
            long lngRes = 0;
            return lngRes;

        }
        #endregion

        #region ģ����ѯҩ���޶�
        /// <summary>
        /// ģ����ѯҩ���޶�
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreLimitByAny(string p_strSQL, out clsMedStoreLimit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreLimit_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreLimitByAny(p_strSQL, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩ����ѯҩ���޶�
        /// <summary>
        /// ��ҩ����ѯҩ���޶�
        /// </summary>
        /// <param name="p_strID">ҩ������</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreLimitByMedStore(string p_strID, out clsMedStoreLimit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreLimit_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreLimitByMedStore(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ҩ����ѯҩ���޶�(�£�
        /// <summary>
        /// ��ҩ����ѯҩ���޶�(�£�
        /// </summary>
        /// <param name="winid"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreLimitByAnyWinID(string winid, out DataTable p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new DataTable();
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreLimitByAnyWinID(winid, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #endregion

        #region ҩ����������
        #region ����ҩ����������
        /// <summary>
        /// ����ҩ����������
        /// </summary>
        /// <param name="p_objItem">ҩ��������������</param>
        /// <returns></returns>
        public long m_lngAddNewMedStoreOrdType(clsMedStoreOrdType_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddNewMedStoreOrdType(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�ҩ����������
        /// <summary>
        /// �޸�ҩ����������
        /// </summary>
        /// <param name="p_objItem">ҩ��������������</param>
        /// <returns></returns>
        public long m_lngUpdMedStoreOrdTypeByID(clsMedStoreOrdType_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdMedStoreOrdTypeByID(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ҩ����������
        /// <summary>
        /// ɾ��ҩ����������
        /// </summary>
        /// <param name="p_strID">ҩ���������ʹ���</param>
        /// <returns></returns>
        public long m_lngDeleteMedStoreOrdType(string p_strID)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedStoreOrdType(p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ģ����ѯҩ����������
        /// <summary>
        /// ģ����ѯҩ����������
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreOrdTypeByAny(string p_strSQL, out clsMedStoreOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdType_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreOrdTypeByAny(p_strSQL, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩ���������ʹ����ѯ
        /// <summary>
        /// ��ҩ���������ʹ����ѯ
        /// </summary>
        /// <param name="p_strID">ҩ���������ʹ���</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreOrdTypeByID(string p_strID, out clsMedStoreOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdType_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreOrdTypeByID(p_strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����־��ѯҩ����������
        /// <summary>
        /// ����־��ѯҩ����������
        /// </summary>
        /// <param name="p_intSign">��־</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreOrdTypeBySign(int p_intSign, out clsMedStoreOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdType_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreOrdTypeBySign(p_intSign, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���е�ҩ����������
        /// <summary>
        /// ��ѯ���е�ҩ����������
        /// </summary>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        public long m_lngGetMedStoreOrdTypeList(out clsMedStoreOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStoreOrdType_VO[0];

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreOrdTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ��ǰ����ҩ����������ID
        /// <summary>
        /// ��ȡ��ǰ����ҩ����������ID
        /// </summary>
        /// <param name="p_strID">ҩ����������ID</param>
        /// <returns></returns>
        public long m_lngGetMedStoreOrdTypeID(out string p_strID)
        {
            long lngRes = 0;
            p_strID = null;

            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreOrdTypeID(out p_strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #endregion


        #region ����ҩ����Ϣ  xg.peng 2006-2-9
        /// <summary>
        /// ����ҩ����Ϣ
        /// </summary>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreInfo(out DataTable p_dtable)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.clsMedStoreBseInfoSvc_m_lngGetMedStoreInfo(out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ����ҩ��ID��ȡҩ���Ű���Ϣ
        /// <summary>
        /// ����ҩ��ID��ȡҩ���Ű���Ϣ
        /// </summary>
        /// <param name="p_TypeID"></param>
        /// <param name="p_objResArr"></param>
        /// <returns></returns>
        public long m_lngGetDeptDutyInfo(string p_TypeID, out clsMedDeptDuty_VO[] p_objResArr)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetDeptDutyInfo(p_TypeID, out p_objResArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ���Ű���Ϣ
        /// <summary>
        /// ����ҩ���Ű���Ϣ
        /// </summary>
        /// <param name="p_intSeq"></param>
        /// <param name="p_objDuty"></param>
        /// <returns></returns>
        public long m_lngAddDeptDutyInfo(out int p_intSeq, clsMedDeptDuty_VO p_objDuty)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddDeptDutyInfo(out p_intSeq, p_objDuty);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���� �޸��Ű���Ϣ
        /// <summary>
        /// ���� �޸��Ű���Ϣ
        /// </summary>
        /// <param name="p_objWorkDuty"></param>
        /// <returns></returns>
        public long m_thUpdateDeptDutyInfo(clsMedDeptDuty_VO p_objWorkDuty)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_thUpdateDeptDutyInfo(p_objWorkDuty);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ɾ���Ű���Ϣ
        /// <summary>
        /// ɾ���Ű���Ϣ
        /// </summary>
        /// <param name="p_intID"></param>
        /// <returns></returns>
        public long m_thDelDeptDutyInfo(int p_intID)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_thDelDeptDutyInfo(p_intID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ҩ��ר�ô�������Ҷ�Ӧ������
        /// <summary>
        /// ҩ��ר�ô�������Ҷ�Ӧ������
        /// </summary>
        public long m_lngGetMedStoreWinDeptDefInfo(string p_strMedStoreId, string p_strWindowId, out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = null;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreWinDeptDefInfo(p_strMedStoreId, p_strWindowId, out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ����ҩ��ר�ô�������Ҷ�Ӧ
        /// </summary>
        public long m_lngInsertMEDSTOREWINDEPT(clsMEDSTOREWINDEPTDEF_VO[] p_VO)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngInsertMEDSTOREWINDEPT(p_VO);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ɾ��ҩ��ר�ô�������Ҷ�Ӧ
        /// </summary>
        public long m_lngDeleteMEDSTOREWINDEPT(clsMEDSTOREWINDEPTDEF_VO[] p_VO)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMEDSTOREWINDEPT(p_VO);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ȡ������
        /// <summary>
        /// ȡ������
        /// </summary>
        public long m_lngGeDataTableInfo(string p_sql, out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = null;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGeDataTableInfo(p_sql, out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ����ҩ�����ͻ��ҩ����Ϣ(����)
        /// <summary>
        /// ����ҩ�����ͻ��ҩ����Ϣ(����)
        /// </summary>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreInfoByMedStoreType(out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = null;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreInfoByMedStoreType(out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡȫ��������Ϣ
        /// <summary>
        /// ��ȡȫ��������Ϣ
        /// </summary>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        public long m_lngGetAreaInformation(out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = null;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAreaInformation(out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��������ҩ��id��ȡ��Ӧ����������Ϣ
        /// <summary>
        /// ��������ҩ��id��ȡ��Ӧ����������Ϣ
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        public long m_lngGetAreaInformationByMedStoreID(string m_strMedStoreID, out DataTable p_dtable)
        {

            long lngRes = 0;
            p_dtable = null;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAreaInformationByMedStoreID(m_strMedStoreID, out p_dtable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��������ҩ��id������Ӧ����������Ϣ
        /// <summary>
        /// ��������ҩ��id������Ӧ����������Ϣ
        /// </summary>
        /// <param name="m_objData"></param>
        /// <returns></returns>
        public long m_lngInsertMedStoreAreaRelation(clsMedStoreVsArea m_objData)
        {
            long lngRes = 0;

            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngInsertMedStoreAreaRelation(m_objData);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        //#region ����ҩ��id�Ͳ���idɾ������ҩ����Ӧ�����ļ�¼
        // /// <summary>
        ///// ����ҩ��id�Ͳ���idɾ������ҩ����Ӧ�����ļ�¼
        // /// </summary>
        // /// <param name="m_strMedStoreId"></param>
        // /// <param name="m_strAreaId"></param>
        // /// <returns></returns>
        //public long m_lngDelMedStoreVsAreaInfoByID(string m_strMedStoreId, string m_strAreaId)
        //{
        //    long lngRes = 0;
        //    clsMedStoreBseInfoSvc objSvc =
        //       (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
        //    lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDelMedStoreVsAreaInfoByID( m_strMedStoreId, m_strAreaId);
        //    objSvc.Dispose();
        //    return lngRes;

        //}
        //#endregion 
        #region ����ҩ��id�Ͳ���id��������ҩ����Ӧ�����ļ�¼
        /// <summary>
        /// ����ҩ��id�Ͳ���id��������ҩ����Ӧ�����ļ�¼
        /// </summary>
        /// <param name="m_objVO"></param>
        /// <returns></returns>
        public long m_lngUpdateMedStoreVsAreaInfo(clsMedStoreVsArea m_objVO)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdateMedStoreVsAreaInfo(m_objVO);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion 
        #region ����ҩ���к�������Ϣ��
        /// <summary>
        /// ����ҩ���к�������Ϣ��
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowID"></param>
        /// <param name="m_strCallContent"></param>
        /// <returns></returns>
        public long m_lngInsertMedStoreCallQue(string m_strMedStoreID, string m_strWindowID, string m_strCallContent)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngInsertMedStoreCallQue(m_strMedStoreID, m_strWindowID, m_strCallContent);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���ݲ���ҩ��id�ʹ���idɾ��ҩ���к�������Ϣ��
        /// <summary>
        /// ���ݲ���ҩ��id�ʹ���idɾ��ҩ���к�������Ϣ��
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowsID"></param>
        /// <returns></returns>
        public long m_lngDelMedStoreCallInfoByID(string m_strMedStoreID, string m_strWindowsID)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDelMedStoreCallInfoByID(m_strMedStoreID, m_strWindowsID);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ���ݲ���ҩ��id�ʹ���id��ȡҩ���к�������Ϣ��
        /// <summary>
        /// ���ݲ���ҩ��id�ʹ���id��ȡҩ���к�������Ϣ��
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowsID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreCallInfoByID(string m_strMedStoreID, string m_strWindowsID, out DataTable m_objTable)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreCallInfoByID(m_strMedStoreID, m_strWindowsID, out m_objTable);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���ݲ���ҩ��id��ȡҩ���к�������Ϣ��
        /// <summary>
        /// ���ݲ���ҩ��id��ȡҩ���к�������Ϣ��
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreCallInfoByID(string m_strMedStoreID, out DataTable m_objTable)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreCallInfoByID(m_strMedStoreID, out m_objTable);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ���ݲ���ID��ȡ��ҩ�������δ��ҩ��Ϣ
        /// <summary>
        /// ���ݲ���ID��ȡ��ҩ�������δ��ҩ��Ϣ
        /// </summary>
        /// <param name="m_strCurrentDataTime"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        public long m_lngGetMedStoreSendInfo(string m_strCurrentDataTime, string m_strMedStoreID, out DataTable m_objTable)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //   (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStoreSendInfo(m_strCurrentDataTime, m_strMedStoreID, out m_objTable);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ��������ҩ����Ӧ������˳���
        /// <summary>
        ///  ��������ҩ����Ӧ������˳���
        /// </summary>
        /// <param name="m_objVOArr"></param>
        /// <returns></returns>
        public long m_lngUpdateOrderOfTable(clsMedStoreVsArea[] m_objVOArr)
        {
            long lngRes = 0;
            //clsMedStoreBseInfoSvc objSvc =
            //    (clsMedStoreBseInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedStoreBseInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdateOrderOfTable(m_objVOArr);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ����ҩ��id���Ҵ�����Ϣ
        /// <summary>
        /// ����ҩ��id���Ҵ�����Ϣ
        /// </summary>
        /// <param name="m_strMedStoreid"></param>
        /// <param name="m_bjTable"></param>
        /// <returns></returns>
        public long m_lngGetWindowInfoByMedstoreid(string m_strMedStoreid, out DataTable m_bjTable)
        {
            long lngRes = 0;
            //clsOPMedStoreSvc objSvc =
            //    (clsOPMedStoreSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOPMedStoreSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetWindowInfoByMedstoreid(m_strMedStoreid, out m_bjTable);
            return lngRes;
        }
        #endregion
        #region ����ҩ��id�ͷ������ڻ�ȡ��ҩ��ҩ��Ϣ
        /// <summary>
        /// ����ҩ��id�ͷ������ڻ�ȡ��ҩ��ҩ��Ϣ
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strCreateDate"></param>
        /// <param name="m_dtSendWindows"></param>
        /// <param name="m_dtWindows"></param>
        /// <returns></returns>
        public long m_lngGetDataByMedStoreID(string m_strMedStoreID, string m_strCreateDate, ref List<clsWindowsInfo> m_objWindowList, ref List<clsWindowsInfo> m_objSendWindowsList)
        {
            long lngRes = 0;
            //clsOPMedStoreSvc objSvc =
            //    (clsOPMedStoreSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOPMedStoreSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetDataByMedStoreID(m_strMedStoreID, m_strCreateDate, ref m_objWindowList, ref m_objSendWindowsList);
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
