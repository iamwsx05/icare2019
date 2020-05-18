using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 签名 的摘要说明。
	/// </summary>
	public class frmSinature : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private clsDcl_InvoiceManage m_objManage = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		public com.digitalwave.controls.exTextBox txtID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btOK;
		private System.Windows.Forms.Button btExit;
		private com.digitalwave.controls.exTextBox txtName;
		private com.digitalwave.controls.exTextBox txtPS;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		public clsDcl_InvoiceManage DataServer
		{
			set
			{
			this.m_objManage =value;
			}
		}
		private string _strInv="";
		public System.Windows.Forms.DateTimePicker m_dtmTime;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.ComboBox m_cboType;
		private string _strStatues ="";
		public System.Windows.Forms.Label m_lbldrip;
		/// <summary>
		/// -1:出错: 0-注射单 1-输液巡视单 2-贴瓶单 3-治疗单 4-手术单 5-输血单 6-配药 7-发药
		/// </summary>
		private int m_intType = -1;
		public System.Windows.Forms.TextBox m_txtDripping;
		public System.Windows.Forms.Panel panel1;
		public string m_Type = "";

		public frmSinature(string strInv,string strStatues)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			_strInv =strInv;
			_strStatues =strStatues;
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}


		public frmSinature()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_intType"> -1:出错: 0-注射单 1-输液巡视单 2-贴瓶单 3-治疗单 4-手术单 5-输血单 6-配药 7-发药</param>
		public frmSinature(int p_intType)
		{
			m_intType = p_intType;
			InitializeComponent();
			m_intAddComBoxItemByType(p_intType);

//			if(m_Type!="")
//			m_mthSetItem(m_Type);

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSinature));
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_cboType = new System.Windows.Forms.ComboBox();
			this.m_txtDripping = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtPS = new com.digitalwave.controls.exTextBox();
			this.txtID = new com.digitalwave.controls.exTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new com.digitalwave.controls.exTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.m_dtmTime = new System.Windows.Forms.DateTimePicker();
			this.btExit = new System.Windows.Forms.Button();
			this.btOK = new System.Windows.Forms.Button();
			this.m_lbldrip = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "工  号:";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_cboType);
			this.groupBox1.Controls.Add(this.m_txtDripping);
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.m_dtmTime);
			this.groupBox1.Controls.Add(this.btExit);
			this.groupBox1.Controls.Add(this.btOK);
			this.groupBox1.Controls.Add(this.m_lbldrip);
			this.groupBox1.Location = new System.Drawing.Point(16, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 276);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "请输入工号和密码";
			// 
			// m_cboType
			// 
			this.m_cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboType.Location = new System.Drawing.Point(144, 56);
			this.m_cboType.Name = "m_cboType";
			this.m_cboType.Size = new System.Drawing.Size(184, 22);
			this.m_cboType.TabIndex = 1;
			this.m_cboType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboType_KeyDown);
			this.m_cboType.SelectedIndexChanged += new System.EventHandler(this.m_cboType_SelectedIndexChanged);
			// 
			// m_txtDripping
			// 
			this.m_txtDripping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_txtDripping.Location = new System.Drawing.Point(144, 84);
			this.m_txtDripping.Name = "m_txtDripping";
			this.m_txtDripping.Size = new System.Drawing.Size(184, 23);
			this.m_txtDripping.TabIndex = 2;
			this.m_txtDripping.Text = "";
			this.m_txtDripping.Visible = false;
			this.m_txtDripping.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDripping_KeyDown);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtPS);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.txtID);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.txtName);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(56, 104);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(296, 104);
			this.panel1.TabIndex = 3;
			// 
			// txtPS
			// 
			this.txtPS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtPS.Location = new System.Drawing.Point(88, 72);
			this.txtPS.MaxLength = 16;
			this.txtPS.Name = "txtPS";
			this.txtPS.PasswordChar = '*';
			this.txtPS.SendTabKey = false;
			this.txtPS.SetFocusColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.txtPS.Size = new System.Drawing.Size(184, 26);
			this.txtPS.TabIndex = 2;
			this.txtPS.Text = "";
			this.txtPS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPS_KeyDown);
			// 
			// txtID
			// 
			this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID.Location = new System.Drawing.Point(88, 8);
			this.txtID.MaxLength = 10;
			this.txtID.Name = "txtID";
			this.txtID.SendTabKey = false;
			this.txtID.SetFocusColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.txtID.Size = new System.Drawing.Size(184, 23);
			this.txtID.TabIndex = 0;
			this.txtID.Text = "";
			this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "姓  名:";
			// 
			// txtName
			// 
			this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtName.Enabled = false;
			this.txtName.Location = new System.Drawing.Point(88, 40);
			this.txtName.MaxLength = 10;
			this.txtName.Name = "txtName";
			this.txtName.SendTabKey = false;
			this.txtName.SetFocusColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.txtName.Size = new System.Drawing.Size(184, 23);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 19);
			this.label3.TabIndex = 4;
			this.label3.Text = "密  码:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(80, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 19);
			this.label5.TabIndex = 10;
			this.label5.Text = "类  型:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(80, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 19);
			this.label4.TabIndex = 9;
			this.label4.Text = "时  间:";
			// 
			// m_dtmTime
			// 
			this.m_dtmTime.CustomFormat = "yyyy年MM月dd日 HH时mm分";
			this.m_dtmTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.m_dtmTime.Location = new System.Drawing.Point(144, 24);
			this.m_dtmTime.Name = "m_dtmTime";
			this.m_dtmTime.Size = new System.Drawing.Size(184, 23);
			this.m_dtmTime.TabIndex = 8;
			// 
			// btExit
			// 
			this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btExit.Location = new System.Drawing.Point(240, 216);
			this.btExit.Name = "btExit";
			this.btExit.Size = new System.Drawing.Size(104, 32);
			this.btExit.TabIndex = 7;
			this.btExit.Text = "退出(ESC)";
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// btOK
			// 
			this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btOK.Location = new System.Drawing.Point(64, 216);
			this.btOK.Name = "btOK";
			this.btOK.Size = new System.Drawing.Size(104, 32);
			this.btOK.TabIndex = 6;
			this.btOK.Text = "签名(&S)";
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// m_lbldrip
			// 
			this.m_lbldrip.AutoSize = true;
			this.m_lbldrip.Location = new System.Drawing.Point(80, 88);
			this.m_lbldrip.Name = "m_lbldrip";
			this.m_lbldrip.Size = new System.Drawing.Size(56, 19);
			this.m_lbldrip.TabIndex = 4;
			this.m_lbldrip.Text = "滴  速:";
			this.m_lbldrip.Visible = false;
			// 
			// frmSinature
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.CancelButton = this.btExit;
			this.ClientSize = new System.Drawing.Size(440, 295);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmSinature";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "签名";
			this.Load += new System.EventHandler(this.frmAudingInvoice_Load);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsSinature();
			objController.Set_GUI_Apperance(this);
		}
		private void frmAudingInvoice_Load(object sender, System.EventArgs e)
		{
			if(this.m_objManage==null)
			{
			this.m_objManage =new clsDcl_InvoiceManage();
			}
			if(m_intType==2)
				this.txtID.Focus();
			else if(m_intType==1)
			{
				
				panel1.TabIndex = 5;
				groupBox1.TabIndex = 0;
				m_cboType.TabIndex = 0;
				
			}

		}

		private void txtPS_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode ==Keys.Enter)
			{
			this.btOK.Focus();
			}
		}
		public string AudingName
		{
			get
			{
			return this.txtName.Text;
			}
		}
		private void btOK_Click(object sender, System.EventArgs e)
		{
			if(this.txtID.Tag==null)
			{
				MessageBox.Show("请输入工号");
				this.txtID.Focus();
				return;
			}
			if(this.txtPS.Tag.ToString().Trim()!=this.txtPS.Text.Trim())
			{
				MessageBox.Show("密码错误");
				this.txtPS.Focus();
				return;
			}
				this.DialogResult=DialogResult.OK;

		}

		private void txtID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				if(this.txtID.Text.Trim()=="")
				{
					MessageBox.Show("请输入工号");
					this.txtID.Focus();
					return;
				}
			    DataTable dt;
				long ret =m_objManage.m_mthGetEmployeeInfo(this.txtID.Text.Trim(),out dt,"");
				if(ret >0&&dt.Rows.Count>0)
				{
					this.txtName.Text =dt.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
					this.txtID.Tag =dt.Rows[0]["empid_chr"].ToString().Trim();
					this.txtPS.Tag =dt.Rows[0]["psw_chr"].ToString().Trim();
					this.txtPS.Focus();
				}
				else
				{
					MessageBox.Show("输入的工号不正确");
					this.txtID.Focus();
					return;
				}
			}
		}

		private void btExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}
		#region  0-注射单 1-输液巡视单 2-贴瓶单 3-治疗单 4-手术单 5-输血单 6-配药 7-发药 
		/// <summary>
		/// -1:出错: 0-注射单 1-输液巡视单 2-贴瓶单 3-治疗单 4-手术单 5-输血单 6-配药 7-发药
		/// </summary>
		public void m_intAddComBoxItemByType(int p_intType)
		{
			switch(p_intType)
			{
				case 0:		
					this.m_cboType.Items.AddRange(new object[] {
																   "执行人",
																   "配药护士"});
//					this.m_cboType.SelectedIndex = 1;
//					m_txtDripping.Visible = false;
//					m_lbldrip.Visible = false;
//					this.panel1.Location = new Point(56,88);
//					txtID.Focus();

					this.m_cboType.SelectedIndex = 1;
					this.m_cboType.Enabled = true;

					m_txtDripping.Visible = true;
					m_lbldrip.Visible = true;
					m_txtDripping.Enabled = false;
					this.panel1.Location = new Point(56,104);
					m_txtDripping.Focus();

					break;
				case 1:
					this.m_cboType.Items.AddRange(new object[] {
																   "巡视"
//																   "巡视2",
//																   "巡视3"
															   });
					m_txtDripping.Visible = true;
					m_lbldrip.Visible = true;
					this.panel1.Location = new Point(56,104);
					this.m_cboType.SelectedIndex = 0;
					m_txtDripping.Focus();
					break;
					
				case 2:
					this.m_cboType.Items.AddRange(new object[] {
																   "贴瓶单"});
					this.m_cboType.SelectedIndex = 0;
					this.m_cboType.Enabled = false;

					m_txtDripping.Visible = false;
					m_lbldrip.Visible = false;
					this.panel1.Location = new Point(56,88);
					txtID.Focus();
					break;	
				case 3:
					this.m_cboType.Items.AddRange(new object[] {
																   "执行人"});
					this.m_cboType.SelectedIndex = 0;
					this.m_cboType.Enabled = false;

					m_txtDripping.Visible = false;
					m_lbldrip.Visible = false;
					this.panel1.Location = new Point(56,88);
					txtID.Focus();
					break;
				case 4:
					this.m_cboType.Items.AddRange(new object[] {
																   "执行人"});
					this.m_cboType.SelectedIndex = 0;
					this.m_cboType.Enabled = false;

					m_txtDripping.Visible = false;
					m_lbldrip.Visible = false;
					this.panel1.Location = new Point(56,88);
					txtID.Focus();
					break;
				case 5:
					this.m_cboType.Items.AddRange(new object[] {
																   "领血人"});
					this.m_cboType.SelectedIndex = 0;
					this.m_cboType.Enabled = false;

					m_txtDripping.Visible = false;
					m_lbldrip.Visible = false;
					this.panel1.Location = new Point(56,88);
					txtID.Focus();
					break;
				case 6:
				
					break;
				case 7:
				
					break;
				default:
					break;
			}
		}
		#endregion

		void m_mthSetItem(string p_str)
		{
			for(int i=0;i<this.m_cboType.Items.Count; ++i)
			{
				if(this.m_cboType.Items[i].ToString().Trim() == p_str)
				{
					this.m_cboType.SelectedIndex = i;
					break;
				}
			}
		}
		bool blnfirst = true;
		private void m_cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{	

			if(this.m_cboType.Text.ToString().Trim()=="配药护士"&&m_txtDripping.Visible==true)
				m_txtDripping.Enabled = false;
			if(this.m_cboType.Text.ToString().Trim()=="执行人"&&m_txtDripping.Visible==true)
				m_txtDripping.Enabled = true;
				
		}

		private void m_txtDripping_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				txtID.Focus();
			}
		}

		private void m_cboType_Enter(object sender, System.EventArgs e)
		{

		}

		private void m_cboType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			SendKeys.Send("{tab}");
		}
	}
}
