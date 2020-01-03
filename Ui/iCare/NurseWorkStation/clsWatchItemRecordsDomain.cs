using System;
using weCare.Core.Entity;
namespace iCare
{
    /// <summary>
    /// Summary description for clsWatchItemRecordsDomain.
    /// Alex 2003-5-14
    /// ���ڹ۲���Ŀ��¼����Domain��
    /// </summary>
    public class clsWatchItemRecordsDomain : clsRecordsDomain
    {
        /// <summary>
        ///  ���캯��������Ϊָ�����м����
        /// </summary>
        /// <param name="p_objRecordsServ"></param>
        public clsWatchItemRecordsDomain(enmRecordsType p_enmRecordsType)
            : base(p_enmRecordsType)
        {

        }

        public long m_lngGetRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            out clsWatchItemDataInfo p_objTansDataInfo)
        {
            //clsWatchItemTrackService m_objServ =
            //    (clsWatchItemTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsWatchItemTrackService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecordContent(p_strInPatientID,
                    p_strInPatientDate,
                    p_strOpenDate,
                    out p_objTansDataInfo);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

    }
}
