using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    /// <summary>
    /// �ɹ��ƻ�����ӡ
    /// </summary>
    public partial class frmStockPlan_DetailReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public DataTable p_dtbVal;
        /// <summary>
        /// �ɹ��ƻ�����ӡ
        /// </summary>
        public frmStockPlan_DetailReport()
        {
            InitializeComponent();

            datWindow.LibraryList = clsMedicineStoreFormFactory.PBLPath;

        }

        private void frmPurchase_DetailReport_Load(object sender, EventArgs e)
        {
            m_mthSetDataToReport();
            datWindow.PrintProperties.Preview = true;
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            clsCtl_Public clsPub = new clsCtl_Public();
            clsPub.ChoosePrintDialog(datWindow, true);
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ��������������
        /// </summary>
        private void m_mthSetDataToReport()
        {
            this.objController = new clsCtl_StockPlan_Detail();
            datWindow.SetRedrawOff();
            datWindow.Retrieve(p_dtbVal);
            datWindow.Refresh();
            datWindow.SetRedrawOn();
        }
    }
}