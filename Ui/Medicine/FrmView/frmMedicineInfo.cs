using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.iCare.middletier.HIS;
using weCare.Core.Entity;
//using System.EnterpriseServices;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 药品基本信息维护 Create by Sam 2004-5-24
	/// </summary>
	public class frmMedicineInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label m_lbName;
		private System.Windows.Forms.Label m_lbNo;
		internal System.Windows.Forms.TextBox m_txtSpec;
		internal System.Windows.Forms.TextBox m_txtWB;
		internal System.Windows.Forms.TextBox m_txtName;
		internal System.Windows.Forms.TextBox m_txtNo;
		internal System.Windows.Forms.TextBox m_txtPY;
		private System.ComponentModel.IContainer components;
		private PinkieControls.ButtonXP m_btClear;
		private PinkieControls.ButtonXP m_btSave;
		internal string MedID=null;
		internal clsMedicine_VO clsMedVO=null;
		private PinkieControls.ButtonXP m_btnExit;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox m_txtEnName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.CheckBox m_chkIsSelfPay;
		internal System.Windows.Forms.CheckBox m_chkIsImport;
		internal System.Windows.Forms.CheckBox m_chkIsSelf;
		internal System.Windows.Forms.CheckBox m_chkIsCostly;
		internal System.Windows.Forms.CheckBox m_chkIsChlorpromazine;
		internal System.Windows.Forms.CheckBox m_chkIsAnaesthesia;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		public System.Windows.Forms.ComboBox m_CobUnit;
		private System.Windows.Forms.Label label12;
		internal System.Windows.Forms.TextBox m_txtDosage;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		internal System.Windows.Forms.TextBox m_txtTRADEPRICE;
		internal System.Windows.Forms.ComboBox m_cboProduct;
		internal System.Windows.Forms.TextBox m_txtPackQty;
		internal System.Windows.Forms.TextBox m_txtUNITPRICE;
		internal System.Windows.Forms.ComboBox m_cboMedType;
		internal System.Windows.Forms.ComboBox m_cboPreType;
		internal System.Windows.Forms.ComboBox m_CobDosageUnit;
		internal System.Windows.Forms.ComboBox m_CobIpUnit;
		private com.digitalwave.iCare.gui.HIS.clsControlMedicine clsParent;

		public frmMedicineInfo()
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.m_txtPY = new System.Windows.Forms.TextBox();
			this.m_txtSpec = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.m_lbName = new System.Windows.Forms.Label();
			this.m_lbNo = new System.Windows.Forms.Label();
			this.m_txtWB = new System.Windows.Forms.TextBox();
			this.m_txtName = new System.Windows.Forms.TextBox();
			this.m_txtNo = new System.Windows.Forms.TextBox();
			this.m_btClear = new PinkieControls.ButtonXP();
			this.m_btSave = new PinkieControls.ButtonXP();
			this.m_btnExit = new PinkieControls.ButtonXP();
			this.label2 = new System.Windows.Forms.Label();
			this.m_txtEnName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_chkIsAnaesthesia = new System.Windows.Forms.CheckBox();
			this.m_chkIsSelfPay = new System.Windows.Forms.CheckBox();
			this.m_chkIsImport = new System.Windows.Forms.CheckBox();
			this.m_chkIsSelf = new System.Windows.Forms.CheckBox();
			this.m_chkIsCostly = new System.Windows.Forms.CheckBox();
			this.m_chkIsChlorpromazine = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.m_CobUnit = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.m_txtDosage = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.m_txtTRADEPRICE = new System.Windows.Forms.TextBox();
			this.m_cboProduct = new System.Windows.Forms.ComboBox();
			this.m_txtPackQty = new System.Windows.Forms.TextBox();
			this.m_txtUNITPRICE = new System.Windows.Forms.TextBox();
			this.m_cboMedType = new System.Windows.Forms.ComboBox();
			this.m_cboPreType = new System.Windows.Forms.ComboBox();
			this.m_CobDosageUnit = new System.Windows.Forms.ComboBox();
			this.m_CobIpUnit = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// m_txtPY
			// 
			this.m_txtPY.Location = new System.Drawing.Point(312, 64);
			this.m_txtPY.MaxLength = 5;
			this.m_txtPY.Name = "m_txtPY";
			this.m_txtPY.Size = new System.Drawing.Size(136, 23);
			this.m_txtPY.TabIndex = 101;
			this.m_txtPY.Text = "";
			// 
			// m_txtSpec
			// 
			this.m_txtSpec.Location = new System.Drawing.Point(312, 104);
			this.m_txtSpec.MaxLength = 100;
			this.m_txtSpec.Name = "m_txtSpec";
			this.m_txtSpec.Size = new System.Drawing.Size(136, 23);
			this.m_txtSpec.TabIndex = 7;
			this.m_txtSpec.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(240, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(54, 17);
			this.label6.TabIndex = 47;
			this.label6.Text = "拼音码";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(240, 147);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 17);
			this.label5.TabIndex = 46;
			this.label5.Text = "剂型";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 17);
			this.label4.TabIndex = 44;
			this.label4.Text = "五笔码";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(-10, 147);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 17);
			this.label1.TabIndex = 43;
			this.label1.Text = "药品类型";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(240, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 17);
			this.label3.TabIndex = 42;
			this.label3.Text = "规格";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_lbName
			// 
			this.m_lbName.Location = new System.Drawing.Point(240, 25);
			this.m_lbName.Name = "m_lbName";
			this.m_lbName.Size = new System.Drawing.Size(54, 17);
			this.m_lbName.TabIndex = 41;
			this.m_lbName.Text = "名称";
			this.m_lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_lbNo
			// 
			this.m_lbNo.Location = new System.Drawing.Point(8, 25);
			this.m_lbNo.Name = "m_lbNo";
			this.m_lbNo.Size = new System.Drawing.Size(54, 17);
			this.m_lbNo.TabIndex = 40;
			this.m_lbNo.Text = "助记码";
			this.m_lbNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtWB
			// 
			this.m_txtWB.Location = new System.Drawing.Point(72, 64);
			this.m_txtWB.MaxLength = 5;
			this.m_txtWB.Name = "m_txtWB";
			this.m_txtWB.Size = new System.Drawing.Size(120, 23);
			this.m_txtWB.TabIndex = 100;
			this.m_txtWB.Text = "";
			// 
			// m_txtName
			// 
			this.m_txtName.Location = new System.Drawing.Point(312, 22);
			this.m_txtName.MaxLength = 200;
			this.m_txtName.Name = "m_txtName";
			this.m_txtName.Size = new System.Drawing.Size(136, 23);
			this.m_txtName.TabIndex = 2;
			this.m_txtName.Text = "";
			this.m_txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtName_KeyDown);
			this.m_txtName.TextChanged += new System.EventHandler(this.m_txtName_TextChanged);
			// 
			// m_txtNo
			// 
			this.m_txtNo.Location = new System.Drawing.Point(72, 22);
			this.m_txtNo.MaxLength = 10;
			this.m_txtNo.Name = "m_txtNo";
			this.m_txtNo.Size = new System.Drawing.Size(120, 23);
			this.m_txtNo.TabIndex = 0;
			this.m_txtNo.Text = "";
			this.m_txtNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtNo_KeyPress);
			this.m_txtNo.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtNo_Validating);
			// 
			// m_btClear
			// 
			this.m_btClear.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btClear.DefaultScheme = true;
			this.m_btClear.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btClear.Hint = "";
			this.m_btClear.Location = new System.Drawing.Point(40, 464);
			this.m_btClear.Name = "m_btClear";
			this.m_btClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btClear.Size = new System.Drawing.Size(96, 32);
			this.m_btClear.TabIndex = 25;
			this.m_btClear.Text = "清空(&C)";
			this.m_btClear.Click += new System.EventHandler(this.m_btClear_Click);
			// 
			// m_btSave
			// 
			this.m_btSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btSave.DefaultScheme = true;
			this.m_btSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btSave.Hint = "";
			this.m_btSave.Location = new System.Drawing.Point(168, 464);
			this.m_btSave.Name = "m_btSave";
			this.m_btSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btSave.Size = new System.Drawing.Size(96, 32);
			this.m_btSave.TabIndex = 24;
			this.m_btSave.Text = "保存(&S)";
			this.m_btSave.Click += new System.EventHandler(this.m_btSave_Click);
			// 
			// m_btnExit
			// 
			this.m_btnExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnExit.DefaultScheme = true;
			this.m_btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnExit.Hint = "";
			this.m_btnExit.Location = new System.Drawing.Point(296, 464);
			this.m_btnExit.Name = "m_btnExit";
			this.m_btnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnExit.Size = new System.Drawing.Size(96, 32);
			this.m_btnExit.TabIndex = 26;
			this.m_btnExit.Text = "退出(&E)";
			this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 64;
			this.label2.Text = "英文名";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtEnName
			// 
			this.m_txtEnName.Location = new System.Drawing.Point(72, 104);
			this.m_txtEnName.MaxLength = 20;
			this.m_txtEnName.Name = "m_txtEnName";
			this.m_txtEnName.Size = new System.Drawing.Size(120, 23);
			this.m_txtEnName.TabIndex = 6;
			this.m_txtEnName.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(232, 315);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 17);
			this.label7.TabIndex = 66;
			this.label7.Text = "生产厂家";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label7.Click += new System.EventHandler(this.label7_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_chkIsAnaesthesia);
			this.groupBox1.Controls.Add(this.m_chkIsSelfPay);
			this.groupBox1.Controls.Add(this.m_chkIsImport);
			this.groupBox1.Controls.Add(this.m_chkIsSelf);
			this.groupBox1.Controls.Add(this.m_chkIsCostly);
			this.groupBox1.Controls.Add(this.m_chkIsChlorpromazine);
			this.groupBox1.Location = new System.Drawing.Point(8, 344);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(472, 100);
			this.groupBox1.TabIndex = 68;
			this.groupBox1.TabStop = false;
			// 
			// m_chkIsAnaesthesia
			// 
			this.m_chkIsAnaesthesia.Location = new System.Drawing.Point(16, 16);
			this.m_chkIsAnaesthesia.Name = "m_chkIsAnaesthesia";
			this.m_chkIsAnaesthesia.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsAnaesthesia.TabIndex = 18;
			this.m_chkIsAnaesthesia.Text = "是否毒麻药品";
			// 
			// m_chkIsSelfPay
			// 
			this.m_chkIsSelfPay.Location = new System.Drawing.Point(344, 56);
			this.m_chkIsSelfPay.Name = "m_chkIsSelfPay";
			this.m_chkIsSelfPay.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsSelfPay.TabIndex = 23;
			this.m_chkIsSelfPay.Text = "是否自费药品";
			// 
			// m_chkIsImport
			// 
			this.m_chkIsImport.Location = new System.Drawing.Point(180, 56);
			this.m_chkIsImport.Name = "m_chkIsImport";
			this.m_chkIsImport.Size = new System.Drawing.Size(116, 24);
			this.m_chkIsImport.TabIndex = 22;
			this.m_chkIsImport.Text = "是否进口药品";
			// 
			// m_chkIsSelf
			// 
			this.m_chkIsSelf.Location = new System.Drawing.Point(16, 56);
			this.m_chkIsSelf.Name = "m_chkIsSelf";
			this.m_chkIsSelf.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsSelf.TabIndex = 21;
			this.m_chkIsSelf.Text = "是否院内制剂";
			// 
			// m_chkIsCostly
			// 
			this.m_chkIsCostly.Location = new System.Drawing.Point(344, 16);
			this.m_chkIsCostly.Name = "m_chkIsCostly";
			this.m_chkIsCostly.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsCostly.TabIndex = 20;
			this.m_chkIsCostly.Text = "是否贵重药品";
			// 
			// m_chkIsChlorpromazine
			// 
			this.m_chkIsChlorpromazine.Location = new System.Drawing.Point(180, 16);
			this.m_chkIsChlorpromazine.Name = "m_chkIsChlorpromazine";
			this.m_chkIsChlorpromazine.Size = new System.Drawing.Size(116, 24);
			this.m_chkIsChlorpromazine.TabIndex = 19;
			this.m_chkIsChlorpromazine.Text = "是否精神药品";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(232, 184);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 17);
			this.label8.TabIndex = 72;
			this.label8.Text = "剂量单位";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(-8, 184);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 17);
			this.label9.TabIndex = 71;
			this.label9.Text = "剂量";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(232, 264);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 17);
			this.label10.TabIndex = 76;
			this.label10.Text = "住院单位";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(-8, 264);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 17);
			this.label11.TabIndex = 75;
			this.label11.Text = "门诊单位";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_CobUnit
			// 
			this.m_CobUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_CobUnit.Location = new System.Drawing.Point(72, 261);
			this.m_CobUnit.Name = "m_CobUnit";
			this.m_CobUnit.Size = new System.Drawing.Size(120, 22);
			this.m_CobUnit.TabIndex = 14;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(-8, 315);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 17);
			this.label12.TabIndex = 78;
			this.label12.Text = "包装量";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtDosage
			// 
			//this.m_txtDosage.EnableAutoValidation = true;
			//this.m_txtDosage.EnableEnterKeyValidate = true;
			//this.m_txtDosage.EnableEscapeKeyUndo = true;
			//this.m_txtDosage.EnableLastValidValue = true;
			//this.m_txtDosage.ErrorProvider = null;
			//this.m_txtDosage.ErrorProviderMessage = "Invalid value";
			//this.m_txtDosage.ForceFormatText = true;
			this.m_txtDosage.Location = new System.Drawing.Point(72, 181);
			this.m_txtDosage.MaxLength = 5;
			this.m_txtDosage.Name = "m_txtDosage";
			//this.m_txtDosage.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtDosage.Size = new System.Drawing.Size(120, 23);
			this.m_txtDosage.TabIndex = 10;
			this.m_txtDosage.Text = "";
			this.m_txtDosage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(232, 227);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 17);
			this.label13.TabIndex = 84;
			this.label13.Text = "单价";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(-8, 227);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(72, 17);
			this.label14.TabIndex = 83;
			this.label14.Text = "批发价";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtTRADEPRICE
			// 
			//this.m_txtTRADEPRICE.EnableAutoValidation = true;
			//this.m_txtTRADEPRICE.EnableEnterKeyValidate = true;
			//this.m_txtTRADEPRICE.EnableEscapeKeyUndo = true;
			//this.m_txtTRADEPRICE.EnableLastValidValue = true;
			//this.m_txtTRADEPRICE.ErrorProvider = null;
			//this.m_txtTRADEPRICE.ErrorProviderMessage = "Invalid value";
			//this.m_txtTRADEPRICE.ForceFormatText = true;
			this.m_txtTRADEPRICE.Location = new System.Drawing.Point(72, 224);
			this.m_txtTRADEPRICE.MaxLength = 10;
			this.m_txtTRADEPRICE.Name = "m_txtTRADEPRICE";
			//this.m_txtTRADEPRICE.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtTRADEPRICE.Size = new System.Drawing.Size(120, 23);
			this.m_txtTRADEPRICE.TabIndex = 12;
			this.m_txtTRADEPRICE.Text = "";
			this.m_txtTRADEPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_cboProduct
			// 
			this.m_cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboProduct.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cboProduct.ItemHeight = 14;
			this.m_cboProduct.Location = new System.Drawing.Point(312, 312);
			this.m_cboProduct.Name = "m_cboProduct";
			this.m_cboProduct.Size = new System.Drawing.Size(136, 22);
			this.m_cboProduct.TabIndex = 17;
			// 
			// m_txtPackQty
			// 
			//this.m_txtPackQty.EnableAutoValidation = true;
			//this.m_txtPackQty.EnableEnterKeyValidate = true;
			//this.m_txtPackQty.EnableEscapeKeyUndo = true;
			//this.m_txtPackQty.EnableLastValidValue = true;
			//this.m_txtPackQty.ErrorProvider = null;
			//this.m_txtPackQty.ErrorProviderMessage = "Invalid value";
			this.m_txtPackQty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			//this.m_txtPackQty.ForceFormatText = true;
			this.m_txtPackQty.Location = new System.Drawing.Point(72, 312);
			this.m_txtPackQty.MaxLength = 5;
			this.m_txtPackQty.Name = "m_txtPackQty";
			//this.m_txtPackQty.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtPackQty.Size = new System.Drawing.Size(120, 23);
			this.m_txtPackQty.TabIndex = 16;
			this.m_txtPackQty.Text = "";
			this.m_txtPackQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_txtUNITPRICE
			// 
			//this.m_txtUNITPRICE.EnableAutoValidation = true;
			//this.m_txtUNITPRICE.EnableEnterKeyValidate = true;
			//this.m_txtUNITPRICE.EnableEscapeKeyUndo = true;
			//this.m_txtUNITPRICE.EnableLastValidValue = true;
			//this.m_txtUNITPRICE.ErrorProvider = null;
			//this.m_txtUNITPRICE.ErrorProviderMessage = "Invalid value";
			//this.m_txtUNITPRICE.ForceFormatText = true;
			this.m_txtUNITPRICE.Location = new System.Drawing.Point(312, 224);
			this.m_txtUNITPRICE.MaxLength = 10;
			this.m_txtUNITPRICE.Name = "m_txtUNITPRICE";
			//this.m_txtUNITPRICE.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtUNITPRICE.Size = new System.Drawing.Size(136, 23);
			this.m_txtUNITPRICE.TabIndex = 13;
			this.m_txtUNITPRICE.Text = "";
			this.m_txtUNITPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_cboMedType
			// 
			this.m_cboMedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboMedType.Location = new System.Drawing.Point(72, 144);
			this.m_cboMedType.Name = "m_cboMedType";
			this.m_cboMedType.Size = new System.Drawing.Size(120, 22);
			this.m_cboMedType.TabIndex = 8;
			// 
			// m_cboPreType
			// 
			this.m_cboPreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboPreType.Items.AddRange(new object[] {
															  "药品供应商",
															  "药品生产商",
															  "两者都是"});
			this.m_cboPreType.Location = new System.Drawing.Point(312, 144);
			this.m_cboPreType.Name = "m_cboPreType";
			this.m_cboPreType.Size = new System.Drawing.Size(136, 22);
			this.m_cboPreType.TabIndex = 9;
			// 
			// m_CobDosageUnit
			// 
			this.m_CobDosageUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_CobDosageUnit.Location = new System.Drawing.Point(312, 184);
			this.m_CobDosageUnit.Name = "m_CobDosageUnit";
			this.m_CobDosageUnit.Size = new System.Drawing.Size(136, 22);
			this.m_CobDosageUnit.TabIndex = 11;
			// 
			// m_CobIpUnit
			// 
			this.m_CobIpUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_CobIpUnit.Location = new System.Drawing.Point(312, 264);
			this.m_CobIpUnit.Name = "m_CobIpUnit";
			this.m_CobIpUnit.Size = new System.Drawing.Size(136, 22);
			this.m_CobIpUnit.TabIndex = 15;
			// 
			// frmMedicineInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(496, 509);
			this.Controls.Add(this.m_CobIpUnit);
			this.Controls.Add(this.m_CobDosageUnit);
			this.Controls.Add(this.m_cboPreType);
			this.Controls.Add(this.m_cboMedType);
			this.Controls.Add(this.m_txtUNITPRICE);
			this.Controls.Add(this.m_txtTRADEPRICE);
			this.Controls.Add(this.m_txtDosage);
			this.Controls.Add(this.m_txtPackQty);
			this.Controls.Add(this.m_txtEnName);
			this.Controls.Add(this.m_btnExit);
			this.Controls.Add(this.m_btSave);
			this.Controls.Add(this.m_btClear);
			this.Controls.Add(this.m_txtPY);
			this.Controls.Add(this.m_txtSpec);
			this.Controls.Add(this.m_txtWB);
			this.Controls.Add(this.m_txtName);
			this.Controls.Add(this.m_txtNo);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.m_CobUnit);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.m_cboProduct);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.m_lbName);
			this.Controls.Add(this.m_lbNo);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "frmMedicineInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药品基本信息";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedicineInfo_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMedicineInfo_KeyPress);
			this.Load += new System.EventHandler(this.frmMedicineInfo_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlMedicineInfo();
			objController.Set_GUI_Apperance(this);
		}

		
		private void m_btClear_Click(object sender, System.EventArgs e)
		{
			((clsControlMedicineInfo)this.objController).m_mthClear();
		}

		private void m_btSave_Click(object sender, System.EventArgs e)
		{
			bool blnCheck=((clsControlMedicineInfo)this.objController).blnCheckItem();
			if(blnCheck==false)
				return;
			long lngRec=((clsControlMedicineInfo)this.objController).SaveRec();
			if (lngRec>0)
			{
				MessageBox.Show("保存成功");
				((clsControlMedicine)this.clsParent).AddMedicineList(this.clsMedVO);
				this.Close();
			}
			else
				MessageBox.Show("保存失败");
		}

		private void m_txtNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (m_txtNo.Text=="")
				return;
		   long lngRes=((clsControlMedicineInfo)this.objController).FillMedicineText(m_txtNo.Text);
			if(lngRes==100)
			{
				m_txtNo.Text="";
			}
		}

		private void frmMedicineInfo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==System.Windows.Forms.Keys.Enter)
			SendKeys.SendWait("{Tab}");
		}
        
		public void ShowMe(clsMedicine_VO MedVO,com.digitalwave.iCare.gui.HIS.clsControlMedicine clsfrm)
		{
			this.clsParent=clsfrm;
			((clsControlMedicineInfo)this.objController).m_mthClear();
			((clsControlMedicineInfo)this.objController).FillMedType();
			((clsControlMedicineInfo)this.objController).FillPrepType();
			((clsControlMedicineInfo)this.objController).FillProductType();
			((clsControlMedicineInfo)this.objController).m_lngeFillCombo();
			if(MedVO==null)//是新增
				((clsControlMedicineInfo)this.objController).GetMedMaxID();
            ((clsControlMedicineInfo)this.objController).EditForm(MedVO);
		}

		private void m_txtNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
//			e.Handled = !(char.IsDigit(e.KeyChar) || (int)e.KeyChar == 8);
		}

		private void frmMedicineInfo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{

		   e.Handled=((e.KeyChar=="'".ToCharArray()[0])||(e.KeyChar==" ".ToCharArray()[0]));
		}

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void label7_Click(object sender, System.EventArgs e)
		{
		
		}

		private void frmMedicineInfo_Load(object sender, System.EventArgs e)
		{
			((clsControlMedicineInfo)this.objController).m_lngResetForm();
//			this.m_mthSetEnter2Tab();

		}

		private void m_txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				((clsControlMedicineInfo)this.objController).m_lngGetpywb();
		
		}

		private void m_txtName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

			

	}
}
