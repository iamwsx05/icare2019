using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrol_ConcertreCipe 的摘要说明。
    /// </summary>
    public class clsDomainConrol_ConcertreCipe : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrol_ConcertreCipe()
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

        #region 协定处方系统(新)

        #region 查找病人类型
        /// <summary>
        /// 查找病人类型
        /// </summary>
        public long m_lngGetPatType(out clsPatientType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            p_objResultArr = new clsPatientType_VO[0];
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsRegisterSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRegisterSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRegisterSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPatType(out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region 查找自付比例
        /// <summary>
        /// 查找自付比例
        /// </summary>
        public long m_longPrecent(DataTable dt, out DataTable dt1, string payType)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //			System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_longPrecent(dt, out dt1, payType);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region 获取协定处方
        /// <summary>
        /// 获取协定处方m_lngGetConcertreCipeDetailByIDOutTb
        /// </summary>
        public long m_lngGetConcertreCipeByEmpIDOutTB(string CREATERID, string strID, out DataTable p_objResultArr, int intFLAG, bool isPublic)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeByEmpIDOutTB(CREATERID, strID, out p_objResultArr, intFLAG, isPublic);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取协定处方ID取协处方明细
        /// <summary>
        /// 获取协定处方
        /// </summary>
        public long m_lngGetConcertreCipeDetailByIDOutTb(string strID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeDetailByIDOutTb(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取协定处方所属的部门
        /// <summary>
        /// 获取协定处方所属的部门
        /// </summary>
        public long m_lngGetDeptByConcertreCipeID(string strID, out DataTable p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetDeptByConcertreCipeID(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取行目数据
        /// <summary>
        /// 获取行目数据
        /// </summary>
        public long m_mthFindMedicine(out DataTable dtbResult, string strType)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindMedicine(out dtbResult, strType);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取所有的部门信息
        /// <summary>
        /// 获取所有的部门信息
        /// </summary>
        public long m_lngGetDeptList(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetDeptList(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取所有的频率
        /// <summary>
        /// 获取所有的频率信息
        /// </summary>
        public long m_mthFindFrequency(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindFrequency(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取所有的用法
        /// <summary>
        /// 获取所有的用法信息
        /// </summary>
        public long m_mthFindUsage(out DataTable dtbResult)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindUsage(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 增加新的协处方
        /// <summary>
        /// 增加新的协处方
        /// </summary>
        public long m_lngAddNewConcertre(out string p_strRecordID, string[] bt, DataTable btDe, DataTable btDetp, string isDetp, int intFLAG)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertre(out p_strRecordID, bt, btDe, btDetp, isDetp, intFLAG);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        public long m_lngDeleteConcertrecipeAndDe(string[] DeleRow, string[] DeleRowDe, string strItem, string strFlag)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipeAndDe(DeleRow, DeleRowDe, strItem, strFlag);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 修改处方明细
        /// <summary>
        /// 修改处方明细
        /// </summary>
        /// <param name="strID">处方ID</param>
        /// <param name="dtRow">新的明细数据</param>
        /// <param name="oldITEMID">旧的明细项目ID，如= null修改单条记录，！=NULL所有修改处方名细中相同项目的数据</param>
        /// <param name="blIsPublic">是否有公用权限</param>
        /// <param name="CREATERID">创建人</param>
        /// <returns></returns>
        public long m_lngConcertreCipeDetailModifyDe(string strID, string[] dtRow, string oldITEMID, string strFLAG, bool blIsPublic, string CREATERID, int m_intSort)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeDetailModifyDe(strID, dtRow, oldITEMID, strFLAG, blIsPublic, CREATERID, m_intSort);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 修改协定处方
        /// <summary>
        /// 修改协定处方
        /// </summary>
        public long m_lngConcertreModify(string[] ModifiyRow, DataTable Deptbt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreModify(ModifiyRow, Deptbt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 新增明细
        /// <summary>
        /// 新增明细
        /// </summary>
        public long m_lngConcertreCipeDetailAddNEWDe(string strID, string[] btDe, int m_intSort)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeDetailAddNEWDe(strID, btDe, m_intSort);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region 检查当前登陆的用户是否有编辑公用处方的权限
        /// <summary>
        /// 检查当前登陆的用户是否有编辑公用处方的权限
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID">用户ID</param>
        /// <param name="isPublic">false-没有权限,true-有权限</param>
        /// <returns></returns>
        public long m_lngGetPublic(string strID, out bool isPublic)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetPublic(strID, out isPublic);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #endregion


        #region 获取频率的次数及天数
        /// <summary>
        /// 获取频率的次数及天数
        /// </summary>
        /// <param name="strResult"></param>
        /// <param name="strFREQID"></param>
        /// <returns></returns>
        public long m_lngGetDayAndTime(out string strResult, out string strResult1, string strFREQID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetDayAndTime(out strResult, out strResult1, strFREQID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取协定处方
        /// <summary>
        /// 获取协定处方
        /// </summary>
        public long m_lngGetConcertreCipeByEmpID(string strID, out clsConcertrectpe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsConcertrectpe_VO[0];
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeByEmpID(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取协定处方明细
        /// <summary>
        /// 获取协定处方明细
        /// </summary>
        public long m_lngGetConcertreCipeDetailByID(string strID, out clsConcertrecipeDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsConcertrecipeDetail_VO[0];
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeDetailByID(strID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取使用部门
        /// <summary>
        /// 获取协定处方明细
        /// </summary>
        public long m_lngGetConcertreCipeDeptByID(string strID, out clsConcertrecipeDept_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsConcertrecipeDept_VO[0];
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetConcertreCipeDeptByID(strID, out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增处方
        /// <summary>
        /// 新增处方
        /// </summary>
        public long m_lngAddNewConcertreCipe(out string strID, clsConcertrectpe_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrectpe_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertreCipe(out strID, p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 新增处方明细
        /// <summary>
        /// 新增处方明细
        /// </summary>
        public long m_lngAddNewConcertreCipeDetail(out string strID, clsConcertrecipeDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrecipeDetail_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertreCipeDetail(out strID, p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 新增使用部门
        /// <summary>
        /// 新增处方明细
        /// </summary>
        public long m_lngAddNewConcertreCipeDept(clsConcertrecipeDept_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrecipeDept_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngAddNewConcertreCipeDept(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 修改协定处方
        /// <summary>
        /// 修改协定处方
        /// </summary>
        public long m_lngConcertreCipeModify(clsConcertrectpe_VO p_objResultArr)
        {
            long lngRes = 0;
            //	p_objResultArr=new clsConcertrectpe_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeModify(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 修改协定处方明细
        /// <summary>
        /// 修改协定处方明细
        /// </summary>
        public long m_lngConcertreCipeDetailModify(clsConcertrecipeDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            //		p_objResultArr=new clsConcertrecipeDetail_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngConcertreCipeDetailModify(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 删除协定处方明细
        /// <summary>
        /// 删除协定处方明细
        /// </summary>
        public long m_lngDeleteConcertrecipeDetail(clsConcertrecipeDetail_VO p_objResultArr)
        {
            long lngRes = 0;
            //		p_objResultArr=new clsConcertrecipeDetail_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipeDetail(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 删除协定处方
        /// <summary>
        /// 删除协定处方
        /// </summary>
        public long m_lngDeleteConcertrecipe(clsConcertrectpe_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrectpe_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipe(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region 删除使用部门
        /// <summary>
        /// 删除使用部门
        /// </summary>
        public long m_lngDeleteConcertrecipeDept(clsConcertrecipeDept_VO p_objResultArr)
        {
            long lngRes = 0;
            //p_objResultArr=new clsConcertrecipeDept_VO();
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDeleteConcertrecipeDept(p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region 检查助记码是否使用
        /// <summary>
        /// 检查助记码是否使用
        /// </summary>
        /// <param name="strID">如是ID 不为空就是修改时使用</param>
        /// <returns></returns>
        public long m_mthCheckCodeIsUsed(string strCode, string strID, string strFlag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthCheckCodeIsUsed(strCode, strID, strFlag);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region 获取检查部位及检验样本
        /// <summary>
        /// 获取检查部位及检验样本
        /// </summary>
        /// <param name="P_dtPark">返回检查部位或检验样本</param>
        /// <param name="ParkName">返回项目对应的检验样本名称</param>
        /// <param name="parkID">返回项目对应的检验样本ID</param>
        /// <param name="strItemId">原项目ID（检验样本）</param>
        /// <param name="strType">0-检验样本，其它检查部位</param>
        /// <returns></returns>
        public long m_lngGetPart(out DataTable P_dtPark, out string ParkName, out string parkID, string strItemId, string strType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsConcertreCipeSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetPart(out P_dtPark, out ParkName, out parkID, strItemId, strType);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
    }
}
