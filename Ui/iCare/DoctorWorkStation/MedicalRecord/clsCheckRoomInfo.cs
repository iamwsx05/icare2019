using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// �鷿��¼��Ϣ��
	/// </summary>
	public class clsCheckRoomInfo	: clsDiseaseTrackInfo
	{

		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";
			
			clsCheckRoomRecordContent objContent=((clsCheckRoomRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();

			strText +="\n���߲���:\n"+objContent.m_strPatientState;
			strText +="\n���:\n"+objContent.m_strDiagnose;
			strText +="\n�������:\n"+objContent.m_strDifferentiateDiagnose;


            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "450101001")
            {
                strText += "\n��ǰ����:\n" + objContent.m_strCurrentCure;
                strText += "\n��һ������:\n" + objContent.m_strNextCure;
            }
            else
            {
                strText += "\n����:\n" + objContent.m_strCurrentCure;
                if (!string.IsNullOrEmpty(objContent.m_strNextCure))
                {
                    strText += "\n" + objContent.m_strNextCure;
                }
            }
			
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

			clsCheckRoomRecordContent objContent=((clsCheckRoomRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();

			string strCreateUserName="";
			clsEmployee objEmployee=new clsEmployee(m_objRecordContent.m_strCreateUserID);
			if(objEmployee !=null)
				strCreateUserName= objEmployee.m_StrLastName;

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���߲���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML5;
            string strXML6 = string.Empty;
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")
            {
                strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
                if (!string.IsNullOrEmpty(objContent.m_strNextCure))
                {
                    strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
                }
            }
            else
            {
                strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ǰ����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
                strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��һ������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            }
            
            string strXML;
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")
            {
                if (!string.IsNullOrEmpty(objContent.m_strNextCure))
                {
                    strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strPatientStateXML, strXML3, objContent.m_strDiagnoseXML, strXML4, objContent.m_strDifferentiateDiagnoseXML, strXML5, objContent.m_strCurrentCureXML, strXML6, objContent.m_strNextCureXML });
                }
                else
                {
                    strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strPatientStateXML, strXML3, objContent.m_strDiagnoseXML, strXML4, objContent.m_strDifferentiateDiagnoseXML, strXML5, objContent.m_strCurrentCureXML });
                }                
            }
            else
            {
                strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strPatientStateXML, strXML3, objContent.m_strDiagnoseXML, strXML4, objContent.m_strDifferentiateDiagnoseXML, strXML5, objContent.m_strCurrentCureXML, strXML6, objContent.m_strNextCureXML });
            }
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

			clsCheckRoomRecordContent objContent=((clsCheckRoomRecordContent)m_objRecordContent);
			string strText;
			 
            //��ʾ�鷿ҽʦ
            string strSigns = "";
            bool blnFirst = false;
            if (m_objRecordContent.objSignerArr != null)
            {
                for (int i = 0; i < m_objRecordContent.objSignerArr.Length; i++)
                {
                    if (m_objRecordContent.objSignerArr[i].controlName == "lsvCheckRoomSign")
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
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmCheckRoom"))+"   "+strSigns+"�鷿��¼";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
//			if(objPatient !=null && objPatient.m_ObjPeopleInfo !=null)
//				strText +="����"+objPatient.m_ObjPeopleInfo.m_StrFirstName+"��"+objPatient.m_ObjPeopleInfo.m_StrSex+"��"+objPatient.m_ObjPeopleInfo.m_IntAge.ToString()+"�꣬";
//			else strText +="����������Ϣ���꣬";				
			
			return strText;
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.CheckRoom;
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

	}// END CLASS DEFINITION clsCheckRoomInfo

}
