using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrol_ConcertreCipe ��ժҪ˵����
    /// </summary>
    public class clsDomainConrol_ConcertreCipe : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrol_ConcertreCipe()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion 

        #region Э������ϵͳ(��)

        #region ���Ҳ�������
        /// <summary>
        /// ���Ҳ�������
        /// </summary>
        public long m_lngGetPatType(out clsPatientType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            p_objResultArr = new clsPatientType_VO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPatType(out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �����Ը�����
        /// <summary>
        /// �����Ը�����
        /// </summary>
        public long m_longPrecent(DataTable dt, out DataTable dt1, string payType)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_longPrecent(dt, out dt1, payType);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ȡЭ������
        /// <summary>
        /// ��ȡЭ������m_lngGetConcertreCipeDetailByIDOutTb
        /// </summary>
        public long m_lngGetConcertreCipeByEmpIDOutTB(string CREATERID, string strID, out DataTable p_objResultArr, int intFLAG, bool isPublic)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeByEmpIDOutTB(CREATERID, strID, out p_objResultArr, intFLAG, isPublic);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡЭ������IDȡЭ������ϸ
        /// <summary>
        /// ��ȡЭ������
        /// </summary>
        public long m_lngGetConcertreCipeDetailByIDOutTb(string strID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeDetailByIDOutTb(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡЭ�����������Ĳ���
        /// <summary>
        /// ��ȡЭ�����������Ĳ���
        /// </summary>
        public long m_lngGetDeptByConcertreCipeID(string strID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetDeptByConcertreCipeID(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ŀ����
        /// <summary>
        /// ��ȡ��Ŀ����
        /// </summary>
        public long m_mthFindMedicine(out DataTable dtbResult, string strType)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindMedicine(out dtbResult, strType);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ���еĲ�����Ϣ
        /// <summary>
        /// ��ȡ���еĲ�����Ϣ
        /// </summary>
        public long m_lngGetDeptList(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetDeptList(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ���е�Ƶ��
        /// <summary>
        /// ��ȡ���е�Ƶ����Ϣ
        /// </summary>
        public long m_mthFindFrequency(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindFrequency(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ���е��÷�
        /// <summary>
        /// ��ȡ���е��÷���Ϣ
        /// </summary>
        public long m_mthFindUsage(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindUsage(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �����µ�Э����
        /// <summary>
        /// �����µ�Э����
        /// </summary>
        public long m_lngAddNewConcertre(out string p_strRecordID, string[] bt, DataTable btDe, DataTable btDetp, string isDetp, int intFLAG)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertre(out p_strRecordID, bt, btDe, btDetp, isDetp, intFLAG);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        public long m_lngDeleteConcertrecipeAndDe(string[] DeleRow, string[] DeleRowDe, string strItem, string strFlag)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipeAndDe(DeleRow, DeleRowDe, strItem, strFlag);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �޸Ĵ�����ϸ
        /// <summary>
        /// �޸Ĵ�����ϸ
        /// </summary>
        /// <param name="strID">����ID</param>
        /// <param name="dtRow">�µ���ϸ����</param>
        /// <param name="oldITEMID">�ɵ���ϸ��ĿID����= null�޸ĵ�����¼����=NULL�����޸Ĵ�����ϸ����ͬ��Ŀ������</param>
        /// <param name="blIsPublic">�Ƿ��й���Ȩ��</param>
        /// <param name="CREATERID">������</param>
        /// <returns></returns>
        public long m_lngConcertreCipeDetailModifyDe(string strID, string[] dtRow, string oldITEMID, string strFLAG, bool blIsPublic, string CREATERID, int m_intSort)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeDetailModifyDe(strID, dtRow, oldITEMID, strFLAG, blIsPublic, CREATERID, m_intSort);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �޸�Э������
        /// <summary>
        /// �޸�Э������
        /// </summary>
        public long m_lngConcertreModify(string[] ModifiyRow, DataTable Deptbt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreModify(ModifiyRow, Deptbt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ������ϸ
        /// <summary>
        /// ������ϸ
        /// </summary>
        public long m_lngConcertreCipeDetailAddNEWDe(string strID, string[] btDe, int m_intSort)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeDetailAddNEWDe(strID, btDe, m_intSort);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��鵱ǰ��½���û��Ƿ��б༭���ô�����Ȩ��
        /// <summary>
        /// ��鵱ǰ��½���û��Ƿ��б༭���ô�����Ȩ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID">�û�ID</param>
        /// <param name="isPublic">false-û��Ȩ��,true-��Ȩ��</param>
        /// <returns></returns>
        public long m_lngGetPublic(string strID, out bool isPublic)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetPublic(strID, out isPublic);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #endregion


        #region ��ȡƵ�ʵĴ���������
        /// <summary>
        /// ��ȡƵ�ʵĴ���������
        /// </summary>
        /// <param name="strResult"></param>
        /// <param name="strFREQID"></param>
        /// <returns></returns>
        public long m_lngGetDayAndTime(out string strResult, out string strResult1, string strFREQID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetDayAndTime(out strResult, out strResult1, strFREQID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡЭ������
        /// <summary>
        /// ��ȡЭ������
        /// </summary>
        public long m_lngGetConcertreCipeByEmpID(string strID, out clsConcertrectpe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsConcertrectpe_VO[0];
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeByEmpID(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡЭ��������ϸ
        /// <summary>
        /// ��ȡЭ��������ϸ
        /// </summary>
        public long m_lngGetConcertreCipeDetailByID(string strID, out clsConcertrecipeDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsConcertrecipeDetail_VO[0];
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeDetailByID(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡʹ�ò���
        /// <summary>
        /// ��ȡЭ��������ϸ
        /// </summary>
        public long m_lngGetConcertreCipeDeptByID(string strID, out clsConcertrecipeDept_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsConcertrecipeDept_VO[0];
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeDeptByID(strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public long m_lngAddNewConcertreCipe(out string strID, clsConcertrectpe_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrectpe_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertreCipe(out strID, p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ����������ϸ
        /// <summary>
        /// ����������ϸ
        /// </summary>
        public long m_lngAddNewConcertreCipeDetail(out string strID, clsConcertrecipeDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrecipeDetail_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertreCipeDetail(out strID, p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ����ʹ�ò���
        /// <summary>
        /// ����������ϸ
        /// </summary>
        public long m_lngAddNewConcertreCipeDept(clsConcertrecipeDept_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrecipeDept_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertreCipeDept(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �޸�Э������
        /// <summary>
        /// �޸�Э������
        /// </summary>
        public long m_lngConcertreCipeModify(clsConcertrectpe_VO p_objResultArr)
        {
            long lngRes = 0;
            //	p_objResultArr=new clsConcertrectpe_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeModify(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �޸�Э��������ϸ
        /// <summary>
        /// �޸�Э��������ϸ
        /// </summary>
        public long m_lngConcertreCipeDetailModify(clsConcertrecipeDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            //		p_objResultArr=new clsConcertrecipeDetail_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeDetailModify(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ɾ��Э��������ϸ
        /// <summary>
        /// ɾ��Э��������ϸ
        /// </summary>
        public long m_lngDeleteConcertrecipeDetail(clsConcertrecipeDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            //		p_objResultArr=new clsConcertrecipeDetail_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipeDetail(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ɾ��Э������
        /// <summary>
        /// ɾ��Э������
        /// </summary>
        public long m_lngDeleteConcertrecipe(clsConcertrectpe_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrectpe_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipe(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ɾ��ʹ�ò���
        /// <summary>
        /// ɾ��ʹ�ò���
        /// </summary>
        public long m_lngDeleteConcertrecipeDept(clsConcertrecipeDept_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrecipeDept_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipeDept(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ����������Ƿ�ʹ��
        /// <summary>
        /// ����������Ƿ�ʹ��
        /// </summary>
        /// <param name="strID">����ID ��Ϊ�վ����޸�ʱʹ��</param>
        /// <returns></returns>
        public long m_mthCheckCodeIsUsed(string strCode, string strID, string strFlag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthCheckCodeIsUsed(strCode, strID, strFlag);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ��鲿λ����������
        /// <summary>
        /// ��ȡ��鲿λ����������
        /// </summary>
        /// <param name="P_dtPark">���ؼ�鲿λ���������</param>
        /// <param name="ParkName">������Ŀ��Ӧ�ļ�����������</param>
        /// <param name="parkID">������Ŀ��Ӧ�ļ�������ID</param>
        /// <param name="strItemId">ԭ��ĿID������������</param>
        /// <param name="strType">0-����������������鲿λ</param>
        /// <returns></returns>
        public long m_lngGetPart(out DataTable P_dtPark, out string ParkName, out string parkID, string strItemId, string strType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetPart(out P_dtPark, out ParkName, out parkID, strItemId, strType);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
    }
}
