namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmAddRoom
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
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.m_cmdSubmit = new PinkieControls.ButtonXP();
            this.m_lblScheme = new System.Windows.Forms.Label();
            this.m_lblAreaName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblDeptName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtRoom = new System.Windows.Forms.TextBox();
            this.m_txtSummary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(163, 104);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(84, 30);
            this.m_cmdCancel.TabIndex = 60;
            this.m_cmdCancel.Text = "取  消";
            // 
            // m_cmdSubmit
            // 
            this.m_cmdSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSubmit.DefaultScheme = true;
            this.m_cmdSubmit.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdSubmit.Hint = "";
            this.m_cmdSubmit.Location = new System.Drawing.Point(35, 104);
            this.m_cmdSubmit.Name = "m_cmdSubmit";
            this.m_cmdSubmit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSubmit.Size = new System.Drawing.Size(84, 30);
            this.m_cmdSubmit.TabIndex = 59;
            this.m_cmdSubmit.Text = "确  定";
            this.m_cmdSubmit.Click += new System.EventHandler(this.m_cmdSubmit_Click);
            // 
            // m_lblScheme
            // 
            this.m_lblScheme.AutoSize = true;
            this.m_lblScheme.Location = new System.Drawing.Point(20, 12);
            this.m_lblScheme.Name = "m_lblScheme";
            this.m_lblScheme.Size = new System.Drawing.Size(70, 14);
            this.m_lblScheme.TabIndex = 57;
            this.m_lblScheme.Text = "诊区名称:";
            // 
            // m_lblAreaName
            // 
            this.m_lblAreaName.AutoSize = true;
            this.m_lblAreaName.Location = new System.Drawing.Point(102, 12);
            this.m_lblAreaName.Name = "m_lblAreaName";
            this.m_lblAreaName.Size = new System.Drawing.Size(63, 14);
            this.m_lblAreaName.TabIndex = 61;
            this.m_lblAreaName.Text = "班次名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 62;
            this.label2.Text = "默认科室:";
            // 
            // m_lblDeptName
            // 
            this.m_lblDeptName.AutoSize = true;
            this.m_lblDeptName.Location = new System.Drawing.Point(104, 30);
            this.m_lblDeptName.Name = "m_lblDeptName";
            this.m_lblDeptName.Size = new System.Drawing.Size(63, 14);
            this.m_lblDeptName.TabIndex = 63;
            this.m_lblDeptName.Text = "班次名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 64;
            this.label1.Text = "诊室名称:";
            // 
            // m_txtRoom
            // 
            this.m_txtRoom.Location = new System.Drawing.Point(105, 47);
            this.m_txtRoom.Name = "m_txtRoom";
            this.m_txtRoom.Size = new System.Drawing.Size(141, 23);
            this.m_txtRoom.TabIndex = 65;
            // 
            // m_txtSummary
            // 
            this.m_txtSummary.Location = new System.Drawing.Point(105, 70);
            this.m_txtSummary.Name = "m_txtSummary";
            this.m_txtSummary.Size = new System.Drawing.Size(141, 23);
            this.m_txtSummary.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 66;
            this.label3.Text = "备    注:";
            // 
            // frmAddRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 146);
            this.Controls.Add(this.m_txtSummary);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtRoom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_lblDeptName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblAreaName);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdSubmit);
            this.Controls.Add(this.m_lblScheme);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmAddRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "添加诊室";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAddRoom_KeyDown);
            this.Load += new System.EventHandler(this.frmAddRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PinkieControls.ButtonXP m_cmdCancel;
        private PinkieControls.ButtonXP m_cmdSubmit;
        private System.Windows.Forms.Label m_lblScheme;
        private System.Windows.Forms.Label m_lblAreaName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_lblDeptName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtRoom;
        private System.Windows.Forms.TextBox m_txtSummary;
        private System.Windows.Forms.Label label3;
    }
}