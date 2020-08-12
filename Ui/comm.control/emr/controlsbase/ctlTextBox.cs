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
    [ToolboxBitmapAttribute(typeof(TextBox))]
    public partial class ctlTextBox : DevExpress.XtraEditors.TextEdit, IRuntimeDesignControl, IFormCtrl
    {
        public ctlTextBox()
        {
            //InitializeComponent();

            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BackColor = Color.White;
            this.AllowDrop = true;

            this.Font = new Font("宋体", 9f);
        }

        private bool _IsUseUpDownSkip = true;

        [Category("杂项")]
        [Description("是否使用上下键调整")]
        public bool IsUseUpDownSkip
        {
            get { return _IsUseUpDownSkip; }
            set { _IsUseUpDownSkip = value; }
        }

        #region 按键
        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && IsUseUpDownSkip)
            {
                SendKeys.SendWait("+{TAB}");
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (this.SelectionStart == 0 && this.SelectionLength == 0)
                {
                    SendKeys.SendWait("+{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Down && IsUseUpDownSkip)
            {
                SendKeys.SendWait("{TAB}");
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (this.SelectionStart == this.Text.Length && this.SelectionLength == 0)
                {
                    SendKeys.SendWait("{TAB}");
                }
            }

            base.OnKeyDown(e);
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
        /// 计算类型
        /// </summary>
        [Browsable(false)]
        public string CalProperty { get; set; }

        /// <summary>
        /// 行缩进字符个数
        /// </summary>
        [Browsable(false)]
        public int RowShrinkDigit
        {
            get { return this.Properties.MaxLength; }
            set { this.Properties.MaxLength = value; }
        }

        #endregion

        #region IRuntimeDesignControl 成员

        /// <summary>
        /// 默认行数
        /// </summary>
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
        //        if (value <= 0)
        //        {
        //            defaultRows = 1;
        //        }
        //        else
        //        {
        //            defaultRows = value;
        //        }
        //    }
        //}

        int masktype = 0;
        [Category("IRuntimeDesignControl属性")]
        [Description("掩码类型")]
        public int MaskType
        {
            get { return masktype; }
            set
            {
                if (value == 0)
                    this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
                else if (value == 1)
                {
                    this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    this.Properties.EditFormat.FormatString = "######";
                }
                else if (value == 2)
                    this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                else
                    this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
                masktype = value;
            }
        }

        [Category("字段属性")]
        [Description("运行时只读")]
        public bool RunTimeReadOnly
        {
            get
            {
                return this.Properties.ReadOnly;
            }
            set
            {
                this.Properties.ReadOnly = value;
            }
        }

        bool referencetype = true;
        [Category("字段属性")]
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

        [Browsable(false)]
        [Category("IRuntimeDesignControl属性")]
        [Description("编辑值")]
        public object EditObject
        {
            get
            {
                return this.Text;
            }
            set
            {
                if (value != null && value != DBNull.Value)
                {
                    this.Text = value.ToString();
                }
                else
                {
                    this.Text = string.Empty;
                }
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

        private int _presentationMode = 1;

        /// <summary>
        /// 展现方式
        /// </summary>
        //[TypeConverter(typeof(PresentationModeConverter))]
        public int PresentationMode
        {
            get
            {
                return _presentationMode;
            }
            set
            {
                if (_presentationMode != value)
                {
                    _presentationMode = value;

                    if (value == 0)
                    {
                        this.BorderStyle = this.prevBorderStyle;
                        this.BackColor = this.prevBackColor;
                    }
                    else if (value == 1 || value == 2 || value == 3)
                    {
                        this.prevBorderStyle = this.BorderStyle;
                        this.prevBackColor = this.BackColor;

                        this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        this.BackColor = Color.White;
                    }
                    else if (value == 4)
                    {
                        this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                        this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        this.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        this.Properties.LookAndFeel.SkinName = "Black";
                    }
                }
            }
        }

        /// <summary>
        /// 记录改变前的边框式样
        /// </summary>
        DevExpress.XtraEditors.Controls.BorderStyles prevBorderStyle;
        Color prevBackColor;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_presentationMode == 1 /*&& _showunderline*/)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                p.DashPattern = ConstValue.DashPattern;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            }
            else if (_presentationMode == 3)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            }
            else if (_presentationMode == 4)
            {
                this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                this.Properties.LookAndFeel.SkinName = "Black";
            }
        }

        bool _showunderline = true;
        /// <summary>
        /// 是否显示下划线
        /// </summary>
        public bool ShowUnderLine
        {
            get
            {
                return _showunderline;
            }
            set
            {
                _showunderline = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public string EssentialGroupNo { get; set; }

        #endregion

        #region 值变化标志
        /// <summary>
        /// 值变化标志
        /// </summary>
        private bool _blnValueChangedFlag = false;
        /// <summary>
        /// 值变化标志
        /// </summary>
        public bool ValueChangedFlag
        {
            get { return _blnValueChangedFlag; }
            set { _blnValueChangedFlag = value; }
        }
        #endregion

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.BringToFront();
        }

        protected override void OnEditValueChanged()
        {
            this.ValueChangedFlag = true;
            base.OnEditValueChanged();
        }

        protected override void OnSpin(DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            if (this.Properties.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
            {
                this.Enabled = false;
            }
            base.OnSpin(e);
            this.Enabled = true;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.Properties.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
            {
                this.Enabled = true;
                this.Focus();
                return;
            }

            base.OnMouseWheel(e);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            drgevent.Effect = DragDropEffects.Move;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                this.Text = this.Text.Insert(this.SelectionStart, Convert.ToString(e.Data.GetData(typeof(System.String))).Trim());
            }
            else
            {
                //原有默认方法
                base.OnDragDrop(e);
            }
        }

    }
}
