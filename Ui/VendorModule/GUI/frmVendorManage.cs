using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.GUI_Base;//GUI_Base.dll

namespace com.digitalwave.iCare.gui.VendorManage
{
	/// <summary>
	/// frmVendorManage 的摘要说明。
	/// kong 2004-05-11
	/// </summary>
	public class frmVendorManage : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		#region 窗体代码
		internal System.Windows.Forms.ColumnHeader clmVendorID;
		internal System.Windows.Forms.ColumnHeader clmVendorName;
		internal System.Windows.Forms.ColumnHeader clmVendorType;
		internal System.Windows.Forms.ColumnHeader clmProdcutType;
		internal System.Windows.Forms.ColumnHeader clmAddress;
		internal System.Windows.Forms.ColumnHeader clmPhone;
		internal System.Windows.Forms.ColumnHeader clmContactor;
		internal System.Windows.Forms.ColumnHeader clmContactorPhone;
		internal System.Windows.Forms.ColumnHeader clmEmail;
		internal System.Windows.Forms.ColumnHeader clmFax;
		internal System.Windows.Forms.ColumnHeader clmPYCode;
		internal System.Windows.Forms.ColumnHeader clmWBCode;
		internal System.Windows.Forms.ListView m_lsvVendorList;
		internal PinkieControls.ButtonXP buttonXP2;
		internal PinkieControls.ButtonXP buttonXP3;
		internal PinkieControls.ButtonXP buttonXP4;
		internal PinkieControls.ButtonXP buttonXP5;
		internal PinkieControls.ButtonXP buttonXP6;
		internal PinkieControls.ButtonXP buttonXP7;
		internal System.Windows.Forms.PrintPreviewDialog printPreviewDlg;
		internal System.Drawing.Printing.PrintDocument printDoc;
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.TextBox m_txtUSERCODE;
		internal System.Windows.Forms.TextBox m_txtVendorID;
		internal System.Windows.Forms.TextBox m_txtWbCode;
		internal System.Windows.Forms.TextBox m_txtPyCode;
		internal System.Windows.Forms.TextBox m_txtVendorName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox m_txtContactorPhone;
		internal System.Windows.Forms.TextBox m_txtContactor;
		private System.Windows.Forms.Label labl3;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.TextBox m_txtEmail;
		internal System.Windows.Forms.TextBox m_txtPhone;
		internal System.Windows.Forms.TextBox m_txtFax;
		internal System.Windows.Forms.TextBox m_txtAddress;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
        internal PinkieControls.ButtonXP m_cmdSave;
        internal ComboBox m_cbxType;
        internal System.Windows.Forms.TextBox m_txtAliasName;
        private Label label4;
        private Label label3;
        internal ColumnHeader clmAlias;
        internal PinkieControls.ButtonXP m_btnQuery;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region 构造函数
		public frmVendorManage()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
	

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendorManage));
            this.printPreviewDlg = new System.Windows.Forms.PrintPreviewDialog();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cbxType = new System.Windows.Forms.ComboBox();
            this.m_txtUSERCODE = new System.Windows.Forms.TextBox();
            this.m_txtAliasName = new System.Windows.Forms.TextBox();
            this.m_txtVendorName = new System.Windows.Forms.TextBox();
            this.m_txtWbCode = new System.Windows.Forms.TextBox();
            this.m_txtPyCode = new System.Windows.Forms.TextBox();
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
            this.m_txtVendorID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonXP7 = new PinkieControls.ButtonXP();
            this.buttonXP6 = new PinkieControls.ButtonXP();
            this.buttonXP5 = new PinkieControls.ButtonXP();
            this.buttonXP4 = new PinkieControls.ButtonXP();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.m_lsvVendorList = new System.Windows.Forms.ListView();
            this.clmVendorID = new System.Windows.Forms.ColumnHeader();
            this.clmVendorName = new System.Windows.Forms.ColumnHeader();
            this.clmAlias = new System.Windows.Forms.ColumnHeader();
            this.clmVendorType = new System.Windows.Forms.ColumnHeader();
            this.clmProdcutType = new System.Windows.Forms.ColumnHeader();
            this.clmAddress = new System.Windows.Forms.ColumnHeader();
            this.clmPhone = new System.Windows.Forms.ColumnHeader();
            this.clmContactor = new System.Windows.Forms.ColumnHeader();
            this.clmContactorPhone = new System.Windows.Forms.ColumnHeader();
            this.clmEmail = new System.Windows.Forms.ColumnHeader();
            this.clmFax = new System.Windows.Forms.ColumnHeader();
            this.clmPYCode = new System.Windows.Forms.ColumnHeader();
            this.clmWBCode = new System.Windows.Forms.ColumnHeader();
            this.m_btnQuery = new PinkieControls.ButtonXP();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewDlg
            // 
            this.printPreviewDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDlg.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDlg.Enabled = true;
            this.printPreviewDlg.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDlg.Icon")));
            this.printPreviewDlg.Name = "printPreviewDlg";
            this.printPreviewDlg.Visible = false;
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(260, 506);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(104, 35);
            this.m_cmdSave.TabIndex = 1;
            this.m_cmdSave.Text = "保存(&S)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_cbxType);
            this.panel1.Controls.Add(this.m_txtUSERCODE);
            this.panel1.Controls.Add(this.m_txtAliasName);
            this.panel1.Controls.Add(this.m_txtVendorName);
            this.panel1.Controls.Add(this.m_txtWbCode);
            this.panel1.Controls.Add(this.m_txtPyCode);
            this.panel1.Controls.Add(this.m_txtEmail);
            this.panel1.Controls.Add(this.m_txtPhone);
            this.panel1.Controls.Add(this.m_txtFax);
            this.panel1.Controls.Add(this.m_txtAddress);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_txtContactorPhone);
            this.panel1.Controls.Add(this.m_txtContactor);
            this.panel1.Controls.Add(this.labl3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.m_txtVendorID);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1019, 103);
            this.panel1.TabIndex = 0;
            // 
            // m_cbxType
            // 
            this.m_cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxType.FormattingEnabled = true;
            this.m_cbxType.Items.AddRange(new object[] {
            "供应商",
            "生产厂家",
            "两者都是"});
            this.m_cbxType.Location = new System.Drawing.Point(331, 69);
            this.m_cbxType.Name = "m_cbxType";
            this.m_cbxType.Size = new System.Drawing.Size(213, 22);
            this.m_cbxType.TabIndex = 6;
            this.m_cbxType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cbxType_KeyDown);
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
            this.m_txtUSERCODE.Location = new System.Drawing.Point(74, 6);
            this.m_txtUSERCODE.MaxLength = 5;
            this.m_txtUSERCODE.Name = "m_txtUSERCODE";
            this.m_txtUSERCODE.Size = new System.Drawing.Size(169, 23);
            this.m_txtUSERCODE.TabIndex = 1;
            this.m_txtUSERCODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtUSERCODE_KeyDown);
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
            this.m_txtAliasName.Location = new System.Drawing.Point(74, 69);
            this.m_txtAliasName.MaxLength = 50;
            this.m_txtAliasName.Name = "m_txtAliasName";
            this.m_txtAliasName.Size = new System.Drawing.Size(169, 23);
            this.m_txtAliasName.TabIndex = 3;
            this.m_txtAliasName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtVendorName_KeyDown);
            this.m_txtAliasName.Leave += new System.EventHandler(this.m_txtVendorName_Leave);
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
            this.m_txtVendorName.Location = new System.Drawing.Point(74, 38);
            this.m_txtVendorName.MaxLength = 50;
            this.m_txtVendorName.Name = "m_txtVendorName";
            this.m_txtVendorName.Size = new System.Drawing.Size(169, 23);
            this.m_txtVendorName.TabIndex = 2;
            this.m_txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtVendorName_KeyDown);
            this.m_txtVendorName.Leave += new System.EventHandler(this.m_txtVendorName_Leave);
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
            this.m_txtWbCode.Location = new System.Drawing.Point(841, 69);
            this.m_txtWbCode.MaxLength = 10;
            this.m_txtWbCode.Name = "m_txtWbCode";
            this.m_txtWbCode.Size = new System.Drawing.Size(159, 23);
            this.m_txtWbCode.TabIndex = 12;
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
            this.m_txtPyCode.Location = new System.Drawing.Point(616, 69);
            this.m_txtPyCode.MaxLength = 10;
            this.m_txtPyCode.Name = "m_txtPyCode";
            this.m_txtPyCode.Size = new System.Drawing.Size(159, 23);
            this.m_txtPyCode.TabIndex = 11;
            this.m_txtPyCode.TabStop = false;
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
            this.m_txtEmail.Location = new System.Drawing.Point(784, 38);
            this.m_txtEmail.MaxLength = 100;
            this.m_txtEmail.Name = "m_txtEmail";
            this.m_txtEmail.Size = new System.Drawing.Size(216, 23);
            this.m_txtEmail.TabIndex = 10;
            this.m_txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtEmail_KeyDown);
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
            this.m_txtPhone.Location = new System.Drawing.Point(616, 6);
            this.m_txtPhone.MaxLength = 18;
            this.m_txtPhone.Name = "m_txtPhone";
            this.m_txtPhone.Size = new System.Drawing.Size(96, 23);
            this.m_txtPhone.TabIndex = 7;
            this.m_txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtPhone_KeyDown);
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
            this.m_txtFax.Location = new System.Drawing.Point(616, 38);
            this.m_txtFax.MaxLength = 18;
            this.m_txtFax.Name = "m_txtFax";
            this.m_txtFax.Size = new System.Drawing.Size(96, 23);
            this.m_txtFax.TabIndex = 8;
            this.m_txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtFax_KeyDown);
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
            this.m_txtAddress.Location = new System.Drawing.Point(784, 6);
            this.m_txtAddress.MaxLength = 50;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.Size = new System.Drawing.Size(216, 23);
            this.m_txtAddress.TabIndex = 9;
            this.m_txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtAddress_KeyDown);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(544, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 20);
            this.label10.TabIndex = 34;
            this.label10.Text = "传    真";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(720, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 18);
            this.label9.TabIndex = 33;
            this.label9.Text = "电子邮件";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(544, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 32;
            this.label6.Text = "联系电话";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(720, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 22);
            this.label5.TabIndex = 31;
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
            this.m_txtContactorPhone.Location = new System.Drawing.Point(331, 38);
            this.m_txtContactorPhone.MaxLength = 18;
            this.m_txtContactorPhone.Name = "m_txtContactorPhone";
            this.m_txtContactorPhone.Size = new System.Drawing.Size(213, 23);
            this.m_txtContactorPhone.TabIndex = 5;
            this.m_txtContactorPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtContactorPhone_KeyDown);
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
            this.m_txtContactor.Location = new System.Drawing.Point(331, 6);
            this.m_txtContactor.MaxLength = 18;
            this.m_txtContactor.Name = "m_txtContactor";
            this.m_txtContactor.Size = new System.Drawing.Size(213, 23);
            this.m_txtContactor.TabIndex = 4;
            this.m_txtContactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtContactor_KeyDown);
            // 
            // labl3
            // 
            this.labl3.Location = new System.Drawing.Point(249, 38);
            this.labl3.Name = "labl3";
            this.labl3.Size = new System.Drawing.Size(80, 24);
            this.labl3.TabIndex = 28;
            this.labl3.Text = "联系人电话";
            this.labl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(249, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 24);
            this.label7.TabIndex = 27;
            this.label7.Text = "联  系  人";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtVendorID
            // 
            this.m_txtVendorID.Location = new System.Drawing.Point(152, 8);
            this.m_txtVendorID.Name = "m_txtVendorID";
            this.m_txtVendorID.Size = new System.Drawing.Size(80, 23);
            this.m_txtVendorID.TabIndex = 26;
            this.m_txtVendorID.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(549, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 14);
            this.label12.TabIndex = 25;
            this.label12.Text = "五 笔 码";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(781, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 24;
            this.label11.Text = "拼 音 码";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 15;
            this.label4.Text = "类      型";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "单位简称";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "单位名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "单位编码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonXP7
            // 
            this.buttonXP7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonXP7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP7.DefaultScheme = true;
            this.buttonXP7.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP7.Hint = "";
            this.buttonXP7.Location = new System.Drawing.Point(843, 507);
            this.buttonXP7.Name = "buttonXP7";
            this.buttonXP7.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP7.Size = new System.Drawing.Size(96, 32);
            this.buttonXP7.TabIndex = 6;
            this.buttonXP7.Text = "关闭(&C)";
            this.buttonXP7.Click += new System.EventHandler(this.buttonXP7_Click);
            // 
            // buttonXP6
            // 
            this.buttonXP6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonXP6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP6.DefaultScheme = true;
            this.buttonXP6.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP6.Hint = "";
            this.buttonXP6.Location = new System.Drawing.Point(613, 507);
            this.buttonXP6.Name = "buttonXP6";
            this.buttonXP6.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP6.Size = new System.Drawing.Size(96, 32);
            this.buttonXP6.TabIndex = 4;
            this.buttonXP6.Text = "打印(&P)";
            this.buttonXP6.Click += new System.EventHandler(this.buttonXP6_Click);
            // 
            // buttonXP5
            // 
            this.buttonXP5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonXP5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP5.DefaultScheme = true;
            this.buttonXP5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP5.Hint = "";
            this.buttonXP5.Location = new System.Drawing.Point(498, 507);
            this.buttonXP5.Name = "buttonXP5";
            this.buttonXP5.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP5.Size = new System.Drawing.Size(96, 32);
            this.buttonXP5.TabIndex = 3;
            this.buttonXP5.Text = "预览(&B)";
            this.buttonXP5.Click += new System.EventHandler(this.buttonXP5_Click);
            // 
            // buttonXP4
            // 
            this.buttonXP4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonXP4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP4.DefaultScheme = true;
            this.buttonXP4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP4.Hint = "";
            this.buttonXP4.Location = new System.Drawing.Point(383, 507);
            this.buttonXP4.Name = "buttonXP4";
            this.buttonXP4.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP4.Size = new System.Drawing.Size(96, 32);
            this.buttonXP4.TabIndex = 2;
            this.buttonXP4.Text = "刷新(&R)";
            this.buttonXP4.Click += new System.EventHandler(this.buttonXP4_Click);
            // 
            // buttonXP3
            // 
            this.buttonXP3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(928, 510);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(96, 32);
            this.buttonXP3.TabIndex = 6;
            this.buttonXP3.Text = "删除(&D)";
            this.buttonXP3.Visible = false;
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(145, 507);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(96, 32);
            this.buttonXP2.TabIndex = 0;
            this.buttonXP2.Text = "新建(&N)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // m_lsvVendorList
            // 
            this.m_lsvVendorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvVendorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmVendorID,
            this.clmVendorName,
            this.clmAlias,
            this.clmVendorType,
            this.clmProdcutType,
            this.clmAddress,
            this.clmPhone,
            this.clmContactor,
            this.clmContactorPhone,
            this.clmEmail,
            this.clmFax,
            this.clmPYCode,
            this.clmWBCode});
            this.m_lsvVendorList.FullRowSelect = true;
            this.m_lsvVendorList.GridLines = true;
            this.m_lsvVendorList.Location = new System.Drawing.Point(5, 0);
            this.m_lsvVendorList.Name = "m_lsvVendorList";
            this.m_lsvVendorList.Size = new System.Drawing.Size(1019, 387);
            this.m_lsvVendorList.TabIndex = 100;
            this.m_lsvVendorList.UseCompatibleStateImageBehavior = false;
            this.m_lsvVendorList.View = System.Windows.Forms.View.Details;
            this.m_lsvVendorList.DoubleClick += new System.EventHandler(this.m_lsvVendorList_DoubleClick);
            // 
            // clmVendorID
            // 
            this.clmVendorID.Text = "编码";
            // 
            // clmVendorName
            // 
            this.clmVendorName.Text = "名称";
            this.clmVendorName.Width = 180;
            // 
            // clmAlias
            // 
            this.clmAlias.Text = "简称";
            this.clmAlias.Width = 120;
            // 
            // clmVendorType
            // 
            this.clmVendorType.Text = "类型";
            this.clmVendorType.Width = 100;
            // 
            // clmProdcutType
            // 
            this.clmProdcutType.Text = "产品类型";
            this.clmProdcutType.Width = 0;
            // 
            // clmAddress
            // 
            this.clmAddress.Text = "联系地址";
            this.clmAddress.Width = 200;
            // 
            // clmPhone
            // 
            this.clmPhone.Text = "联系电话";
            this.clmPhone.Width = 100;
            // 
            // clmContactor
            // 
            this.clmContactor.Text = "联系人";
            this.clmContactor.Width = 100;
            // 
            // clmContactorPhone
            // 
            this.clmContactorPhone.Text = "联系人电话";
            this.clmContactorPhone.Width = 100;
            // 
            // clmEmail
            // 
            this.clmEmail.Text = "电子邮件";
            this.clmEmail.Width = 100;
            // 
            // clmFax
            // 
            this.clmFax.Text = "传真";
            this.clmFax.Width = 100;
            // 
            // clmPYCode
            // 
            this.clmPYCode.Text = "拼音码";
            // 
            // clmWBCode
            // 
            this.clmWBCode.Text = "五笔码";
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnQuery.DefaultScheme = true;
            this.m_btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnQuery.Hint = "";
            this.m_btnQuery.Location = new System.Drawing.Point(728, 507);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnQuery.Size = new System.Drawing.Size(96, 32);
            this.m_btnQuery.TabIndex = 5;
            this.m_btnQuery.Text = "查询(&Q)";
            this.m_btnQuery.Click += new System.EventHandler(this.m_btnQuery_Click);
            // 
            // frmVendorManage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1028, 549);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonXP7);
            this.Controls.Add(this.buttonXP6);
            this.Controls.Add(this.buttonXP5);
            this.Controls.Add(this.buttonXP4);
            this.Controls.Add(this.buttonXP3);
            this.Controls.Add(this.m_btnQuery);
            this.Controls.Add(this.buttonXP2);
            this.Controls.Add(this.m_lsvVendorList);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVendorManage";
            this.Text = "供应商管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVendorManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		
		#region 调用Control方法  欧阳孔伟  2004-06-05
		#region  新增
		/// <summary>
		/// 新增
		/// </summary>
		private void New()
		{
			((clsControlVendor)this.objController).m_mthDoAddNew();
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除记录
		/// </summary>
		private void Delete()
		{
			((clsControlVendor)this.objController).m_mthDoDelete();
		}
		#endregion

		#region 刷新
		/// <summary>
		/// 刷新
		/// </summary>
		private void Refersh()
		{
			((clsControlVendor)this.objController).m_mthRefersh();
		}
		#endregion

		#region 打印预览
		/// <summary>
		/// 打印预览
		/// </summary>
		private void PreView()
		{
			((clsControlVendor)this.objController).m_mthPreView();
		}
		#endregion

		#region 打印 
		/// <summary>
		/// 打印
		/// </summary>
		private void Print()
		{
			((clsControlVendor)this.objController).m_mthPrint();
		}
		#endregion

		#region 关闭窗口
		/// <summary>
		/// 关闭窗口
		/// </summary>
		private void CloseWin()
		{
			this.Close();
		}
		#endregion

		#endregion

		public override void CreateController()
		{
			// TODO:  添加 frmVendorManage.CreateController 实现
            this.objController = new com.digitalwave.iCare.gui.VendorManage.clsControlVendor();
			this.objController.Set_GUI_Apperance(this);
		}

		private void m_lsvVendorList_DoubleClick(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_mthVendorListDoubleClick();
		}

		private void frmVendorManage_Load(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_mthGetVendorList();
			((clsControlVendor)this.objController).m_mthGetReSetHelpCode();
			this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] {m_txtPyCode,m_txtWbCode});
            m_cbxType.SelectedIndex = 2;
		}

        public void m_mthShow(string strID)
        {
            switch (strID)
            {
                case "1":
                    this.Text = "药品供应商及生产厂家";
                    this.Tag = 1;
                    this.Show();
                    break;
                case "2":
                    this.Text = "材料供应商及生产厂家";
                    this.Tag = 2;
                    this.Show();
                    break;
                case "3":
                    this.Text = "设备供应商及生产厂家";
                    this.Tag = 3;
                    this.Show();
                    break;
            }
        }


		public void m_ShowMe(string strID)
		{
			switch(strID)
			{
				case "1":
					this.Text="药品供应商";
					this.Tag=1;
					this.Show();
					break;
				case "2":
					this.Text="材料供应商";
					this.Tag=2;
					this.Show();
					break;
				case "3":
					this.Text="设备供应商";
					this.Tag=3;
					this.Show();
					break;
			}
		}

        /// <summary>
        /// 供应商类别 1-供应商 2-生产厂家 3-既是供应商也是生产厂家
        /// </summary>
        internal int m_intVendorType = 1;

        /// <summary>
        /// 显示生产厂家
        /// </summary>
        /// <param name="strID"></param>
        public void ShowManufacturer(string strID)
        {
            m_intVendorType = 2;
            label1.Text = "生产厂家编码";
            label2.Text = "生产厂家名称";

            switch (strID)
            {
                case "1":
                    this.Text = "药品生产厂家";
                    this.Tag = 1;
                    this.Show();
                    break;
                case "2":
                    this.Text = "材料生产厂家";
                    this.Tag = 2;
                    this.Show();
                    break;
                case "3":
                    this.Text = "设备生产厂家";
                    this.Tag = 3;
                    this.Show();
                    break;
            }
        }

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			if(m_txtVendorName.Text.Trim()=="")
			{
				MessageBox.Show("请输入供应商名称！","Icare",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				m_txtUSERCODE.Focus();
				return;
			}
			if(m_cmdSave.Text=="保存(&S)")
			  ((clsControlVendor)this.objController).m_mthSave();
			else
			  ((clsControlVendor)this.objController).m_mthDoModify();
			m_txtVendorName.Focus();

		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_mthDoClear();
			m_txtVendorName.Focus();
		}

		private void buttonXP5_Click(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_mthPreView();
		}

		private void buttonXP7_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonXP6_Click(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_mthPrint();
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_mthDoDelete();
		}

		private void m_txtUSERCODE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				m_txtVendorName.Focus();
			}
		}

		private void m_txtVendorName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtContactor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtContactorPhone_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtPhone_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtFax_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtAddress_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
		}

		private void m_txtEmail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.m_mthSetKeyTab(e);
            if (e.KeyCode == Keys.Enter)
                m_cmdSave.Focus();
		}

		private void m_txtVendorName_Leave(object sender, System.EventArgs e)
		{
			((clsControlVendor)this.objController).m_lngGetpywb();
		}

        private void m_cbxType_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void buttonXP4_Click(object sender, EventArgs e)
        {
            Refersh();
        }

        private void m_btnQuery_Click(object sender, EventArgs e)
        {
            ((clsControlVendor)this.objController).m_mthQuery();            
        }
	}
}
