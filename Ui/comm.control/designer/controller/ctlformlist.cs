using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 表单管理控制类
    /// </summary>
    public class ctlFormList : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmFormList Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmFormList)child;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// 初始化中
        /// </summary>
        bool IsInit { get; set; }

        /// <summary>
        /// 表单数据源
        /// </summary>
        List<EntityFormDesign> DataSourceForm { get; set; }

        /// <summary>
        /// 打印模板数据源
        /// </summary>
        List<EntityEmrPrintTemplate> DataSourcePrintTemplate { get; set; }

        /// <summary>
        /// 查找索引
        /// </summary>
        internal int FindIndex { get; set; }


        #endregion

        #region 方法

        #region SuspendLayoutUc
        /// <summary>
        /// SuspendLayoutUc
        /// </summary>
        void SuspendLayoutUc()
        {
            Function.SuspendLayout(Viewer.plMainInfo.Handle);
        }
        #endregion

        #region ResumeLayoutUc
        /// <summary>
        /// ResumeLayoutUc
        /// </summary>
        void ResumeLayoutUc()
        {
            Function.ResumeLayout(Viewer.plMainInfo.Handle);
            Viewer.plMainInfo.Refresh();
        }
        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                IsInit = true;
                // 11 健康档案; 12 报表
                if (Function.Int(Viewer.FormType) <= 0) Viewer.FormType = "12";
                string caption = string.Empty;
                switch (Viewer.FormType)
                {
                    case "1":
                        caption = "电子病历";
                        break;
                    case "11":
                        caption = "健康档案";
                        break;
                    case "12":
                        caption = "报表";
                        break;
                    case "13":
                        caption = "病理模板";
                        break;
                    default:
                        break;
                }
                Viewer.Text += " ... " + caption;
                Viewer.cboType.Properties.Items.Add(caption);
                #region init.lue
                DataSourcePrintTemplate = new List<EntityEmrPrintTemplate>();
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    DataTable dt = proxy.Service.SelectFullTable(new EntityEmrPrintTemplate());
                    if (dt != null && dt.Rows.Count > 0)
                        DataSourcePrintTemplate = EntityTools.ConvertToEntityList<EntityEmrPrintTemplate>(dt);
                }
                foreach (EntityEmrPrintTemplate item in DataSourcePrintTemplate)
                {
                    if (string.IsNullOrEmpty(item.pyCode)) item.pyCode = SpellCodeHelper.GetPyCode(item.templateName);
                    if (string.IsNullOrEmpty(item.wbCode)) item.wbCode = SpellCodeHelper.GetWbCode(item.templateName);
                }
                Viewer.luePrint.Properties.PopupWidth = 400;
                Viewer.luePrint.Properties.PopupHeight = 400;
                Viewer.luePrint.Properties.ValueColumn = EntityEmrPrintTemplate.Columns.templateId;
                Viewer.luePrint.Properties.DisplayColumn = EntityEmrPrintTemplate.Columns.templateName;
                Viewer.luePrint.Properties.Essential = false;
                Viewer.luePrint.Properties.IsShowColumnHeaders = true;
                Viewer.luePrint.Properties.ColumnWidth.Add(EntityEmrPrintTemplate.Columns.templateCode, 130);
                Viewer.luePrint.Properties.ColumnWidth.Add(EntityEmrPrintTemplate.Columns.templateName, 270);
                Viewer.luePrint.Properties.ColumnHeaders.Add(EntityEmrPrintTemplate.Columns.templateCode, "编码");
                Viewer.luePrint.Properties.ColumnHeaders.Add(EntityEmrPrintTemplate.Columns.templateName, "名称");
                Viewer.luePrint.Properties.ShowColumn = EntityEmrPrintTemplate.Columns.templateCode + "|" + EntityEmrPrintTemplate.Columns.templateName;
                Viewer.luePrint.Properties.IsUseShowColumn = true;
                //Viewer.luePrint.Properties.FilterColumn = EntityEmrPrintTemplate.Columns.templateCode + "|" + EntityEmrPrintTemplate.Columns.templateName + "|" + EntityEmrPrintTemplate.Columns.pyCode + "|" + EntityEmrPrintTemplate.Columns.wbCode;
                if (DataSourcePrintTemplate != null && DataSourcePrintTemplate.Count > 0) Viewer.luePrint.Properties.DataSource = DataSourcePrintTemplate.ToArray();
                Viewer.luePrint.Properties.SetSize();

                #endregion

                CreateTree();
                LoadDataSource();

                Viewer.rdoStatus.SelectedIndex = 1;
                SetEditValueChangedEvent(Viewer.plMainInfo);
                Viewer.timer.Enabled = true;
                Viewer.ValueChanged = false;
            }
            finally
            {
                IsInit = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region Template

        #region CreateTree
        /// <summary>
        /// CreateTree
        /// </summary>
        void CreateTree()
        {
            // 树结构
            Viewer.tvForm.Columns.Clear();
            uiHelper.SetGridCol(Viewer.tvForm, new string[] { "Formdesc" }, new string[] { "表单列表" }, new int[] { 200 });
            Viewer.tvForm.Columns["Formdesc"].AppearanceCell.Font = new Font("宋体", 9);
            Viewer.tvForm.KeyFieldName = "Formid";
            Viewer.tvForm.ParentFieldName = "parent";
            Viewer.tvForm.ImageIndexFieldName = "imageIndex";

            Viewer.tvForm.OptionsView.ShowFocusedFrame = false;
            //Viewer.tvForm.OptionsView.ShowVertLines = true;
            Viewer.tvForm.Appearance.FocusedRow.Options.UseBackColor = true;
            Viewer.tvForm.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            Viewer.tvForm.Appearance.FocusedRow.BackColor2 = Color.White;
            Viewer.tvForm.Appearance.HideSelectionRow.Options.UseBackColor = true;
            Viewer.tvForm.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            Viewer.tvForm.Appearance.HideSelectionRow.BackColor2 = Color.White;
            if (Viewer.IsLoadModel) Viewer.tvForm.OptionsView.ShowCheckBoxes = true;

            Viewer.tvForm.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(tvForm_FocusedNodeChanged);

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
        private void tvForm_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (IsInit) return;
            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                EntityFormDesign formVoOld = (EntityFormDesign)Viewer.tvForm.GetDataRecordByNode(e.OldNode);
                if (formVoOld.isLeaf)
                {
                    formVoOld.imageIndex = 2;
                }
                LoadForm(e.Node);
            }
            finally
            {
                isTreeDoing = false;
            }
        }
        #endregion

        #region LoadDataSource
        /// <summary>
        /// LoadDataSource
        /// </summary>
        internal void LoadDataSource()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    this.IsInit = true;
                    DataSourceForm = proxy.Service.GetForm(0, false);
                    if (DataSourceForm == null) DataSourceForm = new List<EntityFormDesign>();
                    foreach (EntityFormDesign item in DataSourceForm)
                    {
                        item.imageIndex = 2;
                        item.parent = 99;
                        item.isLeaf = true;
                    }
                    EntityFormDesign defaultVo = null;
                    if (DataSourceForm.Count > 0) defaultVo = DataSourceForm[0];
                    EntityFormDesign vo = new EntityFormDesign();
                    vo.Formid = 99;
                    vo.Formname = "全部表单";
                    vo.imageIndex = 1;
                    DataSourceForm.Add(vo);

                    Viewer.tvForm.BeginUpdate();
                    Viewer.tvForm.DataSource = DataSourceForm;
                    Viewer.tvForm.ExpandAll();
                    Viewer.tvForm.EndUpdate();
                    if (defaultVo != null) SetData(defaultVo);
                    this.IsInit = false;
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #endregion

        #region SetData
        /// <summary>
        /// SetData
        /// </summary>
        /// <param name="formVo"></param>
        internal void SetData(EntityFormDesign formVo)
        {
            if (formVo == null)
            {
                Viewer.txtFormCode.Tag = null;
                Viewer.txtFormCode.Text = string.Empty;
                Viewer.txtFormName.Text = string.Empty;
                Viewer.cboType.SelectedIndex = 0;
                Viewer.rdoStatus.SelectedIndex = 1;
                Viewer.luePrint.Properties.DBValue = string.Empty;
                Viewer.luePrint.Text = string.Empty;
                Viewer.txtCreatDate.Text = string.Empty;
                Viewer.showPanelForm.ClearComponent();
                Viewer.xtraScrollableControl.Tag = 0;
            }
            else
            {
                Viewer.txtFormCode.Tag = formVo;
                Viewer.txtFormCode.Text = formVo.Formcode;
                Viewer.txtFormName.Text = formVo.Formname;
                Viewer.cboType.SelectedIndex = 0;
                Viewer.rdoStatus.SelectedIndex = Function.Int(formVo.Status);
                Viewer.luePrint.Properties.DBValue = formVo.Printtemplateid.ToString();
                if (DataSourcePrintTemplate != null)
                {
                    if (DataSourcePrintTemplate.Any(t => t.templateId == formVo.Printtemplateid))
                    {
                        Viewer.luePrint.Text = DataSourcePrintTemplate.FirstOrDefault(t => t.templateId == formVo.Printtemplateid).templateName;
                    }
                    else
                    {
                        Viewer.luePrint.Text = string.Empty;
                    }
                }
                Viewer.txtCreatDate.Text = formVo.Recorddate.ToString("yyyy-MM-dd HH:mm");
                LoadForm(formVo.Formid);
            }
            Viewer.ValueChanged = false;
        }
        #endregion

        #region 加载模板
        /// <summary>
        /// 加载模板
        /// </summary>      
        void LoadForm(TreeListNode node)
        {
            EntityFormDesign formVo = (EntityFormDesign)Viewer.tvForm.GetDataRecordByNode(node);
            if (formVo.isLeaf)
            {
                formVo.imageIndex = 3;
                SetData(formVo);
            }
        }

        /// <summary>
        /// formId
        /// </summary>
        /// <param name="formId"></param>
        void LoadForm(decimal formId)
        {
            try
            {
                Viewer.xtraScrollableControl.SuspendLayout();
                Viewer.showPanelForm.ClearComponent();
                if (formId <= 0) return;
                uiHelper.BeginLoading(Viewer);
                Viewer.showPanelForm.Formid = (int)formId;
                Viewer.showPanelForm.InitComponent();
            }
            catch (System.Exception e)
            {
                DialogBox.Msg(e.Message);
            }
            finally
            {
                Viewer.xtraScrollableControl.ResumeLayout();
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            SetData(null);
            Viewer.txtFormCode.Focus();
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="isExsit"></param>
        /// <returns></returns>
        internal bool Save(bool isExsit)
        {
            EntityFormDesign formVoOri = null;
            EntityFormDesign formVo = new EntityFormDesign();
            bool isNew = false;
            if (Viewer.txtFormCode.Tag != null)
            {
                formVoOri = Viewer.txtFormCode.Tag as EntityFormDesign;
                formVo.Formid = formVoOri.Formid;
                formVo.Version = formVoOri.Version;
                formVo.Panelsize = formVoOri.Panelsize;
                if (string.IsNullOrEmpty(formVoOri.Layout))
                {
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        EntityFormDesign vo1 = proxy.Service.GetForm(formVoOri.Formid, true)[0];
                        formVoOri.Layout = vo1.Layout;
                    }
                }
                formVo.Layout = formVoOri.Layout;
                formVo.Recorderid = formVoOri.Recorderid;
                formVo.Recorddate = formVoOri.Recorddate;
            }
            else
            {
                formVo.Version = 1;
                formVo.Panelsize = "100|100";
                formVo.Layout = "<XmlData></XmlData>";
                formVo.Recorderid = GlobalLogin.objLogin.EmpNo;
                formVo.Recorddate = Common.Utils.Utils.ServerTime();
                isNew = true;
            }
            formVo.Formtype = Function.Int(Viewer.FormType);
            formVo.Formcode = Viewer.txtFormCode.Text.Trim();
            if (string.IsNullOrEmpty(formVo.Formcode))
            {
                DialogBox.Msg("请输入表单编码。");
                Viewer.txtFormCode.Focus();
                return false;
            }
            formVo.Formname = Viewer.txtFormName.Text.Trim();
            if (string.IsNullOrEmpty(formVo.Formname))
            {
                DialogBox.Msg("请输入表单名称。");
                Viewer.txtFormName.Focus();
                return false;
            }
            formVo.Status = Viewer.rdoStatus.SelectedIndex;
            if (!string.IsNullOrEmpty(Viewer.luePrint.Properties.DBValue) && Viewer.luePrint.Properties.DBRow != null)
            {
                formVo.Printtemplateid = Function.Int(Viewer.luePrint.Properties.DBValue);
            }
            formVo.Pycode = SpellCodeHelper.GetPyCode(formVo.Formname);
            formVo.Wbcode = SpellCodeHelper.GetWbCode(formVo.Formname);

            try
            {
                uiHelper.BeginLoading(Viewer);
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    int formId2 = 0;
                    if (proxy.Service.SaveForm(formVo, out formId2) > 0)
                    {
                        formVo.Formid = formId2;
                        formVo.imageIndex = 2;
                        formVo.isLeaf = true;
                        Viewer.txtFormCode.Tag = formVo;
                        try
                        {
                            this.IsSaving = true;
                            if (isNew)
                            {
                                LoadDataSource();
                                FindForm(formId2.ToString(), true);
                            }
                        }
                        finally
                        {
                            this.IsSaving = false;
                        }
                        for (int i = 0; i < DataSourceForm.Count; i++)
                        {
                            if (DataSourceForm[i].Formid == formId2)
                            {
                                DataSourceForm[i] = formVo;
                                break;
                            }
                        }
                        Viewer.ValueChanged = false;
                        DialogBox.Msg("保存成功!!");
                    }
                    else
                    {
                        DialogBox.Msg("保存失败。");
                        return false;
                    }
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
            return true;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal void Delete()
        {
            EntityFormDesign formVo = Viewer.txtFormCode.Tag as EntityFormDesign;
            if (formVo == null || formVo.Formid <= 0)
            {
                New();
                return;
            }
            else
            {
                if (DialogBox.Msg("是否删除当前表单信息？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        if (proxy.Service.DelForm(formVo.Formid, formVo.Version) > 0)
                        {
                            this.New();
                            LoadDataSource();
                            DialogBox.Msg("删除成功！！");
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

        #region Design
        /// <summary>
        /// Design
        /// </summary>
        internal void Design()
        {
            EntityFormDesign formVo = Viewer.txtFormCode.Tag as EntityFormDesign;
            if (formVo == null || formVo.Formid <= 0)
            {
                DialogBox.Msg("请保存表单信息。");
                return;
            }
            try
            {
                SuspendLayoutUc();
                using (frmFormDesign frm = new frmFormDesign())
                {
                    frm.FormType = Function.Int(Viewer.FormType);
                    frm.FormId = formVo.Formid;
                    frm.ShowDialog();
                    if (frm.IsSave || frm.UpdateFlag)
                    {
                        LoadForm(frm.FormId);
                        if (Viewer.txtFormCode.Tag != null)
                        {
                            (Viewer.txtFormCode.Tag as EntityFormDesign).Layout = Viewer.showPanelForm.LayoutXML();
                        }
                    }
                }
            }
            finally
            {
                ResumeLayoutUc();
            }
        }
        #endregion

        #region 查找模板
        /// <summary>
        /// 查找模板
        /// </summary>
        /// <param name="val"></param>
        /// <param name="isPrecise">是否精确定位</param>
        internal void FindForm(string val, bool isPrecise)
        {
            if (string.IsNullOrEmpty(val)) return;
            if (this.IsSaving) this.FindIndex = 0;
            bool isFind = false;
            EntityFormDesign formVo = null;
            for (int i = this.FindIndex; i < Viewer.tvForm.AllNodesCount; i++)
            {
                formVo = (EntityFormDesign)Viewer.tvForm.GetDataRecordByNode(Viewer.tvForm.GetNodeByVisibleIndex(i));
                if (formVo.isLeaf)
                {
                    if (isPrecise)
                        isFind = (formVo.Formid.ToString() == val) ? true : false;
                    else
                        isFind = ((formVo.Pycode.StartsWith(val) || formVo.Wbcode.StartsWith(val) || formVo.Formname.StartsWith(val) || formVo.Formcode.Equals(val))) ? true : false;
                    if (isFind)
                    {
                        this.FindIndex = i + 1;
                        Viewer.tvForm.SetFocusedNode(Viewer.tvForm.GetNodeByVisibleIndex(i));
                        LoadForm(Viewer.tvForm.GetNodeByVisibleIndex(i));
                        break;
                    }
                }
            }
            if (isFind)
            {
                if (isPrecise == false)
                {
                    if (this.FindIndex < Viewer.tvForm.AllNodesCount && DialogBox.Msg("是否继续查找？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FindForm(val, isPrecise);
                    }
                }
            }
            else
            {
                if (this.FindIndex == 0)
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
