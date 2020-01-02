using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlLibrary;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ��Ʊ��ϸ�嵥UI��
    /// </summary>
    public partial class frmRptInvoiceEntry : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ����ҵ����
        /// </summary>
        private clsCtl_Report objReport;

        /// <summary>
        /// ����
        /// </summary>
        public frmRptInvoiceEntry()
        {
            InitializeComponent();
            objReport = new clsCtl_Report();
        }

        private void frmRptInvoiceEntry_Load(object sender, EventArgs e)
        {
            this.dwRep.LibraryList = clsPublic.PBLPath;
            this.dwRep.DataWindowObject = "d_bih_invoice_entry_diff";
            this.dwRep.InsertRow(0);
            this.txtInvoiceNO.Focus();
            //this.dwRep.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.dwRep.Width) / 2 - 50, this.dwRep.Location.Y);
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            string InvoiceNO = this.txtInvoiceNO.Text.Trim();

            if (InvoiceNO == "")
            {
                MessageBox.Show("�����뷢Ʊ���롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            clsPublic.PlayAvi("findFILE.avi", "���������嵥��Ϣ�����Ժ�...");

            this.dwRep.PrintProperties.Preview = false;
            this.objReport.m_mthRptInvoiceEntry(InvoiceNO.ToUpper(), this.dwRep);
            this.dwRep.PrintProperties.Preview = true;
            this.dwRep.PrintProperties.ShowPreviewRulers = true;

            clsPublic.CloseAvi();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}