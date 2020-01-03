using System;
using System.Xml;
using System.IO;
using System.Text;
//using com.digitalwave.InHospitalMainRecord_XJ;
using com.digitalwave.InHospitalMainRecord_XJ;
using iCareData;
using System.Data;
using com.digitalwave.PatientManagerService;
namespace iCare
{
    /// <summary>
    /// סԺ������ҳ---�½� 
    /// </summary>
    public class clsInHospitalMainRecordDomain_XJ
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
        //private com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ;
        public clsInHospitalMainRecordDomain_XJ()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objServ=new com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ();
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);
        }

        /// <summary>
        /// //��ѯ��һ�δ�ӡʱ��
        /// </summary>		
        public long m_strGetFirstPrintDate(string p_strInPatientID, string p_strInPatientDate,/*string p_strOpenDate,*/out string p_strFirstPrintDate)
        {
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngGetFirstPrintDate(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate,/*p_strOpenDate,*/out p_strFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ���µ�ǰ���˵���סԺ���״δ�ӡʱ�䣨���ڴ˵���һ����Ժ���ڽ���һ��OpenDate���ʲ���OpenDate��������������һ����¼��
        /// </summary>		
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID, string p_strInPatientDate/*,string p_strOpenDate*/)
        {//���µ�һ�δ�ӡʱ��		
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long lngRes = 0;
            try
            {
                lngRes = m_objServ.m_lngUpdateFirstPrintDate(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate/*,p_strOpenDate*/);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngDoSave(clsInHospitalMainRecord_Collection p_objCollection, bool p_blnIsAddNew)
        {
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long lngRes = 0;
            try
            {
                if (p_blnIsAddNew)
                {
                    lngRes = m_objServ.m_lngAddNew(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_objCollection);
                }
                else
                {
                    lngRes = m_objServ.m_lngModify(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_objCollection);
                }
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
            }
            return lngRes;
        }

        public long m_lngDoSave(clsInHospitalMainRecord_Main p_objMain, clsInHospitalMainRecord_Content p_objContent, clsInHospitalMainRecord_OtherDiagnosis[] p_objOtherDiagnosisArr, clsInHospitalMainRecord_Operation[] p_objOperationArr, clsInHospitalMainRecord_Baby[] p_objBabyArr, clsInHospitalMainRecord_Chemotherapy[] p_objChemotherapyArr, bool p_bolIfAddNew)
        {
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long lngRes = 0;
            try
            {
                string m_strMainXML = m_strGetMainXML(p_objMain);
                string m_strContentXML = m_strGetContentXML(p_objContent);
                string[] m_strOtherDiagnosisXMLArr = m_strGetOtherDiagnosisXMLArr(p_objOtherDiagnosisArr);
                string[] m_strOperationXMLArr = m_strGetOperationXMLArr(p_objOperationArr);
                string[] m_strBabyXMLArr = m_strGetBabyXMLArr(p_objBabyArr);
                string[] m_strChemotherapyXMLArr = m_strGetChemotherapyXMLArr(p_objChemotherapyArr);

                if (p_bolIfAddNew)//����
                    lngRes = m_objServ.m_lngAddNew(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, m_strMainXML, m_strContentXML, m_strOtherDiagnosisXMLArr, m_strOperationXMLArr, m_strBabyXMLArr, m_strChemotherapyXMLArr);
                else//�޸�
                    lngRes = m_objServ.m_lngModify(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_objMain.m_strInPatientID, p_objMain.m_strInPatientDate, p_objMain.m_strOpenDate, p_objContent.m_strLastModifyUserID,
                        m_strMainXML, m_strContentXML, m_strOtherDiagnosisXMLArr, m_strOperationXMLArr, m_strBabyXMLArr, m_strChemotherapyXMLArr);
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;

        }

        #region ����XML
        /// <summary>
        /// ����XML
        /// </summary>
        /// <param name="p_objMain"></param>
        /// <returns></returns>
        private string m_strGetMainXML(clsInHospitalMainRecord_Main p_objMain)
        {

            if (p_objMain == null)
                return null;
            m_objXmlMemStream.SetLength(0);
            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Main");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objMain.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objMain.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objMain.m_strOpenDate);
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objMain.m_strCreateUserID);
            m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objMain.m_strDeActivedDate);
            m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objMain.m_strDeActivedOperatorID);
            m_objXmlWriter.WriteAttributeString("STATUS", p_objMain.m_strStatus);
            m_objXmlWriter.WriteAttributeString("DIAGNOSISXML", p_objMain.m_strDiagnosisXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALDIAGNOSISXML", p_objMain.m_strInHospitalDiagnosisXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MAINDIAGNOSISXML", p_objMain.m_strMainDiagnosisXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ICD_10OFMAINXML", p_objMain.m_strICD_10OfMainXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INFECTIONDIAGNOSISXML", p_objMain.m_strInfectionDiagnosisXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ICD_10OFINFECTIONXML", p_objMain.m_strICD_10OfInfectionXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATHOLOGYDIAGNOSISXML", p_objMain.m_strPathologyDiagnosisXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SCACHESOURCEXML", p_objMain.m_strScacheSourceXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSITIVEXML", p_objMain.m_strSensitiveXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HBSAGXML", p_objMain.m_strHbsAgXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HCV_ABXML", p_objMain.m_strHCV_AbXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HIV_ABXML", p_objMain.m_strHIV_AbXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDWITHOUTHOSPITALXML", p_objMain.m_strAccordWithOutHospitalXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDINWITHOUTXML", p_objMain.m_strAccordInWithOutXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDBFOPRWITHAFXML", p_objMain.m_strAccordBeforeOperationWithAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDCLINICWITHPATHOLOGYXML", p_objMain.m_strAccordClinicWithPathologyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDRADIATEWITHPATHOLOGYXML", p_objMain.m_strAccordRadiateWithPathologyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SALVETIMESXML", p_objMain.m_strSalveTimesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SALVESUCCESSXML", p_objMain.m_strSalveSuccessXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASEGYXML", p_objMain.m_strOriginalDiseaseGyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASETIMESXML", p_objMain.m_strOriginalDiseaseTimesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASEDAYSXML", p_objMain.m_strOriginalDiseaseDaysXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHGYXML", p_objMain.m_strLymphGyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHTIMESXML", p_objMain.m_strLymphTimesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHDAYSXML", p_objMain.m_strLymphDaysXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISGYXML", p_objMain.m_strMetastasisGyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISTIMESXML", p_objMain.m_strMetastasisTimesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISDAYSXML", p_objMain.m_strMetastasisDaysXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOTALAMTXML", p_objMain.m_strTotalAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BEDAMTXML", p_objMain.m_strBedAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSEAMTXML", p_objMain.m_strNurseAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WMAMTXML", p_objMain.m_strWMAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CMFINISHEDAMTXML", p_objMain.m_strCMFinishedAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CMSEMIFINISHEDAMTXML", p_objMain.m_strCMSemiFinishedAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RADIATIONAMTXML", p_objMain.m_strRadiationAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSAYAMTXML", p_objMain.m_strAssayAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("O2AMTXML", p_objMain.m_strO2AmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODAMTXML", p_objMain.m_strBloodAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TREATMENTAMTXML", p_objMain.m_strTreatmentAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONAMTXML", p_objMain.m_strOperationAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DELIVERYCHILDAMTXML", p_objMain.m_strDeliveryChildAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKAMTXML", p_objMain.m_strCheckAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANAETHESIAAMTXML", p_objMain.m_strAnaethesiaAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BABYAMTXML", p_objMain.m_strBabyAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCOMPANYAMTXML", p_objMain.m_strAccompanyAmtXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERAMT1XML", p_objMain.m_strOtherAmt1XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERAMT2XML", p_objMain.m_strOtherAmt2XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERAMT3XML", p_objMain.m_strOtherAmt3XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW_WEEKXML", p_objMain.m_strFollow_WeekXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW_MONTHXML", p_objMain.m_strFollow_MonthXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW_YEARXML", p_objMain.m_strFollow_YearXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODTYPEXML", p_objMain.m_strBloodTypeXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RBCXML", p_objMain.m_strRBCXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PLTXML", p_objMain.m_strPLTXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PLASMXML", p_objMain.m_strPlasmXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WHOLEBLOODXML", p_objMain.m_strWholeBloodXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERBLOODXML", p_objMain.m_strOtherBloodXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CONSULTATIONXML", p_objMain.m_strConsultationXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LONGDISTANCTCONSULTATIONXML", p_objMain.m_strLongDistanctConsultationXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOPLEVELXML", p_objMain.m_strTOPLevelXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSELEVELIXML", p_objMain.m_strNurseLevelIXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSELEVELIIXML", p_objMain.m_strNurseLevelIIXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSELEVELIIIXML", p_objMain.m_strNurseLevelIIIXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ICUXML", p_objMain.m_strICUXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALNURSEXML", p_objMain.m_strSpecialNurseXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIAGNOSISZHONGXML", p_objMain.m_strDiagnosiszhongXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("INSURANCENUMXML", p_objMain.m_strInsuranceNumXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MODEOFPAYMENTXML", p_objMain.m_strModeOfPaymentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATIENTHISTORYNOXML", p_objMain.m_strPatientHistoryNOXML.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region ���ӱ�XML
        /// <summary>
        /// ���ӱ�XML
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        private string m_strGetContentXML(clsInHospitalMainRecord_Content p_objContent)
        {
            if (p_objContent == null)
                return null;
            m_objXmlMemStream.SetLength(0);
            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Content");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objContent.m_strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objContent.m_strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objContent.m_strOpenDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objContent.m_strLastModifyDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objContent.m_strLastModifyUserID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objContent.m_strDeActivedDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objContent.m_strDeActivedOperatorID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", p_objContent.m_strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIAGNOSIS", p_objContent.m_strDiagnosis.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALDIAGNOSIS", p_objContent.m_strInHospitalDiagnosis.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOCTOR", p_objContent.m_strDoctor.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CONFIRMDIAGNOSISDATE", p_objContent.m_strConfirmDiagnosisDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CONDICTIONWHENIN", p_objContent.m_strCondictionWhenIn.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MAINDIAGNOSIS", p_objContent.m_strMainDiagnosis.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MAINCONDITIONSEQ", p_objContent.m_strMainConditionSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ICD_10OFMAIN", p_objContent.m_strICD_10OfMain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INFECTIONDIAGNOSIS", p_objContent.m_strInfectionDiagnosis.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INFECTIONCONDICTIONSEQ", p_objContent.m_strInfectionCondictionSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ICD_10OFINFECTION", p_objContent.m_strICD_10OfInfection.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATHOLOGYDIAGNOSIS", p_objContent.m_strPathologyDiagnosis.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SCACHESOURCE", p_objContent.m_strScacheSource.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSITIVE", p_objContent.m_strSensitive.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HBSAG", p_objContent.m_strHbsAg.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HCV_AB", p_objContent.m_strHCV_Ab.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HIV_AB", p_objContent.m_strHIV_Ab.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDWITHOUTHOSPITAL", p_objContent.m_strAccordWithOutHospital.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDINWITHOUT", p_objContent.m_strAccordInWithOut.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDBEFOREOPERATIONWITHAFTER", p_objContent.m_strAccordBeforeOperationWithAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDCLINICWITHPATHOLOGY", p_objContent.m_strAccordClinicWithPathology.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCORDRADIATEWITHPATHOLOGY", p_objContent.m_strAccordRadiateWithPathology.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SALVETIMES", p_objContent.m_strSalveTimes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SALVESUCCESS", p_objContent.m_strSalveSuccess.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIRECTORDT", p_objContent.m_strDirectorDt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SUBDIRECTORDT", p_objContent.m_strSubDirectorDt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DT", p_objContent.m_strDt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALDT", p_objContent.m_strInHospitalDt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ATTENDINFORADVANCESSTUDYDT", p_objContent.m_strAttendInForAdvancesStudyDt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GRADUATESTUDENTINTERN", p_objContent.m_strGraduateStudentIntern.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INTERN", p_objContent.m_strIntern.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CODER", p_objContent.m_strCoder.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUALITY", p_objContent.m_strQuality.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QCDT", p_objContent.m_strQCDt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QCNURSE", p_objContent.m_strQCNurse.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QCTIME", p_objContent.m_strQCTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RTMODESEQ", p_objContent.m_strRTModeSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RTRULESEQ", p_objContent.m_strRTRuleSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RTCO", p_objContent.m_strRTCo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RTACCELERATOR", p_objContent.m_strRTAccelerator.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RTX_RAY", p_objContent.m_strRTX_Ray.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RTLACUNA", p_objContent.m_strRTLacuna.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASESEQ", p_objContent.m_strOriginalDiseaseSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASEGY", p_objContent.m_strOriginalDiseaseGy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASETIMES", p_objContent.m_strOriginalDiseaseTimes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASEDAYS", p_objContent.m_strOriginalDiseaseDays.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASEBEGINDATE", p_objContent.m_strOriginalDiseaseBeginDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORIGINALDISEASEENDDATE", p_objContent.m_strOriginalDiseaseEndDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHSEQ", p_objContent.m_strLymphSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHGY", p_objContent.m_strLymphGy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHTIMES", p_objContent.m_strLymphTimes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHDAYS", p_objContent.m_strLymphDays.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHBEGINDATE", p_objContent.m_strLymphBeginDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LYMPHENDDATE", p_objContent.m_strLymphEndDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISGY", p_objContent.m_strMetastasisGy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISTIMES", p_objContent.m_strMetastasisTimes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISDAYS", p_objContent.m_strMetastasisDays.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISBEGINDATE", p_objContent.m_strMetastasisBeginDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("METASTASISENDDATE", p_objContent.m_strMetastasisEndDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYMODESEQ", p_objContent.m_strChemotherapyModeSeq.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYWHOLEBODY", p_objContent.m_strChemotherapyWholeBody.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYLOCAL", p_objContent.m_strChemotherapyLocal.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYINTUBATE", p_objContent.m_strChemotherapyIntubate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYTHORAX", p_objContent.m_strChemotherapyThorax.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYABDOMEN", p_objContent.m_strChemotherapyAbdomen.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYSPINAL", p_objContent.m_strChemotherapySpinal.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYOTHERTRY", p_objContent.m_strChemotherapyOtherTry.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYOTHER", p_objContent.m_strChemotherapyOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOTALAMT", p_objContent.m_strTotalAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BEDAMT", p_objContent.m_strBedAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSEAMT", p_objContent.m_strNurseAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WMAMT", p_objContent.m_strWMAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CMFINISHEDAMT", p_objContent.m_strCMFinishedAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CMSEMIFINISHEDAMT", p_objContent.m_strCMSemiFinishedAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RADIATIONAMT", p_objContent.m_strRadiationAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSAYAMT", p_objContent.m_strAssayAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("O2AMT", p_objContent.m_strO2Amt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODAMT", p_objContent.m_strBloodAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TREATMENTAMT", p_objContent.m_strTreatmentAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONAMT", p_objContent.m_strOperationAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DELIVERYCHILDAMT", p_objContent.m_strDeliveryChildAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKAMT", p_objContent.m_strCheckAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANAETHESIAAMT", p_objContent.m_strAnaethesiaAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BABYAMT", p_objContent.m_strBabyAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACCOMPANYAMT", p_objContent.m_strAccompanyAmt.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERAMT1", p_objContent.m_strOtherAmt1.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERAMT2", p_objContent.m_strOtherAmt2.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERAMT3", p_objContent.m_strOtherAmt3.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CORPSECHECK", p_objContent.m_strCorpseCheck.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FIRSTCASE", p_objContent.m_strFirstCase.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW", p_objContent.m_strFollow.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW_WEEK", p_objContent.m_strFollow_Week.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW_MONTH", p_objContent.m_strFollow_Month.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLLOW_YEAR", p_objContent.m_strFollow_Year.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MODELCASE", p_objContent.m_strModelCase.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODTYPE", p_objContent.m_strBloodType.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODRH", p_objContent.m_strBloodRh.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODTRANSACTOIN", p_objContent.m_strBloodTransActoin.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RBC", p_objContent.m_strRBC.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PLT", p_objContent.m_strPLT.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PLASM", p_objContent.m_strPlasm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WHOLEBLOOD", p_objContent.m_strWholeBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHERBLOOD", p_objContent.m_strOtherBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CONSULTATION", p_objContent.m_strConsultation.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LONGDISTANCTCONSULTATION", p_objContent.m_strLongDistanctConsultation.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOPLEVEL", p_objContent.m_strTOPLevel.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSELEVELI", p_objContent.m_strNurseLevelI.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSELEVELII", p_objContent.m_strNurseLevelII.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSELEVELIII", p_objContent.m_strNurseLevelIII.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ICU", p_objContent.m_strICU.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SPECIALNURSE", p_objContent.m_strSpecialNurse.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIAGNOSISZHONG", p_objContent.m_strDiagnosiszhong.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("INSURANCENUM", p_objContent.m_strInsuranceNum.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MODEOFPAYMENT", p_objContent.m_strModeOfPayment.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATIENTHISTORYNO", p_objContent.m_strPatientHistoryNO.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OUTPATIENTDATE", p_objContent.m_strOutPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIRTHPLACE", p_objContent.m_strBirthPlace.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region ��������ӱ�
        /// <summary>
        /// OtherDiagnosis ��������ӱ�
        /// </summary>
        /// <param name="p_objOtherDiagnosisArr"></param>
        /// <returns></returns>
        public string[] m_strGetOtherDiagnosisXMLArr(clsInHospitalMainRecord_OtherDiagnosis[] p_objOtherDiagnosisArr)
        {
            if (p_objOtherDiagnosisArr == null || p_objOtherDiagnosisArr.Length <= 0)
                return null;

            string[] m_strXMLArr = new string[p_objOtherDiagnosisArr.Length];
            for (int i1 = 0; i1 < p_objOtherDiagnosisArr.Length; i1++)
            {
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("OtherDiagnosis");
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOtherDiagnosisArr[i1].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOtherDiagnosisArr[i1].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objOtherDiagnosisArr[i1].m_strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objOtherDiagnosisArr[i1].m_strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objOtherDiagnosisArr[i1].m_strLastModifyUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objOtherDiagnosisArr[i1].m_strDeActivedDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objOtherDiagnosisArr[i1].m_strDeActivedOperatorID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", p_objOtherDiagnosisArr[i1].m_strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SEQID", p_objOtherDiagnosisArr[i1].m_strSeqID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DIAGNOSISDESC", p_objOtherDiagnosisArr[i1].m_strDiagnosisDesc.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CONDITIONSEQ", p_objOtherDiagnosisArr[i1].m_strConditionSeq.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ICD10", p_objOtherDiagnosisArr[i1].m_strICD10.Replace('\'', '��'));
                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                m_strXMLArr[i1] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return m_strXMLArr;
        }

        #endregion

        #region ������Ϣ�ӱ�
        /// <summary>
        /// ������Ϣ�ӱ�
        /// </summary>
        /// <param name="p_objOperationArr"></param>
        /// <returns></returns>
        public string[] m_strGetOperationXMLArr(clsInHospitalMainRecord_Operation[] p_objOperationArr)
        {
            if (p_objOperationArr == null || p_objOperationArr.Length <= 0)
                return null;

            string[] m_strXMLArr = new string[p_objOperationArr.Length];
            for (int i1 = 0; i1 < p_objOperationArr.Length; i1++)
            {
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("Operation");
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOperationArr[i1].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOperationArr[i1].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objOperationArr[i1].m_strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objOperationArr[i1].m_strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objOperationArr[i1].m_strLastModifyUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objOperationArr[i1].m_strDeActivedDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objOperationArr[i1].m_strDeActivedOperatorID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", p_objOperationArr[i1].m_strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SEQID", p_objOperationArr[i1].m_strSeqID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONID", p_objOperationArr[i1].m_strOperationID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONDATE", p_objOperationArr[i1].m_strOperationDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONNAME", p_objOperationArr[i1].m_strOperationName.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATOR", p_objOperationArr[i1].m_strOperator.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ASSISTANT1", p_objOperationArr[i1].m_strAssistant1.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ASSISTANT2", p_objOperationArr[i1].m_strAssistant2.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("AANAESTHESIAMODEID", p_objOperationArr[i1].m_strAanaesthesiaModeID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CUTLEVEL", p_objOperationArr[i1].m_strCutLevel.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHETIST", p_objOperationArr[i1].m_strAnaesthetist.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("OPERATIONAANAESTHESIAMODENAME", p_objOperationArr[i1].m_strAanaesthesiaModeName.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONANAESTHETISTNAME", p_objOperationArr[i1].m_strAnaesthetistName.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                m_strXMLArr[i1] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return m_strXMLArr;
        }
        #endregion

        #region Ӥ���ӱ�
        /// <summary>
        /// Ӥ���ӱ�
        /// </summary>
        /// <param name="p_objBabyArr"></param>
        /// <returns></returns>
        public string[] m_strGetBabyXMLArr(clsInHospitalMainRecord_Baby[] p_objBabyArr)
        {
            if (p_objBabyArr == null || p_objBabyArr.Length <= 0)
                return null;

            string[] m_strXMLArr = new string[p_objBabyArr.Length];
            for (int i1 = 0; i1 < p_objBabyArr.Length; i1++)
            {
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("Baby");
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objBabyArr[i1].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objBabyArr[i1].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objBabyArr[i1].m_strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objBabyArr[i1].m_strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objBabyArr[i1].m_strLastModifyUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objBabyArr[i1].m_strDeActivedDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objBabyArr[i1].m_strDeActivedOperatorID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", p_objBabyArr[i1].m_strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SEQID", p_objBabyArr[i1].m_strSeqID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("MALE", p_objBabyArr[i1].m_strMale.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FEMALE", p_objBabyArr[i1].m_strFemale.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LIVEBORN", p_objBabyArr[i1].m_strLiveBorn.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DIEBORN", p_objBabyArr[i1].m_strDieBorn.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DIENOTBORN", p_objBabyArr[i1].m_strDieNotBorn.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("WEIGHT", p_objBabyArr[i1].m_strWeight.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DIE", p_objBabyArr[i1].m_strDie.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CHANGEDEPARTMENT", p_objBabyArr[i1].m_strChangeDepartment.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTHOSPITAL", p_objBabyArr[i1].m_strOutHospital.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("NATURALCONDICTION", p_objBabyArr[i1].m_strNaturalCondiction.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SUFFOCATE1", p_objBabyArr[i1].m_strSuffocate1.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SUFFOCATE2", p_objBabyArr[i1].m_strSuffocate2.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INFECTIONTIMES", p_objBabyArr[i1].m_strInfectionTimes.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INFECTIONNAME", p_objBabyArr[i1].m_strInfectionName.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ICD10", p_objBabyArr[i1].m_strICD10.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SALVETIMES", p_objBabyArr[i1].m_strSalveTimes.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SALVESUCCESSTIMES", p_objBabyArr[i1].m_strSalveSuccessTimes.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                m_strXMLArr[i1] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return m_strXMLArr;
        }

        #endregion

        #region �����ӱ�
        /// <summary>
        /// �����ӱ�
        /// </summary>
        /// <param name="p_objChemotherapyArr"></param>
        /// <returns></returns>
        public string[] m_strGetChemotherapyXMLArr(clsInHospitalMainRecord_Chemotherapy[] p_objChemotherapyArr)
        {
            if (p_objChemotherapyArr == null || p_objChemotherapyArr.Length <= 0)
                return null;

            string[] m_strXMLArr = new string[p_objChemotherapyArr.Length];
            for (int i1 = 0; i1 < p_objChemotherapyArr.Length; i1++)
            {
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("Chemotherapy");
                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objChemotherapyArr[i1].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objChemotherapyArr[i1].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objChemotherapyArr[i1].m_strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objChemotherapyArr[i1].m_strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objChemotherapyArr[i1].m_strLastModifyUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objChemotherapyArr[i1].m_strDeActivedDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objChemotherapyArr[i1].m_strDeActivedOperatorID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", p_objChemotherapyArr[i1].m_strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SEQID", p_objChemotherapyArr[i1].m_strSeqID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CHEMOTHERAPYDATE", p_objChemotherapyArr[i1].m_strChemotherapyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("MEDICINENAME", p_objChemotherapyArr[i1].m_strMedicineName.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PERIOD", p_objChemotherapyArr[i1].m_strPeriod.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIELD_CR", p_objChemotherapyArr[i1].m_strField_CR.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIELD_PR", p_objChemotherapyArr[i1].m_strField_PR.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIELD_MR", p_objChemotherapyArr[i1].m_strField_MR.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIELD_S", p_objChemotherapyArr[i1].m_strField_S.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIELD_P", p_objChemotherapyArr[i1].m_strField_P.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIELD_NA", p_objChemotherapyArr[i1].m_strField_NA.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                m_strXMLArr[i1] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return m_strXMLArr;
        }
        #endregion

        #region ��øô�סԺ��סԺ������ҳ������ʱ��
        /// <summary>
        /// ��øô�סԺ��סԺ������ҳ������ʱ��
        /// ����Ϊû�����ɹ�
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public long m_lngGetOpenDateInfo(string p_strInPatientID, string p_strInPatientDate, out string p_strOpenDate)
        {
            p_strOpenDate = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long lngRes = 0;
            try
            {
                string m_strXML = "";
                int m_intRows = 0;
                lngRes = m_objServ.m_lngGetOpenDateInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, out m_strXML, out m_intRows);
                if (lngRes >= 0 && m_intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    p_strOpenDate = objReader.GetAttribute("OPENDATE").ToString();
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
            return lngRes;
        }

        #endregion

        #region �����ݿ��������¼
        /// <summary>
        /// �����ݿ��������¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objMain"></param>
        /// <returns></returns>
        public long m_lngGetMainInfo(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Main p_objMain)
        {
            p_objMain = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long lngRes = 0;
            try
            {
                string m_strXML = "";
                int m_intRows = 0;

                lngRes = m_objServ.m_lngGetMainInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objMain);
                #region �ɷ������ѷ���
                //if (lngRes >= 0 && lngRes > 0)
                //{
                //    p_objMain = new clsInHospitalMainRecord_Main();
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objMain.m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objMain.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objMain.m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objMain.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID").Replace('��', '\'');
                //                    p_objMain.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objMain.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objMain.m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objMain.m_strDiagnosisXML = objReader.GetAttribute("DIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strInHospitalDiagnosisXML = objReader.GetAttribute("INHOSPITALDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strMainDiagnosisXML = objReader.GetAttribute("MAINDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strICD_10OfMainXML = objReader.GetAttribute("ICD_10OFMAINXML").Replace('��', '\'');
                //                    p_objMain.m_strInfectionDiagnosisXML = objReader.GetAttribute("INFECTIONDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strICD_10OfInfectionXML = objReader.GetAttribute("ICD_10OFINFECTIONXML").Replace('��', '\'');
                //                    p_objMain.m_strPathologyDiagnosisXML = objReader.GetAttribute("PATHOLOGYDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strScacheSourceXML = objReader.GetAttribute("SCACHESOURCEXML").Replace('��', '\'');
                //                    p_objMain.m_strSensitiveXML = objReader.GetAttribute("SENSITIVEXML").Replace('��', '\'');
                //                    p_objMain.m_strHbsAgXML = objReader.GetAttribute("HBSAGXML").Replace('��', '\'');
                //                    p_objMain.m_strHCV_AbXML = objReader.GetAttribute("HCV_ABXML").Replace('��', '\'');
                //                    p_objMain.m_strHIV_AbXML = objReader.GetAttribute("HIV_ABXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordWithOutHospitalXML = objReader.GetAttribute("ACCORDWITHOUTHOSPITALXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordInWithOutXML = objReader.GetAttribute("ACCORDINWITHOUTXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordBeforeOperationWithAfterXML = objReader.GetAttribute("ACCORDBFOPRWITHAFXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordClinicWithPathologyXML = objReader.GetAttribute("ACCORDCLINICWITHPATHOLOGYXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordRadiateWithPathologyXML = objReader.GetAttribute("ACCORDRADIATEWITHPATHOLOGYXML").Replace('��', '\'');
                //                    p_objMain.m_strSalveTimesXML = objReader.GetAttribute("SALVETIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strSalveSuccessXML = objReader.GetAttribute("SALVESUCCESSXML").Replace('��', '\'');
                //                    p_objMain.m_strOriginalDiseaseGyXML = objReader.GetAttribute("ORIGINALDISEASEGYXML").Replace('��', '\'');
                //                    p_objMain.m_strOriginalDiseaseTimesXML = objReader.GetAttribute("ORIGINALDISEASETIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strOriginalDiseaseDaysXML = objReader.GetAttribute("ORIGINALDISEASEDAYSXML").Replace('��', '\'');
                //                    p_objMain.m_strLymphGyXML = objReader.GetAttribute("LYMPHGYXML").Replace('��', '\'');
                //                    p_objMain.m_strLymphTimesXML = objReader.GetAttribute("LYMPHTIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strLymphDaysXML = objReader.GetAttribute("LYMPHDAYSXML").Replace('��', '\'');
                //                    p_objMain.m_strMetastasisGyXML = objReader.GetAttribute("METASTASISGYXML").Replace('��', '\'');
                //                    p_objMain.m_strMetastasisTimesXML = objReader.GetAttribute("METASTASISTIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strMetastasisDaysXML = objReader.GetAttribute("METASTASISDAYSXML").Replace('��', '\'');
                //                    p_objMain.m_strTotalAmtXML = objReader.GetAttribute("TOTALAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strBedAmtXML = objReader.GetAttribute("BEDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseAmtXML = objReader.GetAttribute("NURSEAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strWMAmtXML = objReader.GetAttribute("WMAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strCMFinishedAmtXML = objReader.GetAttribute("CMFINISHEDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strCMSemiFinishedAmtXML = objReader.GetAttribute("CMSEMIFINISHEDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strRadiationAmtXML = objReader.GetAttribute("RADIATIONAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strAssayAmtXML = objReader.GetAttribute("ASSAYAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strO2AmtXML = objReader.GetAttribute("O2AMTXML").Replace('��', '\'');
                //                    p_objMain.m_strBloodAmtXML = objReader.GetAttribute("BLOODAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strTreatmentAmtXML = objReader.GetAttribute("TREATMENTAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strOperationAmtXML = objReader.GetAttribute("OPERATIONAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strDeliveryChildAmtXML = objReader.GetAttribute("DELIVERYCHILDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strCheckAmtXML = objReader.GetAttribute("CHECKAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strAnaethesiaAmtXML = objReader.GetAttribute("ANAETHESIAAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strBabyAmtXML = objReader.GetAttribute("BABYAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strAccompanyAmtXML = objReader.GetAttribute("ACCOMPANYAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strOtherAmt1XML = objReader.GetAttribute("OTHERAMT1XML").Replace('��', '\'');
                //                    p_objMain.m_strOtherAmt2XML = objReader.GetAttribute("OTHERAMT2XML").Replace('��', '\'');
                //                    p_objMain.m_strOtherAmt3XML = objReader.GetAttribute("OTHERAMT3XML").Replace('��', '\'');
                //                    p_objMain.m_strFollow_WeekXML = objReader.GetAttribute("FOLLOW_WEEKXML").Replace('��', '\'');
                //                    p_objMain.m_strFollow_MonthXML = objReader.GetAttribute("FOLLOW_MONTHXML").Replace('��', '\'');
                //                    p_objMain.m_strFollow_YearXML = objReader.GetAttribute("FOLLOW_YEARXML").Replace('��', '\'');
                //                    p_objMain.m_strBloodTypeXML = objReader.GetAttribute("BLOODTYPEXML").Replace('��', '\'');
                //                    p_objMain.m_strRBCXML = objReader.GetAttribute("RBCXML").Replace('��', '\'');
                //                    p_objMain.m_strPLTXML = objReader.GetAttribute("PLTXML").Replace('��', '\'');
                //                    p_objMain.m_strPlasmXML = objReader.GetAttribute("PLASMXML").Replace('��', '\'');
                //                    p_objMain.m_strWholeBloodXML = objReader.GetAttribute("WHOLEBLOODXML").Replace('��', '\'');
                //                    p_objMain.m_strOtherBloodXML = objReader.GetAttribute("OTHERBLOODXML").Replace('��', '\'');
                //                    p_objMain.m_strConsultationXML = objReader.GetAttribute("CONSULTATIONXML").Replace('��', '\'');
                //                    p_objMain.m_strLongDistanctConsultationXML = objReader.GetAttribute("LONGDISTANCTCONSULTATIONXML").Replace('��', '\'');
                //                    p_objMain.m_strTOPLevelXML = objReader.GetAttribute("TOPLEVELXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseLevelIXML = objReader.GetAttribute("NURSELEVELIXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseLevelIIXML = objReader.GetAttribute("NURSELEVELIIXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseLevelIIIXML = objReader.GetAttribute("NURSELEVELIIIXML").Replace('��', '\'');
                //                    p_objMain.m_strICUXML = objReader.GetAttribute("ICUXML").Replace('��', '\'');
                //                    p_objMain.m_strSpecialNurseXML = objReader.GetAttribute("SPECIALNURSEXML").Replace('��', '\'');
                //                    p_objMain.m_strSpecialNurseXML = objReader.GetAttribute("SPECIALNURSEXML").Replace('��', '\'');
                //                    p_objMain.m_strInsuranceNumXML = objReader.GetAttribute("INSURANCENUMXML").Replace('��', '\'');
                //                    p_objMain.m_strModeOfPaymentXML = objReader.GetAttribute("MODEOFPAYMENTXML").Replace('��', '\'');
                //                    p_objMain.m_strPatientHistoryNOXML = objReader.GetAttribute("PATIENTHISTORYNOXML").Replace('��', '\'');
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetDeletedMainInfo(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Main p_objMain)
        {
            p_objMain = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                string m_strXML = "";
                int m_intRows = 0;

                m_lngRes = m_objServ.m_lngGetDeletedMainInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objMain);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objMain = new clsInHospitalMainRecord_Main();
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objMain.m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objMain.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objMain.m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objMain.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID").Replace('��', '\'');
                //                    p_objMain.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objMain.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objMain.m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objMain.m_strDiagnosisXML = objReader.GetAttribute("DIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strInHospitalDiagnosisXML = objReader.GetAttribute("INHOSPITALDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strMainDiagnosisXML = objReader.GetAttribute("MAINDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strICD_10OfMainXML = objReader.GetAttribute("ICD_10OFMAINXML").Replace('��', '\'');
                //                    p_objMain.m_strInfectionDiagnosisXML = objReader.GetAttribute("INFECTIONDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strICD_10OfInfectionXML = objReader.GetAttribute("ICD_10OFINFECTIONXML").Replace('��', '\'');
                //                    p_objMain.m_strPathologyDiagnosisXML = objReader.GetAttribute("PATHOLOGYDIAGNOSISXML").Replace('��', '\'');
                //                    p_objMain.m_strScacheSourceXML = objReader.GetAttribute("SCACHESOURCEXML").Replace('��', '\'');
                //                    p_objMain.m_strSensitiveXML = objReader.GetAttribute("SENSITIVEXML").Replace('��', '\'');
                //                    p_objMain.m_strHbsAgXML = objReader.GetAttribute("HBSAGXML").Replace('��', '\'');
                //                    p_objMain.m_strHCV_AbXML = objReader.GetAttribute("HCV_ABXML").Replace('��', '\'');
                //                    p_objMain.m_strHIV_AbXML = objReader.GetAttribute("HIV_ABXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordWithOutHospitalXML = objReader.GetAttribute("ACCORDWITHOUTHOSPITALXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordInWithOutXML = objReader.GetAttribute("ACCORDINWITHOUTXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordBeforeOperationWithAfterXML = objReader.GetAttribute("ACCORDBFOPRWITHAFXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordClinicWithPathologyXML = objReader.GetAttribute("ACCORDCLINICWITHPATHOLOGYXML").Replace('��', '\'');
                //                    p_objMain.m_strAccordRadiateWithPathologyXML = objReader.GetAttribute("ACCORDRADIATEWITHPATHOLOGYXML").Replace('��', '\'');
                //                    p_objMain.m_strSalveTimesXML = objReader.GetAttribute("SALVETIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strSalveSuccessXML = objReader.GetAttribute("SALVESUCCESSXML").Replace('��', '\'');
                //                    p_objMain.m_strOriginalDiseaseGyXML = objReader.GetAttribute("ORIGINALDISEASEGYXML").Replace('��', '\'');
                //                    p_objMain.m_strOriginalDiseaseTimesXML = objReader.GetAttribute("ORIGINALDISEASETIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strOriginalDiseaseDaysXML = objReader.GetAttribute("ORIGINALDISEASEDAYSXML").Replace('��', '\'');
                //                    p_objMain.m_strLymphGyXML = objReader.GetAttribute("LYMPHGYXML").Replace('��', '\'');
                //                    p_objMain.m_strLymphTimesXML = objReader.GetAttribute("LYMPHTIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strLymphDaysXML = objReader.GetAttribute("LYMPHDAYSXML").Replace('��', '\'');
                //                    p_objMain.m_strMetastasisGyXML = objReader.GetAttribute("METASTASISGYXML").Replace('��', '\'');
                //                    p_objMain.m_strMetastasisTimesXML = objReader.GetAttribute("METASTASISTIMESXML").Replace('��', '\'');
                //                    p_objMain.m_strMetastasisDaysXML = objReader.GetAttribute("METASTASISDAYSXML").Replace('��', '\'');
                //                    p_objMain.m_strTotalAmtXML = objReader.GetAttribute("TOTALAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strBedAmtXML = objReader.GetAttribute("BEDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseAmtXML = objReader.GetAttribute("NURSEAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strWMAmtXML = objReader.GetAttribute("WMAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strCMFinishedAmtXML = objReader.GetAttribute("CMFINISHEDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strCMSemiFinishedAmtXML = objReader.GetAttribute("CMSEMIFINISHEDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strRadiationAmtXML = objReader.GetAttribute("RADIATIONAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strAssayAmtXML = objReader.GetAttribute("ASSAYAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strO2AmtXML = objReader.GetAttribute("O2AMTXML").Replace('��', '\'');
                //                    p_objMain.m_strBloodAmtXML = objReader.GetAttribute("BLOODAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strTreatmentAmtXML = objReader.GetAttribute("TREATMENTAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strOperationAmtXML = objReader.GetAttribute("OPERATIONAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strDeliveryChildAmtXML = objReader.GetAttribute("DELIVERYCHILDAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strCheckAmtXML = objReader.GetAttribute("CHECKAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strAnaethesiaAmtXML = objReader.GetAttribute("ANAETHESIAAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strBabyAmtXML = objReader.GetAttribute("BABYAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strAccompanyAmtXML = objReader.GetAttribute("ACCOMPANYAMTXML").Replace('��', '\'');
                //                    p_objMain.m_strOtherAmt1XML = objReader.GetAttribute("OTHERAMT1XML").Replace('��', '\'');
                //                    p_objMain.m_strOtherAmt2XML = objReader.GetAttribute("OTHERAMT2XML").Replace('��', '\'');
                //                    p_objMain.m_strOtherAmt3XML = objReader.GetAttribute("OTHERAMT3XML").Replace('��', '\'');
                //                    p_objMain.m_strFollow_WeekXML = objReader.GetAttribute("FOLLOW_WEEKXML").Replace('��', '\'');
                //                    p_objMain.m_strFollow_MonthXML = objReader.GetAttribute("FOLLOW_MONTHXML").Replace('��', '\'');
                //                    p_objMain.m_strFollow_YearXML = objReader.GetAttribute("FOLLOW_YEARXML").Replace('��', '\'');
                //                    p_objMain.m_strBloodTypeXML = objReader.GetAttribute("BLOODTYPEXML").Replace('��', '\'');
                //                    p_objMain.m_strRBCXML = objReader.GetAttribute("RBCXML").Replace('��', '\'');
                //                    p_objMain.m_strPLTXML = objReader.GetAttribute("PLTXML").Replace('��', '\'');
                //                    p_objMain.m_strPlasmXML = objReader.GetAttribute("PLASMXML").Replace('��', '\'');
                //                    p_objMain.m_strWholeBloodXML = objReader.GetAttribute("WHOLEBLOODXML").Replace('��', '\'');
                //                    p_objMain.m_strOtherBloodXML = objReader.GetAttribute("OTHERBLOODXML").Replace('��', '\'');
                //                    p_objMain.m_strConsultationXML = objReader.GetAttribute("CONSULTATIONXML").Replace('��', '\'');
                //                    p_objMain.m_strLongDistanctConsultationXML = objReader.GetAttribute("LONGDISTANCTCONSULTATIONXML").Replace('��', '\'');
                //                    p_objMain.m_strTOPLevelXML = objReader.GetAttribute("TOPLEVELXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseLevelIXML = objReader.GetAttribute("NURSELEVELIXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseLevelIIXML = objReader.GetAttribute("NURSELEVELIIXML").Replace('��', '\'');
                //                    p_objMain.m_strNurseLevelIIIXML = objReader.GetAttribute("NURSELEVELIIIXML").Replace('��', '\'');
                //                    p_objMain.m_strICUXML = objReader.GetAttribute("ICUXML").Replace('��', '\'');
                //                    p_objMain.m_strSpecialNurseXML = objReader.GetAttribute("SPECIALNURSEXML").Replace('��', '\'');
                //                    p_objMain.m_strSpecialNurseXML = objReader.GetAttribute("SPECIALNURSEXML").Replace('��', '\'');
                //                    p_objMain.m_strInsuranceNumXML = objReader.GetAttribute("INSURANCENUMXML").Replace('��', '\'');
                //                    p_objMain.m_strModeOfPaymentXML = objReader.GetAttribute("MODEOFPAYMENTXML").Replace('��', '\'');
                //                    p_objMain.m_strPatientHistoryNOXML = objReader.GetAttribute("PATIENTHISTORYNOXML").Replace('��', '\'');
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        #endregion

        #region �����ݿ������ӱ��¼
        /// <summary>
        /// �����ݿ������ӱ��¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetContentInfo(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Content p_objContent)
        {
            p_objContent = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetContentInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objContent);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objContent = new clsInHospitalMainRecord_Content();
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objContent.m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objContent.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objContent.m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objContent.m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objContent.m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objContent.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objContent.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objContent.m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objContent.m_strDiagnosis = objReader.GetAttribute("DIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strInHospitalDiagnosis = objReader.GetAttribute("INHOSPITALDIAGNOSIS").Replace('��', '\'');
                //                    //ID
                //                    p_objContent.m_strDoctor = objReader.GetAttribute("DOCTOR").Replace('��', '\'');
                //                    //Name
                //                    p_objContent.m_strDoctorName = objReader.GetAttribute("DOCTORNAME").Replace('��', '\'');
                //                    p_objContent.m_strConfirmDiagnosisDate = objReader.GetAttribute("CONFIRMDIAGNOSISDATE").Replace('��', '\'');
                //                    p_objContent.m_strCondictionWhenIn = objReader.GetAttribute("CONDICTIONWHENIN").Replace('��', '\'');
                //                    p_objContent.m_strMainDiagnosis = objReader.GetAttribute("MAINDIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strMainConditionSeq = objReader.GetAttribute("MAINCONDITIONSEQ").Replace('��', '\'');
                //                    p_objContent.m_strICD_10OfMain = objReader.GetAttribute("ICD_10OFMAIN").Replace('��', '\'');
                //                    p_objContent.m_strInfectionDiagnosis = objReader.GetAttribute("INFECTIONDIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strInfectionCondictionSeq = objReader.GetAttribute("INFECTIONCONDICTIONSEQ").Replace('��', '\'');
                //                    p_objContent.m_strICD_10OfInfection = objReader.GetAttribute("ICD_10OFINFECTION").Replace('��', '\'');
                //                    p_objContent.m_strPathologyDiagnosis = objReader.GetAttribute("PATHOLOGYDIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strScacheSource = objReader.GetAttribute("SCACHESOURCE").Replace('��', '\'');
                //                    p_objContent.m_strSensitive = objReader.GetAttribute("SENSITIVE").Replace('��', '\'');
                //                    p_objContent.m_strHbsAg = objReader.GetAttribute("HBSAG").Replace('��', '\'');
                //                    p_objContent.m_strHCV_Ab = objReader.GetAttribute("HCV_AB").Replace('��', '\'');
                //                    p_objContent.m_strHIV_Ab = objReader.GetAttribute("HIV_AB").Replace('��', '\'');
                //                    p_objContent.m_strAccordWithOutHospital = objReader.GetAttribute("ACCORDWITHOUTHOSPITAL").Replace('��', '\'');
                //                    p_objContent.m_strAccordInWithOut = objReader.GetAttribute("ACCORDINWITHOUT").Replace('��', '\'');
                //                    p_objContent.m_strAccordBeforeOperationWithAfter = objReader.GetAttribute("ACCORDBEFOREOPERATIONWITHAFTER").Replace('��', '\'');
                //                    p_objContent.m_strAccordClinicWithPathology = objReader.GetAttribute("ACCORDCLINICWITHPATHOLOGY").Replace('��', '\'');
                //                    p_objContent.m_strAccordRadiateWithPathology = objReader.GetAttribute("ACCORDRADIATEWITHPATHOLOGY").Replace('��', '\'');
                //                    p_objContent.m_strSalveTimes = objReader.GetAttribute("SALVETIMES").Replace('��', '\'');
                //                    p_objContent.m_strSalveSuccess = objReader.GetAttribute("SALVESUCCESS").Replace('��', '\'');
                //                    //ID
                //                    p_objContent.m_strDirectorDt = objReader.GetAttribute("DIRECTORDT").Replace('��', '\'');
                //                    p_objContent.m_strSubDirectorDt = objReader.GetAttribute("SUBDIRECTORDT").Replace('��', '\'');
                //                    p_objContent.m_strDt = objReader.GetAttribute("DT").Replace('��', '\'');
                //                    p_objContent.m_strInHospitalDt = objReader.GetAttribute("INHOSPITALDT").Replace('��', '\'');
                //                    p_objContent.m_strAttendInForAdvancesStudyDt = objReader.GetAttribute("ATTENDINFORADVANCESSTUDYDT").Replace('��', '\'');
                //                    p_objContent.m_strGraduateStudentIntern = objReader.GetAttribute("GRADUATESTUDENTINTERN").Replace('��', '\'');
                //                    p_objContent.m_strIntern = objReader.GetAttribute("INTERN").Replace('��', '\'');
                //                    p_objContent.m_strCoder = objReader.GetAttribute("CODER").Replace('��', '\'');
                //                    p_objContent.m_strQCDt = objReader.GetAttribute("QCDT").Replace('��', '\'');
                //                    p_objContent.m_strQCNurse = objReader.GetAttribute("QCNURSE").Replace('��', '\'');
                //                    //Name
                //                    p_objContent.m_strDirectorDtName = objReader.GetAttribute("DIRECTORDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strSubDirectorDtName = objReader.GetAttribute("SUBDIRECTORDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strDtName = objReader.GetAttribute("DTNAME").Replace('��', '\'');
                //                    p_objContent.m_strInHospitalDtName = objReader.GetAttribute("INHOSPITALDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strAttendInForAdvancesStudyDtName = objReader.GetAttribute("ATTENDINFORADVANCESSTUDYDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strGraduateStudentInternName = objReader.GetAttribute("GRADUATESTUDENTINTERNNAME").Replace('��', '\'');
                //                    //ʵϰҽ���Լ�ǩ��
                //                    //								p_objContent.m_strInternName= objReader.GetAttribute("INTERNNAME").Replace('��','\'');
                //                    p_objContent.m_strCoderName = objReader.GetAttribute("CODERNAME").Replace('��', '\'');
                //                    p_objContent.m_strQCDtName = objReader.GetAttribute("QCDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strQCNurseName = objReader.GetAttribute("QCNURSENAME").Replace('��', '\'');

                //                    p_objContent.m_strQuality = objReader.GetAttribute("QUALITY").Replace('��', '\'');

                //                    p_objContent.m_strQCTime = objReader.GetAttribute("QCTIME").Replace('��', '\'');
                //                    p_objContent.m_strRTModeSeq = objReader.GetAttribute("RTMODESEQ").Replace('��', '\'');
                //                    p_objContent.m_strRTRuleSeq = objReader.GetAttribute("RTRULESEQ").Replace('��', '\'');
                //                    p_objContent.m_strRTCo = objReader.GetAttribute("RTCO").Replace('��', '\'');
                //                    p_objContent.m_strRTAccelerator = objReader.GetAttribute("RTACCELERATOR").Replace('��', '\'');
                //                    p_objContent.m_strRTX_Ray = objReader.GetAttribute("RTX_RAY").Replace('��', '\'');

                //                    p_objContent.m_strRTLacuna = objReader.GetAttribute("RTLACUNA").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseSeq = objReader.GetAttribute("ORIGINALDISEASESEQ").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseGy = objReader.GetAttribute("ORIGINALDISEASEGY").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseTimes = objReader.GetAttribute("ORIGINALDISEASETIMES").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseDays = objReader.GetAttribute("ORIGINALDISEASEDAYS").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseBeginDate = objReader.GetAttribute("ORIGINALDISEASEBEGINDATE").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseEndDate = objReader.GetAttribute("ORIGINALDISEASEENDDATE").Replace('��', '\'');
                //                    p_objContent.m_strLymphSeq = objReader.GetAttribute("LYMPHSEQ").Replace('��', '\'');
                //                    p_objContent.m_strLymphGy = objReader.GetAttribute("LYMPHGY").Replace('��', '\'');
                //                    p_objContent.m_strLymphTimes = objReader.GetAttribute("LYMPHTIMES").Replace('��', '\'');
                //                    p_objContent.m_strLymphDays = objReader.GetAttribute("LYMPHDAYS").Replace('��', '\'');
                //                    p_objContent.m_strLymphBeginDate = objReader.GetAttribute("LYMPHBEGINDATE").Replace('��', '\'');
                //                    p_objContent.m_strLymphEndDate = objReader.GetAttribute("LYMPHENDDATE").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisGy = objReader.GetAttribute("METASTASISGY").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisTimes = objReader.GetAttribute("METASTASISTIMES").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisDays = objReader.GetAttribute("METASTASISDAYS").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisBeginDate = objReader.GetAttribute("METASTASISBEGINDATE").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisEndDate = objReader.GetAttribute("METASTASISENDDATE").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyModeSeq = objReader.GetAttribute("CHEMOTHERAPYMODESEQ").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyWholeBody = objReader.GetAttribute("CHEMOTHERAPYWHOLEBODY").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyLocal = objReader.GetAttribute("CHEMOTHERAPYLOCAL").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyIntubate = objReader.GetAttribute("CHEMOTHERAPYINTUBATE").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyThorax = objReader.GetAttribute("CHEMOTHERAPYTHORAX").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyAbdomen = objReader.GetAttribute("CHEMOTHERAPYABDOMEN").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapySpinal = objReader.GetAttribute("CHEMOTHERAPYSPINAL").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyOtherTry = objReader.GetAttribute("CHEMOTHERAPYOTHERTRY").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyOther = objReader.GetAttribute("CHEMOTHERAPYOTHER").Replace('��', '\'');
                //                    p_objContent.m_strTotalAmt = objReader.GetAttribute("TOTALAMT").Replace('��', '\'');
                //                    p_objContent.m_strBedAmt = objReader.GetAttribute("BEDAMT").Replace('��', '\'');
                //                    p_objContent.m_strNurseAmt = objReader.GetAttribute("NURSEAMT").Replace('��', '\'');
                //                    p_objContent.m_strWMAmt = objReader.GetAttribute("WMAMT").Replace('��', '\'');
                //                    p_objContent.m_strCMFinishedAmt = objReader.GetAttribute("CMFINISHEDAMT").Replace('��', '\'');
                //                    p_objContent.m_strCMSemiFinishedAmt = objReader.GetAttribute("CMSEMIFINISHEDAMT").Replace('��', '\'');
                //                    p_objContent.m_strRadiationAmt = objReader.GetAttribute("RADIATIONAMT").Replace('��', '\'');
                //                    p_objContent.m_strAssayAmt = objReader.GetAttribute("ASSAYAMT").Replace('��', '\'');
                //                    p_objContent.m_strO2Amt = objReader.GetAttribute("O2AMT").Replace('��', '\'');
                //                    p_objContent.m_strBloodAmt = objReader.GetAttribute("BLOODAMT").Replace('��', '\'');
                //                    p_objContent.m_strTreatmentAmt = objReader.GetAttribute("TREATMENTAMT").Replace('��', '\'');
                //                    p_objContent.m_strOperationAmt = objReader.GetAttribute("OPERATIONAMT").Replace('��', '\'');
                //                    p_objContent.m_strDeliveryChildAmt = objReader.GetAttribute("DELIVERYCHILDAMT").Replace('��', '\'');
                //                    p_objContent.m_strCheckAmt = objReader.GetAttribute("CHECKAMT").Replace('��', '\'');
                //                    p_objContent.m_strAnaethesiaAmt = objReader.GetAttribute("ANAETHESIAAMT").Replace('��', '\'');
                //                    p_objContent.m_strBabyAmt = objReader.GetAttribute("BABYAMT").Replace('��', '\'');
                //                    p_objContent.m_strAccompanyAmt = objReader.GetAttribute("ACCOMPANYAMT").Replace('��', '\'');
                //                    p_objContent.m_strOtherAmt1 = objReader.GetAttribute("OTHERAMT1").Replace('��', '\'');
                //                    p_objContent.m_strOtherAmt2 = objReader.GetAttribute("OTHERAMT2").Replace('��', '\'');
                //                    p_objContent.m_strOtherAmt3 = objReader.GetAttribute("OTHERAMT3").Replace('��', '\'');
                //                    p_objContent.m_strCorpseCheck = objReader.GetAttribute("CORPSECHECK").Replace('��', '\'');
                //                    p_objContent.m_strFirstCase = objReader.GetAttribute("FIRSTCASE").Replace('��', '\'');
                //                    p_objContent.m_strFollow = objReader.GetAttribute("FOLLOW").Replace('��', '\'');
                //                    p_objContent.m_strFollow_Week = objReader.GetAttribute("FOLLOW_WEEK").Replace('��', '\'');
                //                    p_objContent.m_strFollow_Month = objReader.GetAttribute("FOLLOW_MONTH").Replace('��', '\'');
                //                    p_objContent.m_strFollow_Year = objReader.GetAttribute("FOLLOW_YEAR").Replace('��', '\'');
                //                    p_objContent.m_strModelCase = objReader.GetAttribute("MODELCASE").Replace('��', '\'');
                //                    p_objContent.m_strBloodType = objReader.GetAttribute("BLOODTYPE").Replace('��', '\'');
                //                    p_objContent.m_strBloodRh = objReader.GetAttribute("BLOODRH").Replace('��', '\'');

                //                    p_objContent.m_strBloodTransActoin = objReader.GetAttribute("BLOODTRANSACTOIN").Replace('��', '\'');
                //                    p_objContent.m_strRBC = objReader.GetAttribute("RBC").Replace('��', '\'');
                //                    p_objContent.m_strPLT = objReader.GetAttribute("PLT").Replace('��', '\'');
                //                    p_objContent.m_strPlasm = objReader.GetAttribute("PLASM").Replace('��', '\'');
                //                    p_objContent.m_strWholeBlood = objReader.GetAttribute("WHOLEBLOOD").Replace('��', '\'');
                //                    p_objContent.m_strOtherBlood = objReader.GetAttribute("OTHERBLOOD").Replace('��', '\'');
                //                    p_objContent.m_strConsultation = objReader.GetAttribute("CONSULTATION").Replace('��', '\'');
                //                    p_objContent.m_strLongDistanctConsultation = objReader.GetAttribute("LONGDISTANCTCONSULTATION").Replace('��', '\'');
                //                    p_objContent.m_strTOPLevel = objReader.GetAttribute("TOPLEVEL").Replace('��', '\'');
                //                    p_objContent.m_strNurseLevelI = objReader.GetAttribute("NURSELEVELI").Replace('��', '\'');
                //                    p_objContent.m_strNurseLevelII = objReader.GetAttribute("NURSELEVELII").Replace('��', '\'');
                //                    p_objContent.m_strNurseLevelIII = objReader.GetAttribute("NURSELEVELIII").Replace('��', '\'');
                //                    p_objContent.m_strICU = objReader.GetAttribute("ICU").Replace('��', '\'');
                //                    p_objContent.m_strSpecialNurse = objReader.GetAttribute("SPECIALNURSE").Replace('��', '\'');
                //                    p_objContent.m_strInsuranceNum = objReader.GetAttribute("INSURANCENUM").Replace('��', '\'');
                //                    p_objContent.m_strModeOfPayment = objReader.GetAttribute("MODEOFPAYMENT").Replace('��', '\'');
                //                    p_objContent.m_strPatientHistoryNO = objReader.GetAttribute("PATIENTHISTORYNO").Replace('��', '\'');
                //                    p_objContent.m_strOutPatientDate = objReader.GetAttribute("OUTPATIENTDATE").Replace('��', '\'');
                //                    p_objContent.m_strBirthPlace = objReader.GetAttribute("BIRTHPLACE").Replace('��', '\'');
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetDeletedContentInfo(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Content p_objContent)
        {
            p_objContent = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeletedContentInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objContent);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objContent = new clsInHospitalMainRecord_Content();
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objContent.m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objContent.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objContent.m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objContent.m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objContent.m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objContent.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objContent.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objContent.m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objContent.m_strDiagnosis = objReader.GetAttribute("DIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strInHospitalDiagnosis = objReader.GetAttribute("INHOSPITALDIAGNOSIS").Replace('��', '\'');
                //                    //ID
                //                    p_objContent.m_strDoctor = objReader.GetAttribute("DOCTOR").Replace('��', '\'');
                //                    //Name
                //                    p_objContent.m_strDoctorName = objReader.GetAttribute("DOCTORNAME").Replace('��', '\'');
                //                    p_objContent.m_strConfirmDiagnosisDate = objReader.GetAttribute("CONFIRMDIAGNOSISDATE").Replace('��', '\'');
                //                    p_objContent.m_strCondictionWhenIn = objReader.GetAttribute("CONDICTIONWHENIN").Replace('��', '\'');
                //                    p_objContent.m_strMainDiagnosis = objReader.GetAttribute("MAINDIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strMainConditionSeq = objReader.GetAttribute("MAINCONDITIONSEQ").Replace('��', '\'');
                //                    p_objContent.m_strICD_10OfMain = objReader.GetAttribute("ICD_10OFMAIN").Replace('��', '\'');
                //                    p_objContent.m_strInfectionDiagnosis = objReader.GetAttribute("INFECTIONDIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strInfectionCondictionSeq = objReader.GetAttribute("INFECTIONCONDICTIONSEQ").Replace('��', '\'');
                //                    p_objContent.m_strICD_10OfInfection = objReader.GetAttribute("ICD_10OFINFECTION").Replace('��', '\'');
                //                    p_objContent.m_strPathologyDiagnosis = objReader.GetAttribute("PATHOLOGYDIAGNOSIS").Replace('��', '\'');
                //                    p_objContent.m_strScacheSource = objReader.GetAttribute("SCACHESOURCE").Replace('��', '\'');
                //                    p_objContent.m_strSensitive = objReader.GetAttribute("SENSITIVE").Replace('��', '\'');
                //                    p_objContent.m_strHbsAg = objReader.GetAttribute("HBSAG").Replace('��', '\'');
                //                    p_objContent.m_strHCV_Ab = objReader.GetAttribute("HCV_AB").Replace('��', '\'');
                //                    p_objContent.m_strHIV_Ab = objReader.GetAttribute("HIV_AB").Replace('��', '\'');
                //                    p_objContent.m_strAccordWithOutHospital = objReader.GetAttribute("ACCORDWITHOUTHOSPITAL").Replace('��', '\'');
                //                    p_objContent.m_strAccordInWithOut = objReader.GetAttribute("ACCORDINWITHOUT").Replace('��', '\'');
                //                    p_objContent.m_strAccordBeforeOperationWithAfter = objReader.GetAttribute("ACCORDBEFOREOPERATIONWITHAFTER").Replace('��', '\'');
                //                    p_objContent.m_strAccordClinicWithPathology = objReader.GetAttribute("ACCORDCLINICWITHPATHOLOGY").Replace('��', '\'');
                //                    p_objContent.m_strAccordRadiateWithPathology = objReader.GetAttribute("ACCORDRADIATEWITHPATHOLOGY").Replace('��', '\'');
                //                    p_objContent.m_strSalveTimes = objReader.GetAttribute("SALVETIMES").Replace('��', '\'');
                //                    p_objContent.m_strSalveSuccess = objReader.GetAttribute("SALVESUCCESS").Replace('��', '\'');
                //                    //ID
                //                    p_objContent.m_strDirectorDt = objReader.GetAttribute("DIRECTORDT").Replace('��', '\'');
                //                    p_objContent.m_strSubDirectorDt = objReader.GetAttribute("SUBDIRECTORDT").Replace('��', '\'');
                //                    p_objContent.m_strDt = objReader.GetAttribute("DT").Replace('��', '\'');
                //                    p_objContent.m_strInHospitalDt = objReader.GetAttribute("INHOSPITALDT").Replace('��', '\'');
                //                    p_objContent.m_strAttendInForAdvancesStudyDt = objReader.GetAttribute("ATTENDINFORADVANCESSTUDYDT").Replace('��', '\'');
                //                    p_objContent.m_strGraduateStudentIntern = objReader.GetAttribute("GRADUATESTUDENTINTERN").Replace('��', '\'');
                //                    p_objContent.m_strIntern = objReader.GetAttribute("INTERN").Replace('��', '\'');
                //                    p_objContent.m_strCoder = objReader.GetAttribute("CODER").Replace('��', '\'');
                //                    p_objContent.m_strQCDt = objReader.GetAttribute("QCDT").Replace('��', '\'');
                //                    p_objContent.m_strQCNurse = objReader.GetAttribute("QCNURSE").Replace('��', '\'');
                //                    //Name
                //                    p_objContent.m_strDirectorDtName = objReader.GetAttribute("DIRECTORDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strSubDirectorDtName = objReader.GetAttribute("SUBDIRECTORDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strDtName = objReader.GetAttribute("DTNAME").Replace('��', '\'');
                //                    p_objContent.m_strInHospitalDtName = objReader.GetAttribute("INHOSPITALDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strAttendInForAdvancesStudyDtName = objReader.GetAttribute("ATTENDINFORADVANCESSTUDYDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strGraduateStudentInternName = objReader.GetAttribute("GRADUATESTUDENTINTERNNAME").Replace('��', '\'');
                //                    //ʵϰҽ���Լ�ǩ��
                //                    //								p_objContent.m_strInternName= objReader.GetAttribute("INTERNNAME").Replace('��','\'');
                //                    p_objContent.m_strCoderName = objReader.GetAttribute("CODERNAME").Replace('��', '\'');
                //                    p_objContent.m_strQCDtName = objReader.GetAttribute("QCDTNAME").Replace('��', '\'');
                //                    p_objContent.m_strQCNurseName = objReader.GetAttribute("QCNURSENAME").Replace('��', '\'');

                //                    p_objContent.m_strQuality = objReader.GetAttribute("QUALITY").Replace('��', '\'');

                //                    p_objContent.m_strQCTime = objReader.GetAttribute("QCTIME").Replace('��', '\'');
                //                    p_objContent.m_strRTModeSeq = objReader.GetAttribute("RTMODESEQ").Replace('��', '\'');
                //                    p_objContent.m_strRTRuleSeq = objReader.GetAttribute("RTRULESEQ").Replace('��', '\'');
                //                    p_objContent.m_strRTCo = objReader.GetAttribute("RTCO").Replace('��', '\'');
                //                    p_objContent.m_strRTAccelerator = objReader.GetAttribute("RTACCELERATOR").Replace('��', '\'');
                //                    p_objContent.m_strRTX_Ray = objReader.GetAttribute("RTX_RAY").Replace('��', '\'');

                //                    p_objContent.m_strRTLacuna = objReader.GetAttribute("RTLACUNA").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseSeq = objReader.GetAttribute("ORIGINALDISEASESEQ").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseGy = objReader.GetAttribute("ORIGINALDISEASEGY").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseTimes = objReader.GetAttribute("ORIGINALDISEASETIMES").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseDays = objReader.GetAttribute("ORIGINALDISEASEDAYS").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseBeginDate = objReader.GetAttribute("ORIGINALDISEASEBEGINDATE").Replace('��', '\'');
                //                    p_objContent.m_strOriginalDiseaseEndDate = objReader.GetAttribute("ORIGINALDISEASEENDDATE").Replace('��', '\'');
                //                    p_objContent.m_strLymphSeq = objReader.GetAttribute("LYMPHSEQ").Replace('��', '\'');
                //                    p_objContent.m_strLymphGy = objReader.GetAttribute("LYMPHGY").Replace('��', '\'');
                //                    p_objContent.m_strLymphTimes = objReader.GetAttribute("LYMPHTIMES").Replace('��', '\'');
                //                    p_objContent.m_strLymphDays = objReader.GetAttribute("LYMPHDAYS").Replace('��', '\'');
                //                    p_objContent.m_strLymphBeginDate = objReader.GetAttribute("LYMPHBEGINDATE").Replace('��', '\'');
                //                    p_objContent.m_strLymphEndDate = objReader.GetAttribute("LYMPHENDDATE").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisGy = objReader.GetAttribute("METASTASISGY").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisTimes = objReader.GetAttribute("METASTASISTIMES").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisDays = objReader.GetAttribute("METASTASISDAYS").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisBeginDate = objReader.GetAttribute("METASTASISBEGINDATE").Replace('��', '\'');
                //                    p_objContent.m_strMetastasisEndDate = objReader.GetAttribute("METASTASISENDDATE").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyModeSeq = objReader.GetAttribute("CHEMOTHERAPYMODESEQ").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyWholeBody = objReader.GetAttribute("CHEMOTHERAPYWHOLEBODY").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyLocal = objReader.GetAttribute("CHEMOTHERAPYLOCAL").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyIntubate = objReader.GetAttribute("CHEMOTHERAPYINTUBATE").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyThorax = objReader.GetAttribute("CHEMOTHERAPYTHORAX").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyAbdomen = objReader.GetAttribute("CHEMOTHERAPYABDOMEN").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapySpinal = objReader.GetAttribute("CHEMOTHERAPYSPINAL").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyOtherTry = objReader.GetAttribute("CHEMOTHERAPYOTHERTRY").Replace('��', '\'');
                //                    p_objContent.m_strChemotherapyOther = objReader.GetAttribute("CHEMOTHERAPYOTHER").Replace('��', '\'');
                //                    p_objContent.m_strTotalAmt = objReader.GetAttribute("TOTALAMT").Replace('��', '\'');
                //                    p_objContent.m_strBedAmt = objReader.GetAttribute("BEDAMT").Replace('��', '\'');
                //                    p_objContent.m_strNurseAmt = objReader.GetAttribute("NURSEAMT").Replace('��', '\'');
                //                    p_objContent.m_strWMAmt = objReader.GetAttribute("WMAMT").Replace('��', '\'');
                //                    p_objContent.m_strCMFinishedAmt = objReader.GetAttribute("CMFINISHEDAMT").Replace('��', '\'');
                //                    p_objContent.m_strCMSemiFinishedAmt = objReader.GetAttribute("CMSEMIFINISHEDAMT").Replace('��', '\'');
                //                    p_objContent.m_strRadiationAmt = objReader.GetAttribute("RADIATIONAMT").Replace('��', '\'');
                //                    p_objContent.m_strAssayAmt = objReader.GetAttribute("ASSAYAMT").Replace('��', '\'');
                //                    p_objContent.m_strO2Amt = objReader.GetAttribute("O2AMT").Replace('��', '\'');
                //                    p_objContent.m_strBloodAmt = objReader.GetAttribute("BLOODAMT").Replace('��', '\'');
                //                    p_objContent.m_strTreatmentAmt = objReader.GetAttribute("TREATMENTAMT").Replace('��', '\'');
                //                    p_objContent.m_strOperationAmt = objReader.GetAttribute("OPERATIONAMT").Replace('��', '\'');
                //                    p_objContent.m_strDeliveryChildAmt = objReader.GetAttribute("DELIVERYCHILDAMT").Replace('��', '\'');
                //                    p_objContent.m_strCheckAmt = objReader.GetAttribute("CHECKAMT").Replace('��', '\'');
                //                    p_objContent.m_strAnaethesiaAmt = objReader.GetAttribute("ANAETHESIAAMT").Replace('��', '\'');
                //                    p_objContent.m_strBabyAmt = objReader.GetAttribute("BABYAMT").Replace('��', '\'');
                //                    p_objContent.m_strAccompanyAmt = objReader.GetAttribute("ACCOMPANYAMT").Replace('��', '\'');
                //                    p_objContent.m_strOtherAmt1 = objReader.GetAttribute("OTHERAMT1").Replace('��', '\'');
                //                    p_objContent.m_strOtherAmt2 = objReader.GetAttribute("OTHERAMT2").Replace('��', '\'');
                //                    p_objContent.m_strOtherAmt3 = objReader.GetAttribute("OTHERAMT3").Replace('��', '\'');
                //                    p_objContent.m_strCorpseCheck = objReader.GetAttribute("CORPSECHECK").Replace('��', '\'');
                //                    p_objContent.m_strFirstCase = objReader.GetAttribute("FIRSTCASE").Replace('��', '\'');
                //                    p_objContent.m_strFollow = objReader.GetAttribute("FOLLOW").Replace('��', '\'');
                //                    p_objContent.m_strFollow_Week = objReader.GetAttribute("FOLLOW_WEEK").Replace('��', '\'');
                //                    p_objContent.m_strFollow_Month = objReader.GetAttribute("FOLLOW_MONTH").Replace('��', '\'');
                //                    p_objContent.m_strFollow_Year = objReader.GetAttribute("FOLLOW_YEAR").Replace('��', '\'');
                //                    p_objContent.m_strModelCase = objReader.GetAttribute("MODELCASE").Replace('��', '\'');
                //                    p_objContent.m_strBloodType = objReader.GetAttribute("BLOODTYPE").Replace('��', '\'');
                //                    p_objContent.m_strBloodRh = objReader.GetAttribute("BLOODRH").Replace('��', '\'');

                //                    p_objContent.m_strBloodTransActoin = objReader.GetAttribute("BLOODTRANSACTOIN").Replace('��', '\'');
                //                    p_objContent.m_strRBC = objReader.GetAttribute("RBC").Replace('��', '\'');
                //                    p_objContent.m_strPLT = objReader.GetAttribute("PLT").Replace('��', '\'');
                //                    p_objContent.m_strPlasm = objReader.GetAttribute("PLASM").Replace('��', '\'');
                //                    p_objContent.m_strWholeBlood = objReader.GetAttribute("WHOLEBLOOD").Replace('��', '\'');
                //                    p_objContent.m_strOtherBlood = objReader.GetAttribute("OTHERBLOOD").Replace('��', '\'');
                //                    p_objContent.m_strConsultation = objReader.GetAttribute("CONSULTATION").Replace('��', '\'');
                //                    p_objContent.m_strLongDistanctConsultation = objReader.GetAttribute("LONGDISTANCTCONSULTATION").Replace('��', '\'');
                //                    p_objContent.m_strTOPLevel = objReader.GetAttribute("TOPLEVEL").Replace('��', '\'');
                //                    p_objContent.m_strNurseLevelI = objReader.GetAttribute("NURSELEVELI").Replace('��', '\'');
                //                    p_objContent.m_strNurseLevelII = objReader.GetAttribute("NURSELEVELII").Replace('��', '\'');
                //                    p_objContent.m_strNurseLevelIII = objReader.GetAttribute("NURSELEVELIII").Replace('��', '\'');
                //                    p_objContent.m_strICU = objReader.GetAttribute("ICU").Replace('��', '\'');
                //                    p_objContent.m_strSpecialNurse = objReader.GetAttribute("SPECIALNURSE").Replace('��', '\'');
                //                    p_objContent.m_strInsuranceNum = objReader.GetAttribute("INSURANCENUM").Replace('��', '\'');
                //                    p_objContent.m_strModeOfPayment = objReader.GetAttribute("MODEOFPAYMENT").Replace('��', '\'');
                //                    p_objContent.m_strPatientHistoryNO = objReader.GetAttribute("PATIENTHISTORYNO").Replace('��', '\'');
                //                    p_objContent.m_strOutPatientDate = objReader.GetAttribute("OUTPATIENTDATE").Replace('��', '\'');
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        #endregion

        #region �����ݿ�����������ӱ��¼(�ñ��ѷ���)
        /// <summary>
        /// �����ݿ�����������ӱ��¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public long m_lngGetOtherDiagnosisArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_OtherDiagnosis[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetOtherDiagnosisInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out m_strXML, out m_intRows);
                if (m_lngRes >= 0 && m_intRows > 0)
                {
                    p_objDataArr = new clsInHospitalMainRecord_OtherDiagnosis[m_intRows];
                    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    int m_intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_OtherDiagnosis();
                                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strDiagnosisDesc = objReader.GetAttribute("DIAGNOSISDESC").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strConditionSeq = objReader.GetAttribute("CONDITIONSEQ").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strICD10 = objReader.GetAttribute("ICD10").Replace('��', '\'');

                                    m_intIndex++;
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
            return m_lngRes;
        }

        public long m_lngGetDeletedOtherDiagnosisArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_OtherDiagnosis[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeletedOtherDiagnosisInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out m_strXML, out m_intRows);
                if (m_lngRes >= 0 && m_intRows > 0)
                {
                    p_objDataArr = new clsInHospitalMainRecord_OtherDiagnosis[m_intRows];
                    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    int m_intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_OtherDiagnosis();
                                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strDiagnosisDesc = objReader.GetAttribute("DIAGNOSISDESC").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strConditionSeq = objReader.GetAttribute("CONDITIONSEQ").Replace('��', '\'');
                                    p_objDataArr[m_intIndex].m_strICD10 = objReader.GetAttribute("ICD10").Replace('��', '\'');

                                    m_intIndex++;
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
            return m_lngRes;
        }

        #endregion

        #region �����ݿ��������ӱ��¼
        /// <summary>
        /// �����ݿ��������ӱ��¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public long m_lngGetOperationArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out	clsInHospitalMainRecord_Operation[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetOperationInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objDataArr = new clsInHospitalMainRecord_Operation[m_intRows];
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int m_intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_Operation();
                //                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOperationID = objReader.GetAttribute("OPERATIONID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOperationDate = objReader.GetAttribute("OPERATIONDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOperationName = objReader.GetAttribute("OPERATIONNAME").Replace('��', '\'');
                //                    //ID
                //                    p_objDataArr[m_intIndex].m_strOperator = objReader.GetAttribute("OPERATOR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant1 = objReader.GetAttribute("ASSISTANT1").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant2 = objReader.GetAttribute("ASSISTANT2").Replace('��', '\'');
                //                    //Name
                //                    p_objDataArr[m_intIndex].m_strOperatorName = objReader.GetAttribute("OPERATORNAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant1Name = objReader.GetAttribute("ASSISTANT1NAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant2Name = objReader.GetAttribute("ASSISTANT2NAME").Replace('��', '\'');
                //                    //id
                //                    p_objDataArr[m_intIndex].m_strAanaesthesiaModeID = objReader.GetAttribute("AANAESTHESIAMODEID").Replace('��', '\'');
                //                    //name
                //                    p_objDataArr[m_intIndex].m_strAanaesthesiaModeName = objReader.GetAttribute("OPERATIONAANAESTHESIAMODENAME").Replace('��', '\'');

                //                    p_objDataArr[m_intIndex].m_strCutLevel = objReader.GetAttribute("CUTLEVEL").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAnaesthetist = objReader.GetAttribute("ANAESTHETIST").Replace('��', '\'');
                //                    //Name
                //                    p_objDataArr[m_intIndex].m_strAnaesthetistName = objReader.GetAttribute("OPERATIONANAESTHETISTNAME").Replace('��', '\'');
                //                    m_intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetDeletedOperationArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out	clsInHospitalMainRecord_Operation[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeletedOperationInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objDataArr = new clsInHospitalMainRecord_Operation[m_intRows];
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int m_intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_Operation();
                //                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOperationID = objReader.GetAttribute("OPERATIONID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOperationDate = objReader.GetAttribute("OPERATIONDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOperationName = objReader.GetAttribute("OPERATIONNAME").Replace('��', '\'');
                //                    //ID
                //                    p_objDataArr[m_intIndex].m_strOperator = objReader.GetAttribute("OPERATOR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant1 = objReader.GetAttribute("ASSISTANT1").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant2 = objReader.GetAttribute("ASSISTANT2").Replace('��', '\'');
                //                    //Name
                //                    p_objDataArr[m_intIndex].m_strOperatorName = objReader.GetAttribute("OPERATORNAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant1Name = objReader.GetAttribute("ASSISTANT1NAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAssistant2Name = objReader.GetAttribute("ASSISTANT2NAME").Replace('��', '\'');
                //                    //id
                //                    p_objDataArr[m_intIndex].m_strAanaesthesiaModeID = objReader.GetAttribute("AANAESTHESIAMODEID").Replace('��', '\'');
                //                    //name
                //                    p_objDataArr[m_intIndex].m_strAanaesthesiaModeName = objReader.GetAttribute("OPERATIONAANAESTHESIAMODENAME").Replace('��', '\'');

                //                    p_objDataArr[m_intIndex].m_strCutLevel = objReader.GetAttribute("CUTLEVEL").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strAnaesthetist = objReader.GetAttribute("ANAESTHETIST").Replace('��', '\'');
                //                    //Name
                //                    p_objDataArr[m_intIndex].m_strAnaesthetistName = objReader.GetAttribute("OPERATIONANAESTHETISTNAME").Replace('��', '\'');
                //                    m_intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        #endregion

        #region �����ݿ���Ӥ���ӱ��¼
        /// <summary>
        /// �����ݿ���Ӥ���ӱ��¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public long m_lngGetBabyArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Baby[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetBabyInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objDataArr = new clsInHospitalMainRecord_Baby[m_intRows];
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int m_intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_Baby();
                //                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strMale = objReader.GetAttribute("MALE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strFemale = objReader.GetAttribute("FEMALE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLiveBorn = objReader.GetAttribute("LIVEBORN").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDieBorn = objReader.GetAttribute("DIEBORN").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDieNotBorn = objReader.GetAttribute("DIENOTBORN").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strWeight = objReader.GetAttribute("WEIGHT").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDie = objReader.GetAttribute("DIE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strChangeDepartment = objReader.GetAttribute("CHANGEDEPARTMENT").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOutHospital = objReader.GetAttribute("OUTHOSPITAL").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strNaturalCondiction = objReader.GetAttribute("NATURALCONDICTION").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSuffocate1 = objReader.GetAttribute("SUFFOCATE1").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSuffocate2 = objReader.GetAttribute("SUFFOCATE2").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInfectionTimes = objReader.GetAttribute("INFECTIONTIMES").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInfectionName = objReader.GetAttribute("INFECTIONNAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strICD10 = objReader.GetAttribute("ICD10").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSalveTimes = objReader.GetAttribute("SALVETIMES").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSalveSuccessTimes = objReader.GetAttribute("SALVESUCCESSTIMES").Replace('��', '\'');
                //                    m_intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetDeletedBabyArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Baby[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeletedBabyInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objDataArr = new clsInHospitalMainRecord_Baby[m_intRows];
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int m_intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_Baby();
                //                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strMale = objReader.GetAttribute("MALE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strFemale = objReader.GetAttribute("FEMALE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLiveBorn = objReader.GetAttribute("LIVEBORN").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDieBorn = objReader.GetAttribute("DIEBORN").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDieNotBorn = objReader.GetAttribute("DIENOTBORN").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strWeight = objReader.GetAttribute("WEIGHT").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDie = objReader.GetAttribute("DIE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strChangeDepartment = objReader.GetAttribute("CHANGEDEPARTMENT").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOutHospital = objReader.GetAttribute("OUTHOSPITAL").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strNaturalCondiction = objReader.GetAttribute("NATURALCONDICTION").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSuffocate1 = objReader.GetAttribute("SUFFOCATE1").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSuffocate2 = objReader.GetAttribute("SUFFOCATE2").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInfectionTimes = objReader.GetAttribute("INFECTIONTIMES").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInfectionName = objReader.GetAttribute("INFECTIONNAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strICD10 = objReader.GetAttribute("ICD10").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSalveTimes = objReader.GetAttribute("SALVETIMES").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSalveSuccessTimes = objReader.GetAttribute("SALVESUCCESSTIMES").Replace('��', '\'');
                //                    m_intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        #endregion

        #region �����ݿ��û����ӱ��¼
        /// <summary>
        /// �����ݿ��û����ӱ��¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public long m_lngGetChemotherapyArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Chemotherapy[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetChemotherapyInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objDataArr = new clsInHospitalMainRecord_Chemotherapy[m_intRows];
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int m_intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_Chemotherapy();
                //                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strChemotherapyDate = objReader.GetAttribute("CHEMOTHERAPYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strMedicineName = objReader.GetAttribute("MEDICINENAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strPeriod = objReader.GetAttribute("PERIOD").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_CR = objReader.GetAttribute("FIELD_CR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_PR = objReader.GetAttribute("FIELD_PR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_MR = objReader.GetAttribute("FIELD_MR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_S = objReader.GetAttribute("FIELD_S").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_P = objReader.GetAttribute("FIELD_P").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_NA = objReader.GetAttribute("FIELD_NA").Replace('��', '\'');

                //                    m_intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetDeletedChemotherapyArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Chemotherapy[] p_objDataArr)
        {
            p_objDataArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string m_strXML = "";
            int m_intRows = 0;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeletedChemotherapyInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && m_intRows > 0)
                //{
                //    p_objDataArr = new clsInHospitalMainRecord_Chemotherapy[m_intRows];
                //    XmlTextReader objReader = new XmlTextReader(m_strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int m_intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objDataArr[m_intIndex] = new clsInHospitalMainRecord_Chemotherapy();
                //                    p_objDataArr[m_intIndex].m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strSeqID = objReader.GetAttribute("SEQID").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strChemotherapyDate = objReader.GetAttribute("CHEMOTHERAPYDATE").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strMedicineName = objReader.GetAttribute("MEDICINENAME").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strPeriod = objReader.GetAttribute("PERIOD").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_CR = objReader.GetAttribute("FIELD_CR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_PR = objReader.GetAttribute("FIELD_PR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_MR = objReader.GetAttribute("FIELD_MR").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_S = objReader.GetAttribute("FIELD_S").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_P = objReader.GetAttribute("FIELD_P").Replace('��', '\'');
                //                    p_objDataArr[m_intIndex].m_strField_NA = objReader.GetAttribute("FIELD_NA").Replace('��', '\'');

                //                    m_intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        #endregion

        #region �����ݿ��ȡ��ϼ�¼
        /// <summary>
        /// �����ݿ��ȡ��ϼ�¼
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��󱣴�ʱ��</param>
        /// <param name="p_objDataArr">��ϼ�¼</param>
        /// <returns></returns>
        public long m_lngGetDiagnosisArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Diagnosis[] p_objDataArr)
        {
            p_objDataArr = null;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDiagnosisInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            return m_lngRes;
        }

        /// <summary>
        /// �����ݿ��ȡ��ϼ�¼
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��󱣴�ʱ��</param>
        /// <param name="p_objDataArr">��ϼ�¼</param>
        /// <returns></returns>
        public long m_lngGetDeleteDiagnosisArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Diagnosis[] p_objDataArr)
        {
            p_objDataArr = null;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeleteDiagnosisInfo(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objDataArr);
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            return m_lngRes;
        }
        #endregion

        #region ģ����ѯ�������ʽ
        /// <summary>
        ///ģ����ѯ�������ʽ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetAnaesthesiaModeLikeID(string p_strInput, out clsAnaesthesiaModeInOperation[] p_objAnaesthesiaModeInOperation)
        {
            string strXML = "";
            int intRows = 0;
            p_objAnaesthesiaModeInOperation = null;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetAnaesthesiaModeLikeID(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInput, out p_objAnaesthesiaModeInOperation);
                #region �ɷ������ѷ���
                //if (m_lngRes >= 0 && intRows > 0)
                //{
                //    p_objAnaesthesiaModeInOperation = new clsAnaesthesiaModeInOperation[intRows];
                //    XmlTextReader objReader = new XmlTextReader(strXML, XmlNodeType.Element, m_objXmlParser);
                //    objReader.WhitespaceHandling = WhitespaceHandling.None;
                //    int intIndex = 0;
                //    while (objReader.Read())
                //    {
                //        switch (objReader.NodeType)
                //        {
                //            case XmlNodeType.Element:
                //                if (objReader.HasAttributes)
                //                {
                //                    p_objAnaesthesiaModeInOperation[intIndex] = new clsAnaesthesiaModeInOperation();
                //                    p_objAnaesthesiaModeInOperation[intIndex].strAnaesthesiaModeID = objReader.GetAttribute("ANAESTHESIAMODEID").ToString().Replace('��', '\'');
                //                    p_objAnaesthesiaModeInOperation[intIndex].strAnaesthesiaModeName = objReader.GetAttribute("ANAESTHESIAMODENAME").ToString().Replace('��', '\'');
                //                    intIndex++;
                //                }
                //                break;
                //        }

                //    }

                //} 
                #endregion
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ���Ҹñ��ڸ��������Ƿ����ظ��ļ�¼
        /// <summary>
        /// ���Ҹñ��ڸ��������Ƿ����ظ��ļ�¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_intRows"></param>
        /// <returns></returns>
        public long m_lngGetCreateDateCount(string p_strInPatientID, string p_strInPatientDate, out int p_intRows)
        {
            p_intRows = 0;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetCreateDateCount(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, out p_intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region �������޸�ʱ��,�޸���
        /// <summary>
        /// �������޸�ʱ��,�޸���
        /// ������ؿգ���ʾ�ü�¼�ѱ�ɾ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <param name="p_strLastModifyUserID"></param>
        /// <returns></returns>
        public long m_lngGetLastModifyDateAndUser(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate, out string p_strLastModifyUserID)
        {
            p_strLastModifyDate = null;
            p_strLastModifyUserID = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetLastModifyDateAndUser(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_strLastModifyDate, out p_strLastModifyUserID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ������ɾ��ʱ��,ɾ����
        /// <summary>
        /// ������ɾ��ʱ��,ɾ����
        /// ������ؿգ���ʾ�ü�¼�ѱ�ɾ��
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDeactivedDate"></param>
        /// <param name="p_strDeactivedUserID"></param>
        /// <returns></returns>
        public long m_lngGetDeactivedDateAndUser(string p_strInPatientID, string p_strInPatientDate, out string p_strDeactivedDate, out string p_strDeactivedUserID)
        {
            p_strDeactivedDate = null;
            p_strDeactivedUserID = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDeactivedDateAndUser(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, out p_strDeactivedDate, out p_strDeactivedUserID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        #region ɾ����¼
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strOperatorID"></param>
        /// <returns></returns>
        public long m_lngDeleteRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strOperatorID)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;
            if (p_strInPatientDate == null || p_strInPatientDate == "")
                return -1;
            if (p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngDeleteRecord(clsLoginContext.s_ObjLoginContext.m_ObjPrincial, p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strOperatorID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
        #endregion

        /// <summary>
        /// ��ӳ����ص����б�
        /// </summary>
        /// <param name="p_strDistrict">����</param>
        /// <returns></returns>
        public long m_lngAddDistrict(string p_strDistrict, string p_strParentID, string p_strType)
        {
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngAddDistrict(p_strDistrict, p_strParentID, p_strType);
                if (m_lngRes == -31)
                    MDIParent.ShowInformationMessageBox("�õ����Ѵ��ڣ�");
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// �޸ĳ����ص����б�
        /// </summary>
        /// <param name="p_strDistrict">����</param>
        /// <returns></returns>
        public long m_lngModifyDistrict(string p_strDistrict, string p_strID)
        {
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngModifyDistrict(p_strDistrict, p_strID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ���ݸ���ID��ȡ�����ص���
        /// </summary>
        /// <param name="p_strParentID">����ID</param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetDistrict(string p_strParentID, ref DataTable dtResult)
        {
            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDistrict(p_strParentID, ref dtResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// ���ݵ�����ȡID
        /// </summary>
        /// <param name="p_strDistrict">����</param>
        /// <param name="p_strType">����</param>
        /// <param name="p_strDisID">ID</param>
        /// <returns></returns>
        public long m_lngGetIDByName(string p_strDistrict, string p_strType, string p_strParentID, out string p_strDisID)
        {
            p_strDisID = "";

            com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ m_objServ =
                (com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.InHospitalMainRecord_XJ.clsInHospitalMainRecordServ_XJ));

            long m_lngRes = 0;
            try
            {
                m_lngRes = m_objServ.m_lngGetDisIDByName(p_strDistrict, p_strType, p_strParentID, out p_strDisID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// �����ݿ������б���Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objCollection"></param>
        /// <returns></returns>
        public long m_lngGetAllInfo(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInHospitalMainRecord_Collection p_objCollection)
        {
            p_objCollection = new clsInHospitalMainRecord_Collection();
            long m_lngRes = 0;
            m_lngRes = m_lngGetMainInfo(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objCollection.m_objMain);
            if (m_lngRes < 1)
            {
                p_objCollection = null;
                return -1;
            }
            m_lngRes = m_lngGetContentInfo(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objCollection.m_objContent);
            if (m_lngRes < 1)
            {
                p_objCollection = null;
                return -1;
            }
            m_lngRes = m_lngGetDiagnosisArr(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objCollection.m_objDiagnosisArr);
            if (m_lngRes < 1)
            {
                p_objCollection = null;
                return -1;
            }
            m_lngRes = m_lngGetOperationArr(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objCollection.m_objOperationArr);
            if (m_lngRes < 1)
            {
                p_objCollection = null;
                return -1;
            }
            m_lngRes = m_lngGetBabyArr(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objCollection.m_objBabyArr);
            if (m_lngRes < 1)
            {
                p_objCollection = null;
                return -1;
            }
            m_lngRes = m_lngGetChemotherapyArr(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objCollection.m_objChemotherapyArr);
            if (m_lngRes < 1)
            {
                p_objCollection = null;
                return -1;
            }
            return m_lngRes;
        }
        #region ��ȡת�����
        /// <summary>
        /// ��ȡת�����
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strInPatientDate">סԺ�ǼǱ��е�סԺ����</param>
        /// <param name="p_strRegisterID">סԺ�ǼǺ�</param>
        /// <param name="p_objDeptInstance">ת�����</param>
        /// <returns></returns>
        public long m_lngGetInHospitalMainTransDeptInstance(string p_strPatientID, string p_strInPatientDate, out string p_strRegisterID, out clsInHospitalMainTransDeptInstance p_objDeptInstance)
        {
            p_strRegisterID = "";
            long lngRes = 0;

            clsPatientManagerService objServ =
                (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));


            lngRes = objServ.m_lngGetRegisterIDByPatient(p_strPatientID, p_strInPatientDate, out p_strRegisterID);

            lngRes = m_lngGetInHospitalMainTransDeptInstance(p_strRegisterID, out p_objDeptInstance);
            //objServ.Dispose();
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetInHospitalMainTransDeptInstance(string p_strRegisterID, out clsInHospitalMainTransDeptInstance p_objDeptInstance)
        {
            long lngRes = 0;

            clsInHospitalMainRecordServ_XJ objServ =
                (clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_XJ));

            lngRes = objServ.m_lngGetInHospitalMainTransDeptInstance(p_strRegisterID, out p_objDeptInstance);
            //objServ.Dispose();
            return lngRes;
        }
        #endregion

        /// <summary>
        /// �޸Ĳ��˻�����Ϣ
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_objPeopleInfo">������Ϣ</param>
        /// <returns></returns>
        public long m_lngSavePatientInfo(string p_strPatientID, string p_strRegisterID, clsPeopleInfo p_objPeopleInfo)
        {
            long lngRes = 0;

            clsInHospitalMainRecordServ_XJ objServ =
                (clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_XJ));

            lngRes = objServ.m_lngUpdateToBsePatient(p_strPatientID, p_objPeopleInfo);
            lngRes = objServ.m_lngUpdateToRegisterDetail(p_strRegisterID, p_objPeopleInfo);

            return lngRes;
        }

        /// <summary>
        /// ��ȡ�㶫����ϵͳ��ICD���
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetICDDiagnosisCode(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            clsDictFromBAServ objServ =
                (clsDictFromBAServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDictFromBAServ));


            lngRes = objServ.m_lngGetICDDiagnosisCode(null, out p_dtbResult);

            return lngRes;
        }

        /// <summary>
        /// ��ȡ�㶫����ϵͳ������ʽ
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetAnaesthesiaMode(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            clsDictFromBAServ objServ =
                (clsDictFromBAServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDictFromBAServ));


            lngRes = objServ.m_lngGetAnaesthesiaMode(null, out p_dtbResult);

            return lngRes;
        }

        /// <summary>
        /// ��ȡ�㶫����ϵͳ��������
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetOprationCode(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            clsDictFromBAServ objServ =
                (clsDictFromBAServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDictFromBAServ));

            lngRes = objServ.m_lngGetOprationCode(null, out p_dtbResult);

            return lngRes;
        }

        #region �ύ���㶫ʡҽԺͳ�Ʋ�������ϵͳ
        /// <summary>
        /// �ύ���㶫ʡҽԺͳ�Ʋ�������ϵͳ
        /// </summary>
        /// <param name="p_objCollection">סԺ������ҳ����</param>
        /// <param name="p_objPeoInfo">���˻�����Ϣ</param>
        /// <param name="p_objTransferDept">ת�Ƽ�¼</param>
        /// <param name="p_strHISInDate">HIS��Ժ����</param>
        /// <returns></returns>
        public long m_lngCommitToGD(clsInHospitalMainRecord_Collection p_objCollection, clsPeopleInfo p_objPeoInfo, clsInHospitalMainTransDeptInstance p_objTransferDept, string p_strHISInDate)
        {
            long lngRes = -1;
            if (p_objCollection == null || p_objCollection.m_objMain == null || p_objCollection.m_objContent == null || p_objPeoInfo == null)
            {
                return -1;
            }

            clsInHospitalMainRecordServ_XJ objServ =
                (clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_XJ));

            lngRes = objServ.m_lngCommitRecord(p_objCollection.m_objMain.m_strInPatientID, Convert.ToDateTime(p_objCollection.m_objMain.m_strInPatientDate), Convert.ToDateTime(p_objCollection.m_objMain.m_strOpenDate));
            if (lngRes < 0)
            {
                return -1;
            }

            int intInTimes = 1;
            lngRes = objServ.m_lngGetInTimes(p_objCollection.m_objMain.m_strInPatientID,
                Convert.ToDateTime(p_objCollection.m_objMain.m_strInPatientDate), out intInTimes);

            lngRes = objServ.m_lngCommitToBA1(p_objCollection.m_objMain.m_strInPatientID, intInTimes, p_objPeoInfo);
            if (lngRes < 0)
            {
                return -1;
            }

            lngRes = objServ.m_lngCommitToBA2(p_objCollection, intInTimes, p_objTransferDept, p_objPeoInfo, p_strHISInDate);

            if (lngRes < 0)
                return -1;
            lngRes = objServ.m_lngCommitToBA3(p_objCollection, intInTimes, p_objPeoInfo.m_StrLastName);
            lngRes = objServ.m_lngCommitToBA4(p_objCollection, intInTimes, p_objPeoInfo.m_StrLastName);
            lngRes = objServ.m_lngCommitToBA5(p_objCollection, intInTimes, p_objPeoInfo.m_StrLastName);
            lngRes = objServ.m_lngCommitToBA6(p_objCollection, intInTimes, p_objPeoInfo.m_StrLastName);
            lngRes = objServ.m_lngCommitToBA7(p_objTransferDept, p_objCollection.m_objMain.m_strInPatientID, intInTimes, p_objPeoInfo.m_StrLastName);
            lngRes = objServ.m_lngCommitToBA9(p_objCollection, intInTimes, p_objPeoInfo.m_StrLastName);
            lngRes = objServ.m_lngCommitToBA10(p_objCollection, intInTimes, p_objPeoInfo.m_StrLastName);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ��ȡ����ͬ������Ϣ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_objRecordArr">����ͬ����Ϣ</param>
        /// <returns></returns>
        public long m_lngGetCHRCATE(string p_strRegisterID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;

            clsInHospitalMainRecordServ_XJ objServ =
                (clsInHospitalMainRecordServ_XJ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ_XJ));

            lngRes = objServ.m_lngGetCHRCATE(null, p_strRegisterID, out p_objRecordArr);

            return lngRes;
        }
        internal void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate, ref clsPrintInfo_InHospitalMainRecord p_objPrintInfo)
        {
            p_objPrintInfo.m_strInPatentID = p_objPatient != null ? p_objPatient.m_StrInPatientID : "";
            p_objPrintInfo.m_strPatientName = p_objPatient != null ? p_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            p_objPrintInfo.m_strSex = p_objPatient != null ? p_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            p_objPrintInfo.m_strAge = p_objPatient != null ? p_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            p_objPrintInfo.m_strBedName = p_objPatient != null ? p_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            p_objPrintInfo.m_strDeptName = p_objPatient != null ? p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            p_objPrintInfo.m_strAreaName = p_objPatient != null ? p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            p_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            p_objPrintInfo.m_strHISInPatientID = p_objPatient != null ? p_objPatient.m_StrHISInPatientID : "";
            p_objPrintInfo.m_dtmHISInPatientDate = p_objPatient != null ? p_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            p_objPrintInfo.m_objPeopleInfo = p_objPatient != null ? p_objPatient.m_ObjPeopleInfo : null;
            if (p_dtmOpenDate == DateTime.MinValue)
            {
                string strOpenDate = null;
                long lngRes = m_lngGetOpenDateInfo(p_objPrintInfo.m_strInPatentID, p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out strOpenDate);
                if (lngRes < 1)
                {
                    return;
                }
                if (strOpenDate == null || strOpenDate == "")
                    p_objPrintInfo.m_dtmOpenDate = DateTime.MinValue;
                else p_objPrintInfo.m_dtmOpenDate = DateTime.Parse(strOpenDate);
            }
            else
                p_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            clsInBedSessionInfo m_objSession = p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_objPrintInfo.m_dtmInPatientDate);
            if (m_objSession == null)
                return;
            int m_intSessionIndex = p_objPatient.m_ObjInBedInfo.m_intGetSessionIndex(m_objSession);
            p_objPrintInfo.m_strTimes = ((int)(m_intSessionIndex + 1)).ToString();//�ڼ���סԺ


        }
    }
}
