using System;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// һ�㲡�̼�¼��Ϣ��
	/// </summary>
	public class clsGeneralDiseaseInfo	: clsDiseaseTrackInfo
	{
		private string m_strText,m_strXml,m_strSignText,m_strSignXml;
		private bool m_blnInited = false;
		
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";

			if(!m_blnInited)
			{
				m_mthInit();

				m_blnInited = true;
			}

			return m_strText;
		}

		/// <summary>
		/// �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";

			if(!m_blnInited)
			{
				m_mthInit();

				m_blnInited = true;
			}
			
			return m_strXml;
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

//			if(!m_blnInited)
//			{
//				m_mthInit();
//
//				m_blnInited = true;
//			}
//			
//			return m_strSignXml;
			return "<Root />";
		}		

		private void m_mthInit()
		{
			clsGeneralDiseaseRecordContent objGDRC = (clsGeneralDiseaseRecordContent)m_objRecordContent;
			string strHeader = objGDRC.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmGeneralDisease"))+"   "
				+objGDRC.m_strRecordTitle + "\n";
            string strHeaderXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strHeader, objGDRC.m_strModifyUserID, new clsEmployee(objGDRC.m_strModifyUserID).m_StrFirstName, System.Drawing.Color.White);
			m_strText =  strHeader + objGDRC.m_strRecordContent;
            m_strXml = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strHeaderXml, objGDRC.m_strRecordContentXml });
           
			m_sbdTemp.Length = 0;

			if(objGDRC.m_strGeneralDiseaseDoctorNameArr != null)
			{
				for(int i=0;i<objGDRC.m_strGeneralDiseaseDoctorNameArr.Length;i++)
				{
					if(i !=objGDRC.m_strGeneralDiseaseDoctorNameArr.Length-1)
						m_sbdTemp.Append(objGDRC.m_strGeneralDiseaseDoctorNameArr[i]+"��");//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�
					else
						m_sbdTemp.Append(objGDRC.m_strGeneralDiseaseDoctorNameArr[i]);

				}
			}
			
			m_strSignText = m_sbdTemp.ToString();
			m_strSignXml = "<root />";
		}
		
		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.GeneralDisease;
		}

	}// END CLASS DEFINITION clsGeneralDiseaseInfo
}
