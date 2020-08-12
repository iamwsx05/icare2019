namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmFinancial
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.m_cboAddCheckDetail = new PinkieControls.ButtonXP();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.m_cobFinancial = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctlprintShow1 = new com.digitalwave.controls.Control.ctlprintShow();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.m_txtFindPharm = new System.Windows.Forms.TextBox();
            this.m_DlgResult = new com.digitalwave.controls.ControlMedicineFind();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_txtFindPharm);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonXP1);
            this.panel1.Controls.Add(this.m_cboAddCheckDetail);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.m_cobFinancial);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 52);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 19);
            this.label4.TabIndex = 50;
            this.label4.Text = "药品名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(935, 9);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(80, 32);
            this.buttonXP1.TabIndex = 48;
            this.buttonXP1.Text = "退出(&E)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_cboAddCheckDetail
            // 
            this.m_cboAddCheckDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cboAddCheckDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cboAddCheckDetail.DefaultScheme = true;
            this.m_cboAddCheckDetail.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cboAddCheckDetail.Hint = "";
            this.m_cboAddCheckDetail.Location = new System.Drawing.Point(839, 9);
            this.m_cboAddCheckDetail.Name = "m_cboAddCheckDetail";
            this.m_cboAddCheckDetail.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cboAddCheckDetail.Size = new System.Drawing.Size(80, 32);
            this.m_cboAddCheckDetail.TabIndex = 47;
            this.m_cboAddCheckDetail.Text = "生成(&G)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(690, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 46;
            this.label1.Text = "到";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(710, 13);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(118, 23);
            this.dateTimePicker2.TabIndex = 45;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(565, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(119, 23);
            this.dateTimePicker1.TabIndex = 44;
            // 
            // m_cobFinancial
            // 
            this.m_cobFinancial.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cobFinancial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cobFinancial.Location = new System.Drawing.Point(289, 13);
            this.m_cobFinancial.Name = "m_cobFinancial";
            this.m_cobFinancial.Size = new System.Drawing.Size(179, 22);
            this.m_cobFinancial.TabIndex = 43;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(478, 15);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 18);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "时间范围：";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(213, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 18);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "财务期：";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ctlprintShow1);
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 462);
            this.panel2.TabIndex = 1;
            // 
            // ctlprintShow1
            // 
            this.ctlprintShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlprintShow1.Location = new System.Drawing.Point(0, 0);
            this.ctlprintShow1.Name = "ctlprintShow1";
            this.ctlprintShow1.Size = new System.Drawing.Size(1026, 460);
            this.ctlprintShow1.TabIndex = 0;
            this.ctlprintShow1.Zoom = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // m_txtFindPharm
            // 
            this.m_txtFindPharm.Location = new System.Drawing.Point(71, 13);
            this.m_txtFindPharm.Name = "m_txtFindPharm";
            this.m_txtFindPharm.Size = new System.Drawing.Size(136, 23);
            this.m_txtFindPharm.TabIndex = 51;
            this.m_txtFindPharm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtFindPharm_KeyDown);
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
            this.m_DlgResult.Location = new System.Drawing.Point(226, 512);
            this.m_DlgResult.Name = "m_DlgResult";
            this.m_DlgResult.Size = new System.Drawing.Size(576, 336);
            this.m_DlgResult.status = 0;
            this.m_DlgResult.strMedstorage = null;
            this.m_DlgResult.strSTORAGEID = "-1";
            this.m_DlgResult.TabIndex = 4;
            this.m_DlgResult.Visible = false;
            this.m_DlgResult.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.m_DlgResult_m_evtReturnVal);
            // 
            // frmFinancial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 520);
            this.Controls.Add(this.m_DlgResult);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmFinancial";
            this.Text = "药品进销进报表";
            this.Load += new System.EventHandler(this.frmFinancial_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        internal exComboBox m_cobFinancial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal PinkieControls.ButtonXP buttonXP1;
        internal PinkieControls.ButtonXP m_cboAddCheckDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private com.digitalwave.controls.Control.ctlprintShow ctlprintShow1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        internal System.Windows.Forms.TextBox m_txtFindPharm;
        internal com.digitalwave.controls.ControlMedicineFind m_DlgResult;
    }
}