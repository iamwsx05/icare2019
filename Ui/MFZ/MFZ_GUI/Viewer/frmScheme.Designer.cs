namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmScheme
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cboTime = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.m_dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtSchemeName = new System.Windows.Forms.TextBox();
            this.m_lbltime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cboWeekDay = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lsvScheme = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cboTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.m_dtpEndTime);
            this.groupBox1.Controls.Add(this.m_dtpStartTime);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_txtSchemeName);
            this.groupBox1.Controls.Add(this.m_lbltime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_cboWeekDay);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(827, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // m_cboTime
            // 
            this.m_cboTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboTime.FormattingEnabled = true;
            this.m_cboTime.Items.AddRange(new object[] {
            "上午",
            "下午",
            "晚上"});
            this.m_cboTime.Location = new System.Drawing.Point(85, 70);
            this.m_cboTime.Name = "m_cboTime";
            this.m_cboTime.Size = new System.Drawing.Size(143, 22);
            this.m_cboTime.TabIndex = 66;
            this.m_cboTime.SelectedIndexChanged += new System.EventHandler(this.m_cboTime_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 65;
            this.label4.Text = "排班类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 64;
            this.label2.Text = "至";
            // 
            // m_dtpEndTime
            // 
            this.m_dtpEndTime.CustomFormat = "HH:mm";
            this.m_dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpEndTime.Location = new System.Drawing.Point(452, 71);
            this.m_dtpEndTime.Name = "m_dtpEndTime";
            this.m_dtpEndTime.ShowUpDown = true;
            this.m_dtpEndTime.Size = new System.Drawing.Size(63, 23);
            this.m_dtpEndTime.TabIndex = 63;
            this.m_dtpEndTime.Value = new System.DateTime(2006, 9, 28, 15, 4, 0, 0);
            // 
            // m_dtpStartTime
            // 
            this.m_dtpStartTime.CustomFormat = "HH:mm";
            this.m_dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpStartTime.Location = new System.Drawing.Point(353, 70);
            this.m_dtpStartTime.Name = "m_dtpStartTime";
            this.m_dtpStartTime.ShowUpDown = true;
            this.m_dtpStartTime.Size = new System.Drawing.Size(63, 23);
            this.m_dtpStartTime.TabIndex = 61;
            this.m_dtpStartTime.Value = new System.DateTime(2006, 9, 28, 15, 4, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 60;
            this.label3.Text = "安排名称";
            // 
            // m_txtSchemeName
            // 
            this.m_txtSchemeName.Location = new System.Drawing.Point(85, 22);
            this.m_txtSchemeName.Name = "m_txtSchemeName";
            this.m_txtSchemeName.Size = new System.Drawing.Size(430, 23);
            this.m_txtSchemeName.TabIndex = 59;
            // 
            // m_lbltime
            // 
            this.m_lbltime.AutoSize = true;
            this.m_lbltime.Location = new System.Drawing.Point(292, 75);
            this.m_lbltime.Name = "m_lbltime";
            this.m_lbltime.Size = new System.Drawing.Size(49, 14);
            this.m_lbltime.TabIndex = 58;
            this.m_lbltime.Text = "时间段";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 57;
            this.label1.Text = "星    期";
            // 
            // m_cboWeekDay
            // 
            this.m_cboWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboWeekDay.FormattingEnabled = true;
            this.m_cboWeekDay.Items.AddRange(new object[] {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期日"});
            this.m_cboWeekDay.Location = new System.Drawing.Point(85, 45);
            this.m_cboWeekDay.Name = "m_cboWeekDay";
            this.m_cboWeekDay.Size = new System.Drawing.Size(430, 22);
            this.m_cboWeekDay.TabIndex = 55;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_lsvScheme);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 289);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // m_lsvScheme
            // 
            this.m_lsvScheme.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader4});
            this.m_lsvScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvScheme.FullRowSelect = true;
            this.m_lsvScheme.GridLines = true;
            this.m_lsvScheme.HideSelection = false;
            this.m_lsvScheme.Location = new System.Drawing.Point(3, 19);
            this.m_lsvScheme.MultiSelect = false;
            this.m_lsvScheme.Name = "m_lsvScheme";
            this.m_lsvScheme.Size = new System.Drawing.Size(821, 267);
            this.m_lsvScheme.TabIndex = 3;
            this.m_lsvScheme.UseCompatibleStateImageBehavior = false;
            this.m_lsvScheme.View = System.Windows.Forms.View.Details;
            this.m_lsvScheme.SelectedIndexChanged += new System.EventHandler(this.m_lsvScheme_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = " 安排名称";
            this.columnHeader1.Width = 133;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "星   期";
            this.columnHeader3.Width = 110;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "起始时间";
            this.columnHeader2.Width = 139;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "终止时间";
            this.columnHeader4.Width = 160;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmdDelete);
            this.panel1.Controls.Add(this.m_cmdSave);
            this.panel1.Controls.Add(this.m_cmdNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 387);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 102);
            this.panel1.TabIndex = 2;
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(688, 26);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(117, 44);
            this.m_cmdDelete.TabIndex = 2;
            this.m_cmdDelete.Text = "删除";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(565, 26);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(117, 44);
            this.m_cmdSave.TabIndex = 2;
            this.m_cmdSave.Text = "保存";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(436, 26);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(117, 44);
            this.m_cmdNew.TabIndex = 1;
            this.m_cmdNew.Text = "新增";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // frmScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 489);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "frmScheme";
            this.Text = "门诊班次计划";
            this.Load += new System.EventHandler(this.frmScheme_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label m_lbltime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_cboWeekDay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_txtSchemeName;
        private System.Windows.Forms.ListView m_lsvScheme;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.DateTimePicker m_dtpStartTime;
        private System.Windows.Forms.Panel panel1;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdSave;
        private PinkieControls.ButtonXP m_cmdNew;
        private System.Windows.Forms.DateTimePicker m_dtpEndTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_cboTime;
        private System.Windows.Forms.Label label4;
    }
}