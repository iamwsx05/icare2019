namespace Common.Controls
{
    partial class frmDoctSignByPwd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoctSignByPwd));
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassWord = new DevExpress.XtraEditors.TextEdit();
            this.txtTechName = new DevExpress.XtraEditors.TextEdit();
            this.txtEmpName = new DevExpress.XtraEditors.TextEdit();
            this.txtEmpNo = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.timer = new System.Windows.Forms.Timer();
            this.imageList = new System.Windows.Forms.ImageList();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTechName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.label5);
            this.panelControl.Controls.Add(this.label4);
            this.panelControl.Controls.Add(this.label3);
            this.panelControl.Controls.Add(this.picClose);
            this.panelControl.Controls.Add(this.label2);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Controls.Add(this.txtPassWord);
            this.panelControl.Controls.Add(this.txtTechName);
            this.panelControl.Controls.Add(this.txtEmpName);
            this.panelControl.Controls.Add(this.txtEmpNo);
            this.panelControl.Controls.Add(this.btnOk);
            this.panelControl.Controls.Add(this.btnClose);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(202, 252);
            this.panelControl.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.label5.Location = new System.Drawing.Point(17, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "密码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.label4.Location = new System.Drawing.Point(17, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "职称:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.label3.Location = new System.Drawing.Point(17, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "姓名:";
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(180, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(24, 24);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picClose.TabIndex = 12;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.label2.Location = new System.Drawing.Point(17, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "工号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "确认信息";
            // 
            // txtPassWord
            // 
            this.txtPassWord.EditValue = "";
            this.txtPassWord.Location = new System.Drawing.Point(63, 179);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtPassWord.Properties.Appearance.Options.UseFont = true;
            this.txtPassWord.Properties.LookAndFeel.SkinName = "Money Twins";
            this.txtPassWord.Properties.PasswordChar = '●';
            this.txtPassWord.Size = new System.Drawing.Size(119, 22);
            this.txtPassWord.TabIndex = 1;
            this.txtPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassWord_KeyDown);
            // 
            // txtTechName
            // 
            this.txtTechName.EditValue = "";
            this.txtTechName.Enabled = false;
            this.txtTechName.Location = new System.Drawing.Point(63, 141);
            this.txtTechName.Name = "txtTechName";
            this.txtTechName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtTechName.Properties.Appearance.Options.UseFont = true;
            this.txtTechName.Properties.LookAndFeel.SkinName = "Money Twins";
            this.txtTechName.Size = new System.Drawing.Size(119, 20);
            this.txtTechName.TabIndex = 7;
            // 
            // txtEmpName
            // 
            this.txtEmpName.EditValue = "";
            this.txtEmpName.Enabled = false;
            this.txtEmpName.Location = new System.Drawing.Point(63, 103);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtEmpName.Properties.Appearance.Options.UseFont = true;
            this.txtEmpName.Properties.LookAndFeel.SkinName = "Money Twins";
            this.txtEmpName.Size = new System.Drawing.Size(119, 20);
            this.txtEmpName.TabIndex = 6;
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.EditValue = "";
            this.txtEmpNo.Enabled = false;
            this.txtEmpNo.Location = new System.Drawing.Point(63, 65);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtEmpNo.Properties.Appearance.Options.UseFont = true;
            this.txtEmpNo.Properties.LookAndFeel.SkinName = "Money Twins";
            this.txtEmpNo.Size = new System.Drawing.Size(119, 20);
            this.txtEmpNo.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(30, 222);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 24);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Location = new System.Drawing.Point(116, 222);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "announcements 副本.png");
            this.imageList.Images.SetKeyName(1, "问号.png");
            // 
            // frmDoctSignByPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 252);
            this.Controls.Add(this.panelControl);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoctSignByPwd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "密码确认窗";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDoctSignByPwd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTechName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtPassWord;
        private DevExpress.XtraEditors.TextEdit txtTechName;
        private DevExpress.XtraEditors.TextEdit txtEmpName;
        private DevExpress.XtraEditors.TextEdit txtEmpNo;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label label2;
    }
}