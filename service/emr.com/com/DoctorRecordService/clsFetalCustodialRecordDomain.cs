using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.DiseaseTrackService;
using weCare.Core.Entity; 

namespace iCare
{
    public class clsFetalCustodialRecordDomain
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
            clsFetalCustodialRecordService m_objServ = new clsFetalCustodialRecordService();

            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngGetRecordTimeList( p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
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
            clsFetalCustodialRecordService m_objServ = new clsFetalCustodialRecordService();
            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngGetRecordContent(  p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
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
            clsFetalCustodialRecordService m_objServ = new clsFetalCustodialRecordService();
            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngUpdateFirstPrintDate( p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
    }
}
