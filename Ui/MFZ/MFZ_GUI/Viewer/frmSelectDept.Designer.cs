namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmSelectDept
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
            this.m_cboDeptName = new System.Windows.Forms.ComboBox();
            this.m_lblDeptName = new System.Windows.Forms.Label();
            this.m_cmdSubmit = new PinkieControls.ButtonXP();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.SuspendLayout();
            // 
            // m_cboDeptName
            // 
            this.m_cboDeptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboDeptName.FormattingEnabled = true;
            this.m_cboDeptName.Location = new System.Drawing.Point(74, 15);
            this.m_cboDeptName.Name = "m_cboDeptName";
            this.m_cboDeptName.Size = new System.Drawing.Size(140, 22);
            this.m_cboDeptName.TabIndex = 0;
            // 
            // m_lblDeptName
            // 
            this.m_lblDeptName.AutoSize = true;
            this.m_lblDeptName.Location = new System.Drawing.Point(7, 18);
            this.m_lblDeptName.Name = "m_lblDeptName";
            this.m_lblDeptName.Size = new System.Drawing.Size(70, 14);
            this.m_lblDeptName.TabIndex = 0;
            this.m_lblDeptName.Text = "科室名称:";
            // 
            // m_cmdSubmit
            // 
            this.m_cmdSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSubmit.DefaultScheme = true;
            this.m_cmdSubmit.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdSubmit.Hint = "";
            this.m_cmdSubmit.Location = new System.Drawing.Point(74, 45);
            this.m_cmdSubmit.Name = "m_cmdSubmit";
            this.m_cmdSubmit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSubmit.Size = new System.Drawing.Size(61, 26);
            this.m_cmdSubmit.TabIndex = 1;
            this.m_cmdSubmit.Text = "确定";
            this.m_cmdSubmit.Click += new System.EventHandler(this.m_cmdSubmit_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(145, 45);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(63, 26);
            this.m_cmdCancel.TabIndex = 2;
            this.m_cmdCancel.Text = "取消";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // frmSelectDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 78);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdSubmit);
            this.Controls.Add(this.m_cboDeptName);
            this.Controls.Add(this.m_lblDeptName);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmSelectDept";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "选择病人就诊的科室";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSelectDept_KeyDown);
            this.Load += new System.EventHandler(this.frmSelectDept_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox m_cboDeptName;
        private System.Windows.Forms.Label m_lblDeptName;
        private PinkieControls.ButtonXP m_cmdSubmit;
        private PinkieControls.ButtonXP m_cmdCancel;
    }
}