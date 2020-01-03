using System;
using System.Xml;
using System.IO;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// clsEMR_InPatientEvaluateDomain ��ժҪ˵����
    /// </summary>
    public class clsEMR_InPatientEvaluateDomain
    {
        //private clsEMR_InPatientEvaluateServ m_objServ=new clsEMR_InPatientEvaluateServ();
        private XmlParserContext m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
        private MemoryStream m_objXmlMemStream;
        private XmlTextWriter m_objXmlWriter;

        public clsEMR_InPatientEvaluateDomain()
        {
            m_objXmlMemStream = new MemoryStream(300);
            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
        }


        public long m_lngGetAllRecordDateArr(string p_strInPatientID, out string[] p_strDateArr)
        {
            p_strDateArr = null;

            string strXML = "";
            int intRows = 0;
            long lngResult = 0;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllRecordDateArr(p_strInPatientID, out strXML, out intRows);
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
                                    strDateArr[intIndex++] = objReader.GetAttribute("INPATIENTDATE");//��ʱ����סԺʱ�䣬���Ǵ���ʱ��								
                                }
                                break;
                        }
                    }
                    p_strDateArr = strDateArr;
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        ///��ȡ����Ϣ
        public clsEMR_InPatientEvaluate m_objGetLatestRecord(string p_strXML, int p_intRows, string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_intRows > 0)//p_intRows==1
            {
                clsEMR_InPatientEvaluate objclsInPatientEvaluate = new clsEMR_InPatientEvaluate();

                XmlTextReader objReader = new XmlTextReader(p_strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsInPatientEvaluate.m_strInPatientID = p_strInPatientID;
                                objclsInPatientEvaluate.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                                objclsInPatientEvaluate.m_dtmOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE"));//p_strOpenDate;
                                objclsInPatientEvaluate.m_dtmCreateDate = DateTime.Parse(objReader.GetAttribute("CREATEDATE"));//
                                objclsInPatientEvaluate.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID");
                                objclsInPatientEvaluate.m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");
                                objclsInPatientEvaluate.m_dtmModifyDate = DateTime.Parse(objReader.GetAttribute("MODIFYDATE"));

                                objclsInPatientEvaluate.m_strPaymentMethod = objReader.GetAttribute("PAYMENTMETHOD").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strEducationDegree = objReader.GetAttribute("EDUCATIONDEGREE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strInHospitalMethod = objReader.GetAttribute("INHOSPITALMETHOD").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strInHospitalDiagnose = objReader.GetAttribute("INHOSPITALDIAGNOSE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strCaseHistory = objReader.GetAttribute("CASEHISTORY").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFamilyHistory = objReader.GetAttribute("FAMILYHISTORY").Replace('��', '\'');

                                objclsInPatientEvaluate.m_strChiefComplain = objReader.GetAttribute("CHIEFCOMPLAINT").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSensitiveHistory = objReader.GetAttribute("SENSITIVEHISTORY").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSensitiveHistory_Other = objReader.GetAttribute("SENSITIVEHISTORY_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBodyTemperature = objReader.GetAttribute("BODYTEMPERATURE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPulse = objReader.GetAttribute("PULSE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strHeartPhythm = objReader.GetAttribute("HEARTRHYTHM").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBP_Shrink = objReader.GetAttribute("BP_SHRINK").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBP_Extend = objReader.GetAttribute("BP_EXTEND").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAvoirdupois = objReader.GetAttribute("AVOIRDUPOIS").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strShengao = objReader.GetAttribute("SHENGAO").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strConsciousness = objReader.GetAttribute("CONSCIOUSNESS").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strComplexion = objReader.GetAttribute("COMPLEXION").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPhysique = objReader.GetAttribute("PHYSIQUE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPhysique_Other = objReader.GetAttribute("PHYSIQUE_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strEmotion = objReader.GetAttribute("EMOTION").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSkin = objReader.GetAttribute("SKIN").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSkin_Other = objReader.GetAttribute("SKIN_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLimbsactivity = objReader.GetAttribute("LIMBSACTIVITY").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLimbsactivity_Other = objReader.GetAttribute("LIMBSACTIVITY_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBiteSup = objReader.GetAttribute("BITESUP").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAppetite = objReader.GetAttribute("APPETITE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSleep = objReader.GetAttribute("SLEEP").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStool = objReader.GetAttribute("STOOL").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAstriction = objReader.GetAttribute("ASTRICTION").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDiarrhea = objReader.GetAttribute("DIARRHEA").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStool_Other = objReader.GetAttribute("STOOL_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPee = objReader.GetAttribute("PEE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strHobby = objReader.GetAttribute("HOBBY").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strHobby_Other = objReader.GetAttribute("HOBBY_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSelfSolve = objReader.GetAttribute("SELFSOLVE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFeeling = objReader.GetAttribute("FEELING").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strJob = objReader.GetAttribute("JOB").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strInHospitalWorry = objReader.GetAttribute("INHOSPITALWORRY").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strInHospitalWorry_Other = objReader.GetAttribute("INHOSPITALWORRY_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFamilyForm = objReader.GetAttribute("FAMILYFORM").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFamilyForm_Other = objReader.GetAttribute("FAMILYFORM_OTHER").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strHealthNeed = objReader.GetAttribute("HEALTHNEED").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strKnowDisease = objReader.GetAttribute("KNOWDISEASE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSpecializedCheck = objReader.GetAttribute("SPECIALIZEDCHECK").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPipInstance = objReader.GetAttribute("PIPINSTANCE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strWoodInstance = objReader.GetAttribute("WOODINSTANCE").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strTendPlan = objReader.GetAttribute("TENDPLAN").Replace('��', '\'');

                                objclsInPatientEvaluate.m_strInHospitalDiagnoseXML = objReader.GetAttribute("INHOSPITALDIAGNOSEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strCaseHistoryXML = objReader.GetAttribute("CASEHISTORYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFamilyHistoryXML = objReader.GetAttribute("FAMILYHISTORYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strChiefComplainXML = objReader.GetAttribute("CHIEFCOMPLAINTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSensitiveHistory_OtherXML = objReader.GetAttribute("SENSITIVEHISTORY_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPhysique_OtherXML = objReader.GetAttribute("PHYSIQUE_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSkin_OtherXML = objReader.GetAttribute("SKIN_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLimbsactivity_OtherXML = objReader.GetAttribute("LIMBSACTIVITY_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStool_OtherXML = objReader.GetAttribute("STOOL_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strHobby_OtherXML = objReader.GetAttribute("HOBBY_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strInHospitalWorryXML = objReader.GetAttribute("INHOSPITALWORRY_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFamilyForm_OtherXML = objReader.GetAttribute("FAMILYFORM_OTHERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSpecializedCheckXML = objReader.GetAttribute("SPECIALIZEDCHECKXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPipInstanceXML = objReader.GetAttribute("PIPINSTANCEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strWoodInstanceXML = objReader.GetAttribute("WOODINSTANCEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strTendPlanXML = objReader.GetAttribute("TENDPLANXML").Replace('��', '\'');
                            }
                            break;
                    }
                }

                return objclsInPatientEvaluate;
            }
            return null;
        }

        public clsEMR_InPatientHealth_VO m_objGetLatestHealthEduRecord(string p_strXML, int p_intRows, string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_intRows > 0)//p_intRows==1
            {
                clsEMR_InPatientHealth_VO objclsHealthEdu = new clsEMR_InPatientHealth_VO();

                XmlTextReader objReader = new XmlTextReader(p_strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsHealthEdu.m_strInPatientID = p_strInPatientID;
                                objclsHealthEdu.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                                objclsHealthEdu.m_dtmOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE"));

                                objclsHealthEdu.m_strHEDU_First = objReader.GetAttribute("HEDU_FIRST").Replace('��', '\'');
                                objclsHealthEdu.m_strHEDU_Second = objReader.GetAttribute("HEDU_SECOND").Replace('��', '\'');
                                objclsHealthEdu.m_strHEDU_Three = objReader.GetAttribute("HEDU_THREE").Replace('��', '\'');
                                objclsHealthEdu.m_dtmWriteFormDate = DateTime.Parse(objReader.GetAttribute("WRITEFORMDATE"));
                            }
                            break;
                    }
                }
                return objclsHealthEdu;
            }
            return null;
        }

        public clsEMR_InPatientOutEvaluate_VO m_objGetLatestPatientOutRecord(string p_strXML, int p_intRows, string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_intRows > 0)//p_intRows==1
            {
                clsEMR_InPatientOutEvaluate_VO objclsPatientOut = new clsEMR_InPatientOutEvaluate_VO();

                XmlTextReader objReader = new XmlTextReader(p_strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsPatientOut.m_strInPatientID = p_strInPatientID;
                                objclsPatientOut.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                                objclsPatientOut.m_dtmOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE"));
                                objclsPatientOut.m_strNurseSign_ID = objReader.GetAttribute("NURSESIGN_ID");
                                objclsPatientOut.m_strChargeNurse_ID = objReader.GetAttribute("CHARGENURSE_ID");
                                objclsPatientOut.m_strInHospitalDays = objReader.GetAttribute("INPATIENTDAYS");

                                objclsPatientOut.m_strOutHospitalDiagnose = objReader.GetAttribute("OUTHOSPITALDIAGNOSE").Replace('��', '\'');
                                objclsPatientOut.m_strOutHospitalDiagnoseXML = objReader.GetAttribute("OUTHOSPITALDIAGNOSEXML").Replace('��', '\'');
                                objclsPatientOut.m_strLiveAbility = objReader.GetAttribute("LIVEABILITY").Replace('��', '\'');
                                objclsPatientOut.m_strDieteticCircs = objReader.GetAttribute("DIETETICCIRCS").Replace('��', '\'');
                                objclsPatientOut.m_strOutHospitalMode = objReader.GetAttribute("OUTHOSPITALMODE").Replace('��', '\'');
                                objclsPatientOut.m_strIsNurseSyndrome = objReader.GetAttribute("ISNURSESYNDROME").Replace('��', '\'');
                                objclsPatientOut.m_strNurseSyndrome = objReader.GetAttribute("NURSESYNDROME").Replace('��', '\'');
                                objclsPatientOut.m_strNurseSyndromeXML = objReader.GetAttribute("NURSESYNDROMEXML").Replace('��', '\'');
                                objclsPatientOut.m_strCommonlyCoach = objReader.GetAttribute("COMMONLYCOACH").Replace('��', '\'');
                                objclsPatientOut.m_strIsNurseIssue = objReader.GetAttribute("ISNURSEISSUE").Replace('��', '\'');
                                objclsPatientOut.m_strNurseIssue = objReader.GetAttribute("NURSEISSUE").Replace('��', '\'');
                                objclsPatientOut.m_strNurseIssueXML = objReader.GetAttribute("NURSEISSUEXML").Replace('��', '\'');
                                objclsPatientOut.m_strAdviceDrug = objReader.GetAttribute("ADVICEDRUG").Replace('��', '\'');
                                objclsPatientOut.m_strIsSpecialtiesCoach = objReader.GetAttribute("ISSPECIALTIESCOACH").Replace('��', '\'');
                                objclsPatientOut.m_strSpecialtiesCoach = objReader.GetAttribute("SPECIALTIESCOACH").Replace('��', '\'');
                                objclsPatientOut.m_strSpecialtiesCoachXML = objReader.GetAttribute("SPECIALTIESCOACHXML").Replace('��', '\'');
                            }
                            break;
                    }
                }
                return objclsPatientOut;
            }
            return null;
        }

        public long m_lngGetLatestRecord_All(string p_strInPatientID, string p_strInPatientDate, out clsEMR_InPatientEvaluate_All p_objclsInPatientEvaluate_All)
        {
            p_objclsInPatientEvaluate_All = null;

            string strXML = "";
            int intRows = 0;
            string strXMLEdu = "";
            int intRowsEdu = 0;
            string strXMLOut = "";
            int intRowsOut = 0;
            long lngResult = 0;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLatestRecord_All(p_strInPatientID, p_strInPatientDate, out strXML, out intRows, out strXMLEdu, out intRowsEdu, out strXMLOut, out intRowsOut);
                if (lngResult > 0)
                {
                    p_objclsInPatientEvaluate_All = new clsEMR_InPatientEvaluate_All();
                    p_objclsInPatientEvaluate_All.m_objclsInPatientEvaluate = m_objGetLatestRecord(strXML, intRows, p_strInPatientID, p_strInPatientDate);
                    p_objclsInPatientEvaluate_All.m_objclsInPatientHealth_VO = m_objGetLatestHealthEduRecord(strXMLEdu, intRowsEdu, p_strInPatientID, p_strInPatientDate);
                    p_objclsInPatientEvaluate_All.m_objInPatientOutEvaluate_VO = m_objGetLatestPatientOutRecord(strXMLOut, intRowsOut, p_strInPatientID, p_strInPatientDate);
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        /// ������Ϣ		
        public long m_lngSave(clsEMR_InPatientEvaluate p_objclsInPatientEvaluate, clsEMR_InPatientHealth_VO p_objclsInPatientHealth, clsEMR_InPatientOutEvaluate_VO p_objclsInPatientOutEvaluate, bool p_blnIsAddNew)
        {
            long lngResult = 0;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                if (p_blnIsAddNew == true)
                    lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngAddNew(m_strGetXML(p_objclsInPatientEvaluate), m_strGetEduXML(p_objclsInPatientHealth), m_strGetOutXML(p_objclsInPatientOutEvaluate));
                else
                    lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngModify(m_strGetXML(p_objclsInPatientEvaluate), m_strGetEduXML(p_objclsInPatientHealth), m_strGetOutXML(p_objclsInPatientOutEvaluate));
            }
            finally
            {
                //m_objServ.Dispose();  
            }
            return lngResult;
        }

        private string m_strGetXML(clsEMR_InPatientEvaluate p_objclsInPatientEvaluate)
        {
            if (p_objclsInPatientEvaluate == null)
                return "";
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsInPatientEvaluate.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsInPatientEvaluate.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objclsInPatientEvaluate.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsInPatientEvaluate.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objclsInPatientEvaluate.m_strCreateUserID);
            m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsInPatientEvaluate.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objclsInPatientEvaluate.m_strModifyUserID);
            m_objXmlWriter.WriteAttributeString("NURSEID", p_objclsInPatientEvaluate.m_strNurseID);

            m_objXmlWriter.WriteAttributeString("PAYMENTMETHOD", p_objclsInPatientEvaluate.m_strPaymentMethod.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("EDUCATIONDEGREE", p_objclsInPatientEvaluate.m_strEducationDegree.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALMETHOD", p_objclsInPatientEvaluate.m_strInHospitalMethod.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALDIAGNOSE", p_objclsInPatientEvaluate.m_strInHospitalDiagnose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CASEHISTORY", p_objclsInPatientEvaluate.m_strCaseHistory.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FAMILYHISTORY", p_objclsInPatientEvaluate.m_strFamilyHistory.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIEFCOMPLAINT", p_objclsInPatientEvaluate.m_strChiefComplain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSITIVEHISTORY", p_objclsInPatientEvaluate.m_strSensitiveHistory.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSITIVEHISTORY_OTHER", p_objclsInPatientEvaluate.m_strSensitiveHistory_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BODYTEMPERATURE", p_objclsInPatientEvaluate.m_strBodyTemperature.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PULSE", p_objclsInPatientEvaluate.m_strPulse.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HEARTRHYTHM", p_objclsInPatientEvaluate.m_strHeartPhythm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BP_SHRINK", p_objclsInPatientEvaluate.m_strBP_Shrink.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BP_EXTEND", p_objclsInPatientEvaluate.m_strBP_Extend.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AVOIRDUPOIS", p_objclsInPatientEvaluate.m_strAvoirdupois.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENGAO", p_objclsInPatientEvaluate.m_strShengao.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CONSCIOUSNESS", p_objclsInPatientEvaluate.m_strConsciousness.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("COMPLEXION", p_objclsInPatientEvaluate.m_strComplexion.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHYSIQUE", p_objclsInPatientEvaluate.m_strPhysique.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHYSIQUE_OTHER", p_objclsInPatientEvaluate.m_strPhysique_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("EMOTION", p_objclsInPatientEvaluate.m_strEmotion.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKIN", p_objclsInPatientEvaluate.m_strSkin.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKIN_OTHER", p_objclsInPatientEvaluate.m_strSkin_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LIMBSACTIVITY", p_objclsInPatientEvaluate.m_strLimbsactivity.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LIMBSACTIVITY_OTHER", p_objclsInPatientEvaluate.m_strLimbsactivity_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BITESUP", p_objclsInPatientEvaluate.m_strBiteSup.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("APPETITE", p_objclsInPatientEvaluate.m_strAppetite.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEP", p_objclsInPatientEvaluate.m_strSleep.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOOL", p_objclsInPatientEvaluate.m_strStool.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASTRICTION", p_objclsInPatientEvaluate.m_strAstriction.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIARRHEA", p_objclsInPatientEvaluate.m_strDiarrhea.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOOL_OTHER", p_objclsInPatientEvaluate.m_strStool_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEE", p_objclsInPatientEvaluate.m_strPee.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HOBBY", p_objclsInPatientEvaluate.m_strHobby.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HOBBY_OTHER", p_objclsInPatientEvaluate.m_strHobby_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SELFSOLVE", p_objclsInPatientEvaluate.m_strSelfSolve.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FEELING", p_objclsInPatientEvaluate.m_strFeeling.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JOB", p_objclsInPatientEvaluate.m_strJob.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALWORRY", p_objclsInPatientEvaluate.m_strInHospitalWorry.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALWORRY_OTHER", p_objclsInPatientEvaluate.m_strInHospitalWorry_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FAMILYFORM", p_objclsInPatientEvaluate.m_strFamilyForm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FAMILYFORM_OTHER", p_objclsInPatientEvaluate.m_strFamilyForm_Other.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HEALTHNEED", p_objclsInPatientEvaluate.m_strHealthNeed.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KNOWDISEASE", p_objclsInPatientEvaluate.m_strKnowDisease.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALIZEDCHECK", p_objclsInPatientEvaluate.m_strSpecializedCheck.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PIPINSTANCE", p_objclsInPatientEvaluate.m_strPipInstance.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WOODINSTANCE", p_objclsInPatientEvaluate.m_strWoodInstance.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TENDPLAN", p_objclsInPatientEvaluate.m_strTendPlan.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("INHOSPITALDIAGNOSEXML", p_objclsInPatientEvaluate.m_strInHospitalDiagnoseXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CASEHISTORYXML", p_objclsInPatientEvaluate.m_strCaseHistoryXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FAMILYHISTORYXML", p_objclsInPatientEvaluate.m_strFamilyHistoryXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIEFCOMPLAINTXML", p_objclsInPatientEvaluate.m_strChiefComplainXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSITIVEHISTORY_OTHERXML", p_objclsInPatientEvaluate.m_strSensitiveHistory_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHYSIQUE_OTHERXML", p_objclsInPatientEvaluate.m_strPhysique_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKIN_OTHERXML", p_objclsInPatientEvaluate.m_strSkin_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LIMBSACTIVITY_OTHERXML", p_objclsInPatientEvaluate.m_strLimbsactivity_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOOL_OTHERXML", p_objclsInPatientEvaluate.m_strStool_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HOBBY_OTHERXML", p_objclsInPatientEvaluate.m_strHobby_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALWORRY_OTHERXML", p_objclsInPatientEvaluate.m_strInHospitalWorryXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FAMILYFORM_OTHERXML", p_objclsInPatientEvaluate.m_strFamilyForm_OtherXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALIZEDCHECKXML", p_objclsInPatientEvaluate.m_strSpecializedCheckXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PIPINSTANCEXML", p_objclsInPatientEvaluate.m_strPipInstanceXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WOODINSTANCEXML", p_objclsInPatientEvaluate.m_strWoodInstanceXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TENDPLANXML", p_objclsInPatientEvaluate.m_strTendPlanXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        private string m_strGetEduXML(clsEMR_InPatientHealth_VO p_objclsInPatientHealth)
        {
            if (p_objclsInPatientHealth == null)
                return "";
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsInPatientHealth.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsInPatientHealth.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objclsInPatientHealth.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsInPatientHealth.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objclsInPatientHealth.m_strModifyUserID);
            m_objXmlWriter.WriteAttributeString("WRITEFORMDATE", p_objclsInPatientHealth.m_dtmWriteFormDate.ToString("yyyy-MM-dd HH:mm:ss"));

            m_objXmlWriter.WriteAttributeString("HEDU_FIRST", p_objclsInPatientHealth.m_strHEDU_First.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HEDU_SECOND", p_objclsInPatientHealth.m_strHEDU_Second.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HEDU_THREE", p_objclsInPatientHealth.m_strHEDU_Three.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        private string m_strGetOutXML(clsEMR_InPatientOutEvaluate_VO p_objclsInPatientOutEvaluate)
        {
            if (p_objclsInPatientOutEvaluate == null)
                return "";
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsInPatientOutEvaluate.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsInPatientOutEvaluate.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objclsInPatientOutEvaluate.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsInPatientOutEvaluate.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objclsInPatientOutEvaluate.m_strModifyUserID);
            m_objXmlWriter.WriteAttributeString("NURSESIGN_ID", p_objclsInPatientOutEvaluate.m_strNurseSign_ID);
            m_objXmlWriter.WriteAttributeString("CHARGENURSE_ID", p_objclsInPatientOutEvaluate.m_strChargeNurse_ID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDAYS", p_objclsInPatientOutEvaluate.m_strInHospitalDays);

            m_objXmlWriter.WriteAttributeString("OUTHOSPITALDIAGNOSE", p_objclsInPatientOutEvaluate.m_strOutHospitalDiagnose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OUTHOSPITALDIAGNOSEXML", p_objclsInPatientOutEvaluate.m_strOutHospitalDiagnoseXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LIVEABILITY", p_objclsInPatientOutEvaluate.m_strLiveAbility.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIETETICCIRCS", p_objclsInPatientOutEvaluate.m_strDieteticCircs.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OUTHOSPITALMODE", p_objclsInPatientOutEvaluate.m_strOutHospitalMode.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ISNURSESYNDROME", p_objclsInPatientOutEvaluate.m_strIsNurseSyndrome.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSESYNDROME", p_objclsInPatientOutEvaluate.m_strNurseSyndrome.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSESYNDROMEXML", p_objclsInPatientOutEvaluate.m_strNurseSyndromeXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ISNURSEISSUE", p_objclsInPatientOutEvaluate.m_strIsNurseIssue.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSEISSUE", p_objclsInPatientOutEvaluate.m_strNurseIssue.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSEISSUEXML", p_objclsInPatientOutEvaluate.m_strNurseIssueXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("COMMONLYCOACH", p_objclsInPatientOutEvaluate.m_strCommonlyCoach.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ADVICEDRUG", p_objclsInPatientOutEvaluate.m_strAdviceDrug.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ISSPECIALTIESCOACH", p_objclsInPatientOutEvaluate.m_strIsSpecialtiesCoach.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALTIESCOACH", p_objclsInPatientOutEvaluate.m_strSpecialtiesCoach.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALTIESCOACHXML", p_objclsInPatientOutEvaluate.m_strSpecialtiesCoachXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        /// <summary>
        /// //��ѯ��һ�δ�ӡʱ��
        /// </summary>		
        public long m_strGetFirstPrintDate(string p_strInPatientID, string p_strInPatientDate, out string p_strFirstPrintDate)
        {
            long lngResult = 0;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsEMR_InPatientEvaluateServ_m_lngGetFirstPrintDate(p_strInPatientID, p_strInPatientDate, out p_strFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        /// <summary>
        /// ���µ�ǰ���˵���סԺ���״δ�ӡʱ�䣨���ڴ˵���һ����Ժ���ڽ���һ��OpenDate���ʲ���OpenDate��������������һ����¼��
        /// </summary>		
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID, string p_strInPatientDate)
        {//���µ�һ�δ�ӡʱ��		
            long lngResult = 0;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsEMR_InPatientEvaluateServ_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        public long m_lngDelete(string p_strDeActivedOperatorID, string p_strInPatientID, string p_strInPatientDate)
        {
            long res = -1;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                res = (new weCare.Proxy.ProxyEmr05()).Service.clsEMR_InPatientEvaluateServ_m_lngDelete("T_OPR_EMR_INPATIENTEVALUATE", p_strDeActivedOperatorID, p_strInPatientID, p_strInPatientDate);
                if (res > 0)
                {
                    res = (new weCare.Proxy.ProxyEmr05()).Service.clsEMR_InPatientEvaluateServ_m_lngDelete("T_OPR_EMR_HEALTHEDU", p_strDeActivedOperatorID, p_strInPatientID, p_strInPatientDate);
                    res = (new weCare.Proxy.ProxyEmr05()).Service.clsEMR_InPatientEvaluateServ_m_lngDelete("T_OPR_EMR_INPATIENTOUTEVALUATE", p_strDeActivedOperatorID, p_strInPatientID, p_strInPatientDate);
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return res;
        }

        public long m_lngGetLatestDeleteRecord_All(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsEMR_InPatientEvaluate_All p_objclsInPatientEvaluate_All)
        {
            p_objclsInPatientEvaluate_All = null;

            string strXML = "";
            int intRows = 0;
            string strXMLEdu = "";
            int intRowsEdu = 0;
            string strXMLOut = "";
            int intRowsOut = 0;
            long lngResult = 0;

            //clsEMR_InPatientEvaluateServ m_objServ =
            //    (clsEMR_InPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_InPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLatestDeleteRecord_All(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out strXML, out intRows, out strXMLEdu, out intRowsEdu, out strXMLOut, out intRowsOut);
                if (lngResult > 0)
                {
                    p_objclsInPatientEvaluate_All = new clsEMR_InPatientEvaluate_All();
                    p_objclsInPatientEvaluate_All.m_objclsInPatientEvaluate = m_objGetLatestRecord(strXML, intRows, p_strInPatientID, p_strInPatientDate);
                    p_objclsInPatientEvaluate_All.m_objclsInPatientHealth_VO = m_objGetLatestHealthEduRecord(strXMLEdu, intRowsOut, p_strInPatientID, p_strInPatientDate);
                    p_objclsInPatientEvaluate_All.m_objInPatientOutEvaluate_VO = m_objGetLatestPatientOutRecord(strXMLOut, intRowsOut, p_strInPatientID, p_strInPatientDate);
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
    }
}
