namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmDoctor
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
            this.m_flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_ctlDoctors = new com.digitalwave.iCare.gui.MFZ.Controls.ctlDoctorList();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_cmdDelScheme = new PinkieControls.ButtonXP();
            this.m_cmdImportScheme = new PinkieControls.ButtonXP();
            this.m_cmdAddRoom = new PinkieControls.ButtonXP();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtDeptName = new com.digitalwave.Utility.ctlDeptTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ctlCboScheme = new com.digitalwave.iCare.gui.MFZ.Controls.ctlSchemeCombox(this.components);
            this.ctlCboDiagnoseArea = new com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_flpMain
            // 
            this.m_flpMain.AutoScroll = true;
            this.m_flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_flpMain.Location = new System.Drawing.Point(0, 0);
            this.m_flpMain.Name = "m_flpMain";
            this.m_flpMain.Size = new System.Drawing.Size(795, 753);
            this.m_flpMain.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(795, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 753);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_ctlDoctors);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 606);
            this.panel2.TabIndex = 2;
            // 
            // m_ctlDoctors
            // 
            this.m_ctlDoctors.DeptID = "";
            this.m_ctlDoctors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctlDoctors.Location = new System.Drawing.Point(0, 0);
            this.m_ctlDoctors.Name = "m_ctlDoctors";
            this.m_ctlDoctors.Size = new System.Drawing.Size(233, 606);
            this.m_ctlDoctors.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_cmdDelScheme);
            this.groupBox2.Controls.Add(this.m_cmdImportScheme);
            this.groupBox2.Controls.Add(this.m_cmdAddRoom);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 40);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // m_cmdDelScheme
            // 
            this.m_cmdDelScheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelScheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelScheme.DefaultScheme = true;
            this.m_cmdDelScheme.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdDelScheme.Hint = "";
            this.m_cmdDelScheme.Location = new System.Drawing.Point(149, 11);
            this.m_cmdDelScheme.Name = "m_cmdDelScheme";
            this.m_cmdDelScheme.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelScheme.Size = new System.Drawing.Size(72, 26);
            this.m_cmdDelScheme.TabIndex = 57;
            this.m_cmdDelScheme.Text = "删除安排";
            this.m_cmdDelScheme.Click += new System.EventHandler(this.m_cmdDelScheme_Click);
            // 
            // m_cmdImportScheme
            // 
            this.m_cmdImportScheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdImportScheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdImportScheme.DefaultScheme = true;
            this.m_cmdImportScheme.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdImportScheme.Hint = "";
            this.m_cmdImportScheme.Location = new System.Drawing.Point(76, 11);
            this.m_cmdImportScheme.Name = "m_cmdImportScheme";
            this.m_cmdImportScheme.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdImportScheme.Size = new System.Drawing.Size(72, 26);
            this.m_cmdImportScheme.TabIndex = 56;
            this.m_cmdImportScheme.Text = "导入安排";
            this.m_cmdImportScheme.Click += new System.EventHandler(this.m_cmdImportScheme_Click);
            // 
            // m_cmdAddRoom
            // 
            this.m_cmdAddRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAddRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdAddRoom.DefaultScheme = true;
            this.m_cmdAddRoom.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdAddRoom.Hint = "";
            this.m_cmdAddRoom.Location = new System.Drawing.Point(3, 11);
            this.m_cmdAddRoom.Name = "m_cmdAddRoom";
            this.m_cmdAddRoom.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddRoom.Size = new System.Drawing.Size(72, 26);
            this.m_cmdAddRoom.TabIndex = 54;
            this.m_cmdAddRoom.Text = "添加诊室";
            this.m_cmdAddRoom.Click += new System.EventHandler(this.m_cmdAddRoom_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtDeptName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ctlCboScheme);
            this.groupBox1.Controls.Add(this.ctlCboDiagnoseArea);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "所属诊区科室";
            // 
            // m_txtDeptName
            // 
            //this.m_txtDeptName.EnableAutoValidation = true;
            //this.m_txtDeptName.EnableEnterKeyValidate = false;
            //this.m_txtDeptName.EnableEscapeKeyUndo = true;
            //this.m_txtDeptName.EnableLastValidValue = true;
            //this.m_txtDeptName.ErrorProvider = null;
            //this.m_txtDeptName.ErrorProviderMessage = "Invalid value";
            this.m_txtDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtDeptName.ForceFormatText = true;
            this.m_txtDeptName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtDeptName.Location = new System.Drawing.Point(76, 50);
            this.m_txtDeptName.m_StrDeptID = null;
            this.m_txtDeptName.m_StrDeptName = null;
            this.m_txtDeptName.MaxLength = 50;
            this.m_txtDeptName.Name = "m_txtDeptName";
            this.m_txtDeptName.SetDepartment = com.digitalwave.Utility.ctlDeptTextBox.eDeptArea.All;
            this.m_txtDeptName.Size = new System.Drawing.Size(151, 23);
            this.m_txtDeptName.TabIndex = 14;
            this.m_txtDeptName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDeptName_KeyDown);
            this.m_txtDeptName.TextChanged += new System.EventHandler(this.m_txtDeptName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "班次名称";
            // 
            // ctlCboScheme
            // 
            this.ctlCboScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlCboScheme.FormattingEnabled = true;
            this.ctlCboScheme.Location = new System.Drawing.Point(76, 75);
            this.ctlCboScheme.Name = "ctlCboScheme";
            this.ctlCboScheme.SchemeID = -2147483648;
            this.ctlCboScheme.Size = new System.Drawing.Size(151, 22);
            this.ctlCboScheme.TabIndex = 12;
            this.ctlCboScheme.SelectedIndexChanged += new System.EventHandler(this.ctlCboScheme_SelectedIndexChanged);
            // 
            // ctlCboDiagnoseArea
            // 
            this.ctlCboDiagnoseArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlCboDiagnoseArea.FormattingEnabled = true;
            this.ctlCboDiagnoseArea.Location = new System.Drawing.Point(76, 26);
            this.ctlCboDiagnoseArea.m_intAreaID = -2147483648;
            this.ctlCboDiagnoseArea.Name = "ctlCboDiagnoseArea";
            this.ctlCboDiagnoseArea.Size = new System.Drawing.Size(151, 22);
            this.ctlCboDiagnoseArea.TabIndex = 10;
            this.ctlCboDiagnoseArea.SelectedIndexChanged += new System.EventHandler(this.ctlCboDiagnoseArea_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "科室名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "诊区名称";
            // 
            // frmDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 753);
            this.Controls.Add(this.m_flpMain);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "frmDoctor";
            this.Text = "医生工作站安排维护";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDoctor_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel m_flpMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox ctlCboDiagnoseArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlSchemeCombox ctlCboScheme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private PinkieControls.ButtonXP m_cmdImportScheme;
        private PinkieControls.ButtonXP m_cmdAddRoom;
        private PinkieControls.ButtonXP m_cmdDelScheme;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlDoctorList m_ctlDoctors;
        private com.digitalwave.Utility.ctlDeptTextBox m_txtDeptName;
    }
}