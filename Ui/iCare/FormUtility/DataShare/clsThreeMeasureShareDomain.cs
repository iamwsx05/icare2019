using System;
using System.Data;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsThreeMeasureShareDomain.
    /// </summary>
    public class clsThreeMeasureShareDomain
    {
        //private clsThreeMeasureShareService m_objServ;

        public clsThreeMeasureShareDomain()
        {
            //m_objServ = new clsThreeMeasureShareService();
        }

        public long m_lngGetFirstValue(string p_strInPaitentID, string p_strInPatientDate, out stuFirstValue p_stuResult)
        {
            p_stuResult = new stuFirstValue();
            p_stuResult.m_strDiastolicValue = "";
            p_stuResult.m_strSystolicValue = "";
            p_stuResult.m_strDiastolicValue2 = "";
            p_stuResult.m_strSystolicValue2 = "";
            p_stuResult.m_strPulseValue = "";
            p_stuResult.m_strTemperatureValue = "";
            p_stuResult.m_strBreathValue = "";

            DataTable dtbResult;

            //clsThreeMeasureShareService m_objServ =
            //    (clsThreeMeasureShareService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureShareService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsThreeMeasureShareService_m_lngGetFirstValue(p_strInPaitentID, p_strInPatientDate, out dtbResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes <= 0)
                return lngRes;

            if (dtbResult.Rows.Count != 1)
                return 0;

            p_stuResult.m_strDiastolicValue = dtbResult.Rows[0]["DIASTOLICVALUE"].ToString();
            p_stuResult.m_strSystolicValue = dtbResult.Rows[0]["SYSTOLICVALUE"].ToString();
            p_stuResult.m_strDiastolicValue2 = dtbResult.Rows[0]["DIASTOLICVALUE2"].ToString();
            p_stuResult.m_strSystolicValue2 = dtbResult.Rows[0]["SYSTOLICVALUE2"].ToString();
            p_stuResult.m_strPulseValue = dtbResult.Rows[0]["PULSEVALUE"].ToString();
            p_stuResult.m_strTemperatureValue = dtbResult.Rows[0]["TEMPERATUREVALUE"].ToString();
            p_stuResult.m_strBreathValue = dtbResult.Rows[0]["BREATHVALUE"].ToString();

            return 1;
        }


        /// <summary>
        /// ��ȡ���ڽ�ʱ�������
        /// </summary>
        /// <param name="p_strInPaitentID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtmCompare"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetNearestValue(string p_strInPaitentID, string p_strInPatientDate, DateTime p_dtmCompare, out string[] p_strResult)
        {
            //clsThreeMeasureShareService m_objServ =
            //    (clsThreeMeasureShareService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureShareService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsThreeMeasureShareService_m_lngGetNearestValue(p_strInPaitentID, p_strInPatientDate, p_dtmCompare, out p_strResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// �����ĵ�һ����¼
        /// </summary>
        public struct stuFirstValue
        {
            /// <summary>
            /// ����ѹ
            /// </summary>
            public string m_strDiastolicValue;
            /// <summary>
            /// ����ѹ
            /// </summary>
            public string m_strSystolicValue;
            /// <summary>
            /// ����ѹ2
            /// </summary>
            public string m_strDiastolicValue2;
            /// <summary>
            /// ����ѹ2
            /// </summary>
            public string m_strSystolicValue2;
            /// <summary>
            /// ����
            /// </summary>
            public string m_strPulseValue;
            /// <summary>
            /// ����
            /// </summary>
            public string m_strTemperatureValue;
            /// <summary>
            /// ����
            /// </summary>
            public string m_strBreathValue;
        }
    }
}
