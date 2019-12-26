using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using weCare.Core.Entity; 
using System.Windows.Forms;
using System.Configuration;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsQCDailyReportToolStrategy
    {
        // Fields
        private bool m_blnPrintShowDialog;
        private int m_intPageHigh;
        private int m_intPageWidth;
        private clsLISQCDailyReportPrintVO m_objReportInfo;
        private clsQCDailyReportTool m_objReportPrint;
        private PrintDialog m_printDlg;
        private PrintDocument m_printDoc;
        private PrintPreviewDialog m_printPrev;

        // Methods
        public clsQCDailyReportToolStrategy()
        {
            this.m_objReportInfo = new clsLISQCDailyReportPrintVO();
            this.m_blnPrintShowDialog = false; 
            this.m_mthInit(); 
        }

        public clsQCDailyReportToolStrategy(clsLisQCReportVO objReportInfo, clsLisQCBatchVO objBaseInfo)
        {
            this.m_objReportInfo = new clsLISQCDailyReportPrintVO();
            this.m_blnPrintShowDialog = false; 
            this.m_mthInit();
            this.m_objReportInfo.objReportInfo = objReportInfo;
            this.m_objReportInfo.objBaseInfo = objBaseInfo; 
        }

        private void m_mthGetPageSize()
        {
            string str;
            ConfigXmlDocument document;
            string str2;
            string str3;
            double num;
            int num2;
            double num3;
            int num4;
            bool flag; 
            try
            {
                str = Application.StartupPath + @"\LIS_GUI.dll.config";
                document = new ConfigXmlDocument();
                document.Load(str);
                str2 = document["configuration"]["appSettings"].SelectSingleNode("add[@key=\"LISQCDayReportPrintPaperWidth\"]").Attributes["value"].Value;
                str3 = document["configuration"]["appSettings"].SelectSingleNode("add[@key=\"LISQCDayReportPrintPaperHigh\"]").Attributes["value"].Value;
                num = double.Parse(str2);
                if (num > 0.0)  
                {
                    goto Label_00AE;
                }
                num = 21.0;
            Label_00AE:
                num2 = (int)(num * 100.0);
                this.m_intPageWidth = PrinterUnitConvert.Convert(num2, PrinterUnit.TenthsOfAMillimeter, 0);
                num3 = double.Parse(str3);
                if (num3 > 0.0)  
                {
                    goto Label_00F2;
                }
                num3 = 14.0;
            Label_00F2:
                num4 = (int)(num3 * 100.0);
                this.m_intPageHigh = PrinterUnitConvert.Convert(num4, PrinterUnit.TenthsOfAMillimeter, 0); 
                goto Label_012E;
            }
            catch
            { 
                this.m_intPageWidth = 710;
                this.m_intPageHigh = 550;
                goto Label_012E;
            }
        Label_012E:
            return;
        }

        private void m_mthInit()
        {
            PageSettings settings;
            this.m_mthGetPageSize();
            this.m_printDoc = new PrintDocument();
            this.m_printDoc.DefaultPageSettings.PaperSize = new PaperSize("LIS_QCDayReport", this.m_intPageWidth, this.m_intPageHigh);
            this.m_objReportPrint = new clsQCDailyReportTool();
            this.m_printDoc.PrintController = new StandardPrintController();
            this.m_printDoc.BeginPrint += new PrintEventHandler(this.m_printDoc_BeginPrint);
            this.m_printDoc.PrintPage += new PrintPageEventHandler(this.m_printDoc_PrintPage);
            this.m_printDoc.EndPrint += new PrintEventHandler(this.m_printDoc_EndPrint);
            return;
        }

        public void m_mthPrint()
        {
            try
            {
                if (m_blnPrintShowDialog)
                {
                    m_printDlg = new PrintDialog();
                    m_printDlg.Document = m_printDoc;
                    DialogResult dlgRes = m_printDlg.ShowDialog();
                    if (dlgRes == DialogResult.OK)
                    {
                        m_printDoc.Print();
                    }
                }
                else
                {
                    m_printDoc.Print();
                }
            }
            catch
            {
                MessageBox.Show("打印失败！");
            }
        }

        public void m_mthPrintPreview()
        {
            try
            {
                m_printPrev = new PrintPreviewDialog();
                m_printPrev.Document = m_printDoc;
                m_printPrev.ShowDialog();
            }
            catch
            {
                MessageBox.Show("打印预览失败！");
            }
        }

        private void m_printDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            this.m_objReportPrint.m_mthInitPrintTool(this.m_printDoc);
            this.m_objReportPrint.m_mthBeginPrint(this.m_objReportInfo); 
        }

        private void m_printDoc_EndPrint(object sender, PrintEventArgs e)
        {
            this.m_objReportPrint.m_mthEndPrint(e); 
        }

        private void m_printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            this.m_objReportPrint.m_mthPrintPage(e); 
        }

        // Properties
        public bool PrintWithDialog
        {
            get
            {
                return m_blnPrintShowDialog;
            }
            set
            {
                m_blnPrintShowDialog = value;
            }
        }

        public clsLISQCDailyReportPrintVO ReportInfo
        {
            get { return m_objReportInfo; }
        }
    } 
}
