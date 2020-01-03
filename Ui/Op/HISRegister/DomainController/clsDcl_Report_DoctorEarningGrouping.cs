using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 
    /// </summary>
    class clsDcl_Report_DoctorEarningGrouping : com.digitalwave.GUI_Base.clsDomainController_Base
    {
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

        internal long m_mthSelectDoctorEarningGrouping(string strBeginDat, string strEndDat, string groupid, string[] strTypeOfGh, string[] strTypeOfZc, out DataTable m_dtbReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc));
            long lngRes = proxy.Service.m_lngSelectDoctorEarningGrouping(strBeginDat, strEndDat, groupid, strTypeOfGh, strTypeOfZc, out m_dtbReport);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }



        internal long m_lngSelectGroupIdAndName(string strFindCode, out DataTable m_dtResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc));
            long lngRes = proxy.Service.m_lngSelectGroupIdAndName(strFindCode, out m_dtResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        public long m_mthSelectDoctorEarningWithOutChooseGroup(string strBeginDat, string strEndDat, string[] strTypeOfGh, string[] strTypeOfZc, out DataTable m_dtbReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc));
            long lngRes = proxy.Service.m_lngSelectDoctorEarningCollect(strBeginDat, strEndDat, strTypeOfGh, strTypeOfZc, out m_dtbReport);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;

        }

        #region 按组统计医生挂号费及诊金汇总表-全部(新）

        /// <summary>
        /// 按组统计医生挂号费及诊金汇总表（新）
        /// </summary>
        /// <param name="strBeginDat"></param>
        /// <param name="strEndDat"></param>
        /// <param name="p_strTypeIDArr"></param>
        /// <param name="m_dtbReport"></param>
        /// <returns></returns>
        internal long m_mthGetDoctorEarningWithOutChooseGroup(string strBeginDat, string strEndDat, string[] p_strTypeIDArr1, string[] p_strTypeIDArr2, out DataTable m_dtbReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc));
            long lngRes = proxy.Service.m_lngGetDoctorEarningCollect(strBeginDat, strEndDat, p_strTypeIDArr1, p_strTypeIDArr2, out m_dtbReport);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion

        #region 按组统计医生挂号费及诊金汇总表(新）

        /// <summary>
        /// 按组统计医生挂号费及诊金汇总表（新）
        /// </summary>
        /// <param name="strBeginDat"></param>
        /// <param name="strEndDat"></param>
        /// <param name="p_strTypeIDArr"></param>
        /// <param name="m_dtbReport"></param>
        /// <returns></returns>
        internal long m_lngGetDoctorEarningGrouping(string strBeginDat, string strEndDat, string groupid, string[] p_strTypeIDArr1, string[] p_strTypeIDArr2, out DataTable m_dtbReport)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc));
            long lngRes = proxy.Service.m_lngGetDoctorEarningGrouping(strBeginDat, strEndDat, groupid, p_strTypeIDArr1, p_strTypeIDArr2, out m_dtbReport);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion

        #region 取核算分类
        /// <summary>
        /// 取核算分类
        /// </summary>
        /// <param name="strBeginDat"></param>
        /// <param name="strEndDat"></param>
        /// <param name="strTypeOfGh"></param>
        /// <param name="strTypeOfZc"></param>
        /// <param name="m_dtbReport"></param>
        /// <returns></returns>
        internal long m_lngGetTypeID(string p_strRptID, string p_strGroupID, out DataTable p_dtbTypeID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportEarningSvc));
            long lngRes = proxy.Service.m_lngGetTypeID(p_strRptID, p_strGroupID, out p_dtbTypeID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion

    }
}
