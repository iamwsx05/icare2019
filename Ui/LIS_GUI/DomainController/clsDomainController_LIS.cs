using System;
using System.Data;
using com.digitalwave.iCare.common;//ObjectGenerator.dll 
using com.digitalwave.GUI_Base;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsDomainController_LIS.
    /// </summary>
    public class clsDomainController_LIS : clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public clsDomainController_LIS()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region ����������ϲ�ѯ�������������� ͯ�� 2004.06.22
        public long m_lngGetWorkloadReportByCondition(string p_strFromDat, string p_strToDat, string p_strCheckItemID, string p_strApplEmpID,
            string p_strApplDeptID, string p_strPatientTypeID, string p_strCheckEmpID, string p_strCheckCategoryID, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetWorkloadReportByCondition(p_strFromDat, p_strToDat, p_strCheckItemID, p_strApplEmpID, p_strApplDeptID,
               p_strCheckEmpID, p_strPatientTypeID, p_strCheckCategoryID, out dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        //		#region ��ѯ�걾�ı��浥��Ϣ ͯ�� 2004.05.14
        //		public long m_lngGetAllReportInfo(string strFromDat,string strToDat,string strDeviceID,string strSampleFrom,string strSampleTo,string strPatientType,string strDept,string strPatientCardID,string strPatientName,out weCare.Core.Entity.clsBatchReport_VO[] objBatchReportList)
        //		{
        //			long lngRes = 0;
        //			com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc objCheckResultSvc = (com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc));
        //			lngRes = objCheckResultSvc.m_lngGetAllReportInfo(objPrincipal,strFromDat,strToDat,strDeviceID,strSampleFrom,strSampleTo,strPatientType,strDept,strPatientCardID,strPatientName,out objBatchReportList);
        ////			objCheckResultSvc.Dispose();
        //			return lngRes;
        //		}
        //		#endregion

        #region ��ѯ���еĿ��������Ϣ ͯ�� 2004.05.13
        public long m_lngGetDeptInfo(out weCare.Core.Entity.clsDepartmentVO[] objDepartmentVOList)
        {
            long lngRes = 0;
            objDepartmentVOList = null;
            DataTable dt = null;
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetDeptInfo(out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                objDepartmentVOList = new clsDepartmentVO[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objDepartmentVOList[i] = new clsDepartmentVO();
                    objDepartmentVOList[i].strDeptID = dt.Rows[i]["DEPTID_CHR"].ToString();
                    objDepartmentVOList[i].strDeptName = dt.Rows[i]["DEPTNAME_VCHR"].ToString();
                    objDepartmentVOList[i].strShortNo = dt.Rows[i]["code_vchr"].ToString();
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ���еĲ���������Ϣ ͯ�� 2004.05.13
        public long m_lngGetPatientType(out weCare.Core.Entity.clsDict_VO[] objPatientTypeList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyPatient()).Service.m_lngGetPatientType(out objPatientTypeList);
            //			objPatientSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯĳһ���鵥����Զ������µ����л�������Ϣ ͯ�� 2004.05.12
        public long m_lngQryAllBseCheckGroupBYGroupID(string strCheckGroupID, out weCare.Core.Entity.clsCheckGroup_VO[] objCheckGroupVOList)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngQryBseGroup(strCheckGroupID, out objCheckGroupVOList);

            return lngRes;
        }
        #endregion

        #region �����������ѯ��ص������Ϣ ͯ�� 2004.05.11
        public long m_lngGetCheckGroupByCheckGroupType(string strHasSubGroup, out weCare.Core.Entity.clsCheckGroup_VO[] objCheckGroup)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetCheckGroupByCheckGroupType(strHasSubGroup, out objCheckGroup);

            return lngRes;
        }
        #endregion

        #region �ж�t_opr_lis_application_detail��ĳһ�������µ����������ı�־λ״̬����������t_opr_lis_application_detail�ı�־λ ͯ�� 2004.04.26
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSampleID">��ǰ������ID��</param>
        /// <param name="strApplFormNo">��ǰ�������������뵥��</param>
        /// <param name="strApplID">��ǰ�������������뵥ID��</param>
        /// <param name="strSampleStatus">��ǰ������״̬</param>
        /// <param name="strSetApplDetailStatus">����t_opr_lis_application_detail��״̬</param>
        /// <returns></returns>
        public long m_lngSetApplDetailSatausBySampleSataus(string strSampleID, string strApplFormNo, string strApplID, string strSampleStatus, string strSetApplDetailStatus)
        {
            long lngRes = 0;
            DataTable dtbGroupSample = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleStatusByGroup(strSampleID, strApplFormNo, out dtbGroupSample);
            int count = dtbGroupSample.Rows.Count;
            if (count > 0)
            {
                bool bolRecepted = true;
                for (int i = 0; i < count; i++)
                {
                    string strStatus = dtbGroupSample.Rows[i]["status_int"].ToString().Trim();
                    if (strStatus != strSampleStatus)
                    {
                        bolRecepted = false;
                    }
                }
                if (bolRecepted)
                {
                    this.m_lngSetStatusToConfirmByApplicationIDAndGroupID(dtbGroupSample.Rows[0]["groupid_chr"].ToString().Trim(), strApplID, strSetApplDetailStatus);
                }
            }
            return lngRes;
        }
        #endregion

        #region ����t_opr_lis_application_detail�ı�־λ ͯ�� 2004.04.26
        public long m_lngSetStatusToConfirmByApplicationIDAndGroupID(string strGroupID, string strApplID, string strSetApplDetailStatus)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetStatusToConfirmByApplicationIDAndGroupID(strGroupID, strApplID, strSetApplDetailStatus);
            return lngRes;
        }
        #endregion

        #region ���ñ걾��״̬ ͯ�� 2004.04.26
        /// <summary>
        /// ���ñ걾��״̬
        /// </summary>
        /// <param name="strSampleID">�걾��ID��</param>
        /// <param name="strSampleStatus">�걾���ú��״ֵ̬</param>
        /// <returns></returns>
        public long m_lngSetSampleStatus(string strSampleID, int intSampleStatus)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSetSampleStatusBySampleId(strSampleID, intSampleStatus);
            return lngRes;
        }
        #endregion

        #region ��ѯ����δ�ɼ������뵥��Ϣ ͯ�� 2004.04.26
        public long m_lngGetAllNoCollectedByApplDat(string strFromDat, string strToDat, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllNoCollectSampleByApplDat(strFromDat, strToDat, out dtbApplInfo);
            return lngRes;
        }
        #endregion

        #region ��ѯ�����Ѳɼ������뵥��Ϣ ͯ�� 2004.04.26
        public long m_lngGetAllCollectedByApplDat(string strFromDat, string strToDat, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllCollectedApplInfoByApplDat(strFromDat, strToDat, out dtbApplInfo);
            return lngRes;
        }
        #endregion

        #region ��ѯ�������뵥��Ϣ ͯ�� 2004.04.26
        public long m_lngGetAllApplInfoByApplDat(string strFromDat, string strToDat, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllSendApplInfoByApplDat(strFromDat, strToDat, out dtbApplInfo);
            return lngRes;
        }
        #endregion

        #region ���ݼ��������źͼ����飨��һ�㣩��ѯ�Ѿ��ɼ��ĸ��걾���� ͯ�� 2004.04.26
        public long m_lngGetGroupSampleCountByApplFormNoAndGorupID(string strApplFormNo, string strGroupID, out DataTable dtbGroupSampleCount)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            dtbGroupSampleCount = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllSampleCountByApplFormNoAndGroupID(strApplFormNo, strGroupID, out dtbGroupSampleCount);
            //			objSampleSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����GroupID��ѯ�ü�������Ҫ��������Ϣ ͯ�� 2004.04.26
        public long m_lngGetSampleCountInfoByGroupID(string strGroupID, out DataTable dtbGroupSampleCount)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetSampleTypeInfo(strGroupID, out dtbGroupSampleCount);
            return lngRes;
        }
        #endregion

        #region �������ƿ��Ų�ѯ���뵥��Ϣ ͯ�� 2004.04.26
        public long m_lngGetApplInfoByPatientCardID(string strPatientCardID, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngApplInfoByPatientCardID(strPatientCardID, out dtbApplInfo);
            return lngRes;
        }
        #endregion

    }
}
