namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmMedQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedQuery));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtVen = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_cmdFind = new PinkieControls.ButtonXP();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.m_cmbEsc = new PinkieControls.ButtonXP();
            this.m_txtFindPharm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_DlgResult = new com.digitalwave.controls.ControlMedicineFind();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.m_cboSelPeriodEnd = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboSelPeriodBegion = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_cboSelPeriodEnd);
            this.panel1.Controls.Add(this.m_cboSelPeriodBegion);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_txtVen);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.m_cmbEsc);
            this.panel1.Controls.Add(this.m_cmdFind);
            this.panel1.Controls.Add(this.m_txtFindPharm);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 62);
            this.panel1.TabIndex = 0;
            // 
            // m_txtVen
            // 
            this.m_txtVen.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtVen.intHeight = 120;
            this.m_txtVen.IsEnterShow = true;
            this.m_txtVen.isHide = 2;
            this.m_txtVen.isTxt = 1;
            this.m_txtVen.isUpOrDn = 0;
            this.m_txtVen.isValuse = 2;
            this.m_txtVen.Location = new System.Drawing.Point(584, 18);
            this.m_txtVen.m_IsHaveParent = false;
            this.m_txtVen.m_strParentName = "";
            this.m_txtVen.Name = "m_txtVen";
            this.m_txtVen.nextCtl = this.m_cmdFind;
            this.m_txtVen.Size = new System.Drawing.Size(179, 24);
            this.m_txtVen.TabIndex = 9;
            this.m_txtVen.txtValuse = "";
            this.m_txtVen.Visible = false;
            this.m_txtVen.VsLeftOrRight = 0;
            this.m_txtVen.Load += new System.EventHandler(this.m_txtVen_Load);
            // 
            // m_cmdFind
            // 
            this.m_cmdFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdFind.DefaultScheme = true;
            this.m_cmdFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdFind.Hint = "";
            this.m_cmdFind.Location = new System.Drawing.Point(789, 10);
            this.m_cmdFind.Name = "m_cmdFind";
            this.m_cmdFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdFind.Size = new System.Drawing.Size(80, 35);
            this.m_cmdFind.TabIndex = 13;
            this.m_cmdFind.Text = "查找(&S)";
            this.m_cmdFind.Click += new System.EventHandler(this.m_cmdFind_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(321, 32);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(67, 18);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.Text = "供应商";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(322, 11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 18);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "药品";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部",
            "入库",
            "出库"});
            this.comboBox1.Location = new System.Drawing.Point(414, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(95, 22);
            this.comboBox1.TabIndex = 15;
            // 
            // m_cmbEsc
            // 
            this.m_cmbEsc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmbEsc.DefaultScheme = true;
            this.m_cmbEsc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmbEsc.Hint = "";
            this.m_cmbEsc.Location = new System.Drawing.Point(883, 10);
            this.m_cmbEsc.Name = "m_cmbEsc";
            this.m_cmbEsc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmbEsc.Size = new System.Drawing.Size(80, 35);
            this.m_cmbEsc.TabIndex = 14;
            this.m_cmbEsc.Text = "退出(&E)";
            this.m_cmbEsc.Click += new System.EventHandler(this.m_cmbEsc_Click);
            // 
            // m_txtFindPharm
            // 
            this.m_txtFindPharm.Location = new System.Drawing.Point(584, 19);
            this.m_txtFindPharm.Name = "m_txtFindPharm";
            this.m_txtFindPharm.Size = new System.Drawing.Size(179, 23);
            this.m_txtFindPharm.TabIndex = 8;
            this.m_txtFindPharm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtFindPharm_KeyDown);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(514, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "查找药品";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.m_DlgResult.Location = new System.Drawing.Point(188, 555);
            this.m_DlgResult.Name = "m_DlgResult";
            this.m_DlgResult.Size = new System.Drawing.Size(576, 336);
            this.m_DlgResult.status = 0;
            this.m_DlgResult.strMedstorage = null;
            this.m_DlgResult.strSTORAGEID = "-1";
            this.m_DlgResult.TabIndex = 3;
            this.m_DlgResult.Visible = false;
            this.m_DlgResult.m_evtReturnVal += new com.digitalwave.controls.dlgReturnVal(this.m_DlgResult_m_evtReturnVal);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Location = new System.Drawing.Point(0, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(980, 495);
            this.panel2.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader16,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(980, 495);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "药品名称";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "药品规格";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "药品批号";
            this.columnHeader16.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单据号";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单据日期";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "单位";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "数量";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "单价";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "金额";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "零售价";
            this.columnHeader10.Width = 70;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "零售金额";
            this.columnHeader11.Width = 90;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "中标";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "供应商";
            this.columnHeader13.Width = 130;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "系统批号";
            this.columnHeader14.Width = 90;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "审核日期";
            this.columnHeader15.Width = 150;
            // 
            // m_cboSelPeriodEnd
            // 
            this.m_cboSelPeriodEnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodEnd.Location = new System.Drawing.Point(85, 32);
            this.m_cboSelPeriodEnd.Name = "m_cboSelPeriodEnd";
            this.m_cboSelPeriodEnd.Size = new System.Drawing.Size(194, 22);
            this.m_cboSelPeriodEnd.TabIndex = 67;
            // 
            // m_cboSelPeriodBegion
            // 
            this.m_cboSelPeriodBegion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodBegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodBegion.Location = new System.Drawing.Point(85, 6);
            this.m_cboSelPeriodBegion.Name = "m_cboSelPeriodBegion";
            this.m_cboSelPeriodBegion.Size = new System.Drawing.Size(194, 22);
            this.m_cboSelPeriodBegion.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 68;
            this.label3.Text = "结束财务期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 65;
            this.label5.Text = "开始财务期";
            // 
            // frmMedQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 576);
            this.Controls.Add(this.m_DlgResult);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmMedQuery";
            this.Text = "药品出入库记录查询";
            this.Load += new System.EventHandler(this.frmMedQuery_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal com.digitalwave.controls.ControlMedicineFind m_DlgResult;
        internal System.Windows.Forms.TextBox m_txtFindPharm;
        private System.Windows.Forms.Label label4;
        internal PinkieControls.ButtonXP m_cmbEsc;
        internal PinkieControls.ButtonXP m_cmdFind;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtVen;
        internal exComboBox m_cboSelPeriodEnd;
        internal exComboBox m_cboSelPeriodBegion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}