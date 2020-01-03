
using System;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;


namespace iCare
{
    /// <summary>
    /// clsAYQBabyAssessmentContent_EspRecordDomain ��ժҪ˵����
    /// </summary>
    public class clsAYQBabyAssessmentContentDomain
    {
        //private clsBaseCaseHistorySevice m_objBaseServ;
        //private clsAYQBabyAssessmentRecordService m_objCircsServ;
        //public clsAYQBabyAssessmentContentDomain(clsBaseCaseHistorySevice p_objProcessServ)
        //{
        //          //m_objBaseServ =  p_objProcessServ;
        //}

        public clsAYQBabyAssessmentContentDomain()
        {
            //clsAYQBabyAssessmenEspRecordService objServ = new clsAYQBabyAssessmenEspRecordService();
            //m_objBaseServ =  objServ;
            //m_objCircsServ = new clsAYQBabyAssessmentRecordService();
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

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsAYQBabyAssessmenEspRecordService_m_lngGetRecordTimeList(p_strInPatientID, out p_strInPatientDateArr, out p_strCreateRecordTimeArr, out p_strOpenRecordTimeArr);
            }
            finally
            {
                //objServ.Dispose();
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
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRecordContent(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/out p_objRecordContent, out p_objPicValueArr);
            }
            finally
            {
                //objServ.Dispose();
            }
            return m_lngRes;
        }

        // ����¼�¼��
        public long m_lngAddNewRecord(clsBaseCaseHistoryInfo p_objRecordContent, clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;

            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngAddNewRecord(p_objRecordContent, p_objPicValueArr, p_strDiseaseID, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, out p_objModifyInfo);
            }
            finally
            {
                //objServ.Dispose();
            }
            return m_lngRes;
        }

        // �޸ļ�¼��
        public long m_lngModifyRecord(clsAYQBabyAssessmentContent_EspRecord p_objOldRecordContent,
            clsAYQBabyAssessmentContent_EspRecord p_objNewRecordContent, clsPictureBoxValue[] p_objPicValueArr,
            string p_strDiseaseID,
            out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;

            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngModifyRecord(p_objOldRecordContent, p_objNewRecordContent, p_objPicValueArr, p_strDiseaseID, clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID, out p_objModifyInfo);
            }
            finally
            {
                //objServ.Dispose();
            }
            return m_lngRes;
        }

        // ɾ����¼��
        public long m_lngDeleteRecord(clsAYQBabyAssessmentContent_EspRecord p_objRecordContent, out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�

            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngDeleteRecord(p_objRecordContent, out p_objModifyInfo);
            }
            finally
            {
                //objServ.Dispose();
            }
            return m_lngRes;
        }

        // ����������¼��
        public long m_lngReAddNewRecord(clsInPatientCaseHistoryContent m_objDelRecord,
            clsAYQBabyAssessmentContent_EspRecord m_objAddNewRecord,
            out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;

            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngReAddNewRecord(m_objDelRecord, m_objAddNewRecord, out p_objModifyInfo);
            }
            finally
            {
                //objServ.Dispose();
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


            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetPrintInfo(p_strInPatientID, p_strInPatientDate, p_dtmModifyDate, out p_objContent, out p_objPicValueArr, out p_dtmFirstPrintDate, out p_blnIsFirstPrint);
            }
            finally
            {
                //objServ.Dispose();
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
            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngUpdateFirstPrintDate_factory(enmDiseaseTrackType.AYQBabyAssessmentRecord, p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            }
            finally
            {
                //objServ.Dispose();
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
            out clsAYQBabyAssessmentContent_EspRecord p_objRecordContent)
        {
            p_objRecordContent = null;
            clsBaseCaseHistoryInfo objRecordContent = null;

            //clsAYQBabyAssessmenEspRecordService objServ =
            //    (clsAYQBabyAssessmenEspRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmenEspRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeleteRecordContent(p_strInPatientID, p_strInPatientDate, p_strOpenRecordTime, out objRecordContent);
                p_objRecordContent = (clsAYQBabyAssessmentContent_EspRecord)objRecordContent;
            }
            finally
            {
                //objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ��������������¼
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        public long m_lngAddNewCircsRecord(clsAYQBabyAssessmentContent p_objRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //clsAYQBabyAssessmentRecordService m_objCircsServ =
            //    (clsAYQBabyAssessmentRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmentRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngAddNewRecord_factory(enmDiseaseTrackType.AYQBabyAssessmentRecord, p_objRecordContent, out p_objModifyInfo);
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// �޸������������¼
        /// </summary>
        /// <param name="p_objOldRecordContent"></param>
        /// <param name="p_objNewRecordContent"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        public long m_lngModifyCircsRecord(clsAYQBabyAssessmentContent p_objOldRecordContent,
            clsAYQBabyAssessmentContent p_objNewRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //clsAYQBabyAssessmentRecordService m_objCircsServ =
            //    (clsAYQBabyAssessmentRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmentRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngModifyRecord_factory(enmDiseaseTrackType.AYQBabyAssessmentRecord, p_objOldRecordContent, p_objNewRecordContent, out p_objModifyInfo);
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ɾ�������������¼
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        public long m_lngDeleteCircsRecord(clsAYQBabyAssessmentContent p_objRecordContent)
        {
            //clsAYQBabyAssessmentRecordService m_objCircsServ =
            //    (clsAYQBabyAssessmentRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmentRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsAYQBabyAssessmentRecordService_m_lngDeleteCircsRecord(p_objRecordContent);
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ��ȡ�����������¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        public long m_lngGetCircsRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            out clsAYQBabyAssessmentContent p_objRecordContent)
        {
            p_objRecordContent = null;
            clsTrackRecordContent objTemp = null;

            //clsAYQBabyAssessmentRecordService m_objCircsServ =
            //    (clsAYQBabyAssessmentRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmentRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory(enmDiseaseTrackType.AYQBabyAssessmentRecord, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out objTemp);
                p_objRecordContent = (clsAYQBabyAssessmentContent)objTemp;
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ��ȡ�����������¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objRecordContentArr"></param>
        /// <returns></returns>
        public long m_lngGetAllCircsRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            out clsAYQBabyAssessmentContent[] p_objRecordContentArr)
        {
            p_objRecordContentArr = null;

            //clsAYQBabyAssessmentRecordService m_objCircsServ =
            //    (clsAYQBabyAssessmentRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmentRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAllContent(p_strInPatientID, p_strInPatientDate, out p_objRecordContentArr);
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ��ȡ�޸ĺ����е���������
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objRecordContentArr"></param>
        /// <returns></returns>
        public long m_lngGetAllModifiedCircsRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            out clsAYQBabyAssessmentContent[] p_objRecordContentArr)
        {
            p_objRecordContentArr = null;

            //clsAYQBabyAssessmentRecordService m_objCircsServ =
            //    (clsAYQBabyAssessmentRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsAYQBabyAssessmentRecordService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAllModifiedContent(p_strInPatientID, p_strInPatientDate, out p_objRecordContentArr);
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }
    }
}
