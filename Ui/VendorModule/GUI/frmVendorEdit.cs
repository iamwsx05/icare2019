using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.VendorManage
{
	/// <summary>
	/// frmVendorEdit 的摘要说明。
	/// </summary>
	internal class frmVendorEdit : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		#region 窗体代码
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		internal System.Windows.Forms.TextBox m_txtVendorName;
		internal System.Windows.Forms.TextBox m_txtContactor;
		internal System.Windows.Forms.ComboBox m_cboVendorType;
		internal System.Windows.Forms.ComboBox m_cboProductType;
		internal System.Windows.Forms.TextBox m_txtPyCode;
		internal System.Windows.Forms.TextBox m_txtWbCode;
		private System.Windows.Forms.Label labl3;
		internal System.Windows.Forms.TextBox m_txtContactorPhone;
		internal System.Windows.Forms.TextBox m_txtAddress;
		internal System.Windows.Forms.TextBox m_txtFax;
		internal System.Windows.Forms.TextBox m_txtPhone;
		internal System.Windows.Forms.TextBox m_txtEmail;
		internal PinkieControls.ButtonXP m_cmdClear;
		internal PinkieControls.ButtonXP m_cmdSave;
		internal PinkieControls.ButtonXP m_cmdClose;
		internal System.Windows.Forms.TextBox m_txtUSERCODE;
		internal System.Windows.Forms.TextBox m_txtVendorID;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public frmVendorEdit()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		public string strUSERCODE
		{			
			set
			{
				this.m_txtUSERCODE.Text = value;
			}
		}

		#region 清理
		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_txtUSERCODE = new System.Windows.Forms.TextBox();
			this.m_txtVendorID = new System.Windows.Forms.TextBox();
			this.m_txtEmail = new System.Windows.Forms.TextBox();
			this.m_txtPhone = new System.Windows.Forms.TextBox();
			this.m_txtFax = new System.Windows.Forms.TextBox();
			this.m_txtAddress = new System.Windows.Forms.TextBox();
			this.m_txtContactorPhone = new System.Windows.Forms.TextBox();
			this.m_txtWbCode = new System.Windows.Forms.TextBox();
			this.m_txtPyCode = new System.Windows.Forms.TextBox();
			this.m_cboProductType = new System.Windows.Forms.ComboBox();
			this.m_cboVendorType = new System.Windows.Forms.ComboBox();
			this.m_txtContactor = new System.Windows.Forms.TextBox();
			this.m_txtVendorName = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labl3 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.m_cmdClear = new PinkieControls.ButtonXP();
			this.m_cmdSave = new PinkieControls.ButtonXP();
			this.m_cmdClose = new PinkieControls.ButtonXP();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_txtUSERCODE);
			this.groupBox1.Controls.Add(this.m_txtVendorID);
			this.groupBox1.Controls.Add(this.m_txtEmail);
			this.groupBox1.Controls.Add(this.m_txtPhone);
			this.groupBox1.Controls.Add(this.m_txtFax);
			this.groupBox1.Controls.Add(this.m_txtAddress);
			this.groupBox1.Controls.Add(this.m_txtContactorPhone);
			this.groupBox1.Controls.Add(this.m_txtWbCode);
			this.groupBox1.Controls.Add(this.m_txtPyCode);
			this.groupBox1.Controls.Add(this.m_cboProductType);
			this.groupBox1.Controls.Add(this.m_cboVendorType);
			this.groupBox1.Controls.Add(this.m_txtContactor);
			this.groupBox1.Controls.Add(this.m_txtVendorName);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.labl3);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(576, 312);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
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
			this.m_txtUSERCODE.Location = new System.Drawing.Point(96, 24);
			this.m_txtUSERCODE.MaxLength = 10;
			this.m_txtUSERCODE.Name = "m_txtUSERCODE";
			this.m_txtUSERCODE.TabIndex = 1;
			this.m_txtUSERCODE.Text = "";
			this.m_txtUSERCODE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
			// 
			// m_txtVendorID
			// 
			this.m_txtVendorID.Location = new System.Drawing.Point(160, 24);
			this.m_txtVendorID.Name = "m_txtVendorID";
			this.m_txtVendorID.TabIndex = 13;
			this.m_txtVendorID.Text = "";
			this.m_txtVendorID.Visible = false;
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
			this.m_txtEmail.Location = new System.Drawing.Point(96, 276);
			this.m_txtEmail.MaxLength = 100;
			this.m_txtEmail.Name = "m_txtEmail";
			this.m_txtEmail.Size = new System.Drawing.Size(432, 23);
			this.m_txtEmail.TabIndex = 12;
			this.m_txtEmail.Text = "";
			this.m_txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtPhone.Location = new System.Drawing.Point(96, 234);
			this.m_txtPhone.MaxLength = 18;
			this.m_txtPhone.Name = "m_txtPhone";
			this.m_txtPhone.Size = new System.Drawing.Size(160, 23);
			this.m_txtPhone.TabIndex = 10;
			this.m_txtPhone.Text = "";
			this.m_txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtFax.Location = new System.Drawing.Point(368, 234);
			this.m_txtFax.MaxLength = 18;
			this.m_txtFax.Name = "m_txtFax";
			this.m_txtFax.Size = new System.Drawing.Size(160, 23);
			this.m_txtFax.TabIndex = 11;
			this.m_txtFax.Text = "";
			this.m_txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtAddress.Location = new System.Drawing.Point(96, 192);
			this.m_txtAddress.MaxLength = 50;
			this.m_txtAddress.Name = "m_txtAddress";
			this.m_txtAddress.Size = new System.Drawing.Size(432, 23);
			this.m_txtAddress.TabIndex = 9;
			this.m_txtAddress.Text = "";
			this.m_txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtContactorPhone.Location = new System.Drawing.Point(368, 150);
			this.m_txtContactorPhone.MaxLength = 18;
			this.m_txtContactorPhone.Name = "m_txtContactorPhone";
			this.m_txtContactorPhone.Size = new System.Drawing.Size(160, 23);
			this.m_txtContactorPhone.TabIndex = 8;
			this.m_txtContactorPhone.Text = "";
			this.m_txtContactorPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtWbCode.Location = new System.Drawing.Point(368, 108);
			this.m_txtWbCode.MaxLength = 10;
			this.m_txtWbCode.Name = "m_txtWbCode";
			this.m_txtWbCode.Size = new System.Drawing.Size(160, 23);
			this.m_txtWbCode.TabIndex = 6;
			this.m_txtWbCode.Text = "";
			this.m_txtWbCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
			this.m_txtWbCode.TextChanged += new System.EventHandler(this.m_txtWbCode_TextChanged);
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
			this.m_txtPyCode.Location = new System.Drawing.Point(96, 108);
			this.m_txtPyCode.MaxLength = 10;
			this.m_txtPyCode.Name = "m_txtPyCode";
			this.m_txtPyCode.Size = new System.Drawing.Size(160, 23);
			this.m_txtPyCode.TabIndex = 5;
			this.m_txtPyCode.Text = "";
			this.m_txtPyCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
			this.m_txtPyCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtPyCode_KeyPress);
			// 
			// m_cboProductType
			// 
			this.m_cboProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboProductType.Items.AddRange(new object[] {
																  "药品服务商",
																  "材料服务商",
																  "设备服务商"});
			this.m_cboProductType.Location = new System.Drawing.Point(368, 66);
			this.m_cboProductType.Name = "m_cboProductType";
			this.m_cboProductType.Size = new System.Drawing.Size(160, 22);
			this.m_cboProductType.TabIndex = 4;
			this.m_cboProductType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
			// 
			// m_cboVendorType
			// 
			this.m_cboVendorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboVendorType.Items.AddRange(new object[] {
																 "药品供应商",
																 "药品生产商",
																 "两者都是"});
			this.m_cboVendorType.Location = new System.Drawing.Point(368, 24);
			this.m_cboVendorType.Name = "m_cboVendorType";
			this.m_cboVendorType.Size = new System.Drawing.Size(160, 22);
			this.m_cboVendorType.TabIndex = 2;
			this.m_cboVendorType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtContactor.Location = new System.Drawing.Point(96, 150);
			this.m_txtContactor.MaxLength = 18;
			this.m_txtContactor.Name = "m_txtContactor";
			this.m_txtContactor.Size = new System.Drawing.Size(160, 23);
			this.m_txtContactor.TabIndex = 7;
			this.m_txtContactor.Text = "";
			this.m_txtContactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthText_KeyDown);
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
			this.m_txtVendorName.Location = new System.Drawing.Point(96, 66);
			this.m_txtVendorName.MaxLength = 50;
			this.m_txtVendorName.Name = "m_txtVendorName";
			this.m_txtVendorName.Size = new System.Drawing.Size(160, 23);
			this.m_txtVendorName.TabIndex = 3;
			this.m_txtVendorName.Text = "";
			this.m_txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtVendorName_KeyDown);
			this.m_txtVendorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtVendorName_KeyPress);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 110);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(80, 18);
			this.label12.TabIndex = 11;
			this.label12.Text = "五  笔  码";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(288, 110);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 18);
			this.label11.TabIndex = 10;
			this.label11.Text = "拼    音    码";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(288, 236);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 20);
			this.label10.TabIndex = 9;
			this.label10.Text = "传    真";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 278);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 18);
			this.label9.TabIndex = 8;
			this.label9.Text = "电 子 邮件";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labl3
			// 
			this.labl3.Location = new System.Drawing.Point(280, 152);
			this.labl3.Name = "labl3";
			this.labl3.Size = new System.Drawing.Size(80, 24);
			this.labl3.TabIndex = 7;
			this.labl3.Text = "联系人电话";
			this.labl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 152);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 24);
			this.label7.TabIndex = 6;
			this.label7.Text = "联  系  人";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 236);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 20);
			this.label6.TabIndex = 5;
			this.label6.Text = "联 系 电话";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 194);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 22);
			this.label5.TabIndex = 4;
			this.label5.Text = "联 系 地址";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(288, 67);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 21);
			this.label4.TabIndex = 3;
			this.label4.Text = "产品类型";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(288, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "类    别";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 21);
			this.label2.TabIndex = 1;
			this.label2.Text = "供应商名称";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "助  记  码";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_cmdClear
			// 
			this.m_cmdClear.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdClear.DefaultScheme = true;
			this.m_cmdClear.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdClear.Hint = "";
			this.m_cmdClear.Location = new System.Drawing.Point(56, 336);
			this.m_cmdClear.Name = "m_cmdClear";
			this.m_cmdClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdClear.Size = new System.Drawing.Size(104, 35);
			this.m_cmdClear.TabIndex = 2;
			this.m_cmdClear.Text = "清除(&C)";
			this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
			// 
			// m_cmdSave
			// 
			this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdSave.DefaultScheme = true;
			this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdSave.Hint = "";
			this.m_cmdSave.Location = new System.Drawing.Point(216, 336);
			this.m_cmdSave.Name = "m_cmdSave";
			this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdSave.Size = new System.Drawing.Size(104, 35);
			this.m_cmdSave.TabIndex = 1;
			this.m_cmdSave.Text = "保存(&S)";
			this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
			// 
			// m_cmdClose
			// 
			this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdClose.DefaultScheme = true;
			this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdClose.Hint = "";
			this.m_cmdClose.Location = new System.Drawing.Point(384, 336);
			this.m_cmdClose.Name = "m_cmdClose";
			this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdClose.Size = new System.Drawing.Size(104, 35);
			this.m_cmdClose.TabIndex = 3;
			this.m_cmdClose.Text = "退出(&E)";
			this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
			// 
			// frmVendorEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(592, 405);
			this.Controls.Add(this.m_cmdClose);
			this.Controls.Add(this.m_cmdSave);
			this.Controls.Add(this.m_cmdClear);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmVendorEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmVendorEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		public override void CreateController()
		{
			// TODO:  添加 frmVendorEdit.CreateController 实现
            this.objController = new com.digitalwave.iCare.gui.VendorManage.clsControlVendorEdit();
			this.objController.Set_GUI_Apperance(this);
		}

		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			((clsControlVendorEdit)this.objController).m_mthDoClear();
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			((clsControlVendorEdit)this.objController).m_mthDoSave();
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmVendorEdit_Load(object sender, System.EventArgs e)
		{
			base.m_mthSetFormControlCanBeNull(this);
		}

		#region 导入数据  欧阳孔伟  2004-06-05
		/// <summary>
		/// 导入数据
		/// </summary>
		/// <param name="p_objItem"></param>
		public void m_mthSetVendorInfo( clsVendor_VO p_objItem,clsControlVendor p_objControl)
		{
			((clsControlVendorEdit)this.objController).m_mthSetVendorInfo(p_objItem,p_objControl);
		}
		#endregion

		private void m_txtWbCode_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void m_cmdGetID_Click(object sender, System.EventArgs e)
		{
		
		}

		private void m_txtVendorName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{

		}

		private void m_txtVendorName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				((clsControlVendorEdit)this.objController).m_lngGetpywb();
				System.Windows.Forms.SendKeys.Send("{TAB}");
			}

		}

		private void m_txtPyCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		
		}
		private void m_mthText_KeyDown(object sender,System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				System.Windows.Forms.SendKeys.Send("{TAB}");
			}
		}
	}
}
