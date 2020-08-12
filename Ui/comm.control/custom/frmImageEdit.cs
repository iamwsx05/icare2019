using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using DevExpress.Utils;

namespace Common.Controls
{
    public partial class frmImageEdit : System.Windows.Forms.Form
    {
        public frmImageEdit(Image p_imgCurrentEdit)
        {
            InitializeComponent();
            this.CurrentEditImage = p_imgCurrentEdit;
        }

        /// <summary>
        /// 当前编辑的图片
        /// </summary>
        public Image CurrentEditImage { get; set; }

        /// <summary>
        /// 当前编辑的图片
        /// </summary>
        private Image imgCurrentEdit = null;

        /// <summary>
        /// 画图控件信息
        /// </summary>
        private class PictureValue
        {
            /// <summary>
            /// 底图
            /// </summary>
            public Image imgBack = null;
            /// <summary>
            /// 画在底图上的图案
            /// </summary>
            public Image imgFront = null;
            /// <summary>
            /// 图片宽度
            /// </summary>
            public int intWidth = 0;
            /// <summary>
            /// 图片高度
            /// </summary>
            public int intHeight = 0;
        }

        private bool blnPaint = false;
        private bool blnPaintPoint = false;
        private int intX1= 0;
        private int intX2 = 0;
        private int intY1 = 0;
        private int intY2 = 0;
        private int intXDefault = 0;
        private int intYDefault = 0;
        private enum enmPicturePaintType
        {
            Pen = 0,
            Line,
            Round,
            SolidRound,
            Rectangle,
            SolidRect,
            Polygon,
            Rubber,
            Text,
            DashPen,
            DashRound,
            DashRect,
            None
        }
        private enum enmContainerPaintType
        {
            None,
            DrawBorder,
        }

        private enmContainerPaintType enumContainerPaintType = enmContainerPaintType.None;
        /// <summary>
        /// 画图类型
        /// </summary>
        private enmPicturePaintType enumPicPaintType = enmPicturePaintType.Pen;

        private Image imgDrawRectangle;
        private Image imgDrawRound;
        private Image imgDrawLine;
        private Image imgDrawSolidRound;
        private Image imgDrawSolidRectangle;
        private Image imgDrawDashPen;
        private Image imgFillImage;
        private Pen penDefault = new Pen(Color.Red);
        private Brush bruString = Brushes.Red;
        private Pen penDash;
        private GraphicsPath ghpSelectPath;

        private Region rgnContent;
        private TextureBrush txbContent;

        private PictureBox picSelected;

        private int intDrawBorderWidth = 4;
        private int intLastMouseX = 0;
        private int intLastMouseY = 0;
        private bool blnCanResize = false;
        private int intDefaultHeight = 0;

        private void ChangeCursorWhenMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (picSelected == null)
                return;

            Rectangle rtgOuter = picSelected.Bounds;
            rtgOuter.X -= intDrawBorderWidth;
            rtgOuter.Y -= intDrawBorderWidth;
            rtgOuter.Width += intDrawBorderWidth * 2;
            rtgOuter.Height += intDrawBorderWidth * 2;

