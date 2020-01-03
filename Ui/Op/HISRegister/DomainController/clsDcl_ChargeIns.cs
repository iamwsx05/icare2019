using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ChargeIns 的摘要说明。
    /// </summary>
    public class clsDcl_ChargeIns : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ChargeIns()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP01();
            }
        }
        #endregion

        //保险公司
        #region 获取保险公司列表	张国良		2004-9-22
        /// <summary>
        /// 获取保险公司列表
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetINSCOMPANYDataArr(out clsInsCompany_VO[] p_objResultArr)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngGetINSCOMPANYDataArr(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region 新增保险公司	张国良		2004-9-24
        /// <summary>
        /// 新增保险公司
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngAddNewINSCOMPANYD(clsInsCompany_VO objResult, out string strID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngAddNewINSCOMPANYD(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改保险公司信息	张国良		2004-9-24
        /// <summary>
        /// 修改保险公司信息
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngModifyINSCOMPANYD(clsInsCompany_VO objResult)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngModifyINSCOMPANYD(objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除保险公司信息	张国良		2004-9-24
        /// <summary>
        /// 删除保险公司信息
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngINSCOMPANYDel(string strID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngINSCOMPANYDel(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        //保险计划	
        #region 获取保险计划列表	张国良		2004-9-24
        /// <summary>
        /// 获取保险计划列表
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetINSPLANDataArr(out clsInsPlan_VO[] p_objResultArr)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngGetINSPLANDataArr(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增保险计划	张国良		2004-9-24
        /// <summary>
        /// 新增保险计划
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngAddNewINSPLAN(clsInsPlan_VO objResult, out string strID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngAddNewINSPLAN(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改保险计划	张国良		2004-9-24
        /// <summary>
        /// 修改保险计划
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngModifyINSPLAN(clsInsPlan_VO objResult)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngModifyINSPLAN(objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除保险计划	张国良		2004-9-24
        /// <summary>
        /// 删除保险计划
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngDelINSPLAN(string strID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngDelINSPLAN(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        //保险计划	
        #region 获取保险种类列表	张国良		2004-9-27
        /// <summary>
        /// 获取保险计划列表
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetINSCOPAYataArr(out clsInsPay_VO[] p_objResultArr)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngGetINSCOPAYataArr(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增保险种类	张国良		2004-9-27
        /// <summary>
        /// 新增保险计划
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngAddNewINSCOPAY(clsInsPay_VO objResult, out string strID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngAddNewINSCOPAY(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改保险种类	张国良		2004-9-27
        /// <summary>
        /// 修改保险计划
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngModifyINSCOPAY(clsInsPay_VO objResult)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngModifyINSCOPAY(objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除保险种类	张国良		2004-9-27
        /// <summary>
        /// 删除保险计划
        /// </summary>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public long m_lngDelINSCOPAY(string strID)
        {

            //com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPChargeInsSvc));
            long lngRes = proxy.Service.m_lngDelINSCOPAY(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

    }
}
