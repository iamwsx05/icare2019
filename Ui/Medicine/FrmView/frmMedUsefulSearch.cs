using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmMedUsefulSearch 的摘要说明。
	/// </summary>
	public class frmMedUsefulSearch : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lable2;
		internal System.Windows.Forms.ComboBox m_cmbDate;
		private PinkieControls.ButtonXP m_BtnSearch;
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer m_crvUsefull;
		internal System.Windows.Forms.ComboBox m_cmbStorage;
		private System.Windows.Forms.Panel panel1;
		private PinkieControls.ButtonXP buttonXP1;
		private System.Windows.Forms.Panel panel2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMedUsefulSearch()
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
		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.HIS.clsControlMedUseful();
			this.objController.Set_GUI_Apperance(this);
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.m_cmbStorage = new System.Windows.Forms.ComboBox();
            this.m_BtnSearch = new PinkieControls.ButtonXP();
            this.m_cmbDate = new System.Windows.Forms.ComboBox();
            this.lable2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            //this.m_crvUsefull = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmbStorage
            // 
            this.m_cmbStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbStorage.Location = new System.Drawing.Point(112, 16);
            this.m_cmbStorage.Name = "m_cmbStorage";
            this.m_cmbStorage.Size = new System.Drawing.Size(152, 22);
            this.m_cmbStorage.TabIndex = 56;
            // 
            // m_BtnSearch
            // 
            this.m_BtnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnSearch.DefaultScheme = true;
            this.m_BtnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnSearch.Hint = "";
            this.m_BtnSearch.Location = new System.Drawing.Point(536, 8);
            this.m_BtnSearch.Name = "m_BtnSearch";
            this.m_BtnSearch.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnSearch.Size = new System.Drawing.Size(88, 32);
            this.m_BtnSearch.TabIndex = 55;
            this.m_BtnSearch.Text = "查询(&S)";
            this.m_BtnSearch.Click += new System.EventHandler(this.m_BtnSearch_Click);
            // 
            // m_cmbDate
            // 
            this.m_cmbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbDate.Location = new System.Drawing.Point(336, 16);
            this.m_cmbDate.Name = "m_cmbDate";
            this.m_cmbDate.Size = new System.Drawing.Size(121, 22);
            this.m_cmbDate.TabIndex = 3;
            // 
            // lable2
            // 
            this.lable2.Location = new System.Drawing.Point(272, 16);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(64, 16);
            this.lable2.TabIndex = 2;
            this.lable2.Text = "有效时间";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓    库";
            // 
            // m_crvUsefull
            // 
            //this.m_crvUsefull.ActiveViewIndex = -1;
            //this.m_crvUsefull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.m_crvUsefull.DisplayGroupTree = false;
            //this.m_crvUsefull.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.m_crvUsefull.Location = new System.Drawing.Point(0, 0);
            //this.m_crvUsefull.Name = "m_crvUsefull";
            //this.m_crvUsefull.SelectionFormula = "";
            //this.m_crvUsefull.Size = new System.Drawing.Size(926, 470);
            //this.m_crvUsefull.TabIndex = 0;
            //this.m_crvUsefull.ViewTimeSelectionFormula = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonXP1);
            this.panel1.Controls.Add(this.lable2);
            this.panel1.Controls.Add(this.m_cmbStorage);
            this.panel1.Controls.Add(this.m_BtnSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_cmbDate);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 48);
            this.panel1.TabIndex = 2;
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(672, 8);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(88, 32);
            this.buttonXP1.TabIndex = 57;
            this.buttonXP1.Text = "退出(&E)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.panel2.Controls.Add(this.m_crvUsefull);
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 472);
            this.panel2.TabIndex = 3;
            // 
            // frmMedUsefulSearch
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(928, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "frmMedUsefulSearch";
            this.Text = "有效期预警统计报表";
            this.Load += new System.EventHandler(this.frmMedUsefulSearch_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_BtnSearch_Click(object sender, System.EventArgs e)
		{
			((clsControlMedUseful)this.objController).m_mthSearch();
		}

		private void frmMedUsefulSearch_Load(object sender, System.EventArgs e)
		{
			((clsControlMedUseful)this.objController).m_mthInitFrm();
			((clsControlMedUseful)this.objController).m_mthSearch();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}
}
