using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_injectInfo 的摘要说明。
    /// </summary>
    public class clsDcl_injectInfo : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_injectInfo()
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

        #region 获取病人信息
        public long m_mthGetPatientInfo(string strType, string strValue, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_mthGetPatientInfo(strType, strValue, out dt);
        }
        #endregion
        #region 获取用法信息
        public long m_mthFindUsage(int strValue, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_mthFindUsage(strValue, out dt);
        }
        #endregion
        #region 获取病人对应的类型的数据
        /// <summary>
        /// 护士工作站使用
        /// </summary>
        /// <param name="strPatientID">处方ID</param>
        /// <param name="strUsageType">用法类型</param>
        /// <param name="dt">返回的数据</param>
        /// <returns></returns>
        public long m_mthGetInputWet(string strRecipedeID, string strUsageType, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_mthGetInputWet(strRecipedeID, strUsageType, out dt);
        }
        #endregion
        #region 获取护士工作站分类病人及行目信息
        /// <summary>
        /// 获取护士工作站分类病人及行目信息
        /// </summary>
        /// <param name="strUsageType">分类ID</param>
        /// <param name="begiontime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="dt">返回病人列表</param>
        /// <returns></returns>
        public long m_mthGetAllData(string strUsageType, DateTime begiontime, DateTime endtime, out DataTable dt, string strFlag, string strCarNo, string patenitName, string strEmp, string m_strUseMode)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            //return proxy.Service.m_mthGetPatinetList( begiontime, endtime, strUsageType, out dt, strFlag, strCarNo, patenitName, strEmp, m_strUseMode);
            return proxy.Service.m_mthGetAllData(strUsageType, begiontime, endtime, out dt, strFlag, strCarNo, patenitName, strEmp, m_strUseMode);
        }
        #endregion
        #region 根据处方ID取出病人注射治疗单据签名信息
        public long m_lngGetSignInfoByRecipeID(string m_strRecipeID, out string m_strHasOrNot)
        {
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngGetSignInfoByRecipeID(m_strRecipeID, out m_strHasOrNot);
        }
        #endregion
        #region 获取科室数据
        /// <summary>
        /// 获取科室数据
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_mthGetAllDeptdesc(out DataTable p_dtDeptdesc)
        {
            p_dtDeptdesc = null;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_mthGetAllDeptdesc(out p_dtDeptdesc);
        }
        /// <summary>
        /// 获取用户所属科室数据
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_mthGetAllDeptdescByEmpId(out DataTable p_dtDept, string p_strEmpId)
        {
            p_dtDept = null;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_mthGetAllDeptdescByEmpId(out p_dtDept, p_strEmpId);
        }
        #endregion

        #region 批量新增记录到护士执行记录表
        /// <summary>
        /// 批量新增记录到护士执行记录表
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngAddNewToT_opr_nurseexecute(clst_opr_nurseexecute[] p_clsDataArr, out string[] p_strRecordIDArr)
        {
            p_strRecordIDArr = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngAddNewToT_opr_nurseexecute(p_clsDataArr, out p_strRecordIDArr);
        }
        #endregion

        #region 查询签名列表
        /// <summary>
        /// 查询签名列表
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngQueryOPERATORID_CHRAndName(clst_opr_nurseexecute p_clsData, out DataTable p_dtbData, bool p_blnAll)
        {
            p_dtbData = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngQueryOPERATORID_CHRAndName(p_clsData, out p_dtbData, p_blnAll);
        }
        #endregion

        #region 作废记录到护士执行记录表某字段并新入一条记录
        /// <summary>
        /// 作废记录到护士执行记录表某字段并新入一条记录
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngUpdateStateT_opr_nurseexecute(int p_intRecordID, clst_opr_nurseexecute p_clsData)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngUpdateStateT_opr_nurseexecute(p_intRecordID, p_clsData);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngQueryByID(out clst_opr_nurseexecute p_clsData, int p_intID)
        {
            p_clsData = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngQueryByID(out p_clsData, p_intID);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngUpdateStateT_opr_nurseexecute(clst_opr_nurseexecute p_clsData)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngUpdateStateT_opr_nurseexecute(p_clsData);
        }
        #endregion

        #region 批量更新记录到护士执行记录表(修改同组名称时使用)
        /// <summary>
        /// 批量更新记录到护士执行记录表同组修改同组名称时使用
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngUpdateMoreT_opr_nurseexecute(int p_intRecordID, clst_opr_nurseexecute p_clsTempData)
        {
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngUpdateMoreT_opr_nurseexecute(p_intRecordID, p_clsTempData);
        }
        #endregion

        #region 查询签名根据类型
        /// <summary>
        /// 查询签名根据类型
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngQueryOPERATORID_CHRAndNameByType(clst_opr_nurseexecute p_clsData, out DataTable p_dtbData)
        {
            p_dtbData = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngQueryOPERATORID_CHRAndNameByType(p_clsData, out p_dtbData);
        }
        #endregion

        #region 查询签名(用于治单,)
        /// <summary>
        /// 查询签名(用于治单,)
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngQueryNameBybusiness_intAndrecipeid(int p_business_int, string p_outpatrecipeid_chr, int p_intOPERATORTYPE_INT, out DataTable p_dtbData)

        {
            p_dtbData = null;
            //com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsWaitDiagListManageSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngQueryNameBybusiness_intAndrecipeid(p_business_int, p_outpatrecipeid_chr, p_intOPERATORTYPE_INT, out p_dtbData);
        }
        #endregion

        #region 根据病人ID与处方号,取出过敏表信息
        /// <summary>
        /// 根据病人ID与处方号,取出过敏表信息
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngGetAllergicByPidOutPid(string p_strPATIENTID, string p_strOUTPATRECIPEID, out clsT_opr_allergic p_objResult, out int p_intRecordCount)
        {
            p_objResult = null;
            p_intRecordCount = 0;
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngGetAllergicByPidOutPid(p_strPATIENTID, p_strOUTPATRECIPEID, out p_objResult, out p_intRecordCount);
        }
        #endregion

        #region 根据病人ID取出t_bse_patient表过敏表信息
        /// <summary>
        /// 根据病人ID取出t_bse_patient表过敏表信息
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngGetAllergicByPidFromTBSEPatient(string p_strPATIENTID, out string p_strIFALLERGIC, out string p_strALLERGICDESC)
        {
            p_strIFALLERGIC = "";
            p_strALLERGICDESC = "";

            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngGetAllergicByPidFromTBSEPatient(p_strPATIENTID, out p_strIFALLERGIC, out p_strALLERGICDESC);
        }
        #endregion

        #region 新增过敏表信息
        /// <summary>
        /// 新增过敏表信息
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngAddNewAllergic(clsT_opr_allergic p_objRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngAddNewAllergic(p_objRecord);
        }
        #endregion

        #region 修改过敏表信息(根据PATIENTID_CHR,OUTPATRECIPEID_CHR)
        /// <summary>
        /// 修改过敏表信息(根据PATIENTID_CHR,OUTPATRECIPEID_CHR)
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngAlterAllergic(clsT_opr_allergic p_objRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngAlterAllergic(p_objRecord);
        }
        #endregion

        #region 删除过敏表信息(根据PATIENTID_CHR,OUTPATRECIPEID_CHR)
        /// <summary>
        /// 删除过敏表信息(根据PATIENTID_CHR,OUTPATRECIPEID_CHR)
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngDeleteAllergic(clsT_opr_allergic p_objRecord)
        {
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngDeleteAllergic(p_objRecord);
        }
        /// <summary>
        /// 删除过敏表信息(根据PATIENTID_CHR,OUTPATRECIPEID_CHR)
        /// </summary>
        /// <param name="p_dtDeptdesc"></param>
        /// <returns></returns>
        public long m_lngDeleteAllergic(string p_strPATIENTID_CHR, string p_strCreateDate)
        {
            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngDeleteAllergic(p_strPATIENTID_CHR, p_strCreateDate);
        }
        #endregion
        /// <summary>
        /// 更新护士工作站打印功能
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strRecipeid"></param>
        /// <returns></returns>
        public long m_lngUpdatePrintFlag(string m_strRecipeid)
        {


            //com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsinjectInfoSvc));
            return proxy.Service.m_lngUpdatePrintFlag(m_strRecipeid);


        }

        public long m_lngGetData(string p_StrTypeID, string p_Strorderid, out DataTable dt)
        {
            //        com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            return (new weCare.Proxy.ProxyOP()).Service.m_lngGetData(p_StrTypeID, p_Strorderid, out dt);
        }
    }
}
