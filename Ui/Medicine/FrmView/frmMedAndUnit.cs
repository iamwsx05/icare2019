using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 药品与单位关系维护 Create by Sam 2004-5-24
	/// </summary>
	public class frmMedAndUnit :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		internal System.Windows.Forms.ListView m_lvwList;
		private PinkieControls.ButtonXP m_btnSave;
		private PinkieControls.ButtonXP m_btnNew;
		internal System.Windows.Forms.ComboBox m_cboBig;
		internal System.Windows.Forms.ComboBox m_cboSmall;
		internal System.Windows.Forms.TextBox m_txtBig;
		internal System.Windows.Forms.TextBox m_txtSmall;
		internal System.Windows.Forms.TextBox m_txtLevel;
		internal System.Windows.Forms.ComboBox m_cboFlag;
		private System.Windows.Forms.Label label7;
		private PinkieControls.ButtonXP m_btnFind;
		internal System.Windows.Forms.TextBox m_txtMed;
        internal bool IsNew=false;
		private PinkieControls.ButtonXP m_btnDel;
		private PinkieControls.ButtonXP m_btnExit;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMedAndUnit()
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
			this.m_lvwList = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.m_cboBig = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.m_cboSmall = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.m_txtBig = new System.Windows.Forms.TextBox();
			this.m_txtSmall = new System.Windows.Forms.TextBox();
			this.m_txtLevel = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.m_cboFlag = new System.Windows.Forms.ComboBox();
			this.m_btnSave = new PinkieControls.ButtonXP();
			this.m_btnNew = new PinkieControls.ButtonXP();
			this.m_txtMed = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.m_btnFind = new PinkieControls.ButtonXP();
			this.m_btnDel = new PinkieControls.ButtonXP();
			this.m_btnExit = new PinkieControls.ButtonXP();
			this.SuspendLayout();
			// 
			// m_lvwList
			// 
			this.m_lvwList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lvwList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader3,
																						this.columnHeader1,
																						this.columnHeader4,
																						this.columnHeader2,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7});
			this.m_lvwList.FullRowSelect = true;
			this.m_lvwList.GridLines = true;
			this.m_lvwList.Location = new System.Drawing.Point(0, 0);
			this.m_lvwList.Name = "m_lvwList";
			this.m_lvwList.Size = new System.Drawing.Size(504, 152);
			this.m_lvwList.TabIndex = 0;
			this.m_lvwList.View = System.Windows.Forms.View.Details;
			this.m_lvwList.SelectedIndexChanged += new System.EventHandler(this.m_lvwList_SelectedIndexChanged);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "药品";
			this.columnHeader3.Width = 150;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "大单位";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "数量";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "小单位";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "数量";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "级别";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "使用标志";
			this.columnHeader7.Width = 80;
			// 
			// m_cboBig
			// 
			this.m_cboBig.Enabled = false;
			this.m_cboBig.Location = new System.Drawing.Point(80, 206);
			this.m_cboBig.Name = "m_cboBig";
			this.m_cboBig.Size = new System.Drawing.Size(120, 22);
			this.m_cboBig.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(26, 208);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 19);
			this.label1.TabIndex = 2;
			this.label1.Text = "大单位";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 19);
			this.label2.TabIndex = 3;
			this.label2.Text = "小单位";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(40, 272);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 19);
			this.label3.TabIndex = 4;
			this.label3.Text = "级别";
			// 
			// m_cboSmall
			// 
			this.m_cboSmall.Location = new System.Drawing.Point(80, 238);
			this.m_cboSmall.Name = "m_cboSmall";
			this.m_cboSmall.Size = new System.Drawing.Size(120, 22);
			this.m_cboSmall.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(232, 208);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 19);
			this.label4.TabIndex = 6;
			this.label4.Text = "数量";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(232, 240);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 19);
			this.label5.TabIndex = 7;
			this.label5.Text = "数量";
			// 
			// m_txtBig
			// 
			this.m_txtBig.Location = new System.Drawing.Point(272, 206);
			this.m_txtBig.Name = "m_txtBig";
			this.m_txtBig.Size = new System.Drawing.Size(88, 23);
			this.m_txtBig.TabIndex = 3;
			this.m_txtBig.Text = "";
			this.m_txtBig.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtBig_KeyPress);
			// 
			// m_txtSmall
			// 
			this.m_txtSmall.Location = new System.Drawing.Point(272, 238);
			this.m_txtSmall.Name = "m_txtSmall";
			this.m_txtSmall.Size = new System.Drawing.Size(88, 23);
			this.m_txtSmall.TabIndex = 5;
			this.m_txtSmall.Text = "";
			this.m_txtSmall.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtSmall_KeyPress);
			// 
			// m_txtLevel
			// 
			this.m_txtLevel.Location = new System.Drawing.Point(80, 270);
			this.m_txtLevel.Name = "m_txtLevel";
			this.m_txtLevel.Size = new System.Drawing.Size(96, 23);
			this.m_txtLevel.TabIndex = 6;
			this.m_txtLevel.Text = "";
			this.m_txtLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtLevel_KeyPress);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(203, 272);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 19);
			this.label6.TabIndex = 11;
			this.label6.Text = "使用标志";
			// 
			// m_cboFlag
			// 
			this.m_cboFlag.Location = new System.Drawing.Point(272, 270);
			this.m_cboFlag.Name = "m_cboFlag";
			this.m_cboFlag.Size = new System.Drawing.Size(88, 22);
			this.m_cboFlag.TabIndex = 7;
			// 
			// m_btnSave
			// 
			this.m_btnSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnSave.DefaultScheme = true;
			this.m_btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnSave.Hint = "";
			this.m_btnSave.Location = new System.Drawing.Point(240, 312);
			this.m_btnSave.Name = "m_btnSave";
			this.m_btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnSave.Size = new System.Drawing.Size(70, 32);
			this.m_btnSave.TabIndex = 9;
			this.m_btnSave.Text = "保存(&S)";
			this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
			// 
			// m_btnNew
			// 
			this.m_btnNew.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnNew.DefaultScheme = true;
			this.m_btnNew.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnNew.Hint = "";
			this.m_btnNew.Location = new System.Drawing.Point(32, 312);
			this.m_btnNew.Name = "m_btnNew";
			this.m_btnNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnNew.Size = new System.Drawing.Size(70, 32);
			this.m_btnNew.TabIndex = 8;
			this.m_btnNew.Text = "新增(&A)";
			this.m_btnNew.Click += new System.EventHandler(this.m_btnNew_Click);
			// 
			// m_txtMed
			// 
			this.m_txtMed.Location = new System.Drawing.Point(80, 174);
			this.m_txtMed.Name = "m_txtMed";
			this.m_txtMed.Size = new System.Drawing.Size(80, 23);
			this.m_txtMed.TabIndex = 1;
			this.m_txtMed.Text = "";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(40, 176);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(34, 19);
			this.label7.TabIndex = 16;
			this.label7.Text = "药品";
			// 
			// m_btnFind
			// 
			this.m_btnFind.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnFind.DefaultScheme = true;
			this.m_btnFind.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnFind.Enabled = false;
			this.m_btnFind.Hint = "";
			this.m_btnFind.Location = new System.Drawing.Point(170, 168);
			this.m_btnFind.Name = "m_btnFind";
			this.m_btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnFind.Size = new System.Drawing.Size(70, 32);
			this.m_btnFind.TabIndex = 10;
			this.m_btnFind.Text = "查询(&F)";
			this.m_btnFind.Click += new System.EventHandler(this.m_btnFind_Click);
			// 
			// m_btnDel
			// 
			this.m_btnDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnDel.DefaultScheme = true;
			this.m_btnDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnDel.Hint = "";
			this.m_btnDel.Location = new System.Drawing.Point(136, 312);
			this.m_btnDel.Name = "m_btnDel";
			this.m_btnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnDel.Size = new System.Drawing.Size(70, 32);
			this.m_btnDel.TabIndex = 17;
			this.m_btnDel.Text = "删除(&D)";
			this.m_btnDel.Click += new System.EventHandler(this.m_btnDel_Click);
			// 
			// m_btnExit
			// 
			this.m_btnExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnExit.DefaultScheme = true;
			this.m_btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnExit.Hint = "";
			this.m_btnExit.Location = new System.Drawing.Point(344, 312);
			this.m_btnExit.Name = "m_btnExit";
			this.m_btnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnExit.Size = new System.Drawing.Size(70, 32);
			this.m_btnExit.TabIndex = 64;
			this.m_btnExit.Text = "退出(&E)";
			this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
			// 
			// frmMedAndUnit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.CancelButton = this.m_btnExit;
			this.ClientSize = new System.Drawing.Size(504, 365);
			this.Controls.Add(this.m_btnExit);
			this.Controls.Add(this.m_btnDel);
			this.Controls.Add(this.m_btnFind);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.m_txtMed);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.m_txtLevel);
			this.Controls.Add(this.m_txtSmall);
			this.Controls.Add(this.m_txtBig);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_btnSave);
			this.Controls.Add(this.m_btnNew);
			this.Controls.Add(this.m_cboFlag);
			this.Controls.Add(this.m_cboSmall);
			this.Controls.Add(this.m_cboBig);
			this.Controls.Add(this.m_lvwList);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmMedAndUnit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药品单位维护";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMedAndUnit_KeyPress);
			this.Load += new System.EventHandler(this.frmMedAndUnit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlMedAndUnit();
			objController.Set_GUI_Apperance(this);
		}

		private void m_btnFind_Click(object sender, System.EventArgs e)
		{
			string strID=null;
			string strName=null;
			frmMedicine frm=new frmMedicine();
			frm.ShowMe(true,out strID,out strName);
			if(strID!=null)
			{
				this.m_txtMed.Text=strName;
				this.m_txtMed.Tag=strID;
				((clsControlMedAndUnit)this.objController).SetLevMaxID();
			}
		}
		private void frmMedAndUnit_Load(object sender, System.EventArgs e)
		{
			((clsControlMedAndUnit)this.objController).m_mthClear();
			((clsControlMedAndUnit)this.objController).FillComboBox();
			((clsControlMedAndUnit)this.objController).GetMedAndUnitList();
		}

		private void m_btnNew_Click(object sender, System.EventArgs e)
		{
		   ((clsControlMedAndUnit)this.objController).m_mthClear();
			this.IsNew=true;
			this.m_btnFind.Enabled=true;
			this.m_txtMed.Focus();
			this.m_cboBig.Enabled=true;
		}

		private void m_lvwList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_lvwList.SelectedItems.Count > 0)
			{
				((clsControlMedAndUnit)this.objController).FillToTxt();
				this.m_btnFind.Enabled=false;
			}
		}

		private void frmMedAndUnit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=((e.KeyChar=="'".ToCharArray()[0])||(e.KeyChar==" ".ToCharArray()[0]));
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
		   ((clsControlMedAndUnit)this.objController).m_lngSave();
		}

		private void m_txtLevel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=!clsPublicParm.ValNumer(e.KeyChar,null);
		}

		private void m_txtBig_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		   e.Handled=!clsPublicParm.ValNumer(e.KeyChar,m_txtBig.Text);
		}

		private void m_txtSmall_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		   e.Handled=!clsPublicParm.ValNumer(e.KeyChar,m_txtSmall.Text);
		}

		private void m_btnDel_Click(object sender, System.EventArgs e)
		{
			((clsControlMedAndUnit)this.objController).m_lngDel();
		}

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
