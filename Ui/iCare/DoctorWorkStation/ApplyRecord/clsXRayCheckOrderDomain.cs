using System;
using System.IO;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsXRayCheckOrderDomain.
    /// </summary>

    public class clsXRayCheckOrderDomain
    {
        #region Member
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

        //private clsXRayCheckOrderServ m_objServ;
        #endregion

        #region Constructor
        public clsXRayCheckOrderDomain()
        {
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

            //m_objServ = new clsXRayCheckOrderServ();
        }
        #endregion

        #region ��ȡ��ʼ����Ϣ
        /// <summary>
        /// �������CreateDate
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[] dtmCreateRecordDateArr = null;

            //clsXRayCheckOrderServ m_objServ =
            //    (clsXRayCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsXRayCheckOrderServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsXRayCheckOrderServ_m_lngGetTimeInfoOfAPatient(p_strInPatientID, p_strInPatientDate, ref strXml, ref intRows);

                if (lngRes > 0 && intRows > 0)
                {
                    dtmCreateRecordDateArr = new DateTime[intRows];

                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    dtmCreateRecordDateArr[intIndex] = DateTime.Parse(objReader.GetAttribute("CREATEDATE"));
                                    intIndex++;
                                }
                                break;
                        }
                    }
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return dtmCreateRecordDateArr;
        }
        #endregion

        #region Save
        public long m_lngSave(clsXRayCheckOrder p_objXRayCheckOrder, clsXRayCommonRecord[] p_objXRayCommonRecordArr, clsXRaySpecialRecord[] p_objXRaySpecialRecordArr, clsXRayOperatorID[] p_objOperatorIDArr,
                             ImageRequest p_objImageRequest, ref string p_strApplicationID, bool p_bnlIsNew)

        {
            /*///////////����PS_ImageApplication���еļ�¼///////////*/

            string m_strApplicationID = "";
            long m_lngRe;


            if (p_objXRayCheckOrder == null)
                return -1;
            else
                if (p_bnlIsNew == false)
                m_strApplicationID = p_objXRayCheckOrder.m_strApplicationID;

            if (p_objXRayCheckOrder == null)
                return -1;

            /*/////////////////////////////*/
            //clsXRayCheckOrderServ m_objServ =
            //    (clsXRayCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsXRayCheckOrderServ));

            try
            {
                string strOrderXML = m_strOrderXML(p_objXRayCheckOrder);

                string[] strCommonXML = m_strCommonXML(p_objXRayCommonRecordArr);

                string[] strSpecialXML = m_strSpecialXML(p_objXRaySpecialRecordArr);

                string[] strOperatorXML = m_strOperatorXML(p_objOperatorIDArr);

                m_lngRe = (new weCare.Proxy.ProxyEmr03()).Service.m_lngAddNew(strOrderXML, strCommonXML,
                                             strSpecialXML, strOperatorXML, p_objImageRequest, ref m_strApplicationID, p_bnlIsNew);
                p_strApplicationID = m_strApplicationID;
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRe;

        }

        private string[] m_strOperatorXML(clsXRayOperatorID[] p_objOperatorIDArr)
        {
            if (p_objOperatorIDArr == null)
                return null;

            string[] strXMLArr = new string[p_objOperatorIDArr.Length];

            for (int i = 0; i < p_objOperatorIDArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOperatorIDArr[i].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOperatorIDArr[i].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objOperatorIDArr[i].m_strCreateDate.Replace('\'', '��'));
                //				m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objOperatorIDArr[i].m_strModifyDate.Replace('\'','��'));

                m_objXmlWriter.WriteAttributeString("OPERATORID", p_objOperatorIDArr[i].m_strOperatorID.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return strXMLArr;
        }

        private string[] m_strSpecialXML(clsXRaySpecialRecord[] p_objXRaySpecialRecordArr)
        {
            if (p_objXRaySpecialRecordArr == null)
                return null;

            string[] strXMLArr = new string[p_objXRaySpecialRecordArr.Length];

            for (int i = 0; i < p_objXRaySpecialRecordArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objXRaySpecialRecordArr[i].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objXRaySpecialRecordArr[i].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objXRaySpecialRecordArr[i].m_strCreateDate.Replace('\'', '��'));
                //				m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objXRaySpecialRecordArr[i].m_strModifyDate.Replace('\'','��'));

                m_objXmlWriter.WriteAttributeString("PHOTOID", p_objXRaySpecialRecordArr[i].m_strPhotoID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CHECKPLACE", p_objXRaySpecialRecordArr[i].m_strCheckPlace.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PHOTOSEQ", p_objXRaySpecialRecordArr[i].m_strPhotoSeq.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("TIMEOFAFTERINJECT", p_objXRaySpecialRecordArr[i].m_strTimeOfAfterInject.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FISRTOPERATORID", p_objXRaySpecialRecordArr[i].m_strFisrtOperatorID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("REMARK", p_objXRaySpecialRecordArr[i].m_strRemark.Replace('\'', '��'));


                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }

            return strXMLArr;
        }

        private string[] m_strCommonXML(clsXRayCommonRecord[] p_objXRayCommonRecordArr)
        {
            if (p_objXRayCommonRecordArr == null)
                return null;

            string[] strXMLArr = new string[p_objXRayCommonRecordArr.Length];

            for (int i = 0; i < p_objXRayCommonRecordArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objXRayCommonRecordArr[i].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objXRayCommonRecordArr[i].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objXRayCommonRecordArr[i].m_strCreateDate.Replace('\'', '��'));
                //				m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objXRayCommonRecordArr[i].m_strModifyDate.Replace('\'','��'));

                m_objXmlWriter.WriteAttributeString("PHOTOID", p_objXRayCommonRecordArr[i].m_strPhotoID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CHECKPLACE", p_objXRayCommonRecordArr[i].m_strCheckPlace.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("MAPPINGPLACE", p_objXRayCommonRecordArr[i].m_strMappingPlace.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PHOTOAREA", p_objXRayCommonRecordArr[i].m_strPhotoArea.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PHOTOTHICKNESS", p_objXRayCommonRecordArr[i].m_strPhotoThickness.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DISTANCE", p_objXRayCommonRecordArr[i].m_strDistance.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("VOLTAGE", p_objXRayCommonRecordArr[i].m_strVoltage.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ELECTRICITY", p_objXRayCommonRecordArr[i].m_strElectricity.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DISPOSETIME", p_objXRayCommonRecordArr[i].m_strDisposeTime.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BUCKY", p_objXRayCommonRecordArr[i].m_strBucky.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }

            return strXMLArr;
        }


        private string m_strOrderXML(clsXRayCheckOrder p_objXRayCheckOrder)
        {
            if (p_objXRayCheckOrder == null)
                return null;

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objXRayCheckOrder.m_strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objXRayCheckOrder.m_strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objXRayCheckOrder.m_strCreateDate.Replace('\'', '��'));
            //			m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objXRayCheckOrder.m_strModifyDate.Replace('\'','��'));
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objXRayCheckOrder.m_strCreateUserID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", p_objXRayCheckOrder.m_strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IFCONFIRM", p_objXRayCheckOrder.m_strIfConfirm.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("HISTORY", p_objXRayCheckOrder.m_strHistory.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CLINICALCHECKANDRESULT", p_objXRayCheckOrder.m_strClinicalCheckAndResult.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CLINICALDIGNOSE", p_objXRayCheckOrder.m_strClinicalDignose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKAIM", p_objXRayCheckOrder.m_strCheckAim.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKPLACE", p_objXRayCheckOrder.m_strCheckPlace.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CLAIRVOYANCE", p_objXRayCheckOrder.m_strClairvoyance.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHOTO", p_objXRayCheckOrder.m_strPhoto.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NOTHAVEOLDPHOTO", p_objXRayCheckOrder.m_strNotHaveOldPhoto.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEOLDPHOTO", p_objXRayCheckOrder.m_strHaveOldPhoto.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEOLDPHOTOOUT", p_objXRayCheckOrder.m_strHaveOldPhotoOut.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHARGE", p_objXRayCheckOrder.m_strCharge.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ADDITIONCHARGE", p_objXRayCheckOrder.m_strAdditionCharge.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKPARTSELECTION", p_objXRayCheckOrder.m_strCheckPartSelection.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XRAYNO", p_objXRayCheckOrder.m_strXRayNo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INSURANCENO", p_objXRayCheckOrder.m_strInsuranceNo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERCHECKINFO", p_objXRayCheckOrder.m_strOtherCheckInfo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CONTACTINFO", p_objXRayCheckOrder.m_strContactInfo.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsXRayCheckOrder m_objGetXRayCheckOrder(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return null;

            clsXRayCheckOrder objXRayCheckOrder = null;

            //clsXRayCheckOrderServ m_objServ =
            //    (clsXRayCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsXRayCheckOrderServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.GetXRayCheckOrder(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

                if (lngRes > 0 && intRows > 0)
                {

                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objXRayCheckOrder = new clsXRayCheckOrder();

                                    objXRayCheckOrder.m_strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strCreateDate = objReader.GetAttribute("CREATEDATE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strStatus = objReader.GetAttribute("STATUS").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strIfConfirm = objReader.GetAttribute("IFCONFIRM").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").ToString().Replace('��', '\''); ;
                                    objXRayCheckOrder.m_strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('��', '\'');//ben 2003-4-29

                                    objXRayCheckOrder.m_strHistory = objReader.GetAttribute("HISTORY").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strClinicalCheckAndResult = objReader.GetAttribute("CLINICALCHECKANDRESULT").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strClinicalDignose = objReader.GetAttribute("CLINICALDIGNOSE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strCheckAim = objReader.GetAttribute("CHECKAIM").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strCheckPlace = objReader.GetAttribute("CHECKPLACE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strClairvoyance = objReader.GetAttribute("CLAIRVOYANCE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strPhoto = objReader.GetAttribute("PHOTO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strNotHaveOldPhoto = objReader.GetAttribute("NOTHAVEOLDPHOTO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strHaveOldPhoto = objReader.GetAttribute("HAVEOLDPHOTO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strHaveOldPhotoOut = objReader.GetAttribute("HAVEOLDPHOTOOUT").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strCharge = objReader.GetAttribute("CHARGE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strAdditionCharge = objReader.GetAttribute("ADDITIONCHARGE").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strCheckPartSelection = objReader.GetAttribute("CHECKPARTSELECTION").ToString().Replace('��', '\'');

                                    objXRayCheckOrder.m_strXRayNo = objReader.GetAttribute("XRAYNO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strInsuranceNo = objReader.GetAttribute("INSURANCENO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strOtherCheckInfo = objReader.GetAttribute("OTHERCHECKINFO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strContactInfo = objReader.GetAttribute("CONTACTINFO").ToString().Replace('��', '\'');
                                    objXRayCheckOrder.m_strApplicationID = objReader.GetAttribute("APPLICATIONID").ToString().Replace('��', '\'');

                                }
                                break;
                        }
                    }
                }

            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objXRayCheckOrder;

        }

        /// <summary>
        /// ��ȡ�ӱ���Ϣ -- CommonRecord
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsXRayCommonRecord[] m_objGetCommonRecordArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            clsXRayCommonRecord[] objXRayCommonRecordArr = null;

            //clsXRayCheckOrderServ m_objServ =
            //    (clsXRayCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsXRayCheckOrderServ));

            try
            {
                int m_intReturnRows = 0;
                string m_strReceivedXML = "";

                long lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.GetXRayCommonRecor(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    objXRayCommonRecordArr = new clsXRayCommonRecord[m_intReturnRows];

                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objXRayCommonRecordArr[intIndex] = new clsXRayCommonRecord();

                                    objXRayCommonRecordArr[intIndex].m_strPhotoID = objReader.GetAttribute("PHOTOID").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strCheckPlace = objReader.GetAttribute("CHECKPLACE").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strMappingPlace = objReader.GetAttribute("MAPPINGPLACE").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strPhotoArea = objReader.GetAttribute("PHOTOAREA").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strPhotoThickness = objReader.GetAttribute("PHOTOTHICKNESS").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strDistance = objReader.GetAttribute("DISTANCE").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strVoltage = objReader.GetAttribute("VOLTAGE").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strElectricity = objReader.GetAttribute("ELECTRICITY").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strDisposeTime = objReader.GetAttribute("DISPOSETIME").ToString().Replace('��', '\'');
                                    objXRayCommonRecordArr[intIndex].m_strBucky = objReader.GetAttribute("BUCKY").ToString().Replace('��', '\'');

                                    intIndex++;

                                }
                                break;
                        }
                    }

                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objXRayCommonRecordArr;
        }

        /// <summary>
        /// ��ȡ�ӱ���Ϣ -- SpecialRecord
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsXRaySpecialRecord[] m_objGetSpecialRecordArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            clsXRaySpecialRecord[] objXRaySpecialRecordArr = null;

            //clsXRayCheckOrderServ m_objServ =
            //    (clsXRayCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsXRayCheckOrderServ));

            try
            {
                int m_intReturnRows = 0;
                string m_strReceivedXML = "";

                long lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.GetXRaySpecialRecor(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    objXRaySpecialRecordArr = new clsXRaySpecialRecord[m_intReturnRows];

                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objXRaySpecialRecordArr[intIndex] = new clsXRaySpecialRecord();

                                    objXRaySpecialRecordArr[intIndex].m_strPhotoID = objReader.GetAttribute("PHOTOID").ToString().Replace('��', '\'');
                                    objXRaySpecialRecordArr[intIndex].m_strPhotoSeq = objReader.GetAttribute("PHOTOSEQ").ToString().Replace('��', '\'');
                                    objXRaySpecialRecordArr[intIndex].m_strCheckPlace = objReader.GetAttribute("CHECKPLACE").ToString().Replace('��', '\'');
                                    objXRaySpecialRecordArr[intIndex].m_strTimeOfAfterInject = objReader.GetAttribute("TIMEOFAFTERINJECT").ToString().Replace('��', '\'');
                                    objXRaySpecialRecordArr[intIndex].m_strFisrtOperatorID = objReader.GetAttribute("FISRTOPERATORID").ToString().Replace('��', '\'');
                                    objXRaySpecialRecordArr[intIndex].m_strRemark = objReader.GetAttribute("REMARK").ToString().Replace('��', '\'');


                                    intIndex++;

                                }
                                break;
                        }
                    }

                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objXRaySpecialRecordArr;
        }

        /// <summary>
        /// ��ȡ�ӱ���Ϣ -- SpecialRecord
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsXRayOperatorID[] m_objGetOperatorIDArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            clsXRayOperatorID[] objXRayOperatorIDArr = null;

            //clsXRayCheckOrderServ m_objServ =
            //    (clsXRayCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsXRayCheckOrderServ));

            try
            {
                int m_intReturnRows = 0;
                string m_strReceivedXML = "";

                long lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.GetXRayOperator(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    objXRayOperatorIDArr = new clsXRayOperatorID[m_intReturnRows];

                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objXRayOperatorIDArr[intIndex] = new clsXRayOperatorID();

                                    objXRayOperatorIDArr[intIndex].m_strOperatorID = objReader.GetAttribute("OPERATORID");

                                    intIndex++;

                                }
                                break;
                        }
                    }

                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objXRayOperatorIDArr;
        }
        #endregion

        #region ɾ��
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate,
            string p_strCreateDate)
        {
            long lngRes = 0;

            //clsSPECTCheckOrderServ m_objServ =
            //    (clsSPECTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsSPECTCheckOrderServ));

            try
            {
                string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientID='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " CreateDate='" + p_strCreateDate + "'" + " />";
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeActive(strDeactiveXML, "XRayCheckOrder");
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion
    }


    /// <summary>
    /// ���������
    /// </summary>
    public class clsXRayCheckOrder
    {
        public string m_strInPatientID;
        public string m_strInPatientDate;
        public string m_strCreateDate;
        public string m_strModifyDate;

        public string m_strCreateUserID;
        public string m_strIfConfirm;
        public string m_strConfirmReason;
        public string m_strConfirmReasonXML;
        public string m_strFirstPrintDate;
        public string m_strDeActivedDate;
        public string m_strDeActivedOperatorID;
        public string m_strStatus;

        public string m_strHistory;
        public string m_strClinicalCheckAndResult;
        public string m_strClinicalDignose;
        public string m_strCheckAim;
        public string m_strCheckPlace;
        public string m_strClairvoyance;
        public string m_strPhoto;
        public string m_strNotHaveOldPhoto;
        public string m_strHaveOldPhoto;
        public string m_strHaveOldPhotoOut;
        public string m_strCharge;
        public string m_strAdditionCharge;
        public string m_strCheckPartSelection = "";
        /// <summary>
        /// X���
        /// </summary>
        public string m_strXRayNo = "";
        /// <summary>
        /// ҽ������
        /// </summary>
        public string m_strInsuranceNo = "";

        /// <summary>
        /// �������
        /// </summary>
        public string m_strOtherCheckInfo = "";

        /// <summary>
        ///�ܼ���סַ����ϵ�绰
        /// </summary>
        public string m_strContactInfo = "";

        /// <summary>
        /// ���뵥��
        /// </summary>
        public string m_strApplicationID = "";



    }

    /// <summary>
    /// �ӱ�--��ͨ��Ƭ��¼������
    /// </summary>
    public class clsXRayCommonRecord
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";

        public string m_strPhotoID = "";
        public string m_strCheckPlace = "";
        public string m_strMappingPlace = "";
        public string m_strPhotoArea = "";
        public string m_strPhotoThickness = "";
        public string m_strDistance = "";
        public string m_strVoltage = "";
        public string m_strElectricity = "";
        public string m_strDisposeTime = "";
        public string m_strBucky = "";
    }

    /// <summary>
    /// �ӱ�--������Ӱ��¼������
    /// </summary>
    public class clsXRaySpecialRecord
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";

        public string m_strPhotoID = "";
        public string m_strPhotoSeq = "";
        public string m_strCheckPlace = "";
        public string m_strTimeOfAfterInject = "";
        public string m_strFisrtOperatorID = "";
        public string m_strRemark = "";
    }

    /// <summary>
    /// �ӱ� -- OperatorID
    /// </summary>
    public class clsXRayOperatorID
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";

        public string m_strOperatorID = "";
    }
}
