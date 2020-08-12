using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISCardiogramReport 的摘要说明。
	/// </summary>
    public class clsPrint_RISSportReport : com.digitalwave.GUI_Base.clsController_Base, infPrintRecord
	{
		private long m_lngWidthPage;//打印页的宽度
		private long m_lngY;//当前Y方向坐标

		private float m_fltLeftIndentProp;//左缩进比例
		private float m_fltRightIndentProp;//右缩进比例

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;
		private Font m_fntMiddleNotBold;

		public clsafmt_report_VO objReportVO;

		/// 边框画笔
		/// </summary>
		private Pen m_GridPen;

		#region 构造函数
		public clsPrint_RISSportReport()
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
			m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			
			//打印标题
            string m_strTitle = this.m_objComInfo.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
			float fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2;//标题文本左上角的X轴坐标
			m_lngY = 60;//标题文本左上角Y轴坐标
			p_objPrintArg.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

			m_lngY += (int)szTitle.Height + 10;
			string m_strSubTitle = "心电图活动平板运动试验报告单";
			fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2-szTitle.Width/4;
			//			m_lngY += 20;
			p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);
			//打印标题右边部分
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("平 板 号",m_fntSmallNotBold);
			float fltRightX = m_lngWidthPage*(float)2.87/4;
			long lngRightY = 60;
			p_objPrintArg.Graphics.DrawString("平 板 号",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

			//住 院 号
			szContent = p_objPrintArg.Graphics.MeasureString("住 院 号",m_fntSmallNotBold);
			fltRightX = m_lngWidthPage*(float)2.87/4;
			lngRightY += 10 + (int)szContent.Height;
			p_objPrintArg.Graphics.DrawString("住 院 号",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

			//门 诊 号
			szContent = p_objPrintArg.Graphics.MeasureString("门 诊 号",m_fntSmallNotBold);
			fltRightX = m_lngWidthPage*(float)2.87/4;
			lngRightY += 10 + (int)szContent.Height;
			p_objPrintArg.Graphics.DrawString("门 诊 号",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

			//姓名
			m_lngY = lngRightY + 20 + (int)szContent.Height;
			szContent = p_objPrintArg.Graphics.MeasureString("姓名",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("姓名",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+80,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=80;
			//性别
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/5);
			szContent = p_objPrintArg.Graphics.MeasureString("性别",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("性别",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+45,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=50;
			//年龄
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
			szContent = p_objPrintArg.Graphics.MeasureString("年龄",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("年龄",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			string strAge=objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ","");
			p_objPrintArg.Graphics.DrawString(strAge,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+70,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=80;
			//职业
			szContent = p_objPrintArg.Graphics.MeasureString("职业",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("职业",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString("",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+70,m_fntSmallNotBold.Height+m_lngY);
			//科别
			fltCurrentX+=80;
			szContent = p_objPrintArg.Graphics.MeasureString("科室",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("科室",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX +=40;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR.Trim(),m_fntSmallNotBold,Brushes.Black,new RectangleF(fltCurrentX,m_lngY,110F,m_fntSmallNotBold.Height*2));
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+110,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=110;
			//床号
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
			szContent = p_objPrintArg.Graphics.MeasureString("床号",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("床号",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+35,m_fntSmallNotBold.Height+m_lngY);

			//临床诊断
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
			szContent = p_objPrintArg.Graphics.MeasureString("临床诊断：",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("临床诊断：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+40);
			fltCurrentX += szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strCLINICAL_DIAGNOSE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+40);

		}
		#endregion

		#region 打印报告正文部分
		/// <summary>
		/// 打印报告正文部分
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("三",m_fntSmallNotBold);//获取一个字符的宽度
			SizeF szContentCount = p_objPrintArg.Graphics.MeasureString("运动前心电图：",m_fntSmallBold);
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("运动前心电图：",m_fntSmallBold);
			m_lngY += 60 + (int)szContent.Height;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("运动前心电图：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			//节律
			fltCurrentX+=szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("节律：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("节律：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strRHYTHM_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+140,m_fntSmallNotBold.Height+m_lngY);
			//卧位
			fltCurrentX+=140;
			szContent = p_objPrintArg.Graphics.MeasureString("，心率：卧位",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("，心率：卧位",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strLIE_PST_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			//立位
			fltCurrentX+=30;
			szContent = p_objPrintArg.Graphics.MeasureString("次/分，立位：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("次/分，立位：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSTAND_PST_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("次/分，",m_fntSmallNotBold,Brushes.Black,fltCurrentX+30,m_lngY);

			//深呼吸（换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szContentCount.Width-90;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("深呼吸",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("深呼吸：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEEP_BREATH_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);

			//P_R期间
			fltCurrentX+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("次/分，P-R 间期",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("次/分，P-R 间期：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strP_R_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);

			//QRS时限
			fltCurrentX+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("秒，QRS时限：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("秒，QRS时限：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQRS_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);
			//Q-T
			fltCurrentX+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("秒，Q-T：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("秒，Q-T：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQ_T_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("秒。",m_fntSmallNotBold,Brushes.Black,fltCurrentX+40,m_lngY);
			//运动前血压(换行)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("运动前血压：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("运动前血压：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBEFORE_ACTIVE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+490,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("毫米汞柱",m_fntSmallNotBold,Brushes.Black,fltCurrentX+490,m_lngY);
			//运动平板运动试验(换行)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("运动平板运动试验：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("运动平板运动试验：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			//预测心率
			fltCurrentX+=szContent.Width;
			if(objReportVO.m_intFORECAST_QTY_INT==1)
			{
				p_objPrintArg.Graphics.DrawString("预测亚极量：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			else
			{
				p_objPrintArg.Graphics.DrawString("预测极量：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			fltCurrentX+=szPerWord.Width*4;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strFORECAST_QTY_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+50,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("次/分。",m_fntSmallNotBold,Brushes.Black,fltCurrentX+50,m_lngY);
			//方案（换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent= p_objPrintArg.Graphics.MeasureString("运动前血压：",m_fntSmallBold);
			fltCurrentX+=szContent.Width;
			fltCurrentX-=70;
			p_objPrintArg.Graphics.DrawString("按",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szPerWord.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strTEST_PLAN_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+90,m_fntSmallNotBold.Height+m_lngY);

			//运动负何
			fltCurrentX+=90;
			szContent= p_objPrintArg.Graphics.MeasureString("方案，运动负荷达第",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("方案，运动负荷达第",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_LOAD_LEVEL_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("级（",m_fntSmallNotBold,Brushes.Black,fltCurrentX+30,m_lngY);
			fltCurrentX+=70;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_LOAD_MPH_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=30;
			p_objPrintArg.Graphics.DrawString("MPH/",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=50;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_LOAD_PER_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=30;
			p_objPrintArg.Graphics.DrawString("%),运动总时间",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=p_objPrintArg.Graphics.MeasureString("%),运动总时间",m_fntSmallNotBold).Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_TOTAL_TIME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+60,m_fntSmallNotBold.Height+m_lngY);
			//运动时间(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			
			//画线
			
			p_objPrintArg.Graphics.DrawString("分，",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//最高心率
			fltCurrentX+=30;
			szContent= p_objPrintArg.Graphics.MeasureString("最高心率",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("最高心率",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_TOP_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);

			//预测心率
			fltCurrentX =fltCurrentX+30;
			szContent= p_objPrintArg.Graphics.MeasureString("次/分，为预测心率的",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("次/分，为预测心率的",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_PER_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			//预测心率
			fltCurrentX =fltCurrentX+30;
			szContent= p_objPrintArg.Graphics.MeasureString("%，max work",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("%，max work",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_MAX_WORK_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+50,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString(" METS。",m_fntSmallNotBold,Brushes.Black,fltCurrentX+45,m_lngY);
		

			//运动终止原因（换行)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent= p_objPrintArg.Graphics.MeasureString("运动终止原因",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("运动终止原因",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSTOP_REASON_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+450,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("。",m_fntSmallNotBold,Brushes.Black,fltCurrentX+450,m_lngY);

			//运动后心电图(换行)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("运动后心电图：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("运动后心电图：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			//ST段压低/抬高
			szContent = p_objPrintArg.Graphics.MeasureString("ST段压低/抬高：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("ST段压低/抬高：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+100,m_fntSmallNotBold.Height+m_lngY);

			//形态
			fltCurrentX+=110;
			szContent = p_objPrintArg.Graphics.MeasureString("，形态",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("，形态",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MODE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+245,m_fntSmallNotBold.Height+m_lngY);

			//出现导联(换行)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("，出现导联",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("，出现导联",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			if(objReportVO.m_strAPPEAR_LED_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX =fltLeftX;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strAPPEAR_LED_VCHR ,objReportVO.m_strAPPEAR_LED_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
			}
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strAPPEAR_LED_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+290,m_fntSmallNotBold.Height+m_lngY);

			//心率范围
			fltCurrentX+=300;
			szContent = p_objPrintArg.Graphics.MeasureString("心率范围",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("心率范围",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			if(objReportVO.m_strHR_SCOPE_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = 170;
				fltCurrentX =fltLeftX;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strHR_SCOPE_VCHR ,objReportVO.m_strHR_SCOPE_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
			}
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_SCOPE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+160,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString(",",m_fntSmallNotBold,Brushes.Black,fltCurrentX+160,m_lngY);

			//起止时间(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("起止时间",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("起止时间",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			if(objReportVO.m_strTIME_SCOPE_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX =fltLeftX;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strTIME_SCOPE_VCHR ,objReportVO.m_strTIME_SCOPE_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
			}
//			fltCurrentX+=szContent.Width;
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strTIME_SCOPE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+560,m_fntSmallNotBold.Height+m_lngY);

			//画线(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			fltCurrentX+=szContent.Width;
			m_lngY+=(int)szPerWord.Height+1;
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+550,m_fntSmallNotBold.Height+m_lngY);

			p_objPrintArg.Graphics.DrawString(",",m_fntSmallNotBold,Brushes.Black,fltCurrentX+550,m_lngY);
			
			//ST段(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
            
			if(objReportVO.m_intACTIVE_ST_INT==1)
			{
				szContent = p_objPrintArg.Graphics.MeasureString("ST段压低最大值",m_fntSmallNotBold);
			    p_objPrintArg.Graphics.DrawString("ST段压低最大值",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			else
			{
				szContent = p_objPrintArg.Graphics.MeasureString("ST段抬高最大值",m_fntSmallNotBold);
				p_objPrintArg.Graphics.DrawString("ST段抬高最大值",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MAX_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+490,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("mm，",m_fntSmallNotBold,Brushes.Black,fltCurrentX+490,m_lngY);

			//出现导联(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("出现导联",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("出现导联",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MAX_LED_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+230,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("，",m_fntSmallNotBold,Brushes.Black,fltCurrentX+230,m_lngY);

			//出现时间
			fltCurrentX+=240;
			szContent = p_objPrintArg.Graphics.MeasureString("出现时间",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("出现时间",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MAX_TIME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+250,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString(",",m_fntSmallNotBold,Brushes.Black,fltCurrentX+250,m_lngY);


			//ST段(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;

			szContent = p_objPrintArg.Graphics.MeasureString("心律失常：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("心律失常：",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			fltCurrentX+=szContent.Width;
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_WRONG_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);


			if(objReportVO.m_strHR_WRONG_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX += 4;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strHR_WRONG_VCHR ,objReportVO.m_strHR_WRONG_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
				long lngEndY = (long)m_lngY + m_fntSmallNotBold.Height + 3;
				m_lngY = lngEndY;
			}

			//运动后血压(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("运动后血压：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("运动后血压：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVED_BP_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//画线
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+510,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("毫米汞柱",m_fntSmallNotBold,Brushes.Black,fltCurrentX+510,m_lngY);


			//结论(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("结论：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("结论：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			//运动前(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("运动前：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("运动前：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX+=szContent.Width;
			if(objReportVO.m_strACTIVE_RESULT_VCHR!= null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX += 4;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strACTIVE_RESULT_VCHR ,objReportVO.m_strACTIVE_RESULT_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
				long lngEndY = (long)m_lngY + m_fntSmallNotBold.Height * 2 + 3;
				m_lngY = lngEndY;
			}

			//活动平板运动试验(换行）
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("活动平板运动试验：",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("活动平板运动试验：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX+=szContent.Width;
			if(objReportVO.m_strTEST_RESULT_VCHR!= null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX += 4;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strTEST_RESULT_VCHR ,objReportVO.m_strTEST_RESULT_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);

			}


		}
		#endregion

		#region 打印报告结尾部分
		/// <summary>
		/// 打印报告结尾部分
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			//			if(m_lngY<850) m_lngY=850;
			m_lngY = p_objPrintArg.PageBounds.Height-80;
			float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("报 告 者：",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("报 告 者：",m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3,m_lngY);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3+75,m_lngY);
			//报告日期
			DateTime ReportDat = DateTime.Parse(objReportVO.m_strREPORT_DAT);
			szContent = p_objPrintArg.Graphics.MeasureString("报告日期:",m_fntSmallNotBold);
			m_lngY+=25;
			p_objPrintArg.Graphics.DrawString("报告日期：",m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 ,m_lngY);
			p_objPrintArg.Graphics.DrawString(ReportDat.ToString("yyyy年MM月dd日 HH:mm"),m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +80,m_lngY);
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
