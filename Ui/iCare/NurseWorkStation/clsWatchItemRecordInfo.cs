using System;
using weCare.Core.Entity;
namespace iCare
{
	/// <summary>
	///  �۲���Ŀ��¼����Ϣ��
	/// </summary>
	public class clsWatchItemRecordInfo	: clsDiseaseTrackInfo
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
			return enmDiseaseTrackType.WatchItem;
		}

	}// END CLASS DEFINITION clsWatchItemRecordInfo

}
