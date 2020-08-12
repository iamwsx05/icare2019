using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
    #region 调价控制层
    /// <summary>
    /// 调价控制层
    /// Create by kong 2004-06-09
    /// </summary>
    public class clsDomainControlMedicinePriceChg : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDomainControlMedicinePriceChg()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 新增调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 新增调价记录单
        /// </summary>
        /// <param name="p_objItem">调价记录单信息</param>
        /// <returns>返回值</returns>
        public long m_lngDoAddNewMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicinePriceChgAppl(p_objItem);
            //objSrc.Dispose();

            return lngRes;
        }
        #endregion

        #region 修改调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 修改调价记录单
        /// </summary>
        /// <param name="p_objItem">调价记录单信息</param>
        /// <returns></returns>
        public long m_lngDoUpdMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicinePriceChgAppl(p_objItem);
            //objSrc.Dispose();

            return lngRes;
        }
        #endregion

        #region 删除调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 删除调价记录单
        /// </summary>
        /// <param name="p_strID">调价记录单ID号</param>
        /// <returns></returns>
        public long m_lngDoDeleteMedicinePriceChgAppl(string p_strID)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoDeleteMedicinePriceChgAppl(p_strID);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 新增调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 新增调价明细单
        /// </summary>
        /// <param name="p_objItem">调价明细单信息</param>
        /// <returns></returns>
        public long m_lngDoAddNewMedicinePriceChgApplDe(clsMedicinePriceChgApplDe_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicinePriceChgApplDe(p_objItem);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 修改调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 修改调价明细单
        /// </summary>
        /// <param name="p_objItem">调价明细单信息</param>
        /// <returns></returns>
        public long m_lngDoUpdMedicinePriceChgApplDe(clsMedicinePriceChgApplDe_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicinePriceChgApplDe(p_objItem);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 删除调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 删除调价明细单
        /// </summary>
        /// <param name="p_strID">调价明细单ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteMedicinePriceChgApplDe(string p_strID)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoDeleteMedicinePriceChgApplDe(p_strID);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 审核调价单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 审核调价单
        /// </summary>
        /// <param name="p_objItem">调价记录单信息</param>
        /// <returns></returns>
        public long m_lngDoAduitMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAduitMedicinePriceChgAppl(p_objItem);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 审核调价单，更改价格  欧阳孔伟  2004-06-09
        /// <summary>
        /// 审核调价单更改价格
        /// </summary>
        /// <param name="p_strNo">调价单号</param>
        /// <param name="p_intFlag">运行时输出标识，1为成功，0为更改价格出错，－1为发生异常</param>
        /// <returns></returns>
        public long m_lngDoChangePriceAfterAduit(string p_strNo, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoChangePriceAfterAduit(p_strNo, out p_intFlag);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 模糊查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 模糊查找调价记录单
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByAny(string p_strSQL, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByAny(p_strSQL, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按调价记录单ID号查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按调价记录单ID号查找调价记录单
        /// </summary>
        /// <param name="p_strID">调价记录单ID号</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByID(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByID(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按调价记录单号查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按调价记录单号查找调价记录单
        /// </summary>
        /// <param name="p_strNo">调价记录单号</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByNo(string p_strNo, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByNo(p_strNo, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region  按单据类型查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        ///  按单据类型查找调价记录单
        /// </summary>
        /// <param name="p_strID">单据类型ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByStorageOrdType(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByStorageOrdType(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region  按仓库查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按仓库查找调价记录单
        /// </summary>
        /// <param name="p_strID">仓库ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByStorage(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByStorage(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按帐务期查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按帐务期查找调价记录单
        /// </summary>
        /// <param name="p_strID">帐务期ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByPeriod(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByPeriod(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按申请时间段查找调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按申请时间段查找调价记录单
        /// </summary>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplByDate(string p_strStartDate, string p_strEndDate, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplByDate(p_strStartDate, p_strEndDate, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找所有的调价记录单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 查找所有的调价记录单
        /// </summary>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindAllMedicinePriceChgAppl(out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedicinePriceChgAppl(out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 模糊查找调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 模糊查找调价明细单
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByAny(string p_strSQL, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByAny(p_strSQL, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按调价明细单ID号查找调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按调价明细单ID号查找调价明细单
        /// </summary>
        /// <param name="p_strID">调价明细单ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByID(string p_strID, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByID(p_strID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按调价记录单号查找调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按调价记录单号查找调价明细单
        /// </summary>
        /// <param name="p_strNo">调价记录单号</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByApplNo(string p_strNo, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByApplNo(p_strNo, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 按药品查找调价明细单  欧阳孔伟  2004-06-09
        /// <summary>
        /// 按药品查找调价明细单
        /// </summary>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        public long m_lngFindMedicinePriceChgApplDeByMedicine(string p_strMedicineID, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicinePriceChgApplDeByMedicine(p_strMedicineID, out p_objResultArr);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找当前最大的调价记录单ID号  欧阳孔伟  2004-06-09
        /// <summary>
        /// 查找当前最大的调价记录单ID
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetApplID(out string p_strResult)
        {

            long lngRes = 0;

            p_strResult = null;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetApplID(out p_strResult);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找当前单据类型最大的调价记录单号  欧阳孔伟  2004-06-09
        /// <summary>
        /// 查找当前单据类型最大的调价记录单号
        /// </summary>
        /// <param name="p_strStorageOrdTypeID">单据类型ID</param>
        /// <param name="p_strResult">输出信息</param>
        /// <returns></returns>
        public long m_lngGetApplNo(string p_strStorageOrdTypeID, out string p_strResult)
        {

            long lngRes = 0;

            p_strResult = null;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetApplNo(p_strStorageOrdTypeID, out p_strResult);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

        #region 查找当前最大的调价明细单ID号  欧阳孔伟  2004-06-09
        /// <summary>
        /// 查找当前最大的调价明细单ID号
        /// </summary>
        /// <param name="p_strResult">输出信息</param>
        /// <returns></returns>
        public long m_lngGetApplDeID(out string p_strResult)
        {

            long lngRes = 0;

            p_strResult = null;

            //clsMedicinePriceChgSvc objSrc = (clsMedicinePriceChgSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicinePriceChgSvc));
            //System.Security.Principal.IPrincipal p_objPrincipal = null;

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetApplDeID(out p_strResult);
            //objSrc.Dispose();
            return lngRes;
        }
        #endregion

    }
    #endregion
}
