using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmDifPrice : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmDifPrice()
        {
            InitializeComponent();
        }

        private void m_BtnSearch_Click(object sender, EventArgs e)
        {
            #region
            clsDomainConrol_Medicne domain = new clsDomainConrol_Medicne();
            DataTable dtdein = new DataTable();
            DataTable dtEN = new DataTable();
            DataTable dtCH = new DataTable();
            DataTable dtEH = new DataTable();
            DataTable dtAll = new DataTable();
            domain.m_lngGetReportDataOfMonth1(out dtdein, out dtEN, out dtCH, out dtEH,out dtAll,dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(),0);
            //com.digitalwave.iCare.gui.HIS.baotable.rptMoney rpt1 = new com.digitalwave.iCare.gui.HIS.baotable.rptMoney();

            //((TextObject)rpt1.ReportDefinition.ReportObjects["Text3"]).Text = "ͳ�����ڣ�" + this.dateTimePicker1.Value.ToShortDateString() + " �� " + this.dateTimePicker2.Value.ToShortDateString();
            //switch (comboBox1.Text)
            //{
            //    case "��ҩͳ��":

            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "��ҩ";
            //        rpt1.SetDataSource(dtEN);

            //        break;
            //    case "�г�ҩͳ��":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "�г�ҩ";
            //        rpt1.SetDataSource(dtEH);

            //        break;
            //    case "�в�ҩͳ��":
            //       ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "�в�ҩ";
            //        rpt1.SetDataSource(dtCH);
            //        break;
            //}
            //rpt1.Refresh();
            //this.crystalReportViewer1.ReportSource = rpt1;
            #endregion
        }

        private void frmDifPrice_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Value = Convert.ToDateTime(clsPublicParm.s_datGetServerDate().Year.ToString() + "-" + clsPublicParm.s_datGetServerDate().Month.ToString() + "-" + "01"); 
        }
        
    }
}