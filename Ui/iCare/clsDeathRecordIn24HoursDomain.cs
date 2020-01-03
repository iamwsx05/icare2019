using System;
using weCare.Core.Entity;


namespace iCare
{
    /// <summary>
    /// clsDeathRecordIn24HoursDomain ��ժҪ˵����
    /// Description:��Ժ24Сʱ��������¼DOMAIN��,���������¼���͵������,�������ӽ�����м��
    /// Author:Jock
    /// Date:06-01-03
    /// </summary>
    public class clsDeathRecordIn24HoursDomain
    {
        public clsDeathRecordIn24HoursDomain()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ��ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        public long m_lngGetRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            //clsEMR_DeathRecorIn24HoursService m_objServ =
            //    (clsEMR_DeathRecorIn24HoursService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_DeathRecorIn24HoursService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsDeathRecordService_m_lngGetRecordTimeList(p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡָ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        public long m_lngGetRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            //clsEMR_DeathRecorIn24HoursService m_objServ =
            //    (clsEMR_DeathRecorIn24HoursService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_DeathRecorIn24HoursService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(enmDiseaseTrackType.DeathHospitalIn24Hours, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// �������ݿ��е��״δ�ӡʱ�䡣
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
        /// <returns></returns>
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            //clsEMR_DeathRecorIn24HoursService m_objServ =
            //    (clsEMR_DeathRecorIn24HoursService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_DeathRecorIn24HoursService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsDeathRecordService_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
    }
}
