namespace iCare.Anaesthesia.Requisition
{
    partial class frmPopupAddOps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPopupAddOps));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.opsCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.opsName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Controls.Add(this.txtFind);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(546, 40);
            this.panelControl1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(460, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "返回 &C";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(368, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 28);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(169, 10);
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.Properties.Appearance.Options.UseFont = true;
            this.txtFind.Size = new System.Drawing.Size(191, 22);
            this.txtFind.TabIndex = 1;
            this.txtFind.EditValueChanged += new System.EventHandler(this.txtFind_EditValueChanged);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(6, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(162, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "查找手术(编码/名称/拼音码):";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 40);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(546, 648);
            this.gridControl.TabIndex = 1;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.ColumnPanelRowHeight = 25;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.opsCode,
            this.opsName});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowHeight = 23;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // opsCode
            // 
            this.opsCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.opsCode.AppearanceCell.Options.UseFont = true;
            this.opsCode.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9.5F);
            this.opsCode.AppearanceHeader.Options.UseFont = true;
            this.opsCode.Caption = "ICD码";
            this.opsCode.FieldName = "opsCode";
            this.opsCode.Name = "opsCode";
            this.opsCode.OptionsColumn.AllowEdit = false;
            this.opsCode.OptionsColumn.AllowFocus = false;
            this.opsCode.OptionsFilter.AllowAutoFilter = false;
            this.opsCode.OptionsFilter.AllowFilter = false;
            this.opsCode.Visible = true;
            this.opsCode.VisibleIndex = 0;
            this.opsCode.Width = 142;
            // 
            // opsName
            // 
            this.opsName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9.5F);
            this.opsName.AppearanceCell.Options.UseFont = true;
            this.opsName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9.5F);
            this.opsName.AppearanceHeader.Options.UseFont = true;
            this.opsName.Caption = "手术名称";
            this.opsName.FieldName = "opsName";
            this.opsName.Name = "opsName";
            this.opsName.OptionsColumn.AllowEdit = false;
            this.opsName.OptionsColumn.AllowFocus = false;
            this.opsName.OptionsFilter.AllowAutoFilter = false;
            this.opsName.OptionsFilter.AllowFilter = false;
            this.opsName.Visible = true;
            this.opsName.VisibleIndex = 1;
            this.opsName.Width = 337;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // frmPopupAddOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 688);
            this.ControlBox = false;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopupAddOps";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加手术";
            this.Load += new System.EventHandler(this.frmPopupAddOps_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.TextEdit txtFind;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn opsCode;
        private DevExpress.XtraGrid.Columns.GridColumn opsName;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}