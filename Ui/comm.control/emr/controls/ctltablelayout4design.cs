using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    [ToolboxBitmap(typeof(DataGridView))]
    public partial class ctlTableLayout4Design : Panel, IRuntimeDesignControl, IFormCtrl
    {
        public ctlTableLayout4Design()
        {
            this.BackColor = Color.Transparent;
            this.prevBackColor = this.BackColor;
        }

        #region IRuntimeDesignControl 成员

        [Category("IRuntimeDesignControl属性")]
        [Description("运行时只读")]
        public bool RunTimeReadOnly
        {
            get
            {
                return !this.Enabled;
            }
            set
            {
            }
        }

        bool referencetype = false;
        [Category("IRuntimeDesignControl属性")]
        [Description("是否资料调用")]
        public bool Referencetype
        {
            get
            {
                return referencetype;
            }
            set
            {
                referencetype = value;
            }
        }
        
        bool essential = false;
        [Category("IRuntimeDesignControl属性")]
        [Description("是否必填")]
        public bool Essential
        {
            get
            {
                return essential;
            }
            set
            {
                essential = value;
            }
        }
        
        int masktype = 0;
        [Category("IRuntimeDesignControl属性")]
        [Description("掩码类型")]
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
        }

        [Category("IRuntimeDesignControl属性")]
        [Description("编辑值")]
        public object EditObject
        {
            get
            {
                return string.Empty;
            }
            set
            {

            }
        }

        [Browsable(false)]
        public Font TextFont
        {
            get
            {
                return this.Font;
            }
            set
            {
                this.Font = value;
            }
        }

        [Browsable(false)]
        public decimal ZIndex { get; set; }

        private bool visible4design = true;

        [Browsable(false)]
        public bool Visible4Design
        {
            get
            {
                return visible4design;
            }
            set
            {
                visible4design = value;
            }
        }
        Color prevBackColor;

        [Browsable(false)]
        public bool ShowUnderLine { get; set; }
        #endregion

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
        public bool IsReferenceType { get; set; }
                        
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

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString(string.Format("[表格病历] \r\n \r\n 表格代码 - {0}", this.ItemName), this.Font, Brushes.Black, new PointF(5, 5));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }
    }
}
