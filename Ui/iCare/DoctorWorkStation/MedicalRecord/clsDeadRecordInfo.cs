using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
    /// <summary>
    /// ������¼��Ϣ��
    /// </summary>
    public class clsDeadRecordInfo : clsDiseaseTrackInfo
    {
        clsPatient m_objCurrentPatient = null;
        public clsDeadRecordInfo(clsPatient p_objPatient)
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

            clsDeadRecordContent objContent = ((clsDeadRecordContent)m_objRecordContent);
            string strText = m_strGetHeaderText();

            strText += "\n����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��");
            strText += "\n��Ժ���:" + objContent.m_strInHospitalCase;
            strText += "\n�������:" + objContent.m_strOriginalDiagnose;
            strText += "\n���ξ���:" + objContent.m_strDiagnoseBy;
            strText += "\n�����������:" + objContent.m_strDeadDiagnose;
            strText += "\n����ԭ��:" + objContent.m_strDeadReason;
            strText += "\n�����ѵ:" + objContent.m_strExperience;

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

            clsDeadRecordContent objContent = ((clsDeadRecordContent)m_objRecordContent);
            string strText = m_strGetHeaderText();

            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��Ժ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���ξ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�����������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ԭ��:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�����ѵ:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, strXML3, objContent.m_strInHospitalCaseXML, strXML4, objContent.m_strOriginalDiagnoseXML, strXML5, objContent.m_strDiagnoseByXML, strXML6, objContent.m_strDeadDiagnoseXML, strXML7, objContent.m_strDeadReasonXML, strXML8, objContent.m_strExperienceXML });
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

            clsDeadRecordContent objContent = ((clsDeadRecordContent)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmDeadRecord")) + "   " + "������¼\n";
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
            if (strInHospitalReason != "")
                strInHospitalReason = "��" + strInHospitalReason + "��";
            TimeSpan ts = objContent.m_dtmDeadDate - m_objCurrentPatient.m_DtmSelectedHISInDate;

            strText += strInHospitalReason + (m_objCurrentPatient == null ? "" : (m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd��") + "��Ժ")) + "," + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��") + "��������סԺ" + (ts.Days + 1).ToString() + "�졣";
            return strText;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.Dead;
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

    }// END CLASS DEFINITION clsDeadInfo	

    /// <summary>
    /// ������¼��Ϣ(�¼�)
    /// </summary>
    public class clsDeathRecordInfo : clsDiseaseTrackInfo
    {

        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsDeadRecord_VO objContent = ((clsDeadRecord_VO)m_objRecordContent);
            string strText = m_strGetHeaderText();

            strText += "\n    �ĵ�ͼ��:" + objContent.m_strCardiogramID;
            strText += "\n    X���:" + objContent.m_strXRayID;
            strText += "\n    ��������:" + objContent.m_strUltrasonicID;
            strText += "\n    MRI��:" + objContent.m_strMRIID;
            strText += "\n    �Ե粨��:" + objContent.m_strBrainWaveID;
            strText += "\n��������ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��");
            strText += "\n    ��������:" + objContent.m_strOperationName;
            strText += "\n    ��������:" + objContent.m_dtmOperationDate.ToString("yyyy��MM��dd��");
            strText += "\n������Ժ���:" + objContent.m_strInHospitalDiagnose;
            strText += "\n����סԺ����:" + objContent.m_strInHospitalProcess;
            strText += "\n������������:" + objContent.m_strDeadProcess;
            strText += "\n�����������:" + objContent.m_strDeadDiagnose;
            strText += "\n�����������۽���:" + objContent.m_strDeadVerdict;

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

            clsDeadRecord_VO objContent = ((clsDeadRecord_VO)m_objRecordContent);
            string strText = m_strGetHeaderText();

            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    �ĵ�ͼ��:" + objContent.m_strCardiogramID, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    X���:" + objContent.m_strXRayID, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ��������:" + objContent.m_strUltrasonicID, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    MRI��:" + objContent.m_strMRIID, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    �Ե粨��:" + objContent.m_strBrainWaveID, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��������ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML9 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ��������:" + objContent.m_dtmOperationDate, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML10 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������Ժ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML11 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����סԺ����:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML12 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML13 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�����������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML14 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�����������۽���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, strXML3, strXML4, strXML5, strXML6, strXML7, strXML8, objContent.m_strOperationNameXML, strXML9, strXML10, objContent.m_strInHospitalDiagnoseXML, strXML11, objContent.m_strInHospitalProcessXML, strXML12, objContent.m_strDeadProcessXML, strXML13, objContent.m_strDeadDiagnoseXML, strXML14, objContent.m_strDeadVerdictXML });
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

            clsDeadRecord_VO objContent = ((clsDeadRecord_VO)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmDeathRecord")) + "   " + "������¼\n";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);
            if (objPatient != null && objPatient.m_ObjPeopleInfo != null)
                strText += "����" + objPatient.m_ObjPeopleInfo.m_StrFirstName + "��" + objPatient.m_ObjPeopleInfo.m_StrSex + "��" + objPatient.m_ObjPeopleInfo.m_IntAge.ToString() + "�꣬";
            else strText += "����������Ϣ���꣬";

            TimeSpan ts = objContent.m_dtmDeadDate - objContent.m_dtmInPatientDate;

            strText +=/*strInHospitalReason + */objContent.m_dtmInPatientDate.ToString("yyyy��MM��dd��") + "��Ժ��" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��") + "��������סԺ" + (ts.Days + 1).ToString() + "�졣";
            return strText;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.Death;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignText()
        {
            if (m_objRecordContent == null)
                return "";
            clsDeadRecord_VO objContent = ((clsDeadRecord_VO)m_objRecordContent);
            return objContent.m_strDoctorName;


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

    }// END CLASS DEFINITION clsDeadInfo
}
