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

            //((TextObject)rpt1.ReportDefinition.ReportObjects["Text3"]).Text = "ͳ�����ڣ�" + this.dateTimePicker1.Value.ToShortDateString() + " �� " + this.dateTimePicker2.Value.ToShortDateString();
            ////this.objController.m_objComInfo.m_strGetHospitalTitle() +
            //((TextObject)rpt1.ReportDefinition.ReportObjects["Text1"]).Text =  "�̿���ϸ����";
            //int status=1;
            //switch (comboBox1.Text)
            //{
            //    case "��ҩͳ��":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "ҩƷ���ͣ���ҩ";
            //        status = 1;

            //        break;
            //    case "�г�ҩͳ��":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "ҩƷ���ͣ��г�ҩ";
            //        status = 3;

            //        break;
            //    case "�в�ҩͳ��":
            //        ((TextObject)rpt1.ReportDefinition.ReportObjects["Text2"]).Text = "ҩƷ���ͣ��в�ҩ";
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