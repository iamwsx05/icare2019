using System;
using System.Data;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// MODS���ֵ���ҵ�߼���
    /// </summary>
    public class clsMODSEvaluationDomain
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
        public clsMODSEvaluationDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

        }


        //Load All Create Date

        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate, string p_strFromDate, string p_strToDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[] dtmCreateRecordDateArr = null;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("modsevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
        //END  Load All Create Date

        // Save********
        public long m_lngSave(clsEvalInfoOfMODSEvaluation p_objMODSEvaluationDB)
        {
            if (p_objMODSEvaluationDB == null)
                return -1;

            string strMainXml = m_mthGetMainXml(p_objMODSEvaluationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("modsevaluation", strMainXml);
        }

        private string m_mthGetMainXml(clsEvalInfoOfMODSEvaluation p_objMODSEvaluationDB)
        {
            if (p_objMODSEvaluationDB == null)
                return null;

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RECORDMASTER");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objMODSEvaluationDB.strPatientID.Replace('\'', '��').Trim());
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objMODSEvaluationDB.strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objMODSEvaluationDB.strActivityTime.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objMODSEvaluationDB.strEvalDoctorID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PA02", p_objMODSEvaluationDB.strPa02.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FI02", p_objMODSEvaluationDB.strFi02.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XJG", p_objMODSEvaluationDB.strXJG.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DHS", p_objMODSEvaluationDB.strDHS.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HR", p_objMODSEvaluationDB.strHR.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YFY", p_objMODSEvaluationDB.strYFY.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PJDMY", p_objMODSEvaluationDB.strPJDMY.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XXB", p_objMODSEvaluationDB.strXXB.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPENEYES", p_objMODSEvaluationDB.strOpenEyes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SAY", p_objMODSEvaluationDB.strSay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPORT", p_objMODSEvaluationDB.strSport.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("BREATHEVAL", p_objMODSEvaluationDB.strBreathEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XJGEVAL", p_objMODSEvaluationDB.strXJGEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DHSEVAL", p_objMODSEvaluationDB.strDHSEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XXGEVAL", p_objMODSEvaluationDB.strXXGEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODEVAL", p_objMODSEvaluationDB.strBloodEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NERVEEVAL", p_objMODSEvaluationDB.strNerveEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objMODSEvaluationDB.strTotalEval.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", "0");

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        //END Save

        //��ȡ��Ϣ**********
        public long m_lngGetMODSValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsEvalInfoOfMODSEvaluation p_objMODSEvaluation)
        {
            p_objMODSEvaluation = null;

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return -2;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("modsevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

            if (lngRes <= 0)
                return -1;

            if (intRows <= 0)
                return 0;

            XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
            objReader.WhitespaceHandling = WhitespaceHandling.None;

            p_objMODSEvaluation = new clsEvalInfoOfMODSEvaluation();

            while (objReader.Read())
            {
                switch (objReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (objReader.HasAttributes)
                        {
                            p_objMODSEvaluation.strPatientID = objReader.GetAttribute("INPATIENTNO").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('��', '\'');

                            p_objMODSEvaluation.strEvalDoctorID = objReader.GetAttribute("EVALDOCTORID").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strPa02 = objReader.GetAttribute("PA02").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strFi02 = objReader.GetAttribute("FI02").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strXJG = objReader.GetAttribute("XJG").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strDHS = objReader.GetAttribute("DHS").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strHR = objReader.GetAttribute("HR").ToString().Replace('��', '\'');

                            p_objMODSEvaluation.strYFY = objReader.GetAttribute("YFY").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strPJDMY = objReader.GetAttribute("PJDMY").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strXXB = objReader.GetAttribute("XXB").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strOpenEyes = objReader.GetAttribute("OPENEYES").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strSay = objReader.GetAttribute("SAY").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strSport = objReader.GetAttribute("SPORT").ToString().Replace('��', '\'');

                            p_objMODSEvaluation.strBreathEval = objReader.GetAttribute("BREATHEVAL").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strXJGEval = objReader.GetAttribute("XJGEVAL").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strDHSEval = objReader.GetAttribute("DHSEVAL").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strXXGEval = objReader.GetAttribute("XXGEVAL").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strBloodEval = objReader.GetAttribute("BLOODEVAL").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strNerveEval = objReader.GetAttribute("NERVEEVAL").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strTotalEval = objReader.GetAttribute("TOTALEVAL").ToString().Replace('��', '\'');
                        }
                        break;
                }
            }
            return 1;
        }
        //END ��ȡ��Ϣ

        //Delete  ************
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientNO='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " ActivityTime='" + p_strCreateDate + "'" + " />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("modsevaluation", strDeactiveXML);

            if (lngRes <= 0)
                return -1;

            return 1;
        }
        //End Delete


    }


    public class clsEvalInfoOfMODSEvaluation
    {
        public string strPatientID;
        public string strInPatientDate;
        public string strModifyDate;
        public string strActivityTime;

        public string strEvalDoctorID;
        public string strEvalDoctorName;

        public string strPa02;//ӦΪСд��ĸo����������0
        public string strFi02;//ӦΪСд��ĸo����������0
        public string strXJG;
        public string strDHS;
        public string strHR;
        public string strYFY;
        public string strPJDMY;
        public string strXXB;
        public string strOpenEyes;
        public string strSay;
        public string strSport;

        public string strBreathEval;
        public string strXJGEval;
        public string strDHSEval;
        public string strXXGEval;
        public string strBloodEval;
        public string strNerveEval;
        public string strTotalEval;


        public string strHeartRateEval;
        public string strTemperatureEval;
        public string strWBCorBacillusEval;
        //		public string strEvalDoctorID;
        //		public string strEvalDoctorName;
        //		public string strModifyDate;

        public string strAgeGroup;
        public string strBreath;
        public string strHeartRate;
        public string strTemperature;

        public string strCure;
        public string strChangeGood;
        public string strBad;
        public string strDeath;
        public string strCK;
        public string strCK_MB;
        public string strLDH;
        public string strCVP;
        public string strPaO2;
        public string strPaCO2;
        public string strPa02_Fi02;
        public string strModel;
        public string strPIP;
        public string strMAP;
        public string strPEEP;
        public string strTi;
        public string strMV;
        public string strRR;
        public string strCd;
        public string strCs;


        public string strInHospitalDays;
        public string strOutHospitalDays;
        public string strInDiagnose;
        public string strOutdiagnose;
        public string strOutMode;
        public string strBloodPressure;
        public string strWeight;
        public string strXGNCK;
        public string strXGNCK_MB;
        public string strXGNLDH;
        public string strXGNCVP;
        public string strFGNPAO2;
        public string strFGNPACO2;
        public string strFGNPA02_FI02;
        public string strHXJMODEL;
        public string strHXJPIP;
        public string strHXJMAP;
        public string strHXJPEEP;
        public string strHXJTI;
        public string strHXJMV;
        public string strHXJRR;
        public string strHXJCD;
        public string strHXJCS;
        public string strGGNALT;
        public string strGGNTOTALGALLBLADDER;
        public string strSGNBLOOD;
        public string strSGNBUN;
        public string strSGNURINEWEIGHT;
        public string strNXGNTT;
        public string strNXGNPT;
        public string strNXGNAPPT;
        public string strNXGNFG;
        public string strNFMPH;
        public string strNFMXT;
        public string strNFMXN;
        public string strNFMXG;
        public string strNFMQT;
        public string strNFMYDS;
        public string strNFMCT;
        public string strNFMPZC;
        public string strNFMJPS;
        public string strNFMACTH;
        public string strNFMGH;
        public string strNFMIGF;
        public string strNFMIGG;
        public string strMYIGA;
        public string strMYIGM;
        public string strMYC3;
        public string strMYC4;
        public string strMYCD3;
        public string strMYCD4;
        public string strMYCD8;
        public string strMYCD16;
        public string strXCGBXP;
        public string strXCGGCFL;
        public string strXCGHB;
        public string strXCGPLT;
        public string strBYX;
        public string strXRAYNUM;
        public string strXRAYCONT;
        public string strOTHER;
        public string strSTATUS;
        public string strDEACTIVATEDDATE;
        public string strDEACTIVEDOPERATORID;


    }

    public class cls_EvalInfoOfMODSEvaluation
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

        public string strModel;

        public string strInHospitalDays;
        public string strOutHospitalDays;
        public string strInDiagnose;
        public string strOutdiagnose;
        public string strOutMode;
        public string strBloodPressure;
        public string strWeight;
        public string strXGNCK;
        public string strXGNCK_MB;
        public string strXGNLDH;
        public string strXGNCVP;
        public string strFGNPAO2;
        public string strFGNPACO2;
        public string strFGNPA02_FI02;
        public string strHXJMODEL;
        public string strHXJPIP;
        public string strHXJMAP;
        public string strHXJPEEP;
        public string strHXJTI;
        public string strHXJMV;
        public string strHXJRR;
        public string strHXJCD;
        public string strHXJCS;
        public string strGGNALT;
        public string strGGNTOTALGALLBLADDER;
        public string strSGNBLOOD;
        public string strSGNBUN;
        public string strSGNURINEWEIGHT;
        public string strNXGNTT;
        public string strNXGNPT;
        public string strNXGNAPPT;
        public string strNXGNFG;
        public string strNFMPH;
        public string strNFMXT;
        public string strNFMXN;
        public string strNFMXG;
        public string strNFMQT;
        public string strNFMYDS;
        public string strNFMCT;
        public string strNFMPZC;
        public string strNFMJPS;
        public string strNFMACTH;
        public string strNFMGH;
        public string strNFMIGF;
        public string strNFMIGG;
        public string strMYIGA;
        public string strMYIGM;
        public string strMYC3;
        public string strMYC4;
        public string strMYCD3;
        public string strMYCD4;
        public string strMYCD8;
        public string strMYCD16;
        public string strXCGBXP;
        public string strXCGGCFL;
        public string strXCGHB;
        public string strXCGPLT;
        public string strBYX;
        public string strXRAYNUM;
        public string strXRAYCONT;
        public string strOTHER;
        public string strSTATUS;
        public string strDEACTIVATEDDATE;
        public string strDEACTIVEDOPERATORID;

        public string strAPPARATUS;
        public string strGUIDE;
        public string strMEASURE;
        public string strAPPARATUSCOUNT;

    }

    public class cls_MODSEvaluationDomain
    {
        //private com.digitalwave.MODSEvaluationRecord.clsMODSEvaluationRecord  m_objServices;

        /// <summary>
        /// ����Xml�Ļ���
        /// </summary>
        private MemoryStream m_objXmlMemoryStream;
        /// <summary>
        /// ����Xml�Ĺ���
        /// </summary>
        private XmlTextWriter m_objXmlTextWriter;
        ///  <summary>
        /// ��ȡXml�����������		
        /// </summary>
        private XmlParserContext m_objXmlContextParser;
        public cls_MODSEvaluationDomain()
        {

            //m_objServices = new com.digitalwave.MODSEvaluationRecord.clsMODSEvaluationRecord();          

            m_objXmlMemoryStream = new MemoryStream(300);

            m_objXmlTextWriter = new XmlTextWriter(m_objXmlMemoryStream, System.Text.Encoding.Unicode);
            m_objXmlTextWriter.Flush();//���ԭ�����ַ�
            m_objXmlContextParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

        }

        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate, string p_strFromDate, string p_strToDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[] dtmCreateRecordDateArr = null;

            string strXml = "";
            int intRows = 0;

            long lngRes = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("modsevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

            if (lngRes > 0 && intRows > 0)
            {
                dtmCreateRecordDateArr = new DateTime[intRows];

                XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlContextParser);
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
        //save
        public long m_lngSave(cls_EvalInfoOfMODSEvaluation p_objMODSEvaluationDB)
        {
            if (p_objMODSEvaluationDB == null)
                return -1;

            string strMainXml = m_mthGetMainXml(p_objMODSEvaluationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("modsevaluation", strMainXml);
            return -1;
        }

        private string m_mthGetMainXml(cls_EvalInfoOfMODSEvaluation p_objMODSEvaluationDB)
        {
            if (p_objMODSEvaluationDB == null)
                return null;
            //
            m_objXmlMemoryStream.SetLength(0);
            //
            m_objXmlTextWriter.WriteStartDocument();
            m_objXmlTextWriter.WriteStartElement("RECORDMASTER");

            m_objXmlTextWriter.WriteAttributeString("INPATIENTNO", p_objMODSEvaluationDB.strPatientID.Replace('\'', '��').Trim());
            m_objXmlTextWriter.WriteAttributeString("INPATIENTDATE", p_objMODSEvaluationDB.strInPatientDate.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("ACTIVITYTIME", p_objMODSEvaluationDB.strActivityTime.Replace('\'', '��'));

            m_objXmlTextWriter.WriteAttributeString("EVALDOCTORID", p_objMODSEvaluationDB.strEvalDoctorID.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("INHOSPITALDAYS", p_objMODSEvaluationDB.strInHospitalDays.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("OUTHOSPITALDAYS", p_objMODSEvaluationDB.strOutHospitalDays.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("INDIAGNOSE", p_objMODSEvaluationDB.strInDiagnose.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("OUTDIAGNOSE", p_objMODSEvaluationDB.strOutdiagnose.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("OUTMODE", p_objMODSEvaluationDB.strOutMode.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("BREATH", p_objMODSEvaluationDB.strBreath.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HEARTRATE", p_objMODSEvaluationDB.strHeartRate.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("TEMPERATURE", p_objMODSEvaluationDB.strTemperature.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("BLOODPRESSURE", p_objMODSEvaluationDB.strBloodPressure.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("WEIGHT", p_objMODSEvaluationDB.strWeight.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XGNCK", p_objMODSEvaluationDB.strXGNCK.Replace('\'', '��'));

            m_objXmlTextWriter.WriteAttributeString("XGNCK_MB", p_objMODSEvaluationDB.strXGNCK_MB.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XGNLDH", p_objMODSEvaluationDB.strXGNLDH.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XGNCVP", p_objMODSEvaluationDB.strXGNCVP.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("FGNPAO2", p_objMODSEvaluationDB.strFGNPAO2.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("FGNPACO2", p_objMODSEvaluationDB.strFGNPACO2.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("FGNPA02_FI02", p_objMODSEvaluationDB.strFGNPA02_FI02.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJMODEL", p_objMODSEvaluationDB.strHXJMODEL.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJPIP", p_objMODSEvaluationDB.strHXJPIP.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJMAP", p_objMODSEvaluationDB.strHXJMAP.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJPEEP", p_objMODSEvaluationDB.strHXJPEEP.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJTI", p_objMODSEvaluationDB.strHXJTI.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJMV", p_objMODSEvaluationDB.strHXJMV.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJRR", p_objMODSEvaluationDB.strHXJRR.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJCD", p_objMODSEvaluationDB.strHXJCD.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("HXJCS", p_objMODSEvaluationDB.strHXJCS.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("GGNALT", p_objMODSEvaluationDB.strGGNALT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("GGNTOTALGALLBLADDER", p_objMODSEvaluationDB.strGGNTOTALGALLBLADDER.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("SGNBLOOD", p_objMODSEvaluationDB.strSGNBLOOD.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("SGNBUN", p_objMODSEvaluationDB.strSGNBUN.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("SGNURINEWEIGHT", p_objMODSEvaluationDB.strSGNURINEWEIGHT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NXGNTT", p_objMODSEvaluationDB.strNXGNTT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NXGNPT", p_objMODSEvaluationDB.strNXGNPT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NXGNAPPT", p_objMODSEvaluationDB.strNXGNAPPT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NXGNFG", p_objMODSEvaluationDB.strNXGNFG.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMPH", p_objMODSEvaluationDB.strNFMPH.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMXT", p_objMODSEvaluationDB.strNFMXT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMXN", p_objMODSEvaluationDB.strNFMXN.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMXG", p_objMODSEvaluationDB.strNFMXG.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMQT", p_objMODSEvaluationDB.strNFMQT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMYDS", p_objMODSEvaluationDB.strNFMYDS.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMCT", p_objMODSEvaluationDB.strNFMCT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMPZC", p_objMODSEvaluationDB.strNFMPZC.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMJPS", p_objMODSEvaluationDB.strNFMJPS.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMACTH", p_objMODSEvaluationDB.strNFMACTH.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMGH", p_objMODSEvaluationDB.strNFMGH.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("NFMIGF", p_objMODSEvaluationDB.strNFMIGF.Replace('\'', '��'));

            m_objXmlTextWriter.WriteAttributeString("NFMIGG", p_objMODSEvaluationDB.strNFMIGG.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYIGA", p_objMODSEvaluationDB.strMYIGA.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYIGM", p_objMODSEvaluationDB.strMYIGM.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYC3", p_objMODSEvaluationDB.strMYC3.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYC4", p_objMODSEvaluationDB.strMYC4.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYCD3", p_objMODSEvaluationDB.strMYCD3.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYCD4", p_objMODSEvaluationDB.strMYCD4.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYCD8", p_objMODSEvaluationDB.strMYCD8.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MYCD16", p_objMODSEvaluationDB.strMYCD16.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XCGBXP", p_objMODSEvaluationDB.strXCGBXP.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XCGGCFL", p_objMODSEvaluationDB.strXCGGCFL.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XCGHB", p_objMODSEvaluationDB.strXCGHB.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XCGPLT", p_objMODSEvaluationDB.strXCGPLT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("BYX", p_objMODSEvaluationDB.strBYX.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XRAYNUM", p_objMODSEvaluationDB.strXRAYNUM.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("XRAYCONT", p_objMODSEvaluationDB.strXRAYCONT.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("OTHER", p_objMODSEvaluationDB.strOTHER.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("STATUS", "0");

            m_objXmlTextWriter.WriteAttributeString("APPARATUS", p_objMODSEvaluationDB.strAPPARATUS.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("GUIDE", p_objMODSEvaluationDB.strGUIDE.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("MEASURE", p_objMODSEvaluationDB.strMEASURE.Replace('\'', '��'));
            m_objXmlTextWriter.WriteAttributeString("APPARATUSCOUNT", p_objMODSEvaluationDB.strAPPARATUSCOUNT.Replace('\'', '��'));

            m_objXmlTextWriter.WriteEndElement();
            m_objXmlTextWriter.WriteEndDocument();
            m_objXmlTextWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemoryStream.ToArray(), 39 * 2, (int)m_objXmlMemoryStream.Length - 39 * 2);
        }
        //END Save

        //��ȡ��Ϣ**********
        public long m_lngGetMODSValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out cls_EvalInfoOfMODSEvaluation p_objMODSEvaluation)
        {
            p_objMODSEvaluation = null;

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return -2;

            string strXml = "";
            int intRows = 0;

            long lngRes = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("modsevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

            if (lngRes <= 0)
                return -1;

            if (intRows <= 0)
                return 0;

            XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlContextParser);
            objReader.WhitespaceHandling = WhitespaceHandling.None;

            p_objMODSEvaluation = new cls_EvalInfoOfMODSEvaluation();

            while (objReader.Read())
            {
                switch (objReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (objReader.HasAttributes)
                        {
                            p_objMODSEvaluation.strPatientID = objReader.GetAttribute("INPATIENTNO").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace('��', '\'');
                            p_objMODSEvaluation.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('��', '\'');

                            p_objMODSEvaluation.strEvalDoctorID = objReader.GetAttribute("EVALDOCTORID").ToString().Replace('��', '\'');

                            p_objMODSEvaluation.strInHospitalDays = objReader.GetAttribute("INHOSPITALDAYS").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strOutHospitalDays = objReader.GetAttribute("OUTHOSPITALDAYS").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strInDiagnose = objReader.GetAttribute("INDIAGNOSE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strOutdiagnose = objReader.GetAttribute("OUTDIAGNOSE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strOutMode = objReader.GetAttribute("OUTMODE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strBreath = objReader.GetAttribute("BREATH").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHeartRate = objReader.GetAttribute("HEARTRATE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strTemperature = objReader.GetAttribute("TEMPERATURE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strBloodPressure = objReader.GetAttribute("BLOODPRESSURE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strWeight = objReader.GetAttribute("WEIGHT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXGNCK = objReader.GetAttribute("XGNCK").ToString().Replace('\'', '��');

                            p_objMODSEvaluation.strXGNCK_MB = objReader.GetAttribute("XGNCK_MB").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXGNLDH = objReader.GetAttribute("XGNLDH").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXGNCVP = objReader.GetAttribute("XGNCVP").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strFGNPAO2 = objReader.GetAttribute("FGNPAO2").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strFGNPACO2 = objReader.GetAttribute("FGNPACO2").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strFGNPA02_FI02 = objReader.GetAttribute("FGNPA02_FI02").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJMODEL = objReader.GetAttribute("HXJMODEL").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJPIP = objReader.GetAttribute("HXJPIP").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJMAP = objReader.GetAttribute("HXJMAP").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJPEEP = objReader.GetAttribute("HXJPEEP").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJTI = objReader.GetAttribute("HXJTI").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJMV = objReader.GetAttribute("HXJMV").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJRR = objReader.GetAttribute("HXJRR").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJCD = objReader.GetAttribute("HXJCD").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strHXJCS = objReader.GetAttribute("HXJCS").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strGGNALT = objReader.GetAttribute("GGNALT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strGGNTOTALGALLBLADDER = objReader.GetAttribute("GGNTOTALGALLBLADDER").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strSGNBLOOD = objReader.GetAttribute("SGNBLOOD").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strSGNBUN = objReader.GetAttribute("SGNBUN").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strSGNURINEWEIGHT = objReader.GetAttribute("SGNURINEWEIGHT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNXGNTT = objReader.GetAttribute("NXGNTT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNXGNPT = objReader.GetAttribute("NXGNPT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNXGNAPPT = objReader.GetAttribute("NXGNAPPT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNXGNFG = objReader.GetAttribute("NXGNFG").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMPH = objReader.GetAttribute("NFMPH").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMXT = objReader.GetAttribute("NFMXT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMXN = objReader.GetAttribute("NFMXN").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMXG = objReader.GetAttribute("NFMXG").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMQT = objReader.GetAttribute("NFMQT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMYDS = objReader.GetAttribute("NFMYDS").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMCT = objReader.GetAttribute("NFMCT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMPZC = objReader.GetAttribute("NFMPZC").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMJPS = objReader.GetAttribute("NFMJPS").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMACTH = objReader.GetAttribute("NFMACTH").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMGH = objReader.GetAttribute("NFMGH").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strNFMIGF = objReader.GetAttribute("NFMIGF").ToString().Replace('\'', '��');

                            p_objMODSEvaluation.strNFMIGG = objReader.GetAttribute("NFMIGG").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYIGA = objReader.GetAttribute("MYIGA").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYIGM = objReader.GetAttribute("MYIGM").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYC3 = objReader.GetAttribute("MYC3").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYC4 = objReader.GetAttribute("MYC4").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYCD3 = objReader.GetAttribute("MYCD3").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYCD4 = objReader.GetAttribute("MYCD4").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYCD8 = objReader.GetAttribute("MYCD8").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMYCD16 = objReader.GetAttribute("MYCD16").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXCGBXP = objReader.GetAttribute("XCGBXP").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXCGGCFL = objReader.GetAttribute("XCGGCFL").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXCGHB = objReader.GetAttribute("XCGHB").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXCGPLT = objReader.GetAttribute("XCGPLT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strBYX = objReader.GetAttribute("BYX").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXRAYNUM = objReader.GetAttribute("XRAYNUM").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strXRAYCONT = objReader.GetAttribute("XRAYCONT").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strOTHER = objReader.GetAttribute("OTHER").ToString().Replace('\'', '��');

                            p_objMODSEvaluation.strAPPARATUS = objReader.GetAttribute("APPARATUS").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strGUIDE = objReader.GetAttribute("GUIDE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strMEASURE = objReader.GetAttribute("MEASURE").ToString().Replace('\'', '��');
                            p_objMODSEvaluation.strAPPARATUSCOUNT = objReader.GetAttribute("APPARATUSCOUNT").ToString().Replace('\'', '��');

                        }
                        break;
                }
            }
            return 1;
        }
        //END ��ȡ��Ϣ

        //Delete  ************
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientNO='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " ActivityTime='" + p_strCreateDate + "'" + " />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("modsevaluation", strDeactiveXML);

            if (lngRes <= 0)
                return -1;

            return 1;
        }
        //End Delete
    }

}