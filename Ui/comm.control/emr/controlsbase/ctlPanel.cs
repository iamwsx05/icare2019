using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;

namespace Common.Controls.Emr
{
    public partial class ctlPanel : Panel, IContainerCtrl, IRuntimeDesignControl, IPanel, IFormCtrl
    {
        public ctlPanel()
        {
            this.BackColor = Color.Transparent;
            this.prevBackColor = this.BackColor;
            this.Columns = 1;
            this.Rows = 1;
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
                //throw new NotImplementedException();
            }
        }

        bool referencetype = true;
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

        [Category("IRuntimeDesignControl属性")]
        [Description("必填组号")]
        public string EssentialGroupNo { get; set; }

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

        //int defaultRows = 1;
        //[Category("IRuntimeDesignControl属性")]
        //[Description("默认行数")]
        //public int DefaultRows
        //{
        //    get
        //    {
        //        return defaultRows;
        //    }
        //    set
        //    {
        //        defaultRows = value;
        //    }
        //}

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

        private int _presentationMode = 0;

        /// <summary>
        /// 展现方式
        /// </summary>
        ////[TypeConverter(typeof(PresentationModeConverter))]
        public int PresentationMode
        {
            get
            {
                return _presentationMode;
            }
            set
            {
                _presentationMode = value;
            }
        }
        Color prevBackColor;


        [Browsable(false)]
        public bool ShowUnderLine { get; set; }
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

        #region IPanel 成员

        int _columns = 1;
        public int Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                if (value < 1)
                {
                    _columns = 1;
                }
                else
                {
                    _columns = value;
                }
                this.Invalidate();
            }
        }

        int _rows = 1;
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                if (value < 1)
                {
                    _rows = 1;
                }
                else
                {
                    _rows = value;
                }
                this.Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float colWidth = (float)this.Width / (float)Columns;

            Pen p = new Pen(Color.Black);
            for (int i = 1; i < Columns; i++)
            {
                e.Graphics.DrawLine(p, new PointF(colWidth * i, 0), new PointF(colWidth * i, this.Height));
            }

            float rowHeight = (float)this.Height / (float)this.Rows;

            for (int j = 1; j < Rows; j++)
            {
                e.Graphics.DrawLine(p, new PointF(0, rowHeight * j), new PointF(this.Width, rowHeight * j));
            }
        }
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
                
        #endregion

    }
}
