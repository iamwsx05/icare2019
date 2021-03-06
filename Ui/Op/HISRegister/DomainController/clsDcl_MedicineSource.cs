using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_MedicineSource 的摘要说明。
    /// </summary>
    public class clsDcl_MedicineSource : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_MedicineSource()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP01();
            }
        }
        #endregion

        #region 查询收费项目

        public long m_mthFindChargeItem(string strType, string strContent, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc));
            long lngRes = proxy.Service.m_mthFindChargeItem(strType, strContent, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询项目源

        public long m_mthFindChargeItemSource(out DataTable dt2)
        {
            dt2 = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc));
            long lngRes = proxy.Service.m_lngFindAllSour(out dt2);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 保存数据

        public long m_mthSaveData(string strItemID, string strSourceID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSourceSvc));
            long lngRes = proxy.Service.m_mthSaveData(strItemID, strSourceID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找药房数据
        /// <summary>
        /// 查找药房数据
        /// </summary>
        public long m_lngGetMedStoreData(string p_strWhere, out DataTable p_dtbResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc));
            long lngRes = proxy.Service.m_lngGetMedStoreData(p_strWhere, out p_dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找项目数据
        /// <summary>
        /// 查找项目数据
        /// </summary>
        public long m_lngGetItemData(out DataTable p_dtbResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc));
            long lngRes = proxy.Service.m_lngGetItemData(out p_dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找门诊药房药品发放清单报表数据
        /// <summary>
        /// 查找门诊药房药品发放清单报表数据
        /// </summary>
        public long m_lngGetMedSendItemData(string p_strRecordBeginDate, string p_strRecordEndDate, string[] p_strMedstoreIdArr, string[] p_strStatus, string p_strOrderBy, string p_strSingleItemName, out DataTable p_dtbResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPMedStoreSvc));
            long lngRes = proxy.Service.m_lngGetMedSendItemData(p_strRecordBeginDate, p_strRecordEndDate, p_strMedstoreIdArr, p_strStatus, p_strOrderBy, p_strSingleItemName, out p_dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
