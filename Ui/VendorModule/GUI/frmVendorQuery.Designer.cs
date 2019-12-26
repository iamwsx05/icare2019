namespace com.digitalwave.iCare.gui.VendorManage
{
    partial class frmVendorQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendorQuery));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_btnQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.m_btnReSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.m_btnExit = new System.Windows.Forms.ToolStripButton();
            this.m_cbxType = new System.Windows.Forms.ComboBox();
            this.m_txtUSERCODE = new System.Windows.Forms.TextBox();
            this.m_txtAliasName = new System.Windows.Forms.TextBox();
            this.m_txtVendorName = new System.Windows.Forms.TextBox();
            this.m_txtEmail = new System.Windows.Forms.TextBox();
            this.m_txtPhone = new System.Windows.Forms.TextBox();
            this.m_txtFax = new System.Windows.Forms.TextBox();
            this.m_txtAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtContactorPhone = new System.Windows.Forms.TextBox();
            this.m_txtContactor = new System.Windows.Forms.TextBox();
            this.labl3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtWbCode = new System.Windows.Forms.TextBox();
            this.m_txtPyCode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_btnQuery,
            this.toolStripSeparator6,
            this.m_btnReSet,
            this.toolStripSeparator8,
            this.m_btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(313, 41);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.AutoSize = false;
            this.m_btnQuery.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("m_btnQuery.Image")));
            this.m_btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnQuery.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_btnQuery.ImageTransparentColor = System.Drawing.Color.Black;
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(80, 33);
            this.m_btnQuery.Text = "查询(&F)";
            this.m_btnQuery.Click += new System.EventHandler(this.m_btnQuery_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 41);
            // 
            // m_btnReSet
            // 
            this.m_btnReSet.AutoSize = false;
            this.m_btnReSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_btnReSet.Image = ((System.Drawing.Image)(resources.GetObject("m_btnReSet.Image")));
            this.m_btnReSet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnReSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_btnReSet.ImageTransparentColor = System.Drawing.Color.Black;
            this.m_btnReSet.Name = "m_btnReSet";
            this.m_btnReSet.Size = new System.Drawing.Size(80, 32);
            this.m_btnReSet.Text = "清空(&E)";
            this.m_btnReSet.Click += new System.EventHandler(this.m_btnReSet_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 41);
            // 
            // m_btnExit
            // 
            this.m_btnExit.AutoSize = false;
            this.m_btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_btnExit.Image = ((System.Drawing.Image)(resources.GetObject("m_btnExit.Image")));
            this.m_btnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_btnExit.ImageTransparentColor = System.Drawing.Color.Black;
            this.m_btnExit.Name = "m_btnExit";
            this.m_btnExit.Size = new System.Drawing.Size(80, 33);
            this.m_btnExit.Text = "关闭(&Q)";
            this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
            // 
            // m_cbxType
            // 
            this.m_cbxType.FormattingEnabled = true;
            this.m_cbxType.Items.AddRange(new object[] {
            "供应商",
            "生产厂家",
            "两者都是"});
            this.m_cbxType.Location = new System.Drawing.Point(90, 143);
            this.m_cbxType.Name = "m_cbxType";
            this.m_cbxType.Size = new System.Drawing.Size(213, 22);
            this.m_cbxType.TabIndex = 3;
            this.m_cbxType.Enter += new System.EventHandler(this.m_cboStatus_Enter);
            this.m_cbxType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtUSERCODE
            // 
            //this.m_txtUSERCODE.EnableAutoValidation = false;
            //this.m_txtUSERCODE.EnableEnterKeyValidate = false;
            //this.m_txtUSERCODE.EnableEscapeKeyUndo = false;
            //this.m_txtUSERCODE.EnableLastValidValue = false;
            //this.m_txtUSERCODE.ErrorProvider = null;
            //this.m_txtUSERCODE.ErrorProviderMessage = "Invalid value";
            //this.m_txtUSERCODE.ForceFormatText = true;
            this.m_txtUSERCODE.Location = new System.Drawing.Point(90, 44);
            this.m_txtUSERCODE.MaxLength = 5;
            this.m_txtUSERCODE.Name = "m_txtUSERCODE";
            this.m_txtUSERCODE.Size = new System.Drawing.Size(213, 23);
            this.m_txtUSERCODE.TabIndex = 0;
            this.m_txtUSERCODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtAliasName
            // 
            //this.m_txtAliasName.EnableAutoValidation = false;
            //this.m_txtAliasName.EnableEnterKeyValidate = false;
            //this.m_txtAliasName.EnableEscapeKeyUndo = false;
            //this.m_txtAliasName.EnableLastValidValue = false;
            //this.m_txtAliasName.ErrorProvider = null;
            //this.m_txtAliasName.ErrorProviderMessage = "Invalid value";
            //this.m_txtAliasName.ForceFormatText = true;
            this.m_txtAliasName.Location = new System.Drawing.Point(90, 110);
            this.m_txtAliasName.MaxLength = 50;
            this.m_txtAliasName.Name = "m_txtAliasName";
            this.m_txtAliasName.Size = new System.Drawing.Size(213, 23);
            this.m_txtAliasName.TabIndex = 2;
            this.m_txtAliasName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtVendorName
            // 
            //this.m_txtVendorName.EnableAutoValidation = false;
            //this.m_txtVendorName.EnableEnterKeyValidate = false;
            //this.m_txtVendorName.EnableEscapeKeyUndo = false;
            //this.m_txtVendorName.EnableLastValidValue = false;
            //this.m_txtVendorName.ErrorProvider = null;
            //this.m_txtVendorName.ErrorProviderMessage = "Invalid value";
            //this.m_txtVendorName.ForceFormatText = true;
            this.m_txtVendorName.Location = new System.Drawing.Point(90, 77);
            this.m_txtVendorName.MaxLength = 50;
            this.m_txtVendorName.Name = "m_txtVendorName";
            this.m_txtVendorName.Size = new System.Drawing.Size(213, 23);
            this.m_txtVendorName.TabIndex = 1;
            this.m_txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtEmail
            // 
            //this.m_txtEmail.EnableAutoValidation = false;
            //this.m_txtEmail.EnableEnterKeyValidate = false;
            //this.m_txtEmail.EnableEscapeKeyUndo = false;
            //this.m_txtEmail.EnableLastValidValue = false;
            //this.m_txtEmail.ErrorProvider = null;
            //this.m_txtEmail.ErrorProviderMessage = "Invalid value";
            //this.m_txtEmail.ForceFormatText = true;
            this.m_txtEmail.Location = new System.Drawing.Point(90, 340);
            this.m_txtEmail.MaxLength = 100;
            this.m_txtEmail.Name = "m_txtEmail";
            this.m_txtEmail.Size = new System.Drawing.Size(213, 23);
            this.m_txtEmail.TabIndex = 9;
            this.m_txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtPhone
            // 
            //this.m_txtPhone.EnableAutoValidation = false;
            //this.m_txtPhone.EnableEnterKeyValidate = false;
            //this.m_txtPhone.EnableEscapeKeyUndo = false;
            //this.m_txtPhone.EnableLastValidValue = false;
            //this.m_txtPhone.ErrorProvider = null;
            //this.m_txtPhone.ErrorProviderMessage = "Invalid value";
            //this.m_txtPhone.ForceFormatText = true;
            this.m_txtPhone.Location = new System.Drawing.Point(90, 177);
            this.m_txtPhone.MaxLength = 18;
            this.m_txtPhone.Name = "m_txtPhone";
            this.m_txtPhone.Size = new System.Drawing.Size(213, 23);
            this.m_txtPhone.TabIndex = 4;
            this.m_txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtFax
            // 
            //this.m_txtFax.EnableAutoValidation = false;
            //this.m_txtFax.EnableEnterKeyValidate = false;
            //this.m_txtFax.EnableEscapeKeyUndo = false;
            //this.m_txtFax.EnableLastValidValue = false;
            //this.m_txtFax.ErrorProvider = null;
            //this.m_txtFax.ErrorProviderMessage = "Invalid value";
            //this.m_txtFax.ForceFormatText = true;
            this.m_txtFax.Location = new System.Drawing.Point(90, 307);
            this.m_txtFax.MaxLength = 18;
            this.m_txtFax.Name = "m_txtFax";
            this.m_txtFax.Size = new System.Drawing.Size(213, 23);
            this.m_txtFax.TabIndex = 8;
            this.m_txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtAddress
            // 
            //this.m_txtAddress.EnableAutoValidation = false;
            //this.m_txtAddress.EnableEnterKeyValidate = false;
            //this.m_txtAddress.EnableEscapeKeyUndo = false;
            //this.m_txtAddress.EnableLastValidValue = false;
            //this.m_txtAddress.ErrorProvider = null;
            //this.m_txtAddress.ErrorProviderMessage = "Invalid value";
            //this.m_txtAddress.ForceFormatText = true;
            this.m_txtAddress.Location = new System.Drawing.Point(90, 210);
            this.m_txtAddress.MaxLength = 50;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.Size = new System.Drawing.Size(213, 23);
            this.m_txtAddress.TabIndex = 5;
            this.m_txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 308);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 20);
            this.label10.TabIndex = 54;
            this.label10.Text = "传真";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 342);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 18);
            this.label9.TabIndex = 53;
            this.label9.Text = "电子邮件";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 52;
            this.label6.Text = "联系电话";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 22);
            this.label5.TabIndex = 51;
            this.label5.Text = "联系地址";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtContactorPhone
            // 
            //this.m_txtContactorPhone.EnableAutoValidation = false;
            //this.m_txtContactorPhone.EnableEnterKeyValidate = false;
            //this.m_txtContactorPhone.EnableEscapeKeyUndo = false;
            //this.m_txtContactorPhone.EnableLastValidValue = false;
            //this.m_txtContactorPhone.ErrorProvider = null;
            //this.m_txtContactorPhone.ErrorProviderMessage = "Invalid value";
            //this.m_txtContactorPhone.ForceFormatText = true;
            this.m_txtContactorPhone.Location = new System.Drawing.Point(90, 274);
            this.m_txtContactorPhone.MaxLength = 18;
            this.m_txtContactorPhone.Name = "m_txtContactorPhone";
            this.m_txtContactorPhone.Size = new System.Drawing.Size(213, 23);
            this.m_txtContactorPhone.TabIndex = 7;
            this.m_txtContactorPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // m_txtContactor
            // 
            //this.m_txtContactor.EnableAutoValidation = false;
            //this.m_txtContactor.EnableEnterKeyValidate = false;
            //this.m_txtContactor.EnableEscapeKeyUndo = false;
            //this.m_txtContactor.EnableLastValidValue = false;
            //this.m_txtContactor.ErrorProvider = null;
            //this.m_txtContactor.ErrorProviderMessage = "Invalid value";
            //this.m_txtContactor.ForceFormatText = true;
            this.m_txtContactor.Location = new System.Drawing.Point(90, 243);
            this.m_txtContactor.MaxLength = 5;
            this.m_txtContactor.Name = "m_txtContactor";
            this.m_txtContactor.Size = new System.Drawing.Size(213, 23);
            this.m_txtContactor.TabIndex = 6;
            this.m_txtContactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBillId_KeyDown);
            // 
            // labl3
            // 
            this.labl3.Location = new System.Drawing.Point(0, 273);
            this.labl3.Name = "labl3";
            this.labl3.Size = new System.Drawing.Size(80, 24);
            this.labl3.TabIndex = 50;
            this.labl3.Text = "联系人电话";
            this.labl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 24);
            this.label7.TabIndex = 49;
            this.label7.Text = "联系人";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 47;
            this.label4.Text = "类 型";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 46;
            this.label3.Text = "单位简称";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 48;
            this.label2.Text = "单位名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 45;
            this.label1.Text = "单位编码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtWbCode
            // 
            //this.m_txtWbCode.EnableAutoValidation = false;
            //this.m_txtWbCode.EnableEnterKeyValidate = true;
            //this.m_txtWbCode.EnableEscapeKeyUndo = true;
            //this.m_txtWbCode.EnableLastValidValue = true;
            //this.m_txtWbCode.ErrorProvider = null;
            //this.m_txtWbCode.ErrorProviderMessage = "Invalid value";
            //this.m_txtWbCode.ForceFormatText = true;
            this.m_txtWbCode.Location = new System.Drawing.Point(90, 407);
            this.m_txtWbCode.MaxLength = 10;
            this.m_txtWbCode.Name = "m_txtWbCode";
            this.m_txtWbCode.Size = new System.Drawing.Size(213, 23);
            this.m_txtWbCode.TabIndex = 10;
            this.m_txtWbCode.TabStop = false;
            // 
            // m_txtPyCode
            // 
            //this.m_txtPyCode.EnableAutoValidation = false;
            //this.m_txtPyCode.EnableEnterKeyValidate = true;
            //this.m_txtPyCode.EnableEscapeKeyUndo = true;
            //this.m_txtPyCode.EnableLastValidValue = true;
            //this.m_txtPyCode.ErrorProvider = null;
            //this.m_txtPyCode.ErrorProviderMessage = "Invalid value";
            //this.m_txtPyCode.ForceFormatText = true;
            this.m_txtPyCode.Location = new System.Drawing.Point(90, 373);
            this.m_txtPyCode.MaxLength = 10;
            this.m_txtPyCode.Name = "m_txtPyCode";
            this.m_txtPyCode.Size = new System.Drawing.Size(213, 23);
            this.m_txtPyCode.TabIndex = 11;
            this.m_txtPyCode.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(31, 410);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 58;
            this.label12.Text = "五笔码";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 376);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 57;
            this.label11.Text = "拼音码";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmVendorQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 439);
            this.Controls.Add(this.m_txtWbCode);
            this.Controls.Add(this.m_txtPyCode);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_cbxType);
            this.Controls.Add(this.m_txtUSERCODE);
            this.Controls.Add(this.m_txtAliasName);
            this.Controls.Add(this.m_txtVendorName);
            this.Controls.Add(this.m_txtEmail);
            this.Controls.Add(this.m_txtPhone);
            this.Controls.Add(this.m_txtFax);
            this.Controls.Add(this.m_txtAddress);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_txtContactorPhone);
            this.Controls.Add(this.m_txtContactor);
            this.Controls.Add(this.labl3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmVendorQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供应商、生产厂家查询";
            this.Load += new System.EventHandler(this.frmVendorQuery_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_btnQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripButton m_btnReSet;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton m_btnExit;
        internal System.Windows.Forms.ComboBox m_cbxType;
        internal System.Windows.Forms.TextBox m_txtUSERCODE;
        internal System.Windows.Forms.TextBox m_txtAliasName;
        internal System.Windows.Forms.TextBox m_txtVendorName;
        internal System.Windows.Forms.TextBox m_txtEmail;
        internal System.Windows.Forms.TextBox m_txtPhone;
        internal System.Windows.Forms.TextBox m_txtFax;
        internal System.Windows.Forms.TextBox m_txtAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox m_txtContactorPhone;
        internal System.Windows.Forms.TextBox m_txtContactor;
        private System.Windows.Forms.Label labl3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox m_txtWbCode;
        internal System.Windows.Forms.TextBox m_txtPyCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}