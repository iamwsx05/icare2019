using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace com.digitalwave.Utility.Controls
{
	public enum enmPrintType
	{
		AllCorrect,
		CorrectBefore,
		None
	}

	/// <summary>
	/// Summary description for clsPrintRichTextContext.
	/// </summary>
	public class clsPrintRichTextContext
	{
		private enmPrintType m_enmPrintType;

		public enmPrintType m_enmCurrentPrintType
		{
			get
			{
				return m_enmPrintType;
			}
		}		

		private Color m_clrBaseColor;

		private Font m_fntCharFont;

		public Font m_FntCharFont
		{
			get
			{
				return m_fntCharFont;
			}
			set
			{
				m_fntCharFont = value;
			}
		}

		private Font m_fntScriptFont;

		private Font m_fntDescFont;

		private string m_strText;

		/// <summary>
		/// 开始打印的字符索引
		/// </summary>
		private int m_intCurrentIndex;

		public int m_IntCurrentIndex
		{
			get
			{
				return m_intCurrentIndex;
			}
		}

		private ctlRichTextBox.clsDSTInfo[] m_objDSTInfoArr;

		private ctlRichTextBox.clsUserContentInfo[] m_objContentInfoArr;

		private ctlRichTextBox.clsModifyUserInfo[] m_objModifyUserInfoArr;

		private ctlRichTextBox.clsSuperSubScript[] m_objSuperSubScriptArr;

		public ctlRichTextBox.clsModifyUserInfo[] m_ObjModifyUserArr
		{
			get
			{
				return m_objModifyUserInfoArr;
			}
		}

		private int m_intCurrentDSTIndex;

		private ArrayList m_arlTemp;

		private bool m_blnPrintModifySign = false;

		public bool m_BlnPrintModifySign
		{
			get
			{
				return m_blnPrintModifySign;
			}
			set
			{
				m_blnPrintModifySign = value;
			}
		}

		/// <summary>
		/// 全局控制，标记双划线是否下双划线
		/// </summary>
		private bool m_blnUnderLineDST = false;

		/// <summary>
		/// 全局控制双划线是否下双划线
		/// </summary>
		public bool m_BlnUnderLineDST
		{
			get
			{
				return m_blnUnderLineDST;
			}
			set
			{
				m_blnUnderLineDST = value;
			}
		}

		public clsPrintRichTextContext(Color p_clrBaseColor,Font p_fntCharFont)
		{
			m_enmPrintType = enmPrintType.None;

			m_strText = "";

			m_intCurrentIndex = 0;

			m_objDSTInfoArr = null;
			m_objContentInfoArr = null;

			m_intCurrentDSTIndex = 0;

			m_clrBaseColor = p_clrBaseColor;

			m_fntCharFont = (p_fntCharFont == null?new Font("SimSun",11):p_fntCharFont);

			m_fntDescFont = new Font("",5f);

			m_fntScriptFont = new Font(m_fntCharFont.FontFamily,ctlRichTextBox.c_intSuperSubScriptSize);

			m_arlTemp = new ArrayList();
		}

		public void m_mthSetContextWithAllCorrect(string p_strText,string p_strXml)
		{
			m_strText = ctlRichTextBox.s_strGetRightText(p_strText,p_strXml);

			m_intCurrentIndex = 0;

			m_intCurrentDSTIndex = 0;

			m_enmPrintType = enmPrintType.AllCorrect;
		}

		public void m_mthSetContextWithCorrectBefore(string p_strText,string p_strXml,DateTime p_dtmSeparateTime)
		{
			m_mthSetContextWithCorrectBefore(p_strText,p_strXml,p_dtmSeparateTime,false);
		}

		public void m_mthSetContextWithCorrectBefore(string p_strText,string p_strXml,DateTime p_dtmSeparateTime,bool p_blnPrintModifySign)
		{
			m_strText = ctlRichTextBox.s_strGetRightText(p_strText,p_strXml,p_dtmSeparateTime,out m_objDSTInfoArr,out m_objContentInfoArr,out m_objModifyUserInfoArr,out m_objSuperSubScriptArr);

			#region 将上下标替换为空格
			if(m_objSuperSubScriptArr != null && m_objSuperSubScriptArr.Length > 0)
			{
				for(int i = 0; i < m_objSuperSubScriptArr.Length; i++)
				{
					m_strText = m_strText.Remove(m_objSuperSubScriptArr[i].m_intIndex,m_objSuperSubScriptArr[i].m_strValue.Length);
					string strBlank = "";
					for(int j = 0; j < m_objSuperSubScriptArr[i].m_strValue.Length; j++)
						strBlank += " ";
					m_strText = m_strText.Insert(m_objSuperSubScriptArr[i].m_intIndex,strBlank);
				}
			}
			#endregion

			m_intCurrentIndex = 0;

			m_intCurrentDSTIndex = 0;

			m_enmPrintType = enmPrintType.CorrectBefore;

			m_blnPrintModifySign = p_blnPrintModifySign;
		}

        public void m_mthSetContextWithCorrectBefore(string p_strText, string p_strXml, DateTime p_dtmSeparateTime,  Color p_clrCustomSet)
        {
            m_mthSetContextWithCorrectBefore(p_strText, p_strXml, p_dtmSeparateTime);

            if (m_objContentInfoArr != null && m_objContentInfoArr.Length > 0)
            {
                for (int i = 0; i < m_objContentInfoArr.Length; i++)
                {
                    m_objContentInfoArr[i].objUserInfo.m_clrText = p_clrCustomSet;
                }
            }
        }

		public void m_mthRestartPrint()
		{
			m_intCurrentIndex = 0;

			m_intCurrentDSTIndex = 0;
		}

		public bool m_BlnHaveNextLine()
		{
			return m_intCurrentIndex <= m_strText.Length-1;
		}		

		public void m_mthPrintLine(int p_intX,int p_intY,Graphics p_objGrp,int p_intCharPerLine)
		{
			switch(m_enmPrintType)
			{
				case enmPrintType.AllCorrect:
					m_mthPrintAllCorrectLine(p_intX,p_intY,p_objGrp,p_intCharPerLine);
					break;
				case enmPrintType.CorrectBefore:
					m_mthPrintCorrectBeforeLine(p_intX,p_intY,p_objGrp,p_intCharPerLine);
					break;
			}
		}

		public void m_mthPrintLine(int p_intWidth,int p_intX,int p_intY,Graphics p_objGrp)
		{
			switch(m_enmPrintType)
			{
				case enmPrintType.AllCorrect:
					m_mthPrintAllCorrectLine(p_intWidth,p_intX,p_intY,p_objGrp);
					break;
				case enmPrintType.CorrectBefore:
					m_mthPrintCorrectBeforeLine(p_intWidth,p_intX,p_intY,p_objGrp);
					break;
			}
		}

		private void m_mthPrintAllCorrectLine(int p_intWidth,int p_intX,int p_intY,Graphics p_objGrp)
		{
			int intWidth = p_intWidth;

			if(!m_BlnHaveNextLine())
				return;

			SolidBrush bruText = new SolidBrush(m_clrBaseColor);			

			string strLineText = m_strText.Substring(m_intCurrentIndex);

			string []strSeparaterTextArr = strLineText.Split('\n');

			strLineText = strSeparaterTextArr[0];

			if(strLineText.EndsWith("\r"))
				strLineText = strLineText.Substring(0,strLineText.Length-1);

			if(strLineText.Length == 0)
			{
				m_intCurrentIndex++;
					
				if(strSeparaterTextArr.Length == 1)
				{
					//只有一个换行符
					m_intCurrentIndex++;
					return;
				}
				else
				{
					strLineText = strSeparaterTextArr[1];
					
				}
			}

			string strTempText = strLineText;

			SizeF szfText = p_objGrp.MeasureString(strLineText,m_fntCharFont);
			while(szfText.Width > intWidth)
			{
				int intLen = (int)(((float)intWidth/szfText.Width)*(float)strLineText.Length);

				strLineText = strLineText.Substring(0,intLen);

				szfText = p_objGrp.MeasureString(strLineText,m_fntCharFont);
			}

			if(szfText.Width < intWidth)
			{
				while(strLineText.Length < strTempText.Length -1
					&& szfText.Width < intWidth)
				{
					strLineText = strTempText.Substring(0,strLineText.Length+1);

					szfText = p_objGrp.MeasureString(strLineText,m_fntCharFont);
				}
			}

			if(szfText.Width > intWidth)
			{
				strLineText = strLineText.Substring(0,strLineText.Length-1);
			}

			p_objGrp.DrawString(strLineText,m_fntCharFont,bruText,p_intX,p_intY);

			m_intCurrentIndex += strLineText.Length;
		}

		private void m_mthPrintCorrectBeforeLine(int p_intWidth,int p_intX,int p_intY,Graphics p_objGrp)
		{
			int intWidth = p_intWidth;

			if(!m_BlnHaveNextLine())
				return;

			SolidBrush bruText = new SolidBrush(m_clrBaseColor);			

			string strLineText = m_strText.Substring(m_intCurrentIndex);

			string []strSeparaterTextArr = strLineText.Split('\n');

			strLineText = strSeparaterTextArr[0];

			if(strLineText.EndsWith("\r") || strLineText.EndsWith("\n"))
				strLineText = strLineText.Substring(0,strLineText.Length-1);

			if(strLineText.Length == 0)
			{
				m_intCurrentIndex++;
					
//				if(strSeparaterTextArr.Length == 1)
//				{
//					//只有一个换行符
//					m_intCurrentIndex++;
//					return;
//				}
//				else
//				{
//					strLineText = strSeparaterTextArr[1];
					return;					
//				}
			}

			string strTempText = strLineText;

			SizeF szfText = p_objGrp.MeasureString(strLineText,m_fntCharFont);
			while(szfText.Width > intWidth)
			{
				int intLen = (int)(((float)intWidth/szfText.Width)*(float)strLineText.Length);

				strLineText = strLineText.Substring(0,intLen);

				szfText = p_objGrp.MeasureString(strLineText,m_fntCharFont);
			}

			if(szfText.Width < intWidth)
			{
				while(strLineText.Length < strTempText.Length -1
					&& szfText.Width < intWidth)
				{
					strLineText = strTempText.Substring(0,strLineText.Length+1);

					szfText = p_objGrp.MeasureString(strLineText,m_fntCharFont);
				}
			}

			if(szfText.Width > intWidth)
			{
				strLineText = strLineText.Substring(0,strLineText.Length-1);
			}

			m_mthPrintCorrectBeforeLine(p_intX,p_intY,p_objGrp,strLineText.Length);
		}

		private void m_mthPrintAllCorrectLine(int p_intX,int p_intY,Graphics p_objGrp,int p_intCharPerLine)
		{
			if(!m_BlnHaveNextLine())
				return;

			SolidBrush bruText = new SolidBrush(m_clrBaseColor);			

			if(m_intCurrentIndex+p_intCharPerLine > m_strText.Length)
				p_intCharPerLine = m_strText.Length-m_intCurrentIndex;
		
			string strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine);

			string []strSeparaterTextArr = strLineText.Split('\n');

			strLineText = strSeparaterTextArr[0];

			p_intCharPerLine = strLineText.Length;

			if(p_intCharPerLine == 0)
			{
				m_intCurrentIndex++;
					
				if(strSeparaterTextArr.Length == 1)
				{
					//只有一个换行符
					m_intCurrentIndex++;
					return;
				}
				else
				{
					strLineText = strSeparaterTextArr[1];

					p_intCharPerLine = strLineText.Length;
				}
			}

			p_objGrp.DrawString(strLineText,m_fntCharFont,bruText,p_intX,p_intY);

			m_intCurrentIndex += p_intCharPerLine;
		}

		private void m_mthPrintCorrectBeforeLine(int p_intX,int p_intY,Graphics p_objGrp,int p_intCharPerLine)
		{
			if(!m_BlnHaveNextLine())
				return;

//			if(m_blnPrintModifySign)
//			{
//				p_intY += 10;
//			}

			string strLineText = "";
			enmCharType enm = enmCharType.Normal;

			if(m_intCurrentIndex+p_intCharPerLine > m_strText.Length)
				p_intCharPerLine = m_strText.Length-m_intCurrentIndex;
			else if(m_intCurrentIndex+p_intCharPerLine < m_strText.Length)
				 enm = clsJudgeCharType.s_enmGetCharType(m_strText[m_intCurrentIndex+p_intCharPerLine]);			
			switch(enm)
			{
				case enmCharType.Punctuation :
					if(char.IsNumber(m_strText[m_intCurrentIndex+p_intCharPerLine-1]) && m_strText[m_intCurrentIndex+p_intCharPerLine]=='.')
					{
						strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine);
						clsJudgeCharType.s_mthMoveLastNumbersOrDot(ref strLineText);
						break;

					}
					strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine+1);
					break;
				case enmCharType.Letter :
					strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine);
					clsJudgeCharType.s_mthMoveLastLetters(ref strLineText);
					break;
				case enmCharType.Number :
					strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine);
					clsJudgeCharType.s_mthMoveLastNumbersOrDot(ref strLineText);
					break;
				default :
					strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine);
					break;
			}

			string []strSeparaterTextArr = strLineText.Split('\n');

			strLineText = strSeparaterTextArr[0];			

			p_intCharPerLine = strLineText.Length;

			if(p_intCharPerLine == 0)
			{
				m_intCurrentIndex++;

				if(m_intCurrentIndex >= m_strText.Length)
				{
					return;
				}
					
				if(strSeparaterTextArr.Length == 1)
				{
					//只有一个换行符
					m_intCurrentIndex++;
					return;
				}
				else
				{
					strLineText = strSeparaterTextArr[1];

					p_intCharPerLine = strLineText.Length;
				}
			}


			int intEndIndex = m_intCurrentIndex+p_intCharPerLine-1;

			SolidBrush bruText = new SolidBrush(m_clrBaseColor);	

			CharacterRange []rgnInsertArr = new CharacterRange[1];
			rgnInsertArr[0] = new CharacterRange(0,0);	
			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			

			RectangleF rtfText = new RectangleF(p_intX,p_intY,10000,100);
			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			#region Handle Insert
			bool blnFirstGet = true;
			
			for(int i=0;i<m_objContentInfoArr.Length;i++)
			{
				ctlRichTextBox.clsUserContentInfo objContentInfo = m_objContentInfoArr[i];

				if(blnFirstGet)
				{
					if(m_intCurrentIndex >= objContentInfo.m_intStartIndex
						&& m_intCurrentIndex <= objContentInfo.m_intEndIndex)
					{
						m_arlTemp.Add(objContentInfo);

						if(intEndIndex >= objContentInfo.m_intStartIndex
							&& intEndIndex <= objContentInfo.m_intEndIndex)
						{
							break;
						}

						blnFirstGet = false;
					}
				}
				else
				{
					m_arlTemp.Add(objContentInfo);

					if(intEndIndex >= objContentInfo.m_intStartIndex
						&& intEndIndex <= objContentInfo.m_intEndIndex)
					{
						break;
					}
				}
			}

			ctlRichTextBox.clsUserContentInfo [] objContentArr = 
				(ctlRichTextBox.clsUserContentInfo [])m_arlTemp.ToArray(typeof(ctlRichTextBox.clsUserContentInfo));
			m_arlTemp.Clear();

			if(objContentArr !=null && objContentArr.Length>0)
			{
				int intTempStart = objContentArr[0].m_intStartIndex;
				objContentArr[0].m_intStartIndex = m_intCurrentIndex;
				int intTempEnd = objContentArr[objContentArr.Length-1].m_intEndIndex;
				objContentArr[objContentArr.Length-1].m_intEndIndex = intEndIndex;

				int intPreLen = 0;//上一用户打的字符数

//				DateTime dtmPreInsertDate = new DateTime(1900,1,1);
				for(int i=0;i<objContentArr.Length;i++)
				{
					bruText.Color = objContentArr[i].objUserInfo.m_clrText;

					string strDrawText = strLineText.Substring(objContentArr[i].m_intStartIndex-m_intCurrentIndex,objContentArr[i].m_intEndIndex-objContentArr[i].m_intStartIndex+1);

					if(intPreLen > 0)
					{
						rgnInsertArr[0].First = 0;
						rgnInsertArr[0].Length = intPreLen;

						stfMeasure.SetMeasurableCharacterRanges(rgnInsertArr);					
					
						Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText.Substring(0,intPreLen),m_fntCharFont,rtfText,stfMeasure);
						
						RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

						p_objGrp.DrawString(strDrawText,m_fntCharFont,bruText,p_intX+rtfBounds.Width,p_intY);

						if(m_blnPrintModifySign && objContentArr[i].objUserInfo.m_intUserSequence != 1
							&& 
							(i > 0 ||
							m_intCurrentIndex <= intTempStart)
							)
						{
						
							clsPrintSignInfo objPrintInfo = new clsPrintSignInfo();
							objPrintInfo.m_blnIsDST = false;
							objPrintInfo.m_clrColor = Color.Red;
							objPrintInfo.m_strDesc = (objContentArr[i].objUserInfo.m_intUserSequence-1).ToString();

							SizeF szfDesc = p_objGrp.MeasureString(objPrintInfo.m_strDesc,m_fntDescFont);

							objPrintInfo.m_rtfBound.X = p_intX+rtfBounds.Width;
							objPrintInfo.m_rtfBound.Y = p_intY-10;
							objPrintInfo.m_rtfBound.Width = szfDesc.Width;
							objPrintInfo.m_rtfBound.Height = szfDesc.Height;
						
							m_arlTemp.Add(objPrintInfo);
						}
					}
					else
					{
						p_objGrp.DrawString(strDrawText,m_fntCharFont,bruText,p_intX,p_intY);

						if(m_blnPrintModifySign && objContentArr[i].objUserInfo.m_intUserSequence != 1
							&& 
							(i > 0 ||
							m_intCurrentIndex <= intTempStart)
							)
						{							
							clsPrintSignInfo objPrintInfo = new clsPrintSignInfo();
							objPrintInfo.m_blnIsDST = false;
							objPrintInfo.m_clrColor = Color.Red;
							objPrintInfo.m_strDesc = (objContentArr[i].objUserInfo.m_intUserSequence-1).ToString();
	
							SizeF szfDesc = p_objGrp.MeasureString(objPrintInfo.m_strDesc,m_fntDescFont);
	
							objPrintInfo.m_rtfBound.X = p_intX;
							objPrintInfo.m_rtfBound.Y = p_intY-10;
							objPrintInfo.m_rtfBound.Width = szfDesc.Width;
							objPrintInfo.m_rtfBound.Height = szfDesc.Height;
							
							m_arlTemp.Add(objPrintInfo);
						}
					}

					#region 打印上下标
					for(int j = 0; j < m_objSuperSubScriptArr.Length; j++)
					{
						int intScriptIndex = m_objSuperSubScriptArr[j].m_intIndex - m_intCurrentIndex;

						if(intScriptIndex >= 0 && intScriptIndex < strLineText.Length)
						{
							rgnInsertArr[0].First = 0;
							rgnInsertArr[0].Length = intScriptIndex;

							stfMeasure.SetMeasurableCharacterRanges(rgnInsertArr);					
					
							Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText.Substring(0,intScriptIndex),m_fntCharFont,rtfText,stfMeasure);
						
							RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

							int intScriptY = p_intY - 2;
							if(m_objSuperSubScriptArr[j].m_intCharOffset == ctlRichTextBox.c_intSubScriptCharOffSet)
                                intScriptY = p_intY + 8;
							p_objGrp.DrawString(m_objSuperSubScriptArr[j].m_strValue,m_fntScriptFont,bruText,p_intX+rtfBounds.Width,intScriptY);

//							SizeF sf = p_objGrp.MeasureString(strLineText.Substring(0,intScriptIndex),m_fntCharFont);
//							p_objGrp.DrawString(m_objSuperSubScriptArr[j].m_strValue,m_fntScriptFont,bruText,p_intX+sf.Width+2,p_intY);
						}
					}
					#endregion

					intPreLen += objContentArr[i].m_intEndIndex-objContentArr[i].m_intStartIndex+1;

				}

				objContentArr[0].m_intStartIndex = intTempStart;
				objContentArr[objContentArr.Length-1].m_intEndIndex = intTempEnd;
			}
			
			#endregion

			#region Handle DST
			Pen penDST = new Pen(Color.Red);						

			if(m_intCurrentDSTIndex >= m_objDSTInfoArr.Length)
			{
				m_intCurrentIndex += p_intCharPerLine;

				//把后面的换行符去掉
				if(m_intCurrentIndex < m_strText.Length
					&& m_strText[m_intCurrentIndex] == '\n')
					m_intCurrentIndex++;
				goto PrintSign;										
			}		
			
			ctlRichTextBox.clsDSTInfo objStartInfo = m_objDSTInfoArr[m_intCurrentDSTIndex];
			ctlRichTextBox.clsDSTInfo objEndInfo = m_objDSTInfoArr[m_intCurrentDSTIndex+1];
			int intStartDSTIndex = objStartInfo.m_intIndexRange;
			int intEndDSTIndex = objEndInfo.m_intIndexRange;

			int intBaseIndex = m_intCurrentIndex;

