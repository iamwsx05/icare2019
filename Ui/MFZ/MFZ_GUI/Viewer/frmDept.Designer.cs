namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmDept
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
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_lsvDepts = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtDeptName = new com.digitalwave.Utility.ctlDeptTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtSammary = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_txtDeptNameShort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "部门名称";
            this.columnHeader1.Width = 129;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "备注";
            this.columnHeader4.Width = 160;
            // 
            // m_lsvDepts
            // 
            this.m_lsvDepts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4});
            this.m_lsvDepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvDepts.FullRowSelect = true;
            this.m_lsvDepts.GridLines = true;
            this.m_lsvDepts.HideSelection = false;
            this.m_lsvDepts.Location = new System.Drawing.Point(0, 0);
            this.m_lsvDepts.MultiSelect = false;
            this.m_lsvDepts.Name = "m_lsvDepts";
            this.m_lsvDepts.Size = new System.Drawing.Size(823, 283);
            this.m_lsvDepts.TabIndex = 2;
            this.m_lsvDepts.UseCompatibleStateImageBehavior = false;
            this.m_lsvDepts.View = System.Windows.Forms.View.Details;
            this.m_lsvDepts.SelectedIndexChanged += new System.EventHandler(this.m_lsvDepts_SelectedIndexChanged);
            this.m_lsvDepts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lsvDepts_ColumnClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "部门简称";
            this.columnHeader3.Width = 141;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmdDelete);
            this.panel1.Controls.Add(this.m_cmdSave);
            this.panel1.Controls.Add(this.m_cmdNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 403);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 96);
            this.panel1.TabIndex = 0;
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(684, 20);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(117, 44);
            this.m_cmdDelete.TabIndex = 2;
            this.m_cmdDelete.Text = "删除(F5)";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(561, 20);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(117, 44);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保存(F4)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(432, 20);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(117, 44);
            this.m_cmdNew.TabIndex = 1;
            this.m_cmdNew.Text = "新增(F3)";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "科室名称";
            // 
            // m_txtDeptName
            // 
            //this.m_txtDeptName.EnableAutoValidation = true;
            //this.m_txtDeptName.EnableEnterKeyValidate = true;
            //this.m_txtDeptName.EnableEscapeKeyUndo = true;
            //this.m_txtDeptName.EnableLastValidValue = true;
            //this.m_txtDeptName.ErrorProvider = null;
            //this.m_txtDeptName.ErrorProviderMessage = "Invalid value";
            this.m_txtDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtDeptName.ForceFormatText = true;
            this.m_txtDeptName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtDeptName.Location = new System.Drawing.Point(100, 29);
            this.m_txtDeptName.m_StrDeptID = null;
            this.m_txtDeptName.m_StrDeptName = null;
            this.m_txtDeptName.MaxLength = 50;
            this.m_txtDeptName.Name = "m_txtDeptName";
            this.m_txtDeptName.SetDepartment = com.digitalwave.Utility.ctlDeptTextBox.eDeptArea.All;
            this.m_txtDeptName.Size = new System.Drawing.Size(566, 23);
            this.m_txtDeptName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 24;
            this.label3.Text = "备    注";
            // 
            // m_txtSammary
            // 
            this.m_txtSammary.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtSammary.Location = new System.Drawing.Point(100, 78);
            this.m_txtSammary.MaxLength = 10;
            this.m_txtSammary.Name = "m_txtSammary";
            this.m_txtSammary.Size = new System.Drawing.Size(566, 23);
            this.m_txtSammary.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_txtDeptNameShort);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.m_txtSammary);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_txtDeptName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(823, 120);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // m_txtDeptNameShort
            // 
            this.m_txtDeptNameShort.Location = new System.Drawing.Point(100, 54);
            this.m_txtDeptNameShort.Name = "m_txtDeptNameShort";
            this.m_txtDeptNameShort.Size = new System.Drawing.Size(566, 23);
            this.m_txtDeptNameShort.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 26;
            this.label1.Text = "科室简称";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lsvDepts);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 120);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 283);
            this.panel2.TabIndex = 3;
            // 
            // frmDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 499);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmDept";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科室简称维护";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDept_KeyDown);
            this.Load += new System.EventHandler(this.frmDept_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView m_lsvDepts;
        private System.Windows.Forms.Panel panel1;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdSave;
        private PinkieControls.ButtonXP m_cmdNew;
        private System.Windows.Forms.Label label2;
        private com.digitalwave.Utility.ctlDeptTextBox m_txtDeptName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_txtSammary;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtDeptNameShort;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel2;
    }
}