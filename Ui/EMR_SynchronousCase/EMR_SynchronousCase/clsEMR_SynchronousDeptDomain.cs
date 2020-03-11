using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 科室同步
    /// </summary>
    public class clsEMR_SynchronousDeptDomain : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 获取iCare科室列表
        /// <summary>
        /// 获取iCare科室列表
        /// </summary>
        /// <param name="p_intDeptType">=0时获取专业组,=1时获取科室</param>
        /// <param name="p_objDeptArr">科室列表</param>
        /// <returns></returns>
        public long m_lngGetICareDeptList(int p_intDeptType, out clsEmrDept_VO[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //       (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetICareDeptList(  p_intDeptType, out p_objDeptArr);
            return lngRes;
            //com.digitalwave.emr.HospitalManagerService.clsHospitalManagerService serv = new com.digitalwave.emr.HospitalManagerService.clsHospitalManagerService();
            //serv.m_lngGetAllDeptInfoByAttributeid(null,
        }
        #endregion

        #region 获取病案系统科室列表
        /// <summary>
        /// 获取病案系统科室列表
        /// </summary>
        /// <param name="p_objDeptArr">科室列表</param>
        /// <returns></returns>
        public long m_lngGetBADeptList(out clsEmrDept_VO[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //       (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetBADeptList(  out p_objDeptArr);
            return lngRes;
        }
        #endregion

        #region 修改专业组科室关联

        /// <summary>
        /// 修改专业组科室关联
        /// </summary>
        /// <param name="p_strGroupID">专业组ID</param>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <param name="p_intDeptType">=0时获取专业组,=1时获取科室</param>
        /// <param name="p_strICareDeptName">科室名称</param>
        /// <param name="p_strBA_DeptName">病案系统科室名称</param>
        /// <returns></returns>
        public long m_lngModiftGroupRelation(string[] p_strGroupID, string p_strBA_DeptNum, int p_intDeptType, string[] p_strICareDeptName, string p_strBA_DeptName)
        {
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //       (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngModiftGroupRelation(  p_strGroupID, p_strBA_DeptNum, p_intDeptType, p_strICareDeptName, p_strBA_DeptName);
            return lngRes;
        }
        #endregion

        #region 获取相关的病案系统科号

        /// <summary>
        /// 获取相关的病案系统科号

        /// </summary>
        /// <param name="p_strGroupID">iCare系统费别</param>
        /// <param name="p_intDeptType">=0时获取专业组,=1时获取科室</param>
        /// <param name="p_strBA_DeptNum">病案系统费别</param>
        /// <returns></returns>
        public long m_lngGetBA_DeptNum(string p_strGroupID, int p_intDeptType, out string p_strBA_DeptNum)
        {
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //       (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetBA_DeptNum(  p_strGroupID, p_intDeptType, out p_strBA_DeptNum);
            return lngRes;
        }
        #endregion

        #region 获取相关的iCare系统专业组ID
        /// <summary>
        /// 获取相关的iCare系统专业组ID
        /// </summary>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <param name="p_intDeptType">=0时获取专业组,=1时获取科室</param>
        /// <param name="p_strGroupIDArr">iCare系统专业组ID</param>
        /// <returns></returns>
        public long m_lngGetICare_GroupID(string p_strBA_DeptNum, int p_intDeptType, out string[] p_strGroupIDArr)
        {
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //       (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetICare_GroupID(  p_strBA_DeptNum, p_intDeptType, out p_strGroupIDArr);
            return lngRes;
        }
        #endregion

        #region 获取相关的病案系统科号

        /// <summary>
        /// 获取相关的病案系统科号

        /// </summary>
        /// <param name="p_strGroupID">iCare系统专业组ID</param>
        /// <param name="p_intDeptType">=0时获取专业组,=1时获取科室</param>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <returns></returns>
        public long m_lngGetBADeptNum(string p_strGroupID, int p_intDeptType, out string p_strBA_DeptNum)
        {
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //       (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetBADeptNum(  p_strGroupID, p_intDeptType, out p_strBA_DeptNum);
            return lngRes;
        }
        #endregion

        #region 获取iCare病案科室同步配置
        /// <summary>
        /// 获取iCare病案科室同步配置
        /// </summary>
        /// <param name="p_strSetID">设置ID</param>
        /// <param name="p_intDeptType">设置代码</param>
        /// <returns></returns>
        public long m_lngGetBASynchronousSet(string p_strSetID, out int p_intDeptType)
        {
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //    (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetSysSetting(  p_strSetID, out p_intDeptType);

            return lngRes;
        }
        #endregion

        #region 获取iCare病案数据同步配置
        /// <summary>
        /// 获取iCare病案数据同步配置
        /// </summary>
        /// <param name="p_strSetID">设置ID</param>
        /// <param name="p_intDeptType">设置代码</param>
        /// <returns></returns>
        public long m_lngGetBADataSet(string p_strSetID, out int p_intDeptType)
        {
            long lngRes = 0;

            //clsEMR_SynchronousDeptServ objServ =
            //    (clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousDeptServ));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetSysSetting( p_strSetID, out p_intDeptType);

            return lngRes;
        }
        #endregion

        #region 获取科室关联表数据
        /// <summary>
        /// 获取科室关联表数据
        /// </summary>
        /// <param name="p_dtbDict">关联表数据</param>
        /// <returns></returns>
        public long m_lngDeptRelationDict(out DataTable p_dtbDict)
        {
            p_dtbDict = null;

            //com.digitalwave.emr.EMR_SynchronousCaseServ.clsEMR_SynchronousDeptServ objServ =
            //       (com.digitalwave.emr.EMR_SynchronousCaseServ.clsEMR_SynchronousDeptServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.emr.EMR_SynchronousCaseServ.clsEMR_SynchronousDeptServ));

            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngDeptRelationDict(out p_dtbDict);
            return lngRes;
        }
        #endregion
    }
}
