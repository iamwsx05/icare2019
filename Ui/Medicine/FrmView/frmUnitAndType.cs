using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 单位、剂型、药品类型维护 Create by Sam 2004-5-24
	/// </summary>
	public class frmUnitAndType : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		private PinkieControls.ButtonXP m_btnNew;
		private PinkieControls.ButtonXP m_btnSave;
		internal System.Windows.Forms.TextBox m_txtName;
		internal System.Windows.Forms.ListView m_lvw;
		internal bool IsNew=true;
		internal byte IsType=0;
		private string OldID=null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox m_txtID;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private PinkieControls.ButtonXP m_btnDel;
		private PinkieControls.ButtonXP m_btnExit;
		private System.Windows.Forms.GroupBox groupBox1;
        private Label label3;
        private ColumnHeader columnHeader3;
        internal ComboBox m_cbDoseType;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmUnitAndType()
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
            this.m_lvw = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_txtName = new System.Windows.Forms.TextBox();
            this.m_btnNew = new PinkieControls.ButtonXP();
            this.m_btnSave = new PinkieControls.ButtonXP();
            this.m_txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnDel = new PinkieControls.ButtonXP();
            this.m_btnExit = new PinkieControls.ButtonXP();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cbDoseType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lvw
            // 
            this.m_lvw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.m_lvw.FullRowSelect = true;
            this.m_lvw.GridLines = true;
            this.m_lvw.Location = new System.Drawing.Point(0, 0);
            this.m_lvw.Name = "m_lvw";
            this.m_lvw.Size = new System.Drawing.Size(225, 304);
            this.m_lvw.TabIndex = 0;
            this.m_lvw.UseCompatibleStateImageBehavior = false;
            this.m_lvw.View = System.Windows.Forms.View.Details;
            this.m_lvw.SelectedIndexChanged += new System.EventHandler(this.m_lvw_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "编号";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // m_txtName
            // 
            this.m_txtName.Location = new System.Drawing.Point(275, 46);
            this.m_txtName.Name = "m_txtName";
            this.m_txtName.Size = new System.Drawing.Size(111, 23);
            this.m_txtName.TabIndex = 2;
            // 
            // m_btnNew
            // 
            this.m_btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnNew.DefaultScheme = true;
            this.m_btnNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnNew.Hint = "";
            this.m_btnNew.Location = new System.Drawing.Point(32, 23);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnNew.Size = new System.Drawing.Size(96, 32);
            this.m_btnNew.TabIndex = 3;
            this.m_btnNew.Text = "新增(&A)";
            this.m_btnNew.Click += new System.EventHandler(this.m_btnNew_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnSave.DefaultScheme = true;
            this.m_btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnSave.Hint = "";
            this.m_btnSave.Location = new System.Drawing.Point(32, 108);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnSave.Size = new System.Drawing.Size(96, 32);
            this.m_btnSave.TabIndex = 5;
            this.m_btnSave.Text = "保存(&S)";
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_txtID
            // 
            this.m_txtID.Location = new System.Drawing.Point(275, 10);
            this.m_txtID.MaxLength = 10;
            this.m_txtID.Name = "m_txtID";
            this.m_txtID.Size = new System.Drawing.Size(111, 23);
            this.m_txtID.TabIndex = 1;
            this.m_txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtID_KeyPress);
            this.m_txtID.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtID_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "编号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "名称";
            // 
            // m_btnDel
            // 
            this.m_btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnDel.DefaultScheme = true;
            this.m_btnDel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnDel.Hint = "";
            this.m_btnDel.Location = new System.Drawing.Point(32, 65);
            this.m_btnDel.Name = "m_btnDel";
            this.m_btnDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnDel.Size = new System.Drawing.Size(96, 32);
            this.m_btnDel.TabIndex = 4;
            this.m_btnDel.Text = "删除(&D)";
            this.m_btnDel.Click += new System.EventHandler(this.m_btnDel_Click);
            // 
            // m_btnExit
            // 
            this.m_btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnExit.DefaultScheme = true;
            this.m_btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnExit.Hint = "";
            this.m_btnExit.Location = new System.Drawing.Point(32, 148);
            this.m_btnExit.Name = "m_btnExit";
            this.m_btnExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnExit.Size = new System.Drawing.Size(96, 32);
            this.m_btnExit.TabIndex = 8;
            this.m_btnExit.Text = "退出(&E)";
            this.m_btnExit.Click += new System.EventHandler(this.m_btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_btnExit);
            this.groupBox1.Controls.Add(this.m_btnDel);
            this.groupBox1.Controls.Add(this.m_btnSave);
            this.groupBox1.Controls.Add(this.m_btnNew);
            this.groupBox1.Location = new System.Drawing.Point(250, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 201);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "剂型";
            this.label3.Visible = false;
            // 
            // m_cbDoseType
            // 
            this.m_cbDoseType.FormattingEnabled = true;
            this.m_cbDoseType.Items.AddRange(new object[] {
            "",
            "口服类",
            "针剂类"});
            this.m_cbDoseType.Location = new System.Drawing.Point(275, 79);
            this.m_cbDoseType.Name = "m_cbDoseType";
            this.m_cbDoseType.Size = new System.Drawing.Size(111, 22);
            this.m_cbDoseType.TabIndex = 11;
            this.m_cbDoseType.Visible = false;
            // 
            // frmUnitAndType
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(409, 309);
            this.Controls.Add(this.m_cbDoseType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtID);
            this.Controls.Add(this.m_txtName);
            this.Controls.Add(this.m_lvw);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "frmUnitAndType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单位维护";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmUnitAndType_KeyPress);
            this.Load += new System.EventHandler(this.frmUnitAndType_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsControlUnitAndType();
			objController.Set_GUI_Apperance(this);
		}
        //1-单位 2-药品类型 3-剂型
		public void ShowMe(string  sType)
		{
          
			switch (sType)
			{
				case "1":
					((clsControlUnitAndType)this.objController).GetUnit();
					this.Text="单位维护";
                    this.m_lvw.Columns[1].Width = 150;
                 
					this.m_txtID.MaxLength=10;
					this.m_txtName.MaxLength=20;
					this.IsType=1;
					break;
				case "2":
					((clsControlUnitAndType)this.objController).GetMedType();
					this.Text="药品类型维护";
                    this.m_lvw.Columns[1].Width = 150;
                
					this.m_txtID.MaxLength=7;
					this.m_txtName.MaxLength=100;
					this.IsType=2;
					break;
				case "3":
					((clsControlUnitAndType)this.objController).GetPrepType();
					this.Text="药品剂型维护";
                    columnHeader3 = new ColumnHeader();
                    columnHeader3.Text = "剂型";
                    this.m_lvw.Columns.Add(columnHeader3);
                    this.label3.Visible = true;
                    this.m_cbDoseType.Visible = true;
					this.m_txtID.MaxLength=7;
					this.m_txtName.MaxLength=20;
					this.IsType=3;
					break;
			}
			this.Show();
		}

		private void m_btnNew_Click(object sender, System.EventArgs e)
		{
			string strID=null;
			((clsControlUnitAndType)this.objController).GetItemMaxID(this.IsType,out strID);
			this.m_txtName.Text="";
            this.m_cbDoseType.Text = "";
			this.m_txtID.Text=strID;
			this.IsNew=true;
			this.OldID=null;
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			long lngRes=0;
			if(this.m_txtID.Text=="")
			{
				MessageBox.Show("请填入编码");
				return;
			}
			if(this.m_txtName.Text=="")
			{
				MessageBox.Show("请填入名称");
				return;
			}
			switch (this.IsType)
			{
				case 1: //单位
					lngRes=((clsControlUnitAndType)this.objController).SaveUnit(this.OldID);
					break;
				case 2://药品类型
					lngRes=((clsControlUnitAndType)this.objController).SaveMedType(this.OldID);
					break;
				case 3://剂型
					lngRes=((clsControlUnitAndType)this.objController).SavePrepType(this.OldID);
					break;
			}
			if(lngRes>0)
			{
				MessageBox.Show("保存成功");
				((clsControlUnitAndType)this.objController).RefreshData();
			}
			else
				MessageBox.Show("保存失败");
			
		}
         
		private void m_lvw_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_lvw.SelectedItems.Count > 0&&this.IsType==3)
			{
        
				this.m_txtID.Text=m_lvw.SelectedItems[0].Text;
				this.m_txtName.Text=m_lvw.SelectedItems[0].SubItems[1].Text;
                this.m_cbDoseType.Text = m_lvw.SelectedItems[0].SubItems[2].Text;
				this.OldID=m_txtID.Text;
				this.IsNew=false;
			}
            if (m_lvw.SelectedItems.Count > 0 && this.IsType != 3)
            {

                this.m_txtID.Text = m_lvw.SelectedItems[0].Text;
                this.m_txtName.Text = m_lvw.SelectedItems[0].SubItems[1].Text;
                this.OldID = m_txtID.Text;
                this.IsNew = false;
            }
                 
		}

		private void m_txtID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
          string ItemName="";
		  if(m_txtID.Text=="")
			  return;
			((clsControlUnitAndType)this.objController).GetItemName(m_txtID.Text,this.IsType,out ItemName);
            e.Cancel=((clsControlUnitAndType)this.objController).CheckItemID(m_txtID.Text,ItemName);               
		}

		private void m_txtID_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{		
			//e.Handled = !(char.IsDigit(e.KeyChar)||e.KeyChar=(char)8);			  
			e.Handled = !clsPublicParm.ValNumer(e.KeyChar,null);
		}

		private void frmUnitAndType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled=((e.KeyChar=="'".ToCharArray()[0])||(e.KeyChar==" ".ToCharArray()[0]));
		}

		private void m_btnDel_Click(object sender, System.EventArgs e)
		{
			long lngRes=((clsControlUnitAndType)this.objController).m_lngDelItem(this.IsType);
		}

		private void m_btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmUnitAndType_Load(object sender, System.EventArgs e)
		{
		
		}

	}
}
