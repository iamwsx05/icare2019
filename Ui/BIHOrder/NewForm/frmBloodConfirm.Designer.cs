namespace com.digitalwave.iCare.BIHOrder
{
    partial class frmBloodConfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBloodConfirm));
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.blbiQuery = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiSend = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiBack = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiPrint = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pcBack = new DevExpress.XtraEditors.PanelControl();
            this.gcApply = new DevExpress.XtraGrid.GridControl();
            this.gvApply = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.statusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fappdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.appDoctName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ipNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.patName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.age = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bedNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.doctName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.inDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sendDoctName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fsenddate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fappid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.txtIpNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBack)).BeginInit();
            this.pcBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIpNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.blbiSend,
            this.blbiBack,
            this.blbiPrint,
            this.blbiQuery,
            this.blbiClose});
            this.barManager.MaxItemId = 5;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiQuery, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiSend, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiBack, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // blbiQuery
            // 
            this.blbiQuery.Caption = "查询";
            this.blbiQuery.Id = 3;
            this.blbiQuery.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("blbiQuery.ImageOptions.Image")));
            this.blbiQuery.Name = "blbiQuery";
            this.blbiQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiQuery_ItemClick);
            // 
            // blbiSend
            // 
            this.blbiSend.Caption = "审核发送";
            this.blbiSend.Id = 0;
            this.blbiSend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("blbiSend.ImageOptions.Image")));
            this.blbiSend.Name = "blbiSend";
            this.blbiSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiSend_ItemClick);
            // 
            // blbiBack
            // 
            this.blbiBack.Caption = "拒绝退回";
            this.blbiBack.Id = 1;
            this.blbiBack.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("blbiBack.ImageOptions.Image")));
            this.blbiBack.Name = "blbiBack";
            this.blbiBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiBack_ItemClick);
            // 
            // blbiPrint
            // 
            this.blbiPrint.Caption = "打印";
            this.blbiPrint.Id = 2;
            this.blbiPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("blbiPrint.ImageOptions.Image")));
            this.blbiPrint.Name = "blbiPrint";
            this.blbiPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiPrint_ItemClick);
            // 
            // blbiClose
            // 
            this.blbiClose.Caption = "关闭";
            this.blbiClose.Id = 4;
            this.blbiClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("blbiClose.ImageOptions.Image")));
            this.blbiClose.Name = "blbiClose";
            this.blbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1135, 65);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 690);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1135, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 65);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 625);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1135, 65);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 625);
            // 
            // pcBack
            // 
            this.pcBack.Controls.Add(this.gcApply);
            this.pcBack.Controls.Add(this.panelControl1);
            this.pcBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcBack.Location = new System.Drawing.Point(0, 65);
            this.pcBack.Name = "pcBack";
            this.pcBack.Size = new System.Drawing.Size(1135, 625);
            this.pcBack.TabIndex = 4;
            // 
            // gcApply
            // 
            this.gcApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcApply.Location = new System.Drawing.Point(2, 40);
            this.gcApply.MainView = this.gvApply;
            this.gcApply.MenuManager = this.barManager;
            this.gcApply.Name = "gcApply";
            this.gcApply.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.gcApply.Size = new System.Drawing.Size(1131, 583);
            this.gcApply.TabIndex = 0;
            this.gcApply.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvApply});
            this.gcApply.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gcApply_MouseDoubleClick);
            // 
            // gvApply
            // 
            this.gvApply.Appearance.HeaderPanel.Font = new System.Drawing.Font("宋体", 9F);
            this.gvApply.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvApply.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvApply.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvApply.Appearance.Row.Font = new System.Drawing.Font("宋体", 9F);
            this.gvApply.Appearance.Row.Options.UseFont = true;
            this.gvApply.Appearance.Row.Options.UseTextOptions = true;
            this.gvApply.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvApply.ColumnPanelRowHeight = 28;
            this.gvApply.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.statusName,
            this.fappdate,
            this.appDoctName,
            this.ipNo,
            this.patName,
            this.sex,
            this.age,
            this.deptName,
            this.bedNo,
            this.doctName,
            this.inDate,
            this.sendDoctName,
            this.fsenddate,
            this.fappid});
            this.gvApply.GridControl = this.gcApply;
            this.gvApply.IndicatorWidth = 34;
            this.gvApply.Name = "gvApply";
            this.gvApply.OptionsView.ColumnAutoWidth = false;
            this.gvApply.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gvApply.OptionsView.ShowDetailButtons = false;
            this.gvApply.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvApply.OptionsView.ShowGroupPanel = false;
            this.gvApply.RowHeight = 25;
            this.gvApply.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvApply_CustomDrawRowIndicator);
            // 
            // statusName
            // 
            this.statusName.Caption = "状态";
            this.statusName.FieldName = "statusName";
            this.statusName.Name = "statusName";
            this.statusName.OptionsColumn.AllowEdit = false;
            this.statusName.OptionsColumn.AllowFocus = false;
            this.statusName.OptionsFilter.AllowAutoFilter = false;
            this.statusName.OptionsFilter.AllowFilter = false;
            this.statusName.Visible = true;
            this.statusName.VisibleIndex = 0;
            this.statusName.Width = 60;
            // 
            // fappdate
            // 
            this.fappdate.Caption = "申请时间";
            this.fappdate.ColumnEdit = this.repositoryItemDateEdit1;
            this.fappdate.FieldName = "fappdate";
            this.fappdate.Name = "fappdate";
            this.fappdate.OptionsColumn.AllowEdit = false;
            this.fappdate.OptionsColumn.AllowFocus = false;
            this.fappdate.OptionsFilter.AllowAutoFilter = false;
            this.fappdate.OptionsFilter.AllowFilter = false;
            this.fappdate.Visible = true;
            this.fappdate.VisibleIndex = 1;
            this.fappdate.Width = 108;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // appDoctName
            // 
            this.appDoctName.Caption = "申请人";
            this.appDoctName.FieldName = "appDoctName";
            this.appDoctName.Name = "appDoctName";
            this.appDoctName.OptionsColumn.AllowEdit = false;
            this.appDoctName.OptionsColumn.AllowFocus = false;
            this.appDoctName.OptionsFilter.AllowAutoFilter = false;
            this.appDoctName.OptionsFilter.AllowFilter = false;
            this.appDoctName.Visible = true;
            this.appDoctName.VisibleIndex = 2;
            this.appDoctName.Width = 70;
            // 
            // ipNo
            // 
            this.ipNo.Caption = "住院号";
            this.ipNo.FieldName = "ipNo";
            this.ipNo.Name = "ipNo";
            this.ipNo.OptionsColumn.AllowEdit = false;
            this.ipNo.OptionsColumn.AllowFocus = false;
            this.ipNo.OptionsFilter.AllowAutoFilter = false;
            this.ipNo.OptionsFilter.AllowFilter = false;
            this.ipNo.Visible = true;
            this.ipNo.VisibleIndex = 3;
            this.ipNo.Width = 76;
            // 
            // patName
            // 
            this.patName.Caption = "姓名";
            this.patName.FieldName = "patName";
            this.patName.Name = "patName";
            this.patName.OptionsColumn.AllowEdit = false;
            this.patName.OptionsColumn.AllowFocus = false;
            this.patName.OptionsFilter.AllowAutoFilter = false;
            this.patName.OptionsFilter.AllowFilter = false;
            this.patName.Visible = true;
            this.patName.VisibleIndex = 4;
            this.patName.Width = 70;
            // 
            // sex
            // 
            this.sex.Caption = "性别";
            this.sex.FieldName = "sex";
            this.sex.Name = "sex";
            this.sex.OptionsColumn.AllowEdit = false;
            this.sex.OptionsColumn.AllowFocus = false;
            this.sex.OptionsFilter.AllowAutoFilter = false;
            this.sex.OptionsFilter.AllowFilter = false;
            this.sex.Visible = true;
            this.sex.VisibleIndex = 5;
            this.sex.Width = 41;
            // 
            // age
            // 
            this.age.Caption = "年龄";
            this.age.FieldName = "age";
            this.age.Name = "age";
            this.age.OptionsColumn.AllowEdit = false;
            this.age.OptionsColumn.AllowFocus = false;
            this.age.OptionsFilter.AllowAutoFilter = false;
            this.age.OptionsFilter.AllowFilter = false;
            this.age.Visible = true;
            this.age.VisibleIndex = 6;
            this.age.Width = 96;
            // 
            // deptName
            // 
            this.deptName.Caption = "科室名称";
            this.deptName.FieldName = "deptName";
            this.deptName.Name = "deptName";
            this.deptName.OptionsColumn.AllowEdit = false;
            this.deptName.OptionsColumn.AllowFocus = false;
            this.deptName.OptionsFilter.AllowAutoFilter = false;
            this.deptName.OptionsFilter.AllowFilter = false;
            this.deptName.Visible = true;
            this.deptName.VisibleIndex = 7;
            this.deptName.Width = 113;
            // 
            // bedNo
            // 
            this.bedNo.Caption = "床号";
            this.bedNo.FieldName = "bedNo";
            this.bedNo.Name = "bedNo";
            this.bedNo.OptionsColumn.AllowEdit = false;
            this.bedNo.OptionsColumn.AllowFocus = false;
            this.bedNo.OptionsFilter.AllowAutoFilter = false;
            this.bedNo.OptionsFilter.AllowFilter = false;
            this.bedNo.Visible = true;
            this.bedNo.VisibleIndex = 8;
            this.bedNo.Width = 50;
            // 
            // doctName
            // 
            this.doctName.Caption = "主治医师";
            this.doctName.FieldName = "doctName";
            this.doctName.Name = "doctName";
            this.doctName.OptionsColumn.AllowEdit = false;
            this.doctName.OptionsColumn.AllowFocus = false;
            this.doctName.OptionsFilter.AllowAutoFilter = false;
            this.doctName.OptionsFilter.AllowFilter = false;
            this.doctName.Visible = true;
            this.doctName.VisibleIndex = 9;
            this.doctName.Width = 70;
            // 
            // inDate
            // 
            this.inDate.Caption = "入院时间";
            this.inDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.inDate.FieldName = "inDate";
            this.inDate.Name = "inDate";
            this.inDate.OptionsColumn.AllowEdit = false;
            this.inDate.OptionsColumn.AllowFocus = false;
            this.inDate.OptionsFilter.AllowAutoFilter = false;
            this.inDate.OptionsFilter.AllowFilter = false;
            this.inDate.Visible = true;
            this.inDate.VisibleIndex = 10;
            this.inDate.Width = 108;
            // 
            // sendDoctName
            // 
            this.sendDoctName.Caption = "审核发送人";
            this.sendDoctName.FieldName = "sendDoctName";
            this.sendDoctName.Name = "sendDoctName";
            this.sendDoctName.OptionsColumn.AllowEdit = false;
            this.sendDoctName.OptionsColumn.AllowFocus = false;
            this.sendDoctName.OptionsFilter.AllowAutoFilter = false;
            this.sendDoctName.OptionsFilter.AllowFilter = false;
            this.sendDoctName.Visible = true;
            this.sendDoctName.VisibleIndex = 11;
            this.sendDoctName.Width = 70;
            // 
            // fsenddate
            // 
            this.fsenddate.Caption = "审核发送时间";
            this.fsenddate.ColumnEdit = this.repositoryItemDateEdit1;
            this.fsenddate.FieldName = "fsenddate";
            this.fsenddate.Name = "fsenddate";
            this.fsenddate.OptionsColumn.AllowEdit = false;
            this.fsenddate.OptionsColumn.AllowFocus = false;
            this.fsenddate.OptionsFilter.AllowAutoFilter = false;
            this.fsenddate.OptionsFilter.AllowFilter = false;
            this.fsenddate.Visible = true;
            this.fsenddate.VisibleIndex = 12;
            this.fsenddate.Width = 108;
            // 
            // fappid
            // 
            this.fappid.Caption = "fappid";
            this.fappid.FieldName = "fappid";
            this.fappid.Name = "fappid";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cboStatus);
            this.panelControl1.Controls.Add(this.dteEnd);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.dteStart);
            this.panelControl1.Controls.Add(this.txtIpNo);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1131, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(604, 9);
            this.cboStatus.MenuManager = this.barManager;
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStatus.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Appearance.Options.UseForeColor = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.DropDownItemHeight = 23;
            this.cboStatus.Properties.Items.AddRange(new object[] {
            " ",
            "提交",
            "发送"});
            this.cboStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboStatus.Size = new System.Drawing.Size(100, 20);
            this.cboStatus.TabIndex = 7;
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Location = new System.Drawing.Point(401, 8);
            this.dteEnd.MenuManager = this.barManager;
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dteEnd.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteEnd.Properties.Appearance.Options.UseFont = true;
            this.dteEnd.Properties.Appearance.Options.UseForeColor = true;
            this.dteEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteEnd.Size = new System.Drawing.Size(116, 22);
            this.dteEnd.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(385, 13);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(12, 12);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "到";
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Location = new System.Drawing.Point(264, 8);
            this.dteStart.MenuManager = this.barManager;
            this.dteStart.Name = "dteStart";
            this.dteStart.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.dteStart.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteStart.Properties.Appearance.Options.UseFont = true;
            this.dteStart.Properties.Appearance.Options.UseForeColor = true;
            this.dteStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteStart.Size = new System.Drawing.Size(116, 22);
            this.dteStart.TabIndex = 4;
            // 
            // txtIpNo
            // 
            this.txtIpNo.EditValue = "";
            this.txtIpNo.Location = new System.Drawing.Point(65, 7);
            this.txtIpNo.MenuManager = this.barManager;
            this.txtIpNo.Name = "txtIpNo";
            this.txtIpNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpNo.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.txtIpNo.Properties.Appearance.Options.UseFont = true;
            this.txtIpNo.Properties.Appearance.Options.UseForeColor = true;
            this.txtIpNo.Size = new System.Drawing.Size(89, 22);
            this.txtIpNo.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(542, 13);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 12);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "申请状态：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(181, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 12);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "申请日期： 从";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(17, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "住院号：";
            // 
            // frmBloodConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 690);
            this.Controls.Add(this.pcBack);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBloodConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "临床用血申请审核";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBloodConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBack)).EndInit();
            this.pcBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIpNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl pcBack;
        private DevExpress.XtraBars.BarLargeButtonItem blbiQuery;
        private DevExpress.XtraBars.BarLargeButtonItem blbiSend;
        private DevExpress.XtraBars.BarLargeButtonItem blbiBack;
        private DevExpress.XtraBars.BarLargeButtonItem blbiPrint;
        private DevExpress.XtraBars.BarLargeButtonItem blbiClose;
        private DevExpress.XtraGrid.GridControl gcApply;
        private DevExpress.XtraGrid.Views.Grid.GridView gvApply;
        private DevExpress.XtraGrid.Columns.GridColumn statusName;
        private DevExpress.XtraGrid.Columns.GridColumn ipNo;
        private DevExpress.XtraGrid.Columns.GridColumn patName;
        private DevExpress.XtraGrid.Columns.GridColumn sex;
        private DevExpress.XtraGrid.Columns.GridColumn age;
        private DevExpress.XtraGrid.Columns.GridColumn deptName;
        private DevExpress.XtraGrid.Columns.GridColumn bedNo;
        private DevExpress.XtraGrid.Columns.GridColumn doctName;
        private DevExpress.XtraGrid.Columns.GridColumn inDate;
        private DevExpress.XtraGrid.Columns.GridColumn fappdate;
        private DevExpress.XtraGrid.Columns.GridColumn sendDoctName;
        private DevExpress.XtraGrid.Columns.GridColumn fsenddate;
        private DevExpress.XtraGrid.Columns.GridColumn fappid;
        private DevExpress.XtraGrid.Columns.GridColumn appDoctName;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboStatus;
        private DevExpress.XtraEditors.DateEdit dteEnd;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dteStart;
        private DevExpress.XtraEditors.TextEdit txtIpNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}