using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls.GN
{
	/// <summary>
	/// Summary description for ctlThreeMeasureRecord.
	/// </summary>
	public class clsThreeMeasureRecordPrintTool //: System.Windows.Forms.PictureBox
	{
		public clsThreeMeasureRecordPrintTool()
		{
			// TODO: Add any initialization after the InitForm call

			m_intTotalHeight = 
				c_intRecordDateHeight+
				c_intInpateintDateHeight+
				c_intSpecialDateHeight+
				c_intTimeHeight+
				c_intGridHeight*c_intGridHeightCount+
				c_intBreathHeight+
				c_intInputHeight+
				c_intDejectaHeight+
				c_intPeeHeight+
				c_intOutStreamHeight+
				c_intPressureHeight+
				c_intWeightHeight+
				c_intSkinTestHeight+
				c_intOtherHeight;

//			this.Height = m_intTotalHeight;
//			this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

			m_mthInitMouthTemperatureImage();
			m_mthInitArmpitTemperatureImage();
			m_mthInitAnusTemperatureImage();

			m_mthInitPulseImage();
			m_mthInitHeartRateImage();

			m_mthInitBreathImage();
			m_mthInitStayOutImage();

			m_mthInitDownTemperatureImage();

			m_mthInitMouthTemperatureCoverImage();
			m_mthInitAnusTemperatureCoverImage();
			m_mthInitArmpitTemperatureCoverImage();

			m_objDateManager = new clsThreeMeasureDateRecordManager();
			m_objPulseValueManager = new clsThreeMeasurePulseValueManager(m_objDateManager);
			m_objTemperatureValueManager = new clsThreeMeasureTemperatureValueManager(m_objDateManager);			
			m_objBreathValueManager = new clsThreeMeasureBreathValueManager(m_objDateManager);	
			m_objStayOutValueManager = new clsThreeMeasureStayOutValueManager(m_objDateManager);	

			m_strUserID = "";
			m_strUserName = "";

			m_clrDST = Color.Red;

			m_objXmlStream = new MemoryStream();

			m_objXmlWriter = new XmlTextWriter(m_objXmlStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();

			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);
		}		
	
		private DateTime m_dtmInPatientDate;

		public DateTime m_DtmInPatientDate
		{
			set 
			{
				m_dtmInPatientDate=value;
			}
			get 
			{
				return m_dtmInPatientDate;
			}
		}
		ArrayList arlDate=new ArrayList();//记录画手术第几天的集合
		int intSpecialNewStartTimes = 0; //记录画手术第几天次数

		//其它重新计算高度变量 
		ArrayList arlSkinHeightMax=new ArrayList();
		ArrayList arlOtherHeightMax=new ArrayList();

		/// <summary>
		/// 记录开始的手术日期
		/// </summary>
		private DateTime dtmPreDateForSpecialDate = DateTime.Today;
		/// <summary>
		/// 记录当前的手术次数
		/// </summary>
		
		/// <summary>
		/// Other项目的名称
		/// </summary>
		private string m_strOtherName = null;

		/// <summary>
		/// 生成XML的内存
		/// </summary>
		private MemoryStream m_objXmlStream;

		/// <summary>
		/// 生成XML的工具
		/// </summary>
		private XmlTextWriter m_objXmlWriter;

		/// <summary>
		/// 读取XML的工具
		/// </summary>
		private XmlParserContext m_objXmlParser;

		/// <summary>
		/// 当前用户ID
		/// </summary>
		private string m_strUserID;

		/// <summary>
		/// 当前用户ID的设置和获取
		/// </summary>
		public string m_StrUserID
		{
			get
			{
				return m_strUserID;
			}
			set
			{
				if(value != null)
					m_strUserID = value;
			}
		}

		/// <summary>
		/// 当前用户姓名
		/// </summary>
		private string m_strUserName;

		/// <summary>
		/// 当前用户姓名的设置和获取
		/// </summary>
		public string m_StrUserName
		{
			get
			{
				return m_strUserName;
			}
			set
			{
				if(value != null)
					m_strUserName = value;
			}
		}

		/// <summary>
		/// 当前用户的双删除线的颜色
		/// </summary>
		private Color m_clrDST;		

		/// <summary>
		/// 当前用户的双删除线颜色的设置和获取
		/// </summary>
		public Color m_ClrDST
		{
			get
			{
				return m_clrDST;
			}
			set
			{
				m_clrDST = value;
			}
		}

		/// <summary>
		/// 是否处理控件的SizeChanged事件
		/// </summary>
		private bool m_blnCanResize = true;

		/// <summary>
		/// 记录每天的记录集
		/// </summary>
		private clsThreeMeasureDateRecordManager m_objDateManager;
		/// <summary>
		/// 记录所有的脉搏数据，并已经按时间顺序排序
		/// </summary>
		private clsThreeMeasurePulseValueManager m_objPulseValueManager;
		/// <summary>
		/// 记录所有的体温数据，并已经按时间顺序排序
		/// </summary>
		/// 
		/// 记录所有的呼吸数据，并已经按时间顺序排序
		/// </summary>
		private clsThreeMeasureBreathValueManager m_objBreathValueManager;

		/// 记录所有的外出数据，并已经按时间顺序排序
		/// </summary>
		private clsThreeMeasureStayOutValueManager m_objStayOutValueManager;

		private clsThreeMeasureTemperatureValueManager m_objTemperatureValueManager;

		private static int s_intCoverID = int.MinValue;

		private static int s_IntCoverID
		{
			get
			{
				s_intCoverID++;
				if(s_intCoverID == int.MinValue)
				{
					//s_intCoverID是int.MaxValue时，加1会成为int.MinValue
					s_intCoverID++;
				}

				return s_intCoverID;
			}
		}		

		#region 表格格式
		/// <summary>
		/// 总共的添数，缺省使用7天
		/// </summary>
		private int m_intTotalDate = 7;

		/// <summary>
		/// 总共的高度
		/// </summary>
		private int m_intTotalHeight;

		//各种字体大小的数值
		private const float c_flt12PointFontSize = 12f;
		private const float c_flt10PointFontSize = 10.5f;
		private const float c_flt9PointFontSize = 9f;
		private const float c_flt7PointFontSize = 7.5f;
		private const float c_flt6PointFontSize = 6.75f;
		private const float c_flt5PointFontSize = 5.25f;
		private const float c_flt5MorePointFontSize = 6.00f;
		private const float c_flt14PointFontSize = 14.25f;

		//各种项目的高度
		private const int c_intRecordDateHeight =17;// 28;

		private const int c_intSpecialDateHeight =18;// 24;
		private const int c_intInpateintDateHeight =17;// 24;
		private const int c_intInpateintTotalHeight = 17+18;
		private const int c_intSpecialDateTotalHeight =18+17+17;

		private const int c_intTimeHeight = 24;
		private const int c_intTimeTotalHeight = 28+24+24;

		private const int c_intGridHeight = 12;
		private const int c_intGridHeightCount = 45;
		private int c_intGridTotalHeight = 28+24+24+c_intGridHeight*45;
		private int c_intLowEventTextStartHeight = 28+24+24+c_intGridHeight*35;

		private const int c_intStayOutHeight= 28+24+24+c_intGridHeight*30;

		private int c_intBreathHeight = 28;
		private int c_intBreathTotalHeight = 28+24+24+c_intGridHeight*45+28;
		private int m_intMaxBreathCount = 1;

		private int c_intInputHeight = 28;
		private int c_intInputTotalHeight = 28+24+24+c_intGridHeight*45+28+28;
		private int m_intMaxInputCount = 1;

		private int c_intDejectaHeight = 28;
		private int c_intDejectaTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28;
		private int m_intMaxDejectaCount = 1;

		private int c_intOutStreamHeight = 28;
		private int c_intOutStreamTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28;
		private int m_intMaxOutStreamCount = 1;

		private int c_intPeeHeight = 28;
		private int c_intPeeTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28;
		private int m_intMaxPeeCount = 1;


		private int c_intPressureHeight = 28;
		private int c_intPressureTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28;
		private int m_intMaxPressureCount = 1;

		private int c_intWeightHeight = 28;
		private int c_intWeightTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28;
		private int m_intMaxWeightCount = 1;

		private int c_intSkinTestHeight = 28;
		private int c_intSkinTestTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28+28;
		private int m_intMaxSkinTestCount = 1;		

		private int c_intOtherHeight = 28;
		private int c_intOtherTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28+28+28;
		private int m_intMaxOtherCount = 1;

		private const int c_intTextTotleWidth = 115;	
		#endregion

		#region 画图变量
		#region Border Line
		private Color m_clrSkinTest = Color.Black;
		public Color m_ClrSkinTest
		{
			get
			{
				return m_clrSkinTest;
			}
			set
			{
				m_clrSkinTest = value;
				
			}
		}
//		/// <summary>
//		/// 边框颜色
//		/// </summary>
//		private Color m_clrBorder = Color.White;
//		public Color m_ClrBorder
//		{
//			get
//			{
//				return m_clrBorder;
//			}
//			set
//			{
//				m_clrBorder = value;
//				Invalidate();
//			}
//		}
//		
//		/// <summary>
//		/// 格子线颜色
//		/// </summary>
//		private Color m_clrGridBorder = Color.Black;
//		public Color m_ClrGridBorder
//		{
//			get
//			{
//				return m_clrGridBorder;
//			}
//			set
//			{
//				m_clrGridBorder = value;
//				Invalidate();
//			}
//		}
//		
//		/// <summary>
//		/// 特殊格子线颜色
//		/// </summary>
//		private Color m_clrSpecialGridBorder = Color.Yellow;
//		public Color m_ClrSpecialGridBorder
//		{
//			get
//			{
//				return m_clrSpecialGridBorder;
//			}
//			set
//			{
//				m_clrSpecialGridBorder = value;
//				Invalidate();
//			}
//		}		
		#endregion

		#region Text
//		/// <summary>
//		/// 标题文字颜色
//		/// </summary>
//		private Color m_clrTitleText = Color.White;
//		public Color m_ClrTitleText
//		{
//			get
//			{
//				return m_clrTitleText;
//			}
//			set
//			{
//				m_clrTitleText = value;
//				Invalidate();
//			}
//		}
//		
//		/// <summary>
//		/// 特殊时间颜色
//		/// </summary>
//		private Color m_clrSpecialTimeText = Color.Yellow;
//		public Color m_ClrSpecialTimeText
//		{
//			get
//			{
//				return m_clrSpecialTimeText;
//			}
//			set
//			{
//				m_clrSpecialTimeText = value;
//				Invalidate();
//			}
//		}
//
//		/// <summary>
//		/// 手术或产后日期颜色
//		/// </summary>
//		private Color m_clrSpecialDateText = Color.Yellow;
//		public Color m_ClrSpecialDateText
//		{
//			get
//			{
//				return m_clrSpecialDateText;
//			}
//			set
//			{
//				m_clrSpecialDateText = value;
//				Invalidate();
//			}
//		}
//
//		/// <summary>
//		/// 上部（40℃上方）事件颜色
//		/// </summary>
//		private Color m_clrUpperEventText = Color.Yellow;
//		public Color m_ClrUpperEventText
//		{
//			get
//			{
//				return m_clrUpperEventText;
//			}
//			set
//			{
//				m_clrUpperEventText = value;
//				Invalidate();
//			}
//		}
//
//		/// <summary>
//		/// 下部（35℃下方）事件颜色
//		/// </summary>
//		private Color m_clrLowerEventText = Color.Yellow;
//		public Color m_ClrLowerEventText
//		{
//			get
//			{
//				return m_clrLowerEventText;
//			}
//			set
//			{
//				m_clrLowerEventText = value;
//				Invalidate();
//			}
//		}
//		
//		/// <summary>
//		/// 脉搏坐标信息颜色
//		/// </summary>
//		private Color m_clrPulseParamsText = Color.Yellow;
//		public Color m_ClrPulseParamsText
//		{
//			get
//			{
//				return m_clrPulseParamsText;
//			}
//			set
//			{
//				m_clrPulseParamsText = value;
//				Invalidate();
//			}
//		}
//
//		/// <summary>
//		/// 温度坐标信息颜色
//		/// </summary>
//		private Color m_clrTemperatureParamsText = Color.Black;
//		public Color m_ClrTemperatureParamsText
//		{
//			get
//			{
//				return m_clrTemperatureParamsText;
//			}
//			set
//			{
//				m_clrTemperatureParamsText = value;
//				Invalidate();
//			}
//		}
//
//		/// <summary>
//		/// 一天记录信息颜色
//		/// </summary>
//		private Color m_clrDateValue = Color.Black;
//		public Color m_ClrDateValue
//		{
//			get
//			{
//				return m_clrDateValue;
//			}
//			set
//			{
//				m_clrDateValue = value;
//				Invalidate();
//			}
//		}
//
//		/// 皮试阳性信息颜色
//		/// </summary>
//		private Color m_clrSkinTest = Color.Black;
//		public Color m_ClrSkinTest
//		{
//			get
//			{
//				return m_clrSkinTest;
//			}
//			set
//			{
//				m_clrSkinTest = value;
//				Invalidate();
//			}
//		}
		#endregion

		#region Value Line
//		/// <summary>
//		/// 脉搏连线颜色
//		/// </summary>
//		private Color m_clrPulseLine = Color.Red;
//		public Color m_ClrPulseLine
//		{
//			get
//			{
//				return m_clrPulseLine;
//			}
//			set
//			{
//				m_clrPulseLine = value;
//				Invalidate();
//			}
//		}
//		
//		/// <summary>
//		/// 体温连线颜色
//		/// </summary>
//		private Color m_clrTemperatureLine = Color.Red;
//		public Color m_ClrTemperatureLine
//		{
//			get
//			{
//				return m_clrTemperatureLine;
//			}
//			set
//			{
//				m_clrTemperatureLine = value;
//				Invalidate();
//			}
//		}
//
//		/// 物理降温温度连线信息颜色
//		/// </summary>
//		private Color m_clrDownTemperatureLine = Color.Red;
//		public Color m_ClrDownTemperatureLine
//		{
//			get
//			{
//				return m_clrDownTemperatureLine;
//			}
//			set
//			{
//				m_clrDownTemperatureLine = value;
//				Invalidate();
//			}
//		}

		#endregion		
		#endregion

		private const float c_fltTwoSymbolDiff = 1;

		#region 内部使用特殊标志点
		/// 物理降温温度坐标信息颜色
		/// </summary>
		private Color m_clrDownTemperature = Color.Red;
		public Color m_ClrDownTemperature
		{
			get
			{
				return m_clrDownTemperature;
			}
			set
			{
				m_clrDownTemperature = value;

				m_mthInitDownTemperatureImage();
			}
		}
		private Image m_imgDownTemperature = new Bitmap(c_intGridHeight-2,c_intGridHeight-2);
		private void m_mthInitDownTemperatureImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgDownTemperature);			
			gphImage.DrawEllipse(new Pen(m_clrDownTemperature,2),0,0,c_intGridHeight-3,c_intGridHeight-3);

			gphImage.Dispose();
		}

		private Image m_imgMouthTemperatureCover = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitMouthTemperatureCoverImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgMouthTemperatureCover);			
			gphImage.FillEllipse(new SolidBrush(m_clrTemperatureSymbol),1,1,c_intGridHeight-2,c_intGridHeight-2);
			
			gphImage.DrawEllipse(new Pen(m_clrPulseSymbol,2),0,0,c_intGridHeight-1,c_intGridHeight-1);

			gphImage.Dispose();
		}
		private Image m_imgAnusTemperatureCover = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitAnusTemperatureCoverImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgAnusTemperatureCover);			
			gphImage.FillEllipse(new SolidBrush(m_clrPulseSymbol),1,1,c_intGridHeight-2,c_intGridHeight-2);
			
			gphImage.DrawEllipse(new Pen(m_clrTemperatureSymbol,2),0,0,c_intGridHeight-1,c_intGridHeight-1);

			gphImage.Dispose();
		}
		private Image m_imgArmpitTemperatureCover = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitArmpitTemperatureCoverImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgArmpitTemperatureCover);			
			Pen penLine = new Pen(m_clrTemperatureSymbol,2);
			gphImage.DrawLine(penLine,2,2,c_intGridHeight-2,c_intGridHeight-2);
			gphImage.DrawLine(penLine,2,c_intGridHeight-2,c_intGridHeight-2,2);

			gphImage.DrawEllipse(new Pen(m_clrPulseSymbol,2),0,0,c_intGridHeight-1,c_intGridHeight-1);

			gphImage.Dispose();
		}
		#endregion

		#region 标志点及其初始化
		private Color m_clrDateValue = Color.Black;
		private Color m_clrPulseSymbol = Color.Black;
		public Color m_ClrPulseSymbol
		{
			get
			{
				return m_clrPulseSymbol;
			}
			set
			{
				m_clrPulseSymbol = value;

				m_mthInitPulseImage();
				m_mthInitHeartRateImage();

				m_mthInitMouthTemperatureCoverImage();
				m_mthInitAnusTemperatureCoverImage();
				m_mthInitArmpitTemperatureCoverImage();
			}
		}
		private Image m_imgPulse = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitPulseImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgPulse);			
			gphImage.FillEllipse(new SolidBrush(m_clrPulseSymbol),0,0,c_intGridHeight,c_intGridHeight);

			gphImage.Dispose();
		}
		public Image m_ImgPulse
		{
			get
			{
				return (Image)m_imgPulse.Clone();
			}
		}
		private Image m_imgHeartRate = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitHeartRateImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgHeartRate);
			gphImage.DrawEllipse(new Pen(m_clrPulseSymbol,2),0,0,c_intGridHeight-1,c_intGridHeight-1);

			gphImage.Dispose();			
		}
		public Image m_ImgHeartRate
		{
			get
			{
				return (Image)m_imgHeartRate.Clone();
			}
		}

		//外出
		private Image m_imgStayOut = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitStayOutImage()
		{
			PointF point1 = new PointF( 0.0F,  12.0F);
			PointF point2 = new PointF(6.0F,  0.0F);
			PointF point3 = new PointF(12F,   12.0F);
			
			PointF[] curvePoints ={point1,point2,point3};

			Graphics gphImage = Graphics.FromImage(m_imgStayOut);
			gphImage.DrawPolygon(new Pen(m_clrStayOutSymbol,2),curvePoints);

			//gphImage.DrawEllipse(new Pen(m_clrStayOutSymbol,2),0,0,c_intGridHeight-2,c_intGridHeight-2);

			gphImage.Dispose();			
		}


		//心率用蓝色的圈表示,比肛表圈要小一些.以区分
		private Image m_imgBreath = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitBreathImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgBreath);
			gphImage.DrawEllipse(new Pen(m_clrBreathSymbol,2),0,0,c_intGridHeight-2,c_intGridHeight-2);

			gphImage.Dispose();			
		}

		private Color m_clrTemperatureSymbol = Color.Yellow;
		public Color m_ClrTemperatureSymbol
		{
			get
			{
				return m_clrTemperatureSymbol;
			}
			set
			{
				m_clrTemperatureSymbol = value;

				m_mthInitMouthTemperatureImage();
				m_mthInitArmpitTemperatureImage();
				m_mthInitAnusTemperatureImage();

				m_mthInitMouthTemperatureCoverImage();
				m_mthInitAnusTemperatureCoverImage();
				m_mthInitArmpitTemperatureCoverImage();
			}
		}
		private Image m_imgMouthTemperature = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitMouthTemperatureImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgMouthTemperature);
			gphImage.FillEllipse(new SolidBrush(m_clrTemperatureSymbol),0,0,c_intGridHeight,c_intGridHeight);

			gphImage.Dispose();			
		}
		public Image m_ImgMouthTemperature
		{
			get
			{
				return (Image)m_imgMouthTemperature.Clone();
			}
		}
		private Image m_imgArmpitTemperature = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitArmpitTemperatureImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgArmpitTemperature);
			Pen penLine = new Pen(m_clrTemperatureSymbol,2);
			gphImage.DrawLine(penLine,0,0,c_intGridHeight,c_intGridHeight);
			gphImage.DrawLine(penLine,0,c_intGridHeight,c_intGridHeight,0);

			penLine.Dispose();
			gphImage.Dispose();			
		}
		public Image m_ImgArmpitTemperature
		{
			get
			{
				return (Image)m_imgArmpitTemperature.Clone();
			}
		}
		private Image m_imgAnusTemperature = new Bitmap(c_intGridHeight,c_intGridHeight);
		private void m_mthInitAnusTemperatureImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgAnusTemperature);
			gphImage.DrawEllipse(new Pen(m_clrTemperatureSymbol,2),0,0,c_intGridHeight-1,c_intGridHeight-1);

			gphImage.Dispose();			
		}
		public Image m_ImgAnusTemperature
		{
			get
			{
				return (Image)m_imgAnusTemperature.Clone();
			}
		}				
		#endregion

		/// <summary>
		/// 数字与罗马数字间的转换
		/// </summary>
		/// <param name="p_intTimes">数字</param>
		/// <returns></returns>
		private string m_strGetSpecialDateTimes(int p_intTimes)
		{
			if(p_intTimes <= 0)
				return "";

			string strResult="/"+p_intTimes.ToString();

//			string strResult = com.digitalwave.Utility.clsRomanNumeric.m_strDecToRoman(p_intTimes)+"-";
//
//			strResult = strResult.Replace("III","Ⅲ");
//			strResult = strResult.Replace("II","Ⅱ");
//			strResult = strResult.Replace("I","Ⅰ");

			return strResult;
		}

		private void ctlThreeMeasureRecord_Resize(object sender, System.EventArgs e)
		{
//			if(!m_blnCanResize)
//				return;
//
//			if(this.Height != m_intTotalHeight || this.Width != c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight)
//			{
//				m_blnCanResize = false;
//
//				this.Height = m_intTotalHeight;
//				this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;
//
//				m_blnCanResize = true;
//
//				this.//Invalidate();
//			}
		}
		
		#region 添加函数
		/// <summary>
		/// 添加新的日期信息
		/// </summary>
		/// <param name="p_dtmRecordDate">日期</param>
		public void m_mthAddRecordDate(DateTime p_dtmRecordDate)
		{
		    m_objAddRecordDate(p_dtmRecordDate);
		}
		/// <summary>
		/// 添加新的日期信息
		/// </summary>
		/// <param name="p_dtmRecordDate">日期</param>
		/// <returns></returns>
		private clsThreeMeasureDateRecord m_objAddRecordDate(DateTime p_dtmRecordDate)
		{
			bool blnIsAddNew;
			clsThreeMeasureDateRecord objRecord = m_objDateManager.m_objAddDateRecord(p_dtmRecordDate,out blnIsAddNew);

			clsThreeMeasureDateRecord objResultRecord = objRecord;

			if(blnIsAddNew)
			{
				/*
				 * 如果是在已有日期信息间插入，需重新计算脉搏和体温的坐标
				 */
				if(!m_objDateManager.m_blnIsLast(objRecord))
				{
					//处理插入			
					int intIndex = 0;
					for(;intIndex < m_objDateManager.m_IntRecordCount;intIndex++)
						if(objRecord == m_objDateManager[intIndex])
							break;

					intIndex++;
					for(;intIndex < m_objDateManager.m_IntRecordCount;intIndex++)
					{
						clsThreeMeasureDateRecord objNextRecord = m_objDateManager[intIndex];

						for(int i=0;i<objNextRecord.m_arlPulseValue.Count;i++)
						{
							clsThreeMeasurePulseValue objPulse = (clsThreeMeasurePulseValue)objNextRecord.m_arlPulseValue[i];

							objPulse.m_fltXPos += 6*c_intGridHeight;
						}

						for(int i=0;i<objNextRecord.m_arlTemperatureValue.Count;i++)
						{
							clsThreeMeasureTemperatureValue objTemperature = (clsThreeMeasureTemperatureValue)objNextRecord.m_arlTemperatureValue[i];

							objTemperature.m_fltXPos += 6*c_intGridHeight;
						}
					}
				}

				//修改控件宽度
				if(m_objDateManager.m_IntRecordCount > m_intTotalDate)
				{
					m_intTotalDate += 2;

					m_blnCanResize = false;

//					this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

					m_blnCanResize = true;
				}

				//自动添加手术日期
				clsThreeMeasureSpecialDate objSpecialDate = new clsThreeMeasureSpecialDate();
				objSpecialDate.m_blnIsNewStart = false;
				objSpecialDate.m_dtmSpecialDate = p_dtmRecordDate.Date;

				m_blnAddSpecialDate(objSpecialDate);
			}

			return objResultRecord;
		}		

		/// <summary>
		/// 添加手术日期
		/// </summary>
		/// <param name="p_objValue">手术日期</param>
		/// <returns></returns>
		public bool m_blnAddSpecialDate(clsThreeMeasureSpecialDate p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmSpecialDate];

			if(objRecord == null || (objRecord.m_objSpecialDate != null && objRecord.m_objSpecialDate.m_blnIsNewStart))
				return false;

			objRecord.m_objSpecialDate = p_objValue;

			return true;
		}

		/// <summary>
		/// 添加脉搏
		/// </summary>
		/// <param name="p_objValue">脉搏信息</param>
		/// <returns></returns>
        public bool m_blnAddPulseValue(clsThreeMeasurePulseValue p_objValue)
        {
            if (p_objValue == null)
                return false;

            int intIndex = m_objDateManager.m_intIndexOf(p_objValue.m_dtmValueTime.Date);

            if (intIndex < 0)
                return false;

            p_objValue.m_fltXPos = c_intTextTotleWidth + intIndex * c_intGridHeight * 6 + (float)p_objValue.m_dtmValueTime.TimeOfDay.TotalSeconds / (4 * 3600) * c_intGridHeight;
            if (p_objValue.m_intValue > 20)
                p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(180 - p_objValue.m_intValue) / 180f) * (float)(c_intGridHeight * c_intGridHeightCount);
            else
                p_objValue.m_fltYPos = c_intGridTotalHeight-28*2 - 3;
            switch (p_objValue.m_enmType)
            {
                case enmThreeMeasurePulseType.脉搏:
                    p_objValue.m_imgSymbol = m_imgPulse;
                    break;
                case enmThreeMeasurePulseType.心率:
                    p_objValue.m_imgSymbol = m_imgHeartRate;
                    break;
            }

            //添加到脉搏数据集（数据集有能力保持数据间的顺序）
            bool blnOK = m_objPulseValueManager.m_blnAddValue(p_objValue);

            if (blnOK)
            {
                p_objValue.m_objDeleteInfo = null;
                m_mthUpdateListByPulse(p_objValue);
            }

            return blnOK;
        }
		/// <summary>
		/// 更新重叠的设置
		/// </summary>
		/// <param name="p_objValue">脉搏</param>
		private void m_mthUpdateListByPulse(clsThreeMeasurePulseValue p_objValue)
		{
			while(m_objTemperatureValueManager.m_blnNext())
			{
				clsThreeMeasureTemperatureValue objTemperature = m_objTemperatureValueManager.m_ObjCurrent;

				if(Math.Abs(objTemperature.m_fltXPos-p_objValue.m_fltXPos) <= c_fltTwoSymbolDiff
					&& Math.Abs(objTemperature.m_fltYPos-p_objValue.m_fltYPos) <= c_fltTwoSymbolDiff)
				{
					//重叠
					m_mthHandleCover(p_objValue,objTemperature);
					break;
				}
			}

			m_objTemperatureValueManager.m_mthReset();
		}

		/// <summary>
		/// 添加体温
		/// </summary>
		/// <param name="p_objValue">体温</param>
		/// <returns></returns>
		public bool m_blnAddTemperatureValue(clsThreeMeasureTemperatureValue p_objValue)
		{
			if(p_objValue == null)
				return false;
			
			int intIndex = m_objDateManager.m_intIndexOf(p_objValue.m_dtmValueTime.Date);

			if(intIndex < 0)
				return false;
			
			if(p_objValue.m_fltValue >= 35f)
			{
				p_objValue.m_fltXPos = c_intTextTotleWidth + intIndex*c_intGridHeight*6+(float)p_objValue.m_dtmValueTime.TimeOfDay.TotalSeconds/(4*3600)*c_intGridHeight;
				p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);

				switch(p_objValue.m_enmType)
				{
					case enmThreeMeasureTemperatureType.口表温度:
						p_objValue.m_imgSymbol = m_imgMouthTemperature;
						break;
					case enmThreeMeasureTemperatureType.肛表温度:
						p_objValue.m_imgSymbol = m_imgAnusTemperature;
						break;
					case enmThreeMeasureTemperatureType.腋表温度:
						p_objValue.m_imgSymbol = m_imgArmpitTemperature;
						break;
				}
			}
			else
			{
				p_objValue.m_fltXPos = c_intTextTotleWidth+c_intGridHeight*(intIndex*6+(int)(p_objValue.m_dtmValueTime.TimeOfDay.TotalSeconds)/(4*3600))-1;				
			}

			//添加到体温数据集（数据集有能力保持数据间的顺序）
			bool blnOK = m_objTemperatureValueManager.m_blnAddValue(p_objValue);

			if(blnOK && p_objValue.m_fltValue >= 35f)
			{
				p_objValue.m_objDeleteInfo = null;
				m_mthUpdateListByTemperature(p_objValue);
			}

			return blnOK;
		}
		/// <summary>
		/// 更新重叠的设置
		/// </summary>
		/// <param name="p_objValue">体温</param>
		private void m_mthUpdateListByTemperature(clsThreeMeasureTemperatureValue p_objValue)
		{
			while(m_objPulseValueManager.m_blnNext())
			{
				clsThreeMeasurePulseValue objPulse = m_objPulseValueManager.m_ObjCurrent;

				if(Math.Abs(objPulse.m_fltXPos-p_objValue.m_fltXPos) <= c_fltTwoSymbolDiff
					&& Math.Abs(objPulse.m_fltYPos-p_objValue.m_fltYPos) <= c_fltTwoSymbolDiff)
				{
					//重叠
					m_mthHandleCover(objPulse,p_objValue);
					break;
				}
			}

			m_objPulseValueManager.m_mthReset();
		}

		/// <summary>
		/// 处理脉搏与体温重叠
		/// </summary>
		/// <param name="p_objPulse">脉搏</param>
		/// <param name="p_objTemperature">体温</param>
		private void m_mthHandleCover(clsThreeMeasurePulseValue p_objPulse,clsThreeMeasureTemperatureValue p_objTemperature)
		{
			/*
			 * 只修改体温的图标，脉搏的图标不修改，但画图时不画出来
			 */

			switch(p_objTemperature.m_enmType)
			{
				case enmThreeMeasureTemperatureType.口表温度:
					p_objTemperature.m_imgSymbol = m_imgMouthTemperatureCover;
					break;
				case enmThreeMeasureTemperatureType.肛表温度:
					p_objTemperature.m_imgSymbol = m_imgAnusTemperatureCover;
					break;
				case enmThreeMeasureTemperatureType.腋表温度:
					p_objTemperature.m_imgSymbol = m_imgArmpitTemperatureCover;
					break;
			}
		
			//设置ID，画图时只画ID为 int.MinValue 的图标
			int intCoverID = s_IntCoverID;
			p_objPulse.m_intCoverID = intCoverID;
			p_objTemperature.m_intCoverID = intCoverID;
		}

		/// <summary>
		/// 添加物理降温
		/// </summary>
		/// <param name="p_objValue">物理降温</param>
		/// <param name="p_intDownMinutes">物理降温后的测试时间</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,int p_intDownMinutes)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValue(p_objValue,p_intDownMinutes);
		}
		
		/// <summary>
		/// 添加物理降温
		/// </summary>
		/// <param name="p_objValue">物理降温</param>
		/// <param name="p_objBase">物理降温对应的降温前的温度对象</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValue(p_objValue,p_objBase);
		}

		/// <summary>
		/// 添加物理降温
		/// </summary>
		/// <param name="p_objValue">物理降温</param>
		/// <param name="p_dtmBaseValueTime">物理降温对应的降温前的时间</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,DateTime p_dtmBaseValueTime)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValue(p_objValue,p_dtmBaseValueTime);
		}

		/// <summary>
		/// 往最后的温度对象添加物理降温
		/// </summary>
		/// <param name="p_objValue">物理降温</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValueToLast(clsThreeMeasureTemperaturePhyscalDownValue p_objValue)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValueToLast(p_objValue);
		}

		/// <summary>
		/// 添加事件
		/// </summary>
		/// <param name="p_objEvent">事件</param>
		/// <returns></returns>
		public bool m_blnAddEvent(clsThreeMeasureEvent p_objEvent)
		{
			if(p_objEvent == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objEvent.m_dtmEventTime.Date];

			if(objRecord == null)
				return false;
			//处理时间在0～2小时的情况
			if(p_objEvent.m_dtmEventTime.AddHours(-2).Date != p_objEvent.m_dtmEventTime.Date)
			{
				p_objEvent.m_intTimeIndex = 0;
				p_objEvent.m_intNearTimeIndex = 1;
			}
			else
			{
				p_objEvent.m_intTimeIndex = (int)(p_objEvent.m_dtmEventTime.AddHours(-2).TimeOfDay.TotalSeconds-60)/(4*3600);

				//计算最近的位置，在事件重叠时使用
                //if((p_objEvent.m_dtmEventTime.Hour%6)%4 < 2)
                //    p_objEvent.m_intNearTimeIndex = p_objEvent.m_intTimeIndex - 1;
                //else
                //    p_objEvent.m_intNearTimeIndex = p_objEvent.m_intTimeIndex + 1;
			}
			if(p_objEvent.m_enmEventType != enmThreeMeasureEventType.手术
				&& p_objEvent.m_enmEventType != enmThreeMeasureEventType.请假)
			{
				p_objEvent.m_mthSetTimeString();
			}
			else
			{
				p_objEvent.m_strTime = "";
			}

			p_objEvent.m_objDeleteInfo = null;

			objRecord.m_arlEvent.Add(p_objEvent);
			objRecord.m_arlEvent.Sort();
            m_mthResetEventTimeIndex(ref objRecord.m_arlEvent);

            if (p_objEvent.m_enmEventType == enmThreeMeasureEventType.手术)
            {
                if (!arlDate.Contains(p_objEvent.m_dtmEventTime))
                    arlDate.Add(p_objEvent.m_dtmEventTime);
            }
			return true;
		}
        /// <summary>
        /// 重叠的位置按时间排序显示，第二个事件右移一格，以此类推，右可能和右边的其他事件重叠
        /// 有位置超过pm12 的前面这天所有事件往前挪，到am4格止；
        /// 一天超过7个事件，统一在am4格重叠。
        /// </summary>
        /// <param name="p_arlEvent"></param>
        void m_mthResetEventTimeIndex(ref ArrayList p_arlEvent)
        {
            if (p_arlEvent.Count > 0)
            {
                List<int> lstIndex = new List<int>(6);
                lstIndex.Add(((clsThreeMeasureEvent)p_arlEvent[0]).m_intTimeIndex);
                for (int i = 1 ; i < p_arlEvent.Count ; i++)
                {
                    for (int j = 0 ; j < i ; j++)
                    {
                        if (((clsThreeMeasureEvent)p_arlEvent[i]).m_intTimeIndex == lstIndex[j])
                        {
                            lstIndex.Add(lstIndex[lstIndex.Count - 1] + 1);
                            break;
                        }
                    }
                    if (lstIndex.Count == i)
                    {
                        lstIndex.Add(((clsThreeMeasureEvent)p_arlEvent[i]).m_intTimeIndex);
                    }
                    if (lstIndex[i] > 5)
                    {
                        lstIndex[i]--;
                        for (int k = i - 1 ; k >= 0 ; k--)
                        {
                            if (lstIndex[k] == lstIndex[k + 1])
                                lstIndex[k]--;
                        }
                    }
                }
                for (int m = 0 ; m < lstIndex.Count ; m++)
                {
                    if (lstIndex[m] < 0)
                        lstIndex[m] = 0;
                    ((clsThreeMeasureEvent)p_arlEvent[m]).m_intNearTimeIndex = lstIndex[m];
                }
            }
        }

		/// <summary>
		/// 添加呼吸
		/// </summary>
		/// <param name="p_objValue">呼吸</param>
		/// <returns></returns>
		public bool m_blnAddBreath(clsThreeMeasureBreathValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmBreathTime.Date];

			if(objRecord == null)
				return false;

			int intNewTimeIndex = (int)(p_objValue.m_dtmBreathTime.TimeOfDay.TotalSeconds)/(4*3600);

			int intMaxIndex = 0;

			for(int i=0;i<objRecord.m_arlBreath.Count;i++)
			{
				clsThreeMeasureBreathValue objValue = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[i];

				if(objValue.m_intTimeIndex == intNewTimeIndex)
				{
					if(objValue.m_objDeleteInfo == null)
						return false;
					else
					{
						intMaxIndex++;
					}
				}
			}

			//重新计算呼吸内容的高度
			if(intMaxIndex+1 > m_intMaxBreathCount)
			{
				int intBreathHeight = 14;
				
				c_intBreathHeight += intBreathHeight;

				c_intBreathTotalHeight += intBreathHeight;
				c_intInputTotalHeight += intBreathHeight;
				c_intDejectaTotalHeight += intBreathHeight;
				c_intPeeTotalHeight += intBreathHeight;
				c_intOutStreamTotalHeight += intBreathHeight;
				c_intPressureTotalHeight += intBreathHeight;
				c_intWeightTotalHeight += intBreathHeight;
				c_intSkinTestTotalHeight += intBreathHeight;
				c_intOtherTotalHeight +=intBreathHeight;
				m_intTotalHeight += intBreathHeight;

//				this.Height = m_intTotalHeight;

				m_intMaxBreathCount = intMaxIndex+1;	
			}
			
			p_objValue.m_objDeleteInfo = null;
			p_objValue.m_intTimeIndex = intNewTimeIndex;
			p_objValue.m_intAddSeq = intMaxIndex;
				
			objRecord.m_arlBreath.Add(p_objValue);
			objRecord.m_arlBreath.Sort();

			return true;
		}
		
		/// <summary>
		/// 添加输入液量
		/// </summary>
		/// <param name="p_objValue">输入液量</param>
		/// <returns></returns>
		public bool m_blnAddInput(clsThreeMeasureInputValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmInputDate];

			if(objRecord == null)
				return false;

			if(objRecord.m_arlInputValue.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlInputValue.Add(p_objValue);

				return true;
			}
			else
			{
				clsThreeMeasureInputValue objValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[objRecord.m_arlInputValue.Count-1];

				if(objValue.m_objDeleteInfo == null)
					return false;
				else
				{
					//重新计算输入液量内容的高度
					if(objRecord.m_arlInputValue.Count+1 > m_intMaxInputCount)
					{
						int intInputHeight = 21;
				
						c_intInputHeight += intInputHeight;

						c_intInputTotalHeight += intInputHeight;
						c_intDejectaTotalHeight += intInputHeight;
						c_intPeeTotalHeight += intInputHeight;
						c_intOutStreamTotalHeight += intInputHeight;
						c_intPressureTotalHeight += intInputHeight;
						c_intWeightTotalHeight += intInputHeight;
						c_intSkinTestTotalHeight += intInputHeight;
						c_intOtherTotalHeight +=intInputHeight;
						m_intTotalHeight += intInputHeight;

//						this.Height = m_intTotalHeight;

						m_intMaxInputCount = objRecord.m_arlInputValue.Count+1;	
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlInputValue.Add(p_objValue);

					return true;
				}
			}				
		}

		/// <summary>
		/// 添加大便
		/// </summary>
		/// <param name="p_objValue">大便</param>
		/// <returns></returns>
		public bool m_blnAddDejecta(clsThreeMeasureDejectaValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmDejectaDate];

			if(objRecord == null)
				return false;

			if(objRecord.m_arlDejectaValue.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlDejectaValue.Add(p_objValue);
			}
			else
			{
				clsThreeMeasureDejectaValue objValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[objRecord.m_arlDejectaValue.Count-1];

				if(objValue.m_objDeleteInfo == null)
					return false;
				else
				{
					//重新计算大便内容的高度
					if(objRecord.m_arlDejectaValue.Count+1 > m_intMaxDejectaCount)
					{
						int intDejectaHeight = 21;
				
						c_intDejectaHeight += intDejectaHeight;

						c_intDejectaTotalHeight += intDejectaHeight;
						c_intPeeTotalHeight += intDejectaHeight;
						c_intOutStreamTotalHeight += intDejectaHeight;
						c_intPressureTotalHeight += intDejectaHeight;
						c_intWeightTotalHeight += intDejectaHeight;
						c_intSkinTestTotalHeight += intDejectaHeight;
						c_intOtherTotalHeight =+intDejectaHeight;
						m_intTotalHeight += intDejectaHeight;

//						this.Height = m_intTotalHeight;

						m_intMaxDejectaCount = objRecord.m_arlDejectaValue.Count+1;	
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlDejectaValue.Add(p_objValue);
				}
			}	

			//设置大便值的显示
			if(!p_objValue.m_blnCanDejecta)
			{
				p_objValue.m_strDesc = "      *";
			}
			else if(p_objValue.m_blnNeedWeight)
			{
				p_objValue.m_strDesc = ((int)(p_objValue.m_intBeforeTimes+p_objValue.m_intAfterTimes)).ToString()+"/"+p_objValue.m_fltWeight.ToString("0.0g");
			}
			else if(p_objValue.m_intClysisTimes <= 0)
			{
				p_objValue.m_strDesc = p_objValue.m_intBeforeTimes.ToString();
			}
			else if(p_objValue.m_intBeforeTimes > 0)
			{
				if(!p_objValue.m_blnAfterMoreTimes)
					p_objValue.m_strDesc = p_objValue.m_intBeforeTimes.ToString()+"  "+p_objValue.m_intAfterTimes.ToString()+"/E";
				else
					p_objValue.m_strDesc = p_objValue.m_intBeforeTimes.ToString()+"  "+"*/E";
			}
			else
			{
				if(p_objValue.m_intClysisTimes == 1)
				{
					if(!p_objValue.m_blnAfterMoreTimes)
						p_objValue.m_strDesc = p_objValue.m_intAfterTimes.ToString()+"/E";
					else
						p_objValue.m_strDesc = "*/E";
				}
				else
				{
					if(!p_objValue.m_blnAfterMoreTimes)
						p_objValue.m_strDesc = p_objValue.m_intAfterTimes.ToString()+"/"+p_objValue.m_intClysisTimes.ToString("0E");
					else
						p_objValue.m_strDesc = "*/"+p_objValue.m_intClysisTimes.ToString("0E");
				}
			}

			return true;
		}

		/// <summary>
		/// 添加尿量
		/// </summary>
		/// <param name="p_objValue">尿量</param>
		/// <returns></returns>
		public bool m_blnAddPee(clsThreeMeasurePeeValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPeeDate];

			if(objRecord == null)
				return false;

			if(objRecord.m_arlPeeValue.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlPeeValue.Add(p_objValue);
				return true;
			}
			else
			{
				clsThreeMeasurePeeValue objValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[objRecord.m_arlPeeValue.Count-1];

				if(objValue.m_objDeleteInfo == null)
					return false;
				else
				{
					//重新计算尿量内容的高度
					if(objRecord.m_arlPeeValue.Count+1 > m_intMaxPeeCount)
					{
						int intPeeHeight = 21;
				
						c_intPeeHeight += intPeeHeight;

						c_intPeeTotalHeight += intPeeHeight;
						c_intPressureTotalHeight += intPeeHeight;
						c_intWeightTotalHeight += intPeeHeight;
						c_intSkinTestTotalHeight += intPeeHeight;
						c_intOtherTotalHeight +=intPeeHeight;
						m_intTotalHeight += intPeeHeight;

//						this.Height = m_intTotalHeight;
					
						m_intMaxPeeCount = objRecord.m_arlPeeValue.Count+1;	
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlPeeValue.Add(p_objValue);

					return true;
				}
			}	
		}

		/// <summary>
		/// 小便
		/// </summary>
		/// <param name="p_objValue">小便</param>
		/// <returns></returns>
		public bool m_blnAddOutStream(clsThreeMeasureOutStreamValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOutStreamDate];

			if(objRecord == null)
				return false;

			if(objRecord.m_arlOutStreamValue.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlOutStreamValue.Add(p_objValue);
				return true;
			}
			else
			{
				clsThreeMeasureOutStreamValue objValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[objRecord.m_arlOutStreamValue.Count-1];

				if(objValue.m_objDeleteInfo == null)
					return false;
				else
				{
					//重新计算小便内容的高度
					if(objRecord.m_arlOutStreamValue.Count+1 > m_intMaxOutStreamCount)
					{
						int intOutStreamHeight = 21;
				
						c_intOutStreamHeight += intOutStreamHeight;
						c_intOutStreamTotalHeight += intOutStreamHeight;

						c_intPeeTotalHeight += intOutStreamHeight;
						c_intPressureTotalHeight += intOutStreamHeight;
						c_intWeightTotalHeight += intOutStreamHeight;
						c_intSkinTestTotalHeight += intOutStreamHeight;
						c_intOtherTotalHeight += intOutStreamHeight;
						m_intTotalHeight += intOutStreamHeight;

//						this.Height = m_intTotalHeight;
					
						m_intMaxOutStreamCount = objRecord.m_arlOutStreamValue.Count+1;	
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlOutStreamValue.Add(p_objValue);

					return true;
				}
			}	
		}

		/// <summary>
		/// 添加血压
		/// </summary>
		/// <param name="p_objValue">血压</param>
		/// <returns></returns>
		public bool m_blnAddPressure(clsThreeMeasurePressureValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPressureDate];

			if(objRecord == null)
				return false;

			//第一次没有值。添加到第一次
			if(objRecord.m_arlPressureValue1.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlPressureValue1.Add(p_objValue);
				return true;
			}
			else
			{
				clsThreeMeasurePressureValue objValue1 = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[objRecord.m_arlPressureValue1.Count-1];

				//第一次有值，但已被删除。添加到第一次，并调整高度
				if(objValue1.m_objDeleteInfo != null)
				{
					p_objValue.m_objDeleteInfo = null;
					objRecord.m_arlPressureValue1.Add(p_objValue);					
				}
				else
				{
					//第一次有值，且该值有效；第二次没值。添加到第二次。
					if(objRecord.m_arlPressureValue2.Count <= 0)
					{
						p_objValue.m_objDeleteInfo = null;
						objRecord.m_arlPressureValue2.Add(p_objValue);
						return true;
					}
					else
					{
						clsThreeMeasurePressureValue objValue2 = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[objRecord.m_arlPressureValue2.Count-1];

						//第一次有值，且该值有效；第二次有值，但已被删除。添加到第二次，并调整高度。
						if(objValue2.m_objDeleteInfo != null)
						{
							p_objValue.m_objDeleteInfo = null;
							objRecord.m_arlPressureValue2.Add(p_objValue);	
						}
						else
						{
							//第一次有值，且该值有效；第二次有值，且该值有效。不能添加。
							return false;
						}
					}
				}

			}

			//重新计算血压内容的高度
			/*
			 * 1、取第一次和第二次中最大的个数
			 * 2、与当前最大的血压行最大的个数比较，如果比整行的大就更新高度
			 */
			int intPressureCount = objRecord.m_arlPressureValue1.Count;
			if(intPressureCount < objRecord.m_arlPressureValue2.Count)
				intPressureCount = objRecord.m_arlPressureValue2.Count;

			if(intPressureCount > m_intMaxPressureCount)
			{
				int intPressureHeight = 15;
				
				c_intPressureHeight += intPressureHeight;
				c_intPressureTotalHeight += intPressureHeight;
				c_intWeightTotalHeight += intPressureHeight;
				c_intSkinTestTotalHeight += intPressureHeight;
				c_intOtherTotalHeight+=intPressureHeight;
				m_intTotalHeight += intPressureHeight;

//				this.Height = m_intTotalHeight;

				m_intMaxPressureCount = objRecord.m_arlPressureValue1.Count+1;	

			}

			return true;
		}
		
		/// <summary>
		/// 添加体重
		/// </summary>
		/// <param name="p_objValue">体重</param>
		/// <returns></returns>
		public bool m_blnAddWeight(clsThreeMeasureWeightValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmWeightDate];

			if(objRecord == null)
				return false;

			if(objRecord.m_arlWeightValue.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlWeightValue.Add(p_objValue);
				return true;
			}
			else
			{
				clsThreeMeasureWeightValue objValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[objRecord.m_arlWeightValue.Count-1];

				if(objValue.m_objDeleteInfo == null)
					return false;
				else
				{
					//重新计算体重内容的高度
					if(objRecord.m_arlWeightValue.Count+1 > m_intMaxWeightCount)
					{
						int intWeightHeight = 21;
				
						c_intWeightHeight += intWeightHeight;
						c_intWeightTotalHeight += intWeightHeight;
						c_intSkinTestTotalHeight += intWeightHeight;
						c_intOtherTotalHeight+= intWeightHeight;
						m_intTotalHeight += intWeightHeight;

//						this.Height = m_intTotalHeight;

						m_intMaxWeightCount = objRecord.m_arlWeightValue.Count+1;						 
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlWeightValue.Add(p_objValue);

					return true;
				}
			}		
		}
		
		/// <summary>
		/// 添加皮试
		/// </summary>
		/// <param name="p_objValue">皮试</param>
		/// <returns></returns>
		public bool m_blnAddSkinTest(clsThreeMeasureSkinTestValue p_objValue)
		{
			if(p_objValue == null || p_objValue.m_strMedicineName == null ||p_objValue.m_strMedicineName == "")
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmSkinTestDate];

			if(objRecord == null)
				return false;		
	
			int intPPDCount = 0;
			for(int i=0;i<objRecord.m_arlSkinTestValue.Count;i++)
			{
				clsThreeMeasureSkinTestValue objValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[i];

				if(objValue.m_strMedicineName.ToLower().IndexOf("ppd") >= 0)
					intPPDCount++;
			}

			if(p_objValue.m_strMedicineName.ToLower().IndexOf("ppd") >= 0)
			{
				p_objValue.m_intTimeIndex = intPPDCount;

				p_objValue.m_strPDDValue = "(";
				for(int i=0;i<p_objValue.m_intBadCount;i++)
				{
					p_objValue.m_strPDDValue +="+";
				}
				p_objValue.m_strPDDValue += ")";
			}
			else 
			{
				p_objValue.m_intTimeIndex = -1;
				
				//重新计算皮试内容的高度
				if(objRecord.m_arlSkinTestValue.Count-intPPDCount+1 > m_intMaxSkinTestCount)
				{
					int intSkinTestHeight = 21;
				
					c_intSkinTestHeight += intSkinTestHeight;
					c_intSkinTestTotalHeight += intSkinTestHeight;
					c_intOtherTotalHeight+=intSkinTestHeight;
					m_intTotalHeight += intSkinTestHeight;

//					this.Height = m_intTotalHeight;

					m_intMaxSkinTestCount = objRecord.m_arlSkinTestValue.Count-intPPDCount+1;
				}
			}
				
			objRecord.m_arlSkinTestValue.Add(p_objValue);	
		
			p_objValue.m_objDeleteInfo = null;

			return true;
		}

		private bool m_blnSetOther(string p_strOtherName)
		{
			if(m_strOtherName != null || p_strOtherName == null || p_strOtherName == "")
				return false;

			m_strOtherName = p_strOtherName;

			return true;
		}
		/// <summary>
		/// 添加其它
		/// </summary>
		/// <param name="p_objValue">其它</param>
		/// <returns></returns>
		public bool m_blnAddOther(clsThreeMeasureOtherValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOtherDate];

			if(objRecord == null)
				return false;

			//重新计算其它内容的高度
			if(objRecord.m_arlOtherValue.Count+1 > m_intMaxOtherCount)
			{
				int intOtherHeight = 21;
				
				c_intOtherHeight += intOtherHeight;
			
				m_intTotalHeight += intOtherHeight;

//				this.Height = m_intTotalHeight;				
				
				m_intMaxOtherCount = objRecord.m_arlOtherValue.Count+1;
			}

			p_objValue.m_strOther = "";

			if(m_strOtherName == null)
			{
				for(int i=0;i<=p_objValue.m_strOtherItem.Length/4;i++)
				{
					if((i+1)*4 < p_objValue.m_strOtherItem.Length)
					{
						p_objValue.m_strOther += p_objValue.m_strOtherItem.Substring(i*4,(i+1)*4)+"\r\n";
					}
					else
					{
						p_objValue.m_strOther += p_objValue.m_strOtherItem.Substring(i*4)+":\r\n";
					}
				}	
			}
			
			p_objValue.m_strOther += p_objValue.m_StrOtherValue;

			p_objValue.m_objDeleteInfo = null;
			objRecord.m_arlOtherValue.Add(p_objValue);
			
			return true;
		}
		#endregion

		#region 删除函数
		/// <summary>
		/// 删除日期信息
		/// </summary>
		/// <param name="p_objRecord">日期信息</param>
		/// <returns></returns>
		public bool m_blnDeleteRecordDate(clsThreeMeasureDateRecord p_objRecord)
		{
			bool blnIsLast = m_objDateManager.m_blnIsLast(p_objRecord);

			if(!blnIsLast && m_objDateManager.m_blnContain(p_objRecord))
			{
				//非尾部删除			
				int intIndex = 0;
				for(;intIndex < m_objDateManager.m_IntRecordCount;intIndex++)
					if(p_objRecord == m_objDateManager[intIndex])
						break;

				intIndex++;
				for(;intIndex < m_objDateManager.m_IntRecordCount;intIndex++)
				{
					clsThreeMeasureDateRecord objNextRecord = m_objDateManager[intIndex];

					for(int i=0;i<objNextRecord.m_arlPulseValue.Count;i++)
					{
						clsThreeMeasurePulseValue objPulse = (clsThreeMeasurePulseValue)objNextRecord.m_arlPulseValue[i];

						objPulse.m_fltXPos -= 6*c_intGridHeight;
					}

					for(int i=0;i<objNextRecord.m_arlTemperatureValue.Count;i++)
					{
						clsThreeMeasureTemperatureValue objTemperature = (clsThreeMeasureTemperatureValue)objNextRecord.m_arlTemperatureValue[i];

						objTemperature.m_fltXPos -= 6*c_intGridHeight;
					}
				}
			}

			bool blnOk = m_objDateManager.m_blnRemove(p_objRecord);

			if(!blnOk)
				return false;

			//删除日期信息里的脉搏数据
			for(int i=p_objRecord.m_arlPulseValue.Count;i>0;i--)
			{
				clsThreeMeasurePulseValue objPulse = (clsThreeMeasurePulseValue)p_objRecord.m_arlPulseValue[0];

				m_objPulseValueManager.m_mthRemoveValue(objPulse,p_objRecord);

				m_mthHandleRecoverByPulse(objPulse.m_intCoverID);
			}

			//删除日期信息里的体温数据
			for(int i=p_objRecord.m_arlTemperatureValue.Count;i>0;i--)
			{				
				m_objTemperatureValueManager.m_mthRemoveValue((clsThreeMeasureTemperatureValue)p_objRecord.m_arlTemperatureValue[0],p_objRecord);
			}

			//修改控件宽度
			if(m_intTotalDate != 7 && m_objDateManager.m_IntRecordCount <= m_intTotalDate-2)
			{
				m_intTotalDate -= 2;

				m_blnCanResize = false;

//				this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

				m_blnCanResize = true;
			}

			return true;
		}
