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
	/// <summary>
	/// Summary description for ctlThreeMeasureRecord.
	/// </summary>
    public class clsThreeMeasureRecordPrintTool //: System.Windows.Forms.PictureBox
	{
		public clsThreeMeasureRecordPrintTool()
		{
			// TODO: Add any initialization after the InitForm call

			m_intTotalHeight =
                c_intRecordDateHeight +
                c_intInpateintDateHeight +
				c_intSpecialDateHeight+
				c_intTimeHeight+
				c_intGridHeight*c_intGridHeightCount+
				c_intBreathHeight+
				c_intPressureHeight+
				c_intInputHeight+
				c_intDejectaHeight+
				c_intPeeHeight+
				c_intOutStreamHeight+
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
		/// ��¼��ʼ����������
		/// </summary>
		private DateTime dtmPreDateForSpecialDate = DateTime.Today;
		/// <summary>
		/// ��¼��ǰ����������
		/// </summary>
		private int intSpecialNewStartTimes = 0;
		/// <summary>
		/// Other��Ŀ������
		/// </summary>
		private string m_strOtherName = null;

		/// <summary>
		/// ����XML���ڴ�
		/// </summary>
		private MemoryStream m_objXmlStream;

		/// <summary>
		/// ����XML�Ĺ���
		/// </summary>
		private XmlTextWriter m_objXmlWriter;

		/// <summary>
		/// ��ȡXML�Ĺ���
		/// </summary>
		private XmlParserContext m_objXmlParser;

        /// <summary>
        /// ÿ�ܵ�ĩ����������
        /// </summary>
        private int[] m_strEndSpecialTimes;
        /// <summary>
        /// ÿ�ܵ�ĩ����������
        /// </summary>
        private DateTime[] m_strEndSpecialDate;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private int m_intWeekNum = 1;

		/// <summary>
		/// ��ǰ�û�ID
		/// </summary>
		private string m_strUserID;

		/// <summary>
		/// ��ǰ�û�ID�����úͻ�ȡ
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
		/// ��ǰ�û�����
		/// </summary>
		private string m_strUserName;

		/// <summary>
		/// ��ǰ�û����������úͻ�ȡ
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
		/// ��ǰ�û���˫ɾ���ߵ���ɫ
		/// </summary>
		private Color m_clrDST;		

		/// <summary>
		/// ��ǰ�û���˫ɾ������ɫ�����úͻ�ȡ
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
        /// סԺ�ţ���������סԺ����
        /// </summary>
        public DateTime m_DtmInPatientDate
        {
            set
            {
                m_dtmInPatientDate = value;
            }
        }
		/// <summary>
		/// �Ƿ���ؼ���SizeChanged�¼�
		/// </summary>
		private bool m_blnCanResize = true;

		/// <summary>
		/// ��¼ÿ��ļ�¼��
		/// </summary>
		private clsThreeMeasureDateRecordManager m_objDateManager;
		/// <summary>
		/// ��¼���е��������ݣ����Ѿ���ʱ��˳������
		/// </summary>
		private clsThreeMeasurePulseValueManager m_objPulseValueManager;
		/// <summary>
		/// ��¼���е��������ݣ����Ѿ���ʱ��˳������
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
					//s_intCoverID��int.MaxValueʱ����1���Ϊint.MinValue
					s_intCoverID++;
				}

				return s_intCoverID;
			}
		}		

		#region ����ʽ
		/// <summary>
		/// �ܹ���������ȱʡʹ��7��
		/// </summary>
		private int m_intTotalDate = 7;

		/// <summary>
		/// �ܹ��ĸ߶�
		/// </summary>
		private int m_intTotalHeight;

		//���������С����ֵ
		private const float c_flt12PointFontSize = 12f;
		private const float c_flt10PointFontSize = 10.5f;
		private const float c_flt9PointFontSize = 9f;
		private const float c_flt7PointFontSize = 7.5f;
		private const float c_flt6PointFontSize = 6.75f;
		private const float c_flt5PointFontSize = 6.25f;
		private const float c_flt14PointFontSize = 14.25f;

		//������Ŀ�ĸ߶�
		private const int c_intRecordDateHeight = 17;

        /// <summary>
        /// סԺ�����߶�
        /// </summary>
        private const int c_intInpateintDateHeight = 18;// 24;
        private const int c_intInpateintTotalHeight = c_intRecordDateHeight + 17;

		private const int c_intSpecialDateHeight = 17;
        private const int c_intSpecialDateTotalHeight = c_intInpateintTotalHeight+17;

		private const int c_intTimeHeight = 24;
        private const int c_intTimeTotalHeight = c_intSpecialDateTotalHeight + 24;

		private const int c_intGridHeight = 13;
		private const int c_intGridHeightCount = 45;
        private int c_intGridTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45;
        private int c_intLowEventTextStartHeight = c_intTimeTotalHeight + c_intGridHeight * 35;


		private int c_intBreathHeight = 28;
        private int c_intBreathTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28;
		private int m_intMaxBreathCount = 1;

		private int c_intPressureHeight = 28;
        private int c_intPressureTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *2;
		private int m_intMaxPressureCount = 1;

		private int c_intInputHeight = 28;
        private int c_intInputTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *3;
		private int m_intMaxInputCount = 1;

		private int c_intDejectaHeight = 28;
        private int c_intDejectaTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *4;
		private int m_intMaxDejectaCount = 1;

		private int c_intPeeHeight = 28;
        private int c_intPeeTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *5;
		private int m_intMaxPeeCount = 1;

		private int c_intOutStreamHeight = 28;
        private int c_intOutStreamTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *6;
		private int m_intMaxOutStreamCount = 1;

		private int c_intWeightHeight = 28;
        private int c_intWeightTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *7;
		private int m_intMaxWeightCount = 1;

		private int c_intSkinTestHeight = 28;
        private int c_intSkinTestTotalHeight = c_intTimeTotalHeight + c_intGridHeight * 45 + 28 *8;
		private int m_intMaxSkinTestCount = 1;		

		private int c_intOtherHeight = 28;
		private int m_intMaxOtherCount = 1;

		private const int c_intTextTotleWidth = 115;	
		#endregion

		#region ��ͼ����
		#region Border Line
