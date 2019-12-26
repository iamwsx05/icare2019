namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmDiagnoseArea
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
            this.m_lsvDiagnoseArea = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_txtSummary = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtDiagnoseAreaName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lsvDiagnoseArea);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 320);
            this.panel2.TabIndex = 15;
            // 
            // m_lsvDiagnoseArea
            // 
            this.m_lsvDiagnoseArea.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_lsvDiagnoseArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvDiagnoseArea.FullRowSelect = true;
            this.m_lsvDiagnoseArea.GridLines = true;
            this.m_lsvDiagnoseArea.HideSelection = false;
            this.m_lsvDiagnoseArea.Location = new System.Drawing.Point(0, 0);
            this.m_lsvDiagnoseArea.MultiSelect = false;
            this.m_lsvDiagnoseArea.Name = "m_lsvDiagnoseArea";
            this.m_lsvDiagnoseArea.Size = new System.Drawing.Size(823, 320);
            this.m_lsvDiagnoseArea.TabIndex = 0;
            this.m_lsvDiagnoseArea.UseCompatibleStateImageBehavior = false;
            this.m_lsvDiagnoseArea.View = System.Windows.Forms.View.Details;
            this.m_lsvDiagnoseArea.DoubleClick += new System.EventHandler(this.m_lsvDiagnoseArea_DoubleClick);
            this.m_lsvDiagnoseArea.SelectedIndexChanged += new System.EventHandler(this.m_lsvDiagnoseArea_SelectedIndexChanged);
            this.m_lsvDiagnoseArea.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lsvDiagnoseArea_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "诊区ID";
            this.columnHeader1.Width = 88;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "诊区名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 153;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "备  注";
            this.columnHeader3.Width = 129;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_txtSummary);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.m_txtDiagnoseAreaName);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(823, 83);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // m_txtSummary
            // 
            this.m_txtSummary.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSummary.Location = new System.Drawing.Point(127, 47);
            this.m_txtSummary.MaxLength = 25;
            this.m_txtSummary.Name = "m_txtSummary";
            this.m_txtSummary.Size = new System.Drawing.Size(381, 23);
            this.m_txtSummary.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "备    注  ";
            // 
            // m_txtDiagnoseAreaName
            // 
            this.m_txtDiagnoseAreaName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtDiagnoseAreaName.Location = new System.Drawing.Point(127, 25);
            this.m_txtDiagnoseAreaName.MaxLength = 25;
            this.m_txtDiagnoseAreaName.Name = "m_txtDiagnoseAreaName";
            this.m_txtDiagnoseAreaName.Size = new System.Drawing.Size(381, 23);
            this.m_txtDiagnoseAreaName.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "诊区名称";
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(669, 26);
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
            this.m_cmdSave.Location = new System.Drawing.Point(541, 26);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(117, 44);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保存(F4)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
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
            this.panel1.TabIndex = 1;
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(413, 26);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(117, 44);
            this.m_cmdNew.TabIndex = 1;
            this.m_cmdNew.Text = "新增(F3)";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // frmDiagnoseArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 499);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmDiagnoseArea";
            this.Text = "诊区维护";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDiagnoseArea_KeyDown);
            this.Load += new System.EventHandler(this.frmDiagnoseArea_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView m_lsvDiagnoseArea;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtDiagnoseAreaName;
        private System.Windows.Forms.Label label9;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdSave;
        private System.Windows.Forms.Panel panel1;
        private PinkieControls.ButtonXP m_cmdNew;
        private System.Windows.Forms.TextBox m_txtSummary;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}