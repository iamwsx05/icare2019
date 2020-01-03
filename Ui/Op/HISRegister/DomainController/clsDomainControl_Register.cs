using System;
using weCare.Core.Entity;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_Register ��ժҪ˵����
    /// </summary>
    public class clsDomainControl_Register : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_Register()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP02 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP02();
            }
        }
        #endregion 

        #region ���������շ���Ŀά��

        #region ������еĲ�������
        /// <summary>
        /// ������еĲ�������
        /// </summary>
        /// <param name="btPatientPayType"></param>
        /// <returns></returns>
        public long m_lngGetAllPatientPayType(out DataTable btPatientPayType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = proxy.Service.m_lngGetAllPatientPayType(out btPatientPayType);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ������Ŀ��ϸ
        /// <summary>
        /// ������Ŀ��ϸ
        /// </summary>
        /// <param name="strPayTypeID"></param>
        /// <param name="strItemId"></param>
        /// <param name="intQty"></param>
        /// <returns></returns>
        public long m_lngAddNewItem(string strPayTypeID, string strItemId, int intQty, int REGISTER, int RECIPEFLAG, int EXPERT)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = proxy.Service.m_lngAddNewItem(strPayTypeID, strItemId, intQty, REGISTER, RECIPEFLAG, EXPERT);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion


        #region ɾ����Ŀ
        /// <summary>
        /// ɾ����Ŀ
        /// </summary>
        /// <param name="strPayTypeID"></param>
        /// <param name="strItemId"></param>
        /// <returns></returns>
        public long m_lngDeleItem(string strPayTypeID, string strItemId)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = proxy.Service.m_lngDeleItem(strPayTypeID, strItemId);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �޸���Ŀ����
        /// <summary>
        /// ������Ŀ��ϸ
        /// </summary>
        /// <param name="strPayTypeID"></param>
        /// <param name="strItemId"></param>
        /// <param name="intQty"></param>
        /// <returns></returns>
        public long m_lngModifyItem(string strPayTypeID, string strOldItemId, string strItemId, int intQty, int REGISTER, int RECIPEFLAG, int EXPERT)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = proxy.Service.m_lngModifyItem(strPayTypeID, strOldItemId, strItemId, intQty, REGISTER, RECIPEFLAG, EXPERT);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���ݲ�������ID��ȡ��Ŀ����
        /// <summary>
        /// ���ݲ�������ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="strPayTypeID"></param>
        /// <param name="bt"></param>
        /// <returns></returns>
        public long m_lngGetItemByPayID(string strPayTypeID, out DataTable bt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = proxy.Service.m_lngGetItemByPayID(strPayTypeID, out bt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #endregion

        #region ���ҹҺ�����
        /// <summary>
        /// ���ҹҺ�����
        /// </summary>
        public long m_lngGetRegType(out clsRegisterType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsRegisterType_VO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetRegType(out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ѯ������Ϣ
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="Sex"></param>
        /// <param name="brith"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindPatient(string strName, string Sex, string brith, out DataTable dt)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = proxy.Service.m_lngFindPatient(strName, Sex, brith, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���Ҳ�������
        /// <summary>
        /// ���Ҳ�������
        /// </summary>
        public long m_lngGetPatType(out clsPatientType_VO[] p_objResultArr)
        {
            long lngRes = 0;
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

        #region ����ID���ҹҺŷ���
        /// <summary>
        /// ����ID���ҹҺŷ���
        /// </summary>
        /// <param name="PatTypeID"></param>
        /// <param name="RegTypeID"></param>
        /// <param name="clsVO"></param>
        public long m_lngFindPatRegFeeByID(string PatTypeID, string RegTypeID, out clsPatRegFee_VO clsVO)
        {
            long lngRes = 0;
            clsVO = new clsPatRegFee_VO();
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc));
            lngRes = proxy.Service.m_lngFindFeeListByID(RegTypeID, PatTypeID, out clsVO);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ���ҹҺŷ���
        /// <summary>
        /// ���ҹҺŷ���
        /// </summary>
        /// <param name="dtResult"></param>
        public long m_lngFindPatRegFee(out DataTable dtResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc));
            lngRes = proxy.Service.m_lngFindFeeList(out dtResult);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ����Һŷ���
        /// <summary>
        /// ����Һŷ���
        /// </summary>
        /// <param name="clsVO"></param>
        /// <param name="IsNew"></param>
        public long m_lngSavePatRegFee(clsPatRegFee_VO clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc));
            if (IsNew) //����
                lngRes = proxy.Service.m_lngNewFeeList(clsVO);
            else
                lngRes = proxy.Service.m_lngUPDateFeeList(clsVO);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ɾ���Һŷ���
        /// <summary>
        /// ɾ���Һŷ���
        /// </summary>
        /// <param name="clsVO"></param>
        public long m_lngDelPatRegFee(clsPatRegFee_VO clsVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc));
            lngRes = proxy.Service.m_lngDelFeeList(clsVO);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �������ƿ���ȡ�ò��˵���Ϣ
        /// <summary>
        /// �������ƿ���ȡ�ò��˵���Ϣ
        /// </summary>
        /// <param name="strCardID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetPatByCard(string strCardID, out clsPatient_VO p_objResultArr, string registerDate, out string DepName, out string doctorName, out string registerDate1)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = proxy.Service.m_lngGetPatByCardID(strCardID, out p_objResultArr, registerDate, out DepName, out doctorName, out registerDate1);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ѯ�ҹҺ�����״̬
        /// <summary>
        /// ��ѯ�ҹҺ�����״̬
        /// </summary>
        /// <param name="strTypeid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public long m_lngFindType(string strTypeid, out string command)
        {
            command = "";
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = proxy.Service.m_lngFindType(strTypeid, out command);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ȡ��ǰʱ�εĹҺſ�����Ϣ
        /// <summary>
        /// ��ȡ��ǰʱ�εĹҺſ�����Ϣ
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="strPerio"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetPlanDepByDate(string strDate, string strPerio, out clsDepartmentVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsDepartmentVO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetOPDeptListByDate(strDate, strPerio, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion


        #region ��ȡ��ǰʱ�εĹҺſ�����Ϣ

        public long m_lngGetPlanDep(string strDate, string strPerio, out clsDepartmentVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsDepartmentVO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetOPDeptList(strDate, strPerio, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ȡҽ���б�
        /// <summary>
        /// ��ȡҽ���б�
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="strPerio"></param>
        /// <param name="strDepID"></param>
        /// <param name="strRegType"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetOPDoctorList(string strDepID, out clsEmployeeVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsEmployeeVO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetOPDoctorListForReg(strDepID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ȡû�п�������Ա��
        /// <summary>
        /// ��ȡû�п�������Ա��
        /// </summary>
        /// <returns></returns>
        public long m_lngGetEmployeeNo(out DataTable p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new DataTable();
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetEmployeeNo(out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ����Ƿ��е�ǰʱ�ε��Ű��¼
        public bool m_bnlCheckPlanByDatePerio(string strDate, string strPerio)
        {
            bool bnlRes = false;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            bnlRes = (new weCare.Proxy.ProxyOP01()).Service.m_bnlCheckPlanByDatePerio(strDate, strPerio);
            //objSvc.Dispose();
            //objSvc = null;

            return bnlRes;
        }
        #endregion

        #region ��ȡĳ��ҽ���ļƻ���Ϣ
        /// <summary>
        /// ��ȡĳ��ҽ���ļƻ���Ϣ
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="strPerio"></param>
        /// <param name="strDocID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetDocPlan(string strDate, string strPerio, string strDocID,
            out clsOPDoctorPlan_VO p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPDoctorPlan_VO();
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetDocPlan(strDate, strPerio, strDocID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �����Һż�¼
        /// <summary>
        /// �����Һż�¼
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="strID"></param>
        /// <param name="strNo"></param>
        /// <param name="strOrderNo"></param>
        /// <param name="intRegCount"></param>
        /// <returns></returns>
        public long m_lngAddRegister(clsPatientRegister_VO p_objResultArr, out string strID, out string strNo, out string strOrderNo, out string intRegCount, clsPatient_VO clsPatientvo, int isNewPatient, string strCardID, out string outCardID, clsPatientDetail_VO[] PatientDetail_VO, string strNO, string strPatienID, string patientidentityno)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = proxy.Service.m_lngAddNewPatientRegister(p_objResultArr, out strID, out strNo, out strOrderNo, out intRegCount, clsPatientvo, isNewPatient, strCardID, out outCardID, PatientDetail_VO, strPatienID, patientidentityno);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        public long m_lngAddRegisterDegail(clsPatientDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = proxy.Service.m_lngAddNewRegDetail(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ȡ�÷������ĵ�ǰʱ��
        public DateTime m_GetServTime()
        {
            DateTime DTR;
            //com.digitalwave.iCare.middletier.HIS.clsGetServerDate objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetServerDate)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetServerDate));
            DTR = (new weCare.Proxy.ProxyOP()).Service.m_GetServerDate();
            //objSvc.Dispose();
            //objSvc = null;
            return DTR;
        }
        #endregion

        #region ���ص�ǰҽ���ѽ�������
        public int m_GetDocTakeCout(string strDocID, string RegDate)
        {
            int intCount = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            proxy.Service.m_lngGetDocTakeCount(strDocID, RegDate, out intCount);
            //objSvc.Dispose();
            //objSvc = null;
            return intCount;
        }
        #endregion

        #region �޸ĹҺ�
        public long m_lngModifyRegister(clsPatientRegister_VO objResult)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngDoUpdPatientRegisterByID(objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        public long m_lngModifyRegisterDetail(clsPatientDetail_VO objResult)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngModifyRegDetail(objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ��ȡ���ñ�
        public long m_lngGetPay(out clsRegisterPay[] m_clsRegisterPay)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            m_clsRegisterPay = new clsRegisterPay[0];
            //com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPatRegFeeSvc));
            long LngArg = proxy.Service.m_lngGetRegCharge(out m_clsRegisterPay);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ���ҹҺ�(��)
        /// <summary>
        /// ���ҹҺ�
        /// </summary>
        /// <param name="firstdate"></param>
        /// <param name="lastdate"></param>
        /// <param name="dtRegister"></param>
        /// <returns></returns>
        public long m_lngQulRegByDateNew(string firstdate, string lastdate, out DataTable dtRegister, string EmpID, string Scope)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngQulRegByDateNew(out dtRegister, firstdate, lastdate, EmpID, Scope);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion
        #region �����ֶβ��ҹҺ�(��)
        /// <summary>
        ///  �����ֶβ��ҹҺ�(��)
        /// </summary>
        /// <param name="m_strArr"></param>
        /// <param name="dtRegister"></param>
        /// <returns></returns>
        public long m_lngQulRegByFieldNew(string[] m_strArr, out DataTable dtRegister)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngQulRegByFieldNew(m_strArr, out dtRegister);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ���ùҺ��Ƿ��չ���
        /// <summary>
        /// ���ùҺ��Ƿ��չ���
        /// </summary>
        /// <param name="registerID"></param>
        /// <param name="isReMoney"></param>
        /// <returns></returns>
        public long m_lngCheckRegister(string registerID, out bool isReMoney, out string outstr)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngCheckRegister(registerID, out isReMoney, out outstr);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ��ȡ�Һ����͵�״̬
        /// <summary>
        /// ��ȡ�Һ����͵�״̬
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public long m_lngGetPatTypeFLAG(string strTypeID, out int intType)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetPatTypeFLAG(strTypeID, out intType);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ���ҹҺ�
        /// <summary>
        /// ���ҹҺ�
        /// </summary>
        /// <param name="firstdate"></param>
        /// <param name="lastdate"></param>
        /// <param name="dtRegister"></param>
        /// <returns></returns>
        public long m_lngQulRegByDate(string firstdate, string lastdate, out DataTable dtRegister)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngQulRegByDate(out dtRegister, firstdate, lastdate);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region �����ֶβ��ҹҺ�
        public long m_lngQulRegByCol(out DataTable dtRegister, string strFeilt, string strValue, string Option)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngQulRegByCol(out dtRegister, strFeilt, strValue, Option);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ��ȡ�Һŷ���
        /// <summary>
        /// ��ȡ�Һŷ���
        /// </summary>
        /// <param name="strRegister"></param>
        /// <param name="dtRegister"></param>
        /// <returns></returns>
        public long m_lngQulRegDetail(string strRegister, out DataTable dtRegister)
        {
            dtRegister = new DataTable();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngQulRegDetailByID(strRegister, out dtRegister);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ��ȡ�Һŷ���(��ѯ��)
        /// <summary>
        /// ��ȡ�Һŷ���
        /// </summary>
        /// <param name="strRegister"></param>
        /// <param name="dtRegister"></param>
        /// <returns></returns>
        public long m_lngQulRegDetailfind(string strRegister, out DataTable dtRegister)
        {
            dtRegister = new DataTable();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngQulRegDetailByIDFind(strRegister, out dtRegister);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region �˺�
        /// <summary>
        /// �˺�
        /// </summary>
        /// <param name="strRegisterID"></param>
        /// <param name="strReturnRegEmpno"></param>
        /// <param name="strReturnDate"></param>
        /// <returns></returns>
        public long m_lngCancelReg(string strRegisterID, string strReturnRegEmpno, string strReturnDate, string ConfirmID, out string newID)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngCancelReg(strRegisterID, strReturnRegEmpno, strReturnDate, ConfirmID, out newID);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region �շѽ����ձ���

        public long m_lngGetPayTypeAndCheckOutData(string OPREMPID, string strDate, out DataTable dtPayType, out DataTable dtCheckOut)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetPayTypeAndCheckOutData(OPREMPID, strDate, out dtPayType, out dtCheckOut);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }


        #region �շ�Ա�սᱨ��ֿ���ӡ
        /// <summary>
        /// �շ�Ա�սᱨ��ֿ���ӡ
        /// </summary>
        /// <param name="CheckDate">��������</param>
        /// <param name="checkMan">������</param>
        /// <param name="dt1">�ֽ��սᱨ��</param>
        /// <param name="dtDe1">�ֽ��սᱨ��(��ϸ)</param>
        /// <param name="dt2">ҽ����ˢ���սᱨ��</param>
        ///  <param name="dtDe2">ҽ����ˢ���սᱨ��(��ϸ)</param>
        /// <param name="dt3">�����սᱨ��</param>
        /// <param name="dtDe3">�����սᱨ��(��ϸ)</param>
        /// <param name="dt4">�����սᱨ��</param>
        /// <param name="dtDe4">�����սᱨ��(��ϸ)</param>
        /// <param name="dtType">�շ�����</param>
        /// <returns></returns>
        public long m_lngGetDataAllOfStat(string CheckDate, string checkMan, out DataTable dt1, out DataTable dtDe1, out DataTable dt2, out DataTable dtDe2, out DataTable dt3, out DataTable dtDe3, out DataTable dt4, out DataTable dtDe4, out DataTable dtType)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetDataAllOfStat(CheckDate, checkMan, out dt1, out dtDe1, out dt2, out dtDe2, out dt3, out dtDe3, out dt4, out dtDe4, out dtType);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion
        #region �շ�Ա�սᱨ������շ�
        /// <summary>
        ///  �շ�Ա�սᱨ������շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="CheckDate"></param>
        /// <param name="checkMan"></param>
        /// <param name="dt1">�ֽ�֧��</param>
        /// <param name="dtDe1">�ֽ�֧����ϸ</param>
        /// <param name="dt2">IC��</param>
        /// <param name="dtDe2">IC����ϸ</param>
        /// <param name="dt3">���п�</param>
        /// <param name="dtDe3">���п���ϸ</param>
        /// <param name="dt4">֧Ʊ</param>
        /// <param name="dtDe4">֧Ʊ��ϸ</param>
        /// <param name="dt5">��������</param>
        /// <param name="dtDe5">����������ϸ</param>
        /// <param name="dt6">���Ѽ���</param>
        /// <param name="dtDe6">���Ѽ�����ϸ</param>
        /// <param name="dt7">����</param>
        /// <param name="dtDe7">������ϸ</param>
        /// <param name="dt8">����</param>
        /// <param name="dtDe8">������ϸ</param>
        /// <param name="dt9">��Ժ</param>
        /// <param name="dtDe9">��Ժ��ϸ</param>
        /// <param name="dt10">�ض�ҽ������</param>
        /// <param name="dtDe10">�ض�ҽ��������ϸ</param>
        /// <param name="dtType"></param>
        /// <returns></returns>
        public long m_lngGetCheckOutOfClassification(string CheckDate, string checkMan, out DataTable dt1, out DataTable dtDe1, out DataTable dt2, out DataTable dtDe2, out DataTable dt3, out DataTable dtDe3, out DataTable dt4, out DataTable dtDe4, out DataTable dt5, out DataTable dtDe5, out DataTable dt6, out DataTable dtDe6, out DataTable dt7, out DataTable dtDe7, out DataTable dt8, out DataTable dtDe8, out DataTable dt9, out DataTable dtDe9, out DataTable dt10, out DataTable dtDe10, out DataTable dtType)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetCheckOutOfClassification(CheckDate, checkMan, out dt1, out dtDe1, out dt2, out dtDe2, out dt3, out dtDe3, out dt4, out dtDe4, out dt5, out dtDe5, out dt6, out dtDe6, out dt7, out dtDe7, out dt8, out dtDe8, out dt9, out dtDe9, out dt10, out dtDe10, out dtType);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }

        #endregion
        #region ����(��,ͣ��)
        public long m_lngCheckData(DataTable dt, string CheckName, string CheckDate)
        {
            //System.Security.Principal.IPrincipal p_objPrincipal = null;
            ////com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            ////    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            //long LngArg = proxy.Service.m_lngCheckData(dt, CheckName, CheckDate);
            ////objSvc.Dispose();
            ////objSvc = null;
            //return LngArg;
            return 0;
        }
        #endregion

        #region ����(��)
        /// <summary>
        /// ����(��)
        /// </summary>
        /// <param name="OperID">�տ�ԱID</param>
        /// <param name="CheckDate">�ս�ʱ��</param>
        /// <returns></returns>
        public long m_lngCheckData(string OperID, out string CheckDate)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //                                                    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));

            long l = proxy.Service.m_lngCheckData(OperID, out CheckDate);
            //objSvc.Dispose();
            //objSvc = null;
            return l;
        }
        #endregion

        #region ��ʷ��ѯ
        public long m_lngGetHistory(string startDate, string endDate, string checkMan, out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetHistor(startDate, endDate, checkMan, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion
        #region ��ȡ�ѽ��ʼ�¼����
        public long m_lngGetCheckOutHistoryData(string m_strFirstDate, string strEndDate, string BALANCEEMP, out DataTable dtPayType, out DataTable dtCheckOut)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetCheckOutHistoryData(m_strFirstDate, strEndDate, BALANCEEMP, out dtPayType, out dtCheckOut);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion
        #region ��ʷ��ѯ����
        public long m_lngGetPayTypeAndCheckOutDatahistory(string strDate, string BALANCEEMP, out DataTable dtPayType, out DataTable dtCheckOut)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetPayTypeAndCheckOutDatahistory(strDate, BALANCEEMP, out dtPayType, out dtCheckOut);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #endregion

        #region ��ȡĬ�ϵĴ�ӡ״̬
        /// <summary>
        /// ��ȡĬ�ϵĴ�ӡ״̬
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="STATUSINT">0,Ĭ�ϴ�ӡ��1,����ӡ�� -2��û������Ĭ��ֵ</param>
        /// <returns></returns>
        public long m_lngPrint(string strID, out int STATUSINT)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngPrint(strID, out STATUSINT);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion
        #region ��ȡ�Һŷ�Ʊ�Ĵ�ӡ��ʽ
        /// <summary>
        /// ��ȡ�Һŷ�Ʊ�Ĵ�ӡ��ʽ
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="m_intStatus">0-�Һ�ר�÷�Ʊ;1-�����շѷ�Ʊ</param>
        /// <returns></returns>
        public long m_mthGetRegisterSetting(string strID, out int m_intStatus)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngPrint(strID, out m_intStatus);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion
        #region ��ȡ�˺ŵ�״̬
        /// <summary>
        /// ��ȡ�˺ŵ�״̬
        /// </summary>
        /// <param name="strSetStatus"></param>
        /// <returns></returns>
        public long m_lngGetSetStatus(out int strSetStatus)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetSetStatus(out strSetStatus);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region �˿�ϵͳ
        #region �������ڷ��ؼ�������Ϣ
        public long m_lngGetCarData(string startDate, string endDate, out DataTable dt, string strCardID, string strName)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = proxy.Service.m_lngGetCarData(startDate, endDate, out dt, strCardID, strName);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region �˿�
        public long m_lngReturnCar(string CarID, string patientNO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = proxy.Service.m_lngReturnCar(CarID, patientNO);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region �޸Ŀ���
        public long m_lngUpdateCar(string CarID, string patientNO, string strEmpID, string oldCardID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = proxy.Service.m_lngUpdateCar(CarID, patientNO, strEmpID, oldCardID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region �жϿ����Ƿ��Ѿ�����
        /// <summary>
        /// �жϿ����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns>����3����</returns>
        public long m_lngCheckCarID(string CarID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = proxy.Service.m_lngCheckCarID(CarID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #endregion

        #region ��ԭ�˺�
        public long m_lngResetReg(string strRegisterID, string strResetRegEmpno, string strResetRegDate, out string newID, out int waitNO)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngResetReg(strRegisterID, strResetRegEmpno, strResetRegDate, out newID, out waitNO);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ��鷢Ʊ��
        /// <summary>
        /// ��鷢Ʊ��
        /// </summary>
        /// <param name="strNO"></param>
        /// <returns></returns>
        public long m_lngCheckNO(string strNO, out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngCheckNO(strNO, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ��ȡĳһ���Ű�ƻ�
        public long m_lngGetTodayPlan(out clsOPDoctorPlan_VO[] p_objResult)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            DateTime date = m_GetServTime();
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //   (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            long lngarg = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByday(date.ToShortDateString(), out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngarg;
        }
        public long m_lngGetTodayPlanByDate(string strdate, out clsOPDoctorPlan_VO[] p_objResult)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            long lngarg = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByday(strdate, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngarg;
        }

        public long m_lngGetSomedayPlan(DateTime date, out clsOPDoctorPlan_VO[] p_objResult)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            long lngarg = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByday(date.ToShortDateString(), out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngarg;
        }
        #endregion

        #region ��Ʊ����

        #region ��ȡ���еķ�Ʊ����
        /// <summary>
        /// ��ȡ���еķ�Ʊ����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetAllData(out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetAllData(out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ���Ϸ�Ʊ
        /// <summary>
        /// ���Ϸ�Ʊ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngCancel(string strID, string acctID, DateTime AccDate)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngCancel(strID, acctID, AccDate);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ���ݹ��Ų���Ա��
        /// <summary>
        /// ���ݹ��Ų���Ա��
        /// </summary>
        /// <param name="strNO"></param>
        /// <param name="dtEmp"></param>
        /// <returns></returns>
        public long m_lngfindEmp(string strNO, out DataTable dtEmp)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngfindEmp(strNO, out dtEmp);
            //objSvc.Dispose();
            //objSvc = null;

            return LngArg;
        }
        #endregion

        #region �������
        /// <summary>
        ///������� 
        /// </summary>
        /// <param name="AddRow"></param>
        /// <returns></returns>
        public long m_lngAddNew(DataTable AddRow, out string newID)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngAddNew(AddRow, out newID);
            //objSvc.Dispose();
            //objSvc = null;

            return LngArg;
        }
        #endregion
        #endregion

        #region ��ȡ�շ�Ա�ĵ�����շѼ�¼
        public long m_lngGetOneDayData(string OPREMPID, string strDate, out DataTable dtCheckOut)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetOneDayData(OPREMPID, strDate, out dtCheckOut);
            //objSvc.Dispose();
            //objSvc = null;

            return LngArg;
        }

        #endregion

        #region ��ȡĳһ��ʱ��Ľ��ʼ�¼
        public long m_lngGetPayTypeAndCheckOutBetWeenDate(string startDate, string EndDate, out DataTable dtPayType, out DataTable dtCheckOut, out DataTable dtEmp, string strINTERNALFLAG, string strCheckMan)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetPayTypeAndCheckOutBetWeenDate(startDate, EndDate, out dtPayType, out dtCheckOut, out dtEmp, strINTERNALFLAG, strCheckMan);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }

        #endregion

        #region ��ȡĳһ�յĽ��ʼ�¼
        public long m_lngGetPayTypeAndCheckOutBetWeenDay(string strDate, out DataTable dtPayType, out DataTable dtCheckOut, out DataTable dtEmp, string INTERNALFLAG, string CheckOutName)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetPayTypeAndCheckOutBetWeenDay(strDate, out dtPayType, out dtCheckOut, out dtEmp, INTERNALFLAG, CheckOutName);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ������н���Ա����(���ﴦ����Ʊ��)
        public long m_lngGetCheckMan(out DataTable dtEmpAll, string strINTERNALFLAG)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetCheckMan(out dtEmpAll, strINTERNALFLAG);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ������н���Ա����(�Һ���Ϣ��)
        public long m_lngGetCheckMan(out DataTable dtEmpAll)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetCheckMan(out dtEmpAll);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ����ͳ�Ʊ�
        public long m_lngGetPublicMoney(string startDate, string endDate, out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetPublicMoney(startDate, endDate, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }

        public long m_lngGetGopRla(out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetGopRla(out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ����������ͳ�Ʊ���
        /// <summary>
        /// ����������ͳ�Ʊ���
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="EndDate">����ʱ��</param>
        /// <param name="dtPayType">�����շ�����</param>
        /// <param name="dtCheckOut">�����շ�����</param>
        /// <param name="dtCheckOutDe">�����շ���ϸ���ݱ�</param>
        /// <param name="dtEmp">�����շ�Ա�б�</param>
        /// <param name="isOne">�����շ�ԱID��ALLȫ��ͳ��</param>
        /// <param name="isFull">ָ�Ƿ���������������ݷֿ�ͳ��-1-ͳ���������������(ҽ������0-����ͳ����������(ҽ������1-����ͳ�ƺ������(ҽ����.2-ͳ��������������ݣ����ѣ���3-����ͳ���������ݣ����ѣ���4-����ͳ�ƺ�����ݣ����ѣ���5-ͳ��������������ݣ��Էѣ���6-����ͳ���������ݣ��Էѣ���7-����ͳ�ƺ�����ݣ��Էѣ���8-ͳ��������������ݣ���������9-����ͳ���������ݣ���������10-����ͳ�ƺ�����ݣ�������</param>
        /// <returns></returns>
        public long m_lngGetIatrical(string startDate, string EndDate, out DataTable dtPayType, out DataTable dtCheckOut, out DataTable dtCheckOutDe, out DataTable dtEmp, string isOne, string isFull)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetIatrical(startDate, EndDate, out dtPayType, out dtCheckOut, out dtCheckOutDe, out dtEmp, isOne, isFull);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }


        public long m_lngReturnAllBALANCEEMP(out DataTable dt)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngReturnAllBALANCEEMP(out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ����ܷ���һЩϵͳ���õĲ���
        public bool m_mthIsCanDo(string strID)
        {
            bool isCheck = false;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            isCheck = proxy.Service.clsRegisterSvc_m_mthIsCanDo(strID);
            //objSvc.Dispose();
            //objSvc = null;
            return isCheck;
        }
        #endregion


        #region �����շѲ�ѯģ��
        #region ����ʱ��λ�ȡ��Ʊ����
        /// <summary>
        /// ����ʱ��λ�ȡ��Ʊ����
        /// </summary>
        /// <param name="strDateStart"></param>
        /// <param name="strDateEnd"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeByDate(string strDateStart, string strDateEnd, out DataTable dt, bool p_strOnlySelectVip, bool isWechatRePrt)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetChargeByDate(strDateStart, strDateEnd, out dt, p_strOnlySelectVip, isWechatRePrt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���ݲ�ѯ���������ݻ�ȡ��Ʊ����  add by liuyingrui
        /// <summary>
        /// ���ݲ�ѯ���������ݻ�ȡ��Ʊ����
        /// </summary>
        /// <param name="m_strArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeByCondition(string[] m_strArr, out DataTable dt)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetChargeByCondition(m_strArr, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���ݲ�ѯ���������ݼ�����Ա(�շ�Աid)��ȡ��Ʊ����  add by liuyingrui
        /// <summary>
        /// ���ݲ�ѯ���������ݼ�����Ա(�շ�Աid)��ȡ��Ʊ����
        /// </summary>
        /// <param name="m_strArr"></param>
        /// <param name="dt">
        /// <param name="m_strEmpid"></param>
        /// <returns></returns>
        public long m_lngGetChargeByEmpid(string[] m_strArr, string m_strEmpid, out DataTable dt)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetChargeByEmpID(m_strArr, m_strEmpid, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���ݲ���Ա(�շ�Ա)ID��ʱ��β�ѯ��Ʊ��Ϣ
        /// <summary>
        /// ���ݲ���Ա(�շ�Ա)ID��ʱ��β�ѯ��Ʊ��Ϣ
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeByempid(string strDateStart, string strDateEnd, string empid, out DataTable dt, bool p_strOnlySelectVip, bool isWechatRePrt)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetChargeByempid(strDateStart, strDateEnd, empid, out dt, p_strOnlySelectVip, isWechatRePrt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="blIS">������ 0--�� 1--��ʱ��false ��true �ǣ������� 1--�� 0--��ʱ��false �ǣ�true ��</param>
        /// <param name="strsetid">����ID��</param>
        /// <returns></returns>
        public long m_lngGetCollocate(out bool blIS, string strsetid)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetCollocate(out blIS, strsetid);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// by huafeng.xiao
        /// 2009��9��15��9:31:13
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStatus">����״̬���ʺ϶�״̬����ʹ��</param>
        /// <param name="strsetid">����ID��</param>
        /// <returns></returns>
        public long m_lngGetCollocateStatus(out string p_strStatus, string strsetid)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetCollocateStatus(out p_strStatus, strsetid);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ����ʱ��λ�ȡ��Ʊ����
        /// <summary>
        /// ����ʱ��λ�ȡ��Ʊ����
        /// </summary>
        /// <param name="strDateStart"></param>
        /// <param name="strDateEnd"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeByDate1(string strDateStart, string strDateEnd, out DataTable dt)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetChargeByDate1(strDateStart, strDateEnd, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �����ڲ���Ż�ȡ��Ʊ����¼��Ϣ
        /// <summary>
        /// �����ڲ���Ż�ȡ��Ʊ����¼��Ϣ
        /// </summary>
        /// <param name="seqid"></param>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public long m_lngGetChargeByseqid(string seqid, out DataTable dtRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            long ret = proxy.Service.m_lngGetChargeByseqid(seqid, out dtRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return ret;
        }
        #endregion

        #region ��ȡ��Ʊ������ϸ
        /// <summary>
        /// ��ȡ��Ʊ������ϸ
        /// </summary>
        /// <param name="strINVOICENO"></param>
        /// <param name="strSEQID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeDe(string strINVOICENO, string strSEQID, out DataTable dt)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetChargeDe(strINVOICENO, strSEQID, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ʊ������ϸ
        /// <summary>
        /// ��ȡ��Ʊ������ϸ
        /// </summary>
        /// <param name="strINVOICENO"></param>
        /// <param name="strSEQID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetRecipeDate(string recipeNO, out DataTable dt)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngGetRecipeDate(recipeNO, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ����֤��
        /// <summary>
        /// ��ȡ����֤��
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetPatientCertificateInfo(string strID, out DataTable dt)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_mthGetPatientCertificateInfo(strID, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ʊ������ϸ
        /// <summary>
        /// ��ȡ��Ʊ������ϸ
        /// </summary>
        /// <param name="strINVOICENO"></param>
        /// <param name="strSEQID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngModifiyType(string strType, string strINVOICENO, string strSEQID, string modifiyMan)
        {
            long lngRes;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeCheckSvc));
            lngRes = proxy.Service.m_lngModifiyType(strType, strINVOICENO, strSEQID, modifiyMan);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���ݷ�Ʊ��.��Ʊ�Ż�ȡҽ�����ʵ���
        /// <summary>
        /// ���ݷ�Ʊ��.��Ʊ�Ż�ȡҽ�����ʵ���
        /// </summary>
        /// <param name="InvoNo"></param>
        /// <returns></returns>
        public string m_strGetBillNoByInvoNo(string InvoNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //                (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            string BillNo = (new weCare.Proxy.ProxyOP01()).Service.m_strGetBillNoByInvoNo(InvoNo);
            //objSvc.Dispose();
            //objSvc = null;
            return BillNo;
        }
        #endregion
        #endregion

        public long m_lngGetRegiterByNo(string RegisterID, string strdate, out clsPatientRegister_VO objreg)
        {
            objreg = new clsPatientRegister_VO();
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = proxy.Service.m_lngGetCurRegisterByNo(RegisterID, strdate, out objreg);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        public long m_mthGetPatientInfo(string strPatientType, out int intPayType)
        {
            //com.digitalwave.iCare.middletier.HIS.clsGetPatientType objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetPatientType)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetPatientType));
            long lngRes = proxy.Service.m_mthGetPatientInfo(strPatientType, out intPayType);
            return lngRes;
        }

        #region (������ݶ�Ӧ�ű�)
        /// <summary>
        /// ���ݲ���ID���뻼����������ҳ�֤����(������ݶ�Ӧ�ű�)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPATIENTID_CHR">���߱��</param>
        /// <param name="p_strPAYTYPEID_CHR">�����������</param>
        /// <param name="p_strNo">������Ͷ�Ӧ����</param>
        /// <param name="p_strResultPAYTYPEID_CHR">�������</param>
        /// <param name="p_strPAYTYPENAME_VCHR"></param>
        /// <param name="p_strINTERNALFLAG_INT">0-��ͨ 1-���� 2-ҽ�� 3-���� ���ڲ�ʹ�ã��������֣�</param>
        /// <param name="?"></param>
        /// <returns></returns>
        public long m_lngFindNoByPatientIdAndTypeId(string p_strPATIENTID_CHR, string p_strPAYTYPEID_CHR,
            out string p_strNo,
            out string p_strResultPAYTYPEID_CHR,
            out string p_strPAYTYPENAME_VCHR,
            out string p_strINTERNALFLAG_INT
            )
        {
            p_strNo = null;
            p_strResultPAYTYPEID_CHR = null;
            p_strPAYTYPENAME_VCHR = null;
            p_strINTERNALFLAG_INT = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngFindNoByPatientIdAndTypeId(p_strPATIENTID_CHR, p_strPAYTYPEID_CHR, out p_strNo, out p_strResultPAYTYPEID_CHR, out p_strPAYTYPENAME_VCHR, out p_strINTERNALFLAG_INT);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        /// <summary>
        /// ���Ӳ��������֤����(������ݶ�Ӧ�ű�)
        /// </summary>
        public long m_lngAddPatientIdTypeIdNo(string p_strPATIENTID_CHR, string p_strPAYTYPEID_CHR, string p_strNo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngAddPatientIdTypeIdNo(p_strPATIENTID_CHR, p_strPAYTYPEID_CHR, p_strNo);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        /// <summary>
        /// ���²��������֤����(������ݶ�Ӧ�ű�)
        /// </summary>
        public long m_lngUpdatePatientIdTypeIdNo(string p_strPATIENTID_CHR, string p_strPAYTYPEID_CHR, string p_strNoNew)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngUpdatePatientIdTypeIdNo(p_strPATIENTID_CHR, p_strPAYTYPEID_CHR, p_strNoNew);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }
        #endregion

        #region ���ݷ�Ʊ�Ż�ȡ�������Ӧ��
        /// <summary>
        /// ���ݷ�Ʊ�Ż�ȡ�������Ӧ��
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="paytypeid"></param>
        /// <returns></returns>
        public string m_strGetpatientidentityno(string invo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                   (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            string idno = (new weCare.Proxy.ProxyOP()).Service.m_strGetpatientidentityno(invo);
            //objSvc.Dispose();
            //objSvc = null;
            return idno;
        }
        #endregion

        #region ���ݽ����ˡ�����ʱ���ȡ��Ӧ���ش�Ʊ��Ϣ
        /// <summary>
        /// ���ݽ����ˡ�����ʱ���ȡ��Ӧ���ش�Ʊ��Ϣ
        /// </summary>
        /// <param name="BalanceEmp"></param>
        /// <param name="BalanceTime"></param>
        /// <returns></returns>
        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeQuerySvc));
            (new weCare.Proxy.ProxyOP01()).Service.m_mthGetbalancerepeatinvoinfo(BalanceEmp, BalanceTime, out InvonoArr, status);

        }
        #endregion

        #region ���淢Ʊ�ش���Ϣ
        /// <summary>
        /// ���淢Ʊ�ش���Ϣ
        /// </summary>
        /// <param name="TypeID"> '1' �շѷ�Ʊ '2' �Һŷ�Ʊ</param>
        /// <param name="Seqid"></param>
        /// <param name="Oldinvono"></param>
        /// <param name="Newinvono"></param>
        /// <param name="Empid"></param>
        /// <returns></returns>        
        public long m_lngSaveinvorepeatprninfo(string TypeID, string Seqid, string Oldinvono, string Newinvono, string Empid)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeSvc));

            long l = (new weCare.Proxy.ProxyOP01()).Service.m_lngSaveinvorepeatprninfo(TypeID, Seqid, Oldinvono, Newinvono, Empid);
            //objSvc.Dispose();

            return l;
        }
        #endregion

        #region ��ȡ�Һŷ�Ʊͳ������
        /// <summary>
        /// ��ȡ�Һŷ�Ʊͳ������
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetRegisterStatData(string p_opratorId, string p_beginDate, string p_endDate, out DataTable p_dtbStat)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = proxy.Service.m_lngGetRegisterStatData(p_opratorId, p_beginDate, p_endDate, out p_dtbStat);
            //objSvc.Dispose();
            //objSvc = null;
            return LngArg;
        }

        /// <summary>
        /// ��ȡ�Һŷ�Ʊ�ش�����
        /// </summary>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtbRePrint"></param>
        /// <returns></returns>
        public long m_lngGetBillRePrintData(string p_strOperatorId,
                                            string p_strStartDate,
                                            string p_strEndDate,
                                            out DataTable p_dtbRePrint)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = proxy.Service.GetRegisterBillReprintByDate(p_strOperatorId, p_strStartDate, p_strEndDate, out p_dtbRePrint);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion //��ȡ�Һŷ�Ʊͳ������


    }
    /// <summary>
    /// �����շ��Ż� 
    /// </summary>
    public class clsDomainControl_RegisterDetail : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_RegisterDetail()
        { }

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

        public long m_lngGetRegisterdetail()
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetRegisterdetail( );
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="dt">�������ݵı�</param>
        /// <returns></returns>
        public long m_lngLoadData(out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterDetailSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterDetailSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterDetailSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngLoadData(out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="ID1">�Һ�����ID</param>
        /// <param name="ID2">����ID</param>
        /// <param name="ID3">�ѱ�ID</param>
        /// <param name="PAYMENT_MNY">����</param>
        /// <param name="DISCOUNT_DEC">�Żݱ���</param>
        /// <returns></returns>
        public long m_lngSave(string ID1, string ID2, string ID3, string PAYMENT_MNY, string DISCOUNT_DEC)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRegisterDetailSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterDetailSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterDetailSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngSave(ID1, ID2, ID3, PAYMENT_MNY, DISCOUNT_DEC);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
    }
    public class clsGetIsUsing : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsGetIsUsing()
        {
        }

        /// <summary>
        /// �ж��Ƿ����ù���������Ϊ�Ѿ����ã�
        /// </summary>
        /// <param name="feild">�ֶΣ���ĿID��</param>
        /// <param name="valueid">ֵ����ĿID��</param>m_lngGetIsUsingChargeType
        /// <returns></returns>
        public static bool m_blGetIsUsing(string feild, string valueid)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetIsUsing(feild, valueid);
            //objSvc.Dispose();
            //objSvc = null;

            if (LngArg == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// �жϹҺ������Ƿ����ù���������Ϊ�Ѿ����ã�
        /// </summary>
        /// <param name="feild">�ֶΣ���ĿID��</param>
        /// <param name="valueid">ֵ����ĿID��</param>
        /// <returns></returns>
        public static bool m_blGetIsUsingChargeType(string feild, string valueid)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetIsUsingChargeType(feild, valueid);
            //objSvc.Dispose();
            //objSvc = null;

            if (LngArg == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ɾ���Żݱ�
        /// </summary>
        /// <param name="feild">�ֶΣ���ĿID��</param>
        /// <param name="valueid">ֵ����ĿID��</param>
        /// <returns></returns>
        public static bool m_blDeleteDetail(string feild, string valueid)
        {
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            long LngArg = (new weCare.Proxy.ProxyOP02()).Service.m_blDeleteDetail(feild, valueid);
            //objSvc.Dispose();
            //objSvc = null;

            if (LngArg == 0)
            {
                return false;
            }
            return true;
        }


    }
}
