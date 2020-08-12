using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDomainControlMedicineReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlMedicineReport()
        {
        }

        #region 获取药品目录
        /// <summary>
        /// 获取药品目录
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedicineList(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedicineList(out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region 根据条件获取药品信息
        /// <summary>
        /// 根据条件获取药品信息
        /// </summary>
        /// <param name="p_strVal"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngImpMedItem(string p_strVal, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngImpMedItem(p_strVal, out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region 获取药品信息
        /// <summary>
        /// 获取药品信息
        /// </summary>
        /// <param name="p_strVal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedItem(string p_strVal, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedItem(p_strVal, out p_dtbResult);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 医生用药
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time2"></param>
        /// <param name="p_strVal"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngStatDoctUseMed(DateTime time, DateTime time2, string p_strVal, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngStatDoctUseMed(time, time2, p_strVal, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// 库存
        /// </summary>
        /// <param name="str"></param>
        /// <param name="table2"></param>
        /// <returns></returns>
        public long m_lngStatMedStock(string str, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngStatMedStock(str, out p_dtbResult);
            return lngRes;
        }

        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time2"></param>
        /// <param name="str"></param>
        /// <param name="table2"></param>
        /// <returns></returns>
        public long m_lngStatMedInOutStore(DateTime time, DateTime time2, string str, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineReportSVC));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngStatMedInOutStore(time, time2, str, out p_dtbResult);
            return lngRes;
        }
    }
}
