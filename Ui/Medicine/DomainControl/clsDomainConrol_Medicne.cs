using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.DLL
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrol_Medicne:数据控制类 Create by Sam 2004-5-24
    /// </summary>
    public class clsDomainConrol_Medicne : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrol_Medicne()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 设置中心药房缺药标志
        /// <summary>
        /// 设置中心药房缺药标志
        /// </summary>
        /// <param name="p_strMedID">药品ID</param>
        /// <param name="p_intFlag">中心药房缺药标志 0-有药 1－缺药</param>
        /// <returns></returns>
        public long m_lngSetCenterStorageFlag(string p_strMedID, int p_intFlag)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSetCenterStorageFlag(p_strMedID, p_intFlag);

            return lngRes;
        }
        #endregion

        #region 查询所有的药品（药品缺药设置模块）
        /// <summary>
        /// 查询所有的药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_intFlag">指示药房标志 0-药房 1-中心药房</param>
        /// <returns></returns>
        public long m_lngGetMetList(string[] MedTypeList, int p_intFlag, out System.Data.DataTable dt)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMetList(MedTypeList, p_intFlag, out dt);
            return lngRes;
        }
        #endregion

        #region 获取所有的药品类型
        /// <summary>
        /// 获取所有的药品类型
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedType(out DataTable dtbResult)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedType(out dtbResult);
            return lngRes;
        }
        #endregion


        #region 获取执行医嘱分类名称
        /// <summary>
        /// 获取执行医嘱分类名称
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetAllBihCate(out DataTable dtbResult)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllBihCate(out dtbResult);
            return lngRes;
        }
        #endregion

        #region 显示药品库存
        /// <summary>
        /// 获取所有的药品类型
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedStorage(string strMedID, out DataTable dt)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStorage(strMedID, out dt);
            return lngRes;
        }
        #endregion

        #region 获得药品类别名称
        /// <summary>
        /// 获得药品类别名称
        /// </summary>
        /// <param name="ArrMedTypeName">药品类型名称</param>
        /// <returns></returns>
        public long m_lngGetMedTypeArr(out string[] ArrMedTypeName, string[] MedTypeList)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedTypeArr(out ArrMedTypeName, MedTypeList);
            return lngRes;
        }
        #endregion

        #region 检查该药品是否已经同收费项目同步
        /// <summary>
        /// 检查该药品是否已经同收费项目同步
        /// </summary>
        /// <param name="strMedid"></param>
        /// <param name="stritemID">如果存在还回收费项目ID，不存在返回NULL</param>
        /// <returns></returns>
        public long m_lngGetItemID(string strMedid, out string stritemID)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetItemID(strMedid, out stritemID);
            return lngRes;
        }
        #endregion


        #region 删除药品对应的药典
        /// <summary>
        /// 删除药品对应的药典
        /// </summary>
        /// <param name="strMedid"></param>
        /// <returns></returns>
        public long m_lngDeleteMedByID(string strMedid)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedByID(strMedid);
            return lngRes;
        }
        #endregion

        #region 查询所有的药品(药品资料模块)
        /// <summary>
        /// 查询所有的药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMetDgList(string[] medType, out System.Data.DataTable dt, bool p_blStop)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMetDgList(medType, out dt, p_blStop);
            return lngRes;
        }
        #endregion

        #region 更改药品缺药标志
        /// <summary>
        /// 通过药品ID更改药品当前是否有货
        /// </summary>
        /// <param name="MedID">药品ID</param>
        /// <param name="p_ing">缺药标志</param>
        /// <returns></returns>
        public long m_lngSetStorage(string MedID, int p_ing)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSetStorage(MedID, p_ing);

            return lngRes;
        }

        #endregion

        #region 查询所有的药品
        /// <summary>
        /// 查询所有的药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedicine(out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedList(out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 查询所有的药品
        /// <summary>
        /// 查询所有的药品
        /// </summary>
        /// <returns></returns>
        public long m_lngGetMed(string strageID, out DataTable p_objResultArr)
        {
            p_objResultArr = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMed(strageID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 获取医保类型
        /// <summary>
        /// 获取医保类型
        /// </summary>
        /// <param name="dtMEDICARETYPE"></param>
        /// <returns></returns>
        public long m_lngGetMEDICARETYPE(out DataTable dtMEDICARETYPE)
        {
            dtMEDICARETYPE = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMEDICARETYPE(out dtMEDICARETYPE);
            return lngRes;
        }
        #endregion

        #region 检测药品助记码是否有其它药品在使用
        /// <summary>
        /// 检测药品助记码是否有其它药品在使用
        /// </summary>
        /// <returns></returns>
        public long m_lngCheckIsUse(string helpCode, out DataTable dt)
        {
            dt = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngCheckIsUse(helpCode, out dt);
            return lngRes;
        }
        #endregion

        #region 保存药品信息
        /// <summary>
        /// 保存药品信息
        /// </summary>
        /// <returns></returns>
        public long m_lngSaveed(string p_strType, DataTable SaveRow, out string newID, int isInsertItem, string strEmpID, bool IsAuto, string strStorageID, DataTable p_dtbChargeItem)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveed(p_strType, SaveRow, out newID, isInsertItem, strEmpID, IsAuto, strStorageID, p_dtbChargeItem);
            return lngRes;
        }
        #endregion

        #region 修改对应ID
        /// <summary>
        /// 修改对应ID
        /// </summary>
        /// <param name="medID"></param>
        /// <param name="id"></param>
        /// <param name="IDname"></param>
        /// <returns></returns>
        public long m_lngModifyMEDICINESTDID(string medID, string id, string IDname)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngModifyMEDICINESTDID(medID, id, IDname);
            return lngRes;
        }
        #endregion

        #region 获取厂家名称
        /// <summary>
        /// 获取厂家名称
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public long m_lngGetVendorName(string vendorID, out string vendorName)
        {
            long lngRes = 0;
            vendorName = null;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetVendorName(vendorID, out vendorName);

            return lngRes;
        }
        #endregion

        #region 获取药品的最大ID
        public long getMedMaxID(out string strID)
        {
            strID = "1";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedMaxID(out strID);

            return lngRes;
        }
        #endregion

        #region 根据ID查询药品
        /// <summary>
        /// 根据ID查询药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedicineByID(string strMedID, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByID(strMedID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 根据其它条件查询药品
        /// <summary>
        /// 根据其它条件查询药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedicineByAll(string strSubSQL, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByAny(strSubSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 新增药品信息
        /// <summary>
        /// 新增药品信息
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewMed(clsMedicine_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicine(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 修改药品信息
        /// <summary>
        ///修改药品信息
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoMed(clsMedicine_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicineByID(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region 删除药品信息
        public long m_lngDeleteMedicineByID(string strID, bool isDeleItem, string strEmpID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedicineByID(strID, isDeleItem, strEmpID);
            return lngRes;
        }
        #endregion

        #region 获取单位信息
        public long m_lngGetUnitArr(out clsUnit_Vo[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetUnitArr(out p_objResultArr);

            return lngRes;
        }
        #endregion

        //药品类型、剂型、单位 Create by Sam 2004-5-24

        #region 取回所有的药品类型
        public long m_lngGetMedType(out clsMedicineType_VO[] objResultArr)
        {
            objResultArr = new clsMedicineType_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedicineType(out objResultArr);

            return lngRes;
        }
        #endregion

        #region 取回所有的剂型
        public long m_lngGetPrepType(out clsMedicinePrepType_VO[] objResultArr)
        {
            objResultArr = new clsMedicinePrepType_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMeicinePrep(out objResultArr);

            return lngRes;
        }
        #endregion

        #region 取回所有的单位
        public long m_lngGetUnit(out clsUnit_VO[] objResultArr)
        {
            objResultArr = new clsUnit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllUnit(out objResultArr);

            return lngRes;
        }
        #endregion

        #region 新增单位
        /// <summary>
        /// 新增单位
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewUnit(clsUnit_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewUnit(p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 新增药品类型
        /// <summary>
        /// 新增药品类型
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewMedType(clsMedicineType_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicineType(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 新增药品剂型
        /// <summary>
        /// 新增药品剂型
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewPrepType(clsMedicinePrepType_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewPrepType(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 修改单位
        /// <summary>
        ///修改单位
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoUnit(clsUnit_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdUnit(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region 修改药品类型
        /// <summary>
        ///修改药品类型
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoMedType(clsMedicineType_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicineTypeByID(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region 修改剂型
        /// <summary>
        ///修改剂型
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoPrepType(clsMedicinePrepType_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdPrepType(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region 删除单位
        /// <summary>
        ///删除单位
        /// </summary>
        public long m_lngDelUnit(string strUnitID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteUnit(strUnitID);

            return lngRes;
        }
        #endregion

        #region 删除药品类型
        /// <summary>
        ///删除药品类型
        /// </summary>
        public long m_lngDelMedType(clsMedicineType_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedicineTypeByID(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 删除剂型
        /// <summary>
        ///删除剂型
        /// </summary>
        public long m_lngDelPrepType(string strID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeletePrepType(strID);

            return lngRes;
        }
        #endregion

        #region 根据ID查询项目名称(药品类型、剂型、单位)
        /// <summary>
        ///  根据ID查询项目名称
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetItemByID(string strID, byte sType, out string strName)
        {
            strName = "";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetItemByID(sType, strID, out strName);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取最大的ID(药品类型、剂型、单位)
        public long getItemMaxID(byte sType, out string strID)
        {
            strID = "1";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngMaxID(sType, out strID);

            return lngRes;
        }
        #endregion

        //药品与单位的关系 Create by Sam 2004-5-24

        #region 查询所有的药品与单位的关系
        public long m_lngGetMedAndUnit(out clsMedUnitAndUnit[] objResult)
        {
            objResult = new clsMedUnitAndUnit[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedAndUnit(out objResult);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据ID查询药品与单位的关系
        /// <summary>
        /// 根据ID查询药品与单位的关系
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedAndUnitByID(string strMedID, string strUnitID, out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByID(strMedID, strUnitID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据药品ID查询药品与单位的关系
        /// <summary>
        /// 根据药品ID查询药品与单位的关系
        /// </summary>
        /// <param name="strMedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedAndUnitByMedID(string strMedID, out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByMedID(strMedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增药品与单位的关系
        public long m_lngNewMedUnit(clsMedUnitAndUnit p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedUnitAndUnit(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 修改药品与单位的关系
        public long m_lngUpMedUnit(clsMedUnitAndUnit p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpMedUnitAndUnit(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 删除药品与单位的关系
        public long m_lngDelMedAndUnit(string strMedID, string strUnitID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedUnitAndUnit(strMedID, strUnitID);

            return lngRes;
        }
        #endregion

        #region 得到药品与单位关系的最大级别
        public long getLevelMaxID(string MedID, out string strID)
        {
            strID = "1";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            (new weCare.Proxy.ProxyMedStore()).Service.GetMaxLeve(MedID, out strID);

            return lngRes;
        }
        #endregion

        //药品价格列表 Create by Sam
        #region 查询所有的药品价格
        /// <summary>
        /// 查询所有的药品价格
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedPrice(out clsMedicinePrice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrice_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllCurPrice(out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 根据药品ID查询药品价格
        /// <summary>
        /// 根据药品ID查询药品价格
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedPriceByID(string strMedID, out clsMedicinePrice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrice_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindCurPriceByMedicineID(strMedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询药品历史价格
        /// <summary>
        /// 查询药品历史价格
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedPriceHistory(string strMedID, out clsMedicinePrice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrice_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindPriceHistoryByMedicineID(strMedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增药品价格
        /// <summary>
        /// 新增药品价格
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngAddMedPrice(clsMedicinePrice_VO p_objResultArr, out string ModifyDate)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewPrice(p_objResultArr, out ModifyDate);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 更新药品价格
        /// <summary>
        /// 更新药品价格
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUPMedPrice(clsMedicinePrice_VO p_objResultArr, out string ModifyDate)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdPriceByMedicineID(p_objResultArr, out ModifyDate);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除药品价格
        /// <summary>
        /// 删除药品价格
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDelMedPrice(clsMedicinePrice_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeletePriceByMedicineID(p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        //药品与药库
        #region 新增药品到药库
        /// <summary>
        /// 新增药品到药库
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngAddMedAndSto(clsMedicineAndStorage p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicineAndStorage(p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改药品与药库关系
        /// <summary>
        /// 修改药品与药库关系
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpMedAndSto(clsMedicineAndStorage p_objResultArr, string OldMedID, string OldStoID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicineAndStorage(p_objResultArr, OldMedID, OldStoID);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除药品与药库关系
        /// <summary>
        /// 删除药品与药库关系
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDelMedAndSto(clsMedicineAndStorage p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedicineAndStorage(p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据药库ID查询所有药品
        /// <summary>
        /// 根据药库ID查询所有药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindAllMedByStoID(string StoID, out clsMedicineAndStorage[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedicineByStorageID(StoID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 根据药库ID和药品ID查询药品
        /// <summary>
        /// 根据药库ID和药品ID查询药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindMedByStoIDAndMedID(string StoID, string MedID, out clsMedicineAndStorage[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindItemByStoIDAndMedID(StoID, MedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 取得仓库的信息
        public long m_lngGetStorage(out clsStorage_VO[] objStorage)
        {
            objStorage = new clsStorage_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetStorage(out objStorage);

            return lngRes;
        }
        #endregion

        #region 查询所有的药品
        /// <summary>
        /// 查询所有的药品
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedNoIn(string StoID, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedList(StoID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获得所有药品资料
        public long m_lngGetMedicine(out DataTable dtbResult)
        {
            dtbResult = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedicine(out dtbResult);

            return lngRes;
        }

        #endregion

        #region  药库配药管理   2004-9-29

        #region 获取所有的申请单据
        public long m_lngGetMedAppl(out clsStoreMedAppl_VO[] p_objResultArr, string STORAGEID, string strDate)
        {
            p_objResultArr = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedAppl(out p_objResultArr, STORAGEID, strDate);

            return lngRes;
        }

        #endregion

        #region 获取药库
        public long m_lngGetMedstroage(string medStroageID, out string medStroageName)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedstroage(medStroageID, out medStroageName);

            return lngRes;
        }

        #endregion

        #region 根据申请单号获取所有的申请明细
        /// <summary>
        /// 根据申请单号获取所有的申请明细
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="flat">false 标志此申请单有没有设置包装量的药品</param>
        /// <returns></returns>
        public long m_lngGetMedApplDeByID(string strID, out DataTable dtbResult, out bool flat)
        {
            dtbResult = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedApplDeByID(strID, out dtbResult, out flat);

            return lngRes;
        }

        #endregion

        #region 根据药品ID查询库存明细表
        public long m_lngGetDeTailByMedID(string MedID, out DataTable dtbResult)
        {
            dtbResult = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetDeTailByMedID(MedID, out dtbResult);

            return lngRes;
        }
        #endregion

        #region 保存与生成出库单
        public long m_lngChangAndSave(string strMedApplId, clsMedStorageOrd_VO objResult, clsMedStorageOrdDe_VO[] objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngChangAndSave(strMedApplId, objResult, objResultArr);
            return lngRes;
        }
        #endregion

        #endregion

        #region 药品基本信息管理
        /// <summary>
        /// 药品基本信息管理
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtUnit">返回单位信息</param>
        /// <param name="dtMeicinePrep">返回药品制剂类型</param>
        /// <param name="dtuse">返回用法</param>
        /// <param name="dtvendor">返回厂家</param>
        ///<param name="dtmedtype">返回药品类型信息</param>
        ///<param name="dtItemextype">返回门诊项目核算分类</param>
        ///<param name="dtItemextype1">返回门诊项目发票分类</param>
        ///<param name="dtItemextype3">返回住院项目发票分类</param>
        ///<param name="dtItemextype4">返回住院项目发票分类</param>
        ///<param name="dtMEDICARETYPE">返回医保类型数据</param>
        ///<param name="dtPharMatype">药理分类</param>
        ///<param name="Isuse">是否可以直接在药品基本界面修改药品价格</param>
        /// <returns></returns>
        public long m_lngFindAllBase(out DataTable dtUnit, out DataTable dtMeicinePrep, out DataTable dtuse, out DataTable dtFreq, out DataTable dtvendor, out DataTable dtmedtype, out DataTable dtItemextype, out DataTable dtItemextype1, out DataTable dtItemextype3, out DataTable dtItemextype4, out DataTable dtMEDICARETYPE, out DataTable dtItemextype5, out DataTable dtPharMatype, out bool Isuse, out DataTable dtCATEID1)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllBase(out dtUnit, out dtMeicinePrep, out dtuse, out dtFreq, out dtvendor, out dtmedtype, out dtItemextype, out dtItemextype1, out dtItemextype3, out dtItemextype4, out dtMEDICARETYPE, out dtItemextype5, out dtPharMatype, out Isuse, out dtCATEID1);
            return lngRes;
        }

        #endregion

        #region 修改药品信息
        /// <summary>
        /// 修改药品信息
        /// </summary>
        /// <param name="ModifyRow"></param>
        /// <returns></returns>
        public long m_lngModify(DataTable ModifyRow, int isInsertItem, string strEmpID)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngModify(ModifyRow, isInsertItem, strEmpID);
            return lngRes;
        }

        #endregion

        #region 仓库药品维护

        #region 获取所有的药品信息
        /// <summary>
        /// 获取所有的药品信息
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="btStorage"></param>
        /// <returns></returns>
        public long m_lngGetAllMed(out DataTable bt, out DataTable btStorage)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMed(out bt, out btStorage);
            return lngRes;
        }

        #endregion


        #region 根据药库ID取出药品信息
        /// <summary>
        /// 根据药库ID取出药品信息
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="btStorage"></param>
        /// <returns></returns>
        public long m_lngGetMedByStorageID(string strID, out DataTable tb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedByStorageID(strID, out tb);
            return lngRes;
        }

        #endregion

        #region 把药品添加到仓库(全部药品）
        /// <summary>
        /// 把药品添加到仓库(全部药品）
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="storageIDb"></param>
        /// <returns></returns>
        public long m_lngAddMedToStorage(DataTable tb, DataTable tb1, string storageIDb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddMedToStorage(tb, tb1, storageIDb);
            return lngRes;
        }

        #endregion

        #region 删除指定仓库的药品
        /// <summary>
        /// 删除指定仓库的药品
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="medID"></param>
        /// <returns></returns>
        public long m_lngDeleMedToStorage(string storageID, string medID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleMedToStorage(storageID, medID);
            return lngRes;
        }

        #endregion

        #region 把药品添加到仓库(某一条记录）
        /// <summary>
        /// 把药品添加到仓库(某一条记录）
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="strMedID"></param>
        /// <returns></returns>
        public long m_lngAddNoeMedToStorage(string storageID, string strMedID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddNoeMedToStorage(storageID, strMedID);
            return lngRes;
        }

        #endregion


        #endregion

        #region 药库数据维护
        #region 获取仓库类别
        /// <summary>
        /// 获取仓库类别
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public long m_lngGetAllMedType(out DataTable tb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMedType(out tb);
            return lngRes;
        }
        #endregion
        #region 获仓库信息
        /// <summary>
        /// 获仓库信息
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public long m_lngGetAllstorage(out DataTable tb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllstorage(out tb);
            return lngRes;
        }

        #endregion

        #region 插入仓库信息
        /// <summary>
        /// 插入仓库信息
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public long m_lngInsertStorageData(string strStorageTypeID, string strStorageName, out string newID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngInsertStorageData(strStorageTypeID, strStorageName, out newID);
            return lngRes;
        }

        #endregion

        #region 修改仓库信息
        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public long m_lngModifyStorageData(string p_strStorageTypeID, string p_strStorageName, string p_strStorageID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngModifyStorageData(p_strStorageTypeID, p_strStorageName, p_strStorageID);
            return lngRes;
        }

        #endregion

        #region 删除仓库信息
        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDeleStorageData(string strID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleStorageData(strID);
            return lngRes;
        }

        #endregion


        #endregion

        #region 药库月购进报表统计模块
        /// <summary>
        /// 药库月购进报表统计模块
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfMonth(out DataTable dtdein, out DataTable dtENAim, out DataTable dtENNoAim, out DataTable dtCHAim, out DataTable dtCHNoAim, out DataTable dtEHAim, out DataTable dtEHNoAim, out DataTable dtImportAim, out DataTable dtImportNoAim, System.Collections.Generic.List<string> ArrList, int statues)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfMonth(out dtdein, out dtENAim, out dtENNoAim, out dtCHAim, out dtCHNoAim, out dtEHAim, out dtEHNoAim, out dtImportAim, out dtImportNoAim, ArrList, statues);
            return lngRes;
        }

        #endregion

        #region 药品进销存报表
        /// <summary>
        /// 药品进销存报表
        /// </summary>
        /// <param name="arrPrID">要统计的财务期列表</param>
        /// <param name="strUpPr">上一期的财务期</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfInAndOut(System.Collections.Generic.List<string> arrPrID, string strUpPr, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfInAndOut(arrPrID, strUpPr, out dt);
            return lngRes;
        }

        #endregion

        #region 药库月购进报表统计模块
        /// <summary>
        /// 药库月购进报表统计模块
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfMonth1(out DataTable dtdein, out DataTable dtEN, out DataTable dtCH, out DataTable dtEH, out DataTable dtImport, string startDate, string EndDate, int statues)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfMonth(out dtdein, out dtEN, out dtCH, out dtEH, out dtImport, startDate, EndDate, statues);
            return lngRes;
        }

        #endregion

        #region 盘亏明细表统计报表
        /// <summary>
        /// 盘亏明细表统计报表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="statues"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngCheckLoseDe(string startDate, string EndDate, int statues, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngCheckLoseDe(startDate, EndDate, statues, out dt);
            return lngRes;
        }

        #endregion

        #region 药品财务统计报表
        /// <summary>
        /// 药品财务统计报表
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        public long m_lngGetReportData(string date1, string date2, out DataTable dtDeIn, out DataTable dtDeOut)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportData(date1, date2, out dtDeIn, out dtDeOut);
            return lngRes;
        }

        #endregion

        #region 药品年购进统计报表
        /// <summary>
        /// 药品年购进统计报表
        /// </summary>
        /// <param name="arrPrID">一年中的所有财务期</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfInAndOutYear(System.Collections.Generic.List<string> arrPrID, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfInAndOutYear(arrPrID, out dt);
            return lngRes;
        }

        #endregion

        #region 药品进销存明细报表
        /// <summary>
        /// 药品进销存明细报表
        /// </summary>
        /// <param name="arrPrID">财务期</param>
        /// <param name="dt">药品ID</param>
        /// <returns></returns>
        public long m_lngGetReportDataOfInAndOutDe(System.Collections.Generic.List<string> arrPrID, System.Collections.Generic.List<string> arrUpPrID, string strMedID, int intMedType, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfInAndOutDe(arrPrID, arrUpPrID, strMedID, intMedType, out dt);
            return lngRes;
        }
        #endregion

        #region 获取药品类型毛利率
        /// <summary>
        /// 获取药品类型毛利率
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineTypeID">药品类型ID</param>
        /// <param name="p_douGrossprofitrate"></param>
        /// <returns></returns>
        public long m_lngGetGrossprofitrate(string p_strMedicineTypeID, out double p_douGrossprofitrate)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetGrossprofitrate(p_strMedicineTypeID, out p_douGrossprofitrate);
            return lngRes;
        }
        #endregion

        #region 获取生成零售价方式
        /// <summary>
        /// 获取生成零售价方式
        /// </summary>
        /// <param name="p_intRetailMethod">生成零售价方式</param>
        /// <returns></returns>
        internal long m_lngGetRetailMethod(out int p_intRetailMethod)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetSysSetting("5019", out p_intRetailMethod);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 取得中标年份数
        /// </summary>
        /// <param name="p_intYear">中标年份</param>
        internal long m_lngGetStandardDate(out int p_intYear)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetSysSetting("5030", out p_intYear);
            return lngRes;
        }

        /// <summary>
        /// 保存中标年份
        /// </summary>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <param name="p_strReturn">年份</param>
        internal long m_lngSaveStandardYear(string p_strMedicineID, string p_strYear)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveStandardYear(p_strMedicineID, p_strYear);
            return lngRes;
        }

        /// <summary>
        /// 保存项目别名表
        /// </summary>
        /// <param name="isAddNew"></param>
        /// <param name="objAlias_Vo"></param>
        public void m_mthSaveAlias(byte isAddNew, clsAlias_VO objAlias_Vo)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveItemAlias(isAddNew, objAlias_Vo);
        }

        /// <summary>
        /// 获取物资仓库
        /// </summary>
        /// <param name="objStorageArr">物资仓库</param>
        public void m_mthGetMaterialStorage(out clsStorage_VO[] objStorageArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_mthGetMaterialStorage(out objStorageArr);
        }

        internal long m_lngFillChargeItem(string p_MedicineID, DataTable p_dtbChargeItem)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFillChargeItem(p_MedicineID, out p_dtbChargeItem);
            return lngRes;
        }
    }
}
