using System;
using System.Collections.Generic;
using System.Text;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 广东病案系统字典基本VO，用于初始化界面列表
    /// </summary>
    public class clsGDCaseDictVO
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string m_strCode = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string m_strName = string.Empty;
        /// <summary>
        /// 获取此项目的描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_strName;
        }
    }
}
