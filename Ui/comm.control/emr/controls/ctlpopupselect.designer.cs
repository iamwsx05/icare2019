namespace Common.Controls.Emr
{
    partial class ctlPopupSelect
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.innerEditor = new DevExpress.XtraEditors.PopupContainerEdit();
            this.innerContainer = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridControlPop = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewPop = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.innerEditor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.innerContainer)).BeginInit();
            this.innerContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPop)).BeginInit();
            this.SuspendLayout();
            // 
            // innerEditor
            // 
            this.innerEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerEditor.EditValue = "";
            this.innerEditor.Location = new System.Drawing.Point(0, 0);
            this.innerEditor.Name = "innerEditor";
            this.innerEditor.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.innerEditor.Properties.Appearance.Options.UseFont = true;
            this.innerEditor.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.innerEditor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.innerEditor.Properties.PopupControl = this.innerContainer;
            this.innerEditor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.innerEditor.Size = new System.Drawing.Size(140, 21);
            this.innerEditor.TabIndex = 3;
            // 
            // innerContainer
            // 
            this.innerContainer.Controls.Add(this.gridControlPop);
            this.innerContainer.Location = new System.Drawing.Point(200, 3);
            this.innerContainer.Name = "innerContainer";
            this.innerContainer.Size = new System.Drawing.Size(400, 250);
            this.innerContainer.TabIndex = 4;
            // 
            // gridControlPop
            // 
            this.gridControlPop.DataSource = this.bindingSource1;
            this.gridControlPop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPop.EmbeddedNavigator.Name = "";
            this.gridControlPop.Location = new System.Drawing.Point(0, 0);
            this.gridControlPop.MainView = this.gridViewPop;
            this.gridControlPop.Name = "gridControlPop";
            this.gridControlPop.Size = new System.Drawing.Size(400, 250);
            this.gridControlPop.TabIndex = 2;
            this.gridControlPop.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPop});
            // 
            // gridViewPop
            // 
            this.gridViewPop.GridControl = this.gridControlPop;
            this.gridViewPop.Name = "gridViewPop";
            this.gridViewPop.OptionsView.ShowGroupPanel = false;
            this.gridViewPop.OptionsView.ShowIndicator = false;
            // 
            // ctlPopupSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.innerContainer);
            this.Controls.Add(this.innerEditor);
            this.Name = "ctlPopupSelect";
            this.Size = new System.Drawing.Size(140, 22);
            this.Load += new System.EventHandler(this.ctlPopupSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.innerEditor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.innerContainer)).EndInit();
            this.innerContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit innerEditor;
        private DevExpress.XtraEditors.PopupContainerControl innerContainer;
        private DevExpress.XtraGrid.GridControl gridControlPop;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPop;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}
