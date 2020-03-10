using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using weCare.Core.Entity;
using System.Collections.Generic;

namespace com.digitalwave.Utility.Controls
{
	#region 事件参数
	public class clsThreeMeasureDateRecordArg : EventArgs
	{
		public clsThreeMeasureDateRecord m_objRecord;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureSpecialDateArg : EventArgs
	{
		public clsThreeMeasureSpecialDate m_objSpecialDate;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasurePulseArg : EventArgs
	{
		public clsThreeMeasurePulseValue [] m_objPulseValueArr;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureTemperatureArg : EventArgs
	{
		public clsThreeMeasureTemperatureValue [] m_objTemperatureArr;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureEventArg : EventArgs
	{
		public clsThreeMeasureEvent [] m_objEventArr;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureBreathArg : EventArgs
	{
		public clsThreeMeasureBreathValue m_objBreath;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureInputArg : EventArgs
	{
		public clsThreeMeasureInputValue m_objInput;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureDejectaArg : EventArgs
	{
		public clsThreeMeasureDejectaValue m_objDejecta;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasurePeeArg : EventArgs
	{
		public clsThreeMeasurePeeValue m_objPee;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureOutStreamArg : EventArgs
	{
		public clsThreeMeasureOutStreamValue m_objOutStream;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasurePressureArg : EventArgs
	{
		public clsThreeMeasurePressureValue m_objPressure;

		public int m_intXPos;
		public int m_intYPos;

		public int m_intPressureIndex;
	}

	public class clsThreeMeasureWeightArg : EventArgs
	{
		public clsThreeMeasureWeightValue m_objWeight;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureSkinTestArg : EventArgs
	{
		public clsThreeMeasureSkinTestValue m_objSkinTest;

		public int m_intXPos;
		public int m_intYPos;
	}

	public class clsThreeMeasureOtherArg : EventArgs
	{
		public clsThreeMeasureOtherValue m_objOther;

		public int m_intXPos;
		public int m_intYPos;
	}
	#endregion

	/// <summary>
	/// Summary description for ctlThreeMeasureRecord.
	/// </summary>
	//	public class ctlThreeMeasureRecord : System.Windows.Forms.UserControl
	public class ctlThreeMeasureRecord : System.Windows.Forms.PictureBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        /// <summary>
        /// 每周的末次手术次数
        /// </summary>
        private int[] m_strEndSpecialTimes;
        /// <summary>
        /// 每周的末次手术日期
        /// </summary>
        private DateTime[] m_strEndSpecialDate;

		public ctlThreeMeasureRecord()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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

			this.Height = m_intTotalHeight;
			this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

			m_mthInitMouthTemperatureImage();
			m_mthInitArmpitTemperatureImage();
			m_mthInitAnusTemperatureImage();

			m_mthInitPulseImage();
			m_mthInitHeartRateImage();

			m_mthInitDownTemperatureImage();

			m_mthInitMouthTemperatureCoverImage();
			m_mthInitAnusTemperatureCoverImage();
			m_mthInitArmpitTemperatureCoverImage();

			m_objDateManager = new clsThreeMeasureDateRecordManager();
			m_objPulseValueManager = new clsThreeMeasurePulseValueManager(m_objDateManager);
			m_objTemperatureValueManager = new clsThreeMeasureTemperatureValueManager(m_objDateManager);			

			m_strUserID = "";
			m_strUserName = "";

			m_clrDST = Color.Red;

			m_objXmlStream = new MemoryStream();

			m_objXmlWriter = new XmlTextWriter(m_objXmlStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();

			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);
		}			
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

        private DateTime m_dtmInPatientDate;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public DateTime m_DtmInPatientDate
        {
            set
            {
                m_dtmInPatientDate = value;
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

		#region 事件
		public event EventHandler m_evtDateRecordMouseDown;
		public event EventHandler m_evtSpecialDateMouseDown;
		public event EventHandler m_evtPulseMouseDown;
		public event EventHandler m_evtTemperatureMouseDown;
		public event EventHandler m_evtEventMouseDown;
		public event EventHandler m_evtBreathMouseDown;
		public event EventHandler m_evtInputMouseDown;
		public event EventHandler m_evtDejectaMouseDown;
		public event EventHandler m_evtPeeMouseDown;
		public event EventHandler m_evtOutStreamMouseDown;
		public event EventHandler m_evtPressureMouseDown;
		public event EventHandler m_evtWeightMouseDown;
		public event EventHandler m_evtSkinTestMouseDown;
		public event EventHandler m_evtOtherMouseDown;
		public event EventHandler m_evtIsShortChanged;
		public event EventHandler m_evtWeekNumChanged;

		private ArrayList m_arlMouseEventTemp = new ArrayList();
		#endregion

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
		private const float c_flt14PointFontSize = 14.25f;

		//各种项目的高度
        /// <summary>
        /// 日期
        /// </summary>
		private const int c_intRecordDateHeight = 17;

        /// <summary>
        /// 住院日数高度
        /// </summary>
        private const int c_intInpateintDateHeight = 18;// 24;
        private const int c_intInpateintTotalHeight = c_intRecordDateHeight + 18;
        /// <summary>
        /// 手术
        /// </summary>
		private const int c_intSpecialDateHeight = 17;
        private const int c_intSpecialDateTotalHeight = c_intInpateintTotalHeight + 17;
        /// <summary>
        /// 时间
        /// </summary>
		private const int c_intTimeHeight = 24;
        private const int c_intTimeTotalHeight = 24 + c_intSpecialDateTotalHeight;

		private const int c_intGridHeight = 12;
		private const int c_intGridHeightCount = 45;
        private int c_intGridTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45;
        private int c_intLowEventTextStartHeight = c_intTimeTotalHeight + c_intGridHeight * 35;

        /// <summary>
        /// 呼吸
        /// </summary>
		private int c_intBreathHeight = 28;
        private int c_intBreathTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28;
		private int m_intMaxBreathCount = 1;
        /// <summary>
        /// 血压
        /// </summary>
		private int c_intPressureHeight = 22;
        private int c_intPressureTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22;
		private int m_intMaxPressureCount = 1;

        /// <summary>
        /// 总入量
        /// </summary>
		private int c_intInputHeight = 22;
        private int c_intInputTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22 * 2;
		private int m_intMaxInputCount = 1;

        /// <summary>
        /// 大便
        /// </summary>
		private int c_intDejectaHeight = 22;
        private int c_intDejectaTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22 * 3;
		private int m_intMaxDejectaCount = 1;

        /// <summary>
        /// 尿量
        /// </summary>
		private int c_intPeeHeight = 22;
        private int c_intPeeTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22 * 4;
		private int m_intMaxPeeCount = 1;

        /// <summary>
        /// 排出量>>其它
        /// </summary>
		private int c_intOutStreamHeight = 22;
        private int c_intOutStreamTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22 * 5;
		private int m_intMaxOutStreamCount = 1;


        /// <summary>
        /// 体重
        /// </summary>
		private int c_intWeightHeight = 22;
        private int c_intWeightTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22 * 6;
		private int m_intMaxWeightCount = 1;

        /// <summary>
        /// 皮试
        /// </summary>
		private int c_intSkinTestHeight = 22;
        private int c_intSkinTestTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 + 22 * 7;
		private int m_intMaxSkinTestCount = 1;

        /// <summary>
        /// 其他
        /// </summary>
		private int c_intOtherHeight = 22;
		private int m_intMaxOtherCount = 1;

		private const int c_intTextTotleWidth = 115;	
		#endregion

		#region 画图变量
		#region Border Line
		/// <summary>
		/// 边框颜色
		/// </summary>
		private Color m_clrBorder = Color.White;
		public Color m_ClrBorder
		{
			get
			{
				return m_clrBorder;
			}
			set
			{
				m_clrBorder = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// 格子线颜色
		/// </summary>
		private Color m_clrGridBorder = Color.Black;
		public Color m_ClrGridBorder
		{
			get
			{
				return m_clrGridBorder;
			}
			set
			{
				m_clrGridBorder = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// 特殊格子线颜色
		/// </summary>
		private Color m_clrSpecialGridBorder = Color.Yellow;
		public Color m_ClrSpecialGridBorder
		{
			get
			{
				return m_clrSpecialGridBorder;
			}
			set
			{
				m_clrSpecialGridBorder = value;
				Invalidate();
			}
		}		
		#endregion

		#region Text
		/// <summary>
		/// 标题文字颜色
		/// </summary>
		private Color m_clrTitleText = Color.White;
		public Color m_ClrTitleText
		{
			get
			{
				return m_clrTitleText;
			}
			set
			{
				m_clrTitleText = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// 特殊时间颜色
		/// </summary>
		private Color m_clrSpecialTimeText = Color.Yellow;
		public Color m_ClrSpecialTimeText
		{
			get
			{
				return m_clrSpecialTimeText;
			}
			set
			{
				m_clrSpecialTimeText = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 手术或产后日期颜色
		/// </summary>
		private Color m_clrSpecialDateText = Color.Yellow;
		public Color m_ClrSpecialDateText
		{
			get
			{
				return m_clrSpecialDateText;
			}
			set
			{
				m_clrSpecialDateText = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 上部（40℃上方）事件颜色
		/// </summary>
		private Color m_clrUpperEventText = Color.Yellow;
		public Color m_ClrUpperEventText
		{
			get
			{
				return m_clrUpperEventText;
			}
			set
			{
				m_clrUpperEventText = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 下部（35℃下方）事件颜色
		/// </summary>
		private Color m_clrLowerEventText = Color.Yellow;
		public Color m_ClrLowerEventText
		{
			get
			{
				return m_clrLowerEventText;
			}
			set
			{
				m_clrLowerEventText = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// 脉搏坐标信息颜色
		/// </summary>
		private Color m_clrPulseParamsText = Color.Yellow;
		public Color m_ClrPulseParamsText
		{
			get
			{
				return m_clrPulseParamsText;
			}
			set
			{
				m_clrPulseParamsText = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 温度坐标信息颜色
		/// </summary>
		private Color m_clrTemperatureParamsText = Color.Black;
		public Color m_ClrTemperatureParamsText
		{
			get
			{
				return m_clrTemperatureParamsText;
			}
			set
			{
				m_clrTemperatureParamsText = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 一天记录信息颜色
		/// </summary>
		private Color m_clrDateValue = Color.Black;
		public Color m_ClrDateValue
		{
			get
			{
				return m_clrDateValue;
			}
			set
			{
				m_clrDateValue = value;
				Invalidate();
			}
		}

		/// 皮试阳性信息颜色
		/// </summary>
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
				Invalidate();
			}
		}
		#endregion

		#region Value Line
		/// <summary>
		/// 脉搏连线颜色
		/// </summary>
		private Color m_clrPulseLine = Color.Red;
		public Color m_ClrPulseLine
		{
			get
			{
				return m_clrPulseLine;
			}
			set
			{
				m_clrPulseLine = value;
				Invalidate();
			}
		}
		
		/// <summary>
		/// 体温连线颜色
		/// </summary>
		private Color m_clrTemperatureLine = Color.Red;
		public Color m_ClrTemperatureLine
		{
			get
			{
				return m_clrTemperatureLine;
			}
			set
			{
				m_clrTemperatureLine = value;
				Invalidate();
			}
		}

		/// 物理降温温度连线信息颜色
		/// </summary>
		private Color m_clrDownTemperatureLine = Color.Red;
		public Color m_ClrDownTemperatureLine
		{
			get
			{
				return m_clrDownTemperatureLine;
			}
			set
			{
				m_clrDownTemperatureLine = value;
				Invalidate();
			}
		}

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

				Invalidate();
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

				this.Invalidate();
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

				this.Invalidate();
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
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ctlThreeMeasureRecord
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
			this.Name = "ctlThreeMeasureRecord";
			this.Resize += new System.EventHandler(this.ctlThreeMeasureRecord_Resize);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ctlThreeMeasureRecord_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlThreeMeasureRecord_MouseDown);

		}
		#endregion

		/// <summary>
		/// 一页的天数
		/// </summary>
		private int m_intRecordLength = 7;
		/// <summary>
		/// 第几周
		/// </summary>
		private int m_intWeekNum = 1;

		/// <summary>
		/// 总共的周数
		/// </summary>
		public int m_IntTotalWeek
		{
			get
			{
				//不足一周的要补足一周，如果刚好一周，增加新一周。
				return m_objDateManager.m_IntRecordCount/7+1;
				
			}
		}

		/// <summary>
		/// 第几周
		/// </summary>
		public int m_IntWeekNum
		{
			get
			{
				return m_intWeekNum;
			}
			set
			{
				m_intWeekNum = value;				
				if(m_intWeekNum > m_IntTotalWeek)
					m_intWeekNum = m_IntTotalWeek;
				else if(m_intWeekNum < 1)
					m_intWeekNum = 1;
				if(m_evtWeekNumChanged != null)
					m_evtWeekNumChanged(null,EventArgs.Empty);
				m_mthUpdateDisplay();
			}
		}

		/// <summary>
		/// 是否短格式
		/// </summary>
		private bool m_blnIsShort = true;
		/// <summary>
		/// 是否短格式
		/// </summary>
		public bool m_BlnIsShort
		{
			get
			{
				return m_blnIsShort;
			}
			set
			{
				m_blnIsShort = value;
				m_mthUpdateDisplay();
				if(m_evtIsShortChanged != null)
					m_evtIsShortChanged(null,EventArgs.Empty);
			}
		}
		private void ctlThreeMeasureRecord_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(!m_blnIsShort)
			{
				m_mthPaint(e.Graphics);
			}
			else
			{
				System.Drawing.Image imgTop = new Bitmap(this.Width,this.Height);
				Graphics objTopGrp = Graphics.FromImage(imgTop);
				objTopGrp.SetClip(new Rectangle(0,0,this.Width,c_intTimeTotalHeight));
				m_mthPaint(objTopGrp);						
				objTopGrp.Dispose();

				System.Drawing.Image imgMid = new Bitmap(this.Width,this.Height);
				Graphics objMidGrp = Graphics.FromImage(imgMid);
				objMidGrp.SetClip(new Rectangle(0,c_intTimeTotalHeight+c_intGridHeight*15,this.Width,c_intGridHeight*20));
				m_mthPaint(objMidGrp);
				objMidGrp.Dispose();	

				System.Drawing.Image imgBottom = new Bitmap(this.Width,this.Height);
				Graphics objBottomGrp = Graphics.FromImage(imgBottom);
				objBottomGrp.SetClip(new Rectangle(0,c_intGridTotalHeight,this.Width,this.Height-c_intGridTotalHeight));
				m_mthPaint(objBottomGrp);
				objBottomGrp.Dispose();						

				e.Graphics.DrawImage(imgTop,0,0);
				e.Graphics.DrawImage(imgMid,0,-1*(c_intGridHeight*15));	
				e.Graphics.DrawImage(imgBottom,0,-1*(c_intGridHeight*25));
				
				imgTop.Dispose();
				imgBottom.Dispose();
				imgMid.Dispose();
			}
		}
        /// <summary>
        /// 画结果
        /// </summary>
        /// <param name="p_objGrp"></param>
        private void m_mthPaint(System.Drawing.Graphics p_objGrp)
		{
			Pen penOneWidthLine = new Pen(m_clrBorder);
			Pen penTwoWidthLine = new Pen(m_clrSpecialGridBorder,2);
			Pen penDotLine = new Pen(m_clrGridBorder);
			Pen penReaLine = new Pen(m_clrGridBorder);

			penDotLine.DashStyle = DashStyle.Dot;
			penReaLine.DashStyle =DashStyle.Solid;
			SolidBrush bruTemp = new SolidBrush(m_clrTitleText);

			Color clrPenTemp;
			Color clrBrushTemp;
            int intStartDateIndex = (m_intWeekNum - 1) * m_intRecordLength;
            m_strEndSpecialTimes = new int[m_IntTotalWeek];
            m_strEndSpecialDate = new DateTime[m_IntTotalWeek];
            if (m_objDateManager.m_IntRecordCount > 0)
            {

                m_strEndSpecialTimes = new int[m_IntTotalWeek];
                m_strEndSpecialDate = new DateTime[m_IntTotalWeek];
                m_strEndSpecialTimes[0] = 0;
                m_strEndSpecialDate[0] = ((clsThreeMeasureDateRecord)m_objDateManager[0]).m_dtmRecordDate;
                int intIndex = 0;
                for (int m = 0; m < m_IntTotalWeek - 1; m++)
                {

                    int intDateIndex = m * m_intRecordLength;
                    for (int i = intDateIndex; i < m_objDateManager.m_IntRecordCount && i - intDateIndex < m_intRecordLength; i++)
                    {
                        clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
                        if (objRecord.m_objSpecialDate != null)
                        {
                            if (objRecord.m_objSpecialDate.m_blnIsNewStart)
                            {
                                m_strEndSpecialTimes[m + 1] = ++intIndex;
                                m_strEndSpecialDate[m + 1] = objRecord.m_dtmRecordDate;
                            }
                        }
                    }
                }
            }
			//画边框
			p_objGrp.DrawRectangle(penOneWidthLine,0,0,this.Width-1,this.Height-1);

			//画文字与值的分隔线
			penOneWidthLine.Color = m_clrGridBorder;
			p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth,1,c_intTextTotleWidth,m_intTotalHeight);		
			
			//画日期文字和与下面的分隔线
            Font fntRecordDateText = new Font("宋体", c_flt12PointFontSize);
			p_objGrp.DrawString("  日  期  ",fntRecordDateText,bruTemp,6,2);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intRecordDateHeight,this.Width-2,c_intRecordDateHeight);

            //画住院日期和与下面的分隔线
            Font fntInpateintDateText = new Font("宋体", c_flt10PointFontSize);
            p_objGrp.DrawString("   住院日数 ", fntInpateintDateText, bruTemp, 1, c_intRecordDateHeight + 2);
            p_objGrp.DrawLine(penOneWidthLine, 1, c_intInpateintTotalHeight, this.Width - 2, c_intInpateintTotalHeight);

			//画特殊日期文字和与下面的分隔线
			Font fntSpecialDateText = new Font("宋体",c_flt10PointFontSize);
            p_objGrp.DrawString(" 手术或产后日期 ", fntSpecialDateText, bruTemp, 1, c_intInpateintTotalHeight + 2);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intSpecialDateTotalHeight,this.Width-2,c_intSpecialDateTotalHeight);

			//画时间
			p_objGrp.DrawString("  时  间  ",fntRecordDateText,bruTemp,6,c_intSpecialDateTotalHeight+4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intTimeTotalHeight,this.Width-2,c_intTimeTotalHeight);

            Font fntGridText = new Font("宋体", c_flt7PointFontSize);
			#region 画格子与时间
			//画竖格子线和时间
			int intNormalGridHeight = c_intSpecialDateTotalHeight+8;
			for(int i=0;i<m_intRecordLength;i++)
			{
				clrBrushTemp = bruTemp.Color;
				bruTemp.Color = m_clrSpecialTimeText;
				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight+i*c_intGridHeight*6,c_intSpecialDateTotalHeight,c_intTextTotleWidth+c_intGridHeight+i*c_intGridHeight*6,c_intBreathTotalHeight);
				p_objGrp.DrawString("4",fntGridText,bruTemp,c_intTextTotleWidth+i*c_intGridHeight*6+2,intNormalGridHeight);			
				bruTemp.Color = clrBrushTemp;

				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*2+i*c_intGridHeight*6,c_intSpecialDateTotalHeight,c_intTextTotleWidth+c_intGridHeight*2+i*c_intGridHeight*6,c_intBreathTotalHeight);
				p_objGrp.DrawString("8",fntGridText,bruTemp,c_intTextTotleWidth+c_intGridHeight+i*c_intGridHeight*6+2,intNormalGridHeight);			

				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*3+i*c_intGridHeight*6,c_intSpecialDateTotalHeight,c_intTextTotleWidth+c_intGridHeight*3+i*c_intGridHeight*6,c_intBreathTotalHeight);
				p_objGrp.DrawString("12",fntGridText,bruTemp,c_intTextTotleWidth+c_intGridHeight*2+i*c_intGridHeight*6-2,intNormalGridHeight);			
				
				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*4+i*c_intGridHeight*6,c_intSpecialDateTotalHeight,c_intTextTotleWidth+c_intGridHeight*4+i*c_intGridHeight*6,c_intBreathTotalHeight);
				p_objGrp.DrawString("4",fntGridText,bruTemp,c_intTextTotleWidth+c_intGridHeight*3+i*c_intGridHeight*6+2,intNormalGridHeight);			

				clrBrushTemp = bruTemp.Color;
				bruTemp.Color = m_clrSpecialTimeText;
				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*5+i*c_intGridHeight*6,c_intSpecialDateTotalHeight,c_intTextTotleWidth+c_intGridHeight*5+i*c_intGridHeight*6,c_intBreathTotalHeight);
				p_objGrp.DrawString("8",fntGridText,bruTemp,c_intTextTotleWidth+c_intGridHeight*4+i*c_intGridHeight*6+2,intNormalGridHeight);			
				
				clrPenTemp = penOneWidthLine.Color;
				penOneWidthLine.Color = m_clrSpecialGridBorder;
				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*6+i*c_intGridHeight*6,1,c_intTextTotleWidth+c_intGridHeight*6+i*c_intGridHeight*6,c_intGridTotalHeight);
				p_objGrp.DrawString("12",fntGridText,bruTemp,c_intTextTotleWidth+c_intGridHeight*5+i*c_intGridHeight*6-2,intNormalGridHeight);							
				penOneWidthLine.Color = clrPenTemp;
				bruTemp.Color = clrBrushTemp;

				p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*6+i*c_intGridHeight*6,c_intGridTotalHeight,c_intTextTotleWidth+c_intGridHeight*6+i*c_intGridHeight*6,m_intTotalHeight);
				
			}

			//画横格子线
			int intStartX = c_intTextTotleWidth+1;
			int intEndX = this.Width-2;
			
			for(int i=1;i<=24;i++)
			{
				p_objGrp.DrawLine(penOneWidthLine,intStartX,c_intTimeTotalHeight+i*c_intGridHeight,intEndX,c_intTimeTotalHeight+i*c_intGridHeight);
			}

			p_objGrp.DrawLine(penTwoWidthLine,intStartX,c_intTimeTotalHeight+25*c_intGridHeight,intEndX,c_intTimeTotalHeight+25*c_intGridHeight);

			for(int i=26;i<c_intGridHeightCount;i++)
			{
				p_objGrp.DrawLine(penOneWidthLine,intStartX,c_intTimeTotalHeight+i*c_intGridHeight,intEndX,c_intTimeTotalHeight+i*c_intGridHeight);
			}

			p_objGrp.DrawLine(penOneWidthLine,1,c_intTimeTotalHeight+c_intGridHeightCount*c_intGridHeight,this.Width-2,c_intTimeTotalHeight+c_intGridHeightCount*c_intGridHeight);
			#endregion

			#region 画参数坐标信息
			//画参数坐标值
			int intParamCount = c_intGridHeightCount/5;					
			for(int i=1;i<intParamCount;i++)
			{
				//脉搏（心率）
				int intPulse = 180-i*20;
				bruTemp.Color = m_clrPulseParamsText;
				p_objGrp.DrawString(intPulse.ToString(),fntSpecialDateText,bruTemp,c_intTextTotleWidth-75,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2);

				//温度
				int intTemp = 42-i;
				bruTemp.Color = m_clrTemperatureParamsText;
				p_objGrp.DrawString(intTemp.ToString("0°"),fntSpecialDateText,bruTemp,c_intTextTotleWidth-28,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2);
			}

			//画参数描述
			//脉搏（心率）
			bruTemp.Color = m_clrPulseParamsText;
            p_objGrp.DrawString("脉搏", fntSpecialDateText, bruTemp, c_intTextTotleWidth - 80, c_intTimeTotalHeight + 1);
            p_objGrp.DrawString("(次/分)", fntSpecialDateText, bruTemp, c_intTextTotleWidth - 95, c_intTimeTotalHeight + 18);
			
			//体温
			bruTemp.Color = m_clrTemperatureParamsText;
            p_objGrp.DrawString("体温", fntSpecialDateText, bruTemp, c_intTextTotleWidth - 35, c_intTimeTotalHeight + 1);
            p_objGrp.DrawString("  ℃", fntSpecialDateText, bruTemp, c_intTextTotleWidth - 30, c_intTimeTotalHeight + 18);			
			#endregion			

			#region 画数值图标
            Font fntSymbol = new Font("宋体", c_flt9PointFontSize);
			bruTemp.Color = m_clrTitleText;

			p_objGrp.DrawString("口表",fntSymbol,bruTemp,4,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+15);
			p_objGrp.DrawImage(m_imgMouthTemperature,35,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+16,c_intGridHeight,c_intGridHeight);
			p_objGrp.DrawString("腋表",fntSymbol,bruTemp,4,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+19+c_intGridHeight);
			p_objGrp.DrawImage(m_imgArmpitTemperature,35,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+21+c_intGridHeight,c_intGridHeight,c_intGridHeight);
			p_objGrp.DrawString("肛表",fntSymbol,bruTemp,4,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+23+2*c_intGridHeight);
			p_objGrp.DrawImage(m_imgAnusTemperature,35,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+25+2*c_intGridHeight,c_intGridHeight,c_intGridHeight);
			
			p_objGrp.DrawString("脉搏",fntSymbol,bruTemp,4,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+15);
			p_objGrp.DrawImage(m_imgPulse,35,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+16,c_intGridHeight,c_intGridHeight);
			p_objGrp.DrawString("心率",fntSymbol,bruTemp,4,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+19+c_intGridHeight);
			p_objGrp.DrawImage(m_imgHeartRate,35,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+21+c_intGridHeight,c_intGridHeight,c_intGridHeight);
			#endregion

			//画呼吸
			bruTemp.Color = m_clrTitleText;
			p_objGrp.DrawString("呼吸(次/分)",fntSpecialDateText,bruTemp,6,c_intGridTotalHeight+4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intBreathTotalHeight,this.Width-2,c_intBreathTotalHeight);

			//画血压
            p_objGrp.DrawString("血压(mmHg)", fntSpecialDateText, bruTemp, 6, c_intBreathTotalHeight + 4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intPressureTotalHeight,this.Width-2,c_intPressureTotalHeight);

			//画输入液量
            p_objGrp.DrawString("总入液量(ml)", fntSpecialDateText, bruTemp, 6, c_intPressureTotalHeight + 4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intInputTotalHeight,this.Width-2,c_intInputTotalHeight);

			//画排出量
			penOneWidthLine.Color = m_clrGridBorder;
			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			p_objGrp.DrawString("排出量",fntSpecialDateText,bruTemp,4,c_intInputTotalHeight+6,stfDirectionVertical);
			p_objGrp.DrawLine(penOneWidthLine,32,c_intInputTotalHeight,32,c_intOutStreamTotalHeight);
			p_objGrp.DrawString("大便(次)",fntSpecialDateText,bruTemp,35,c_intInputTotalHeight+4);			
			p_objGrp.DrawLine(penOneWidthLine,33,c_intDejectaTotalHeight,this.Width-2,c_intDejectaTotalHeight);
			p_objGrp.DrawString("尿量(ml)",fntSpecialDateText,bruTemp,35,c_intDejectaTotalHeight+4);			
			p_objGrp.DrawLine(penOneWidthLine,33,c_intPeeTotalHeight,this.Width-2,c_intPeeTotalHeight);
			p_objGrp.DrawString("其它",fntSpecialDateText,bruTemp,33,c_intPeeTotalHeight+4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intOutStreamTotalHeight,this.Width-2,c_intOutStreamTotalHeight);
			
			//画体重
            p_objGrp.DrawString("体重(kg)", fntSpecialDateText, bruTemp, 6, c_intOutStreamTotalHeight + 4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intWeightTotalHeight,this.Width-2,c_intWeightTotalHeight);

			//画皮试
			p_objGrp.DrawString("皮试",fntSpecialDateText,bruTemp,6,c_intWeightTotalHeight+4);			
			p_objGrp.DrawLine(penOneWidthLine,1,c_intSkinTestTotalHeight,this.Width-2,c_intSkinTestTotalHeight);
			
			//画其它
            //if(m_strOtherName == null)
            //{
				p_objGrp.DrawString("其它",fntSpecialDateText,bruTemp,6,c_intSkinTestTotalHeight+4);						
            //}
            //else
            //{
            //    p_objGrp.DrawString(m_strOtherName,fntSpecialDateText,bruTemp,6,c_intSkinTestTotalHeight+4);						
            //}

			#region 画数值
			//画日期及其子信息
			bool blnIsUpBreath = true;
			Font fntBigSymbol = new Font("宋体",c_flt14PointFontSize);
			DateTime dtmPreDateForDateRecord = DateTime.Today;
			DateTime dtmPreDateForSpecialDate = DateTime.Today;
			int intSpecialNewStartTimes = 0;
			int intCurrentDateDiff = 0;
			//int intStartDateIndex = (m_intWeekNum-1)*m_intRecordLength;
			int intPreWidth = intStartDateIndex*6*c_intGridHeight;
			int intStartPrintX = 0;
			intStartPrintX -= intPreWidth;
            if (m_IntTotalWeek >= m_intWeekNum)
            {//避免选择别的病人时出现索引越界
                dtmPreDateForSpecialDate = m_strEndSpecialDate[m_intWeekNum - 1];
                intSpecialNewStartTimes = m_strEndSpecialTimes[m_intWeekNum - 1];
            }
			for(int i=intStartDateIndex;i<m_objDateManager.m_IntRecordCount&& i-intStartDateIndex < m_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];

				bruTemp.Color = m_clrTitleText;
				//画日期
				string strRecordDate = "";
				if(i%m_intRecordLength==0 || dtmPreDateForDateRecord.Year != objRecord.m_dtmRecordDate.Year)
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

				p_objGrp.DrawString(strRecordDate,fntSpecialDateText,bruTemp,c_intTextTotleWidth+i*c_intGridHeight*6+4+intStartPrintX,3);

                //画住院日数
                string strInpateintNum = "";
                DateTime dtmCurrDate = DateTime.Parse(objRecord.m_dtmRecordDate.ToString("yyyy-MM-dd"));
                DateTime dtmInDate = DateTime.Parse(m_dtmInPatientDate.ToString("yyyy-MM-dd"));
                System.TimeSpan diff = dtmCurrDate.Subtract(dtmInDate);
                strInpateintNum = ((int)diff.TotalDays + 1).ToString();
                p_objGrp.DrawString("     " + strInpateintNum, fntSpecialDateText, bruTemp, c_intTextTotleWidth + i * c_intGridHeight * 6 + 4 + intStartPrintX, c_intRecordDateHeight + 3);
				
				bruTemp.Color = m_clrSpecialDateText;
				//画手术或产后日期
				if(objRecord.m_objSpecialDate != null)
				{
					if(objRecord.m_objSpecialDate.m_blnIsNewStart)
					{
						//新的手术日期
						intCurrentDateDiff = 0;
						dtmPreDateForSpecialDate = objRecord.m_dtmRecordDate;
						intSpecialNewStartTimes++;
						string strSpecialDataText = null;

						if(intSpecialNewStartTimes == 1)
						{
							//第一次手术，不显示罗马数字
							strSpecialDataText = intCurrentDateDiff.ToString();							
						}
						else
						{
							strSpecialDataText = m_strGetSpecialDateTimes(intSpecialNewStartTimes)+intCurrentDateDiff.ToString();
						}

						p_objGrp.DrawString(strSpecialDataText,fntSpecialDateText,bruTemp,c_intTextTotleWidth+i*c_intGridHeight*6+c_intGridHeight*2+4+intStartPrintX,c_intInpateintTotalHeight+2);
					}
					else if(intSpecialNewStartTimes > 0)
					{
						//旧的手术日期
						intCurrentDateDiff = ((TimeSpan)(objRecord.m_dtmRecordDate-dtmPreDateForSpecialDate)).Days;

						if(intCurrentDateDiff < 11)
						{
							string strSpecialDataText = null;
							//手术后10天内，继续显示手术日期
							if(intSpecialNewStartTimes == 1)
							{
								//第一次手术，不显示罗马数字
								strSpecialDataText = intCurrentDateDiff.ToString();							
							}
							else
							{
								strSpecialDataText = m_strGetSpecialDateTimes(intSpecialNewStartTimes)+intCurrentDateDiff.ToString();
							}

							p_objGrp.DrawString(strSpecialDataText,fntSpecialDateText,bruTemp,c_intTextTotleWidth+i*c_intGridHeight*6+c_intGridHeight*2+4+intStartPrintX,c_intInpateintTotalHeight+2);
						}

					}
					//else 没有做过手术
				}

				#region 画事件
				bruTemp.Color = m_clrUpperEventText;
				penOneWidthLine.Color = m_clrUpperEventText;
				int intPreTimeIndex = -1;
				int intActureIndex = 0;
                int intEventNamesCounts = 0;
                //enmThreeMeasureEventType enmTMEventType = enmThreeMeasureEventType.入院;
                //List<clsThreeMeasureEvent> arlEvent = new List<clsThreeMeasureEvent>(objRecord.m_arlEvent.Count);
				for(int j2=0;j2<objRecord.m_arlEvent.Count;j2++)
				{
					clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[j2];
                    //if (objEvent.m_intTimeIndex > intPreTimeIndex)
                    //{
                    //    intActureIndex = objEvent.m_intTimeIndex;
                    //}
                    //else
                    //{
                    //    if (m_blnIsNearType(enmTMEventType, objEvent.m_enmEventType) || m_blnIsOccupy(arlEvent, objEvent.m_enmEventType, objEvent.m_intTimeIndex))
                            intActureIndex = objEvent.m_intNearTimeIndex;
                    //}
                    //arlEvent.Add(objEvent);
                    //enmTMEventType = objEvent.m_enmEventType;
					
                    if (objEvent.m_enmEventType == enmThreeMeasureEventType.请假 || 
                        objEvent.m_enmEventType == enmThreeMeasureEventType.冰敷 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.停冰敷 || 
                        objEvent.m_enmEventType == enmThreeMeasureEventType.外出 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.拒测 || 
                        objEvent.m_enmEventType == enmThreeMeasureEventType.停降温毡 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.上呼吸机 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.停呼吸机 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.酒精擦浴 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.温水擦浴 ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.降温毡)
					{
						bruTemp.Color = m_clrLowerEventText;
                        if (intPreTimeIndex == objEvent.m_intTimeIndex)//同一时段在同一行接连显示
                            p_objGrp.DrawString(objEvent.m_enmEventType.ToString(), fntSymbol, bruTemp, c_intTextTotleWidth + c_intGridHeight * (i * 6 + intActureIndex) - 1 + intStartPrintX, c_intLowEventTextStartHeight + intEventNamesCounts * c_intGridHeight - 1, stfDirectionVertical);
                        else//不同时段在不同时段顶格显示
                            p_objGrp.DrawString(objEvent.m_enmEventType.ToString(), fntSymbol, bruTemp, c_intTextTotleWidth + c_intGridHeight * (i * 6 + intActureIndex) - 1 + intStartPrintX, c_intLowEventTextStartHeight, stfDirectionVertical);
                        intEventNamesCounts += objEvent.m_enmEventType.ToString().Length;
					}
					else
					{

						bruTemp.Color = m_clrUpperEventText;

						p_objGrp.DrawString(objEvent.m_enmEventType.ToString(),fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)-1+intStartPrintX,c_intTimeTotalHeight-1,stfDirectionVertical);
					
						if(objEvent.m_strTime != "")
						{
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+2*c_intGridHeight,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+3*c_intGridHeight);
							for(int k3=0;k3<objEvent.m_strTime.Length;k3++)
							{
								p_objGrp.DrawString(objEvent.m_strTime[k3].ToString(),fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)-1+intStartPrintX,c_intTimeTotalHeight+(3+k3)*c_intGridHeight-1);
							}
						}
					}
                    intPreTimeIndex = objEvent.m_intTimeIndex;
					if(objEvent.m_objDeleteInfo != null)
					{
						int intLen = 3+objEvent.m_strTime.Length;

						penOneWidthLine.Color = m_clrDST;
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+intStartPrintX,c_intTimeTotalHeight+intLen*c_intGridHeight);
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+3+intStartPrintX,c_intTimeTotalHeight,c_intTextTotleWidth+c_intGridHeight*(i*6+intActureIndex)+c_intGridHeight/2+3+intStartPrintX,c_intTimeTotalHeight+intLen*c_intGridHeight);
						penOneWidthLine.Color = m_clrUpperEventText;
					}
				}
				#endregion

				bruTemp.Color = m_clrDateValue;
				int intCurrentBreathIndex = -1;
				int intDeleteBreath = 0;
				penOneWidthLine.Color = m_clrDST;
				byte bytBreathIndex = 0;
				#region 画呼吸
				for(int j2=0;j2<objRecord.m_arlBreath.Count;j2++)
				{
					clsThreeMeasureBreathValue objBreath = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[j2];

					if(objBreath.m_intTimeIndex != intCurrentBreathIndex)
					{
						intCurrentBreathIndex = objBreath.m_intTimeIndex;
						intDeleteBreath = 0;
					}

					if(objBreath.m_enmBreathType == enmThreeMeasureBreathType.一般)
					{
						int intHeight = 0;
							
						if(intDeleteBreath == 0)
						{
							if(blnIsUpBreath)
							{
								intHeight = c_intGridTotalHeight;
							}
							else
							{
								intHeight = c_intGridTotalHeight+14;
							}

							blnIsUpBreath = !blnIsUpBreath;							
						}
						else
						{
							intHeight = c_intGridTotalHeight+14+14*intDeleteBreath;
						}

						p_objGrp.DrawString(objBreath.m_intValue.ToString(),fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+objBreath.m_intTimeIndex)-2+intStartPrintX,intHeight);

						if(objBreath.m_objDeleteInfo != null)
						{
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+objBreath.m_intTimeIndex)+intStartPrintX,intHeight+6,c_intTextTotleWidth+c_intGridHeight*(i*6+objBreath.m_intTimeIndex+1)+intStartPrintX,intHeight+6);
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+objBreath.m_intTimeIndex)+intStartPrintX,intHeight+9,c_intTextTotleWidth+c_intGridHeight*(i*6+objBreath.m_intTimeIndex+1)+intStartPrintX,intHeight+9);

							intDeleteBreath++;
						}
					}
					else
					{
						bruTemp.Color = m_clrLowerEventText;
						string strType = objBreath.m_enmBreathType.ToString();
						for(int k3=0;k3<strType.Length;k3++)
						{
							p_objGrp.DrawString(strType[k3].ToString(),fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+objBreath.m_intTimeIndex)-1+intStartPrintX,c_intLowEventTextStartHeight+c_intGridHeight*k3-1);
						}

						bytBreathIndex += (byte)(1<<objBreath.m_intTimeIndex);

						bruTemp.Color = m_clrDateValue;
					}
				}
				#endregion

				penOneWidthLine.Color = m_clrDST;
				for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
				{
					clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

					if(objSkinValue.m_intTimeIndex >= 0)
					{
						int intSkinTextY = c_intLowEventTextStartHeight;
						if((bytBreathIndex & (1<<objSkinValue.m_intTimeIndex)) != 0)
						{
							intSkinTextY += 6*c_intGridHeight;
						}
						//ppd					
						SizeF szfName = p_objGrp.MeasureString(objSkinValue.m_strMedicineName,fntSpecialDateText,10,stfDirectionVertical);

						string strValue = null;
						bruTemp.Color = m_clrLowerEventText;
						p_objGrp.DrawString(objSkinValue.m_strMedicineName,fntSpecialDateText,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)-4+intStartPrintX,intSkinTextY,stfDirectionVertical);
						if(objSkinValue.m_blnIsBad)
						{
							strValue = objSkinValue.m_strPDDValue;
							bruTemp.Color = Color.Red;
							p_objGrp.DrawString(strValue,fntSpecialDateText,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)-2+intStartPrintX,intSkinTextY+(int)szfName.Height,stfDirectionVertical);
						}
						else
						{
							strValue = "(-)";
							bruTemp.Color = m_clrLowerEventText;
							p_objGrp.DrawString(strValue,fntSpecialDateText,bruTemp,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)-2+intStartPrintX,intSkinTextY+(int)szfName.Height,stfDirectionVertical);
						}		
				
						if(objSkinValue.m_objDeleteInfo != null)
						{
							SizeF szfDesc = p_objGrp.MeasureString(objSkinValue.m_strMedicineName+strValue,fntRecordDateText,10,stfDirectionVertical);
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+intStartPrintX,intSkinTextY,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+intStartPrintX,intSkinTextY+szfDesc.Height);
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+3+intStartPrintX,intSkinTextY,c_intTextTotleWidth+c_intGridHeight*(i*6+objSkinValue.m_intTimeIndex)+c_intGridHeight/2+3+intStartPrintX,intSkinTextY+szfDesc.Height);
						}
					}
				}


