using System;
using iCare;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;

namespace com.digitalwave.AdjustByDept
{
	/// <summary>
	/// ���ݿ��ҽ��е���
	/// </summary>
	public class clsAdjustByDept
	{
//		private clsDepartment m_objDept;

		private infAdjustMethod m_objAdjustMethod;

		/// <summary>
		/// ����Ա�����ڲ��ţ����ɶ�Ӧ��infAdjustMethodʵ��
		/// </summary>
		/// <param name="p_objDept">Ա�����ڲ���</param>
		public clsAdjustByDept(clsDepartment p_objDept)
		{
			switch(p_objDept.m_StrDeptID)
			{
				case "1110000" :
					m_objAdjustMethod =  new clsEBHKAdjustMethod();
					break;
				case "1560000" :
					m_objAdjustMethod = new clsICUAdjustMethod();
					break;
			}
		}
		
		/// <summary>
		/// ����Ա�����ڿ��Һʹ���Ĵ��壬�޸Ĵ���������ʱ��ؼ��ĸ�ʽ
		/// </summary>
		/// <param name="p_objForm">����Ĵ���</param>
		public void m_mthAdjustDateTimePick(Form p_objForm)//form��object,Ҳ����ref type,�������ref
		{
			foreach(Control ctlSub in p_objForm.Controls)
			{
				m_mthAdustDateTimePick(ctlSub);			
			}
		}

		/// <summary>
		/// ����Ա�����ڿ��ң��ı���Ӧ��ʱ���ʽ
		/// </summary>
		/// <param name="p_dtmDate">ʱ��</param>
		/// <returns></returns>
		public string m_strAdjustPrintDate(DateTime p_dtmDate)
		{
			return m_objAdjustMethod.m_strAdjustPrintDate(p_dtmDate);	
		}

		/// <summary>
		/// ���õݹ���ã����ô�����ÿ��ʱ��ؼ��Ŀ��
		/// </summary>
		/// <param name="p_ctlControl"></param>
		public void m_mthAdustDateTimePick(Control p_ctlControl)
		{
			if(p_ctlControl.GetType().Name=="ctlTimePicker")
			{
				ctlTimePicker ctlTP = (ctlTimePicker)p_ctlControl;
				m_objAdjustMethod.m_mthAdjuestDateTimePick(ref ctlTP);
			}

			foreach(Control ctlSub in p_ctlControl.Controls)
			{
				m_mthAdustDateTimePick(ctlSub);
			}
		}
	}
}
