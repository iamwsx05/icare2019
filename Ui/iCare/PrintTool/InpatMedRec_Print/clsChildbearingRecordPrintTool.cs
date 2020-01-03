using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;


namespace iCare
{
	/// <summary>
	/// ����������ʱ��¼��ӡ������
	/// </summary>
	public class clsChildbearingRecordPrintTool : clsInpatMedRecPrintBase
	{
		public clsChildbearingRecordPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo(),
																	   new clsPrintInPatMedRecCaseMain(),
																		   new clsPrintInPatApgar(),
																			new clsPrintInPatMedDocAndDate()
																	   });			
		}

		
		#region ��ӡʵ��

		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				          
				p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,40);
		
				p_objGrp.DrawString("�� �� �� �� �� ʱ �� ¼",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,310,70);
			
				p_objGrp.DrawString("ĸ��סԺ�ţ�",p_fntNormalText,Brushes.Black,550,110);
				p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID ,p_fntNormalText,Brushes.Black,640,110);	
				p_intPosY =150;
				m_blnHaveMoreLine = false;
				#region
//				p_objGrp.DrawString("����������ʱ��¼����",m_fotItemHead,Brushes.Black,m_intRecBaseX+250,p_intPosY - 10);
//		
//				p_intPosY += 20;
//				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				p_objGrp.DrawString("��¼���ڣ�"+(m_objContent==null ? "": m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
//		
//				p_intPosY += 20;
//				p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�"+(m_objContent==null ? "": m_objContent.m_strRepresentor+"," +m_objContent.m_strCredibility),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
//		
//				p_intPosY += 20;
//				p_objGrp.DrawString("�Ա�"+ m_objPrintInfo.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);	
//				p_objGrp.DrawString("�����أ�"+ m_objPrintInfo.m_strNativePlace ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			
//
//				p_intPosY += 20;
//				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strInPatientID ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
//		
//				p_intPosY += 20;
//				p_objGrp.DrawString("ְҵ��"+ m_objPrintInfo.m_strOccupation ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				p_objGrp.DrawString("��ϵ�ˣ�"+m_objPrintInfo.m_strLinkManFirstName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
//		
//				p_intPosY += 20;
//				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strMarried,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				p_objGrp.DrawString("�绰��"+ m_objPrintInfo.m_strHomePhone ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
//
//				p_intPosY += 20;
//				p_objGrp.DrawString("���壺"+ m_objPrintInfo.m_strNationality ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				p_objGrp.DrawString("������λ��"+ m_objPrintInfo.m_strOfficeName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);	
//		
//				p_intPosY += 20;
//				if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
//				}
//				else
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//				}				
//			
//				m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��"+ m_objPrintInfo.m_strHomeAddress ,"<root />");
//				int intRealHeight;
//				Rectangle rtgBlock = new Rectangle(m_intPatientInfoX+350,p_intPosY,(int)enmRectangleInfo.RightX-(m_intPatientInfoX+350),30);
//				m_objPrintContext.m_blnPrintAllBySimSun(11,rtgBlock,p_objGrp,out intRealHeight,false);
				#endregion

			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}

		#endregion
		protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{}
		protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{
		}

		#region ĸ������,�������
		/// <summary>
		/// ĸ������,�������
		/// </summary>
		private class clsPrintInPatMedRecCaseMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			//private string[] m_strKeysArr1 = {"ĸ������","Ӥ���Ա�>>��","Ӥ���Ա�>>Ů"};
			private string[] m_strKeysArr1 = {"ĸ������",""};
			private string[] m_strKeysArr2 = {"Ӥ���Ա�","Ӥ���Ա�>>��","Ӥ���Ա�>>Ů"};
			private string[] m_strKeysArr3 = {"����>>ʱ��"};
			private string[] m_strKeysArr4 = {"","�������>>����","�������>>����","�������>>����ܾ�","�������>>��","�������>>����"};
			private string[] m_strKeysArr5 = {"","�������>>ͷ��","�������>>��״","�������>>��","�������>>��","�������>>��","�������>>��ǻ"};
			
			private string[] m_strKeysArr7 = {"","�������>>�ز�","�������>>��","�������>>��"};
			private string[] m_strKeysArr8 = {"","�������>>������","�������>>Ƣ","�������>>���"};
			private string[] m_strKeysArr9 = {"","�������>>��֫","�������>>��ֳ��","�������>>�ؿ�"};

			private string[] m_strKeysArr11 = {"","�������>>����>>����","�������>>����>>�۾���ҩ"};
			private string[] m_strKeysArr12 = {"","�������>>����>>��ע"};

			  //m_mthMakeText(new string[]{"����ʱ��:","","#��$$","","#��$$","","#��$$","","#��$$","","#ʱ$$","","#��$$"},m_strKeysArr3,ref strAllText,ref strXml);
						
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{"ĸ��������","    "},m_strKeysArr1,ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"ĸ��������Ӥ���Ա�>>�У�Ӥ���Ա�>>Ů"},m_strKeysArr1,ref strAllText,ref strXml);
					
							m_mthMakeCheckText(m_strKeysArr2,ref strAllText,ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr3) != false)
                            {
                                m_strDateType = "yyyy��MM��dd�� HH:mm:ss";
                                m_mthMakeText(new string[] { "    ����ʱ�䣺" }, m_strKeysArr3, ref strAllText, ref strXml);
                            }
						m_mthMakeText(new string[]{"\n������Σ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n    ","������","������","����ܾ�(cm)��","��(cm)��","����(g)��"},m_strKeysArr4,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n    ","ͷ����","��״��","��      ��","��  ��","��  ��","��ǻ ��"},m_strKeysArr5,ref strAllText,ref strXml);

						m_mthMakeText(new string[]{"\n    ","�ز���","��  ��","��      ��"},m_strKeysArr7,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n    ","�����Σ�","Ƣ  ��","�����"},m_strKeysArr8,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n    ","��֫��","��ֳ����","�ؿ�  ��"},m_strKeysArr9,ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n    ","���Σ�","�۾���ҩ��"},m_strKeysArr11,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n    ","��ע��"},m_strKeysArr12,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					//m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;
				}
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
				}
				else
				{
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}
		#endregion

		#region Apgar���ֱ�
		/// <summary>
		/// Apgar���ֱ�
		/// </summary>
		private class clsPrintInPatApgar : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			private Font m_fontItem = new Font("����",10);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {"Apgar���ֱ�>>����/����>>0","Apgar���ֱ�>>����/����>>С��100��","Apgar���ֱ�>>����/����>>����100��"};
			private string[] m_strKeysArr2 = {"Apgar���ֱ�>>����>>0 ","Apgar���ֱ�>>����>>ǳ��������","Apgar���ֱ�>>����>>�ѿ�����"};
			private string[] m_strKeysArr3 = {"Apgar���ֱ�>>������>>�ɳ�","Apgar���ֱ�>>������>>��֫����","Apgar���ֱ�>>������>>��֫�"};
			private string[] m_strKeysArr4 = {"Apgar���ֱ�>>�̼�����>>�޷�Ӧ","Apgar���ֱ�>>�̼�����>>��Щ����","Apgar���ֱ�>>�̼�����>>����ҭ"};
			private string[] m_strKeysArr5 = {"Apgar���ֱ�>>Ƥ����ɫ>>���ϲ԰�","Apgar���ֱ�>>Ƥ����ɫ>>���ɺ���֫��","Apgar���ֱ�>>Ƥ����ɫ>>ȫ�����"};
			private string[] m_strKeysArr6 = {"Apgar���ֱ�>>�ܷ�>>һ��","Apgar���ֱ�>>�ܷ�>>����","Apgar���ֱ�>>�ܷ�>>����"};
            private string[] m_strKeysArr7 = { "Apgar���ֱ�>>�ܷ�>>�ܷ�", "Apgar���ֱ�>>�ܷ�>>�ܷ�", "Apgar���ֱ�>>�ܷ�>>�ܷ�" };
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if( !(m_blnHavePrintInfo(m_strKeysArr1) == true || m_blnHavePrintInfo(m_strKeysArr2) == true 
					|| m_blnHavePrintInfo(m_strKeysArr3) == true|| m_blnHavePrintInfo(m_strKeysArr4) == true|| m_blnHavePrintInfo(m_strKeysArr5) == true
					|| m_blnHavePrintInfo(m_strKeysArr6) == true))
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					SizeF size = p_objGrp.MeasureString("Apgar���ֱ�",m_fontItemMidHead);
					int intMiddle = (clsPrintPosition.c_intRightX - clsPrintPosition.c_intLeftX)/2 - Convert.ToInt32(size.Width/2);
					p_intPosY += 20;
					p_objGrp.DrawString("Apgar���ֱ�",m_fontItemMidHead,Brushes.Black,intMiddle,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
//						if(m_blnHavePrintInfo(m_strKeysArr) != false)
//							m_mthMakeText(new string[]{"��Ӫ����","��������"},m_strKeysArr,ref strAllText,ref strXml);
////						m_mthMakeCheckText(m_strKeysArr1,ref strAllText,ref strXml);
//						if(m_blnHavePrintInfo(m_strKeysArr) != false)
//							m_mthMakeText(new string[]{"�������˶���","������"},m_strKeysArr2,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("Apgar���ֱ�",m_objPrintContext.m_ObjModifyUserArr);

					m_blnIsFirstPrint = false;	
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
					p_intPosY += 20;
					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					p_intPosY += 10;
					m_mthPrintSportTable(ref p_intPosY, p_objGrp, p_fntNormalText);
					m_blnHaveMoreLine = false;
				}
			}
			#region ��ӡ���
			private void m_mthPrintSportTable(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				int intLineWidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth;
				int intRowStep = m_intRecBaseX + 10;
				int intPosY = p_intPosY;
				int inttempY = p_intPosY;
				int intInTableInitY = p_intPosY; //���ڴ��ݸ������ݿ����ݺ����ĳ�ʼ����
				int intDiv = intLineWidth/5;
				int intPing = intDiv/3;
				int intRowHeigth = 30;

				int int2Score =0;
				int int3Score =0;
				int X=0;
				int Y=0;
				string printStr = "";

				#region ������
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth,intPosY);
						intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth,intPosY);

				intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth/5*4,intPosY);
				intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth/5*4,intPosY);
				intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth/5*4,intPosY);
				intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth/5*4,intPosY);
				intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth,intPosY);
				intPosY += intRowHeigth;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth,intPosY);

				#endregion 

				#region ȡ������
				clsInpatMedRec_Item[] strRet1 = m_mthPrintSubItem(new string[]{"","",""},m_strKeysArr1);
				clsInpatMedRec_Item[] strRet2 = m_mthPrintSubItem(new string[]{"","",""},m_strKeysArr2);
				clsInpatMedRec_Item[] strRet3 = m_mthPrintSubItem(new string[]{"","",""},m_strKeysArr3);
				clsInpatMedRec_Item[] strRet4 = m_mthPrintSubItem(new string[]{"","",""},m_strKeysArr4);
				clsInpatMedRec_Item[] strRet5 = m_mthPrintSubItem(new string[]{"","",""},m_strKeysArr5);
				string[] strRet6 = m_mthPrintTextItem(new string[]{"","",""},m_strKeysArr6);
                string[] strRet7 = m_mthPrintTextItem(new string[] { "", "", "" }, m_strKeysArr7);
				#endregion 

				#region ������
				p_objGrp.DrawLine(Pens.Black,intRowStep,p_intPosY,intRowStep ,intPosY);
				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv,p_intPosY,intRowStep + intDiv,intPosY);
				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv*2,p_intPosY,intRowStep + intDiv*2,intPosY - intRowHeigth);
				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv*3,p_intPosY,intRowStep + intDiv*3,intPosY - intRowHeigth);
				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv*4,p_intPosY,intRowStep + intDiv*4,intPosY);
				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv*5,p_intPosY,intRowStep + intDiv*5,intPosY);

				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv*4 + intPing,p_intPosY,intRowStep + intDiv*4 +intPing,intPosY);
				p_objGrp.DrawLine(Pens.Black,intRowStep + intDiv*4 + intPing*2,p_intPosY,intRowStep + intDiv*4 +intPing*2,intPosY);
				#endregion 
	
				#region ��ʵ���һ��
				printStr = "����";
				SizeF size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				 X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				printStr = "0��";
				size = p_objGrp.MeasureString(printStr,m_fontItem);				
				X = intRowStep + intDiv + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				printStr = "1��";
				size = p_objGrp.MeasureString(printStr,m_fontItem);				
				X = intRowStep + intDiv*2 + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				printStr = "2��";
				size = p_objGrp.MeasureString(printStr,m_fontItem);				
				X = intRowStep + intDiv*3 + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				printStr = "һ��";
				size = p_objGrp.MeasureString(printStr,m_fontItem);				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				printStr = "����";
				size = p_objGrp.MeasureString(printStr,m_fontItem);				
				X = intRowStep + intDiv*4 + intPing +intPing/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				int2Score = X;

				printStr = "����";
				size = p_objGrp.MeasureString(printStr,m_fontItem);				
				X = intRowStep + intDiv*4 + intPing*2 + intPing/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				int3Score = X;
				//
				inttempY  = inttempY + intRowHeigth;
				//

				#endregion 

				#region ��һ��
				printStr = "����/����";
				 size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				//�����Ӧ������
				m_mthPrintStr(Y,strRet1,p_objGrp,new string[]{"0","<100����",">100����"});
				//
	
				inttempY  = inttempY + intRowHeigth;
				//
				printStr = "����";
				 size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);
				//�����Ӧ������
				m_mthPrintStr(Y,strRet2,p_objGrp,new string[]{"0","ǳ��������","�ѿ�����"});
				//
				//
				inttempY  = inttempY + intRowHeigth;
				//
				printStr = "������";
				 size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);
				//�����Ӧ������
				m_mthPrintStr(Y,strRet3,p_objGrp,new string[]{"�ɳ�","��֫����","��֫�"});
				//
				//
				inttempY  = inttempY + intRowHeigth;
				//
				printStr = "�̼�����";
				 size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);
				//�����Ӧ������
				m_mthPrintStr(Y,strRet4,p_objGrp,new string[]{"�޷�Ӧ","�ж�����ü","����ҭ"});
				//
				//
				inttempY  = inttempY + intRowHeigth;
				//
				printStr = "Ƥ����ɫ";
				 size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);
				//�����Ӧ������
				m_mthPrintStr(Y,strRet5,p_objGrp,new string[]{"���ϲ԰�","���ɺ���֫��","ȫ�����"});
				//
				//
				inttempY  = inttempY + intRowHeigth;
				//
				printStr = "�ܷ�";
				 size = p_objGrp.MeasureString(printStr,m_fontItem);
				Y = intRowHeigth/2 + inttempY - Convert.ToInt32(size.Height/2);
				X = intRowStep + intDiv/2 - Convert.ToInt32(size.Width/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);
				//�����Ӧ������
				int tX = intRowStep + intDiv + intDiv*3/2 - Convert.ToInt32(size.Width/4);

				p_objGrp.DrawString(strRet7[0].ToString(),m_fontItem,Brushes.Black,tX,Y);
				tX = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/4);
				p_objGrp.DrawString(strRet6[0].ToString(),m_fontItem,Brushes.Black,tX,Y);
				tX = intRowStep + intDiv*4 + intPing/2 + intPing - Convert.ToInt32(size.Width/4);
				p_objGrp.DrawString(strRet6[1].ToString(),m_fontItem,Brushes.Black,tX,Y);
				tX = intRowStep + intDiv*4 + intPing/2 + intPing*2 - Convert.ToInt32(size.Width/4);
				p_objGrp.DrawString(strRet6[2].ToString(),m_fontItem,Brushes.Black,tX,Y);
				
				//
				
				#endregion 

				#region ���������(����һ�������֣�
				int intZiHeight = Convert.ToInt32(size.Height*1);
				int2Score = intRowStep + intDiv*4 + intPing + intPing/2 - Convert.ToInt32(size.Width/2);
				int3Score = intRowStep + intDiv*4 + intPing*2 + intPing/2 - Convert.ToInt32(size.Width/2);
				printStr = "��";
				size = p_objGrp.MeasureString(printStr,m_fontItem);
                X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);

				printStr = "һ";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) - intZiHeight;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				printStr = "��";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) - intZiHeight;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);

				printStr = "ʮ";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) - intZiHeight;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);


				printStr = "��";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) - intZiHeight*2;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);

				printStr = "��";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) - intZiHeight*3;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);

				printStr = "��";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) + intZiHeight;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);

				printStr = "��";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) + intZiHeight*2;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);

				printStr = "��";				
				X = intRowStep + intDiv*4 + intPing/2 - Convert.ToInt32(size.Width/2);
				Y = inttempY - intRowHeigth*5 /2 - Convert.ToInt32(size.Height/2) + intZiHeight*3;
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,X,Y);

				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int2Score,Y);
				p_objGrp.DrawString(printStr,m_fontItem,Brushes.Black,int3Score,Y);


				#endregion 

				p_intPosY += intRowHeigth * 8 ;
			}

			private void m_mthPrintStr(int p_intY,clsInpatMedRec_Item[] p_strPrintArr,System.Drawing.Graphics p_objGrp,string[] strText)
			{
				
				int intX = 0;
				int intLineWidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth;
					int intDiv = intLineWidth/5;
				for(int i=0;i<p_strPrintArr.Length; i++)
				{
				clsInpatMedRec_Item strTemp = p_strPrintArr[i];
					if(strTemp != null)
					{
						SizeF size = p_objGrp.MeasureString(strText[i].ToString(),m_fontItem);
						intX = m_intRecBaseX + 10 + intDiv*(i+1) + intDiv/2 - Convert.ToInt32(size.Width/2);
						p_objGrp.DrawString(strText[i].ToString()+" ��",m_fontItem,Brushes.Black,intX,p_intY);
					}
					else
					{
						SizeF size2 = p_objGrp.MeasureString(strText[i].ToString(),m_fontItem);
						intX = m_intRecBaseX + 10 + intDiv*(i+1) + intDiv/2 - Convert.ToInt32(size2.Width/2);
						p_objGrp.DrawString(strText[i].ToString(),m_fontItem,Brushes.Black,intX,p_intY);
				
					}
				}
			}
			private clsInpatMedRec_Item[] m_mthPrintSubItem(string[] p_strTitleArr,string[] p_strKeyArr)
			{
				clsInpatMedRec_Item[] objConArr = m_objGetContentFromItemArr(p_strKeyArr);
				if(objConArr == null || objConArr.Length != 3 || p_strTitleArr == null)
					return null;
//
//				clsInpatMedRec_Item[] strConArr = new clsInpatMedRec_Item[objConArr.Length];
//				for(int i=0;i<objConArr.Length; i++)
//				{
//					if(objConArr[i]!=null)
//					if(objConArr[i].m_strItemContent != null)
//						strConArr[i].m_strItemContent = objConArr[i].m_strItemContent;
//					else
//						strConArr[i].m_strItemContent = "";
//				}				
				//				return strConArr;
								return objConArr;
			}

			private string[] m_mthPrintTextItem(string[] p_strTitleArr,string[] p_strKeyArr)
			{
				clsInpatMedRec_Item[] objConArr = m_objGetContentFromItemArr(p_strKeyArr);
				if(objConArr == null || objConArr.Length != 3 || p_strTitleArr == null)
					return null;
				
								string[] strConArr = new string[objConArr.Length];
								for(int i=0;i<objConArr.Length; i++)
								{
									if(objConArr[i]!=null)
									{
										if(objConArr[i].m_strItemContent != null)
											strConArr[i] = objConArr[i].m_strItemContent;
										else
											strConArr[i] = "";
									}
									else
											strConArr[i] = "";
								}				
								return strConArr;
		
			}
			#endregion

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}

		#endregion

		#region ָӡ����ҽʦ����¼����
		/// <summary>
		///  ָӡ����ҽʦ����¼����
		/// </summary>
		private class clsPrintInPatMedDocAndDate : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
			private string[] m_strKeysArr1 = {"ҽ��ǩ��"};
			private string[] m_strKeysArr2 = {"��¼����"};
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false )
				{
					m_blnHaveMoreLine = false;
					return;
				}
//				if(blnNextPage)
//				{
//					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
//					m_blnHaveMoreLine = true;
//					blnNextPage = false;
//					p_intPosY += 1500;
//					return;
//				}
				if(m_blnIsFirstPrint)
				{
                    p_objGrp.DrawString("ĸ������Ĵָָӡ��", m_fotItemHead, Brushes.Black, m_intRecBaseX + 30, p_intPosY);

                    p_objGrp.DrawString("�����������ӡ��", m_fotItemHead, Brushes.Black, m_intRecBaseX + 400, p_intPosY);
                    p_intPosY += 250;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
								if(m_blnHavePrintInfo(m_strKeysArr1) != false)
						m_mthMakeText(new string[]{"���ҽʦ��"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\n��¼���ڣ�"},m_strKeysArr2,ref strAllText,ref strXml);
						
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
                    strAllText = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(strAllText, strXml);
                    p_objGrp.DrawString(strAllText, new Font("", 10), Brushes.Black, m_intRecBaseX + 450, 980);
                   
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
                    //m_mthAddSign2("ҽ��ǩ�֣�", m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                    return;
				}

				int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 450, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
				}
				else
				{
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}
		#endregion


		#endregion
	}

}
