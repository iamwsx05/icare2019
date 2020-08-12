using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.DLL
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_MedStoLimit:数据控制类 Create by Sam 2004-5-24
    /// </summary>
    public class clsDomainControl_MedStoLimit : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_MedStoLimit()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 获取所有的仓库
        /// <summary>
        /// 获取所有的仓库
        /// </summary>
        public long m_lngGetStorageList(out DataTable strStorageArr)
        {
            strStorageArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageList(out strStorageArr);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取所有的仓库类型
        /// <summary>
        /// 获取所有的仓库类型
        /// </summary>
        public long m_lngGetStorageTypeList(out DataTable strStorageTypeArr)
        {
            strStorageTypeArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageTypeList(out strStorageTypeArr);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据仓库ID取回药品限额
        /// <summary>
        /// 根据仓库ID取回药品限额
        /// </summary>
        public long m_lngGetLimitByStoID(string strStoID, string p_strStorageFlag, out DataTable p_dbtResultArr)
        {
            p_dbtResultArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindLimitByStoID(strStoID, p_strStorageFlag, out p_dbtResultArr);
            return lngRes;
        }
        #endregion

        #region 根据类型获取药品基本资料
        /// <summary>
        /// 根据类型获取药品基本资料
        /// </summary>
        public long m_lngGetMedListByType(string strMedTypeID, out DataTable dbtMedArr)
        {
            dbtMedArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedListByType(strMedTypeID, out dbtMedArr);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region Add限额
        /// <summary>
        /// Add限额
        /// </summary>
        public long m_lngAddLimit(DataTable AddRow)
        {
            //p_objResultArr = new clsStorageMedLimit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewLimit(AddRow);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改限额
        /// <summary>
        /// 修改限额
        /// </summary>
        public long m_lngUpLimit(DataTable UpdataRow)
        {
            //p_objResultArr = new clsStorageMedLimit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdLimitByKey(UpdataRow);
            return lngRes;
        }
        #endregion

        #region 删除限额
        /// <summary>
        /// 删除限额
        /// </summary>
        public long m_lngDelLimit(string strStorageID, string strMedID)
        {
            //p_objResultArr = new clsStorageMedLimit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteLimitByKey(strStorageID, strMedID);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取仓库警戒报告
        public long m_lngGetMedWatchRpt(string p_strStorageID, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedWatchRpt(p_strStorageID, out dtResult);
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion 获取仓库警戒报告
    }
}
