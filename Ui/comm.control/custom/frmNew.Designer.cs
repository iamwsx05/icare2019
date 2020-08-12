namespace Common.Controls
{
    partial class frmNew
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
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
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
            this.pcBackGround.Controls.Add(this.btnCancel);
            this.pcBackGround.Controls.Add(this.btnOk);
            this.pcBackGround.Controls.Add(this.gcData);
            this.pcBackGround.Size = new System.Drawing.Size(308, 422);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // gcData
            // 
            this.gcData.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcData.Location = new System.Drawing.Point(2, 2);
            this.gcData.MainView = this.gvData;
            this.gcData.Margin = new System.Windows.Forms.Padding(0);
            this.gcData.Name = "gcData";
            this.gcData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
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
            this.gcData.Size = new System.Drawing.Size(304, 378);
            this.gcData.TabIndex = 18;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvData});
            // 
            // gvData
            // 
            this.gvData.Appearance.Row.Font = new System.Drawing.Font("Arial", 10.5F);
            this.gvData.Appearance.Row.Options.UseFont = true;
            this.gvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn17,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4});
            this.gvData.GridControl = this.gcData;
            this.gvData.Name = "gvData";
            this.gvData.OptionsView.ColumnAutoWidth = false;
            this.gvData.OptionsView.ShowDetailButtons = false;
            this.gvData.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvData.OptionsView.ShowGroupPanel = false;
            this.gvData.RowHeight = 25;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "gridColumn17";
            this.gridColumn17.FieldName = "id";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn17.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "选择";
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "check";
            this.gridColumn1.Name = "gridColumn1";
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
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Arial", 10F);
            this.gridColumn3.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "编码";
            this.gridColumn3.FieldName = "code";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 60;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F);
            this.gridColumn4.AppearanceCell.ForeColor = System.Drawing.Color.Crimson;
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "名称";
            this.gridColumn4.FieldName = "name";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 160;
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
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(178, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 24);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "取消 &C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(18, 389);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(124, 24);
            this.btnOk.TabIndex = 60;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 422);
            this.MaximizeBox = false;
            this.Name = "frmNew";
            this.Text = "新建--选择项";
            this.Load += new System.EventHandler(this.frmNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
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

        internal DevExpress.XtraGrid.GridControl gcData;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
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
        internal DevExpress.XtraEditors.SimpleButton btnCancel;
        internal DevExpress.XtraEditors.SimpleButton btnOk;
    }
}