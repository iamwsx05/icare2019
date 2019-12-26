namespace com.digitalwave.iCare.RemindMessage
{
    partial class frmEMR_RemindMessageDetail
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("会诊通知", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("病案审批", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("病案申请", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEMR_RemindMessageDetail));
            this.m_lsvDetail = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_cmdRefresh = new System.Windows.Forms.Button();
            this.m_cmdClose = new System.Windows.Forms.Button();
            this.m_chkConsultation = new System.Windows.Forms.CheckBox();
            this.m_chkCaseHistoryArchiving = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_lsvDetail
            // 
            this.m_lsvDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader4,
            this.columnHeader1});
            this.m_lsvDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvDetail.FullRowSelect = true;
            this.m_lsvDetail.GridLines = true;
            listViewGroup1.Header = "会诊通知";
            listViewGroup1.Name = "Consultation";
            listViewGroup2.Header = "病案审批";
            listViewGroup2.Name = "CaseArchivedAgree";
            listViewGroup3.Header = "病案申请";
            listViewGroup3.Name = "CaseArchivedRequest";
            this.m_lsvDetail.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.m_lsvDetail.Location = new System.Drawing.Point(3, 3);
            this.m_lsvDetail.MultiSelect = false;
            this.m_lsvDetail.Name = "m_lsvDetail";
            this.m_lsvDetail.ShowItemToolTips = true;
            this.m_lsvDetail.Size = new System.Drawing.Size(582, 261);
            this.m_lsvDetail.TabIndex = 0;
            this.m_lsvDetail.UseCompatibleStateImageBehavior = false;
            this.m_lsvDetail.View = System.Windows.Forms.View.Details;
            this.m_lsvDetail.DoubleClick += new System.EventHandler(this.m_lsvDetail_DoubleClick);
            this.m_lsvDetail.SelectedIndexChanged += new System.EventHandler(this.m_lsvDetail_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "信息内容";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "发送者";
            this.columnHeader5.Width = 260;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "发送时间";
            this.columnHeader4.Width = 160;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // m_cmdRefresh
            // 
            this.m_cmdRefresh.Location = new System.Drawing.Point(421, 270);
            this.m_cmdRefresh.Name = "m_cmdRefresh";
            this.m_cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.m_cmdRefresh.TabIndex = 1;
            this.m_cmdRefresh.Text = "刷新";
            this.m_cmdRefresh.UseVisualStyleBackColor = true;
            this.m_cmdRefresh.Click += new System.EventHandler(this.m_cmdRefresh_Click);
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.Location = new System.Drawing.Point(510, 270);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Size = new System.Drawing.Size(75, 23);
            this.m_cmdClose.TabIndex = 1;
            this.m_cmdClose.Text = "关闭";
            this.m_cmdClose.UseVisualStyleBackColor = true;
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_chkConsultation
            // 
            this.m_chkConsultation.AutoSize = true;
            this.m_chkConsultation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_chkConsultation.Location = new System.Drawing.Point(8, 275);
            this.m_chkConsultation.Name = "m_chkConsultation";
            this.m_chkConsultation.Size = new System.Drawing.Size(78, 17);
            this.m_chkConsultation.TabIndex = 2;
            this.m_chkConsultation.Text = "会诊通知";
            this.m_chkConsultation.UseVisualStyleBackColor = true;
            this.m_chkConsultation.CheckedChanged += new System.EventHandler(this.m_chkConsultation_CheckedChanged);
            // 
            // m_chkCaseHistoryArchiving
            // 
            this.m_chkCaseHistoryArchiving.AutoSize = true;
            this.m_chkCaseHistoryArchiving.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_chkCaseHistoryArchiving.Location = new System.Drawing.Point(86, 275);
            this.m_chkCaseHistoryArchiving.Name = "m_chkCaseHistoryArchiving";
            this.m_chkCaseHistoryArchiving.Size = new System.Drawing.Size(102, 17);
            this.m_chkCaseHistoryArchiving.TabIndex = 2;
            this.m_chkCaseHistoryArchiving.Text = "病案审批答复";
            this.m_chkCaseHistoryArchiving.UseVisualStyleBackColor = true;
            this.m_chkCaseHistoryArchiving.CheckedChanged += new System.EventHandler(this.m_chkCaseHistoryArchiving_CheckedChanged);
            // 
            // frmEMR_RemindMessageDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 302);
            this.Controls.Add(this.m_chkCaseHistoryArchiving);
            this.Controls.Add(this.m_chkConsultation);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.m_cmdRefresh);
            this.Controls.Add(this.m_lsvDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmEMR_RemindMessageDetail";
            this.Text = "信息提醒";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEMR_RemindMessageDetail_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView m_lsvDetail;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button m_cmdRefresh;
        private System.Windows.Forms.Button m_cmdClose;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.CheckBox m_chkConsultation;
        private System.Windows.Forms.CheckBox m_chkCaseHistoryArchiving;
    }
}

