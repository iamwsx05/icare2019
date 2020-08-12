namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmPeriodTotailReportYear
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dw_1 = new Sybase.DataWindow.DataWindowControl();
            this.m_btnPrint = new PinkieControls.ButtonXP();
            this.m_btnQuery = new PinkieControls.ButtonXP();
            this.btnesc = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnSeleYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dw_1);
            this.panel2.Location = new System.Drawing.Point(-1, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1029, 525);
            this.panel2.TabIndex = 67;
            // 
            // dw_1
            // 
            this.dw_1.DataWindowObject = "d_medinoutrportyear";
            this.dw_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dw_1.LibraryList = ".\\\\pbreport.pbl";
            this.dw_1.Location = new System.Drawing.Point(0, 0);
            this.dw_1.Name = "dw_1";
            this.dw_1.Size = new System.Drawing.Size(1027, 523);
            this.dw_1.TabIndex = 1;
            this.dw_1.Text = "dataWindowControl1";
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnPrint.DefaultScheme = true;
            this.m_btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnPrint.Hint = "";
            this.m_btnPrint.Location = new System.Drawing.Point(721, 11);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnPrint.Size = new System.Drawing.Size(93, 37);
            this.m_btnPrint.TabIndex = 67;
            this.m_btnPrint.TabStop = false;
            this.m_btnPrint.Text = "打印(&P)";
            this.m_btnPrint.Click += new System.EventHandler(this.m_btnPrint_Click);
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnQuery.DefaultScheme = true;
            this.m_btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnQuery.Hint = "";
            this.m_btnQuery.Location = new System.Drawing.Point(596, 11);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnQuery.Size = new System.Drawing.Size(93, 37);
            this.m_btnQuery.TabIndex = 66;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Text = "统计(&S)";
            this.m_btnQuery.Click += new System.EventHandler(this.m_btnQuery_Click);
            // 
            // btnesc
            // 
            this.btnesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnesc.DefaultScheme = true;
            this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnesc.Hint = "";
            this.btnesc.Location = new System.Drawing.Point(846, 11);
            this.btnesc.Name = "btnesc";
            this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnesc.Size = new System.Drawing.Size(93, 37);
            this.btnesc.TabIndex = 65;
            this.btnesc.TabStop = false;
            this.btnesc.Text = "退出(&E)";
            this.btnesc.Click += new System.EventHandler(this.btnesc_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_btnSeleYear);
            this.panel1.Controls.Add(this.m_btnPrint);
            this.panel1.Controls.Add(this.m_btnQuery);
            this.panel1.Controls.Add(this.btnesc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 56);
            this.panel1.TabIndex = 66;
            // 
            // m_btnSeleYear
            // 
            this.m_btnSeleYear.FormattingEnabled = true;
            this.m_btnSeleYear.Items.AddRange(new object[] {
            "2006",
            "2007",
            "2008",
            "2009",
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.m_btnSeleYear.Location = new System.Drawing.Point(443, 18);
            this.m_btnSeleYear.Name = "m_btnSeleYear";
            this.m_btnSeleYear.Size = new System.Drawing.Size(121, 22);
            this.m_btnSeleYear.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "统计年份：";
            // 
            // frmPeriodTotailReportYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 588);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmPeriodTotailReportYear";
            this.Text = "药品年购进统计报表";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Sybase.DataWindow.DataWindowControl dw_1;
        internal PinkieControls.ButtonXP m_btnPrint;
        internal PinkieControls.ButtonXP m_btnQuery;
        internal PinkieControls.ButtonXP btnesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox m_btnSeleYear;
        private System.Windows.Forms.Label label1;
    }
}