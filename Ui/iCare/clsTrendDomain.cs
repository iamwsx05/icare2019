using System;
using System.Data;
using System.Drawing;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsTrendDomain.
    /// </summary>
    public class clsTrendDomain
    {


        public clsTrendDomain()
        {

        }

        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        /// <param name="p_objVitalGroup"></param>
        /// <returns></returns>
        public long m_lngGetVitalGroup(out clsVitalGroup[] p_objVitalGroupArr)
        {
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetVitalGroup(out p_objVitalGroupArr);
            ////objSvc.Dispose();
            //return lngRes;

            p_objVitalGroupArr = null;
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objVitalSetArr"></param>
        /// <returns></returns>
        public long m_lngGetVitalSet(out clsVitalSet[] p_objVitalSetArr)
        {
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetVitalSet(out p_objVitalSetArr);
            ////objSvc.Dispose();
            //return lngRes;

            p_objVitalSetArr = null;
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_intBedID"></param>
        /// <param name="p_objPatDemoArr"></param>
        /// <returns></returns>
        public long m_lngGetPatientInfo(string p_strBedID, out clsPatDemo[] p_objPatDemoArr)
        {
            p_objPatDemoArr = null;
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetPatientInfo(p_strBedID, out p_objPatDemoArr);
            ////objSvc.Dispose();
            //return lngRes;
            return 0L;
        }

        /// <summary>
        /// ��ȡ���ƣ��Ѿ����ϣ�
        /// </summary>
        /// <param name="p_intCaseID"></param>
        /// <param name="p_dtmBeginDate"></param>
        /// <param name="p_dtmEndDate"></param>
        /// <param name="p_objTrendDataArr"></param>
        /// <returns></returns>
        public long m_lngGetTrendData(int p_intCaseID, DateTime p_dtmBeginDate, DateTime p_dtmEndDate, int[] p_intEMFC_IDArr, out clsTrendData[] p_objTrendDataArr)
        {
            p_objTrendDataArr = null;
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetTrendData(p_intCaseID, p_dtmBeginDate, p_dtmEndDate, p_intEMFC_IDArr, out p_objTrendDataArr);
            ////objSvc.Dispose();
            //return lngRes;
            return 0;
        }


        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_dtmBeginDate"></param>
        /// <param name="p_dtmEndDate"></param>
        /// <param name="p_intEMFC_IDArr"></param>
        /// <param name="p_objTrendDataArr"></param>
        /// <returns></returns>
        public long m_lngGetTrendData(string p_strInPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmBeginDate, DateTime p_dtmEndDate, int[] p_intEMFC_IDArr, out clsTrendData[] p_objTrendDataArr)
        {
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetTrendData(p_strInPatientID, p_dtmInPatientDate, p_dtmBeginDate, p_dtmEndDate, p_intEMFC_IDArr, out p_objTrendDataArr);
            ////objSvc.Dispose();
            //return lngRes;

            p_objTrendDataArr = null;
            return 0;
        }

        public long m_lngGetDocvueResult(string p_strInPatientID, DateTime p_dtmInPatientDate, string p_strEMFC_ID, DateTime p_dtmRecordDate, out string p_strResult)
        {
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetDocvueResult(int.Parse(p_strInPatientID), p_dtmInPatientDate, p_strEMFC_ID, p_dtmRecordDate, out p_strResult);
            ////objSvc.Dispose();
            //return lngRes;

            p_strResult = null;
            return 0;
        }

        public long m_lngGetDocvueResultArr(string p_strInPatientID, DateTime p_dtmInPatientDate, string[] p_strEMFC_IDArr, DateTime p_dtmRecordDate, out string[] p_strResultArr)
        {
            ////clsTrendServ objSvc =
            ////    (clsTrendServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsTrendServ));

            //long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetDocvueResultArr(int.Parse(p_strInPatientID), p_dtmInPatientDate, p_strEMFC_IDArr, p_dtmRecordDate, out p_strResultArr);
            ////objSvc.Dispose();
            //return lngRes;

            p_strResultArr = null;
            return 0;
        }


    }
}
