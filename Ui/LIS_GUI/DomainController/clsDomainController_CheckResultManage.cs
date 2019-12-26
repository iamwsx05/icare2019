using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.iCare.common;//iCareData.dll

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_CheckResultManage ��ժҪ˵����
    /// ���� 2004.05.26
    /// </summary>
    public class clsDomainController_CheckResultManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainController_CheckResultManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ���ݼ������ں�������Ų�ѯDeviceResultLog ͯ�� 2004.11.08
        /// <summary>
        /// ���ݼ������ں�������Ų�ѯDeviceResultLog
        /// </summary>
        /// <param name="p_strCheckDatFrom"></param>
        /// <param name="p_strCheckDatTo"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceResultLogByCondition(string p_strCheckDatFrom, string p_strCheckDatTo,
            string p_strDeviceID, out clsResultLogVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceResultLogByCondition(p_strCheckDatFrom, p_strCheckDatTo, p_strDeviceID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������ŵõ���Ӧ�������������� ���� 2004.10.22
        /// <summary>
        /// ��������ŵõ���Ӧ��������������
        /// </summary>
        /// <param name="p_strAppID"></param>
        /// <param name="p_objDRVOArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceRelationByAppID(
            string p_strAppID, out clsT_LIS_DeviceRelationVO[] p_objDRVOArr)
        {
            long lngRes = 0;
            p_objDRVOArr = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceRelationByAppID(p_strAppID, out p_objDRVOArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������������������־ ���� 2004.08.26
        /// <summary>
        /// ������������������־
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_intImportReq"></param>
        /// <returns></returns>
        public long m_lngSetDeviceSamplesRecheck(
            string p_strDeviceID, int p_intImportReq)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetDeviceSamplesRecheck(p_strDeviceID, p_intImportReq);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion


        #region ����Imp_Req_int������ID��ѯ�걾�б� ͯ�� 2004.08.20
        public long m_lngGetDeviceSampleListByCondition(string p_strImpReq, string p_strDeviceID, out clsResultLogVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceSampleListByCondition(p_strImpReq, p_strDeviceID, out p_objResultArr);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region �����ںϺ����������������־��Ϣ ͯ�� 2004.08.16
        public long m_lngAddNewDeviceCheckResultArrANDLog(clsDeviceReslutVO[] p_objDeviceResultArr, clsResultLogVO p_objResultLog)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewDeviceCheckResultArrANDLog(p_objDeviceResultArr, p_objResultLog);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region �������������ţ�����ID�ͼ���ʱ���ѯ�걾�б� ͯ�� 2004.08.16
        public long m_lngGetDeviceSampleListByCondition(string p_strDeviceSampleID, string p_strDeviceID, string p_strCheckDat,
            out clsResultLogVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceSampleListByCondition(p_strDeviceSampleID, p_strDeviceID, p_strCheckDat, out p_objResultArr);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region �������뵥�źͱ�����Ų�ѯ������ӡ��Ϣ�б�
        public long m_lngGetLisBatchReportDetailByCondition(clsLisBatchReportList_VO[] p_objReportList, out clsLisBatchReportDetail_VO[] p_objResultArr)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetLisBatchReportDetailByCondition(p_objReportList, out p_objResultArr);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ѯ������ӡ�ı��浥�б�
        public long m_lngGetLisBatchReportListByCondition(string p_strFromSampleID, string p_strToSampleID, string p_strFromConfirmDat,
            string p_strToConfirmDat, string p_strReportGroupID, string p_strPatientType, out clsLisBatchReportList_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetLisBatchReportListByCondition(p_strFromSampleID, p_strToSampleID, p_strFromConfirmDat, p_strToConfirmDat,
                          p_strReportGroupID, p_strPatientType, out p_objResultArr);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����T_OPR_LIS_RESULT_IMPORT_REQ��״̬ ͯ�� 2004.07.26
        public long m_lngSetResultImportReqStatus(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDat, string p_strStatus)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetResultImportReqStatus(p_strDeviceID, p_strDeviceSampleID, p_strCheckDat, p_strStatus);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ѯT_OPR_LIS_RESULT_IMPORT_REQ�����Ϣ ͯ�� 2004.07.23
        public long m_lngGetResultImportReqByCondition(string p_strDeviceID, string p_strCheckDatFrom, string p_strCheckDatTo,
            out clsLisResultImportReq_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetResultImportReqByCondition(p_strDeviceID, p_strCheckDatFrom, p_strCheckDatTo, out p_objResultArr);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ���±�T_OPR_LIS_RESULT_IMPORT_REQ����Ϣ ͯ�� 2004.07.23
        public long m_lngSetResultImportReq(clsLisResultImportReq_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetResultImportReq(p_objRecord);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ��������ָ���λ�� ͯ�� 2004.07.23
        public long m_lngSetResultImportReqEndPoint(clsLisResultImportReq_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetResultImportReqEndPoint(p_objRecord);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ѯ������ӡ���浥��Ϣ ͯ�� 2004.07.22
        public long m_lngGetBatchReportDataByCondition(string p_strFromSampleID, string p_strToSampleID, string p_strFromConfirmDat,
            string p_strToConfirmDat, string p_strReportGroupID, out clsLisBatchReport_VO[] p_objResultArr)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetBatchReportDataByCondition(p_strFromSampleID, p_strToSampleID, p_strFromConfirmDat, p_strToConfirmDat,
               p_strReportGroupID, out p_objResultArr);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ��ѯ�õ�  CheckResultVO ���� 2004.06.04
        /// <summary>
        /// ��ѯ�õ�  CheckResultVO
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objResultVO"></param>
        /// <returns></returns>

        public long m_lngGetCheckResultVO(string p_strSampleID,
            string p_strCheckItemID, out clsCheckResult_VO p_objResultVO)
        {
            long lngRes = 0;
            p_objResultVO = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetCheckResultVO(p_strSampleID, p_strCheckItemID, out p_objResultVO);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ��ѯ�õ�  CheckResultTable ���� 2004.06.04

        /// <summary>
        /// ��ѯ�õ�  CheckResultTable ���� 2004.06.04
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strGroupID"></param>
        /// <param name="p_blnRealResult"></param>
        /// <param name="p_dtbResultList"></param>
        /// <returns></returns>
        public long m_lngGetCheckResultTable(string p_strAppID, string p_strOringinDate, bool p_blnRealResult, out DataTable p_dtbResultList)
        {
            long lngRes = 0;
            p_dtbResultList = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetCheckResultTable(p_strAppID, p_strOringinDate, p_blnRealResult, out p_dtbResultList);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�󶨺���ȡ���� ���� 2004.06.10
        /// <summary>
        /// ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�󶨺���ȡ����
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns>
        /// С�� 0 : ��ѯʧ��; 
        /// 100: �޿ɰ󶨵���������;  
        /// 300: ָ�������������Ŵ���,������ʷ��¼; 
        /// 350: ָ�������������ѱ�����;
        /// 400:ָ��������������ԭʼ����; 
        /// ����: �ɹ�����
        /// </returns>
        public long m_lngQueryBindAndGetDeviceDataByAppointment(
            string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate,
            out clsDeviceReslutVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryBindAndGetDeviceDataByAppointment(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�� ���� 2004.06.10
        /// <summary>
        /// ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�� ���� 2004.06.10
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDate"></param>
        /// <param name="p_objResultLogVO">�Դ˵������������ȡ����</param>
        /// <returns>
        /// С�� 0 : ��ѯʧ��;
        /// 100: �޿ɰ󶨵���������;
        /// 300: ָ�������������Ŵ���,������ʷ��¼;
        /// 350: ָ�������������ѱ�����;
        /// ����: �ɹ�����
        /// </returns>
        public long m_lngQueryBindByAppointment(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, out clsResultLogVO p_objResultLogVO)
        {
            long lngRes = 0;
            p_objResultLogVO = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryBindByAppointment(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, out p_objResultLogVO);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region   ����ָ�����������,REQ ,��ѯ DeviceResultLog ����ȡ�������� ���� 2004.06.10
        /// <summary>
        ///  ����ָ�����������,REQ ,��ѯ DeviceResultLog ����ȡ�������� ���� 2004.06.10	
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_intImportReq"></param>
        /// <param name="p_objDeviceResultList"></param>
        /// <returns>
        /// С�� 0 : ��ѯʧ��; 
        /// 400:ָ��������������ԭʼ����
        /// ����: �ɹ����� 
        /// </returns>
        public long m_lngGetDeviceData(
            string p_strDeviceID, int p_intImportReq,
            out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            long lngRes = 0;
            p_objDeviceResultList = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceData(p_strDeviceID, p_intImportReq, out p_objDeviceResultList);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region  m_lngGetDeviceData ����ָ�����������,��������,�������������,����ʼ�����ͽ���������ȡ�������� ���� 2004.06.10	
        public long m_lngGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, int p_intBeginIndex, int p_intEndIndex,
            out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            long lngRes = 0;
            p_objDeviceResultList = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceData(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, p_intBeginIndex, p_intEndIndex, out p_objDeviceResultList);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region ����report_group_id��application_id_chr��ѯ���浥�����Ϣ ���� 2004.06.04
        /// <summary>
        /// ����report_group_id��application_id_chr��ѯ���浥�����Ϣ
        /// </summary>
        /// <param name="p_strReportGroupID">������ID</param>
        /// <param name="p_strApplID">���뵥ID</param>
        /// <param name="p_blnConfirmed">�Ƿ����</param>
        /// <param name="p_dtbReportInfo">���ر��浥�����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetReportPrintInfo(string p_strReportGroupID, string p_strApplID, bool p_blnConfirmed, out clsPrintValuePara p_objPrintContent)
        {
            p_objPrintContent = null;
            long lngRes = 0;
            DataTable dtbReportInfo = null;
            DataTable dtbCheckResult = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetReportInfoByReportGroupIDAndApplicationID(p_strReportGroupID, p_strApplID, p_blnConfirmed, out dtbReportInfo);
            if (lngRes > 0)
            {
                lngRes = 0;
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetCheckResultByReportGroupIDAndApplicationID(p_strApplID, p_strReportGroupID, p_blnConfirmed, out dtbCheckResult);
            }
            if (lngRes > 0)
            {
                p_objPrintContent = new clsPrintValuePara();
                p_objPrintContent.m_dtbBaseInfo = dtbReportInfo;
                p_objPrintContent.m_dtbResult = dtbCheckResult;
            }
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����report_group_id��application_id_chr��ѯ���浥�����Ϣ ���� 2004.06.04
        /// <summary>
        /// ����report_group_id��application_id_chr��ѯ���浥�����Ϣ
        /// </summary>
        /// <param name="p_strReportGroupID">������ID</param>
        /// <param name="p_strApplID">���뵥ID</param>
        /// <param name="p_blnConfirmed">�Ƿ����</param>
        /// <param name="p_dtbReportInfo">���ر��浥�����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetReportInfoByReportGroupIDAndApplicationID(string p_strReportGroupID, string p_strApplID, bool p_blnConfirmed, out DataTable p_dtbReportInfo)
        {
            long lngRes = 0;
            p_dtbReportInfo = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetReportInfoByReportGroupIDAndApplicationID(p_strReportGroupID, p_strApplID, p_blnConfirmed, out p_dtbReportInfo);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����report_group_id��application_id_chr��ѯ����Ľ����¼ ���� 2004.06.04
        /// <summary>
        /// ����report_group_id��application_id_chr��ѯ����Ľ����¼
        /// </summary>
        /// <param name="strApplicationID">���뵥ID</param>
        /// <param name="strReportGroupID">������ID</param>
        /// <param name="blnConfirmed">�Ƿ����</param>
        /// <param name="dtbCheckResult">���ؽ����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetCheckResultByReportGroupIDAndApplicationID(string p_strApplicationID, string p_strReportGroupID, bool p_blnConfirmed, out DataTable p_dtbCheckResult)
        {
            long lngRes = 0;
            p_dtbCheckResult = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetCheckResultByReportGroupIDAndApplicationID(p_strApplicationID, p_strReportGroupID, p_blnConfirmed, out p_dtbCheckResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��t_opr_lis_check_result�����һ����¼ ���� 2004.06.4
        //		public long m_lngAddNewCheckResult(com.digitalwave.iCare.ValueObject.clsCheckResult_VO p_objCheckResultVO)
        //		{
        //			long lngRes=0;
        //			com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc objSvc = 
        //				(com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsCheckResultSvc));
        //			lngRes = proxy.Service.m_lngAddNewCheckResult(p_objCheckResultVO);
        //			//			objSvc.Dispose();
        //			return lngRes;
        //		}
        #endregion

        #region [U]��t_opr_lis_check_result����������¼ ���� 2004.06.4
        /// <summary>
        /// ���ñ�����ʱ,���贫�� p_strSampleIDArr �е����е����������м�����Ŀ���,��ֻ�ܴ���
        /// �� p_strSampleIDArr �б�֮�е������ļ�����Ŀ���;
        /// </summary>
        /// <param name="p_objCheckResultList"></param>
        /// <param name="p_strSampleIDArr"></param>
        /// <returns></returns>
        public long m_lngAddCheckResultList(clsCheckResult_VO[] p_objCheckResultList, string[] p_strSampleIDArr, string p_strOriginDate)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddCheckResultList(p_objCheckResultList, p_strSampleIDArr, p_strOriginDate);
            //			objSvc.Dispose();			
            return lngRes;
        }
        #endregion

        //************************************************************************

        #region ���Զ��󶨷�ʽ,����ָ�����������  ��ѯ�󶨺���ȡ���� ���� 2004.06.10
        /// <summary>
        /// ��ָ����ŷ�ʽ,����ָ�����������,��������(trunc),������������Ų�ѯ�󶨺���ȡ����
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns>
        /// С�� 0 : ��ѯʧ��; 
        /// 100: �޿ɰ󶨵���������;
        /// 300: ָ�������������Ŵ�����δ��,������ʷ��¼; 
        /// 400:ָ��������������ԭʼ����; 
        /// ����: �ɹ�����
        /// </returns>
        public long m_lngQueryBindAndGetDeviceDataByAutoBind(string p_strDeviceID, out clsDeviceReslutVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryBindAndGetDeviceDataByAutoBind(p_strDeviceID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���Զ��󶨷�ʽ,����ָ����������� ��ѯ�� ���� 2004.06.10
        /// <summary>
        /// ���Զ��󶨷�ʽ,����ָ����������� ��ѯ�� ���� 2004.06.10
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultLogVO">�Դ˵������������ȡ����</param>
        /// <returns>
        /// С�� 0 : ��ѯʧ��;
        /// 100: �޿ɰ󶨵���������;
        /// 300: �ɰ󶨵����������Ŵ�����δ��,������ʷ��¼ 
        /// ����: �ɹ�����
        /// </returns>
        public long m_lngQueryBindByAutoBind(string p_strDeviceID, out clsResultLogVO p_objResultLogVO)
        {
            long lngRes = 0;
            p_objResultLogVO = null;
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryBindByAutoBind(p_strDeviceID, out p_objResultLogVO);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region ����ָ�����ˣ�ָ��������ĿID�ļ�����
        /// <summary>
        /// ����ָ�����ˣ�ָ��������ĿID�ļ�����
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strExcItemID"> ��Ҫ���⴦�����ĿID</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryPatientExcItemResult(string p_strPatientID, string p_strExcItemID, out DataTable p_dtResult)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryPatientExcItemResult(p_strPatientID, p_strExcItemID, out p_dtResult);
        }

        #endregion
    }
}
