using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ReportMaintenance 的摘要说明。
    /// </summary>
    public class clsDcl_ReportMaintenance : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ReportMaintenance()
        {
            //
            // TODO: 在此处添加构造函数逻辑
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

        public long m_mthGetReportInfo(string str, out clsReportMain_VO[] objResult)
        {
            long lngRes = 0;
            objResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthGetReportInfo(str, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthGetGroupByID(string str, out clsReportDetail_VO[] objResult)
        {
            long lngRes = 0;
            objResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthGetGroupByID(str, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthGetGroupDetailByID(string strReportID, string strGroupID, string strflag, out clsGroupDetail_VO[] objResult)
        {
            long lngRes = 0;
            objResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthGetGroupDetailByID(strReportID, strGroupID, strflag, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #region 保存报表信息
        public long m_mthAddNewReportInfo(clsReportMain_VO obj_VO)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthAddNewReportInfo(obj_VO);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthUpdateReportInfo(string strID, clsReportMain_VO obj_VO, bool flag)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthUpdateReportInfo(strID, obj_VO, flag);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthDeleteReportByID(string strID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthDeleteReportByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 
        public long m_mthAddNewReportInfo2(clsReportDetail_VO obj_VO)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthAddNewReportInfo2(obj_VO);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthUpdateReportInfo2(string strID, clsReportDetail_VO obj_VO, bool flag)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthUpdateReportInfo2(strID, obj_VO, flag);
            //objSvc.Dispose();
            return lngRes;
        }
        public long m_mthDeleteReportByID2(string strID, string strReportID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthDeleteReportByID2(strID, strReportID);
            //objSvc.Dispose();
            return lngRes;
        }

        public long m_mthSaveGroupDetail(clsGroupDetail_VO[] obj_VO)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReportMaintenanceSvc));
            lngRes = proxy.Service.m_mthSaveGroupDetail(obj_VO);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查找收费特别类别
        public long m_mthGetCat(string strFlag, out clsChargeItemEXType_VO[] objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngFindChargeItemEXTypeListByFlag(strFlag, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
