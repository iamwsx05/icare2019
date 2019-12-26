﻿using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsDcl_MBY2010 : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        /// <summary>
        /// 酶标仪接口 摘要说明
        /// baojian.mo 2007-10-11
        /// </summary>
        public clsDcl_MBY2010()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 插入报告单

        public long m_lngInsertReport(int instrumentFlag, List<clsMBY2010VO> objResultArr, DateTime datReportDate)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis()).Service.lngInsertReport(  instrumentFlag, objResultArr, datReportDate);
            return lngRes;
        }
        #endregion

    }
}