//
//		/// <summary>
//		/// 删除手术日期
//		/// </summary>
//		/// <param name="p_objValue">手术日期</param>
//		/// <returns></returns>
//		public bool m_blnDeleteSpecialDate(clsThreeMeasureSpecialDate p_objValue)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmSpecialDate];
//
//			if(objRecord == null 
//				|| objRecord.m_objSpecialDate == null 
//				|| objRecord.m_objSpecialDate != p_objValue)
//				return false;
//
//			objRecord.m_objSpecialDate.m_blnIsNewStart = false;
//
//			return true;
//		}
//
//		/// <summary>
//		/// 删除脉搏
//		/// </summary>
//		/// <param name="p_objValue">脉搏</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeletePulseValue(clsThreeMeasurePulseValue p_objValue,bool p_blnInControl)
//		{
//			if(!p_blnInControl)
//			{
//				//直接去掉
//				bool blnOk = m_objPulseValueManager.m_blnRemoveValue(p_objValue);
//
//				if(!blnOk)
//					return false;
//
//				m_mthHandleRecoverByPulse(p_objValue.m_intCoverID);
//			}
//			else
//			{
//                //设置删除者
//				if(p_objValue.m_objDeleteInfo != null)
//					return false;
//
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				p_objValue.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
		/// <summary>
		/// 恢复被重叠的图标
		/// </summary>
		/// <param name="p_intCoverID">重叠号</param>
		private void m_mthHandleRecoverByPulse(int p_intCoverID)
		{
			if(p_intCoverID == int.MinValue)
				return;

			while(m_objTemperatureValueManager.m_blnNext())
			{
				clsThreeMeasureTemperatureValue objTemperature
					= m_objTemperatureValueManager.m_ObjCurrent;

				if(objTemperature.m_intCoverID == p_intCoverID)
				{
					objTemperature.m_intCoverID = int.MinValue;

					switch(objTemperature.m_enmType)
					{
						case enmThreeMeasureTemperatureType.口表温度:
							objTemperature.m_imgSymbol = m_imgMouthTemperature;
							break;
						case enmThreeMeasureTemperatureType.肛表温度:
							objTemperature.m_imgSymbol = m_imgAnusTemperature;
							break;
						case enmThreeMeasureTemperatureType.腋表温度:
							objTemperature.m_imgSymbol = m_imgArmpitTemperature;
							break;
					}

					break;
				}
			}
		}
