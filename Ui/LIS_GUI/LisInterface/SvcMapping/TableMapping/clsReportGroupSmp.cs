﻿using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui.LIS
{
    internal class clsReportGroupSmp : clsDomainController_Base
    {

        #region 构  造


        private clsReportGroupSmp()
        {
        }

        public static clsReportGroupSmp s_obj
        {
            get
            {
                return new clsReportGroupSmp();
            }
        }

        #endregion

        #region 根据检验标本组ID得到它所在报告组的VO,及打印顺序


        /// <summary>
        /// 根据检验标本组ID得到它所在报告组的VO及打印顺序

        /// </summary>
        /// <param name="sampleGroupId">检验标本组ID</param>
        /// <param name="reportGroupVO">报告组的VO</param>
        /// <returns></returns>
        public long m_lngGetReportGoupVO(string sampleGroupId, out clsReportGroup_VO reportGroupVO)
        {
            long lngRes = 0;
            reportGroupVO = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReportGoupVOBySampleGroupID(sampleGroupId, out reportGroupVO);
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
