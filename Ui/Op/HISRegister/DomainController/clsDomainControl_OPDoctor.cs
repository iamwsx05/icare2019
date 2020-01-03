using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_OPDoctor 的摘要说明。
    /// </summary>
    public class clsDomainControl_OPDoctor : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_OPDoctor()
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

        #region 返回候诊列表
        /// <summary>
        /// 返回候诊列表
        /// </summary>
        public long m_lngGetWaitList(string strDocID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindWaitDiagList(strDocID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 接诊
        /// <summary>
        /// 接诊
        /// </summary>
        public long m_lngTakeWait(string strWaitID, string strRegID, string DepID, string DocID)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngTakeDiag(strWaitID, strRegID, DepID, DocID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 撤消接诊
        /// <summary>
        /// 撤消接诊
        /// </summary>
        public long m_lngUndoTakeWait(string strID, string strRegID)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngUndoTakeDiag(strID, strRegID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 返回接诊列表
        /// <summary>
        /// 返回接诊列表
        /// </summary>
        public long m_lngGetTakeList(string strDocID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindTakeDiagList(strDocID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询西药处方
        /// <summary>
        /// 查询西药处方
        /// </summary>
        public long m_lngGetWestRec(string strRegID, string strRecID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindWestRecipe(strRegID, strRecID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询中药处方
        /// <summary>
        /// 查询中药处方
        /// </summary>
        public long m_lngGetCMRec(string strRegID, string strRecID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindCMRecipe(strRegID, strRecID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询检验申请单
        /// <summary>
        /// 查询检验申请单
        /// </summary>
        public long m_lngGetCHKRec(string strRegID, string strRecID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindCHKRecipe(strRegID, strRecID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询检查申请单
        /// <summary>
        /// 查询检查申请单
        /// </summary>
        public long m_lngGetTestRec(string strRegID, string strRecID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindTestRecipe(strRegID, strRecID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询手术申请单
        /// <summary>
        /// 查询手术申请单
        /// </summary>
        public long m_lngGetOPSRec(string strRegID, string strRecID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindOPSRecipe(strRegID, strRecID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询其它处方
        /// <summary>
        /// 查询其它处方
        /// </summary>
        public long m_lngGetOtherRec(string strRegID, string strRecID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindOtherRecipe(strRegID, strRecID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询主处方
        /// <summary>
        /// 查询主处方
        /// </summary>
        public long m_lngGetMainRec(string strRegID, out clsOutpatientRecipe_VO[] clsVO)
        {
            long lngRes = 0;
            clsVO = new clsOutpatientRecipe_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindMainRecipe(strRegID, out clsVO);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 查询处方描述
        /// <summary>
        /// 查询处方描述
        /// </summary>
        public long m_lngGetRecDesc(string strRecID, out clsOutpatientRecipeDesc_VO clsVO)
        {
            long lngRes = 0;
            clsVO = new clsOutpatientRecipeDesc_VO();
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindRecipeDesc(strRecID, out clsVO);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        //保存处方
        #region 西药
        public long m_lngSaveWest(clsOutpatientRecipe_VO clsRec, clsOutpatientPWMRecipeDe_VO[] clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            if (IsNew)
                lngRes = proxy.Service.m_lngAddWestRecipe(clsVO, clsRec);
            else
            {
                lngRes = proxy.Service.m_lngUPDWestRecipe(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 中药
        public long m_lngSaveCM(clsOutpatientRecipe_VO clsRec, clsOutpatientCMRecipeDe_VO[] clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            if (IsNew)
                lngRes = proxy.Service.m_lngAddCMRecipe(clsVO, clsRec);
            else
            {
                lngRes = proxy.Service.m_lngUPDCMRecipe(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 检验
        public long m_lngSaveChk(clsOutpatientRecipe_VO clsRec, clsOutpatientCHKRecipeDe_VO[] clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            if (IsNew)
                lngRes = proxy.Service.m_lngAddCHKRecipe(clsVO, clsRec);
            else
            {
                lngRes = proxy.Service.m_lngUPDCHKRecipe(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 检查
        public long m_lngSaveTest(clsOutpatientRecipe_VO clsRec, clsOutpatientTestRecipeDe_VO[] clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            if (IsNew)
                lngRes = proxy.Service.m_lngAddTestRecipe(clsVO, clsRec);
            else
            {
                lngRes = proxy.Service.m_lngUPDTestRecipe(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 手术治疗
        public long m_lngSaveOPS(clsOutpatientRecipe_VO clsRec, clsOutpatientOPSRecipeDe_VO[] clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            if (IsNew)
                lngRes = proxy.Service.m_lngAddOPSRecipe(clsVO, clsRec);
            else
            {
                lngRes = proxy.Service.m_lngUPDOPSRecipe(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 其它处方
        public long m_lngSaveOther(clsOutpatientRecipe_VO clsRec, clsOutpatientOtherRecipeDe_VO[] clsVO, bool IsNew)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            if (IsNew)
                lngRes = proxy.Service.m_lngAddOtherRecipe(clsVO, clsRec);
            else
            {
                lngRes = proxy.Service.m_lngUPDOtherRecipe(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 处方描述
        public long m_lngSaveDesc(clsOutpatientRecipe_VO[] clsVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngUPDRecipeDesc(clsVO);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除处方明细
        public long m_lngDelRecipeDet(string strID, string RecID, int RecType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            switch (RecType)
            {
                case 1: //西药
                    lngRes = proxy.Service.m_lngDelWestRecipe(strID, RecID);
                    break;
                case 2: //中药
                    lngRes = proxy.Service.m_lngDelCMRecipe(strID, RecID);
                    break;
                case 3: //检验
                    lngRes = proxy.Service.m_lngDelCHKRecipe(strID, RecID);
                    break;
                case 4: //检查
                    lngRes = proxy.Service.m_lngDelTestRecipe(strID, RecID);
                    break;
                case 5: //手术治疗
                    lngRes = proxy.Service.m_lngDelOPSRecipe(strID, RecID);
                    break;
                case 6: //其它
                    lngRes = proxy.Service.m_lngDelOtherRecipe(strID, RecID);
                    break;
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 检查病人是否存在处方
        public long m_lngCheckMainRecipe(string strRegID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            long lngRes = proxy.Service.m_lngCheckMainRecipe(strRegID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 检查是否能查看病人处方
        /// <summary>
        /// 检查是否能查看病人处方
        /// </summary>
        public long m_lngCheckPatRecipe(string strRegID, string DocID, out DataTable dtResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngCheckPatRecipe(strRegID, DocID, out dtResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查看病历
        /// <summary>
        /// 查看病历
        /// </summary>
        public long m_lngFindPatCase(string strRegID,
            out clsOutpatientCaseHis_VO clsCase, out clsOutpatientDiagRec_VO clsDiag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngFindCaseAndCure(strRegID, out clsCase, out clsDiag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 保存病历
        /// <summary>
        /// 保存病历
        /// </summary>
        public long m_lngSavePatCase(clsOutpatientCaseHis_VO clsCase)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngSaveCase(clsCase);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 保存治疗记录
        /// <summary>
        /// 保存治疗记录
        /// </summary>
        public long m_lngSaveCureRec(clsOutpatientDiagRec_VO clsVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPDoctorSvc));
            lngRes = proxy.Service.m_lngSaveCure(clsVO);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
    }
}
