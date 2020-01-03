using System;
using weCare.Core.Entity;
using com.digitalwave.controls;
namespace iCare
{
	/// <summary>
	/// ��ǰ���ۼ�¼��Ϣ��
	/// </summary>
	public class clsBeforeOperationDiscussInfo	: clsDiseaseTrackInfo
	{

		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";
			
			clsBeforeOperationDiscussRecordContent objContent=((clsBeforeOperationDiscussRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();

			strText +="\n�ص�:"+objContent.m_strAddress;
			strText +="\n�μ���Ա:"+m_strGetName(m_objRecordContent.objSignerArr,"lsvAttend");
			strText +="\n������:"+m_strGetName(m_objRecordContent.objSignerArr,"lsvCompere");
			strText +="\n��������:\n"+objContent.m_strDiscussContent;			
			return strText;
		}

		private string m_strGetName(clsEmrSigns_VO[] p_strNameArr,string strLsv)
		{

            //��ȡǩ��
            string strSigns = "";
            bool blnFirst = false;
            if (m_objRecordContent.objSignerArr != null)
            {
                for (int i = 0; i < m_objRecordContent.objSignerArr.Length; i++)
                {
                    if (m_objRecordContent.objSignerArr[i].controlName == strLsv)
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
		/// �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";

			clsBeforeOperationDiscussRecordContent objContent=((clsBeforeOperationDiscussRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();

			string strCreateUserName="";
			clsEmployee objEmployee=new clsEmployee(m_objRecordContent.m_strCreateUserID);
			if(objEmployee !=null)
				strCreateUserName= objEmployee.m_StrLastName;
            //����m_strGetTrackText()��Ŀ����һ�£�����ᵼ���޸ĺۼ���λ
            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�ص�:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�μ���Ա:" + m_strGetName(m_objRecordContent.objSignerArr, "lsvAttend"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������:" + m_strGetName(m_objRecordContent.objSignerArr, "lsvCompere"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strAddressXML, strXML3, strXML4, strXML5, objContent.m_strDiscussContentXML });
            //string strXML=ctlRichTextBox.s_strCombineXml(new string[]{strXML1,strXML2,objContent.m_strAddressXML,strXML5,objContent.m_strDiscussContentXML});
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

			clsBeforeOperationDiscussRecordContent objContent=((clsBeforeOperationDiscussRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmBeforeOperationDiscuss"))+"   "+"��ǰ���ۼ�¼";
			return strText;
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.BeforeOperationDiscuss;
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

	}// END CLASS DEFINITION clsBeforeOperationDiscussInfo

}
