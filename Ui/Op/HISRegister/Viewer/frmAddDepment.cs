using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmAddDepment ��ժҪ˵����
	/// </summary>
	public class frmAddDepment : System.Windows.Forms.Form
	{
		internal PinkieControls.ButtonXP btAdd;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		internal PinkieControls.ButtonXP btOK;
		internal PinkieControls.ButtonXP btDel;
		internal com.digitalwave.Utility.ctlDeptTextBox ctlDepartment1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAddDepment()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.btAdd = new PinkieControls.ButtonXP();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.btOK = new PinkieControls.ButtonXP();
			this.btDel = new PinkieControls.ButtonXP();
			this.ctlDepartment1 = new com.digitalwave.Utility.ctlDeptTextBox();
			this.SuspendLayout();
			// 
			// btAdd
			// 
			this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btAdd.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btAdd.DefaultScheme = true;
			this.btAdd.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btAdd.Hint = "";
			this.btAdd.Location = new System.Drawing.Point(164, 4);
			this.btAdd.Name = "btAdd";
			this.btAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btAdd.Size = new System.Drawing.Size(72, 32);
			this.btAdd.TabIndex = 1;
			this.btAdd.Text = "���(&A)";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// listView1
			// 
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(8, 36);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(148, 256);
			this.listView1.TabIndex = 4;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "��������";
			this.columnHeader1.Width = 122;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 1;
			// 
			// btOK
			// 
			this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btOK.DefaultScheme = true;
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOK.Hint = "";
			this.btOK.Location = new System.Drawing.Point(168, 260);
			this.btOK.Name = "btOK";
			this.btOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btOK.Size = new System.Drawing.Size(72, 32);
			this.btOK.TabIndex = 3;
			this.btOK.Text = "ȷ��(&S)";
			// 
			// btDel
			// 
			this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btDel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btDel.DefaultScheme = true;
			this.btDel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btDel.Hint = "";
			this.btDel.Location = new System.Drawing.Point(168, 212);
			this.btDel.Name = "btDel";
			this.btDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btDel.Size = new System.Drawing.Size(72, 32);
			this.btDel.TabIndex = 2;
			this.btDel.Text = "ɾ��(&D)";
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			// 
			// ctlDepartment1
			// 
			this.ctlDepartment1.CausesValidation = false;
			//this.ctlDepartment1.EnableAutoValidation = false;
			//this.ctlDepartment1.EnableEnterKeyValidate = false;
			//this.ctlDepartment1.EnableEscapeKeyUndo = true;
			//this.ctlDepartment1.EnableLastValidValue = true;
			//this.ctlDepartment1.ErrorProvider = null;
			//this.ctlDepartment1.ErrorProviderMessage = "Invalid value";
			this.ctlDepartment1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			//this.ctlDepartment1.ForceFormatText = true;
			this.ctlDepartment1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ctlDepartment1.Location = new System.Drawing.Point(8, 8);
			this.ctlDepartment1.m_StrDeptID = null;
			this.ctlDepartment1.m_StrDeptName = null;
			this.ctlDepartment1.Name = "ctlDepartment1";
			this.ctlDepartment1.SetDepartment = com.digitalwave.Utility.ctlDeptTextBox.eDeptArea.All;
			this.ctlDepartment1.Size = new System.Drawing.Size(148, 23);
			this.ctlDepartment1.TabIndex = 0;
			this.ctlDepartment1.Text = "";
			// 
			// frmAddDepment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(248, 300);
			this.Controls.Add(this.ctlDepartment1);
			this.Controls.Add(this.btDel);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btAdd);
			this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "frmAddDepment";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ѡ�����";
			this.ResumeLayout(false);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			string strdepID =this.ctlDepartment1.m_StrDeptID.Trim();
			if(strdepID=="")
			{
			return;
			}
			for(int i =0;i<this.listView1.Items.Count;i++)
			{
				if(strdepID ==this.listView1.Items[i].SubItems[1].Text)
				{
					MessageBox.Show("�����Ѿ�����!");
					return;
				}
			}
			ListViewItem lv=new ListViewItem(this.ctlDepartment1.m_StrDeptName);
			lv.SubItems.Add(strdepID);
			this.listView1.Items.Add(lv);
			this.ctlDepartment1.Text="";
			this.ctlDepartment1.Focus();
			
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			if(this.listView1.SelectedItems.Count>0)
			{
			this.listView1.SelectedItems[0].Remove();
			}
		}
		private System.Data.DataTable dt;
		public System.Data.DataTable DeparmentData
		{
//			dtDepement.Columns.Add("RECIPEID_CHR");
//		dtDepement.Columns.Add("DEPTID_CHR");
//		dtDepement.Columns.Add("DEPTNAME_VCHR");
			set
			{
				dt =value;
				for(int i =0;i<dt.Rows.Count;i++)
				{
					ListViewItem lv=new ListViewItem(dt.Rows[i]["DEPTNAME_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["DEPTID_CHR"].ToString().Trim());
					this.listView1.Items.Add(lv);
				}
			}
			get
			{
				dt.Rows.Clear();
				for(int i=0;i<this.listView1.Items.Count;i++)
				{
				System.Data.DataRow dr =dt.NewRow();
				dr["DEPTNAME_VCHR"]=this.listView1.Items[i].SubItems[0].Text;
				dr["DEPTID_CHR"]=this.listView1.Items[i].SubItems[1].Text;
				this.dt.Rows.Add(dr);
				}
				return dt;
			}
		}
		
	}
}
