using DevExpress.XtraGrid.Columns;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    [ToolboxBitmap(typeof(ComboBox))]
    public partial class ctlPopupSelect : UserControl, IRuntimeDesignControl, IFormCtrl, IPopupSelect
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public ctlPopupSelect()
        {
            InitializeComponent();

            this.AutoScaleMode = AutoScaleMode.None;

            //this.Columns = new PopSelectColumnCollection();
            this.gridControlPop.DoubleClick += new EventHandler(gridControlPop_DoubleClick);

            this.innerEditor.Popup += new EventHandler(innerEditor_Popup);
            this.innerEditor.KeyDown += new KeyEventHandler(innerEditor_KeyDown);
            this.innerEditor.KeyPress += new KeyPressEventHandler(innerEditor_KeyPress);
            this.innerEditor.PreviewKeyDown += new PreviewKeyDownEventHandler(innerEditor_PreviewKeyDown);
            this.innerEditor.TextChanged += new EventHandler(innerEditor_TextChanged);

            this.innerContainer.PreviewKeyDown += new PreviewKeyDownEventHandler(innerContainer_PreviewKeyDown);

            this.gridControlPop.KeyDown += new KeyEventHandler(gridControlPop_KeyDown);

            this.gridViewPop.OptionsBehavior.AutoExpandAllGroups = true;
            //this.gridViewPop.Appearance.GroupPanel.Options.UseFont = true;
            //this.gridViewPop.Appearance.GroupPanel.Font = new Font("宋体", 10.5f);
            this.gridViewPop.Appearance.GroupRow.Options.UseFont = true;
            this.gridViewPop.Appearance.GroupRow.Font = new Font("宋体", 9f);

            this.gridViewPop.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.innerEditor.Height = this.Height;
            this.innerEditor.Font = new Font("宋体", 9f);

            this.prevBackColor = this.innerEditor.Properties.AppearanceDisabled.BackColor;
        }



        DataRow currentRow = null;

        public DataRow CurrentRow
        {
            get { return currentRow; }
        }

        /// <summary>
        /// 内部编辑控件KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void innerEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)//按下空格弹出选择框
            {
                if (this.innerEditor.Properties.PopupControl.Visible == true)
                {
                }
                else
                {
                    e.Handled = true;
                    //innerEditor_TextChanged(null, null);
                    this.innerEditor.ShowPopup();
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                if (this.innerEditor.SelectionLength == this.innerEditor.Text.Length)
                {
                    this.Text = "";
                }
                this.innerEditor.ClosePopup();
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.innerEditor.Properties.PopupControl.Visible)
                {
                    this.SetSelectedValue();
                }
            }
        }

        bool bEnablePop = true;
        /// <summary>
        /// 内部编辑控件文本值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void innerEditor_TextChanged(object sender, EventArgs e)
        {
            if (bEnablePop)
            {
                if (this.innerEditor.Properties.PopupControl.Visible == false)
                {
                    popBySys = true;
                    this.innerEditor.ShowPopup();
                    popBySys = false;
                    innerEditor_Popup(this.innerEditor, null);
                    this.innerEditor.Focus();
                }
                else
                {
                    innerEditor_Popup(this.innerEditor, null);
                }
            }

            if (this.Text == string.Empty)
            {
                object prevValue = this._value;
                this._value = null;
                this.OnAfterValueChanged(this._value, prevValue);
            }
        }

        /// <summary>
        /// 获取过滤字串
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        private string GetFilterString(string cond)
        {
            cond = cond.Trim().Replace("'", "''").Replace("[", "[[]").ToUpper();
            StringBuilder sb = new StringBuilder();
            if (this.bindingSource1.DataSource != null && this.bindingSource1.DataSource is DataTable)
            {
                DataTable data = (DataTable)this.bindingSource1.DataSource;
                sb.Append("1<>1");
                foreach (PopupSelectColumn col in this.Columns)
                {
                    //if (col.ColumnWidth != 0)
                    //{
                    sb.Append(string.Format(" or {0} like '%{1}%' ", col.FieldName, cond));
                    //}
                }
            }
            return sb.ToString();
        }

        void innerContainer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Debug.WriteLine("innerContainer_PreviewKeyDown:" + e.KeyCode.ToString());
        }

        void innerEditor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Debug.WriteLine("innerEditor_PreviewKeyDown:" + e.KeyCode.ToString());

        }

        /// <summary>
        /// 网格KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridControlPop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSelectedValue();
            }
            else if (
                (((int)e.KeyValue >= 65 && (int)e.KeyValue <= 90) //a-z
                || ((int)e.KeyValue >= 97 && (int)e.KeyValue <= 122)//A-Z
                || ((int)e.KeyValue >= 48 && (int)e.KeyValue <= 57)//0-9
                || ((int)e.KeyValue >= 96 && (int)e.KeyValue <= 105)//0-9
                )
                && this.innerEditor.Focused == false
                )
            {
                this.innerEditor.Focus();
                this.innerEditor.Text += (char)e.KeyData;
                this.innerEditor.SelectionStart = this.innerEditor.Text.Length;
            }
            else if (e.KeyData == Keys.Back)
            {
                this.innerEditor.Focus();
                //if (this.Text.Length > 1)
                //{
                //    this.innerEditor.Text = this.innerEditor.Text.Remove(this.innerEditor.Text.Length - 2);
                //    this.innerEditor.SelectionStart = this.innerEditor.Text.Length;
                //}
            }
        }

        /// <summary>
        /// 编辑文本框KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void innerEditor_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("innerEditor_KeyDown:" + e.KeyCode.ToString());

            bool blnExternal = true;
            if (e.KeyCode == Keys.Enter && enterMoveNextControl)
            {
                //回车跳转
                if (this.Parent != null && this.innerEditor.IsPopupOpen == false)
                {
                    SendKeys.Send("{Tab}");
                    blnExternal = false;
                }
            }
            else if (e.KeyCode == (Keys.Up))
            {
                if (this.innerEditor.Properties.PopupControl.Visible)
                {
                    this.gridViewPop.Focus();
                    this.gridViewPop.MovePrev();
                    blnExternal = false;
                }
            }
            else if (e.KeyCode == (Keys.Down))
            {
                if (this.innerEditor.Properties.PopupControl.Visible)
                {
                    this.gridViewPop.Focus();
                    this.gridViewPop.MoveNext();
                    blnExternal = false;
                }
            }
            //else if (e.KeyCode == (Keys.Escape))
            //{
            //    if (!this.innerEditor.Properties.PopupControl.Visible)
            //    {
            //        bEnablePop = false;
            //        this.Value = null;
            //        this.Text = string.Empty;

            //        e.Handled = true;
            //        bEnablePop = true;
            //    }
            //}

            if (blnExternal && ExternalKeyDown != null)
                ExternalKeyDown(sender, e);
        }


        /// <summary>
        /// 当前的过滤条件
        /// </summary>
        private string _filter = string.Empty;

        bool popBySys = false;
        /// <summary>
        /// 弹出下拉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void innerEditor_Popup(object sender, EventArgs e)
        {
            if (popBySys)
            {
                return;
            }

            _filter = "";

            this.OnBeforeFilter(ref _filter);

            //内部过滤非SQL
            string strFilter = "1=1";

            if (e == null)
            {
                //键盘输入后弹出
                strFilter = GetFilterString(this.Text);
            }

            if (string.IsNullOrEmpty(_filter))
            {
                _filter = strFilter;
            }
            else
            {
                _filter += "and (" + strFilter + ")";
            }

            try
            {
                this.bindingSource1.Filter = _filter;

                if (e != null && this._value != null)
                {
                    for (int i = 0; i < bindingSource1.Count; i++)
                    {
                        if (((DataRowView)bindingSource1[i])[ValueMember].ToString() == _value.ToString())
                        {
                            bindingSource1.Position = i;
                            break;
                        }
                    }
                }
            }
            catch //过滤条件有误时不抛出异常
            {

            }
        }

        /// <summary>
        /// 获取当前选中的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public object GetCurrentSelectEntity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 双击网格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridControlPop_DoubleClick(object sender, EventArgs e)
        {
            SetSelectedValue();
        }

        private void SetSelectedValue()
        {
            DataRow dr = this.gridViewPop.GetFocusedDataRow();
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(this.DisplayMember))
                {
                    string text = string.Empty;
                    if (dr.Table.Columns.Contains(this.DisplayMember)
                        && dr[this.DisplayMember] != null
                        && dr[this.DisplayMember] != DBNull.Value
                        )
                    {
                        text = dr[this.DisplayMember].ToString();
                    }
                    this.Text = text;
                }

                if (!string.IsNullOrEmpty(this.ValueMember))
                {
                    object value = null;
                    if (dr.Table.Columns.Contains(this.ValueMember))
                    {
                        value = dr[this.ValueMember];
                    }
                    this.Value = value;
                }

                this.innerEditor.ClosePopup();
                this.innerEditor.SelectionStart = this.Text.Length;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void ctlPopupSelect_Load(object sender, EventArgs e)
        {
            InitColumns();
        }

        public DataTable DataSource
        {
            get
            {
                return this.bindingSource1.DataSource as DataTable;
            }
            set
            {
                //InitColumns();
                //this.bindingSource1.DataSource = value;
                //this.bindingSource1.ResetBindings(true);

                this.bindingSource1.DataSource = value;
            }
        }

        bool loaded = false;
        public void InitColumns(bool reload)
        {
            if (reload)
            {
                loaded = false;
            }
            InitColumns();
        }
        public void InitColumns()
        {

            if (!DesignMode && loaded == false)
            {
                //生成列
                this.gridViewPop.Columns.Clear();
                int count = 1;
                foreach (PopupSelectColumn item in this.Columns)
                {
                    if (item.ColumnWidth != 0)
                    {
                        GridColumn gridCol = new GridColumn();
                        string caption = item.FieldName;
                        if (!string.IsNullOrEmpty(item.HeaderCaption))
                        {
                            caption = item.HeaderCaption;
                        }
                        gridCol.Caption = caption;
                        gridCol.FieldName = item.FieldName;
                        gridCol.Width = item.ColumnWidth;

                        gridCol.AppearanceHeader.Options.UseFont = true;
                        gridCol.AppearanceHeader.Font = new Font("宋体", 9.5f);
                        gridCol.AppearanceCell.Options.UseFont = true;
                        gridCol.AppearanceCell.Font = new Font("宋体", 9.5f);
                        gridCol.Visible = true;
                        gridCol.VisibleIndex = count;
                        gridCol.OptionsColumn.AllowEdit = false;
                        gridCol.OptionsColumn.AllowFocus = false;
                        gridCol.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
                        gridCol.OptionsColumn.AllowMove = false;
                        gridCol.OptionsColumn.AllowSize = true;
                        this.gridViewPop.Columns.Add(gridCol);
                        count++;
                    }
                }

                //绑定数据
                if (!string.IsNullOrEmpty(this.SourceClass))
                {
                    DataTable data = null;// CacheContext.Get(this.SourceClass);

                    DataView dv = new DataView(data);
                    dv.RowFilter = this.InitFilter;
                    DataTable dt = dv.ToTable();

                    //把数据列都转为文本
                    for (int i = dt.Columns.Count - 1; i >= 0; i--)
                    {
                        DataColumn col = dt.Columns[i];
                        string colName = col.ColumnName;
                        if (col.DataType != Type.GetType("System.String"))
                        {
                            DataColumn newCol = new DataColumn("tempCol", Type.GetType("System.String"));
                            dt.Columns.Add(newCol);
                            foreach (DataRow row in dt.Rows)
                            {
                                row[newCol] = row[col].ToString();
                            }
                            dt.Columns.Remove(col);
                            newCol.ColumnName = colName;
                        }
                    }

                    this.bindingSource1.DataSource = dt;
                }

                //设置默认过滤条件
                try
                {
                    //this.bindingSource1.Filter = this.InitFilter;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    this.bindingSource1.Filter = string.Empty;
                }
            }
            loaded = true;
        }

        //设置分组
        public void SetGroupIndex(string fieldName, int index, bool expand)
        {
            this.gridViewPop.Columns[fieldName].GroupIndex = index;
            if (expand)
            {
                this.gridViewPop.ExpandAllGroups();
            }
        }

        #region 是否在Value值改变时自动设置文本
        bool setTextWhenValueChanged = false;

        public bool SetTextWhenValueChanged
        {
            get { return setTextWhenValueChanged; }
            set { setTextWhenValueChanged = value; }
        }
        #endregion

        #region 是否在文本值改变时自动设置Value
        bool setValueWhenTextChanged = false;

        public bool SetValueWhenTextChanged
        {
            get { return setValueWhenTextChanged; }
            set { setValueWhenTextChanged = value; }
        }
        #endregion

        #region 回车时是否跳转到下一控件
        bool enterMoveNextControl = true;

        public bool EnterMoveNextControl
        {
            get { return enterMoveNextControl; }
            set { enterMoveNextControl = value; }
        }
        #endregion

        #region Text

        /// <summary>
        /// 文本值
        /// </summary>
        [Bindable(BindableSupport.Yes)]
        public new string Text
        {
            get
            {
                return this.innerEditor.Text;
            }
            set
            {
                bEnablePop = false;
                this.innerEditor.Text = value;
                bEnablePop = true;
            }
        }
        #endregion

        #region Value
        private object _value = null;
        /// <summary>
        /// 编辑值
        /// </summary>
        [Bindable(BindableSupport.Yes)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (this._value != value)
                {
                    bEnablePop = false;

                    object prevValue = this._value;

                    this.OnBeforeValueChange(this._value, value);
                    this._value = value;

                    //设置Value时自动改变文本
                    bEnablePop = false;
                    if (setTextWhenValueChanged && bindingSource1.DataSource != null && value != null && value.ToString() != "")
                    {
                        DataTable data = (DataTable)this.bindingSource1.DataSource;
                        DataRow[] drs = data.Select(ValueMember + "='" + value.ToString() + "'");
                        if (drs.Length > 0)
                        {
                            this.innerEditor.Text = drs[0][DisplayMember].ToString();
                            currentRow = drs[0];
                        }
                        else
                        {
                            this.innerEditor.Text = "";
                            currentRow = null;
                        }
                    }
                    else
                    {
                        this.innerEditor.Text = "";
                        currentRow = null;
                    }
                    bEnablePop = true;

                    this.OnAfterValueChanged(this._value, prevValue);

                    bEnablePop = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 编辑方式
        /// </summary>
        public DevExpress.XtraEditors.Controls.TextEditStyles TextEditStyle
        {
            get
            {
                return this.innerEditor.Properties.TextEditStyle;
            }
            set
            {
                this.innerEditor.Properties.TextEditStyle = value;
            }
        }

        #region 控件最大高度
        const int MaxHeight = 22;
        public override Size MaximumSize
        {
            get
            {
                return new Size(base.MaximumSize.Width, MaxHeight);
            }
            set
            {
                base.MaximumSize = new Size(value.Width, MaxHeight);
            }
        }


        public override Size MinimumSize
        {
            get
            {
                return new Size(base.MinimumSize.Width, MaxHeight);
            }
            set
            {
                base.MinimumSize = new Size(value.Width, MaxHeight);
            }
        }
        #endregion

        #region IPopupSelect 成员

        [Category("PopupSelect属性")]
        [Description("弹出窗体宽度")]
        public int PopupWidth
        {
            get
            {
                return this.innerContainer.Width;
            }
            set
            {
                this.innerContainer.Width = value;
            }
        }

        [Category("PopupSelect属性")]
        [Description("弹出窗体高度")]
        public int PopupHeight
        {
            get
            {
                return this.innerContainer.Height;
            }
            set
            {
                this.innerContainer.Height = value;
            }
        }

        [Category("PopupSelect属性")]
        [Description("显示字段")]
        public string DisplayMember { get; set; }

        [Category("PopupSelect属性")]
        [Description("数据字段")]
        public string ValueMember { get; set; }


        [Category("PopupSelect属性")]
        [Description("数据源类名")]
        //[TypeConverter(typeof(EntityNameConverter))]
        public string SourceClass { get; set; }


        private string popupselectsource = null;

        [Browsable(false)]
        public string popupSelectSource
        {
            get
            {
                return popupselectsource;
            }
            set
            {
                popupselectsource = value;
                if (!string.IsNullOrEmpty(value) && value.Trim() != string.Empty && popupselectsource.Split(';').Count() > 1)
                {
                    this.SourceClass = popupselectsource.Split(';')[1];
                }
                else
                {
                    this.SourceClass = string.Empty;
                }
            }
        }

        [Category("PopupSelect属性")]
        [Description("sql过滤条件")]
        public string InitFilter { get; set; }

        [Category("PopupSelect属性")]
        [Description("列集合")]
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PopSelectColumnCollection Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        private PopSelectColumnCollection columns = new PopSelectColumnCollection();

        [Category("PopupSelect属性")]
        [Description("只选")]
        public bool SelectOnly
        {
            get
            {
                //if (SelectOnly == true)
                //{
                //    this.innerEditor.Properties.ReadOnly = true;
                //}
                //else
                //{

                //}
                return this.innerEditor.Properties.ReadOnly;
            }
            set
            {
                this.innerEditor.Properties.ReadOnly = value;
            }
        }

        #endregion

        #region IRuntimeDesignControl 成员

        public object EditObject
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value;
            }
        }

        [Browsable(false)]
        public bool RunTimeReadOnly
        {
            get
            {
                return this.innerEditor.Properties.ReadOnly;
            }
            set
            {
                this.innerEditor.Properties.ReadOnly = value;

                if (this.PresentationMode == 1)
                {
                    this.innerEditor.BackColor = Color.White;
                }
            }
        }
        
        public string EssentialGroupNo { get; set; }



        int masktype = 0;
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
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

        /// <summary>
        /// 记录改变前的边框式样
        /// </summary>
        BorderStyle prevBorderStyle;
        Color prevBackColor;
        Color prevDisableBackColor;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (PresentationMode == 1 /*&& _showunderline*/)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                p.DashPattern = ConstValue.DashPattern;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 1), new Point(this.Width - 2, this.Height - 1));
            }
            else if (PresentationMode == 3)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 1), new Point(this.Width - 2, this.Height - 1));
            }
        }

        public Font TextFont
        {
            get
            {
                return this.innerEditor.Font;
            }
            set
            {
                this.innerEditor.Font = value;
            }
        }

        public decimal ZIndex { get; set; }

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

        /// <summary>
        /// 引用类型
        /// </summary>
        [Browsable(false)]
        public bool IsReferenceType { get; set; }

        /// <summary>
        /// (是否)必须
        /// </summary>
        [Browsable(false)]
        public bool Essential { get; set; }
                
        [Browsable(false)]
        public bool Referencetype { get; set; }

        /// <summary>
        /// 展现方式
        /// </summary>
        int _PresentationMode = 0;
        /// <summary>
        /// 展现方式
        /// </summary>
        [Browsable(false)]
        public int PresentationMode
        {
            get { return _PresentationMode; }
            set
            {
                _PresentationMode = value;
                if (value == 0)
                {
                    this.BorderStyle = this.prevBorderStyle;
                    this.BackColor = this.prevBackColor;
                    this.innerEditor.Properties.AppearanceDisabled.BackColor = this.prevDisableBackColor;
                    this.innerEditor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                }
                else if (value == 1 || value == 2 || value == 3)
                {
                    this.prevBorderStyle = this.BorderStyle;
                    this.prevBackColor = this.BackColor;
                    this.prevDisableBackColor = this.innerEditor.Properties.AppearanceDisabled.BackColor;

                    this.innerEditor.Properties.AppearanceDisabled.BackColor = Color.White;
                    this.innerEditor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                    this.BorderStyle = BorderStyle.None;
                    this.BackColor = Color.White;
                }
            }
        }

        #endregion

        #region Event

        #region ValueChange
        #region AfterValueChanged

        public delegate void AfterValueChangedEventArgs(object sender, ValueChangedEventAgrs args);
        public event AfterValueChangedEventArgs AfterValueChanged;
        protected virtual void OnAfterValueChanged(object newVal, object prevValue)
        {
            if (AfterValueChanged != null)
            {
                AfterValueChanged(this, new ValueChangedEventAgrs(newVal, prevValue));
            }
        }
        #endregion

        #region BeforeValueChange
        public delegate void BeforeValueChangeEventArgs(object sender, ValueChangingEventArgs args);
        public event BeforeValueChangeEventArgs BeforeValueChange;
        protected virtual void OnBeforeValueChange(object currValue, object newVal)
        {
            if (BeforeValueChange != null)
            {
                BeforeValueChange(this, new ValueChangingEventArgs(currValue, newVal));
            }
        }

        #endregion

        public class ValueChangedEventAgrs : EventArgs
        {
            public ValueChangedEventAgrs(object newVal, object prevValue)
            {
                this.NewValue = NewValue;
                this.PrevValue = prevValue;
            }

            public object NewValue { get; set; }
            public object PrevValue { get; set; }
        }

        public class ValueChangingEventArgs : EventArgs
        {
            public ValueChangingEventArgs(object currValue, object newVal)
            {
                this.NewValue = NewValue;
                this.CurrentValue = currValue;
                this.Cancel = false;
            }

            public object CurrentValue { get; set; }
            public object NewValue { get; set; }
            public bool Cancel { get; set; }
        }
        #endregion

        #region BeforeFilter

        public delegate void BeforeFilterEventHandler(object sender, ref string strFilterString);
        public event BeforeFilterEventHandler BeforeFilter;

        public void OnBeforeFilter(ref string strFilterString)
        {
            if (BeforeFilter != null)
            {
                BeforeFilter(this, ref strFilterString);
            }
        }

        #endregion

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

        public event HandleExternalKeyDown ExternalKeyDown;
    }

    public delegate void HandleExternalKeyDown(object sender, KeyEventArgs e);
}
