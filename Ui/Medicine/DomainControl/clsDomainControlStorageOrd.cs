using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ������Ŀ����߼���
    /// </summary>
    public class clsDomainControlStorageOrd : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        public clsDomainControlStorageOrd()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �����¼���ݡ�ŷ����ΰ��2004-05-26]
        /// <summary>
        /// �����¼��
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoSaveOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoSaveOrd(p_objOrd);

            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region ������ϸ���ݡ�ŷ����ΰ��2004-05-26
        /// <summary>
        /// ������ϸ��
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoSaveOrdDe(clsStorageOrdDe_VO p_objOrdDe)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoSaveOrdDe(p_objOrdDe);

            //			lngDoSaveOrdDe(p_objOrdDe);

            return lngRes;
        }
        #endregion

        #region �޸ļ�¼�� ŷ����ΰ
        /// <summary>
        /// �޸ļ�¼��
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrdByID(p_objOrd);

            return lngRes;
        }
        #endregion

        #region �޸���ϸ��ŷ����ΰ��2004-05-26
        /// <summary>
        /// �޸���ϸ��
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOrdDe(clsStorageOrdDe_VO p_objOrdDe)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrdDeByDeID(p_objOrdDe);

            return lngRes;
        }
        #endregion

        #region ɾ�����ݡ�ŷ����ΰ��2004-05-17
        /// <summary>
        /// ɾ����¼����¼
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoDeleteOrdByID(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteOrdByID(p_objOrd);

            return lngRes;
        }
        #endregion

        #region ɾ����ϸ�����ݡ�ŷ����ΰ��2004-05-26
        /// <summary>
        /// ����ϸIDɾ������
        /// </summary>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        public long m_lngDoDeleteOrdDeByDeID(string p_strOrdDeID)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteOrdDeByDeID(p_strOrdDeID);

            return lngRes;
        }
        #endregion

        #region ��˵��ݡ�ŷ����ΰ��2004-05-17
        /// <summary>
        /// ��˵���
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public long m_lngAduitOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAuditOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region ��˵��ݣ����Ŀ�桡ŷ����ΰ��2004-06-08
        /// <summary>
        /// ��˵��ݣ����Ŀ��
        /// </summary>
        /// <param name="p_strStorageOrdID">���ݺ�</param>
        /// <param name="p_strStorageOrdTypeID">�������ͺ�</param>
        /// <param name="p_intFlag">���ر�ʶ��1���ɹ�  0��ʧ��  -1���쳣</param>
        /// <returns></returns>
        public long m_lngAuditOrdToChangeStorage(string p_strStorageOrdID, string p_strStorageOrdTypeID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //clsStorageOrdSvc objStoSvc = (clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAuditOrdToChangeStorage(p_strStorageOrdID, p_strStorageOrdTypeID, out p_intFlag);
            //objStoSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region �������е��ݼ�¼��ŷ����ΰ��2004-05-17
        /// <summary>
        /// ģ����ѯ���ݼ�¼��Ϣ
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindOrdByAny(string p_strSQL, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;

            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �����ݺŲ��ҵ��ݼ�¼��ŷ����ΰ��2004-05-17
        /// <summary>
        /// �����ݺŲ��ҵ��ݼ�¼��Ϣ
        /// </summary>
        /// <param name="p_strOrdID">���ݺ�</param>
        /// <param name="p_strOrdType">��������</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindOrdByID(string p_strOrdID, string p_strOrdType, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByID(p_strOrdID, p_strOrdType, out p_objResult);
            return lngRes;

        }
        #endregion

        #region ���ⷿ���ҵ��ݼ�¼
        /// <summary>
        /// ���ⷿ���ҵ��ݼ�¼��Ϣ
        /// </summary>
        /// <param name="p_strID">�ⷿ����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindOrdByStorage(string p_strID, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByStorage(p_strID, out p_objResult);
            return lngRes;

        }
        #endregion

        #region �������ڲ��ҵ��ݼ�¼
        /// <summary>
        /// �������ڲ��ҵ��ݼ�¼��Ϣ
        /// </summary>
        /// <param name="p_strID">���ݺ�</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindOrdByPeriod(string p_strID, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByPeriod(p_strID, out p_objResult);
            return lngRes;

        }
        #endregion

        #region ģ����ѯ������ϸ��Ϣ��ŷ����ΰ��2004-05-25
        /// <summary>
        /// ģ�����ҵ�����ϸ
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngFindOrdDeByAny(string p_strSQL, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdDeByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ID��ѯ������ϸ��Ϣ��ŷ����ΰ��2004-05-25
        /// <summary>
        /// �����ݼ�¼�Ų��ҵ�����ϸ
        /// </summary>
        /// <param name="p_strOrdID">��¼��ID</param>
        /// <param name="p_strOrdType">��������</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngFindOrdDeByOrdID(string p_strOrdID, string p_strOrdType, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdDeByOrdID(p_strOrdID, p_strOrdType, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ģ����ѯ������ϸ��Ϣ��ŷ����ΰ��2004-05-25
        /// <summary>
        /// ģ�����ҵ�����ϸ
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngFindOrdDeByOrdMedicine(string p_strID, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdDeByMedicine(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region�������ϸ��ǰID��ŷ����ΰ��2004-05-24
        /// <summary>
        /// ��ȡ��ϸ����ǰ���ID
        /// </summary>
        /// <param name="p_strOrdDeID">����ֵΪ��ǰ���ID</param>
        /// <returns></returns>
        public long m_lngGetOrdDeID(out string p_strOrdDeID)
        {
            long lngRes = 0;
            p_strOrdDeID = null;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetOrdDeID(10, out p_strOrdDeID);

            long lngDeID = long.Parse(p_strOrdDeID);

            if (lngDeID < 1)
            {
                lngDeID = 1;
                p_strOrdDeID = lngDeID.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ���ݼ�¼�š�ŷ����ΰ��2004-05-24
        /// <summary>
        /// ��ȡ��¼����ǰ���ID
        /// </summary>
        /// <param name="p_strOrdType">��������</param>
        /// <param name="p_strOrdID">�������Ϊ��õĵ�ǰ���ID</param>
        /// <returns></returns>
        public long m_lngGetOrdID(string p_strOrdType, out string p_strOrdID)
        {
            long lngRes = 0;

            p_strOrdID = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetNewOrdID(p_strOrdType, out p_strOrdID);

            if (p_strOrdID == "")
            {
                p_strOrdID = "000000000";
            }

            long lngOrdID = long.Parse(p_strOrdID);


            if (lngOrdID < 1)
            {
                lngOrdID = 1;
                p_strOrdID = lngOrdID.ToString("0000000000");
            }
            else
            {
                lngOrdID += 1;
                p_strOrdID = lngOrdID.ToString("0000000000");
            }


            return lngRes;
        }
        #endregion

        #region ��ⵥ�ݼ�¼���Ƿ��Ѵ������ݿ��С�ŷ����ΰ��2004-05-25
        /// <summary>
        /// ��ⵥ�ݺ��Ƿ��Ѵ��ڱ��У�1Ϊ�����ڣ�0Ϊ���ڡ�
        /// </summary>
        /// <param name="p_strOrdID"></param>
        /// <returns></returns>
        public long m_lngCheckOrdID(string p_strOrdID, string p_strOrdTypeID, string p_strStorageID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngCheckOrdID(p_strOrdID, p_strOrdTypeID, p_strStorageID);

            return lngRes;
        }
        #endregion

        #region ��ϵͳ�ķ���

        #region ��ȡ���ⵥ������
        /// <summary>
        /// ��ȡ���ⵥ������
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strSign">1-��⣬2-����</param>
        /// <returns></returns>
        public long m_lngGetOutOrdType(out DataTable dt, string strSign)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetOutOrdType(out dt, strSign);

            return lngRes;
        }
        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strINordID">��ⵥID</param>
        /// <param name="strOrdTypeID">���ⵥ����</param>
        /// <param name="strPERIODID">���ⵥ�Ĳ�����</param>
        /// <param name="strOldDocID">�ɵĵ��ݺ�</param>
        /// <param name="strNewDOCID">�µĵ��ݺ�</param>
        /// <param name="strCREATOR">������</param>
        /// <param name="strSIGN">�����־ 1����� 2������</param>
        /// <returns></returns>
        public long m_lngGuideRope(string strINordID, string strOrdTypeID, string strPERIODID, string strInDOCID, string strOutDOCID, string strCREATOR, string strSIGN)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGuideRope(strINordID, strOrdTypeID, strPERIODID, strInDOCID, strOutDOCID, strCREATOR, strSIGN);

            return lngRes;
        }
        #endregion

        #region �����¼����(��⣩��
        /// <summary>
        /// �����¼����(��⣩
        /// </summary>
        /// <param name="p_objResult">��������</param>
        /// <param name="listItem">��ϸ����</param>
        /// <param name="newID">���ص���ID</param>
        /// <param name="isCheck">�Ƿ��鵥�ݺ��Ƿ���ڣ�trun ��飬false-�����</param>
        /// <param name="signint">1����� 2������3-�˿�4-�˿�</param>
        /// <returns>����-2��ʾ���ݺ��Ѿ���ռ��</returns>

        public long m_lngInsertMetStorageOrd(clsMedStorageOrd_VO p_objResult, clsMedStorageOrdDe_VO[] listItem, out string newID, bool isCheck, int signint)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrd(p_objResult, listItem, out newID, isCheck, signint);

            return lngRes;
        }
        #endregion

        #region �޸���ⵥ���кš�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngModifyRowNO(DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngModifyRowNO(dt);
            return lngRes;
        }
        #endregion

        #region ˢ�����ݡ�
        /// <summary>
        /// ˢ������
        /// </summary>
        /// <param name="OrdDe_VO"></param>
        /// <param name="OrderID">null-��ǰ��ⵥΪ����</param>
        /// <returns></returns>
        public long m_lonResetData(ref clsMedStorageOrdDe_VO[] OrdDe_VO, string OrderID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lonResetData(ref OrdDe_VO, OrderID);

            return lngRes;
        }
        #endregion


        #region �ж��������Ƿ��Ѿ����̵㲢��ˡ�
        /// <summary>
        /// �ж��������Ƿ��Ѿ����̵㲢���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPeriod">������</param>
        /// <param name="IsAppl">true-�Ѿ����̵㲢���</param>
        /// <returns></returns>
        public long m_lngEstimatePeriod(string strPeriod, out bool IsAppl)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngEstimatePeriod(strPeriod, out IsAppl);

            return lngRes;
        }
        #endregion

        #region �������ID��������
        /// <summary>
        /// �������ID��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ordTypeID">��������ID,���Ϊ""���Ա�ר״̬��������Ӧ�ĵ�����Ϣ</param>
        /// <param name="oreType">���ص�����Ϣ</param>
        /// <param name="status_int">ҩ����ҩ������ͬ����־0-ҩ������ҩƷʹ�õ��ݣ�1-��ҩ����ҩʹ�õ��ݣ�-1-����ͬ������</param>
        /// <returns></returns>
        public long m_lngFindOrdTypeNameByID(string ordTypeID, out clsStorageOrdType_VO oreType, string status_int)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdTypeNameByID(ordTypeID, out oreType, status_int);
            return lngRes;
        }
        #endregion

        #region ������ϸ����(��⣩��
        /// <summary>
        /// ������ϸ����(��⣩
        /// </summary>
        /// <param name="p_objResult"></param>
        /// <param name="tolMoney"></param>
        /// <returns></returns>
        public long m_lngInsertMetStorageOrdDe(clsMedStorageOrdDe_VO p_objResult, out double tolMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrdDe(p_objResult, out tolMoney);
            return lngRes;
        }
        #endregion

        #region �������е����ݵ�����
        /// <summary>
        /// �������е����ݵ�����
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <param name="priod">������</param>
        /// <param name="ordTypeID">���������ID</param>
        /// <returns></returns>
        public long m_lngGetStorageOrdList(out clsMedStorageOrd_VO[] p_objOrd, string priod, string ordTypeID)
        {
            p_objOrd = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageOrdList(out p_objOrd, priod, ordTypeID);
            return lngRes;
        }
        #endregion

        #region  ���ݵ��Ż����ϸ����
        /// <summary>
        ///  ���ݵ��Ż����ϸ����
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="StorageID">����</param>
        /// <param name="p_objResultArr">����ֵ</param>
        /// <returns></returns>
        public long m_lngGetMedStorageOrdDe(string StorageID, out clsMedStorageOrdDe_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStorageOrdDe(StorageID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region ���ݵ���IDɾ����ⵥ
        /// <summary>
        /// ���ݵ���IDɾ����ⵥ
        /// </summary>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        public long m_lngDeleStorageOrd(string p_strOrdDeID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleStorageOrd(p_strOrdDeID);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region ɾ������ⵥ��ϸ
        /// <summary>
        /// ɾ������ⵥ��ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdDeID">��ϸ�ɣ�</param>
        /// <returns></returns>
        public long m_lngDeleteOrdDeBy(string p_strOrdDeID, out double tolMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteOrdDeBy(p_strOrdDeID, out tolMoney);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }

        #endregion

        #region �޸���ⵥ����ϸ
        /// <summary>
        /// �޸���ⵥ����ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdID">��ⵥID</param>
        /// <param name="p_objOrdDe">��ϸ����</param>
        /// <param name="tolMoney">������ϸ���</param>
        /// <returns></returns>
        public long m_lngDoUpdateOrdAndDe(string p_objOrdID, clsMedStorageOrdDe_VO p_objOrdDe, out double tolmoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrdDe(p_objOrdID, p_objOrdDe, out tolmoney);

            return lngRes;
        }
        #endregion

        #region �޸���ⵥ
        /// <summary>
        /// �޸���ⵥ
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOrd(clsMedStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrd(p_objOrd);

            return lngRes;

        }
        #endregion

        #region ��˵���
        /// <summary>
        /// ��˵���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedOrdID">��ⵥID</param>
        /// <param name="storageID">�����ֿ�ID</param>
        /// <param name="Empman">�����</param>
        /// <param name="EmpDate">�������</param>
        /// <param name="p_objOrdDe">��ⵥ��ϸ����</param>
        /// <param name="intDept">�������ͣ�1��Ժ�ڣ�0��Ժ��</param>
        /// <returns></returns>
        public long m_lngEmpOrd(string MedOrdID, string storageID, string Empman, string EmpDate, clsMedStorageOrdDe_VO[] p_objOrdDe, int intDept)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngEmpOrd(MedOrdID, storageID, Empman, EmpDate, p_objOrdDe, intDept);
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�Ĳο���
        /// <summary>
        /// ��ȡҩƷ�Ĳο���
        /// </summary>
        /// <param name="medID">ҩƷID</param>
        /// <param name="ConsoltPrice">����ҩƷ�ο���</param>
        /// <returns></returns>
        public long m_lngGetConsoltPrice(string medID, out string ConsoltPrice, out string ConsoltUnit, out string ConsoltOrdPrice, out string ConsoltORDERPKGQTY, out string ConsoltAIMUNITPRICE, out string ConsoltLIMITUNITPRICE)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetConsoltPrice(medID, out ConsoltPrice, out ConsoltUnit, out ConsoltOrdPrice, out ConsoltORDERPKGQTY, out ConsoltAIMUNITPRICE, out ConsoltLIMITUNITPRICE);
            return lngRes;
        }
        #endregion

        #region ������е�����ҩ��
        public long m_lngGetStorage(out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorage(out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��˵��ݣ����Ŀ��

        public long m_lngOrdToChangeStorage(string p_strStorageID, string p_strMeDID, double TolNumber)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeStorage(p_strStorageID, p_strMeDID, TolNumber);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region ��˵��ݣ����Ŀ����ϸ��

        public long m_lngOrdToChangeDetail(string storageID, clsMedStorageOrdDe_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeDetail(storageID, p_objResultArr);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region ���ݲֿ��ѯ�����ϸ��

        public long m_lngGetDeTail(string storageID, out System.Data.DataTable p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetDeTail(storageID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region ���ݲ����ڻ�����г��ⵥ����
        /// <summary>
        /// ���ݲ����ڻ�����г��ⵥ����
        /// </summary>
        /// <param name="p_objResultArr">��������</param>
        /// <param name="priod">������ID</param>
        /// <returns></returns>
        public long m_lngGetStorageOrdOut(out clsMedStorageOrd_VO[] p_objResultArr, string priod, string strordType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageOrdOut(out p_objResultArr, priod, strordType);

            return lngRes;
        }
        #endregion

        #region ��ȡĳ�ֿ����󵥾ݺ�
        /// <summary>
        /// ��ȡĳ�ֿ����󵥾ݺ�
        /// </summary>
        /// <param name="p_strMaxDoc"></param>
        /// <param name="strDate"></param>
        /// <param name="strSIGN"></param>
        /// <param name="STORAGEID">�ֿ�ID</param>
        /// <returns></returns>
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strDate, string strSIGN, string STORAGEID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxDoc(out p_strMaxDoc, strDate, strSIGN, STORAGEID);

            return lngRes;
        }
        #endregion

        #region �����¼����(���⣩��
        /// <summary>
        /// �����¼����(���⣩
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResult">���뵥������</param>
        /// <param name="p_objResultDe">������ϸ����</param>
        /// <param name="newID">�������ID</param>
        /// <param name="isCheck">�Ƿ��鵥�ݺ��Ƿ���ڣ�trun ��飬false-�����</param>
        /// <param name="signint">1����� 2������3-�˿�4-�˿�</param>
        /// <returns>����-2��ʾ���ݺ��Ѿ���ռ��</returns>
        public long m_lngInsertMetStorageOrdOut(clsMedStorageOrd_VO p_objOrd, clsMedStorageOrdDe_VO[] p_objResultDe, out string newID, bool isCheck, int signint)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrdOut(p_objOrd, p_objResultDe, out newID, isCheck, signint);
            return lngRes;
        }
        #endregion

        #region ������ϸ����(���⣩
        /// <summary>
        /// ������ϸ��
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngInsertMetStorageOrdDeOut(clsMedStorageOrdDe_VO p_objOrdDe, out double tolMoney)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrdDeOut(p_objOrdDe, out tolMoney);
            //			lngDoSaveOrdDe(p_objOrdDe);

            return lngRes;
        }
        #endregion

        #region ���ݳ��ⵥID������еĳ�����ϸ
        /// <summary>
        /// ���ݳ��ⵥID������еĳ�����ϸ
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngGetMedStorageOrdDeOut(string storageID, out clsMedStorageOrdDe_VO[] p_objOrdDe)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStorageOrdDeOut(storageID, out p_objOrdDe);
            //			lngDoSaveOrdDe(p_objOrdDe);


            return lngRes;
        }
        #endregion

        #region �޸ĳ��ⵥ��
        /// <summary>
        /// �޸ĳ��ⵥ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOutOrd(clsMedStorageOrd_VO p_objOrdDe)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOutOrd(p_objOrdDe);
            return lngRes;

        }
        #endregion

        #region �޸���ϸ��
        /// <summary>
        /// �޸���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOutOrdDe(clsMedStorageOrdDe_VO p_objOrdDe, string strTotailMoney, string strOrdID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOutOrdDe(p_objOrdDe, strTotailMoney, strOrdID);
            return lngRes;
        }
        #endregion

        #region �����ⵥ�ݣ����Ŀ����ϸ��
        /// <summary>
        /// �����ⵥ�ݣ����Ŀ����ϸ��
        /// </summary>

        /// <returns></returns>
        public long m_lngOrdToChangeDetailOut(string SysNO, double OutNumber)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeDetailOut(SysNO, OutNumber);
            //			lngDoSaveOrd(p_objOrd);


            return lngRes;

        }
        #endregion

        #region �����ⵥ�ݣ����Ŀ��
        /// <summary>
        /// �����ⵥ�ݣ����Ŀ��
        /// </summary>
        /// <returns></returns>
        public long m_lngOrdToChangeStorageOut(string p_strStorageID, string p_strMeDID, double TolNumber)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeStorageOut(p_strStorageID, p_strMeDID, TolNumber);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;

        }
        #endregion

        #region ��˳��ⵥ��
        /// <summary>
        /// ��˳��ⵥ��
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objResultDeArr"></param>
        /// <param name="flag">true,�Զ�����ҩ���������</param> 
        /// <param name="status">�Ƿ�������0-������1-����</param>
        /// <returns>1,��˳ɹ�;-1,ʧ��;-2,��û�����ø�ҩƷ�İ�װ��;-3,�������Ͳ���ȷ</returns>
        public long m_lngAduitOrdOut(clsMedStorageOrd_VO p_objResultArr, clsMedStorageOrdDe_VO[] p_objResultDeArr, bool flag, int status)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAduitOrdOut(p_objResultArr, p_objResultDeArr, flag, status);
            return lngRes;
        }
        #endregion

        #region ��ȡ���е�ҩ��
        /// <summary>
        /// ��ȡ���е�ҩ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lonGetStore(out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lonGetStore(out dt);
            return lngRes;
        }
        #endregion

        #endregion


    }
}
