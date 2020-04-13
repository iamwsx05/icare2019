﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    /// <summary>
    /// 药品调价
    /// </summary>
    public class clsDcl_AdjustmentDetail : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 添加药品调价明细
        /// <summary>
        /// 添加药品调价明细
        /// </summary>
        /// <param name="p_objDetailArr">药品调价明细内容</param>
        /// <param name="p_lngSEQ">生成的序列号</param>
        /// <returns></returns>
        internal long m_lngAddNewAdjustmentDetail(clsMS_Adjustment_Detail[] p_objDetailArr, out long[] p_lngSEQ)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngAddNewAdjustmentDetail(p_objDetailArr, out p_lngSEQ);
            return lngRes;
        }
        #endregion

        #region 添加调价主表内容
        /// <summary>
        /// 添加调价主表内容
        /// </summary>
        /// <param name="p_objMain">调价主表内容</param>
        /// <param name="p_lngMainSEQ">序列</param>
        /// <param name="p_strAdjustID">调价单据号</param>
        /// <returns></returns>
        internal long m_lngAddNewAdjustmentMain(clsMS_Adjustment_VO p_objMain, out long p_lngMainSEQ, out string p_strAdjustID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngAddNewAdjustmentMain(p_objMain, out p_lngMainSEQ, out p_strAdjustID);
            return lngRes;
        }
        #endregion

        #region 删除指定药品调价明细记录
        /// <summary>
        /// 删除指定药品调价明细记录
        /// </summary>
        /// <param name="p_lngSEQ">明细记录序列</param>
        /// <param name="p_blnIsCommit">是否保存即审核</param>
        /// <param name="p_blnIsDiffLotNO">是否分批号</param>
        /// <param name="p_objAdjustMedicine">调价审核药品信息</param>
        /// <returns></returns>
        internal long m_lngDeleteSpecAdjustmentDetail(long p_lngSEQ, bool p_blnIsCommit, bool p_blnIsDiffLotNO, clsMS_MedicineInfoForAdjustPrice p_objAdjustMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngDeleteSpecAdjustmentDetail(p_lngSEQ, p_blnIsCommit, p_blnIsDiffLotNO, p_objAdjustMedicine);
            return lngRes;
        }

        /// <summary>
        /// 删除指定药品调价明细记录
        /// </summary>
        /// <param name="p_lngSEQ">明细记录序列</param>
        /// <param name="p_blnIsCommit">是否保存即审核</param>
        /// <param name="p_blnIsDiffLotNO">是否分批号</param>
        /// <param name="p_objAdjustMedicine">调价审核药品信息</param>
        /// <returns></returns>
        internal long m_lngDeleteSpecAdjustmentDetail(long[] p_lngSEQ, bool p_blnIsCommit, bool p_blnIsDiffLotNO, clsMS_MedicineInfoForAdjustPrice p_objAdjustMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngDeleteSpecAdjustmentDetail(p_lngSEQ, p_blnIsCommit, p_blnIsDiffLotNO, p_objAdjustMedicine);
            return lngRes;
        }
        #endregion

        #region 修改调价主表内容
        /// <summary>
        /// 修改调价主表内容
        /// </summary>
        /// <param name="p_objMain">调价主表内容</param>
        /// <returns></returns>
        internal long m_lngModifyAdjustmentMain(clsMS_Adjustment_VO p_objMain)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngModifyAdjustmentMain(p_objMain);
            return lngRes;
        }
        #endregion

        #region 获取同一药品是否分批号调价设置


        /// <summary>
        /// 获取同一药品是否分批号调价设置

        /// </summary>
        /// <param name="p_intDiffLotNO">同一药品是否分批号调价设置</param>
        /// <returns></returns>
        internal long m_lngGetIsDiffLotNO(out int p_intDiffLotNO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("5009", out p_intDiffLotNO);
            return lngRes;
        }
        #endregion

        #region 获取药库调价是否同时调整药房价格设置

        /// <summary>
        /// 获取药库调价是否同时调整药房价格设置
        /// </summary>
        /// <param name="p_intAdjustDrugstore">药库调价是否同时调整药房价格设置</param>
        /// <returns></returns>
        internal long m_lngGetIsAdjustDrugstore(out int p_intAdjustDrugstore)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("5010", out p_intAdjustDrugstore);
            return lngRes;
        }
        #endregion

        #region 获取调价是否同时调整批发价

        /// <summary>
        /// 获取药库调价是否同时调整批发价
        /// </summary>
        /// <param name="p_intAdjustDrugstore">获取药库调价是否同时调整批发价</param>
        /// <returns></returns>
        internal long m_lngGetIsShowWholeSalePrice(out int p_intAdjustDrugstore)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("5039", out p_intAdjustDrugstore);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 根据仓库ID获取仓库名称
        /// </summary>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_strStorageName">仓库名称</param>
        /// <returns></returns>
        internal long m_lngGetStorageNameByStorageID(string p_strStorageID, out string p_strStorageName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetStoreRoomName(p_strStorageID, out p_strStorageName);
            return lngRes;
        }

        #region 根据药品ID获取药品
        /// <summary>
        /// 根据药品ID获取药品
        /// </summary>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <param name="p_strStorageID">仓库ID</param>
        /// <param name="p_dtbMedicine">药品数据</param>
        /// <returns></returns>
        internal long m_lngGetMedicineByMedicineID(string p_strMedicineID, string p_strStorageID, out DataTable p_dtbMedicine)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetMedicineByMedicineID(p_strMedicineID, p_strStorageID, out p_dtbMedicine);
            return lngRes;
        }
        #endregion

        #region 保存药品调价记录
        /// <summary>
        /// 保存药品调价记录
        /// </summary>
        /// <param name="p_objMain">主表记录</param>
        /// <param name="p_objDetailArr">明细记录</param>
        /// <param name="p_blnIsCommit">是否直接审核</param>
        /// <param name="p_blnIsDiffLotNO">是否分批号</param>
        /// <param name="p_blnIsImmAccount">是否审核即入帐</param>
        /// <param name="p_lngMainSEQ">主表序列</param>
        /// <param name="p_strAdjustID">调价单据号</param>
        /// <param name="p_lngSEQ">明细表序列</param>
        /// <returns></returns>
        internal long m_lngAddNewAdjustment(clsMS_Adjustment_VO p_objMain, clsMS_Adjustment_Detail[] p_objDetailArr, bool p_blnIsCommit, bool p_blnIsDiffLotNO, bool p_blnIsImmAccount, out long p_lngMainSEQ, out string p_strAdjustID, out long[] p_lngSEQ, bool m_blnChangeMedStore)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngAddNewAdjustment(p_objMain, p_objDetailArr, p_blnIsCommit, p_blnIsDiffLotNO, p_blnIsImmAccount, out p_lngMainSEQ, out p_strAdjustID, out p_lngSEQ, m_blnChangeMedStore);
            return lngRes;
        }
        #endregion

        #region 修改药品调价记录
        /// <summary>
        /// 修改药品调价记录
        /// </summary>
        /// <param name="p_objMain">主表记录</param>
        /// <param name="p_objDetailArr">明细记录</param>
        /// <param name="p_blnIsCommit">是否直接审核</param>
        /// <param name="p_blnIsDiffLotNO">是否分批号</param>
        /// <param name="p_lngSEQ">明细表序列</param>
        /// <returns></returns>
        internal long m_lngModifyAdjustment(clsMS_Adjustment_VO p_objMain, clsMS_Adjustment_Detail[] p_objDetailArr, bool p_blnIsCommit, bool p_blnIsDiffLotNO, out long[] p_lngSEQ, bool m_blnChangeMedStore)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustmentSVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngModifyAdjustment(p_objMain, p_objDetailArr, p_blnIsCommit, p_blnIsDiffLotNO, out p_lngSEQ, m_blnChangeMedStore);
            return lngRes;
        }
        #endregion

        #region  获取最后帐务结转的结束日期
        /// <summary>
        ///  获取最后帐务结转的结束日期
        /// </summary>
        /// <returns></returns>
        public long m_mthGetAccountperiodTime(out DateTime datAccountperiodTime)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_mthGetAccountperiodTime(out datAccountperiodTime);
            return lngRes;
        }
        #endregion

        #region 获取药品是否已调价

        /// <summary>
        /// 获取药品是否已调价

        /// </summary>
        /// <param name="medicineid_chr">药品ID</param>
        /// <param name="lotno_vchr">批号</param>
        /// <param name="instorageid_vchr">入库单号</param>
        /// <param name="p_dtmValiDate">有效期</param>
        /// <param name="p_dblInPrice">购入价</param>
        /// <param name="p_dblAdjustrice">是否已调价</param>
        /// <returns></returns>
        public long m_mthGetAdjustrice(string medicineid_chr, string lotno_vchr, string instorageid_vchr, DateTime p_dtmValiDate, double p_dblInPrice, DateTime datNewdate, out bool p_dblAdjustrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_mthGetAdjustrice(medicineid_chr, lotno_vchr, instorageid_vchr, p_dtmValiDate, p_dblInPrice, datNewdate, out p_dblAdjustrice);
            return lngRes;
        }
        #endregion

        #region 获取药品调价单打印格式


        /// <summary>
        /// 获取药品调价单打印格式0,伦教 1,茶山
        /// </summary>
        /// limitunitprice_mny
        internal long m_lngGetPrintType(out int p_intCommitFolw)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsMS_Public_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetSysSetting("5024", out p_intCommitFolw);
            return lngRes;
        }
        #endregion
        #region 判断药品是否可以调价
        /// <summary>
        ///  判断药品是否可以调价
        /// </summary>
        /// <param name="m_strMedicineid">药品id</param>
        /// <param name="m_strReturnMsg">返回信息,如果返回为空表示可以进行调价；非空返回不能调价原因</param>
        /// <returns></returns>
        public long m_lngJudgeCanAdjustPriceByMedicineid(string m_strMedicineid, out string m_strReturnMsg)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngJudgeCanAdjustPriceByMedicineid(m_strMedicineid, out m_strReturnMsg);
            return lngRes;
        }

        /// <summary>
        ///  判断药品是否可以调价
        /// </summary>
        /// <param name="m_strMedicineid">药品id</param>
        /// <param name="m_strReturnMsg">返回信息,如果返回为空表示可以进行调价；非空返回不能调价原因</param>
        /// <returns></returns>
        public long m_lngJudgeCanAdjustPriceByMedicineID_ALLNewBill(string m_strMedicineid, out string m_strReturnMsg)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngJudgeCanAdjustPriceByMedicineID_ALLNewBill(m_strMedicineid, out m_strReturnMsg);
            return lngRes;
        }
        #endregion
        #region 根据药品获取药品的购入价信息
        /// <summary>
        /// 根据药品获取药品的购入价信息
        /// </summary>
        /// <param name="m_strMedicineid">药品id</param>
        /// <param name="m_dblCallInPrice">购入价</param>
        /// <returns></returns>
        public long m_lngGetMedicineCallInPriceByMedicineid(string m_strMedicineid, out double m_dblCallInPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsAdjustment_Supported_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetMedicineCallInPriceByMedicineid(m_strMedicineid, out m_dblCallInPrice);
            return lngRes;
        }
        #endregion

        #region 获取药品毛利率


        /// <summary>
        /// 获取药品毛利率

        /// </summary>
        /// <param name="p_strMedicineTypeID">药品类型ID</param>
        /// <param name="p_dblRate">毛利率</param>
        /// <returns></returns>
        public long m_lngGetGrossProfitRate(string p_strMedicineTypeID, out double p_dblRate)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC objSvc =
            //    (com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.MedicineStoreService.clsInStorage_Supported_MS_SVC));
            lngRes = (new weCare.Proxy.ProxyDrug01()).Service.m_lngGetGrossProfitRate(p_strMedicineTypeID, out p_dblRate);
            return lngRes;
        }
        #endregion
    }
}
