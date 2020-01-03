using System;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsPrintDateInfo.
    /// ���ڶ�ȡ�����ô�ӡʱ�����
    /// Alex 2003-3-1
    /// </summary>
    public class clsPrintDateInfo
    {
        /// <summary>
        /// ���ڶ�ȡ�����ô�ӡʱ�����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        public clsPrintDateInfo()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objServ = new clsPrintDateInfoServ();
        }

        //private clsPrintDateInfoServ m_objServ;

        /// <summary>
        /// ���Ҹ�����¼�ĵ�һ�δ�ӡʱ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        public string m_strGetFirstPrintDate(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, string p_strTableName)
        {
            //clsPrintDateInfoServ m_objServ =
            //    (clsPrintDateInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPrintDateInfoServ));

            return (new weCare.Proxy.ProxyEmr()).Service.m_strGetFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, p_strTableName);
        }

        /// <summary>
        /// ���ø�����¼�ĵ�һ�δ�ӡʱ��
        /// </summary>
        /// <param name="p_strInPatientIDArr"></param>
        /// <param name="p_strInPatientDateArr"></param>
        /// <param name="p_strCreateDateArr"></param>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        public long m_lngSetFirstPrintDate(string[] p_strInPatientIDArr, string[] p_strInPatientDateArr, string[] p_strCreateDateArr, string p_strTableName)
        {
            //clsPrintDateInfoServ m_objServ =
            //    (clsPrintDateInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPrintDateInfoServ));

            long m_lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetFirstPrintDate(p_strInPatientIDArr, p_strInPatientDateArr, p_strCreateDateArr, p_strTableName);
            return m_lngRes;
        }

        /// <summary>
        /// ���жϸü�¼�Ƿ��Ѿ���ӡ�������У�����д��ʱ�䣬���򣬱��������¼�ĵ�һ�δ�ӡʱ��
        /// </summary>
        /// <param name="p_strInPatientIDArr"></param>
        /// <param name="p_strInPatientDateArr"></param>
        /// <param name="p_strCreateDateArr"></param>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        public long m_lngSetFirstPrintDateWithCheck(string[] p_strInPatientIDArr, string[] p_strInPatientDateArr, string[] p_strCreateDateArr, string p_strTableName)
        {
            //clsPrintDateInfoServ m_objServ =
            //    (clsPrintDateInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPrintDateInfoServ));

            long m_lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetFirstPrintDateWithCheck(p_strInPatientIDArr, p_strInPatientDateArr, p_strCreateDateArr, p_strTableName);
            return m_lngRes;
        }

        /// <summary>
        /// ����ĳ����¼����ӡ���ڼ�����¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_intRecord"></param>
        /// <returns></returns>
        public long m_lngSavePrintToRecord(string p_strInPatientID, string p_strInPatientDate, string p_strFormName, int p_intToRecord)
        {
            //clsPrintDateInfoServ m_objServ =
            //    (clsPrintDateInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPrintDateInfoServ));

            return (new weCare.Proxy.ProxyEmr()).Service.m_lngSavePrintToRecord(p_strInPatientID, p_strInPatientDate, p_strFormName, p_intToRecord);
        }

        /// <summary>
        /// ��ȡĳ����¼����ӡ���ڼ�����¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strFormName"></param>
        /// <param name="p_intRecord"></param>
        /// <returns></returns>
        public long m_lngGetPrintFromRecord(string p_strInPatientID, string p_strInPatientDate, string p_strFormName, out int p_intFromRecord)
        {
            //clsPrintDateInfoServ m_objServ =
            //    (clsPrintDateInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPrintDateInfoServ));

            return (new weCare.Proxy.ProxyEmr()).Service.m_lngGetPrintFromRecord(p_strInPatientID, p_strInPatientDate, p_strFormName, out p_intFromRecord);
        }

    }
}
