using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �Ű����ݿ����� Create by Sam 2004-6-3
    /// </summary>
    public class clsDomainConrol_Plan : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrol_Plan()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        //������Ϣ
        #region ��ѯһ������
        /// <summary>
        /// ��ѯһ������
        /// </summary>
        public long m_lngGetDepList(out DataTable p_objResultArr)
        {
            p_objResultArr = null;

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetOPDeptForPlan(out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ѯȫ������ 
        public long m_lngGetDep(out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetOPDeptForPlan(out dt);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ���ݸ�����IDȡ���ӿ�����Ϣ
        /// <summary>
        /// ���ݸ�����IDȡ���ӿ�����Ϣ
        /// </summary>
        public long m_lngGetChildDepList(string strDepID, out clsDepartmentVO[] p_objResultArr)
        {
            p_objResultArr = new clsDepartmentVO[0];

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.lng_getChildrenDep(strDepID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ���ݿ���IDȡ��ҽ����Ϣ
        /// <summary>
        /// ���ݿ���IDȡ��ҽ����Ϣ
        /// </summary>
        public long m_lngGetDocByDepID(string strDepID, out clsEmployeeVO[] p_objResultArr)
        {
            p_objResultArr = new clsEmployeeVO[0];

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetEmployeeList(null, strDepID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
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

        #region ����ID���ҿ��ң����ؿ�����
        /// <summary>
        /// ���ҿ��ң����ؿ�����
        /// </summary>
        public string m_lngGetDepName(string strDepID)
        {
            long lngRes = 0;
            clsDepartmentVO[] p_objResultArr = new clsDepartmentVO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetDeptList(strDepID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            if (lngRes > 0)
            {
                if (p_objResultArr.Length > 0)
                    return p_objResultArr[0].strDeptName;
            }

            return "";
        }
        #endregion
        //���Ű�
        #region ���ݿ���ID������ȡ�����Ű���Ϣ
        /// <summary>
        /// ���ݿ���ID������ȡ�����Ű���Ϣ
        /// </summary>
        public long m_lngGetDayPlan(string strDate, string strDepID, out clsOPDoctorPlan_VO[] p_objResultArr)
        {
            p_objResultArr = new clsOPDoctorPlan_VO[0];

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByDateAndDep(strDate, strDepID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �������Ű���Ϣ
        /// <summary>
        /// �������Ű���Ϣ
        /// </summary>
        public long m_lngAddDayPlan(clsOPDoctorPlan_VO p_objResultArr, out string strPlanID)
        {
            strPlanID = "";
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngDoAddNewDayPlan(p_objResultArr, out strPlanID);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �޸����Ű���Ϣ
        /// <summary>
        /// �޸����Ű���Ϣ
        /// </summary>
        public long m_lngUPDateDayPlan(clsOPDoctorPlan_VO p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngDoUpdDayPlanByID(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ɾ�����Ű���Ϣ
        /// <summary>
        /// ɾ�����Ű���Ϣ
        /// </summary>
        public long m_lngDelDayPlan(string strPlanID)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngDeleteDayPlanByID(strPlanID);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �������Ű���Ϣ
        /// <summary>
        /// �������Ű���Ϣ
        /// </summary>
        public long m_lngCheckDayPlan(string strDate, string strPerio, string strDocID,
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
        //���Ű�
        #region ���ݿ���ID����ȡ�����Ű���Ϣ
        /// <summary>
        /// ���ݿ���ID����ȡ�����Ű���Ϣ
        /// </summary>
        public long m_lngGetWeekPlan(string strWeek, string strDepID, out clsOPDoctorWkPlan_VO[] p_objResultArr)
        {
            p_objResultArr = new clsOPDoctorWkPlan_VO[0];

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByWeekAndDep(strWeek, strDepID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ������ȡ�����Ű���Ϣ
        /// <summary>
        /// ������ȡ�����Ű���Ϣ
        /// </summary>
        public long m_lngGetPlanByWeekAndDepAll(string strWeek, out clsOPDoctorWkPlan_VO[] p_objResultArr)
        {
            p_objResultArr = new clsOPDoctorWkPlan_VO[0];

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByWeekAndDepAll(strWeek, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ������ȡ�����Ű���Ϣ
        /// <summary>
        /// ������ȡ�����Ű���Ϣ
        /// </summary>
        public long m_lngGetPlanByDateAndDepAll(string strDate, out clsOPDoctorPlan_VO[] objPlan)
        {
            objPlan = new clsOPDoctorPlan_VO[0];

            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPlanByDateAndDepAll(strDate, out objPlan);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �������Ű���Ϣ
        /// <summary>
        /// �������Ű���Ϣ
        /// </summary>
        public long m_lngAddWeekPlan(clsOPDoctorWkPlan_VO p_objResultArr, out string strPlanID)
        {
            strPlanID = "";
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngDoAddNewWeekPlan(p_objResultArr, out strPlanID);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion
        #region �޸����Ű���Ϣ
        /// <summary>
        /// �޸����Ű���Ϣ
        /// </summary>
        public long m_lngUPDateWeekPlan(clsOPDoctorWkPlan_VO p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngDoUpdWeekPlanByID(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ɾ�����Ű���Ϣ
        /// <summary>
        /// ɾ�����Ű���Ϣ
        /// </summary>
        public long m_lngDelWeekPlan(string strPlanID)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngDeleteWeekPlanByID(strPlanID);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �������Ű���Ϣ
        /// <summary>
        /// �������Ű���Ϣ
        /// </summary>
        public long m_lngCheckWeekPlan(string strWeek, string strPerio, string strDocID,
            out clsOPDoctorWkPlan_VO p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsOPDoctorWkPlan_VO();
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetDocWeekPlan(strWeek, strPerio, strDocID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region �����ܼƻ�
        public long m_lngCreatePlan(DateTime startDate, DateTime endDate, string strEmp)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngCreatePlan(startDate, endDate, strEmp);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region Ӧ���ܼƻ���ÿ��
        public long m_lngAppWeekPlan(clsOPDoctorWkPlan_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngAppWeek(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ������еĲ�����Ϣ
        public long m_lngGetDept(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetDept(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ������е�Ա����Ϣ
        public long m_lngGetEmployee(string strDepID, out clsEmployeeVO[] dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorPlanSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetOPDoctorListForReg(strDepID, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion
    }
}
