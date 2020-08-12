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
    [ToolboxBitmap(typeof(ComboBox))]
    public partial class ctlComboBox : DevExpress.XtraEditors.ComboBoxEdit, IRuntimeDesignControl, IFormCtrl, ICombox
    {
        public ctlComboBox()
        {
            this.BackColor = Color.Transparent;
            this.prevBackColor = this.BackColor;

            this.Font = new Font("宋体", 9f);
            this.BackColor = Color.Transparent;
            this.AllowDrop = true;
            this.Properties.Appearance.Options.UseForeColor = true;
            this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            if (this.DesignMode == false)
            {
                //this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                this.Properties.LookAndFeel.SkinName = "Sliver";
                this.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            }
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
                return this.Text;
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

        [Browsable(false)]
        public string EssentialGroupNo { get; set; }

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
                    if (DesignMode) return;
                    if (value == 0)
                    {
                        this.BorderStyle = this.prevBorderStyle;
                        this.BackColor = this.prevBackColor;
                    }
                    else if (value == 1 || value == 2 || value == 3)
                    {
                        this.prevBorderStyle = this.BorderStyle;
                        this.prevBackColor = this.BackColor;

                        //this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
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

            if (_presentationMode == 1) /* && _showunderline)*/
            {
                this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                p.DashPattern = ConstValue.DashPattern;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));

                //Pen p = new Pen(Brushes.Black);
                //p.DashStyle = System.Drawing.Drawing2D.DashStyle.
                //e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            }
            else if (_presentationMode == 3)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
            }
            else
            {
                if (ShowUnderLine)
                {
                    Pen p = new Pen(Brushes.Black);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    p.DashPattern = ConstValue.DashPattern;
                    //p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    e.Graphics.DrawLine(p, new Point(0, this.Height - 2), new Point(this.Width - 2, this.Height - 2));
                }
            }
        }

        bool _showunderline = false;
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

        #region ICombo

        public DevExpress.XtraEditors.Controls.ComboBoxItemCollection Items
        {
            get { return this.Properties.Items; }
        }

        public string DictionaryName { get; set; }

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
    }
}
