using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Controls;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ��ӡ��ҩ֪ͨ��
    /// </summary>
    public partial class frmNewPurchaseMedicineReport:Form
    {
        #region ����
        /// <summary>
        /// ��ϸ
        /// </summary>
        internal DataTable p_dtbVal;
        /// <summary>
        /// �ֿ�����
        /// </summary>
        internal string p_strStorageName;
        /// <summary>
        /// �������
        /// </summary>
        internal string p_strDate;
        #endregion 

        /// <summary>
        /// ��ӡ��ҩ֪ͨ��
        /// </summary>
        public frmNewPurchaseMedicineReport()
        {
            InitializeComponent();
        }

        #region �����¼�
        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            PrintDialog pDiag = new PrintDialog();
            pDiag.UseEXDialog = true;
            pDiag.AllowSomePages = true;
            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                datWindow.PrintProperties.PrinterName = pDiag.PrinterSettings.PrinterName;

                if (pDiag.PrinterSettings.PrintRange == System.Drawing.Printing.PrintRange.SomePages)
                {
                    datWindow.PrintProperties.PageRange = pDiag.PrinterSettings.FromPage.ToString() + "-" + pDiag.PrinterSettings.ToPage.ToString();
                }
                try
                {
                    datWindow.Print(true);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            }
            pDiag = null;
        }

        private void frmNewPurchaseMedicineReport_Load(object sender, EventArgs e)
        {
            datWindow.LibraryList = Application.StartupPath + "\\pb_ms.pbl";
            DataTable m_dtbPrint = p_dtbVal.Copy();
            datWindow.DataWindowObject = "ms_newpurchasemedicine";
            datWindow.Modify("t_title.text = '��ҩ֪ͨ��(" + p_strStorageName + ")'");
            datWindow.Modify("t_date.text = '" + p_strDate + "'");
            datWindow.SetRedrawOff();
            datWindow.Retrieve(m_dtbPrint);
            datWindow.SetRedrawOn();
            datWindow.Refresh();
        }
        #endregion 
    }
}