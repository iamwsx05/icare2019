using System;
using iCareData;
using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// ������¼��Ϣ��
    /// </summary>
    public class clsDeathRecordInfo_XJ : clsDiseaseTrackInfo
    {

        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsDeathRecordContent_XJ objContent = ((clsDeathRecordContent_XJ)m_objRecordContent);
            string strText = m_strGetHeaderText();

            //strText += "\n�����ĵ�ͼ��:" + objContent.m_strHeartID;
            //strText += "\n����X���:" + objContent.m_strXRayID;
            strText += "\n������ҽ��Ժ���:" + objContent.m_strInHospitalDiagnose;
            strText += "\n������ҽ��Ժ���:" + objContent.m_strInHospitalDiagnoseXi;
            strText += "\n������ҽ�������:" + objContent.m_strOutHospitalDiagnose;
            strText += "\n������ҽ�������:" + objContent.m_strOutHospitalDiagnoseXi;
            strText += "\n��������ҽʦ:" + objContent.m_strMainDoctorName;
            strText += "\n������Ժ���:" + objContent.m_strInHospitalCase;
            //  strText += "\n�������ƾ���:" + objContent.m_strInHospitalBy;
            strText += "\n�������ƾ���:" + objContent.m_strOutHospitalCase;
            strText += "\n��������ԭ��:" + objContent.m_strOutHospitalAdvice;

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

            clsDeathRecordContent_XJ objContent = ((clsDeathRecordContent_XJ)m_objRecordContent);
            string strText = m_strGetHeaderText();

            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            string strXML1 = ctlRichTextBox.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML2 = ctlRichTextBox.s_strMakeTextXml("\n�����ĵ�ͼ��:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML3 = ctlRichTextBox.s_strMakeTextXml("\n����X���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.s_strMakeTextXml("\n������ҽ��Ժ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.s_strMakeTextXml("\n������ҽ��Ժ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.s_strMakeTextXml("\n������ҽ�������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.s_strMakeTextXml("\n������ҽ�������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.s_strMakeTextXml("\n��������ҽʦ:" + objContent.m_strMainDoctorName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.s_strMakeTextXml("\n������Ժ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML8 = ctlRichTextBox.s_strMakeTextXml("\n�������ƾ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.s_strMakeTextXml("\n�������ƾ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML9 = ctlRichTextBox.s_strMakeTextXml("\n��������ԭ��:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strInHospitalDiagnoseXML, strXML3, objContent.m_strInHospitalDiagnoseXiXML, strXML4, objContent.m_strOutHospitalDiagnoseXML, strXML5, objContent.m_strOutHospitalDiagnoseXiXML, strXML6, strXML7, objContent.m_strInHospitalCaseXML, strXML8, objContent.m_strOutHospitalCaseXML, strXML9, objContent.m_strOutHospitalAdviceXML });
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

            clsDeathRecordContent_XJ objContent = ((clsDeathRecordContent_XJ)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmDeathRecord_XJ")) + "   " + "��Ժ��¼\n";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);
            if (objPatient != null && objPatient.m_ObjPeopleInfo != null)
                strText += "����" + objPatient.m_ObjPeopleInfo.m_StrFirstName + "��" + objPatient.m_ObjPeopleInfo.m_StrSex + "��" + objPatient.m_ObjPeopleInfo.m_IntAge.ToString() + "�꣬";
            else strText += "����������Ϣ���꣬";

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
            TimeSpan ts = objContent.m_dtmOutHospitalDate - objContent.m_dtmInPatientDate;

            //MM��->01��,M��->1��			ֻ��Ӣ���ַ�
            strText += strInHospitalReason + objContent.m_dtmInPatientDate.ToString("yyyy��MM��dd��") + "��Ժ��" + objContent.m_dtmOutHospitalDate.ToString("yyyy��MM��dd��") + "��Ժ����סԺ" + (ts.Days + 1).ToString() + "�졣";
            return strText;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.OutHospital;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignText()
        {
            if (m_objRecordContent == null)
                return "";
            clsDeathRecordContent_XJ objContent = ((clsDeathRecordContent_XJ)m_objRecordContent);
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

    }// END CLASS DEFINITION clsOutHospitalInfo

}
