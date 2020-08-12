using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Emr
{
    public interface IXtraDateTime
    {
        /// <summary>
        /// 特殊默认值
        /// </summary>
        string SPDefaultValue { get; set; }
        DateTime? DateTimeValue { get; set; }
        string EditMask { get; set; }
    }
}
