using Common.Controls;
using Common.Controls.Emr;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 表单.Form
    /// </summary>
    public partial class frmFormDesign : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmFormDesign()
        {
            InitializeComponent();

            _KeyStrokeMessageFilter = new KeystrokMessageFilter();
            Application.AddMessageFilter(_KeyStrokeMessageFilter);

            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 5;
        }
        #endregion

        #region Override
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlFormDesign();
            Controller.SetUI(this);
        }

        #endregion

        #region 变量.属性

        #region 表单类型
        /// <summary>
        /// 表单类型
        /// </summary>
        int _FormType = 1;
        /// <summary>
        /// 1  病历表单
        /// 2  路径表单
        /// 3  电话随访模板
        /// 4  电话提醒模板
        /// 5  电话问卷模板
        /// 11 健康档案
        /// 12 报表
        /// 13 病理模板
        /// </summary>
        public int FormType
        {
            get { return _FormType; }
            set { _FormType = value; }
        }
        #endregion

        #region 主信息

        /// <summary>
        /// FormId
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool DelFlag { get; set; }

        /// <summary>
        /// 新建标识
        /// </summary>
        public bool NewFlag { get; set; }

        /// <summary>
        /// 更新标识
        /// </summary>
        public bool UpdateFlag { get; set; }

        bool isSelected { get; set; }
        DesignSurface designSurface;
        UndoEngineImpl undoEngine;
        ComponentSerializationServiceImpl serializationService;

        /// <summary>
        /// 菜单服务
        /// </summary>
        MenuCommandServiceImpl menuService;

        /// <summary>
        /// 控件加载器
        /// </summary>
        DesignLoaderForm loader;

        /// <summary>
        /// 设计器宿主
        /// </summary>
        IDesignerHost idh;

        /// <summary>
        /// 消息筛选器
        /// </summary>
        KeystrokMessageFilter _KeyStrokeMessageFilter;

        bool IsExists { get; set; }

        bool Loading { get; set; }

        public List<int> DelFormId { get; set; }

        public bool IsSave { get; set; }

        List<EntityFormCtrl> formCtrlData { get; set; }

        /// <summary>
        /// 打印模板ID
        /// </summary>
        int PrintTemplateId { get; set; }

        #endregion

        #region 随访

        /// <summary>
        /// 随访模板主信息
        /// </summary>
        public EntityFuTemplate TemplateMainVo { get; set; }

        /// <summary>
        /// 随访计划操作类型 0 null; 1 new; 2 edit
        /// </summary>
        public int FuPlanOpType { get; set; }

        /// <summary>
        /// 随访计划表单VO
        /// </summary>
        public List<EntityFuPlanForm> FuPlanFormData { get; set; }

        #endregion

        #endregion

        #region MouseDown

        private void xtraScrollableControl_MouseDown(object sender, MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }

        private void pageProperty_MouseDown(object sender, MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }

        private void pageEvent_MouseDown(object sender, MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }

        private void xtcProperty_MouseDown(object sender, MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }

        #endregion

        #region Reset

        private DesignerVerb StandartVerb(string text, CommandID commandID)
        {
            return new DesignerVerb(text, delegate(object o, EventArgs e)
            {
                IMenuCommandService ms = GetService(typeof(IMenuCommandService)) as IMenuCommandService;
                bool success = ms.GlobalInvoke(commandID);
            }
            );
        }

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            this.FormId = 0;

            this.txtEfCode.Text = string.Empty;
            this.txtEfName.Text = string.Empty;
            this.cboEfStatus.SelectedIndex = 1;
            if (GlobalHospital.Current == EnumHospitalCode.茂名人医 || GlobalHospital.Current == EnumHospitalCode.高州中医 || GlobalHospital.Current == EnumHospitalCode.湛江中心 || GlobalHospital.Current == EnumHospitalCode.高州二院)
            {
                this.txtEfCode.Text = DateTime.Now.ToString("yyMMddHHmm");
            }

            this.txtEfCode.Tag = null;
            this.txtEfName.Tag = null;

            foreach (IComponent comp in idh.Container.Components)
            {
                if (comp != idh.RootComponent)
                {
                    idh.DestroyComponent(comp);
                }
            }

            this.IsExists = false;
            this.ValueChanged = false;
        }
        #endregion

        #region ItemClick

        private void bbiToolBox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelL.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValueChanged)
            {
                if (DialogBox.Msg("是否保存已更改的资料？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        return;
                    }
                }
            }
            this.Reset();
        }

        private void bbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.FormId > 0)
            {
                int version = Function.Int(this.cboVersion.Text);
                if (version == 0)
                {
                    DialogBox.Msg("请选择版本号.", MessageBoxIcon.Information);
                    return;
                }
                if (DialogBox.Msg("是否删除当前表单模板资料？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProxyFormDesign proxy = new ProxyFormDesign();
                    int intRet = proxy.Service.DelForm(this.FormId, version);
                    proxy = null;
                    if (intRet > 0)
                    {
                        if (DelFormId == null)
                        {
                            DelFormId = new List<int>();
                        }
                        DelFormId.Add(this.FormId);

                        int count = this.cboVersion.Properties.Items.Count;
                        for (int i = count - 1; i >= 0; i--)
                        {
                            if (this.cboVersion.Properties.Items[i].ToString() == version.ToString())
                            {
                                this.cboVersion.Properties.Items.Remove(version.ToString());
                                break;
                            }
                        }

                        this.cboVersion.Text = string.Empty;
                        this.ValueChanged = false;
                        DelFlag = true;
                        DialogBox.Msg("删除成功!", MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogBox.Msg("删除失败.", MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            bbiNew_ItemClick(null, null);
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        public bool Save()
        {
            return SaveForm();
        }

        #region SaveForm
        /// <summary>
        /// SaveForm
        /// </summary>
        /// <returns></returns>
        public bool SaveForm()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                formCtrlData = this.loader.Save();

                if (formCtrlData != null && formCtrlData.Count > 0)
                {
                    string strFormCode = string.Empty;
                    string strFormName = string.Empty;

                    strFormCode = this.txtEfCode.Text.Trim();
                    if (string.IsNullOrEmpty(strFormCode))
                    {
                        DialogBox.Msg("请输申请单编码。", MessageBoxIcon.Exclamation);
                        this.txtEfCode.Focus();
                        return false;
                    }

                    strFormName = this.txtEfName.Text.Trim();
                    if (string.IsNullOrEmpty(strFormName))
                    {
                        DialogBox.Msg("请输入申请单名称。", MessageBoxIcon.Exclamation);
                        this.txtEfName.Focus();
                        return false;
                    }
                    int intVersion = Function.Int(this.cboVersion.Text);
                    if (intVersion == 0)
                    {
                        DialogBox.Msg("请输入版本号。", MessageBoxIcon.Exclamation);
                        this.cboVersion.Focus();
                        return false;
                    }
                    int intStatus = this.cboEfStatus.SelectedIndex;

                    List<string> lstCtrl = new List<string>();
                    foreach (EntityFormCtrl ctrl in formCtrlData)
                    {
                        if (!string.IsNullOrEmpty(ctrl.ItemName))
                        {
                            if (lstCtrl.IndexOf(ctrl.ItemName) < 0)
                            {
                                lstCtrl.Add(ctrl.ItemName);
                            }
                            else
                            {
                                DialogBox.Msg("存在同名的控件名称:" + ctrl.ItemCaption + "(" + ctrl.ItemName + "),请修正。", MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                        else
                        {
                            if (Function.Int(ctrl.ItemType) > 0)
                            {
                                DialogBox.Msg("请录入项目代码:" + ctrl.ItemCaption, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                    }

                    try
                    {
                        // 1.
                        EntityFormDesign formVo = new EntityFormDesign();
                        formVo.Formid = FormId;
                        formVo.Formcode = strFormCode;
                        formVo.Formname = strFormName;
                        formVo.Formtype = this.FormType;
                        formVo.Version = intVersion;
                        formVo.Pycode = SpellCodeHelper.GetPyCode(strFormName);
                        formVo.Wbcode = SpellCodeHelper.GetWbCode(strFormName);
                        formVo.Recorderid = GlobalLogin.objLogin.EmpNo;
                        formVo.RecorderName = GlobalLogin.objLogin.EmpName;
                        formVo.Status = intStatus;
                        formVo.Printtemplateid = this.PrintTemplateId;

                        int heightCtrl = 0;
                        foreach (EntityFormCtrl item in formCtrlData)
                        {
                            heightCtrl = Math.Max(item.Top, heightCtrl);
                        }
                        formVo.Panelsize = Convert.ToString(heightCtrl + 50) + "|" + pnlDesignPanel.Width.ToString();
                        // 2.
                        formVo.Layout = FormTool.LayoutXml(formCtrlData);

                        int intAppFid = 0;
                        ProxyFormDesign proxy = new ProxyFormDesign();
                        formVo.Recorddate = Common.Utils.Utils.ServerTime();

                        if (this.FuPlanOpType > 0)
                        {
                            #region 随访计划表单处理

                            if (this.FuPlanOpType == 1)     // new
                            {
                                foreach (EntityFuPlanForm item in this.FuPlanFormData)
                                {
                                    item.formId = 0;
                                    item.formCode = formVo.Formcode;
                                    item.formName = formVo.Formname;
                                    item.version = formVo.Version;
                                    item.pyCode = formVo.Pycode;
                                    item.wbCode = formVo.Wbcode;
                                    item.recorder = GlobalLogin.objLogin.EmpNo;
                                    item.recordDate = formVo.Recorddate;
                                    item.panelSize = formVo.Panelsize;
                                    item.layout = formVo.Layout;
                                }
                            }
                            else if (this.FuPlanOpType == 2)    // edit
                            {
                                foreach (EntityFuPlanForm item in this.FuPlanFormData)
                                {
                                    item.panelSize = formVo.Panelsize;
                                    item.layout = formVo.Layout;
                                }
                            }
                            else
                            {
                                return false;
                            }
                            using (ProxyFormDesign proxy1 = new ProxyFormDesign())
                            {
                                decimal formId = 0;
                                if (proxy1.Service.SaveFuPlanForm(this.FuPlanFormData, out formId) > 0)
                                {
                                    this.FuPlanFormData[0].formId = formId;
                                    intAppFid = (int)formId;
                                    this.FormId = (int)formId;
                                    if (wrapper != null)
                                    {
                                        wrapper.FormId = this.FormId;
                                    }
                                    if (this.FuPlanOpType == 1)
                                        NewFlag = true;
                                    else if (this.FuPlanOpType == 2)
                                        UpdateFlag = true;
                                    this.IsSave = true;
                                    DialogBox.Msg("保存表单成功!", MessageBoxIcon.Exclamation);
                                    this.ValueChanged = false;
                                    return true;
                                }
                                else
                                {
                                    DialogBox.Msg("保存表单失败。", MessageBoxIcon.Exclamation);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 一般表单处理

                            if (proxy.Service.SaveForm(formVo, out intAppFid) > 0)
                            {
                                if (this.FormId <= 0)
                                {
                                    NewFlag = true;
                                }
                                else
                                {
                                    if (!NewFlag)
                                    {
                                        UpdateFlag = true;
                                    }
                                }
                                this.ValueChanged = false;
                                this.FormId = intAppFid;

                                if (wrapper != null)
                                {
                                    wrapper.FormId = this.FormId;
                                }

                                bool isExsit = false;
                                int count = this.cboVersion.Properties.Items.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    if (this.cboVersion.Properties.Items[i].ToString() == formVo.Version.ToString())
                                    {
                                        IsExists = true;
                                        break;
                                    }
                                }
                                if (!isExsit) this.cboVersion.Properties.Items.Add(formVo.Version.ToString());

                                this.IsSave = true;
                                DialogBox.Msg("保存表单成功!", MessageBoxIcon.Exclamation);
                                this.ValueChanged = false;
                                return true;
                            }
                            else
                            {
                                DialogBox.Msg("保存表单失败。", MessageBoxIcon.Exclamation);
                            }
                            #endregion
                        }
                    }
                    catch (Exception e)
                    {
                        DialogBox.Msg(e.Message);
                        return false;
                    }
                }
                else
                {
                    DialogBox.Msg("请制作表单。", MessageBoxIcon.Information);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return false;
        }
        #endregion

        void SetVersion(EntityFormDesign formVo)
        {
            this.cboVersion.Properties.Items.Clear();
            if (formVo.lstVersion != null && formVo.lstVersion.Count > 0)
            {
                foreach (int no in formVo.lstVersion)
                {
                    this.cboVersion.Properties.Items.Add(no.ToString());
                }
            }
        }

        void SetMainInfo(EntityFormDesign formVo)
        {
            SetVersion(formVo);
            this.txtEfCode.Text = formVo.Formcode;
            this.txtEfName.Text = formVo.Formname;
            isSelected = true;
            this.cboVersion.Text = formVo.Version.ToString();
            isSelected = false;
            this.cboEfStatus.SelectedIndex = Function.Int(formVo.Status);
            this.pnlDesignPanel.Width = formVo.PanelWidth;
            this.pnlDesignPanel.Height = formVo.PanelHeight < this.Height ? this.Height - 80 : formVo.PanelHeight;
        }

        #endregion

        #region SuspendLayoutUc
        /// <summary>
        /// SuspendLayoutUc
        /// </summary>
        void SuspendLayoutUc()
        {
            Function.SuspendLayout(this.toolboxServiceXtraTree.Handle);
        }
        #endregion

        #region ResumeLayoutUc
        /// <summary>
        /// ResumeLayoutUc
        /// </summary>
        void ResumeLayoutUc()
        {
            Function.ResumeLayout(this.toolboxServiceXtraTree.Handle);
            this.toolboxServiceXtraTree.Refresh();
        }
        #endregion

        #region 窗体事件

        private void ucBox_MouseDoubleClick(object sender, EntityFormObject formVo)
        {
            toolboxServiceXtraTree.AddControl(new ToolboxItem(formVo.objType));
        }

        private void ucBox_MouseMove(object sender, EntityFormObject formVo)
        {
            DoDragDrop(new ToolboxItem(formVo.objType), DragDropEffects.Copy);
        }

        ucToolBoxEf ucBox = null;

        /// <summary>
        /// 填充控件工具箱
        /// </summary>
        /// <param name="toolbox"></param>
        private void PopulateToolbox(IToolboxService toolbox)
        {
            ucBox = new ucToolBoxEf();
            ucBox.Visible = false;
            ucBox.Dock = DockStyle.Fill;
            ucBox.BringToFront();
            ucBox.CtrlMouseDoubleClick += new HandleEfCtrlMouseDoubleClick(ucBox_MouseDoubleClick);
            ucBox.CtrlMouseMove += new HandleEfCtrlMouseMove(ucBox_MouseMove);
            toolboxServiceXtraTree.Controls.Add(ucBox);

            // if (this.FormType > 2 || this.FuPlanOpType > 0) ucBox.navBarGroup2.Visible = false;
        }

        private class clsToolBoxItem : IComparable
        {
            public ToolboxItem objItem = null;
            public string strCategory = string.Empty;
            public int intSortNo = 0;
            public int CompareTo(object obj)
            {
                if (obj is clsToolBoxItem)
                {
                    return this.intSortNo.CompareTo(((clsToolBoxItem)obj).intSortNo);
                }
                return 0;
            }
        }

        #region InitDesigner
        /// <summary>
        /// InitDesigner
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="version"></param>
        void InitDesigner(int formId, int version)
        {
            if (this.isSelected) return;
            this.Loading = true;
            try
            {
                SuspendLayoutUc();
                this.Cursor = Cursors.WaitCursor;
                designSurface = new DesignSurface();
                this.pnlDesignPanel.Controls.Clear();

                //服务容器
                IServiceContainer serviceContainer = designSurface.GetService(typeof(IServiceContainer)) as IServiceContainer;

                idh = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
                _KeyStrokeMessageFilter.SetHostAndMDIForm(idh, designSurface);

                //工具箱服务
                this.toolboxServiceXtraTree.host = idh;
                serviceContainer.AddService(typeof(IToolboxService), this.toolboxServiceXtraTree);
                PopulateToolbox(this.toolboxServiceXtraTree);
                this.toolboxServiceXtraTree.designPanel = this.pnlDesignPanel;

                //菜单命令服务
                menuService = new MenuCommandServiceImpl(serviceContainer);
                serviceContainer.AddService(typeof(IMenuCommandService), menuService);

                //命名服务
                serviceContainer.AddService(typeof(INameCreationService), new NameCreationService(idh));

                //控件序列化反序列化服务
                serviceContainer.AddService(typeof(IDesignerSerializationService), new DesignerSerializationServiceForm(idh));
                //获取控件信息  
                EntityFormDesign formVo = new EntityFormDesign();

                if (this.FuPlanOpType == 2)
                {
                    if (this.FuPlanFormData != null && !string.IsNullOrEmpty(this.FuPlanFormData[0].layout))
                    {
                        formCtrlData = FormTool.Entities(this.FuPlanFormData[0].layout);

                        this.txtEfCode.Text = this.FuPlanFormData[0].formCode;
                        this.txtEfName.Text = this.FuPlanFormData[0].formName;
                        isSelected = true;
                        this.cboVersion.Text = this.FuPlanFormData[0].version.ToString();
                        isSelected = false;
                        this.cboEfStatus.SelectedIndex = Function.Int(this.FuPlanFormData[0].status);
                        this.pnlDesignPanel.Width = this.FuPlanFormData[0].PanelWidth;
                        this.pnlDesignPanel.Height = this.FuPlanFormData[0].PanelHeight < this.Height ? this.Height - 80 : this.FuPlanFormData[0].PanelHeight;
                    }
                }
                else
                {
                    if (formId > 0)
                    {
                        this.IsExists = true;
                        ProxyFormDesign proxy = new ProxyFormDesign();
                        proxy.Service.GetForm(formId, version, out formVo);
                        proxy = null;
                        this.PrintTemplateId = formVo.Printtemplateid;
                    }
                    if (formVo != null && formVo.Formid > 0)
                    {
                        SetMainInfo(formVo);
                        if (!string.IsNullOrEmpty(formVo.Layout))
                        {
                            formCtrlData = FormTool.Entities(formVo.Layout);
                        }
                    }
                    else
                    {
                        this.cboEfStatus.SelectedIndex = 1;
                        this.pnlDesignPanel.Height = this.Height - 80;
                    }
                }
                if ((this.FormType > 2 && formId <= 0 && (formVo == null || string.IsNullOrEmpty(formVo.Layout))) || this.FuPlanOpType == 2)
                {
                    this.txtEfCode.Properties.ReadOnly = true;
                    this.txtEfName.Properties.ReadOnly = true;
                }
                if (this.FormType > 2 || this.FuPlanOpType > 0)
                {
                    this.cboVersion.Text = "1";
                    this.cboVersion.Properties.ReadOnly = true;
                    this.cboEfStatus.SelectedIndex = 1;
                    this.cboEfStatus.Properties.ReadOnly = true;
                }
                if (this.FormType > 10)
                {
                    this.txtEfCode.Properties.ReadOnly = true;
                    this.txtEfName.Properties.ReadOnly = true;
                    this.cboVersion.Text = "1";
                    this.cboVersion.Properties.ReadOnly = true;
                    this.cboEfStatus.SelectedIndex = 1;
                    this.cboEfStatus.Properties.ReadOnly = true;
                }
                if (this.TemplateMainVo != null)
                {
                    this.txtEfCode.Text = this.TemplateMainVo.templateCode;
                    this.txtEfName.Text = this.TemplateMainVo.templateName;
                }
                if (this.txtEfCode.Text.Trim() == string.Empty)
                {
                    if (GlobalHospital.Current == EnumHospitalCode.茂名人医 || GlobalHospital.Current == EnumHospitalCode.高州中医 || GlobalHospital.Current == EnumHospitalCode.湛江中心 || GlobalHospital.Current == EnumHospitalCode.高州二院 || GlobalHospital.Current == EnumHospitalCode.南沙一院)
                    {
                        this.txtEfCode.Text = DateTime.Now.ToString("yyMMddHHmm");
                    }
                }
                loader = new DesignLoaderForm(idh, formCtrlData);
                loader.InitWidth = this.pnlDesignPanel.Width - 30;
                loader.InitHeight = this.pnlDesignPanel.Height - 30;
                loader.FormFlag = true;

                //绑定事件
                loader.ComponentAdded -= new ComponentEventHandler(loader_ComponentAdded);
                loader.ComponentAdded += new ComponentEventHandler(loader_ComponentAdded);
                loader.ComponentChanged -= new ComponentChangedEventHandler(loader_ComponentChanged);
                loader.ComponentChanged += new ComponentChangedEventHandler(loader_ComponentChanged);
                loader.ComponentRemoved -= new ComponentEventHandler(loader_ComponentRemoved);
                loader.ComponentRemoved += new ComponentEventHandler(loader_ComponentRemoved);

                designSurface.BeginLoad(loader);

                Control ctrl = designSurface.View as Control;
                ctrl.Parent = pnlDesignPanel;
                ctrl.Dock = DockStyle.Fill;

                ISelectionService selectionService = designSurface.GetService(typeof(ISelectionService)) as ISelectionService;
                selectionService.SelectionChanged -= new EventHandler(selectionService_SelectionChanged);
                selectionService.SelectionChanged += new EventHandler(selectionService_SelectionChanged);

                BehaviorService bhsvc = idh.GetService(typeof(BehaviorService)) as BehaviorService;

                //undo/redo服务
                serializationService = new ComponentSerializationServiceImpl(idh);
                serviceContainer.AddService(typeof(ComponentSerializationService), serializationService);
                undoEngine = new UndoEngineImpl(idh);
                serviceContainer.AddService(typeof(UndoEngine), undoEngine);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ResumeLayoutUc();
                Loading = false;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void frmFormDesign_Load(object sender, EventArgs e)
        {
            try
            {
                ((ctlFormDesign)Controller).Init();
                if (this.FormId > 0)
                {
                    this.timerLayout.Enabled = true;
                }
                else
                {
                    InitDesigner(this.FormId, 0);
                }
                if (this.FormType > 2 || this.FuPlanOpType > 0)
                {
                    this.blbiPrintTemplate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.bbiNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.bbiDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
            finally
            {
                if (this.FormId <= 0)
                    this.timer.Enabled = true;
            }
        }

        private void frmFormDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.pnlDesignPanel.Focus();

            if (this.ValueChanged)// || this.loader.Unsave)
            {
                if (DialogBox.Msg("是否保存修改的内容？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Save())
                    {
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 组件被添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void loader_ComponentAdded(object sender, ComponentEventArgs e)
        {
            if (this.loader.Unsave && !this.loader.Loading)
            {
                this.ValueChanged = true;
            }
        }

        /// <summary>
        /// 组件被移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void loader_ComponentRemoved(object sender, ComponentEventArgs e)
        {
            if (this.loader.Unsave && !this.loader.Loading)
            {
                this.ValueChanged = true;
            }
        }

        /// <summary>
        /// 组件改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void loader_ComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            try
            {
                if (e.Member != null && (e.Member.Name == "Width" || e.Member.Name == "Height" || e.Member.Name == "Location"))
                {
                    if (this.controlPropertyGrid.SelectedObject != null)
                    {
                        this.controlPropertyGrid.Refresh();
                    }
                }

                if (this.loader.Unsave && !this.loader.Loading)
                {
                    this.ValueChanged = true;
                }

                if ((e.Member != null && e.Member.Name.ToLower() != "controls") || e.Member == null)
                {
                    if (undoEngine != null && undoEngine.UndoInProgress == false && e.Member != null && e.NewValue != null && e.OldValue != null && e.NewValue.ToString() != e.OldValue.ToString())
                    {
                        serializationService.AddUndoUnitImpl(e.Component, e.Member.Name, e.OldValue);
                    }

                }
                else
                {
                    serializationService.ClearUndoUnitImpl();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        BaseControlWarpper wrapper = null;

        /// <summary>
        /// 选择的控件改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void selectionService_SelectionChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                //窗体关闭后还会触发此事件导致错误
                return;
            }

            ISelectionService selectionService = designSurface.GetService(typeof(ISelectionService)) as ISelectionService;
            object[] selection;

            //propertyGridControl1.RetrieveFields();
            if (selectionService.SelectionCount == 0)
            {
                controlPropertyGrid.SelectedObject = null;
            }
            else
            {
                selection = new object[selectionService.SelectionCount];
                selectionService.GetSelectedComponents().CopyTo(selection, 0);

                string fieldName = string.Empty;
                string parentNodeName = string.Empty;
                Component ctrl = selection[0] as Component;
                IRuntimeDesignControl ICtrl = ctrl as IRuntimeDesignControl;
                if (ICtrl != null)
                {
                    wrapper = new BaseControlWarpper(ICtrl, idh);
                    wrapper.FormId = this.FormId;
                    wrapper.PropertyGrid = controlPropertyGrid;
                    controlPropertyGrid.SetEditObject(wrapper);
                    fieldName = wrapper.项目代码;
                    parentNodeName = wrapper.父节点名;
                }
                else
                {
                    controlPropertyGrid.SelectedObject = null;
                }
                wrapper_PropertyChanged(fieldName, parentNodeName);
            }
        }

        void wrapper_PropertyChanged(string itemName, string parentNodeName)
        {
            ((ctlFormDesign)Controller).FieldName = itemName;
            ((ctlFormDesign)Controller).ParentNodeName = parentNodeName;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboEfStatus.SelectedIndex == 0)
            {
                this.cboEfStatus.ForeColor = Color.Red;
            }
            else
            {
                this.cboEfStatus.ForeColor = Color.Black;
            }
        }

        private void bbiLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "bbiLeft":
                    menuService.GlobalInvoke(StandardCommands.AlignLeft);
                    break;

                case "bbiUD":
                    menuService.GlobalInvoke(StandardCommands.AlignHorizontalCenters);
                    break;

                case "bbiRight":
                    menuService.GlobalInvoke(StandardCommands.AlignRight);
                    break;

                case "bbiTop":
                    menuService.GlobalInvoke(StandardCommands.AlignTop);
                    break;

                case "bbiLR":
                    menuService.GlobalInvoke(StandardCommands.AlignVerticalCenters);
                    break;

                case "bbiBottom":
                    menuService.GlobalInvoke(StandardCommands.AlignBottom);
                    break;

                case "bbiBringToTop":
                    menuService.GlobalInvoke(StandardCommands.BringToFront);
                    break;

                case "bbiBringToBottom":
                    menuService.GlobalInvoke(StandardCommands.SendToBack);
                    break;

                case "bbiWidth":
                    menuService.HorizSpaceMakeEqual();
                    break;

                case "bbiHeight":
                    menuService.VertSpaceMakeEqual();
                    break;

                case "bbiSize":
                    menuService.HorizVertSpaceMakeEqual();
                    break;

            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.ucBox.Visible = true;
        }

        private void barDockControlTop_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Function.SendMessage(this.Handle);
        }

        #region PrintTable
        /// <summary>
        /// PrintTable
        /// </summary>
        /// <returns></returns>
        DataTable PrintTable()
        {
            if (formCtrlData != null && formCtrlData.Count > 0)
            {
                DataTable dtColumn = new System.Data.DataTable();
                foreach (EntityFormCtrl item in formCtrlData)
                {
                    if (item.ItemType == "1" || item.ItemType == "2" || item.ItemType == "3" ||
                        item.ItemType == "4" || item.ItemType == "5" || item.ItemType == "6")
                    {
                        dtColumn.Columns.Add(item.ControlName);
                        //if (string.IsNullOrEmpty(item.Text))
                        //{
                        //    dtColumn.Columns.Add(item.ControlName);
                        //}
                        //else
                        //{
                        //    dtColumn.Columns.Add(item.ControlName + "(" + item.Text + ")");
                        //}
                    }
                }
                dtColumn.TableName = "数据源";
                return dtColumn;
            }
            return null;
        }
        #endregion

        private void blbiPrintTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ValueChanged || this.loader.Unsave)
            {
                if (SaveForm() == false) return;
            }

            DataTable dtColumn = PrintTable();
            //if (dtColumn != null)
            //{
            //    EntityEform eForm = new EntityEform();
            //    eForm.Efid = this.EfId;
            //    eForm.Printfilename = this.txtEfName.Text.Trim();
            //    if (this.txtEfName.Tag != null)
            //    {
            //        eForm.Printfiledata = this.txtEfName.Tag as byte[];
            //    }
            //    //frmPrDesign frm = new frmPrDesign(dtColumn, eForm);
            //    frmPrintDesigner frm = new frmPrintDesigner(dtColumn, eForm);
            //    frm.ShowDialog();
            //    if (frm.IsSave)
            //    {
            //        this.txtEfName.Tag = frm.Eform.Printfiledata;
            //    }
            //}
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            ((ctlFormDesign)Controller).NewRow();
        }

        private void btnDelRow_Click(object sender, EventArgs e)
        {
            ((ctlFormDesign)Controller).DelRow();
        }

        private void btnSaveRow_Click(object sender, EventArgs e)
        {
            ((ctlFormDesign)Controller).SaveOrderItem();
        }

        private void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int version = Function.Int(this.cboVersion.Text);
            if (this.FormId <= 0 || version == 0) return;
            InitDesigner(this.FormId, version);
        }

        private void timerLayout_Tick(object sender, EventArgs e)
        {
            this.timerLayout.Enabled = false;
            InitDesigner(this.FormId, 0);
            this.timer.Enabled = true;
        }

        #endregion

        #region ValueChanged

        private void txtEafCode_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.Loading)
            {
                this.ValueChanged = true;
            }
        }

        private void txtEafName_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.Loading)
            {
                this.ValueChanged = true;
            }
        }

        private void cboEafType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!this.Loading)
            {
                this.ValueChanged = true;
            }
        }

        private void cboEafScope_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!this.Loading)
            {
                this.ValueChanged = true;
            }
        }

        private void txtEafRID_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.Loading)
            {
                this.ValueChanged = true;
            }
        }

        private void cboEafStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!this.Loading)
            {
                this.ValueChanged = true;
            }
        }

        #endregion


    }
}
