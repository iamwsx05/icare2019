using System;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// ���̼�¼���ӿ��е������
    /// </summary>
    public class clsDiseaseTrackAddBlankDomain
    {
        //private clsDiseaseTrackAddBlankService m_objAddBlankServ;

        public clsDiseaseTrackAddBlankDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objAddBlankServ = new clsDiseaseTrackAddBlankService();
        }

        #region  ���ӿհ��еļ�¼����

        /// <summary>
        /// ��ȡҪ��ӵĿ��м�¼���ݡ�
        /// </summary>
        public long m_lngGetBlankRecordContent(string p_strInPatientID,
            DateTime p_strInPatientDate, out System.Data.DataTable p_dtbResult)
        {
            p_dtbResult = null;
            //�����ж�
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            //clsDiseaseTrackAddBlankService m_objAddBlankServ =
            //    (clsDiseaseTrackAddBlankService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDiseaseTrackAddBlankService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAddBlankValue(p_strInPatientID, p_strInPatientDate, out p_dtbResult);
            }
            finally
            {
                //m_objAddBlankServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ӿ��м�¼��
        /// </summary>
        public long m_lngAddNewBlankRecord(clsTrackRecordContent p_objRecordContent)
        {
            //�����ж�
            if (p_objRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            //clsDiseaseTrackAddBlankService m_objAddBlankServ =
            //    (clsDiseaseTrackAddBlankService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDiseaseTrackAddBlankService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngAddNewBlankRecord2DB(p_objRecordContent);
            }
            finally
            {
                //m_objAddBlankServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// �޸Ŀ��м�¼��
        /// </summary>
        public long m_lngModifyBlankRecord(clsTrackRecordContent p_objRecordContent)
        {
            //�����ж�
            if (p_objRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            //clsDiseaseTrackAddBlankService m_objAddBlankServ =
            //    (clsDiseaseTrackAddBlankService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDiseaseTrackAddBlankService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngModifyBlankRecord2DB(p_objRecordContent);
            }
            finally
            {
                //m_objAddBlankServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ɾ�����м�¼��
        /// </summary>
        public long m_lngDeleteBlankRecord(clsTrackRecordContent p_objRecordContent)
        {
            //�����ж�
            if (p_objRecordContent == null)
                return (long)enmOperationResult.Parameter_Error;

            //clsDiseaseTrackAddBlankService m_objAddBlankServ =
            //    (clsDiseaseTrackAddBlankService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDiseaseTrackAddBlankService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeleteBlankRecord2DB(p_objRecordContent);
            }
            finally
            {
                //m_objAddBlankServ.Dispose();
            }
            return lngRes;
        }

        #endregion
    }
}
