using System;
using System.Diagnostics;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsHelpFile ��ժҪ˵����
	/// </summary>
	public class clsHelpFile
	{
		public clsHelpFile()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public void m_mthShowHelp()
		{
			try
			{
				Process.Start("Help/HelpFile.chm");
			}
			catch
			{
			System.Windows.Forms.MessageBox.Show("�Ҳ��������ļ�!","ICare");
			}
		}
	}
	/// <summary>
	/// ��ʾ������
	/// </summary>
	public class clsShowCalc
	{
		public clsShowCalc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public void m_mthShowCalc()
		{
			try
			{
				foreach(Process p in Process.GetProcesses())
				{
					if(p.ProcessName.Trim()=="calc")
					{
					return;
					}
				}
				System.Diagnostics.Process.Start("calc.exe");
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("������������!","iCareϵͳ��ܰ��ʾ:");
			}
		}
	}
}
