using Common.Controls;
using System.Windows.Forms;

namespace Common.Controls
{
    public class ctlFormDesign : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmFormDesign Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmFormDesign)child;
            this.gvDataBindingSource = new BindingSource();
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 下拉Order数据源
        /// </summary>
        //private static EntityOrderDic[] DataSourceOrder = null;

        /// <summary>
        /// 字段名
        /// </summary>
        string _FieldName { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        internal string FieldName
        {
            get { return _FieldName; }
            set
            {
                _FieldName = value;
                GetDataSource();
            }
        }

        /// <summary>
        /// 父节点名
        /// </summary>
        internal string ParentNodeName { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            #region 判断缓存
            //if (GlobalDic.DataSourceOrderDic == null)
            //{
            //    try
            //    {
            //        DialogBox.Msg("医嘱字典加载中,请重新打开...");
            //        if ((Viewer) != null) (Viewer).Close();
            //    }
            //    catch
            //    { }
            //    return;
            //}
            #endregion

            // 初始化
            // 1.
            #region 控制基类初始化
            //this.gridView = Viewer.gvOrderItem;
            #endregion

            //uiHelper.SetViewAppearance(this.gridView);
            //Viewer.gvOrderItem.IndicatorWidth = 10;
            //this.gvDataBindingSource = new BindingSource();
            //Viewer.gcOrderItem.DataSource = gvDataBindingSource;

            //// 初始化
            //// 1.            
            //this.MatchColFieldName = new Dictionary<string, string>();
            //this.MatchColFieldName.Add(EntityEfOrderItem.Columns.OrderCode, EntityEfOrderItem.Columns.OrderName);
            //this.TextColFieldName = new List<string>();
            //this.ValueColFieldName = new List<string>();
            //this.LookUpEditFieldName.Clear();
            ////this.LookUpEditFieldName.Add(EntityEfOrderItem.Columns.Ordercode, EntityEfOrderItem.Columns.Ordername);
            //this.LookUpEditFieldName.Add(EntityEfOrderItem.Columns.OrderName, EntityEfOrderItem.Columns.OrderCode);

            //// 2.
            //LookUpEditContainer cell = new LookUpEditContainer(450, 300);
            //cell.FieldName = EntityOrderDic.Columns.OrderCode;
            //cell.ValueColumn = EntityOrderDic.Columns.OrderCode;
            //cell.DisplayColumn = EntityOrderDic.Columns.OrderName;
            //cell.Essential = true;
            //cell.IsShowColumnHeaders = true;
            //cell.DescCode = GlobalSysParameter.DESC_ORDER_CODE;
            //cell.ColumnWidth.Add(EntityOrderDic.Columns.OrderCode, 60);
            //cell.ColumnWidth.Add(EntityOrderDic.Columns.OrderName, 150);
            //cell.ColumnWidth.Add(EntityOrderDic.Columns.Spec, 80);
            //cell.ColumnWidth.Add(EntityOrderDic.Columns.Price, 50);
            //cell.ColumnHeaders.Add(EntityOrderDic.Columns.OrderCode, "医嘱代码");
            //cell.ColumnHeaders.Add(EntityOrderDic.Columns.OrderName, "医嘱名称");
            //cell.ColumnHeaders.Add(EntityOrderDic.Columns.Spec, "规格");
            //cell.ColumnHeaders.Add(EntityOrderDic.Columns.Price, "单价");
            //cell.ShowColumn = EntityOrderDic.Columns.OrderCode + "|" + EntityOrderDic.Columns.OrderName + "|" + EntityOrderDic.Columns.Spec + "|" + EntityOrderDic.Columns.Price;
            //cell.ColumnHeaders.Add(EntityOrderDic.Columns.PharmacyName, "地点");
            //cell.ShowColumn += "|" + EntityOrderDic.Columns.PharmacyName;
            //cell.IsUseShowColumn = true;
            //cell.FilterColumn = EntityOrderDic.Columns.OrderCode + "|" + EntityOrderDic.Columns.OrderName + "|" + EntityOrderDic.Columns.PyCode + "|" + EntityOrderDic.Columns.WbCode;
            //cell.Enter += new EventHandler(cell_Enter); ;
            //cell.KeyDown += new KeyEventHandler(cell_KeyDown);
            //cell.HandleResetDBValue += new _HandleResetDBValue(cell_HandleResetDBValue);
            //cell.HandleDBValueChanged += new _HandleDBValueChanged(cell_HandleDBValueChanged);

            //if (DataSourceOrder == null)
            //{
            //    // 查询医嘱字典
            //    DataSourceOrder = GlobalDic.DataSourceOrderDic.ToArray();
            //}
            //cell.DataSource = DataSourceOrder;
            //Viewer.colOrdername.ColumnEdit = cell;

        }
        #endregion

        #region AppendRow
        /// <summary>
        /// AppendRow
        /// </summary>
        internal void NewRow()
        {
            AppendRow();
        }
        #endregion

        #region DelRow
        /// <summary>
        /// DelRow
        /// </summary>
        internal void DelRow()
        {
            DeleteCurrentRow();
        }
        #endregion

        #region GetDataSource
        /// <summary>
        /// GetDataSource
        /// </summary>
        /// <returns></returns>
        private void GetDataSource()
        {
            //ProxyCpDesign proxy = new ProxyCpDesign();
            //BindingListView<EntityEfOrderItem> DataSource = new BindingListView<EntityEfOrderItem>();
            //DataSource.AddRange(proxy.Service.GetEfOrderItem(Viewer.EfId, this.FieldName));
            //proxy = null;
            //if (DataSource.Count == 0)
            //{
            //    DataSource.Add(new EntityEfOrderItem());
            //}
            //else
            //{
            //    foreach (EntityEfOrderItem item in DataSource)
            //    {
            //        item.CopyOriginalValue();
            //        item.CloneObject = item.Clone();
            //    }
            //}

            //// 3.
            //this.gvDataBindingSource.DataSource = DataSource;
            ////return DataSource;
        }
        #endregion

        #region ClearInvalidData
        /// <summary>
        /// 清空无效数据
        /// </summary>
        protected override void ClearInvalidData()
        {
            //if (gvDataBindingSource == null || gvDataBindingSource.DataSource == null) return;
            //gridView.CloseEditor();

            //List<EntityEfOrderItem> lstOrder = new List<EntityEfOrderItem>((gvDataBindingSource.DataSource as BindingListView<EntityEfOrderItem>));
            //if (lstOrder == null || lstOrder.Count == 0) return;
            //for (int i = lstOrder.Count - 1; i >= 0; i--)
            //{
            //    if (string.IsNullOrEmpty(lstOrder[i].OrderCode) && string.IsNullOrEmpty(lstOrder[i].OrderName))
            //    {
            //        DeleteRow(i);
            //    }
            //}
        }
        #endregion

        #region CheckNotNullColumn
        /// <summary>
        /// 检查不为空列
        /// </summary>
        /// <returns></returns>
        protected override bool CheckNotNullColumn()
        {
            //if (gvDataBindingSource == null || gvDataBindingSource.DataSource == null) return true;
            //gridView.CloseEditor();

            //List<EntityEfOrderItem> lstOrder = new List<EntityEfOrderItem>((gvDataBindingSource.DataSource as BindingListView<EntityEfOrderItem>));
            //if (lstOrder == null || lstOrder.Count == 0) return true;
            //for (int i = lstOrder.Count - 1; i >= 0; i--)
            //{
            //    if (string.IsNullOrEmpty(lstOrder[i].OrderName))
            //    {
            //        gridView.FocusedRowHandle = i;
            //        DialogBox.Msg(RowDataHint(i + 1, "医嘱名称"), MessageBoxIcon.Exclamation);
            //        return false;
            //    }
            //}
            return true;
        }
        #endregion

        #region CheckDataChanged
        /// <summary>
        /// 检查数据是否更改
        /// </summary>
        /// <returns></returns>
        public override bool CheckDataChanged()
        {
            //ClearInvalidData();
            //EntityDML<EntityEfOrderItem> voDML = Verify<EntityEfOrderItem>();
            //if (voDML.IsAdd || voDML.IsDelete || voDML.IsUpdate)
            //{
            //    return true;
            //}
            return false;
        }
        #endregion

        #region SaveOrderItem
        /// <summary>
        /// SaveOrderItem
        /// </summary>
        internal void SaveOrderItem()
        {
            //if (Viewer.EfId <= 0)
            //{
            //    DialogBox.Msg("请先保存表单。");
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.FieldName))
            //{
            //    DialogBox.Msg("请先给项目代码命名并保存。");
            //    return;
            //}
            //Viewer.IsSave = false;
            //ClearInvalidData();
            //if (!CheckNotNullColumn()) return;

            //try
            //{
            //    Viewer.Cursor = Cursors.WaitCursor;
            //    EntityDML<EntityEfOrderItem> voDML = Verify<EntityEfOrderItem>();
            //    if (voDML.IsAdd || voDML.IsDelete || voDML.IsUpdate)
            //    {
            //        if (voDML.AddSource != null && voDML.AddSource.Count > 0)
            //        {
            //            if (string.IsNullOrEmpty(this.ParentNodeName))
            //            {
            //                DialogBox.Msg("请给 父节点名 赋值。");
            //                return;
            //            }
            //            foreach (EntityEfOrderItem item in voDML.AddSource)
            //            {
            //                item.Efid = Viewer.EfId;
            //                item.Fieldname = this.FieldName;
            //                item.Nodename = this.ParentNodeName;
            //            }
            //        }
            //        ProxyCpDesign proxy = new ProxyCpDesign();
            //        int intRet = proxy.Service.SaveEfOrderItem(voDML.AddSource, voDML.DeleteSource, voDML.UpdateSource);
            //        proxy = null;
            //        if (intRet > 0)
            //        {
            //            Viewer.IsSave = true;
            //            DialogBox.Msg("保存医嘱项目成功！", MessageBoxIcon.Exclamation);
            //        }
            //        else
            //        {
            //            DialogBox.Msg("保存医嘱项目失败.", MessageBoxIcon.Exclamation);
            //        }
            //    }
            //    else
            //    {
            //        DialogBox.Msg("数据无变化...", MessageBoxIcon.Exclamation);
            //    }
            //}
            //finally
            //{
            //    Viewer.Cursor = Cursors.Default;
            //}
        }
        #endregion

        #endregion
    }
}
