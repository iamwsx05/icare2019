using System;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// Summary description for infPrintRecord.
	/// </summary>
	/// <summary>
	/// ͳһ�Ĵ�ӡ�ӿ�
	/// </summary>
	public interface infPrintRecord
	{
		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
		/// </summary>
		void m_mthInitPrintContent();
		
		/// <summary>
		/// ��ʼ����ӡ����
		/// </summary>
		/// <param name="p_objArg">�ⲿ��Ҫ��ʼ���ı��������ݲ�ͬ��ʵ��ʹ��</param>
		void m_mthInitPrintTool(object p_objArg);

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		/// <param name="p_objArg">�ⲿʹ�õ��ı��������ݲ�ͬ��ʵ��ʹ��</param>
		void m_mthDisposePrintTools(object p_objArg);

		/// <summary>
		/// ��ӡ��ʼ
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		void m_mthBeginPrint(object p_objPrintArg);

		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		void m_mthPrintPage(object p_objPrintArg);

		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		void m_mthEndPrint(object p_objPrintArg);
	}
}
