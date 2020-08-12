using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmStroageSet 的摘要说明。
	/// </summary>
	public class frmStroageSet: com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.TextBox txtName;
		internal System.Windows.Forms.ComboBox cobType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		internal PinkieControls.ButtonXP btnSave;
		internal PinkieControls.ButtonXP buttonXP1;
		internal PinkieControls.ButtonXP buttonXP3;
		internal System.Windows.Forms.ListView StorageLisv;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		internal PinkieControls.ButtonXP buttonXP2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStroageSet()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonXP2 = new PinkieControls.ButtonXP();
			this.buttonXP3 = new PinkieControls.ButtonXP();
			this.buttonXP1 = new PinkieControls.ButtonXP();
			this.btnSave = new PinkieControls.ButtonXP();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cobType = new System.Windows.Forms.ComboBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.StorageLisv = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cobType);
			this.panel1.Controls.Add(this.txtName);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(264, 472);
			this.panel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonXP2);
			this.groupBox1.Controls.Add(this.buttonXP3);
			this.groupBox1.Controls.Add(this.buttonXP1);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Location = new System.Drawing.Point(48, 160);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 280);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			// 
			// buttonXP2
			// 
			this.buttonXP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP2.DefaultScheme = true;
			this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP2.Hint = "";
			this.buttonXP2.Location = new System.Drawing.Point(24, 32);
			this.buttonXP2.Name = "buttonXP2";
			this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP2.Size = new System.Drawing.Size(144, 40);
			this.buttonXP2.TabIndex = 9;
			this.buttonXP2.Text = "新增(&A)";
			this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
			// 
			// buttonXP3
			// 
			this.buttonXP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP3.DefaultScheme = true;
			this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP3.Hint = "";
			this.buttonXP3.Location = new System.Drawing.Point(24, 208);
			this.buttonXP3.Name = "buttonXP3";
			this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP3.Size = new System.Drawing.Size(144, 40);
			this.buttonXP3.TabIndex = 8;
			this.buttonXP3.Text = "退出(Esc)";
			this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
			// 
			// buttonXP1
			// 
			this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.buttonXP1.DefaultScheme = true;
			this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP1.Hint = "";
			this.buttonXP1.Location = new System.Drawing.Point(24, 144);
			this.buttonXP1.Name = "buttonXP1";
			this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP1.Size = new System.Drawing.Size(144, 40);
			this.buttonXP1.TabIndex = 6;
			this.buttonXP1.Text = "删除（&D）";
			this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnSave.DefaultScheme = true;
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnSave.Hint = "";
			this.btnSave.Location = new System.Drawing.Point(24, 88);
			this.btnSave.Name = "btnSave";
			this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnSave.Size = new System.Drawing.Size(144, 40);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "保存(&S)";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 96);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "仓库类别：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 48);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "仓库名称：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cobType
			// 
			this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cobType.ImeMode = System.Windows.Forms.ImeMode.On;
			this.cobType.Location = new System.Drawing.Point(104, 96);
			this.cobType.Name = "cobType";
			this.cobType.Size = new System.Drawing.Size(128, 22);
			this.cobType.TabIndex = 1;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(104, 48);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(128, 23);
			this.txtName.TabIndex = 0;
			this.txtName.Text = "";
			// 
			// StorageLisv
			// 
			this.StorageLisv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.StorageLisv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.columnHeader1,
																						  this.columnHeader2});
			this.StorageLisv.FullRowSelect = true;
			this.StorageLisv.GridLines = true;
			this.StorageLisv.Location = new System.Drawing.Point(280, 0);
			this.StorageLisv.Name = "StorageLisv";
			this.StorageLisv.Size = new System.Drawing.Size(376, 472);
			this.StorageLisv.TabIndex = 1;
			this.StorageLisv.View = System.Windows.Forms.View.Details;
			this.StorageLisv.Click += new System.EventHandler(this.StorageLisv_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "仓库名称";
			this.columnHeader1.Width = 195;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "仓库类型";
			this.columnHeader2.Width = 147;
			// 
			// frmStroageSet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(656, 477);
			this.Controls.Add(this.StorageLisv);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.KeyPreview = true;
			this.Name = "frmStroageSet";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "药库维护";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStroageSet_KeyDown);
			this.Load += new System.EventHandler(this.frmStroageSet_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 变量
		/// <summary>
		/// 
		/// </summary>
		clsDomainConrol_Medicne  objSvc=new clsDomainConrol_Medicne();
		/// <summary>
		/// 保存仓库类型数据
		/// </summary>
	    DataTable tbType=new DataTable();
		/// <summary>
		/// 保存仓库信息
		/// </summary>
		DataTable StorageTb=new DataTable();
		#endregion

		private void frmStroageSet_Load(object sender, System.EventArgs e)
		{
			long lngRes=objSvc.m_lngGetAllMedType(out tbType);
			if(tbType.Rows.Count>0)
			{
				for(int i1=0;i1<tbType.Rows.Count;i1++)
				{
					cobType.Items.Add(tbType.Rows[i1]["STORAGETYPENAME_VCHR"].ToString());
				}
				this.cobType.SelectedIndex=0;
			}
			
			lngRes=objSvc.m_lngGetAllstorage(out StorageTb);
			if(StorageTb.Rows.Count>0)
			{
				for(int i1=0;i1<StorageTb.Rows.Count;i1++)
				{
					ListViewItem newItem=new ListViewItem(StorageTb.Rows[i1]["STORAGENAME_VCHR"].ToString());
					newItem.SubItems.Add(StorageTb.Rows[i1]["STORAGETYPENAME_VCHR"].ToString());
					newItem.Tag=StorageTb.Rows[i1];
					StorageLisv.Items.Add(newItem);
				}
			}

		
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(cobType.Tag==null)
			{
				DataRow newRow=StorageTb.NewRow();
				newRow["STORAGENAME_VCHR"]=txtName.Text.Trim();
				newRow["STORAGETYPEID_CHR"]=tbType.Rows[cobType.SelectedIndex]["STORAGETYPEID_CHR"].ToString();
				newRow["STORAGETYPENAME_VCHR"]=cobType.Text;
				string newid="";
                long lngRes = objSvc.m_lngInsertStorageData(tbType.Rows[cobType.SelectedIndex]["STORAGETYPEID_CHR"].ToString(), txtName.Text.Trim(), out newid);
				if(lngRes==1)
				{
					newRow["STORAGEID_CHR"]=newid;
					ListViewItem newItem=new ListViewItem(newRow["STORAGENAME_VCHR"].ToString());
					newItem.SubItems.Add(newRow["STORAGETYPENAME_VCHR"].ToString());
					newItem.Tag=newRow;
					StorageLisv.Items.Add(newItem);
					
				}
			}
			else
			{
				DataRow newRow=StorageTb.NewRow();
				newRow["STORAGENAME_VCHR"]=txtName.Text.Trim();
				newRow["STORAGEID_CHR"]=(string)txtName.Tag;
				newRow["STORAGETYPEID_CHR"]=tbType.Rows[cobType.SelectedIndex]["STORAGETYPEID_CHR"].ToString();	
				newRow["STORAGETYPENAME_VCHR"]=cobType.Text;
                long lngRes = objSvc.m_lngModifyStorageData(tbType.Rows[cobType.SelectedIndex]["STORAGETYPEID_CHR"].ToString(), txtName.Text.Trim(), (string)txtName.Tag);
				if(lngRes==1)
				{
					StorageLisv.SelectedItems[0].SubItems[0].Text=newRow["STORAGENAME_VCHR"].ToString();
					StorageLisv.SelectedItems[0].SubItems[1].Text=newRow["STORAGETYPENAME_VCHR"].ToString();
					StorageLisv.SelectedItems[0].Tag=newRow;
					MessageBox.Show("修改成功","iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
			}
			txtName.Text="";
			cobType.Text="";
			txtName.Tag=null;
			cobType.Tag=null;
			txtName.Focus();
		}

		private void buttonXP3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			if(StorageLisv.SelectedItems.Count==1)
			{
				if(MessageBox.Show("确认删除该项吗?","iCare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.No)
				return;
				DataRow seleRow=(DataRow)StorageLisv.SelectedItems[0].Tag;
				string strID=seleRow["STORAGEID_CHR"].ToString();
				int index=StorageLisv.SelectedIndices[0];
				long lngRes=objSvc.m_lngDeleStorageData(strID);
				if(lngRes==1)
				{
					StorageLisv.SelectedItems[0].Remove();
				}
				if(StorageLisv.Items.Count>0)
				{
					StorageLisv.Focus();
					if(index>0)
						StorageLisv.Items[index-1].Selected=true;
					else
						StorageLisv.Items[index].Selected=true;
				}
				
			}
			else
			{
				MessageBox.Show("请选择一个药库","系统提示");
			}
		}
		
		private void StorageLisv_Click(object sender, System.EventArgs e)
		{
			if(StorageLisv.SelectedItems.Count==1)
			{
				DataRow seleRow=(DataRow)StorageLisv.SelectedItems[0].Tag;
				txtName.Tag=seleRow["STORAGEID_CHR"].ToString();
				txtName.Text=seleRow["STORAGENAME_VCHR"].ToString();
				cobType.Text=seleRow["STORAGETYPENAME_VCHR"].ToString();
				cobType.Tag=seleRow["STORAGETYPEID_CHR"].ToString();
			}
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			txtName.Text="";
			cobType.Text="";
			txtName.Tag=null;
			cobType.Tag=null;
			txtName.Focus();
		}

		private void frmStroageSet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				if(MessageBox.Show("确认退出吗?","iCare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
					return;
				buttonXP3_Click(sender,e);
			}   
		}
	}
}
