using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISEEGReport 的摘要说明。
	/// </summary>
	public class clsPrint_RISEEGReport:infPrintRecord
	{
		private long m_lngWidthPage;//打印页的宽度
		private long m_lngY;//当前Y方向坐标

		private float m_fltLeftIndentProp;//左缩进比例
		private float m_fltRightIndentProp;//右缩进比例

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;

		public clsRIS_EEG_REPORT_VO objReportVO;

		/// 边框画笔
		/// </summary>
		private Pen m_GridPen;

		#region 构造函数
		
		public clsPrint_RISEEGReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		//说明:这里的标志是为了与医生工作站查看所有报告的模块兼容,因为在脑电脑这里用的是
		//自定义预览控件,它控制了打印中间部分. gphuang 2005-1-31 补上
		/// <summary>
		/// 标志
		/// </summary>
		private int flag=1;
		public clsPrint_RISEEGReport(int i)
		{
			flag =i;
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion

		private com.digitalwave.iCare.common.clsCommmonInfo m_objComm = new com.digitalwave.iCare.common.clsCommmonInfo();

		#region 打印报告起始部分
		/// <summary>
		/// 打印报告起始部分
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_lngWidthPage=p_objPrintArg.PageBounds.Width;
			m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			
			//打印标题
			string m_strTitle = m_objComm.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
			float fltCurrentX = m_lngWidthPage*(float)1.5/3-(long)szTitle.Width/2;//标题文本左上角的X轴坐标
			m_lngY = 68;//标题文本左上角Y轴坐标
			p_objPrintArg.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

			string m_strSubTitle = "脑电图报告单";
			fltCurrentX = m_lngWidthPage*(float)1.5/3-(long)szTitle.Width/2-szTitle.Width/9;
			m_lngY += 40;
			fltCurrentX += 80;
			p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntTitle,Brushes.Black,fltCurrentX-5,m_lngY);
			m_lngY += 60;
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("姓名：",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("姓名：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4),m_fntSmallNotBold.Height+m_lngY);

			//科别
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("科室：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("科室：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);

			//脑电图号
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+2*(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("脑电图号：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("脑电图号：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.8/4),m_fntSmallNotBold.Height+m_lngY);
			//性别
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("性别：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("性别：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			//床号
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("床号：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("床号：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+2*(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("门诊号：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("门诊号：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.8/4),m_fntSmallNotBold.Height+m_lngY);
			//年龄
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("年龄：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("年龄：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX = fltCurrentX+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(m_mthAgeChange(objReportVO.m_strAGE_FLT.Trim()),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			//左右利
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("左右利：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("左右利：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strLEFT_RIGHT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//住院号
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+2*(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("住院号：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("住院号：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.8/4),m_fntSmallNotBold.Height+m_lngY);
			//临床诊断
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("临床诊断：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("临床诊断：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			string strSummary2 = objReportVO.m_strDIAGNOSE_VCHR;
			long CurrentY = m_lngY;
			if(strSummary2 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;
//				CurrentY-=20;
//				fltRightX-=500;
				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strDIAGNOSE_VCHR,objReportVO.m_strDIAGNOSE_XML_VCHR,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
				//p_objPrintArg.Graphics.DrawString(strSummary2,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
//			p_objPrintArg.Graphics.DrawString(objReportVO.,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            //检查日期
			m_lngY += 40;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("检查日期：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("检查日期：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			DateTime dt =DateTime.Parse(objReportVO.m_strCHECK_DAT);
			fltCurrentX = szContent.Width+fltCurrentX;
			p_objPrintArg.Graphics.DrawString(dt.ToString("yyyy-MM-dd"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			//进餐前
//			m_lngY += 30;
//			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//			szContent = p_objPrintArg.Graphics.MeasureString("检查前：",m_fntSmallNotBold);
//			p_objPrintArg.Graphics.DrawString("检查前：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			string strBeforecheck="进餐后"+objReportVO.m_strBEFORE_CHECK+"小时";
//			p_objPrintArg.Graphics.DrawString(strBeforecheck,m_fntSmallNotBold,Brushes.Black,fltCurrentX+60,m_lngY);
			//检查时
			//
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("检查时：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("检查时：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 80;
			p_objPrintArg.Graphics.DrawString("患者体位：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            szContent = p_objPrintArg.Graphics.MeasureString("患者体位：",m_fntSmallNotBold);
			fltCurrentX =fltCurrentX +szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBODY_STAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX =m_lngWidthPage/2;
			p_objPrintArg.Graphics.DrawString("意识状态：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			szContent = p_objPrintArg.Graphics.MeasureString("意识状态：",m_fntSmallNotBold);
			fltCurrentX =fltCurrentX +szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSENSE_STAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+80;
			szContent = p_objPrintArg.Graphics.MeasureString("用药情况：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("用药情况：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX =fltCurrentX +szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDRUG_STAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);


		}
		#endregion

		#region 打印报告正文部分
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_lngY += 5;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("Imp:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("脑电图所见：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height + 10;

			long CurrentY = m_lngY + 4;
			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
			if(strSummary2 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX-50;
				CurrentY -= 15;
				fltRightX-=50;
				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR,objReportVO.m_strSUMMARY2_XML_VCHR,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
				CurrentY = lngEndY;
			}
			m_lngY += 200;

			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("Imp:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("印象：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height + 10;

			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			CurrentY = m_lngY;
			if(strSummary1 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;
				fltRightX-=50;
				int intRealHeight=0;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR,objReportVO.m_strSUMMARY1_XML_VCHR,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
//				p_objPrintArg.Graphics.DrawString(strSummary2,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
			m_lngY += m_fntSmallNotBold.Height * 5 + 4;
		}
		#endregion

		#region 打印报告结尾部分
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("报告医师：",m_fntSmallNotBold);
			m_lngY=900+(long)(szContent.Height*2);
//			m_lngY = p_objPrintArg.PageBounds.Height-200;
			float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
			p_objPrintArg.Graphics.DrawString("报告医师：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX+80,m_lngY);
			m_lngY+=20;
		    fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
		    szContent = p_objPrintArg.Graphics.MeasureString("报告日期：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("报告日期：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			DateTime dt =DateTime.Parse(objReportVO.m_strREPORT_DAT);
			p_objPrintArg.Graphics.DrawString(dt.ToString("yyyy-MM-dd"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//fltCurrentX += 40;
			m_lngY += 20;
			szContent = p_objPrintArg.Graphics.MeasureString("(此报告仅供本院医师参考)",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage/2 - szContent.Width/2;
			p_objPrintArg.Graphics.DrawString("(此报告仅供本院医师参考)",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)3.8f/4;
//			szContent = p_objPrintArg.Graphics.MeasureString("第",m_fntSmallNotBold);
//			p_objPrintArg.Graphics.DrawString("第",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			fltCurrentX += (long)szContent.Width+20;
//			p_objPrintArg.Graphics.DrawString("页",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
		}
		#endregion
		/// <summary>
		/// 年龄转换
		/// </summary>
		/// <param name="strage"></param>
		private string m_mthAgeChange(string strage)
		{
			int length =strage.Length;
			string  strTextAge="1";
			string strCmbAge="年";
			strCmbAge=strage.Substring(0,1);//年龄单位
			switch(strCmbAge.Trim())
			{
				case "C":
					strCmbAge="岁";
					break;
				case "B":
					strCmbAge="月";
					break;
				case "A":
					strCmbAge="天";
					break;
			}
			strTextAge=strage.Substring(1,length-1);
			return strTextAge+strCmbAge;
		}
		private void m_mthPrintPageSub(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_mthPrintStart(p_objPrintArg);
			if(this.flag==1)
			{
				m_mthPrintMiddle(p_objPrintArg);
			}
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

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.1f;
			m_fltRightIndentProp=0.1f;
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
