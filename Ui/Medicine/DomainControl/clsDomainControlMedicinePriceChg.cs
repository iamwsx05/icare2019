using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
    #region ���ۿ��Ʋ�
    /// <summary>
    /// ���ۿ��Ʋ�
    /// Create by kong 2004-06-09
    /// </summary>
    public class clsDomainControlMedicinePriceChg : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainControlMedicinePriceChg()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region �������ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �������ۼ�¼��
        /// </summary>
        /// <param name="p_objItem">���ۼ�¼����Ϣ</param>
        /// <returns>����ֵ</returns>
        public long m_lngDoAddNewMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicinePriceChgAppl(p_objItem);
            //objSrc.Dispose();

            return lngRes;
        }
        #endregion

        #region �޸ĵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �޸ĵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objItem">���ۼ�¼����Ϣ</param>
        /// <returns></returns>
        public long m_lngDoUpdMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicinePriceChgAppl(p_objItem);
            //objSrc.Dispose();

            return lngRes;
        }
        #endregion

        #region ɾ�����ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ɾ�����ۼ�¼��
        /// </summary>
        /// <param name="p_strID">���ۼ�¼��ID��</param>
        /// <returns></returns>
        public long m_lngDoDeleteMedicinePriceChgAppl(string p_strID)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoDeleteMedicinePriceChgAppl(p_strID);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ����������ϸ��
        /// </summary>
        /// <param name="p_objItem">������ϸ����Ϣ</param>
        /// <returns></returns>
        public long m_lngDoAddNewMedicinePriceChgApplDe(clsMedicinePriceChgApplDe_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicinePriceChgApplDe(p_objItem);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸ĵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �޸ĵ�����ϸ��
        /// </summary>
        /// <param name="p_objItem">������ϸ����Ϣ</param>
        /// <returns></returns>
        public long m_lngDoUpdMedicinePriceChgApplDe(clsMedicinePriceChgApplDe_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicinePriceChgApplDe(p_objItem);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��������ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ɾ��������ϸ��
        /// </summary>
        /// <param name="p_strID">������ϸ��ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteMedicinePriceChgApplDe(string p_strID)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoDeleteMedicinePriceChgApplDe(p_strID);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��˵��۵�  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��˵��۵�
        /// </summary>
        /// <param name="p_objItem">���ۼ�¼����Ϣ</param>
        /// <returns></returns>
        public long m_lngDoAduitMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAduitMedicinePriceChgAppl(p_objItem);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��˵��۵������ļ۸�  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��˵��۵����ļ۸�
        /// </summary>
        /// <param name="p_strNo">���۵���</param>
        /// <param name="p_intFlag">����ʱ�����ʶ��1Ϊ�ɹ���0Ϊ���ļ۸������1Ϊ�����쳣</param>
        /// <returns></returns>
        public long m_lngDoChangePriceAfterAduit(string p_strNo, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoChangePriceAfterAduit(p_strNo, out p_intFlag);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ģ�����ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ģ�����ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByAny(string p_strSQL, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByAny(p_strSQL, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����ۼ�¼��ID�Ų��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �����ۼ�¼��ID�Ų��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strID">���ۼ�¼��ID��</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByID(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByID(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����ۼ�¼���Ų��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �����ۼ�¼���Ų��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strNo">���ۼ�¼����</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByNo(string p_strNo, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByNo(p_strNo, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ���������Ͳ��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        ///  ���������Ͳ��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strID">��������ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByStorageOrdType(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByStorageOrdType(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region  ���ֿ���ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ֿ���ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strID">�ֿ�ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByStorage(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByStorage(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������ڲ��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �������ڲ��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strID">������ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByPeriod(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByPeriod(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ʱ��β��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ������ʱ��β��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByDate(string p_strStartDate, string p_strEndDate, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByDate(p_strStartDate, p_strEndDate, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������еĵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �������еĵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindAllMedicinePriceChgAppl(out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedicinePriceChgAppl(out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ģ�����ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ģ�����ҵ�����ϸ��
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByAny(string p_strSQL, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByAny(p_strSQL, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������ϸ��ID�Ų��ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��������ϸ��ID�Ų��ҵ�����ϸ��
        /// </summary>
        /// <param name="p_strID">������ϸ��ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByID(string p_strID, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByID(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����ۼ�¼���Ų��ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �����ۼ�¼���Ų��ҵ�����ϸ��
        /// </summary>
        /// <param name="p_strNo">���ۼ�¼����</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByApplNo(string p_strNo, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByApplNo(p_strNo, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ҩƷ���ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��ҩƷ���ҵ�����ϸ��
        /// </summary>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByMedicine(string p_strMedicineID, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByMedicine(p_strMedicineID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ҵ�ǰ���ĵ��ۼ�¼��ID��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ҵ�ǰ���ĵ��ۼ�¼��ID
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetApplID(out string p_strResult)
        {

            long lngRes = 0;

            p_strResult = null;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetApplID(out p_strResult);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ҵ�ǰ�����������ĵ��ۼ�¼����  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ҵ�ǰ�����������ĵ��ۼ�¼����
        /// </summary>
        /// <param name="p_strStorageOrdTypeID">��������ID</param>
        /// <param name="p_strResult">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetApplNo(string p_strStorageOrdTypeID, out string p_strResult)
        {

            long lngRes = 0;

            p_strResult = null;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetApplNo(p_strStorageOrdTypeID, out p_strResult);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ҵ�ǰ���ĵ�����ϸ��ID��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ҵ�ǰ���ĵ�����ϸ��ID��
        /// </summary>
        /// <param name="p_strResult">�����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetApplDeID(out string p_strResult)
        {

            long lngRes = 0;

            p_strResult = null;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            //System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetApplDeID(out p_strResult);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

    }
    #endregion
}
