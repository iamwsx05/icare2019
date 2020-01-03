using System;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;

namespace com.digitalwave.AdjustByDept
{
	/// <summary>
	/// ����ʱ��ؼ��ӿ�
	/// </summary>
	public interface infAdjustMethod
	{
		/// <summary>
		/// ����ʱ��ؼ��Ŀ��
		/// </summary>
		/// <param name="m_dtpDateTimePick">ʱ��ؼ�</param>
		void m_mthAdjuestDateTimePick(ref ctlTimePicker p_dtpDateTimePick);

		/// <summary>
		/// ������ӡʱ���ʽ
		/// </summary>
		/// <param name="m_dtmDate">��ӡʱ��</param>
		string m_strAdjustPrintDate(DateTime p_dtmDate);
	}
}