                //画血压:先画第一次，再画第二次。
                penOneWidthLine.Color = m_clrDST;
                float fltPressureTotalHeight = 0;
                for (int j2 = 0; j2 < objRecord.m_arlPressureValue1.Count; j2++)
                {
                    clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

                    string strDesc = objPressureValue.m_fltSystolicValue.ToString("0") + "/" + objPressureValue.m_fltDiastolicValue.ToString("0");
                    p_objGrp.DrawString(strDesc, fntGridText, bruTemp, c_intTextTotleWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 5);

                    SizeF szfDesc = p_objGrp.MeasureString(strDesc, fntGridText);

                    if (objPressureValue.m_objDeleteInfo != null)
                    {
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11, c_intTextTotleWidth + c_intGridHeight * i * 6 + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11);
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13, c_intTextTotleWidth + c_intGridHeight * i * 6 + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13);
                    }

                    fltPressureTotalHeight += szfDesc.Height + 5;
                }
                fltPressureTotalHeight = 0;
                for (int j2 = 0; j2 < objRecord.m_arlPressureValue2.Count; j2++)
                {
                    clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[j2];

                    string strDesc = objPressureValue.m_fltSystolicValue.ToString("0") + "/" + objPressureValue.m_fltDiastolicValue.ToString("0");
                    p_objGrp.DrawString(strDesc, fntGridText, bruTemp, c_intTextTotleWidth + c_intGridHeight * (i * 6 + 3) + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 5);

                    SizeF szfDesc = p_objGrp.MeasureString(strDesc, fntGridText);

                    if (objPressureValue.m_objDeleteInfo != null)
                    {
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * (i * 6 + 3) + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11, c_intTextTotleWidth + c_intGridHeight * (i * 6 + 3) + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11);
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * (i * 6 + 3) + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13, c_intTextTotleWidth + c_intGridHeight * (i * 6 + 3) + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13);
                    }

                    fltPressureTotalHeight += szfDesc.Height + 5;
                }
				//画输入液量
				bruTemp.Color = m_clrDateValue;
				penOneWidthLine.Color = m_clrDST;
				float fltInputTotalHeight = 0;
				for(int j2=0;j2<objRecord.m_arlInputValue.Count;j2++)
				{
					clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];

					string strDesc = objInputValue.m_fltValue.ToString("0.00");
                    p_objGrp.DrawString(strDesc, fntSpecialDateText, bruTemp, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 2);
					
					SizeF szfDesc = p_objGrp.MeasureString(strDesc,fntSpecialDateText);

					if(objInputValue.m_objDeleteInfo != null)
					{
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 10, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 10);
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 13, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 13);
					}

					fltInputTotalHeight += szfDesc.Height;
				}
				//画大便
				penOneWidthLine.Color = m_clrDST;
				float fltDejectaTotalHeight = 0;
				for(int j2=0;j2<objRecord.m_arlDejectaValue.Count;j2++)
				{
					clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];

					string strDesc = objDejectaValue.m_strDesc;					
					p_objGrp.DrawString(strDesc,fntSpecialDateText,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+2);
					
					SizeF szfDesc = p_objGrp.MeasureString(strDesc,fntSpecialDateText);

					if(objDejectaValue.m_objDeleteInfo != null)
					{
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+10,c_intTextTotleWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+10);
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+13,c_intTextTotleWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intInputTotalHeight+(int)fltDejectaTotalHeight+13);
					}

					fltDejectaTotalHeight += szfDesc.Height;
				}

				//画尿量
				penOneWidthLine.Color = m_clrDST;
				float fltPeeTotalHeight = 0;
				for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
				{
					clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

					string strDesc = null;
					if(!objPeeValue.m_blnIsIrretention)
					{
						strDesc = objPeeValue.m_fltValue.ToString("0.00");
					}
					else
					{
						strDesc = "*";
					}

					p_objGrp.DrawString(strDesc,fntSpecialDateText,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intDejectaTotalHeight+(int)fltPeeTotalHeight+2);
					
					SizeF szfDesc = p_objGrp.MeasureString(strDesc,fntSpecialDateText);

					if(objPeeValue.m_objDeleteInfo != null)
					{
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intDejectaTotalHeight+(int)fltPeeTotalHeight+10,c_intTextTotleWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intDejectaTotalHeight+(int)fltPeeTotalHeight+10);
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intDejectaTotalHeight+(int)fltPeeTotalHeight+13,c_intTextTotleWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intDejectaTotalHeight+(int)fltPeeTotalHeight+13);
					}

					fltPeeTotalHeight += szfDesc.Height;
				}

				//画引流量
				penOneWidthLine.Color = m_clrDST;
				float fltOutStreamTotalHeight = 0;
				for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
				{
					clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

					string strDesc = objOutStreamValue.m_fltValue.ToString("0.00");
					p_objGrp.DrawString(strDesc,fntSpecialDateText,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPeeTotalHeight+(int)fltOutStreamTotalHeight+2);
					
					SizeF szfDesc = p_objGrp.MeasureString(strDesc,fntSpecialDateText);

					if(objOutStreamValue.m_objDeleteInfo != null)
					{
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPeeTotalHeight+(int)fltOutStreamTotalHeight+10,c_intTextTotleWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intPeeTotalHeight+(int)fltOutStreamTotalHeight+10);
						p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+4+intStartPrintX,c_intPeeTotalHeight+(int)fltOutStreamTotalHeight+13,c_intTextTotleWidth+c_intGridHeight*i*6+4+(int)szfDesc.Width+intStartPrintX,c_intPeeTotalHeight+(int)fltOutStreamTotalHeight+13);
					}

					fltOutStreamTotalHeight += szfDesc.Height;
				}

				//画体重
				penOneWidthLine.Color = m_clrDST;
				float fltWeightTotalHeight = 0;
				for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
				{
					clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

					string strDesc = null;
					if(objWeightValue.m_enmWeightType == enmThreeMeasureWeightType.一般)
					{
						strDesc = objWeightValue.m_fltValue.ToString("0.00");
                        p_objGrp.DrawString(strDesc, fntSpecialDateText, bruTemp, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 2);
					}
					else
					{
						strDesc = objWeightValue.m_enmWeightType.ToString();
                        p_objGrp.DrawString(strDesc, fntSpecialDateText, bruTemp, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 2);
					}

					
					SizeF szfDesc = p_objGrp.MeasureString(strDesc,fntSpecialDateText);

					if(objWeightValue.m_objDeleteInfo != null)
					{
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 10, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 10);
                        p_objGrp.DrawLine(penOneWidthLine, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 13, c_intTextTotleWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 13);
					}

					fltWeightTotalHeight += szfDesc.Height;
				}
				
				//画皮试	
				int intSkinTestNotPPDCount = 0;
				penOneWidthLine.Color = m_clrDST;
				for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
				{
					clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

					if(objSkinValue.m_intTimeIndex >= 0)
					{

					}
					else
					{
						//非ppd
						SizeF szfName = p_objGrp.MeasureString(objSkinValue.m_strMedicineName,fntSymbol);

						string strValue = null;
						bruTemp.Color = m_clrSkinTest;
						p_objGrp.DrawString(objSkinValue.m_strMedicineName,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+2);
						if(objSkinValue.m_blnIsBad)
						{
							strValue = "(";
							for(int z1=0;z1<objSkinValue.m_intBadCount;z1++)
							{
								strValue +="+";
							}
							strValue += ")";
							bruTemp.Color = Color.Red;
							p_objGrp.DrawString(strValue,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height);
						}
						else
						{
							strValue = "(-)";
							bruTemp.Color = m_clrSkinTest;
							p_objGrp.DrawString(strValue,fntSymbol,bruTemp,c_intTextTotleWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height);
						}		
				
						if(objSkinValue.m_objDeleteInfo != null)
						{
							SizeF szfDesc = p_objGrp.MeasureString(objSkinValue.m_strMedicineName+strValue,fntSymbol);
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+10,c_intTextTotleWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+10);
							p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+13,c_intTextTotleWidth+c_intGridHeight*i*6+1+(int)szfDesc.Width+1+intStartPrintX,c_intWeightTotalHeight+intSkinTestNotPPDCount*(int)szfName.Height+13);
						}

						intSkinTestNotPPDCount++;
					}
				}

				//画其它
				bruTemp.Color = m_clrSkinTest;
				penOneWidthLine.Color = m_clrDST;
				float fltTotalHeight = 0;
                Font fntOtherText = new Font("宋体", c_flt9PointFontSize);
				for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
				{
					clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];
                    RectangleF rectF = new RectangleF(c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+2,c_intGridHeight*6,30);
                    SizeF szfDesc = p_objGrp.MeasureString(objOtherValue.m_strOther, fntOtherText,(int)rectF.Width);
                    rectF.Height = szfDesc.Height;
                    p_objGrp.DrawString(objOtherValue.m_strOther, fntOtherText, bruTemp, rectF);

                    //if(objOtherValue.m_objDeleteInfo != null)
                    //{
                    //    string [] strValueArr = objOtherValue.m_strOther.Split('\r','\n');
                    //    int intHeight = 0;
                    //    for(int k3=0;k3<strValueArr.Length;k3++)
                    //    {
                    //        if(strValueArr[k3].Trim().Length == 0)
                    //            continue;

                    //        SizeF szfDescPart = p_objGrp.MeasureString(strValueArr[k3],fntSpecialDateText);
                    //        p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+10+intHeight,c_intTextTotleWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+10+intHeight);
                    //        p_objGrp.DrawLine(penOneWidthLine,c_intTextTotleWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+13+intHeight,c_intTextTotleWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+13+intHeight);

                    //        intHeight += 19;
                    //    }
                    //}

                    fltTotalHeight += szfDesc.Height;
				}
			}	

			//画脉搏
			penOneWidthLine.Color = m_clrPulseLine;
			bool blnFirstPulse = true;
			while(m_objPulseValueManager.m_blnNext())
			{
				clsThreeMeasurePulseValue objValue = m_objPulseValueManager.m_ObjCurrent;

				float fltXPosTemp = objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX;

				if(fltXPosTemp+(float)c_intGridHeight/2f+3 < c_intTextTotleWidth)
					continue;
				else if(fltXPosTemp+(float)c_intGridHeight/2f+3 > c_intTextTotleWidth+m_intRecordLength*6*c_intGridHeight)
					break;

				if(objValue.m_intCoverID == int.MinValue)
					p_objGrp.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX,objValue.m_fltYPos-(float)c_intGridHeight/2f);

				if(!blnFirstPulse && objValue.m_blnLineToPreValue && objValue.m_objDeleteInfo == null )// && objValue.m_objPreValue != m_objPulseValueManager.m_ObjHeader && objValue.m_objPreValue.m_objDeleteInfo == null)
				{
					clsThreeMeasurePulseValue objPreTempValue = objValue.m_objPreValue;

					while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objPulseValueManager.m_ObjHeader)
						objPreTempValue = objPreTempValue.m_objPreValue;

					if(objPreTempValue != m_objPulseValueManager.m_ObjHeader)
						p_objGrp.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX,objValue.m_fltYPos,objPreTempValue.m_fltXPos+intStartPrintX,objPreTempValue.m_fltYPos);
				}
				else if(blnFirstPulse)
				{
					blnFirstPulse = false;
				}
			}
			m_objPulseValueManager.m_mthReset();

			//画温度
			penOneWidthLine.Color = m_clrTemperatureLine;
			penDotLine.Color = m_clrDownTemperatureLine;
            penReaLine.Color = m_clrDownTemperatureLine;
			bruTemp.Color = m_clrLowerEventText;
			bool blnFirstTemperature = true;
			while(m_objTemperatureValueManager.m_blnNext())
			{
				clsThreeMeasureTemperatureValue objValue = m_objTemperatureValueManager.m_ObjCurrent;

				float fltXPosTemp = objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX;

				if(fltXPosTemp+(float)c_intGridHeight/2f+3 < c_intTextTotleWidth)
					continue;
				else if(fltXPosTemp+(float)c_intGridHeight/2f+3 > c_intTextTotleWidth+m_intRecordLength*6*c_intGridHeight)
					break;

				if(objValue.m_fltValue >= 35)
				{
					p_objGrp.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX,objValue.m_fltYPos-(float)c_intGridHeight/2f);

					if(!blnFirstTemperature && objValue.m_blnLineToPreValue && objValue.m_objDeleteInfo == null )// && objValue.m_objPreValue != m_objPulseValueManager.m_ObjHeader && objValue.m_objPreValue.m_objDeleteInfo == null)
					{
						clsThreeMeasureTemperatureValue objPreTempValue = objValue.m_objPreValue;

						while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objTemperatureValueManager.m_ObjHeader)
							objPreTempValue = objPreTempValue.m_objPreValue;

						if(objPreTempValue != m_objTemperatureValueManager.m_ObjHeader
							&& objPreTempValue.m_fltValue >= 35)
							p_objGrp.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX,objValue.m_fltYPos,objPreTempValue.m_fltXPos+intStartPrintX,objPreTempValue.m_fltYPos);
					}
					else if(blnFirstTemperature)
					{
						blnFirstTemperature = false;
					}

					for(int i=0;i<objValue.m_arlPhyscalDownValue.Count;i++)
					{
						clsThreeMeasureTemperaturePhyscalDownValue objDownValue = (clsThreeMeasureTemperaturePhyscalDownValue)objValue.m_arlPhyscalDownValue[i];

						p_objGrp.DrawImage(m_imgDownTemperature,objValue.m_fltXPos-(float)(c_intGridHeight-2)/2+intStartPrintX,objDownValue.m_fltYPos-(float)(c_intGridHeight-2)/2f,c_intGridHeight-2,c_intGridHeight-2);

						if(objDownValue.m_objDeleteInfo == null)
						{
							#region 加入比较，如果名降实升，则用实线，否则用虚线
							if(objDownValue.m_fltValue>objValue.m_fltValue)
							{
								p_objGrp.DrawLine(penReaLine,objValue.m_fltXPos+intStartPrintX,objValue.m_fltYPos,objValue.m_fltXPos+intStartPrintX,objDownValue.m_fltYPos);
							}
							else
							{
								p_objGrp.DrawLine(penDotLine,objValue.m_fltXPos+intStartPrintX,objValue.m_fltYPos,objValue.m_fltXPos+intStartPrintX,objDownValue.m_fltYPos);
							}
							#endregion
							
						}
					}
				}
				else
				{					
					p_objGrp.DrawString("体",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX,c_intLowEventTextStartHeight-1);
					p_objGrp.DrawString("温",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX,c_intLowEventTextStartHeight+c_intGridHeight-1);
					p_objGrp.DrawString("不",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX,c_intLowEventTextStartHeight+2*c_intGridHeight-1);
					p_objGrp.DrawString("升",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX,c_intLowEventTextStartHeight+3*c_intGridHeight-1);

                    //if(objValue.m_objDeleteInfo != null)
                    //{
						penOneWidthLine.Color = m_clrDST;
						p_objGrp.DrawLine(penOneWidthLine,objValue.m_fltXPos+c_intGridHeight/2+intStartPrintX,c_intLowEventTextStartHeight,objValue.m_fltXPos+c_intGridHeight/2+intStartPrintX,c_intLowEventTextStartHeight+4*c_intGridHeight);
						p_objGrp.DrawLine(penOneWidthLine,objValue.m_fltXPos+c_intGridHeight/2+3+intStartPrintX,c_intLowEventTextStartHeight,objValue.m_fltXPos+c_intGridHeight/2+3+intStartPrintX,c_intLowEventTextStartHeight+4*c_intGridHeight);
						penOneWidthLine.Color = m_clrTemperatureLine;
                    //}
				}
			}
			m_objTemperatureValueManager.m_mthReset();
			#endregion
            fntGridText.Dispose();
            fntBigSymbol.Dispose();
            fntRecordDateText.Dispose();
            fntSpecialDateText.Dispose();
            fntSymbol.Dispose();
		}

		/// <summary>
		/// 数字与罗马数字间的转换
		/// </summary>
		/// <param name="p_intTimes">数字</param>
		/// <returns></returns>
		private string m_strGetSpecialDateTimes(int p_intTimes)
		{
			if(p_intTimes <= 0)
				return "";

			string strResult = com.digitalwave.Utility.clsRomanNumeric.m_strDecToRoman(p_intTimes)+"-";

			strResult = strResult.Replace("III","Ⅲ");
			strResult = strResult.Replace("II","Ⅱ");
			strResult = strResult.Replace("I","Ⅰ");

			return strResult;
		}

		private void ctlThreeMeasureRecord_Resize(object sender, System.EventArgs e)
		{
			if(!m_blnCanResize)
				return;

			if(this.Height != m_intTotalHeight || this.Width != c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight)
			{
				m_blnCanResize = false;

				this.Height = m_intTotalHeight;
				this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

				m_blnCanResize = true;

				this.Invalidate();
			}
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

				//翻页
				if(m_objDateManager.m_IntRecordCount > m_intTotalDate)
				{
//					m_intTotalDate += 2;
//
//					m_blnCanResize = false;
//
//					this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;
//
//					m_blnCanResize = true;
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
			if(p_objValue == null)
				return false;
			
			int intIndex = m_objDateManager.m_intIndexOf(p_objValue.m_dtmValueTime.Date);

			if(intIndex < 0)
				return false;

			p_objValue.m_fltXPos = c_intTextTotleWidth + intIndex*c_intGridHeight*6+(float)p_objValue.m_dtmValueTime.TimeOfDay.TotalSeconds/(4*3600)*c_intGridHeight;
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(180-p_objValue.m_intValue)/180f)*(float)(c_intGridHeight*c_intGridHeightCount);

			switch(p_objValue.m_enmType)
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

			if(blnOK)
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

			p_objEvent.m_intTimeIndex = (int)(p_objEvent.m_dtmEventTime.TimeOfDay.TotalSeconds-60)/(4*3600);

			//计算最近的位置，在事件重叠时使用
            //if((p_objEvent.m_dtmEventTime.Hour%6)%4 < 2)
            //    p_objEvent.m_intNearTimeIndex = p_objEvent.m_intTimeIndex - 1;
            //else
            //    p_objEvent.m_intNearTimeIndex = p_objEvent.m_intTimeIndex + 1;

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
                List<clsEventIndex> lstIndex = new List<clsEventIndex>(6);//存放每个事件的实际位置
                clsThreeMeasureEvent objEventPre = (clsThreeMeasureEvent)p_arlEvent[0];
                lstIndex.Add(new clsEventIndex(objEventPre.m_intTimeIndex,objEventPre.m_enmEventType));
                for (int i = 1; i < p_arlEvent.Count; i++)
                {
                    objEventPre = (clsThreeMeasureEvent)p_arlEvent[i];
                    for (int j = 0; j < i; j++)//向前搜索，查看是否已有重叠
                    {
                        if (objEventPre.m_intTimeIndex == lstIndex[j].Index)
                        {
                            if (m_blnIsNearType(((clsThreeMeasureEvent)p_arlEvent[j]).m_enmEventType, objEventPre.m_enmEventType))
                            {
                                if (objEventPre.m_enmEventType == enmThreeMeasureEventType.入院 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.出院 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.分娩 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.出生 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.手术 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.死亡 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.转入 ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.转出)
                                {//体温下方的重叠事件竖着显示，不需要调整
                                    //如果有，并且都是在上面的事件，则放在自己左方最近的第一个事件的右方的格子
                                    lstIndex.Add(new clsEventIndex(lstIndex[lstIndex.Count - 1].Index + 1, objEventPre.m_enmEventType));
                                }
                                break;
                            }
                        }
                    }
                    if (lstIndex.Count == i)//如果前面的事件没有和这个重叠的，则位置不变
                    {
                        lstIndex.Add(new clsEventIndex(objEventPre.m_intTimeIndex, objEventPre.m_enmEventType));
                    }
                    if (lstIndex[i].Index > 5)//查看调整后的位置是否有超过所在的一天的范围（0～5），如果有，则全部事件向前（左）挪一格
                    {
                        lstIndex[i].Index--;
                        clsEventIndex objIndex = lstIndex[i];
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (lstIndex[k].IsUp == lstIndex[i].IsUp)
                            {
                                if (lstIndex[k].Index == objIndex.Index)//调整后有重叠的才向前挪
                                    lstIndex[k].Index--;
                                objIndex = lstIndex[k];
                            }
                        }
                    }
                }
                for (int m = 0; m < lstIndex.Count; m++)
                {
                    if (lstIndex[m].Index < 0)//经过上面的调整，可能有位置为－1的索引，统一放在第一格
                        lstIndex[m].Index = 0;
                    ((clsThreeMeasureEvent)p_arlEvent[m]).m_intNearTimeIndex = lstIndex[m].Index;
                }
            }
        }

        private bool m_blnIsNearType(enmThreeMeasureEventType p_enmTMEventType, enmThreeMeasureEventType p_enmCurrentTMEventType)
        {
            bool blnTMType = p_enmTMEventType == enmThreeMeasureEventType.出院 || 
                p_enmTMEventType == enmThreeMeasureEventType.分娩 || 
                p_enmTMEventType == enmThreeMeasureEventType.入院 || 
                p_enmTMEventType == enmThreeMeasureEventType.手术 || 
                p_enmTMEventType == enmThreeMeasureEventType.死亡 ||
                p_enmTMEventType == enmThreeMeasureEventType.出生 ||
                p_enmTMEventType == enmThreeMeasureEventType.转出 ||
                p_enmTMEventType == enmThreeMeasureEventType.转入;
            bool blnCurrentType = p_enmCurrentTMEventType == enmThreeMeasureEventType.出院 || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.分娩 || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.入院 || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.手术 || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.死亡 ||
                p_enmCurrentTMEventType == enmThreeMeasureEventType.出生 ||
                p_enmCurrentTMEventType == enmThreeMeasureEventType.转出 ||
                p_enmCurrentTMEventType == enmThreeMeasureEventType.转入;
            return blnTMType == blnCurrentType;
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
			
				m_intTotalHeight += intBreathHeight;

				this.Height = m_intTotalHeight;

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
                        //c_intPressureTotalHeight += intInputHeight;
						c_intWeightTotalHeight += intInputHeight;
						c_intSkinTestTotalHeight += intInputHeight;
			
						m_intTotalHeight += intInputHeight;

						this.Height = m_intTotalHeight;

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
                        //c_intPressureTotalHeight += intDejectaHeight;
						c_intWeightTotalHeight += intDejectaHeight;
						c_intSkinTestTotalHeight += intDejectaHeight;
			
						m_intTotalHeight += intDejectaHeight;

						this.Height = m_intTotalHeight;

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
		/// 添加小便
		/// </summary>
		/// <param name="p_objValue">小便</param>
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
					//重新计算小便内容的高度
					if(objRecord.m_arlPeeValue.Count+1 > m_intMaxPeeCount)
					{
						int intPeeHeight = 21;
				
						c_intPeeHeight += intPeeHeight;

						c_intPeeTotalHeight += intPeeHeight;
						c_intOutStreamTotalHeight += intPeeHeight;
                        //c_intPressureTotalHeight += intPeeHeight;
						c_intWeightTotalHeight += intPeeHeight;
						c_intSkinTestTotalHeight += intPeeHeight;
			
						m_intTotalHeight += intPeeHeight;

						this.Height = m_intTotalHeight;
					
						m_intMaxPeeCount = objRecord.m_arlPeeValue.Count+1;	
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlPeeValue.Add(p_objValue);

					return true;
				}
			}	
		}

		/// <summary>
		/// 添加引流量
		/// </summary>
		/// <param name="p_objValue">引流量</param>
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
					//重新计算引流量内容的高度
					if(objRecord.m_arlOutStreamValue.Count+1 > m_intMaxOutStreamCount)
					{
						int intOutStreamHeight = 21;
				
						c_intOutStreamHeight += intOutStreamHeight;

						c_intOutStreamTotalHeight += intOutStreamHeight;
                        //c_intPressureTotalHeight += intOutStreamHeight;
						c_intWeightTotalHeight += intOutStreamHeight;
						c_intSkinTestTotalHeight += intOutStreamHeight;
			
						m_intTotalHeight += intOutStreamHeight;

						this.Height = m_intTotalHeight;
					
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
			
				m_intTotalHeight += intPressureHeight;

				this.Height = m_intTotalHeight;

				m_intMaxPressureCount = objRecord.m_arlPressureValue1.Count+1;	

			}

			return true;
