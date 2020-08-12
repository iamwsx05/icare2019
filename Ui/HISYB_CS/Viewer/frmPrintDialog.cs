using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// DATAWINDOWͨ�ô�ӡ�Ի�����
    /// </summary>
    public partial class frmPrintDialog : Form
    {
        /// <summary>
        /// ����Դ�����﷨

        /// </summary>
        private string DataSourceSyntax = "";
        /// <summary>
        /// ����Դ��������״̬

        /// </summary>
        private DataWindowFullState DataFullState;

        /// <summary>
        /// ���캯��

        /// </summary>
        public frmPrintDialog(DataWindowControl dw)
        {
            InitializeComponent();
            DataSourceSyntax = dw.Describe("datawindow.syntax");
            DataFullState = dw.GetFullState();
        }

        /// <summary>
        /// ���캯��

        /// </summary>
        public frmPrintDialog(DataStore ds)
        {
            InitializeComponent();
            DataSourceSyntax = ds.Describe("datawindow.syntax");
            DataFullState = ds.GetFullState();
        }

        private void frmPrintDialog_Load(object sender, EventArgs e)
        {
            this.dwRep.Create(DataSourceSyntax);
            this.dwRep.SetFullState(DataFullState);
            this.dwRep.CalculateGroups();
            this.dwRep.Refresh();

            this.dwRep.Modify("datawindow.print.preview = yes");
            this.dwRep.Modify("datawindow.print.preview.rulers = yes");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            clsYBPublic_cs.ExportDataWindow(this.dwRep, null);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            clsYBPublic_cs.ChoosePrintDialog(this.dwRep, true);
            this.dwRep.Modify("datawindow.print.preview = yes");
            this.dwRep.Modify("datawindow.print.preview.rulers = yes");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}