//
//		/// <summary>
//		/// 删除体温
//		/// </summary>
//		/// <param name="p_objValue">体温</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteTemperatureValue(clsThreeMeasureTemperatureValue p_objValue,bool p_blnInControl)
//		{
//			if(!p_blnInControl)
//			{
//				//直接去掉
//				bool blnOk = m_objTemperatureValueManager.m_blnRemoveValue(p_objValue);
//
//				if(!blnOk)
//					return false;
//
//				m_mthHandleRecoverByTemperature(p_objValue.m_intCoverID);
//			}
//			else
//			{
//				//设置删除者
//				if(p_objValue.m_objDeleteInfo != null)
//					return false;
//
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				p_objValue.m_objDeleteInfo = objDeleteInfo;
//				
//				p_objValue.m_arlPhyscalDownValue.Clear();
//			}
//
//			return true;
//		}
		/// <summary>
		/// 恢复被重叠的图标
		/// </summary>
		/// <param name="p_intCoverID">重叠号</param>
		private void m_mthHandleRecoverByTemperature(int p_intCoverID)
		{
			if(p_intCoverID == int.MinValue)
				return;

			while(m_objPulseValueManager.m_blnNext())
			{
				clsThreeMeasurePulseValue objPulse
					= m_objPulseValueManager.m_ObjCurrent;

				if(objPulse.m_intCoverID == p_intCoverID)
				{
					objPulse.m_intCoverID = int.MinValue;
					break;
				}
			}
		}
//
//		/// <summary>
//		/// 删除物理降温
//		/// </summary>
//		/// <param name="p_objValue">物理降温</param>
//		/// <param name="p_objBase">物理降温相应的温度信息</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeletePhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase,bool p_blnInControl)
//		{
//			if(!p_blnInControl)
//			{
//				//直接去掉
//				return m_objTemperatureValueManager.m_blnRemovePhyscalDownValue(p_objValue,p_objBase);
//			}
//			else
//			{
//				//设置删除者
//				if(p_objValue.m_objDeleteInfo != null)
//					return false;
//
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				p_objValue.m_objDeleteInfo = objDeleteInfo;
//
//				return true;
//			}
//		}
//
//		/// <summary>
//		/// 删除事件
//		/// </summary>
//		/// <param name="p_objEvent">事件</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteEvent(clsThreeMeasureEvent p_objEvent,bool p_blnInControl)
//		{
//			if(p_objEvent == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objEvent.m_dtmEventTime.Date];
//
//			if(objRecord == null)
//				return false;
//
//			if(!p_blnInControl)
//			{
//				//直接去掉
//				objRecord.m_arlEvent.Remove(p_objEvent);
//			}
//			else
//			{
//				//设置删除者
//				if(p_objEvent.m_objDeleteInfo != null)
//					return false;
//
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				p_objEvent.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
//
//		/// <summary>
//		/// 删除呼吸
//		/// </summary>
//		/// <param name="p_objValue">呼吸</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteBreath(clsThreeMeasureBreathValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmBreathTime.Date];
//
//			if(objRecord == null)
//				return false;
//
//			for(int i=0;i<objRecord.m_arlBreath.Count;i++)
//			{
//				clsThreeMeasureBreathValue objValue = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[i];
//
//				if(objValue == p_objValue)
//				{
//					if(objValue.m_objDeleteInfo != null)
//						return false;
//
//					if(!p_blnInControl)
//					{						
//						//直接去掉
//						objRecord.m_arlBreath.Remove(p_objValue);						
//					}
//					else
//					{
//						//设置删除者
//						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//						objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//						objDeleteInfo.m_strUserID = m_strUserID;
//						objDeleteInfo.m_strUserName = m_strUserName;
//
//						p_objValue.m_objDeleteInfo = objDeleteInfo;
//					}
//
//					return true;
//				}
//			}			
//
//			return false;
//		}
//		
//		/// <summary>
//		/// 删除输入液量
//		/// </summary>
//		/// <param name="p_objValue">输入液量</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteInput(clsThreeMeasureInputValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmInputDate];
//
//			if(objRecord == null || objRecord.m_arlInputValue.Count <= 0)
//				return false;			
//
//			clsThreeMeasureInputValue objDeleteValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[objRecord.m_arlInputValue.Count-1];
//
//			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
//				return false;
//
//			if(!p_blnInControl)
//			{
//				//直接去掉，但高度不重新计算
//				//				if(objRecord.m_arlInputValue.Count > 0)
//				//				{
//				//					int intInputHeight = 21;
//				//
//				//					c_intInputHeight -= intInputHeight;
//				//
//				//					c_intInputTotalHeight -= intInputHeight;
//				//					c_intDejectaTotalHeight -= intInputHeight;
//				//					c_intPeeTotalHeight -= intInputHeight;
//				//					c_intOutStreamTotalHeight -= intInputHeight;
//				//					c_intPressureTotalHeight -= intInputHeight;
//				//					c_intWeightTotalHeight -= intInputHeight;
//				//					c_intSkinTestTotalHeight -= intInputHeight;
//				//					m_intTotalHeight -= intInputHeight;
//				//					this.Height = m_intTotalHeight;
//				//				}
//
//				objRecord.m_arlInputValue.Remove(objDeleteValue);
//			}
//			else
//			{
//				//设置删除者
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
//
//		/// <summary>
//		/// 删除大便
//		/// </summary>
//		/// <param name="p_objValue">大便</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteDejecta(clsThreeMeasureDejectaValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmDejectaDate];
//
//			if(objRecord == null || objRecord.m_arlDejectaValue.Count <= 0)
//				return false;			
//
//			clsThreeMeasureDejectaValue objDeleteValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[objRecord.m_arlDejectaValue.Count-1];
//
//			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
//				return false;
//
//			if(!p_blnInControl)
//			{
//				//直接去掉，但高度不重新计算
//				//				if(objRecord.m_arlDejectaValue.Count > 0)
//				//				{
//				//					int intDejectaHeight = 21;
//				//
//				//					c_intDejectaHeight -= intDejectaHeight;
//				//
//				//					c_intDejectaTotalHeight -= intDejectaHeight;
//				//					c_intPeeTotalHeight -= intDejectaHeight;
//				//					c_intOutStreamTotalHeight -= intDejectaHeight;
//				//					c_intPressureTotalHeight -= intDejectaHeight;
//				//					c_intWeightTotalHeight -= intDejectaHeight;
//				//					c_intSkinTestTotalHeight -= intDejectaHeight;
//				//					m_intTotalHeight -= intDejectaHeight;
//				//					this.Height = m_intTotalHeight;
//				//				}
//
//				objRecord.m_arlDejectaValue.Remove(objDeleteValue);
//			}
//			else
//			{
//				//设置删除者
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
//
//		/// <summary>
//		/// 删除小便
//		/// </summary>
//		/// <param name="p_objValue">小便</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeletePee(clsThreeMeasurePeeValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPeeDate];
//
//			if(objRecord == null || objRecord.m_arlPeeValue.Count <= 0)
//				return false;			
//
//			clsThreeMeasurePeeValue objDeleteValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[objRecord.m_arlPeeValue.Count-1];
//
//			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
//				return false;
//
//			if(!p_blnInControl)
//			{
//				//直接去掉，但高度不重新计算
//				//				if(objRecord.m_arlPeeValue.Count > 0)
//				//				{
//				//					int intPeeHeight = 21;
//				//
//				//					c_intPeeHeight -= intPeeHeight;
//				//
//				//					c_intPeeTotalHeight -= intPeeHeight;
//				//					c_intOutStreamTotalHeight -= intPeeHeight;
//				//					c_intPressureTotalHeight -= intPeeHeight;
//				//					c_intWeightTotalHeight -= intPeeHeight;
//				//					c_intSkinTestTotalHeight -= intPeeHeight;
//				//					m_intTotalHeight -= intPeeHeight;
//				//					this.Height = m_intTotalHeight;
//				//				}
//
//				objRecord.m_arlPeeValue.Remove(objDeleteValue);
//			}
//			else
//			{
//				//设置删除者
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
//
//		/// <summary>
//		/// 删除引流量
//		/// </summary>
//		/// <param name="p_objValue">引流量</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteOutStream(clsThreeMeasureOutStreamValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOutStreamDate];
//
//			if(objRecord == null || objRecord.m_arlOutStreamValue.Count <= 0)
//				return false;			
//
//			clsThreeMeasureOutStreamValue objDeleteValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[objRecord.m_arlOutStreamValue.Count-1];
//
//			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
//				return false;
//
//			if(!p_blnInControl)
//			{
//				//直接去掉，但高度不重新计算
//				//				if(objRecord.m_arlOutStreamValue.Count > 0)
//				//				{
//				//					int intOutStreamHeight = 21;
//				//
//				//					c_intOutStreamHeight -= intOutStreamHeight;
//				//
//				//					c_intOutStreamTotalHeight -= intOutStreamHeight;
//				//					c_intPressureTotalHeight -= intOutStreamHeight;
//				//					c_intWeightTotalHeight -= intOutStreamHeight;
//				//					c_intSkinTestTotalHeight -= intOutStreamHeight;
//				//					m_intTotalHeight -= intOutStreamHeight;
//				//					this.Height = m_intTotalHeight;
//				//				}
//
//				objRecord.m_arlOutStreamValue.Remove(objDeleteValue);
//			}
//			else
//			{
//				//设置删除者
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
//
////		/// <summary>
////		/// 删除血压
////		/// </summary>
////		/// <param name="p_objValue">血压</param>
////		/// <param name="p_blnInControl">是否需要作控制</param>
////		/// <returns></returns>
////		public bool m_blnDeletePressure(clsThreeMeasurePressureValue p_objValue,bool p_blnInControl)
////		{
////			if(p_objValue == null)
////				return false;
////
////			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPressureDate];
////
////			if(objRecord == null || objRecord.m_arlPressureValue.Count <= 0)
////				return false;			
////
////			clsThreeMeasurePressureValue objDeleteValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue[objRecord.m_arlPressureValue.Count-1];
////
////			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
////				return false;
////
////			if(!p_blnInControl)
////			{
////				//直接去掉，但高度不重新计算
////				//				if(objRecord.m_arlPressureValue.Count > 0)
////				//				{
////				//					int intPressureHeight = 21;
////				//
////				//					c_intPressureHeight -= intPressureHeight;
////				//
////				//					c_intPressureTotalHeight -= intPressureHeight;
////				//					c_intWeightTotalHeight -= intPressureHeight;
////				//					c_intSkinTestTotalHeight -= intPressureHeight;
////				//					m_intTotalHeight -= intPressureHeight;
////				//					this.Height = m_intTotalHeight;
////				//				}
////
////				objRecord.m_arlPressureValue.Remove(objDeleteValue);
////			}
////			else
////			{
////				//设置删除者
////				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
////				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
////				objDeleteInfo.m_strUserID = m_strUserID;
////				objDeleteInfo.m_strUserName = m_strUserName;
////
////				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
////			}
////
////			return true;
////		}
//		
//		/// <summary>
//		/// 删除体重
//		/// </summary>
//		/// <param name="p_objValue">体重</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteWeight(clsThreeMeasureWeightValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmWeightDate];
//
//			if(objRecord == null || objRecord.m_arlWeightValue.Count <= 0)
//				return false;			
//
//			clsThreeMeasureWeightValue objDeleteValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[objRecord.m_arlWeightValue.Count-1];
//
//			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
//				return false;
//
//			if(!p_blnInControl)
//			{
//				//直接去掉，但高度不重新计算
//				//				if(objRecord.m_arlWeightValue.Count > 0)
//				//				{
//				//					int intWeightHeight = 21;
//				//
//				//					c_intWeightHeight -= intWeightHeight;
//				//
//				//					c_intWeightTotalHeight -= intWeightHeight;
//				//					c_intSkinTestTotalHeight -= intWeightHeight;
//				//					m_intTotalHeight -= intWeightHeight;
//				//					this.Height = m_intTotalHeight;
//				//				}
//
//				objRecord.m_arlWeightValue.Remove(objDeleteValue);
//			}
//			else
//			{
//				//设置删除者
//				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//				objDeleteInfo.m_strUserID = m_strUserID;
//				objDeleteInfo.m_strUserName = m_strUserName;
//
//				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
//			}
//
//			return true;
//		}
//		
//		/// <summary>
//		/// 删除皮试
//		/// </summary>
//		/// <param name="p_objValue">皮试</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteSkinTest(clsThreeMeasureSkinTestValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null || p_objValue.m_objDeleteInfo != null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmSkinTestDate];
//
//			if(objRecord == null || objRecord.m_arlSkinTestValue.Count == 0)
//				return false;
//
//			for(int i=0;i<objRecord.m_arlSkinTestValue.Count;i++)
//			{
//				clsThreeMeasureSkinTestValue objValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[i];
//
//				if(objValue == p_objValue)
//				{
//					if(!p_blnInControl)
//					{
//						//直接去掉，但高度不重新计算
//						objRecord.m_arlSkinTestValue.RemoveAt(i);
//					
////						if(objValue.m_intTimeIndex == -1)
////						{
////							if(objRecord.m_arlSkinTestValue.Count == m_intMaxSkinTestCount)
////							{
////								int intSkinTestHeight = 21;
////
////								c_intSkinTestHeight -= intSkinTestHeight;
////
////								c_intSkinTestTotalHeight -= intSkinTestHeight;
////								m_intTotalHeight -= intSkinTestHeight;
////								this.Height = m_intTotalHeight;
////							}
////						}
//					}
//					else
//					{
//						//设置删除者
//						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//						objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//						objDeleteInfo.m_strUserID = m_strUserID;
//						objDeleteInfo.m_strUserName = m_strUserName;
//
//						objValue.m_objDeleteInfo = objDeleteInfo;
//					}
//					return true;
//				}
//			}
//
//			return false;
//		}
//
//		/// <summary>
//		/// 删除其它
//		/// </summary>
//		/// <param name="p_objValue">其它</param>
//		/// <param name="p_blnInControl">是否需要作控制</param>
//		/// <returns></returns>
//		public bool m_blnDeleteOther(clsThreeMeasureOtherValue p_objValue,bool p_blnInControl)
//		{
//			if(p_objValue == null || p_objValue.m_objDeleteInfo != null)
//				return false;
//
//			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOtherDate];
//
//			if(objRecord == null || objRecord.m_arlOtherValue.Count == 0)
//				return false;
//
//			for(int i=0;i<objRecord.m_arlOtherValue.Count;i++)
//			{
//				clsThreeMeasureOtherValue objValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[i];
//
//				if(objValue == p_objValue)
//				{
//					if(!p_blnInControl)
//					{
//						//直接去掉，但高度不重新计算
//						objRecord.m_arlOtherValue.RemoveAt(i);
//					
////						if(objRecord.m_arlOtherValue.Count == m_intMaxOtherCount)
////						{							
////							int intOtherHeight = 21;
////							c_intOtherHeight -= intOtherHeight;
////							m_intTotalHeight -= intOtherHeight;
////							this.Height = m_intTotalHeight;
////						}
//					}
//					else
//					{
//						//设置删除者
//						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//						objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
//						objDeleteInfo.m_strUserID = m_strUserID;
//						objDeleteInfo.m_strUserName = m_strUserName;
//
//						objValue.m_objDeleteInfo = objDeleteInfo;
//					}
//					
//					return true;
//				}
//			}
//
//			return false;
//		}
		#endregion

		#region 修改函数（先删除后添加）
