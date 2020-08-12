using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PinkieControls;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// frmLisDeviceManage 的摘要说明。
	/// </summary>
	public class frmCardiogramReportManage : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		internal System.Windows.Forms.TabControl m_tbcMain;
		internal System.Windows.Forms.TabPage tabPage1;
		internal System.Windows.Forms.TabPage tabPage3;
		internal System.Windows.Forms.ColumnHeader columnHeader1;
		internal System.Windows.Forms.ColumnHeader columnHeader2;
		internal System.Windows.Forms.ColumnHeader columnHeader3;
		internal System.Windows.Forms.ColumnHeader columnHeader4;
		internal System.Windows.Forms.ColumnHeader columnHeader5;
		internal System.Windows.Forms.ColumnHeader columnHeader6;
		internal System.Windows.Forms.ColumnHeader columnHeader7;
		internal System.Windows.Forms.ColumnHeader columnHeader8;
		internal System.Windows.Forms.ColumnHeader columnHeader9;
		internal System.Windows.Forms.ColumnHeader columnHeader19;
		internal System.Windows.Forms.ColumnHeader columnHeader20;
		internal System.Windows.Forms.ColumnHeader columnHeader21;
		internal System.Windows.Forms.ColumnHeader columnHeader22;
		internal System.Windows.Forms.ColumnHeader columnHeader23;
		internal System.Windows.Forms.ColumnHeader columnHeader24;
		internal System.Windows.Forms.ColumnHeader columnHeader25;
		internal System.Windows.Forms.ColumnHeader columnHeader27;
		internal PinkieControls.ButtonXP m_cmdAddNew;
		internal PinkieControls.ButtonXP m_cmdAddNewDnm;
		internal System.Windows.Forms.ListView m_lsvCardiogramReportList;
		internal System.Windows.Forms.ListView m_lsvDCardiogramReportList;
		internal PinkieControls.ButtonXP m_cmdRefresh;
		internal PinkieControls.ButtonXP m_cmdQuery;
		private PinkieControls.ButtonXP m_cmdReport;
		private PinkieControls.ButtonXP m_cmdQuit;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		internal PinkieControls.ButtonXP btnSport;
		private System.Windows.Forms.TabPage tabPage2;
		internal System.Windows.Forms.ListView lisvSport;
		internal System.Windows.Forms.ColumnHeader columnHeader11;
		internal System.Windows.Forms.ColumnHeader columnHeader12;
		internal System.Windows.Forms.ColumnHeader columnHeader13;
		internal System.Windows.Forms.ColumnHeader columnHeader14;
		internal System.Windows.Forms.ColumnHeader columnHeader15;
		internal System.Windows.Forms.ColumnHeader columnHeader16;
		internal System.Windows.Forms.ColumnHeader columnHeader17;
		internal System.Windows.Forms.ColumnHeader columnHeader18;
		internal System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.TabPage tabPage4;
		internal System.Windows.Forms.ListView lisvAcct;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader columnHeader34;
		private System.Windows.Forms.ColumnHeader columnHeader35;
		private System.Windows.Forms.ColumnHeader columnHeader36;
		private System.Windows.Forms.ColumnHeader columnHeader37;
		private System.Windows.Forms.ColumnHeader columnHeader38;
		private System.Windows.Forms.ColumnHeader 缴费状态;
        private PinkieControls.ButtonXP m_cmdApplyForm;
        private Label label1;
        private Label label4;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCardiogramReportManage()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
        }
        #region 记录心电图查询条件

        public static string strOPQueryButtonName = "当天";

        public string strFormName1 = "心电图";
        public string strFromDat1 = "";
        public string strToDat1 = "";
        public string strPatientNo1 = "";
        public string strInPatientNo1 = "";
        public string strPatientName1 = "";
        public string strDept1 = "";
        public string strReportNo1 = "";
        public string strIsSpecail1 = "";
        public string strReporter1 = "";
        #endregion 
        #region 记录动态心电图查询条件
        public string strFormName2 = "动态心电图";
        public string strFromDat2 = "";
        public string strToDat2 = "";
        public string strPatientNo2 = "";
        public string strInPatientNo2 = "";
        public string strPatientName2 = "";
        public string strDept2 = "";
        public string strReportNo2 = "";
        public string strIsSpecail2 = "";
        public string strReporter2 = "";
        #endregion 
        #region 记录平板动态心电图查询条件
        public string strFormName3 = "平板动态心电图";
        public string strFromDat3 = "";
        public string strToDat3 = "";
        public string strPatientNo3 = "";
        public string strInPatientNo3 = "";
        public string strPatientName3 = "";
        public string strDept3 = "";
        public string strReportNo3 = "";
        public string strIsSpecail3 = "";
        public string strReporter3 = "";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCardiogramReportManage));
            this.m_tbcMain = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lisvAcct = new System.Windows.Forms.ListView();
            this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader35 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader34 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader33 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader31 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader32 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader36 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader37 = new System.Windows.Forms.ColumnHeader();
            this.缴费状态 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader38 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_lsvCardiogramReportList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_lsvDCardiogramReportList = new System.Windows.Forms.ListView();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lisvSport = new System.Windows.Forms.ListView();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
            this.m_cmdAddNew = new PinkieControls.ButtonXP();
            this.m_cmdRefresh = new PinkieControls.ButtonXP();
            this.m_cmdAddNewDnm = new PinkieControls.ButtonXP();
            this.m_cmdQuery = new PinkieControls.ButtonXP();
            this.m_cmdReport = new PinkieControls.ButtonXP();
            this.m_cmdQuit = new PinkieControls.ButtonXP();
            this.btnSport = new PinkieControls.ButtonXP();
            this.m_cmdApplyForm = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_tbcMain.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tbcMain
            // 
            this.m_tbcMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbcMain.Controls.Add(this.tabPage4);
            this.m_tbcMain.Controls.Add(this.tabPage1);
            this.m_tbcMain.Controls.Add(this.tabPage3);
            this.m_tbcMain.Controls.Add(this.tabPage2);
            this.m_tbcMain.Location = new System.Drawing.Point(1, 1);
            this.m_tbcMain.Name = "m_tbcMain";
            this.m_tbcMain.SelectedIndex = 0;
            this.m_tbcMain.Size = new System.Drawing.Size(1028, 580);
            this.m_tbcMain.TabIndex = 0;
            this.m_tbcMain.SelectedIndexChanged += new System.EventHandler(this.m_tbcMain_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.lisvAcct);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1020, 553);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "心电图申请单";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.YellowGreen;
            this.label4.Location = new System.Drawing.Point(618, -18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = " ";
            // 
            // lisvAcct
            // 
            this.lisvAcct.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader30,
            this.columnHeader35,
            this.columnHeader34,
            this.columnHeader33,
            this.columnHeader31,
            this.columnHeader32,
            this.columnHeader36,
            this.columnHeader29,
            this.columnHeader37,
            this.缴费状态,
            this.columnHeader28,
            this.columnHeader38});
            this.lisvAcct.FullRowSelect = true;
            this.lisvAcct.GridLines = true;
            this.lisvAcct.HideSelection = false;
            this.lisvAcct.Location = new System.Drawing.Point(0, 0);
            this.lisvAcct.Name = "lisvAcct";
            this.lisvAcct.Size = new System.Drawing.Size(1024, 552);
            this.lisvAcct.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lisvAcct.TabIndex = 0;
            this.lisvAcct.UseCompatibleStateImageBehavior = false;
            this.lisvAcct.View = System.Windows.Forms.View.Details;
            this.lisvAcct.DoubleClick += new System.EventHandler(this.lisvAcct_DoubleClick);
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "病人卡号";
            this.columnHeader30.Width = 120;
            // 
            // columnHeader35
            // 
            this.columnHeader35.Text = "姓名";
            this.columnHeader35.Width = 100;
            // 
            // columnHeader34
            // 
            this.columnHeader34.Text = "性别";
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "年龄";
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "科别";
            this.columnHeader31.Width = 80;
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "床号";
            // 
            // columnHeader36
            // 
            this.columnHeader36.Text = "住院号";
            this.columnHeader36.Width = 80;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "申请日期";
            this.columnHeader29.Width = 150;
            // 
            // columnHeader37
            // 
            this.columnHeader37.Text = "门诊号";
            this.columnHeader37.Width = 80;
            // 
            // 缴费状态
            // 
            this.缴费状态.Text = "缴费状态";
            this.缴费状态.Width = 75;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "申请单类型";
            this.columnHeader28.Width = 150;
            // 
            // columnHeader38
            // 
            this.columnHeader38.Text = "前次心电图号";
            this.columnHeader38.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.YellowGreen;
            this.label1.Location = new System.Drawing.Point(598, -18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = " ";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lsvCardiogramReportList);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1020, 553);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "心电图";
            // 
            // m_lsvCardiogramReportList
            // 
            this.m_lsvCardiogramReportList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader9,
            this.columnHeader6,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8});
            this.m_lsvCardiogramReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvCardiogramReportList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvCardiogramReportList.FullRowSelect = true;
            this.m_lsvCardiogramReportList.GridLines = true;
            this.m_lsvCardiogramReportList.Location = new System.Drawing.Point(0, 0);
            this.m_lsvCardiogramReportList.Name = "m_lsvCardiogramReportList";
            this.m_lsvCardiogramReportList.Size = new System.Drawing.Size(1020, 553);
            this.m_lsvCardiogramReportList.TabIndex = 0;
            this.m_lsvCardiogramReportList.UseCompatibleStateImageBehavior = false;
            this.m_lsvCardiogramReportList.View = System.Windows.Forms.View.Details;
            this.m_lsvCardiogramReportList.DoubleClick += new System.EventHandler(this.m_lsvCardiogramReportList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "心电图号";
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
            this.columnHeader4.Width = 90;
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
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "心电图诊断";
            this.columnHeader7.Width = 220;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "报告日期";
            this.columnHeader8.Width = 180;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_lsvDCardiogramReportList);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1020, 553);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "动态心电图";
            // 
            // m_lsvDCardiogramReportList
            // 
            this.m_lsvDCardiogramReportList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader10,
            this.columnHeader27});
            this.m_lsvDCardiogramReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvDCardiogramReportList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvDCardiogramReportList.FullRowSelect = true;
            this.m_lsvDCardiogramReportList.GridLines = true;
            this.m_lsvDCardiogramReportList.Location = new System.Drawing.Point(0, 0);
            this.m_lsvDCardiogramReportList.Name = "m_lsvDCardiogramReportList";
            this.m_lsvDCardiogramReportList.Size = new System.Drawing.Size(1020, 553);
            this.m_lsvDCardiogramReportList.TabIndex = 1;
            this.m_lsvDCardiogramReportList.UseCompatibleStateImageBehavior = false;
            this.m_lsvDCardiogramReportList.View = System.Windows.Forms.View.Details;
            this.m_lsvDCardiogramReportList.DoubleClick += new System.EventHandler(this.m_lsvDCardiogramReportList_DoubleClick);
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "心电图号";
            this.columnHeader19.Width = 80;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "姓名";
            this.columnHeader20.Width = 80;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "性别";
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "年龄";
            this.columnHeader22.Width = 90;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "门诊号";
            this.columnHeader23.Width = 80;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "住院号";
            this.columnHeader24.Width = 80;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "科室";
            this.columnHeader25.Width = 82;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "心电图诊断";
            this.columnHeader10.Width = 254;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "报告日期";
            this.columnHeader27.Width = 221;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lisvSport);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1020, 553);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "平板运动心电图";
            // 
            // lisvSport
            // 
            this.lisvSport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader26});
            this.lisvSport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisvSport.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lisvSport.FullRowSelect = true;
            this.lisvSport.GridLines = true;
            this.lisvSport.Location = new System.Drawing.Point(0, 0);
            this.lisvSport.Name = "lisvSport";
            this.lisvSport.Size = new System.Drawing.Size(1020, 553);
            this.lisvSport.TabIndex = 1;
            this.lisvSport.UseCompatibleStateImageBehavior = false;
            this.lisvSport.View = System.Windows.Forms.View.Details;
            this.lisvSport.DoubleClick += new System.EventHandler(this.lisvSport_DoubleClick);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "心电图号";
            this.columnHeader11.Width = 80;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "姓名";
            this.columnHeader12.Width = 80;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "性别";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "年龄";
            this.columnHeader14.Width = 90;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "门诊号";
            this.columnHeader15.Width = 80;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "住院号";
            this.columnHeader16.Width = 80;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "科室";
            this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader17.Width = 100;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "心电图诊断";
            this.columnHeader18.Width = 220;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "报告日期";
            this.columnHeader26.Width = 180;
            // 
            // m_cmdAddNew
            // 
            this.m_cmdAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdAddNew.DefaultScheme = true;
            this.m_cmdAddNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddNew.Hint = "";
            this.m_cmdAddNew.Location = new System.Drawing.Point(428, 599);
            this.m_cmdAddNew.Name = "m_cmdAddNew";
            this.m_cmdAddNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddNew.Size = new System.Drawing.Size(120, 28);
            this.m_cmdAddNew.TabIndex = 1;
            this.m_cmdAddNew.Text = "添加心电图(&M)";
            this.m_cmdAddNew.Click += new System.EventHandler(this.m_cmdAddNew_Click);
            // 
            // m_cmdRefresh
            // 
            this.m_cmdRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdRefresh.DefaultScheme = true;
            this.m_cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdRefresh.Hint = "";
            this.m_cmdRefresh.Location = new System.Drawing.Point(20, 599);
            this.m_cmdRefresh.Name = "m_cmdRefresh";
            this.m_cmdRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdRefresh.Size = new System.Drawing.Size(88, 28);
            this.m_cmdRefresh.TabIndex = 4;
            this.m_cmdRefresh.Text = "刷新(&Q)";
            this.m_cmdRefresh.Click += new System.EventHandler(this.m_cmdRefresh_Click);
            // 
            // m_cmdAddNewDnm
            // 
            this.m_cmdAddNewDnm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdAddNewDnm.DefaultScheme = true;
            this.m_cmdAddNewDnm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAddNewDnm.Hint = "";
            this.m_cmdAddNewDnm.Location = new System.Drawing.Point(562, 599);
            this.m_cmdAddNewDnm.Name = "m_cmdAddNewDnm";
            this.m_cmdAddNewDnm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAddNewDnm.Size = new System.Drawing.Size(143, 28);
            this.m_cmdAddNewDnm.TabIndex = 5;
            this.m_cmdAddNewDnm.Text = "添加动态心电图(&B)";
            this.m_cmdAddNewDnm.Click += new System.EventHandler(this.m_cmdAddNewDnm_Click);
            // 
            // m_cmdQuery
            // 
            this.m_cmdQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdQuery.DefaultScheme = true;
            this.m_cmdQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdQuery.Hint = "";
            this.m_cmdQuery.Location = new System.Drawing.Point(122, 599);
            this.m_cmdQuery.Name = "m_cmdQuery";
            this.m_cmdQuery.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdQuery.Size = new System.Drawing.Size(88, 28);
            this.m_cmdQuery.TabIndex = 6;
            this.m_cmdQuery.Text = "查 询(&F)";
            this.m_cmdQuery.Click += new System.EventHandler(this.m_cmdQuery_Click);
            // 
            // m_cmdReport
            // 
            this.m_cmdReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdReport.DefaultScheme = true;
            this.m_cmdReport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdReport.Hint = "";
            this.m_cmdReport.Location = new System.Drawing.Point(224, 599);
            this.m_cmdReport.Name = "m_cmdReport";
            this.m_cmdReport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdReport.Size = new System.Drawing.Size(88, 28);
            this.m_cmdReport.TabIndex = 7;
            this.m_cmdReport.Text = "报表(&P)";
            this.m_cmdReport.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_cmdQuit
            // 
            this.m_cmdQuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdQuit.DefaultScheme = true;
            this.m_cmdQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdQuit.Hint = "";
            this.m_cmdQuit.Location = new System.Drawing.Point(907, 599);
            this.m_cmdQuit.Name = "m_cmdQuit";
            this.m_cmdQuit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdQuit.Size = new System.Drawing.Size(88, 28);
            this.m_cmdQuit.TabIndex = 8;
            this.m_cmdQuit.Text = "退出(&ESC)";
            this.m_cmdQuit.Click += new System.EventHandler(this.m_cmdQuit_Click);
            // 
            // btnSport
            // 
            this.btnSport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnSport.DefaultScheme = true;
            this.btnSport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSport.Hint = "";
            this.btnSport.Location = new System.Drawing.Point(719, 599);
            this.btnSport.Name = "btnSport";
            this.btnSport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSport.Size = new System.Drawing.Size(174, 28);
            this.btnSport.TabIndex = 9;
            this.btnSport.Text = "添加平板运动心电图(&C)";
            this.btnSport.Click += new System.EventHandler(this.btnSport_Click);
            // 
            // m_cmdApplyForm
            // 
            this.m_cmdApplyForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdApplyForm.DefaultScheme = true;
            this.m_cmdApplyForm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdApplyForm.Hint = "";
            this.m_cmdApplyForm.Location = new System.Drawing.Point(326, 599);
            this.m_cmdApplyForm.Name = "m_cmdApplyForm";
            this.m_cmdApplyForm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdApplyForm.Size = new System.Drawing.Size(88, 28);
            this.m_cmdApplyForm.TabIndex = 10;
            this.m_cmdApplyForm.Text = "申请单(&A)";
            this.m_cmdApplyForm.Click += new System.EventHandler(this.m_cmdApplyForm_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(364, 581);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "黄色为已完成申请单";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.YellowGreen;
            this.label3.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(344, 582);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 10);
            this.label3.TabIndex = 15;
            this.label3.Text = "  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(558, 583);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 10);
            this.label5.TabIndex = 16;
            this.label5.Text = "  ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(577, 581);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "白色为未完成申请单";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // frmCardiogramReportManage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1028, 639);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_cmdApplyForm);
            this.Controls.Add(this.btnSport);
            this.Controls.Add(this.m_cmdQuit);
            this.Controls.Add(this.m_cmdReport);
            this.Controls.Add(this.m_cmdQuery);
            this.Controls.Add(this.m_cmdAddNewDnm);
            this.Controls.Add(this.m_cmdRefresh);
            this.Controls.Add(this.m_cmdAddNew);
            this.Controls.Add(this.m_tbcMain);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCardiogramReportManage";
            this.Text = "心电图室管理";
            this.Load += new System.EventHandler(this.frmCardiogramReportManage_Load);
            this.m_tbcMain.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmCardiogramReportManage());
		}

		public override void CreateController()
		{
			this.objController = new com.digitalwave.iCare.gui.RIS.clsController_RISCardiogramManage();
			objController.Set_GUI_Apperance(this);
		}

		private void frmCardiogramReportManage_Load(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramManage)this.objController).m_mthGetCardiogramReportArr();
			((clsController_RISCardiogramManage)this.objController).m_mthGetDCardiogramReportArr();
			((clsController_RISCardiogramManage)this.objController).m_mthGetSportReportArr();
			((clsController_RISCardiogramManage)this.objController).m_lngGetAcctData(DateTime.Now,DateTime.Now);
            
		}

		private void m_cmdAddNew_Click(object sender, System.EventArgs e)
		{
            //if (this.m_lsvCardiogramReportList.Items.Count == 0)
            //    return;
			if(m_tbcMain.SelectedIndex!=0||lisvAcct.SelectedItems.Count <=0)
			{
				frmCardiogramReport m_objViewerShow = new frmCardiogramReport();
				m_objViewerShow.m_mthSetParentApperance(this);
				m_objViewerShow.m_cmdDelete.Enabled = false;
				m_objViewerShow.m_cmdConfirm.Enabled = false;
				m_objViewerShow.m_cmdPrint.Enabled = false;
				m_objViewerShow.m_cheIsNew.Visible=false;
                m_objViewerShow.m_btnDisplayApplyOrder.Enabled = false;
                m_objViewerShow.m_cmdSave.Tag = "NO";
				m_objViewerShow.ShowDialog();
			}
			else
			((clsController_RISCardiogramManage)this.objController).m_mthShowCardiogramReportAddNew(this);
		}

		private void m_cmdAddNewDnm_Click(object sender, System.EventArgs e)
		{
            //if (this.m_lsvDCardiogramReportList.Items.Count == 0)
            //    return;
			if(m_tbcMain.SelectedIndex!=0||lisvAcct.SelectedItems.Count <=0)
			{
				frmDnmCardiogramReport m_objViewer = new frmDnmCardiogramReport();
				m_objViewer.SetParentApperance(this);
				m_objViewer.m_cmdDelete.Enabled = false;
				m_objViewer.m_cmdConfirm.Enabled = false;
				m_objViewer.m_cmdPrint.Enabled = false;
				m_objViewer.m_cheIsNew.Visible=false;
				m_objViewer.ShowDialog();
			}
			else
			((clsController_RISCardiogramManage)this.objController).m_mthShowCardiogramReportSportAddNew(this);
		}

		public void m_cmdRefresh_Click(object sender, System.EventArgs e)
		{
            lisvAcct.Items.Clear();
            m_cmdRefreshClick();            
		
		}
        public void m_cmdRefreshClick()
        {
            frmCardiogramReportManage.strOPQueryButtonName = "当天";
            int index = this.m_tbcMain.SelectedIndex;

            ((clsController_RISCardiogramManage)this.objController).m_lngGetAcctData(DateTime.Now, DateTime.Now);
            ((clsController_RISCardiogramManage)this.objController).m_mthGetCardiogramReportArr();

            ((clsController_RISCardiogramManage)this.objController).m_mthGetDCardiogramReportArr();

            ((clsController_RISCardiogramManage)this.objController).m_mthGetSportReportArr();  
        }

		private void m_lsvCardiogramReportList_DoubleClick(object sender, System.EventArgs e)
		{

			((clsController_RISCardiogramManage)this.objController).m_mthSetViewerComInfo();
			((clsController_RISCardiogramManage)this.objController).m_mthShowCardiogramReport(this);

		}

		private void m_lsvDCardiogramReportList_DoubleClick(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramManage)this.objController).m_mthShowDCardiogramReport(this);
		}

		private void m_cmdQuery_Click(object sender, System.EventArgs e)
		{
			frmQueryCardiogramReport m_objViewer = new frmQueryCardiogramReport();
			m_objViewer.m_mthGetParentApperance(this);
			m_objViewer.ShowDialog();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			frmCardiogramReportrptNew CardiogramReport=new frmCardiogramReportrptNew();
			CardiogramReport.ShowDialog();
		}

		private void m_cmdQuit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnSport_Click(object sender, System.EventArgs e)
		{
            //if (this.lisvSport.Items.Count == 0)
            //    return;
			if(m_tbcMain.SelectedIndex!=0||lisvAcct.SelectedItems.Count <=0)
			{
				frmFlatAndSportReport m_objViewerShowPenBan=new frmFlatAndSportReport();
				m_objViewerShowPenBan.m_mthSetParentApperance(this);
				m_objViewerShowPenBan.m_cmdDelete.Enabled = false;
				m_objViewerShowPenBan.m_cmdConfirm.Enabled = false;
				m_objViewerShowPenBan.m_cmdPrint.Enabled = false;
				m_objViewerShowPenBan.ShowDialog();
			}
			else
			((clsController_RISCardiogramManage)this.objController).m_mthShowCardiogramReportPenBanSportAddNew(this);
		}

		private void lisvSport_DoubleClick(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramManage)this.objController).m_mthSetViewerComInfo();
			((clsController_RISCardiogramManage)this.objController).m_mthShowSportReport(this);
		}

		private void m_tbcMain_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.m_tbcMain.SelectedIndex == 0)
				m_cmdApplyForm.Enabled = true;
			else
				m_cmdApplyForm.Enabled = false;
		}

		private void lisvAcct_DoubleClick(object sender, System.EventArgs e)
		{
		((clsController_RISCardiogramManage)this.objController).m_mthOpenHeartApplyFormByApplyId( lisvAcct);
		}

		private void m_cmdApplyForm_Click(object sender, System.EventArgs e)
		{
			((clsController_RISCardiogramManage)this.objController).m_mthOpenHeartApplyFormByApplyId( lisvAcct);		
		}

        //private void lisvAcct_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //private void lisvAcct_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    this.lisvAcct.Sorting = SortOrder.Ascending;
        //}

	}
}
