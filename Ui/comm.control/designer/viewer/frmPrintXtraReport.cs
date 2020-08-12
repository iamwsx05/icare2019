using Common.Entity;
using DevExpress.XtraReports.UI;
using System;
using System.Windows.Forms;

namespace Common.Controls
{
    /// <summary>
    /// XtraReport预览、打印、导出
    /// </summary>
    public partial class frmPrintXtraReport : frmBasePopup
    {
        public frmPrintXtraReport(XtraReport _xrReport)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.xrReport = _xrReport;             
            }
        }

        /// <summary>
        /// 报表XR
        /// </summary>
        XtraReport xrReport { get; set; }

        private void frmPrintXtraReport_Load(object sender, EventArgs e)
        {
            try
            {
                uiHelper.BeginLoading(this);
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Location = new System.Drawing.Point(this.Location.X, 0);
                this.ucPrintControl.PrintingSystem = xrReport.PrintingSystem;
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
    }
}
