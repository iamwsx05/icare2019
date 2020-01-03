using System;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// ר�Ʋ��������
    /// </summary>
    public class clsInpatMedRecDomain
    {
        // ���캯��������Ϊָ�����м����
        public clsInpatMedRecDomain()
        {

        }

        /// <summary>
        /// ����¼�¼
        /// </summary>
        public long m_lngAddNewRecord(clsInpatMedRecContent p_objContent)
        {
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngAddNewRecord(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���˸������¼��ʱ���б�
        /// </summary>
        public long m_lngGetRecordTimeList(string p_strTypeID, string p_strInPatientID,
            out string[] p_strInPatientDateArr,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strInPatientDateArr = null;
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRecordTimeList(p_strTypeID, p_strInPatientID, out p_strInPatientDateArr, out p_strCreateRecordTimeArr, out p_strOpenRecordTimeArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡָ����¼����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        public long m_lngGetRecordContent(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate,
            out clsInpatMedRecContent p_objContent)
        {
            //�����ж�
            p_objContent = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRecordContent(p_strTypeID, p_strInPatientID, p_strInPatientDate, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡָ����¼����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        public long m_lngGetRecordContent(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, DateTime p_dtmOpenDate,
            out clsInpatMedRecContent p_objContent)
        {
            //�����ж�
            p_objContent = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRecordContent(p_strTypeID, p_strInPatientID, p_strInPatientDate, p_dtmOpenDate, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }


        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngModifyRecord(clsInpatMedRecContent p_objContent)
        {
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngModifyRecord(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ��ӡ����
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_objPrintContent"></param>
        /// <returns></returns>
        public long m_lngGetPrintInfo(string p_strTypeID, ref clsPrintInfo_InpatMedRec p_objPrintContent)
        {
            //�����ж�
            p_objPrintContent.m_objContent = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetPrintInfo(p_strTypeID, ref p_objPrintContent);
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
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        public long m_lngUpdateFirstPrintDate(string p_strTypeID, clsPrintInfo_InpatMedRec p_objPrintContent)
        {
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngUpdateFirstPrintDate(p_strTypeID, p_objPrintContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngDeleteRecord(clsInpatMedRecContent p_objContent)
        {
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngDeleteRecord(p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ�ؼ�����
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetType_ItemRecord(string p_strTypeID, out clsInpatMedRec_Type_Item[] p_objContentArr)
        {
            p_objContentArr = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetType_ItemRecord(p_strTypeID, out p_objContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡһ����Ŀ����
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strItemID"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetOneItemValue(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, string p_strItemID, out clsInpatMedRec_Item p_objContent)
        {
            p_objContent = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOneItemValue(p_strTypeID, p_strInPatientID, p_strInPatientDate, p_strItemID, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ�������п���������Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetPatientAllMainRecord(string p_strInPatientID, string p_strInPatientDate, out clsInpatMedRecContent[] p_objContentArr)
        {
            p_objContentArr = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetPatientAllMainRecord(p_strInPatientID, p_strInPatientDate, out p_objContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡһ��ר�Ʋ�������Ŀ��Ϣ
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_dtmOpenDate"></param>
        /// <param name="p_objItemContentArr"></param>
        /// <returns></returns>
        public long m_lngGetItemRecord(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, DateTime p_dtmOpenDate, out clsInpatMedRec_Item[] p_objItemContentArr)
        {
            p_objItemContentArr = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetItemRecord(p_strTypeID, p_strInPatientID, p_strInPatientDate, p_dtmOpenDate, out p_objItemContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡר�Ʋ�����������
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetTypeName(ref clsInpatMedRec_Type p_objContent)
        {
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetTypeName(ref p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡɾ����¼
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetDeactiveRecInfo(string p_strTypeID, string p_strInPatientID, string p_strOpenDate, out clsInpatMedRecContent p_objContent)
        {
            p_objContent = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeactiveRecInfo(p_strTypeID, p_strInPatientID, p_strOpenDate, out p_objContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡȫ��ר�Ʋ���
        /// </summary>
        /// <param name="p_objTypeArr"></param>
        /// <returns></returns>
        public long m_lngGetAllFormID(out clsInpatMedRec_Type[] p_objTypeArr)
        {
            p_objTypeArr = null;

            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllFormID(out p_objTypeArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ��Ӧ���ҿ�ʹ�õĵ��Ӳ���
        /// </summary>
        /// <param name="p_strDeptIDArr"></param>
        /// <param name="p_objTypeArr"></param>
        /// <returns></returns>
        public long m_lngGetFormByChargeDept(string[] p_strDeptIDArr, out clsInpatMedRec_Type[] p_objTypeArr)
        {
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetFormByChargeDept(p_strDeptIDArr, out p_objTypeArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡһ��סԺȫ�����ϼ�¼
        /// </summary>
        /// <param name="p_strType"></param>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetAllInactiveInfo(string p_strType, string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            //clsInpatMedRecServ objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetAllInactiveInfo(p_strType, p_strInpatientId, p_dtmInpatientDate, out p_objInactiveRecordInfoArr);
            return lngRes;
        }

        /// <summary>
        /// ��ȡ����һ����Ժ�ļ�¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        public long m_lngGetRecordTimeList(string p_strTypeID, string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            //�����ж�
            if (p_strTypeID == null || p_strTypeID == "" || p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;
            //clsInpatMedRecServ m_objServ =
            //    (clsInpatMedRecServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInpatMedRecServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRecordTimeList(p_strTypeID, p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
            }
            finally
            {
                //m_objTrackServ.Dispose();
            }
            return lngRes;
        }
    }
}
