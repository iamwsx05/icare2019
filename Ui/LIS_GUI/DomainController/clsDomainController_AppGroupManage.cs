using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_AppGroupManage 的摘要说明。
    /// 刘彬 2004.05.26
    /// </summary>
    public class clsDomainController_AppGroupManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDomainController_AppGroupManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 查询所有的自定义组的子组信息 童华 2004.09.14
        public long m_lngGetAllApplUserGroupRelation(out clsApplUserGroupRelation_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAllApplUserGroupRelation(out p_objResultArr);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询所有的一级自定义组信息

        public long m_lngGetMasterUserGroup(out clsApplUserGroup_VO[] objApplUserGroupList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetMasterUserGroup(out objApplUserGroupList);
            //objCheckGroupSvc.Dispose();
            return lngRes;
        }

        #endregion

        #region 查询某个自定义组下(不含自定义组)的所有检验项目明细 童华 2004.05.28
        public long m_lngGetCheckItemApplGroupDetailByApplUserGroupID(string strApplUserGroupID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetCheckItemInApplGroupDetailByApplUserGroupID(strApplUserGroupID, out dtbCheckItem);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询某个自定义组下(含自定义组)的所有检验项目明细 童华 2004.05.28
        public long m_lngGetCheckItemApplGroupRelationByApplUserGroupID(string strApplUserGroupID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetCheckItemApplGroupRelationByApplUserGroupID(strApplUserGroupID, out dtbCheckItem);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除自定义组的关系及其明细 童华 2004.05.28
        public long m_lngDelApplUserGroupDetailAndRelation(string strApplUserGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelApplUserGroupDetailAndRelation(strApplUserGroupID);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除自定义组的所有信息 童华 2004.05.28
        public long m_lngDelApplUserGroupInfo(string strApplUserGroup, string strParentUserGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelApplUserGroupInfo(strApplUserGroup, strParentUserGroupID);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取自定义组下的所有申请单元ID 童华 2004.05.28
        public long m_lngGetAllUserGroupApplUnitID(out DataTable dtbApplUnitID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAllUserGroupApplUnitID(out dtbApplUnitID);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 保存用户自定义组及其明细和关系信息 童华 2004.05.27
        public long m_lngAddApplUserGroupAndDetailRelation(ref clsApplUserGroup_VO objApplUserGroupVO, ref clsApplUserGroupDetail_VO[] objApplUserGroupDetailVOList,
            ref clsApplUserGroupRelation_VO[] objApplUserGroupRelationVOList, clsApplUserGroupRelation_VO p_objParentRelation)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddApplUserGroupAndDetail(ref objApplUserGroupVO, ref objApplUserGroupDetailVOList,
                 ref objApplUserGroupRelationVOList, p_objParentRelation);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据用户自定义ID查询其包含的所有申请单元 童华 2004.05.26
        public long m_lngGetApplUnitByUserGroupID(string strUserGroupID, out clsApplUnit_VO[] objApplUnit)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetApplUnitByUserGroupID(strUserGroupID, out objApplUnit);
            //			objAppGroupSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据用户自定义ID查询其包含的所有子组 童华 2004.05.26
        public long m_lngGetSubGroupByUserGroupID(string strUserGroupID, out clsApplUserGroup_VO[] objApplUserGroupVOList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetChildGroupByUserGroupID(strUserGroupID, out objApplUserGroupVOList);

            return lngRes;
        }
        #endregion

        #region 获取所有的用户自定义组 童华 2004.05.26
        public long m_lngGetAllUserGroup(out clsApplUserGroup_VO[] objApplUserGroupList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllUserGroup(out objApplUserGroupList);

            return lngRes;
        }
        #endregion
    }
}
