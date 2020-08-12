using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 采购计划单
    /// kong 2004-05-27
    /// </summary>
    public class clsDomainControlStockMedAppl : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlStockMedAppl()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 保存采购记录单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 保存采购记录单
        /// </summary>
        /// <param name="p_objStockMedAppl"></param>
        /// <returns></returns>
        public long m_lngDoSaveStockMedAppl(clsStockMedApplication_VO p_objStockMedAppl)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStockAppl(p_objStockMedAppl);

            return lngRes;
        }
        #endregion

        #region 保存采购明细 欧阳孔伟 2004-05-31
        /// <summary>
        /// 保存采购明细
        /// </summary>
        /// <param name="p_objStockMedApplDetail"></param>
        /// <returns></returns>
        public long m_lngDoSaveStockMedApplDetail(clsStockMedApplDetail_VO p_objStockMedApplDetail)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStockApplDe(p_objStockMedApplDetail);

            return lngRes;
        }
        #endregion

        #region 更改记录单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 更改记录单
        /// </summary>
        /// <param name="p_objStockMedAppl"></param>
        /// <returns></returns>
        public long m_lngDoUpdateStockMedAppl(clsStockMedApplication_VO p_objStockMedAppl)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStockApplByID(p_objStockMedAppl);

            return lngRes;
        }
        #endregion

        #region 更改明细单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 更改明细单
        /// </summary>
        /// <param name="p_objStockMedApplDetail"></param>
        /// <returns></returns>
        public long m_lngDoUpdateStockMedApplDetail(clsStockMedApplDetail_VO p_objStockMedApplDetail)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStockApplDeByID(p_objStockMedApplDetail);

            return lngRes;
        }
        #endregion

        #region 删除记录单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 删除记录单
        /// </summary>
        /// <param name="p_strId">记录单ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteStockMedApplID(string p_strId)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStockApplByID(p_strId);

            return lngRes;
        }
        #endregion

        #region 删除明细单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 删除明细单
        /// </summary>
        /// <param name="p_strID">明细单ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteStockMedApplDetailByID(string p_strID)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStockApplDeByID(p_strID);

            return lngRes;
        }
        #endregion

        #region 模糊查询记录单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 模糊查询记录单
        /// </summary>
        /// <param name="p_strSQL">SQL脚本语句</param>
        /// <param name="p_objResult">输出值</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByAny(string p_strSQL, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以ID查找记录单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 以ID查找记录单信息
        /// </summary>
        /// <param name="p_strId">ID号</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByID(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByApplID(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 根据采购单号查询采购记录单
        /// <summary>
        /// 以采购单号查询采购记录单
        /// </summary>
        /// <param name="p_strNo">采购单号</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByNo(string p_strNo, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByApplNo(p_strNo, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以库房查询采购记录单
        /// <summary>
        /// 以库房查询采购记录单
        /// </summary>
        /// <param name="p_strId">库房代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByStorage(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByStorage(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以供应商查询采购记录单
        /// <summary>
        /// 以供应商查询采购记录单
        /// </summary>
        /// <param name="p_strId">供应商代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByVendor(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByVendor(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以部门查询采购记录单
        /// <summary>
        /// 以部门查询采购记录单
        /// </summary>
        /// <param name="p_strId">部门代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByDept(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByDept(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以采购时间段查询采购记录单
        /// <summary>
        /// 以采购时间段查询采购记录单
        /// </summary>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByDate(string p_strStartDate, string p_strEndDate, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByDate(p_strStartDate, p_strEndDate, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 模糊查找明细单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 模糊查找明细单
        /// </summary>
        /// <param name="p_strSQL">SQL脚本语句</param>
        /// <param name="p_objResult">输出值</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplDeByAny(string p_strSQL, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplDeByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以采购单ID查找明细单 欧阳孔伟 2004-05-31
        /// <summary>
        /// 以采购单ID查找明细单
        /// </summary>
        /// <param name="p_strID">采购记录单ID</param>
        /// <param name="p_dtbResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplDeByID(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplDeByID(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 以药品查询采购明细单
        /// <summary>
        /// 以药品查询采购明细单
        /// </summary>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplDeByMedicine(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplDeByMedicine(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 获得最大记录单号 欧阳孔伟 2004-05-31
        /// <summary>
        /// 获得最大记录单号
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetMaxStockMedApplNo(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStockMedApplNo(out p_strResult);

            long lngNo = long.Parse(p_strResult);

            if (lngNo < 1)
            {
                lngNo = 1;
                p_strResult = lngNo.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region 获得最大记录单ID 欧阳孔伟 2004-05-31
        /// <summary>
        /// 获得最大记录单ID
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetMaxStockMedApplID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStockMedApplId(out p_strResult);

            long lngID = long.Parse(p_strResult);

            if (lngID < 1)
            {
                lngID = 1;
                p_strResult = lngID.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region 获得最大明细单号 欧阳孔伟 2004-05-31
        /// <summary>
        /// 获得最大明细单号
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetMaxStockMedApplDeId(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStockMedApplDeId(out p_strResult);

            long lngDeID = long.Parse(p_strResult);

            if (lngDeID < 1)
            {
                lngDeID = 1;
                p_strResult = lngDeID.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region 自动生成采购单明细 欧阳孔伟 2004-05-31
        /// <summary>
        /// 自动生成采购单明细
        /// </summary>
        /// <param name="p_strID">库房ID　</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngDoAutoCalcStockMedApplDe(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAutoCalcStockMedAppl(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 新系统的方法

        #region 获取申请单信息
        public long m_lngGetApplCation(out DataTable dtbResult, string date, string p_strStorageID)
        {
            long lngRes = 0;
            dtbResult = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetApplCation(out dtbResult, date, p_strStorageID);
            return lngRes;

        }
        #endregion

        #region 获取申请部门
        public long m_lngGetDept(out clsT_BSE_DEPTDESC_VO[] DEPTDESC_VO)
        {
            long lngRes = 0;
            DEPTDESC_VO = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetDept(out DEPTDESC_VO);
            return lngRes;
        }
        #endregion

        #region 获取供应商
        public long m_lngGetVendor(out clsVendor_VO[] VendorVO)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetVendor(out VendorVO);

            return lngRes;
        }
        #endregion

        #region 获取厂家
        public long m_lngGetManufacturer(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetManufacturer(out dtbResult);

            return lngRes;
        }
        #endregion

        #region 获取最大的单据号
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strdate)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxDoc(out p_strMaxDoc, strdate);
            return lngRes;
        }
        #endregion

        #region 采购完成
        /// <summary>
        /// 采购完成
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objResultDeArr"></param>
        /// <param name="blnAuto"></param>
        /// <returns></returns>
        public long m_lngAutoCompleteApp(clsMedStorageOrd_VO p_objResultArr, clsMedStorageOrdDe_VO[] p_objResultDeArr, bool blnAuto)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAutoCompleteApp(p_objResultArr, p_objResultDeArr, blnAuto);
            return lngRes;
        }
        #endregion

        #region 根据单号ID获得单据明细
        public long m_lngGetApplDeByID(string strID, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetApplDeByID(strID, out dtbResult);
            return lngRes;
        }
        #endregion



        #region  插入明细
        public long m_lngInsertDe(DataTable newRow)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertDe(newRow);

            return lngRes;
        }
        #endregion

        #region  修改单据
        public long m_lngModify(DataTable newRow, DataTable newRowDe)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngModify(newRow, newRowDe);

            return lngRes;
        }
        #endregion

        #region  保存单据
        public long m_lngSaveData(DataTable newRow, DataTable newTableDe, out string p_strNewID)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngSaveData(newRow, newTableDe, out p_strNewID);

            return lngRes;
        }
        #endregion

        #region 修改单据
        /// <summary>
        /// 修改单据
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public long m_lngModifyData(DataTable newRow)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngModifyData(newRow);


            return lngRes;
        }
        #endregion

        #region 根据单号ID获得单据明细
        public long m_lngGetVen(string strMedID, out string venName)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetVen(strMedID, out venName);
            return lngRes;
        }
        #endregion

        #region 删除记录单
        /// <summary>
        /// 删除记录单
        /// </summary>
        /// <param name="p_strId">记录单ID</param>
        /// <returns></returns>
        public long m_lngDoDelStockApplByID(string p_strId)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));


            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDelStockApplByID(p_strId);


            return lngRes;
        }
        #endregion

        #region 删除明细单
        /// <summary>
        /// 删除明细单
        /// </summary>
        /// <param name="p_strID">明细单ID</param>
        /// <returns></returns>
        public long m_lngDoDelStockMedApplDetailByID(string p_strID)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStockApplDeByID(p_strID);


            return lngRes;
        }
        #endregion

        #region 获取符合采购条件的数据
        public long m_lngGetData(out DataTable objDataTable)
        {
            long lngRes = 0;
            objDataTable = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetData(out objDataTable);

            return lngRes;
        }
        #endregion
        #endregion
    }
}
