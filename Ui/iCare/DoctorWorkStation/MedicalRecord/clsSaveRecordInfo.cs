using System;
using weCare.Core.Entity;
using com.digitalwave.controls;
namespace iCare
{
	/// <summary>
	/// �����������ۼ�¼��Ϣ��
	/// </summary>
	public class clsSaveRecordInfo	: clsDiseaseTrackInfo
	{

		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";
			
			clsSaveRecordContent objContent=((clsSaveRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();

			strText +="\n    ����ʱ��:"+objContent.m_dtmSaveTime.ToString("yyyy��MM��dd��HHʱmm��");				
			strText +="\nΣ�ز�������:\n"+objContent.m_strDiseaseName;	
			strText +="\n����仯��������ȴ�ʩ:\n"+objContent.m_strDiseaseChangeCase;	
			strText +="\n���Ƚ��:\n"+objContent.m_strSaveResult;
			strText +="\n�ڳ�����:\n"+objContent.m_strSaveDeal;	
			strText +="\n�μ���Ա:\n"+m_strGetName(m_objRecordContent.objSignerArr,"lsvAttendPeople");

			return strText;
		}

		private string m_strGetName(clsEmrSigns_VO[] p_strNameArr,string strLsv)
		{
			string strSigns="";
			bool blnFirst=false;
			//��ʾǩ����
			if (m_objRecordContent.objSignerArr!=null)
			{
				for (int i = 0; i < m_objRecordContent.objSignerArr.Length; i++)
				{
					if (m_objRecordContent.objSignerArr[i].controlName==strLsv)
					{
						//����
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
		/// �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";

			clsSaveRecordContent objContent=((clsSaveRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();

			string strCreateUserName="";
			clsEmployee objEmployee=new clsEmployee(m_objRecordContent.m_strCreateUserID);
			if(objEmployee !=null)
				strCreateUserName= objEmployee.m_StrLastName;
            //����m_strGetTrackText()��Ŀ����һ�£�����ᵼ���޸ĺۼ���λ
			string strXML1=ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText,objContent.m_strCreateUserID,strCreateUserName,System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ����ʱ��:" + objContent.m_dtmSaveTime.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nΣ�ز�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����仯��������ȴ�ʩ:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n���Ƚ��:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�ڳ�����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�μ���Ա:\n" + m_strGetName(m_objRecordContent.objSignerArr, "lsvAttendPeople"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[]{strXML1,strXML2,strXML3,objContent.m_strDiseaseNameXML,strXML4,objContent.m_strDiseaseChangeCaseXML,strXML5,objContent.m_strSaveResultXML,
																		 strXML6,objContent.m_strSaveDealXML/*});*/,strXML7/*,objContent.m_strAttendPeopleXML*/ });
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

			clsSaveRecordContent objContent=((clsSaveRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmSaveRecord"))+"   "+"���ȼ�¼";
			return strText;
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.Save;
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

	}// END CLASS DEFINITION clsSaveInfo

}
