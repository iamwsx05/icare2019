namespace Common.Controls
{
    partial class frmSelectFields
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectFields));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.lueForm = new Common.Controls.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gcFields = new DevExpress.XtraGrid.GridControl();
            this.gvFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTimeEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTextEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit10 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit11 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit12 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit13 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit14 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueForm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.gcFields);
            this.pcBackGround.Controls.Add(this.panelControl1);
            this.pcBackGround.Size = new System.Drawing.Size(519, 730);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Controls.Add(this.labelControl27);
            this.panelControl1.Controls.Add(this.lueForm);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(515, 34);
            this.panelControl1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(425, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 24);
            this.btnClose.TabIndex = 133;
            this.btnClose.Text = "关闭 &C";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(338, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(78, 24);
            this.btnOk.TabIndex = 132;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelControl27
            // 
            this.labelControl27.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl27.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl27.Location = new System.Drawing.Point(6, 10);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(10, 23);
            this.labelControl27.TabIndex = 49;
            this.labelControl27.Text = "*";
            // 
            // lueForm
            // 
            this.lueForm.CellValueChanged = true;
            this.lueForm.EditValue = "";
            this.lueForm.EnterMoveNextControl = true;
            this.lueForm.IsButtonFind = false;
            this.lueForm.Location = new System.Drawing.Point(81, 7);
            this.lueForm.Name = "lueForm";
            this.lueForm.ParentBandedGridView = null;
            this.lueForm.ParentBindingSource = null;
            this.lueForm.ParentGridView = null;
            this.lueForm.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.lueForm.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueForm.Properties.Appearance.Options.UseFont = true;
            this.lueForm.Properties.Appearance.Options.UseForeColor = true;
            this.lueForm.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueForm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueForm.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueForm.Properties.DataSource = null;
            this.lueForm.Properties.DBRow = null;
            this.lueForm.Properties.DBValue = "";
            this.lueForm.Properties.DescCode = null;
            this.lueForm.Properties.DisplayColumn = null;
            this.lueForm.Properties.DisplayValue = "";
            this.lueForm.Properties.Essential = false;
            this.lueForm.Properties.FieldName = null;
            this.lueForm.Properties.FilterColumn = null;
            this.lueForm.Properties.ForbidPoput = false;
            this.lueForm.Properties.HideColumn = null;
            this.lueForm.Properties.IsAutoPopup = false;
            this.lueForm.Properties.IsCheckValid = true;
            this.lueForm.Properties.IsDescField = false;
            this.lueForm.Properties.IsFreeInput = false;
            this.lueForm.Properties.IsHideValueColumn = false;
            this.lueForm.Properties.IsSelectedMoveNextControl = false;
            this.lueForm.Properties.IsShowColumnHeaders = false;
            this.lueForm.Properties.IsShowDescInfo = false;
            this.lueForm.Properties.IsShowRowNo = false;
            this.lueForm.Properties.IsTab = true;
            this.lueForm.Properties.IsUseShowColumn = false;
            this.lueForm.Properties.ParentBandedGridView = null;
            this.lueForm.Properties.ParentBindingSource = null;
            this.lueForm.Properties.ParentGridView = null;
            this.lueForm.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueForm.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueForm.Properties.PopupHeight = 0;
            this.lueForm.Properties.PopupSizeable = false;
            this.lueForm.Properties.PopupWidth = 0;
            this.lueForm.Properties.PresentationMode = 0;
            this.lueForm.Properties.ShowColumn = null;
            this.lueForm.Properties.ShowPopupCloseButton = false;
            this.lueForm.Properties.ShowPopupShadow = false;
            this.lueForm.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueForm.Properties.ValueColumn = null;
            this.lueForm.Size = new System.Drawing.Size(240, 20);
            this.lueForm.TabIndex = 47;
            this.lueForm.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueForm_HandleDBValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(20, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 12);
            this.labelControl3.TabIndex = 48;
            this.labelControl3.Text = "表单/表格:";
            // 
            // gcFields
            // 
            this.gcFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcFields.Location = new System.Drawing.Point(2, 36);
            this.gcFields.MainView = this.gvFields;
            this.gcFields.Margin = new System.Windows.Forms.Padding(0);
            this.gcFields.Name = "gcFields";
            this.gcFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTimeEdit3,
            this.repositoryItemTimeEdit4,
            this.repositoryItemTextEdit8,
            this.repositoryItemTextEdit9,
            this.repositoryItemTextEdit10,
            this.repositoryItemTextEdit11,
            this.repositoryItemTextEdit12,
            this.repositoryItemTextEdit13,
            this.repositoryItemCheckEdit2,
            this.repositoryItemTextEdit14,
            this.repositoryItemComboBox2,
            this.repositoryItemCheckEdit1});
            this.gcFields.Size = new System.Drawing.Size(515, 692);
            this.gcFields.TabIndex = 19;
            this.gcFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFields});
            // 
            // gvFields
            // 
            this.gvFields.Appearance.Row.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gvFields.Appearance.Row.Options.UseFont = true;
            this.gvFields.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4});
            this.gvFields.GridControl = this.gcFields;
            this.gvFields.Name = "gvFields";
            this.gvFields.OptionsView.ColumnAutoWidth = false;
            this.gvFields.OptionsView.ShowDetailButtons = false;
            this.gvFields.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvFields.OptionsView.ShowGroupPanel = false;
            this.gvFields.RowHeight = 25;
            this.gvFields.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvFields_CustomDrawColumnHeader);
            this.gvFields.Click += new System.EventHandler(this.gvFields_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = " ";
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "check";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 38;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "字段编码";
            this.gridColumn3.FieldName = "itemName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 141;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn4.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "字段描述";
            this.gridColumn4.FieldName = "itemCaption";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 299;
            // 
            // repositoryItemTimeEdit3
            // 
            this.repositoryItemTimeEdit3.Appearance.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemTimeEdit3.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTimeEdit3.Appearance.Options.UseFont = true;
            this.repositoryItemTimeEdit3.Appearance.Options.UseForeColor = true;
            this.repositoryItemTimeEdit3.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTimeEdit3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTimeEdit3.AutoHeight = false;
            this.repositoryItemTimeEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit3.Mask.EditMask = "HH:mm";
            this.repositoryItemTimeEdit3.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTimeEdit3.Name = "repositoryItemTimeEdit3";
            this.repositoryItemTimeEdit3.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            // 
            // repositoryItemTimeEdit4
            // 
            this.repositoryItemTimeEdit4.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTimeEdit4.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTimeEdit4.Appearance.Options.UseFont = true;
            this.repositoryItemTimeEdit4.Appearance.Options.UseForeColor = true;
            this.repositoryItemTimeEdit4.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTimeEdit4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTimeEdit4.AutoHeight = false;
            this.repositoryItemTimeEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit4.Mask.EditMask = "HH:mm";
            this.repositoryItemTimeEdit4.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTimeEdit4.Name = "repositoryItemTimeEdit4";
            this.repositoryItemTimeEdit4.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            // 
            // repositoryItemTextEdit8
            // 
            this.repositoryItemTextEdit8.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit8.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit8.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit8.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit8.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit8.AutoHeight = false;
            this.repositoryItemTextEdit8.Mask.EditMask = "####.##";
            this.repositoryItemTextEdit8.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit8.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit8.Name = "repositoryItemTextEdit8";
            // 
            // repositoryItemTextEdit9
            // 
            this.repositoryItemTextEdit9.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit9.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit9.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit9.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit9.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit9.AutoHeight = false;
            this.repositoryItemTextEdit9.Mask.EditMask = "###";
            this.repositoryItemTextEdit9.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit9.Name = "repositoryItemTextEdit9";
            // 
            // repositoryItemTextEdit10
            // 
            this.repositoryItemTextEdit10.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit10.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit10.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit10.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit10.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit10.AutoHeight = false;
            this.repositoryItemTextEdit10.Mask.EditMask = "###";
            this.repositoryItemTextEdit10.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit10.Name = "repositoryItemTextEdit10";
            // 
            // repositoryItemTextEdit11
            // 
            this.repositoryItemTextEdit11.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit11.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit11.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit11.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit11.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit11.AutoHeight = false;
            this.repositoryItemTextEdit11.Mask.EditMask = "###";
            this.repositoryItemTextEdit11.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit11.Name = "repositoryItemTextEdit11";
            // 
            // repositoryItemTextEdit12
            // 
            this.repositoryItemTextEdit12.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit12.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit12.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit12.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit12.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit12.AutoHeight = false;
            this.repositoryItemTextEdit12.Mask.EditMask = "###";
            this.repositoryItemTextEdit12.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit12.Name = "repositoryItemTextEdit12";
            // 
            // repositoryItemTextEdit13
            // 
            this.repositoryItemTextEdit13.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit13.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit13.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit13.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit13.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit13.AutoHeight = false;
            this.repositoryItemTextEdit13.Mask.EditMask = "###";
            this.repositoryItemTextEdit13.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit13.Name = "repositoryItemTextEdit13";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Caption = "Check";
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = 1;
            this.repositoryItemCheckEdit2.ValueUnchecked = 0;
            // 
            // repositoryItemTextEdit14
            // 
            this.repositoryItemTextEdit14.AutoHeight = false;
            this.repositoryItemTextEdit14.Mask.EditMask = "####.##";
            this.repositoryItemTextEdit14.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit14.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit14.Name = "repositoryItemTextEdit14";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemComboBox2.Appearance.Options.UseFont = true;
            this.repositoryItemComboBox2.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9F);
            this.repositoryItemComboBox2.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.DropDownItemHeight = 23;
            this.repositoryItemComboBox2.Items.AddRange(new object[] {
            "停用",
            "启用"});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // frmSelectFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 730);
            this.ControlBox = false;
            this.Name = "frmSelectFields";
            this.Text = "选择打印列";
            this.Load += new System.EventHandler(this.frmSelectFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueForm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        internal LookUpEdit lueForm;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.SimpleButton btnClose;
        internal DevExpress.XtraEditors.SimpleButton btnOk;
        internal DevExpress.XtraGrid.GridControl gcFields;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvFields;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit10;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit11;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit12;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit13;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit14;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
    }
}