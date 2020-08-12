using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISDCardiogramReport 的摘要说明。
	/// </summary>
    public class clsPrint_RISDCardiogramReport : com.digitalwave.GUI_Base.clsController_Base, infPrintRecord
	{
		public clsRIS_DCardiogramReport_VO objReportVO;
		private long m_lngWidthPage;//打印页的宽度
		private long m_lngY;//当前Y方向坐标

		private float m_fltLeftIndentProp;//左缩进比例
		private float m_fltRightIndentProp;//右缩进比例

		private Font m_fntTitle;
		private Font m_fntSubTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;
		private Font m_fntMiddleNotBold;

		/// 边框画笔
		/// </summary>
		private Pen m_GridPen;

		#region 构造函数
		public clsPrint_RISDCardiogramReport()
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
		private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_lngWidthPage=p_objPrintArg.PageBounds.Width;

			//打印标题
            string strTitle = this.m_objComInfo.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(strTitle,m_fntTitle);			
			float fltCurrentX = (m_lngWidthPage-(long)szTitle.Width)/2;//标题文本左上角的X轴坐标
			m_lngY = 60;//标题文本左上角Y轴坐标
			p_objPrintArg.Graphics.DrawString(strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

			string strSubTitle = "动 态 心 电 图 报 告 单";
			szTitle = p_objPrintArg.Graphics.MeasureString(strSubTitle,m_fntSubTitle);
			fltCurrentX = (m_lngWidthPage-(long)szTitle.Width)/2;//标题文本左上角的X轴坐标
			m_lngY += (long)szTitle.Height;
			p_objPrintArg.Graphics.DrawString(strSubTitle,m_fntSubTitle,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += m_fntSubTitle.Height + 10;

			//打印标题右边部分
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("动态号",m_fntSmallNotBold);
			float fltRightX = m_lngWidthPage*3/4;
			float fltRightY = 70;
			p_objPrintArg.Graphics.DrawString("动态号",m_fntSmallNotBold,Brushes.Black,fltRightX,fltRightY);
			fltRightX += 5 + szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,fltRightY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,fltRightY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp),fltRightY+m_fntSmallNotBold.Height);

			szContent = p_objPrintArg.Graphics.MeasureString("住院号",m_fntSmallNotBold);
			fltRightX = m_lngWidthPage*3/4;
			fltRightY += 10 + m_fntSmallNotBold.Height;
			p_objPrintArg.Graphics.DrawString("住院号",m_fntSmallNotBold,Brushes.Black,fltRightX,fltRightY);
			fltRightX += 5 + szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,fltRightY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,fltRightY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp),fltRightY+m_fntSmallNotBold.Height);

		}
		#endregion

		#region 打印报告的中间部分
		private void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("三",m_fntSmallNotBold);//获取一个字符的宽度

			//姓名
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("姓名",m_fntSmallNotBold);
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("姓名",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 2;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX-2,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_lngY+m_fntSmallNotBold.Height,fltCurrentX+54,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX+=60;	
			//性别
			szContent = p_objPrintArg.Graphics.MeasureString("性别",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1.35/6;
			p_objPrintArg.Graphics.DrawString("性别",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width+2 ;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX-2,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_lngY+m_fntSmallNotBold.Height,fltCurrentX+30,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX+=32;
			//年龄
			szContent = p_objPrintArg.Graphics.MeasureString("年龄",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1/3-5;
			p_objPrintArg.Graphics.DrawString("年龄",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width+2 ;
			string strAge=objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ","");
			p_objPrintArg.Graphics.DrawString(strAge,m_fntSmallNotBold,Brushes.Black,fltCurrentX-2,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_lngY+m_fntSmallNotBold.Height,fltCurrentX+54,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX+=60;
			//科别
			szContent = p_objPrintArg.Graphics.MeasureString("科室",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.85/2+10;

            p_objPrintArg.Graphics.DrawString("科室", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
			fltCurrentX += szContent.Width+2;// + 5;
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, m_fntSmallNotBold, Brushes.Black, new RectangleF(fltCurrentX - 2, m_lngY,130F,m_fntSmallNotBold.Height*2));
            //p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, m_fntSmallNotBold, Brushes.Black, fltCurrentX - 2, m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_lngY+m_fntSmallNotBold.Height,fltCurrentX+54+30,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX+=60+30+30;
			//床号
			szContent = p_objPrintArg.Graphics.MeasureString("床号",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1.75/3;
			p_objPrintArg.Graphics.DrawString("床号",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width +2;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX-2,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_lngY+m_fntSmallNotBold.Height,fltCurrentX+30,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX+=32;
			//报告日期
			szContent = p_objPrintArg.Graphics.MeasureString("报告日期",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)4/6;
			p_objPrintArg.Graphics.DrawString("报告日期",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_DAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX-5,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp),m_lngY+m_fntSmallNotBold.Height);

			//临床诊断(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("临床诊断",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("临床诊断",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strCLINICAL_DIAGNOSE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp),m_lngY+m_fntSmallNotBold.Height);

			//监测时间(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("监测时间",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("监测时间",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 10;
			DateTime CheckFromDat = DateTime.Parse(objReportVO.m_strCHECKFROM_DAT);
			//年
			p_objPrintArg.Graphics.DrawString(CheckFromDat.Year.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 45;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-55,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("年",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("年",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//月
			fltCurrentX += szContent.Width + 10;
			p_objPrintArg.Graphics.DrawString(CheckFromDat.Month.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 25; 
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("月",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("月",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//日
			fltCurrentX += szContent.Width + 10;
			p_objPrintArg.Graphics.DrawString(CheckFromDat.Day.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 25;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("日",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("日",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//时
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(CheckFromDat.Hour.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 25;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("时",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//分
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(CheckFromDat.Minute.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 20;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-30,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("分至",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("分至",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//截止日期
			DateTime CheckDatTo = DateTime.Parse(objReportVO.m_strCHECKTO_DAT);
			//日
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(CheckDatTo.Day.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 20;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-30,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("日",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("日",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//时
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(CheckDatTo.Hour.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 20;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-30,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("时",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//分
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(CheckDatTo.Minute.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 20;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-30,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("分共",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("分共",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//统计总的时间
			fltCurrentX += szContent.Width + 10;
			string strTotalTime = m_mthTotalTime(CheckFromDat,CheckDatTo);
			p_objPrintArg.Graphics.DrawString(strTotalTime,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 60;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-70,m_lngY+m_fntSmallNotBold.Height,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("时",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//监测导联(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("监测导联",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("监测导联",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strCHECK_CHANNELS_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1.2/3,m_lngY+m_fntSmallNotBold.Height);

			//图形记录状况
			szContent = p_objPrintArg.Graphics.MeasureString("，图形记录状况：",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1.2/3;
			p_objPrintArg.Graphics.DrawString("，图形记录状况：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			string strGraphStatus = "";
			if(objReportVO.m_intGRAPH_TYPE_INT == 0)
			{
				strGraphStatus = "好";
			}
			else if(objReportVO.m_intGRAPH_TYPE_INT == 1)
			{
				strGraphStatus = "较好";
			}
			else if(objReportVO.m_intGRAPH_TYPE_INT == 2)
			{
				strGraphStatus = "一般";
			}
			else if(objReportVO.m_intGRAPH_TYPE_INT == 3)
			{
				strGraphStatus = "欠佳";
			}
			p_objPrintArg.Graphics.DrawString(strGraphStatus,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//QRS总数(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("QRS总数",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("QRS总数",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQRS_TOTAL_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.8/2,m_lngY+m_fntSmallNotBold.Height);

			//平均心率
			szContent = p_objPrintArg.Graphics.MeasureString("平均心率",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.8/2;
			p_objPrintArg.Graphics.DrawString("平均心率",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_intHEARTRATE_AVERAGE_INT.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1.8/3,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)1.8/3;
			p_objPrintArg.Graphics.DrawString("次/分。",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//最高心率(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("最高心率",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("最高心率",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_intHEARTRATE_MAX_INT.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.645/2,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.645/2;
			szContent = p_objPrintArg.Graphics.MeasureString("次/分。",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("次/分。",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//最高心率时间
			DateTime maxHeartRateDat = DateTime.Parse(objReportVO.m_strHEARTRATE_MAX_DAT);
			fltCurrentX += (long)szContent.Width + 10;
			szContent = p_objPrintArg.Graphics.MeasureString("时间",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时间",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//日
			fltCurrentX += (long)szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(maxHeartRateDat.Day.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 40;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-45,m_lngY+m_fntSmallNotBold.Height,fltCurrentX,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("日",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("日",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//时
			fltCurrentX += (long)szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(maxHeartRateDat.Hour.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 30;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("时",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//分
			fltCurrentX += (long)szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(maxHeartRateDat.Minute.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 30;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("分",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("分",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//最低心率(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("最低心率",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("最低心率",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_intHEARTRATE_MIN_INT.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.645/2,m_lngY+m_fntSmallNotBold.Height);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.645/2;
			szContent = p_objPrintArg.Graphics.MeasureString("次/分。",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("次/分。",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//最高心率时间
			DateTime minHeartRateDat = DateTime.Parse(objReportVO.m_strHEARTRATE_MIN_DAT);
			fltCurrentX += (long)szContent.Width + 10;
			szContent = p_objPrintArg.Graphics.MeasureString("时间",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时间",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//日
			fltCurrentX += (long)szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(minHeartRateDat.Day.ToString().Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 40;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-45,m_lngY+m_fntSmallNotBold.Height,fltCurrentX,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("日",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("日",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//时
			fltCurrentX += (long)szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(minHeartRateDat.Hour.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 30;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("时",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("时",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//分
			fltCurrentX += (long)szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(minHeartRateDat.Minute.ToString().Trim().PadLeft(2,'0'),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 30;
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-35,m_lngY+m_fntSmallNotBold.Height,fltCurrentX,m_lngY+m_fntSmallNotBold.Height);
			szContent = p_objPrintArg.Graphics.MeasureString("分",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("分",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//基础心律(换行)
			szContent = p_objPrintArg.Graphics.MeasureString("基础心律",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("基础心律",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHEARTRATE_BASE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			
			//分析及结论
			szContent = p_objPrintArg.Graphics.MeasureString("分析：",m_fntSmallBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += 15+(long)szContent.Height;
			p_objPrintArg.Graphics.DrawString("分析：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			m_lngY += 15 + (int)szContent.Height; 
			long CurrentY = m_lngY;
			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			if(strSummary1 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width*2;//m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width*2;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;

				int intRealHeight=0;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntMiddleNotBold.FontFamily.Name,m_fntMiddleNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

			}
			m_lngY += m_fntMiddleNotBold.Height * 12 + 4;

			m_lngY += (int)szContent.Height*5 + 5;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;

			szContent = p_objPrintArg.Graphics.MeasureString("结论：",m_fntSmallBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY += (long)szContent.Height-130;
			p_objPrintArg.Graphics.DrawString("结论：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			m_lngY += 15 + (int)szContent.Height; 

			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
			CurrentY = m_lngY;
			if(strSummary2 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width*2;//m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width*2;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;

				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntMiddleNotBold.FontFamily.Name,m_fntMiddleNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);
			}
			m_lngY += m_fntMiddleNotBold.Height * 10 + 4;
		}
		#endregion

		#region 打印报告结尾部分
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_lngY = p_objPrintArg.PageBounds.Height-95;
			//报告者
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("报告者",m_fntSmallNotBold);
			float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)3/4;
			p_objPrintArg.Graphics.DrawString("报告者",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngY+m_fntSmallNotBold.Height,m_lngWidthPage*(1-m_fltLeftIndentProp)*0.92f,m_lngY+m_fntSmallNotBold.Height);

			m_lngY += 40;
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)0.92;
			szContent = p_objPrintArg.Graphics.MeasureString("第",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("第  1",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += (long)szContent.Width+20;
			p_objPrintArg.Graphics.DrawString("页",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
		}
		#endregion

		#region 统计时间函数
		private string m_mthTotalTime(DateTime fromDat,DateTime ToDat)
		{
			TimeSpan tspTemp = ToDat - fromDat;
			if(tspTemp.TotalMinutes <=0)
			{
				return "0";
			}
			int intTotalMinutes = Convert.ToInt32(tspTemp.TotalMinutes);
			int intHours = intTotalMinutes / 60;
			int intMinutes = intTotalMinutes - intHours * 60;
			string strMinutes = "";
			if(intMinutes == 0)
			{
				strMinutes = "";
			}
			else if(intMinutes < 10 && intMinutes > 0)
			{
				strMinutes = ":0" + intMinutes.ToString();
			}
			else
			{
				strMinutes = ":" + intMinutes.ToString();
			}
			return intHours.ToString() + strMinutes;
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
			m_fntTitle= new Font("SimSun", 13,FontStyle.Bold);
			m_fntSubTitle = new Font("SimSun", 18,FontStyle.Bold);
			m_fntSmallBold= new Font("SimSun",13,FontStyle.Bold);
			m_fntSmallNotBold=new Font("SimSun",11.5f,FontStyle.Regular);
			m_fntMiddleNotBold=new Font("SimSun",14,FontStyle.Regular);

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.05f;
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
