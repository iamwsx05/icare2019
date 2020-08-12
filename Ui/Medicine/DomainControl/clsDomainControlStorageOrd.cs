using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 出入库库的控制逻辑层
    /// </summary>
    public class clsDomainControlStorageOrd : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        public clsDomainControlStorageOrd()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 保存记录单据　欧阳孔伟　2004-05-26]
        /// <summary>
        /// 保存记录表
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoSaveOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoSaveOrd(p_objOrd);

            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 保存明细数据　欧阳孔伟　2004-05-26
        /// <summary>
        /// 保存明细表
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoSaveOrdDe(clsStorageOrdDe_VO p_objOrdDe)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoSaveOrdDe(p_objOrdDe);

            //			lngDoSaveOrdDe(p_objOrdDe);

            return lngRes;
        }
        #endregion

        #region 修改记录表 欧阳孔伟
        /// <summary>
        /// 修改记录单
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrdByID(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 修改明细　欧阳孔伟　2004-05-26
        /// <summary>
        /// 修改明细表
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOrdDe(clsStorageOrdDe_VO p_objOrdDe)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrdDeByDeID(p_objOrdDe);

            return lngRes;
        }
        #endregion

        #region 删除单据　欧阳孔伟　2004-05-17
        /// <summary>
        /// 删除记录单记录
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoDeleteOrdByID(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteOrdByID(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 删除明细表数据　欧阳孔伟　2004-05-26
        /// <summary>
        /// 按明细ID删除数据
        /// </summary>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        public long m_lngDoDeleteOrdDeByDeID(string p_strOrdDeID)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteOrdDeByDeID(p_strOrdDeID);

            return lngRes;
        }
        #endregion

        #region 审核单据　欧阳孔伟　2004-05-17
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public long m_lngAduitOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAuditOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 审核单据，更改库存　欧阳孔伟　2004-06-08
        /// <summary>
        /// 审核单据，更改库存
        /// </summary>
        /// <param name="p_strStorageOrdID">单据号</param>
        /// <param name="p_strStorageOrdTypeID">单据类型号</param>
        /// <param name="p_intFlag">返回标识，1：成功  0：失败  -1：异常</param>
        /// <returns></returns>
        public long m_lngAuditOrdToChangeStorage(string p_strStorageOrdID, string p_strStorageOrdTypeID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            //clsStorageOrdSvc objStoSvc = (clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAuditOrdToChangeStorage(p_strStorageOrdID, p_strStorageOrdTypeID, out p_intFlag);
            //objStoSvc.Dispose();
            return lngRes;

        }
        #endregion

        #region 查找所有单据记录　欧阳孔伟　2004-05-17
        /// <summary>
        /// 模糊查询单据记录信息
        /// </summary>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindOrdByAny(string p_strSQL, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;

            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按单据号查找单据记录　欧阳孔伟　2004-05-17
        /// <summary>
        /// 按单据号查找单据记录信息
        /// </summary>
        /// <param name="p_strOrdID">单据号</param>
        /// <param name="p_strOrdType">单据类型</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindOrdByID(string p_strOrdID, string p_strOrdType, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByID(p_strOrdID, p_strOrdType, out p_objResult);
            return lngRes;

        }
        #endregion

        #region 按库房查找单据记录
        /// <summary>
        /// 按库房查找单据记录信息
        /// </summary>
        /// <param name="p_strID">库房代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindOrdByStorage(string p_strID, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByStorage(p_strID, out p_objResult);
            return lngRes;

        }
        #endregion

        #region 按帐务期查找单据记录
        /// <summary>
        /// 按帐务期查找单据记录信息
        /// </summary>
        /// <param name="p_strID">单据号</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindOrdByPeriod(string p_strID, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdByPeriod(p_strID, out p_objResult);
            return lngRes;

        }
        #endregion

        #region 模糊查询单据明细信息　欧阳孔伟　2004-05-25
        /// <summary>
        /// 模糊查找单据明细
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngFindOrdDeByAny(string p_strSQL, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdDeByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按ID查询单据明细信息　欧阳孔伟　2004-05-25
        /// <summary>
        /// 将单据记录号查找单据明细
        /// </summary>
        /// <param name="p_strOrdID">记录单ID</param>
        /// <param name="p_strOrdType">单据类型</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngFindOrdDeByOrdID(string p_strOrdID, string p_strOrdType, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdDeByOrdID(p_strOrdID, p_strOrdType, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 模糊查询单据明细信息　欧阳孔伟　2004-05-25
        /// <summary>
        /// 模糊查找单据明细
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngFindOrdDeByOrdMedicine(string p_strID, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdDeByMedicine(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region　获得明细当前ID　欧阳孔伟　2004-05-24
        /// <summary>
        /// 获取明细单当前最大ID
        /// </summary>
        /// <param name="p_strOrdDeID">返回值为当前最大ID</param>
        /// <returns></returns>
        public long m_lngGetOrdDeID(out string p_strOrdDeID)
        {
            long lngRes = 0;
            p_strOrdDeID = null;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetOrdDeID(10, out p_strOrdDeID);

            long lngDeID = long.Parse(p_strOrdDeID);

            if (lngDeID < 1)
            {
                lngDeID = 1;
                p_strOrdDeID = lngDeID.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region 获取单据记录号　欧阳孔伟　2004-05-24
        /// <summary>
        /// 获取记录单当前最大ID
        /// </summary>
        /// <param name="p_strOrdType">单据类型</param>
        /// <param name="p_strOrdID">输出参数为获得的当前最大ID</param>
        /// <returns></returns>
        public long m_lngGetOrdID(string p_strOrdType, out string p_strOrdID)
        {
            long lngRes = 0;

            p_strOrdID = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetNewOrdID(p_strOrdType, out p_strOrdID);

            if (p_strOrdID == "")
            {
                p_strOrdID = "000000000";
            }

            long lngOrdID = long.Parse(p_strOrdID);


            if (lngOrdID < 1)
            {
                lngOrdID = 1;
                p_strOrdID = lngOrdID.ToString("0000000000");
            }
            else
            {
                lngOrdID += 1;
                p_strOrdID = lngOrdID.ToString("0000000000");
            }


            return lngRes;
        }
        #endregion

        #region 检测单据记录号是否已存在数据库中　欧阳孔伟　2004-05-25
        /// <summary>
        /// 检测单据号是否已存在表中，1为不存在，0为存在。
        /// </summary>
        /// <param name="p_strOrdID"></param>
        /// <returns></returns>
        public long m_lngCheckOrdID(string p_strOrdID, string p_strOrdTypeID, string p_strStorageID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngCheckOrdID(p_strOrdID, p_strOrdTypeID, p_strStorageID);

            return lngRes;
        }
        #endregion

        #region 新系统的方法

        #region 获取出库单据类型
        /// <summary>
        /// 获取出库单据类型
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strSign">1-入库，2-出库</param>
        /// <returns></returns>
        public long m_lngGetOutOrdType(out DataTable dt, string strSign)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetOutOrdType(out dt, strSign);

            return lngRes;
        }
        #endregion

        #region 导数据
        /// <summary>
        /// 导数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strINordID">入库单ID</param>
        /// <param name="strOrdTypeID">出库单类型</param>
        /// <param name="strPERIODID">出库单的财务期</param>
        /// <param name="strOldDocID">旧的单据号</param>
        /// <param name="strNewDOCID">新的单据号</param>
        /// <param name="strCREATOR">创建人</param>
        /// <param name="strSIGN">出入标志 1－入库 2－出库</param>
        /// <returns></returns>
        public long m_lngGuideRope(string strINordID, string strOrdTypeID, string strPERIODID, string strInDOCID, string strOutDOCID, string strCREATOR, string strSIGN)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGuideRope(strINordID, strOrdTypeID, strPERIODID, strInDOCID, strOutDOCID, strCREATOR, strSIGN);

            return lngRes;
        }
        #endregion

        #region 保存记录单据(入库）　
        /// <summary>
        /// 保存记录单据(入库）
        /// </summary>
        /// <param name="p_objResult">输入数据</param>
        /// <param name="listItem">明细数据</param>
        /// <param name="newID">返回单据ID</param>
        /// <param name="isCheck">是否检查单据号是否存在，trun 检查，false-不检查</param>
        /// <param name="signint">1－入库 2－出库3-退库4-退库</param>
        /// <returns>返回-2表示单据号已经被占用</returns>

        public long m_lngInsertMetStorageOrd(clsMedStorageOrd_VO p_objResult, clsMedStorageOrdDe_VO[] listItem, out string newID, bool isCheck, int signint)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrd(p_objResult, listItem, out newID, isCheck, signint);

            return lngRes;
        }
        #endregion

        #region 修改入库单的行号　
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngModifyRowNO(DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngModifyRowNO(dt);
            return lngRes;
        }
        #endregion

        #region 刷新数据　
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="OrdDe_VO"></param>
        /// <param name="OrderID">null-当前入库单为新增</param>
        /// <returns></returns>
        public long m_lonResetData(ref clsMedStorageOrdDe_VO[] OrdDe_VO, string OrderID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lonResetData(ref OrdDe_VO, OrderID);

            return lngRes;
        }
        #endregion


        #region 判定财务期是否已经被盘点并审核　
        /// <summary>
        /// 判定财务期是否已经被盘点并审核
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPeriod">财务期</param>
        /// <param name="IsAppl">true-已经被盘点并审核</param>
        /// <returns></returns>
        public long m_lngEstimatePeriod(string strPeriod, out bool IsAppl)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngEstimatePeriod(strPeriod, out IsAppl);

            return lngRes;
        }
        #endregion

        #region 根据入库ID查找数据
        /// <summary>
        /// 根据入库ID查找数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ordTypeID">单据类型ID,如果为""则以标专状态来查找相应的单据信息</param>
        /// <param name="oreType">返回单据信息</param>
        /// <param name="status_int">药房与药库数据同步标志0-药房申请药品使用单据，1-是药房退药使用单据，-1-不是同步单据</param>
        /// <returns></returns>
        public long m_lngFindOrdTypeNameByID(string ordTypeID, out clsStorageOrdType_VO oreType, string status_int)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindOrdTypeNameByID(ordTypeID, out oreType, status_int);
            return lngRes;
        }
        #endregion

        #region 插入明细数据(入库）　
        /// <summary>
        /// 插入明细数据(入库）
        /// </summary>
        /// <param name="p_objResult"></param>
        /// <param name="tolMoney"></param>
        /// <returns></returns>
        public long m_lngInsertMetStorageOrdDe(clsMedStorageOrdDe_VO p_objResult, out double tolMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrdDe(p_objResult, out tolMoney);
            return lngRes;
        }
        #endregion

        #region 返回所有的数据单数据
        /// <summary>
        /// 返回所有的数据单数据
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <param name="priod">财务期</param>
        /// <param name="ordTypeID">出入库类型ID</param>
        /// <returns></returns>
        public long m_lngGetStorageOrdList(out clsMedStorageOrd_VO[] p_objOrd, string priod, string ordTypeID)
        {
            p_objOrd = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageOrdList(out p_objOrd, priod, ordTypeID);
            return lngRes;
        }
        #endregion

        #region  根据单号获得明细数据
        /// <summary>
        ///  根据单号获得明细数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="StorageID">单号</param>
        /// <param name="p_objResultArr">返回值</param>
        /// <returns></returns>
        public long m_lngGetMedStorageOrdDe(string StorageID, out clsMedStorageOrdDe_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStorageOrdDe(StorageID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据单号ID删除入库单
        /// <summary>
        /// 根据单号ID删除入库单
        /// </summary>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        public long m_lngDeleStorageOrd(string p_strOrdDeID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleStorageOrd(p_strOrdDeID);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 删除出入库单明细
        /// <summary>
        /// 删除出入库单明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdDeID">明细ＩＤ</param>
        /// <returns></returns>
        public long m_lngDeleteOrdDeBy(string p_strOrdDeID, out double tolMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteOrdDeBy(p_strOrdDeID, out tolMoney);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }

        #endregion

        #region 修改入库单表及明细
        /// <summary>
        /// 修改入库单表及明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdID">入库单ID</param>
        /// <param name="p_objOrdDe">明细数据</param>
        /// <param name="tolMoney">新增明细金额</param>
        /// <returns></returns>
        public long m_lngDoUpdateOrdAndDe(string p_objOrdID, clsMedStorageOrdDe_VO p_objOrdDe, out double tolmoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrdDe(p_objOrdID, p_objOrdDe, out tolmoney);

            return lngRes;
        }
        #endregion

        #region 修改入库单
        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOrd(clsMedStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOrd(p_objOrd);

            return lngRes;

        }
        #endregion

        #region 审核单据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedOrdID">入库单ID</param>
        /// <param name="storageID">所属仓库ID</param>
        /// <param name="Empman">审核人</param>
        /// <param name="EmpDate">审核日期</param>
        /// <param name="p_objOrdDe">入库单明细数据</param>
        /// <param name="intDept">部门类型，1－院内，0－院外</param>
        /// <returns></returns>
        public long m_lngEmpOrd(string MedOrdID, string storageID, string Empman, string EmpDate, clsMedStorageOrdDe_VO[] p_objOrdDe, int intDept)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngEmpOrd(MedOrdID, storageID, Empman, EmpDate, p_objOrdDe, intDept);
            return lngRes;
        }
        #endregion

        #region 获取药品的参考价
        /// <summary>
        /// 获取药品的参考价
        /// </summary>
        /// <param name="medID">药品ID</param>
        /// <param name="ConsoltPrice">返回药品参考价</param>
        /// <returns></returns>
        public long m_lngGetConsoltPrice(string medID, out string ConsoltPrice, out string ConsoltUnit, out string ConsoltOrdPrice, out string ConsoltORDERPKGQTY, out string ConsoltAIMUNITPRICE, out string ConsoltLIMITUNITPRICE)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetConsoltPrice(medID, out ConsoltPrice, out ConsoltUnit, out ConsoltOrdPrice, out ConsoltORDERPKGQTY, out ConsoltAIMUNITPRICE, out ConsoltLIMITUNITPRICE);
            return lngRes;
        }
        #endregion

        #region 获得所有的门诊药房
        public long m_lngGetStorage(out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorage(out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 审核单据，更改库存

        public long m_lngOrdToChangeStorage(string p_strStorageID, string p_strMeDID, double TolNumber)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeStorage(p_strStorageID, p_strMeDID, TolNumber);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 审核单据，更改库存明细表

        public long m_lngOrdToChangeDetail(string storageID, clsMedStorageOrdDe_VO p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeDetail(storageID, p_objResultArr);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;
        }
        #endregion

        #region 根据仓库查询库存明细表

        public long m_lngGetDeTail(string storageID, out System.Data.DataTable p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetDeTail(storageID, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据财务期获得所有出库单数据
        /// <summary>
        /// 根据财务期获得所有出库单数据
        /// </summary>
        /// <param name="p_objResultArr">返回数据</param>
        /// <param name="priod">财务期ID</param>
        /// <returns></returns>
        public long m_lngGetStorageOrdOut(out clsMedStorageOrd_VO[] p_objResultArr, string priod, string strordType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageOrdOut(out p_objResultArr, priod, strordType);

            return lngRes;
        }
        #endregion

        #region 获取某仓库的最大单据号
        /// <summary>
        /// 获取某仓库的最大单据号
        /// </summary>
        /// <param name="p_strMaxDoc"></param>
        /// <param name="strDate"></param>
        /// <param name="strSIGN"></param>
        /// <param name="STORAGEID">仓库ID</param>
        /// <returns></returns>
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strDate, string strSIGN, string STORAGEID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxDoc(out p_strMaxDoc, strDate, strSIGN, STORAGEID);

            return lngRes;
        }
        #endregion

        #region 保存记录单据(出库）　
        /// <summary>
        /// 保存记录单据(出库）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResult">输入单据数据</param>
        /// <param name="p_objResultDe">输入明细数据</param>
        /// <param name="newID">输出单号ID</param>
        /// <param name="isCheck">是否检查单据号是否存在，trun 检查，false-不检查</param>
        /// <param name="signint">1－入库 2－出库3-退库4-退库</param>
        /// <returns>返回-2表示单据号已经被占用</returns>
        public long m_lngInsertMetStorageOrdOut(clsMedStorageOrd_VO p_objOrd, clsMedStorageOrdDe_VO[] p_objResultDe, out string newID, bool isCheck, int signint)
        {
            long lngRes = 0;


            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrdOut(p_objOrd, p_objResultDe, out newID, isCheck, signint);
            return lngRes;
        }
        #endregion

        #region 保存明细数据(出库）
        /// <summary>
        /// 保存明细表
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngInsertMetStorageOrdDeOut(clsMedStorageOrdDe_VO p_objOrdDe, out double tolMoney)
        {
            long lngRes = 0;



            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertMetStorageOrdDeOut(p_objOrdDe, out tolMoney);
            //			lngDoSaveOrdDe(p_objOrdDe);

            return lngRes;
        }
        #endregion

        #region 根据出库单ID获得所有的出库明细
        /// <summary>
        /// 根据出库单ID获得所有的出库明细
        /// </summary>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngGetMedStorageOrdDeOut(string storageID, out clsMedStorageOrdDe_VO[] p_objOrdDe)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedStorageOrdDeOut(storageID, out p_objOrdDe);
            //			lngDoSaveOrdDe(p_objOrdDe);


            return lngRes;
        }
        #endregion

        #region 修改出库单表
        /// <summary>
        /// 修改出库单表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOutOrd(clsMedStorageOrd_VO p_objOrdDe)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOutOrd(p_objOrdDe);
            return lngRes;

        }
        #endregion

        #region 修改明细表
        /// <summary>
        /// 修改明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        public long m_lngDoUpdateOutOrdDe(clsMedStorageOrdDe_VO p_objOrdDe, string strTotailMoney, string strOrdID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateOutOrdDe(p_objOrdDe, strTotailMoney, strOrdID);
            return lngRes;
        }
        #endregion

        #region 审核入库单据，更改库存明细表
        /// <summary>
        /// 审核入库单据，更改库存明细表
        /// </summary>

        /// <returns></returns>
        public long m_lngOrdToChangeDetailOut(string SysNO, double OutNumber)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeDetailOut(SysNO, OutNumber);
            //			lngDoSaveOrd(p_objOrd);


            return lngRes;

        }
        #endregion

        #region 审核入库单据，更改库存
        /// <summary>
        /// 审核入库单据，更改库存
        /// </summary>
        /// <returns></returns>
        public long m_lngOrdToChangeStorageOut(string p_strStorageID, string p_strMeDID, double TolNumber)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngOrdToChangeStorageOut(p_strStorageID, p_strMeDID, TolNumber);
            //			lngDoSaveOrd(p_objOrd);

            return lngRes;

        }
        #endregion

        #region 审核出库单据
        /// <summary>
        /// 审核出库单据
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objResultDeArr"></param>
        /// <param name="flag">true,自动生成药房入库数据</param> 
        /// <param name="status">是否充许负库存0-不充许，1-允许</param>
        /// <returns>1,审核成功;-1,失败;-2,还没有设置该药品的包装量;-3,数据类型不正确</returns>
        public long m_lngAduitOrdOut(clsMedStorageOrd_VO p_objResultArr, clsMedStorageOrdDe_VO[] p_objResultDeArr, bool flag, int status)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAduitOrdOut(p_objResultArr, p_objResultDeArr, flag, status);
            return lngRes;
        }
        #endregion

        #region 获取所有的药房
        /// <summary>
        /// 获取所有的药房
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lonGetStore(out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageOrdSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lonGetStore(out dt);
            return lngRes;
        }
        #endregion

        #endregion


    }
}
