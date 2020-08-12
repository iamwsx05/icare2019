using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// DomainControlMedReport 的摘要说明。
    /// </summary>
    public class DomainControlMedReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public DomainControlMedReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 药品入库明细报表(月)
        /// <summary>
        /// 药品入库明细报表(月)
        /// </summary>
        /// <param name="datestar"></param>
        /// <param name="dateEnd"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtde"></param>
        /// <returns></returns>
        public long m_mthOrdDeByMonth(System.Collections.Generic.List<string> list, out DataTable dtVendor, out DataTable dtde, string strSTANDARD, string strSTORAGETYPEID, string strSIGN, string strIN)
        {
            dtVendor = null;
            dtde = null;
            long lngRes = 0;
            try
            {
                //clsMedReportSvc objMed = (clsMedReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedReportSvc));
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_mthOrdDeByMonth(list, out dtVendor, out dtde, strSTANDARD, strSTORAGETYPEID, strSIGN, strIN);
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        #endregion

        #region 获取调价数据按时间段

        public long m_lngGetChangPriceDataOfMonth(System.Collections.Generic.List<string> arrlist, string strStorageType, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            //clsMedReportSvc objMed = (clsMedReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedReportSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChangPriceDataOfMonth(arrlist, strStorageType, out dt);

            return lngRes;
        }
        #endregion


        #region 获取调价单明细数据

        public long m_lngGetChangPriceDeOfMonth(string strChangPriceID, string strStorageType, out DataTable dt, bool bl)
        {
            dt = new DataTable();
            long lngRes = 0;
            //clsMedReportSvc objMed = (clsMedReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedReportSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChangPriceDeOfMonth(strChangPriceID, strStorageType, out dt, bl);

            return lngRes;
        }
        /// <summary>
        /// 获取调价明细数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <param name="date1">开始日期</param>
        /// <param name="date2">结束日期</param>
        /// <param name="intStatuse">0-全部,1-西药,2-中草药,3-中成药</param>
        /// <returns></returns>
        public long m_lngGetChangPriceDe(out DataTable dt, System.Collections.Generic.List<string> ArrList, int intStatuse, bool bl)
        {
            dt = new DataTable();
            long lngRes = 0;
            //clsMedReportSvc objMed = (clsMedReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedReportSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChangPriceDe(out dt, ArrList, intStatuse, bl);

            return lngRes;
        }
        #endregion
        #region 药品库存积压报表

        public long m_lngGetOverStock(string storageID, int TimeSpace, int intStau, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            //clsMedReportSvc objMed = (clsMedReportSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedReportSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetOverStock(storageID, TimeSpace, intStau, out dt);

            return lngRes;
        }
        #endregion
    }
}
