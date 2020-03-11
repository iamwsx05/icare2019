using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmDocList 的摘要说明。
	/// </summary>
	public class frmDocList2 : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.CheckBox checkBox1;
		internal PinkieControls.ButtonXP btOK;
		internal PinkieControls.ButtonXP btExit;
		internal System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		internal System.Windows.Forms.ComboBox cmbDep;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		clsDcl_WaitDiagListManage objSvc =null;
		public frmDocList2()
		{

			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			objSvc =new clsDcl_WaitDiagListManage();
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		#region 自定义属性
		private ArrayList _objDepArr;
		/// <summary>
		/// 部门ID
		/// </summary>
		public ArrayList objDepArr
		{
			get
			{
				return _objDepArr;
			}
			set
			{
				_objDepArr=value;
			}
		}
		private string strListID;
		/// <summary>
		/// 获取设置候诊ID
		/// </summary>
		public string ListID
		{
			get
			{
				return strListID;
			}
			set
			{
				this.strListID=value;
			}
		}
		private string strDocID="";
		/// <summary>
		/// 获取设置医生ID
		/// </summary>
		public string DoctorID
		{
			get
			{
				return strDocID;
			}
			set
			{
				this.strDocID=value;
			}
		}
		#endregion
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
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btOK = new PinkieControls.ButtonXP();
			this.btExit = new PinkieControls.ButtonXP();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.cmbDep = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(184, 176);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 36);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "优先";
			// 
			// btOK
			// 
			this.btOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btOK.DefaultScheme = true;
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btOK.Hint = "";
			this.btOK.Location = new System.Drawing.Point(180, 224);
			this.btOK.Name = "btOK";
			this.btOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btOK.Size = new System.Drawing.Size(80, 32);
			this.btOK.TabIndex = 2;
			this.btOK.Text = "确定";
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btExit.DefaultScheme = true;
			this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btExit.Hint = "";
			this.btExit.Location = new System.Drawing.Point(180, 280);
			this.btExit.Name = "btExit";
			this.btExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btExit.Size = new System.Drawing.Size(80, 32);
			this.btExit.TabIndex = 3;
			this.btExit.Text = "退出";
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader1,
																						this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(8, 28);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(166, 288);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "工号";
			this.columnHeader6.Width = 49;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "医生姓名";
			this.columnHeader1.Width = 95;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "ID";
			this.columnHeader7.Width = 0;
			// 
			// cmbDep
			// 
			this.cmbDep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDep.Location = new System.Drawing.Point(8, 4);
			this.cmbDep.Name = "cmbDep";
			this.cmbDep.Size = new System.Drawing.Size(168, 20);
			this.cmbDep.TabIndex = 15;
			this.cmbDep.SelectedIndexChanged += new System.EventHandler(this.cmbDep_SelectedIndexChanged);
			// 
			// frmDocList2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btExit;
			this.ClientSize = new System.Drawing.Size(270, 331);
			this.Controls.Add(this.cmbDep);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btExit);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.checkBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmDocList2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "医生列表";
			this.Load += new System.EventHandler(this.frmDocList2_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btOK_Click(object sender, System.EventArgs e)
		{
			if(this.listView1.SelectedItems.Count<1)
			{
				return ;
			}
			if(this.listView1.SelectedItems[0].SubItems[2].Text.Trim()==this.strDocID.Trim())
			{
			return;
			}
			long l =objSvc.m_mthChangeDoc(this.cmbDep.Tag.ToString(),this.listView1.SelectedItems[0].SubItems[2].Text.Trim(),this.ListID);
			if(l>0&&this.checkBox1.Checked)
			{
				l= objSvc.m_mthPrecedence(this.listView1.SelectedItems[0].SubItems[2].Text.Trim(),this.cmbDep.Tag.ToString(),1000,this.ListID);
			}
			if(l>0)
			{
				this.DialogResult=DialogResult.OK;
			}
			else
			{
				MessageBox.Show("保存失败!");
			}
			this.Close();
		}

		private void frmDocList2_Load(object sender, System.EventArgs e)
		{
			if(this.cmbDep.Items.Count>0)
			{
				this.cmbDep.SelectedIndex =0;
			}
			else
			{
			this.cmbDep.Tag ="";
			}
		}

		private void cmbDep_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbDep.SelectedIndex<0)
			{
				return;
			}
			this.cmbDep.Tag=this._objDepArr[this.cmbDep.SelectedIndex].ToString();	
			DataTable dt;
			long l =objSvc.m_mthGetDocByDepID(this.cmbDep.Tag.ToString(),out dt);
			this.listView1.Items.Clear();
			if(l>0&&dt.Rows.Count>0)
			{
				ListViewItem lv;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					lv=new ListViewItem(dt.Rows[i]["EMPNO_CHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["LASTNAME_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["EMPID_CHR"].ToString().Trim());
					this.listView1.Items.Add(lv);
					
				}
				this.listView1.Items[0].Selected=true;
				this.listView1.Focus();
			}
		}

		private void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode ==Keys.Enter)
			{
			this.btOK_Click(null,null);
			}
		}
	}
}
