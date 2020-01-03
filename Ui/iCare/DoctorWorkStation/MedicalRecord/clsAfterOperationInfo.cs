using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// �����󲡳̼�¼��Ϣ��
	/// </summary>
	public class clsAfterOperationInfo	: clsDiseaseTrackInfo
	{

		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";
			
			clsAfterOperationRecordContent objContent=((clsAfterOperationRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();			

			strText +="\n����"+objContent.m_dtmTakeOutStitchesDate.ToString("yyyy��M��d��Hʱm��")+"��"+objContent.m_strAnaesthesiaMode ;
			strText += "�����£�����" + objContent.m_strOperationName ;
            strText += "��" + "\n�������:\n" + objContent.m_strOperationDiagnose;
			strText +="\n����������(������Ҫ����,������,�����걾���䴦��):\n"+objContent.m_strInOperationSeeing;
			strText +="\n������:\n"+objContent.m_strAfterOperationDeal;
			strText +="\n����ע��:\n"+objContent.m_strAfterOperationNotice;
			strText +="\n�˿��������:\n"+objContent.m_strCutHealUpStatus;
			
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
			
			clsAfterOperationRecordContent objContent=((clsAfterOperationRecordContent)m_objRecordContent);
			string strText=m_strGetHeaderText();
			string strCreateUserName=m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����" + objContent.m_dtmTakeOutStitchesDate.ToString("yyyy��M��d��Hʱm��") + "��", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("�����£�����", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��" + "\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����������(������Ҫ����,������,�����걾���䴦��):\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ע��:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�˿��������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[]{strXML1,strXML2,objContent.m_strAnaesthesiaModeXML,strXML3,objContent.m_strOperationNameXML,
																		 strXML4,objContent.m_strOperationDiagnoseXML,strXML5,objContent.m_strInOperationSeeingXML,strXML6,objContent.m_strAfterOperationDealXML,
																		 strXML7,objContent.m_strAfterOperationNoticeXML,strXML8,objContent.m_strCutHealUpStatusXML	});
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

			clsAfterOperationRecordContent objContent=((clsAfterOperationRecordContent)m_objRecordContent);
			string strText;
			strText=objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmAfterOperation"))+"   "+"�����󲡳̼�¼";
						
			return strText;
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.AfterOperation;
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

	}// END CLASS DEFINITION clsAfterOperationInfo

}
