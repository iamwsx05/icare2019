using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_ReportManage 的摘要说明。
    /// 刘彬 2004.05.26
    /// </summary>
    public class clsDomainController_ReportManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDomainController_ReportManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 根据报告组ID获取报告组VO 童华 2004.06.21
        public long m_lngGetReportGroupVOByReportGroupID(string p_strReportGroupID, out clsReportGroup_VO p_objReportGroupVO)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReportGroupVOByReportGroupID(p_strReportGroupID, out p_objReportGroupVO);
            //			objReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据检验标本组ID得到它所在报告组的VO,及打印顺序 刘彬 2004.06.1
        public long m_lngGetReportGoupVOBySampleGroupID(string p_strSampleGroupID, out clsReportGroup_VO p_objReportGroupVO)
        {
            long lngRes = 0;
            p_objReportGroupVO = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReportGoupVOBySampleGroupID(p_strSampleGroupID, out p_objReportGroupVO);
            //			objReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region 删除报告组及其明细 童华 2004.05.26
        public long m_lngDelReportGroupAndDetail(string strReportGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelReportGroupAndDetail(strReportGroupID);
            //			objReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除报告组明细 童华 2004.05.26
        public long m_lngDelReportGroupDetail(string strReportGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelReportGroupDetail(strReportGroupID);
            //			objReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 保存报告组及其明细 童华 2004.05.26
        public long m_lngAddReportGroupAndDetail(ref clsReportGroup_VO objReportGroupVO, ref clsReportGroupDetail_VO[] objReportGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddReportGroupAndDetail(ref objReportGroupVO, ref objReportGroupDetailVOList);
            //			objReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有的报告组明细列表 童华 2004.05.26
        public long m_lngGetAllReportGroupDetail(out clsReportGroupDetail_VO[] objReportGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllReportGroupDetail(out objReportGroupDetailVOList);
            //			clsReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有的报告组及其明细资料 童华 2004.05.25
        public long m_lngGetAllReportGroup(out clsReportGroup_VO[] objReportGroupVOList)
        {
            long lngRes = 0;
            objReportGroupVOList = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllReportGroup(ref objReportGroupVOList);
            //			objReportGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取系统参数的值
        /// <summary>
        /// 获取系统参数的值
        /// </summary>
        /// <param name="p_strParm"></param>
        /// <param name="p_strValue"></param>
        /// <returns></returns>
        public long m_lngGetSysParmValue(string p_strParm, out string p_strValue)
        {
            long lngRes = proxy.Service.m_lngGetCollocate(out p_strValue, p_strParm);
            return lngRes;
        }
        #endregion
    }
}
