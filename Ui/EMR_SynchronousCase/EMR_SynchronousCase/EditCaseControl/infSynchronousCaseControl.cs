using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// 定义各控件的操作
    /// </summary>
    public interface infSynchronousCaseControl
    {
        /// <summary>
        /// 是否已初始化数据
        /// </summary>
        bool m_BlnHasInit
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化病案内容
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strPatientID">病人ID</param>
        void m_mthInitCase(string p_strRegisterID,string p_strPatientID);

        /// <summary>
        /// 根据字典初始化界面固定选项值
        /// </summary>
        /// <param name="p_dtbDict">字典</param>
        void m_mthInitDict(System.Data.DataTable p_dtbDict);
        /// <summary>
        /// 获取病案内容
        /// </summary>
        /// <param name="p_dsCaseContent">病案内容</param>
        void m_mthGetCaseContent(System.Data.DataSet p_dsCaseContent);
        /// <summary>
        /// 初始化病案内容
        /// </summary>
        /// <param name="p_dsContent">数据库中获取的已保存数据</param>
        void m_mthInitCase(System.Data.DataSet p_dsContent);
    }
}
