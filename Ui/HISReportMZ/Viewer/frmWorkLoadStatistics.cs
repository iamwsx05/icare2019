using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISReportMZ.Viewer
{
    public partial class frmWorkLoadStatistics : Form
    {
        public frmWorkLoadStatistics()
        {
            InitializeComponent();
        }

        private void frmWorkLoadStatistics_Load(object sender, EventArgs e)
        {
            this.dwRep.LibraryList = Application.StartupPath + @"\dgcs_report.pbl";
            this.dwRep.DataWindowObject = "t_opr_workload";
            this.dwRep.InsertRow(0);
            this.dwRep.PrintProperties.ShowPreviewRulers = true;
            this.dwRep.PrintProperties.Preview = true;

            this.dtm_start.Value = DateTime.Now;
            this.dtm_end.Value = DateTime.Now;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.PlayAvi("");
                string dtmStart = this.dtm_start.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                string dtmEnd = this.dtm_end.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                string Sql = string.Empty;

                Sql = @"select b.lastname_vchr, count(1)
                          from t_opr_recipesend a
                         inner join t_bse_employee b
                            on a.treatemp_chr = b.empid_chr
                         where (a.treatdate_dat between to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and
                               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
                           and (a.pstatus_int = 2 or a.pstatus_int = 3)
                         group by b.lastname_vchr";

                Sql = string.Format(Sql, dtmStart, dtmEnd);
                DataTable dt = null;
                (new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dwRep.Reset();
                    this.dwRep.SetRedrawOff();
                    this.dwRep.Retrieve(dt);
                    this.dwRep.CalculateGroups();
                    this.dwRep.SetRedrawOn();
                    this.dwRep.Refresh();
                    this.dwRep.PrintProperties.ShowPreviewRulers = true;
                    this.dwRep.PrintProperties.Preview = true;
                }
                else
                {
                    MessageBox.Show("查无数据.");
                }
            }
            catch
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.CloseAvi();
            }
            finally
            {
                com.digitalwave.iCare.gui.HIS.clsPublic.CloseAvi();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.dwRep.PrintProperties.Preview = !this.dwRep.PrintProperties.Preview;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            com.digitalwave.iCare.gui.HIS.clsPublic.ChoosePrintDialog(this.dwRep, true);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            com.digitalwave.iCare.gui.HIS.clsPublic.ExportDataWindow(this.dwRep);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
