namespace DiagnoseClient
{
    partial class frmDiagnoseClientMain
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
            this.timConnect = new System.Windows.Forms.Timer(this.components);
            this.lnkCurrPatient = new System.Windows.Forms.LinkLabel();
            this.lnkNext = new System.Windows.Forms.LinkLabel();
            this.lnkPrivate = new System.Windows.Forms.LinkLabel();
            this.lnkShare = new System.Windows.Forms.LinkLabel();
            this.lnkClose = new System.Windows.Forms.LinkLabel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timConnect
            // 
            this.timConnect.Interval = 10000;
            this.timConnect.Tick += new System.EventHandler(this.timConnect_Tick);
            // 
            // lnkCurrPatient
            // 
            this.lnkCurrPatient.Location = new System.Drawing.Point(24, 3);
            this.lnkCurrPatient.Name = "lnkCurrPatient";
            this.lnkCurrPatient.Size = new System.Drawing.Size(80, 12);
            this.lnkCurrPatient.TabIndex = 5;
            this.lnkCurrPatient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCurrPatient_LinkClicked);
            // 
            // lnkNext
            // 
            this.lnkNext.AutoSize = true;
            this.lnkNext.Location = new System.Drawing.Point(106, 4);
            this.lnkNext.Name = "lnkNext";
            this.lnkNext.Size = new System.Drawing.Size(49, 14);
            this.lnkNext.TabIndex = 6;
            this.lnkNext.TabStop = true;
            this.lnkNext.Text = "下一个";
            this.lnkNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNext_LinkClicked);
            // 
            // lnkPrivate
            // 
            this.lnkPrivate.Location = new System.Drawing.Point(166, 4);
            this.lnkPrivate.Name = "lnkPrivate";
            this.lnkPrivate.Size = new System.Drawing.Size(96, 16);
            this.lnkPrivate.TabIndex = 7;
            this.lnkPrivate.TabStop = true;
            this.lnkPrivate.Text = "私有队列(0)";
            this.lnkPrivate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPrivate_LinkClicked);
            // 
            // lnkShare
            // 
            this.lnkShare.Location = new System.Drawing.Point(275, 4);
            this.lnkShare.Name = "lnkShare";
            this.lnkShare.Size = new System.Drawing.Size(122, 14);
            this.lnkShare.TabIndex = 8;
            this.lnkShare.TabStop = true;
            this.lnkShare.Text = "共享队列(0)";
            this.lnkShare.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkShare_LinkClicked);
            // 
            // lnkClose
            // 
            this.lnkClose.AutoSize = true;
            this.lnkClose.Location = new System.Drawing.Point(410, 4);
            this.lnkClose.Name = "lnkClose";
            this.lnkClose.Size = new System.Drawing.Size(21, 14);
            this.lnkClose.TabIndex = 9;
            this.lnkClose.TabStop = true;
            this.lnkClose.Text = "×";
            this.lnkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClose_LinkClicked);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(436, 4);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(205, 16);
            this.lblMsg.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(2, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(18, 20);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "●";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblStatus_MouseMove);
            // 
            // frmDiagnoseClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(649, 52);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lnkClose);
            this.Controls.Add(this.lnkShare);
            this.Controls.Add(this.lnkPrivate);
            this.Controls.Add(this.lnkNext);
            this.Controls.Add(this.lnkCurrPatient);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmDiagnoseClientMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "分诊系统客户端";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmDiagnoseClientMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timConnect;
        private System.Windows.Forms.LinkLabel lnkCurrPatient;
        private System.Windows.Forms.LinkLabel lnkNext;
        private System.Windows.Forms.LinkLabel lnkPrivate;
        private System.Windows.Forms.LinkLabel lnkShare;
        private System.Windows.Forms.LinkLabel lnkClose;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblStatus;
    }
}

