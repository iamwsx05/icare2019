using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrolChangPrice 的摘要说明。
    /// </summary>
    public class clsDomainConrolChangPrice : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrolChangPrice()
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

            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMedicine(out dtbMedicine);
            return lngRes;
        }
        #endregion

        #region 统计库存
        /// <summary>
        /// 统计库存
        /// </summary>
        /// <param name="MedicineID"></param>
        /// <param name="AllAmount"></param>
        /// <returns></returns>
        public long m_lngCountStroage(string MedicineID, out int AllAmount)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngCountStroage(MedicineID, out AllAmount);
            return lngRes;
        }
        #endregion
        #region 保存调价单
        /// <summary>
        /// 保存调价单
        /// </summary>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="objPriceChgeApplDe"></param>
        /// <returns></returns>
        public long m_lngSaveChangPriceData(clsPriceChgeAppl objPriceChgeApplVO, clsPriceChgeApplDe[] objPriceChgeApplDe, out string strID)
        {
            long lngRes = 0;
            strID = "";
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));

            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveChangPriceData(objPriceChgeApplVO, objPriceChgeApplDe, out strID);
            }
            catch (Exception ee)
            {
                string err = ee.Message;
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion

        #region 获取所有的调价单数据根据财务期
        /// <summary>
        /// 获取所有的调价单数据
        /// </summary>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="nowPriod">财务期</param>
        /// <returns></returns>
        public long m_lngGetAllChgAppl(out clsPriceChgeAppl[] objPriceChgeApplVO, string nowPriod)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllChgAppl(out objPriceChgeApplVO, nowPriod);
            return lngRes;
        }
        #endregion


        #region 删除调价申请单
        /// <summary>
        /// 删除调价申请单
        /// </summary>
        /// <param name="ChangPriceID"></param>
        /// <returns></returns>
        public long m_mthDele(string ChangPriceID)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_mthDele(ChangPriceID);
            return lngRes;
        }
        #endregion

        #region 合并调价单
        /// <summary>
        /// 合并调价单
        /// </summary>
        /// <param name="ArrAppLID"></param>
        /// <param name="ArrAppLNO"></param>
        /// <param name="strPriod"></param>
        /// <param name="strEmp"></param>
        /// <param name="newNO"></param>
        /// <param name="objPriceChgeApplVO"></param>
        /// <returns></returns>
        public long m_lngUniteChangPriceData(System.Collections.Generic.List<string> ArrAppLID, System.Collections.Generic.List<string> ArrAppLNO, string strPriod, string strEmp, string newNO, out clsPriceChgeAppl objPriceChgeApplVO)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngUniteChangPriceData(ArrAppLID, ArrAppLNO, strPriod, strEmp, newNO, out objPriceChgeApplVO);
            return lngRes;
        }
        #endregion

        #region 获取所有的调价单数据根据财务期
        /// <summary>
        /// 获取所有的调价单数据
        /// </summary>
        /// <param name="objPriceChgeApplVO"></param>
        /// <param name="nowPriod">财务期</param>
        /// <returns></returns>
        public long m_lngGetAllChgAppl(out DataSet ds, string nowPriod)
        {
            long lngRes = 0;
            ds = null;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllChgAppl(out ds, nowPriod);
            }
            catch
            {
                return -1;
            }

            return lngRes;
        }
        #endregion

        #region 通过单号ID获得调价单明细
        /// <summary>
        /// 通过单号ID获得调价单明细
        /// </summary>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetChgApplDe(string p_strID, out clsPriceChgeApplDe[] p_objResult, int PSTATUS_INT)
        {
            p_objResult = null;
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChgApplDe(p_strID, out p_objResult, PSTATUS_INT);
            return lngRes;
        }
        #endregion

        #region 修改调价单
        /// <summary>
        /// 修改调价单
        /// </summary>
        /// <param name="p_objResultDe">明细</param>
        /// <param name="p_objResult">单数据</param>
        /// <returns></returns>
        public long m_lngMondifiy(clsPriceChgeApplDe p_objResultDe, clsPriceChgeAppl p_objResult)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngMondifiy(p_objResultDe, p_objResult);
            return lngRes;
        }
        #endregion

        #region 删除调价单
        /// <summary>
        /// 删除调价单
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDeleAppl(string strID)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleAppl(strID);
            return lngRes;
        }
        #endregion

        #region 删除调价单明细
        /// <summary>
        /// 删除调价单明细
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDeleteDeById(string strID)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteDeById(strID);
            return lngRes;
        }
        #endregion

        #region 审核调价单
        /// <summary>
        /// 审核调价单
        /// </summary>
        /// <param name="ADUITEMPID"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngTrialChang(clsPriceChgeAppl objApplVO, clsPriceChgeApplDe[] objApplDeArr)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAuditChangePrice(objApplVO, objApplDeArr);
            }
            catch (Exception ee)
            {
                string err = ee.Message;
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion

        #region 增加调价明细数据
        /// <summary>
        /// 增加调价明细数据
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="Odds"></param>
        /// <param name="strAPPLID"></param>
        /// <param name="objPriceChgeApplDe"></param>
        /// <returns></returns>
        public long m_lngAddDe(string p_strID, clsPriceChgeApplDe objPriceChgeApplDe)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddDe(p_strID, objPriceChgeApplDe);
            return lngRes;
        }
        #endregion

        #region 获取调价类别
        public long m_lngGetChangeType(out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAppChangePriceType(out dt);
            return lngRes;
        }
        #endregion 获取调价类别

        #region 调价报告
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strStartDate">yyyy-mm-dd</param>
        /// <param name="p_strEndDate">yyyy-mm-dd</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChangePriceRpt(System.Collections.Generic.List<string> arrList, out System.Data.DataTable dt)
        {
            long lngRes = 0;
            //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetChangePriceRpt(arrList, out dt);
            return lngRes;
        }
        #endregion 调价报告 
        #region 获取最大单号
        public string m_mthGetMaxNo(string strDate)
        {
            string ret = "";
            try
            {
                //clsChangPrice objChangPrice = (clsChangPrice)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChangPrice));
                ret = (new weCare.Proxy.ProxyMedStore()).Service.m_mthGetMaxNo(strDate);
            }
            catch
            {

            }
            return ret;
        }
        #endregion
    }
}
