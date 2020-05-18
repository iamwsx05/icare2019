namespace com.digitalwave.emr.EMR_SynchronousCase
{
    partial class frmEMR_WarningDialog
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEMR_WarningDialog));
            this.m_txtWarningText = new System.Windows.Forms.TextBox();
            this.m_cmdYes = new System.Windows.Forms.Button();
            this.m_cmdNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_txtWarningText
            // 
            this.m_txtWarningText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtWarningText.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_txtWarningText.Location = new System.Drawing.Point(1, 0);
            this.m_txtWarningText.Multiline = true;
            this.m_txtWarningText.Name = "m_txtWarningText";
            this.m_txtWarningText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtWarningText.Size = new System.Drawing.Size(499, 289);
            this.m_txtWarningText.TabIndex = 30;
            // 
            // m_cmdYes
            // 
            this.m_cmdYes.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cmdYes.Location = new System.Drawing.Point(141, 296);
            this.m_cmdYes.Name = "m_cmdYes";
            this.m_cmdYes.Size = new System.Drawing.Size(80, 26);
            this.m_cmdYes.TabIndex = 20;
            this.m_cmdYes.Text = "是(&Y)";
            this.m_cmdYes.UseVisualStyleBackColor = true;
            this.m_cmdYes.Click += new System.EventHandler(this.m_cmdYes_Click);
            // 
            // m_cmdNo
            // 
            this.m_cmdNo.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cmdNo.Location = new System.Drawing.Point(262, 296);
            this.m_cmdNo.Name = "m_cmdNo";
            this.m_cmdNo.Size = new System.Drawing.Size(80, 26);
            this.m_cmdNo.TabIndex = 10;
            this.m_cmdNo.Text = "否(&N)";
            this.m_cmdNo.UseVisualStyleBackColor = true;
            this.m_cmdNo.Click += new System.EventHandler(this.m_cmdNo_Click);
            // 
            // frmEMR_WarningDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 326);
            this.Controls.Add(this.m_cmdNo);
            this.Controls.Add(this.m_cmdYes);
            this.Controls.Add(this.m_txtWarningText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEMR_WarningDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "警告";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_cmdYes;
        private System.Windows.Forms.Button m_cmdNo;
        internal System.Windows.Forms.TextBox m_txtWarningText;
    }
}