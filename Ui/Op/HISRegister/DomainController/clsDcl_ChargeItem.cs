using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ChargeItem 的摘要说明。
    /// </summary>
    public class clsDcl_ChargeItem : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ChargeItem()
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

        #region 查询收费项目分类类型
        public long m_mthFindCat(out clsCharegeItemCat_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.clsChargeItemSvc_m_lngFindChargeItemCatList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取医嘱类型
        /// <summary>
        /// 获取医嘱类型
        /// </summary>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        public long m_mthSelectOrderCate(out DataTable m_objTable)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthSelectOrderCate(out m_objTable);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region 获取执行医嘱分类名称
        /// <summary>
        /// 获取执行医嘱分类名称
        /// </summary>
        /// <param name="p_objResultdt"></param>
        /// <returns></returns>
        public long m_lngGetAllBihCate(out DataTable p_objResultdt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngGetAllBihCate(out p_objResultdt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取用药频率
        /// <summary>
        /// 获取用药频率
        /// </summary>
        /// <param name="m_strFindText"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindRecipeFreq(string m_strFindText, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindRecipeFreq(m_strFindText, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取执行分类
        /// <summary>
        /// 获取执行分类
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strFindText"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindExeType(string m_strFindText, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //  (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindExeType(m_strFindText, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取医嘱类型
        /// <summary>
        /// 获取医嘱类型
        /// </summary>
        /// <param name="m_strFindText"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindOrderType(string m_strFindText, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindOrderType(m_strFindText, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询收费项目

        public long m_mthFindChargeItem(string strCatID, string strType, string strContent, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            long lngRes = proxy.Service.clsChargeItemSvc_m_mthFindChargeItem(strCatID, strType, strContent, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询收费项目

        public long m_mthFindChargeItem1(string strCatID, string strType, string strContent, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            long lngRes = proxy.Service.m_mthFindChargeItem1(strCatID, strType, strContent, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询所有单位
        public long m_mthGetUnit(out clsUnit_VO[] objResult)
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
        #region 查询所有用法
        public long m_mthGetUsage(out clsUsageType_VO[] objResult, string strEx)
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

        #region 查询所有单据t_bse_nurseorder
        public long m_lngFindAllORDERIDFromT_bse_nurseorder(out clsUsageType_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsUsageType_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllORDERIDFromT_bse_nurseorder(out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找收费特别类别
        public long m_mthEXType(string strFlag, out clsChargeItemEXType_VO[] objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindChargeItemEXTypeListByFlag(strFlag, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 新增收费项目
        public long m_mthDoAddNewChargeItem(clsChargeItem_VO p_objResultArr, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItem(p_objResultArr, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取申请单类型
        public long m_mthFindApplyType(out DataTable dt, string p_strEx)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindApplyType(out dt, p_strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取用药频率
        /// <summary>
        ///  获取用药频率
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindRecipeFreq(out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindRecipeFreq(out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 修改收费项目
        public long m_mthDoUpdChargeItem(clsChargeItem_VO p_objResultArr, string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpdChargeItemByID(p_objResultArr, strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 获取医保分类
        /// <summary>
        /// 获取医保分类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_getMEDICARETYPE(out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_getMEDICARETYPE(out dt);
            return lngRes;
        }
        #endregion
        #region 删除收费项目
        public long m_mthDelChargeItem(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDeleteChargeItemByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 删除收费项目
        public long m_mthChangeCat(string strID, string strType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthChangeCat(strID, strType);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 判断是否占用编号
        public long m_mthItemIsUsed(string strCode, string strItemID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthItemIsUsed(strCode, strItemID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 将收费项目同步到诊疗项目
        /// <summary>
        /// 将收费项目同步到诊疗项目
        /// </summary>
        /// <param name="m_objData"></param>
        /// <returns></returns>
        public long m_mthChargeItemSynOrderDic(clsChargeItemSynToOrderDic[] m_objDataArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthChargeItemSynOrderDic(m_objDataArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 编辑收费项目关联项方法
        /// <summary>
        /// 获取已经关联的项目
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_getSUBCHARGEITEM(string itemID, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_getSUBCHARGEITEM(itemID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngSaveSunItem(string itemID, DataTable dt, DataTable updt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngSaveSunItem(itemID, dt, updt);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// 删除关联项目
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="sumItemID"></param>
        /// <returns></returns>
        public long m_lngDeleteSunItem(string itemID, string sumItemID, bool isDeleAll)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDeleteSunItem(itemID, sumItemID, isDeleAll);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// 查询收费项目
        /// </summary>
        /// <param name="p_strFindString"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_mthFindMedicineByID(out DataTable p_dtResult, string strFindfild, string strFind)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindMedicineByID(out p_dtResult, strFindfild, strFind);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 门诊用法单位频率天数的领量
        /// <summary>
        /// 获取门诊用法单位频率天数的领量
        /// </summary>
        /// <param name="p_intTIMES">周期用药次数</param>
        /// <param name="p_dbleQTY">数量	{if(p_intType==1) 一次领量; if(p_intType==2) 医生下的剂量;}</param>
        /// <param name="p_intType">{1=领量单位;2=剂量单位}</param>
        /// <param name="p_dblUnitDosage">单位剂量	{只有p_intType==2，此参数才有意义}</param>
        /// <param name="p_dblGet">单位频率天数的领量	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)；}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        public long m_lngGetMeasureClinicUsage(int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetMeasureClinicUsage(p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblGet);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 门诊用法收费
        /// <summary>
        /// 获取门诊用法收费
        /// </summary>
        /// <param name="p_dblPrice">价格</param>
        /// <param name="p_intTIMES">周期用药次数</param>
        /// <param name="p_dbleQTY">数量	{if(p_intType==1) 一次领量; if(p_intType==2) 医生下的剂量;}</param>
        /// <param name="p_intType">{1=领量单位;2=剂量单位}</param>
        /// <param name="p_dblUnitDosage">单位剂量	{只有p_intType==2，此参数才有意义}</param>
        /// <param name="p_dblMoney">单位频率天数总价	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        public long m_lngGetChargeClinicUsage(double p_dblPrice, int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetChargeClinicUsage(p_dblPrice, p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblMoney);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 住院用法单位频率天数的领量
        /// <summary>
        /// 获取住院用法单位频率天数的领量
        /// </summary>
        /// <param name="p_intTIMES">周期用药次数</param>
        /// <param name="p_dbleQTY">数量	{if(p_intType==1) 一次领量; if(p_intType==2) 医生下的剂量;}</param>
        /// <param name="p_intType">{1=领量单位;2=剂量单位}</param>
        /// <param name="p_dblUnitDosage">单位剂量	{只有p_intType==2，此参数才有意义}</param>
        /// <param name="p_dblGet">单位频率天数的领量	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)；}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        public long m_lngGetMeasureBIHUsage(int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetMeasureBIHUsage(p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblGet);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 住院用法收费
        /// <summary>
        /// 获取住院用法收费
        /// </summary>
        /// <param name="p_dblPrice">价格</param>
        /// <param name="p_intTIMES">周期用药次数</param>
        /// <param name="p_dbleQTY">数量	{if(p_intType==1) 一次领量; if(p_intType==2) 医生下的剂量;}</param>
        /// <param name="p_intType">{1=领量单位;2=剂量单位}</param>
        /// <param name="p_dblUnitDosage">单位剂量	{只有p_intType==2，此参数才有意义}</param>
        /// <param name="p_dblMoney">单位频率天数总价	[out 参数]</param>
        /// <returns></returns>
        /// <remarks>
        /// 业务描述：
        ///		if(TYPE_INT==1[领量单位]) then {=次数*领量}
        ///		if(TYPE_INT==2[剂量单位]) then {=次数*(医生下的剂量/单位剂量)；}
        /// 业务描述：[领量、用量、频率]
        ///		领量 = 用量 * 周期用药次数
        ///		例如：用量=2,频率=3天4次,则 领量(3天的)=2*4;
        /// </remarks>
        public long m_lngGetChargeBIHUsage(double p_dblPrice, int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetChargeBIHUsage(p_dblPrice, p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblMoney);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取门诊总价及住院总价
        /// <summary>
        /// 获取门诊总价及住院总价
        /// </summary>
        /// <param name="strITEMID_CHR">项目ID</param>
        /// <param name="intType">1-门诊总价，2-住院总价</param>
        /// <param name="dblQTY">数量</param>
        /// <param name="intNuit">1-领药单位，2-剂量单位</param>
        /// <param name="dblTotailMoney">返回总金额</param>
        /// <returns></returns>
        public long m_lngGetChargeUsageTotailMoney(string strITEMID_CHR, int intType, double dblQTY, int intNuit, out double dblTotailMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetChargeUsageTotailMoney(strITEMID_CHR, intType, dblQTY, intNuit, out dblTotailMoney);
            return lngRes;
        }
        #endregion
        #region 加载分类
        public long m_mthLoadCheckType(out DataTable dt, string strEx)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthLoadCheckType(out dt, strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 加载分类
        public long m_lngUpdateOrderDicByChargeItemId(clsChargeItem_VO clsVO)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));

            string strSetStatus = "0";

            lngRes = proxy.Service.GetSysSetting("1031", out strSetStatus);

            if (lngRes > 0 && strSetStatus == "1")
            {
                lngRes = proxy.Service.m_lngUpdateOrderDicByChargeItemId(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获取收费项目执行分类
        /// <summary>
        /// 获取收费项目执行分类
        /// </summary>
        /// <param name="m_strFindText"></param>
        /// <param name="m_intFlag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindExeType(string m_strFindText, int m_intFlag, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindExeType(m_strFindText, m_intFlag, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region GetItemChildPriceHis
        /// <summary>
        /// GetItemChildPriceHis
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public DataTable GetItemChildPriceHis(string itemId)
        {
            //using (clsChargeItemSvc svc = (clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChargeItemSvc)))
            //{
            return proxy.Service.GetItemChildPriceHis(itemId);
            //}
        }
        #endregion
    }
}
