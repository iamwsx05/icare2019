using System;
using System.IO;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsPathologyOrgCheckOrderDomain.
    /// </summary>
    public class clsPathologyOrgCheckOrderDomain
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

        //private clsPathologyOrgCheckOrderServ m_objServ;
        #endregion

        #region Constructor
        public clsPathologyOrgCheckOrderDomain()
        {
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

            //m_objServ = new clsPathologyOrgCheckOrderServ();
        }
        #endregion

        #region ��ȡ��ʼ����Ϣ
        /// <summary>
        /// �������CreateDate
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <returns></returns>
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[] dtmCreateRecordDateArr = null;

            //clsPathologyOrgCheckOrderServ m_objServ =
            //    (clsPathologyOrgCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPathologyOrgCheckOrderServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsPathologyOrgCheckOrderServ_m_lngGetTimeInfoOfAPatient(p_strInPatientID, p_strInPatientDate, ref strXml, ref intRows);

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
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return dtmCreateRecordDateArr;
        }
        #endregion

        #region Save
        public long m_lngSave(clsPathologyOrgCheckOrderInfo p_objPathologyOrgCheckOrde, clsPathologyOrgCheckOperatorID[] p_objOperatorArr)
        {
            if (p_objPathologyOrgCheckOrde == null)
                return -1;
            long lngRes = 0;

            //clsPathologyOrgCheckOrderServ m_objServ =
            //    (clsPathologyOrgCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPathologyOrgCheckOrderServ));

            try
            {
                string strOrderXML = m_strOrderXML(p_objPathologyOrgCheckOrde);

                string[] strOperatorXML = m_strOperatorXML(p_objOperatorArr);

                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngAddNew(strOrderXML, strOperatorXML);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        private string[] m_strOperatorXML(clsPathologyOrgCheckOperatorID[] p_objOperatorArr)
        {
            if (p_objOperatorArr == null)
                return null;

            string[] strXMLArr = new string[p_objOperatorArr.Length];

            for (int i = 0; i < p_objOperatorArr.Length; i++)
            {
                if (p_objOperatorArr[i] == null)
                    continue;
                m_objXmlMemStream.SetLength(0);

                m_objXmlWriter.WriteStartDocument();
                m_objXmlWriter.WriteStartElement("RecordMaster");

                m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objOperatorArr[i].m_strInPatientID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objOperatorArr[i].m_strInPatientDate.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objOperatorArr[i].m_strCreateDate.Replace('\'', '��'));
                //				m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objOperatorArr[i].m_strModifyDate.Replace('\'','��'));

                m_objXmlWriter.WriteAttributeString("OPERATORID", p_objOperatorArr[i].m_strOperatorID.Replace('\'', '��'));
                m_objXmlWriter.WriteAttributeString("OPERATORFLAG", p_objOperatorArr[i].m_strOperatorFlag.Replace('\'', '��'));

                m_objXmlWriter.WriteEndElement();
                m_objXmlWriter.WriteEndDocument();
                m_objXmlWriter.Flush();

                strXMLArr[i] = System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
            }
            return strXMLArr;
        }

        private string m_strOrderXML(clsPathologyOrgCheckOrderInfo p_objPathologyOrgCheckOrde)
        {
            if (p_objPathologyOrgCheckOrde == null)
                return null;

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", p_objPathologyOrgCheckOrde.m_strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objPathologyOrgCheckOrde.m_strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CREATEDATE", p_objPathologyOrgCheckOrde.m_strCreateDate.Replace('\'', '��'));
            //			m_objXmlWriter.WriteAttributeString("MODIFYDATE", p_objPathologyOrgCheckOrde.m_strModifyDate.Replace('\'','��'));
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", p_objPathologyOrgCheckOrde.m_strCreateUserID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", p_objPathologyOrgCheckOrde.m_strStatus.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("IFCONFIRM", p_objPathologyOrgCheckOrde.m_strIfConfirm.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("MEDICALCHECKNO", p_objPathologyOrgCheckOrde.m_strMedicalCheckNo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HOSPITALNAME", p_objPathologyOrgCheckOrde.m_strHospitalName.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LASTCHECKNUMBER", p_objPathologyOrgCheckOrde.m_strLastCheckNumber.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENDTHINGS", p_objPathologyOrgCheckOrde.m_strSendThings.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FROMBODY", p_objPathologyOrgCheckOrde.m_strFromBody.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SICKENPERIOD", p_objPathologyOrgCheckOrde.m_strSickenPeriod.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HISTORY", p_objPathologyOrgCheckOrde.m_strHistory.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CLINICALINFO", p_objPathologyOrgCheckOrde.m_strClinicalInfo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONINFO", p_objPathologyOrgCheckOrde.m_strOperationInfo.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHECKAIM", p_objPathologyOrgCheckOrde.m_strCheckAim.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIOLOGYCHEMISTRY", p_objPathologyOrgCheckOrde.m_strBiologyChemistry.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOOD", p_objPathologyOrgCheckOrde.m_strBlood.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XRAY", p_objPathologyOrgCheckOrde.m_strXRay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BLOODSERUM", p_objPathologyOrgCheckOrde.m_strBloodSerum.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OTHER", p_objPathologyOrgCheckOrde.m_strOther.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CLINICALDIGNOSE", p_objPathologyOrgCheckOrde.m_strClinicalDignose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SENDDATE", p_objPathologyOrgCheckOrde.m_strSendDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RECEIVEDATE", p_objPathologyOrgCheckOrde.m_strReceiveDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("COLORANDSLICE", p_objPathologyOrgCheckOrde.m_strColorAndSlice.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("EYECHECK", p_objPathologyOrgCheckOrde.m_strEyeCheck.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORGANISEBURYFULL", p_objPathologyOrgCheckOrde.m_strOrganiseBuryFull.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ORGANISESTAY", p_objPathologyOrgCheckOrde.m_strOrganiseStay.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("EYESAMPLE", p_objPathologyOrgCheckOrde.m_strEyeSample.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PATHOLOGYDIGNOSE", p_objPathologyOrgCheckOrde.m_strPathologyDignose.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("REPORTDATE", p_objPathologyOrgCheckOrde.m_strReportDate.Replace('\'', '��'));
            //���ǩ�� tfzhang 2005-7-8 17:25
            m_objXmlWriter.WriteAttributeString("DOCTORID", p_objPathologyOrgCheckOrde.m_strDoctorID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DOCTORNAME", p_objPathologyOrgCheckOrde.m_strDoctorName.Replace('\'', '��'));
            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsPathologyOrgCheckOrderInfo m_objGetPathologyOrgCheckOrder(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return null;

            clsPathologyOrgCheckOrderInfo objPathologyOrgCheckOrder = null;

            //clsPathologyOrgCheckOrderServ m_objServ =
            //    (clsPathologyOrgCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPathologyOrgCheckOrderServ));

            try
            {
                string strXml = "";
                int intRows = 0;

                long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.GetPathologyOrgCheckOrder(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

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
                                    objPathologyOrgCheckOrder = new clsPathologyOrgCheckOrderInfo();

                                    objPathologyOrgCheckOrder.m_strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strCreateDate = objReader.GetAttribute("CREATEDATE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strStatus = objReader.GetAttribute("STATUS").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strIfConfirm = objReader.GetAttribute("IFCONFIRM").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strDeActivedDate = objReader.GetAttribute("DEACTIVEDDATE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strDeActivedOperatorID = objReader.GetAttribute("DEACTIVEDOPERATORID").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strConfirmReason = objReader.GetAttribute("CONFIRMREASON").ToString().Replace('��', '\''); ;
                                    objPathologyOrgCheckOrder.m_strConfirmReasonXML = objReader.GetAttribute("CONFIRMREASONXML").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strCreateUserID = objReader.GetAttribute("CREATEUSERID").ToString().Replace('��', '\'');

                                    objPathologyOrgCheckOrder.m_strMedicalCheckNo = objReader.GetAttribute("MEDICALCHECKNO").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strHospitalName = objReader.GetAttribute("HOSPITALNAME").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strLastCheckNumber = objReader.GetAttribute("LASTCHECKNUMBER").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strSendThings = objReader.GetAttribute("SENDTHINGS").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strFromBody = objReader.GetAttribute("FROMBODY").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strSickenPeriod = objReader.GetAttribute("SICKENPERIOD").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strHistory = objReader.GetAttribute("HISTORY").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strClinicalInfo = objReader.GetAttribute("CLINICALINFO").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strOperationInfo = objReader.GetAttribute("OPERATIONINFO").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strCheckAim = objReader.GetAttribute("CHECKAIM").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strBiologyChemistry = objReader.GetAttribute("BIOLOGYCHEMISTRY").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strBlood = objReader.GetAttribute("BLOOD").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strXRay = objReader.GetAttribute("XRAY").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strBloodSerum = objReader.GetAttribute("BLOODSERUM").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strOther = objReader.GetAttribute("OTHER").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strClinicalDignose = objReader.GetAttribute("CLINICALDIGNOSE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strSendDate = objReader.GetAttribute("SENDDATE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strReceiveDate = objReader.GetAttribute("RECEIVEDATE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strColorAndSlice = objReader.GetAttribute("COLORANDSLICE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strEyeCheck = objReader.GetAttribute("EYECHECK").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strOrganiseBuryFull = objReader.GetAttribute("ORGANISEBURYFULL").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strOrganiseStay = objReader.GetAttribute("ORGANISESTAY").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strEyeSample = objReader.GetAttribute("EYESAMPLE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strPathologyDignose = objReader.GetAttribute("PATHOLOGYDIGNOSE").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strReportDate = objReader.GetAttribute("REPORTDATE").ToString().Replace('��', '\'');
                                    //���ǩ�� tfzhang 2005-7-8 17:25
                                    objPathologyOrgCheckOrder.m_strDoctorID = objReader.GetAttribute("DOCTORID").ToString().Replace('��', '\'');
                                    objPathologyOrgCheckOrder.m_strDoctorName = objReader.GetAttribute("DOCTORNAME").ToString().Replace('��', '\'');



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
            return objPathologyOrgCheckOrder;

        }

        /// <summary>
        /// ��ȡ�ӱ���Ϣ -- Operator
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <returns></returns>
        public clsPathologyOrgCheckOperatorID[] m_objGetOperatorIDArr(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            clsPathologyOrgCheckOperatorID[] objPathologyOrgCheckOperatorIDArr = null;

            //clsPathologyOrgCheckOrderServ m_objServ =
            //    (clsPathologyOrgCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPathologyOrgCheckOrderServ));

            try
            {
                int m_intReturnRows = 0;
                string m_strReceivedXML = "";

                long lngSucceed = (new weCare.Proxy.ProxyEmr03()).Service.GetPathologyOrgOperator(p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    objPathologyOrgCheckOperatorIDArr = new clsPathologyOrgCheckOperatorID[m_intReturnRows];

                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;

                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objPathologyOrgCheckOperatorIDArr[intIndex] = new clsPathologyOrgCheckOperatorID();

                                    objPathologyOrgCheckOperatorIDArr[intIndex].m_strOperatorID = objReader.GetAttribute("OPERATORID");
                                    objPathologyOrgCheckOperatorIDArr[intIndex].m_strOperatorFlag = objReader.GetAttribute("OPERATORFLAG");

                                    intIndex++;

                                }
                                break;
                        }//end switch
                    }//end while
                }//end if
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objPathologyOrgCheckOperatorIDArr;
        }
        #endregion

        #region ɾ��
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate,
            string p_strCreateDate)
        {
            //clsSPECTCheckOrderServ m_objServ =
            //    (clsSPECTCheckOrderServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsSPECTCheckOrderServ));

            long lngRes = 0;
            try
            {
                string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientID='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " CreateDate='" + p_strCreateDate + "'" + " />";
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeActive(strDeactiveXML, "PathologyOrgCheckOrder");
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
        #endregion

    }

    /// <summary>
    /// �������Ϣ
    /// </summary>
    public class clsPathologyOrgCheckOrderInfo
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";

        public string m_strCreateUserID = "";
        public string m_strIfConfirm = "";
        public string m_strConfirmReason = "";
        public string m_strConfirmReasonXML = "";
        public string m_strFirstPrintDate = "";
        public string m_strDeActivedDate = "";
        public string m_strDeActivedOperatorID = "";
        public string m_strStatus;

        public string m_strMedicalCheckNo = "";
        public string m_strHospitalName = "";
        public string m_strLastCheckNumber = "";
        public string m_strSendThings = "";
        public string m_strFromBody = "";
        public string m_strSickenPeriod = "";
        public string m_strHistory = "";
        public string m_strClinicalInfo = "";
        public string m_strOperationInfo = "";
        public string m_strCheckAim = "";
        public string m_strBiologyChemistry = "";
        public string m_strBlood = "";
        public string m_strXRay = "";
        public string m_strBloodSerum = "";
        public string m_strOther = "";
        public string m_strClinicalDignose = "";
        public string m_strSendDate = "";
        public string m_strReceiveDate = "";
        public string m_strColorAndSlice = "";
        public string m_strEyeCheck = "";
        public string m_strOrganiseBuryFull = "";
        public string m_strOrganiseStay = "";
        public string m_strEyeSample = "";
        public string m_strPathologyDignose = "";
        public string m_strReportDate = "";
        //���ǩ�� tfzhang 2005-7-8 17:25
        public string m_strDoctorID = "";    //ǩ��ID
        public string m_strDoctorName = "";  //ǩ������
    }

    /// <summary>
    /// �ӱ� -- OperatorID
    /// </summary>
    public class clsPathologyOrgCheckOperatorID
    {
        public string m_strInPatientID = "";
        public string m_strInPatientDate = "";
        public string m_strCreateDate = "";
        public string m_strModifyDate = "";

        public string m_strOperatorID = "";
        public string m_strOperatorFlag = "";
    }
}
