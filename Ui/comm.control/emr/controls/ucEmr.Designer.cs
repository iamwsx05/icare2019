namespace Common.Controls.Emr
{
    partial class ucEmr
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
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.SuspendLayout();
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(591, 492);
            this.xtraScrollableControl.TabIndex = 0;
            // 
            // ucEmr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl);
            this.Name = "ucEmr";
            this.Size = new System.Drawing.Size(591, 492);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
    }
}
