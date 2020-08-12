namespace Common.Controls
{
    partial class frmSignaturePwd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSignaturePwd));
            this.xtraPanelControl1 = new Common.Controls.xtraPanelControl();
            this.xtraPanelControl2 = new Common.Controls.xtraPanelControl();
            this.btnCancel = new Common.Controls.xtraSimpleButton();
            this.btnOk = new Common.Controls.xtraSimpleButton();
            this.xtraLabelControl1 = new Common.Controls.xtraLabelControl();
            this.xtraLabelControl2 = new Common.Controls.xtraLabelControl();
            this.xtraLabelControl3 = new Common.Controls.xtraLabelControl();
            this.xtraLabelControl4 = new Common.Controls.xtraLabelControl();
            this.txtEmpNo = new Common.Controls.xtraTextEdit();
            this.txtEmpName = new Common.Controls.xtraTextEdit();
            this.txtTechName = new Common.Controls.xtraTextEdit();
            this.txtPwd = new Common.Controls.xtraTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraPanelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraPanelControl2)).BeginInit();
            this.xtraPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTechName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Appearance.BackColor = System.Drawing.Color.White;
            this.pcBackGround.Appearance.Options.UseBackColor = true;
            this.pcBackGround.Controls.Add(this.txtPwd);
            this.pcBackGround.Controls.Add(this.txtTechName);
            this.pcBackGround.Controls.Add(this.txtEmpName);
            this.pcBackGround.Controls.Add(this.txtEmpNo);
            this.pcBackGround.Controls.Add(this.xtraLabelControl4);
            this.pcBackGround.Controls.Add(this.xtraLabelControl3);
            this.pcBackGround.Controls.Add(this.xtraLabelControl2);
            this.pcBackGround.Controls.Add(this.xtraLabelControl1);
            this.pcBackGround.Controls.Add(this.xtraPanelControl2);
            this.pcBackGround.Controls.Add(this.xtraPanelControl1);
            this.pcBackGround.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pcBackGround.Margin = new System.Windows.Forms.Padding(0);
            this.pcBackGround.Size = new System.Drawing.Size(213, 216);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // xtraPanelControl1
            // 
            this.xtraPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraPanelControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraPanelControl1.Name = "xtraPanelControl1";
            this.xtraPanelControl1.Size = new System.Drawing.Size(209, 0);
            this.xtraPanelControl1.TabIndex = 0;
            // 
            // xtraPanelControl2
            // 
            this.xtraPanelControl2.Controls.Add(this.btnCancel);
            this.xtraPanelControl2.Controls.Add(this.btnOk);
            this.xtraPanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xtraPanelControl2.Location = new System.Drawing.Point(2, 183);
            this.xtraPanelControl2.Name = "xtraPanelControl2";
            this.xtraPanelControl2.Size = new System.Drawing.Size(209, 31);
            this.xtraPanelControl2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(119, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消 &C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(24, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // xtraLabelControl1
            // 
            this.xtraLabelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl1.Location = new System.Drawing.Point(22, 27);
            this.xtraLabelControl1.Name = "xtraLabelControl1";
            this.xtraLabelControl1.Size = new System.Drawing.Size(33, 13);
            this.xtraLabelControl1.TabIndex = 7;
            this.xtraLabelControl1.Text = "工号:";
            // 
            // xtraLabelControl2
            // 
            this.xtraLabelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl2.Location = new System.Drawing.Point(22, 66);
            this.xtraLabelControl2.Name = "xtraLabelControl2";
            this.xtraLabelControl2.Size = new System.Drawing.Size(33, 13);
            this.xtraLabelControl2.TabIndex = 8;
            this.xtraLabelControl2.Text = "姓名:";
            // 
            // xtraLabelControl3
            // 
            this.xtraLabelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl3.Location = new System.Drawing.Point(22, 105);
            this.xtraLabelControl3.Name = "xtraLabelControl3";
            this.xtraLabelControl3.Size = new System.Drawing.Size(33, 13);
            this.xtraLabelControl3.TabIndex = 9;
            this.xtraLabelControl3.Text = "职称:";
            // 
            // xtraLabelControl4
            // 
            this.xtraLabelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl4.Location = new System.Drawing.Point(22, 144);
            this.xtraLabelControl4.Name = "xtraLabelControl4";
            this.xtraLabelControl4.Size = new System.Drawing.Size(33, 13);
            this.xtraLabelControl4.TabIndex = 10;
            this.xtraLabelControl4.Text = "密码:";
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Enabled = false;
            this.txtEmpNo.Location = new System.Drawing.Point(68, 23);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEmpNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmpNo.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtEmpNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtEmpNo.Properties.Appearance.Options.UseFont = true;
            this.txtEmpNo.Properties.Appearance.Options.UseForeColor = true;
            this.txtEmpNo.Size = new System.Drawing.Size(124, 20);
            this.txtEmpNo.TabIndex = 11;
            // 
            // txtEmpName
            // 
            this.txtEmpName.Enabled = false;
            this.txtEmpName.Location = new System.Drawing.Point(68, 63);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEmpName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtEmpName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtEmpName.Properties.Appearance.Options.UseBackColor = true;
            this.txtEmpName.Properties.Appearance.Options.UseFont = true;
            this.txtEmpName.Properties.Appearance.Options.UseForeColor = true;
            this.txtEmpName.Size = new System.Drawing.Size(124, 20);
            this.txtEmpName.TabIndex = 12;
            // 
            // txtTechName
            // 
            this.txtTechName.Enabled = false;
            this.txtTechName.Location = new System.Drawing.Point(68, 102);
            this.txtTechName.Name = "txtTechName";
            this.txtTechName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtTechName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtTechName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTechName.Properties.Appearance.Options.UseBackColor = true;
            this.txtTechName.Properties.Appearance.Options.UseFont = true;
            this.txtTechName.Properties.Appearance.Options.UseForeColor = true;
            this.txtTechName.Size = new System.Drawing.Size(124, 20);
            this.txtTechName.TabIndex = 13;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(68, 140);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPwd.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.txtPwd.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPwd.Properties.Appearance.Options.UseBackColor = true;
            this.txtPwd.Properties.Appearance.Options.UseFont = true;
            this.txtPwd.Properties.Appearance.Options.UseForeColor = true;
            this.txtPwd.Properties.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(124, 20);
            this.txtPwd.TabIndex = 0;
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            // 
            // frmSignaturePwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 216);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSignaturePwd";
            this.ShowInTaskbar = false;
            this.Text = "密码验证";
            this.Load += new System.EventHandler(this.frmSignaturePwd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSignaturePwd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraPanelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraPanelControl2)).EndInit();
            this.xtraPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTechName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Controls.xtraPanelControl xtraPanelControl2;
        private Common.Controls.xtraPanelControl xtraPanelControl1;
        private Common.Controls.xtraSimpleButton btnCancel;
        private Common.Controls.xtraSimpleButton btnOk;
        private Common.Controls.xtraTextEdit txtPwd;
        private Common.Controls.xtraTextEdit txtTechName;
        private Common.Controls.xtraTextEdit txtEmpName;
        private Common.Controls.xtraTextEdit txtEmpNo;
        private Common.Controls.xtraLabelControl xtraLabelControl4;
        private Common.Controls.xtraLabelControl xtraLabelControl3;
        private Common.Controls.xtraLabelControl xtraLabelControl2;
        private Common.Controls.xtraLabelControl xtraLabelControl1;

    }
}