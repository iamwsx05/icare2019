using System;
using weCare.Core.Entity;

namespace iCare
{
	/// <summary>
	///  ICU��������Ϣ��
	/// </summary>
	public class clsICUBreathInfo	: clsDiseaseTrackInfo
	{

		/// <summary>
		///  �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackText()
		{
			return "";
		}

		/// <summary>
		///  �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override string m_strGetTrackXml()
		{
			return "";
		}

		/// <summary>
		///  �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public override enmDiseaseTrackType m_enmGetTrackType()
		{
			return enmDiseaseTrackType.ICUBreath;
		}

	}// END CLASS DEFINITION clsWatchItemRecordInfo
}
