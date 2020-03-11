namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmDeleteNameComfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteNameComfirm));
            this.txtName = new com.digitalwave.controls.exTextBox();
            this.btExit = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPS = new com.digitalwave.controls.exTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtID = new com.digitalwave.controls.exTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(88, 40);
            this.txtName.MaxLength = 10;
            this.txtName.Name = "txtName";
            this.txtName.SendTabKey = false;
            this.txtName.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtName.Size = new System.Drawing.Size(184, 21);
            this.txtName.TabIndex = 1;
            // 
            // btExit
            // 
            this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Location = new System.Drawing.Point(240, 138);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(104, 32);
            this.btExit.TabIndex = 7;
            this.btExit.Text = "退出(ESC)";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btOK
            // 
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Location = new System.Drawing.Point(64, 138);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(104, 32);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "权限确认(&S)";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密  码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工  号:";
            // 
            // txtPS
            // 
            this.txtPS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPS.Location = new System.Drawing.Point(88, 72);
            this.txtPS.MaxLength = 16;
            this.txtPS.Name = "txtPS";
            this.txtPS.PasswordChar = '*';
            this.txtPS.SendTabKey = false;
            this.txtPS.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPS.Size = new System.Drawing.Size(184, 26);
            this.txtPS.TabIndex = 2;
            this.txtPS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPS_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btExit);
            this.groupBox1.Controls.Add(this.btOK);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 176);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请输入工号和密码";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPS);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(48, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 104);
            this.panel1.TabIndex = 3;
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Location = new System.Drawing.Point(88, 8);
            this.txtID.MaxLength = 10;
            this.txtID.Name = "txtID";
            this.txtID.SendTabKey = false;
            this.txtID.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtID.Size = new System.Drawing.Size(184, 21);
            this.txtID.TabIndex = 0;
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "姓  名:";
            // 
            // frmDeleteNameComfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btExit;
            this.ClientSize = new System.Drawing.Size(401, 204);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDeleteNameComfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "删除签名权限确认窗口";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private com.digitalwave.controls.exTextBox txtName;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private com.digitalwave.controls.exTextBox txtPS;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Panel panel1;
        public com.digitalwave.controls.exTextBox txtID;
        private System.Windows.Forms.Label label2;
    }
}