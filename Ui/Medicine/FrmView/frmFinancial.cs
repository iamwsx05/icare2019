using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmFinancial : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmFinancial()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            m_cobFinancial.Enabled = radioButton1.Checked;
        }

        private void m_txtFindPharm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Point p = this.m_txtFindPharm.Parent.PointToScreen(this.m_txtFindPharm.Location);
                p.Offset(0, 10);
                p = this.m_txtFindPharm.FindForm().PointToClient(p);
                p.Y += this.m_txtFindPharm.Height;
                m_DlgResult.Location = p;
                this.m_DlgResult.Visible = true;
                this.m_DlgResult.Focus();
                this.m_DlgResult.strSTORAGEID = "";
                this.m_DlgResult.FindMedmode = 0;
                this.m_DlgResult.m_txtFindMed.Text = m_txtFindPharm.Text;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = radioButton2.Checked;
            dateTimePicker2.Enabled = radioButton2.Checked;
        }

        private void frmFinancial_Load(object sender, EventArgs e)
        {
            clsDomainControlMedicineStorageCheck objSVC = new clsDomainControlMedicineStorageCheck();
            System.Data.DataTable dt = new DataTable();
            long lngRes = objSVC.m_lngGetPeriod(out dt);
            if (lngRes > 0 && dt.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    this.m_cobFinancial.Item.Add(dt.Rows[i1]["PERIODNAME"].ToString(), dt.Rows[i1]["PERIODID_CHR"].ToString());
                }
            }
            ctlprintShow1.setDocument = this.printDocument1;
        }

        private void m_DlgResult_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
        {
            this.m_txtFindPharm.Text = e.ReturnVo.strMEDICINENAME_VCHR;
            this.m_txtFindPharm.Tag = e.ReturnVo.strMEDICINEID_CHR;
            this.m_DlgResult.Visible = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}