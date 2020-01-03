using System;
using System.Data;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// Summary description for clsSIRSEvaluationDomain.
    /// </summary>
    public class clsSIRSEvaluationDomain
    {
        /// <summary>
        /// 生成Xml的缓冲
        /// </summary>
        private MemoryStream m_objXmlMemStream;
        /// <summary>
        /// 生成Xml的工具
        /// </summary>
        private XmlTextWriter m_objXmlWriter;
        ///  <summary>
        /// 读取Xml工具输入参数		
        /// </summary>
        private XmlParserContext m_objXmlParser;

        public clsSIRSEvaluationDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//清空原来的字符
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

        }
        #region New 
        #region Load All Create Date
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strFromDate"></param>
        /// <param name="p_strToDate"></param>
        /// <returns></returns>
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate, string p_strFromDate, string p_strToDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[] dtmCreateRecordDateArr = null;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("sirsevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
                                dtmCreateRecordDateArr[intIndex] = DateTime.Parse(objReader.GetAttribute("ACTIVITYTIME"));
                                intIndex++;
                            }
                            break;
                    }
                }
            }
            return dtmCreateRecordDateArr;
        }
        #endregion

        #region Save
        public long m_lngSave(clsEvalInfoOfSIRSEvaluation p_objSIRSEvaluationDB)
        {
            if (p_objSIRSEvaluationDB == null)
                return -1;

            string strMainXml = m_mthGetMainXml(p_objSIRSEvaluationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("sirsevaluation", strMainXml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objAPACHEIIValuationDB"></param>
        /// <returns></returns>
        private string m_mthGetMainXml(clsEvalInfoOfSIRSEvaluation p_objSIRSEvaluationDB)
        {
            if (p_objSIRSEvaluationDB == null)
                return null;

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RECORDMASTER");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objSIRSEvaluationDB.strPatientID.Replace('\'', 'き').Trim());
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objSIRSEvaluationDB.strInPatientDate.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objSIRSEvaluationDB.strActivityTime.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objSIRSEvaluationDB.strEvalDoctorID.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("AGEGROUP", p_objSIRSEvaluationDB.strAgeGroup.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BREATH", p_objSIRSEvaluationDB.strBreath.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HEARTRATE", p_objSIRSEvaluationDB.strHeartRate.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TEMPERATURE", p_objSIRSEvaluationDB.strTemperature.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("WBCORBACILLUS", p_objSIRSEvaluationDB.strWBCorBacillus.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("WBCORBACILLUSSEL", p_objSIRSEvaluationDB.strWBCorBacillusSel.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BREATHEVAL", p_objSIRSEvaluationDB.strBreathEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HEARTRATEEVAL", p_objSIRSEvaluationDB.strHeartRateEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TEMPERATUREEVAL", p_objSIRSEvaluationDB.strTemperatureEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("WBCORBACILLUSEVAL", p_objSIRSEvaluationDB.strWBCorBacillusEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objSIRSEvaluationDB.strTotalEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("STATUS", "0");

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region 读取信息
        public long m_lngGetSIRSValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsEvalInfoOfSIRSEvaluation p_objSIRSEvaluation)
        {
            p_objSIRSEvaluation = null;

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return -2;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("sirsevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

            if (lngRes <= 0)
                return -1;

            if (intRows <= 0)
                return 0;

            XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
            objReader.WhitespaceHandling = WhitespaceHandling.None;

            p_objSIRSEvaluation = new clsEvalInfoOfSIRSEvaluation();

            while (objReader.Read())
            {
                switch (objReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (objReader.HasAttributes)
                        {
                            p_objSIRSEvaluation.strPatientID = objReader.GetAttribute("INPATIENTNO").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('き', '\'');

                            p_objSIRSEvaluation.strEvalDoctorID = objReader.GetAttribute("EVALDOCTORID").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strAgeGroup = objReader.GetAttribute("AGEGROUP").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strBreath = objReader.GetAttribute("BREATH").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strHeartRate = objReader.GetAttribute("HEARTRATE").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strTemperature = objReader.GetAttribute("TEMPERATURE").ToString().Replace('き', '\'');

                            p_objSIRSEvaluation.strWBCorBacillus = objReader.GetAttribute("WBCORBACILLUS").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strWBCorBacillusSel = objReader.GetAttribute("WBCORBACILLUSSEL").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strWBCorBacillusEval = objReader.GetAttribute("WBCORBACILLUSEVAL").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strBreathEval = objReader.GetAttribute("BREATHEVAL").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strHeartRateEval = objReader.GetAttribute("HEARTRATEEVAL").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strTemperatureEval = objReader.GetAttribute("TEMPERATUREEVAL").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strWBCorBacillusEval = objReader.GetAttribute("WBCORBACILLUSEVAL").ToString().Replace('き', '\'');
                            p_objSIRSEvaluation.strTotalEval = objReader.GetAttribute("TOTALEVAL").ToString().Replace('き', '\'');
                        }
                        break;
                }
            }
            return 1;
        }
        #endregion

        #region Delete
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientNO='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " ActivityTime='" + p_strCreateDate + "'" + " />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("sirsevaluation", strDeactiveXML);
            if (lngRes <= 0)
                return -1;

            return 1;
        }
        #endregion
        #endregion

    }
    public class clsEvalInfoOfSIRSEvaluation
    {
        public string strPatientID;
        public string strInPatientDate;
        public string strModifyDate;
        public string strActivityTime;

        public string strEvalDoctorID;
        public string strEvalDoctorName;
        public string strAgeGroup;
        public string strBreath;
        public string strHeartRate;
        public string strTemperature;

        public string strWBCorBacillus;
        public string strWBCorBacillusSel;
        public string strBreathEval;
        public string strHeartRateEval;
        public string strTemperatureEval;
        public string strWBCorBacillusEval;
        public string strTotalEval;
    }
}
