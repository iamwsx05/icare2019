namespace Registration.Ui
{
    partial class frmRegisterStatStopAdmi
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
            this.gcNo = new DevExpress.XtraGrid.GridControl();
            this.gvNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTimeEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.lueDoct = new Common.Controls.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lueDept = new Common.Controls.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.lueDoct);
            this.pcBackGround.Controls.Add(this.labelControl3);
            this.pcBackGround.Controls.Add(this.lueDept);
            this.pcBackGround.Controls.Add(this.labelControl1);
            this.pcBackGround.Controls.Add(this.labelControl7);
            this.pcBackGround.Controls.Add(this.labelControl2);
            this.pcBackGround.Controls.Add(this.dteEnd);
            this.pcBackGround.Controls.Add(this.dteStart);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(971, 35);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // gcNo
            // 
            this.gcNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNo.Location = new System.Drawing.Point(0, 35);
            this.gcNo.MainView = this.gvNo;
            this.gcNo.Margin = new System.Windows.Forms.Padding(0);
            this.gcNo.Name = "gcNo";
            this.gcNo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTimeEdit1,
            this.repositoryItemTimeEdit2,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this.repositoryItemTextEdit4,
            this.repositoryItemTextEdit5,
            this.repositoryItemTextEdit6,
            this.repositoryItemCheckEdit1,
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2});
            this.gcNo.Size = new System.Drawing.Size(971, 563);
            this.gcNo.TabIndex = 64;
            this.gcNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNo});
            // 
            // gvNo
            // 
            this.gvNo.Appearance.Row.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gvNo.Appearance.Row.Options.UseFont = true;
            this.gvNo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn9,
            this.gridColumn1,
            this.gridColumn10,
            this.gridColumn6});
            this.gvNo.GridControl = this.gcNo;
            this.gvNo.IndicatorWidth = 30;
            this.gvNo.Name = "gvNo";
            this.gvNo.OptionsView.ColumnAutoWidth = false;
            this.gvNo.OptionsView.ShowDetailButtons = false;
            this.gvNo.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvNo.OptionsView.ShowGroupPanel = false;
            this.gvNo.RowHeight = 25;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn5.AppearanceCell.ForeColor = System.Drawing.Color.Crimson;
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "状态";
            this.gridColumn5.FieldName = "comment";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F);
            this.gridColumn9.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.Caption = "出诊时间";
            this.gridColumn9.FieldName = "regDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn9.OptionsFilter.AllowFilter = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            this.gridColumn9.Width = 120;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.Caption = "科别";
            this.gridColumn10.FieldName = "deptName";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn10.OptionsFilter.AllowFilter = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 120;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.Caption = "医师";
            this.gridColumn6.FieldName = "doctName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn6.OptionsFilter.AllowFilter = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 120;
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.Appearance.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemTimeEdit1.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTimeEdit1.Appearance.Options.UseFont = true;
            this.repositoryItemTimeEdit1.Appearance.Options.UseForeColor = true;
            this.repositoryItemTimeEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTimeEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";
            this.repositoryItemTimeEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            this.repositoryItemTimeEdit1.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            // 
            // repositoryItemTimeEdit2
            // 
            this.repositoryItemTimeEdit2.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTimeEdit2.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTimeEdit2.Appearance.Options.UseFont = true;
            this.repositoryItemTimeEdit2.Appearance.Options.UseForeColor = true;
            this.repositoryItemTimeEdit2.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTimeEdit2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTimeEdit2.AutoHeight = false;
            this.repositoryItemTimeEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit2.Mask.EditMask = "HH:mm";
            this.repositoryItemTimeEdit2.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTimeEdit2.Name = "repositoryItemTimeEdit2";
            this.repositoryItemTimeEdit2.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit1.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit1.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit1.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "###";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit2.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit2.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit2.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit2.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Mask.EditMask = "###";
            this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit3.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit3.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit3.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit3.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Mask.EditMask = "###";
            this.repositoryItemTextEdit3.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // repositoryItemTextEdit4
            // 
            this.repositoryItemTextEdit4.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit4.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit4.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit4.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit4.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit4.AutoHeight = false;
            this.repositoryItemTextEdit4.Mask.EditMask = "###";
            this.repositoryItemTextEdit4.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            // 
            // repositoryItemTextEdit5
            // 
            this.repositoryItemTextEdit5.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit5.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit5.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit5.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit5.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit5.AutoHeight = false;
            this.repositoryItemTextEdit5.Mask.EditMask = "###";
            this.repositoryItemTextEdit5.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit5.Name = "repositoryItemTextEdit5";
            // 
            // repositoryItemTextEdit6
            // 
            this.repositoryItemTextEdit6.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit6.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit6.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit6.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit6.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit6.AutoHeight = false;
            this.repositoryItemTextEdit6.Mask.EditMask = "###";
            this.repositoryItemTextEdit6.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit6.Name = "repositoryItemTextEdit6";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit2.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // lueDoct
            // 
            this.lueDoct.CellValueChanged = true;
            this.lueDoct.EditValue = "";
            this.lueDoct.IsButtonFind = false;
            this.lueDoct.Location = new System.Drawing.Point(477, 7);
            this.lueDoct.Name = "lueDoct";
            this.lueDoct.ParentBandedGridView = null;
            this.lueDoct.ParentBindingSource = null;
            this.lueDoct.ParentGridView = null;
            this.lueDoct.Properties.Appearance.Font = new System.Drawing.Font("黑体", 12F);
            this.lueDoct.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueDoct.Properties.Appearance.Options.UseFont = true;
            this.lueDoct.Properties.Appearance.Options.UseForeColor = true;
            this.lueDoct.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueDoct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDoct.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueDoct.Properties.DataSource = null;
            this.lueDoct.Properties.DBRow = null;
            this.lueDoct.Properties.DBValue = "";
            this.lueDoct.Properties.DescCode = null;
            this.lueDoct.Properties.DisplayColumn = null;
            this.lueDoct.Properties.DisplayValue = "";
            this.lueDoct.Properties.Essential = false;
            this.lueDoct.Properties.FieldName = null;
            this.lueDoct.Properties.FilterColumn = null;
            this.lueDoct.Properties.ForbidPoput = false;
            this.lueDoct.Properties.HideColumn = null;
            this.lueDoct.Properties.IsAutoPopup = false;
            this.lueDoct.Properties.IsCheckValid = true;
            this.lueDoct.Properties.IsDescField = false;
            this.lueDoct.Properties.IsFreeInput = false;
            this.lueDoct.Properties.IsHideValueColumn = false;
            this.lueDoct.Properties.IsSelectedMoveNextControl = false;
            this.lueDoct.Properties.IsShowColumnHeaders = false;
            this.lueDoct.Properties.IsShowDescInfo = false;
            this.lueDoct.Properties.IsShowRowNo = false;
            this.lueDoct.Properties.IsTab = true;
            this.lueDoct.Properties.IsUseShowColumn = false;
            this.lueDoct.Properties.ParentBandedGridView = null;
            this.lueDoct.Properties.ParentBindingSource = null;
            this.lueDoct.Properties.ParentGridView = null;
            this.lueDoct.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueDoct.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueDoct.Properties.PopupHeight = 0;
            this.lueDoct.Properties.PopupSizeable = false;
            this.lueDoct.Properties.PopupWidth = 0;
            this.lueDoct.Properties.PresentationMode = 0;
            this.lueDoct.Properties.ShowColumn = null;
            this.lueDoct.Properties.ShowPopupCloseButton = false;
            this.lueDoct.Properties.ShowPopupShadow = false;
            this.lueDoct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDoct.Properties.ValueColumn = null;
            this.lueDoct.Size = new System.Drawing.Size(100, 22);
            this.lueDoct.TabIndex = 79;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(444, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 12);
            this.labelControl3.TabIndex = 78;
            this.labelControl3.Text = "医师:";
            // 
            // lueDept
            // 
            this.lueDept.CellValueChanged = true;
            this.lueDept.EditValue = "";
            this.lueDept.IsButtonFind = false;
            this.lueDept.Location = new System.Drawing.Point(332, 7);
            this.lueDept.Name = "lueDept";
            this.lueDept.ParentBandedGridView = null;
            this.lueDept.ParentBindingSource = null;
            this.lueDept.ParentGridView = null;
            this.lueDept.Properties.Appearance.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lueDept.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueDept.Properties.Appearance.Options.UseFont = true;
            this.lueDept.Properties.Appearance.Options.UseForeColor = true;
            this.lueDept.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDept.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueDept.Properties.DataSource = null;
            this.lueDept.Properties.DBRow = null;
            this.lueDept.Properties.DBValue = "";
            this.lueDept.Properties.DescCode = null;
            this.lueDept.Properties.DisplayColumn = null;
            this.lueDept.Properties.DisplayValue = "";
            this.lueDept.Properties.Essential = false;
            this.lueDept.Properties.FieldName = null;
            this.lueDept.Properties.FilterColumn = null;
            this.lueDept.Properties.ForbidPoput = false;
            this.lueDept.Properties.HideColumn = null;
            this.lueDept.Properties.IsAutoPopup = false;
            this.lueDept.Properties.IsCheckValid = true;
            this.lueDept.Properties.IsDescField = false;
            this.lueDept.Properties.IsFreeInput = false;
            this.lueDept.Properties.IsHideValueColumn = false;
            this.lueDept.Properties.IsSelectedMoveNextControl = false;
            this.lueDept.Properties.IsShowColumnHeaders = false;
            this.lueDept.Properties.IsShowDescInfo = false;
            this.lueDept.Properties.IsShowRowNo = false;
            this.lueDept.Properties.IsTab = true;
            this.lueDept.Properties.IsUseShowColumn = false;
            this.lueDept.Properties.ParentBandedGridView = null;
            this.lueDept.Properties.ParentBindingSource = null;
            this.lueDept.Properties.ParentGridView = null;
            this.lueDept.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueDept.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueDept.Properties.PopupHeight = 0;
            this.lueDept.Properties.PopupSizeable = false;
            this.lueDept.Properties.PopupWidth = 0;
            this.lueDept.Properties.PresentationMode = 0;
            this.lueDept.Properties.ShowColumn = null;
            this.lueDept.Properties.ShowPopupCloseButton = false;
            this.lueDept.Properties.ShowPopupShadow = false;
            this.lueDept.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDept.Properties.ValueColumn = null;
            this.lueDept.Size = new System.Drawing.Size(100, 22);
            this.lueDept.TabIndex = 77;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(299, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 12);
            this.labelControl1.TabIndex = 76;
            this.labelControl1.Text = "科室:";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(7, 12);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(54, 12);
            this.labelControl7.TabIndex = 75;
            this.labelControl7.Text = "出诊日期:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(172, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(7, 12);
            this.labelControl2.TabIndex = 74;
            this.labelControl2.Text = "~";
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Location = new System.Drawing.Point(186, 7);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dteEnd.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteEnd.Properties.Appearance.Options.UseFont = true;
            this.dteEnd.Properties.Appearance.Options.UseForeColor = true;
            this.dteEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteEnd.Size = new System.Drawing.Size(100, 22);
            this.dteEnd.TabIndex = 73;
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Location = new System.Drawing.Point(65, 7);
            this.dteStart.Name = "dteStart";
            this.dteStart.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dteStart.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteStart.Properties.Appearance.Options.UseFont = true;
            this.dteStart.Properties.Appearance.Options.UseForeColor = true;
            this.dteStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteStart.Size = new System.Drawing.Size(100, 22);
            this.dteStart.TabIndex = 72;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "上/下午";
            this.gridColumn1.FieldName = "amPmName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 67;
            // 
            // frmRegisterStatStopAdmi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 598);
            this.Controls.Add(this.gcNo);
            this.Name = "frmRegisterStatStopAdmi";
            this.Text = "停诊查询";
            this.Load += new System.EventHandler(this.frmRegisterStatStopAdmi_Load);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.gcNo, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gcNo;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        internal Common.Controls.LookUpEdit lueDoct;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal Common.Controls.LookUpEdit lueDept;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.DateEdit dteEnd;
        internal DevExpress.XtraEditors.DateEdit dteStart;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}