namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmMedFinanceReport
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.datestart = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlprintShow1 = new com.digitalwave.controls.Control.ctlprintShow();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(256, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 23);
            this.label2.TabIndex = 328;
            this.label2.Text = "至";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 327;
            this.label1.Text = "统计日期";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonXP3
            // 
            this.buttonXP3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(879, 12);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(88, 35);
            this.buttonXP3.TabIndex = 326;
            this.buttonXP3.Text = "退出(&E)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(767, 12);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(88, 35);
            this.buttonXP1.TabIndex = 325;
            this.buttonXP1.Text = "打印(&P)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(647, 12);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(88, 35);
            this.buttonXP2.TabIndex = 324;
            this.buttonXP2.Text = "统计(&S)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // dateEnd
            // 
            this.dateEnd.Location = new System.Drawing.Point(283, 16);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(120, 23);
            this.dateEnd.TabIndex = 1;
            // 
            // datestart
            // 
            this.datestart.Location = new System.Drawing.Point(136, 16);
            this.datestart.Name = "datestart";
            this.datestart.Size = new System.Drawing.Size(120, 23);
            this.datestart.TabIndex = 329;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ctlprintShow1);
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(972, 432);
            this.panel1.TabIndex = 3;
            // 
            // ctlprintShow1
            // 
            this.ctlprintShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlprintShow1.Location = new System.Drawing.Point(0, 0);
            this.ctlprintShow1.Name = "ctlprintShow1";
            this.ctlprintShow1.Size = new System.Drawing.Size(970, 430);
            this.ctlprintShow1.TabIndex = 2;
            this.ctlprintShow1.Zoom = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.datestart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonXP3);
            this.groupBox1.Controls.Add(this.buttonXP1);
            this.groupBox1.Controls.Add(this.buttonXP2);
            this.groupBox1.Controls.Add(this.dateEnd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(972, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // frmMedFinanceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 486);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmMedFinanceReport";
            this.Text = "药品进销存明细帐";
            this.Load += new System.EventHandler(this.frmMedFinanceReport_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PinkieControls.ButtonXP buttonXP3;
        private PinkieControls.ButtonXP buttonXP1;
        private PinkieControls.ButtonXP buttonXP2;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.DateTimePicker datestart;
        private System.Windows.Forms.Panel panel1;
        private com.digitalwave.controls.Control.ctlprintShow ctlprintShow1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}