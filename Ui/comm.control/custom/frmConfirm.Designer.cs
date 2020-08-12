namespace Common.Controls
{
    partial class frmConfirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPwd = new Common.Controls.xtraTextEdit();
            this.txtEmpNo = new Common.Controls.xtraTextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpName = new Common.Controls.xtraTextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.lblInfo);
            this.pcBackGround.Controls.Add(this.txtEmpName);
            this.pcBackGround.Controls.Add(this.label1);
            this.pcBackGround.Controls.Add(this.txtPwd);
            this.pcBackGround.Controls.Add(this.txtEmpNo);
            this.pcBackGround.Controls.Add(this.label2);
            this.pcBackGround.Controls.Add(this.label3);
            this.pcBackGround.Size = new System.Drawing.Size(206, 157);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(55, 108);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.PasswordChar = '●';
            this.txtPwd.Size = new System.Drawing.Size(137, 20);
            this.txtPwd.TabIndex = 25;
            this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Location = new System.Drawing.Point(55, 24);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(137, 20);
            this.txtEmpNo.TabIndex = 24;
            this.txtEmpNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmpNo_KeyDown);
            this.txtEmpNo.Leave += new System.EventHandler(this.txtEmpNo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label2.Location = new System.Drawing.Point(11, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "工号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label3.Location = new System.Drawing.Point(11, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "密码：";
            // 
            // txtEmpName
            // 
            this.txtEmpName.Location = new System.Drawing.Point(55, 66);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtEmpName.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtEmpName.Properties.Appearance.Options.UseFont = true;
            this.txtEmpName.Properties.Appearance.Options.UseForeColor = true;
            this.txtEmpName.Properties.ReadOnly = true;
            this.txtEmpName.Size = new System.Drawing.Size(137, 20);
            this.txtEmpName.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label1.Location = new System.Drawing.Point(11, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "姓名：";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(69, 139);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(71, 13);
            this.lblInfo.TabIndex = 28;
            this.lblInfo.Text = "*工号有误";
            this.lblInfo.Visible = false;
            // 
            // frmConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 157);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmConfirm";
            this.ShowInTaskbar = false;
            this.Text = "确认";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfirm_FormClosing);
            this.Load += new System.EventHandler(this.frmConfirm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmConfirm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private xtraTextEdit txtEmpName;
        private System.Windows.Forms.Label label1;
        private xtraTextEdit txtPwd;
        private xtraTextEdit txtEmpNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInfo;
    }
}