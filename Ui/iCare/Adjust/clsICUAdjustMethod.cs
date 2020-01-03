using System;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;

namespace com.digitalwave.AdjustByDept
{
	/// <summary>
	/// ICU����
	/// </summary>
	public class clsICUAdjustMethod : infAdjustMethod
	{
		public clsICUAdjustMethod()
		{			
		}

		/// <summary>
		/// ����ʱ��ؼ��Ŀ��
		/// </summary>
		/// <param name="m_dtpDateTimePick">ʱ��ؼ�</param>
		public void m_mthAdjuestDateTimePick(ref ctlTimePicker p_dtpDateTimePick)
		{
			p_dtpDateTimePick.Width = 188;
		}

		/// <summary>
		/// ������ӡʱ���ʽ
		/// </summary>
		/// <param name="m_dtmDate">��ӡʱ��</param>
		public string m_strAdjustPrintDate(DateTime p_dtmDate)
		{
			return p_dtmDate.ToString("yyyy-MM-dd HH:mm");
		}
	}
}
