using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMetInfo 的摘要说明。
	/// </summary>
	public class frmMetInfo  : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		#region SystemGenerate
		internal System.Windows.Forms.TextBox m_txtDosage;
		internal System.Windows.Forms.TextBox m_txtPackQty;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.CheckBox m_chkIsAnaesthesia;
		internal System.Windows.Forms.CheckBox m_chkIsSelfPay;
		internal System.Windows.Forms.CheckBox m_chkIsImport;
		internal System.Windows.Forms.CheckBox m_chkIsSelf;
		internal System.Windows.Forms.CheckBox m_chkIsCostly;
		internal System.Windows.Forms.CheckBox m_chkIsChlorpromazine;
		internal System.Windows.Forms.TextBox m_txtEnName;
		private PinkieControls.ButtonXP m_btnExit;
		internal System.Windows.Forms.TextBox m_txtSpec;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label m_lbName;
		internal System.Windows.Forms.TextBox m_txtName;
		internal System.Windows.Forms.TextBox m_txtNo;
		internal System.Windows.Forms.TextBox m_txtMedType;
		internal System.Windows.Forms.TextBox DosageUnit;
		internal System.Windows.Forms.TextBox m_txtPreType;
		internal System.Windows.Forms.TextBox Unit;
		internal System.Windows.Forms.TextBox Product;
		internal System.Windows.Forms.TextBox IpUnit;
		internal System.Windows.Forms.CheckBox checkBox1;
		private System.ComponentModel.IContainer components;

		public frmMetInfo()
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
		#endregion

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.m_txtDosage = new System.Windows.Forms.TextBox();
			this.m_txtPackQty = new System.Windows.Forms.TextBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.m_chkIsAnaesthesia = new System.Windows.Forms.CheckBox();
			this.m_chkIsSelfPay = new System.Windows.Forms.CheckBox();
			this.m_chkIsImport = new System.Windows.Forms.CheckBox();
			this.m_chkIsSelf = new System.Windows.Forms.CheckBox();
			this.m_chkIsCostly = new System.Windows.Forms.CheckBox();
			this.m_chkIsChlorpromazine = new System.Windows.Forms.CheckBox();
			this.m_txtEnName = new System.Windows.Forms.TextBox();
			this.m_btnExit = new PinkieControls.ButtonXP();
			this.m_txtSpec = new System.Windows.Forms.TextBox();
			this.m_lbName = new System.Windows.Forms.Label();
			this.m_txtName = new System.Windows.Forms.TextBox();
			this.m_txtNo = new System.Windows.Forms.TextBox();
			this.m_txtMedType = new System.Windows.Forms.TextBox();
			this.DosageUnit = new System.Windows.Forms.TextBox();
			this.m_txtPreType = new System.Windows.Forms.TextBox();
			this.Unit = new System.Windows.Forms.TextBox();
			this.Product = new System.Windows.Forms.TextBox();
			this.IpUnit = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_txtDosage
			// 
			//this.m_txtDosage.EnableAutoValidation = true;
			this.m_txtDosage.Enabled = false;
			//this.m_txtDosage.EnableEnterKeyValidate = true;
			//this.m_txtDosage.EnableEscapeKeyUndo = true;
			//this.m_txtDosage.EnableLastValidValue = true;
			//this.m_txtDosage.ErrorProvider = null;
			//this.m_txtDosage.ErrorProviderMessage = "Invalid value";
			//this.m_txtDosage.ForceFormatText = true;
			this.m_txtDosage.Location = new System.Drawing.Point(80, 120);
			this.m_txtDosage.MaxLength = 5;
			this.m_txtDosage.Name = "m_txtDosage";
			//this.m_txtDosage.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtDosage.Size = new System.Drawing.Size(80, 23);
			this.m_txtDosage.TabIndex = 113;
			this.m_txtDosage.Text = "";
			this.m_txtDosage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_txtPackQty
			// 
			//this.m_txtPackQty.EnableAutoValidation = true;
			this.m_txtPackQty.Enabled = false;
			//this.m_txtPackQty.EnableEnterKeyValidate = true;
			//this.m_txtPackQty.EnableEscapeKeyUndo = true;
			//this.m_txtPackQty.EnableLastValidValue = true;
			//this.m_txtPackQty.ErrorProvider = null;
			//this.m_txtPackQty.ErrorProviderMessage = "Invalid value";
			//this.m_txtPackQty.ForceFormatText = true;
			this.m_txtPackQty.Location = new System.Drawing.Point(80, 192);
			this.m_txtPackQty.MaxLength = 5;
			this.m_txtPackQty.Name = "m_txtPackQty";
			//this.m_txtPackQty.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtPackQty.Size = new System.Drawing.Size(80, 23);
			this.m_txtPackQty.TabIndex = 112;
			this.m_txtPackQty.Text = "";
			this.m_txtPackQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.m_chkIsAnaesthesia);
			this.groupBox1.Controls.Add(this.m_chkIsSelfPay);
			this.groupBox1.Controls.Add(this.m_chkIsImport);
			this.groupBox1.Controls.Add(this.m_chkIsSelf);
			this.groupBox1.Controls.Add(this.m_chkIsCostly);
			this.groupBox1.Controls.Add(this.m_chkIsChlorpromazine);
			this.groupBox1.Location = new System.Drawing.Point(0, 248);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(464, 112);
			this.groupBox1.TabIndex = 103;
			this.groupBox1.TabStop = false;
			// 
			// checkBox1
			// 
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(16, 80);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(112, 24);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "是否缺药";
			// 
			// m_chkIsAnaesthesia
			// 
			this.m_chkIsAnaesthesia.Enabled = false;
			this.m_chkIsAnaesthesia.Location = new System.Drawing.Point(16, 16);
			this.m_chkIsAnaesthesia.Name = "m_chkIsAnaesthesia";
			this.m_chkIsAnaesthesia.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsAnaesthesia.TabIndex = 6;
			this.m_chkIsAnaesthesia.Text = "是否毒麻药品";
			// 
			// m_chkIsSelfPay
			// 
			this.m_chkIsSelfPay.Enabled = false;
			this.m_chkIsSelfPay.Location = new System.Drawing.Point(344, 48);
			this.m_chkIsSelfPay.Name = "m_chkIsSelfPay";
			this.m_chkIsSelfPay.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsSelfPay.TabIndex = 5;
			this.m_chkIsSelfPay.Text = "是否自费药品";
			// 
			// m_chkIsImport
			// 
			this.m_chkIsImport.Enabled = false;
			this.m_chkIsImport.Location = new System.Drawing.Point(180, 48);
			this.m_chkIsImport.Name = "m_chkIsImport";
			this.m_chkIsImport.Size = new System.Drawing.Size(116, 24);
			this.m_chkIsImport.TabIndex = 4;
			this.m_chkIsImport.Text = "是否进口药品";
			// 
			// m_chkIsSelf
			// 
			this.m_chkIsSelf.Enabled = false;
			this.m_chkIsSelf.Location = new System.Drawing.Point(16, 48);
			this.m_chkIsSelf.Name = "m_chkIsSelf";
			this.m_chkIsSelf.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsSelf.TabIndex = 3;
			this.m_chkIsSelf.Text = "是否院内制剂";
			// 
			// m_chkIsCostly
			// 
			this.m_chkIsCostly.Enabled = false;
			this.m_chkIsCostly.Location = new System.Drawing.Point(344, 16);
			this.m_chkIsCostly.Name = "m_chkIsCostly";
			this.m_chkIsCostly.Size = new System.Drawing.Size(112, 24);
			this.m_chkIsCostly.TabIndex = 2;
			this.m_chkIsCostly.Text = "是否贵重药品";
			// 
			// m_chkIsChlorpromazine
			// 
			this.m_chkIsChlorpromazine.Enabled = false;
			this.m_chkIsChlorpromazine.Location = new System.Drawing.Point(180, 16);
			this.m_chkIsChlorpromazine.Name = "m_chkIsChlorpromazine";
			this.m_chkIsChlorpromazine.Size = new System.Drawing.Size(116, 24);
			this.m_chkIsChlorpromazine.TabIndex = 1;
			this.m_chkIsChlorpromazine.Text = "是否精神药品";
			// 
			// m_txtEnName
			// 
			this.m_txtEnName.Enabled = false;
			this.m_txtEnName.Location = new System.Drawing.Point(80, 48);
			this.m_txtEnName.Name = "m_txtEnName";
			this.m_txtEnName.Size = new System.Drawing.Size(136, 23);
			this.m_txtEnName.TabIndex = 100;
			this.m_txtEnName.Text = "";
			// 
			// m_btnExit
			// 
			this.m_btnExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnExit.DefaultScheme = true;
			this.m_btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnExit.Hint = "";
			this.m_btnExit.Location = new System.Drawing.Point(176, 368);
			this.m_btnExit.Name = "m_btnExit";
			this.m_btnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnExit.Size = new System.Drawing.Size(96, 32);
			this.m_btnExit.TabIndex = 98;
			this.m_btnExit.Text = "关闭(&E)";
			this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
			// 
			// m_txtSpec
			// 
			this.m_txtSpec.Enabled = false;
			this.m_txtSpec.Location = new System.Drawing.Point(304, 49);
			this.m_txtSpec.MaxLength = 100;
			this.m_txtSpec.Name = "m_txtSpec";
			this.m_txtSpec.Size = new System.Drawing.Size(128, 23);
			this.m_txtSpec.TabIndex = 88;
			this.m_txtSpec.Text = "";
			// 
			// m_lbName
			// 
			this.m_lbName.AutoSize = true;
			this.m_lbName.Location = new System.Drawing.Point(256, 16);
			this.m_lbName.Name = "m_lbName";
			this.m_lbName.Size = new System.Drawing.Size(34, 19);
			this.m_lbName.TabIndex = 90;
			this.m_lbName.Text = "名称";
			this.m_lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_txtName
			// 
			this.m_txtName.Enabled = false;
			this.m_txtName.Location = new System.Drawing.Point(304, 12);
			this.m_txtName.MaxLength = 200;
			this.m_txtName.Name = "m_txtName";
			this.m_txtName.Size = new System.Drawing.Size(128, 23);
			this.m_txtName.TabIndex = 84;
			this.m_txtName.Text = "";
			// 
			// m_txtNo
			// 
			this.m_txtNo.Enabled = false;
			this.m_txtNo.Location = new System.Drawing.Point(80, 12);
			this.m_txtNo.MaxLength = 10;
			this.m_txtNo.Name = "m_txtNo";
			this.m_txtNo.Size = new System.Drawing.Size(80, 23);
			this.m_txtNo.TabIndex = 82;
			this.m_txtNo.Text = "";
			// 
			// m_txtMedType
			// 
			this.m_txtMedType.Enabled = false;
			this.m_txtMedType.Location = new System.Drawing.Point(80, 84);
			this.m_txtMedType.Name = "m_txtMedType";
			this.m_txtMedType.Size = new System.Drawing.Size(136, 23);
			this.m_txtMedType.TabIndex = 114;
			this.m_txtMedType.Text = "";
			// 
			// DosageUnit
			// 
			this.DosageUnit.Enabled = false;
			this.DosageUnit.Location = new System.Drawing.Point(304, 123);
			this.DosageUnit.Name = "DosageUnit";
			this.DosageUnit.Size = new System.Drawing.Size(128, 23);
			this.DosageUnit.TabIndex = 115;
			this.DosageUnit.Text = "";
			// 
			// m_txtPreType
			// 
			this.m_txtPreType.Enabled = false;
			this.m_txtPreType.Location = new System.Drawing.Point(304, 86);
			this.m_txtPreType.Name = "m_txtPreType";
			this.m_txtPreType.Size = new System.Drawing.Size(128, 23);
			this.m_txtPreType.TabIndex = 116;
			this.m_txtPreType.Text = "";
			// 
			// Unit
			// 
			this.Unit.Enabled = false;
			this.Unit.Location = new System.Drawing.Point(80, 156);
			this.Unit.Name = "Unit";
			this.Unit.Size = new System.Drawing.Size(80, 23);
			this.Unit.TabIndex = 117;
			this.Unit.Text = "";
			// 
			// Product
			// 
			this.Product.Enabled = false;
			this.Product.Location = new System.Drawing.Point(304, 196);
			this.Product.Name = "Product";
			this.Product.Size = new System.Drawing.Size(128, 23);
			this.Product.TabIndex = 118;
			this.Product.Text = "";
			// 
			// IpUnit
			// 
			this.IpUnit.Enabled = false;
			this.IpUnit.Location = new System.Drawing.Point(304, 164);
			this.IpUnit.Name = "IpUnit";
			this.IpUnit.Size = new System.Drawing.Size(128, 23);
			this.IpUnit.TabIndex = 119;
			this.IpUnit.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(256, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 19);
			this.label4.TabIndex = 94;
			this.label4.Text = "剂型";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(1, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 19);
			this.label6.TabIndex = 92;
			this.label6.Text = "药品类型";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(256, 52);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(34, 19);
			this.label13.TabIndex = 91;
			this.label13.Text = "规格";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(16, 16);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(48, 19);
			this.label14.TabIndex = 89;
			this.label14.Text = "助记码";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(224, 160);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(63, 19);
			this.label15.TabIndex = 110;
			this.label15.Text = "住院单位";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(224, 196);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(63, 19);
			this.label16.TabIndex = 101;
			this.label16.Text = "生产厂家";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(16, 52);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(48, 19);
			this.label17.TabIndex = 99;
			this.label17.Text = "英文名";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(1, 160);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(63, 19);
			this.label18.TabIndex = 109;
			this.label18.Text = "门诊单位";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(16, 196);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(48, 19);
			this.label19.TabIndex = 111;
			this.label19.Text = "包装量";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(224, 124);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(63, 19);
			this.label20.TabIndex = 106;
			this.label20.Text = "剂量单位";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(30, 124);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(34, 19);
			this.label21.TabIndex = 105;
			this.label21.Text = "剂量";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmMetInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(464, 405);
			this.Controls.Add(this.IpUnit);
			this.Controls.Add(this.Product);
			this.Controls.Add(this.Unit);
			this.Controls.Add(this.m_txtPreType);
			this.Controls.Add(this.DosageUnit);
			this.Controls.Add(this.m_txtMedType);
			this.Controls.Add(this.m_txtPackQty);
			this.Controls.Add(this.m_txtEnName);
			this.Controls.Add(this.m_btnExit);
			this.Controls.Add(this.m_txtSpec);
			this.Controls.Add(this.m_txtName);
			this.Controls.Add(this.m_txtNo);
			this.Controls.Add(this.m_txtDosage);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.m_lbName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label21);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmMetInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药品资料";
			this.Load += new System.EventHandler(this.frmMetInfo_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmMetInfo_Load(object sender, System.EventArgs e)
		{
		
		}

		public void ShowMe(clsMedicines_VO objArr,int p_intFlag)
		{

			((clscontrolMetInfo)this.objController).OpenWindow(objArr,p_intFlag);
		}
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clscontrolMetInfo();
			objController.Set_GUI_Apperance(this);
		}
	}
}
