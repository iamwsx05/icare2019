using System;
using weCare.Core.Entity;

namespace iCare.RecordSearch.PatientDetailMaker
{
    /// <summary>
    /// Summary description for clsInHospitalHistoryRecordMaker.
    /// </summary>
    public class clsInHospitalHistoryRecordMaker : clsPatientDetailMakerBase
    {
        public override string m_strMakerPatientDetailDesc(clsPatient p_objPatient, iCare.RecordSearch.clsRecordSearchDomain.clsPatientList p_objPatientList)
        {
            clsInPatientCaseHistoryPrintTool objPrintRec = new clsInPatientCaseHistoryPrintTool();
            objPrintRec.m_mthSetPrintInfo(p_objPatient, DateTime.Parse(p_objPatientList.m_strInPatientDate), DateTime.Parse(p_objPatientList.m_strOpenDate));
            objPrintRec.m_mthInitPrintContent();
            clsPrintInfo_InPatientCaseHistory objRecordInfo = (clsPrintInfo_InPatientCaseHistory)objPrintRec.m_objGetPrintInfo();
            clsInPatientCaseHistoryContent objContent = (clsInPatientCaseHistoryContent)objRecordInfo.m_objContent;

            System.Text.StringBuilder sbdTemp = new System.Text.StringBuilder(1000);
            sbdTemp.Append(p_objPatient.m_StrName + "  " + p_objPatient.m_StrSex + "  " + p_objPatient.m_ObjPeopleInfo.m_StrAge + "  " + p_objPatient.m_ObjPeopleInfo.m_StrMarried + "\r\n");
            sbdTemp.Append(p_objPatient.m_ObjPeopleInfo.m_StrNationality + "  " + p_objPatient.m_ObjPeopleInfo.m_StrNativePlace + "  " + p_objPatient.m_ObjPeopleInfo.m_StrOccupation + "\r\n");
            sbdTemp.Append("��Ժ���ڣ�" + objRecordInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd��") + "\r\n\r\n");
            sbdTemp.Append("���ߣ�" + objContent.m_strMainDescription + "\r\n\r\n");
            if (objContent.m_strPrimaryDiagnoseAll != null && objContent.m_strPrimaryDiagnoseAll.Trim() != "")
            {
                sbdTemp.Append("��Ժ��ϣ�\r\n");
                //				for(int i=0;i<objContent.m_strPrimaryDiagnoseAll.Length;i++)
                //				{
                sbdTemp.Append(com.digitalwave.Utility.Controls.ctlRichTextBox.s_strGetRightText(objContent.m_strPrimaryDiagnoseAll, objContent.m_strPrimaryDiagnoseXML) + "\r\n");
                //				}
                sbdTemp.Append("\r\n");
            }

            sbdTemp.Append("��ʷ��¼�ߣ�" + objContent.m_strCreateName + "\r\n��¼���ڣ�" + objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory")));
            return sbdTemp.ToString();
        }

        public override string m_StrFormName
        {
            get
            {
                return "סԺ����";
            }
        }
    }
}
