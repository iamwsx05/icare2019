using System;
using System.IO;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// �� �� �� ¼ ��
    /// </summary>
    public class clsOperationRecordDoctorDomain
    {
        #region Member
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
        /// <summary>
        /// 
        /// </summary>
        //private clsOperationRecordDoctorServ m_objServ;
        #endregion

        #region Constructor
        public clsOperationRecordDoctorDomain()
        {
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

            //m_objServ = new clsOperationRecordDoctorServ();
        }
        #endregion

        #region ��ȡ��ʼ����Ϣ
        /// <summary>
        /// �������CreateDate
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public DateTime[,] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[,] dtmCreateRecordDateArr = null;

            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOperationRecordDoctorServ_m_lngGetTimeInfoOfAPatient(p_strInPatientID, p_strInPatientDate, ref strXml, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes > 0 && intRows > 0)
            {
                dtmCreateRecordDateArr = new DateTime[intRows, 2];

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
                                dtmCreateRecordDateArr[intIndex, 0] = DateTime.Parse(objReader.GetAttribute("CREATEDATE"));
                                dtmCreateRecordDateArr[intIndex, 1] = DateTime.Parse(objReader.GetAttribute("OPENDATE"));
                                intIndex++;
                            }
                            break;
                    }
                }
            }
            return dtmCreateRecordDateArr;
        }

        /// <summary>
        /// ����������ƺ�ID
        /// </summary>
        /// <returns></returns>
        public clsOperationIDInOperation[] m_objGetOperationID()
        {
            string strXML = "";
            int intRows = 0;

            clsOperationIDInOperation[] m_objOperationArr = null;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOperationRecordDoctorServ_m_lngGetOperationIDName(out strXML, out intRows);
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
                                m_objOperationArr[intIndex].strOperationID = objReader.GetAttribute("OPERATIONID").Replace('��', '\'');
                                m_objOperationArr[intIndex].strOperationName = objReader.GetAttribute("OPERATIONNAME").Replace('��', '\'');
                                intIndex++;
                            }
                            break;
                    }//end swith
                }//end while
            }//end if
            return m_objOperationArr;
        }
        #endregion

        #region Save
        public long m_lngSave(clsOperationRecordDoctor p_objOperationRecord, clsOperationRecordContentDoctor p_objOperationRecordContent, bool p_blnIsAddNew, clsOperationRecord_OperationID[] p_objOperationRecordOperationArr, clsOperationDoctorNurse[] p_objOperationNurseArr, clsOperationRecordDoctorSign objDoctorSign, clsPictureBoxValue[] p_objPics)
        {
            long lngRes = 0;
            if (p_objOperationRecord == null || p_objOperationRecordContent == null || p_objOperationRecord.objSignerArr == null)
                return -1;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            //��ȡǩ����ˮ��
            long lngSequence = 0;

            //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
            //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

            lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);


            try
            {
                //string[] strOperationXMLArr = m_strGetOperationXMLArr(p_objOperationRecordOperationArr);
                //string[] m_strNurseXMLArr = m_strGetNurseXML(p_objOperationNurseArr);
                p_objOperationRecord.int_Sequence = lngSequence;

                //if (p_blnIsAddNew == true)
                //    lngRes = m_objServ.m_lngAddNew(  m_strGetMasterXML(p_objOperationRecord), m_strGetContentXML(p_objOperationRecordContent), strOperationXMLArr, m_strNurseXMLArr, objDoctorSign, p_objOperationRecord.m_strInPatientID, p_objOperationRecord.m_strInPatientDate, p_objOperationRecord.m_strOpenDate, p_objPics);
                //else
                //    lngRes = m_objServ.m_lngModify(  m_strGetMasterXML(p_objOperationRecord), m_strGetContentXML(p_objOperationRecordContent), strOperationXMLArr, m_strNurseXMLArr, objDoctorSign, p_objOperationRecord.m_strInPatientID, p_objOperationRecord.m_strInPatientDate, p_objOperationRecord.m_strOpenDate, p_objPics);
                if (p_blnIsAddNew == true)
                    lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngAddNew(p_objOperationRecord, p_objOperationRecordContent, p_objPics);
                else
                    lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngModify(p_objOperationRecord, p_objOperationRecordContent, p_objPics);

            }
            finally
            {
                //m_objServ = null;
            }
            return lngRes;
        }

        /// <summary>
        /// ƴ��ʿ��ҽ��
        /// </summary>
        /// <param name="objclsOperationEqipmentQtyContent"></param>
        /// <returns></returns>
        private string[] m_strGetNurseXML(clsOperationDoctorNurse[] objOperationNurseArr)
        {
            if (objOperationNurseArr == null || objOperationNurseArr.Length <= 0)
                return null;
            string[] m_strXMLArr = new string[objOperationNurseArr.Length];
            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
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

        private string[] m_strGetOperationXMLArr(clsOperationRecord_OperationID[] p_objOperationRecordOperationArr)
        {
            if (p_objOperationRecordOperationArr == null || p_objOperationRecordOperationArr.Length == 0)
                return null;

            string[] strXMLArr = new string[p_objOperationRecordOperationArr.Length];
            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            for (int i = 0; i < p_objOperationRecordOperationArr.Length; i++)
            {
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOperationRecordOperationArr[i].strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOperationRecordOperationArr[i].strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objOperationRecordOperationArr[i].strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONID", p_objOperationRecordOperationArr[i].strOperationID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objOperationRecordOperationArr[i].strModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", p_objOperationRecordOperationArr[i].strStatus.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();
                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return strXMLArr;

        }

        private string m_strGetMasterXML(clsOperationRecordDoctor objOperationRecord)
        {
            try
            {
                m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
                m_objXmlWriter.Flush();//���ԭ�����ַ�
                m_objXmlMemStream.SetLength(0);
                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", objOperationRecord.m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", objOperationRecord.m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEDATE", objOperationRecord.m_strCreateDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", objOperationRecord.m_strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEUSERID", objOperationRecord.m_strCreateUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", objOperationRecord.m_strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("IFCONFIRM", objOperationRecord.m_strIfConfirm.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", objOperationRecord.m_strDeActivedDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", objOperationRecord.m_strDeActivedOperatorID.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("ANAESTHESIABEFOREOPERATIONXML", objOperationRecord.m_strAnaesthesiaBeforeOperationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIAINOPERATIONXML", objOperationRecord.m_strAnaesthesiaInOperationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIACATEGORYDOSAGEXML", objOperationRecord.m_strAnaesthesiaCategoryDosageXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DIAGNOSEBEFOREOPERATIONXML", objOperationRecord.m_strDiagnoseBeforeOperationXML.Replace('\'', '��'));


                m_objXmlWriter.WriteAttributeString("OPERATIONNAMEXML", objOperationRecord.m_strOperationNameXML.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("DIAGNOSEAFTEROPERATIONXML", objOperationRecord.m_strDiagnoseAfterOperationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONPROCESSXML", objOperationRecord.m_strOperationProcessXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PATHOLOGYXML", objOperationRecord.m_strPathologyXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SAMPLEOREXTRARECORDXML", objOperationRecord.m_strSampleOrExtraRecordXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SUMMARYAFTEROPERATIONXML", objOperationRecord.m_strSummaryAfterOperationXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INLIQUIDXML", objOperationRecord.m_strInLiquidXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTFLOWXML", objOperationRecord.m_strOutFlowXML.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("XRAYNUMBERXML", objOperationRecord.m_strXRayNumberXML.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("SEQUENCE_INT", objOperationRecord.int_Sequence.ToString().Replace('\'', '��'));


                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        private string m_strGetContentXML(clsOperationRecordContentDoctor p_objOperationRecordContent)
        {
            try
            {
                m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
                m_objXmlWriter.Flush();//���ԭ�����ַ�

                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordContent");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOperationRecordContent.m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOperationRecordContent.m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPENDATE", p_objOperationRecordContent.m_strOpenDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYUSERID", p_objOperationRecordContent.m_strLastModifyUserID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("LASTMODIFYDATE", p_objOperationRecordContent.m_strLastModifyDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("STATUS", p_objOperationRecordContent.m_strStatus.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", p_objOperationRecordContent.m_strDeActivedDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", p_objOperationRecordContent.m_strDeActivedOperatorID.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("DIAGNOSEBEFOREOPERATION", p_objOperationRecordContent.m_strDiagnoseBeforeOperation.Replace('\'', '��'));

                m_objXmlWriter.WriteAttributeString("OPERATIONNAME", p_objOperationRecordContent.m_strOperationName.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("DIAGNOSEAFTEROPERATION", p_objOperationRecordContent.m_strDiagnoseAfterOperation.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONBEGINDATE", p_objOperationRecordContent.m_strOperationBeginDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONENDDATE", p_objOperationRecordContent.m_strOperationEndDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIABEFOREOPERATION", p_objOperationRecordContent.m_strAnaesthesiaBeforeOperation.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIAINOPERATION", p_objOperationRecordContent.m_strAnaesthesiaInOperation.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIACATEGORYDOSAGE", p_objOperationRecordContent.m_strAnaesthesiaCategoryDosage.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIABEGINDATE", p_objOperationRecordContent.m_strAnaesthesiaBeginDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHESIAENDDATE", p_objOperationRecordContent.m_strAnaesthesiaEndDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATIONPROCESS", p_objOperationRecordContent.m_strOperationProcess.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("PATHOLOGY", p_objOperationRecordContent.m_strPathology.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INLIQUID", p_objOperationRecordContent.m_strInLiquid.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OUTFLOW", p_objOperationRecordContent.m_strOutFlow.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SAMPLEOREXTRARECORD", p_objOperationRecordContent.m_strSampleOrExtraRecord.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SUMMARYAFTEROPERATION", p_objOperationRecordContent.m_strSummaryAfterOperation.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("XRAYNUMBER", p_objOperationRecordContent.m_strXRayNumber.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("ANAESTHER", p_objOperationRecordContent.m_strAnaesther.Replace('\'', '��'));
                //m_objXmlWriter.WriteAttributeString("DOCTOR1", p_objOperationRecordContent.m_strDoctor1.Replace('\'','��'));
                //m_objXmlWriter.WriteAttributeString("DOCTOR2", p_objOperationRecordContent.m_strDoctor2.Replace('\'','��'));
                //			//����ҽʦǩ��
                //m_objXmlWriter.WriteAttributeString("OPERATIONDOCTOR", p_objOperationRecordContent.m_strOperationDoctor.Replace('\'','��'));
                //			//����ǩ��
                //m_objXmlWriter.WriteAttributeString("ASSISTANT", p_objOperationRecordContent.m_strAssistant.Replace('\'','��'));

                //��ʿǩ��
                //m_objXmlWriter.WriteAttributeString("NURSE", p_objOperationRecordContent.m_strNurse.Replace('\'','��'));

                //m_objXmlWriter.WriteAttributeString("OPERATIONDOCTORID", p_objOperationRecordContent.m_strOperationDoctorID.Replace('\'','��'));
                //m_objXmlWriter.WriteAttributeString("OPERATIONDOCTORNAME", p_objOperationRecordContent.m_strOperationDoctorName.Replace('\'','��'));
                m_objXmlWriter.WriteAttributeString("FIRSTASSISTANTID", p_objOperationRecordContent.m_strFirstAssistantID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("FIRSTASSISTANTNAME", p_objOperationRecordContent.m_strFirstAssistantName.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SECONDASSISTANTID", p_objOperationRecordContent.m_strSecondAssistantID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("SECONDASSISTANTNAME", p_objOperationRecordContent.m_strSecondAssistantName.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
        #endregion

        #region ��ȡ��ʾ��Ϣ
        /// <summary>
        /// ��ȡ��ʿҽ������Ϣ
        /// </summary>
        /// <param name="strInPatientID"></param>
        /// <param name="strInPatientDate"></param>
        /// <param name="strCreateDate"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public clsOperationDoctorNurse[] m_lngGetOperation_Nurse(string strInPatientID, string strInPatientDate, string strCreateDate, out int Rows)
        {
            clsOperationDoctorNurse[] objOperationNurse = null;
            string m_strReceivedXML = "";
            int m_intReturnRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                long lngSucceed = (new weCare.Proxy.ProxyEmr04()).Service.clsOperationRecordDoctorServ_m_lngGetOperation_Nurse(strInPatientID, strInPatientDate, strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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
                                objOperationNurse[intIndex].strNurseName = objReader.GetAttribute("FIRSTNAME");
                                intIndex++;

                            }
                            break;
                    }
                }
            }
            Rows = m_intReturnRows;
            return objOperationNurse;

        }

        /// <summary>
        /// ��ôӱ����Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsOperationRecordContentDoctor m_objGetOperationRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return null;

            clsOperationRecordContentDoctor objOperationRecordContent = null;

            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOperationRecordDoctorServ_m_lngGetOperationRecordContent(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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

                                objOperationRecordContent = new clsOperationRecordContentDoctor();

                                objOperationRecordContent.m_strInPatientID = objReader.GetAttribute("INPATIENTID");
                                objOperationRecordContent.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE");
                                objOperationRecordContent.m_strOpenDate = objReader.GetAttribute("OPENDATE");
                                objOperationRecordContent.m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE");
                                objOperationRecordContent.m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID");
                                objOperationRecordContent.m_strStatus = objReader.GetAttribute("STATUS");
                                objOperationRecordContent.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE");
                                objOperationRecordContent.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID");

                                objOperationRecordContent.m_strDiagnoseBeforeOperation = objReader.GetAttribute("DIAGNOSEBEFOREOPERATION").Replace('��', '\'');

                                objOperationRecordContent.m_strOperationName = objReader.GetAttribute("OPERATIONNAME").Replace('��', '\'');
                                objOperationRecordContent.m_strDiagnoseAfterOperation = objReader.GetAttribute("DIAGNOSEAFTEROPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strOperationBeginDate = objReader.GetAttribute("OPERATIONBEGINDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strOperationEndDate = objReader.GetAttribute("OPERATIONENDDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaBeforeOperation = objReader.GetAttribute("ANAESTHESIABEFOREOPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaInOperation = objReader.GetAttribute("ANAESTHESIAINOPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaCategoryDosage = objReader.GetAttribute("ANAESTHESIACATEGORYDOSAGE").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaBeginDate = objReader.GetAttribute("ANAESTHESIABEGINDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaEndDate = objReader.GetAttribute("ANAESTHESIAENDDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strOperationProcess = objReader.GetAttribute("OPERATIONPROCESS").Replace('��', '\'');
                                objOperationRecordContent.m_strPathology = objReader.GetAttribute("PATHOLOGY").Replace('��', '\'');
                                objOperationRecordContent.m_strInLiquid = objReader.GetAttribute("INLIQUID").Replace('��', '\'');
                                objOperationRecordContent.m_strOutFlow = objReader.GetAttribute("OUTFLOW").Replace('��', '\'');
                                objOperationRecordContent.m_strSampleOrExtraRecord = objReader.GetAttribute("SAMPLEOREXTRARECORD").Replace('��', '\'');
                                objOperationRecordContent.m_strSummaryAfterOperation = objReader.GetAttribute("SUMMARYAFTEROPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strXRayNumber = objReader.GetAttribute("XRAYNUMBER").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesther = objReader.GetAttribute("ANAESTHER").Replace('��', '\'');
                                objOperationRecordContent.m_strDoctor1 = objReader.GetAttribute("DOCTOR1").Replace('��', '\'');
                                objOperationRecordContent.m_strDoctor2 = objReader.GetAttribute("DOCTOR2").Replace('��', '\'');
                                objOperationRecordContent.m_strAssistant = objReader.GetAttribute("ASSISTANT").Replace('��', '\'');//����ǩ��
                                objOperationRecordContent.m_strOperationDoctor = objReader.GetAttribute("OPERATIONDOCTOR").Replace('��', '\'');//����ҽʦǩ��
                                objOperationRecordContent.m_strNurse = objReader.GetAttribute("NURSE").Replace('��', '\'');//��ʿǩ��
                            }
                            break;
                    }
                }
            }

            return objOperationRecordContent;


        }

        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsOperationRecordDoctor m_objGetOperationRecord(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return null;

            clsOperationRecordDoctor objOperationRecord = null;
            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOperationRecord(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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
                                objOperationRecord = new clsOperationRecordDoctor();
                                objOperationRecord.m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                                objOperationRecord.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                                objOperationRecord.m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                                objOperationRecord.m_strCreateDate = objReader.GetAttribute("CREATEDATE").Replace('��', '\'');
                                objOperationRecord.m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                                objOperationRecord.m_strFirstPrintDate = objReader.GetAttribute("FIRSTPRINTDATE").Replace('��', '\'');
                                objOperationRecord.m_strIfConfirm = objReader.GetAttribute("IFCONFIRM").Replace('��', '\'');
                                objOperationRecord.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                                objOperationRecord.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                                objOperationRecord.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\''); ;
                                objOperationRecord.m_strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").Replace('��', '\'');
                                objOperationRecord.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID").Replace('��', '\'');

                                objOperationRecord.m_strAnaesthesiaBeforeOperationXML = objReader.GetAttribute("ANAESTHESIABEFOREOPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strAnaesthesiaInOperationXML = objReader.GetAttribute("ANAESTHESIAINOPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strAnaesthesiaCategoryDosageXML = objReader.GetAttribute("ANAESTHESIACATEGORYDOSAGEXML").Replace('��', '\'');
                                objOperationRecord.m_strDiagnoseBeforeOperationXML = objReader.GetAttribute("DIAGNOSEBEFOREOPERATIONXML").Replace('��', '\'');

                                objOperationRecord.m_strOperationNameXML = objReader.GetAttribute("OPERATIONNAMEXML").Replace('��', '\'');
                                objOperationRecord.m_strDiagnoseAfterOperationXML = objReader.GetAttribute("DIAGNOSEAFTEROPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strOperationProcessXML = objReader.GetAttribute("OPERATIONPROCESSXML").Replace('��', '\'');
                                objOperationRecord.m_strPathologyXML = objReader.GetAttribute("PATHOLOGYXML").Replace('��', '\'');
                                objOperationRecord.m_strSampleOrExtraRecordXML = objReader.GetAttribute("SAMPLEOREXTRARECORDXML").Replace('��', '\'');
                                objOperationRecord.m_strSummaryAfterOperationXML = objReader.GetAttribute("SUMMARYAFTEROPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strInLiquidXML = objReader.GetAttribute("INLIQUIDXML").Replace('��', '\'');
                                objOperationRecord.m_strOutFlowXML = objReader.GetAttribute("OUTFLOWXML").Replace('��', '\'');
                                objOperationRecord.m_strXRayNumberXML = objReader.GetAttribute("XRAYNUMBERXML").Replace('��', '\'');

                                try
                                {
                                    //��ȡǩ��
                                    objOperationRecord.int_Sequence = Convert.ToInt64(objReader.GetAttribute("SEQUENCE_INT").Replace('��', '\''));

                                    long lngS = objOperationRecord.int_Sequence;

                                    //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign =
                                    //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                                    long lngTemp = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetSign(lngS, out objOperationRecord.objSignerArr);

                                    //�ͷ�
                                    //objSign = null;

                                }
                                catch (Exception)
                                {

                                    objOperationRecord.int_Sequence = 0;
                                }

                                objOperationRecord.m_strXML_TotalRecord = strXml;
                            }
                            break;
                    }
                }
            }

            return objOperationRecord;

        }

        /// <summary>
        /// ���������������ID
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public long m_lngGetLastestOperationIDArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out string[] strOpertionIDArr)
        {
            strOpertionIDArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return -1;

            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetLastestOperationIDArr(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out strXml, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes > 0 && intRows > 0)
            {
                XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                strOpertionIDArr = new string[intRows];
                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                strOpertionIDArr[intIndex++] = objReader.GetAttribute("OPERATIONID");
                            }
                            break;
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�Ѿ���ɾ������Ϣ		
        /// <summary>
        /// ��ôӱ����Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsOperationRecordContentDoctor m_objGetDeletedOperationRecordContent(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return null;

            clsOperationRecordContentDoctor objOperationRecordContent = null;

            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeletedOperationRecordContent(p_strInPatientID, p_strInPatientDate, p_strOpenDate, ref strXml, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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

                                objOperationRecordContent = new clsOperationRecordContentDoctor();

                                objOperationRecordContent.m_strInPatientID = objReader.GetAttribute("INPATIENTID");
                                objOperationRecordContent.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE");
                                objOperationRecordContent.m_strOpenDate = objReader.GetAttribute("OPENDATE");
                                objOperationRecordContent.m_strLastModifyDate = objReader.GetAttribute("LASTMODIFYDATE");
                                objOperationRecordContent.m_strLastModifyUserID = objReader.GetAttribute("LASTMODIFYUSERID");
                                objOperationRecordContent.m_strStatus = objReader.GetAttribute("STATUS");
                                objOperationRecordContent.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE");
                                objOperationRecordContent.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID");

                                objOperationRecordContent.m_strDiagnoseBeforeOperation = objReader.GetAttribute("DIAGNOSEBEFOREOPERATION").Replace('��', '\'');

                                objOperationRecordContent.m_strOperationName = objReader.GetAttribute("OPERATIONNAME").Replace('��', '\'');
                                objOperationRecordContent.m_strDiagnoseAfterOperation = objReader.GetAttribute("DIAGNOSEAFTEROPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strOperationBeginDate = objReader.GetAttribute("OPERATIONBEGINDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strOperationEndDate = objReader.GetAttribute("OPERATIONENDDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaBeforeOperation = objReader.GetAttribute("ANAESTHESIABEFOREOPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaInOperation = objReader.GetAttribute("ANAESTHESIAINOPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaCategoryDosage = objReader.GetAttribute("ANAESTHESIACATEGORYDOSAGE").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaBeginDate = objReader.GetAttribute("ANAESTHESIABEGINDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strAnaesthesiaEndDate = objReader.GetAttribute("ANAESTHESIAENDDATE").Replace('��', '\'');
                                objOperationRecordContent.m_strOperationProcess = objReader.GetAttribute("OPERATIONPROCESS").Replace('��', '\'');
                                objOperationRecordContent.m_strPathology = objReader.GetAttribute("PATHOLOGY").Replace('��', '\'');
                                objOperationRecordContent.m_strInLiquid = objReader.GetAttribute("INLIQUID").Replace('��', '\'');
                                objOperationRecordContent.m_strOutFlow = objReader.GetAttribute("OUTFLOW").Replace('��', '\'');
                                objOperationRecordContent.m_strSampleOrExtraRecord = objReader.GetAttribute("SAMPLEOREXTRARECORD").Replace('��', '\'');
                                objOperationRecordContent.m_strSummaryAfterOperation = objReader.GetAttribute("SUMMARYAFTEROPERATION").Replace('��', '\'');
                                objOperationRecordContent.m_strXRayNumber = objReader.GetAttribute("XRAYNUMBER").Replace('��', '\'');
                            }
                            break;
                    }
                }
            }

            return objOperationRecordContent;


        }

        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsOperationRecordDoctor m_objGetDeletedOperationRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return null;

            clsOperationRecordDoctor objOperationRecord = null;
            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeletedOperationRecord(p_strInPatientID, p_strInPatientDate, p_strOpenDate, ref strXml, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
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
                                objOperationRecord = new clsOperationRecordDoctor();
                                objOperationRecord.m_strInPatientID = objReader.GetAttribute("INPATIENTID").Replace('��', '\'');
                                objOperationRecord.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").Replace('��', '\'');
                                objOperationRecord.m_strOpenDate = objReader.GetAttribute("OPENDATE").Replace('��', '\'');
                                objOperationRecord.m_strCreateDate = objReader.GetAttribute("CREATEDATE").Replace('��', '\'');
                                objOperationRecord.m_strStatus = objReader.GetAttribute("STATUS").Replace('��', '\'');
                                objOperationRecord.m_strFirstPrintDate = objReader.GetAttribute("FIRSTPRINTDATE").Replace('��', '\'');
                                objOperationRecord.m_strIfConfirm = objReader.GetAttribute("IFCONFIRM").Replace('��', '\'');
                                objOperationRecord.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").Replace('��', '\'');
                                objOperationRecord.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").Replace('��', '\'');
                                objOperationRecord.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").Replace('��', '\''); ;
                                objOperationRecord.m_strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").Replace('��', '\'');
                                objOperationRecord.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID").Replace('��', '\'');

                                objOperationRecord.m_strAnaesthesiaBeforeOperationXML = objReader.GetAttribute("ANAESTHESIABEFOREOPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strAnaesthesiaInOperationXML = objReader.GetAttribute("ANAESTHESIAINOPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strAnaesthesiaCategoryDosageXML = objReader.GetAttribute("ANAESTHESIACATEGORYDOSAGEXML").Replace('��', '\'');
                                objOperationRecord.m_strDiagnoseBeforeOperationXML = objReader.GetAttribute("DIAGNOSEBEFOREOPERATIONXML").Replace('��', '\'');

                                objOperationRecord.m_strOperationNameXML = objReader.GetAttribute("OPERATIONNAMEXML").Replace('��', '\'');
                                objOperationRecord.m_strDiagnoseAfterOperationXML = objReader.GetAttribute("DIAGNOSEAFTEROPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strOperationProcessXML = objReader.GetAttribute("OPERATIONPROCESSXML").Replace('��', '\'');
                                objOperationRecord.m_strPathologyXML = objReader.GetAttribute("PATHOLOGYXML").Replace('��', '\'');
                                objOperationRecord.m_strSampleOrExtraRecordXML = objReader.GetAttribute("SAMPLEOREXTRARECORDXML").Replace('��', '\'');
                                objOperationRecord.m_strSummaryAfterOperationXML = objReader.GetAttribute("SUMMARYAFTEROPERATIONXML").Replace('��', '\'');
                                objOperationRecord.m_strInLiquidXML = objReader.GetAttribute("INLIQUIDXML").Replace('��', '\'');
                                objOperationRecord.m_strOutFlowXML = objReader.GetAttribute("OUTFLOWXML").Replace('��', '\'');
                                objOperationRecord.m_strXRayNumberXML = objReader.GetAttribute("XRAYNUMBERXML").Replace('��', '\'');

                                objOperationRecord.m_strXML_TotalRecord = strXml;
                            }
                            break;
                    }
                }
            }

            return objOperationRecord;

        }

        /// <summary>
        /// ���������������ID
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public long m_lngGetDeletedLastestOperationIDArr(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string[] strOpertionIDArr)
        {
            strOpertionIDArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return -1;

            string strXml = "";
            int intRows = 0;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDeletedLastestOperationIDArr(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out strXml, out intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (lngRes > 0 && intRows > 0)
            {
                XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                strOpertionIDArr = new string[intRows];
                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                strOpertionIDArr[intIndex++] = objReader.GetAttribute("OPERATIONID");
                            }
                            break;
                    }
                }
            }
            return lngRes;
        }
        #endregion

        #region Delete
        public long m_lngDeleteRecord(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strOperatorID)
        {
            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.clsOperationRecordDoctorServ_m_lngDelete(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strOperatorID);
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
            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
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

        /// <summary>
        /// ��Ӽ�¼ʱ��ѯ�û������ʱ�������ظ�
        /// </summary>		
        public long m_lngRecordExist(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out bool p_blnExist)
        {
            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngRecordExist(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_blnExist);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ���Ҹ�����¼�ĵ�һ�δ�ӡʱ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        public long m_lngGetFirstPrintDate(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strFirstPrintDate)
        {
            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_strFirstPrintDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// �������޸�ʱ��
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strLastModifyDate"></param>
        /// <returns></returns>
        public long m_lngGetOperationRecordLastModifyDate(string p_strPatientID, string p_strInPatientDate, string p_strOpenDate, out string p_strLastModifyDate)
        {
            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOperationRecordLastModifyDate(p_strPatientID, p_strInPatientDate, p_strOpenDate, out p_strLastModifyDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngGetDoctorSign(string p_strInPatientID, string p_strInPatientDate,
            string p_strOpenDate, out clsOperationRecordDoctorSign p_objRecordContent)
        {
            p_objRecordContent = null;

            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetDoctorSign(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡͼƬ��Ϣ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetPics(string p_strInPatientID, string p_strInPatientDate,
            string p_strOpenDate, out clsPictureBoxValue[] p_objPics)
        {
            p_objPics = null;
            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;

            //clsOperationRecordDoctorServ m_objServ =
            //    (clsOperationRecordDoctorServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetPics(p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objPics);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
    }

    //	/// <summary>
    //	/// �������Ϣ
    //	/// </summary>
    //	/// 
    //	[Serializable]
    //	public class clsOperationRecordDoctor
    //	{
    //		public string m_strInPatientID;
    //		public string m_strInPatientDate;
    //		public string m_strOpenDate;
    //		public string m_strCreateDate;
    //		public string m_strCreateUserID;
    //		
    //		public string m_strIfConfirm;
    //		public string m_strConfirmReason;
    //		public string m_strConfirmReasonXML;
    //		
    //		public string m_strStatus;
    //
    //		public string m_strFirstPrintDate;
    //
    //		public string m_strDeActivedDate;
    //		public string m_strDeActivedOperatorID;
    //
    //		public string m_strAnaesthesiaBeforeOperationXML;
    //		public string m_strAnaesthesiaInOperationXML;
    //		public string m_strAnaesthesiaCategoryDosageXML;
    //		public string m_strDiagnoseBeforeOperationXML;
    //		public string m_strDiagnoseAfterOperationXML;
    //		public string m_strOperationProcessXML;
    //		public string m_strPathologyXML;
    //		public string m_strSampleOrExtraRecordXML;
    //		public string m_strSummaryAfterOperationXML;
    //		public string m_strInLiquidXML;
    //		public string m_strOutFlowXML;
    //
    //		/// <summary>
    //		/// ���ڴ����ݿ����ݿ��ѯʱʹ��
    //		/// �����Ժ�ȽϺ��Ƿ�Ϊ��ͬ���ݵ�Object
    //		/// </summary>
    //		public string m_strXML_TotalRecord;
    //
    //		public string m_strXRayNumberXML;
    //
    //		public string m_strOperationNameXML;
    //	}
    //
    //	/// <summary>
    //	/// �ӱ����Ϣ
    //	/// </summary>
    //	/// 
    //	[Serializable]
    //	public class clsOperationRecordContentDoctor
    //	{
    //		public string m_strInPatientID;
    //		public string m_strInPatientDate;
    //		public string m_strOpenDate;
    //		public string m_strLastModifyDate;
    //
    //		public string m_strLastModifyUserID;
    //		public string m_strDeActivedDate;
    //		public string m_strDeActivedOperatorID;
    //		public string m_strStatus;
    //
    //		public string m_strDiagnoseBeforeOperation;
    //		public string m_strDiagnoseAfterOperation;
    //		public string m_strOperationBeginDate;
    //		public string m_strOperationEndDate;
    //		public string m_strAnaesthesiaBeforeOperation;
    //		public string m_strAnaesthesiaInOperation;
    //		public string m_strAnaesthesiaCategoryDosage;
    //		public string m_strAnaesthesiaBeginDate;
    //		public string m_strAnaesthesiaEndDate;
    //		public string m_strOperationProcess;
    //		public string m_strPathology;
    //		public string m_strInLiquid;
    //		public string m_strOutFlow;
    //		public string m_strSampleOrExtraRecord;
    //		public string m_strSummaryAfterOperation;
    //		public string m_strXRayNumber;
    //
    //		public string m_strOperationName;//��������
    //	}
}
