using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iCare.Core.Entity;
using Common.Entity;

namespace Common.Controls.Emr
{
    /// <summary>
    /// 通用字典选择控件
    /// </summary>
    public partial class ctlTreeSelect_Common : ctlTreeSelect
    {
        /// <summary>
        /// 通用字典分类ID
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("通用字典分类ID")]
        public int intClassID { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable m_dtSource = null;

        public ctlTreeSelect_Common()
        {
            InitializeComponent();

            LoadData();
        }

        public ctlTreeSelect_Common(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            LoadData();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            LoadData();
        }

        /// <summary>
        /// 原因: 自定义单只在InitializeComponent后调用有效,非自定义单只在OnLoaded后调用后有效,所以2个地方都调
        /// </summary>
        private void LoadData()
        {
            //字典信息处理
            if (!DesignMode && this.DataSource == null)
            {
                this.Properties.PopupFormMinSize = new Size(250, 300);
                this.KeyFieldName = "itemcode";
                this.ParentFieldName = "";
                this.DisplayMember = "itemname";
                this.ValueMember = "itemname";
                PopTreeColumnCollection cols = new PopTreeColumnCollection();
                cols.Add(new PopupTreeColumn("itemcode", "编号", 0));
                cols.Add(new PopupTreeColumn("itemname", "名称", 200));
                cols.Add(new PopupTreeColumn("pycode", "拼音码", 0));
                cols.Add(new PopupTreeColumn("wbcode", "五笔码", 0));
                this.Columns = cols;
                this.SetToGridMode();

                this.DataSource = this.m_dtGetDataSource();
            }
        }

        /// <summary>
        /// 重载实现过滤效果
        /// </summary>
        protected override void DoFilter()
        {
            DataTable dtNewSource = this.m_dtGetDataSource();
            if (dtNewSource != null)
            {
                if (!dtNewSource.Columns.Contains("superFilterCol"))
                {
                    dtNewSource.BeginLoadData();
                    DataColumn filterCol = new DataColumn("superFilterCol", Type.GetType("System.String"));
                    dtNewSource.Columns.Add(filterCol);
                    foreach (DataRow dr in dtNewSource.Rows)
                    {
                        string filterValue = "";
                        foreach (PopupTreeColumn col in base.columns)
                        {
                            filterValue += dr[col.FieldName].ToString() + "^";
                        }
                        dr[filterCol] = filterValue;
                    }
                    dtNewSource.EndLoadData();
                }
                string filter = this.Text.Trim().Replace("'", "''").Replace("[", "[[]");

                dtNewSource.DefaultView.RowFilter = "superFilterCol like '%" + filter + "%'";
                dtNewSource = dtNewSource.DefaultView.ToTable();
                this.DataSource = dtNewSource;

                if (dtNewSource.Rows.Count > 0)
                {
                    base.ctlTreeList.FocusedNode = ctlTreeList.FindNodeByFieldValue(ctlTreeList.KeyFieldName, dtNewSource.Rows[0][ctlTreeList.KeyFieldName]);
                }
            }
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private DataTable m_dtGetDataSource()
        {
            DataTable dtResult = null;
            if (this.m_dtSource == null && intClassID != 0)
            {
                System.Collections.Hashtable hsFilter = new System.Collections.Hashtable();
                hsFilter["Classid"] = intClassID;
                hsFilter["Status"] = 1;
                clsProxyEntityFactory objFacProxy = new clsProxyEntityFactory();
                m_dtSource = objFacProxy.m_GetTable(string.Empty, new clsEntityFactory(new clsEntityCommon()), hsFilter, true);


                m_dtSource = EntityTools.ConvertToDataTable<EntityIcd>(GlobalDic.DataSourceICD);
            }

            if (m_dtSource != null)
            {
                dtResult = m_dtSource.Copy();
            }
            return dtResult;
        }

        /// <summary>
        /// 是否自由编辑
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("是否自由编辑")]
        public bool blnFreeEdit { get; set; }

        /// <summary>
        /// 改写属性
        /// </summary>
        public new string DBValue
        {
            get
            {
                if (blnFreeEdit)
                {
                    return this.Text;
                }
                else
                {
                    if (string.IsNullOrEmpty(base.DBValue))
                    {
                        return this.Text;
                    }
                    else
                    {
                        return base.DBValue;
                    }
                }
            }
            set
            {
                if (blnFreeEdit)
                {
                    base.DBValue = value;
                }
                else
                {
                    DataTable dtSource = this.DataSource;
                    DataRow[] drSelectArr = dtSource.Select("itemname = '" + value + "'");
                    if (drSelectArr.Length > 0)
                    {
                        base.DBValue = value;
                    }
                    else
                    {
                        base.DBValue = null;
                        this.Text = value;
                    }
                }
            }
        }

        private void ctlTreeSelect_Common_Leave(object sender, EventArgs e)
        {
            if (!blnFreeEdit)
            {
                if (string.IsNullOrEmpty(base.DBValue))
                {
                    this.Text = string.Empty;
                    DoFilter();
                }
                else if (this.ValueMember == this.DisplayMember)
                {
                    if (this.Text != base.DBValue)
                    {
                        this.Text = base.DBValue;
                    }
                }
            }
        }
    }
}
