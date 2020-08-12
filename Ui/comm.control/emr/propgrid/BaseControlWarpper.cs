using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public class BaseControlWarpper : BaseObjectPropertyWrapper<IRuntimeDesignControl>
    {
        #region 接口

        internal ICpNode ICpNode;
        internal IFormCtrl IFormCtrl;
        internal ICheckBox IChkBox;
        internal ICombox ICbx;
        internal ICtlLine ILine;
        internal IPictureBox IPic;
        internal IPanel IPnl;
        internal ITabControl ITabCtrl;
        internal ISignatureControl ISign;
        internal IRtfEditor IRtf;
        internal IXtraDateTime IDateTime;

        [Browsable(false)]
        public int FormId { get; set; }
        [Browsable(false)]
        public ControlPropertyGrid PropertyGrid { get; set; }

        #endregion

        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="host"></param>
        public BaseControlWarpper(IRuntimeDesignControl ctrl, IDesignerHost host)
            : base(host)
        {
            if (ctrl == null)
            {
                this.WrappedObject = null;
            }
            else
            {
                this.WrappedObject = ctrl;
                ICpNode = ctrl as ICpNode;
                IFormCtrl = ctrl as IFormCtrl;
                IChkBox = ctrl as ICheckBox;
                ICbx = ctrl as ICombox;
                ILine = ctrl as ICtlLine;
                IPic = ctrl as IPictureBox;
                IPnl = ctrl as IPanel;
                ITabCtrl = ctrl as ITabControl;
                ISign = ctrl as ISignatureControl;
                IRtf = ctrl as IRtfEditor;
                IDateTime = ctrl as IXtraDateTime;
                if (ICbx != null)
                {
                    ICbx.Items.CollectionChanged -= new CollectionChangeEventHandler(Items_CollectionChanged);
                    ICbx.Items.CollectionChanged += new CollectionChangeEventHandler(Items_CollectionChanged);
                }
                if (ITabCtrl != null)
                {
                    ITabCtrl.TabPages.CollectionChanged -= new CollectionChangeEventHandler(TabPages_CollectionChanged);
                    ITabCtrl.TabPages.CollectionChanged += new CollectionChangeEventHandler(TabPages_CollectionChanged);
                }
            }
        }
        #endregion

        #region 布局

        [Category("布局")]
        public System.Windows.Forms.AnchorStyles 锚定样式
        {
            get
            {
                return this.WrappedObject.Anchor;
            }
            set
            {
                if (this.WrappedObject.Anchor != value)
                {
                    OnPropertyChanging("Anchor");
                    object oldValue = this.WrappedObject.Anchor;
                    this.WrappedObject.Anchor = value;
                    OnPropertyChanged("Anchor", oldValue, value);
                }
            }
        }

        [Category("布局")]
        public int 宽度
        {
            get
            {
                return this.WrappedObject.Width;
            }
            set
            {
                if (this.WrappedObject.Width != value)
                {
                    OnPropertyChanging("Width");
                    object oldValue = this.WrappedObject.Width;
                    this.WrappedObject.Width = value;
                    OnPropertyChanged("Width", oldValue, value);
                }
            }
        }

        [Category("布局")]
        public int 高度
        {
            get
            {
                return this.WrappedObject.Height;
            }
            set
            {
                if (this.WrappedObject.Height != value)
                {
                    OnPropertyChanging("Height");
                    object oldValue = this.WrappedObject.Height;
                    this.WrappedObject.Height = value;
                    OnPropertyChanged("Height", oldValue, this.WrappedObject.Height);
                }
            }
        }

        [Category("布局")]
        public System.Drawing.Point 坐标
        {
            get
            {
                return this.WrappedObject.Location;
            }
            set
            {
                if (this.WrappedObject.Location != value)
                {
                    OnPropertyChanging("Location");
                    object oldValue = this.WrappedObject.Location;
                    this.WrappedObject.Location = value;
                    OnPropertyChanged("Location", oldValue, value);
                }
            }
        }

        #endregion

        #region 设计

        [Category("设计")]
        public System.Drawing.Font 字体
        {
            get
            {
                return this.WrappedObject.TextFont;
            }
            set
            {
                OnPropertyChanging("TextFont");
                object oldValue = this.WrappedObject.TextFont;
                this.WrappedObject.TextFont = value;
                ((Control)this.WrappedObject).Font = value;
                OnPropertyChanged("TextFont", oldValue, value);
            }
        }

        [Category("设计")]
        public System.Drawing.Color 前景色
        {
            get
            {
                return this.WrappedObject.ForeColor;
            }
            set
            {
                if (this.WrappedObject.ForeColor != value)
                {
                    OnPropertyChanging("ForeColor");
                    object oldValue = this.WrappedObject.ForeColor;
                    this.WrappedObject.ForeColor = value;
                    OnPropertyChanged("ForeColor", oldValue, value);
                }
            }
        }

        [Category("设计")]
        public System.Drawing.Color 背景色
        {
            get
            {
                return this.WrappedObject.BackColor;
            }
            set
            {
                if (this.WrappedObject.BackColor != value)
                {
                    OnPropertyChanging("BackColor");
                    object oldValue = this.WrappedObject.BackColor;
                    this.WrappedObject.BackColor = value;
                    OnPropertyChanged("BackColor", oldValue, value);
                }
            }
        }

        [Category("设计")]
        [Editor(typeof(StringArrayEditor), typeof(UITypeEditor))]
        public string 文本值
        {
            get
            {
                return this.WrappedObject.Text;
            }
            set
            {
                if (this.WrappedObject.Text != value)
                {
                    OnPropertyChanging("Text");
                    object oldValue = this.WrappedObject.Text;
                    this.WrappedObject.Text = value;
                    OnPropertyChanged("Text", oldValue, value);
                }
                this.WrappedObject.Text = value;
            }
        }

        [Category("设计")]
        public string 实例
        {
            get
            {
                if (this.WrappedObject.Site != null)
                {
                    return this.WrappedObject.Site.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [Category("设计")]
        public int Tab顺序
        {
            get
            {
                return this.WrappedObject.TabIndex;
            }
            set
            {
                if (this.WrappedObject.TabIndex != value)
                {
                    OnPropertyChanging("TabIndex");
                    object oldValue = this.WrappedObject.TabIndex;
                    this.WrappedObject.TabIndex = value;
                    OnPropertyChanged("TabIndex", oldValue, value);
                }
            }
        }

        [Category("设计")]
        [TypeConverter(typeof(PresentationModeConverter))]
        public int 样式
        {
            get
            {
                return WrappedObject.PresentationMode;
            }
            set
            {
                if (this.WrappedObject.PresentationMode != value)
                {
                    OnPropertyChanging("PresentationMode");
                    object oldValue = this.WrappedObject.PresentationMode;
                    this.WrappedObject.PresentationMode = value;
                    OnPropertyChanged("PresentationMode", oldValue, value);
                }
            }
        }

        [Category("字段")]
        [TypeConverter(typeof(BoolToStringConverter))]
        [DefaultValue(false)]
        public bool 是否资料引用
        {
            get
            {
                return this.WrappedObject.Referencetype;
            }
            set
            {
                if (this.WrappedObject.Referencetype != value)
                {
                    OnPropertyChanging("Referencetype");
                    object oldValue = this.WrappedObject.Referencetype;
                    this.WrappedObject.Referencetype = value;
                    OnPropertyChanged("Referencetype", oldValue, value);
                }
            }
        }

        [Category("字段")]
        [TypeConverter(typeof(BoolToStringConverter))]
        [DefaultValue(false)]
        public bool 是否必填
        {
            get
            {
                return this.WrappedObject.Essential;
            }
            set
            {
                if (this.WrappedObject.Essential != value)
                {
                    OnPropertyChanging("Essential");
                    object oldValue = this.WrappedObject.Essential;
                    this.WrappedObject.Essential = value;
                    OnPropertyChanged("Essential", oldValue, value);
                }
            }
        }


        #endregion

        #region IFormCtrl

        [Category("字段")]
        public string 项目代码
        {
            get
            {
                if (IFormCtrl != null)
                {
                    return IFormCtrl.ItemName;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (IFormCtrl != null)
                {
                    if (IFormCtrl.ItemName != value)
                    {
                        foreach (Component comp in this.designerhost.Container.Components)
                        {
                            if (comp is IFormCtrl && (IFormCtrl)comp != IFormCtrl)
                            {
                                if (((IFormCtrl)comp).ItemName == value && value != null && value.Trim() != string.Empty)
                                {
                                    DialogBox.Msg(string.Format("项目代码[{0}]已存在！", value), System.Windows.Forms.MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                        OnPropertyChanging("ItemName");
                        object oldValue = IFormCtrl.ItemName;

                        string val = string.Empty;
                        if (value != null)
                        {
                            val = value.Trim();
                        }

                        IFormCtrl.ItemName = val;
                        OnPropertyChanged("ItemName", oldValue, val);


                    }
                }
            }
        }

        [Category("字段")]
        public string 项目描述
        {
            get
            {
                if (IFormCtrl != null)
                {
                    return IFormCtrl.ItemCaption;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (IFormCtrl != null)
                {
                    if (IFormCtrl.ItemCaption != value)
                    {
                        OnPropertyChanging("ItemCaption");
                        object oldValue = IFormCtrl.ItemCaption;
                        IFormCtrl.ItemCaption = value;
                        OnPropertyChanged("ItemCaption", oldValue, value);
                    }
                }
            }
        }

        [Category("字段")]
        public string 父节点名
        {
            get
            {
                if (IFormCtrl != null)
                {
                    return IFormCtrl.ParentNode;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (IFormCtrl != null)
                {
                    if (IFormCtrl.ParentNode != value)
                    {
                        OnPropertyChanging("ParentNode");
                        object oldValue = IFormCtrl.ParentNode;
                        IFormCtrl.ParentNode = value;
                        OnPropertyChanged("ParentNode", oldValue, value);
                    }
                }
            }
        }

        [Category("字段")]
        [TypeConverter(typeof(ItemTypeConverter))]
        public string 项目类型
        {
            get
            {
                if (IFormCtrl != null)
                {
                    return IFormCtrl.ItemType;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (IFormCtrl != null)
                {
                    if (IFormCtrl.ItemType != value)
                    {
                        OnPropertyChanging("ItemType");
                        object oldValue = IFormCtrl.ItemType;
                        IFormCtrl.ItemType = value;
                        OnPropertyChanged("ItemType", oldValue, value);
                    }
                }
            }
        }

        //[Category("字段")]
        //public string 计算属性
        //{
        //    get
        //    {
        //        if (IFormCtrl != null)
        //        {
        //            return IFormCtrl.CalProperty;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        if (IFormCtrl != null)
        //        {
        //            if (IFormCtrl.CalProperty != value)
        //            {
        //                OnPropertyChanging("CalProperty");
        //                object oldValue = IFormCtrl.CalProperty;
        //                IFormCtrl.CalProperty = value;
        //                OnPropertyChanged("CalProperty", oldValue, value);
        //            }
        //        }
        //    }
        //}

        [Category("字段")]
        public int 缩进字符数
        {
            get
            {
                if (IFormCtrl != null)
                {
                    return IFormCtrl.RowShrinkDigit;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (IFormCtrl != null)
                {
                    if (IFormCtrl.RowShrinkDigit != value)
                    {
                        OnPropertyChanging("RowShrinkDigit");
                        object oldValue = IFormCtrl.RowShrinkDigit;
                        IFormCtrl.RowShrinkDigit = value;
                        OnPropertyChanged("RowShrinkDigit", oldValue, value);
                    }
                }
            }
        }

        //[TypeConverter(typeof(BoolToStringConverter))]
        //[Category("字段"), Description("是否自动签名 0 否 1 是")]
        //[DefaultValue(true)]
        //public bool 是否自动签名
        //{
        //    get
        //    {
        //        return (IFormCtrl.IsAutoSignature == 1);
        //    }
        //    set
        //    {
        //        bool b = (IFormCtrl.IsAutoSignature == 1);
        //        if (b != value)
        //        {
        //            OnPropertyChanging("IsAutoSignature");
        //            object oldValue = b;
        //            IFormCtrl.IsAutoSignature = (value ? 1 : 0);
        //            OnPropertyChanged("IsAutoSignature", oldValue, value);
        //        }
        //    }
        //}

        //[TypeConverter(typeof(BoolToStringConverter))]
        //[Category("字段"), Description("是否允许空签 0 否 1 是")]
        //[DefaultValue(true)]
        //public bool 是否允许空签
        //{
        //    get
        //    {
        //        return (IFormCtrl.IsAllowSignNull == 1);
        //    }
        //    set
        //    {
        //        bool b = (IFormCtrl.IsAllowSignNull == 1);
        //        if (b != value)
        //        {
        //            OnPropertyChanging("IsAllowSignNull");
        //            object oldValue = b;
        //            IFormCtrl.IsAllowSignNull = (value ? 1 : 0);
        //            OnPropertyChanged("IsAllowSignNull", oldValue, value);
        //        }
        //    }
        //}

        #endregion

        #region IPictureBox
        /// <summary>
        /// PictureBox.文件名
        /// </summary>
        [Category("PictureBox属性")]
        public string 文件名
        {
            get
            {
                if (IPic != null)
                {
                    return IPic.FileName;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (IPic != null)
                {
                    if (IPic.FileName != value)
                    {
                        OnPropertyChanging("FileName");
                        object oldValue = IPic.FileName;
                        IPic.FileName = value;
                        OnPropertyChanged("FileName", oldValue, value);
                    }
                }
            }
        }
        #endregion

        #region ICPNode

        [Category("节点")]
        public string 名称
        {
            get
            {
                if (ICpNode != null)
                {
                    return ICpNode.NodeName;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (ICpNode != null)
                {
                    if (ICpNode.NodeName != value)
                    {
                        foreach (Component comp in this.designerhost.Container.Components)
                        {
                            if (comp is ICpNode && (ICpNode)comp != ICpNode)
                            {
                                if (((ICpNode)comp).NodeName == value && value != null && value.Trim() != string.Empty)
                                {
                                    DialogBox.Msg(string.Format("节点名[{0}]已存在！", value), System.Windows.Forms.MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                        OnPropertyChanging("NodeName");
                        object oldValue = ICpNode.NodeName;

                        string val = string.Empty;
                        if (value != null)
                        {
                            val = value.Trim();
                        }

                        ICpNode.NodeName = val;
                        OnPropertyChanged("NodeName", oldValue, val);
                    }
                }
            }
        }

        //[Category("节点")]
        //public string 标题
        //{
        //    get
        //    {
        //        if (IsBindable)
        //        {
        //            return CPNode.NodeCaption;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        if (IsBindable)
        //        {
        //            if (CPNode.NodeCaption != value)
        //            {
        //                OnPropertyChanging("NodeCaption");
        //                object oldValue = CPNode.NodeCaption;
        //                CPNode.NodeCaption = value;
        //                OnPropertyChanged("NodeCaption", oldValue, value);
        //            }
        //        }
        //    }
        //}

        [Category("节点")]
        [TypeConverter(typeof(CpNodeTypeConverter))]
        public string 类型
        {
            get
            {
                if (ICpNode != null)
                {
                    return ICpNode.NodeType;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (ICpNode != null)
                {
                    if (ICpNode.NodeType != value)
                    {
                        OnPropertyChanging("NodeType");
                        object oldValue = ICpNode.NodeType;
                        ICpNode.NodeType = value;
                        OnPropertyChanged("NodeType", oldValue, value);
                    }
                }
            }
        }
        [Category("节点")]
        public string 父节点
        {
            get
            {
                if (ICpNode != null)
                {
                    return ICpNode.ParentNodeName;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (ICpNode != null)
                {
                    if (ICpNode.ParentNodeName != value)
                    {
                        OnPropertyChanging("ParentNodeName");
                        object oldValue = ICpNode.ParentNodeName;
                        ICpNode.ParentNodeName = value;
                        OnPropertyChanged("ParentNodeName", oldValue, value);
                    }
                }
            }
        }

        [Category("节点")]
        public string 日天
        {
            get
            {
                if (ICpNode != null)
                {
                    return ICpNode.NodeDays;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (ICpNode != null)
                {
                    if (ICpNode.NodeDays != value)
                    {
                        OnPropertyChanging("NodeDays");
                        object oldValue = ICpNode.NodeDays;
                        ICpNode.NodeDays = value;
                        OnPropertyChanged("NodeDays", oldValue, value);
                    }
                }
            }
        }

        #endregion

        #region ICheckBox

        [Category("CheckBox属性")]
        public string 分组名
        {
            get
            {
                if (this.IChkBox != null)
                {
                    return this.IChkBox.GroupName;
                }
                return string.Empty;
            }
            set
            {
                if (this.IChkBox != null)
                {
                    if (this.IChkBox.GroupName != value)
                    {
                        OnPropertyChanging("GroupName");
                        object oldValue = this.IChkBox.GroupName;
                        this.IChkBox.GroupName = value;
                        OnPropertyChanged("GroupName", oldValue, value);
                    }
                }
            }
        }

        [Category("CheckBox属性")]
        public string 合计名
        {
            get
            {
                if (this.IChkBox != null)
                {
                    return this.IChkBox.SumName;
                }
                return string.Empty;
            }
            set
            {
                if (this.IChkBox != null)
                {
                    if (this.IChkBox.SumName != value)
                    {
                        OnPropertyChanging("SumName");
                        object oldValue = this.IChkBox.SumName;
                        this.IChkBox.SumName = value;
                        OnPropertyChanged("SumName", oldValue, value);
                    }
                }
            }
        }

        [Category("CheckBox属性")]
        [TypeConverter(typeof(BoolToStringConverter))]
        [DefaultValue(false)]
        public bool 勾选
        {
            get
            {
                if (this.IChkBox != null)
                {
                    return this.IChkBox.Checked;
                }
                return false;
            }
            set
            {
                if (this.IChkBox != null)
                {
                    if (this.IChkBox.Checked != value)
                    {
                        OnPropertyChanging("Checked");
                        object oldValue = this.IChkBox.Checked;
                        this.IChkBox.Checked = value;
                        OnPropertyChanged("Checked", oldValue, value);
                    }
                }
            }
        }

        [Category("CheckBox属性")]
        public decimal 勾选时计算值
        {
            get
            {
                if (this.IChkBox != null)
                {
                    return this.IChkBox.CheckedWeightValue;
                }
                return 0m;
            }
            set
            {
                if (this.IChkBox != null)
                {
                    if (this.IChkBox.CheckedWeightValue != value)
                    {
                        OnPropertyChanging("CheckedWeightValue");
                        object oldValue = this.IChkBox.CheckedWeightValue;
                        this.IChkBox.CheckedWeightValue = value;
                        OnPropertyChanged("CheckedWeightValue", oldValue, value);
                    }
                }
            }
        }

        #endregion

        #region ICtlLine
        [TypeConverter(typeof(LineStyleEnumConverter))]
        [Category("线条属性")]
        public CtlLineStyle 线条式样
        {
            get
            {
                if (ILine != null)
                {
                    return ILine.LineStyle;
                }
                return CtlLineStyle.Dash;
            }
            set
            {
                if (ILine != null)
                {
                    if (ILine.LineStyle != value)
                    {
                        OnPropertyChanging("LineStyle");
                        object oldValue = this.ILine.LineStyle;
                        this.ILine.LineStyle = value;
                        OnPropertyChanged("LineStyle", oldValue, value);
                    }
                }
            }
        }

        [Category("线条属性")]
        public int 线条粗度
        {
            get
            {
                if (ILine != null)
                {
                    return ILine.LineWidth;
                }
                return 1;
            }
            set
            {
                if (ILine != null)
                {
                    if (ILine.LineWidth != value)
                    {
                        OnPropertyChanging("LineWidth");
                        object oldValue = this.ILine.LineWidth;
                        this.ILine.LineWidth = value;
                        OnPropertyChanged("LineWidth", oldValue, value);
                    }
                }
            }
        }
        #endregion

        #region IPanel
        [Category("Panel属性")]
        public int Panel列
        {
            get
            {
                if (IPnl != null)
                {
                    return IPnl.Columns;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (IPnl != null)
                {
                    if (IPnl.Columns != value)
                    {
                        OnPropertyChanging("Columns");
                        object oldValue = this.IPnl.Columns;
                        this.IPnl.Columns = value;
                        OnPropertyChanged("Columns", oldValue, value);
                    }
                }
            }
        }

        [Category("Panel属性")]
        public int Panel行
        {
            get
            {
                if (IPnl != null)
                {
                    return IPnl.Rows;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (IPnl != null)
                {
                    if (IPnl.Rows != value)
                    {
                        OnPropertyChanging("Rows");
                        object oldValue = this.IPnl.Rows;
                        this.IPnl.Rows = value;
                        OnPropertyChanged("Rows", oldValue, value);
                    }
                }
            }
        }

        [Category("Panel属性")]
        public BorderStyle 边框
        {
            get
            {
                if (IPnl != null)
                {
                    return IPnl.BorderStyle;
                }
                else
                {
                    return BorderStyle.None;
                }
            }
            set
            {
                if (IPnl != null)
                {
                    if (IPnl.BorderStyle != value)
                    {
                        OnPropertyChanging("BorderStyle");
                        object oldValue = this.IPnl.BorderStyle;
                        this.IPnl.BorderStyle = value;
                        OnPropertyChanged("BorderStyle", oldValue, value);
                    }
                }
            }
        }
        #endregion

        #region ICombox

        [Category("ComboBox属性")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public DevExpress.XtraEditors.Controls.ComboBoxItemCollection 下拉项目
        {
            get
            {
                if (ICbx != null)
                {
                    return ICbx.Items;
                }
                else
                {
                    return null;
                }
            }
            set
            { }
        }

        void Items_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            this.OnPropertyChanged("Items", null, null);
        }
        #endregion

        #region ITabControl
        [Category("TabControl属性")]
        [ListBindable(false)]
        [Editor(typeof(XtraTabCollectionEditor), typeof(UITypeEditor))]
        public DevExpress.XtraTab.XtraTabPageCollection 页签
        {
            get
            {
                if (ITabCtrl != null)
                {
                    return ITabCtrl.TabPages;
                }
                else
                {
                    return null;
                }
            }
            set
            {

            }
        }

        void TabPages_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            this.OnPropertyChanged("TabPages", null, null);
        }

        [Category("TabControl属性")]
        public DevExpress.XtraTab.TabHeaderLocation 页签位置
        {
            get
            {
                if (ITabCtrl != null)
                {
                    return ITabCtrl.HeaderLocation;
                }
                return DevExpress.XtraTab.TabHeaderLocation.Top;
            }
            set
            {
                if (ITabCtrl != null)
                {
                    if (ITabCtrl.HeaderLocation != value)
                    {
                        this.OnPropertyChanging("HeaderLocation");

                        object oldValue = ITabCtrl.HeaderLocation;

                        ITabCtrl.HeaderLocation = value;

                        this.OnPropertyChanged("HeaderLocation", oldValue, value);
                    }
                }
            }
        }
        #endregion

        #region ISign
        [Category("签名控件属性"), Description("签名标题")]
        public string 签名标题
        {
            get
            {
                if (ISign != null)
                {
                    return ISign.Caption;
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                if (ISign != null)
                {
                    if (ISign.Caption != value)
                    {
                        OnPropertyChanging("Caption");
                        object oldValue = ISign.Caption;
                        ISign.Caption = value;
                        OnPropertyChanged("Caption", oldValue, value);
                    }
                }
            }
        }

        [TypeConverter(typeof(BoolToStringConverter))]
        [Category("签名控件属性"), Description("是否自动签名 0 否 1 是")]
        [DefaultValue(true)]
        public bool 是否自动签名
        {
            get
            {
                if (ISign != null)
                {
                    return (ISign.IsAutoSignature == 1);
                }
                else
                {
                    return true;
                }
            }

            set
            {
                if (ISign != null)
                {
                    bool b = (ISign.IsAutoSignature == 1);
                    if (b != value)
                    {
                        OnPropertyChanging("IsAutoSignature");
                        object oldValue = b;
                        ISign.IsAutoSignature = (value ? 1 : 0);
                        OnPropertyChanged("IsAutoSignature", oldValue, value);
                    }
                }
            }
        }

        [TypeConverter(typeof(BoolToStringConverter))]
        [Category("签名控件属性"), Description("是否允许空签 0 否 1 是.")]
        [DefaultValue(false)]
        public bool 是否允许空签
        {
            get
            {
                if (ISign != null)
                {
                    return (ISign.IsAllowSignNull == 1);
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (ISign != null)
                {
                    bool b = (ISign.IsAllowSignNull == 1);
                    if (b != value)
                    {
                        OnPropertyChanging("IsAllowSignNull");
                        object oldValue = b;
                        ISign.IsAllowSignNull = (value ? 1 : 0);
                        OnPropertyChanged("IsAllowSignNull", oldValue, value);
                    }
                }
            }
        }

        #endregion

        #region IRtfEditor

        [TypeConverter(typeof(BoolToStringConverter))]
        [Category("RichText属性")]
        [DefaultValue(false)]
        public bool 多行编辑
        {
            get
            {
                if (this.IRtf != null)
                {
                    return this.IRtf.Multiline;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (IRtf != null)
                {
                    if (IRtf.Multiline != value)
                    {
                        OnPropertyChanging("Multiline");
                        object oldValue = this.IRtf.Multiline;
                        this.IRtf.Multiline = value;
                        OnPropertyChanged("Multiline", oldValue, value);
                    }
                }
            }
        }

        //[Category("RichText属性")]
        //[TypeConverter(typeof(BoolToStringConverter))]
        //[DefaultValue(false)]
        //public bool 信息页绑定
        //{
        //    get
        //    {
        //        if (this.IRtf != null)
        //        {
        //            return this.IRtf.BandingPage;
        //        }
        //        return false;
        //    }
        //    set
        //    {
        //        if (this.IRtf != null)
        //        {
        //            if (this.IRtf.BandingPage != value)
        //            {
        //                OnPropertyChanging("BandingPage");
        //                object oldValue = this.IRtf.BandingPage;
        //                this.IRtf.BandingPage = value;
        //                OnPropertyChanged("BandingPage", oldValue, value);
        //            }
        //        }
        //    }
        //}

        [TypeConverter(typeof(BoolToStringConverter))]
        [Category("RichText属性")]
        [DefaultValue(false)]
        public bool 固定高度
        {
            get
            {
                if (this.IRtf != null)
                {
                    return this.IRtf.FixedHeight;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (IRtf != null)
                {
                    if (IRtf.FixedHeight != value)
                    {
                        OnPropertyChanging("FixedHeight");
                        object oldValue = this.IRtf.FixedHeight;
                        this.IRtf.FixedHeight = value;
                        OnPropertyChanged("FixedHeight", oldValue, value);
                    }
                }
            }
        }

        [Category("RichText属性")]
        public int 默认行数
        {
            get
            {
                return (this.IRtf == null ? 0 : this.IRtf.DefaultRows);
            }
            set
            {
                if (this.IRtf != null && this.IRtf.DefaultRows != value)
                {
                    OnPropertyChanging("DefaultRows");
                    object oldValue = this.IRtf.DefaultRows;
                    this.IRtf.DefaultRows = value;
                    OnPropertyChanged("DefaultRows", oldValue, value);
                }
            }
        }

        [Category("RichText属性")]
        public int 行缩进字符个数
        {
            get
            {
                if (this.IRtf != null)
                {
                    return this.IRtf.RowShrinkdigit;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (this.IRtf != null)
                {
                    if (this.IRtf.RowShrinkdigit != value)
                    {
                        OnPropertyChanging("RowShrinkdigit");
                        object oldValue = this.IRtf.RowShrinkdigit;
                        this.IRtf.RowShrinkdigit = value;
                        OnPropertyChanged("RowShrinkdigit", oldValue, value);
                    }
                }
            }
        }

        [Category("RichText属性")]
        public string 首行标题字符
        {
            get
            {
                if (this.IRtf != null)
                {
                    return this.IRtf.FirstlineCaption;
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                if (this.IRtf != null)
                {
                    if (this.IRtf.FirstlineCaption != value)
                    {
                        OnPropertyChanging("FirstlineCaption");
                        object oldValue = this.IRtf.FirstlineCaption;
                        this.IRtf.FirstlineCaption = value;
                        OnPropertyChanged("FirstlineCaption", oldValue, value);
                    }
                }
            }
        }

        #endregion


        #region IXtraDateTime
        [Category("日期时间属性")]
        public DateTime 默认值
        {
            get
            {
                if (this.IDateTime != null && this.IDateTime.DateTimeValue != null)
                {
                    return this.IDateTime.DateTimeValue.Value;
                }
                return DateTime.Parse("0001-1-1 0:00:00");
            }
            set
            {
                if (this.IDateTime != null)
                {
                    this.OnPropertyChanging("DateTimeValue");

                    object oldValue = this.IDateTime.DateTimeValue;
                    if (value < DateTime.Parse("1800-01-01"))
                    {
                        this.IDateTime.DateTimeValue = null;
                    }
                    else
                    {
                        this.IDateTime.DateTimeValue = value;
                    }
                    this.OnPropertyChanged("DateTimeValue", oldValue, this.IDateTime.DateTimeValue);
                }
            }
        }

        [Category("日期时间属性")]
        [TypeConverter(typeof(SpecialDateTimeConverter))]
        public string 其他默认值
        {
            get
            {
                if (this.IDateTime != null && this.IDateTime.SPDefaultValue != null)
                {
                    return this.IDateTime.SPDefaultValue;
                }
                return string.Empty;
            }
            set
            {
                if (this.IDateTime != null)
                {
                    this.OnPropertyChanging("SPDefaultValue");

                    object oldValue = this.IDateTime.SPDefaultValue;

                    this.IDateTime.SPDefaultValue = value;

                    this.OnPropertyChanged("SPDefaultValue", oldValue, this.IDateTime.SPDefaultValue);
                }
            }
        }

        [Category("日期时间属性")]
        //[TypeConverter(typeof(DateTimeMaskConverter))]
        [DefaultValue("yyyy-MM-dd HH:ss:mm")]
        public string 日期时间格式
        {
            get
            {
                if (this.IDateTime != null)
                {
                    return this.IDateTime.EditMask;
                }
                return string.Empty;
            }
            set
            {
                if (this.IDateTime != null)
                {
                    if (this.IDateTime.EditMask != value)
                    {
                        this.OnPropertyChanging("EditMask");
                        object oldValue = this.IDateTime.EditMask;
                        this.IDateTime.EditMask = value;
                        this.OnPropertyChanged("EditMask", oldValue, value);
                    }
                }
            }
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// ICloneable
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}
