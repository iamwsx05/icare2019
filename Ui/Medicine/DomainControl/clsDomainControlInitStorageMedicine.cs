using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    #region  原始库存DomainControl
    /// <summary>
    /// 原始库存
    /// Create kong by 2004-06-08
    /// </summary>
    public class clsDomainControlInitStorageMedicine : com.digitalwave.GUI_Base.clsDomainController_Base    //GUI_Base.dll
    {
        public clsDomainControlInitStorageMedicine()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 新增原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 新增原始库存记录
        /// </summary>
        /// <param name="p_objItem">原始库存信息</param>
        /// <returns></returns>
        public long m_lngDoAddNewInitStorageMedicine(clsInitStorageMedicine_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewInitStorageMedicine(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 修改原始库存记录
        /// </summary>
        /// <param name="p_objItem">原始库存信息</param>
        /// <returns></returns>
        public long m_lngDoUpdateInitStorageMedicine(clsInitStorageMedicine_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdInitStorageMedicine(p_objItem);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  删除原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 删除原始库存记录
        /// </summary>
        /// <param name="p_strStorageID">仓库</param>
        /// <param name="p_strMedicineID">药品</param>
        /// <returns></returns>
        public long m_lngDoDeleteInitStorageMedicine(string p_strStorageID, string p_strMedicineID)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteInitStorageMedicine(p_strStorageID, p_strMedicineID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  模糊查找原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 模糊查找原始库存记录
        /// </summary>
        /// <param name="p_strSQL">SQL脚本</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns>返回值</returns>
        public long m_lngFindInitStorageMedicineByAny(string p_strSQL, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByAny(p_strSQL, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  按仓库ID和药品ID查找原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 按仓库和药品查找原始库存信息
        /// </summary>
        /// <param name="p_strStorageID">仓库</param>
        /// <param name="p_strMedicineID">药品</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns>返回值</returns>
        public long m_lngFindInitStorageMedicineByKey(string p_strStorageID, string p_strMedicineID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByKey(p_strStorageID, p_strMedicineID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  按仓库查找原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 按仓库查找原始库存记录
        /// </summary>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns>返回值</returns>
        public long m_lngFindInitStorageMedicineByStorageID(string p_strStorageID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByStorageID(p_strStorageID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  按药品查找原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 按药品查找原始库存记录
        /// </summary>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns>返回值</returns>
        public long m_lngFindInitStorageMedicineByMedicineID(string p_strMedicineID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindInitStorageMedicineByMedicineID(p_strMedicineID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  查找所有原始库存记录  欧阳孔伟  2004-06-08
        /// <summary>
        /// 查找所有原始库存记录
        /// </summary>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns>返回值</returns>
        public long m_lngFindAllInitStorageMedicine(out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInitStorageMedicineSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllInitStorageMedicine(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
    #endregion
}
