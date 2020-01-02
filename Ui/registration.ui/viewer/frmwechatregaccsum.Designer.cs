namespace Registration.Ui
{
    partial class frmWeChatRegAccSum
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeChatRegAccSum));
            this.gcDate = new DevExpress.XtraGrid.GridControl();
            this.gvDate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTimeEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTextEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit10 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit11 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit12 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnClose);
            this.pcBackGround.Controls.Add(this.btnExport);
            this.pcBackGround.Controls.Add(this.gcDate);
            this.pcBackGround.Size = new System.Drawing.Size(512, 662);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // gcDate
            // 
            this.gcDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcDate.Location = new System.Drawing.Point(2, 32);
            this.gcDate.MainView = this.gvDate;
            this.gcDate.Name = "gcDate";
            this.gcDate.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTimeEdit3,
            this.repositoryItemTimeEdit4,
            this.repositoryItemTextEdit7,
            this.repositoryItemTextEdit8,
            this.repositoryItemTextEdit9,
            this.repositoryItemTextEdit10,
            this.repositoryItemTextEdit11,
            this.repositoryItemTextEdit12,
            this.repositoryItemComboBox2,
            this.repositoryItemComboBox3,
            this.repositoryItemComboBox4});
            this.gcDate.Size = new System.Drawing.Size(508, 628);
            this.gcDate.TabIndex = 6;
            this.gcDate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDate});
            // 
            // gvDate
            // 
            this.gvDate.Appearance.FocusedCell.Font = new System.Drawing.Font("宋体", 10F);
            this.gvDate.Appearance.FocusedCell.Options.UseFont = true;
            this.gvDate.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gvDate.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDate.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvDate.Appearance.Row.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gvDate.Appearance.Row.Options.UseFont = true;
            this.gvDate.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccDate,
            this.colTypeName,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn1});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "停诊";
            this.gvDate.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gvDate.GridControl = this.gcDate;
            this.gvDate.Name = "gvDate";
            this.gvDate.OptionsView.ColumnAutoWidth = false;
            this.gvDate.OptionsView.ShowDetailButtons = false;
            this.gvDate.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvDate.OptionsView.ShowGroupPanel = false;
            this.gvDate.RowHeight = 25;
            this.gvDate.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvDate_RowCellStyle);
            // 
            // colAccDate
            // 
            this.colAccDate.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colAccDate.AppearanceCell.Options.UseFont = true;
            this.colAccDate.AppearanceCell.Options.UseTextOptions = true;
            this.colAccDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAccDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAccDate.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colAccDate.AppearanceHeader.Options.UseFont = true;
            this.colAccDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAccDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAccDate.Caption = "交易日期";
            this.colAccDate.ColumnEdit = this.repositoryItemComboBox3;
            this.colAccDate.FieldName = "accDate";
            this.colAccDate.Name = "colAccDate";
            this.colAccDate.OptionsColumn.AllowEdit = false;
            this.colAccDate.OptionsColumn.AllowFocus = false;
            this.colAccDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAccDate.OptionsFilter.AllowAutoFilter = false;
            this.colAccDate.OptionsFilter.AllowFilter = false;
            this.colAccDate.Visible = true;
            this.colAccDate.VisibleIndex = 0;
            this.colAccDate.Width = 100;
            // 
            // repositoryItemComboBox3
            // 
            this.repositoryItemComboBox3.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.repositoryItemComboBox3.Appearance.Options.UseFont = true;
            this.repositoryItemComboBox3.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9F);
            this.repositoryItemComboBox3.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBox3.AutoHeight = false;
            this.repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox3.DropDownItemHeight = 22;
            this.repositoryItemComboBox3.Items.AddRange(new object[] {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期日"});
            this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
            this.repositoryItemComboBox3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colTypeName
            // 
            this.colTypeName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colTypeName.AppearanceCell.Options.UseFont = true;
            this.colTypeName.AppearanceCell.Options.UseTextOptions = true;
            this.colTypeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTypeName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colTypeName.AppearanceHeader.Options.UseFont = true;
            this.colTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTypeName.Caption = "收支方向";
            this.colTypeName.FieldName = "typeName";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.OptionsColumn.AllowEdit = false;
            this.colTypeName.OptionsColumn.AllowFocus = false;
            this.colTypeName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTypeName.OptionsFilter.AllowAutoFilter = false;
            this.colTypeName.OptionsFilter.AllowFilter = false;
            this.colTypeName.Visible = true;
            this.colTypeName.VisibleIndex = 1;
            this.colTypeName.Width = 70;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn15.Caption = "微信";
            this.gridColumn15.FieldName = "weChatMny";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.AllowFocus = false;
            this.gridColumn15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn15.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn15.OptionsFilter.AllowFilter = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            this.gridColumn15.Width = 100;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn16.Caption = "HIS";
            this.gridColumn16.FieldName = "hisMny";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsColumn.AllowFocus = false;
            this.gridColumn16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn16.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn16.OptionsFilter.AllowFilter = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 3;
            this.gridColumn16.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "差额";
            this.gridColumn1.FieldName = "diffMny";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 100;
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
            // repositoryItemTextEdit7
            // 
            this.repositoryItemTextEdit7.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit7.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit7.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit7.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit7.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit7.AutoHeight = false;
            this.repositoryItemTextEdit7.Mask.EditMask = "###";
            this.repositoryItemTextEdit7.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit7.Name = "repositoryItemTextEdit7";
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
            this.repositoryItemTextEdit8.Mask.EditMask = "###";
            this.repositoryItemTextEdit8.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
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
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemComboBox2.Appearance.Options.UseFont = true;
            this.repositoryItemComboBox2.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemComboBox2.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.DropDownItemHeight = 23;
            this.repositoryItemComboBox2.Items.AddRange(new object[] {
            "上午",
            "中午",
            "下午",
            "晚上"});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemComboBox4
            // 
            this.repositoryItemComboBox4.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.repositoryItemComboBox4.Appearance.Options.UseFont = true;
            this.repositoryItemComboBox4.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9F);
            this.repositoryItemComboBox4.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBox4.AutoHeight = false;
            this.repositoryItemComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox4.DropDownItemHeight = 22;
            this.repositoryItemComboBox4.Items.AddRange(new object[] {
            "停诊",
            "出诊"});
            this.repositoryItemComboBox4.Name = "repositoryItemComboBox4";
            this.repositoryItemComboBox4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // btnExport
            // 
            this.btnExport.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Location = new System.Drawing.Point(305, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(84, 26);
            this.btnExport.TabIndex = 135;
            this.btnExport.Text = " 导出 &E";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(405, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 26);
            this.btnClose.TabIndex = 136;
            this.btnClose.Text = "关闭 &C";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmWeChatRegAccSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 662);
            this.ControlBox = false;
            this.Name = "frmWeChatRegAccSum";
            this.Text = "对账汇总";
            this.Load += new System.EventHandler(this.frmWeChatRegAccSum_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmWeChatRegAccSum_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gcDate;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvDate;
        internal DevExpress.XtraGrid.Columns.GridColumn colAccDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
        internal DevExpress.XtraGrid.Columns.GridColumn colTypeName;
        internal DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit8;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit10;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit11;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit12;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        internal DevExpress.XtraEditors.SimpleButton btnClose;
        internal DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}