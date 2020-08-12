using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PinkieControls;
namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmLisDeviceManage 的摘要说明。
	/// </summary>
	public class frmRISEEGReportNamage : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		#region Define
		internal System.Windows.Forms.TabControl m_tbcMain;
		internal System.Windows.Forms.TabPage tabPage1;
		internal System.Windows.Forms.ColumnHeader columnHeader1;
		internal System.Windows.Forms.ColumnHeader columnHeader2;
		internal System.Windows.Forms.ColumnHeader columnHeader3;
		internal System.Windows.Forms.ColumnHeader columnHeader4;
		internal System.Windows.Forms.ColumnHeader columnHeader5;
		internal System.Windows.Forms.ColumnHeader columnHeader6;
		internal System.Windows.Forms.ColumnHeader columnHeader7;
		internal System.Windows.Forms.ColumnHeader columnHeader8;
		internal System.Windows.Forms.ColumnHeader columnHeader9;
		internal PinkieControls.ButtonXP m_cmdAddNew;
		internal System.Windows.Forms.ListView m_lsvTCDReportList;
		internal PinkieControls.ButtonXP m_cmdRefresh;
		#endregion
		internal System.Windows.Forms.TabPage tabPage2;

		internal System.Windows.Forms.ColumnHeader columnHeader10;
		internal System.Windows.Forms.ColumnHeader columnHeader11;
		internal System.Windows.Forms.ColumnHeader columnHeader12;
		internal System.Windows.Forms.ColumnHeader columnHeader13;
		internal System.Windows.Forms.ColumnHeader columnHeader14;
		internal System.Windows.Forms.ColumnHeader columnHeader15;
		internal System.Windows.Forms.ColumnHeader columnHeader16;
		internal System.Windows.Forms.ColumnHeader columnHeader17;
		internal System.Windows.Forms.ColumnHeader columnHeader18;
		internal System.Windows.Forms.ListView m_lsvEEGReportList;
		private PinkieControls.ButtonXP m_cmdAddNewEEG;
		private PinkieControls.ButtonXP buttonXP1;
		private PinkieControls.ButtonXP cmdReport;
		private PinkieControls.ButtonXP m_cmdQuit;
		private System.Windows.Forms.TabPage tabPage3;
		internal System.Windows.Forms.ListView lisvAcct;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private PinkieControls.ButtonXP m_cmdApplyForm;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmRISEEGReportNamage()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
        #region 刷新列表查询条件
        public static string strOPQueryButtonName = "当天";

        public string strFromDat1 = "";
        public string strToDat1 = "";
        public string strPatientNo1 = "";
        public string strInPatientNo1 = "";
        public string strPatientName1 = "";
        public string strDept1 = "";
        public string strReportNo1 = "";
        public string strReporter1 = "";

        public string strFromDat2 = "";
        public string strToDat2 = "";
        public string strPatientNo2 = "";
        public string strInPatientNo2 = "";
        public string strPatientName2 = "";
        public string strDept2 = "";
        public string strReportNo2 = "";
        public string strReporter2 = "";
        #endregion
        /// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRISEEGReportNamage));
            this.m_tbcMain = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lisvAcct = new System.Windows.Forms.ListView();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_lsvTCDReportList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_lsvEEGReportList = new System.Windows.Forms.ListView();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.m_cmdAddNew = new PinkieControls.ButtonXP();
            this.m_cmdRefresh = new PinkieControls.ButtonXP();
            this.m_cmdAddNewEEG = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.cmdReport = new PinkieControls.ButtonXP();
            this.m_cmdQuit = new PinkieControls.ButtonXP();
            this.m_cmdApplyForm = new PinkieControls.ButtonXP();
            this.m_tbcMain.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tbcMain
            // 
            this.m_tbcMain.Controls.Add(this.tabPage3);
            this.m_tbcMain.Controls.Add(this.tabPage1);
            this.m_tbcMain.Controls.Add(this.tabPage2);
            this.m_tbcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_tbcMain.Location = new System.Drawing.Point(0, 0);
            this.m_tbcMain.Name = "m_tbcMain";
            this.m_tbcMain.SelectedIndex = 0;
            this.m_tbcMain.Size = new System.Drawing.Size(1016, 588);
            this.m_tbcMain.TabIndex = 0;
            this.m_tbcMain.DoubleClick += new System.EventHandler(this.m_lsvEEGReportList_DoubleClick);
            this.m_tbcMain.SelectedIndexChanged += new System.EventHandler(this.m_tbcMain_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lisvAcct);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1008, 561);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "申请单";
            // 
            // lisvAcct
            // 
            this.lisvAcct.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.columnHeader30,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader27,
            this.columnHeader28,
            this.columnHeader29});
            this.lisvAcct.FullRowSelect = true;
            this.lisvAcct.GridLines = true;
            this.lisvAcct.Location = new System.Drawing.Point(2, 8);
            this.lisvAcct.Name = "lisvAcct";
            this.lisvAcct.Size = new System.Drawing.Size(1000, 552);
            this.lisvAcct.TabIndex = 1;
            this.lisvAcct.UseCompatibleStateImageBehavior = false;
            this.lisvAcct.View = System.Windows.Forms.View.Details;
            this.lisvAcct.DoubleClick += new System.EventHandler(this.lisvAcct_DoubleClick);
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "申请单类型";
            this.columnHeader19.Width = 150;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "缴费状态";
            this.columnHeader30.Width = 75;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "申请日期";
            this.columnHeader20.Width = 150;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "病人卡号";
            this.columnHeader21.Width = 120;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "科别";
            this.columnHeader22.Width = 120;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "床号";
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "年龄";
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "性别";
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "姓名";
            this.columnHeader26.Width = 120;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "住院号";
            this.columnHeader27.Width = 80;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "门诊号";
            this.columnHeader28.Width = 80;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "前次脑电图号";
            this.columnHeader29.Width = 120;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lsvTCDReportList);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1008, 561);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = " TCD";
            // 
            // m_lsvTCDReportList
            // 
            this.m_lsvTCDReportList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader9,
            this.columnHeader6,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8});
            this.m_lsvTCDReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvTCDReportList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvTCDReportList.FullRowSelect = true;
            this.m_lsvTCDReportList.GridLines = true;
            this.m_lsvTCDReportList.Location = new System.Drawing.Point(0, 0);
            this.m_lsvTCDReportList.Name = "m_lsvTCDReportList";
            this.m_lsvTCDReportList.Size = new System.Drawing.Size(1008, 561);
            this.m_lsvTCDReportList.TabIndex = 0;
            this.m_lsvTCDReportList.UseCompatibleStateImageBehavior = false;
            this.m_lsvTCDReportList.View = System.Windows.Forms.View.Details;
            this.m_lsvTCDReportList.DoubleClick += new System.EventHandler(this.m_lsvTCDReportList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "TCD号";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "性别";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "年龄";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "门诊号";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "住院号";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "科室";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "检查日期";
            this.columnHeader7.Width = 180;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "报告日期";
            this.columnHeader8.Width = 180;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_lsvEEGReportList);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1008, 561);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = " EEG";
            // 
            // m_lsvEEGReportList
            // 
            this.m_lsvEEGReportList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
            this.m_lsvEEGReportList.Font = new System.Drawing.Font("宋体", 12F);
            this.m_lsvEEGReportList.FullRowSelect = true;
            this.m_lsvEEGReportList.GridLines = true;
            this.m_lsvEEGReportList.HideSelection = false;
            this.m_lsvEEGReportList.Location = new System.Drawing.Point(0, 0);
            this.m_lsvEEGReportList.Name = "m_lsvEEGReportList";
            this.m_lsvEEGReportList.Size = new System.Drawing.Size(1008, 568);
            this.m_lsvEEGReportList.TabIndex = 0;
            this.m_lsvEEGReportList.UseCompatibleStateImageBehavior = false;
            this.m_lsvEEGReportList.View = System.Windows.Forms.View.Details;
            this.m_lsvEEGReportList.DoubleClick += new System.EventHandler(this.m_lsvEEGReportList_DoubleClick);
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "脑电图号";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "姓名";
            this.columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "性别";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "年龄";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "门诊号";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "住院号";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "科室";
            this.columnHeader16.Width = 100;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "检查日期";
            this.columnHeader17.Width = 170;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "报告日期";
            this.columnHeader18.Width = 170;
            // 
            // m_cmdAddNew
            // 
            this.m_cmdAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdAddNew.DefaultScheme = true;
            this.m_cmdAddNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddNew.Hint = "";
            this.m_cmdAddNew.Location = new System.Drawing.Point(560, 596);
            this.m_cmdAddNew.Name = "m_cmdAddNew";
            this.m_cmdAddNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddNew.Size = new System.Drawing.Size(132, 28);
            this.m_cmdAddNew.TabIndex = 1;
            this.m_cmdAddNew.Text = "添加TCD报告单(&M)";
            this.m_cmdAddNew.Click += new System.EventHandler(this.m_cmdAddNew_Click);
            // 
            // m_cmdRefresh
            // 
            this.m_cmdRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdRefresh.DefaultScheme = true;
            this.m_cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdRefresh.Hint = "";
            this.m_cmdRefresh.Location = new System.Drawing.Point(16, 596);
            this.m_cmdRefresh.Name = "m_cmdRefresh";
            this.m_cmdRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdRefresh.Size = new System.Drawing.Size(120, 28);
            this.m_cmdRefresh.TabIndex = 4;
            this.m_cmdRefresh.Text = "刷新(&Q)";
            this.m_cmdRefresh.Click += new System.EventHandler(this.m_cmdRefresh_Click);
            // 
            // m_cmdAddNewEEG
            // 
            this.m_cmdAddNewEEG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdAddNewEEG.DefaultScheme = true;
            this.m_cmdAddNewEEG.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddNewEEG.Hint = "";
            this.m_cmdAddNewEEG.Location = new System.Drawing.Point(708, 596);
            this.m_cmdAddNewEEG.Name = "m_cmdAddNewEEG";
            this.m_cmdAddNewEEG.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddNewEEG.Size = new System.Drawing.Size(132, 28);
            this.m_cmdAddNewEEG.TabIndex = 6;
            this.m_cmdAddNewEEG.Text = "添加EEG报告单(&B)";
            this.m_cmdAddNewEEG.Click += new System.EventHandler(this.m_cmdAddNewEEG_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(152, 596);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(120, 28);
            this.buttonXP1.TabIndex = 7;
            this.buttonXP1.Text = "查询(&F)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // cmdReport
            // 
            this.cmdReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cmdReport.DefaultScheme = true;
            this.cmdReport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdReport.Hint = "";
            this.cmdReport.Location = new System.Drawing.Point(288, 596);
            this.cmdReport.Name = "cmdReport";
            this.cmdReport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdReport.Size = new System.Drawing.Size(120, 28);
            this.cmdReport.TabIndex = 8;
            this.cmdReport.Text = "报表(&P)";
            this.cmdReport.Click += new System.EventHandler(this.cmdReport_Click);
            // 
            // m_cmdQuit
            // 
            this.m_cmdQuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdQuit.DefaultScheme = true;
            this.m_cmdQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdQuit.Hint = "";
            this.m_cmdQuit.Location = new System.Drawing.Point(856, 596);
            this.m_cmdQuit.Name = "m_cmdQuit";
            this.m_cmdQuit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdQuit.Size = new System.Drawing.Size(120, 28);
            this.m_cmdQuit.TabIndex = 9;
            this.m_cmdQuit.Text = "退出(&ESC)";
            this.m_cmdQuit.Click += new System.EventHandler(this.m_cmdQuit_Click);
            // 
            // m_cmdApplyForm
            // 
            this.m_cmdApplyForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdApplyForm.DefaultScheme = true;
            this.m_cmdApplyForm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdApplyForm.Hint = "";
            this.m_cmdApplyForm.Location = new System.Drawing.Point(424, 596);
            this.m_cmdApplyForm.Name = "m_cmdApplyForm";
            this.m_cmdApplyForm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdApplyForm.Size = new System.Drawing.Size(120, 28);
            this.m_cmdApplyForm.TabIndex = 11;
            this.m_cmdApplyForm.Text = "申请单(&A)";
            this.m_cmdApplyForm.Click += new System.EventHandler(this.m_cmdApplyForm_Click);
            // 
            // frmRISEEGReportNamage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.CancelButton = this.m_cmdQuit;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.m_cmdApplyForm);
            this.Controls.Add(this.m_cmdQuit);
            this.Controls.Add(this.cmdReport);
            this.Controls.Add(this.buttonXP1);
            this.Controls.Add(this.m_cmdAddNewEEG);
            this.Controls.Add(this.m_cmdRefresh);
            this.Controls.Add(this.m_cmdAddNew);
            this.Controls.Add(this.m_tbcMain);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRISEEGReportNamage";
            this.Text = "脑电图室管理";
            this.Load += new System.EventHandler(this.frmCardiogramReportManage_Load);
            this.m_tbcMain.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmRISEEGReportNamage());
		}

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.RIS.clsController_RISEEGManage();
			objController.Set_GUI_Apperance(this);
		}

		private void frmCardiogramReportManage_Load(object sender, System.EventArgs e)
		{
			((clsController_RISEEGManage)this.objController).m_mthGetTCDReportArr();
			((clsController_RISEEGManage)this.objController).m_mthGetEEGReportArr();
			((clsController_RISEEGManage)this.objController).m_lngGetAcctData(false);
		}

		private void m_cmdAddNew_Click(object sender, System.EventArgs e)
		{
			if(m_tbcMain.SelectedIndex!=0||lisvAcct.SelectedItems.Count <=0)
			{
				frmRISTCDReport m_objViewer = new frmRISTCDReport();
				m_objViewer.LoginInfo=this.LoginInfo;
				m_objViewer.m_mthSetParentApperance(this);
				m_objViewer.m_cmdDelete.Enabled = false;
				m_objViewer.m_cmdConfirm.Enabled = false;
				m_objViewer.m_cmdPrint.Enabled = false;
				m_objViewer.ShowDialog();
			}
			else
				((clsController_RISEEGManage)this.objController).m_mthShowCardiogramReportAddNew(this);
		
			
		}

		private void m_cmdAddNewDnm_Click(object sender, System.EventArgs e)
		{
//			frmDnmCardiogramReport m_objViewer = new frmDnmCardiogramReport();
//			m_objViewer.SetParentApperance(this);
//			m_objViewer.m_cmdDelete.Enabled = false;
//			m_objViewer.m_cmdConfirm.Enabled = false;
//			m_objViewer.m_cmdPrint.Enabled = false;
//			m_objViewer.ShowDialog();
		}

		public  void m_cmdRefresh_Click(object sender, System.EventArgs e)
		{
            frmRISEEGReportNamage.strOPQueryButtonName = "当天";
            this.lisvAcct.Items.Clear();
            int index = m_tbcMain.SelectedIndex;
            if (index ==0)
            {
                ((clsController_RISEEGManage)this.objController).m_lngGetAcctData(true);                
            }
            if (index == 1)
            {
                ((clsController_RISEEGManage)this.objController).m_mthGetTCDReportArr();
                
            }
            if (index  == 2)
            {
                ((clsController_RISEEGManage)this.objController).m_mthGetEEGReportArr();                
            }
			

		}

		private void m_lsvTCDReportList_DoubleClick(object sender, System.EventArgs e)
		{
			((clsController_RISEEGManage)this.objController).m_mthShowCardiogramReport(this);
		}

		private void m_lsvDCardiogramReportList_DoubleClick(object sender, System.EventArgs e)
		{
//			((clsController_RISCardiogramManage)this.objController).m_mthShowDCardiogramReport(this);
		}

		private void m_cmdAddNewEEG_Click(object sender, System.EventArgs e)
		{
			if(m_tbcMain.SelectedIndex!=0||lisvAcct.SelectedItems.Count <=0)
			{
				frmRISEEGReport m_objViewer = new frmRISEEGReport();
				m_objViewer.LoginInfo=this.LoginInfo;
				m_objViewer.m_mthSetParentApperance(this);
				m_objViewer.m_cmdDelete.Enabled = false;
				m_objViewer.m_cmdConfirm.Enabled = false;
				m_objViewer.m_cmdPrint.Enabled = false;
				m_objViewer.ShowDialog();
			}
			else
				((clsController_RISEEGManage)this.objController).m_mthShowRISEEGReportAddNew(this);
		
		}

		private void m_lsvEEGReportList_DoubleClick(object sender, System.EventArgs e)
		{
		   ((clsController_RISEEGManage)this.objController).m_mthShowEEGCardiogramReport(this);
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
		  frmQueryEEGReport m_objViewer = new frmQueryEEGReport();
		  m_objViewer.m_mthGetParentApperance(this);
		  m_objViewer.ShowDialog();
		  
		}

		private void cmdReport_Click(object sender, System.EventArgs e)
		{
			frmRISrepot frmrpt=new frmRISrepot();
			frmrpt.ShowDialog();
		}

		private void m_cmdQuit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void lisvAcct_DoubleClick(object sender, System.EventArgs e)
		{
			clsController_RISCardiogramManage cls = new  clsController_RISCardiogramManage();
			cls.m_mthOpenHeartApplyFormByApplyId(lisvAcct);	
		}

		private void m_cmdApplyForm_Click(object sender, System.EventArgs e)
		{
			clsController_RISCardiogramManage cls = new  clsController_RISCardiogramManage();
			cls.m_mthOpenHeartApplyFormByApplyId(lisvAcct);		
		}

		private void m_tbcMain_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.m_tbcMain.SelectedIndex == 0)
				m_cmdApplyForm.Enabled = true;
			else
				m_cmdApplyForm.Enabled = false;
		}

	}
}
