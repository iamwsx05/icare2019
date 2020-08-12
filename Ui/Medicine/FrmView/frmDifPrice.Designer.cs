namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmDifPrice : com.digitalwave.GUI_Base.frmMDI_Child_Base
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
            //this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.m_BtnSearch = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
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
            //this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1087, 659);
            this.panel2.TabIndex = 3;
            // 
            // crystalReportViewer1
            // 
            //this.crystalReportViewer1.ActiveViewIndex = -1;
            //this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crystalReportViewer1.DisplayGroupTree = false;
            //this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.crystalReportViewer1.Name = "crystalReportViewer1";
            //this.crystalReportViewer1.SelectionFormula = "";
            //this.crystalReportViewer1.ShowGroupTreeButton = false;
            //this.crystalReportViewer1.ShowRefreshButton = false;
            //this.crystalReportViewer1.ShowTextSearchButton = false;
            //this.crystalReportViewer1.ShowZoomButton = false;
            //this.crystalReportViewer1.Size = new System.Drawing.Size(1085, 657);
            //this.crystalReportViewer1.TabIndex = 0;
            //this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "西药统计",
            "中成药统计",
            "中草药统计"});
            this.comboBox1.Location = new System.Drawing.Point(100, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(140, 22);
            this.comboBox1.TabIndex = 57;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(482, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(139, 23);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(360, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "统计日期:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(678, 9);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(139, 23);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // m_BtnSearch
            // 
            this.m_BtnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnSearch.DefaultScheme = true;
            this.m_BtnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnSearch.Hint = "";
            this.m_BtnSearch.Location = new System.Drawing.Point(864, 3);
            this.m_BtnSearch.Name = "m_BtnSearch";
            this.m_BtnSearch.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnSearch.Size = new System.Drawing.Size(103, 37);
            this.m_BtnSearch.TabIndex = 56;
            this.m_BtnSearch.Text = "统计(&F)";
            this.m_BtnSearch.Click += new System.EventHandler(this.m_BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.m_BtnSearch);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 46);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(635, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "到";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDifPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 713);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmDifPrice";
            this.Text = "药品材料入库汇总单";
            this.Load += new System.EventHandler(this.frmDifPrice_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        //private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private PinkieControls.ButtonXP m_BtnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}