namespace com.digitalwave.iCare.gui.DataExchangeSystem
{
    partial class frmDataExchangeMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataExchangeMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewUpdata = new System.Windows.Forms.DataGridView();
            this.AutoUpdataTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAutoUpdata = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtmEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmBegin = new System.Windows.Forms.DateTimePicker();
            this.rtb_showLog = new System.Windows.Forms.RichTextBox();
            this.cmsForMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiCleanLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.butUpload = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsp_showProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.sfdForSaveLog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUpdata)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.cmsForMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(631, 441);
            this.splitContainer1.SplitterDistance = 268;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewUpdata);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 268);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择需要上传的表";
            // 
            // dataGridViewUpdata
            // 
            this.dataGridViewUpdata.AllowUserToAddRows = false;
            this.dataGridViewUpdata.AllowUserToDeleteRows = false;
            this.dataGridViewUpdata.AllowUserToResizeColumns = false;
            this.dataGridViewUpdata.AllowUserToResizeRows = false;
            this.dataGridViewUpdata.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUpdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewUpdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUpdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AutoUpdataTableName,
            this.isAutoUpdata});
            this.dataGridViewUpdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUpdata.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewUpdata.MultiSelect = false;
            this.dataGridViewUpdata.Name = "dataGridViewUpdata";
            this.dataGridViewUpdata.ReadOnly = true;
            this.dataGridViewUpdata.RowHeadersWidth = 4;
            this.dataGridViewUpdata.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewUpdata.RowTemplate.Height = 23;
            this.dataGridViewUpdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUpdata.Size = new System.Drawing.Size(625, 248);
            this.dataGridViewUpdata.TabIndex = 6;
            this.dataGridViewUpdata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUpdata_CellClick);
            // 
            // AutoUpdataTableName
            // 
            this.AutoUpdataTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AutoUpdataTableName.DataPropertyName = "UpdataTableName";
            this.AutoUpdataTableName.FillWeight = 178.7234F;
            this.AutoUpdataTableName.HeaderText = "表名";
            this.AutoUpdataTableName.Name = "AutoUpdataTableName";
            this.AutoUpdataTableName.ReadOnly = true;
            // 
            // isAutoUpdata
            // 
            this.isAutoUpdata.DataPropertyName = "isAutoUpdata";
            this.isAutoUpdata.FillWeight = 150F;
            this.isAutoUpdata.HeaderText = "是否自动上传";
            this.isAutoUpdata.Name = "isAutoUpdata";
            this.isAutoUpdata.ReadOnly = true;
            this.isAutoUpdata.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isAutoUpdata.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtmEnd);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtmBegin);
            this.groupBox2.Controls.Add(this.rtb_showLog);
            this.groupBox2.Controls.Add(this.butUpload);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 168);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // dtmEnd
            // 
            this.dtmEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmEnd.Location = new System.Drawing.Point(191, 31);
            this.dtmEnd.Name = "dtmEnd";
            this.dtmEnd.Size = new System.Drawing.Size(88, 21);
            this.dtmEnd.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "~";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "时间范围:";
            // 
            // dtmBegin
            // 
            this.dtmBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmBegin.Location = new System.Drawing.Point(81, 31);
            this.dtmBegin.Name = "dtmBegin";
            this.dtmBegin.Size = new System.Drawing.Size(88, 21);
            this.dtmBegin.TabIndex = 3;
            // 
            // rtb_showLog
            // 
            this.rtb_showLog.ContextMenuStrip = this.cmsForMain;
            this.rtb_showLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_showLog.Location = new System.Drawing.Point(3, 67);
            this.rtb_showLog.Name = "rtb_showLog";
            this.rtb_showLog.Size = new System.Drawing.Size(625, 98);
            this.rtb_showLog.TabIndex = 1;
            this.rtb_showLog.Text = "";
            // 
            // cmsForMain
            // 
            this.cmsForMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiCleanLog,
            this.tmiSaveLog});
            this.cmsForMain.Name = "cmsForMain";
            this.cmsForMain.Size = new System.Drawing.Size(153, 70);
            // 
            // tmiCleanLog
            // 
            this.tmiCleanLog.Image = ((System.Drawing.Image)(resources.GetObject("tmiCleanLog.Image")));
            this.tmiCleanLog.Name = "tmiCleanLog";
            this.tmiCleanLog.Size = new System.Drawing.Size(152, 22);
            this.tmiCleanLog.Text = "清空Log";
            this.tmiCleanLog.Click += new System.EventHandler(this.tmiCleanLog_Click);
            // 
            // tmiSaveLog
            // 
            this.tmiSaveLog.Enabled = false;
            this.tmiSaveLog.Image = ((System.Drawing.Image)(resources.GetObject("tmiSaveLog.Image")));
            this.tmiSaveLog.Name = "tmiSaveLog";
            this.tmiSaveLog.Size = new System.Drawing.Size(152, 22);
            this.tmiSaveLog.Text = "Log另存为";
            this.tmiSaveLog.Click += new System.EventHandler(this.tmiSaveLog_Click);
            // 
            // butUpload
            // 
            this.butUpload.Location = new System.Drawing.Point(295, 30);
            this.butUpload.Name = "butUpload";
            this.butUpload.Size = new System.Drawing.Size(75, 23);
            this.butUpload.TabIndex = 2;
            this.butUpload.Text = "立即上传";
            this.butUpload.UseVisualStyleBackColor = true;
            this.butUpload.Click += new System.EventHandler(this.butUpload_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(3, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(625, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStat,
            this.tsp_showProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 441);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "1";
            // 
            // tslStat
            // 
            this.tslStat.Name = "tslStat";
            this.tslStat.Size = new System.Drawing.Size(314, 17);
            this.tslStat.Spring = true;
            // 
            // tsp_showProgress
            // 
            this.tsp_showProgress.Name = "tsp_showProgress";
            this.tsp_showProgress.Size = new System.Drawing.Size(300, 16);
            // 
            // frmDataExchangeMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 463);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(639, 497);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(339, 497);
            this.Name = "frmDataExchangeMain";
            this.Text = "茶山万能转账接口数据上传";
            this.Load += new System.EventHandler(this.frmDataExchangeMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUpdata)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.cmsForMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn AutoUpdataTableName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAutoUpdata;
        internal System.Windows.Forms.DataGridView dataGridViewUpdata;
        internal System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.RichTextBox rtb_showLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ToolStripStatusLabel tslStat;
        internal System.Windows.Forms.ToolStripProgressBar tsp_showProgress;
        internal System.Windows.Forms.DateTimePicker dtmBegin;
        internal System.Windows.Forms.DateTimePicker dtmEnd;
        private System.Windows.Forms.ContextMenuStrip cmsForMain;
        private System.Windows.Forms.ToolStripMenuItem tmiCleanLog;
        private System.Windows.Forms.ToolStripMenuItem tmiSaveLog;
        private System.Windows.Forms.SaveFileDialog sfdForSaveLog;
    }
}