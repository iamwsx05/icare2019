namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmStation
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_strSammary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_strWorkStationDesc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtWorkStationName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblRoomName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.m_lsvWorkStations = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_strSammary);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_strWorkStationDesc);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.m_txtWorkStationName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.m_lblRoomName);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 115);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // m_strSammary
            // 
            this.m_strSammary.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_strSammary.Location = new System.Drawing.Point(118, 86);
            this.m_strSammary.MaxLength = 10;
            this.m_strSammary.Name = "m_strSammary";
            this.m_strSammary.Size = new System.Drawing.Size(346, 23);
            this.m_strSammary.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 24;
            this.label3.Text = "备 注";
            // 
            // m_strWorkStationDesc
            // 
            this.m_strWorkStationDesc.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_strWorkStationDesc.Location = new System.Drawing.Point(118, 59);
            this.m_strWorkStationDesc.MaxLength = 10;
            this.m_strWorkStationDesc.Name = "m_strWorkStationDesc";
            this.m_strWorkStationDesc.Size = new System.Drawing.Size(346, 23);
            this.m_strWorkStationDesc.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 14);
            this.label10.TabIndex = 22;
            this.label10.Text = "工作站描述";
            // 
            // m_txtWorkStationName
            // 
            this.m_txtWorkStationName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtWorkStationName.Location = new System.Drawing.Point(118, 34);
            this.m_txtWorkStationName.MaxLength = 50;
            this.m_txtWorkStationName.Name = "m_txtWorkStationName";
            this.m_txtWorkStationName.Size = new System.Drawing.Size(345, 23);
            this.m_txtWorkStationName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "工作站计算机名";
            // 
            // m_lblRoomName
            // 
            this.m_lblRoomName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.m_lblRoomName.Location = new System.Drawing.Point(120, 17);
            this.m_lblRoomName.Name = "m_lblRoomName";
            this.m_lblRoomName.Size = new System.Drawing.Size(343, 14);
            this.m_lblRoomName.TabIndex = 0;
            this.m_lblRoomName.Text = "label1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "诊室名称";
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(395, 17);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(100, 38);
            this.m_cmdDelete.TabIndex = 2;
            this.m_cmdDelete.Text = "删除(F5)";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(288, 17);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(100, 38);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保存(F4)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(179, 17);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(100, 38);
            this.m_cmdNew.TabIndex = 1;
            this.m_cmdNew.Text = "新增(F3)";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // m_lsvWorkStations
            // 
            this.m_lsvWorkStations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.m_lsvWorkStations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvWorkStations.FullRowSelect = true;
            this.m_lsvWorkStations.GridLines = true;
            this.m_lsvWorkStations.Location = new System.Drawing.Point(0, 0);
            this.m_lsvWorkStations.Name = "m_lsvWorkStations";
            this.m_lsvWorkStations.Size = new System.Drawing.Size(514, 274);
            this.m_lsvWorkStations.TabIndex = 0;
            this.m_lsvWorkStations.UseCompatibleStateImageBehavior = false;
            this.m_lsvWorkStations.View = System.Windows.Forms.View.Details;
            this.m_lsvWorkStations.SelectedIndexChanged += new System.EventHandler(this.m_lsvWorkStation_Click);
            this.m_lsvWorkStations.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lsvWorkStations_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "工作站ID";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "工作站计算机名";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 237;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "工作站描述";
            this.columnHeader4.Width = 173;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmdDelete);
            this.panel1.Controls.Add(this.m_cmdSave);
            this.panel1.Controls.Add(this.m_cmdNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 389);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 82);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lsvWorkStations);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 274);
            this.panel2.TabIndex = 3;
            // 
            // frmStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 471);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作站维护";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStation_KeyDown);
            this.Load += new System.EventHandler(this.frmStation_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label m_lblRoomName;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdSave;
        private PinkieControls.ButtonXP m_cmdNew;
        private System.Windows.Forms.ListView m_lsvWorkStations;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_strWorkStationDesc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_txtWorkStationName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox m_strSammary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
    }
}