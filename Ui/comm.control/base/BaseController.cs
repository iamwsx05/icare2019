using Common.Entity;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// BaseController
    /// </summary>
    public class BaseController : IDisposable
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public BaseController()
        { }
        #endregion

        #region 变量
        /// <summary>
        /// 权限对象
        /// </summary>
        protected System.Security.Principal.IPrincipal objPrincipal = null;

        /// <summary>
        /// CellSize
        /// </summary>
        protected class CellSize
        {
            /// <summary>
            /// 宽度
            /// </summary>
            public int Width { get; set; }
            /// <summary>
            /// 高度
            /// </summary>
            public int Height { get; set; }
            /// <summary>
            /// LookUpEditContainer
            /// </summary>
            public LookUpEditContainer Cell { get; set; }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        protected BindingSource gvDataBindingSource = null;

        /// <summary>
        /// GridView
        /// </summary>
        protected GridView gridView { get; set; }

        /// <summary>
        /// BandedGridView
        /// </summary>
        protected BandedGridView gridViewBanded { get; set; }

        /// <summary>
        /// LookUpEdit列.FieldName
        /// </summary>
        protected Dictionary<string, string> LookUpEditFieldName = new Dictionary<string, string>();

        /// <summary>
        /// 清空列字段名
        /// </summary>
        protected string ResetColFieldName { get; set; }

        /// <summary>
        /// 清空列字段名（数组)
        /// </summary>
        protected List<string> ResetColFieldNameArr { get; set; }

        /// <summary>
        /// 文本列字段名
        /// </summary>
        protected List<string> TextColFieldName { get; set; }

        /// <summary>
        /// 数值列字段名
        /// </summary>
        protected List<string> ValueColFieldName { get; set; }

        /// <summary>
        /// 匹配列字段名
        /// </summary>
        protected Dictionary<string, string> MatchColFieldName { get; set; }
        protected Dictionary<string, string> MatchColFieldName2 { get; set; }

        /// <summary>
        /// 不能编辑列
        /// </summary>
        protected List<GridColumn> DisenableEditCols = new List<GridColumn>();

        /// <summary>
        /// 是否保存状态中
        /// </summary>
        protected bool IsSaving { get; set; }

        /// <summary>
        /// 当前焦点行
        /// </summary>
        protected int FocusedRowHandle
        {
            get
            {
                int rowHandle = -1;
                if (gridView != null)
                    rowHandle = gridView.FocusedRowHandle;
                else if (gridViewBanded != null)
                    rowHandle = gridViewBanded.FocusedRowHandle;
                return rowHandle;
            }
        }

        /// <summary>
        /// 不能编辑列单元格前景色
        /// </summary>
        protected Color CustomDisenableCell
        {
            get
            {
                //if (GlobalLoginInfo.LookAndFeelSkinValue == 8)
                //    return Color.FromArgb(115, 115, 115);
                //else
                //return Color.FromArgb(242, 242, 242);
                //return Color.FromArgb(226, 236, 247);

                return Color.FromArgb(236, 245, 250);
            }
        }

        /// <summary>
        /// 是否完成初始化过滤
        /// </summary>
        private bool IsInitFilter { get; set; }

        /// <summary>
        /// ParentViewer
        /// </summary>
        public frmBase ParentViewer { get; set; }

        /// <summary>
        /// 正在加载
        /// </summary>
        public bool IsLoading { get; set; }

        #endregion

        #region 释放
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            objPrincipal = null;
            GC.SuppressFinalize(this);
        }
        #endregion

        #region 方法

        #region GetFieldValueStr
        /// <summary>
        /// GetFieldValueStr
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected string GetFieldValueStr(int rowHandle, string fieldName)
        {
            if (gridView != null)
            {
                if (gridView.GetRowCellValue(rowHandle, fieldName) == null)
                    return string.Empty;
                else
                    return gridView.GetRowCellValue(rowHandle, fieldName).ToString().Trim();
            }
            else if (gridViewBanded != null)
            {
                if (gridViewBanded.GetRowCellValue(rowHandle, fieldName) == null)
                    return string.Empty;
                else
                    return gridViewBanded.GetRowCellValue(rowHandle, fieldName).ToString().Trim();
            }
            return string.Empty;
        }
        /// <summary>
        /// GetFieldValueStr
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected string GetFieldValueStr(GridView gv, int rowHandle, string fieldName)
        {
            if (gv != null)
            {
                if (gv.GetRowCellValue(rowHandle, fieldName) == null)
                    return string.Empty;
                else
                    return gv.GetRowCellValue(rowHandle, fieldName).ToString().Trim();
            }
            return string.Empty;
        }
        #endregion

        #region GetFieldValueObj
        /// <summary>
        /// GetFieldValueObj
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected object GetFieldValueObj(int rowHandle, string fieldName)
        {
            if (gridView != null)
                return gridView.GetRowCellValue(rowHandle, fieldName);
            else
                return gridViewBanded.GetRowCellValue(rowHandle, fieldName);
        }
        /// <summary>
        /// GetFieldValueObj
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected object GetFieldValueObj(GridView gv, int rowHandle, string fieldName)
        {
            return gv.GetRowCellValue(rowHandle, fieldName);
        }
        /// <summary>
        /// GetFieldValueObj
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected object GetFieldValueObj(string rowHandle, string fieldName)
        {
            if (gridView != null)
                return gridView.GetRowCellValue(int.Parse(rowHandle), fieldName);
            else
                return gridViewBanded.GetRowCellValue(int.Parse(rowHandle), fieldName);
        }
        /// <summary>
        /// GetFieldValueObj
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected object GetFieldValueObj(GridView gv, string rowHandle, string fieldName)
        {
            return gv.GetRowCellValue(int.Parse(rowHandle), fieldName);
        }
        #endregion

        #region 删除当前行
        /// <summary>
        /// 删除当前行
        /// </summary>
        protected virtual void DeleteCurrentRow()
        {
            if (FocusedRowHandle < 0) return;
            DeleteRow(FocusedRowHandle);
        }
        #endregion

        #region 删除行
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowHandle"></param>
        protected virtual void DeleteRow(int rowHandle)
        {
            if (rowHandle < 0) return;
            if (gridView != null)
            {
                gridView.CloseEditor();
                gridView.SelectRow(rowHandle);
            }
            if (gridViewBanded != null)
            {
                gridViewBanded.CloseEditor();
                gridViewBanded.SelectRow(rowHandle);
            }
            gvDataBindingSource.RemoveAt(rowHandle);
        }

        protected virtual void DeleteRow(GridView gv, BindingSource bind, int rowHandle)
        {
            if (rowHandle < 0) return;
            if (gv != null)
            {
                gv.CloseEditor();
                gv.SelectRow(rowHandle);
            }
            if (bind.Count > 0) bind.RemoveAt(rowHandle);
        }

        #endregion

        #region 插入行
        /// <summary>
        /// 插入行
        /// </summary>
        protected virtual void InsertRow<T>() where T : new()
        {
            if (FocusedRowHandle < 0) return;
            gvDataBindingSource.Insert(FocusedRowHandle, new T());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="rowHandle"></param>
        protected virtual void InsertRow<T>(int rowHandle) where T : new()
        {
            if (rowHandle < 0) return;
            gvDataBindingSource.Insert(rowHandle, new T());
        }

        protected virtual void InsertRow<T>(BindingSource bind, int rowHandle) where T : new()
        {
            if (rowHandle < 0) return;
            bind.Insert(rowHandle, new T());
        }
        #endregion

        #region 添加行
        /// <summary>
        /// 添加行
        /// </summary>
        protected virtual void AppendRow()
        {
            gvDataBindingSource.AddNew();
        }

        protected virtual void AppendRow(BindingSource bind)
        {
            bind.AddNew();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="rowCount"></param>
        protected virtual void AppendRows(int rowCount)
        {
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    gvDataBindingSource.AddNew();
                }
            }
        }

        protected virtual void AppendRows(BindingSource bind, int rowCount)
        {
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    bind.AddNew();
                }
            }
        }
        #endregion

        #region SetColumnOption
        /// <summary>
        /// SetColumnOption
        /// </summary>
        protected void SetColumnOption()
        {
            if (gridView != null)
            {
                SetColumnOption(gridView);
            }
            else if (gridViewBanded != null)
            {
                for (int i = 0; i < gridViewBanded.Columns.Count; i++)
                {
                    gridViewBanded.Columns[i].OptionsColumn.AllowMove = false;
                    gridViewBanded.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    gridViewBanded.Columns[i].OptionsColumn.ShowInCustomizationForm = false;
                    gridViewBanded.Columns[i].OptionsFilter.AllowAutoFilter = false;
                    gridViewBanded.Columns[i].OptionsFilter.AllowFilter = false;
                    gridViewBanded.Columns[i].OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
                    gridViewBanded.Columns[i].OptionsFilter.ImmediateUpdateAutoFilter = false;
                }
            }
        }

        /// <summary>
        /// SetColumnOption
        /// </summary>
        /// <param name="gv"></param>
        protected void SetColumnOption(GridView gv)
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                gv.Columns[i].OptionsColumn.AllowMove = false;
                gv.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gv.Columns[i].OptionsColumn.ShowInCustomizationForm = false;
                gv.Columns[i].OptionsFilter.AllowAutoFilter = false;
                gv.Columns[i].OptionsFilter.AllowFilter = false;
                gv.Columns[i].OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
                gv.Columns[i].OptionsFilter.ImmediateUpdateAutoFilter = false;
            }
        }
        #endregion

        #region FillRowValueByDBRow
        /// <summary>
        /// FillRowValueByDBRow
        /// </summary>
        /// <param name="DBRow"></param>
        protected virtual void FillRowValueByDBRow(BaseDataContract DBRow)
        { }

        protected virtual void FillRowValueByDBRow(GridView gv, BaseDataContract DBRow)
        { }
        #endregion

        #region FillRowValueByDBRow
        /// <summary>
        /// FillRowValueByDBRow
        /// </summary>
        /// <param name="val"></param>
        protected virtual void FillRowValueByDBRow(string val)
        { }
        #endregion

        #region ClearCache
        /// <summary>
        /// ClearCache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void ClearCache<T>() where T : BaseDataContract
        {
            if (gvDataBindingSource.DataSource != null && gvDataBindingSource.DataSource is BindingListView<T>)
            {
                if ((gvDataBindingSource.DataSource as BindingListView<T>).RemoveItemSource != null && (gvDataBindingSource.DataSource as BindingListView<T>).Count > 0)
                {
                    (gvDataBindingSource.DataSource as BindingListView<T>).RemoveItemSource.Clear();
                }
                List<T> lstT = new List<T>((gvDataBindingSource.DataSource as BindingListView<T>));
                if (lstT != null)
                {
                    foreach (T item in lstT)
                    {
                        item.CopyFieldObject = new Dictionary<string, object>();
                    }
                }
            }
        }
        #endregion

        #region Verify
        /// <summary>
        /// Verify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected EntityDML<T> Verify<T>() where T : BaseDataContract
        {
            if (gridView != null)
                gridView.CloseEditor();
            else if (gridViewBanded != null)
                gridViewBanded.CloseEditor();
            EntityDML<T> voDML = new EntityDML<T>();
            voDML.AddSource = new List<T>();
            voDML.UpdateSource = new List<T>();
            voDML.UpdatePreSource = new List<T>();
            if (gvDataBindingSource == null || gvDataBindingSource.DataSource == null)
            {
                return voDML;
            }
            List<T> lstT = new List<T>((gvDataBindingSource.DataSource as BindingListView<T>));
            if (gvDataBindingSource.DataSource is BindingListView<T> && (gvDataBindingSource.DataSource as BindingListView<T>).RemoveItemSource != null && (gvDataBindingSource.DataSource as BindingListView<T>).Count > 0)
            {
                voDML.DeleteSource = new List<T>();
                foreach (T item in (gvDataBindingSource.DataSource as BindingListView<T>).RemoveItemSource)
                {
                    if (item.CopyFieldObject != null)   // 删除
                    {
                        voDML.DeleteSource.Add(item);
                    }
                }
                if (voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                {
                    voDML.IsDelete = true;
                }
            }

            if (lstT == null || lstT.Count == 0) return voDML;

            string orgValue = string.Empty;
            string newValue = string.Empty;
            List<string> keys = null;
            foreach (T item in lstT)
            {
                if (item.CopyFieldObject == null)   // 新加
                {
                    voDML.AddSource.Add(item);
                }
                else                                // 修改
                {
                    if (keys == null) keys = new List<string>(item.CopyFieldObject.Keys);
                    item.CopyNewValue();
                    foreach (string key in keys)
                    {
                        if (item.CopyFieldObject[key] == null)
                        {
                            orgValue = string.Empty;
                        }
                        else
                        {
                            orgValue = item.CopyFieldObject[key].ToString();
                        }
                        if (item.NewFieldObject[key] == null)
                        {
                            newValue = string.Empty;
                        }
                        else
                        {
                            newValue = item.NewFieldObject[key].ToString();
                        }
                        if (orgValue != newValue)
                        {
                            item.IsModify = true;
                            voDML.UpdateSource.Add(item);
                            if (item.CloneObject != null)
                            {
                                voDML.UpdatePreSource.Add(item.CloneObject as T);
                            }
                            break;
                        }
                    }
                }
            }
            if (voDML.AddSource != null && voDML.AddSource.Count > 0)
            {
                voDML.IsAdd = true;
            }
            if (voDML.UpdateSource != null && voDML.UpdateSource.Count > 0)
            {
                voDML.IsUpdate = true;
            }
            return voDML;
        }

        /// <summary>
        /// Verify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected EntityDML<T> Verify<T>(GridView gv, BindingSource bind) where T : BaseDataContract
        {
            if (gv != null) gv.CloseEditor();
            EntityDML<T> voDML = new EntityDML<T>();
            voDML.AddSource = new List<T>();
            voDML.UpdateSource = new List<T>();
            voDML.UpdatePreSource = new List<T>();
            if (bind == null || bind.DataSource == null)
            {
                return voDML;
            }
            List<T> lstT = new List<T>((bind.DataSource as BindingListView<T>));
            if (bind.DataSource is BindingListView<T> && (bind.DataSource as BindingListView<T>).RemoveItemSource != null && (bind.DataSource as BindingListView<T>).RemoveItemSource.Count > 0)
            {
                voDML.DeleteSource = new List<T>();
                foreach (T item in (bind.DataSource as BindingListView<T>).RemoveItemSource)
                {
                    if (item.CopyFieldObject != null)   // 删除
                    {
                        voDML.DeleteSource.Add(item);
                    }
                }
                if (voDML.DeleteSource != null && voDML.DeleteSource.Count > 0)
                {
                    voDML.IsDelete = true;
                }
            }

            if (lstT == null || lstT.Count == 0) return voDML;

            bool isNumber = false;
            bool isEqual = false;
            string orgValue = string.Empty;
            string newValue = string.Empty;
            List<string> keys = null;
            foreach (T item in lstT)
            {
                if (item.CopyFieldObject == null)   // 新加
                {
                    voDML.AddSource.Add(item);
                }
                else                                // 修改
                {
                    if (keys == null) keys = new List<string>(item.CopyFieldObject.Keys);
                    item.CopyNewValue();
                    foreach (string key in keys)
                    {
                        if (item.CopyFieldObject[key] == null)
                        {
                            orgValue = string.Empty;
                        }
                        else
                        {
                            orgValue = item.CopyFieldObject[key].ToString();
                        }
                        if (item.NewFieldObject[key] == null)
                        {
                            newValue = string.Empty;
                        }
                        else
                        {
                            newValue = item.NewFieldObject[key].ToString();
                        }
                        isNumber = (Function.IsNumber(orgValue) && Function.IsNumber(newValue)) ? true : false;
                        if (isNumber)
                            isEqual = Function.Dec(orgValue) == Function.Dec(newValue) ? true : false;
                        else
                            isEqual = orgValue == newValue ? true : false;
                        if (isEqual == false)
                        {
                            item.IsModify = true;
                            voDML.UpdateSource.Add(item);
                            if (item.CloneObject != null)
                            {
                                voDML.UpdatePreSource.Add(item.CloneObject as T);
                            }
                            break;
                        }
                    }
                }
            }
            if (voDML.AddSource != null && voDML.AddSource.Count > 0)
            {
                voDML.IsAdd = true;
            }
            if (voDML.UpdateSource != null && voDML.UpdateSource.Count > 0)
            {
                voDML.IsUpdate = true;
            }
            return voDML;
        }

        #endregion

        #region RefreshOriginalValue
        /// <summary>
        /// RefreshOriginalValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void RefreshOriginalValue<T>() where T : BaseDataContract
        {
            (gvDataBindingSource.DataSource as BindingListView<T>).RemoveItemSource.Clear();
            List<T> lstT = new List<T>((gvDataBindingSource.DataSource as BindingListView<T>));
            foreach (T item in lstT)
            {
                item.CopyOriginalValue();
            }
        }

        protected void RefreshOriginalValue<T>(BindingSource bind) where T : BaseDataContract
        {
            (bind.DataSource as BindingListView<T>).RemoveItemSource.Clear();
            List<T> lstT = new List<T>((bind.DataSource as BindingListView<T>));
            foreach (T item in lstT)
            {
                item.CopyOriginalValue();
            }
        }
        #endregion

        #region ClearInvalidData
        /// <summary>
        /// 清空无效数据
        /// </summary>
        protected virtual void ClearInvalidData()
        { }
        #endregion

        #region 行数据提醒
        /// <summary>
        /// 行数据提醒
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string RowDataHint(int rowIndex, string data)
        {
            return "第" + rowIndex.ToString() + "行: <" + data + "> 不能为空，请填写。";
        }
        #endregion

        #region 检查不为空列
        /// <summary>
        /// 检查不为空列
        /// </summary>
        /// <returns>true 通过 false 不通过</returns>
        protected virtual bool CheckNotNullColumn()
        {
            return true;
        }
        #endregion

        #region 检查数据是否更改
        /// <summary>
        /// 检查数据是否更改
        /// </summary>
        /// <returns>true 有变化 false 无变化</returns>
        public virtual bool CheckDataChanged()
        {
            return false;
        }
        #endregion

        #region SetEditValueChangedEvent

        /// <summary>
        /// SetEditValueChangedEvent
        /// </summary>
        /// <param name="ctrl"></param>
        public void SetEditValueChangedEvent(Control ctrl)
        {
            if (ctrl is DevExpress.XtraEditors.TextEdit)
            {
                ((DevExpress.XtraEditors.TextEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.MemoEdit)
            {
                ((DevExpress.XtraEditors.MemoEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.ComboBoxEdit)
            {
                ((DevExpress.XtraEditors.ComboBoxEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.DateEdit)
            {
                ((DevExpress.XtraEditors.DateEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.TimeEdit)
            {
                ((DevExpress.XtraEditors.TimeEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.RadioGroup)
            {
                ((DevExpress.XtraEditors.RadioGroup)ctrl).SelectedIndexChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is Common.Controls.LookUpEdit)
            {
                ((Common.Controls.LookUpEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else
            {
                if (ctrl.HasChildren)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        SetEditValueChangedEvent(ctrl2);
                    }
                }
            }
        }

        /// <summary>
        /// SetEditValueChangedEvent
        /// </summary>
        /// <param name="gv"></param>
        public void SetEditValueChangedEvent(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            gv.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_CellValueChanged);
        }

        void ctrl_EditValueChanged(object sender, EventArgs e)
        {
            if (ParentViewer != null) ParentViewer.ValueChanged = true;
        }

        void gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (ParentViewer != null) ParentViewer.ValueChanged = true;
        }

        #endregion

        #endregion

        #region LookUpEdit.四事件

        /// <summary>
        /// FieldName
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string FieldName(LookUpEditContainer lue, string key)
        {
            string fieldName = string.Empty;
            if (MatchColFieldName != null && MatchColFieldName.ContainsKey(key))
            {
                fieldName = MatchColFieldName[key];
            }
            if (MatchColFieldName2 != null && MatchColFieldName2.ContainsKey(key))
            {
                fieldName = MatchColFieldName2[key];
            }
            if (!string.IsNullOrEmpty(fieldName))
            {
                if (lue.ParentGridView != null)
                {
                    for (int i = 0; i < lue.ParentGridView.Columns.Count; i++)
                    {
                        if (lue.ParentGridView.Columns[i].FieldName == fieldName) return fieldName;
                    }
                }
                else if (lue.ParentBandedGridView != null)
                {
                    for (int i = 0; i < lue.ParentBandedGridView.Columns.Count; i++)
                    {
                        if (lue.ParentBandedGridView.Columns[i].FieldName == fieldName) return fieldName;
                    }
                }
                else if (gridView != null)
                {
                    for (int i = 0; i < gridView.Columns.Count; i++)
                    {
                        if (gridView.Columns[i].FieldName == fieldName) return fieldName;
                    }
                }
                else if (gridViewBanded != null)
                {
                    for (int i = 0; i < gridViewBanded.Columns.Count; i++)
                    {
                        if (gridViewBanded.Columns[i].FieldName == fieldName) return fieldName;
                    }
                }
            }
            return fieldName;
        }

        #region cell_Enter
        /// <summary>
        /// 自定义事件cell_Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cell_Enter(object sender, EventArgs e)
        {
            LookUpEdit cell = sender as LookUpEdit;
            if (cell == null) return;

            string strDBValue = string.Empty;
            string fieldName = FieldName(cell.Properties, cell.Properties.FieldName);
            if (LookUpEditFieldName.ContainsKey(fieldName))
            {
                try
                {
                    if (cell.Properties.ParentGridView != null)
                    {
                        strDBValue = GetFieldValueStr(cell.Properties.ParentGridView, cell.Properties.ParentGridView.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                        cell.Properties.DisplayValue = cell.Properties.ParentGridView.GetRowCellDisplayText(cell.Properties.ParentGridView.FocusedRowHandle, fieldName);
                    }
                    else if (cell.Properties.ParentBandedGridView != null)
                    {
                        strDBValue = GetFieldValueStr(cell.Properties.ParentBandedGridView, cell.Properties.ParentBandedGridView.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                        cell.Properties.DisplayValue = cell.Properties.ParentBandedGridView.GetRowCellDisplayText(cell.Properties.ParentBandedGridView.FocusedRowHandle, fieldName);
                    }
                    else if (gridView != null)
                    {
                        strDBValue = GetFieldValueStr(gridView, gridView.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                        cell.Properties.DisplayValue = gridView.GetRowCellDisplayText(gridView.FocusedRowHandle, fieldName);
                    }
                    else if (gridViewBanded != null)
                    {
                        strDBValue = GetFieldValueStr(gridViewBanded, gridViewBanded.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                        cell.Properties.DisplayValue = gridViewBanded.GetRowCellDisplayText(gridViewBanded.FocusedRowHandle, fieldName);
                    }
                }
                catch { }
                cell.Properties.DBValue = strDBValue;
                cell.Properties.DBRow = null;

                bool b = strDBValue == GlobalParm.DESC_ORDER_CODE;
                cell.SetDescEdit(cell.Properties.DisplayValue, b);
                if (!IsInitFilter && !string.IsNullOrEmpty(strDBValue))
                {
                    IsInitFilter = true;
                    cell.Filter(strDBValue);
                }
            }
        }
        #endregion

        #region cell_KeyDown
        /// <summary>
        /// 自定义事件cell_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cell_KeyDown(object sender, KeyEventArgs e)
        {
            LookUpEdit cell = sender as LookUpEdit;
            if (cell == null) return;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                string fieldName = FieldName(cell.Properties, cell.Properties.FieldName);
                string strDisplay = string.Empty;
                try
                {
                    if (cell.Properties.ParentGridView != null)
                        strDisplay = cell.Properties.ParentGridView.GetRowCellDisplayText(cell.Properties.ParentGridView.FocusedRowHandle, fieldName);
                    else if (cell.Properties.ParentBandedGridView != null)
                        strDisplay = cell.Properties.ParentBandedGridView.GetRowCellDisplayText(cell.Properties.ParentBandedGridView.FocusedRowHandle, fieldName);
                    else if (gridView != null)
                        strDisplay = gridView.GetRowCellDisplayText(gridView.FocusedRowHandle, fieldName);
                    else if (gridViewBanded != null)
                        strDisplay = gridViewBanded.GetRowCellDisplayText(gridViewBanded.FocusedRowHandle, fieldName);
                }
                catch { }
                if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(strDisplay) && LookUpEditFieldName.ContainsKey(fieldName))
                {
                    string strValue = string.Empty;
                    if (cell.Properties.ParentGridView != null)
                        strValue = GetFieldValueStr(cell.Properties.ParentGridView, cell.Properties.ParentGridView.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                    else if (cell.Properties.ParentBandedGridView != null)
                        strValue = GetFieldValueStr(cell.Properties.ParentBandedGridView, cell.Properties.ParentBandedGridView.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                    else if (gridView != null)
                        strValue = GetFieldValueStr(gridView, gridView.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                    else if (gridViewBanded != null)
                        strValue = GetFieldValueStr(gridViewBanded, gridViewBanded.FocusedRowHandle, LookUpEditFieldName[fieldName]);
                    if (!string.IsNullOrEmpty(strValue) && string.IsNullOrEmpty(cell.Properties.DBValue))
                    {
                        cell.Properties.DBValue = strValue;
                        cell.Properties.DisplayValue = strDisplay;
                    }
                }
            }
            else
            {
                if (e.Control && (e.KeyCode == Keys.Delete || e.KeyCode == Keys.D))
                {
                    if (cell.Properties.ParentGridView != null && cell.Properties.ParentBindingSource != null)
                        DeleteRow(cell.Properties.ParentGridView, cell.Properties.ParentBindingSource, cell.Properties.ParentGridView.FocusedRowHandle);
                    else
                        DeleteCurrentRow();
                }
            }
        }
        #endregion

        #region cell_HandleResetDBValue
        /// <summary>
        /// 自定义事件cell_HandleResetDBValue
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void cell_HandleResetDBValue(object sender)
        {
            LookUpEdit cell = sender as LookUpEdit;
            if (cell == null || cell.Properties.FieldName == null || (string.IsNullOrEmpty(this.ResetColFieldName) && this.ResetColFieldNameArr == null && this.ResetColFieldNameArr.Count == 0))
            {
                return;
            }
            if (IsSaving) return;
            string fieldName = FieldName(cell.Properties, cell.Properties.FieldName);
            if (cell.Properties.ParentGridView != null)
            {
                cell.Properties.ParentGridView.CloseEditor();
                cell.Properties.ParentGridView.SetRowCellValue(cell.Properties.ParentGridView.FocusedRowHandle, LookUpEditFieldName[fieldName], null);
                if ((fieldName == this.ResetColFieldName || this.ResetColFieldNameArr.IndexOf(fieldName) >= 0) && string.IsNullOrEmpty(GetFieldValueStr(cell.Properties.ParentGridView, cell.Properties.ParentGridView.FocusedRowHandle, fieldName)))
                {
                    foreach (string str in TextColFieldName)
                    {
                        cell.Properties.ParentGridView.SetRowCellValue(cell.Properties.ParentGridView.FocusedRowHandle, str, string.Empty);
                    }
                    foreach (string str in ValueColFieldName)
                    {
                        cell.Properties.ParentGridView.SetRowCellValue(cell.Properties.ParentGridView.FocusedRowHandle, str, null);
                    }
                }
            }
            else if (cell.Properties.ParentBandedGridView != null)
            {
                cell.Properties.ParentBandedGridView.CloseEditor();
                cell.Properties.ParentBandedGridView.SetRowCellValue(cell.Properties.ParentBandedGridView.FocusedRowHandle, LookUpEditFieldName[fieldName], null);
                if ((fieldName == this.ResetColFieldName || this.ResetColFieldNameArr.IndexOf(fieldName) >= 0) && !string.IsNullOrEmpty(GetFieldValueStr(cell.Properties.ParentBandedGridView, cell.Properties.ParentBandedGridView.FocusedRowHandle, fieldName)))
                {
                    foreach (string str in TextColFieldName)
                    {
                        cell.Properties.ParentBandedGridView.SetRowCellValue(cell.Properties.ParentBandedGridView.FocusedRowHandle, str, string.Empty);
                    }
                    foreach (string str in ValueColFieldName)
                    {
                        cell.Properties.ParentBandedGridView.SetRowCellValue(cell.Properties.ParentBandedGridView.FocusedRowHandle, str, null);
                    }
                }
            }
            else if (gridView != null)
            {
                gridView.CloseEditor();
                gridView.SetRowCellValue(gridView.FocusedRowHandle, LookUpEditFieldName[fieldName], null);
                if ((fieldName == this.ResetColFieldName || this.ResetColFieldNameArr.IndexOf(fieldName) >= 0) && string.IsNullOrEmpty(GetFieldValueStr(gridView, gridView.FocusedRowHandle, fieldName)))
                {
                    foreach (string str in TextColFieldName)
                    {
                        gridView.SetRowCellValue(gridView.FocusedRowHandle, str, string.Empty);
                    }
                    foreach (string str in ValueColFieldName)
                    {
                        gridView.SetRowCellValue(gridView.FocusedRowHandle, str, null);
                    }
                }
            }
            else if (gridViewBanded != null)
            {
                gridViewBanded.CloseEditor();
                gridViewBanded.SetRowCellValue(gridViewBanded.FocusedRowHandle, LookUpEditFieldName[fieldName], null);
                if ((fieldName == this.ResetColFieldName || this.ResetColFieldNameArr.IndexOf(fieldName) >= 0) && !string.IsNullOrEmpty(GetFieldValueStr(gridViewBanded, gridViewBanded.FocusedRowHandle, fieldName)))
                {
                    foreach (string str in TextColFieldName)
                    {
                        gridViewBanded.SetRowCellValue(gridViewBanded.FocusedRowHandle, str, string.Empty);
                    }
                    foreach (string str in ValueColFieldName)
                    {
                        gridViewBanded.SetRowCellValue(gridViewBanded.FocusedRowHandle, str, null);
                    }
                }
            }
        }
        #endregion

        #region cell_HandleDBValueChanged
        /// <summary>
        /// 自定义事件cell_HandleDBValueChanged
        /// </summary>
        /// <param name="sender"></param>
        protected void cell_HandleDBValueChanged(object sender)
        {
            LookUpEdit cell = sender as LookUpEdit;
            if (cell == null || !cell.CellValueChanged) return;
            if (string.IsNullOrEmpty(cell.Properties.DBValue)) return;

            string fieldName = FieldName(cell.Properties, cell.Properties.FieldName);
            cell.CellValueChanged = false;

            try
            {
                if (cell.Properties.ParentGridView != null)
                {
                    string strDisplay = cell.Properties.ParentGridView.GetRowCellDisplayText(cell.Properties.ParentGridView.FocusedRowHandle, fieldName);
                    if (strDisplay != cell.Properties.DisplayValue)
                        cell.Properties.ParentGridView.SetRowCellValue(cell.Properties.ParentGridView.FocusedRowHandle, fieldName, cell.Properties.DisplayValue);
                    cell.Properties.ParentGridView.SetRowCellValue(cell.Properties.ParentGridView.FocusedRowHandle, LookUpEditFieldName[fieldName], cell.Properties.DBValue);
                }
                else if (cell.Properties.ParentBandedGridView != null)
                {
                    string strDisplay = cell.Properties.ParentBandedGridView.GetRowCellDisplayText(cell.Properties.ParentBandedGridView.FocusedRowHandle, fieldName);
                    if (strDisplay != cell.Properties.DisplayValue)
                        cell.Properties.ParentBandedGridView.SetRowCellValue(cell.Properties.ParentBandedGridView.FocusedRowHandle, fieldName, cell.Properties.DisplayValue);
                    cell.Properties.ParentBandedGridView.SetRowCellValue(cell.Properties.ParentBandedGridView.FocusedRowHandle, LookUpEditFieldName[fieldName], cell.Properties.DBValue);
                }
                else if (gridView != null)
                {
                    string strDisplay = gridView.GetRowCellDisplayText(gridView.FocusedRowHandle, fieldName);
                    if (strDisplay != cell.Properties.DisplayValue)
                        gridView.SetRowCellValue(gridView.FocusedRowHandle, fieldName, cell.Properties.DisplayValue);
                    gridView.SetRowCellValue(gridView.FocusedRowHandle, LookUpEditFieldName[fieldName], cell.Properties.DBValue);
                }
                else if (gridViewBanded != null)
                {
                    string strDisplay = gridViewBanded.GetRowCellDisplayText(gridViewBanded.FocusedRowHandle, fieldName);
                    if (strDisplay != cell.Properties.DisplayValue)
                        gridViewBanded.SetRowCellValue(gridViewBanded.FocusedRowHandle, fieldName, cell.Properties.DisplayValue);
                    gridViewBanded.SetRowCellValue(gridViewBanded.FocusedRowHandle, LookUpEditFieldName[fieldName], cell.Properties.DBValue);
                }
            }
            catch { }

            if (cell.Properties.FieldName == "OrderCode" && GlobalHospital.HospitalCode == "0015" && cell.Properties.DBValue == GlobalParm.DESC_ORDER_CODE)
            {
                //EntityOrderDic tmpVo = GlobalDic.DataSourceOrderDic.FirstOrDefault(t => t.OrderName == "##");
                //if (tmpVo != null)
                //{
                //    EntityOrderDic dbRow = new EntityOrderDic();
                //    dbRow.OrderCode = cell.Properties.DBValue;
                //    dbRow.OrderName = cell.Properties.DisplayValue;

                //    dbRow.Spec = tmpVo.Spec;
                //    dbRow.TypeID = tmpVo.TypeID;
                //    dbRow.TypeName = tmpVo.TypeName;
                //    dbRow.Price = tmpVo.Price;
                //    dbRow.Amount = tmpVo.Amount;
                //    dbRow.Days = tmpVo.Days;
                //    dbRow.IsDrug = tmpVo.IsDrug;
                //    dbRow.Dosage = tmpVo.Dosage;
                //    dbRow.DosageScale = tmpVo.DosageScale;
                //    dbRow.DosageUnitID = tmpVo.DosageUnitID;
                //    dbRow.DosageUnitName = tmpVo.DosageUnitName;
                //    dbRow.UnitID = tmpVo.UnitID;
                //    dbRow.UnitName = tmpVo.UnitName;
                //    dbRow.FreqID = tmpVo.FreqID;
                //    dbRow.FreqName = tmpVo.FreqName;
                //    dbRow.ExecDeptID = tmpVo.ExecDeptID;
                //    dbRow.ExecDeptName = tmpVo.ExecDeptName;
                //    dbRow.PackSn = tmpVo.PackSn;
                //    dbRow.UsageID = tmpVo.UsageID;
                //    dbRow.UsageName = tmpVo.UsageName;
                //    dbRow.OrgSysCode = tmpVo.OrgSysCode;
                //    dbRow.PoisonClass = tmpVo.PoisonClass;
                //    dbRow.MedLimit = tmpVo.MedLimit;
                //    dbRow.SkinTestFlag = tmpVo.SkinTestFlag;
                //    dbRow.BaseMedFlag = tmpVo.BaseMedFlag;
                //    dbRow.PharmacyNo = tmpVo.PharmacyNo;
                //    dbRow.PharmacyName = tmpVo.PharmacyName;
                //    dbRow.PyCode = tmpVo.PyCode;
                //    dbRow.WbCode = tmpVo.WbCode;
                //    FillRowValueByDBRow(dbRow);
                //}
                //return;
            }

            if ((fieldName == this.ResetColFieldName || (this.ResetColFieldNameArr != null && this.ResetColFieldNameArr.IndexOf(fieldName) >= 0)) &&
                cell.Properties.DBRow != null)
            {
                if (cell.Properties.ParentGridView != null)
                {
                    FillRowValueByDBRow(cell.Properties.ParentGridView, cell.Properties.DBRow);
                }
                else
                {
                    FillRowValueByDBRow(cell.Properties.DBRow);
                }
            }
            else
            {
                FillRowValueByDBRow(cell.Properties.DBValue);
            }
        }
        #endregion

        #endregion

        #region 设置UI
        /// <summary>
        /// 设置UI
        /// </summary>
        /// <param name="child"></param>
        public virtual void SetUI(frmBase child)
        {
            ParentViewer = child;
        }
        #endregion

        #region 设置UC
        /// <summary>
        /// 设置UC
        /// </summary>
        /// <param name="child"></param>
        public virtual void SetUC(ucBase child)
        { }
        #endregion
    }
}
