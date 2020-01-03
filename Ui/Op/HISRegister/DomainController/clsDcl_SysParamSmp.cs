using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 系统参数Smp
    /// </summary>
    internal class clsDcl_SysParamSmp : GUI_Base.clsDomainController_Base
    {
        public static clsDcl_SysParamSmp s_object
        {
            get
            {
                return new clsDcl_SysParamSmp();
            }
        }

        public long m_lngInsert(clsSysParamVO sysParamVO)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngInsert(sysParamVO);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        public long m_lngDelete(string sysParamCode)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngDelete(sysParamCode);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        public long m_lngUpdate(clsSysParamVO sysParamVO)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngUpdate(sysParamVO);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }


        public long m_lngFind(out clsSysParamVO[] arrParams)
        {
            long lngRes = 0;
            arrParams = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngFind(out arrParams);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

    }
}
