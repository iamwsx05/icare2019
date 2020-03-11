using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ctlBornScheduleRecord ��ժҪ˵����
	/// </summary>
	public class ctlBornScheduleRecord : System.Windows.Forms.PictureBox
	{
		private System.ComponentModel.IContainer components;

		#region user-defined variable
		
		private  clsBornRecordManager m_objBornRecordManager ;
		private clsBornScheduleEveryDay m_objBornScheduleEveryDay;
		private System.Windows.Forms.ToolTip toolTip1;
		private RichTextBox[] m_conExceptionRtb;
		private RichTextBox[] m_conDealRtb;


		private DateTime m_mdtRecordDate;

		#endregion
		

		public ctlBornScheduleRecord()
		{
			///
			/// Windows.Forms ��׫д�����֧���������
			///
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			m_objBornRecordManager = new clsBornRecordManager();
			m_objBornScheduleEveryDay=new clsBornScheduleEveryDay(m_mdtRecordDate);

			m_thInitRichTextBox();

		}

		//��ʼ���ı�
		private void m_thInitRichTextBox()
		{
			m_conExceptionRtb=new RichTextBox[24];
			m_conDealRtb=new RichTextBox[24];

			//�ı������ʾ����
			for(int i=0;i<24;i++)
			{
				m_conExceptionRtb[i]=new RichTextBox();
				m_conDealRtb[i]=new RichTextBox();
				m_conExceptionRtb[i].Width =c_intGridWidth-2;
				m_conExceptionRtb[i].Height  =c_intExceptionNoteHeight-2;
				m_conExceptionRtb[i].BorderStyle= System.Windows.Forms.BorderStyle.None ;
				m_conExceptionRtb[i].ScrollBars =System.Windows.Forms.RichTextBoxScrollBars.None;
				m_conExceptionRtb[i].Tag =Convert.ToString(i+1);
				m_conExceptionRtb[i].ReadOnly =true;
				

				m_conDealRtb[i].Height  =c_intDealNoteHeight-2;
				m_conDealRtb[i].Width =c_intGridWidth-2;
				m_conDealRtb[i].BorderStyle= System.Windows.Forms.BorderStyle.None;
				m_conDealRtb[i].ScrollBars =System.Windows.Forms.RichTextBoxScrollBars.None;
				m_conDealRtb[i].Tag =Convert.ToString(i+1);
				m_conDealRtb[i].ReadOnly =true;

				toolTip1.Active =true;
				toolTip1.AutomaticDelay =500;
				toolTip1.AutoPopDelay =3000;
				toolTip1.InitialDelay =500;
				toolTip1.ReshowDelay =100;
				toolTip1.ShowAlways = false;
				m_conExceptionRtb[i].MouseHover +=new EventHandler(m_conExceptionRtb_MouseHover);
				m_conExceptionRtb[i].MouseDown +=new MouseEventHandler(m_conExceptionRtb_MouseDown);
				m_conDealRtb[i].MouseDown +=new MouseEventHandler(m_conDealRtb_MouseDown);
				m_conDealRtb[i].MouseHover +=new EventHandler(m_conDealRtb_MouseHover);
				
				this.Controls.AddRange(m_conExceptionRtb);
				this.Controls.AddRange(m_conDealRtb);
			}

			

		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
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


		#region �����������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			// 
			// ctlBornScheduleRecord
			// 
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ctlBornScheduleRecord_Paint);
			
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlBornScheduleRecord_MouseDown);

		}
		#endregion

		#region ����

		#region ��ǰ�û�
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
		#endregion
		#endregion
		
		#region  ������һЩ���� 
		
		
		
		#region  //���������С����ֵ
		private const float c_flt12PointFontSize = 12f;
		private const float c_flt10PointFontSize = 10.5f;

		private const float c_flt9PointFontSize = 9f;
		private const float c_flt7PointFontSize = 7.5f;
		private const float c_flt6PointFontSize = 6.75f;
		private const float c_flt5PointFontSize = 5.25f;
		private const float c_flt14PointFontSize = 14.25f;

		#endregion

		

		#region   //������ɲ��ֵĸ߶�
		private const int c_intTopBankHeight=15;

		private const int c_intGridHeight = 24; //���Ӹ�
		private const int c_intGridWidth = 22;  //���ӿ�
		private const int c_intGridHeightCount = 10;
		private int c_intGridTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight;

		private const int c_intMiddleTapHeight=53;  //�м����Ŀհײ���
		private int c_intMiddleTapTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight;

		private const int c_intCheckTimeHeight=25;
		private int c_intCheckTimeTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight;

		private const int c_intBloodPressureHeight=18;
		private int c_intBloodPressureTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight;

		private const int c_intEmbryoHeartHeight=18;
		private int c_intEmbryoHeartTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight;

		private const int c_intEvnterScaleHeight=18;
		private int c_intEvnterScaleTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight;

		private const int c_intExceptionNoteHeight=54;
		private int c_intExceptionNoteTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight;

		private const int c_intDealNoteHeight=54;
		private int c_intDealNoteTotalHeight =  c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight+c_intDealNoteHeight;

		private const int c_intSignNameHeight=22;
		private int c_intSignNameTotalHeight =  c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight+c_intDealNoteHeight+c_intSignNameHeight;

		/// �ܹ��ĸ߶�
		/// </summary>
		private int m_intTotalHeight= c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight+c_intDealNoteHeight+c_intSignNameHeight;

		//���������ı��Ŀ��
		private const int c_intLeftTextWidth=25;
		private const int c_intRightTextWidth=15;
		private const int c_intLeftBeginDrawWidth=5;

		//�ܿ�
		private const int c_intColumnCount = 25;
		private  int c_intTotalWidth=c_intGridWidth*c_intColumnCount;
		
		#endregion

		
		#region //������õ���ɫ

		//�����ɫ
		private Color m_clrBorder=Color.Black ;

		public Color m_ClrBorder
		{
			set 
			{
				m_clrBorder=value;
				Invalidate();
			}

			get
			{
				return m_clrBorder;
			}
		}

		//������ɫ
		private Color m_clrGridLine=Color.Black ;

		public Color m_ClrGridLine
		{
			get
			{
				return m_clrGridLine;
			}
			set 
			{
				m_clrGridLine=value;
				Invalidate();
			}

		}

		//�����߻�ԲȦ��ɫ
		private Color m_clrSpecialLine=Color.Red  ;

		public Color m_ClrSpecialLine
		{
			get
			{
				return m_clrSpecialLine;
			}
			set 
			{
				m_clrSpecialLine=value;
				Invalidate();
			}

		}


		//�ı�����ɫ
		private Color m_clrDrawText=Color.Black  ;

		public Color m_ClrDrawText
		{
			get
			{
				return m_clrDrawText;
			}
			set 
			{
				m_clrDrawText=value;
				Invalidate();
			}

		}

		//��������ɫ
		private Color m_clrContinueLine=Color.Black  ;

		public Color m_ClrContinueLine
		{
			get
			{
				return m_clrContinueLine;
			}
			set 
			{
				m_clrContinueLine=value;
				Invalidate();
			}

		}




		#endregion 

		#region  //���ڿ���־
		private Image m_imgVenterSymbol  = new Bitmap(10,10);
		private void m_mthInitVenterFlagImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgVenterSymbol);			
			gphImage.DrawEllipse(new Pen(m_clrSpecialLine,1),0,0,9,9);

			gphImage.Dispose();
		}
		//�����õĵ�
		private Image m_imgVenterPoint  = new Bitmap(8,8);
		private void m_mthInitVenterPointImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgVenterPoint);			
			gphImage.FillEllipse(new SolidBrush(Color.Red),0,0,8,8);

			gphImage.Dispose();
		}

		//��ɫ��ǵĵ�
		private Image m_imgVenterPointBlue  = new Bitmap(12,12);
		private void m_mthInitVenterPointBlueImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgVenterPointBlue);
			Pen penLine =new Pen(Color.Blue,2);
			gphImage.DrawLine(penLine,0,9,9,0);
			gphImage.DrawLine(penLine,0,0,9,9);
			
			gphImage.Dispose();
		}

		private Color m_clrVenterSymbol = Color.Red ;
		public Color m_ClrVenterSymbol
		{
			get
			{
				return m_clrVenterSymbol;
			}
			set
			{
				m_clrVenterSymbol = value;

				m_mthInitVenterFlagImage();
				m_mthInitVenterPointImage();
				m_mthInitVenterPointBlueImage();
				this.Invalidate();
			}
		}

		#endregion

		#endregion
		

		//�ؼ�����ӿ�
		//�����¼
		public clsBornScheduleEveryDay m_clsCurrentDay
		{
			set 
			{
				m_objBornScheduleEveryDay=value;
			}
			get 
			{
				return m_objBornScheduleEveryDay;
			}
		}

		//��Ů��¼
		public clsBornRecordManager m_clsBornRecordManager
		{
			set 
			{
				m_objBornRecordManager=value;
			}
			get 
			{
				return m_objBornRecordManager;
			}
		}

		private void m_conExceptionRtb_MouseHover(object sender, System.EventArgs e)
		{
			RichTextBox objrtb=(RichTextBox)sender;
			toolTip1.SetToolTip(objrtb,objrtb.Text.Trim());
			
			
		}
		private void m_conDealRtb_MouseHover(object sender, System.EventArgs e)
		{
			RichTextBox objrtb=(RichTextBox)sender;
			toolTip1.SetToolTip(objrtb,objrtb.Text.Trim());
			
		}

		private void m_conExceptionRtb_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//�쳣����¼�
			
			RichTextBox objrtb=(RichTextBox)sender;
			if(m_evnExceptionNoteMouseDown !=null)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlExceptionNoteCol.Count;i++)
				{
					clsExceptionNoteCol objCurrentPoint=(clsExceptionNoteCol)m_objBornScheduleEveryDay.m_arlExceptionNoteCol[i];
						
					
					if(int.Parse(objrtb.Tag.ToString())==objCurrentPoint.m_intHourValue  && objCurrentPoint.m_strExceptionNoteValue.Trim().Length>0 && objCurrentPoint.m_strExceptionNoteValue.Trim()==objrtb.Text.Trim())		
					{
						//�����¼�
						clsBornScheduleExceptionNoteEventArgs objEventArgs=new clsBornScheduleExceptionNoteEventArgs();
						objEventArgs.objArgsValue=(clsExceptionNoteCol)objCurrentPoint;
						
						m_evnExceptionNoteMouseDown(sender,objEventArgs);
						break;

					}

				}
			}

			
		}

		private void m_conDealRtb_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//�����¼�¼�
			
			RichTextBox objrtb=(RichTextBox)sender;
			if(m_evnDealNoteMouseDown !=null)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlDealNoteCol.Count;i++)
				{
					clsDealNoteCol objCurrentPoint=(clsDealNoteCol)m_objBornScheduleEveryDay.m_arlDealNoteCol[i];
					if(int.Parse(objrtb.Tag.ToString())==objCurrentPoint.m_intHourValue  && objCurrentPoint.m_strDealNoteValue.Trim().Length>0 && objCurrentPoint.m_strDealNoteValue.Trim()==objrtb.Text.Trim())		
					{
						//�����¼�
						clsBornScheduleDealNoteEventArgs objEventArgs=new clsBornScheduleDealNoteEventArgs();
						objEventArgs.objArgsValue=(clsDealNoteCol)objCurrentPoint;
						
						m_evnDealNoteMouseDown(sender,objEventArgs);
						break;

					}

				}
			}
			
		}

		

		//��PAINT�¼��л�ͼ
		private void ctlBornScheduleRecord_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics p_objGrp=e.Graphics ;
			m_mthPaint(p_objGrp);
			//			System.Drawing.Image imgTop = new Bitmap(this.Width,this.Height);
			//			Graphics objTopGrp = Graphics.FromImage(imgTop);
			//			objTopGrp.SetClip(new Rectangle(0,0,this.Width,this.Height-c_intGridTotalHeight));
			//			m_mthPaint(objTopGrp);						
			//			objTopGrp.Dispose();
			//
			//			System.Drawing.Image imgBottom = new Bitmap(this.Width,this.Height);
			//			Graphics objBottomGrp = Graphics.FromImage(imgBottom);
			//			objBottomGrp.SetClip(new Rectangle(0,c_intGridTotalHeight,this.Width,this.Height-c_intGridTotalHeight));
			//			m_mthPaint(objBottomGrp);
			//			objBottomGrp.Dispose();						
			//
			//			e.Graphics.DrawImage(imgTop,0,0);
			//		
			//			e.Graphics.DrawImage(imgBottom,0,-1*(c_intGridHeight*25));
			//				
			//			imgTop.Dispose();
			//			imgBottom.Dispose();
		
		}	
	
		private int m_thSplitString(string p_strOriginal,out int p_strLast)
		{
			try
			{
				if(p_strOriginal==null || p_strOriginal=="")
				{
					p_strLast=0;
					return 0;
				}

				string strTemp=p_strOriginal;
				bool bnlIsString=false;
				bnlIsString=strTemp.StartsWith("$");
				if(bnlIsString)
				{
					strTemp=strTemp.Substring(2,strTemp.Length -2);
				}
				
				strTemp=strTemp.Substring(1,strTemp.Length-1);
				int strPre=0;
				int intIndex=strTemp.IndexOf("|",0);
				if(intIndex>0)
				{
					strPre=int.Parse(strTemp.Substring(0,intIndex));
					p_strLast=int.Parse(strTemp.Substring(intIndex+1,strTemp.Length-intIndex-1));
					return strPre;
				}
				p_strLast=0;
				return 0;
			}
			catch
			{
				p_strLast=0;
				return 0;
			}
		}

		private int[] m_thSetDefalutValue()
		{
			//�в���,Ԥ����,Ĭ���ĵ�ȸ�ֵ,
			

			int intFirstLeft=0;
			int intFirstRight=0;
			int intSecondLeft=0;
			int intSecondRight=0;
			int intThreeLeft=0;
			int intThreeRight=0;
			int intFourLeft=0;
			int intFourRight=0;
			int strLast=0;

			string strTempVlaue=m_objBornRecordManager.m_strFIRSTPOINT==null?null: m_objBornRecordManager.m_strFIRSTPOINT.Trim();
			
			intFirstLeft=m_thSplitString(strTempVlaue,out strLast);//��һ��
			intFirstRight =strLast;//��һ��

			strTempVlaue=m_objBornRecordManager.m_strSECONDPOINT==null?null: m_objBornRecordManager.m_strSECONDPOINT.Trim();
			intSecondLeft = m_thSplitString(strTempVlaue,out strLast);//�ڶ���
			intSecondRight =strLast;

			strTempVlaue=m_objBornRecordManager.m_strTHREEPOINT==null?null: m_objBornRecordManager.m_strTHREEPOINT.Trim();
			intThreeLeft = m_thSplitString(strTempVlaue,out strLast);//������
			intThreeRight  =strLast;

			strTempVlaue=m_objBornRecordManager.m_strFOUTPOINT==null?null: m_objBornRecordManager.m_strFOUTPOINT.Trim();;//���ĵ�
			intFourLeft= m_thSplitString(strTempVlaue,out strLast);
			intFourRight = strLast;

			int[] intPointColArr=new int[8];
			intPointColArr[0]=intFirstLeft;
			intPointColArr[1]=10-intFirstRight;
			intPointColArr[2]=intSecondLeft;
			intPointColArr[3]=10-intSecondRight;
			intPointColArr[4]=intThreeLeft;
			intPointColArr[5]=10-intThreeRight;
			intPointColArr[6]=intFourLeft;
			intPointColArr[7]=10-intFourRight;

			if(m_objBornRecordManager.m_arlBornScheduleEveryDay.Count<=0)
			{
				intPointColArr[0]=3;
				intPointColArr[1]=10-3;
				intPointColArr[2]=9;
				intPointColArr[3]=10-10;
				intPointColArr[4]=7;
				intPointColArr[5]=10-3;
				intPointColArr[6]=13;
				intPointColArr[7]=10-10;
			}


			return intPointColArr;



		}

		private void m_mthPaint(System.Drawing.Graphics p_objGrp)
		{
			
			Pen penOneWidthLine = new Pen(m_clrGridLine);
			Pen penTwoWidthLine = new Pen(m_clrBorder,2);
			Pen penSpecialLine = new Pen(m_clrSpecialLine);
						
			SolidBrush bruTemp = new SolidBrush(m_clrDrawText);

			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			
			Font fntRecordText = new Font("",c_flt7PointFontSize);
			Font fntSpeText = new Font("",c_flt6PointFontSize);
			Font fntCorrdinateText = new Font("",c_flt7PointFontSize);
			#region  ���������ı�
			//���߿����ұ�Ե���հ׻��ı�,������5���ؿ�ʼ����
			p_objGrp.DrawRectangle(penTwoWidthLine,c_intLeftTextWidth,c_intTopBankHeight,c_intTotalWidth-c_intGridWidth,m_intTotalHeight-1);

			//����������
			for(int i=0;i<c_intColumnCount;i++)
			{
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth,c_intTopBankHeight,c_intLeftTextWidth+i*c_intGridWidth,c_intGridTotalHeight);		
			}

			//����������
			for(int i=0;i<c_intGridHeightCount;i++)
			{
				if(i==4)
					p_objGrp.DrawLine(penSpecialLine,c_intLeftTextWidth,c_intTopBankHeight+(i+1)*c_intGridHeight,c_intTotalWidth,c_intTopBankHeight+(i+1)*c_intGridHeight);		
				else
					p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth,c_intTopBankHeight+(i+1)*c_intGridHeight,c_intTotalWidth,c_intTopBankHeight+(i+1)*c_intGridHeight);		
			}

			//�����������ֵ

			//��������
			for(int i=1;i<c_intColumnCount-1;i++)
			{
				p_objGrp.DrawString(i.ToString(),fntCorrdinateText,bruTemp,c_intLeftTextWidth+i*c_intGridWidth-5,c_intTopBankHeight+c_intGridTotalHeight-11);
			}

			////��������0
			p_objGrp.DrawString("0",fntCorrdinateText,bruTemp,c_intLeftTextWidth,c_intTopBankHeight+c_intGridTotalHeight-11);

			////��������24
			p_objGrp.DrawString("24",fntCorrdinateText,bruTemp,c_intTotalWidth-11,c_intTopBankHeight+c_intGridTotalHeight-11);
			

			//�����������
			for(int i=1;i<c_intGridHeightCount+1;i++)
			{
				p_objGrp.DrawString(i.ToString(),fntCorrdinateText,bruTemp,c_intLeftTextWidth+6,c_intGridTotalHeight-i*c_intGridHeight+5);
			}

			//���Ҳ�������
			for(int i=1;i<c_intGridHeightCount+1;i++)
			{
				if(i<6)
					p_objGrp.DrawString(Convert.ToString(i-6),fntCorrdinateText,bruTemp,c_intTotalWidth-12,c_intGridTotalHeight-i*c_intGridHeight+5);
				else
					p_objGrp.DrawString("+"+Convert.ToString(i-5),fntCorrdinateText,bruTemp,c_intTotalWidth-14,c_intGridTotalHeight-i*c_intGridHeight+5);
			}

			//����๬���ı�
			p_objGrp.DrawString("���ڿ������� (",fntRecordText,bruTemp,c_intLeftBeginDrawWidth,c_intTopBankHeight+c_intGridHeight*2,stfDirectionVertical);

			//����ɫ��Ȧͼ��
			p_objGrp.DrawImage(m_imgVenterSymbol,c_intLeftBeginDrawWidth+3,c_intTopBankHeight+c_intGridHeight*2+c_flt7PointFontSize*11-2);
			p_objGrp.DrawString("��ɫ���)",fntRecordText,bruTemp,c_intLeftBeginDrawWidth,c_intTopBankHeight+c_intGridHeight*2+c_flt7PointFontSize*12,stfDirectionVertical);

			//���Ҳ�̥ͷ�½�
			p_objGrp.DrawString("̥ͷ�½�",fntRecordText,bruTemp,c_intTotalWidth+3,c_intTopBankHeight+c_intGridHeight*2,stfDirectionVertical);

			//���Ҳ��ɫ��Ȧͼ��
			p_objGrp.DrawImage(m_imgVenterSymbol,c_intTotalWidth+5,c_intGridTotalHeight-5*c_intGridHeight-2);
		
			//���м�հײ��ֵĲ���Сʱ
			p_objGrp.DrawString("����Сʱ",fntRecordText,bruTemp,c_intTotalWidth-c_intGridWidth*7,c_intMiddleTapTotalHeight-15);

			//�����²�����
			//�����ʱ��TOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intMiddleTapTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intMiddleTapTotalHeight);

			//�����ʱ���ı�
			p_objGrp.DrawString("���",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5,c_intMiddleTapTotalHeight+2);
			p_objGrp.DrawString("ʱ��",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5,c_intMiddleTapTotalHeight+12);

			//��ѪѹTOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intCheckTimeTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intCheckTimeTotalHeight);

			//��Ѫѹ�ı�
			p_objGrp.DrawString("Ѫѹ",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5,c_intCheckTimeTotalHeight+4);

			//��̥��TOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intBloodPressureTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intBloodPressureTotalHeight);

			//��̥���ı�
			p_objGrp.DrawString("̥��",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5,c_intBloodPressureTotalHeight+4);

			//������TOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intEmbryoHeartTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intEmbryoHeartTotalHeight);

			//�������ı�
			p_objGrp.DrawString("����",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5,c_intEmbryoHeartTotalHeight+4);

			//���쳣���TOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intEvnterScaleTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intEvnterScaleTotalHeight);

			//���쳣����ı�
			p_objGrp.DrawString("�쳣���",fntRecordText,bruTemp,c_intLeftBeginDrawWidth,c_intEvnterScaleTotalHeight+6,stfDirectionVertical);

			//�������¼TOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intExceptionNoteTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intExceptionNoteTotalHeight);

			//�������¼�ı�
			p_objGrp.DrawString("�����¼",fntRecordText,bruTemp,c_intLeftBeginDrawWidth,c_intExceptionNoteTotalHeight+6,stfDirectionVertical);

			//��ǩ��TOP��
			p_objGrp.DrawLine(penOneWidthLine,0,c_intDealNoteTotalHeight,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,c_intDealNoteTotalHeight);

			//��ǩ��Lower��
			p_objGrp.DrawLine(penOneWidthLine,0,m_intTotalHeight+13,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth,m_intTotalHeight+13);


			//��ǩ���ı�
			p_objGrp.DrawString("ǩ��",fntRecordText,bruTemp,c_intLeftBeginDrawWidth,c_intDealNoteTotalHeight+4,stfDirectionVertical);

			//��̥�������־�ı�
			p_objGrp.DrawString("̥��������",fntRecordText,bruTemp,c_intTotalWidth+3,c_intBloodPressureTotalHeight+8,stfDirectionVertical);

			//�����沿����������
			for(int i=0;i<c_intColumnCount-1;i++)
			{
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth,c_intMiddleTapTotalHeight,c_intLeftTextWidth+i*c_intGridWidth,m_intTotalHeight+c_intTopBankHeight-1);		
				
				//��бѪѹ�Խ���
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth+2,c_intBloodPressureTotalHeight-3,c_intLeftTextWidth+(i+1)*c_intGridWidth-3,c_intCheckTimeTotalHeight+2);		
				
				//��б�����Խ���
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth+2,c_intEvnterScaleTotalHeight-3,c_intLeftTextWidth+(i+1)*c_intGridWidth-3,c_intEmbryoHeartTotalHeight+2);		

			}

			//��3��9����ĺ���
			p_objGrp.DrawLine(penSpecialLine,c_intLeftTextWidth,c_intTopBankHeight+c_intGridHeight*7,c_intLeftTextWidth+c_intGridWidth*9,c_intTopBankHeight+c_intGridHeight*7);
			p_objGrp.DrawLine(penSpecialLine,c_intLeftTextWidth+c_intGridWidth*9,c_intTopBankHeight+c_intGridHeight*7,c_intLeftTextWidth+c_intGridWidth*9,c_intTopBankHeight+c_intGridHeight*10);
		

			if(m_objBornRecordManager!=null)
			{
				//���û��趨������趨ֵ,�����Ĭ��ֵ
				int[] intDefalutPointArr=(int[])m_thSetDefalutValue();
				
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[0],c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[1],c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[2],c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[3]);
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[4],c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[5],c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[6],c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[7]);

				//��ƽ�������˵�
				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[0]-4,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[1]-4);
				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[2]-4,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[3]-4);

				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[4]-4,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[5]-4);
				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[6]-4,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[7]-4);

				//				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[0],c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[1],c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[2],c_intTopBankHeight);
				//				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*7,c_intTopBankHeight+c_intGridHeight*7,c_intLeftTextWidth+c_intGridWidth*13,c_intTopBankHeight);
				//
				//				//��ƽ�������˵�
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*3-4,c_intTopBankHeight+c_intGridHeight*7-4);
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*9-4,c_intTopBankHeight-4);
				//
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*7-4,c_intTopBankHeight+c_intGridHeight*7-4);
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*13-4,c_intTopBankHeight-4);

			}

			//�ı���λ���趨
			for(int i=0;i<24;i++)
			{
				Point ribPointExcption=new Point(c_intLeftTextWidth+i*c_intGridWidth+1,c_intEvnterScaleTotalHeight+1);
				m_conExceptionRtb[i].Location =ribPointExcption;

				Point ribPointDeal=new Point(c_intLeftTextWidth+i*c_intGridWidth+1,c_intExceptionNoteTotalHeight+1);
				m_conDealRtb[i].Location =ribPointDeal;
				
			}



			#endregion
			
			//����ֵ
			#region

			//�����ڵ�
			int intXCorrd=0;
			int intYCorrd=0;
			int intPreXCorrd=0;
			int intPreYCorrd=0;
			if(m_objBornScheduleEveryDay !=null)
			{
				if(m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol.Count>0)
				{
					//sort
				
					
					for(int i=0;i<m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol.Count;i++)
					{
						clsBornScheduleEveryHourCol objCurrentPoint=(clsBornScheduleEveryHourCol)m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol[i];
						
						intXCorrd=objCurrentPoint.m_intHourValue;
						intYCorrd=10-objCurrentPoint.m_intVenterValue;
						//����

						SolidBrush bruTemp1 = new SolidBrush(m_clrDrawText);
			
						Font fntRecordText1 = new Font("",c_flt7PointFontSize);

						p_objGrp.DrawImage(m_imgVenterPointBlue,c_intLeftTextWidth+c_intGridWidth*intXCorrd-4,c_intTopBankHeight+c_intGridHeight*intYCorrd-4);
						
					
						if(objCurrentPoint.m_bnlIsHavePreValue)
						{
							if(i>0)
							{
								clsBornScheduleEveryHourCol objPrePoint=(clsBornScheduleEveryHourCol)m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol[i-1];
								intPreXCorrd=objPrePoint.m_intHourValue;
								intPreYCorrd=10-objPrePoint.m_intVenterValue;
							
								p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*intPreXCorrd,c_intTopBankHeight+c_intGridHeight*intPreYCorrd,c_intLeftTextWidth+c_intGridWidth*intXCorrd-1,c_intTopBankHeight+c_intGridHeight*intYCorrd);
							}
						}
					}
				}
			
				
				

				//�����ʱ��
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlCheckTimeCol.Count;i++)
				{
					clsCheckTimeCol objCurrentValue=(clsCheckTimeCol)m_objBornScheduleEveryDay.m_arlCheckTimeCol[i];

					p_objGrp.DrawString(objCurrentValue.m_strCheckTime,fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1),c_intMiddleTapTotalHeight+4);
				}

				//��Ѫѹ
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlBloodPressureCol.Count;i++)
				{
					clsBloodPressureCol objCurrentValue=(clsBloodPressureCol)m_objBornScheduleEveryDay.m_arlBloodPressureCol[i];

					p_objGrp.DrawString(objCurrentValue.m_strScaleBloodPressureValue,fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)-1,c_intCheckTimeTotalHeight+1);
					p_objGrp.DrawString(objCurrentValue.m_strExtendBloodPressureValue,fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+13-4,c_intBloodPressureTotalHeight-11);
				}


				//��̥��
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlEmbryoHeartCol.Count;i++)
				{
					clsEmbryoHeartCol objCurrentValue=(clsEmbryoHeartCol)m_objBornScheduleEveryDay.m_arlEmbryoHeartCol[i];

					p_objGrp.DrawString(objCurrentValue.m_strEmbryoHeartValue,fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+2,c_intBloodPressureTotalHeight+3);
				}

				//������
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlVenterScaleExtendCol.Count;i++)
				{
					clsVenterScaleExtendCol objCurrentValue=(clsVenterScaleExtendCol)m_objBornScheduleEveryDay.m_arlVenterScaleExtendCol[i];

					p_objGrp.DrawString(objCurrentValue.m_strScaleVenterValue,fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)-1,c_intEmbryoHeartTotalHeight+1);
					p_objGrp.DrawString(objCurrentValue.m_strExtendVenterValue,fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+13-5,c_intEmbryoHeartTotalHeight+8);
				}

				try
				{

					//���쳣���
					int intHourValue=0;
					for(int i=0;i<24;i++)
					{
						m_conExceptionRtb[i].Text="";
					}
					if(m_objBornScheduleEveryDay.m_arlExceptionNoteCol.Count>0)
					{
						for(int i=0;i<m_objBornScheduleEveryDay.m_arlExceptionNoteCol.Count;i++)
						{
							clsExceptionNoteCol objCurrentValue=(clsExceptionNoteCol)m_objBornScheduleEveryDay.m_arlExceptionNoteCol[i];
					
							intHourValue=objCurrentValue.m_intHourValue;
							if((objCurrentValue.m_strExceptionNoteValue!="" && objCurrentValue.m_strExceptionNoteValue!=null) && intHourValue-1>=0)
								m_conExceptionRtb[intHourValue-1].Text=objCurrentValue.m_strExceptionNoteValue;
							else
								m_conExceptionRtb[intHourValue-1].Clear();
							//p_objGrp.DrawString(objCurrentValue.m_strExceptionNoteValue,fntRecordText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1),c_intEvnterScaleTotalHeight+13);
						}
					}
					

					//�������¼
					intHourValue=0;
					for(int i=0;i<24;i++)
					{
						m_conDealRtb[i].Text="";
					}
					if(m_objBornScheduleEveryDay.m_arlDealNoteCol.Count>0)
					{
						for(int i=0;i<m_objBornScheduleEveryDay.m_arlDealNoteCol.Count;i++)
						{
							clsDealNoteCol objCurrentValue=(clsDealNoteCol)m_objBornScheduleEveryDay.m_arlDealNoteCol[i];
					
							intHourValue=objCurrentValue.m_intHourValue;
							if((objCurrentValue.m_strDealNoteValue!="" && objCurrentValue.m_strDealNoteValue!=null) && intHourValue-1>=0)
								m_conDealRtb[intHourValue-1].Text=objCurrentValue.m_strDealNoteValue;
							else
								m_conDealRtb[intHourValue-1].Clear();
							//p_objGrp.DrawString(objCurrentValue.m_strDealNoteValue,fntRecordText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1),c_intExceptionNoteTotalHeight+13);
						}
					}
					
				}
				catch(Exception ex)
				{
				  ex.ToString();
				}

			
				//��ǩ��
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlSignNameCol.Count;i++)
				{
					clsSignNameCol objCurrentValue=(clsSignNameCol)m_objBornScheduleEveryDay.m_arlSignNameCol[i];
					string strLast=null;

					p_objGrp.DrawString(m_thSplitName(objCurrentValue.m_strSignNameID,out strLast),fntRecordText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1),c_intDealNoteTotalHeight+2,stfDirectionVertical);
				}

			}
			#endregion
		

		

		}
		private string m_thSplitName(string p_strOriginal,out string p_strLast)
		{
			if(p_strOriginal==null || p_strOriginal=="")
			{
				p_strLast=null;
				return null;
			}

			//strTemp���ı��ĸ�ʽ��:"A3|3" ��"3|3"ϵͳ��ת��Ϊ2005-03-03 00:00:00״̬��clsXML_SQL_Converter 
			string strTemp=p_strOriginal;
			strTemp=strTemp.Substring(0,strTemp.Length-1);
			string strPre=null;
			int intIndex=strTemp.IndexOf("|",0);
			if(intIndex>0)
			{
				strPre=strTemp.Substring(0,intIndex);
				p_strLast=strTemp.Substring(intIndex+1,strTemp.Length-intIndex-1);
				return strPre;
			}
			p_strLast=null;
			return null;
		}
		#region �ؼ����¼�

		public event EventHandler m_evnEveryHourMouseDown;
		public event EventHandler m_evnCheckTimeMouseDown;
		public event EventHandler m_evnBloodPressureMouseDown;
		public event EventHandler m_evnEmbryoHeartMouseDown;
		public event EventHandler m_evnVenterScaleExtendMouseDown;
		public event EventHandler m_evnExceptionNoteMouseDown;
		public event EventHandler m_evnDealNoteMouseDown;
		public event EventHandler m_evnSignNameMouseDown;
		
												   
		private void ctlBornScheduleRecord_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int intXCorrd=0;
			int intyCorrd=0;
			

			//�����곬����Χ����.
			if(e.Y>(c_intSignNameTotalHeight) || e.X >(c_intTotalWidth+c_intLeftTextWidth))
				return;

			//���ڿ��¼�
			if(m_evnEveryHourMouseDown !=null 
				&& e.X <=(c_intTotalWidth+c_intLeftTextWidth) && e.X >=c_intLeftTextWidth
				&& e.Y<=c_intGridTotalHeight && e.Y >=c_intTopBankHeight)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol.Count;i++)
				{
					clsBornScheduleEveryHourCol objCurrentPoint=(clsBornScheduleEveryHourCol)m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol[i];
						
					intXCorrd=objCurrentPoint.m_intHourValue*c_intGridWidth+ c_intLeftTextWidth;
					intyCorrd=(10-objCurrentPoint.m_intVenterValue)*c_intGridHeight +c_intTopBankHeight;

					//��λ������
					

					if(Math.Abs(intXCorrd - e.X) <=c_intGridWidth/3 && Math.Abs(intyCorrd- e.Y) <=c_intGridHeight/3)
					{
						//�����¼�
						clsBornScheduleEveryHourEventArgs objEventArgs=new clsBornScheduleEveryHourEventArgs();
						objEventArgs.objArgsValue=(clsBornScheduleEveryHourCol)objCurrentPoint;
						
						m_evnEveryHourMouseDown(sender,objEventArgs);
						break;


					}

				}
			}

			//���ʱ���¼�
			if(m_evnCheckTimeMouseDown !=null 
				&& e.X <=(c_intTotalWidth+c_intLeftTextWidth) && e.X >=c_intLeftTextWidth
				&& e.Y<=c_intBloodPressureTotalHeight && e.Y >=c_intMiddleTapTotalHeight)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlCheckTimeCol.Count;i++)
				{
					clsCheckTimeCol objCurrentPoint=(clsCheckTimeCol)m_objBornScheduleEveryDay.m_arlCheckTimeCol[i];
						
					//					intXCorrd=objCurrentPoint.m_intHourValue*c_intGridWidth+ c_intLeftTextWidth;
					intXCorrd=(int)Math.Floor((double)e.X/c_intGridWidth);
					

					if(intXCorrd==objCurrentPoint.m_intHourValue && e.Y<c_intCheckTimeTotalHeight && e.Y>c_intMiddleTapTotalHeight && objCurrentPoint.m_strCheckTime.Trim().Length>0)	
					{
						//�����¼�
						clsBornScheduleCheckTimeEventArgs objEventArgs=new clsBornScheduleCheckTimeEventArgs();
						objEventArgs.objArgsValue=(clsCheckTimeCol)objCurrentPoint;
						
						m_evnCheckTimeMouseDown(sender,objEventArgs);
						break;

					}

				}
			}

			//Ѫѹ�¼�
			if(m_evnBloodPressureMouseDown !=null 
				&& e.X <=(c_intTotalWidth+c_intLeftTextWidth) && e.X >=c_intLeftTextWidth
				&& e.Y<=c_intEmbryoHeartTotalHeight && e.Y >=c_intCheckTimeTotalHeight)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlBloodPressureCol.Count;i++)
				{
					clsBloodPressureCol objCurrentPoint=(clsBloodPressureCol)m_objBornScheduleEveryDay.m_arlBloodPressureCol[i];

                    intXCorrd = (int)Math.Floor((double)e.X / c_intGridWidth);
					

					if(intXCorrd==objCurrentPoint.m_intHourValue && e.Y<c_intBloodPressureTotalHeight && e.Y>c_intCheckTimeTotalHeight && (objCurrentPoint.m_strScaleBloodPressureValue.Trim().Length>0 || objCurrentPoint.m_strExtendBloodPressureValue.Trim().Length>0))
					{
						//�����¼�
						clsBornScheduleBloodPressureEventArgs objEventArgs=new clsBornScheduleBloodPressureEventArgs();
						objEventArgs.objArgsValue=(clsBloodPressureCol)objCurrentPoint;
						
						m_evnBloodPressureMouseDown(sender,objEventArgs);
						break;

					}

				}
			}


			//̥���¼�
			if(m_evnEmbryoHeartMouseDown !=null 
				&& e.X <=(c_intTotalWidth+c_intLeftTextWidth) && e.X >=c_intLeftTextWidth
				&& e.Y<=c_intEmbryoHeartTotalHeight && e.Y >=c_intCheckTimeTotalHeight)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlEmbryoHeartCol.Count;i++)
				{
					clsEmbryoHeartCol objCurrentPoint=(clsEmbryoHeartCol)m_objBornScheduleEveryDay.m_arlEmbryoHeartCol[i];

                    intXCorrd = (int)Math.Floor((double)e.X / c_intGridWidth);
					

					if(intXCorrd==objCurrentPoint.m_intHourValue && e.Y<c_intEmbryoHeartTotalHeight && e.Y>c_intBloodPressureTotalHeight  && objCurrentPoint.m_strEmbryoHeartValue.Trim().Length>0)	
					{
						//�����¼�
						clsBornScheduleEmbryoHeartEventArgs objEventArgs=new clsBornScheduleEmbryoHeartEventArgs();
						objEventArgs.objArgsValue=(clsEmbryoHeartCol)objCurrentPoint;
						
						m_evnEmbryoHeartMouseDown(sender,objEventArgs);
						break;

					}

				}
			}
			
			//�����¼�
			if(m_evnVenterScaleExtendMouseDown !=null 
				&& e.X <=(c_intTotalWidth+c_intLeftTextWidth) && e.X >=c_intLeftTextWidth
				&& e.Y<=c_intEvnterScaleTotalHeight && e.Y >=c_intEmbryoHeartTotalHeight)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlVenterScaleExtendCol.Count;i++)
				{
					clsVenterScaleExtendCol objCurrentPoint=(clsVenterScaleExtendCol)m_objBornScheduleEveryDay.m_arlVenterScaleExtendCol[i];

                    intXCorrd = (int)Math.Floor((double)e.X / c_intGridWidth);
					

					if(intXCorrd==objCurrentPoint.m_intHourValue && e.Y<c_intEvnterScaleTotalHeight && e.Y>c_intEmbryoHeartTotalHeight && (objCurrentPoint.m_strScaleVenterValue.Trim().Length>0 || objCurrentPoint.m_strExtendVenterValue.Trim().Length>0))	
					{
						//�����¼�
						clsBornVenterScaleExtendEventArgs objEventArgs=new clsBornVenterScaleExtendEventArgs();
						objEventArgs.objArgsValue=(clsVenterScaleExtendCol)objCurrentPoint;
						
						m_evnVenterScaleExtendMouseDown(sender,objEventArgs);
						break;

					}

				}
			}

			


			//ǩ���¼�
			if(m_evnSignNameMouseDown !=null 
				&& e.X <=(c_intTotalWidth+c_intLeftTextWidth) && e.X >=c_intLeftTextWidth
				&& e.Y<=c_intSignNameTotalHeight && e.Y >=c_intDealNoteTotalHeight)
			{
				for(int i=0;i<m_objBornScheduleEveryDay.m_arlSignNameCol.Count;i++)
				{
					clsSignNameCol objCurrentPoint=(clsSignNameCol)m_objBornScheduleEveryDay.m_arlSignNameCol[i];
						
					//					intXCorrd=objCurrentPoint.m_intHourValue*c_intGridWidth+ c_intLeftTextWidth;
					//
					//					if(Math.Abs(intXCorrd - e.X) <=c_intGridWidth && e.X>((objCurrentPoint.m_intHourValue-1)*c_intGridWidth+ c_intLeftTextWidth))
                    intXCorrd = (int)Math.Floor((double)e.X / c_intGridWidth);
					

					if(intXCorrd==objCurrentPoint.m_intHourValue && e.Y<c_intSignNameTotalHeight && e.Y>c_intDealNoteTotalHeight && objCurrentPoint.m_strSignNameID.Trim().Length>0)		
					{
						//�����¼�
						clsBornScheduleSignNameEventArgs objEventArgs=new clsBornScheduleSignNameEventArgs();
						objEventArgs.objArgsValue=(clsSignNameCol)objCurrentPoint;
						
						m_evnSignNameMouseDown(sender,objEventArgs);
						break;

					}

				}
			}
			
			
		}
		#endregion

		/// <summary>
		/// ˢ��
		/// </summary>
		public void m_mthRefreshDispaly()
		{
			this.Invalidate();
		}

		
}
		//���������¼�
		public class clsBornScheduleEveryHourEventArgs : EventArgs
		{
			public clsBornScheduleEveryHourCol objArgsValue;
		}

		public class clsBornScheduleCheckTimeEventArgs : EventArgs
		{
			public clsCheckTimeCol objArgsValue;
		}

		public class clsBornScheduleBloodPressureEventArgs : EventArgs
		{
			public clsBloodPressureCol objArgsValue;
		}

		public class clsBornScheduleEmbryoHeartEventArgs : EventArgs
		{
			public clsEmbryoHeartCol objArgsValue;
		}

		public class clsBornVenterScaleExtendEventArgs : EventArgs
		{
			public clsVenterScaleExtendCol objArgsValue;
		}

		public class clsBornScheduleExceptionNoteEventArgs : EventArgs
		{
			public clsExceptionNoteCol objArgsValue;
		}

		public class clsBornScheduleDealNoteEventArgs : EventArgs
		{
			public clsDealNoteCol objArgsValue;
		}

		public class clsBornScheduleSignNameEventArgs : EventArgs
		{
			public clsSignNameCol objArgsValue;
		}

	


}
