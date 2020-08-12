using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    #region 自定义委托
    /// <summary>
    /// 值变化
    /// </summary>
    /// <param name="sender"></param>
    public delegate void _HandleDBValueChanged(object sender);
    /// <summary>
    /// 重置值
    /// </summary>
    /// <param name="sender"></param>
    public delegate void _HandleResetDBValue(object sender);
    #endregion

    /// <summary>
    /// LookUpEditContainer.用于GridControl.Cell下拉编辑
    /// </summary>
    [UserRepositoryItem("RegisterLookUpEdit")]
    public class LookUpEditContainer : RepositoryItemPopupContainerEdit
    {
        #region 构造控件

        internal PopupContainerControl popupContainerControl = null;
        internal GridControl gcData = null;
        internal GridView gvData = null;
        internal BindingSource gvDataBindingSource = null;
        private Panel plBack = null;
        private XtraScrollableControl plDesc = null;
        internal MemoEdit txtDesc = null;
        //private ctlRichTextBox richText = null;

        /// <summary>
        /// Construct
        /// </summary>
        private void Construct()
        {
            this.popupContainerControl = new PopupContainerControl();
            //this.popupContainerControl.BackColor = Color.White;
            this.popupContainerControl.MinimumSize = new Size(10, 10);
            this.popupContainerControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            base.PopupFormMinSize = this.popupContainerControl.MinimumSize;

            this.gcData = new GridControl();
            this.gvData = new GridView();
            this.gvDataBindingSource = new BindingSource();

            // 
            // gcData
            // 
            this.gcData.DataSource = this.gvDataBindingSource;
            this.gcData.BackColor = Color.White;
            this.gcData.EmbeddedNavigator.Name = "";
            this.gcData.MainView = this.gvData;
            this.gcData.Name = "gcData";
            //this.gcData.LookAndFeel.SkinName = "iMaginary";
            this.gcData.LookAndFeel.UseDefaultLookAndFeel = true;//false;
            this.gcData.Size = new Size(0, 0);
            this.gcData.TabIndex = 0;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvData});
            // 
            // gvData
            // 
            this.gvData.GridControl = this.gcData;
            this.gvData.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gvData.ColumnPanelRowHeight = 26;
            this.gvData.Name = "gvData";
            this.gvData.OptionsBehavior.Editable = false;
            this.gvData.OptionsCustomization.AllowRowSizing = true;
            this.gvData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvData.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gvData.OptionsSelection.UseIndicatorForSelection = false;
            this.gvData.OptionsView.RowAutoHeight = true;
            this.gvData.OptionsView.ShowColumnHeaders = false;
            this.gvData.OptionsView.ShowDetailButtons = false;
            this.gvData.OptionsView.ShowGroupPanel = false;
            this.gvData.OptionsView.ShowIndicator = false;
            this.gvData.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll;

            //this.richText = new ctlRichTextBox();
            //this.richText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this.richText.Dock = DockStyle.Top;
            //this.richText.BringToFront();
            //this.richText.ReadOnly = true;

            this.txtDesc = new MemoEdit();
            this.txtDesc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtDesc.Font = new Font("宋体", 9.5f);
            this.txtDesc.Dock = DockStyle.Fill;
            this.txtDesc.BringToFront();
            this.txtDesc.Visible = false;

            //
            // plDesc
            //
            //this.plDesc = new XtraScrollableControl();
            //this.plDesc.Appearance.BackColor = System.Drawing.Color.White;
            //this.plDesc.Appearance.Options.UseBackColor = true;
            //this.plDesc.Size = new System.Drawing.Size(400, 180);
            //this.plDesc.Name = "plDesc";
            //this.plDesc.AutoScroll = true;
            //this.plDesc.Dock = DockStyle.Bottom;
            ////this.plDesc.LookAndFeel.SkinName = "iMaginary";
            //this.plDesc.LookAndFeel.UseDefaultLookAndFeel = true;//false;
            //this.plDesc.Visible = true;
            //this.plDesc.Controls.Add(this.richText);

            //
            // plBack
            //
            this.plBack = new Panel();
            //this.plBack.BackColor = System.Drawing.Color.White;
            this.plBack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.plBack.Name = "plBack";
            this.plBack.Dock = DockStyle.Fill;
            this.plBack.Visible = true;
            this.plBack.Controls.Add(this.gcData);
            //this.plBack.Controls.Add(this.plDesc);
            this.plBack.Controls.Add(this.txtDesc);

            this.popupContainerControl.Controls.Add(this.plBack);
            this.SetPresentationMode(PresentationMode);
            this.SetSize();
            /////
            this.ShowPopupCloseButton = false;
            this.ShowPopupShadow = false;
            this.LookAndFeel.UseDefaultLookAndFeel = true;
            //this.LookAndFeel.SkinName = "Black";
            this.PopupSizeable = false;
            this.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            base.PopupControl = popupContainerControl;
            base.AppearanceDisabled.Options.UseBackColor = true;
            base.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        #region 设置样式
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="mode"></param>
        private void SetPresentationMode(int mode)
        {
            //if (GlobalLoginInfo.LookAndFeelSkinValue != 8) return;
            //if (mode == 0)
            //{
            //    //this.gcData.LookAndFeel.UseDefaultLookAndFeel = true;
            //    //this.gcData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //    this.gcData.LookAndFeel.SkinName = "iMaginary";
            //    this.gcData.LookAndFeel.UseDefaultLookAndFeel = false;

            //    //this.customPopupContainerControl.LookAndFeel.UseDefaultLookAndFeel = true;
            //    //this.customPopupContainerControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

            //    this.popupContainerControl.LookAndFeel.SkinName = "iMaginary";
            //    this.popupContainerControl.LookAndFeel.UseDefaultLookAndFeel = false;
            //}
            //else if (mode == 1 || mode == 2 || mode == 3)
            //{
            //    this.gcData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            //    this.gcData.LookAndFeel.UseDefaultLookAndFeel = false;

            //    this.popupContainerControl.LookAndFeel.UseDefaultLookAndFeel = false;
            //    this.popupContainerControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            //}
        }
        #endregion

        #region 设置尺度
        /// <summary>
        /// 设置尺度
        /// </summary>
        public void SetSize()
        {
            this.popupContainerControl.Width = PopupWidth;
            this.popupContainerControl.Height = PopupHeight;
            base.PopupFormSize = this.popupContainerControl.Size;
        }

        internal void SetPopupControl()
        {
            base.PopupControl = popupContainerControl;
        }
        #endregion

        #endregion

        #region Base

        //静态构造用于登记此方法
        static LookUpEditContainer() { RegisterPopTextEdit(); }

        /// <summary>
        /// 构造
        /// </summary>
        public LookUpEditContainer()
        {
            if (!DesignMode)
            {
                this.Construct();
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        public LookUpEditContainer(int _width, int _height)
        {
            if (!DesignMode)
            {
                PopupWidth = _width;
                PopupHeight = _height;
                this.Construct();
            }
        }

        /// <summary>
        /// 自定义编辑器的唯一名称
        /// </summary>
        public const string PopTextEditName = "LookUpEdit";
        /// <summary>
        /// 返回编辑器的唯一名称
        /// </summary>
        public override string EditorTypeName { get { return PopTextEditName; } }
        /// <summary>
        /// 登记编辑器
        /// </summary>
        public static void RegisterPopTextEdit()
        {
            //设计时代表该容器内的编辑器
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(PopTextEditName, typeof(LookUpEdit), typeof(LookUpEditContainer), typeof(DevExpress.XtraEditors.ViewInfo.PopupContainerEditViewInfo), new ButtonEditPainter(), true));
        }

        public override BaseEdit CreateEditor()
        {
            LookUpEdit edit = (LookUpEdit)base.CreateEditor();
            edit.Properties = this;
            edit.InitEvent();
            return (BaseEdit)edit;
        }
        #endregion

        #region 事件
        public event _HandleDBValueChanged HandleDBValueChanged;
        public void DBValueChanged(object sender)
        {
            if (HandleDBValueChanged != null)
            {
                HandleDBValueChanged(sender);
            }
        }
        /// <summary>
        /// DBValueChanged
        /// </summary>
        public event _HandleResetDBValue HandleResetDBValue;
        public void DBValueReset(object sender)
        {
            if (HandleResetDBValue != null)
            {
                HandleResetDBValue(sender);
            }
        }
        #endregion

        #region 变量.属性
        /// <summary>
        /// 数据源
        /// </summary>
        [Category("自定义属性"), Description("数据源")]
        public BaseDataContract[] DataSource
        {
            get
            {
                if (gvDataBindingSource != null && gvDataBindingSource.DataSource != null)
                {
                    return (gvDataBindingSource.DataSource as BindingListView<BaseDataContract>).LookUpItemSource.ToArray();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    BindingListView<BaseDataContract> data = new BindingListView<BaseDataContract>();
                    data.AddRange(value);
                    if (!string.IsNullOrEmpty(FilterColumn)) data.FilterColumns = FilterColumn.Split('|');
                    if (this.gvDataBindingSource != null)
                    {
                        this.gvDataBindingSource.DataSource = data;
                        this.gcData.RefreshDataSource();
                    }
                }
            }
        }

        /// <summary>
        /// IsCheckValid
        /// </summary>
        private bool _IsCheckValid = true;

        /// <summary>
        /// IsCheckValid
        /// </summary>
        [Browsable(false)]
        public bool IsCheckValid
        {
            get { return _IsCheckValid; }
            set { _IsCheckValid = value; }
        }

        /// <summary>
        /// DescCode
        /// </summary>
        [Browsable(false)]
        public string DescCode { get; set; }

        /// <summary>
        /// 是否自由录入
        /// </summary>
        [Browsable(false)]
        public bool IsFreeInput { get; set; }

        /// <summary>
        /// 弹出宽度
        /// </summary>
        [Category("自定义属性"), Description("弹出宽度")]
        public int PopupWidth { get; set; }

        /// <summary>
        /// 弹出高度
        /// </summary>]
        [Category("自定义属性"), Description("弹出高度")]
        public int PopupHeight { get; set; }

        [Category("自定义属性"), Description("是否显示列标题 True 显示; False 不显示")]
        public bool IsShowColumnHeaders { get; set; }

        /// <summary>
        /// 展现方式
        /// </summary>
        [Category("自定义属性"), Description("展现方式 0 普通; 1 虚线; 3 实线; 4 边框")]
        public int PresentationMode { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Category("自定义属性"), Description("是否必填")]
        public bool Essential { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Parent.DBValue
        /// </summary>
        public string DBValue { get; set; }
        /// <summary>
        /// Parent.DBRow
        /// </summary>
        public BaseDataContract DBRow { get; set; }

        /// <summary>
        /// Parent.DisplayValue
        /// </summary>
        public string DisplayValue { get; set; }
        /// <summary>
        /// Parent.IsDescField
        /// </summary>
        public bool IsDescField { get; set; }

        public bool ForbidPoput { get; set; }

        /// <summary>
        /// 显示列
        /// </summary>
        [Category("自定义属性"), Description("显示列")]
        public string DisplayColumn { get; set; }

        /// <summary>
        /// 数据列
        /// </summary>
        [Category("自定义属性"), Description("数据列")]
        public string ValueColumn { get; set; }

        /// <summary>
        /// 是否使用显示列
        /// </summary>
        [Category("自定义属性"), Description("是否使用显示列")]
        public bool IsUseShowColumn { get; set; }
        /// <summary>
        /// 显示列,多个列以竖线'|'隔开
        /// </summary>
        [Category("自定义属性"), Description("表格显示列,多个列以竖线'|'隔开")]
        public string ShowColumn { get; set; }

        /// <summary>
        /// 隐藏列,多个列以竖线'|'隔开
        /// </summary>
        [Category("自定义属性"), Description("表格隐藏列,多个列以竖线'|'隔开")]
        public string HideColumn { get; set; }
        /// <summary>
        /// 查找列,多个列以竖线'|'隔开
        /// </summary>
        [Category("自定义属性"), Description("过滤列,多个列以竖线'|'隔开")]
        public string FilterColumn { get; set; }

        [Category("自定义属性"), Description("是否获取焦点即弹出")]
        public bool IsAutoPopup { get; set; }

        [Category("自定义属性"), Description("是否隐藏数据列")]
        public bool IsHideValueColumn { get; set; }

        [Category("自定义属性"), Description("是否选择项目自动跳到下一个控件")]
        public bool IsSelectedMoveNextControl { get; set; }

        /// <summary>
        /// 指定列宽
        /// </summary>
        public Dictionary<string, int> ColumnWidth = new Dictionary<string, int>();

        /// <summary>
        /// 指定列名
        /// </summary>
        public Dictionary<string, string> ColumnHeaders = new Dictionary<string, string>();

        /// <summary>
        /// 分组信息
        /// </summary>
        public Dictionary<string, int> ColumnGroup = new Dictionary<string, int>();

        /// <summary>
        /// 居中显示文本列
        /// </summary>
        public List<string> ColumnCenterText = new List<string>();

        /// <summary>
        /// 是否显示描述信息
        /// </summary>
        [Category("自定义属性"), Description("是否显示项目描述信息")]
        public bool IsShowDescInfo { get; set; }

        /// <summary>
        /// 是否显示描述信息
        /// </summary>
        [Category("自定义属性"), Description("是否显示行号")]
        public bool IsShowRowNo { get; set; }

        private bool _IsTab = true;
        /// <summary>
        /// 是否自动跳转
        /// </summary>
        [Category("自定义属性"), Description("是否自动跳转")]
        [Browsable(false)]
        public bool IsTab
        {
            get { return _IsTab; }
            set { _IsTab = value; }
        }

        [Browsable(false)]
        public GridView ParentGridView
        { get; set; }

        [Browsable(false)]
        public DevExpress.XtraGrid.Views.BandedGrid.BandedGridView ParentBandedGridView
        { get; set; }

        [Browsable(false)]
        public BindingSource ParentBindingSource
        { get; set; }

        #endregion

    }

    /// <summary>
    /// LookUpEdit.用于组合+独立使用
    /// </summary>
    public class LookUpEdit : PopupContainerEdit
    {
        #region Base

        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public LookUpEdit()
        {
            if (!DesignMode)
            {
                InitEvent();
            }
        }
        #endregion

        static LookUpEdit() { LookUpEditContainer.RegisterPopTextEdit(); }

        public override string EditorTypeName
        {
            get { return LookUpEditContainer.PopTextEditName; }
        }

        private LookUpEditContainer _Properties;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        public new LookUpEditContainer Properties
        {
            get { return _Properties == null ? base.Properties as LookUpEditContainer : _Properties; }
            set { _Properties = value; }
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 是否初始化
        /// </summary>
        private bool IsInit = true;

        /// <summary>
        /// 单元格值变化
        /// </summary>
        private bool _CellValueChanged = false;
        /// <summary>
        /// 单元格值变化
        /// </summary>
        [Browsable(false)]
        [Category("自定义属性"), Description("单元格值变化")]
        public bool CellValueChanged
        {
            get { return _CellValueChanged; }
            set { _CellValueChanged = value; }
        }

        [Browsable(true)]
        [Category("自定义属性"), Description("是否使用默认LookAndFeel")]
        public bool IsUseDefaultLookAnkFeel
        {
            set { this.Properties.LookAndFeel.UseDefaultLookAndFeel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string cellDBValue = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private string cellDispValue = string.Empty;
        /// <summary>
        /// ValueChanged
        /// </summary>
        private bool ValueChanged { get; set; }

        /// <summary>
        /// FocusRowChanged
        /// </summary>
        private bool FocusRowChanged { get; set; }
        /// <summary>
        /// TextChanged
        /// </summary>
        private new bool TextChanged { get; set; }
        /// <summary>
        /// 属性.字段
        /// </summary>
        private List<string> FieldName = null;

        /// <summary>
        /// 是否存在分组
        /// </summary>
        private bool IsExistsGroup { get; set; }

        [Browsable(false)]
        public GridView ParentGridView
        { get; set; }

        [Browsable(false)]
        public DevExpress.XtraGrid.Views.BandedGrid.BandedGridView ParentBandedGridView
        { get; set; }

        [Browsable(false)]
        public BindingSource ParentBindingSource
        { get; set; }

        [Browsable(true)]
        [Category("自定义属性"), Description("AccessibleName")]
        public new string AccessibleName
        {
            get
            {
                return base.Properties.AccessibleName;
            }
            set
            {
                base.Properties.AccessibleName = value;
            }
        }

        BaseDataContract CurrentDataContract { get; set; }

        #endregion

        #region Override

        private void MoveNextControl()
        {
            if (Properties.IsTab)
            {
                SendKeys.SendWait("{TAB}");
            }
            else
            {
                Properties.IsTab = true;
            }
        }

        private void SetTextWidth()
        {
            if (Properties.PopupControl.Width < this.Width)
            {
                Properties.PopupControl.Width = this.Width;
            }
        }

        private new void ShowPopup()
        {
            if (IsInit || Properties.ForbidPoput) return;
            base.ShowPopup();
            this.Focus();
        }

        protected override void OnPopupShown()
        {
            if (Properties.ForbidPoput) return;
            if (isAutoRowHeight)
            {
                base.OnPopupShown();
                FocusRowChanged = false;
                this.Focus();
            }
            else
            {
                try
                {
                    SuspendLayout();
                    SetRowAutoHeight();
                }
                finally
                {
                    ResumeLayout();
                }
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            CheckDBValue();
            base.OnLeave(e);
            if (Properties.IsFreeInput)
            {
                CellValueChanged = true;
                Properties.DBValue = this.Text;
                Properties.DBValueChanged(this);
            }
            else if (IsF1)
            {
                CellValueChanged = true;
                //this.Text = Properties.txtDesc.Text;   // 通过单元格直接录入，暂时屏蔽编辑框录入。
                Properties.DBValue = Properties.DescCode;
                //Properties.DisplayValue = Properties.txtDesc.Text;    // 通过单元格直接录入，暂时屏蔽编辑框录入。
                Properties.DBValueChanged(this);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            AutoPopup();
        }

        protected override void OnPopupClosed(PopupCloseMode closeMode)
        {
            InitPopup = false;
            base.OnPopupClosed(closeMode);
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            char chr = e.KeyChar;

            if ((chr >= (char)48 && chr <= (char)57) || (chr >= (char)65 && chr <= (char)90) || (chr >= (char)97 && chr <= (char)122))
            {
                TextChanged = true;
            }
            else if (chr == (char)32) // Space
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    ShowPopup();
                    e.Handled = true;
                }
            }
            else if (chr == (char)8) //Backspace
            {
            }
            else
            {
                ClosePopup();
            }
            base.OnKeyPress(e);
        }

        private bool isTrigger { get; set; }
        public bool IsButtonFind { get; set; }

        public void TriggerKeyEnter()
        {
            // if (e.KeyCode == Keys.Enter)
            if (Properties.IsFreeInput)
            {
                CellValueChanged = true;
                Properties.DisplayValue = this.Text;
                Properties.DBValue = this.Text;
                Properties.DBValueChanged(this);
                SendKeys.SendWait("{TAB}");
                return;
            }
            else if (IsF1)
            {
                CellValueChanged = true;
                Properties.DisplayValue = this.Text;
                Properties.DBValue = Properties.DescCode;
                Properties.DBValueChanged(this);
                SendKeys.SendWait("{TAB}");
                return;
            }
            if (!TextChanged && !string.IsNullOrEmpty(Properties.DBValue) && !FocusRowChanged)
            {
                CheckDBValue();
            }
            else
            {
                try
                {
                    isTrigger = true;
                    SelectedRow(false);
                }
                finally
                {
                    isTrigger = false;
                }
            }
        }

        private bool IsKeyDown { get; set; }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            IsInit = false;
            IsKeyDown = true;
            if (e.KeyCode == Keys.Enter)
            {
                if (Properties.IsFreeInput)
                {
                    CellValueChanged = true;
                    Properties.DisplayValue = this.Text;
                    Properties.DBValue = this.Text;
                    Properties.DBValueChanged(this);
                    SendKeys.SendWait("{TAB}");
                    return;
                }
                else if (IsF1)
                {
                    CellValueChanged = true;
                    Properties.DisplayValue = this.Text;
                    Properties.DBValue = Properties.DescCode;
                    Properties.DBValueChanged(this);
                    SendKeys.SendWait("{TAB}");
                    return;
                }
                if (!TextChanged && !string.IsNullOrEmpty(Properties.DBValue) && !FocusRowChanged)
                {
                    CheckDBValue();
                }
                else if (this.IsPopupOpen)
                {
                    SelectedRow(false);
                }
                else if (!this.IsPopupOpen)
                {
                    CheckDBValue();
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    if (this.IsPopupOpen)
                    {
                    }
                    else
                    {
                        ShowPopup();
                    }
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                /* 采用gvDataBindingSource滚动当使用rowgroup时，顺序会乱。 */
                if (Properties.gvData.FocusedRowHandle == Properties.gvDataBindingSource.Count - 1)
                {
                    //Properties.gvDataBindingSource.MoveFirst(); 
                    Properties.gvData.MoveFirst();
                }
                else
                {
                    //Properties.gvDataBindingSource.MoveNext();
                    Properties.gvData.MoveNext();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (Properties.gvData.FocusedRowHandle == 0)
                {
                    //Properties.gvDataBindingSource.MoveLast();
                    Properties.gvData.MoveLast();
                }
                else
                {
                    //Properties.gvDataBindingSource.MovePrevious();
                    Properties.gvData.MovePrev();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //Properties.gvDataBindingSource.MoveFirst();
                Properties.gvData.MoveFirst();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                //Properties.gvDataBindingSource.MoveLast();
                Properties.gvData.MoveLast();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (this.SelectionStart == this.Text.Length)
                {
                    if (!CheckDBValue())
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (!CheckDBValue())
                {
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (this.IsPopupOpen)
                {
                    ClosePopup();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                SetDescEdit();
                return;
            }
            base.OnKeyDown(e);
        }

        private bool IsF1 { get; set; }
        private void SetDescEdit()
        {
            IsF1 = !IsF1;
            Properties.txtDesc.Visible = IsF1;
            Properties.gcData.Visible = !IsF1;
        }

        public void SetDescEdit(string descVal, bool isF1)
        {
            IsF1 = isF1;
            Properties.txtDesc.Visible = IsF1;
            Properties.txtDesc.Text = descVal;
            Properties.gcData.Visible = !IsF1;
        }

        private bool isOnEnter { get; set; }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            SetTextWidth();
            if (this.IsExistsGroup)
            {
                ExpandRows();
            }
            isOnEnter = true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Properties.ForbidPoput || IsLeave || IsF1) return;
            if (!ValueChanged)
            {
                if (IsInit)
                {
                    AutoPopup();
                }
                if (isOnEnter)
                {
                    if (Properties.DisplayValue == this.Text)
                    {
                        Filter(this.Text.Trim());
                        return;
                    }
                    isOnEnter = false;
                }

                if (this.Text.Trim() == string.Empty)
                {
                    CellValueChanged = true;
                    Properties.DBValue = string.Empty;
                    Properties.DBRow = null;
                    Properties.DisplayValue = string.Empty;
                    this.Text = string.Empty;
                    EditValue = string.Empty;

                    //Properties.DBValueReset(this);
                }

                if (TextChanged == false && !string.IsNullOrEmpty(Properties.DBValue))
                {
                    Filter(Properties.DBValue);
                }
                else
                {
                    Filter(this.Text.Trim());
                }

                if (IsKeyDown)
                {
                    ShowPopup();
                }
            }
            TextChanged = true;
            IsKeyDown = false;
            base.OnTextChanged(e);
        }

        bool InitPopup = false;
        private void AutoPopup()
        {
            if (Properties.ForbidPoput) return;
            if (Properties.IsAutoPopup && !InitPopup)
            {
                InitPopup = true;
                ShowPopup();
            }
        }

        bool IsLeave = false;
        private bool CheckDBValue()
        {
            if (IsF1 || Properties.IsFreeInput || IsButtonFind) return true;
            if (!Properties.IsCheckValid)
                return true;
            try
            {
                IsLeave = true;
                if (string.IsNullOrEmpty(Properties.DBValue))
                {
                    Properties.DisplayValue = string.Empty;
                    this.Text = string.Empty;

                    Filter(this.Text.Trim());

                    if (Properties.Essential)
                    {
                        return false;
                    }
                }
                else
                {
                    if (this.Text != Properties.DisplayValue)
                    {
                        this.Text = Properties.DisplayValue;
                        this.SelectionStart = this.Text.Length;
                    }
                }
            }
            finally
            {
                IsLeave = false;
            }
            return true;
        }
        #endregion

        #region 事件
        public event _HandleDBValueChanged HandleDBValueChanged;
        public void DBValueChanged(object sender)
        {
            if (HandleDBValueChanged != null)
            {
                HandleDBValueChanged(sender);
            }
        }

        /// <summary>
        /// DBValueChanged
        /// </summary>
        public event _HandleResetDBValue HandleResetDBValue;
        public void DBValueReset(object sender)
        {
            if (HandleResetDBValue != null)
            {
                HandleResetDBValue(sender);
            }
        }

        private void gvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectedRow(false);
            }
        }

        private void gvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusRowChanged = true;
        }

        private void gvData_DoubleClick(object sender, EventArgs e)
        {
            SelectedRow(true);
        }

        private void gvData_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            int pos = GridGroupRowInfo.GroupText.LastIndexOf(":");
            if (pos >= 0)
            {
                GridGroupRowInfo.GroupText = GridGroupRowInfo.GroupText.Substring(pos + 1);
            }
        }
        #endregion

        #region 方法

        #region InitEvent
        /// <summary>
        /// InitEvent
        /// </summary>
        internal void InitEvent()
        {
            if (Properties != null)
            {
                Properties.gvData.KeyDown -= new KeyEventHandler(gvData_KeyDown);
                Properties.gvData.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvData_FocusedRowChanged);
                Properties.gvData.DoubleClick -= new EventHandler(gvData_DoubleClick);
                Properties.gvData.CustomDrawGroupRow -= new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(gvData_CustomDrawGroupRow);
                Properties.gvData.KeyDown += new KeyEventHandler(gvData_KeyDown);
                Properties.gvData.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvData_FocusedRowChanged);
                Properties.gvData.DoubleClick += new EventHandler(gvData_DoubleClick);
                Properties.gvData.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(gvData_CustomDrawGroupRow);
            }
        }
        #endregion

        #region 设置分组
        /// <summary>
        /// 设置分组
        /// </summary>
        private void SetColumnGroup()
        {
            if (DesignMode) return;
            if (Properties.ColumnGroup != null && Properties.ColumnGroup.Count > 0 && Properties.DataSource != null &&
                Properties.gvData.Columns.Count > 0)
            {
                string[] keys = Properties.ColumnGroup.Keys.ToArray();
                foreach (string key in keys)
                {
                    Properties.gvData.Columns[key].GroupIndex = Properties.ColumnGroup[key];
                    Properties.gvData.Columns[key].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    this.IsExistsGroup = true;
                }
                ExpandRows();
            }
        }

        private void ExpandRows()
        {
            for (int i = 1; i < Properties.DataSource.Length; i++)
            {
                Properties.gvData.SetRowExpanded(i * -1, true);
            }
        }
        #endregion

        #region 设置列可视
        /// <summary>
        /// 设置列可视
        /// </summary>
        private void SetColumnVisible()
        {
            List<string> lstCol = new List<string>();
            if (FieldName == null)
            {
                FieldName = new List<string>();
                for (int i = 0; i < Properties.gvData.Columns.Count; i++)
                {
                    FieldName.Add(Properties.gvData.Columns[i].FieldName);
                }
            }
            if (Properties.IsUseShowColumn && !string.IsNullOrEmpty(Properties.ShowColumn))
            {
                lstCol.AddRange(Properties.ShowColumn.Split('|'));
                foreach (string fieldName in FieldName)
                {
                    if (lstCol.IndexOf(fieldName) < 0)
                    {
                        Properties.gvData.Columns[fieldName].Visible = false;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Properties.HideColumn))
                {
                    lstCol.AddRange(Properties.HideColumn.Split('|'));
                    foreach (string col in lstCol)
                    {
                        if (FieldName.IndexOf(col) >= 0)
                        {
                            Properties.gvData.Columns[col].Visible = false;
                        }
                    }
                }
            }
        }

        #endregion

        #region 设置列宽
        /// <summary>
        /// 设置列宽
        /// </summary>
        private void SetColumnWidth()
        {
            if (Properties.DataSource != null && Properties.gvData.DataSource != null && Properties.gvData.Columns.Count > 0)
            {
                if (Properties.IsHideValueColumn)
                {
                    Properties.gvData.Columns[Properties.ValueColumn].Visible = false;
                }

                try
                {
                    int width = 0;
                    foreach (string item in Properties.ColumnWidth.Keys)
                    {
                        Properties.gvData.Columns[item].Width = Properties.ColumnWidth[item];
                        width += Properties.gvData.Columns[item].Width;
                    }

                    if (width < Properties.gcData.Width)
                    {
                        if (Properties.gvData.Columns.Count == Properties.ColumnWidth.Keys.Count)
                        {
                            Properties.gvData.Columns[Properties.gvData.Columns.Count - 1].Width += Properties.gcData.Width - width - 4;
                        }
                        else
                        {
                            width = 0;
                            for (int i = 0; i < Properties.gvData.Columns.Count; i++)
                            {
                                if (Properties.gvData.Columns[i].Visible)
                                {
                                    width += Properties.gvData.Columns[i].Width;
                                }
                            }
                            Properties.gvData.Columns[Properties.gvData.Columns.Count - 1].Width += Properties.gcData.Width - width - 4;
                        }
                    }
                }
                catch (Exception e)
                {
                    DialogBox.Msg(e.Message);
                }
            }
        }
        #endregion

        #region 设置行自动高
        /// <summary>
        /// 是否完成设置行高
        /// </summary>
        private bool isAutoRowHeight = false;
        /// <summary>
        /// 设置行自动高
        /// </summary>
        private void SetRowAutoHeight()
        {
            if (Properties.gvData.OptionsView.RowAutoHeight && !isAutoRowHeight)
            {
                try
                {
                    this.SuspendLayout();
                    if (Properties == null || Properties.gvData.DataSource == null || Properties.gvData.Columns.Count == 0) return;
                    isAutoRowHeight = true;

                    uiHelper.SetViewAppearance(Properties.gvData);
                    SetColumnGroup();
                    SetColumnVisible();
                    SetColumnWidth();

                    for (int i = 0; i < FieldName.Count; i++)
                    {
                        DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                        Properties.gvData.Columns[i].ColumnEdit = memo;
                        Properties.gvData.Columns[i].AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Properties.gvData.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                        if (Properties.ColumnCenterText.IndexOf(Properties.gvData.Columns[i].FieldName) >= 0)
                        {
                            Properties.gvData.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            if (this.IsExistsGroup)
                            {
                                Properties.gvData.Columns[i].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }
                        }
                    }

                    if (Properties.gvDataBindingSource.DataSource != null)
                    {
                        Properties.gvData.OptionsView.ShowColumnHeaders = Properties.IsShowColumnHeaders;
                        if (Properties.IsShowColumnHeaders && Properties.ColumnHeaders.Keys.Count > 0)
                        {
                            for (int i = 0; i < FieldName.Count; i++)
                            {
                                if (Properties.ColumnHeaders.ContainsKey(Properties.gvData.Columns[i].FieldName))
                                {
                                    Properties.gvData.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    Properties.gvData.Columns[i].Caption = Properties.ColumnHeaders[Properties.gvData.Columns[i].FieldName];
                                    Properties.gvData.Columns[i].OptionsFilter.AllowAutoFilter = false;
                                    Properties.gvData.Columns[i].OptionsFilter.AllowFilter = false;
                                    Properties.gvData.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                                }
                            }
                        }
                    }

                    Properties.gvData.ClearSelection();
                }
                catch (Exception e)
                {
                    DialogBox.Msg(e.Message);
                }
                finally
                {
                    this.IsInit = false;
                    Properties.gcData.Dock = DockStyle.Fill;
                    this.ResumeLayout();
                }
            }
        }
        #endregion

        #region 过滤
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="val"></param>
        public void Filter(string val)
        {
            if (Properties.gvDataBindingSource != null && Properties.gvDataBindingSource.DataSource != null)
            {
                if (Properties != null && !string.IsNullOrEmpty(Properties.FilterColumn))
                {
                    (Properties.gvDataBindingSource.DataSource as BindingListView<BaseDataContract>).Filter = val;
                    SetColumnGroup();
                }
                else  // 查找模式
                {
                    for (int i = 0; i < Properties.gvData.RowCount; i++)
                    {
                        foreach (string fieldName in FieldName)
                        {
                            try
                            {
                                if (Properties.gvData.GetRowCellValue(i, fieldName).ToString().ToLower().StartsWith(val.ToLower()))
                                {
                                    Properties.gvData.FocusedRowHandle = i;
                                    CurrentDataContract = CurrentData();
                                    return;
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        #endregion

        #region 当前行
        /// <summary>
        /// 当前行
        /// </summary>
        /// <returns></returns>
        private BaseDataContract CurrentData()
        {
            if (Properties.gvDataBindingSource.Current == null) return null;
            return Properties.gvDataBindingSource.Current as BaseDataContract;
        }
        #endregion

        #region 选择行
        /// <summary>
        /// 选择行
        /// </summary>
        /// <param name="isTab"></param>
        private void SelectedRow(bool isTab)
        {
            BaseDataContract dr = (isTrigger ? CurrentDataContract : CurrentData());
            if (dr == null) return;
            if (string.IsNullOrEmpty(Properties.ValueColumn))
            {
                DialogBox.Msg("请指定数据列-ValueColumn.", MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(Properties.DisplayColumn))
            {
                DialogBox.Msg("请指定显示列-DisplayColumn.", MessageBoxIcon.Exclamation);
                return;
            }
            ValueChanged = true;
            CellValueChanged = true;
            Properties.DBValue = EntityTools.GetFieldValue(dr, Properties.ValueColumn).ToString();
            Properties.DBRow = dr;
            Properties.DisplayValue = EntityTools.GetFieldValue(dr, Properties.DisplayColumn).ToString();
            this.Text = Properties.DisplayValue;
            Properties.DBValueChanged(this);
            this.DBValueChanged(this);
            this.SelectionStart = this.Text.Length;
            ClosePopup();
            TextChanged = false;
            ValueChanged = false;

            if (isTab)
            {
                MoveNextControl();
            }
            if (this.IsExistsGroup)
            {
                ExpandRows();
            }
            this.Focus();
        }
        #endregion

        #region SetDisplayText 通过设置DisplayText 来设置DBValue
        /// <summary>
        /// SetDisplayText 通过设置DisplayText 来设置DBValue
        /// </summary>
        /// <param name="displayText"></param>
        public void SetDisplayText(string displayText)
        {
            this.Properties.ForbidPoput = true;
            if (this.Properties.DataSource != null)
            {
                try
                {
                    for (int i = 0; i < Properties.gvData.RowCount; i++)
                    {
                        if (this.Properties.gvData.GetRowCellDisplayText(i, this.Properties.DisplayColumn) == displayText)
                        {
                            this.Properties.DBValue = this.Properties.gvData.GetRowCellValue(i, this.Properties.ValueColumn).ToString();
                            break;
                        }
                    }
                }
                catch { }
            }
            this.Text = displayText;
            this.Properties.DisplayValue = displayText;
            this.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetDisplayText 通过设置DisplayText 来设置DBValue -- 更方便
        /// <summary>
        /// SetDisplayText 通过设置DisplayText 来设置DBValue, 用于初始化该控件.赋值.简化了很多语句
        /// </summary>
        /// <param name="displayText"></param>
        public void SetDisplayText<T>(string displayText) where T : BaseDataContract
        {
            this.Properties.ForbidPoput = true;
            if (this.Properties.DataSource != null)
            {
                try
                {
                    List<BaseDataContract> data = (Properties.gvDataBindingSource.DataSource as BindingListView<BaseDataContract>).LookUpItemSource.ToList();
                    List<T> lstT = new List<T>();
                    foreach (var item in data)
                    {
                        lstT.Add(item as T);
                    }
                    DataTable dt = EntityTools.ConvertToDataTable<T>(lstT);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][this.Properties.DisplayColumn].ToString() == displayText)
                            {
                                this.Properties.DBRow = this.Properties.DataSource[i];
                                this.Properties.DBValue = dt.Rows[i][this.Properties.ValueColumn].ToString();
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                { }
            }
            this.Text = displayText;
            this.Properties.DisplayValue = displayText;
            this.Properties.ForbidPoput = false;
        }
        #endregion

        #endregion
    }
}