using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 单元格文本属性
    /// </summary>
    public class TextProperty
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 前景颜色
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 是否垂直显示问题 是:垂直显示 否:水平显示
        /// </summary>
        public bool IsVerticalText { get; set; }

        /// <summary>
        /// 大小是否自适应
        /// </summary>
        public bool AutoSize { get; set; }

        /// <summary>
        /// 水平对其方式 0:left 1:center 2:right
        /// </summary>
        public int AlignHort { get; set; }

        /// <summary>
        /// 垂直对其方式 0:top 1:middle 2:bottom
        /// </summary>
        public int AlignVert { get; set; }

        public TextProperty()
        {
            this.ForeColor = Color.Black;
            this.Font = new Font(ThreeItemConstValue.DefaultFontFamilyName, ThreeItemConstValue.DefaultFontSize);

            this.AutoSize = true;

            AlignHort = 1;
            AlignVert = 1;

            IsVerticalText = false;
        }
    }
}
