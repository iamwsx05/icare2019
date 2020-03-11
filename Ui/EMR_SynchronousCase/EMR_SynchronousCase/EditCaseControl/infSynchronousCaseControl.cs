using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// ������ؼ��Ĳ���
    /// </summary>
    public interface infSynchronousCaseControl
    {
        /// <summary>
        /// �Ƿ��ѳ�ʼ������
        /// </summary>
        bool m_BlnHasInit
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strPatientID">����ID</param>
        void m_mthInitCase(string p_strRegisterID,string p_strPatientID);

        /// <summary>
        /// �����ֵ��ʼ������̶�ѡ��ֵ
        /// </summary>
        /// <param name="p_dtbDict">�ֵ�</param>
        void m_mthInitDict(System.Data.DataTable p_dtbDict);
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_dsCaseContent">��������</param>
        void m_mthGetCaseContent(System.Data.DataSet p_dsCaseContent);
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="p_dsContent">���ݿ��л�ȡ���ѱ�������</param>
        void m_mthInitCase(System.Data.DataSet p_dsContent);
    }
}
