using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// 申请单收费状态Smp
    /// </summary>
    internal class clsChargeInfoStatusSmp : clsDomainController_Base
    {

        #region 构  造


        private clsChargeInfoStatusSmp()
        {
        }

        public static clsChargeInfoStatusSmp s_obj
        {
            get
            {
                return new clsChargeInfoStatusSmp();
            }
        }

        #endregion

        #region 根据申请单元Id获取收费信息

        /// <summary>
        /// 根据申请单元Id获取收费信息
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="chargeStatusVO"></param>
        /// <returns></returns>
        public long m_lngFind(string applicationId, out clsChargeStatusVO chargeStatusVO)
        {
            long lngRes = 0;
            chargeStatusVO = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFind(applicationId, out chargeStatusVO);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

    }
}
