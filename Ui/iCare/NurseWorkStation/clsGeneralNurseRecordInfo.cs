using System;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;

namespace iCare
{
	/// <summary>
	/// һ�㻤���¼��Ϣ��
	/// </summary>
	public class clsGeneralNurseRecordInfo	: clsDiseaseTrackInfo
	{

//		private string m_strText,m_strXml,m_strSignText,m_strSignXml;
//		private bool m_blnInited = false;

		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			if(m_objRecordContent==null)
				return "";
			return ((clsGeneralNurseRecordContent)m_objRecordContent).m_strRecordContent;

//			if(!m_blnInited)
//			{
//				m_mthInit();
//
//				m_blnInited = true;
//			}
//
//			return m_strText;
		}

		/// <summary>
		/// �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";
			return ((clsGeneralNurseRecordContent)m_objRecordContent).m_strRecordContentXml;

//			if(!m_blnInited)
//			{
//				m_mthInit();
//
//				m_blnInited = true;
//			}
//			
//			return m_strXml;
		}

		/// <summary>
		/// �����¼����ǩ���Ļ�ȡ
		/// </summary>
		/// <returns></returns>
//		public override string m_strGetSignXml()
//		{
//			if(m_objRecordContent==null)
//				return "";
//
//			if(!m_blnInited)
//			{
//				m_mthInit();
//
//				m_blnInited = true;
//			}
//			
//			return m_strSignXml;
//		}		

		/// <summary>
		/// �����¼����ǩ���Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetSignText()
		{
			if(m_objRecordContent==null)
				return "";
		//
		//	if(!m_blnInited)
		//	{
		//		m_mthInit();
		//		m_blnInited = true;
		//	}
		//			
            return ((clsGeneralNurseRecordContent)m_objRecordContent).m_strSignName;

            //clsEmployee objEmployee=new clsEmployee(m_objRecordContent.m_strModifyUserID);
            //if(objEmployee !=null)
            //    return objEmployee.m_StrLastName;
            //else return "";
		}		

		private void m_mthInit()
		{
//			clsGeneralNurseRecordContent objGDRC = (clsGeneralNurseRecordContent)m_objRecordContent;
//			m_strText = objGDRC.m_strRecordContent;
//			m_strXml = objGDRC.m_strRecordContentXml;

//			m_sbdTemp.Length = 0;

//			if(objGDRC.m_strGeneralDiseaseDoctorNameArr != null)
//			{
//				for(int i=0;i<objGDRC.m_strGeneralDiseaseDoctorNameArr.Length;i++)
//				{
//					m_sbdTemp.Append(objGDRC.m_strGeneralDiseaseDoctorNameArr[i]+" ");
//				}
//			}
			
//			m_strSignText = m_sbdTemp.ToString();
//			m_strSignXml = "<root />";
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.GeneralNurseRecord;
		}

	}// END CLASS DEFINITION clsGeneralNurseRecordInfo
}
