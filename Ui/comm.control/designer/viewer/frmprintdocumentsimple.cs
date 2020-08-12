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
using System.IO;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 打印预览(简单)
    /// </summary>
    public partial class frmPrintDocumentSimple : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 初始化
        /// </summary>
        public frmPrintDocumentSimple(XtraReport _xrReport)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.xrReport = _xrReport;
                printDocument.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize(_xrReport.PaperName, _xrReport.PageSize.Width, _xrReport.PageSize.Height);
                printDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
                GenerateBmp();               
            }
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 文件路径
        /// </summary>
        public string fileDir { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// 打印源
        /// </summary>
        public XtraReport xrReport { get; set; }

        /// <summary>
        /// 续打线
        /// </summary>
        private Panel printLine = new Panel();

        int n = 0;
        int cPrt = 0;
        bool isPrint { get; set; }
        List<Image> lstImage = new List<Image>();

        #endregion

        #region 方法

        #region printDocument_BeginPrint
        /// <summary>  
        /// 3、得到打印內容  
        /// 每个打印任务只调用OnBeginPrint()一次。  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int startNo = 2;
            string[] files = System.IO.Directory.GetFiles(this.fileDir, "*.bmp");
            lstImage.Clear();
            for (int i = 0; i < files.Length; i++)
            {
                if (i < StartPageNo1 - 1) continue;
                Image org = Image.FromFile(this.fileDir + "\\" + this.fileName + Convert.ToString(i + 1) + ".bmp");
                lstImage.Add(AcquireRectangleImage(org, new Rectangle(1, 1, org.Width - 2, org.Height - 2)));
            }
            n = 0;

            printDocument.PrinterSettings.PrintRange = PrintRange.SomePages;
            printDocument.PrinterSettings.FromPage = startNo;
            txtPageNum.Caption = "/ " + this.lstImage.Count.ToString() + "页";
        }


        /// <summary>
        /// 截取图像的矩形区域
        /// </summary>
        /// <param name="source">源图像对应picturebox1</param>
        /// <param name="rect">矩形区域，如上初始化的rect</param>
        /// <returns>矩形区域的图像</returns>
        public Image AcquireRectangleImage(Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty) return null;
            Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb); //.Format32bppRgb); // 改变像素可能会提高打印清晰度
            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {
                grSmall.DrawImage(source,
                                  new System.Drawing.Rectangle(0, 0, bmSmall.Width, bmSmall.Height),
                                  rect,
                                  GraphicsUnit.Pixel);
                grSmall.Dispose();
            }
            return bmSmall;
        }
        #endregion

        #region OnPrintPage
        /// <summary>  
        /// 4、绘制多个打印界面  
        /// printDocument的PrintPage事件  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            int leftMargin = Convert.ToInt32((e.MarginBounds.Left) * 3 / 4);　 //左边距  
            int topMargin = Convert.ToInt32(e.MarginBounds.Top * 2 / 3);　　　 //顶边距  

            //一下涉及剪切图片,  
            int width = e.MarginBounds.Width;// image.Width;
            int height = e.MarginBounds.Height;// image.Height;
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(topMargin, leftMargin, e.MarginBounds.Width, e.MarginBounds.Height);

            if (n == lstImage.Count) return;
            if (n < lstImage.Count)
            {
                width = lstImage[n].Width;
                height = lstImage[n].Height;
                destRect = new Rectangle(0, 0, lstImage[n].Width, lstImage[n].Height);
                e.HasMorePages = true;
                e.Graphics.DrawImage(lstImage[n], destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                printControl.Rows += 1;


            }
            n++;

            /*********************************************/

            if (isPrint && cPrt == 0)
            {
                //续打和套打
                int space = spaceOrg * printControl.Screendpi.Y / 100;

                //每页在当前界面显示的像素
                int pageHeight = (int)(GetPageHeight() * printControl.Zoom) + space;

                //总页面高度(已去掉因缩放比例过小造成的空白区域)
                int totlaHeight = (printLine.Top + printLine.Height - printControl.FirstTop + space);
                ////当前页
                //int pageIndex = totlaHeight / pageHeight;
                //当前页继打或套打的起始Y坐标
                double pageStartY = totlaHeight % pageHeight - space - 1;

                Rectangle rec = new Rectangle(0, 0, e.PageBounds.Width, (int)GetLength(pageStartY / printControl.Zoom));
                e.Graphics.FillRectangle(Brushes.White, rec);

                cPrt++;
            }
            /********************************************/

            if (n == lstImage.Count)
            {
                printControl.Rows += 1;
                e.HasMorePages = false;
            }
        }
        #endregion

        #region 打印页

        /// <summary>
        /// 需要打印的页序列
        /// </summary>
        public List<int> PrintPageList { get; set; }

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
        /// 页面在预览控件的像素
        /// </summary>
        /// <returns></returns>
        private double GetPageHeight()
        {
            float fltHeight = 0;

            fltHeight = printDocument.DefaultPageSettings.PrintableArea.Height;

            return (double)(fltHeight * printControl.Screendpi.Y) / 100;
        }

        /// <summary>
        /// 预览时页间宽度
        /// </summary>
        private int spaceOrg = 10;

        #endregion

        #region 续打线

        /// <summary>
        /// 是否移动续打线标志位
        /// </summary>
        private bool movePrintLine = false;

        /// <summary>
        /// Y方向移动
        /// </summary>
        private int startY = 0;

        /// <summary>
        /// X方向移动
        /// </summary>
        private int startX = 0;

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

        /// <summary>
        /// 移动续打线_鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void printLine_MouseMove(object sender, MouseEventArgs e)
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
        void printLine_MouseUp(object sender, MouseEventArgs e)
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
        void printLine_MouseDown(object sender, MouseEventArgs e)
        {
            movePrintLine = true;
            startY = e.Y;
        }
        #endregion

        #region 获取续打开始页

        int StartPageNo1 = 0;

        int StartPageNo()
        {
            int space = spaceOrg * printControl.Screendpi.Y / 100;

            //总页面高度(已去掉因缩放比例过小造成的空白区域)
            int totlaHeight = (printLine.Top + printLine.Height - printControl.FirstTop + space);

            //每页在当前界面显示的像素
            int pageHeight = (int)(GetPageHeight() * printControl.Zoom) + space;

            ////当前页数
            int pageIndex = totlaHeight / pageHeight;

            return pageIndex + 1;
        }

        #endregion

        #region GenerateBmp
        /// <summary>
        /// GenerateBmp
        /// </summary>
        void GenerateBmp()
        {
            try
            {
                uiHelper.BeginLoading(this);
                string dir = Common.Entity.GlobalParm.PrintFileDir;
                if (Directory.Exists(dir))
                {
                    Function.DeleteFolder(dir);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                }
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                dirInfo.Attributes = FileAttributes.Normal;

                string path = dir + "\\" + Common.Entity.GlobalParm.PrintFileName + ".bmp";
                DevExpress.XtraPrinting.ImageExportOptions opt = new DevExpress.XtraPrinting.ImageExportOptions();
                opt.ExportMode = DevExpress.XtraPrinting.ImageExportMode.DifferentFiles;
                opt.Format = System.Drawing.Imaging.ImageFormat.Bmp;
                this.xrReport.ExportToImage(path, opt);
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region parm

        Size pageSize = new Size(850, 1170);
        public Size PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        bool needSetPage = true;

        /// <summary>
        /// 当前显示的页
        /// </summary>
        int viewIndex = 0;

        /// <summary>
        /// 是否正在执行翻页
        /// </summary>
        bool inGoToPage = false;

        /// <summary>
        /// 初始时页码
        /// </summary>
        int intInitPageIndex = 1;

        public int InitPageIndex
        {
            set { intInitPageIndex = value; }
        }

        #endregion

        #region GetPageHeight
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
        #endregion

        #region GetPageWidth
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
        #endregion

        #region vScrollBar_ValueChanged
        /// <summary>
        /// vScrollBar_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 设置续打线宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printControl_SizeChanged(object sender, EventArgs e)
        {
            printLine.Width = printControl.Width;
        }

        private void printControl_OnSetPage(object sender, EventArgs e)
        {
            GoToPage(intInitPageIndex);
        }

        #endregion

        #region GoToPage
        /// <summary>
        /// 转到第几页
        /// </summary>
        /// <param name="pageIndex"></param>
        private void GoToPage(int pageIndex)
        {
            viewIndex = pageIndex;
            inGoToPage = true;

            if (!(viewIndex > lstImage.Count) && !(viewIndex < 1 && lstImage.Count > 0))
            {
                int space = spaceOrg * printControl.Screendpi.Y / 100;
                double yPosition = 0;

                int intPageCount = lstImage.Count;
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
                if (viewIndex > lstImage.Count)
                {
                    viewIndex = lstImage.Count;
                }
                if (viewIndex < 1 && lstImage.Count > 0)
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

            if (viewIndex == lstImage.Count)
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
        #endregion

        #region SetPage
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
                int intPageCount = lstImage.Count;
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

                    if (viewIndex == lstImage.Count)
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
            }
        }
        #endregion

        #region GoToPageByTxt 翻页
        /// <summary>
        /// 防止第一次加载窗体时部分事件重复触发
        /// </summary>
        bool blnFirstLoad = true;

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
        #endregion

        #endregion

        #region 事件

        private void frmPrintDocumentSimple_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fileDir)) this.fileDir = Common.Entity.GlobalParm.PrintFileDir;
            if (string.IsNullOrEmpty(this.fileName)) this.fileName = Common.Entity.GlobalParm.PrintFileName;
            InitializePrintLine();
        }

        private void btnFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(1);
        }

        private void btnPageUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(viewIndex - 1);
        }

        private void txtPageIndex_EditValueChanged(object sender, EventArgs e)
        {
            GoToPageByTxt();
        }

        private void btnPageDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(viewIndex + 1);
        }

        private void btnLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToPage(lstImage.Count);
        }

        private void btnContinue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            printLine.Visible = !printLine.Visible;
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (printLine.Visible)
            {
                StartPageNo1 = StartPageNo();   //2;
                cPrt = 0;
                isPrint = true;
                printDocument.Print();
                isPrint = false;
                StartPageNo1 = 0;
            }
            else
            {
                #region 打印设置

                //frmPrintPageSetting frmSetting = new frmPrintPageSetting();
                //frmSetting.BlnPreformatted = false;
                //if (frmSetting.ShowDialog() == DialogResult.OK)
                //{
                //    xrReport.PrinterName = frmSetting.PrinterName;
                //    switch (frmSetting.PrintPageType)
                //    {
                //        case 0:
                //            // 全部                            
                //            break;
                //        case 1:
                //            // 奇数
                //            for (int i = 0; i < xrReport.Pages.Count; i++)
                //            {
                //                if (i <= frmSetting.PrintStartIndex)
                //                {
                //                    xrReport.Pages[i].Printed = true;
                //                }
                //                else
                //                {
                //                    xrReport.Pages[i].Printed = (((i + 1) % 2 != 0) ? false : true);
                //                }
                //            }
                //            break;
                //        case 2:
                //            // 偶数
                //            for (int i = 0; i < xrReport.Pages.Count; i++)
                //            {
                //                if (i <= frmSetting.PrintStartIndex)
                //                {
                //                    xrReport.Pages[i].Printed = true;
                //                }
                //                else
                //                {
                //                    xrReport.Pages[i].Printed = (((i + 1) % 2 == 0) ? false : true);
                //                }
                //            }
                //            break;
                //        case 3:
                //            // 指定页
                //            for (int i = 0; i < xrReport.Pages.Count; i++)
                //            {
                //                if (i <= frmSetting.PrintStartIndex)
                //                {
                //                    xrReport.Pages[i].Printed = true;
                //                }
                //                else
                //                {
                //                    xrReport.Pages[i].Printed = ((frmSetting.PrintPageScope.IndexOf(i.ToString()) >= 0) ? false : true);
                //                }
                //            }
                //            break;
                //        default:
                //            break;
                //    }
                //    for (int i = 0; i < frmSetting.PrintPageCopies; i++)
                //    {
                //        xrReport.Print();
                //    }
                //}
                #endregion

                xrReport.PrintDialog();
            }
        }

        private void barScale_EditValueChanged(object sender, EventArgs e)
        {
            //不使用工具条内嵌的DEV控件的原因:内嵌该类型控件焦点存在问题,需要失去焦点后才触发EditValueChanged事件
            if (barScale.EditValue != null && barScale.EditValue.ToString() != "")// && blnFirstLoad == false)
            {
                printControl.Zoom = double.Parse(barScale.EditValue.ToString()) / 100f;
                labScale.Caption = "  " + barScale.EditValue.ToString() + "%";
            }
        }

        private void bbiPrinter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
            printDialog.ShowDialog();
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            uiHelper.Export(this.xrReport);
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
