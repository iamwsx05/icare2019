using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlSelectGrid : UserControl
    {
        public ctlSelectGrid()
        {
            InitializeComponent();
            gvRight.OptionsSelection.MultiSelect = true;
            gvLeft.OptionsSelection.MultiSelect = true;
            gvRight.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvLeft.OptionsSelection.EnableAppearanceFocusedCell = false;
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
        /// 取选择变化后的对应表
        /// </summary>
        /// <param name="p_dtDef">关联表</param>
        /// <param name="p_strFilter">关联表的过滤字段</param>
        /// <param name="p_intFilterValue">关联表过滤的值</param>
        /// <param name="p_strKeyName">对应的关键字段</param>
        public void GetChange(ref DataTable p_dtDef, string p_strFilter, object p_objFilterValue, string p_strKeyName)
        {
            if (p_dtDef.Columns[p_strKeyName].DataType == typeof(String))
            {
                GetChange2(ref  p_dtDef, p_strFilter, p_objFilterValue, p_strKeyName);
                return;
            }
            DataTable dt = (gdLeft.DataSource as DataView).Table;
            dt.AcceptChanges();
            string sf = p_dtDef.Columns[p_strFilter].DataType == typeof(String) ? "{0}='{1}'" : "{0}={1}";

            DataRow[] drs = p_dtDef.Select(string.Format(sf, p_strFilter, p_objFilterValue));
            var q1 = from def in drs
                     select def.Field<decimal>(p_strKeyName);

            var q2 = from s in dt.AsEnumerable()
                     select s.Field<decimal>(p_strKeyName);

            //新增的
            var add = q2.Except(q1);
            foreach (decimal d in add)
            {
                DataRow r = p_dtDef.NewRow();
                if (p_dtDef.Columns[p_strFilter].DataType == typeof(String))
                    r[p_strFilter] = Convert.ToString(p_objFilterValue);
                else
                    r[p_strFilter] =  weCare.Core.Utils.Function.Dec(p_objFilterValue);
                r[p_strKeyName] = d;
                p_dtDef.Rows.Add(r);
            }

            //删除的
            var dele = q1.Except(q2);
            DataView dv = p_dtDef.DefaultView;
            dv.RowFilter = string.Format(sf, p_strFilter, p_objFilterValue);
            dv.Sort = p_strKeyName;
            foreach (decimal d in dele)
            {
                int r = dv.Find(d);
                if (r != -1) dv.Delete(r);
            }
        }
        //对于关键字是字符型的处理
        private void GetChange2(ref DataTable p_dtDef, string p_strFilter, object p_objFilterValue, string p_strKeyName)
        {
            DataTable dt = (gdLeft.DataSource as DataView).Table;
            dt.AcceptChanges();
            string sf = p_dtDef.Columns[p_strFilter].DataType == typeof(String) ? "{0}='{1}'" : "{0}={1}";

            DataRow[] drs = p_dtDef.Select(string.Format(sf, p_strFilter, p_objFilterValue));
            var q1 = from def in drs
                     select def.Field<string>(p_strKeyName);

            var q2 = from s in dt.AsEnumerable()
                     select s.Field<string>(p_strKeyName);

            //新增的
            var add = q2.Except(q1);
            foreach (string d in add)
            {
                DataRow r = p_dtDef.NewRow();
                if (p_dtDef.Columns[p_strFilter].DataType == typeof(String))
                    r[p_strFilter] = Convert.ToString(p_objFilterValue);
                else
                    r[p_strFilter] = weCare.Core.Utils.Function.Dec(p_objFilterValue);
                r[p_strKeyName] = d;
                p_dtDef.Rows.Add(r);
            }

            //删除的
            var dele = q1.Except(q2);
            DataView dv = p_dtDef.DefaultView;
            dv.RowFilter = string.Format(sf, p_strFilter, p_objFilterValue);
            dv.Sort = p_strKeyName;
            foreach (string d in dele)
            {
                int r = dv.Find(d);
                if (r != -1) dv.Delete(r);
            }
        }
        /// <summary>
        /// 关联的表
        /// </summary>
        private DataTable _dtDef;
        /// <summary>
        /// 用于关联的主键
        /// </summary>
        private string _strKeyName;

        /// <summary>
        /// 左侧标题
        /// </summary>
        public string TitleLeft
        {
            set { gpLeft.Text = value; }
        }
        /// <summary>
        /// 右侧标题
        /// </summary>
        public string TitleRight
        {
            set { gpRight.Text = value; }
        }
        /// <summary>
        /// 关联表并显示
        /// </summary>
        ///<param name="p_dtAll">显示所有的表</param>
        ///<param name="p_dtDef">定义关联的表</param>
        ///<param name="p_strKeyName">对应的关键字段</param>
        public void JoinDataSource(DataTable p_dtDef, DataTable p_dtAll, string p_strKeyName)
        {
            _dtDef = p_dtDef;
            _strKeyName = p_strKeyName;
            try
            {
                if (p_dtDef != null && p_dtAll != null)
                {
                    DataTable t1 = new DataTable();
                    if (p_dtDef.Columns[p_strKeyName].DataType == typeof(String))
                    {
                        IEnumerable<DataRow> q1 = from def in p_dtDef.AsEnumerable()
                                                  join all in p_dtAll.AsEnumerable() on def.Field<string>(p_strKeyName) equals all.Field<string>(p_strKeyName)
                                                  select all;
                        if (q1.Count() > 0) t1 = q1.CopyToDataTable();
                    }
                    else
                    {
                        IEnumerable<DataRow> q1 = from def in p_dtDef.AsEnumerable()
                                                  join all in p_dtAll.AsEnumerable() on def.Field<decimal>(p_strKeyName) equals all.Field<decimal>(p_strKeyName)
                                                  select all;
                        if (q1.Count() > 0) t1 = q1.CopyToDataTable();
                    }
                    if (t1.Rows.Count > 0)
                        gdLeft.DataSource = t1.AsEnumerable().Distinct().CopyToDataTable().DefaultView;
                    else
                        gdLeft.DataSource = p_dtAll.Clone().DefaultView;

                    IEnumerable<DataRow> q2 = p_dtAll.AsEnumerable().Except(t1.AsEnumerable(), DataRowComparer.Default);

                    if (q2.Count() > 0)
                        gdRight.DataSource = q2.Distinct().CopyToDataTable().DefaultView;
                    else
                        gdRight.DataSource = p_dtAll.Clone().DefaultView;
                }
            }
            catch (Exception e)
            {
                DialogBox.Msg(e.Message);
            }
        }

        /// <summary>
        /// 关联表并显示
        /// </summary>
        ///<param name="p_dtAll">显示所有的表</param>
        ///<param name="p_dtDef">定义关联的表</param>
        ///<param name="p_strKeyName">对应的关键字段</param>
        ///<param name="p_strFilter">关联表的过滤字段</param>
        /// <param name="p_intFilterValue">关联表过滤的值</param>
        public void JoinDataSource(DataTable p_dtDef, DataTable p_dtAll, string p_strKeyName, string p_strFilter, object p_objFilterValue)
        {
            string sf = p_dtDef.Columns[p_strFilter].DataType == typeof(String) ? "{0}='{1}'" : "{0}={1}";
            DataRow[] dr = p_dtDef.Select(string.Format(sf, p_strFilter, p_objFilterValue));
            DataTable dt;
            if (dr != null && dr.Count() > 0)
                dt = dr.CopyToDataTable();
            else
                dt = p_dtDef.Clone();
            JoinDataSource(dt, p_dtAll, p_strKeyName);
        }


        /// <summary>
        /// 设置表格显示的列
        /// </summary>
        /// <param name="visibleFields"></param>
        /// <param name="visibleCaption"></param>
        /// <param name="visibleWidth"></param>
        public void SetGrid(string[] visibleFields, string[] visibleCaption, int[] visibleWidth)
        {
            uiHelper.SetGridCol(gvRight, visibleFields, visibleCaption, visibleWidth);
            uiHelper.SetGridCol(gvLeft, visibleFields, visibleCaption, visibleWidth);
        }

        private void gvRight_DoubleClick(object sender, EventArgs e)
        {
            //右边移至左边
            MouseEventArgs eventE = (MouseEventArgs)(e);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo ghiRight = gvRight.CalcHitInfo(new Point(eventE.X, eventE.Y));

            if (ghiRight.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                uiHelper.MoveGridViewSelectedRow(gvRight, gvLeft);
            }
        }

        private void gvLeft_DoubleClick(object sender, EventArgs e)
        {
            //左边移至右边
            MouseEventArgs eventE = (MouseEventArgs)(e);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo ghiLeft = gvLeft.CalcHitInfo(new Point(eventE.X, eventE.Y));

            if (ghiLeft.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                uiHelper.MoveGridViewSelectedRow(gvLeft, gvRight);
            }
        }

        private void txtRight_TextChanged(object sender, EventArgs e)
        {
            string strFilter = txtRight.Text;//.GetFilter();

            if (gdRight.DataSource == null)
                return;
            try
            {
                (gdRight.DataSource as DataView).RowFilter = strFilter == string.Empty ? string.Empty : string.Format(FilterFormat, strFilter);
            }
            catch { }
            if (gvRight.SelectedRowsCount > 0)
            {
                int rowHandle = gvRight.GetSelectedRows()[0];
                gvRight.ClearSelection();
                gvRight.SelectRow(rowHandle);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            uiHelper.MoveGridViewSelectedRow(gvRight, gvLeft);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            uiHelper.MoveGridViewSelectedRow(gvLeft, gvRight);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            gvRight.SelectAll();
            uiHelper.MoveGridViewSelectedRow(gvRight, gvLeft);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gvLeft.SelectAll();
            uiHelper.MoveGridViewSelectedRow(gvLeft, gvRight);
        }

        private void txtLeft_TextChanged(object sender, EventArgs e)
        {
            string strFilter = txtLeft.Text;//.GetFilter();

            if (gdLeft.DataSource == null)
                return;
            try
            {
                (gdLeft.DataSource as DataView).RowFilter = strFilter == string.Empty ? string.Empty : string.Format(FilterFormat, strFilter);
            }
            catch { }

            if (gvLeft.SelectedRowsCount > 0)
            {
                int rowHandle = gvLeft.GetSelectedRows()[0];
                gvLeft.ClearSelection();
                gvLeft.SelectRow(rowHandle);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
