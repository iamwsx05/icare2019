namespace DiagnoseClient
{
    partial class frmDiagClientMain
    {
        /// <summary>
        /// 必需的设计器变量.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源.
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
        /// 使用代码编辑器修改此方法的内容.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_lblNotifier = new System.Windows.Forms.Label();
            this.m_lblMessage = new DiagnoseClient.RolloverLabel();
            this.straightLine2 = new Microsoft.Samples.StraightLine();
            this.m_lblShareQueue = new DiagnoseClient.RolloverLabel();
            this.straightLine1 = new Microsoft.Samples.StraightLine();
            this.m_lblPrivateQueue = new DiagnoseClient.RolloverLabel();
            this.straightLine5 = new Microsoft.Samples.StraightLine();
            this.m_rolblLast = new DiagnoseClient.RolloverLabel();
            this.straightLine4 = new Microsoft.Samples.StraightLine();
            this.m_lblMin = new DiagnoseClient.RolloverLabel();
            this.m_lblClose = new DiagnoseClient.RolloverLabel();
            this.m_lblCurrentPatient = new DiagnoseClient.RolloverLabel();
            this.m_lblNext = new DiagnoseClient.RolloverLabel();
            this.straightLine6 = new Microsoft.Samples.StraightLine();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.straightLine9 = new Microsoft.Samples.StraightLine();
            this.m_lblCorpLogo = new DiagnoseClient.RolloverLabel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.straightLine7 = new Microsoft.Samples.StraightLine();
            this.timConnect = new System.Windows.Forms.Timer(this.components);
            this.toolTipMessage = new System.Windows.Forms.ToolTip(this.components);
            this.timerReCalled = new System.Windows.Forms.Timer(this.components);
            this.m_pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_pnlMain.Controls.Add(this.m_lblNotifier);
            this.m_pnlMain.Controls.Add(this.m_lblMessage);
            this.m_pnlMain.Controls.Add(this.straightLine2);
            this.m_pnlMain.Controls.Add(this.m_lblShareQueue);
            this.m_pnlMain.Controls.Add(this.straightLine1);
            this.m_pnlMain.Controls.Add(this.m_lblPrivateQueue);
            this.m_pnlMain.Controls.Add(this.straightLine5);
            this.m_pnlMain.Controls.Add(this.m_rolblLast);
            this.m_pnlMain.Controls.Add(this.straightLine4);
            this.m_pnlMain.Controls.Add(this.m_lblMin);
            this.m_pnlMain.Controls.Add(this.m_lblClose);
            this.m_pnlMain.Controls.Add(this.m_lblCurrentPatient);
            this.m_pnlMain.Controls.Add(this.m_lblNext);
            this.m_pnlMain.Controls.Add(this.straightLine6);
            this.m_pnlMain.Controls.Add(this.pictureBox6);
            this.m_pnlMain.Controls.Add(this.straightLine9);
            this.m_pnlMain.Controls.Add(this.m_lblCorpLogo);
            this.m_pnlMain.Controls.Add(this.pictureBox4);
            this.m_pnlMain.Controls.Add(this.pictureBox5);
            this.m_pnlMain.Controls.Add(this.straightLine7);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(673, 21);
            this.m_pnlMain.TabIndex = 4;
            // 
            // m_lblNotifier
            // 
            this.m_lblNotifier.AutoEllipsis = true;
            this.m_lblNotifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblNotifier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(149)))));
            this.m_lblNotifier.Location = new System.Drawing.Point(395, 1);
            this.m_lblNotifier.Name = "m_lblNotifier";
            this.m_lblNotifier.Size = new System.Drawing.Size(230, 17);
            this.m_lblNotifier.TabIndex = 33;
            this.m_lblNotifier.Text = "1";
            this.m_lblNotifier.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblMessage.IconImage = null;
            this.m_lblMessage.Image = global::DiagnoseClient.Properties.Resources.message_open;
            this.m_lblMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblMessage.Location = new System.Drawing.Point(335, 1);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.RollOutImage = null;
            this.m_lblMessage.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblMessage.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblMessage.RollOverFont = null;
            this.m_lblMessage.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblMessage.RollOverImage = null;
            this.m_lblMessage.Size = new System.Drawing.Size(60, 17);
            this.m_lblMessage.TabIndex = 28;
            this.m_lblMessage.Click += new System.EventHandler(this.m_lblMessage_Click);
            // 
            // straightLine2
            // 
            this.straightLine2.BackColor = System.Drawing.Color.Transparent;
            this.straightLine2.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.straightLine2.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine2.Location = new System.Drawing.Point(331, 1);
            this.straightLine2.Name = "straightLine2";
            this.straightLine2.Size = new System.Drawing.Size(4, 17);
            this.straightLine2.TabIndex = 25;
            this.straightLine2.Text = "straightLine2";
            this.straightLine2.Thickness = 2;
            // 
            // m_lblShareQueue
            // 
            this.m_lblShareQueue.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblShareQueue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(149)))));
            this.m_lblShareQueue.IconImage = null;
            this.m_lblShareQueue.Image = global::DiagnoseClient.Properties.Resources.ShareQueue;
            this.m_lblShareQueue.Location = new System.Drawing.Point(248, 1);
            this.m_lblShareQueue.Name = "m_lblShareQueue";
            this.m_lblShareQueue.RollOutImage = null;
            this.m_lblShareQueue.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblShareQueue.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblShareQueue.RollOverFont = null;
            this.m_lblShareQueue.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblShareQueue.RollOverImage = null;
            this.m_lblShareQueue.Size = new System.Drawing.Size(83, 17);
            this.m_lblShareQueue.TabIndex = 15;
            this.m_lblShareQueue.Text = "0";
            this.m_lblShareQueue.Click += new System.EventHandler(this.m_lblShareQueue_Click);
            // 
            // straightLine1
            // 
            this.straightLine1.BackColor = System.Drawing.Color.Transparent;
            this.straightLine1.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.straightLine1.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine1.Location = new System.Drawing.Point(244, 1);
            this.straightLine1.Name = "straightLine1";
            this.straightLine1.Size = new System.Drawing.Size(4, 17);
            this.straightLine1.TabIndex = 23;
            this.straightLine1.Text = "straightLine1";
            this.straightLine1.Thickness = 2;
            // 
            // m_lblPrivateQueue
            // 
            this.m_lblPrivateQueue.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblPrivateQueue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(149)))));
            this.m_lblPrivateQueue.IconImage = null;
            this.m_lblPrivateQueue.Image = global::DiagnoseClient.Properties.Resources.PrivateQueue;
            this.m_lblPrivateQueue.Location = new System.Drawing.Point(161, 1);
            this.m_lblPrivateQueue.Name = "m_lblPrivateQueue";
            this.m_lblPrivateQueue.RollOutImage = null;
            this.m_lblPrivateQueue.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblPrivateQueue.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblPrivateQueue.RollOverFont = null;
            this.m_lblPrivateQueue.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblPrivateQueue.RollOverImage = null;
            this.m_lblPrivateQueue.Size = new System.Drawing.Size(83, 17);
            this.m_lblPrivateQueue.TabIndex = 24;
            this.m_lblPrivateQueue.Text = "0";
            this.m_lblPrivateQueue.Click += new System.EventHandler(this.m_lblPrivateQueue_Click);
            // 
            // straightLine5
            // 
            this.straightLine5.BackColor = System.Drawing.Color.Transparent;
            this.straightLine5.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.straightLine5.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine5.Location = new System.Drawing.Point(157, 1);
            this.straightLine5.Name = "straightLine5";
            this.straightLine5.Size = new System.Drawing.Size(4, 17);
            this.straightLine5.TabIndex = 35;
            this.straightLine5.Text = "straightLine5";
            this.straightLine5.Thickness = 2;
            // 
            // m_rolblLast
            // 
            this.m_rolblLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rolblLast.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_rolblLast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(149)))));
            this.m_rolblLast.IconImage = null;
            this.m_rolblLast.Location = new System.Drawing.Point(127, 1);
            this.m_rolblLast.Name = "m_rolblLast";
            this.m_rolblLast.RollOutImage = null;
            this.m_rolblLast.RollOverBackColor = System.Drawing.Color.White;
            this.m_rolblLast.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_rolblLast.RollOverFont = null;
            this.m_rolblLast.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_rolblLast.RollOverImage = null;
            this.m_rolblLast.Size = new System.Drawing.Size(30, 17);
            this.m_rolblLast.TabIndex = 34;
            this.m_rolblLast.Text = "轮回";
            this.m_rolblLast.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_rolblLast.Click += new System.EventHandler(this.m_rolblLast_Click);
            // 
            // straightLine4
            // 
            this.straightLine4.BackColor = System.Drawing.Color.Transparent;
            this.straightLine4.Dock = System.Windows.Forms.DockStyle.Right;
            this.straightLine4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.straightLine4.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine4.Location = new System.Drawing.Point(625, 1);
            this.straightLine4.Name = "straightLine4";
            this.straightLine4.Size = new System.Drawing.Size(4, 17);
            this.straightLine4.TabIndex = 29;
            this.straightLine4.Text = "straightLine4";
            this.straightLine4.Thickness = 2;
            // 
            // m_lblMin
            // 
            this.m_lblMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lblMin.IconImage = null;
            this.m_lblMin.Image = global::DiagnoseClient.Properties.Resources.MIN;
            this.m_lblMin.Location = new System.Drawing.Point(629, 1);
            this.m_lblMin.Name = "m_lblMin";
            this.m_lblMin.RollOutImage = null;
            this.m_lblMin.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblMin.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblMin.RollOverFont = null;
            this.m_lblMin.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblMin.RollOverImage = null;
            this.m_lblMin.Size = new System.Drawing.Size(17, 17);
            this.m_lblMin.TabIndex = 30;
            this.m_lblMin.Click += new System.EventHandler(this.m_lblMin_Click);
            // 
            // m_lblClose
            // 
            this.m_lblClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lblClose.IconImage = null;
            this.m_lblClose.Image = global::DiagnoseClient.Properties.Resources.Close;
            this.m_lblClose.Location = new System.Drawing.Point(646, 1);
            this.m_lblClose.Name = "m_lblClose";
            this.m_lblClose.RollOutImage = null;
            this.m_lblClose.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblClose.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblClose.RollOverFont = null;
            this.m_lblClose.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblClose.RollOverImage = null;
            this.m_lblClose.Size = new System.Drawing.Size(11, 17);
            this.m_lblClose.TabIndex = 31;
            this.m_lblClose.Click += new System.EventHandler(this.m_lblClose_Click);
            // 
            // m_lblCurrentPatient
            // 
            this.m_lblCurrentPatient.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblCurrentPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(149)))));
            this.m_lblCurrentPatient.IconImage = null;
            this.m_lblCurrentPatient.Image = global::DiagnoseClient.Properties.Resources.CURRENT_MAN;
            this.m_lblCurrentPatient.Location = new System.Drawing.Point(71, 1);
            this.m_lblCurrentPatient.Name = "m_lblCurrentPatient";
            this.m_lblCurrentPatient.RollOutImage = null;
            this.m_lblCurrentPatient.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblCurrentPatient.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblCurrentPatient.RollOverFont = null;
            this.m_lblCurrentPatient.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblCurrentPatient.RollOverImage = null;
            this.m_lblCurrentPatient.Size = new System.Drawing.Size(56, 17);
            this.m_lblCurrentPatient.TabIndex = 22;
            this.m_lblCurrentPatient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblCurrentPatient.Click += new System.EventHandler(this.m_lblCurrentMan_Click);
            // 
            // m_lblNext
            // 
            this.m_lblNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNext.IconImage = null;
            this.m_lblNext.Image = global::DiagnoseClient.Properties.Resources.NEXT;
            this.m_lblNext.Location = new System.Drawing.Point(41, 1);
            this.m_lblNext.Name = "m_lblNext";
            this.m_lblNext.RollOutImage = null;
            this.m_lblNext.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblNext.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblNext.RollOverFont = null;
            this.m_lblNext.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblNext.RollOverImage = null;
            this.m_lblNext.Size = new System.Drawing.Size(30, 17);
            this.m_lblNext.TabIndex = 21;
            this.m_lblNext.Click += new System.EventHandler(this.m_lblNext_Click);
            // 
            // straightLine6
            // 
            this.straightLine6.BackColor = System.Drawing.Color.Transparent;
            this.straightLine6.Dock = System.Windows.Forms.DockStyle.Right;
            this.straightLine6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.straightLine6.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine6.Location = new System.Drawing.Point(657, 1);
            this.straightLine6.Name = "straightLine6";
            this.straightLine6.Size = new System.Drawing.Size(4, 17);
            this.straightLine6.TabIndex = 20;
            this.straightLine6.Text = "straightLine3";
            this.straightLine6.Thickness = 2;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = global::DiagnoseClient.Properties.Resources.LAST;
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox6.Location = new System.Drawing.Point(661, 1);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(12, 17);
            this.pictureBox6.TabIndex = 1;
            this.pictureBox6.TabStop = false;
            // 
            // straightLine9
            // 
            this.straightLine9.BackColor = System.Drawing.Color.Transparent;
            this.straightLine9.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(193)))), ((int)(((byte)(212)))));
            this.straightLine9.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine9.Location = new System.Drawing.Point(37, 1);
            this.straightLine9.Name = "straightLine9";
            this.straightLine9.Size = new System.Drawing.Size(4, 17);
            this.straightLine9.TabIndex = 6;
            this.straightLine9.Text = "straightLine1";
            this.straightLine9.Thickness = 2;
            // 
            // m_lblCorpLogo
            // 
            this.m_lblCorpLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblCorpLogo.IconImage = null;
            this.m_lblCorpLogo.Image = global::DiagnoseClient.Properties.Resources.Logo_offLine;
            this.m_lblCorpLogo.Location = new System.Drawing.Point(12, 1);
            this.m_lblCorpLogo.Name = "m_lblCorpLogo";
            this.m_lblCorpLogo.RollOutImage = null;
            this.m_lblCorpLogo.RollOverBackColor = System.Drawing.Color.White;
            this.m_lblCorpLogo.RollOverCursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblCorpLogo.RollOverFont = null;
            this.m_lblCorpLogo.RollOverForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblCorpLogo.RollOverImage = null;
            this.m_lblCorpLogo.Size = new System.Drawing.Size(25, 17);
            this.m_lblCorpLogo.TabIndex = 32;
            this.m_lblCorpLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_lblCorpLogo_MouseMove);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::DiagnoseClient.Properties.Resources.Header_bg;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox4.Location = new System.Drawing.Point(0, 1);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(12, 17);
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::DiagnoseClient.Properties.Resources.bg;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox5.Location = new System.Drawing.Point(0, 18);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(673, 3);
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            // 
            // straightLine7
            // 
            this.straightLine7.BackColor = System.Drawing.Color.Transparent;
            this.straightLine7.Dock = System.Windows.Forms.DockStyle.Top;
            this.straightLine7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(190)))), ((int)(((byte)(212)))));
            this.straightLine7.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine7.Location = new System.Drawing.Point(0, 0);
            this.straightLine7.Name = "straightLine7";
            this.straightLine7.Size = new System.Drawing.Size(673, 1);
            this.straightLine7.TabIndex = 1;
            this.straightLine7.Text = "straightLine7";
            // 
            // timConnect
            // 
            this.timConnect.Interval = 10000;
            this.timConnect.Tick += new System.EventHandler(this.timConnect_Tick);
            // 
            // toolTipMessage
            // 
            this.toolTipMessage.AutomaticDelay = 0;
            this.toolTipMessage.ShowAlways = true;
            this.toolTipMessage.UseAnimation = false;
            this.toolTipMessage.UseFading = false;
            // 
            // timerReCalled
            // 
            this.timerReCalled.Interval = 1000;
            this.timerReCalled.Tick += new System.EventHandler(this.timerReCalled_Tick);
            // 
            // frmDiagClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(673, 21);
            this.Controls.Add(this.m_pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDiagClientMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "iCare-门诊呼叫器";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDiagClientMain_FormClosing);
            this.Load += new System.EventHandler(this.frmDiagClientMain_Load);
            this.m_pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlMain;
        private RolloverLabel m_lblNext;
        private Microsoft.Samples.StraightLine straightLine6;
        private System.Windows.Forms.PictureBox pictureBox6;
        private Microsoft.Samples.StraightLine straightLine9;
        private System.Windows.Forms.PictureBox pictureBox4;
        private Microsoft.Samples.StraightLine straightLine7;
        private System.Windows.Forms.PictureBox pictureBox5;
        private RolloverLabel m_lblMin;
        private RolloverLabel m_lblClose;
        private Microsoft.Samples.StraightLine straightLine4;
        private RolloverLabel m_lblMessage;
        private RolloverLabel m_lblShareQueue;
        private Microsoft.Samples.StraightLine straightLine2;
        private RolloverLabel m_lblPrivateQueue;
        private Microsoft.Samples.StraightLine straightLine1;
        private RolloverLabel m_lblCurrentPatient;
        private RolloverLabel m_lblCorpLogo;
        private System.Windows.Forms.Timer timConnect;
        private System.Windows.Forms.Label m_lblNotifier;
        private System.Windows.Forms.ToolTip toolTipMessage;
        private RolloverLabel m_rolblLast;
        private Microsoft.Samples.StraightLine straightLine5;
        private System.Windows.Forms.Timer timerReCalled;
    }
}