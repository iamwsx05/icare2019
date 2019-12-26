using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 辅助资料DOMAIN类
    /// </summary>
    public class clsDcl_AidDictionary : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public clsDcl_AidDictionary()
        {
        }

        weCare.Proxy.ProxyIP02 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP02();
            }
        }

        #endregion

        #region 查找医生职称
        /// <summary>
        /// 查找医生职称
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDoctorDuty(out DataTable dt)
        {
            return proxy.Service.m_lngGetDoctorDuty(out dt); 
        }
        #endregion

        #region 保存门诊默认加收项目表
        /// <summary>
        /// 保存门诊默认加收项目表
        /// </summary>
        /// <param name="RecordsArr"></param>
        /// <param name="Flag">-1 只删除</param>
        /// <param name="PayTypeID"></param>
        /// <returns></returns>        
        public long m_lngSaveOutPatientDefaultAddItem(List<clsOutPatientDefaultAddItem_VO> RecordsArr, int Flag, string PayTypeID)
        {
            return proxy.Service.m_lngSaveOutPatientDefaultAddItem(RecordsArr, Flag, PayTypeID); 
        }
        #endregion

        #region 获取门诊默认加收项目表
        /// <summary>
        /// 获取门诊默认加收项目表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PayTypeID"></param>
        /// <returns></returns>
        public long m_lngGetOutPatientDefaultAddItem(out DataTable dt, string PayTypeID)
        { 
            return proxy.Service.m_lngGetOutPatientDefaultAddItem(out dt, PayTypeID); 
        }
        #endregion
    }
}
