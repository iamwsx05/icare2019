using Common.Controls;
using Common.Entity;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class frmPrintDocument : frmBasePopup
    {
        private Size pageSize = new Size(850, 1170);
        public Size PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// 防止第一次加载窗体时部分事件重复触发
        /// </summary>
        private bool blnFirstLoad = true;

        /// <summary>
        /// 当前打印页在打印序列中的索引
        /// </summary>
        int intPrintIndex = 0;
        /// <summary>
        /// 初始时页码
        /// </summary>
        int intInitPageIndex = 1;

        public int InitPageIndex
        {
            set { intInitPageIndex = value; }
        }

        private List<int> lstPrintPageList = new List<int>();

        /// <summary>
        /// 需要打印的页序列
        /// </summary>
        public List<int> PrintPageList
        {
            get { return lstPrintPageList; }
            set { lstPrintPageList = value; }
        }

        private enum EnumPrintType { 正常, 续打, 套打 };

        /// <summary>
        /// 打印方式
        /// </summary>
        private EnumPrintType printType = EnumPrintType.正常;

        private EnumPrintType PrintType
        {
            get { return printType; }
            set
            {
                switch (value)
                {
                    case EnumPrintType.续打:
                        {
                            //续打
                            btnContinue.Border = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
                            btnSelect.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            printLine.Visible = true;
                            printBox.Visible = false;
                            printBox.Tag = "idle";//标识套打控件为“空闲”
                            if (printType != value)
                            {
                                //重置Y坐标
                                printLine.Top = 30;
                            }
                            break;
                        }
                    case EnumPrintType.套打:
                        {
                            //套打
                            btnContinue.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            btnSelect.Border = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
                            printLine.Visible = false;
                            printBox.Visible = false;

                            //标识套打pannel为“活跃”状态,滚动时PrintPreviewControl控件以这个值判断是否对printBox重新设置top位置
                            //解决套打不能超过一页的问题。 --黄泳亮 2013.5.9
                            printBox.Tag = "active";
                            break;
                        }
                    default:
                        {
                            //正常情况
                            btnContinue.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            btnSelect.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                            printLine.Visible = false;
                            printBox.Visible = false;
                            printBox.Tag = "idle";//标识套打控件为“空闲”
                            break;
                        }
                }
                printType = value;
            }
        }

        /// <summary>
        /// 续打线
        /// </summary>
        private Panel printLine = new Panel();

        /// <summary>
        /// 套打框
        /// </summary>
        private Panel printBox = new Panel();

        /// <summary>
        /// 是否移动续打线标志位
        /// </summary>
        private bool movePrintLine = false;

        /// <summary>
        /// 是否移动套打框标志位
        /// </summary>
        private bool movePrintBox = false;

        /// <summary>
        /// Y方向移动
        /// </summary>
        private int startY = 0;

        /// <summary>
        /// X方向移动
        /// </summary>
        private int startX = 0;

        /// <summary>
        /// 预览时页间宽度
        /// </summary>
        private int spaceOrg = 10;

        /// <summary>
        /// 当前显示的页
        /// </summary>
        private int viewIndex = 0;

        /// <summary>
        /// 是否正在执行翻页
        /// </summary>
        private bool inGoToPage = false;

        public int PrintPageType { set; get; }

        /// <summary>
        /// 页范围
        /// </summary>
        private List<string> PrintPageScope { set; get; }

        /// <summary>
        /// 页打印方向
        /// </summary>
        private Dictionary<int, bool> PrintPageLandScape = new Dictionary<int, bool>();

        private List<Image> lstPrintImg = new List<Image>();
        private List<Image> lstPrintNoLineImg = new List<Image>();

        /// <summary>
        /// 初始化
        /// </summary>
        public frmPrintDocument(List<Image> p_lstImg, List<Image> p_lstNoLineImg)
        {
            InitializeComponent();
            InitializePrintLine();
            InitializePrintBox();
            lstPrintImg = p_lstImg;
            lstPrintNoLineImg = p_lstNoLineImg; 
        }

        #region 初始化续打线
        /// <summary>
        /// 初始化续打线
        /// </summary>
        private void InitializePrintLine()
        {
            //设置父控件为printControl以保证坐标系一致
            printControl.Controls.Add(printLine);
            printLine.BackColor = Color.Red;
            printLine.Height = 3;
            printLine.Width = printControl.Width;
            printLine.Cursor = Cursors.HSplit;
            printLine.Top = 30;
            printLine.Visible = false;
            printLine.MouseDown += new MouseEventHandler(printLine_MouseDown);
            printLine.MouseUp += new MouseEventHandler(printLine_MouseUp);
            printLine.MouseMove += new MouseEventHandler(printLine_MouseMove);
        }
        #endregion

        #region 初始化套打框
        /// <summary>
        /// 初始化套打框
        /// </summary>
        private void InitializePrintBox()
        {
            printControl.Controls.Add(printBox);
            printBox.Size = new Size(0, 0);
            printBox.Visible = false;
            printBox.Cursor = Cursors.Hand;
            printBox.BackColor = Color.Green;
            printBox.BorderStyle = BorderStyle.FixedSingle;
            printBox.MouseDown += new MouseEventHandler(printBox_MouseDown);
            printBox.MouseUp += new MouseEventHandler(printBox_MouseUp);
            printBox.MouseMove += new MouseEventHandler(printBox_MouseMove);
            printBox.SizeChanged += new EventHandler(printBox_SizeChanged);
        }
        #endregion

        #region 套打框事件
        /// <summary>
        /// 移动套打框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (movePrintBox)
            {
                if (printBox.Visible)
                {
                    printBox.Visible = false;
                    printBox.BackColor = Color.White;
                }
                printBox.Top += e.Y - startY;
                printBox.Left += e.X - startX;

                if (printBox.Top < 10)
                {
                    printBox.Top = 10;
                }
                if (printBox.Top > printControl.Height - 10)
                {
                    printBox.Top = printControl.Height - 10;
                }

                printControl.Refresh();

                Graphics g = printControl.CreateGraphics();
                Rectangle rec = printBox.Bounds;
                g.DrawRectangle(Pens.Red, rec);
            }
        }

        /// <summary>
        /// 移动套打框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (movePrintBox)
            {
                printControl.Refresh();
                ResetPanelBackColor();
            }

            movePrintBox = false;
        }

        /// <summary>
        /// 移动套打框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printBox_MouseDown(object sender, MouseEventArgs e)
        {
            movePrintBox = true;
            startY = e.Y;
            startX = e.X;
        }

        private void printBox_SizeChanged(object sender, EventArgs e)
        {
            ResetPanelBackColor();
        }
        #endregion

        #region 装订线事件
        /// <summary>
        /// 移动续打线_鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printLine_MouseMove(object sender, MouseEventArgs e)
        {
            //为了避免不停设置控件位置造成的界面刷新,所以移动过程中采用画图
            if (movePrintLine)
            {
                if (printLine.Visible)
                {
                    printLine.Visible = false;
                }
                printLine.Top += e.Y - startY;

                if (printLine.Top < 10)
                {
                    printLine.Top = 10;
                }
                if (printLine.Top > printControl.Height - 10)
                {
                    printLine.Top = printControl.Height - 10;
                }

                printControl.Refresh();
                Graphics g = printControl.CreateGraphics();
                Rectangle rec = printLine.Bounds;
                g.FillRectangle(Brushes.Red, rec);

            }
        }

        /// <summary>
        /// 移动续打线_鼠标释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printLine_MouseUp(object sender, MouseEventArgs e)
        {
            //移动续打线
            if (movePrintLine)
            {
                printLine.Visible = true;
                printControl.Refresh();
            }

            movePrintLine = false;
        }

        /// <summary>
        /// 移动续打线_鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printLine_MouseDown(object sender, MouseEventArgs e)
        {
            movePrintLine = true;
            startY = e.Y;
        }
        #endregion

        /// <summary>
        /// 打印前初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (lstPrintImg == null || lstPrintImg.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            PrintPageList.Clear();
            int space = spaceOrg * printControl.Screendpi.Y / 100;
            switch (printType)
            {
                case EnumPrintType.续打:
                    {
                        //总页面高度(已去掉因缩放比例过小造成的空白区域)
                        int totlaHeight = 0;
                        totlaHeight = (printLine.Top + printLine.Height - printControl.FirstTop + space);
                        int intPageCount = lstPrintImg.Count;
                        int intTemp = 0;

                        for (int intI = 0; intI < intPageCount; intI++)
                        {
                            intTemp += (int)(GetPageHeight(intI) * printControl.Zoom + space);
                            if (intTemp > totlaHeight)
                            {
                                if (this.PrintPageType == 0)
                                {
                                    PrintPageList.Add(intI);
                                }
                                else if (this.PrintPageType == 1)
                                {
                                    if ((intI + 1) % 2 != 0)
                                    {
                                        PrintPageList.Add(intI);
                                    }
                                }
                                else if (this.PrintPageType == 2)
                                {
                                    if ((intI + 1) % 2 == 0)
                                    {
                                        PrintPageList.Add(intI);
                                    }
                                }
                                else if (this.PrintPageType == 3)
                                {
                                    if (this.PrintPageScope.IndexOf(intI.ToString()) >= 0)
                                    {
                                        PrintPageList.Add(intI);
                                    }
                                }
                            }
                        }

                        break;
                    }
                case EnumPrintType.套打:
                    {
                        //总页面高度(已去掉因缩放比例过小造成的空白区域)
                        int totlaHeight = 0;
                        totlaHeight = (printBox.Top - printControl.FirstTop + space);
                        int intPageCount = lstPrintImg.Count;
                        int intTemp = 0;

                        for (int intI = 0; intI < intPageCount; intI++)
                        {
                            intTemp += (int)(GetPageHeight(intI) * printControl.Zoom + space);
                            if (intTemp > totlaHeight)
                            {
                                if (intI > lstPrintImg.Count)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    if (this.PrintPageType == 0)
                                    {
                                        PrintPageList.Add(intI);
                                    }
                                    else if (this.PrintPageType == 1)
                                    {
                                        if ((intI + 1) % 2 != 0)
                                        {
                                            PrintPageList.Add(intI);
                                        }
                                    }
                                    else if (this.PrintPageType == 2)
                                    {
                                        if ((intI + 1) % 2 == 0)
                                        {
                                            PrintPageList.Add(intI);
                                        }
                                    }
                                    else if (this.PrintPageType == 3)
                                    {
                                        if (this.PrintPageScope.IndexOf(intI.ToString()) >= 0)
                                        {
                                            PrintPageList.Add(intI);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                default:
                    {
                        for (int i = 0; i < lstPrintImg.Count; i++)
                        {
                            if (this.PrintPageType == 0)
                            {
                                PrintPageList.Add(i);
                            }
                            else if (this.PrintPageType == 1)
                            {
                                if ((i + 1) % 2 != 0)
                                {
                                    PrintPageList.Add(i);
                                }
                            }
                            else if (this.PrintPageType == 2)
                            {
                                if ((i + 1) % 2 == 0)
                                {
                                    PrintPageList.Add(i);
                                }
                            }
                            else if (this.PrintPageType == 3)
                            {
                                if (this.PrintPageScope.IndexOf(i.ToString()) >= 0)
                                {
                                    PrintPageList.Add(i);
                                }
                            }
                        }
                        break;
                    }
            }

            printControl.Rows = lstPrintImg.Count;
            intPrintIndex = 0;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //绘制页面开始_转换坐标尺寸
            e.Graphics.PageUnit = GraphicsUnit.Document;
            e.Graphics.ResetTransform();
            //绘制页面
            if ((printType == EnumPrintType.续打 || printType == EnumPrintType.套打) && lstPrintNoLineImg != null && lstPrintNoLineImg.Count > 0)
            {
                e.Graphics.DrawImage(lstPrintNoLineImg[PrintPageList[intPrintIndex]], new Point(0, 0));
            }
            else
            {
                e.Graphics.DrawImage(lstPrintImg[PrintPageList[intPrintIndex]], new Point(0, 0));
            }
            //绘制页面结束_恢复坐标尺寸以便继打和套打
            e.Graphics.PageUnit = GraphicsUnit.Display;
            e.Graphics.ResetTransform();

            //续打和套打
            int space = spaceOrg * printControl.Screendpi.Y / 100;
            //每页在当前界面显示的像素
            int pageHeight = (int)(GetPageHeight(PrintPageList[intPrintIndex]) * printControl.Zoom) + space;

            //装订线
            //总页面高度(已去掉因缩放比例过小造成的空白区域)
            int totlaHeight = (printLine.Top + printLine.Height - printControl.FirstTop + space);
            //当前页
            int pageIndex = totlaHeight / pageHeight;
            //当前页继打或套打的起始Y坐标
            double pageStartY = totlaHeight % pageHeight - space - 1;

            Rectangle rec = new Rectangle(0, 0, e.PageBounds.Width, (int)GetLength(pageStartY / printControl.Zoom));
            if (intPrintIndex == 0)
            {
                switch (printType)
                {
                    case EnumPrintType.续打:
                        {
                            //总页面高度(已去掉因缩放比例过小造成的空白区域)
                            totlaHeight = (printLine.Top + printLine.Height - printControl.FirstTop + space);
                            //当前页
                            pageIndex = totlaHeight / pageHeight;
                            //当前页继打或套打的起始Y坐标
                            pageStartY = totlaHeight % pageHeight - space - 1;

                            rec = new Rectangle(0, 0, e.PageBounds.Width, (int)GetLength(pageStartY / printControl.Zoom));
                            e.Graphics.FillRectangle(Brushes.White, rec);
                            break;
                        }
                    case EnumPrintType.套打:
                        {
                            //总页面高度(已去掉因缩放比例过小造成的空白区域)
                            totlaHeight = (printBox.Top - printControl.FirstTop + space);
                            //当前页
                            pageIndex = totlaHeight / pageHeight;
                            //当前页继打或套打的起始Y坐标_补Panel的边框
                            pageStartY = totlaHeight % pageHeight - space - 1;
                            //当前页套打的起始X坐标
                            int pageStartX = (int)(printBox.Left - printControl.FirstLeft);

                            int offSet = 2;

                            Rectangle rec1 = new Rectangle(0, 0, e.PageBounds.Width, (int)GetLength(pageStartY / printControl.Zoom) + offSet);
                            e.Graphics.FillRectangle(Brushes.White, rec1);
                            Rectangle rec2 = new Rectangle(0, (int)GetLength((double)(pageStartY + printBox.Height) / printControl.Zoom) + offSet,
                                e.PageBounds.Width,
                                (int)GetLength((double)(GetPageHeight(PrintPageList[intPrintIndex])
                                - pageStartX - printBox.Height) / printControl.Zoom) - offSet);
                            e.Graphics.FillRectangle(Brushes.White, rec2);
                            Rectangle rec3 = new Rectangle(0, 0, (int)GetLength(pageStartX / printControl.Zoom) - 9 * offSet, e.PageBounds.Height);
                            e.Graphics.FillRectangle(Brushes.White, rec3);
                            Rectangle rec4 = new Rectangle((int)GetLength((double)(pageStartX + printBox.Width) / printControl.Zoom) - 6 * offSet,
                                0, (int)GetLength((double)(GetPageWidth() - pageStartX - printBox.Width) / printControl.Zoom) - offSet,
                                e.PageBounds.Height);
                            e.Graphics.FillRectangle(Brushes.White, rec4);
                            break;
                        }
                    default: break;
                }
            }

            //判断是否打印完所有打印页
            intPrintIndex++;
            if (intPrintIndex < PrintPageList.Count)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        /// <summary>
        /// 页面在预览控件的像素
        /// </summary>
        /// <returns></returns>
        private double GetPageHeight(int p_intPageIndex)
        {
            float fltHeight = 0;
            fltHeight = pageSize.Height;

            return (double)(fltHeight * printControl.Screendpi.Y) / 100;
        }

        /// <summary>
        /// 页面在预览控件的像素
        /// </summary>
        /// <returns></returns>
        private double GetPageWidth()
        {
            float fltWidth = 0;
            fltWidth = pageSize.Width;

            return (double)(fltWidth * printControl.Screendpi.X) / 100;
        }

        /// <summary>
        /// 取得长度
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        private double GetLength(double len)
        {
            return (double)(len * 100) / printControl.Screendpi.X;
        }

        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrintDocument_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            txtPageNum.Caption = " / " + lstPrintImg.Count.ToString() + " 页";
            if (lstPrintImg.Count > 0)
            {
                GoToPage(1);
            }
            barScale.EditValue = 100;
            printDocument.DefaultPageSettings.Margins = new Margins();
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", pageSize.Width, pageSize.Height);
            blnFirstLoad = false;
        }

        /// <summary>
        /// 在预览控件发生重绘时取得当前滚动条坐标,判断是否修改当前页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printControl_OnScroll(object sender, EventArgs e)
        {
            this.vScrollBar.Maximum = printControl.maxHeight - 800;
            this.vScrollBar.Value = printControl.GetScrollPosY();
        }

        /// <summary>
        /// 移动报表或滚轮移动时,设置当前页
        /// </summary>
        private void SetPage()
        {
            if (blnFirstLoad == false)
            {
                int yPosition = printControl.GetScrollPosY();

                int space = spaceOrg * printControl.Screendpi.Y / 100;

                int totlaHeight = -printControl.FirstTop + space;
                int intPageCount = lstPrintImg.Count;
                int intTemp = 0;

                for (int intI = 0; intI < intPageCount; intI++)
                {
                    intTemp += (int)(GetPageHeight(intI) * printControl.Zoom + space);
                    if (intTemp > totlaHeight)
                    {
                        viewIndex = intI + 1;
                        break;
                    }
                }



                if (txtPageIndex.EditValue.ToString() != viewIndex.ToString())
                {
                    inGoToPage = true;

                    txtPageIndex.EditValue = viewIndex.ToString();

                    if (viewIndex <= 1)
                    {
                        btnPageUp.Enabled = false;
                        btnFirstPage.Enabled = false;
                    }
                    else
                    {
                        btnPageUp.Enabled = true;
                        btnFirstPage.Enabled = true;
                    }

                    if (viewIndex == lstPrintImg.Count)
                    {
                        btnPageDown.Enabled = false;
                        btnLastPage.Enabled = false;
                    }
                    else
                    {
                        btnPageDown.Enabled = true;
                        btnLastPage.Enabled = true;
                    }

                    inGoToPage = false;
                }

                if (printBox.Visible)
                {
                    //printBox.Refresh();
                }
            }
        }

        /// <summary>
        /// 特殊边距
        /// </summary>
        private bool m_blnSpecMargins = false;

        /// <summary>
        /// 改变报表边距
        /// </summary>
        /// <param name="xr"></param>
        /// <param name="p_blnStatus"></param>
        private void m_mthChangeXRMargins(XtraReport xr, bool p_blnStatus)
        {
            if (xr == null) return;
            if (p_blnStatus)
            {
                xr.Margins.Top = 30;
                xr.Margins.Bottom = 70;
            }
            else
            {
                xr.Margins.Top = 100;
                xr.Margins.Bottom = 0;
            }
            xr.CreateDocument();
            this.m_blnSpecMargins = p_blnStatus;
        }

        private void SetPageSize()
        {
            string PaperKind = "A4"; //纸张类型
            bool PageLandscape = false;//判断是否需要翻转
            printDocument.DefaultPageSettings.Landscape = PageLandscape;

            int PageSizeWidth = 0;
            int PageSizeHeight = 0;
            if (PageLandscape)//如果是横向把纸张的长宽颠倒
            {
                PageSizeWidth = pageSize.Height;
                PageSizeHeight = pageSize.Width;
            }
            else
            {
                PageSizeWidth = pageSize.Width;
                PageSizeHeight = pageSize.Height;
            }

            //然后重置一下真正的纸张尺寸
            printDocument.DefaultPageSettings.PaperSize =
                        new System.Drawing.Printing.PaperSize(
                            "A4",
                            pageSize.Width, pageSize.Height);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                frmPrintPageSetting frmSetting = new frmPrintPageSetting();
                frmSetting.BlnPreformatted = PrintType == EnumPrintType.套打;
                if (frmSetting.ShowDialog() == DialogResult.OK)
                {
                    printDocument.PrinterSettings.PrinterName = frmSetting.PrinterName;
                    this.PrintPageType = frmSetting.PrintPageType;
                    this.PrintPageScope = frmSetting.PrintPageScope;
                    int intStartIndex = frmSetting.PrintStartIndex;
                    //Kim 设置真正的打印尺寸
                    SetPageSize();
                }
                else
                {
                    return;
                }

                printDocument.Print();

                if (frmSetting.PrintPageCopies > 1)
                {
                    for (int i = 0; i < frmSetting.PrintPageCopies - 1; i++)
                    {
                        printDocument.Print();
                    }
                }
                frmSetting = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 设置由于CreateDocument会去掉表头信息（病程）
        /// </summary>
        /// <param name="p_xrReport"></param>
        /// <param name="p_dtHeadeMsg"></param>
        /// <param name="p_intType"></param>
        private void m_mthSetHeadeMsg(XtraReport p_xrReport, ref DataTable p_dtHeadeMsg, int p_intType)
        {
            DataRow drTemp = null;
            bool blnFlag = false;
            Page page = null;
            LabelBrick labelBrick = null;

            if (p_intType == 0)
            {
                p_dtHeadeMsg = new DataTable();
                p_dtHeadeMsg.Columns.Add("patientname");
                p_dtHeadeMsg.Columns.Add("sex");
                p_dtHeadeMsg.Columns.Add("age");
                p_dtHeadeMsg.Columns.Add("deptname");
                p_dtHeadeMsg.Columns.Add("areaname");
                p_dtHeadeMsg.Columns.Add("bedno");
                p_dtHeadeMsg.Columns.Add("patientipno");
                p_dtHeadeMsg.Columns.Add("pageindex");

                for (int i = 0; i < p_xrReport.Pages.Count; i++)
                {
                    page = p_xrReport.Pages[i];
                    blnFlag = false;
                    drTemp = p_dtHeadeMsg.NewRow();

                    if (page.InnerBricks.Count() == 1 && page.InnerBricks[0] is CompositeBrick)
                    {
                        foreach (BrickBase brickBase in ((CompositeBrick)page.InnerBricks[0]).InnerBricks)
                        {
                            if (brickBase is LabelBrick)
                            {
                                labelBrick = brickBase as LabelBrick;
                                if (string.IsNullOrEmpty(labelBrick.BookmarkInfo.Bookmark) == false)
                                {
                                    switch (labelBrick.BookmarkInfo.Bookmark)
                                    {
                                        case "病人姓名":
                                            {
                                                blnFlag = true;
                                                drTemp["patientname"] = labelBrick.Text;
                                                break;
                                            }
                                        case "性别":
                                            {

                                                blnFlag = true;
                                                drTemp["sex"] = labelBrick.Text;
                                                break;
                                            }
                                        case "年龄":
                                            {

                                                blnFlag = true;
                                                drTemp["age"] = labelBrick.Text;
                                                break;
                                            }
                                        case "科室名称":
                                            {

                                                blnFlag = true;
                                                drTemp["deptname"] = labelBrick.Text;
                                                break;
                                            }
                                        case "病区名称":
                                            {
                                                blnFlag = true;
                                                drTemp["areaname"] = labelBrick.Text;
                                                break;
                                            }
                                        case "床号":
                                            {
                                                blnFlag = true;
                                                drTemp["bedno"] = labelBrick.Text;
                                                break;
                                            }
                                        case "住院号":
                                            {

                                                blnFlag = true;
                                                drTemp["patientipno"] = labelBrick.Text;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }

                    if (blnFlag)
                    {
                        drTemp["pageindex"] = i;
                        p_dtHeadeMsg.Rows.Add(drTemp);
                    }
                }
            }
            else
            {
                int intPageIndex = -1;
                for (int i = 0; i < p_dtHeadeMsg.Rows.Count; i++)
                {
                    blnFlag = false;
                    drTemp = p_dtHeadeMsg.Rows[i];
                    intPageIndex = Convert.ToInt32(drTemp["pageindex"]);
                    if (p_xrReport.Pages.Count < intPageIndex)
                        break;
                    page = p_xrReport.Pages[intPageIndex];

                    if (page.InnerBricks.Count() == 1 && page.InnerBricks[0] is CompositeBrick)
                    {
                        foreach (BrickBase brickBase in ((CompositeBrick)page.InnerBricks[0]).InnerBricks)
                        {
                            if (brickBase is LabelBrick)
                            {
                                labelBrick = brickBase as LabelBrick;
                                if (string.IsNullOrEmpty(labelBrick.BookmarkInfo.Bookmark) == false && string.IsNullOrEmpty(labelBrick.Text))
                                {
                                    switch (labelBrick.BookmarkInfo.Bookmark)
                                    {
                                        case "病人姓名":
                                            {
                                                labelBrick.Text = drTemp["patientname"].ToString();
                                                break;
                                            }
                                        case "性别":
                                            {
                                                labelBrick.Text = drTemp["sex"].ToString();
                                                break;
                                            }
                                        case "年龄":
                                            {

                                                labelBrick.Text = drTemp["age"].ToString();
                                                break;
                                            }
                                        case "科室名称":
                                            {
                                                labelBrick.Text = drTemp["deptname"].ToString();
                                                break;
                                            }
                                        case "病区名称":
                                            {
                                                labelBrick.Text = drTemp["areaname"].ToString();
                                                break;
                                            }
                                        case "床号":
                                            {
                                                labelBrick.Text = drTemp["bedno"].ToString();
                                                break;
                                            }
                                        case "住院号":
                                            {

                                                labelBrick.Text = drTemp["patientipno"].ToString();
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 设置续打线宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printControl_SizeChanged(object sender, EventArgs e)
        {
            printLine.Width = printControl.Width;
        }

        /// <summary>
        /// 续打设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PrintType == EnumPrintType.续打)
            {
                PrintType = EnumPrintType.正常;
            }
            else
            {
                PrintType = EnumPrintType.续打;
            }
        }

        /// <summary>
        /// 鼠标移动报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (movePrintLine == false && e.Button == MouseButtons.Left)
            {
                if (printType != EnumPrintType.套打)
                {
                    //正常打印或续打时用鼠标拖动浏览报表
                    if (printControl.Cursor != Cursors.Hand)
                    {
                        printControl.Cursor = Cursors.Hand;
                    }

                    int xPosition = printControl.GetScrollPosX() - e.X + startX;
                    int yPosition = printControl.GetScrollPosY() - e.Y + startY;

                    startY = e.Y;
                    startX = e.X;

                    printControl.SetPositionNoInvalidate(new Point(xPosition, yPosition));
                    needSetPage = true;
                    this.vScrollBar.Value = (int)yPosition;
                    needSetPage = true;
                }
                else
                {
                    //套打时画框
                    if (printBox.Visible)
                    {
                        printBox.Visible = false;
                    }
                    printBox.Width = e.X - printBox.Left;
                    printBox.Height = e.Y - printBox.Top;

                    printControl.Refresh();
                    Graphics g = printControl.CreateGraphics();
                    Rectangle rec = printBox.Bounds;
                    g.DrawRectangle(Pens.Red, rec);
                }
            }
            else
            {
                if (printControl.Cursor != Cursors.Arrow)
                {
                    printControl.Cursor = Cursors.Arrow;
                }
            }
        }

        /// <summary>
        /// 打印结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printControl.Focus();
        }

        /// <summary>
        /// 记录鼠标移动报表参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printControl_MouseDown(object sender, MouseEventArgs e)
        {
            startX = e.X;
            startY = e.Y;

            printBox.Size = new Size(0, 0);
            printBox.Visible = false;

            if (printType == EnumPrintType.套打)
            {
                printBox.Left = e.X;
                printBox.Top = e.Y;
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(1);
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(lstPrintImg.Count);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPageUp_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(viewIndex - 1);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPageDown_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(viewIndex + 1);
        }

        bool needSetPage = true;

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            printControl.OnSetPage -= new EventHandler(printControl_OnSetPage);
            printControl.SetPositionNoInvalidate(new Point(printControl.GetScrollPosX(), vScrollBar.Value));
            if (needSetPage)
            {
                SetPage();
            }
        }
        
        /// <summary>
        /// 转到第几页
        /// </summary>
        /// <param name="pageIndex"></param>
        private void GoToPage(int pageIndex)
        {
            viewIndex = pageIndex;
            inGoToPage = true;

            if (!(viewIndex > lstPrintImg.Count) && !(viewIndex < 1 && lstPrintImg.Count > 0))
            {
                int space = spaceOrg * printControl.Screendpi.Y / 100;
                double yPosition = 0;

                int intPageCount = lstPrintImg.Count;
                int intTemp = viewIndex - 1;

                for (int intI = 0; intI < intTemp; intI++)
                {
                    yPosition += (GetPageHeight(intI) * printControl.Zoom + space);
                }
                needSetPage = false;
                this.vScrollBar.Value = (int)yPosition;
                needSetPage = true;
            }
            else
            {
                if (viewIndex > lstPrintImg.Count)
                {
                    viewIndex = lstPrintImg.Count;
                }
                if (viewIndex < 1 && lstPrintImg.Count > 0)
                {
                    viewIndex = 1;
                }
            }

            txtPageIndex.EditValue = viewIndex.ToString();
            if (viewIndex <= 1)
            {
                btnPageUp.Enabled = false;
                btnFirstPage.Enabled = false;
            }
            else
            {
                btnPageUp.Enabled = true;
                btnFirstPage.Enabled = true;
            }

            if (viewIndex == lstPrintImg.Count)
            {
                btnPageDown.Enabled = false;
                btnLastPage.Enabled = false;
            }
            else
            {
                btnPageDown.Enabled = true;
                btnLastPage.Enabled = true;
            }

            if (printBox.Visible)
            {
                //printBox.Refresh();
            }

            inGoToPage = false;
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageIndex_Leave(object sender, EventArgs e)
        {
            GoToPageByTxt();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageIndex_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoToPageByTxt();
            }
        }

        /// <summary>
        /// 翻页
        /// </summary>
        private void GoToPageByTxt()
        {
            if (inGoToPage == false)
            {
                try
                {
                    int pageIndex = int.Parse(txtPageIndex.EditValue.ToString());
                    GoToPage(pageIndex);
                }
                catch
                {
                    txtPageIndex.EditValue = viewIndex.ToString();
                }
            }
        }

        /// <summary>
        /// 套打
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PrintType == EnumPrintType.套打)
            {
                PrintType = EnumPrintType.正常;
            }
            else
            {
                PrintType = EnumPrintType.套打;
            }
        }

        /// <summary>
        /// 鼠标拖动报表后也应设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (printType == EnumPrintType.套打)
            {
                printBox.Visible = true;

                printControl.Refresh();
            }

        }

        /// <summary>
        /// 窗体大小改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrintDocument_SizeChanged(object sender, EventArgs e)
        {
            //窗体改变时重置一些界面状态
            PrintType = printType;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "导出Pdf (*.pdf)|*.pdf";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strFileName = saveFile.FileName;
                    //if (DialogBox.Msg("导出成功,是否立刻打开文档?", MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    Process process = new Process();
                    //    process.StartInfo.FileName = saveFile.FileName;
                    //    process.Start();
                    //}
                }
                catch
                {
                    DialogBox.Msg("导出失败,请检查目标文件是否正在使用");
                }
            }
        }

        /// <summary>
        /// 设置缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barScale_EditValueChanged(object sender, EventArgs e)
        {
            //不使用工具条内嵌的DEV控件的原因:内嵌该类型控件焦点存在问题,需要失去焦点后才触发EditValueChanged事件
            if (barScale.EditValue != null && barScale.EditValue.ToString() != "" && blnFirstLoad == false)
            {
                printControl.Zoom = double.Parse(barScale.EditValue.ToString()) / 100f;
                labScale.Caption = barScale.EditValue.ToString() + "%";
            }
        }

        /// <summary>
        /// 移开barScale焦点,使鼠标滚轮对预览控件有效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barScale_MouseLeave(object sender, EventArgs e)
        {
            printControl.Focus();
        }

        private void printControl_OnSetPage(object sender, EventArgs e)
        {
            GoToPage(intInitPageIndex);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.ParentForm == null)
                {
                    this.Close();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void m_mthShow()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = 900;
            this.ShowDialog();
        }

        private void printDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
        }

        private void ResetPanelBackColor()
        {
            printBox.Visible = false;
            timer.Interval = 200;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            printBox.BackColor = Color.FromArgb(80, Color.Green);
            printBox.Visible = true;
            printBox.Refresh();
        }
        /// <summary>
        /// 画装订线
        /// </summary>
        private void DrawSplitLine(ref Graphics p_gPainter, Point p_pntStart, Point p_pntEnd)
        {
            Pen p = new Pen(Color.Black);
            p.Width = 2f;
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Point pMid = new Point((p_pntStart.X + p_pntEnd.X) / 2, (p_pntStart.Y + p_pntEnd.Y) / 2);
            Point pMLeft = new Point((p_pntStart.X + pMid.X) / 2, (p_pntStart.Y + pMid.Y) / 2);
            Point pMRight = new Point((p_pntEnd.X + pMid.X) / 2, (p_pntEnd.Y + pMid.Y) / 2);

            p_gPainter.DrawString("装", new Font("宋体", 10.5F), Brushes.Black, pMLeft - new Size(8, 8));
            p_gPainter.DrawString("订", new Font("宋体", 10.5F), Brushes.Black, pMid - new Size(8, 8));
            p_gPainter.DrawString("线", new Font("宋体", 10.5F), Brushes.Black, pMRight - new Size(8, 8));

            p_gPainter.DrawLine(p, p_pntStart, p_pntEnd);
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
