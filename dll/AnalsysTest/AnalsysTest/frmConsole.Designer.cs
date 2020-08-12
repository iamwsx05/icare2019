namespace AnalsysTest
{
    partial class frmConsole
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.cboDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.lblPortName = new System.Windows.Forms.Label();
            this.lblBit = new System.Windows.Forms.Label();
            this.lblStop = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 52);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(704, 348);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(24, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(104, 32);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(172, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭串口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "COM Port:";
            // 
            // cboPort
            // 
            this.cboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort.Font = new System.Drawing.Font("宋体", 10F);
            this.cboPort.FormattingEnabled = true;
            this.cboPort.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cboPort.Location = new System.Drawing.Point(366, 4);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(68, 21);
            this.cboPort.TabIndex = 4;
            // 
            // cboDataType
            // 
            this.cboDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataType.Font = new System.Drawing.Font("宋体", 10F);
            this.cboDataType.FormattingEnabled = true;
            this.cboDataType.Items.AddRange(new object[] {
            "ASCII",
            "BYTE"});
            this.cboDataType.Location = new System.Drawing.Point(530, 4);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Size = new System.Drawing.Size(68, 21);
            this.cboDataType.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(462, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Data Type:";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(626, 9);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(59, 12);
            this.lblTip.TabIndex = 7;
            this.lblTip.Text = "COM Port:";
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(375, 37);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(59, 12);
            this.lblPortName.TabIndex = 8;
            this.lblPortName.Text = "COM Port:";
            // 
            // lblBit
            // 
            this.lblBit.AutoSize = true;
            this.lblBit.Location = new System.Drawing.Point(550, 37);
            this.lblBit.Name = "lblBit";
            this.lblBit.Size = new System.Drawing.Size(59, 12);
            this.lblBit.TabIndex = 9;
            this.lblBit.Text = "COM Port:";
            // 
            // lblStop
            // 
            this.lblStop.AutoSize = true;
            this.lblStop.Location = new System.Drawing.Point(626, 37);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(59, 12);
            this.lblStop.TabIndex = 10;
            this.lblStop.Text = "COM Port:";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(462, 37);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(59, 12);
            this.lblB.TabIndex = 11;
            this.lblB.Text = "COM Port:";
            // 
            // frmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 408);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblStop);
            this.Controls.Add(this.lblBit);
            this.Controls.Add(this.lblPortName);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.cboDataType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "接口测试";
            this.Load += new System.EventHandler(this.frmConsole_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.ComboBox cboDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.Label lblBit;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.Label lblB;
    }
}

