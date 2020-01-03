using System;
using weCare.Core.Entity;
using System.Text;

namespace iCare
{
	/// <summary>
	/// �����̼�¼����Ϣ�����ڲ��̼�¼������������¼��������ϵ��
	/// ���ڸ���¼�����и��Ե���Ϣ��ʹ�ó���������Ϣ���������塣
	/// </summary>
	public abstract class clsDiseaseTrackInfo	: IComparable
	{

		/// <summary>
		/// �����¼�ı���
		/// </summary>
		protected string m_strTitle;

		/// <summary>
		/// �����¼������
		/// </summary>
		protected DateTime m_dtmRecordTime;

		/// <summary>
		/// �����¼������
		/// </summary>
		protected clsTrackRecordContent m_objRecordContent;

		protected StringBuilder m_sbdTemp = new StringBuilder();

		/// <summary>
		/// �Ƚϲ���ֵ�뵱ǰʵ���Ĵ�С
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		public int CompareTo(object p_objValue)
		{
			clsDiseaseTrackInfo objDiff = (clsDiseaseTrackInfo)p_objValue;

			if(this.m_objRecordContent.m_dtmCreateDate == objDiff.m_objRecordContent.m_dtmCreateDate)
				return 0;
			else if(this.m_objRecordContent.m_dtmCreateDate > objDiff.m_objRecordContent.m_dtmCreateDate)
				return 1;
			else 
				return -1;
		}

		/// <summary>
		/// �����¼����Ļ�ȡ������
		/// </summary>
		public string m_StrTitle
		{
			get
			{
				return m_strTitle;
			}
			set
			{
				m_strTitle=value;
			}
		}
		/// <summary>
		/// �����¼���ڵĻ�ȡ������
		/// </summary>
		public DateTime m_DtmRecordTime
		{
			get
			{
				return m_dtmRecordTime;
			}
			set
			{
				m_dtmRecordTime=value;
			}
		}
		/// <summary>
		/// �����¼���ݵĻ�ȡ������
		/// </summary>
		public clsTrackRecordContent m_ObjRecordContent
		{
			get
			{
				return m_objRecordContent;
			}
			set
			{
				m_objRecordContent=value;
			}
		}
		/// <summary>
		/// �����¼�����ı��Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public abstract string m_strGetTrackText();

		/// <summary>
		/// �����¼���ݸ�ʽXml�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public abstract string m_strGetTrackXml();

		/// <summary>
		/// �����¼����ǩ���Ļ�ȡ��
		/// </summary>
		/// <returns></returns>
		public virtual string m_strGetSignText()
		{
			return "";
		}

		/// <summary>
		/// �����¼����ǩ��Xml�Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public virtual string m_strGetSignXml()
		{
			return "";
		}

		/// <summary>
		/// �����¼���͵Ļ�ȡ
		/// </summary>
		/// <returns></returns>
		public abstract enmDiseaseTrackType m_enmGetTrackType();
		/// <summary>
		///  ���Ʒ�ҳ��0������ҳ��1���Ӵ˼�¼���ҳ��
		/// </summary>
		private string m_strPagination = "";
		/// <summary>
		/// ���Ʒ�ҳ��0������ҳ��1���Ӵ˼�¼���ҳ��
		/// </summary>
		public string m_StrPagination
		{
			get{return m_strPagination;}
			set{m_strPagination = value;}
		}

	}

}