//			DateTime dtmPreDSTDate = new DateTime(1900,1,1);

			while(m_intCurrentIndex <= intEndIndex)
			{
				//当前索引小于双删除线的开始位，修正当前索引
				if(m_intCurrentIndex < intStartDSTIndex)
				{
					//双删除线位置不再本行
					if(intStartDSTIndex > intEndIndex)
					{
						m_intCurrentIndex = intEndIndex + 1;
						continue;
					}

					m_intCurrentIndex = intStartDSTIndex;
				}

				intStartDSTIndex = m_intCurrentIndex;

				if(intEndDSTIndex <= intEndIndex)
				{
					if(intEndDSTIndex >= intStartDSTIndex)
					{

						rgnDSTArr[0].First = intStartDSTIndex-intBaseIndex;
						rgnDSTArr[0].Length = intEndDSTIndex-intStartDSTIndex+1;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText,m_fntCharFont,rtfText,stfMeasure);

						RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

						if(m_blnPrintModifySign && objStartInfo.m_intUserSequence != 1 && intBaseIndex <= objStartInfo.m_intIndexRange)
						{						
							clsPrintSignInfo objPrintInfo = new clsPrintSignInfo();
							objPrintInfo.m_blnIsDST = true;
							objPrintInfo.m_clrColor = Color.Red;
							objPrintInfo.m_strDesc = (objStartInfo.m_intUserSequence-1).ToString();

							SizeF szfDesc = p_objGrp.MeasureString(objPrintInfo.m_strDesc,m_fntDescFont);

							objPrintInfo.m_rtfBound.X = rtfBounds.X;
							objPrintInfo.m_rtfBound.Y = rtfBounds.Y-10;
							objPrintInfo.m_rtfBound.Width = szfDesc.Width;
							objPrintInfo.m_rtfBound.Height = szfDesc.Height;
						
							m_arlTemp.Add(objPrintInfo);
						}

						if(!m_blnUnderLineDST)
						{
							p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
							p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
						}
						else
						{
							p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1);
							p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1+3);
						}
					}

					m_intCurrentDSTIndex += 2;

					if(m_intCurrentDSTIndex >= m_objDSTInfoArr.Length)
					{
						m_intCurrentIndex = intEndIndex+1;
						break;
					}

					m_intCurrentIndex = intEndDSTIndex;

					objStartInfo = m_objDSTInfoArr[m_intCurrentDSTIndex];
					objEndInfo = m_objDSTInfoArr[m_intCurrentDSTIndex+1];
					intStartDSTIndex = objStartInfo.m_intIndexRange;
					intEndDSTIndex = objEndInfo.m_intIndexRange;
				}
				else
				{
					intEndDSTIndex = intEndIndex;

					rgnDSTArr[0].First = intStartDSTIndex-intBaseIndex;
					rgnDSTArr[0].Length = intEndDSTIndex-intStartDSTIndex+1;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

					Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText,m_fntCharFont,rtfText,stfMeasure);

					RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

					if(m_blnPrintModifySign && objStartInfo.m_intUserSequence != 1 && intBaseIndex <= objStartInfo.m_intIndexRange)
					{						
						clsPrintSignInfo objPrintInfo = new clsPrintSignInfo();
						objPrintInfo.m_blnIsDST = true;
						objPrintInfo.m_clrColor = Color.Red;
						objPrintInfo.m_strDesc = (objStartInfo.m_intUserSequence-1).ToString();

						SizeF szfDesc = p_objGrp.MeasureString(objPrintInfo.m_strDesc,m_fntDescFont);

						objPrintInfo.m_rtfBound.X = rtfBounds.X;
						objPrintInfo.m_rtfBound.Y = rtfBounds.Y-10;
						objPrintInfo.m_rtfBound.Width = szfDesc.Width;
						objPrintInfo.m_rtfBound.Height = szfDesc.Height;
						
						m_arlTemp.Add(objPrintInfo);
					}

					if(!m_blnUnderLineDST)
					{
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
					else
					{
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1);
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1+3);
					}

					m_intCurrentIndex = intEndDSTIndex+1;		
					break;
				}				
			}
			#endregion

			#region 处理上下标
