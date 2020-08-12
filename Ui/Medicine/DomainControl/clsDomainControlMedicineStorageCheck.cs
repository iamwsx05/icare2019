using System;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlMedicineStorageCheck 的摘要说明。
    /// </summary>
    public class clsDomainControlMedicineStorageCheck : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlMedicineStorageCheck()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 获得所有的仓库信息
        /// <summary>
        /// 获得所有的仓库信息
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetStorage(string p_strStorageFlag, out DataTable dtbResult)
        {
            //long lngRes = 0;
            //dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            //lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArr( out dtbResult);
            //return lngRes;
            long lngRes = 0;
    //        com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
    //(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageArr2(p_strStorageFlag, out dtbResult);

            return lngRes;

        }
        #endregion

        #region 获取仓库下面所有盘点单据
        /// <summary>
        /// 获取仓库下面所有盘点单据
        /// </summary>
        /// <param name="dtbResult"></param>		
        /// <returns></returns>
        public long m_lngGetCheckBill(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckByAny(out dtbResult);
            return lngRes;
        }
        #endregion

        #region 获取盘点单下面的明细
        public long m_lngGetCheckDetail(string strStorageID, string strStorageFlag, string strCheckBillID, out System.Data.DataTable dtbResult, string str1, bool isEm)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageCheckDetail(strCheckBillID, out dtbResult, str1, isEm);
            return lngRes;
        }
        #endregion 获取盘点单下面的明细

        #region 获取当前库存明细数据
        /// <summary>
        /// 获取当前库存明细数据
        /// </summary>
        /// <param name="strStorageID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="strFlag"></param>
        /// <returns></returns>
        public long m_lngGetStorageCheckDetail(string strStorageID, out System.Data.DataTable dtbResult, string strFlag, clsHISMedType_VO[] medType, clsMedicinePrepType_VO[] PrepType, clsMedicineType_VO[] MedicineType, bool isShowZero, bool isShowStop)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageCheckDetail(strStorageID, out dtbResult, strFlag, medType, PrepType, MedicineType, isShowZero, isShowStop);
            return lngRes;
        }
        #endregion 获取盘点单下面的明细

        #region 获得所有的药品剂型
        /// <summary>
        /// 获得所有的药品剂型
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedinicePrepType(out DataTable dtbResult, out DataTable dtbResult1, out DataTable dtbResult2)
        {
            long lngRes = 0;
            dtbResult = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedinicePrepType(out dtbResult, out dtbResult1, out dtbResult2);
            return lngRes;
        }
        #endregion

        #region 新增盘点记录单
        /// <summary>
        /// 新增盘点记录单
        /// </summary>
        /// <param name="p_objItem">盘点记录单数据</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageCheck(ref clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageCheck(p_objItem);
            return lngRes;

        }
        #endregion 新增盘点记录单

        #region 根据盘点单号删除盘点单
        /// <summary>
        /// 根据盘点单号删除盘点单
        /// </summary>
        /// <param name="strCheckBillID">盘点单号</param>
        /// <returns></returns>
        public long m_lngDelCheckBill(string strCheckBillID)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageCheck(strCheckBillID);
            return lngRes;

        }
        #endregion 根据盘点单号删除盘点单

        #region 盘点单审核
        public long m_lngAuditCheckBill(clsStorageCheckDetail_VO[] p_objItem, string strAuditorID, string strAuditDate, string p_strStorageFlag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAuditCheckBill(p_objItem, strAuditorID, strAuditDate, p_strStorageFlag);
            return lngRes;
        }
        #endregion

        #region 合并盘点单
        /// <summary>
        /// 合并盘点单
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="p_objItem"></param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        public long m_lngUnionData(System.Collections.Generic.List<string> objList, clsStorageCheck_VO p_objItem, out DataTable dtCheckOut)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngUnionData(objList, p_objItem, out dtCheckOut);
            return lngRes;
        }
        #endregion

        #region 新增盘点明细
        public long m_lngDoAddNewStorageCheckDetail(clsStorageCheckDetail_VO[] p_objItem, string strRemark)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageChekDetail(p_objItem, strRemark);
            return lngRes;
        }
        #endregion 新增盘点明细

        #region 获取账务日期
        public long m_lngGetPeriod(out System.Data.DataTable dt)
        {
            long lngRes = 0;
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriod(out dt);
            return lngRes;
        }
        #endregion 获取账务日期
    }
}
