using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    class clsDcl_MedIcineCheckReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        internal long m_lngGetMedIcineCheck(string strVendor, int intMedicinetype, string strDateStar, string strDateEnd, string STORAGEID, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedIcineCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedIcineCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedIcineCheckSVC));
            //lngRes = objSvc.m_lngGetGrossProfitRateSet( out p_objRateArr);
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetMedIcineCheck(strVendor, intMedicinetype, strDateStar, strDateEnd, STORAGEID, out dt);
            return lngRes;
        }
        #region 获取供应商


        /// <summary>
        /// 获取供应商

        /// </summary>
        /// <param name="p_dtbVendor">供应商数据</param>
        /// <returns></returns>
        internal long m_lngGetVendor(out DataTable p_dtbVendor)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetVendor(out p_dtbVendor);
            return lngRes;
        }
        #endregion

        #region 获取指定仓库的药品类型

        /// <summary>
        /// 获取指定仓库的药品类型

        /// </summary>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_objMTVO">药品制剂类型</param>
        /// <returns></returns>
        internal long m_lngGetStorageMedicineType(string p_strStorageID, out clsMS_MedicineType_VO[] p_objMTVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_PublicSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStorageMedicineType(p_strStorageID, out p_objMTVO);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 入库统计(外退)
        /// </summary>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_dtmBegin">查询开始时间</param>
        /// <param name="p_dtmEnd">查询结束时间</param>
        /// <param name="p_strVendorID">供应商ID</param>
        /// <param name="p_dtbData">统计结果</param>
        /// <returns></returns>
        internal long m_lngStatisticsForeignRetreat(string p_strStorageID, DateTime p_dtmBegin, DateTime p_dtmEnd, string p_strVendorID, int p_intMedicineSetID, out DataTable p_dtbData)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMedIcineCheckSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMedIcineCheckSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMedIcineCheckSVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngStatisticsForeignRetreat(p_strStorageID, p_dtmBegin, p_dtmEnd, p_strVendorID, p_intMedicineSetID, out p_dtbData);
            return lngRes;
        }
    }
}
