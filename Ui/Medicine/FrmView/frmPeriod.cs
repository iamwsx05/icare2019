using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmPeriod 的摘要说明。
	/// </summary>
	public class frmPeriod : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		internal System.Windows.Forms.ListView m_lsvDetail;
		private System.Windows.Forms.ColumnHeader clnPeriodID;
		private System.Windows.Forms.ColumnHeader clnStartDate;
		private System.Windows.Forms.ColumnHeader clnEndDate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.DateTimePicker m_dtpStartDate;
		internal PinkieControls.ButtonXP btExit;
		internal PinkieControls.ButtonXP btOK;
		internal System.Windows.Forms.ComboBox m_cboYear;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPeriod()
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
			this.m_lsvDetail = new System.Windows.Forms.ListView();
			this.clnPeriodID = new System.Windows.Forms.ColumnHeader();
			this.clnStartDate = new System.Windows.Forms.ColumnHeader();
			this.clnEndDate = new System.Windows.Forms.ColumnHeader();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_cboYear = new System.Windows.Forms.ComboBox();
			this.btExit = new PinkieControls.ButtonXP();
			this.btOK = new PinkieControls.ButtonXP();
			this.m_dtpStartDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_lsvDetail
			// 
			this.m_lsvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.clnPeriodID,
																						  this.clnStartDate,
																						  this.clnEndDate});
			this.m_lsvDetail.FullRowSelect = true;
			this.m_lsvDetail.GridLines = true;
			this.m_lsvDetail.Location = new System.Drawing.Point(0, 136);
			this.m_lsvDetail.Name = "m_lsvDetail";
			this.m_lsvDetail.Size = new System.Drawing.Size(562, 296);
			this.m_lsvDetail.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.m_lsvDetail.TabIndex = 1;
			this.m_lsvDetail.View = System.Windows.Forms.View.Details;
			this.m_lsvDetail.DoubleClick += new System.EventHandler(this.m_lsvDetail_DoubleClick);
			// 
			// clnPeriodID
			// 
			this.clnPeriodID.Text = "帐务期";
			this.clnPeriodID.Width = 100;
			// 
			// clnStartDate
			// 
			this.clnStartDate.Text = "开始时间";
			this.clnStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clnStartDate.Width = 182;
			// 
			// clnEndDate
			// 
			this.clnEndDate.Text = "结束时间";
			this.clnEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.clnEndDate.Width = 193;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.m_cboYear);
			this.groupBox1.Controls.Add(this.btExit);
			this.groupBox1.Controls.Add(this.btOK);
			this.groupBox1.Controls.Add(this.m_dtpStartDate);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(562, 128);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// m_cboYear
			// 
			this.m_cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_cboYear.Items.AddRange(new object[] {
														   "1",
														   "2",
														   "3"});
			this.m_cboYear.Location = new System.Drawing.Point(88, 80);
			this.m_cboYear.Name = "m_cboYear";
			this.m_cboYear.Size = new System.Drawing.Size(152, 22);
			this.m_cboYear.TabIndex = 7;
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btExit.DefaultScheme = true;
			this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btExit.Hint = "";
			this.btExit.Location = new System.Drawing.Point(328, 73);
			this.btExit.Name = "btExit";
			this.btExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btExit.Size = new System.Drawing.Size(120, 32);
			this.btExit.TabIndex = 6;
			this.btExit.Text = "关闭(&ESC)";
			this.btExit.Click += new System.EventHandler(this.m_cmdClose_Click);
			// 
			// btOK
			// 
			this.btOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btOK.DefaultScheme = true;
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btOK.Hint = "";
			this.btOK.Location = new System.Drawing.Point(328, 27);
			this.btOK.Name = "btOK";
			this.btOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btOK.Size = new System.Drawing.Size(120, 32);
			this.btOK.TabIndex = 4;
			this.btOK.Text = "生成账务期(&S)";
			this.btOK.Click += new System.EventHandler(this.m_cmdConfirm_Click);
			// 
			// m_dtpStartDate
			// 
			this.m_dtpStartDate.Location = new System.Drawing.Point(88, 32);
			this.m_dtpStartDate.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
			this.m_dtpStartDate.MinDate = new System.DateTime(2004, 5, 1, 0, 0, 0, 0);
			this.m_dtpStartDate.Name = "m_dtpStartDate";
			this.m_dtpStartDate.Size = new System.Drawing.Size(152, 23);
			this.m_dtpStartDate.TabIndex = 2;
			this.m_dtpStartDate.ValueChanged += new System.EventHandler(this.m_dtpStartDate_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 19);
			this.label2.TabIndex = 1;
			this.label2.Text = "年   限:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "开始时间:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmPeriod
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.CancelButton = this.btExit;
			this.ClientSize = new System.Drawing.Size(562, 431);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.m_lsvDetail);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmPeriod";
			this.Text = "帐务期设置";
			this.Load += new System.EventHandler(this.frmPeriod_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void CreateController()
		{
			// TODO:  添加 frmStorageOrdType.CreateController 实现
			this.objController = new clsControlPeriod();
			this.objController.Set_GUI_Apperance(this);
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_lsvDetail_DoubleClick(object sender, System.EventArgs e)
		{
//			((clsControlPeriod)this.objController).m_mthModify();
		}

		private void m_dtpStartDate_ValueChanged(object sender, System.EventArgs e)
		{
//			((clsControlPeriod)this.objController).m_mthStartDateChange();
		}

		private void m_cmdConfirm_Click(object sender, System.EventArgs e)
		{
			((clsControlPeriod)this.objController).m_mthDoSave();
		}

		private void frmPeriod_Load(object sender, System.EventArgs e)
		{
			((clsControlPeriod)this.objController).m_mthGetStorageOrdTypeList();
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

	}
}
