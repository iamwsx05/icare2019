using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	// ת���¼��Ϣ��
	public class clsTurnInInfo	: clsDiseaseTrackInfo
	{
        clsPatient m_objCurrentPatient = null;
        public clsTurnInInfo(clsPatient p_objPatient)
        {
            m_objCurrentPatient = p_objPatient;
        }
		// �����¼�����ı��Ļ�ȡ��
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";
			
			clsTurnInRecordContent objContent=((clsTurnInRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();			

			strText +="\nת��ǰ����:\n"+objContent.m_strCaseBeforeTurnIn;
			strText +="\nת��ԭ��:\n"+objContent.m_strTurnInReason;
			strText +="\nת������:\n"+objContent.m_strCaseAfterTurnIn;
			strText +="\nת������:\n"+objContent.m_strTurnInDiagnose;
			strText +="\n���Ƽƻ�:\n"+objContent.m_strReferral;
			
			return strText;
		}

		// �����¼���ݸ�ʽXml�Ļ�ȡ
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";
			
			clsTurnInRecordContent objContent=((clsTurnInRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();
			string strCreateUserName=m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nת��ǰ����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nת��ԭ��:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nת������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nת������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[]{strXML1,strXML2,objContent.m_strCaseBeforeTurnInXML,strXML3,objContent.m_strTurnInReasonXML,
					strXML4,objContent.m_strCaseAfterTurnInXML,strXML5,objContent.m_strTurnInDiagnoseXML,strXML6,objContent.m_strReferralXML});
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

			clsTurnInRecordContent objContent=((clsTurnInRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmTurnIn"))+"   "+"ת���¼\n";
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
			TimeSpan ts=objContent.m_dtmCreateDate-objContent.m_dtmInPatientDate;

            strText += strInHospitalReason + m_objCurrentPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd��") + "��Ժ.��סԺ��" + (ts.Days + 1).ToString() + "�졣";
			return strText;
		}

		// �����¼���͵Ļ�ȡ
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.TurnIn;
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

	}// END CLASS DEFINITION clsTurnInInfo

}
