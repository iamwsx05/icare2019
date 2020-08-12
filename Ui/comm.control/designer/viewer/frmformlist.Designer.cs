namespace Common.Controls
{
    partial class frmFormList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormList));
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.lblFind = new DevExpress.XtraEditors.LabelControl();
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            this.tvForm = new DevExpress.XtraTreeList.TreeList();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.showPanelForm = new Common.Controls.ShowPanelForm();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.plTopR = new System.Windows.Forms.Panel();
            this.plMainInfo = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCreatDate = new DevExpress.XtraEditors.TextEdit();
            this.txtFormCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.luePrint = new Common.Controls.LookUpEdit();
            this.txtFormName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.rdoStatus = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cboType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.xtraScrollableControl.SuspendLayout();
            this.plTopR.SuspendLayout();
            this.plMainInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePrint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.tvForm);
            this.pcBackGround.Controls.Add(this.lblFind);
            this.pcBackGround.Controls.Add(this.txtFind);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(250, 682);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(250, 0);
            this.splitterControl1.MaximumSize = new System.Drawing.Size(4, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(4, 682);
            this.splitterControl1.TabIndex = 11;
            this.splitterControl1.TabStop = false;
            // 
            // lblFind
            // 
            this.lblFind.Appearance.BackColor = System.Drawing.Color.White;
            this.lblFind.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.lblFind.Appearance.ForeColor = System.Drawing.Color.Silver;
            this.lblFind.Location = new System.Drawing.Point(80, 6);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(84, 12);
            this.lblFind.TabIndex = 94;
            this.lblFind.Text = "请输入查找条件";
            // 
            // txtFind
            // 
            this.txtFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFind.Location = new System.Drawing.Point(2, 2);
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.txtFind.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFind.Properties.Appearance.Options.UseFont = true;
            this.txtFind.Properties.Appearance.Options.UseForeColor = true;
            this.txtFind.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFind.Size = new System.Drawing.Size(246, 20);
            this.txtFind.TabIndex = 95;
            this.txtFind.Enter += new System.EventHandler(this.txtFind_Enter);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            this.txtFind.Leave += new System.EventHandler(this.txtFind_Leave);
            // 
            // tvForm
            // 
            this.tvForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvForm.Location = new System.Drawing.Point(2, 22);
            this.tvForm.Margin = new System.Windows.Forms.Padding(0);
            this.tvForm.Name = "tvForm";
            this.tvForm.OptionsBehavior.AllowExpandOnDblClick = false;
            this.tvForm.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tvForm.OptionsSelection.UseIndicatorForSelection = true;
            this.tvForm.OptionsView.ShowColumns = false;
            this.tvForm.OptionsView.ShowHorzLines = false;
            this.tvForm.OptionsView.ShowIndicator = false;
            this.tvForm.OptionsView.ShowVertLines = false;
            this.tvForm.RowHeight = 22;
            this.tvForm.SelectImageList = this.imageList;
            this.tvForm.Size = new System.Drawing.Size(246, 658);
            this.tvForm.StateImageList = this.imageList;
            this.tvForm.TabIndex = 96;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Home_16x16.png");
            this.imageList.Images.SetKeyName(1, "Open_16x16.png");
            this.imageList.Images.SetKeyName(2, "GroupHeader_16x16.png");
            this.imageList.Images.SetKeyName(3, "InsertHeader_16x16.png");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.xtraScrollableControl);
            this.panelControl1.Controls.Add(this.splitterControl2);
            this.panelControl1.Controls.Add(this.plTopR);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(254, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(748, 682);
            this.panelControl1.TabIndex = 12;
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(200)))), ((int)(((byte)(205)))));
            this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl.Controls.Add(this.showPanelForm);
            this.xtraScrollableControl.Controls.Add(this.panel1);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(2, 80);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(744, 600);
            this.xtraScrollableControl.TabIndex = 1;
            // 
            // showPanelForm
            // 
            this.showPanelForm.BackColor = System.Drawing.Color.White;
            this.showPanelForm.Formid = 0;
            this.showPanelForm.FormLayout = null;
            this.showPanelForm.FormXmlData = null;
            this.showPanelForm.HintInfo = null;
            this.showPanelForm.IsAllowSave = false;
            this.showPanelForm.Location = new System.Drawing.Point(12, 10);
            this.showPanelForm.Name = "showPanelForm";
            this.showPanelForm.Size = new System.Drawing.Size(724, 474);
            this.showPanelForm.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 30);
            this.panel1.TabIndex = 0;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl2.Location = new System.Drawing.Point(2, 76);
            this.splitterControl2.MaximumSize = new System.Drawing.Size(0, 4);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(744, 4);
            this.splitterControl2.TabIndex = 2;
            this.splitterControl2.TabStop = false;
            // 
            // plTopR
            // 
            this.plTopR.Controls.Add(this.plMainInfo);
            this.plTopR.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTopR.Location = new System.Drawing.Point(2, 2);
            this.plTopR.Name = "plTopR";
            this.plTopR.Size = new System.Drawing.Size(744, 74);
            this.plTopR.TabIndex = 0;
            // 
            // plMainInfo
            // 
            this.plMainInfo.Controls.Add(this.labelControl1);
            this.plMainInfo.Controls.Add(this.txtCreatDate);
            this.plMainInfo.Controls.Add(this.txtFormCode);
            this.plMainInfo.Controls.Add(this.labelControl19);
            this.plMainInfo.Controls.Add(this.labelControl2);
            this.plMainInfo.Controls.Add(this.labelControl14);
            this.plMainInfo.Controls.Add(this.luePrint);
            this.plMainInfo.Controls.Add(this.txtFormName);
            this.plMainInfo.Controls.Add(this.labelControl13);
            this.plMainInfo.Controls.Add(this.labelControl12);
            this.plMainInfo.Controls.Add(this.labelControl6);
            this.plMainInfo.Controls.Add(this.labelControl10);
            this.plMainInfo.Controls.Add(this.labelControl7);
            this.plMainInfo.Controls.Add(this.rdoStatus);
            this.plMainInfo.Controls.Add(this.labelControl8);
            this.plMainInfo.Controls.Add(this.cboType);
            this.plMainInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.plMainInfo.Location = new System.Drawing.Point(0, 0);
            this.plMainInfo.Name = "plMainInfo";
            this.plMainInfo.Size = new System.Drawing.Size(744, 72);
            this.plMainInfo.TabIndex = 166;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(9, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(10, 23);
            this.labelControl1.TabIndex = 152;
            this.labelControl1.Text = "*";
            // 
            // txtCreatDate
            // 
            this.txtCreatDate.EditValue = "";
            this.txtCreatDate.EnterMoveNextControl = true;
            this.txtCreatDate.Location = new System.Drawing.Point(599, 44);
            this.txtCreatDate.Name = "txtCreatDate";
            this.txtCreatDate.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCreatDate.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtCreatDate.Properties.Appearance.Options.UseFont = true;
            this.txtCreatDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtCreatDate.Properties.ReadOnly = true;
            this.txtCreatDate.Size = new System.Drawing.Size(136, 18);
            this.txtCreatDate.TabIndex = 151;
            // 
            // txtFormCode
            // 
            this.txtFormCode.EnterMoveNextControl = true;
            this.txtFormCode.Location = new System.Drawing.Point(80, 12);
            this.txtFormCode.Name = "txtFormCode";
            this.txtFormCode.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtFormCode.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtFormCode.Properties.Appearance.Options.UseFont = true;
            this.txtFormCode.Properties.Appearance.Options.UseForeColor = true;
            this.txtFormCode.Size = new System.Drawing.Size(108, 20);
            this.txtFormCode.TabIndex = 0;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl19.Location = new System.Drawing.Point(9, 15);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(10, 23);
            this.labelControl19.TabIndex = 107;
            this.labelControl19.Text = "*";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(23, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 12);
            this.labelControl2.TabIndex = 106;
            this.labelControl2.Text = "表单编号:";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl14.Location = new System.Drawing.Point(203, 15);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(10, 23);
            this.labelControl14.TabIndex = 112;
            this.labelControl14.Text = "*";
            // 
            // luePrint
            // 
            this.luePrint.CellValueChanged = true;
            this.luePrint.EditValue = "";
            this.luePrint.EnterMoveNextControl = true;
            this.luePrint.IsButtonFind = false;
            this.luePrint.Location = new System.Drawing.Point(272, 42);
            this.luePrint.MenuManager = this.barManager;
            this.luePrint.MinimumSize = new System.Drawing.Size(0, 22);
            this.luePrint.Name = "luePrint";
            this.luePrint.ParentBandedGridView = null;
            this.luePrint.ParentBindingSource = null;
            this.luePrint.ParentGridView = null;
            this.luePrint.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.luePrint.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.luePrint.Properties.Appearance.Options.UseFont = true;
            this.luePrint.Properties.Appearance.Options.UseForeColor = true;
            this.luePrint.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.luePrint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePrint.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.luePrint.Properties.DataSource = null;
            this.luePrint.Properties.DBRow = null;
            this.luePrint.Properties.DBValue = "";
            this.luePrint.Properties.DescCode = null;
            this.luePrint.Properties.DisplayColumn = null;
            this.luePrint.Properties.DisplayValue = "";
            this.luePrint.Properties.Essential = false;
            this.luePrint.Properties.FieldName = null;
            this.luePrint.Properties.FilterColumn = null;
            this.luePrint.Properties.ForbidPoput = false;
            this.luePrint.Properties.HideColumn = null;
            this.luePrint.Properties.IsAutoPopup = false;
            this.luePrint.Properties.IsCheckValid = true;
            this.luePrint.Properties.IsDescField = false;
            this.luePrint.Properties.IsFreeInput = false;
            this.luePrint.Properties.IsHideValueColumn = false;
            this.luePrint.Properties.IsSelectedMoveNextControl = false;
            this.luePrint.Properties.IsShowColumnHeaders = false;
            this.luePrint.Properties.IsShowDescInfo = false;
            this.luePrint.Properties.IsShowRowNo = false;
            this.luePrint.Properties.IsTab = true;
            this.luePrint.Properties.IsUseShowColumn = false;
            this.luePrint.Properties.LookAndFeel.SkinName = "Office 2010 Blue";
            this.luePrint.Properties.ParentBandedGridView = null;
            this.luePrint.Properties.ParentBindingSource = null;
            this.luePrint.Properties.ParentGridView = null;
            this.luePrint.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.luePrint.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.luePrint.Properties.PopupHeight = 0;
            this.luePrint.Properties.PopupSizeable = false;
            this.luePrint.Properties.PopupWidth = 0;
            this.luePrint.Properties.PresentationMode = 0;
            this.luePrint.Properties.ShowColumn = null;
            this.luePrint.Properties.ShowPopupCloseButton = false;
            this.luePrint.Properties.ShowPopupShadow = false;
            this.luePrint.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.luePrint.Properties.ValueColumn = null;
            this.luePrint.Size = new System.Drawing.Size(242, 20);
            this.luePrint.TabIndex = 5;
            // 
            // txtFormName
            // 
            this.txtFormName.EnterMoveNextControl = true;
            this.txtFormName.Location = new System.Drawing.Point(272, 12);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtFormName.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtFormName.Properties.Appearance.Options.UseFont = true;
            this.txtFormName.Properties.Appearance.Options.UseForeColor = true;
            this.txtFormName.Size = new System.Drawing.Size(242, 20);
            this.txtFormName.TabIndex = 1;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Location = new System.Drawing.Point(216, 16);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(54, 12);
            this.labelControl13.TabIndex = 111;
            this.labelControl13.Text = "表单名称:";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Location = new System.Drawing.Point(216, 47);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(54, 12);
            this.labelControl12.TabIndex = 145;
            this.labelControl12.Text = "打印模板:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(543, 16);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(54, 12);
            this.labelControl6.TabIndex = 132;
            this.labelControl6.Text = "表单类型:";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Location = new System.Drawing.Point(543, 47);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(54, 12);
            this.labelControl10.TabIndex = 144;
            this.labelControl10.Text = "创建时间:";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(23, 47);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(54, 12);
            this.labelControl7.TabIndex = 133;
            this.labelControl7.Text = "表单状态:";
            // 
            // rdoStatus
            // 
            this.rdoStatus.Location = new System.Drawing.Point(79, 42);
            this.rdoStatus.MenuManager = this.barManager;
            this.rdoStatus.Name = "rdoStatus";
            this.rdoStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdoStatus.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoStatus.Properties.Appearance.Options.UseBackColor = true;
            this.rdoStatus.Properties.Appearance.Options.UseFont = true;
            this.rdoStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoStatus.Properties.Columns = 2;
            this.rdoStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "禁用"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "启用")});
            this.rdoStatus.Size = new System.Drawing.Size(125, 24);
            this.rdoStatus.TabIndex = 4;
            this.rdoStatus.SelectedIndexChanged += new System.EventHandler(this.rdoStatus_SelectedIndexChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl8.Location = new System.Drawing.Point(529, 15);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(10, 23);
            this.labelControl8.TabIndex = 138;
            this.labelControl8.Text = "*";
            // 
            // cboType
            // 
            this.cboType.EnterMoveNextControl = true;
            this.cboType.Location = new System.Drawing.Point(599, 12);
            this.cboType.MinimumSize = new System.Drawing.Size(0, 20);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.cboType.Properties.Appearance.Options.UseFont = true;
            this.cboType.Properties.Appearance.Options.UseForeColor = true;
            this.cboType.Properties.AppearanceDisabled.Font = new System.Drawing.Font("宋体", 9.5F);
            this.cboType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Crimson;
            this.cboType.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cboType.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.cboType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9.5F);
            this.cboType.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Crimson;
            this.cboType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboType.Properties.AppearanceDropDown.Options.UseForeColor = true;
            this.cboType.Properties.AppearanceFocused.Font = new System.Drawing.Font("宋体", 9.5F);
            this.cboType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Crimson;
            this.cboType.Properties.AppearanceFocused.Options.UseFont = true;
            this.cboType.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.cboType.Properties.AutoHeight = false;
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.DropDownItemHeight = 22;
            this.cboType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboType.Size = new System.Drawing.Size(136, 20);
            this.cboType.TabIndex = 2;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmFormList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 682);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Name = "frmFormList";
            this.Text = "表单管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCaseTemplate_FormClosing);
            this.Load += new System.EventHandler(this.frmCaseTemplate_Load);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.xtraScrollableControl.ResumeLayout(false);
            this.plTopR.ResumeLayout(false);
            this.plMainInfo.ResumeLayout(false);
            this.plMainInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePrint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        internal DevExpress.XtraEditors.LabelControl lblFind;
        internal DevExpress.XtraEditors.TextEdit txtFind;
        internal DevExpress.XtraTreeList.TreeList tvForm;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        internal DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        internal System.Windows.Forms.ImageList imageList;
        internal System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panel1;
        internal Common.Controls.ShowPanelForm showPanelForm;
        internal System.Windows.Forms.Panel plTopR;
        internal System.Windows.Forms.Panel plMainInfo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.TextEdit txtCreatDate;
        internal DevExpress.XtraEditors.TextEdit txtFormCode;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        internal LookUpEdit luePrint;
        internal DevExpress.XtraEditors.TextEdit txtFormName;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        internal DevExpress.XtraEditors.RadioGroup rdoStatus;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        internal DevExpress.XtraEditors.ComboBoxEdit cboType;
    }
}