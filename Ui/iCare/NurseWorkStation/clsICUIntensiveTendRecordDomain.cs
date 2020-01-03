using System;
using System.Xml;
using weCare.Core.Entity;
using System.IO;
using System.Text;
using System.Collections;

namespace iCare
{
    /// <summary>
    /// Summary description for clsICUIntensiveTendRecordDomain.
    /// </summary>
    public class clsICUIntensiveTendRecordDomain
    {
        //private clsICUIntensiveTendRecordServ m_objServ=new clsICUIntensiveTendRecordServ();
        private XmlParserContext m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
        private MemoryStream m_objXmlMemStream;
        private XmlTextWriter m_objXmlWriter;
        public clsICUIntensiveTendRecordDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
        }
        ///ֻ��Ҫ���ʱ��
        public string[] m_strGetAllTendRecordCreateDateArr(string p_strInPatientID, string p_strInPatientDate)
        {
            string strXML = "";
            int intRows = 0;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            try
            {
                long lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllTendRecordCreateDateArr(p_strInPatientID, p_strInPatientDate, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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
                                strDateArr[intIndex++] = objReader.GetAttribute("CREATEDATE");
                            }
                            break;
                    }
                }
                return strDateArr;
            }
            return null;
        }

        public clsICUIntensiveTendRecord m_objGetLatestTendRecord(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strXML = "";
            int intRows = 0;
            string strParamXML = "";
            int intParamRows = 0;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            try
            {
                long lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLatestTendRecord(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows, out strParamXML, out intParamRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)
            {
                clsICUIntensiveTendRecord objclsICUIntensiveTendRecord = new clsICUIntensiveTendRecord();

                XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsICUIntensiveTendRecord.m_strInPatientID = p_strInPatientID;
                                objclsICUIntensiveTendRecord.m_strInPatientDate = p_strInPatientDate;
                                objclsICUIntensiveTendRecord.m_strCreateDate = p_strCreateDate;
                                objclsICUIntensiveTendRecord.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID");
                                objclsICUIntensiveTendRecord.m_strRecordContentXML = objReader.GetAttribute("RECORDCONTENTXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strTemperatureXML = objReader.GetAttribute("TEMPERATUREXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strBreathXML = objReader.GetAttribute("BREATHXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strPulseXML = objReader.GetAttribute("PULSEXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strBloodPressureSXML = objReader.GetAttribute("BLOODPRESSURESXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strBloodPressureAXML = objReader.GetAttribute("BLOODPRESSUREAXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strPupilLeftXML = objReader.GetAttribute("PUPILLEFTXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strPupilRightXML = objReader.GetAttribute("PUPILRIGHTXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strEchoLeftXML = objReader.GetAttribute("ECHOLEFTXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strEchoRightXML = objReader.GetAttribute("ECHORIGHTXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strInDXML = objReader.GetAttribute("INDXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strInIXML = objReader.GetAttribute("INIXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutUXML = objReader.GetAttribute("OUTUXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutSXML = objReader.GetAttribute("OUTSXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutVXML = objReader.GetAttribute("OUTVXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutEXML = objReader.GetAttribute("OUTEXML").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strIfConfirm = objReader.GetAttribute("IFCONFIRM");
                                objclsICUIntensiveTendRecord.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").Replace('��', '\'');

                                objclsICUIntensiveTendRecord.m_strSensesXML = objReader.GetAttribute("SENSESXML").Replace('��', '\'');
                                //objclsICUIntensiveTendRecord.m_strAbsorbPhlegmDateXML= objReader.GetAttribute("ABSORBPHLEGMDATEXML").Replace('��','\'');
                                objclsICUIntensiveTendRecord.m_strPhlegmAttributeXML = objReader.GetAttribute("PHLEGMATTRIBUTEXML").Replace('��', '\'');


                                objclsICUIntensiveTendRecord.m_strRecordContent = objReader.GetAttribute("RECORDCONTENT").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strTemperature = objReader.GetAttribute("TEMPERATURE").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strBreath = objReader.GetAttribute("BREATH").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strPulse = objReader.GetAttribute("PULSE").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strBloodPressureS = objReader.GetAttribute("BLOODPRESSURES").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strBloodPressureA = objReader.GetAttribute("BLOODPRESSUREA").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strPupilLeft = objReader.GetAttribute("PUPILLEFT").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strPupilRight = objReader.GetAttribute("PUPILRIGHT").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strEchoLeft = objReader.GetAttribute("ECHOLEFT").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strEchoRight = objReader.GetAttribute("ECHORIGHT").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strInD = objReader.GetAttribute("IND").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strInI = objReader.GetAttribute("INI").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutU = objReader.GetAttribute("OUTU").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutS = objReader.GetAttribute("OUTS").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutV = objReader.GetAttribute("OUTV").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strOutE = objReader.GetAttribute("OUTE").Replace('��', '\'');

                                objclsICUIntensiveTendRecord.m_strSenses = objReader.GetAttribute("SENSES").Replace('��', '\'');
                                objclsICUIntensiveTendRecord.m_strAbsorbPhlegmDate = objReader.GetAttribute("ABSORBPHLEGMDATE");
                                objclsICUIntensiveTendRecord.m_strPhlegmAttribute = objReader.GetAttribute("PHLEGMATTRIBUTE").Replace('��', '\'');


                            }
                            break;
                    }
                }
                objclsICUIntensiveTendRecord.m_objclsICUIntensiveTendRecordParamArr = m_objGetLatestTendRecordParam(strParamXML, intParamRows);

                return objclsICUIntensiveTendRecord;
            }
            return null;
        }

        ///��ȡ��е��������Ϣ
        private clsICUIntensiveTendRecordParam[] m_objGetLatestTendRecordParam(string p_strXML, int p_intRows)
        {
            if (p_intRows > 0)
            {
                clsICUIntensiveTendRecordParam[] objclsICUIntensiveTendRecordParamArr = new clsICUIntensiveTendRecordParam[p_intRows];

                XmlTextReader objReader = new XmlTextReader(p_strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsICUIntensiveTendRecordParamArr[intIndex] = new clsICUIntensiveTendRecordParam();
                                //								objclsICUIntensiveTendRecordParamArr[intIndex].m_strInPatientID= objReader.GetAttribute("INPATIENTID");//��ȡʱ���ֶβ���Ҫ
                                //								objclsICUIntensiveTendRecordParamArr[intIndex].m_strInPatientDate= objReader.GetAttribute("INPATIENTDATE");
                                //								objclsICUIntensiveTendRecordParamArr[intIndex].m_strCreateDate= objReader.GetAttribute("CREATEDATE");
                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strModifyDate = objReader.GetAttribute("MODIFYDATE");
                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");
                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strModifyUserName = objReader.GetAttribute("MODIFYUSERNAME");

                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strStandardParamID = objReader.GetAttribute("STANDARDPARAMID").Replace('��', '\'');
                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strStandardParamValue = objReader.GetAttribute("STANDARDPARAMVALUE").Replace('��', '\'');
                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strParamFlag = objReader.GetAttribute("PARAMFLAG");
                                objclsICUIntensiveTendRecordParamArr[intIndex].m_strParamName = objReader.GetAttribute("PARAMNAME");
                                intIndex++;
                            }
                            break;
                    }
                }
                return objclsICUIntensiveTendRecordParamArr;
            }
            return null;
        }

        ///���ʱ���Ӧ�Ĵӱ���Ϣ�����ã���ͳ��ʱ������صı�����
        public clsICUIntensiveTendRecordContent m_objGetLatestTendRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strXML = "";
            int intRows = 0;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            try
            {
                long lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLatestTendRecordContent(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)//intRows==1
            {
                clsICUIntensiveTendRecordContent objclsICUIntensiveTendRecordContent = new clsICUIntensiveTendRecordContent();

                XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsICUIntensiveTendRecordContent.m_strInPatientID = p_strInPatientID;
                                objclsICUIntensiveTendRecordContent.m_strInPatientDate = p_strInPatientDate;
                                objclsICUIntensiveTendRecordContent.m_strCreateDate = p_strCreateDate;

                                objclsICUIntensiveTendRecordContent.m_strModifyDate = objReader.GetAttribute("MODIFYDATE");
                                objclsICUIntensiveTendRecordContent.m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");

                                objclsICUIntensiveTendRecordContent.m_strInD_Last = objReader.GetAttribute("IND_LAST");
                                objclsICUIntensiveTendRecordContent.m_strInI_Last = objReader.GetAttribute("INI_LAST");
                                objclsICUIntensiveTendRecordContent.m_strOutU_Last = objReader.GetAttribute("OUTU_LAST");
                                objclsICUIntensiveTendRecordContent.m_strOutS_Last = objReader.GetAttribute("OUTS_LAST");
                                objclsICUIntensiveTendRecordContent.m_strOutV_Last = objReader.GetAttribute("OUTV_LAST");
                                objclsICUIntensiveTendRecordContent.m_strOutE_Last = objReader.GetAttribute("OUTE_LAST");

                            }
                            break;
                    }
                }
                return objclsICUIntensiveTendRecordContent;
            }
            return null;
        }

        /// <summary>
        /// ��ѯ����ʱ��p_strCreateDate��Ӧ�ĵ��������ͳ����Ϣ
        /// </summary>		
        public clsStatisticInfo_TotalInOut m_objGetStatisticInfo(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strXML = "";
            int intRows = 0;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            try
            {
                long lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetStatisticInfo(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)//intRows==1
            {
                clsStatisticInfo_TotalInOut objclsStatisticInfo_TotalInOut = new clsStatisticInfo_TotalInOut();

                XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsStatisticInfo_TotalInOut.m_strTotalInD = objReader.GetAttribute("TOTALIND");
                                objclsStatisticInfo_TotalInOut.m_strTotalInI = objReader.GetAttribute("TOTALINI");
                                objclsStatisticInfo_TotalInOut.m_strTotalOutU = objReader.GetAttribute("TOTALOUTU");
                                objclsStatisticInfo_TotalInOut.m_strTotalOutS = objReader.GetAttribute("TOTALOUTS");
                                objclsStatisticInfo_TotalInOut.m_strTotalOutV = objReader.GetAttribute("TOTALOUTV");
                                objclsStatisticInfo_TotalInOut.m_strTotalOutE = objReader.GetAttribute("TOTALOUTE");
                                objclsStatisticInfo_TotalInOut.m_strTotalIn = objReader.GetAttribute("TOTALIN");
                                objclsStatisticInfo_TotalInOut.m_strTotalOut = objReader.GetAttribute("TOTALOUT");
                            }
                            break;
                    }
                }
                objclsStatisticInfo_TotalInOut.m_strCreateDate = DateTime.Parse(p_strCreateDate).ToShortDateString();
                return objclsStatisticInfo_TotalInOut;
            }
            return null;
        }

        /// ������Ϣ		
        public long m_lngSave(clsICUIntensiveTendRecord p_objclsICUIntensiveTendRecord, clsICUIntensiveTendRecordContent p_objclsICUIntensiveTendRecordContent, bool p_blnIsAddNew)
        {
            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            long lngResult = 0;
            try
            {
                if (p_blnIsAddNew == true)
                    lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngAddNew(m_strGetMainXML(p_objclsICUIntensiveTendRecord), m_strGetMainAllContentXML(p_objclsICUIntensiveTendRecord), m_strGetParamXMLArr(p_objclsICUIntensiveTendRecord.m_objclsICUIntensiveTendRecordParamArr), m_strGetSubXML(p_objclsICUIntensiveTendRecordContent));
                else
                    lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngModify(m_strGetMainXML(p_objclsICUIntensiveTendRecord), m_strGetMainAllContentXML(p_objclsICUIntensiveTendRecord), m_strGetParamXMLArr(p_objclsICUIntensiveTendRecord.m_objclsICUIntensiveTendRecordParamArr), m_strGetSubXML(p_objclsICUIntensiveTendRecordContent));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        private string m_strGetMainXML(clsICUIntensiveTendRecord p_objclsICUIntensiveTendRecord)
        {

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");

            try
            {
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsICUIntensiveTendRecord.m_strInPatientID);
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsICUIntensiveTendRecord.m_strInPatientDate);
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsICUIntensiveTendRecord.m_strCreateDate);
                m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objclsICUIntensiveTendRecord.m_strCreateUserID);
                m_objXmlWriter.WriteAttributeString("RECORDCONTENTXML", p_objclsICUIntensiveTendRecord.m_strRecordContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("TEMPERATUREXML", p_objclsICUIntensiveTendRecord.m_strTemperatureXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BREATHXML", p_objclsICUIntensiveTendRecord.m_strBreathXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PULSEXML", p_objclsICUIntensiveTendRecord.m_strPulseXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPRESSURESXML", p_objclsICUIntensiveTendRecord.m_strBloodPressureSXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPRESSUREAXML", p_objclsICUIntensiveTendRecord.m_strBloodPressureAXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PUPILLEFTXML", p_objclsICUIntensiveTendRecord.m_strPupilLeftXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PUPILRIGHTXML", p_objclsICUIntensiveTendRecord.m_strPupilRightXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ECHOLEFTXML", p_objclsICUIntensiveTendRecord.m_strEchoLeftXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ECHORIGHTXML", p_objclsICUIntensiveTendRecord.m_strEchoRightXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INDXML", p_objclsICUIntensiveTendRecord.m_strInDXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INIXML", p_objclsICUIntensiveTendRecord.m_strInIXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTUXML", p_objclsICUIntensiveTendRecord.m_strOutUXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTSXML", p_objclsICUIntensiveTendRecord.m_strOutSXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTVXML", p_objclsICUIntensiveTendRecord.m_strOutVXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTEXML", p_objclsICUIntensiveTendRecord.m_strOutEXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("IFCONFIRM", "0");//p_objclsICUIntensiveTendRecord.m_strIfConfirm);
                                                                      //	���ޣ�����			m_objXmlWriter.WriteAttributeString("CONFIRMREASON", p_objclsICUIntensiveTendRecord.m_strConfirmReason.Replace('\'','��'));
                                                                      //	���ޣ�����			m_objXmlWriter.WriteAttributeString("CONFIRMREASONXML", p_objclsICUIntensiveTendRecord.m_strConfirmReasonXML.Replace('\'','��'));

                m_objXmlWriter.WriteAttributeString("SENSESXML", p_objclsICUIntensiveTendRecord.m_strSensesXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PHLEGMATTRIBUTEXML", p_objclsICUIntensiveTendRecord.m_strPhlegmAttributeXML.Replace('\'', '��'));

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

        private string m_strGetMainAllContentXML(clsICUIntensiveTendRecord p_objclsICUIntensiveTendRecord)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            try
            {
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsICUIntensiveTendRecord.m_strInPatientID);
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsICUIntensiveTendRecord.m_strInPatientDate);
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsICUIntensiveTendRecord.m_strCreateDate);
                m_objXmlWriter.WriteAttributeString("RECORDCONTENT", p_objclsICUIntensiveTendRecord.m_strRecordContent.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("TEMPERATURE", p_objclsICUIntensiveTendRecord.m_strTemperature.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BREATH", p_objclsICUIntensiveTendRecord.m_strBreath.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PULSE", p_objclsICUIntensiveTendRecord.m_strPulse.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPRESSURES", p_objclsICUIntensiveTendRecord.m_strBloodPressureS.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPRESSUREA", p_objclsICUIntensiveTendRecord.m_strBloodPressureA.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PUPILLEFT", p_objclsICUIntensiveTendRecord.m_strPupilLeft.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PUPILRIGHT", p_objclsICUIntensiveTendRecord.m_strPupilRight.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ECHOLEFT", p_objclsICUIntensiveTendRecord.m_strEchoLeft.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ECHORIGHT", p_objclsICUIntensiveTendRecord.m_strEchoRight.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("IND", p_objclsICUIntensiveTendRecord.m_strInD.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INI", p_objclsICUIntensiveTendRecord.m_strInI.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTU", p_objclsICUIntensiveTendRecord.m_strOutU.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTS", p_objclsICUIntensiveTendRecord.m_strOutS.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTV", p_objclsICUIntensiveTendRecord.m_strOutV.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTE", p_objclsICUIntensiveTendRecord.m_strOutE.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("SENSES", p_objclsICUIntensiveTendRecord.m_strSenses.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ABSORBPHLEGMDATE", p_objclsICUIntensiveTendRecord.m_strAbsorbPhlegmDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PHLEGMATTRIBUTE", p_objclsICUIntensiveTendRecord.m_strPhlegmAttribute.Replace('\'', '��'));
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
            }
            //�����ڴ��ֶΣ�m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }



        private string m_strGetSubXML(clsICUIntensiveTendRecordContent p_objclsICUIntensiveTendRecordContent)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            try
            {
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsICUIntensiveTendRecordContent.m_strInPatientID);
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsICUIntensiveTendRecordContent.m_strInPatientDate);
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsICUIntensiveTendRecordContent.m_strCreateDate);
                m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsICUIntensiveTendRecordContent.m_strModifyDate);
                m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objclsICUIntensiveTendRecordContent.m_strModifyUserID);
                m_objXmlWriter.WriteAttributeString("RECORDCONTENT_LAST", p_objclsICUIntensiveTendRecordContent.m_strRecordContent_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("TEMPERATURE_LAST", p_objclsICUIntensiveTendRecordContent.m_strTemperature_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BREATH_LAST", p_objclsICUIntensiveTendRecordContent.m_strBreath_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PULSE_LAST", p_objclsICUIntensiveTendRecordContent.m_strPulse_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPRESSURES_LAST", p_objclsICUIntensiveTendRecordContent.m_strBloodPressureS_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPRESSUREA_LAST", p_objclsICUIntensiveTendRecordContent.m_strBloodPressureA_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PUPILLEFT_LAST", p_objclsICUIntensiveTendRecordContent.m_strPupilLeft_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PUPILRIGHT_LAST", p_objclsICUIntensiveTendRecordContent.m_strPupilRight_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ECHOLEFT_LAST", p_objclsICUIntensiveTendRecordContent.m_strEchoLeft_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ECHORIGHT_LAST", p_objclsICUIntensiveTendRecordContent.m_strEchoRight_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("IND_LAST", p_objclsICUIntensiveTendRecordContent.m_strInD_Last);
                m_objXmlWriter.WriteAttributeString("INI_LAST", p_objclsICUIntensiveTendRecordContent.m_strInI_Last);
                m_objXmlWriter.WriteAttributeString("OUTU_LAST", p_objclsICUIntensiveTendRecordContent.m_strOutU_Last);
                m_objXmlWriter.WriteAttributeString("OUTS_LAST", p_objclsICUIntensiveTendRecordContent.m_strOutS_Last);
                m_objXmlWriter.WriteAttributeString("OUTV_LAST", p_objclsICUIntensiveTendRecordContent.m_strOutV_Last);
                m_objXmlWriter.WriteAttributeString("OUTE_LAST", p_objclsICUIntensiveTendRecordContent.m_strOutE_Last);

                m_objXmlWriter.WriteAttributeString("SENSES_LAST", p_objclsICUIntensiveTendRecordContent.m_strSenses_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ABSORBPHLEGMDATE_LAST", p_objclsICUIntensiveTendRecordContent.m_strAbsorbPhlegmDate_Last.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PHLEGMATTRIBUTE_LAST", p_objclsICUIntensiveTendRecordContent.m_strPhlegmAttribute_Last.Replace('\'', '��'));
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
            }
            //�����ڴ��ֶΣ�m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        private string[] m_strGetParamXMLArr(clsICUIntensiveTendRecordParam[] p_objclsICUIntensiveTendRecordParamArr)
        {
            if (p_objclsICUIntensiveTendRecordParamArr == null || p_objclsICUIntensiveTendRecordParamArr.Length == 0)
                return null;
            string[] strParamXMLArr = new string[p_objclsICUIntensiveTendRecordParamArr.Length];
            for (int i = 0; i < p_objclsICUIntensiveTendRecordParamArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("Model");
                try
                {
                    m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsICUIntensiveTendRecordParamArr[i].m_strInPatientID);
                    m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsICUIntensiveTendRecordParamArr[i].m_strInPatientDate);
                    m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsICUIntensiveTendRecordParamArr[i].m_strCreateDate);
                    m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsICUIntensiveTendRecordParamArr[i].m_strModifyDate);
                    m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objclsICUIntensiveTendRecordParamArr[i].m_strModifyUserID);
                    m_objXmlWriter.WriteAttributeString("STANDARDPARAMID", p_objclsICUIntensiveTendRecordParamArr[i].m_strStandardParamID.Replace('\'', '��'));
                    m_objXmlWriter.WriteAttributeString("STANDARDPARAMVALUE", p_objclsICUIntensiveTendRecordParamArr[i].m_strStandardParamValue.Replace('\'', '��'));
                }
                catch (Exception ex)
                {
                    clsPublicFunction.ShowInformationMessageBox(ex.Message);
                }
                m_objXmlWriter.WriteAttributeString("STATUS", "0");
                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strParamXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return strParamXMLArr;

        }



        public clsICUIntensiveTendRecordContent_All[] m_strGetclsICUIntensiveTendRecordContent_AllArr(string p_strInPatientID, string p_strInPatientDate, out clsStatisticInfo_TotalInOut[] p_objclsStatisticInfo_TotalInOutArr)
        {
            string strXML = "";
            int intRows = 0;
            p_objclsStatisticInfo_TotalInOutArr = null;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllTendRecord(p_strInPatientID, p_strInPatientDate, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            ArrayList arlStatisticInfo = new ArrayList();
            if (intRows > 0)
            {
                clsICUIntensiveTendRecordContent_All[] objclsICUIntensiveTendRecordContent_AllArr = new clsICUIntensiveTendRecordContent_All[intRows];

                XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                string strPreCreateDate = "";
                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex] = new clsICUIntensiveTendRecordContent_All();
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateDate = objReader.GetAttribute("CREATEDATE");
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateUserID = objReader.GetAttribute("CREATEUSERID");
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strPhlegmAttribute = objReader.GetAttribute("PHLEGMATTRIBUTE");
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strPhlegmAttributeXML = objReader.GetAttribute("PHLEGMATTRIBUTEXML");
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strSenses = objReader.GetAttribute("SENSES");
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strSensesXML = objReader.GetAttribute("SENSESXML");

                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strRecordContent = objReader.GetAttribute("RECORDCONTENT");
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strRecordContentXML = objReader.GetAttribute("RECORDCONTENTXML");

                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strFirstPrintDate = objReader.GetAttribute("FIRSTPRINTDATE");    //�״δ�ӡʱ��

                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_objclsICUIntensiveTendRecordContentArr = m_strGetAllTendRecordContentArr(p_strInPatientID, p_strInPatientDate, objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateDate);
                                objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_objclsICUIntensiveTendRecordParamArr = m_strGetAllTendRecordParamArr(p_strInPatientID, p_strInPatientDate, objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateDate);


                                if (strPreCreateDate != DateTime.Parse(objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateDate).ToShortDateString())
                                {
                                    arlStatisticInfo.Add(m_objGetStatisticInfo(p_strInPatientID, p_strInPatientDate, objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateDate));
                                }
                                strPreCreateDate = DateTime.Parse(objclsICUIntensiveTendRecordContent_AllArr[intIndex].m_strCreateDate).ToShortDateString();//������һ�������

                                intIndex++;
                            }
                            break;
                    }
                }

                p_objclsStatisticInfo_TotalInOutArr = (clsStatisticInfo_TotalInOut[])(arlStatisticInfo.ToArray(typeof(clsStatisticInfo_TotalInOut)));
                return objclsICUIntensiveTendRecordContent_AllArr;
            }
            return null;
        }

        /// <summary>
        /// ���������Ϣ���ӱ��������޸Ĺ��Ľ������p_strInPatientDate�ĸ�ʽ����Ϊ"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public clsICUIntensiveTendRecordContent[] m_strGetAllTendRecordContentArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strXML = "";
            int intRows = 0;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllTendRecordContent(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)
            {
                clsICUIntensiveTendRecordContent[] objclsICUIntensiveTendRecordContentArr = new clsICUIntensiveTendRecordContent[intRows];

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

                                objclsICUIntensiveTendRecordContentArr[intIndex] = new clsICUIntensiveTendRecordContent();

                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strCreateDate = objReader.GetAttribute("CDATE");//p_strCreateDate;
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strModifyDate = objReader.GetAttribute("MODIFYDATE");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strModifyUserName = objReader.GetAttribute("MODIFYUSERNAME");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strInD_Last = objReader.GetAttribute("IND_LAST");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strInI_Last = objReader.GetAttribute("INI_LAST");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strOutU_Last = objReader.GetAttribute("OUTU_LAST");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strOutS_Last = objReader.GetAttribute("OUTS_LAST");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strOutV_Last = objReader.GetAttribute("OUTV_LAST");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strOutE_Last = objReader.GetAttribute("OUTE_LAST");

                                ///////////////////////////////////////
                                ////.///////////////////////////////
                                ///
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strTemperature_Last = objReader.GetAttribute("TEMPERATURE_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strBreath_Last = objReader.GetAttribute("BREATH_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strPulse_Last = objReader.GetAttribute("PULSE_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strBloodPressureS_Last = objReader.GetAttribute("BLOODPRESSURES_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strBloodPressureA_Last = objReader.GetAttribute("BLOODPRESSUREA_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strPupilLeft_Last = objReader.GetAttribute("PUPILLEFT_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strPupilRight_Last = objReader.GetAttribute("PUPILRIGHT_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strEchoLeft_Last = objReader.GetAttribute("ECHOLEFT_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strEchoRight_Last = objReader.GetAttribute("ECHORIGHT_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strSenses_Last = objReader.GetAttribute("SENSES_LAST").Replace('��', '\'');
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strAbsorbPhlegmDate_Last = objReader.GetAttribute("ABSORBPHLEGMDATE_LAST");
                                objclsICUIntensiveTendRecordContentArr[intIndex].m_strPhlegmAttribute_Last = objReader.GetAttribute("PHLEGMATTRIBUTE_LAST").Replace('��', '\'');

                                intIndex++;
                            }
                            break;
                    }
                }
                return objclsICUIntensiveTendRecordContentArr;
            }
            return null;
        }

        /// <summary>
        /// ������л�е������Ϣ���ӱ��������޸Ĺ��Ľ������p_strInPatientDate�ĸ�ʽ����Ϊ"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public clsICUIntensiveTendRecordParam[] m_strGetAllTendRecordParamArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strXML = "";
            int intRows = 0;

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllTendRecordParam(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_objGetLatestTendRecordParam(strXML, intRows);
        }

        /// <summary>
        /// ���µ�ǰ�������ж�Ӧ��¼���״δ�ӡʱ��
        /// </summary>		
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID, string p_strInPatientDate/*,string p_strCreateDate*/)
        {//���µ�һ�δ�ӡʱ��		

            //clsICUIntensiveTendRecordServ m_objServ =
            //    (clsICUIntensiveTendRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUIntensiveTendRecordServ));

            long lngResult = 0;
            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsICUIntensiveTendRecordServ_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate/*,p_strCreateDate*/);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
    }

    /// <summary>
    /// ICUΣ�ػ����¼�������
    /// </summary>
    public class clsICUIntensiveTendRecord
    {
        public string m_strInPatientID;
        public string m_strInPatientDate;
        public string m_strCreateDate;

        public string m_strCreateUserID;

        public string m_strSensesXML;//��־,��clsIntensiveTendRecord������ֶ�
                                     //��ɾ�����ֶΣ�public string m_strAbsorbPhlegmDateXML;//��̵ʱ�䣬��clsIntensiveTendRecord������ֶ�
        public string m_strPhlegmAttributeXML;//̵Һ��״,��clsIntensiveTendRecord������ֶ�

        public string m_strRecordContentXML;
        public string m_strTemperatureXML;
        public string m_strBreathXML;
        public string m_strPulseXML;
        public string m_strBloodPressureSXML;
        public string m_strBloodPressureAXML;
        public string m_strPupilLeftXML;
        public string m_strPupilRightXML;
        public string m_strEchoLeftXML;
        public string m_strEchoRightXML;
        public string m_strInDXML;
        public string m_strInIXML;
        public string m_strOutUXML;
        public string m_strOutSXML;
        public string m_strOutVXML;
        public string m_strOutEXML;
        public string m_strConfirmReasonXML;

        public string m_strSenses;//������ʷ���ݵ�����ֵ��	
        public string m_strAbsorbPhlegmDate;//���ֶ�ֻ��������ֵ����ȡʱֱ�Ӷ�ȡ��ʱ�伴��
        public string m_strPhlegmAttribute;
        public string m_strRecordContent;
        public string m_strTemperature;
        public string m_strBreath;
        public string m_strPulse;
        public string m_strBloodPressureS;
        public string m_strBloodPressureA;
        public string m_strPupilLeft;
        public string m_strPupilRight;
        public string m_strEchoLeft;
        public string m_strEchoRight;
        public string m_strInD;
        public string m_strInI;
        public string m_strOutU;
        public string m_strOutS;
        public string m_strOutV;
        public string m_strOutE;

        public string m_strIfConfirm;
        public string m_strConfirmReason;

        public clsICUIntensiveTendRecordParam[] m_objclsICUIntensiveTendRecordParamArr;
    }

    /// <summary>
    /// ICUΣ�ػ����¼�ӱ����
    /// </summary>
    public class clsICUIntensiveTendRecordContent
    {
        public string m_strInPatientID;
        public string m_strInPatientDate;
        public string m_strCreateDate;
        public string m_strModifyDate;
        public string m_strModifyUserID;
        public string m_strModifyUserName;//������ʱʹ��

        public string m_strSenses_Last;//�����ֵ	
        public string m_strAbsorbPhlegmDate_Last;
        public string m_strPhlegmAttribute_Last;
        public string m_strRecordContent_Last;
        public string m_strTemperature_Last;
        public string m_strBreath_Last;
        public string m_strPulse_Last;
        public string m_strBloodPressureS_Last;
        public string m_strBloodPressureA_Last;
        public string m_strPupilLeft_Last;
        public string m_strPupilRight_Last;
        public string m_strEchoLeft_Last;
        public string m_strEchoRight_Last;

        public string m_strInD_Last;//����ʱ�õ�����ѯͳ��ʱ�õ�		
        public string m_strInI_Last;
        public string m_strOutU_Last;
        public string m_strOutS_Last;
        public string m_strOutV_Last;
        public string m_strOutE_Last;
    }

    /// <summary>
    /// ������ͳ�Ƶ���
    /// </summary>
    public class clsStatisticInfo_TotalInOut
    {
        public string m_strTotalInD;//��ѯͳ��ʱ�������������ʱ����
        public string m_strTotalInI;
        public string m_strTotalOutU;
        public string m_strTotalOutS;
        public string m_strTotalOutV;
        public string m_strTotalOutE;
        public string m_strTotalIn;
        public string m_strTotalOut;
        public string m_strCreateDate;
    }

    //	/// <summary>
    //	/// ICUΣ�ػ����¼������Ϣ����
    //	/// </summary>
    //	public class clsICUIntensiveTendRecord_All
    //	{
    //		public clsICUIntensiveTendRecord m_objclsICUIntensiveTendRecord;
    //		public clsICUIntensiveTendRecordContent m_objclsICUIntensiveTendRecordContent;
    //	}

    public class clsICUIntensiveTendRecordParam
    {
        public string m_strInPatientID;
        public string m_strInPatientDate;
        public string m_strCreateDate;
        public string m_strModifyDate;
        public string m_strModifyUserID;
        public string m_strModifyUserName;//������ʱʹ��

        public string m_strStandardParamID;//����ID		
        public string m_strStandardParamValue;//����ֵ,����ֵ�ͣ�

        /// <summary>
        /// ������־��0���໤�ǣ�2��������
        /// </summary>
        public string m_strParamFlag;//������ʱʹ��
                                     /// <summary>
                                     /// ��������
                                     /// </summary>
        public string m_strParamName;//������ʱʹ��
    }

    /// <summary>
    /// �������д�ӡ���ݵ��ࣨ�����м������м������
    /// </summary>
    public class clsICUIntensiveTendRecordContent_All
    {
        public string m_strCreateDate;
        public string m_strCreateUserID;
        public string m_strSenses;//��־	
        public string m_strPhlegmAttribute;//̵Һ��״
        public string m_strSensesXML;//��־	
        public string m_strPhlegmAttributeXML;//̵Һ��״
        public string m_strRecordContentXML;//�������޸ĺۼ������ݵĸ�ʽ
        public string m_strRecordContent;//�������޸ĺۼ�������
        public string m_strFirstPrintDate;//�״δ�ӡʱ��
        public clsICUIntensiveTendRecordContent[] m_objclsICUIntensiveTendRecordContentArr;//Ҫ��ӡ��ÿ���޸ĺ����ȷ����
        public clsICUIntensiveTendRecordParam[] m_objclsICUIntensiveTendRecordParamArr;//���һ�α�������µ���ȷ����
    }

}
