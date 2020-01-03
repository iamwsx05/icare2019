using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_RegDefine 的摘要说明。
    /// </summary>
    public class clsDomainControl_RegDefine : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_RegDefine()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP02 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP02();
            }
        }
        #endregion

        // 挂号种类
        #region 查询挂号种类 created by Cameron Wong on Aug 9, 2004
        public long m_lngFindRegType(out clsRegType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngFindRegTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增挂号种类 created by Cameron Wong on Aug 9, 2004
        public long m_lngAddRegType(clsRegType_VO p_objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoAddNewRegType(p_objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改挂号种类 created by Cameron Wong on Aug 9, 2004
        public long m_lngDoUpdRegByID(clsRegType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdRegTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 停用挂号种类 created by Cameron Wong on Aug 9, 2004
        public long m_lngIsUseing(string p_strID, string p_Isusering)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngIsUseing(p_strID, p_Isusering);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除挂号种类 created by Cameron Wong on Aug 9, 2004
        public long m_lngDelRegByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDelRegTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        // 煎制方法
        #region 查询挂号种类 created by Cameron Wong on Aug 9, 2004
        public long m_lngFindCookMethodList(out clsCookMethod_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngFindCookMethodList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增煎制方法 created by Cameron Wong on Aug 11, 2004
        public long m_lngAddCookMethod(string strName, string strMNemonic, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoAddNewCookMethod(strName, strMNemonic, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改煎制方法 created by Cameron Wong on Aug 11, 2004
        public long m_lngDoUpdMethodByID(clsCookMethod_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdMethodByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除煎制方法 created by Cameron Wong on Aug 11, 2004
        public long m_lngDelMethodByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegTypeSvc));
            lngRes = proxy.Service.m_lngDelMethodByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        //挂号费种
        #region 获取挂号费种列表	张国良		2004-8-8
        public long m_lngFindType(out clsRegchargeType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngFindRegChargeTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增挂号费种	张国良		2004-8-8
        public long m_lngAddType(clsRegchargeType_VO objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngAddNewRegChargeType(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改挂号费种	张国良		2004-8-8
        public long m_lngDoUpdTypeByID(clsRegchargeType_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdRegChargeTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 停用挂号费种	张国良		2004-9-22
        public long m_lngIsUseingRgechargeType(string p_strID, string p_Isusering)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));

            lngRes = proxy.Service.m_lngIsUseingRgechargeType(p_strID, p_Isusering);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除挂号费种	张国良		2004-8-8
        public long m_lngDelTypeByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDeleteRegChargeTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion


        //挂号身份
        #region 获取挂号身份列表	张国良		2004-8-9
        public long m_lngFindPatientPayTypeList(out clstPatientPaytype_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngFindPatientPayTypeList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增挂号身份	张国良		2004-8-9
        public long m_lngAddPatientPayType(clstPatientPaytype_VO objResult, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngAddNewPatientPayType(objResult, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改挂号身份	张国良		2004-8-9
        public long m_lngDoUpdPatientPayTypeByID(clstPatientPaytype_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDoUpdPatientPayTypeByID(p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 停用挂号身份	张国良		2004-9-22
        public long m_lngIsUseingPAYTYPE(string p_strID, string p_Isusering)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngIsUseingPAYTYPE(p_strID, p_Isusering);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除挂号身份	张国良		2004-8-9
        public long m_lngDelTPatientPayTypeByID(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegChargeTypeSvc));
            lngRes = proxy.Service.m_lngDeletePatientPayTypeByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

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
            long lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetINSPLANDataArr(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

    }
}
