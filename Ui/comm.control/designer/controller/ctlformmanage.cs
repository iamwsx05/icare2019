using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 表单设计管理
    /// </summary>
    public class ctlFormManage : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmFormManage Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmFormManage)child;
            this.gvDataBindingSource = new BindingSource();
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// isInit
        /// </summary>
        bool IsInit { get; set; }

        /// <summary>
        /// 查找索引
        /// </summary>
        internal int FindIndex { get; set; }

        /// <summary>
        /// lueFormData
        /// </summary>
        List<EntityFormDesign> lueFormData { get; set; }

        /// <summary>
        /// luePrintData
        /// </summary>
        List<EntityEmrPrintTemplate> luePrintData { get; set; }

        /// <summary>
        /// lueCataDatalog
        /// </summary>
        List<EntityEmrCatalog> lueCataDatalog { get; set; }

        /// <summary>
        /// DataSourceEmrDept
        /// </summary>
        List<EntityEmrDept> DataSourceEmrDept { get; set; }

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
                uiHelper.BeginLoading(Viewer);
                IsInit = true;

                #region Lue

                #region 表单模板
                lueFormData = new List<EntityFormDesign>();
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    lueFormData = proxy.Service.GetForm(0, false);
                }
                Viewer.lueFormTemplate.Properties.PopupWidth = Viewer.lueFormTemplate.Width;
                Viewer.lueFormTemplate.Properties.PopupHeight = 500;
                Viewer.lueFormTemplate.Properties.ValueColumn = EntityFormDesign.Columns.Formid;
                Viewer.lueFormTemplate.Properties.DisplayColumn = EntityFormDesign.Columns.Formname;
                Viewer.lueFormTemplate.Properties.Essential = false;
                Viewer.lueFormTemplate.Properties.IsShowColumnHeaders = true;
                Viewer.lueFormTemplate.Properties.ColumnWidth.Add(EntityFormDesign.Columns.Formcode, 70);
                Viewer.lueFormTemplate.Properties.ColumnWidth.Add(EntityFormDesign.Columns.Formname, 330);
                Viewer.lueFormTemplate.Properties.ColumnHeaders.Add(EntityFormDesign.Columns.Formcode, "编号");
                Viewer.lueFormTemplate.Properties.ColumnHeaders.Add(EntityFormDesign.Columns.Formname, "名称");
                Viewer.lueFormTemplate.Properties.ShowColumn = EntityFormDesign.Columns.Formcode + "|" + EntityFormDesign.Columns.Formname;
                Viewer.lueFormTemplate.Properties.IsUseShowColumn = true;
                Viewer.lueFormTemplate.Properties.FilterColumn = EntityFormDesign.Columns.Formcode + "|" + EntityFormDesign.Columns.Formname + "|" + EntityFormDesign.Columns.Pycode + "|" + EntityFormDesign.Columns.Wbcode;
                if (lueFormData != null) Viewer.lueFormTemplate.Properties.DataSource = lueFormData.ToArray();
                Viewer.lueFormTemplate.Properties.SetSize();

                #endregion

                #region 打印模板
                DataTable dt = null;
                luePrintData = new List<EntityEmrPrintTemplate>();
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    dt = proxy.Service.SelectFullTable(new EntityEmrPrintTemplate());
                    if (dt != null) luePrintData = EntityTools.ConvertToEntityList<EntityEmrPrintTemplate>(dt);
                }
                Viewer.luePrintTemplate.Properties.PopupWidth = Viewer.luePrintTemplate.Width;
                Viewer.luePrintTemplate.Properties.PopupHeight = 500;
                Viewer.luePrintTemplate.Properties.ValueColumn = EntityEmrPrintTemplate.Columns.templateId;
                Viewer.luePrintTemplate.Properties.DisplayColumn = EntityEmrPrintTemplate.Columns.templateName;
                Viewer.luePrintTemplate.Properties.Essential = false;
                Viewer.luePrintTemplate.Properties.IsShowColumnHeaders = true;
                Viewer.luePrintTemplate.Properties.ColumnWidth.Add(EntityEmrPrintTemplate.Columns.templateCode, 70);
                Viewer.luePrintTemplate.Properties.ColumnWidth.Add(EntityEmrPrintTemplate.Columns.templateName, 330);
                Viewer.luePrintTemplate.Properties.ColumnHeaders.Add(EntityEmrPrintTemplate.Columns.templateCode, "编号");
                Viewer.luePrintTemplate.Properties.ColumnHeaders.Add(EntityEmrPrintTemplate.Columns.templateName, "名称");
                Viewer.luePrintTemplate.Properties.ShowColumn = EntityEmrPrintTemplate.Columns.templateCode + "|" + EntityEmrPrintTemplate.Columns.templateName;
                Viewer.luePrintTemplate.Properties.IsUseShowColumn = true;
                Viewer.luePrintTemplate.Properties.FilterColumn = EntityEmrPrintTemplate.Columns.templateCode + "|" + EntityEmrPrintTemplate.Columns.templateName + "|" + EntityEmrPrintTemplate.Columns.pyCode + "|" + EntityEmrPrintTemplate.Columns.wbCode;
                if (luePrintData != null) Viewer.luePrintTemplate.Properties.DataSource = luePrintData.ToArray();
                Viewer.luePrintTemplate.Properties.SetSize();

                #endregion

                #region 病历目录
                lueCataDatalog = new List<EntityEmrCatalog>();
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    dt = proxy.Service.SelectFullTable(new EntityEmrCatalog());
                    if (dt != null) lueCataDatalog = EntityTools.ConvertToEntityList<EntityEmrCatalog>(dt);
                }
                Viewer.lueCaseCata.Properties.PopupWidth = 200;
                Viewer.lueCaseCata.Properties.PopupHeight = 400;
                Viewer.lueCaseCata.Properties.ValueColumn = EntityEmrCatalog.Columns.catalogId;
                Viewer.lueCaseCata.Properties.DisplayColumn = EntityEmrCatalog.Columns.catalogName;
                Viewer.lueCaseCata.Properties.Essential = false;
                Viewer.lueCaseCata.Properties.IsShowColumnHeaders = true;
                Viewer.lueCaseCata.Properties.ColumnWidth.Add(EntityEmrCatalog.Columns.catalogId, 40);
                Viewer.lueCaseCata.Properties.ColumnWidth.Add(EntityEmrCatalog.Columns.catalogName, 120);
                Viewer.lueCaseCata.Properties.ColumnHeaders.Add(EntityEmrCatalog.Columns.catalogId, "编号");
                Viewer.lueCaseCata.Properties.ColumnHeaders.Add(EntityEmrCatalog.Columns.catalogName, "名称");
                Viewer.lueCaseCata.Properties.ShowColumn = EntityEmrCatalog.Columns.catalogId + "|" + EntityEmrCatalog.Columns.catalogName;
                Viewer.lueCaseCata.Properties.IsUseShowColumn = true;
                Viewer.lueCaseCata.Properties.FilterColumn = EntityEmrCatalog.Columns.catalogId + "|" + EntityEmrCatalog.Columns.catalogName;
                if (lueCataDatalog != null) Viewer.lueCaseCata.Properties.DataSource = lueCataDatalog.ToArray();
                Viewer.lueCaseCata.Properties.SetSize();

                #endregion

                #endregion

                InitCatalog();

            }
            finally
            {
                IsInit = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region 加载病历树
        /// <summary>
        /// 加载病历树
        /// </summary>
        void InitCatalog()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                List<EntityEmrCatalog> dataSourceEmrCatalog = null;
                List<EntityEmrBasicInfo> dataSourceEmrBasicInfo = null;
                List<EntityProgressNoteType> dataSourceEmrPnType = null;
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    EntityEmrCatalog vo1 = new EntityEmrCatalog();
                    vo1.caseScope = 2;
                    vo1.status = 1;
                    dataSourceEmrCatalog = EntityTools.ConvertToEntityList<EntityEmrCatalog>(proxy.Service.SelectSort(vo1, new List<string> { EntityEmrCatalog.Columns.caseScope, EntityEmrCatalog.Columns.status }, new List<string> { EntityEmrCatalog.Columns.sortNo }));

                    EntityEmrBasicInfo vo2 = new EntityEmrBasicInfo();
                    vo2.attribute = -1;
                    vo2.showType = -1;
                    dataSourceEmrBasicInfo = EntityTools.ConvertToEntityList<EntityEmrBasicInfo>(proxy.Service.Select(vo2, new List<string> { EntityEmrBasicInfo.Columns.attribute, EntityEmrBasicInfo.Columns.showType }, "<>", new List<string> { EntityEmrBasicInfo.Columns.parentCode, EntityEmrBasicInfo.Columns.sortNo }));

                    EntityProgressNoteType vo3 = new EntityProgressNoteType();
                    vo3.status = 1;
                    dataSourceEmrPnType = EntityTools.ConvertToEntityList<EntityProgressNoteType>(proxy.Service.SelectSort(vo3, new List<string> { EntityProgressNoteType.Columns.status }, new List<string> { EntityProgressNoteType.Columns.sortNo }));
                }
                CreateCatalog(dataSourceEmrCatalog, dataSourceEmrBasicInfo, dataSourceEmrPnType);
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        /// <summary>
        /// CreateCatalog
        /// </summary>
        /// <param name="dataSourceEmrCatalog"></param>
        /// <param name="dataSourceEmrBasicInfo"></param>
        /// <param name="dataSourceEmrPnType"></param>
        /// <param name="caseType"></param>
        void CreateCatalog(List<EntityEmrCatalog> dataSourceEmrCatalog, List<EntityEmrBasicInfo> dataSourceEmrBasicInfo, List<EntityProgressNoteType> dataSourceEmrPnType)
        {
            // 树结构
            Viewer.tvForm.Columns.Clear();
            uiHelper.SetGridCol(Viewer.tvForm, new string[] { "caseName" }, new string[] { "病历列表" }, new int[] { 200 });
            Viewer.tvForm.Columns["caseName"].AppearanceCell.Font = new Font("宋体", 9);
            Viewer.tvForm.KeyFieldName = "caseCode";
            Viewer.tvForm.ParentFieldName = "parentCode";
            Viewer.tvForm.ImageIndexFieldName = "tempSerNo";

            Viewer.tvForm.OptionsView.ShowFocusedFrame = false;
            Viewer.tvForm.Appearance.FocusedRow.Options.UseBackColor = true;
            Viewer.tvForm.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            Viewer.tvForm.Appearance.FocusedRow.BackColor2 = Color.White;
            Viewer.tvForm.Appearance.HideSelectionRow.Options.UseBackColor = true;
            Viewer.tvForm.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            Viewer.tvForm.Appearance.HideSelectionRow.BackColor2 = Color.White;

            EntityEmrBasicInfo emrBasicInfo = null;
            List<decimal> lstCatalogID = new List<decimal>();
            List<EntityEmrBasicInfo> lstCaseBasicInfo = new List<EntityEmrBasicInfo>();
            // 主目录
            foreach (EntityEmrCatalog catalog in dataSourceEmrCatalog)
            {
                emrBasicInfo = new EntityEmrBasicInfo();
                emrBasicInfo.serNo = -1;
                emrBasicInfo.caseCode = catalog.catalogId.ToString() + "^"; // 避免文件夹的自增序号与casecode相同
                emrBasicInfo.parentCode = string.Empty;
                emrBasicInfo.tableName = string.Empty;
                emrBasicInfo.tempSerNo = 2;
                emrBasicInfo.caseName = catalog.catalogName;
                emrBasicInfo.pyCode = SpellCodeHelper.GetPyCode(catalog.catalogName);
                emrBasicInfo.wbCode = SpellCodeHelper.GetWbCode(catalog.catalogName);

                if (lstCaseBasicInfo.Any(t => t.caseCode == emrBasicInfo.caseCode)) continue;
                if (lstCatalogID.IndexOf(catalog.catalogId) < 0) lstCatalogID.Add(catalog.catalogId);
                if (catalog.type == 0) lstCaseBasicInfo.Add(emrBasicInfo);
                else
                {
                    if (catalog.type == 1)
                    {
                        //if (emrBasicInfo.caseName == "病程记录") emrBasicInfo.caseCode = progressNoteCaseCode;
                        lstCaseBasicInfo.Add(emrBasicInfo);
                    }
                    else if (catalog.type == 2)
                    {
                        lstCaseBasicInfo.Add(emrBasicInfo);
                    }
                }
            }

            List<string> lstExistsCaseCode = new List<string>(); // 记录不需要显示的表单
            List<string> lstProgressCaseCode = new List<string>();
            foreach (EntityEmrBasicInfo caseInfo in dataSourceEmrBasicInfo)
            {
                //if (caseInfo.catalogId != 0 && lstCatalogID.IndexOf(caseInfo.catalogId) < 0) continue;                
                #region doct.Case
                // 公用、医生                
                if (caseInfo.attribute == 0 || caseInfo.attribute == 1)
                {
                    if (lstCaseBasicInfo.Any(t => t.caseCode == caseInfo.caseCode)) continue;
                    if (lstProgressCaseCode.IndexOf(caseInfo.caseCode) >= 0) continue;
                    else
                    {
                        bool blnChk = false;
                        foreach (EntityProgressNoteType pnType in dataSourceEmrPnType)
                        {
                            if (pnType.caseCode == caseInfo.caseCode)
                            {
                                blnChk = true;
                                break;
                            }
                        }
                        if (blnChk) continue;
                    }
                    caseInfo.parentCode = Function.Dec(caseInfo.catalogId).ToString() + "^";
                    caseInfo.tempSerNo = 1;
                    caseInfo.pyCode = SpellCodeHelper.GetPyCode(caseInfo.caseName);
                    caseInfo.wbCode = SpellCodeHelper.GetWbCode(caseInfo.caseName);
                    lstCaseBasicInfo.Add(caseInfo);
                }
                #endregion

                #region nurse.Case
                if (caseInfo.attribute == 0 || caseInfo.attribute == 2)
                {
                    if (lstCaseBasicInfo.Any(t => t.caseCode == caseInfo.caseCode)) continue;
                    caseInfo.parentCode = caseInfo.catalogId.ToString() + "^";
                    caseInfo.tempSerNo = 1;
                    caseInfo.pyCode = SpellCodeHelper.GetPyCode(caseInfo.caseName);
                    caseInfo.wbCode = SpellCodeHelper.GetWbCode(caseInfo.caseName);
                    lstCaseBasicInfo.Add(caseInfo);
                }
                #endregion
            }
            foreach (EntityEmrBasicInfo item in lstCaseBasicInfo)
            {
                if (item.multiPageFlag == 1) item.tempSerNo = 2;
            }
            Viewer.tvForm.BeginUpdate();
            Viewer.tvForm.DataSource = lstCaseBasicInfo;
            //EntityEmrBasicInfo[] tmpArr = new EntityEmrBasicInfo[lstCaseBasicInfo.Count];
            //lstCaseBasicInfo.CopyTo(tmpArr);
            Viewer.tvForm.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(tvForm_FocusedNodeChanged);
            Viewer.tvForm.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(tvForm_FocusedNodeChanged);
            Viewer.tvForm.ExpandAll();
            Viewer.tvForm.EndUpdate();
        }
        #endregion

        #region tvForm_FocusedNodeChanged
        /// <summary>
        /// 树操作中
        /// </summary>
        bool isTreeDoing { get; set; }
        /// <summary>
        /// tvForm_FocusedNodeChanged
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
                LoadForm(e.Node);
            }
            finally
            {
                isTreeDoing = false;
            }
        }
        #endregion

        #region 加载表单
        /// <summary>
        /// 加载表单
        /// </summary>      
        void LoadForm(TreeListNode node)
        {
            EntityEmrBasicInfo caseVo = (EntityEmrBasicInfo)Viewer.tvForm.GetDataRecordByNode(node);
            LoadFormData(caseVo);
        }
        /// <summary>
        /// LoadForm
        /// </summary>
        /// <param name="formVo"></param>
        void LoadFormData(EntityEmrBasicInfo caseVo)
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                Viewer.page1.Controls.Clear();
                Panel pnl = new Panel();
                pnl.BackColor = Color.FromArgb(196, 200, 205);
                pnl.Height = 30;
                pnl.Dock = DockStyle.Bottom;
                if (caseVo == null)
                {
                    Viewer.txtCaseCode.Text = string.Empty;
                    Viewer.txtCaseName.Text = string.Empty;
                    Viewer.lueFormTemplate.Text = string.Empty;
                    Viewer.luePrintTemplate.Text = string.Empty;
                    Viewer.cboVersion.Text = string.Empty;
                    Viewer.cboFormType.Text = string.Empty;
                    Viewer.cboFormStatus.Text = string.Empty;
                    Viewer.lueCaseCata.Text = string.Empty; ;
                    Viewer.cboCaseStyle.Text = string.Empty;
                    Viewer.cboCaseAttribute.Text = string.Empty;
                    Viewer.chkShowFormStatus1.Checked = false;
                    Viewer.chkShowFormStatus2.Checked = false;
                    Viewer.chkExpert1.Checked = false;
                    Viewer.chkExpert2.Checked = false;
                    Viewer.chkMultPage1.Checked = false;
                    Viewer.chkMultPage2.Checked = false;
                    Viewer.chkRef1.Checked = false;
                    Viewer.chkRef2.Checked = false;
                    Viewer.chkBanding1.Checked = false;
                    Viewer.chkBanding2.Checked = false;
                    Viewer.cboDateType.Text = string.Empty;
                    Viewer.txtHeadOfDeptTime.EditValue = null;
                    Viewer.txtWriteLimitTime.EditValue = null;
                    Viewer.txtQcOfDeptTime.EditValue = null;
                    Viewer.txtTimePeriod.EditValue = null;
                    Viewer.txtPriorTime.EditValue = null;
                    Viewer.cboSigLevel.Text = string.Empty;
                    Viewer.chkHeadOfDeptCheck1.Checked = false;
                    Viewer.chkHeadOfDeptCheck2.Checked = false;
                    Viewer.chkSuperDoctCheck1.Checked = false;
                    Viewer.chkSuperDoctCheck2.Checked = false;
                    Viewer.gcDept.DataSource = null;
                }
                else
                {
                    if (caseVo.formId > 0)
                    {
                        ShowPanelForm efPanel = new ShowPanelForm((int)caseVo.formId);
                        efPanel.Location = new Point(12, 10);

                        XtraScrollableControl scrollCtrl = new XtraScrollableControl();
                        scrollCtrl.Dock = DockStyle.Fill;
                        scrollCtrl.BackColor = Color.FromArgb(196, 200, 205);
                        scrollCtrl.Controls.Add(efPanel);
                        scrollCtrl.Controls.Add(pnl);
                        Viewer.page1.Controls.Add(scrollCtrl);
                    }

                    Viewer.txtCaseCode.Text = caseVo.caseCode;
                    Viewer.txtCaseName.Text = caseVo.caseName;
                    Viewer.lueFormTemplate.Properties.DBValue = caseVo.formId.ToString();
                    SetLueFormTemplate(caseVo.formId);
                    Viewer.luePrintTemplate.Properties.DBValue = caseVo.printTemplateId.ToString();
                    SetLuePrintTemplate(caseVo.printTemplateId);
                    Viewer.cboVersion.Text = "1";
                    Viewer.cboFormType.SelectedIndex = (int)caseVo.typeId;
                    Viewer.cboFormStatus.SelectedIndex = caseVo.status;
                    Viewer.lueCaseCata.Properties.DBValue = caseVo.catalogId.ToString();
                    SetLueCaseCata(caseVo.catalogId);
                    Viewer.cboCaseStyle.SelectedIndex = caseVo.caseStyle;
                    Viewer.cboCaseAttribute.SelectedIndex = Function.Int(caseVo.attribute) + 1;
                    Viewer.chkShowFormStatus1.Checked = caseVo.showCaseStatus == 1 ? true : false;
                    Viewer.chkShowFormStatus2.Checked = caseVo.showCaseStatus == 0 ? true : false;
                    Viewer.chkExpert1.Checked = caseVo.specialFlag == 1 ? true : false;
                    Viewer.chkExpert2.Checked = caseVo.specialFlag == 0 ? true : false;
                    Viewer.chkMultPage1.Checked = caseVo.multiPageFlag == 1 ? true : false;
                    Viewer.chkMultPage2.Checked = caseVo.multiPageFlag == 0 ? true : false;
                    Viewer.chkRef1.Checked = caseVo.referenceType == 1 ? true : false;
                    Viewer.chkRef2.Checked = caseVo.referenceType == 0 ? true : false;
                    Viewer.chkBanding1.Checked = caseVo.bandingFlag == 1 ? true : false;
                    Viewer.chkBanding2.Checked = caseVo.bandingFlag == 0 ? true : false;
                    Viewer.cboDateType.SelectedIndex = caseVo.timeType;
                    Viewer.txtHeadOfDeptTime.EditValue = caseVo.lockDateDirector;
                    Viewer.txtWriteLimitTime.EditValue = caseVo.timeLimit;
                    Viewer.txtQcOfDeptTime.EditValue = caseVo.lockDateQcDept;
                    Viewer.txtTimePeriod.EditValue = caseVo.timePeriod;
                    Viewer.txtPriorTime.EditValue = caseVo.aheadTime;
                    Viewer.cboSigLevel.SelectedIndex = caseVo.signLevel;
                    Viewer.chkHeadOfDeptCheck1.Checked = caseVo.dirDoctSignFlag == 1 ? true : false;
                    Viewer.chkHeadOfDeptCheck2.Checked = caseVo.dirDoctSignFlag == 0 ? true : false;
                    Viewer.chkSuperDoctCheck1.Checked = caseVo.supDoctSignFlag == 1 ? true : false;
                    Viewer.chkSuperDoctCheck2.Checked = caseVo.supDoctSignFlag == 0 ? true : false;
                    LoadCaseDept(caseVo.caseCode);
                }
                // form.Main
                Viewer.txtCaseCode.Tag = caseVo;
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

        #region SetLueFormTemplate
        /// <summary>
        /// SetLueFormTemplate
        /// </summary>
        /// <param name="rankCode"></param>
        void SetLueFormTemplate(decimal formId)
        {
            Viewer.lueFormTemplate.Properties.ForbidPoput = true;
            if (lueFormData != null && lueFormData.Count > 0)
            {
                if (lueFormData.Any(t => t.Formid == formId))
                    Viewer.lueFormTemplate.Text = (lueFormData.FirstOrDefault(t => t.Formid == formId)).Formname;
                else
                    Viewer.lueFormTemplate.Text = string.Empty;
            }
            Viewer.lueFormTemplate.Properties.DisplayValue = Viewer.lueFormTemplate.Text;
            Viewer.lueFormTemplate.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLuePrintTemplate
        /// <summary>
        /// SetLuePrintTemplate
        /// </summary>
        /// <param name="rankCode"></param>
        void SetLuePrintTemplate(decimal printId)
        {
            Viewer.luePrintTemplate.Properties.ForbidPoput = true;
            if (luePrintData != null && luePrintData.Count > 0)
            {
                if (luePrintData.Any(t => t.templateId == printId))
                    Viewer.luePrintTemplate.Text = (luePrintData.FirstOrDefault(t => t.templateId == printId)).templateName;
                else
                    Viewer.luePrintTemplate.Text = string.Empty;
            }
            Viewer.luePrintTemplate.Properties.DisplayValue = Viewer.luePrintTemplate.Text;
            Viewer.luePrintTemplate.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueCaseCata
        /// <summary>
        /// SetLueCaseCata
        /// </summary>
        /// <param name="rankCode"></param>
        void SetLueCaseCata(decimal cataId)
        {
            Viewer.lueCaseCata.Properties.ForbidPoput = true;
            if (lueCataDatalog != null && lueCataDatalog.Count > 0)
            {
                if (lueCataDatalog.Any(t => t.catalogId == cataId))
                    Viewer.lueCaseCata.Text = (lueCataDatalog.FirstOrDefault(t => t.catalogId == cataId)).catalogName;
                else
                    Viewer.lueCaseCata.Text = string.Empty;
            }
            Viewer.lueCaseCata.Properties.DisplayValue = Viewer.lueCaseCata.Text;
            Viewer.lueCaseCata.Properties.ForbidPoput = false;
        }
        #endregion

        #endregion

        #region 查找表单
        /// <summary>
        /// 查找表单
        /// </summary>
        /// <param name="val"></param>
        internal void FindForm(string val)
        {
            if (string.IsNullOrEmpty(val)) return;
            bool isFind = false;
            EntityEmrBasicInfo caseVo = null;
            for (int i = this.FindIndex; i < Viewer.tvForm.AllNodesCount; i++)
            {
                caseVo = (EntityEmrBasicInfo)Viewer.tvForm.GetDataRecordByNode(Viewer.tvForm.GetNodeByVisibleIndex(i));
                //if (caseVo.isLeaf)
                //{
                if (caseVo.pyCode.StartsWith(val) || caseVo.wbCode.StartsWith(val) || caseVo.caseName.StartsWith(val) ||
                        caseVo.caseCode.Equals(val))
                {
                    this.FindIndex = i + 1;
                    Viewer.tvForm.SetFocusedNode(Viewer.tvForm.GetNodeByVisibleIndex(i));
                    isFind = true;
                    break;
                }
                //}
            }
            if (isFind)
            {
                if (this.FindIndex < Viewer.tvForm.AllNodesCount && DialogBox.Msg("是否继续查找？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FindForm(val);
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

        #region GetCaseVo
        /// <summary>
        /// GetCaseVo
        /// </summary>
        /// <returns></returns>
        EntityEmrBasicInfo GetCaseVo()
        {
            if (Viewer.txtCaseCode.Tag != null)
                return Viewer.txtCaseCode.Tag as EntityEmrBasicInfo;
            else
                return null;
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            LoadFormData(null);
            this.Viewer.txtCaseCode.Focus();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal void Delete()
        {
            EntityEmrBasicInfo caseVo = GetCaseVo();
            if (caseVo != null && caseVo.serNo > 0)
            {
                if (DialogBox.Msg("是否删除当前病历资料？？\r\n\r\n" + "【" + caseVo.caseName + "】", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (ProxyCommon proxy = new ProxyCommon())
                    {
                        if (proxy.Service.DeleteCaseBasicInfo((int)caseVo.serNo, caseVo.caseCode))
                        {
                            DialogBox.Msg("删除成功！");
                            this.New();
                            this.RefreshData();
                        }
                        else
                        {
                            DialogBox.Msg("删除失败。");
                        }
                    }
                }
            }
            else
            {
                this.New();
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        internal void Save()
        {
            EntityEmrBasicInfo caseVo = new EntityEmrBasicInfo();
            if (Viewer.txtCaseCode.Tag != null) caseVo = Viewer.txtCaseCode.Tag as EntityEmrBasicInfo;
            if (lueCataDatalog.Any(t => t.catalogId + "^" == caseVo.caseCode))
            {
                DialogBox.Msg("病历目录，无需保存。");
                return;
            }
            caseVo.caseCode = Viewer.txtCaseCode.Text;
            caseVo.caseName = Viewer.txtCaseName.Text;
            caseVo.formId = Function.Int(Viewer.lueFormTemplate.Properties.DBValue);
            caseVo.printTemplateId = Function.Int(Viewer.luePrintTemplate.Properties.DBValue);
            caseVo.typeId = Viewer.cboFormType.SelectedIndex;
            caseVo.status = Viewer.cboFormStatus.SelectedIndex;
            caseVo.catalogId = Function.Int(Viewer.lueCaseCata.Properties.DBValue);
            caseVo.caseStyle = Viewer.cboCaseStyle.SelectedIndex;
            caseVo.attribute = Viewer.cboCaseAttribute.SelectedIndex - 1;
            caseVo.showCaseStatus = Viewer.chkShowFormStatus1.Checked ? 1 : 0;
            caseVo.specialFlag = Viewer.chkExpert1.Checked ? 1 : 0;
            caseVo.multiPageFlag = Viewer.chkMultPage1.Checked ? 1 : 0;
            caseVo.referenceType = Viewer.chkRef1.Checked ? 1 : 0;
            caseVo.bandingFlag = Viewer.chkBanding1.Checked ? 1 : 0;
            caseVo.timeType = Viewer.cboDateType.SelectedIndex;
            caseVo.lockDateDirector = Function.Int(Viewer.txtHeadOfDeptTime.EditValue);
            caseVo.timeLimit = Function.Int(Viewer.txtWriteLimitTime.EditValue);
            caseVo.lockDateQcDept = Function.Int(Viewer.txtQcOfDeptTime.EditValue);
            caseVo.timePeriod = Function.Int(Viewer.txtTimePeriod.EditValue);
            caseVo.aheadTime = Function.Int(Viewer.txtPriorTime.EditValue);
            caseVo.signLevel = Viewer.cboSigLevel.SelectedIndex;
            caseVo.dirDoctSignFlag = Viewer.chkHeadOfDeptCheck1.Checked ? 1 : 0;
            caseVo.supDoctSignFlag = Viewer.chkSuperDoctCheck1.Checked ? 1 : 0;
            caseVo.caseScope = 2;       // 1 门诊病历； 2 住院病历

            if (string.IsNullOrEmpty(caseVo.caseCode) || caseVo.caseCode.Trim() == string.Empty)
            {
                DialogBox.Msg("请输入病历编码");
                Viewer.txtCaseCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(caseVo.caseName) || caseVo.caseName.Trim() == string.Empty)
            {
                DialogBox.Msg("请输入病历名称");
                Viewer.txtCaseName.Focus();
                return;
            }

            List<EntityEmrDept> lstEmrDept = new List<EntityEmrDept>();
            using (ProxyCommon proxy = new ProxyCommon())
            {
                bool isNew = caseVo.serNo <= 0 ? true : false;
                if (proxy.Service.SaveCaseBasicInfo(ref caseVo, lstEmrDept))
                {
                    Viewer.txtCaseCode.Tag = caseVo;
                    if (isNew) this.RefreshData();
                    DialogBox.Msg("保存成功");
                }
                else
                {
                    DialogBox.Msg("保存失败");
                }
            }
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        internal void RefreshData()
        {
            InitCatalog();
        }
        #endregion

        #region Design
        /// <summary>
        /// Design
        /// </summary>
        internal void Design()
        {
            EntityEmrBasicInfo caseVo = GetCaseVo();
            if (caseVo == null)
            {
                DialogBox.Msg("请选择表单。");
            }
            else
            {
                if (string.IsNullOrEmpty(caseVo.caseCode))
                {
                    DialogBox.Msg("请先保存病历基本信息。");
                    return;
                }
                using (frmFormDesign frm = new frmFormDesign())
                {
                    //frm.FormType = 11;  // 0412.临时
                    frm.FormType = 1;
                    frm.FormId = Function.Int((Viewer.txtCaseCode.Tag as EntityEmrBasicInfo).formId);
                    frm.ShowDialog();
                }
            }
        }
        #endregion

        #region NewFormDept
        /// <summary>
        /// NewFormDept
        /// </summary>
        internal void NewFormDept()
        {
            EntityEmrBasicInfo caseVo = GetCaseVo();
            if (caseVo == null)
            {
                DialogBox.Msg("请先保存病历基础信息。");
                return;
            }
            frmNew frm = new frmNew(EntityTools.ConvertToDataTable<EntityCodeDepartment>(GlobalDic.DataSourceDepartment), "deptCode", "deptCode", "deptName");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.lstNo.Count > 0)
                {
                    foreach (int index in frm.lstNo)
                    {
                        EntityEmrDept emrDept = new EntityEmrDept();
                        emrDept.caseCode = caseVo.caseCode;
                        emrDept.deptCode = GlobalDic.DataSourceDepartment[index].deptCode;
                        emrDept.attrFlag = 1;

                        using (ProxyCommon proxy = new ProxyCommon())
                        {
                            if (proxy.Service.SaveCaseDept(emrDept) < 0)
                            {
                                DialogBox.Msg("保存病历所属专科失败。");
                                return;
                            }
                        }
                    }
                    LoadCaseDept(caseVo.caseCode);
                }
            }
        }
        #endregion

        #region DelFormDept
        /// <summary>
        /// DelFormDept
        /// </summary>
        internal void DelFormDept()
        {
            if (Viewer.gvDept.FocusedRowHandle < 0) return;
            if (DialogBox.Msg("是否删除？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EntityEmrDept vo = DataSourceEmrDept[Viewer.gvDept.FocusedRowHandle];
                if (vo != null)
                {
                    using (ProxyCommon proxy = new ProxyCommon())
                    {
                        if (proxy.Service.DelCaseDept(vo) > 0)
                        {
                            LoadCaseDept(vo.caseCode);
                            DialogBox.Msg("删除成功!");
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

        #region LoadCaseDept
        /// <summary>
        /// caseCode
        /// </summary>
        /// <param name="caseCode"></param>
        void LoadCaseDept(string caseCode)
        {
            using (ProxyCommon proxy = new ProxyCommon())
            {
                DataSourceEmrDept = proxy.Service.GetCaseDept(caseCode);
            }
            if (DataSourceEmrDept != null && DataSourceEmrDept.Count > 0)
            {
                foreach (EntityEmrDept item in DataSourceEmrDept)
                {
                    if (GlobalDic.DataSourceDepartment.Any(t => t.deptCode.Trim() == item.deptCode.Trim()))
                    {
                        item.deptName = GlobalDic.DataSourceDepartment.FirstOrDefault(t => t.deptCode.Trim() == item.deptCode.Trim()).deptName;
                    }
                }
            }
            Viewer.gcDept.DataSource = DataSourceEmrDept;
        }
        #endregion

        #endregion


    }
}
