namespace Common.Controls
{
    partial class frmPathNodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPathNodes));
            this.gcItem = new DevExpress.XtraGrid.GridControl();
            this.gvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNodeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNodeDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // gcItem
            // 
            this.gcItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcItem.Location = new System.Drawing.Point(0, 0);
            this.gcItem.MainView = this.gvItem;
            this.gcItem.Name = "gcItem";
            this.gcItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2});
            this.gcItem.Size = new System.Drawing.Size(155, 303);
            this.gcItem.TabIndex = 3;
            this.gcItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItem});
            // 
            // gvItem
            // 
            this.gvItem.ColumnPanelRowHeight = 25;
            this.gvItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNodeName,
            this.colNodeDesc});
            this.gvItem.GridControl = this.gcItem;
            this.gvItem.Name = "gvItem";
            this.gvItem.OptionsView.ColumnAutoWidth = false;
            this.gvItem.OptionsView.ShowDetailButtons = false;
            this.gvItem.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvItem.OptionsView.ShowGroupPanel = false;
            this.gvItem.RowHeight = 32;
            this.gvItem.DoubleClick += new System.EventHandler(this.gvItem_DoubleClick);
            // 
            // colNodeName
            // 
            this.colNodeName.AppearanceCell.Options.UseTextOptions = true;
            this.colNodeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNodeName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNodeName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colNodeName.AppearanceHeader.Options.UseFont = true;
            this.colNodeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colNodeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNodeName.Caption = "colNodeName";
            this.colNodeName.FieldName = "NodeName";
            this.colNodeName.Name = "colNodeName";
            this.colNodeName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNodeName.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colNodeDesc
            // 
            this.colNodeDesc.AppearanceCell.Font = new System.Drawing.Font("宋体", 10F);
            this.colNodeDesc.AppearanceCell.Options.UseFont = true;
            this.colNodeDesc.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.colNodeDesc.AppearanceHeader.Options.UseFont = true;
            this.colNodeDesc.Caption = "节点描述";
            this.colNodeDesc.FieldName = "NodeDesc";
            this.colNodeDesc.Name = "colNodeDesc";
            this.colNodeDesc.OptionsColumn.AllowEdit = false;
            this.colNodeDesc.OptionsColumn.AllowFocus = false;
            this.colNodeDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNodeDesc.OptionsColumn.ShowInCustomizationForm = false;
            this.colNodeDesc.OptionsFilter.AllowAutoFilter = false;
            this.colNodeDesc.OptionsFilter.AllowFilter = false;
            this.colNodeDesc.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colNodeDesc.Visible = true;
            this.colNodeDesc.VisibleIndex = 0;
            this.colNodeDesc.Width = 159;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.repositoryItemComboBox2.Appearance.Options.UseFont = true;
            this.repositoryItemComboBox2.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9.5F);
            this.repositoryItemComboBox2.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.DropDownItemHeight = 20;
            this.repositoryItemComboBox2.Items.AddRange(new object[] {
            "疾病",
            "手术"});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // frmPathNodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 303);
            this.Controls.Add(this.gcItem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPathNodes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择节点";
            this.Load += new System.EventHandler(this.frmPathNodes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPathNodes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gcItem;
        internal DevExpress.XtraGrid.Views.Grid.GridView gvItem;
        internal DevExpress.XtraGrid.Columns.GridColumn colNodeName;
        internal DevExpress.XtraGrid.Columns.GridColumn colNodeDesc;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
    }
}