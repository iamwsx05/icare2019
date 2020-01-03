using System;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
//using iCare.Common;

namespace iCare.ICU.Evaluation
{
    #region Class
    /// <summary>
    /// 方象垂佚連
    /// </summary>
    public class APACHEIIValuationDB
    {
        public string strPatientID;
        public string strInPatientDate;
        public string strActivityTime;
        public string strModifyDate;

        public string strAgeGroup;
        public string strEvalDoctorID;
        public string strTemperature;
        public string strAdvArteryPress;
        public string strHeartRate;
        public string strAmountLeucocyte;
        public string strFiO2;
        public string strBreath;
        public string strPao2;
        public string strDo2;
        public string strArteryBloodpH;
        public string strBloodNa;
        public string strBloodK;
        public string strGCS;
        public string strBloodFlesh;
        public string strBloodCorpuscle;
        public string strHCO;
        public string strKidneyProstrate;
        public string strHealthGroup;
        public string strPatientUnOp1;
        public string strPatientUnOp2;
        public string strOthers;
        public string strNeurotic;
        public string strNoInRange;
        public string strHurts;
        public string strPatientUnOp1Eval;
        public string strPatientUnOp2Eval;
        public string strOthersEval;
        public string strNeuroticEval;
        public string strNoInRangeEval;
        public string strHurtsEval;
        public string strPatientSelOpEval;
        public string strPatientAfterEmergency;
        public string strMainReasonNoIn;
        public string strMainReasonNoInEval;
        public string strTemperatureEval;
        public string strAdvArteryPressEval;
        public string strHeartRateEval;
        public string strAmountLeucocyteEval;
        public string strBreathEval;
        public string strPao2Eval;
        public string strDo2Eval;
        public string strArteryBloodpHEval;
        public string strBloodNaEval;
        public string strBloodKEval;
        public string strGCSEval;
        public string strBloodFleshEval;
        public string strBloodCorpuscleEval;
        public string strHCOEval;
        public string strTotalEval;
        public string strREval;
        public string strPaCO2;
    }
    #endregion

    /// <summary>
    /// Summary description for APACHEIIValuationDomain.
    /// </summary>
    public class APACHEIIValuationDomain
    {
        #region Member		
        /// <summary>
        /// 伏撹Xml議産喝
        /// </summary>
        private MemoryStream m_objXmlMemStream;
        /// <summary>
        /// 伏撹Xml議垢醤
        /// </summary>
        private XmlTextWriter m_objXmlWriter;
        ///  <summary>
        /// 響函Xml垢醤補秘歌方		
        /// </summary>
        private XmlParserContext m_objXmlParser;
        #endregion

        #region Constructor
        public APACHEIIValuationDomain()
        {
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//賠腎圻栖議忖憲
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

        }
        #endregion


        public long lngAddNewRecordOfAutoEval(APACHEIIValuationDB objAPACHEIIValuationDB)//
        {
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_lngAddNewRecordOfAutoEval("apacheiivaluation", ""); //
        }

