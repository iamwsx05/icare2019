using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;

namespace Common.Controls.Emr
{
    [ToolboxBitmap(typeof(DateTime))]
    public partial class ctlDatetime : DevExpress.XtraEditors.DateEdit, IRuntimeDesignControl, IFormCtrl, IXtraDateTime
    {
        public ctlDatetime()
        {
            this.BackColor = Color.Transparent;
            this.prevBackColor = this.BackColor;

            this.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.Width = 140;
            this.Font = new Font("宋体", 9f, FontStyle.Regular);
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BackColor = Color.Transparent;
            this.AllowDrop = true;
            //this.Properties.Appearance.Options.UseForeColor = true;
            this.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Properties.LookAndFeel.SkinName = "Black";
            this.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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

        #region IRuntimeDesignControl 成员

        [Category("IRuntimeDesignControl属性")]
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
                return this.DateTime;
            }
            set
            {
                this.Text = value.ToString();
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

        [Browsable(false)]
        public decimal ZIndex { get; set; }

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

            //if (ShowUnderLine)
            //{
            //    Pen p = new Pen(Brushes.Black);
            //    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //    e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            //}

            if (this.EditValue != null)
            {
                DateTime dt;
                DateTime.TryParse(this.Text, out dt);
                try
                {
                    this.Text = dt.ToString(this.Properties.DisplayFormat.FormatString);
                }
                catch
                {
                    this.Text = dt.ToString();
                }
                if (this.Text.StartsWith("0001-01-01"))
                {
                    this.Text = string.Empty;
                    this.EditValue = null;
                    ValueChangedFlag = false;
                }
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

        #region IXtraDateTime 成员

        [Browsable(false)]
        public DateTime? DateTimeValue
        {
            get
            {
                if (this.EditValue != null && this.EditValue != DBNull.Value && !string.IsNullOrEmpty(this.EditValue.ToString()))
                {
                    return Convert.ToDateTime(this.EditValue);
                }
                return null;
            }
            set
            {
                if (string.IsNullOrEmpty(this._SPDefaultValue))
                {
                    this.EditValue = value;
                }
            }
        }

        [Browsable(false)]
        public string EditMask
        {
            get
            {
                return this.Properties.EditMask;
            }
            set
            {
                this.Properties.EditMask = value;
                this.Properties.DisplayFormat.FormatString = value;
                this.Properties.EditFormat.FormatString = value;
            }
        }

        private string _SPDefaultValue = string.Empty;
        /// <summary>
        /// 特殊默认值
        /// </summary>
        public string SPDefaultValue
        {
            get
            {
                return _SPDefaultValue;
            }
            set
            {
                if (value == null)
                {
                    _SPDefaultValue = string.Empty;
                }
                else
                {
                    _SPDefaultValue = value;
                }
                if (this._SPDefaultValue != null && _SPDefaultValue == "DateTime.Now")
                {
                    this.EditValue = DateTime.Now;
                }
            }
        }
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

        public event HandleItemMouseClick ItemMouseClick;

        protected override void OnEditValueChanging(DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.ValueChangedFlag = true;
            base.OnEditValueChanging(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (ItemMouseClick != null)
            {
                if (this.Tag == null)
                    ItemMouseClick(this, null);
                else
                    ItemMouseClick(this, this.Tag as EntityFormCtrl);
            }
        }

        protected override void OnSpin(DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            this.Enabled = false;
            base.OnSpin(e);
            this.Enabled = true;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.Enabled = true;
            this.Focus();
        }

        private bool m_blnValueStatus = false;

        protected override void OnEditValueChanged()
        {
            if (this.DateTimeValue != null && !string.IsNullOrEmpty(this.Text))
            {
                if (Convert.ToDateTime(this.Text) < Convert.ToDateTime("1901-01-01 00:00:00"))
                {
                    if (this.m_blnValueStatus) return;
                    this.m_blnValueStatus = true;
                    //clsDialog.Msg("时间小于系统允许的最小值(1900-01-01 00:00:00)。", MessageBoxIcon.Information, this.FindForm());
                    this.EditValue = DateTime.Now;
                    this.Refresh();
                    this.m_blnValueStatus = false;
                    return;
                }
            }
            this.m_blnValueStatus = false;
            base.OnEditValueChanged();
        }
    }
}
