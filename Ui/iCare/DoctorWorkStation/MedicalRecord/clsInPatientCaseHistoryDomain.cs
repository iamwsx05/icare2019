using System;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// Summary description for clsInPatientCaseHistoryDomain.
    /// </summary>
    public class clsBaseCaseHistoryDomain
    {

        //private clsBaseCaseHistorySevice m_objBaseServ;
        private enmBaseCaseHistoryTypeInfo m_enmProcessType = new enmBaseCaseHistoryTypeInfo();

        // ���캯��������Ϊָ�����м����
        public clsBaseCaseHistoryDomain(enmBaseCaseHistoryTypeInfo p_enmProcessType)
        {
            //m_objBaseServ =  p_objProcessServ;
            m_enmProcessType = p_enmProcessType;
        }

        // ��ȡ���˸������¼��ʱ���б�
        public long m_lngGetRecordTimeList(string p_strInPatientID,
            out string[] p_strInPatientDateArr,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            //�����ж�
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;

            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordTimeList_factory(m_enmProcessType, p_strInPatientID, out p_strInPatientDateArr, out p_strCreateRecordTimeArr, out p_strOpenRecordTimeArr);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // ��ȡָ����¼���ݡ�
        public long m_lngGetRecordContent(string p_strInPatientID,
            string p_strInPatientDate,/*string p_strOpenRecordTime,*/
            out clsBaseCaseHistoryInfo p_objRecordContent,
            out clsPictureBoxValue[] p_objPicValueArr)
        {
            //�����ж�
            p_objRecordContent = null;
            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/out p_objRecordContent, out p_objPicValueArr);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // ����¼�¼��
        public long m_lngAddNewRecord(clsBaseCaseHistoryInfo p_objRecordContent, clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;
            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngAddNewRecord_factory(m_enmProcessType, p_objRecordContent, p_objPicValueArr, p_strDiseaseID, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, out p_objModifyInfo);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // �޸ļ�¼��
        public long m_lngModifyRecord(clsInPatientCaseHistoryContent p_objOldRecordContent,
            clsInPatientCaseHistoryContent p_objNewRecordContent, clsPictureBoxValue[] p_objPicValueArr,
            string p_strDiseaseID,
            out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;
            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngModifyRecord_factory(m_enmProcessType, p_objOldRecordContent, p_objNewRecordContent, p_objPicValueArr, p_strDiseaseID, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, out p_objModifyInfo);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // ɾ����¼��
        public long m_lngDeleteRecord(clsInPatientCaseHistoryContent p_objRecordContent, out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�

            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngDeleteRecord_factory(m_enmProcessType, p_objRecordContent, out p_objModifyInfo);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // ����������¼��
        public long m_lngReAddNewRecord(clsInPatientCaseHistoryContent m_objDelRecord,
            clsInPatientCaseHistoryContent m_objAddNewRecord,
            out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;

            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngReAddNewRecord_factory(m_enmProcessType, m_objDelRecord, m_objAddNewRecord, out p_objModifyInfo);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // ��ȡ��ӡ��Ϣ��
        // 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
        //   �������µ����ݣ������������Ϊnull��
        // 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
        //   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣
        public long m_lngGetPrintInfo(string p_strInPatientID, string p_strInPatientDate,/*string p_strOpenDate,*/DateTime p_dtmModifyDate,
            out clsBaseCaseHistoryInfo p_objContent,
            out clsPictureBoxValue[] p_objPicValueArr,
            out DateTime p_dtmFirstPrintDate,
            out bool p_blnIsFirstPrint)
        {
            p_dtmFirstPrintDate = DateTime.MinValue;
            p_blnIsFirstPrint = false;
            p_objContent = null;
            p_objPicValueArr = null;

            if (p_strInPatientID == "" || p_strInPatientID == null || p_strInPatientDate == "" || p_strInPatientDate == null)//|| p_strOpenDate=="" || p_strOpenDate==null )
                return (long)enmOperationResult.Parameter_Error;


            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetPrintInfo_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_dtmModifyDate, out p_objContent, out p_objPicValueArr, out p_dtmFirstPrintDate, out p_blnIsFirstPrint);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;

        }

        // �������ݿ��е��״δ�ӡʱ�䡣
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            //			return (long)enmOperationResult.DB_Succeed;
            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngUpdateFirstPrintDate_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        // ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        public long m_lngGetDeleteRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            clsPatient p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return (long)enmOperationResult.DB_Succeed;
        }

        // ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        public long m_lngGetDeleteRecordTimeListAll(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return (long)enmOperationResult.DB_Succeed;
        }

        // ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        public long m_lngGetDeleteRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            out clsInPatientCaseHistoryContent p_objRecordContent)
        {
            p_objRecordContent = null;
            clsBaseCaseHistoryInfo objRecordContent = null;
            //clsBaseCaseHistorySevice m_objBaseServ = clsCaseHistoryFactory.s_objGetDomain(m_enmProcessType);
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetDeleteRecordContent_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strOpenRecordTime, out objRecordContent);
                p_objRecordContent = (clsInPatientCaseHistoryContent)objRecordContent;
            }
            finally
            {
                //m_objBaseServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetEMRInPatientInfo(string p_strRegisterID,
            out string p_strEMRInPatientID,
            out DateTime p_dtmEMRInDate)
        {
            p_strEMRInPatientID = string.Empty;
            p_dtmEMRInDate = new DateTime(1900, 1, 1);

            return (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetEMRInPatientInfo(p_strRegisterID, out p_strEMRInPatientID, out p_dtmEMRInDate);

        }

    }

    public class clsInPatientCaseHistoryDomain
    {
        /// <summary>
        /// ��ȡһ��סԺȫ�����ϼ�¼
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetAllInactiveInfo(string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            return (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllInactiveInfo(p_strInpatientId, p_dtmInpatientDate, out p_objInactiveRecordInfoArr);

        }
    }
}
