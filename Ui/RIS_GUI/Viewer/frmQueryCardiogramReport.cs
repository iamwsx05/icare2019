using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.RIS
{
	public class frmQueryCardiogramReport : com.digitalwave.GUI_Base.frmQueryBaseForm
	{
		private System.ComponentModel.IContainer components = null;
		private com.digitalwave.iCare.gui.RIS.clsDomainController_RISCardiogramManage m_objManage = null;
		private com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage m_objParentViewer= null;
		private System.Windows.Forms.CheckBox m_chkCReportNo;
		private System.Windows.Forms.TextBox m_txtCReportNo;
		protected internal System.Windows.Forms.RadioButton m_rdbCReport;
		protected internal System.Windows.Forms.RadioButton m_rdbDCReport;
		private System.Windows.Forms.CheckBox checkBox1;
		protected internal System.Windows.Forms.RadioButton m_rdbSport;
        protected com.digitalwave.controls.ctlTextBoxFind m_txtDept;
        protected internal RadioButton m_objRadioApply;
        private TextBox m_txtCheckPart;
        private CheckBox m_chkApplyPart;
        private CheckBox m_chkFinished;
        private CheckBox m_chkReporter;
        public ListViewBox m_txtREPORTOR_NAME_VCHR;
		private com.digitalwave.iCare.gui.RIS.clsController_RISCardiogramManage m_objParentController=null;




		public frmQueryCardiogramReport()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			m_objManage = new clsDomainController_RISCardiogramManage();
			m_objParentController = new clsController_RISCardiogramManage();
		}

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

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryCardiogramReport));
            this.m_chkCReportNo = new System.Windows.Forms.CheckBox();
            this.m_txtCReportNo = new System.Windows.Forms.TextBox();
            this.m_rdbCReport = new System.Windows.Forms.RadioButton();
            this.m_rdbDCReport = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.m_rdbSport = new System.Windows.Forms.RadioButton();
            this.m_txtDept = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_objRadioApply = new System.Windows.Forms.RadioButton();
            this.m_txtCheckPart = new System.Windows.Forms.TextBox();
            this.m_chkApplyPart = new System.Windows.Forms.CheckBox();
            this.m_chkFinished = new System.Windows.Forms.CheckBox();
            this.m_chkReporter = new System.Windows.Forms.CheckBox();
            this.m_txtREPORTOR_NAME_VCHR = new ListViewBox();
            this.SuspendLayout();
            // 
            // m_chkPatientNO
            // 
            this.m_chkPatientNO.Location = new System.Drawing.Point(38, 137);
            this.m_chkPatientNO.CheckedChanged += new System.EventHandler(this.m_chkPatientNO_CheckedChanged);
            // 
            // m_chkDept
            // 
            this.m_chkDept.Location = new System.Drawing.Point(38, 221);
            this.m_chkDept.CheckedChanged += new System.EventHandler(this.m_chkDept_CheckedChanged);
            // 
            // m_chkPatientName
            // 
            this.m_chkPatientName.Location = new System.Drawing.Point(38, 193);
            this.m_chkPatientName.CheckedChanged += new System.EventHandler(this.m_chkPatientName_CheckedChanged);
            // 
            // m_chkInPatientNO
            // 
            this.m_chkInPatientNO.Location = new System.Drawing.Point(38, 165);
            this.m_chkInPatientNO.CheckedChanged += new System.EventHandler(this.m_chkInPatientNO_CheckedChanged);
            // 
            // m_dtpTimeForm
            // 
            this.m_dtpTimeForm.CustomFormat = "yyyy年MM月dd日";
            this.m_dtpTimeForm.Enabled = false;
            this.m_dtpTimeForm.Location = new System.Drawing.Point(126, 48);
            this.m_dtpTimeForm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeForm_KeyDown);
            // 
            // m_dtpTimeTo
            // 
            this.m_dtpTimeTo.CustomFormat = "yyyy年MM月dd日 ";
            this.m_dtpTimeTo.Enabled = false;
            this.m_dtpTimeTo.Location = new System.Drawing.Point(126, 76);
            this.m_dtpTimeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_chkTimeFrom
            // 
            this.m_chkTimeFrom.Location = new System.Drawing.Point(38, 47);
            this.m_chkTimeFrom.CheckedChanged += new System.EventHandler(this.m_chkTimeFrom_CheckedChanged);
            // 
            // m_txtPatientNO
            // 
            this.m_txtPatientNO.Enabled = false;
            this.m_txtPatientNO.Location = new System.Drawing.Point(126, 137);
            this.m_txtPatientNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_txtInPatientNO
            // 
            this.m_txtInPatientNO.Enabled = false;
            this.m_txtInPatientNO.Location = new System.Drawing.Point(126, 165);
            this.m_txtInPatientNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Enabled = false;
            this.m_txtPatientName.Location = new System.Drawing.Point(126, 193);
            this.m_txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.Location = new System.Drawing.Point(130, 338);
            this.m_cmdClear.Size = new System.Drawing.Size(95, 28);
            this.m_cmdClear.Text = "清  空(&C)";
            this.m_cmdClear.Click += new System.EventHandler(this.m_cmdClear_Click);
            // 
            // m_cmdQuery
            // 
            this.m_cmdQuery.Location = new System.Drawing.Point(30, 338);
            this.m_cmdQuery.Size = new System.Drawing.Size(92, 28);
            this.m_cmdQuery.Text = "查  询(&Q)";
            this.m_cmdQuery.Click += new System.EventHandler(this.m_cmdQuery_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.Location = new System.Drawing.Point(232, 338);
            this.m_cmdExit.Size = new System.Drawing.Size(103, 28);
            this.m_cmdExit.Text = "退  出(&E)";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_lblTimeTo
            // 
            this.m_lblTimeTo.Location = new System.Drawing.Point(98, 80);
            // 
            // m_chkCReportNo
            // 
            this.m_chkCReportNo.Location = new System.Drawing.Point(38, 278);
            this.m_chkCReportNo.Name = "m_chkCReportNo";
            this.m_chkCReportNo.Size = new System.Drawing.Size(84, 24);
            this.m_chkCReportNo.TabIndex = 1021;
            this.m_chkCReportNo.Text = "心电图号";
            this.m_chkCReportNo.CheckedChanged += new System.EventHandler(this.m_chkCReportNo_CheckedChanged);
            // 
            // m_txtCReportNo
            // 
            this.m_txtCReportNo.Enabled = false;
            this.m_txtCReportNo.Location = new System.Drawing.Point(126, 278);
            this.m_txtCReportNo.Name = "m_txtCReportNo";
            this.m_txtCReportNo.Size = new System.Drawing.Size(184, 23);
            this.m_txtCReportNo.TabIndex = 1022;
            this.m_txtCReportNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_rdbCReport
            // 
            this.m_rdbCReport.Location = new System.Drawing.Point(82, 13);
            this.m_rdbCReport.Name = "m_rdbCReport";
            this.m_rdbCReport.Size = new System.Drawing.Size(68, 24);
            this.m_rdbCReport.TabIndex = 1023;
            this.m_rdbCReport.Text = "心电图";
            // 
            // m_rdbDCReport
            // 
            this.m_rdbDCReport.Location = new System.Drawing.Point(149, 14);
            this.m_rdbDCReport.Name = "m_rdbDCReport";
            this.m_rdbDCReport.Size = new System.Drawing.Size(96, 24);
            this.m_rdbDCReport.TabIndex = 1024;
            this.m_rdbDCReport.Text = "动态心电图";
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(38, 308);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 24);
            this.checkBox1.TabIndex = 1025;
            this.checkBox1.Text = "查询特殊病人";
            // 
            // m_rdbSport
            // 
            this.m_rdbSport.Location = new System.Drawing.Point(245, 15);
            this.m_rdbSport.Name = "m_rdbSport";
            this.m_rdbSport.Size = new System.Drawing.Size(96, 24);
            this.m_rdbSport.TabIndex = 1026;
            this.m_rdbSport.Text = "运动心电图";
            // 
            // m_txtDept
            // 
            this.m_txtDept.Enabled = false;
            this.m_txtDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDept.intHeight = 100;
            this.m_txtDept.IsEnterShow = true;
            this.m_txtDept.isHide = 4;
            this.m_txtDept.isTxt = 1;
            this.m_txtDept.isUpOrDn = 0;
            this.m_txtDept.isValuse = 0;
            this.m_txtDept.Location = new System.Drawing.Point(126, 220);
            this.m_txtDept.m_IsHaveParent = false;
            this.m_txtDept.m_strParentName = "";
            this.m_txtDept.Name = "m_txtDept";
            this.m_txtDept.nextCtl = this.m_cmdQuery;
            this.m_txtDept.Size = new System.Drawing.Size(184, 24);
            this.m_txtDept.TabIndex = 1027;
            this.m_txtDept.txtValuse = "";
            this.m_txtDept.VsLeftOrRight = 1;
            // 
            // m_objRadioApply
            // 
            this.m_objRadioApply.Checked = true;
            this.m_objRadioApply.Location = new System.Drawing.Point(14, 12);
            this.m_objRadioApply.Name = "m_objRadioApply";
            this.m_objRadioApply.Size = new System.Drawing.Size(80, 24);
            this.m_objRadioApply.TabIndex = 1028;
            this.m_objRadioApply.TabStop = true;
            this.m_objRadioApply.Text = "申请单";
            this.m_objRadioApply.CheckedChanged += new System.EventHandler(this.m_objRadioApply_CheckedChanged);
            // 
            // m_txtCheckPart
            // 
            this.m_txtCheckPart.Enabled = false;
            this.m_txtCheckPart.Location = new System.Drawing.Point(126, 248);
            this.m_txtCheckPart.Name = "m_txtCheckPart";
            this.m_txtCheckPart.Size = new System.Drawing.Size(184, 23);
            this.m_txtCheckPart.TabIndex = 1030;
            // 
            // m_chkApplyPart
            // 
            this.m_chkApplyPart.Location = new System.Drawing.Point(38, 248);
            this.m_chkApplyPart.Name = "m_chkApplyPart";
            this.m_chkApplyPart.Size = new System.Drawing.Size(84, 24);
            this.m_chkApplyPart.TabIndex = 1029;
            this.m_chkApplyPart.Text = "申请部位";
            this.m_chkApplyPart.CheckedChanged += new System.EventHandler(this.m_chkApplyPart_CheckedChanged);
            // 
            // m_chkFinished
            // 
            this.m_chkFinished.Location = new System.Drawing.Point(178, 309);
            this.m_chkFinished.Name = "m_chkFinished";
            this.m_chkFinished.Size = new System.Drawing.Size(143, 24);
            this.m_chkFinished.TabIndex = 1031;
            this.m_chkFinished.Text = "查询已完成申请单";
            // 
            // m_chkReporter
            // 
            this.m_chkReporter.Location = new System.Drawing.Point(38, 105);
            this.m_chkReporter.Name = "m_chkReporter";
            this.m_chkReporter.Size = new System.Drawing.Size(84, 24);
            this.m_chkReporter.TabIndex = 1032;
            this.m_chkReporter.Text = "报告医师";
            this.m_chkReporter.CheckedChanged += new System.EventHandler(this.m_chkReporter_CheckedChanged);
            // 
            // m_txtREPORTOR_NAME_VCHR
            // 
            this.m_txtREPORTOR_NAME_VCHR.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtREPORTOR_NAME_VCHR.Enabled = false;
            this.m_txtREPORTOR_NAME_VCHR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtREPORTOR_NAME_VCHR.intHeight = 200;
            this.m_txtREPORTOR_NAME_VCHR.IsEnterShow = true;
            this.m_txtREPORTOR_NAME_VCHR.isHide = 3;
            this.m_txtREPORTOR_NAME_VCHR.isTxt = 1;
            this.m_txtREPORTOR_NAME_VCHR.isUpOrDn = 0;
            this.m_txtREPORTOR_NAME_VCHR.isValuse = 3;
            this.m_txtREPORTOR_NAME_VCHR.Location = new System.Drawing.Point(126, 106);
            this.m_txtREPORTOR_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtREPORTOR_NAME_VCHR.m_strParentName = "";
            this.m_txtREPORTOR_NAME_VCHR.Name = "m_txtREPORTOR_NAME_VCHR";
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtREPORTOR_NAME_VCHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtREPORTOR_NAME_VCHR.TabIndex = 1033;
            this.m_txtREPORTOR_NAME_VCHR.txtValuse = "";
            this.m_txtREPORTOR_NAME_VCHR.VsLeftOrRight = 1;
            // 
            // frmQueryCardiogramReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(347, 382);
            this.Controls.Add(this.m_txtREPORTOR_NAME_VCHR);
            this.Controls.Add(this.m_chkReporter);
            this.Controls.Add(this.m_chkFinished);
            this.Controls.Add(this.m_txtCheckPart);
            this.Controls.Add(this.m_chkApplyPart);
            this.Controls.Add(this.m_rdbCReport);
            this.Controls.Add(this.m_objRadioApply);
            this.Controls.Add(this.m_txtDept);
            this.Controls.Add(this.m_rdbSport);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.m_rdbDCReport);
            this.Controls.Add(this.m_txtCReportNo);
            this.Controls.Add(this.m_chkCReportNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQueryCardiogramReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询窗口";
            this.Load += new System.EventHandler(this.frmQueryCardiogramReport_Load);
            this.Controls.SetChildIndex(this.m_chkCReportNo, 0);
            this.Controls.SetChildIndex(this.m_txtCReportNo, 0);
            this.Controls.SetChildIndex(this.m_rdbDCReport, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.m_cmdClear, 0);
            this.Controls.SetChildIndex(this.m_chkPatientNO, 0);
            this.Controls.SetChildIndex(this.m_cmdQuery, 0);
            this.Controls.SetChildIndex(this.m_cmdExit, 0);
            this.Controls.SetChildIndex(this.m_txtInPatientNO, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtPatientNO, 0);
            this.Controls.SetChildIndex(this.m_chkDept, 0);
            this.Controls.SetChildIndex(this.m_chkPatientName, 0);
            this.Controls.SetChildIndex(this.m_chkInPatientNO, 0);
            this.Controls.SetChildIndex(this.m_dtpTimeForm, 0);
            this.Controls.SetChildIndex(this.m_dtpTimeTo, 0);
            this.Controls.SetChildIndex(this.m_chkTimeFrom, 0);
            this.Controls.SetChildIndex(this.m_lblTimeTo, 0);
            this.Controls.SetChildIndex(this.m_rdbSport, 0);
            this.Controls.SetChildIndex(this.m_txtDept, 0);
            this.Controls.SetChildIndex(this.m_objRadioApply, 0);
            this.Controls.SetChildIndex(this.m_rdbCReport, 0);
            this.Controls.SetChildIndex(this.m_chkApplyPart, 0);
            this.Controls.SetChildIndex(this.m_txtCheckPart, 0);
            this.Controls.SetChildIndex(this.m_chkFinished, 0);
            this.Controls.SetChildIndex(this.m_chkReporter, 0);
            this.Controls.SetChildIndex(this.m_txtREPORTOR_NAME_VCHR, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region 界面CheckBox事件
		private void m_chkTimeFrom_CheckedChanged(object sender, System.EventArgs e)
		{
			if(m_chkTimeFrom.Checked)
			{
				this.m_dtpTimeForm.Enabled = true;
				this.m_dtpTimeTo.Enabled = true;
				this.m_dtpTimeForm.Select();
			}
			else
			{
				this.m_dtpTimeForm.Enabled = false;
				this.m_dtpTimeTo.Enabled = false;
			}
		}

		private void m_chkPatientNO_CheckedChanged(object sender, System.EventArgs e)
		{
			if(m_chkPatientNO.Checked)
			{
				this.m_txtPatientNO.Enabled = true;
				this.m_txtPatientNO.Select();
			}
			else
			{
				this.m_txtPatientNO.Enabled = false;
			}
		}

		private void m_chkInPatientNO_CheckedChanged(object sender, System.EventArgs e)
		{
			if(m_chkInPatientNO.Checked)
			{
				this.m_txtInPatientNO.Enabled = true;
				this.m_txtInPatientNO.Select();
			}
			else
			{
				this.m_txtInPatientNO.Enabled = false;
			}
		}

		private void m_chkPatientName_CheckedChanged(object sender, System.EventArgs e)
		{
			if(m_chkPatientName.Checked)
			{
				this.m_txtPatientName.Enabled = true;
				this.m_txtPatientName.Select();
			}
			else
			{
				this.m_txtPatientName.Enabled = false;
			}
		}

		private void m_chkDept_CheckedChanged(object sender, System.EventArgs e)
		{
			if(m_chkDept.Checked)
			{
				this.m_txtDept.Enabled = true;
				this.m_txtDept.Select();
			}
			else
			{
				this.m_txtDept.Enabled = false;
			}
		}

        private void m_chkReporter_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkReporter.Checked)
            {
                this.m_txtREPORTOR_NAME_VCHR.Enabled = true;
                m_mthInitData();
            }
            else
            {
                this.m_txtREPORTOR_NAME_VCHR.Enabled = false;
                this.m_txtREPORTOR_NAME_VCHR.txtValuse = string.Empty;
            }
        }

		private void m_chkCReportNo_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.m_chkCReportNo.Checked)
			{
				this.m_txtCReportNo.Enabled = true;
				this.m_txtCReportNo.Select();
			}
			else
			{
				this.m_txtCReportNo.Enabled = false;
			}
		}
		#endregion

		#region 获取父窗体
		public void m_mthGetParentApperance(com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage infrmCardiogramReportManage)
		{
			m_objParentViewer = infrmCardiogramReportManage;
		}
		#endregion

		#region 根据条件组合查询心电图报告
		/// <summary>
		/// 根据条件组合查询心电图报告
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_cmdQuery_Click(object sender, System.EventArgs e)
		{
			long lngRes = 0;

         string strFromDat = "";
         string strToDat = "";
         string strPatientNo = "";
         string strInPatientNo = "";
         string strPatientName = "";
         string strDept = "";
         string strReportNo = "";
         string strIsSpecail = "";
         string strCheckPart="";
            //报告医师
            string strReporter="";

            if(this.m_chkApplyPart.Checked)
            {
                strCheckPart=this.m_txtCheckPart.Text.Trim();
            }
			if(this.m_chkTimeFrom.Checked)
			{
				strFromDat = this.m_dtpTimeForm.Value.ToShortDateString() + " 00:00:00";
				strToDat = this.m_dtpTimeTo.Value.ToShortDateString() + " 23:59:59";
			}
			if(this.m_chkPatientNO.Checked)
			{
				strPatientNo = this.m_txtPatientNO.Text.ToString().Trim();
			}
			if(this.m_chkInPatientNO.Checked)
			{
				strInPatientNo = this.m_txtInPatientNO.Text.ToString().Trim();
			}
			if(this.m_chkPatientName.Checked)
			{
				strPatientName = this.m_txtPatientName.Text.ToString().Trim();
			}
			if(this.m_chkDept.Checked)
			{
				strDept = this.m_txtDept.txtValuse.Trim();
			}
			if(this.m_chkCReportNo.Checked)
			{
				strReportNo = this.m_txtCReportNo.Text.ToString().Trim();
			}
            if (this.m_chkReporter.Checked)
            {
                strReporter = this.m_txtREPORTOR_NAME_VCHR.txtValuse.ToString();
            }
			if(this.checkBox1.Checked)
			{
			strIsSpecail="1";
			}
			if(m_chkTimeFrom.Checked||this.m_chkPatientNO.Checked||m_chkInPatientNO.Checked||m_chkPatientName.Checked||m_chkDept.Checked||m_chkCReportNo.Checked||checkBox1.Checked)
			{}
			else
			{
			MessageBox.Show("请选择条件!","ICare");
				return;
			}
			if(this.m_rdbCReport.Checked)
			{
         #region 记录查询条件
        m_objParentViewer.strFromDat1 = strFromDat;
        m_objParentViewer.strToDat1 = strToDat;
        m_objParentViewer.strPatientNo1 = strPatientNo;
        m_objParentViewer.strInPatientNo1 = strInPatientNo;
        m_objParentViewer.strPatientName1 = strPatientName;
        m_objParentViewer.strDept1 = strDept;
        m_objParentViewer.strReportNo1 = strReportNo;
        m_objParentViewer.strIsSpecail1 = strIsSpecail;
        m_objParentViewer.strReporter1 = strReporter;
        #endregion 
				m_objParentViewer.m_tbcMain.SelectedIndex=1;
                m_objParentViewer.m_lsvCardiogramReportList.Items.Clear();
				clsRIS_CardiogramReport_VO[] objResultArr = null;
				lngRes = m_objManage.m_lngGetCardiogramReportByCondition(strFromDat,strToDat,strPatientNo,strInPatientNo,strPatientName,strDept,strReportNo
					,strIsSpecail,strReporter,out objResultArr);
				if(lngRes > 0 && objResultArr != null)
				{
					if(objResultArr.Length > 0)
					{
                        frmCardiogramReportManage.strOPQueryButtonName = "查询";

						m_objParentController.Set_GUI_Apperance(m_objParentViewer);
						m_objParentController.m_mthShowCardiogramReportByCondition(objResultArr);
						this.Close();
					}
					else
					{
						MessageBox.Show("没有符合条件的记录！");
					}
				}
				else
				{
					MessageBox.Show("没有符合条件的记录！");
				}
			}
			else if(m_rdbDCReport.Checked)
			{
                #region 记录查询条件
                m_objParentViewer.strFromDat2 = strFromDat;
                m_objParentViewer.strToDat2 = strToDat;
                m_objParentViewer.strPatientNo2 = strPatientNo;
                m_objParentViewer.strInPatientNo2 = strInPatientNo;
                m_objParentViewer.strPatientName2 = strPatientName;
                m_objParentViewer.strDept2 = strDept;
                m_objParentViewer.strReportNo2 = strReportNo;
                m_objParentViewer.strIsSpecail2 = strIsSpecail;
                m_objParentViewer.strReporter2 = strReporter;
                #endregion 
				m_objParentViewer.m_tbcMain.SelectedIndex=2;
                m_objParentViewer.m_lsvDCardiogramReportList.Items.Clear();
				clsRIS_DCardiogramReport_VO[] objResultArr = null;
				lngRes = m_objManage.m_lngGetDCardiogramReportByCondition(strFromDat,strToDat,strPatientNo,strInPatientNo,strPatientName,strDept,strReportNo,strIsSpecail,strReporter
					,out objResultArr);
				if(lngRes > 0 && objResultArr != null)
				{
					if(objResultArr.Length > 0)
					{
                        frmCardiogramReportManage.strOPQueryButtonName = "查询";
						m_objParentController.Set_GUI_Apperance(m_objParentViewer);
						m_objParentController.m_mthShowDCardiogramReportByCondition(objResultArr);
						this.Close();
					}
					else
					{
						MessageBox.Show("没有符合条件的记录！");
					}
				}
				else
				{
					MessageBox.Show("没有符合条件的记录！");
				}
			}
            else if (this.m_rdbSport.Checked)
            {
                #region 记录查询条件
                m_objParentViewer.strFromDat3 = strFromDat;
                m_objParentViewer.strToDat3 = strToDat;
                m_objParentViewer.strPatientNo3 = strPatientNo;
                m_objParentViewer.strInPatientNo3 = strInPatientNo;
                m_objParentViewer.strPatientName3 = strPatientName;
                m_objParentViewer.strDept3 = strDept;
                m_objParentViewer.strReportNo3 = strReportNo;
                m_objParentViewer.strIsSpecail3 = strIsSpecail;
                m_objParentViewer.strReporter3 = strReporter;
                #endregion
                m_objParentViewer.m_tbcMain.SelectedIndex = 3;
                m_objParentViewer.lisvSport.Items.Clear();
                clsafmt_report_VO[] objResultArr = null;
                lngRes = m_objManage.m_lngGetSportReportByCondition(strFromDat, strToDat, strPatientNo, strInPatientNo, strPatientName, strDept, strReportNo, strIsSpecail,strReporter
                    , out objResultArr);
                if (lngRes > 0 && objResultArr != null)
                {
                    if (objResultArr.Length > 0)
                    {
                        frmCardiogramReportManage.strOPQueryButtonName = "查询";
                        m_objParentController.Set_GUI_Apperance(m_objParentViewer);
                        m_objParentController.m_mthGetSportReportArrFind(objResultArr);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("没有符合条件的记录！");
                    }
                }
                else
                {
                    MessageBox.Show("没有符合条件的记录！");
                }

            }
            else
            {
                #region 记录查询条件
                m_objParentViewer.strFromDat3 = strFromDat;
                m_objParentViewer.strToDat3 = strToDat;
                m_objParentViewer.strPatientNo3 = strPatientNo;
                m_objParentViewer.strInPatientNo3 = strInPatientNo;
                m_objParentViewer.strPatientName3 = strPatientName;
                m_objParentViewer.strDept3 = strDept;
                m_objParentViewer.strReportNo3 = strReportNo;
                m_objParentViewer.strIsSpecail3 = strIsSpecail;
                #endregion
                m_objParentViewer.m_tbcMain.SelectedIndex = 0;
                clsApplyRecord[] objResultArr = null;
                com.digitalwave.GLS_WS.clsApplyForm Aps = new com.digitalwave.GLS_WS.clsApplyForm();
                objResultArr = Aps.GetApplyRecordByConditions(strFromDat, strToDat, strPatientNo, strInPatientNo, strPatientName, strDept,strCheckPart,this.m_chkFinished.Checked);
                if (objResultArr != null)
                {
                    if (objResultArr.Length > 0)
                    {
                        frmCardiogramReportManage.strOPQueryButtonName = "查询";
                        m_objParentController.Set_GUI_Apperance(m_objParentViewer);
                        m_objParentController.m_mthShowAcctData(objResultArr);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("没有符合条件的记录！");
                    }
                }
                else
                {
                    MessageBox.Show("没有符合条件的记录！");
                }
            }

		}
		#endregion

		#region 退出窗体
		private void m_cmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#endregion

		#region 清空窗体
		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			this.m_dtpTimeForm.Value = DateTime.Now;
			this.m_dtpTimeTo.Value = DateTime.Now;
			this.m_txtPatientNO.Clear();
			this.m_txtInPatientNO.Clear();
			this.m_txtPatientName.Clear();
            this.m_txtDept.txtValuse = "";
		}
		#endregion

		private void m_dtpTimeForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
			this.m_dtpTimeTo.Select();
			}
		}

		private void m_dtpTimeTo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
			if(e.KeyCode==Keys.Enter)
			{
			this.m_cmdQuery.Focus();
			}
		}

        private void frmQueryCardiogramReport_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dtDept = new System.Data.DataTable();
            m_objManage.m_mthGetDEPTDESC(out dtDept);
            dtDept.Columns[0].ColumnName = "部门编码";
            dtDept.Columns[1].ColumnName = "部 门 名 称";
            dtDept.Columns[2].ColumnName = "拼音码";
            dtDept.Columns[3].ColumnName = "五笔码";
            m_txtDept.m_GetDataTable = dtDept;
            this.m_objRadioApply.Checked = false;
            this.m_objRadioApply.Checked = true;
            this.m_chkFinished.Checked = true;
            this.m_chkTimeFrom.Checked = true;
            this.m_dtpTimeForm.Value = System.DateTime.Now;
        }
        private void m_objRadioApply_CheckedChanged(object sender, EventArgs e)
        {
            if (m_objRadioApply.Checked == true)
            {
                this.checkBox1.Checked = false;
                this.checkBox1.Enabled = false;
                this.m_chkCReportNo.Enabled = false;
                this.m_chkApplyPart.Enabled = true;
                this.m_chkFinished.Checked =false;
                this.m_chkFinished.Enabled = true;
                this.m_chkReporter.Enabled = false;
                this.m_txtREPORTOR_NAME_VCHR.Enabled = false;
            }
            else
            {
                this.checkBox1.Enabled = true;
                this.m_chkApplyPart.Enabled = false;
                this.m_chkCReportNo.Enabled = true;
                this.m_chkFinished.Checked = false;
                this.m_chkFinished.Enabled = false;
                this.m_chkReporter.Enabled = true;
            }

        }

        private void m_chkApplyPart_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_chkApplyPart.Checked)
            {
                this.m_txtCheckPart.Enabled = true;
            }
            else
            {
                this.m_txtCheckPart.Clear();
                this.m_txtCheckPart.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void m_mthInitData()
        {
             //DataTable dtValue = null;
            //long lngRes = objSvc.m_lngGetDeptData(out dtValue);
            //if (lngRes > 0)
            //{
            //    m_objViewer.m_txtDept.m_GetDataTable = dtValue;
            //}
            DataTable dtDoctor = null;
           long lngRes = (new weCare.Proxy.ProxyRIS()).Service.m_lngGetDoctorData(out dtDoctor,frmQueryCardiogramReport.CurrentLoginInfo.m_strDepartmentID, false);
            if (lngRes > 0)
            {
                m_txtREPORTOR_NAME_VCHR.m_GetDataTable = dtDoctor;
            }
        }

	}
}

