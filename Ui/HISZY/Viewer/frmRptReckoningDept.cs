using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �շ�Ա�սᱨ��UI��
    /// </summary>
    public partial class frmRptReckoningDept : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        /// <summary>
        /// ����ҵ����
        /// </summary>
        private clsCtl_Report objReport;        
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        public frmRptReckoningDept()
        {
            InitializeComponent();
            objReport = new clsCtl_Report();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRptReckoningDept_Load(object sender, EventArgs e)
        {
            this.dwRep.LibraryList = clsPublic.PBLPath;
            this.dwRep.DataWindowObject = "d_bih_reckoningdept_new";
            this.dwRep.InsertRow(0);
            dwRep.Modify("t_title.text = '" + this.objReport.HospitalName + "סԺ�ɿ��սᱨ��'");
            dwRep.Modify("t_dyrq.text = '" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.dwRep.PrintProperties.Preview = !this.dwRep.PrintProperties.Preview;
            this.dwRep.PrintProperties.ShowPreviewRulers = this.dwRep.PrintProperties.Preview;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsPublic.ChoosePrintDialog(this.dwRep, true);          
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dwRep.RowCount > 0)
            {
                clsPublic.ExportDataWindow(this.dwRep, null);
            }
        }        

        private void btnStat_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(this.dteBegin.Value.ToString("yyyy-MM-dd") + " 00:00:01") > Convert.ToDateTime(this.dteEnd.Value.ToString("yyyy-MM-dd") + " 00:00:01"))
            {
                MessageBox.Show("��ʼ���ڲ��ܴ��ڽ����������ڡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);             
            }
            else
            {
                clsPublic.PlayAvi("findFILE.avi", "����ͳ�Ʒ������ݣ����Ժ�...");
                this.objReport.m_mthRptReckoningDept(this.dteBegin.Value.ToString("yyyy-MM-dd"), this.dteEnd.Value.ToString("yyyy-MM-dd"), this.dwRep);
                clsPublic.CloseAvi();
            }
        }              

    }
}
