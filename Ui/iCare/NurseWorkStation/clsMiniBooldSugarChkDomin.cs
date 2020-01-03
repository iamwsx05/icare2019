using System;
using System.Collections;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// clsMiniBooldSugarChkDomin ��ժҪ˵����
    /// </summary>
    public class clsMiniBooldSugarChkDomin
    {
        //clsMiniBloodSugarChkServ m_objServ;
        public clsMiniBooldSugarChkDomin()
        {
            //m_objServ = new clsMiniBloodSugarChkServ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        public long m_lngAddNewRecoed(clsMiniBloodSugarChkValue p_objValue)
        {
            //clsMiniBloodSugarChkServ m_objServ =
            //    (clsMiniBloodSugarChkServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChkServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewRecoed(p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        public long m_lngModifyRecoed(clsMiniBloodSugarChkValue p_objValue)
        {
            //clsMiniBloodSugarChkServ m_objServ =
            //    (clsMiniBloodSugarChkServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChkServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngModifyRecoed(p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        public long m_lngDeleteRecoed(clsMiniBloodSugarChkValue p_objValue)
        {
            //clsMiniBloodSugarChkServ m_objServ =
            //    (clsMiniBloodSugarChkServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChkServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteRecoed(p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objValues"></param>
        /// <returns></returns>
        public long m_lngGetRecoedByInPatient(string p_strInPatientID, DateTime p_dtmInPatientDate, out clsMiniBloodSugarChkValue[] p_objValues)
        {
            //clsMiniBloodSugarChkServ m_objServ =
            //    (clsMiniBloodSugarChkServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChkServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecoedByInPatient(p_strInPatientID, p_dtmInPatientDate, out p_objValues);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
    }
}
