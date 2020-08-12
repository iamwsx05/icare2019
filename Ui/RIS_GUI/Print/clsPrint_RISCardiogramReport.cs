using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISCardiogramReport 的摘要说明。
	/// </summary>
	public class clsPrint_RISCardiogramReport:com.digitalwave.GUI_Base.clsController_Base,infPrintRecord
	{
		private long m_lngWidthPage;//打印页的宽度
		private long m_lngY;//当前Y方向坐标

		private float m_fltLeftIndentProp;//左缩进比例
		private float m_fltRightIndentProp;//右缩进比例

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;
		private Font m_fntMiddleNotBold;

        // 2011.5.6
        private float m_intY = 0;
        private float m_intLeftX = 0;
        private float m_intX = 0;

        private Font objFontHospitalTitle = null;
        private Font objFontNormal = null;
        private Font objFontNormaler = null;
        private Font objFontBold = null;
        private Font objFontBoldReportTitle = null;
        string m_strTemp = "";
        // ************************** 2011.5.6 *******************************************

        /// <summary>
        /// 2011.8.12 茶山报告页眉
        /// </summary>
        public Image log;

		public clsRIS_CardiogramReport_VO objReportVO;

		/// 边框画笔
		/// </summary>
		private Pen m_GridPen;

		#region 构造函数
		public clsPrint_RISCardiogramReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion

		#region 打印报告起始部分
		/// <summary>
		/// 打印报告起始部分
		/// </summary>
		/// <param name="p_objPrintArg"></param>
        private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs e)
		{
			m_lngWidthPage=e.PageBounds.Width;
			m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            #region new 2011.5.6
            m_intY = e.PageBounds.Height * 0.03f;
            m_intLeftX = e.PageBounds.Width * 0.045f;
            m_intX = e.PageBounds.Width * 0.1f;
            int m_intHeight = 400;
            string m_strTemp = string.Empty;
            try
            {

                //e.Graphics.DrawImage(objImage, m_intX + 20, m_intY);
                //m_intY += objImage.Height - 25;
            }
            catch
            {
            }

            // 2011.8.12
            Image imgLog = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\Picture\茶山log.bmp");
            e.Graphics.DrawImage(imgLog, new RectangleF(20, 20, 200, 35));
            //if (log != null)
                
            // 2011.5.6
            SizeF m_objSizef = e.Graphics.MeasureString("东莞市茶山医院", objFontHospitalTitle);
            m_intY += 10;

            m_objSizef = e.Graphics.MeasureString("心  电  图  报  告  单", objFontHospitalTitle);//objFontBoldReportTitle);
            e.Graphics.DrawString("心电图报告单", objFontHospitalTitle, Brushes.Black, (e.PageBounds.Width - m_objSizef.Width) / 2 + 60, m_intY);
            m_intY += 20;

            m_intY += 23;
            e.Graphics.DrawLine(Pens.Black, m_intLeftX, m_intY, e.PageBounds.Width * 0.93f, m_intY);
            m_intY += 10;
            e.Graphics.DrawString("姓名:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.045f, m_intY);
            m_objSizef = e.Graphics.MeasureString("姓名:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.045f + m_objSizef.Width + 2, m_intY);

            e.Graphics.DrawString("性别:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.31f, m_intY);
            m_objSizef = e.Graphics.MeasureString("性别:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strSEX_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.31f + m_objSizef.Width + 2, m_intY);

            string strAge = objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ", "");
            e.Graphics.DrawString("年龄:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);
            m_objSizef = e.Graphics.MeasureString("年龄:", objFontNormal);
            e.Graphics.DrawString(strAge, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);

            e.Graphics.DrawString("心电图号:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.65f + 50, m_intY);
            m_objSizef = e.Graphics.MeasureString("心电图号:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.65f + m_objSizef.Width + 60, m_intY);

            m_intY += 30;
            e.Graphics.DrawString("科室:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.045f, m_intY);
            m_objSizef = e.Graphics.MeasureString("科室:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.045f + m_objSizef.Width, m_intY);

            e.Graphics.DrawString("床号:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.31f, m_intY);//e.PageBounds.Width * 0.51f, m_intY);
            m_objSizef = e.Graphics.MeasureString("床号:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strBED_NO_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.31f + m_objSizef.Width, m_intY);//e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);

            // 2011.8.12
            //e.Graphics.DrawString("住院号/诊疗卡号:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);//e.PageBounds.Width * 0.65f, m_intY);
            //m_objSizef = e.Graphics.MeasureString("住院号/诊疗卡号:", objFontNormal);

            // 2011.5.6
            if (!string.IsNullOrEmpty(objReportVO.m_strINPATIENT_NO_CHR))
            {
                // 2011.8.12
                e.Graphics.DrawString("住院号:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);//e.PageBounds.Width * 0.65f, m_intY);
                m_objSizef = e.Graphics.MeasureString("住院号:", objFontNormal);
                e.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);// e.PageBounds.Width * 0.65f + m_objSizef.Width, m_intY);
            }
            else if (!string.IsNullOrEmpty(objReportVO.m_strCARD_ID_CHR))
            {
                // 2011.8.12
                e.Graphics.DrawString("诊疗卡号:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);//e.PageBounds.Width * 0.65f, m_intY);
                m_objSizef = e.Graphics.MeasureString("诊疗卡号:", objFontNormal);
                e.Graphics.DrawString(objReportVO.m_strCARD_ID_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);// e.PageBounds.Width * 0.65f + m_objSizef.Width, m_intY);
            }

            m_intY += 23;
            e.Graphics.DrawLine(Pens.Black, m_intLeftX, m_intY + 2, e.PageBounds.Width * 0.93f, m_intY + 2);
            m_intY += 30;
            #endregion

            #region
            //打印标题
//            string m_strTitle = this.m_objComInfo.m_strGetHospitalTitle();
//            SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
//            float fltCurrentX = m_lngWidthPage*(float)1.5/3-(long)szTitle.Width/2;//标题文本左上角的X轴坐标
//            m_lngY = 60;//标题文本左上角Y轴坐标
//            p_objPrintArg.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

//            m_lngY += (int)szTitle.Height + 10;
//            string m_strSubTitle = "心  电  图  报  告  单";
//            fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2-szTitle.Width/9;
////			m_lngY += 20;
//            p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);
//            //打印标题右边部分
//            SizeF szContent = p_objPrintArg.Graphics.MeasureString("心电图号",m_fntSmallNotBold);
//            float fltRightX = m_lngWidthPage*(float)2.87/4;
//            long lngRightY = 60;
//            p_objPrintArg.Graphics.DrawString("心电图号",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

//            //门 诊 号
//            szContent = p_objPrintArg.Graphics.MeasureString("门 诊 号",m_fntSmallNotBold);
//            fltRightX = m_lngWidthPage*(float)2.87/4;
//            lngRightY += 10 + (int)szContent.Height;
//            p_objPrintArg.Graphics.DrawString("门 诊 号",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

//            //住 院 号
//            szContent = p_objPrintArg.Graphics.MeasureString("住 院 号",m_fntSmallNotBold);
//            fltRightX = m_lngWidthPage*(float)2.87/4;
//            lngRightY += 10 + (int)szContent.Height;
//            p_objPrintArg.Graphics.DrawString("住 院 号",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

//            //姓名
//            m_lngY = lngRightY + 20 + (int)szContent.Height;
//            szContent = p_objPrintArg.Graphics.MeasureString("姓名",m_fntSmallNotBold);
//            fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//            p_objPrintArg.Graphics.DrawString("姓名",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+120,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=120;
//            //性别
////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/5);
//            szContent = p_objPrintArg.Graphics.MeasureString("性别",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("性别",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+45,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=50;
//            //年龄
////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
//            szContent = p_objPrintArg.Graphics.MeasureString("年龄",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("年龄",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width;
//            string strAge=objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ","");
//            p_objPrintArg.Graphics.DrawString(strAge,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+125,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX += 125;
//            //检查日期
//            DateTime CheckDat = DateTime.Parse(objReportVO.m_strCHECK_DAT);

////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.75/4);
//            szContent = p_objPrintArg.Graphics.MeasureString("检查日期",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("检查日期",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(CheckDat.ToString("yyyy年MM月dd日 HH:mm"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+165,m_fntSmallNotBold.Height+m_lngY);

//            //科别
//            m_lngY +=  10 + (int)szContent.Height;
//            szContent = p_objPrintArg.Graphics.MeasureString("科室",m_fntSmallNotBold);
//            fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//            p_objPrintArg.Graphics.DrawString("科室",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, m_fntSmallNotBold, Brushes.Black, new RectangleF(fltCurrentX, m_lngY, 140F, szContent.Height*2));
//            //p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+120F,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=120;
//            //床号
//            //			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
//            szContent = p_objPrintArg.Graphics.MeasureString("床号",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("床号",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+45,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=50;
//            //病室或门诊
////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
//            szContent = p_objPrintArg.Graphics.MeasureString("申请医生",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("申请医生",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strApplyDoctorName,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+70,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=70;
			
//            //报告日期
//            DateTime ReportDat = DateTime.Parse(objReportVO.m_strREPORT_DAT);

////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.75/4);
//            szContent = p_objPrintArg.Graphics.MeasureString("报告日期",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("报告日期",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(ReportDat.ToString("yyyy年MM月dd日 HH:mm"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //画线
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+165,m_fntSmallNotBold.Height+m_lngY);
            #endregion
        }
		#endregion

		#region 打印报告正文部分
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
            // 2011.5.6
            m_lngY = (long)m_intY - 40;

			SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("三",m_fntSmallNotBold);//获取一个字符的宽度

			SizeF szContent = p_objPrintArg.Graphics.MeasureString("心电图所见一：",m_fntSmallBold);
			m_lngY += 20 + (int)szContent.Height;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("心电图所见一：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			m_lngY += 10 + (int)szContent.Height;

			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+40;
//			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("节律：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("节律：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strRHYTHM_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3)+60;
            szContent = p_objPrintArg.Graphics.MeasureString("心房率：", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("心房率：", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
			fltCurrentX += szContent.Width;

            szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strHEART_ROOM_VCHR, m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHEART_ROOM_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width ;
			p_objPrintArg.Graphics.DrawString("次/分",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*2/3)+60;
            szContent = p_objPrintArg.Graphics.MeasureString("心室率：", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("心室率：", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
			fltCurrentX += szContent.Width ;
            szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strHEART_RATE_VCHR, m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strHEART_RATE_VCHR.Trim(), m_fntSmallNotBold, Brushes.Black, fltCurrentX - 10, m_lngY);
			fltCurrentX += szContent.Width ;
			p_objPrintArg.Graphics.DrawString("次/分",m_fntSmallNotBold,Brushes.Black,fltCurrentX-13,m_lngY);
            szContent = p_objPrintArg.Graphics.MeasureString("心室率：", m_fntSmallNotBold);

			m_lngY +=  10 + (int)szContent.Height;

//			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+40;
			szContent = p_objPrintArg.Graphics.MeasureString("P-R",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("P-R",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY-2);
			fltCurrentX += szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("间期：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("间期：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strP_R_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX-10,m_lngY);
			szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strP_R_VCHR.Trim(),m_fntSmallNotBold);
			fltCurrentX += szContent.Width ;
			p_objPrintArg.Graphics.DrawString("秒",m_fntSmallNotBold,Brushes.Black,fltCurrentX-15,m_lngY);
//			fltCurrentX+=30;
			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3)+60;
			szContent = p_objPrintArg.Graphics.MeasureString("QRS",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("QRS",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY-2);
			fltCurrentX += szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("时限：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时限：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strQRS_VCHR,m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQRS_VCHR.Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX-5,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString("秒",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			fltCurrentX+=30;
			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*2/3)+60;
			szContent = p_objPrintArg.Graphics.MeasureString("Q-T：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("Q-T：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY-2);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQ_T_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString("秒",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

            m_lngY += 10 + (int)szContent.Height;
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp+40;
            szContent = p_objPrintArg.Graphics.MeasureString("电轴：", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("电轴：", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY - 2);
            fltCurrentX += szContent.Width;
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strE_Axes_vchr, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);

			m_lngY += 10 + (int)szContent.Height; 

			long CurrentY = m_lngY + 4;
			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			if(strSummary1 != null)
			{
                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+40;// +szPerWord.Width * 2;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;

				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,600-(int)CurrentY);

//				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY1_VCHR ,m_fntMiddleNotBold ,Brushes.Black,rectSummary1);
//				int intRealHeight=0;
//				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold );
//				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
//				objPrint.m_blnPrintInBlock(m_fntMiddleNotBold.FontFamily.Name,m_fntMiddleNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight,false,false);
new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR,m_fntMiddleNotBold,Color.Black,rectSummary1,p_objPrintArg.Graphics);


				long lngEndY = CurrentY + m_fntMiddleNotBold.Height * 5 + 3;
//				p_objPrintArg.Graphics.DrawString(strSummary1,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
			m_lngY += m_fntSmallNotBold.Height * 5 + 4;

			//心电图诊断及见解
			m_lngY += (int)szContent.Height + 10;
			if(m_lngY<680) m_lngY=680;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("心电图诊断及见解：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("心电图诊断及见解：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height + 10;

			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
			CurrentY = m_lngY;
			if(strSummary2 != null)
			{
                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+40;// +szContent.Width;
				//float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+szContent.Width;//m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width*2;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;

//				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
//				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold );
//				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
//				
//				objPrint.m_blnPrintInBlock(m_fntMiddleNotBold.FontFamily.Name,m_fntMiddleNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);
new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);

//				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY2_VCHR ,m_fntSmallNotBold ,Brushes.Black,rectSummary2);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
				//p_objPrintArg.Graphics.DrawString(strSummary2,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
			m_lngY += m_fntMiddleNotBold.Height * 5 + 4;
		}
		#endregion

		#region 打印报告结尾部分
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs e)
        {
            // 2011.5.6
            m_intY = e.PageBounds.Height * 0.85f;
            e.Graphics.DrawString("报告日期:" + objReportVO.m_strREPORT_DAT, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.06f, m_intY + 40);

            SizeF m_objSizef = e.Graphics.MeasureString("报告日期:   " + objReportVO.m_strREPORT_DAT, objFontNormal);//objReportVO.m_strCHECK_DAT, objFontNormal);
            e.Graphics.DrawString("报告医师:" + objReportVO.m_strREPORTOR_NAME_VCHR, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.15f + m_objSizef.Width + 2, m_intY + 40);

            m_objSizef = e.Graphics.MeasureString("报告医师:" + objReportVO.m_strREPORTOR_NAME_VCHR, objFontNormal);
            e.Graphics.DrawString("审核医师:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.45f + m_objSizef.Width + 40, m_intY + 40);

            m_intY = e.PageBounds.Height * 0.9f;
            e.Graphics.DrawLine(Pens.Black, m_intLeftX, m_intY, e.PageBounds.Width * 0.93f, m_intY);
            m_intY += 15;
            // 2011.5.6 
            //m_strTemp = " 祝您身体健康！ 此报告单仅供临床参考！" + "                           " + "检查医师:" + m_objBultraSoundVo.m_strCheckDoctor;
            m_strTemp = "祝您身体健康！ 此报告单仅供临床参考！";
            e.Graphics.DrawString(m_strTemp, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.06f, m_intY);
            m_intY += 20;

            m_strTemp = "第 1 " + "页/共 1 " + "页";
            e.Graphics.DrawString(m_strTemp, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.45f, m_intY);

            #region old
            //m_lngY = p_objPrintArg.PageBounds.Height-95;
            //float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
            //SizeF szContent = p_objPrintArg.Graphics.MeasureString("报告者：",m_fntSmallNotBold);
            //p_objPrintArg.Graphics.DrawString("报告者：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            //fltCurrentX += szContent.Width + 5;
            //p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            ////fltCurrentX += 40;
            //m_lngY += 40;
            //fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)3.6f/4;
            //szContent = p_objPrintArg.Graphics.MeasureString("第",m_fntSmallNotBold);
            //p_objPrintArg.Graphics.DrawString("第 1 ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            //fltCurrentX += (long)szContent.Width+20;
            //p_objPrintArg.Graphics.DrawString("页",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            #endregion
        }
		#endregion

		private void m_mthPrintPageSub(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_mthPrintStart(p_objPrintArg);
			m_mthPrintMiddle(p_objPrintArg);
			m_mthPrintEnd(p_objPrintArg);
		}

		#region 实现接口
		/// <summary>
		/// 从数据库初始化打印内容。如果没有记录，打印空报表。
		/// </summary>
		public void m_mthInitPrintContent()
		{
		}
		
		/// <summary>
		/// 初始化打印变量
		/// </summary>
		/// <param name="p_objArg">外部需要初始化的变量，根据不同的实现使用</param>
		public void m_mthInitPrintTool(object p_objArg)
		{
			m_fntTitle= new Font("SimSun", 18,FontStyle.Bold);
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			m_fntSmallNotBold=new Font("SimSun",12f,FontStyle.Regular);
			m_fntMiddleNotBold=new Font("SimSun",14,FontStyle.Regular);

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.06f;
			m_fltRightIndentProp=0.1f;

            // 2011.5.6
            objFontBoldReportTitle = new Font("宋体", 20, FontStyle.Bold);
            objFontHospitalTitle = new Font("楷体_GB2312", 25);
            objFontNormal = new Font("宋体", 12);
            objFontBold = new Font("宋体", 12, FontStyle.Bold);
            objFontNormaler = new Font("宋体", 11, FontStyle.Bold);
		}

		/// <summary>
		/// 释放打印变量
		/// </summary>
		/// <param name="p_objArg">外部使用到的变量，根据不同的实现使用</param>
		public void m_mthDisposePrintTools(object p_objArg)
		{
		}

		/// <summary>
		/// 打印开始
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{
		}

		/// <summary>
		/// 打印中
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthPrintPage(object p_objPrintArg)
		{
			m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// 打印结束。一般使用它来更新数据库信息。
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthEndPrint(object p_objPrintArg)
		{
		}
		#endregion
	}
}
