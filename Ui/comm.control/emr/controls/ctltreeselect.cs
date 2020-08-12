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
    public partial class ctlTreeSelect : DevExpress.XtraEditors.PopupContainerEdit, IRuntimeDesignControl, IFormCtrl
    {
        private DevExpress.XtraEditors.PopupContainerControl ctlContainer = new DevExpress.XtraEditors.PopupContainerControl();
        protected DevExpress.XtraTreeList.TreeList ctlTreeList = new DevExpress.XtraTreeList.TreeList();

        public new bool DesignMode
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() == "devenv"; }
        }

        public bool FillValue { get; set; }

        public TreeListNode FocusedNode
        {
            get
            {
                return ctlTreeList.FocusedNode;
            }
            set
            {
                ctlTreeList.FocusedNode = value;
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

        public ctlTreeSelect()
        {
            InitializeComponent();

            this.ctlTreeList.OptionsView.ShowIndicator = false;
            this.ctlTreeList.OptionsBehavior.AllowExpandOnDblClick = false;
            this.ctlTreeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ctlTreeList.OptionsSelection.EnableAppearanceFocusedRow = true;
            this.ctlTreeList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ctlTreeList.Dock = DockStyle.Fill;
            this.ctlTreeList.MouseDoubleClick += new MouseEventHandler(ctlTreeList_MouseDoubleClick);
            this.ctlTreeList.KeyPress += new KeyPressEventHandler(ctlTreeList_KeyPress);
            this.ctlContainer.Controls.Add(ctlTreeList);

            this.Properties.PopupControl = ctlContainer;
            this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.Properties.Appearance.Options.UseFont = true;
            this.Properties.Appearance.Font = new Font("宋体", 9.5f);
            this.EnterMoveNextControl = true;
            this.Properties.LookAndFeel.SkinName = "Black"; //"Sliver";
            this.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.KeyDown += new KeyEventHandler(ctlTreeSelect_KeyDown);
            this.KeyUp += new KeyEventHandler(ctlTreeSelect_KeyUp);
            this.TextChanged += new EventHandler(ctlTreeSelect_TextChanged);
            this.Popup += new EventHandler(ctlTreeSelect_Popup);
        }

        /// <summary>
        /// 树控件KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTreeList_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyValue = (int)e.KeyChar;
            if (keyValue == (int)Keys.Enter)
            {
                e.Handled = true;
                this.GetFocusedNodeValue();
            }
            else if (
                (((int)keyValue >= 65 && (int)keyValue <= 90) //A-Z
                || ((int)keyValue >= 97 && (int)keyValue <= 122)//a-z
                || ((int)keyValue >= 48 && (int)keyValue <= 57)//0-9
                || ((int)keyValue >= 96 && (int)keyValue <= 105)//0-9
                )
                && this.Focused == false
                )
            {
                this.Focus();
                this.Text += e.KeyChar.ToString();
                this.SelectionStart = this.Text.Length;
            }
            else if (keyValue == (int)Keys.Back)
            {
                this.Focus();
                if (this.Text.Length > 1)
                {
                    this.Text = this.Text.Remove(this.Text.Length - 1);
                    this.SelectionStart = this.Text.Length;
                }
            }
        }

        /// <summary>
        /// Popup时设置选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTreeSelect_Popup(object sender, EventArgs e)
        {
            //在设置Value同时设置FocusedNode,可能存在TreeList因为没有显示所以Node数为0的控件问题
            TreeListNode node = ctlTreeList.FindNodeByFieldValue(this.ValueMember, this.value);
            if (ctlTreeList.FocusedNode != node)
            {
                ctlTreeList.FocusedNode = node;
            }
            if (ctlTreeList.FocusedNode == null && ctlTreeList.Nodes.Count > 0)
            {
                ctlTreeList.FocusedNode = ctlTreeList.FindNodeByID(0);
            }

            //value为空时录入第一个字符弹出浮动框时未过滤_不运行Application.DoEvents则this.Text此时不对
            Application.DoEvents();
            if (this.GetType().Name.ToLower() == ICD10_CONTROL)
            {
                if (this.IcdDataSource != null && string.IsNullOrEmpty(this.Text) == false)
                {
                    this.DoFilter();
                }
            }
            else
            {
                if (this.DataSource != null && this.value == null && string.IsNullOrEmpty(this.Text) == false)
                {
                    this.DoFilter();
                }
            }
        }

        private void ctlTreeSelect_TextChanged(object sender, EventArgs e)
        {
            if (this.Text == "")
            {
                this.value = null;
            }
        }

        /// <summary>
        /// 显示为列表样式
        /// </summary>
        public void SetToGridMode()
        {
            this.ctlTreeList.OptionsView.ShowRoot = false;
        }

        /// <summary>
        /// 实现过滤效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTreeSelect_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.GetType().Name.ToLower() == ICD10_CONTROL)
            {
                if (this.IsPopupOpen && this.IcdDataSource != null)
                {
                    this.DoFilter();
                }
            }
            else
            {
                if (this.IsPopupOpen && this.DataSource != null)
                {
                    this.DoFilter();
                }
            }
        }

        private const string ICD10_CONTROL = "ctltreeselect_icd";

        /// <summary>
        /// 实现过滤效果
        /// </summary>
        protected virtual void DoFilter()
        {
            string strFilter = this.Text.Trim().Replace("'", "''").Replace("[", "[[]");

            if (this.GetType().Name.ToLower() == ICD10_CONTROL)
            {
                if (IcdDataSource == null) return;

                if (string.IsNullOrEmpty(strFilter))
                {
                    strFilter = "1=1";
                }
                else
                {
                    strFilter = "icdcode like '%" + strFilter + "%' or icdcnname like '%" + strFilter + "%' or icdpycode like '%" + strFilter + "%' or icdwbcode like '%" + strFilter + "%'";
                }

                DataRow[] drr = IcdDataSource.Select(strFilter);
                if (drr == null)
                {
                    return;
                }

                DataRow dr2 = null;
                IcdDataSourceSort.Clear();
                for (int i = 0; i < drr.Length; i++)
                {
                    dr2 = IcdDataSourceSort.LoadDataRow(drr[i].ItemArray, true);
                    if (drr[i]["icdpycode"].ToString().IndexOf(strFilter) >= 0)
                    {
                        dr2["sort1"] = drr[i]["icdpycode"].ToString().IndexOf(strFilter);
                    }
                    else if (drr[i]["icdcnname"].ToString().IndexOf(strFilter) >= 0)
                    {
                        dr2["sort1"] = drr[i]["icdcnname"].ToString().IndexOf(strFilter);
                    }
                    else
                    {
                        dr2["sort1"] = 99999;
                    }
                    dr2["sort2"] = drr[i]["icdcnname"].ToString().Length;
                }
                DataView dv = new DataView(IcdDataSourceSort);
                dv.Sort = "sort2 asc, sort1 asc";

                DataTable dt = IcdDataSource.Clone();
                int count = 0;
                DataRow dr3 = null;
                foreach (DataRowView item in dv)
                {
                    if (count > 39) break;
                    dr3 = dt.NewRow();
                    foreach (DataColumn dc in IcdDataSourceSort.Columns)
                    {
                        if (dc.ColumnName != "sort1" && dc.ColumnName != "sort2")
                        {
                            dr3[dc.ColumnName] = item.Row[dc.ColumnName];
                        }
                    }
                    dt.Rows.Add(dr3);
                    //dt.LoadDataRow(item.Row.ItemArray, true);
                    count++;
                }
                if (dt.Rows.Count > 0)
                {
                    this.ctlTreeList.BeginUpdate();
                    this.ctlTreeList.DataSource = dt;
                    this.ctlTreeList.EndUpdate();

                    this.ctlTreeList.FocusedNode = ctlTreeList.FindNodeByFieldValue(this.ValueMember, dt.Rows[0][this.ValueMember]);
                }
            }
            else
            {
                DataRow[] drs = this.DataSource.Select("superFilterCol like '%" + strFilter + "%'");
                if (drs.Length > 0)
                {
                    ctlTreeList.FocusedNode = ctlTreeList.FindNodeByFieldValue(this.ValueMember, drs[0][this.ValueMember]);
                }
            }
        }

        /// <summary>
        /// F1可以随意录入
        /// </summary>
        private bool m_blnF1 = false;

        /// <summary>
        /// 编辑控件KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTreeSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.m_blnF1 = !this.m_blnF1;
            }

            if (this.m_blnF1)
            {
                this.ClosePopup();
                e.Handled = false;
                return;
            }

            if (e.KeyCode == Keys.Enter && this.IsPopupOpen)
            {
                e.Handled = true;
                this.GetFocusedNodeValue();
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                if (!this.IsPopupOpen)
                {
                    this.ShowPopupAndSetFocusedNode();
                    ctlTreeList.Focus();
                }
                else
                {
                    ctlTreeList.Focus();

                    if (e.KeyCode == Keys.Up)
                    {
                        ctlTreeList.MovePrev();
                    }

                    if (e.KeyCode == Keys.Down)
                    {
                        ctlTreeList.MoveNext();
                    }
                }
            }
            else if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Escape)
            {
                if (!this.IsPopupOpen)
                {
                    this.ShowPopupAndSetFocusedNode();
                    ctlTreeList.Focus();
                }
            }
        }

        /// <summary>
        /// 显示列表并设置当前选中项
        /// </summary>
        private void ShowPopupAndSetFocusedNode()
        {
            this.ShowPopup();

            //设置当前选中项已改到ctlTreeSelect_Popup时间中
        }


        /// <summary>
        /// 鼠标双击选择数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctlTreeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.GetFocusedNodeValue();
        }

        /// <summary>
        /// 设置Value为当前选中节点值
        /// </summary>
        private void GetFocusedNodeValue()
        {
            if (ctlTreeList.FocusedNode != null)
            {
                DataRowView dr = ctlTreeList.GetDataRecordByNode(ctlTreeList.FocusedNode) as DataRowView;
                this.Text = dr[this.DisplayMember].ToString();
                this.Select(this.Text.Length, 0);
                this.value = dr[this.ValueMember];
                this.ClosePopup();
            }
        }

        /// <summary>
        /// 根据Columns设置ctlTreeList.Columns
        /// </summary>
        public void SetColumns()
        {
            this.ctlTreeList.Columns.Clear();
            List<TreeListColumn> cols = new List<TreeListColumn>();
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].ColumnWidth == 0)
                {
                    continue;
                }

                TreeListColumn col = new TreeListColumn();
                col.Caption = columns[i].HeaderCaption;
                col.FieldName = columns[i].FieldName;
                col.Width = columns[i].ColumnWidth;
                col.Visible = true;
                col.VisibleIndex = i;
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Font = this.Font;
                col.AppearanceCell.Options.UseFont = true;
                col.AppearanceCell.Font = this.Font;
                col.OptionsColumn.AllowEdit = false;
                col.OptionsColumn.AllowMove = false;
                col.OptionsColumn.AllowSort = false;

                if (cols.Count == 0)
                {
                    col.AppearanceCell.Options.UseTextOptions = true;
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                }
                cols.Add(col);
            }
            this.ctlTreeList.Columns.AddRange(cols.ToArray());
        }

        /// <summary>
        /// Columns
        /// </summary>
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PopTreeColumnCollection Columns
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
        protected PopTreeColumnCollection columns = new PopTreeColumnCollection();

        private DataTable IcdDataSource = null;
        private DataTable IcdDataSourceSort = null;

        /// <summary>
        /// DataSource
        /// </summary>
        public DataTable DataSource
        {
            get
            {
                return this.ctlTreeList.DataSource as DataTable;
            }
            set
            {
                this.ctlTreeList.BeginUpdate();
                this.SetColumns();

                //把需要用于过滤的数据列都处理到一个特定文本列,以便使用
                if (value != null)
                {
                    if (this.GetType().Name.ToLower() != ICD10_CONTROL)
                    {
                        if (!value.Columns.Contains("superFilterCol"))
                        {
                            value.BeginLoadData();
                            DataColumn filterCol = new DataColumn("superFilterCol", Type.GetType("System.String"));
                            value.Columns.Add(filterCol);
                            foreach (DataRow dr in value.Rows)
                            {
                                string filterValue = "";
                                foreach (PopupTreeColumn col in this.columns)
                                {
                                    filterValue += dr[col.FieldName].ToString() + "^";
                                }
                                dr[filterCol] = filterValue;
                            }
                            value.EndLoadData();
                        }
                    }
                }

                if (this.GetType().Name.ToLower() == ICD10_CONTROL)
                {
                    this.ParentFieldName = string.Empty;
                    IcdDataSource = value;
                    if (IcdDataSource != null)
                    {
                        IcdDataSourceSort = IcdDataSource.Clone();
                        DataColumn col1 = new DataColumn("sort1", typeof(Int32));
                        IcdDataSourceSort.Columns.Add(col1);
                        DataColumn col2 = new DataColumn("sort2", typeof(Int32));
                        IcdDataSourceSort.Columns.Add(col2);
                    }
                }
                else
                {
                    this.ctlTreeList.DataSource = value;
                }

                this.ctlTreeList.EndUpdate();
            }
        }

        /// <summary>
        /// KeyFieldName
        /// </summary>
        public string KeyFieldName
        {
            get
            {
                return this.ctlTreeList.KeyFieldName;
            }
            set
            {
                this.ctlTreeList.KeyFieldName = value;
            }
        }

        /// <summary>
        /// KeyFieldName
        /// </summary>
        public string ParentFieldName
        {
            get
            {
                return this.ctlTreeList.ParentFieldName;
            }
            set
            {
                this.ctlTreeList.ParentFieldName = value;
            }
        }

        /// <summary>
        /// DisplayMember
        /// </summary>
        public string DisplayMember { get; set; }
        /// <summary>
        /// ValueMember
        /// </summary>
        public string ValueMember { get; set; }

        private object value = null;
        [Bindable(BindableSupport.Yes)]
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                if (this.DataSource is DataTable && value != null)
                {
                    //显示Text
                    string filter = value.ToString().Replace("'", "''").Replace("[", "[[]");
                    DataRow[] drs = this.DataSource.Select(this.ValueMember + "='" + filter + "'");
                    if (drs.Length > 0)
                    {
                        this.Text = drs[0][this.DisplayMember].ToString();
                    }
                    else
                    {
                        if (FillValue)
                        {
                            this.Text = value.ToString();
                            DataRow dr = this.DataSource.NewRow();
                            dr["empno"] = "temp";
                            dr["empname"] = this.Text;
                            this.DataSource.LoadDataRow(dr.ItemArray, true);
                        }
                        else
                        {
                            this.Text = "";
                        }
                    }
                }
                else
                {
                    this.Text = "";
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_presentationMode == 1 /*&& _showunderline*/)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                p.DashPattern = ConstValue.DashPattern;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 1), new Point(this.Width - 2, this.Height - 1));
            }
            else if (_presentationMode == 3)
            {
                Pen p = new Pen(Brushes.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                e.Graphics.DrawLine(p, new Point(0, this.Height - 1), new Point(this.Width - 2, this.Height - 1));
            }
        }

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
        public bool RunTimeReadOnly
        {
            get
            {
                return this.Properties.ReadOnly;
            }
            set
            {
                this.Properties.ReadOnly = value;

                if (this.PresentationMode == 1)
                {
                    this.BackColor = Color.White;
                }
            }
        }

        private int _presentationMode = 0;

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
                        this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                        this.BackColor = this.prevBackColor;
                        this.Properties.AppearanceDisabled.BackColor = this.prevDisableBackColor;
                        this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                    }
                    else if (value == 1 || value == 2 || value == 3)
                    {
                        this.prevBackColor = this.BackColor;
                        this.prevDisableBackColor = this.Properties.AppearanceDisabled.BackColor;

                        this.Properties.AppearanceDisabled.BackColor = Color.White;
                        this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        this.BackColor = Color.White;
                    }
                }
            }
        }

        /// <summary>
        /// 记录改变前的边框式样
        /// </summary>
        Color prevBackColor;
        Color prevDisableBackColor;

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

        bool referencetype = true;
        [Browsable(false)]
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

        public string EssentialGroupNo { get; set; }

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
        
        int masktype = 0;
        public int MaskType
        {
            get { return masktype; }
            set { masktype = value; }
        }
        #endregion

        #region IDBColProperty 成员

        public string DBColName { get; set; }

        public string DBColDesc { get; set; }

        public string DBColType { get; set; }

        public string DBColPrecision { get; set; }

        public string DBValue
        {
            get
            {
                if (this.Value != null && this.Value != DBNull.Value)
                {
                    return this.Value.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.Value = value;
            }
        }

        public int RowShrinkdigit { get; set; }
        public string FirstlineCaption { get; set; }
        //public int IsAutoSignature { get; set; }
        //public int IsAllowSignNull { get; set; }

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

        [Category("PopupSelect属性")]
        [Description("弹出窗体宽度")]
        public Size PopupSize
        {
            get { return this.Properties.PopupFormMinSize; }
            set { this.Properties.PopupFormMinSize = value; }
        }

    }

    #region PopTreeColumnCollection
    [ListBindable(false)]
    public class PopTreeColumnCollection : CollectionBase, IList, ICollection, IEnumerable
    {
        public event CollectionChangeEventHandler CollectionChanged;
        protected void OnCollectionChange(CollectionChangeAction action, object element)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new CollectionChangeEventArgs(action, element));
            }
        }


        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            OnCollectionChange(CollectionChangeAction.Add, value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            OnCollectionChange(CollectionChangeAction.Remove, value);
        }

        public virtual PopupTreeColumn this[int index]
        {
            get
            {
                if (this.List.Count > index - 1)
                {
                    object obj = this.InnerList[index];
                    if (obj != null)
                    {
                        return (PopupTreeColumn)obj;
                    }
                }
                return null;
            }
        }

        public virtual void Add(PopupTreeColumn col)
        {
            this.InnerList.Add(col);
        }

        public virtual void Remove(PopupTreeColumn col)
        {
            foreach (object item in this.InnerList)
            {
                if ((PopupTreeColumn)item == col)
                {
                    this.InnerList.Remove(item);
                    break;
                }
            }
        }
    }

    public class PopupTreeColumn
    {
        private string _fieldname = string.Empty;

        [Description("列名")]
        public virtual string FieldName
        {
            get
            {
                return _fieldname;
            }
            set
            {
                _fieldname = value;
                if (HeaderCaption == null)
                {
                    HeaderCaption = _fieldname;
                }
            }
        }

        [Description("标题")]
        public virtual string HeaderCaption { get; set; }

        [Description("列宽")]
        public virtual int ColumnWidth { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.HeaderCaption, this.FieldName);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        public PopupTreeColumn()
        {
            this.ColumnWidth = 100;
        }

        public PopupTreeColumn(string fieldName, string headerCaption, int columnWidth)
        {
            this._fieldname = fieldName;
            this.HeaderCaption = headerCaption;
            this.ColumnWidth = columnWidth;
        }
    }


    #endregion
}
