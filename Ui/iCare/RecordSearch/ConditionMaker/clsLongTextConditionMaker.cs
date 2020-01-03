using System;

namespace iCare.RecordSearch.ConditionMaker
{
	/// <summary>
	/// Summary description for clsLongTextConditionMaker.
	/// </summary>
	public class clsLongTextConditionMaker : clsConditionMakerBase
	{
		protected override void m_mthHandleRecordSearchFormSet()
		{
			string [] strTypeArr = new string[]{"���ݰ���","������","���ݿ�ͷ��","���ݽ�β��"};
			m_frmRecordSearch.m_cboLongTextConditionType.AddRangeItems(strTypeArr);	
			m_frmRecordSearch.m_cboLongTextConditionType.SelectedIndex = 0;
		}

		private string m_strGetConditionSQL()
		{
			string strText = null;
			switch(m_frmRecordSearch.m_cboLongTextConditionType.SelectedIndex)
			{
				case 0://"���ݰ���"
					strText = " ("+m_strConditionField+" like '%"+m_frmRecordSearch.m_txtLongTextContent.Text+"%') ";
					break;
				case 1://"������"
					strText = " ("+m_strConditionField+" = '"+m_frmRecordSearch.m_txtLongTextContent.Text+"') ";
					break;
				case 2://"���ݿ�ͷ��"
					strText = " ("+m_strConditionField+" like '"+m_frmRecordSearch.m_txtLongTextContent.Text+"%') ";
					break;
				case 3://"���ݽ�β��"
					strText = " ("+m_strConditionField+" like '%"+m_frmRecordSearch.m_txtLongTextContent.Text+"') ";
					break;				
			}
			return strText;
		}

		private string m_strGetConditionDesc()
		{
			return m_frmRecordSearch.m_cboLongTextConditionType.Text+m_frmRecordSearch.m_txtLongTextContent.Text;
		}

		public override void m_mthResetConditionInput()
		{
			m_frmRecordSearch.m_cboLongTextConditionType.SelectedIndex = 0;
			m_frmRecordSearch.m_txtLongTextContent.Text = "";
		}

		public override iCare.RecordSearch.ConditionMaker.clsConditionMakerBase.clsConditionStatus m_ObjStatus
		{
			get
			{
				clsLongTextConditionStatus objStatus = new clsLongTextConditionStatus();
				objStatus.m_intTypeIndex = m_frmRecordSearch.m_cboLongTextConditionType.SelectedIndex;
				objStatus.m_strContentValue = m_frmRecordSearch.m_txtLongTextContent.Text;
				objStatus.m_strConditionSQL = m_strGetConditionSQL();
				objStatus.m_strConditionDesc = m_strGetConditionDesc();
				
				return objStatus;
			}
			set
			{
				clsLongTextConditionStatus objStatus = value as clsLongTextConditionStatus;
                    
				if(objStatus != null)
				{
					m_frmRecordSearch.m_txtLongTextContent.Text = objStatus.m_strContentValue;
					m_frmRecordSearch.m_cboLongTextConditionType.SelectedIndex = objStatus.m_intTypeIndex;
				}	
			}
		}

		/// <summary>
		/// ��¼�ı�������ĵ�״̬
		/// </summary>
		public class clsLongTextConditionStatus : clsConditionMakerBase.clsConditionStatus
		{
			public int m_intTypeIndex;

			public string m_strContentValue;
		}
	}
}
