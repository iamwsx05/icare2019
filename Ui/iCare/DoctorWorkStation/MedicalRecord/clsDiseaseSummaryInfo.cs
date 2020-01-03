using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// �׶�С�ᡣ
	/// </summary>
	public class clsDiseaseSummaryInfo	: clsDiseaseTrackInfo
	{
		
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";

			clsDiseaseSummaryRecordContent objContent=((clsDiseaseSummaryRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmDiseaseSummary"))+"   "+"�׶�С��";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
			
			strText +="\n��Ժ���:\n"+objContent.m_strInHospitalCase;
			strText +="\n��Ժ���:\n"+objContent.m_strInHospitalDiagnose;
			strText +="\n���ƾ���:\n"+objContent.m_strDiagnoseBy;
			strText +="\nĿǰ���:\n"+objContent.m_strCurrentCase;
			strText +="\nĿǰ���:\n"+objContent.m_strCurrentDiagnose;
			strText +="\n���Ƽƻ�:\n"+objContent.m_strReferral;
			
			return strText;
		}

		/// <summary>
		/// �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";

			clsDiseaseSummaryRecordContent objContent=((clsDiseaseSummaryRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmDiseaseSummary"))+"   "+"�׶�С��";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);

			string strCreateUserName=m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��Ժ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��Ժ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���ƾ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nĿǰ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nĿǰ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strInHospitalCaseXML, strXML3, objContent.m_strInHospitalDiagnoseXML, strXML4, objContent.m_strDiagnoseByXML, strXML5, objContent.m_strCurrentCaseXML, strXML6, objContent.m_strCurrentDiagnoseXML, strXML7, objContent.m_strReferralXML });
			return strXML;			
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.DiseaseSummary;
		}

		/// <summary>
		/// �����¼����ǩ���Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetSignText()
		{
			if(m_objRecordContent==null)
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
		/// �����¼����ǩ��XML�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetSignXml()
		{
			if(m_objRecordContent==null)
				return "";
			
			return "<Root />";
		}	

	}

}
