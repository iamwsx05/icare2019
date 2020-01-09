using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll 
using weCare.Core.Entity;
using com.digitalwave.iCare.common;
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public partial class frmRptOpDoctorPerformance : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ҽ��ID��
        /// </summary>
        internal string doctIDArr = "";

        /// <summary>
        /// ѡ��״̬
        /// </summary>
        internal bool selectStatus = false;

        public frmRptOpDoctorPerformance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���ÿ�����
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtlRptOpDoctorPerformance();
            this.objController.Set_GUI_Apperance(this);
        }

        private string _strReportName;
        /// <summary>
        /// ��������
        /// </summary>
        public string StrReportName
        {
            get { return _strReportName; }
            set { _strReportName = value; }
        }
        private void frmRptOpDoctorPerformance_Load(object sender, EventArgs e)
        {
            //this.rdoDoctor.Checked = true;
            //this.buttonXP1.Enabled = true;
            //this.btnByDept.Enabled = false;          
            this.rdoDept.Checked = true;
            this.buttonXP1.Enabled = false;
            this.btnByDept.Enabled = true;
            this.m_dwShow.LibraryList = Application.StartupPath + "\\PB_OP.pbl";
            this.m_dwShow.DataWindowObject = "d_op_doctorperformance";
            dteRq1.Value = Convert.ToDateTime(dteRq1.Value.Year.ToString() + "-" + dteRq1.Value.Month.ToString() + "-" + "01");
            this.cmbSeachType.SelectedIndex = 0;
            _strReportName = this.m_dwShow.Describe("t_title.text");
            this.m_dwShow.Modify("t_title.text = '" + this.objController.m_objComInfo.m_strGetHospitalTitle() + _strReportName + "'");
        }

        #region ѡ��ҽ��
        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        private void m_mthChooseDoctor()
        {
            frmAidChooseDoct fDoct = new frmAidChooseDoct();
            if (fDoct.ShowDialog() == DialogResult.OK)
            {
                this.doctIDArr = fDoct.DoctIDArr;
            }
        }
        #endregion

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.m_dwShow.PrintProperties.Preview = !this.m_dwShow.PrintProperties.Preview;
            this.m_dwShow.PrintProperties.ShowPreviewRulers = this.m_dwShow.PrintProperties.Preview;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.m_dwShow.RowCount > 0)
            {
                clsPublic.ExportDataWindow(this.m_dwShow, null);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog pDiag = new PrintDialog();
            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                this.m_dwShow.PrintProperties.PrinterName = pDiag.PrinterSettings.PrinterName;
                this.m_dwShow.Print();
            }
            pDiag = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string beginDate = this.dteRq1.Value.ToString("yyyy-MM-dd");
            string endDate = this.dteRq2.Value.ToString("yyyy-MM-dd");

            if (Convert.ToDateTime(beginDate + " 00:00:01") > Convert.ToDateTime(endDate + " 00:00:01"))
            {
                MessageBox.Show("��ʼ���ڲ��ܴ��ڽ������ڡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                //clsPublic.PlayAvi("findFILE.avi", "���ڽ������ݻ��ܣ����Ժ�...");
                ((clsCtlRptOpDoctorPerformance)this.objController).GetRptDoctorPerformance(beginDate, endDate, this.cmbSeachType.SelectedIndex.ToString());
            }
            finally
            {
                //clsPublic.CloseAvi();
            }
        }
        public string m_strDoctorID = string.Empty;
        private void buttonXP1_Click(object sender, EventArgs e)
        {
            frmAidChooseDoct m_objForm = new frmAidChooseDoct();
            if (m_objForm.ShowDialog() == DialogResult.OK)
            {
                m_strDoctorID = m_objForm.DoctIDArr;
            }
            else
            {
                m_strDoctorID = string.Empty;
            }
        }
        public List<string> DeptIDArr = new List<string>();
        private void btnByDept_Click(object sender, EventArgs e)
        {
            frmAidOutpbillDeptList fDept = new frmAidOutpbillDeptList();
            if (fDept.ShowDialog() == DialogResult.OK)
            {
                DeptIDArr = fDept.DeptIDArr;
            }
        }

        private void rdoDoctor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoDoctor.Checked == false)
            {
                m_strDoctorID = string.Empty;
                this.buttonXP1.Enabled = false;
            }
            else
            {
                this.buttonXP1.Enabled = true;
            }
        }

        private void rdoDept_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoDept.Checked == false)
            {
                DeptIDArr = null;
                this.btnByDept.Enabled = false;
            }
            else
            {
                this.btnByDept.Enabled = true;
            }
        }

    }
}