        #region New
        #region Save
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objAPACHEIIValuationDB"></param>
        /// <returns></returns>
        public long m_lngSave(APACHEIIValuationDB p_objAPACHEIIValuationDB)
        {
            if (p_objAPACHEIIValuationDB == null)
                return -1;

            string strMainXml = m_mthGetMainXml(p_objAPACHEIIValuationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("apacheiivaluation", strMainXml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objAPACHEIIValuationDB"></param>
        /// <returns></returns>
        private string m_mthGetMainXml(APACHEIIValuationDB p_objAPACHEIIValuationDB)
        {
            if (p_objAPACHEIIValuationDB == null)
                return null;

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objAPACHEIIValuationDB.strPatientID.Replace('\'', 'き').Trim());
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objAPACHEIIValuationDB.strInPatientDate.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objAPACHEIIValuationDB.strActivityTime.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objAPACHEIIValuationDB.strEvalDoctorID.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("AGEGROUP", p_objAPACHEIIValuationDB.strAgeGroup.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TEMPERATURE", p_objAPACHEIIValuationDB.strTemperature.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ADVARTERYPRESS", p_objAPACHEIIValuationDB.strAdvArteryPress.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("HEARTRATE", p_objAPACHEIIValuationDB.strHeartRate.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("AMOUNTLEUCOCYTE", p_objAPACHEIIValuationDB.strAmountLeucocyte.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("FIO2", p_objAPACHEIIValuationDB.strFiO2.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BREATH", p_objAPACHEIIValuationDB.strBreath.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("PAO2", p_objAPACHEIIValuationDB.strPao2.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("DO2", p_objAPACHEIIValuationDB.strDo2.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ARTERYBLOODPH", p_objAPACHEIIValuationDB.strArteryBloodpH.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODNA", p_objAPACHEIIValuationDB.strBloodNa.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("BLOODK", p_objAPACHEIIValuationDB.strBloodK.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("GCS", p_objAPACHEIIValuationDB.strGCS.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODFLESH", p_objAPACHEIIValuationDB.strBloodFlesh.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODCORPUSCLE", p_objAPACHEIIValuationDB.strBloodCorpuscle.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HCO", p_objAPACHEIIValuationDB.strHCO.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("KIDNEYPROSTRATE", p_objAPACHEIIValuationDB.strKidneyProstrate.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HEALTHGROUP", p_objAPACHEIIValuationDB.strHealthGroup.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PATIENTUNOP1", p_objAPACHEIIValuationDB.strPatientUnOp1.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PATIENTUNOP2", p_objAPACHEIIValuationDB.strPatientUnOp2.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("OTHERS", p_objAPACHEIIValuationDB.strOthers.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("NEUROTIC", p_objAPACHEIIValuationDB.strNeurotic.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("NOINRANGE", p_objAPACHEIIValuationDB.strNoInRange.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HURTS", p_objAPACHEIIValuationDB.strHurts.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("PATIENTUNOP1EVAL", p_objAPACHEIIValuationDB.strPatientUnOp1Eval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PATIENTUNOP2EVAL", p_objAPACHEIIValuationDB.strPatientUnOp2Eval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("OTHERSEVAL", p_objAPACHEIIValuationDB.strOthersEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("NEUROTICEVAL", p_objAPACHEIIValuationDB.strNeuroticEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("NOINRANGEEVAL", p_objAPACHEIIValuationDB.strNoInRangeEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HURTSEVAL", p_objAPACHEIIValuationDB.strHurtsEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PATIENTSELOPEVAL", p_objAPACHEIIValuationDB.strPatientSelOpEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PATIENTAFTEREMERGENCY", p_objAPACHEIIValuationDB.strPatientAfterEmergency.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("MAINREASONNOIN", p_objAPACHEIIValuationDB.strMainReasonNoIn.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("MAINREASONNOINEVAL", p_objAPACHEIIValuationDB.strMainReasonNoInEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TEMPERATUREEVAL", p_objAPACHEIIValuationDB.strTemperatureEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ADVARTERYPRESSEVAL", p_objAPACHEIIValuationDB.strAdvArteryPressEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HEARTRATEEVAL", p_objAPACHEIIValuationDB.strHeartRateEval.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("AMOUNTLEUCOCYTEEVAL", p_objAPACHEIIValuationDB.strAmountLeucocyteEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BREATHEVAL", p_objAPACHEIIValuationDB.strBreathEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PAO2EVAL", p_objAPACHEIIValuationDB.strPao2Eval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("DO2EVAL", p_objAPACHEIIValuationDB.strDo2Eval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ARTERYBLOODPHEVAL", p_objAPACHEIIValuationDB.strArteryBloodpHEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODNAEVAL", p_objAPACHEIIValuationDB.strBloodNaEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODKEVAL", p_objAPACHEIIValuationDB.strBloodKEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("GCSEVAL", p_objAPACHEIIValuationDB.strGCSEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODFLESHEVAL", p_objAPACHEIIValuationDB.strBloodFleshEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BLOODCORPUSCLEEVAL", p_objAPACHEIIValuationDB.strBloodCorpuscleEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("HCOEVAL", p_objAPACHEIIValuationDB.strHCOEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objAPACHEIIValuationDB.strTotalEval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("REVAL", p_objAPACHEIIValuationDB.strREval.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("PACO2", p_objAPACHEIIValuationDB.strPaCO2.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("STATUS", "0");

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("apacheiivaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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

        #region 響函佚連
        public long m_lngGetApacheIIValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out APACHEIIValuationDB p_objApacheIIValuation)
        {
            p_objApacheIIValuation = null;

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return -2;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("apacheiivaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

            if (lngRes <= 0)
                return -1;

            if (intRows <= 0)
                return 0;

            XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
            objReader.WhitespaceHandling = WhitespaceHandling.None;

            p_objApacheIIValuation = new APACHEIIValuationDB();

            while (objReader.Read())
            {
                switch (objReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (objReader.HasAttributes)
                        {
                            p_objApacheIIValuation.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strEvalDoctorID = objReader.GetAttribute("EVALDOCTORID").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strAgeGroup = objReader.GetAttribute("AGEGROUP").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strTemperature = objReader.GetAttribute("TEMPERATURE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strAdvArteryPress = objReader.GetAttribute("ADVARTERYPRESS").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHeartRate = objReader.GetAttribute("HEARTRATE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strAmountLeucocyte = objReader.GetAttribute("AMOUNTLEUCOCYTE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strFiO2 = objReader.GetAttribute("FIO2").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBreath = objReader.GetAttribute("BREATH").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPao2 = objReader.GetAttribute("PAO2").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strDo2 = objReader.GetAttribute("DO2").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strArteryBloodpH = objReader.GetAttribute("ARTERYBLOODPH").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodNa = objReader.GetAttribute("BLOODNA").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodK = objReader.GetAttribute("BLOODK").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strGCS = objReader.GetAttribute("GCS").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodFlesh = objReader.GetAttribute("BLOODFLESH").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodCorpuscle = objReader.GetAttribute("BLOODCORPUSCLE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHCO = objReader.GetAttribute("HCO").ToString().Replace('き', '\'');

                            p_objApacheIIValuation.strKidneyProstrate = objReader.GetAttribute("KIDNEYPROSTRATE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHealthGroup = objReader.GetAttribute("HEALTHGROUP").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPatientUnOp1 = objReader.GetAttribute("PATIENTUNOP1").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPatientUnOp2 = objReader.GetAttribute("PATIENTUNOP2").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strOthers = objReader.GetAttribute("OTHERS").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strNeurotic = objReader.GetAttribute("NEUROTIC").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strNoInRange = objReader.GetAttribute("NOINRANGE").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHurts = objReader.GetAttribute("HURTS").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPatientUnOp1Eval = objReader.GetAttribute("PATIENTUNOP1EVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPatientUnOp2Eval = objReader.GetAttribute("PATIENTUNOP2EVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strOthersEval = objReader.GetAttribute("OTHERSEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strNeuroticEval = objReader.GetAttribute("NEUROTICEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strNoInRangeEval = objReader.GetAttribute("NOINRANGEEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHurtsEval = objReader.GetAttribute("HURTSEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPatientSelOpEval = objReader.GetAttribute("PATIENTSELOPEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPatientAfterEmergency = objReader.GetAttribute("PATIENTAFTEREMERGENCY").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strMainReasonNoIn = objReader.GetAttribute("MAINREASONNOIN").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strMainReasonNoInEval = objReader.GetAttribute("MAINREASONNOINEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strTemperatureEval = objReader.GetAttribute("TEMPERATUREEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strAdvArteryPressEval = objReader.GetAttribute("ADVARTERYPRESSEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHeartRateEval = objReader.GetAttribute("HEARTRATEEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strAmountLeucocyteEval = objReader.GetAttribute("AMOUNTLEUCOCYTEEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBreathEval = objReader.GetAttribute("BREATHEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPao2Eval = objReader.GetAttribute("PAO2EVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strDo2Eval = objReader.GetAttribute("DO2EVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strArteryBloodpHEval = objReader.GetAttribute("ARTERYBLOODPHEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodNaEval = objReader.GetAttribute("BLOODNAEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodKEval = objReader.GetAttribute("BLOODKEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strGCSEval = objReader.GetAttribute("GCSEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodFleshEval = objReader.GetAttribute("BLOODFLESHEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strBloodCorpuscleEval = objReader.GetAttribute("BLOODCORPUSCLEEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strHCOEval = objReader.GetAttribute("HCOEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strTotalEval = objReader.GetAttribute("TOTALEVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strREval = objReader.GetAttribute("REVAL").ToString().Replace('き', '\'');
                            p_objApacheIIValuation.strPaCO2 = objReader.GetAttribute("PACO2").ToString().Replace('き', '\'');
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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("apacheiivaluation", strDeactiveXML);

            if (lngRes <= 0)
                return -1;

            return 1;
        }
        #endregion
        #endregion

    }
}
