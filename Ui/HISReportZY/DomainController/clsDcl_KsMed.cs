﻿using System;
using System.Collections.Generic;
using System.Text; 
using System.Data;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public class clsDcl_KsMed : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 科室用药查询
        /// <summary>
        /// 科室用药查询
        /// </summary>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetKsMed(string dteStart, string dteEnd, string deptStr, out DataTable dtbResult)
        {
            //using (clsHISReportZy_Supported_Svc svc = (clsHISReportZy_Supported_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsHISReportZy_Supported_Svc)))
            //{
                return (new weCare.Proxy.ProxyReport()).Service.lngGetKsMed(dteStart, dteEnd, deptStr, out dtbResult);
            //}
        }
        #endregion
    }
}
