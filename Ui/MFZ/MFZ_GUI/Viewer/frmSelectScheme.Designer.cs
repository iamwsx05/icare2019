namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmSelectScheme
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
            this.components = new System.ComponentModel.Container();
            this.m_lblScheme = new System.Windows.Forms.Label();
            this.ctlCboScheme = new com.digitalwave.iCare.gui.MFZ.Controls.ctlSchemeCombox(this.components);
            this.m_cmdImport = new PinkieControls.ButtonXP();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.SuspendLayout();
            // 
            // m_lblScheme
            // 
            this.m_lblScheme.AutoSize = true;
            this.m_lblScheme.Location = new System.Drawing.Point(12, 25);
            this.m_lblScheme.Name = "m_lblScheme";
            this.m_lblScheme.Size = new System.Drawing.Size(63, 14);
            this.m_lblScheme.TabIndex = 0;
            this.m_lblScheme.Text = "班次名称";
            // 
            // ctlCboScheme
            // 
            this.ctlCboScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlCboScheme.FormattingEnabled = true;
            this.ctlCboScheme.Location = new System.Drawing.Point(84, 21);
            this.ctlCboScheme.Name = "ctlCboScheme";
            this.ctlCboScheme.SchemeID = -2147483648;
            this.ctlCboScheme.Size = new System.Drawing.Size(155, 22);
            this.ctlCboScheme.TabIndex = 13;
            // 
            // m_cmdImport
            // 
            this.m_cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdImport.DefaultScheme = true;
            this.m_cmdImport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdImport.Hint = "";
            this.m_cmdImport.Location = new System.Drawing.Point(33, 70);
            this.m_cmdImport.Name = "m_cmdImport";
            this.m_cmdImport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdImport.Size = new System.Drawing.Size(72, 26);
            this.m_cmdImport.TabIndex = 55;
            this.m_cmdImport.Text = "导  入";
            this.m_cmdImport.Click += new System.EventHandler(this.m_cmdImport_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(145, 70);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(72, 26);
            this.m_cmdCancel.TabIndex = 56;
            this.m_cmdCancel.Text = "取  消";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // frmSelectScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 117);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdImport);
            this.Controls.Add(this.ctlCboScheme);
            this.Controls.Add(this.m_lblScheme);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "frmSelectScheme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "导入安排";
            this.Load += new System.EventHandler(this.frmSelectScheme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblScheme;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlSchemeCombox ctlCboScheme;
        private PinkieControls.ButtonXP m_cmdImport;
        private PinkieControls.ButtonXP m_cmdCancel;
    }
}