//		public bool m_blnModifySpecialDate(clsThreeMeasureSpecialDate p_objOldValue,clsThreeMeasureSpecialDate p_objNewValue)
//		{
//			bool blnOk = m_blnDeleteSpecialDate(p_objOldValue);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddSpecialDate(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddSpecialDate(p_objOldValue);
//
//			return blnOk;
//		}
//		public bool m_blnModifyPulseValue(clsThreeMeasurePulseValue p_objOldValue,clsThreeMeasurePulseValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeletePulseValue(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddPulseValue(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddPulseValue(p_objOldValue);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyTemperatureValue(clsThreeMeasureTemperatureValue p_objOldValue,clsThreeMeasureTemperatureValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnAddTemperatureValue(p_objNewValue);
//
//			if(!blnOk)
//				return false;
//
//			for(int i=0;i<p_objOldValue.m_arlPhyscalDownValue.Count;i++)
//			{
//				blnOk = m_blnAddPhyscalDownValue((clsThreeMeasureTemperaturePhyscalDownValue)p_objOldValue.m_arlPhyscalDownValue[i],p_objNewValue);
//
//				if(!blnOk)
//					break;
//			}
//
//			if(blnOk)
//				blnOk = m_blnDeleteTemperatureValue(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				m_blnDeleteTemperatureValue(p_objNewValue,p_blnInControl);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objOldValue,clsThreeMeasureTemperaturePhyscalDownValue p_objNewValue,clsThreeMeasureTemperatureValue p_objBase,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeletePhyscalDownValue(p_objOldValue,p_objBase,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddPhyscalDownValue(p_objNewValue,p_objBase);
//
//			if(!blnOk)
//				m_blnAddPhyscalDownValue(p_objOldValue,p_objBase);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyEvent(clsThreeMeasureEvent p_objOldEvent,clsThreeMeasureEvent p_objNewEvent,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteEvent(p_objOldEvent,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddEvent(p_objNewEvent);
//
//			if(!blnOk)
//				m_blnAddEvent(p_objOldEvent);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyBreath(clsThreeMeasureBreathValue p_objOldValue,clsThreeMeasureBreathValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteBreath(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddBreath(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddBreath(p_objOldValue);
//
//			return blnOk;
//		}
//		
//		public bool m_blnModifyInput(clsThreeMeasureInputValue p_objOldValue,clsThreeMeasureInputValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteInput(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddInput(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddInput(p_objOldValue);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyDejecta(clsThreeMeasureDejectaValue p_objOldValue,clsThreeMeasureDejectaValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteDejecta(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddDejecta(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddDejecta(p_objOldValue);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyPee(clsThreeMeasurePeeValue p_objOldValue,clsThreeMeasurePeeValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeletePee(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddPee(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddPee(p_objOldValue);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyOutStream(clsThreeMeasureOutStreamValue p_objOldValue,clsThreeMeasureOutStreamValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteOutStream(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddOutStream(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddOutStream(p_objOldValue);
//
//			return blnOk;
//		}
//
////		public bool m_blnModifyPressure(clsThreeMeasurePressureValue p_objOldValue,clsThreeMeasurePressureValue p_objNewValue,bool p_blnInControl)
////		{
////			bool blnOk = m_blnDeletePressure(p_objOldValue,p_blnInControl);
////
////			if(!blnOk)
////				return false;
////
////			blnOk = m_blnAddPressure(p_objNewValue);
////
////			if(!blnOk)
////				m_blnAddPressure(p_objOldValue);
////
////			return blnOk;
////		}
//		
//		public bool m_blnModifyWeight(clsThreeMeasureWeightValue p_objOldValue,clsThreeMeasureWeightValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteWeight(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddWeight(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddWeight(p_objOldValue);
//
//			return blnOk;
//		}
//		
//		public bool m_blnModifySkinTest(clsThreeMeasureSkinTestValue p_objOldValue,clsThreeMeasureSkinTestValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteSkinTest(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddSkinTest(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddSkinTest(p_objOldValue);
//
//			return blnOk;
//		}
//
//		public bool m_blnModifyOther(clsThreeMeasureOtherValue p_objOldValue,clsThreeMeasureOtherValue p_objNewValue,bool p_blnInControl)
//		{
//			bool blnOk = m_blnDeleteOther(p_objOldValue,p_blnInControl);
//
//			if(!blnOk)
//				return false;
//
//			blnOk = m_blnAddOther(p_objNewValue);
//
//			if(!blnOk)
//				m_blnAddOther(p_objOldValue);
//
//			return blnOk;
//		}
		#endregion

//		/// <summary>
//		/// 刷新
//		/// </summary>
//		public void m_mthUpdateDisplay()
//		{
//			this.//Invalidate();
//		}

		/// <summary>
		/// 清空所有
		/// </summary>
		public void m_mthClearAll()
		{
			/*
			 * 重置所有变量
			 */
			dtmPreDateForSpecialDate = DateTime.Today;
			intSpecialNewStartTimes = 0;

			c_intBreathHeight = 28;
			c_intInputHeight = 28;
			c_intDejectaHeight = 28;
			c_intPeeHeight = 28;
			c_intOutStreamHeight = 28;
			c_intPressureHeight = 28;
			c_intWeightHeight = 28;
			c_intSkinTestHeight = 28;
			c_intOtherHeight = 28;			
			
			m_intTotalHeight = 
				c_intRecordDateHeight+
				c_intInpateintDateHeight+
				c_intSpecialDateHeight+
				c_intTimeHeight+
				c_intGridHeight*c_intGridHeightCount+
				c_intBreathHeight+
				c_intInputHeight+
				c_intDejectaHeight+
				c_intPeeHeight+
				c_intOutStreamHeight+
				c_intPressureHeight+
				c_intWeightHeight+
				c_intSkinTestHeight+
				c_intOtherHeight;

//			this.Height = m_intTotalHeight;		
	
			c_intBreathTotalHeight = 28+24+24+c_intGridHeight*45+28;
			c_intInputTotalHeight = 28+24+24+c_intGridHeight*45+28+28;
			c_intDejectaTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28;
			c_intOutStreamTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28;
			c_intPeeTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28;
			c_intPressureTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28;
			c_intWeightTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28;
			c_intSkinTestTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28+28;            
			c_intOtherTotalHeight  = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28+28+28;     
			m_intMaxBreathCount = 1;
			m_intMaxInputCount = 1;
			m_intMaxDejectaCount = 1;
			m_intMaxPeeCount = 1;
			m_intMaxOutStreamCount = 1;
			m_intMaxPressureCount = 1;
			m_intMaxWeightCount = 1;
			m_intMaxSkinTestCount = 1;
			m_intMaxOtherCount = 1;

			m_objDateManager.m_mthClear();
			m_objPulseValueManager.m_mthClear();
			m_objTemperatureValueManager.m_mthClear();
			m_objBreathValueManager.m_mthClear();
			m_objStayOutValueManager.m_mthClear();
			
			m_intTotalDate = 7;

			m_blnCanResize = false;

//			this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

			m_blnCanResize = true;

			m_strOtherName = null;
			
//			this.//Invalidate();
		}

		#region Handle Xml
		/// <summary>
		/// 根据Xml设置三测表中日期信息
		/// </summary>
		/// <param name="p_objXmlValue">Xml集合</param>
		/// <returns></returns>
		public bool m_blnSetXml(clsThreeMeasureXmlValue p_objXmlValue,DateTime p_dtmFirstPrintDate)
		{
			if(p_objXmlValue == null)
				return false;	
			
			m_blnDeleteRecordDate(m_objDateManager[DateTime.Parse(p_objXmlValue.m_strRecordDate)]);

			clsThreeMeasureDateRecord objRecord = m_objAddRecordDate(DateTime.Parse(p_objXmlValue.m_strRecordDate));
			objRecord.m_dtmFirstPrintDate = p_dtmFirstPrintDate;

			if(objRecord == null)
				return false;

			m_blnSetOther(p_objXmlValue.m_strOtherName);

			XmlDocument objDoc = new XmlDocument();			

			//SpecialDate
			if(p_objXmlValue.m_strSpecialDateXml != "0")
			{
				objDoc.LoadXml(p_objXmlValue.m_strSpecialDateXml);

				clsThreeMeasureSpecialDate objValue = new clsThreeMeasureSpecialDate();
				objValue.m_dtmSpecialDate = DateTime.Parse(objDoc.DocumentElement.Attributes["Time"].Value);
				objValue.m_blnIsNewStart = bool.Parse(objDoc.DocumentElement.Attributes["IsNew"].Value);
				objValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.Attributes["ModifyTime"].Value);

				objRecord.m_objSpecialDate = objValue;
			}

			#region EventXml
			objDoc.LoadXml(p_objXmlValue.m_strEventXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureEvent objEventValue = new clsThreeMeasureEvent();
				objEventValue.m_dtmEventTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objEventValue.m_enmEventType = (enmThreeMeasureEventType)Enum.Parse(typeof(enmThreeMeasureEventType),objDoc.DocumentElement.ChildNodes[i].Attributes["EventType"].Value);
				objEventValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

				m_blnAddEvent(objEventValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objEventValue.m_objDeleteInfo = objDeleteInfo;
				}
			}
			#endregion

			#region BreathXml old

//			objDoc.LoadXml(p_objXmlValue.m_strBreathXml);
//			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
//			{
//				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);
//
//				clsThreeMeasureBreathValue objBreathValue = new clsThreeMeasureBreathValue();
//
//				objBreathValue.m_dtmBreathTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
//				objBreathValue.m_enmBreathType = (enmThreeMeasureBreathType)Enum.Parse(typeof(enmThreeMeasureBreathType),objDoc.DocumentElement.ChildNodes[i].Attributes["BreathType"].Value);
//				objBreathValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime),objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
//				objBreathValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
//				objBreathValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
//				
//				m_blnAddBreath(objBreathValue);
//
//				if(blnIsDelete)
//				{
//					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
//					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
//					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
//					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;
//
//					objBreathValue.m_objDeleteInfo = objDeleteInfo;
//				}
//			}
			#endregion

			#region InputXml
			objDoc.LoadXml(p_objXmlValue.m_strInputXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureInputValue objInputValue = new clsThreeMeasureInputValue();
				objInputValue.m_dtmInputDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objInputValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
				objInputValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

				m_blnAddInput(objInputValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objInputValue.m_objDeleteInfo = objDeleteInfo;
				}
			}
			#endregion

			#region DejectaXml
			objDoc.LoadXml(p_objXmlValue.m_strDejectaXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureDejectaValue objDejectaValue = new clsThreeMeasureDejectaValue();
				objDejectaValue.m_blnAfterMoreTimes = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["AfterMoreTimes"].Value);
				objDejectaValue.m_blnCanDejecta = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["CanDejecta"].Value);
				objDejectaValue.m_blnNeedWeight = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["NeedWeight"].Value);
				objDejectaValue.m_dtmDejectaDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objDejectaValue.m_fltWeight = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Weight"].Value);
				objDejectaValue.m_intAfterTimes = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["AfterTimes"].Value);
				objDejectaValue.m_intBeforeTimes = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["BeforeTimes"].Value);
				objDejectaValue.m_intClysisTimes = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ClysisTimes"].Value);
				objDejectaValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
				m_blnAddDejecta(objDejectaValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objDejectaValue.m_objDeleteInfo = objDeleteInfo;
				}						
			}
			#endregion

			#region PeeXml
			objDoc.LoadXml(p_objXmlValue.m_strPeeXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasurePeeValue objPeeValue = new clsThreeMeasurePeeValue();
//				objPeeValue.m_blnIsIrretention = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsIrretention"].Value);
				objPeeValue.m_dtmPeeDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objPeeValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
				objPeeValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

				m_blnAddPee(objPeeValue);
				
				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objPeeValue.m_objDeleteInfo = objDeleteInfo;
				}			
			}
			#endregion

			#region OutStreamXml
			objDoc.LoadXml(p_objXmlValue.m_strOutStreamXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureOutStreamValue objOutStreamValue = new clsThreeMeasureOutStreamValue();
				try
				{
					objOutStreamValue.m_enmIsIrretention = (enmIrretention)Enum.Parse(typeof(enmIrretention),objDoc.DocumentElement.ChildNodes[i].Attributes["IsIrretention"].Value);
				}
				catch{objOutStreamValue.m_enmIsIrretention = enmIrretention.一般;}
				objOutStreamValue.m_dtmOutStreamDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objOutStreamValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
				objOutStreamValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
				m_blnAddOutStream(objOutStreamValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objOutStreamValue.m_objDeleteInfo = objDeleteInfo;
				}		
			}
			#endregion

			#region PressureXml
			objDoc.LoadXml(p_objXmlValue.m_strPressureXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasurePressureValue objPressureValue = new clsThreeMeasurePressureValue();
				objPressureValue.m_dtmPressureDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objPressureValue.m_fltDiastolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DiastolicValue"].Value);
				objPressureValue.m_fltSystolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["SystolicValue"].Value);
				objPressureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
				m_blnAddPressure(objPressureValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objPressureValue.m_objDeleteInfo = objDeleteInfo;
				}		
			}
			#endregion

			#region PressureXml2
			if(p_objXmlValue.m_strPressureXml2 != null && p_objXmlValue.m_strPressureXml2 != "")
			{
				objDoc.LoadXml(p_objXmlValue.m_strPressureXml2);
				for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
				{
					bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

					clsThreeMeasurePressureValue objPressureValue = new clsThreeMeasurePressureValue();
					objPressureValue.m_dtmPressureDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
					objPressureValue.m_fltDiastolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DiastolicValue"].Value);
					objPressureValue.m_fltSystolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["SystolicValue"].Value);
					objPressureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
					m_blnAddPressure(objPressureValue);

					if(blnIsDelete)
					{
						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
						objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
						objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
						objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

						objPressureValue.m_objDeleteInfo = objDeleteInfo;
					}		
				}
			}
			#endregion

			#region WeightXml
			objDoc.LoadXml(p_objXmlValue.m_strWeightXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureWeightValue objWeightValue = new clsThreeMeasureWeightValue();
				objWeightValue.m_dtmWeightDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				try
				{
					objWeightValue.m_enmWeightType = (enmThreeMeasureWeightType)Enum.Parse(typeof(enmThreeMeasureWeightType),objDoc.DocumentElement.ChildNodes[i].Attributes["WeightType"].Value);
				}
				catch{objWeightValue.m_enmWeightType = enmThreeMeasureWeightType.平车;}
				objWeightValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["WeightValue"].Value);
				objWeightValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

				m_blnAddWeight(objWeightValue);
				
				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objWeightValue.m_objDeleteInfo = objDeleteInfo;
				}		
			}
			#endregion

			#region SkinTestXml
			objDoc.LoadXml(p_objXmlValue.m_strSkinTestXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureSkinTestValue objSkinTestValue = new clsThreeMeasureSkinTestValue();
				objSkinTestValue.m_dtmSkinTestDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objSkinTestValue.m_strMedicineName = objDoc.DocumentElement.ChildNodes[i].Attributes["MedicineName"].Value;
				try
				{
					objSkinTestValue.m_intBadStatus = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsBad"].Value);
				}
				catch
				{
					try
					{
						bool blnIsBed = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsBad"].Value);
						objSkinTestValue.m_intBadStatus = blnIsBed?1:0;
					}
					catch{objSkinTestValue.m_intBadStatus = -1;}
				}
				objSkinTestValue.m_intBadCount = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["BadCount"].Value);
				objSkinTestValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
                try
                {
                    objSkinTestValue.m_StrOtherResult = objDoc.DocumentElement.ChildNodes[i].Attributes["OtherResult"].Value;
                }
                catch
                {
                    objSkinTestValue.m_StrOtherResult = string.Empty;
                }
				m_blnAddSkinTest(objSkinTestValue);
				
				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objSkinTestValue.m_objDeleteInfo = objDeleteInfo;
				}	
			}
			#endregion

			#region OtherXml
			objDoc.LoadXml(p_objXmlValue.m_strOtherXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureOtherValue objOtherValue = new clsThreeMeasureOtherValue();
				objOtherValue.m_dtmOtherDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objOtherValue.m_strOtherItem = objDoc.DocumentElement.ChildNodes[i].Attributes["Item"].Value;
				objOtherValue.m_strOtherUnit = objDoc.DocumentElement.ChildNodes[i].Attributes["Unit"].Value;
				objOtherValue.m_StrOtherValue = objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value;
				objOtherValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

				m_blnAddOther(objOtherValue);
			
				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objOtherValue.m_objDeleteInfo = objDeleteInfo;
				}		
			}
			#endregion

			#region StayOutXml
			if(p_objXmlValue.m_strStayOutXml.Length>0)
			{
				objDoc.LoadXml(p_objXmlValue.m_strStayOutXml);
				for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
				{
					bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

					clsThreeMeasureStayOutValue objStayOutValue = new clsThreeMeasureStayOutValue();
					objStayOutValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
					objStayOutValue.m_dtmStayOutTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
					objStayOutValue.m_enmStayOutType = (enmThreeMeasureStayOutType)Enum.Parse(typeof(enmThreeMeasureStayOutType),objDoc.DocumentElement.ChildNodes[i].Attributes["StayOutType"].Value);
					objStayOutValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime),objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
					objStayOutValue.m_blnValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
					objStayOutValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
					objStayOutValue.m_dtmValueTime=DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
					m_blnAddStayOutValue(objStayOutValue);

					if(blnIsDelete)
					{
						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
						objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
						objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
						objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

						objStayOutValue.m_objDeleteInfo = objDeleteInfo;
					}
				}
			}
			#endregion

			#region BreathXml
			objDoc.LoadXml(p_objXmlValue.m_strBreathXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureBreathValue objBreathValue = new clsThreeMeasureBreathValue();
				try
				{
					objBreathValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
				}
				catch
				{
				objBreathValue.m_blnLineToPreValue =false;
				}
				objBreathValue.m_dtmBreathTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objBreathValue.m_enmBreathType = (enmThreeMeasureBreathType)Enum.Parse(typeof(enmThreeMeasureBreathType),objDoc.DocumentElement.ChildNodes[i].Attributes["BreathType"].Value);
				objBreathValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime),objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
				objBreathValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
				objBreathValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
				m_blnAddBreathValue(objBreathValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objBreathValue.m_objDeleteInfo = objDeleteInfo;
				}
			}
			#endregion

			#region PulseXml
			objDoc.LoadXml(p_objXmlValue.m_strPulseXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasurePulseValue objPulseValue = new clsThreeMeasurePulseValue();
				objPulseValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
				objPulseValue.m_dtmValueTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objPulseValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime),objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
				objPulseValue.m_enmType = (enmThreeMeasurePulseType)Enum.Parse(typeof(enmThreeMeasurePulseType),objDoc.DocumentElement.ChildNodes[i].Attributes["Type"].Value);
				objPulseValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);				
				objPulseValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

				m_blnAddPulseValue(objPulseValue);

				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objPulseValue.m_objDeleteInfo = objDeleteInfo;
				}		
			}
			#endregion

			#region TemperatureXml
			objDoc.LoadXml(p_objXmlValue.m_strTemperatureXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureTemperatureValue objTemperatureValue = new clsThreeMeasureTemperatureValue();

				objTemperatureValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
				objTemperatureValue.m_dtmValueTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objTemperatureValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime),objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
				objTemperatureValue.m_enmType = (enmThreeMeasureTemperatureType)Enum.Parse(typeof(enmThreeMeasureTemperatureType),objDoc.DocumentElement.ChildNodes[i].Attributes["Type"].Value);
				objTemperatureValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);				
				objTemperatureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
				m_blnAddTemperatureValue(objTemperatureValue);
			
				if(blnIsDelete)
				{
					clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
					objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
					objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
					objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

					objTemperatureValue.m_objDeleteInfo = objDeleteInfo;
				}		
				else
				{
					XmlNode xndTempValue = objDoc.DocumentElement.ChildNodes[i];
					for(int j2=0;j2<xndTempValue.ChildNodes.Count;j2++)
					{
						bool blnIsDeleteDown = bool.Parse(xndTempValue.ChildNodes[j2].Attributes["IsDelete"].Value);

						clsThreeMeasureTemperaturePhyscalDownValue objDownValue = new clsThreeMeasureTemperaturePhyscalDownValue();
						objDownValue.m_dtmValueTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["Time"].Value);
						objDownValue.m_fltValue = float.Parse(xndTempValue.ChildNodes[j2].Attributes["Value"].Value);						
						objDownValue.m_dtmModifyTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["ModifyTime"].Value);

						m_blnAddPhyscalDownValue(objDownValue,objTemperatureValue);
			
						if(blnIsDeleteDown)
						{
							clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
							objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["DeleteTime"].Value);
							objDeleteInfo.m_strUserID = xndTempValue.ChildNodes[j2].Attributes["DeleteUserID"].Value;
							objDeleteInfo.m_strUserName = xndTempValue.ChildNodes[j2].Attributes["DeleteUserName"].Value;

							objDownValue.m_objDeleteInfo = objDeleteInfo;
						}	
					}
				}
			}
			#endregion			

			return true;
		}

		#endregion		

		private void m_mthUpdateHeight(int p_intStartDateIndex,int p_intRecordLength)
		{
			//Breath
			int intMaxBreathExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];

				int intDeleteBreath = 0;

				for(int j2=0;j2<objRecord.m_arlBreath.Count;j2++)
				{
					clsThreeMeasureBreathValue objBreath = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[j2];

					if(objBreath.m_objDeleteInfo != null && objBreath.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objBreath.m_objDeleteInfo != null)
					{
						intDeleteBreath++;
					}
				}

				if(intMaxBreathExt < intDeleteBreath)
					intMaxBreathExt = intDeleteBreath;
			}
			c_intBreathHeight = intMaxBreathExt*14+28;
			c_intBreathTotalHeight = c_intGridTotalHeight+c_intBreathHeight;

			//Input
			int intMaxInputExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeleteInput = 0;

				for(int j2=0;j2<objRecord.m_arlInputValue.Count;j2++)
				{
					clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];

					if(objInputValue.m_objDeleteInfo != null && objInputValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objInputValue.m_objDeleteInfo != null)
					{
						intDeleteInput++;
					}
				}

				if(intMaxInputExt < intDeleteInput)
					intMaxInputExt = intDeleteInput;
			}
			c_intInputHeight = 28+intMaxInputExt*21;
			c_intInputTotalHeight = c_intBreathTotalHeight+c_intInputHeight;

			//Dejecta
			int intMaxDejectaExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeleteDejecta = 0;

				for(int j2=0;j2<objRecord.m_arlDejectaValue.Count;j2++)
				{
					clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];

					if(objDejectaValue.m_objDeleteInfo != null && objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objDejectaValue.m_objDeleteInfo != null)
					{
						intDeleteDejecta++;
					}
				}

				if(intMaxDejectaExt < intDeleteDejecta)
					intMaxDejectaExt = intDeleteDejecta;
			}
			c_intDejectaHeight = 28+intMaxDejectaExt*21;
			c_intDejectaTotalHeight = c_intInputTotalHeight+c_intDejectaHeight;

			//Pee
			int intMaxPeeExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeletePee = 0;

				for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
				{
					clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

					if(objPeeValue.m_objDeleteInfo != null && objPeeValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objPeeValue.m_objDeleteInfo != null)
					{
						intDeletePee++;
					}
				}

				if(intMaxPeeExt < intDeletePee)
					intMaxPeeExt = intDeletePee;
			}
			c_intPeeHeight = 28+intMaxPeeExt*21;
			c_intPeeTotalHeight = c_intDejectaTotalHeight+c_intPeeHeight;

			//OutStream
			int intMaxOutStreamExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeleteOutStream = 0;

				for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
				{
					clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

					if(objOutStreamValue.m_objDeleteInfo != null && objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objOutStreamValue.m_objDeleteInfo != null)
					{
						intDeleteOutStream++;
					}
				}

				if(intMaxOutStreamExt < intDeleteOutStream)
					intMaxOutStreamExt = intDeleteOutStream;
			}
			c_intOutStreamHeight = 28+intMaxOutStreamExt*21;
			c_intOutStreamTotalHeight = c_intPeeTotalHeight+c_intOutStreamHeight;
			

			//Pressure
			int intMaxPressureExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeletePressure = 0;

				for(int j2=0;j2<objRecord.m_arlPressureValue1.Count;j2++)
				{
					clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

					if(objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objPressureValue.m_objDeleteInfo != null)
					{
						intDeletePressure++;
					}
				}

				if(intMaxPressureExt < intDeletePressure)
					intMaxPressureExt = intDeletePressure;
			}
			c_intPressureHeight = 28+intMaxPressureExt*21;
			c_intPressureTotalHeight = c_intOutStreamTotalHeight+c_intPressureHeight;


			//Weight
			int intMaxWeightExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeleteWeight = 0;

				for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
				{
					clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

					if(objWeightValue.m_enmWeightType != enmThreeMeasureWeightType.一般)
					{
						continue;
					}

					if(objWeightValue.m_objDeleteInfo != null && objWeightValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objWeightValue.m_objDeleteInfo != null)
					{
						intDeleteWeight++;
					}
				}

				if(intMaxWeightExt < intDeleteWeight)
					intMaxWeightExt = intDeleteWeight;
			}
			c_intWeightHeight = 28+intMaxWeightExt*21;
			c_intWeightTotalHeight = c_intPressureTotalHeight+c_intWeightHeight;


			//SkinTest
			int intMaxSkinTestExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeleteSkinTest = 0;

				for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
				{
					clsThreeMeasureSkinTestValue objSkinTestValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

					if(objSkinTestValue.m_intTimeIndex >= 0)
					{
						continue;
					}

					if(objSkinTestValue.m_objDeleteInfo != null && objSkinTestValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					intDeleteSkinTest++;					
				}

				if(intMaxSkinTestExt < intDeleteSkinTest)
					intMaxSkinTestExt = intDeleteSkinTest;
			}
			if(intMaxSkinTestExt > 0)
				intMaxSkinTestExt -= 1;
			c_intSkinTestHeight = 28+intMaxSkinTestExt*21;
			c_intSkinTestTotalHeight = c_intWeightTotalHeight+c_intSkinTestHeight;

			//Other
			int intMaxOtherExt = 0;
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				int intDeleteOther = 0;
				for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
				{
					clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];
					
					if(objOtherValue.m_objDeleteInfo != null && objOtherValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
					{
						continue;
					}

					if(objOtherValue.m_objDeleteInfo != null)
					{
						intDeleteOther++;
					}
				}

				if(intMaxOtherExt < intDeleteOther)
					intMaxOtherExt = intDeleteOther;
				
			}
			// = 28+intMaxOtherExt*21;
