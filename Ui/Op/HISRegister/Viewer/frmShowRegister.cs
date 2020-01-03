using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmShowRegister : Form
    {
        public frmShowRegister()
        {
            InitializeComponent();
        }
        DataTable printDt = new DataTable();
        public DataTable GetPrintData
        {
            set
            {
                printDt = value;
            }
        }
        int _pintNuber = 0;
        public int printNuber
        {
            set
            {
                _pintNuber = value;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("����", 10.5F);
            Font font1 = new Font("����", 9F);
            Font font2 = new Font("����", 8F);
            e.Graphics.DrawString(printDt.Rows[0]["registertype"].ToString(), font, Brushes.Black, 59, 13);
            
            e.Graphics.DrawString(printDt.Rows[0]["txtNO"].ToString(), font, Brushes.Black, 295,13);
            e.Graphics.DrawString(printDt.Rows[0]["patienCard"].ToString(), font, Brushes.Black, 59, 29);

            e.Graphics.DrawString(printDt.Rows[0]["patientname"].ToString(), font, Brushes.Black, 118, 70);
            e.Graphics.DrawString(printDt.Rows[0]["orderno"].ToString(), font, Brushes.Black, 319, 70);

            e.Graphics.DrawString(printDt.Rows[0]["strDiagdept"].ToString(), font, Brushes.Black, 110, 100);

            e.Graphics.DrawString(printDt.Rows[0]["doctorName"].ToString(), font, Brushes.Black, 307, 100);

            e.Graphics.DrawString("��" + printDt.Rows[0]["decimal1"].ToString() + " Ԫ", font, Brushes.Black, 118, 136);
            e.Graphics.DrawString("��" + printDt.Rows[0]["decimal2"].ToString() + " Ԫ", font, Brushes.Black, 307, 136);
            e.Graphics.DrawString(printDt.Rows[0]["date"].ToString(), font, Brushes.Black, 118, 166);
            decimal tolmoney = decimal.Parse(printDt.Rows[0]["decimal1"].ToString()) + decimal.Parse(printDt.Rows[0]["decimal2"].ToString());
            e.Graphics.DrawString("�ϼƣ�" + tolmoney + " Ԫ", font, Brushes.Black, 87, 193);
            e.Graphics.DrawString("�Һ�Ա�� " + printDt.Rows[0]["strEmpt"].ToString(), font, Brushes.Black, 260, 193);
            e.Graphics.DrawString("��Ʊ��ǹҺ�ר���£�������Ч", font1, Brushes.Black, 98, 210);
            Pen linePen = new Pen(Brushes.Black, 1);
            e.Graphics.DrawLine(linePen, 346, 206, 405, 206);
            e.Graphics.DrawLine(linePen, 346, 206, 346, 242);
            e.Graphics.DrawLine(linePen, 405, 206, 405, 242);
            e.Graphics.DrawLine(linePen, 346, 242, 405, 242);
            e.Graphics.DrawString("��ɽ�еڶ�", font2, Brushes.Black, 346, 208);
            e.Graphics.DrawString("����ҽԺ", font2, Brushes.Black, 350, 218);
            e.Graphics.DrawString("�շ�ר����", font2, Brushes.Black, 346, 228);
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

           //WritePrinter((IntPtr)sender, new byte(chprintdata), 2, ref dwwriten);
            //printDocument1.DefaultPageSettings = PrtSetUp(printDocument1);
        }
        public PageSettings PrtSetUp(PrintDocument printDocument)
        {
            //��������ֵ��PageSettings
            PageSettings ps = new PageSettings();
            //������ʵ����PageSetupDialog
            PageSetupDialog psDlg = new PageSetupDialog();
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("�ҺŴ�ӡ", 472, 276);
            try
            {
                //����ĵ����ĵ�ҳ��Ĭ������
                psDlg.Document = printDocument;
                psDlg.PageSettings = printDocument.DefaultPageSettings;
                psDlg.PageSettings.PaperSize = size;
                ps = psDlg.PageSettings;
                printDocument.DefaultPageSettings = psDlg.PageSettings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "���ִ�ӡ����");
            }
            finally
            {
                psDlg.Dispose();
                psDlg = null;
            }
            return ps;
        }

        private void frmShowRegister_Load(object sender, EventArgs e)
        {
            this.ctlprintShow1.IsShowClose = false;
            ctlprintShow1.setDocument = printDocument1;
        }

    }
}