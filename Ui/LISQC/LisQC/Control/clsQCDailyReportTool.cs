using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using weCare.Core.Entity;
using ZedGraph;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsQCDailyReportTool : infPrintRecord
    {
        // Fields
        private float m_fltPrintHeight;
        private float m_fltPrintWidth;
        private Font m_fntMiddleNotBold;
        private Font m_fntMidleBold;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntTitle;
        private Pen m_GridPen;
        private long m_lngTitleTop;
        private long m_lngVerticalLineEnd;
        private long m_lngVerticalLineStart;
        private long m_lngY;
        private string m_strBrokeRules;
        private string m_strDevice;
        private string m_strItem;
        private string m_strOperator;
        private string m_strProcess;
        private string m_strQCSampleLotNO;
        private string m_strQCSampleVendor;
        private string m_strQCStatus;
        private string m_strReagentLotNO;
        private string m_strReagentVendor;
        private string m_strReportDate;
        private string m_strResson;
        private string m_strTitle;
        private string m_strWorkGroup;

        // Methods
        public clsQCDailyReportTool()
        {
            this.m_lngTitleTop = 30L;
            this.m_strTitle = "检验科室内质控日报告";
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            this.m_mthInitalPrintInfo((clsLISQCDailyReportPrintVO)p_objPrintArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
            return;
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
            return;
        }

        public void m_mthInitalPrintInfo(clsLISQCDailyReportPrintVO objReportVO)
        {
            if (objReportVO == null || objReportVO.objReportInfo == null || objReportVO.objBaseInfo == null)
                return;
            m_strWorkGroup = objReportVO.objBaseInfo.m_strWorkGroupName;// = "临检室";
            m_strDevice = objReportVO.objBaseInfo.m_strDeviceModel;// = "雅培1700A";
            m_strItem = objReportVO.objBaseInfo.m_strCheckItemName;// = "血红蛋白";
            m_strQCSampleLotNO = objReportVO.objBaseInfo.m_strSampleLotNo;// = "T13052";
            m_strQCSampleVendor = objReportVO.objBaseInfo.m_strSampleVendor;// = "北京中生";
            m_strReagentLotNO = objReportVO.objBaseInfo.m_strReagentBatch;// = "200606143";
            m_strReagentVendor = objReportVO.objBaseInfo.m_strReagent;// = "雅培";

            m_strQCStatus = objReportVO.objReportInfo.m_enmQCControlStatus == enmQCControlStatus.Control ? "在控" : "失控";// = "失控";
            m_strBrokeRules = objReportVO.objReportInfo.m_strUnmatchedRule;// = "13S";
            m_strResson = objReportVO.objReportInfo.m_strReason;// = "试剂";
            m_strProcess = objReportVO.objReportInfo.m_strProcess;// = "更换试剂";

            m_strOperator = objReportVO.objReportInfo.m_strReportorName;// = "陈明";
            m_strReportDate = objReportVO.objReportInfo.m_dtReport.ToString("yyyy-MM-dd");// = "2006-06-20";
        }

        public void m_mthInitPrintContent()
        {
            return;
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            m_fntTitle = new Font("SimSun", 16, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 9, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntMidleBold = new Font("SimSun", 11f, FontStyle.Bold);
            m_fntMiddleNotBold = new Font("SimSun", 11f, FontStyle.Regular);

            m_GridPen = new Pen(Color.Black, 1);


            m_fltPrintWidth = ((PrintDocument)p_objArg).DefaultPageSettings.Bounds.Width;
            m_fltPrintHeight = ((PrintDocument)p_objArg).DefaultPageSettings.Bounds.Height;
        }

        private void m_mthPrintBottomLine(PrintPageEventArgs p_objPrintPageArgs)
        {
            m_lngY = (long)(m_fltPrintHeight - 80);
            m_lngVerticalLineEnd = m_lngY;
            p_objPrintPageArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.08f, m_lngY, p_objPrintPageArgs.PageBounds.Width * 0.96f, m_lngY);
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            this.m_mthPrintReportTop((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintTopLine((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintReport((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintBottomLine((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintReportBotton((PrintPageEventArgs)p_objPrintArg);
        }

        public void m_mthPrintReport(PrintPageEventArgs p_objPrintPageArgs)
        {
            float num;
            float num2;
            SizeF ef;
            RectangleF ef2;
            SizeF ef3;
            num = p_objPrintPageArgs.Graphics.MeasureString("XXX", this.m_fntMidleBold).Height - p_objPrintPageArgs.Graphics.MeasureString("XXX", this.m_fntSmallNotBold).Height;
            num2 = this.m_fltPrintWidth * 0.08f;
            this.m_lngY += 10L;
            ef = p_objPrintPageArgs.Graphics.MeasureString("质控状态:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("质控状态:", this.m_fntMidleBold, Brushes.Black, num2, (float)this.m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strQCStatus, this.m_fntSmallNotBold, Brushes.Black, num2 + ef.Width, ((float)this.m_lngY) + num);
            this.m_lngY += ((long)ef.Height) + 10L;
            ef = p_objPrintPageArgs.Graphics.MeasureString("违背的质控规则:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("违背的质控规则:", this.m_fntMidleBold, Brushes.Black, num2, (float)this.m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strBrokeRules, this.m_fntSmallNotBold, Brushes.Black, num2 + ef.Width, ((float)this.m_lngY) + num);
            this.m_lngY += ((long)ef.Height) + 10L;
            ef = p_objPrintPageArgs.Graphics.MeasureString("失控原因:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("失控原因:", this.m_fntMidleBold, Brushes.Black, num2, (float)this.m_lngY);
            this.m_lngY += (long)(ef.Height + 5f);
            ef2 = new RectangleF(num2, (float)this.m_lngY, this.m_fltPrintWidth * 0.86f, 5f * ef.Height);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strResson, this.m_fntSmallNotBold, Brushes.Black, ef2);
            this.m_lngY += (long)((5f * ef.Height) + 10f);
            ef = p_objPrintPageArgs.Graphics.MeasureString("处理方法:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("处理方法:", this.m_fntMidleBold, Brushes.Black, num2, (float)this.m_lngY);
            this.m_lngY += (long)(ef.Height + 5f);
            ef2 = new RectangleF(num2, (float)this.m_lngY, this.m_fltPrintWidth * 0.86f, 5f * ef.Height);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strProcess, this.m_fntSmallNotBold, Brushes.Black, ef2);
            this.m_lngY += (long)(5f * ef.Height);
        }

        private void m_mthPrintReportBotton(PrintPageEventArgs p_objPrintPageArgs)
        {
            float num;
            SizeF ef;
            this.m_lngY += 5L;
            num = this.m_fltPrintWidth * 0.3f;
            ef = p_objPrintPageArgs.Graphics.MeasureString("报告日期:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("报告日期:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strReportDate, this.m_fntSmallNotBold, Brushes.Black, num + ef.Width, (float)this.m_lngY);
            num = this.m_fltPrintWidth * 0.6f;
            ef = p_objPrintPageArgs.Graphics.MeasureString("报告人员:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("报告人员:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strOperator, this.m_fntSmallNotBold, Brushes.Black, num + ef.Width, (float)this.m_lngY);
        }

        private void m_mthPrintReportTop(PrintPageEventArgs p_objPrintArgs)
        {
            SizeF ef;
            float num;
            float num2;
            SizeF ef2;
            Rectangle rectangle;
            SizeF ef3;
            this.m_lngY = this.m_lngTitleTop;
            ef = p_objPrintArgs.Graphics.MeasureString(this.m_strTitle, this.m_fntTitle);
            num = (((float)p_objPrintArgs.PageBounds.Width) - ef.Width) / 2f;
            p_objPrintArgs.Graphics.DrawString(this.m_strTitle, this.m_fntTitle, Brushes.Black, num, (float)this.m_lngTitleTop);
            num2 = p_objPrintArgs.Graphics.MeasureString("工作室", this.m_fntMidleBold).Height - p_objPrintArgs.Graphics.MeasureString("工作室", this.m_fntSmallNotBold).Height;
            this.m_lngY = (this.m_lngY + ((long)ef.Height)) + 10L;
            num = this.m_fltPrintWidth * 0.08f;
            p_objPrintArgs.Graphics.DrawString("工作室:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            ef2 = p_objPrintArgs.Graphics.MeasureString("工作室:", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_strWorkGroup, this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            num = this.m_fltPrintWidth * 0.4f;
            p_objPrintArgs.Graphics.DrawString("仪器:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            ef2 = p_objPrintArgs.Graphics.MeasureString("仪器:", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_strDevice, this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            num = this.m_fltPrintWidth * 0.6f;
            p_objPrintArgs.Graphics.DrawString("项目:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            ef2 = p_objPrintArgs.Graphics.MeasureString("项目:", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_strItem, this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            this.m_lngY += ((long)ef2.Height) + 10L;
            num = this.m_fltPrintWidth * 0.08f;
            p_objPrintArgs.Graphics.DrawString("质控品批号及厂家:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            ef2 = p_objPrintArgs.Graphics.MeasureString("质控品批号及厂家:", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_strQCSampleLotNO + "-" + this.m_strQCSampleVendor, this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            num = this.m_fltPrintWidth * 0.54f;
            p_objPrintArgs.Graphics.DrawString("试剂批号及厂家:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            ef2 = p_objPrintArgs.Graphics.MeasureString("试剂批号及厂家:", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_strReagentLotNO + "-" + this.m_strReagentVendor, this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            this.m_lngY += (long)ef2.Height;
        }

        private void m_mthPrintTopLine(PrintPageEventArgs p_objPrintArgs)
        {
            Rectangle rectangle;
            this.m_lngY += 3L;
            this.m_lngVerticalLineStart = this.m_lngY;
            p_objPrintArgs.Graphics.DrawLine(this.m_GridPen, this.m_fltPrintWidth * 0.08f, (float)this.m_lngY, ((float)p_objPrintArgs.PageBounds.Width) * 0.96f, (float)this.m_lngY);
        }

        public SizeF m_SFGetPrintSize(int p_intPrintWidth, DataTable p_dtSample, DataTable p_dtResult)
        {
            return new SizeF(0f, 0f);
        }
    }
}
