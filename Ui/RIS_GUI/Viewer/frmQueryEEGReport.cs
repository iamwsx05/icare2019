using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.RIS
{
	public class frmQueryEEGReport : com.digitalwave.GUI_Base.frmQueryBaseForm
	{
		protected internal System.Windows.Forms.RadioButton m_rdbDCReport;
		protected internal System.Windows.Forms.RadioButton m_rdbCReport;
		private System.Windows.Forms.TextBox m_txtCReportNo;
		private System.Windows.Forms.CheckBox m_chkCReportNo;
		private com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage m_objParentViewer= null;
		private System.ComponentModel.IContainer components = null;
        private com.digitalwave.iCare.gui.RIS.clsDomainController_RISEEGManage m_objManage = null;
        protected com.digitalwave.controls.ctlTextBoxFind m_txtDept;
        public ListViewBox m_txtREPORTOR_NAME_VCHR;
        private CheckBox m_chkReporter;
		private com.digitalwave.iCare.gui.RIS.clsController_RISEEGManage m_objParentController=null;
		public frmQueryEEGReport()
		{
			// �õ����� Windows ���������������ġ�
			InitializeComponent();
			m_objManage = new  clsDomainController_RISEEGManage();
			m_objParentController = new clsController_RISEEGManage();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��
		}
	
		
		

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region ��������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryEEGReport));
            this.m_rdbDCReport = new System.Windows.Forms.RadioButton();
            this.m_rdbCReport = new System.Windows.Forms.RadioButton();
            this.m_txtCReportNo = new System.Windows.Forms.TextBox();
            this.m_chkCReportNo = new System.Windows.Forms.CheckBox();
            this.m_txtDept = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtREPORTOR_NAME_VCHR = new ListViewBox();
            this.m_chkReporter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_chkPatientNO
            // 
            this.m_chkPatientNO.Location = new System.Drawing.Point(12, 116);
            this.m_chkPatientNO.CheckedChanged += new System.EventHandler(this.m_chkPatientNO_CheckedChanged);
            // 
            // m_chkDept
            // 
            this.m_chkDept.Location = new System.Drawing.Point(12, 200);
            this.m_chkDept.CheckedChanged += new System.EventHandler(this.m_chkDept_CheckedChanged);
            // 
            // m_chkPatientName
            // 
            this.m_chkPatientName.Location = new System.Drawing.Point(12, 172);
            this.m_chkPatientName.CheckedChanged += new System.EventHandler(this.m_chkPatientName_CheckedChanged);
            // 
            // m_chkInPatientNO
            // 
            this.m_chkInPatientNO.Location = new System.Drawing.Point(12, 144);
            this.m_chkInPatientNO.CheckedChanged += new System.EventHandler(this.m_chkInPatientNO_CheckedChanged);
            // 
            // m_dtpTimeForm
            // 
            this.m_dtpTimeForm.CustomFormat = "yyyy��MM��dd��";
            this.m_dtpTimeForm.Location = new System.Drawing.Point(100, 60);
            this.m_dtpTimeForm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeForm_KeyDown);
            // 
            // m_dtpTimeTo
            // 
            this.m_dtpTimeTo.CustomFormat = "yyyy��MM��dd�� ";
            this.m_dtpTimeTo.Location = new System.Drawing.Point(100, 88);
            this.m_dtpTimeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_chkTimeFrom
            // 
            this.m_chkTimeFrom.Checked = true;
            this.m_chkTimeFrom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkTimeFrom.Location = new System.Drawing.Point(12, 64);
            this.m_chkTimeFrom.CheckedChanged += new System.EventHandler(this.m_chkTimeFrom_CheckedChanged);
            // 
            // m_txtPatientNO
            // 
            this.m_txtPatientNO.Enabled = false;
            this.m_txtPatientNO.Location = new System.Drawing.Point(100, 116);
            this.m_txtPatientNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_txtInPatientNO
            // 
            this.m_txtInPatientNO.Enabled = false;
            this.m_txtInPatientNO.Location = new System.Drawing.Point(100, 144);
            this.m_txtInPatientNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Enabled = false;
            this.m_txtPatientName.Location = new System.Drawing.Point(100, 172);
            this.m_txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_cmdClear
            // 
            this.m_cmdClear.Location = new System.Drawing.Point(108, 288);
            // 
            // m_cmdQuery
            // 
            this.m_cmdQuery.Location = new System.Drawing.Point(8, 288);
            this.m_cmdQuery.Click += new System.EventHandler(this.m_cmdQuery_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.Location = new System.Drawing.Point(208, 288);
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_lblTimeTo
            // 
            this.m_lblTimeTo.Location = new System.Drawing.Point(72, 92);
            // 
            // m_rdbDCReport
            // 
            this.m_rdbDCReport.Location = new System.Drawing.Point(164, 24);
            this.m_rdbDCReport.Name = "m_rdbDCReport";
            this.m_rdbDCReport.Size = new System.Drawing.Size(124, 24);
            this.m_rdbDCReport.TabIndex = 1026;
            this.m_rdbDCReport.Text = "EEG���浥��ѯ";
            // 
            // m_rdbCReport
            // 
            this.m_rdbCReport.Checked = true;
            this.m_rdbCReport.Location = new System.Drawing.Point(16, 24);
            this.m_rdbCReport.Name = "m_rdbCReport";
            this.m_rdbCReport.Size = new System.Drawing.Size(124, 24);
            this.m_rdbCReport.TabIndex = 1025;
            this.m_rdbCReport.TabStop = true;
            this.m_rdbCReport.Text = "TCD���浥��ѯ";
            // 
            // m_txtCReportNo
            // 
            this.m_txtCReportNo.Enabled = false;
            this.m_txtCReportNo.Location = new System.Drawing.Point(100, 227);
            this.m_txtCReportNo.Name = "m_txtCReportNo";
            this.m_txtCReportNo.Size = new System.Drawing.Size(184, 23);
            this.m_txtCReportNo.TabIndex = 1028;
            this.m_txtCReportNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtpTimeTo_KeyDown);
            // 
            // m_chkCReportNo
            // 
            this.m_chkCReportNo.Location = new System.Drawing.Point(12, 227);
            this.m_chkCReportNo.Name = "m_chkCReportNo";
            this.m_chkCReportNo.Size = new System.Drawing.Size(84, 24);
            this.m_chkCReportNo.TabIndex = 1027;
            this.m_chkCReportNo.Text = "�Ե�ͼ��";
            this.m_chkCReportNo.CheckedChanged += new System.EventHandler(this.m_chkCReportNo_CheckedChanged);
            // 
            // m_txtDept
            // 
            this.m_txtDept.Enabled = false;
            this.m_txtDept.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDept.intHeight = 100;
            this.m_txtDept.IsEnterShow = true;
            this.m_txtDept.isHide = 4;
            this.m_txtDept.isTxt = 1;
            this.m_txtDept.isUpOrDn = 0;
            this.m_txtDept.isValuse = 0;
            this.m_txtDept.Location = new System.Drawing.Point(100, 200);
            this.m_txtDept.m_IsHaveParent = false;
            this.m_txtDept.m_strParentName = "";
            this.m_txtDept.Name = "m_txtDept";
            this.m_txtDept.nextCtl = this.m_cmdQuery;
            this.m_txtDept.Size = new System.Drawing.Size(184, 24);
            this.m_txtDept.TabIndex = 1030;
            this.m_txtDept.txtValuse = "";
            this.m_txtDept.VsLeftOrRight = 1;
            // 
            // m_txtREPORTOR_NAME_VCHR
            // 
            this.m_txtREPORTOR_NAME_VCHR.BackColor = System.Drawing.SystemColors.Window;
            this.m_txtREPORTOR_NAME_VCHR.Enabled = false;
            this.m_txtREPORTOR_NAME_VCHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtREPORTOR_NAME_VCHR.intHeight = 200;
            this.m_txtREPORTOR_NAME_VCHR.IsEnterShow = true;
            this.m_txtREPORTOR_NAME_VCHR.isHide = 3;
            this.m_txtREPORTOR_NAME_VCHR.isTxt = 1;
            this.m_txtREPORTOR_NAME_VCHR.isUpOrDn = 1;
            this.m_txtREPORTOR_NAME_VCHR.isValuse = 3;
            this.m_txtREPORTOR_NAME_VCHR.Location = new System.Drawing.Point(100, 258);
            this.m_txtREPORTOR_NAME_VCHR.m_IsHaveParent = false;
            this.m_txtREPORTOR_NAME_VCHR.m_strParentName = "";
            this.m_txtREPORTOR_NAME_VCHR.Name = "m_txtREPORTOR_NAME_VCHR";
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemBackColor = System.Drawing.Color.ForestGreen;
            this.m_txtREPORTOR_NAME_VCHR.SelectedItemForeColor = System.Drawing.Color.White;
            this.m_txtREPORTOR_NAME_VCHR.Size = new System.Drawing.Size(184, 23);
            this.m_txtREPORTOR_NAME_VCHR.TabIndex = 1035;
            this.m_txtREPORTOR_NAME_VCHR.txtValuse = "";
            this.m_txtREPORTOR_NAME_VCHR.VsLeftOrRight = 1;
            // 
            // m_chkReporter
            // 
            this.m_chkReporter.Location = new System.Drawing.Point(12, 257);
            this.m_chkReporter.Name = "m_chkReporter";
            this.m_chkReporter.Size = new System.Drawing.Size(84, 24);
            this.m_chkReporter.TabIndex = 1034;
            this.m_chkReporter.Text = "����ҽʦ";
            this.m_chkReporter.CheckedChanged += new System.EventHandler(this.m_chkReporter_CheckedChanged);
            // 
            // frmQueryEEGReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(296, 329);
            this.Controls.Add(this.m_txtREPORTOR_NAME_VCHR);
            this.Controls.Add(this.m_chkReporter);
            this.Controls.Add(this.m_txtDept);
            this.Controls.Add(this.m_txtCReportNo);
            this.Controls.Add(this.m_chkCReportNo);
            this.Controls.Add(this.m_rdbDCReport);
            this.Controls.Add(this.m_rdbCReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(340, 260);
            this.Name = "frmQueryEEGReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "�Ե�ͼ�����ѯ";
            this.Load += new System.EventHandler(this.frmQueryEEGReport_Load);
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
            this.Controls.SetChildIndex(this.m_rdbCReport, 0);
            this.Controls.SetChildIndex(this.m_rdbDCReport, 0);
            this.Controls.SetChildIndex(this.m_chkCReportNo, 0);
            this.Controls.SetChildIndex(this.m_txtCReportNo, 0);
            this.Controls.SetChildIndex(this.m_txtDept, 0);
            this.Controls.SetChildIndex(this.m_chkReporter, 0);
            this.Controls.SetChildIndex(this.m_txtREPORTOR_NAME_VCHR, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#region ����CheckBox�¼�
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

		#region ��ȡ������
		public void m_mthGetParentApperance(com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage infrmRISEEGReportNamage)
		{
			m_objParentViewer = infrmRISEEGReportNamage;
		}
		#endregion
		#region ����������ϲ�ѯ�Ե�ͼ����
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
            //����ҽʦ
            string strReporter = "";
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
            if (this.m_chkReporter.Checked)
            {
                strReporter = this.m_txtREPORTOR_NAME_VCHR.txtValuse.ToString();
            }
			if(this.m_chkCReportNo.Checked)
			{
				strReportNo = this.m_txtCReportNo.Text.ToString().Trim();
			}
			if(this.m_rdbCReport.Checked)
			{
				m_objParentViewer.m_tbcMain.SelectedIndex=1;
                m_objParentViewer.m_lsvTCDReportList.Items.Clear();
				clsRIS_TCD_REPORT_VO[] objResultArr = null;
                lngRes = m_objManage.m_lngGetTCDReportByCondition(strFromDat, strToDat, strPatientNo, strInPatientNo, strPatientName, strDept, strReportNo, strReporter
					,out objResultArr);
				if(lngRes > 0 && objResultArr != null)
				{
					if(objResultArr.Length > 0)
                    {
                        #region ��������
                        frmRISEEGReportNamage.strOPQueryButtonName = "��ѯ";
                        m_objParentViewer.strFromDat1 = strFromDat;
                        m_objParentViewer.strToDat1 = strToDat;
                        m_objParentViewer.strPatientNo1 = strPatientNo;
                        m_objParentViewer.strInPatientNo1 = strInPatientNo;
                        m_objParentViewer.strPatientName1 = strPatientName;
                        m_objParentViewer.strDept1 = strDept;
                        m_objParentViewer.strReportNo1 = strReportNo;
                        m_objParentViewer.strReporter1 = strReporter;
                        #endregion 

						m_objParentController.Set_GUI_Apperance(m_objParentViewer);
						m_objParentController.m_mthShowTCDReportByCondition(objResultArr);
						this.Close();
					}
					else
					{
						MessageBox.Show("û�з��������ļ�¼��");
					}
				}
				else
				{
					MessageBox.Show("û�з��������ļ�¼��");
				}
			}
			else
			{
				m_objParentViewer.m_tbcMain.SelectedIndex=2;
                m_objParentViewer.m_lsvEEGReportList.Items.Clear();
				clsRIS_EEG_REPORT_VO[] objResultArr = null;
				lngRes = m_objManage.m_lngGetEEGReportByCondition(strFromDat,strToDat,strPatientNo,strInPatientNo,strPatientName,strDept,strReportNo,strReporter
					,out objResultArr);
				if(lngRes > 0 && objResultArr != null)
				{
					if(objResultArr.Length > 0)
                    {
                        #region ��������
                        frmRISEEGReportNamage.strOPQueryButtonName = "��ѯ";
                        m_objParentViewer.strFromDat2 = strFromDat;
                        m_objParentViewer.strToDat2 = strToDat;
                        m_objParentViewer.strPatientNo2 = strPatientNo;
                        m_objParentViewer.strInPatientNo2 = strInPatientNo;
                        m_objParentViewer.strPatientName2 = strPatientName;
                        m_objParentViewer.strDept2 = strDept;
                        m_objParentViewer.strReportNo2 = strReportNo;
                        m_objParentViewer.strReporter2 = strReporter;
                        #endregion 
                        m_objParentController.Set_GUI_Apperance(m_objParentViewer);
						m_objParentController.m_mthShowEEGReportByCondition(objResultArr);
						this.Close();
					}
					else
					{
						MessageBox.Show("û�з��������ļ�¼��");
					}
				}
				else
				{
					MessageBox.Show("û�з��������ļ�¼��");
				}
			}
		}
		#endregion
		#region �˳�����
		private void m_cmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#endregion

		#region ��մ���
		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			this.m_dtpTimeForm.Value = DateTime.Now;
			this.m_dtpTimeTo.Value = DateTime.Now;
			this.m_txtPatientNO.Clear();
			this.m_txtInPatientNO.Clear();
			this.m_txtPatientName.Clear();
            this.m_txtDept.txtValuse = ""; ;
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

        private void frmQueryEEGReport_Load(object sender, EventArgs e)
        {
            clsDomainController_RISCardiogramManage m_objManage1 = new clsDomainController_RISCardiogramManage();
            System.Data.DataTable dtDept = new System.Data.DataTable();
            m_objManage1.m_mthGetDEPTDESC(out dtDept);
            dtDept.Columns[0].ColumnName = "���ű���";
            dtDept.Columns[1].ColumnName = "�� �� �� ��";
            dtDept.Columns[2].ColumnName = "ƴ����";
            dtDept.Columns[3].ColumnName = "�����";
            m_txtDept.m_GetDataTable = dtDept;
        }
        /// <summary>
        /// ��ȡ����ҽ������
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
            long lngRes = (new weCare.Proxy.ProxyRIS()).Service.m_lngGetDoctorData(out dtDoctor, frmQueryCardiogramReport.CurrentLoginInfo.m_strDepartmentID, false);
            if (lngRes > 0)
            {
                m_txtREPORTOR_NAME_VCHR.m_GetDataTable = dtDoctor;
            }
        }
	}
}

