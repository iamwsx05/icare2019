using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISTCDReport 的摘要说明。
	/// </summary>
	public class clsPrint_RISTCDReport:infPrintRecord
	{
		private long m_lngWidthPage;//打印页的宽度
		private long m_lngHeightPage;//打印页高度
		private long m_lngY;//当前Y方向坐标

		private float m_fltLeftIndentProp;//左缩进比例
		private float m_fltRightIndentProp;//右缩进比例

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;

		public clsRIS_TCD_REPORT_VO objReportVO;

		/// 边框画笔
		/// </summary>
		private Pen m_GridPen;

		#region 构造函数
		public clsPrint_RISTCDReport()
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
		public clsPrint_RISTCDReport(int i)
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
			m_lngHeightPage=p_objPrintArg.PageBounds.Height;
            m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			
			//打印标题
			string m_strTitle = m_objComm.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+30;//标题文本左上角的X轴坐标
			m_lngY = m_lngHeightPage/13;//标题文本左上角Y轴坐标
			p_objPrintArg.Graphics.DrawString(m_strTitle+"经颅多普勒（TCD）报告单",m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

//			string m_strSubTitle = "经颅多普勒（TCD）报告单";
//			fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2-szTitle.Width/9;
//			m_lngY += 20;
//			p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			//打印标题右边部分
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("TCD 号",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*(1-m_fltRightIndentProp)-szContent.Width-50;
			float fltTempX = fltCurrentX;//记录TCD号的X坐标，为了统一
			p_objPrintArg.Graphics.DrawString("TCD 号",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+15);
			fltCurrentX+= szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+15);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY+15,fltCurrentX+50,m_fntSmallNotBold.Height+m_lngY+15);

			//姓名
			m_lngY += m_lngHeightPage/24;//Y坐标
			szContent = p_objPrintArg.Graphics.MeasureString("姓名",m_fntSmallNotBold);
			float m_lngLineY =m_fntSmallNotBold.Height+m_lngY;//线的Y坐标
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("姓名",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+54,m_lngLineY);
			fltCurrentX+=54;
			//性别
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
			szContent = p_objPrintArg.Graphics.MeasureString("性别",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("性别",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+26,m_lngLineY);
			fltCurrentX+=26;

			//年龄
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2);
			szContent = p_objPrintArg.Graphics.MeasureString("年龄",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("年龄",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(m_mthAgeChange(objReportVO.m_strAGE_FLT.ToString().Trim()),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+46,m_lngLineY);
			fltCurrentX+=46;

			//科别

			szContent = p_objPrintArg.Graphics.MeasureString("科室",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("科室",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR.Trim(), m_fntSmallNotBold, Brushes.Black, new RectangleF(fltCurrentX , m_lngY,115F,m_fntSmallNotBold.Height*2));
			//画线
            p_objPrintArg.Graphics.DrawLine(m_GridPen, fltCurrentX - 5, m_lngLineY, fltCurrentX + 75 + 30, m_lngLineY);
			fltCurrentX+=75+30;

			//病室或门诊
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
			szContent = p_objPrintArg.Graphics.MeasureString("房号",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("房号",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+31,m_lngLineY);
			fltCurrentX+=31;
			//床号
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2);
			szContent = p_objPrintArg.Graphics.MeasureString("床号",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("床号",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+32,m_lngLineY);
//			住 院 号
			szContent = p_objPrintArg.Graphics.MeasureString("住院号",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("住院号",m_fntSmallNotBold,Brushes.Black,fltTempX,m_lngY);
			fltCurrentX = fltTempX+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+50,m_lngLineY);
//			门 诊 号
			szContent = p_objPrintArg.Graphics.MeasureString("门诊号",m_fntSmallNotBold);
			m_lngLineY+= m_lngHeightPage/24;
			m_lngY += m_lngHeightPage/24;
			p_objPrintArg.Graphics.DrawString("门诊号",m_fntSmallNotBold,Brushes.Black,fltTempX,m_lngY-15);
			fltTempX= fltTempX+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltTempX,m_lngY-15);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltTempX-5,m_lngLineY-15,fltTempX+50,m_lngLineY-15);



		}
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
		#endregion

		#region 打印报告正文部分
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
//			objReportVO.
			m_lngY=m_lngHeightPage*3/4-50;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;

			SizeF szContent = p_objPrintArg.Graphics.MeasureString("分析:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("分析:",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY+=(long)szContent.Height-75;
			fltCurrentX+=szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("正文",m_fntSmallNotBold);
			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			float fltLeftX =fltCurrentX;
			float fltRightX = m_lngWidthPage*4/5-50;
			if(strSummary1 != null)
			{
					int intRealHeight1=0;
				m_lngY-=50;
				fltRightX-=10;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY+szContent.Height*2));
				com.digitalwave.controls.clsPrintRichTextContext objPrint2=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint2.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint2.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight1,false,false);

				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY1_VCHR ,m_fntSmallNotBold ,Brushes.Black,rectSummary1);
			}
			m_lngY =m_lngY+(long)szContent.Height*4;

			
			szContent = p_objPrintArg.Graphics.MeasureString("印象:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("印象:",m_fntSmallBold,Brushes.Black,fltCurrentX-szContent.Width,m_lngY);
			m_lngY += (int)szContent.Height;

			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
		
			if(strSummary2 != null)
			{
			int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY));
			
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY2_VCHR ,m_fntSmallNotBold ,Brushes.Black,rectSummary2);
			}
		}
		#endregion

		#region 打印报告结尾部分
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("(此报告仅供本院医师参考)",m_fntSmallNotBold);
			m_lngY = p_objPrintArg.PageBounds.Height-150+(long)(szContent.Height*5);
			float fltCurrentX = m_lngWidthPage/2 - szContent.Width/2;
			p_objPrintArg.Graphics.DrawString("(此报告仅供本院医师参考)",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			
			m_lngY = p_objPrintArg.PageBounds.Height*11/13+40-65+(long)(szContent.Height*5);
		    fltCurrentX = m_lngWidthPage*5/8;
			szContent = p_objPrintArg.Graphics.MeasureString("医    师：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("医    师：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX+szContent.Width+5,m_lngY);
			m_lngY = p_objPrintArg.PageBounds.Height*23/26+20-65+(long)(szContent.Height*5);
			p_objPrintArg.Graphics.DrawString("报告日期：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			DateTime dt =DateTime.Parse(objReportVO.m_strREPORT_DAT);
			p_objPrintArg.Graphics.DrawString(dt.ToString("yyyy-MM-dd"),m_fntSmallNotBold,Brushes.Black,fltCurrentX+szContent.Width+5,m_lngY);

		}
		#endregion

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
			m_fntTitle= new Font("SimSun", 15,FontStyle.Bold);
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			m_fntSmallNotBold=new Font("SimSun",10.5f,FontStyle.Regular);

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.092f;
			m_fltRightIndentProp=0.092f;
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
