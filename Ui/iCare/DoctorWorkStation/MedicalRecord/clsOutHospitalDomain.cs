using System;
using weCare.Core.Entity;

namespace iCare
{

    /// <summary>
    ///  ���������¼���͵�����㡣��Ϊ��Ϣ��ת�����������������ӽ�����м����
    /// </summary>
    public class clsOutHospitalDomain
    {
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
            //clsOutHospitalService m_objServ =
            //    (clsOutHospitalService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOutHospitalService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOutHospitalService_m_lngGetRecordTimeList(  p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
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
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOutHospitalService_m_lngGetRecordContent(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
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
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOutHospitalService_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ���³�Ժʱ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtmOutDate">��Ժʱ��</param>
        /// <returns></returns>
        public long m_lngUpdateOutDate(string p_strRegisterID, DateTime p_dtmOutDate)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngUpdateOutDate(p_strRegisterID, p_dtmOutDate);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���˳�Ժҽ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_dtbOrder">��Ժҽ��</param>
        /// <returns></returns>
        public long m_lngGetOutOrderByRegID(string p_strRegisterID, out System.Data.DataTable p_dtbOrder)
        {
            p_dtbOrder = null;
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001") return -2;//������ҽԺ�ݲ���ȡҽ��
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOutOrderByRegID(p_strRegisterID, out p_dtbOrder);
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
            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllInactiveInfo(p_strInpatientId, p_dtmInpatientDate, out p_objInactiveRecordInfoArr);
            return lngRes;
        }
    }
}
