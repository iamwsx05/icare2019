using System;
using weCare.Core.Entity;

namespace iCare
{
	/// <summary>
	/// Summary description for clsIntensiveRecordInfo.
	/// </summary>
	public class clsIntensiveRecordInfo : clsDiseaseTrackInfo
	{
		#region  �����¼�����ı��Ļ�ȡ��,��ӱԴ,2003-5-13 14:21:05
		public override string m_strGetTrackText()
		{
            if (m_objRecordContent == null)
                return "";
            return ((clsIntensiveTendRecordContent1)m_objRecordContent).m_strRecordContent;
		}
		#endregion

		#region �����¼���ݸ�ʽXml�Ļ�ȡ,��ӱԴ,2003-5-13 14:21:05
		public override string m_strGetTrackXml()
		{
			if(m_objRecordContent==null)
				return "";
			return ((clsIntensiveTendRecordContent1 )m_objRecordContent).m_strRecordContentXml;
		}
		#endregion

		#region �����¼���͵Ļ�ȡ,��ӱԴ,2003-5-13 14:21:05
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.IntensiveTend ;
		}
		#endregion
		
	}
}
