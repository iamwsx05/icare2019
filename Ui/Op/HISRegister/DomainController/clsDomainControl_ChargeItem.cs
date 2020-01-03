using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 收费项目以及各相关项目的数据控制层 Create By Sam 2004-6-9
    /// </summary>
    public class clsDomainControl_ChargeItem : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_ChargeItem()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion 

        //收费项目
        #region 新增收费项目
        public long m_mthInsertCASEHISCHR(string GroupID, string strCatID, string strName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthInsertCASEHISCHR(GroupID, strCatID, strName);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        //		#region 修改收费项目
        //		public long m_lngDoUpdChargeItem(clsChargeItem_VO p_objResultArr)
        //		{
        //			long lngRes=0;
        //			com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc = 
        //				(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
        //			lngRes = proxy.Service.m_lngDoUpdChargeItemByID(p_objResultArr);
        //			//objSvc.Dispose();
        //			return lngRes;
        //		}
        //		#endregion
        #region 删除收费项目
        public long m_mthDeleteCASEHISCHR(string strID, string strCatID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthDeleteCASEHISCHR(strID, strCatID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找收费项目（根据组ID查询项目)
        public long m_mthGetCASEHISCHR(string strID, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthGetCASEHISCHR(strID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion



        //收费项目分类类型
        #region 新增收费项目分类类型
        public long m_lngAddCat(clsCharegeItemCat_VO objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItemCat(objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改收费项目分类类型
        public long m_lngDoUpdCatByID(clsCharegeItemCat_VO p_objResultArr, string ID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpdChargeItemCatByID(p_objResultArr, ID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除收费项目分类类型
        public long m_lngDelCatByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDeleteChargeItemCatByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询收费项目分类类型
        public long m_lngFindCat(out clsCharegeItemCat_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.clsChargeItemSvc_m_lngFindChargeItemCatList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        //收费特别类别
        #region 新增收费特别类别
        public long m_lngAddEXType(clsChargeItemEXType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItemEXType(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改收费特别类别
        public long m_lngDoUpdEXType(clsChargeItemEXType_VO p_objResultArr, string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpdChargeItemEXTypeByID(p_objResultArr, strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除收费特别类别
        public long m_lngDelEXType(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDeleteCharegeItemEXTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查找收费特别类别
        public long m_GetEXType(string strFlag, out clsChargeItemEXType_VO[] objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindChargeItemEXTypeListByFlag(strFlag, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        //用法
        #region 新增用法
        public long m_lngAddUsage(clsUsageType_VO p_objResultArr, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngAddNewUsage(p_objResultArr, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改用法
        public long m_lngDoUpdUsage(clsUsageType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpUsage(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除用法
        public long m_lngDelUsage(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDelUsage(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        //用法项目
        #region 新增用法项目
        /// <summary>
        /// 新增用法项目	[没有用了]
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDoAddNewChargeItemUsageGroup(clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItemUsageGroup(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// 新增用法项目	徐斌辉加	2005-03-17
        /// </summary>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDoAddNewChargeItemUsageGroup(out string p_strRecordID, clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItemUsageGroup(out p_strRecordID, p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 新增中药用法带出的项目
        /// <summary>
        ///新增中药用法带出的项目
        /// </summary>
        /// <param name="p_strRecordID">流水号</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDoAddNewChargeItemCMUsageGroup(out string p_strRecordID, clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItemCMUsageGroup(out p_strRecordID, p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改用法项目
        /// <summary>
		/// 修改用法项目	徐斌辉修改	2005-03-17
		/// </summary>
		/// <param name="p_objResultArr"></param>
		/// <returns></returns>
		public long m_lngDoModifyChargeItemUsageGroup(clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoModifyChargeItemUsageGroup(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改中药用法的项目
        /// <summary>
        /// 修改中药用法的项目
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDoModifyChargeItemCMUsageGroup(clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoModifyChargeItemCMUsageGroup(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除用法项目
        public long m_lngDelUsageGroupByID(clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDelUsageGroupByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除中药用法项目
        public long m_lngDelCMUsageGroupByID(clsChargeItemUsageGroup_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDelCMUsageGroupByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询用法项目
        public long m_lngFindItemByUsageID(string strUsageID, out clsChargeItem_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_GetItemByUsageID(strUsageID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询用法项目
        public long m_lngGetItemByCMUsageID(string strUsageID, out clsChargeItem_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngGetItemByCMUsageID(strUsageID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询用法表中没有的项目
        public long m_lngFindItemNoUsageGroup(string strCatID, string strUsageID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_GetItemNoUsageGroup(strCatID, strUsageID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        //查询
        #region 根据项目分类的ID取得最上级的目录
        public long m_lngGetGroupCat(string strID, out clsChargeItem_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsChargeItem_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_GetGroupCat(strID, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询所有用法
        public long m_lngGetUsage(out clsUsageType_VO[] objResult, string strEx)
        {
            long lngRes = 0;
            objResult = new clsUsageType_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllUsage(out objResult, strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查询所有用法设置
        public long m_lngGetUsageSet(out clsUsageType_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsUsageType_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllUsageSet(out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增用法设置
        public long m_lngDoAddNewUsageType(string p_usageID, string p_usageType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngDoAddNewUsageType(p_usageID, p_usageType);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除用法设置
        public long m_lngDelUsageSet(string p_usageID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngDelUsageSet(p_usageID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion




        #region 查询所有单位
        public long m_lngGetUnit(out clsUnit_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsUnit_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllUnit(out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region 取回所有未有目录的项目
        public long m_lngGetNoGroup(string strCatID, out clsChargeItem_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsChargeItem_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_GetItemNoGroup(strCatID, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据项目编号取回项目
        public long m_GetItemByItemCode(string strID, out clsChargeItem_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsChargeItem_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_GetItemByItemCode(strID, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据用法ID查询用法
        public long m_lngGetUsageByCode(string strCode, out clsUsageType_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsUsageType_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_GetUsage(out objResult, strCode);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据源ID取回源名称
        public long m_GetSourName(string strID, string SourType, out string strName)
        {
            long lngRes = 0;
            strName = "";
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindSour(strID, SourType, out strName);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据源列表
        public long m_GetAllSour(string SourType, out DataTable dtResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindAllSour(SourType, out dtResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        // 收费项目用法
        #region 查询收费项目用法 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 查询收费项目用法
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindUsageTypeList(out clsUsageType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindUsageTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增收费项目用法 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 新增收费项目用法
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strName"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngAddUsageType(string strCode, string strName, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewUsageType(strCode, strName, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改收费项目用法 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 修改收费项目用法
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDoUpdUsageTypeByID(clsUsageType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpdUsageTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除收费项目用法 created by Cameron Wong on Aug 12, 2004
        /// <summary>
        /// 删除收费项目用法
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDelUsageTypeByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDelUsageTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 新增与删除单据
        /// <summary>
        /// 新增与删除单据
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDoUpdUsageorderid_vchrByIDAndTypeId(int p_intTypeindex, string p_strUsageID, string p_strGroupID, bool p_blnAdd)

        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpdUsageorderid_vchrByIDAndTypeId(p_intTypeindex, p_strUsageID, p_strGroupID, p_blnAdd);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 根据传入的SQL语句查询出数据

        public long m_lngGetData(string SQLstr, out DataTable dt)
        {
            dt = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngGetData(SQLstr, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除收费项目

        public long m_lngDel(string ID)
        {

            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDelCharegeItem(ID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
