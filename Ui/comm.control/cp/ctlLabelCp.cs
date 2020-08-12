using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Common.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// ctlLabel
    /// </summary>
    public class ctlLabelEf : System.Windows.Forms.Label, IRuntimeDesignControl, IFormCtrl
    {
        /// <summary>
        /// ctlLabel
        /// </summary>
        public ctlLabelEf()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BackColor = Color.Transparent;
            this.AllowDrop = true;

            this.Font = new Font("宋体", 9f); 
            this.TextAlign = ContentAlignment.MiddleLeft;
        }

        #region IEfCtrl

        /// <summary>
        /// 项目代码(名称)
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目标题(描述)
        /// </summary>
        public string ItemCaption { get; set; }

        /// <summary>
        /// 父节点名
        /// </summary>
        public string ParentNode { get; set; }

        /// <summary>
        /// 值变化标志
        /// </summary>
        public bool ValueChangedFlag { get; set; }
        
        /// <summary>
        /// 计算类型
        /// </summary>
        [Browsable(false)]
        public string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [Browsable(false)]
        public int RowShrinkDigit { get; set; }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        [Browsable(false)]
        public bool Referencetype { get; set; }

        /// <summary>
        /// (是否)必须
        /// </summary>
        [Browsable(false)]
        public bool Essential { get; set; }
        
        /// <summary>
        /// 展现方式
        /// </summary>
        [Browsable(false)]
        public int PresentationMode
        {
            get;
            set;
        }

        #endregion
        
        #region IRuntimeDesignControl 成员

        /// <summary>
        /// Z轴深度
        /// </summary>
        [Browsable(false)]
        public decimal ZIndex { get; set; }


        /// <summary>
        /// 编辑值
        /// </summary>
        [Browsable(false)]
        public object EditObject { get; set; }

        /// <summary>
        /// 运行时只读
        /// </summary>
        [Browsable(false)]
        public bool RunTimeReadOnly { get; set; }

        ///// <summary>
        ///// 展示方式
        ///// </summary>
        //[Browsable(false)]
        //public int PresentationMode { get; set; }

        //
        // 摘要:
        //     获取或设置 System.ComponentModel.Component 的 System.ComponentModel.ISite。
        //
        // 返回结果:
        //     与 System.ComponentModel.Component 关联的 System.ComponentModel.ISite（如果有）。如果
        //     System.ComponentModel.Component 未封装在 System.ComponentModel.IContainer 中，System.ComponentModel.Component
        //     没有与其关联的 System.ComponentModel.ISite 或者 System.ComponentModel.Component 已从其
        //     System.ComponentModel.IContainer 中移除，则为 null。
        //System.ComponentModel.ISite Site { get; set; }


        /// <summary>
        /// 用于存储当前控件是否可见值，如只用Visible字段的话，在设计时若把Visible=false则控件会消失
        /// 所以要另开字段存储：只供设计时使用
        /// Visible则在控件加载时使用
        /// </summary>
        [Browsable(false)]
        public bool Visible4Design { get; set; }

        /// <summary>
        /// 是否显示下划线
        /// </summary>
        [Browsable(false)]
        public bool ShowUnderLine { get; set; }

        /// <summary>
        /// 掩码类型
        /// </summary>
        [Browsable(false)]
        public int MaskType { set; get; }

        /// <summary>
        /// 同组必填控件组号(1,2,3... a,b,c...)
        /// </summary>
        [Browsable(false)]
        public string EssentialGroupNo { set; get; }

        #endregion

        public Font TextFont { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            #region 为实现文本的行间距随控件高度变化_性能较差_在有了较好的设计时多行编辑控件使用base.OnPaint(e)即可
            if (this.AutoSize)
            {
                base.OnPaint(e);
            }
            else
            {
                int rowCount = 1;
                DrawLabel(ref rowCount, e.Graphics, false, 0);
                if (rowCount == 1)
                {
                    base.OnPaint(e);
                }
                else
                {
                    int rowHeight = this.Height / rowCount;
                    DrawLabel(ref rowCount, e.Graphics, true, rowHeight);
                }
            }
            #endregion

        }

        private void DrawLabel(ref int rowCount, Graphics g, bool draw, int rowHeight)
        {
            int y = 1;
            int x = 1;
            rowCount = 1;

            //计算行高
            for (int i = 0; i < this.Text.Length; i++)
            {
                string strThis = this.Text[i].ToString();

                if (strThis == "\t")
                {
                    strThis = " ";
                }

                int charWidth = (int)g.MeasureString(strThis, this.Font).Width;
                if (strThis != " ")
                {
                    if (System.Text.Encoding.Default.GetByteCount(strThis) == 1)
                    {
                        //英文
                        charWidth -= charWidth / 2;
                    }
                    else
                    {
                        //汉字
                        charWidth -= (charWidth * 2 / 7);
                    }
                }
                else
                {
                    charWidth += charWidth / 2;
                }

                if (draw)
                {
                    g.DrawString(strThis, this.Font, new SolidBrush(this.ForeColor), x, y);
                }

                int nextCharWidth = 0;
                if (i != this.Text.Length - 1)
                {
                    string strNext = this.Text[i + 1].ToString();
                    nextCharWidth = (int)g.MeasureString(strNext, this.Font).Width;
                    if (System.Text.Encoding.Default.GetByteCount(strNext) == 1)
                    {
                        //英文
                        nextCharWidth -= nextCharWidth / 2;
                    }
                    else
                    {
                        //汉字
                        nextCharWidth -= (nextCharWidth * 2 / 7);
                    }
                }

                if (strThis == "\n" || (x + charWidth + nextCharWidth > this.Width - 2))
                {
                    rowCount++;
                    x = 0;
                    y += rowHeight;
                }
                else
                {
                    x += charWidth;
                }
            }
        }
    }
}