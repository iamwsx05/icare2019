using System;
using weCare.Core.Entity;
using System.Data;
namespace iCare
{

    /// <summary>
    /// ���̼�¼������㡣��Ϊ��Ϣ��ת�����������������ӽ�����м����
    /// </summary>
    public class clsDiseaseTrackDomain : com.digitalwave.GUI_Base.clsDomainController_Base
    {

        //private clsDiseaseTrackService m_objTrackServ;
        enmDiseaseTrackType m_enmProcessType = new enmDiseaseTrackType();

        /// <summary>
        /// ���캯��������Ϊָ�����м����
        /// </summary>
        /// <param name="p_objProcessServ"></param>
        public clsDiseaseTrackDomain(enmDiseaseTrackType p_enmProcessType)
        {
            //m_objTrackServ =  p_objProcessServ;
            m_enmProcessType = p_enmProcessType;
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
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            //�����ж�
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordTimeList_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
        }

        /// <summary>
        /// ��ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        public long m_lngGetRecordTimeList(string p_strRegisterID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            //�����ж�
            if (string.IsNullOrEmpty(p_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordTimeList_factory(m_enmProcessType, p_strRegisterID, out p_strCreateDateArr, out p_strOpenDateArr);
        }

        /// <summary>
        /// ��ȡָ����¼���ݡ�
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
            p_objRecordContent = null;
            //�����ж�
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
        }
        /// <summary>
        /// ��ȡָ����¼���ݡ�
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strCreatedDateDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        public long m_lngGetRecordContent(string p_strRegisterID,
            string p_strCreatedDateDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //�����ж�
            if (string.IsNullOrEmpty(p_strRegisterID) || string.IsNullOrEmpty(p_strCreatedDateDate))
                return (long)enmOperationResult.Parameter_Error;


            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(m_enmProcessType, p_strRegisterID, p_strCreatedDateDate, out p_objRecordContent);
        }
        #region ����
        /// <summary>
        /// ��ȡָ����¼����(��ɽ�������ų��Ļ������)��
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_strFormID">��ID</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        //public long m_lngGetRecordContent(string p_strInPatientID,
        //    string p_strInPatientDate,
        //    string p_strOpenDate,string p_strFormID,
        //    out clsTrackRecordContent p_objRecordContent)
        //{
        //    p_objRecordContent = null;
        //    //�����ж�
        //    if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
        //        return (long)enmOperationResult.Parameter_Error;

        //    clsDiseaseTrackService m_objTrackServ = clsDiseaseTrackDomainFactory.s_objGetDiseaseTrackDomain(m_enmProcessType);
        //    long lngRes = 0;
        //    try
        //    {
        //        lngRes = m_objTrackServ.m_lngGetRecordContent(  p_strInPatientID, p_strInPatientDate, p_strOpenDate,p_strFormID, out p_objRecordContent);
        //    }
        //    finally
        //    {
        //        //m_objTrackServ.Dispose();
        //    }
        //    return lngRes;
        //}
        #endregion
        /// <summary>
		/// ����¼�¼��
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objModifyInfo">��������ͬ�Ĵ���ʱ��,���ظü�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		public long m_lngAddNewRecord(clsTrackRecordContent p_objRecordContent, out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //�����ж�
            if (p_objRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngAddNewRecord_factory(m_enmProcessType, p_objRecordContent, out p_objModifyInfo);
        }


        /// <summary>
        /// ��ȡָ�����ң��������µ���Ժ����
        /// ����һ�㻤������¼��
        /// </summary>
        /// <param name="p_strID">����ID������ID��</param>
        /// <param name="p_objdtPatient"></param>
        /// <returns></returns>
        public long m_lngGetBedPatientInfo(string p_strID, out DataTable p_objdtPatient)
        {
            long lngRes = 0;
            p_objdtPatient = null;
            try
            {
                //�����ж�
                //long lngRes=0;
                if (p_strID == null)
                    return (long)enmOperationResult.Parameter_Error;
                lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetPatientInfo(p_strID, true, out p_objdtPatient);

            }
            catch (Exception exp)
            {
                string strError = exp.Message;
            }
            finally
            {
                //m_objHospitalManager.Dispose();
            }
            return lngRes;

        }


        /// <summary>
        /// �޸ļ�¼��
        /// </summary>
        /// <param name="p_objOldRecordContent">�޸�֮ǰ��ԭ��¼����</param>
        /// <param name="p_objNewRecordContent">�޸ĺ�ļ�¼����</param>
        /// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        public long m_lngModifyRecord(clsTrackRecordContent p_objOldRecordContent,
            clsTrackRecordContent p_objNewRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //�����ж�
            if (p_objOldRecordContent == null || p_objNewRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngModifyRecord_factory(m_enmProcessType, p_objOldRecordContent, p_objNewRecordContent, out p_objModifyInfo);
        }

        /// <summary>
        /// ɾ����¼��
        /// </summary>
        /// <param name="p_objRecordContent">��ǰҪɾ���ļ�¼</param>
        /// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        public long m_lngDeleteRecord(clsTrackRecordContent p_objRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //�����ж�
            if (p_objRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngDeleteRecord_factory(m_enmProcessType, p_objRecordContent, out p_objModifyInfo);
        }

        /// <summary>
        /// ����������¼��
        /// </summary>
        /// <param name="p_objDelRecord">Ҫ���ϵļ�¼</param>
        /// <param name="p_objAddNewRecord">�µļ�¼</param>
        /// <param name="p_objPreModifyInfo">����ǰҪ���ϵļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        public long m_lngReAddNewRecord(clsTrackRecordContent p_objDelRecord,
            clsTrackRecordContent p_objAddNewRecord,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //�����ж�
            if (p_objDelRecord == null || p_objAddNewRecord == null)
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngReAddNewRecord_factory(m_enmProcessType, p_objDelRecord, p_objAddNewRecord, out p_objModifyInfo);
        }

        // ��ȡ��ӡ��Ϣ��
        // 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
        //   �������µ����ݣ������������Ϊnull��
        // 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
        //   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣
        public long m_lngGetPrintInfo(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmModifyDate,
            out clsTrackRecordContent p_objContent,
            out DateTime p_dtmFirstPrintDate,
            out bool p_blnIsFirstPrint)
        {
            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetPrintInfo_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strOpenDate,
                p_dtmModifyDate, out p_objContent, out p_dtmFirstPrintDate, out p_blnIsFirstPrint);
        }

        // �������ݿ��е��״δ�ӡʱ�䡣
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngUpdateFirstPrintDate_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
        }

        // ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        public long m_lngGetDeleteRecordTimeList(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetDeleteRecordTimeList_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strDeleteUserID,
                out p_strCreateRecordTimeArr, out p_strOpenRecordTimeArr);
        }

        // ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        public long m_lngGetDeleteRecordTimeListAll(string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetDeleteRecordTimeListAll_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate,
                out p_strCreateRecordTimeArr, out p_strOpenRecordTimeArr);
        }

        // ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        public long m_lngGetDeleteRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //�����ж�
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenRecordTime == null || p_strOpenRecordTime == "")
                return (long)enmOperationResult.Parameter_Error;

            return (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetDeleteRecordContent_factory(m_enmProcessType, p_strInPatientID, p_strInPatientDate, p_strOpenRecordTime, out p_objRecordContent);
        }

    }// END CLASS DEFINITION clsDiseaseTrackDomain

}
