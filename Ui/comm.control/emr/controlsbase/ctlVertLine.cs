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
    [ToolboxBitmap(typeof(ctlLine), "Icon.ctlVertLine.bmp")]
    public partial class ctlVertLine : UserControl, IRuntimeDesignControl, ICtlLine
    {
        public ctlVertLine()
        {
            InitializeComponent();

            this.ForeColor = Color.Black;
            this.BackColor = Color.Transparent;

            this.AutoScaleMode = AutoScaleMode.None;

            this.LineStyle = CtlLineStyle.Dash;
            this.BorderStyle = BorderStyle.None;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Pen p = new Pen(this.ForeColor);
            p.Width = this.linewidth;
            if (this.LineStyle == CtlLineStyle.Dash)
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                p.DashPattern = ConstValue.DashPattern;
            }
            else if (this.LineStyle == CtlLineStyle.Solid)
            {

            }
            e.Graphics.DrawLine(p, new Point(2, 0), new Point(2, this.Height));
        }


        private int _intWidth = MaxWidth;
        [Category("其他"), Description("宽度")]
        public int intWidth
        {
            get
            {
                if (_intWidth < 0)
                    _intWidth = MaxWidth;
                return _intWidth;
            }
            set
            {
                _intWidth = value;
            }
        }

        const int MaxWidth = 3;// 10;
        //public override Size MaximumSize
        //{
        //    get
        //    {
        //        return new Size(intWidth, base.MaximumSize.Height);
        //    }
        //    set
        //    {
        //        base.MaximumSize = new Size(intWidth, value.Height);
        //    }
        //}


        //public override Size MinimumSize
        //{
        //    get
        //    {
        //        return new Size(intWidth, base.MinimumSize.Height);
        //    }
        //    set
        //    {
        //        base.MinimumSize = new Size(intWidth, value.Height);
        //    }
        //}

        #region IRuntimeDesignControl 成员

        [Browsable(false)]
        public string EssentialGroupNo { get; set; }

        public object EditObject
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

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

        bool referencetype = true;
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

        /// <summary>
        /// 展现方式
        /// </summary>
        //[TypeConverter(typeof(PresentationModeConverter))]
        public int PresentationMode { get; set; }

        [Browsable(false)]
        public bool ShowUnderLine { get; set; }
        #endregion

        #region ILine 成员       

        private CtlLineStyle _lineStyle;
        public CtlLineStyle LineStyle
        {
            get
            {
                return _lineStyle;
            }
            set
            {
                _lineStyle = value;
                this.Invalidate();
                //this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
            }
        }


        private int linewidth = 1;

        /// <summary>
        /// 线条粗度
        /// </summary>
        public int LineWidth
        {
            get
            {
                return linewidth;
            }
            set
            {
                if (value < 1)
                {
                    linewidth = 1;
                }
                else
                {
                    linewidth = value;
                }
                this.Invalidate();
            }
        }

        #endregion
    }
}
