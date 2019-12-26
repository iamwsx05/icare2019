using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsDomainController_MicReport : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainController_MicReport()
        {
        } 

        public long lngGetAllAnti(out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetAllAnti(out dtbResult);
            return lngRes;
        }

        public long lngGetFuzzyQueryAnti(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetFuzzyQueryAnti(micName, IsEnglish, out dtbResult);
            return lngRes;
        }

        public long lngGetAllMic(out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetAllMic(out dtbResult);
            return lngRes;
        }

        public long lngGetFuzzyQueryMic(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetFuzzyQueryMic(micName, IsEnglish, out dtbResult);
            return lngRes;
        }

        //细菌分布报告统计报表
        public long lngGetBacteriaDistribution(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetBacteriaDistribution(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, out dtbResult);
            return lngRes;
        }
        //细菌分布趋势报告
        public long lngGetMicdistributionTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetMicdistributionTend(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, out dtbResult);
            return lngRes;
        }

        // 累计敏感性
        public long lngGetMicSensitive(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTo, string SamtName, string DisName, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetMicSensitive(micname, p_dtDateFrom, p_dtDateTo, SamtName, DisName, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
            return lngRes;
        }
        // 敏感率趋势报告
        public long lngGetSensitiveTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetSensitiveTend(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
            return lngRes;
        }

        public long lngGetMicCumulative(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetMicCumulative(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
            return lngRes;
        }

        public long lngGetSensitiveRate(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetSensitiveRate(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
            return lngRes;
        }

        //取得病区
        public long lngGetDeptName(out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetDeptName(out dtbResult);
            return lngRes;
        }

        //取得样本类型
        public long lngGetSamType(out DataTable dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.lngGetSamType(out dtbResult);
            return lngRes;
        }
    }
}