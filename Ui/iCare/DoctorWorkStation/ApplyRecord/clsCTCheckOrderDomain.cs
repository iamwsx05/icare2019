using System;
using System.IO;
using System.Xml;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsCTCheckOrderDomain.
    /// </summary>
    public class clsCTCheckOrderDomain
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
        //private com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ  m_objServ;

        /// <summary>
        /// ���е�CT����ʱ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;
            DateTime[] dtmCreateRecordDateArr = null;

            //com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ m_objServ =
            //    (com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsCTCheckOrderServ_m_lngGetTimeInfoOfAPatient(p_strInPatientID, p_strInPatientDate, ref strXml, ref intRows);

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

        /// <summary>
        /// ���е�CT����ʱ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;
            DateTime[] dtmCreateRecordDateArr = null;

            //com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ m_objServ =
            //    (com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsCTCheckOrderServ_m_lngGetTimeInfoOfAPatient(p_strInPatientID, null, ref strXml, ref intRows);

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

        public clsCTCheckOrderDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�

            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);

            //m_objServ =new com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ(); 

        }


        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate,
            string p_strCreateDate)
        {
            //clsSPECTCheckOrderServ m_objServ =
            //    (clsSPECTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsSPECTCheckOrderServ));

            long lngRes = 0;
            try
            {
                string strDeactiveXML = "<Deactive STATUS='1' DEACTIVEDOPERATORID='" + p_strDeactiveUserID + "' INPATIENTID='" + p_strInPatientID + "'" + " INPATIENTDATE='" + p_strInPatientDate + "'" + " CREATEDATE='" + p_strCreateDate + "'" + " />";
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeActive(strDeactiveXML, "CTCheckOrder");
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        //
        //		//�������������RequestImage,ApplicationID��
        //		public long lngSave(clsCTCheckOrder m_objCTCheckOrderContent)
        //
        //		{			
        //			string strXML=this.strSaveXML(m_objCTCheckOrderContent); 
        //			long lngSucceed=m_objServ.m_lngAddNewRecord( strXML); 
        //
        //			return lngSucceed;
        //		}

        public long lngSave(clsCTCheckOrder m_objCTCheckOrderContent, bool p_bnlIsAddNew,
                            ImageRequest p_objImageRequest, ref string p_strApplicationID)

        {
            string m_strApplicationID = "";

            if (p_bnlIsAddNew == false)
            {
                if (m_objCTCheckOrderContent != null)
                    //m_strApplicationID=m_objCTCheckOrderContent.strApplicationID;
                    m_strApplicationID = p_strApplicationID;

            }
            else
            {
                m_lngGetApplicationid(ref m_strApplicationID);
            }
            long lngSucceed = 0;
            //com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ m_objServ =
            //    (com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ));

            try
            {
                string strXML = this.strSaveXML(m_objCTCheckOrderContent);
                lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.m_lngSaveRecord(strXML,
                    p_objImageRequest, ref m_strApplicationID, p_bnlIsAddNew);

                p_strApplicationID = m_strApplicationID;
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngSucceed;
        }

        private string strSaveXML(clsCTCheckOrder m_objCTCheckOrderContent)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", m_objCTCheckOrderContent.strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", m_objCTCheckOrderContent.strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CREATEDATE", m_objCTCheckOrderContent.strCreateDate.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("IFCONFIRM", m_objCTCheckOrderContent.strIfConfirm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", m_objCTCheckOrderContent.strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", m_objCTCheckOrderContent.strCreateUserID.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ADVANCEID", m_objCTCheckOrderContent.strAdvanceID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ADVANCETIME", m_objCTCheckOrderContent.strAdvanceTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AKP", m_objCTCheckOrderContent.strAKP.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ALBUMEN", m_objCTCheckOrderContent.strAlbumen.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("APPLYDOTORID", m_objCTCheckOrderContent.strApplyDotorID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLADDER", m_objCTCheckOrderContent.strBladder.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("BLOOD", m_objCTCheckOrderContent.strBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODDOOP", m_objCTCheckOrderContent.strBloodDoop.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BREAST", m_objCTCheckOrderContent.strBreast.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CANCER", m_objCTCheckOrderContent.strCancer.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKMONEYCONTENT", m_objCTCheckOrderContent.strCheckMoneyContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKPART", m_objCTCheckOrderContent.strCheckPart.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CLINIC", m_objCTCheckOrderContent.strClinic.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CTNO", m_objCTCheckOrderContent.strCTNO.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("EMICTION", m_objCTCheckOrderContent.strEmiction.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FECULA", m_objCTCheckOrderContent.strFecula.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FETUS", m_objCTCheckOrderContent.strFetus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IDEA", m_objCTCheckOrderContent.strIdea.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LIVER", m_objCTCheckOrderContent.strLiver.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHER", m_objCTCheckOrderContent.strOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PANCREAS", m_objCTCheckOrderContent.strPancreas.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("PARTICULAR", m_objCTCheckOrderContent.strParticular.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEE17", m_objCTCheckOrderContent.strPee17.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHLEGM", m_objCTCheckOrderContent.strPhlegm.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("PHOTOMONTYCONTENT", m_objCTCheckOrderContent.strPhotoMontyContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RED", m_objCTCheckOrderContent.strRed.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RESUMEASTHMAHAVE", m_objCTCheckOrderContent.strResumeAsthmaHave.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("RESUMEASTHMANONE", m_objCTCheckOrderContent.strResumeAsthmaNone.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RESUMEHAVE", m_objCTCheckOrderContent.strResumeHave.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RESUMENONE", m_objCTCheckOrderContent.strResumeNone.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SCAN", m_objCTCheckOrderContent.strScan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOOL", m_objCTCheckOrderContent.strTool.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ULTRASONIC", m_objCTCheckOrderContent.strUltrasonic.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WALK", m_objCTCheckOrderContent.strWalk.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);

        }


        public clsCTCheckOrder objDisplay(string strInPatientID, string strInPatientDate, string strCreateDate)
        {
            string strXML = "";
            int intRows = 0;

            clsCTCheckOrder m_objDisplay = new clsCTCheckOrder();

            //com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ m_objServ =
            //    (com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ));

            try
            {
                if (strInPatientDate == null || strInPatientDate.Trim() == "")
                {
                    long lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.lngSelectNewRecord(strInPatientID, strCreateDate, ref strXML, ref intRows);
                }
                else
                {
                    long lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.clsCTCheckOrderServ_lngSelectNewRecord(strInPatientID, strInPatientDate, strCreateDate, ref strXML, ref intRows);
                }
                if (intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    m_objDisplay.strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                    m_objDisplay.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                    m_objDisplay.strCreateDate = objReader.GetAttribute("CREATEDATE").ToString().Replace('��', '\'');

                                    m_objDisplay.strAdvanceID = objReader.GetAttribute("ADVANCEID").ToString().Replace('��', '\'');
                                    m_objDisplay.strAdvanceTime = objReader.GetAttribute("ADVANCETIME").ToString().Replace('��', '\'');
                                    m_objDisplay.strAKP = objReader.GetAttribute("AKP").ToString().Replace('��', '\'');

                                    m_objDisplay.strAlbumen = objReader.GetAttribute("ALBUMEN").ToString().Replace('��', '\'');
                                    m_objDisplay.strApplyDotorID = objReader.GetAttribute("APPLYDOTORID").ToString().Replace('��', '\'');
                                    m_objDisplay.strBladder = objReader.GetAttribute("BLADDER").ToString().Replace('��', '\'');

                                    m_objDisplay.strBlood = objReader.GetAttribute("BLOOD").ToString().Replace('��', '\'');
                                    m_objDisplay.strBloodDoop = objReader.GetAttribute("BLOODDOOP").ToString().Replace('��', '\'');
                                    m_objDisplay.strBreast = objReader.GetAttribute("BREAST").ToString().Replace('��', '\'');

                                    m_objDisplay.strCancer = objReader.GetAttribute("CANCER").ToString().Replace('��', '\'');
                                    m_objDisplay.strCheckMoneyContent = objReader.GetAttribute("CHECKMONEYCONTENT").ToString().Replace('��', '\'');
                                    m_objDisplay.strCheckPart = objReader.GetAttribute("CHECKPART").ToString().Replace('��', '\'');

                                    m_objDisplay.strClinic = objReader.GetAttribute("CLINIC").ToString().Replace('��', '\'');
                                    m_objDisplay.strCreateUserID = objReader.GetAttribute("CREATEUSERID").ToString().Replace('��', '\'');

                                    m_objDisplay.strCTNO = objReader.GetAttribute("CTNO").ToString().Replace('��', '\'');
                                    m_objDisplay.strEmiction = objReader.GetAttribute("EMICTION").ToString().Replace('��', '\'');
                                    m_objDisplay.strFecula = objReader.GetAttribute("FECULA").ToString().Replace('��', '\'');

                                    m_objDisplay.strFetus = objReader.GetAttribute("FETUS").ToString().Replace('��', '\'');
                                    m_objDisplay.strIdea = objReader.GetAttribute("IDEA").ToString().Replace('��', '\'');
                                    m_objDisplay.strLiver = objReader.GetAttribute("LIVER").ToString().Replace('��', '\'');

                                    m_objDisplay.strOther = objReader.GetAttribute("OTHER").ToString().Replace('��', '\'');
                                    m_objDisplay.strPancreas = objReader.GetAttribute("PANCREAS").ToString().Replace('��', '\'');
                                    m_objDisplay.strParticular = objReader.GetAttribute("PARTICULAR").ToString().Replace('��', '\'');

                                    m_objDisplay.strPee17 = objReader.GetAttribute("PEE17").ToString().Replace('��', '\'');
                                    m_objDisplay.strPhlegm = objReader.GetAttribute("PHLEGM").ToString().Replace('��', '\'');
                                    m_objDisplay.strPhotoMontyContent = objReader.GetAttribute("PHOTOMONTYCONTENT").ToString().Replace('��', '\'');

                                    m_objDisplay.strRed = objReader.GetAttribute("RED").ToString().Replace('��', '\'');
                                    m_objDisplay.strResumeAsthmaHave = objReader.GetAttribute("RESUMEASTHMAHAVE").ToString().Replace('��', '\'');
                                    m_objDisplay.strResumeAsthmaNone = objReader.GetAttribute("RESUMEASTHMANONE").ToString().Replace('��', '\'');

                                    m_objDisplay.strResumeHave = objReader.GetAttribute("RESUMEHAVE").ToString().Replace('��', '\'');
                                    m_objDisplay.strResumeNone = objReader.GetAttribute("RESUMENONE").ToString().Replace('��', '\'');
                                    m_objDisplay.strScan = objReader.GetAttribute("SCAN").ToString().Replace('��', '\'');

                                    m_objDisplay.strTool = objReader.GetAttribute("TOOL").ToString().Replace('��', '\'');
                                    m_objDisplay.strUltrasonic = objReader.GetAttribute("ULTRASONIC").ToString().Replace('��', '\'');
                                    m_objDisplay.strWalk = objReader.GetAttribute("WALK").ToString().Replace('��', '\'');
                                    m_objDisplay.strApplicationID = objReader.GetAttribute("APPLICATIONID").ToString().Replace('��', '\'');


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
            return m_objDisplay;
        }

        public long m_lngGetApplicationid(ref string p_strApplicationid)
        {
            long lngRet = 0;
            string strXML = "";
            int intRows = 0;

            //com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ m_objServ =
            //    (com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.CTCheckOrderServ.clsCTCheckOrderServ));

            try
            {
                lngRet = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetApplicationID(ref strXML, ref intRows);
                if (intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    p_strApplicationid = objReader.GetAttribute("APPLICATIONID").ToString().Replace('��', '\'');
                                }
                                break;
                        }
                    }
                    if (p_strApplicationid.StartsWith("P"))
                        p_strApplicationid = p_strApplicationid.Remove(0, 1);
                    long mMax = long.Parse(p_strApplicationid) + 1;
                    if (mMax < 100000)
                    {
                        p_strApplicationid = "P" + mMax.ToString("00000");
                    }
                    else
                    {
                        p_strApplicationid = "P" + mMax.ToString();
                    }
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRet;
        }
    }

    public class clsCTCheckOrder
    {
        public string strResumeHave = "";
        public string strResumeNone = "";
        public string strResumeAsthmaHave = "";
        public string strResumeAsthmaNone = "";
        public string strTool = "";
        public string strWalk = "";
        public string strParticular = "";
        public string strClinic = "";
        public string strCheckPart = "";
        public string strApplyDotorID = "";
        public string strLiver = "";
        public string strAlbumen = "";
        public string strFetus = "";
        public string strRed = "";
        public string strAKP = "";
        public string strCancer = "";
        public string strFecula = "";
        public string strPee17 = "";
        public string strBlood = "";
        public string strBloodDoop = "";
        public string strPhlegm = "";
        public string strEmiction = "";
        public string strBreast = "";
        public string strPancreas = "";
        public string strScan = "";
        public string strBladder = "";
        public string strUltrasonic = "";
        public string strOther = "";
        public string strIdea = "";
        public string strAdvanceTime = "";
        public string strAdvanceID = "";
        public string strCheckMoneyContent = "";
        public string strCTNO = "";
        public string strPhotoMontyContent = "";
        public string strInPatientID = "";
        public string strInPatientDate = "";
        public string strCreateDate = "";
        public string strCreateUserID = "";
        public string strIfConfirm = "";
        public string strStatus = "";
        public string strApplicationID = "";
    }

}
