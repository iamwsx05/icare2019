namespace com.digitalwave.iCare.RemindMessage
{
    partial class frmInf_SimpleRemindMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInf_SimpleRemindMessage));
            this.m_txtSimpleMessage = new System.Windows.Forms.TextBox();
            this.m_cmdClose = new System.Windows.Forms.Button();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_txtSimpleMessage
            // 
            this.m_txtSimpleMessage.BackColor = System.Drawing.SystemColors.Info;
            this.m_txtSimpleMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtSimpleMessage.Location = new System.Drawing.Point(-2, 1);
            this.m_txtSimpleMessage.Multiline = true;
            this.m_txtSimpleMessage.Name = "m_txtSimpleMessage";
            this.m_txtSimpleMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtSimpleMessage.Size = new System.Drawing.Size(636, 413);
            this.m_txtSimpleMessage.TabIndex = 11;
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdClose.Location = new System.Drawing.Point(279, 420);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Size = new System.Drawing.Size(75, 23);
            this.m_cmdClose.TabIndex = 12;
            this.m_cmdClose.Text = "关闭(&Q)";
            this.m_cmdClose.UseVisualStyleBackColor = true;
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_timer
            // 
            this.m_timer.Interval = 1000;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // frmInf_SimpleRemindMessage
            // 
            this.AcceptButton = this.m_cmdClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cmdClose;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.m_txtSimpleMessage);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInf_SimpleRemindMessage";
            this.Text = "提示信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInf_SimpleRemindMessage_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtSimpleMessage;
        private System.Windows.Forms.Button m_cmdClose;
        private System.Windows.Forms.Timer m_timer;
    }
}