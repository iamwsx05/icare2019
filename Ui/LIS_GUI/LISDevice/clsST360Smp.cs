using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// 酶标360的Mapping类

    /// </summary>
    public class clsST360Smp
    {
        public static clsST360Smp s_object
        {
            get { return new clsST360Smp(); }
        }

        public clsST360Smp()
        {
        }

        public long m_lngFindSTGroupResult(out List<clsDeviceReslutVO> lstCheckResults)
        {
            long lngRes = 0;
            lstCheckResults = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindSTGroupResult(out lstCheckResults);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        public long m_lngUpdateDeviceResult(clsDeviceReslutVO deviceResult)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateDeviceResult(deviceResult);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }
    }

    /// <summary>
    /// 酶标360CheckResult的Mapping类

    /// </summary>
    public class clsST360CheckResultSmp
    {
        #region 辅  助

        public static clsST360CheckResultSmp s_object
        {
            get { return new clsST360CheckResultSmp(); }
        }


        public clsST360CheckResultSmp()
        {
        }
        #endregion

        #region 公开方法

        public long m_lngInsert(clsST360CheckResultVO deviceResult)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsert(deviceResult);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        public long m_lngFind(string boardNo, out clsST360CheckResultVO[] arrCheckResult)
        {
            long lngRes = 0;
            arrCheckResult = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFind(boardNo, out arrCheckResult);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        public long m_lngFindBoardName(out string[] arrBoardName, DateTime begin, DateTime end)
        {
            long lngRes = 0;
            arrBoardName = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindBoardName(out arrBoardName, begin, end);
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        public long m_lngFindBoardName(out string[] arrBoardName)
        {
            long lngRes = 0;
            arrBoardName = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindBoardName(out arrBoardName);
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
