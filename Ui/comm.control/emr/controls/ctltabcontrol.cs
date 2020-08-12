using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using DevExpress.XtraTreeList.Nodes;

namespace Common.Controls.Emr
{
    [ToolboxBitmap(typeof(TabControl))]
    public class ctlTabControl : DevExpress.XtraTab.XtraTabControl, IContainerCtrl, ITabControl, IFormCtrl, IRuntimeDesignControl
    {
        private bool prev_UseDefaultLookAndFeel;
        DevExpress.LookAndFeel.LookAndFeelStyle prev_lookandfeelstyle;

        public ctlTabControl()
        {
            prev_UseDefaultLookAndFeel = this.LookAndFeel.UseDefaultLookAndFeel;
            prev_lookandfeelstyle = this.LookAndFeel.Style;
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
