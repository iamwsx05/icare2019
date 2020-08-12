using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlStorageQuery 的摘要说明。
    /// </summary>
    public class clsDomainControlStorageQuery : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlStorageQuery()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取仓库信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetStorageInfo(string strStorageFlag, out DataTable dt)
        {
            long lngRes = 0;
            dt = null;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArr(strStorageFlag, out dt);

            return lngRes;
        }
        /// <summary>
        /// 获取仓库库存信
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_outDs"></param>
        /// <returns></returns>
        public long m_lngGetMedStock(string p_strStorageID, string p_strStorageFlag, bool blIsShowZer, out DataSet p_outDs)
        {
            long lngRes = 0;
            p_outDs = null;
            try
            {
                //clsStorageMedicineSvc objMed = (clsStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageMedicineSvc));
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStock(p_strStorageID, p_strStorageFlag, blIsShowZer, out p_outDs);
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }


        /// <summary>
        /// 查询药品出入库信息
        /// </summary>
        /// <param name="MedID"></param>
        /// <param name="strBegion"></param>
        /// <param name="strEnd"></param>
        /// <param name="strStatus"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngOutInReMark(string MedID, System.Collections.Generic.List<string> list, string strStatus, out System.Data.DataTable dtbResult, bool ismed)
        {
            long lngRes = 0;
            dtbResult = null;
            try
            {
                //clsStorageMedicineSvc objMed = (clsStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageMedicineSvc));
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOutInReMark(MedID, list, strStatus, out dtbResult, ismed);
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="dtvendor"></param>
        /// <returns></returns>
        public long m_lngGetAllVendor(out System.Data.DataTable dtvendor)
        {
            long lngRes = 0;
            dtvendor = null;
            try
            {
                //clsStorageMedicineSvc objMed = (clsStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageMedicineSvc));
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetAllVendor(out dtvendor);
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
    }
}
