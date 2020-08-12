namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmMedDeIOCCReport
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
            this.btnesc = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dw_1 = new Sybase.DataWindow.DataWindowControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnPrint = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtFindPharm = new System.Windows.Forms.TextBox();
            this.m_cboSelPeriodEnd = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboSelPeriodBegion = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_btnQuery = new PinkieControls.ButtonXP();
            this.m_DlgResult = new com.digitalwave.controls.ControlMedicineFind();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnesc
            // 
            this.btnesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnesc.DefaultScheme = true;
            this.btnesc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnesc.Hint = "";
            this.btnesc.Location = new System.Drawing.Point(952, 10);
            this.btnesc.Name = "btnesc";
            this.btnesc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnesc.Size = new System.Drawing.Size(59, 37);
            this.btnesc.TabIndex = 65;
            this.btnesc.TabStop = false;
            this.btnesc.Text = "退出(&E)";
            this.btnesc.Click += new System.EventHandler(this.btnesc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 64;
            this.label2.Text = "结束财务期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始财务期";
            // 
            // dw_1
            // 
            this.dw_1.DataWindowObject = "t_medioccreport";
            this.dw_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dw_1.LibraryList = ".\\\\pbreport.pbl";
            this.dw_1.Location = new System.Drawing.Point(0, 0);
            this.dw_1.Name = "dw_1";
            this.dw_1.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dw_1.Size = new System.Drawing.Size(1026, 675);
            this.dw_1.TabIndex = 1;
            this.dw_1.Text = "dataWindowControl1";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dw_1);
            this.panel2.Location = new System.Drawing.Point(0, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 677);
            this.panel2.TabIndex = 67;
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnPrint.DefaultScheme = true;
            this.m_btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnPrint.Hint = "";
            this.m_btnPrint.Location = new System.Drawing.Point(888, 10);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnPrint.Size = new System.Drawing.Size(59, 37);
            this.m_btnPrint.TabIndex = 67;
            this.m_btnPrint.TabStop = false;
            this.m_btnPrint.Text = "打印(&P)";
            this.m_btnPrint.Click += new System.EventHandler(this.m_btnPrint_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.m_txtFindPharm);
            this.panel1.Controls.Add(this.m_cboSelPeriodEnd);
            this.panel1.Controls.Add(this.m_cboSelPeriodBegion);
            this.panel1.Controls.Add(this.m_btnPrint);
            this.panel1.Controls.Add(this.m_btnQuery);
            this.panel1.Controls.Add(this.btnesc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 70);
            this.panel1.TabIndex = 66;
            // 
            // m_txtFindPharm
            // 
            this.m_txtFindPharm.Location = new System.Drawing.Point(673, 6);
            this.m_txtFindPharm.Name = "m_txtFindPharm";
            this.m_txtFindPharm.Size = new System.Drawing.Size(126, 23);
            this.m_txtFindPharm.TabIndex = 68;
            this.m_txtFindPharm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtFindPharm_KeyDown);
            // 
            // m_cboSelPeriodEnd
            // 
            this.m_cboSelPeriodEnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodEnd.Location = new System.Drawing.Point(362, 21);
            this.m_cboSelPeriodEnd.Name = "m_cboSelPeriodEnd";
            this.m_cboSelPeriodEnd.Size = new System.Drawing.Size(200, 22);
            this.m_cboSelPeriodEnd.TabIndex = 63;
            // 
            // m_cboSelPeriodBegion
            // 
            this.m_cboSelPeriodBegion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodBegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodBegion.Location = new System.Drawing.Point(79, 21);
            this.m_cboSelPeriodBegion.Name = "m_cboSelPeriodBegion";
            this.m_cboSelPeriodBegion.Size = new System.Drawing.Size(194, 22);
            this.m_cboSelPeriodBegion.TabIndex = 62;
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnQuery.DefaultScheme = true;
            this.m_btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnQuery.Hint = "";
            this.m_btnQuery.Location = new System.Drawing.Point(823, 10);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnQuery.Size = new System.Drawing.Size(59, 37);
            this.m_btnQuery.TabIndex = 66;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Text = "统计(&S)";
            this.m_btnQuery.Click += new System.EventHandler(this.m_btnQuery_Click);
            // 
            // m_DlgResult
            // 
            this.m_DlgResult.blIsMedStorage = true;
            this.m_DlgResult.blISOutStorage = false;
            this.m_DlgResult.blRepertory = false;
            this.m_DlgResult.FindMedmode = 0;
            this.m_DlgResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_DlgResult.intIsReData = 0;
            this.m_DlgResult.isApplMebMod = null;
            this.m_DlgResult.isApplModel = false;
            this.m_DlgResult.isShowFindType = true;
            this.m_DlgResult.IsShowZero = false;
            this.m_DlgResult.Location = new System.Drawing.Point(-576, 53);
            this.m_DlgResult.Name = "m_DlgResult";
            this.m_DlgResult.Size = new System.Drawing.Size(576, 336);
            this.m_DlgResult.status = 0;
            this.m_DlgResult.strMedstorage = null;
            this.m_DlgResult.strSTORAGEID = "-1";
            this.m_DlgResult.TabIndex = 69;
            this.m_DlgResult.Visible = false;
            this.m_DlgResult.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.m_DlgResult_m_evtReturnVal);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(586, 11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 18);
            this.radioButton1.TabIndex = 70;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "药品名称";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(586, 35);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 18);
            this.radioButton2.TabIndex = 71;
            this.radioButton2.Text = "药品类型";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "西药",
            "中草药",
            "中成药"});
            this.comboBox1.Location = new System.Drawing.Point(673, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 22);
            this.comboBox1.TabIndex = 72;
            // 
            // frmMedDeIOCCReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 753);
            this.Controls.Add(this.m_DlgResult);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmMedDeIOCCReport";
            this.Text = "进销存明细报表";
            this.Load += new System.EventHandler(this.frmMedDeIOCCReport_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal PinkieControls.ButtonXP btnesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Sybase.DataWindow.DataWindowControl dw_1;
        private System.Windows.Forms.Panel panel2;
        internal PinkieControls.ButtonXP m_btnPrint;
        private System.Windows.Forms.Panel panel1;
        internal PinkieControls.ButtonXP m_btnQuery;
        internal exComboBox m_cboSelPeriodEnd;
        internal exComboBox m_cboSelPeriodBegion;
        internal System.Windows.Forms.TextBox m_txtFindPharm;
        internal com.digitalwave.controls.ControlMedicineFind m_DlgResult;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}