//			else
//			{
//				clsThreeMeasurePressureValue objValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue[objRecord.m_arlPressureValue.Count-1];
//
//				if(objValue.m_objDeleteInfo == null)
//					return false;
//				else
//				{
//					//重新计算血压内容的高度
//					if(objRecord.m_arlPressureValue.Count+1 > m_intMaxPressureCount)
//					{
//						int intPressureHeight = 21;
//				
//						c_intPressureHeight += intPressureHeight;
//						c_intPressureTotalHeight += intPressureHeight;
//						c_intWeightTotalHeight += intPressureHeight;
//						c_intSkinTestTotalHeight += intPressureHeight;
//			
//						m_intTotalHeight += intPressureHeight;
//
//						this.Height = m_intTotalHeight;
//
//						m_intMaxPressureCount = objRecord.m_arlPressureValue.Count+1;	
//					}
//
//					p_objValue.m_objDeleteInfo = null;
//
//
//					objRecord.m_arlPressureValue.Add(p_objValue);
//
//					return true;
//				}
//			}	
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
			
						m_intTotalHeight += intWeightHeight;

						this.Height = m_intTotalHeight;

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


                //if(objValue.m_strMedicineName.ToLower().IndexOf("ppd") >= 0)
                // 当皮试是PPD时显示不正确，所以加了个a跳过这里。
				if(objValue.m_strMedicineName.ToLower().IndexOf("ppda") >= 0)
					intPPDCount++;
			}

            //if(objValue.m_strMedicineName.ToLower().IndexOf("ppd") >= 0)
            // 当皮试是PPD时显示不正确，所以加了个a跳过这里。
			if(p_objValue.m_strMedicineName.ToLower().IndexOf("ppda") >= 0)
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

					m_intTotalHeight += intSkinTestHeight;

					this.Height = m_intTotalHeight;

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

		public string m_strGetOtherName()
		{
			if(m_strOtherName == null)
				return "";
			return m_strOtherName;
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

			m_blnSetOther(p_objValue.m_strOtherItem);

			//重新计算其它内容的高度
			if(objRecord.m_arlOtherValue.Count+1 > m_intMaxOtherCount)
			{
				int intOtherHeight = 21;
				
				c_intOtherHeight += intOtherHeight;
			
				m_intTotalHeight += intOtherHeight;

				this.Height = m_intTotalHeight;				
				
				m_intMaxOtherCount = objRecord.m_arlOtherValue.Count+1;

			}

            //p_objValue.m_strOther = "";

            //if(m_strOtherName == null)
            //{
            //    for(int i=0;i<=p_objValue.m_strOtherItem.Length/4;i++)
            //    {
            //        if((i+1)*4 < p_objValue.m_strOtherItem.Length)
            //        {
            //            p_objValue.m_strOther += p_objValue.m_strOtherItem.Substring(i*4,(i+1)*4)+"\r\n";
            //        }
            //        else
            //        {
            //            p_objValue.m_strOther += p_objValue.m_strOtherItem.Substring(i*4)+":\r\n";
            //        }
            //    }			
            //}			

            p_objValue.m_strOther = p_objValue.m_strOtherItem+p_objValue.m_fltOtherValue.ToString("0.00");

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

				this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

				m_blnCanResize = true;
			}

			return true;
		}

		/// <summary>
		/// 删除手术日期
		/// </summary>
		/// <param name="p_objValue">手术日期</param>
		/// <returns></returns>
		public bool m_blnDeleteSpecialDate(clsThreeMeasureSpecialDate p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmSpecialDate];

			if(objRecord == null 
				|| objRecord.m_objSpecialDate == null 
				|| objRecord.m_objSpecialDate != p_objValue)
				return false;

			objRecord.m_objSpecialDate.m_blnIsNewStart = false;

			return true;
		}

		/// <summary>
		/// 删除脉搏
		/// </summary>
		/// <param name="p_objValue">脉搏</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeletePulseValue(clsThreeMeasurePulseValue p_objValue,bool p_blnInControl)
		{
			if(!p_blnInControl)
			{
				//直接去掉
				bool blnOk = m_objPulseValueManager.m_blnRemoveValue(p_objValue);

				if(!blnOk)
					return false;

				m_mthHandleRecoverByPulse(p_objValue.m_intCoverID);
			}
			else
			{
                //设置删除者
				if(p_objValue.m_objDeleteInfo != null)
					return false;

				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				p_objValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}
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

		/// <summary>
		/// 删除体温
		/// </summary>
		/// <param name="p_objValue">体温</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteTemperatureValue(clsThreeMeasureTemperatureValue p_objValue,bool p_blnInControl)
		{
			if(!p_blnInControl)
			{
				//直接去掉
				bool blnOk = m_objTemperatureValueManager.m_blnRemoveValue(p_objValue);

				if(!blnOk)
					return false;

				m_mthHandleRecoverByTemperature(p_objValue.m_intCoverID);
			}
			else
			{
				//设置删除者
				if(p_objValue.m_objDeleteInfo != null)
					return false;

				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				p_objValue.m_objDeleteInfo = objDeleteInfo;
				
				p_objValue.m_arlPhyscalDownValue.Clear();
			}

			return true;
		}
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

		/// <summary>
		/// 删除物理降温
		/// </summary>
		/// <param name="p_objValue">物理降温</param>
		/// <param name="p_objBase">物理降温相应的温度信息</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeletePhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase,bool p_blnInControl)
		{
			if(!p_blnInControl)
			{
				//直接去掉
				return m_objTemperatureValueManager.m_blnRemovePhyscalDownValue(p_objValue,p_objBase);
			}
			else
			{
				//设置删除者
				if(p_objValue.m_objDeleteInfo != null)
					return false;

				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				p_objValue.m_objDeleteInfo = objDeleteInfo;

				return true;
			}
		}

		/// <summary>
		/// 删除事件
		/// </summary>
		/// <param name="p_objEvent">事件</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteEvent(clsThreeMeasureEvent p_objEvent,bool p_blnInControl)
		{
			if(p_objEvent == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objEvent.m_dtmEventTime.Date];

			if(objRecord == null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉
				objRecord.m_arlEvent.Remove(p_objEvent);
			}
			else
			{
				//设置删除者
				if(p_objEvent.m_objDeleteInfo != null)
					return false;

				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				p_objEvent.m_objDeleteInfo = objDeleteInfo;
			}

            m_mthResetEventTimeIndex(ref objRecord.m_arlEvent);
			return true;
		}

		/// <summary>
		/// 删除呼吸
		/// </summary>
		/// <param name="p_objValue">呼吸</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteBreath(clsThreeMeasureBreathValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmBreathTime.Date];

			if(objRecord == null)
				return false;

			for(int i=0;i<objRecord.m_arlBreath.Count;i++)
			{
				clsThreeMeasureBreathValue objValue = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[i];

				if(objValue == p_objValue)
				{
					if(objValue.m_objDeleteInfo != null)
						return false;

					if(!p_blnInControl)
					{						
						//直接去掉
						objRecord.m_arlBreath.Remove(p_objValue);						
					}
					else
					{
						//设置删除者
						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
						objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
						objDeleteInfo.m_strUserID = m_strUserID;
						objDeleteInfo.m_strUserName = m_strUserName;

						p_objValue.m_objDeleteInfo = objDeleteInfo;
					}

					return true;
				}
			}			

			return false;
		}
		
		/// <summary>
		/// 删除输入液量
		/// </summary>
		/// <param name="p_objValue">输入液量</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteInput(clsThreeMeasureInputValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmInputDate];

			if(objRecord == null || objRecord.m_arlInputValue.Count <= 0)
				return false;			

			clsThreeMeasureInputValue objDeleteValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[objRecord.m_arlInputValue.Count-1];

			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉，但高度不重新计算
				//				if(objRecord.m_arlInputValue.Count > 0)
				//				{
				//					int intInputHeight = 21;
				//
				//					c_intInputHeight -= intInputHeight;
				//
				//					c_intInputTotalHeight -= intInputHeight;
				//					c_intDejectaTotalHeight -= intInputHeight;
				//					c_intPeeTotalHeight -= intInputHeight;
				//					c_intOutStreamTotalHeight -= intInputHeight;
				//					c_intPressureTotalHeight -= intInputHeight;
				//					c_intWeightTotalHeight -= intInputHeight;
				//					c_intSkinTestTotalHeight -= intInputHeight;
				//					m_intTotalHeight -= intInputHeight;
				//					this.Height = m_intTotalHeight;
				//				}

				objRecord.m_arlInputValue.Remove(objDeleteValue);
			}
			else
			{
				//设置删除者
				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}

		/// <summary>
		/// 删除大便
		/// </summary>
		/// <param name="p_objValue">大便</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteDejecta(clsThreeMeasureDejectaValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmDejectaDate];

			if(objRecord == null || objRecord.m_arlDejectaValue.Count <= 0)
				return false;			

			clsThreeMeasureDejectaValue objDeleteValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[objRecord.m_arlDejectaValue.Count-1];

			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉，但高度不重新计算
				//				if(objRecord.m_arlDejectaValue.Count > 0)
				//				{
				//					int intDejectaHeight = 21;
				//
				//					c_intDejectaHeight -= intDejectaHeight;
				//
				//					c_intDejectaTotalHeight -= intDejectaHeight;
				//					c_intPeeTotalHeight -= intDejectaHeight;
				//					c_intOutStreamTotalHeight -= intDejectaHeight;
				//					c_intPressureTotalHeight -= intDejectaHeight;
				//					c_intWeightTotalHeight -= intDejectaHeight;
				//					c_intSkinTestTotalHeight -= intDejectaHeight;
				//					m_intTotalHeight -= intDejectaHeight;
				//					this.Height = m_intTotalHeight;
				//				}

				objRecord.m_arlDejectaValue.Remove(objDeleteValue);
			}
			else
			{
				//设置删除者
				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}

		/// <summary>
		/// 删除小便
		/// </summary>
		/// <param name="p_objValue">小便</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeletePee(clsThreeMeasurePeeValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPeeDate];

			if(objRecord == null || objRecord.m_arlPeeValue.Count <= 0)
				return false;			

			clsThreeMeasurePeeValue objDeleteValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[objRecord.m_arlPeeValue.Count-1];

			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉，但高度不重新计算
				//				if(objRecord.m_arlPeeValue.Count > 0)
				//				{
				//					int intPeeHeight = 21;
				//
				//					c_intPeeHeight -= intPeeHeight;
				//
				//					c_intPeeTotalHeight -= intPeeHeight;
				//					c_intOutStreamTotalHeight -= intPeeHeight;
				//					c_intPressureTotalHeight -= intPeeHeight;
				//					c_intWeightTotalHeight -= intPeeHeight;
				//					c_intSkinTestTotalHeight -= intPeeHeight;
				//					m_intTotalHeight -= intPeeHeight;
				//					this.Height = m_intTotalHeight;
				//				}

				objRecord.m_arlPeeValue.Remove(objDeleteValue);
			}
			else
			{
				//设置删除者
				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}

		/// <summary>
		/// 删除引流量
		/// </summary>
		/// <param name="p_objValue">引流量</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteOutStream(clsThreeMeasureOutStreamValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOutStreamDate];

			if(objRecord == null || objRecord.m_arlOutStreamValue.Count <= 0)
				return false;			

			clsThreeMeasureOutStreamValue objDeleteValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[objRecord.m_arlOutStreamValue.Count-1];

			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉，但高度不重新计算
				//				if(objRecord.m_arlOutStreamValue.Count > 0)
				//				{
				//					int intOutStreamHeight = 21;
				//
				//					c_intOutStreamHeight -= intOutStreamHeight;
				//
				//					c_intOutStreamTotalHeight -= intOutStreamHeight;
				//					c_intPressureTotalHeight -= intOutStreamHeight;
				//					c_intWeightTotalHeight -= intOutStreamHeight;
				//					c_intSkinTestTotalHeight -= intOutStreamHeight;
				//					m_intTotalHeight -= intOutStreamHeight;
				//					this.Height = m_intTotalHeight;
				//				}

				objRecord.m_arlOutStreamValue.Remove(objDeleteValue);
			}
			else
			{
				//设置删除者
				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}

		/// <summary>
		/// 删除血压
		/// </summary>
		/// <param name="p_objValue">血压</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeletePressure(clsThreeMeasurePressureValue p_objValue,int p_intPressureIndex,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPressureDate];

			if(objRecord == null)
				return false;	
		
			ArrayList arlPressureValue = null;
			if(p_intPressureIndex == 0)
				arlPressureValue = objRecord.m_arlPressureValue1;
			else
				arlPressureValue = objRecord.m_arlPressureValue2;

			if(arlPressureValue.Count <= 0)
				return false;

			clsThreeMeasurePressureValue objDeleteValue = (clsThreeMeasurePressureValue)arlPressureValue[arlPressureValue.Count-1];

			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉，但高度不重新计算
				//				if(objRecord.m_arlPressureValue.Count > 0)
				//				{
				//					int intPressureHeight = 21;
				//
				//					c_intPressureHeight -= intPressureHeight;
				//
				//					c_intPressureTotalHeight -= intPressureHeight;
				//					c_intWeightTotalHeight -= intPressureHeight;
				//					c_intSkinTestTotalHeight -= intPressureHeight;
				//					m_intTotalHeight -= intPressureHeight;
				//					this.Height = m_intTotalHeight;
				//				}

				arlPressureValue.Remove(objDeleteValue);
			}
			else
			{
				//设置删除者
				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}
		
		/// <summary>
		/// 删除体重
		/// </summary>
		/// <param name="p_objValue">体重</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteWeight(clsThreeMeasureWeightValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmWeightDate];

			if(objRecord == null || objRecord.m_arlWeightValue.Count <= 0)
				return false;			

			clsThreeMeasureWeightValue objDeleteValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[objRecord.m_arlWeightValue.Count-1];

			if(objDeleteValue != p_objValue || objDeleteValue.m_objDeleteInfo != null)
				return false;

			if(!p_blnInControl)
			{
				//直接去掉，但高度不重新计算
				//				if(objRecord.m_arlWeightValue.Count > 0)
				//				{
				//					int intWeightHeight = 21;
				//
				//					c_intWeightHeight -= intWeightHeight;
				//
				//					c_intWeightTotalHeight -= intWeightHeight;
				//					c_intSkinTestTotalHeight -= intWeightHeight;
				//					m_intTotalHeight -= intWeightHeight;
				//					this.Height = m_intTotalHeight;
				//				}

				objRecord.m_arlWeightValue.Remove(objDeleteValue);
			}
			else
			{
				//设置删除者
				clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
				objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
				objDeleteInfo.m_strUserID = m_strUserID;
				objDeleteInfo.m_strUserName = m_strUserName;

				objDeleteValue.m_objDeleteInfo = objDeleteInfo;
			}

			return true;
		}
		
		/// <summary>
		/// 删除皮试
		/// </summary>
		/// <param name="p_objValue">皮试</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteSkinTest(clsThreeMeasureSkinTestValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null || p_objValue.m_objDeleteInfo != null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmSkinTestDate];

			if(objRecord == null || objRecord.m_arlSkinTestValue.Count == 0)
				return false;

			for(int i=0;i<objRecord.m_arlSkinTestValue.Count;i++)
			{
				clsThreeMeasureSkinTestValue objValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[i];

				if(objValue == p_objValue)
				{
					if(!p_blnInControl)
					{
						//直接去掉，但高度不重新计算
						objRecord.m_arlSkinTestValue.RemoveAt(i);
					
//						if(objValue.m_intTimeIndex == -1)
//						{
//							if(objRecord.m_arlSkinTestValue.Count == m_intMaxSkinTestCount)
//							{
//								int intSkinTestHeight = 21;
//
//								c_intSkinTestHeight -= intSkinTestHeight;
//
//								c_intSkinTestTotalHeight -= intSkinTestHeight;
//								m_intTotalHeight -= intSkinTestHeight;
//								this.Height = m_intTotalHeight;
//							}
//						}
					}
					else
					{
						//设置删除者
						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
						objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
						objDeleteInfo.m_strUserID = m_strUserID;
						objDeleteInfo.m_strUserName = m_strUserName;

						objValue.m_objDeleteInfo = objDeleteInfo;
					}
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 删除其它
		/// </summary>
		/// <param name="p_objValue">其它</param>
		/// <param name="p_blnInControl">是否需要作控制</param>
		/// <returns></returns>
		public bool m_blnDeleteOther(clsThreeMeasureOtherValue p_objValue,bool p_blnInControl)
		{
			if(p_objValue == null || p_objValue.m_objDeleteInfo != null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOtherDate];

			if(objRecord == null || objRecord.m_arlOtherValue.Count == 0)
				return false;

			for(int i=0;i<objRecord.m_arlOtherValue.Count;i++)
			{
				clsThreeMeasureOtherValue objValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[i];

				if(objValue == p_objValue)
				{
					if(!p_blnInControl)
					{
						//直接去掉，但高度不重新计算
						objRecord.m_arlOtherValue.RemoveAt(i);
					
//						if(objRecord.m_arlOtherValue.Count == m_intMaxOtherCount)
//						{							
//							int intOtherHeight = 21;
//							c_intOtherHeight -= intOtherHeight;
//							m_intTotalHeight -= intOtherHeight;
//							this.Height = m_intTotalHeight;
//						}
					}
					else
					{
						//设置删除者
						clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
						objDeleteInfo.m_dtmDeleteTime = new DateTime(1900,1,1);
						objDeleteInfo.m_strUserID = m_strUserID;
						objDeleteInfo.m_strUserName = m_strUserName;

						objValue.m_objDeleteInfo = objDeleteInfo;
					}
					
					return true;
				}
			}

			return false;
		}
		#endregion

		#region 修改函数（先删除后添加）
		public bool m_blnModifySpecialDate(clsThreeMeasureSpecialDate p_objOldValue,clsThreeMeasureSpecialDate p_objNewValue)
		{
			bool blnOk = m_blnDeleteSpecialDate(p_objOldValue);

			if(!blnOk)
				return false;

			blnOk = m_blnAddSpecialDate(p_objNewValue);

			if(!blnOk)
				m_blnAddSpecialDate(p_objOldValue);

			return blnOk;
		}
		public bool m_blnModifyPulseValue(clsThreeMeasurePulseValue p_objOldValue,clsThreeMeasurePulseValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeletePulseValue(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddPulseValue(p_objNewValue);

			if(!blnOk)
				m_blnAddPulseValue(p_objOldValue);

			return blnOk;
		}

		public bool m_blnModifyTemperatureValue(clsThreeMeasureTemperatureValue p_objOldValue,clsThreeMeasureTemperatureValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnAddTemperatureValue(p_objNewValue);

			if(!blnOk)
				return false;

			for(int i=0;i<p_objOldValue.m_arlPhyscalDownValue.Count;i++)
			{
				blnOk = m_blnAddPhyscalDownValue((clsThreeMeasureTemperaturePhyscalDownValue)p_objOldValue.m_arlPhyscalDownValue[i],p_objNewValue);

				if(!blnOk)
					break;
			}

			if(blnOk)
				blnOk = m_blnDeleteTemperatureValue(p_objOldValue,p_blnInControl);

			if(!blnOk)
				m_blnDeleteTemperatureValue(p_objNewValue,p_blnInControl);

			return blnOk;
		}

		public bool m_blnModifyPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objOldValue,clsThreeMeasureTemperaturePhyscalDownValue p_objNewValue,clsThreeMeasureTemperatureValue p_objBase,bool p_blnInControl)
		{
			bool blnOk = m_blnDeletePhyscalDownValue(p_objOldValue,p_objBase,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddPhyscalDownValue(p_objNewValue,p_objBase);

			if(!blnOk)
				m_blnAddPhyscalDownValue(p_objOldValue,p_objBase);

			return blnOk;
		}

		public bool m_blnModifyEvent(clsThreeMeasureEvent p_objOldEvent,clsThreeMeasureEvent p_objNewEvent,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteEvent(p_objOldEvent,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddEvent(p_objNewEvent);

			if(!blnOk)
				m_blnAddEvent(p_objOldEvent);

			return blnOk;
		}

		public bool m_blnModifyBreath(clsThreeMeasureBreathValue p_objOldValue,clsThreeMeasureBreathValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteBreath(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddBreath(p_objNewValue);

			if(!blnOk)
				m_blnAddBreath(p_objOldValue);

			return blnOk;
		}
		
		public bool m_blnModifyInput(clsThreeMeasureInputValue p_objOldValue,clsThreeMeasureInputValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteInput(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddInput(p_objNewValue);

			if(!blnOk)
				m_blnAddInput(p_objOldValue);

			return blnOk;
		}

		public bool m_blnModifyDejecta(clsThreeMeasureDejectaValue p_objOldValue,clsThreeMeasureDejectaValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteDejecta(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddDejecta(p_objNewValue);

			if(!blnOk)
				m_blnAddDejecta(p_objOldValue);

			return blnOk;
		}

		public bool m_blnModifyPee(clsThreeMeasurePeeValue p_objOldValue,clsThreeMeasurePeeValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeletePee(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddPee(p_objNewValue);

			if(!blnOk)
				m_blnAddPee(p_objOldValue);

			return blnOk;
		}

		public bool m_blnModifyOutStream(clsThreeMeasureOutStreamValue p_objOldValue,clsThreeMeasureOutStreamValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteOutStream(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddOutStream(p_objNewValue);

			if(!blnOk)
				m_blnAddOutStream(p_objOldValue);

			return blnOk;
		}

		public bool m_blnModifyPressure(clsThreeMeasurePressureValue p_objOldValue,clsThreeMeasurePressureValue p_objNewValue,int p_intPressureIndex,bool p_blnInControl)
		{
			bool blnOk = m_blnDeletePressure(p_objOldValue,p_intPressureIndex,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddPressure(p_objNewValue);

			if(!blnOk)
				m_blnAddPressure(p_objOldValue);

			return blnOk;
		}
		
		public bool m_blnModifyWeight(clsThreeMeasureWeightValue p_objOldValue,clsThreeMeasureWeightValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteWeight(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddWeight(p_objNewValue);

			if(!blnOk)
				m_blnAddWeight(p_objOldValue);

			return blnOk;
		}
		
		public bool m_blnModifySkinTest(clsThreeMeasureSkinTestValue p_objOldValue,clsThreeMeasureSkinTestValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteSkinTest(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddSkinTest(p_objNewValue);

			if(!blnOk)
				m_blnAddSkinTest(p_objOldValue);

			return blnOk;
		}

		public bool m_blnModifyOther(clsThreeMeasureOtherValue p_objOldValue,clsThreeMeasureOtherValue p_objNewValue,bool p_blnInControl)
		{
			bool blnOk = m_blnDeleteOther(p_objOldValue,p_blnInControl);

			if(!blnOk)
				return false;

			blnOk = m_blnAddOther(p_objNewValue);

			if(!blnOk)
				m_blnAddOther(p_objOldValue);

			return blnOk;
		}
		#endregion

		/// <summary>
		/// 刷新
		/// </summary>
		public void m_mthUpdateDisplay()
		{
			this.Invalidate();
		}

		/// <summary>
		/// 清空所有
		/// </summary>
		public void m_mthClearAll()
		{
			/*
			 * 重置所有变量
			 */

			c_intBreathHeight = 28;
			c_intInputHeight = 22;
			c_intDejectaHeight = 22;
			c_intPeeHeight = 22;
			c_intOutStreamHeight = 22;
			c_intPressureHeight = 22;
			c_intWeightHeight = 22;
			c_intSkinTestHeight = 22;
			c_intOtherHeight = 22;			
			
			m_intTotalHeight =
                c_intRecordDateHeight +
                c_intInpateintDateHeight +
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

			this.Height = m_intTotalHeight;		
	
			c_intBreathTotalHeight = 28+24+24+c_intGridHeight*45+28;
			c_intPressureTotalHeight = 28+24+24+c_intGridHeight*45+28+22;
			c_intInputTotalHeight = 28+24+24+c_intGridHeight*45+28+22*2;
			c_intDejectaTotalHeight = 28+24+24+c_intGridHeight*45+28+22*3;
			c_intPeeTotalHeight = 28+24+24+c_intGridHeight*45+28+22*4;
			c_intOutStreamTotalHeight = 28+24+24+c_intGridHeight*45+28+22*5;
			c_intWeightTotalHeight = 28+24+24+c_intGridHeight*45+28+22*6;
			c_intSkinTestTotalHeight = 28+24+24+c_intGridHeight*45+28+22*7;            

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

			
			m_intTotalDate = 7;

			m_blnCanResize = false;

			this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

			m_blnCanResize = true;

			m_strOtherName = null;
			
			this.Invalidate();
		}

		#region 鼠标点击事件
		private void ctlThreeMeasureRecord_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{	
			//翻页后模拟鼠标X坐标向右移动多少周
			int intX = e.X + c_intGridHeight * 42 * (m_intWeekNum - 1);

			if(e.X > c_intTextTotleWidth && e.Y > 0
				&& e.X < this.Width  && e.Y < this.Height)
			{
				int intDateIndex = (intX-c_intTextTotleWidth)/(c_intGridHeight*6);
				int intTimeIndex = ((intX-c_intTextTotleWidth)/c_intGridHeight)%6;

				clsThreeMeasureDateRecord objRecord = m_objDateManager[intDateIndex];

				if(objRecord == null)
					return;

				#region DateRecord
				if(m_evtDateRecordMouseDown != null
					&& e.Y <= c_intRecordDateHeight)
				{
					clsThreeMeasureDateRecordArg objArg = new clsThreeMeasureDateRecordArg();
					objArg.m_objRecord = objRecord;
					objArg.m_intXPos = e.X;
					objArg.m_intYPos = e.Y;

					m_evtDateRecordMouseDown(this,objArg);
				}
				#endregion

				#region SpecialDate
				if(m_evtSpecialDateMouseDown != null
					&& e.Y > c_intInpateintTotalHeight 
					&& e.Y <= c_intSpecialDateTotalHeight)
				{	
					clsThreeMeasureSpecialDateArg objArg = new clsThreeMeasureSpecialDateArg();
					objArg.m_objSpecialDate = objRecord.m_objSpecialDate;
					objArg.m_intXPos = e.X;
					objArg.m_intYPos = e.Y;

					m_evtSpecialDateMouseDown(this,objArg);
				}
				#endregion

				#region Pulse
				if(m_evtPulseMouseDown != null
					&& e.Y > c_intTimeTotalHeight
					&& e.Y <= c_intGridTotalHeight)
				{
					for(int i=0;i<objRecord.m_arlPulseValue.Count;i++)
					{
						clsThreeMeasurePulseValue objPulse = (clsThreeMeasurePulseValue)objRecord.m_arlPulseValue[i];
						
                        //if((!m_blnIsShort && Math.Abs(objPulse.m_fltXPos - (float)e.X) <= c_intGridHeight/2
                        //    && Math.Abs(objPulse.m_fltYPos - (float)e.Y) <= c_intGridHeight/2)
                        //    ||
                        //    (m_blnIsShort && Math.Abs(objPulse.m_fltXPos - (float)e.X) <= c_intGridHeight/2
                        //    && Math.Abs(objPulse.m_fltYPos - ((float)e.Y+c_intGridHeight*15)) <= c_intGridHeight/2))
                        if ((!m_blnIsShort && Math.Abs(objPulse.m_fltXPos - (float)intX) <= c_intGridHeight / 2
                            && Math.Abs(objPulse.m_fltYPos - (float)e.Y) <= c_intGridHeight / 2)
                            ||
                            (m_blnIsShort && Math.Abs(objPulse.m_fltXPos - (float)intX) <= c_intGridHeight / 2
                            && Math.Abs(objPulse.m_fltYPos - ((float)e.Y + c_intGridHeight * 15)) <= c_intGridHeight / 2))
						{
							m_arlMouseEventTemp.Add(objPulse);
						}						
					}

					if(m_arlMouseEventTemp.Count > 0)
					{
						clsThreeMeasurePulseArg objArg = new clsThreeMeasurePulseArg();
						objArg.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arlMouseEventTemp.ToArray(typeof(clsThreeMeasurePulseValue));
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_arlMouseEventTemp.Clear();					

						m_evtPulseMouseDown(this,objArg);
					}
				}
				#endregion

				#region Temperature
				if(m_evtTemperatureMouseDown != null
					&& e.Y > c_intTimeTotalHeight
					&& e.Y <= c_intGridTotalHeight)
				{
					for(int i=0;i<objRecord.m_arlTemperatureValue.Count;i++)
					{
						clsThreeMeasureTemperatureValue objTemperature = (clsThreeMeasureTemperatureValue)objRecord.m_arlTemperatureValue[i];

						if(!m_blnIsShort && objTemperature.m_fltValue < 35
							//&& objTemperature.m_fltXPos - c_intTextTotleWidth - (float)(intDateIndex*6*c_intGridHeight)-(float)(intTimeIndex*c_intGridHeight) <= c_intGridHeight
                            && objTemperature.m_fltXPos  - (float)(intDateIndex * 6 * c_intGridHeight) - (float)(intTimeIndex * c_intGridHeight) <= c_intGridHeight
							&& e.Y > c_intTimeTotalHeight+35*c_intGridHeight )
						{
							m_arlMouseEventTemp.Add(objTemperature);							
						}
						else if(objTemperature.m_fltValue >= 35)
						{
							if((!m_blnIsShort && Math.Abs(objTemperature.m_fltXPos - (float)intX) <= c_intGridHeight/2
								&& Math.Abs(objTemperature.m_fltYPos - (float)e.Y) <= c_intGridHeight/2)
								||
								(m_blnIsShort && Math.Abs(objTemperature.m_fltXPos - (float)intX) <= c_intGridHeight/2
								&& Math.Abs(objTemperature.m_fltYPos - ((float)e.Y+c_intGridHeight*15)) <= c_intGridHeight/2))
							{
								m_arlMouseEventTemp.Add(objTemperature);
							}
							else
							{
								for(int j2=0;j2<objTemperature.m_arlPhyscalDownValue.Count;j2++)
								{
									clsThreeMeasureTemperaturePhyscalDownValue objDownValue = 
										(clsThreeMeasureTemperaturePhyscalDownValue)objTemperature.m_arlPhyscalDownValue[j2];

									if((!m_blnIsShort && Math.Abs(objTemperature.m_fltXPos - (float)intX) <= c_intGridHeight/2-1
										&& Math.Abs(objDownValue.m_fltYPos - (float)e.Y) <= c_intGridHeight/2-1)
										||
										(m_blnIsShort && Math.Abs(objTemperature.m_fltXPos - (float)intX) <= c_intGridHeight/2-1
										&& Math.Abs(objDownValue.m_fltYPos - ((float)e.Y+c_intGridHeight*15)) <= c_intGridHeight/2-1))
									{
										m_arlMouseEventTemp.Add(objTemperature);
										break;
									}
								}
							}
						}
					}

					if(m_arlMouseEventTemp.Count > 0)
					{
						clsThreeMeasureTemperatureArg objArg = new clsThreeMeasureTemperatureArg();
						objArg.m_objTemperatureArr = (clsThreeMeasureTemperatureValue[])m_arlMouseEventTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_arlMouseEventTemp.Clear();					

						m_evtTemperatureMouseDown(this,objArg);
					}
				}
				#endregion

				#region Event
				if(m_evtEventMouseDown != null
					&& e.Y > c_intTimeTotalHeight 
					&& e.Y <= c_intTimeTotalHeight+10*c_intGridHeight
					&& !m_blnIsShort)
				{			
					int intPreTimeIndex = -1;
					int intActureIndex = 0;
					for(int i=0;i<objRecord.m_arlEvent.Count;i++)
					{
						clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[i];

                        if (objEvent.m_enmEventType == enmThreeMeasureEventType.请假 || 
                            objEvent.m_enmEventType == enmThreeMeasureEventType.外出 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.拒测 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.冰敷 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.停冰敷 || 
                            objEvent.m_enmEventType == enmThreeMeasureEventType.降温毡 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.酒精擦浴 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.上呼吸机 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.停呼吸机 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.温水擦浴 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.停降温毡)
                            continue;
						intActureIndex = objEvent.m_intTimeIndex;

						intPreTimeIndex = objEvent.m_intTimeIndex;	

						if(intActureIndex == intTimeIndex)
						{
							m_arlMouseEventTemp.Add(objEvent);
						}
					}

					if(m_arlMouseEventTemp.Count > 0)
					{
						clsThreeMeasureEventArg objArg = new clsThreeMeasureEventArg();
						objArg.m_objEventArr = (clsThreeMeasureEvent[])m_arlMouseEventTemp.ToArray(typeof(clsThreeMeasureEvent));
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_arlMouseEventTemp.Clear();					

						m_evtEventMouseDown(this,objArg);
					}
                }
                else if (m_evtEventMouseDown != null
                    && e.Y > c_intTimeTotalHeight + 35 * c_intGridHeight
                    && e.Y <= c_intTimeTotalHeight + +40 * c_intGridHeight
                    && !m_blnIsShort)
                { 
                    int intPreTimeIndex = -1;
					int intActureIndex = 0;
					for(int i=0;i<objRecord.m_arlEvent.Count;i++)
					{
						clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[i];

                        if (objEvent.m_enmEventType == enmThreeMeasureEventType.出院 || 
                            objEvent.m_enmEventType == enmThreeMeasureEventType.入院 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.转入 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.分娩 || 
                            objEvent.m_enmEventType == enmThreeMeasureEventType.手术 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.出生 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.转出 ||
                            objEvent.m_enmEventType == enmThreeMeasureEventType.死亡)
                            continue;
						intActureIndex = objEvent.m_intTimeIndex;

						intPreTimeIndex = objEvent.m_intTimeIndex;	

						if(intActureIndex == intTimeIndex)
						{
							m_arlMouseEventTemp.Add(objEvent);
						}
					}

					if(m_arlMouseEventTemp.Count > 0)
					{
						clsThreeMeasureEventArg objArg = new clsThreeMeasureEventArg();
						objArg.m_objEventArr = (clsThreeMeasureEvent[])m_arlMouseEventTemp.ToArray(typeof(clsThreeMeasureEvent));
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_arlMouseEventTemp.Clear();					

						m_evtEventMouseDown(this,objArg);
					}
                }
				#endregion

				#region Breath
				if(m_evtBreathMouseDown != null
					&& 
					(
					(!m_blnIsShort && e.Y > c_intTimeTotalHeight+35*c_intGridHeight 
					&& e.Y <= c_intBreathTotalHeight)
					||
					(m_blnIsShort && e.Y > c_intTimeTotalHeight+35*c_intGridHeight-c_intGridHeight*25  
					&& e.Y <= c_intBreathTotalHeight-c_intGridHeight*25 )
					)
					)
				{		
					int intAddIndex = 0;
					if(e.Y > c_intGridTotalHeight+28)
					{
						intAddIndex = (e.Y - (c_intGridTotalHeight+28-c_intGridHeight*25 ))/14+1;
					}

					for(int i=0;i<objRecord.m_arlBreath.Count;i++)
					{
						clsThreeMeasureBreathValue objBreath = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[i];
						
						if(objBreath.m_intTimeIndex == intTimeIndex
							&& 
							(
							(objBreath.m_enmBreathType == enmThreeMeasureBreathType.一般
							&& 
							(
							(!m_blnIsShort && e.Y > c_intGridTotalHeight)
							||
							(m_blnIsShort && e.Y > c_intGridTotalHeight-c_intGridHeight*25 )
							)
							&& objBreath.m_intAddSeq == intAddIndex)
							||
							(!m_blnIsShort && objBreath.m_enmBreathType != enmThreeMeasureBreathType.一般
							&& e.Y <= c_intGridTotalHeight)
							)
							)
						{
							clsThreeMeasureBreathArg objArg = new clsThreeMeasureBreathArg();
							objArg.m_objBreath = objBreath;
							objArg.m_intXPos = e.X;
							objArg.m_intYPos = e.Y;

							m_evtBreathMouseDown(this,objArg);
							break;
						}
					}					
				}
				#endregion

				#region Pressure
				if(m_evtPressureMouseDown != null
					&& 
					(
                    (!m_blnIsShort && e.Y > c_intBreathTotalHeight 
					&& e.Y <= c_intPressureTotalHeight
					&& objRecord.m_arlPressureValue1.Count > 0)
					||
                    (m_blnIsShort && e.Y > c_intBreathTotalHeight - c_intGridHeight * 25 
					&& e.Y <= c_intPressureTotalHeight-c_intGridHeight*25
					&& objRecord.m_arlPressureValue1.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
                    int intValueIndex = (e.Y - (c_intBreathTotalHeight - intDiffHeight)) / 21;
					int intPressureIndex = intTimeIndex<3?0:1;

					ArrayList arlPressureValue = null;

					if(intPressureIndex == 0)
						arlPressureValue = objRecord.m_arlPressureValue1;
					else 
						arlPressureValue = objRecord.m_arlPressureValue2;
					
					if(intValueIndex < arlPressureValue.Count)
					{
						clsThreeMeasurePressureValue objPressure = (clsThreeMeasurePressureValue)arlPressureValue[intValueIndex];

						clsThreeMeasurePressureArg objArg = new clsThreeMeasurePressureArg();
						objArg.m_objPressure = objPressure;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;
						objArg.m_intPressureIndex = intPressureIndex;

						m_evtPressureMouseDown(this,objArg);
					}
					
				}
				#endregion

				#region Input
				if(m_evtInputMouseDown != null
					&& 
					(
                    (!m_blnIsShort && e.Y > c_intPressureTotalHeight 
					&& e.Y <= c_intInputTotalHeight
					&& objRecord.m_arlInputValue.Count > 0)
					||
                    (m_blnIsShort && e.Y > c_intPressureTotalHeight - c_intGridHeight * 25 
					&& e.Y <= c_intInputTotalHeight-c_intGridHeight*25
					&& objRecord.m_arlInputValue.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
                    int intValueIndex = (e.Y - (c_intPressureTotalHeight - intDiffHeight)) / 21;
					
					if(intValueIndex < objRecord.m_arlInputValue.Count)
					{
						clsThreeMeasureInputValue objInput = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[intValueIndex];

						clsThreeMeasureInputArg objArg = new clsThreeMeasureInputArg();
						objArg.m_objInput = objInput;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_evtInputMouseDown(this,objArg);
					}		
					
				}
				#endregion

				#region Dejecta
				if(m_evtDejectaMouseDown != null
					&& 
					(
					(!m_blnIsShort && e.Y > c_intInputTotalHeight 
					&& e.Y <= c_intDejectaTotalHeight
					&& objRecord.m_arlDejectaValue.Count > 0)
					||
					(m_blnIsShort && e.Y > c_intInputTotalHeight-c_intGridHeight*25 
					&& e.Y <= c_intDejectaTotalHeight-c_intGridHeight*25
					&& objRecord.m_arlDejectaValue.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
					int intValueIndex = (e.Y-(c_intInputTotalHeight-intDiffHeight))/21;
					
					if(intValueIndex < objRecord.m_arlDejectaValue.Count)
					{
						clsThreeMeasureDejectaValue objDejecta = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[intValueIndex];

						clsThreeMeasureDejectaArg objArg = new clsThreeMeasureDejectaArg();
						objArg.m_objDejecta = objDejecta;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_evtDejectaMouseDown(this,objArg);
					}					
				}
				#endregion

				#region Pee
				if(m_evtPeeMouseDown != null
					&& 
					(
					(!m_blnIsShort && e.Y > c_intDejectaTotalHeight 
					&& e.Y <= c_intPeeTotalHeight
					&& objRecord.m_arlPeeValue.Count > 0)
					||
					(m_blnIsShort && e.Y > c_intDejectaTotalHeight-c_intGridHeight*25 
					&& e.Y <= c_intPeeTotalHeight-c_intGridHeight*25
					&& objRecord.m_arlPeeValue.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
					int intValueIndex = (e.Y-(c_intDejectaTotalHeight-intDiffHeight))/21;
					
					if(intValueIndex < objRecord.m_arlPeeValue.Count)
					{
						clsThreeMeasurePeeValue objPee = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[intValueIndex];

						clsThreeMeasurePeeArg objArg = new clsThreeMeasurePeeArg();
						objArg.m_objPee = objPee;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_evtPeeMouseDown(this,objArg);
					}
					
				}
				#endregion

				#region OutStream
				if(m_evtOutStreamMouseDown != null
					&&
					(
					(!m_blnIsShort && e.Y > c_intPeeTotalHeight 
					&& e.Y <= c_intOutStreamTotalHeight
					&& objRecord.m_arlOutStreamValue.Count > 0)
					||
					(m_blnIsShort && e.Y > c_intPeeTotalHeight-c_intGridHeight*25 
					&& e.Y <= c_intOutStreamTotalHeight-c_intGridHeight*25
					&& objRecord.m_arlOutStreamValue.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
					int intValueIndex = (e.Y-(c_intPeeTotalHeight-intDiffHeight))/21;
					
					if(intValueIndex < objRecord.m_arlOutStreamValue.Count)
					{
						clsThreeMeasureOutStreamValue objOutStream = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[intValueIndex];

						clsThreeMeasureOutStreamArg objArg = new clsThreeMeasureOutStreamArg();
						objArg.m_objOutStream = objOutStream;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_evtOutStreamMouseDown(this,objArg);
					}
				}
				#endregion

				#region Weight
				if(m_evtWeightMouseDown != null
					&& 
					(
                    (!m_blnIsShort && e.Y > c_intOutStreamTotalHeight 
					&& e.Y <= c_intWeightTotalHeight
					&& objRecord.m_arlWeightValue.Count > 0)
					||
                    (m_blnIsShort && e.Y > c_intOutStreamTotalHeight - c_intGridHeight * 25 
					&& e.Y <= c_intWeightTotalHeight-c_intGridHeight*25
					&& objRecord.m_arlWeightValue.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
                    int intValueIndex = (e.Y - (c_intOutStreamTotalHeight - intDiffHeight)) / 21;
					
					if(intValueIndex < objRecord.m_arlWeightValue.Count)
					{
						clsThreeMeasureWeightValue objWeight = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[intValueIndex];

						clsThreeMeasureWeightArg objArg = new clsThreeMeasureWeightArg();
						objArg.m_objWeight = objWeight;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_evtWeightMouseDown(this,objArg);
					}
				}
				#endregion

				#region SkinTest
				if(m_evtSkinTestMouseDown != null)
				{
					int intType = 0;//不在里面

					if(!m_blnIsShort && e.Y > c_intTimeTotalHeight+35*c_intGridHeight 
						&& e.Y <= c_intBreathTotalHeight)
						intType = 1;//PPD

					if(intType == 0 &&
						(
						(!m_blnIsShort && e.Y > c_intWeightTotalHeight
						&& e.Y <= c_intSkinTestTotalHeight
						&& objRecord.m_arlSkinTestValue.Count != 0)
						||
						(m_blnIsShort && e.Y > c_intWeightTotalHeight-c_intGridHeight*25 
						&& e.Y <= c_intSkinTestTotalHeight-c_intGridHeight*25 
						&& objRecord.m_arlSkinTestValue.Count != 0)
						)
						)
						intType = 2;//非PPD

					switch(intType)
					{
						case 1:
							for(int i=0;i<objRecord.m_arlSkinTestValue.Count;i++)
							{
								clsThreeMeasureSkinTestValue objSkinTest = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[i];

								if(objSkinTest.m_intTimeIndex == intTimeIndex)
								{
									clsThreeMeasureSkinTestArg objArg = new clsThreeMeasureSkinTestArg();
									objArg.m_objSkinTest = objSkinTest;
									objArg.m_intXPos = e.X;
									objArg.m_intYPos = e.Y;

									m_evtSkinTestMouseDown(this,objArg);
									break;
								}												
							}
							break;
						case 2:
							int intNotPPDCount = 0;
							int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
							int intValueIndex = (e.Y-(c_intWeightTotalHeight-intDiffHeight))/21;
							for(int i=0;i<objRecord.m_arlSkinTestValue.Count;i++)
							{
								clsThreeMeasureSkinTestValue objSkinTest = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[i];

								if(objSkinTest.m_intTimeIndex == -1)
								{
									if(intNotPPDCount == intValueIndex)
									{
										clsThreeMeasureSkinTestArg objArg = new clsThreeMeasureSkinTestArg();
										objArg.m_objSkinTest = objSkinTest;
										objArg.m_intXPos = e.X;
										objArg.m_intYPos = e.Y;

										m_evtSkinTestMouseDown(this,objArg);
										break;
									}
									intNotPPDCount++;
								}												
							}
							break;
					}
				}
				#endregion

				#region Other
				if(m_evtOtherMouseDown != null
					&& 
					(
					(!m_blnIsShort && e.Y > c_intSkinTestTotalHeight 
					&& objRecord.m_arlOtherValue.Count > 0)
					||
					(m_blnIsShort && e.Y > c_intSkinTestTotalHeight-c_intGridHeight*25 
					&& objRecord.m_arlOtherValue.Count > 0)
					)
					)
				{	
					int intDiffHeight = m_blnIsShort?c_intGridHeight*25:0;
					int intValueIndex = (e.Y-(c_intSkinTestTotalHeight-intDiffHeight))/21;
					
					if(intValueIndex < objRecord.m_arlOtherValue.Count)
					{
						clsThreeMeasureOtherValue objOther = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[intValueIndex];

						clsThreeMeasureOtherArg objArg = new clsThreeMeasureOtherArg();
						objArg.m_objOther = objOther;
						objArg.m_intXPos = e.X;
						objArg.m_intYPos = e.Y;

						m_evtOtherMouseDown(this,objArg);

					}
				}
				#endregion
			}
		}		
		#endregion

		/// <summary>
		/// 结束编辑，设置所有修改者的修改时间
		/// </summary>
		public void m_mthFinishEdit()
		{
			for(int i=0;i<m_objDateManager.m_IntRecordCount;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];

				//日期

				//手术或产后日期

				//事件
				for(int j2=0;j2<objRecord.m_arlEvent.Count;j2++)
				{
					clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[j2];

					if(objEvent.m_objDeleteInfo != null
						&& objEvent.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objEvent.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//呼吸
				for(int j2=0;j2<objRecord.m_arlBreath.Count;j2++)
				{
					clsThreeMeasureBreathValue objBreath = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[j2];

					if(objBreath.m_objDeleteInfo != null
						&& objBreath.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objBreath.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//输入液量
				for(int j2=0;j2<objRecord.m_arlInputValue.Count;j2++)
				{
					clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];
				
					if(objInputValue.m_objDeleteInfo != null
						&& objInputValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objInputValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//大便
				for(int j2=0;j2<objRecord.m_arlDejectaValue.Count;j2++)
				{
					clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];
					
					if(objDejectaValue.m_objDeleteInfo != null
						&& objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//尿量
				for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
				{
					clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];
						
					if(objPeeValue.m_objDeleteInfo != null
						&& objPeeValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objPeeValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//引流量
				for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
				{
					clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];
						
					if(objOutStreamValue.m_objDeleteInfo != null
						&& objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//血压
				for(int j2=0;j2<objRecord.m_arlPressureValue1.Count;j2++)
				{
					clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];
						
					if(objPressureValue.m_objDeleteInfo != null
						&& objPressureValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objPressureValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//体重
				for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
				{
					clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];
						
					if(objWeightValue.m_objDeleteInfo != null
						&& objWeightValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objWeightValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//皮试
				for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
				{
					clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];
					
					if(objSkinValue.m_objDeleteInfo != null
						&& objSkinValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objSkinValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}

				//其它
				for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
				{
					clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];
				
					if(objOtherValue.m_objDeleteInfo != null
						&& objOtherValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
						objOtherValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
				}
			}

			//脉搏
			while(m_objPulseValueManager.m_blnNext())
			{
				clsThreeMeasurePulseValue objValue = m_objPulseValueManager.m_ObjCurrent;
				
				if(objValue.m_objDeleteInfo != null
					&& objValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
					objValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
			}

			//温度
			while(m_objTemperatureValueManager.m_blnNext())
			{
				clsThreeMeasureTemperatureValue objValue = m_objTemperatureValueManager.m_ObjCurrent;
				
				if(objValue.m_objDeleteInfo != null
					&& objValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
					objValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;

				if(objValue.m_fltValue >= 35)
				{
					for(int i=0;i<objValue.m_arlPhyscalDownValue.Count;i++)
					{
						clsThreeMeasureTemperaturePhyscalDownValue objDownValue = (clsThreeMeasureTemperaturePhyscalDownValue)objValue.m_arlPhyscalDownValue[i];

						if(objDownValue.m_objDeleteInfo != null
							&& objDownValue.m_objDeleteInfo.m_dtmDeleteTime.Year == 1900)
							objDownValue.m_objDeleteInfo.m_dtmDeleteTime = DateTime.Now;
					}
				}
			}
		}

		#region Handle Xml
		/// <summary>
		/// 根据Xml设置三测表中日期信息
		/// </summary>
		/// <param name="p_objXmlValue">Xml集合</param>
		/// <returns></returns>
		public bool m_blnSetXml(clsThreeMeasureXmlValue p_objXmlValue)
		{
			if(p_objXmlValue == null)
				return false;	
			
			m_blnDeleteRecordDate(m_objDateManager[DateTime.Parse(p_objXmlValue.m_strRecordDate)]);

			clsThreeMeasureDateRecord objRecord = m_objAddRecordDate(DateTime.Parse(p_objXmlValue.m_strRecordDate));

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

			#region BreathXml
			objDoc.LoadXml(p_objXmlValue.m_strBreathXml);
			for(int i=0;i<objDoc.DocumentElement.ChildNodes.Count;i++)
			{
				bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

				clsThreeMeasureBreathValue objBreathValue = new clsThreeMeasureBreathValue();

				objBreathValue.m_dtmBreathTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
				objBreathValue.m_enmBreathType = (enmThreeMeasureBreathType)Enum.Parse(typeof(enmThreeMeasureBreathType),objDoc.DocumentElement.ChildNodes[i].Attributes["BreathType"].Value);
				objBreathValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime),objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
				objBreathValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
				objBreathValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);
				
				m_blnAddBreath(objBreathValue);

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
				objPeeValue.m_blnIsIrretention = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsIrretention"].Value);
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
				objWeightValue.m_enmWeightType = (enmThreeMeasureWeightType)Enum.Parse(typeof(enmThreeMeasureWeightType),objDoc.DocumentElement.ChildNodes[i].Attributes["WeightType"].Value);
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
				objSkinTestValue.m_blnIsBad = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsBad"].Value);
				objSkinTestValue.m_intBadCount = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["BadCount"].Value);
				objSkinTestValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

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
				objOtherValue.m_fltOtherValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
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

		/// <summary>
		/// 获取指定日期的日期信息的Xml
		/// </summary>
		/// <param name="p_dtmRecordDate">日期</param>
		/// <returns></returns>
		public clsThreeMeasureXmlValue m_objGetXml(DateTime p_dtmRecordDate)
		{
			m_mthFinishEdit();

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_dtmRecordDate];

			if(objRecord == null)
				return null;

			clsThreeMeasureXmlValue objXmlValue = new clsThreeMeasureXmlValue();

			if(m_strOtherName == null)
			{
				objXmlValue.m_strOtherName = "";
			}
			else
			{
				objXmlValue.m_strOtherName = m_strOtherName;
			}

			objXmlValue.m_strRecordDate = p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss");

			//手术或产后日期
			if(objRecord.m_objSpecialDate != null)
			{
				m_objXmlStream.SetLength(0);
				m_objXmlWriter.WriteStartDocument();

				m_objXmlWriter.WriteStartElement("SpecialDate");
				m_objXmlWriter.WriteAttributeString("IsNew",objRecord.m_objSpecialDate.m_blnIsNewStart.ToString());
				m_objXmlWriter.WriteAttributeString("Time",objRecord.m_objSpecialDate.m_dtmSpecialDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objRecord.m_objSpecialDate.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteEndElement();
				
				m_objXmlWriter.WriteEndDocument();
				m_objXmlWriter.Flush();
				objXmlValue.m_strSpecialDateXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			}
			else
				objXmlValue.m_strSpecialDateXml = "0";

			//事件
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Events");
			for(int j2=0;j2<objRecord.m_arlEvent.Count;j2++)
			{
				clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[j2];

				m_objXmlWriter.WriteStartElement("Event");
				m_objXmlWriter.WriteAttributeString("EventType",objEvent.m_enmEventType.ToString());
				m_objXmlWriter.WriteAttributeString("Time",objEvent.m_dtmEventTime.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objEvent.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));

				clsThreeMeasureDeleteInfo objDeleteInfo = objEvent.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strEventXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			
			//呼吸
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Breaths");
			for(int j2=0;j2<objRecord.m_arlBreath.Count;j2++)
			{
				clsThreeMeasureBreathValue objBreath = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[j2];

				m_objXmlWriter.WriteStartElement("Breath");
				m_objXmlWriter.WriteAttributeString("BreathType",objBreath.m_enmBreathType.ToString());
				m_objXmlWriter.WriteAttributeString("ParamTime",objBreath.m_enmParamTime.ToString());
				m_objXmlWriter.WriteAttributeString("Value",objBreath.m_intValue.ToString());
				m_objXmlWriter.WriteAttributeString("Time",objBreath.m_dtmBreathTime.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objBreath.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objBreath.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strBreathXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//输入液量
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Inputs");
			for(int j2=0;j2<objRecord.m_arlInputValue.Count;j2++)
			{
				clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];
				
				m_objXmlWriter.WriteStartElement("Input");
				m_objXmlWriter.WriteAttributeString("Value",objInputValue.m_fltValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objInputValue.m_dtmInputDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objInputValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objInputValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strInputXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//大便
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Dejectas");
			for(int j2=0;j2<objRecord.m_arlDejectaValue.Count;j2++)
			{
				clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];
					
				m_objXmlWriter.WriteStartElement("Dejecta");
				m_objXmlWriter.WriteAttributeString("AfterMoreTimes",objDejectaValue.m_blnAfterMoreTimes.ToString());
				m_objXmlWriter.WriteAttributeString("CanDejecta",objDejectaValue.m_blnCanDejecta.ToString());
				m_objXmlWriter.WriteAttributeString("NeedWeight",objDejectaValue.m_blnNeedWeight.ToString());
				m_objXmlWriter.WriteAttributeString("Weight",objDejectaValue.m_fltWeight.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("AfterTimes",objDejectaValue.m_intAfterTimes.ToString());
				m_objXmlWriter.WriteAttributeString("BeforeTimes",objDejectaValue.m_intBeforeTimes.ToString());
				m_objXmlWriter.WriteAttributeString("ClysisTimes",objDejectaValue.m_intClysisTimes.ToString());
				m_objXmlWriter.WriteAttributeString("Time",objDejectaValue.m_dtmDejectaDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objDejectaValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objDejectaValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strDejectaXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//尿量
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Pees");
			for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
			{
				clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];
						
				m_objXmlWriter.WriteStartElement("Pee");
				m_objXmlWriter.WriteAttributeString("IsIrretention",objPeeValue.m_blnIsIrretention.ToString());
				m_objXmlWriter.WriteAttributeString("Value",objPeeValue.m_fltValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objPeeValue.m_dtmPeeDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objPeeValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objPeeValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strPeeXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//引流量
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("OutStreams");
			for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
			{
				clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];
						
				m_objXmlWriter.WriteStartElement("OutStream");
				m_objXmlWriter.WriteAttributeString("Value",objOutStreamValue.m_fltValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objOutStreamValue.m_dtmOutStreamDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objOutStreamValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objOutStreamValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strOutStreamXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//血压
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Pressures");
			for(int j2=0;j2<objRecord.m_arlPressureValue1.Count;j2++)
			{
				clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];
						
				m_objXmlWriter.WriteStartElement("Pressure");
				m_objXmlWriter.WriteAttributeString("SystolicValue",objPressureValue.m_fltSystolicValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("DiastolicValue",objPressureValue.m_fltDiastolicValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objPressureValue.m_dtmPressureDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objPressureValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objPressureValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strPressureXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			
			//血压2
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Pressures2");
			for(int j2=0;j2<objRecord.m_arlPressureValue2.Count;j2++)
			{
				clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[j2];
						
				m_objXmlWriter.WriteStartElement("Pressure");
				m_objXmlWriter.WriteAttributeString("SystolicValue",objPressureValue.m_fltSystolicValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("DiastolicValue",objPressureValue.m_fltDiastolicValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objPressureValue.m_dtmPressureDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objPressureValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objPressureValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strPressureXml2 = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//体重
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Weights");
			for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
			{
				clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];
						
				m_objXmlWriter.WriteStartElement("Weight");
				m_objXmlWriter.WriteAttributeString("WeightType",objWeightValue.m_enmWeightType.ToString());
				m_objXmlWriter.WriteAttributeString("WeightValue",objWeightValue.m_fltValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objWeightValue.m_dtmWeightDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objWeightValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objWeightValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strWeightXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//皮试
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("SkinTests");
			for(int j2=0;j2<objRecord.m_arlSkinTestValue.Count;j2++)
			{
				clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];
					
				m_objXmlWriter.WriteStartElement("SkinTest");
				m_objXmlWriter.WriteAttributeString("IsBad",objSkinValue.m_blnIsBad.ToString());
				m_objXmlWriter.WriteAttributeString("MedicineName",objSkinValue.m_strMedicineName);
				m_objXmlWriter.WriteAttributeString("Time",objSkinValue.m_dtmSkinTestDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("BadCount",objSkinValue.m_intBadCount.ToString());
				m_objXmlWriter.WriteAttributeString("ModifyTime",objSkinValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objSkinValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strSkinTestXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//其它
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Others");
			for(int j2=0;j2<objRecord.m_arlOtherValue.Count;j2++)
			{
				clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];
				
				m_objXmlWriter.WriteStartElement("Other");
				m_objXmlWriter.WriteAttributeString("Value",objOtherValue.m_fltOtherValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Item",objOtherValue.m_strOtherItem);
				m_objXmlWriter.WriteAttributeString("Unit",objOtherValue.m_strOtherUnit);
				m_objXmlWriter.WriteAttributeString("Time",objOtherValue.m_dtmOtherDate.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objOtherValue.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objOtherValue.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strOtherXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//脉搏
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Pulses");
			for(int j2=0;j2<objRecord.m_arlPulseValue.Count;j2++)
			{
				clsThreeMeasurePulseValue objPulse = (clsThreeMeasurePulseValue)objRecord.m_arlPulseValue[j2];
						
				m_objXmlWriter.WriteStartElement("Pulse");
				m_objXmlWriter.WriteAttributeString("LineToPreValue",objPulse.m_blnLineToPreValue.ToString());
				m_objXmlWriter.WriteAttributeString("ParamTime",objPulse.m_enmParamTime.ToString());
				m_objXmlWriter.WriteAttributeString("Type",objPulse.m_enmType.ToString());
				m_objXmlWriter.WriteAttributeString("Value",objPulse.m_intValue.ToString());
				m_objXmlWriter.WriteAttributeString("Time",objPulse.m_dtmValueTime.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objPulse.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objPulse.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}
				m_objXmlWriter.WriteEndElement();					
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strPulseXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);
			

			//体温
			m_objXmlStream.SetLength(0);
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("Temperatures");
			for(int i1=0;i1<objRecord.m_arlTemperatureValue.Count;i1++)
			{
				clsThreeMeasureTemperatureValue objTemperature = (clsThreeMeasureTemperatureValue)objRecord.m_arlTemperatureValue[i1];

				m_objXmlWriter.WriteStartElement("Temperature");
				m_objXmlWriter.WriteAttributeString("LineToPreValue",objTemperature.m_blnLineToPreValue.ToString());
				m_objXmlWriter.WriteAttributeString("ParamTime",objTemperature.m_enmParamTime.ToString());
				m_objXmlWriter.WriteAttributeString("Type",objTemperature.m_enmType.ToString());
				m_objXmlWriter.WriteAttributeString("Value",objTemperature.m_fltValue.ToString("0.00"));
				m_objXmlWriter.WriteAttributeString("Time",objTemperature.m_dtmValueTime.ToString("yyyy-MM-dd HH:mm:ss"));
				m_objXmlWriter.WriteAttributeString("ModifyTime",objTemperature.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
				clsThreeMeasureDeleteInfo objDeleteInfo = objTemperature.m_objDeleteInfo;

				m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteInfo != null)).ToString());

				if(objDeleteInfo != null)
				{
					m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteInfo.m_strUserID);
					m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteInfo.m_strUserName);
				}

				for(int j2=0;j2<objTemperature.m_arlPhyscalDownValue.Count;j2++)
				{
					clsThreeMeasureTemperaturePhyscalDownValue objDown = (clsThreeMeasureTemperaturePhyscalDownValue)objTemperature.m_arlPhyscalDownValue[j2];

					m_objXmlWriter.WriteStartElement("TemperatureDown");
					m_objXmlWriter.WriteAttributeString("Value",objDown.m_fltValue.ToString("0.00"));
					m_objXmlWriter.WriteAttributeString("Time",objDown.m_dtmValueTime.ToString("yyyy-MM-dd HH:mm:ss"));
					m_objXmlWriter.WriteAttributeString("ModifyTime",objDown.m_dtmModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));
					
					clsThreeMeasureDeleteInfo objDeleteDownInfo = objDown.m_objDeleteInfo;

					m_objXmlWriter.WriteAttributeString("IsDelete",((bool)(objDeleteDownInfo != null)).ToString());

					if(objDeleteDownInfo != null)
					{
						m_objXmlWriter.WriteAttributeString("DeleteTime",objDeleteDownInfo.m_dtmDeleteTime.ToString("yyyy-MM-dd HH:mm:ss"));
						m_objXmlWriter.WriteAttributeString("DeleteUserID",objDeleteDownInfo.m_strUserID);
						m_objXmlWriter.WriteAttributeString("DeleteUserName",objDeleteDownInfo.m_strUserName);
					}
					m_objXmlWriter.WriteEndElement();	
				}

				m_objXmlWriter.WriteEndElement();			
			}	
			m_objXmlWriter.WriteEndElement();
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();
			objXmlValue.m_strTemperatureXml = System.Text.Encoding.Unicode.GetString(m_objXmlStream.ToArray(),39*2,(int)m_objXmlStream.Length-39*2);

			return objXmlValue;
		}
		#endregion		

		private ArrayList m_arlTemp = new ArrayList();
		private ArrayList m_arl4AMTemp = new ArrayList();
		private ArrayList m_arl8AMTemp = new ArrayList();
		private ArrayList m_arl12AMTemp = new ArrayList();
		private ArrayList m_arl4PMTemp = new ArrayList();
		private ArrayList m_arl8PMTemp = new ArrayList();
		private ArrayList m_arl12PMTemp = new ArrayList();
		private ArrayList m_arl4AMHalfTemp = new ArrayList();
		private ArrayList m_arl8AMHalfTemp = new ArrayList();
		private ArrayList m_arl12AMHalfTemp = new ArrayList();
		private ArrayList m_arl4PMHalfTemp = new ArrayList();
		private ArrayList m_arl8PMHalfTemp = new ArrayList();
		private ArrayList m_arl12PMHalfTemp = new ArrayList();
		private void m_mthGetDateContentAccessValue(clsThreeMeasureDateRecord p_objRecord,ref clsThreeMeasureNewValueInDate p_objDateValue)
		{
			p_objDateValue.m_obj4AM = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj8AM = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj12AM = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj4PM = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj8PM = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj12PM = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj4AMHalf = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj8AMHalf = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj12AMHalf = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj4PMHalf = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj8PMHalf = new clsThreeMeasureNewContentAccess();
			p_objDateValue.m_obj12PMHalf = new clsThreeMeasureNewContentAccess();

			#region Pulse
			for(int i=0;i<p_objRecord.m_arlPulseValue.Count;i++)
			{
				clsThreeMeasurePulseValue objPulseValue = (clsThreeMeasurePulseValue)p_objRecord.m_arlPulseValue[i];
				if(objPulseValue.m_objDeleteInfo == null)
				{
					m_mthSwitchContentAccessValue(objPulseValue,objPulseValue.m_enmParamTime);
				}
			}
			p_objDateValue.m_obj4AM.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl4AMTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl4AMTemp.Clear();
			p_objDateValue.m_obj4AMHalf.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl4AMHalfTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl4AMHalfTemp.Clear();
			p_objDateValue.m_obj8AM.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl8AMTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl8AMTemp.Clear();
			p_objDateValue.m_obj8AMHalf.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl8AMHalfTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl8AMHalfTemp.Clear();
			p_objDateValue.m_obj12AM.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl12AMTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl12AMTemp.Clear();
			p_objDateValue.m_obj12AMHalf.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl12AMHalfTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl12AMHalfTemp.Clear();
			p_objDateValue.m_obj4PM.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl4PMTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl4PMTemp.Clear();
			p_objDateValue.m_obj4PMHalf.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl4PMHalfTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl4PMHalfTemp.Clear();
			p_objDateValue.m_obj8PM.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl8PMTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl8PMTemp.Clear();
			p_objDateValue.m_obj8PMHalf.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl8PMHalfTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl8PMHalfTemp.Clear();
			p_objDateValue.m_obj12PM.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl12PMTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl12PMTemp.Clear();
			p_objDateValue.m_obj12PMHalf.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arl12PMHalfTemp.ToArray(typeof(clsThreeMeasurePulseValue));
			m_arl12PMHalfTemp.Clear();
			#endregion Pulse

			#region Temperature
			for(int i=0;i<p_objRecord.m_arlTemperatureValue.Count;i++)
			{
				clsThreeMeasureTemperatureValue objTemperatureValue = (clsThreeMeasureTemperatureValue)p_objRecord.m_arlTemperatureValue[i];
				if(objTemperatureValue.m_objDeleteInfo == null)
				{
					m_mthSwitchContentAccessValue(objTemperatureValue,objTemperatureValue.m_enmParamTime);
				}
			}
			p_objDateValue.m_obj4AM.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl4AMTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl4AMTemp.Clear();
			p_objDateValue.m_obj4AMHalf.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl4AMHalfTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl4AMHalfTemp.Clear();
			p_objDateValue.m_obj8AM.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl8AMTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl8AMTemp.Clear();
			p_objDateValue.m_obj8AMHalf.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl8AMHalfTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl8AMHalfTemp.Clear();
			p_objDateValue.m_obj12AM.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl12AMTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl12AMTemp.Clear();
			p_objDateValue.m_obj12AMHalf.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl12AMHalfTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl12AMHalfTemp.Clear();
			p_objDateValue.m_obj4PM.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl4PMTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl4PMTemp.Clear();
			p_objDateValue.m_obj4PMHalf.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl4PMHalfTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl4PMHalfTemp.Clear();
			p_objDateValue.m_obj8PM.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl8PMTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl8PMTemp.Clear();
			p_objDateValue.m_obj8PMHalf.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl8PMHalfTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl8PMHalfTemp.Clear();
			p_objDateValue.m_obj12PM.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl12PMTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl12PMTemp.Clear();
			p_objDateValue.m_obj12PMHalf.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arl12PMHalfTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
			m_arl12PMHalfTemp.Clear();
			#endregion Temperature			

			#region Breath
			for(int i=0;i<p_objRecord.m_arlBreath.Count;i++)
			{
				clsThreeMeasureBreathValue objBreathValue = (clsThreeMeasureBreathValue)p_objRecord.m_arlBreath[i];
				if(objBreathValue.m_objDeleteInfo == null)
				{
					m_mthSwitchContentAccessValue(objBreathValue,objBreathValue.m_enmParamTime);
				}
			}
			p_objDateValue.m_obj4AM.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl4AMTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl4AMTemp.Clear();
			p_objDateValue.m_obj4AMHalf.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl4AMHalfTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl4AMHalfTemp.Clear();
			p_objDateValue.m_obj8AM.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl8AMTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl8AMTemp.Clear();
			p_objDateValue.m_obj8AMHalf.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl8AMHalfTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl8AMHalfTemp.Clear();
			p_objDateValue.m_obj12AM.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl12AMTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl12AMTemp.Clear();
			p_objDateValue.m_obj12AMHalf.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl12AMHalfTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl12AMHalfTemp.Clear();
			p_objDateValue.m_obj4PM.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl4PMTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl4PMTemp.Clear();
			p_objDateValue.m_obj4PMHalf.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl4PMHalfTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl4PMHalfTemp.Clear();
			p_objDateValue.m_obj8PM.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl8PMTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl8PMTemp.Clear();
			p_objDateValue.m_obj8PMHalf.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl8PMHalfTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl8PMHalfTemp.Clear();
			p_objDateValue.m_obj12PM.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl12PMTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl12PMTemp.Clear();
			p_objDateValue.m_obj12PMHalf.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arl12PMHalfTemp.ToArray(typeof(clsThreeMeasureBreathValue));
			m_arl12PMHalfTemp.Clear();
			#endregion Breath
		}
		private void m_mthSwitchContentAccessValue(object p_objValue,enmParamTime p_enmTime)
		{
			switch(p_enmTime)
			{
				case enmParamTime.am4:
					m_arl4AMTemp.Add(p_objValue);
					break;
				case enmParamTime.am4h:
					m_arl4AMHalfTemp.Add(p_objValue);
					break;
				case enmParamTime.am8:
					m_arl8AMTemp.Add(p_objValue);
					break;
				case enmParamTime.am8h:
					m_arl8AMHalfTemp.Add(p_objValue);
					break;
				case enmParamTime.am12:
					m_arl12AMTemp.Add(p_objValue);
					break;
				case enmParamTime.am12h:
					m_arl12AMHalfTemp.Add(p_objValue);
					break;
				case enmParamTime.pm4:
					m_arl4PMTemp.Add(p_objValue);
					break;
				case enmParamTime.pm4h:
					m_arl4PMHalfTemp.Add(p_objValue);
					break;
				case enmParamTime.pm8:
					m_arl8PMTemp.Add(p_objValue);
					break;
				case enmParamTime.pm8h:
					m_arl8PMHalfTemp.Add(p_objValue);
					break;
				case enmParamTime.pm12:
					m_arl12PMTemp.Add(p_objValue);
					break;
				case enmParamTime.pm12h:
					m_arl12PMHalfTemp.Add(p_objValue);
					break;
			}
		}
		/// <summary>
		/// 获取完全正确的日期信息
		/// </summary>
		/// <param name="p_dtmRecordDate">日期</param>
		/// <returns></returns>
		public clsThreeMeasureNewValueInDate m_objGetDateValue(DateTime p_dtmRecordDate)
		{
			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_dtmRecordDate];

			if(objRecord == null)
				return null;

			clsThreeMeasureNewValueInDate objDateValue = new clsThreeMeasureNewValueInDate();

			objDateValue.m_dtmRecordDate = objRecord.m_dtmRecordDate;

			objDateValue.m_objSpecialDate = objRecord.m_objSpecialDate;

			m_mthGetDateContentAccessValue(objRecord,ref objDateValue);

//			for(int i=0;i<objRecord.m_arlPulseValue.Count;i++)
//			{
//				if(((clsThreeMeasurePulseValue)objRecord.m_arlPulseValue[i]).m_objDeleteInfo == null)
//				{
//					m_arlTemp.Add(objRecord.m_arlPulseValue[i]);
//				}
//			}
//			objDateValue.m_objPulseValueArr = (clsThreeMeasurePulseValue[])m_arlTemp.ToArray(typeof(clsThreeMeasurePulseValue));
//			m_arlTemp.Clear();

//			for(int i=0;i<objRecord.m_arlTemperatureValue.Count;i++)
//			{
//				if(((clsThreeMeasureTemperatureValue)objRecord.m_arlTemperatureValue[i]).m_objDeleteInfo == null)
//				{
//					m_arlTemp.Add(objRecord.m_arlTemperatureValue[i]);
//				}
//			}
//			objDateValue.m_objTemperatureValueArr = (clsThreeMeasureTemperatureValue[])m_arlTemp.ToArray(typeof(clsThreeMeasureTemperatureValue));
//			m_arlTemp.Clear();

			for(int i=0;i<objRecord.m_arlEvent.Count;i++)
			{
				if(((clsThreeMeasureEvent)objRecord.m_arlEvent[i]).m_objDeleteInfo == null)
				{
					m_arlTemp.Add(objRecord.m_arlEvent[i]);
				}
			}
			objDateValue.m_objEventArr = (clsThreeMeasureEvent[])m_arlTemp.ToArray(typeof(clsThreeMeasureEvent));
			m_arlTemp.Clear();

//			for(int i=0;i<objRecord.m_arlBreath.Count;i++)
//			{
//				if(((clsThreeMeasureBreathValue)objRecord.m_arlBreath[i]).m_objDeleteInfo == null)
//				{
//					m_arlTemp.Add(objRecord.m_arlBreath[i]);
//				}
//			}
//			objDateValue.m_objBreathValueArr = (clsThreeMeasureBreathValue[])m_arlTemp.ToArray(typeof(clsThreeMeasureBreathValue));
//			m_arlTemp.Clear();

			objDateValue.m_objInputValue = null;
			if(objRecord.m_arlInputValue.Count > 0)
			{
				clsThreeMeasureInputValue objInput = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[objRecord.m_arlInputValue.Count-1];

				if(objInput.m_objDeleteInfo == null)
					objDateValue.m_objInputValue = objInput;
			}

			objDateValue.m_objDejectaValue = null;
			if(objRecord.m_arlDejectaValue.Count > 0)
			{
				clsThreeMeasureDejectaValue objDejecta = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[objRecord.m_arlDejectaValue.Count-1];

				if(objDejecta.m_objDeleteInfo == null)
					objDateValue.m_objDejectaValue = objDejecta;
			}

			objDateValue.m_objPeeValue = null;
			if(objRecord.m_arlPeeValue.Count > 0)
			{
				clsThreeMeasurePeeValue objPee = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[objRecord.m_arlPeeValue.Count-1];

				if(objPee.m_objDeleteInfo == null)
					objDateValue.m_objPeeValue = objPee;
			}

			objDateValue.m_objOutStreamValue = null;
			if(objRecord.m_arlOutStreamValue.Count > 0)
			{
				clsThreeMeasureOutStreamValue objOutStream = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[objRecord.m_arlOutStreamValue.Count-1];

				if(objOutStream.m_objDeleteInfo == null)
					objDateValue.m_objOutStreamValue = objOutStream;
			}

			objDateValue.m_objPressureValue1 = null;
			if(objRecord.m_arlPressureValue1.Count > 0)
			{
				clsThreeMeasurePressureValue objPressure = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[objRecord.m_arlPressureValue1.Count-1];

				if(objPressure.m_objDeleteInfo == null)
					objDateValue.m_objPressureValue1 = objPressure;
			}
			objDateValue.m_objPressureValue2 = null;
			if(objRecord.m_arlPressureValue2.Count > 0)
			{
				clsThreeMeasurePressureValue objPressure = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[objRecord.m_arlPressureValue2.Count-1];

				if(objPressure.m_objDeleteInfo == null)
					objDateValue.m_objPressureValue2 = objPressure;
			}

			objDateValue.m_objWeightValue = null;
			if(objRecord.m_arlWeightValue.Count > 0)
			{
				clsThreeMeasureWeightValue objWeight = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[objRecord.m_arlWeightValue.Count-1];

				if(objWeight.m_objDeleteInfo == null)
					objDateValue.m_objWeightValue = objWeight;
			}

			for(int i=0;i<objRecord.m_arlSkinTestValue.Count;i++)
			{
				if(((clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[i]).m_objDeleteInfo == null)
				{
					m_arlTemp.Add(objRecord.m_arlSkinTestValue[i]);
				}
			}
			objDateValue.m_objSkinTestValueArr = (clsThreeMeasureSkinTestValue[])m_arlTemp.ToArray(typeof(clsThreeMeasureSkinTestValue));
			m_arlTemp.Clear();

			for(int i=0;i<objRecord.m_arlOtherValue.Count;i++)
			{
				if(((clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[i]).m_objDeleteInfo == null)
				{
					m_arlTemp.Add(objRecord.m_arlOtherValue[i]);
				}
			}
			objDateValue.m_objOtherValueArr = (clsThreeMeasureOtherValue[])m_arlTemp.ToArray(typeof(clsThreeMeasureOtherValue));
			m_arlTemp.Clear();

			return objDateValue;
		}		
	}

	public class clsThreeMeasureNewContentAccess
	{
		public clsThreeMeasurePulseValue[] m_objPulseValueArr;

		//注意，体温信息包含了所有的物理降温信息（正确的和已被删除的）
		public clsThreeMeasureTemperatureValue [] m_objTemperatureValueArr;

		public clsThreeMeasureBreathValue [] m_objBreathValueArr;
	}

	/// <summary>
	/// 存放完全正确的日期信息
	/// </summary>
	public class clsThreeMeasureNewValueInDate
	{
		public DateTime m_dtmRecordDate;

		public clsThreeMeasureSpecialDate m_objSpecialDate;

		public clsThreeMeasureNewContentAccess m_obj4AM;

		public clsThreeMeasureNewContentAccess m_obj8AM;

		public clsThreeMeasureNewContentAccess m_obj12AM;

		public clsThreeMeasureNewContentAccess m_obj4PM;

		public clsThreeMeasureNewContentAccess m_obj8PM;

		public clsThreeMeasureNewContentAccess m_obj12PM;

		public clsThreeMeasureNewContentAccess m_obj4AMHalf;

		public clsThreeMeasureNewContentAccess m_obj8AMHalf;

		public clsThreeMeasureNewContentAccess m_obj12AMHalf;

		public clsThreeMeasureNewContentAccess m_obj4PMHalf;

		public clsThreeMeasureNewContentAccess m_obj8PMHalf;

		public clsThreeMeasureNewContentAccess m_obj12PMHalf;

//		public clsThreeMeasurePulseValue[] m_objPulseValueArr;

//		//注意，体温信息包含了所有的物理降温信息（正确的和已被删除的）
//		public clsThreeMeasureTemperatureValue [] m_objTemperatureValueArr;

		public clsThreeMeasureEvent [] m_objEventArr;

//		public clsThreeMeasureBreathValue [] m_objBreathValueArr;

		public clsThreeMeasureInputValue m_objInputValue;

		public clsThreeMeasureDejectaValue m_objDejectaValue;

		public clsThreeMeasurePeeValue m_objPeeValue;

		public clsThreeMeasureOutStreamValue m_objOutStreamValue;

		public clsThreeMeasurePressureValue m_objPressureValue1;

		public clsThreeMeasurePressureValue m_objPressureValue2;

		public clsThreeMeasureWeightValue m_objWeightValue;

		public clsThreeMeasureSkinTestValue [] m_objSkinTestValueArr;

		public clsThreeMeasureOtherValue [] m_objOtherValueArr;
	}


}
