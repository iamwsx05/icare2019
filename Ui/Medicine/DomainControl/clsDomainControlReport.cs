using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlReport 的摘要说明。
    /// </summary>
    public class clsDomainControlReport : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        public clsDomainControlReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region  中标药品统计报表  张国良 2005-02-29
        /// <summary>
        /// 新增帐务期
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        public long m_lngGetStanMed(out System.Data.DataTable p_datStanMed)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageReportSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageReportSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStanMed(out p_datStanMed);

            return lngRes;
        }
        #endregion
    }
}
