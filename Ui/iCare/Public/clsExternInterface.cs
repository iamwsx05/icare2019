using System;
using System.Reflection;

namespace iCare
{
	/// <summary>
	/// ���ⲿ�ӿڵĽ���
	/// </summary>
	public class clsExternInterface
	{
		public clsExternInterface()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region PACS
		/// <summary>
		/// ����PACS�Ǽǵ���δ���
		/// </summary>
		/// <returns></returns>
		public long m_lngGeneratePACSBookIn()
		{
			return 1;
		}
		#endregion

		#region LIS
		/// <summary>
		/// ���ü������뵥
		/// </summary>
		public void m_mthInvokeLISApplyRecord(string p_strInPatientID)
		{
			
		}

		/// <summary>
		/// ���ü��鱨�浥
		/// </summary>
		public void m_mthInvokeLISReportRecord(string p_strInPatientID)
		{
			Assembly asm = Assembly.LoadFrom("LIS_GUI.dll");
			object obj = asm.CreateInstance("com.digitalwave.iCare.gui.LIS.frmReportQuery");
			Type type = obj.GetType();
			MethodInfo mi = type.GetMethod("m_mthQueryReports");
			mi.Invoke(obj,new string[]{"3",null,p_strInPatientID,null,null,null,null,null});
		}
		#endregion

		#region ҽ��
		#endregion

		#region ����
		#endregion

	}
}
