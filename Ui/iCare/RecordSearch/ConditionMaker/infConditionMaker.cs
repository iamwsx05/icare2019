using System;

namespace iCare.RecordSearch.ConditionMaker
{	
	/// <summary>
	/// Ϊ���������������ṩĬ��ʵ��
	/// </summary>
	public class clsConditionMakerBase 
	{
		protected iCare.frmRecordSearch m_frmRecordSearch;
		protected string m_strConditionField;
	
		/// <summary>
		/// ���ü�¼��ѯ����
		/// </summary>
		/// <param name="p_frmRecordSearch"></param>
		public void m_mthSetRecordSearchForm(iCare.frmRecordSearch p_frmRecordSearch)
		{
			if(p_frmRecordSearch == null)
				return; 

			m_frmRecordSearch = p_frmRecordSearch;

			m_mthHandleRecordSearchFormSet();
		}

		/// <summary>
		/// ���ò�ѯ�������ֶ�
		/// </summary>
		/// <param name="p_strConditionField"></param>
		public void m_mthSetConditionField(string p_strConditionField)
		{
			if(p_strConditionField == null)
				return;

			m_strConditionField = p_strConditionField;
		}		

		/// <summary>
		/// ���ò�ѯ����������
		/// </summary>
		public virtual void m_mthResetConditionInput()
		{
			
		}	
		/// <summary>
		/// �ڼ�¼��ѯ�������ú���д���
		/// </summary>
		/// <param name="p_frmRecordSearch"></param>
		protected virtual void m_mthHandleRecordSearchFormSet()
		{
		}
		/// <summary>
		/// ���������������ʵ���жԴ�������������ѡ�
		/// </summary>
		/// <returns></returns>
		public virtual bool m_blnCheckCondition()
		{
			return true;
		}

		public virtual clsConditionStatus m_ObjStatus
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public class clsConditionStatus
		{
			/// <summary>
			/// ��ѯ����SQL
			/// </summary>
			public string m_strConditionSQL;

			/// <summary>
			/// ��ѯ��������
			/// </summary>
			public string m_strConditionDesc;

			public string m_strPreDesc = "";

			public int m_intFieldIndex;

			public override string ToString()
			{
				return m_strPreDesc+m_strConditionDesc;
			}
		}
	}
}
