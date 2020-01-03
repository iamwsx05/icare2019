using System;
using System.Xml;
using System.IO;
using System.Text;
using weCare.Core.Entity;


namespace iCare
{
    /// <summary>
    /// Summary description for clsBeforeOperationSummaryDomain.
    /// </summary>
    public class clsBeforeOperationSummaryDomain
    {
        #region ����


        /// <summary>
        /// ����Xml�Ļ���
        /// </summary>
        private MemoryStream m_objXmlMemStream;

        /// <summary>
        /// ����Xml�Ĺ���
        /// </summary>
        private XmlTextWriter m_objXmlWriter;

        /// <summary>
        /// ��ȡXml�����������
        /// </summary>
        private XmlParserContext m_objXmlParser;
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsBeforeOperationSummaryDomain()
        {
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�

            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
        }
        #endregion

        #region  �ӱ�XML
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>���ɵ�XML</returns>
        private string m_strMakeNewContentXml(clsBeforeOperationSummaryContentInfo p_objInfo, bool blnIsAddNew)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("BeforeOperationSummaryContent");

            if (!blnIsAddNew)
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objInfo.m_strOpenDate);

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objInfo.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objInfo.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objInfo.m_strModifyUserID);
            m_objXmlWriter.WriteAttributeString("OPERATEDOCTORID", p_objInfo.m_strOperateDoctorID);
            m_objXmlWriter.WriteAttributeString("CHARGEDOCTORID", p_objInfo.m_strChargeDoctorID);
            m_objXmlWriter.WriteAttributeString("DIAGNOSE", p_objInfo.m_strDiagnose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIAGNOSEGIST", p_objInfo.m_strDiagnoseGist.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BODYINFO", p_objInfo.m_strBodyInfo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALHANDLE", p_objInfo.m_strSpecialHandle.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PREPARATION", p_objInfo.m_strPreparation.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATIENTNOTION", p_objInfo.m_strPatientNotion.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANAESTHESIA", p_objInfo.m_strAnaesthesia.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AFTERNOTICE", p_objInfo.m_strAfterNotice.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DISCUSSNOTION", p_objInfo.m_strDiscussNotion.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONDATE", p_objInfo.m_strOperationDate.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();

            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region ����XML
        /// <summary>
        /// ����XML
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>���ɵ�XML</returns>
        private string m_strMakeNewMainXml(clsBeforeOperationSummaryInfo p_objInfo, bool blnIsAddNew)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("BeforeOperationSummary");

            if (!blnIsAddNew)
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objInfo.m_strOpenDate);

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objInfo.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objInfo.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objInfo.m_strCreateDate);
            m_objXmlWriter.WriteAttributeString("CREATEID", p_objInfo.m_strCreateID);
            m_objXmlWriter.WriteAttributeString("DIAGNOSEXML", p_objInfo.m_strDiagnoseXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIAGNOSEGISTXML", p_objInfo.m_strDiagnoseGistXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BODYINFOXML", p_objInfo.m_strBodyInfoXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALHANDLEXML", p_objInfo.m_strSpecialHandleXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PREPARATIONXML", p_objInfo.m_strPreparationXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATIENTNOTIONXML", p_objInfo.m_strPatientNotionXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANAESTHESIAXML", p_objInfo.m_strAnaesthesiaXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AFTERNOTICEXML", p_objInfo.m_strAfterNoticeXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DISCUSSNOTIONXML", p_objInfo.m_strDiscussNotionXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IFCONFIRM", "0");
            m_objXmlWriter.WriteAttributeString("STATUS", "0");

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();

            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region �������ʱ�Ƿ��һ�����
        /// <summary>
        /// �������ʱ�Ƿ��һ�����
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_blnIsAddNew"></param>
        /// <returns>
        /// ���������
        /// 0��ʧ�ܡ�
        /// 1���ɹ���
        /// </returns>
        public long m_lngCheckNewCreateDate(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out bool p_blnIsAddNew)
        {
            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsBeforeOperationSummaryService_m_lngCheckNewCreateDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_blnIsAddNew);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>
        /// ���������
        /// 0��ʧ�ܡ�
        /// 1���ɹ���
        /// </returns>
        public long m_lngAddNew(clsBeforeOperationSummaryInfo p_objMainInfo, clsBeforeOperationSummaryContentInfo p_objContentInfo)
        {
            string strMainXml = m_strMakeNewMainXml(p_objMainInfo, true);
            string strContentXml = m_strMakeNewContentXml(p_objContentInfo, true);

            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsBeforeOperationSummaryService_m_lngAddNew(strMainXml, strContentXml);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ����޸�ʱ��
        /// <summary>
        /// ����޸�ʱ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_blnIsLast"></param>
        /// <returns></returns>
        public long m_lngGetLastModifyDate(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate, out string strModifyUserID)
        {
            p_strLastModifyDate = "";
            strModifyUserID = "";

            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsBeforeOperationSummaryService_m_lngCheckLastModifyDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_strLastModifyDate, out strModifyUserID);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region �޸�
        public long m_lngModify(clsBeforeOperationSummaryInfo p_objMainInfo, clsBeforeOperationSummaryContentInfo p_objContentInfo)
        {
            string strMainXml = m_strMakeNewMainXml(p_objMainInfo, false);
            string strContentXml = m_strMakeNewContentXml(p_objContentInfo, false);

            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsBeforeOperationSummaryService_m_lngModify(strMainXml, strContentXml);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ɾ��

        public long m_lngDeleteRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsBeforeOperationSummaryService_m_lngDelete(p_strInPatientID, p_strInPatientDate, p_strOpenDate, MDIParent.OperatorID);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ���е�ʱ���¼
        public long m_lngGetOperationDateArr(string p_strInPatientID, string p_strInPatientDate, out string[] objCreateDateArr)
        {
            string strXML = "";
            int intRows = 0;
            objCreateDateArr = null;

            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetOperationDate(p_strInPatientID, p_strInPatientDate, out strXML, out intRows);

                objCreateDateArr = null;

                if (lngRes > 0 && intRows > 0)
                {
                    objCreateDateArr = new string[intRows];

                    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objCreateDateArr[intIndex] = DateTime.Parse(objReader.GetAttribute("CREATEDATE")).ToString("yyyy-MM-dd HH:mm:ss"); ;
                                    intIndex++;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    objCreateDateArr = new string[0];
                }
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }

        #endregion

        #region ���ʱ���Ӧ��������Ϣ
        ///���ʱ���Ӧ��������Ϣ
        public long m_lngGetSummary_All(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsBeforeOperationSummary_All objclsBeforeOperationSummary_All)
        {
            objclsBeforeOperationSummary_All = null;
            string strXML = "";
            int intRows = 0;

            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetSummaryInfo(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows);
                if (intRows > 0)
                {
                    objclsBeforeOperationSummary_All = new clsBeforeOperationSummary_All();
                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo = new clsBeforeOperationSummaryInfo();
                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo = new clsBeforeOperationSummaryContentInfo();


                    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {

                                    objclsBeforeOperationSummary_All.m_strFirstPrintDate = objReader.GetAttribute("FIRSTPRINTDATE");	//�״δ�ӡʱ��

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strInPatientID = p_strInPatientID;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strInPatientDate = p_strInPatientDate;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateDate = p_strCreateDate;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                                    if (objReader.GetAttribute("FIRSTPRINTDATE") == "")
                                        objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strFirstPrintDate = "";
                                    else
                                        objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strFirstPrintDate = DateTime.Parse(objReader.GetAttribute("FIRSTPRINTDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateID = objReader.GetAttribute("CREATEID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAfterNoticeXml = objReader.GetAttribute("AFTERNOTICEXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAnaesthesiaXml = objReader.GetAttribute("ANAESTHESIAXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strBodyInfoXml = objReader.GetAttribute("BODYINFOXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strConfirmReasonXMLString = objReader.GetAttribute("CONFIRMREASONXMLSTRING").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseGistXml = objReader.GetAttribute("DIAGNOSEGISTXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseXml = objReader.GetAttribute("DIAGNOSEXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiscussNotionXml = objReader.GetAttribute("DISCUSSNOTIONXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPatientNotionXml = objReader.GetAttribute("PATIENTNOTIONXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPreparationXml = objReader.GetAttribute("PREPARATIONXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strSpecialHandleXml = objReader.GetAttribute("SPECIALHANDLEXML").Replace('��', '\'');

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strInPatientID = p_strInPatientID;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strInPatientDate = p_strInPatientDate;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOpenDate = objReader.GetAttribute("OPENDATE");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strModifyDate = DateTime.Parse(objReader.GetAttribute("MODIFYDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorID = objReader.GetAttribute("OPERATEDOCTORID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorID = objReader.GetAttribute("CHARGEDOCTORID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorName = objReader.GetAttribute("OPERATEDOCTORNAME").Replace('��', '\'').Trim();
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorName = objReader.GetAttribute("CHARGEDOCTORNAME").Replace('��', '\'').Trim();

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnose = objReader.GetAttribute("DIAGNOSE").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnoseGist = objReader.GetAttribute("DIAGNOSEGIST").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strBodyInfo = objReader.GetAttribute("BODYINFO").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strSpecialHandle = objReader.GetAttribute("SPECIALHANDLE").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPreparation = objReader.GetAttribute("PREPARATION").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPatientNotion = objReader.GetAttribute("PATIENTNOTION").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAnaesthesia = objReader.GetAttribute("ANAESTHESIA").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAfterNotice = objReader.GetAttribute("AFTERNOTICE").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiscussNotion = objReader.GetAttribute("DISCUSSNOTION").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperationDate = objReader.GetAttribute("OPERATIONDATE").Replace('��', '\'');
                                }
                                break;
                        }
                    }

                }
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }


        ///���ʱ���Ӧ��������Ϣ
        public long m_lngGetDeletedSummary_All(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsBeforeOperationSummary_All objclsBeforeOperationSummary_All)
        {
            objclsBeforeOperationSummary_All = null;
            string strXML = "";
            int intRows = 0;

            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetDeletedSummaryInfo(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows);
                if (intRows > 0)
                {
                    objclsBeforeOperationSummary_All = new clsBeforeOperationSummary_All();
                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo = new clsBeforeOperationSummaryInfo();
                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo = new clsBeforeOperationSummaryContentInfo();


                    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {

                                    objclsBeforeOperationSummary_All.m_strFirstPrintDate = objReader.GetAttribute("FIRSTPRINTDATE");	//�״δ�ӡʱ��

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strInPatientID = p_strInPatientID;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strInPatientDate = p_strInPatientDate;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateDate = p_strCreateDate;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                                    if (objReader.GetAttribute("FIRSTPRINTDATE") == "")
                                        objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strFirstPrintDate = "";
                                    else
                                        objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strFirstPrintDate = DateTime.Parse(objReader.GetAttribute("FIRSTPRINTDATE")).ToString("yyyy-MM-dd HH:mm:ss");

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strCreateID = objReader.GetAttribute("CREATEID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAfterNoticeXml = objReader.GetAttribute("AFTERNOTICEXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAnaesthesiaXml = objReader.GetAttribute("ANAESTHESIAXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strBodyInfoXml = objReader.GetAttribute("BODYINFOXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strConfirmReasonXMLString = objReader.GetAttribute("CONFIRMREASONXMLSTRING").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseGistXml = objReader.GetAttribute("DIAGNOSEGISTXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseXml = objReader.GetAttribute("DIAGNOSEXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiscussNotionXml = objReader.GetAttribute("DISCUSSNOTIONXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPatientNotionXml = objReader.GetAttribute("PATIENTNOTIONXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPreparationXml = objReader.GetAttribute("PREPARATIONXML").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strSpecialHandleXml = objReader.GetAttribute("SPECIALHANDLEXML").Replace('��', '\'');

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strInPatientID = p_strInPatientID;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strInPatientDate = p_strInPatientDate;
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOpenDate = objReader.GetAttribute("OPENDATE");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strModifyDate = DateTime.Parse(objReader.GetAttribute("MODIFYDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorID = objReader.GetAttribute("OPERATEDOCTORID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorID = objReader.GetAttribute("CHARGEDOCTORID");
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorName = objReader.GetAttribute("OPERATEDOCTORNAME").Replace('��', '\'').Trim();
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorName = objReader.GetAttribute("CHARGEDOCTORNAME").Replace('��', '\'').Trim();

                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnose = objReader.GetAttribute("DIAGNOSE").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnoseGist = objReader.GetAttribute("DIAGNOSEGIST").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strBodyInfo = objReader.GetAttribute("BODYINFO").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strSpecialHandle = objReader.GetAttribute("SPECIALHANDLE").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPreparation = objReader.GetAttribute("PREPARATION").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPatientNotion = objReader.GetAttribute("PATIENTNOTION").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAnaesthesia = objReader.GetAttribute("ANAESTHESIA").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAfterNotice = objReader.GetAttribute("AFTERNOTICE").Replace('��', '\'');
                                    objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiscussNotion = objReader.GetAttribute("DISCUSSNOTION").Replace('��', '\'');
                                }
                                break;
                        }
                    }

                }
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }


        #endregion

        #region ���µ�ǰ���˵�ǰ����ʱ���ϵ��״δ�ӡʱ��
        /// <summary>
        /// ���µ�ǰ���˵�ǰ����ʱ���ϵ��״δ�ӡʱ�䣬����һ����¼
        /// </summary>		
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, string p_strFirstPrintDate)
        {//���µ�һ�δ�ӡʱ��		
            //clsBeforeOperationSummaryService m_objService =
            //    (clsBeforeOperationSummaryService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperationSummaryService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, p_strFirstPrintDate);
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }

        #endregion
    }

    //	#region ���������õ���
    //	/// <summary>
    //	/// 
    //	/// </summary>
    //	[Serializable]
    //	public class clsBeforeOperationSummaryInfo
    //	{
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strInPatientID;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strInPatientDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strCreateDate;
    //		public string m_strOpenDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strCreateID;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDiagnoseXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDiagnoseGistXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strBodyInfoXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strSpecialHandleXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strPreparationXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strPatientNotionXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strAnaesthesiaXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strAfterNoticeXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDiscussNotionXml;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strIfConfirm;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strConfirmReason;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strConfirmReasonXMLString;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strStatus;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDeActivedDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDeActivedOperatorID;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strFirstPrintDate;	
    //
    //	}
    //	
    //	/// <summary>
    //	/// 
    //	/// </summary>
    //	[Serializable]
    //	public class clsBeforeOperationSummaryContentInfo
    //	{
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strInPatientID;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strInPatientDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strOpenDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strModifyDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strModifyUserID;
    //		
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strOperateDoctorID;
    //		public string m_strOperateDoctorName;//�����Ӧ�����ƣ����ڶ���ʱʹ��
    //		
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strChargeDoctorID;
    //		public string m_strChargeDoctorName;//�����Ӧ�����ƣ����ڶ���ʱʹ��
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDiagnose;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strDiagnoseGist;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strBodyInfo;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strSpecialHandle;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strPreparation;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strPatientNotion;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strAnaesthesia;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strAfterNotice;		
    //
    //		public string m_strDiscussNotion;
    //
    //	}
    //
    //	/// <summary>
    //	/// ��ǰС��������Ϣ
    //	/// </summary>
    //	[Serializable]
    //	public class clsBeforeOperationSummary_All
    //	{
    //		public string m_strFirstPrintDate;//�״δ�ӡʱ��
    //		public clsBeforeOperationSummaryInfo m_objclsBeforeOperationSummaryInfo;
    //		public clsBeforeOperationSummaryContentInfo m_objclsBeforeOperationSummaryContentInfo;
    //	}
    //	#endregion
}
