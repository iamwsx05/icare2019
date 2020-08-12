namespace Common.Controls
{
    partial class frmSignature
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSignature));
            this.gcItem = new DevExpress.XtraGrid.GridControl();
            this.gvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmpID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmpNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTechnicalLevelName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraPanelControl = new Common.Controls.xtraPanelControl();
            this.btnCancel = new Common.Controls.xtraSimpleButton();
            this.btnOk = new Common.Controls.xtraSimpleButton();
            this.txtFind = new Common.Controls.xtraTextEdit();
            this.xtraLabelControl1 = new Common.Controls.xtraLabelControl();
            this.chkAll = new Common.Controls.xtraCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraPanelControl)).BeginInit();
            this.xtraPanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.gcItem);
            this.pcBackGround.Controls.Add(this.xtraPanelControl);
            this.pcBackGround.Size = new System.Drawing.Size(341, 443);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // gcItem
            // 
            this.gcItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcItem.Location = new System.Drawing.Point(2, 30);
            this.gcItem.MainView = this.gvItem;
            this.gcItem.Name = "gcItem";
            this.gcItem.Size = new System.Drawing.Size(337, 411);
            this.gcItem.TabIndex = 4;
            this.gcItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItem});
            // 
            // gvItem
            // 
            this.gvItem.ColumnPanelRowHeight = 25;
            this.gvItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmpID,
            this.colEmpNo,
            this.colEmpName,
            this.colTechnicalLevelName,
            this.colDeptName});
            this.gvItem.GridControl = this.gcItem;
            this.gvItem.Name = "gvItem";
            this.gvItem.OptionsView.ColumnAutoWidth = false;
            this.gvItem.OptionsView.ShowDetailButtons = false;
            this.gvItem.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvItem.OptionsView.ShowGroupPanel = false;
            this.gvItem.RowHeight = 26;
            this.gvItem.DoubleClick += new System.EventHandler(this.gvItem_DoubleClick);
            // 
            // colEmpID
            // 
            this.colEmpID.Caption = "colEmpID";
            this.colEmpID.FieldName = "operCode";
            this.colEmpID.Name = "colEmpID";
            // 
            // colEmpNo
            // 
            this.colEmpNo.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colEmpNo.AppearanceCell.Options.UseFont = true;
            this.colEmpNo.AppearanceCell.Options.UseTextOptions = true;
            this.colEmpNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmpNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmpNo.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colEmpNo.AppearanceHeader.Options.UseFont = true;
            this.colEmpNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmpNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmpNo.Caption = "工号";
            this.colEmpNo.FieldName = "operCode";
            this.colEmpNo.Name = "colEmpNo";
            this.colEmpNo.OptionsColumn.AllowEdit = false;
            this.colEmpNo.OptionsColumn.AllowFocus = false;
            this.colEmpNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEmpNo.OptionsColumn.ReadOnly = true;
            this.colEmpNo.OptionsColumn.ShowInCustomizationForm = false;
            this.colEmpNo.OptionsFilter.AllowAutoFilter = false;
            this.colEmpNo.OptionsFilter.AllowFilter = false;
            this.colEmpNo.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colEmpNo.Visible = true;
            this.colEmpNo.VisibleIndex = 0;
            this.colEmpNo.Width = 61;
            // 
            // colEmpName
            // 
            this.colEmpName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colEmpName.AppearanceCell.Options.UseFont = true;
            this.colEmpName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colEmpName.AppearanceHeader.Options.UseFont = true;
            this.colEmpName.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmpName.Caption = "姓名";
            this.colEmpName.FieldName = "operName";
            this.colEmpName.Name = "colEmpName";
            this.colEmpName.OptionsColumn.AllowEdit = false;
            this.colEmpName.OptionsColumn.AllowFocus = false;
            this.colEmpName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEmpName.OptionsColumn.ReadOnly = true;
            this.colEmpName.OptionsColumn.ShowInCustomizationForm = false;
            this.colEmpName.OptionsFilter.AllowAutoFilter = false;
            this.colEmpName.OptionsFilter.AllowFilter = false;
            this.colEmpName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colEmpName.Visible = true;
            this.colEmpName.VisibleIndex = 1;
            this.colEmpName.Width = 83;
            // 
            // colTechnicalLevelName
            // 
            this.colTechnicalLevelName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colTechnicalLevelName.AppearanceCell.Options.UseFont = true;
            this.colTechnicalLevelName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colTechnicalLevelName.AppearanceHeader.Options.UseFont = true;
            this.colTechnicalLevelName.AppearanceHeader.Options.UseTextOptions = true;
            this.colTechnicalLevelName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTechnicalLevelName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTechnicalLevelName.Caption = "职称";
            this.colTechnicalLevelName.FieldName = "TechnicalLevelName";
            this.colTechnicalLevelName.Name = "colTechnicalLevelName";
            this.colTechnicalLevelName.OptionsColumn.AllowEdit = false;
            this.colTechnicalLevelName.OptionsColumn.AllowFocus = false;
            this.colTechnicalLevelName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTechnicalLevelName.OptionsColumn.ReadOnly = true;
            this.colTechnicalLevelName.OptionsColumn.ShowInCustomizationForm = false;
            this.colTechnicalLevelName.OptionsFilter.AllowAutoFilter = false;
            this.colTechnicalLevelName.OptionsFilter.AllowFilter = false;
            this.colTechnicalLevelName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colTechnicalLevelName.Visible = true;
            this.colTechnicalLevelName.VisibleIndex = 2;
            this.colTechnicalLevelName.Width = 78;
            // 
            // colDeptName
            // 
            this.colDeptName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F);
            this.colDeptName.AppearanceCell.Options.UseFont = true;
            this.colDeptName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colDeptName.AppearanceHeader.Options.UseFont = true;
            this.colDeptName.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeptName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeptName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDeptName.Caption = "科室";
            this.colDeptName.FieldName = "DeptName";
            this.colDeptName.Name = "colDeptName";
            this.colDeptName.OptionsColumn.AllowEdit = false;
            this.colDeptName.OptionsColumn.AllowFocus = false;
            this.colDeptName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDeptName.OptionsColumn.ReadOnly = true;
            this.colDeptName.OptionsColumn.ShowInCustomizationForm = false;
            this.colDeptName.OptionsFilter.AllowAutoFilter = false;
            this.colDeptName.OptionsFilter.AllowFilter = false;
            this.colDeptName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colDeptName.Visible = true;
            this.colDeptName.VisibleIndex = 3;
            this.colDeptName.Width = 78;
            // 
            // xtraPanelControl
            // 
            this.xtraPanelControl.Controls.Add(this.btnCancel);
            this.xtraPanelControl.Controls.Add(this.btnOk);
            this.xtraPanelControl.Controls.Add(this.txtFind);
            this.xtraPanelControl.Controls.Add(this.xtraLabelControl1);
            this.xtraPanelControl.Controls.Add(this.chkAll);
            this.xtraPanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraPanelControl.Location = new System.Drawing.Point(2, 2);
            this.xtraPanelControl.Name = "xtraPanelControl";
            this.xtraPanelControl.Size = new System.Drawing.Size(337, 28);
            this.xtraPanelControl.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(270, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 21);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消 &C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(204, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 21);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(107, 6);
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.Properties.Appearance.Options.UseFont = true;
            this.txtFind.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFind.Size = new System.Drawing.Size(81, 18);
            this.txtFind.TabIndex = 6;
            this.txtFind.EditValueChanged += new System.EventHandler(this.txtFind_EditValueChanged);
            // 
            // xtraLabelControl1
            // 
            this.xtraLabelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl1.Location = new System.Drawing.Point(72, 10);
            this.xtraLabelControl1.Name = "xtraLabelControl1";
            this.xtraLabelControl1.Size = new System.Drawing.Size(33, 13);
            this.xtraLabelControl1.TabIndex = 6;
            this.xtraLabelControl1.Text = "查找:";
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(8, 7);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.chkAll.Properties.Appearance.Options.UseFont = true;
            this.chkAll.Properties.Caption = "全部";
            this.chkAll.Size = new System.Drawing.Size(52, 19);
            this.chkAll.TabIndex = 6;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // frmSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 443);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSignature";
            this.ShowInTaskbar = false;
            this.Text = "签名";
            this.Load += new System.EventHandler(this.frmSignature_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSignature_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraPanelControl)).EndInit();
            this.xtraPanelControl.ResumeLayout(false);
            this.xtraPanelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gcItem;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvItem;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpID;
        internal DevExpress.XtraGrid.Columns.GridColumn colEmpNo;
        internal DevExpress.XtraGrid.Columns.GridColumn colEmpName;
        private DevExpress.XtraGrid.Columns.GridColumn colTechnicalLevelName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeptName;
        private Common.Controls.xtraPanelControl xtraPanelControl;
        private Common.Controls.xtraTextEdit txtFind;
        private Common.Controls.xtraLabelControl xtraLabelControl1;
        private Common.Controls.xtraCheckEdit chkAll;
        private Common.Controls.xtraSimpleButton btnCancel;
        private Common.Controls.xtraSimpleButton btnOk;
    }
}