            Rectangle rtgLeftTop = new Rectangle(rtgOuter.Left, rtgOuter.Top, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgMidTop = new Rectangle(rtgOuter.Left + rtgOuter.Width / 2 - intDrawBorderWidth / 2, rtgOuter.Top, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgRightTop = new Rectangle(rtgOuter.Right - intDrawBorderWidth, rtgOuter.Top, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgLeftMid = new Rectangle(rtgOuter.Left, rtgOuter.Top + rtgOuter.Height / 2 - intDrawBorderWidth / 2, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgRightMid = new Rectangle(rtgOuter.Right - intDrawBorderWidth, rtgOuter.Top + rtgOuter.Height / 2 - intDrawBorderWidth / 2, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgLeftBottom = new Rectangle(rtgOuter.Left, rtgOuter.Bottom - intDrawBorderWidth, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgMidBottom = new Rectangle(rtgOuter.Left + rtgOuter.Width / 2 - intDrawBorderWidth / 2, rtgOuter.Bottom - intDrawBorderWidth, intDrawBorderWidth, intDrawBorderWidth);
            Rectangle rtgRightBottom = new Rectangle(rtgOuter.Right - intDrawBorderWidth, rtgOuter.Bottom - intDrawBorderWidth, intDrawBorderWidth, intDrawBorderWidth);

            Point ptMouse = new Point(e.X, e.Y);

            if (rtgRightBottom.Contains(ptMouse))
            {
                this.Cursor = Cursors.SizeNWSE;
                blnCanResize = true;
                return;
            }
            else if (rtgRightMid.Contains(ptMouse))
            {
                this.Cursor = Cursors.SizeWE;
                blnCanResize = true;
                return;
            }
            else if (rtgMidBottom.Contains(ptMouse))
            {
                this.Cursor = Cursors.SizeNS;
                blnCanResize = true;
                return;
            }
        }

        private void ResizeControls(System.Windows.Forms.MouseEventArgs e)
        {
            int intHSpace = e.X - intLastMouseX;
            int intVSpace = e.Y - intLastMouseY;

            if (this.Cursor == Cursors.SizeNS)
                intHSpace = 0;
            else if (this.Cursor == Cursors.SizeWE)
                intVSpace = 0;

            if (picSelected.Width + intHSpace > 50)
                picSelected.Width += intHSpace;
            if (picSelected.Height + intVSpace > 50)
                picSelected.Height += intVSpace;

            intLastMouseX = e.X;
            intLastMouseY = e.Y;

            picSelected.Invalidate();
        }

        private void DrawSelectedPictureBoxBorder(Graphics g)
        {
            Pen pn = new Pen(Color.Gray, intDrawBorderWidth);
            Brush bs = new SolidBrush(Color.White);
            Rectangle rtgBounds = picSelected.Bounds;
            rtgBounds.X -= intDrawBorderWidth / 2;
            rtgBounds.Y -= intDrawBorderWidth / 2;
            rtgBounds.Width += intDrawBorderWidth;
            rtgBounds.Height += intDrawBorderWidth;
            g.DrawRectangle(pn, rtgBounds);

            Rectangle rtgOuter = new Rectangle();
            rtgOuter.X = rtgBounds.X - intDrawBorderWidth / 2;
            rtgOuter.Y = rtgBounds.Y - intDrawBorderWidth / 2;
            rtgOuter.Width += rtgBounds.Width + intDrawBorderWidth;
            rtgOuter.Height += rtgBounds.Height + intDrawBorderWidth;

            g.FillRectangle(bs, rtgOuter.Left, rtgOuter.Top, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Left + rtgOuter.Width / 2 - intDrawBorderWidth / 2, rtgOuter.Top, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Left + rtgOuter.Width - intDrawBorderWidth, rtgOuter.Top, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Left, rtgOuter.Top + rtgOuter.Height / 2 - intDrawBorderWidth / 2, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Right - intDrawBorderWidth, rtgOuter.Top + rtgOuter.Height / 2 - intDrawBorderWidth / 2, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Left, rtgOuter.Bottom - intDrawBorderWidth, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Left + rtgOuter.Width / 2 - intDrawBorderWidth / 2, rtgOuter.Bottom - intDrawBorderWidth, intDrawBorderWidth, intDrawBorderWidth);
            g.FillRectangle(bs, rtgOuter.Right - intDrawBorderWidth, rtgOuter.Bottom - intDrawBorderWidth, intDrawBorderWidth, intDrawBorderWidth);
        }

        private void ClearBorder()
        {
            enumContainerPaintType = enmContainerPaintType.None;
            this.xtraScrollableControl.Invalidate();
        }

        private void LoadImage()
        {
            Image img = this.CurrentEditImage;
            int intX = 3;// (this.xtraScrollableControl.Width - img.Width) / 2;
            int intY = 3;// (this.xtraScrollableControl.Height - img.Height) / 2;
            if (intX < 0) intX = 0;
            if (intY < 0) intY = 0;

            PictureBox objPicBox = new PictureBox();
            objPicBox.Location = new Point(intX, intY);
            objPicBox.Image = img;
            objPicBox.Size = new Size(img.Width, img.Height);
            objPicBox.BackColor = Color.White;
            objPicBox.BorderStyle = BorderStyle.None;

            objPicBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
            objPicBox.MouseMove += new MouseEventHandler(PictureBox_MouseMove);
            objPicBox.MouseUp += new MouseEventHandler(PictureBox_MouseUp);
            objPicBox.Paint += new PaintEventHandler(PictureBox_Paint);
            objPicBox.Resize += new EventHandler(PictureBox_Resize);

            PictureValue voPicture = new PictureValue();
            voPicture.imgBack = img;
            voPicture.intWidth = img.Width;
            voPicture.intHeight = img.Height;

            objPicBox.Tag = voPicture;
            objPicBox.Invalidate();

            this.enumPicPaintType = enmPicturePaintType.None;
            this.xtraScrollableControl.Controls.Clear();
            this.xtraScrollableControl.Controls.Add(objPicBox);

            Graphics g = Graphics.FromImage(objPicBox.Image);
            SolidBrush sbRubber = new SolidBrush(Color.White);
            g.FillRectangle(sbRubber, objPicBox.Image.Width - 2, 0, 2, objPicBox.Image.Height);
            g.FillRectangle(sbRubber, 0, objPicBox.Image.Height - 2, objPicBox.Image.Width, 2);
            g = null;

            this.penDash = new Pen(Color.Black);
            this.penDash.DashPattern = new float[] { 3.0f, 3.0f };
            this.ghpSelectPath = new GraphicsPath();
            this.imgFillImage = new Bitmap(32, 32);
            this.txbContent = new TextureBrush(imgFillImage);
        }

        private void PictureBox_Resize(object sender, EventArgs e)
        {
            PictureBox ctl1 = (PictureBox)sender;
            PictureValue objPic = (PictureValue)(ctl1.Tag);
            objPic.intWidth = ctl1.ClientRectangle.Width;
            objPic.intHeight = ctl1.ClientRectangle.Height;
            ctl1.Invalidate();
        }

        private void PictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PictureValue objPic = (PictureValue)(((PictureBox)sender).Tag);
            SelectPictureBox((PictureBox)sender);
            blnPaintPoint = true;

            if (enumPicPaintType != enmPicturePaintType.Polygon)
            {
                intX1 = e.X;
                intY1 = e.Y;
            }

            //一开始鼠标未按下时
            if (intX1 == 0 && intY1 == 0)
            {
                intX1 = e.X;
                intY1 = e.Y;

                intXDefault = intX1;
                intYDefault = intY1;
            }
            HandleTextInput(sender, e);
        }

        /// <summary>
        /// 处理文字输入
        /// </summary>
        /// <param name="sender"></param>
        private void HandleTextInput(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (enumPicPaintType == enmPicturePaintType.Text)
            {
                PictureBox picContainer = (PictureBox)sender;
                if (rtfText.Visible)
                {
                    if (rtfText.Text != "")
                    {
                        PictureValue objPic = (PictureValue)picContainer.Tag;
                        Image imgFront;
                        Graphics g;
                        if (objPic.imgFront == null)
                            imgFront = new Bitmap(objPic.intWidth, objPic.intHeight);
                        else
                            imgFront = objPic.imgFront;
                        g = Graphics.FromImage(imgFront);
                        Rectangle rtg = new Rectangle(rtfText.Location, new Size(rtfText.Width, rtfText.Height));
                        g.DrawString(rtfText.Text, rtfText.Font, this.bruString, rtg);                        
                        g.Dispose();
                        objPic.imgFront = imgFront;
                        picContainer.Tag = objPic;
                        rtfText.Text = string.Empty;
                        picContainer.Invalidate();
                    }
                    rtfText.ImeMode = ImeMode.Close;
                    rtfText.Visible = false;
                }
                else
                {
                    picContainer.Controls.Add(rtfText);
                    rtfText.Location = new Point(e.X, e.Y);
                    rtfText.Width = picContainer.Width - rtfText.Left - 10;
                    rtfText.Height = (this.intDefaultHeight < picContainer.Height - rtfText.Top) ? this.intDefaultHeight : (picContainer.Height - rtfText.Top);
                    rtfText.BackColor = Color.Black;
                    rtfText.ForeColor = Color.White;
                    rtfText.Visible = true;
                    rtfText.ImeMode = ImeMode.OnHalf;
                    rtfText.Focus();
                }
            }
            else//没有选中文字工具时
            {
                if (rtfText.Visible)
                {
                    rtfText.ImeMode = ImeMode.Close;
                    rtfText.Visible = false;
                }
            }
        }

        private void PictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            blnPaint = true;
            blnPaintPoint = false;
            intX2 = e.X;
            intY2 = e.Y;

            PictureValue objPic = (PictureValue)((Control)sender).Tag;
            Image imgFront;
            Graphics g;

            if (objPic.imgFront == null)
                imgFront = new Bitmap(objPic.intWidth, objPic.intHeight);
            else
                imgFront = objPic.imgFront;
            g = Graphics.FromImage(imgFront);
            if (objPic.imgFront != null)
                g.DrawImage(objPic.imgFront, 0, 0);
            switch (enumPicPaintType)
            {
                case enmPicturePaintType.Pen:
                    {
                        SolidBrush sb = new SolidBrush(penDefault.Color);
                        g.FillRectangle(sb, intX1, intY1, 1, 1);
                    }
                    break;
                case enmPicturePaintType.Line:
                    g.DrawLine(penDefault, intX1, intY1, intX2, intY2);
                    break;
                case enmPicturePaintType.Round:
                    g.DrawEllipse(penDefault, intX1, intY1, intX2 - intX1, intY2 - intY1);
                    break;
                case enmPicturePaintType.Rectangle:
                    {
                        Rectangle rtg = GetRectangle(intX1, intX2, intY1, intY2); ;
                        g.DrawRectangle(penDefault, rtg);
                    }
                    break;
                case enmPicturePaintType.Rubber:
                    {
                        SolidBrush sb = new SolidBrush(Color.White);
                        g.FillRectangle(sb, intX1 - 2, intY1 - 2, 4, 4);
                    }
                    break;
                case enmPicturePaintType.Polygon:
                    g.DrawLine(penDefault, intX1, intY1, intX2, intY2);
                    intX1 = intX2;
                    intY1 = intY2;

                    if (intX2 == intXDefault && intY2 == intYDefault)
                    {
                        intX1 = 0;
                        intY1 = 0;
                    }
                    break;
                case enmPicturePaintType.SolidRound:
                    {
                        SolidBrush sb = new SolidBrush(penDefault.Color);
                        g.FillEllipse(sb, intX1, intY1, intX2 - intX1, intY2 - intY1);
                    }
                    break;
                case enmPicturePaintType.SolidRect:
                    {
                        SolidBrush sb = new SolidBrush(penDefault.Color);
                        g.FillRectangle(sb, intX1, intY1, intX2 - intX1, intY2 - intY1);
                    }
                    break;
                case enmPicturePaintType.DashPen:
                    imgDrawDashPen = null;
                    FillImage(g);
                    break;
                case enmPicturePaintType.DashRect:
                    {
                        Rectangle rtg = GetRectangle(intX1, intX2, intY1, intY2); ;
                        ghpSelectPath.Reset();
                        ghpSelectPath.AddRectangle(rtg);
                        FillImage(g);
                    }
                    break;
                case enmPicturePaintType.DashRound:
                    ghpSelectPath.Reset();
                    ghpSelectPath.AddEllipse(intX1, intY1, intX2 - intX1, intY2 - intY1);
                    FillImage(g);
                    break;
            }
            g.Dispose();
            objPic.imgFront = imgFront;
            ((Control)sender).Tag = objPic;
            ((Control)sender).Invalidate();
        }

        private void FillImage(Graphics p_gphSender)
        {
            ghpSelectPath.CloseAllFigures();
            rgnContent = new Region(ghpSelectPath);
            p_gphSender.FillRegion(txbContent, rgnContent);
            ghpSelectPath.Reset();
        }

        private Rectangle GetRectangle(int p_intX1, int p_intX2, int p_intY1, int p_intY2)
        {
            if ((p_intX2 - p_intX1) < 0 && (intY2 - p_intY1) < 0)
                return new Rectangle(intX2, intY2, Math.Abs(p_intX2 - p_intX1), Math.Abs(p_intY2 - p_intY1));
            else if ((p_intX2 - p_intX1) < 0)
                return new Rectangle(intX2, intY1, Math.Abs(p_intX2 - p_intX1), Math.Abs(p_intY2 - p_intY1));
            else if ((p_intY2 - p_intY1) < 0)
                return new Rectangle(intX1, intY2, Math.Abs(p_intX2 - p_intX1), Math.Abs(p_intY2 - p_intY1));
            else
                return new Rectangle(intX1, intY1, Math.Abs(p_intX2 - p_intX1), Math.Abs(p_intY2 - p_intY1));
        }

        private void PictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!blnPaint)
                return;

            PictureBox ctl1 = (PictureBox)sender;

            PictureValue objPic = (PictureValue)ctl1.Tag;

            if (objPic.imgBack != null)
            {
                e.Graphics.DrawImage(objPic.imgBack, 0, 0, ctl1.Width, ctl1.Height);
                objPic.intWidth = ctl1.ClientRectangle.Width;
            }

            if (objPic.imgFront != null)
            {
                e.Graphics.DrawImage(objPic.imgFront, ctl1.ClientRectangle);

            }

            //只是画矩形轨迹
            if (imgDrawRectangle != null)
            {
                e.Graphics.DrawImage(imgDrawRectangle, 0, 0, ctl1.Width, ctl1.Height);
                imgDrawRectangle = null;
            }
            //只是画圆轨迹
            if (imgDrawRound != null)
            {
                e.Graphics.DrawImage(imgDrawRound, 0, 0, ctl1.Width, ctl1.Height);
                imgDrawRound = null;
            }
            //只是线轨迹
            if (imgDrawLine != null)
            {
                e.Graphics.DrawImage(imgDrawLine, 0, 0, ctl1.Width, ctl1.Height);
                imgDrawLine = null;
            }
            //只是画实心圆轨迹
            if (imgDrawSolidRound != null)
            {
                e.Graphics.DrawImage(imgDrawSolidRound, 0, 0, ctl1.Width, ctl1.Height);
                imgDrawSolidRound = null;
            }
            //只是画实心矩形轨迹
            if (imgDrawSolidRectangle != null)
            {
                e.Graphics.DrawImage(imgDrawSolidRectangle, 0, 0, ctl1.Width, ctl1.Height);
                imgDrawSolidRectangle = null;
            }
            //只是画任意形状轨迹
            if (imgDrawDashPen != null)
            {
                e.Graphics.DrawImage(imgDrawDashPen, 0, 0, ctl1.Width, ctl1.Height);
                imgDrawDashPen = null;
            }
        }

        private void PictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            intX2 = e.X;
            intY2 = e.Y;

            if (blnPaintPoint)
            {
                PictureBox picContainer = (PictureBox)sender;
                PictureValue objPic = (PictureValue)picContainer.Tag;
                Image imgFront = new Bitmap(picContainer.Width, picContainer.Height);
                Graphics g = Graphics.FromImage(imgFront);
                SolidBrush sb = new SolidBrush(penDefault.Color);
                if (objPic.imgFront != null)
                    g.DrawImage(objPic.imgFront, 0, 0);

                switch (enumPicPaintType)
                {
                    case enmPicturePaintType.Pen:
                        g.DrawLine(penDefault, intX1, intY1, intX2, intY2);
                        intX1 = intX2;
                        intY1 = intY2;
                        break;
                    case enmPicturePaintType.Line:
                        penDefault.DashStyle = DashStyle.Dot;
                        imgDrawLine = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawLine);
                        g.DrawLine(penDefault, intX1, intY1, intX2, intY2);
                        penDefault.DashStyle = DashStyle.Solid;
                        break;
                    case enmPicturePaintType.Round:
                        penDefault.DashStyle = DashStyle.Dot;
                        imgDrawRound = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawRound);
                        g.DrawEllipse(penDefault, intX1, intY1, intX2 - intX1, intY2 - intY1);
                        penDefault.DashStyle = DashStyle.Solid;
                        break;
                    case enmPicturePaintType.Rectangle:
                        {
                            penDefault.DashStyle = DashStyle.Dot;
                            imgDrawRectangle = new Bitmap(picContainer.Width, picContainer.Height);
                            g = Graphics.FromImage(imgDrawRectangle);
                            Rectangle rtg = GetRectangle(intX1, intX2, intY1, intY2);
                            g.DrawRectangle(penDefault, rtg);
                            penDefault.DashStyle = DashStyle.Solid;
                        }
                        break;
                    case enmPicturePaintType.Rubber:
                        Color clrRubber = System.Drawing.Color.FromArgb(0, 100, 100, 100);//(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
                        SolidBrush sbRubber = new SolidBrush(Color.White);
                        g.FillRectangle(sbRubber, intX1 - 2, intY1 - 2, 4, 4);

                        intX1 = intX2;
                        intY1 = intY2;
                        break;
                    case enmPicturePaintType.Polygon:
                        penDefault.DashStyle = DashStyle.Dot;
                        imgDrawRound = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawRound);
                        g.DrawLine(penDefault, intX1, intY1, intX2, intY2);
                        penDefault.DashStyle = DashStyle.Solid;
                        break;
                    case enmPicturePaintType.SolidRound:
                        imgDrawSolidRound = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawSolidRound);
                        g.FillEllipse(sb, intX1, intY1, intX2 - intX1, intY2 - intY1);
                        break;
                    case enmPicturePaintType.SolidRect:
                        imgDrawSolidRectangle = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawSolidRectangle);
                        g.FillRectangle(sb, intX1, intY1, Math.Abs(intX2 - intX1), Math.Abs(intY2 - intY1));
                        break;
                    case enmPicturePaintType.DashPen:
                        Pen p = new Pen(Color.Black);
                        p.DashStyle = DashStyle.DashDot;
                        imgDrawDashPen = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawDashPen);
                        ghpSelectPath.AddLine(intX1, intY1, intX2, intY2);
                        g.DrawPath(p, ghpSelectPath);
                        intX1 = intX2;
                        intY1 = intY2;
                        break;
                    case enmPicturePaintType.DashRect:
                        {
                            imgDrawRectangle = new Bitmap(picContainer.Width, picContainer.Height);
                            g = Graphics.FromImage(imgDrawRectangle);
                            Rectangle rtg = GetRectangle(intX1, intX2, intY1, intY2);
                            g.DrawRectangle(penDash, rtg);
                        }
                        break;
                    case enmPicturePaintType.DashRound:
                        imgDrawRound = new Bitmap(picContainer.Width, picContainer.Height);
                        g = Graphics.FromImage(imgDrawRound);
                        g.DrawEllipse(penDash, intX1, intY1, intX2 - intX1, intY2 - intY1);
                        break;
                }

                g.Dispose();
                objPic.imgFront = imgFront;
                ((Control)sender).Tag = objPic;

                ((Control)sender).Invalidate();
            }
        }

        private void Save()
        {
            foreach (Control ctl in this.xtraScrollableControl.Controls)
            {
                if (ctl.GetType().Name.ToLower() == "picturebox")
                {
                    if (ctl.Tag != null)
                    {
                        PictureValue voPicture = ctl.Tag as PictureValue;
                        Image img = new Bitmap(ctl.Width, ctl.Height);
                        Graphics gdi = Graphics.FromImage(img);
                        SolidBrush brush = new SolidBrush(Color.White);
                        gdi.FillRectangle(brush, 0, 0, voPicture.intWidth, voPicture.intHeight);
                        if (voPicture.imgBack != null)
                        {
                            gdi.DrawImage(voPicture.imgBack, 0, 0, voPicture.intWidth, voPicture.intHeight);
                        }
                        if (voPicture.imgFront != null)
                        {
                            gdi.DrawImage(voPicture.imgFront, 0, 0, voPicture.intWidth, voPicture.intHeight);
                        }

                        this.CurrentEditImage = img;
                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                }
            }
        }

        private void SelectPictureBox(PictureBox p_picSender)
        {
            enumContainerPaintType = enmContainerPaintType.DrawBorder;
            picSelected = p_picSender;
            this.xtraScrollableControl.Invalidate();
        }

        private void frmImageEdit_Load(object sender, EventArgs e)
        {
            this.LoadImage();
            this.xtraScrollableControl.BackColor = Color.FromArgb(128, 128, 128);
            this.intDefaultHeight = rtfText.Height * 5;
        }

        private void xtraScrollableControl_MouseDown(object sender, MouseEventArgs e)
        {
            intLastMouseX = e.X;
            intLastMouseY = e.Y;
        }

        private void xtraScrollableControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                this.Cursor = Cursors.Default;
                blnCanResize = false;
                ChangeCursorWhenMove(e);
            }
            else
            {
                if (blnCanResize)
                {
                    ResizeControls(e);
                    this.xtraScrollableControl.Invalidate();
                }
            }
        }

        private void xtraScrollableControl_Paint(object sender, PaintEventArgs e)
        {
            switch (enumContainerPaintType)
            {
                case enmContainerPaintType.DrawBorder:
                    DrawSelectedPictureBoxBorder(e.Graphics);
                    break;
                default:
                    break;
            }
        }

        private void Function(DevExpress.XtraEditors.SimpleButton p_btnImage)
        {
            if (p_btnImage == null) return;
            this.picCurrentImage.Image = p_btnImage.Image;

            if (p_btnImage.Tag == null) return;
            switch (p_btnImage.Tag.ToString())
            {
                case "0":
                    this.enumPicPaintType = enmPicturePaintType.None;
                    break;
                case "1":
                    this.enumPicPaintType = enmPicturePaintType.Pen;
                    break;
                case "2":
                    this.enumPicPaintType = enmPicturePaintType.Line;
                    break;
                case "3":
                    this.enumPicPaintType = enmPicturePaintType.Rectangle;
                    break;
                case "4":
                    this.enumPicPaintType = enmPicturePaintType.Round;
                    break;
                case "5":
                    this.enumPicPaintType = enmPicturePaintType.Polygon;
                    break;
                case "6":
                    this.enumPicPaintType = enmPicturePaintType.Rubber;
                    break;
                case "7":
                    this.enumPicPaintType = enmPicturePaintType.Text;
                    break;
                case "8":
                    this.enumPicPaintType = enmPicturePaintType.DashRect;
                    break;
                case "9":
                    this.enumPicPaintType = enmPicturePaintType.DashRound;
                    break;
                case "10":
                    this.enumPicPaintType = enmPicturePaintType.DashPen;
                    break;
                case "11":
                    break;
                default:
                    break;
            }
        }

        private void Function(System.Windows.Forms.ToolStripMenuItem p_tsiMenuItem)
        {
            if (this.enumPicPaintType != enmPicturePaintType.DashPen && this.enumPicPaintType != enmPicturePaintType.DashRect &&
                this.enumPicPaintType != enmPicturePaintType.DashRound)
                return;

            if (this.imgFillImage == null) return;
            this.imgFillImage = p_tsiMenuItem.Image;
            this.btn12.Image = p_tsiMenuItem.Image;
            this.txbContent = new TextureBrush(this.imgFillImage);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Function(btn1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.Function(btn2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.Function(btn3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.Function(btn4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.Function(btn5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this.Function(btn6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this.Function(btn7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this.Function(btn8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this.Function(btn9);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            this.Function(btn10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            this.Function(btn11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show(btn12, new Point(btn12.Width, btn12.Height));
        }

        private void tsmi1_Click(object sender, EventArgs e)
        {
            this.Function(tsmi1);
        }

        private void tsmi2_Click(object sender, EventArgs e)
        {
            this.Function(tsmi2);
        }

        private void tsmi3_Click(object sender, EventArgs e)
        {
            this.Function(tsmi3);
        }

        private void tsmi4_Click(object sender, EventArgs e)
        {
            this.Function(tsmi4);
        }

        private void tsmi5_Click(object sender, EventArgs e)
        {
            this.Function(tsmi5);
        }

        private void tsmi6_Click(object sender, EventArgs e)
        {
            this.Function(tsmi6);
        }

        private void tsmi7_Click(object sender, EventArgs e)
        {
            this.Function(tsmi7);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmImageEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void lblColor_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                this.penDefault = new Pen(cdg.Color);
                this.bruString = new SolidBrush(cdg.Color);
                this.lblColor.BackColor = cdg.Color;
            }
        }
    }
}