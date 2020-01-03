using System;
using weCare.Core.Entity;

namespace iCare
{

    /// <summary>
    ///  ���������¼���͵�����㡣��Ϊ��Ϣ��ת�����������������ӽ�����м����
    /// </summary>
    public class clsOutHospitalIn24HoursDomain
    {

        //protected clsDiseaseTrackService m_objRecordsServ = new clsEMR_OutHospitalIn24HoursService();

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
            //clsDiseaseTrackService m_objRecordsServ =
            //    (clsDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OutHospitalIn24HoursService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordTimeList_factory(enmDiseaseTrackType.OutHospitalIn24Hours, p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
            }
            finally
            {
                //m_objRecordsServ.Dispose();
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
            //clsDiseaseTrackService m_objRecordsServ =
            //    (clsDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OutHospitalIn24HoursService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(enmDiseaseTrackType.OutHospitalIn24Hours, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
            }
            finally
            {
                //m_objRecordsServ.Dispose();
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
            //clsDiseaseTrackService m_objRecordsServ =
            //    (clsDiseaseTrackService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OutHospitalIn24HoursService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngUpdateFirstPrintDate_factory(enmDiseaseTrackType.OutHospitalIn24Hours, p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            }
            finally
            {
                //m_objRecordsServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡȫ�����ϼ�¼clsConsultationService
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetAllInactiveInfo(string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            //com.digitalwave.DiseaseTrackService.clsEMR_OutHospitalIn24HoursService objServ =
            //        (com.digitalwave.DiseaseTrackService.clsEMR_OutHospitalIn24HoursService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.DiseaseTrackService.clsEMR_OutHospitalIn24HoursService));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllInactiveInfo(p_strInpatientId, p_dtmInpatientDate, out p_objInactiveRecordInfoArr);
            return lngRes;
        }

    }
}
