using System;
using System.Xml;
using System.IO;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsMRIApplyDomain.
    /// </summary>
    public class clsMRIApplyDomain
    {
        //private clsMRIApplyServ m_objServ=new clsMRIApplyServ();
        private XmlParserContext m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
        private MemoryStream m_objXmlMemStream;
        private XmlTextWriter m_objXmlWriter;
        public clsMRIApplyDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
        }
        /// <summary>
        /// ɾ������ dick 2003-3-27
        /// </summary>
        /// <param name="p_strDeactiveUserID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate,
            string p_strCreateDate)
        {
            //clsSPECTCheckOrderServ m_objServ =
            //    (clsSPECTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsSPECTCheckOrderServ));

            long lngRes = 0;
            try
            {
                string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientID='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " CreateDate='" + p_strCreateDate + "'" + " />";
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeActive(strDeactiveXML, "MRIApply");
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        ///ֻ��Ҫ���ʱ��
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;
            DateTime[] dtmCreateRecordDateArr = null;

            //clsMRIApplyServ m_objServ =
            //    (clsMRIApplyServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMRIApplyServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAllCreateDate(p_strInPatientID, p_strInPatientDate, out strXml, out intRows);

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


        public string[] m_strGetAllCreateDateArr(string p_strInPatientID, string p_strInPatientDate)
        {
            string strXML = "";
            int intRows = 0;
            long lngResult = 0;

            //clsMRIApplyServ m_objServ =
            //    (clsMRIApplyServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMRIApplyServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAllCreateDate(p_strInPatientID, p_strInPatientDate, out strXML, out intRows);
                if (intRows > 0)
                {
                    string[] strDateArr = new string[intRows];

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
                                    strDateArr[intIndex++] = DateTime.Parse(objReader.GetAttribute("CREATEDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                break;
                        }
                    }
                    return strDateArr;
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return null;
        }

        ///���ʱ���Ӧ��������Ϣ
        public clsMRIApply_All m_objGetMRIApply_All(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strXML = "";
            int intRows = 0;
            string strSubXML = "";
            int intSubRows = 0;
            string strSub2XML = "";
            int intSub2Rows = 0;
            clsMRIApply_All objclsMRIApply_All = null;

            //clsMRIApplyServ m_objServ =
            //    (clsMRIApplyServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMRIApplyServ));

            try
            {
                long lngResult = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetMRIApply_All(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows, out strSubXML, out intSubRows, out strSub2XML, out intSub2Rows);
                if (intRows == 1)
                {
                    objclsMRIApply_All = new clsMRIApply_All();
                    objclsMRIApply_All.m_objclsMRIApply = new clsMRIApply();
                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr = new clsMRIApply_MRRoom[intSubRows];
                    objclsMRIApply_All.m_strOperationHistoryTimeArr = new string[intSub2Rows];

                    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objclsMRIApply_All.m_objclsMRIApply.m_strInPatientID = p_strInPatientID;
                                    objclsMRIApply_All.m_objclsMRIApply.m_strInPatientDate = p_strInPatientDate;
                                    objclsMRIApply_All.m_objclsMRIApply.m_strCreateDate = p_strCreateDate;
                                    objclsMRIApply_All.m_objclsMRIApply.m_strModifyDate = objReader.GetAttribute("MODIFYDATE");
                                    objclsMRIApply_All.m_objclsMRIApply.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID");
                                    objclsMRIApply_All.m_objclsMRIApply.m_strCreateUserName = objReader.GetAttribute("CREATEUSERNAME");//
                                    objclsMRIApply_All.m_objclsMRIApply.m_strApplyDeptID = objReader.GetAttribute("APPLYDEPTID");
                                    objclsMRIApply_All.m_objclsMRIApply.m_strApplyDeptName = objReader.GetAttribute("APPLYDEPTNAME");//
                                    objclsMRIApply_All.m_objclsMRIApply.m_strCheckPrice = objReader.GetAttribute("CHECKPRICE").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strMR_ID = objReader.GetAttribute("MR_ID").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strSicknessHistoryAndBodyCharacter = objReader.GetAttribute("SICKHISTANDBODYCHARACTER").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strOtherCheckResultAndRegisterID = objReader.GetAttribute("OTHERCHECKRESULTANDREGISTERID").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strClinicDiagnose = objReader.GetAttribute("CLINICDIAGNOSE").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strCheckPart = objReader.GetAttribute("CHECKPART").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strHasOperationHistory = objReader.GetAttribute("HASOPERATIONHISTORY").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strHasMetalInBodyAndPart = objReader.GetAttribute("HASMETALINBODYANDPART").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strMakeShadowQty = objReader.GetAttribute("MAKESHADOWQTY").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strPatientReactionInScan = objReader.GetAttribute("PATIENTREACTIONINSCAN").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strScanTime = objReader.GetAttribute("SCANTIME").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply.m_strTechnicianSignID = objReader.GetAttribute("TECHNICIANSIGNID");
                                    objclsMRIApply_All.m_objclsMRIApply.m_strTechnicianSignName = objReader.GetAttribute("TECHNICIANSIGNNAME");//
                                    objclsMRIApply_All.m_objclsMRIApply.m_strWeight = objReader.GetAttribute("WEITHT");

                                }
                                break;
                        }
                    }


                    objReader = new XmlTextReader(strSubXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex] = new clsMRIApply_MRRoom();
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strInPatientID = p_strInPatientID;
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strInPatientDate = p_strInPatientDate;
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strCreateDate = p_strCreateDate;
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strModifyDate = objReader.GetAttribute("MODIFYDATE");
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strSerialNO = objReader.GetAttribute("SERIALNO");
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strPartAndLine = objReader.GetAttribute("PARTANDLINE").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strPulseSerial = objReader.GetAttribute("PULSESERIAL").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strParam = objReader.GetAttribute("PARAM").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strFix = objReader.GetAttribute("FIX").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strLayerNum = objReader.GetAttribute("LAYERNUM").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strLayerHeight = objReader.GetAttribute("LAYERHEIGHT").Replace('��', '\'');
                                    objclsMRIApply_All.m_objclsMRIApply_MRRoomArr[intIndex].m_strLayerDistance = objReader.GetAttribute("LAYERDISTANCE").Replace('��', '\'');
                                    intIndex++;
                                }
                                break;
                        }
                    }


                    objReader = new XmlTextReader(strSub2XML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    //objclsMRIApply_All.m_strOperationHistoryTimeArr[intIndex]=new string();
                                    objclsMRIApply_All.m_strOperationHistoryTimeArr[intIndex] = objReader.GetAttribute("OPERATIONHISTORYTIME");
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

            return objclsMRIApply_All;
        }


        /// ������Ϣ		
        public long m_lngSave(clsMRIApply_All p_objclsMRIApply_All, bool p_blnIsAddNew)
        {
            long lngRes = 0;

            //clsMRIApplyServ m_objServ =
            //    (clsMRIApplyServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMRIApplyServ));

            try
            {
                if (p_blnIsAddNew == true)
                    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngAddNew(m_strGetMainXML(p_objclsMRIApply_All.m_objclsMRIApply), m_strGetSubXML(p_objclsMRIApply_All.m_objclsMRIApply_MRRoomArr), m_strGetOperationDateXML(p_objclsMRIApply_All));
                else
                    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngModify(m_strGetMainXML(p_objclsMRIApply_All.m_objclsMRIApply), m_strGetSubXML(p_objclsMRIApply_All.m_objclsMRIApply_MRRoomArr), m_strGetOperationDateXML(p_objclsMRIApply_All));

            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        private string m_strGetMainXML(clsMRIApply p_objclsMRIApply)
        {

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");

            try
            {
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsMRIApply.m_strInPatientID);
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsMRIApply.m_strInPatientDate);
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsMRIApply.m_strCreateDate);
                m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsMRIApply.m_strModifyDate);
                m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objclsMRIApply.m_strCreateUserID);

                m_objXmlWriter.WriteAttributeString("APPLYDEPTID", p_objclsMRIApply.m_strApplyDeptID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CHECKPRICE", p_objclsMRIApply.m_strCheckPrice.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("MR_ID", p_objclsMRIApply.m_strMR_ID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SICKHISTANDBODYCHARACTER", p_objclsMRIApply.m_strSicknessHistoryAndBodyCharacter.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OTHERCHECKRESULTANDREGISTERID", p_objclsMRIApply.m_strOtherCheckResultAndRegisterID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CLINICDIAGNOSE", p_objclsMRIApply.m_strClinicDiagnose.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CHECKPART", p_objclsMRIApply.m_strCheckPart.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("HASOPERATIONHISTORY", p_objclsMRIApply.m_strHasOperationHistory.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("HASMETALINBODYANDPART", p_objclsMRIApply.m_strHasMetalInBodyAndPart.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("MAKESHADOWQTY", p_objclsMRIApply.m_strMakeShadowQty.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PATIENTREACTIONINSCAN", p_objclsMRIApply.m_strPatientReactionInScan.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SCANTIME", p_objclsMRIApply.m_strScanTime.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("TECHNICIANSIGNID", p_objclsMRIApply.m_strTechnicianSignID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("WEITHT", p_objclsMRIApply.m_strWeight.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("IFCONFIRM", "0");//p_objclsWatchItemRecord.m_strIfConfirm);
                                                                      //	���ޣ�����			m_objXmlWriter.WriteAttributeString("CONFIRMREASON", p_objclsWatchItemRecord.m_strConfirmReason.Replace('\'','��'));
                                                                      //	���ޣ�����			m_objXmlWriter.WriteAttributeString("CONFIRMREASONXML", p_objclsWatchItemRecord.m_strConfirmReasonXML.Replace('\'','��'));

                m_objXmlWriter.WriteAttributeString("STATUS", "0");
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
            }

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);

        }

        private string[] m_strGetSubXML(clsMRIApply_MRRoom[] p_objclsMRIApply_MRRoomArr)
        {
            if (p_objclsMRIApply_MRRoomArr == null)
                return null;
            string[] strXMLArr = new String[p_objclsMRIApply_MRRoomArr.Length];

            for (int i = 0; i < p_objclsMRIApply_MRRoomArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("Model");
                try
                {
                    m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsMRIApply_MRRoomArr[i].m_strInPatientID);
                    m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsMRIApply_MRRoomArr[i].m_strInPatientDate);
                    m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsMRIApply_MRRoomArr[i].m_strCreateDate);
                    m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsMRIApply_MRRoomArr[i].m_strModifyDate);

                    m_objXmlWriter.WriteAttributeString("SERIALNO", p_objclsMRIApply_MRRoomArr[i].m_strSerialNO.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("PARTANDLINE", p_objclsMRIApply_MRRoomArr[i].m_strPartAndLine.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("PULSESERIAL", p_objclsMRIApply_MRRoomArr[i].m_strPulseSerial.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("PARAM", p_objclsMRIApply_MRRoomArr[i].m_strParam.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("FIX", p_objclsMRIApply_MRRoomArr[i].m_strFix.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("LAYERNUM", p_objclsMRIApply_MRRoomArr[i].m_strLayerNum.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("LAYERHEIGHT", p_objclsMRIApply_MRRoomArr[i].m_strLayerHeight.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("LAYERDISTANCE", p_objclsMRIApply_MRRoomArr[i].m_strLayerDistance.Replace('\'', '��'));

                }
                catch (Exception ex)
                {
                    clsPublicFunction.ShowInformationMessageBox(ex.Message);
                }
                //�����ڴ��ֶΣ�m_objXmlWriter.WriteAttributeString("STATUS", "0");
                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return strXMLArr;
        }

        private string[] m_strGetOperationDateXML(clsMRIApply_All p_objclsMRIApply_All)
        {
            if (p_objclsMRIApply_All.m_strOperationHistoryTimeArr == null)
                return null;
            string[] strXMLArr = new String[p_objclsMRIApply_All.m_strOperationHistoryTimeArr.Length];

            for (int i = 0; i < p_objclsMRIApply_All.m_strOperationHistoryTimeArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("Model");
                try
                {
                    m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsMRIApply_All.m_objclsMRIApply.m_strInPatientID);
                    m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsMRIApply_All.m_objclsMRIApply.m_strInPatientDate);
                    m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsMRIApply_All.m_objclsMRIApply.m_strCreateDate);
                    m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsMRIApply_All.m_objclsMRIApply.m_strModifyDate);

                    m_objXmlWriter.WriteAttributeString("OPERATIONHISTORYTIME", p_objclsMRIApply_All.m_strOperationHistoryTimeArr[i]);

                }
                catch (Exception ex)
                {
                    clsPublicFunction.ShowInformationMessageBox(ex.Message);
                }
                //�����ڴ��ֶΣ�m_objXmlWriter.WriteAttributeString("STATUS", "0");
                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return strXMLArr;
        }

    }


    public class clsMRIApply
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";
        public string m_strCreateUserID = "";
        public string m_strCreateUserName = "";//������ȡʹ��

        public string m_strApplyDeptID = "";
        public string m_strApplyDeptName = "";//������ȡʹ��	

        public string m_strCheckPrice = "";
        public string m_strMR_ID = "";
        public string m_strSicknessHistoryAndBodyCharacter = "";
        public string m_strOtherCheckResultAndRegisterID = "";
        public string m_strClinicDiagnose = "";
        public string m_strCheckPart = "";
        public string m_strHasOperationHistory = "";
        public string m_strHasMetalInBodyAndPart = "";
        public string m_strMakeShadowQty = "";
        public string m_strPatientReactionInScan = "";
        public string m_strScanTime = "";
        public string m_strTechnicianSignID = "";
        public string m_strTechnicianSignName = "";//������ȡʹ��
        public string m_strWeight = "";
    }

    public class clsMRIApply_MRRoom
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";
        public string m_strSerialNO = "";
        public string m_strPartAndLine = "";
        public string m_strPulseSerial = "";
        public string m_strParam = "";
        public string m_strFix = "";
        public string m_strLayerNum = "";
        public string m_strLayerHeight = "";
        public string m_strLayerDistance = "";
    }
    public class clsMRIApply_All
    {
        public clsMRIApply m_objclsMRIApply;
        public clsMRIApply_MRRoom[] m_objclsMRIApply_MRRoomArr;
        public string[] m_strOperationHistoryTimeArr;
    }
}
