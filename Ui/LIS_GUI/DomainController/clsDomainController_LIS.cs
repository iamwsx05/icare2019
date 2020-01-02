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
        #region 根据条件组合查询工作量报表数据 童华 2004.06.22
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

        //		#region 查询标本的报告单信息 童华 2004.05.14
        //		public long m_lngGetAllReportInfo(string strFromDat,string strToDat,string strDeviceID,string strSampleFrom,string strSampleTo,string strPatientType,string strDept,string strPatientCardID,string strPatientName,out weCare.Core.Entity.clsBatchReport_VO[] objBatchReportList)
        //		{
        //			long lngRes = 0;
        //			com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc objCheckResultSvc = (com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc));
        //			lngRes = objCheckResultSvc.m_lngGetAllReportInfo(objPrincipal,strFromDat,strToDat,strDeviceID,strSampleFrom,strSampleTo,strPatientType,strDept,strPatientCardID,strPatientName,out objBatchReportList);
        ////			objCheckResultSvc.Dispose();
        //			return lngRes;
        //		}
        //		#endregion

        #region 查询所有的科室类别信息 童华 2004.05.13
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

        #region 查询所有的病人类型信息 童华 2004.05.13
        public long m_lngGetPatientType(out weCare.Core.Entity.clsDict_VO[] objPatientTypeList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyPatient()).Service.m_lngGetPatientType(out objPatientTypeList);
            //			objPatientSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询某一检验单组或自定义组下的所有基本组信息 童华 2004.05.12
        public long m_lngQryAllBseCheckGroupBYGroupID(string strCheckGroupID, out weCare.Core.Entity.clsCheckGroup_VO[] objCheckGroupVOList)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngQryBseGroup(strCheckGroupID, out objCheckGroupVOList);

            return lngRes;
        }
        #endregion

        #region 根据组合类别查询相关的组合信息 童华 2004.05.11
        public long m_lngGetCheckGroupByCheckGroupType(string strHasSubGroup, out weCare.Core.Entity.clsCheckGroup_VO[] objCheckGroup)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetCheckGroupByCheckGroupType(strHasSubGroup, out objCheckGroup);

            return lngRes;
        }
        #endregion

        #region 判断t_opr_lis_application_detail的某一检验组下的所有样本的标志位状态，并且设置t_opr_lis_application_detail的标志位 童华 2004.04.26
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSampleID">当前样本的ID号</param>
        /// <param name="strApplFormNo">当前样本所属的申请单号</param>
        /// <param name="strApplID">当前样本所属的申请单ID号</param>
        /// <param name="strSampleStatus">当前样本的状态</param>
        /// <param name="strSetApplDetailStatus">设置t_opr_lis_application_detail的状态</param>
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

        #region 设置t_opr_lis_application_detail的标志位 童华 2004.04.26
        public long m_lngSetStatusToConfirmByApplicationIDAndGroupID(string strGroupID, string strApplID, string strSetApplDetailStatus)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetStatusToConfirmByApplicationIDAndGroupID(strGroupID, strApplID, strSetApplDetailStatus);
            return lngRes;
        }
        #endregion

        #region 设置标本的状态 童华 2004.04.26
        /// <summary>
        /// 设置标本的状态
        /// </summary>
        /// <param name="strSampleID">标本的ID号</param>
        /// <param name="strSampleStatus">标本设置后的状态值</param>
        /// <returns></returns>
        public long m_lngSetSampleStatus(string strSampleID, int intSampleStatus)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSetSampleStatusBySampleId(strSampleID, intSampleStatus);
            return lngRes;
        }
        #endregion

        #region 查询所有未采集的申请单信息 童华 2004.04.26
        public long m_lngGetAllNoCollectedByApplDat(string strFromDat, string strToDat, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllNoCollectSampleByApplDat(strFromDat, strToDat, out dtbApplInfo);
            return lngRes;
        }
        #endregion

        #region 查询所有已采集的申请单信息 童华 2004.04.26
        public long m_lngGetAllCollectedByApplDat(string strFromDat, string strToDat, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllCollectedApplInfoByApplDat(strFromDat, strToDat, out dtbApplInfo);
            return lngRes;
        }
        #endregion

        #region 查询所有申请单信息 童华 2004.04.26
        public long m_lngGetAllApplInfoByApplDat(string strFromDat, string strToDat, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            lngRes = proxy.Service.m_lngGetAllSendApplInfoByApplDat(strFromDat, strToDat, out dtbApplInfo);
            return lngRes;
        }
        #endregion

        #region 根据检验申请表号和检验组（第一层）查询已经采集的各标本数量 童华 2004.04.26
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

        #region 根据GroupID查询该检验组需要的样本信息 童华 2004.04.26
        public long m_lngGetSampleCountInfoByGroupID(string strGroupID, out DataTable dtbGroupSampleCount)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetSampleTypeInfo(strGroupID, out dtbGroupSampleCount);
            return lngRes;
        }
        #endregion

        #region 根据诊疗卡号查询申请单信息 童华 2004.04.26
        public long m_lngGetApplInfoByPatientCardID(string strPatientCardID, out DataTable dtbApplInfo)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngApplInfoByPatientCardID(strPatientCardID, out dtbApplInfo);
            return lngRes;
        }
        #endregion

    }
}
