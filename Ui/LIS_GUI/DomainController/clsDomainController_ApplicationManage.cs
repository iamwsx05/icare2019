using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_ApplicationManage ��ժҪ˵����
    /// </summary>
    public class clsDomainController_ApplicationManage : clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region �������뵥
        public long m_lngSendApplications(string[] p_strAppIDs)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSendApplictions(p_strAppIDs);
            }
            catch { }

            return lngRes;
        }
        #endregion

        #region �������������    xing.chen add
        public long m_lngBlankOutApplication(clsLisApplMainVO p_objApplMainVO, clsBlankOutApplicationVO p_objBlankOutInfo)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddBlankOutInfo(p_objApplMainVO, p_objBlankOutInfo);
            }
            catch { }

            return lngRes;
        }
        #endregion

        #region		����������ȷ�� xing.chen add
        public long m_lngCheckComfirmLogin(string p_strLoignName, string p_strLoginPwd, out bool blnLogin, out string strLoginMsg, out string p_strEmpID)
        {
            long lngRes = 0;
            blnLogin = false;
            strLoginMsg = "";
            p_strEmpID = "";

            string strEmpID;
            string strEmpPwd;
            try
            {
                lngRes = proxy.Service.m_lngFindEmpMsgByEmpNO(p_strLoignName, out strEmpID, out strEmpPwd);
                if (lngRes < 0)
                {
                    return -1;
                }

                if (strEmpID == null || strEmpID == "")
                {
                    blnLogin = false;
                    strLoginMsg = "���ʺŲ�����";
                }
                else if (strEmpPwd.Trim() != p_strLoginPwd.Trim())
                {
                    blnLogin = false;
                    strLoginMsg = "�������";
                }
                else
                {
                    blnLogin = true;
                    p_strEmpID = strEmpID;
                    strLoginMsg = "��ݺ˶Գɹ�";
                }
            }
            catch
            {
                blnLogin = false;
                strLoginMsg = "�˶��쳣";
            }

            return lngRes;
        }
        #endregion

        #region �������ݶ���
        #region Get

        public long m_lngGetReportObject(string p_strApplicationID, out clsReportObject p_objReportObject)
        {
            long lngRes = 0;
            p_objReportObject = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReportObject(p_strApplicationID, out p_objReportObject);
            }
            catch
            {
                lngRes = 0;
            }

            return lngRes;
        }

        #endregion

        #region Insert
        public long m_lngInsertReportObject(clsReportObject p_objReportObject)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertReportObject(p_objReportObject);
            }
            catch
            {
                lngRes = 0;
            }

            return lngRes;
        }
        #endregion

        #region Update
        public long m_lngUpdateReportObject(clsReportObject p_objReportObject)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateReportObject(p_objReportObject);
            }
            catch
            {
                lngRes = 0;
            }

            return lngRes;
        }
        #endregion

        #region Delete
        public long m_lngDeleteReportObject(string p_strApplicationID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteReportObject(p_strApplicationID);
            }
            catch
            {
                lngRes = 0;
            }

            return lngRes;
        }
        #endregion
        #endregion

        #region		��ȡ������Ϣ	xing.chen 2005.9.22
        public long m_lngGetPatientInfoVOList(string p_InHospitalID, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;

            try
            {
                lngRes = proxy.Service.m_lngFindPatientInfoByInpatientID(p_InHospitalID, out p_dtResult);
            }
            catch
            {
                lngRes = 0;
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ����������Ϣ		xing.chen add 2005/12/14
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="p_blnConfig"></param>
        /// <param name="p_strSetID"></param>
        /// <returns></returns>
        public long m_lngGetCollocate(out string p_strConfig, string p_strSetID)
        {
            p_strConfig = "";
            long lngRes = proxy.Service.m_lngGetCollocate(out p_strConfig, p_strSetID);
            return lngRes;
        }
        #endregion

        #region ��ϲ�ѯ(��������סԺ��)���뵥��Ϣ xing.chen add 2005/12/14
        /// <summary>
        /// ��ϲ�ѯ��ѯ���뵥��Ϣ
        /// </summary>
        /// <param name="p_objSchVO"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        public long m_lngGetAppInfoByConditionAndInHospitalNO(
            clsLISApplicationSchVO p_objSchVO, string p_strInHospitalNO,
            out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            p_objAppVOArr = null;
            lngRes = proxy.Service.m_lngGetAppInfoByConditionAndInHospitalNO(p_objSchVO, p_strInHospitalNO, out p_objAppVOArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 	����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ,����������(barcodeΪseq��ȡ��)��ȫ����Ϣ)	 xing.chen  2005/12/14
        /// <summary>
        /// ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ,����������(barcodeΪseq��ȡ��)��ȫ����Ϣ)
        /// </summary>
        /// <param name="applMain"></param>
        /// <param name="arrReports"></param>
        /// <param name="arrSamples"></param>
        /// <param name="arrCheckItems"></param>
        /// <param name="arrApplyUnits"></param>
        /// <param name="arrUnitItemRelations"></param>
        /// <returns></returns>
        public long m_lngAddNewAppAndSampleInfoWithBarcode(weCare.Core.Entity.clsLisApplMainVO p_objLisApplMainVO,
            weCare.Core.Entity.clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
            weCare.Core.Entity.clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            weCare.Core.Entity.clsLisApplMainVO objLisApplMainVO = null;
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppAndSampleInfoWithBarcode(p_objLisApplMainVO, out objLisApplMainVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            if (lngRes > 0 && objLisApplMainVO != null)
            {
                objLisApplMainVO.m_mthCopyTo(p_objLisApplMainVO);
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ���뵥���շ���Ϣ ���� 2005.08.03
        /// <summary>
        /// ��ѯ���뵥���շ���Ϣ
        /// </summary>
        /// <param name="p_strApplicationID">���뵥ID</param>
        /// <returns>0:��ѯʧ��;1:δ�շ�;2:���շ�</returns>
        public static long m_lngGetChargeState(string p_strApplicationID)
        {
            long lngRes = 0;

            try
            {
                bool blnRes = (new weCare.Proxy.ProxyBase()).Service.m_mthCheckIsCharge(p_strApplicationID, (int)ApplyOrigin.LIS);
                if (blnRes)
                    lngRes = 2;
                else
                    lngRes = 1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                lngRes = 0;
            }

            return lngRes;
        }
        #endregion
        #region �������뵥ID������Ӧ���������� ���� 2004.11.18
        /// <summary>
        /// �������뵥ID������Ӧ����������
        /// </summary>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strOringinDate"></param>
        /// <returns></returns>
        public long m_lngDeleteDeviceRelationByApplicationID(string p_strApplicationID, string p_strOringinDate)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDeleteDeviceRelationByApplicationID(p_strApplicationID, p_strOringinDate);

            return lngRes;
        }

        #endregion

        #region �޸����뵥������Ϣ����Ӧ�޸�������Ϣ ͯ�� 2004.11.16
        public long m_lngSetApplicationAndSamplePatientInfo(clsLisApplMainVO p_objApplVO)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetApplicationAndSamplePatientInfo(p_objApplVO);

            return lngRes;
        }
        #endregion

        #region ��ϲ�ѯ��ѯ�ѷ������뵥��������Ϣ ���� 2004.11.10
        /// <summary>
        /// ��ϲ�ѯ��ѯ�ѷ������뵥��������Ϣ
        /// </summary>
        /// <param name="p_intSampleStatus">1Ϊδ�ɼ�,2Ϊ�Ѳɼ�,0Ϊ����</param>
        /// <param name="p_strAppDept"></param>
        /// <param name="p_strFromDatApp"></param>
        /// <param name="p_strToDatApp"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        public long m_lngGetAppAndSampleInfo(int p_intSampleStatus, string p_strAppDept, string p_strFromDatApp,
                                                string p_strToDatApp, string p_strPatientName, string p_strPatientCardID, string p_strAcceptStatus, int p_intSampleBackeStatus, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAppAndSampleInfo(p_intSampleStatus, p_strAppDept, p_strFromDatApp, p_strToDatApp, p_strPatientName, p_strPatientCardID, p_strAcceptStatus, p_intSampleBackeStatus, out p_objAppVOArr);

            return lngRes;
        }
        #endregion

        #region [סԺ�����ɼ���ѯ]��ϲ�ѯ�ѷ������뵥��������ϢYMH 2006.9.26[BIH]
        /// <summary>
        /// [סԺ�����ɼ���ѯ]��ϲ�ѯ�ѷ������뵥��������Ϣ
        /// </summary>
        /// <param name="p_intSampleStatus">1Ϊδ�ɼ�,2Ϊ�Ѳɼ�,0Ϊ����</param>
        /// <param name="p_strAppDept"></param>
        /// <param name="p_strFromDatApp"></param>
        /// <param name="p_strToDatApp"></param>
        /// <param name="p_strHosipitalNO"></param>
        /// <param name="p_strPatientCardID"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        public long m_lngGetAppAndSampleInfo(int p_intSampleStatus, string p_strAppDept, string p_strFromDatApp,
                                             string p_strToDatApp, string p_strPatientName, string p_strPatientCardID, string p_strHosipitalNO, string bedNo, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAppAndSampleInfo(p_intSampleStatus, p_strAppDept, p_strFromDatApp, p_strToDatApp, p_strPatientName, p_strPatientCardID, p_strHosipitalNO, bedNo, out p_objAppVOArr);

            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ��ӡ���뵥��Ϣ ͯ�� 2004.11.08
        public long m_lngGetApplicationReportInfo(string p_strApplicationID, out clsLisApplyReportInfo_VO p_objResult)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetApplicationReportInfo(p_strApplicationID, out p_objResult);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ�õ����뵥��ϸ��Ϣ ���� 2004.10.18
        /// <summary>
        /// �������뵥ID��ѯ�õ����뵥��ϸ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strAppModifyDate"></param>
        /// <param name="p_objLISInfoVO"></param>
        /// <returns></returns>
        public long m_lngGetLISInfoByApplicationID(string p_strApplicationID, string p_strAppModifyDate, out clsLISInfoVO p_objLISInfoVO)
        {
            long lngRes = 0;
            p_objLISInfoVO = null;
            lngRes = proxy.Service.m_lngGetLISInfoByApplicationID(p_strApplicationID, p_strAppModifyDate, out p_objLISInfoVO);
            //			objAppSvc.Dispose();
            return lngRes;

        }

        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainController_ApplicationManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ����������ѯ������Ϣ
        public long m_lngGetPatientInfoByCondition(string p_strPatientInhosptalNO, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetPatientInfoByCondition(p_strPatientInhosptalNO, out p_dtbResult);

            return lngRes;
        }
        #endregion


        #region ��ϲ�ѯ��ѯ���뵥��Ϣ ���� 2004.06.21
        /// <summary>
        /// ��ϲ�ѯ��ѯ���뵥��Ϣ
        /// </summary>
        /// <param name="p_objSchVO"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        public long m_lngGetAppInfoByCondition(clsLISApplicationSchVO p_objSchVO, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            p_objAppVOArr = null;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppInfoByCondition(p_objSchVO, out p_objAppVOArr);
            return lngRes;
        }
        #endregion

        #region ����SampleID �� AppSampleGroup ���� 2004.06.26

        /// <summary>
        /// ����SampleID �� AppSampleGroup ���� 2004.06.26
        /// </summary>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strSampleID"></param>
        /// <returns></returns>
        public long m_lngUpdateAppSampleGroupSampleID(
            string p_strAppID, string p_strSampleID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngUpdateAppSampleGroupSampleID(p_strAppID, p_strSampleID);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ������ ���� 2004.07.23
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strOpID"></param>
        /// <returns></returns>
        public long m_lngDeleteApp(string p_strAppID, string p_strOpID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDeleteApp(p_strAppID, p_strOpID);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ȷ�ϱ��� ���� 2004.06.26
        /// <summary>
        /// ȷ�ϱ���,ͬʱȷ������
        /// </summary>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strConfirmerID"></param>
        /// <returns></returns>
        public long m_lngConfirmAppReport(
            string p_strAppID, string p_strReportID, string p_strConfirmerID, DateTime p_dtmConfirmDate)
        {
            string strConfirmDate = p_dtmConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngConfirmAppReport(p_strAppID, p_strReportID, p_strConfirmerID, strConfirmDate);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����������ڡ�����״̬��ϲ�ѯ���뵥��Ϣ ���� 2004.06.21
        /// <summary>
        /// �����������ڡ�����״̬��ϲ�ѯ���뵥��Ϣ
        /// </summary>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_blnSend"></param>
        /// <param name="p_blnUnSend"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetApplicationVOArrByCondition(string p_strFromDat, string p_strToDat, bool p_blnSend, bool p_blnUnSend, out clsLisApplMainVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            lngRes = proxy.Service.m_lngGetApplicationVOArrByCondition(p_strFromDat, p_strToDat, p_blnSend, p_blnUnSend, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݼ������ڡ��걾�š�����ID�Ͳ���������ϲ�ѯ��ѯ���뵥��Ϣ ͯ�� 2004.06.21
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strFromDat">������ʼ����</param>
        /// <param name="p_strToDat">������ֹ����</param>
        /// <param name="p_strDeviceModelID">�����ͺ�</param>
        /// <param name="p_strPatientName">��������</param>
        /// <param name="p_strSampleID">�걾��</param>
        /// <param name="p_objResultArr">��ѯ���VO</param>
        /// <returns></returns>
        public long m_lngGetApplicationVOArrByCondition(string p_strFromDat, string p_strToDat, string p_strDeviceID, string p_strPatientName,
            string p_strSampleID, out clsLisApplMainVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetApplicationVOArrByCondition(p_strFromDat, p_strToDat, p_strDeviceID, p_strPatientName, p_strSampleID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ���뵥��Ϣ ͯ�� 2004.06.21
        public long m_lngGetApplicationInfoByApplicationID(string p_strApplicationID, out clsLisApplMainVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetApplicationInfoByApplicationID(p_strApplicationID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ���뵥�µ����뵥Ԫ ͯ�� 2004.06.17
        public long m_lngGetAppApplyUnitVOByApplicationID(string p_strApplicationID, out clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppApplyUnitVOByApplicationID(p_strApplicationID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ���뵥�µļ�����Ŀ ͯ�� 2004.06.17
        public long m_lngGetAppCheckItemVOArr(string p_strApplicationID, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppCheckItemVOArr(p_strApplicationID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��������ID�ͱ�����ID��ѯ���뵥�µļ�����Ŀ ͯ�� 2004.06.17
        public long m_lngGetAppCheckItemVOArr(string p_strApplicationID, string p_strSampleGroupID, string p_strReportGroupID, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppCheckItemVOArr(p_strApplicationID, p_strSampleGroupID, p_strReportGroupID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ���뵥�µı걾�� ͯ�� 2004.06.17
        public long m_lngGetAppSampleGroupVOArr(string p_strApplicationID, out clsT_OPR_LIS_APP_SAMPLE_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppSampleGroupVOArr(p_strApplicationID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID�ͱ�����ID��ѯ�������뵥�µı걾�� ͯ�� 2004.06.17
        public long m_lngGetAppSampleGroupVOArr(string p_strApplicationID, string p_strReportGroupID, out clsT_OPR_LIS_APP_SAMPLE_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppSampleGroupVOArr(p_strApplicationID, p_strReportGroupID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥ID��ѯ���뵥�µı����� ͯ�� 2004.06.17
        public long m_lngGetAppReportVOArrByApplicationID(string p_strApplicationID, out clsT_OPR_LIS_APP_REPORT_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppReportVOArrByApplicationID(p_strApplicationID, out p_objResultArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region Ϊ�� T_OPR_LIS_APPLICATION ����,�޸�,ɾ�� �õķ��� ���� 2004.06.3
        /// <summary>
        /// Ϊ�� T_OPR_LIS_APPLICATION ����,�޸�,ɾ�� �õķ��� ���� 2004.06.3
        /// </summary>
        /// <param name="objApplMainVO"></param>
        /// <returns></returns>
        public long m_lngAddNewApplication(weCare.Core.Entity.clsLisApplMainVO objApplMainVO)
        {
            weCare.Core.Entity.clsLisApplMainVO objOutVO = null;
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppl(objApplMainVO, out objOutVO);

            if (lngRes > 0 && objOutVO != null)
            {
                objOutVO.m_mthCopyTo(objApplMainVO);
            }
            return lngRes;
        }
        #endregion

        #region [U]m_lngInsertAppReportRecord  Ϊ�� t_opr_lis_app_report ����,�޸�,ɾ�� ��¼ʱ�� ���� 2004.05.26

        /// <summary>
        /// Ϊ�� t_opr_lis_app_report ����,�޸�,ɾ�� ��¼ʱ�� 
        /// ���� 2004.05.26
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        public long m_lngInsertAppReportRecord(clsT_OPR_LIS_APP_REPORT_VO[] p_objRecordVOArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngInsertAppReportRecord(p_objRecordVOArr);
            //			objAppSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region m_lngAddNewAppSampleGroup Ϊ�� T_OPR_LIS_APP_SAMPLE ���� ��¼ʱ�� ���� 2004.05.26
        /// <summary>
        /// Ϊ�� T_OPR_LIS_APP_SAMPLE ������¼ʱ�� ���� 2004.05.26
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>

        public long m_lngAddNewAppSampleGroup(clsT_OPR_LIS_APP_SAMPLE_VO[] p_objRecordVOArr)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppSampleGroup(p_objRecordVOArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region m_lngAddNewAppApplyUint Ϊ�� T_OPR_LIS_APP_APPLY_UNIT ������¼ʱ�� ���� 2004.05.26
        /// <summary>
        /// Ϊ�� T_OPR_LIS_APP_APPLY_UNIT ������¼ʱ�� ���� 2004.05.26
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>

        public long m_lngAddNewAppApplyUint(clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objRecordVOArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngAddNewAppApplyUint(p_objRecordVOArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region m_lngAddNewAppCheckItem Ϊ�� T_OPR_LIS_APP_CHECK_ITEM ������¼ʱ�� ���� 2004.05.26
        /// <summary>
        /// Ϊ�� T_OPR_LIS_APP_CHECK_ITEM ������¼ʱ�� ���� 2004.05.26
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>

        public long m_lngAddNewAppCheckItem(clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objRecordVOArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppCheckItem(p_objRecordVOArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ���ڵ�ȫ����Ϣ) ���� 2004.05.26
        /// <summary>
        /// ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ���ڵ�ȫ����Ϣ)
        /// </summary>
        /// <param name="applMain"></param>
        /// <param name="arrReports"></param>
        /// <param name="arrSamples"></param>
        /// <param name="arrCheckItems"></param>
        /// <param name="arrUnitItemRelations"></param>
        /// <param name="arrApplyUnits"></param>
        /// <returns></returns>
        public long m_lngAddNewAppInfo(weCare.Core.Entity.clsLisApplMainVO p_objLisApplMainVO,
            weCare.Core.Entity.clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
            weCare.Core.Entity.clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            weCare.Core.Entity.clsLisApplMainVO objLisApplMainVO = null;
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppInfo(p_objLisApplMainVO, out objLisApplMainVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            if (lngRes > 0 && objLisApplMainVO != null)
            {
                objLisApplMainVO.m_mthCopyTo(p_objLisApplMainVO);
            }
            return lngRes;
        }
        #endregion

        #region ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ,���������ڵ�ȫ����Ϣ) ���� 2004.05.26
        /// <summary>
        /// ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ,���������ڵ�ȫ����Ϣ)
        /// </summary>
        /// <param name="applMain"></param>
        /// <param name="arrReports"></param>
        /// <param name="arrSamples"></param>
        /// <param name="arrCheckItems"></param>
        /// <param name="arrUnitItemRelations"></param>
        /// <param name="arrApplyUnits"></param>
        /// <returns></returns>
        public long m_lngAddNewAppAndSampleInfo(ref weCare.Core.Entity.clsLisApplMainVO p_objLisApplMainVO,
             ref weCare.Core.Entity.clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr,
            ref weCare.Core.Entity.clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
            ref weCare.Core.Entity.clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr,
            ref weCare.Core.Entity.clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
            ref weCare.Core.Entity.clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            weCare.Core.Entity.clsLisApplMainVO objLisApplMainVO = null;
            long lngRes = 0;
            //lngRes = proxy.Service.m_lngAddNewAppAndSampleInfo(p_objLisApplMainVO,out objLisApplMainVO, p_objReportArr,p_objAppSampleArr,	p_objAppItemArr,p_objAppUnitArr,p_objAppUnitItemArr);
            //change by wjqin(07-4-28) ������㱬������� �����˸�ref 
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppAndSampleInfoNew(p_objLisApplMainVO, out objLisApplMainVO, ref p_objReportArr, ref p_objAppSampleArr, ref p_objAppItemArr, ref p_objAppUnitArr, ref p_objAppUnitItemArr);
            /*<=======================*/
            if (lngRes > 0 && objLisApplMainVO != null)
            {
                objLisApplMainVO.m_mthCopyTo(p_objLisApplMainVO);
            }
            return lngRes;
        }
        #endregion

        #region ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ���ڵ�ȫ����Ϣ) ���� 2004.05.26
        /// <summary>
        /// ����-���µļ���������Ϣ(����������,���뵥Ԫ,������,������Ŀ���ڵ�ȫ����Ϣ)
        /// </summary>
        /// <param name="applMain"></param>
        /// <param name="arrReports"></param>
        /// <param name="arrSamples"></param>
        /// <param name="arrCheckItems"></param>
        /// <param name="arrUnitItemRelations"></param>
        /// <param name="arrApplyUnits"></param>
        /// <returns></returns>
        public long m_lngAddAppInfoWithoutReceive(clsLisApplMainVO p_objLisApplMainVO,
                                        clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr,
                                        clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
                                        clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr,
                                        clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
                                        clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            clsLisApplMainVO objLisApplMainVO = null;
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewAppAndSampleInfoWithoutReceive(p_objLisApplMainVO, out objLisApplMainVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);

                if (lngRes > 0 && objLisApplMainVO != null)
                {
                    objLisApplMainVO.m_mthCopyTo(p_objLisApplMainVO);
                }
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }

        #endregion

        #region  �޸�-�����������Ϣ(����������,���뵥Ԫ,������,������Ŀ���ڵ�ȫ����Ϣ) ���� 2004.06.30
        /// <summary>
        /// �޸�-�����������Ϣ(����������,���뵥Ԫ,������,������Ŀ���ڵ�ȫ����Ϣ)
        /// </summary>
        /// <param name="applMain"></param>
        /// <param name="arrReports"></param>
        /// <param name="arrSamples"></param>
        /// <param name="arrCheckItems"></param>
        /// <param name="arrApplyUnits"></param>
        /// <param name="arrUnitItemRelations"></param>
        /// <returns></returns>
        public long m_lngModifyAppInfo(weCare.Core.Entity.clsLisApplMainVO p_objLisApplMainVO,
            weCare.Core.Entity.clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr,
            weCare.Core.Entity.clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
            weCare.Core.Entity.clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngModifyAppInfo(p_objLisApplMainVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������ǼǱ�ʹ��״̬Ϊ����
        /// <summary>
        /// �������ǼǱ�ʹ��״̬Ϊ����
        /// </summary>
        /// <param name="strApplicationID"></param>
        /// <returns></returns>
        public long m_lngUpdatePEReg(string strApplicationID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdatePEReg(strApplicationID);
            //			objAppSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="p_strAppID"></param>
        /// <returns></returns>
        public long m_lngUpdateVoidApply(string p_strAppID, string p_strOperatorID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngUpdateVoidApply(p_strAppID, p_strOperatorID);
            return lngRes;
        }
        #endregion

        #region ͨ�����뵥���ж��Ƿ����
        /// <summary>
        /// ͨ�����뵥���ж��Ƿ����
        /// </summary>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_dtResult"></param>
        /// <param name="p_dtUnitResult"></param>
        /// <returns></returns>
        public long m_lnqQueryConfirmReport(string p_strApplicationID, out DataTable p_dtResult, out DataTable p_dtUnitResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lnqQueryConfirmReport(p_strApplicationID, out p_dtResult, out p_dtUnitResult);
            return lngRes;
        }
        #endregion

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="p_strParmCode"></param>
        /// <param name="p_strParmValue"></param>
        /// <returns></returns>
        public long m_lngGetSysParm(string p_strParmCode, out string p_strParmValue)
        {
            p_strParmValue = "";
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetSysParm(p_strParmCode, out p_strParmValue);
            return lngRes;
        }
        #endregion

        #region �޸�t_opr_lis_app_report ��Ĵ�ӡ����
        /// <summary>
        /// �޸�t_opr_lis_app_report ��Ĵ�ӡ����
        /// </summary>
        /// <param name="p_strApplicaionID"></param>
        /// <returns></returns>
        public long m_lngUpdatePrinctTime(string p_strApplicaionID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngUpdatePrinctTime(p_strApplicaionID);
            return lngRes;
        }
        #endregion

        #region ȡ�����
        /// <summary>
        /// ȡ�����
        /// </summary>
        /// <param name="p_strAppID">���뵥ID</param>
        /// <param name="p_strOperatorID">����Ա��ID</param>
        /// <returns>����0�ɹ�������ʧ��</returns>
        public long m_lngCancelConfimReport(string p_strAppID, string p_strOperatorID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngCancelConfimReport(p_strAppID, p_strOperatorID);
            return lngRes;
        }
        #endregion

        #region ��ȡ�������
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryCheckCategory(out DataTable p_dtResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngQueryCheckCategory(out p_dtResult);
            return lngRes;
        }
        #endregion

        #region ��ѯ������Ϣ
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="p_objSchVO"></param>
        /// <param name="p_strCheckCategory"></param>
        /// <param name="p_objAppVOArr"></param>
        /// <returns></returns>
        public long m_lngGetAppInfoByModifDate(clsLISApplicationSchVO p_objSchVO, string p_strCheckCategory, out clsLisApplMainVO[] p_objAppVOArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAppInfoByModifDate(p_objSchVO, p_strCheckCategory, out p_objAppVOArr);
            return lngRes;
        }
        #endregion

        #region ��ȡ���������Ŀ
        /// <summary>
        /// ��ȡ���������Ŀ
        /// </summary>
        /// <param name="regNo"></param>
        /// <returns></returns>
        public DataTable GetAppItem(string regNo)
        {
            return (new weCare.Proxy.ProxyLis01()).Service.GetAppItem(regNo);

        }
        #endregion

        #region ����-���ӿ�

        #region ���-��ȡ���������Ϣ
        /// <summary>
        /// ���-��ȡ���������Ϣ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public DataTable GetPeSample(string barCode)
        {
            return proxy.Service.GetPeSample(barCode);

        }
        #endregion

        #region ���-У���Ƿ��Ѵ��
        /// <summary>
        /// ���-У���Ƿ��Ѵ��
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public bool SamplePackIsExist(string barCode)
        {
            return proxy.Service.SamplePackIsExist(barCode);

        }
        #endregion

        #region ���-����
        /// <summary>
        /// ���-����
        /// </summary>
        /// <param name="lstSamplePack"></param>
        /// <param name="lstSamplePackDet"></param>
        /// <param name="packId"></param>
        /// <returns></returns>
        public int SamplePackInsert(List<EntitySamplePack> lstSamplePack, List<EntitySamplePackDetail> lstSamplePackDet, int bizType, out decimal packId)
        {
            return proxy.Service.SamplePackInsert(lstSamplePack, lstSamplePackDet, bizType, out packId);

        }
        #endregion

        #region ���-ɾ��
        /// <summary>
        /// ���-ɾ��
        /// </summary>
        /// <param name="lstBarCode"></param>
        /// <returns></returns>
        public int SamplePackDel(List<string> lstBarCode)
        {
            return proxy.Service.SamplePackDel(lstBarCode);

        }
        #endregion

        #region ���-��ѯ
        /// <summary>
        /// ���-��ѯ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public List<EntitySamplePack> SamplePackQuery(string barCode, int bizType)
        {
            return proxy.Service.SamplePackQuery(barCode, bizType);

        }
        #endregion

        #region ���-����
        /// <summary>
        /// ���-����
        /// </summary>
        /// <param name="sampleVo"></param>
        /// <returns></returns>
        public bool SamplePackCheck(EntitySamplePack sampleVo)
        {
            return proxy.Service.SamplePackCheck(sampleVo);

        }
        #endregion

        #region bak-����-���ӿ�
        /// <summary>
        /// bak-����-���ӿ� 
        /// </summary>
        /// <param name="patVo"></param>
        /// <returns></returns>
        public bool PEItf(clsLisApplMainVO patVo, DataTable dtPe, out List<clsLisApplMainVO> lstApp)
        {
            return proxy.Service.PEItf(patVo, dtPe, out lstApp);

        }
        #endregion

        #region סԺ������Ŀ��ѯ
        /// <summary>
        /// סԺ������Ŀ��ѯ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public DataTable GetIpSample(string barCode)
        {
            return proxy.Service.GetIpSample(barCode);

        }
        #endregion

        #endregion

        #region ���������ѯ
        /// <summary>
        /// ���������ѯ
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ipNo"></param>
        /// <returns></returns>
        public DataTable QueryAreaReport(string deptId, string startDate, string endDate, string ipNo)
        {
            return proxy.Service.QueryAreaReport(deptId, startDate, endDate, ipNo);

        }
        #endregion

        #region ͨ�����������뵥��
        /// <summary>
        /// ͨ�����������뵥��
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public string GetApplicationIdByBarcode(string barCode)
        {
            return proxy.Service.GetApplicationIdByBarcode(barCode);

        }
        #endregion

        #region ���-��ѯ��ʱ��
        /// <summary>
        /// ���-��ѯ��ʱ��
        /// </summary>
        /// <param name="floorNo"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public bool SamplePackQueryTemp(decimal floorNo, out string barCode)
        {
            return proxy.Service.SamplePackQueryTemp(floorNo, out barCode);

        }
        #endregion

        #region ͨ�����뵥���ұ걾��Ϣ(΢��)
        /// <summary>
        /// ͨ�����뵥���ұ걾��Ϣ(΢��)
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public DataTable GetWechatSampleInfo(string applicationId)
        {
            return proxy.Service.GetWechatSampleInfo(applicationId);

        }
        #endregion

        #region ����Ƿ��(΢��)
        /// <summary>
        /// ΢�ż���Ƿ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool IsWechatBanding(string cardNo)
        {
            return proxy.Service.IsWechatBanding(cardNo);

        }
        #endregion

        #region ��ȡ�걾����ԭ��
        /// <summary>
        /// ��ȡ�걾����ԭ��
        /// </summary>
        /// <returns></returns>
        public DataTable GetRejectReason()
        {
            return proxy.Service.GetRejectReason();

        }
        #endregion

        #region ͨ�������ŷ������ƿ���
        /// <summary>
        /// ͨ�������ŷ������ƿ���
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public string GetCardNoByRecipeId(string recipeId)
        {
            return proxy.Service.GetCardNoByRecipeId(recipeId);

        }
        #endregion

        #region ��ȡҽ���ֵ�.���뵥Ԫ.��������
        /// <summary>
        /// ��ȡҽ���ֵ�.���뵥Ԫ.��������
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="applyUnitId"></param>
        /// <returns></returns>
        public DataTable GetOrderDicSamplingTimes(string orderId, string applyUnitId)
        {
            return proxy.Service.GetOrderDicSamplingTimes(orderId, applyUnitId);

        }
        #endregion

        #region ��ȡ���뵥�����ˡ������
        /// <summary>
        /// ��ȡ���뵥�����ˡ������
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public DataTable GetApplicationOperInfo(string applicationId)
        {
            return proxy.Service.GetApplicationOperInfo(applicationId);

        }
        #endregion

        #region ��ѯ������ĿID��ʷֵ
        /// <summary>
        /// ��ѯ������ĿID��ʷֵ
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="itemIdArr"></param>
        /// <returns></returns>
        public DataTable GetCheckItemHistoryValue(string applicationId, string itemIdArr)
        {
            return proxy.Service.GetCheckItemHistoryValue(applicationId, itemIdArr);

        }
        #endregion

        #region ��ѯ:ҽ��->������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// <summary>
        /// ��ѯ:ҽ��->������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetOrderDicLisApplyUnitByOrderId(string orderId)
        {
            return proxy.Service.GetOrderDicLisApplyUnitByOrderId(orderId);

        }
        #endregion

        #region ��ѯ:������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// <summary>
        /// ��ѯ:������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// </summary>
        /// <param name="orderDicId"></param>
        /// <returns></returns>
        public DataTable GetOrderDicLisApplyUnit(string orderDicId)
        {
            return proxy.Service.GetOrderDicLisApplyUnit(orderDicId);

        }
        #endregion

        #region �޸ļ���״̬
        /// <summary>
        /// �޸ļ���״̬
        /// </summary>
        /// <param name="appMainVo"></param>
        /// <param name="emerStatus"></param>
        /// <returns></returns>
        public int UpdateEmergencyStatus(clsLisApplMainVO appMainVo)
        {
            return (new weCare.Proxy.ProxyLis01()).Service.UpdateEmergencyStatus(appMainVo);

        }
        #endregion

        #region �������뵥ԪID��ȡ�걾
        /// <summary>
        /// �������뵥ԪID��ȡ�걾
        /// </summary>
        /// <param name="appUnitId"></param>
        /// <returns></returns>
        public DataTable GetSampleInfo(string appUnitId)
        {
            return (new weCare.Proxy.ProxyLis01()).Service.GetSampleInfo(appUnitId);

        }
        #endregion
    }
}