//			m_intTotalHeight = c_intSkinTestTotalHeight+c_intOtherHeight;
//			c_intOtherTotalHeight = c_intSkinTestTotalHeight+c_intOtherHeight;

			
		
		}

		/// <summary>
		/// 打印三测表
		/// </summary>
		/// <param name="p_intStartX">开始的X坐标</param>
		/// <param name="p_intStartY">开始的Y坐标</param>
		/// <param name="p_intMaxHeight">最大的高度</param>
		/// <param name="e">打印变量</param>
		/// <param name="p_intStartDateIndex">开始打印的日期</param>
		/// <param name="p_intRecordLength">打印的日期数目</param>
		/// <param name="p_blnHasMoreDate">返回是否还有日期没有打印</param>
		/// <param name="p_intEndY">返回最后的Y坐标</param>
		/// <param name="p_intLeftItem">返回因高度不足而没打印的内容</param>
		public void m_mthPrintRecord(int p_intStartX,int p_intStartY,int p_intMaxHeight,System.Drawing.Printing.PrintPageEventArgs e,int p_intStartDateIndex,int p_intRecordLength,out bool p_blnHasMoreDate,out int p_intEndY,out int p_intLeftItem)
		{
			//画日期及其子信息
			bool blnIsUpBreath = true;
			Font fntBigSymbol = new Font("",c_flt14PointFontSize);
			DateTime dtmPreDateForDateRecord = DateTime.Today;			
			int intCurrentDateDiff = 0;
			int intPreWidth = p_intStartDateIndex*6*c_intGridHeight;
			int intStartPrintX = p_intStartX;
			int intStartPrintY = p_intStartY;
			intStartPrintX -= intPreWidth;
			int intLastIndex = 0;	
			string strSpecialDataText = "";

			
			Color clrTemp = m_ClrPulseSymbol;
			m_ClrPulseSymbol = Color.Red;
			
			Color clrTempBase = m_ClrTemperatureSymbol;
			Color clrTempDown = m_ClrDownTemperature;
			m_ClrTemperatureSymbol = Color.Blue;
			m_ClrDownTemperature = Color.Red;
			
			Pen penOneWidthLine = new Pen(Color.Black);
			Pen penTwoWidthLine = new Pen(Color.Red,2);
			Pen penDotLine = new Pen(Color.BlueViolet);
			penDotLine.DashStyle = DashStyle.Dot;
			
			Font fntRecordDateText = new Font("",c_flt6PointFontSize);
			Font fntRecordDateTextTop = new Font("",c_flt5MorePointFontSize);
			
			Font fntSpecialDateText = new Font("",c_flt5PointFontSize);
			Font fntGridText = new Font("",c_flt7PointFontSize);
			Font fntSymbol = new Font("",c_flt9PointFontSize);
			Font fnt6PtText = new Font("",c_flt6PointFontSize);
			
			SolidBrush bruTemp = new SolidBrush(Color.Black);

			Color clrPenTemp;
			Color clrBrushTemp;

			Font fntTemp = new Font("",c_flt7PointFontSize);
			
			//先计算皮试与其它的高度,固定高度
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				intLastIndex = i;
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
				
//				if(p_intLeftItem > 7)
//				{
					//画皮试(不只画PPD)		
//					bruTemp.Color = Color.Blue ;
					float intSkinTestNotPPDCount = 0;
//					penOneWidthLine.Color = m_clrDST;
					//Font fntTemp = new Font("",c_flt7PointFontSize);
					for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
					{
						clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

						if(objSkinValue.m_objDeleteInfo != null && objSkinValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						if(objSkinValue.m_intTimeIndex >= 0)
						{
						}
						else
						{
							//非ppd



							string strValue = "";
//							bruTemp.Color = m_clrSkinTest;
							//e.Graphics.DrawString(objSkinValue.m_strMedicineName,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+2);
							strValue =objSkinValue.m_strMedicineName;
							if(objSkinValue.m_intBadStatus == 1)
							{
								strValue +="(";
								for(int z1=0;z1<objSkinValue.m_intBadCount;z1++)
								{
									strValue +="+";
								}
								strValue += ")";
//								bruTemp.Color = Color.Red;
								//e.Graphics.DrawString(strValue,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height);
							}
							else if(objSkinValue.m_intBadStatus == 0)
							{
								strValue += "(-)";
//								bruTemp.Color = m_clrSkinTest;
								//e.Graphics.DrawString(strValue,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height);
							}

                            else if (objSkinValue.m_intBadStatus == 2)
                            {
                                strValue += "(" + objSkinValue.m_StrOtherResult + ")";
                            }
							string strText=strValue;
							SizeF szfName = e.Graphics.MeasureString(strText,fntSymbol);
						

							Font fntOtherText = new Font("",c_flt7PointFontSize);
							float lngRowChar=0;
							char strTempDraw;
							float fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+intStartPrintX;
							float intloop=0;
//							bruTemp.Color =Color.Blue ;
							for(int j=0;j<strText.Length;j++)
							{
						
								strTempDraw=strText[j];
								lngRowChar=j*fntTemp.Size;
								fltXpoint =fltXpoint+fntTemp.Size+4;
								//大于一行就换行
								if(lngRowChar-intloop>=35)
								{
									//行高
									intloop=lngRowChar;
									intSkinTestNotPPDCount=intSkinTestNotPPDCount+fntTemp.Size+2;
									fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+intStartPrintX;
								}

								//e.Graphics.DrawString(strTempDraw.ToString(),fntTemp,bruTemp,fltXpoint+intStartPrintX-144,c_intWeightTotalHeight+intSkinTestNotPPDCount+intStartPrintY);//intSkinTestNotPPDCount*(int)szfName.Height);
							}
							intSkinTestNotPPDCount =intSkinTestNotPPDCount+10;		
				
							if(objSkinValue.m_objDeleteInfo != null)
							{
								SizeF szfDesc = e.Graphics.MeasureString(objSkinValue.m_strMedicineName+strValue,fntSymbol);
								//								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+10+intStartPrintY);
								//								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+13+intStartPrintY);
							}

							intSkinTestNotPPDCount++;
													
						}
						arlSkinHeightMax.Add(intSkinTestNotPPDCount);
												
					}
					//c_intSkinTestTotalHeight=c_intWeightTotalHeight+(int)intSkinTestNotPPDCount+10+intStartPrintY;

//				}
//				if(p_intLeftItem > 8)
//				{				
					//画其它
//					bruTemp.Color = Color.Blue ;
//					penOneWidthLine.Color = m_clrDST;
					float intOtherRowCount=0;
					for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
					{

						clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];

						Font fntOtherText = new Font("",c_flt7PointFontSize);
					
					
						string strText="";
						string strItemValue="";

						//要画的文本
                        //try
                        //{
                            //double fltValue=Math.Round((double)objOtherValue.m_fltOtherValue,2);
                            strItemValue = objOtherValue.m_StrOtherValue;
                        //}
                        //catch
                        //{
                        //    strItemValue=objOtherValue.m_fltOtherValue.ToString();
                        //}
						if(objOtherValue.m_strOtherItem.Trim()=="身高")
							strText=objOtherValue.m_strOtherItem+":"+strItemValue+"CM";
						else
							strText=objOtherValue.m_strOtherItem+":"+strItemValue;

						SizeF szfDesc = e.Graphics.MeasureString(strText,fntSpecialDateText);
													
						float lngRowChar=0;
						string strTempDraw="";
					
						char strTempDrawText;
						float fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX;
						float intloop=0;
						for(int k=0;k<strText.Length;k++)
						{
					
							strTempDrawText=strText[k];
							lngRowChar=k*fntTemp.Size;
							fltXpoint =fltXpoint+fntTemp.Size+4;
							//大于一行就换行
							if(lngRowChar-intloop>=35)
							{
								//行高
								intloop=lngRowChar;
								intOtherRowCount=intOtherRowCount+fntTemp.Size+2;
								fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+intStartPrintX;
							}
							//e.Graphics.DrawString(strTempDrawText.ToString(),fntTemp,bruTemp,fltXpoint+intStartPrintX-144,c_intSkinTestTotalHeight+(int)intOtherRowCount);//+intLoop* fntTemp.Height);
						}
													
						if(objOtherValue.m_objDeleteInfo != null)
						{
							string [] strValueArr = objOtherValue.m_strOther.Split('\r','\n');
							int intHeight = 0;
							for(int k3=0;k3<strValueArr.Length;k3++)
							{
								if(strValueArr[k3].Trim().Length == 0)
									continue;

								SizeF szfDescPart = e.Graphics.MeasureString(strValueArr[k3],fntSpecialDateText);

								intHeight += 19;
							}
						}
											
						intOtherRowCount +=10;
												
					}
					//所有列的最高值
					arlOtherHeightMax.Add(intOtherRowCount);
											
					//c_intOtherTotalHeight=c_intSkinTestTotalHeight+(int)intOtherRowCount+20+intStartPrintY;
//				}							
				
			}
		

			float fltOtherMaxHeight=c_intOtherHeight ;
			arlOtherHeightMax.Sort();
			if(arlOtherHeightMax.Count>0)
				fltOtherMaxHeight=(float)arlOtherHeightMax[arlOtherHeightMax.Count -1];

			float fltSkinMaxHeight=c_intSkinTestHeight ;
			arlSkinHeightMax.Sort();
			if(arlSkinHeightMax.Count >0)
				fltSkinMaxHeight=(float)arlSkinHeightMax[arlSkinHeightMax.Count -1];
			//			
			if(c_intSkinTestTotalHeight < c_intWeightTotalHeight+(int)fltSkinMaxHeight+intStartPrintY)
				c_intSkinTestTotalHeight=c_intWeightTotalHeight+(int)fltSkinMaxHeight+intStartPrintY;
			if(c_intOtherTotalHeight < c_intWeightTotalHeight+(int)fltSkinMaxHeight+(int)fltOtherMaxHeight+intStartPrintY)
				c_intOtherTotalHeight=c_intWeightTotalHeight+(int)fltSkinMaxHeight+(int)fltOtherMaxHeight+intStartPrintY;
			m_intTotalHeight=c_intOtherTotalHeight+10;


			//m_mthUpdateHeight(p_intStartDateIndex,p_intRecordLength); 

			intStartPrintX = p_intStartX;
//			int intStartPrintY = p_intStartY;

			int intDiffWidth = -43;

			int intTextWidth = c_intTextTotleWidth + intDiffWidth;

			int intRecordHeight = m_intTotalHeight;
			p_intLeftItem = int.MaxValue;

			#region Check Height
//			if(intRecordHeight > p_intMaxHeight)
//			{
//				if(intRecordHeight > p_intMaxHeight)
//				{
//					p_intLeftItem = 8;
//					//intRecordHeight = c_intSkinTestTotalHeight;
//					intRecordHeight = c_intOtherTotalHeight;
//					if(intRecordHeight > p_intMaxHeight)
//					{
//						p_intLeftItem = 7;
//						intRecordHeight = c_intWeightTotalHeight;
//			
//						if(intRecordHeight > p_intMaxHeight)
//						{
//							p_intLeftItem = 6;
//							intRecordHeight = c_intPressureTotalHeight;
//			
//							if(intRecordHeight > p_intMaxHeight)
//							{
//								p_intLeftItem = 5;
//								intRecordHeight = c_intOutStreamTotalHeight;
//			
//								if(intRecordHeight > p_intMaxHeight)
//								{
//									p_intLeftItem = 4;
//									intRecordHeight = c_intPeeTotalHeight;
//			
//									if(intRecordHeight > p_intMaxHeight)
//									{
//										p_intLeftItem = 3;
//										intRecordHeight = c_intDejectaTotalHeight;
//			
//										if(intRecordHeight > p_intMaxHeight)
//										{
//											p_intLeftItem = 2;
//											intRecordHeight = c_intInputTotalHeight;
//			
//											if(intRecordHeight > p_intMaxHeight)
//											{
//												p_intLeftItem = 1;
//												intRecordHeight = c_intBreathTotalHeight;
//											}
//										}
//									}
//								}
//							}
//						}
//					}
//				}
//			}			
			#endregion

//			p_intEndY = intStartPrintY+intRecordHeight;
			p_intEndY = intRecordHeight+10;

			int intRecordWidth = intTextWidth+p_intRecordLength*6*c_intGridHeight;
			
			//画边框
			e.Graphics.DrawRectangle(penOneWidthLine,0+intStartPrintX,0+intStartPrintY,intRecordWidth,m_intTotalHeight-intStartPrintY+10);//intRecordHeight);

			//画文字与值的分隔线
			penOneWidthLine.Color = Color.Black;
