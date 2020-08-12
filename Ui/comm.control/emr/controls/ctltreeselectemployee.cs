using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using Common.Entity;

namespace Common.Controls.Emr
{
    public partial class ctlTreeSelectEmployee : ctlTreeSelect
    {
        public ctlTreeSelectEmployee()
        {
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
                this.KeyFieldName = "operCode";
                this.ParentFieldName = "";
                this.DisplayMember = "operName";
                this.ValueMember = "operName";
                PopTreeColumnCollection cols = new PopTreeColumnCollection();
                cols.Add(new PopupTreeColumn("operCode", "工号", 100));
                cols.Add(new PopupTreeColumn("operName", "姓名", 150));
                cols.Add(new PopupTreeColumn("pyCode", "拼音码", 0));
                cols.Add(new PopupTreeColumn("wbCode", "五笔码", 0));
                this.Columns = cols;
                this.SetToGridMode();
                this.DataSource = EntityTools.ConvertToDataTable<EntityCodeOperator>(GlobalDic.DataSourceEmployee);
            }
        }

        /// <summary>
        /// 重载实现过滤效果
        /// </summary>
        protected override void DoFilter()
        {
            DataTable dtNewSource = EntityTools.ConvertToDataTable<EntityCodeOperator>(GlobalDic.DataSourceEmployee);
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
            string filter = this.Text.Trim().Replace("'", "''").Replace("[", "[[]");

            dtNewSource.DefaultView.RowFilter = "superFilterCol like '%" + filter + "%'";
            dtNewSource = dtNewSource.DefaultView.ToTable(); 
            this.DataSource = dtNewSource;

            if (dtNewSource.Rows.Count > 0)
            {
                base.ctlTreeList.FocusedNode = ctlTreeList.FindNodeByFieldValue(base.ValueMember, dtNewSource.Rows[0][base.ValueMember]);
            }
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
                    return base.DBValue;
                }
            }
            set
            {
                base.DBValue = value;
            }
        }
    }
}
