using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStorageMedLimitMgr 的摘要说明。
	/// </summary>
	public class frmStorageMedLimitMgr :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		internal System.Windows.Forms.ListView m_lvMed;
		private System.Windows.Forms.ColumnHeader col1;
		private System.Windows.Forms.ColumnHeader col2;
		private System.Windows.Forms.ColumnHeader col3;
		private System.Windows.Forms.ColumnHeader col4;
		private System.Windows.Forms.ColumnHeader col5;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox m_cboStorage;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox m_txtMed;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.TextBox m_txtLow;
		internal System.Windows.Forms.TextBox m_txtHi;
		internal System.Windows.Forms.TextBox m_txtQTY;
		internal System.Windows.Forms.ComboBox m_cboUnit;
		private PinkieControls.ButtonXP m_btnAdd;
		private PinkieControls.ButtonXP m_btnSave;
		private PinkieControls.ButtonXP m_btnDel;
		internal System.Windows.Forms.ListView m_lvw;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		internal System.Windows.Forms.NumericUpDown m_txtPer;
		private System.Windows.Forms.Label label8;
		private PinkieControls.ButtonXP m_btnExit;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStorageMedLimitMgr()
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
			this.m_lvMed = new System.Windows.Forms.ListView();
			this.col1 = new System.Windows.Forms.ColumnHeader();
			this.col2 = new System.Windows.Forms.ColumnHeader();
			this.col3 = new System.Windows.Forms.ColumnHeader();
			this.col4 = new System.Windows.Forms.ColumnHeader();
			this.col5 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.m_cboStorage = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.m_txtMed = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.m_txtLow = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.m_txtHi = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.m_txtQTY = new System.Windows.Forms.TextBox();
			this.m_cboUnit = new System.Windows.Forms.ComboBox();
			this.m_btnAdd = new PinkieControls.ButtonXP();
			this.m_btnSave = new PinkieControls.ButtonXP();
			this.m_btnDel = new PinkieControls.ButtonXP();
			this.m_lvw = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.m_txtPer = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.m_btnExit = new PinkieControls.ButtonXP();
			((System.ComponentModel.ISupportInitialize)(this.m_txtPer)).BeginInit();
			this.SuspendLayout();
			// 
			// m_lvMed
			// 
			this.m_lvMed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lvMed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.col1,
																					  this.col2,
																					  this.col3,
																					  this.col4,
																					  this.col5});
			this.m_lvMed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvMed.FullRowSelect = true;
			this.m_lvMed.GridLines = true;
			this.m_lvMed.HideSelection = false;
			this.m_lvMed.Location = new System.Drawing.Point(0, 48);
			this.m_lvMed.MultiSelect = false;
			this.m_lvMed.Name = "m_lvMed";
			this.m_lvMed.Size = new System.Drawing.Size(568, 176);
			this.m_lvMed.TabIndex = 1;
			this.m_lvMed.View = System.Windows.Forms.View.Details;
			this.m_lvMed.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvMed_ColumnClick);
			this.m_lvMed.SelectedIndexChanged += new System.EventHandler(this.m_lvMed_SelectedIndexChanged);
			// 
			// col1
			// 
			this.col1.Text = "药品编号";
			this.col1.Width = 100;
			// 
			// col2
			// 
			this.col2.Text = "名称";
			this.col2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col2.Width = 100;
			// 
			// col3
			// 
			this.col3.Text = "单位";
			this.col3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col3.Width = 100;
			// 
			// col4
			// 
			this.col4.Text = "库存下限";
			this.col4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col4.Width = 100;
			// 
			// col5
			// 
			this.col5.Text = "库存上限";
			this.col5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.col5.Width = 100;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "选择仓库";
			// 
			// m_cboStorage
			// 
			this.m_cboStorage.Location = new System.Drawing.Point(72, 16);
			this.m_cboStorage.Name = "m_cboStorage";
			this.m_cboStorage.Size = new System.Drawing.Size(144, 22);
			this.m_cboStorage.TabIndex = 0;
			this.m_cboStorage.SelectedIndexChanged += new System.EventHandler(this.m_cboStorage_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 258);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 19);
			this.label2.TabIndex = 60;
			this.label2.Text = "药品";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_txtMed
			// 
			this.m_txtMed.Location = new System.Drawing.Point(80, 256);
			this.m_txtMed.Name = "m_txtMed";
			this.m_txtMed.Size = new System.Drawing.Size(112, 23);
			this.m_txtMed.TabIndex = 2;
			this.m_txtMed.Text = "";
			this.m_txtMed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtMed_KeyDown);
			this.m_txtMed.TextChanged += new System.EventHandler(this.m_txtMed_TextChanged);
			this.m_txtMed.Leave += new System.EventHandler(this.m_txtMed_Leave);
			this.m_txtMed.Enter += new System.EventHandler(this.m_txtMed_Enter);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 297);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 19);
			this.label3.TabIndex = 62;
			this.label3.Text = "库存下限";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_txtLow
			// 
			this.m_txtLow.Location = new System.Drawing.Point(80, 295);
			this.m_txtLow.MaxLength = 6;
			this.m_txtLow.Name = "m_txtLow";
			this.m_txtLow.Size = new System.Drawing.Size(112, 23);
			this.m_txtLow.TabIndex = 4;
			this.m_txtLow.Text = "";
			this.m_txtLow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtLow_KeyPress);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 336);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 19);
			this.label4.TabIndex = 64;
			this.label4.Text = "库存上限";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_txtHi
			// 
			this.m_txtHi.Location = new System.Drawing.Point(80, 334);
			this.m_txtHi.MaxLength = 6;
			this.m_txtHi.Name = "m_txtHi";
			this.m_txtHi.Size = new System.Drawing.Size(112, 23);
			this.m_txtHi.TabIndex = 6;
			this.m_txtHi.Text = "";
			this.m_txtHi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtHi_KeyPress);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(232, 258);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 19);
			this.label5.TabIndex = 66;
			this.label5.Text = "单位";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(232, 297);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 19);
			this.label6.TabIndex = 67;
			this.label6.Text = "采购数量";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(232, 336);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 19);
			this.label7.TabIndex = 68;
			this.label7.Text = "采购比例";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_txtQTY
			// 
			this.m_txtQTY.Location = new System.Drawing.Point(296, 295);
			this.m_txtQTY.MaxLength = 6;
			this.m_txtQTY.Name = "m_txtQTY";
			this.m_txtQTY.Size = new System.Drawing.Size(112, 23);
			this.m_txtQTY.TabIndex = 5;
			this.m_txtQTY.Text = "";
			this.m_txtQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtQTY_KeyPress);
			// 
			// m_cboUnit
			// 
			this.m_cboUnit.Location = new System.Drawing.Point(296, 256);
			this.m_cboUnit.Name = "m_cboUnit";
			this.m_cboUnit.Size = new System.Drawing.Size(112, 22);
			this.m_cboUnit.TabIndex = 3;
			// 
			// m_btnAdd
			// 
			this.m_btnAdd.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnAdd.DefaultScheme = true;
			this.m_btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnAdd.Hint = "";
			this.m_btnAdd.Location = new System.Drawing.Point(16, 384);
			this.m_btnAdd.Name = "m_btnAdd";
			this.m_btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnAdd.Size = new System.Drawing.Size(88, 32);
			this.m_btnAdd.TabIndex = 8;
			this.m_btnAdd.Text = "新增(&A)";
			this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
			// 
			// m_btnSave
			// 
			this.m_btnSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnSave.DefaultScheme = true;
			this.m_btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnSave.Hint = "";
			this.m_btnSave.Location = new System.Drawing.Point(144, 384);
			this.m_btnSave.Name = "m_btnSave";
			this.m_btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnSave.Size = new System.Drawing.Size(88, 32);
			this.m_btnSave.TabIndex = 9;
			this.m_btnSave.Text = "保存(&S)";
			this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
			// 
			// m_btnDel
			// 
			this.m_btnDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnDel.DefaultScheme = true;
			this.m_btnDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_btnDel.Hint = "";
			this.m_btnDel.Location = new System.Drawing.Point(272, 384);
			this.m_btnDel.Name = "m_btnDel";
			this.m_btnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnDel.Size = new System.Drawing.Size(88, 32);
			this.m_btnDel.TabIndex = 10;
			this.m_btnDel.Text = "删除(&D)";
			this.m_btnDel.Click += new System.EventHandler(this.m_btnDel_Click);
			// 
			// m_lvw
			// 
			this.m_lvw.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.m_lvw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					this.columnHeader1,
																					this.columnHeader2});
			this.m_lvw.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_lvw.FullRowSelect = true;
			this.m_lvw.GridLines = true;
			this.m_lvw.HideSelection = false;
			this.m_lvw.HoverSelection = true;
			this.m_lvw.Location = new System.Drawing.Point(328, 416);
			this.m_lvw.MultiSelect = false;
			this.m_lvw.Name = "m_lvw";
			this.m_lvw.Size = new System.Drawing.Size(192, 136);
			this.m_lvw.TabIndex = 75;
			this.m_lvw.View = System.Windows.Forms.View.Details;
			this.m_lvw.Visible = false;
			this.m_lvw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lvw_KeyDown);
			this.m_lvw.Click += new System.EventHandler(this.m_lvw_Click);
			this.m_lvw.Leave += new System.EventHandler(this.m_lvw_Leave);
			this.m_lvw.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvw_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "药品编号";
			this.columnHeader1.Width = 80;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "名称";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader2.Width = 111;
			// 
			// m_txtPer
			// 
			this.m_txtPer.Location = new System.Drawing.Point(296, 334);
			this.m_txtPer.Name = "m_txtPer";
			this.m_txtPer.Size = new System.Drawing.Size(48, 23);
			this.m_txtPer.TabIndex = 7;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(344, 336);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(12, 19);
			this.label8.TabIndex = 77;
			this.label8.Text = "%";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_btnExit
			// 
			this.m_btnExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_btnExit.DefaultScheme = true;
			this.m_btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnExit.Hint = "";
			this.m_btnExit.Location = new System.Drawing.Point(400, 384);
			this.m_btnExit.Name = "m_btnExit";
			this.m_btnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_btnExit.Size = new System.Drawing.Size(88, 32);
			this.m_btnExit.TabIndex = 78;
			this.m_btnExit.Text = "退出(&E)";
			this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
			// 
			// frmStorageMedLimitMgr
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.CancelButton = this.m_btnExit;
			this.ClientSize = new System.Drawing.Size(552, 445);
			this.Controls.Add(this.m_btnExit);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.m_txtPer);
			this.Controls.Add(this.m_lvw);
			this.Controls.Add(this.m_btnDel);
			this.Controls.Add(this.m_btnSave);
			this.Controls.Add(this.m_btnAdd);
			this.Controls.Add(this.m_cboUnit);
			this.Controls.Add(this.m_txtQTY);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.m_txtHi);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.m_txtLow);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.m_txtMed);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_cboStorage);
			this.Controls.Add(this.m_lvMed);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.MaximizeBox = false;
			this.Name = "frmStorageMedLimitMgr";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药库限额管理";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmStorageMedLimitMgr_KeyPress);
			this.Load += new System.EventHandler(this.frmStorageMedLimitMgr_Load);
			((System.ComponentModel.ISupportInitialize)(this.m_txtPer)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlStorageMedLimit();
			objController.Set_GUI_Apperance(this);
		}

		private void frmStorageMedLimitMgr_Load(object sender, System.EventArgs e)
		{
            ((clsControlStorageMedLimit)this.objController).m_mthClear();
			((clsControlStorageMedLimit)this.objController).FillUnit();
			((clsControlStorageMedLimit)this.objController).FillStorage();
			((clsControlStorageMedLimit)this.objController).GetMedicineList();
		}

		private void m_txtMed_TextChanged(object sender, System.EventArgs e)
		{
		   ((clsControlStorageMedLimit)this.objController).FindListByIDorName();
		}

		private void m_txtMed_Enter(object sender, System.EventArgs e)
		{
			this.m_lvw.Left=m_txtMed.Left;
			this.m_lvw.Top=m_txtMed.Top+m_txtMed.Height;
			this.m_lvw.Visible=true;
		}

		private void m_txtMed_Leave(object sender, System.EventArgs e)
		{
			if(this.ActiveControl.Name!="m_lvw")
			{
				this.m_lvw.Visible=false;
				if (m_txtMed.Text=="")
					return;
				long lngRes=((clsControlStorageMedLimit)this.objController).FillText(m_txtMed.Text);
				if(lngRes==100)
				{
					m_txtMed.Text="";
				}
			}   
		}

		private void m_lvw_Leave(object sender, System.EventArgs e)
		{
			if(this.ActiveControl.Name!="m_txtMed")
				this.m_lvw.Visible=false;
		}

		private void m_lvw_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageMedLimit)this.objController).blnListClick();
		}

		private void m_lvw_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			bool IsAsc=false;//是否为升序
			if(m_lvw.Sorting==SortOrder.Ascending)
				m_lvw.Sorting=SortOrder.Descending;
			else
			{
				m_lvw.Sorting=SortOrder.Ascending;
				IsAsc=true;
			}
			m_lvw.ListViewItemSorter=new ListViewItemComparer(e.Column,IsAsc,m_lvw);
			m_lvw.Sort();
		
		}

		private void m_lvMed_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			bool IsAsc=false;//是否为升序
			if(m_lvMed.Sorting==SortOrder.Ascending)
				m_lvMed.Sorting=SortOrder.Descending;
			else
			{
				m_lvMed.Sorting=SortOrder.Ascending;
				IsAsc=true;
			}
			m_lvMed.ListViewItemSorter=new ListViewItemComparer(e.Column,IsAsc,m_lvMed);
			m_lvMed.Sort();
		}

		private void m_lvMed_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_lvMed.SelectedItems.Count > 0)
			{
                m_txtMed.Tag=m_lvMed.SelectedItems[0].Text;
				((clsControlStorageMedLimit)this.objController).FillText(m_lvMed.SelectedItems[0].Text);
				//this.m_btnFind.Enabled=false;
			}
		}

		private void m_lvw_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==System.Windows.Forms.Keys.Enter)
				((clsControlStorageMedLimit)this.objController).blnListClick();
		}

		private void m_btnAdd_Click(object sender, System.EventArgs e)
		{
			if(this.m_cboStorage.SelectedIndex<0)
			{
				MessageBox.Show("请选择仓库！","提示");
				this.m_cboStorage.Focus();
			}
			else
			{
				((clsControlStorageMedLimit)this.objController).m_mthClear();
				this.m_txtMed.Focus();
			}
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			if(this.m_cboStorage.SelectedIndex<0)
			{
				MessageBox.Show("请选择仓库！","提示");
				this.m_cboStorage.Focus();
			}
			else
			((clsControlStorageMedLimit)this.objController).SaveRec();
		}

		private void m_btnDel_Click(object sender, System.EventArgs e)
		{
			if(this.m_cboStorage.SelectedIndex<0)
			{
				MessageBox.Show("请选择仓库！","提示");
				this.m_cboStorage.Focus();
			}
			else
			((clsControlStorageMedLimit)this.objController).m_lngDel();
		}

		private void m_cboStorage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		  ((clsControlStorageMedLimit)this.objController).m_GetList();
		}

		private void frmStorageMedLimitMgr_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=((e.KeyChar=="'".ToCharArray()[0])||(e.KeyChar==" ".ToCharArray()[0]));
		}

		private void m_txtLow_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=!clsPublicParm.ValNumer(e.KeyChar,m_txtLow.Text);
		}

		private void m_txtHi_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=!clsPublicParm.ValNumer(e.KeyChar,m_txtHi.Text);
		}

		private void m_txtQTY_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=!clsPublicParm.ValNumer(e.KeyChar,m_txtQTY.Text);
		}

		private void m_txtMed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Down)
			{
				this.m_lvw.Focus();
			}
		}

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	
	}
}
