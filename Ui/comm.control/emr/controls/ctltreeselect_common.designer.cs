namespace Common.Controls.Emr
{
    partial class ctlTreeSelect_Common
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
            this.fProperties11 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties11)).BeginInit();
            this.SuspendLayout();
            // 
            // fProperties11
            // 
            this.fProperties11.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.fProperties11.Appearance.Options.UseFont = true;
            this.fProperties11.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fProperties11.Name = "fProperties11";
            this.fProperties11.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // ctlTreeSelect_Common
            // 
            this.Leave += new System.EventHandler(this.ctlTreeSelect_Common_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.fProperties11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit fProperties11;
    }
}
