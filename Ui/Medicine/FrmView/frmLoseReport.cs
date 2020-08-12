using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmLoseReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmLoseReport()
        {
            InitializeComponent();
        }

        private void m_BtnSearch_Click(object sender, EventArgs e)
        {
            #region
            //clsDomainConrol_Medicne domain = new clsDomainConrol_Medicne();
            //DataTable dtdein = new DataTable();
            //com.digitalwave.iCare.gui.HIS.baotable.loseReport rpt1 = new com.digitalwave.iCare.gui.HIS.baotable.loseReport();

            //((TextObject)rpt1.ReportDefinition.ReportObjects["Text3"]).Text = "统计日期：" + this.dateTimePicker1.Value.ToShortDateString() + " 到 " + this.dateTimePicker2.Value.ToShortDateString();
            ////this.objController.m_objComInfo.m_strGetHospitalTitle() +
            //((TextObject)rpt1.ReportDefinition.ReportObjects["Text1"]).Text =  "盘亏明细报表";
            //int status=1;
            //switch (comboBox1.Text)
            //{
            //    case "西药统计":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "药品类型：西药";
            //        status = 1;

            //        break;
            //    case "中成药统计":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "药品类型：中成药";
            //        status = 3;

            //        break;
            //    case "中草药统计":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "药品类型：中草药";
            //        status = 2;
            //        break;
            //}
            //domain.m_lngCheckLoseDe(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(), status, out dtdein);
            //rpt1.SetDataSource(dtdein);
            //rpt1.Refresh();
            //this.crystalReportViewer1.ReportSource = rpt1;
            #endregion
        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}