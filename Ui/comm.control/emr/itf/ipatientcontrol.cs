using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;

namespace Common.Controls.Emr
{
    public interface IPatientControl
    {
        /// <summary>
        /// 是否显示标题
        /// </summary>
        bool ShowCaption { get; set; }

        string CaptionText { get; set; }

        /// <summary>
        /// 计算年龄类型
        /// </summary>
        int CalcAgeType { get; set; }

        /// <summary>
        /// 病人信息类型
        /// </summary>
        EnumPatientInfoType InfoType { get; set; }

        /// <summary>
        /// enumPatientInfoType类型 获取病人信息对应的字段文本信息
        /// </summary>
        /// <returns></returns>
        string GetDataText();

        /// <summary>
        /// 刷新数据
        /// </summary>
        void RefreshData();

        /// <summary>
        /// 绑定页(主要针对表格式病历)
        /// </summary>
        bool BandingPage { get; set; }

        /// <summary>
        /// 从全局对象获取病人宏元素信息
        /// </summary>
        bool ReadPatientInfoFromGolbolEnv { get; set; }
    }
}
