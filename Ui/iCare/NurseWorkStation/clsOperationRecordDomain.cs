using System;
using System.IO;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// һ�㻤���Domain��
    /// </summary>
    public class clsOperationRecordDomain
    {
        #region ���������캯��
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
        //private com.digitalwave.OperationRecord.clsOperationRecordServ   m_objServ;
        public clsOperationRecordDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objServ=new com.digitalwave.OperationRecord.clsOperationRecordServ ();
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);
        }
        #endregion

        #region ��ȡһ��������¼��Record||Content||OperationID||AnaesthesiaID||Nurse||WoundThing)
        public long m_lngGetOperationRecord(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsOperationRecord objOperationRecordReturn)
        {
            objOperationRecordReturn = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;

            string strXml = "";
            int intRows = 0;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngGetOperationRecord(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {

                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    clsOperationRecord objOperationRecord = new clsOperationRecord();
                                    objOperationRecord.strCreateDate = objReader.GetAttribute("CREATEDATE").Replace('��', '\'');

                                    objOperationRecord.strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                                    objOperationRecord.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                                    objOperationRecord.strOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE").Replace('��', '\'')).ToString("yyyy-MM-dd HH:mm:ss");

                                    if (objReader.GetAttribute("FIRSTPRINTDATE") == "")
                                        objOperationRecord.strFirstPrintDate = "";
                                    else
                                        objOperationRecord.strFirstPrintDate = DateTime.Parse(objReader.GetAttribute("FIRSTPRINTDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                                    objOperationRecord.strCreateUserID = objReader.GetAttribute("CREATEUSERID").Replace('��', '\'');
                                    objOperationRecord.strIfConfirm = objReader.GetAttribute("IFCONFIRM").Replace('��', '\'');
                                    objOperationRecord.strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                    objOperationRecord.strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").Replace('��', '\'');
                                    objOperationRecord.strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                                    //								objOperationRecord.strDeActivedDate= objReader.GetAttribute("DEACTIVEDDATE").Replace('��','\'');
                                    //								objOperationRecord.strDeActivedOperatorID= objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��','\'');
                                    objOperationRecord.strSensesXML = objReader.GetAttribute("SENSESXML").Replace('��', '\'');
                                    objOperationRecord.strAllergicXML = objReader.GetAttribute("ALLERGICXML").Replace('��', '\'');
                                    objOperationRecord.strOperationLocationXML = objReader.GetAttribute("OPERATIONLOCATIONXML").Replace('��', '\'');
                                    objOperationRecord.strElectKnifeXML = objReader.GetAttribute("ELECTKNIFEXML").Replace('��', '\'');
                                    objOperationRecord.strDoublePoleXML = objReader.GetAttribute("DOUBLEPOLEXML").Replace('��', '\'');
                                    objOperationRecord.strCathodeLocationSkinXML = objReader.GetAttribute("CATHODELOCATIONSKINXML").Replace('��', '\'');
                                    objOperationRecord.strCathodeLocationSkinAfterOperationXML = objReader.GetAttribute("CATHODELOCSKINAFOPRXML").Replace('��', '\'');
                                    objOperationRecord.strStypticRubberXML = objReader.GetAttribute("STYPTICRUBBERXML").Replace('��', '\'');
                                    objOperationRecord.strUpXML = objReader.GetAttribute("UPXML").Replace('��', '\'');
                                    objOperationRecord.strDownXML = objReader.GetAttribute("DOWNXML").Replace('��', '\'');
                                    objOperationRecord.strFoleyXML = objReader.GetAttribute("FOLEYXML").Replace('��', '\'');
                                    objOperationRecord.strStomachXML = objReader.GetAttribute("STOMACHXML").Replace('��', '\'');
                                    objOperationRecord.strSkinAntisepsisXML = objReader.GetAttribute("SKINANTISEPSISXML").Replace('��', '\'');
                                    objOperationRecord.strBloodXML = objReader.GetAttribute("BLOODXML").Replace('��', '\'');
                                    objOperationRecord.strInLiquidQtyXML = objReader.GetAttribute("INLIQUIDQTYXML").Replace('��', '\'');
                                    objOperationRecord.strPeeOperatingQtyXML = objReader.GetAttribute("PEEOPERATINGQTYXML").Replace('��', '\'');
                                    objOperationRecord.strOutFlowXML = objReader.GetAttribute("OUTFLOWXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinXML = objReader.GetAttribute("FROMHEADTOFOOTSKINXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinAfterOperationXML = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRXML").Replace('��', '\''); objOperationRecord.strSampleXML = objReader.GetAttribute("SAMPLEXML").Replace('��', '\'');
                                    objOperationRecord.strAfterOperationSendXML = objReader.GetAttribute("AFTEROPERATIONSENDXML").Replace('��', '\'');

                                    objOperationRecord.strTendRecordXML = objReader.GetAttribute("TENDRECORDXML").Replace('��', '\'');
                                    objOperationRecord.strOperationNameXML = objReader.GetAttribute("OPERATIONNAMEXML").Replace('��', '\'');
                                    objOperationRecord.strAnaesthesiaModeXML = objReader.GetAttribute("ANAESTHESIAMODEXML").Replace('��', '\'');

                                    objOperationRecord.strOperationRoomXML = objReader.GetAttribute("OPERATIONROOMXML").Replace('��', '\'');
                                    objOperationRecord.strOperation_AnaesthesiaXML = objReader.GetAttribute("OPERATION_ANAESTHESIAXML").Replace('��', '\'');


                                    objOperationRecord.strAllergicContentXML = objReader.GetAttribute("ALLERGICCONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strOtherOperationLocationXML = objReader.GetAttribute("OTHEROPERATIONLOCATIONXML").Replace('��', '\'');
                                    objOperationRecord.strElectKnifeModelXML = objReader.GetAttribute("ELECTKNIFEMODELXML").Replace('��', '\'');
                                    objOperationRecord.strDoublePoleContentXML = objReader.GetAttribute("DOUBLEPOLECONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strCathodeLocationXML = objReader.GetAttribute("CATHODELOCATIONXML").Replace('��', '\'');
                                    objOperationRecord.strStypticPressureModeXML = objReader.GetAttribute("STYPTICPRESSUREMODEXML").Replace('��', '\'');
                                    objOperationRecord.strUpPuffDateTimeXML = objReader.GetAttribute("UPPUFFDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strUpDeflateDateTimeXML = objReader.GetAttribute("UPDEFLATEDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strUpTotalDateTimeXML = objReader.GetAttribute("UPTOTALDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strUpPressXML = objReader.GetAttribute("UPPRESSXML").Replace('��', '\'');
                                    objOperationRecord.strDownPuffDateTimeXML = objReader.GetAttribute("DOWNPUFFDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strDownDeflateDateTimeXML = objReader.GetAttribute("DOWNDEFLATEDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strDownTotalDateTimeXML = objReader.GetAttribute("DOWNTOTALDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strDownPressXML = objReader.GetAttribute("DOWNPRESSXML").Replace('��', '\'');
                                    objOperationRecord.strFoleyOtherContentXML = objReader.GetAttribute("FOLEYOTHERCONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strSkinAntisepsisOtherContentXML = objReader.GetAttribute("SKINANTISEPSISOTHERCONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strAllBloodQtyXML = objReader.GetAttribute("ALLBLOODQTYXML").Replace('��', '\'');
                                    objOperationRecord.strRedCellQtyXML = objReader.GetAttribute("REDCELLQTYXML").Replace('��', '\'');
                                    objOperationRecord.strBloodPlasmQtyXML = objReader.GetAttribute("BLOODPLASMQTYXML").Replace('��', '\'');
                                    objOperationRecord.strOwnBloodQtyXML = objReader.GetAttribute("OWNBLOODQTYXML").Replace('��', '\'');
                                    objOperationRecord.strBloodOtherQtyXML = objReader.GetAttribute("BLOODOTHERQTYXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinBeforeOperationContentXML = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRCONTXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinAfterOperationContentXML = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRCONTXML").Replace('��', '\'');
                                    objOperationRecord.strSampleOtherContentXML = objReader.GetAttribute("SAMPLEOTHERCONTENTXML").Replace('��', '\'');

                                    string strSeqSign = objReader.GetAttribute("SEQUENCE_INT").Replace('��', '\'');
                                    long lngS = long.Parse(strSeqSign);
                                    //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objService =
                                    //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                                    long lngTemp = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetSign(lngS, out objOperationRecord.objSignerArr);

                                    objOperationRecordReturn = objOperationRecord;

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

        public long m_lngGetDeleteOperationRecord(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsOperationRecord objOperationRecordReturn)
        {
            objOperationRecordReturn = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;

            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetDeleteOperationRecord(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {

                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    clsOperationRecord objOperationRecord = new clsOperationRecord();
                                    objOperationRecord.strCreateDate = objReader.GetAttribute("CREATEDATE").Replace('��', '\'');

                                    objOperationRecord.strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                                    objOperationRecord.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                                    objOperationRecord.strOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE").Replace('��', '\'')).ToString("yyyy-MM-dd HH:mm:ss");

                                    if (objReader.GetAttribute("FIRSTPRINTDATE") == "")
                                        objOperationRecord.strFirstPrintDate = "";
                                    else
                                        objOperationRecord.strFirstPrintDate = DateTime.Parse(objReader.GetAttribute("FIRSTPRINTDATE")).ToString("yyyy-MM-dd HH:mm:ss");
                                    objOperationRecord.strCreateUserID = objReader.GetAttribute("CREATEUSERID").Replace('��', '\'');
                                    objOperationRecord.strIfConfirm = objReader.GetAttribute("IFCONFIRM").Replace('��', '\'');
                                    objOperationRecord.strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\'');
                                    objOperationRecord.strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").Replace('��', '\'');
                                    objOperationRecord.strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                                    //								objOperationRecord.strDeActivedDate= objReader.GetAttribute("DEACTIVEDDATE").Replace('��','\'');
                                    //								objOperationRecord.strDeActivedOperatorID= objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��','\'');
                                    objOperationRecord.strSensesXML = objReader.GetAttribute("SENSESXML").Replace('��', '\'');
                                    objOperationRecord.strAllergicXML = objReader.GetAttribute("ALLERGICXML").Replace('��', '\'');
                                    objOperationRecord.strOperationLocationXML = objReader.GetAttribute("OPERATIONLOCATIONXML").Replace('��', '\'');
                                    objOperationRecord.strElectKnifeXML = objReader.GetAttribute("ELECTKNIFEXML").Replace('��', '\'');
                                    objOperationRecord.strDoublePoleXML = objReader.GetAttribute("DOUBLEPOLEXML").Replace('��', '\'');
                                    objOperationRecord.strCathodeLocationSkinXML = objReader.GetAttribute("CATHODELOCATIONSKINXML").Replace('��', '\'');
                                    objOperationRecord.strCathodeLocationSkinAfterOperationXML = objReader.GetAttribute("CATHODELOCSKINAFOPRXML").Replace('��', '\'');
                                    objOperationRecord.strStypticRubberXML = objReader.GetAttribute("STYPTICRUBBERXML").Replace('��', '\'');
                                    objOperationRecord.strUpXML = objReader.GetAttribute("UPXML").Replace('��', '\'');
                                    objOperationRecord.strDownXML = objReader.GetAttribute("DOWNXML").Replace('��', '\'');
                                    objOperationRecord.strFoleyXML = objReader.GetAttribute("FOLEYXML").Replace('��', '\'');
                                    objOperationRecord.strStomachXML = objReader.GetAttribute("STOMACHXML").Replace('��', '\'');
                                    objOperationRecord.strSkinAntisepsisXML = objReader.GetAttribute("SKINANTISEPSISXML").Replace('��', '\'');
                                    objOperationRecord.strBloodXML = objReader.GetAttribute("BLOODXML").Replace('��', '\'');
                                    objOperationRecord.strInLiquidQtyXML = objReader.GetAttribute("INLIQUIDQTYXML").Replace('��', '\'');
                                    objOperationRecord.strPeeOperatingQtyXML = objReader.GetAttribute("PEEOPERATINGQTYXML").Replace('��', '\'');
                                    objOperationRecord.strOutFlowXML = objReader.GetAttribute("OUTFLOWXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinXML = objReader.GetAttribute("FROMHEADTOFOOTSKINXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinAfterOperationXML = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRXML").Replace('��', '\''); objOperationRecord.strSampleXML = objReader.GetAttribute("SAMPLEXML").Replace('��', '\'');
                                    objOperationRecord.strAfterOperationSendXML = objReader.GetAttribute("AFTEROPERATIONSENDXML").Replace('��', '\'');

                                    objOperationRecord.strTendRecordXML = objReader.GetAttribute("TENDRECORDXML").Replace('��', '\'');
                                    objOperationRecord.strOperationNameXML = objReader.GetAttribute("OPERATIONNAMEXML").Replace('��', '\'');
                                    objOperationRecord.strAnaesthesiaModeXML = objReader.GetAttribute("ANAESTHESIAMODEXML").Replace('��', '\'');

                                    objOperationRecord.strOperationRoomXML = objReader.GetAttribute("OPERATIONROOMXML").Replace('��', '\'');
                                    objOperationRecord.strOperation_AnaesthesiaXML = objReader.GetAttribute("OPERATION_ANAESTHESIAXML").Replace('��', '\'');


                                    objOperationRecord.strAllergicContentXML = objReader.GetAttribute("ALLERGICCONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strOtherOperationLocationXML = objReader.GetAttribute("OTHEROPERATIONLOCATIONXML").Replace('��', '\'');
                                    objOperationRecord.strElectKnifeModelXML = objReader.GetAttribute("ELECTKNIFEMODELXML").Replace('��', '\'');
                                    objOperationRecord.strDoublePoleContentXML = objReader.GetAttribute("DOUBLEPOLECONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strCathodeLocationXML = objReader.GetAttribute("CATHODELOCATIONXML").Replace('��', '\'');
                                    objOperationRecord.strStypticPressureModeXML = objReader.GetAttribute("STYPTICPRESSUREMODEXML").Replace('��', '\'');
                                    objOperationRecord.strUpPuffDateTimeXML = objReader.GetAttribute("UPPUFFDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strUpDeflateDateTimeXML = objReader.GetAttribute("UPDEFLATEDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strUpTotalDateTimeXML = objReader.GetAttribute("UPTOTALDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strUpPressXML = objReader.GetAttribute("UPPRESSXML").Replace('��', '\'');
                                    objOperationRecord.strDownPuffDateTimeXML = objReader.GetAttribute("DOWNPUFFDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strDownDeflateDateTimeXML = objReader.GetAttribute("DOWNDEFLATEDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strDownTotalDateTimeXML = objReader.GetAttribute("DOWNTOTALDATETIMEXML").Replace('��', '\'');
                                    objOperationRecord.strDownPressXML = objReader.GetAttribute("DOWNPRESSXML").Replace('��', '\'');
                                    objOperationRecord.strFoleyOtherContentXML = objReader.GetAttribute("FOLEYOTHERCONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strSkinAntisepsisOtherContentXML = objReader.GetAttribute("SKINANTISEPSISOTHERCONTENTXML").Replace('��', '\'');
                                    objOperationRecord.strAllBloodQtyXML = objReader.GetAttribute("ALLBLOODQTYXML").Replace('��', '\'');
                                    objOperationRecord.strRedCellQtyXML = objReader.GetAttribute("REDCELLQTYXML").Replace('��', '\'');
                                    objOperationRecord.strBloodPlasmQtyXML = objReader.GetAttribute("BLOODPLASMQTYXML").Replace('��', '\'');
                                    objOperationRecord.strOwnBloodQtyXML = objReader.GetAttribute("OWNBLOODQTYXML").Replace('��', '\'');
                                    objOperationRecord.strBloodOtherQtyXML = objReader.GetAttribute("BLOODOTHERQTYXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinBeforeOperationContentXML = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRCONTXML").Replace('��', '\'');
                                    objOperationRecord.strFromHeadToFootSkinAfterOperationContentXML = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRCONTXML").Replace('��', '\'');
                                    objOperationRecord.strSampleOtherContentXML = objReader.GetAttribute("SAMPLEOTHERCONTENTXML").Replace('��', '\'');

                                    string strSeqSign = objReader.GetAttribute("SEQUENCE_INT").Replace('��', '\'');
                                    long lngS = long.Parse(strSeqSign);
                                    //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objService =
                                    //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));
                                    long lngTemp = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetSign(lngS, out objOperationRecord.objSignerArr);

                                    objOperationRecordReturn = objOperationRecord;

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

        public long m_lngGetOperationRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsOperationRecordContent objOperationRecordContent)
        {
            objOperationRecordContent = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;
            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngGetOperationRecordContent(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {

                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {

                                    objOperationRecordContent = new clsOperationRecordContent();
                                    objOperationRecordContent.strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE").ToString().Replace('��', '\'')).ToString("yyyy-MM-dd HH:mm:ss");
                                    objOperationRecordContent.strLastModifyDate = DateTime.Parse(objReader.GetAttribute("LASTMODIFYDATE").ToString().Replace('��', '\'')).ToString("yyyy-MM-dd HH:mm:ss");
                                    objOperationRecordContent.strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStatus = objReader.GetAttribute("STATUS").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strPatientInDate = objReader.GetAttribute("PATIENTINDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationBeginDate = objReader.GetAttribute("OPERATIONBEGINDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationEndDate = objReader.GetAttribute("OPERATIONENDDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLeaveDate = objReader.GetAttribute("OPERATIONLEAVEDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSensesClearHeaded = objReader.GetAttribute("SENSESCLEARHEADED").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSensesSleep = objReader.GetAttribute("SENSESSLEEP").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSensesComa = objReader.GetAttribute("SENSESCOMA").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveAllergic = objReader.GetAttribute("HAVEALLERGIC").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveNotAllergic = objReader.GetAttribute("HAVENOTALLERGIC").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAllergicContent = objReader.GetAttribute("ALLERGICCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationOnHisBack = objReader.GetAttribute("OPERATIONLOCATIONONHISBACK").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationSide = objReader.GetAttribute("OPERATIONLOCATIONSIDE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationPA = objReader.GetAttribute("OPERATIONLOCATIONPA").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationParaplegic = objReader.GetAttribute("OPERATIONLOCATIONPARAPLEGIC").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationHypothyroid = objReader.GetAttribute("OPERATIONLOCATIONHYPOTHYROID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationOther = objReader.GetAttribute("OPERATIONLOCATIONOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOtherOperationLocation = objReader.GetAttribute("OTHEROPERATIONLOCATION").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveNotElectKnife = objReader.GetAttribute("HAVENOTELECTKNIFE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveUsedElectKnife = objReader.GetAttribute("HAVEUSEDELECTKNIFE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strElectKnifeModel = objReader.GetAttribute("ELECTKNIFEMODEL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveNotDoublePole = objReader.GetAttribute("HAVENOTDOUBLEPOLE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveDoublePole = objReader.GetAttribute("HAVEDOUBLEPOLE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDoublePoleContent = objReader.GetAttribute("DOUBLEPOLECONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocation = objReader.GetAttribute("CATHODELOCATION").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinBeforOperationMar = objReader.GetAttribute("CATHODELOCSKINBFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinBeforOperationFull = objReader.GetAttribute("CATHODELOCSKINBFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinAfterOperationMar = objReader.GetAttribute("CATHODELOCSKINAFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinAfterOperationFull = objReader.GetAttribute("CATHODELOCSKINAFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStypticRubber = objReader.GetAttribute("STYPTICRUBBER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStypticPressure = objReader.GetAttribute("STYPTICPRESSURE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStypticPressureMode = objReader.GetAttribute("STYPTICPRESSUREMODE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpForearm = objReader.GetAttribute("UPFOREARM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpThigh = objReader.GetAttribute("UPTHIGH").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpRight = objReader.GetAttribute("UPRIGHT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpLeft = objReader.GetAttribute("UPLEFT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpPuffDateTime = objReader.GetAttribute("UPPUFFDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpDeflateDateTime = objReader.GetAttribute("UPDEFLATEDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpTotalDateTime = objReader.GetAttribute("UPTOTALDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpPress = objReader.GetAttribute("UPPRESS").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownForearm = objReader.GetAttribute("DOWNFOREARM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownThigh = objReader.GetAttribute("DOWNTHIGH").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownRight = objReader.GetAttribute("DOWNRIGHT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownLeft = objReader.GetAttribute("DOWNLEFT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownPuffDateTime = objReader.GetAttribute("DOWNPUFFDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownDeflateDateTime = objReader.GetAttribute("DOWNDEFLATEDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownTotalDateTime = objReader.GetAttribute("DOWNTOTALDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownPress = objReader.GetAttribute("DOWNPRESS").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleySickroom = objReader.GetAttribute("FOLEYSICKROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyOperationRoom = objReader.GetAttribute("FOLEYOPERATIONROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyDoubleAntrum = objReader.GetAttribute("FOLEYDOUBLEANTRUM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyThreeAntrum = objReader.GetAttribute("FOLEYTHREEANTRUM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyOther = objReader.GetAttribute("FOLEYOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyOtherContent = objReader.GetAttribute("FOLEYOTHERCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStomachSickroom = objReader.GetAttribute("STOMACHSICKROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStomachOprationRoom = objReader.GetAttribute("STOMACHOPRATIONROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsis2 = objReader.GetAttribute("SKINANTISEPSIS2").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsis75 = objReader.GetAttribute("SKINANTISEPSIS75").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisIodin = objReader.GetAttribute("SKINANTISEPSISIODIN").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisIodinRare = objReader.GetAttribute("SKINANTISEPSISIODINRARE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisOther = objReader.GetAttribute("SKINANTISEPSISOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisOtherContent = objReader.GetAttribute("SKINANTISEPSISOTHERCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAllBlood = objReader.GetAttribute("ALLBLOOD").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAllBloodQty = objReader.GetAttribute("ALLBLOODQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strRedCell = objReader.GetAttribute("REDCELL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strRedCellQty = objReader.GetAttribute("REDCELLQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodPlasm = objReader.GetAttribute("BLOODPLASM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodPlasmQty = objReader.GetAttribute("BLOODPLASMQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOwnBlood = objReader.GetAttribute("OWNBLOOD").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOwnBloodQty = objReader.GetAttribute("OWNBLOODQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodOther = objReader.GetAttribute("BLOODOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodOtherQty = objReader.GetAttribute("BLOODOTHERQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strInLiquidQty = objReader.GetAttribute("INLIQUIDQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strPeeOperatingQty = objReader.GetAttribute("PEEOPERATINGQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveOutFlow = objReader.GetAttribute("HAVEOUTFLOW").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strNotHaveOutFlow = objReader.GetAttribute("NOTHAVEOUTFLOW").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinBeforeOperationMar = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinBeforeOperationFull = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinBeforeOperationContent = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRCONT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinAfterOperationMar = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinAfterOperationFull = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinAfterOperationContent = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRCONT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleGeneral = objReader.GetAttribute("SAMPLEGENERAL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleSlice = objReader.GetAttribute("SAMPLESLICE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleBacilli = objReader.GetAttribute("SAMPLEBACILLI").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleOther = objReader.GetAttribute("SAMPLEOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleOtherContent = objReader.GetAttribute("SAMPLEOTHERCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAfterOperationSendRenew = objReader.GetAttribute("AFTEROPERATIONSENDRENEW").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAfterOperationSendICU = objReader.GetAttribute("AFTEROPERATIONSENDICU").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAfterOperationSendSickRoom = objReader.GetAttribute("AFTEROPERATIONSENDSICKROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strTendRecord = objReader.GetAttribute("TENDRECORD").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationName = objReader.GetAttribute("OPERATIONNAME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAnaesthesiaMode = objReader.GetAttribute("ANAESTHESIAMODE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationRoom = objReader.GetAttribute("OPERATIONROOM").ToString().Replace('��', '\'');


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

        public long m_lngGetDeleteOperationRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsOperationRecordContent objOperationRecordContent)
        {
            objOperationRecordContent = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;
            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetDeleteOperationRecordContent(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {

                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {

                                    objOperationRecordContent = new clsOperationRecordContent();
                                    objOperationRecordContent.strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOpenDate = DateTime.Parse(objReader.GetAttribute("OPENDATE").ToString().Replace('��', '\'')).ToString("yyyy-MM-dd HH:mm:ss");
                                    objOperationRecordContent.strLastModifyDate = DateTime.Parse(objReader.GetAttribute("LASTMODIFYDATE").ToString().Replace('��', '\'')).ToString("yyyy-MM-dd HH:mm:ss");
                                    objOperationRecordContent.strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStatus = objReader.GetAttribute("STATUS").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strPatientInDate = objReader.GetAttribute("PATIENTINDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationBeginDate = objReader.GetAttribute("OPERATIONBEGINDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationEndDate = objReader.GetAttribute("OPERATIONENDDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLeaveDate = objReader.GetAttribute("OPERATIONLEAVEDATE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSensesClearHeaded = objReader.GetAttribute("SENSESCLEARHEADED").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSensesSleep = objReader.GetAttribute("SENSESSLEEP").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSensesComa = objReader.GetAttribute("SENSESCOMA").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveAllergic = objReader.GetAttribute("HAVEALLERGIC").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveNotAllergic = objReader.GetAttribute("HAVENOTALLERGIC").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAllergicContent = objReader.GetAttribute("ALLERGICCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationOnHisBack = objReader.GetAttribute("OPERATIONLOCATIONONHISBACK").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationSide = objReader.GetAttribute("OPERATIONLOCATIONSIDE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationPA = objReader.GetAttribute("OPERATIONLOCATIONPA").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationParaplegic = objReader.GetAttribute("OPERATIONLOCATIONPARAPLEGIC").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationHypothyroid = objReader.GetAttribute("OPERATIONLOCATIONHYPOTHYROID").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationLocationOther = objReader.GetAttribute("OPERATIONLOCATIONOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOtherOperationLocation = objReader.GetAttribute("OTHEROPERATIONLOCATION").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveNotElectKnife = objReader.GetAttribute("HAVENOTELECTKNIFE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveUsedElectKnife = objReader.GetAttribute("HAVEUSEDELECTKNIFE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strElectKnifeModel = objReader.GetAttribute("ELECTKNIFEMODEL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveNotDoublePole = objReader.GetAttribute("HAVENOTDOUBLEPOLE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveDoublePole = objReader.GetAttribute("HAVEDOUBLEPOLE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDoublePoleContent = objReader.GetAttribute("DOUBLEPOLECONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocation = objReader.GetAttribute("CATHODELOCATION").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinBeforOperationMar = objReader.GetAttribute("CATHODELOCSKINBFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinBeforOperationFull = objReader.GetAttribute("CATHODELOCSKINBFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinAfterOperationMar = objReader.GetAttribute("CATHODELOCSKINAFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strCathodeLocationSkinAfterOperationFull = objReader.GetAttribute("CATHODELOCSKINAFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStypticRubber = objReader.GetAttribute("STYPTICRUBBER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStypticPressure = objReader.GetAttribute("STYPTICPRESSURE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStypticPressureMode = objReader.GetAttribute("STYPTICPRESSUREMODE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpForearm = objReader.GetAttribute("UPFOREARM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpThigh = objReader.GetAttribute("UPTHIGH").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpRight = objReader.GetAttribute("UPRIGHT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpLeft = objReader.GetAttribute("UPLEFT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpPuffDateTime = objReader.GetAttribute("UPPUFFDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpDeflateDateTime = objReader.GetAttribute("UPDEFLATEDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpTotalDateTime = objReader.GetAttribute("UPTOTALDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strUpPress = objReader.GetAttribute("UPPRESS").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownForearm = objReader.GetAttribute("DOWNFOREARM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownThigh = objReader.GetAttribute("DOWNTHIGH").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownRight = objReader.GetAttribute("DOWNRIGHT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownLeft = objReader.GetAttribute("DOWNLEFT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownPuffDateTime = objReader.GetAttribute("DOWNPUFFDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownDeflateDateTime = objReader.GetAttribute("DOWNDEFLATEDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownTotalDateTime = objReader.GetAttribute("DOWNTOTALDATETIME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strDownPress = objReader.GetAttribute("DOWNPRESS").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleySickroom = objReader.GetAttribute("FOLEYSICKROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyOperationRoom = objReader.GetAttribute("FOLEYOPERATIONROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyDoubleAntrum = objReader.GetAttribute("FOLEYDOUBLEANTRUM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyThreeAntrum = objReader.GetAttribute("FOLEYTHREEANTRUM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyOther = objReader.GetAttribute("FOLEYOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFoleyOtherContent = objReader.GetAttribute("FOLEYOTHERCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStomachSickroom = objReader.GetAttribute("STOMACHSICKROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strStomachOprationRoom = objReader.GetAttribute("STOMACHOPRATIONROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsis2 = objReader.GetAttribute("SKINANTISEPSIS2").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsis75 = objReader.GetAttribute("SKINANTISEPSIS75").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisIodin = objReader.GetAttribute("SKINANTISEPSISIODIN").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisIodinRare = objReader.GetAttribute("SKINANTISEPSISIODINRARE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisOther = objReader.GetAttribute("SKINANTISEPSISOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSkinAntisepsisOtherContent = objReader.GetAttribute("SKINANTISEPSISOTHERCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAllBlood = objReader.GetAttribute("ALLBLOOD").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAllBloodQty = objReader.GetAttribute("ALLBLOODQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strRedCell = objReader.GetAttribute("REDCELL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strRedCellQty = objReader.GetAttribute("REDCELLQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodPlasm = objReader.GetAttribute("BLOODPLASM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodPlasmQty = objReader.GetAttribute("BLOODPLASMQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOwnBlood = objReader.GetAttribute("OWNBLOOD").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOwnBloodQty = objReader.GetAttribute("OWNBLOODQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodOther = objReader.GetAttribute("BLOODOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strBloodOtherQty = objReader.GetAttribute("BLOODOTHERQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strInLiquidQty = objReader.GetAttribute("INLIQUIDQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strPeeOperatingQty = objReader.GetAttribute("PEEOPERATINGQTY").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strHaveOutFlow = objReader.GetAttribute("HAVEOUTFLOW").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strNotHaveOutFlow = objReader.GetAttribute("NOTHAVEOUTFLOW").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinBeforeOperationMar = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinBeforeOperationFull = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinBeforeOperationContent = objReader.GetAttribute("FROMHEADTOFOOTSKINBFOPRCONT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinAfterOperationMar = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRMAR").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinAfterOperationFull = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRFULL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strFromHeadToFootSkinAfterOperationContent = objReader.GetAttribute("FROMHEADTOFOOTSKINAFOPRCONT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleGeneral = objReader.GetAttribute("SAMPLEGENERAL").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleSlice = objReader.GetAttribute("SAMPLESLICE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleBacilli = objReader.GetAttribute("SAMPLEBACILLI").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleOther = objReader.GetAttribute("SAMPLEOTHER").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strSampleOtherContent = objReader.GetAttribute("SAMPLEOTHERCONTENT").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAfterOperationSendRenew = objReader.GetAttribute("AFTEROPERATIONSENDRENEW").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAfterOperationSendICU = objReader.GetAttribute("AFTEROPERATIONSENDICU").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAfterOperationSendSickRoom = objReader.GetAttribute("AFTEROPERATIONSENDSICKROOM").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strTendRecord = objReader.GetAttribute("TENDRECORD").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationName = objReader.GetAttribute("OPERATIONNAME").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strAnaesthesiaMode = objReader.GetAttribute("ANAESTHESIAMODE").ToString().Replace('��', '\'');
                                    objOperationRecordContent.strOperationRoom = objReader.GetAttribute("OPERATIONROOM").ToString().Replace('��', '\'');


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

        public long m_lngGetLastestOperationID(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out string m_strOperationID)
        {
            m_strOperationID = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;


            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetLastestOperationID(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    m_strOperationID = objReader.GetAttribute("OPERATIONID");
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
        /// <summary>
        /// ����ͬm_lngGetLastestOperationID(),���������а�����������.
        /// </summary>		
        public long m_lngGetLastestOperationMode(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsOperationIDInOperation p_objOperationID)
        {
            p_objOperationID = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;


            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetLastestOperationID(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    p_objOperationID = new clsOperationIDInOperation();

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    p_objOperationID.strOperationID = objReader.GetAttribute("OPERATIONID");
                                    p_objOperationID.strOperationName = objReader.GetAttribute("OPERATIONNAME");
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

        public long m_lngGetLastestAnaesthesiaID(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out string m_strAnaesthesiaModeID)
        {
            m_strAnaesthesiaModeID = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;

            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetLastestAnaesthesiaID(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    m_strAnaesthesiaModeID = objReader.GetAttribute("ANAESTHESIAMODEID");
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
        /// <summary>
        /// ����ͬm_lngGetLastestAnaesthesiaID(),���������а�������ʽ����.
        /// </summary>		
        public long m_lngGetLastestAnaesthesiaMode(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsAnaesthesiaModeInOperation p_objAnaesthesiaModeID)
        {
            p_objAnaesthesiaModeID = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return 0;

            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetLastestAnaesthesiaID(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);

                if (lngRes > 0 && intRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    p_objAnaesthesiaModeID = new clsAnaesthesiaModeInOperation();
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    p_objAnaesthesiaModeID.strAnaesthesiaModeID = objReader.GetAttribute("ANAESTHESIAMODEID");
                                    p_objAnaesthesiaModeID.strAnaesthesiaModeName = objReader.GetAttribute("ANAESTHESIAMODENAME");
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
        public long m_lngGetOperation_Nurse(string strInPatientID, string strInPatientDate, string p_strCreateDate, out int Rows, out clsOperationDoctorNurse[] objOperationNurse)
        {
            objOperationNurse = null;
            string m_strReceivedXML = "";
            int m_intReturnRows = 0;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngGetOperation_Nurse(strInPatientID, strInPatientDate, p_strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    objOperationNurse = new clsOperationDoctorNurse[m_intReturnRows];
                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objOperationNurse[intIndex] = new clsOperationDoctorNurse();
                                    objOperationNurse[intIndex].strNurseID = objReader.GetAttribute("OPERTORID");
                                    objOperationNurse[intIndex].strNurseFlag = objReader.GetAttribute("OPERATORFLAG");
                                    objOperationNurse[intIndex].strNurseName = objReader.GetAttribute("OPERATORNAME");
                                    intIndex++;

                                }
                                break;
                        }
                    }
                }
                Rows = m_intReturnRows;
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;

        }

        public long m_lngGetWoundThing(string strInPatientID, string strInPatientDate, string strCreateDate, out int Rows, out clsOperationWoundThingInfo[] objOperationWoundThingArr)
        {
            objOperationWoundThingArr = null;
            string m_strReceivedXML = "";
            int m_intReturnRows = 0;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetOperation_WoundThing(strInPatientID, strInPatientDate, strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    objOperationWoundThingArr = new clsOperationWoundThingInfo[m_intReturnRows];
                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objOperationWoundThingArr[intIndex] = new clsOperationWoundThingInfo();
                                    objOperationWoundThingArr[intIndex].strWoundThingID = objReader.GetAttribute("WOUNDTHINGID");
                                    objOperationWoundThingArr[intIndex].strWoundThingName = objReader.GetAttribute("WOUNDTHINGNAME");
                                    objOperationWoundThingArr[intIndex].strQuantity = objReader.GetAttribute("QUANTITY");
                                    intIndex++;
                                }
                                break;
                        }
                    }
                }
                Rows = m_intReturnRows;
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetOperationRecord_All(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsOperationRecord_All p_objOperationRecord_All)
        {
            int intRowTemp = 0;
            long lngRes = 0;
            p_objOperationRecord_All = new clsOperationRecord_All();
            lngRes = m_lngGetOperationRecord(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_objOperationRecord_All.m_objOperationRecord);
            if (lngRes <= 0) return lngRes;
            lngRes = m_lngGetOperationRecordContent(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_objOperationRecord_All.m_objOperationRecordContent);
            if (lngRes <= 0) return lngRes;
            lngRes = m_lngGetLastestOperationMode(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_objOperationRecord_All.m_objOperationID);
            if (lngRes <= 0) return lngRes;
            lngRes = m_lngGetLastestAnaesthesiaMode(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_objOperationRecord_All.m_objAnaesthesiaModeID);
            if (lngRes <= 0) return lngRes;
            lngRes = m_lngGetOperation_Nurse(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out intRowTemp, out p_objOperationRecord_All.m_objOperatorArr);
            if (lngRes <= 0) return lngRes;
            lngRes = m_lngGetWoundThing(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out intRowTemp, out p_objOperationRecord_All.m_objWoundThingArr);
            return lngRes;
        }
        #endregion

        #region ��ʼ����ʽ||������||��������||ҽ��ģ����ѯ�б�

        public long m_lngGetAnaesthesiaMode(out clsAnaesthesiaModeInOperation[] objAnaesthesiaModeInOperation)
        {
            string strXML = "";
            int intRows = 0;
            objAnaesthesiaModeInOperation = null;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetAnaesthesiaModeID(ref strXML, ref intRows);
                if (lngRes >= 0 && intRows > 0)
                {
                    objAnaesthesiaModeInOperation = new clsAnaesthesiaModeInOperation[intRows];
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
                                    objAnaesthesiaModeInOperation[intIndex] = new clsAnaesthesiaModeInOperation();
                                    objAnaesthesiaModeInOperation[intIndex].strAnaesthesiaModeID = objReader.GetAttribute("ANAESTHESIAMODEID").ToString().Replace('��', '\'');
                                    objAnaesthesiaModeInOperation[intIndex].strAnaesthesiaModeName = objReader.GetAttribute("ANAESTHESIAMODENAME").ToString().Replace('��', '\'');
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
            return lngRes;
        }


        public long m_lngGetWoundThing(string p_strQuery, out clsPublicIDAndName[] m_objWoundThingArr)
        {
            string strXML = "";
            int intRows = 0;
            m_objWoundThingArr = null;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetWoundThingIDName(p_strQuery, out strXML, out intRows);
                if (lngRes >= 0 && intRows > 0)
                {
                    m_objWoundThingArr = new clsPublicIDAndName[intRows];
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
                                    m_objWoundThingArr[intIndex] = new clsPublicIDAndName();
                                    m_objWoundThingArr[intIndex].m_strID = objReader.GetAttribute("WOUNDTHINGID").ToString().Replace('��', '\'');
                                    m_objWoundThingArr[intIndex].m_strName = objReader.GetAttribute("WOUNDTHINGNAME").ToString().Replace('��', '\'');
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
            return lngRes;

        }


        public long m_lngGetOperationID(out clsOperationIDInOperation[] m_objOperationArr)
        {
            string strXML = "";
            int intRows = 0;
            m_objOperationArr = null;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngGetOperationIDName(out strXML, out intRows);
                if (lngRes >= 0 && intRows > 0)
                {
                    m_objOperationArr = new clsOperationIDInOperation[intRows];
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
                                    m_objOperationArr[intIndex] = new clsOperationIDInOperation();
                                    m_objOperationArr[intIndex].strOperationID = objReader.GetAttribute("OPERATIONID").ToString().Replace('��', '\'');
                                    m_objOperationArr[intIndex].strOperationName = objReader.GetAttribute("OPERATIONNAME").ToString().Replace('��', '\'');
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
            return lngRes;
        }


        public System.Windows.Forms.ListViewItem[] m_lviGetEmployee(string strOperator, ref bool bolSuccess)
        {//ģ����ѯҽ����

            System.Windows.Forms.ListViewItem[] item1 = null;
            string strSetXML = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_lngXMLLikeQuery_Doctor(strOperator, ref strSetXML, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)
            {
                item1 = new System.Windows.Forms.ListViewItem[intRows];
                XmlTextReader objReader = new XmlTextReader(strSetXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;
                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                item1[intIndex] = new System.Windows.Forms.ListViewItem(objReader.GetAttribute("EMPLOYEEID").ToString().Replace('��', '\''));
                                item1[intIndex].SubItems.Add(objReader.GetAttribute("FIRSTNAME").ToString().Replace('��', '\''));
                                intIndex++;
                            }
                            break;
                    }

                }
                bolSuccess = true;

            }
            else
                bolSuccess = false;

            return item1;

        }


        #endregion

        #region CheckLastModifyDate
        public long m_lngCheckLastModifyRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate, out string strModifyUserID)
        {
            p_strLastModifyDate = "";
            strModifyUserID = "";
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngCheckLastModifyDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_strLastModifyDate, out strModifyUserID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        #endregion

        #region CheckIfFirstCreate

        public long m_lngCheckNewCreateDate(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out bool p_blnIsAddNew)
        {
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngCheckNewCreateDate(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_blnIsAddNew);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region GetDeleteUser
        public long m_lngGetDeleteUser(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strDeActiveDate, out string strDeActiveUserID)
        {
            p_strDeActiveDate = "";
            strDeActiveUserID = "";
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngCheckLastModifyDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_strDeActiveDate, out strDeActiveUserID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }


        #endregion

        #region All TreeNode(Time)
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;
            DateTime[] dtmCreateRecordDateArr = null;
            string strXml = "";
            int intRows = 0;

            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngGetTimeInfoOfAPatient(p_strInPatientID, p_strInPatientDate, ref strXml, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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
            return dtmCreateRecordDateArr;
        }

        #endregion

        #region ����
        public long m_lngSave(clsOperationRecord p_objOperationRecord, clsOperationRecordContent p_objOperationRecordContent, clsOperationDoctorNurse[] objOperationNurseArr, clsOperationRecord_OperationID m_objOperationRecordOperation, clsOperationRecord_Anaesthesia m_objOperationRecordAnaesthesia, clsOperationWoundThingInfo[] m_objWoundThingArr, bool p_blnIsAddNew)
        {
            if (p_objOperationRecord == null || p_objOperationRecordContent == null || m_objOperationRecordOperation == null || m_objOperationRecordAnaesthesia == null)
                return -1;
            //string [] m_strNurseXMLArr = m_strGetNurseXML(objOperationNurseArr ,p_blnIsAddNew);
            //string m_strOperationTableXML = m_strGetOperationTableXML(m_objOperationRecordOperation,p_blnIsAddNew);
            //string m_strAnaesthesiaTableXML = m_strGetAnaesthesiaTableXML(m_objOperationRecordAnaesthesia);
            string[] m_strNurseXMLArr = null;
            string m_strOperationTableXML = null;
            string m_strAnaesthesiaTableXML = null;
            string[] m_strWoundThingTableXMLArr = m_strGetWoundThingTableXML(m_objWoundThingArr, p_blnIsAddNew);
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                if (p_blnIsAddNew == true)
                    lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNew(m_strGetMasterXML(p_objOperationRecord, p_blnIsAddNew), m_strGetContentXML(p_objOperationRecordContent, p_blnIsAddNew), m_strNurseXMLArr, m_strOperationTableXML, m_strAnaesthesiaTableXML, m_strWoundThingTableXMLArr);
                else
                    lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngModify(m_strGetMasterXML(p_objOperationRecord, p_blnIsAddNew), m_strGetContentXML(p_objOperationRecordContent, p_blnIsAddNew), m_strNurseXMLArr, m_strOperationTableXML, m_strAnaesthesiaTableXML, m_strWoundThingTableXMLArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ɾ��

        public long m_lngDeleteRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngDelete(p_strInPatientID, p_strInPatientDate, p_strOpenDate, MDIParent.OperatorID);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region FirstPrint
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strFirstPrintDate)
        {//���µ�һ�δ�ӡʱ��		
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        #endregion

        #region Obj->XML For Save
        private string[] m_strGetNurseXML(clsOperationDoctorNurse[] objOperationNurseArr, bool IsAddNew)
        {
            if (objOperationNurseArr == null || objOperationNurseArr.Length <= 0)
                return null;
            string[] m_strXMLArr = new string[objOperationNurseArr.Length];
            for (int i1 = 0; i1 < objOperationNurseArr.Length; i1++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", objOperationNurseArr[i1].strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", objOperationNurseArr[i1].strInPatientDate.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("OPENDATE", objOperationNurseArr[i1].strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", objOperationNurseArr[i1].strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERTORID", objOperationNurseArr[i1].strNurseID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATORFLAG", objOperationNurseArr[i1].strNurseFlag.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", objOperationNurseArr[i1].strStatus.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                m_strXMLArr[i1] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return m_strXMLArr;
        }


        private string m_strGetOperationTableXML(clsOperationRecord_OperationID m_objOperationRecordOperation, bool IsAddNew)
        {
            try
            {
                if (m_objOperationRecordOperation == null)
                    return null;
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
            }

            m_objXmlWriter.WriteAttributeString("INPATIENTID", m_objOperationRecordOperation.strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", m_objOperationRecordOperation.strInPatientDate.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("OPENDATE", m_objOperationRecordOperation.strOpenDate.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", m_objOperationRecordOperation.strModifyDate.Replace('\'', '��'));
            //m_objXmlWriter.WriteAttributeString("OPERATIONID", m_objOperationRecordOperation.strOperationID.Replace('\'','��'));
            m_objXmlWriter.WriteAttributeString("STATUS", m_objOperationRecordOperation.strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }


        private string m_strGetAnaesthesiaTableXML(clsOperationRecord_Anaesthesia m_objOperationRecordAnaesthesia)
        {
            if (m_objOperationRecordAnaesthesia == null)
                return null;
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", m_objOperationRecordAnaesthesia.strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", m_objOperationRecordAnaesthesia.strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPENDATE", m_objOperationRecordAnaesthesia.strOpenDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANAESTHESIAMODEID", m_objOperationRecordAnaesthesia.strAnaesthesiaModeID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", m_objOperationRecordAnaesthesia.strModifyDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", m_objOperationRecordAnaesthesia.strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }


        private string[] m_strGetWoundThingTableXML(clsOperationWoundThingInfo[] m_objWoundThingArr, bool IsAddNew)
        {
            if (m_objWoundThingArr == null || m_objWoundThingArr.Length <= 0)
                return null;
            string[] m_strXMLArr = new string[m_objWoundThingArr.Length];
            for (int i1 = 0; i1 < m_objWoundThingArr.Length; i1++)
            {
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("WoundThing");
                m_objXmlWriter.WriteAttributeString("INPATIENTID", m_objWoundThingArr[i1].strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", m_objWoundThingArr[i1].strInPatientDate.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("OPENDATE", m_objWoundThingArr[i1].strOpenDate.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", m_objWoundThingArr[i1].strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("QUANTITY", m_objWoundThingArr[i1].strQuantity.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("WOUNDTHINGID", m_objWoundThingArr[i1].strWoundThingID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", m_objWoundThingArr[i1].strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                m_strXMLArr[i1] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return m_strXMLArr;
        }

        private string m_strGetContentXML(clsOperationRecordContent p_objOperationRecordContent, bool IsAddNew)
        {

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordContent");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOperationRecordContent.strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOperationRecordContent.strInPatientDate.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("OPENDATE", p_objOperationRecordContent.strOpenDate.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objOperationRecordContent.strLastModifyDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objOperationRecordContent.strLastModifyUserID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", p_objOperationRecordContent.strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATIENTINDATE", p_objOperationRecordContent.strPatientInDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONBEGINDATE", p_objOperationRecordContent.strOperationBeginDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONENDDATE", p_objOperationRecordContent.strOperationEndDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLEAVEDATE", p_objOperationRecordContent.strOperationLeaveDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSESCLEARHEADED", p_objOperationRecordContent.strSensesClearHeaded.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSESSLEEP", p_objOperationRecordContent.strSensesSleep.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENSESCOMA", p_objOperationRecordContent.strSensesComa.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEALLERGIC", p_objOperationRecordContent.strHaveAllergic.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVENOTALLERGIC", p_objOperationRecordContent.strHaveNotAllergic.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLERGICCONTENT", p_objOperationRecordContent.strAllergicContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONONHISBACK", p_objOperationRecordContent.strOperationLocationOnHisBack.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONSIDE", p_objOperationRecordContent.strOperationLocationSide.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONPA", p_objOperationRecordContent.strOperationLocationPA.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONPARAPLEGIC", p_objOperationRecordContent.strOperationLocationParaplegic.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONHYPOTHYROID", p_objOperationRecordContent.strOperationLocationHypothyroid.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONOTHER", p_objOperationRecordContent.strOperationLocationOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHEROPERATIONLOCATION", p_objOperationRecordContent.strOtherOperationLocation.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVENOTELECTKNIFE", p_objOperationRecordContent.strHaveNotElectKnife.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEUSEDELECTKNIFE", p_objOperationRecordContent.strHaveUsedElectKnife.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ELECTKNIFEMODEL", p_objOperationRecordContent.strElectKnifeModel.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVENOTDOUBLEPOLE", p_objOperationRecordContent.strHaveNotDoublePole.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEDOUBLEPOLE", p_objOperationRecordContent.strHaveDoublePole.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOUBLEPOLECONTENT", p_objOperationRecordContent.strDoublePoleContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CATHODELOCATION", p_objOperationRecordContent.strCathodeLocation.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CATHODELOCSKINBFOPRMAR", p_objOperationRecordContent.strCathodeLocationSkinBeforOperationMar.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CATHODELOCSKINBFOPRFULL", p_objOperationRecordContent.strCathodeLocationSkinBeforOperationFull.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CATHODELOCSKINAFOPRMAR", p_objOperationRecordContent.strCathodeLocationSkinAfterOperationMar.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CATHODELOCSKINAFOPRFULL", p_objOperationRecordContent.strCathodeLocationSkinAfterOperationFull.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STYPTICRUBBER", p_objOperationRecordContent.strStypticRubber.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STYPTICPRESSURE", p_objOperationRecordContent.strStypticPressure.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPFOREARM", p_objOperationRecordContent.strUpForearm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPTHIGH", p_objOperationRecordContent.strUpThigh.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPRIGHT", p_objOperationRecordContent.strUpRight.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPLEFT", p_objOperationRecordContent.strUpLeft.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPPUFFDATETIME", p_objOperationRecordContent.strUpPuffDateTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPDEFLATEDATETIME", p_objOperationRecordContent.strUpDeflateDateTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPTOTALDATETIME", p_objOperationRecordContent.strUpTotalDateTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("UPPRESS", p_objOperationRecordContent.strUpPress.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNFOREARM", p_objOperationRecordContent.strDownForearm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNTHIGH", p_objOperationRecordContent.strDownThigh.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNRIGHT", p_objOperationRecordContent.strDownRight.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNLEFT", p_objOperationRecordContent.strDownLeft.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNPUFFDATETIME", p_objOperationRecordContent.strDownPuffDateTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNDEFLATEDATETIME", p_objOperationRecordContent.strDownDeflateDateTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNTOTALDATETIME", p_objOperationRecordContent.strDownTotalDateTime.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOWNPRESS", p_objOperationRecordContent.strDownPress.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FOLEYSICKROOM", p_objOperationRecordContent.strFoleySickroom.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLEYOPERATIONROOM", p_objOperationRecordContent.strFoleyOperationRoom.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLEYDOUBLEANTRUM", p_objOperationRecordContent.strFoleyDoubleAntrum.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLEYTHREEANTRUM", p_objOperationRecordContent.strFoleyThreeAntrum.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLEYOTHER", p_objOperationRecordContent.strFoleyOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHSICKROOM", p_objOperationRecordContent.strStomachSickroom.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STOMACHOPRATIONROOM", p_objOperationRecordContent.strStomachOprationRoom.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTISEPSIS2", p_objOperationRecordContent.strSkinAntisepsis2.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTISEPSIS75", p_objOperationRecordContent.strSkinAntisepsis75.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTISEPSISIODIN", p_objOperationRecordContent.strSkinAntisepsisIodin.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTISEPSISIODINRARE", p_objOperationRecordContent.strSkinAntisepsisIodinRare.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTISEPSISOTHER", p_objOperationRecordContent.strSkinAntisepsisOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLBLOOD", p_objOperationRecordContent.strAllBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ALLBLOODQTY", p_objOperationRecordContent.strAllBloodQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("REDCELL", p_objOperationRecordContent.strRedCell.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("REDCELLQTY", p_objOperationRecordContent.strRedCellQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODPLASM", p_objOperationRecordContent.strBloodPlasm.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODPLASMQTY", p_objOperationRecordContent.strBloodPlasmQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OWNBLOOD", p_objOperationRecordContent.strOwnBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OWNBLOODQTY", p_objOperationRecordContent.strOwnBloodQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODOTHER", p_objOperationRecordContent.strBloodOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODOTHERQTY", p_objOperationRecordContent.strBloodOtherQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INLIQUIDQTY", p_objOperationRecordContent.strInLiquidQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PEEOPERATINGQTY", p_objOperationRecordContent.strPeeOperatingQty.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HAVEOUTFLOW", p_objOperationRecordContent.strHaveOutFlow.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NOTHAVEOUTFLOW", p_objOperationRecordContent.strNotHaveOutFlow.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINBFOPRMAR", p_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationMar.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINBFOPRFULL", p_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationFull.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINBFOPRCONT", p_objOperationRecordContent.strFromHeadToFootSkinBeforeOperationContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINAFOPRMAR", p_objOperationRecordContent.strFromHeadToFootSkinAfterOperationMar.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINAFOPRFULL", p_objOperationRecordContent.strFromHeadToFootSkinAfterOperationFull.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINAFOPRCONT", p_objOperationRecordContent.strFromHeadToFootSkinAfterOperationContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SAMPLEGENERAL", p_objOperationRecordContent.strSampleGeneral.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SAMPLESLICE", p_objOperationRecordContent.strSampleSlice.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SAMPLEBACILLI", p_objOperationRecordContent.strSampleBacilli.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SAMPLEOTHER", p_objOperationRecordContent.strSampleOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AFTEROPERATIONSENDRENEW", p_objOperationRecordContent.strAfterOperationSendRenew.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AFTEROPERATIONSENDICU", p_objOperationRecordContent.strAfterOperationSendICU.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("AFTEROPERATIONSENDSICKROOM", p_objOperationRecordContent.strAfterOperationSendSickRoom.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TENDRECORD", p_objOperationRecordContent.strTendRecord.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ANAESTHESIAMODE", p_objOperationRecordContent.strAnaesthesiaMode.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONNAME", p_objOperationRecordContent.strOperationName.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONROOM", p_objOperationRecordContent.strOperationRoom.Replace('\'', '��'));
            //
            m_objXmlWriter.WriteAttributeString("STYPTICPRESSUREMODE", p_objOperationRecordContent.strStypticPressureMode.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FOLEYOTHERCONTENT", p_objOperationRecordContent.strFoleyOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SKINANTISEPSISOTHERCONTENT", p_objOperationRecordContent.strSkinAntisepsisOtherContent.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SAMPLEOTHERCONTENT", p_objOperationRecordContent.strSampleOtherContent.Replace('\'', '��'));
            //			

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }

        public string m_strGetMasterXML(clsOperationRecord objOperationRecord, bool IsAddNew)
        {
            try
            {
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");
                m_objXmlWriter.WriteAttributeString("INPATIENTID", objOperationRecord.strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", objOperationRecord.strInPatientDate.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("OPENDATE", objOperationRecord.strOpenDate.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("CREATEDATE", objOperationRecord.strCreateDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEUSERID", objOperationRecord.strCreateUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", objOperationRecord.strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("IFCONFIRM", objOperationRecord.strIfConfirm.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SENSESXML", objOperationRecord.strSensesXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ALLERGICXML", objOperationRecord.strAllergicXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONLOCATIONXML", objOperationRecord.strOperationLocationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ELECTKNIFEXML", objOperationRecord.strElectKnifeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOUBLEPOLEXML", objOperationRecord.strDoublePoleXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CATHODELOCATIONSKINXML", objOperationRecord.strCathodeLocationSkinXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STYPTICRUBBERXML", objOperationRecord.strStypticRubberXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("UPXML", objOperationRecord.strUpXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOWNXML", objOperationRecord.strDownXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FOLEYXML", objOperationRecord.strFoleyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STOMACHXML", objOperationRecord.strStomachXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SKINANTISEPSISXML", objOperationRecord.strSkinAntisepsisXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODXML", objOperationRecord.strBloodXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INLIQUIDQTYXML", objOperationRecord.strInLiquidQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PEEOPERATINGQTYXML", objOperationRecord.strPeeOperatingQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTFLOWXML", objOperationRecord.strOutFlowXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINXML", objOperationRecord.strFromHeadToFootSkinXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SAMPLEXML", objOperationRecord.strSampleXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("AFTEROPERATIONSENDXML", objOperationRecord.strAfterOperationSendXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("TENDRECORDXML", objOperationRecord.strTendRecordXML.Replace('\'', '��'));
                //m_objXmlWriter.WriteAttributeString("ANAESTHESIAMODEXML", objOperationRecord.strAnaesthesiaModeXML.Replace('\'','��'));
                //m_objXmlWriter.WriteAttributeString("OPERATIONNAMEXML", objOperationRecord.strOperationNameXML.Replace('\'','��'));
                m_objXmlWriter.WriteAttributeString("CATHODELOCSKINAFOPRXML", objOperationRecord.strCathodeLocationSkinAfterOperationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINAFOPRXML", objOperationRecord.strFromHeadToFootSkinAfterOperationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONROOMXML", objOperationRecord.strOperationRoomXML.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("OPERATION_ANAESTHESIAXML", objOperationRecord.strOperation_AnaesthesiaXML.Replace('\'', '��'));


                m_objXmlWriter.WriteAttributeString("ALLERGICCONTENTXML", objOperationRecord.strAllergicContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OTHEROPERATIONLOCATIONXML", objOperationRecord.strOtherOperationLocationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ELECTKNIFEMODELXML", objOperationRecord.strElectKnifeModelXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOUBLEPOLECONTENTXML", objOperationRecord.strDoublePoleContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CATHODELOCATIONXML", objOperationRecord.strCathodeLocationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STYPTICPRESSUREMODEXML", objOperationRecord.strStypticPressureModeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("UPPUFFDATETIMEXML", objOperationRecord.strUpPuffDateTimeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("UPDEFLATEDATETIMEXML", objOperationRecord.strUpDeflateDateTimeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("UPTOTALDATETIMEXML", objOperationRecord.strUpTotalDateTimeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("UPPRESSXML", objOperationRecord.strUpPressXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOWNPUFFDATETIMEXML", objOperationRecord.strDownPuffDateTimeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOWNDEFLATEDATETIMEXML", objOperationRecord.strDownDeflateDateTimeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOWNTOTALDATETIMEXML", objOperationRecord.strDownTotalDateTimeXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DOWNPRESSXML", objOperationRecord.strDownPressXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FOLEYOTHERCONTENTXML", objOperationRecord.strFoleyOtherContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SKINANTISEPSISOTHERCONTENTXML", objOperationRecord.strSkinAntisepsisOtherContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ALLBLOODQTYXML", objOperationRecord.strAllBloodQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("REDCELLQTYXML", objOperationRecord.strRedCellQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODPLASMQTYXML", objOperationRecord.strBloodPlasmQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OWNBLOODQTYXML", objOperationRecord.strOwnBloodQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("BLOODOTHERQTYXML", objOperationRecord.strBloodOtherQtyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINBFOPRCONTXML", objOperationRecord.strFromHeadToFootSkinBeforeOperationContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FROMHEADTOFOOTSKINAFOPRCONTXML", objOperationRecord.strFromHeadToFootSkinAfterOperationContentXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SAMPLEOTHERCONTENTXML", objOperationRecord.strSampleOtherContentXML.Replace('\'', '��'));

                long lngSignSequence = 0;
                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objService =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));
                long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);
                m_objXmlWriter.WriteAttributeString("SEQUENCE_INT", lngSignSequence.ToString().Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddSign(objOperationRecord.objSignerArr, lngSignSequence);

                return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            catch (Exception e)
            {
                clsPublicFunction.ShowInformationMessageBox(e.Message);
            }
            return null;
        }

        #endregion

        #region Old Code 
        #region ģ����ѯ

        /// <summary>
        /// �������ʽ
        /// </summary>
        /// <returns></returns>
        public clsAnaesthesiaModeInOperation[] m_objGetAnaesthesiaMode()
        {
            string strXML = "";
            int intRows = 0;
            clsAnaesthesiaModeInOperation[] objAnaesthesiaModeInOperation = null;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetAnaesthesiaModeID(ref strXML, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes >= 0 && intRows > 0)
            {
                objAnaesthesiaModeInOperation = new clsAnaesthesiaModeInOperation[intRows];
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
                                objAnaesthesiaModeInOperation[intIndex] = new clsAnaesthesiaModeInOperation();
                                objAnaesthesiaModeInOperation[intIndex].strAnaesthesiaModeID = objReader.GetAttribute("ANAESTHESIAMODEID").ToString().Replace('��', '\'');
                                objAnaesthesiaModeInOperation[intIndex].strAnaesthesiaModeName = objReader.GetAttribute("ANAESTHESIAMODENAME").ToString().Replace('��', '\'');
                                intIndex++;
                            }
                            break;
                    }

                }

            }
            return objAnaesthesiaModeInOperation;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public clsOperationIDInOperation[] m_objGetOperationID()
        {
            string strXML = "";
            int intRows = 0;
            clsOperationIDInOperation[] m_objOperationArr = null;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationRecordServ_m_lngGetOperationIDName(out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes >= 0 && intRows > 0)
            {
                m_objOperationArr = new clsOperationIDInOperation[intRows];
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
                                m_objOperationArr[intIndex] = new clsOperationIDInOperation();
                                m_objOperationArr[intIndex].strOperationID = objReader.GetAttribute("OPERATIONID").ToString().Replace('��', '\'');
                                m_objOperationArr[intIndex].strOperationName = objReader.GetAttribute("OPERATIONNAME").ToString().Replace('��', '\'');
                                intIndex++;
                            }
                            break;
                    }

                }

            }
            return m_objOperationArr;
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        public clsPublicIDAndName[] m_objGetWoundThing(string p_strQuery)
        {
            string strXML = "";
            int intRows = 0;
            clsPublicIDAndName[] m_objWoundThingArr = null;
            long lngRes = 0;

            //com.digitalwave.OperationRecord.clsOperationRecordServ m_objServ =
            //    (com.digitalwave.OperationRecord.clsOperationRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationRecord.clsOperationRecordServ));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetWoundThingIDName(p_strQuery, out strXML, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes >= 0 && intRows > 0)
            {
                m_objWoundThingArr = new clsPublicIDAndName[intRows];
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
                                m_objWoundThingArr[intIndex] = new clsPublicIDAndName();
                                m_objWoundThingArr[intIndex].m_strID = objReader.GetAttribute("WOUNDTHINGID").ToString().Replace('��', '\'');
                                m_objWoundThingArr[intIndex].m_strName = objReader.GetAttribute("WOUNDTHINGNAME").ToString().Replace('��', '\'');
                                intIndex++;
                            }
                            break;
                    }

                }

            }
            return m_objWoundThingArr;

        }
        #endregion
        #endregion

    }
    #region ���ݴ�����

    //	[Serializable]
    //	public class clsOperationRecordContent
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strLastModifyDate;
    //		
    //		public string strLastModifyUserID;
    //		public string strStatus;
    //		public string strDeActivedDate;
    //		public string strDeActivedOperatorID;
    //
    //
    //		public string strOperationName;
    //		public string strAnaesthesiaMode;
    //		public string strOperationDoctorID;
    //		public string strAnaesthesiaDoctorID;
    //		public string strPatientInDate;
    //		public string strOperationBeginDate;
    //		public string strOperationEndDate;
    //		public string strOperationLeaveDate;
    //		public string strNoBacillusDcotorID;
    //		public string strNurseID;
    //		public string strCircuitNurseID;
    //		public string strWashHandsNurseID;
    //
    //		
    //		public string strSensesClearHeaded;
    //		public string strSensesSleep;
    //		public string strSensesComa;
    //		public string strHaveAllergic;
    //		public string strHaveNotAllergic;
    //		public string strAllergicContent;
    //		public string strOperationLocationOnHisBack;
    //		public string strOperationLocationSide;
    //		public string strOperationLocationPA;
    //		public string strOperationLocationParaplegic;
    //		public string strOperationLocationHypothyroid;
    //		public string strOperationLocationOther;
    //		public string strOtherOperationLocation;
    //		public string strHaveNotElectKnife;
    //		public string strHaveUsedElectKnife;
    //		public string strElectKnifeModel;
    //		public string strHaveNotDoublePole;
    //		public string strHaveDoublePole;
    //		public string strDoublePoleContent;
    //		public string strCathodeLocation;
    //		public string strCathodeLocationSkinBeforOperationMar;
    //		public string strCathodeLocationSkinBeforOperationFull;
    //		public string strCathodeLocationSkinAfterOperationMar;
    //		public string strCathodeLocationSkinAfterOperationFull;
    //		public string strStypticRubber;
    //		public string strStypticPressure;
    //	    public string strStypticPressureMode;
    //		public string strUpForearm;
    //		public string strUpThigh;
    //		public string strUpRight;
    //		public string strUpLeft;
    //		public string strUpPuffDateTime;
    //		public string strUpDeflateDateTime;
    //		public string strUpTotalDateTime;
    //		public string strUpPress;
    //		public string strDownForearm;
    //		public string strDownThigh;
    //		public string strDownRight;
    //		public string strDownLeft;
    //		public string strDownPuffDateTime;
    //		public string strDownDeflateDateTime;
    //		public string strDownTotalDateTime;
    //		public string strDownPress;
    //		public string strFoleySickroom;
    //		public string strFoleyOperationRoom;
    //		public string strFoleyDoubleAntrum;
    //        public string strFoleyThreeAntrum;
    //	    public string strFoleyOther;
    //        public string strFoleyOtherContent;
    //	    public string strStomachSickroom;
    //		public string strStomachOprationRoom;
    //		public string strSkinAntisepsis2;
    //		public string strSkinAntisepsis75;
    //		public string strSkinAntisepsisIodin;
    //		public string strSkinAntisepsisIodinRare;
    //		public string strSkinAntisepsisOther;
    //        public string strSkinAntisepsisOtherContent;
    //		public string strAllBlood;
    //		public string strAllBloodQty;
    //		public string strRedCell;
    //		public string strRedCellQty;
    //		public string strBloodPlasm;
    //		public string strBloodPlasmQty;
    //		public string strOwnBlood;
    //		public string strOwnBloodQty;
    //		public string strBloodOther;
    //		public string strBloodOtherQty;
    //		public string strInLiquidQty;
    //		public string strPeeOperatingQty;
    //		public string strHaveOutFlow;
    //		public string strNotHaveOutFlow;
    //		public string strFromHeadToFootSkinBeforeOperationMar;
    //		public string strFromHeadToFootSkinBeforeOperationFull;
    //		public string strFromHeadToFootSkinBeforeOperationContent;
    //		public string strFromHeadToFootSkinAfterOperationMar;
    //		public string strFromHeadToFootSkinAfterOperationFull;
    //		public string strFromHeadToFootSkinAfterOperationContent;
    //		public string strSampleGeneral;
    //		public string strSampleSlice;
    //		public string strSampleBacilli;
    //		public string strSampleOther;
    //        public string strSampleOtherContent;
    //		public string strAfterOperationSendRenew;
    //		public string strAfterOperationSendICU;
    //		public string strAfterOperationSendSickRoom;
    //		public string strTendRecord;
    //		public string strOperationRoom;
    //		
    //
    //
    //	}
    //	
    //	[Serializable]
    //	public class clsOperationRecord
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strFirstPrintDate;
    //
    //		public string strCreateDate;
    //        
    //		public string strCreateUserID;
    //		public string strStatus;
    //		public string strIfConfirm;
    //		public string strConfirmReason;
    //		public string strConfirmReasonXML;
    //
    //		public string strDeActivedDate;
    //		public string strDeActivedOperatorID;
    //	
    //
    //		public string strOperationNameXML;
    //		public string strAnaesthesiaModeXML;
    //		public string strSensesXML;
    //		public string strAllergicXML;
    //		public string strOperationLocationXML;
    //    	public string strElectKnifeXML;
    //		public string strDoublePoleXML;
    //		public string strCathodeLocationSkinXML;
    //		public string strStypticRubberXML;
    //		public string strUpXML;
    //		public string strDownXML;
    //		public string strFoleyXML;
    //		public string strStomachXML;
    //		public string strSkinAntisepsisXML;
    //		public string strBloodXML;
    //		public string strInLiquidQtyXML;
    //		public string strPeeOperatingQtyXML;
    //		public string strOutFlowXML;
    //		public string strFromHeadToFootSkinXML;
    //		public string strSampleXML;
    //		public string strAfterOperationSendXML;
    //		public string strTendRecordXML;
    //		public string strOperationRoomXML;
    //		public string strCathodeLocationSkinAfterOperationXML;
    //		public string strFromHeadToFootSkinAfterOperationXML;
    //
    //        			
    //		public string strOperation_AnaesthesiaXML;
    //		/// <summary>
    //		/// ���ڴ����ݿ����ݿ��ѯʱʹ��
    //		/// �����Ժ�ȽϺ��Ƿ�Ϊ��ͬ���ݵ�Object
    //		/// </summary>
    //		public string strXML_TotalRecord;
    //
    //		public string strPatientInDate;
    //		public string strOperationBeginDate;
    //		public string strOperationEndDate;
    //		public string strOperationLeaveDate;
    //
    //        
    //		public string strAllergicContentXML;
    //		public string strOtherOperationLocationXML;
    //		public string strElectKnifeModelXML;
    //		public string strDoublePoleContentXML;
    //		public string strCathodeLocationXML;
    //		public string strStypticPressureModeXML;
    //		public string strUpPuffDateTimeXML;
    //		public string strUpDeflateDateTimeXML;
    //		public string strUpTotalDateTimeXML;
    //		public string strUpPressXML;
    //		public string strDownPuffDateTimeXML;
    //		public string strDownDeflateDateTimeXML;
    //		public string strDownTotalDateTimeXML;
    //		public string strDownPressXML;
    //		public string strFoleyOtherContentXML;
    //		public string strSkinAntisepsisOtherContentXML;
    //		public string strAllBloodQtyXML;
    //		public string strRedCellQtyXML;
    //		public string strBloodPlasmQtyXML;
    //		public string strOwnBloodQtyXML;
    //		public string strBloodOtherQtyXML;
    //		public string strFromHeadToFootSkinBeforeOperationContentXML;
    //		public string strFromHeadToFootSkinAfterOperationContentXML;
    //		public string strSampleOtherContentXML;
    //	
    //	}
    //	
    //	public class clsOperationRecord_Anaesthesia
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //
    //		public string strAnaesthesiaModeID;
    //		public string strModifyDate;
    //		public string strStatus;
    //
    //
    //	}
    //	
    //	public class clsOperationRecord_OperationID
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strCreateDate;
    //
    //		public string strOperationID;
    //		public string strModifyDate;
    //		public string strStatus;
    //	
    //	}
    //	
    //	public class clsOperationRecord_OperatorAGroup
    //	{
    //		public string strRecordNurse;
    //		public string strWashNurse;
    //		public string strCircuitNurse;
    //
    //		public string strOperationDt;
    //		public string strAnaesthesiaDt;
    //		public string strNoBacillusNurse;
    //
    //	}

    //	[Serializable]
    //	public class clsOperationDoctorNurse
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strCreateDate;
    //		public string strLastModifyDate;
    //		public string strNurseID;
    //		public string strNurseName;//added ,jacky-2003-6-9
    //		public string strNurseFlag;
    //		public string strStatus;
    //		public string strDeActivedDate;
    //		public string strDeActivedOperatorID;
    //	}
    //
    //	[Serializable]
    //	public class clsAnaesthesiaModeInOperation
    //	{
    //		public string strAnaesthesiaModeID;
    //		public string strAnaesthesiaModeName;
    //		public override string ToString()
    //		{
    //			return strAnaesthesiaModeName ;
    //		}
    //
    //	}
    //
    //	[Serializable]
    //	public class clsOperationIDInOperation
    //	{
    //		public string strOperationID;
    //		public string strOperationName;
    //	
    //		public override string ToString()
    //		{
    //			return strOperationName ;
    //		}
    //	}
    //
    //	[Serializable]
    //	public class clsOperationWoundThingInfo
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strWoundThingID;
    //		public string strWoundThingName;
    //		public string strLastModifyDate;
    //		public string strQuantity;
    //		public string strStatus;
    //		public string strDeActivedDate;
    //		public string strDeActivedOperatorID;
    //	}
    //
    //	[Serializable]
    //	public class clsOperationRecord_All
    //	{
    //		public clsOperationRecord m_objOperationRecord;
    //		public clsOperationRecordContent m_objOperationRecordContent;
    //		public clsOperationIDInOperation m_objOperationID;
    //		public clsAnaesthesiaModeInOperation m_objAnaesthesiaModeID;
    //		public clsOperationDoctorNurse[]  m_objOperatorArr;
    //		public clsOperationWoundThingInfo[] m_objWoundThingArr;
    //	}
    #endregion

}

