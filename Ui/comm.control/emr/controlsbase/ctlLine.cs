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
    [ToolboxBitmap(typeof(ctlLine), "Resources.垂直线.png")]
    public partial class ctlLine : UserControl, IRuntimeDesignControl, ICtlLine
    {
        public ctlLine()
        {
            InitializeComponent();
            this.ForeColor = Color.Black;
            this.BackColor = Color.Transparent;
            //this.SetStyle(ControlStyles.FixedHeight, true);
            this.AutoScaleMode = AutoScaleMode.None;
            this.LineStyle = CtlLineStyle.Dash;
            this.BorderStyle = BorderStyle.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (linewidth > 1)
                intHeight = 10;
            else
            {
                intHeight = 2;
            }

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
            e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
        }


        private int _intHeight = MaxHeight;
        [Category("其他"), Description("高度")]
        public int intHeight
        {
            get
            {
                if (_intHeight < 0)
                    _intHeight = MaxHeight;                               
                
                return _intHeight;
            }
            set
            {
                _intHeight = value;
            }
        }

        const int MaxHeight = 10;
        //public override Size MaximumSize
        //{
        //    get
        //    {
        //        return new Size(base.MaximumSize.Width, intHeight);
        //    }
        //    set
        //    {
        //        base.MaximumSize = new Size(value.Width, intHeight);
        //    }
        //}


        //public override Size MinimumSize
        //{
        //    get
        //    {
        //        return new Size(base.MinimumSize.Width, intHeight);
        //    }
        //    set
        //    {
        //        base.MinimumSize = new Size(value.Width, intHeight);
        //    }
        //}

        #region IRuntimeDesignControl 成员

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

        [Browsable(false)]
        public string EssentialGroupNo { get; set; }

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
                //this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
                this.Invalidate();
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
