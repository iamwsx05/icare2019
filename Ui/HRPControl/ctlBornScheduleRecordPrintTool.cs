using System;
using System.Drawing ;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ctlBornScheduleRecordPrintTool 的摘要说明。
	/// </summary>
	public class ctlBornScheduleRecordPrintTool
	{

		#region user-defined variable
        private clsbornScheduleVO m_objbornvo;
		private  clsBornRecordManager m_objBornRecordManager ;
		private clsBornScheduleEveryDay m_objBornScheduleEveryDay;
		private DateTime m_mdtRecordDate;
		private int m_intMaxExceptionStringCount=0; //记录异常情况最大字符数
		private int m_intMaxDealStringCount=0; //记录处理记录最大字符数
		private int m_intExceptionHeight=0;
		private int m_intDealHeight=0;
		#endregion
		public ctlBornScheduleRecordPrintTool()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
            m_objbornvo = new clsbornScheduleVO();
			m_objBornRecordManager = new clsBornRecordManager();
			m_objBornScheduleEveryDay=new clsBornScheduleEveryDay(m_mdtRecordDate);

			m_mthInitVenterFlagImage();
			m_mthInitVenterPointImage();
			m_mthInitVenterPointBlueImage();
		}

		
		private void m_mthUpdateHeight()
		{
			//因异常情况和处理记录可能出现文字长短不一的情况,获取最长的一文本高度来画网格
			
			int intVariableExcepHeight=0;
			string strTemp="";
			Font fntTemp = new Font("",c_flt5PointFontSize);
			int intstrCount=0;
			clsExceptionNoteCol objExcep;
			clsDealNoteCol objDeal;

			//取出最长字符
			for(int i =0; i<m_objBornScheduleEveryDay.m_arlExceptionNoteCol.Count ;i++)
			{
				objExcep=(clsExceptionNoteCol)m_objBornScheduleEveryDay.m_arlExceptionNoteCol[i];
				if(objExcep.m_strExceptionNoteValue!=null && objExcep.m_strExceptionNoteValue!="")
				{
					if(objExcep.m_strExceptionNoteValue.Trim().Length>strTemp.Length)
						strTemp=objExcep.m_strExceptionNoteValue.Trim();
				}
			}

			//先计算异常高度能竖着放下几个字符,如果strTemp竖二排能放下就不变长,否则按字符多少增长
			intstrCount=Convert.ToInt32(c_intExceptionNoteHeight/fntTemp.GetHeight());
			if(strTemp.Length>intstrCount)
			{
				c_intExceptionNoteTotalHeight +=Convert.ToInt32((strTemp.Length-intstrCount)/2*fntTemp.GetHeight()*1.2); 
				m_intExceptionHeight=Convert.ToInt32((strTemp.Length-intstrCount)/2*fntTemp.GetHeight()*1.2); 
				m_intMaxExceptionStringCount =  Convert.ToInt32(strTemp.Length/2);
			}
			else
				m_intMaxExceptionStringCount =  Convert.ToInt32(c_intExceptionNoteHeight/fntTemp.GetHeight());

			//取出最长字符
			strTemp="";
			for(int i =0; i<m_objBornScheduleEveryDay.m_arlDealNoteCol.Count ;i++)
			{
				objDeal=(clsDealNoteCol)m_objBornScheduleEveryDay.m_arlDealNoteCol[i];
				if(objDeal.m_strDealNoteValue !=null  && objDeal.m_strDealNoteValue !="" )
				{
					if(objDeal.m_strDealNoteValue.Trim().Length>strTemp.Length)
						strTemp=objDeal.m_strDealNoteValue.Trim();
				}
			}
		

			//先计算异常高度能竖着放下几个字符,如果strTemp竖二排能放下就不变长,否则按字符多少增长
			intstrCount=0;
			intstrCount=Convert.ToInt32(c_intDealNoteHeight/fntTemp.GetHeight());
			if(strTemp.Length>intstrCount)
			{
				c_intDealNoteTotalHeight +=Convert.ToInt32((strTemp.Length-intstrCount)/2*fntTemp.GetHeight()*1.2)+m_intExceptionHeight; 
				m_intDealHeight=Convert.ToInt32((strTemp.Length-intstrCount)/2*fntTemp.GetHeight()*1.2); 
				m_intMaxDealStringCount =  Convert.ToInt32(strTemp.Length/2);
			}
			else
			{
				m_intMaxDealStringCount =  Convert.ToInt32(c_intDealNoteHeight/fntTemp.GetHeight());
				c_intDealNoteTotalHeight+=m_intExceptionHeight;
			}


//			c_intSignNameTotalHeight=c_intDealNoteTotalHeight+c_intSignNameHeight;
//			m_intTotalHeight=c_intSignNameTotalHeight;
			c_intSignNameTotalHeight += m_intExceptionHeight+m_intDealHeight;
			m_intTotalHeight = c_intSignNameTotalHeight;

		}
		public void m_mthPrintRecord(int p_intStartX,int p_intStartY,int p_intMaxHeight,System.Drawing.Printing.PrintPageEventArgs e,int p_intStartDateIndex,int p_intRecordLength,out bool p_blnHasMoreDate,out int p_intEndY,out int p_intLeftItem)
		{
			
			//先更新整个打印内容的高度,再画网格
			m_mthUpdateHeight();

			//判断TotalHeight是否大于p_intMaxHeight,如果刚好一页打印完则
			p_intLeftItem = int.MaxValue;  
			int intStartPrintX = p_intStartX;
			int intStartPrintY = p_intStartY;

			//打印设置的左边距与上边距来调整打印位置
			Graphics p_objGrp=(Graphics)e.Graphics;
		
			Pen penOneWidthLine = new Pen(m_clrGridLine);
			Pen penTwoWidthLine = new Pen(m_clrBorder,2);
			Pen penSpecialLine = new Pen(m_clrSpecialLine);
						
			SolidBrush bruTemp = new SolidBrush(m_clrDrawText);

			StringFormat stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			StringFormat stfFitBlackBox = new StringFormat(StringFormatFlags.FitBlackBox);

			Font fntRecordText = new Font("",c_flt7PointFontSize);
			Font fntSpeText = new Font("",c_flt6PointFontSize);
			Font fntCorrdinateText = new Font("",c_flt7PointFontSize);
			Font fntTitleText = new Font("",c_flt10PointFontSize,System.Drawing.FontStyle.Bold);
			Font fntLargeTitleText = new Font("",c_flt14PointFontSize,System.Drawing.FontStyle.Bold);
			Font fntPatientInfoText = new Font("",c_flt9PointFontSize);

			//画表头


            p_objGrp.DrawString(m_strHospitalName, fntTitleText, bruTemp, 155 + intStartPrintX, c_intTitleTopHeight + intStartPrintY);
			p_objGrp.DrawString("   产  程  进  展  图",fntLargeTitleText,bruTemp,185+intStartPrintX,c_intSecondTitleHeight+intStartPrintY);

			p_objGrp.DrawString("预产期",fntPatientInfoText,bruTemp,c_intLeftTextWidth+2+intStartPrintX,c_intFirstPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_objBornRecordManager.m_dtmFORECASTDATE.ToString("yyyy-MM-dd"),fntPatientInfoText,bruTemp,c_intLeftTextWidth+45+intStartPrintX,c_intFirstPatientHeight+intStartPrintY);

			p_objGrp.DrawString("床号",fntPatientInfoText,bruTemp,280+intStartPrintX,c_intFirstPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_strBedNo,fntPatientInfoText,bruTemp,310+intStartPrintX,c_intFirstPatientHeight+intStartPrintY);

			p_objGrp.DrawString("住院号",fntPatientInfoText,bruTemp,450+intStartPrintX,c_intFirstPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_objBornRecordManager.m_strINPATIENTID,fntPatientInfoText,bruTemp,500+intStartPrintX,c_intFirstPatientHeight+intStartPrintY);
	
			p_objGrp.DrawString("姓名",fntPatientInfoText,bruTemp,c_intLeftTextWidth+12+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_strWoman ,fntPatientInfoText,bruTemp,c_intLeftTextWidth+42+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);

			p_objGrp.DrawString("年龄",fntPatientInfoText,bruTemp,180+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_strAge,fntPatientInfoText,bruTemp,210+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);

			p_objGrp.DrawString("孕产次",fntPatientInfoText,bruTemp,315+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_objBornRecordManager.m_strPREGNANCYNUM,fntPatientInfoText,bruTemp,365+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);

			p_objGrp.DrawString("日期",fntPatientInfoText,bruTemp,465+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);
			p_objGrp.DrawString(m_objBornRecordManager.m_dtmOPENDATE.ToString("yyyy-MM-dd"),fntPatientInfoText,bruTemp,500+intStartPrintX,c_intSecondPatientHeight+intStartPrintY);

			#region  画格线与文本
			//画边框左右边缘留空白画文本,上面留5象素开始画框
			p_objGrp.DrawRectangle(penTwoWidthLine,c_intLeftTextWidth+intStartPrintX,c_intTopBankHeight-20+intStartPrintY,c_intTotalWidth-c_intGridWidth,c_intSignNameTotalHeight-100);//m_intTotalHeight-1);
			//			p_objGrp.DrawRectangle(penTwoWidthLine,c_intLeftTextWidth,c_intTopBankHeight,c_intTotalWidth-c_intGridWidth,m_intTotalHeight-1);

			//画10坐标TOP线
			p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+intStartPrintX,c_intTopBankHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intTopBankHeight+intStartPrintY);
			//画竖网格线
			for(int i=0;i<c_intColumnCount;i++)
			{
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth+intStartPrintX,c_intTopBankHeight+intStartPrintY,c_intLeftTextWidth+i*c_intGridWidth+intStartPrintX,c_intGridTotalHeight+intStartPrintY);		
			}

			//画横网格线
			for(int i=0;i<c_intGridHeightCount;i++)
			{
				if(i==4)
					p_objGrp.DrawLine(penSpecialLine,c_intLeftTextWidth+intStartPrintX,c_intTopBankHeight+(i+1)*c_intGridHeight+intStartPrintY,c_intTotalWidth+intStartPrintX,c_intTopBankHeight+(i+1)*c_intGridHeight+intStartPrintY);		
				else
					p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+intStartPrintX,c_intTopBankHeight+(i+1)*c_intGridHeight+intStartPrintY,c_intTotalWidth+intStartPrintX,c_intTopBankHeight+(i+1)*c_intGridHeight+intStartPrintY);		
			}

			//网格添加坐标值

			//画横坐标
			for(int i=1;i<c_intColumnCount-1;i++)
			{
				p_objGrp.DrawString(i.ToString(),fntCorrdinateText,bruTemp,c_intLeftTextWidth+i*c_intGridWidth-5+intStartPrintX,c_intGridTotalHeight+intStartPrintY);
			}

			////画横坐标0
			p_objGrp.DrawString("0",fntCorrdinateText,bruTemp,c_intLeftTextWidth+intStartPrintX,c_intGridTotalHeight+intStartPrintY);

			////画横坐标24
			p_objGrp.DrawString("24",fntCorrdinateText,bruTemp,c_intTotalWidth-13+intStartPrintX,c_intGridTotalHeight+intStartPrintY);//c_intTopBankHeight+
			

			//画左侧纵坐标
			for(int i=1;i<c_intGridHeightCount+1;i++)
			{
				p_objGrp.DrawString(i.ToString(),fntCorrdinateText,bruTemp,c_intLeftTextWidth+6+intStartPrintX,c_intGridTotalHeight-i*c_intGridHeight+5+intStartPrintY);
			}

			//画右侧纵坐标
			for(int i=1;i<c_intGridHeightCount+1;i++)
			{
				if(i<6)
					p_objGrp.DrawString(Convert.ToString(i-6),fntCorrdinateText,bruTemp,c_intTotalWidth-12+intStartPrintX,c_intGridTotalHeight-i*c_intGridHeight+5+intStartPrintY);
				else
					p_objGrp.DrawString("+"+Convert.ToString(i-5),fntCorrdinateText,bruTemp,c_intTotalWidth-14+intStartPrintX,c_intGridTotalHeight-i*c_intGridHeight+5+intStartPrintY);
			}

			//画左侧宫口文本
			p_objGrp.DrawString("宫口开大厘米 (",fntRecordText,bruTemp,c_intLeftBeginDrawWidth+intStartPrintX,c_intTopBankHeight+c_intGridHeight*2+intStartPrintY,stfDirectionVertical);

			//画红色的圈图标
			p_objGrp.DrawImage(m_imgVenterSymbol,c_intLeftBeginDrawWidth+3+intStartPrintX,c_intTopBankHeight+c_intGridHeight*2+c_flt7PointFontSize*11-2+intStartPrintY);
			p_objGrp.DrawString("红色标记)",fntRecordText,bruTemp,c_intLeftBeginDrawWidth+intStartPrintX,c_intTopBankHeight+c_intGridHeight*2+c_flt7PointFontSize*12+intStartPrintY,stfDirectionVertical);

			//画右侧胎头下降
			p_objGrp.DrawString("胎头下降",fntRecordText,bruTemp,c_intTotalWidth+3+intStartPrintX,c_intTopBankHeight+c_intGridHeight*2+intStartPrintY,stfDirectionVertical);

			//画右侧红色的圈图标
			p_objGrp.DrawImage(m_imgVenterSymbol,c_intTotalWidth+5+intStartPrintX,c_intGridTotalHeight-5*c_intGridHeight-2+intStartPrintY);
		
			//画中间空白部分的产程小时
			p_objGrp.DrawString("产程小时",fntRecordText,bruTemp,c_intTotalWidth-c_intGridWidth*7+intStartPrintX,c_intMiddleTapTotalHeight-22+intStartPrintY);

			//画中下部格线
			//画检查时间TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intMiddleTapTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intMiddleTapTotalHeight+intStartPrintY);

			//画检查时间文本
			p_objGrp.DrawString("检查",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5+intStartPrintX,c_intMiddleTapTotalHeight+2+intStartPrintY);
			p_objGrp.DrawString("时间",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5+intStartPrintX,c_intMiddleTapTotalHeight+12+intStartPrintY);

			//画血压TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intCheckTimeTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intCheckTimeTotalHeight+intStartPrintY);

			//画血压文本
			p_objGrp.DrawString("血压",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5+intStartPrintX,c_intCheckTimeTotalHeight+4+intStartPrintY);

			//画胎心TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intBloodPressureTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intBloodPressureTotalHeight+intStartPrintY);

			//画胎心文本
			p_objGrp.DrawString("胎心",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5+intStartPrintX,c_intBloodPressureTotalHeight+4+intStartPrintY);

			//画宫缩TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intEmbryoHeartTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intEmbryoHeartTotalHeight+intStartPrintY);

			//画宫缩文本
			p_objGrp.DrawString("宫缩",fntRecordText,bruTemp,c_intLeftBeginDrawWidth-5+intStartPrintX,c_intEmbryoHeartTotalHeight+4+intStartPrintY);

			//画异常情况TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intEvnterScaleTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intEvnterScaleTotalHeight+intStartPrintY);

			//画异常情况文本
			p_objGrp.DrawString("异常情况",fntRecordText,bruTemp,c_intLeftBeginDrawWidth+intStartPrintX,c_intEvnterScaleTotalHeight+6+intStartPrintY,stfDirectionVertical);

			//画处理记录TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intExceptionNoteTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intExceptionNoteTotalHeight+intStartPrintY);

			//画处理记录文本
			p_objGrp.DrawString("处理记录",fntRecordText,bruTemp,c_intLeftBeginDrawWidth+intStartPrintX,c_intExceptionNoteTotalHeight+6+intStartPrintY,stfDirectionVertical);

			//画签名TOP线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intDealNoteTotalHeight+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intDealNoteTotalHeight+intStartPrintY);

			//画签名Lower线
			p_objGrp.DrawLine(penOneWidthLine,0+intStartPrintX,c_intSignNameTotalHeight+2+intStartPrintY,c_intLeftTextWidth+c_intTotalWidth-c_intGridWidth+intStartPrintX,c_intSignNameTotalHeight+2+intStartPrintY);//m_intTotalHeight+13);


			//画签名文本
			p_objGrp.DrawString("签名",fntRecordText,bruTemp,c_intLeftBeginDrawWidth+intStartPrintX,c_intDealNoteTotalHeight+4+intStartPrintY,stfDirectionVertical);

			//画胎儿娩出标志文本
			p_objGrp.DrawString("胎儿娩出标记",fntRecordText,bruTemp,c_intTotalWidth+3+intStartPrintX,c_intBloodPressureTotalHeight+8+intStartPrintY,stfDirectionVertical);

			//画下面部分竖网格线
			for(int i=0;i<c_intColumnCount-1;i++)
			{
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth+intStartPrintX,c_intMiddleTapTotalHeight+intStartPrintY,c_intLeftTextWidth+i*c_intGridWidth+intStartPrintX,c_intSignNameTotalHeight+intStartPrintY);//m_intTotalHeight+c_intTopBankHeight);		
				
				//画斜血压对角线
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth+2+intStartPrintX,c_intBloodPressureTotalHeight-3+intStartPrintY,c_intLeftTextWidth+(i+1)*c_intGridWidth-3+intStartPrintX,c_intCheckTimeTotalHeight+2+intStartPrintY);		
				
				//画斜宫缩对角线
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+i*c_intGridWidth+2+intStartPrintX,c_intEvnterScaleTotalHeight-3+intStartPrintY,c_intLeftTextWidth+(i+1)*c_intGridWidth-3+intStartPrintX,c_intEmbryoHeartTotalHeight+2+intStartPrintY);		

			}

			//画3到9区间的红线
			p_objGrp.DrawLine(penSpecialLine,c_intLeftTextWidth+intStartPrintX,c_intTopBankHeight+c_intGridHeight*7+intStartPrintY,c_intLeftTextWidth+c_intGridWidth*9+intStartPrintX,c_intTopBankHeight+c_intGridHeight*7+intStartPrintY);
			p_objGrp.DrawLine(penSpecialLine,c_intLeftTextWidth+c_intGridWidth*9+intStartPrintX,c_intTopBankHeight+c_intGridHeight*7+intStartPrintY,c_intLeftTextWidth+c_intGridWidth*9+intStartPrintX,c_intTopBankHeight+c_intGridHeight*10+intStartPrintY);
		

			if(m_objBornRecordManager!=null)
			{
				//如用户设定则调用设定值,另则调默认值

				int[] intDefalutPointArr=(int[])m_thSetDefalutValue();
				
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[0]+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[1]+intStartPrintY,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[2]+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[3]+intStartPrintY);
				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[4]+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[5]+intStartPrintY,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[6]+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[7]+intStartPrintY);

				//画平行线两端点
				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[0]-4+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[1]-4+intStartPrintY);
				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[2]-4+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[3]-4+intStartPrintY);

				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[4]-4+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[5]-4+intStartPrintY);
				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[6]-4+intStartPrintX,c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[7]-4+intStartPrintY);

				//				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[0],c_intTopBankHeight+c_intGridHeight*(int)intDefalutPointArr[1],c_intLeftTextWidth+c_intGridWidth*(int)intDefalutPointArr[2],c_intTopBankHeight);
				//				p_objGrp.DrawLine(penOneWidthLine,c_intLeftTextWidth+c_intGridWidth*7,c_intTopBankHeight+c_intGridHeight*7,c_intLeftTextWidth+c_intGridWidth*13,c_intTopBankHeight);
				//
				//				//画平行线两端点
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*3-4,c_intTopBankHeight+c_intGridHeight*7-4);
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*9-4,c_intTopBankHeight-4);
				//
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*7-4,c_intTopBankHeight+c_intGridHeight*7-4);
				//				p_objGrp.DrawImage(m_imgVenterPoint,c_intLeftTextWidth+c_intGridWidth*13-4,c_intTopBankHeight-4);

			}

		

			#endregion
			
			//产程天数,大于1打印后面的页
			if(m_objBornRecordManager.m_arlBornScheduleEveryDay.Count >1)
			{
				p_blnHasMoreDate =true;
				p_intLeftItem=0;
			}
			else
			{
				p_blnHasMoreDate =false;
				p_intLeftItem=int.MaxValue;
			}


			//画数值
			#region

			//画宫口点
			int intXCorrd=0;
			int intYCorrd=0;
			int intPreXCorrd=0;
			int intPreYCorrd=0;
            if (m_objBornScheduleEveryDay != null)
            {
                if (m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol.Count > 0)
                {
                    for (int i = 0; i < m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol.Count; i++)
                    {
                        clsBornScheduleEveryHourCol objCurrentPoint = (clsBornScheduleEveryHourCol)m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol[i];

                        intXCorrd = objCurrentPoint.m_intHourValue;
                        intYCorrd = 10 - objCurrentPoint.m_intVenterValue;
                        //画点

                        SolidBrush bruTemp1 = new SolidBrush(m_clrDrawText);

                        Font fntRecordText1 = new Font("", c_flt7PointFontSize);

                        p_objGrp.DrawImage(m_imgVenterPointBlue, c_intLeftTextWidth + c_intGridWidth * intXCorrd - 4 + intStartPrintX, c_intTopBankHeight + c_intGridHeight * intYCorrd - 4 + intStartPrintY);


                        if (objCurrentPoint.m_bnlIsHavePreValue)
                        {
                            if (i > 0)
                            {
                                clsBornScheduleEveryHourCol objPrePoint = (clsBornScheduleEveryHourCol)m_objBornScheduleEveryDay.m_arlBornScheduleEveryHourCol[i - 1];
                                intPreXCorrd = objPrePoint.m_intHourValue;
                                intPreYCorrd = 10 - objPrePoint.m_intVenterValue;

                                p_objGrp.DrawLine(penOneWidthLine, c_intLeftTextWidth + c_intGridWidth * intPreXCorrd + intStartPrintX, c_intTopBankHeight + c_intGridHeight * intPreYCorrd + intStartPrintY, c_intLeftTextWidth + c_intGridWidth * intXCorrd - 1 + intStartPrintX, c_intTopBankHeight + c_intGridHeight * intYCorrd + intStartPrintY);
                            }
                        }
                    }
                }




                //画检查时间
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlCheckTimeCol.Count; i++)
                {
                    clsCheckTimeCol objCurrentValue = (clsCheckTimeCol)m_objBornScheduleEveryDay.m_arlCheckTimeCol[i];

                    p_objGrp.DrawString(objCurrentValue.m_strCheckTime, fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX, c_intMiddleTapTotalHeight + 4 + intStartPrintY);
                }

                //画血压
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlBloodPressureCol.Count; i++)
                {
                    clsBloodPressureCol objCurrentValue = (clsBloodPressureCol)m_objBornScheduleEveryDay.m_arlBloodPressureCol[i];

                    p_objGrp.DrawString(objCurrentValue.m_strScaleBloodPressureValue, fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX - 1, c_intCheckTimeTotalHeight + 2 + intStartPrintY);
                    p_objGrp.DrawString(objCurrentValue.m_strExtendBloodPressureValue, fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + 13 + intStartPrintX - 4, c_intBloodPressureTotalHeight - 13 + intStartPrintY);
                }


                //画胎心
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlEmbryoHeartCol.Count; i++)
                {
                    clsEmbryoHeartCol objCurrentValue = (clsEmbryoHeartCol)m_objBornScheduleEveryDay.m_arlEmbryoHeartCol[i];

                    p_objGrp.DrawString(objCurrentValue.m_strEmbryoHeartValue, fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX + 2, c_intBloodPressureTotalHeight + 3 + intStartPrintY);
                }

                //画宫缩
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlVenterScaleExtendCol.Count; i++)
                {
                    clsVenterScaleExtendCol objCurrentValue = (clsVenterScaleExtendCol)m_objBornScheduleEveryDay.m_arlVenterScaleExtendCol[i];

                    p_objGrp.DrawString(objCurrentValue.m_strScaleVenterValue, fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX - 1, c_intEmbryoHeartTotalHeight + 2 + intStartPrintY);
                    p_objGrp.DrawString(objCurrentValue.m_strExtendVenterValue, fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + 13 + intStartPrintX - 4, c_intEmbryoHeartTotalHeight + 10 + intStartPrintY);
                }



                //画异常情况,字符竖着放,从左到右放置
                int intHourValue = 0;
                string strTemp = null;
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlExceptionNoteCol.Count; i++)
                {
                    clsExceptionNoteCol objCurrentValue = (clsExceptionNoteCol)m_objBornScheduleEveryDay.m_arlExceptionNoteCol[i];

                    intHourValue = objCurrentValue.m_intHourValue;
                    if (objCurrentValue.m_strExceptionNoteValue != "" && objCurrentValue.m_strExceptionNoteValue != null)
                    {
                        //根据最长字符的高度来放置,只能画二排文字
                        strTemp = objCurrentValue.m_strExceptionNoteValue;
                        if (strTemp.Length > m_intMaxExceptionStringCount)
                        {
                            System.Drawing.Rectangle Rec = new Rectangle(c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX + 1, c_intEvnterScaleTotalHeight + 1 + intStartPrintY, 12, c_intExceptionNoteTotalHeight - c_intEvnterScaleTotalHeight + 120);
                            p_objGrp.DrawString(strTemp.Substring(0, m_intMaxExceptionStringCount), fntSpeText, bruTemp, Rec, stfFitBlackBox);
                            System.Drawing.Rectangle RecRight = new Rectangle(Convert.ToInt32(c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + fntRecordText.Size + 1 + intStartPrintX) + 3, c_intEvnterScaleTotalHeight + 1 + intStartPrintY, 12, c_intExceptionNoteTotalHeight - c_intEvnterScaleTotalHeight + 120);//m_intExceptionHeight+c_intExceptionNoteHeight);
                            p_objGrp.DrawString(strTemp.Substring(m_intMaxExceptionStringCount, strTemp.Length - m_intMaxExceptionStringCount), fntSpeText, bruTemp, RecRight, stfFitBlackBox);

                            //							p_objGrp.DrawString(strTemp.Substring(0,m_intMaxExceptionStringCount),fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+intStartPrintX,c_intEvnterScaleTotalHeight+1+intStartPrintY,stfDirectionVertical);
                            //							p_objGrp.DrawString(strTemp.Substring(m_intMaxExceptionStringCount,strTemp.Length -m_intMaxExceptionStringCount),fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+fntRecordText.Size+1+intStartPrintX,c_intEvnterScaleTotalHeight+1+intStartPrintY,stfDirectionVertical);
                        }
                        else
                        {
                            System.Drawing.Rectangle Rec = new Rectangle(c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX + 1, c_intEvnterScaleTotalHeight + 1 + intStartPrintY, 12, c_intExceptionNoteTotalHeight - c_intEvnterScaleTotalHeight + 120);
                            p_objGrp.DrawString(strTemp, fntRecordText, bruTemp, Rec, stfFitBlackBox);
                            //							p_objGrp.DrawString(strTemp,fntRecordText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+intStartPrintX,c_intEvnterScaleTotalHeight+1+intStartPrintY,stfDirectionVertical);
                        }
                    }
                }

                //画处理记录,字符竖着放,从左到右放置
                intHourValue = 0;
                strTemp = null;
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlDealNoteCol.Count; i++)
                {
                    clsDealNoteCol objCurrentValue = (clsDealNoteCol)m_objBornScheduleEveryDay.m_arlDealNoteCol[i];

                    intHourValue = objCurrentValue.m_intHourValue;
                    if (objCurrentValue.m_strDealNoteValue != "" && objCurrentValue.m_strDealNoteValue != null)
                    {
                        //根据最长字符的高度来放置,只能画二排文字
                        strTemp = objCurrentValue.m_strDealNoteValue;
                        if (strTemp.Length > m_intMaxDealStringCount)
                        {
                            System.Drawing.Rectangle Rec = new Rectangle(c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX, c_intExceptionNoteTotalHeight + 1 + intStartPrintY, 12, c_intDealNoteTotalHeight - c_intExceptionNoteTotalHeight + 120);
                            p_objGrp.DrawString(strTemp.Substring(0, m_intMaxDealStringCount), fntSpeText, bruTemp, Rec, stfFitBlackBox);
                            System.Drawing.Rectangle RecRight = new Rectangle(Convert.ToInt32(c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + fntRecordText.Size + 1 + intStartPrintX) + 2, c_intExceptionNoteTotalHeight + 1 + intStartPrintY, 12, c_intDealNoteTotalHeight - c_intExceptionNoteTotalHeight + 120);//m_intDealHeight+c_intDealNoteHeight);
                            p_objGrp.DrawString(strTemp.Substring(m_intMaxDealStringCount, strTemp.Length - m_intMaxDealStringCount), fntSpeText, bruTemp, RecRight, stfFitBlackBox);

                            //							p_objGrp.DrawString(strTemp.Substring(0,m_intMaxDealStringCount),fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+intStartPrintX,c_intExceptionNoteTotalHeight+1+intStartPrintY,stfDirectionVertical);
                            //							p_objGrp.DrawString(strTemp.Substring(m_intMaxDealStringCount,strTemp.Length -m_intMaxDealStringCount),fntSpeText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+fntRecordText.Size+1+intStartPrintX,c_intExceptionNoteTotalHeight+1+intStartPrintY,stfDirectionVertical);
                            //					
                        }
                        else
                        {
                            System.Drawing.Rectangle Rec = new Rectangle(c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX, c_intExceptionNoteTotalHeight + 1 + intStartPrintY, 12, c_intDealNoteTotalHeight - c_intExceptionNoteTotalHeight + 120);
                            p_objGrp.DrawString(strTemp, fntRecordText, bruTemp, Rec, stfFitBlackBox);
                            //							p_objGrp.DrawString(strTemp,fntRecordText,bruTemp,c_intLeftTextWidth+c_intGridWidth*(objCurrentValue.m_intHourValue-1)+intStartPrintX,c_intExceptionNoteTotalHeight+1+intStartPrintY,stfDirectionVertical);
                        }
                    }
                }


                //画签名
                for (int i = 0; i < m_objBornScheduleEveryDay.m_arlSignNameCol.Count; i++)
                {
                    clsSignNameCol objCurrentValue = (clsSignNameCol)m_objBornScheduleEveryDay.m_arlSignNameCol[i];
                    string strLast = null;
                    p_objGrp.DrawString(m_thSplitName(objCurrentValue.m_strSignNameID, out strLast), fntSpeText, bruTemp, c_intLeftTextWidth + c_intGridWidth * (objCurrentValue.m_intHourValue - 1) + intStartPrintX, c_intDealNoteTotalHeight + 5 + intStartPrintY, stfDirectionVertical);
                }

            }
			#endregion
		//画评分表
            p_objGrp.DrawLine(Pens.Black, 100, 1000, 750, 1000);
            p_objGrp.DrawLine(Pens.Black, 100, 975, 750, 975);
            p_objGrp.DrawLine(Pens.Black, 100, 950, 500, 950);
            p_objGrp.DrawLine(Pens.Black, 100, 925, 500, 925);
            p_objGrp.DrawLine(Pens.Black, 100, 900, 500, 900);
            p_objGrp.DrawLine(Pens.Black, 100, 875, 500, 875);
            p_objGrp.DrawLine(Pens.Black, 100, 850, 750, 850);
            p_objGrp.DrawLine(Pens.Black, 100, 825, 750, 825);
            p_objGrp.DrawLine(Pens.Black, 100, 800, 750, 800);
            //画竖的
            p_objGrp.DrawLine(Pens.Black, 100, 800, 100, 1000);
            p_objGrp.DrawLine(Pens.Black, 200, 825, 200, 1000);
            p_objGrp.DrawLine(Pens.Black, 300, 825, 300, 975);
            p_objGrp.DrawLine(Pens.Black, 400, 825, 400, 975);
            p_objGrp.DrawLine(Pens.Black, 500, 825, 500, 1000);
            p_objGrp.DrawLine(Pens.Black, 583, 825, 583, 1000);
            p_objGrp.DrawLine(Pens.Black, 666, 825, 666, 1000);
            p_objGrp.DrawLine(Pens.Black, 750, 800, 750, 1000);
            //画字
            p_objGrp.DrawString("Apgar评分表", fntPatientInfoText, bruTemp,350,800);
            p_objGrp.DrawString("体征", fntPatientInfoText, bruTemp, 100, 825);
            p_objGrp.DrawString("心率/分", fntPatientInfoText, bruTemp, 100, 850);
            p_objGrp.DrawString("呼吸情况", fntPatientInfoText, bruTemp, 100, 875);
            p_objGrp.DrawString("肌张力", fntPatientInfoText, bruTemp, 100, 900);
            p_objGrp.DrawString("弹足底插管", fntPatientInfoText, bruTemp, 100, 925);
            p_objGrp.DrawString("皮肤颜色", fntPatientInfoText, bruTemp, 100, 950);
            p_objGrp.DrawString("总分", fntPatientInfoText, bruTemp, 100, 975);

          //  p_objGrp.DrawString("Apgar评分表", fntPatientInfoText, bruTemp, 350, 825);
            p_objGrp.DrawString("0分", fntPatientInfoText, bruTemp, 200, 825);
            p_objGrp.DrawString("无", fntPatientInfoText, bruTemp, 200, 850);
            p_objGrp.DrawString("无", fntPatientInfoText, bruTemp, 200, 875);
            p_objGrp.DrawString("松弛", fntPatientInfoText, bruTemp, 200, 900);
            p_objGrp.DrawString("无反应", fntPatientInfoText, bruTemp, 200, 925);
            p_objGrp.DrawString("青紫苍白", fntPatientInfoText, bruTemp, 200, 950);

            p_objGrp.DrawString("1分", fntPatientInfoText, bruTemp, 300, 825);
            p_objGrp.DrawString("<100", fntPatientInfoText, bruTemp, 300, 850);
            p_objGrp.DrawString("浅 哭声小", fntPatientInfoText, bruTemp, 300, 875);
            p_objGrp.DrawString("四肢屈曲", fntPatientInfoText, bruTemp, 300, 900);
            p_objGrp.DrawString("蹙颜 徽动", fntPatientInfoText, bruTemp, 300, 925);
            p_objGrp.DrawString("体红肢紫", fntPatientInfoText, bruTemp, 300, 950);

            p_objGrp.DrawString("2分", fntPatientInfoText, bruTemp, 400, 825);
            p_objGrp.DrawString(">100", fntPatientInfoText, bruTemp, 400, 850);
            p_objGrp.DrawString("佳 哭声响", fntPatientInfoText, bruTemp, 400,875);
            p_objGrp.DrawString("四肢活动", fntPatientInfoText, bruTemp, 400, 900);
            p_objGrp.DrawString("哭,咳,嚏", fntPatientInfoText, bruTemp, 400, 925);
            p_objGrp.DrawString("全身红", fntPatientInfoText, bruTemp, 400, 950);
            p_objGrp.DrawString("一评", fntPatientInfoText, bruTemp, 500, 825);
            p_objGrp.DrawString("即刻", fntPatientInfoText, bruTemp, 500, 875);
            p_objGrp.DrawString("二评", fntPatientInfoText, bruTemp, 583, 825);
            p_objGrp.DrawString("一分钟", fntPatientInfoText, bruTemp, 583, 875);
            p_objGrp.DrawString("三评", fntPatientInfoText, bruTemp, 666, 825);
            p_objGrp.DrawString("五分钟", fntPatientInfoText, bruTemp, 666, 875);
            if (m_objbornvo != null)
            {
              if(m_objbornvo.m_strHEART=="1")
                p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 350, 850);
              if(m_objbornvo.m_strHEART=="2")
                     p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 450, 850);
              if (m_objbornvo.m_strHEART == "0")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 230, 850);
               if (m_objbornvo.m_strBREATH == "1")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 370, 875);
               if (m_objbornvo.m_strBREATH == "2")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 470, 875);
               if (m_objbornvo.m_strBREATH == "0")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 230, 875);

               if (m_objbornvo.m_strPOWER == "1")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 370, 900);
               if (m_objbornvo.m_strPOWER == "2")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 470, 900);
               if (m_objbornvo.m_strPOWER == "0")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 250, 900);

               if (m_objbornvo.m_strFOOT == "1")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 370, 925);
               if (m_objbornvo.m_strFOOT == "2")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 470, 925);
               if (m_objbornvo.m_strFOOT == "0")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 250, 925);

               if (m_objbornvo.m_strSKIN == "1")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 370, 950);
               if (m_objbornvo.m_strSKIN == "2")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 470, 950);
               if (m_objbornvo.m_strSKIN == "0")
                   p_objGrp.DrawString("√", fntPatientInfoText, bruTemp, 270, 950);
               p_objGrp.DrawString(m_objbornvo.m_strtotal, fntPatientInfoText, bruTemp, 350, 975);
               p_objGrp.DrawString(m_objbornvo.m_strSOON, fntPatientInfoText, bruTemp, 510, 975);
               p_objGrp.DrawString(m_objbornvo.m_strONEMINI, fntPatientInfoText, bruTemp, 593, 975);
               p_objGrp.DrawString(m_objbornvo.m_strFIVEMINI, fntPatientInfoText, bruTemp, 676, 975);
            }
		p_intEndY=0;
		 

		}
		private string m_thSplitName(string p_strOriginal,out string p_strLast)
		{
			if(p_strOriginal==null || p_strOriginal=="")
			{
				p_strLast=null;
				return null;
			}

			//strTemp的文本的格式如:"A3|3" 如"3|3"系统会转换为2005-03-03 00:00:00状态在clsXML_SQL_Converter 
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
		#region 属性

		#region 当前用户
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
        /// 医院名
        /// </summary>
        private string m_strHospitalName;

        /// <summary>
        /// 医院名的设置和获取
        /// </summary>
        public string m_StrHospitalName
        {
            get
            {
                return m_strHospitalName;
            }
            set
            {
                m_strHospitalName = value;
            }
        }
		#endregion
		#endregion
		
		#region  画表格的一些变量 
		
		
		
		#region  //各种字体大小的数值
		private const float c_flt12PointFontSize = 12f;
		private const float c_flt10PointFontSize = 16.5f;

		private const float c_flt9PointFontSize = 9f;
		private const float c_flt7PointFontSize = 7.5f;
		private const float c_flt6PointFontSize = 6.75f;
		private const float c_flt5PointFontSize = 5.25f;
		private const float c_flt14PointFontSize = 18.25f;

		#endregion

		

		#region   //各种组成部分的高度
		private const int c_intTopBankHeight=122;
		private const int c_intTitleTopHeight=10;
		private const int c_intFirstTitleHeight=18+10;
		private const int c_intSecondTitleHeight=18+18+10;
		private const int c_intFirstPatientHeight=28+28+28;
		private const int c_intSecondPatientHeight=28+28+28+20;

		private const int c_intGridHeight = 25; //格子高
		private const int c_intGridWidth = 23;  //格子宽
		private const int c_intGridHeightCount = 10;
		private int c_intGridTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight;

		private const int c_intMiddleTapHeight=53;  //中间最宽的空白部分
		private int c_intMiddleTapTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight;

		private const int c_intCheckTimeHeight=30;
		private int c_intCheckTimeTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight;

		private const int c_intBloodPressureHeight=24;
		private int c_intBloodPressureTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight;

		private const int c_intEmbryoHeartHeight=24;
		private int c_intEmbryoHeartTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight;

		private const int c_intEvnterScaleHeight=24;
		private int c_intEvnterScaleTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight;

		private const int c_intExceptionNoteHeight=70;
		private int c_intExceptionNoteTotalHeight = c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight;


		private const int c_intDealNoteHeight=90;
		private int c_intDealNoteTotalHeight =  c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight+c_intDealNoteHeight;

		private const int c_intSignNameHeight=50;
		private int c_intSignNameTotalHeight =  c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight+c_intDealNoteHeight+c_intSignNameHeight;

		/// 总共的高度
		/// </summary>
		private int m_intTotalHeight= c_intGridHeight*c_intGridHeightCount +c_intTopBankHeight+c_intMiddleTapHeight+c_intCheckTimeHeight+c_intBloodPressureHeight+c_intEmbryoHeartHeight+c_intEvnterScaleHeight+c_intExceptionNoteHeight+c_intDealNoteHeight+c_intSignNameHeight-100;

		//左右两侧文本的宽度
		private const int c_intLeftTextWidth=25;
		private const int c_intRightTextWidth=15;
		private const int c_intLeftBeginDrawWidth=5;

		//总宽
		private const int c_intColumnCount = 25;
		private  int c_intTotalWidth=c_intGridWidth*c_intColumnCount;
		
		#endregion

		
		#region //画表格用的颜色

		//外框颜色
		private Color m_clrBorder=Color.Black ;

		public Color m_ClrBorder
		{
			set 
			{
				m_clrBorder=value;
				
			}

			get
			{
				return m_clrBorder;
			}
		}

		//格线颜色
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
				
			}

		}

		//特殊线或圆圈颜色
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
				
			}

		}


		//文本的颜色
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
				
			}

		}

		//连接线颜色
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
				
			}

		}




		#endregion 

		#region  //宫口开标志
		private Image m_imgVenterSymbol  = new Bitmap(10,10);
		private void m_mthInitVenterFlagImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgVenterSymbol);			
			gphImage.DrawEllipse(new Pen(m_clrSpecialLine,1),0,0,9,9);

			gphImage.Dispose();
		}
		//画线用的点
		private Image m_imgVenterPoint  = new Bitmap(8,8);
		private void m_mthInitVenterPointImage()
		{
			Graphics gphImage = Graphics.FromImage(m_imgVenterPoint);			
			gphImage.FillEllipse(new SolidBrush(Color.Red),0,0,8,8);

			gphImage.Dispose();
		}

		//蓝色标记的点
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
			
			}
		}

		#endregion

		#endregion
		

		//控件对外接口
		//当天记录
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
        /// <summary>
        /// 平分表
        /// </summary>
        public clsbornScheduleVO m_clsBornvo
        {
            set
            {
                m_objbornvo = value;
            }
            get
            {
                return m_objbornvo;
            }
        }
		//产女记录
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

		private int m_thSplitString(string p_strOriginal,out int p_strLast)
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

		private int[] m_thSetDefalutValue()
		{
			//孕产次,预产期,默认四点等赋值,
			

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
			
			intFirstLeft=m_thSplitString(strTempVlaue,out strLast);//第一点
			intFirstRight =strLast;//第一点

			strTempVlaue=m_objBornRecordManager.m_strSECONDPOINT==null?null: m_objBornRecordManager.m_strSECONDPOINT.Trim();
			intSecondLeft = m_thSplitString(strTempVlaue,out strLast);//第二点
			intSecondRight =strLast;

			strTempVlaue=m_objBornRecordManager.m_strTHREEPOINT==null?null: m_objBornRecordManager.m_strTHREEPOINT.Trim();
			intThreeLeft = m_thSplitString(strTempVlaue,out strLast);//第三点
			intThreeRight  =strLast;

			strTempVlaue=m_objBornRecordManager.m_strFOUTPOINT==null?null: m_objBornRecordManager.m_strFOUTPOINT.Trim();;//第四点
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

			try
			{
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
			}
			catch
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

		private string m_strBedNo=null; //床号
		public string m_StrBedNo
		{
			set { m_strBedNo = value;}
			get { return m_strBedNo ;}
		}
		private string m_strWoman=null; //姓名 
		public string m_StrWoman
		{
			set { m_strWoman = value;}
			get { return m_strWoman ;}
		}
		private string m_strAge = null; //年龄
		public string m_StrAge
		{
			set { m_strAge = value;}
			get { return m_strAge ;}
		}




	}
}
