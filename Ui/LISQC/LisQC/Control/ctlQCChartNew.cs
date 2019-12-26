using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using PinkieControls;
using System.Drawing;
using ZedGraph;
using System.Xml; 
using System.IO;
using ExpressionEvaluation; 

namespace com.digitalwave.iCare.gui.LIS.QC.Control
{
    public class ctlQCChartNew : UserControl
    {
        // Fields
        private ColumnHeader columnHeader1;
        private IContainer components;
        private Label label1;
        private ButtonXP m_cmdAnalysis;
        private ButtonXP m_cmdPrint;
        private ContextMenuStrip m_cmsQCAnalysis;
        private GroupBox m_gpbChartStyle;
        private List<clsLisQCConcentrationVO> m_lstChoiceConcentration;
        private List<clsQCDataPairItem> m_lstQCDataPairItem;
        private ListView m_lsvQCResult;
        private clsQCBatchNew m_objBatch;
        private Panel m_pnlControl;
        private RadioButton m_rdbLeveyChart;
        private RadioButton m_rdbYoudenChart;
        private RadioButton m_rdbZChart;
        private ZedGraphControl m_zedChart;
        private Panel panel2;

        // Methods
        public ctlQCChartNew()
        {
            this.m_objBatch = new clsQCBatchNew();
            this.m_lstQCDataPairItem = null;
            this.m_lstChoiceConcentration = new List<clsLisQCConcentrationVO>();
            this.components = null; 
            this.InitializeComponent();
            this.InitializeZedChart(); 
        }

        private void AddAssistLine(float p_X, float p_SD)
        {
            float num;
            float num2;
            XDate date;
            XDate date2;
            double[] numArray;
            double[] numArray2;
            double[] numArray3;
            double[] numArray4;
            double[] numArray5;
            double[] numArray6;
            double[] numArray7;
            double[] numArray8;
            GraphPane pane;
            double[] numArray9;
            num = p_X;
            num2 = p_SD;
            date = (double)XDate.DateTimeToXLDate(this.m_objBatch.DateBegin);
            date2 = (double)XDate.DateTimeToXLDate(this.m_objBatch.DateEnd);
            numArray = new double[] { (double)date, (double)date2 };
            numArray2 = new double[] { (double)num, (double)num };
            numArray3 = new double[] { (double)(num + num2), (double)(num + num2) };
            numArray4 = new double[] { (double)(num - num2), (double)(num - num2) };
            numArray5 = new double[] { (double)(num + (2f * num2)), (double)(num + (2f * num2)) };
            numArray6 = new double[] { (double)(num - (2f * num2)), (double)(num - (2f * num2)) };
            numArray7 = new double[] { (double)(num + (3f * num2)), (double)(num + (3f * num2)) };
            numArray8 = new double[] { (double)(num - (3f * num2)), (double)(num - (3f * num2)) };
            pane = this.m_zedChart.GraphPane;
            AddLine(numArray, numArray2, pane, Color.Black);
            AddLine(numArray, numArray5, pane, Color.Blue);
            AddLine(numArray, numArray6, pane, Color.Blue);
            AddLine(numArray, numArray3, pane, Color.Green);
            AddLine(numArray, numArray4, pane, Color.Green);
            AddLine(numArray, numArray7, pane, Color.Red);
            AddLine(numArray, numArray8, pane, Color.Red);
            AddLabel(" X", (float)date2, num, pane);
            AddLabel("-SD", (float)date2, num - num2, pane);
            AddLabel("+SD", (float)date2, num + num2, pane);
            AddLabel("-2SD", (float)date2, num - (2f * num2), pane);
            AddLabel("+2SD", (float)date2, num + (2f * num2), pane);
            AddLabel("-3SD", (float)date2, num - (3f * num2), pane);
            AddLabel("+3SD", (float)date2, num + (3f * num2), pane);
            return;
        }

        private static void AddLabel(string label, float x, float y, GraphPane myPane)
        {
            TextItem myText1 = new TextItem(label, x, y);
            myText1.Location.CoordinateFrame = CoordType.AxisXYScale;
            myText1.Location.AlignH = AlignH.Left;
            myText1.Location.AlignV = AlignV.Center;
            myText1.FontSpec.IsItalic = true;
            myText1.FontSpec.Fill.IsVisible = false;
            myText1.FontSpec.Border.IsVisible = false;
            myText1.FontSpec.FontColor = Color.Teal;
            myPane.GraphItemList.Add(myText1);
        }

        private void AddLeveyLineItems()
        {
            List<clsLisQCDataVO> QCDataList = m_lstDataListByConcentration();

            PointPairList pointsPair = new PointPairList();
            foreach (clsLisQCDataVO vo in QCDataList)
            {
                double x = (double)XDate.DateTimeToXLDate(vo.m_datQCDate);
                double y = vo.m_dlbResult;
                pointsPair.Add(x, y);
            }
            LineItem itemCurve = m_zedChart.GraphPane.AddCurve(m_lstChoiceConcentration[0].m_strConcentration, pointsPair, Color.Red, SymbolType.Diamond);
            itemCurve.Symbol.Fill = new Fill(Color.White);
        }

        private static void AddLine(double[] x1SD, double[] y1SD, GraphPane myPane, Color color)
        {
            LineItem sdLine1 = myPane.AddCurve("", x1SD, y1SD, color, SymbolType.None);
            sdLine1.Symbol.IsVisible = false;
            sdLine1.Line.Width = 0.5F;
        }

