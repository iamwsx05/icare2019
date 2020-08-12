using System;
using System.Data;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.RIS
{
    /// <summary>
    /// clsDomainController_RISCardiogramManage ��ժҪ˵����
    /// Alex 2004-5-27
    /// </summary>
    public class clsDomainController_RISCardiogramManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyRIS proxy
        {
            get
            {
                return new weCare.Proxy.ProxyRIS();
            }
        }

        #region �ĵ�ͼ����
        #region ����ĵ�ͼ����
        /// <summary>
        /// ����ĵ�ͼ����
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objResult"></param>
        public long m_lngDoAddNewCardiogramReport(out string p_strRecordID, clsRIS_CardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewCardiogramReport(out p_strRecordID, p_objResult);
            return lngRes;
            //			objSvc.Dispose();
        }
        #endregion
        #region ��ȡ��Ӧ���뵥������ID(�ĵ�ͼ��
        /// <summary>
        /// ��ȡ��Ӧ���뵥������ID(�ĵ�ͼ��
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public long m_mthGetApplTypeID(out string TypeID)
        {
            long lngRes = proxy.Service.m_mthGetApplTypeID(out TypeID);
            return lngRes;
        }
        #endregion

        #region ����ĵ�ͼ����
        /// <summary>
        /// ����ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResultArr"></param>
        public void m_mthGetCardiogramReportArr(out clsRIS_CardiogramReport_VO[] p_objResultArr)
        {
            long lngRes = proxy.Service.m_lngGetCardiogramReportArr(out p_objResultArr);

        }
        #endregion

        #region ����˶��ĵ�ͼ
        /// <summary>
        /// ����˶��ĵ�ͼ
        /// </summary>
        /// <param name="p_objResultArrSport"></param>
        public void m_lngGetSportReportArr(out clsafmt_report_VO[] p_objResultArrSport)
        {
            long lngRes = proxy.Service.m_lngGetSportReportArr(out p_objResultArrSport);

        }
        #endregion

        #region ����ĵ�ͼ����
        /// <summary>
        /// ����ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResultArr"></param>
        public void m_mthGetCardiogramReportByConditionArr(out clsRIS_CardiogramReport_VO[] p_objResultArr, Hashtable p_hasQueryCondiction)
        {
            p_objResultArr = null;
            //com.digitalwave.iCare.middletier.RIS.clsCardiogramManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.RIS.clsCardiogramManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.RIS.clsCardiogramManageSvc));

            //long lngRes = proxy.Service.m_lngGetCardiogramReportArr(p_hasQueryCondiction, out p_objResultArr);
            //objSvc.Dispose();
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
        public long m_lngDoModifyCardiogramReport(clsRIS_CardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngModifyCardiogramReport(p_objResult);

            return lngRes;
        }
        #endregion

        #region ɾ���ĵ�ͼ����
        /// <summary>
        /// ɾ���ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoDeleteCardiogramReport(clsRIS_CardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngDeleteCardiogramReport(p_objResult);

            return lngRes;
        }
        #endregion

        #region ����������ϲ�ѯ�ĵ�ͼ���� ͯ�� 2004.06.22
        public long m_lngGetCardiogramReportByCondition(string p_strFromDat, string p_strToDat, string p_strPatientNo, string p_strInPatientNo,
            string p_strPatientName, string p_strDept, string p_strReportNo, string strIsSpecail, string strReporter, out clsRIS_CardiogramReport_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetCardiogramReportByCondition(p_strFromDat, p_strToDat, p_strPatientNo, p_strInPatientNo, p_strPatientName,
                p_strDept, p_strReportNo, strIsSpecail, strReporter, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ����ĵ�ͼ���淵��DataTable
        public void m_mthGetCardiogramReportdtb(string P_fromDat, string P_toDat, string P_strdept, string strDiagnoses, out DataTable p_dtbResult)
        {
            long lngRes = proxy.Service.m_lngGetCardiogramdbt(P_fromDat, P_toDat, P_strdept, strDiagnoses, out p_dtbResult);

        }
        #endregion

        #endregion

        #region ��̬�ĵ�ͼ����
        #region ��Ӷ�̬�ĵ�ͼ����
        /// <summary>
        /// ��Ӷ�̬�ĵ�ͼ����
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objResult"></param>
        public long m_lngDoAddNewDCardiogramReport(out string p_strRecordID, clsRIS_DCardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewDCardiogramReport(out p_strRecordID, p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ö�̬�ĵ�ͼ����
        /// <summary>
        /// ��ö�̬�ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResultArr"></param>
        public void m_mthGetDCardiogramReportArr(out clsRIS_DCardiogramReport_VO[] p_objResultArr)
        {
            long lngRes = proxy.Service.m_lngGetDCardiogramReportArr(out p_objResultArr);

        }
        #endregion

        #region ��ö�̬�ĵ�ͼ����ByID
        /// <summary>
        /// ��ö�̬�ĵ�ͼ����ByID
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_objResult"></param>
        public void m_mthGetDCardiogramReportByID(string p_strID, out clsRIS_DCardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngGetDCardiogramReportByID(p_strID, out p_objResult);

        }
        #endregion

        #region �޸Ķ�̬�ĵ�ͼ����
        /// <summary>
        /// �޸Ķ�̬�ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoModifyDCardiogramReport(clsRIS_DCardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngModifyDCardiogramReport(p_objResult);

            return lngRes;

        }
        #endregion

        #region ɾ����̬�ĵ�ͼ����
        /// <summary>
        /// ɾ����̬�ĵ�ͼ����
        /// </summary>
        /// <param name="p_objResult"></param>
        public long m_lngDoDeleteDCardiogramReport(clsRIS_DCardiogramReport_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngDeleteDCardiogramReport(p_objResult);

            return lngRes;

        }
        #endregion

        #region ����������ϲ�ѯ��̬�ĵ�ͼ���� ͯ�� 2004.06.23
        public long m_lngGetDCardiogramReportByCondition(string p_strFromDat, string p_strToDat, string p_strPatientNo, string p_strInPatientNo,
            string p_strPatientName, string p_strDept, string p_strReportNo, string strIsSpecail, string strReporter, out clsRIS_DCardiogramReport_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetDCardiogramReportByCondition(p_strFromDat, p_strToDat, p_strPatientNo, p_strInPatientNo, p_strPatientName,
               p_strDept, p_strReportNo, strIsSpecail, strReporter, out p_objResultArr);

            return lngRes;
        }
        #endregion
        #region ����������ϲ�ѯ��˶��ĵ�ͼ����
        /// <summary>
        /// ����������ϲ�ѯ��˶��ĵ�ͼ����
        /// </summary>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_strPatientNo"></param>
        /// <param name="p_strInPatientNo"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strDept"></param>
        /// <param name="p_strReportNo"></param>
        /// <param name="strIsSpecail"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetSportReportByCondition(string p_strFromDat, string p_strToDat, string p_strPatientNo, string p_strInPatientNo,
            string p_strPatientName, string p_strDept, string p_strReportNo, string strIsSpecail, string strReporter, out clsafmt_report_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetSportReportByCondition(p_strFromDat, p_strToDat, p_strPatientNo, p_strInPatientNo, p_strPatientName,
               p_strDept, p_strReportNo, strIsSpecail, strReporter, out p_objResultArr);

            return lngRes;
        }
        #endregion
        #region ����ĵ�ͼ���淵��DataTable
        public void m_mthGetDnmCardiogramReportdtb(string P_fromDat, string P_toDat, string P_strdept, string strDiagnoses, out DataTable p_dtbResult)
        {
            long lngRes = proxy.Service.m_lngGetDnmCardiogramdbt(P_fromDat, P_toDat, P_strdept, strDiagnoses, out p_dtbResult);

        }
        #endregion

        #endregion

        #region ����ĵ�ͼ����
        /// <summary>
        /// ����ĵ�ͼ����m_lngDeleteSportReport
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objResult"></param>
        public long m_lngDoAddNewSportReport(out string p_strRecordID, clsafmt_report_VO p_objResult)
        {
            long lngRes = proxy.Service.m_lngAddNewSportReport(out p_strRecordID, p_objResult);

            return lngRes;
        }
        #endregion

        #region �޸��ĵ�ͼ���˶�ƽ���˶�ʵ�鱨�棩
        /// <summary>
        /// �޸��ĵ�ͼ���˶�ƽ���˶�ʵ�鱨�棩
        /// </summary>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>

        public long m_lngModifySportReport(clsafmt_report_VO p_objRecord)
        {
            long lngRes = proxy.Service.m_lngModifySportReport(p_objRecord);

            return lngRes;

        }
        #endregion

        #region ɾ���ĵ�ͼ����
        /// <summary>
        /// ɾ���ĵ�ͼ����
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objResult"></param>
        public long m_lngDeleteSportReport(string p_objRecordID)
        {
            long lngRes = proxy.Service.m_lngDeleteSportReport(p_objRecordID);

            return lngRes;
        }
        #endregion

        #region ��ˣ��˶�ƽ���˶�ʵ�鱨�棩
        /// <summary>
        /// ��ˣ��˶�ƽ���˶�ʵ�鱨�棩
        /// </summary>
        /// <param name="p_objRecordID"></param>
        /// <returns></returns>
        public long m_lngDeleteSportReportEmp(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = proxy.Service.m_lngDeleteSportReportEmp(p_objRecordID, strEmpID, strEmpName);

            return lngRes;

        }
        #endregion
        #region ��ˣ���̬�ĵ�ͼʵ�鱨�棩
        /// <summary>
        /// ��ˣ���̬�ĵ�ͼʵ�鱨�棩
        /// </summary>
        /// <param name="p_objRecordID"></param>
        /// <returns></returns>
        public long m_lngConfigDmnCardiogramReport(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = proxy.Service.m_lngConfigDmnCardiogramReport(p_objRecordID, strEmpID, strEmpName);

            return lngRes;

        }
        #endregion

        #region ��ˣ��ĵ�ͼʵ�鱨�棩
        /// <summary>
        /// ��ˣ��˶�ƽ���˶�ʵ�鱨�棩
        /// </summary>
        /// <param name="p_objRecordID"></param>
        /// <returns></returns>
        public long m_lngConfigCardiogramReport(string p_objRecordID, string strEmpID, string strEmpName)
        {
            long lngRes = proxy.Service.m_lngConfigCardiogramReport(p_objRecordID, strEmpID, strEmpName);

            return lngRes;

        }
        #endregion

        #region ���ݿ��ż�����������
        /// <summary>
        /// ���ݿ��ż�����������
        /// </summary>
        /// <param name="carID">����</param>
        /// <param name="tbPat">���ز�������</param>
        /// <returns></returns>
        public long m_lngGetPat(string carID, out DataTable tbPat)
        {
            long lngRes = proxy.Service.m_lngGetPat(carID, out tbPat);

            return lngRes;
        }
        #endregion

        #region ����סԺ�Ż�ȡ��������
        public long m_lngGetPatByInPatientID(string strInpatientID, out DataTable dtResult)
        {
            long lngRes = proxy.Service.m_lngGetPatByInPatientID(strInpatientID, out dtResult);
            return lngRes;
        }


        /// <summary>
        /// ����סԺ�Ż�ȡ��������
        /// </summary>
        /// <param name="m_strBihNo"></param>
        /// <param name="m_objDatable"></param>
        /// <returns></returns>
        public long m_lngGetPatientInfo(string m_strBihNo, out DataTable m_objDatable)
        {
            long lngRes = proxy.Service.m_lngGetPatientInfo(m_strBihNo, out m_objDatable);

            return lngRes;
        }
        #endregion

        #region ��ȡ�ĵ�ͼ��������
        public long m_lngGetAllSyncInfoForInPatient(out clsCustom_SyncInfo[] p_objSyncInfoArr)
        {
            long lngRes = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetAllSyncInfo(out p_objSyncInfoArr);
            return lngRes;
            //			objSvc.Dispose();
        }
        #endregion

        #region ��t_sys_setting ��setstatus_int
        /// <summary>
        ///  ��t_sys_setting ��setstatus_int
        /// </summary>
        /// <param name="p_strsetid_chr"></param>
        /// <param name="p_strModuledid"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_strGetsetstatusFromt_sys_setting(string p_strsetid_chr, string p_strModuledid, out string p_strResult)
        {
            p_strResult = "";
            long lngRes = proxy.Service.m_strGetsetstatusFromt_sys_setting(p_strsetid_chr, p_strModuledid, out p_strResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="dtDept"></param>
        /// <returns></returns>
        public long m_mthGetDEPTDESC(out DataTable dtDept)
        {
            long lngRes = proxy.Service.m_mthGetDEPTDESC(out dtDept);
            return lngRes;
            //			objSvc.Dispose();
        }
        #endregion
        public void m_mthUpdateApply(string p_strApplyID)
        {
            proxy.Service.m_mthUpdateApply(p_strApplyID);

        }

    }
}
