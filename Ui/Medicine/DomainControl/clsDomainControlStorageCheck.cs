using System;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll

using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 盘点管理
    /// Create by kong 2004-06-14
    /// </summary>
    public class clsDomainControlStorageCheck : com.digitalwave.GUI_Base.clsDomainController_Base   //GUI_Base.dll
    {
        #region clsDomainControlStorageCheck
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDomainControlStorageCheck()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 新增盘点记录单
        /// <summary>
        /// 新增盘点记录单
        /// </summary>
        /// <param name="p_objItem">盘点记录单数据</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageCheck(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageCheck(p_objItem);

            return lngRes;

        }
        #endregion

        #region 修改盘点记录单
        /// <summary>
        /// 修改盘点记录单
        /// </summary>
        /// <param name="p_objItem">盘点记录单数据</param>
        /// <returns></returns>
        public long m_lngDoUpdateStorageCheck(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStorageCheck(p_objItem);

            return lngRes;

        }
        #endregion

        #region 删除盘点记录单
        /// <summary>
        /// 删除盘点记录单
        /// </summary>
        /// <param name="p_strID">盘点记录单号</param>
        /// <returns></returns>
        public long m_lngDoDeleteStorageCheck(string p_strID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageCheck(p_strID);

            return lngRes;

        }
        #endregion

        #region 新增盘点明细单
        /// <summary>
        /// 新增盘点明细单
        /// </summary>
        /// <param name="p_objItem">盘点明细单数据</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageChekDetail(clsStorageCheckDetail_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageChekDetail(p_objItem);

            return lngRes;

        }
        #endregion

        #region 修改盘点明细单
        /// <summary>
        /// 修改盘点明细单
        /// </summary>
        /// <param name="p_objItem">盘点明细单数据</param>
        /// <returns></returns>
        public long m_lngDoUpdateStorageChekDetail(clsStorageCheckDetail_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStorageChekDetail(p_objItem);

            return lngRes;

        }
        #endregion

        #region 删除盘点明细单
        /// <summary>
        /// 删除盘点明细单
        /// </summary>
        /// <param name="p_strID">盘点明细单号</param>
        /// <returns></returns>
        public long m_lngDoDeleteStorageChekDetail(string p_strID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageChekDetail(p_strID);

            return lngRes;

        }
        #endregion

        #region 审核盘点单
        /// <summary>
        /// 审核盘点单
        /// </summary>
        /// <param name="p_objItem">盘点记录单数据</param>
        /// <returns></returns>
        public long m_lngAduit(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAduit(p_objItem);

            return lngRes;
        }
        #endregion

        #region 审核盘点单后更改库存
        /// <summary>
        /// 审核后更改库存
        /// </summary>
        /// <param name="p_strID">盘点单号</param>
        /// <param name="p_intFlag">返回标识，1：成功，0：失败，-1：异常</param>
        /// <returns></returns>
        public long m_lngChangeStorageAfterAduit(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngChangeStorageAfterAduit(p_strID, out p_intFlag);

            return lngRes;
        }
        #endregion

        #region 模糊查找盘点记录单
        /// <summary>
        /// 模糊查找盘点记录单
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByAny(string p_strSQL, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByAny(p_strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单号查找盘点记录单
        /// <summary>
        /// 按单号查找盘点记录单
        /// </summary>
        /// <param name="p_strID">盘点记录单号</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByID(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByID(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按仓库查找盘点记录单
        /// <summary>
        /// 按仓库查找盘点记录单
        /// </summary>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByStorage(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByStorage(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单据类型查找盘点记录单
        /// <summary>
        /// 按单据类型查找盘点记录单
        /// </summary>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByType(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByType(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按帐务期查找盘点记录单
        /// <summary>
        /// 按帐务期查找盘点记录单
        /// </summary>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByPeriod(string p_strID, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByPeriod(p_strID, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按时间段查找盘点记录单
        /// <summary>
        /// 按时间段查找盘点记录单
        /// </summary>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckByDate(string p_strStartDate, string p_strEndDate, out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByDate(p_strStartDate, p_strEndDate, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 查找所有的盘点记录单
        /// <summary>
        /// 查找所有的盘点记录单
        /// </summary>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindAllStorageCheck(out clsStorageCheck_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheck_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllStorageCheck(out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 模糊查找盘点明细单
        /// <summary>
        /// 模糊查找盘点明细单
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckDetailByAny(string p_strSQL, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetailByAny(p_strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按盘点单号查找盘点明细单
        /// <summary>
        /// 按盘点单号查找盘点明细单
        /// </summary>
        /// <param name="p_strID">盘点记录单号</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckDetailByID(string p_strID, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetailByCheckID(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按药品查找盘点明细单
        /// <summary>
        /// 按药品查找盘点明细单
        /// </summary>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStorageCheckDetailByMedicine(string p_strID, out clsStorageCheckDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageCheckDetail_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetailByMedicine(p_strID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 获得当前最大的盘点记录单号
        /// <summary>
        ///  获得当前最大的盘点记录单号
        /// </summary>
        /// <param name="p_strResult">输出数据</param>
        /// <returns></returns>
        public long m_lngGetMaxCheckID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxCheckID(out p_strResult);

            return lngRes;
        }
        #endregion

        #region 获得当前最大的盘点明细单号
        /// <summary>
        /// 获得当前最大的盘点明细单号
        /// </summary>
        /// <param name="p_strResult">输出数据</param>
        /// <returns></returns>
        public long m_lngGetMaxDetailID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxDetailID(out p_strResult);

            return lngRes;
        }
        #endregion

        #region 新系统的方法
        #region 获得所有的盘点数据
        /// <summary>
        /// 获得所有的盘点数据
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetStorageDeTail(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageDeTail(out dtbResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 获得所有的仓库信息
        /// <summary>
        /// 获得所有的仓库信息
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetStorage(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetAllStorage(out dtbResult);
            return lngRes;
        }
        #endregion

        #region 获得所有的药品剂型
        /// <summary>
        /// 获得所有的药品剂型
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedinicePrepType(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            DataTable dtbResult1 = null;
            DataTable dtbResult2 = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedinicePrepType(out dtbResult, out dtbResult1, out dtbResult2);
            return lngRes;
        }
        #endregion

        #region 自动生成出入库单
        /// <summary>
        /// 自动生成出入库单
        /// </summary>
        /// <param name="dtStorCheckData"></param>
        /// <returns></returns>
        public long m_lngGetAutoGreat(DataTable dtStorCheckData)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAutoGreat(dtStorCheckData);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 判断是否设置有盘点出库或入库的单据类型
        /// <summary>
        /// 判断是否设置有盘点出库或入库的单据类型
        /// </summary>
        /// <param name="typeName">出入库类型名称</param>
        /// <param name="typeID">返回单据类型ID</param>
        /// <returns>2有，3则是该类型在更新库类别中不存在</returns>
        public long m_lngisCheckType(string typeName, out string typeID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngisCheckType(typeName, out typeID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion



    }
}
