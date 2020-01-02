using System;
using System.Data;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsDomainController_SampleManage.
    /// ���� 2004.05.10
    /// </summary>
    public class clsDomainController_SampleManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region xing.chen add for
        //public long m_lngFindItemNameByApplicationID(string strAppID,out string strItemName)
        //{
        //    strItemName = "";
        //    long lngRes = 0;
        //    com.digitalwave.iCare.middletier.LIS.clsApplicationSvc objSvc = (com.digitalwave.iCare.middletier.LIS.clsApplicationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsApplicationSvc));
        //    lngRes = proxy.Service.m_lngFindItemNameByApplicationID(this.objPrincipal,strAppID,out strItemName);
        //    return lngRes;
        //}
        #endregion

        #region		xing.chen add for ���ݱ걾�Ų�ѯ�걾״̬
        public long m_lngFindStatusBySampleID(string strSampleID, out int intStatus)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngFindStatusBySampleID(  strSampleID, out intStatus);
            return lngRes;
        }
        #endregion

        #region	[U]xing.chen add for �޸�������
        public long m_lngModifyBarCode(string strSampleID, string strAppID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngModifyBarCode(  strSampleID, strAppID);
            return lngRes;
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainController_SampleManage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region ����������־λ ���� 2004.10.31
        /// <summary>
        /// ����������־λ
        ///  ���� 2004.10.31
        /// </summary>
        /// <param name="p_strSampleIDArr"></param>
        /// <param name="p_intSourceStatus"></param>
        /// <param name="p_intTargetStatus"></param>
        /// <returns>
        /// С�ڵ���0:����;
        ///1:�ɹ���
        /// </returns>
        public long m_lngUpdateSampleFlag(string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateSampleFlag( p_strSampleIDArr, p_intSourceStatus, p_intTargetStatus);
         
            return lngRes;
        }
        #endregion

        #region �걾����
        #region [U]����BarCode��ѯ�����յ�������Ϣ
        /// <summary>
        /// ����BarCode��ѯ�����յ�������Ϣ
        /// </summary>
        public long m_mthGetUnReceivedSampleByBarCode(string p_strBarCode, out clsSampleReceive_VO p_objRecord)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_mthGetUnReceivedSampleByBarCode( p_strBarCode, out p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������ѯ�Ѳɼ�,��δ���յı걾��Ϣ
        /// <summary>
        /// ����������ѯ�Ѳɼ�,��δ���յı걾��Ϣ
        /// </summary>
        /// <param name="p_strDatFrom"></param>
        /// <param name="p_strDatTo"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_strAcceptEmp"></param>
        /// <param name="p_strPatientName"></param>
        /// <param name="p_strPatientCardID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strCheckCategory"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetUnReceivedSampleByCondition(string p_strDatFrom, string p_strDatTo, string p_strSampleType,
            string p_strAcceptEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out clsSampleUnReceive_VO[] p_objResultArr)
        {
             return (new weCare.Proxy.ProxyLis02()).Service.m_lngGetUnReceivedSampleByCondition( p_strDatFrom, p_strDatTo, p_strSampleType, p_strAcceptEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum, out p_objResultArr);
        }
        #endregion

        #region ����������ѯ�ѽ��յı걾��Ϣ
        public long m_lngGetReceivedSampleByCondition(string p_strDatFrom, string p_strDatTo, string p_strSampleType,
            string p_strAcceptEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out clsSampleReceive_VO[] p_objResultArr)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReceivedSampleByCondition( p_strDatFrom, p_strDatTo, p_strSampleType, p_strAcceptEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����Զ������������뵥Ԫ(�������Ϊ����)
        /// <summary>
        /// �����Զ������������뵥Ԫ
        /// baojian.mo 2007.09.11 add
        /// </summary>
        /// <param name="p_strCheckCategory">�Զ�������������</param>
        /// <param name="p_dtbDetail"></param>
        /// <returns></returns>
        public long m_lngGetAppuserGroupDetail(string p_strCheckCategory, out DataTable p_dtbDetail)
        {
            return (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAppuserGroupDetail( p_strCheckCategory, out p_dtbDetail);
        }
        #endregion

        #region ���ձ걾���˻�
        public long m_lngReceiveSample(int p_intStatus, string p_strSampleID, string p_strReceiveDat, string p_strReceiveEmp, string p_strSendPeopleID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngReceiveSample( p_intStatus, p_strSampleID, p_strReceiveDat, p_strReceiveEmp, p_strSendPeopleID);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion

        #region ����������Ӵ��� ͯ�� 2004.08.27
        #region ����һ����¼
        public long m_lngAddNewSampleInterpose(clsLisSampleInterposeVO p_objRecord)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAddNewSampleInterpose( p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������ID��ѯ��Ӽ�¼
        public long m_lngGetSampleInterposeByDeviceID(string p_strDeviceID, out clsLisSampleInterposeVO p_objResult)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleInterposeByDeviceID( p_strDeviceID, out p_objResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��������ID���²�Ӽ�¼
        public long m_lngSetSampleInterpose(clsLisSampleInterposeVO p_objRecord)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSetSampleInterpose( p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��Ӵ���
        public long m_lngSampleInterpose(clsLisSampleInterposeVO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSampleInterpose( p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion

        #region ���±�T_OPR_LIS_DEVICE_RELATION ͯ�� 2004.07.26
        public long m_lngSetLisDeviceRelation(clsT_LIS_DeviceRelationVO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngSetLisDeviceRelation( p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����������ѯ����������֮��Ĺ�ϵ ͯ�� 2004.07.26
        public long m_lngGetDeviceRelationVOArrByCondition(string p_strDeviceID, string p_strCheckDatFrom, string p_strCheckDatTo,
            out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceRelationVOArrByCondition( p_strDeviceID, p_strCheckDatFrom, p_strCheckDatTo, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region [U]����һ������,ͬʱ�޸����������� ���� 2004.07.21
        /// <summary>
        /// ����һ������,ͬʱ�޸�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_objRecordVO"></param>
        /// <returns></returns>
        public long m_lngAddNewSampleAndModifyAppSampleGroup(
            string p_strAppID, ref clsT_OPR_LIS_SAMPLE_VO p_objRecordVO)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAddNewSampleAndModifyAppSampleGroup(
             p_strAppID, ref p_objRecordVO);
            proxy.Service.Dispose();
            return lngRes;
        }
        #endregion

        #region ���������걾 ͯ�� 2004.07.07
        public long m_lngReceptSample(clsT_OPR_LIS_SAMPLE_VO p_objSampleVO, clsT_LIS_DeviceRelationVO p_objDeviceRelationVO)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngReceptSample( p_objSampleVO, p_objDeviceRelationVO);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����BarCode�жϸñ걾�Ƿ��Ѿ����� ͯ�� 2004.07.07
        public long m_lngGetReceptedSampleInfoByBarCode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO p_objRecord)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReceptedSampleInfoByBarCode( p_strBarCode, out p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������ڷ�Χ��ѯ�Ѻ��յı걾 ͯ�� 2004.07.07
        public long m_lngGetReceptedSampleByDateRange(string p_strDeviceID, string p_strFromDat, string p_strToDat, out DataTable p_dtbResult)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetReceptedSampleByDateRange( p_strDeviceID, p_strFromDat, p_strToDat, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲ������ڲ�ѯδ���յı걾��Ϣ ͯ�� 2004.07.07
        public long m_lngGetNotReceptSampleBySamplingDat(string p_strFromDat, string p_strToDat, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllNotReceptSample( p_strFromDat, p_strToDat, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݱ걾��BarCode��ѯ��Ӧ�ı걾����Ϣ ͯ�� 2004.07.06
        public long m_lngGetSampleInfoByBarCode(string p_strBarCode, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleInfoByBarCode( p_strBarCode, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���еı걾������Ϣ ͯ�� 2004.06.29
        public long m_lngGetSampleTypeArr(out clsSampleType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleTypeArr( out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݱ걾ID��ѯ�걾��Ϣ ͯ�� 2004.06.21
        public long m_lngGetSampleVOArrBySampleID(string p_strSampleID, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleVOArrBySampleID( p_strSampleID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����BarCode �õ�����VO ���� 2004.07.6
        public long m_lngGetSampleVOByBarcode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleVOByBarcode( p_strBarCode, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region ���ݱ걾ID��ѯ�걾�������걾�Ĺ�ϵVO ͯ�� 2004.06.21
        public long m_lngGetDeviceRelationVOArrBySampleID(string p_strSampleID, out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceRelationVOArrBySampleID( p_strSampleID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region m_lngModifyBind ɾ������һ������������һ������ ���� 2004.05.26
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_objSourceVO"></param>
        /// <param name="p_objTargetVO"></param>
        /// <returns></returns>
        public long m_lngModifyBind(clsT_LIS_DeviceRelationVO p_objSourceVO, clsT_LIS_DeviceRelationVO p_objTargetVO)
        {
            long lngRes = 0;
           lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngModifyBind( p_objSourceVO, p_objTargetVO);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion


        #region m_lngAddNewDeviceRelation  Ϊ�� t_opr_lis_device_relation  ���� ��¼ʱ�� ���� 2004.06.9
        /// <summary>
        /// Ϊ�� t_opr_lis_device_relation  ���� ��¼ʱ�� ���� 2004.06.9
        /// </summary>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngAddNewDeviceRelation(clsT_LIS_DeviceRelationVO p_objRecord)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAddNewDeviceRelation( p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region m_lngDeleteDeviceRelation ɾ���������� ���� 2004.05.26
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strRelationDate"></param>
        /// <param name="p_strSeq"></param>
        /// <returns></returns>
        public long m_lngDeleteDeviceRelation(string p_strDeviceID, string p_strRelationDate, string p_strSeq)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteDeviceRelation( p_strDeviceID, p_strRelationDate, p_strSeq);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region m_lngDeleteDeviceRelation ɾ���������� ���� 2004.05.26
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_objRelation"></param>
        /// <param name="p_blnReleaseDeviceSample"></param>
        /// <returns></returns>
        public long m_lngDeleteDeviceRelation(clsT_LIS_DeviceRelationVO p_objRelation, bool p_blnReleaseDeviceSample)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDeleteDeviceRelation( p_objRelation);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region m_lngInsertSampleRecord  Ϊ�� t_opr_lis_sample ����,�޸�,ɾ�� ��¼ʱ�� ���� 2004.05.26

        /// <summary>
        /// Ϊ�� t_opr_lis_sample ����,�޸�,ɾ�� ��¼ʱ�� ;
        /// ���� 2004.05.26
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>		
        public long m_lngInsertSampleRecord(clsT_OPR_LIS_SAMPLE_VO[] p_objRecordVOArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertSampleRecord( p_objRecordVOArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����SampleID,GroupID�������飩,��ѯ����� ���� 2004.05.11
        /// <summary>
        /// ����SampleID,GroupID�����������飩��ѯ�����
        /// </summary>
        /// <param name="p_strGroupID"></param>
        /// <param name="p_strSmapleID"></param>
        /// <param name="p_dtbPrintResult">
        /// modify_dat
        /// groupid_chr
        /// check_item_id_chr
        /// sample_id_chr
        /// result_vchr
        /// unit_vchr 
        /// device_check_item_name_vchr
        /// refrange_vchr
        /// check_item_name_vchr
        /// check_item_english_name_vchr
        /// min_val_dec
        /// max_val_dec
        /// abnormal_flag_chr
        /// check_dat
        /// clinicapp_vchr
        /// memo_vchr
        /// confirm_dat
        /// deviceid_chr
        /// pointliststr_vchr
        /// summary_vchr
        /// graph_img
        /// status_int
        /// checker1_chr
        /// checker2_chr
        /// confirm_person_chr
        /// operator_id_chr
        /// check_deptid_chr
        /// print_ord_int
        /// </param>
        /// <returns></returns>
        public long m_lngGetPrintResult(string p_strSampleID, string p_strGroupID, out System.Data.DataTable p_dtbPrintResult)
        {
            long lngRes = 0;
            p_dtbPrintResult = null;
           
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetPrintResult( p_strGroupID, p_strSampleID, out p_dtbPrintResult);
            //objResultSvc.Dispose();
            return lngRes;


        }
        #endregion

        #region ��˱걾 ���� 2004.05.11
        /// <summary>
        /// ��˱걾 
        /// ���� 2004.05.11
        /// </summary>
        /// <param name="p_strSampleID"></param>
        /// <returns></returns>
        public long m_lngAuditingSample(string p_strSampleID)
        {
            long lngRes = 0;
             
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAuditingSample( p_strSampleID);
            //objResultSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �õ����м�����Ʒ���� ���� 2004.05.13
        /// <summary>
        /// �õ����м�����Ʒ����
        /// ���� 2004.05.13
        /// </summary>
        /// <param name="p_dtbSampleTypeList">
        /// SAMPLE_TYPE_ID_CHR
        /// SAMPLE_TYPE_DESC_VCHR
        /// PYCODE_CHR
        /// WBCODE_CHR
        /// STDCODE1_CHR
        /// STDCODE2_CHR
        /// </param>
        /// <returns></returns>
        public long m_lngGetSampleTypeList(out System.Data.DataTable p_dtbSampleTypeList)
        {
            long lngRes = 0;
            p_dtbSampleTypeList = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleTypeList( out p_dtbSampleTypeList);
            //			objSampleSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���еļ������
        /// <summary>
        /// ��ȡ���еļ������
        /// baojian.mo 2007.09.10
        /// </summary>
        /// <param name="p_dtbCheckCategory"></param>
        /// <returns></returns>
        public long m_lngGetCheckCategoryList(out DataTable p_dtbCheckCategory)
        {
            long lngRes = 0;
            p_dtbCheckCategory = null;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetCheckCategoryList( out p_dtbCheckCategory);
            return lngRes;
        }
        #endregion

        #region ������Ʒ����ID��ȡ��Ʒ״̬ ���� 2004.05.13
        /// <summary>
		/// ������Ʒ����ID��ȡ��Ʒ״̬
		/// ���� 2004.05.13
		/// </summary>
		/// <param name="strSampleTypeID"></param>
		/// <param name="dtbSampleState">
		/// character_desc_vchr
		/// </param>
		/// <returns></returns>
		public long m_lngGetStateBySampleType(string p_strSampleTypeID, out System.Data.DataTable p_dtbSampleState)
        {
            p_dtbSampleState = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleState( p_strSampleTypeID, out p_dtbSampleState);
            //			objSampleSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �õ����е�����״̬��Ϣ�б� ���� 2004.05.27
        /// <summary>
        /// �õ����е�����״̬��Ϣ�б�
        ///  ���� 2004.05.27
        /// </summary>
        /// <param name="p_dtbSampleState">
        /// table:t_aid_lis_sample_character
        /// column:
        /// character_desc_vchr
        /// pycode_chr
        /// wbcode_chr
        /// sample_type_id_chr
        /// </param>
        /// <returns></returns>
        public long m_lngGetSampleStateList(out System.Data.DataTable p_dtbSampleState)
        {
            p_dtbSampleState = null;
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetSampleState( out p_dtbSampleState);
            //			objSampleSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������õĺ���
        #region ���ݸ�������ϲ�ѯ�õ������б� ���� 2004.05.10
        /// <summary>
        /// ���ݸ�������ϲ�ѯ�õ������б�
        /// ���� 2004.05.10
        /// </summary>
        /// <param name="p_intDevice">
        /// 0: �ֹ�������
        /// 1������
        /// 2���ֹ�
        /// </param>
        /// <param name="p_strDeviceID">��Ϊ��</param>
        /// <param name="p_intAuditing">
        /// 0������˺�δ���
        /// 1��δ���
        /// 2�������
        /// </param>
        /// <param name="p_strCheckDate_Begin">��Ϊ��</param>
        /// <param name="p_strCheckDate_End">��Ϊ��</param>
        /// <param name="p_dtbSampleList">���ؽ������
        /// sample_id_chr
        /// barcode_vchr
        /// groupid_chr
        /// check_date_dat
        /// status_int
        /// application_form_no_chr
        /// patient_name_vchr
        /// sex_chr
        /// age_chr
        /// diagnose_vchr
        /// appl_empid_chr
        /// appl_deptid_chr
        /// </param>
        /// <returns>>0 ʱ��Ч</returns>
        //        public long m_lngGetSampleList(int p_intDevice,string p_strDeviceID,int p_intAuditing, string p_strCheckDate_Begin, string p_strCheckDate_End,out DataTable p_dtbSampleList)
        //        {
        //            long lngRes = 0;
        //            p_dtbSampleList = null;

        //            com.digitalwave.iCare.middletier.LIS.clsSampleSvc objSvc = 
        //                (com.digitalwave.iCare.middletier.LIS.clsSampleSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsSampleSvc));
        //            lngRes = proxy.Service.m_lngGetSampleList( p_intDevice, p_strDeviceID, p_intAuditing,  p_strCheckDate_Begin,  p_strCheckDate_End,out  p_dtbSampleList);
        ////			objSvc.Dispose();
        //            return lngRes;
        //        }
        #endregion
        #endregion

        #region ���Ҳ���
        /// <summary>
        /// ���Ҳ���	���������ַ���
        /// </summary>
        /// <param name="strCode">�����ַ���</param>
        /// <param name="p_objResultArr">��������	[out ����]</param>
        public long m_lngFindArea(string strCode, out clsBIHArea[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strCode, out p_objResultArr);
            //			objSampleSvc.Dispose();
            return lngRes;


        }
        #endregion

        #region �޸Ĳ�����Ա
        /// <summary>
        /// �޸Ĳ�����Ա
        /// </summary>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        public long m_lngInsertCollector(string p_strEmpId, string p_strSampleId, string p_strApplicationID)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertCollector( p_strEmpId, p_strSampleId, p_strApplicationID);
            return lngRes;
        }
        #endregion

        #region ��ȡ����״̬
        /// <summary>
        /// ��ȡ����״̬
        /// </summary>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_intStatus"></param>
        /// <returns></returns>
        public long m_lngQuerySampleStatus(string p_strSampleID, out int p_intStatus, out string p_strIsSampleBack)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngQuerySampleStatus(p_strSampleID, out p_intStatus, out p_strIsSampleBack);
            return lngRes;
        }
        #endregion

        #region ����������������Ϣ
        /// <summary>
        /// ����������������Ϣ
        /// </summary>
        /// <param name="p_objSampleFeedBack"></param>
        /// <returns></returns>
        public long m_lngInsertSampleFeedBack(clslissample_feedback p_objSampleFeedBack)
        {
            long lngRes = 0;
             lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertSampleFeedBack(p_objSampleFeedBack);
            return lngRes;
        }
        #endregion

    }
}
