using System;
using com.digitalwave.DiseaseTrackService;
using iCareData;
using com.digitalwave.iCare.middletier.BIHOrderServer;

namespace iCare
{

    /// <summary>
    ///  ���������¼���͵�����㡣��Ϊ��Ϣ��ת�����������������ӽ�����м����
    /// </summary>
    public class clsBeforeOperationSummaryDomain_xj
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
            clsBeforeOperationSummaryService_xj m_objServ =
                (clsBeforeOperationSummaryService_xj)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService_xj));

            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngGetRecordTimeList(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
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
            clsBeforeOperationSummaryService_xj m_objServ =
                (clsBeforeOperationSummaryService_xj)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService_xj));

            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngGetRecordContent(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
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
            clsBeforeOperationSummaryService_xj m_objServ =
                (clsBeforeOperationSummaryService_xj)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService_xj));

            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngUpdateFirstPrintDate(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
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
            clsBeforeOperationSummaryService_xj m_objServ =
                (clsBeforeOperationSummaryService_xj)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService_xj));

            long lngRes = 0;

            lngRes = m_objServ.m_lngUpdateOutDate(p_strRegisterID, p_dtmOutDate);
            m_objServ = null;
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
            clsBIHOrderInterface m_objServ =
                (clsBIHOrderInterface)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBIHOrderInterface));

            long lngRes = 0;

            lngRes = m_objServ.m_lngGetOutOrderByRegID(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strRegisterID, out p_dtbOrder);
            m_objServ = null;
            return lngRes;
        }
        /// <summary>
        /// ��ȡȫ�����ϼ�¼clsConsultationService
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetAllInactiveInfo(string p_strInpatientId, DateTime p_dtmInpatientDate, out com.digitalwave.emr.AssistModuleVO.clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            com.digitalwave.DiseaseTrackService.clsBeforeOperationSummaryService_xj objServ =
                    (com.digitalwave.DiseaseTrackService.clsBeforeOperationSummaryService_xj)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.DiseaseTrackService.clsBeforeOperationSummaryService_xj));

            long lngRes = objServ.m_lngGetAllInactiveInfo(p_strInpatientId, p_dtmInpatientDate, out p_objInactiveRecordInfoArr);
            objServ = null;
            return lngRes;
        }
    }
}
