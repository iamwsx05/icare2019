using System;
using System.Xml;
using System.IO;
using System.Text;
using weCare.Core.Entity;

using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// �����

    /// </summary>
    public class clsThreeMeasureRecordDomain
    {
        /// <summary>
        /// ����������м��
        /// </summary>
        //private clsThreeMeasureRecordService m_objThreeMeasureRecordServ;

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

        /// <summary>
        /// ���캯��

        /// </summary>
        public clsThreeMeasureRecordDomain()
        {
            //m_objThreeMeasureRecordServ = new clsThreeMeasureRecordService();

            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�


            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>���ɵ�XML</returns>
        private string m_strMakeNewMainXml(clsThreeMeasureRecordInfo p_objInfo)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("ThreeMeasureRecord");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objInfo.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objInfo.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objInfo.m_strOpenDate);
            m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objInfo.m_strCreateDate);
            if (p_objInfo.m_strCreateID != null)
            {
                m_objXmlWriter.WriteAttributeString("CREATEID", p_objInfo.m_strCreateID);
            }
            m_objXmlWriter.WriteAttributeString("SPECIALDATEXML", p_objInfo.m_objXmlValue.m_strSpecialDateXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("EVENTXML", p_objInfo.m_objXmlValue.m_strEventXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BREATHXML", p_objInfo.m_objXmlValue.m_strBreathXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPUTXML", p_objInfo.m_objXmlValue.m_strInputXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTAXML", p_objInfo.m_objXmlValue.m_strDejectaXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEXML", p_objInfo.m_objXmlValue.m_strPeeXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OUTSTREAMXML", p_objInfo.m_objXmlValue.m_strOutStreamXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PRESSUREXML", p_objInfo.m_objXmlValue.m_strPressureXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PRESSURE2XML", p_objInfo.m_objXmlValue.m_strPressureXml2.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIGHTXML", p_objInfo.m_objXmlValue.m_strWeightXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINTESTXML", p_objInfo.m_objXmlValue.m_strSkinTestXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERXML", p_objInfo.m_objXmlValue.m_strOtherXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PULSEXML", p_objInfo.m_objXmlValue.m_strPulseXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TEMPERATUREXML", p_objInfo.m_objXmlValue.m_strTemperatureXml.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERNAME", p_objInfo.m_objXmlValue.m_strOtherName.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IFCONFIRM", "0");
            if (p_objInfo.m_strConfirmReason != null)
            {
                m_objXmlWriter.WriteAttributeString("CONFIRMREASON", p_objInfo.m_strConfirmReason.Replace('\'', '��'));
            }
            if (p_objInfo.m_strConfirmReasonXMLString != null)
            {
                m_objXmlWriter.WriteAttributeString("CONFIRMREASONXMLSTRING", p_objInfo.m_strConfirmReasonXMLString.Replace('\'', '��'));
            }
            m_objXmlWriter.WriteAttributeString("STATUS", "0");

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();

            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>���ɵ�XML</returns>
        private string m_strMakeNewContentXml(clsThreeMeasureRecordContentInfo p_objInfo)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("ThreeMeasureRecordContent");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objInfo.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objInfo.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objInfo.m_strOpenDate);
            m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objInfo.m_strModifyDate);
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objInfo.m_strModifyUserID);
            if (p_objInfo.m_strSpecialDateIsNew != null)
            {
                m_objXmlWriter.WriteAttributeString("SPECIALDATEISNEW", p_objInfo.m_strSpecialDateIsNew);
            }
            if (p_objInfo.m_strInputStreamValue != null)
            {
                m_objXmlWriter.WriteAttributeString("INPUTSTREAMVALUE", p_objInfo.m_strInputStreamValue);
            }
            if (p_objInfo.m_strDejectaNeedWeight != null)
            {
                m_objXmlWriter.WriteAttributeString("DEJECTANEEDWEIGHT", p_objInfo.m_strDejectaNeedWeight);
                m_objXmlWriter.WriteAttributeString("DEJECTABEFORETIMES", p_objInfo.m_strDejectaBeforeTimes);
                m_objXmlWriter.WriteAttributeString("DEJECTAAFTERMORETIMES", p_objInfo.m_strDejectaAfterMoreTimes);
                m_objXmlWriter.WriteAttributeString("DEJECTAAFTERTIMES", p_objInfo.m_strDejectaAfterTimes);
                m_objXmlWriter.WriteAttributeString("DEJECTACLYSISTIMES", p_objInfo.m_strDejectaClysisTimes);
                m_objXmlWriter.WriteAttributeString("DEJECTAWEIGHT", p_objInfo.m_strDejectaWeight);
                m_objXmlWriter.WriteAttributeString("CANDEJECTA", p_objInfo.m_strCanDejecta);
            }
            if (p_objInfo.m_strPeeIsIrretention != null)
            {
                m_objXmlWriter.WriteAttributeString("PEEISIRRETENTION", p_objInfo.m_strPeeIsIrretention);
                m_objXmlWriter.WriteAttributeString("PEEVALUE", p_objInfo.m_strPeeValue);
            }
            if (p_objInfo.m_strOutStreamValue != null)
            {
                m_objXmlWriter.WriteAttributeString("OUTSTREAMVALUE", p_objInfo.m_strOutStreamValue);
            }
            if (p_objInfo.m_strDiastolicValue1 != null)
            {
                m_objXmlWriter.WriteAttributeString("DIASTOLICVALUE", p_objInfo.m_strDiastolicValue1);
                m_objXmlWriter.WriteAttributeString("SYSTOLICVALUE", p_objInfo.m_strSystolicValue1);
            }
            if (p_objInfo.m_strDiastolicValue2 != null)
            {
                m_objXmlWriter.WriteAttributeString("DIASTOLICVALUE2", p_objInfo.m_strDiastolicValue2);
                m_objXmlWriter.WriteAttributeString("SYSTOLICVALUE2", p_objInfo.m_strSystolicValue2);
            }
            if (p_objInfo.m_strWeightType != null)
            {
                m_objXmlWriter.WriteAttributeString("WEIGHTTYPE", p_objInfo.m_strWeightType);
                m_objXmlWriter.WriteAttributeString("WEIGHTVALUE", p_objInfo.m_strWeightValue);
            }

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();

            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>���ɵ�XML</returns>
        private string[] m_strMakeNewContentAccessXmlArr(clsThreeMeasureRecordContentAccessInfo[] p_objContentAccessInfoArr)
        {
            string[] strXmlArr = new string[p_objContentAccessInfoArr.Length];

            for (int i = 0; i < strXmlArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("ThreeMeasureRecordContentAccess");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objContentAccessInfoArr[i].m_strInPatientID);
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objContentAccessInfoArr[i].m_strInPatientDate);
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objContentAccessInfoArr[i].m_strOpenDate);
                m_objXmlWriter.WriteAttributeString("CREATETIME", p_objContentAccessInfoArr[i].m_strCreateTime);
                m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objContentAccessInfoArr[i].m_strModifyDate);
                m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objContentAccessInfoArr[i].m_strModifyUserID);
                if (p_objContentAccessInfoArr[i].m_strPulseValue != null)
                {
                    m_objXmlWriter.WriteAttributeString("PULSEVALUE", p_objContentAccessInfoArr[i].m_strPulseValue);
                    m_objXmlWriter.WriteAttributeString("PULSETYPE", p_objContentAccessInfoArr[i].m_strPulseType);
                    m_objXmlWriter.WriteAttributeString("PULSELINEPREVALUE", p_objContentAccessInfoArr[i].m_strPulseLinePreValue);
                    m_objXmlWriter.WriteAttributeString("PULSETIMEFLAG", p_objContentAccessInfoArr[i].m_strPulseTimeFlag);
                }
                if (p_objContentAccessInfoArr[i].m_strTemperatureValue != null)
                {
                    m_objXmlWriter.WriteAttributeString("TEMPERATUREVALUE", p_objContentAccessInfoArr[i].m_strTemperatureValue);
                    m_objXmlWriter.WriteAttributeString("TEMPERATURETYPE", p_objContentAccessInfoArr[i].m_strTemperatureType);
                    m_objXmlWriter.WriteAttributeString("TEMPERATURELINEPREVALUE", p_objContentAccessInfoArr[i].m_strTemperatureLinePreValue);
                    m_objXmlWriter.WriteAttributeString("TEMPERATURETIMEFLAG", p_objContentAccessInfoArr[i].m_strTemperatureTimeFlag);
                    m_objXmlWriter.WriteAttributeString("DOWNTEMPERATURVALUE", p_objContentAccessInfoArr[i].m_strDownTemperaturValue);
                }
                if (p_objContentAccessInfoArr[i].m_strBreathType != null)
                {
                    m_objXmlWriter.WriteAttributeString("BREATHTYPE", p_objContentAccessInfoArr[i].m_strBreathType);
                    m_objXmlWriter.WriteAttributeString("BREATHVALUE", p_objContentAccessInfoArr[i].m_strBreathValue);
                    m_objXmlWriter.WriteAttributeString("BREATHTIMEFLAG", p_objContentAccessInfoArr[i].m_strBreathTimeFlag);
                }
                if (p_objContentAccessInfoArr[i].m_strEventFlag != null)
                {
                    m_objXmlWriter.WriteAttributeString("EVENTFLAG", p_objContentAccessInfoArr[i].m_strEventFlag);
                }

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();

                m_objXmlWriter.Flush();

                strXmlArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }

            return strXmlArr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>���ɵ�XML</returns>
        private string[] m_strMakeNewContentEventXmlArr(clsThreeMeasureRecordContentEventInfo[] p_objContentEventInfoArr)
        {
            string[] strContentEventArr = new string[p_objContentEventInfoArr.Length];

            for (int i = 0; i < p_objContentEventInfoArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("ThreeMeasureRecordContentEvent");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objContentEventInfoArr[i].m_strInPatientID);
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objContentEventInfoArr[i].m_strInPatientDate);
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objContentEventInfoArr[i].m_strOpenDate);
                m_objXmlWriter.WriteAttributeString("CREATETIME", p_objContentEventInfoArr[i].m_strCreateTime);
                m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objContentEventInfoArr[i].m_strModifyDate);
                m_objXmlWriter.WriteAttributeString("THREEMEASUREEVENTID", p_objContentEventInfoArr[i].m_strThreeMeasureEventID);
                m_objXmlWriter.WriteAttributeString("BEGINEVENTDATE", p_objContentEventInfoArr[i].m_strBeginEventDate);
                m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objContentEventInfoArr[i].m_strModifyUserID);
                m_objXmlWriter.WriteAttributeString("EVENTVALUE", p_objContentEventInfoArr[i].m_strEventValue.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();

                m_objXmlWriter.Flush();

                strContentEventArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }

            return strContentEventArr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objInfo"></param>
        /// <returns>
        /// ���������

        /// 0��ʧ�ܡ�

        /// 1���ɹ���

        /// </returns>
        public long m_lngAddNew(clsThreeMeasureRecordInfo p_objInfo, clsThreeMeasureRecordContentInfo p_objContentInfo, clsThreeMeasureRecordContentAccessInfo[] p_objContentAccessInfoArr, clsThreeMeasureRecordContentEventInfo[] p_objContentEventInfoArr, bool p_blnIsAddNew)
        {
            //clsThreeMeasureRecordService m_objThreeMeasureRecordServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            long lngRes = 0;
            try
            {
                string strMainXml = m_strMakeNewMainXml(p_objInfo);
                string strContentXml = m_strMakeNewContentXml(p_objContentInfo);
                string[] strContentAccessXmlArr = m_strMakeNewContentAccessXmlArr(p_objContentAccessInfoArr);
                string[] strContentEventXmlArr = m_strMakeNewContentEventXmlArr(p_objContentEventInfoArr);
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNew(strMainXml, strContentXml, strContentAccessXmlArr, strContentEventXmlArr, p_blnIsAddNew);
            }
            finally
            {
                //m_objThreeMeasureRecordServ.Dispose();
            }
            return lngRes;
        }

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
            //clsThreeMeasureRecordService m_objThreeMeasureRecordServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsThreeMeasureRecordService_m_lngCheckNewCreateDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_blnIsAddNew);
            }
            finally
            {
                //m_objThreeMeasureRecordServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ����޸�ʱ���Ƿ������޸�ʱ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_blnIsLast"></param>
        /// <returns></returns>
        public long m_lngCheckLastModifyDate(string p_strInPatientID, string p_strInPatientDate, string p_strOepnDate, string p_strLastModifyDate, out bool p_blnIsLast, out bool p_blnIsDelete, out string p_strChangedUserID, out string p_strChangedDate)
        {
            //clsThreeMeasureRecordService m_objThreeMeasureRecordServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngCheckLastModifyDate(p_strInPatientID, p_strInPatientDate, p_strOepnDate, p_strLastModifyDate, out p_blnIsLast, out p_blnIsDelete, out p_strChangedUserID, out p_strChangedDate);
            }
            finally
            {
                //m_objThreeMeasureRecordServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngDeleteRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strOperatorID)
        {
            //clsThreeMeasureRecordService m_objThreeMeasureRecordServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeleteRecord(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strOperatorID);
            }
            finally
            {
                //m_objThreeMeasureRecordServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public clsThreeMeasureRecordInfo[] m_objGetThreeMeasureRecordInfoArr(string p_strInPatientID, string p_strInPatientDate)
        {
            string strXML = "";
            int intRows = 0;

            //clsThreeMeasureRecordService m_objThreeMeasureRecordServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetPatientRecord(p_strInPatientID, p_strInPatientDate, ref strXML, ref intRows);
            }
            finally
            {
                //m_objThreeMeasureRecordServ.Dispose();
            }

            clsThreeMeasureRecordInfo[] objInfoArr = null;

            if (lngRes > 0 && intRows > 0)
            {
                objInfoArr = new clsThreeMeasureRecordInfo[intRows];

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
                                objInfoArr[intIndex] = new clsThreeMeasureRecordInfo();
                                objInfoArr[intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID");
                                objInfoArr[intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE");
                                objInfoArr[intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE");
                                objInfoArr[intIndex].m_strCreateDate = objReader.GetAttribute("CREATEDATE");
                                objInfoArr[intIndex].m_strCreateID = objReader.GetAttribute("CREATEID");

                                objInfoArr[intIndex].m_objXmlValue = new clsThreeMeasureXmlValue();
                                objInfoArr[intIndex].m_objXmlValue.m_strRecordDate = objInfoArr[intIndex].m_strCreateDate;
                                objInfoArr[intIndex].m_objXmlValue.m_strSpecialDateXml = objReader.GetAttribute("SPECIALDATEXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strEventXml = objReader.GetAttribute("EVENTXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strBreathXml = objReader.GetAttribute("BREATHXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strInputXml = objReader.GetAttribute("INPUTXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strDejectaXml = objReader.GetAttribute("DEJECTAXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strPeeXml = objReader.GetAttribute("PEEXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strOutStreamXml = objReader.GetAttribute("OUTSTREAMXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strPressureXml = objReader.GetAttribute("PRESSUREXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strPressureXml2 = objReader.GetAttribute("PRESSURE2XML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strWeightXml = objReader.GetAttribute("WEIGHTXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strSkinTestXml = objReader.GetAttribute("SKINTESTXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strOtherXml = objReader.GetAttribute("OTHERXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strPulseXml = objReader.GetAttribute("PULSEXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strTemperatureXml = objReader.GetAttribute("TEMPERATUREXML").Replace('��', '\'');
                                objInfoArr[intIndex].m_objXmlValue.m_strOtherName = objReader.GetAttribute("OTHERNAME").Replace('��', '\'');

                                objInfoArr[intIndex].m_strIfConfirm = objReader.GetAttribute("IFCONFIRM");
                                objInfoArr[intIndex].m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                objInfoArr[intIndex].m_strConfirmReasonXMLString = objReader.GetAttribute("CONFIRMREASONXMLSTRING").Replace('��', '\'');
                                objInfoArr[intIndex].m_strStatus = objReader.GetAttribute("STATUS");
                                objInfoArr[intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE");
                                objInfoArr[intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID");
                                objInfoArr[intIndex].m_strFirstPrintDate = objReader.GetAttribute("FIRSTPRINTDATE");

                                objInfoArr[intIndex].m_strModifyDate = objReader.GetAttribute("MODIFYDATE");

                                intIndex++;
                            }
                            break;
                    }
                }
            }
            else
            {
                objInfoArr = new clsThreeMeasureRecordInfo[0];
            }

            return objInfoArr;
        }

        /// <summary>
        /// ���ø�����¼�ĵ�һ�δ�ӡʱ��

        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strTableName"></param>
        /// <returns></returns>
        public long m_lngSetFirstPrintDate(string[] p_strInPatientIDArr, string[] p_strInPatientDateArr, string[] p_strOpenDateArr)
        {
            //clsThreeMeasureRecordService m_objThreeMeasureRecordServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetFirstPrintDate(p_strInPatientIDArr, p_strInPatientDateArr, p_strOpenDateArr);
            }
            finally
            {
                //m_objThreeMeasureRecordServ.Dispose();
            }
            return lngRes;
        }

        public bool m_blnCheckTValueExistBySelectTime(string p_strInpatientId, string p_strInpatientDate, string p_strCreateTime)
        {
            bool blnCheck = false;

            //clsThreeMeasureRecordService objServ =
            //    (clsThreeMeasureRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsThreeMeasureRecordService));

            blnCheck = (new weCare.Proxy.ProxyEmr()).Service.m_blnCheckTValueExistBySelectTime(p_strInpatientId, p_strInpatientDate, p_strCreateTime);
      
            return blnCheck;
        }
    }

    #region ���������õ���

    /// <summary>
    /// 
    /// </summary>
    //	[Serializable]
    //	public class clsThreeMeasureRecordInfo
    //	{
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strModifyDate;
    //
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
    //		public string m_strCreateDate;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public string m_strCreateID;
    //
    //		/// <summary>
    //		/// 
    //		/// </summary>
    //		public clsThreeMeasureXmlValue m_objXmlValue;
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
    //		public string m_strFirstPrintDate;
    //
    //	}

    /// <summary>
    /// 
    /// </summary>
    public class clsThreeMeasureRecordContentInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string m_strInPatientID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strInPatientDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strOpenDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strModifyDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strModifyUserID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strSpecialDateIsNew;

        /// <summary>
        /// 
        /// </summary>
        public string m_strInputStreamValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDejectaNeedWeight;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDejectaBeforeTimes;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDejectaAfterMoreTimes;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDejectaAfterTimes;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDejectaClysisTimes;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDejectaWeight;

        /// <summary>
        /// 
        /// </summary>
        public string m_strCanDejecta;

        /// <summary>
        /// 
        /// </summary>
        public string m_strPeeIsIrretention;

        /// <summary>
        /// 
        /// </summary>
        public string m_strPeeValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strOutStreamValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDiastolicValue1;

        /// <summary>
        /// 
        /// </summary>
        public string m_strSystolicValue1;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDiastolicValue2;

        /// <summary>
        /// 
        /// </summary>
        public string m_strSystolicValue2;

        /// <summary>
        /// 
        /// </summary>
        public string m_strWeightType;

        /// <summary>
        /// 
        /// </summary>
        public string m_strWeightValue;

    }

    /// <summary>
    /// 
    /// </summary>
    public class clsThreeMeasureRecordContentAccessInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string m_strInPatientID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strInPatientDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strOpenDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strCreateTime;

        /// <summary>
        /// 
        /// </summary>
        public string m_strModifyDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strModifyUserID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strPulseValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strPulseType;

        /// <summary>
        /// 
        /// </summary>
        public string m_strPulseLinePreValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strPulseTimeFlag;

        /// <summary>
        /// 
        /// </summary>
        public string m_strTemperatureValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strTemperatureType;

        /// <summary>
        /// 
        /// </summary>
        public string m_strTemperatureLinePreValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strTemperatureTimeFlag;

        /// <summary>
        /// 
        /// </summary>
        public string m_strDownTemperaturValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strBreathType;

        /// <summary>
        /// 
        /// </summary>
        public string m_strBreathValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strBreathTimeFlag;

        /// <summary>
        /// 
        /// </summary>
        public string m_strEventFlag;

        public string m_strBreathLinePreValue;

        public string m_strStayOutType;

        /// <summary>
        /// 
        /// </summary>
        public string m_strStayOutValue;

        /// <summary>
        /// 
        /// </summary>
        public string m_strStayOutTimeFlag;

        /// <summary>
        /// 
        /// </summary>
        //public string m_strEventFlag;

        public string m_strStayOutLinePreValue;
    }

    /// <summary>
    /// 
    /// </summary>
    public class clsThreeMeasureRecordContentEventInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string m_strInPatientID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strInPatientDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strOpenDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strCreateTime;

        /// <summary>
        /// 
        /// </summary>
        public string m_strModifyDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strThreeMeasureEventID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strBeginEventDate;

        /// <summary>
        /// 
        /// </summary>
        public string m_strModifyUserID;

        /// <summary>
        /// 
        /// </summary>
        public string m_strEventValue;

    }
    #endregion
}
