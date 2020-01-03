using System;
using weCare.Core.Entity;
using System.Text;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// �����¼��Ϣ��
	/// </summary>
	public class clsConsultationInfo	: clsDiseaseTrackInfo
	{		
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";

			clsConsultationRecordContent objContent=((clsConsultationRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmConsultation"))+"   "+"�����¼\n";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
			if(objPatient !=null && objPatient.m_ObjPeopleInfo !=null)
				strText +="����"+objPatient.m_ObjPeopleInfo.m_StrFirstName+"��"+objPatient.m_ObjPeopleInfo.m_StrSex+"��"+objPatient.m_ObjPeopleInfo.m_IntAge.ToString()+"�꣬";
			else strText +="����������Ϣ���꣬";					
						
			strText+=objContent.m_strApplyConsultationDeptName;

			StringBuilder m_sbdTemp1 = new StringBuilder();
			
			m_sbdTemp1.Length = 0;

			if(objContent.m_strRequestDoctorNameArr != null)
			{
				for(int i=0;i<objContent.m_strRequestDoctorNameArr.Length;i++)
				{
					m_sbdTemp1.Append(objContent.m_strRequestDoctorNameArr[i]+"��");//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�
				}
			}
			string strRequestDoctor =  m_sbdTemp1.ToString();

			StringBuilder m_sbdTemp2 = new StringBuilder();

			m_sbdTemp2.Length = 0;

			if(objContent.m_strConsultationDoctorNameArr != null)
			{
				for(int i=0;i<objContent.m_strConsultationDoctorNameArr.Length;i++)
				{
					m_sbdTemp2.Append(objContent.m_strConsultationDoctorNameArr[i]+"��");//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�
				}
			}
			string strConsultationDoctor =  m_sbdTemp2.ToString();
			
			string strConsultationTime;
			if (objContent.m_intConsultationTime==1)
				strConsultationTime="�뼴ʱ����";
			else if(objContent.m_intConsultationTime==2)
				strConsultationTime="����24Сʱ�ڻ���";
			else 
				strConsultationTime="һ�����";
			
			clsEmployee objEmployee = new clsEmployee(objContent.m_strMainDoctorID);

			strText +="\n������Ҫ����������Ŀ��:"+objContent.m_strCaseHistory;
			strText +="\n����Ŀǰ���:"+objContent.m_strConsultationOrder;
			strText +="\n��������ҽʦ:"+objEmployee.m_StrLastName+"סԺҽʦ��"+strRequestDoctor;
			strText +="\n��������ʱ��:"+strConsultationTime;
			strText +="\n���������:"+objContent.m_strConsultationIdea;
			strText +="\n��������ҽʦ: "+ strConsultationDoctor;
			
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

			clsConsultationRecordContent objContent=((clsConsultationRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmConsultation"))+"   "+"�����¼\n";
			clsPatient objPatient=new clsPatient(objContent.m_strInPatientID);
			if(objPatient !=null && objPatient.m_ObjPeopleInfo !=null)
				strText +="����"+objPatient.m_ObjPeopleInfo.m_StrFirstName+"��"+objPatient.m_ObjPeopleInfo.m_StrSex+"��"+objPatient.m_ObjPeopleInfo.m_IntAge.ToString()+"�꣬";
			else strText +="����������Ϣ���꣬";					
						
			strText+=objContent.m_strApplyConsultationDeptName;
		
			StringBuilder m_sbdTemp1 = new StringBuilder();
			
			m_sbdTemp1.Length = 0;

			if(objContent.m_strRequestDoctorNameArr != null)
			{
				for(int i=0;i<objContent.m_strRequestDoctorNameArr.Length;i++)
				{
					m_sbdTemp1.Append(objContent.m_strRequestDoctorNameArr[i]+"��");//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�
				}
			}
			string strRequestDoctor =  m_sbdTemp1.ToString();

			StringBuilder m_sbdTemp2 = new StringBuilder();

			m_sbdTemp2.Length = 0;

			if(objContent.m_strConsultationDoctorNameArr != null)
			{
				for(int i=0;i<objContent.m_strConsultationDoctorNameArr.Length;i++)
				{
					m_sbdTemp2.Append(objContent.m_strConsultationDoctorNameArr[i]+"��");//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�
				}
			}
			string strConsultationDoctor =  m_sbdTemp2.ToString();

			string strCreateUserName = m_strGetSignText();

			string strConsultationTime;
			if (objContent.m_intConsultationTime==1)
				strConsultationTime="�뼴ʱ����";
			else if(objContent.m_intConsultationTime==2)
				strConsultationTime="����24Сʱ�ڻ���";
			else 
				strConsultationTime="һ�����";

			clsEmployee objEmployee = new clsEmployee(objContent.m_strMainDoctorID);
			
			/*ctlRichTextBox.s_strMakeTextXml����Ӧ���ı����Xml��ʽ��
			 * ctlRichTextBox.s_strCombineXml�Ѹ��������������൱�ڼӺ�*/
            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������Ҫ����������Ŀ��:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����Ŀǰ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��������ҽʦ:" + objEmployee.m_StrLastName + "סԺҽʦ��" + strRequestDoctor, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��������ʱ��:" + strConsultationTime, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��������ҽʦ:" + strConsultationDoctor, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strCaseHistoryXml, strXML3, objContent.m_strConsultationOrderXml, strXML4, strXML5, strXML6, objContent.m_strConsultationIdeaXml, strXML7 });
			return strXML;
			

		}

		/// <summary>
		/// �����¼�����������ҽ��ǩ���Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetSignText()
		{
			if(m_objRecordContent==null)
				return "";		
			
						clsEmployee objEmployee=new clsEmployee(m_objRecordContent.m_strCreateUserID);
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
		
		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.Consultation;
		}

	}// END CLASS DEFINITION clsGeneralDiseaseInfo
}
