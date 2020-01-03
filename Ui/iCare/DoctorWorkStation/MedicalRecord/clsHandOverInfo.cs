using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// �����¼��Ϣ��
	/// </summary>
	public class clsHandOverInfo	: clsDiseaseTrackInfo
	{
        clsPatient m_objCurrentPatient = null;
        public clsHandOverInfo(clsPatient p_objPatient)
        {
            m_objCurrentPatient = p_objPatient;
        }
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";

			clsHandOverRecordContent objContent=((clsHandOverRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();
			
			strText +="\n�������:\n"+objContent.m_strOriginalDiagnose;
			strText +="\nĿǰ���:\n"+objContent.m_strCurrentDiagnose;
			strText +="\n������:\n"+objContent.m_strCaseHistory;
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

			clsHandOverRecordContent objContent=((clsHandOverRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();				

			string strCreateUserName=m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nĿǰ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strOriginalDiagnoseXML, strXML3, objContent.m_strCurrentDiagnoseXML, strXML4, objContent.m_strCaseHistoryXML, strXML5, objContent.m_strReferralXML });
			return strXML;			
		}

		/// <summary>
		/// ��ȡ��ͷ������Ϣ
		/// </summary>
		/// <returns></returns>
		private string m_strGetHeaderText()
		{
			if(m_objRecordContent==null)
				return "";

			clsHandOverRecordContent objContent=((clsHandOverRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmHandOver"))+"   "+"�����¼\n";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
			
			string strInHospitalReason="";
			#region ��Ժԭ��(����)
			clsInPatientCaseHisoryDefaultValue[] objInPatientCaseHisoryDefaultValueArr= new clsInPatientCaseHisoryDefaultDomain().lngGetAllInPatientCaseHisoryDefault(objContent.m_strInPatientID,objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
			if(objInPatientCaseHisoryDefaultValueArr !=null && objInPatientCaseHisoryDefaultValueArr.Length>0)
			{
				strInHospitalReason=objInPatientCaseHisoryDefaultValueArr[0].m_strMainDescription;
			}
			#endregion ��Ժԭ��(����)

			string strLastChar="";
			if(strInHospitalReason !=null && strInHospitalReason.Length>0)
				strLastChar=strInHospitalReason.Substring(strInHospitalReason.Length-1,1);
			if(strLastChar =="." ||strLastChar =="," || strLastChar=="��" ||strLastChar=="��")//ȥ�����ı��
				strInHospitalReason=strInHospitalReason.Substring(0,strInHospitalReason.Length-1);
			if(strInHospitalReason !=null && strInHospitalReason.Trim() !="")
				strInHospitalReason="��"+strInHospitalReason+"��";
            strText += strInHospitalReason + (m_objCurrentPatient == null ? "" : (m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd��") + "��Ժ��"));
			return strText;
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.HandOver;
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
		/// �����¼����ǩ���Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetSignXml()
		{
			if(m_objRecordContent==null)
				return "";
			
			return "<Root />";
		}	

	}// END CLASS DEFINITION clsHandOverInfo

}
