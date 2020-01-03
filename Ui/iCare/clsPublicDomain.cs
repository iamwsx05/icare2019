using System;
using System.IO;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsPublicDomain.
    /// </summary>
    public class clsPublicDomain
    {
        /// <summary>
        /// ����Xml�Ļ���
        /// </summary>
        private MemoryStream m_objXmlMemStream;

        /// <summary>
        /// ����Xml�Ĺ���
        /// </summary>

        private XmlTextWriter m_objXmlWriter;
        ///  <summary>
        /// ��ȡXml�����������		
        /// </summary>
        private XmlParserContext m_objXmlParser;
        //private com.digitalwave.PublicMiddleTier.clsPublicMiddleTier m_objServ;

        public clsPublicDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objServ=new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            m_objXmlMemStream = new MemoryStream(300);
            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);
        }



        /// <summary>
        /// ���Middle Tier �˵�ʱ��
        /// </summary>
        /// <returns></returns>
        public string m_strGetServerTime()
        {
            //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier m_objServ =
            //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

            string strRes = "";
            try
            {
                strRes = (new weCare.Proxy.ProxyEmr()).Service.m_strGetServerTime();
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return strRes;
        }

        /// <summary>
        /// ���Middle Tier �˵�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime m_dtmGetServerTime()
        {
            //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier m_objServ =
            //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

            string strDateTime = "";
            try
            {
                strDateTime = (new weCare.Proxy.ProxyEmr()).Service.m_strGetServerTime();
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return DateTime.Parse(strDateTime);
        }

        #region ��ȡ��Ժ�Ǽ���ˮ��
        /// <summary>
        /// ��ȡ��Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strInPatientDate">HISסԺ����</param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <returns></returns>
        public long m_lngGetRegisterID(string p_strPatientID, string p_strInPatientDate, out string p_strRegisterID)
        {
            p_strRegisterID = "";
            long lngRes = 0;
            //com.digitalwave.PatientManagerService.clsPatientManagerService objServ =
            //    (com.digitalwave.PatientManagerService.clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PatientManagerService.clsPatientManagerService));

            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(p_strPatientID, p_strInPatientDate, out p_strRegisterID);
            return lngRes;
        }
        #endregion

        #region ��ȡָ��Ա���������ָ�����˲������������
        /// <summary>
        /// ��ȡָ��Ա���������ָ�����˲������������,
        /// ֻ��ѯ�����������ʱ��δ������ǰʱ��ļ�¼
        /// </summary>
        /// <param name="p_strSubscriberID">Ա��ID</param>
        /// <param name="p_strRegisterID">������Ժ�ǼǺ�</param>
        /// <param name="p_intSubscribStatusArr">�������</param>
        /// <returns></returns>
        public long m_lngGetSpecifyApproveInfo(string p_strSubscriberID, string p_strRegisterID, out int[] p_intSubscribStatusArr)
        {
            p_intSubscribStatusArr = null;

            //com.digitalwave.emr.EMR_CaseArchivingService.clsEMR_ApproveArchivedCaseService objServ =
            //    (com.digitalwave.emr.EMR_CaseArchivingService.clsEMR_ApproveArchivedCaseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.emr.EMR_CaseArchivingService.clsEMR_ApproveArchivedCaseService));

            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetSpecifyApproveInfo(p_strSubscriberID, p_strRegisterID, out p_intSubscribStatusArr);
            return lngRes;
        }
        #endregion
    }
}
