using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll 
using weCare.Core.Entity;
using com.digitalwave.iCare.common;
namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public partial class frmCheckOutOfDayNewAuto : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        internal string m_rptId;

        public frmCheckOutOfDayNewAuto()
        {
            InitializeComponent();
        }

        public void ShowWithParm(string p_rptId)
        {
            this.m_rptId = p_rptId;
            this.Show();
        }

        /// <summary>
        /// ���ô������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtlCheckOutOfDayNewAuto();
            this.objController.Set_GUI_Apperance(this);
        }

        private void frmCheckOutOfDayNew_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            starDate.Value = Convert.ToDateTime(starDate.Value.Year.ToString() + "-" + starDate.Value.Month.ToString() + "-" + "01");
            //if (isDoctorDean == true)
            //{
            //    DataTable dtBalanceemp = new DataTable();
            //    clsDomainControl_Register domain = new clsDomainControl_Register();
            //    domain.m_lngReturnAllBALANCEEMP(out dtBalanceemp);
            //    if (dtBalanceemp.Rows.Count > 0)
            //    {
            //        for (int i1 = 0; i1 < dtBalanceemp.Rows.Count; i1++)
            //        {
            //            m_cboCheckMan.Item.Add(dtBalanceemp.Rows[i1]["LASTNAME_VCHR"].ToString(), dtBalanceemp.Rows[i1]["BALANCEEMP_CHR"].ToString());
            //        }
            //    }
            //}
            //Hospital_No = this.objController.m_objComInfo.m_mthGetHospitalNo();

            //this.ctlprintShow2.IsShowClose = false;
            //ctlprintShow2.setDocument = printDocument1;
            this.m_dwShow.LibraryList = Application.StartupPath + "\\PB_OP.pbl";
            ((clsCtlCheckOutOfDayNewAuto)this.objController).m_mthGetParameters();
            ((clsCtlCheckOutOfDayNewAuto)this.objController).Reset();
            this.Cursor = Cursors.Default;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ((clsCtlCheckOutOfDayNewAuto)this.objController).Reset();
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ע�⣺���ʺ����޸����ݣ��Ƿ�Ҫ���ʣ�", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ((clsCtlCheckOutOfDayNewAuto)this.objController).CheckData();
            }
        }

        private void ctlDgFind_m_evtClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (ctlDgFind.CurrentCell.RowNumber == e.m_intRowNumber)
            {
                ((clsCtlCheckOutOfDayNewAuto)this.objController).dgSelect();
            }
            //ctlprintShow2.Tag = "printDocument1";
        }

        private void ctlDgFind_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            ((clsCtlCheckOutOfDayNewAuto)this.objController).dgSelect();
        }

        private void starDate_ValueChanged(object sender, EventArgs e)
        {
            ((clsCtlCheckOutOfDayNewAuto)this.objController).FindHistory();
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            ((clsCtlCheckOutOfDayNewAuto)this.objController).FindHistory();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //this.m_dwShow.Print();
            PrintDialog pDiag = new PrintDialog();
            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                this.m_dwShow.PrintProperties.PrinterName = pDiag.PrinterSettings.PrinterName;
                this.m_dwShow.Print();
            }
            pDiag = null;
        }
    }
}