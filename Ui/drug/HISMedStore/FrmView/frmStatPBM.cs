using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmStatPBM : Form
    {
        public frmStatPBM()
        {
            InitializeComponent();
        }

        private void frmStatPBM_Load(object sender, EventArgs e)
        {
            this.dwMed.LibraryList = Application.StartupPath + "\\pb_new.pbl";
            this.dwMed.DataWindowObject = "d_statpbm";
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            string beginDate = this.dteStart.Value.ToString("yyyy-MM-dd");
            string endDate = this.dteEnd.Value.ToString("yyyy-MM-dd");
            if (Convert.ToDateTime(beginDate + " 00:00:01") > Convert.ToDateTime(endDate + " 00:00:01"))
            {
                MessageBox.Show("开始日期不能大于结束日期。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string medCode = this.txtMedCode.Text.Trim();
            int opIp = this.cboType.SelectedIndex;
            int allTimes = 0;
            int djdsTimes = 0;
            try
            {
                this.dwMed.Reset();
                clsPublic.PlayAvi("数据统计中，请稍候...");
                clsDomainControlOPMedStore svc = new clsDomainControlOPMedStore();
                DataTable dtResult = svc.StatPBM(beginDate, endDate, medCode, opIp, out allTimes, out djdsTimes);
                svc = null;
                //this.dwMed.Retrieve(dtResult);
                this.dwMed.SetRedrawOff();
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        int rowIndex = this.dwMed.InsertRow(0);
                        this.dwMed.SetItemString(rowIndex, "medCode", dr["medCode"].ToString());
                        this.dwMed.SetItemString(rowIndex, "medname", dr["medname"].ToString());
                        this.dwMed.SetItemString(rowIndex, "unit", dr["unit"].ToString());
                        this.dwMed.SetItemDecimal(rowIndex, "price", Convert.ToDecimal(dr["price"].ToString()));
                        this.dwMed.SetItemDecimal(rowIndex, "tradprice", Convert.ToDecimal(dr["tradprice"].ToString()));
                        this.dwMed.SetItemDecimal(rowIndex, "qty", Convert.ToDecimal(dr["qty"].ToString()));
                        this.dwMed.SetItemDecimal(rowIndex, "total", Convert.ToDecimal(dr["total"].ToString()));
                        this.dwMed.SetItemDecimal(rowIndex, "tradtotal", Convert.ToDecimal(dr["tradtotal"].ToString()));
                    }
                }
                this.dwMed.Modify("t_date.text='" + beginDate + " 至 " + endDate + "'");
                this.dwMed.Modify("t_times.text='" + string.Format("代煎代送 {0}剂，自煎代送 {1}剂。", djdsTimes, allTimes - djdsTimes) + "'");
                this.dwMed.SetRedrawOn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.dwMed.Refresh();
                clsPublic.CloseAvi();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dwMed.RowCount > 0)
            {
                try
                {
                    clsVolDatawindowToExcel[] volExcel = new clsVolDatawindowToExcel[1];
                    volExcel[0] = new clsVolDatawindowToExcel(1);
                    volExcel[0].m_rowheight[0] = 20;
                    volExcel[0].m_title_text[0] = "";
                    volExcel[0].m_HorizontalAlignment[0] = "0";
                    volExcel[0].m_firstcommn[0] = "A1";
                    volExcel[0].m_endcommn[0] = "ALL";
                    clsPublic.ExportDataWindow(this.dwMed, volExcel);
                }
                catch
                {
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsPublic.ChoosePrintDialog(this.dwMed, true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
