using Common.Entity;
using Common.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 打印模板管理
    /// </summary>
    public class ctlPrintTemplate : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmPrintTemplate Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmPrintTemplate)child;
            this.gvDataBindingSource = new BindingSource();
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// isInit
        /// </summary>
        bool isInit { get; set; }

        /// <summary>
        /// isChecking
        /// </summary>
        bool isChecking { get; set; }

        /// <summary>
        /// isLoading
        /// </summary>
        bool isLoading { get; set; }

        /// <summary>
        /// 表格.数据源
        /// </summary>
        List<EntityEmrPrintTemplate> DataSourcePrtTemplate { get; set; }

        /// <summary>
        /// 查找索引
        /// </summary>
        internal int findIndex { get; set; }

        /// <summary>
        /// XtraReport
        /// </summary>
        XtraReport xr { get; set; }

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
                SetEditValueChangedEvent(Viewer.plTop);
                CreateTree();
                LoadDataSource(false);
            }
            finally
            {
                isInit = false;
            }
        }
        #endregion

        #region CreateTree
        /// <summary>
        /// CreateTree
        /// </summary>
        void CreateTree()
        {
            // 树结构
            Viewer.tvTemplate.Columns.Clear();
            uiHelper.SetGridCol(Viewer.tvTemplate, new string[] { "templateName" }, new string[] { "模板列表" }, new int[] { 200 });
            Viewer.tvTemplate.Columns["templateName"].AppearanceCell.Font = new Font("宋体", 9);
            Viewer.tvTemplate.KeyFieldName = "templateId";
            Viewer.tvTemplate.ParentFieldName = "parent";
            Viewer.tvTemplate.ImageIndexFieldName = "imageIndex";

            Viewer.tvTemplate.OptionsView.ShowFocusedFrame = false;
            //Viewer.tvTemplate.OptionsView.ShowVertLines = true;
            Viewer.tvTemplate.Appearance.FocusedRow.Options.UseBackColor = true;
            Viewer.tvTemplate.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            Viewer.tvTemplate.Appearance.FocusedRow.BackColor2 = Color.White;
            Viewer.tvTemplate.Appearance.HideSelectionRow.Options.UseBackColor = true;
            Viewer.tvTemplate.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            Viewer.tvTemplate.Appearance.HideSelectionRow.BackColor2 = Color.White;

            Viewer.tvTemplate.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(tvTemplate_FocusedNodeChanged);

        }
        #endregion

        #region tvTemplate_FocusedNodeChanged
        /// <summary>
        /// 树操作中
        /// </summary>
        bool isTreeDoing { get; set; }
        /// <summary>
        /// tvTemplate_FocusedNodeChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvTemplate_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (isInit) return;
            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                LoadTemplate(e.Node);
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
        internal void LoadDataSource(bool isFind)
        {
            this.isInit = true;
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                DataTable dt = proxy.Service.SelectFullTable(new EntityEmrPrintTemplate());
                DataSourcePrtTemplate = EntityTools.ConvertToEntityList<EntityEmrPrintTemplate>(dt);
            }
            if (DataSourcePrtTemplate == null) DataSourcePrtTemplate = new List<EntityEmrPrintTemplate>();
            foreach (EntityEmrPrintTemplate item in DataSourcePrtTemplate)
            {
                if (string.IsNullOrEmpty(item.pyCode)) item.pyCode = SpellCodeHelper.GetPyCode(item.templateName);
                if (string.IsNullOrEmpty(item.wbCode)) item.wbCode = SpellCodeHelper.GetWbCode(item.templateName);
                item.imageIndex = 1;
                //item.parent = "999999999";
                item.isLeaf = true;
            }
            EntityEmrPrintTemplate defaultVo = null;
            if (DataSourcePrtTemplate.Count > 0 && defaultVo == null) defaultVo = DataSourcePrtTemplate[0];
            //EntityEmrPrintTemplate vo = new EntityEmrPrintTemplate();
            //vo.templateId = 999999999;
            //vo.templateName = "全部模板";
            //vo.imageIndex = 2;
            //DataSourcePrtTemplate.Add(vo);

            Viewer.tvTemplate.BeginUpdate();
            Viewer.tvTemplate.DataSource = DataSourcePrtTemplate;
            Viewer.tvTemplate.ExpandAll();
            Viewer.tvTemplate.EndUpdate();
            if (defaultVo != null && isFind == false) LoadTemplate(defaultVo);
            Viewer.ValueChanged = false;
            this.isInit = false;
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            EntityEmrPrintTemplate vo = null;
            LoadTemplate(vo);
            Viewer.txtTemplateCode.Focus();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal void Delete()
        {
            EntityEmrPrintTemplate templateVo = Viewer.txtTemplateCode.Tag as EntityEmrPrintTemplate;
            if (templateVo == null || templateVo.templateId == 0) return;
            if (DialogBox.Msg("是否删除当前打印模板？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    int ret = proxy.Service.DelFormPrintTemplate((int)templateVo.templateId);
                    if (ret > 0)
                    {
                        New();
                        Viewer.tvTemplate.Nodes.Remove(Viewer.tvTemplate.FocusedNode);
                        DialogBox.Msg("删除成功！");
                    }
                    else
                    {
                        DialogBox.Msg("删除失败。");
                    }
                }
            }
        }
        #endregion

        #region 加载模板
        /// <summary>
        /// 加载模板
        /// </summary>      
        void LoadTemplate(TreeListNode node)
        {
            if (IsSaving) return;
            EntityEmrPrintTemplate templateVo = (EntityEmrPrintTemplate)Viewer.tvTemplate.GetDataRecordByNode(node);
            LoadTemplate(templateVo);
        }
        /// <summary>
        /// LoadTemplate
        /// </summary>
        /// <param name="tableVo"></param>
        void LoadTemplate(EntityEmrPrintTemplate templateVo)
        {
            if (isLoading || IsSaving) return;
            try
            {
                isLoading = true;
                uiHelper.BeginLoading(Viewer);
                Viewer.txtTemplateCode.Tag = templateVo;
                if (templateVo == null)
                {
                    Viewer.txtTemplateCode.Text = string.Empty;
                    Viewer.txtTemplateName.Text = string.Empty;
                    Viewer.txtTemplateDesc.Text = string.Empty;
                    Viewer.dteUseEndDate.EditValue = null;
                    Viewer.txtTableCols.Text = "0";
                    Viewer.txtTableRows.Text = "0";
                    Viewer.txtVersion.Text = string.Empty; ;
                    Viewer.chkTemplateStyle1.Checked = false;
                    Viewer.chkTemplateStyle2.Checked = false;
                    Viewer.chkTemplateStyle3.Checked = false;

                    Viewer.lstDataCols.ResetText();
                    xr = new XtraReport();
                    this.Viewer.ucPrintControl.PrintingSystem = xr.PrintingSystem;
                    xr.CreateDocument();
                }
                else
                {
                    Viewer.txtTemplateCode.Text = templateVo.templateCode;
                    Viewer.txtTemplateName.Text = templateVo.templateName;
                    Viewer.txtTemplateDesc.Text = templateVo.templateRemark;
                    Viewer.dteUseEndDate.EditValue = templateVo.useEndDate;
                    Viewer.txtTableCols.Text = templateVo.acrossCols == null ? "0" : templateVo.acrossCols.Value.ToString();
                    Viewer.txtTableRows.Text = templateVo.vrows == null ? "0" : templateVo.vrows.Value.ToString();
                    Viewer.txtVersion.Text = templateVo.templateVersion.ToString();

                    if (Function.Int(templateVo.tableType) == 0)
                    {
                        Viewer.chkTemplateStyle1.Checked = true;
                    }
                    else if (Function.Int(templateVo.tableType) == 1)
                    {
                        Viewer.chkTemplateStyle2.Checked = true;
                    }
                    else if (Function.Int(templateVo.tableType) == 2)
                    {
                        Viewer.chkTemplateStyle3.Checked = true;
                    }

                    #region 数据列
                    Viewer.lstDataCols.ResetText();
                    if (templateVo.templateColumns != null && templateVo.templateColumns.Length > 0)
                    {
                        string colStr = System.Text.Encoding.Default.GetString(templateVo.templateColumns);
                        char[] sep = new char[] { '\r', '\n' };
                        string[] cols = colStr.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        string[] currCol = null;
                        string fieldName = null;
                        string displayName = null;
                        StringBuilder sb = new StringBuilder();
                        foreach (string col in cols)
                        {
                            sb.AppendLine(col);
                        }
                        Viewer.lstDataCols.Text = sb.ToString();
                    }
                    #endregion

                    xr = new XtraReport();
                    if (templateVo.templateFile != null && templateVo.templateFile.Length > 0)
                    {
                        MemoryStream stream = new MemoryStream(templateVo.templateFile);
                        xr.LoadLayout(stream);
                        xr.DataSource = GetDataSource();
                    }
                    this.Viewer.ucPrintControl.PrintingSystem = xr.PrintingSystem;
                    xr.CreateDocument();
                }
                Viewer.ValueChanged = false;
            }
            catch (System.Exception e)
            {
                DialogBox.Msg(e.Message);
            }
            finally
            {
                isLoading = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="isExits"></param>
        /// <returns></returns>
        internal bool Save(bool isExits)
        {
            if (IsSaving) return false;
            IsSaving = true;
            try
            {
                bool isNew = false;
                EntityEmrPrintTemplate templateVo = null;
                if (Viewer.txtTemplateCode.Tag == null)
                {
                    templateVo = new EntityEmrPrintTemplate();
                    templateVo.templatCreator = GlobalLogin.objLogin.EmpNo;
                    templateVo.templateDate = Common.Utils.Utils.ServerTime();
                    isNew = true;
                }
                else
                {
                    templateVo = Viewer.txtTemplateCode.Tag as EntityEmrPrintTemplate;
                }
                templateVo.templateCode = Viewer.txtTemplateCode.Text.Trim();
                if (templateVo.templateCode == string.Empty)
                {
                    DialogBox.Msg("请输入模板编码。");
                    Viewer.txtTemplateCode.Focus();
                    return false;
                }
                templateVo.templateName = Viewer.txtTemplateName.Text.Trim();
                if (templateVo.templateName == string.Empty)
                {
                    DialogBox.Msg("请输入模板名称。");
                    Viewer.txtTemplateName.Focus();
                    return false;
                }
                templateVo.templateRemark = Viewer.txtTemplateDesc.Text.Trim();
                templateVo.useEndDate = Function.Datetimenull(Viewer.dteUseEndDate.EditValue);
                templateVo.acrossCols = Function.Decnull(Viewer.txtTableCols.Text);
                templateVo.vrows = Function.Decnull(Viewer.txtTableRows.Text);
                templateVo.templateVersion = Function.Int(Viewer.txtVersion.Text);
                if (Viewer.chkTemplateStyle1.Checked) templateVo.tableType = 0;
                else if (Viewer.chkTemplateStyle2.Checked) templateVo.tableType = 1;
                else if (Viewer.chkTemplateStyle3.Checked) templateVo.tableType = 2;
                templateVo.pyCode = SpellCodeHelper.GetPyCode(templateVo.templateName);
                templateVo.wbCode = SpellCodeHelper.GetWbCode(templateVo.templateName);
                templateVo.status = 1;

                templateVo.templateColumns = System.Text.Encoding.Default.GetBytes(Viewer.lstDataCols.Text);
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    int templateId = 0;
                    if (proxy.Service.SaveFormPrintTemplate(templateVo, out templateId) > 0)
                    {
                        Viewer.txtTemplateCode.Tag = templateVo;
                        if (isNew)
                        {
                            templateVo.imageIndex = 1;
                            templateVo.isLeaf = true;
                            (Viewer.tvTemplate.DataSource as List<EntityEmrPrintTemplate>).Add(templateVo);
                            Viewer.tvTemplate.RefreshDataSource();
                            //Viewer.tvTemplate.FocusedNode = Viewer.tvTemplate.FindNodeByKeyID(templateId);
                            FindTemplate(templateId.ToString(), true);
                        }
                        else
                        {
                            int index = (Viewer.tvTemplate.DataSource as List<EntityEmrPrintTemplate>).FindIndex(t => t.templateId == templateId);
                            (Viewer.tvTemplate.DataSource as List<EntityEmrPrintTemplate>)[index] = templateVo;
                            Viewer.tvTemplate.RefreshDataSource();
                        }
                        DialogBox.Msg("保存打印模板成功！");
                    }
                    else
                    {
                        DialogBox.Msg("保存打印模板失败。");
                    }
                }
                Viewer.ValueChanged = false;
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
                return false;
            }
            finally
            {
                IsSaving = false;
            }
            return true;
        }
        #endregion

        #region GetDataSource
        /// <summary>
        /// GetDataSource
        /// </summary>
        /// <returns></returns>
        DataTable GetDataSource()
        {
            EntityEmrPrintTemplate rptVo = Viewer.txtTemplateCode.Tag as EntityEmrPrintTemplate;
            if (rptVo == null)
            {
                DialogBox.Msg("请保存报表。");
                return null;
            }
            if (rptVo.templateColumns != null && rptVo.templateColumns.Length > 0)
                return FillFields(System.Text.Encoding.Default.GetString(rptVo.templateColumns));
            else
                return null;
        }
        #endregion

        #region FillFields
        /// <summary>
        /// FillFields
        /// </summary>
        /// <param name="fieldsStr"></param>
        /// <returns></returns>
        DataTable FillFields(string fieldsStr)
        {
            DataTable dtReportFields = new DataTable("可设计字段");
            char[] sep = new char[] { '\r', '\n' };
            string[] cols = fieldsStr.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string[] currCol = null;
            string fieldName = null;
            string displayName = null;
            foreach (string col in cols)
            {
                currCol = col.Split(',');
                fieldName = currCol[0].Trim();
                displayName = currCol[0].Trim();
                if (currCol.Length > 1)
                {
                    displayName = currCol[1];
                }
                if (dtReportFields.Columns.IndexOf(displayName) == -1)
                {
                    try
                    {
                        DataColumn dataCol = new DataColumn();
                        dataCol.ColumnName = displayName;
                        dtReportFields.Columns.Add(dataCol);
                    }
                    catch
                    { }
                }
                else
                {
                    DialogBox.Msg("出现重复列描述:[" + displayName + "],请检查!");
                    return null;
                }
            }
            // 添加基本信息
            dtReportFields.Columns.Add("[" + EnumPatientInfoType.医院名称.ToString() + "]");
            return dtReportFields;
        }
        #endregion

        #region Design
        /// <summary>
        /// Design
        /// </summary>
        internal void Design()
        {
            EntityEmrPrintTemplate rptVo = Viewer.txtTemplateCode.Tag as EntityEmrPrintTemplate;
            if (rptVo == null)
            {
                DialogBox.Msg("请保存打印模板。");
                return;
            }
            if (Viewer.ValueChanged)
            {
                if (this.Save(false) == false)
                {
                    return;
                }
            }
            rptVo.dataSource = GetDataSource();
            using (frmReportDesigner frm = new frmReportDesigner(rptVo))
            {
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    int index = (Viewer.tvTemplate.DataSource as List<EntityEmrPrintTemplate>).FindIndex(t => t.templateId == rptVo.templateId);
                    (Viewer.tvTemplate.DataSource as List<EntityEmrPrintTemplate>)[index] = rptVo;
                    LoadTemplate(rptVo);
                }
            }
        }
        #endregion

        #region LoadFields
        /// <summary>
        /// LoadFields
        /// </summary>
        internal void LoadFields()
        {
            frmSelectFields frm = new frmSelectFields();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.FieldsResult != string.Empty)
                {
                    #region 数据列

                    if (Viewer.lstDataCols.Text.Trim() != string.Empty)
                    {
                        if (DialogBox.Msg("是否清空已选字段？", MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Viewer.lstDataCols.ResetText();
                        }
                    }
                    char[] sep = new char[] { '\r', '\n' };
                    string[] cols = frm.FieldsResult.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sb = new StringBuilder();
                    foreach (string col in cols)
                    {
                        sb.AppendLine(col);
                    }
                    Viewer.lstDataCols.Text += sb.ToString();
                    #endregion
                }
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        internal void Print()
        {
            if (xr != null)
            {
                string dir = Common.Entity.GlobalParm.PrintFileDir;
                if (Directory.Exists(dir))
                {
                    Function.DeleteFolder(dir);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                }
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                dirInfo.Attributes = FileAttributes.Normal;

                xr.PrintingSystem.ExportOptions.Image.ExportMode = DevExpress.XtraPrinting.ImageExportMode.DifferentFiles;
                xr.PrintingSystem.ExportOptions.Image.Format = System.Drawing.Imaging.ImageFormat.Bmp;
                xr.PrintingSystem.ExportToImage(dir + "\\" + Common.Entity.GlobalParm.PrintFileName + ".bmp");

                frmPrintDocumentSimple frm = new frmPrintDocumentSimple(xr);
                frm.ShowDialog();

            }
        }
        #endregion

        #region 查找模板
        /// <summary>
        /// 查找模板
        /// </summary>
        /// <param name="val"></param>
        /// <param name="isPrecise">是否精确定位</param>
        internal void FindTemplate(string val, bool isPrecise)
        {
            if (string.IsNullOrEmpty(val)) return;
            if (this.IsSaving) this.findIndex = 0;
            bool isFind = false;
            EntityEmrPrintTemplate templateVo = null;
            for (int i = this.findIndex; i < Viewer.tvTemplate.AllNodesCount; i++)
            {
                templateVo = (EntityEmrPrintTemplate)Viewer.tvTemplate.GetDataRecordByNode(Viewer.tvTemplate.GetNodeByVisibleIndex(i));
                if (templateVo.isLeaf)
                {
                    if (isPrecise)
                        isFind = (templateVo.templateId.ToString() == val) ? true : false;
                    else
                        isFind = ((templateVo.pyCode.StartsWith(val) || templateVo.wbCode.StartsWith(val) || templateVo.templateName.StartsWith(val) || templateVo.templateCode.Equals(val))) ? true : false;
                    if (isFind)
                    {
                        this.findIndex = i + 1;
                        Viewer.tvTemplate.SetFocusedNode(Viewer.tvTemplate.GetNodeByVisibleIndex(i));
                        LoadTemplate(Viewer.tvTemplate.GetNodeByVisibleIndex(i));
                        break;
                    }
                }
            }
            if (isFind)
            {
                if (isPrecise == false)
                {
                    if (this.findIndex < Viewer.tvTemplate.AllNodesCount && DialogBox.Msg("是否继续查找？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FindTemplate(val, isPrecise);
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

        #endregion

    }
}
