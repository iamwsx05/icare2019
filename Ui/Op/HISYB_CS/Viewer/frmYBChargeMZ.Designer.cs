namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmYBChargeMZ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmYBChargeMZ));
            this.llblReadQRcode = new System.Windows.Forms.LinkLabel();
            this.rdoEk = new System.Windows.Forms.RadioButton();
            this.rdoSk = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSdywh = new System.Windows.Forms.Label();
            this.lblIsYBjs = new System.Windows.Forms.Label();
            this.lblCancelCharge = new System.Windows.Forms.LinkLabel();
            this.lblCancelUpload = new System.Windows.Forms.LinkLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheckPsw = new DevExpress.XtraEditors.SimpleButton();
            this.btnCharge = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblZyh = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.TextEdit();
            this.lbldyxsdz = new DevExpress.XtraEditors.TextEdit();
            this.lblTotal = new DevExpress.XtraEditors.TextEdit();
            this.lblJsxh = new DevExpress.XtraEditors.TextEdit();
            this.lblAcc = new DevExpress.XtraEditors.TextEdit();
            this.lblSub = new DevExpress.XtraEditors.TextEdit();
            this.txtReason = new DevExpress.XtraEditors.MemoEdit();
            this.cmbJzlb = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblZyh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldyxsdz.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJsxh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAcc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSub.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbJzlb.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // llblReadQRcode
            // 
            this.llblReadQRcode.ActiveLinkColor = System.Drawing.Color.Crimson;
            this.llblReadQRcode.AutoSize = true;
            this.llblReadQRcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.llblReadQRcode.Font = new System.Drawing.Font("宋体", 9.5F);
            this.llblReadQRcode.LinkColor = System.Drawing.Color.Crimson;
            this.llblReadQRcode.Location = new System.Drawing.Point(301, 365);
            this.llblReadQRcode.Name = "llblReadQRcode";
            this.llblReadQRcode.Size = new System.Drawing.Size(137, 13);
            this.llblReadQRcode.TabIndex = 100;
            this.llblReadQRcode.TabStop = true;
            this.llblReadQRcode.Text = "扫描电子社保卡二维码";
            this.llblReadQRcode.VisitedLinkColor = System.Drawing.Color.Crimson;
            this.llblReadQRcode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblReadECard_LinkClicked);
            // 
            // rdoEk
            // 
            this.rdoEk.AutoSize = true;
            this.rdoEk.Location = new System.Drawing.Point(226, 363);
            this.rdoEk.Name = "rdoEk";
            this.rdoEk.Size = new System.Drawing.Size(59, 16);
            this.rdoEk.TabIndex = 99;
            this.rdoEk.Text = "电子卡";
            this.rdoEk.UseVisualStyleBackColor = true;
            // 
            // rdoSk
            // 
            this.rdoSk.AutoSize = true;
            this.rdoSk.Checked = true;
            this.rdoSk.Location = new System.Drawing.Point(148, 363);
            this.rdoSk.Name = "rdoSk";
            this.rdoSk.Size = new System.Drawing.Size(59, 16);
            this.rdoSk.TabIndex = 98;
            this.rdoSk.TabStop = true;
            this.rdoSk.Text = "实体卡";
            this.rdoSk.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label7.Location = new System.Drawing.Point(361, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 22);
            this.label7.TabIndex = 96;
            this.label7.Text = "结算序号";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSdywh
            // 
            this.lblSdywh.AutoSize = true;
            this.lblSdywh.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lblSdywh.Location = new System.Drawing.Point(579, 468);
            this.lblSdywh.Name = "lblSdywh";
            this.lblSdywh.Size = new System.Drawing.Size(59, 13);
            this.lblSdywh.TabIndex = 93;
            this.lblSdywh.Text = "结算序号";
            this.lblSdywh.Visible = false;
            // 
            // lblIsYBjs
            // 
            this.lblIsYBjs.AutoSize = true;
            this.lblIsYBjs.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lblIsYBjs.ForeColor = System.Drawing.Color.Red;
            this.lblIsYBjs.Location = new System.Drawing.Point(424, 468);
            this.lblIsYBjs.Name = "lblIsYBjs";
            this.lblIsYBjs.Size = new System.Drawing.Size(124, 13);
            this.lblIsYBjs.TabIndex = 92;
            this.lblIsYBjs.Text = "此病人已做医保结算";
            // 
            // lblCancelCharge
            // 
            this.lblCancelCharge.AutoSize = true;
            this.lblCancelCharge.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lblCancelCharge.Location = new System.Drawing.Point(300, 468);
            this.lblCancelCharge.Name = "lblCancelCharge";
            this.lblCancelCharge.Size = new System.Drawing.Size(85, 13);
            this.lblCancelCharge.TabIndex = 89;
            this.lblCancelCharge.TabStop = true;
            this.lblCancelCharge.Text = "取消医保结算";
            this.lblCancelCharge.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCancelCharge_LinkClicked);
            // 
            // lblCancelUpload
            // 
            this.lblCancelUpload.AutoSize = true;
            this.lblCancelUpload.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lblCancelUpload.Location = new System.Drawing.Point(47, 468);
            this.lblCancelUpload.Name = "lblCancelUpload";
            this.lblCancelUpload.Size = new System.Drawing.Size(85, 13);
            this.lblCancelUpload.TabIndex = 88;
            this.lblCancelUpload.TabStop = true;
            this.lblCancelUpload.Text = "撤销数据上传";
            this.lblCancelUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCancelUpload_LinkClicked);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label16.Location = new System.Drawing.Point(44, 148);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 13);
            this.label16.TabIndex = 76;
            this.label16.Text = "不能享受待遇原因";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(361, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 22);
            this.label4.TabIndex = 67;
            this.label4.Text = "自费金额";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(66, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 22);
            this.label3.TabIndex = 66;
            this.label3.Text = "记账金额";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label9.Location = new System.Drawing.Point(361, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 22);
            this.label9.TabIndex = 62;
            this.label9.Text = "待遇享受标志";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label12.Location = new System.Drawing.Point(83, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 22);
            this.label12.TabIndex = 58;
            this.label12.Text = "就诊类别";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(12, 360);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(86, 22);
            this.lblInfo.TabIndex = 55;
            this.lblInfo.Text = "【功能区域】";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label48.Location = new System.Drawing.Point(361, 64);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(118, 22);
            this.label48.TabIndex = 51;
            this.label48.Text = "姓名";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label44
            // 
            this.label44.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.label44.Location = new System.Drawing.Point(12, 28);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(86, 21);
            this.label44.TabIndex = 47;
            this.label44.Text = "【基本资料】";
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label43.Location = new System.Drawing.Point(43, 64);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(112, 22);
            this.label43.TabIndex = 46;
            this.label43.Text = "门诊号(住院号)";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label6.Location = new System.Drawing.Point(55, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 38;
            this.label6.Text = "医疗费总金额";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9.5F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "【总金额】";
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.cmbJzlb);
            this.groupControl1.Controls.Add(this.txtReason);
            this.groupControl1.Controls.Add(this.lblSub);
            this.groupControl1.Controls.Add(this.lblAcc);
            this.groupControl1.Controls.Add(this.lblJsxh);
            this.groupControl1.Controls.Add(this.lblTotal);
            this.groupControl1.Controls.Add(this.lbldyxsdz);
            this.groupControl1.Controls.Add(this.lblName);
            this.groupControl1.Controls.Add(this.lblZyh);
            this.groupControl1.Controls.Add(this.btnClose);
            this.groupControl1.Controls.Add(this.btnOk);
            this.groupControl1.Controls.Add(this.btnCharge);
            this.groupControl1.Controls.Add(this.btnCheckPsw);
            this.groupControl1.Controls.Add(this.btnUpload);
            this.groupControl1.Controls.Add(this.llblReadQRcode);
            this.groupControl1.Controls.Add(this.label44);
            this.groupControl1.Controls.Add(this.rdoEk);
            this.groupControl1.Controls.Add(this.label43);
            this.groupControl1.Controls.Add(this.rdoSk);
            this.groupControl1.Controls.Add(this.label48);
            this.groupControl1.Controls.Add(this.lblSdywh);
            this.groupControl1.Controls.Add(this.lblIsYBjs);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.lblCancelCharge);
            this.groupControl1.Controls.Add(this.lblCancelUpload);
            this.groupControl1.Controls.Add(this.label12);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.lblInfo);
            this.groupControl1.Controls.Add(this.label16);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(694, 507);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "门诊医保结算";
            // 
            // btnUpload
            // 
            this.btnUpload.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnUpload.Appearance.Options.UseFont = true;
            this.btnUpload.Location = new System.Drawing.Point(46, 416);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 32);
            this.btnUpload.TabIndex = 101;
            this.btnUpload.Text = "数据上传 »";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCheckPsw
            // 
            this.btnCheckPsw.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnCheckPsw.Appearance.Options.UseFont = true;
            this.btnCheckPsw.Location = new System.Drawing.Point(161, 416);
            this.btnCheckPsw.Name = "btnCheckPsw";
            this.btnCheckPsw.Size = new System.Drawing.Size(134, 32);
            this.btnCheckPsw.TabIndex = 102;
            this.btnCheckPsw.Text = "社保卡密码验证 »";
            this.btnCheckPsw.Click += new System.EventHandler(this.btnCheckPsw_Click);
            // 
            // btnCharge
            // 
            this.btnCharge.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnCharge.Appearance.Options.UseFont = true;
            this.btnCharge.Location = new System.Drawing.Point(303, 416);
            this.btnCharge.Name = "btnCharge";
            this.btnCharge.Size = new System.Drawing.Size(106, 32);
            this.btnCharge.TabIndex = 103;
            this.btnCharge.Text = "医保结算 »";
            this.btnCharge.Click += new System.EventHandler(this.btnCharge_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(478, 416);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 32);
            this.btnOk.TabIndex = 104;
            this.btnOk.Text = "完成  ";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(583, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 105;
            this.btnClose.Text = "退出  ";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblZyh
            // 
            this.lblZyh.Location = new System.Drawing.Point(161, 61);
            this.lblZyh.Name = "lblZyh";
            this.lblZyh.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lblZyh.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZyh.Properties.Appearance.Options.UseBackColor = true;
            this.lblZyh.Properties.Appearance.Options.UseFont = true;
            this.lblZyh.Properties.AutoHeight = false;
            this.lblZyh.Properties.ReadOnly = true;
            this.lblZyh.Size = new System.Drawing.Size(188, 28);
            this.lblZyh.TabIndex = 107;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(483, 61);
            this.lblName.Name = "lblName";
            this.lblName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lblName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Properties.Appearance.Options.UseBackColor = true;
            this.lblName.Properties.Appearance.Options.UseFont = true;
            this.lblName.Properties.AutoHeight = false;
            this.lblName.Properties.ReadOnly = true;
            this.lblName.Size = new System.Drawing.Size(188, 28);
            this.lblName.TabIndex = 108;
            // 
            // lbldyxsdz
            // 
            this.lbldyxsdz.Location = new System.Drawing.Point(483, 100);
            this.lbldyxsdz.Name = "lbldyxsdz";
            this.lbldyxsdz.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lbldyxsdz.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldyxsdz.Properties.Appearance.Options.UseBackColor = true;
            this.lbldyxsdz.Properties.Appearance.Options.UseFont = true;
            this.lbldyxsdz.Properties.AutoHeight = false;
            this.lbldyxsdz.Properties.ReadOnly = true;
            this.lbldyxsdz.Size = new System.Drawing.Size(188, 28);
            this.lbldyxsdz.TabIndex = 109;
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(161, 256);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lblTotal.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Properties.Appearance.Options.UseBackColor = true;
            this.lblTotal.Properties.Appearance.Options.UseFont = true;
            this.lblTotal.Properties.AutoHeight = false;
            this.lblTotal.Properties.ReadOnly = true;
            this.lblTotal.Size = new System.Drawing.Size(188, 28);
            this.lblTotal.TabIndex = 110;
            // 
            // lblJsxh
            // 
            this.lblJsxh.Location = new System.Drawing.Point(483, 256);
            this.lblJsxh.Name = "lblJsxh";
            this.lblJsxh.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lblJsxh.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJsxh.Properties.Appearance.Options.UseBackColor = true;
            this.lblJsxh.Properties.Appearance.Options.UseFont = true;
            this.lblJsxh.Properties.AutoHeight = false;
            this.lblJsxh.Properties.ReadOnly = true;
            this.lblJsxh.Size = new System.Drawing.Size(188, 28);
            this.lblJsxh.TabIndex = 111;
            // 
            // lblAcc
            // 
            this.lblAcc.Location = new System.Drawing.Point(161, 300);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lblAcc.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcc.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblAcc.Properties.Appearance.Options.UseBackColor = true;
            this.lblAcc.Properties.Appearance.Options.UseFont = true;
            this.lblAcc.Properties.Appearance.Options.UseForeColor = true;
            this.lblAcc.Properties.AutoHeight = false;
            this.lblAcc.Properties.ReadOnly = true;
            this.lblAcc.Size = new System.Drawing.Size(188, 28);
            this.lblAcc.TabIndex = 112;
            // 
            // lblSub
            // 
            this.lblSub.Location = new System.Drawing.Point(483, 300);
            this.lblSub.Name = "lblSub";
            this.lblSub.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lblSub.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblSub.Properties.Appearance.Options.UseBackColor = true;
            this.lblSub.Properties.Appearance.Options.UseFont = true;
            this.lblSub.Properties.Appearance.Options.UseForeColor = true;
            this.lblSub.Properties.AutoHeight = false;
            this.lblSub.Properties.ReadOnly = true;
            this.lblSub.Size = new System.Drawing.Size(188, 28);
            this.lblSub.TabIndex = 113;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(161, 145);
            this.txtReason.Name = "txtReason";
            this.txtReason.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtReason.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txtReason.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtReason.Properties.Appearance.Options.UseBackColor = true;
            this.txtReason.Properties.Appearance.Options.UseFont = true;
            this.txtReason.Properties.Appearance.Options.UseForeColor = true;
            this.txtReason.Properties.ReadOnly = true;
            this.txtReason.Size = new System.Drawing.Size(510, 83);
            this.txtReason.TabIndex = 114;
            // 
            // cmbJzlb
            // 
            this.cmbJzlb.Location = new System.Drawing.Point(161, 100);
            this.cmbJzlb.Name = "cmbJzlb";
            this.cmbJzlb.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cmbJzlb.Properties.Appearance.Options.UseFont = true;
            this.cmbJzlb.Properties.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cmbJzlb.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmbJzlb.Properties.AutoHeight = false;
            this.cmbJzlb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbJzlb.Properties.DropDownItemHeight = 25;
            this.cmbJzlb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbJzlb.Size = new System.Drawing.Size(188, 28);
            this.cmbJzlb.TabIndex = 115;
            // 
            // frmYBChargeMZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 507);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmYBChargeMZ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "门诊医保结算";
            this.Load += new System.EventHandler(this.frmYBChargeMZ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblZyh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldyxsdz.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJsxh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAcc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSub.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbJzlb.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblCancelUpload;
        private System.Windows.Forms.LinkLabel lblCancelCharge;
        internal System.Windows.Forms.Label lblSdywh;
        internal System.Windows.Forms.Label lblIsYBjs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel llblReadQRcode;
        internal System.Windows.Forms.RadioButton rdoEk;
        internal System.Windows.Forms.RadioButton rdoSk;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraEditors.ComboBoxEdit cmbJzlb;
        internal DevExpress.XtraEditors.MemoEdit txtReason;
        internal DevExpress.XtraEditors.TextEdit lblSub;
        internal DevExpress.XtraEditors.TextEdit lblAcc;
        internal DevExpress.XtraEditors.TextEdit lblJsxh;
        internal DevExpress.XtraEditors.TextEdit lblTotal;
        internal DevExpress.XtraEditors.TextEdit lbldyxsdz;
        internal DevExpress.XtraEditors.TextEdit lblName;
        internal DevExpress.XtraEditors.TextEdit lblZyh;
        internal DevExpress.XtraEditors.SimpleButton btnClose;
        internal DevExpress.XtraEditors.SimpleButton btnOk;
        internal DevExpress.XtraEditors.SimpleButton btnCharge;
        internal DevExpress.XtraEditors.SimpleButton btnCheckPsw;
        internal DevExpress.XtraEditors.SimpleButton btnUpload;
    }
}