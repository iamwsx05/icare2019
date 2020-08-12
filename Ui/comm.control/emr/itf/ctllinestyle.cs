using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls
{
    public enum CtlLineStyle
    {
        Dash,
        Solid
    }

    public interface ICtlLine
    {
        CtlLineStyle LineStyle { get; set; }

        /// <summary>
        /// 线条粗度
        /// </summary>
        int LineWidth { get; set; }
    }
}
