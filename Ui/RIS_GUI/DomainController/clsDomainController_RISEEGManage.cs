using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.RIS
{
    /// <summary>
    /// clsDomainController_RISCardiogramManage ��ժҪ˵����
    /// Alex 2004-5-27
    /// </summary>
    public class clsDomainController_RISEEGManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyRIS proxy
        {
            get
            {
                return new weCare.Proxy.ProxyRIS();
            }
        }

        #region TCD����
        #region ���TCD����
        /// <summary>
        /// ���TCD����
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objResult"></param>
        public long m_lngDoAddNewTCDmReport(out string p_strRecordID, clsRIS_TCD_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewTCDReport(out p_strRecordID, p_objResult);
            return lngRes;
        }
        #endregion

        #region ����ĵ�ͼ����
        /// <summary>
        /// ����ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResultArr"></param>
        public void m_mthGetTCDReportArr(out clsRIS_TCD_REPORT_VO[] p_objResultArr)
        {
            long lngRes = proxy.Service.m_lngGetTCDReportArr(out p_objResultArr);
        }
        #endregion
        #region ����ĵ�ͼ���淵��DataTable
        public void m_mthGetTCDReportdtb(string P_fromDat, string P_toDat, string P_strdept, out DataTable p_dtbResult, string strFirstType, string strFirstValue, string strLastType, string strLastValue, bool flag)
        {
            long lngRes = proxy.Service.m_lngGetTCDReportDtb(P_fromDat, P_toDat, P_strdept, out p_dtbResult, strFirstType, strFirstValue, strLastType, strLastValue, flag);

        }
        #endregion

        #region ����ĵ�ͼ����ByID
        /// <summary>
        /// ����ĵ�ͼ����ByID
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        public void m_mthGetCardiogramReportByID(string p_strID, out clsRIS_CardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngGetCardiogramReportByID(p_strID, out p_objResult);
        }
        #endregion

        #region �޸��ĵ�ͼ����
        /// <summary>
        /// �޸��ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoModifyTCDReport(clsRIS_TCD_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngModifyTCDReport(p_objResult);
            return lngRes;
        }
        #endregion

        #region ɾ���ĵ�ͼ����
        /// <summary>
        /// ɾ���ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoDeleteTCDReport(clsRIS_TCD_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngDeleteTCDReport(p_objResult);
            //						objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ����������ϲ�ѯ�Ե�ͼ����
        public long m_lngGetTCDReportByCondition(string p_strFromDat, string p_strToDat, string p_strPatientNo, string p_strInPatientNo,
            string p_strPatientName, string p_strDept, string p_strReportNo, string strReporter, out clsRIS_TCD_REPORT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetTCDReportByCondition(p_strFromDat, p_strToDat, p_strPatientNo, p_strInPatientNo, p_strPatientName,
                p_strDept, p_strReportNo, strReporter, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #endregion

        #region EEG����
        #region ���EEG����
        public long m_lngDoAddNewEEGmReport(out string p_strRecordID, clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewEEGReport(out p_strRecordID, p_objResult);

            return lngRes;
        }
        #endregion
        #region ���EEG����
        public void m_mthGetEEGReportArr(out clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            long lngRes = proxy.Service.m_lngGetEEGReportArr(out p_objResultArr);

        }
        #endregion
        #region ���EEG����ByID
        /// <summary>
        /// ���EEG����ByID
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        public void m_mthGetEEGReportByID(string p_strID, out clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngGetEEGReportByID(p_strID, out p_objResult);

        }
        #endregion
        #region �޸��ĵ�ͼ����
        /// <summary>
        /// �޸�EEG����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoModifyEEGReport(clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngModifyEEGReport(p_objResult);

            return lngRes;
        }
        #endregion
        #region ɾ���ĵ�ͼ����
        /// <summary>
        /// ɾ��EEG����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoDeleteEEGReport(clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngDeleteEEGReport(p_objResult);

            return lngRes;
        }
        #endregion
        #region ����������ϲ�ѯ��EEG��ͼ���� ͯ�� 2004.06.22
        public long m_lngGetEEGReportByCondition(string p_strFromDat, string p_strToDat, string p_strPatientNo, string p_strInPatientNo,
            string p_strPatientName, string p_strDept, string p_strReportNo, string strReporter, out clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetEEGReportByCondition(p_strFromDat, p_strToDat, p_strPatientNo, p_strInPatientNo, p_strPatientName,
                p_strDept, p_strReportNo, strReporter, out p_objResultArr);

            return lngRes;
        }
        #endregion
        #region ����Ե�ͼ���淵��DataTable
        public void m_mthGetEEGReportdtb(string P_fromDat, string P_toDat, string P_strdept, out DataTable p_dtbResult, string strFirstType, string strFirstValue, string strLastType, string strLastValue, bool flag)
        {
            long lngRes = proxy.Service.m_lngGetEEGReportDtb(P_fromDat, P_toDat, P_strdept, out p_dtbResult, strFirstType, strFirstValue, strLastType, strLastValue, flag);
        }
        #endregion
        #endregion

        #region ��ȡ��Ӧ���뵥������ID(�Ե�ͼ��
        public void m_mthGetApplTypeIDRISEEGR(out string TypeID)
        {
            long lngRes = proxy.Service.m_mthGetApplTypeIDRISEEGR(out TypeID);
        }
        #endregion

        #region ��ȡ�ĵ�ͼ��������
        public long m_lngGetAllSyncInfoForInPatient(out clsCustom_SyncInfo[] p_objSyncInfoArr)
        {
            long lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetAllSyncInfo(out p_objSyncInfoArr);
            return lngRes;
        }
        #endregion

        #region ��ˣ�EEG���浥��
        /// <summary>
        ///  ��ˣ�EEG���浥��
        /// </summary>
        /// <param name="strRordID"></param>
        /// <param name="m_strEmpID"></param>
        /// <param name="m_strEmpName"></param>
        /// <returns></returns>
        public long m_lngConfigEEGReport(string strRordID, string m_strEmpID, string m_strEmpName)
        {
            long lngRes = proxy.Service.m_lngConfigEEGReport(strRordID, m_strEmpID, m_strEmpName);

            return lngRes;
        }

        #endregion

        #region ��ˣ�TCD���浥��
        /// <summary>
        /// ��ˣ�TCD���浥��
        /// </summary>
        /// <param name="strRordID"></param>
        /// <param name="m_strEmpID"></param>
        /// <param name="m_strEmpName"></param>
        /// <returns></returns>
        public long m_lngConfigTCDReport(string strRordID, string m_strEmpID, string m_strEmpName)
        {
            long lngRes = proxy.Service.m_lngConfigTCDReport(strRordID, m_strEmpID, m_strEmpName);

            return lngRes;
        }

        #endregion

        #region ���ݾ��￨�ŷ��ز��������Ϣ
        /// <summary>
        /// ���ݾ��￨�ŷ��ز��������Ϣ
        /// </summary>
        /// <returns></returns>
        public long m_strDiagByCardId(string p_strCard, out string p_strResult)
        {
            p_strResult = "";
            long lngRes = proxy.Service.m_strDiagByCardId(p_strCard, out p_strResult);

            return lngRes;
        }
        #endregion

        #region ����סԺ�Ż�ȡ������Ϣ
        /// <summary>
        /// ����סԺ�Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="strInpatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetPatByInPatientID(string strInpatientID, out DataTable dtResult)
        {
            long lngRes = proxy.Service.m_lngGetPatByInPatientID(strInpatientID, out dtResult);
            return lngRes;
        }
        #endregion
    }
}
