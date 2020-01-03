using System;

namespace iCare
{
	/// <summary>
	/// ���򹤾�
	/// </summary>
	public class clsContinuousPrintTool
	{		
		private string m_strInPatientID,m_strInPatientDate;

		protected int m_intFromRecord = 0;
		/// <summary>
		/// ����һ����¼��ʼ��
		/// </summary>
		public int m_IntFromRecord
		{
			set 
			{
				m_intFromRecord = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public void m_mthSetContinuePrint(string p_strInPatientID,string p_strInPatientDate)
		{
			m_strInPatientID = p_strInPatientID;
			m_strInPatientDate = p_strInPatientDate;
			if(m_strGetRecordType() != "" && m_strGetRecordType() != null)
			{
				//��ȡ�ϴ�ӡ��������¼
				new clsPrintDateInfo().m_lngGetPrintFromRecord(p_strInPatientID,p_strInPatientDate,m_strGetRecordType(),out m_intFromRecord);
			}
		}

		/// <summary>
		/// ��ȡ��¼����
		/// </summary>
		protected virtual string m_strGetRecordType()
		{
			return "";
		}

		/// <summary>
		/// ��һ�δ�ӡ�Ƿ��Ѵ�ӡ��ȫ����¼
		/// </summary>
		/// <returns></returns>
		public virtual bool m_blnHavePrintAllRecords()
		{
			return false;
		}

		/// <summary>
		/// ���浱ǰ��ӡ��������¼
		/// </summary>
		protected void m_mthSavePrintToRecord(int p_intRecord)
		{
			if(m_strInPatientID != null && m_strGetRecordType() != "" && m_strGetRecordType() != null)
				new clsPrintDateInfo().m_lngSavePrintToRecord(m_strInPatientID,m_strInPatientDate,m_strGetRecordType(),p_intRecord);
		}

	}
}
