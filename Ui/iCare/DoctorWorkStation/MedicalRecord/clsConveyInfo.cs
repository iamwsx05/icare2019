using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
    /// <summary>
    /// ת����¼��Ϣ��
    /// </summary>
    public class clsConveyInfo : clsDiseaseTrackInfo
    {
        clsPatient m_objCurrentPatient = null;
        public clsConveyInfo(clsPatient p_objPatient)
        {
            m_objCurrentPatient = p_objPatient;
        }
        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsConveyRecordContent objContent = ((clsConveyRecordContent)m_objRecordContent);
            string strText = m_strGetHeaderText();

            strText += "\n�������:\n" + objContent.m_strOriginalDiagnose;
            strText += "\nת�����:\n" + objContent.m_strConveyDiagnose;
            strText += "\n��ʷ���:\n" + objContent.m_strCaseHistory;
            strText += "\n�������:\n" + objContent.m_strConsultation;
            strText += "\nת��ԭ��:\n" + objContent.m_strConveyReason;
            strText += "\n������տ���ע������:\n" + objContent.m_strNotice;

            return strText;
        }

        /// <summary>
        /// �����¼���ݸ�ʽXml�Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackXml()
        {
            if (m_objRecordContent == null)
                return "";

            clsConveyRecordContent objContent = ((clsConveyRecordContent)m_objRecordContent);
            string strText = m_strGetHeaderText();

            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nת�����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ʷ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nת��ԭ��:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������տ���ע������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strOriginalDiagnoseXML, strXML3, objContent.m_strConveyDiagnoseXML, strXML4, objContent.m_strCaseHistoryXML, strXML5, objContent.m_strConsultationXML, strXML6, objContent.m_strConveyReasonXML, strXML7, objContent.m_strNoticeXML });
            return strXML;
        }

        /// <summary>
        /// ��ȡ��ͷ������Ϣ
        /// </summary>
        /// <returns></returns>
        private string m_strGetHeaderText()
        {
            if (m_objRecordContent == null)
                return "";

            clsConveyRecordContent objContent = ((clsConveyRecordContent)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmConvey")) + "   " + "ת����¼\n";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);


            string strInHospitalReason = "";
            #region ��Ժԭ��(����)
            clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr = new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(objContent.m_strInPatientID, objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (objInPatientCaseHisoryDefaultValueArr != null && objInPatientCaseHisoryDefaultValueArr.Length > 0)
            {
                strInHospitalReason = objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
            }
            #endregion ��Ժԭ��(����)

            string strLastChar = "";
            if (strInHospitalReason != null && strInHospitalReason.Length > 0)
                strLastChar = strInHospitalReason.Substring(strInHospitalReason.Length - 1, 1);
            if (strLastChar == "." || strLastChar == "," || strLastChar == "��" || strLastChar == "��")//ȥ�����ı��
                strInHospitalReason = strInHospitalReason.Substring(0, strInHospitalReason.Length - 1);
            if (strInHospitalReason != null && strInHospitalReason.Trim() != "")
                strInHospitalReason = "��" + strInHospitalReason + "��";
            TimeSpan ts = objContent.m_dtmCreateDate - m_objCurrentPatient.m_DtmSelectedHISInDate;

            strText += strInHospitalReason + m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd��") + "��Ժ.��סԺ��" + (ts.Days + 1).ToString() + "�졣";
            return strText;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.Convey;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignText()
        {
            if (m_objRecordContent == null)
                return "";
            //��ȡǩ��
            string strSigns = "";
            bool blnFirst = false;
            if (m_objRecordContent.objSignerArr != null)
            {
                for (int i = 0; i < m_objRecordContent.objSignerArr.Length; i++)
                {
                    if (m_objRecordContent.objSignerArr[i].controlName == "lsvSign")
                    {
                        //����
                        if (!blnFirst)
                        {
                            strSigns = m_objRecordContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName;
                            blnFirst = true;
                        }
                        else
                            strSigns = strSigns + "��" + m_objRecordContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName;
                    }
                }
            }

            return strSigns;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignXml()
        {
            if (m_objRecordContent == null)
                return "";

            return "<Root />";
        }

    }// END CLASS DEFINITION clsConveyInfo

}
