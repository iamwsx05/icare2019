namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmMedicineStandardYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedicineStandardYear));
            this.m_cklbYear = new System.Windows.Forms.CheckedListBox();
            this.m_btnSet = new PinkieControls.ButtonXP();
            this.m_btnClose = new PinkieControls.ButtonXP();
            this.SuspendLayout();
            // 
            // m_cklbYear
            // 
            this.m_cklbYear.CheckOnClick = true;
            this.m_cklbYear.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cklbYear.FormattingEnabled = true;
            this.m_cklbYear.Items.AddRange(new object[] {
            "2000",
            "2001",
            "2002",
            "2003",
            "2004",
            "2005",
            "2006",
            "2007",
            "2008"});
            this.m_cklbYear.Location = new System.Drawing.Point(1, 0);
            this.m_cklbYear.Name = "m_cklbYear";
            this.m_cklbYear.Size = new System.Drawing.Size(161, 148);
            this.m_cklbYear.TabIndex = 331;
            // 
            // m_btnSet
            // 
            this.m_btnSet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_btnSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnSet.DefaultScheme = true;
            this.m_btnSet.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnSet.Hint = "";
            this.m_btnSet.Location = new System.Drawing.Point(12, 155);
            this.m_btnSet.Name = "m_btnSet";
            this.m_btnSet.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnSet.Size = new System.Drawing.Size(66, 32);
            this.m_btnSet.TabIndex = 332;
            this.m_btnSet.Text = "确定(&O)";
            this.m_btnSet.Click += new System.EventHandler(this.m_btnSet_Click);
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnClose.DefaultScheme = true;
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Hint = "";
            this.m_btnClose.Location = new System.Drawing.Point(84, 155);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnClose.Size = new System.Drawing.Size(66, 32);
            this.m_btnClose.TabIndex = 332;
            this.m_btnClose.Text = "关闭(&X)";
            this.m_btnClose.Click += new System.EventHandler(this.m_btnClose_Click);
            // 
            // frmMedicineStandardYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 196);
            this.Controls.Add(this.m_btnClose);
            this.Controls.Add(this.m_btnSet);
            this.Controls.Add(this.m_cklbYear);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMedicineStandardYear";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中标年份";
            this.Load += new System.EventHandler(this.frmMedicineStandardYear_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.CheckedListBox m_cklbYear;
        private PinkieControls.ButtonXP m_btnSet;
        private PinkieControls.ButtonXP m_btnClose;
    }
}