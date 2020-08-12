using System;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 帐务控制层
    /// Create by kong 2004-06-16
    /// </summary>
    public class clsDomainControlStorageFin : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDomainControlStorageFin()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 登帐及改帐  欧阳孔伟  2004-06-16

        #region 查询登帐单据
        /// <summary>
        /// 查询登帐单据
        /// </summary>
        /// <param name="p_strPeriodID">帐务期代码</param>
        /// <param name="p_strStorageID">库房代码</param>
        /// <param name="p_blnFlag">审核标志，true：未审核，false：已审核</param>
        /// <param name="p_dtbResult">输出数据</param>
        /// <returns></returns>
        public long m_lngSelectAcct(string p_strPeriodID, string p_strStorageID, bool p_blnFlag, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngSelectAcct(p_strStorageID, p_strPeriodID, p_blnFlag, out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region 出入库登帐
        /// <summary>
        /// 出入库登帐
        /// </summary>
        /// <param name="p_objItem">出入库数据</param>
        /// <returns></returns>
        public long m_lngAcct(clsStorageOrd_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAcct(p_objItem);

            return lngRes;
        }
        #endregion

        #region 出入库登帐后更改帐务
        /// <summary>
        /// 出入登帐后更改帐务
        /// </summary>
        /// <param name="p_strID">单据号</param>
        /// <param name="p_strTypeID">单据类型号</param>
        /// <param name="p_intFlag">返回标识，1：成功  0：失败  -1：异常</param>
        /// <returns></returns>
        public long m_lngChgFinAfterOrdAcct(string p_strID, string p_strTypeID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChgFinAfterOrdAcct(p_strID, p_strTypeID, out p_intFlag);


            return lngRes;
        }
        #endregion

        #region 盘点单登帐
        /// <summary>
        /// 盘点单登帐
        /// </summary>
        /// <param name="p_objItem">盘点单数据</param>
        /// <returns></returns>
        public long m_lngAcct(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAcct(p_objItem);


            return lngRes;
        }
        #endregion

        #region 盘点单登帐后更改帐务
        /// <summary>
        /// 盘点单登帐后更改帐务
        /// </summary>
        ///	<param name="p_strID">盘点单号</param>
        /// <param name="p_intFlag">返回标识，1：成功，0：失败，-1：异常</param>
        /// <returns></returns>
        public long m_lngChgFinAfterCheckAcct(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChgFinAfterCheckAcct(p_strID, out p_intFlag);


            return lngRes;
        }
        #endregion

        #region 调价单登帐
        /// <summary>
        /// 调价单登帐
        /// </summary>
        /// <param name="p_objItem">调价单数据</param>
        /// <returns></returns>
        public long m_lngAcct(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAcct(p_objItem);


            return lngRes;
        }
        #endregion

        #region 调价登帐后更改帐务
        /// <summary>
        /// 调价登帐后更改帐务
        /// </summary>
        /// <param name="p_strNo">调价单号</param>
        /// <param name="p_intFlag">返回标识，1：成功  0：失败  -1：异常</param>
        /// <returns></returns>
        public long m_lngChgFinAfterChangePriceAcct(string p_strNo, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChgFinAfterChangePriceAcct(p_strNo, out p_intFlag);


            return lngRes;
        }
        #endregion
        #endregion

        #region 帐务查询  欧阳孔伟  2004-06-16

        #region 库房药品明细帐

        #region 模糊查找库房药品明细帐
        /// <summary>
        /// 模糊查询库房药品明细帐
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByAny(string p_strSQL, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按库房查询库房药品明细帐
        /// <summary>
        /// 按库房查询库房药品明细帐
        /// </summary>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByStorage(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按帐务期查询库房药品明细帐
        /// <summary>
        /// 按帐务期查询库房药品明细帐
        /// </summary>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByPeriod(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByPeriod(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按单据类型查询库房药品明细帐
        /// <summary>
        /// 按单据类型查询库房药品明细帐
        /// </summary>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByType(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按药品查询库房药品明细帐
        /// <summary>
        /// 按药品查询库房药品明细帐
        /// </summary>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByMedicine(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按时间段查询库房药品明细帐
        /// <summary>
        /// 按登帐时间段查询库房药品明细帐
        /// </summary>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageOrdFinByDate(string p_strStartDate, string p_strEndDate, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdFinByDate(p_strStartDate, p_strEndDate, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region 库房药品月结帐

        #region 模糊查询库房药品月结帐
        /// <summary>
        /// 模糊查询库房药品月结帐
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByAny(string p_strSQL, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按仓库查询库房药品月结帐
        /// <summary>
        /// 按仓库查询库房药品月结帐
        /// </summary>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByStorage(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按帐务期查询库房药品月结帐
        /// <summary>
        /// 按帐务期查询库房药品月结帐
        /// </summary>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByPeriod(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByPeriod(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按单据类型查询库房药品月结帐
        /// <summary>
        /// 按单据类型查询库房药品月结帐
        /// </summary>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByType(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按药品查询库房药品月结帐
        /// <summary>
        /// 按药品查询库房药品月结帐
        /// </summary>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedMonFinByMedicine(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedMonFinByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region 库房药品总帐

        #region 模糊查询库房药品总帐
        /// <summary>
        /// 模糊查询库房药品总帐
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByAny(string p_strSQL, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按库房查询库房药品总帐
        /// <summary>
        /// 按库房查询库房药品总帐
        /// </summary>
        /// <param name="p_strID">库房帐号</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByStorage(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按单据类型查询库房药品总帐
        /// <summary>
        /// 按单据类型查询库房药品总帐
        /// </summary>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByType(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按药品查询库房药品总帐
        /// <summary>
        /// 按药品查询库房药品总帐
        /// </summary>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMedFinByMedicine(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMedFinByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region 库房月结帐

        #region 模糊查询库房月结帐
        /// <summary>
        /// 模糊查询库房月结帐
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByAny(string p_strSQL, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按库房查询库房月结帐
        /// <summary>
        /// 按库房查询库房月结帐
        /// </summary>
        /// <param name="p_strID">库房代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByStorage(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按帐务期查询库房月结帐
        /// <summary>
        /// 按帐务期查询库房月结帐
        /// </summary>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByPeriod(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByPeriod(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按单据类型查询库房月结帐
        /// <summary>
        /// 按单据类型查询库房月结帐
        /// </summary>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageMonFinByType(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageMonFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region 库房总帐

        #region 模糊查询库房总帐
        /// <summary>
        /// 模糊查询库房总帐
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageFinByAny(string p_strSQL, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageFinByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按库房查询库房总帐
        /// <summary>
        /// 按库房查询库房总帐
        /// </summary>
        /// <param name="p_strID">库房代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageFinByStorage(string p_strID, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageFinByStorage(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按单据类型查询库房总帐
        /// <summary>
        /// 按单据类型查询库房总帐
        /// </summary>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageFinByType(string p_strID, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageFinByType(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #endregion

        #region 新系统的方法

        #region 获得出入库及调价未登账的数据
        /// <summary>
        /// 获得出入库及调价未登账的数据
        /// </summary>
        /// <param name="MedStorageArr"></param>
        /// <param name="MedStorageChangArr"></param>
        /// <returns></returns>
        public long m_lngGetMedStorageUnAcct(out DataTable MedStorageArr, out DataTable MedStorageChangArr)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStorageUnAcct(out MedStorageArr, out MedStorageChangArr);
            return lngRes;
        }
        #endregion


        #region 根据单号ID获得单号明细
        /// <summary>
        /// 根据单号ID获得单号明细
        /// </summary>
        /// <param name="command">1,出入库ID。2，调价ID</param>
        /// <param name="strID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetDeById(int command, string strID, out DataTable dtbResult)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageFinSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetDeById(command, strID, out dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #endregion
    }
}
