using Common.Controls;
using Common.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 表格设计管理
    /// </summary>
    public class ctlTableManage : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmTableManage Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmTableManage)child;
            this.gvDataBindingSource = new BindingSource();
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// isInit
        /// </summary>
        bool isInit { get; set; }

        /// <summary>
        /// isChecking
        /// </summary>
        bool isChecking { get; set; }

        /// <summary>
        /// 表格.数据源
        /// </summary>
        List<EntityEmrTableBasicInfo> DataSourceTable { get; set; }

        /// <summary>
        /// 默认VO
        /// </summary>
        EntityEmrTableBasicInfo defaultVo { get; set; }

        /// <summary>
        /// 查找索引
        /// </summary>
        internal int findIndex { get; set; }

        /// <summary>
        /// frmContent
        /// </summary>
        frmContent frmConfig { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            try
            {
                isInit = true;
                InitTableField();
                SetEditValueChangedEvent(Viewer.plTop);
                SetEditValueChangedEvent(Viewer.gvTable);
                CreateTree();
                LoadDataSource(false);
            }
            finally
            {
                isInit = false;
            }
        }
        #endregion

        #region InitTableField
        /// <summary>
        /// InitTableField
        /// </summary>
        void InitTableField()
        {
            this.gvDataBindingSource = null;
            this.gvDataBindingSource = new BindingSource();
            Viewer.gcTable.DataSource = this.gvDataBindingSource;
        }
        #endregion

        #region CreateTree
        /// <summary>
        /// CreateTree
        /// </summary>
        void CreateTree()
        {
            // 树结构
            Viewer.tvTable.Columns.Clear();
            uiHelper.SetGridCol(Viewer.tvTable, new string[] { "tableName" }, new string[] { "表格列表" }, new int[] { 200 });
            Viewer.tvTable.Columns["tableName"].AppearanceCell.Font = new Font("宋体", 9);
            Viewer.tvTable.KeyFieldName = "tableCode";
            Viewer.tvTable.ParentFieldName = "parent";
            Viewer.tvTable.ImageIndexFieldName = "imageIndex";

            Viewer.tvTable.OptionsView.ShowFocusedFrame = false;
            //Viewer.tvForm.OptionsView.ShowVertLines = true;
            Viewer.tvTable.Appearance.FocusedRow.Options.UseBackColor = true;
            Viewer.tvTable.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            Viewer.tvTable.Appearance.FocusedRow.BackColor2 = Color.White;
            Viewer.tvTable.Appearance.HideSelectionRow.Options.UseBackColor = true;
            Viewer.tvTable.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            Viewer.tvTable.Appearance.HideSelectionRow.BackColor2 = Color.White;

            Viewer.tvTable.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(tvTable_FocusedNodeChanged);

        }
        #endregion

        #region tvTable_FocusedNodeChanged
        /// <summary>
        /// 树操作中
        /// </summary>
        bool isTreeDoing { get; set; }
        /// <summary>
        /// tvTable_FocusedNodeChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvTable_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (isInit) return;
            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                LoadTable(e.Node);
            }
            finally
            {
                Viewer.ValueChanged = false;
                isTreeDoing = false;
            }
        }
        #endregion

        #region LoadDataSource
        /// <summary>
        /// LoadDataSource
        /// </summary>
        void LoadDataSource(bool isFind)
        {
            this.isInit = true;
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                DataTable dt = proxy.Service.SelectFullTable(new EntityEmrTableBasicInfo());
                DataSourceTable = EntityTools.ConvertToEntityList<EntityEmrTableBasicInfo>(dt);
            }
            if (DataSourceTable == null) DataSourceTable = new List<EntityEmrTableBasicInfo>();
            foreach (EntityEmrTableBasicInfo item in DataSourceTable)
            {
                if (string.IsNullOrEmpty(item.pyCode)) item.pyCode = SpellCodeHelper.GetPyCode(item.tableName);
                if (string.IsNullOrEmpty(item.wbCode)) item.wbCode = SpellCodeHelper.GetWbCode(item.tableName);
                item.imageIndex = 1;
                item.parent = "99";
                item.isLeaf = true;
            }
            EntityEmrTableBasicInfo defaultVo = null;
            if (DataSourceTable.Count > 0 && defaultVo == null) defaultVo = DataSourceTable[0];
            EntityEmrTableBasicInfo vo = new EntityEmrTableBasicInfo();
            vo.tableCode = "99";
            vo.tableName = "全部表格";
            vo.imageIndex = 2;
            DataSourceTable.Add(vo);

            Viewer.tvTable.BeginUpdate();
            Viewer.tvTable.DataSource = DataSourceTable;
            Viewer.tvTable.ExpandAll();
            Viewer.tvTable.EndUpdate();
            if (defaultVo != null && isFind == false) LoadTable(defaultVo);
            Viewer.ValueChanged = false;
            this.isInit = false;
        }
        #endregion

        #region 加载表格
        /// <summary>
        /// 加载表格
        /// </summary>      
        void LoadTable(TreeListNode node)
        {
            EntityEmrTableBasicInfo tableVo = (EntityEmrTableBasicInfo)Viewer.tvTable.GetDataRecordByNode(node);
            LoadTable(tableVo);
        }
        /// <summary>
        /// LoadForm
        /// </summary>
        /// <param name="tableVo"></param>
        void LoadTable(EntityEmrTableBasicInfo tableVo)
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                Viewer.txtTableCode.Tag = tableVo;
                Viewer.txtTableCode.Text = tableVo.tableCode;
                Viewer.txtTableName.Text = tableVo.tableName;
                Viewer.txtSortFieldName.Text = tableVo.sortFieldName;
                Viewer.txtRows.Text = Function.Int(tableVo.displayRows).ToString();
                Viewer.txtHeadWidth.Text = tableVo.headerWidth;
                Viewer.txtRowHeight.Text = Function.Dec(tableVo.rowHeight).ToString();
                if (Function.Int(tableVo.displayType) == 1)
                {
                    Viewer.chkTableStyle2.Checked = true;
                }
                else
                {
                    Viewer.chkTableStyle1.Checked = true;
                }
                if (Function.Int(tableVo.tableHeaderDisplay) == 1)
                {
                    Viewer.chkHeadStyle2.Checked = true;
                }
                else
                {
                    Viewer.chkHeadStyle1.Checked = true;
                }
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    this.gvDataBindingSource.DataSource = proxy.Service.GetTableFieldInfo(tableVo.tableCode);
                }
                Viewer.ValueChanged = false;
            }
            catch (System.Exception e)
            {
                DialogBox.Msg(e.Message);
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region 查找表单
        /// <summary>
        /// 查找表单
        /// </summary>
        /// <param name="val"></param>
        internal void FindTable(string val, bool isNext)
        {
            if (string.IsNullOrEmpty(val)) return;
            bool isFind = false;
            EntityEmrTableBasicInfo tableVo = null;
            for (int i = this.findIndex; i < Viewer.tvTable.AllNodesCount; i++)
            {
                tableVo = (EntityEmrTableBasicInfo)Viewer.tvTable.GetDataRecordByNode(Viewer.tvTable.GetNodeByVisibleIndex(i));
                if (tableVo.isLeaf)
                {
                    if (tableVo.pyCode.StartsWith(val) || tableVo.wbCode.StartsWith(val) || tableVo.tableName.StartsWith(val) ||
                        tableVo.tableCode.Equals(val))
                    {
                        this.findIndex = i + 1;
                        Viewer.tvTable.SetFocusedNode(Viewer.tvTable.GetNodeByVisibleIndex(i));
                        isFind = true;
                        break;
                    }
                }
            }
            if (isFind)
            {
                if (isNext)
                {
                    if (this.findIndex < Viewer.tvTable.AllNodesCount && DialogBox.Msg("是否继续查找？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FindTable(val, true);
                    }
                }
            }
            else
            {
                if (this.findIndex == 0)
                {
                    DialogBox.Msg("没有找到匹配项。");
                }
                else
                {
                    DialogBox.Msg("已找到末尾.");
                }
            }
        }
        #endregion

        #region Reset
        /// <summary>
        /// Reset
        /// </summary>
        internal void Reset()
        {
            Viewer.txtTableCode.Tag = null;
            Viewer.txtTableCode.Text = string.Empty;
            Viewer.txtTableName.Text = string.Empty;
            Viewer.txtSortFieldName.Text = string.Empty;
            Viewer.txtRows.Text = string.Empty;
            Viewer.txtHeadWidth.Text = string.Empty;
            Viewer.txtRowHeight.Text = string.Empty;
            Viewer.chkTableStyle1.Checked = false;
            Viewer.chkTableStyle2.Checked = false;
            Viewer.chkHeadStyle1.Checked = false;
            Viewer.chkHeadStyle2.Checked = false;
            InitTableField();
            Viewer.ValueChanged = false;
        }
        #endregion

        #region 新建

        #region 新建.主记录
        /// <summary>
        /// 新建.主记录
        /// </summary>
        internal void NewTable()
        {
            Reset();
            Viewer.txtTableCode.Focus();
        }
        #endregion

        #region 新建.字段记录
        /// <summary>
        /// 新建.字段记录
        /// </summary>
        internal void NewTableRow()
        {
            AppendRow(this.gvDataBindingSource);
        }
        #endregion

        #endregion

        #region 删除

        #region 删除.主记录
        /// <summary>
        /// 删除.主记录
        /// </summary>
        internal void DelTable()
        {
            if (Viewer.txtTableCode.Tag == null)
            {
                NewTable();
            }
            else
            {
                EntityEmrTableBasicInfo tableVo = Viewer.txtTableCode.Tag as EntityEmrTableBasicInfo;
                if (tableVo != null && !string.IsNullOrEmpty(tableVo.tableCode))
                {
                    if (DialogBox.Msg("是否删除表格资料？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ProxyFormDesign proxy = new ProxyFormDesign();
                        int ret = proxy.Service.DeleteTableInfo(tableVo.tableCode);
                        proxy = null;
                        if (ret > 0)
                        {
                            // 刷新树
                            DataSourceTable.Remove(tableVo);
                            Viewer.tvTable.Refresh();
                            if (Viewer.tvTable.AllNodesCount > 1)
                            {
                                LoadTable(Viewer.tvTable.GetNodeByVisibleIndex(1));
                            }
                            else
                            {
                                NewTable();
                            }
                            Viewer.ValueChanged = false;
                            DialogBox.Msg("删除成功！");
                        }
                        else
                        {
                            DialogBox.Msg("删除失败。");
                        }
                    }

                }
            }
        }
        #endregion

        #region 删除.字段记录
        /// <summary>
        /// 删除.字段记录
        /// </summary>
        internal void DelTableRow()
        {
            DeleteRow(Viewer.gvTable, this.gvDataBindingSource, Viewer.gvTable.FocusedRowHandle);
        }
        #endregion

        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="isExit"></param>
        /// <returns></returns>
        internal bool Save(bool isExit)
        {
            // 1.mainInfo
            bool isRefresh = true;
            EntityEmrTableBasicInfo tableVo = new EntityEmrTableBasicInfo();
            EntityEmrTableBasicInfo tmpVo = null;
            if (Viewer.txtTableCode.Tag != null)
            {
                tmpVo = Viewer.txtTableCode.Tag as EntityEmrTableBasicInfo;
                tableVo.origTableCode = tmpVo.tableCode;
            }
            tableVo.tableCode = Viewer.txtTableCode.Text.Trim();
            tableVo.tableName = Viewer.txtTableName.Text.Trim();
            tableVo.sortFieldName = Viewer.txtSortFieldName.Text.Trim();
            tableVo.displayRows = Function.Int(Viewer.txtRows.Text);
            tableVo.headerWidth = Viewer.txtHeadWidth.Text.Trim();
            tableVo.rowHeight = Function.Dec(Viewer.txtRowHeight.Text);
            tableVo.displayType = (Viewer.chkTableStyle2.Checked ? 1 : 0);
            tableVo.tableHeaderDisplay = (Viewer.chkHeadStyle2.Checked ? 1 : 0);
            if (string.IsNullOrEmpty(tableVo.tableCode))
            {
                DialogBox.Msg("请输入表格代码。");
                Viewer.txtTableCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(tableVo.tableName))
            {
                DialogBox.Msg("请输入表格名称。");
                Viewer.txtTableName.Focus();
                return false;
            }
            if (tableVo.displayRows.Value <= 0)
            {
                DialogBox.Msg("请输入表格行数。");
                Viewer.txtRows.Focus();
                return false;
            }
            tableVo.pyCode = SpellCodeHelper.GetPyCode(tableVo.tableName);
            tableVo.wbCode = SpellCodeHelper.GetWbCode(tableVo.tableName);
            if (tmpVo != null && tmpVo.tableCode == tableVo.tableCode && tmpVo.tableName == tableVo.tableName)
            {
                isRefresh = false;
            }

            // 2.fieldInfo
            Viewer.gvTable.CloseEditor();
            List<EntityEmrTableFieldInfo> lstTableField = this.gvDataBindingSource.DataSource as List<EntityEmrTableFieldInfo>;
            if (lstTableField != null)
            {
                for (int i = lstTableField.Count - 1; i >= 0; i--)
                {
                    if (string.IsNullOrEmpty(lstTableField[i].bandName) && string.IsNullOrEmpty(lstTableField[i].fieldName))
                    {
                        lstTableField.RemoveAt(i);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(lstTableField[i].bandName))
                        {
                            DialogBox.Msg(string.Format("第{0}行 分组名称不能为空,请输入.", Convert.ToString(i + 1)));
                            return false;
                        }
                        if (string.IsNullOrEmpty(lstTableField[i].fieldName))
                        {
                            DialogBox.Msg(string.Format("第{0}行 字段名称不能为空,请输入.", Convert.ToString(i + 1)));
                            return false;
                        }
                        lstTableField[i].tableCode = tableVo.tableCode;
                    }
                }
            }
            if (lstTableField != null && lstTableField.Count > 0)
            {
                //检查是否有两个以上相同字段
                foreach (EntityEmrTableFieldInfo item in lstTableField)
                {
                    if (lstTableField.Count(t => t.fieldName == item.fieldName) > 1)
                    {
                        DialogBox.Msg(string.Format("存在多个{0}字段", item.fieldName));
                        return false;
                    }
                }
            }

            int ret = 0;
            using (ProxyFormDesign proxy = new ProxyFormDesign())
            {
                // 保存前检查
                if (string.IsNullOrEmpty(tableVo.origTableCode) || (!string.IsNullOrEmpty(tableVo.origTableCode) && tableVo.origTableCode != tableVo.tableCode))
                {
                    if (proxy.Service.IsExistsTableCodeOrName(tableVo.tableCode, tableVo.tableName))
                    {
                        DialogBox.Msg("表格代码、名称已经存在，请重新输入。");
                        return false;
                    }
                }
                ret = proxy.Service.SaveTableInfo(tableVo, lstTableField);
            }
            if (ret > 0)
            {
                Viewer.ValueChanged = false;
                // 刷新树 
                if (!isExit)
                {
                    if (isRefresh)
                    {
                        LoadDataSource(true);
                        FindTable(tableVo.tableCode, false);
                    }
                    tableVo.origTableCode = string.Empty;
                    Viewer.txtTableCode.Tag = tableVo;
                }
                DialogBox.Msg("保存成功！");
                return true;
            }
            else
            {
                DialogBox.Msg("保存失败。");
            }
            return false;
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        internal void Refresh()
        {
            if (Viewer.txtTableCode.Tag != null)
            {
                EntityEmrTableBasicInfo tableVo = Viewer.txtTableCode.Tag as EntityEmrTableBasicInfo;
                if (tableVo != null) LoadTable(tableVo);
            }
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        internal void Print()
        {
            uiHelper.Print(Viewer.gcTable);
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        internal void Export()
        {
            uiHelper.ExportToXls(Viewer.gvTable);
        }
        #endregion

        #region SetConfig
        /// <summary>
        /// SetConfig
        /// </summary>
        internal void SetConfig(object sender)
        {
            if (frmConfig == null || frmConfig.IsDisposed)
            {
                frmConfig = new frmContent();
            }
            frmConfig.DataConfig = ((ButtonEdit)sender).Text;
            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                ((ButtonEdit)sender).Text = frmConfig.DataConfig;
                Viewer.gvTable.CloseEditor();
            }
        }
        #endregion

        #region ShowHelp
        /// <summary>
        /// ShowHelp
        /// </summary>
        internal void ShowHelp()
        {
            StringBuilder sf = new StringBuilder();
            sf.Append("[文本]\r\n");
            sf.Append("数据类型: 使用文本框控件编辑\r\n");
            sf.Append("数据定义: 无作用\r\n");

            sf.Append("\r\n");
            sf.Append("[数字]\r\n");
            sf.Append("数据类型: 使用文本框控件编辑\r\n");
            sf.Append("数据定义: 可以使用逗号隔开的两个数字标识允许的最大值和最小值,如\"1,100\"\r\n");

            sf.Append("\r\n");
            sf.Append("[日期]\r\n");
            sf.Append("数据类型: 使用日期控件编辑\r\n");
            sf.Append("数据定义: 可以使用字符串描述编辑时使用的日期格式,如\"yyyy年MM月dd日\"\r\n");

            sf.Append("\r\n");
            sf.Append("[是否]\r\n");
            sf.Append("数据类型: 使用复选按钮控件编辑\r\n");
            sf.Append("数据定义: 可以使用字符串描述复选按钮控件的文本值\r\n");

            sf.Append("\r\n");
            sf.Append("[枚举]\r\n");
            sf.Append("数据类型: 使用下拉控件编辑\r\n");
            sf.Append("数据定义: 可以使用逗号隔开的字符串描述下拉选项,如\"男,女,未知\"\r\n");

            sf.Append("\r\n");
            sf.Append("[签名]\r\n");
            sf.Append("数据类型: 使用签名控件编辑\r\n");
            sf.Append("数据定义: 可以使用字符串描述签名控件的标题,如\"上级医师:\"\r\n");

            sf.Append("\r\n");
            sf.Append("[病历]\r\n");
            sf.Append("数据类型: 使用病历书写控件编辑\r\n");
            sf.Append("数据定义:  可以像[数字]类型一样设置上下限,设定后限制输入指定区间的数字,不设定不限制\r\n");

            sf.Append("\r\n");
            sf.Append("[字典]\r\n");
            sf.Append("数据类型: 使用指定的字典控件进行编辑\r\n");
            sf.Append("数据定义: 不能为空,必须使用字符串描述控件的名称\r\n");

            sf.Append("\r\n");
            sf.Append("[目前已支持的字典控件] \r\n");
            sf.Append("\"ctlTreeSelect_Employee\": 职工\r\n");
            sf.Append("\"ctlTreeSelect_ICD\": ICD10码\r\n");

            sf.Append("\r\n");
            sf.Append("[求和]\r\n");
            sf.Append("数据类型: 使用文本框控件显示数据(只读)\r\n");
            sf.Append("数据定义: 不能为空,使用逗号隔开需要求和的字段名(求和字段只能是[数字]或[病历]类型)\r\n");

            sf.Append("\r\n");
            sf.Append("[允许空] \r\n");
            sf.Append("作用: 保存表格数据时会对不允许为空的列校验并提示\r\n");
            sf.Append("例外: 数据类型为[是否]的列,此属性无效\r\n");

            sf.Append("\r\n");
            sf.Append("[下划线] \r\n");
            sf.Append("作用: 编辑控件是否显示下划线\r\n");

            sf.Append("\r\n");
            sf.Append("[修改] \r\n");
            sf.Append("作用: 已经保存的数据行,此列内容是否可修改\r\n");

            sf.Append("\r\n");
            sf.Append("[小技巧] \r\n");
            sf.Append("1: 表头中同时显示分组头和列头时,可通过设置某一分组下的所有字段标题与分组名相同,实现此分组头的合并显示效果\r\n");

            frmBasePopup frmHelp = new frmBasePopup();
            frmHelp.Text = "帮助";
            frmHelp.Size = new Size(500, 700);
            frmHelp.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmHelp.MinimizeBox = false;
            frmHelp.MaximizeBox = false;
            frmHelp.StartPosition = FormStartPosition.CenterParent;
            frmHelp.ShowInTaskbar = false;
            MemoEdit memo = new MemoEdit();
            frmHelp.pcBackGround.Visible = false;
            frmHelp.Controls.Add(memo);
            memo.BackColor = Color.White;
            memo.Dock = DockStyle.Fill;
            memo.Properties.ReadOnly = true;
            memo.Text = sf.ToString();
            memo.Select(memo.Text.Length, 0);
            frmHelp.ShowDialog(Viewer);
        }
        #endregion

        #region SetCheck
        /// <summary>
        /// SetCheck
        /// </summary>
        /// <param name="typeId"></param>
        internal void SetCheck(int typeId)
        {
            if (isChecking) return;
            isChecking = true;
            switch (typeId)
            {
                case 1:
                    Viewer.chkTableStyle1.Checked = true;
                    Viewer.chkTableStyle2.Checked = false;
                    break;
                case 2:
                    Viewer.chkTableStyle1.Checked = false;
                    Viewer.chkTableStyle2.Checked = true;
                    break;
                case 3:
                    Viewer.chkHeadStyle1.Checked = true;
                    Viewer.chkHeadStyle2.Checked = false;
                    break;
                case 4:
                    Viewer.chkHeadStyle1.Checked = false;
                    Viewer.chkHeadStyle2.Checked = true;
                    break;
                default:
                    break;
            }
            isChecking = false;
        }
        #endregion

        #endregion

    }
}
