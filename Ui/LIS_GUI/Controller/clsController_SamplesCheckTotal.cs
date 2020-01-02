using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    class clsController_SamplesCheckTotal : com.digitalwave.GUI_Base.clsController_Base
    { 
        public long m_lngGetSamplesCheckTotal(out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            long lngRes = 0;
             
            return lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSamplesCheckTotal(  out p_dtbResult, strDateFrom, strDateTo);
        }

        public long m_lngGetGermOccurRate(out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            long lngRes = 0;
             
            return lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetGermOccurRate(  out p_dtbResult, strDateFrom, strDateTo);
        }

        /// <summary>
        /// 获取 【细菌分布趋势】报表数据

        /// </summary>
        public long m_lngGetGermDistributeTrend(out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            long lngRes = 0; 
            return lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetGermDistributeTrend(  out p_dtbResult, strDateFrom, strDateTo);
        }

        public long m_lngGetAnimalculeCheckTotoal(out DataTable p_dtbResult, string strDateFrom, string strDateTo, List<string> listSamples, List<string> listPatientArea)
        { 
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAnimalculeCheckTotal(  out p_dtbResult, strDateFrom, strDateTo, listSamples, listPatientArea);
        }

        /// <summary>
        /// 样本列表
        /// </summary>
        /// <returns></returns>
        public DataTable m_dtbGetSamplesList()
        { 
            return (new weCare.Proxy.ProxyLis02()).Service.m_dtbGetSamplesList();
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public DataTable m_dtbGetDeptList()
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_dtbGetDeptList();
        }
    }
}
