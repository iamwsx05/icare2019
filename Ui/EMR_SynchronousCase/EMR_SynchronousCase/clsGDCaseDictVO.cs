using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// �㶫����ϵͳ�ֵ����VO�����ڳ�ʼ�������б�
    /// </summary>
    public class clsGDCaseDictVO
    {
        /// <summary>
        /// ����
        /// </summary>
        public string m_strCode = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string m_strName = string.Empty;
        /// <summary>
        /// ��ȡ����Ŀ������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_strName;
        }
    }
}