//			e.Graphics.DrawLine(penOneWidthLine,intTextWidth+intStartPrintX,1+intStartPrintY,intTextWidth+intStartPrintX,intRecordHeight+intStartPrintY);		
			e.Graphics.DrawLine(penOneWidthLine,intTextWidth+intStartPrintX,1+intStartPrintY,intTextWidth+intStartPrintX,intRecordHeight+10);		
			//画日期文字和与下面的分隔线
			e.Graphics.DrawString("  日    期  ",fntRecordDateText,bruTemp,20+intStartPrintX,1+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intRecordDateHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intRecordDateHeight+intStartPrintY);

			////画住院日期和与下面的分隔线
			Font fntInpateintDateText= new Font("",c_flt10PointFontSize);
			e.Graphics.DrawString(" 住院日数 ",fntRecordDateText,bruTemp,4+intStartPrintX,c_intRecordDateHeight+3+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intInpateintTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intInpateintTotalHeight+intStartPrintY);

			//画特殊日期文字和与下面的分隔线
			e.Graphics.DrawString(" 手术日期 ",fntRecordDateText,bruTemp,4+intStartPrintX,c_intInpateintTotalHeight+3+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY);

			//画时间
			e.Graphics.DrawString("时间  ",fntRecordDateText,bruTemp,20+intStartPrintX,c_intSpecialDateTotalHeight+4+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intTimeTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intTimeTotalHeight+intStartPrintY);

			#region 画格子与时间
			//画竖格子线和时间
			int intNormalGridHeight = c_intSpecialDateTotalHeight+8;
			penOneWidthLine.Color = Color.Black;
			for(int i=0;i<p_intRecordLength;i++)
			{
				clrBrushTemp = bruTemp.Color;
				bruTemp.Color = Color.Red;
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("4",fntGridText,bruTemp,intTextWidth+i*c_intGridHeight*6+2+intStartPrintX,intNormalGridHeight+intStartPrintY);			
				bruTemp.Color = clrBrushTemp;

				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*2+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*2+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("8",fntGridText,bruTemp,intTextWidth+c_intGridHeight+i*c_intGridHeight*6+2+intStartPrintX,intNormalGridHeight+intStartPrintY-c_intGridHeight+3);			

				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*3+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*3+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("12",fntGridText,bruTemp,intTextWidth+c_intGridHeight*2+i*c_intGridHeight*6-2+intStartPrintX,intNormalGridHeight+intStartPrintY-c_intGridHeight+3);			
				
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*4+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*4+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("4",fntGridText,bruTemp,intTextWidth+c_intGridHeight*3+i*c_intGridHeight*6+2+intStartPrintX,intNormalGridHeight+intStartPrintY-c_intGridHeight+3);			

				clrBrushTemp = bruTemp.Color;
				bruTemp.Color = Color.Red;
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*5+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*5+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("8",fntGridText,bruTemp,intTextWidth+c_intGridHeight*4+i*c_intGridHeight*6+2+intStartPrintX,intNormalGridHeight+intStartPrintY);			
				
				clrPenTemp = penOneWidthLine.Color;
				if((i+1)%7 != 0)
				{
					penOneWidthLine.Color = Color.Red;
					e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,1+intStartPrintY,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,c_intGridTotalHeight+intStartPrintY);
				}
				e.Graphics.DrawString("12",fntGridText,bruTemp,intTextWidth+c_intGridHeight*5+i*c_intGridHeight*6-2+intStartPrintX,intNormalGridHeight+intStartPrintY);							
				penOneWidthLine.Color = clrPenTemp;
				bruTemp.Color = clrBrushTemp;

				//e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,c_intGridTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,m_intTotalHeight+intStartPrintY);// intRecordHeight+intStartPrintY);
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,c_intGridTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,m_intTotalHeight+10);// intRecordHeight+intStartPrintY);
			}

			//画横格子线
			int intStartX = intTextWidth+1;
			int intEndX = intRecordWidth-2;
			Pen penOneLine = new Pen(Color.Black);
			Pen penTwoLine = new Pen(Color.Black,3);
			Pen penThreeLine = new Pen(Color.Black,2);

			penOneWidthLine.Color = Color.Black;
			for(int i=1;i<=25;i++)
			{
				if(i==5 || i==10 || i==15 || i==20|| i==25)
					e.Graphics.DrawLine(penThreeLine,intStartX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY);
				else
					e.Graphics.DrawLine(penOneLine,intStartX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY);
			}

			e.Graphics.DrawLine(penTwoWidthLine,intStartX+intStartPrintX,c_intTimeTotalHeight+30*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+30*c_intGridHeight+intStartPrintY);

			for(int i=26;i<c_intGridHeightCount;i++)
			{
				if( i==35 || i==40)
					e.Graphics.DrawLine(penThreeLine,intStartX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY);
				else
				{
					if(i!=30)
						e.Graphics.DrawLine(penOneLine,intStartX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY);
				}
			}

			e.Graphics.DrawLine(penOneLine,1+intStartPrintX,c_intTimeTotalHeight+c_intGridHeightCount*c_intGridHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intTimeTotalHeight+c_intGridHeightCount*c_intGridHeight+intStartPrintY);
			#endregion

			#region 画参数坐标信息
			//画参数坐标值
			int intParamCount = c_intGridHeightCount/5;					
			for(int i=1;i<intParamCount;i++)
			{
				//外出图标先不画

				//呼吸
				int intBreath = 80-i*10;
				bruTemp.Color = m_clrBreathParamsText;
				e.Graphics.DrawString(intBreath.ToString(),fntRecordDateText,bruTemp,intTextWidth-intTextWidth/3-44+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2+intStartPrintY);


				//脉搏（心率）
				int intPulse = 200-i*20;
				bruTemp.Color = Color.Red;
				e.Graphics.DrawString(intPulse.ToString(),fntRecordDateText,bruTemp,intTextWidth-intTextWidth/3-27+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2+intStartPrintY);

				//温度
				int intTemp = 43-i;
				bruTemp.Color = Color.Black;
				e.Graphics.DrawString(intTemp.ToString("0°"),fntRecordDateText,bruTemp,intTextWidth-20+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2+intStartPrintY);
			}

			//外出图标先不画

			//画参数描述
			//呼吸
			bruTemp.Color = m_ClrBreathParamsText;				
			e.Graphics.DrawString("呼吸",fntRecordDateTextTop,bruTemp,intTextWidth-intTextWidth/3-43+intStartPrintX,c_intRecordDateHeight+c_intInpateintDateHeight+c_intSpecialDateHeight+c_intTimeHeight+1+intStartPrintY);
			e.Graphics.DrawString("(次/分)",fntRecordDateTextTop,bruTemp,intTextWidth-intTextWidth/3-46+intStartPrintX,c_intRecordDateHeight+c_intInpateintDateHeight+c_intSpecialDateHeight+c_intTimeHeight+18+intStartPrintY);
			


			//画参数描述
			//脉搏（心率）
			bruTemp.Color = Color.Red;				
			e.Graphics.DrawString("脉搏",fntRecordDateTextTop,bruTemp,intTextWidth-intTextWidth/3-20+intStartPrintX,c_intRecordDateHeight+c_intInpateintDateHeight+c_intSpecialDateHeight+c_intTimeHeight+1+intStartPrintY);
			e.Graphics.DrawString("(次/分)",fntRecordDateTextTop,bruTemp,intTextWidth-intTextWidth/3-18+intStartPrintX,c_intRecordDateHeight+c_intInpateintDateHeight+c_intSpecialDateHeight+c_intTimeHeight+18+intStartPrintY);
			
			//体温
			bruTemp.Color = Color.Black;
			e.Graphics.DrawString("体温",fntRecordDateTextTop,bruTemp,intTextWidth-25+intStartPrintX+3,c_intRecordDateHeight+c_intSpecialDateHeight+c_intInpateintDateHeight+c_intTimeHeight+1+intStartPrintY);
			e.Graphics.DrawString("  ℃",fntRecordDateTextTop,bruTemp,intTextWidth-20+intStartPrintX+2,c_intRecordDateHeight+c_intSpecialDateHeight+c_intInpateintDateHeight+c_intTimeHeight+18+intStartPrintY);			
			#endregion			

			#region 画数值图标
			bruTemp.Color = Color.Black;

			e.Graphics.DrawString("口表",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+15+intStartPrintY);
			e.Graphics.DrawImage(m_imgMouthTemperature,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+16+intStartPrintY,c_intGridHeight,c_intGridHeight);
			e.Graphics.DrawString("腋表",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+19+c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgArmpitTemperature,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+21+c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			e.Graphics.DrawString("肛表",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+23+2*c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgAnusTemperature,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+25+2*c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			
			e.Graphics.DrawString("脉搏",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+15+intStartPrintY);
			e.Graphics.DrawImage(m_imgPulse,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+16+intStartPrintY,c_intGridHeight,c_intGridHeight);
			e.Graphics.DrawString("心率",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+19+c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgHeartRate,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+21+c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			
			e.Graphics.DrawString("呼吸",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+35+c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgBreath,36+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+39+c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			
			#endregion

			//画呼吸
			bruTemp.Color = Color.Black  ;
			//e.Graphics.DrawString("呼吸(次/分)",fntRecordDateText,bruTemp,6+intStartPrintX,c_intGridTotalHeight+4+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intBreathTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);

			penOneWidthLine.Color = Color.Black;
			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			if(p_intLeftItem > 1)
			{
				//画输入液量
				e.Graphics.DrawString("输入液量(ml)",fntRecordDateText,bruTemp,1+intStartPrintX,c_intBreathTotalHeight+4+intStartPrintY);			
				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intInputTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intInputTotalHeight+intStartPrintY);

				if(p_intLeftItem > 2)
				{
					//画排出量
					e.Graphics.DrawString("排出量",fntRecordDateText,bruTemp,1+intStartPrintX,c_intInputTotalHeight+6+intStartPrintY,stfDirectionVertical);
					e.Graphics.DrawLine(penOneWidthLine,16+intStartPrintX,c_intInputTotalHeight+intStartPrintY,16+intStartPrintX,c_intPeeTotalHeight+intStartPrintY);

					e.Graphics.DrawString("大便(次)",fntRecordDateText,bruTemp,25+intStartPrintX,c_intInputTotalHeight+4+intStartPrintY);			
					e.Graphics.DrawLine(penOneWidthLine,16+intStartPrintX,c_intDejectaTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intDejectaTotalHeight+intStartPrintY);
					
					if(p_intLeftItem > 3)
					{
						e.Graphics.DrawString("小便(次)",fntRecordDateText,bruTemp,25+intStartPrintX,c_intDejectaTotalHeight+4+intStartPrintY);			
						e.Graphics.DrawLine(penOneWidthLine,16+intStartPrintX,c_intOutStreamTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intOutStreamTotalHeight+intStartPrintY);
						
						if(p_intLeftItem > 4)
						{
							e.Graphics.DrawString("尿量(ml)",fntRecordDateText,bruTemp,17+intStartPrintX,c_intOutStreamTotalHeight+4+intStartPrintY);			
							e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intPeeTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intPeeTotalHeight+intStartPrintY);

							if(p_intLeftItem > 5)
							{
								//画血压
								e.Graphics.DrawString("血压(mmHg)",fntRecordDateText,bruTemp,6+intStartPrintX,c_intPeeTotalHeight+4+intStartPrintY);			
								e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intPressureTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intPressureTotalHeight+intStartPrintY);
			
								if(p_intLeftItem > 6)
								{
									//画体重
									e.Graphics.DrawString("体重(kg)",fntRecordDateText,bruTemp,6+intStartPrintX,c_intPressureTotalHeight+4+intStartPrintY);			
									e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intWeightTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intWeightTotalHeight+intStartPrintY);

									if(p_intLeftItem > 7)
									{
										//画皮试
										e.Graphics.DrawString("皮试",fntRecordDateText,bruTemp,6+intStartPrintX,c_intWeightTotalHeight+4+intStartPrintY);			
////										e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intSkinTestTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intSkinTestTotalHeight+intStartPrintY);
//										e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intSkinTestTotalHeight,intRecordWidth-2+intStartPrintX,c_intSkinTestTotalHeight);
//			
										e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intSkinTestTotalHeight,intRecordWidth-2+intStartPrintX,c_intSkinTestTotalHeight);
										e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intSkinTestTotalHeight,intRecordWidth-2+intStartPrintX,c_intSkinTestTotalHeight);
			
										if(p_intLeftItem > 8)
										{
											//画其它
//											if(m_strOtherName == null)
//											{
//												e.Graphics.DrawString("特殊记录",fnt6PtText,bruTemp,1+intStartPrintX,c_intSkinTestTotalHeight+4+intStartPrintY);						
												e.Graphics.DrawString("特殊记录",fnt6PtText,bruTemp,1+intStartPrintX,c_intSkinTestTotalHeight);						
//											}
//											else
//											{
//												//e.Graphics.DrawString("特殊记录",fnt6PtText,bruTemp,1+intStartPrintX,c_intSkinTestTotalHeight+4+intStartPrintY);	
//												e.Graphics.DrawString("特殊记录",fnt6PtText,bruTemp,1+intStartPrintX,c_intSkinTestTotalHeight);						
//											}
//											e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intOtherTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intOtherTotalHeight+intStartPrintY);
//											e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intOtherTotalHeight,intRecordWidth-2+intStartPrintX,c_intOtherTotalHeight);
										}
									}
								}
							}
						}
					}
				}
			}

			fntRecordDateText = new Font("",c_flt12PointFontSize);
			fntSpecialDateText = new Font("",c_flt10PointFontSize);
			
			#region 画数值
			
			intStartPrintX -= intPreWidth;
				for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
				{
					intLastIndex = i;
					clsThreeMeasureDateRecord objRecord = m_objDateManager[i];

					bruTemp.Color = Color.Black;
					//画日期
					string strRecordDate = "";
					if(i%7==0 || dtmPreDateForDateRecord.Year != objRecord.m_dtmRecordDate.Year)
					{
						strRecordDate = objRecord.m_dtmRecordDate.ToString("yy-M-d");
					}
					else
					{
						if(dtmPreDateForDateRecord.Month != objRecord.m_dtmRecordDate.Month)
						{
							strRecordDate = objRecord.m_dtmRecordDate.ToString("   M-d");
						}
						else
						{
							strRecordDate = objRecord.m_dtmRecordDate.ToString("     %d");
						}
					}
					dtmPreDateForDateRecord = objRecord.m_dtmRecordDate;

					e.Graphics.DrawString(strRecordDate,fntSpecialDateText,bruTemp,intTextWidth+i*c_intGridHeight*6+4+intStartPrintX,2+intStartPrintY);

					//画住院日数
					string strInpateintNum="";
					DateTime dtmCurrDate = DateTime.Parse(objRecord.m_dtmRecordDate.ToString("yyyy-MM-dd 00:00:00"));
					DateTime dtmInDate = DateTime.Parse(m_dtmInPatientDate.ToString("yyyy-MM-dd 00:00:00"));
					System.TimeSpan diff=dtmCurrDate.Subtract(dtmInDate);
					strInpateintNum = ((int)diff.TotalDays+1).ToString();
					e.Graphics.DrawString("     "+ strInpateintNum,fntSpecialDateText,bruTemp,intTextWidth+i*c_intGridHeight*6+4+intStartPrintX,c_intRecordDateHeight+2+intStartPrintY);
				
//					bruTemp.Color = Color.Red;

					//画手术或产后日期
			
					strSpecialDataText="";
					if(objRecord.m_objSpecialDate != null && arlDate.Count > 0)
					{
                        //if(objRecord.m_objSpecialDate.m_blnIsNewStart)
                        //{
                        //    //新的手术日期
                        //    intCurrentDateDiff = 0;
                        //    dtmPreDateForSpecialDate = objRecord.m_dtmRecordDate;
                        //    intSpecialNewStartTimes++;
						
                        //    //在翻页时如果没有增加此日期则往集合中加
                        //    if(!arlDate.Contains(objRecord.m_objSpecialDate.m_dtmSpecialDate))
                        //        arlDate.Add(objRecord.m_objSpecialDate.m_dtmSpecialDate);//放入集合中
                        //    arlDate.Sort();
                        //    string strDateDiff="";
                        //    DateTime dtTemp;
                        //    for(int k=0;k<arlDate.Count ;k++)
                        //    {
                        //        dtTemp=(DateTime)arlDate[k];
                        //        TimeSpan objTS=(TimeSpan)(objRecord.m_dtmRecordDate-dtTemp);
                        //        //如果大于当前日期则不往下循环
                        //        if(objRecord.m_dtmRecordDate.DayOfYear<dtTemp.DayOfYear)
                        //            break;
                        //        if(objRecord.m_dtmRecordDate.ToString("yyyy-MM-dd")!=dtTemp.ToString("yyyy-MM-dd"))
                        //            intCurrentDateDiff=objTS.Days+1 ;
                        //        else
                        //            intCurrentDateDiff=objTS.Days;
                        //        strDateDiff=intCurrentDateDiff.ToString();
                        //        if(intCurrentDateDiff==0)
                        //            strDateDiff="";
                        //        if(k!=0)
                        //            strSpecialDataText=strDateDiff+"/"+strSpecialDataText;
                        //        else
                        //            strSpecialDataText=strDateDiff+strSpecialDataText;
							
                        //    }

                        strSpecialDataText = m_strIsOperationOutOfDate(objRecord.m_dtmRecordDate);
                        Font fntOperation = new Font("宋体", c_flt10PointFontSize); ;
                        if (strSpecialDataText.Length > 10)
                            fntOperation = new Font("宋体", c_flt7PointFontSize);
                        e.Graphics.DrawString(strSpecialDataText, fntOperation, bruTemp, intTextWidth + i * c_intGridHeight * 6 + c_intGridHeight * 2 + intStartPrintX - 20, c_intInpateintTotalHeight + 1 + intStartPrintY);

                        fntOperation.Dispose();
                        //}
                        //else if(intSpecialNewStartTimes > 0)
                        //{
                        //    //没有手术时
			
                        //    string strDateDiff="";
                        //    DateTime dtTemp;
                        //    for(int k=0;k<arlDate.Count ;k++)
                        //    {
                        //        dtTemp=(DateTime)arlDate[k];
                        //        TimeSpan objTS=(TimeSpan)(objRecord.m_dtmRecordDate-dtTemp);
                        //        //如果大于当前日期则不往下循环
                        //        if(objRecord.m_dtmRecordDate.DayOfYear<dtTemp.DayOfYear)
                        //            break;
                        //        if(objRecord.m_dtmRecordDate.ToString("yyyy-MM-dd")!=dtTemp.ToString("yyyy-MM-dd"))
                        //            intCurrentDateDiff=objTS.Days+1 ;
                        //        else
                        //            intCurrentDateDiff=objTS.Days;
                        //        strDateDiff=intCurrentDateDiff.ToString();
                        //        if(intCurrentDateDiff==0)
                        //            strDateDiff="";
                        //        if(k!=0)
                        //            strSpecialDataText=strDateDiff+"/"+strSpecialDataText;
                        //        else
                        //            strSpecialDataText=strDateDiff+strSpecialDataText;
							
                        //    }
					
                        //    e.Graphics.DrawString(strSpecialDataText,fntSpecialDateText,bruTemp,intTextWidth+i*c_intGridHeight*6+c_intGridHeight*2+intStartPrintX-20,c_intInpateintTotalHeight+1+intStartPrintY);

                        //}
				
					}
		


					#region 画事件
					bruTemp.Color = Color.Red;
					penOneWidthLine.Color = Color.Red;
					int intPreTimeIndex = -1;
					int intActureIndex = 0;
					int c_intDrawEventHeight = 28+24+24+60;
					for(int j2=0;j2<objRecord.m_arlEvent.Count;j2++)
					{
						clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[j2];

						if(objEvent.m_objDeleteInfo != null && objEvent.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

                        //if(objEvent.m_intTimeIndex > intPreTimeIndex)
                        //{
                        //    intActureIndex = objEvent.m_intTimeIndex;
                        //}
                        //else
							intActureIndex = objEvent.m_intNearTimeIndex;

						intPreTimeIndex = objEvent.m_intTimeIndex;						
						if(objEvent.m_enmEventType.ToString()=="请假")
						{
							bruTemp.Color =Color.Blue  ;
							e.Graphics.DrawString(objEvent.m_enmEventType.ToString(),fntSymbol,bruTemp,intTextWidth+c_intGridHeight*(i*6+intActureIndex)-1+intStartPrintX,c_intLowEventTextStartHeight-1+intStartPrintY,stfDirectionVertical);
					
						}
						else
						{
							bruTemp.Color =Color.Red;
							if(objEvent.m_enmEventType.ToString().Trim()!="外出")
							{
								//e.Graphics.DrawString(objEvent.m_enmEventType.ToString(),fntSymbol,bruTemp,intTextWidth+c_intGridHeight*(i*6+intActureIndex)-1+intStartPrintX,c_intTimeTotalHeight-1+intStartPrintY,stfDirectionVertical);
								e.Graphics.DrawString(objEvent.m_enmEventType.ToString(),fntSymbol,bruTemp,intTextWidth+c_intGridHeight*(i*6+intActureIndex)-3+intStartPrintX,c_intDrawEventHeight-1+intStartPrintY,stfDirectionVertical);
								if(objEvent.m_strTime != "")
								{
									e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intDrawEventHeight+2*c_intGridHeight+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intDrawEventHeight+4*c_intGridHeight+intStartPrintY);
									//e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+2*c_intGridHeight+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+3*c_intGridHeight+intStartPrintY);
									for(int k3=0;k3<objEvent.m_strTime.Length;k3++)
									{
										e.Graphics.DrawString(objEvent.m_strTime[k3].ToString(),fntSymbol,bruTemp,intTextWidth+c_intGridHeight*(i*6+intActureIndex)-1+intStartPrintX,c_intDrawEventHeight+(4+k3)*c_intGridHeight-1+intStartPrintY);
										//e.Graphics.DrawString(objEvent.m_strTime[k3].ToString(),fntSymbol,bruTemp,intTextWidth+c_intGridHeight*(i*6+intActureIndex)-1+intStartPrintX,c_intTimeTotalHeight+(3+k3)*c_intGridHeight-1+intStartPrintY);
									}
								}
							}
						}

						if(objEvent.m_objDeleteInfo != null)
						{
							int intLen = 3+objEvent.m_strTime.Length;

							penOneWidthLine.Color = m_clrDST;
							//						e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+intLen*c_intGridHeight+intStartPrintY);
							//						e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+3+intStartPrintX,c_intTimeTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+3+intStartPrintX,c_intTimeTotalHeight+intLen*c_intGridHeight+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intDrawEventHeight+intLen*c_intGridHeight+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+3+intStartPrintX,c_intTimeTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+3+intStartPrintX,c_intDrawEventHeight+intLen*c_intGridHeight+intStartPrintY);
					
							penOneWidthLine.Color = Color.Red;
						}
					}
					#endregion

					bruTemp.Color = Color.Blue;
					int intCurrentBreathIndex = -1;
					int intDeleteBreath = 0;
					penOneWidthLine.Color = m_clrDST;
					byte bytBreathIndex = 0;

					//画皮试(只画PPD)	
					penOneWidthLine.Color = m_clrDST;
					for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
					{
						clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

						if(objSkinValue.m_objDeleteInfo != null && objSkinValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						if(objSkinValue.m_intTimeIndex >= 0)
						{
							int intSkinTextY = c_intLowEventTextStartHeight;
							if((bytBreathIndex & (1<<objSkinValue.m_intTimeIndex)) != 0)
							{
								intSkinTextY += 6*c_intGridHeight;
							}
							//ppd					
							SizeF szfName = e.Graphics.MeasureString(objSkinValue.m_strMedicineName,fntSpecialDateText,10,stfDirectionVertical);

							string strValue = "";
							bruTemp.Color = Color.Blue ;
							e.Graphics.DrawString(objSkinValue.m_strMedicineName,fntSpecialDateText,bruTemp,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)-4+intStartPrintX,intSkinTextY+intStartPrintY,stfDirectionVertical);
							if(objSkinValue.m_intBadStatus == 1)
							{
								strValue = objSkinValue.m_strPDDValue;

								bruTemp.Color = Color.Red;
								e.Graphics.DrawString(strValue,fntSpecialDateText,bruTemp,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)-2+intStartPrintX,intSkinTextY+(int)szfName.Height+intStartPrintY,stfDirectionVertical);
							}
							else if(objSkinValue.m_intBadStatus == 0)
							{
								strValue = "(-)";
								bruTemp.Color = Color.Blue ;
								e.Graphics.DrawString(strValue,fntSpecialDateText,bruTemp,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)-2+intStartPrintX,intSkinTextY+(int)szfName.Height+intStartPrintY,stfDirectionVertical);
							}		
				
							if(objSkinValue.m_objDeleteInfo != null)
							{
								SizeF szfDesc = e.Graphics.MeasureString(objSkinValue.m_strMedicineName+strValue,fntRecordDateText,10,stfDirectionVertical);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+intStartPrintX,intSkinTextY+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+intStartPrintX,intSkinTextY+szfDesc.Height+intStartPrintY);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+3+intStartPrintX,intSkinTextY+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+3+intStartPrintX,intSkinTextY+szfDesc.Height+intStartPrintY);
							}
						}
					}

					if(p_intLeftItem > 1)
					{
						//画输入液量
						bruTemp.Color = Color.Blue ;
						penOneWidthLine.Color = m_clrDST;
						float fltInputTotalHeight = 0;
						for(int j2=0;j2<objRecord.m_arlInputValue.Count;j2++)
						{
							clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];

							if(objInputValue.m_objDeleteInfo != null && objInputValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
							{
								continue;
							}

							string strDesc = objInputValue.m_fltValue.ToString("0.00");					
							e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intBreathTotalHeight+(int)fltInputTotalHeight+2+intStartPrintY);
					
							SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

							if(objInputValue.m_objDeleteInfo != null)
							{
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intBreathTotalHeight+(int)fltInputTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intBreathTotalHeight+(int)fltInputTotalHeight+10+intStartPrintY);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intBreathTotalHeight+(int)fltInputTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intBreathTotalHeight+(int)fltInputTotalHeight+13+intStartPrintY);
							}

							fltInputTotalHeight += szfDesc.Height;
						}
					
						if(p_intLeftItem > 2)
						{
							//画大便
							penOneWidthLine.Color = m_clrDST;
							float fltDejectaTotalHeight = 0;
							for(int j2=0;j2<objRecord.m_arlDejectaValue.Count;j2++)
							{
								clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];

								if(objDejectaValue.m_objDeleteInfo != null && objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
								{
									continue;
								}

								string strDesc = objDejectaValue.m_strDesc;					
								e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+2+intStartPrintY);
					
								SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

								if(objDejectaValue.m_objDeleteInfo != null)
								{
									e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+10+intStartPrintY);
									e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+13+intStartPrintY);
								}

								fltDejectaTotalHeight += szfDesc.Height;
							}

							if(p_intLeftItem > 3)
							{
								//画小便
									penOneWidthLine.Color = m_clrDST;
			
									float fltOutStreamTotalHeight=0;
									//float fltOutStreamTotalHeight = 0;
									for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
									{
										clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

										if(objOutStreamValue.m_objDeleteInfo != null && objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
										{
											continue;
										}

										string strDesc = "";
	
										if(objOutStreamValue.m_enmIsIrretention == enmIrretention.一般)
											strDesc = objOutStreamValue.m_fltValue.ToString("0");
										else if(objOutStreamValue.m_enmIsIrretention == enmIrretention.失禁)
										{
											strDesc = "*";
										}
										else if(objOutStreamValue.m_enmIsIrretention == enmIrretention.导尿)
											strDesc = "C";
										else if(objOutStreamValue.m_enmIsIrretention == enmIrretention.留置导尿)
											strDesc = "C+";
										e.Graphics.DrawString(strDesc,fntSpecialDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intDejectaTotalHeight+(int)fltOutStreamTotalHeight+2+intStartPrintY);
					
										SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

										if(objOutStreamValue.m_objDeleteInfo != null)
										{
											e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intDejectaTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intDejectaTotalHeight+10+intStartPrintY);
											e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intDejectaTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intDejectaTotalHeight+13+intStartPrintY);
										}

										fltOutStreamTotalHeight += szfDesc.Height;
									}


								if(p_intLeftItem > 4)
								{
									//画尿量
									penOneWidthLine.Color = m_clrDST;
				
									float fltPeeTotalHeight=0;
									for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
									{
										clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

										if(objPeeValue.m_objDeleteInfo != null && objPeeValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
										{
											continue;
										}

										string strDesc = objPeeValue.m_fltValue.ToString("0.00");

										e.Graphics.DrawString(strDesc,fntSpecialDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intOutStreamTotalHeight+(int)fltPeeTotalHeight+2+intStartPrintY);
					
										SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

										if(objPeeValue.m_objDeleteInfo != null)
										{
											e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intOutStreamTotalHeight+(int)fltPeeTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intOutStreamTotalHeight+(int)fltPeeTotalHeight+10+intStartPrintY);
											e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intOutStreamTotalHeight+(int)fltPeeTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intOutStreamTotalHeight+(int)fltPeeTotalHeight+13+intStartPrintY);
										}

										fltPeeTotalHeight += szfDesc.Height;
									}
									if(p_intLeftItem > 5)
									{
										//画血压
										penOneWidthLine.Color = m_clrDST;
										float fltPressureTotalHeight = 0;
										for(int j2=0;j2<objRecord.m_arlPressureValue1.Count;j2++)
										{
											clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

											if(objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
											{
												continue;
											}

											string strDesc = objPressureValue.m_fltSystolicValue.ToString("0")+"/"+objPressureValue.m_fltDiastolicValue.ToString("0");
                                            e.Graphics.DrawString(strDesc, fntGridText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intPeeTotalHeight + (int)fltPressureTotalHeight + 2 + intStartPrintY);
					
											SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntGridText);

											if(objPressureValue.m_objDeleteInfo != null)
											{
												e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+11+intStartPrintY,intTextWidth+c_intGridHeight*i*6+(int)szfDesc.Width+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+11+intStartPrintY);
												e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+(int)szfDesc.Width+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+13+intStartPrintY);
											}

											fltPressureTotalHeight += szfDesc.Height+2;
										}
                                        //fltPressureTotalHeight = 0;
										for(int j2=0;j2<objRecord.m_arlPressureValue2.Count;j2++)
										{
											clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[j2];

											if(objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
											{
												continue;
											}

											string strDesc = objPressureValue.m_fltSystolicValue.ToString("0")+"/"+objPressureValue.m_fltDiastolicValue.ToString("0");
											e.Graphics.DrawString(strDesc,fntGridText,bruTemp,intTextWidth+c_intGridHeight*(i*6+2)+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+intStartPrintY);
					
											SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntGridText);

											if(objPressureValue.m_objDeleteInfo != null)
											{
												e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+2)+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+11+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+2)+(int)szfDesc.Width+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+11+intStartPrintY);
												e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*(i*6+2)+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*(i*6+2)+(int)szfDesc.Width+intStartPrintX,c_intPeeTotalHeight+(int)fltPressureTotalHeight+13+intStartPrintY);
											}

											fltPressureTotalHeight += szfDesc.Height+2;
										}

										if(p_intLeftItem > 6)
										{
											//画体重
											penOneWidthLine.Color = m_clrDST;
											float fltWeightTotalHeight = 0;
											for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
											{
												clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

												if(objWeightValue.m_objDeleteInfo != null && objWeightValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
												{
													continue;
												}

												string strDesc = "";
												if(objWeightValue.m_enmWeightType == enmThreeMeasureWeightType.一般)
												{
													strDesc = objWeightValue.m_fltValue.ToString("0.00");
													e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPressureTotalHeight+(int)fltWeightTotalHeight+2+intStartPrintY);
												}
												else
												{
													strDesc = objWeightValue.m_enmWeightType.ToString();
													e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPressureTotalHeight+(int)fltWeightTotalHeight+2+intStartPrintY);
												}

					
												SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

												if(objWeightValue.m_objDeleteInfo != null)
												{
													e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPressureTotalHeight+(int)fltWeightTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intPressureTotalHeight+(int)fltWeightTotalHeight+10+intStartPrintY);
													e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPressureTotalHeight+(int)fltWeightTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intPressureTotalHeight+(int)fltWeightTotalHeight+13+intStartPrintY);
												}

												fltWeightTotalHeight += szfDesc.Height;
											}
				
											if(p_intLeftItem > 7)
											{
												//画皮试(不只画PPD)		
												bruTemp.Color = Color.Blue ;
												float intSkinTestNotPPDCount = 0;
												penOneWidthLine.Color = m_clrDST;
//												Font fntTemp = new Font("",c_flt7PointFontSize);
												for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
												{
													clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

													if(objSkinValue.m_objDeleteInfo != null && objSkinValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
													{
														continue;
													}

													if(objSkinValue.m_intTimeIndex >= 0)
													{
													}
													else
													{
														//非ppd



														string strValue = "";
														bruTemp.Color = m_clrSkinTest;
														//e.Graphics.DrawString(objSkinValue.m_strMedicineName,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+2);
														strValue =objSkinValue.m_strMedicineName;
														if(objSkinValue.m_intBadStatus == 1)
														{
															strValue += "(";
															for(int z1=0;z1<objSkinValue.m_intBadCount;z1++)
															{
																strValue +="+";
															}
															strValue += ")";
                                                            bruTemp.Color = Color.Blue;
															//e.Graphics.DrawString(strValue,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height);
														}
														else if(objSkinValue.m_intBadStatus == 0)
														{
															strValue += "(-)";
															bruTemp.Color = m_clrSkinTest;
															//e.Graphics.DrawString(strValue,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height);
														}
                                                        else if (objSkinValue.m_intBadStatus == 2)
                                                        {
                                                            strValue += "(" + objSkinValue.m_StrOtherResult + ")";
                                                            bruTemp.Color = Color.Blue;
                                                        }
														string strText=strValue;
														SizeF szfName = e.Graphics.MeasureString(strText,fntSymbol);
						

														Font fntOtherText = new Font("",c_flt7PointFontSize);
														float lngRowChar=0;
														char strTempDraw;
														float fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+intStartPrintX;
														float intloop=0;
														bruTemp.Color =Color.Blue ;
														for(int j=0;j<strText.Length;j++)
														{
						
															strTempDraw=strText[j];
															lngRowChar=j*fntTemp.Size;
															fltXpoint =fltXpoint+fntTemp.Size+4;
															//大于一行就换行
															if(lngRowChar-intloop>=35)
															{
																//行高
																intloop=lngRowChar;
																intSkinTestNotPPDCount=intSkinTestNotPPDCount+fntTemp.Size+2;
																fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+intStartPrintX;
															}

															e.Graphics.DrawString(strTempDraw.ToString(),fntTemp,bruTemp,fltXpoint-40,c_intWeightTotalHeight+intSkinTestNotPPDCount+intStartPrintY);//intSkinTestNotPPDCount*(int)szfName.Height);
														}
														intSkinTestNotPPDCount =intSkinTestNotPPDCount+10;		
				
														if(objSkinValue.m_objDeleteInfo != null)
														{
															SizeF szfDesc = e.Graphics.MeasureString(objSkinValue.m_strMedicineName+strValue,fntSymbol);
															e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+10+intStartPrintY);
															e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+13+intStartPrintY);
														}

														intSkinTestNotPPDCount++;
													
													}
													//arlSkinHeightMax.Add(intSkinTestNotPPDCount);
												
												}
												//c_intSkinTestTotalHeight=c_intWeightTotalHeight+(int)intSkinTestNotPPDCount+10+intStartPrintY;

												//
											

												if(p_intLeftItem > 8)
												{				
													//画其它
													bruTemp.Color = Color.Blue ;
													penOneWidthLine.Color = m_clrDST;
													float intOtherRowCount=0;
													for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
													{
//													clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];
//					
//													if(objOtherValue.m_objDeleteInfo != null && objOtherValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
//													{
//														continue;
//													}
														clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];

														Font fntOtherText = new Font("",c_flt7PointFontSize);
					
					
														string strText="";
														string strItemValue="";

														//要画的文本
                                                        //try
                                                        //{
                                                        //    double fltValue=Math.Round((double)objOtherValue.m_fltOtherValue,2);
                                                        strItemValue = objOtherValue.m_StrOtherValue;
                                                        //}
                                                        //catch
                                                        //{
                                                        //    strItemValue=objOtherValue.m_fltOtherValue.ToString();
                                                        //}
														if(objOtherValue.m_strOtherItem.Trim()=="身高")
															strText=objOtherValue.m_strOtherItem+":"+strItemValue+"CM";
														else
															strText=objOtherValue.m_strOtherItem+":"+strItemValue;

														SizeF szfDesc = e.Graphics.MeasureString(strText,fntSpecialDateText);
													
														float lngRowChar=0;
														string strTempDraw="";
					
														char strTempDrawText;
														float fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX;
														float intloop=0;
														for(int k=0;k<strText.Length;k++)
														{
					
															strTempDrawText=strText[k];
															lngRowChar=k*fntTemp.Size;
															fltXpoint =fltXpoint+fntTemp.Size+4;
															//大于一行就换行
															if(lngRowChar-intloop>=35)
															{
																//行高
																intloop=lngRowChar;
																intOtherRowCount=intOtherRowCount+fntTemp.Size+2;
																fltXpoint=c_intTextTotleWidth+c_intGridHeight*i*6+intStartPrintX;
															}
															e.Graphics.DrawString(strTempDrawText.ToString(),fntTemp,bruTemp,fltXpoint-40,c_intSkinTestTotalHeight+(int)intOtherRowCount);//+intLoop* fntTemp.Height);
														}
													
														if(objOtherValue.m_objDeleteInfo != null)
														{
															string [] strValueArr = objOtherValue.m_strOther.Split('\r','\n');
															int intHeight = 0;
															for(int k3=0;k3<strValueArr.Length;k3++)
															{
																if(strValueArr[k3].Trim().Length == 0)
																	continue;

																SizeF szfDescPart = e.Graphics.MeasureString(strValueArr[k3],fntSpecialDateText);
																e.Graphics.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)intOtherRowCount+10+intHeight,c_intTextTotleWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,c_intSkinTestTotalHeight+(int)intOtherRowCount+10+intHeight);
																e.Graphics.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)intOtherRowCount+13+intHeight,c_intTextTotleWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,c_intSkinTestTotalHeight+(int)intOtherRowCount+13+intHeight);

																intHeight += 19;
															}
														}
											
														intOtherRowCount +=10;
												
													}
													//所有列的最高值
													arlOtherHeightMax.Add(intOtherRowCount);
											
													//c_intOtherTotalHeight=c_intSkinTestTotalHeight+(int)intOtherRowCount+20+intStartPrintY;
												
												
												}
											}
										}
									}
								}
							}
						}
					}
				}
				//画完所有其它之后再重置高度.皮试+其它+体重总高.
