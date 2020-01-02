namespace Registration.Ui
{
    partial class frmSchedulingEditAddNo
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
            this.colStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gcDate = new DevExpress.XtraGrid.GridControl();
            this.gvDate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeekIdName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colShift = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTimeEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemTextEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit10 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit11 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit12 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnDel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnDel);
            this.pcBackGround.Controls.Add(this.btnAdd);
            this.pcBackGround.Controls.Add(this.gcDate);
            this.pcBackGround.Size = new System.Drawing.Size(789, 214);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // colStatusName
            // 
            this.colStatusName.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colStatusName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colStatusName.AppearanceCell.Options.UseBackColor = true;
            this.colStatusName.AppearanceCell.Options.UseFont = true;
            this.colStatusName.AppearanceCell.Options.UseTextOptions = true;
            this.colStatusName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colStatusName.AppearanceHeader.Options.UseFont = true;
            this.colStatusName.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusName.Caption = "出/停诊";
            this.colStatusName.ColumnEdit = this.repositoryItemComboBox4;
            this.colStatusName.FieldName = "statusName";
            this.colStatusName.Name = "colStatusName";
            this.colStatusName.OptionsColumn.AllowEdit = false;
            this.colStatusName.OptionsColumn.AllowFocus = false;
            this.colStatusName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStatusName.OptionsFilter.AllowAutoFilter = false;
            this.colStatusName.OptionsFilter.AllowFilter = false;
            this.colStatusName.Visible = true;
            this.colStatusName.VisibleIndex = 7;
            this.colStatusName.Width = 83;
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
            // gcDate
            // 
            this.gcDate.Location = new System.Drawing.Point(4, 4);
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
            this.gcDate.Size = new System.Drawing.Size(700, 205);
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
            this.gridColumn3,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn18,
            this.colWeekIdName,
            this.colShift,
            this.gridColumn12,
            this.colRegCode,
            this.gridColumn15,
            this.gridColumn16,
            this.colStatusName,
            this.gridColumn1,
            this.gridColumn2});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.colStatusName;
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
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "serNo";
            this.gridColumn3.FieldName = "serNo";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "weekId";
            this.gridColumn10.FieldName = "regWid";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "amPm";
            this.gridColumn11.FieldName = "amPm";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "regCode";
            this.gridColumn18.FieldName = "regCode";
            this.gridColumn18.Name = "gridColumn18";
            // 
            // colWeekIdName
            // 
            this.colWeekIdName.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colWeekIdName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colWeekIdName.AppearanceCell.Options.UseBackColor = true;
            this.colWeekIdName.AppearanceCell.Options.UseFont = true;
            this.colWeekIdName.AppearanceCell.Options.UseTextOptions = true;
            this.colWeekIdName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWeekIdName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWeekIdName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colWeekIdName.AppearanceHeader.Options.UseFont = true;
            this.colWeekIdName.AppearanceHeader.Options.UseTextOptions = true;
            this.colWeekIdName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWeekIdName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWeekIdName.Caption = "日期";
            this.colWeekIdName.ColumnEdit = this.repositoryItemComboBox3;
            this.colWeekIdName.FieldName = "regDate";
            this.colWeekIdName.Name = "colWeekIdName";
            this.colWeekIdName.OptionsColumn.AllowEdit = false;
            this.colWeekIdName.OptionsColumn.AllowFocus = false;
            this.colWeekIdName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colWeekIdName.OptionsFilter.AllowAutoFilter = false;
            this.colWeekIdName.OptionsFilter.AllowFilter = false;
            this.colWeekIdName.Visible = true;
            this.colWeekIdName.VisibleIndex = 0;
            this.colWeekIdName.Width = 85;
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
            // colShift
            // 
            this.colShift.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colShift.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colShift.AppearanceCell.Options.UseBackColor = true;
            this.colShift.AppearanceCell.Options.UseFont = true;
            this.colShift.AppearanceCell.Options.UseTextOptions = true;
            this.colShift.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShift.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShift.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colShift.AppearanceHeader.Options.UseFont = true;
            this.colShift.AppearanceHeader.Options.UseTextOptions = true;
            this.colShift.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShift.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShift.Caption = "班次";
            this.colShift.FieldName = "amPmName";
            this.colShift.Name = "colShift";
            this.colShift.OptionsColumn.AllowEdit = false;
            this.colShift.OptionsColumn.AllowFocus = false;
            this.colShift.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colShift.OptionsFilter.AllowAutoFilter = false;
            this.colShift.OptionsFilter.AllowFilter = false;
            this.colShift.Visible = true;
            this.colShift.VisibleIndex = 2;
            this.colShift.Width = 85;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.5F);
            this.gridColumn12.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn12.AppearanceCell.Options.UseFont = true;
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn12.Caption = "时间范围";
            this.gridColumn12.FieldName = "dateScope";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.AllowFocus = false;
            this.gridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn12.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn12.OptionsFilter.AllowFilter = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 90;
            // 
            // colRegCode
            // 
            this.colRegCode.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colRegCode.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colRegCode.AppearanceCell.Options.UseBackColor = true;
            this.colRegCode.AppearanceCell.Options.UseFont = true;
            this.colRegCode.AppearanceCell.Options.UseTextOptions = true;
            this.colRegCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRegCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRegCode.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colRegCode.AppearanceHeader.Options.UseFont = true;
            this.colRegCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colRegCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRegCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRegCode.Caption = "号别";
            this.colRegCode.FieldName = "regCodeName";
            this.colRegCode.Name = "colRegCode";
            this.colRegCode.OptionsColumn.AllowEdit = false;
            this.colRegCode.OptionsColumn.AllowFocus = false;
            this.colRegCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRegCode.OptionsFilter.AllowAutoFilter = false;
            this.colRegCode.OptionsFilter.AllowFilter = false;
            this.colRegCode.Visible = true;
            this.colRegCode.VisibleIndex = 1;
            this.colRegCode.Width = 85;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gridColumn15.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn15.Caption = "限号";
            this.gridColumn15.ColumnEdit = this.repositoryItemTextEdit7;
            this.gridColumn15.FieldName = "limitNum";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.AllowFocus = false;
            this.gridColumn15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn15.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn15.OptionsFilter.AllowFilter = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 4;
            // 
            // repositoryItemTextEdit7
            // 
            this.repositoryItemTextEdit7.Appearance.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit7.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.repositoryItemTextEdit7.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit7.Appearance.Options.UseForeColor = true;
            this.repositoryItemTextEdit7.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit7.AppearanceFocused.Font = new System.Drawing.Font("Arial", 10.5F);
            this.repositoryItemTextEdit7.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemTextEdit7.AutoHeight = false;
            this.repositoryItemTextEdit7.Mask.EditMask = "###";
            this.repositoryItemTextEdit7.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit7.Name = "repositoryItemTextEdit7";
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gridColumn16.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn16.Caption = "接诊频率";
            this.gridColumn16.ColumnEdit = this.repositoryItemTextEdit8;
            this.gridColumn16.FieldName = "freqNum";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsColumn.AllowFocus = false;
            this.gridColumn16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn16.OptionsColumn.ReadOnly = true;
            this.gridColumn16.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn16.OptionsFilter.AllowFilter = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 6;
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
            // gridColumn1
            // 
            this.gridColumn1.Caption = "regDid";
            this.gridColumn1.FieldName = "regDid";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gridColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Crimson;
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "加号";
            this.gridColumn2.ColumnEdit = this.repositoryItemTextEdit7;
            this.gridColumn2.FieldName = "addNo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
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
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(714, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "加号 &A";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Appearance.Options.UseFont = true;
            this.btnDel.Location = new System.Drawing.Point(714, 52);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(64, 25);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "取消 &C";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // frmSchedulingEditAddNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 214);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSchedulingEditAddNo";
            this.Text = "加号";
            this.Load += new System.EventHandler(this.frmSchedulingEditAddNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDel;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        internal DevExpress.XtraGrid.GridControl gcDate;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        internal DevExpress.XtraGrid.Columns.GridColumn colWeekIdName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
        internal DevExpress.XtraGrid.Columns.GridColumn colShift;
        internal DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        internal DevExpress.XtraGrid.Columns.GridColumn colRegCode;
        internal DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit8;
        internal DevExpress.XtraGrid.Columns.GridColumn colStatusName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit9;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit10;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit11;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit12;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
    }
}