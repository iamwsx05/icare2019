using System;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// ��ʱ��¼ ��ժҪ˵����
    /// </summary>
    public class clsBrothRecords_F2Domain
    {
        //private clsBaseCaseHistorySevice m_objBaseServ;
        //private clsNewBabyCircsRecordService m_objCircsServ;
        //public clsBrothRecords_F2Domain(clsBaseCaseHistorySevice p_objProcessServ)
        //{
        //    //m_objBaseServ =  p_objProcessServ;
        //}

        //public clsBrothRecords_F2Domain()
        //{
        //    //clsNewBabyInRoomRecordService objServ = new clsNewBabyInRoomRecordService();
        //    //m_objBaseServ =  objServ;
        //    //m_objCircsServ = new clsNewBabyCircsRecordService();
        //}

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
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsNewBabyInRoomRecordService_m_lngGetRecordTimeList(p_strInPatientID, out p_strInPatientDateArr, out p_strCreateRecordTimeArr, out p_strOpenRecordTimeArr);
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
        public long m_lngModifyRecord(clsBrothRecords_F2 p_objOldRecordContent,
            clsBrothRecords_F2 p_objNewRecordContent, clsPictureBoxValue[] p_objPicValueArr,
            string p_strDiseaseID,
            out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;
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
        public long m_lngDeleteRecord(clsBrothRecords_F2 p_objRecordContent, out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж� 
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
            clsBrothRecords_F2 m_objAddNewRecord,
            out clsPreModifyInfo p_objModifyInfo)
        {
            //�����ж�
            p_objModifyInfo = null;
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
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsNewBabyInRoomRecordService_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
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
            out clsBrothRecords_F2 p_objRecordContent)
        {
            p_objRecordContent = null;
            clsBaseCaseHistoryInfo objRecordContent = null;

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeleteRecordContent(p_strInPatientID, p_strInPatientDate, p_strOpenRecordTime, out objRecordContent);
                p_objRecordContent = (clsBrothRecords_F2)objRecordContent;
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
        public long m_lngAddNewCircsRecord(clsBrothRecords_F2Rec p_objRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsNewBabyCircsRecordService_m_lngAddNewRecord(p_objRecordContent, out p_objModifyInfo);
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
        public long m_lngModifyCircsRecord(clsBrothRecords_F2Rec p_objOldRecordContent,
            clsBrothRecords_F2Rec p_objNewRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsNewBabyCircsRecordService_m_lngModifyRecord(p_objOldRecordContent, p_objNewRecordContent, out p_objModifyInfo);
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
        public long m_lngDeleteCircsRecord(clsBrothRecords_F2Rec p_objRecordContent)
        {
            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsNewBabyCircsRecordService_m_lngDeleteCircsRecord(p_objRecordContent);
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
            out clsBrothRecords_F2Rec p_objRecordContent)
        {
            p_objRecordContent = null;
            clsTrackRecordContent objTemp = null;

            long m_lngRes = 0;
            try
            {
                // m_lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecordContent(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out objTemp);
                p_objRecordContent = (clsBrothRecords_F2Rec)objTemp;
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
            string p_strBirthTime,
            out clsBrothRecords_F2Rec[] p_objRecordContentArr)
        {
            p_objRecordContentArr = null;

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAllContent(p_strInPatientID, p_strInPatientDate, p_strBirthTime, out p_objRecordContentArr);
            }
            finally
            {
                //m_objCircsServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ��ȡ�޸ĺ����е������������¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objRecordContentArr"></param>
        /// <returns></returns>
        public long m_lngGetAllModifiedCircsRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            out clsBrothRecords_F2Rec[] p_objRecordContentArr)
        {
            p_objRecordContentArr = null;

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
