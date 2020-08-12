using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrolStorageSetupStar 的摘要说明。
    /// </summary>
    public class clsDomainConrolStorageSetupStar : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrolStorageSetupStar()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 获取药品基本信息
        /// <summary>
        /// 获取药品基本信息
        /// </summary>
        /// <param name="p_objStockMedAppl"></param>
        /// <returns></returns>
        public long m_lngGetAllMedicine(out DataTable dtbMedicine)
        {
            long lngRes = 0;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMedicine(out dtbMedicine);
            return lngRes;
        }
        #endregion


        #region 获得仓库信息
        /// <summary>
        /// 获得仓库信息
        /// </summary>
        /// <param name="strStorageFlag"></param>
        /// <param name="dtStorage"></param>
        /// <returns></returns>
        public long m_lngGetStorageArr(string strStorageFlag, out DataTable dtStorage)
        {
            long lngRes = 0;
            dtStorage = null;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArr(strStorageFlag, out dtStorage);

            return lngRes;
        }
        #endregion

        #region 获得生产厂家
        /// <summary>
        /// 获得生产厂家
        /// </summary>
        /// <param name="dtVerdor"></param>
        /// <returns></returns>
        public long m_lngGetVerdorArr(out DataTable dtVerdor)
        {
            long lngRes = 0;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetVerdorArr(out dtVerdor);
            return lngRes;
        }
        #endregion

        #region 根据仓库ID获得该仓库所有药品的初始化信息
        /// <summary>
        /// 根据仓库ID获得该仓库所有药品的初始化信息
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="dtStorageInit"></param>
        /// <returns></returns>
        public long m_lngGetStorageArrByID(string storageID, string strStorageFlag, out DataTable dtStorageInit, string strWhere)
        {
            long lngRes = 0;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArrByID(storageID, strStorageFlag, out dtStorageInit, strWhere);
            return lngRes;
        }
        #endregion

        #region 保存库存初始化信息
        /// <summary>
        /// 保存库存初始化信息
        /// </summary>
        /// <param name="SaveRow"></param>
        /// <returns></returns>
        public long m_lngSaveSetup(DataTable SaveRow)
        {
            long lngRes = 0;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngSaveSetup(SaveRow);
            }
            catch
            {
            }
            return lngRes;
        }
        #endregion

        public long m_lngSaveStorageInit(DataTable dt, DataTable dtDel)
        {
            long lngRes = 0;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngSaveSetup(dt, dtDel);
                if (lngRes < 0)
                {
                    return -1;
                }

            }
            catch
            {
            }
            return lngRes;
        }
        public long m_lngAuditStorageInit(DataTable dt, string p_strStorageFlag)
        {
            long lngRes = 0;
            //clsStorageSetupStarSVC objSVC = (clsStorageSetupStarSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageSetupStarSVC));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAuditInit(dt, p_strStorageFlag);
                if (lngRes < 0)
                {
                    return -1;
                }

            }
            catch
            {
            }
            return lngRes;
        }
    }
}
