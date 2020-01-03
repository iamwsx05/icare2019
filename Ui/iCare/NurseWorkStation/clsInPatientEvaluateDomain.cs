using System;
using System.Xml;
using System.IO;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsInPatientEvaluateDomain.
    /// </summary>
    public class clsInPatientEvaluateDomain
    {
        //private clsInPatientEvaluateServ m_objServ=new clsInPatientEvaluateServ();
        private XmlParserContext m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
        private MemoryStream m_objXmlMemStream;
        private XmlTextWriter m_objXmlWriter;
        public clsInPatientEvaluateDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            m_objXmlMemStream = new MemoryStream(300);
            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
        }
        ///ֻ��Ҫ���ʱ��
        public long m_lngGetAllRecordDateArr(string p_strInPatientID /*,string p_strInPatientDate*/, out string[] p_strDateArr)
        {
            p_strDateArr = null;

            string strXML = "";
            int intRows = 0;
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllRecordDateArr(p_strInPatientID,/*p_strInPatientDate*/out strXML, out intRows);
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

        ///��ȡ������Ϣ
        private clsInPatientEvaluate m_objGetLatestRecord(string p_strXML, int p_intRows, string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_intRows > 0)//p_intRows==1
            {
                clsInPatientEvaluate objclsInPatientEvaluate = new clsInPatientEvaluate();

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
                                objclsInPatientEvaluate.m_strInPatientDate = p_strInPatientDate;
                                objclsInPatientEvaluate.m_strOpenDate = objReader.GetAttribute("OPENDATE");//p_strOpenDate;
                                objclsInPatientEvaluate.m_strCreateDate = objReader.GetAttribute("CREATEDATE");//
                                objclsInPatientEvaluate.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID");

                                objclsInPatientEvaluate.m_strEducation = objReader.GetAttribute("EDUCATION");
                                objclsInPatientEvaluate.m_strReligion = objReader.GetAttribute("RELIGION");
                                objclsInPatientEvaluate.m_strReligionContent = objReader.GetAttribute("RELIGIONCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDataFrom = objReader.GetAttribute("DATAFROM");
                                objclsInPatientEvaluate.m_strDataFromOtherContent = objReader.GetAttribute("DATAFROMOTHERCONTENT").Replace('��', '\'');

                                objclsInPatientEvaluate.m_strInPatientDiagnoseXML = objReader.GetAttribute("INPATIENTDIAGNOSEXML").Replace('��', '\'');

                                #region DataTable��Ӧ���ֶ�XML
                                objclsInPatientEvaluate.m_strInPatientMode_TableXML = objReader.GetAttribute("INPATIENTMODE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAllergicHistory_TableXML = objReader.GetAttribute("ALLERGICHISTORY_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAppetite_TableXML = objReader.GetAttribute("APPETITE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strWeight_TableXML = objReader.GetAttribute("WEIGHT_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strMouth_TableXML = objReader.GetAttribute("MOUTH_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strChaw_TableXML = objReader.GetAttribute("CHAW_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDeglutition_TableXML = objReader.GetAttribute("DEGLUTITION_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStomach_TableXML = objReader.GetAttribute("STOMACH_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSkin_TableXML = objReader.GetAttribute("SKIN_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strChogh_TableXML = objReader.GetAttribute("CHOGH_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPhlegm_TableXML = objReader.GetAttribute("PHLEGM_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDejecta_TableXML = objReader.GetAttribute("DEJECTA_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPee_TableXML = objReader.GetAttribute("PEE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strCanMyself_TableXML = objReader.GetAttribute("CANMYSELF_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBreathUrge_TableXML = objReader.GetAttribute("BREATHURGE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLimbActive_TableXML = objReader.GetAttribute("LIMBACTIVE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSleep_TableXML = objReader.GetAttribute("SLEEP_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAssistantSleep_TableXML = objReader.GetAttribute("ASSISTANTSLEEP_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strShout_TableXML = objReader.GetAttribute("SHOUT_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAnswer_TableXML = objReader.GetAttribute("ANSWER_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSeeingBalk_TableXML = objReader.GetAttribute("SEEINGBALK_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strListenBalk_TableXML = objReader.GetAttribute("LISTENBALK_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAche_TableXML = objReader.GetAttribute("ACHE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLanguage_TableXML = objReader.GetAttribute("LANGUAGE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strInHospitalFeel_TableXML = objReader.GetAttribute("INHOSPITALFEEL_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLookIn_TableXML = objReader.GetAttribute("LOOKIN_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strEconomic_TableXML = objReader.GetAttribute("ECONOMIC_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strIllnessUnderstand_TableXML = objReader.GetAttribute("ILLNESSUNDERSTAND_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDoctorsAdvice_TableXML = objReader.GetAttribute("DOCTORSADVICE_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSmoking_TableXML = objReader.GetAttribute("SMOKING_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDrink_TableXML = objReader.GetAttribute("DRINK_TABLEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFreakOut_TableXML = objReader.GetAttribute("FREAKOUT_TABLEXML").Replace('��', '\'');
                                #endregion

                                objclsInPatientEvaluate.m_strAllergicSourceXML = objReader.GetAttribute("ALLERGICSOURCEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAllergicSymptomXML = objReader.GetAttribute("ALLERGICSYMPTOMXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strHowMuchWeightXML = objReader.GetAttribute("HOWMUCHWEIGHTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStomachColorXML = objReader.GetAttribute("STOMACHCOLORXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStomachCharacterXML = objReader.GetAttribute("STOMACHCHARACTERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStomachTimesXML = objReader.GetAttribute("STOMACHTIMESXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strStomachQtyXML = objReader.GetAttribute("STOMACHQTYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBodyXML = objReader.GetAttribute("BODYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSkinOtherContentXML = objReader.GetAttribute("SKINOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPhlegmColorXML = objReader.GetAttribute("PHLEGMCOLORXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML = objReader.GetAttribute("DEJECTATIMESINONEDAYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML = objReader.GetAttribute("DEJECTAHOWMANYDAYSONCEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDejectaCharacterXML = objReader.GetAttribute("DEJECTACHARACTERXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strPeeOtherContentXML = objReader.GetAttribute("PEEOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strParalysisPartXML = objReader.GetAttribute("PARALYSISPARTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSleepHoursXML = objReader.GetAttribute("SLEEPHOURSXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSleepOtherContentXML = objReader.GetAttribute("SLEEPOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML = objReader.GetAttribute("ASSISTANTSLEEPMEDICINESXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAssistantSleepModelXML = objReader.GetAttribute("ASSISTANTSLEEPMODELXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLeftEyeOtherContentXML = objReader.GetAttribute("LEFTEYEOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strRightEyeOtherContentXML = objReader.GetAttribute("RIGHTEYEOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBothEyeOtherContentXML = objReader.GetAttribute("BOTHEYEOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strLeftListenOtherContentXML = objReader.GetAttribute("LEFTLISTENOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strRightListenOtherContentXML = objReader.GetAttribute("RIGHTLISTENOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strBothListenOtherContentXML = objReader.GetAttribute("BOTHLISTENOTHERCONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAchePartXML = objReader.GetAttribute("ACHEPARTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strAlwayslanguageXML = objReader.GetAttribute("ALWAYSLANGUAGEXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDoctorsAdviceContentXML = objReader.GetAttribute("DOCTORSADVICECONTENTXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSmokingYearsXML = objReader.GetAttribute("SMOKINGYEARSXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strSmokingQtyOneDayXML = objReader.GetAttribute("SMOKINGQTYONEDAYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDrinkYearsXML = objReader.GetAttribute("DRINKYEARSXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strDrinkQtyOneDayXML = objReader.GetAttribute("DRINKQTYONEDAYXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFreakOutMedicinesXML = objReader.GetAttribute("FREAKOUTMEDICINESXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFreakOutYearsXML = objReader.GetAttribute("FREAKOUTYEARSXML").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML = objReader.GetAttribute("FREAKOUTQTYONEDAYXML").Replace('��', '\'');

                                objclsInPatientEvaluate.m_strIfConfirm = objReader.GetAttribute("IFCONFIRM");
                                objclsInPatientEvaluate.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                objclsInPatientEvaluate.m_strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").Replace('��', '\'');

                            }
                            break;
                    }
                }

                return objclsInPatientEvaluate;
            }
            return null;
        }

        ///��ȡ�ӱ���Ϣ
        private clsInPatientEvaluateContent m_objGetLatestRecordContent(string p_strXML, int p_intRows, string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_intRows > 0)//p_intRows==1
            {
                clsInPatientEvaluateContent objclsInPatientEvaluateContent = new clsInPatientEvaluateContent();

                XmlTextReader objReader = new XmlTextReader(p_strXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsInPatientEvaluateContent.m_strInPatientID = p_strInPatientID;
                                objclsInPatientEvaluateContent.m_strInPatientDate = p_strInPatientDate;
                                objclsInPatientEvaluateContent.m_strOpenDate = objReader.GetAttribute("OPENDATE");

                                objclsInPatientEvaluateContent.m_strModifyDate = objReader.GetAttribute("MODIFYDATE");
                                objclsInPatientEvaluateContent.m_strModifyUserID = objReader.GetAttribute("MODIFYUSERID");

                                objclsInPatientEvaluateContent.m_strInPatientDiagnose = objReader.GetAttribute("INPATIENTDIAGNOSE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strInPatientMode = objReader.GetAttribute("INPATIENTMODE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAllergicHistory = objReader.GetAttribute("ALLERGICHISTORY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAllergicSource = objReader.GetAttribute("ALLERGICSOURCE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAllergicSymptom = objReader.GetAttribute("ALLERGICSYMPTOM").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAppetite = objReader.GetAttribute("APPETITE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strWeight = objReader.GetAttribute("WEIGHT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strHowMuchWeight = objReader.GetAttribute("HOWMUCHWEIGHT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strMouth = objReader.GetAttribute("MOUTH").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strChaw = objReader.GetAttribute("CHAW").Replace('��', '\'');

                                objclsInPatientEvaluateContent.m_strDeglutition = objReader.GetAttribute("DEGLUTITION").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachNothing = objReader.GetAttribute("STOMACHNOTHING").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachRise = objReader.GetAttribute("STOMACHRISE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachAche = objReader.GetAttribute("STOMACHACHE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachNaupathia = objReader.GetAttribute("STOMACHNAUPATHIA").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachSpew = objReader.GetAttribute("STOMACHSPEW").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachColor = objReader.GetAttribute("STOMACHCOLOR").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachCharacter = objReader.GetAttribute("STOMACHCHARACTER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachTimes = objReader.GetAttribute("STOMACHTIMES").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strStomachQty = objReader.GetAttribute("STOMACHQTY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinFull = objReader.GetAttribute("SKINFULL").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinPallor = objReader.GetAttribute("SKINPALLOR").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinIcterus = objReader.GetAttribute("SKINICTERUS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinRed = objReader.GetAttribute("SKINRED").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinCyanopathy = objReader.GetAttribute("SKINCYANOPATHY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinDehydrate = objReader.GetAttribute("SKINDEHYDRATE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinAnthema = objReader.GetAttribute("SKINANTHEMA").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinBlood = objReader.GetAttribute("SKINBLOOD").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinSore = objReader.GetAttribute("SKINSORE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinCut = objReader.GetAttribute("SKINCUT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinDropsy = objReader.GetAttribute("SKINDROPSY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBody = objReader.GetAttribute("BODY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strUpperLimbs = objReader.GetAttribute("UPPERLIMBS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLowerLimbs = objReader.GetAttribute("LOWERLIMBS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinOther = objReader.GetAttribute("SKINOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSkinOtherContent = objReader.GetAttribute("SKINOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strChogh = objReader.GetAttribute("CHOGH").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strHavePhlegm = objReader.GetAttribute("HAVEPHLEGM").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPhlegmEasy = objReader.GetAttribute("PHLEGMEASY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPhlegmChroma = objReader.GetAttribute("PHLEGMCHROMA").Replace('��', '\'');

                                objclsInPatientEvaluateContent.m_strPhlegmColor = objReader.GetAttribute("PHLEGMCOLOR").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDejectaTimesInOneDay = objReader.GetAttribute("DEJECTATIMESINONEDAY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDejectaHowManyDaysOnce = objReader.GetAttribute("DEJECTAHOWMANYDAYSONCE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDejectaIrretention = objReader.GetAttribute("DEJECTAIRRETENTION").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDejectaFistula = objReader.GetAttribute("DEJECTAFISTULA").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDejectaCharacter = objReader.GetAttribute("DEJECTACHARACTER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeIrretention = objReader.GetAttribute("PEEIRRETENTION").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeNatural = objReader.GetAttribute("PEENATURAL").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeUraemia = objReader.GetAttribute("PEEURAEMIA").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeMuch = objReader.GetAttribute("PEEMUCH").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeBlood = objReader.GetAttribute("PEEBLOOD").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeCyst = objReader.GetAttribute("PEECYST").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeePipe = objReader.GetAttribute("PEEPIPE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeOther = objReader.GetAttribute("PEEOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strPeeOtherContent = objReader.GetAttribute("PEEOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strCanMyself = objReader.GetAttribute("CANMYSELF").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBreathUrge = objReader.GetAttribute("BREATHURGE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLimbActive = objReader.GetAttribute("LIMBACTIVE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strParalysis = objReader.GetAttribute("PARALYSIS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strParalysisPart = objReader.GetAttribute("PARALYSISPART").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepHours = objReader.GetAttribute("SLEEPHOURS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepNothing = objReader.GetAttribute("SLEEPNOTHING").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepDifficulty = objReader.GetAttribute("SLEEPDIFFICULTY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepWakeEasy = objReader.GetAttribute("SLEEPWAKEEASY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepWakeEarly = objReader.GetAttribute("SLEEPWAKEEARLY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepDreamMuch = objReader.GetAttribute("SLEEPDREAMMUCH").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepOther = objReader.GetAttribute("SLEEPOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSleepOtherContent = objReader.GetAttribute("SLEEPOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAssistantSleep = objReader.GetAttribute("ASSISTANTSLEEP").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAssistantSleepMedicines = objReader.GetAttribute("ASSISTANTSLEEPMEDICINES").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAssistantSleepModel = objReader.GetAttribute("ASSISTANTSLEEPMODEL").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strShout = objReader.GetAttribute("SHOUT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAnswer = objReader.GetAttribute("ANSWER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSeeingBalk = objReader.GetAttribute("SEEINGBALK").Replace('��', '\'');
                                //								objclsInPatientEvaluateContent.m_strLeftRightEye= objReader.GetAttribute("LEFTRIGHTEYE").Replace('��','\'');	
                                objclsInPatientEvaluateContent.m_strLeftEyeDown = objReader.GetAttribute("LEFTEYEDOWN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftEyeBlur = objReader.GetAttribute("LEFTEYEBLUR").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftEyeAgain = objReader.GetAttribute("LEFTEYEAGAIN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftEyeOther = objReader.GetAttribute("LEFTEYEOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftEyeOtherContent = objReader.GetAttribute("LEFTEYEOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightEyeDown = objReader.GetAttribute("RIGHTEYEDOWN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightEyeBlur = objReader.GetAttribute("RIGHTEYEBLUR").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightEyeAgain = objReader.GetAttribute("RIGHTEYEAGAIN").Replace('��', '\'');

                                objclsInPatientEvaluateContent.m_strRightEyeOther = objReader.GetAttribute("RIGHTEYEOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightEyeOtherContent = objReader.GetAttribute("RIGHTEYEOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothEyeDown = objReader.GetAttribute("BOTHEYEDOWN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothEyeBlur = objReader.GetAttribute("BOTHEYEBLUR").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothEyeAgain = objReader.GetAttribute("BOTHEYEAGAIN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothEyeOther = objReader.GetAttribute("BOTHEYEOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothEyeOtherContent = objReader.GetAttribute("BOTHEYEOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strListenBalk = objReader.GetAttribute("LISTENBALK").Replace('��', '\'');
                                //								objclsInPatientEvaluateContent.m_strLeftRightListen= objReader.GetAttribute("LEFTRIGHTLISTEN").Replace('��','\'');	
                                objclsInPatientEvaluateContent.m_strLeftListenDown = objReader.GetAttribute("LEFTLISTENDOWN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftListenTinnitus = objReader.GetAttribute("LEFTLISTENTINNITUS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftListenAgain = objReader.GetAttribute("LEFTLISTENAGAIN").Replace('��', '\'');

                                objclsInPatientEvaluateContent.m_strLeftListenOther = objReader.GetAttribute("LEFTLISTENOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLeftListenOtherContent = objReader.GetAttribute("LEFTLISTENOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightListenDown = objReader.GetAttribute("RIGHTLISTENDOWN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightListenTinnitus = objReader.GetAttribute("RIGHTLISTENTINNITUS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightListenAgain = objReader.GetAttribute("RIGHTLISTENAGAIN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightListenOther = objReader.GetAttribute("RIGHTLISTENOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strRightListenOtherContent = objReader.GetAttribute("RIGHTLISTENOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothListenDown = objReader.GetAttribute("BOTHLISTENDOWN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothListenTinnitus = objReader.GetAttribute("BOTHLISTENTINNITUS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothListenAgain = objReader.GetAttribute("BOTHLISTENAGAIN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothListenOther = objReader.GetAttribute("BOTHLISTENOTHER").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strBothListenOtherContent = objReader.GetAttribute("BOTHLISTENOTHERCONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAche = objReader.GetAttribute("ACHE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAchePart = objReader.GetAttribute("ACHEPART").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAcheTimes = objReader.GetAttribute("ACHETIMES").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAlwayslanguage = objReader.GetAttribute("ALWAYSLANGUAGE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strMandarin = objReader.GetAttribute("MANDARIN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strCantSay = objReader.GetAttribute("CANTSAY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strInHospitalFeel = objReader.GetAttribute("INHOSPITALFEEL").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strLookIn = objReader.GetAttribute("LOOKIN").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strEconomic = objReader.GetAttribute("ECONOMIC").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strIllnessUnderstand = objReader.GetAttribute("ILLNESSUNDERSTAND").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDoctorsAdvice = objReader.GetAttribute("DOCTORSADVICE").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDoctorsAdviceContent = objReader.GetAttribute("DOCTORSADVICECONTENT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strIfSmoking = objReader.GetAttribute("IFSMOKING").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSmokingYears = objReader.GetAttribute("SMOKINGYEARS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strSmokingQtyOneDay = objReader.GetAttribute("SMOKINGQTYONEDAY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strIfDrink = objReader.GetAttribute("IFDRINK").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDrinkYears = objReader.GetAttribute("DRINKYEARS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strDrinkQtyOneDay = objReader.GetAttribute("DRINKQTYONEDAY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strFreakOut = objReader.GetAttribute("FREAKOUT").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strFreakOutMedicines = objReader.GetAttribute("FREAKOUTMEDICINES").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strFreakOutYears = objReader.GetAttribute("FREAKOUTYEARS").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strFreakOutQtyOneDay = objReader.GetAttribute("FREAKOUTQTYONEDAY").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strAbonrmalDesc = objReader.GetAttribute("ABONRMALDESC").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strChargeNurseID = objReader.GetAttribute("CHARGENURSEID").Replace('��', '\'');
                                objclsInPatientEvaluateContent.m_strCheckTime = objReader.GetAttribute("CHECKTIME").Replace('��', '\'');

                            }
                            break;
                    }
                }
                return objclsInPatientEvaluateContent;
            }
            return null;
        }

        public long m_lngGetLatestRecord_All(string p_strInPatientID, string p_strInPatientDate, out clsInPatientEvaluate_All p_objclsInPatientEvaluate_All)
        {
            p_objclsInPatientEvaluate_All = null;

            string strXML = "";
            int intRows = 0;
            string strContentXML = "";
            int intContentRows = 0;
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLatestRecord_All(p_strInPatientID, p_strInPatientDate/*,p_strOpenDate*/, out strXML, out intRows, out strContentXML, out intContentRows);
                if (lngResult > 0)
                {
                    p_objclsInPatientEvaluate_All = new clsInPatientEvaluate_All();
                    p_objclsInPatientEvaluate_All.m_objclsInPatientEvaluate = m_objGetLatestRecord(strXML, intRows, p_strInPatientID, p_strInPatientDate);
                    p_objclsInPatientEvaluate_All.m_objclsInPatientEvaluateContent = m_objGetLatestRecordContent(strContentXML, intContentRows, p_strInPatientID, p_strInPatientDate);
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        public long m_lngGetLatestDeleteRecord_All(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out clsInPatientEvaluate_All p_objclsInPatientEvaluate_All)
        {
            p_objclsInPatientEvaluate_All = null;

            string strXML = "";
            int intRows = 0;
            string strContentXML = "";
            int intContentRows = 0;
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetLatestDeleteRecord_All(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out strXML, out intRows, out strContentXML, out intContentRows);
                if (lngResult > 0)
                {
                    p_objclsInPatientEvaluate_All = new clsInPatientEvaluate_All();
                    p_objclsInPatientEvaluate_All.m_objclsInPatientEvaluate = m_objGetLatestRecord(strXML, intRows, p_strInPatientID, p_strInPatientDate);
                    p_objclsInPatientEvaluate_All.m_objclsInPatientEvaluateContent = m_objGetLatestRecordContent(strContentXML, intContentRows, p_strInPatientID, p_strInPatientDate);
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        public long m_lngGetAbnormalInfo(string p_strInPatientID, string p_strInPatientDate, out string p_strAbnormalInfo)
        {
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAbnormalInfo(p_strInPatientID, p_strInPatientDate, out p_strAbnormalInfo);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        /// ������Ϣ		
        public long m_lngSave(clsInPatientEvaluate p_objclsInPatientEvaluate, clsInPatientEvaluateContent p_objclsInPatientEvaluateContent, bool p_blnIsAddNew)
        {
            //			//�Ѱ�ɫ��Ϊ��ɫ
            //			clsXML_DataGrid objclsXML_DataGrid=new clsXML_DataGrid();
            //			p_objclsInPatientEvaluate.m_strAche_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAche_TableXML);
            //			p_objclsInPatientEvaluate.m_strAchePartXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAchePartXML);
            //			p_objclsInPatientEvaluate.m_strAllergicHistory_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAllergicHistory_TableXML);
            //			p_objclsInPatientEvaluate.m_strAllergicSourceXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAllergicSourceXML);
            //			p_objclsInPatientEvaluate.m_strAllergicSymptomXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAllergicSymptomXML);
            //			p_objclsInPatientEvaluate.m_strAlwayslanguageXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAlwayslanguageXML);
            //			p_objclsInPatientEvaluate.m_strAnswer_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAnswer_TableXML);
            //			p_objclsInPatientEvaluate.m_strAppetite_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAppetite_TableXML);
            //			p_objclsInPatientEvaluate.m_strAssistantSleep_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAssistantSleep_TableXML);
            //			p_objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML);
            //			p_objclsInPatientEvaluate.m_strAssistantSleepModelXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAssistantSleepModelXML);
            //			p_objclsInPatientEvaluate.m_strBodyXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBodyXML);
            //			p_objclsInPatientEvaluate.m_strBothEyeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBothEyeOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strBothListenOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBothListenOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strBreathUrge_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBreathUrge_TableXML);
            //			p_objclsInPatientEvaluate.m_strCanMyself_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strCanMyself_TableXML);
            //			p_objclsInPatientEvaluate.m_strChaw_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strChaw_TableXML);
            //			p_objclsInPatientEvaluate.m_strChogh_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strChogh_TableXML);
            //			p_objclsInPatientEvaluate.m_strConfirmReasonXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strConfirmReasonXML);
            //			p_objclsInPatientEvaluate.m_strDeglutition_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDeglutition_TableXML);
            //			p_objclsInPatientEvaluate.m_strDejecta_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejecta_TableXML);
            //			p_objclsInPatientEvaluate.m_strDejectaCharacterXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejectaCharacterXML);
            //			p_objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML);
            //			p_objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML);
            //			p_objclsInPatientEvaluate.m_strDoctorsAdvice_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDoctorsAdvice_TableXML);
            //			p_objclsInPatientEvaluate.m_strDoctorsAdviceContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDoctorsAdviceContentXML);
            //			p_objclsInPatientEvaluate.m_strDrink_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDrink_TableXML);
            //			p_objclsInPatientEvaluate.m_strDrinkQtyOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDrinkQtyOneDayXML);
            //			p_objclsInPatientEvaluate.m_strDrinkYearsXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDrinkYearsXML);
            //			p_objclsInPatientEvaluate.m_strEconomic_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strEconomic_TableXML);
            //			p_objclsInPatientEvaluate.m_strFreakOut_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOut_TableXML);
            //			p_objclsInPatientEvaluate.m_strFreakOutMedicinesXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOutMedicinesXML);
            //			p_objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML);
            //			p_objclsInPatientEvaluate.m_strFreakOutYearsXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOutYearsXML);
            //			p_objclsInPatientEvaluate.m_strHowMuchWeightXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strHowMuchWeightXML);
            //			p_objclsInPatientEvaluate.m_strIllnessUnderstand_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strIllnessUnderstand_TableXML);
            //			p_objclsInPatientEvaluate.m_strInHospitalFeel_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strInHospitalFeel_TableXML);
            //			p_objclsInPatientEvaluate.m_strInPatientDiagnoseXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strInPatientDiagnoseXML);
            //			p_objclsInPatientEvaluate.m_strInPatientMode_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strInPatientMode_TableXML);
            //			p_objclsInPatientEvaluate.m_strLanguage_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLanguage_TableXML);
            //			p_objclsInPatientEvaluate.m_strLeftEyeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLeftEyeOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strLeftListenOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLeftListenOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strLimbActive_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLimbActive_TableXML);
            //			p_objclsInPatientEvaluate.m_strListenBalk_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strListenBalk_TableXML);
            //			p_objclsInPatientEvaluate.m_strLookIn_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLookIn_TableXML);
            //			p_objclsInPatientEvaluate.m_strMouth_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strMouth_TableXML);
            //			p_objclsInPatientEvaluate.m_strParalysisPartXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strParalysisPartXML);
            //			p_objclsInPatientEvaluate.m_strPee_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPee_TableXML);
            //			p_objclsInPatientEvaluate.m_strPeeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPeeOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strPhlegm_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPhlegm_TableXML);
            //			p_objclsInPatientEvaluate.m_strPhlegmColorXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPhlegmColorXML);
            //			p_objclsInPatientEvaluate.m_strRightEyeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strRightEyeOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strRightListenOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strRightListenOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strSeeingBalk_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSeeingBalk_TableXML);
            //			p_objclsInPatientEvaluate.m_strShout_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strShout_TableXML);
            //			p_objclsInPatientEvaluate.m_strSkin_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSkin_TableXML);
            //			p_objclsInPatientEvaluate.m_strSkinOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSkinOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strSleep_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSleep_TableXML);
            //			p_objclsInPatientEvaluate.m_strSleepHoursXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSleepHoursXML);
            //			p_objclsInPatientEvaluate.m_strSleepOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSleepOtherContentXML);
            //			p_objclsInPatientEvaluate.m_strSmoking_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSmoking_TableXML);
            //			p_objclsInPatientEvaluate.m_strSmokingQtyOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSmokingQtyOneDayXML);
            //			p_objclsInPatientEvaluate.m_strSmokingYearsXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSmokingYearsXML);
            //			p_objclsInPatientEvaluate.m_strStomach_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomach_TableXML);
            //			p_objclsInPatientEvaluate.m_strStomachCharacterXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachCharacterXML);
            //			p_objclsInPatientEvaluate.m_strStomachColorXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachColorXML);
            //			p_objclsInPatientEvaluate.m_strStomachQtyXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachQtyXML);
            //			p_objclsInPatientEvaluate.m_strStomachTimesXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachTimesXML);
            //			p_objclsInPatientEvaluate.m_strWeight_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strWeight_TableXML);
            //			
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                if (p_blnIsAddNew == true)
                    lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsInPatientEvaluateServ_m_lngAddNew(m_strGetMainXML(p_objclsInPatientEvaluate), m_strGetSubXML(p_objclsInPatientEvaluateContent));
                else
                    lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsInPatientEvaluateServ_m_lngModify(m_strGetMainXML(p_objclsInPatientEvaluate), m_strGetSubXML(p_objclsInPatientEvaluateContent));
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        private string m_strGetMainXML(clsInPatientEvaluate p_objclsInPatientEvaluate)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsInPatientEvaluate.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsInPatientEvaluate.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objclsInPatientEvaluate.m_strOpenDate);
            m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objclsInPatientEvaluate.m_strCreateDate);
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objclsInPatientEvaluate.m_strCreateUserID);

            #region DataTable��Ӧ���ֶ�XML
            m_objXmlWriter.WriteAttributeString("INPATIENTMODE_TABLEXML", p_objclsInPatientEvaluate.m_strInPatientMode_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLERGICHISTORY_TABLEXML", p_objclsInPatientEvaluate.m_strAllergicHistory_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("APPETITE_TABLEXML", p_objclsInPatientEvaluate.m_strAppetite_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIGHT_TABLEXML", p_objclsInPatientEvaluate.m_strWeight_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MOUTH_TABLEXML", p_objclsInPatientEvaluate.m_strMouth_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHAW_TABLEXML", p_objclsInPatientEvaluate.m_strChaw_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEGLUTITION_TABLEXML", p_objclsInPatientEvaluate.m_strDeglutition_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACH_TABLEXML", p_objclsInPatientEvaluate.m_strStomach_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKIN_TABLEXML", p_objclsInPatientEvaluate.m_strSkin_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHOGH_TABLEXML", p_objclsInPatientEvaluate.m_strChogh_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHLEGM_TABLEXML", p_objclsInPatientEvaluate.m_strPhlegm_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTA_TABLEXML", p_objclsInPatientEvaluate.m_strDejecta_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEE_TABLEXML", p_objclsInPatientEvaluate.m_strPee_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CANMYSELF_TABLEXML", p_objclsInPatientEvaluate.m_strCanMyself_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BREATHURGE_TABLEXML", p_objclsInPatientEvaluate.m_strBreathUrge_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LIMBACTIVE_TABLEXML", p_objclsInPatientEvaluate.m_strLimbActive_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEP_TABLEXML", p_objclsInPatientEvaluate.m_strSleep_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSISTANTSLEEP_TABLEXML", p_objclsInPatientEvaluate.m_strAssistantSleep_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHOUT_TABLEXML", p_objclsInPatientEvaluate.m_strShout_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANSWER_TABLEXML", p_objclsInPatientEvaluate.m_strAnswer_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SEEINGBALK_TABLEXML", p_objclsInPatientEvaluate.m_strSeeingBalk_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LISTENBALK_TABLEXML", p_objclsInPatientEvaluate.m_strListenBalk_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACHE_TABLEXML", p_objclsInPatientEvaluate.m_strAche_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LANGUAGE_TABLEXML", p_objclsInPatientEvaluate.m_strLanguage_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INHOSPITALFEEL_TABLEXML", p_objclsInPatientEvaluate.m_strInHospitalFeel_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LOOKIN_TABLEXML", p_objclsInPatientEvaluate.m_strLookIn_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ECONOMIC_TABLEXML", p_objclsInPatientEvaluate.m_strEconomic_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ILLNESSUNDERSTAND_TABLEXML", p_objclsInPatientEvaluate.m_strIllnessUnderstand_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOCTORSADVICE_TABLEXML", p_objclsInPatientEvaluate.m_strDoctorsAdvice_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SMOKING_TABLEXML", p_objclsInPatientEvaluate.m_strSmoking_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DRINK_TABLEXML", p_objclsInPatientEvaluate.m_strDrink_TableXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUT_TABLEXML", p_objclsInPatientEvaluate.m_strFreakOut_TableXML.Replace('\'', '��'));
            #endregion

            m_objXmlWriter.WriteAttributeString("EDUCATION", p_objclsInPatientEvaluate.m_strEducation);
            m_objXmlWriter.WriteAttributeString("RELIGION", p_objclsInPatientEvaluate.m_strReligion);
            m_objXmlWriter.WriteAttributeString("RELIGIONCONTENT", p_objclsInPatientEvaluate.m_strReligionContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DATAFROM", p_objclsInPatientEvaluate.m_strDataFrom);
            m_objXmlWriter.WriteAttributeString("DATAFROMOTHERCONTENT", p_objclsInPatientEvaluate.m_strDataFromOtherContent.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("INPATIENTDIAGNOSEXML", p_objclsInPatientEvaluate.m_strInPatientDiagnoseXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ALLERGICSOURCEXML", p_objclsInPatientEvaluate.m_strAllergicSourceXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLERGICSYMPTOMXML", p_objclsInPatientEvaluate.m_strAllergicSymptomXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HOWMUCHWEIGHTXML", p_objclsInPatientEvaluate.m_strHowMuchWeightXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHCOLORXML", p_objclsInPatientEvaluate.m_strStomachColorXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHCHARACTERXML", p_objclsInPatientEvaluate.m_strStomachCharacterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHTIMESXML", p_objclsInPatientEvaluate.m_strStomachTimesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHQTYXML", p_objclsInPatientEvaluate.m_strStomachQtyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BODYXML", p_objclsInPatientEvaluate.m_strBodyXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strSkinOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHLEGMCOLORXML", p_objclsInPatientEvaluate.m_strPhlegmColorXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTATIMESINONEDAYXML", p_objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTAHOWMANYDAYSONCEXML", p_objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTACHARACTERXML", p_objclsInPatientEvaluate.m_strDejectaCharacterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strPeeOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PARALYSISPARTXML", p_objclsInPatientEvaluate.m_strParalysisPartXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPHOURSXML", p_objclsInPatientEvaluate.m_strSleepHoursXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strSleepOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSISTANTSLEEPMEDICINESXML", p_objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSISTANTSLEEPMODELXML", p_objclsInPatientEvaluate.m_strAssistantSleepModelXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTEYEOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strLeftEyeOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTEYEOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strRightEyeOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHEYEOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strBothEyeOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTLISTENOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strLeftListenOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTLISTENOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strRightListenOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHLISTENOTHERCONTENTXML", p_objclsInPatientEvaluate.m_strBothListenOtherContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACHEPARTXML", p_objclsInPatientEvaluate.m_strAchePartXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALWAYSLANGUAGEXML", p_objclsInPatientEvaluate.m_strAlwayslanguageXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOCTORSADVICECONTENTXML", p_objclsInPatientEvaluate.m_strDoctorsAdviceContentXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SMOKINGYEARSXML", p_objclsInPatientEvaluate.m_strSmokingYearsXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SMOKINGQTYONEDAYXML", p_objclsInPatientEvaluate.m_strSmokingQtyOneDayXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DRINKYEARSXML", p_objclsInPatientEvaluate.m_strDrinkYearsXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DRINKQTYONEDAYXML", p_objclsInPatientEvaluate.m_strDrinkQtyOneDayXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUTMEDICINESXML", p_objclsInPatientEvaluate.m_strFreakOutMedicinesXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUTYEARSXML", p_objclsInPatientEvaluate.m_strFreakOutYearsXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUTQTYONEDAYXML", p_objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("IFCONFIRM", "0");//p_objclsInPatientEvaluate.m_strIfConfirm);
                                                                  //���ޣ�����			m_objXmlWriter.WriteAttributeString("CONFIRMREASON", p_objclsInPatientEvaluate.m_strConfirmReason.Replace('\'','��'));
                                                                  //���ޣ�����			m_objXmlWriter.WriteAttributeString("CONFIRMREASONXML", p_objclsInPatientEvaluate.m_strConfirmReasonXML.Replace('\'','��'));

            m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        private string m_strGetSubXML(clsInPatientEvaluateContent p_objclsInPatientEvaluateContent)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("Model");
            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objclsInPatientEvaluateContent.m_strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objclsInPatientEvaluateContent.m_strInPatientDate);
            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objclsInPatientEvaluateContent.m_strOpenDate);
            m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objclsInPatientEvaluateContent.m_strModifyDate);
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", p_objclsInPatientEvaluateContent.m_strModifyUserID);

            m_objXmlWriter.WriteAttributeString("INPATIENTDIAGNOSE", p_objclsInPatientEvaluateContent.m_strInPatientDiagnose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTMODE", p_objclsInPatientEvaluateContent.m_strInPatientMode.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ALLERGICHISTORY", p_objclsInPatientEvaluateContent.m_strAllergicHistory.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLERGICSOURCE", p_objclsInPatientEvaluateContent.m_strAllergicSource.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLERGICSYMPTOM", p_objclsInPatientEvaluateContent.m_strAllergicSymptom.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("APPETITE", p_objclsInPatientEvaluateContent.m_strAppetite.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIGHT", p_objclsInPatientEvaluateContent.m_strWeight.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HOWMUCHWEIGHT", p_objclsInPatientEvaluateContent.m_strHowMuchWeight.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MOUTH", p_objclsInPatientEvaluateContent.m_strMouth.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHAW", p_objclsInPatientEvaluateContent.m_strChaw.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEGLUTITION", p_objclsInPatientEvaluateContent.m_strDeglutition.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHNOTHING", p_objclsInPatientEvaluateContent.m_strStomachNothing.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHRISE", p_objclsInPatientEvaluateContent.m_strStomachRise.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHACHE", p_objclsInPatientEvaluateContent.m_strStomachAche.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHNAUPATHIA", p_objclsInPatientEvaluateContent.m_strStomachNaupathia.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHSPEW", p_objclsInPatientEvaluateContent.m_strStomachSpew.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHCOLOR", p_objclsInPatientEvaluateContent.m_strStomachColor.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHCHARACTER", p_objclsInPatientEvaluateContent.m_strStomachCharacter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHTIMES", p_objclsInPatientEvaluateContent.m_strStomachTimes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHQTY", p_objclsInPatientEvaluateContent.m_strStomachQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINFULL", p_objclsInPatientEvaluateContent.m_strSkinFull.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINPALLOR", p_objclsInPatientEvaluateContent.m_strSkinPallor.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINICTERUS", p_objclsInPatientEvaluateContent.m_strSkinIcterus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINRED", p_objclsInPatientEvaluateContent.m_strSkinRed.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINCYANOPATHY", p_objclsInPatientEvaluateContent.m_strSkinCyanopathy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINDEHYDRATE", p_objclsInPatientEvaluateContent.m_strSkinDehydrate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTHEMA", p_objclsInPatientEvaluateContent.m_strSkinAnthema.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINBLOOD", p_objclsInPatientEvaluateContent.m_strSkinBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINSORE", p_objclsInPatientEvaluateContent.m_strSkinSore.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINCUT", p_objclsInPatientEvaluateContent.m_strSkinCut.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINDROPSY", p_objclsInPatientEvaluateContent.m_strSkinDropsy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BODY", p_objclsInPatientEvaluateContent.m_strBody.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPPERLIMBS", p_objclsInPatientEvaluateContent.m_strUpperLimbs.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LOWERLIMBS", p_objclsInPatientEvaluateContent.m_strLowerLimbs.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINOTHER", p_objclsInPatientEvaluateContent.m_strSkinOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strSkinOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHOGH", p_objclsInPatientEvaluateContent.m_strChogh.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEPHLEGM", p_objclsInPatientEvaluateContent.m_strHavePhlegm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHLEGMEASY", p_objclsInPatientEvaluateContent.m_strPhlegmEasy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHLEGMCHROMA", p_objclsInPatientEvaluateContent.m_strPhlegmChroma.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PHLEGMCOLOR", p_objclsInPatientEvaluateContent.m_strPhlegmColor.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTATIMESINONEDAY", p_objclsInPatientEvaluateContent.m_strDejectaTimesInOneDay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTAHOWMANYDAYSONCE", p_objclsInPatientEvaluateContent.m_strDejectaHowManyDaysOnce.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTAIRRETENTION", p_objclsInPatientEvaluateContent.m_strDejectaIrretention.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTAFISTULA", p_objclsInPatientEvaluateContent.m_strDejectaFistula.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DEJECTACHARACTER", p_objclsInPatientEvaluateContent.m_strDejectaCharacter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEIRRETENTION", p_objclsInPatientEvaluateContent.m_strPeeIrretention.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEENATURAL", p_objclsInPatientEvaluateContent.m_strPeeNatural.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEURAEMIA", p_objclsInPatientEvaluateContent.m_strPeeUraemia.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEMUCH", p_objclsInPatientEvaluateContent.m_strPeeMuch.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEBLOOD", p_objclsInPatientEvaluateContent.m_strPeeBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEECYST", p_objclsInPatientEvaluateContent.m_strPeeCyst.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEPIPE", p_objclsInPatientEvaluateContent.m_strPeePipe.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEOTHER", p_objclsInPatientEvaluateContent.m_strPeeOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strPeeOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CANMYSELF", p_objclsInPatientEvaluateContent.m_strCanMyself.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BREATHURGE", p_objclsInPatientEvaluateContent.m_strBreathUrge.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LIMBACTIVE", p_objclsInPatientEvaluateContent.m_strLimbActive.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PARALYSIS", p_objclsInPatientEvaluateContent.m_strParalysis.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PARALYSISPART", p_objclsInPatientEvaluateContent.m_strParalysisPart.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPHOURS", p_objclsInPatientEvaluateContent.m_strSleepHours.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPNOTHING", p_objclsInPatientEvaluateContent.m_strSleepNothing.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPDIFFICULTY", p_objclsInPatientEvaluateContent.m_strSleepDifficulty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPWAKEEASY", p_objclsInPatientEvaluateContent.m_strSleepWakeEasy.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPWAKEEARLY", p_objclsInPatientEvaluateContent.m_strSleepWakeEarly.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPDREAMMUCH", p_objclsInPatientEvaluateContent.m_strSleepDreamMuch.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPOTHER", p_objclsInPatientEvaluateContent.m_strSleepOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SLEEPOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strSleepOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSISTANTSLEEP", p_objclsInPatientEvaluateContent.m_strAssistantSleep.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSISTANTSLEEPMEDICINES", p_objclsInPatientEvaluateContent.m_strAssistantSleepMedicines.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ASSISTANTSLEEPMODEL", p_objclsInPatientEvaluateContent.m_strAssistantSleepModel.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHOUT", p_objclsInPatientEvaluateContent.m_strShout.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANSWER", p_objclsInPatientEvaluateContent.m_strAnswer.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SEEINGBALK", p_objclsInPatientEvaluateContent.m_strSeeingBalk.Replace('\'', '��'));
            //			m_objXmlWriter.WriteAttributeString("LEFTRIGHTEYE", p_objclsInPatientEvaluateContent.m_strLeftRightEye.Replace('\'','��'));
            m_objXmlWriter.WriteAttributeString("LEFTEYEDOWN", p_objclsInPatientEvaluateContent.m_strLeftEyeDown.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTEYEBLUR", p_objclsInPatientEvaluateContent.m_strLeftEyeBlur.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTEYEAGAIN", p_objclsInPatientEvaluateContent.m_strLeftEyeAgain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTEYEOTHER", p_objclsInPatientEvaluateContent.m_strLeftEyeOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTEYEOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strLeftEyeOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTEYEDOWN", p_objclsInPatientEvaluateContent.m_strRightEyeDown.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTEYEBLUR", p_objclsInPatientEvaluateContent.m_strRightEyeBlur.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTEYEAGAIN", p_objclsInPatientEvaluateContent.m_strRightEyeAgain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTEYEOTHER", p_objclsInPatientEvaluateContent.m_strRightEyeOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTEYEOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strRightEyeOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHEYEDOWN", p_objclsInPatientEvaluateContent.m_strBothEyeDown.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHEYEBLUR", p_objclsInPatientEvaluateContent.m_strBothEyeBlur.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHEYEAGAIN", p_objclsInPatientEvaluateContent.m_strBothEyeAgain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHEYEOTHER", p_objclsInPatientEvaluateContent.m_strBothEyeOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHEYEOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strBothEyeOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LISTENBALK", p_objclsInPatientEvaluateContent.m_strListenBalk.Replace('\'', '��'));
            //			m_objXmlWriter.WriteAttributeString("LEFTRIGHTLISTEN", p_objclsInPatientEvaluateContent.m_strLeftRightListen.Replace('\'','��'));
            m_objXmlWriter.WriteAttributeString("LEFTLISTENDOWN", p_objclsInPatientEvaluateContent.m_strLeftListenDown.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTLISTENTINNITUS", p_objclsInPatientEvaluateContent.m_strLeftListenTinnitus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTLISTENAGAIN", p_objclsInPatientEvaluateContent.m_strLeftListenAgain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTLISTENOTHER", p_objclsInPatientEvaluateContent.m_strLeftListenOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEFTLISTENOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strLeftListenOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTLISTENDOWN", p_objclsInPatientEvaluateContent.m_strRightListenDown.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTLISTENTINNITUS", p_objclsInPatientEvaluateContent.m_strRightListenTinnitus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTLISTENAGAIN", p_objclsInPatientEvaluateContent.m_strRightListenAgain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTLISTENOTHER", p_objclsInPatientEvaluateContent.m_strRightListenOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RIGHTLISTENOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strRightListenOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHLISTENDOWN", p_objclsInPatientEvaluateContent.m_strBothListenDown.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHLISTENTINNITUS", p_objclsInPatientEvaluateContent.m_strBothListenTinnitus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHLISTENAGAIN", p_objclsInPatientEvaluateContent.m_strBothListenAgain.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHLISTENOTHER", p_objclsInPatientEvaluateContent.m_strBothListenOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BOTHLISTENOTHERCONTENT", p_objclsInPatientEvaluateContent.m_strBothListenOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACHE", p_objclsInPatientEvaluateContent.m_strAche.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACHEPART", p_objclsInPatientEvaluateContent.m_strAchePart.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ACHETIMES", p_objclsInPatientEvaluateContent.m_strAcheTimes.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALWAYSLANGUAGE", p_objclsInPatientEvaluateContent.m_strAlwayslanguage.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MANDARIN", p_objclsInPatientEvaluateContent.m_strMandarin.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CANTSAY", p_objclsInPatientEvaluateContent.m_strCantSay.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("INHOSPITALFEEL", p_objclsInPatientEvaluateContent.m_strInHospitalFeel.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LOOKIN", p_objclsInPatientEvaluateContent.m_strLookIn.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ECONOMIC", p_objclsInPatientEvaluateContent.m_strEconomic.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ILLNESSUNDERSTAND", p_objclsInPatientEvaluateContent.m_strIllnessUnderstand.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOCTORSADVICE", p_objclsInPatientEvaluateContent.m_strDoctorsAdvice.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOCTORSADVICECONTENT", p_objclsInPatientEvaluateContent.m_strDoctorsAdviceContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IFSMOKING", p_objclsInPatientEvaluateContent.m_strIfSmoking.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SMOKINGYEARS", p_objclsInPatientEvaluateContent.m_strSmokingYears.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SMOKINGQTYONEDAY", p_objclsInPatientEvaluateContent.m_strSmokingQtyOneDay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IFDRINK", p_objclsInPatientEvaluateContent.m_strIfDrink.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DRINKYEARS", p_objclsInPatientEvaluateContent.m_strDrinkYears.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DRINKQTYONEDAY", p_objclsInPatientEvaluateContent.m_strDrinkQtyOneDay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUT", p_objclsInPatientEvaluateContent.m_strFreakOut.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUTMEDICINES", p_objclsInPatientEvaluateContent.m_strFreakOutMedicines.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUTYEARS", p_objclsInPatientEvaluateContent.m_strFreakOutYears.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FREAKOUTQTYONEDAY", p_objclsInPatientEvaluateContent.m_strFreakOutQtyOneDay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ABONRMALDESC", p_objclsInPatientEvaluateContent.m_strAbonrmalDesc.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHARGENURSEID", p_objclsInPatientEvaluateContent.m_strChargeNurseID);
            m_objXmlWriter.WriteAttributeString("CHECKTIME", p_objclsInPatientEvaluateContent.m_strCheckTime);

            ////�����ڴ��ֶΣ�m_objXmlWriter.WriteAttributeString("STATUS", "0");
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        /// <summary>
        /// //��ѯ��һ�δ�ӡʱ��
        /// </summary>		
        public long m_strGetFirstPrintDate(string p_strInPatientID, string p_strInPatientDate,/*string p_strOpenDate,*/out string p_strFirstPrintDate)
        {
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsInPatientEvaluateServ_m_lngGetFirstPrintDate(p_strInPatientID, p_strInPatientDate,/*p_strOpenDate,*/out p_strFirstPrintDate);
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
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID, string p_strInPatientDate/*,string p_strOpenDate*/)
        {//���µ�һ�δ�ӡʱ��		
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsInPatientEvaluateServ_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate/*,p_strOpenDate*/);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }

        public long m_lngDelete(string p_strDeActivedOperatorID, string p_strInPatientID, string p_strInPatientDate/*,string p_strOpenDate*/)
        {
            long lngResult = 0;

            //clsInPatientEvaluateServ m_objServ =
            //    (clsInPatientEvaluateServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInPatientEvaluateServ));

            try
            {
                lngResult = (new weCare.Proxy.ProxyEmr05()).Service.clsInPatientEvaluateServ_m_lngDelete(p_strDeActivedOperatorID, p_strInPatientID, p_strInPatientDate/*,p_strOpenDate*/);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
        }
    }

    //	/// <summary>
    //	/// ��Ժ�����������������
    //	/// </summary>
    //	[Serializable]
    //	public class clsInPatientEvaluate
    //	{
    //		public string m_strInPatientID;
    //		public string m_strInPatientDate;
    //		public string m_strOpenDate;
    //
    //		public string m_strCreateDate;
    //		public string m_strCreateUserID;
    //		/// <summary>
    //		/// tinyint��
    //		/// </summary>
    //		public string m_strEducation;
    //		/// <summary>
    //		/// bit��
    //		/// </summary>
    //		public string m_strReligion;
    //		public string m_strReligionContent;
    //		/// <summary>
    //		/// tinyint��
    //		/// </summary>
    //		public string m_strDataFrom;
    //		public string m_strDataFromOtherContent;
    //
    //		public string m_strInPatientDiagnoseXML;	
    //		
    //		public string m_strInPatientMode_TableXML;//DataGrid��GroupBox��Ӧ�ı��XML
    //		public string m_strAllergicHistory_TableXML;
    //		public string m_strAppetite_TableXML;
    //		public string m_strWeight_TableXML;
    //		public string m_strMouth_TableXML;
    //		public string m_strChaw_TableXML;
    //		public string m_strDeglutition_TableXML;
    //		public string m_strStomach_TableXML;
    //		public string m_strSkin_TableXML;
    //		public string m_strChogh_TableXML;
    //		public string m_strPhlegm_TableXML;
    //		public string m_strDejecta_TableXML;
    //		public string m_strPee_TableXML;
    //		public string m_strCanMyself_TableXML;
    //		public string m_strBreathUrge_TableXML;
    //		public string m_strLimbActive_TableXML;
    //		public string m_strSleep_TableXML;
    //		public string m_strAssistantSleep_TableXML;
    //		public string m_strShout_TableXML;
    //		public string m_strAnswer_TableXML;
    //		public string m_strSeeingBalk_TableXML;
    //		public string m_strListenBalk_TableXML;
    //		public string m_strAche_TableXML;
    //		public string m_strLanguage_TableXML;
    //		public string m_strInHospitalFeel_TableXML;
    //		public string m_strLookIn_TableXML;
    //		public string m_strEconomic_TableXML;
    //		public string m_strIllnessUnderstand_TableXML;
    //		public string m_strDoctorsAdvice_TableXML;
    //		public string m_strSmoking_TableXML;
    //		public string m_strDrink_TableXML;
    //		public string m_strFreakOut_TableXML;		
    //
    //		public string m_strAllergicSourceXML;		
    //		public string m_strAllergicSymptomXML;		
    //		public string m_strHowMuchWeightXML;		
    //		public string m_strStomachColorXML;		
    //		public string m_strStomachCharacterXML;		
    //		public string m_strStomachTimesXML;		
    //		public string m_strStomachQtyXML;
    //		public string m_strBodyXML;
    //		public string m_strSkinOtherContentXML;
    //		public string m_strPhlegmColorXML;
    //		public string m_strDejectaTimesInOneDayXML;
    //		public string m_strDejectaHowManyDaysOnceXML;
    //		public string m_strDejectaCharacterXML;
    //		public string m_strPeeOtherContentXML;
    //		public string m_strParalysisPartXML;
    //		public string m_strSleepHoursXML;			
    //		public string m_strSleepOtherContentXML ;
    //		public string m_strAssistantSleepMedicinesXML ;
    //		public string m_strAssistantSleepModelXML ;
    //		public string m_strLeftEyeOtherContentXML ;
    //		public string m_strRightEyeOtherContentXML ;
    //		public string m_strBothEyeOtherContentXML ;
    //		public string m_strLeftListenOtherContentXML ;
    //		public string m_strRightListenOtherContentXML ;
    //		public string m_strBothListenOtherContentXML ;
    //		public string m_strAchePartXML ;
    //		public string m_strAlwayslanguageXML ;
    //		public string m_strDoctorsAdviceContentXML ;
    //		public string m_strSmokingYearsXML ;
    //		public string m_strSmokingQtyOneDayXML ;
    //		public string m_strDrinkYearsXML ;
    //		public string m_strDrinkQtyOneDayXML ;
    //		public string m_strFreakOutMedicinesXML ;
    //		public string m_strFreakOutYearsXML ;
    //		public string m_strFreakOutQtyOneDayXML ;
    //		
    //		public string m_strIfConfirm;
    //		public string m_strConfirmReason;	
    //		public string m_strConfirmReasonXML ;			
    //		
    //	}
    //
    //	/// <summary>
    //	/// ��Ժ�����������ӱ����
    //	/// </summary>
    //	[Serializable]
    //	public class clsInPatientEvaluateContent
    //	{
    //		public string m_strInPatientID;
    //		public string m_strInPatientDate;
    //		public string m_strOpenDate;
    //		public string m_strModifyDate;
    //		public string m_strModifyUserID;
    //		
    //		public string m_strInPatientDiagnose;
    //		public string m_strInPatientMode;
    //		public string m_strAllergicHistory;
    //		public string m_strAllergicSource;
    //		public string m_strAllergicSymptom;
    //		public string m_strAppetite;
    //		public string m_strWeight;
    //		public string m_strHowMuchWeight;
    //		public string m_strMouth;
    //		public string m_strChaw;
    //		public string m_strDeglutition;
    //		public string m_strStomachNothing;
    //		public string m_strStomachRise;
    //		public string m_strStomachAche;
    //		public string m_strStomachNaupathia;
    //		public string m_strStomachSpew;
    //		public string m_strStomachColor;
    //		public string m_strStomachCharacter;
    //		public string m_strStomachTimes;
    //		public string m_strStomachQty;
    //		public string m_strSkinFull;
    //		public string m_strSkinPallor;	
    //		public string m_strSkinIcterus ;
    //		public string m_strSkinRed ;
    //		public string m_strSkinCyanopathy ;
    //		public string m_strSkinDehydrate ;
    //		public string m_strSkinAnthema ;
    //		public string m_strSkinBlood ;
    //		public string m_strSkinSore ;
    //		public string m_strSkinCut ;
    //		public string m_strSkinDropsy ;
    //		public string m_strBody ;
    //		public string m_strUpperLimbs ;
    //		public string m_strLowerLimbs ;
    //		public string m_strSkinOther ;
    //		public string m_strSkinOtherContent ;
    //		public string m_strChogh ;
    //		public string m_strHavePhlegm ;
    //		public string m_strPhlegmEasy ;
    //		public string m_strPhlegmChroma ;
    //		public string m_strPhlegmColor ;
    //		public string m_strDejectaTimesInOneDay ;
    //		public string m_strDejectaHowManyDaysOnce ;
    //		public string m_strDejectaIrretention ;
    //		public string m_strDejectaFistula ;
    //		public string m_strDejectaCharacter ;
    //		public string m_strPeeIrretention ;
    //		public string m_strPeeNatural ;
    //		public string m_strPeeUraemia ;
    //		public string m_strPeeMuch ;
    //		public string m_strPeeBlood ;
    //		public string m_strPeeCyst ;
    //		public string m_strPeePipe ;
    //		public string m_strPeeOther ;
    //		public string m_strPeeOtherContent ;
    //		public string m_strCanMyself ;
    //		public string m_strBreathUrge ;
    //		public string m_strLimbActive ;
    //		public string m_strParalysis ;
    //		public string m_strParalysisPart ;
    //		public string m_strSleepHours ;
    //		public string m_strSleepNothing ;
    //		public string m_strSleepDifficulty ;
    //		public string m_strSleepWakeEasy ;
    //		public string m_strSleepWakeEarly ;
    //		public string m_strSleepDreamMuch ;
    //		public string m_strSleepOther ;
    //		public string m_strSleepOtherContent ;
    //		public string m_strAssistantSleep ;
    //		public string m_strAssistantSleepMedicines ;
    //		public string m_strAssistantSleepModel ;
    //		public string m_strShout ;
    //		public string m_strAnswer ;
    //		public string m_strSeeingBalk ;
    ////		public string m_strLeftRightEye ;
    //		public string m_strLeftEyeDown ;
    //		public string m_strLeftEyeBlur ;
    //		public string m_strLeftEyeAgain ;
    //		public string m_strLeftEyeOther ;
    //		public string m_strLeftEyeOtherContent ;
    //		public string m_strRightEyeDown ;
    //		public string m_strRightEyeBlur ;
    //		public string m_strRightEyeAgain ;
    //		public string m_strRightEyeOther ;
    //		public string m_strRightEyeOtherContent ;
    //		public string m_strBothEyeDown ;
    //		public string m_strBothEyeBlur ;
    //		public string m_strBothEyeAgain ;
    //		public string m_strBothEyeOther ;
    //		public string m_strBothEyeOtherContent ;
    //		public string m_strListenBalk ;
    ////		public string m_strLeftRightListen ;
    //		public string m_strLeftListenDown ;
    //		public string m_strLeftListenTinnitus ;
    //		public string m_strLeftListenAgain ;
    //		public string m_strLeftListenOther ;
    //		public string m_strLeftListenOtherContent ;
    //		public string m_strRightListenDown ;
    //		public string m_strRightListenTinnitus ;
    //		public string m_strRightListenAgain ;
    //		public string m_strRightListenOther ;
    //		public string m_strRightListenOtherContent ;
    //		public string m_strBothListenDown ;
    //		public string m_strBothListenTinnitus ;
    //		public string m_strBothListenAgain ;
    //		public string m_strBothListenOther ;
    //		public string m_strBothListenOtherContent ;
    //		public string m_strAche ;
    //		public string m_strAchePart ;
    //		public string m_strAcheTimes ;
    //		public string m_strAlwayslanguage ;
    //		public string m_strMandarin ;
    //		public string m_strCantSay ;
    //		public string m_strInHospitalFeel ;
    //		public string m_strLookIn ;
    //		public string m_strEconomic ;
    //		public string m_strIllnessUnderstand ;
    //		public string m_strDoctorsAdvice ;
    //		public string m_strDoctorsAdviceContent ;
    //		public string m_strIfSmoking ;
    //		public string m_strSmokingYears ;
    //		public string m_strSmokingQtyOneDay ;
    //		public string m_strIfDrink ;
    //		public string m_strDrinkYears ;
    //		public string m_strDrinkQtyOneDay ;
    //		public string m_strFreakOut ;
    //		public string m_strFreakOutMedicines ;
    //		public string m_strFreakOutYears ;
    //		public string m_strFreakOutQtyOneDay ;
    //		public string m_strAbonrmalDesc;//�쳣��Ϣ
    //		public string m_strChargeNurseID ;		
    //	}	
    //
    //	/// <summary>
    //	/// ��Ժ����������������Ϣ����
    //	/// </summary>
    //	[Serializable]
    //	public class clsInPatientEvaluate_All
    //	{
    //		public clsInPatientEvaluate m_objclsInPatientEvaluate;
    //		public clsInPatientEvaluateContent m_objclsInPatientEvaluateContent;
    //	}
}
