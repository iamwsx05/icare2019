using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MFZ
{
    public partial class frmStatistics : Form
    {
        public frmStatistics()
        {
            InitializeComponent();
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            DataTable dt;
            long res = clsTmdStatisticsSmp.s_object.m_lngFind(out dt);
            if (res> 0)
            {
                BindDataToDataWindow(dt);
            }
        }

        private void BindDataToDataWindow(DataTable dt) 
        {
            Sybase.DataWindow.DataWindowControl dwRep = this.m_dwStat;
            dwRep.SetRedrawOff();
            dwRep.Retrieve(dt);
            dwRep.CalculateGroups();

            if (dwRep.RowCount == 0)
            {
                dwRep.InsertRow(0);
            }

            dwRep.SetRedrawOn();
        }
    }
}