//				float fltOtherMaxHeight=c_intOtherHeight ;
//				arlOtherHeightMax.Sort();
//				if(arlOtherHeightMax.Count>0)
//					fltOtherMaxHeight=(float)arlOtherHeightMax[arlOtherHeightMax.Count -1];
//
//				//			float fltSkinMaxHeight=c_intSkinTestHeight ;
//				//			arlSkinHeightMax.Sort();
//				//			if(arlSkinHeightMax.Count >0)
//				//				fltSkinMaxHeight=(float)arlSkinHeightMax[arlSkinHeightMax.Count -1];
//				//			
//			
//				c_intSkinTestTotalHeight=c_intWeightTotalHeight+(int)fltSkinMaxHeight+intStartPrintY;
//				c_intOtherTotalHeight=c_intWeightTotalHeight+(int)fltSkinMaxHeight+(int)fltOtherMaxHeight+intStartPrintY;
//				m_intTotalHeight=c_intOtherTotalHeight;
			

				if(intLastIndex+1<m_objDateManager.m_IntRecordCount)
					p_blnHasMoreDate = true;
				else
					p_blnHasMoreDate = false;	

				//画脉搏
				penOneWidthLine.Color = Color.Red;
				bool blnFirstPulse = true;
				while(m_objPulseValueManager.m_blnNext())
				{
					clsThreeMeasurePulseValue objValue = m_objPulseValueManager.m_ObjCurrent;

					if(objValue.m_objDeleteInfo != null && objValue.m_objDeleteInfo.m_dtmDeleteTime <= objValue.m_objRecordDate.m_dtmFirstPrintDate)
					{
						continue;
					}

					float fltXPosTemp = objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth;

					if(fltXPosTemp+(float)c_intGridHeight/2f+3 < p_intStartX+intTextWidth)
						continue;
					else if(fltXPosTemp+(float)c_intGridHeight/2f+3 > intRecordWidth+p_intStartX)
						break;

					if(objValue.m_intCoverID == int.MinValue)
						e.Graphics.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,objValue.m_fltYPos-(float)c_intGridHeight/2f+intStartPrintY+60);

					if(!blnFirstPulse && objValue.m_blnLineToPreValue && objValue.m_objDeleteInfo == null )
					{
						clsThreeMeasurePulseValue objPreTempValue = objValue.m_objPreValue;

						while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objPulseValueManager.m_ObjHeader)
							objPreTempValue = objPreTempValue.m_objPreValue;

						if(objPreTempValue != m_objPulseValueManager.m_ObjHeader && m_blnCanPrintLine(objPreTempValue.m_dtmValueTime,objValue.m_dtmValueTime))
							e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY+60,objPreTempValue.m_fltXPos+intStartPrintX + intDiffWidth,objPreTempValue.m_fltYPos+intStartPrintY+60);
					}
					else if(blnFirstPulse)
					{
						blnFirstPulse = false;
					}
				}
				m_objPulseValueManager.m_mthReset();


				#region  //画外出三角形
				penOneWidthLine.Color = m_clrStayOutLine;
				bool blnFirstStayOut = true;
				while(m_objStayOutValueManager.m_blnNext())
				{
					clsThreeMeasureStayOutValue objValue = m_objStayOutValueManager.m_ObjCurrent;

					float fltXPosTemp = objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX ;

					if(fltXPosTemp+(float)c_intGridHeight/2f+3 < p_intStartX+intTextWidth)
						continue;
					else if(fltXPosTemp+(float)c_intGridHeight/2f+3 > intRecordWidth+p_intStartX)
						break;

					//三角形的Y坐标在37度处
					if(objValue.m_intCoverID == int.MinValue)
						e.Graphics.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,c_intStayOutHeight+intStartPrintY);
			

				}
				m_objStayOutValueManager.m_mthReset();
				#endregion

				#region  //画呼吸
				penOneWidthLine.Color = m_clrBreathLine;
				bool blnFirstBreath = true;
				while(m_objBreathValueManager.m_blnNext())
				{
					clsThreeMeasureBreathValue objValue = m_objBreathValueManager.m_ObjCurrent;
				
					if(objValue.m_objDeleteInfo != null && objValue.m_objDeleteInfo.m_dtmDeleteTime <= objValue.m_objRecordDate.m_dtmFirstPrintDate)
					{
						continue;
					}

					float fltXPosTemp = objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth;

					if(fltXPosTemp+(float)c_intGridHeight/2f+3 < p_intStartX+intTextWidth)
						continue;
					else if(fltXPosTemp+(float)c_intGridHeight/2f+3 > intRecordWidth+p_intStartX)
						break;

					if(objValue.m_enmBreathType == enmThreeMeasureBreathType.一般 || objValue.m_enmBreathType == enmThreeMeasureBreathType.特殊值)
					{
						if(objValue.m_intValue >= 10)
						{
							if(objValue.m_intCoverID == int.MinValue)
								e.Graphics.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,objValue.m_fltYPos-(float)c_intGridHeight/2f+intStartPrintY);

							if(!blnFirstBreath && objValue.m_objDeleteInfo == null )//&& objValue.m_blnLineToPreValue  && objValue.m_objPreValue != m_objPulseValueManager.m_ObjHeader && objValue.m_objPreValue.m_objDeleteInfo == null)
							{
								clsThreeMeasureBreathValue objPreTempValue = objValue.m_objPreValue;

								while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objBreathValueManager.m_ObjHeader)
									objPreTempValue = objPreTempValue.m_objPreValue;

								if(objPreTempValue != m_objBreathValueManager.m_ObjHeader && objPreTempValue.m_enmBreathType == enmThreeMeasureBreathType.一般 && m_blnCanPrintLine(objPreTempValue.m_dtmBreathTime,objValue.m_dtmBreathTime))
									e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY,objPreTempValue.m_fltXPos+intStartPrintX + intDiffWidth,objPreTempValue.m_fltYPos+intStartPrintY);
							}
							else if(blnFirstBreath)
							{
								blnFirstBreath = false;
							}
						}
						else
						{
							e.Graphics.DrawString(objValue.m_intValue.ToString(),fntSymbol,Brushes.Red,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,objValue.m_fltYPos-(float)c_intGridHeight/2f+intStartPrintY);
						}

					
					}
					else
					{
						bruTemp.Color =Color.Red ;// m_clrLowerEventText;
						string strType = objValue.m_enmBreathType.ToString();
						int intPreTemp = 0;
						//以下处理有小于10的值时的重叠问题
						if(objValue.m_objPreValue != null)
						{
							if(objValue.m_objPreValue.m_intValue < 10 && objValue.m_objPreValue.m_enmParamTime == objValue.m_enmParamTime)
							{
								if(objValue.m_objPreValue.m_enmParamTime ==enmParamTime.am4 || objValue.m_objPreValue.m_enmParamTime ==enmParamTime.am12 || objValue.m_objPreValue.m_enmParamTime ==enmParamTime.pm8)
									intPreTemp = 2;
								else
									intPreTemp = 1;
							}
						}
						if(objValue.m_objNextValue != null && intPreTemp == 0)
						{
							if(objValue.m_objNextValue.m_intValue < 10 && objValue.m_objNextValue.m_enmParamTime == objValue.m_enmParamTime)
							{
								if(objValue.m_objNextValue.m_enmParamTime ==enmParamTime.am4 || objValue.m_objNextValue.m_enmParamTime ==enmParamTime.am12 || objValue.m_objNextValue.m_enmParamTime ==enmParamTime.pm8)
									intPreTemp = 2;
								else
									intPreTemp = 1;
							}
						}
						for(int k3=0;k3<strType.Length;k3++)
						{
							e.Graphics.DrawString(strType[k3].ToString(),fntSymbol,bruTemp,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX+ intDiffWidth,c_intStayOutHeight+c_intGridHeight*(k3+intPreTemp+10)-1);
						}

				

						bruTemp.Color = m_clrDateValue;

					}
					objValue=null;
				}
				m_objBreathValueManager.m_mthReset();
				#endregion

				//画温度
				penOneWidthLine.Color = Color.Blue;
				penDotLine.Color = Color.Red;
				bruTemp.Color = Color.Red;
				bool blnFirstTemperature = true;
				while(m_objTemperatureValueManager.m_blnNext())
				{
					clsThreeMeasureTemperatureValue objValue = m_objTemperatureValueManager.m_ObjCurrent;

					if(objValue.m_objDeleteInfo != null && objValue.m_objDeleteInfo.m_dtmDeleteTime <= objValue.m_objRecordDate.m_dtmFirstPrintDate)
					{
						continue;
					}

					float fltXPosTemp = objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth;

					if(fltXPosTemp+(float)c_intGridHeight/2f+3 < p_intStartX+intTextWidth)
						continue;
					else if(fltXPosTemp+(float)c_intGridHeight/2f+3 > intRecordWidth+p_intStartX)
						break;

					if(objValue.m_fltValue >= 35)
					{
						e.Graphics.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,objValue.m_fltYPos-(float)c_intGridHeight/2f+intStartPrintY+60);//

						if(!blnFirstTemperature && objValue.m_blnLineToPreValue && objValue.m_objDeleteInfo == null )// && objValue.m_objPreValue != m_objPulseValueManager.m_ObjHeader && objValue.m_objPreValue.m_objDeleteInfo == null)
						{
							clsThreeMeasureTemperatureValue objPreTempValue = objValue.m_objPreValue;

							while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objTemperatureValueManager.m_ObjHeader)
								objPreTempValue = objPreTempValue.m_objPreValue;

							if(objPreTempValue != m_objTemperatureValueManager.m_ObjHeader
								&& objPreTempValue.m_fltValue >= 35 && m_blnCanPrintLine(objPreTempValue.m_dtmValueTime,objValue.m_dtmValueTime))
								e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY+60,objPreTempValue.m_fltXPos+intStartPrintX + intDiffWidth,objPreTempValue.m_fltYPos+intStartPrintY+60);
						}
						else if(blnFirstTemperature)
						{
							blnFirstTemperature = false;
						}

						for(int i=0;i<objValue.m_arlPhyscalDownValue.Count;i++)
						{
							clsThreeMeasureTemperaturePhyscalDownValue objDownValue = (clsThreeMeasureTemperaturePhyscalDownValue)objValue.m_arlPhyscalDownValue[i];

							e.Graphics.DrawImage(m_imgDownTemperature,objValue.m_fltXPos-(float)(c_intGridHeight-2)/2f+intStartPrintX + intDiffWidth,objDownValue.m_fltYPos-(float)(c_intGridHeight-2)/2f+intStartPrintY+60,c_intGridHeight-2,c_intGridHeight-2);

							if(objDownValue.m_objDeleteInfo == null)
							{
								if(objDownValue.m_fltValue <= objValue.m_fltValue)
									e.Graphics.DrawLine(penDotLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY+60,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objDownValue.m_fltYPos+intStartPrintY+60);
								else
								{
									penOneWidthLine.Color = Color.Red;
									e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY+60,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objDownValue.m_fltYPos+intStartPrintY+60);
									penOneWidthLine.Color = Color.Blue;
								}
							}
						}
					}
					else
					{					
						e.Graphics.DrawString("体",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight-1+intStartPrintY+60);
						e.Graphics.DrawString("温",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+c_intGridHeight-1+intStartPrintY+60);
						e.Graphics.DrawString("不",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+2*c_intGridHeight-1+intStartPrintY+60);
						e.Graphics.DrawString("升",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+3*c_intGridHeight-1+intStartPrintY+60);

						if(objValue.m_objDeleteInfo != null)
						{
							penOneWidthLine.Color = m_clrDST;
							e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+c_intGridHeight/2+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+intStartPrintY,objValue.m_fltXPos+c_intGridHeight/2+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+4*c_intGridHeight+intStartPrintY+60);
							e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+c_intGridHeight/2+3+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+intStartPrintY,objValue.m_fltXPos+c_intGridHeight/2+3+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+4*c_intGridHeight+intStartPrintY+60);
							penOneWidthLine.Color = Color.Blue;
						}
					}
				}
				m_objTemperatureValueManager.m_mthReset();
				#endregion
			
				m_ClrPulseSymbol = clrTemp;

				m_ClrTemperatureSymbol = clrTempBase;
                m_ClrDownTemperature = clrTempDown;

                if (penOneWidthLine != null)
                    penOneWidthLine.Dispose();
                if (penTwoWidthLine != null)
                    penTwoWidthLine.Dispose();
                if (penDotLine != null)
                    penDotLine.Dispose();
                if (fntRecordDateText != null)
                    fntRecordDateText.Dispose();
                if (fntSpecialDateText != null)
                    fntSpecialDateText.Dispose();
                if (fntGridText != null)
                    fntGridText.Dispose();
                if (fntSymbol != null)
                    fntSymbol.Dispose();
                if (fnt6PtText != null)
                    fnt6PtText.Dispose();
                if (bruTemp != null)
                    bruTemp.Dispose();
			}
		

		/// <summary>
		/// 打印因高度不足而剩余的内容
		/// </summary>
		/// <param name="p_intLeftItem">剩余的内容</param>
		/// <param name="p_intStartX">开始的X坐标</param>
		/// <param name="p_intStartY">开始的Y坐标</param>
		/// <param name="e">打印变量</param>
		/// <param name="p_intStartDateIndex">开始打印的日期</param>
		/// <param name="p_intRecordLength">打印的日期数目</param>
		/// <param name="p_intEndY">返回结束打印的Y坐标</param>
		/// <param name="p_blnHasMoreDate">返回是否还有日期信息没有打印</param>
		public void m_mthPrintLeftRecord(int p_intLeftItem,int p_intStartX,int p_intStartY,System.Drawing.Printing.PrintPageEventArgs e,int p_intStartDateIndex,int p_intRecordLength,out int p_intEndY,out bool p_blnHasMoreDate)
		{
			int intStartPrintX = p_intStartX;
			int intStartPrintY = p_intStartY;

			int intDiffWidth = -43;

			int intTextWidth = c_intTextTotleWidth + intDiffWidth;

			int intRecordHeight = c_intRecordDateHeight;
			
			#region Check Height
			if(p_intLeftItem < int.MaxValue)
			{
				intRecordHeight += c_intOtherHeight;

				if(p_intLeftItem < 8)
				{
					intRecordHeight += c_intSkinTestHeight;

					if(p_intLeftItem < 7)
					{
						intRecordHeight += c_intWeightHeight;

						if(p_intLeftItem < 6)
						{
							intRecordHeight += c_intPressureHeight;

							if(p_intLeftItem < 5)
							{
								intRecordHeight += c_intOutStreamHeight;

								if(p_intLeftItem < 4)
								{
									intRecordHeight += c_intPeeHeight;

									if(p_intLeftItem < 3)
									{
										intRecordHeight += c_intDejectaHeight;

										if(p_intLeftItem < 2)
										{
											intRecordHeight += c_intInputHeight;
										}
									}
								}
							}
						}
					}
				}
			}
			#endregion

			p_intEndY = intStartPrintY+intRecordHeight;

			int intRecordWidth = intTextWidth+p_intRecordLength*6*c_intGridHeight;
			
			Pen penOneWidthLine = new Pen(Color.Black);
			Pen penTwoWidthLine = new Pen(Color.Red,2);
			Pen penDotLine = new Pen(Color.BlueViolet);
			penDotLine.DashStyle = DashStyle.Dot;
			
			Font fntRecordDateText = new Font("",c_flt7PointFontSize);
			Font fntSpecialDateText = new Font("",c_flt5PointFontSize);
			Font fnt6PtText = new Font("",c_flt6PointFontSize);
			
			SolidBrush bruTemp = new SolidBrush(Color.Black);

			//画边框
			e.Graphics.DrawRectangle(penOneWidthLine,0+intStartPrintX,0+intStartPrintY,intRecordWidth,intRecordHeight);

			for(int i=0;i<p_intRecordLength;i++)
			{
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,intStartPrintY,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,intRecordHeight+intStartPrintY);
			}

			//画文字与值的分隔线
			penOneWidthLine.Color = Color.Black;
			e.Graphics.DrawLine(penOneWidthLine,intTextWidth+intStartPrintX,1+intStartPrintY,intTextWidth+intStartPrintX,intRecordHeight+intStartPrintY);		
			
			//画日期文字和与下面的分隔线
			e.Graphics.DrawString("  日\t期  ",fntRecordDateText,bruTemp,6+intStartPrintX,4+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intRecordDateHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intRecordDateHeight+intStartPrintY);

			int intPreHeight = c_intRecordDateHeight;
			penOneWidthLine.Color = Color.Black;
			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			if(p_intLeftItem == 1)
			{
				//画输入液量
				e.Graphics.DrawString("输入液量(ml)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			
				
				intPreHeight += c_intInputHeight;
				
				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);

			}

			if(p_intLeftItem <= 2)
			{
				//画排出量
//				e.Graphics.DrawString("排出量",fntRecordDateText,bruTemp,4+intStartPrintX,c_intInputTotalHeight+6+intStartPrintY,stfDirectionVertical);
//				e.Graphics.DrawLine(penOneWidthLine,32+intStartPrintX,c_intInputTotalHeight+intStartPrintY,32+intStartPrintX,c_intOutStreamTotalHeight+intStartPrintY);
				e.Graphics.DrawString("大便(次)",fntRecordDateText,bruTemp,35+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intDejectaHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
					
			if(p_intLeftItem <= 3)
			{
				e.Graphics.DrawString("小便(次)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);	
		
				intPreHeight += c_intOutStreamHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
						
			if(p_intLeftItem <= 4)
			{
				e.Graphics.DrawString("尿量(ml)",fntSpecialDateText,bruTemp,33+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intPeeHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}

			if(p_intLeftItem <= 5)
			{
				//画血压
				e.Graphics.DrawString("血压(mmHg)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intPressureHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
			
			if(p_intLeftItem <= 6)
			{
				//画体重
				e.Graphics.DrawString("体重(kg)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intWeightHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}

			if(p_intLeftItem <= 7)
			{
				//画皮试
				e.Graphics.DrawString("皮试",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intSkinTestHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
			
			if(p_intLeftItem <= 8)
			{
				//画其它
//				e.Graphics.DrawString("其它",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);						
				if(m_strOtherName == null)
				{
					e.Graphics.DrawString("其它",fnt6PtText,bruTemp,1+intStartPrintX,intPreHeight+4+intStartPrintY);						
				}
				else
				{
					e.Graphics.DrawString(m_strOtherName,fnt6PtText,bruTemp,1+intStartPrintX,intPreHeight+4+intStartPrintY);						
				}
			}			

			fntRecordDateText = new Font("",c_flt12PointFontSize);
			fntSpecialDateText = new Font("",c_flt10PointFontSize);
			
			#region 画数值
			//画日期及其子信息
			Font fntBigSymbol = new Font("",c_flt14PointFontSize);
			DateTime dtmPreDateForDateRecord = DateTime.Today;
			DateTime dtmPreDateForSpecialDate = DateTime.Today;
			int intPreWidth = p_intStartDateIndex*6*c_intGridHeight;
			intStartPrintX -= intPreWidth;
			int intLastIndex = 0;			
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				intLastIndex = i;
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];

				bruTemp.Color = Color.Black;
				//画日期
				string strRecordDate = "";
				if(i==0 || dtmPreDateForDateRecord.Year != objRecord.m_dtmRecordDate.Year)
				{
					strRecordDate = objRecord.m_dtmRecordDate.ToString("yy-M-d");
				}
				else
				{
					if(dtmPreDateForDateRecord.Month != objRecord.m_dtmRecordDate.Month)
					{
						strRecordDate = objRecord.m_dtmRecordDate.ToString("   M-d");
					}
					else
					{
						strRecordDate = objRecord.m_dtmRecordDate.ToString("     %d");
					}
				}
				dtmPreDateForDateRecord = objRecord.m_dtmRecordDate;

				e.Graphics.DrawString(strRecordDate,fntSpecialDateText,bruTemp,intTextWidth+i*c_intGridHeight*6+4+intStartPrintX,6+intStartPrintY);
				
				intPreHeight = c_intRecordDateHeight;

				if(p_intLeftItem == 1)
				{
					//画输入液量
					bruTemp.Color = Color.Black;
					penOneWidthLine.Color = m_clrDST;
					float fltInputTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlInputValue.Count;j2++)
					{
						clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];

						if(objInputValue.m_objDeleteInfo != null && objInputValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = objInputValue.m_fltValue.ToString("0.00");					
						e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltInputTotalHeight+2+intStartPrintY);
					
						SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

						if(objInputValue.m_objDeleteInfo != null)
						{
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltInputTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltInputTotalHeight+10+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltInputTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltInputTotalHeight+13+intStartPrintY);
						}

						fltInputTotalHeight += szfDesc.Height;
					}

					intPreHeight += c_intInputHeight;
				}

				if(p_intLeftItem <= 2)
				{
					//画大便
					penOneWidthLine.Color = m_clrDST;
					float fltDejectaTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlDejectaValue.Count;j2++)
					{
						clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];

						if(objDejectaValue.m_objDeleteInfo != null && objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = objDejectaValue.m_strDesc;					
						e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltDejectaTotalHeight+2+intStartPrintY);
					
						SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

						if(objDejectaValue.m_objDeleteInfo != null)
						{
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltDejectaTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltDejectaTotalHeight+10+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltDejectaTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltDejectaTotalHeight+13+intStartPrintY);
						}

						fltDejectaTotalHeight += szfDesc.Height;
					}

					intPreHeight += c_intDejectaHeight;
				}

				if(p_intLeftItem <= 3)
				{
					//画小便
					penOneWidthLine.Color = m_clrDST;
					float fltOutStreamTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
					{
						clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

						if(objOutStreamValue.m_objDeleteInfo != null && objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = "";
	
						if(objOutStreamValue.m_enmIsIrretention == enmIrretention.一般)
							strDesc = objOutStreamValue.m_fltValue.ToString("0");
						else if(objOutStreamValue.m_enmIsIrretention == enmIrretention.失禁)
						{
							strDesc = "*";
						}
						else if(objOutStreamValue.m_enmIsIrretention == enmIrretention.导尿)
							strDesc = "C";
						else if(objOutStreamValue.m_enmIsIrretention == enmIrretention.留置导尿)
							strDesc = "C+";
						e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX+20,intPreHeight+(int)fltOutStreamTotalHeight+2+intStartPrintY);
					
						SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

						if(objOutStreamValue.m_objDeleteInfo != null)
						{
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltOutStreamTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltOutStreamTotalHeight+10+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltOutStreamTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltOutStreamTotalHeight+13+intStartPrintY);
						}

						fltOutStreamTotalHeight += szfDesc.Height;
					}

					intPreHeight += c_intOutStreamHeight;
				}

				if(p_intLeftItem <= 4)
				{
					//画尿量
					penOneWidthLine.Color = m_clrDST;
					float fltPeeTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
					{
						clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

						if(objPeeValue.m_objDeleteInfo != null && objPeeValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = objPeeValue.m_fltValue.ToString("0.00");

						e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltPeeTotalHeight+2+intStartPrintY);
					
						SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

						if(objPeeValue.m_objDeleteInfo != null)
						{
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltPeeTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltPeeTotalHeight+10+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltPeeTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltPeeTotalHeight+13+intStartPrintY);
						}

						fltPeeTotalHeight += szfDesc.Height;
					}

					intPreHeight += c_intPeeHeight;
				}

				if(p_intLeftItem <= 5)
				{
					//画血压
					penOneWidthLine.Color = m_clrDST;
					float fltPressureTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlPressureValue1.Count;j2++)
					{
						clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

						if(objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = objPressureValue.m_fltSystolicValue.ToString("0")+"/"+objPressureValue.m_fltDiastolicValue.ToString("0");
						e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltPressureTotalHeight+2+intStartPrintY);
					
						SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

						if(objPressureValue.m_objDeleteInfo != null)
						{
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltPressureTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltPressureTotalHeight+10+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltPressureTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltPressureTotalHeight+13+intStartPrintY);
						}

						fltPressureTotalHeight += szfDesc.Height;
					}

					intPreHeight += c_intPressureHeight;
				}

				if(p_intLeftItem <= 6)
				{
					//画体重
					penOneWidthLine.Color = m_clrDST;
					float fltWeightTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
					{
						clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

						if(objWeightValue.m_objDeleteInfo != null && objWeightValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = "";
						if(objWeightValue.m_enmWeightType == enmThreeMeasureWeightType.一般)
						{
							strDesc = objWeightValue.m_fltValue.ToString("0.00");
							e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltWeightTotalHeight+2+intStartPrintY);
						}
						else
						{
							strDesc = objWeightValue.m_enmWeightType.ToString();
							e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltWeightTotalHeight+2+intStartPrintY);
						}

					
						SizeF szfDesc = e.Graphics.MeasureString(strDesc,fntRecordDateText);

						if(objWeightValue.m_objDeleteInfo != null)
						{
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltWeightTotalHeight+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltWeightTotalHeight+10+intStartPrintY);
							e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltWeightTotalHeight+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,intPreHeight+(int)fltWeightTotalHeight+13+intStartPrintY);
						}

						fltWeightTotalHeight += szfDesc.Height;
					}

					intPreHeight += c_intWeightHeight;
				}

				if(p_intLeftItem <= 7)
				{
					//画皮试	
					int intSkinTestNotPPDCount = 0;
					penOneWidthLine.Color = m_clrDST;
					for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
					{
						clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

						if(objSkinValue.m_objDeleteInfo != null && objSkinValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						if(objSkinValue.m_intTimeIndex >= 0)
						{

						}
						else
						{
							//非ppd
							SizeF szfName = e.Graphics.MeasureString(objSkinValue.m_strMedicineName,fntRecordDateText);

							string strValue = "";
							bruTemp.Color = Color.Black;
							e.Graphics.DrawString(objSkinValue.m_strMedicineName,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+2+intStartPrintY);
							if(objSkinValue.m_intBadStatus == 1)
							{
								strValue = "(+)";
								bruTemp.Color = Color.Red;
								e.Graphics.DrawString(strValue,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+intStartPrintY);
							}
							else if(objSkinValue.m_intBadStatus == 0)
							{
								strValue = "(-)";
								bruTemp.Color = Color.Black;
								e.Graphics.DrawString(strValue,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+intStartPrintY);
							}		
				
							if(objSkinValue.m_objDeleteInfo != null)
							{
								SizeF szfDesc = e.Graphics.MeasureString(objSkinValue.m_strMedicineName+strValue,fntRecordDateText);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+10+intStartPrintY,intTextWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+10+intStartPrintY);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+13+intStartPrintY,intTextWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+13+intStartPrintY);
							}

							intSkinTestNotPPDCount++;
						}
					}

					intPreHeight += c_intSkinTestHeight;
				}

				if(p_intLeftItem <= 8)
				{				
					//画其它
					bruTemp.Color = Color.Black;
					penOneWidthLine.Color = m_clrDST;
					float fltTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
					{
						clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];
					
						if(objOtherValue.m_objDeleteInfo != null && objOtherValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						SizeF szfDesc = e.Graphics.MeasureString(objOtherValue.m_strOther,fntSpecialDateText);
						Font fntOtherText = new Font("",c_flt5PointFontSize);
						//画文字说明
						string strDrawText=objOtherValue.m_strOtherItem.Trim().Length>6 ? objOtherValue.m_strOtherItem.Substring(0,6): objOtherValue.m_strOtherItem;
						strDrawText=strDrawText+":";
						e.Graphics.DrawString(strDrawText,fntOtherText,bruTemp,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+(int)fltTotalHeight+2+intStartPrintY);

						//画数值
						e.Graphics.DrawString(objOtherValue.m_strOther,fntOtherText,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX+30,c_intSkinTestTotalHeight+(int)fltTotalHeight+2);


//						SizeF szfDesc = e.Graphics.MeasureString(objOtherValue.m_strOther,fntRecordDateText);
//
//						e.Graphics.DrawString(objOtherValue.m_strOther,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+(int)fltTotalHeight+2+intStartPrintY);

						if(objOtherValue.m_objDeleteInfo != null)
						{
							string [] strValueArr = objOtherValue.m_strOther.Split('\r','\n');
							int intHeight = 0;
							for(int k3=0;k3<strValueArr.Length;k3++)
							{
								if(strValueArr[k3].Trim().Length == 0)
									continue;

								SizeF szfDescPart = e.Graphics.MeasureString(strValueArr[k3],fntRecordDateText);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+(int)fltTotalHeight+10+intStartPrintY+intHeight,intTextWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,intPreHeight+(int)fltTotalHeight+10+intStartPrintY+intHeight);
								e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+(int)fltTotalHeight+13+intStartPrintY+intHeight,intTextWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,intPreHeight+(int)fltTotalHeight+13+intStartPrintY+intHeight);
							
								intHeight += 19;
							}
						}

						fltTotalHeight += szfDesc.Height;
					}					
				}
			}

			if(intLastIndex+1<m_objDateManager.m_IntRecordCount)
				p_blnHasMoreDate = true;
			else
				p_blnHasMoreDate = false;				
			#endregion
		}

		/// <summary>
		/// 添加呼吸
		/// </summary>
		/// <param name="p_objValue">呼吸信息</param>
		/// <returns></returns>
		public bool m_blnAddBreathValue(clsThreeMeasureBreathValue p_objValue)
		{
			if(p_objValue == null)
				return false;
			
//			int intIndex = m_objDateManager.m_intIndexOf(p_objValue.m_dtmValueTime.Date);
			int intIndex = m_objDateManager.m_intIndexOf(p_objValue.m_dtmBreathTime.Date);//p_objValue.m_dtmModifyTime.Date);
			if(intIndex < 0)
				return false;

			p_objValue.m_fltXPos = c_intTextTotleWidth + intIndex*c_intGridHeight*6+(float)p_objValue.m_dtmBreathTime.TimeOfDay.TotalSeconds/(4*3600)*c_intGridHeight;
			if(p_objValue.m_intValue >= 10)
				p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(80-p_objValue.m_intValue)/90f)*(float)(c_intGridHeight*c_intGridHeightCount);
			else
			{
				int intTempValue = 19;
				if(p_objValue.m_enmParamTime ==enmParamTime.am4 || p_objValue.m_enmParamTime ==enmParamTime.am12 || p_objValue.m_enmParamTime ==enmParamTime.pm8)
					intTempValue = 17;
				p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(80-intTempValue)/90f)*(float)(c_intGridHeight*c_intGridHeightCount);
			}
