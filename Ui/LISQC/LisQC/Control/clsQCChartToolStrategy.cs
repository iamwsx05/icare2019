using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Text;
using ZedGraph;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsQCChartToolStrategy
    {
        // Fields
        private bool m_blnPrintShowDialog;
        private clsLISQCChartPrintVO m_objChartInfo;
        private clsQCChartTool m_objChartPrint;
        private PrintDialog m_printDlg;
        private PrintDocument m_printDoc;
        private PrintPreviewDialog m_printPrev;

        // Methods
        public clsQCChartToolStrategy()
        {
            this.m_objChartInfo = new clsLISQCChartPrintVO();
            this.m_blnPrintShowDialog = false;
            this.m_mthInit();
        }

        public clsQCChartToolStrategy(ZedGraphControl p_zedChart, clsQCBatchNew p_objBatchInfo) : this()
        {
            this.m_objChartInfo.zedChart = p_zedChart;
            this.m_objChartInfo.objBatch = p_objBatchInfo;
        }

        public clsQCChartToolStrategy(ZedGraphControl p_zedChart, clsQCBatchNew p_objBatchInfo, clsLisQCConcentrationVO p_objQCCon) : this()
        {
            this.m_objChartInfo.zedChart = p_zedChart;
            this.m_objChartInfo.objBatch = p_objBatchInfo;
            this.m_objChartInfo.objSelectQCCon = p_objQCCon;
        }

        private void m_mthInit()
        {
            this.m_printDoc = new PrintDocument();
            this.m_printDoc.DefaultPageSettings.Landscape = false;
            this.m_objChartPrint = new clsQCChartTool();
            this.m_printDoc.PrintController = new StandardPrintController();
            this.m_printDoc.BeginPrint += new PrintEventHandler(this.m_printDoc_BeginPrint);
            this.m_printDoc.PrintPage += new PrintPageEventHandler(this.m_printDoc_PrintPage);
            this.m_printDoc.EndPrint += new PrintEventHandler(this.m_printDoc_EndPrint); 
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
            this.m_objChartPrint.m_mthInitPrintTool(this.m_printDoc);
            this.m_objChartPrint.m_mthBeginPrint(this.m_objChartInfo); 
        }

        private void m_printDoc_EndPrint(object sender, PrintEventArgs e)
        {
            this.m_objChartPrint.m_mthEndPrint(e); 
        }

        private void m_printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            this.m_objChartPrint.m_mthPrintPage(e); 
        }

        // Properties
        internal clsLISQCChartPrintVO ChartInfo
        {
            get { return m_objChartInfo; }
        }

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
    }
}
