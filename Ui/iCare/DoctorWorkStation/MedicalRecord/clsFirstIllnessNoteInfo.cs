using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	#region �״β��̼�¼��Ϣ
	/// <summary>
	/// �״β��̼�¼��Ϣ��
	/// </summary>
	public class clsFirstIllnessNoteInfo  : clsDiseaseTrackInfo
	{
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";

			clsFirstIllnessNoteRecordContent objContent=((clsFirstIllnessNoteRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote"))+"   "+"�״β��̼�¼";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
			
			strText +="\n��Ҫ����:\n"+objContent.m_strMostlyContent;
			strText +="\n�������:\n"+objContent.m_strOriginalDiagnose;
			strText +="\n�������:\n"+objContent.m_strThereunderDiagnose;
			strText +="\n�������:\n"+objContent.m_strDiagnoseDiffe;
			strText +="\n���Ƽƻ�:\n"+objContent.m_strCurePlan;
			
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

			clsFirstIllnessNoteRecordContent objContent=((clsFirstIllnessNoteRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote"))+"   "+"�״β��̼�¼";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);

			string strCreateUserName=m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��Ҫ����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White, System.Drawing.FontStyle.Bold.ToString());
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strMostlyContentXML, strXML3, objContent.m_strOriginalDiagnoseXML, strXML4, objContent.m_strThereunderDiagnoseXML, strXML5, objContent.m_strDiagnoseDiffeXML, strXML6, objContent.m_strCurePlanXML });
			return strXML;			
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.FirstIllnessNote;
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
	}// END CLASS DEFINITION clsFirstIllnessNoteInfo
	#endregion

	#region �״β��̼�¼��Ϣ(��һ��ҽ��)
	/// <summary>
	/// �״β��̼�¼��Ϣ(��һ��ҽ��)��
	/// </summary>
	public class clsFirstIllnessNote_ZYInfo  : clsDiseaseTrackInfo
	{
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";

			clsFirstIllnessNote_ZYRecordContent objContent=((clsFirstIllnessNote_ZYRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote_ZY"))+"   "+"�״β��̼�¼";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
			
			strText +="\n��Ҫ����:\n"+objContent.m_strMostlyContent;
			strText +="\n�������:\n"+objContent.m_strOriginalDiagnose;
			strText +="\n��ҽ�没��֤����:\n"+objContent.m_strIdentifyReston;
			strText +="\n��ҽ�������:\n"+objContent.m_strThereunderDiagnose;
			strText +="\n��ҽ�������:\n"+objContent.m_strIdentifyDiagnos;
			strText +="\n��ҽ�������:\n"+objContent.m_strDiagnoseDiffe;
			strText +="\n���Ƽƻ�:\n"+objContent.m_strCurePlan;
			
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

			clsFirstIllnessNote_ZYRecordContent objContent=((clsFirstIllnessNote_ZYRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote_ZY"))+"   "+"�״β��̼�¼";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);

			string strCreateUserName=m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��Ҫ����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ҽ�没��֤����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
			
			string strXML=ctlRichTextBox.s_strCombineXml(new string[]{strXML1,strXML2,objContent.m_strMostlyContentXML,strXML3,objContent.m_strOriginalDiagnoseXML,strXML4,objContent.m_strIdentifyRestonXML, 
																		 strXML5,objContent.m_strThereunderDiagnoseXML,strXML6,objContent.m_strIdentifyDiagnoseXML, strXML7,objContent.m_strDiagnoseDiffeXML,strXML8,objContent.m_strCurePlanXML});
			return strXML;			
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.FirstIllnessNote_ZY;
		}

		/// <summary>
		/// �����¼����ǩ���Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetSignText()
		{
			if(m_objRecordContent==null)
				return "";
			clsEmployee objEmployee=new clsEmployee(m_objRecordContent.m_strModifyUserID);
			if(objEmployee !=null)
				return objEmployee.m_StrLastName;
			else return "";
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
	}
	#endregion

    #region �״β��̼�¼��Ϣ(���)
    /// <summary>
    /// �״β��̼�¼��Ϣ��
    /// </summary>
    public class clsFirstIllnessNoteInfo_F2 : clsDiseaseTrackInfo
    {
        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsFirstIllnessNoteRecordContent objContent = ((clsFirstIllnessNoteRecordContent)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote")) + "   " + "�״β��̼�¼";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);

            strText += "\n" + objContent.m_strMostlyContent;
            strText += "\n(һ)�����ص�:\n" + objContent.m_strOriginalDiagnose;
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "440605001")
                strText += "\n(��)���������������:\n" + objContent.m_strDiagnoseDiffe;
            else
                strText += "\n(��)�����������:\n" + objContent.m_strDiagnoseDiffe;
            strText += "\n(��)���Ƽƻ�:\n" + objContent.m_strCurePlan;

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

            clsFirstIllnessNoteRecordContent objContent = ((clsFirstIllnessNoteRecordContent)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote")) + "   " + "�״β��̼�¼";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);

            string strCreateUserName = m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(һ)�����ص�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)�����������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5="";
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "440605001")
                strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)���������������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            else strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)�����������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strMostlyContentXML, strXML3, objContent.m_strOriginalDiagnoseXML, strXML5, objContent.m_strDiagnoseDiffeXML, strXML6, objContent.m_strCurePlanXML });
            return strXML;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.FirstIllnessNote;
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
    }// END CLASS DEFINITION clsFirstIllnessNoteInfo
    #endregion

}
