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
	/// 药价维护 Create by Sam 2004-5-24
	/// </summary>
	public class frmMedPriceInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label m_lbName;
		private System.Windows.Forms.Label m_lbNo;
		private System.ComponentModel.IContainer components = null;
		private PinkieControls.ButtonXP m_btClear;
		private PinkieControls.ButtonXP m_btSave;
		internal string MedID=null; //保存当前的药品ID
        internal bool IsNew=true;  //表示是否为新增
		internal string strModifyDate=DateTime.Now.ToString(); //记录修改日期
		internal clsMedicinePrice_VO clsMedVO=null;
		internal System.Windows.Forms.ListView m_lvMed;
		private System.Windows.Forms.ColumnHeader col1;
		private System.Windows.Forms.ColumnHeader col2;
		internal System.Windows.Forms.DateTimePicker m_dtpStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.TextBox m_txtHiIn;
		internal System.Windows.Forms.TextBox m_txtLowOut;
		internal System.Windows.Forms.TextBox m_txtLowIn;
		internal System.Windows.Forms.TextBox m_txtHiOut;
		internal System.Windows.Forms.TextBox m_txtCurIn;
		internal System.Windows.Forms.TextBox m_txtCurOut;
		internal System.Windows.Forms.ComboBox m_cboUnit;
		internal System.Windows.Forms.TextBox m_txtMed;
		internal System.Windows.Forms.DateTimePicker m_dtpEnd;
		private PinkieControls.ButtonXP m_btnExit;
		private com.digitalwave.iCare.gui.HIS.clsControlMedPriceList clsParent;

		public frmMedPriceInfo()
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
			this.m_txtHiOut = new System.Windows.Forms.TextBox();
			this.m_txtHiIn = new System.Windows.Forms.TextBox();
			this.m_txtLowOut = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.m_cboUnit = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.m_lbName = new System.Windows.Forms.Label();
			this.m_lbNo = new System.Windows.Forms.Label();
			this.m_txtLowIn = new System.Windows.Forms.TextBox();
			this.m_txtMed = new System.Windows.Forms.TextBox();
			this.m_btClear = new PinkieControls.ButtonXP();
			this.m_btSave = new PinkieControls.ButtonXP();
			this.m_lvMed = new System.Windows.Forms.ListView();
			this.col1 = new System.Windows.Forms.ColumnHeader();
			this.col2 = new System.Windows.Forms.ColumnHeader();
			this.m_dtpStart = new System.Windows.Forms.DateTimePicker();
			this.m_dtpEnd = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.m_txtCurIn = new System.Windows.Forms.TextBox();
			this.m_txtCurOut = new System.Windows.Forms.TextBox();
			this.m_btnExit = new PinkieControls.ButtonXP();
			this.SuspendLayout();
			// 
			// m_txtHiOut
			// 
			this.m_txtHiOut.Location = new System.Drawing.Point(296, 104);
			this.m_txtHiOut.Name = "m_txtHiOut";
			this.m_txtHiOut.TabIndex = 7;
			this.m_txtHiOut.Text = "";
			// 
			// m_txtHiIn
			// 
			this.m_txtHiIn.Location = new System.Drawing.Point(296, 74);
			this.m_txtHiIn.MaxLength = 5;
			this.m_txtHiIn.Name = "m_txtHiIn";
			this.m_txtHiIn.TabIndex = 5;
			this.m_txtHiIn.Text = "";
			// 
			// m_txtLowOut
			// 
			this.m_txtLowOut.Location = new System.Drawing.Point(88, 104);
			this.m_txtLowOut.MaxLength = 100;
			this.m_txtLowOut.Name = "m_txtLowOut";
			this.m_txtLowOut.TabIndex = 6;
			this.m_txtLowOut.Text = "";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(208, 106);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(77, 19);
			this.label10.TabIndex = 48;
			this.label10.Text = "最高出货价";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(208, 76);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 19);
			this.label6.TabIndex = 47;
			this.label6.Text = "最高进货价";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(222, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63, 19);
			this.label5.TabIndex = 46;
			this.label5.Text = "结束时间";
			// 
			// m_cboUnit
			// 
			this.m_cboUnit.Location = new System.Drawing.Point(296, 14);
			this.m_cboUnit.Name = "m_cboUnit";
			this.m_cboUnit.Size = new System.Drawing.Size(100, 22);
			this.m_cboUnit.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(2, 76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 19);
			this.label4.TabIndex = 44;
			this.label4.Text = "最低进货价";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(251, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 19);
			this.label1.TabIndex = 43;
			this.label1.Text = "单位";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(2, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 19);
			this.label3.TabIndex = 42;
			this.label3.Text = "最低出货价";
			// 
			// m_lbName
			// 
			this.m_lbName.AutoSize = true;
			this.m_lbName.Location = new System.Drawing.Point(16, 46);
			this.m_lbName.Name = "m_lbName";
			this.m_lbName.Size = new System.Drawing.Size(63, 19);
			this.m_lbName.TabIndex = 41;
			this.m_lbName.Text = "起始时间";
			// 
			// m_lbNo
			// 
			this.m_lbNo.AutoSize = true;
			this.m_lbNo.Location = new System.Drawing.Point(45, 16);
			this.m_lbNo.Name = "m_lbNo";
			this.m_lbNo.Size = new System.Drawing.Size(34, 19);
			this.m_lbNo.TabIndex = 40;
			this.m_lbNo.Text = "药品";
			// 
			// m_txtLowIn
			// 
			this.m_txtLowIn.Location = new System.Drawing.Point(88, 74);
			this.m_txtLowIn.MaxLength = 5;
			this.m_txtLowIn.Name = "m_txtLowIn";
			this.m_txtLowIn.TabIndex = 4;
			this.m_txtLowIn.Text = "";
			// 
			// m_txtMed
			// 
			this.m_txtMed.Location = new System.Drawing.Point(88, 14);
			this.m_txtMed.MaxLength = 10;
			this.m_txtMed.Name = "m_txtMed";
			this.m_txtMed.TabIndex = 0;
			this.m_txtMed.Text = "";
			this.m_txtMed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMed_KeyDown);
			this.m_txtMed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtMed_KeyPress);
			this.m_txtMed.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtMed_Validating);
			this.m_txtMed.TextChanged += new System.EventHandler(this.m_txtMed_TextChanged);
			this.m_txtMed.Leave += new System.EventHandler(this.m_txtMed_Leave);
			this.m_txtMed.Enter += new System.EventHandler(this.m_txtMed_Enter);
			// 
			// m_btClear
			// 
			this.m_btClear.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btClear.DefaultScheme = true;
			this.m_btClear.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btClear.Hint = "";
			this.m_btClear.Location = new System.Drawing.Point(32, 192);
			this.m_btClear.Name = "m_btClear";
			this.m_btClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btClear.Size = new System.Drawing.Size(88, 32);
			this.m_btClear.TabIndex = 53;
			this.m_btClear.Text = "清空(&C)";
			this.m_btClear.Click += new System.EventHandler(this.m_btClear_Click);
			// 
			// m_btSave
			// 
			this.m_btSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btSave.DefaultScheme = true;
			this.m_btSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btSave.Hint = "";
			this.m_btSave.Location = new System.Drawing.Point(176, 192);
			this.m_btSave.Name = "m_btSave";
			this.m_btSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btSave.Size = new System.Drawing.Size(88, 32);
			this.m_btSave.TabIndex = 54;
			this.m_btSave.Text = "保存(&S)";
			this.m_btSave.Click += new System.EventHandler(this.m_btSave_Click);
			// 
			// m_lvMed
			// 
			this.m_lvMed.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.m_lvMed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lvMed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.col1,
																					  this.col2});
			this.m_lvMed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvMed.FullRowSelect = true;
			this.m_lvMed.GridLines = true;
			this.m_lvMed.HideSelection = false;
			this.m_lvMed.HoverSelection = true;
			this.m_lvMed.Location = new System.Drawing.Point(28, 224);
			this.m_lvMed.MultiSelect = false;
			this.m_lvMed.Name = "m_lvMed";
			this.m_lvMed.Size = new System.Drawing.Size(192, 136);
			this.m_lvMed.TabIndex = 55;
			this.m_lvMed.View = System.Windows.Forms.View.Details;
			this.m_lvMed.Visible = false;
			this.m_lvMed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lvMed_KeyDown);
			this.m_lvMed.Click += new System.EventHandler(this.m_lvMed_Click);
			this.m_lvMed.Leave += new System.EventHandler(this.m_lvMed_Leave);
			// 
			// col1
			// 
			this.col1.Text = "药品编号";
			this.col1.Width = 80;
			// 
			// col2
			// 
			this.col2.Text = "名称";
			this.col2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col2.Width = 111;
			// 
			// m_dtpStart
			// 
			this.m_dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.m_dtpStart.Location = new System.Drawing.Point(88, 44);
			this.m_dtpStart.Name = "m_dtpStart";
			this.m_dtpStart.Size = new System.Drawing.Size(100, 23);
			this.m_dtpStart.TabIndex = 56;
			// 
			// m_dtpEnd
			// 
			this.m_dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.m_dtpEnd.Location = new System.Drawing.Point(296, 44);
			this.m_dtpEnd.Name = "m_dtpEnd";
			this.m_dtpEnd.Size = new System.Drawing.Size(100, 23);
			this.m_dtpEnd.TabIndex = 57;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 19);
			this.label2.TabIndex = 58;
			this.label2.Text = "正常价格";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(208, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 19);
			this.label7.TabIndex = 59;
			this.label7.Text = "当前出库价";
			// 
			// m_txtCurIn
			// 
			this.m_txtCurIn.Location = new System.Drawing.Point(88, 134);
			this.m_txtCurIn.MaxLength = 5;
			this.m_txtCurIn.Name = "m_txtCurIn";
			this.m_txtCurIn.TabIndex = 60;
			this.m_txtCurIn.Text = "";
			// 
			// m_txtCurOut
			// 
			this.m_txtCurOut.Location = new System.Drawing.Point(296, 134);
			this.m_txtCurOut.MaxLength = 5;
			this.m_txtCurOut.Name = "m_txtCurOut";
			this.m_txtCurOut.TabIndex = 61;
			this.m_txtCurOut.Text = "";
			// 
			// m_btnExit
			// 
			this.m_btnExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnExit.DefaultScheme = true;
			this.m_btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnExit.Hint = "";
			this.m_btnExit.Location = new System.Drawing.Point(320, 192);
			this.m_btnExit.Name = "m_btnExit";
			this.m_btnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnExit.Size = new System.Drawing.Size(88, 32);
			this.m_btnExit.TabIndex = 62;
			this.m_btnExit.Text = "退出(&E)";
			this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
			// 
			// frmMedPriceInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.CancelButton = this.m_btnExit;
			this.ClientSize = new System.Drawing.Size(448, 237);
			this.Controls.Add(this.m_btnExit);
			this.Controls.Add(this.m_lvMed);
			this.Controls.Add(this.m_txtCurOut);
			this.Controls.Add(this.m_txtCurIn);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.m_txtHiOut);
			this.Controls.Add(this.m_txtHiIn);
			this.Controls.Add(this.m_txtLowOut);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.m_lbName);
			this.Controls.Add(this.m_lbNo);
			this.Controls.Add(this.m_txtLowIn);
			this.Controls.Add(this.m_txtMed);
			this.Controls.Add(this.m_dtpEnd);
			this.Controls.Add(this.m_dtpStart);
			this.Controls.Add(this.m_btSave);
			this.Controls.Add(this.m_btClear);
			this.Controls.Add(this.m_cboUnit);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "frmMedPriceInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药品价格信息";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedPriceInfo_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMedPriceInfo_KeyPress);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlMedPriceInfo();
			objController.Set_GUI_Apperance(this);
		}

		
		private void m_btClear_Click(object sender, System.EventArgs e)
		{
			((clsControlMedPriceInfo)this.objController).m_mthClear();
		}

		private void m_btSave_Click(object sender, System.EventArgs e)
		{
			bool blnCheck=((clsControlMedPriceInfo)this.objController).blnCheckItem();
			if(blnCheck==false)
				return;
			long lngRec=((clsControlMedPriceInfo)this.objController).SaveRec();
			if (lngRec>0)
			{
				MessageBox.Show("保存成功");
				((clsControlMedPriceList)this.clsParent).AddMedPriceList(this.clsMedVO);
				this.Close();
			}
			else
				MessageBox.Show("保存失败");
		}
        
		public void ShowMe(clsMedicinePrice_VO MedVO,com.digitalwave.iCare.gui.HIS.clsControlMedPriceList clsfrm)
		{
			this.clsParent=clsfrm;
			((clsControlMedPriceInfo)this.objController).FillUnit();
			((clsControlMedPriceInfo)this.objController).m_mthClear();
			((clsControlMedPriceInfo)this.objController).GetMedicineList();
			((clsControlMedPriceInfo)this.objController).EditForm(MedVO);
		}

		private void m_txtMed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
		}

		private void m_txtMed_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled = !(char.IsDigit(e.KeyChar) || (int)e.KeyChar == 8);
		}

		private void frmMedPriceInfo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==System.Windows.Forms.Keys.Enter)
				SendKeys.SendWait("{Tab}");
		}

		private void frmMedPriceInfo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=((e.KeyChar=="'".ToCharArray()[0])||(e.KeyChar==" ".ToCharArray()[0]));
		}

		private void m_txtMed_Enter(object sender, System.EventArgs e)
		{
			this.m_lvMed.Left=m_txtMed.Left;
			this.m_lvMed.Top=m_txtMed.Top+m_txtMed.Height;
			this.m_lvMed.Visible=true;
		}

		private void m_txtMed_Leave(object sender, System.EventArgs e)
		{
			if(this.ActiveControl.Name!="m_lvMed")
			{
				this.m_lvMed.Visible=false;
				if (m_txtMed.Text=="")
					return;
				long lngRes=((clsControlMedPriceInfo)this.objController).FillText(m_txtMed.Text);
				if(lngRes==100)
				{
					m_txtMed.Text="";
				}
			}   
		}
		private void m_lvMed_Leave(object sender, System.EventArgs e)
		{
          if(this.ActiveControl.Name!="m_txtMed")
		    this.m_lvMed.Visible=false;
		}

		private void m_lvMed_Click(object sender, System.EventArgs e)
		{
			((clsControlMedPriceInfo)this.objController).blnListClick();
		}

		private void m_lvMed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==System.Windows.Forms.Keys.Enter)
			((clsControlMedPriceInfo)this.objController).blnListClick();
		}

		private void m_txtMed_TextChanged(object sender, System.EventArgs e)
		{
		   ((clsControlMedPriceInfo)this.objController).FindListByIDorName();
		}

		private void m_txtMed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Down)
			{
				this.m_lvMed.Focus();
			}
		}

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

			

	}
}
