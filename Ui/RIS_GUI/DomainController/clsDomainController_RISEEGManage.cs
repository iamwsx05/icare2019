using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.RIS
{
    /// <summary>
    /// clsDomainController_RISCardiogramManage 的摘要说明。
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

        #region TCD报告
        #region 添加TCD报告
        /// <summary>
        /// 添加TCD报告
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objResult"></param>
        public long m_lngDoAddNewTCDmReport(out string p_strRecordID, clsRIS_TCD_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewTCDReport(out p_strRecordID, p_objResult);
            return lngRes;
        }
        #endregion

        #region 获得心电图报告
        /// <summary>
        /// 获得心电图报告
        /// </summary>
        /// <param name="p_objResultArr"></param>
        public void m_mthGetTCDReportArr(out clsRIS_TCD_REPORT_VO[] p_objResultArr)
        {
            long lngRes = proxy.Service.m_lngGetTCDReportArr(out p_objResultArr);
        }
        #endregion
        #region 获得心电图报告返回DataTable
        public void m_mthGetTCDReportdtb(string P_fromDat, string P_toDat, string P_strdept, out DataTable p_dtbResult, string strFirstType, string strFirstValue, string strLastType, string strLastValue, bool flag)
        {
            long lngRes = proxy.Service.m_lngGetTCDReportDtb(P_fromDat, P_toDat, P_strdept, out p_dtbResult, strFirstType, strFirstValue, strLastType, strLastValue, flag);

        }
        #endregion

        #region 获得心电图报告ByID
        /// <summary>
        /// 获得心电图报告ByID
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        public void m_mthGetCardiogramReportByID(string p_strID, out clsRIS_CardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngGetCardiogramReportByID(p_strID, out p_objResult);
        }
        #endregion

        #region 修改心电图报告
        /// <summary>
        /// 修改心电图报告
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoModifyTCDReport(clsRIS_TCD_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngModifyTCDReport(p_objResult);
            return lngRes;
        }
        #endregion

        #region 删除心电图报告
        /// <summary>
        /// 删除心电图报告
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoDeleteTCDReport(clsRIS_TCD_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngDeleteTCDReport(p_objResult);
            //						objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region 根据条件组合查询脑电图报告
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

        #region EEG报告
        #region 添加EEG报告
        public long m_lngDoAddNewEEGmReport(out string p_strRecordID, clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewEEGReport(out p_strRecordID, p_objResult);

            return lngRes;
        }
        #endregion
        #region 获得EEG报告
        public void m_mthGetEEGReportArr(out clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            long lngRes = proxy.Service.m_lngGetEEGReportArr(out p_objResultArr);

        }
        #endregion
        #region 获得EEG报告ByID
        /// <summary>
        /// 获得EEG报告ByID
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        public void m_mthGetEEGReportByID(string p_strID, out clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngGetEEGReportByID(p_strID, out p_objResult);

        }
        #endregion
        #region 修改心电图报告
        /// <summary>
        /// 修改EEG报告
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoModifyEEGReport(clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngModifyEEGReport(p_objResult);

            return lngRes;
        }
        #endregion
        #region 删除心电图报告
        /// <summary>
        /// 删除EEG报告
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoDeleteEEGReport(clsRIS_EEG_REPORT_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngDeleteEEGReport(p_objResult);

            return lngRes;
        }
        #endregion
        #region 根据条件组合查询脑EEG电图报告 童华 2004.06.22
        public long m_lngGetEEGReportByCondition(string p_strFromDat, string p_strToDat, string p_strPatientNo, string p_strInPatientNo,
            string p_strPatientName, string p_strDept, string p_strReportNo, string strReporter, out clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetEEGReportByCondition(p_strFromDat, p_strToDat, p_strPatientNo, p_strInPatientNo, p_strPatientName,
                p_strDept, p_strReportNo, strReporter, out p_objResultArr);

            return lngRes;
        }
        #endregion
        #region 获得脑电图报告返回DataTable
        public void m_mthGetEEGReportdtb(string P_fromDat, string P_toDat, string P_strdept, out DataTable p_dtbResult, string strFirstType, string strFirstValue, string strLastType, string strLastValue, bool flag)
        {
            long lngRes = proxy.Service.m_lngGetEEGReportDtb(P_fromDat, P_toDat, P_strdept, out p_dtbResult, strFirstType, strFirstValue, strLastType, strLastValue, flag);
        }
        #endregion
        #endregion

        #region 获取对应申请单的类型ID(脑电图）
        public void m_mthGetApplTypeIDRISEEGR(out string TypeID)
        {
            long lngRes = proxy.Service.m_mthGetApplTypeIDRISEEGR(out TypeID);
        }
        #endregion

        #region 获取心电图申请资料
        public long m_lngGetAllSyncInfoForInPatient(out clsCustom_SyncInfo[] p_objSyncInfoArr)
        {
            long lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetAllSyncInfo(out p_objSyncInfoArr);
            return lngRes;
        }
        #endregion

        #region 审核（EEG报告单）
        /// <summary>
        ///  审核（EEG报告单）
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

        #region 审核（TCD报告单）
        /// <summary>
        /// 审核（TCD报告单）
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

        #region 根据就诊卡号返回病人诊断信息
        /// <summary>
        /// 根据就诊卡号返回病人诊断信息
        /// </summary>
        /// <returns></returns>
        public long m_strDiagByCardId(string p_strCard, out string p_strResult)
        {
            p_strResult = "";
            long lngRes = proxy.Service.m_strDiagByCardId(p_strCard, out p_strResult);

            return lngRes;
        }
        #endregion

        #region 根据住院号获取病人信息
        /// <summary>
        /// 根据住院号获取病人信息
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
