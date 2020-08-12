using Common.Utils;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 打印预览(简单)
    /// </summary>
    public partial class frmSelectFields : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmSelectFields()
        {
            InitializeComponent();
        }
        #endregion

        #region 属性.变量

        #region EntityObjectList
        /// <summary>
        /// EntityObjectList
        /// </summary>
        class EntityObjectList : BaseDataContract
        {
            public string formId { get; set; }
            public string formCode { get; set; }
            public string formName { get; set; }
            public string pyCode { get; set; }
            public string wbCode { get; set; }
            /// <summary>
            /// Columns
            /// </summary>
            public static EnumCols Columns = new EnumCols();

            /// <summary>
            /// EnumCols
            /// </summary>
            public class EnumCols
            {
                public string formId = "formId";
                public string formCode = "formCode";
                public string formName = "formName";
                public string pyCode = "pyCode";
                public string wbCode = "wbCode";
            }
        }
        #endregion

        /// <summary>
        /// 数据源
        /// </summary>
        List<EntityObjectList> DataSourceForm { get; set; }

        /// <summary>
        /// 选择结果
        /// </summary>
        internal string FieldsResult { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        class XmlField
        {
            public int check { get; set; }
            public string itemName { get; set; }
            public string itemCaption { get; set; }
        }

        bool IsAllChecked { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            // 
            DataSourceForm = new List<EntityObjectList>();
            // 
            this.lueForm.Properties.PopupWidth = 280;
            this.lueForm.Properties.PopupHeight = 400;
            this.lueForm.Properties.ValueColumn = EntityObjectList.Columns.formId;
            this.lueForm.Properties.DisplayColumn = EntityObjectList.Columns.formName;
            this.lueForm.Properties.Essential = false;
            this.lueForm.Properties.IsShowColumnHeaders = true;
            this.lueForm.Properties.ColumnWidth.Add(EntityObjectList.Columns.formCode, 80);
            this.lueForm.Properties.ColumnWidth.Add(EntityObjectList.Columns.formName, 200);
            this.lueForm.Properties.ColumnHeaders.Add(EntityObjectList.Columns.formCode, "编码");
            this.lueForm.Properties.ColumnHeaders.Add(EntityObjectList.Columns.formName, "名称");
            this.lueForm.Properties.ShowColumn = EntityObjectList.Columns.formCode + "|" + EntityObjectList.Columns.formName;
            this.lueForm.Properties.IsUseShowColumn = true;
            this.lueForm.Properties.FilterColumn = EntityObjectList.Columns.formCode + "|" + EntityObjectList.Columns.formName + "|" + EntityObjectList.Columns.pyCode + "|" + EntityObjectList.Columns.wbCode;
            // 表单
            using (ProxyFormDesign proxy = new ProxyFormDesign())
            {
                List<EntityFormDesign> data = proxy.Service.GetForm(0, false);
                if (data != null && data.Count > 0)
                {
                    foreach (EntityFormDesign item in data)
                    {
                        DataSourceForm.Add(new EntityObjectList() { formId = "a" + item.Formid.ToString(), formCode = item.Formcode, formName = item.Formname, pyCode = item.Pycode, wbCode = item.Wbcode });
                    }
                }
            }
            // 表格
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                DataTable dt = proxy.Service.SelectFullTable(new EntityEmrTableBasicInfo());
                List<EntityEmrTableBasicInfo> DataSourceTable = EntityTools.ConvertToEntityList<EntityEmrTableBasicInfo>(dt);
                if (DataSourceTable == null) DataSourceTable = new List<EntityEmrTableBasicInfo>();
                foreach (EntityEmrTableBasicInfo item in DataSourceTable)
                {
                    DataSourceForm.Add(new EntityObjectList() { formId = "b" + item.tableCode, formCode = item.tableCode, formName = item.tableName, pyCode = SpellCodeHelper.GetPyCode(item.tableName), wbCode = SpellCodeHelper.GetWbCode(item.tableName) });
                }
            }
            if (DataSourceForm != null && DataSourceForm.Count > 0)
            {
                this.lueForm.Properties.DataSource = DataSourceForm.ToArray();
            }
            this.lueForm.Properties.SetSize();
        }
        #endregion

        #region Filter
        /// <summary>
        /// Filter
        /// </summary>
        void Filter()
        {
            List<XmlField> dataSource = null;
            string layoutXml = string.Empty;
            string formId = this.lueForm.Properties.DBValue;
            List<EntityFormCtrl> formCtrls = null;
            EntityObjectList vo = DataSourceForm.FirstOrDefault(t => t.formId == formId);
            if (formId.StartsWith("a"))
            {
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    EntityFormDesign vo1 = proxy.Service.GetForm(int.Parse(formId.Substring(1)), true)[0];
                    layoutXml = vo1.Layout;
                }
                formCtrls = FormTool.Entities(layoutXml).FindAll(t => t.ItemType == "1" || t.ItemType == "2" || t.ItemType == "6");
                if (formCtrls != null)
                {
                    dataSource = new List<XmlField>();
                    foreach (EntityFormCtrl item in formCtrls)
                    {
                        dataSource.Add(new XmlField() { check = 0, itemName = item.ItemName, itemCaption = item.ItemCaption });
                    }
                }
            }
            else if (formId.StartsWith("b"))
            {
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    List<EntityEmrTableFieldInfo> tabFields = proxy.Service.GetTableFieldInfo(formId.Substring(1));
                    if (tabFields != null && tabFields.Count > 0)
                    {
                        dataSource = new List<XmlField>();
                        foreach (EntityEmrTableFieldInfo item in tabFields)
                        {
                            dataSource.Add(new XmlField() { check = 0, itemName = item.fieldName, itemCaption = item.fieldCaptain });
                        }
                    }
                }
            }
            this.gcFields.DataSource = dataSource;
        }

        #endregion

        #region Check
        /// <summary>
        /// Check
        /// </summary>
        void Check()
        {
            this.FieldsResult = string.Empty;
            List<XmlField> dataSource = this.gcFields.DataSource as List<XmlField>;
            if (dataSource != null && dataSource.Count > 0)
            {
                foreach (XmlField item in dataSource)
                {
                    if (item.check == 1)
                    {
                        this.FieldsResult += item.itemName + "," + item.itemCaption + "\r\n";
                    }
                }
            }
            if (this.FieldsResult != string.Empty)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogBox.Msg("请选择字段。");
            }
        }
        #endregion

        #endregion

        #region 事件

        private void frmSelectFields_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void lueForm_HandleDBValueChanged(object sender)
        {
            this.Filter();
        }

        private void gvFields_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            RepositoryItemCheckEdit CheckItem = new RepositoryItemCheckEdit();
            uiHelper.DrawHeaderCheckBox(gvFields, CheckItem, "check", e);
        }

        private void gvFields_Click(object sender, EventArgs e)
        {
            Point pt = gcFields.PointToClient(Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = gvFields.CalcHitInfo(pt);
            if (info != null && info.Column != null)
            {
                if (!info.InRowCell && info.Column.AbsoluteIndex == 0)//Index判断用于哪一列的列头
                {
                    IsAllChecked = !IsAllChecked;
                    for (int i = 0; i < gvFields.RowCount; i++)
                    {
                        gvFields.SetRowCellValue(i, "check", (IsAllChecked ? 1 : 0));
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Check();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }

    
}
