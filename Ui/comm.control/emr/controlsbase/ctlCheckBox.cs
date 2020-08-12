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
    [ToolboxBitmap(typeof(CheckBox))]
    public partial class ctlCheckBox : DevExpress.XtraEditors.CheckEdit, IRuntimeDesignControl, IFormCtrl, ICheckBox
    {
        public ctlCheckBox()
        {
            this.GroupName = string.Empty;
            this.Font = new Font("宋体", 9f);
            this.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Properties.Appearance.Options.UseForeColor = true;
        }

        public event HandleItemMouseClick ItemMouseClick;
        public event HandleItemClick ItemClick;

        #region 颜色处理
        //private bool _IsUseDefaultForeColor = false;

        //public bool IsUseDefaultForeColor
        //{
        //    get { return _IsUseDefaultForeColor; }
        //    set { _IsUseDefaultForeColor = value; }
        //}

        //public Color _CustomForeColor = Color.FromArgb(21, 66, 139);

        //public override Color ForeColor
        //{
        //    get
        //    {
        //        if (IsUseDefaultForeColor)
        //        {
        //            return Color.Black;
        //        }

        //        return _CustomForeColor;
        //    }
        //    set { _CustomForeColor = value; }
        //}
        #endregion

        #region ICheckBox

        /// <summary>
        /// 分组名
        /// </summary>
        [Browsable(true)]
        public string GroupName { get; set; }

        /// <summary>
        /// 合计名
        /// </summary>
        [Browsable(true)]
        public string SumName { get; set; }

        [Browsable(true)]
        public decimal CheckedWeightValue { get; set; }

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
                return this.Checked;
            }
            set
            {
                if (value is bool)
                {
                    this.Checked = Convert.ToBoolean(value);
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
                        this.BackColor = this.prevBackColor;
                        this.Properties.LookAndFeel.UseDefaultLookAndFeel = true;
                        this.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                    }
                    else if (value == 1 || value == 2 || value == 3)
                    {
                        this.prevBackColor = this.BackColor;
                        this.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                        this.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                        //this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        this.BackColor = Color.Transparent;
                    }
                }
            }
        }
        Color prevBackColor;

        [Browsable(false)]
        public bool ShowUnderLine { get; set; }

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

        protected override void OnEditValueChanging(DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.ValueChangedFlag = true;
            base.OnEditValueChanging(e);
        }

        //protected override void OnClick(EventArgs e)
        //{
        //    if (ItemClick != null)
        //    {
        //        if (this.Tag == null)
        //            ItemClick(this, null);
        //        else
        //            ItemClick(this, this.Tag as EntityEformCtrl);
        //    }
        //    base.OnClick(e);
        //}


    }
}