//			switch(p_objValue.m_enmBreathType)
//			{
//				case enmThreeMeasureBreathType.一般 :
			p_objValue.m_imgSymbol = m_imgBreath;
//					break;
//				case enmThreeMeasureBreathType.辅助呼吸 :
//					p_objValue.m_imgSymbol = m_imgBreath;
//					break;
//				case enmThreeMeasureBreathType.停辅助呼吸 :
//					p_objValue.m_imgSymbol = m_imgBreath;
//					break;
//			}

			//添加到呼吸数据集（数据集有能力保持数据间的顺序）
			bool blnOK = m_objBreathValueManager.m_blnAddValue(p_objValue);

			if(blnOK)
			{
				p_objValue.m_objDeleteInfo = null;
				m_mthUpdateListByBreath(p_objValue);
			}

			return blnOK;
		}
		/// <summary>
		/// 更新重叠的设置
		/// </summary>
		/// <param name="p_objValue">呼吸</param>
		private void m_mthUpdateListByBreath(clsThreeMeasureBreathValue p_objValue)
		{
			while(m_objBreathValueManager.m_blnNext())
			{
				clsThreeMeasureBreathValue objBreath = m_objBreathValueManager.m_ObjCurrent;

				if(Math.Abs(objBreath.m_fltXPos-p_objValue.m_fltXPos) <= c_fltTwoSymbolDiff
					&& Math.Abs(objBreath.m_fltYPos-p_objValue.m_fltYPos) <= c_fltTwoSymbolDiff)
				{
					//重叠
					//m_mthHandleCover(p_objValue,objBreath);
					break;
				}
			}

			m_objBreathValueManager.m_mthReset();
		}

		/// <summary>
		/// 添加外出
		/// </summary>
		/// <param name="p_objValue">外出信息</param>
		/// <returns></returns>
		public bool m_blnAddStayOutValue(clsThreeMeasureStayOutValue p_objValue)
		{
			if(p_objValue == null)
				return false;
			
			int intIndex = m_objDateManager.m_intIndexOf(p_objValue.m_dtmValueTime.Date);

			if(intIndex < 0)
				return false;

			p_objValue.m_fltXPos = c_intTextTotleWidth + intIndex*c_intGridHeight*6+(float)p_objValue.m_dtmValueTime.TimeOfDay.TotalSeconds/(4*3600)*c_intGridHeight;
			p_objValue.m_fltYPos = c_intStayOutHeight;

			switch(p_objValue.m_enmStayOutType)
			{
				case enmThreeMeasureStayOutType.一般 :
					p_objValue.m_imgSymbol = m_imgStayOut;
					break;
				
			}

			//添加到外出数据集（数据集有能力保持数据间的顺序）
			bool blnOK = m_objStayOutValueManager.m_blnAddValue(p_objValue);

			if(blnOK)
			{
				p_objValue.m_objDeleteInfo = null;
				//m_mthUpdateListByStayOut(p_objValue);
			}

			return blnOK;
		}



		/// <summary>
		/// 呼吸连线颜色
		/// </summary>
		private Color m_clrBreathLine = Color.Blue;
		public Color m_ClrBreathLine
		{
			get
			{
				return m_clrBreathLine;
			}
			set
			{
				m_clrBreathLine = value;
				//Invalidate();
			}
		}

		/// <summary>
		/// 外出坐标信息颜色
		/// </summary>
		private Color m_clrStayOutParamsText = Color.Blue;
		public Color m_ClrStayOutParamsText
		{
			get
			{
				return m_clrStayOutParamsText;
			}
			set
			{
				m_clrStayOutParamsText = value;
				//Invalidate();
			}
		}

		/// <summary>
		/// 呼吸坐标信息颜色
		/// </summary>
		private Color m_clrBreathParamsText = Color.Blue;
		public Color m_ClrBreathParamsText
		{
			get
			{
				return m_clrBreathParamsText;
			}
			set
			{
				m_clrBreathParamsText = value;
				//Invalidate();
			}
		}

		private Color m_clrStayOutSymbol = Color.Blue ;
		public Color m_ClrStayOutSymbol
		{
			get
			{
				return m_clrStayOutSymbol;
			}
			set
			{
				m_clrStayOutSymbol = value;

				m_mthInitStayOutImage();

//				this.//Invalidate();
			}
		}

		private Color m_clrBreathSymbol = Color.Navy ;
		public Color m_ClrBreathSymbol
		{
			get
			{
				return m_clrBreathSymbol;
			}
			set
			{
				m_clrBreathSymbol = value;

				m_mthInitBreathImage();

//				this.//Invalidate();
			}
		}

		/// <summary>
		/// 外出连线颜色
		/// </summary>
		private Color m_clrStayOutLine = Color.Red;
		public Color m_ClrStayOutLine
		{
			get
			{
				return m_clrStayOutLine;
			}
			set
			{
				m_clrStayOutLine = value;
				//Invalidate();
			}
		}

//		/// <summary>
//		/// 呼吸连线颜色
//		/// </summary>
//		private Color m_clrBreathLine = Color.Red;
//		public Color m_ClrBreathLine
//		{
//			get
//			{
//				return m_clrBreathLine;
//			}
//			set
//			{
//				m_clrBreathLine = value;
//				//Invalidate();
//			}
//		}
		
		/// <summary>
		/// 判断连线之间是否有外出事件，如果有则不连线
		/// </summary>
		/// <param name="p_dtmPreDate">上一个点的时间</param>
		/// <param name="p_dtmThisDate">本点的失禁</param>
		/// <returns>true＝可以连线</returns>
		private bool m_blnCanPrintLine(DateTime p_dtmPreDate,DateTime p_dtmThisDate)
		{
			bool blnCanLine = true;
			while(m_objStayOutValueManager.m_blnNext())
			{
				clsThreeMeasureStayOutValue objValue = m_objStayOutValueManager.m_ObjCurrent;
				if(objValue.m_dtmStayOutTime >= p_dtmPreDate && objValue.m_dtmStayOutTime <= p_dtmThisDate)
				{
					blnCanLine = false;
					break;
				}
			}
			m_objStayOutValueManager.m_mthReset();
			return blnCanLine;
		}

        private string m_strIsOperationOutOfDate(DateTime p_dtmRecordDate)
        {
            List<int> arlDays = new List<int>(arlDate.Count);
            DateTime dtTemp;
            string strResult = string.Empty;
            for (int i = 0; i < arlDate.Count; i++)
            {
                dtTemp = (DateTime)arlDate[i];
                if (p_dtmRecordDate < dtTemp) break;

                TimeSpan objTS = (TimeSpan)(p_dtmRecordDate - dtTemp);
                arlDays.Add(objTS.Days + 1);
            }
            if (arlDays.Count == 1)
                return (arlDays[0] > 10 ? string.Empty : arlDays[0].ToString());
            if (arlDays.Count > 0)
            {
                int intPreDay = arlDays[arlDays.Count - 1];
                if (intPreDay > 10) return string.Empty;
                strResult = intPreDay.ToString();
                for (int j = arlDays.Count - 2; j >= 0; j--)
                {
                    if (Math.Abs(arlDays[j] - intPreDay) > 10)
                    {
                        return strResult;
                    }
                    else
                    {
                        strResult += "/" + arlDays[j];
                    }
                    intPreDay = arlDays[j];
                }
            }
            return strResult;
        }
	}	

}