//			for(int i = 0; i < m_objSuperSubScriptArr.Length; i++)
//			{
//				int intScriptIndex = m_objSuperSubScriptArr[i].m_intIndex - m_intCurrentIndex;
//				if(intScriptIndex >= 0 && intScriptIndex < strLineText.Length)
//				{
//					SizeF sfPreText = p_objGrp.MeasureString(strLineText.Substring(0,intScriptIndex));
//					p_objGrp.DrawString(m_objSuperSubScriptArr[i].m_strValue,
//				}
//			}
			#endregion

			//把后面的换行符去掉
			if(m_intCurrentIndex < m_strText.Length
				&& m_strText[m_intCurrentIndex] == '\n')
				m_intCurrentIndex++;
			
			PrintSign:

				if(m_blnPrintModifySign)
				{
					bruText.Color = Color.Blue;
					m_arlTemp.Sort();
					for(int i=0;i<m_arlTemp.Count;i++)
					{
						clsPrintSignInfo objPrintInfo = (clsPrintSignInfo)m_arlTemp[i];
						if (objPrintInfo.m_strDesc!="-1")
						{
							p_objGrp.DrawString(objPrintInfo.m_strDesc,m_fntDescFont,bruText,objPrintInfo.m_rtfBound.X+4,objPrintInfo.m_rtfBound.Y);				
						
						}
					}
					m_arlTemp.Clear();
				}

			bruText.Dispose();
			penDST.Dispose();
			stfMeasure.Dispose();
		}

		public void m_mthPrintCorrectBeforeLineNoSign(int p_intX,int p_intY,Graphics p_objGrp,int p_intCharPerLine)
		{
			if(!m_BlnHaveNextLine())
				return;

			if(m_intCurrentIndex+p_intCharPerLine > m_strText.Length)
				p_intCharPerLine = m_strText.Length-m_intCurrentIndex;
			
			string strLineText = m_strText.Substring(m_intCurrentIndex,p_intCharPerLine);

			string []strSeparaterTextArr = strLineText.Split('\n');

			strLineText = strSeparaterTextArr[0];

			p_intCharPerLine = strLineText.Length;

			if(p_intCharPerLine == 0)
			{
				//只有一个换行符
				m_intCurrentIndex++;
				return;
			}


			int intEndIndex = m_intCurrentIndex+p_intCharPerLine-1;

			SolidBrush bruText = new SolidBrush(m_clrBaseColor);	

			CharacterRange []rgnInsertArr = new CharacterRange[1];
			rgnInsertArr[0] = new CharacterRange(0,0);	
			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			

			RectangleF rtfText = new RectangleF(p_intX,p_intY,10000,100);
			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			#region Handle Insert
			bool blnFirstGet = true;
			
			for(int i=0;i<m_objContentInfoArr.Length;i++)
			{
				ctlRichTextBox.clsUserContentInfo objContentInfo = m_objContentInfoArr[i];

				if(blnFirstGet)
				{
					if(m_intCurrentIndex >= objContentInfo.m_intStartIndex
						&& m_intCurrentIndex <= objContentInfo.m_intEndIndex)
					{
						m_arlTemp.Add(objContentInfo);

						if(intEndIndex >= objContentInfo.m_intStartIndex
							&& intEndIndex <= objContentInfo.m_intEndIndex)
						{
							break;
						}

						blnFirstGet = false;
					}
				}
				else
				{
					m_arlTemp.Add(objContentInfo);

					if(intEndIndex >= objContentInfo.m_intStartIndex
						&& intEndIndex <= objContentInfo.m_intEndIndex)
					{
						break;
					}
				}
			}

			ctlRichTextBox.clsUserContentInfo [] objContentArr = 
				(ctlRichTextBox.clsUserContentInfo [])m_arlTemp.ToArray(typeof(ctlRichTextBox.clsUserContentInfo));
			m_arlTemp.Clear();

			int intTempStart = objContentArr[0].m_intStartIndex;
			objContentArr[0].m_intStartIndex = m_intCurrentIndex;
			int intTempEnd = objContentArr[objContentArr.Length-1].m_intEndIndex;
			objContentArr[objContentArr.Length-1].m_intEndIndex = intEndIndex;

			int intPreLen = 0;

			DateTime dtmPreInsertDate = new DateTime(1900,1,1);
			for(int i=0;i<objContentArr.Length;i++)
			{
				bruText.Color = objContentArr[i].objUserInfo.m_clrText;

				string strDrawText = strLineText.Substring(objContentArr[i].m_intStartIndex-m_intCurrentIndex,objContentArr[i].m_intEndIndex-objContentArr[i].m_intStartIndex+1);

				if(intPreLen > 0)
				{
					rgnInsertArr[0].First = 0;
					rgnInsertArr[0].Length = intPreLen;

					stfMeasure.SetMeasurableCharacterRanges(rgnInsertArr);					
					
					Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText.Substring(0,intPreLen),m_fntCharFont,rtfText,stfMeasure);

					RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

					p_objGrp.DrawString(strDrawText,m_fntCharFont,bruText,p_intX+rtfBounds.Width,p_intY);
				}
				else
				{
					p_objGrp.DrawString(strDrawText,m_fntCharFont,bruText,p_intX,p_intY);

				}

				intPreLen += objContentArr[i].m_intEndIndex-objContentArr[i].m_intStartIndex+1;
			}

			objContentArr[0].m_intStartIndex = intTempStart;
			objContentArr[objContentArr.Length-1].m_intEndIndex = intTempEnd;
			#endregion

			#region Handle DST
			Pen penDST = new Pen(Color.Red);						

			if(m_intCurrentDSTIndex >= m_objDSTInfoArr.Length)
			{
				m_intCurrentIndex += p_intCharPerLine;

				//把后面的换行符去掉
				if(m_intCurrentIndex < m_strText.Length
					&& m_strText[m_intCurrentIndex] == '\n')
					m_intCurrentIndex++;
				goto PrintSign;										
			}		
			
			ctlRichTextBox.clsDSTInfo objStartInfo = m_objDSTInfoArr[m_intCurrentDSTIndex];
			ctlRichTextBox.clsDSTInfo objEndInfo = m_objDSTInfoArr[m_intCurrentDSTIndex+1];
			int intStartDSTIndex = objStartInfo.m_intIndexRange;
			int intEndDSTIndex = objEndInfo.m_intIndexRange;

			int intBaseIndex = m_intCurrentIndex;

			DateTime dtmPreDSTDate = new DateTime(1900,1,1);

			while(m_intCurrentIndex <= intEndIndex)
			{
				if(m_intCurrentIndex < intStartDSTIndex)
				{
					m_intCurrentIndex++;
					continue;
				}

				intStartDSTIndex = m_intCurrentIndex;

				if(intEndDSTIndex <= intEndIndex)
				{
					rgnDSTArr[0].First = intStartDSTIndex-intBaseIndex;
					rgnDSTArr[0].Length = intEndDSTIndex-intStartDSTIndex+1;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText,m_fntCharFont,rtfText,stfMeasure);

					RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

					if(!m_blnUnderLineDST)
					{
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
					else
					{
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1);
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1+3);
					}

					m_intCurrentDSTIndex += 2;

					if(m_intCurrentDSTIndex >= m_objDSTInfoArr.Length)
					{
						m_intCurrentIndex = intEndIndex+1;
						break;
					}

					m_intCurrentIndex = intEndDSTIndex;

					objStartInfo = m_objDSTInfoArr[m_intCurrentDSTIndex];
					objEndInfo = m_objDSTInfoArr[m_intCurrentDSTIndex+1];
					intStartDSTIndex = objStartInfo.m_intIndexRange;
					intEndDSTIndex = objEndInfo.m_intIndexRange;
				}
				else
				{
					intEndDSTIndex = intEndIndex;

					rgnDSTArr[0].First = intStartDSTIndex-intBaseIndex;
					rgnDSTArr[0].Length = intEndDSTIndex-intStartDSTIndex+1;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

					Region [] rgnDST = p_objGrp.MeasureCharacterRanges(strLineText,m_fntCharFont,rtfText,stfMeasure);

					RectangleF rtfBounds = rgnDST[0].GetBounds(p_objGrp);

					if(!m_blnUnderLineDST)
					{
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
					else
					{
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1);
						p_objGrp.DrawLine(penDST,rtfBounds.X,rtfBounds.Y+rtfBounds.Height-1+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height-1+3);
					}

					m_intCurrentIndex = intEndDSTIndex+1;		
					break;
				}				
			}
			#endregion

			//把后面的换行符去掉
			if(m_intCurrentIndex < m_strText.Length
				&& m_strText[m_intCurrentIndex] == '\n')
				m_intCurrentIndex++;
			
			PrintSign:

			bruText.Dispose();
			penDST.Dispose();
			stfMeasure.Dispose();
		}		

		private class clsPrintSignInfo : IComparable
		{
			public RectangleF m_rtfBound;

			public string m_strDesc;

			public Color m_clrColor;

			public bool m_blnIsDST;

			public int CompareTo(object obj)
			{
				clsPrintSignInfo objPrintInfo = (clsPrintSignInfo)obj;

				return (int)(this.m_rtfBound.X-objPrintInfo.m_rtfBound.X);
			}
		}

		#region 指定区域内打印
		/// <summary>
		/// 量度打印内容所需要的高度
		/// </summary>
		/// <param name="p_fltFontSize">打印字体大小</param>
		/// <param name="p_intWidth">打印区域的宽度</param>
		/// <param name="p_objGrp">打印变量</param>
		/// <returns>打印内容所需要的高度</returns>
		public int m_intMeasureBlockHeightBySimSun(float p_fltFontSize,int p_intWidth,Graphics p_objGrp)
		{
			if(m_strText == "" || m_strText.Length == 0)
				return 0;

			int intHeight;
			Rectangle rtgNone = new Rectangle(0,0,p_intWidth,1);
			StringFormat stfBlock = new StringFormat(StringFormatFlags.FitBlackBox);
			Font fntText = new Font("SimSun",p_fltFontSize);
			float fltHeight = fntText.GetHeight();
			int intMaxLine = (int)((float)rtgNone.Height/fltHeight)+1;
			rtgNone.Height = (int)(intMaxLine*fltHeight)+1;
			intHeight = rtgNone.Height;
			SizeF szfText = p_objGrp.MeasureString(m_strText,fntText,rtgNone.Width,stfBlock);
			if(rtgNone.Bottom < rtgNone.Top+szfText.Height)
				intHeight = (int)szfText.Height;

			//			m_blnPrintInBlock("SimSun",p_fltFontSize,rtgNone,p_objGrp,false,out intHeight,true,false);

			return intHeight;
		}
		/// <summary>
		/// 使用SimSun字体打印，如果内容超出范围也打印所有内容
		/// </summary>
		/// <param name="p_fltFontSize">打印字体大小</param>
		/// <param name="p_rtgBlock">打印区域</param>
		/// <param name="p_objGrp">打印变量</param>
		/// <param name="p_intRealHeight">实际需要打印的区域高度</param>
		/// <param name="p_blnPrintInMiddleBlock">如果打印区域可以容纳所有内容，是否自动调整内容在区域中间打印。true，调整；false，不调整</param>
		/// <returns>实际打印范围是否超出指定区域范围（指定区域范围是调整后的范围）</returns>
		public bool m_blnPrintAllBySimSun(float p_fltFontSize,Rectangle p_rtgBlock,Graphics p_objGrp,out int p_intRealHeight,bool p_blnPrintInMiddleBlock)
		{
			return m_blnPrintInBlock("SimSun",p_fltFontSize,p_rtgBlock,p_objGrp,true,out p_intRealHeight,true,p_blnPrintInMiddleBlock);
		}
		/// <summary>
		/// 在指定区域内打印
		/// </summary>
		/// <param name="p_strFontFamilyName">打印字体名称</param>
		/// <param name="p_fltFontSize">打印字体大小</param>
		/// <param name="p_rtgBlock">打印区域</param>
		/// <param name="p_objGrp">打印变量</param>
		/// <param name="p_blnNoClip">如果实际区域超出所给范围，是否打印。true，打印；false，不打印</param>
		/// <param name="p_intRealHeight">实际需要打印的区域高度</param>
		/// <param name="p_blnResizeBlock">是否自动调整打印区域为整行</param>
		/// <param name="p_blnPrintInMiddleBlock">如果打印区域可以容纳所有内容，是否自动调整内容在区域中间打印。true，调整；false，不调整</param>
		/// <returns>实际打印范围是否超出指定区域范围（如果自动调整区域，指定区域范围是调整后的范围）</returns>
		public bool m_blnPrintInBlock(string p_strFontFamilyName,float p_fltFontSize,Rectangle p_rtgBlock,Graphics p_objGrp,bool p_blnNoClip,out int p_intRealHeight,bool p_blnResizeBlock,bool p_blnPrintInMiddleBlock)
		{
			if(m_strText == "" || m_strText.Length == 0)
			{
				p_intRealHeight = 0;
				return false;
			}

			bool blnOutOfBlock = false;

			#region 初始化打印变量
			SolidBrush bruText = new SolidBrush(m_clrBaseColor);
			Pen penDST = new Pen(Color.White);
			Font fntText = new Font(p_strFontFamilyName,p_fltFontSize);			
			StringFormat stfBlock = new StringFormat(StringFormatFlags.FitBlackBox);
			#endregion 初始化打印变量

			float fltHeight = fntText.GetHeight();
			
			#region 调整打印区域大小
			if(p_blnResizeBlock)
			{
				int intMaxLine = (int)((float)p_rtgBlock.Height/fltHeight)+1;

				p_rtgBlock.Height = (int)(intMaxLine*fltHeight)+1;
			}
			#endregion 调整打印区域大小

			p_intRealHeight = p_rtgBlock.Height;

			SizeF szfText = p_objGrp.MeasureString(m_strText,fntText,p_rtgBlock.Width,stfBlock);			

			#region 调整打印内容在区域中心
			if(p_blnPrintInMiddleBlock)
			{
				//只在打印内容不能填满打印区域才调整到打印区域中间打印
				if(szfText.Height < p_rtgBlock.Height)
				{
					p_rtgBlock.Y += (int)(((float)p_rtgBlock.Height-szfText.Height)/2);
				}
			}
			#endregion 调整打印内容在区域中心

			#region 处理内容超出区域范围
			//使用MeasureString量度的结果，会比实际的大一点，但不会大于一行高度
			if(p_rtgBlock.Bottom < p_rtgBlock.Top+szfText.Height)
			{
				p_intRealHeight = (int)szfText.Height;

				if(p_rtgBlock.Bottom+fltHeight >= p_rtgBlock.Top+szfText.Height)
				{
					//由于误差引起，直接修改
					p_rtgBlock.Height = p_intRealHeight;
				}
				else
				{
					blnOutOfBlock = true;
					if(p_blnNoClip)
					{
						p_rtgBlock.Height = p_intRealHeight;					
					}	
					else
					{
						goto Release;
					}	
				}
			}
			#endregion 处理内容超出区域范围

			//打印所有内容
			p_objGrp.DrawString(m_strText,fntText,bruText,p_rtgBlock,stfBlock);			

			#region 打印其它颜色文字、双划线和签名
			if(m_enmPrintType == enmPrintType.CorrectBefore)
			{
				m_arlTemp.Clear();

				CharacterRange [] objCRArr = new CharacterRange[1];
				objCRArr[0] = new CharacterRange();

				Matrix objMatrix = new Matrix();
					
				#region 打印其它颜色文字
				if(m_objContentInfoArr != null && m_objContentInfoArr.Length > 0)
				{
					System.Drawing.Drawing2D.GraphicsContainer objInsertContain = p_objGrp.BeginContainer();
			
					for(int i1=0;i1<m_objContentInfoArr.Length;i1++)
					{
						if(m_objContentInfoArr[i1] != null && m_objContentInfoArr[i1].objUserInfo != null)
						{
							objCRArr[0].First = m_objContentInfoArr[i1].m_intStartIndex;
							objCRArr[0].Length = m_objContentInfoArr[i1].m_intEndIndex-m_objContentInfoArr[i1].m_intStartIndex+1;
							stfBlock.SetMeasurableCharacterRanges(objCRArr);

							try
							{
								System.Drawing.Region[] objRnArr = p_objGrp.MeasureCharacterRanges(m_strText,fntText,p_rtgBlock,stfBlock);
								if(objRnArr != null && objRnArr.Length == 1)
								{
									bruText.Color = m_objContentInfoArr[i1].objUserInfo.m_clrText;

									p_objGrp.SetClip(objRnArr[0],System.Drawing.Drawing2D.CombineMode.Replace);
									p_objGrp.FillRegion(Brushes.White,objRnArr[0]);
									p_objGrp.DrawString(m_strText,fntText,bruText,p_rtgBlock,stfBlock);								
									
									//获取签名信息
									if(m_blnPrintModifySign && m_objContentInfoArr[i1].objUserInfo.m_intUserSequence != 1)
									{						
										RectangleF [] rtgInsertRect = objRnArr[0].GetRegionScans(objMatrix);

										if(rtgInsertRect != null && rtgInsertRect.Length > 0)
										{
											clsPrintSignInfo objPrintInfo = new clsPrintSignInfo();
											objPrintInfo.m_blnIsDST = false;
											objPrintInfo.m_clrColor = Color.Red;
											objPrintInfo.m_strDesc = (m_objContentInfoArr[i1].objUserInfo.m_intUserSequence-1).ToString();

											objPrintInfo.m_rtfBound.X = rtgInsertRect[0].Left;
											objPrintInfo.m_rtfBound.Y = rtgInsertRect[0].Top;
							
											m_arlTemp.Add(objPrintInfo);
										}
									}

									objRnArr[0].Dispose();
								}
							}
							catch(Exception exp)
							{
								string strtemp=exp.Message;
							}


//							System.Drawing.Region[] objRnArr = p_objGrp.MeasureCharacterRanges(m_strText,fntText,p_rtgBlock,stfBlock);

							
						}
					}

					p_objGrp.EndContainer(objInsertContain);
					
				}
				#endregion 打印其它颜色文字

				#region 打印上下标
				if(m_objSuperSubScriptArr != null && m_objSuperSubScriptArr.Length > 0)
				{
					try
					{
						for(int j = 0; j < m_objSuperSubScriptArr.Length; j++)
						{
	//						objCRArr[0].First = 0;
	//						objCRArr[0].Length = m_objSuperSubScriptArr[j].m_intIndex;
							objCRArr[0].First = m_objSuperSubScriptArr[j].m_intIndex;
							objCRArr[0].Length = m_objSuperSubScriptArr[j].m_strValue.Length;

							RectangleF rtfText = new RectangleF(p_rtgBlock.X,p_rtgBlock.Y,10000,100);
	//						StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);
	//						stfMeasure.SetMeasurableCharacterRanges(objCRArr);
							stfBlock.SetMeasurableCharacterRanges(objCRArr);

	//						Region [] objRnArr = p_objGrp.MeasureCharacterRanges(m_strText,fntText,rtfText,stfMeasure);
							Region [] objRnArr = p_objGrp.MeasureCharacterRanges(m_strText,fntText,p_rtgBlock,stfBlock);
											
							RectangleF rtfBounds = objRnArr[0].GetBounds(p_objGrp);

							if(m_objSuperSubScriptArr[j].m_intCharOffset == ctlRichTextBox.c_intSubScriptCharOffSet)
								rtfBounds.Y += 6;
							else
								rtfBounds.Y -= 3;
	//						p_objGrp.DrawString(m_objSuperSubScriptArr[j].m_strValue,m_fntScriptFont,bruText,rtfBounds.Right,fltScriptY);
							p_objGrp.DrawString(m_objSuperSubScriptArr[j].m_strValue,m_fntScriptFont,bruText,rtfBounds);

	//						p_objGrp.DrawString(rtfBounds.Width.ToString(),fntText,bruText,10,300);
						}
					}
					catch(Exception exp)
					{
						string strtemp=exp.Message;
					}
				}
				#endregion

				#region 打印双划线
				if(m_objDSTInfoArr != null && m_objDSTInfoArr.Length > 0)
				{
					try
					{
						for(int i1=0;i1<m_objDSTInfoArr.Length/2;i1++)
						{
							int intDSTStart = i1*2;
							int intDSTEnd = i1*2+1;
							
							if(m_objDSTInfoArr[intDSTStart] != null && m_objDSTInfoArr[intDSTEnd] != null)
							{
								objCRArr[0].First = m_objDSTInfoArr[intDSTStart].m_intIndexRange;
								objCRArr[0].Length = m_objDSTInfoArr[intDSTEnd].m_intIndexRange-m_objDSTInfoArr[intDSTStart].m_intIndexRange+1;
								stfBlock.SetMeasurableCharacterRanges(objCRArr);

								System.Drawing.Region[] objRnArr = p_objGrp.MeasureCharacterRanges(m_strText,fntText,p_rtgBlock,stfBlock);
								
								if(objRnArr != null && objRnArr.Length == 1)
								{
									RectangleF [] rtgDSTRect = objRnArr[0].GetRegionScans(objMatrix);

									if(rtgDSTRect != null)
									{
										penDST.Color = m_objDSTInfoArr[intDSTStart].m_clrDST;

										for(int j2=0;j2<rtgDSTRect.Length;j2++)
										{										
											p_objGrp.DrawLine(penDST,rtgDSTRect[j2].Left,rtgDSTRect[j2].Top+rtgDSTRect[j2].Height/2-2,rtgDSTRect[j2].Right,rtgDSTRect[j2].Top+rtgDSTRect[j2].Height/2-2);
											p_objGrp.DrawLine(penDST,rtgDSTRect[j2].Left,rtgDSTRect[j2].Top+rtgDSTRect[j2].Height/2+2,rtgDSTRect[j2].Right,rtgDSTRect[j2].Top+rtgDSTRect[j2].Height/2+2);
										}
									}

									//获取签名信息
									if(m_blnPrintModifySign && m_objDSTInfoArr[intDSTStart].m_intUserSequence != 1)
									{						
										if(rtgDSTRect != null && rtgDSTRect.Length > 0)
										{
											clsPrintSignInfo objPrintInfo = new clsPrintSignInfo();
											objPrintInfo.m_blnIsDST = false;
											objPrintInfo.m_clrColor = Color.Red;
											objPrintInfo.m_strDesc = (m_objDSTInfoArr[intDSTStart].m_intUserSequence-1).ToString();

											objPrintInfo.m_rtfBound.X = rtgDSTRect[0].Left;
											objPrintInfo.m_rtfBound.Y = rtgDSTRect[0].Top;
							
											m_arlTemp.Add(objPrintInfo);
										}
									}

									objRnArr[0].Dispose();
								}
							}

						}
					}
					catch(Exception exp)
					{
						string strtemp=exp.Message;
					}
				}
				#endregion 打印双划线

				#region 打印签名
				if(m_blnPrintModifySign)
				{
					bruText.Color = Color.Blue;
					m_arlTemp.Sort();

					for(int i=0;i<m_arlTemp.Count;i++)
					{
						clsPrintSignInfo objPrintInfo = (clsPrintSignInfo)m_arlTemp[i];

						p_objGrp.DrawString(objPrintInfo.m_strDesc,m_fntDescFont,bruText,objPrintInfo.m_rtfBound.X+4,objPrintInfo.m_rtfBound.Y-5);				
					}
					m_arlTemp.Clear();
				}
				#endregion 打印签名

				objMatrix.Dispose();
			}
			#endregion 打印其它颜色文字、双划线和签名

			Release:
			#region 释放打印变量
			bruText.Dispose();
			fntText.Dispose();
			stfBlock.Dispose();
			penDST.Dispose();
			#endregion 释放打印变量

			return blnOutOfBlock;
		}
		#endregion 制定区域内打印
	}
}
