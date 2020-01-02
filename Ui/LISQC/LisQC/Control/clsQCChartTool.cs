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
    public class clsQCChartTool : infPrintRecord
    {
        // Fields
        private DateTime m_dtEndDate;
        private DateTime m_dtStartDate;
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
        private List<clsLisQCDataVO> m_lstQCDatas;
        private clsLisQCConcentrationVO m_objQCCon;
        private clsLisQCConcentrationVO m_objQCConReal;
        private clsLisQCReportVO m_objQCReports;
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
        private ZedGraphControl m_zedGraph;

        // Methods
        public clsQCChartTool()
        {
            this.m_lngTitleTop = 30L;
            this.m_strTitle = "室内质控纪录";
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            this.m_mthInitalPrintInfo((clsLISQCChartPrintVO)p_objPrintArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
            return;
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
            return;
        }

        public void m_mthInitalPrintInfo(clsLISQCChartPrintVO objChartVO)
        {
            List<clsLisQCReportVO> list;
            int num;
            clsLisQCReportVO tvo;
            clsLisQCBatchVO hvo;
            bool flag;
            List<clsLisQCReportVO>.Enumerator enumerator;
            if (objChartVO == null || objChartVO.objBatch == null || objChartVO.zedChart == null)
            {
                goto Label_0315;
            }
            else
            {
                goto Label_0026;
            }
        Label_0026:
            this.m_zedGraph = objChartVO.zedChart;
            this.m_objQCCon = objChartVO.objSelectQCCon;
            this.m_lstQCDatas = objChartVO.objBatch.GetDatas();
            if (m_lstQCDatas != null && m_lstQCDatas.Count > 0)
            {
                m_lstQCDatas.Sort(clsLisQCDataVO.CompareVO);
            }
            else
            {
                goto Label_008B;
            }
        Label_008B:
            this.m_objQCReports = null;
            list = objChartVO.objBatch.GetReports();
            num = 0;
            goto Label_0118;
        Label_00A2:
            if (list[num].m_intReportStats != 1)
            {
                goto Label_0113;
            }
            if (this.m_objQCReports == null)
            {
                goto Label_0103;
            }
            if (this.m_objQCReports.m_dtReport >= list[num].m_dtReport)
            {
                goto Label_0100;
            }
            this.m_objQCReports = list[num];
        Label_0100:
            goto Label_0112;
        Label_0103:
            this.m_objQCReports = list[num];
        Label_0112:;
        Label_0113:
            num += 1;
        Label_0118:
            if (num < list.Count)
            {
                goto Label_00A2;
            }
            if (this.m_objQCReports == null)
            {
                goto Label_01D5;
            }
            if (this.m_objQCReports.m_enmQCControlStatus == enmQCControlStatus.UnControl)
            {
                goto Label_0163;
            }
            this.m_strQCStatus = "在控";
            goto Label_0170;
        Label_0163:
            this.m_strQCStatus = "失控";
        Label_0170:
            this.m_strBrokeRules = this.m_objQCReports.m_strUnmatchedRule;
            this.m_strResson = this.m_objQCReports.m_strReason;
            this.m_strProcess = this.m_objQCReports.m_strProcess;
            this.m_strOperator = this.m_objQCReports.m_strReportorName;
            this.m_strReportDate = this.m_objQCReports.m_dtReport.ToString("yyyy年MM月");
            goto Label_0292;
        Label_01D5:
            this.m_strBrokeRules = objChartVO.objBatch.BrokenRules;
            this.m_strQCStatus = "在控";
            if (!string.IsNullOrEmpty(this.m_strBrokeRules))
            {
                goto Label_0284;
            }
            if (list != null && list.Count > 0)
            {
                enumerator = list.GetEnumerator();
            }
            else
            {
                goto Label_0281;
            }
        Label_0229:
            try
            {
                goto Label_0261;
            Label_022B:
                tvo = enumerator.Current;
                if (tvo.m_enmQCControlStatus == enmQCControlStatus.Control)
                {
                    goto Label_0260;
                }
                this.m_strQCStatus = "失控";
                this.m_strBrokeRules = tvo.m_strUnmatchedRule;
                goto Label_026E;
            Label_0260:;
            Label_0261:
                if (enumerator.MoveNext())
                {
                    goto Label_022B;
                }
            Label_026E:
                goto Label_027F;
            }
            finally
            {
            Label_0270:
                enumerator.Dispose();
            }
        Label_027F:;
        Label_0281:
            goto Label_0291;
        Label_0284:
            this.m_strQCStatus = "失控";
        Label_0291:;
        Label_0292:
            this.m_dtStartDate = objChartVO.objBatch.DateBegin;
            this.m_dtEndDate = objChartVO.objBatch.DateEnd;
            hvo = objChartVO.objBatch[0];
            this.m_strWorkGroup = hvo.m_strWorkGroupName;
            this.m_strDevice = hvo.m_strDeviceModel;
            this.m_strItem = hvo.m_strCheckItemName;
            this.m_strQCSampleLotNO = hvo.m_strSampleLotNo;
            this.m_strQCSampleVendor = hvo.m_strSampleVendor;
            this.m_strReagentLotNO = hvo.m_strReagentBatch;
            this.m_strReagentVendor = hvo.m_strReagent;
        Label_0315:
            return;
        }

        public void m_mthInitPrintContent()
        {
            return;
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            Rectangle rectangle;
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
            Rectangle rectangle;
            m_lngY = (long)(m_fltPrintHeight - 80);
            m_lngVerticalLineEnd = m_lngY;
            p_objPrintPageArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.08f, m_lngY, p_objPrintPageArgs.PageBounds.Width * 0.96f, m_lngY);
        }

        private void m_mthPrintChart(PrintPageEventArgs p_objPrintPageArgs)
        {
            Graphics g = p_objPrintPageArgs.Graphics;
            int chartHeight = 300;
            Rectangle rect = new Rectangle((int)(m_fltPrintWidth * 0.10f), (int)m_lngY + 10, (int)(m_fltPrintWidth * 0.8), chartHeight);
            m_zedGraph.DrawNoFontChart(g, rect);
            m_lngY += (long)chartHeight + 8;

            #region 本室浓度
            //本室浓度
            float fltStartX = m_fltPrintWidth * 0.125f;
            float fltCurrentX = fltStartX;
            SizeF sfWords = g.MeasureString("本室均值: ", m_fntMidleBold);
            g.DrawString("本室均值: ", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            // SD
            fltCurrentX = m_fltPrintWidth * 0.4f;
            g.DrawString("SD: ", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            // CV
            fltCurrentX = m_fltPrintWidth * 0.6f;
            g.DrawString("CV: ", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            if (m_lstQCDatas != null && m_lstQCDatas.Count > 0)
            {
                double[] dblDataArr = new double[m_lstQCDatas.Count];
                for (int index = 0; index < m_lstQCDatas.Count; index++)
                {
                    dblDataArr[index] = m_lstQCDatas[index].m_dlbResult;
                }

                double dblX = 0;
                double dblSD = 0;
                double dblCV = 0;
                long lngRes = clsLISPublic.m_lngCalculateSDXCV(dblDataArr, out dblX, out dblSD, out dblCV);

                if (lngRes > 0)
                {
                    float fltInY = g.MeasureString("本室均值", m_fntMidleBold).Height - g.MeasureString("本室均值", m_fntSmallNotBold).Height;

                    fltCurrentX = fltStartX;
                    sfWords = g.MeasureString("本室均值: ", m_fntMidleBold);
                    fltCurrentX += sfWords.Width;
                    g.DrawString(dblX.ToString("0.00"), m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

                    // SD
                    fltCurrentX = m_fltPrintWidth * 0.4f;
                    sfWords = g.MeasureString("SD: ", m_fntMidleBold);
                    fltCurrentX += sfWords.Width;
                    g.DrawString(dblSD.ToString("0.00"), m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

                    // CV
                    fltCurrentX = m_fltPrintWidth * 0.6f;
                    sfWords = g.MeasureString("CV: ", m_fntMidleBold);
                    fltCurrentX += sfWords.Width;
                    g.DrawString(dblCV.ToString("0.0"), m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

                }
            }

            m_lngY += Convert.ToInt64(sfWords.Height) + 3;
            #endregion

            #region 本室浓度数据

            fltCurrentX = fltStartX;
            SizeF sizef = g.MeasureString("12.50", m_fntSmallNotBold);
            float width = sizef.Width;
            float height = sizef.Height + 2;
            int widSpace = 2;
            int heiSpace = 2;

            float xStep = width + widSpace;
            float yStep = height + heiSpace;
            float fltCurrentY = m_lngY;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            clsLisQCDataVO objTemp = null;
            int iOneRowCount = 16;
            int iRowCount = m_lstQCDatas.Count / iOneRowCount;
            if (m_lstQCDatas.Count % iOneRowCount > 0)
            {
                iRowCount += 1;
            }
            int idxResult = 0;
            int fltCurPrintWidth = Convert.ToInt32(fltCurrentX + xStep * iOneRowCount);
            RectangleF recgF = new RectangleF(fltCurrentX, fltCurrentY, xStep, yStep);
            // 每行打印16个数据
            if (iRowCount > 0)
            {
                g.DrawLine(m_GridPen, fltCurrentX, fltCurrentY, fltCurPrintWidth, fltCurrentY);

                for (int iRow = 0; iRow < iRowCount; iRow++)
                {
                    fltCurrentX = fltStartX;
                    g.DrawLine(m_GridPen, fltCurrentX, fltCurrentY + yStep, fltCurPrintWidth, fltCurrentY + yStep);
                    g.DrawLine(m_GridPen, fltCurrentX, fltCurrentY + 2 * yStep, fltCurPrintWidth, fltCurrentY + 2 * yStep);

                    g.DrawLine(m_GridPen, fltCurrentX, fltCurrentY, fltCurrentX, fltCurrentY + 2 * yStep);
                    for (int index = 0; index < iOneRowCount; index++)
                    {
                        g.DrawLine(m_GridPen, fltCurrentX + xStep, fltCurrentY, fltCurrentX + xStep, fltCurrentY + 2 * yStep);

                        if (idxResult < m_lstQCDatas.Count)
                        {
                            objTemp = m_lstQCDatas[idxResult];
                            recgF = new RectangleF(fltCurrentX, fltCurrentY, xStep, yStep);
                            g.DrawString(objTemp.m_datQCDate.ToString("M.d"), m_fntSmallNotBold, Brushes.Black, recgF, sf);
                            recgF.Y = fltCurrentY + yStep;
                            g.DrawString(objTemp.m_dlbResult.ToString(), m_fntSmallNotBold, Brushes.Black, recgF, sf);

                            idxResult += 1;
                        }

                        fltCurrentX += xStep;
                    }

                    fltCurrentY += 2 * yStep;
                }
            }
            #endregion
            m_lngY += Convert.ToInt64(yStep * 2 * iRowCount) + 10;
        }

        private void m_mthPrintChartBotton(PrintPageEventArgs p_objPrintPageArgs)
        {
            m_lngY += 5;
            float fltCurrentX = m_fltPrintWidth * 0.3f;

            //报告日期
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("报告月份:", m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("报告月份:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strReportDate, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //报告人员
            fltCurrentX = m_fltPrintWidth * 0.6f;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("报告人员:", m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("报告人员:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strOperator, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);
        }

        private void m_mthPrintChartTop(PrintPageEventArgs p_objPrintArgs)
        {
            SizeF ef;
            float num;
            float num2;
            SizeF ef2;
            Rectangle rectangle;
            SizeF ef3;
            bool flag;
            this.m_lngY = this.m_lngTitleTop;
            this.m_strTitle = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalName + this.m_strTitle;
            ef = p_objPrintArgs.Graphics.MeasureString(this.m_strTitle, this.m_fntTitle);
            num = (((float)p_objPrintArgs.PageBounds.Width) - ef.Width) / 2f;
            p_objPrintArgs.Graphics.DrawString(this.m_strTitle, this.m_fntTitle, Brushes.Black, num, (float)this.m_lngTitleTop);
            num2 = p_objPrintArgs.Graphics.MeasureString("控制日期", this.m_fntMidleBold).Height - p_objPrintArgs.Graphics.MeasureString("控制日期", this.m_fntSmallNotBold).Height;
            this.m_lngY = (this.m_lngY + ((long)ef.Height)) + 10L;
            num = this.m_fltPrintWidth * 0.08f;
            p_objPrintArgs.Graphics.DrawString("控制日期:", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            ef2 = p_objPrintArgs.Graphics.MeasureString("控制日期:", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_dtStartDate.ToString("yyyy-MM-dd") + " -- " + this.m_dtEndDate.ToString("yyyy-MM-dd"), this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
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
            this.m_lngY += ((long)ef2.Height) + 10L;
            num = this.m_fltPrintWidth * 0.08f;
            p_objPrintArgs.Graphics.DrawString("均值: ", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            num = this.m_fltPrintWidth * 0.4f;
            p_objPrintArgs.Graphics.DrawString("SD: ", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            num = this.m_fltPrintWidth * 0.6f;
            p_objPrintArgs.Graphics.DrawString("CV: ", this.m_fntMidleBold, Brushes.Black, num, (float)this.m_lngY);
            if (this.m_objQCCon == null)
            {
                goto Label_056F;
            }
            num = this.m_fltPrintWidth * 0.08f;
            ef2 = p_objPrintArgs.Graphics.MeasureString("均值: ", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_objQCCon.m_dblAVG.ToString(), this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            num = this.m_fltPrintWidth * 0.4f;
            ef2 = p_objPrintArgs.Graphics.MeasureString("SD: ", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_objQCCon.m_dblSD.ToString(), this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
            num = this.m_fltPrintWidth * 0.6f;
            ef2 = p_objPrintArgs.Graphics.MeasureString("CV: ", this.m_fntMidleBold);
            num += ef2.Width;
            p_objPrintArgs.Graphics.DrawString(this.m_objQCCon.m_dblCV.ToString(), this.m_fntSmallNotBold, Brushes.Black, num, ((float)this.m_lngY) + num2);
        Label_056F:
            this.m_lngY += (long)ef2.Height;
            return;
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            this.m_mthPrintChartTop((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintTopLine((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintChart((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintReport((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintBottomLine((PrintPageEventArgs)p_objPrintArg);
            this.m_mthPrintChartBotton((PrintPageEventArgs)p_objPrintArg);
            return;
        }

        public void m_mthPrintReport(PrintPageEventArgs p_objPrintPageArgs)
        {
            //沉降系数
            float fltInY = p_objPrintPageArgs.Graphics.MeasureString("XXX", m_fntMidleBold).Height - p_objPrintPageArgs.Graphics.MeasureString("XXX", m_fntSmallNotBold).Height;

            float fltCurrentX = m_fltPrintWidth * 0.08f;

            //质控状态
            m_lngY += 30;
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("质控状态:", m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("质控状态:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            RectangleF rectF = new RectangleF(fltCurrentX + sfWords.Width + 10, m_lngY, m_fltPrintWidth * 0.7f, sfWords.Height);

            p_objPrintPageArgs.Graphics.DrawString("  " + this.m_strQCStatus, this.m_fntMidleBold, Brushes.Red, rectF);
            this.m_lngY += ((long)sfWords.Height) + 15L;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("违背的质控规则:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("违背的质控规则:", this.m_fntMidleBold, Brushes.Black, fltCurrentX, (float)this.m_lngY);
            this.m_lngY += (long)(sfWords.Height + 5f);
            rectF = new RectangleF(fltCurrentX + 40f, (float)this.m_lngY, this.m_fltPrintWidth * 0.86f, 2f * sfWords.Height);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strBrokeRules, this.m_fntSmallNotBold, Brushes.Black, rectF);
            this.m_lngY += (long)((2f * sfWords.Height) + 15f);
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("失控原因:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("失控原因:", this.m_fntMidleBold, Brushes.Black, fltCurrentX, (float)this.m_lngY);
            this.m_lngY += (long)(sfWords.Height + 5f);
            rectF = new RectangleF(fltCurrentX, (float)this.m_lngY, this.m_fltPrintWidth * 0.86f, 2f * sfWords.Height);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strResson, this.m_fntSmallNotBold, Brushes.Black, rectF);
            this.m_lngY += (long)((2f * sfWords.Height) + 15f);
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("处理方法:", this.m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("处理方法:", this.m_fntMidleBold, Brushes.Black, fltCurrentX, (float)this.m_lngY);
            this.m_lngY += (long)(sfWords.Height + 5f);
            rectF = new RectangleF(fltCurrentX, (float)this.m_lngY, this.m_fltPrintWidth * 0.86f, 3f * sfWords.Height);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strProcess, this.m_fntSmallNotBold, Brushes.Black, rectF);
            this.m_lngY += (long)(3f * sfWords.Height);
        }

        private void m_mthPrintTopLine(PrintPageEventArgs p_objPrintArgs)
        {
            m_lngY += 3;
            m_lngVerticalLineStart = m_lngY;
            p_objPrintArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.08f, m_lngY, p_objPrintArgs.PageBounds.Width * 0.96f, m_lngY);
        }

        public SizeF m_SFGetPrintSize(int p_intPrintWidth, DataTable p_dtSample, DataTable p_dtResult)
        {
            return new SizeF(0f, 0f);
        }
    }

    #region 封装公共方法
    /// <summary>
    /// 封装公共方法 
    /// </summary>
    public class clsLISPublic
    {
        ///// <summary>
        ///// 打开检验采集控制台以及自动连接检验仪器
        ///// </summary>
        //public static void m_mthOpenLisDataController(Form p_frmMdiParent)
        //{
        //    Form[] frmChildren = null;
        //    com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller objDataController = null;

        //    if (p_frmMdiParent != null)
        //    {
        //        frmChildren = p_frmMdiParent.MdiChildren;
        //        foreach (Form frmChild in frmChildren)
        //        {
        //            objDataController = frmChild as com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller;
        //            if (objDataController != null)
        //            {
        //                break;
        //            }
        //        }

        //        if (objDataController == null)
        //        {
        //            objDataController = new com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller();
        //            objDataController.MdiParent = p_frmMdiParent;
        //            objDataController.WindowState = FormWindowState.Maximized;
        //            objDataController.Show();
        //        }
        //    }
        //}
        /// <summary>
        /// 计算均值、标准差、变异系数
        /// </summary>
        /// <param name="p_dblSourceDate"></param>
        /// <param name="p_dblX"></param>
        /// <param name="p_dblSD"></param>
        /// <param name="p_dblCV">%</param>
        /// <returns></returns>
        internal static long m_lngCalculateSDXCV(double[] p_dblSourceDate, out double p_dblX, out double p_dblSD, out double p_dblCV)
        {
            p_dblX = 0;
            p_dblSD = 0;
            p_dblCV = 0;
            if (p_dblSourceDate == null || p_dblSourceDate.Length <= 0)
            {
                return -1L;
            }

            double X = 0;
            double SD = 0;
            foreach (double d in p_dblSourceDate)
            {
                X += d;
            }
            p_dblX = X / p_dblSourceDate.Length;

            foreach (double d in p_dblSourceDate)
            {
                SD += Math.Pow((d - p_dblX), 2);
            }
            SD = SD / p_dblSourceDate.Length;

            p_dblSD = Math.Sqrt(SD);

            p_dblCV = (p_dblSD / p_dblX) * 100;//(SD / p_dblX) * 100;

            return 1L;
        }

    }

    #endregion
}