//		/// <summary>
//		/// �߿���ɫ
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
//		/// ��������ɫ
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
//		/// �����������ɫ
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
//		/// ����������ɫ
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
//		/// ����ʱ����ɫ
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
//		/// ���������������ɫ
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
//		/// �ϲ���40���Ϸ����¼���ɫ
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
//		/// �²���35���·����¼���ɫ
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
//		/// ����������Ϣ��ɫ
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
//		/// �¶�������Ϣ��ɫ
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
//		/// һ���¼��Ϣ��ɫ
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
//		/// Ƥ��������Ϣ��ɫ
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
//		/// ����������ɫ
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
//		/// ����������ɫ
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
//		/// �������¶�������Ϣ��ɫ
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

		#region �ڲ�ʹ�������־��
		/// �������¶�������Ϣ��ɫ
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

		#region ��־�㼰���ʼ��
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
		/// �������������ּ��ת��
		/// </summary>
		/// <param name="p_intTimes">����</param>
		/// <returns></returns>
		private string m_strGetSpecialDateTimes(int p_intTimes)
		{
			if(p_intTimes <= 0)
				return "";

			string strResult = com.digitalwave.Utility.clsRomanNumeric.m_strDecToRoman(p_intTimes)+"-";

			strResult = strResult.Replace("III","��");
			strResult = strResult.Replace("II","��");
			strResult = strResult.Replace("I","��");

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
//				this.Invalidate();
//			}
		}
		
		#region ��Ӻ���
		/// <summary>
		/// ����µ�������Ϣ
		/// </summary>
		/// <param name="p_dtmRecordDate">����</param>
		public void m_mthAddRecordDate(DateTime p_dtmRecordDate)
		{
		    m_objAddRecordDate(p_dtmRecordDate);
		}
		/// <summary>
		/// ����µ�������Ϣ
		/// </summary>
		/// <param name="p_dtmRecordDate">����</param>
		/// <returns></returns>
		private clsThreeMeasureDateRecord m_objAddRecordDate(DateTime p_dtmRecordDate)
		{
			bool blnIsAddNew;
			clsThreeMeasureDateRecord objRecord = m_objDateManager.m_objAddDateRecord(p_dtmRecordDate,out blnIsAddNew);

			clsThreeMeasureDateRecord objResultRecord = objRecord;

			if(blnIsAddNew)
			{
				/*
				 * �����������������Ϣ����룬�����¼������������µ�����
				 */
				if(!m_objDateManager.m_blnIsLast(objRecord))
				{
					//�������			
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

				//�޸Ŀؼ����
				if(m_objDateManager.m_IntRecordCount > m_intTotalDate)
				{
					m_intTotalDate += 2;

					m_blnCanResize = false;

//					this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

					m_blnCanResize = true;
				}

				//�Զ������������
				clsThreeMeasureSpecialDate objSpecialDate = new clsThreeMeasureSpecialDate();
				objSpecialDate.m_blnIsNewStart = false;
				objSpecialDate.m_dtmSpecialDate = p_dtmRecordDate.Date;

				m_blnAddSpecialDate(objSpecialDate);
			}

			return objResultRecord;
		}		

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="p_objValue">��������</param>
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
		/// �������
		/// </summary>
		/// <param name="p_objValue">������Ϣ</param>
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
				case enmThreeMeasurePulseType.����:
					p_objValue.m_imgSymbol = m_imgPulse;
					break;
				case enmThreeMeasurePulseType.����:
					p_objValue.m_imgSymbol = m_imgHeartRate;
					break;
			}

			//��ӵ��������ݼ������ݼ��������������ݼ��˳��
			bool blnOK = m_objPulseValueManager.m_blnAddValue(p_objValue);

			if(blnOK)
			{
				p_objValue.m_objDeleteInfo = null;
				m_mthUpdateListByPulse(p_objValue);
			}

			return blnOK;
		}
		/// <summary>
		/// �����ص�������
		/// </summary>
		/// <param name="p_objValue">����</param>
		private void m_mthUpdateListByPulse(clsThreeMeasurePulseValue p_objValue)
		{
			while(m_objTemperatureValueManager.m_blnNext())
			{
				clsThreeMeasureTemperatureValue objTemperature = m_objTemperatureValueManager.m_ObjCurrent;

				if(Math.Abs(objTemperature.m_fltXPos-p_objValue.m_fltXPos) <= c_fltTwoSymbolDiff
					&& Math.Abs(objTemperature.m_fltYPos-p_objValue.m_fltYPos) <= c_fltTwoSymbolDiff)
				{
					//�ص�
					m_mthHandleCover(p_objValue,objTemperature);
					break;
				}
			}

			m_objTemperatureValueManager.m_mthReset();
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="p_objValue">����</param>
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
					case enmThreeMeasureTemperatureType.�ڱ��¶�:
						p_objValue.m_imgSymbol = m_imgMouthTemperature;
						break;
					case enmThreeMeasureTemperatureType.�ر��¶�:
						p_objValue.m_imgSymbol = m_imgAnusTemperature;
						break;
					case enmThreeMeasureTemperatureType.Ҹ���¶�:
						p_objValue.m_imgSymbol = m_imgArmpitTemperature;
						break;
				}
			}
			else
			{
				p_objValue.m_fltXPos = c_intTextTotleWidth+c_intGridHeight*(intIndex*6+(int)(p_objValue.m_dtmValueTime.TimeOfDay.TotalSeconds)/(4*3600))-1;				
			}

			//��ӵ��������ݼ������ݼ��������������ݼ��˳��
			bool blnOK = m_objTemperatureValueManager.m_blnAddValue(p_objValue);

			if(blnOK && p_objValue.m_fltValue >= 35f)
			{
				p_objValue.m_objDeleteInfo = null;
				m_mthUpdateListByTemperature(p_objValue);
			}

			return blnOK;
		}
		/// <summary>
		/// �����ص�������
		/// </summary>
		/// <param name="p_objValue">����</param>
		private void m_mthUpdateListByTemperature(clsThreeMeasureTemperatureValue p_objValue)
		{
			while(m_objPulseValueManager.m_blnNext())
			{
				clsThreeMeasurePulseValue objPulse = m_objPulseValueManager.m_ObjCurrent;

				if(Math.Abs(objPulse.m_fltXPos-p_objValue.m_fltXPos) <= c_fltTwoSymbolDiff
					&& Math.Abs(objPulse.m_fltYPos-p_objValue.m_fltYPos) <= c_fltTwoSymbolDiff)
				{
					//�ص�
					m_mthHandleCover(objPulse,p_objValue);
					break;
				}
			}

			m_objPulseValueManager.m_mthReset();
		}

		/// <summary>
		/// ���������������ص�
		/// </summary>
		/// <param name="p_objPulse">����</param>
		/// <param name="p_objTemperature">����</param>
		private void m_mthHandleCover(clsThreeMeasurePulseValue p_objPulse,clsThreeMeasureTemperatureValue p_objTemperature)
		{
			/*
			 * ֻ�޸����µ�ͼ�꣬������ͼ�겻�޸ģ�����ͼʱ��������
			 */

			switch(p_objTemperature.m_enmType)
			{
				case enmThreeMeasureTemperatureType.�ڱ��¶�:
					p_objTemperature.m_imgSymbol = m_imgMouthTemperatureCover;
					break;
				case enmThreeMeasureTemperatureType.�ر��¶�:
					p_objTemperature.m_imgSymbol = m_imgAnusTemperatureCover;
					break;
				case enmThreeMeasureTemperatureType.Ҹ���¶�:
					p_objTemperature.m_imgSymbol = m_imgArmpitTemperatureCover;
					break;
			}
		
			//����ID����ͼʱֻ��IDΪ int.MinValue ��ͼ��
			int intCoverID = s_IntCoverID;
			p_objPulse.m_intCoverID = intCoverID;
			p_objTemperature.m_intCoverID = intCoverID;
		}

		/// <summary>
		/// ���������
		/// </summary>
		/// <param name="p_objValue">������</param>
		/// <param name="p_intDownMinutes">�����º�Ĳ���ʱ��</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,int p_intDownMinutes)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValue(p_objValue,p_intDownMinutes);
		}
		
		/// <summary>
		/// ���������
		/// </summary>
		/// <param name="p_objValue">������</param>
		/// <param name="p_objBase">�����¶�Ӧ�Ľ���ǰ���¶ȶ���</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValue(p_objValue,p_objBase);
		}

		/// <summary>
		/// ���������
		/// </summary>
		/// <param name="p_objValue">������</param>
		/// <param name="p_dtmBaseValueTime">�����¶�Ӧ�Ľ���ǰ��ʱ��</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,DateTime p_dtmBaseValueTime)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValue(p_objValue,p_dtmBaseValueTime);
		}

		/// <summary>
		/// �������¶ȶ������������
		/// </summary>
		/// <param name="p_objValue">������</param>
		/// <returns></returns>
		public bool m_blnAddPhyscalDownValueToLast(clsThreeMeasureTemperaturePhyscalDownValue p_objValue)
		{
			p_objValue.m_fltYPos = c_intTimeTotalHeight + ((float)(42f-p_objValue.m_fltValue)/9f)*(float)(c_intGridHeight*c_intGridHeightCount);
			p_objValue.m_objDeleteInfo = null;

			return m_objTemperatureValueManager.m_blnAddPhyscalDownValueToLast(p_objValue);
		}

		/// <summary>
		/// ����¼�
		/// </summary>
		/// <param name="p_objEvent">�¼�</param>
		/// <returns></returns>
		public bool m_blnAddEvent(clsThreeMeasureEvent p_objEvent)
		{
			if(p_objEvent == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objEvent.m_dtmEventTime.Date];

			if(objRecord == null)
				return false;

			p_objEvent.m_intTimeIndex = (int)(p_objEvent.m_dtmEventTime.TimeOfDay.TotalSeconds-60)/(4*3600);

			//���������λ�ã����¼��ص�ʱʹ��
            //if((p_objEvent.m_dtmEventTime.Hour%6)%4 < 2)
            //    p_objEvent.m_intNearTimeIndex = p_objEvent.m_intTimeIndex - 1;
            //else
            //    p_objEvent.m_intNearTimeIndex = p_objEvent.m_intTimeIndex + 1;

			if(p_objEvent.m_enmEventType != enmThreeMeasureEventType.����
				&& p_objEvent.m_enmEventType != enmThreeMeasureEventType.���)
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
        /// �ص���λ�ð�ʱ��������ʾ���ڶ����¼�����һ���Դ����ƣ��ҿ��ܺ��ұߵ������¼��ص�
        /// ��λ�ó���pm12 ��ǰ�����������¼���ǰŲ����am4��ֹ��
        /// һ�쳬��7���¼���ͳһ��am4���ص���
        /// </summary>
        /// <param name="p_arlEvent"></param>
        void m_mthResetEventTimeIndex(ref ArrayList p_arlEvent)
        {
            if (p_arlEvent.Count > 0)
            {
                List<clsEventIndex> lstIndex = new List<clsEventIndex>(6);//���ÿ���¼���ʵ��λ��
                clsThreeMeasureEvent objEventPre = (clsThreeMeasureEvent)p_arlEvent[0];
                lstIndex.Add(new clsEventIndex(objEventPre.m_intTimeIndex, objEventPre.m_enmEventType));
                for (int i = 1; i < p_arlEvent.Count; i++)
                {
                    objEventPre = (clsThreeMeasureEvent)p_arlEvent[i];
                    for (int j = 0; j < i; j++)//��ǰ�������鿴�Ƿ������ص�
                    {
                        if (objEventPre.m_intTimeIndex == lstIndex[j].Index)
                        {
                            if (m_blnIsNearType(((clsThreeMeasureEvent)p_arlEvent[j]).m_enmEventType, objEventPre.m_enmEventType))
                            {
                                if (objEventPre.m_enmEventType == enmThreeMeasureEventType.��Ժ ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.��Ժ ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.���� ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.���� ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.���� ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.���� ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.ת�� ||
                                        objEventPre.m_enmEventType == enmThreeMeasureEventType.ת��)
                                {//�����·����ص��¼�������ʾ������Ҫ����
                                    //����У����Ҷ�����������¼���������Լ�������ĵ�һ���¼����ҷ��ĸ���
                                    lstIndex.Add(new clsEventIndex(lstIndex[lstIndex.Count - 1].Index + 1, objEventPre.m_enmEventType));
                                }
                                break;
                            }
                        }
                    }
                    if (lstIndex.Count == i)//���ǰ����¼�û�к�����ص��ģ���λ�ò���
                    {
                        lstIndex.Add(new clsEventIndex(objEventPre.m_intTimeIndex, objEventPre.m_enmEventType));
                    }
                    if (lstIndex[i].Index > 5)//�鿴�������λ���Ƿ��г������ڵ�һ��ķ�Χ��0��5��������У���ȫ���¼���ǰ����Ųһ��
                    {
                        lstIndex[i].Index--;
                        clsEventIndex objIndex = lstIndex[i];
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (lstIndex[k].IsUp == lstIndex[i].IsUp)
                            {
                                if (lstIndex[k].Index == objIndex.Index)//���������ص��Ĳ���ǰŲ
                                    lstIndex[k].Index--;
                                objIndex = lstIndex[k];
                            }
                        }
                    }
                }
                for (int m = 0; m < lstIndex.Count; m++)
                {
                    if (lstIndex[m].Index < 0)//��������ĵ�����������λ��Ϊ��1��������ͳһ���ڵ�һ��
                        lstIndex[m].Index = 0;
                    ((clsThreeMeasureEvent)p_arlEvent[m]).m_intNearTimeIndex = lstIndex[m].Index;
                }
            }
        }
		/// <summary>
		/// ��Ӻ���
		/// </summary>
		/// <param name="p_objValue">����</param>
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

			//���¼���������ݵĸ߶�
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
		/// �������Һ��
		/// </summary>
		/// <param name="p_objValue">����Һ��</param>
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
					//���¼�������Һ�����ݵĸ߶�
					if(objRecord.m_arlInputValue.Count+1 > m_intMaxInputCount)
					{
						int intInputHeight = 21;
				
						c_intInputHeight += intInputHeight;

                        //c_intPressureTotalHeight += intInputHeight;
						c_intInputTotalHeight += intInputHeight;
						c_intDejectaTotalHeight += intInputHeight;
						c_intPeeTotalHeight += intInputHeight;
						c_intOutStreamTotalHeight += intInputHeight;
						c_intWeightTotalHeight += intInputHeight;
						c_intSkinTestTotalHeight += intInputHeight;
			
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
		/// ��Ӵ��
		/// </summary>
		/// <param name="p_objValue">���</param>
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
					//���¼��������ݵĸ߶�
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
			
						m_intTotalHeight += intDejectaHeight;

//						this.Height = m_intTotalHeight;

						m_intMaxDejectaCount = objRecord.m_arlDejectaValue.Count+1;	
					}

					p_objValue.m_objDeleteInfo = null;

					objRecord.m_arlDejectaValue.Add(p_objValue);
				}
			}	

			//���ô��ֵ����ʾ
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
		/// ���С��
		/// </summary>
		/// <param name="p_objValue">С��</param>
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
					//���¼���С�����ݵĸ߶�
					if(objRecord.m_arlPeeValue.Count+1 > m_intMaxPeeCount)
					{
						int intPeeHeight = 21;
				
						c_intPeeHeight += intPeeHeight;

						c_intPeeTotalHeight += intPeeHeight;
						c_intOutStreamTotalHeight += intPeeHeight;
						c_intPressureTotalHeight += intPeeHeight;
						c_intWeightTotalHeight += intPeeHeight;
						c_intSkinTestTotalHeight += intPeeHeight;
			
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
		/// ���������
		/// </summary>
		/// <param name="p_objValue">������</param>
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
					//���¼������������ݵĸ߶�
					if(objRecord.m_arlOutStreamValue.Count+1 > m_intMaxOutStreamCount)
					{
						int intOutStreamHeight = 21;
				
						c_intOutStreamHeight += intOutStreamHeight;

						c_intOutStreamTotalHeight += intOutStreamHeight;
						c_intPressureTotalHeight += intOutStreamHeight;
						c_intWeightTotalHeight += intOutStreamHeight;
						c_intSkinTestTotalHeight += intOutStreamHeight;
			
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
		/// ���Ѫѹ
		/// </summary>
		/// <param name="p_objValue">Ѫѹ</param>
		/// <returns></returns>
		public bool m_blnAddPressure(clsThreeMeasurePressureValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmPressureDate];

			if(objRecord == null)
				return false;

			//��һ��û��ֵ����ӵ���һ��
			if(objRecord.m_arlPressureValue1.Count <= 0)
			{
				p_objValue.m_objDeleteInfo = null;
				objRecord.m_arlPressureValue1.Add(p_objValue);
				return true;
			}
			else
			{
				clsThreeMeasurePressureValue objValue1 = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[objRecord.m_arlPressureValue1.Count-1];

				//��һ����ֵ�����ѱ�ɾ������ӵ���һ�Σ��������߶�
				if(objValue1.m_objDeleteInfo != null)
				{
					p_objValue.m_objDeleteInfo = null;
					objRecord.m_arlPressureValue1.Add(p_objValue);					
				}
				else
				{
					//��һ����ֵ���Ҹ�ֵ��Ч���ڶ���ûֵ����ӵ��ڶ��Ρ�
					if(objRecord.m_arlPressureValue2.Count <= 0)
					{
						p_objValue.m_objDeleteInfo = null;
						objRecord.m_arlPressureValue2.Add(p_objValue);
						return true;
					}
					else
					{
						clsThreeMeasurePressureValue objValue2 = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[objRecord.m_arlPressureValue2.Count-1];

						//��һ����ֵ���Ҹ�ֵ��Ч���ڶ�����ֵ�����ѱ�ɾ������ӵ��ڶ��Σ��������߶ȡ�
						if(objValue2.m_objDeleteInfo != null)
						{
							p_objValue.m_objDeleteInfo = null;
							objRecord.m_arlPressureValue2.Add(p_objValue);	
						}
						else
						{
							//��һ����ֵ���Ҹ�ֵ��Ч���ڶ�����ֵ���Ҹ�ֵ��Ч��������ӡ�
							return false;
						}
					}
				}

			}

			//���¼���Ѫѹ���ݵĸ߶�
			/*
			 * 1��ȡ��һ�κ͵ڶ��������ĸ���
			 * 2���뵱ǰ����Ѫѹ�����ĸ����Ƚϣ���������еĴ�͸��¸߶�
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

//				this.Height = m_intTotalHeight;

				m_intMaxPressureCount = objRecord.m_arlPressureValue1.Count+1;	

			}

			return true;
		}
		
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="p_objValue">����</param>
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
					//���¼����������ݵĸ߶�
					if(objRecord.m_arlWeightValue.Count+1 > m_intMaxWeightCount)
					{
						int intWeightHeight = 21;
				
						c_intWeightHeight += intWeightHeight;
						c_intWeightTotalHeight += intWeightHeight;
						c_intSkinTestTotalHeight += intWeightHeight;
			
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
		/// ���Ƥ��
		/// </summary>
		/// <param name="p_objValue">Ƥ��</param>
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

				if(objValue.m_strMedicineName.ToLower().IndexOf("ppda") >= 0)
					intPPDCount++;
			}

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
				
				//���¼���Ƥ�����ݵĸ߶�
				if(objRecord.m_arlSkinTestValue.Count-intPPDCount+1 > m_intMaxSkinTestCount)
				{
					int intSkinTestHeight = 21;
				
					c_intSkinTestHeight += intSkinTestHeight;
					c_intSkinTestTotalHeight += intSkinTestHeight;

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
		/// �������
		/// </summary>
		/// <param name="p_objValue">����</param>
		/// <returns></returns>
		public bool m_blnAddOther(clsThreeMeasureOtherValue p_objValue)
		{
			if(p_objValue == null)
				return false;

			clsThreeMeasureDateRecord objRecord = m_objDateManager[p_objValue.m_dtmOtherDate];

			if(objRecord == null)
				return false;

			//���¼����������ݵĸ߶�
			if(objRecord.m_arlOtherValue.Count+1 > m_intMaxOtherCount)
			{
				int intOtherHeight = 21;
				
				c_intOtherHeight += intOtherHeight;
			
				m_intTotalHeight += intOtherHeight;

//				this.Height = m_intTotalHeight;				
				
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

		#region ɾ������
		/// <summary>
		/// ɾ��������Ϣ
		/// </summary>
		/// <param name="p_objRecord">������Ϣ</param>
		/// <returns></returns>
		public bool m_blnDeleteRecordDate(clsThreeMeasureDateRecord p_objRecord)
		{
			bool blnIsLast = m_objDateManager.m_blnIsLast(p_objRecord);

			if(!blnIsLast && m_objDateManager.m_blnContain(p_objRecord))
			{
				//��β��ɾ��			
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

			//ɾ��������Ϣ�����������
			for(int i=p_objRecord.m_arlPulseValue.Count;i>0;i--)
			{
				clsThreeMeasurePulseValue objPulse = (clsThreeMeasurePulseValue)p_objRecord.m_arlPulseValue[0];

				m_objPulseValueManager.m_mthRemoveValue(objPulse,p_objRecord);

				m_mthHandleRecoverByPulse(objPulse.m_intCoverID);
			}

			//ɾ��������Ϣ�����������
			for(int i=p_objRecord.m_arlTemperatureValue.Count;i>0;i--)
			{				
				m_objTemperatureValueManager.m_mthRemoveValue((clsThreeMeasureTemperatureValue)p_objRecord.m_arlTemperatureValue[0],p_objRecord);
			}

			//�޸Ŀؼ����
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
//		/// ɾ����������
//		/// </summary>
//		/// <param name="p_objValue">��������</param>
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
//		/// ɾ������
//		/// </summary>
//		/// <param name="p_objValue">����</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
//		/// <returns></returns>
//		public bool m_blnDeletePulseValue(clsThreeMeasurePulseValue p_objValue,bool p_blnInControl)
//		{
//			if(!p_blnInControl)
//			{
//				//ֱ��ȥ��
//				bool blnOk = m_objPulseValueManager.m_blnRemoveValue(p_objValue);
//
//				if(!blnOk)
//					return false;
//
//				m_mthHandleRecoverByPulse(p_objValue.m_intCoverID);
//			}
//			else
//			{
//                //����ɾ����
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
		/// �ָ����ص���ͼ��
		/// </summary>
		/// <param name="p_intCoverID">�ص���</param>
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
						case enmThreeMeasureTemperatureType.�ڱ��¶�:
							objTemperature.m_imgSymbol = m_imgMouthTemperature;
							break;
						case enmThreeMeasureTemperatureType.�ر��¶�:
							objTemperature.m_imgSymbol = m_imgAnusTemperature;
							break;
						case enmThreeMeasureTemperatureType.Ҹ���¶�:
							objTemperature.m_imgSymbol = m_imgArmpitTemperature;
							break;
					}

					break;
				}
			}
		}
//
//		/// <summary>
//		/// ɾ������
//		/// </summary>
//		/// <param name="p_objValue">����</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
//		/// <returns></returns>
//		public bool m_blnDeleteTemperatureValue(clsThreeMeasureTemperatureValue p_objValue,bool p_blnInControl)
//		{
//			if(!p_blnInControl)
//			{
//				//ֱ��ȥ��
//				bool blnOk = m_objTemperatureValueManager.m_blnRemoveValue(p_objValue);
//
//				if(!blnOk)
//					return false;
//
//				m_mthHandleRecoverByTemperature(p_objValue.m_intCoverID);
//			}
//			else
//			{
//				//����ɾ����
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
		/// �ָ����ص���ͼ��
		/// </summary>
		/// <param name="p_intCoverID">�ص���</param>
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
//		/// ɾ��������
//		/// </summary>
//		/// <param name="p_objValue">������</param>
//		/// <param name="p_objBase">��������Ӧ���¶���Ϣ</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
//		/// <returns></returns>
//		public bool m_blnDeletePhyscalDownValue(clsThreeMeasureTemperaturePhyscalDownValue p_objValue,clsThreeMeasureTemperatureValue p_objBase,bool p_blnInControl)
//		{
//			if(!p_blnInControl)
//			{
//				//ֱ��ȥ��
//				return m_objTemperatureValueManager.m_blnRemovePhyscalDownValue(p_objValue,p_objBase);
//			}
//			else
//			{
//				//����ɾ����
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
//		/// ɾ���¼�
//		/// </summary>
//		/// <param name="p_objEvent">�¼�</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//				//ֱ��ȥ��
//				objRecord.m_arlEvent.Remove(p_objEvent);
//			}
//			else
//			{
//				//����ɾ����
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
//		/// ɾ������
//		/// </summary>
//		/// <param name="p_objValue">����</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//						//ֱ��ȥ��
//						objRecord.m_arlBreath.Remove(p_objValue);						
//					}
//					else
//					{
//						//����ɾ����
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
//		/// ɾ������Һ��
//		/// </summary>
//		/// <param name="p_objValue">����Һ��</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//				//ֱ��ȥ�������߶Ȳ����¼���
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
//				//����ɾ����
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
//		/// ɾ�����
//		/// </summary>
//		/// <param name="p_objValue">���</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//				//ֱ��ȥ�������߶Ȳ����¼���
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
//				//����ɾ����
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
//		/// ɾ��С��
//		/// </summary>
//		/// <param name="p_objValue">С��</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//				//ֱ��ȥ�������߶Ȳ����¼���
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
//				//����ɾ����
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
//		/// ɾ��������
//		/// </summary>
//		/// <param name="p_objValue">������</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//				//ֱ��ȥ�������߶Ȳ����¼���
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
//				//����ɾ����
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
////		/// ɾ��Ѫѹ
////		/// </summary>
////		/// <param name="p_objValue">Ѫѹ</param>
////		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
////				//ֱ��ȥ�������߶Ȳ����¼���
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
////				//����ɾ����
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
//		/// ɾ������
//		/// </summary>
//		/// <param name="p_objValue">����</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//				//ֱ��ȥ�������߶Ȳ����¼���
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
//				//����ɾ����
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
//		/// ɾ��Ƥ��
//		/// </summary>
//		/// <param name="p_objValue">Ƥ��</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//						//ֱ��ȥ�������߶Ȳ����¼���
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
//						//����ɾ����
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
//		/// ɾ������
//		/// </summary>
//		/// <param name="p_objValue">����</param>
//		/// <param name="p_blnInControl">�Ƿ���Ҫ������</param>
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
//						//ֱ��ȥ�������߶Ȳ����¼���
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
//						//����ɾ����
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

		#region �޸ĺ�������ɾ������ӣ�
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
//		/// ˢ��
//		/// </summary>
//		public void m_mthUpdateDisplay()
//		{
//			this.Invalidate();
//		}

		/// <summary>
		/// �������
		/// </summary>
		public void m_mthClearAll()
		{
			/*
			 * �������б���
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
			c_intPeeTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28;
			c_intOutStreamTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28;
			c_intPressureTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28;
			c_intWeightTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28;
			c_intSkinTestTotalHeight = 28+24+24+c_intGridHeight*45+28+28+28+28+28+28+28+28;            

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

//			this.Width = c_intTextTotleWidth+m_intTotalDate*6*c_intGridHeight;

			m_blnCanResize = true;

			m_strOtherName = null;
			
//			this.Invalidate();
		}

		#region Handle Xml
		/// <summary>
		/// ����Xml�����������������Ϣ
		/// </summary>
		/// <param name="p_objXmlValue">Xml����</param>
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
        /// ����Xml�����������������Ϣ������ѡ���ӡ�򲻴�ӡ�ۼ���
        /// </summary>
        /// <param name="p_objXmlValue">Xml����</param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <param name="p_blnIsPrintDel">�Ƿ��ӡ�ۼ���true��ӡ��false����ӡ</param>
        /// <returns></returns>
        public bool m_blnSetXml(clsThreeMeasureXmlValue p_objXmlValue, DateTime p_dtmFirstPrintDate, bool p_blnIsPrintDel)
        {
            if (p_objXmlValue == null)
                return false;

            m_blnDeleteRecordDate(m_objDateManager[DateTime.Parse(p_objXmlValue.m_strRecordDate)]);

            clsThreeMeasureDateRecord objRecord = m_objAddRecordDate(DateTime.Parse(p_objXmlValue.m_strRecordDate));
            objRecord.m_dtmFirstPrintDate = p_dtmFirstPrintDate;

            if (objRecord == null)
                return false;

            m_blnSetOther(p_objXmlValue.m_strOtherName);

            XmlDocument objDoc = new XmlDocument();

            //SpecialDate
            if (p_objXmlValue.m_strSpecialDateXml != "0")
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
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureEvent objEventValue = new clsThreeMeasureEvent();
                    objEventValue.m_dtmEventTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objEventValue.m_enmEventType = (enmThreeMeasureEventType)Enum.Parse(typeof(enmThreeMeasureEventType), objDoc.DocumentElement.ChildNodes[i].Attributes["EventType"].Value);
                    objEventValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddEvent(objEventValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objEventValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureEvent objEventValue = new clsThreeMeasureEvent();
                        objEventValue.m_dtmEventTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objEventValue.m_enmEventType = (enmThreeMeasureEventType)Enum.Parse(typeof(enmThreeMeasureEventType), objDoc.DocumentElement.ChildNodes[i].Attributes["EventType"].Value);
                        objEventValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddEvent(objEventValue);
                    }
                }
            }
            #endregion

            #region BreathXml
            objDoc.LoadXml(p_objXmlValue.m_strBreathXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureBreathValue objBreathValue = new clsThreeMeasureBreathValue();

                    objBreathValue.m_dtmBreathTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objBreathValue.m_enmBreathType = (enmThreeMeasureBreathType)Enum.Parse(typeof(enmThreeMeasureBreathType), objDoc.DocumentElement.ChildNodes[i].Attributes["BreathType"].Value);
                    objBreathValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime), objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
                    objBreathValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objBreathValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddBreath(objBreathValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objBreathValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureBreathValue objBreathValue = new clsThreeMeasureBreathValue();

                        objBreathValue.m_dtmBreathTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objBreathValue.m_enmBreathType = (enmThreeMeasureBreathType)Enum.Parse(typeof(enmThreeMeasureBreathType), objDoc.DocumentElement.ChildNodes[i].Attributes["BreathType"].Value);
                        objBreathValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime), objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
                        objBreathValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objBreathValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddBreath(objBreathValue);
                    }
                }
            }
            #endregion

            #region InputXml
            objDoc.LoadXml(p_objXmlValue.m_strInputXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureInputValue objInputValue = new clsThreeMeasureInputValue();
                    objInputValue.m_dtmInputDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objInputValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objInputValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddInput(objInputValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objInputValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureInputValue objInputValue = new clsThreeMeasureInputValue();
                        objInputValue.m_dtmInputDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objInputValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objInputValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddInput(objInputValue);
                    }
                }
            }
            #endregion

            #region DejectaXml
            objDoc.LoadXml(p_objXmlValue.m_strDejectaXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
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

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objDejectaValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
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
                    }
                }
            }
            #endregion

            #region PeeXml
            objDoc.LoadXml(p_objXmlValue.m_strPeeXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasurePeeValue objPeeValue = new clsThreeMeasurePeeValue();
                    objPeeValue.m_blnIsIrretention = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsIrretention"].Value);
                    objPeeValue.m_dtmPeeDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objPeeValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objPeeValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddPee(objPeeValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objPeeValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasurePeeValue objPeeValue = new clsThreeMeasurePeeValue();
                        objPeeValue.m_blnIsIrretention = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsIrretention"].Value);
                        objPeeValue.m_dtmPeeDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objPeeValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objPeeValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddPee(objPeeValue);
                    }
                }
            }
            #endregion

            #region OutStreamXml
            objDoc.LoadXml(p_objXmlValue.m_strOutStreamXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureOutStreamValue objOutStreamValue = new clsThreeMeasureOutStreamValue();
                    objOutStreamValue.m_dtmOutStreamDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objOutStreamValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objOutStreamValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddOutStream(objOutStreamValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objOutStreamValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureOutStreamValue objOutStreamValue = new clsThreeMeasureOutStreamValue();
                        objOutStreamValue.m_dtmOutStreamDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objOutStreamValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objOutStreamValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddOutStream(objOutStreamValue);
                    }
                }
            }
            #endregion

            #region PressureXml
            objDoc.LoadXml(p_objXmlValue.m_strPressureXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasurePressureValue objPressureValue = new clsThreeMeasurePressureValue();
                    objPressureValue.m_dtmPressureDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objPressureValue.m_fltDiastolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DiastolicValue"].Value);
                    objPressureValue.m_fltSystolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["SystolicValue"].Value);
                    objPressureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddPressure(objPressureValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objPressureValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasurePressureValue objPressureValue = new clsThreeMeasurePressureValue();
                        objPressureValue.m_dtmPressureDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objPressureValue.m_fltDiastolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DiastolicValue"].Value);
                        objPressureValue.m_fltSystolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["SystolicValue"].Value);
                        objPressureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddPressure(objPressureValue);
                    }
                }
            }
            #endregion

            #region PressureXml2
            if (p_objXmlValue.m_strPressureXml2 != null && p_objXmlValue.m_strPressureXml2 != "")
            {
                objDoc.LoadXml(p_objXmlValue.m_strPressureXml2);
                for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                    if (p_blnIsPrintDel)
                    {
                        clsThreeMeasurePressureValue objPressureValue = new clsThreeMeasurePressureValue();
                        objPressureValue.m_dtmPressureDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objPressureValue.m_fltDiastolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DiastolicValue"].Value);
                        objPressureValue.m_fltSystolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["SystolicValue"].Value);
                        objPressureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddPressure(objPressureValue);

                        if (blnIsDelete)
                        {
                            clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                            objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                            objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                            objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                            objPressureValue.m_objDeleteInfo = objDeleteInfo;
                        }
                    }
                    else
                    {
                        if (!blnIsDelete)
                        {
                            clsThreeMeasurePressureValue objPressureValue = new clsThreeMeasurePressureValue();
                            objPressureValue.m_dtmPressureDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                            objPressureValue.m_fltDiastolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DiastolicValue"].Value);
                            objPressureValue.m_fltSystolicValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["SystolicValue"].Value);
                            objPressureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                            m_blnAddPressure(objPressureValue);
                        }
                    }
                }
            }
            #endregion

            #region WeightXml
            objDoc.LoadXml(p_objXmlValue.m_strWeightXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureWeightValue objWeightValue = new clsThreeMeasureWeightValue();
                    objWeightValue.m_dtmWeightDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objWeightValue.m_enmWeightType = (enmThreeMeasureWeightType)Enum.Parse(typeof(enmThreeMeasureWeightType), objDoc.DocumentElement.ChildNodes[i].Attributes["WeightType"].Value);
                    objWeightValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["WeightValue"].Value);
                    objWeightValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddWeight(objWeightValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objWeightValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureWeightValue objWeightValue = new clsThreeMeasureWeightValue();
                        objWeightValue.m_dtmWeightDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objWeightValue.m_enmWeightType = (enmThreeMeasureWeightType)Enum.Parse(typeof(enmThreeMeasureWeightType), objDoc.DocumentElement.ChildNodes[i].Attributes["WeightType"].Value);
                        objWeightValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["WeightValue"].Value);
                        objWeightValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddWeight(objWeightValue);
                    }
                }
            }
            #endregion

            #region SkinTestXml
            objDoc.LoadXml(p_objXmlValue.m_strSkinTestXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureSkinTestValue objSkinTestValue = new clsThreeMeasureSkinTestValue();
                    objSkinTestValue.m_dtmSkinTestDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objSkinTestValue.m_strMedicineName = objDoc.DocumentElement.ChildNodes[i].Attributes["MedicineName"].Value;
                    objSkinTestValue.m_blnIsBad = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsBad"].Value);
                    objSkinTestValue.m_intBadCount = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["BadCount"].Value);
                    objSkinTestValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddSkinTest(objSkinTestValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objSkinTestValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureSkinTestValue objSkinTestValue = new clsThreeMeasureSkinTestValue();
                        objSkinTestValue.m_dtmSkinTestDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objSkinTestValue.m_strMedicineName = objDoc.DocumentElement.ChildNodes[i].Attributes["MedicineName"].Value;
                        objSkinTestValue.m_blnIsBad = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsBad"].Value);
                        objSkinTestValue.m_intBadCount = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["BadCount"].Value);
                        objSkinTestValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddSkinTest(objSkinTestValue);
                    }
                }
            }
            #endregion

            #region OtherXml
            objDoc.LoadXml(p_objXmlValue.m_strOtherXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureOtherValue objOtherValue = new clsThreeMeasureOtherValue();
                    objOtherValue.m_dtmOtherDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objOtherValue.m_strOtherItem = objDoc.DocumentElement.ChildNodes[i].Attributes["Item"].Value;
                    objOtherValue.m_strOtherUnit = objDoc.DocumentElement.ChildNodes[i].Attributes["Unit"].Value;
                    objOtherValue.m_fltOtherValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objOtherValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddOther(objOtherValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objOtherValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureOtherValue objOtherValue = new clsThreeMeasureOtherValue();
                        objOtherValue.m_dtmOtherDate = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objOtherValue.m_strOtherItem = objDoc.DocumentElement.ChildNodes[i].Attributes["Item"].Value;
                        objOtherValue.m_strOtherUnit = objDoc.DocumentElement.ChildNodes[i].Attributes["Unit"].Value;
                        objOtherValue.m_fltOtherValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objOtherValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddOther(objOtherValue);
                    }
                }
            }
            #endregion

            #region PulseXml
            objDoc.LoadXml(p_objXmlValue.m_strPulseXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasurePulseValue objPulseValue = new clsThreeMeasurePulseValue();
                    objPulseValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
                    objPulseValue.m_dtmValueTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objPulseValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime), objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
                    objPulseValue.m_enmType = (enmThreeMeasurePulseType)Enum.Parse(typeof(enmThreeMeasurePulseType), objDoc.DocumentElement.ChildNodes[i].Attributes["Type"].Value);
                    objPulseValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objPulseValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddPulseValue(objPulseValue);

                    if (blnIsDelete)
                    {
                        clsThreeMeasureDeleteInfo objDeleteInfo = new clsThreeMeasureDeleteInfo();
                        objDeleteInfo.m_dtmDeleteTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteTime"].Value);
                        objDeleteInfo.m_strUserID = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserID"].Value;
                        objDeleteInfo.m_strUserName = objDoc.DocumentElement.ChildNodes[i].Attributes["DeleteUserName"].Value;

                        objPulseValue.m_objDeleteInfo = objDeleteInfo;
                    }
                }
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasurePulseValue objPulseValue = new clsThreeMeasurePulseValue();
                        objPulseValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
                        objPulseValue.m_dtmValueTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objPulseValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime), objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
                        objPulseValue.m_enmType = (enmThreeMeasurePulseType)Enum.Parse(typeof(enmThreeMeasurePulseType), objDoc.DocumentElement.ChildNodes[i].Attributes["Type"].Value);
                        objPulseValue.m_intValue = int.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objPulseValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddPulseValue(objPulseValue);
                    }
                }
            }
            #endregion

            #region TemperatureXml
            objDoc.LoadXml(p_objXmlValue.m_strTemperatureXml);
            for (int i = 0; i < objDoc.DocumentElement.ChildNodes.Count; i++)
            {
                bool blnIsDelete = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["IsDelete"].Value);

                if (p_blnIsPrintDel)
                {
                    clsThreeMeasureTemperatureValue objTemperatureValue = new clsThreeMeasureTemperatureValue();

                    objTemperatureValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
                    objTemperatureValue.m_dtmValueTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                    objTemperatureValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime), objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
                    objTemperatureValue.m_enmType = (enmThreeMeasureTemperatureType)Enum.Parse(typeof(enmThreeMeasureTemperatureType), objDoc.DocumentElement.ChildNodes[i].Attributes["Type"].Value);
                    objTemperatureValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                    objTemperatureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                    m_blnAddTemperatureValue(objTemperatureValue);

                    if (blnIsDelete)
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
                        for (int j2 = 0; j2 < xndTempValue.ChildNodes.Count; j2++)
                        {
                            bool blnIsDeleteDown = bool.Parse(xndTempValue.ChildNodes[j2].Attributes["IsDelete"].Value);

                            clsThreeMeasureTemperaturePhyscalDownValue objDownValue = new clsThreeMeasureTemperaturePhyscalDownValue();
                            objDownValue.m_dtmValueTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["Time"].Value);
                            objDownValue.m_fltValue = float.Parse(xndTempValue.ChildNodes[j2].Attributes["Value"].Value);
                            objDownValue.m_dtmModifyTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["ModifyTime"].Value);

                            m_blnAddPhyscalDownValue(objDownValue, objTemperatureValue);

                            if (blnIsDeleteDown)
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
                else
                {
                    if (!blnIsDelete)
                    {
                        clsThreeMeasureTemperatureValue objTemperatureValue = new clsThreeMeasureTemperatureValue();

                        objTemperatureValue.m_blnLineToPreValue = bool.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["LineToPreValue"].Value);
                        objTemperatureValue.m_dtmValueTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Time"].Value);
                        objTemperatureValue.m_enmParamTime = (enmParamTime)Enum.Parse(typeof(enmParamTime), objDoc.DocumentElement.ChildNodes[i].Attributes["ParamTime"].Value);
                        objTemperatureValue.m_enmType = (enmThreeMeasureTemperatureType)Enum.Parse(typeof(enmThreeMeasureTemperatureType), objDoc.DocumentElement.ChildNodes[i].Attributes["Type"].Value);
                        objTemperatureValue.m_fltValue = float.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["Value"].Value);
                        objTemperatureValue.m_dtmModifyTime = DateTime.Parse(objDoc.DocumentElement.ChildNodes[i].Attributes["ModifyTime"].Value);

                        m_blnAddTemperatureValue(objTemperatureValue);

                        XmlNode xndTempValue = objDoc.DocumentElement.ChildNodes[i];
                        for (int j2 = 0; j2 < xndTempValue.ChildNodes.Count; j2++)
                        {
                            bool blnIsDeleteDown = bool.Parse(xndTempValue.ChildNodes[j2].Attributes["IsDelete"].Value);

                            clsThreeMeasureTemperaturePhyscalDownValue objDownValue = new clsThreeMeasureTemperaturePhyscalDownValue();
                            objDownValue.m_dtmValueTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["Time"].Value);
                            objDownValue.m_fltValue = float.Parse(xndTempValue.ChildNodes[j2].Attributes["Value"].Value);
                            objDownValue.m_dtmModifyTime = DateTime.Parse(xndTempValue.ChildNodes[j2].Attributes["ModifyTime"].Value);

                            m_blnAddPhyscalDownValue(objDownValue, objTemperatureValue);

                            if (blnIsDeleteDown)
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
            }
            #endregion

            return true;
        }

		#endregion		

        private void m_mthUpdateHeight(int p_intStartDateIndex, int p_intRecordLength, Graphics p_objGrap)
		{
			//Breath
			int intMaxBreathExt = 0;
            //Pressure
            int intMaxPressureExt = 0;
			//Input
            int intMaxInputExt = 0;
            //Dejecta
            int intMaxDejectaExt = 0;
            //Pee
            int intMaxPeeExt = 0;
            //OutStream
            int intMaxOutStreamExt = 0;
            //Weight
            int intMaxWeightExt = 0;
            //SkinTest
            int intMaxSkinTestExt = 0;
            //Other
            int intMaxOtherExt = 0;
            Font fntOtherText = new Font("����", c_flt9PointFontSize);
			for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
			{
				clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
                #region Breath
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
                #endregion Breath
                #region Pressure
                int intDeletePressure = 0;

                for (int j2 = 0 ; j2 < objRecord.m_arlPressureValue1.Count ; j2++)
                {
                    clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

                    if (objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objPressureValue.m_objDeleteInfo != null)
                    {
                        intDeletePressure++;
                    }
                }

                if (intMaxPressureExt < intDeletePressure)
                    intMaxPressureExt = intDeletePressure;
                #endregion  Pressure
                #region DeleteInput
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
                #endregion DeleteInput
                #region Dejecta
                int intDeleteDejecta = 0;

                for (int j2 = 0 ; j2 < objRecord.m_arlDejectaValue.Count ; j2++)
                {
                    clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];

                    if (objDejectaValue.m_objDeleteInfo != null && objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objDejectaValue.m_objDeleteInfo != null)
                    {
                        intDeleteDejecta++;
                    }
                }

                if (intMaxDejectaExt < intDeleteDejecta)
                    intMaxDejectaExt = intDeleteDejecta;
                #endregion Dejecta
                #region Pee
                int intDeletePee = 0;

                for (int j2 = 0 ; j2 < objRecord.m_arlPeeValue.Count ; j2++)
                {
                    clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

                    if (objPeeValue.m_objDeleteInfo != null && objPeeValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objPeeValue.m_objDeleteInfo != null)
                    {
                        intDeletePee++;
                    }
                }

                if (intMaxPeeExt < intDeletePee)
                    intMaxPeeExt = intDeletePee;
                #endregion Pee
                #region OutStream
                int intDeleteOutStream = 0;

                for (int j2 = 0 ; j2 < objRecord.m_arlOutStreamValue.Count ; j2++)
                {
                    clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

                    if (objOutStreamValue.m_objDeleteInfo != null && objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objOutStreamValue.m_objDeleteInfo != null)
                    {
                        intDeleteOutStream++;
                    }
                }

                if (intMaxOutStreamExt < intDeleteOutStream)
                    intMaxOutStreamExt = intDeleteOutStream;
                #endregion OutStream
                #region Weight
                int intDeleteWeight = 0;

                for (int j2 = 0 ; j2 < objRecord.m_arlWeightValue.Count ; j2++)
                {
                    clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

                    if (objWeightValue.m_enmWeightType != enmThreeMeasureWeightType.һ��)
                    {
                        continue;
                    }

                    if (objWeightValue.m_objDeleteInfo != null && objWeightValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objWeightValue.m_objDeleteInfo != null)
                    {
                        intDeleteWeight++;
                    }
                }

                if (intMaxWeightExt < intDeleteWeight)
                    intMaxWeightExt = intDeleteWeight;
                #endregion Weight
                #region SkinTest
                int intDeleteSkinTest = 0;

                for (int j2 = 0 ; j2 < objRecord.m_arlSkinTestValue.Count ; j2++)
                {
                    clsThreeMeasureSkinTestValue objSkinTestValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

                    if (objSkinTestValue.m_intTimeIndex >= 0)
                    {
                        continue;
                    }

                    if (objSkinTestValue.m_objDeleteInfo != null && objSkinTestValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    intDeleteSkinTest++;
                }

                if (intMaxSkinTestExt < intDeleteSkinTest)
                    intMaxSkinTestExt = intDeleteSkinTest;
                #endregion SkinTest
                #region Other
                int intDeleteOther = 0;
                float fltOtherH = 0;
                for (int j2 = 0 ; j2 < objRecord.m_arlOtherValue.Count ; j2++)
                {
                    clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];

                    if (objOtherValue.m_objDeleteInfo != null && objOtherValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objOtherValue.m_objDeleteInfo != null)
                    {
                        intDeleteOther++;
                    }
                    else
                    { 
                        SizeF szfDesc = p_objGrap.MeasureString(objOtherValue.m_strOther, fntOtherText, c_intGridHeight * 6);
                        fltOtherH += szfDesc.Height + 2;
                    }
                }

                if (intMaxOtherExt < intDeleteOther)
                    intMaxOtherExt = intDeleteOther;
                if (c_intOtherHeight < (int)fltOtherH)
                    c_intOtherHeight = (int)fltOtherH;
                #endregion Other
            }
			c_intBreathHeight = intMaxBreathExt*14+28;
			c_intBreathTotalHeight = c_intGridTotalHeight+c_intBreathHeight;

            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}
            c_intPressureHeight = 28 + intMaxPressureExt * 21;
            c_intPressureTotalHeight = c_intBreathTotalHeight + c_intPressureHeight;
			c_intInputHeight = 28+intMaxInputExt*21;
            c_intInputTotalHeight = c_intPressureTotalHeight + c_intInputHeight;

            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}
			c_intDejectaHeight = 28+intMaxDejectaExt*21;
			c_intDejectaTotalHeight = c_intInputTotalHeight+c_intDejectaHeight;

            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}
			c_intPeeHeight = 28+intMaxPeeExt*21;
			c_intPeeTotalHeight = c_intDejectaTotalHeight+c_intPeeHeight;

            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}
			c_intOutStreamHeight = 28+intMaxOutStreamExt*21;
			c_intOutStreamTotalHeight = c_intPeeTotalHeight+c_intOutStreamHeight;


            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}


            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}
			c_intWeightHeight = 28+intMaxWeightExt*21;
            c_intWeightTotalHeight = c_intOutStreamTotalHeight + c_intWeightHeight;


            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
            //}
			if(intMaxSkinTestExt > 0)
				intMaxSkinTestExt -= 1;
			c_intSkinTestHeight = 28+intMaxSkinTestExt*21;
			c_intSkinTestTotalHeight = c_intWeightTotalHeight+c_intSkinTestHeight;

            //for(int i=p_intStartDateIndex;i<m_objDateManager.m_IntRecordCount && i-p_intStartDateIndex < p_intRecordLength;i++)
            //{
            //    clsThreeMeasureDateRecord objRecord = m_objDateManager[i];
			
				
            //}
			c_intOtherHeight += intMaxOtherExt*21;
			m_intTotalHeight = c_intSkinTestTotalHeight+c_intOtherHeight;
            fntOtherText.Dispose();
		}

        private bool m_blnIsNearType(enmThreeMeasureEventType p_enmTMEventType, enmThreeMeasureEventType p_enmCurrentTMEventType)
        {
            bool blnTMType = p_enmTMEventType == enmThreeMeasureEventType.��Ժ || 
                p_enmTMEventType == enmThreeMeasureEventType.���� || 
                p_enmTMEventType == enmThreeMeasureEventType.��Ժ || 
                p_enmTMEventType == enmThreeMeasureEventType.���� || 
                p_enmTMEventType == enmThreeMeasureEventType.���� ||
                p_enmTMEventType == enmThreeMeasureEventType.ת�� ||
                p_enmTMEventType == enmThreeMeasureEventType.���� ||
                p_enmTMEventType == enmThreeMeasureEventType.ת��;
            bool blnCurrentType = p_enmCurrentTMEventType == enmThreeMeasureEventType.��Ժ || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.���� || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.��Ժ || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.���� || 
                p_enmCurrentTMEventType == enmThreeMeasureEventType.���� ||
                p_enmCurrentTMEventType == enmThreeMeasureEventType.ת�� ||
                p_enmCurrentTMEventType == enmThreeMeasureEventType.���� ||
                p_enmCurrentTMEventType == enmThreeMeasureEventType.ת��;
            return blnTMType == blnCurrentType;
        }
		/// <summary>
		/// ��ӡ�����
		/// </summary>
		/// <param name="p_intStartX">��ʼ��X����</param>
		/// <param name="p_intStartY">��ʼ��Y����</param>
		/// <param name="p_intMaxHeight">���ĸ߶�</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intStartDateIndex">��ʼ��ӡ������</param>
		/// <param name="p_intRecordLength">��ӡ��������Ŀ</param>
		/// <param name="p_blnHasMoreDate">�����Ƿ�������û�д�ӡ</param>
		/// <param name="p_intEndY">��������Y����</param>
		/// <param name="p_intLeftItem">������߶Ȳ����û��ӡ������</param>
		public void m_mthPrintRecord(int p_intStartX,int p_intStartY,int p_intMaxHeight,System.Drawing.Printing.PrintPageEventArgs e,int p_intStartDateIndex,int p_intRecordLength,out bool p_blnHasMoreDate,out int p_intEndY,out int p_intLeftItem)
		{
			m_mthUpdateHeight(p_intStartDateIndex,p_intRecordLength,e.Graphics);

			int intStartPrintX = p_intStartX;
			int intStartPrintY = p_intStartY;

			int intDiffWidth = -43;

			int intTextWidth = c_intTextTotleWidth + intDiffWidth;

			int intRecordHeight = m_intTotalHeight;
			p_intLeftItem = int.MaxValue;

			#region Check Height
			if(intRecordHeight > p_intMaxHeight)
			{
				p_intLeftItem = 8;
				intRecordHeight = c_intSkinTestTotalHeight;

				if(intRecordHeight > p_intMaxHeight)
				{
					p_intLeftItem = 7;
					intRecordHeight = c_intWeightTotalHeight;
			
					if(intRecordHeight > p_intMaxHeight)
					{
						p_intLeftItem = 6;
						intRecordHeight = c_intPressureTotalHeight;
			
						if(intRecordHeight > p_intMaxHeight)
						{
							p_intLeftItem = 5;
							intRecordHeight = c_intOutStreamTotalHeight;
			
							if(intRecordHeight > p_intMaxHeight)
							{
								p_intLeftItem = 4;
								intRecordHeight = c_intPeeTotalHeight;
			
								if(intRecordHeight > p_intMaxHeight)
								{
									p_intLeftItem = 3;
									intRecordHeight = c_intDejectaTotalHeight;
			
									if(intRecordHeight > p_intMaxHeight)
									{
										p_intLeftItem = 2;
										intRecordHeight = c_intInputTotalHeight;
			
										if(intRecordHeight > p_intMaxHeight)
										{
											p_intLeftItem = 1;
											intRecordHeight = c_intBreathTotalHeight;
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
			
			Font fntRecordDateText = new Font("",c_flt7PointFontSize);
			Font fntSpecialDateText = new Font("",c_flt5PointFontSize);
			Font fntGridText = new Font("",c_flt7PointFontSize);
			Font fntSymbol = new Font("",c_flt9PointFontSize);
			Font fnt6PtText = new Font("",c_flt6PointFontSize);
			
			SolidBrush bruTemp = new SolidBrush(Color.Black);

			Color clrPenTemp;
			Color clrBrushTemp;
            int m_IntTotalWeek = m_objDateManager.m_IntRecordCount / 7 + 1;
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

                    int intDateIndex = m * p_intRecordLength;
                    for (int i = intDateIndex; i < m_objDateManager.m_IntRecordCount && i - intDateIndex < p_intRecordLength; i++)
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
			//���߿�
			e.Graphics.DrawRectangle(penOneWidthLine,0+intStartPrintX,0+intStartPrintY,intRecordWidth,intRecordHeight);

			//��������ֵ�ķָ���
			penOneWidthLine.Color = Color.Black;
			e.Graphics.DrawLine(penOneWidthLine,intTextWidth+intStartPrintX,1+intStartPrintY,intTextWidth+intStartPrintX,intRecordHeight+intStartPrintY);		
			
			//���������ֺ�������ķָ���
			e.Graphics.DrawString("��  ��  ",fntRecordDateText,bruTemp,20+intStartPrintX,1+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intRecordDateHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intRecordDateHeight+intStartPrintY);

            ////��סԺ���ں�������ķָ���
            Font fntInpateintDateText = new Font("", c_flt10PointFontSize);
            e.Graphics.DrawString(" סԺ���� ", fntRecordDateText, bruTemp, 4 + intStartPrintX, c_intRecordDateHeight + 2 + intStartPrintY);
            e.Graphics.DrawLine(penOneWidthLine, 1 + intStartPrintX, c_intInpateintTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intInpateintTotalHeight + intStartPrintY);

			//�������������ֺ�������ķָ���
            e.Graphics.DrawString("�������������", fntSpecialDateText, bruTemp, 4 + intStartPrintX, c_intInpateintTotalHeight + 3 + intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY);

			//��ʱ��
			e.Graphics.DrawString("ʱ  ��  ",fntRecordDateText,bruTemp,20+intStartPrintX,c_intSpecialDateTotalHeight+4+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intTimeTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intTimeTotalHeight+intStartPrintY);

			#region ��������ʱ��
			//���������ߺ�ʱ��
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
				e.Graphics.DrawString("8",fntGridText,bruTemp,intTextWidth+c_intGridHeight+i*c_intGridHeight*6+2+intStartPrintX,intNormalGridHeight+intStartPrintY);			

				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*3+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*3+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("12",fntGridText,bruTemp,intTextWidth+c_intGridHeight*2+i*c_intGridHeight*6-2+intStartPrintX,intNormalGridHeight+intStartPrintY);			
				
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*4+i*c_intGridHeight*6+intStartPrintX,c_intSpecialDateTotalHeight+intStartPrintY,intTextWidth+c_intGridHeight*4+i*c_intGridHeight*6+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);
				e.Graphics.DrawString("4",fntGridText,bruTemp,intTextWidth+c_intGridHeight*3+i*c_intGridHeight*6+2+intStartPrintX,intNormalGridHeight+intStartPrintY);			

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

                e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * 6 + i * c_intGridHeight * 6 + intStartPrintX, c_intGridTotalHeight + intStartPrintY, intTextWidth + c_intGridHeight * 6 + i * c_intGridHeight * 6 + intStartPrintX, intRecordHeight+ intStartPrintY);
			}

			//���������
			int intStartX = intTextWidth+1;
			int intEndX = intRecordWidth-2;
			
			penOneWidthLine.Color = Color.Black;
			for(int i=1;i<=24;i++)
			{
				e.Graphics.DrawLine(penOneWidthLine,intStartX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY);
			}

			e.Graphics.DrawLine(penTwoWidthLine,intStartX+intStartPrintX,c_intTimeTotalHeight+25*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+25*c_intGridHeight+intStartPrintY);

			for(int i=26;i<c_intGridHeightCount;i++)
			{
				e.Graphics.DrawLine(penOneWidthLine,intStartX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY,intEndX+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight+intStartPrintY);
			}

			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intTimeTotalHeight+c_intGridHeightCount*c_intGridHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intTimeTotalHeight+c_intGridHeightCount*c_intGridHeight+intStartPrintY);
			#endregion

			#region ������������Ϣ
			//����������ֵ
			int intParamCount = c_intGridHeightCount/5;					
			for(int i=1;i<intParamCount;i++)
			{
				//���������ʣ�
				int intPulse = 180-i*20;
				bruTemp.Color = Color.Red;
				e.Graphics.DrawString(intPulse.ToString(),fntRecordDateText,bruTemp,intTextWidth-intTextWidth/3-25+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2+intStartPrintY);

				//�¶�
				int intTemp = 42-i;
				bruTemp.Color = Color.Black;
				e.Graphics.DrawString(intTemp.ToString("0��"),fntRecordDateText,bruTemp,intTextWidth-20+intStartPrintX,c_intTimeTotalHeight+i*c_intGridHeight*5-c_intGridHeight/2+intStartPrintY);
			}

			//����������
			//���������ʣ�
			bruTemp.Color = Color.Red;
            e.Graphics.DrawString("����", fntRecordDateText, bruTemp, intTextWidth - intTextWidth / 3 - 35 + intStartPrintX, c_intTimeTotalHeight + 1 + intStartPrintY);
            e.Graphics.DrawString("(��/��)", fntRecordDateText, bruTemp, intTextWidth - intTextWidth / 3 - 40 + intStartPrintX, c_intTimeTotalHeight + 18 + intStartPrintY);
			
			//����
			bruTemp.Color = Color.Black;
            e.Graphics.DrawString("����", fntRecordDateText, bruTemp, intTextWidth - 25 + intStartPrintX, c_intTimeTotalHeight + 1 + intStartPrintY);
            e.Graphics.DrawString("  ��", fntRecordDateText, bruTemp, intTextWidth - 20 + intStartPrintX, c_intTimeTotalHeight + 18 + intStartPrintY);			
			#endregion			

			#region ����ֵͼ��
			bruTemp.Color = Color.Black;

			e.Graphics.DrawString("�ڱ�",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+15+intStartPrintY);
			e.Graphics.DrawImage(m_imgMouthTemperature,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+16+intStartPrintY,c_intGridHeight,c_intGridHeight);
			e.Graphics.DrawString("Ҹ��",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+19+c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgArmpitTemperature,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+21+c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			e.Graphics.DrawString("�ر�",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+23+2*c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgAnusTemperature,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-2)*c_intGridHeight*5-c_intGridHeight/2+25+2*c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			
			e.Graphics.DrawString("����",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+15+intStartPrintY);
			e.Graphics.DrawImage(m_imgPulse,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+16+intStartPrintY,c_intGridHeight,c_intGridHeight);
			e.Graphics.DrawString("����",fntSymbol,bruTemp,4+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+19+c_intGridHeight+intStartPrintY);
			e.Graphics.DrawImage(m_imgHeartRate,35+intStartPrintX,c_intTimeTotalHeight+(intParamCount-1)*c_intGridHeight*5-c_intGridHeight/2+21+c_intGridHeight+intStartPrintY,c_intGridHeight,c_intGridHeight);
			#endregion

			//������
			bruTemp.Color = Color.Black  ;
			e.Graphics.DrawString("����(��/��)",fntRecordDateText,bruTemp,6+intStartPrintX,c_intGridTotalHeight+4+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intBreathTotalHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intBreathTotalHeight+intStartPrintY);

			penOneWidthLine.Color = Color.Black;
			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
            if (p_intLeftItem > 1)
            {
                //��Ѫѹ
                e.Graphics.DrawString("Ѫѹ(mmHg)", fntRecordDateText, bruTemp, 6 + intStartPrintX, c_intBreathTotalHeight + 4 + intStartPrintY);
                e.Graphics.DrawLine(penOneWidthLine, 1 + intStartPrintX, c_intPressureTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intPressureTotalHeight + intStartPrintY);

                if (p_intLeftItem > 2)
                {
                    //������Һ��
                    e.Graphics.DrawString("����Һ��(ml)", fntRecordDateText, bruTemp, 1 + intStartPrintX, c_intPressureTotalHeight + 4 + intStartPrintY);
                    e.Graphics.DrawLine(penOneWidthLine, 1 + intStartPrintX, c_intInputTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intInputTotalHeight + intStartPrintY);

                    if (p_intLeftItem > 3)
                    {
                        //���ų���
                        e.Graphics.DrawString("�ų���", fntRecordDateText, bruTemp, 1 + intStartPrintX, c_intInputTotalHeight + 6 + intStartPrintY, stfDirectionVertical);
                        e.Graphics.DrawLine(penOneWidthLine, 16 + intStartPrintX, c_intInputTotalHeight + intStartPrintY, 16 + intStartPrintX, c_intOutStreamTotalHeight + intStartPrintY);

                        e.Graphics.DrawString("���(��)", fntRecordDateText, bruTemp, 25 + intStartPrintX, c_intInputTotalHeight + 4 + intStartPrintY);
                        e.Graphics.DrawLine(penOneWidthLine, 16 + intStartPrintX, c_intDejectaTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intDejectaTotalHeight + intStartPrintY);

                        if (p_intLeftItem > 4)
                        {
                            e.Graphics.DrawString("����(ml)", fntRecordDateText, bruTemp, 25 + intStartPrintX, c_intDejectaTotalHeight + 4 + intStartPrintY);
                            e.Graphics.DrawLine(penOneWidthLine, 16 + intStartPrintX, c_intPeeTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intPeeTotalHeight + intStartPrintY);

                            if (p_intLeftItem > 5)
                            {
                                e.Graphics.DrawString("����", fntRecordDateText, bruTemp, 17 + intStartPrintX, c_intPeeTotalHeight + 4 + intStartPrintY);
                                e.Graphics.DrawLine(penOneWidthLine, 1 + intStartPrintX, c_intOutStreamTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intOutStreamTotalHeight + intStartPrintY);

                                if (p_intLeftItem > 6)
                                {
                                    //������
                                    e.Graphics.DrawString("����(kg)", fntRecordDateText, bruTemp, 6 + intStartPrintX, c_intOutStreamTotalHeight + 4 + intStartPrintY);
                                    e.Graphics.DrawLine(penOneWidthLine, 1 + intStartPrintX, c_intWeightTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intWeightTotalHeight + intStartPrintY);

                                    if (p_intLeftItem > 7)
                                    {
                                        //��Ƥ��
                                        e.Graphics.DrawString("Ƥ��", fntRecordDateText, bruTemp, 6 + intStartPrintX, c_intWeightTotalHeight + 4 + intStartPrintY);
                                        e.Graphics.DrawLine(penOneWidthLine, 1 + intStartPrintX, c_intSkinTestTotalHeight + intStartPrintY, intRecordWidth - 2 + intStartPrintX, c_intSkinTestTotalHeight + intStartPrintY);

                                        if (p_intLeftItem > 8)
                                        {
                                            //������
                                            //if(m_strOtherName == null)
                                            //{
                                            e.Graphics.DrawString("����", fnt6PtText, bruTemp, 1 + intStartPrintX, c_intSkinTestTotalHeight + 4 + intStartPrintY);
                                            //}
                                            //else
                                            //{
                                            //    e.Graphics.DrawString(m_strOtherName,fnt6PtText,bruTemp,1+intStartPrintX,c_intSkinTestTotalHeight+4+intStartPrintY);						
                                            //}
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
			
			#region ����ֵ
			//�����ڼ�������Ϣ
			bool blnIsUpBreath = true;
			Font fntBigSymbol = new Font("",c_flt14PointFontSize);
			DateTime dtmPreDateForDateRecord = DateTime.Today;			
			int intCurrentDateDiff = 0;
			int intPreWidth = p_intStartDateIndex*6*c_intGridHeight;
			intStartPrintX -= intPreWidth;
			int intLastIndex = 0;
            m_intWeekNum = p_intStartDateIndex / 7 + 1;
            if (m_IntTotalWeek >= m_intWeekNum)
            {//����ѡ���Ĳ���ʱ��������Խ��
                dtmPreDateForSpecialDate = m_strEndSpecialDate[m_intWeekNum - 1];
                intSpecialNewStartTimes = m_strEndSpecialTimes[m_intWeekNum - 1];
            }
            for (int i = p_intStartDateIndex; i < m_objDateManager.m_IntRecordCount && i - p_intStartDateIndex < p_intRecordLength; i++)
            {
                intLastIndex = i;
                clsThreeMeasureDateRecord objRecord = m_objDateManager[i];

                bruTemp.Color = Color.Black;
                //������
                string strRecordDate = "";
                if (i % 7 == 0 || dtmPreDateForDateRecord.Year != objRecord.m_dtmRecordDate.Year)
                {
                    strRecordDate = objRecord.m_dtmRecordDate.ToString("yy-M-d");
                }
                else
                {
                    if (dtmPreDateForDateRecord.Month != objRecord.m_dtmRecordDate.Month)
                    {
                        strRecordDate = objRecord.m_dtmRecordDate.ToString("   M-d");
                    }
                    else
                    {
                        strRecordDate = objRecord.m_dtmRecordDate.ToString("     %d");
                    }
                }
                dtmPreDateForDateRecord = objRecord.m_dtmRecordDate;

                e.Graphics.DrawString(strRecordDate, fntSpecialDateText, bruTemp, intTextWidth + i * c_intGridHeight * 6 + 4 + intStartPrintX, 1 + intStartPrintY);

                //��סԺ����
                string strInpateintNum = "";
                DateTime dtmCurrDate = DateTime.Parse(objRecord.m_dtmRecordDate.ToString("yyyy-MM-dd 00:00:00"));
                DateTime dtmInDate = DateTime.Parse(m_dtmInPatientDate.ToString("yyyy-MM-dd 00:00:00"));
                System.TimeSpan diff = dtmCurrDate.Subtract(dtmInDate);
                strInpateintNum = ((int)diff.TotalDays + 1).ToString();
                e.Graphics.DrawString("     " + strInpateintNum, fntSpecialDateText, bruTemp, intTextWidth + i * c_intGridHeight * 6 + 4 + intStartPrintX, c_intRecordDateHeight + 1 + intStartPrintY);

                bruTemp.Color = Color.Red;
                //���������������
                if (objRecord.m_objSpecialDate != null)
                {
                    if (objRecord.m_objSpecialDate.m_blnIsNewStart)
                    {
                        //�µ���������
                        intCurrentDateDiff = 0;
                        dtmPreDateForSpecialDate = objRecord.m_dtmRecordDate;
                        intSpecialNewStartTimes++;
                        string strSpecialDataText = null;

                        if (intSpecialNewStartTimes == 1)
                        {
                            //��һ������������ʾ��������
                            strSpecialDataText = intCurrentDateDiff.ToString();
                        }
                        else
                        {
                            strSpecialDataText = m_strGetSpecialDateTimes(intSpecialNewStartTimes) + intCurrentDateDiff.ToString();
                        }

                        e.Graphics.DrawString(strSpecialDataText, fntSpecialDateText, bruTemp, intTextWidth + i * c_intGridHeight * 6 + c_intGridHeight * 2 + 4 + intStartPrintX, c_intInpateintTotalHeight + 1 + intStartPrintY);
                    }
                    else if (intSpecialNewStartTimes > 0)
                    {
                        //�ɵ���������
                        intCurrentDateDiff = ((TimeSpan)(objRecord.m_dtmRecordDate - dtmPreDateForSpecialDate)).Days;

                        if (intCurrentDateDiff < 11)
                        {
                            string strSpecialDataText = null;
                            //������10���ڣ�������ʾ��������
                            if (intSpecialNewStartTimes == 1)
                            {
                                //��һ������������ʾ��������
                                strSpecialDataText = intCurrentDateDiff.ToString();
                            }
                            else
                            {
                                strSpecialDataText = m_strGetSpecialDateTimes(intSpecialNewStartTimes) + intCurrentDateDiff.ToString();
                            }

                            e.Graphics.DrawString(strSpecialDataText, fntSpecialDateText, bruTemp, intTextWidth + i * c_intGridHeight * 6 + c_intGridHeight * 2 + 4 + intStartPrintX, c_intInpateintTotalHeight + 1 + intStartPrintY);
                        }

                    }
                    //else û����������
                }

                #region ���¼�
                bruTemp.Color = Color.Red;
                penOneWidthLine.Color = Color.Red;
                int intPreTimeIndex = -1;
                int intActureIndex = 0;
                for (int j2 = 0; j2 < objRecord.m_arlEvent.Count; j2++)
                {
                    clsThreeMeasureEvent objEvent = (clsThreeMeasureEvent)objRecord.m_arlEvent[j2];

                    if (objEvent.m_objDeleteInfo != null && objEvent.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }
                    intActureIndex = objEvent.m_intNearTimeIndex;
                    intPreTimeIndex = objEvent.m_intTimeIndex;
                    if (objEvent.m_enmEventType == enmThreeMeasureEventType.��� || 
                        objEvent.m_enmEventType == enmThreeMeasureEventType.���� ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.ͣ���� ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.��� ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.�ܲ� || 
                        objEvent.m_enmEventType == enmThreeMeasureEventType.ͣ����ձ ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.�ƾ���ԡ ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.�Ϻ����� ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.ͣ������ ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.��ˮ��ԡ ||
                        objEvent.m_enmEventType == enmThreeMeasureEventType.����ձ)
                    {
                        bruTemp.Color = Color.Blue;
                        e.Graphics.DrawString(objEvent.m_enmEventType.ToString(), fntSymbol, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) - 1 + intStartPrintX, c_intLowEventTextStartHeight  + intStartPrintY, stfDirectionVertical);
                    }
                    else
                    {
                        bruTemp.Color = Color.Red;
                        e.Graphics.DrawString(objEvent.m_enmEventType.ToString(), fntSymbol, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) - 1 + intStartPrintX, c_intTimeTotalHeight - 1 + intStartPrintY, stfDirectionVertical);
                        if (objEvent.m_strTime != "")
                        {
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) + c_intGridHeight / 2 + intStartPrintX, c_intTimeTotalHeight + 2 * c_intGridHeight + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) + c_intGridHeight / 2 + intStartPrintX, c_intTimeTotalHeight + 3 * c_intGridHeight + intStartPrintY);
                            for (int k3 = 0; k3 < objEvent.m_strTime.Length; k3++)
                            {
                                e.Graphics.DrawString(objEvent.m_strTime[k3].ToString(), fntSymbol, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) - 1 + intStartPrintX, c_intTimeTotalHeight + (3 + k3) * c_intGridHeight - 1 + intStartPrintY);
                            }
                        }
                    }

                    if (objEvent.m_objDeleteInfo != null)
                    {
                        int intLen = 3 + objEvent.m_strTime.Length;

                        penOneWidthLine.Color = m_clrDST;
                        e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) + c_intGridHeight / 2 + intStartPrintX, c_intTimeTotalHeight + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) + c_intGridHeight / 2 + intStartPrintX, c_intTimeTotalHeight + intLen * c_intGridHeight + intStartPrintY);
                        e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) + c_intGridHeight / 2 + 3 + intStartPrintX, c_intTimeTotalHeight + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + intActureIndex) + c_intGridHeight / 2 + 3 + intStartPrintX, c_intTimeTotalHeight + intLen * c_intGridHeight + intStartPrintY);
                        penOneWidthLine.Color = Color.Red;
                    }
                }
                #endregion

                bruTemp.Color = Color.Blue;
                int intCurrentBreathIndex = -1;
                int intDeleteBreath = 0;
                penOneWidthLine.Color = m_clrDST;
                byte bytBreathIndex = 0;
                #region ������
                for (int j2 = 0; j2 < objRecord.m_arlBreath.Count; j2++)
                {
                    clsThreeMeasureBreathValue objBreath = (clsThreeMeasureBreathValue)objRecord.m_arlBreath[j2];

                    if (objBreath.m_objDeleteInfo != null && objBreath.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objBreath.m_intTimeIndex != intCurrentBreathIndex)
                    {
                        intCurrentBreathIndex = objBreath.m_intTimeIndex;
                        intDeleteBreath = 0;
                    }

                    if (objBreath.m_enmBreathType == enmThreeMeasureBreathType.һ��)
                    {
                        int intHeight = 0;

                        if (intDeleteBreath == 0)
                        {
                            if (blnIsUpBreath)
                            {
                                intHeight = c_intGridTotalHeight;
                            }
                            else
                            {
                                intHeight = c_intGridTotalHeight + 14;
                            }

                            blnIsUpBreath = !blnIsUpBreath;
                        }
                        else
                        {
                            intHeight = c_intGridTotalHeight + 14 + 14 * intDeleteBreath;
                        }

                        e.Graphics.DrawString(objBreath.m_intValue.ToString(), fntSymbol, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + objBreath.m_intTimeIndex) - 2 + intStartPrintX, intHeight + intStartPrintY);

                        if (objBreath.m_objDeleteInfo != null)
                        {
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + objBreath.m_intTimeIndex) + intStartPrintX, intHeight + 6 + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + objBreath.m_intTimeIndex + 1) + intStartPrintX, intHeight + 6 + intStartPrintY);
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + objBreath.m_intTimeIndex) + intStartPrintX, intHeight + 9 + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + objBreath.m_intTimeIndex + 1) + intStartPrintX, intHeight + 9 + intStartPrintY);

                            intDeleteBreath++;
                        }
                    }
                    else
                    {
                        //Modify 
                        bruTemp.Color = Color.Blue;
                        string strType = objBreath.m_enmBreathType.ToString();
                        for (int k3 = 0; k3 < strType.Length; k3++)
                        {
                            e.Graphics.DrawString(strType[k3].ToString(), fntSymbol, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + objBreath.m_intTimeIndex) - 1 + intStartPrintX, c_intLowEventTextStartHeight + c_intGridHeight * k3 - 1 + intStartPrintY);
                        }

                        bytBreathIndex += (byte)(1 << objBreath.m_intTimeIndex);
                        //						bruTemp.Color = m_clrDateValue;
                    }
                }
                #endregion

                //��Ƥ��(ֻ��PPD)	
                penOneWidthLine.Color = m_clrDST;
                for (int j2 = 0; j2 < objRecord.m_arlSkinTestValue.Count; j2++)
                {
                    clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

                    if (objSkinValue.m_objDeleteInfo != null && objSkinValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                    {
                        continue;
                    }

                    if (objSkinValue.m_intTimeIndex >= 0)
                    {
                        int intSkinTextY = c_intLowEventTextStartHeight;
                        if ((bytBreathIndex & (1 << objSkinValue.m_intTimeIndex)) != 0)
                        {
                            intSkinTextY += 6 * c_intGridHeight;
                        }
                        //ppd					
                        SizeF szfName = e.Graphics.MeasureString(objSkinValue.m_strMedicineName, fntSpecialDateText, 10, stfDirectionVertical);

                        string strValue = null;
                        bruTemp.Color = Color.Blue;
                        e.Graphics.DrawString(objSkinValue.m_strMedicineName, fntSpecialDateText, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) - 4 + intStartPrintX, intSkinTextY + intStartPrintY, stfDirectionVertical);
                        if (objSkinValue.m_blnIsBad)
                        {
                            strValue = objSkinValue.m_strPDDValue;

                            bruTemp.Color = Color.Red;
                            e.Graphics.DrawString(strValue, fntSpecialDateText, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) - 2 + intStartPrintX, intSkinTextY + (int)szfName.Height + intStartPrintY, stfDirectionVertical);
                        }
                        else
                        {
                            strValue = "(-)";
                            bruTemp.Color = Color.Blue;
                            e.Graphics.DrawString(strValue, fntSpecialDateText, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) - 2 + intStartPrintX, intSkinTextY + (int)szfName.Height + intStartPrintY, stfDirectionVertical);
                        }

                        if (objSkinValue.m_objDeleteInfo != null)
                        {
                            SizeF szfDesc = e.Graphics.MeasureString(objSkinValue.m_strMedicineName + strValue, fntRecordDateText, 10, stfDirectionVertical);
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) + c_intGridHeight / 2 + intStartPrintX, intSkinTextY + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) + c_intGridHeight / 2 + intStartPrintX, intSkinTextY + szfDesc.Height + intStartPrintY);
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) + c_intGridHeight / 2 + 3 + intStartPrintX, intSkinTextY + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + objSkinValue.m_intTimeIndex) + c_intGridHeight / 2 + 3 + intStartPrintX, intSkinTextY + szfDesc.Height + intStartPrintY);
                        }
                    }
                }

                if (p_intLeftItem > 1)
                {
                    //��Ѫѹ
                    penOneWidthLine.Color = m_clrDST;
                    float fltPressureTotalHeight = 0;
                    for (int j2 = 0; j2 < objRecord.m_arlPressureValue1.Count; j2++)
                    {
                        clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

                        if (objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                        {
                            continue;
                        }

                        string strDesc = objPressureValue.m_fltSystolicValue.ToString("0") + "/" + objPressureValue.m_fltDiastolicValue.ToString("0");
                        e.Graphics.DrawString(strDesc, fntGridText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 5 + intStartPrintY);

                        SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntGridText);

                        if (objPressureValue.m_objDeleteInfo != null)
                        {
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11 + intStartPrintY);
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13 + intStartPrintY);
                        }

                        fltPressureTotalHeight += szfDesc.Height + 5;
                    }
                    fltPressureTotalHeight = 0;
                    for (int j2 = 0; j2 < objRecord.m_arlPressureValue2.Count; j2++)
                    {
                        clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue2[j2];

                        if (objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                        {
                            continue;
                        }

                        string strDesc = objPressureValue.m_fltSystolicValue.ToString("0") + "/" + objPressureValue.m_fltDiastolicValue.ToString("0");
                        e.Graphics.DrawString(strDesc, fntGridText, bruTemp, intTextWidth + c_intGridHeight * (i * 6 + 3) + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 5 + intStartPrintY);

                        SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntGridText);

                        if (objPressureValue.m_objDeleteInfo != null)
                        {
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + 3) + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11 + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + 3) + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 11 + intStartPrintY);
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * (i * 6 + 3) + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * (i * 6 + 3) + (int)szfDesc.Width + intStartPrintX, c_intBreathTotalHeight + (int)fltPressureTotalHeight + 13 + intStartPrintY);
                        }

                        fltPressureTotalHeight += szfDesc.Height + 5;
                    }

                    if (p_intLeftItem > 2)
                    {
                        //������Һ��
                        bruTemp.Color = Color.Blue;
                        penOneWidthLine.Color = m_clrDST;
                        float fltInputTotalHeight = 0;
                        for (int j2 = 0; j2 < objRecord.m_arlInputValue.Count; j2++)
                        {
                            clsThreeMeasureInputValue objInputValue = (clsThreeMeasureInputValue)objRecord.m_arlInputValue[j2];

                            if (objInputValue.m_objDeleteInfo != null && objInputValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                            {
                                continue;
                            }

                            string strDesc = objInputValue.m_fltValue.ToString("0.00");
                            e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 2 + intStartPrintY);

                            SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntRecordDateText);

                            if (objInputValue.m_objDeleteInfo != null)
                            {
                                e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 10 + intStartPrintY);
                                e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intPressureTotalHeight + (int)fltInputTotalHeight + 13 + intStartPrintY);
                            }

                            fltInputTotalHeight += szfDesc.Height;
                        }


                        if (p_intLeftItem > 3)
                        {//�����
                            penOneWidthLine.Color = m_clrDST;
                            float fltDejectaTotalHeight = 0;
                            for (int j2 = 0; j2 < objRecord.m_arlDejectaValue.Count; j2++)
                            {
                                clsThreeMeasureDejectaValue objDejectaValue = (clsThreeMeasureDejectaValue)objRecord.m_arlDejectaValue[j2];

                                if (objDejectaValue.m_objDeleteInfo != null && objDejectaValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                                {
                                    continue;
                                }

                                string strDesc = objDejectaValue.m_strDesc;
                                e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intInputTotalHeight + (int)fltDejectaTotalHeight + 2 + intStartPrintY);

                                SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntRecordDateText);

                                if (objDejectaValue.m_objDeleteInfo != null)
                                {
                                    e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intInputTotalHeight + (int)fltDejectaTotalHeight + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intInputTotalHeight + (int)fltDejectaTotalHeight + 10 + intStartPrintY);
                                    e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intInputTotalHeight + (int)fltDejectaTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intInputTotalHeight + (int)fltDejectaTotalHeight + 13 + intStartPrintY);
                                }

                                fltDejectaTotalHeight += szfDesc.Height;
                            }

                            if (p_intLeftItem > 4)
                            {
                                //������
                                penOneWidthLine.Color = m_clrDST;
                                float fltPeeTotalHeight = 0;
                                for (int j2 = 0; j2 < objRecord.m_arlPeeValue.Count; j2++)
                                {
                                    clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

                                    if (objPeeValue.m_objDeleteInfo != null && objPeeValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                                    {
                                        continue;
                                    }

                                    string strDesc = null;
                                    if (!objPeeValue.m_blnIsIrretention)
                                    {
                                        strDesc = objPeeValue.m_fltValue.ToString("0.00");
                                    }
                                    else
                                    {
                                        strDesc = "*";
                                    }

                                    e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intDejectaTotalHeight + (int)fltPeeTotalHeight + 2 + intStartPrintY);

                                    SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntRecordDateText);

                                    if (objPeeValue.m_objDeleteInfo != null)
                                    {
                                        e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intDejectaTotalHeight + (int)fltPeeTotalHeight + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intDejectaTotalHeight + (int)fltPeeTotalHeight + 10 + intStartPrintY);
                                        e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intDejectaTotalHeight + (int)fltPeeTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intDejectaTotalHeight + (int)fltPeeTotalHeight + 13 + intStartPrintY);
                                    }

                                    fltPeeTotalHeight += szfDesc.Height;
                                }


                                if (p_intLeftItem > 5)
                                {//��������
                                    penOneWidthLine.Color = m_clrDST;
                                    float fltOutStreamTotalHeight = 0;
                                    for (int j2 = 0; j2 < objRecord.m_arlOutStreamValue.Count; j2++)
                                    {
                                        clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

                                        if (objOutStreamValue.m_objDeleteInfo != null && objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                                        {
                                            continue;
                                        }

                                        string strDesc = objOutStreamValue.m_fltValue.ToString("0.00");
                                        e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPeeTotalHeight + (int)fltOutStreamTotalHeight + 2 + intStartPrintY);

                                        SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntRecordDateText);

                                        if (objOutStreamValue.m_objDeleteInfo != null)
                                        {
                                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPeeTotalHeight + (int)fltOutStreamTotalHeight + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intPeeTotalHeight + (int)fltOutStreamTotalHeight + 10 + intStartPrintY);
                                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intPeeTotalHeight + (int)fltOutStreamTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intPeeTotalHeight + (int)fltOutStreamTotalHeight + 13 + intStartPrintY);
                                        }

                                        fltOutStreamTotalHeight += szfDesc.Height;
                                    }

                                    if (p_intLeftItem > 6)
                                    {
                                        //������
                                        penOneWidthLine.Color = m_clrDST;
                                        float fltWeightTotalHeight = 0;
                                        for (int j2 = 0; j2 < objRecord.m_arlWeightValue.Count; j2++)
                                        {
                                            clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

                                            if (objWeightValue.m_objDeleteInfo != null && objWeightValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                                            {
                                                continue;
                                            }

                                            string strDesc = null;
                                            if (objWeightValue.m_enmWeightType == enmThreeMeasureWeightType.һ��)
                                            {
                                                strDesc = objWeightValue.m_fltValue.ToString("0.00");
                                                e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 2 + intStartPrintY);
                                            }
                                            else
                                            {
                                                strDesc = objWeightValue.m_enmWeightType.ToString();
                                                e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 2 + intStartPrintY);
                                            }


                                            SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntRecordDateText);

                                            if (objWeightValue.m_objDeleteInfo != null)
                                            {
                                                e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 10 + intStartPrintY);
                                                e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, c_intOutStreamTotalHeight + (int)fltWeightTotalHeight + 13 + intStartPrintY);
                                            }

                                            fltWeightTotalHeight += szfDesc.Height;
                                        }

                                        if (p_intLeftItem > 7)
                                        {
                                            //��Ƥ��(��ֻ��PPD)		
                                            int intSkinTestNotPPDCount = 0;
                                            penOneWidthLine.Color = m_clrDST;
                                            for (int j2 = 0; j2 < objRecord.m_arlSkinTestValue.Count; j2++)
                                            {
                                                clsThreeMeasureSkinTestValue objSkinValue = (clsThreeMeasureSkinTestValue)objRecord.m_arlSkinTestValue[j2];

                                                if (objSkinValue.m_objDeleteInfo != null && objSkinValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                                                {
                                                    continue;
                                                }

                                                if (objSkinValue.m_intTimeIndex >= 0)
                                                {
                                                }
                                                else
                                                {
                                                    //��ppd
                                                    SizeF szfName = e.Graphics.MeasureString(objSkinValue.m_strMedicineName, fntSymbol);

                                                    string strValue = null;
                                                    bruTemp.Color = Color.Blue;
                                                    e.Graphics.DrawString(objSkinValue.m_strMedicineName, fntSymbol, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 1 + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + 2 + intStartPrintY);
                                                    if (objSkinValue.m_blnIsBad)
                                                    {
                                                        strValue = "(";
                                                        for (int z1 = 0; z1 < objSkinValue.m_intBadCount; z1++)
                                                        {
                                                            strValue += "+";
                                                        }
                                                        strValue += ")";
                                                        bruTemp.Color = Color.Red;
                                                        e.Graphics.DrawString(strValue, fntSymbol, bruTemp, intTextWidth + c_intGridHeight * i * 6 + (int)szfName.Width + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + intStartPrintY);
                                                    }
                                                    else
                                                    {
                                                        strValue = "(-)";
                                                        bruTemp.Color = Color.Blue;
                                                        e.Graphics.DrawString(strValue, fntSymbol, bruTemp, intTextWidth + c_intGridHeight * i * 6 + (int)szfName.Width + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + intStartPrintY);
                                                    }

                                                    if (objSkinValue.m_objDeleteInfo != null)
                                                    {
                                                        SizeF szfDesc = e.Graphics.MeasureString(objSkinValue.m_strMedicineName + strValue, fntSymbol);
                                                        e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 1 + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 1 + (int)szfDesc.Width + 1 + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + 10 + intStartPrintY);
                                                        e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 1 + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 1 + (int)szfDesc.Width + 1 + intStartPrintX, c_intWeightTotalHeight + intSkinTestNotPPDCount * (int)szfName.Height + 13 + intStartPrintY);
                                                    }

                                                    intSkinTestNotPPDCount++;
                                                }
                                            }

                                            if (p_intLeftItem > 8)
                                            {
                                                //������
                                                bruTemp.Color = Color.Blue;
                                                penOneWidthLine.Color = m_clrDST;
                                                float fltTotalHeight = 0;
                                                Font fntOtherText = new Font("����", c_flt9PointFontSize);
                                                for (int j2 = 0; j2 < objRecord.m_arlOtherValue.Count; j2++)
                                                {
                                                    clsThreeMeasureOtherValue objOtherValue = (clsThreeMeasureOtherValue)objRecord.m_arlOtherValue[j2];

                                                    if (objOtherValue.m_objDeleteInfo != null && objOtherValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                                                    {
                                                        continue;
                                                    }
                                                    RectangleF rectF = new RectangleF(intTextWidth + c_intGridHeight * i * 6 + 1 + intStartPrintX, c_intSkinTestTotalHeight + (int)fltTotalHeight + 2 + intStartPrintY, c_intGridHeight * 6, 30);
                                                    SizeF szfDesc = e.Graphics.MeasureString(objOtherValue.m_strOther, fntOtherText, (int)rectF.Width);
                                                    rectF.Height = szfDesc.Height;
                                                    e.Graphics.DrawString(objOtherValue.m_strOther, fntOtherText, bruTemp, rectF);

                                                    //if(objOtherValue.m_objDeleteInfo != null)
                                                    //{
                                                    //    string [] strValueArr = objOtherValue.m_strOther.Split('\r','\n');
                                                    //    int intHeight = 0;
                                                    //    for(int k3=0;k3<strValueArr.Length;k3++)
                                                    //    {
                                                    //        if(strValueArr[k3].Trim().Length == 0)
                                                    //            continue;

                                                    //        SizeF szfDescPart = e.Graphics.MeasureString(strValueArr[k3],fntRecordDateText);

                                                    //        e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+10+intStartPrintY+intHeight,intTextWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+10+intStartPrintY+intHeight);
                                                    //        e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+13+intStartPrintY+intHeight,intTextWidth+c_intGridHeight*i*6+1+(int)szfDescPart.Width+intStartPrintX,c_intSkinTestTotalHeight+(int)fltTotalHeight+13+intStartPrintY+intHeight);

                                                    //        intHeight += 19;
                                                    //    }
                                                    //}

                                                    fltTotalHeight += szfDesc.Height;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
			if(intLastIndex+1<m_objDateManager.m_IntRecordCount)
				p_blnHasMoreDate = true;
			else
				p_blnHasMoreDate = false;	

			//������
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
					e.Graphics.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,objValue.m_fltYPos-(float)c_intGridHeight/2f+intStartPrintY);

				if(!blnFirstPulse && objValue.m_blnLineToPreValue && objValue.m_objDeleteInfo == null )
				{
					clsThreeMeasurePulseValue objPreTempValue = objValue.m_objPreValue;

					while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objPulseValueManager.m_ObjHeader)
						objPreTempValue = objPreTempValue.m_objPreValue;

					if(objPreTempValue != m_objPulseValueManager.m_ObjHeader)
						e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY,objPreTempValue.m_fltXPos+intStartPrintX + intDiffWidth,objPreTempValue.m_fltYPos+intStartPrintY);
				}
				else if(blnFirstPulse)
				{
					blnFirstPulse = false;
				}
			}
			m_objPulseValueManager.m_mthReset();

			//���¶�
			penOneWidthLine.Color = Color.Blue;
			penDotLine.Color = Color.Red;
			bruTemp.Color = Color.Black;
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
					e.Graphics.DrawImage(objValue.m_imgSymbol,objValue.m_fltXPos-(float)c_intGridHeight/2f+intStartPrintX + intDiffWidth,objValue.m_fltYPos-(float)c_intGridHeight/2f+intStartPrintY);

					if(!blnFirstTemperature && objValue.m_blnLineToPreValue && objValue.m_objDeleteInfo == null )// && objValue.m_objPreValue != m_objPulseValueManager.m_ObjHeader && objValue.m_objPreValue.m_objDeleteInfo == null)
					{
						clsThreeMeasureTemperatureValue objPreTempValue = objValue.m_objPreValue;

						while(objPreTempValue.m_objDeleteInfo != null && objPreTempValue != m_objTemperatureValueManager.m_ObjHeader)
							objPreTempValue = objPreTempValue.m_objPreValue;

						if(objPreTempValue != m_objTemperatureValueManager.m_ObjHeader
							&& objPreTempValue.m_fltValue >= 35)
							e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY,objPreTempValue.m_fltXPos+intStartPrintX + intDiffWidth,objPreTempValue.m_fltYPos+intStartPrintY);
					}
					else if(blnFirstTemperature)
					{
						blnFirstTemperature = false;
					}

					for(int i=0;i<objValue.m_arlPhyscalDownValue.Count;i++)
					{
						clsThreeMeasureTemperaturePhyscalDownValue objDownValue = (clsThreeMeasureTemperaturePhyscalDownValue)objValue.m_arlPhyscalDownValue[i];

						e.Graphics.DrawImage(m_imgDownTemperature,objValue.m_fltXPos-(float)(c_intGridHeight-2)/2+intStartPrintX + intDiffWidth,objDownValue.m_fltYPos-(float)(c_intGridHeight-2)/2f+intStartPrintY,c_intGridHeight-2,c_intGridHeight-2);

						if(objDownValue.m_objDeleteInfo == null)
						{
							e.Graphics.DrawLine(penDotLine,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objValue.m_fltYPos+intStartPrintY,objValue.m_fltXPos+intStartPrintX + intDiffWidth,objDownValue.m_fltYPos+intStartPrintY);
						}
					}
				}
				else
				{					
					e.Graphics.DrawString("��",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight-1+intStartPrintY);
					e.Graphics.DrawString("��",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+c_intGridHeight-1+intStartPrintY);
					e.Graphics.DrawString("��",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+2*c_intGridHeight-1+intStartPrintY);
					e.Graphics.DrawString("��",fntSymbol,bruTemp,objValue.m_fltXPos+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+3*c_intGridHeight-1+intStartPrintY);

					if(objValue.m_objDeleteInfo != null)
					{
						penOneWidthLine.Color = m_clrDST;
						e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+c_intGridHeight/2+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+intStartPrintY,objValue.m_fltXPos+c_intGridHeight/2+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+4*c_intGridHeight+intStartPrintY);
						e.Graphics.DrawLine(penOneWidthLine,objValue.m_fltXPos+c_intGridHeight/2+3+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+intStartPrintY,objValue.m_fltXPos+c_intGridHeight/2+3+intStartPrintX + intDiffWidth,c_intLowEventTextStartHeight+4*c_intGridHeight+intStartPrintY);
						penOneWidthLine.Color = Color.Blue;
					}
				}
			}
			m_objTemperatureValueManager.m_mthReset();
			#endregion
			
			m_ClrPulseSymbol = clrTemp;

			m_ClrTemperatureSymbol = clrTempBase;
			m_ClrDownTemperature = clrTempDown;
            intSpecialNewStartTimes = 0;
            if(penOneWidthLine != null)
                penOneWidthLine.Dispose();
            if(penTwoWidthLine != null)
                penTwoWidthLine.Dispose();
            if(penDotLine != null)
                penDotLine.Dispose();
            if(fntRecordDateText != null)
                fntRecordDateText.Dispose();
            if(fntSpecialDateText != null)
                fntSpecialDateText.Dispose();
            if(fntGridText != null)
                fntGridText.Dispose();
            if(fntSymbol != null)
                fntSymbol.Dispose();
            if(fnt6PtText != null)
                fnt6PtText.Dispose();
            if(bruTemp != null)
                bruTemp.Dispose();
		}

		/// <summary>
		/// ��ӡ��߶Ȳ����ʣ�������
		/// </summary>
		/// <param name="p_intLeftItem">ʣ�������</param>
		/// <param name="p_intStartX">��ʼ��X����</param>
		/// <param name="p_intStartY">��ʼ��Y����</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intStartDateIndex">��ʼ��ӡ������</param>
		/// <param name="p_intRecordLength">��ӡ��������Ŀ</param>
		/// <param name="p_intEndY">���ؽ�����ӡ��Y����</param>
		/// <param name="p_blnHasMoreDate">�����Ƿ���������Ϣû�д�ӡ</param>
		public void m_mthPrintLeftRecord(int p_intLeftItem,int p_intStartX,int p_intStartY,System.Drawing.Printing.PrintPageEventArgs e,int p_intStartDateIndex,int p_intRecordLength,out int p_intEndY,out bool p_blnHasMoreDate)
		{
			int intStartPrintX = p_intStartX;
			int intStartPrintY = p_intStartY;

			int intDiffWidth = -43;

			int intTextWidth = c_intTextTotleWidth + intDiffWidth;

			int intRecordHeight = c_intRecordDateHeight;
			
			#region Check Height
            if (p_intLeftItem < int.MaxValue)
            {
                intRecordHeight += c_intOtherHeight;

                if (p_intLeftItem < 8)
                {
                    intRecordHeight += c_intSkinTestHeight;

                    if (p_intLeftItem < 7)
                    {
                        intRecordHeight += c_intWeightHeight;

                        if (p_intLeftItem < 6)
                        {
                            intRecordHeight += c_intOutStreamHeight;
                            if (p_intLeftItem < 5)
                            {
                                intRecordHeight += c_intPeeHeight;
                                if (p_intLeftItem < 4)
                                {
                                    intRecordHeight += c_intDejectaHeight;
                                    if (p_intLeftItem < 3)
                                    {
                                        intRecordHeight += c_intInputHeight;
                                        if (p_intLeftItem < 2)
                                        {
                                            intRecordHeight += c_intPressureHeight;
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

			//���߿�
			e.Graphics.DrawRectangle(penOneWidthLine,0+intStartPrintX,0+intStartPrintY,intRecordWidth,intRecordHeight);

			for(int i=0;i<p_intRecordLength;i++)
			{
				e.Graphics.DrawLine(penOneWidthLine,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,intStartPrintY,intTextWidth+c_intGridHeight*6+i*c_intGridHeight*6+intStartPrintX,intRecordHeight+intStartPrintY);
			}

			//��������ֵ�ķָ���
			penOneWidthLine.Color = Color.Black;
			e.Graphics.DrawLine(penOneWidthLine,intTextWidth+intStartPrintX,1+intStartPrintY,intTextWidth+intStartPrintX,intRecordHeight+intStartPrintY);		
			
			//���������ֺ�������ķָ���
			e.Graphics.DrawString("  ��  ��  ",fntRecordDateText,bruTemp,6+intStartPrintX,4+intStartPrintY);			
			e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,c_intRecordDateHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,c_intRecordDateHeight+intStartPrintY);

			int intPreHeight = c_intRecordDateHeight;
			penOneWidthLine.Color = Color.Black;
			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			if(p_intLeftItem == 1)
			{
				//��Ѫѹ
				e.Graphics.DrawString("Ѫѹ(mmHg)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intPressureHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			
			}

			if(p_intLeftItem <= 2)
			{
				//������Һ��
				e.Graphics.DrawString("����Һ��(ml)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			
				
				intPreHeight += c_intInputHeight;
				
				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
}
					
			if(p_intLeftItem <= 3)
			{
				//���ų���
//				e.Graphics.DrawString("�ų���",fntRecordDateText,bruTemp,4+intStartPrintX,c_intInputTotalHeight+6+intStartPrintY,stfDirectionVertical);
//				e.Graphics.DrawLine(penOneWidthLine,32+intStartPrintX,c_intInputTotalHeight+intStartPrintY,32+intStartPrintX,c_intOutStreamTotalHeight+intStartPrintY);
				e.Graphics.DrawString("���(��)",fntRecordDateText,bruTemp,35+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intDejectaHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
						
			if(p_intLeftItem <= 4)
			{
				e.Graphics.DrawString("����(ml)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);	
		
				intPreHeight += c_intPeeHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}

			if(p_intLeftItem <= 5)
			{
				e.Graphics.DrawString("����",fntSpecialDateText,bruTemp,33+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intOutStreamHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
			
			if(p_intLeftItem <= 6)
			{
				//������
				e.Graphics.DrawString("����(kg)",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intWeightHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}

			if(p_intLeftItem <= 7)
			{
				//��Ƥ��
				e.Graphics.DrawString("Ƥ��",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);			

				intPreHeight += c_intSkinTestHeight;

				e.Graphics.DrawLine(penOneWidthLine,1+intStartPrintX,intPreHeight+intStartPrintY,intRecordWidth-2+intStartPrintX,intPreHeight+intStartPrintY);
			}
			
			if(p_intLeftItem <= 8)
			{
				//������
//				e.Graphics.DrawString("����",fntRecordDateText,bruTemp,6+intStartPrintX,intPreHeight+4+intStartPrintY);						
				if(m_strOtherName == null)
				{
					e.Graphics.DrawString("����",fnt6PtText,bruTemp,1+intStartPrintX,intPreHeight+4+intStartPrintY);						
				}
				else
				{
					e.Graphics.DrawString(m_strOtherName,fnt6PtText,bruTemp,1+intStartPrintX,intPreHeight+4+intStartPrintY);						
				}
			}			

			fntRecordDateText = new Font("",c_flt12PointFontSize);
			fntSpecialDateText = new Font("",c_flt10PointFontSize);
			
			#region ����ֵ
			//�����ڼ�������Ϣ
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
				//������
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
                    //��Ѫѹ
                    penOneWidthLine.Color = m_clrDST;
                    float fltPressureTotalHeight = 0;
                    for (int j2 = 0; j2 < objRecord.m_arlPressureValue1.Count; j2++)
                    {
                        clsThreeMeasurePressureValue objPressureValue = (clsThreeMeasurePressureValue)objRecord.m_arlPressureValue1[j2];

                        if (objPressureValue.m_objDeleteInfo != null && objPressureValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
                        {
                            continue;
                        }

                        string strDesc = objPressureValue.m_fltSystolicValue.ToString("0") + "/" + objPressureValue.m_fltDiastolicValue.ToString("0");
                        e.Graphics.DrawString(strDesc, fntRecordDateText, bruTemp, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, intPreHeight + (int)fltPressureTotalHeight + 2 + intStartPrintY);

                        SizeF szfDesc = e.Graphics.MeasureString(strDesc, fntRecordDateText);

                        if (objPressureValue.m_objDeleteInfo != null)
                        {
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, intPreHeight + (int)fltPressureTotalHeight + 10 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, intPreHeight + (int)fltPressureTotalHeight + 10 + intStartPrintY);
                            e.Graphics.DrawLine(penOneWidthLine, intTextWidth + c_intGridHeight * i * 6 + 4 + intStartPrintX, intPreHeight + (int)fltPressureTotalHeight + 13 + intStartPrintY, intTextWidth + c_intGridHeight * i * 6 + 4 + (int)szfDesc.Width + intStartPrintX, intPreHeight + (int)fltPressureTotalHeight + 13 + intStartPrintY);
                        }

                        fltPressureTotalHeight += szfDesc.Height;
                    }

                    intPreHeight += c_intPressureHeight;
				}

				if(p_intLeftItem <= 2)
				{
					//������Һ��
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

				if(p_intLeftItem <= 3)
				{
					//�����
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

				if(p_intLeftItem <= 4)
				{
					//������
					penOneWidthLine.Color = m_clrDST;
					float fltPeeTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlPeeValue.Count;j2++)
					{
						clsThreeMeasurePeeValue objPeeValue = (clsThreeMeasurePeeValue)objRecord.m_arlPeeValue[j2];

						if(objPeeValue.m_objDeleteInfo != null && objPeeValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = null;
						if(!objPeeValue.m_blnIsIrretention)
						{
							strDesc = objPeeValue.m_fltValue.ToString("0.00");
						}
						else
						{
							strDesc = "*";
						}

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
					//��������
					penOneWidthLine.Color = m_clrDST;
					float fltOutStreamTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlOutStreamValue.Count;j2++)
					{
						clsThreeMeasureOutStreamValue objOutStreamValue = (clsThreeMeasureOutStreamValue)objRecord.m_arlOutStreamValue[j2];

						if(objOutStreamValue.m_objDeleteInfo != null && objOutStreamValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = objOutStreamValue.m_fltValue.ToString("0.00");
						e.Graphics.DrawString(strDesc,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+4+intStartPrintX,intPreHeight+(int)fltOutStreamTotalHeight+2+intStartPrintY);
					
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

				if(p_intLeftItem <= 6)
				{
					//������
					penOneWidthLine.Color = m_clrDST;
					float fltWeightTotalHeight = 0;
					for(int j2=0;j2<objRecord.m_arlWeightValue.Count;j2++)
					{
						clsThreeMeasureWeightValue objWeightValue = (clsThreeMeasureWeightValue)objRecord.m_arlWeightValue[j2];

						if(objWeightValue.m_objDeleteInfo != null && objWeightValue.m_objDeleteInfo.m_dtmDeleteTime <= objRecord.m_dtmFirstPrintDate)
						{
							continue;
						}

						string strDesc = null;
						if(objWeightValue.m_enmWeightType == enmThreeMeasureWeightType.һ��)
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
					//��Ƥ��	
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
							//��ppd
							SizeF szfName = e.Graphics.MeasureString(objSkinValue.m_strMedicineName,fntRecordDateText);

							string strValue = null;
							bruTemp.Color = Color.Black;
							e.Graphics.DrawString(objSkinValue.m_strMedicineName,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+2+intStartPrintY);
							if(objSkinValue.m_blnIsBad)
							{
								strValue = "(+)";
								bruTemp.Color = Color.Red;
								e.Graphics.DrawString(strValue,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+(int)szfName.Width+intStartPrintX,intPreHeight+intSkinTestNotPPDCount*(int)szfName.Height+intStartPrintY);
							}
							else
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
					//������
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

						SizeF szfDesc = e.Graphics.MeasureString(objOtherValue.m_strOther,fntRecordDateText);

						e.Graphics.DrawString(objOtherValue.m_strOther,fntRecordDateText,bruTemp,intTextWidth+c_intGridHeight*i*6+1+intStartPrintX,intPreHeight+(int)fltTotalHeight+2+intStartPrintY);

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
	}	
}