        private void AddYouDenAssistLine()
        {
            float num;
            float num2;
            double[] numArray;
            double[] numArray2;
            double[] numArray3;
            double[] numArray4;
            double[] numArray5;
            double[] numArray6;
            double[] numArray7;
            double[] numArray8;
            GraphPane pane;
            double[] numArray9;
            num = 0f;
            num2 = 1f;
            numArray = new double[] { (double)(num - (4f * num2)), (double)(num + (4f * num2)) };
            numArray2 = new double[] { (double)num, (double)num };
            numArray3 = new double[] { (double)(num + num2), (double)(num + num2) };
            numArray4 = new double[] { (double)(num - num2), (double)(num - num2) };
            numArray5 = new double[] { (double)(num + (2f * num2)), (double)(num + (2f * num2)) };
            numArray6 = new double[] { (double)(num - (2f * num2)), (double)(num - (2f * num2)) };
            numArray7 = new double[] { (double)(num + (3f * num2)), (double)(num + (3f * num2)) };
            numArray8 = new double[] { (double)(num - (3f * num2)), (double)(num - (3f * num2)) };
            pane = this.m_zedChart.GraphPane;
            AddLine(numArray, numArray2, pane, Color.Black);
            AddLine(numArray, numArray5, pane, Color.Blue);
            AddLine(numArray, numArray6, pane, Color.Blue);
            AddLine(numArray, numArray3, pane, Color.Green);
            AddLine(numArray, numArray4, pane, Color.Green);
            AddLine(numArray, numArray7, pane, Color.Red);
            AddLine(numArray, numArray8, pane, Color.Red);
            AddLabel(" X", num + (4f * num2), num, pane);
            AddLabel("-SD", num + (4f * num2), num - num2, pane);
            AddLabel("+SD", num + (4f * num2), num + num2, pane);
            AddLabel("-2SD", num + (4f * num2), num - (2f * num2), pane);
            AddLabel("+2SD", num + (4f * num2), num + (2f * num2), pane);
            AddLabel("-3SD", num + (4f * num2), num - (3f * num2), pane);
            AddLabel("+3SD", num + (4f * num2), num + (3f * num2), pane);
            AddLine(numArray2, numArray, pane, Color.Black);
            AddLine(numArray5, numArray, pane, Color.Blue);
            AddLine(numArray6, numArray, pane, Color.Blue);
            AddLine(numArray3, numArray, pane, Color.Green);
            AddLine(numArray4, numArray, pane, Color.Green);
            AddLine(numArray7, numArray, pane, Color.Red);
            AddLine(numArray8, numArray, pane, Color.Red);
            AddLabel("X", num - (0.1f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLabel("-SD", (num - num2) - (0.2f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLabel("+SD", (num + num2) - (0.2f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLabel("-2SD", (num - (2f * num2)) - (0.2f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLabel("+2SD", (num + (2f * num2)) - (0.2f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLabel("-3SD", (num - (3f * num2)) - (0.2f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLabel("+3SD", (num + (3f * num2)) - (0.2f * num2), (num + (4f * num2)) + (0.2f * num2), pane);
            AddLine(numArray, numArray, pane, Color.Black);
            return;
        }

        private void AddYouDenLineItems()
        {
            PointPairList lstPairYouDen = new PointPairList();
            if (m_lstQCDataPairItem != null && m_lstQCDataPairItem.Count > 0)
            {
                double X1 = m_lstChoiceConcentration[0].m_dblAVG;
                double X2 = m_lstChoiceConcentration[1].m_dblAVG;
                double SD1 = m_lstChoiceConcentration[0].m_dblSD;
                double SD2 = m_lstChoiceConcentration[1].m_dblSD;
                if (SD1 == 0)
                {
                    SD1 = double.MaxValue;
                }
                if (SD2 == 0)
                {
                    SD2 = double.MaxValue;
                }
                foreach (clsQCDataPairItem pair in m_lstQCDataPairItem)
                {
                    clsLisQCDataVO conQCData = pair[m_lstChoiceConcentration[0].m_intQCBatchSeq];
                    if (conQCData != null)
                    {
                        lstPairYouDen.Add((conQCData.m_dlbResult - X1) / SD1, (conQCData.m_dlbResult - X2) / SD2);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (lstPairYouDen != null && lstPairYouDen.Count > 0)
            {
                LineItem itemCurve = m_zedChart.GraphPane.AddCurve("", lstPairYouDen, Color.Red, SymbolType.Diamond);
                itemCurve.Line.IsVisible = false;
                itemCurve.Symbol.Fill = new Fill(Color.Red);
            }
        }

        private void AddZLineItems()
        {
            List<clsLisQCDataVO> QCDataList = m_lstDataListByConcentration();

            PointPairList[] arrPointPair = new PointPairList[m_lstChoiceConcentration.Count];

            for (int i = 0; i < m_lstChoiceConcentration.Count; i++)
            {
                clsLisQCConcentrationVO con = m_lstChoiceConcentration[i];
                double X = con.m_dblAVG;
                double SD = con.m_dblSD;
                if (SD == 0)
                {
                    SD = double.MaxValue;
                }
                PointPairList pointsPair = new PointPairList();
                foreach (clsLisQCDataVO vo in QCDataList)
                {
                    double x = (double)XDate.DateTimeToXLDate(vo.m_datQCDate);
                    double y = (vo.m_dlbResult - X) / SD;
                    pointsPair.Add(x, y);
                }
                arrPointPair[i] = pointsPair;
            }

            Color[] colors = new Color[] { Color.Red, Color.Green, Color.Purple, Color.RosyBrown, Color.Yellow, Color.Blue, Color.Black, Color.Brown };

            for (int i = 0; i < arrPointPair.Length; i++)
            {
                Color color;
                if (i < colors.Length)
                    color = colors[i];
                else
                {
                    color = Color.Red;
                }

                LineItem itemCurve = m_zedChart.GraphPane.AddCurve(m_lstChoiceConcentration[i].m_strConcentration, arrPointPair[i], color, SymbolType.Diamond);
                itemCurve.Symbol.Fill = new Fill(Color.White);
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void InitializeChartNull()
        {
            GraphPane pane;
            XDate date;
            XDate date2;
            this.m_zedChart.GraphPane = new GraphPane(this.m_zedChart.GraphPane.PaneRect, "", "", "");
            pane = this.m_zedChart.GraphPane;
            date = (double)XDate.DateTimeToXLDate(this.m_objBatch.DateBegin);
            date2 = (double)XDate.DateTimeToXLDate(this.m_objBatch.DateEnd);
            pane.XAxis.Min = (double)date;
            pane.XAxis.Max = (double)date2;
            pane.XAxis.BaseTic = (double)date;
            pane.XAxis.IsShowGrid = false;
            pane.AxisFill = new Fill(Color.White, Color.LightGray, 45f);
            pane.MarginRight = 30f;
            pane.IsFontsScaled = true;
            pane.IsPenWidthScaled = true;
            pane.PaneBorder.IsVisible = false;
            pane.IsShowTitle = false;
            pane.IsShowTitle = false;
            pane.FontSpec.Size = 10.5f;
            pane.FontSpec.Family = "宋体";
            pane.YAxis.IsShowTitle = false;
            pane.XAxis.IsZeroLine = false;
            pane.YAxis.IsZeroLine = false;
            pane.XAxis.IsShowTitle = false;
            pane.XAxis.Type = AxisType.Date; 
            pane.XAxis.Step = 1.0;
            pane.XAxis.MajorUnit = DateUnit.Day; 
            pane.XAxis.ScaleFontSpec.Angle = 90f;
            pane.XAxis.ScaleFontSpec.IsBold = true;
            pane.XAxis.ScaleFormat = "M/dd";
            pane.XAxis.ScaleFontSpec.Family = "宋体";
            pane.XAxis.ScaleFontSpec.Size = 12f;
            pane.XAxis.ScaleFontSpec.IsBold = false;
            pane.YAxis.IsShowGrid = false;
            this.m_zedChart.AxisChange();
            this.m_zedChart.Refresh();
            return;
        }

        private void InitializeComponent()
        {
            ColumnHeader[] headerArray;
            this.components = new Container();
            this.m_pnlControl = new Panel();
            this.m_cmdPrint = new ButtonXP();
            this.label1 = new Label();
            this.m_cmdAnalysis = new ButtonXP();
            this.m_lsvQCResult = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.panel2 = new Panel();
            this.m_zedChart = new ZedGraphControl();
            this.m_gpbChartStyle = new GroupBox();
            this.m_rdbYoudenChart = new RadioButton();
            this.m_rdbZChart = new RadioButton();
            this.m_rdbLeveyChart = new RadioButton();
            this.m_cmsQCAnalysis = new ContextMenuStrip(this.components);
            this.m_pnlControl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_gpbChartStyle.SuspendLayout();
            base.SuspendLayout();
            this.m_pnlControl.Controls.Add(this.m_cmdPrint);
            this.m_pnlControl.Controls.Add(this.label1);
            this.m_pnlControl.Controls.Add(this.m_cmdAnalysis);
            this.m_pnlControl.Controls.Add(this.m_lsvQCResult);
            this.m_pnlControl.Dock = DockStyle.Right; 
            this.m_pnlControl.Location = new Point(0x264, 0);
            this.m_pnlControl.Name = "m_pnlControl";
            this.m_pnlControl.Size = new Size(0xaf, 0x21a);
            this.m_pnlControl.TabIndex = 5;
            this.m_cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdPrint.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = 0;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new Point(7, 0x13);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = 0;
            this.m_cmdPrint.Size = new Size(160, 0x21);
            this.m_cmdPrint.TabIndex = 10;
            this.m_cmdPrint.Text = "打印预览";
            this.m_cmdPrint.Click += new EventHandler(this.m_cmdPrint_Click);
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = Color.LightSteelBlue;
            this.label1.Location = new Point(6, 0x63);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa1, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "分析结果:";
            this.m_cmdAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAnalysis.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdAnalysis.ContextMenuStrip = this.m_cmsQCAnalysis;
            this.m_cmdAnalysis.DefaultScheme = true;
            this.m_cmdAnalysis.DialogResult = 0;
            this.m_cmdAnalysis.Hint = "";
            this.m_cmdAnalysis.Location = new Point(7, 0x3a);
            this.m_cmdAnalysis.Name = "m_cmdAnalysis";
            this.m_cmdAnalysis.Scheme = 0;
            this.m_cmdAnalysis.Size = new Size(160, 0x21);
            this.m_cmdAnalysis.TabIndex = 8;
            this.m_cmdAnalysis.Text = "质控分析(月)";
            this.m_cmdAnalysis.Click += new EventHandler(this.m_cmdAnalysis_Click);
            this.m_lsvQCResult.Activation = ItemActivation.OneClick;
            this.m_lsvQCResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvQCResult.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
            this.m_lsvQCResult.ForeColor = Color.Blue;
            this.m_lsvQCResult.GridLines = true;
            this.m_lsvQCResult.Location = new Point(6, 0x71);
            this.m_lsvQCResult.Name = "m_lsvQCResult";
            this.m_lsvQCResult.Size = new Size(0xa1, 0x19c);
            this.m_lsvQCResult.TabIndex = 6;
            this.m_lsvQCResult.UseCompatibleStateImageBehavior = false;
            this.m_lsvQCResult.View = System.Windows.Forms.View.Details;
            this.m_lsvQCResult.SelectedIndexChanged += new EventHandler(this.m_lsvQCResult_SelectedIndexChanged);
            this.columnHeader1.Text = "违反规则";
            this.columnHeader1.Width = 150;
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.m_zedChart);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0x37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x264, 0x1e3);
            this.panel2.TabIndex = 6;
            this.m_zedChart.Dock = DockStyle.Fill;
            this.m_zedChart.IsAutoScrollRange = false;
            this.m_zedChart.IsEnableHPan = true;
            this.m_zedChart.IsEnableHZoom = true;
            this.m_zedChart.IsEnableVPan = true;
            this.m_zedChart.IsEnableVZoom = true;
            this.m_zedChart.IsPrintFillPage = true;
            this.m_zedChart.IsPrintKeepAspectRatio = true;
            this.m_zedChart.IsScrollY2 = false;
            this.m_zedChart.IsShowContextMenu = true;
            this.m_zedChart.IsShowCopyMessage = true;
            this.m_zedChart.IsShowCursorValues = false;
            this.m_zedChart.IsShowHScrollBar = false;
            this.m_zedChart.IsShowPointValues = true;
            this.m_zedChart.IsShowVScrollBar = false;
            this.m_zedChart.IsZoomOnMouseCenter = false;
            this.m_zedChart.Location = new Point(0, 0);
            this.m_zedChart.Name = "m_zedChart";
            this.m_zedChart.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.m_zedChart.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.m_zedChart.PanModifierKeys2 = 0;
            this.m_zedChart.PointDateFormat = "g";
            this.m_zedChart.PointValueFormat = "G";
            this.m_zedChart.ScrollMaxX = 0.0;
            this.m_zedChart.ScrollMaxY = 0.0;
            this.m_zedChart.ScrollMaxY2 = 0.0;
            this.m_zedChart.ScrollMinX = 0.0;
            this.m_zedChart.ScrollMinY = 0.0;
            this.m_zedChart.ScrollMinY2 = 0.0;
            this.m_zedChart.Size = new Size(0x264, 0x1e3);
            this.m_zedChart.TabIndex = 6;
            this.m_zedChart.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.m_zedChart.ZoomButtons2 = 0;
            this.m_zedChart.ZoomModifierKeys = 0;
            this.m_zedChart.ZoomModifierKeys2 = 0;
            this.m_zedChart.ZoomStepFraction = 0.1;
            this.m_gpbChartStyle.Controls.Add(this.m_rdbYoudenChart);
            this.m_gpbChartStyle.Controls.Add(this.m_rdbZChart);
            this.m_gpbChartStyle.Controls.Add(this.m_rdbLeveyChart);
            this.m_gpbChartStyle.Dock = DockStyle.Top;
            this.m_gpbChartStyle.Location = new Point(0, 0);
            this.m_gpbChartStyle.Name = "m_gpbChartStyle";
            this.m_gpbChartStyle.Size = new Size(0x264, 0x37);
            this.m_gpbChartStyle.TabIndex = 7;
            this.m_gpbChartStyle.TabStop = false;
            this.m_gpbChartStyle.Text = "质控图类型选择";
            this.m_rdbYoudenChart.AutoSize = true;
            this.m_rdbYoudenChart.Location = new Point(0x129, 0x16);
            this.m_rdbYoudenChart.Name = "m_rdbYoudenChart";
            this.m_rdbYoudenChart.Size = new Size(0x43, 0x12);
            this.m_rdbYoudenChart.TabIndex = 2;
            this.m_rdbYoudenChart.Text = "Youden";
            this.m_rdbYoudenChart.UseVisualStyleBackColor = true;
            this.m_rdbYoudenChart.CheckedChanged += new EventHandler(this.m_rdbYouDen_CheckedChanged);
            this.m_rdbZChart.AutoSize = true;
            this.m_rdbZChart.Checked = true;
            this.m_rdbZChart.Location = new Point(0xa8, 0x16);
            this.m_rdbZChart.Name = "m_rdbZChart";
            this.m_rdbZChart.Size = new Size(0x51, 0x12);
            this.m_rdbZChart.TabIndex = 1;
            this.m_rdbZChart.TabStop = true;
            this.m_rdbZChart.Text = "Z-分数图";
            this.m_rdbZChart.UseVisualStyleBackColor = true;
            this.m_rdbZChart.CheckedChanged += new EventHandler(this.m_rdbZ_CheckedChanged);
            this.m_rdbLeveyChart.AutoSize = true;
            this.m_rdbLeveyChart.Location = new Point(0x1b, 0x16);
            this.m_rdbLeveyChart.Name = "m_rdbLeveyChart";
            this.m_rdbLeveyChart.Size = new Size(0x7b, 0x12);
            this.m_rdbLeveyChart.TabIndex = 0;
            this.m_rdbLeveyChart.Text = "Levey-Jennings";
            this.m_rdbLeveyChart.UseVisualStyleBackColor = true;
            this.m_rdbLeveyChart.CheckedChanged += new EventHandler(this.m_rdbLevey_CheckedChanged);
            this.m_cmsQCAnalysis.Name = "m_cmsQCAnalysis";
            this.m_cmsQCAnalysis.Size = new Size(0x99, 0x1a);
            base.AutoScaleDimensions = new SizeF(7f, 14f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.m_gpbChartStyle);
            base.Controls.Add(this.m_pnlControl);
            this.Font = new Font("宋体", 10.5f);
            base.Name = "ctlQCChartNew";
            base.Size = new Size(0x313, 0x21a);
            this.m_pnlControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_gpbChartStyle.ResumeLayout(false);
            this.m_gpbChartStyle.PerformLayout();
            this.ResumeLayout(false); 
        }

        private void InitializeZedChart()
        {
            GraphPane pane;
            this.m_zedChart.GraphPane = new GraphPane(this.m_zedChart.GraphPane.PaneRect, "", "", "");
            pane = this.m_zedChart.GraphPane;
            pane.MarginRight = 30f;
            this.m_zedChart.IsPrintFillPage = true;
            this.m_zedChart.IsPrintKeepAspectRatio = false;
            pane.IsFontsScaled = false;
            pane.IsPenWidthScaled = false;
            pane.PaneBorder.IsVisible = false;
            pane.IsShowTitle = false;
            pane.IsShowTitle = false;
            pane.FontSpec.Size = 10.5f;
            pane.FontSpec.Family = "宋体";
            pane.XAxis.IsZeroLine = false;
            pane.XAxis.IsShowTitle = false;
            pane.XAxis.Type = AxisType.Date;
            pane.XAxis.Step = 1.0;
            pane.XAxis.MajorUnit = DateUnit.Day;
            pane.XAxis.ScaleFontSpec.Angle = 90f;
            pane.XAxis.ScaleFontSpec.IsBold = true;
            pane.XAxis.ScaleFontSpec.Size = 12f;
            pane.XAxis.ScaleFormat = "M/dd";
            pane.XAxis.ScaleFontSpec.Family = "宋体";
            pane.XAxis.ScaleFontSpec.Size = 12f;
            pane.XAxis.ScaleFontSpec.IsBold = false;
            pane.YAxis.IsShowTitle = false;
            pane.YAxis.IsShowGrid = false;
            pane.YAxis.IsZeroLine = false;
            return;
        }

        private bool m_blnChoicedInNewConcentrations(List<clsLisQCConcentrationVO> p_choiced, List<clsLisQCConcentrationVO> p_newCons)
        {
            if (p_newCons.Count < p_choiced.Count)
                return false;
            bool bExist = false;
            foreach (clsLisQCConcentrationVO choice in p_choiced)
            {
                foreach (clsLisQCConcentrationVO con in p_newCons)
                {
                    if (choice.m_intConcentrationSeq == con.m_intConcentrationSeq)
                    {
                        bExist = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (bExist == false)
                {
                    return false;
                }
            }
            return true;
        }

        private void m_cmdAnalysis_Click(object sender, EventArgs e)
        {
            if (this.m_objBatch == null)
            {
                MessageBox.Show("数据读取失败！", "质控管理");
            }
            else
            {
                if (this.m_objBatch.Count == 1)
                {
                    if (this.m_cmdAnalysis.ContextMenuStrip == null)
                    {
                        this.m_mthQCAnalysisMonth(this.m_objBatch.DateBegin);
                    }
                    else
                    {
                        this.m_cmsQCAnalysis.Show(this.m_cmdAnalysis, 10, 10);
                    }
                }
            }
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            if(this.m_objBatch==null)
            {
                MessageBox.Show("数据读取失败！", "质控管理");
                return;
            }

            clsLisQCConcentrationVO objTemp = null;
            if (m_lstChoiceConcentration != null && m_lstChoiceConcentration.Count == 1)
            {
                objTemp = m_lstChoiceConcentration[0];
            }

            clsQCChartToolStrategy print = new clsQCChartToolStrategy(m_zedChart, this.m_objBatch, objTemp);
            print.m_mthPrintPreview();
        }

        private List<clsLisQCDataVO> m_lstDataListByConcentration()
        {
            List<clsQCDataPairItem> lstQCDataPair = m_lstQCDataPairItem;

            int intQCBatch = m_lstChoiceConcentration[0].m_intQCBatchSeq;

            List<clsLisQCDataVO> lst = new List<clsLisQCDataVO>();
            clsLisQCDataVO objVOTemp = null;

            foreach (clsQCDataPairItem pair in lstQCDataPair)
            {
                objVOTemp = pair[intQCBatch];

                if (objVOTemp != null)
                {
                    lst.Add(objVOTemp);
                }
            }

            return lst;
        }

        private void m_lsvQCResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lsvQCResult.FocusedItem == null)
            {
                return;
            }
            // 不符合规则数据


            List<clsLisQCDataVO> lstUnmatched = (List<clsLisQCDataVO>)m_lsvQCResult.FocusedItem.Tag;
            PointPairList lstPoints = new PointPairList();
            foreach (clsLisQCDataVO vo in lstUnmatched)
            {
                lstPoints.Add(XDate.DateTimeToXLDate(vo.m_datQCDate), vo.m_dlbResult);
            }

            if (lstPoints.Count > 0)
            {
                if (m_rdbLeveyChart.Checked)
                {
                    m_zedChart.GraphPane.CurveList.Clear();

                    LineItem itemCurveUnmatched = m_zedChart.GraphPane.AddCurve("违规数据", lstPoints, Color.Blue, SymbolType.Circle);
                    itemCurveUnmatched.Symbol.Fill = new Fill(Color.Blue);
                    itemCurveUnmatched.Line.IsVisible = false;

                    m_mthDrawLeveyStyle(m_lstChoiceConcentration[0]);
                    AddLeveyLineItems();
                    AddAssistLine((float)m_lstChoiceConcentration[0].m_dblAVG, (float)m_lstChoiceConcentration[0].m_dblSD);
                    m_zedChart.AxisChange();
                    m_zedChart.Refresh();

                }
                else if (m_rdbZChart.Checked)
                {
                    m_zedChart.GraphPane.CurveList.Clear();
                    LineItem itemCurveUnmatched = m_zedChart.GraphPane.AddCurve("违规数据", lstPoints, Color.Blue, SymbolType.Circle);
                    itemCurveUnmatched.Symbol.Fill = new Fill(Color.Blue);
                    itemCurveUnmatched.Line.IsVisible = false;

                    m_mthDrawZStyle();
                    AddZLineItems();
                    AddAssistLine(0, 1);
                    m_zedChart.AxisChange();
                    m_zedChart.Refresh();
                }
            }
        }

        private void m_mthAnalysisContext()
        {
            this.m_cmdAnalysis.ContextMenuStrip = null;
            if (!this.m_objBatch.IsNull && this.m_objBatch.Count == 1)
            {
                List<string> list = new List<string>();
                DateTime t = this.m_objBatch.m_datBegin.Date;
                while (t <= this.m_objBatch.m_datEnd.Date)
                {
                    list.Add(t.ToString("yyyy-MM"));
                    t = t.AddMonths(1);
                }
                if (list.Count > 1)
                {
                    this.m_cmsQCAnalysis.Items.Clear();
                    for (int i = 0; i < list.Count; i++)
                    {
                        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(list[i]);
                        toolStripMenuItem.Click += new EventHandler(this.QCAnalysisMonth_Click);
                        this.m_cmsQCAnalysis.Items.Add(toolStripMenuItem);
                    }
                    this.m_cmdAnalysis.ContextMenuStrip = this.m_cmsQCAnalysis;
                }
            }
        }

        private void m_mthConstructDataPairList()
        {
            if (!m_objBatch.IsNull)
                m_lstQCDataPairItem = clsQCDataPairItem.GetQCDataPairItemList(m_objBatch.GetDatas());
        }

        private void m_mthDrawLeveyChart()
        {
            if (!this.m_objBatch.IsNull && this.m_objBatch.Count == 1)
            {
                this.InitializeZedChart();
                if (this.m_lstChoiceConcentration.Count > 0)
                {
                    this.m_mthDrawLeveyStyle(this.m_lstChoiceConcentration[0]);
                    this.m_zedChart.GraphPane.CurveList.Clear();
                    this.AddLeveyLineItems();
                    this.AddAssistLine((float)this.m_lstChoiceConcentration[0].m_dblAVG, (float)this.m_lstChoiceConcentration[0].m_dblSD);
                    this.m_zedChart.AxisChange();
                    this.m_zedChart.Refresh();
                    this.m_mthSetAnalysisClick();
                }
                else
                {
                    this.InitializeChartNull();
                }
            }
            else
            {
                this.InitializeChartNull();
            }
        }

        private void m_mthDrawLeveyStyle(clsLisQCConcentrationVO p_con)
        {
            GraphPane pane;
            float num;
            float num2;
            XDate date;
            XDate date2;
            pane = this.m_zedChart.GraphPane;
            num = (float)p_con.m_dblAVG;
            num2 = (float)p_con.m_dblSD;
            date = (double)XDate.DateTimeToXLDate(this.m_objBatch.DateBegin);
            date2 = (double)XDate.DateTimeToXLDate(this.m_objBatch.DateEnd);
            pane.YAxis.Max = (double)(num + (4f * num2));
            pane.YAxis.Min = (double)(num - (4f * num2));
            pane.YAxis.BaseTic = pane.YAxis.Min;
            pane.YAxis.MinorStep = (double)num2;
            pane.YAxis.Step = (double)num2;
            pane.YAxis.ScaleFormat = "0.00";
            pane.XAxis.Min = (double)date;
            pane.XAxis.Max = (double)date2;
            pane.XAxis.BaseTic = (double)date;
            pane.XAxis.IsShowGrid = false;
            pane.AxisFill = new Fill(Color.White, Color.LightGray, 45f);
            return;
        }

        private void m_mthDrawYouDenBaseStyle()
        {
            GraphPane pane;
            float num;
            float num2;
            pane = this.m_zedChart.GraphPane;
            num = 0f;
            num2 = 1f;
            pane.YAxis.Max = (double)(num + (4f * num2));
            pane.YAxis.Min = (double)(num - (4f * num2));
            pane.YAxis.BaseTic = pane.YAxis.Min;
            pane.YAxis.Step = (double)num2;
            pane.YAxis.IsZeroLine = false;
            pane.YAxis.IsShowGrid = false;
            pane.XAxis.Min = (double)(num - (4f * num2));
            pane.XAxis.Max = (double)(num + (4f * num2));
            pane.XAxis.BaseTic = pane.XAxis.Min;
            pane.XAxis.Step = (double)num2;
            pane.XAxis.IsShowGrid = false;
            pane.XAxis.IsZeroLine = false;
            return;
        }

        private void m_mthDrawYouDenChart()
        {
            if (!m_objBatch.IsNull)
            {
                if (m_lstChoiceConcentration.Count == 2)
                {
                    #region Init
                    m_zedChart.GraphPane = new GraphPane(m_zedChart.GraphPane.PaneRect, "", "XAxis", "YAxis");
                    // print set
                    m_zedChart.IsPrintFillPage = true;
                    m_zedChart.IsPrintKeepAspectRatio = false;
                    GraphPane myPane = m_zedChart.GraphPane;

                    myPane.MarginRight = 50F;
                    myPane.MarginTop = 50F;
                    myPane.IsFontsScaled = true;
                    myPane.IsPenWidthScaled = true;
                    myPane.PaneBorder.IsVisible = false;
                    myPane.IsShowTitle = false;

                    myPane.IsShowTitle = false;
                    myPane.FontSpec.Size = 10.5F;
                    myPane.FontSpec.Family = "宋体";
                    myPane.YAxis.IsShowTitle = false;
                    myPane.YAxis.IsZeroLine = false;

                    myPane.XAxis.IsZeroLine = false;
                    myPane.XAxis.IsShowTitle = false;
                    myPane.YAxis.IsShowGrid = true;
                    myPane.XAxis.IsShowGrid = true;

                    myPane.AxisFill = new Fill(Color.White, Color.LightGray, 45.0f);
                    #endregion

                    myPane.CurveList.Clear();
                    m_mthDrawYouDenBaseStyle();

                    AddYouDenLineItems();
                    AddYouDenAssistLine();

                    m_zedChart.AxisChange();
                    m_zedChart.Refresh();
                }
                else
                {
                    InitializeChartNull();
                }
            }
            else
            {
                InitializeChartNull();
            }
        }

        private void m_mthDrawZChart()
        {
            if (!m_objBatch.IsNull)
            {
                if (m_lstChoiceConcentration.Count > 0)
                {
                    InitializeZedChart();
                    m_mthDrawZStyle();
                    m_zedChart.GraphPane.CurveList.Clear();
                    AddZLineItems();
                    AddAssistLine(0, 1);

                    m_zedChart.AxisChange();
                    m_zedChart.Refresh();

                    m_cmdAnalysis_Click(null, null);
                }
                else
                {
                    InitializeChartNull();
                }
            }
            else
            {
                InitializeChartNull();
            }
        }

        private void m_mthDrawZStyle()
        {
            GraphPane myPane = m_zedChart.GraphPane;

            float X = 0f;
            float SD = 1f;

            XDate xdtBegin = XDate.DateTimeToXLDate(m_objBatch.DateBegin);
            XDate xdtEnd = XDate.DateTimeToXLDate(m_objBatch.DateEnd);

            myPane.YAxis.Max = X + 4f * SD;
            myPane.YAxis.Min = X - 4f * SD;
            myPane.YAxis.BaseTic = myPane.YAxis.Min;
            myPane.XAxis.IsShowGrid = false;


            myPane.XAxis.Min = (double)xdtBegin;
            myPane.XAxis.Max = (double)xdtEnd;
            myPane.XAxis.BaseTic = (double)xdtBegin;
            myPane.YAxis.MinorStep = SD;

            myPane.AxisFill = new Fill(Color.White, Color.LightGray, 45.0f);
        }

        private void m_mthFreshChoiceConcentration(List<clsLisQCConcentrationVO> p_choiced, List<clsLisQCConcentrationVO> p_newCons)
        {
            if (m_blnChoicedInNewConcentrations(p_choiced, p_newCons))
            {
                for (int i = 0; i < p_choiced.Count; i++)
                {
                    for (int j = 0; j < p_newCons.Count; j++)
                    {
                        if (p_choiced[i].m_intConcentrationSeq == p_newCons[j].m_intConcentrationSeq)
                        {
                            p_choiced[i] = p_newCons[j];
                        }
                    }
                }
            }
        }

        private void m_mthQCAnalysisMonth(DateTime p_dtMonth)
        {
            if (!this.m_objBatch.IsNull)
            {
                if (this.m_objBatch.Count == 1)
                {
                    DateTime t = Convert.ToDateTime(p_dtMonth.ToString("yyyy-MM"));
                    DateTime t2 = p_dtMonth.AddMonths(1).AddSeconds(-1.0);
                    List<clsLisQCReportVO> objReports = this.m_objBatch.m_objReports;
                    clsLisQCReportVO clsLisQCReportVO = null;
                    for (int i = 0; i < objReports.Count; i++)
                    {
                        clsLisQCReportVO clsLisQCReportVO2 = objReports[i];
                        if (objReports[i].m_intReportStats == 1)
                        {
                            if (clsLisQCReportVO2.m_dtReport >= t && clsLisQCReportVO2.m_dtReport <= t2)
                            {
                                clsLisQCReportVO = clsLisQCReportVO2;
                                break;
                            }
                        }
                    }
                    frmQCReport frmQCReport;
                    if (clsLisQCReportVO != null)
                    {
                        frmQCReport = new frmQCReport(clsLisQCReportVO);
                        frmQCReport.m_blIsDate = false;
                    }
                    else
                    {
                        frmQCReport = new frmQCReport(this.m_objBatch.SeqArr[0], p_dtMonth);
                        frmQCReport.m_blIsDate = false;
                        frmQCReport.BrokenRules = this.m_objBatch.BrokenRules;
                        frmQCReport.m_lblSeq.Text = "质控批序号:" + this.m_objBatch.SeqArr[0].ToString();
                    }
                    if (frmQCReport.ShowDialog() == DialogResult.OK)
                    {
                        clsLisQCReportVO report = frmQCReport.Report;
                        if (clsLisQCReportVO == null)
                        {
                            this.m_objBatch.m_objReports.Add(report);
                        }
                        else
                        {
                            report.m_mthCopyTo(clsLisQCReportVO);
                        }
                    }
                }
            }
        }

        private void m_mthSetAnalysisClick()
        {
            if (!this.m_objBatch.IsNull && this.m_objBatch.Count == 1)
            {
                if (this.m_rdbLeveyChart.Checked)
                {
                    this.m_mthShowAnalysisList(false);
                }
                else
                {
                    this.m_mthShowAnalysisList(true);
                }
            }
        }

        private void m_mthShowAnalysisList(bool p_blnChange)
        {
            List<QualityControlRule> m_ruls = new List<QualityControlRule>();
            QualityControlData m_qcData;
            List<clsLisQCDataVO> m_lstQCAnalysisVO = new List<clsLisQCDataVO>();
            Hashtable m_hasAnalysisResult = new Hashtable();
            QcParserXmlRules parser = new QcParserXmlRules(m_objBatch.GetQCBatchSet()[0].m_strQCRules);
            m_ruls = parser.RuleList;

            List<clsQCDataPairItem> lstQCDataPair = m_lstQCDataPairItem;

            foreach (clsQCDataPairItem pair in lstQCDataPair)
            {
                foreach (clsLisQCConcentrationVO con in m_lstChoiceConcentration)
                {
                    if (pair[con.m_intQCBatchSeq] != null)
                    {
                        clsLisQCDataVO temp = pair[con.m_intQCBatchSeq];
                        if (p_blnChange)
                        {
                            temp = new clsLisQCDataVO();
                            pair[con.m_intQCBatchSeq].m_mthCopyTo(temp);
                            double X = con.m_dblAVG;
                            double SD = con.m_dblSD;
                            if (SD == 0)
                            {
                                SD = double.MaxValue;
                            }
                            temp.m_dlbResult = (temp.m_dlbResult - X) / SD;
                        }
                        m_lstQCAnalysisVO.Add(temp);
                    }
                }
            }

            double[] arrResult = new double[m_lstQCAnalysisVO.Count];
            for (int i = 0; i < m_lstQCAnalysisVO.Count; i++)
            {
                arrResult[i] = m_lstQCAnalysisVO[i].m_dlbResult;
            }

            m_qcData = new QualityControlData(arrResult);
            if (p_blnChange)
            {
                m_qcData.X = 0f;
                m_qcData.SD = 1f;
            }
            else if (m_lstChoiceConcentration.Count > 0)
            {
                m_qcData.X = m_lstChoiceConcentration[0].m_dblAVG;
                m_qcData.SD = m_lstChoiceConcentration[0].m_dblSD;
            }

            m_hasAnalysisResult = QualityControlRulesParser.GetRulesDataResult(m_ruls, m_qcData);

            this.m_lsvQCResult.BeginUpdate();
            this.m_lsvQCResult.Items.Clear();
            if (m_hasAnalysisResult != null)
            {
                string ruleReport = string.Empty;
                foreach (DictionaryEntry dic in m_hasAnalysisResult)
                {
                    List<clsLisQCDataVO> lstUnmatchedData = new List<clsLisQCDataVO>();
                    ruleReport += dic.Key.ToString() + "、";
                    ListViewItem item = new ListViewItem(dic.Key.ToString());
                    StringBuilder sb = new StringBuilder();
                    List<int> lstPos = (List<int>)dic.Value;
                    foreach (int pos in lstPos)
                    {
                        // 填充不符合规则列表
                        lstUnmatchedData.Add(m_lstQCAnalysisVO[pos]);
                        sb.Append("|");
                        sb.Append(m_lstQCAnalysisVO[pos].m_dlbResult);
                    }
                    item.Tag = lstUnmatchedData;
                    item.SubItems.Add(sb.ToString());
                    this.m_lsvQCResult.Items.Add(item);
                }
                if (ruleReport != string.Empty)
                {
                    m_objBatch.BrokenRules = ruleReport.Remove(ruleReport.Length - 1, 1);
                }
            }
            this.m_lsvQCResult.EndUpdate();//结束更新列表
        }

        private void m_objBatch_ConcentrationChanged(object sender, EventArgs e)
        {
            m_lstQCDataPairItem = null;
            m_lsvQCResult.Items.Clear();

            m_mthConstructDataPairList();
            List<clsLisQCConcentrationVO> list = this.m_objBatch.GetConcentrations();
            if (m_blnChoicedInNewConcentrations(m_lstChoiceConcentration, m_objBatch.GetConcentrations()))
            {
                m_mthFreshChoiceConcentration(m_lstChoiceConcentration, m_objBatch.GetConcentrations());
                if (m_rdbLeveyChart.Checked)
                {
                    m_mthDrawLeveyChart();
                    this.m_cmdAnalysis.Enabled = true;
                }
                if (m_rdbZChart.Checked)
                {
                    m_mthDrawZChart();
                    this.m_cmdAnalysis.Enabled = true;
                }
                if (m_rdbYoudenChart.Checked)
                {
                    m_mthDrawYouDenChart();
                }
            }
            else if (list.Count > 0)
            {
                m_rdbLeveyChart.CheckedChanged -= new EventHandler(m_rdbLevey_CheckedChanged);
                this.m_rdbLeveyChart.Checked = true;
                m_rdbLeveyChart.CheckedChanged += new EventHandler(m_rdbLevey_CheckedChanged);

                m_lstChoiceConcentration.Clear();
                this.m_lstChoiceConcentration.Add(list[0]);
                this.m_cmdAnalysis.Enabled = true;
                m_mthDrawLeveyChart();
            }
        }

        private void m_objBatch_DataChanged(object sender, EventArgs e)
        {
            this.m_lsvQCResult.Items.Clear();
            m_mthConstructDataPairList();
            if (this.m_rdbLeveyChart.Checked)
            {
                m_mthDrawLeveyChart();
            }
            else if (this.m_rdbZChart.Checked)
            {
                m_mthDrawZChart();
            }
            else if (this.m_rdbYoudenChart.Checked)
            {
                m_mthDrawYouDenChart();
            }
        }

        private void m_objBatch_Loaded(object sender, EventArgs e)
        {
            m_lstChoiceConcentration.Clear();
            m_lstQCDataPairItem = null;
            m_lsvQCResult.Items.Clear();

            m_rdbLeveyChart.CheckedChanged -= new EventHandler(m_rdbLevey_CheckedChanged);
            this.m_rdbLeveyChart.Checked = true;
            m_rdbLeveyChart.CheckedChanged += new EventHandler(m_rdbLevey_CheckedChanged);

            m_mthConstructDataPairList();
            List<clsLisQCConcentrationVO> list = this.m_objBatch.GetConcentrations();
            if (list.Count > 0)
            {
                this.m_lstChoiceConcentration.Add(list[0]);
            }
            m_mthDrawLeveyChart();
            this.m_mthAnalysisContext();
        }

        private void m_objBatch_Reloaded(object sender, EventArgs e)
        {
            this.m_lsvQCResult.Items.Clear();
            m_mthConstructDataPairList();
            if (this.m_rdbLeveyChart.Checked)
            {
                m_mthDrawLeveyChart();
            }
            else if (this.m_rdbZChart.Checked)
            {
                m_mthDrawZChart();
            }
            else if (this.m_rdbYoudenChart.Checked)
            {
                m_mthDrawYouDenChart();
            }
            else
            {
                this.m_mthAnalysisContext();
            }
        }

        private void m_objBatch_Reseted(object sender, EventArgs e)
        {
            m_lstChoiceConcentration.Clear();
            m_lstQCDataPairItem = null;
            m_lsvQCResult.Items.Clear();

            // 默认样式
            m_gpbChartStyle.Visible = true;
            m_pnlControl.Visible = true;

            m_rdbLeveyChart.CheckedChanged -= new EventHandler(m_rdbLevey_CheckedChanged);
            this.m_rdbLeveyChart.Checked = true;
            m_rdbLeveyChart.CheckedChanged += new EventHandler(m_rdbLevey_CheckedChanged);
            m_cmdAnalysis.Enabled = true;
            m_mthDrawLeveyChart();
        }

        private void m_objBatch_SetChanged(object sender, EventArgs e)
        {
            if (this.m_rdbLeveyChart.Checked)
            {
                m_mthDrawLeveyChart();
            }
            else if (this.m_rdbZChart.Checked)
            {
                m_mthDrawZChart();
            }
            else if (this.m_rdbYoudenChart.Checked)
            {
                m_mthDrawYouDenChart();
            }
            this.m_lsvQCResult.Items.Clear();
        }

        private void m_rdbLevey_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdbLeveyChart.Checked)
            {
                List<clsLisQCConcentrationVO> lstQCCon = m_objBatch.GetConcentrations();
                if (lstQCCon == null || lstQCCon.Count <= 0)
                    return;

                if (lstQCCon.Count == 1)
                {
                    m_lstChoiceConcentration = lstQCCon;
                    this.m_lsvQCResult.Items.Clear();
                    m_cmdAnalysis.Enabled = true;
                    m_mthDrawLeveyChart();
                }
                else
                {
                    frmQCConcentrationSelector frm = new frmQCConcentrationSelector(lstQCCon, 1, 1);
                    Point p = m_rdbLeveyChart.Parent.PointToScreen(m_rdbLeveyChart.Location);
                    frm.Location = new Point(p.X, p.Y + 15);

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        m_lstChoiceConcentration = frm.Concentrations;
                        this.m_lsvQCResult.Items.Clear();
                        m_cmdAnalysis.Enabled = true;
                        m_mthDrawLeveyChart();
                    }
                }
            }
        }

        private void m_rdbYouDen_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdbYoudenChart.Checked)
            {
                frmQCConcentrationSelector frm = new frmQCConcentrationSelector(m_objBatch.GetConcentrations(), 2, 2);
                Point p = m_rdbYoudenChart.Parent.PointToScreen(m_rdbYoudenChart.Location);
                frm.Location = new Point(p.X, p.Y + 15);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    m_lstChoiceConcentration = frm.Concentrations;
                    this.m_lsvQCResult.Items.Clear();
                    m_cmdAnalysis.Enabled = false;
                    m_mthDrawYouDenChart();
                }
            }
        }

        private void m_rdbZ_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdbZChart.Checked)
            {
                List<clsLisQCConcentrationVO> lstQCCon = m_objBatch.GetConcentrations();
                if (lstQCCon == null || lstQCCon.Count <= 0)
                    return;
                if (lstQCCon.Count == 1)
                {
                    m_lstChoiceConcentration = lstQCCon;
                    m_cmdAnalysis.Enabled = true;
                    this.m_lsvQCResult.Items.Clear();
                    m_mthDrawZChart();
                }
                else
                {
                    frmQCConcentrationSelector frm = new frmQCConcentrationSelector(lstQCCon, 1, m_objBatch.GetConcentrations().Count);
                    Point p = m_rdbZChart.Parent.PointToScreen(m_rdbZChart.Location);
                    frm.Location = new Point(p.X, p.Y + 15);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        m_lstChoiceConcentration = frm.Concentrations;
                        m_cmdAnalysis.Enabled = true;
                        this.m_lsvQCResult.Items.Clear();
                        m_mthDrawZChart();
                    }
                }
            }
        }

        private void QCAnalysisMonth_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                DateTime p_dtMonth = DateTime.MinValue;
                try
                {
                    p_dtMonth = Convert.ToDateTime(toolStripMenuItem.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "质控管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                this.m_mthQCAnalysisMonth(p_dtMonth);
            }
        }

        // Properties
        internal clsQCBatchNew ObjBatch
        {
            set
            {
                if (value != null)
                {
                    m_objBatch_Reseted(this, EventArgs.Empty);
                    m_objBatch = value;
                    m_objBatch.Reseted += new EventHandler(m_objBatch_Reseted);
                    m_objBatch.Loaded += new EventHandler(m_objBatch_Loaded);
                    m_objBatch.Reloaded += new EventHandler(m_objBatch_Reloaded);
                    m_objBatch.SetChanged += new EventHandler(m_objBatch_SetChanged);
                    m_objBatch.DataChanged += new EventHandler(m_objBatch_DataChanged);
                    m_objBatch.ConcentrationChanged += new EventHandler(m_objBatch_ConcentrationChanged);

                    m_mthConstructDataPairList();
                    List<clsLisQCConcentrationVO> list = this.m_objBatch.GetConcentrations();
                    if (list != null && list.Count > 0)
                    {
                        this.m_lstChoiceConcentration.Add(list[0]);
                    }
                    m_mthDrawLeveyChart();
                }
                else
                {
                    throw new System.ArgumentNullException();
                }
            }
        }
    }

    public class QcParserXmlRules
    {
        private List<QualityControlRule> ruleList = null;
        private XmlReader rulesReader;

        public QcParserXmlRules(string xmlData)
        {
            ruleList = new List<QualityControlRule>();
            ParserXmlStringRules(xmlData);
        }

        private bool ParserXmlStringRules(string xmlData)
        {
            try
            {
                CreateReaderByString(xmlData);
                processXmlReader(rulesReader);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public QualityControlRule Rule
        {
            get
            {
                if (ruleList.Count > 0)
                    return ruleList[0];
                return null;
            }
        }

        public List<QualityControlRule> RuleList
        {
            get
            {
                if (ruleList.Count > 0)
                    return ruleList;
                else
                    return null;
            }
        }

        /// <summary>
        /// 创建Xml的访问对象

        /// </summary>
        /// <returns></returns>
        private XmlReader CreateReaderByString(string xmlData)
        {
            try
            {
                return rulesReader = XmlReader.Create(new StringReader(xmlData));
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// 解析Xml文件成实例

        /// </summary>
        /// <param name="xmlRule"></param>
        private void processXmlReader(XmlReader xmlRule)
        {
            try
            {
                QualityControlRule oneRule = new QualityControlRule();
                while (xmlRule.Read())
                {
                    xmlRule.MoveToElement();
                    while (xmlRule.Read())
                    {
                        if (xmlRule.Name.ToLower().Trim() == "rule")
                        {
                            oneRule.Name = xmlRule.GetAttribute("name");
                        }
                        if (xmlRule.Name.ToLower().Trim() == "formula")
                        {
                            oneRule.Formula = xmlRule.ReadString();
                        }
                        if (xmlRule.Name.ToLower().Trim() == "joined")
                        {
                            oneRule.Joined = xmlRule.ReadElementContentAsBoolean();
                        }
                        if (xmlRule.Name.ToLower().Trim() == "number")
                        {
                            oneRule.Numer = xmlRule.ReadElementContentAsInt();
                        }
                        if (xmlRule.Name.ToLower().Trim() == "warning")
                        {
                            oneRule.IsWarning = xmlRule.ReadElementContentAsBoolean();
                            ruleList.Add(oneRule);
                            oneRule = new QualityControlRule();
                        }
                    }
                    break;
                }
            }
            finally
            {
                xmlRule.Close();
            }
        }

        public static string ParserOneRuleToXmlString(QualityControlRule rule)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<rule name=\"{0}\">");
            sb.AppendLine();
            sb.Append(@"  <formula>{1}</formula>");
            sb.AppendLine();
            sb.Append(@"    <joined>{2}</joined>");
            sb.AppendLine();
            sb.Append(@"     <Number>{3}</Number>");
            sb.AppendLine();
            sb.Append(@"      <warning>{4}</warning>");
            sb.AppendLine();
            sb.Append(@"</rule>");
            return
                string.Format(sb.ToString(), rule.Name, rule.Formula, Convert.ToInt32(rule.Joined), rule.Numer,
                              Convert.ToInt32(rule.IsWarning));
        }

        public static string ParserRuleArrToXmlString(List<QualityControlRule> rules)
        {
            //    if (rules.Count == 0)
            //        return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("<Rules>");
            foreach (QualityControlRule rule in rules)
            {
                sb.Append(ParserOneRuleToXmlString(rule));
            }
            sb.Append("</Rules>");
            return sb.ToString();
        }
    }

    public class QualityControlRule
    {
        private string name;    //规则名称
        private string formula; //判定表达式名称

        private bool joined;    //是否要求连续
        private int number;     //符合规则的数目

        private bool isWarning; //质控规则的报警


        public QualityControlRule() { }

        public QualityControlRule(string name, string formula, bool joined, int number, bool isWarning)
        {
            this.name = name;
            this.formula = formula;
            this.joined = joined;
            this.number = number;
            this.isWarning = isWarning;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Formula
        {
            get
            {
                return formula;
            }
            set
            {
                formula = value;
            }
        }

        public bool Joined
        {
            get
            {
                return joined;
            }
            set
            {
                joined = value;
            }
        }

        public int Numer
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public bool IsWarning
        {
            get { return isWarning; }
            set { isWarning = value; }
        }

    }

    public class QualityControlData
    {
        private double[] ruleData;
        private double max;
        private double min;
        private double x;
        private double sd;

        public QualityControlData(double[] ruleData)
        {
            if (ruleData.Length > 0)
            {
                this.ruleData = (double[])ruleData.Clone();
                min = ruleData[0];
                foreach (double num in ruleData)
                {
                    if (num < min)
                        min = num;
                }

                max = ruleData[0];
                foreach (double num in ruleData)
                {
                    if (num > max)
                    {
                        max = num;
                    }
                }
            }
        }

        /// <summary>
        /// 低质控测定值

        /// </summary>
        public double Min
        {
            get
            {

                return min;
            }
        }

        /// <summary>
        ///  高质控测定值

        /// </summary>
        public double Max
        {
            get
            {

                return max;
            }
        }

        /// <summary>
        /// 平均数（X）

        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x >= 0)
                {
                    x = value;
                }
            }
        }

        /// <summary>
        /// s
        /// </summary>
        public double SD
        {
            get
            {
                return sd;
            }
            set
            {
                if (sd >= 0)
                {
                    sd = value;
                }
            }
        }
        public double[] Data
        {
            get
            {
                return ruleData;
            }
        }
    }

    public class QualityControlRulesParser
    {
        private QualityControlData data;
        private QualityControlRule rule;
        private List<int> result;

        #region 已定义的Pattern
        private string valuePattern = @"$VALUE";
        private string prevValuePattern = @"$PREVVALUE";
        private string maxPattern = @"$MAX";
        private string minPattern = @"$MIN";
        private string xPattern = @"$X";
        private string sPattern = @"$S";


        #endregion

        #region 把已经定义的字符串转化为具体的数值

        /// <summary>
        /// 替换方程中定义的变量
        /// </summary>
        /// <param name="formula">方程字符串</param>
        /// <param name="value">值</param>
        /// <param name="prevValue">下一个值</param>
        /// <returns>数学表达式的比较表达式字符串</returns>
        private string ReplaceFormulaDefinedVariable(string formula, string value, string prevValue)
        {
            StringBuilder sb = new StringBuilder(formula);
            sb.Replace(maxPattern, data.Max.ToString());
            sb.Replace(minPattern, data.Min.ToString());
            sb.Replace(xPattern, data.X.ToString());
            sb.Replace(sPattern, data.SD.ToString());
            sb.Replace(valuePattern, value);
            sb.Replace(prevValuePattern, prevValue);
            return sb.ToString();
        }

        /// <summary>
        /// 替换方程中定义的变量
        /// </summary>
        /// <param name="formula">方程字符串</param>
        /// <returns>比较表达式字符串</returns>
        private string ReplaceFormulaDefinedVariable(string formula)
        {
            StringBuilder sb = new StringBuilder(formula.ToUpper());
            sb.Replace(maxPattern, data.Max.ToString());
            sb.Replace(minPattern, data.Min.ToString());
            sb.Replace(xPattern, data.X.ToString());
            sb.Replace(sPattern, data.SD.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 替换方程中定义的变量
        /// </summary>
        /// <param name="formula">方程字符串</param>
        /// <returns>比较表达式字符串</returns>
        private string ReplaceFormulaDefinedVariable(string formula, string value)
        {
            StringBuilder sb = new StringBuilder(formula.ToUpper());
            sb.Replace(maxPattern, data.Max.ToString());
            sb.Replace(minPattern, data.Min.ToString());
            sb.Replace(xPattern, data.X.ToString());
            sb.Replace(sPattern, data.SD.ToString());
            sb.Replace(valuePattern, value);
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 构造解析器
        /// </summary>
        /// <param name="rule">规则</param>
        /// <param name="data">质控数据</param>
        public QualityControlRulesParser(QualityControlRule rule, QualityControlData data)
        {
            this.rule = rule;
            this.data = data;
            FillResult();
        }

        #region 处理代码

        /// <summary>
        /// 解析方程字符串为一个方程的集合
        /// </summary>
        /// <returns>方程集合</returns>
        private string[] GetRuleFormulaArray()
        {
            return rule.Formula.Trim().Split('|');
        }

        /// <summary>
        /// 集合中的值在表达式中做运算，得出得结果Bool集合
        /// </summary>
        /// <param name="arr">数组集</param>
        /// <returns>Bool集合</returns>
        private List<bool> ProcessDataToBoolArray(string formulaRule)
        {
            string formula = formulaRule;
            ExpressionEval eval = new ExpressionEval();
            List<bool> contented = new List<bool>();
            if (data != null)
            {
                if (data.Data != null)
                {
                    for (int i = 0; i < data.Data.Length; i++)
                    {
                        if (!formula.ToUpper().Contains("$PREVVALUE") && !formula.ToUpper().Contains("$MAX"))
                        {
                            eval.Expression = ReplaceFormulaDefinedVariable(formula, data.Data[i].ToString());
                            contented.Add(eval.EvaluateBool());
                        }

                        //处理包含最大最小值

                        if (formula.ToUpper().Contains("$MAX"))
                        {
                            eval.Expression = ReplaceFormulaDefinedVariable(formula, data.Data[i].ToString());
                            //contented.Add(eval.EvaluateBool());
                            if (eval.EvaluateBool())
                            {
                                if (data.Data[i] == data.Max || data.Data[i] == data.Min)
                                {
                                    contented.Add(true);
                                }
                                else
                                {
                                    contented.Add(false);
                                }
                            }
                        }

                        //处理上升或下降趋势

                        if (formula.ToUpper().Contains("$PREVVALUE") && i != 0)
                        {
                            eval.Expression = ReplaceFormulaDefinedVariable(formula, data.Data[i].ToString(), data.Data[i - 1].ToString());
                            contented.Add(eval.EvaluateBool());
                        }
                        if (formula.ToUpper().Contains("$PREVVALUE") && i == 0)
                        {
                            contented.Add(true);
                        }
                    }
                }
            }
            return contented;
        }

        //获取不符合数据的位置集合
        private List<int> GetUnmatchedDataPosList(string formulaRule)
        {
            string formula = formulaRule;
            List<int> posList = new List<int>();

            //转化为Bool类型数组
            List<bool> lstAccordData = ProcessDataToBoolArray(formula);
            int trueNum = 0;

            for (int i = 0; i < lstAccordData.Count; i++)
            {
                if (lstAccordData[i])
                {
                    posList.Add(i);
                    trueNum++;
                }

            }

            //如果不要求连续，并且数目超过限定值

            if (!rule.Joined && trueNum >= rule.Numer && !rule.Formula.ToUpper().Contains("$MAX"))
            {
                return posList;
            }

            if (trueNum >= rule.Numer && rule.Formula.ToUpper().Contains("$MAX"))
            {
                return posList;
            }

            //如果连续
            if (rule.Joined)
            {
                int startPos = 0;   //连续集合起始位

                int joinedNum = 0;  //连续的数目

                posList.Clear();

                for (int i = 0; i < lstAccordData.Count; i++)
                {
                    if (lstAccordData[i])
                    {
                        startPos = joinedNum == 0 ? i : startPos;
                        joinedNum++;
                    }
                    // 当判定为假，或者最后一个的时候

                    if (!lstAccordData[i] || (i == lstAccordData.Count - 1 && lstAccordData[i]))
                    {
                        if (joinedNum >= rule.Numer)
                        {
                            for (int j = startPos; j < startPos + joinedNum; j++)
                            {
                                posList.Add(j);
                            }
                        }
                        joinedNum = 0;
                    }
                }
                return posList;
            }
            return null;
        }

        /// <summary>
        /// 根据Rule的多条方程。产生符合规则的数据
        /// </summary>
        /// <returns>HasTable,formula为Key</returns>
        private Hashtable GetProcessData()
        {
            Hashtable hasAccordFormulaData = new Hashtable();
            foreach (string formula in GetRuleFormulaArray())
            {
                if (GetUnmatchedDataPosList(formula) != null)
                    hasAccordFormulaData.Add(formula, GetUnmatchedDataPosList(formula));
            }
            return hasAccordFormulaData;
        }

        //填充结果
        public void FillResult()
        {
            List<int> lstPos = new List<int>();
            Hashtable result = GetProcessData();
            if (result != null)
            {
                foreach (DictionaryEntry dic in result)
                {
                    List<int> posList = (List<int>)dic.Value;
                    foreach (int pos in posList)
                    {
                        lstPos.Add(pos);
                    }
                }
                this.result = lstPos;
            }
        }

        /// <summary>
        /// 计算单独的表达式子

        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        private double EvalExpression()
        {
            string formula = rule.Formula;
            string expression = ReplaceFormulaDefinedVariable(formula);
            ExpressionEval eval = new ExpressionEval(expression);
            return eval.EvaluateDouble();
        }
        #endregion

        public List<int> GetResult()
        {
            return result;
        }

        public static Hashtable GetRulesDataResult(List<QualityControlRule> rules, QualityControlData qcData)
        {
            Hashtable result = new Hashtable();
            if (rules != null)
            {
                foreach (QualityControlRule rule in rules)
                {
                    QualityControlRulesParser parser = new QualityControlRulesParser(rule, qcData);
                    if (parser.GetResult().Count > 0)
                    {
                        result.Add(rule.Name, parser.GetResult());
                    }
                }
            }
            return result;
        }

        public static Hashtable GetRulesDataResult(List<QualityControlRule> rules, List<QualityControlData> qcDatas)
        {
            Hashtable result = new Hashtable();
            if (rules != null && qcDatas != null)
            {
                foreach (QualityControlRule rule in rules)
                {
                    foreach (QualityControlData qcData in qcDatas)
                    {
                        QualityControlRulesParser parser = new QualityControlRulesParser(rule, qcData);
                        if (parser.GetResult().Count > 0)
                        {
                            result.Add(rule.Name, parser.GetResult());
                        }
                    }
                }
            }
            return result;
        }

    }
}
