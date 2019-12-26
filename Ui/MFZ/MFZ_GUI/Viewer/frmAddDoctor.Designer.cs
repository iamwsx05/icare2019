namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmAddDoctor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cboDoctorList = new System.Windows.Forms.ComboBox();
            this.m_txtDeptName = new com.digitalwave.Utility.ctlDeptTextBox();
            this.m_cmdSubmit = new PinkieControls.ButtonXP();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cboDoctorExpert = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtDeptDesc = new System.Windows.Forms.TextBox();
            this.m_chkVisible = new System.Windows.Forms.CheckBox();
            this.m_chkReverse = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属科室";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "医生姓名";
            // 
            // m_cboDoctorList
            // 
            this.m_cboDoctorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboDoctorList.FormattingEnabled = true;
            this.m_cboDoctorList.Location = new System.Drawing.Point(89, 16);
            this.m_cboDoctorList.Name = "m_cboDoctorList";
            this.m_cboDoctorList.Size = new System.Drawing.Size(170, 22);
            this.m_cboDoctorList.TabIndex = 1;
            this.m_cboDoctorList.SelectedIndexChanged += new System.EventHandler(this.m_cboDoctorList_SelectedIndexChanged);
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
            this.m_txtDeptName.Location = new System.Drawing.Point(89, 42);
            this.m_txtDeptName.m_StrDeptID = null;
            this.m_txtDeptName.m_StrDeptName = null;
            this.m_txtDeptName.MaxLength = 50;
            this.m_txtDeptName.Name = "m_txtDeptName";
            this.m_txtDeptName.SetDepartment = com.digitalwave.Utility.ctlDeptTextBox.eDeptArea.All;
            this.m_txtDeptName.Size = new System.Drawing.Size(170, 23);
            this.m_txtDeptName.TabIndex = 0;
            this.m_txtDeptName.TextChanged += new System.EventHandler(this.m_txtDeptName_TextChanged);
            // 
            // m_cmdSubmit
            // 
            this.m_cmdSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSubmit.DefaultScheme = true;
            this.m_cmdSubmit.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdSubmit.Hint = "";
            this.m_cmdSubmit.Location = new System.Drawing.Point(107, 149);
            this.m_cmdSubmit.Name = "m_cmdSubmit";
            this.m_cmdSubmit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSubmit.Size = new System.Drawing.Size(72, 26);
            this.m_cmdSubmit.TabIndex = 2;
            this.m_cmdSubmit.Text = "确 定";
            this.m_cmdSubmit.Click += new System.EventHandler(this.m_cmdSubmit_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(186, 149);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(72, 26);
            this.m_cmdCancel.TabIndex = 3;
            this.m_cmdCancel.Text = "取 消";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "医生职称";
            // 
            // m_cboDoctorExpert
            // 
            this.m_cboDoctorExpert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboDoctorExpert.FormattingEnabled = true;
            this.m_cboDoctorExpert.Items.AddRange(new object[] {
            "普 通",
            "专 家"});
            this.m_cboDoctorExpert.Location = new System.Drawing.Point(89, 67);
            this.m_cboDoctorExpert.Name = "m_cboDoctorExpert";
            this.m_cboDoctorExpert.Size = new System.Drawing.Size(83, 22);
            this.m_cboDoctorExpert.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "专    科";
            // 
            // m_txtDeptDesc
            // 
            this.m_txtDeptDesc.Location = new System.Drawing.Point(89, 92);
            this.m_txtDeptDesc.Name = "m_txtDeptDesc";
            this.m_txtDeptDesc.Size = new System.Drawing.Size(170, 23);
            this.m_txtDeptDesc.TabIndex = 7;
            // 
            // m_chkVisible
            // 
            this.m_chkVisible.AutoSize = true;
            this.m_chkVisible.Checked = true;
            this.m_chkVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkVisible.Location = new System.Drawing.Point(177, 70);
            this.m_chkVisible.Name = "m_chkVisible";
            this.m_chkVisible.Size = new System.Drawing.Size(82, 18);
            this.m_chkVisible.TabIndex = 8;
            this.m_chkVisible.Text = "显示职称";
            this.m_chkVisible.UseVisualStyleBackColor = true;
            // 
            // m_chkReverse
            // 
            this.m_chkReverse.AutoSize = true;
            this.m_chkReverse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkReverse.Location = new System.Drawing.Point(20, 120);
            this.m_chkReverse.Name = "m_chkReverse";
            this.m_chkReverse.Size = new System.Drawing.Size(82, 18);
            this.m_chkReverse.TabIndex = 10;
            this.m_chkReverse.Text = "项目反转";
            this.m_chkReverse.UseVisualStyleBackColor = true;
            // 
            // frmAddDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 179);
            this.Controls.Add(this.m_chkReverse);
            this.Controls.Add(this.m_chkVisible);
            this.Controls.Add(this.m_txtDeptDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_cboDoctorExpert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdSubmit);
            this.Controls.Add(this.m_txtDeptName);
            this.Controls.Add(this.m_cboDoctorList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmAddDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "添加医生";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAddDoctor_KeyDown);
            this.Load += new System.EventHandler(this.frmAddDoctor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_cboDoctorList;
        private com.digitalwave.Utility.ctlDeptTextBox m_txtDeptName;
        private PinkieControls.ButtonXP m_cmdSubmit;
        private PinkieControls.ButtonXP m_cmdCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_cboDoctorExpert;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_txtDeptDesc;
        private System.Windows.Forms.CheckBox m_chkVisible;
        private System.Windows.Forms.CheckBox m_chkReverse;
    }
}