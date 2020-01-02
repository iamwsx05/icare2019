namespace com.digitalwave.iCare.LIS
{
    partial class frmDefaultHandle
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
            this.m_txtDeviceSampleID = new System.Windows.Forms.TextBox();
            this.m_txtDeviceSampleID_2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_dtpDeviceCheckDate = new System.Windows.Forms.DateTimePicker();
            this.m_cmdOK = new System.Windows.Forms.Button();
            this.m_cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "仪器样本编号:";
            // 
            // m_txtDeviceSampleID
            // 
            this.m_txtDeviceSampleID.Location = new System.Drawing.Point(106, 23);
            this.m_txtDeviceSampleID.Name = "m_txtDeviceSampleID";
            this.m_txtDeviceSampleID.Size = new System.Drawing.Size(101, 23);
            this.m_txtDeviceSampleID.TabIndex = 0;
            // 
            // m_txtDeviceSampleID_2
            // 
            this.m_txtDeviceSampleID_2.Location = new System.Drawing.Point(235, 23);
            this.m_txtDeviceSampleID_2.Name = "m_txtDeviceSampleID_2";
            this.m_txtDeviceSampleID_2.Size = new System.Drawing.Size(101, 23);
            this.m_txtDeviceSampleID_2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "~~";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "仪器检验日期:";
            // 
            // m_dtpDeviceCheckDate
            // 
            this.m_dtpDeviceCheckDate.Location = new System.Drawing.Point(106, 71);
            this.m_dtpDeviceCheckDate.Name = "m_dtpDeviceCheckDate";
            this.m_dtpDeviceCheckDate.Size = new System.Drawing.Size(230, 23);
            this.m_dtpDeviceCheckDate.TabIndex = 2;
            // 
            // m_cmdOK
            // 
            this.m_cmdOK.Location = new System.Drawing.Point(76, 117);
            this.m_cmdOK.Name = "m_cmdOK";
            this.m_cmdOK.Size = new System.Drawing.Size(75, 23);
            this.m_cmdOK.TabIndex = 3;
            this.m_cmdOK.Text = "确定";
            this.m_cmdOK.UseVisualStyleBackColor = true;
            this.m_cmdOK.Click += new System.EventHandler(this.m_cmdOK_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdCancel.Location = new System.Drawing.Point(210, 117);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.m_cmdCancel.TabIndex = 4;
            this.m_cmdCancel.Text = "取消";
            this.m_cmdCancel.UseVisualStyleBackColor = true;
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // frmDefaultHandle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new System.Drawing.Size(364, 164);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdOK);
            this.Controls.Add(this.m_dtpDeviceCheckDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtDeviceSampleID_2);
            this.Controls.Add(this.m_txtDeviceSampleID);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmDefaultHandle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仪器结果";
            this.Load += new System.EventHandler(this.frmDefaultHandle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtDeviceSampleID;
        private System.Windows.Forms.TextBox m_txtDeviceSampleID_2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker m_dtpDeviceCheckDate;
        private System.Windows.Forms.Button m_cmdOK;
        private System.Windows.Forms.Button m_cmdCancel;
    }
}