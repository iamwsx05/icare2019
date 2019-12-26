using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_CommonFind : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public clsDcl_CommonFind()
        {
        }

        weCare.Proxy.ProxyIP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP01();
            }
        }
        #endregion              

        #region 根据病区ID获取该病区床位信息
        /// <summary>
        /// 根据病区ID获取该病区床位信息
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="status"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetBedinfo(string AreaID, int status, out DataTable dt)
        {
            return proxy.Service.m_lngGetBedinfo(AreaID, status, out dt);
        }
        #endregion

        #region 根据住院号或诊疗卡号获取当前在院病人信息
        /// <summary>
        /// 根据住院号或诊疗卡号获取当前在院病人信息
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type)
        {
            return proxy.Service.m_lngGetPatientinfoByZyh(no, out dt, type);
        }
        #endregion

        #region 通用查找窗口用
        /// <summary>
        /// 通用查找窗口用
        /// </summary>
        /// <param name="SqlWhereZY"></param>
        /// <param name="Status">0 全部 1 在院 2 出院</param>
        /// <param name="IsIncludeMZ"></param>
        /// <param name="SqlWhereMZ"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientinfo(string SqlWhereZY, int Status, bool IsIncludeMZ, string SqlWhereMZ, clsCommonQueryDate_VO CommonQueryDate_VO, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientinfo(SqlWhereZY, Status, IsIncludeMZ, SqlWhereMZ, CommonQueryDate_VO, out dt);
        }
        #endregion

        #region 获取频率列表
        /// <summary>
        /// 获取频率列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>       
        public long m_lngGetUsageInfo(out DataTable dt)
        {
            return proxy.Service.m_lngGetUsageInfo(out dt);
        }
        #endregion

        #region 获取用法带出收费项目信息
        /// <summary>
        /// 获取用法带出收费项目信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PatType">病人身份</param>
        /// <param name="ApplyAreaID">开单病区ID</param>
        /// <returns></returns>        
        public long m_lngGetUsageAddItem(out DataTable dt, string PatType, string ApplyAreaID)
        {
            return proxy.Service.m_lngGetUsageAddItem(out dt, PatType, ApplyAreaID);
        }
        #endregion

        #region 获取收费组合主信息
        /// <summary>
        /// 获取收费组合主信息
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetItemGroup(string OperID, out DataTable dt)
        {
            return proxy.Service.m_lngGetItemGroup(OperID, out dt);
        }
        #endregion

        #region 获取收费组合明细
        /// <summary>
        /// 获取收费组合明细
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PatType"></param>
        /// <param name="ApplyAreaID"></param>
        /// <returns></returns>        
        public long m_lngGetItemGroupDet(out DataTable dt, string PatType, string ApplyAreaID)
        {
            return proxy.Service.m_lngGetItemGroupDet(out dt, PatType, ApplyAreaID);
        }
        #endregion

        #region 获取全院当前在院病人清单
        /// <summary>
        /// 获取全院当前在院病人清单
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetBihPatient(out DataTable dt)
        {
            return proxy.Service.m_lngGetBihPatient(out dt);
        }
        #endregion

        #region 根据住院号获取病人基本资料
        /// <summary>
        /// 根据住院号获取病人基本资料
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientinfoByZyh(string Zyh, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientinfoByZyh(Zyh, out dt);
        }
        #endregion

        #region 获取医嘱类型
        /// <summary>
        /// 获取医嘱类型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetOrderCate(out DataTable dt)
        {
            return proxy.Service.m_lngGetOrderCate(out dt);
        }
        #endregion

        #region 通过摆药ID查询该条记录医嘱是否已收取药袋费用及医嘱开单病区
        /// <summary>
        /// 通过摆药ID查询该条记录医嘱是否已收取药袋费用及医嘱开单病区
        /// </summary>
        /// <param name="p_strPutMedDetailID"></param>
        /// <param name="p_blnIfCharge"></param>
        /// <returns></returns>
        public long m_lngQueryIfChargeMedBag(string p_strPutMedDetailID, ref bool p_blnIfCharge, ref string p_strOrderCreateAreaID)
        {
            return proxy.Service.m_lngQueryIfChargeMedBag(p_strPutMedDetailID, ref p_blnIfCharge, ref p_strOrderCreateAreaID);
        }
        #endregion

        #region 更新摆药明细单中指定记录的药袋费用收取状态
        /// <summary>
        /// 更新摆药明细单中指定记录的药袋费用收取状态
        /// </summary>
        /// <param name="p_strPutMedDetailID"></param>
        /// <param name="p_intStatus"></param>
        /// <returns></returns>
        public long m_lngUpdateIfChargeMedBag(string p_strPutMedDetailID, int p_intStatus)
        {
            return proxy.Service.m_lngUpdateIfChargeMedBag(p_strPutMedDetailID, p_intStatus);
        }
        #endregion

        #region 查询生成药袋收费记录所需的信息
        /// <summary>
        /// 查询生成药袋收费记录所需的信息
        /// </summary>
        /// <param name="p_strOrderDicID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtOrderInfo"></param>
        /// <param name="p_dtPatientInfo"></param>
        /// <returns></returns>
        public long m_lngQueryInfoForChargeMedBag(string p_strOrderDicID, string p_strInPatientID, out DataTable p_dtOrderInfo, out DataTable p_dtPatientInfo)
        {
            return proxy.Service.m_lngQueryInfoForChargeMedBag(p_strOrderDicID, p_strInPatientID, out p_dtOrderInfo, out p_dtPatientInfo);
        }
        #endregion
    }
}
