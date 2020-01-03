using System;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;

namespace com.digitalwave.AdjustByDept
{
	/// <summary>
	/// ���Ǻ�Ƶ���
	/// </summary>
	public class clsEBHKAdjustMethod : infAdjustMethod
	{
		public clsEBHKAdjustMethod()
		{			
		}

		/// <summary>
		/// ����ʱ��ؼ��Ŀ��
		/// </summary>
		/// <param name="m_dtpDateTimePick"></param>
		public void m_mthAdjuestDateTimePick(ref ctlTimePicker p_dtpDateTimePick)
		{
			p_dtpDateTimePick.Width = 140;
		}

		/// <summary>
		/// ������ӡʱ���ʽ
		/// </summary>
		/// <param name="m_dtmDate"></param>
		public string m_strAdjustPrintDate(DateTime p_dtmDate)
		{
			return p_dtmDate.ToString("yyyy-MM-dd 00:00:00");
		}
	}
}
