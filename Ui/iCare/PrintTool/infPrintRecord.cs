using System;

namespace iCare
{
	/// <summary>
	/// ͳһ�Ĵ�ӡ�ӿ�
	/// </summary>
	public interface infPrintRecord
	{
		/// <summary>
		/// ���ô�ӡ��Ϣ
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate);

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
		/// </summary>
		void m_mthInitPrintContent();

		/// <summary>
		/// ���ô�ӡ���ݡ��������Ѿ�����ʱʹ�á�
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		void m_mthSetPrintContent(object p_objPrintContent);

		/// <summary>
		/// ��ȡ��ӡ����
		/// </summary>
		/// <returns>��ӡ����</returns>
		object m_objGetPrintInfo();
		
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
