using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlSelectTree : UserControl
    {
        public bool CanCheck
        {
            get;
            set;
        }

        public ctlSelectTree()
        {
            InitializeComponent();
            lstCheck.OptionsView.ShowCheckBoxes = true;
            lstCheck.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(lstCheck_BeforeCheckNode);
        }

        void lstCheck_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (!CanCheck) e.CanCheck = false;
        }
        /// <summary>
        /// 过滤行的格式
        /// </summary>
        public string FilterFormat
        {
            get;
            set;
        }
        /// <summary>
        /// 设置功能树形结构绑定参数
        /// </summary>
        /// <param name="p_strKeyFieldName"></param>
        /// <param name="p_strParentFieldName"></param>
        /// <param name="p_dtDataSource"></param>
        public void SetCtl(string p_strKeyFieldName, string p_strParentFieldName, DataTable p_dtDataSource)
        {
            lstCheck.KeyFieldName = p_strKeyFieldName;
            lstCheck.ParentFieldName = p_strParentFieldName;
            lstCheck.DataSource = p_dtDataSource;
        }
        ///<summary>
        /// 设置树形显示的列
        /// </summary>
        public void SetGrid(string[] visibleFields, string[] visibleCaption, int[] visibleWidth)
        {
            uiHelper.SetGridCol(lstCheck, visibleFields, visibleCaption, visibleWidth, true);
        }
        /// <summary>
        /// 关联表并显示
        /// </summary>
        ///<param name="p_dtDef">定义关联的表</param>
        ///<param name="p_strKeyName">对应的关键字段</param>
        public void JoinDataSource(DataTable p_dtDef, string p_strKeyName)
        {
            for (int i = 0; i < lstCheck.AllNodesCount; i++)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode tn = lstCheck.FindNodeByID(i);
                tn.Checked = false;
                foreach (DataRow r in p_dtDef.Rows)
                {
                    if ((p_dtDef.Columns[p_strKeyName].DataType == typeof(String) && Convert.ToString(r[p_strKeyName]).Trim() == Convert.ToString(tn.GetValue(p_strKeyName)).Trim())
                    || (p_dtDef.Columns[p_strKeyName].DataType != typeof(String) && weCare.Core.Utils.Function.Dec(r[p_strKeyName]) == weCare.Core.Utils.Function.Dec(tn.GetValue(p_strKeyName))))
                    {
                        tn.Checked = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 关联表并显示
        /// </summary>
        ///<param name="p_dtDef">定义关联的表</param>
        ///<param name="p_strKeyName">对应的关键字段</param>
        ///<param name="p_strFilter">关联表的过滤字段</param>
        /// <param name="p_intFilterValue">关联表过滤的值</param>
        public void JoinDataSource(DataTable p_dtDef, string p_strKeyName, string p_strFilter, object p_objFilterValue)
        {
            string sf = p_dtDef.Columns[p_strFilter].DataType == typeof(String) ? "{0}='{1}'" : "{0}={1}";
            DataRow[] dr = p_dtDef.Select(string.Format(sf, p_strFilter, p_objFilterValue));
            DataTable dt;
            if (dr != null && dr.Count() > 0)
                dt = dr.CopyToDataTable();
            else
                dt = p_dtDef.Clone();
            JoinDataSource(dt, p_strKeyName);
        }
        /// <summary>
        /// 取选择变化后的对应表
        /// </summary>
        /// <param name="p_dtDef">关联表</param>
        /// <param name="p_strFilter">关联表的过滤字段</param>
        /// <param name="p_intFilterValue">关联表过滤的值</param>
        /// <param name="p_strKeyName">对应的关键字段</param>
        public void GetChange(ref DataTable p_dtDef, string p_strFilter, object p_objFilterValue, string p_strKeyName)
        {

            DataView dv = new DataView(p_dtDef);
            string sf = p_dtDef.Columns[p_strFilter].DataType == typeof(String) ? "{0}='{1}'" : "{0}={1}";
            dv.RowFilter = string.Format(sf, p_strFilter, p_objFilterValue);

            for (int i = 0; i < lstCheck.AllNodesCount; i++)
            {
                bool add = true;
                DevExpress.XtraTreeList.Nodes.TreeListNode tn = lstCheck.FindNodeByID(i);
                foreach (DataRowView r in dv)
                {
                    if ((p_dtDef.Columns[p_strKeyName].DataType == typeof(String) && Convert.ToString(r[p_strKeyName]).Trim() == Convert.ToString(tn.GetValue(p_strKeyName)).Trim())
                    || (p_dtDef.Columns[p_strKeyName].DataType != typeof(String) && weCare.Core.Utils.Function.Dec(r[p_strKeyName]) == weCare.Core.Utils.Function.Dec(tn.GetValue(p_strKeyName))))
                    {
                        add = false;
                        if (!tn.Checked) r.Delete();
                        break;
                    }
                }
                if (add && tn.Checked)
                {
                    DataRow r = p_dtDef.NewRow();
                    if (p_dtDef.Columns[p_strFilter].DataType == typeof(String))
                        r[p_strFilter] = Convert.ToString(p_objFilterValue);
                    else
                        r[p_strFilter] = weCare.Core.Utils.Function.Dec(p_objFilterValue);

                    if (p_dtDef.Columns[p_strKeyName].DataType == typeof(String))
                        r[p_strKeyName] = Convert.ToString(tn.GetValue(p_strKeyName));
                    else
                        r[p_strKeyName] = weCare.Core.Utils.Function.Dec(tn.GetValue(p_strKeyName));
                    p_dtDef.Rows.Add(r);
                }
            }
        }

        /// <summary>
        /// 查找起始项
        /// </summary>
        private int intStartIndex = 0;
        /// <summary>
        /// 标志
        /// </summary>
        private bool blnFlags = false;

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtFilter.Text))
            {
                uiHelper.TreeListFind(lstCheck, txtFilter.Text.Trim(), FilterFormat, ref blnFlags, ref intStartIndex);
                //将焦点置回搜索输入框
                Application.DoEvents();
                txtFilter.Select(txtFilter.Text.Length, 0);
                txtFilter.Focus();
            }
        }
    }
}
