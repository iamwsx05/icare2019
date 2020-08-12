namespace Common.Controls
{
    partial class frmDeptSelect
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
            DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeptSelect));
            DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition styleFormatCondition4 = new DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition();
            this.colAllLeaf = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSelLeaf = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtDepart = new DevExpress.XtraEditors.TextEdit();
            this.btnToright = new DevExpress.XtraEditors.SimpleButton();
            this.btnToleft = new DevExpress.XtraEditors.SimpleButton();
            this.tlAllDept = new DevExpress.XtraTreeList.TreeList();
            this.colAllDept = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList = new System.Windows.Forms.ImageList();
            this.tlSelDept = new DevExpress.XtraTreeList.TreeList();
            this.colSelDept = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctlLabel1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlAllDept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlSelDept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // colAllLeaf
            // 
            this.colAllLeaf.Caption = "Leafflag";
            this.colAllLeaf.FieldName = "Leafflag";
            this.colAllLeaf.Name = "colAllLeaf";
            // 
            // colSelLeaf
            // 
            this.colSelLeaf.Caption = "Leafflag";
            this.colSelLeaf.FieldName = "Leafflag";
            this.colSelLeaf.Name = "colSelLeaf";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(451, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.Location = new System.Drawing.Point(380, 6);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(65, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "确定(&O)";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtDepart
            // 
            this.txtDepart.AllowDrop = true;
            this.txtDepart.Location = new System.Drawing.Point(35, 6);
            this.txtDepart.Name = "txtDepart";
            this.txtDepart.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtDepart.Properties.Appearance.Options.UseFont = true;
            this.txtDepart.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDepart.Size = new System.Drawing.Size(200, 20);
            this.txtDepart.TabIndex = 0;
            this.txtDepart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnToright
            // 
            this.btnToright.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToright.Appearance.Options.UseFont = true;
            this.btnToright.Location = new System.Drawing.Point(237, 231);
            this.btnToright.Name = "btnToright";
            this.btnToright.Size = new System.Drawing.Size(52, 23);
            this.btnToright.TabIndex = 3;
            this.btnToright.Text = "<<";
            this.btnToright.Click += new System.EventHandler(this.btnToright_Click);
            // 
            // btnToleft
            // 
            this.btnToleft.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToleft.Appearance.Options.UseFont = true;
            this.btnToleft.Location = new System.Drawing.Point(237, 179);
            this.btnToleft.Name = "btnToleft";
            this.btnToleft.Size = new System.Drawing.Size(52, 23);
            this.btnToleft.TabIndex = 1;
            this.btnToleft.Text = ">>";
            this.btnToleft.Click += new System.EventHandler(this.btnToleft_Click);
            // 
            // tlAllDept
            // 
            this.tlAllDept.Appearance.HeaderPanel.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tlAllDept.Appearance.HeaderPanel.Options.UseFont = true;
            this.tlAllDept.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.tlAllDept.Appearance.OddRow.Options.UseBackColor = true;
            this.tlAllDept.Appearance.Row.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tlAllDept.Appearance.Row.Options.UseFont = true;
            this.tlAllDept.Appearance.TreeLine.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tlAllDept.Appearance.TreeLine.Options.UseFont = true;
            this.tlAllDept.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tlAllDept.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colAllDept,
            this.colAllLeaf});
            this.tlAllDept.Cursor = System.Windows.Forms.Cursors.Arrow;
            styleFormatCondition3.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            styleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.Black;
            styleFormatCondition3.Appearance.Options.UseFont = true;
            styleFormatCondition3.Appearance.Options.UseForeColor = true;
            styleFormatCondition3.ApplyToRow = true;
            styleFormatCondition3.Column = this.colAllLeaf;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition3.Value1 = "1";
            this.tlAllDept.FormatConditions.AddRange(new DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition[] {
            styleFormatCondition3});
            this.tlAllDept.KeyFieldName = "Deptid";
            this.tlAllDept.Location = new System.Drawing.Point(4, 32);
            this.tlAllDept.Name = "tlAllDept";
            this.tlAllDept.OptionsBehavior.Editable = false;
            this.tlAllDept.OptionsBehavior.EnterMovesNextColumn = true;
            this.tlAllDept.OptionsMenu.EnableColumnMenu = false;
            this.tlAllDept.OptionsMenu.EnableFooterMenu = false;
            this.tlAllDept.OptionsPrint.AutoWidth = false;
            this.tlAllDept.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlAllDept.OptionsSelection.MultiSelect = true;
            this.tlAllDept.OptionsView.ShowHorzLines = false;
            this.tlAllDept.OptionsView.ShowIndicator = false;
            this.tlAllDept.OptionsView.ShowVertLines = false;
            this.tlAllDept.ParentFieldName = "Parentid";
            this.tlAllDept.RowHeight = 24;
            this.tlAllDept.SelectImageList = this.imageList;
            this.tlAllDept.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.tlAllDept.Size = new System.Drawing.Size(231, 472);
            this.tlAllDept.TabIndex = 0;
            this.tlAllDept.DoubleClick += new System.EventHandler(this.tlAllDept_DoubleClick);
            // 
            // colAllDept
            // 
            this.colAllDept.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.colAllDept.AppearanceHeader.Options.UseFont = true;
            this.colAllDept.AppearanceHeader.Options.UseTextOptions = true;
            this.colAllDept.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAllDept.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAllDept.Caption = "全部科室";
            this.colAllDept.FieldName = "Deptname";
            this.colAllDept.MinWidth = 33;
            this.colAllDept.Name = "colAllDept";
            this.colAllDept.OptionsColumn.AllowSort = false;
            this.colAllDept.Visible = true;
            this.colAllDept.VisibleIndex = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "itemH.png");
            this.imageList.Images.SetKeyName(1, "小确定.png");
            this.imageList.Images.SetKeyName(2, "agt_action_success.png");
            this.imageList.Images.SetKeyName(3, "HomeHS.png");
            // 
            // tlSelDept
            // 
            this.tlSelDept.Appearance.HeaderPanel.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tlSelDept.Appearance.HeaderPanel.Options.UseFont = true;
            this.tlSelDept.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.tlSelDept.Appearance.OddRow.Options.UseBackColor = true;
            this.tlSelDept.Appearance.Row.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tlSelDept.Appearance.Row.Options.UseFont = true;
            this.tlSelDept.Appearance.TreeLine.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tlSelDept.Appearance.TreeLine.Options.UseFont = true;
            this.tlSelDept.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tlSelDept.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colSelDept,
            this.colSelLeaf});
            this.tlSelDept.Cursor = System.Windows.Forms.Cursors.Arrow;
            styleFormatCondition4.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            styleFormatCondition4.Appearance.ForeColor = System.Drawing.Color.Black;
            styleFormatCondition4.Appearance.Options.UseFont = true;
            styleFormatCondition4.Appearance.Options.UseForeColor = true;
            styleFormatCondition4.ApplyToRow = true;
            styleFormatCondition4.Column = this.colSelLeaf;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition4.Value1 = "1";
            this.tlSelDept.FormatConditions.AddRange(new DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition[] {
            styleFormatCondition4});
            this.tlSelDept.KeyFieldName = "Deptid";
            this.tlSelDept.Location = new System.Drawing.Point(292, 32);
            this.tlSelDept.Name = "tlSelDept";
            this.tlSelDept.OptionsBehavior.Editable = false;
            this.tlSelDept.OptionsBehavior.EnterMovesNextColumn = true;
            this.tlSelDept.OptionsMenu.EnableColumnMenu = false;
            this.tlSelDept.OptionsMenu.EnableFooterMenu = false;
            this.tlSelDept.OptionsPrint.AutoWidth = false;
            this.tlSelDept.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlSelDept.OptionsSelection.MultiSelect = true;
            this.tlSelDept.OptionsView.ShowHorzLines = false;
            this.tlSelDept.OptionsView.ShowIndicator = false;
            this.tlSelDept.OptionsView.ShowVertLines = false;
            this.tlSelDept.ParentFieldName = "Parentid";
            this.tlSelDept.RowHeight = 24;
            this.tlSelDept.SelectImageList = this.imageList;
            this.tlSelDept.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.tlSelDept.Size = new System.Drawing.Size(231, 472);
            this.tlSelDept.TabIndex = 2;
            this.tlSelDept.DoubleClick += new System.EventHandler(this.tlSelDept_DoubleClick);
            // 
            // colSelDept
            // 
            this.colSelDept.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.colSelDept.AppearanceHeader.Options.UseFont = true;
            this.colSelDept.AppearanceHeader.Options.UseTextOptions = true;
            this.colSelDept.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSelDept.Caption = "已选科室";
            this.colSelDept.FieldName = "Deptname";
            this.colSelDept.MinWidth = 33;
            this.colSelDept.Name = "colSelDept";
            this.colSelDept.OptionsColumn.AllowSort = false;
            this.colSelDept.Visible = true;
            this.colSelDept.VisibleIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.pictureBox1);
            this.panelControl2.Controls.Add(this.txtDepart);
            this.panelControl2.Controls.Add(this.ctlLabel1);
            this.panelControl2.Controls.Add(this.btnToright);
            this.panelControl2.Controls.Add(this.btnSubmit);
            this.panelControl2.Controls.Add(this.btnToleft);
            this.panelControl2.Controls.Add(this.tlAllDept);
            this.panelControl2.Controls.Add(this.tlSelDept);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(523, 505);
            this.panelControl2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 138;
            this.pictureBox1.TabStop = false;
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ctlLabel1.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlLabel1.Location = new System.Drawing.Point(9, 7);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(0, 14);
            this.ctlLabel1.TabIndex = 137;
            // 
            // frmDeptSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 505);
            this.Controls.Add(this.panelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDeptSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择专科";
            this.Load += new System.EventHandler(this.frmExcludeEdit_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDeptSelect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtDepart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlAllDept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlSelDept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.TextEdit txtDepart;
        private DevExpress.XtraTreeList.TreeList tlAllDept;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAllDept;
        private DevExpress.XtraTreeList.TreeList tlSelDept;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSelDept;
        private DevExpress.XtraEditors.SimpleButton btnToright;
        private DevExpress.XtraEditors.SimpleButton btnToleft;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAllLeaf;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSelLeaf;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl ctlLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}